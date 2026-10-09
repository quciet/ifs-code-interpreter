Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic

Imports IFs.Data
'jrs 2023/10/03
Imports System.Globalization
Imports System.Threading
Imports Microsoft.VisualBasic.Compatibility
Imports System.Drawing
Imports System.Diagnostics.Metrics
Imports System.Diagnostics.PerformanceData
Imports NLog.LayoutRenderers
Imports Accord.IO



Partial Class frm_TableDisplay
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

        'jrs 2023/10/03
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")

        InitializeComponent()

        'jrs 2023/10/10
        'Reset First Year if set in Graph
        If Session("OriginalFirstYear") <> Nothing Then
            Session("FirstYear") = Session("OriginalFirstYear")
            Session("OriginalFirstYear") = Nothing
            Session("UserFirstYear") = Nothing
            Session("RowAdjustment") = 0
            'jrs 2024/09/04
            If Session("mnuUseStartYrDivby5") Then
                Session("mnuUseStartYrDivby5") = False
                Session("StartYrIdx") = 1
            End If
        End If


        'jrs 2012/02/22
        If Session("PlotLabelsPE") Is Nothing Then
            'JRS 12/08/03
            bLoadLongitudinalBFYH = False
            iFirstYearBFYH = FirstYearHistory
        Else
            If IsNumeric(Session("PlotLabelsPE")(1, 1)) Then
                If CInt(Session("PlotLabelsPE")(1, 1)) < FirstYearHistory Then
                    bLoadLongitudinalBFYH = True
                    iFirstYearBFYH = CInt(Session("PlotLabelsPE")(1, 1))
                Else
                    bLoadLongitudinalBFYH = False
                    iFirstYearBFYH = FirstYearHistory
                End If
            Else
                bLoadLongitudinalBFYH = False
                iFirstYearBFYH = FirstYearHistory
            End If
        End If

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If

            'jrs 2016/03/30
            If Session("StartYrIdx") = Nothing Then
                Session("StartYrIdx") = 1
            End If

            'jrs 2009/01/30
            ''JRS 1/14/04
            'ReDim SPLOT(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            ''jrs 2005/06/17
            'If IsNothing(Session("SPLOT")) Then
            '    Session("SPLOT") = SPLOT
            'Else
            '    If UBound(Session("PLOT"), 1) = UBound(Session("SPLOT"), 1) And UBound(Session("PLOT"), 2) = UBound(Session("SPLOT"), 2) Then
            '        SPLOT = Session("SPLOT")
            '    Else
            '        Session("SPLOT") = SPLOT
            '    End If
            'End If
            ReDim Preserve Session("Percent_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            ReDim Preserve Session("IntervalAvg_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            ReDim Preserve Session("MovingAvg_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            ReDim Preserve Session("Validation_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            'jrs 2011/07/01
            ReDim Preserve Session("IntervalTot_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            ReDim Preserve Session("IntervalAbs_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            'jrs 2012/01/02
            ReDim Preserve Session("RatioFstYr_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))
            'jrs 2013/02/20
            ReDim Preserve Session("Unit_PLOT")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))

            'JAM 2016-05-16
            ReDim Preserve Session("PLOTTrillions")(UBound(Session("PLOT"), 1), UBound(Session("PLOT"), 2))

            Dim i As Integer
            Dim j As Integer = 0

            'JRS 11/03/03
            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
                'jrs 2006/09/25
                'If Session("buseAllHis") And UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                '    For i = 1 To Session("COUNTE")
                '        If Session("FinYearHis")(i) > Session("DisplayLastYear") Then
                '            Session("DisplayLastYear") = Session("FinYearHis")(i)
                '        End If
                '    Next i
                'End If
            End If
            'jrs 2015/06/05
            If Session("buseAllHis") And UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                For i = 1 To Session("COUNTE")
                    If Session("FinYearHis")(i) > Session("DisplayLastYear") Then
                        Session("DisplayLastYear") = Session("FinYearHis")(i)
                    End If
                Next i
            End If

            'jrs 2014/03/27
            '2015/08/28
            'removed the historic condition, becasue forecast comparison could be comparing vs forecast only
            'If Session("FirstYear") = FirstYearHistory Then
            For i = 1 To Session("COUNTE")
                If Session("StaYearHis")(i) > 0 And Session("StaYearHis")(i) < Session("FirstYear") Then
                    'Check for DATA
                    For j = 1 To Session("FirstYear") - Session("StaYearHis")(i)
                        If IsNumeric(Session("PLOT")(j, i)) Then
                            bLoadLongitudinalBFYH = True
                            iFirstYearBFYH = Session("StaYearHis")(i) + j - 1
                            Session("FirstYear") = Session("StaYearHis")(i) + j - 1
                            Exit For
                        End If
                    Next j
                End If
            Next i
            'End If

            'jrs 2015/08/28
            'removed the historic condition, becasue forecast comparison could be comparing vs forecast only
            'If LastYearTbl = LastYearHistory Then
            For i = 1 To Session("COUNTE")
                If Session("FinYearHis")(i) > 0 And Session("FinYearHis")(i) > Session("LastYear") Then
                    'Check for DATA
                    For j = Session("FinYearHis")(i) - Session("LastYear") To 1 Step -1
                        If Not (IsNumeric(Session("PLOT")(j + Session("LastYear") - Session("FirstYear") + 1, i))) Then
                            Session("LastYear") = Session("LastYear") + j
                            Exit For
                        End If
                    Next j
                End If
            Next i
            'End If

            'JRS 12/26/02
            RefreshTable()
            'JRS 04/14/03
            bUsingComp = False
            If Session("mnuFilter") Is Nothing Then
                Session("mnuFilter") = "mnuFilterNone"
            End If
            Session("TableHideFilter") = False
            'jrs 09/15/04
            Session("RefreshGraph") = "true"
            'jrs 11/09/04
            ErrorTable = False
            Session("ErrorTable") = ErrorTable
            'jrs 2005/05/10
            iFirstYearSave = Session("FirstYear")
            Session("iFirstYearSave") = iFirstYearSave
            'jrs 2006/07/31 when loading it was showing Whole as if Percentages were selected
            'and when switching it would loose the values
            If Session("PercentOn") Is Nothing Then
                Session("PercentOn") = "False"
            End If
            'jrs 2013/06/07
            If Session("Rank_Year") Is Nothing Then
                'jrs 2008/03/21
                Session("Rank_Year") = Session("FirstYear")
            End If
            'jrs 2008/03/25
            ReDim Session("PLOT_Pct_Tot")(UBound(Session("PLOT"), 1), Session("COUNTE"))
            'jrs 2013/06/07
            If Session("RankDescOrd") Is Nothing Then
                'jrs 2008/06/11
                Session("RankDescOrd") = True
            End If
            'jrs 2008/06/26
            If Session("bMovingAvg") Is Nothing Then
                Session("bMovingAvg") = False
            End If
            If Not Session("MovAvgYrs") Is Nothing Then
                txtMovAvgYrs.Text = Session("MovAvgYrs")
            End If
            'jrs 2008/08/29
            If Session("bCumDis") Is Nothing Then
                Session("bCumDis") = False
            End If
            'jrs 2009/01/22
            If Session("bDis") Is Nothing Then
                Session("bDis") = False
            End If
            If Not Session("CumDisRatePct") Is Nothing Then
                txtCumDisRate.Text = Session("CumDisRatePct")
            Else
                Session("CumDisRatePct") = 3
            End If
            'jrs 2009/01/15
            If Session("bPctTot") Is Nothing Then
                Session("bPctTot") = False
            End If
            If Session("bComparisonOn") Is Nothing Then
                Session("bComparisonOn") = False
                'jrs 2009/12/08
                ReDim Session("CompBackup")(UBound(Session("PLOT"), 1), Session("COUNTE"))
                ReDim Session("CompLabelBackup")(11, Session("COUNTE")) 'jrs 2012/07/12
            End If

            'jrs 2012/01/16
            If Session("bFiltDisplayCount") Is Nothing Then
                Session("bFiltDisplayCount") = False
                ReDim Session("FilterBackup")(UBound(Session("PLOT"), 1), Session("COUNTE"))
                ReDim Session("FilterLabelBackup")(10, Session("COUNTE"))
            End If

            'jrs 2009/01/30
            If Session("TblDispOpt") Is Nothing Then
                Session("TblDispOpt") = New Stack
            End If

            'jrs 2018/05/21
            If Session("RestorePos") Is Nothing Then
                'jrs 2010/11/01
                ReDim Session("RestorePos")(Session("COUNTE"))
            Else
                'jrs 2018/05/21
                If UBound(Session("RestorePos")) < Session("COUNTE") Then
                    ReDim Session("RestorePos")(Session("COUNTE"))
                End If
            End If

            'jrs 2013/02/20
            ReDim Preserve Session("oGR4N")(Session("COUNTE"))
            Session("bNotSameUnits") = False

            'jrs 2021/02/08 
            If Session("bAlias") = Nothing Then
                'jrs 2011/07/01
                Session("bAlias") = False
            End If

            'jrs 2021/02/08
            If Session("bAlias") = False Then
                'jrs 2010/02/04
                ReDim Session("oGR1N")(Session("COUNTE"))
            Else 'jrs 2023/09/27
                'jrs 2026/04/03
                If Session("oGR1N") Is Nothing OrElse Session("COUNTE") > UBound(Session("oGR1N")) Then
                    ReDim Preserve Session("oGR1N")(Session("COUNTE"))
                End If
            End If
            For i = 1 To Session("COUNTE")
                'jrs 2021/02/08
                If Session("bAlias") = False Or Session("oGR1N")(i) = "" Then
                    Session("oGR1N")(i) = Session("GR1N")(i)
                End If
                'jrs 2010/11/01
                Session("RestorePos")(i) = i
                'jrs 2013/02/20
                If Session("UnitCount") Is Nothing Then
                    Session("oGR4N")(i) = Session("GR4N")(i)
                    If Not Session("bNotSameUnits") And i > 1 Then
                        If Session("GR4N")(i) <> Session("GR4N")(1) Then
                            Session("bNotSameUnits") = True
                        End If
                    End If
                End If
            Next i



            'jrs 2011/04/01
            Dim pos As Integer
            pos = InStr(Session("VARN")(1, 1), "[")
            If pos > 0 Then
                Dim sVar As String
                sVar = Left(Session("VARN")(1, 1), pos - 1)
                If IfsVars.Item(sVar).Index > 0 Then
                    'jrs 2025/03/10 added 2nd condition to avoid bilaterals in the map
                    'jrs 2023/11/14 added condition
                    If VDim1(IfsVars.Item(sVar).Index) = 1 AndAlso VDim2(IfsVars.Item(sVar).Index) <> 1 Then
                        Session("MMVarName") = sVar
                        Session("MMDim2") = Session("GR2N")(1)
                        Session("MMDim3") = Session("GR3N")(1)
                    End If
                End If
            End If

            'jrs 2013/02/20
            'Setup Conversion menu
            If Session("UnitCount") Is Nothing Then 'This is to avoid resetting when coming back from the Graph
                'Open Database
                Dim dbU As String
                dbU = Session("IFs_MDB2Active")
                Dim rsU As DataTable
                'Load Unit conversions
                SetDataTablefromAccess(dbU, "SELECT * FROM UnitConversions WHERE UnitIFsName = """ & Session("GR4N")(1) & """", "", rsU)
                Session("UnitCount") = 0
                If rsU.Rows.Count > 0 Then
                    Session("UnitCount") = rsU.Rows.Count
                    ReDim Session("sUnitLbl")(Session("UnitCount") - 1), Session("sUnitLblLong")(Session("UnitCount") - 1)
                    ReDim Session("vUnitFactor")(Session("UnitCount") - 1), Session("sUnitMenuLbl")(Session("UnitCount") - 1)
                    i = 0
                    Do While i < rsU.Rows.Count
                        Session("sUnitLbl")(i) = rsU.Rows(i).Item("UnitOtherName")
                        Session("vUnitFactor")(i) = rsU.Rows(i).Item("ConvFactor")
                        Session("sUnitLblLong")(i) = rsU.Rows(i).Item("UnitOtherLongName")
                        Session("sUnitMenuLbl")(i) = "Convert to " & Session("sUnitLbl")(i)
                        i = i + 1
                    Loop
                Else
                    i = 0
                End If
                'Save original Display Units
                Dim idx As Integer
                idx = InStr(Session("VARN")(1, 1), "[")
                If idx > 0 Then
                    idx = IfsVars.Item(Mid(Session("VARN")(1, 1), 1, idx - 1)).Index
                Else
                    idx = IfsVars.Item(Session("VARN")(1, 1)).Index
                End If
                Session("UnitIdx") = 0
                Session("oUnitDisplayName") = ""
                If idx > 0 Then
                    Session("UnitIdx") = idx
                    If Session("VDisplayUnitsName")(idx) > "" Then
                        Session("oUnitDisplayName") = getDisplayUnitsWCurrency(idx, Session("CurrencyName"), Session("VDisplayUnitsName"))
                    End If
                End If
                'Check if it's currency
                If InStr(Session("GR4N")(1), "$") > 0 Then
                    SetDataTablefromAccess(dbU, "SELECT Symbol, [Currency Value], [Currency Name], MIN(Country) as MinCountry " &
                "FROM [Currency] WHERE [Currency Value] > 0 AND [Currency Value] <> 1 AND not (Symbol is NULL) " &
                "GROUP BY Symbol, [Currency Value], [Currency Name] ORDER BY MIN(Country)", "", rsU)
                    Session("UnitCount") = Session("UnitCount") + rsU.Rows.Count
                    ReDim Preserve Session("sUnitLbl")(Session("UnitCount") - 1)
                    ReDim Preserve Session("sUnitLblLong")(Session("UnitCount") - 1)
                    ReDim Preserve Session("vUnitFactor")(Session("UnitCount") - 1)
                    ReDim Preserve Session("sUnitMenuLbl")(Session("UnitCount") - 1)

                    j = 0
                    Do While j < rsU.Rows.Count
                        Session("sUnitLbl")(i) = Replace(Session("GR4N")(1), "$", rsU.Rows(j).Item("Symbol"))
                        Session("vUnitFactor")(i) = 1 / rsU.Rows(j).Item("Currency Value") 'jrs 2013/03/22 removed [] from field name, it was giving an error
                        Session("sUnitLblLong")(i) = Replace(Session("oUnitDisplayName"), "Dollar", rsU.Rows(j).Item("Currency Name")) 'jrs 2013/03/22 removed [] from field name
                        If rsU.Rows(j).Item("Currency Name") = "Euro" Then 'jrs 2013/03/22 removed [] from field name
                            Session("sUnitMenuLbl")(i) = "Convert to Euros"
                        Else
                            Session("sUnitMenuLbl")(i) = "Convert to " & rsU.Rows(j).Item("MinCountry") & " " & Session("sUnitLbl")(i)
                        End If
                        i = i + 1
                        j = j + 1
                    Loop
                End If
            End If

            'jrs 2016/03/30
            If Not (Session("bUseStartYrOrg") = Nothing) Then
                If Session("bUseStartYrOrg") Then
                    mnuUseStartYrDivby5_Click()
                    Session("bUseStartYrOrg") = Nothing
                End If
            End If

            'jrs 2019/07/29
            If Session("YAxisLabel") Is Nothing Then
                Session("YAxisLabel") = ""
            End If
            If Session("YAxisLabelOrg") Is Nothing Then
                Session("YAxisLabelOrg") = Session("YAxisLabel")
            End If

        Else
            'JRS 03/22/04
            Timepoints = Session("Timepoints")
            DisYear = Session("TableDisYear")
            'jrs 2009/01/30 SPLOT = Session("SPLOT")
            'jrs 11/09/04
            ErrorTable = Session("ErrorTable")
            'jrs 2005/05/11
            iFirstYearSave = Session("iFirstYearSave")
        End If
        'jrs 2011/05/03 WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
        'jrs 2012/09/26
        If Session("TableTitle") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("TableTitle")
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
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents cmd_ScriptFile As AxMSComDlg.AxCommonDialog 
    'CONVERSION_NOTE: Class 'AxPEGOALib.AxPegoa' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents Pego1 As AxPEGOALib.AxPegoa 
    'CONVERSION_NOTE: Class 'AxPEGOALib.AxPegoa' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents Pego2 As AxPEGOALib.AxPegoa 
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    Protected WithEvents ToolTip1 As System.Web.UI.WebControls.Label
    Protected WithEvents MainMenu1 As ArtinSoft.WinFormsToWeb.WebControls.MainMenu
    Protected WithEvents WinFormsToWeb_MenuManager As ArtinSoft.WinFormsToWeb.WebControls.MenuManager
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_TableDisplayPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_TableDisplay
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_TableDisplay()
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
    '------------------------------------------TablDisp.Frm-------------------------------------
    '------------------------------------------------------------------------------------------

    '
    '   Copyright by Barry B. Hughes 1988-2001
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last revision by BBH on 12/4/99


    Dim Timepoints, DisYear As Short
    'JRS 01/28/04
    'jrs 2009/01/30 Dim SPLOT(200, 200) As Object '100 goes out of range, so 200

    Dim VH(100) As Object
    Dim VF(100) As Object

    'mti Validation changes August, 2002
    Dim ValCol As Object

    'mti Validation changes 13 August, 2002
    Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal HWND As Integer, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As Integer
    'JRS 12/11/02 FILTER
    Public bFilter_Group As Boolean
    'JRS 04/14/03 Computations
    Dim bUsingComp As Boolean
    'JRS 10/02/03
    Dim CurrencyFct As Single 'Stores the Currency Factor for Conversion
    'Declares and opens a RecordSet with configuration values
    'jrs 2005/12/09 Dim rsCurr As DataTable
    'JRS 12/08/03
    Public bLoadLongitudinalBFYH As Boolean 'flag to load values before first year history ex. PolityDemoc
    Public iFirstYearBFYH As Short 'first year before first year history
    Public ValtoShow As New VB6.FixedLengthString(15)
    'JRS 11/09/04
    Dim ErrorTable As Boolean
    'jrs 2005/05/10
    Dim iFirstYearSave As Integer

    'jrs 2021/05/05
    Dim FilesCol As New Collection
    Dim dfCol As New Collection

    Dim ScenarioName As String

    '=============================TablDisp.Frm=======================
    'jrs 2024/11/25 Private Sub FillData(ByRef COUNTE As Short, ByRef ry() As TableRow)
    Private Sub FillData(ByRef COUNTE As Integer, ByRef ry() As TableRow)
        'jrs 2006/09/25
        Dim bFlagUseallHis As Boolean
        bFlagUseallHis = (UCase(Session("ActiveBase")) = "IFSBASE.RUN" And Session("FirstYear") <= FirstYearHistory)
        'jrs 2025/09/22
        If Not bFlagUseallHis AndAlso Not Session("buseAllHis") Is Nothing Then
            bFlagUseallHis = Session("buseAllHis")
        End If

        'jrs 2009/03/04
        ReDim Session("FourDecs")(COUNTE)

        Dim Value As Object
        Dim rr As Short
        Dim Y As Short
        Dim RFSN As Short
        'jrs 2024/11/25 Dim cc As Short ''mti Validation changes
        Dim cc As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim min As Single
        Dim max As Single
        For cc = 1 To Session("COUNTE")
            'jrs 2009/03/04
            Session("FourDecs")(cc) = False

            RFSN = Val(Session("FileNumbr")(cc))
            Y = Session("StartYrIdx") 'jrs 2016/03/30 1
            'JRS 08/08/03
            min = 9 * 10 ^ 32
            max = -9 * 10 ^ 32
            For rr = 1 To Timepoints
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(Y, cc). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Value = Session("PLOT")(Y, cc)
                Y = Y + Session("Interval")
                'JRS 08/08/03
                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'JRS 03/21/04
                If Not IsDBNull(Value) Then
                    If CStr(Value) <> "" Then
                        If Val(Value) < min Then
                            min = Val(Value)
                        End If
                        'jrs 2005/10/31
                        If Val(Value) > max Then
                            max = Val(Value)
                        End If
                    End If
                End If

                '            If Y% > DisYear% + 1 Then Y% = DisYear% + 1
                'jrs 2014/05/01 If y% > DisYear% Then y% = DisYear%
                'jrs 2016/04/11 If Y > DisYear And Y - Session("Interval") < DisYear Then Y = DisYear
                If Y > DisYear + Session("StartYrIdx") - 1 And Y - Session("Interval") < DisYear + Session("StartYrIdx") - 1 Then Y = DisYear + Session("StartYrIdx") - 1


                'JRS 03/21/04
                If Not IsDBNull(Value) Then
                    'jrs 2022/02/24 added Show Extra Decimals
                    If Session("ShowExtraDecimals") = "YES" Then
                        Select Case Val(Value)
                            Case -1000 To 999999
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00000000"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.00000000}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00000000"), Len(ValtoShow.Value))
                                End If
                            Case Else
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.0}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0"), Len(ValtoShow.Value))
                                End If

                        End Select
                    Else
                        Select Case Val(Value)
                            Case 100 To 1000
                                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.0}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0"), Len(ValtoShow.Value))
                                End If

                            Case 10 To 100
                                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.00}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00"), Len(ValtoShow.Value))
                                End If

                            Case -10 To 10
                                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.000"), Len(ValtoShow.Value))
                                'jrs 2024/07/11 added 2 more decimals, from 3 to 5, CARANN showed only 3
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.00000}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00000"), Len(ValtoShow.Value))
                                End If

                                'mti Validation changes  'for % value below 10
                                If Asc(VB.Right(ValtoShow.Value, 1)) = 48 Then '48 = "0"
                                    'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                    'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00"), Len(ValtoShow.Value))
                                    If StandAloneMode Then
                                        ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.00}", Value)
                                    Else
                                        ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00"), Len(ValtoShow.Value))
                                    End If

                                End If
                                'mti Validation changes
                            Case -100 To -10
                                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.00}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00"), Len(ValtoShow.Value))
                                End If

                            Case -1000 To -100
                                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###.0}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0"), Len(ValtoShow.Value))
                                End If

                            Case Else
                                'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###,###,###"), Len(ValtoShow.Value))
                                If StandAloneMode Then
                                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,###,###,###,###}", Value)
                                Else
                                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###,###,###"), Len(ValtoShow.Value))
                                End If

                        End Select
                    End If
                Else
                    ValtoShow.Value = ""
                End If
                Dim ce1 As New TableCell
                ce1.Text = ValtoShow.Value
                'jrs 2006/09/25
                If bFlagUseallHis Then
                    'jrs 2014/05/01 If Y - 2 + Session("FirstYear") <= Session("FinYearHis")(cc) Then
                    If Y - 1 - Session("Interval") + Session("FirstYear") <= Session("FinYearHis")(cc) Then
                        ce1.ForeColor = Color.Blue
                    End If
                End If
                ry(rr).Cells.Add(ce1)

            Next rr
            'JRS 08/08/03
            If max - min < 0.003 Then
                'jrs 2009/03/04 added parameter ry to reproduce what we do in vb6
                ReFillData(cc, ry)
            End If
        Next cc
        'JRS 02/25/04
        For rr = 1 To Timepoints
            grd_TableDisplay.Rows.Add(ry(rr))
        Next rr

    End Sub

    '==========================frm_TableDisplay==================
    Private Sub SaveValidation(ByRef ValPairs As Short)
        Dim pos As Object
        Dim Msg As Object
        Dim i2 As Object
        Dim FileSaved As Object
        Dim CDERR_CANCEL As Object
        Dim GraphOutputFileName As Object
        '------------------------------------------------------------

        'mti Validation changes August, 2002 - Take Whole Procedure

        On Error GoTo SelGraphFileError_Line

        Dim PEGraphFile, PETitle As String
        Dim Cnt, LenStr As Short

        'JRSVBNET cmd_ScriptFile.DefaultExt = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        'JRSVBNET cmd_ScriptFile.DialogTitle = "Export Validation Pair" ''mti Validation changes 19 August, 2002
        'JRSVBNET cmd_ScriptFile.InitDir = PutFdrv & "Validation"

        'mti Validation changes 19 August, 2002
        'JRSVBNET cmd_ScriptFile.FLAGS = MSComDlg.FileOpenConstants.cdlOFNNoChangeDir
        'JRSVBNET cmd_ScriptFile.Action = 1 'was 1
        'mti Validation changes 19 August, 2002

        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '        On Error GoTo 0
        'If left$(GraphOutputFileName, 1) = "*" Then Exit Sub

        'mti Validation changes 10 august 2002
        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If GraphOutputFileName = "" Then Exit Sub
        'mti Validation changes 10 august 2002

        GoTo WriteGraphFile_Line

SelGraphFileError_Line:
        'UPGRADE_WARNING: Couldn't resolve default property of object CDERR_CANCEL. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Err.Number = CDERR_CANCEL Then
            LoadMessage("Cancel", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        Else
            LoadMessage(ErrorToString(Err.Number), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        FileClose()

WriteGraphFile_Line:

        'mti Validation changes 19 August, 2002
        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(GraphOutputFileName + ".val") <> "" Then
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If False Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("A validation file with the same name already exists." & "<br>" & "Would you like to overwrite?.", MsgBoxStyle.YesNo, "IFs Validation") = MsgBoxResult.Yes Then

                'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Kill(GraphOutputFileName + ".val")
                'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                If Dir(GraphOutputFileName, FileAttribute.Normal) <> "" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Kill(GraphOutputFileName)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                If Dir(GraphOutputFileName, FileAttribute.Directory) <> "" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    RmDir(GraphOutputFileName)
                End If
            Else
                Exit Sub
            End If
        End If


        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(GraphOutputFileName, FileAttribute.Normal) <> "" Then
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If False Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Unfortunately a file with the same name already exists." & "<br>" & "Would you like to overwrite?.", MsgBoxStyle.YesNo, "IFs Validation") = MsgBoxResult.Yes Then
                'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Kill(GraphOutputFileName)
                'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                If Dir(GraphOutputFileName, FileAttribute.Directory) <> "" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    RmDir(GraphOutputFileName)
                End If
            Else
                Exit Sub
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        If Dir(GraphOutputFileName, FileAttribute.Directory) <> "" Then
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If False Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("A Validation Folder with the same name already exists." & "<br>" & "Would you like to overwrite?.", MsgBoxStyle.YesNo, "IFs Validation") = MsgBoxResult.Yes Then
                'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                RmDir(GraphOutputFileName)
            Else
                Exit Sub
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object FileSaved. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        FileSaved = False
        'If exporting all validation pairs and there are more than 1 pair
        'Save Validation Definition in IFS\Validation\Namechosen.Val file
        If False Then 'JRSVBNET Pego1.Visible = False And ValPairs > 1 Then
            'jrs 2005/02/16
            Dim fNum As Integer
            fNum = FreeFile()
            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            FileOpen(fNum, GraphOutputFileName + ".val", OpenMode.Output)

            For i2 = 1 To ValPairs

                If ValPairs > 1 Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ValCol = i2
                Else
                    'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                    If IsNothing(ValCol) Then '1 pair of validation only and save all
                        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        ValCol = 1
                    Else
                        'Valcol loaded in mnuViewPair
                    End If
                End If

                'mti Validation changes 19 August, 2002
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = ""
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + Trim(Session("VARN")(5, VH(ValCol))) + ","
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + shedbracket(Session("VARN")(1, VH(ValCol)), "[") + ","
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                pos = InStr(Session("GR1N")(VH(ValCol)), "/")
                'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If pos > 0 Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Msg = Msg + Mid(Session("GR1N")(VH(ValCol)), 1, pos - 1) + ","
                Else
                    'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Msg = Msg + Session("GR1N")(VH(ValCol)) + ","
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + shedbracket(Session("VARN")(1, VF(ValCol)), "[") + ","
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + Session("GR2N")(VF(ValCol))

                PrintLine(fNum, Msg)
                'mti Validation changes 19 August, 2002
            Next i2
            'UPGRADE_WARNING: Couldn't resolve default property of object FileSaved. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            FileSaved = True
            FileClose(fNum)
        End If
        'mti Validation changes 19 August, 2002



        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MkDir(GraphOutputFileName) 'PutFdrv$ + "Validation\" +
        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET FileOpen(2, GraphOutputFileName + "\" + cmd_ScriptFile.FileTitle + ".htm", OpenMode.Output)

        'Write HTML Header
        PrintLine(2, "<html>")
        PrintLine(2, "<head>")
        'JRSVBNET PrintLine(2, "<title>IFs Validation Report" & 'JRSVBNET cmd_ScriptFile.FileTitle & "</title>")
        PrintLine(2, "<meta http-equiv=" & Quote & "Content-Type" & Quote & "; content = " & Quote & "text/html; charset=iso-8859-1" & Quote & " > ")
        PrintLine(2, "</head>")
        PrintLine(2, "<body bgcolor=" & Quote & "#FFFFFF" & Quote & " text=" & Quote & "#000000" & Quote & ">")
        PrintLine(2, "<p>&nbsp;</p>")

        'mti Validation changes 19 August, 2002
        PrintLine(2, "<p align=" & Quote & "center" & Quote & "><font size=5>" & "IFs Validation Result" & "</font><a name=" & Quote & "Top" & Quote & "></a></p>")

        'UPGRADE_WARNING: Couldn't resolve default property of object FileSaved. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If FileSaved Then
            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            PrintLine(2, "<p align=" & Quote & "center" & Quote & ">" & "(Validation list saved in " & "<a href=" & Quote + GraphOutputFileName + ".val" + Quote + ">" + GraphOutputFileName + ".val" + "</a>" + ")</p>")
        End If
        'Print #2, "<p><a href=" + Quote + "#Top" + Quote + ">" + " " + "</a></p>"

        For i2 = 1 To ValPairs

            If ValPairs > 1 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                ValCol = i2
            Else
                'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                If IsNothing(ValCol) Then '1 pair of validation only and save all
                    'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ValCol = 1
                Else
                    'Valcol loaded in mnuViewPair
                End If
            End If

            'mti Validation changes 19 August, 2002
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = shedbracket(Session("VARN")(1, VH(ValCol)), "[")

            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + " vs "

            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + shedbracket(Session("VARN")(1, VF(ValCol)), "[")

            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + IIf(Len(Trim(Session("GR2N")(VF(ValCol)))) = 0, "", " - " & Session("GR2N")(VF(ValCol)))
            'mti Validation changes 19 August, 2002

            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            pos = InStr(Session("GR1N")(VH(ValCol)), "/")
            'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If pos > 0 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + " for " + Mid(Session("GR1N")(VH(ValCol)), 1, pos - 1)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + " for " + Session("GR1N")(VH(ValCol))
            End If



            'Write Html TOC or Links to Subsequent Tables
            'mti Validation changes 19 August, 2002
            If ValPairs > 1 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                PrintLine(2, "<p><a href=" & Quote & "#" + Msg + Quote + ">" + Msg + "</a></p>")
            End If
            'mti Validation changes 19 August, 2002

            'Draw Proessential graphs (invisible?)
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Call DrawValidationGraphs(i2 * 2 - 1, i2 * 2)

            'Save Proessential graphs to jpeg

            'mti Validation changes 19 August, 2002
            'JRSVBNET Pego1.PEnarg1 = 400 'width in pixels
            'JRSVBNET Pego1.PEnarg2 = 300 'height in pixels
            ''JRSVBNET Pego1.PEstrarg1 = GraphOutputFileName + "\" + Msg + "_Distance.jpg"
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Pego1.PEstrarg1 = GraphOutputFileName + "\" + Trim(Str(i2)) + "_Distance.jpg"
            'JRSVBNET Pego1.PEactions = 22 ' Calls PEcopyjpegtofile

            'JRSVBNET Pego2.PEnarg1 = 400 'width in pixels
            'JRSVBNET Pego2.PEnarg2 = 300 'height in pixels
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Pego2.PEstrarg1 = GraphOutputFileName + "\" + Trim(Str(i2)) + "_PcntErr.jpg"
            'JRSVBNET Pego2.PEactions = 22 ' Calls PEcopyjpegtofile

            'mti Validation changes 09 August, 2002
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Call DrawDataGraph(VH(ValCol), VF(ValCol))

            'JRSVBNET Pego1.PEnarg1 = 400 'width in pixels
            'JRSVBNET Pego1.PEnarg2 = 300 'height in pixels
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Pego1.PEstrarg1 = GraphOutputFileName + "\" + Trim(Str(i2)) + "_Data.jpg"
            'JRSVBNET Pego1.PEactions = 22 ' Calls PEcopyjpegtofile
            ''mti Validation changes 19 August, 2002

        Next i2

        'again
        For i2 = 1 To ValPairs
            If ValPairs > 1 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                ValCol = i2
            Else
                'UPGRADE_WARNING: IsEmpty was upgraded to IsNothing and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
                If IsNothing(ValCol) Then '1 pair of validation only and save all
                    'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    ValCol = 1
                Else
                    'Valcol loaded in mnuViewPair
                End If
            End If

            'mti Validation changes 19 August, 2002
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = shedbracket(Session("VARN")(1, VH(ValCol)), "[")

            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + " vs "

            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + shedbracket(Session("VARN")(1, VF(ValCol)), "[")

            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + IIf(Len(Trim(Session("GR2N")(VF(ValCol)))) = 0, "", " - " & Session("GR2N")(VF(ValCol)))

            'mti Validation changes 19 August, 2002


            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            pos = InStr(Session("GR1N")(VH(ValCol)), "/")
            'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If pos > 0 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object pos. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + " for " + Mid(Session("GR1N")(VH(ValCol)), 1, pos - 1)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Msg = Msg + " for " + Session("GR1N")(VH(ValCol))
            End If

            'mti Validation changes 19 August, 2002
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Call SavePair(2, Msg, i2 * 2 - 1, i2 * 2, Val(i2), ValPairs)
            'SavePair(FileNo As Integer, TblName, DistanceCol%, PcntErrorCol%,pairno%)
            'mti Validation changes 19 August, 2002

        Next i2

        'mti Validation changes 13 August, 2002
        FileClose(2)

        'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = "Validation exported to the folder " + GraphOutputFileName + ", which you could browse later." + "<br>" + "Would you prefer to browse now instead?"
        '+ "Please click on the file " + 'JRSVBNET cmd_ScriptFile.FileTitle + ".htm in the mentioned " _
        '+ "folder to browse over validation."
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        If False Then 'JRSVBNET WinFormsToWeb_MessageBox.Show(Msg, MsgBoxStyle.YesNo, "IFs Validation") = MsgBoxResult.Yes Then
            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Call OpenURL(GraphOutputFileName + "\" + 'JRSVBNET cmd_ScriptFile.FileTitle + ".htm")
        End If
        'mti Validation changes 13 August, 2002



    End Sub
    '==========================frm_TableDisplay==================
    Public Function OpenURL(ByVal URL As String) As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        OpenURL = ShellExecute(0, vbNullString, URL, vbNullString, vbNullString, AppWinStyle.NormalFocus)
    End Function
    '==========================frm_TableDisplay==================
    Private Sub cmdContinue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdContinue.Click
        Dim rr As Short
        Dim cc As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes July 28, 2002 - Take Whole Procedure
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor

        For cc = 1 To Session("COUNTE")
            For rr = 1 To DisYear + 1
                'jrs 2009/01/30 Session("PLOT")(rr, cc) = SPLOT(rr, cc)
                Session("PLOT")(rr, cc) = Session("Validation_PLOT")(rr, cc)
            Next rr
        Next cc


        'Me.Visible = False
        'JRSVBNET Pego1.Visible = False
        'JRSVBNET Pego2.Visible = False
        cmdContinue.Visible = False

        'mti Validation changes 09 August, 2002
        cmdNext.Visible = False
        cmdPrevious.Visible = False
        'mti Validation changes 09 August, 2002

        'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
        'JRSVBNET grd_TableDisplay.Rows = 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) - 1
        'JRSVBNET mnuValidation.Text = "&Validation"
        'JRSVBNET mnuValidation_Click('JRSVBNET mnuValidation, New System.EventArgs)

        'mti Validation changes 19 August, 2002
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Historical Validation" & VInfo()

        'Me.Visible = True
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
    End Sub
    '==========================frm_TableDisplay==================
    Private Sub cmdNext_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdNext.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes 09 August, 2002 - Take Whole Procedure
        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If ValCol < Session("VCOUNTE") Then

            cmdContinue_Click(cmdContinue, New System.EventArgs)
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, (ValCol + 1) * 2 - 1)
            mnuViewPair_Click(Me, New System.EventArgs)



            'Else
        End If

    End Sub

    '==========================frm_TableDisplay==================
    Private Sub cmdPrevious_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdPrevious.Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes 09 August, 2002 - Take Whole Procedure
        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If ValCol > 1 Then

            cmdContinue_Click(cmdContinue, New System.EventArgs)
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, (ValCol - 1) * 2 - 1)
            mnuViewPair_Click(Me, New System.EventArgs)
            'Else
        End If

    End Sub

    Private Sub frm_TableDisplay_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
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
        cswmGroup1.ID = "Group_bc9b"
        'jrs 09/22/04 cswmGroup1.DownLevelURL = Session("TableDisplayParent")

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"
        'JAM 2013-04-09 adding hourglass cursor
        cswmItem.OnClick = "showhg();"

        'jrs 2026/08/07
        If StandAloneMode Then
            'jrs 2011/04/05
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Main Menu"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Refresh"
        cswmGroup1.ID = "Group_a624"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Refresh"

        'jrs 09/07/04
        'cdv 2022/06/30 ****START****
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Graph"
        cswmGroup1.ID = "mnuGraph"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Line"
        cswmItem.ID = "mnuPELine"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Bar"
        cswmItem.ID = "mnuPEBar"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Pie"
        cswmItem.ID = "mnuPEPie"
        'cdv 2022/06/30 ****END****

        'jrs 2006/08/24
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Map"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display Map"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Save"
        cswmGroup1.ID = "mnuSave"

        'jrs 2005/05/11
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "First Year to Save"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save Normal View"

        'jrs 2020/08/20 
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save to Clipboard"
        cswmItem.OnClick = "tablecopy(); return false;"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save Transpose"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save All Years."

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save every 5 Years."

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save every 10 Years."

        'jrs 2023/05/15
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save All Years to Clipboard."
        cswmItem.OnClick = "transtablecopy(); return false;"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save Transpose and Decompose"
        'jrs 2009/01/16
        cswmItem.ID = "mnuSaveDec"
        If Session("bComparisonOn") Then
            cswmItem.Enabled = False
        Else
            cswmItem.Enabled = True
        End If

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save All Years"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save every 5 Years"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Save every 10 Years"

        'jrs 2014/04/25
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save in Panel format"

        'jrs 2025/09/04 this option should only be available in Stand alone mode
        If StandAloneMode Then
            'jrs 2023/05/26
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Save in Google format (PDE)"
            cswmItem.OnClick = "showhg();"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        'jrs 2006/08/24
        cswmGroup1.Caption = "Display Options"
        cswmItem = cswmGroup1.Items.Add()
        'cswmGroup1.ID = "mnuPercent"
        cswmGroup1.ID = "mnuDispOpt"
        'jrs 2009/01/30
        ''jrs 2009/01/15
        'If Session("bPctTot") Then
        '    cswmGroup1.ShowMenuBarItem = False
        'Else
        '    cswmGroup1.ShowMenuBarItem = True
        'End If

        cswmItem.ID = "mnuPercent"
        If Session("PercentOn") = "False" Then
            cswmItem.Caption = "Percent"
        Else
            cswmItem.Caption = "Whole"
        End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Switch"

        ''jrs 2005/05/11
        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Cumulative"
        'cswmGroup1.ID = "mnuCum"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Cumulative"
        cswmItem.ID = "mnuCum"

        'jrs 2009/01/30
        ''jrs 2008/08/29 added or bCumDis
        ''jrs 2005/10/14
        'If Session("bIntAvg") = True Or Session("bCumDis") = True Or Session("bDis") Or Session("bMovingAvg") Then
        '    'cswmGroup1.ShowMenuBarItem = False
        '    cswmItem.Enabled = False
        'End If

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuCumOn"
        If Session("bCum") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2016/04/05
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On with Discount"
        cswmItem.ID = "mnuCumDisOn"
        If Session("bCumDis") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Discount Rate (" & txtCumDisRate.Text & "%)"
        cswmItem.ID = "mnuCumDisRate"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuCumOff"
        If Session("bCum") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If

        'jrs 2009/01/22
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Discount"
        cswmItem.ID = "mnuDis"

        'jrs 2009/01/30
        'If Session("bIntAvg") = True Or Session("bCum") = True Or Session("bCumDis") Or Session("bMovingAvg") Then
        '    cswmItem.Enabled = False
        'End If

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuDisOn"
        If Session("bDis") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuDisOff"
        If Session("bDis") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Discount Rate (" & txtCumDisRate.Text & "%)"
        cswmItem.ID = "mnuDisRate"

        'jrs 2016/04/05 *** begin ***
        ''jrs 2008/08/29
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Cumulative with Discount"
        'cswmItem.ID = "mnuCumDis"

        ''jrs 2009/01/30
        ''If Session("bIntAvg") = True Or Session("bCum") = True Or Session("bDis") Or Session("bMovingAvg") Then 'jrs 2009/01/22 added bDis
        ''    cswmItem.Enabled = False
        ''End If

        'cswmGroup2 = cswmItem.AddGroup()
        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "On"
        'cswmItem.ID = "mnuCumDisOn"
        'If Session("bCumDis") = True Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Off"
        'cswmItem.ID = "mnuCumDisOff"
        'If Session("bCumDis") = True Then
        '    cswmItem.IconImage = "blank.gif"
        'Else
        '    cswmItem.IconImage = "checkbox.gif"
        'End If

        'cswmItem = cswmGroup2.Items.Add()
        'cswmItem.Caption = "Discount Rate (" & txtCumDisRate.Text & "%)"
        'cswmItem.ID = "mnuCumDisRate"
        'jrs 2016/04/05 *** end ***

        ''jrs 2005/10/14
        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Interval Average"
        'cswmGroup1.ID = "mnuIntAvg"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Interval Average"
        cswmItem.ID = "mnuIntAvg"
        'jrs 2009/01/30
        'If Session("bCum") = True Or Session("bCumDis") Or Session("bDis") Or Session("bMovingAvg") Then 'jrs 2009/01/22 added bCumDis or bDis
        '    'cswmGroup1.ShowMenuBarItem = False
        '    cswmItem.Enabled = False
        'End If

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuIntAvgOn"
        If Session("bIntAvg") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuIntAvgOff"
        If Session("bIntAvg") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If

        'jrs 2011/07/01 *** begin ***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Interval Total Change"
        cswmItem.ID = "mnuIntTot"

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuIntTotOn"
        If Session("bIntTot") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuIntTotOff"
        If Session("bIntTot") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Interval Total Absolute Change"
        cswmItem.ID = "mnuIntAbs"

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuIntAbsOn"
        If Session("bIntAbs") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuIntAbsOff"
        If Session("bIntAbs") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If
        'jrs 2011/07/01 *** end ***

        'jrs 2012/01/02 *** begin ***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Index Base One"
        cswmItem.ID = "mnuIdxBase1"

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuIdxBase1On"
        If Session("bIdxBase1") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuIdxBase1Off"
        If Session("bIdxBase1") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If
        'jrs 2012/01/02 *** end ***

        'jrs 2008/06/26
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Moving Average"
        cswmItem.ID = "mnuMovingAvg"
        'jrs 2009/01/30
        'If Session("bCum") = True Or Session("bIntAvg") = True Or Session("bCumDis") Or Session("bDis") Then 'jrs 2009/01/22 added or bCumDis or bDis
        '    cswmItem.Enabled = False
        'End If

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Years in Avg(" & txtMovAvgYrs.Text & ")"
        cswmItem.ID = "mnuMovingAvgYrs"

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuMovingAvgOn"
        If Session("bMovingAvg") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuMovingAvgOff"
        If Session("bMovingAvg") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If

        'jrs 2013/02/20 *** begin ***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Unit Conversion"
        cswmItem.ID = "mnuUnit"

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Define Conversion Factor"
        cswmItem.IconImage = "blank.gif"

        Dim i As Integer
        For i = 0 To Session("UnitCount") - 1
            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = Session("sUnitMenuLbl")(i)
            cswmItem.ID = "mnuUnitOn" & CStr(i)
            If Session("UnitConvSel") = i And Not (Session("UnitConvSel") Is Nothing) Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        Next

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuUnitOff"
        If Session("UnitConvSel") Is Nothing Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        'jrs 2013/02/20 *** end ***

        'jrs 2011/07/01 *** begin ***
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Alias for Group/Country Names"
        cswmItem.ID = "mnuAlias"
        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuAliasOn"
        If Session("bAlias") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuAliasOff"
        If Session("bAlias") = True Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Display Interval"
        cswmItem.ID = "mnChgYearInt"
        'jrs 2011/07/01 *** end ***

        'jrs 2024/09/04 *** start ***
        If Session("Interval") = Nothing Then
            Session("Interval") = 1
        End If

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "1"
        cswmItem.ID = "mnChgYearInt1"
        If Session("Interval") = 1 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "5"
        cswmItem.ID = "mnChgYearInt5"
        If Session("Interval") = 5 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "10"
        cswmItem.ID = "mnChgYearInt10"
        If Session("Interval") = 10 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Custom Interval"
        cswmItem.ID = "mnChgYearIntCus"
        If Session("Interval") <> 1 And Session("Interval") <> 5 And Session("Interval") <> 10 Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        'jrs 2024/09/04 *** end ***

        'jrs 2016/03/30
        If FirstYearForecast Mod 5 <> 0 Then
            If Session("mnuUseStartYrDivby5") Is Nothing Then
                Session("mnuUseStartYrDivby5") = False
            End If
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Use Start Year Divisible by 5"
            cswmItem.ID = "mnuUseStartYrDivby5"
            'jrs 2023/12/08
            'If Session("mnuUseStartYrDivby5") Then
            '    cswmItem.IconImage = "checkbox.gif"
            'Else
            '    cswmItem.IconImage = "blank.gif"
            'End If

            cswmGroup2 = cswmItem.AddGroup()

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "On"
            cswmItem.ID = "mnuUseStartYrDivby5On"
            If Session("mnuUseStartYrDivby5") = True Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If

            cswmItem = cswmGroup2.Items.Add()
            cswmItem.Caption = "Off"
            cswmItem.ID = "mnuUseStartYrDivby5Off"
            If Session("mnuUseStartYrDivby5") = True Then
                cswmItem.IconImage = "blank.gif"
            Else
                cswmItem.IconImage = "checkbox.gif"
            End If

        End If

        'jrs 2022/02/24
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Extra Decimals"
        cswmItem.ID = "mnuShowExtraDecimals"
        'jrs 2023/12/08
        'If Session("ShowExtraDecimals") = "YES" Then
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.IconImage = "blank.gif"
        'End If

        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "On"
        cswmItem.ID = "mnuShowExtraDecimalsOn"
        If Session("ShowExtraDecimals") = "YES" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Off"
        cswmItem.ID = "mnuShowExtraDecimalsOff"
        If Session("ShowExtraDecimals") = "YES" Then
            cswmItem.IconImage = "blank.gif"
        Else
            cswmItem.IconImage = "checkbox.gif"
        End If


        'jrs 09/07/04
        If Session("TableDisplayParent") <> "frmLongPlotproessential.aspx" And Session("TableDisplayParent") <> "LongVarS.aspx" Then
            'jrs 2005/09/26 DISABLED disabling filter in 2 cases
            '1) when using empirical history
            If Not (UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And InStr(Session("VARN")(1, 1), "-1") > 0) Then
                '2) when using history + forecast and 2 dimensions.
                If Not (UCase(Session("ActiveBase")) = "IFSBASE.RUN" And Session("FirstYear") = FirstYearHistory And Session("GR2N")(1) > "") Then
                    'jrs 2005/12/07
                    Dim p As Integer, h As Integer, s As String, bFlag As Boolean
                    p = InStr(Session("VARN")(1, 1), "[")
                    h = InStr(Session("VARN")(1, 1), "[-1]")
                    If p > 0 And h = 0 Then
                        'jrs 2007/02/19
                        Dim iPos As Integer, iPos2 As Integer
                        iPos = InStr(Session("VARN")(1, 1), "INCOMELT")
                        iPos2 = InStr(Session("VARN")(1, 1), "LN")
                        If iPos = 0 And iPos2 = 0 Then
                            s = Mid(Session("VARN")(1, 1), 1, p - 1)
                            If IfsVars.Item(s).Index > 0 Then 'jrs 2012/07/05
                                bFlag = (VDim1(IfsVars.Item(s).Index) = 1) 'validates that dim1 = 1
                            Else
                                bFlag = False
                            End If
                        ElseIf iPos > 0 And iPos2 > 0 Then 'jrs 2007/03/16
                            bFlag = True
                        End If

                    Else
                        bFlag = True 'could be formula
                    End If
                    'jrs 2012/02/06
                    Session("bFilterVisible") = bFlag
                    'jrs 2005/12/07 
                    If bFlag Then
                        cswmGroup1 = cswmWebMenu.Groups.Add()
                        cswmGroup1.Caption = "Filter"
                        cswmGroup1.ID = "mnuFilter"
                        If Session("TableHideFilter") Then
                            cswmGroup1.ShowMenuBarItem = False
                        Else
                            cswmGroup1.ShowMenuBarItem = True
                        End If

                        cswmItem = cswmGroup1.Items.Add()
                        cswmItem.Caption = "None"
                        cswmItem.ID = "mnuFilterNone"
                        If Session("mnuFilter") = "mnuFilterNone" Then
                            cswmItem.IconImage = "checkbox.gif"
                        Else
                            cswmItem.IconImage = "blank.gif"
                        End If

                        cswmItem = cswmGroup1.Items.Add()
                        cswmItem.Caption = "Set filter parameters"

                        cswmItem = cswmGroup1.Items.Add()
                        cswmItem.Caption = "Apply Min Level Filter"
                        cswmItem.ID = "mnuFilterMin"
                        If Session("mnuFilter") = "mnuFilterMin" Then
                            cswmItem.IconImage = "checkbox.gif"
                        Else
                            cswmItem.IconImage = "blank.gif"
                        End If

                        cswmItem = cswmGroup1.Items.Add()
                        cswmItem.Caption = "Apply Max Level Filter"
                        cswmItem.ID = "mnuFilterMax"
                        If Session("mnuFilter") = "mnuFilterMax" Then
                            cswmItem.IconImage = "checkbox.gif"
                        Else
                            cswmItem.IconImage = "blank.gif"
                        End If

                        cswmItem = cswmGroup1.Items.Add()
                        cswmItem.Caption = "Apply Rate Level Filter"
                        cswmItem.ID = "mnuFilterRate"
                        If Session("mnuFilter") = "mnuFilterRate" Then
                            cswmItem.IconImage = "checkbox.gif"
                        Else
                            cswmItem.IconImage = "blank.gif"
                        End If

                        cswmItem = cswmGroup1.Items.Add()
                        cswmItem.Caption = "Apply Range Level Filter"
                        cswmItem.ID = "mnuFilterRange"
                        If Session("mnuFilter") = "mnuFilterRange" Then
                            cswmItem.IconImage = "checkbox.gif"
                        Else
                            cswmItem.IconImage = "blank.gif"
                        End If
                    End If
                End If
            End If
        End If

        'jrs 2008/06/09
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Rank Options"
        cswmGroup1.ID = "RankOpt"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Rank"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select Rank Year"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Rank Order"
        cswmItem.ID = "Order"

        cswmGroup2 = cswmItem.AddGroup()
        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Ascending"
        cswmItem.ID = "Asc"
        If Session("RankDescOrd") = False Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Descending"
        cswmItem.ID = "Desc"
        If Session("RankDescOrd") = True Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2011/07/04
        If Session("bRank") = True Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "No Rank"
        End If

        'jrs 2009/01/15
        '*** begin ***'
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Pct of Tot. Options"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent of Total"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset"
        cswmItem.ID = "mnuPctTotReset"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Comparison Options"
        cswmGroup1.ID = "mnuCompOpt"
        'jrs 2009/01/30
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    cswmGroup1.ShowMenuBarItem = False
        'Else
        '    cswmGroup1.ShowMenuBarItem = True
        'End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Ratio Comparison (a/b)"
        cswmItem.ID = "mnuCompRatAB"
        If Session("sComparison") = "RatAB" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Ratio Comparison (b/a)"
        cswmItem.ID = "mnuCompRatBA"
        If Session("sComparison") = "RatBA" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Difference Comparison (a-b)"
        cswmItem.ID = "mnuCompDiffAB"
        If Session("sComparison") = "DiffAB" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Difference Comparison (b-a)"
        cswmItem.ID = "mnuCompDiffBA"
        If Session("sComparison") = "DiffBA" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        'cdv 2021/12/14 ****START****
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scenario Ratio Comparison (a/(b...n))"
        cswmItem.ID = "mnuCompRatAOther"
        If Session("sComparison") = "RatAOther" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scenario Ratio Comparison ((b...n)/a)"
        cswmItem.ID = "mnuCompRatOtherA"
        If Session("sComparison") = "RatOtherA" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scenario Difference Comparison (a-(b...n))"
        cswmItem.ID = "mnuCompDiffAOther"
        If Session("sComparison") = "DiffAOther" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Scenario Difference Comparison ((b...n)-a)"
        cswmItem.ID = "mnuCompDiffOtherA"
        If Session("sComparison") = "DiffOtherA" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        'cdv 2021/12/14 ****END****
        'jrs 2019/07/29
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Comparison (a-b)/b*100"
        cswmItem.ID = "mnuCompPctAB"
        If Session("sComparison") = "PctAB" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Comparison (b-a)/a*100"
        cswmItem.ID = "mnuCompPctBA"
        If Session("sComparison") = "PctBA" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset"
        cswmItem.ID = "mnuCompReset"
        cswmItem.IconImage = "blank.gif"
        '*** end ***'
        'jrs 2009/01/15

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Run Horizon"
        cswmGroup1.ID = "Group_1f5a"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Display Horizon"

        Session("GraphDisplayParent") = "frm_TableDisplay.aspx"

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16030);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS("Table Display", "")




        If Page.IsPostBack Then
            RefreshTable()
        Else
            'jrs 2013/06/06
            If Session("PerfRiskAction") = "Rank" Then
                mnuRank_Click()
                'Find and color cell
                For i = 1 To NR
                    If Session("GR1N")(i) = Session("PerfRiskCountry") Then
                        Exit For
                    End If
                Next
                Dim j As Integer, rr As Integer = 0
                For j = 3 To grd_TableDisplay.Rows.Count - 1
                    If grd_TableDisplay.Rows(j).Cells(0).Text > "" Then
                        rr = Session("PerfRiskYear") - Session("FirstYear") + j
                        Exit For
                    End If
                Next
                grd_TableDisplay.Rows(rr).Cells(i).BackColor = Session("PerfRiskColor")
                Session("PerfRiskAction") = Nothing

                'JAM 2013-06-08 Background Color for body cells
                GridView1.Rows(rr - j).Cells(i).BackColor = Session("PerfRiskColor")
                'JAM 2013-06-08 Adding ID cell in order to focus cell
                GridView1.Rows(rr - j).Cells(i).ID = "C" & i.ToString
                'jrs 2013/06/10
                Dim sJs As String
                If rr - j + 2 < 10 Then
                    sJs = "location.hash = 'GridView1__ctl1_C" & CStr(i) & "';"
                Else
                    sJs = "location.hash = 'GridView1__ctl" & CStr(rr - j + 2) & "_C" & CStr(i) & "';"
                End If
                ClientScript.RegisterClientScriptBlock(Me.[GetType](), "FocusCell", sJs, True)
            End If
        End If

        'jrs 2021/05/05
        If Session("FilesCol") Is Nothing Then
            FilesCol = New Collection
        Else
            FilesCol = Session("FilesCol")
        End If
        If Session("dfCol") Is Nothing Then
            dfCol = New Collection
        Else
            dfCol = Session("dfCol")
        End If


        'jrs 2005/12/10
        Session("TableHis") = Nothing

        'jrs 2025/07/18
        'ResetTrillionsVars()

        ''JAM 2016-03-05
        If Session("bTrillionsOk") Is Nothing Then
            Session("bTrillionsOk") = False
        End If

        Call Convert_Billions_To_Trillions()


    End Sub

    'jrs 2009/01/30
    ''==========================frm_TableDisplay==================
    'Private Sub frm_TableDisplay_Closed()
    '    Dim rr As Short
    '    Dim cc As Short
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    'mti Validation changes

    '    'reset values
    '    'JRS 11/25/02
    '    If False Or Session("PercentOn") = "True" Then 'JRSVBNET mnuValidation.Text = "&Data" Or Session("PercentOn") = "True" Then
    '        For cc = 1 To Session("COUNTE")
    '            For rr = 1 To DisYear + 1
    '                Session("PLOT")(rr, cc) = SPLOT(rr, cc)
    '            Next rr
    '        Next cc

    '        'JRSVBNET mnuValidation.Text = "Validation"
    '        'JRSVBNET mnuPercent.Enabled = True
    '    End If

    '    'jrs 2005/05/17
    '    If (Session("bCum") = True) Then
    '        mnuCumOff_Click()
    '    End If
    '    'mti Validation changes
    '    'JRS 12/11/02 FILTER
    '    Session("Fil_Region") = ""
    'End Sub


    Public Sub mnuByPair_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes 12 August, 2002 - whole procedure

        If cmdContinue.Visible = True Then cmdContinue_Click(cmdContinue, New System.EventArgs)
        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, 1)
        mnuViewPair_Click(Me, New System.EventArgs)

    End Sub


    Public Sub mnuDefine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------
        'mti 07 Feb 03 - whole proc
        'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.ShowDialog().html'.
        'frm_NameDisp.DefInstance.ShowDialog() 

    End Sub


    Public Sub mnuDrivers_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN ***
        '      '------------------------------------------------------------
        ''mti 07 Feb 03- whole proc
        'Dim TStr As String
        'Dim pos1 As Short


        'TStr = grd_TableDisplay.get_TextMatrix(0, grd_TableDisplay.col)
        'pos1 = InStr(TStr, "[")
        'If pos1 > 0 Then
        '	frm_Drivers.DefInstance.VName = Mid(TStr, 1, pos1 - 1)
        'Else
        '	frm_Drivers.DefInstance.VName = TStr
        'End If
        'frm_Drivers.DefInstance.ShowDialog()
        'JRSVBNET *** END ***
    End Sub


    Public Sub mnuEquations_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim chk As Object
        '-----------------------------------------------------------------------------------------

        Dim THelp As Short
        Dim VName As String

        Dim pos As Short
        With grd_TableDisplay
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
            'JRSVBNET VName = 'JRSVBNET Support.DataGridManager.GetText(grd_TableDisplay)
            pos = InStr(1, VName, "[")
            If pos > 0 Then
                VName = VB.Left(VName, pos - 1)
            End If
        End With

        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        chk = 0
        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        chk = IfsVars.Item(VName)

        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If chk <= 0 Then

            LoadMessage("IFs Variable not defined.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'THelp = Me.HelpContextID
        'UPGRADE_WARNING: Couldn't resolve default property of object chk. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_ISSUE: Form property frm_TableDisplay.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET Me.HelpContextID = VHelpIDEqn(chk)
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.SendKeys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.SendKeys.Send("{F1}")
        'Me.HelpContextID = THelp

    End Sub

    Public Sub mnuFilter_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRS 06/27/03
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If VDim1(Session("SEL")) = 1 And VDim2(Session("SEL")) = 1 And Session("COUNTE") = NR Then
            If Session("GR1N")(1) = Session("GR1N")(NR) Then
                cswmWebMenu.Groups("mnuFilter").ShowMenuBarItem = False
                Session("TableHideFilter") = True

                LoadMessage("For Filter to work on variables dimensioned " & "region by region, please select All for first region dimension.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
        End If
    End Sub




    Public Sub mnuSaveDec10_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim cc As Object
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/27
        'Dim GraphOutputFileName As Object
        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002


        '   Get graph file
        '   --------------

        'jrs 2012/01/27
        '            'jrs 2005/02/16
        '            Dim fNum As Integer
        '            fNum = FreeFile()

        '            GraphOutputFileName = Session("PutFdrvRun") & "Save.csv"

        '            On Error GoTo SelGraphFileError_Line
        '            'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        '            'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        '            'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        '            'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'JRSVBNET cmd_ScriptFile.Action = 1
        '            'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then

        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")

        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteGraphFile_Line:

        'FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        'JRS 10/02/03
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        LoadCurrencyFct()

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        SaveDec(10, Rpoints)

        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************

    End Sub


    Public Sub mnuSaveDec5_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim cc As Object
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/27 Dim GraphOutputFileName As Object
        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002


        'jrs 2012/01/27
        '            '   Get graph file
        '            '   --------------

        '            GraphOutputFileName = Session("PutFdrvRun") & "Save.csv" 'JRSVBNET cmd_ScriptFile.Filename

        '            On Error GoTo SelGraphFileError_Line
        '            'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        '            'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        '            'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        '            'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'JRSVBNET cmd_ScriptFile.Action = 1
        '            'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then

        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")

        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteGraphFile_Line:

        '            'jrs 2005/02/16
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/3/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        'JRS 10/02/03
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        LoadCurrencyFct()

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        'jrs 2012/01/27 removed , GraphOutputFileName, fNum to avoid creating file in server (concurrency problems)
        SaveDec(5, Rpoints)

        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************

    End Sub


    Public Sub mnuSaveDecAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim cc As Object
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/27 Dim GraphOutputFileName As Object
        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002

        'jrs 2012/01/27
        '            '   Get graph file
        '            '   --------------

        '            GraphOutputFileName = Session("PutFdrvRun") & "Save.csv" 'JRSVBNET cmd_ScriptFile.Filename

        '            On Error GoTo SelGraphFileError_Line
        '            'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        '            'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        '            'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        '            'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'JRSVBNET cmd_ScriptFile.Action = 1
        '            'JRSVBNET 'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")

        '            End If

        'WriteGraphFile_Line:
        '            'jrs 2005/02/16
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        'JRS 10/02/03
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        LoadCurrencyFct()

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        'jrs 2012/01/27 removed , GraphOutputFileName, fNum
        SaveDec(1, Rpoints)

        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************

    End Sub



    Public Sub mnuSaveTransAllYrs_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim cc As Object
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/27 Dim GraphOutputFileName As Object
        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002


        'jrs 2012/01/27
        '            '   Get graph file
        '            '   --------------

        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            GraphOutputFileName = Session("PutFdrvRun") & "Save.csv" 'JRSVBNET cmd_ScriptFile.Filename

        '            On Error GoTo SelGraphFileError_Line
        '            'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        '            'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        '            'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'JRSVBNET cmd_ScriptFile.Action = 1
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'JRSVBNET 'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            'UPGRADE_WARNING: Couldn't resolve default property of object CDERR_CANCEL. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteGraphFile_Line:
        '            'jrs 2005/02/16
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        For cc = 0 To Session("COUNTE")
            'jrs 2012/01/27 removed , fNum
            LoadColumn(cc, Rpoints, 1, False)
        Next cc
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        'jrs 2012/01/27 FileClose(fNum)
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************
        ' Default Response.ContentType = "application/octet-stream"
        'Select Case ext
        '    Case ".htm", ".html"
        '        Type = "text/HTML"
        '    Case ".txt"
        '        Type = "text/plain"
        '    Case ".doc", ".rtf"
        '        Type = "Application/msword"
        '    Case ".csv", ".xls"
        '        Type = "Application/x-msexcel"
        '    Case Else
        '        Type = "text/plain"
        'End Select

    End Sub


    Public Sub mnuSaveTrans5_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim cc As Object
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/27 Dim GraphOutputFileName As Object
        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002

        'jrs 2012/01/27
        '            '   Get graph file
        '            '   --------------

        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            GraphOutputFileName = Session("PutFdrvRun") & "Save.csv" 'JRSVBNET cmd_ScriptFile.Filename

        '            On Error GoTo SelGraphFileError_Line
        '            'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        '            'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        '            'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'JRSVBNET cmd_ScriptFile.Action = 1
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            'UPGRADE_WARNING: Couldn't resolve default property of object CDERR_CANCEL. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")

        '            End If

        'WriteGraphFile_Line:
        '            'jrs 2005/02/16
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        For cc = 0 To Session("COUNTE")
            'jrs 2012/01/27 removed , fNum
            LoadColumn(cc, Rpoints, 5, False)
        Next cc
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        'jrs 2012/01/27 FileClose(fNum)
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************

    End Sub


    Public Sub mnuSaveTrans10_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim cc As Object
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/27 Dim GraphOutputFileName As Object
        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002

        'jrs 2012/01/27
        '            '   Get graph file
        '            '   --------------

        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            GraphOutputFileName = Session("PutFdrvRun") & "Save.csv" 'JRSVBNET cmd_ScriptFile.Filename

        '            On Error GoTo SelGraphFileError_Line
        '            'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        '            'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        '            'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        '            'JRSVBNET cmd_ScriptFile.Action = 1
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            'JRSVBNET GraphOutputFileName = 'JRSVBNET cmd_ScriptFile.Filename
        '            '        On Error GoTo 0
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            'UPGRADE_WARNING: Couldn't resolve default property of object CDERR_CANCEL. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            If Err.Number = CDERR_CANCEL Then
        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)
        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteGraphFile_Line:
        '            'jrs 2005/02/16
        '            Dim fNum As Integer
        '            fNum = FreeFile()
        '            'UPGRADE_WARNING: Couldn't resolve default property of object GraphOutputFileName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        '            FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        For cc = 0 To Session("COUNTE")
            'jrs 2012/01/27 removed fNum
            LoadColumn(cc, Rpoints, 10, False)
        Next cc
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

        'jrs 2012/01/27 FileClose(fNum)
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/27
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************

    End Sub

    Public Sub mnuViewCode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim CurVar As Object
        '----------------------------------------------------------------------------------------
        Dim valtbl As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM IFSVar", "NAME", valtbl)

        Dim pos As Short
        With grd_TableDisplay
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
            'UPGRADE_WARNING: Couldn't resolve default property of object CurVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET CurVar = 'JRSVBNET Support.DataGridManager.GetText(grd_TableDisplay)
            'UPGRADE_WARNING: Couldn't resolve default property of object CurVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            pos = InStr(1, CurVar, "[")
            If pos > 0 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object CurVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                CurVar = VB.Left(CurVar, pos - 1)
            End If
        End With

        'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1039"'
        'JRSVBNET Load(frm_Code)
        'UPGRADE_WARNING: Couldn't resolve default property of object CurVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET frm_Code.DefInstance.codeRTBox.Text = CurVar + "<br>" + New String("-", Len(CurVar) + 10) + "<br>"



        'JRSVBNET If Not .NoMatch Then
        'JRSVBNET frm_Code.DefInstance.codeRTBox.Text = frm_Code.DefInstance.codeRTBox.Text & "<br>" + SeekValue(valtbl, CurVar, "vbcode")
        'JRSVBNET End If


        'JRSVBNET frm_Code.DefInstance.ShowDialog()

    End Sub

    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '------------------------------------------------------------

        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
        Support.FormManager.Close(Me)

    End Sub

    Public Sub mnuFilterMax_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim i As Object
        Dim j As Object
        'JRS 12/10/02

        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterNone").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMin").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMax").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRange").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRate").IconImage = "blank.gif"

        'Display all regions or groups with at least
        'one value bigger than filter variable
        FilterAction("Max")

        'jrs 2012/01/16
        If Not Session("bFiltDisplayCount") Then
            'format grid
            With grd_TableDisplay
                For j = 1 To .Rows(0).Cells.Count - 1
                    'jrs 2005/05/17
                    For i = 6 To .Rows.Count - 1
                        'jrs 2007/02/16 If IsNumeric(Session("PLOT")(i - 5, j)) Then
                        'using intervals
                        If IsNumeric(.Rows(i).Cells(j).Text) Then
                            'jrs 2007/02/16 If Session("PLOT")(i - 5, j) <= Session("Fil_Max_Level") Then
                            If .Rows(i).Cells(j).Text <= Session("Fil_Max_Level") Then
                                .Rows(i).Cells(j).Font.Bold = True
                            Else
                                .Rows(i).Cells(j).Font.Bold = False
                            End If
                        End If
                    Next i
                Next j
            End With
        End If

        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("Fil_VarName")
        If Session("Fil_Dim2") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim2")
            If Session("Fil_Dim3") > "" Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim3")
            End If
        End If
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & ": Filter Maximum Level " & Str(Session("Fil_Max_Level"))
        'JRS 03/21/04
        Session("mnuFilter") = "mnuFilterMax"

        'JAM 2012-04-03
        Call BuildTable()
    End Sub


    Public Sub mnuFilterMin_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim i As Object
        Dim j As Object
        'JRS 12/10/02

        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterNone").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMin").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMax").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRange").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRate").IconImage = "blank.gif"

        'Display all regions or groups with at least
        'one value bigger than filter variable
        FilterAction("Min")
        'jrs 2012/01/16
        If Not Session("bFiltDisplayCount") Then
            'format grid
            With grd_TableDisplay
                For j = 1 To .Rows(0).Cells.Count - 1
                    'jrs 2005/05/17
                    For i = 6 To .Rows.Count - 1
                        'jrs 2007/02/16 If IsNumeric(Session("PLOT")(i - 5, j)) Then
                        'using intervals
                        If IsNumeric(.Rows(i).Cells(j).Text) Then
                            'jrs 2007/02/16 If Session("PLOT")(i - 5, j) >= Session("Fil_Min_Level") Then
                            If .Rows(i).Cells(j).Text >= Session("Fil_Min_Level") Then
                                .Rows(i).Cells(j).Font.Bold = True
                            Else
                                .Rows(i).Cells(j).Font.Bold = False
                            End If
                        End If
                    Next i
                Next j
            End With
        End If
        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("Fil_VarName")
        If Session("Fil_Dim2") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim2")
            If Session("Fil_Dim3") > "" Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim3")
            End If
        End If
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & ": Filter Minimum Level " & Str(Session("Fil_Min_Level"))
        'JRS 03/21/04
        Session("mnuFilter") = "mnuFilterMin"
        'JAM 2012-04-03
        Call BuildTable()
    End Sub

    Public Sub mnuFilterNone_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'JRS 12/10/02
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterNone").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMin").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMax").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRange").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRate").IconImage = "blank.gif"

        If Session("Fil_Region") = "" Then

            LoadMessage("No filter variable available", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'jrs 2012/01/17
        Dim i As Short
        Dim j As Short
        If Session("bFiltLoadedValues") Then
            ResetForFilter()
            Session("COUNTE") = UBound(Session("FilterBackup"), 2)
            RefreshTable()
            ''format grid
            'With grd_TableDisplay
            '    For j = 1 To .Rows(0).Cells.Count - 1
            '        'jrs 2006/05/29 dim3
            '        For i = 6 To .Rows.Count - 1
            '            .Rows(i).Cells(j).Font.Bold = True
            '        Next i
            '    Next j
            'End With
            Session("mnuFilter") = "mnuFilterNone"
            Exit Sub
        End If

        'Define filter variables
        Dim fileIdx As Short
        fileIdx = InStr(1, Session("VARN")(1, 1), "[")
        'jrs 2006/08/28 error if no regions found.
        If Session("COUNTE") > 0 Then
            If fileIdx > 0 Then
                Session("Fil_VarName") = VB.Left(Session("VARN")(1, 1), fileIdx - 1)
                Session("Fil_File") = Val(Mid(Session("VARN")(1, 1), fileIdx + 1, Len(Session("VARN")(1, 1)) - fileIdx - 1))
            Else
                Session("Fil_VarName") = Session("VARN")(1, 1)
                'jrs 2005/12/20 RAM for Formulas
                bUsingComp = Not (Session("aFormulas")(1) Is Nothing)
                'jrs 2006/01/22
                If bUsingComp Then
                    If Session("aFormulas")(1).Name <> Session("VARN")(1, 1) Then
                        bUsingComp = False
                    End If
                End If
                If Session("FirstYear") = FirstYearHistory Then
                    Session("Fil_File") = -1
                Else
                    Session("Fil_File") = 0
                End If

                Session("Fil_File") = -1
            End If
            If Session("Fil_Region") = "" Then
                Session("Fil_Region") = Session("GR1N")(1)
                Session("Fil_Dim2") = Session("GR2N")(1)
                Session("Fil_Dim3") = Session("GR3N")(1)
            End If
        End If
        'define if working with groups
        bFilter_Group = False
        For i = 1 To GCount
            If Session("Fil_Region") = GroupNames(i) Then
                bFilter_Group = True
                Exit For
            End If
        Next i
        'JRS 12/16/02        
        If cswmWebMenu.Groups("mnuDispOpt").Items("mnuPercent").Caption = "Whole" Then
            mnuPercent_Click(Me, New System.EventArgs)
        End If
        'clear plot
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))
        'jrs 2005/12/19 RAM for Formulas
        If bUsingComp Then
            FilterCompNone()
        Else
            Dim iRowPosition As Integer, iFY As Integer
            iRowPosition = 1
            iFY = Session("FirstYear")
            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                iFY = FirstYearForecast
                iRowPosition = LastYearHistory - FirstYearHistory + 1
            End If

            'jrs 2007/02/19
            Dim bPoverty As Boolean
            Dim iPos As Integer, iPos2 As Integer, iPos3 As Integer
            Dim level As Single
            Dim bPctPop As Boolean
            Dim bFind As Boolean
            'jrs 2014/10/21
            Dim b2005PPP As Boolean, iPos4 As Integer, iPos5 As Integer, bIncRange As Boolean, level2 As Single

            iPos = InStr(Session("Fil_VarName"), "INCOMELT")
            iPos2 = InStr(Session("Fil_VarName"), "LN")
            iPos3 = InStr(Session("Fil_VarName"), "%")
            'jrs 2014/10/21
            iPos4 = InStr(Session("Fil_VarName"), "GT")
            iPos5 = InStr(Session("Fil_VarName"), "2011")

            bPoverty = False
            bPctPop = False
            bFind = False
            'jrs 2014/10/21
            b2005PPP = False
            bIncRange = False
            If iPos > 0 And iPos2 > 0 Then
                bFind = (iPos3 > 0 And iPos3 < iPos2)
                If bFind Then
                    level = Val(Mid(Session("Fil_VarName"), 9, iPos2 - 10))
                Else
                    'jrs 2011/06/17
                    If iPos4 > 0 Then
                        level2 = Val(Mid(Session("Fil_VarName"), 9, iPos4 - 9))
                        'jrs 2013/03/12 level = Val(mid(sVar, iPos4 + 2, iPos2 - 11 - Len(CStr(level))))
                        level = Val(Mid(Session("Fil_VarName"), iPos4 + 2, iPos2 - 11 - Len(CStr(level2))))
                        bIncRange = True
                        If InStr(Session("Fil_VarName"), "%") > 0 Then
                            bPctPop = True
                        End If
                    Else
                        level = Val(Mid(Session("Fil_VarName"), 9, iPos2 - 9))
                        If level <> 1 And level <> 2 Then
                            bPoverty = True
                        End If
                        If Right(Session("Fil_VarName"), 1) = "%" Then
                            bPctPop = True
                        End If
                    End If
                End If
            End If
            'jrs 2014/10/21
            If iPos5 > iPos2 Then
                b2005PPP = True
            End If

            'if working with forecast
            Dim idx As Short
            If Session("Fil_File") > -1 Then
                idx = IfsVars.Item(Session("Fil_VarName")).Index
                Session("COUNTE") = Session("COUNTE") + 1
                'jrs 2007/02/19
                If bPoverty Or bPctPop Then
                    gLoadPovertyDisp(level, Session("Fil_Region"), Session("Fil_File"), bFilter_Group, bPctPop, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                    Session("SensitivityFlag"), Session("mnuSQLite"))
                ElseIf bFind Then 'jrs 2007/03/07
                    gFindPovertyLevel(Session("Fil_Region"), Session("Fil_File"), bFilter_Group, level, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                Else
                    'reload plot
                    gLoadForDisplayNET(Session("Fil_VarName"), idx, Session("Fil_Region"), -1, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), True, True, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    'gLoadForDisplay(Session("Fil_VarName"), idx, Session("Fil_Region"), -1, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), True, True, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    'iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, Session("Fil_Region"), Session("Fil_Dim2"), Session("Fil_Dim3"), bFilter_Group, Session("Fil_File"), "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

                    'jrs 2005/07/28
                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                        'jrs 2006/06/08 lastyearhistory -1 to keep forecast value in year 2000
                        gLoadForDisplayHis(Session("Fil_VarName"), Session("Fil_Region"), -1, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                        bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                        Session("FinYear")(Session("COUNTE")) = Session("LastYear") - FirstYearHistory + 1
                    End If
                End If
                'if not using forecast but history
            Else
                Session("COUNTE") = Session("COUNTE") + 1
                Dim sDef As String, sUn As String
                gLoadForDisplayHis(Session("Fil_VarName"), Session("Fil_Region"), -1, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory,
                False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                gLoadForDisplayHisSaveLbls(Session("COUNTE"), Session("Fil_VarName"), Session("Fil_Region"), bFilter_Group, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
            End If
        End If 'eof computations 
        'refresh grid
        RefreshTable()
        'jrs 2012/01/16
        'format grid
        'With grd_TableDisplay
        '    For j = 1 To .Rows(0).Cells.Count - 1
        '        'jrs 2006/05/29 dim3
        '        For i = 6 To .Rows.Count - 1
        '            .Rows(i).Cells(j).Font.Bold = True
        '        Next i
        '    Next j
        'End With
        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("Fil_VarName") & " - " & Session("Fil_Region")
        If Session("Fil_Dim2") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim2")
            If Session("Fil_Dim3") > "" Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim3")
            End If
        End If
        'JRS 03/21/04
        Session("mnuFilter") = "mnuFilterNone"
    End Sub

    Public Sub mnuFilterParameters_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRS 12/10/02
        'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.ShowDialog().html'.
        'frm_FilterParameters.DefInstance.ShowDialog() 
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_FilterParameters.aspx")
    End Sub

    Public Sub mnuFilterRange_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim i As Object
        Dim j As Object
        'JRS 12/10/02

        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterNone").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMin").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMax").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRange").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRate").IconImage = "blank.gif"

        'Display all regions or groups with at least
        'one value bigger than filter variable
        FilterAction("Range")
        'jrs 2012/01/16
        If Not Session("bFiltDisplayCount") Then
            'format grid
            With grd_TableDisplay
                For j = 1 To .Rows(0).Cells.Count - 1
                    'jrs 2005/05/17
                    For i = 6 To .Rows.Count - 1
                        'jrs 2007/02/16 If IsNumeric(Session("PLOT")(i - 5, j)) Then
                        'using intervals
                        If IsNumeric(.Rows(i).Cells(j).Text) Then
                            'jrs 2007/02/16 If Session("PLOT")(i - 5, j) <= Session("Fil_Range_Max") And Session("PLOT")(i - 5, j) >= Session("Fil_Range_Min") Then
                            If .Rows(i).Cells(j).Text <= Session("Fil_Range_Max") And .Rows(i).Cells(j).Text >= Session("Fil_Range_Min") Then
                                .Rows(i).Cells(j).Font.Bold = True
                            Else
                                .Rows(i).Cells(j).Font.Bold = False
                            End If
                        End If
                    Next i
                Next j
            End With
        End If
        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("Fil_VarName")
        If Session("Fil_Dim2") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim2")
            If Session("Fil_Dim3") > "" Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim3")
            End If
        End If
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & ": Filter Range from " & Str(Session("Fil_Range_Min")) & " to " & Str(Session("Fil_Range_Max"))
        'JRS 03/21/04
        Session("mnuFilter") = "mnuFilterRange"
        'JAM 2012-04-03
        Call BuildTable()
    End Sub

    Public Sub mnuFilterRate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim i As Object
        Dim j As Object
        'JRS 12/10/02

        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterNone").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMin").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterMax").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRange").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuFilter").Items("mnuFilterRate").IconImage = "checkbox.gif"

        'Display all regions or groups with at least
        'one value bigger than filter variable
        FilterAction("Rate")
        'jrs 2012/01/16
        If Not Session("bFiltDisplayCount") Then
            'format grid
            With grd_TableDisplay
                'jrs 2007/02/16
                Dim Val1 As Single

                For j = 1 To .Rows(0).Cells.Count - 1
                    'jrs 2007/02/16 If IsNumeric(Session("PLOT")(2, j)) And IsNumeric(Session("PLOT")(1, j)) Then
                    'If (Session("PLOT")(2, j) - Session("PLOT")(1, j)) * 100 >= Session("Fil_Rate") * Session("PLOT")(1, j) Then
                    '    .Rows(6).Cells(j).Font.Bold = True
                    'Else
                    '    .Rows(6).Cells(j).Font.Bold = False
                    'End If
                    'jrs 2007/02/16 For i = 7 To .Rows.Count - 1
                    For i = 6 To .Rows.Count - 2
                        If IsNumeric(.Rows(i).Cells(j).Text) And IsNumeric(.Rows(i + 1).Cells(j).Text) Then
                            Val1 = Val(.Rows(i).Cells(j).Text)
                            If Val1 = 0 Then
                                .Rows(i + 1).Cells(j).Font.Bold = False
                            Else
                                'If IsNumeric(Session("PLOT")(i - 5, j)) And IsNumeric(Session("PLOT")(i - 6, j)) Then
                                '    If (Session("PLOT")(i - 5, j) - Session("PLOT")(i - 6, j)) * 100 >= Session("Fil_Rate") * Session("PLOT")(i - 6, j) Then
                                '        .Rows(i).Cells(j).Font.Bold = True
                                '    Else
                                '        .Rows(i).Cells(j).Font.Bold = False
                                '    End If
                                'End If
                                If Session("Interval") > 1 Then
                                    If Math.Sign(Val(.Rows(i + 1).Cells(j).Text)) = Math.Sign(Val1) Then
                                        If (Val(.Rows(i + 1).Cells(j).Text) / Val1) ^ (1 / Session("Interval")) - 1 >= Session("Fil_Rate") / 100 Then
                                            .Rows(i + 1).Cells(j).Font.Bold = True
                                        Else
                                            .Rows(i + 1).Cells(j).Font.Bold = False
                                        End If
                                    Else
                                        .Rows(i + 1).Cells(j).Font.Bold = False
                                    End If
                                Else
                                    If (Val(.Rows(i + 1).Cells(j).Text) - Val1) / Val1 >= Session("Fil_Rate") / 100 Then
                                        .Rows(i + 1).Cells(j).Font.Bold = True
                                    Else
                                        .Rows(i + 1).Cells(j).Font.Bold = False
                                    End If
                                End If
                            End If
                        End If
                    Next i
                    'jrs 2007/02/16 End If
                Next j
            End With
        End If
        'Title
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Session("Fil_VarName")
        If Session("Fil_Dim2") > "" Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim2")
            If Session("Fil_Dim3") > "" Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & " - " & Session("Fil_Dim3")
            End If
        End If
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & ": Filter Rate " & Str(Session("Fil_Rate")) & "%"
        'JRS 03/21/04
        Session("mnuFilter") = "mnuFilterRate"
        'JAM 2012-04-03
        Call BuildTable()
    End Sub

    Public Sub mnuPercent_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        'jrs 2009/01/30 
        ''jrs 2008/06/26
        'Dim bMovAvg As Boolean
        'bMovAvg = Session("bMovingAvg")
        'If bMovAvg Then
        '    If Session("PercentOn") = "False" Then
        '        mnuMovingAvgOff_Click()
        '    Else
        '        Session("bMovingAvg") = False
        '    End If
        'End If

        Dim Numer As Object
        Dim rr As Short
        Dim cc As Short
        Dim Msg As String
        '------------------------------------------------------------

        'JRS 1/8/03
        grd_TableDisplay.Visible = False

        If Session("PercentOn") = "False" Then
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Percent")

            Session("PercentOn") = "True"
        Else
            'jrs 2009/01/30
            If CType(Session("TblDispOpt"), Stack).Peek <> "Percent" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            Session("PercentOn") = "False"
        End If

        '   Compute Percentages or Reload Absolute Values
        '   ---------------------------------------------

        If Session("PercentOn") = "True" Then 'Compute Percentages
            'JRS 07/04/03 it used to be 100
            If Session("COUNTE") > 200 Then
                Msg = "There are too many output variables selected for this operation."
                Session("PercentOn") = "False"

                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            For cc = 1 To Session("COUNTE")
                For rr = DisYear To 2 Step -1 'jrs 2005/11/21 remove + 1 from rr = DisYear
                    'jrs 2005/03/23
                    If IsDBNull(Session("PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = ""
                    End If
                    'jrs 2005/05/20
                    If IsDBNull(Session("PLOT")(rr - 1, cc)) Then
                        Session("PLOT")(rr - 1, cc) = ""
                    End If

                    'jrs 2009/01/30
                    ''jrs 2005/10/14
                    'If Session("bIntAvg") = False Then
                    '    SPLOT(rr, cc) = Session("PLOT")(rr, cc)
                    'End If
                    Session("Percent_PLOT")(rr, cc) = Session("PLOT")(rr, cc)

                    'jrs 2008/07/31
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        'jrs 2005/08/19
                        If Session("Interval") > 1 Then
                            If rr - Session("Interval") < 1 Then
                                'jrs 2026/02/18 changed limit from 0.0001 to 0.0000001
                                If System.Math.Abs(Val(Session("PLOT")(1, cc))) < 0.0000001 Then
                                    Session("PLOT")(rr, cc) = 99999999
                                Else
                                    Session("PLOT")(rr, cc) = ((Session("PLOT")(rr, cc) / Session("PLOT")(1, cc)) ^ (1 / (rr - 1)) - 1) * 100 'jrs 2006/07/14 changed (1/Interval) for (1/(rr-1))
                                End If
                            Else
                                'jrs 2005/11/21
                                If IsDBNull(Session("PLOT")(rr - Session("Interval"), cc)) Then
                                    Session("PLOT")(rr - Session("Interval"), cc) = ""
                                End If
                                'jrs 2005/11/21 added val()
                                'jrs 2026/02/18 changed limit from 0.0001 to 0.0000001
                                If System.Math.Abs(Val(Session("PLOT")(rr - Session("Interval"), cc))) < 0.0000001 Then
                                    Session("PLOT")(rr, cc) = 99999999
                                Else
                                    Session("PLOT")(rr, cc) = ((Session("PLOT")(rr, cc) / Session("PLOT")(rr - Session("Interval"), cc)) ^ (1 / Session("Interval")) - 1) * 100
                                End If
                            End If
                        Else
                            'jrs 2026/02/18 changed limit from 0.0001 to 0.0000001
                            If System.Math.Abs(Val(Session("PLOT")(rr - 1, cc))) < 0.0000001 Then
                                'jrs 2005/05/23
                                If IsNumeric(Session("PLOT")(rr - 1, cc)) Then
                                    Session("PLOT")(rr, cc) = 99999999
                                Else
                                    Session("PLOT")(rr, cc) = ""
                                End If
                            Else
                                'jrs 2005/05/23
                                If IsNumeric(Session("PLOT")(rr, cc)) And IsNumeric(Session("PLOT")(rr - 1, cc)) Then
                                    'jrs 2005/01/21
                                    Numer = (Val(Session("PLOT")(rr, cc)) - Session("PLOT")(rr - 1, cc))
                                    Session("PLOT")(rr, cc) = Numer / Session("PLOT")(rr - 1, cc) * 100
                                Else
                                    Session("PLOT")(rr, cc) = ""
                                End If
                            End If
                        End If 'if interval > 1 else
                    End If 'if is numeric plot(rr, cc)
                Next rr
                'jrs 2009/01/30 SPLOT(1, cc) = Session("PLOT")(1, cc)
                Session("Percent_PLOT")(1, cc) = Session("PLOT")(1, cc)

                Session("PLOT")(1, cc) = 0 'jrs 2005/06/28 Session("PLOT")(2, cc)

            Next cc

            'jrs 2009/01/30 Session("SPLOT") = SPLOT

        Else 'Replace Original Values
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear 'jrs 2015/02/24 + 1
                    'jrs 2009/01/30 Session("PLOT")(rr, cc) = SPLOT(rr, cc)
                    Session("PLOT")(rr, cc) = Session("Percent_PLOT")(rr, cc)
                Next rr

            Next cc

            'jrs 2009/01/30
            ''jrs 2005/10/14
            'If Session("bIntAvg") = True Then
            '    Session("bIntAvg") = False
            '    mnuIntAvgOn_Click()
            'End If
        End If


        '   Fill in top of table
        '   --------------------
        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow, r5 As New TableRow, r6 As New TableRow
        Dim ce1 As New TableCell, ce2 As New TableCell, ce3 As New TableCell, ce4 As New TableCell, ce5 As New TableCell, ce6 As New TableCell
        Dim st As Integer, en As Integer, fn As Integer

        ce1.Text = ""
        ce1.BackColor = System.Drawing.Color.Gray
        r1.Cells.Add(ce1)
        ce2.Text = ""
        ce2.BackColor = System.Drawing.Color.Gray
        r2.Cells.Add(ce2)
        ce3.Text = ""
        ce3.BackColor = System.Drawing.Color.Gray
        r3.Cells.Add(ce3)
        ce4.Text = ""
        ce4.BackColor = System.Drawing.Color.Gray
        r4.Cells.Add(ce4)
        ce5.Text = ""
        ce5.BackColor = System.Drawing.Color.Gray
        r5.Cells.Add(ce5)
        ce6.Text = ""
        ce6.BackColor = System.Drawing.Color.Gray
        r6.Cells.Add(ce6)

        For cc = 1 To Session("COUNTE")
            Dim cet1 As New TableCell, cet2 As New TableCell, cet3 As New TableCell, cet4 As New TableCell, cet5 As New TableCell, cet6 As New TableCell

            cet1.Text = Session("VARN")(1, cc)
            cet1.BackColor = System.Drawing.Color.Gray
            r1.Cells.Add(cet1)
            cet2.Text = Session("GR1N")(cc)
            cet2.BackColor = System.Drawing.Color.Gray
            r2.Cells.Add(cet2)
            cet3.Text = Session("GR2N")(cc)
            cet3.BackColor = System.Drawing.Color.Gray
            r3.Cells.Add(cet3)
            cet4.Text = Session("GR3N")(cc)
            cet4.BackColor = System.Drawing.Color.Gray
            r4.Cells.Add(cet4)
            If Session("PercentOn") = "True" Then
                cet5.Text = "% Change"
            Else
                cet5.Text = Session("GR4N")(cc)
            End If

            cet5.BackColor = System.Drawing.Color.Gray
            r5.Cells.Add(cet5)

            cet6.Text = ""
            cet6.BackColor = System.Drawing.Color.Gray

            st = InStr(cet1.Text, "[", CompareMethod.Text) + 1
            en = InStr(cet1.Text, "]", CompareMethod.Text) + 1
            If st > 0 And en > st Then
                If IsNumeric(Mid(cet1.Text, st, en - st - 1)) Then
                    fn = Int(Mid(cet1.Text, st, en - st - 1))
                    'jrs 2005/05/03
                    If fn <= UBound(Session("ShortSce")) And fn > -1 Then
                        cet6.Text = Session("ShortSce")(fn)
                    ElseIf fn = -1 Then
                        cet6.Text = "Historical"
                    End If
                End If
            Else
                fn = -1
            End If
            r6.Cells.Add(cet6)

        Next cc

        grd_TableDisplay.Rows.Clear()
        grd_TableDisplay.Rows.Add(r1)
        grd_TableDisplay.Rows.Add(r2)
        grd_TableDisplay.Rows.Add(r3)
        grd_TableDisplay.Rows.Add(r4)
        grd_TableDisplay.Rows.Add(r5)
        grd_TableDisplay.Rows.Add(r6)

        '   Fill in data
        '   ------------
        '   Fill in left column of table
        '   ----------------------------
        Dim ry(Timepoints) As TableRow
        Dim Y As Integer
        'JRS 12/08/03
        If bLoadLongitudinalBFYH Then
            Y = iFirstYearBFYH
        Else
            Y = Session("FirstYear")
        End If
        rr = 4 ' was 3
        For cc = 1 To Timepoints
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, rr)
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Str(Y))
            Dim cel1 As New TableCell
            cel1.Text = Str(Y)
            cel1.BackColor = System.Drawing.Color.Gray
            Dim nry As New TableRow
            ry(cc) = nry
            ry(cc).Cells.Add(cel1)

            Y = Y + Session("Interval")
            'JRS 12/08/03
            If bLoadLongitudinalBFYH Then
                If Y > iFirstYearBFYH + DisYear - 1 Then Y = iFirstYearBFYH + DisYear - 1 Else If Y > Session("FirstYear") + DisYear - 1 Then Y = Session("FirstYear") + DisYear - 1
            End If
            rr = rr + 1
        Next cc

        Call FillData(Session("COUNTE"), ry)

        'jrs 2005/06/17
        If Not (cswmWebMenu Is Nothing) Then
            If Session("PercentOn") = "True" Then
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuPercent").Caption = "Whole"
            Else
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuPercent").Caption = "Percent"
            End If
        End If

        'jrs 2009/01/30
        ''jrs 2008/06/26
        'If bMovAvg Then
        '    mnuMovingAvgOn_Click()
        'End If

        'JRS 1/8/03
        grd_TableDisplay.Visible = True
        'JAM 2012-04-03
        Call BuildTable()

    End Sub



    Public Sub mnuPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN 
        '      Dim r As Short
        'Dim VTABL As String
        'Dim col As Short
        'Dim RowNo As Short
        'Dim TabPos As Short
        'Dim ColEnd As Short
        'Dim ColBeg As Short
        'Dim l As Short
        'Dim ColsToPrint As Short
        'Dim PageCenter As Short
        'Dim PrintableCols As Short
        'Dim PrintableLines As Short
        'Dim PrintableChars As Short
        'Dim AvgPrintWidth As Object
        'Dim AvgPrintHeight As Object
        'Dim PageNo As Short
        'Dim LinesPrinted As Short
        'Dim NumString As String
        'Dim TabWidth As Short
        'Dim YearWidth As Short
        ''------------------------------------------------------------

        'Dim ValToPrint As New VB6.FixedLengthString(15)
        'YearWidth = 6
        'TabWidth = 15
        'NumString = "0123456789"


        ''   Temporary for printer substitute
        ''    Picture1.Visible = TRUE
        ''    grd_TableDisplay.Visible = FALSE
        ''    cmd_Print.Visible = FALSE
        ''    cmd_Continue.Visible = FALSE

        ''    frm_TableDisplay.Width = Screen.Width
        ''    frm_TableDisplay.Height = Screen.Height
        ''    frm_TableDisplay.Left = 0
        ''    frm_TableDisplay.Top = 0
        ''    Picture1.Width = frm_TableDisplay.ScaleWidth
        ''    Picture1.Height = frm_TableDisplay.ScaleHeight
        ''    Picture1.Left = 0
        ''    Picture1.Top = 0

        ''   Set/reset lines printed and page number
        ''   ---------------------------------------

        'LinesPrinted = LinesPrintedCarryOver
        'PageNo = PageNoCarryOver


        ''   Set page print area
        ''   -------------------

        ''UPGRADE_ISSUE: Printer method Printer.TextHeight was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object AvgPrintHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'AvgPrintHeight = Printer.TextHeight(NumString)
        ''UPGRADE_ISSUE: Printer method Printer.TextWidth was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        ''UPGRADE_WARNING: Couldn't resolve default property of object AvgPrintWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'AvgPrintWidth = Printer.TextWidth(NumString) / 10
        ''MsgBox Str$(Printer.ScaleHeight) + " " + Str$(Printer.ScaleWidth)
        ''UPGRADE_WARNING: Couldn't resolve default property of object AvgPrintWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_ISSUE: Printer property Printer.ScaleWidth was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'PrintableChars = (Printer.ScaleWidth / AvgPrintWidth) - LeftPrintMargin - RightPrintMargin
        ''UPGRADE_WARNING: Couldn't resolve default property of object AvgPrintHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ''UPGRADE_ISSUE: Printer property Printer.ScaleHeight was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'PrintableLines = (Printer.ScaleHeight / AvgPrintHeight) - TopPrintMargin - BottomPrintMargin
        'PrintableCols = PrintableChars / TabWidth
        ''MsgBox Str$(PrintableCols%)
        'PageCenter = LeftPrintMargin + (PrintableChars / 2)

        ''mti Validation changes 09 August, 2002
        'If 'JRSVBNET Pego1.Visible = True Then 'mnuViewPair
        '	ColsToPrint = 2
        'Else
        '	If 'JRSVBNET mnuValidation.Text = "&Data" Then 'doing Validation
        '		ColsToPrint = VCOUNTE * 2
        '	Else
        '		ColsToPrint = Session("COUNTE")
        '	End If
        'End If
        ''MsgBox Str$(ColsToPrint%)
        ''mti Validation changes 09 August, 2002

        'PageNo = PageNoCarryOver
        'LinesPrinted = LinesPrintedCarryOver

        ''   Space down lines if first print
        ''   -------------------------------

        'If LinesPrinted = 0 Then
        '	For l = 1 To TopPrintMargin
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print() : Next l
        '	LinesPrinted = TopPrintMargin
        'End If



        ''   Column print loop start
        ''   =====================================================================

        'For ColBeg = 1 To ColsToPrint Step PrintableCols

        '	ColEnd = ColBeg + PrintableCols - 1
        '	If ColEnd > ColsToPrint Then ColEnd = ColsToPrint

        '	'       Print table title if present
        '	'       ----------------------------
        '	If Len(Session("TableTitle")) > 0 Then
        '		If LinesPrinted > TopPrintMargin Then
        '			'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '			'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			Printer.Print()
        '			PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)
        '		End If
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print()
        '		PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)
        '		TabPos = PageCenter - Len(Session("TableTitle")) / 2
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print(TAB(TabPos), Session("TableTitle"))
        '		PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print()
        '		PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)
        '	End If

        '	'       Print top labels on sets of columns
        '	'       -----------------------------------

        '	For RowNo = 0 To 3
        '		grd_TableDisplay.row = RowNo
        '		grd_TableDisplay.col = 0
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print(TAB(LeftPrintMargin + 1), grd_TableDisplay.Text)
        '		For col = ColBeg To ColEnd
        '			grd_TableDisplay.col = col
        '			VTABL = grd_TableDisplay.Text
        '			TabPos = LeftPrintMargin + YearWidth + TabWidth * (col - ColBeg) + (TabWidth - Len(VTABL)) / 2
        '			'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '			'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			Printer.Print(TAB(TabPos), VTABL)
        '		Next col
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print()
        '		PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)
        '	Next RowNo

        '	'       Print line of equal signs
        '	'       -------------------------

        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print(TAB(LeftPrintMargin), New String("=", YearWidth))
        '	For col = ColBeg To ColEnd
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print(New String("=", TabWidth))
        '	Next col
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print()
        '	PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)

        '	'       Print values in sets of columns
        '	'       -------------------------------
        '	'mti Validation changes 09 August, 2002
        '	If 'JRSVBNET mnuValidation.Text = "&Data" Then
        '		Timepoints = grd_TableDisplay.rows - 6
        '	Else
        '		Timepoints = grd_TableDisplay.rows - 5 '4
        '	End If
        '	'mti Validation changes 09 August, 2002

        '	For r = 1 To Timepoints
        '		grd_TableDisplay.row = r + 3
        '		grd_TableDisplay.col = 0
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print(TAB(LeftPrintMargin), grd_TableDisplay.Text)
        '		For col = ColBeg To ColEnd
        '			grd_TableDisplay.col = col
        '			TabPos = LeftPrintMargin + YearWidth + TabWidth * (col - ColBeg)
        '			'JRS 09/01/03
        '			'UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			If Not mnuFilterNone.Checked Then Printer.FontBold = False
        '			'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '			'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			Printer.Print(TAB(TabPos))
        '			'JRS 12/26/02
        '			'UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			If Not mnuFilterNone.Checked Then Printer.FontBold = grd_TableDisplay.CellFontBold
        '			'JRS 09/01/03 Printer.Print Tab(TabPos%); grd_TableDisplay.Text;
        '			'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '			'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '			Printer.Print(grd_TableDisplay.Text)
        '		Next col
        '		'JRS 08/25/03
        '		'UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.FontBold = False
        '		'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '		'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '		Printer.Print()
        '		PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)
        '	Next r

        '	'JRS 12/26/02
        '	'UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.FontBold = False
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print()
        '	PageEjectCheck(LinesPrinted, PrintableLines, PageNo, PageCenter)

        'Next ColBeg
        ''===================================================================================

        ''    If MsgBox("Start new page?", 4, "") = 6 Then
        'PageNo = PageNo + 1
        'For l = LinesPrinted To PrintableLines + 3
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print()
        'Next l
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print(TAB(PageCenter), PageNo)
        ''UPGRADE_ISSUE: Printer method Printer.NewPage was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.NewPage()
        ''UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.EndDoc()
        'For l = 1 To TopPrintMargin
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print() : Next l
        'LinesPrintedCarryOver = TopPrintMargin
        ''    Else
        ''       PageEjectCheck LinesPrinted%, PrintableLines%, PageNo%, PageCenter%
        ''        LinesPrintedCarryOver = LinesPrinted%
        ''    End If
        'PageNoCarryOver = PageNo

        ''mti Validation changes 09 August, 2002
        'If 'JRSVBNET Pego1.Visible = True Then
        '	'JRSVBNET Pego1.PEnarg1 = 400 'width in pixels
        '	'JRSVBNET Pego1.PEnarg2 = 300 'height in pixels
        '	''JRSVBNET Pego1.PEstrarg1 = GraphOutputFileName + "\" + msg + "_Distance.jpg"
        '	''JRSVBNET Pego1.PEactions = 22 ' Calls PEcopyjpegtofile

        '	'JRSVBNET Pego1.PEactions = 18

        '	'JRSVBNET Pego2.PEnarg1 = 400 'width in pixels
        '	'JRSVBNET Pego2.PEnarg2 = 300 'height in pixels
        '	''JRSVBNET Pego1.PEstrarg1 = GraphOutputFileName + "\" + msg + "_Distance.jpg"
        '	''JRSVBNET Pego1.PEactions = 22 ' Calls PEcopyjpegtofile

        '	'JRSVBNET Pego2.PEactions = 18

        '	'UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.EndDoc()
        'End If
        ''mti Validation changes 09 August, 2002
        'JRSVBNET *** END ****



    End Sub



    '==========================frm_TableDisplay==================
    Public Sub RefreshTable()
        Dim j As Object
        Dim cc As Short
        Dim rr As Short
        Dim Y As Short
        Dim GRID_ALIGNCENTER As Object
        'jrs 2024/11/25 Dim c1 As Short
        Dim c1 As Integer
        Dim ButSpac As Short
        Dim butWidth As Object
        Dim Resid As Object
        'jrs 2024/11/25 Dim ii As Short
        Dim ii As Integer
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim TotalColWidth As Short
        Dim MaxFormWidth As Short
        Dim MinFormWidth As Short
        Dim ColWidth As Short
        Dim MaxNoRows As Short
        Dim ButSpace As Object
        Dim ButCushion As Object
        Dim RowHeight As Object
        Dim TitleBarSpace As Object
        '------------------------------------------------------------
        'JRS 12/26/02
        If Session("COUNTE") = 0 Then
            'jrs 2005/11/03 Support.FormManager.StateStore(Me)
            'jrs 2005/11/03 Support.FormManager.Show(Me, Session("TableDisplayParent"))
            'jrs 2025/01/29
            LoadMessage("Can't refresh when no variables selected", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        'jrs 2005/06/17 
        ''JRS 12/12/02
        'If Not Page.IsPostBack Then
        '    If Session("PercentOn") = "True" Then
        '        mnuPercent_Click(Me, New System.EventArgs)
        '    End If
        '    Session("PercentOn") = "False"
        'End If
        'mti Validation changes 12 August, 2002
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
        If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
            'back from proessential graph
            Exit Sub
        End If
        'mti Validation changes 12 August, 2002

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 1/8/03
        grd_TableDisplay.Visible = False

        If Session("Interval") = 0 Then Session("Interval") = 1
        'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TitleBarSpace = 425
        'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RowHeight = 230
        'UPGRADE_WARNING: Couldn't resolve default property of object ButCushion. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ButCushion = 200
        'UPGRADE_WARNING: Couldn't resolve default property of object ButCushion. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ButSpace = ButCushion
        'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET MaxNoRows = (VB6.PixelsToTwipsY('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - ButSpace - TitleBarSpace) / RowHeight

        'frm_TableDisplay.Caption = "Table Display"

        '   Set width and left margin of form
        '   ---------------------------------

        ColWidth = Inch
        'UPGRADE_ISSUE: Form method frm_TableDisplay.TextWidth was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET MinFormWidth = TextWidth(Session("TableTitle")) + Inch
        If MinFormWidth < 4 * Inch Then MinFormWidth = 4 * Inch
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET MaxFormWidth = VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width)
        If Session("COUNTE") > 19 Then 'too large causes overflow
            TotalColWidth = 20 * ColWidth
        Else
            TotalColWidth = (Session("COUNTE") + 1) * ColWidth
        End If

        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
        'JRSVBNET If TotalColWidth > MaxFormWidth Then Width = VB6.TwipsToPixelsX(MaxFormWidth)
        If TotalColWidth + HalfInch < MinFormWidth Then
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
            'JRSVBNET Width = VB6.TwipsToPixelsX(MinFormWidth)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
            'JRSVBNET Width = VB6.TwipsToPixelsX(Amin(TotalColWidth + HalfInch, MaxFormWidth))
        End If
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        If False Then 'JRSVBNET VB6.PixelsToTwipsX(Width) < VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) Then
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
            'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
        Else
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
            'Left = 0 
        End If

        '   Set rows of grid
        '   ----------------

        MaxYear = 1
        'JRS 12/08/03
        If bLoadLongitudinalBFYH Then
            MinYear = Session("DisplayLastYear") - iFirstYearBFYH + 1
        Else
            'JRS 11/03/03
            MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        End If
        'jrs 2006/08/28
        If IsNumeric(Session("FinYear")(1)) Then
            For ii = 1 To Session("COUNTE")
                'jrs 2009/02/24
                If IsNumeric(Session("FinYear")(ii)) Then
                    If Session("FinYear")(ii) > MaxYear Then
                        'jrs 2007/11/12 comparing to display years, stored in MinYear
                        'jrs 2008/07/11 If Session("FinYear")(ii) > MinYear Then
                        'compare directly to display years, MinYear can change after a few loops
                        If Session("FinYear")(ii) > Session("DisplayLastYear") - Session("FirstYear") + 1 And Not bLoadLongitudinalBFYH Then
                            MaxYear = Session("DisplayLastYear") - Session("FirstYear") + 1
                        ElseIf Session("FinYear")(ii) > Session("DisplayLastYear") - iFirstYearBFYH + 1 And bLoadLongitudinalBFYH Then
                            MaxYear = Session("DisplayLastYear") - iFirstYearBFYH + 1
                        Else
                            MaxYear = Session("FinYear")(ii) '- 1
                        End If
                    End If

                    If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii) '- 1
                End If
            Next ii
        End If

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            DisYear = MaxYear
        End If

        Resid = (DisYear - 1) Mod Session("Interval")
        Timepoints = 1 + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Timepoints = Timepoints + 1 'Show Final Year Always

        '    Timepoints% = DisYear% / Session("Interval")% + 1
        If Timepoints < 1 Then Timepoints = 1

        'mti Validation changes 19 August, 2002
        If Timepoints < 10 Then
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'JRSVBNET grd_TableDisplay.Rows = Timepoints + 5 'was 6 ?? 'was 3
        Else
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'JRSVBNET grd_TableDisplay.Rows = Timepoints + 5 'was 3
        End If
        'mti Validation changes 19 August, 2002

        'CONVERSION_TODO: The equivalent of Property 'AxMSFlexGridLib.AxMSFlexGrid.FixedRows' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET Support.DataGridManager.manager.SetFixedRows(grd_TableDisplay, 4)


        '   Set height and top margin of form
        '   ---------------------------------
        'JRS 12/14/02
        If False Then 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) + 2 < MaxNoRows Then 
            'JRS 12/10/02
            'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
            'JRSVBNET Height = VB6.TwipsToPixelsY(ButSpace + (grd_TableDisplay.rows + 2) * RowHeight + TitleBarSpace)
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Top.html'.
            'JRSVBNET Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
        Else
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
            'JRSVBNET Height = 'JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Top.html'.
            'Top = 0 
        End If


        '   Set width and left margin of grid
        '   ---------------------------------

        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
        If False Then 'JRSVBNET TotalColWidth < VB6.PixelsToTwipsX(ClientRectangle.Width) Then
            grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(TotalColWidth))
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
            'JRSVBNET grd_TableDisplay.Style.Item("LEFT") = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(ClientRectangle.Width) - VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value)) / 2)
        Else
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
            'JRSVBNET grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(ClientRectangle.Width)
            grd_TableDisplay.Style.Item("LEFT") = 0
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(Amin(VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value), VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - HalfInch)))

        '   Set height of grid
        '   ------------------
        'JRS 12/14/02
        If True Then 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) + 1 <= MaxNoRows Then 
            'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET grd_TableDisplay.Height = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsY(('JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) + 1) * RowHeight + 10))
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            grd_TableDisplay.Height = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsY(MaxNoRows * RowHeight + 10))
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET grd_TableDisplay.Height = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsY(Amin(VB6.PixelsToTwipsY(grd_TableDisplay.Height.Value), VB6.PixelsToTwipsY('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - Inch)))

        '   Position buttons
        '   ----------------

        'butWidth = cmd_Print.Width + cmd_Continue.Width _
        '+ Cmd_Save.Width + cmd_Percent.Width
        'UPGRADE_WARNING: Couldn't resolve default property of object butWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
        'JRSVBNET ButSpac = (VB6.PixelsToTwipsX(ClientRectangle.Width) - butWidth) / 5
        'cmd_Print.left = ButSpac%
        'NextLeft% = cmd_Print.left + cmd_Print.Width + ButSpac%
        'cmd_Percent.left = NextLeft%
        'NextLeft% = cmd_Percent.left + cmd_Percent.Width + ButSpac%
        'Cmd_Save.left = NextLeft%
        'NextLeft% = Cmd_Save.left + Cmd_Save.Width + ButSpac%
        'cmd_Continue.left = NextLeft%
        'cmd_Print.top = grd_TableDisplay.Height + ButCushion / 2
        'cmd_Percent.top = cmd_Print.top
        'Cmd_Save.top = cmd_Print.top
        'cmd_Continue.top = cmd_Print.top

        '   Set number of columns
        '   ---------------------

        'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
        'JRSVBNET grd_TableDisplay.Cols = Session("COUNTE") + 1
        'CONVERSION_TODO: The equivalent of Property 'AxMSFlexGridLib.AxMSFlexGrid.FixedCols' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET Support.DataGridManager.manager.SetFixedCols(grd_TableDisplay, 1)

        '   Fill in top of table
        '   --------------------
        'jrs 2005/02/09
        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow, r5 As New TableRow, r6 As New TableRow
        Dim ce1 As New TableCell, ce2 As New TableCell, ce3 As New TableCell, ce4 As New TableCell, ce5 As New TableCell, ce6 As New TableCell
        Dim st As Integer, en As Integer, fn As Integer

        ce1.Text = ""
        ce1.BackColor = System.Drawing.Color.Gray
        r1.Cells.Add(ce1)
        ce2.Text = ""
        ce2.BackColor = System.Drawing.Color.Gray
        r2.Cells.Add(ce2)
        ce3.Text = ""
        ce3.BackColor = System.Drawing.Color.Gray
        r3.Cells.Add(ce3)
        ce4.Text = ""
        ce4.BackColor = System.Drawing.Color.Gray
        r4.Cells.Add(ce4)
        ce5.Text = ""
        ce5.BackColor = System.Drawing.Color.Gray
        r5.Cells.Add(ce5)
        ce6.Text = ""
        ce6.BackColor = System.Drawing.Color.Gray
        r6.Cells.Add(ce6)

        For c1 = 1 To Session("COUNTE")
            Dim cet1 As New TableCell, cet2 As New TableCell, cet3 As New TableCell, cet4 As New TableCell, cet5 As New TableCell, cet6 As New TableCell

            cet1.Text = Session("VARN")(1, c1)
            cet1.BackColor = System.Drawing.Color.Gray
            r1.Cells.Add(cet1)
            cet2.Text = Session("GR1N")(c1)
            cet2.BackColor = System.Drawing.Color.Gray
            r2.Cells.Add(cet2)
            cet3.Text = Session("GR2N")(c1)
            cet3.BackColor = System.Drawing.Color.Gray
            r3.Cells.Add(cet3)
            cet4.Text = Session("GR3N")(c1)
            cet4.BackColor = System.Drawing.Color.Gray
            r4.Cells.Add(cet4)
            'jrs 2024/02/06 moved Percentage of Total to separate branch
            'jrs 2023/12/01 added Or Session("bPctTot")=true
            If Session("PercentOn") = "True" Or Session("bIntTot") = True Then 'jrs 2011/07/01
                cet5.Text = "% Change"
            ElseIf Session("bPctTot") = True Then
                cet5.Text = "%"
            Else
                cet5.Text = Session("GR4N")(c1)
            End If
            cet5.BackColor = System.Drawing.Color.Gray
            r5.Cells.Add(cet5)
            cet6.Text = ""
            cet6.BackColor = System.Drawing.Color.Gray

            st = InStr(cet1.Text, "[", CompareMethod.Text) + 1
            en = InStr(cet1.Text, "]", CompareMethod.Text) + 1
            If st > 0 And en > st Then
                If IsNumeric(Mid(cet1.Text, st, en - st - 1)) Then
                    fn = Int(Mid(cet1.Text, st, en - st - 1))
                    'jrs 2005/05/03
                    If fn <= UBound(Session("ShortSce")) And fn > -1 Then
                        cet6.Text = Session("ShortSce")(fn)
                    ElseIf fn = -1 Then
                        cet6.Text = "Historical"
                    End If
                End If
            Else
                'jrs 2005/08/19 fn = -1
                st = InStr(Session("VARN")(0, c1), "[") + 1
                en = InStr(Session("VARN")(0, c1), "]") + 1
                If st > 0 And en > st Then
                    cet6.Text = Mid(Session("VARN")(0, c1), st, en - st - 1)
                End If
            End If
            r6.Cells.Add(cet6)


            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, c1)
            'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, c1, ColWidth)
            'JRSVBNET grd_TableDisplay.set_FixedAlignment(c1, GRID_ALIGNCENTER)
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
            If Val(Session("FileNumbr")(c1)) > 1 Then
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, RTrim(Session("VARN")(1, c1)))
            Else
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, RTrim(Session("VARN")(1, c1)))
            End If

            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 1)
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Session("GR1N")(c1))
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 2)
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Session("GR2N")(c1))
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 3)
            If Session("PercentOn") = "False" Then
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Session("GR3N")(c1))
            Else
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "% Change")
            End If

        Next c1

        grd_TableDisplay.Rows.Clear()
        grd_TableDisplay.Rows.Add(r1)
        grd_TableDisplay.Rows.Add(r2)
        grd_TableDisplay.Rows.Add(r3)
        grd_TableDisplay.Rows.Add(r4)
        grd_TableDisplay.Rows.Add(r5)
        grd_TableDisplay.Rows.Add(r6)

        '   Fill in left column of table
        '   ----------------------------
        Dim ry(Timepoints) As TableRow
        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, 0)
        'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, 0, HalfInch)
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 3)' Was 2
        'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "Year")
        'JRS 12/08/03
        If bLoadLongitudinalBFYH Then
            Y = iFirstYearBFYH
        Else
            Y = Session("FirstYear")
        End If
        rr = 4 ' was 3
        For cc = 1 To Timepoints
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, rr)
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Str(Y))
            Dim cel1 As New TableCell
            cel1.Text = Str(Y)
            cel1.BackColor = System.Drawing.Color.Gray
            Dim nry As New TableRow
            ry(cc) = nry
            ry(cc).Cells.Add(cel1)

            Y = Y + Session("Interval")
            'JRS 12/08/03
            If bLoadLongitudinalBFYH Then
                If Y > iFirstYearBFYH + DisYear - 1 Then Y = iFirstYearBFYH + DisYear - 1 Else If Y > Session("FirstYear") + DisYear - 1 Then Y = Session("FirstYear") + DisYear - 1
            End If
            rr = rr + 1
            'jrs 2005/12/11
            If Y > Session("DisplayLastYear") Then
                Y = Session("DisplayLastYear")
            End If
        Next cc

        '   Fill in data
        '   ------------

        Call FillData(Session("COUNTE"), ry)

        'If Session("PercentOn")$ = "True" Then
        'cmd_Percent.Caption = "&Whole"
        'Else
        'cmd_Percent.Caption = "&Percent"
        'End If

        'JRS 5/9/02
        'jrs 2024/11/25 Dim icol As Short
        Dim icol As Integer
        With grd_TableDisplay
            If .Rows.Count <= MaxNoRows Then
                For icol = 0 To .Rows(0).Cells.Count - 1
                    .Rows(.Rows.Count - 1).Cells(icol).Text = ""
                Next icol
            End If
        End With


        'mti Validation changes
        Dim sValidationVar As String
        'JRS 04/01/03 'JRSVBNET mnuValidation.Enabled = False
        Session("VCOUNTE") = 0
        For j = 1 To Session("COUNTE") - 1 'the last one need to be forecast
            sValidationVar = Trim(Session("VARN")(4, j))
            If Mid(sValidationVar, 1, 2) = "VH" Then 'asc("V") = 86
                sValidationVar = "VF" & Mid(sValidationVar, 3)
                For ii = j + 1 To Session("COUNTE")
                    If Trim(Session("VARN")(4, ii)) = sValidationVar Then
                        Session("VCOUNTE") = Session("VCOUNTE") + 1
                        VH(Session("VCOUNTE")) = j
                        VF(Session("VCOUNTE")) = ii
                        Exit For
                    End If
                Next ii
            End If
        Next j

        If Session("VCOUNTE") > 0 Then
            'JRS 04/01/03 'JRSVBNET mnuValidation.Enabled = True
            'JRSVBNET mnuValidation.Visible = True
            'JRSVBNET mnuValidation.Text = "&Validation"
            'later :bring back in the validation display, if left there
        End If
        'mti Validation changes

        'mti Validation changes July 28, 2002
        'JRSVBNET mnuGraph.Enabled = True
        'JRSVBNET mnuPercent.Enabled = True
        'mti Validation changes July 28, 2002


        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
        'JRSVBNET If frm_DisplayMenu.DefInstance.'JRSVBNET mnuValidation.Checked = True Then
        'JRSVBNET     mnuValidation_Click('JRSVBNET mnuValidation, New System.EventArgs)
        'JRSVBNET End If


        'if coming from Validation activate Validation First


        'JRS 1/8/03
        grd_TableDisplay.Visible = True

        'JRS 03/22/04
        Session("Timepoints") = Timepoints
        Session("TableDisYear") = DisYear

        'JAM 2012-04-03
        Call BuildTable()
    End Sub

    '=============================TablDisp.Frm=======================
    Private Sub PageEjectCheck(ByRef LinesPrinted As Short, ByRef PrintableLines As Short, ByRef PageNo As Short, ByRef PageCenter As Short)
        'JRSVBNET *** BEGIN ***
        '      Dim l As Short
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'If LinesPrinted < PrintableLines Then
        '	LinesPrinted = LinesPrinted + 1
        '	Exit Sub
        'End If

        ''   Print page number and go to new page
        ''   ------------------------------------

        'PageNo = PageNo + 1
        'For l = 1 To 3
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print() : Next l
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print(TAB(PageCenter), PageNo)
        ''UPGRADE_ISSUE: Printer method Printer.NewPage was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.NewPage()
        ''UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.EndDoc()
        'For l = 1 To TopPrintMargin
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print() : Next l
        'LinesPrinted = TopPrintMargin
        'JRSVBNET *** END ***
    End Sub

    '==========================frm_TableDisplay==================
    'JRS 11/04/02
    'CONVERSION_ISSUE: Event 'AxMSFlexGridLib.AxMSFlexGrid.DblClick' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.DblClick.html'.
    Private Sub grd_TableDisplay_DblClick()
        Dim Msg As Object
        Dim curr_col As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -



        'mti Validation changes July 28, 2002

        If False Then 'JRSVBNET Pego1.Visible = True Then
            Exit Sub
        End If

        'If Session("COUNTE") > 0 And Button = vbLeftButton Then
        If False Then 'JRSVBNET mnuValidation.Text = "&Validation" Then
            'mti 07 Feb 03 'add popup menu popup1
            'UPGRADE_ISSUE: Form method frm_TableDisplay.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'JRSVBNET Me.PopupMenu(mnuPop1)
            '        frm_NameDisp.Show vbModal
        Else
            'show popup menu



            'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET curr_col = 'JRSVBNET Support.DataGridManager.manager.GetCol(grd_TableDisplay)
            'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            ValCol = (curr_col + curr_col Mod 2) / 2

            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Session("VARN")(1, VH(ValCol)) & " vs. " & Session("VARN")(1, VF(ValCol))
            'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Msg = Msg + " [" + Session("GR1N")(VH(ValCol)) + "]" '+ "-" + Session("GR2N")$(VH(valcol)) + "-" + Session("GR3N")$(VH(valcol))
            '            msg = msg + "<br>" + Chr(10) + Chr(10) + "Delete this varaiable?"
            '            If MsgBox(msg, vbYesNo, "Variable Selected") = vbYes Then
            '                DeleteVar (curr_col)
            '                'JRS 9/14/01 COMPUTATIONS
            '                Dim dbV As Database
            '                Set dbV = DBEngine.OpenDatabase(PutFdrvRun$ + "IFSVAR.MDB")
            '                dbV.Execute "DELETE * FROM [Formulas] WHERE [Code] = " & Str(Session("COUNTE") + 1)
            '                dbV.Close
            '            End If


            'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET mnuTitle.Text = Msg

            'UPGRADE_ISSUE: Form method frm_TableDisplay.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'JRSVBNET Me.PopupMenu(mnuPop)
        End If

        'mti Validation changes July 28, 200

    End Sub



    Public Sub mnuPEBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Session("TableTitle") = txt_TableTitle

        On Error GoTo DefaultInterval

        'mti Validation changes

        Select Case cswmWebMenu.Groups("mnuGraph").Items("mnuPEBar").Caption
            Case "Bar"
                Session("GType") = "BAR"
                If Session("Interval") < 1 Then Session("Interval") = 1
            Case "% Error" '"% Error"
                Session("GType") = "Validation % Error"
            Case Else
                Exit Sub
        End Select

        'JAM 2016-03-06
        Session("fromTableDisp") = True

        'jrs 2016/03/30
        Session("bUseStartYrOrg") = Session("bUseStartYr")
        If Session("bUseStartYr") Then
            mnuUseStartYrDivby5_Click()
        End If

        '    frm_ProEssential.Show
        'JRS 07/30/02 GUIDEDUSE
        If Session("bGuidedUse") Then
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.ShowDialog().html'.
            'frm_ProEssential.DefInstance.ShowDialog() 
        Else
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
            'frm_ProEssential.DefInstance.Show() 
        End If

        Exit Sub

DefaultInterval:
        Session("Interval") = 1
        Resume Next

    End Sub

    'jrs 2016/03/28
    'jrs 2016/03/28
    Private Sub mnuPEPie_Click()

        'TableTitle = txt_TableTitle

        Session("GType") = "PIE"
        Session("PYear") = 1 'jrs 2009/01/12 CInt(Txt_PieYear.text) - FirstYear% + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("LastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("LastYear") - Session("FirstYear") + 1
        'jrs 2008/07/01
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        'JAM 2016-01-11
        Session("fromTableDisp") = True

        'jrs 2016/03/30
        Session("bUseStartYrOrg") = Session("bUseStartYr")
        If Session("bUseStartYr") Then
            mnuUseStartYrDivby5_Click()
        End If

        '    frm_ProEssential.Show
        'JRS 07/30/02 GUIDEDUSE
        If Session("bGuidedUse") Then
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.ShowDialog().html'.
            'frm_ProEssential.DefInstance.ShowDialog() 
        Else
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
            'frm_ProEssential.DefInstance.Show() 
        End If



    End Sub


    Public Sub mnuPELine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes

        Select Case cswmWebMenu.Groups("mnuGraph").Items("mnuPELine").Caption
            Case "Line"
                Session("GType") = "LINE"
            Case "Distance"
                Session("GType") = "Validation Distance"
            Case Else
                Exit Sub
        End Select


        'JAM 2016-03-06
        Session("fromTableDisp") = True

        'jrs 2016/03/30
        Session("bUseStartYrOrg") = Session("bUseStartYr")
        If Session("bUseStartYr") Then
            mnuUseStartYrDivby5_Click()
        End If

        '    frm_ProEssential.Show
        'JRS 07/30/02 GUIDEDUSE
        If Session("bGuidedUse") Then
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.ShowDialog().html'.
            'frm_ProEssential.DefInstance.ShowDialog() 
        Else

            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            If Session("LongVarTable") Then 'cdv 2022/07/01
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_LongPlot.aspx")
                'frm_ProEssential.DefInstance.Show() 
            Else
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
            End If
        End If

    End Sub

    Public Sub mnuRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRS 12/26/02
        RefreshTable()
    End Sub

    Public Sub mnuSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Value As Object
        Dim cc As Short
        Dim rr As Short
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim CDERR_CANCEL As Object
        'jrs 2012/01/26 Dim GraphOutputFileName As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'mti Validation changes August, 2002
        If False Then 'JRSVBNET Pego1.Visible = True Then
            SaveValidation(1)
            Exit Sub
        Else
            If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then
                SaveValidation(Session("VCOUNTE"))
                Exit Sub
            End If
        End If
        'mti Validation changes August, 2002


        '   Get graph file
        '   --------------

        'JRSVBNET GraphOutputFileName = "*.CSV"

        'jrs 2012/01/26 On Error GoTo SelGraphFileError_Line
        'JRSVBNET cmd_ScriptFile.DefaultExt = "CSV"
        'JRSVBNET cmd_ScriptFile.Filename = GraphOutputFileName
        'JRSVBNET cmd_ScriptFile.DialogTitle = "Save Table"
        'JRSVBNET cmd_ScriptFile.InitDir = PutFdrvRun2
        'JRSVBNET cmd_ScriptFile.Action = 1
        'jrs 2012/01/26 GraphOutputFileName = Session("PutFdrvRun") & "Save.csv" 'JRSVBNET cmd_ScriptFile.Filename
        '        On Error GoTo 0
        'jrs 2012/01/26 If VB.Left(GraphOutputFileName, 1) = "*" Then Exit Sub
        'jrs 2012/01/26 
        '            GoTo WriteGraphFile_Line

        'SelGraphFileError_Line:
        '            If Err.Number = CDERR_CANCEL Then

        '                LoadMessage("Cancel", Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")

        '            Else
        '                'jrs 11/09/04
        '                FileClose(2)

        '                LoadMessage(ErrorToString(Err.Number), Session("Message"))
        '                Support.FormManager.StateStore(Me)
        '                Support.FormManager.Show(Me, "frm_Message.aspx")
        '            End If

        'WriteGraphFile_Line:
        ''jrs 2005/02/16
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, GraphOutputFileName, OpenMode.Output)
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")


        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 10/22/03 MinYear% = 500
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 dim3 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always
        For rr = 0 To Rpoints
            'jrs 2008/03/12 include first year to save
            If rr >= 6 Then
                Do While iFirstYearSave > Val(grd_TableDisplay.Rows(rr).Cells(0).Text)
                    Y = Y + Session("Interval")
                    If Y > DisYear Then Y = DisYear
                    rr = rr + 1
                    If rr > Rpoints Then Exit For
                Loop
            End If

            For cc = 0 To Session("COUNTE")
                'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
                If rr >= 6 And cc >= 1 Then
                    Value = Session("PLOT")(Y, cc)
                    If cc = Session("COUNTE") Then
                        Y = Y + Session("Interval")
                        If Y > DisYear Then Y = DisYear
                    End If
                    'JRS 01/28/04
                    'JRS 01/29/04
                    'jrs 2007/05/15
                    If IsNumeric(Value) Then
                        ValtoShow.Value = ValueScale(CDbl(Value), Session("FourDecs")(cc), False)
                    Else
                        ValtoShow.Value = RSet(VB6.Format("", "############"), Len(ValtoShow.Value))
                    End If

                    If cc < Session("COUNTE") Then
                        'jrs 2012/01/26 Print(fNum, ValtoShow.Value & ",")
                        Context.Response.Write(ValtoShow.Value & ",")
                    Else
                        'jrs 2012/01/26 PrintLine(fNum, ValtoShow.Value)
                        Context.Response.Write(ValtoShow.Value)
                        Context.Response.Write(Environment.NewLine)
                    End If
                Else
                    If cc < Session("COUNTE") Then
                        'jrs 2005/03/18
                        'jrs 2012/01/26 Print(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                        Context.Response.Write(Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                    Else
                        'jrs 2005/03/18
                        'jrs 2012/01/26 PrintLine(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                        Context.Response.Write(Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                        Context.Response.Write(Environment.NewLine)
                    End If
                End If
            Next cc
        Next rr

        'jrs 2012/01/26 FileClose(fNum)

        'JRS 03/22/04
        Session("TableDisYear") = DisYear

        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/26
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************
        Exit Sub

        'jrs 2012/01/26
        'avoiding creating a file in the server, to avoid concurrency problems
        'I took the idea from this website
        'http://www.codeproject.com/Tips/53231/How-to-create-CSV-file-in-ASP-NET-from-database

    End Sub

    Public Sub mnuSavePanel_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Value As Object
        Dim cc As Short
        Dim rr As Short
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short


        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")


        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 10/22/03 MinYear% = 500
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        For ii = 1 To Session("COUNTE")
            If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
            If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        Next ii

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        'JAM 2010-11-30 Validating that doesn't exist duplicated variables
        Dim Duplicated As Boolean
        'If IsTranslated Then
        'FirstVariable = GR1AN(1)
        'Else
        Dim FirstVariable As String
        FirstVariable = Session("GR1N")(1)
        'End If
        FirstVariable = FirstVariable & Session("VARN")(1, 1) & Session("GR2N")(1) & Session("GR3N")(1) & Session("FirstYear")
        Duplicated = False
        For D = 2 To Session("COUNTE")
            'If IsTranslated Then
            '    If FirstVariable = GR1AN(D) & VARN(1, D) & GR2N(D) & GR3N(D) & FirstYear% Then
            '        Duplicated = True
            '        Exit For
            '    End If
            'Else
            If FirstVariable = Session("GR1N")(D) & Session("VARN")(1, D) & Session("GR2N")(D) & Session("GR3N")(D) & Session("FirstYear") Then
                Duplicated = True
                Exit For
            End If
            'End If

        Next D
        If Duplicated Then
            Dim Msg As String
            Msg = "This saving format can't be used with repeated columns"

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        'JAM 2010-11-08
        Dim vtemp() As String
        Dim vcontador() As Integer
        Dim vcountries() As String

        ReDim vtemp(MaxYears * MaxVariables)
        ReDim vcontador(MaxYears * MaxVariables)
        ReDim vcountries(MaxYears * MaxVariables)


        'JAM 2010-03-25
        Dim NumCols As Integer, j As Integer, v_exists As Boolean
        NumCols = 3

        j = 0
        vtemp(0) = Session("VARN")(1, 1) & Session("GR2N")(1) & Session("GR3N")(1)
        vcontador(0) = 1
        For h = 1 To Session("COUNTE")
            v_exists = True
            For i = 0 To j
                If vtemp(i) = (Session("VARN")(1, h) & Session("GR2N")(h) & Session("GR3N")(h)) Then
                    'JAM 2010-11-27 Testing dimmension
                    '  If h > 1 And (VARN(1, h) & GR2N(h) & GR3N(h)) <> (VARN(1, h - 1) & GR2N(h - 1) & GR3N(h - 1)) Then
                    v_exists = False
                    '    End If

                    Exit For

                End If
            Next i
            If v_exists Then
                j = j + 1
                vtemp(j) = Session("VARN")(1, h) & Session("GR2N")(h) & Session("GR3N")(h)
                vcontador(j) = 1
            End If


        Next h

        Dim NumVars As Integer, k As Integer, chosencountry As String
        NumVars = j
        NumCols = NumCols + j

        'JAM 2010-03-31 Defining rows
        k = 0
        'If IsTranslated Then
        '    vcountries(0) = GR1AN(1)
        'Else
        vcountries(0) = Session("GR1N")(1)
        'End If

        For l = 1 To Session("COUNTE")
            v_exists = True
            'If IsTranslated Then
            '    chosencountry = GR1AN(l)
            'Else
            chosencountry = Session("GR1N")(l)
            'End If
            For i = 0 To k
                If vcountries(i) = chosencountry Then
                    'JAM 2010-11-27 Testing dimmension
                    'If l > 1 And GR1N(l) <> GR1N(l - 1) Then
                    v_exists = False

                    Exit For
                    'End If
                End If
            Next i
            If v_exists Then
                k = k + 1
                vcountries(k) = chosencountry

            End If
        Next l

        Dim v_rows As Integer
        v_rows = k + 1

        'JAM 2010-11-08
        Dim Excel(0, 0) As Object
        ReDim Excel((v_rows * DisYear) + 1, NumCols + 1)
        'ReDim Excel(5000, NumCols + 1)

        'JAM 2010-03-30 Filling Countries and Year columns
        rr = 1
        For i = 1 To v_rows
            For j = 1 To DisYear
                rr = rr + 1
                'If IsTranslated Then
                '    Excel(rr, 1) = Replace(vcountries(i - 1), ",", ";")
                'Else
                Excel(rr, 1) = Replace(vcountries(i - 1), ",", ";")
                'End If
                'JAM 2011-08-01 Changing FirstYear to Firstyeartbl
                'Excel(rr, 2) = (FirstYear% - 1) + j
                Excel(rr, 2) = (Session("FirstYear") - 1) + j

            Next j
        Next i

        Dim Pattern As String, cols As Integer, a As Integer
        Pattern = Session("VARN")(1, 1) & Session("GR2N")(1) & Session("GR3N")(1)
        cols = 3
        'rr = 1
        For i = 1 To Session("COUNTE")

            For j = 1 To DisYear
                'rr = rr + 1
                For k = 0 To NumVars
                    If vtemp(k) = (Session("VARN")(1, i) & Session("GR2N")(i) & Session("GR3N")(i)) Then
                        Exit For
                    End If
                Next k
                vcontador(k) = vcontador(k) + 1
                If (vcontador(k) > (v_rows * DisYear) + 1) Or ((k + 3) > (NumCols + 1)) Then
                    a = 0
                End If
                If IsNumeric(Session("PLOT")(j, i)) Then
                    Excel(vcontador(k), k + 3) = Session("PLOT")(j, i)
                Else   'JAM 2010-11-09 It's necessary print null values

                    Excel(vcontador(k), k + 3) = ""
                End If
            Next j
        Next i



        '   Write Table to .CSV File
        '   ------------------------

        Y = 1
        Rpoints = 6 'jrs 2006/05/29 dim3 5 'Points for 4 header lines (0-3) plus first point
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always

        Context.Response.Write("Country,Year,")
        For j = 0 To NumbVars
            If j < NumbVars Then
                Context.Response.Write(vtemp(j) & ",")
            Else
                Context.Response.Write(vtemp(j))
                Context.Response.Write(Environment.NewLine)
            End If
        Next

        'JAM 2010-04-03 Optimizing .CSV File Generation
        Dim stringtowrite As String
        For cc = 2 To (v_rows * DisYear + 1)
            stringtowrite = ""
            For dd = 1 To NumCols
                If dd = 1 Then
                    stringtowrite = stringtowrite & Replace(CStr(Excel(cc, dd)), ",", ";")
                Else
                    stringtowrite = stringtowrite & "," & CStr(Excel(cc, dd))
                End If
            Next dd
            Context.Response.Write(stringtowrite)
            Context.Response.Write(Environment.NewLine)
        Next

        'jrs 2012/01/26 FileClose(fNum)

        'JRS 03/22/04
        Session("TableDisYear") = DisYear

        'JRS 07/05/04 SAVE FILE IN CLIENT *****
        'jrs 2012/01/26
        'Response.Clear()
        'Response.ContentType = "application/octet-stream"
        'Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        'Response.Flush()
        'Response.WriteFile(GraphOutputFileName)
        Response.End()
        '**************************************
        Exit Sub

        'jrs 2012/01/26
        'avoiding creating a file in the server, to avoid concurrency problems
        'I took the idea from this website
        'http://www.codeproject.com/Tips/53231/How-to-create-CSV-file-in-ASP-NET-from-database

    End Sub

    Public Sub mnuValidation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim AvgSum As Object
        Dim TStr As String
        Dim rr As Short
        Dim cc As Short
        Dim GRID_ALIGNCENTER As Object
        Dim c1 As Short
        Dim TotalColWidth As Short
        Dim MaxFormWidth As Short
        Dim MinFormWidth As Short
        Dim ColWidth As Short
        '------------------------------------------------------------


        'mti Validation changes: take whole procedure

        Dim AvgErr As Double
        Dim AvgCnt As Short

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor

        'mti Validation changes 12 August, 2002
        If False Then 'JRSVBNET mnuPercent.Text = "&Whole" Then
            mnuPercent_Click(Me, New System.EventArgs)
        End If
        'mti Validation changes 12 August, 2002


        'mti Validation changes July 28, 2002
        'JRSVBNET mnuGraph.Enabled = True
        'JRSVBNET mnuPercent.Enabled = True

        'JRSVBNET Pego1.Visible = False
        'JRSVBNET Pego2.Visible = False
        cmdContinue.Visible = False

        'mti Validation changes 09 August, 2002
        cmdNext.Visible = False
        cmdPrevious.Visible = False
        'mti Validation changes 09 August, 2002


        'mti Validation changes July 28, 2002

        ColWidth = Inch

        If False Then 'JRSVBNET mnuValidation.Text = "&Data" Then

            'mti Validation changes 12 August, 2002
            'JRSVBNET 'JRSVBNET mnuByPair.Visible = False
            'JRSVBNET 'JRSVBNET mnuSave.Text = "&Save"
            'mti Validation changes 12 August, 2002


            'mti Validation changes August, 2002
            grd_TableDisplay.ToolTip = ""


            ''JRSVBNET mnuValidation.Caption = "&Validation"

            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'JRSVBNET grd_TableDisplay.Rows = 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) - 1
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'JRSVBNET grd_TableDisplay.Cols = Session("COUNTE") + 1
            'CONVERSION_NOTE: Method 'AxMSFlexGridLib.AxMSFlexGrid.CtlRefresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.CtlRefresh().html'.
            'grd_TableDisplay.CtlRefresh() 

            'mti Validation changes July 28, 2002
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Table Display"
            '   Set width and left margin of form
            '   ---------------------------------

            ColWidth = Inch
            'UPGRADE_ISSUE: Form method frm_TableDisplay.TextWidth was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'JRSVBNET MinFormWidth = TextWidth(Session("TableTitle")) + Inch
            If MinFormWidth < 4 * Inch Then MinFormWidth = 4 * Inch
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET MaxFormWidth = VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width)
            If Session("COUNTE") > 19 Then 'too large causes overflow
                TotalColWidth = 20 * ColWidth
            Else
                TotalColWidth = (Session("COUNTE") + 1) * ColWidth
            End If

            'mti Validation changes 12 August, 2002
            'grd_TableDisplay.Width = TotalColWidth%
            'mti Validation changes 12 August, 2002

            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
            'JRSVBNET If TotalColWidth > MaxFormWidth Then Width = VB6.TwipsToPixelsX(MaxFormWidth)
            If TotalColWidth + HalfInch < MinFormWidth Then
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Width = VB6.TwipsToPixelsX(MinFormWidth)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Width = VB6.TwipsToPixelsX(Amin(TotalColWidth + HalfInch, MaxFormWidth))
            End If
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            If False Then 'JRSVBNET VB6.PixelsToTwipsX(Width) < VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) Then
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
                'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
            Else
                'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
                'Left = 0 
            End If

            'mti Validation changes 12 August, 2002
            'grid width and let
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
            If False Then 'JRSVBNET TotalColWidth < VB6.PixelsToTwipsX(ClientRectangle.Width) Then
                grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(TotalColWidth))
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
                'JRSVBNET grd_TableDisplay.Style.Item("LEFT") = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(ClientRectangle.Width) - VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value)) / 2)
            Else
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
                'JRSVBNET grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(ClientRectangle.Width)
                grd_TableDisplay.Style.Item("LEFT") = 0
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(Amin(VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value), VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - HalfInch)))
            'mti Validation changes 12 August, 2002

            'mti Validation changes July 28, 2002


            '   Fill in top of table
            '   --------------------

            For c1 = 1 To Session("COUNTE")
                'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, c1)
                'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, c1, ColWidth)
                'UPGRADE_WARNING: Couldn't resolve default property of object GRID_ALIGNCENTER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Method 'AxMSFlexGridLib.AxMSFlexGrid.set_FixedAlignment' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.html'.
                'JRSVBNET grd_TableDisplay.set_FixedAlignment(c1, GRID_ALIGNCENTER)
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
                If Val(Session("FileNumbr")(c1)) > 1 Then
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, RTrim(Session("VARN")(1, c1)))
                Else
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, RTrim(Session("VARN")(1, c1)))
                End If
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 1)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Session("GR1N")(c1))
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 2)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Session("GR2N")(c1))
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 3)
                'If Session("PercentOn")$ = "False" Then
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, Session("GR3N")(c1))
                'Else
                'grd_TableDisplay.Text = "% Change"
                'End If

            Next c1


            'replace values
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear + 1
                    'jrs 2009/01/30 Session("PLOT")(rr, cc) = SPLOT(rr, cc)
                    Session("PLOT")(rr, cc) = Session("Validation_PLOT")(rr, cc)
                Next rr
            Next cc

            'fill Grid
            'JRSVBNET Call FillData(Session("COUNTE"))
            'JRSVBNET mnuValidation.Text = "&Validation"
            'JRSVBNET mnuPercent.Enabled = True

            'JRSVBNET mnuPELine.Text = "&Line"
            'JRSVBNET mnuPEBar.Text = "&Bar"

            'mti Validation changes 19 August, 2002
            Call setFormHeight()

            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
            Exit Sub

        Else

            'mti Validation changes 19 August, 2002
            ColWidth = Inch + 600

            'mti Validation changes August, 2002
            grd_TableDisplay.ToolTip = "Click on pair to view detail"

            'mti Validation changes 19 August, 2002
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Historical Validation" & VInfo()
            'mti Validation changes 19 August, 2002

            'mti Validation changes 12 August, 2002
            'JRSVBNET mnuByPair.Text = "View By &Pair"
            'JRSVBNET mnuByPair.Visible = True
            'JRSVBNET mnuSave.Text = "&Export Validation"
            'mti Validation changes 12 August, 2002


            'JRSVBNET mnuPercent.Text = "&Percent"
            'JRSVBNET mnuPercent.Enabled = False

            'JRSVBNET mnuValidation.Text = "&Data"
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'JRSVBNET grd_TableDisplay.Rows = 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) + 1
            'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
            'JRSVBNET grd_TableDisplay.Cols = (VCOUNTE * 2) + 1
            'CONVERSION_NOTE: Method 'AxMSFlexGridLib.AxMSFlexGrid.CtlRefresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.CtlRefresh().html'.
            'grd_TableDisplay.CtlRefresh() 

            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, 0)
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) - 1)
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "Average:")

            'mti Validation changes July 28, 2002
            '   Set width and left margin of form
            '   ---------------------------------
            'UPGRADE_ISSUE: Form method frm_TableDisplay.TextWidth was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'JRSVBNET MinFormWidth = TextWidth(Session("TableTitle")) + Inch
            If MinFormWidth < 4 * Inch Then MinFormWidth = 4 * Inch
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET MaxFormWidth = VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width)
            If Session("VCOUNTE") > 10 Then 'too large causes overflow
                'mti Validation changes 19 August, 2002
                TotalColWidth = 15 * ColWidth 'was 20
            Else
                TotalColWidth = (Session("VCOUNTE") * 2 + 1) * ColWidth
            End If

            'mti Validation changes 12 August, 2002
            'grd_TableDisplay.Width = TotalColWidth%
            'mti Validation changes 12 August, 2002

            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
            'JRSVBNET If TotalColWidth > MaxFormWidth Then Width = VB6.TwipsToPixelsX(MaxFormWidth)
            If TotalColWidth + HalfInch < MinFormWidth Then
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Width = VB6.TwipsToPixelsX(MinFormWidth)
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
                'JRSVBNET Width = VB6.TwipsToPixelsX(Amin(TotalColWidth + HalfInch, MaxFormWidth))
            End If
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            If False Then 'JRSVBNET VB6.PixelsToTwipsX(Width) < VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) Then
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
                'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
            Else
                'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
                'Left = 0 
            End If
            'mti Validation changes July 28, 2002


            'mti Validation changes 12 August, 2002
            'grid width and let
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
            If False Then 'JRSVBNET TotalColWidth < VB6.PixelsToTwipsX(ClientRectangle.Width) Then
                grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(TotalColWidth))
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
                'JRSVBNET grd_TableDisplay.Style.Item("LEFT") = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(ClientRectangle.Width) - VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value)) / 2)
            Else
                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.ClientRectangle' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.ClientRectangle.html'.
                'JRSVBNET grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(ClientRectangle.Width)
                grd_TableDisplay.Style.Item("LEFT") = 0
            End If
            'UPGRADE_WARNING: Couldn't resolve default property of object Amin(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(Amin(VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value), VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - HalfInch)))
            'mti Validation changes 12 August, 2002


            'JRSVBNET mnuPELine.Text = "Distance"
            'JRSVBNET mnuPEBar.Text = "% Error"

        End If

        '   Fill in top of table for validation
        '   ---------------------------------
        'mti Validation changes 19 August, 2002
        For c1 = 1 To Session("VCOUNTE") * 2
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, c1)
            'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, c1, ColWidth)
            'UPGRADE_WARNING: Couldn't resolve default property of object GRID_ALIGNCENTER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Method 'AxMSFlexGridLib.AxMSFlexGrid.set_FixedAlignment' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.html'.
            'JRSVBNET grd_TableDisplay.set_FixedAlignment(c1, GRID_ALIGNCENTER)
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
            If c1 Mod 2 = 1 Then
                'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, c1, ColWidth)
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "Empirical  Variable:")
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 1)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "Forecasted Variable:")
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 2)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "Region/Group:")
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 3)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "        Distance")
            Else
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 0)
                'UPGRADE_WARNING: Couldn't resolve default property of object VH(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                TStr = shedbracket(RTrim(Session("VARN")(1, VH(c1 / 2))), "[")
                If Len(TStr) < 17 Then
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, New String(" ", 17 - Len(TStr)) & TStr)'strip off [-1]
                Else
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, TStr)
                End If

                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 1)
                'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                TStr = RTrim(Session("VARN")(1, VF(c1 / 2))) & IIf(Len(Session("GR2N")(VF(c1 / 2))) = 0, "", "-" & Session("GR2N")(VF(c1 / 2)))

                If Len(TStr) < 17 Then
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, New String(" ", 17 - Len(TStr)) & TStr)'strip off [-1]
                Else
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, TStr)
                End If

                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 2)
                'UPGRADE_WARNING: Couldn't resolve default property of object VF(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                TStr = Session("GR1N")(VF(c1 / 2))
                If Len(TStr) < 17 Then
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, New String(" ", 17 - Len(TStr)) & TStr)'strip off [-1]
                Else
                    'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, TStr)
                End If

                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 3)
                'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, "         % Error")
            End If
            'mti Validation changes 19 August, 2002
        Next c1

        'save data to sPLOT
        For cc = 1 To Session("COUNTE")
            For rr = 1 To DisYear + 1
                'jrs 2009/01/30 SPLOT(rr, cc) = Session("PLOT")(rr, cc)
                Session("Validation_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
            Next rr
        Next cc


        'jrs 2009/01/30 Session("SPLOT") = SPLOT

        For cc = 1 To Session("VCOUNTE") '* 2
            'UPGRADE_WARNING: Couldn't resolve default property of object AvgSum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            AvgSum = 0
            AvgCnt = 0
            For rr = DisYear + 1 To 1 Step -1
                'jrs 2009/01/30
                'If Not IsDBNull(SPLOT(rr, VH(cc))) And Not IsDBNull(SPLOT(rr, VH(cc))) And Not IsNothing(SPLOT(rr, VF(cc))) And Not IsNothing(SPLOT(rr, VF(cc))) Then
                '    'forecast - history
                '    Session("PLOT")(rr, cc * 2 - 1) = SPLOT(rr, VF(cc)) - SPLOT(rr, VH(cc))
                '    If SPLOT(rr, VH(cc)) <> 0 Then
                '        'Abs((forecast-history)/history)*100
                '        'max function(.01) 'test for smaller value
                '        'mti Validation changes 09 August, 2002
                '        Session("PLOT")(rr, cc * 2) = VB6.Format(System.Math.Abs((SPLOT(rr, VF(cc)) - SPLOT(rr, VH(cc))) / AMAX(SPLOT(rr, VH(cc)), 0.0001)) * 100, "standard")
                '        'mti Validation changes 09 August, 2002
                '        '1 should be history
                '        'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                '        AvgErr = AvgErr + Session("PLOT")(rr, cc * 2)
                '        AvgCnt = AvgCnt + 1
                '    Else
                '        Session("PLOT")(rr, cc * 2) = System.DBNull.Value '99999999
                '    End If
                'Else
                '    Session("PLOT")(rr, cc * 2 - 1) = System.DBNull.Value '99999999  '????
                '    Session("PLOT")(rr, cc * 2) = System.DBNull.Value '99999999
                'End If
                If Not IsDBNull(Session("Validation_PLOT")(rr, VH(cc))) And Not IsDBNull(Session("Validation_PLOT")(rr, VH(cc))) And Not IsNothing(Session("Validation_PLOT")(rr, VF(cc))) And Not IsNothing(Session("Validation_PLOT")(rr, VF(cc))) Then
                    'forecast - history
                    Session("PLOT")(rr, cc * 2 - 1) = Session("Validation_PLOT")(rr, VF(cc)) - Session("Validation_PLOT")(rr, VH(cc))
                    If Session("Validation_PLOT")(rr, VH(cc)) <> 0 Then
                        'Abs((forecast-history)/history)*100
                        'max function(.01) 'test for smaller value
                        'mti Validation changes 09 August, 2002
                        Session("PLOT")(rr, cc * 2) = VB6.Format(System.Math.Abs((Session("Validation_PLOT")(rr, VF(cc)) - Session("Validation_PLOT")(rr, VH(cc))) / AMAX(Session("Validation_PLOT")(rr, VH(cc)), 0.0001)) * 100, "standard")
                        'mti Validation changes 09 August, 2002
                        '1 should be history
                        'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        AvgErr = AvgErr + Session("PLOT")(rr, cc * 2)
                        AvgCnt = AvgCnt + 1
                    Else
                        Session("PLOT")(rr, cc * 2) = System.DBNull.Value '99999999
                    End If
                Else
                    Session("PLOT")(rr, cc * 2 - 1) = System.DBNull.Value '99999999  '????
                    Session("PLOT")(rr, cc * 2) = System.DBNull.Value '99999999
                End If
            Next rr
            If AvgCnt > 0 Then
                AvgErr = AvgErr / AvgCnt
            End If
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc * 2)
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) - 1)
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, VB6.Format(AvgErr, "        ###.00"))
        Next cc


        'JRSVBNET Call FillData(VCOUNTE * 2)

        'mti Validation changes 19 August, 2002
        Call setFormHeight()


    End Sub

    Public Sub mnuViewPair_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ButSpace As Object
        Dim TitleBarSpace As Object
        Dim GRID_ALIGNCENTER As Object
        Dim TmpVal As Object
        Dim i2 As Object
        Dim RowCnt As Object
        Dim rr As Short
        Dim curr_col As Object
        Dim ColWidth As Short
        '-----------------------------------------------------------
        'mti Validation changes July 28, 2002 - Take Whole Procedure
        Dim pairstart As Short

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor

        'mti Validation changes 09 August, 2002
        grd_TableDisplay.ToolTip = ""
        'mti Validation changes 09 August, 2002

        'JRSVBNET mnuGraph.Enabled = False
        'JRSVBNET mnuPercent.Enabled = False

        'mti Validation changes 19 August, 2002
        ColWidth = Inch + 600

        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET curr_col = 'JRSVBNET Support.DataGridManager.manager.GetCol(grd_TableDisplay)'+ 1
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ValCol = (curr_col + curr_col Mod 2) / 2

        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        pairstart = ValCol * 2 - 1

        For rr = 1 To DisYear + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(rr, pairstart). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(rr, 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("PLOT")(rr, 1) = Session("PLOT")(rr, pairstart)
        Next rr

        For rr = 1 To DisYear + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(rr, pairstart + 1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(rr, 2). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("PLOT")(rr, 2) = Session("PLOT")(rr, pairstart + 1)
        Next rr



        '            grd_TableDisplay.Col = 0
        '            grd_TableDisplay.Row = grd_TableDisplay.Rows '- 1
        '            grd_TableDisplay.Text = "Average:"
        'UPGRADE_WARNING: Couldn't resolve default property of object RowCnt. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET RowCnt = 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay)

        'UPGRADE_WARNING: Couldn't resolve default property of object RowCnt. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For i2 = 0 To RowCnt - 1
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, pairstart)
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, i2)'- 1
            'UPGRADE_WARNING: Couldn't resolve default property of object TmpVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET TmpVal = 'JRSVBNET Support.DataGridManager.GetText(grd_TableDisplay)'= "Average:"
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object TmpVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, TmpVal)
        Next i2

        'UPGRADE_WARNING: Couldn't resolve default property of object RowCnt. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For i2 = 0 To RowCnt - 1
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, pairstart + 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, i2)'- 1
            'UPGRADE_WARNING: Couldn't resolve default property of object TmpVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET TmpVal = 'JRSVBNET Support.DataGridManager.GetText(grd_TableDisplay)'= "Average:"
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, 2)
            'UPGRADE_WARNING: Couldn't resolve default property of object TmpVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.SetText(grd_TableDisplay, TmpVal)
        Next i2


        'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
        'JRSVBNET grd_TableDisplay.Cols = 3
        'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, 1, ColWidth)
        'JRSVBNET Support.DataGridManager.SetColWidth(grd_TableDisplay, 1, ColWidth)
        'UPGRADE_WARNING: Couldn't resolve default property of object GRID_ALIGNCENTER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Method 'AxMSFlexGridLib.AxMSFlexGrid.set_FixedAlignment' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.html'.
        'JRSVBNET grd_TableDisplay.set_FixedAlignment(1, GRID_ALIGNCENTER)
        'UPGRADE_WARNING: Couldn't resolve default property of object GRID_ALIGNCENTER. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Method 'AxMSFlexGridLib.AxMSFlexGrid.set_FixedAlignment' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.html'.
        'JRSVBNET grd_TableDisplay.set_FixedAlignment(1, GRID_ALIGNCENTER)

        grd_TableDisplay.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(3 * ColWidth))
        'CONVERSION_NOTE: Method 'AxMSFlexGridLib.AxMSFlexGrid.CtlRefresh' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.AxMSFlexGridLib.AxMSFlexGrid.CtlRefresh().html'.
        'grd_TableDisplay.CtlRefresh() 

        'mti Validation changes 19 August, 2002
        'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TitleBarSpace = 425
        'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ButSpace = 200
        'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
        'JRSVBNET Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - ButSpace - TitleBarSpace)
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Top.html'.
        'Top = 0 
        'mti Validation changes 19 August, 2002


        'JRSVBNET 'JRSVBNET Pego1.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(grd_TableDisplay.Style.Item("LEFT")).Value) + VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value) + 20)
        'JRSVBNET 'JRSVBNET Pego1.Style.Item("TOP") = System.Web.UI.WebControls.Unit.Parse(grd_TableDisplay.Style.Item("TOP")).Value
        'JRSVBNET 'JRSVBNET Pego1.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(6500))
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
        'JRSVBNET 'JRSVBNET Pego1.Height = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) / 3)) ' 4400
        'JRSVBNET 'JRSVBNET Pego1.Enabled = True
        'JRSVBNET 'JRSVBNET Pego1.Visible = True

        'JRSVBNET 'JRSVBNET Pego2.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse(grd_TableDisplay.Style.Item("LEFT")).Value) + VB6.PixelsToTwipsX(grd_TableDisplay.Width.Value) + 20)
        'JRSVBNET 'JRSVBNET Pego2.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Web.UI.WebControls.Unit.Parse('JRSVBNET Pego1.Style.Item("TOP")).value) + VB6.PixelsToTwipsY('JRSVBNET Pego1.Height.Value) + 100)
        'JRSVBNET 'JRSVBNET Pego2.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(6500))
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
        'JRSVBNET 'JRSVBNET Pego2.Height = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) / 3)) ' 4400
        'JRSVBNET 'JRSVBNET Pego2.Enabled = True
        'JRSVBNET 'JRSVBNET Pego2.Visible = True

        'mti Validation changes 19 August, 2002

        'JRSVBNET cmdContinue.Width = System.Web.UI.WebControls.Unit.Pixel('JRSVBNET Pego2.Width.Value)
        'JRSVBNET cmdContinue.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse('JRSVBNET Pego2.Style.Item("LEFT")).Value) + 100)
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Top.html'.
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
        'JRSVBNET cmdContinue.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Top) + VB6.PixelsToTwipsY(Me.Height) - Inch - VB6.PixelsToTwipsY(cmdContinue.Height.Value)) '+ 100 'grd_TableDisplay.top + grd_TableDisplay.Height - cmdContinue.Height
        cmdContinue.Visible = True

        'JRSVBNET cmdPrevious.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX((VB6.PixelsToTwipsX('JRSVBNET Pego2.Width.Value)) / 3))
        'JRSVBNET cmdPrevious.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse('JRSVBNET Pego2.Style.Item("LEFT")).Value) + 100)
        cmdPrevious.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Web.UI.WebControls.Unit.Parse(cmdContinue.Style.Item("TOP")).Value) - VB6.PixelsToTwipsY(cmdPrevious.Height.Value) - 100) ' + grd_TableDisplay.Height - cmdContinue.Height - 50 - cmdPrevious.Height
        cmdPrevious.Visible = True

        'JRSVBNET cmdNext.Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX((VB6.PixelsToTwipsX('JRSVBNET Pego2.Width.Value)) / 3))
        'JRSVBNET cmdNext.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse('JRSVBNET Pego2.Style.Item("LEFT")).Value) + VB6.PixelsToTwipsX('JRSVBNET Pego2.Width.Value) - VB6.PixelsToTwipsX('JRSVBNET Pego2.Width.Value) / 3 + 100)
        cmdNext.Style.Item("TOP") = System.Web.UI.WebControls.Unit.Parse(cmdPrevious.Style.Item("TOP")).Value 'grd_TableDisplay.top + grd_TableDisplay.Height - cmdContinue.Height - 50 - cmdPrevious.Height
        cmdNext.Visible = True

        'JRSVBNET cmdContinue.Width = System.Web.UI.WebControls.Unit.Pixel('JRSVBNET Pego2.Width.Value)
        'JRSVBNET cmdContinue.Style.Item("LEFT") = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Web.UI.WebControls.Unit.Parse('JRSVBNET Pego2.Style.Item("LEFT")).Value) + 100)
        cmdContinue.Style.Item("TOP") = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Web.UI.WebControls.Unit.Parse(cmdNext.Style.Item("TOP")).Value) + VB6.PixelsToTwipsY(cmdNext.Height.Value) + 100) 'grd_TableDisplay.top + grd_TableDisplay.Height - cmdContinue.Height
        cmdContinue.Visible = True
        'mti Validation changes 19 August, 2002

        'form width
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
        'JRSVBNET Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(grd_TableDisplay.Width) + VB6.PixelsToTwipsX('JRSVBNET Pego1.Width) + 1000)
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Width.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        If False Then 'JRSVBNET VB6.PixelsToTwipsX(Width) < VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) Then
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
            'JRSVBNET Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(Width)) / 2)
        Else
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Left.html'.
            'Left = 0 
        End If

        'mti Validation changes August, 2002
        Call DrawValidationGraphs(1, 2)



        'msg = Session("VARN")$(1, VH(ValCol)) + " vs. " + Session("VARN")$(1, VF(ValCol))


        'mti Validation changes 12 August, 2002
        'JRSVBNET 'JRSVBNET mnuByPair.Text = "View &First Pair"
        'JRSVBNET 'JRSVBNET mnuSave.Text = "&Save"

        'mti Validation changes 19 August, 2002
        '    Msg = Session("VARN")$(1, VH(ValCol)) + " vs. " + Session("VARN")$(1, VF(ValCol))
        '    Msg = Msg + " [" + Session("GR1N")$(VH(ValCol)) + "]" '+ "-" + Session("GR2N")$(VH(valcol)) + "-" + Session("GR3N")$(VH(valcol))
        '    Caption = Caption + " - " + Msg
        WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Historical Validation" & VInfo()
        'mti Validation changes 19 August, 2002

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub
    '==========================frm_TableDisplay==================
    Private Sub SavePair(ByRef FileNo As Short, ByRef TblName As Object, ByRef DistanceCol As Short, ByRef PcntErrorCol As Short, ByRef Pairno As Short, ByRef TotalPairs As Short)
        Dim PETitle As String
        Dim PEGraphFile As String
        Dim Value As Object
        Dim cc As Short
        Dim rr As Short
        Dim Resid As Object
        Dim Rpoints As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        Dim LargeError As Object
        'mti Validation changes 19 August, 2002 : added another parameter
        '------------------------------------------------------------

        'BuildHTML_TableCell(Text$, Rowspan%, Width%, Height%, Align$, valign$, fontcolor$, fontsize%, Bold$, UnderLine$) As String

        'mti Validation changes 19 August, 2002
        On Error GoTo WriteError
        'UPGRADE_WARNING: Couldn't resolve default property of object LargeError. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        LargeError = 25 '% Errors larger than this will be bold and underlined in html file
        'mti Validation changes 19 August, 2002

        'Write Table Headers
        PrintLine(FileNo, "<p>&nbsp;</p>")
        'UPGRADE_WARNING: Couldn't resolve default property of object TblName. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PrintLine(FileNo, "<p>" + TblName + "<a name=" + Quote + TblName + Quote + "></a>") '</p>"

        'mti Validation changes 19 August, 2002
        If TotalPairs > 1 Then
            PrintLine(FileNo, "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=" & Quote & "#" & "Top" & Quote & ">" & "Top" & "</a></p>")
        End If
        'mti Validation changes 19 August, 2002

        'Begin Table adjust height here
        PrintLine(FileNo, "<table width=" & Quote & "95%" & Quote & " border=" & Quote & "1" & " height=" & Quote & "142" & Quote & ">") 'adjust height here

        'mti Validation changes 19 August, 2002
        'Start Header Row
        PrintLine(FileNo, "<tr>")

        PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:="Year", Width_Renamed:=10, Height_Renamed:=32, Bold:="Yes"))

        'BuildHTML_TableCell(Text$, Rowspan%, Width%, Height%, Align$, valign$, fontcolor$, fontsize%, Bold$, UnderLine$) As String
        PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:="Distance", Width_Renamed:=10, Height_Renamed:=32, Bold:="Yes"))

        PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:="% Error", Width_Renamed:=10, Height_Renamed:=32, Bold:="Yes"))

        'column for graphic header
        PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:="Graph", Width_Renamed:=70, Height_Renamed:=32, Bold:="Yes"))

        PrintLine(FileNo, "</tr>")
        'End Header Row
        'mti Validation changes 19 August, 2002

        '   Determine Length of Table
        '   -------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        'For ii% = 1 To Session("COUNTE")
        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VH(ValCol). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ii = VH(ValCol)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        'Next ii%

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If


        Y = 1
        Rpoints = 5 'Points for 4 header lines (0-3) plus first point
        'UPGRADE_WARNING: Couldn't resolve default property of object Resid. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Resid = (DisYear - 1) Mod Session("Interval")
        Rpoints = Rpoints + (DisYear - 1) \ Session("Interval") 'Truncating Division
        'UPGRADE_WARNING: Couldn't resolve default property of object Resid. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Resid > 0 Then Rpoints = Rpoints + 1 'Show Final Year Always
        For rr = 4 To Rpoints '0
            PrintLine(FileNo, "<tr>")
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, rr)

            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, 0)'Years
            'Print #FileNo, "<td width=" + Quote + "10%" + Quote + ">" + grd_TableDisplay.Text + "</td>"

            'mti Validation changes 19 August, 2002
            'BuildHTML_TableCell(Text$, Rowspan%, Width%, Height%, Align$, valign$, fontcolor$, fontsize%, Bold$, UnderLine$) As String
            'JRSVBNET PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:='JRSVBNET Support.DataGridManager.GetText(grd_TableDisplay), Width_Renamed:=10, Bold:="Yes"))


            'For cc% = 1 To 2  'Session("COUNTE")
            cc = DistanceCol
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(Y, cc). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Value = Session("PLOT")(Y, cc)

            'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
            'JRS 01/28/04
            'jrs 2009/03/04
            If IsNumeric(Value) Then
                ValtoShow.Value = ValueScale(Value, Session("FourDecs")(cc), True)
            Else
                ValtoShow.Value = RSet(VB6.Format("", "############"), Len(ValtoShow.Value))
            End If

            'Print #FileNo, "<td width=" + Quote + "10%" + Quote + ">" + ValtoShow + "</td>"

            PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:=ValtoShow.Value, Width_Renamed:=10))

            cc = PcntErrorCol
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(Y, cc). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Value. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Value = Session("PLOT")(Y, cc)



            'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
            'JRS 01/28/04
            'JRS 01/29/04
            'jrs 2009/03/04 ValueScale(Value)
            If IsNumeric(Value) Then
                ValtoShow.Value = ValueScale(Value, Session("FourDecs")(cc), True)
            Else
                ValtoShow.Value = RSet(VB6.Format("", "############"), Len(ValtoShow.Value))
            End If

            'mti Validation changes 19 August, 2002
            'UPGRADE_WARNING: Couldn't resolve default property of object LargeError. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Val(ValtoShow.Value) > Val(LargeError) Then
                'Print #FileNo, "<td width=" + Quote + "10%" + Quote + ">" + "<B><U>" + ValtoShow + "</U></B>" + "</td>"
                PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:=ValtoShow.Value, Width_Renamed:=10, UnderLine:="YES"))
            Else
                'Print #FileNo, "<td width=" + Quote + "10%" + Quote + ">" + ValtoShow + "</td>"
                PrintLine(FileNo, BuildHTML_TableCell(Text_Renamed:=ValtoShow.Value, Width_Renamed:=10))
            End If
            'mti Validation changes 19 August, 2002

            'Next cc%
            If Y = 1 Then 'Or DisYear% - Y% = Y% Then  'Put Graph
                'Span half of the total rows
                PrintLine(FileNo, "<td rowspan=" & Quote & Trim(Str(Int(DisYear / Session("Interval")))) & Quote & " align=" & Quote & "center" & Quote & " valign=" & Quote & "middle" & Quote & ">")
                'Check height and width
                'If Y% = 1 Then

                'mti Validation changes 19 August, 2002
                PEGraphFile = Trim(Str(Pairno)) & "_Data.jpg"
                PETitle = "Data"
                PrintLine(FileNo, "<p>&nbsp;</p>")
                PrintLine(FileNo, "<p><img src=" & Quote & PEGraphFile & Quote & " width=" & Quote & "500" & Quote & " height=" & Quote & "300" & Quote & "></p>")
                PrintLine(FileNo, "<p>&nbsp;</p>")
                PrintLine(FileNo, "<p>" & PETitle & "</p>")
                PrintLine(FileNo, "<p>&nbsp;</p>")

                PEGraphFile = Trim(Str(Pairno)) & "_Distance.jpg"
                PETitle = "Distance"
                PrintLine(FileNo, "<p>&nbsp;</p>")
                PrintLine(FileNo, "<p><img src=" & Quote & PEGraphFile & Quote & " width=" & Quote & "500" & Quote & " height=" & Quote & "300" & Quote & "></p>")
                PrintLine(FileNo, "<p>&nbsp;</p>")
                PrintLine(FileNo, "<p>" & PETitle & "</p>")
                PrintLine(FileNo, "<p>&nbsp;</p>")

                PEGraphFile = Trim(Str(Pairno)) & "_PcntErr.jpg"
                PETitle = "Percentage Error"
                PrintLine(FileNo, "<p>&nbsp;</p>")
                PrintLine(FileNo, "<p><img src=" & Quote & PEGraphFile & Quote & " width=" & Quote & "500" & Quote & " height=" & Quote & "300" & Quote & "></p>")
                PrintLine(FileNo, "<p>&nbsp;</p>")
                PrintLine(FileNo, "<p>" & PETitle & "</p>")

                'mti Validation changes 19 August, 2002
                'column already set
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, Rpoints + 2)
                'JRSVBNET PrintLine(FileNo, "<p>Average of  %  Error : " & Trim('JRSVBNET Support.DataGridManager.GetText(grd_TableDisplay)) & "</p>")
                'mti Validation changes 19 August, 2002

                PrintLine(FileNo, "</td>")
                'mti Validation changes 19 August, 2002

            End If
            Y = Y + Session("Interval")
            If Y > DisYear Then Y = DisYear
            PrintLine(FileNo, "</tr>")


        Next rr

        'close HTML Table
        PrintLine(FileNo, "</table>")
        PrintLine(FileNo, "<p>&nbsp;</p>")
        PrintLine(FileNo, "<p>&nbsp;</p>")
        PrintLine(FileNo, "<p>&nbsp;</p>")

        'Close #FileNo
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
        Exit Sub

        'mti Validation changes 19 August, 2002
WriteError:
        LoadMessage("IFs encountered some error while exporting validation.", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub


    '==========================frm_TableDisplay==================
    Private Sub DrawValidationGraphs(ByRef DistanceCol As Short, ByRef PcntErrorCol As Short)
        Dim rr As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        '------------------------------------------------------------

        'mti Validation changes 19 August, 2002
        Dim gInterval As Short
        gInterval = 1
        'after adding these lines replace all interval% with gInterVal% in this proc
        'mti Validation changes 19 August, 2002


        'mti Validation changes August, 2002 - Take Whole Procedure
        'Initialize 'JRSVBNET Pego1

        'JRSVBNET Pego1.MainTitle = "Distance"
        'JRSVBNET Pego1.SubTitle = "" 'needed for printing oer menu caption
        'JRSVBNET Pego1.MainTitleBold = True
        'JRSVBNET Pego1.DeskColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255)) 'Bright White
        'JRSVBNET Pego1.GraphBackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255)) 'Bright White
        'JRSVBNET Pego1.DataShadows = PEDS_3D

        'JRSVBNET Pego1.FocalRect = False '??

        'JRSVBNET Pego1.GridLineControl = PEGLC_NONE

        'for saving to jpeg file
        'JRSVBNET Pego1.AllowJpegOutput = True


        'JRSVBNET Pego1.Subsets = 1 'pCOUNTE

        'JRSVBNET Pego1.YAxisLabel = ""
        'JRSVBNET Pego1.XAxisLabel = "Year"

        'JRSVBNET Pego1.PlottingMethod = GPM_LINE

        'JRSVBNET Pego1.NullDataValue = -9999


        'Initialize 'JRSVBNET Pego2

        'JRSVBNET Pego2.MainTitle = "% Error"
        'JRSVBNET Pego2.SubTitle = ""
        'JRSVBNET Pego2.MainTitleBold = True
        'JRSVBNET Pego2.DeskColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255)) 'Bright White
        'JRSVBNET Pego2.GraphBackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255)) 'Bright White
        'JRSVBNET Pego2.DataShadows = PEDS_3D

        'JRSVBNET Pego2.FocalRect = False '??

        'JRSVBNET Pego2.GridLineControl = PEGLC_NONE

        'for saving to jpeg file
        'JRSVBNET Pego2.AllowJpegOutput = True


        'JRSVBNET Pego2.Subsets = 1 'pCOUNTE

        'JRSVBNET Pego2.YAxisLabel = ""
        'JRSVBNET Pego2.XAxisLabel = "Year"

        'JRSVBNET Pego2.PlottingMethod = GPM_LINE

        'JRSVBNET Pego2.NullDataValue = -9999


        '   Set number of points, interval
        '   ------------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        'For ii% = 1 To pCOUNTE
        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VH(ValCol). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ii = VH(ValCol)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        'Next ii%



        If gInterval > MaxYear Then
            gInterval = MaxYear - 1
        End If

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        '        If DisYear% * pCOUNTE > 3500 Then
        '                DisYear% = 3500 / pCOUNTE
        '        End If
        'JRS 11/03/03
        If gInterval > 1 Or DisYear <> Session("DisplayLastYear") - Session("FirstYear") + 1 Then
            'JRSVBNET Pego1.Points = (DisYear / gInterval) + 1 'UBound(Session("PLOT"), 1) 'check
            'JRSVBNET Pego2.Points = (DisYear / gInterval) + 1
        Else
            'JRSVBNET Pego1.Points = DisYear / gInterval
            'JRSVBNET Pego2.Points = DisYear / gInterval
        End If


        For Y = Session("FirstYear") To Session("FirstYear") + DisYear 'Step 5
            'JRSVBNET Pego1.set_PointLabels(Y - Session("FirstYear"), LTrim(Str(Y)))
            'JRSVBNET Pego2.set_PointLabels(Y - Session("FirstYear"), LTrim(Str(Y)))
        Next Y

        'JRSVBNET Pego1.set_PointLabels('JRSVBNET Pego1.Points - 1, LTrim(Str(Session("FirstYear") + DisYear)))
        'JRSVBNET Pego2.set_PointLabels('JRSVBNET Pego1.Points - 1, LTrim(Str(Session("FirstYear") + DisYear)))

        'mti Validation changes 19 August, 2002
        'Timepoints% = Session("LastYear")% - Session("FirstYear")% + 1
        'mti Validation changes 19 August, 2002

        '   Fill in data
        '   ------------
        For rr = 1 To DisYear Step gInterval 'MSChart1.RowCount
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            If Not IsDBNull(Session("PLOT")(rr, DistanceCol)) Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'JRSVBNET Pego1.set_YData(0, (rr - 1) / gInterval, VB6.Format(Session("PLOT")(rr, DistanceCol), "0.000"))
            Else
                'JRSVBNET Pego1.set_YData(0, (rr - 1) / gInterval, -9999)  'JRS 5/10/02 0
            End If
        Next rr

        For rr = 1 To DisYear Step gInterval 'MSChart1.RowCount
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            If Not IsDBNull(Session("PLOT")(rr, PcntErrorCol)) Then
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'JRSVBNET Pego2.set_YData(0, (rr - 1) / gInterval, VB6.Format(Session("PLOT")(rr, PcntErrorCol), "0.000"))
            Else
                'JRSVBNET Pego2.set_YData(0, (rr - 1) / gInterval, -9999)  'JRS 5/10/02 0
            End If
        Next rr


        'Scalegraph

        'JRSVBNET Pego1.PEactions = 0
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
    End Sub

    '==========================frm_TableDisplay==================
    Private Sub DrawDataGraph(ByRef HistCol As Object, ByRef ForecastCol As Object)
        Dim ForecastColCol As Object
        Dim rr As Short
        Dim Y As Short
        Dim ii As Short
        Dim MinYear As Short
        Dim MaxYear As Short
        '------------------------------------------------------------

        'mti Validation changes 19 August, 2002
        Dim gInterval As Short
        gInterval = 1
        'after adding these lines replace all gInterval% with gInterVal% in this proc
        'mti Validation changes 19 August, 2002


        ''mti Validation changes 09 August, 2002 -Take Whole Procedure
        'Initialize 'JRSVBNET Pego1
        ''JRSVBNET Pego1.SubsetLabels(ii% - 1) = Legend$

        'JRSVBNET Pego1.MainTitle = "Data"
        'JRSVBNET Pego1.SubTitle = "" 'needed for printing oer menu caption
        'JRSVBNET Pego1.MainTitleBold = True
        'JRSVBNET Pego1.DeskColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255)) 'Bright White
        'JRSVBNET Pego1.GraphBackColor = System.Drawing.ColorTranslator.FromOle(RGB(255, 255, 255)) 'Bright White
        'JRSVBNET Pego1.DataShadows = PEDS_3D

        'JRSVBNET Pego1.FocalRect = False '??

        'JRSVBNET Pego1.GridLineControl = PEGLC_NONE

        'for saving to jpeg file
        'JRSVBNET Pego1.AllowJpegOutput = True


        'JRSVBNET Pego1.Subsets = 2 'pCOUNTE

        'UPGRADE_WARNING: Couldn't resolve default property of object HistCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET Pego1.set_SubsetLabels(0, Session("VARN")(1, HistCol) & "-" & Session("GR1N")(HistCol))
        'UPGRADE_WARNING: Couldn't resolve default property of object ForecastCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET Pego1.set_SubsetLabels(1, CDbl(Session("VARN")(1, ForecastCol) & "-" & Session("GR1N")(ForecastCol)) + IIf(Len(Trim(Session("GR2N")(ForecastCol))) = 0, "", "-" & Trim(Session("GR2N")(ForecastCol))))


        'JRSVBNET Pego1.YAxisLabel = ""
        'JRSVBNET Pego1.XAxisLabel = "Year"

        'JRSVBNET Pego1.PlottingMethod = GPM_LINE

        'JRSVBNET Pego1.NullDataValue = -9999



        '   Set number of points, interval
        '   ------------------------------

        MaxYear = 1
        'JRS 11/03/03
        MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
        'For ii% = 1 To pCOUNTE
        'UPGRADE_WARNING: Couldn't resolve default property of object ValCol. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VH(ValCol). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ii = VH(ValCol)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("FinYear")(ii). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
        'Next ii%



        If gInterval > MaxYear Then
            gInterval = MaxYear - 1
        End If

        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            'jrs 2007/11/30
            If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                DisYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            Else
                DisYear = MaxYear
            End If
        End If

        '        If DisYear% * pCOUNTE > 3500 Then
        '                DisYear% = 3500 / pCOUNTE
        '        End If
        'JRS 11/03/03
        If gInterval > 1 Or DisYear <> Session("DisplayLastYear") - Session("FirstYear") + 1 Then
            'JRSVBNET Pego1.Points = (DisYear / gInterval) + 1 'UBound(Session("PLOT"), 1) 'check
        Else
            'JRSVBNET Pego1.Points = DisYear / gInterval
        End If


        For Y = Session("FirstYear") To Session("FirstYear") + DisYear 'Step 5
            'JRSVBNET Pego1.set_PointLabels(Y - Session("FirstYear"), LTrim(Str(Y)))
        Next Y

        'JRSVBNET Pego1.set_PointLabels('JRSVBNET Pego1.Points - 1, LTrim(Str(Session("FirstYear") + DisYear)))

        'mti Validation changes 19 August, 2002
        'Timepoints% = Session("LastYear")% - Session("FirstYear")% + 1
        'mti Validation changes 19 August, 2002


        '   Fill in data
        '   ------------
        For rr = 1 To DisYear Step gInterval 'MSChart1.RowCount
            'jrs 2009/01/30
            If Not IsDBNull(Session("Validation_PLOT")(rr, HistCol)) Then
                'JRSVBNET Pego1.set_YData(0, (rr - 1) / gInterval, VB6.Format(SPLOT(rr, HistCol), "0.000"))
            Else
                'JRSVBNET Pego1.set_YData(0, (rr - 1) / gInterval, -9999)  'JRS 5/10/02 0
            End If
            If Not IsDBNull(Session("Validation_PLOT")(rr, ForecastColCol)) Then
                'JRSVBNET Pego1.set_YData(1, (rr - 1) / gInterval, VB6.Format(SPLOT(rr, ForecastCol), "0.000"))
            Else
                'JRSVBNET Pego1.set_YData(1, (rr - 1) / gInterval, -9999)  'JRS 5/10/02 0
            End If
        Next rr

        'Scalegraph

        'JRSVBNET Pego1.PEactions = 0
        'JRS 03/22/04
        Session("TableDisYear") = DisYear
    End Sub
    '==========================frm_TableDisplay==================
    Private Sub setFormHeight()
        Dim MaxNoRows As Short
        Dim ButSpace As Object
        Dim ButCushion As Object
        Dim RowHeight As Object
        Dim TitleBarSpace As Object
        '-----------------------------------------------------------
        'mti Validation changes 19 August, 2002- new proc
        'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TitleBarSpace = 425
        'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RowHeight = 230
        'UPGRADE_WARNING: Couldn't resolve default property of object ButCushion. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ButCushion = 200
        'UPGRADE_WARNING: Couldn't resolve default property of object ButCushion. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ButSpace = ButCushion
        'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Screen from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET MaxNoRows = (VB6.PixelsToTwipsY('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - ButSpace - TitleBarSpace) / RowHeight
        If False Then 'JRSVBNET Support.DataGridManager.manager.GetRows(grd_TableDisplay) < MaxNoRows Then 
            'UPGRADE_WARNING: Couldn't resolve default property of object TitleBarSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object ButSpace. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
            'JRSVBNET Height = VB6.TwipsToPixelsY(ButSpace + grd_TableDisplay.Rows * RowHeight + TitleBarSpace)
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Top.html'.
            'JRSVBNET Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY('JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - VB6.PixelsToTwipsY(Height)) / 2)
        Else
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Height.html'.
            'JRSVBNET Height = 'JRSVBNET System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Top.html'.
            'Top = 0 
        End If

    End Sub
    '==========================frm_TableDisplay===================================
    Private Function shedbracket(ByRef TString As String, ByRef BrString As String) As String
        '--------------------------------------------------------------------------
        'mti Validation changes 19 August, 2002 - new proc
        '-takes all the brackets of a particular type,e.g.,[],() or {} off the string
        '-will not work as desired if brackets are not in right order

        Dim pos, pos1 As Short
        Dim BrStart, BrEnd As String

        Select Case BrString
            Case "[", "]", "[]"
                BrStart = "["
                BrEnd = "]"
            Case "(", ")", "()"
                BrStart = "("
                BrEnd = ")"
            Case "{", "}", "{}"
                BrStart = "{"
                BrEnd = "}"
        End Select

        Do While True
            pos = InStr(TString, BrStart)
            pos1 = InStr(TString, BrEnd)
            If pos > 0 Then
                TString = Mid(TString, 1, pos - 1) & Mid(TString, pos1 + 1)
            Else
                Exit Do
            End If
        Loop

        shedbracket = TString

    End Function
    '==========================frm_TableDisplay=================
    Private Function VInfo() As String
        '-----------------------------------------------------------
        'mti Validation changes 19 August, 2002-new func

        VInfo = "           Distance = forecast-empirical ;   % Error = Absolute((forecast-empirical)/empirical)*100"
    End Function
    '==========================frm_TableDisplay=================
    'UPGRADE_NOTE: Height was upgraded to Height_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    'UPGRADE_NOTE: Width was upgraded to Width_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    'UPGRADE_NOTE: Text was upgraded to Text_Renamed. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1061"'
    Private Function BuildHTML_TableCell(Optional ByRef Text_Renamed As String = "", Optional ByRef Rowspan As Short = 0, Optional ByRef Width_Renamed As Short = 0, Optional ByRef Height_Renamed As Short = 0, Optional ByRef Align As String = "", Optional ByRef valign As String = "", Optional ByRef fontcolor As String = "", Optional ByRef fontsize As Short = 0, Optional ByRef Bold As String = "NO", Optional ByRef UnderLine As String = "NO") As String
        '-----------------------------------------------------------
        'mti Validation changes 19 August, 2002-new func
        'width is in percentage

        Dim Qot, TStr As String
        Qot = """"

        BuildHTML_TableCell = ""



        TStr = "<td"

        If Width_Renamed > 0 Then
            TStr = TStr & " width = " & Qot & Trim(Str(Width_Renamed)) & "%" & Qot
        End If

        If Height_Renamed > 0 Then
            TStr = TStr & " height = " & Qot & Trim(Str(Height_Renamed)) & Qot
        End If


        If Rowspan > 0 Then
            TStr = TStr & " rowspan = " & Qot & Trim(Str(Rowspan)) & Qot
        End If

        If Len(Align) > 0 Then
            TStr = TStr & " align = " & Qot & Align & Qot
        End If

        If Len(valign) > 0 Then
            TStr = TStr & " vAlign = " & Qot & valign & Qot
        End If
        TStr = TStr & ">"

        If UCase(Bold) = "YES" Then
            TStr = TStr & "<b>"
        End If
        If UCase(UnderLine) = "YES" Then
            TStr = TStr & "<u>"
        End If

        If Len(fontcolor) > 0 Or fontsize > 0 Then
            TStr = TStr & "<font"
            If Len(fontcolor) > 0 Then
                TStr = TStr & " color = " & Qot & fontcolor & Qot
            End If
            If fontsize > 0 Then
                TStr = TStr & " size = " & Qot & Trim(Str(fontsize)) & Qot
            End If
            TStr = TStr & ">"
        End If
        TStr = TStr & Text_Renamed
        If Len(fontcolor) > 0 Or fontsize > 0 Then
            TStr = TStr & "</font>"
        End If

        If UCase(UnderLine) = "YES" Then
            TStr = TStr & "</u>"
        End If

        If UCase(Bold) = "YES" Then
            TStr = TStr & "</b>"
        End If

        TStr = TStr & "</td>"

        BuildHTML_TableCell = TStr
        '                Print #FileNo, "<td width=" + Qot + "10%" + Qot + ">" + "<B><U>" + ValtoShow + "</U></B>" + "</td>"
        '                Print #FileNo, "<td rowspan=" + Qot + Trim(Str(Int(DisYear% / Session("Interval")%))) + Qot + " align=" + Qot + "center" + Qot + " valign=" + Qot + "middle" + Qot + ">"

    End Function

    'jrs 2009/01/30
    'Public Sub ResetPLOT()
    '    Dim rr As Short
    '    Dim cc As Short
    '    'JRS 11/25/02
    '    'Used when plot vector contains percentages.
    '    For cc = 1 To Session("COUNTE")
    '        For rr = 1 To DisYear + 1
    '            'UPGRADE_WARNING: Couldn't resolve default property of object SPLOT(rr, cc). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(rr, cc). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '            Session("PLOT")(rr, cc) = SPLOT(rr, cc)
    '        Next rr
    '    Next cc
    'End Sub

    'JRS 12/12/02
    Private Sub FilterAction(ByRef sOption As String)
        Dim O As Object
        'Define filter variables
        Dim fileIdx As Short
        fileIdx = InStr(1, Session("VARN")(1, 1), "[")
        If fileIdx > 0 Then
            Session("Fil_VarName") = VB.Left(Session("VARN")(1, 1), fileIdx - 1)
            Session("Fil_File") = Val(Mid(Session("VARN")(1, 1), fileIdx + 1, Len(Session("VARN")(1, 1)) - fileIdx - 1))
        Else
            Session("Fil_VarName") = Session("VARN")(1, 1)
            'JRS 04/14/03
            bUsingComp = Not (Session("aFormulas")(1) Is Nothing)
            'jrs 2006/01/22
            If bUsingComp Then
                If Session("aFormulas")(1).Name <> Session("VARN")(1, 1) Then
                    bUsingComp = False
                End If
            End If
            'JRS 04/07/03
            If Session("FirstYear") = FirstYearHistory Then
                Session("Fil_File") = -1
            Else
                Session("Fil_File") = 0
                'jrs 2006/08/22 Handling Files with formulas
                If bUsingComp Then
                    fileIdx = InStr(1, Session("aFormulas")(1).disVar.displayDet(1), "[")
                    Session("Fil_File") = Val(Mid(Session("aFormulas")(1).disVar.displayDet(1), fileIdx + 1, Len(Session("aFormulas")(1).disVar.displayDet(1)) - fileIdx - 1))
                End If
            End If
        End If
        If Session("Fil_Region") = "" Then
            Session("Fil_Region") = Session("GR1N")(1)
            Session("Fil_Dim2") = Session("GR2N")(1)
            Session("Fil_Dim3") = Session("GR3N")(1)
        End If
        'jrs 2006/02/19
        If Session("Fil_Region") = "" Then
            LoadMessage("Filter only works for Variables where the first dimension is Region/Country or Group.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        'define if working with groups
        Dim i As Short
        Dim j As Short
        bFilter_Group = False
        For i = 1 To GCount
            If Session("Fil_Region") = GroupNames(i) Then
                bFilter_Group = True
                Exit For
            End If
        Next i

        'jrs 2012/01/16
        If Session("bFiltLoadedValues") Then
            'Save original plot
            SaveForFilter()
            For i = Session("COUNTE") To 1 Step -1
                'validate column
                gFilterColumn(i, sOption, Session("COUNTE"), Session("PLOT"), Timepoints,
                Session("Fil_Min_Level"), Session("Fil_Max_Level"), Session("Fil_Rate"),
                Session("Fil_Range_Min"), Session("Fil_Range_Max"), Session("Interval"), True,
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                Session("VARN"))
            Next i
        Else
            'jrs 11/17/04
            ''JRS 12/16/02
            'If Session("PercentOn") = "True" Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            'jrs 2012/01/09
            Dim fCopy As New Formula
            fCopy = Session("aFormulas")(1)

            'clear plot
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))

            'jrs 2012/01/09
            Session("aFormulas")(1) = fCopy

            '04/15/03
            If bUsingComp Then
                FilterComp(sOption, bFilter_Group)
            Else
                'if working with forecast
                If Session("Fil_File") > -1 Then
                    FilterVar(sOption, bFilter_Group)
                    'if working with history
                Else
                    FilterHisVar(sOption, bFilter_Group)
                End If
            End If
        End If

        'check if empty
        If Session("COUNTE") = O Then
            mnuFilterNone_Click(Me, New System.EventArgs)

            LoadMessage("No regions found for filter", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
            'jrs 2008/06/07 if using rank options after filter, blowing up
        Else
            ReDim Preserve Session("PLOT_Pct_Tot")(UBound(Session("PLOT"), 1), Session("COUNTE"))
        End If

        'jrs 10/27/04 add percentage filtering
        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If

        'refresh grid
        RefreshTable()
        'JRS 10/27/04
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If
        'jrs 2012/01/16
        If Session("bFiltDisplayCount") Then
            FilterCount(sOption)
        End If
    End Sub

    'jrs 2012/01/16
    Public Sub FilterCount(ByRef filter_Renamed As String)
        Dim i As Short, j As Short
        Dim FiltCount() As Integer
        ReDim FiltCount(Timepoints)

        For i = 1 To Timepoints
            For j = 1 To Session("COUNTE")
                If IsNumeric(Session("PLOT")(i, j)) Then
                    Select Case filter_Renamed
                        Case "Min"
                            If Session("PLOT")(i, j) >= Session("Fil_Min_Level") Then
                                FiltCount(i) = FiltCount(i) + 1
                            End If
                        Case "Max"
                            If Session("PLOT")(i, j) <= Session("Fil_Max_Level") Then
                                FiltCount(i) = FiltCount(i) + 1
                            End If
                        Case "Rate"
                            If i > 1 And IsNumeric(Session("PLOT")(i, j)) Then
                                'jrs 2007/02/12 considering intervals
                                If j > 1 Then
                                    If Math.Sign(Session("PLOT")(i, j)) = Math.Sign(Session("PLOT")(i - 1, j)) And
                                    Math.Abs(Session("PLOT")(i - 1, j)) > 0.0001 Then
                                        If (Session("PLOT")(i, j) / Session("PLOT")(i - 1, j)) ^ (1 / j) - 1 >= Session("Fil_Rate") / 100 Then
                                            FiltCount(i) = FiltCount(i) + 1
                                        End If
                                    End If
                                End If
                            End If
                        Case "Range"
                            If Session("PLOT")(i, j) <= Session("Fil_Range_Max") And Session("PLOT")(i, j) >= Session("Fil_Range_Min") Then
                                FiltCount(i) = FiltCount(i) + 1
                            End If
                    End Select
                End If
            Next j
        Next i
        For i = 1 To Timepoints
            Session("PLOT")(i, 1) = FiltCount(i)
        Next i
        Session("GR1N")(1) = ""
        For j = 2 To Session("COUNTE")
            For i = 1 To Timepoints
                Session("PLOT")(i, j) = 0
            Next i
        Next j
        Session("COUNTE") = 1
        RefreshTable()

    End Sub

    'Subroutine to load filtered variable across all regions or groups
    Private Sub FilterVar(ByRef sOption As String, ByRef bFilter_Group As Boolean)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'jrs 10/27/04 add percentage filtering
        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")

        'jrs 2006/04/19
        Dim iRowPosition As Integer, iFY As Integer
        iRowPosition = 1
        iFY = Session("FirstYear")
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
            iFY = FirstYearForecast
            iRowPosition = LastYearHistory - FirstYearHistory + 1
        End If

        Dim idx As Short
        Dim i As Short
        Dim j As Short
        idx = IfsVars.Item(Session("Fil_VarName")).Index
        'JRS 04/07/03
        If idx = 0 Then
            idx = -1
        End If

        'jrs 2007/02/19
        Dim bPoverty As Boolean
        Dim iPos As Integer, iPos2 As Integer, iPos3 As Integer
        Dim level As Single
        Dim bPctPop As Boolean
        Dim bFind As Boolean
        iPos = InStr(Session("Fil_VarName"), "INCOMELT")
        iPos2 = InStr(Session("Fil_VarName"), "LN")
        iPos3 = InStr(Session("Fil_VarName"), "%")
        bPoverty = False
        bPctPop = False
        bFind = False
        'jrs 2014/10/21
        Dim b2005PPP As Boolean, iPos4 As Integer, iPos5 As Integer, bIncRange As Boolean, level2 As Single
        'jrs 2014/10/21
        iPos4 = InStr(Session("Fil_VarName"), "GT")
        iPos5 = InStr(Session("Fil_VarName"), "2011")
        'jrs 2014/10/21
        b2005PPP = False
        bIncRange = False

        If iPos > 0 And iPos2 > 0 Then
            bFind = (iPos3 > 0 And iPos3 < iPos2)
            If bFind Then
                level = Val(Mid(Session("Fil_VarName"), 9, iPos2 - 10))
            Else
                'jrs 2011/06/17
                If iPos4 > 0 Then
                    level2 = Val(Mid(Session("Fil_VarName"), 9, iPos4 - 9))
                    'jrs 2021/05/05
                    level2 = level2 / 100
                    'jrs 2013/03/12 level = Val(mid(sVar, iPos4 + 2, iPos2 - 11 - Len(CStr(level))))
                    level = Val(Mid(Session("Fil_VarName"), iPos4 + 2, iPos2 - 11 - Len(CStr(level2))))
                    bIncRange = True
                    If InStr(Session("Fil_VarName"), "%") > 0 Then
                        bPctPop = True
                    End If
                Else
                    level = Val(Mid(Session("Fil_VarName"), 9, iPos2 - 9))
                    'jrs 2021/05/05 If level <> 1 And level <> 2 Then
                    If level <> 190 And level <> 310 Then
                        bPoverty = True
                    End If
                    If Right(Session("Fil_VarName"), 1) = "%" Then
                        bPctPop = True
                    End If
                End If
            End If
            'jrs 2021/05/05
            level = level / 100
        End If
        'jrs 2014/10/21
        If iPos5 > iPos2 Then
            b2005PPP = True
        End If

        '******************** FOR REGIONS ***********************
        Dim sCountries() As String
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe
        If Not bFilter_Group Then

            For i = 1 To NR
                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If

                Session("COUNTE") = Session("COUNTE") + 1
                'jrs 2021/05/06
                If bIncRange Then
                    gLoadIncomeRange(level, level2, GRPS(1, i), Session("Fil_File"), bFilter_Group, bPctPop, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                    Session("SensitivityFlag"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                    'jrs 2007/02/19
                ElseIf bPoverty Or bPctPop Then
                    gLoadPovertyDisp(level, GRPS(1, i), Session("Fil_File"), bFilter_Group, bPctPop, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                    Session("SensitivityFlag"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                ElseIf bFind Then 'jrs 2007/03/07
                    gFindPovertyLevel(GRPS(1, i), Session("Fil_File"), bFilter_Group, level, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/06
                Else
                    'reload plot
                    gLoadForDisplayNET(Session("Fil_VarName"), idx, GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), i = 1, i = NR, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay(Session("Fil_VarName"), idx, GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), i = 1, i = NR, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    'iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, i), Session("Fil_Dim2"), Session("Fil_Dim3"), bFilter_Group, Session("Fil_File"), "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                    'jrs 2005/07/25
                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                        'jrs 2006/06/08 lastyearhistory -1 in order to keep forecast value for year 2000
                        gLoadForDisplayHis(Session("Fil_VarName"), GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                        bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                        Session("FinYear")(Session("COUNTE")) = Session("LastYear") - FirstYearHistory + 1
                    End If
                End If

                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If
                'validate column
                gFilterColumn(Session("COUNTE"), sOption, Session("COUNTE"), Session("PLOT"), Timepoints,
                Session("Fil_Min_Level"), Session("Fil_Max_Level"), Session("Fil_Rate"), Session("Fil_Range_Min"),
                Session("Fil_Range_Max"), Session("Interval"), False,
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                Session("VARN"))
            Next i
            '******************** FOR GROUPS ***********************
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object GCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For i = 1 To GCount
                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If
                Session("COUNTE") = Session("COUNTE") + 1
                'jrs 2021/05/06
                If bIncRange Then
                    gLoadIncomeRange(level, level2, GroupNames$(i), Session("Fil_File"), bFilter_Group, bPctPop, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                    Session("SensitivityFlag"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                    'jrs 2007/02/19
                ElseIf bPoverty Or bPctPop Then
                    gLoadPovertyDisp(level, GroupNames$(i), Session("Fil_File"), bFilter_Group, bPctPop, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                    Session("SensitivityFlag"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                ElseIf bFind Then
                    gFindPovertyLevel(GroupNames$(i), Session("Fil_File"), bFilter_Group, level, b2005PPP,
                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/06
                Else
                    'Load countries in group
                    gLoadForDisplayNET(Session("Fil_VarName"), idx, GroupNames$(i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), i = 1, i = GCount, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay(Session("Fil_VarName"), idx, GroupNames$(i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), i = 1, i = GCount, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    'iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GroupNames$(i), Session("Fil_Dim2"), Session("Fil_Dim3"), bFilter_Group, Session("Fil_File"), "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                    'jrs 2005/08/18
                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                        'jrs 2006/06/08 lastyearhistory -1 in order to keep forecast value for year 2000
                        gLoadForDisplayHis(Session("Fil_VarName"), GroupNames$(i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                        bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                        Session("FinYear")(Session("COUNTE")) = Session("LastYear") - FirstYearHistory + 1
                    End If
                End If

                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If
                'validate column
                gFilterColumn(Session("COUNTE"), sOption, Session("COUNTE"), Session("PLOT"), Timepoints,
                Session("Fil_Min_Level"), Session("Fil_Max_Level"), Session("Fil_Rate"), Session("Fil_Range_Min"),
                Session("Fil_Range_Max"), Session("Interval"), False,
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                Session("VARN"))
            Next i
        End If
    End Sub

    'Subroutine to load filtered historical variable across all regions or groups
    Private Sub FilterHisVar(ByRef sOption As String, ByRef bFilter_Group As Boolean)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'jrs 10/27/04 add percentage filtering
        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")
        Dim sDef As String, sUn As String

        Dim i As Short
        '******************** FOR REGIONS ***********************
        If Not bFilter_Group Then
            For i = 1 To NR
                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If

                Session("COUNTE") = Session("COUNTE") + 1
                'Set region
                gLoadForDisplayHis(Session("Fil_VarName"), GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, Session("LastYear"),
                False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                gLoadForDisplayHisSaveLbls(Session("COUNTE"), Session("Fil_VarName"), GRPS(1, i), bFilter_Group, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If
                'jrs 2011/11/02 using Timepoints instead of FinYear (array)
                'validate column
                gFilterColumn(Session("COUNTE"), sOption, Session("COUNTE"), Session("PLOT"), Timepoints,
                Session("Fil_Min_Level"), Session("Fil_Max_Level"), Session("Fil_Rate"), Session("Fil_Range_Min"),
                Session("Fil_Range_Max"), Session("Interval"), False,
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                Session("VARN"))
            Next i
            '******************** FOR GROUPS ***********************
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object GCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For i = 1 To GCount
                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If
                Session("COUNTE") = Session("COUNTE") + 1
                gLoadForDisplayHis(Session("Fil_VarName"), GroupNames(i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, Session("LastYear"),
                False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                'jrs 2011/11/02 fixed error replacing GroupNames for GRPS(1,i)
                gLoadForDisplayHisSaveLbls(Session("COUNTE"), Session("Fil_VarName"), GroupNames(i), bFilter_Group, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                'validate column
                'jrs 10/27/04 move to whole for loading
                If bPct Then
                    mnuPercent_Click(Me, New EventArgs)
                End If
                'jrs 2011/11/02 using Timepoints instead of FinYear (array)
                gFilterColumn(Session("COUNTE"), sOption, Session("COUNTE"), Session("PLOT"), Timepoints,
                Session("Fil_Min_Level"), Session("Fil_Max_Level"), Session("Fil_Rate"), Session("Fil_Range_Min"),
                Session("Fil_Range_Max"), Session("Interval"), False,
                Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
                Session("VARN"))
            Next i
        End If
    End Sub

    'JRS 04/14/03
    'Subroutine to load filtered computed variable across all regions or groups
    Private Sub FilterComp(ByRef sOption As String, ByVal bFilter_Group As Boolean)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))
        'jrs 2006/02/19
        Dim frm_msg As frm_Message
        If Session("aFormulas")(1).Dimension = "" Then
            frm_msg.LoadMessage("This Formula can't be used with Filter.")
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        'jrs 2006/02/01
        Dim oldLastYear As Integer
        oldLastYear = Session("LastYear")
        Dim fCopy As New Formula
        fCopy = Session("aFormulas")(1)

        'jrs 10/27/04 add percentage filtering
        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")
        'jrs 10/27/04 move to whole for loading
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If

        Dim i As Integer
        If Not bFilter_Group Then
            For i = 1 To NR
                If Session("aFormulas")(1).Formula2 > "" Then
                    gLoadCompHisFor(Nothing, GRPS(1, i), Session("Fil_Dim2"), Session("Fil_Dim3"), Session("Fil_File"), 1, Session("bFillHis"),
                    Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"), Session("aFormulas"),
                    Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me,
                    Session("FinYear"), Session("VARN"), Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                Else
                    Session("COUNTE") = Session("COUNTE") + 1
                    gLoadComp(Session("COUNTE"), Session("aFormulas")(1).Formula_Renamed, Session("aFormulas")(1).Name, GRPS(1, i), Session("Fil_Dim2"), Session("Fil_Dim3"),
                    Session("aFormulas")(1).Unit, -1, bFilter_Group, Session("Fil_File"), (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory),
                    False, fCopy, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                    True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                    Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                    Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                End If
                FormatFormAfterLoad(Session("Fil_File"), Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
                IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
                Session("MaxFinYearHis"))
            Next i
        Else
            For i = 1 To GCount
                If Session("aFormulas")(1).Formula2 > "" Then
                    gLoadCompHisFor(Nothing, GroupNames(i), Session("Fil_Dim2"), Session("Fil_Dim3"), Session("Fil_File"), 1,
                    Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"),
                    Session("aFormulas"), Session("bComputation"), Session("FirstYear"), Session("LastYear"),
                    Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
                    Session("VARN"), Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                Else
                    Session("COUNTE") = Session("COUNTE") + 1
                    gLoadComp(Session("COUNTE"), Session("aFormulas")(1).Formula_Renamed, Session("aFormulas")(1).Name, GroupNames(i), Session("Fil_Dim2"), Session("Fil_Dim3"),
                    Session("aFormulas")(1).Unit, -1, bFilter_Group, Session("Fil_File"), (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory),
                    False, fCopy, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                    True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars, Session("PLOT"),
                    Session("aFormulas"), Session("bFillHisOnlyHoles"),
                    Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                End If
                FormatFormAfterLoad(Session("Fil_File"), Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
                IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
                Session("MaxFinYearHis"))
            Next i
        End If

        'jrs 10/27/04 move to whole for loading
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If
        'validate column
        gFilterAll(sOption, Session("COUNTE"), Session("PLOT"), Timepoints, Session("Fil_Min_Level"),
        Session("Fil_Max_Level"), Session("Fil_Rate"), Session("Fil_Range_Min"), Session("Fil_Range_Max"),
        Session("LastYear"), Session("buseAllHis"), Session("ActiveBase"), Session("FinYearHis"), Session("FirstYear"),
        Session("aFormulas"), Session("FileNumbr"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"),
        Session("GR4N"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"))

    End Sub

    'jrs 2005/12/19 RAM for Formulas
    Private Sub FilterCompNone()
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'jrs 2006/02/01
        Dim oldLastYear As Integer
        oldLastYear = Session("LastYear")

        'jrs 10/27/04 add percentage filtering
        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")
        'jrs 10/27/04 move to whole for loading
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If

        If Session("aFormulas")(1).Formula2 > "" Then
            gLoadCompHisFor(Nothing, Session("Fil_Region"), Session("Fil_Dim2"), Session("Fil_Dim3"), Session("Fil_File"), 1,
            Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"),
            Session("aFormulas"), Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("VARN"), Session("FileforDisplay"), IfsPars,
            Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
        Else
            Dim fCopy As New Formula
            fCopy = Session("aFormulas")(1)

            Session("COUNTE") = Session("COUNTE") + 1
            gLoadComp(Session("COUNTE"), Session("aFormulas")(1).Formula_Renamed, Session("aFormulas")(1).Name, Session("Fil_Region"), Session("Fil_Dim2"), Session("Fil_Dim3"),
            Session("aFormulas")(1).Unit, -1, bFilter_Group, Session("Fil_File"), (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory),
            False, fCopy, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), False, Session("bComputation"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
            Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
        End If
        FormatFormAfterLoad(Session("Fil_File"), Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
        IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
        Session("MaxFinYearHis"))


        'jrs 10/27/04 move to pct for filtering
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If
    End Sub

    '=============================TablDisp.Frm=======================
    'JRS 08/08/03
    'jrs 2024/11/25 Private Sub ReFillData(ByRef cc As Short, ByRef ry() As TableRow)
    Private Sub ReFillData(ByRef cc As Integer, ByRef ry() As TableRow)
        Dim Value As Object
        Dim rr As Short
        Dim Y As Short
        Dim RFSN As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
        RFSN = Val(Session("FileNumbr")(cc))
        Y = Session("StartYrIdx") 'jrs 2016/03/30 1

        For rr = 1 To Timepoints
            Value = Session("PLOT")(Y, cc)
            Y = Y + Session("Interval")

            '            If Y% > DisYear% + 1 Then Y% = DisYear% + 1
            'jrs 2014/05/01 If y% > DisYear% Then y% = DisYear%
            'jrs 2016/04/11 If Y > DisYear And Y - Session("Interval") < DisYear Then Y = DisYear
            If Y > DisYear + Session("StartYrIdx") - 1 And Y - Session("Interval") < DisYear + Session("StartYrIdx") - 1 Then Y = DisYear + Session("StartYrIdx") - 1

            'jrs 2022/02/24 added Show Extra Decimals
            If Session("ShowExtraDecimals") = "YES" Then
                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00000000"), Len(ValtoShow.Value))
                If StandAloneMode Then
                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,##0.00000000}", Value)
                Else
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00000000"), Len(ValtoShow.Value))
                End If

            Else
                'jrs 2025/03/20 added extra decimal
                'jrs 2023/10/03 ValtoShow.Value = RSet(VB6.Format(Value, "###,###.0000"), Len(ValtoShow.Value))
                If StandAloneMode Then
                    ValtoShow.Value = String.Format(New CultureInfo("en-US"), "{0:#,##0.00000}", Value)
                Else
                    ValtoShow.Value = RSet(VB6.Format(Value, "###,###.00000"), Len(ValtoShow.Value))
                End If

            End If

            'jrs 2009/03/04
            ry(rr).Cells(cc).Text = ValtoShow.Value

        Next rr

        'jrs 2009/03/04
        Session("FourDecs")(cc) = True

    End Sub

    'JRS 09/29/03
    Private Sub LoadRow(ByRef rr As Short, ByVal fNum As Integer)
        Dim Value As Object
        Dim Y As Short
        Dim cc As Short
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, rr)
        For cc = 0 To Session("COUNTE")
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
            If rr >= 6 And cc >= 1 Then
                Value = Session("PLOT")(Y, cc)
                If cc = Session("COUNTE") Then
                    Y = Y + Session("Interval")
                    If Y > DisYear Then Y = DisYear
                End If
                'jrs 2009/03/04 ValtoShow.Value = ValueScale(Value)
                If IsNumeric(Value) Then
                    ValtoShow.Value = ValueScale(Value, Session("FourDecs")(cc), True)
                Else
                    ValtoShow.Value = RSet(VB6.Format("", "############"), Len(ValtoShow.Value))
                End If

                If cc < Session("COUNTE") Then
                    Print(fNum, ValtoShow.Value & ",")
                Else
                    PrintLine(fNum, ValtoShow.Value)
                End If
            Else
                If cc < Session("COUNTE") Then
                    If rr = 5 And cc = 0 Then
                        Print(fNum, ",")
                    Else
                        'jrs 2005/03/18
                        Print(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                    End If
                Else
                    'jrs 2005/03/18
                    PrintLine(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                End If
            End If
        Next cc
    End Sub

    'jrs 2012/01/27 removed ByVal fNum As Integer to avoid creating file in server (concurrency issues)
    'JRS 09/29/03
    Private Sub LoadColumn(ByVal cc As Short, ByVal Rpoints As Short, ByRef Frec As Short, ByRef bTotal As Boolean)
        Dim Value As Object
        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
        Dim rr As Short, rrPLOT As Integer, rrOflw As Integer
        For rr = 0 To Rpoints
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, rr)
            If rr > 5 And cc > 0 Then
                'jrs 2006/07/12
                rrPLOT = (rr - 6) * Session("Interval") + 1
                If rrPLOT > DisYear Then
                    rrOflw = rrPLOT - DisYear
                    rrPLOT = DisYear
                End If

                If IsDBNull(Session("PLOT")(rrPLOT, cc)) Then
                    Value = ""
                Else
                    'jrs 2005/08/19
                    If CBool(Session("PercentOn")) And Frec > 1 Then
                        If (rrPLOT > (iFirstYearSave - Session("FirstYear") + 1)) Then
                            Value = ((Session("Percent_PLOT")(rrPLOT, cc) / Session("Percent_PLOT")((rr - 6 - Frec) * Session("Interval") + 1, cc)) ^ (1 / (Frec * Session("Interval") - rrOflw)) - 1) * 100
                        ElseIf iFirstYearSave <> Session("FirstYear") Then
                            Value = ((Session("Percent_PLOT")(iFirstYearSave - Session("FirstYear") + 1, cc) / Session("Percent_PLOT")(1, cc)) ^ (1 / (iFirstYearSave - Session("FirstYear"))) - 1) * 100
                        Else
                            Value = 0
                        End If
                    Else
                        Value = Session("PLOT")(rrPLOT, cc)
                    End If
                End If
                'JRS 2006/07/12
                'If Session("Interval") > 1 Then rr = rr + Session("Interval") - 1
                'If rr - 5 > DisYear Then
                '    rr = DisYear + 5
                'End If

                'jrs 2009/03/04
                'If CStr(Value) = "" Then
                '    ValtoShow.Value = ""
                'Else
                '    ValtoShow.Value = ValueScale(CDbl(Value))
                'End If
                'jrs 2009/03/04
                If IsNumeric(Value) Then
                    ValtoShow.Value = ValueScale(Value, Session("FourDecs")(cc), False)
                Else
                    ValtoShow.Value = RSet(VB6.Format("", "############"), Len(ValtoShow.Value))
                End If

                If rr < Rpoints Then
                    'jrs 2012/01/27 Print(fNum, ValtoShow.Value & ",")
                    Context.Response.Write(ValtoShow.Value & ",")
                Else
                    'jrs 2012/01/27 PrintLine(fNum, ValtoShow.Value)
                    Context.Response.Write(ValtoShow.Value)
                    Context.Response.Write(Environment.NewLine)
                End If
            Else
                If rr = 1 And bTotal Then
                    'jrs 2012/01/27 Print(fNum, "Total ")
                    Context.Response.Write("Total ")
                End If
                If rr < Rpoints Then
                    'jrs 2005/03/18
                    'jrs 2012/01/27 Print(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                    Context.Response.Write(Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                Else
                    'jrs 2005/03/18
                    'jrs 2012/01/27 PrintLine(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                    Context.Response.Write(Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                    Context.Response.Write(Environment.NewLine)
                End If
            End If
            If Frec > 1 And rr > 5 And rr < Rpoints Then
                rr = rr + Frec - 1
                If rr >= Rpoints Then
                    rr = Rpoints - 1
                End If
            End If
            'jrs 2005/05/10
            If rr = 5 Then
                'jrs 2006/07/13
                If Session("Interval") = 0 Then Session("Interval") = 1
                rr = 5 + (iFirstYearSave - Session("FirstYear")) / Session("Interval")
            End If
        Next rr
    End Sub

    'jrs 2012/01/27 removed , ByVal fNum As Integer to avoid creating file in server (concurrency problems)
    'JRS 10/02/03
    Private Sub LoadGroupMembers(ByRef sReg As String, ByVal cc As Short, ByVal Rpoints As Short, ByRef Frec As Short)
        Dim Value As Object
        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_TableDisplay, cc)
        Dim rr As Short, rrPLOT As Integer, rrOflw As Integer
        For rr = 0 To Rpoints
            If rr > 5 And cc > 0 Then
                'jrs 2006/07/12
                rrPLOT = (rr - 6) * Session("Interval") + 1
                If rrPLOT > DisYear Then
                    rrOflw = rrPLOT - DisYear
                    rrPLOT = DisYear
                End If

                If IsDBNull(Session("PLOT")(rrPLOT, Session("COUNTE") + 1)) Then
                    Value = ""
                Else
                    'jrs 2005/08/19
                    If CBool(Session("PercentOn")) Then
                        If (rrPLOT > (iFirstYearSave - Session("FirstYear") + 1)) Then
                            Value = ((Session("PLOT")(rrPLOT, Session("COUNTE") + 1) / Session("PLOT")((rr - 6 - Frec) * Session("Interval") + 1, Session("COUNTE") + 1)) ^ (1 / (Frec * Session("Interval") - rrOflw)) - 1) * 100
                        ElseIf iFirstYearSave <> Session("FirstYear") Then
                            Value = ((Session("PLOT")(iFirstYearSave - Session("FirstYear") + 1, Session("COUNTE") + 1) / Session("PLOT")(1, Session("COUNTE") + 1)) ^ (1 / (iFirstYearSave - Session("FirstYear"))) - 1) * 100
                        Else
                            Value = 0
                        End If
                    Else
                        Value = Session("PLOT")(rrPLOT, Session("COUNTE") + 1)
                    End If
                End If
                'JRS 2006/07/12
                'If Session("Interval") > 1 Then rr = rr + Session("Interval") - 1
                'If rr - 5 > DisYear Then
                '    rr = DisYear + 5
                'End If
                'jrs 2009/03/04
                'If CStr(Value) = "" Then
                '    ValtoShow.Value = ""
                'Else
                '    ValtoShow.Value = ValueScale(CDbl(Value))
                'End If
                'jrs 2009/03/04
                If IsNumeric(Value) Then
                    ValtoShow.Value = ValueScale(Value, False, False)
                Else
                    ValtoShow.Value = RSet(VB6.Format("", "############"), Len(ValtoShow.Value))
                End If

                If rr < Rpoints Then
                    'jrs 2012/01/27 Print(fNum, ValtoShow.Value & ",")
                    Context.Response.Write(ValtoShow.Value & ",")
                Else
                    'jrs 2012/01/27 PrintLine(fNum, ValtoShow.Value)
                    Context.Response.Write(ValtoShow.Value)
                    Context.Response.Write(Environment.NewLine)
                End If
            Else
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_TableDisplay, rr)
                If rr = 1 Then
                    'jrs 2016/05/26
                    If Session("bAlias") And IFsRegs.Item(sReg).sAlias > "" Then
                        Context.Response.Write(Replace(IFsRegs.Item(sReg).sAlias, ",", ";") & ",")
                    Else 'jrs 2005/03/18
                        'jrs 2012/01/27 Print(fNum, Replace(sReg, ",", ";") & ",")
                        Context.Response.Write(Replace(sReg, ",", ";") & ",")
                    End If
                ElseIf rr < Rpoints Then
                    'jrs 2005/03/18
                    'jrs 2012/01/27 Print(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                    Context.Response.Write(Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";") & ",")
                Else
                    'jrs 2005/03/18
                    'jrs 2012/01/27 PrintLine(fNum, Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                    Context.Response.Write(Replace(grd_TableDisplay.Rows(rr).Cells(cc).Text, ",", ";"))
                    Context.Response.Write(Environment.NewLine)
                End If
            End If
            If Frec > 1 And rr > 5 And rr < Rpoints Then
                rr = rr + Frec - 1
                If rr >= Rpoints Then
                    rr = Rpoints - 1
                End If
            End If
            'jrs 2005/05/10
            If rr = 5 Then
                'jrs 2006/07/13
                If Session("Interval") = 0 Then Session("Interval") = 1
                rr = 5 + (iFirstYearSave - Session("FirstYear")) / Session("Interval")
            End If
        Next rr
    End Sub


    'JRS 10/02/03
    Private Sub LoadCurrencyFct()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   JRS 5-1-01
        '- - - - - - - - - - - - - - By Jose Solorzano - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - RA Working for Barry Hughes - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - 5-01-01 - - - - - - - - - - - - - - -
        'jrs 2005/12/09 ReadCurrency()
        'If it returns information, load it to the variables
        If rsCurr.Rows.Count > 0 Then
            'Updates the Currency Factor
            CurrencyFct = rsCurr.Rows(0).Item("Currency Value")
            'JRS 6/19/01 CurrencyLableSwitch
        Else
            CurrencyFct = 1
        End If

        'JRS 10/03/02
        If CurrencyFct <> 0 Then
            CurrencyFct = 1 / CurrencyFct
        Else
            CurrencyFct = 1
        End If
    End Sub


    Public Sub mnuDisplayRunHorizon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If CBool(Session("HistoricBase")) = True Then
            Msg = "You cannot change the horizon for historical analysis."

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        'jrs 11/17/04
        cmb_Horizon.Items.Clear()
        'jrs 2005/12/11
        cmb_Horizon.Items.Add("")
        'jrs 2008/12/22
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added Session("Interval")
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
            'jrs 2013/01/22
            If Session("DisplayLastYear") = ICount Then
                cmb_Horizon.SelectedIndex = cmb_Horizon.Items.Count - 1
            End If
        Next ICount
        'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 2012/03/09 WinFormsToWeb_FocusControl.Focus("cmb_Horizon")

        txtHorizon.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdHorizon.ClientID + "')")
        cswmWebMenu.Visible = False

    End Sub

    'JRS 10/28/03
    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        'jrs 2011/07/01
        ''jrs 2005/12/11
        'Dim bFlagInt As Boolean, bFlagCum As Boolean, bFlagPct As Boolean
        'bFlagInt = Session("bIntAvg")
        'bFlagCum = Session("bCum")
        'bFlagPct = (Session("PercentOn") = "True")
        ''jrs 2009/01/22
        'Dim bFlagCumDis As Boolean, bFlagDis As Boolean
        'bFlagCumDis = Session("bCumDis")
        'bFlagDis = Session("bDis")

        'If bFlagInt Then
        '    mnuIntAvgOff_Click()
        'ElseIf bFlagCum Then
        '    mnuCumOff_Click()
        'ElseIf bFlagCumDis Then 'jrs 2009/01/22
        '    mnuCumDisOff_Click()
        'ElseIf bFlagDis Then 'jrs 2009/01/22
        '    mnuDisOff_Click()
        'ElseIf bFlagPct Then
        '    mnuPercent_Click(Me, New EventArgs)
        'End If
        'jrs 2013/02/21
        Dim UnitIdx As Integer
        Dim rr, cc As Integer

        ''''''''''''''''''''''''''''''''''''''''''''
        'JAM 2016-03-09 Reverting trillions units applied
        If Session("bTrillionsOk") Then
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'JAM 2016-05-16 
                Next rr
                Session("GR4N")(cc) = Replace(Session("GR4N")(cc), "Trillion", "Billion")
            Next cc
        End If
        Session("bTrillionsOk") = False


        '''''''''''''''''''''''''''''''''''''''''''''''''





        Dim TblDispOptCopy As Stack
        TblDispOptCopy = New Stack
        'Restore original values
        With CType(Session("TblDispOpt"), Stack)
            Do While .Count > 0
                TblDispOptCopy.Push(.Peek)
                Select Case .Peek.ToString
                    Case "Percent"
                        mnuPercent_Click(Me, New EventArgs)
                    Case "Cumulative"
                        mnuCumOff_Click()
                    Case "Discount"
                        mnuDisOff_Click()
                    Case "Cumulative Discount"
                        mnuCumDisOff_Click()
                    Case "Interval Avg"
                        mnuIntAvgOff_Click()
                    Case "Interval Tot"
                        mnuIntTotOff_Click()
                    Case "Interval Abs" 'jrs 2011/07/01
                        mnuIntAbsOff_Click()
                    Case "Moving Avg"
                        mnuMovingAvgOff_Click()
                    Case "Validation"
                        mnuValidation_Click(Me, New EventArgs)
                    Case "Percent of Total"
                        mnuPctTotReset_Click()
                    Case "Comparison"
                        mnuCompReset_Click()
                    Case "Index Base One" 'jrs 2012/01/02
                        mnuIdxBase1Off_Click()
                    Case "Unit Conv" 'jrs 2013/02/20
                        UnitIdx = Session("UnitConvSel")
                        mnuUnitOff_Click()
                    Case Else
                        LoadMessage("There's a problem with the implementation of " & .Peek.ToString & ". Please reload the variables.", Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                End Select
            Loop
        End With

        Dim Ind As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 12/08/03
        'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1039"'
        'JRS 01/28/04 Load(frm_Report3)
        'JRSVBNET Dim frm_Report3 As New frm_Report3

        Ind = cmb_Horizon.SelectedIndex - 1 'jrs 2008/12/22
        'jrs 2005/12/11
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        'JRS 11/03/03 CHANGE LASTYEAR WITH DISPLAYLASTYEAR
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval") 'jrs 2005/12/11 added * session("Interval")
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            'JRS 12/08/03
            If Session("FirstYear") = FirstYearForecast Then
                'jrs 2008/12/18 If Session("DisplayLastYear") > LastYearForecast Or Session("DisplayLastYear") < FirstYearForecast + 1 Then Exit Sub Else If Session("DisplayLastYear") > LastYearHistory Or Session("DisplayLastYear") < FirstYearHistory + 1 Then Exit Sub
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

            ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then 'jrs 2005/08/19
                'jrs 2008/12/18 If Session("DisplayLastYear") > LastYearHistory Or Session("DisplayLastYear") < FirstYearHistory + 1 Then Exit Sub
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

            End If
        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False

        'JRSVBNET *** BEGIN ***
        'If frm_Report3.DefInstance.Visible Then
        '    frm_Report3.DefInstance.cboFiles_SelectedIndexChanged(frm_Report3.DefInstance.cboFiles, New System.EventArgs)
        'Else
        '    frm_Report3.DefInstance.Close()
        'End If
        'JRSVBNET *** END ***
        mnuRefresh_Click(Me, New System.EventArgs)

        'jrs 2011/07/01
        ''jrs 2005/12/11
        'If bFlagInt Then
        '    mnuIntAvgOn_Click()
        'ElseIf bFlagCum Then
        '    mnuCumOn_Click()
        'ElseIf bFlagCumDis Then 'jrs 2009/01/22
        '    mnuCumDisOn_Click()
        'ElseIf bFlagDis Then 'jrs 2009/01/22
        '    mnuDisOn_Click()
        'ElseIf bFlagPct Then
        '    mnuPercent_Click(Me, New EventArgs)
        'End If
        'Restore original values
        With TblDispOptCopy
            Do While .Count > 0
                Select Case .Peek.ToString
                    Case "Percent"
                        mnuPercent_Click(Me, New EventArgs)
                    Case "Cumulative"
                        mnuCumOn_Click()
                    Case "Discount"
                        mnuDisOn_Click()
                    Case "Cumulative Discount"
                        mnuCumDisOn_Click()
                    Case "Interval Avg"
                        mnuIntAvgOn_Click()
                    Case "Interval Tot"
                        mnuIntTotOn_Click()
                    Case "Interval Abs" 'jrs 2011/07/01
                        mnuIntAbsOn_Click()
                    Case "Moving Avg"
                        mnuMovingAvgOn_Click()
                    Case "Validation"
                        mnuValidation_Click(Me, New EventArgs)
                    Case "Percent of Total"
                        mnuPctTotal_Click()
                    Case "Index Base One" 'jrs 2012/01/02
                        mnuIdxBase1On_Click()
                    Case "Unit Conv" 'jrs 2013/02/20
                        mnuUnitOn_Click(UnitIdx)
                End Select
                TblDispOptCopy.Pop()
            Loop
        End With

        'JAM 2016-03-06
        Call Convert_Billions_To_Trillions()


        cswmWebMenu.Visible = True

    End Sub

    'JRS 10/28/03
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.KeyPress.html'.
    Private Sub cmb_Horizon_KeyPress() 'JRSVBNET System.Windows.Forms.KeyPressEventArgs) Handles cmb_Horizon.KeyPress
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        Dim KeyAscii As Short = 0 'JRSVBNET Asc(eventArgs.KeyChar)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If KeyAscii = 13 Then Call cmb_Horizon_SelectedIndexChanged(cmb_Horizon, New System.EventArgs) 'enter key
        If KeyAscii = 27 Then 'Escape
            cmb_Horizon.Visible = False
            frm_Horizon.Visible = False

            'JRS *** BEGIN ***
            'If frm_Report3.DefInstance.Visible Then
            '    frm_Report3.DefInstance.cboFiles_SelectedIndexChanged(frm_Report3.DefInstance.cboFiles, New System.EventArgs)
            'Else
            '    frm_Report3.DefInstance.Close()
            'End If
            'JRS *** END ***
            mnuRefresh_Click(Me, New System.EventArgs)
        End If

        If KeyAscii = 0 Then
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET eventArgs.Handled = True
        End If
    End Sub


    'JRS 03/21/04
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
                'JRS 09/22/04
            Case "Continue", "Main Menu"
                'jrs 2009/01/30
                ''jrs 2009/01/15
                'If Session("bPctTot") Then
                '    mnuPctTotReset_Click()
                'End If

                'If Session("PercentOn") = "True" Then
                '    mnuPercent_Click(Me, New System.EventArgs)
                'End If
                ''jrs 2005/05/17
                'If (Session("bCum") = True) Then
                '    mnuCumOff_Click()
                'End If
                ''jrs 2005/10/14
                'If (Session("bIntAvg") = True) Then
                '    mnuIntAvgOff_Click() 'jrs 2005/11/21 change to off
                'End If
                ''jrs 2008/06/26
                'If (Session("bMovingAvg") = True) Then
                '    mnuMovingAvgOff_Click()
                'End If
                ''jrs 2008/08/29
                'If Session("bCumDis") = True Then
                '    mnuCumDisOff_Click()
                'End If
                ''jrs 2009/01/22
                'If Session("bDis") = True Then
                '    mnuDisOff_Click()
                'End If
                ''jrs 2009/01/07
                'If Session("bComparisonOn") Then
                '    mnuCompReset_Click()
                'End If

                'jrs 2009/01/30
                'Restore original values
                'jrs 2013/02/11
                If Not (Session("TblDispOpt") Is Nothing) Then

                    With CType(Session("TblDispOpt"), Stack)
                        Do While .Count > 0
                            Select Case .Peek
                                Case "Percent"
                                    mnuPercent_Click(Me, New System.EventArgs)
                                Case "Cumulative"
                                    mnuCumOff_Click()
                                Case "Discount"
                                    mnuDisOff_Click()
                                Case "Cumulative Discount"
                                    mnuCumDisOff_Click()
                                Case "Interval Avg"
                                    mnuIntAvgOff_Click()
                                Case "Interval Tot" 'jrs 2011/07/01 
                                    mnuIntTotOff_Click()
                                Case "Interval Abs"
                                    mnuIntAbsOff_Click()
                                Case "Moving Avg"
                                    mnuMovingAvgOff_Click()
                                Case "Validation"
                                        'jrs 2009/01/30 mnuValidation_Click()
                                Case "Percent of Total"
                                    mnuPctTotReset_Click()
                                Case "Comparison"
                                    mnuCompReset_Click()
                                Case "Index Base One" 'jrs 2012/01/02
                                    mnuIdxBase1Off_Click()
                                Case "Unit Conv" 'jrs 2013/02/21
                                    mnuUnitOff_Click()
                            End Select
                        Loop
                    End With

                End If

                Session.Remove("Percent_PLOT")
                Session.Remove("IntervalAvg_PLOT")
                Session.Remove("MovingAvg_PLOT")
                Session.Remove("Validation_PLOT")
                'jrs 2011/07/01
                Session.Remove("IntervalTot_PLOT")
                Session.Remove("IntervalAbs_PLOT")
                'jrs 2012/01/02
                Session.Remove("RatioFstYr_PLOT")

                Session.Remove("TblDispOpt")

                Session.Remove("MovAvgYrs")
                Session.Remove("bDis")
                Session.Remove("bCumDis")
                Session.Remove("CumDisRatePct")
                'jrs 2009/01/15
                Session.Remove("PLOT_Pct_Tot")
                Session.Remove("bPctTot")
                Session.Remove("bComparisonOn")
                Session.Remove("CompBackup")
                Session.Remove("CompLabelBackup")
                Session.Remove("sComparison")
                'jrs 2009/03/04
                Session.Remove("FourDecs")
                'jrs 2021/02/15
                If Session("bAlias") = True Then
                    For i = 1 To Session("COUNTE")
                        'jrs 2023/08/09
                        If i >= Session("oGR1N").length Then Exit For

                        Session("GR1N")(i) = Session("oGR1N")(i)
                        Session("FDNG1")(i) = Session("GR1N")(i)
                    Next
                    Session("bAlias") = False
                End If
                'jrs 2011/07/04
                Session.Remove("oGR1N")
                'jrs 2020/12/22
                Session.Remove("bVARN0Loaded")

                Session.Remove("RestorePos")
                Session.Remove("bRank")
                'jrs 2012/01/16
                Session.Remove("bFiltDisplayCount")
                Session.Remove("FilterBackup")
                Session.Remove("FilterLabelBackup")
                Session.Remove("mnuFilter")
                'jrs 2012/02/06
                Session.Remove("bFilterVisible")
                'jrs 2013/02/21
                Session.Remove("oGR4N")
                Session.Remove("UnitCount")
                Session.Remove("sUnitLbl")
                Session.Remove("sUnitLblLong")
                Session.Remove("vUnitFactor")
                Session.Remove("sUnitMenuLbl")
                Session.Remove("UnitIdx")
                Session.Remove("oUnitDisplayName")

                'jrs 2005/10/25
                If Session("TableDisplayParent") = "frmLongPlotproessential.aspx" Then
                    Session("LastYear") = Session("oldLastYear")
                    Session("FirstYear") = Session("oldFirstYear")
                    Session("DisplayLastYear") = Session("LastYear")
                End If
                'jrs 2005/12/07
                Session("Fil_Region") = ""

                'JAM 2016-03-05 Resetting Trillions Values
                Dim rr, cc As Integer
                If Session("bTrillionsOk") Then
                    For cc = 1 To Session("COUNTE")
                        For rr = 1 To DisYear
                            Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'JAM 2016-05-16
                        Next rr
                        Session("GR4N")(cc) = Replace(Session("GR4N")(cc), "Trillion", "Billion")
                    Next cc
                End If
                Session.Remove("bTrillionsOk")
                Session.Remove("fromTableDisp")

                'jrs 2011/04/05
                If objEA.Item.Caption = "Continue" Then
                    Support.FormManager.Show(Me, Session("TableDisplayParent"))
                Else
                    Support.FormManager.Show(Me, "frm_MainMenu.aspx")
                End If
            Case "Refresh"
                mnuRefresh_Click(sender, e)
            Case "Line"
                mnuPELine_Click(sender, e)
            Case "Bar"
                mnuPEBar_Click(sender, e)
            Case "Pie" 'jrs 2016/03/28
                mnuPEPie_Click()
            Case "Display Map" 'jrs 2006/08/24
                LoadMap()
            Case "First Year to Save" 'jrs 2005/05/11
                mnuFirstYearSave_Click()
            Case "Save Normal View"
                mnuSave_Click(sender, e)
                'ttt 2020/09/03 -> OnClick()
                'Case "Save to Clipboard" 'jrs 2020/08/20
                '    mnuSaveClip_Click()
            Case "Save in Panel format"
                mnuSavePanel_Click(sender, e) 'jrs 2014/04/25
            Case "Save in Google format (PDE)" 'jrs 2023/05/26
                mnuSaveGoogle_Click()
            Case "Save All Years."
                mnuSaveTransAllYrs_Click(sender, e)
            Case "Save every 5 Years."
                mnuSaveTrans5_Click(sender, e)
            Case "Save every 10 Years."
                mnuSaveTrans10_Click(sender, e)
            Case "Save All Years"
                mnuSaveDecAll_Click(sender, e)
            Case "Save every 5 Years"
                mnuSaveDec5_Click(sender, e)
            Case "Save every 10 Years"
                mnuSaveDec10_Click(sender, e)
            Case "Percent", "Whole"
                mnuPercent_Click(sender, e)
            Case "None"
                mnuFilterNone_Click(sender, e)
            Case "Set filter parameters"
                mnuFilterParameters_Click(sender, e)
            Case "Apply Min Level Filter"
                mnuFilterMin_Click(sender, e)
            Case "Apply Max Level Filter"
                mnuFilterMax_Click(sender, e)
            Case "Apply Rate Level Filter"
                mnuFilterRate_Click(sender, e)
            Case "Apply Range Level Filter"
                mnuFilterRange_Click(sender, e)
            Case "Years in Avg(" & txtMovAvgYrs.Text & ")"
                'jrs 2012/05/08
                txtMovAvgYrs.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdMovAvgYrsOk.ClientID + "')")
                cswmWebMenu.Visible = False

                frm_MovingAvg.Visible = True
            Case "Discount Rate (" & txtCumDisRate.Text & "%)" 'jrs 2008/08/29
                'jrs 2012/05/08
                txtCumDisRate.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdCumDisRateOk.ClientID + "')")
                cswmWebMenu.Visible = False

                frm_CumDisRate.Visible = True
            Case "Custom Interval" 'jrs 2024/09/04 "Change Display Interval"
                If Session("Interval") = Nothing Then
                    Session("Interval") = 1
                End If
                txtInterval.Text = Session("Interval")
                frm_Interval.Visible = True
                'jrs 2012/05/08
                txtInterval.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdInterval.ClientID + "')")
                cswmWebMenu.Visible = False
                    'jrs 2023/12/08
                'Case "Use Start Year Divisible by 5" 'jrs 2016/03/30
                '    mnuUseStartYrDivby5_Click()
                'Case "Show Extra Decimals" 'jrs 2022/02/24
                '    mnuShowExtraDecimals_Click()
            Case "1" 'jrs 204/09/04
                txtInterval.Text = 1
                cmdInterval_Click(Me, New EventArgs)
            Case "5" 'jrs 204/09/04
                txtInterval.Text = 5
                cmdInterval_Click(Me, New EventArgs)
            Case "10" 'jrs 204/09/04
                txtInterval.Text = 10
                cmdInterval_Click(Me, New EventArgs)
            Case "Define Conversion Factor" 'jrs 2013/02/21
                txtUnitName.Text = Session("GR4N")(1)
                txtUnitFactor.Text = 1
                txtUnitFactor.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdDefineUnits.ClientID + "')")
                cswmWebMenu.Visible = False
                frm_DefineUnits.Visible = True
            Case "On with Discount" 'jrs 2016/04/05
                mnuCumDisOn_Click()
            Case "On"
                If objEA.Item.ID = "mnuCumOn" Then
                    mnuCumOn_Click()
                ElseIf objEA.Item.ID = "mnuDisOn" Then 'jrs 2009/01/22
                    mnuDisOn_Click()
                ElseIf objEA.Item.ID = "mnuCumDisOn" Then 'jrs 2008/08/29
                    mnuCumDisOn_Click()
                ElseIf objEA.Item.ID = "mnuIntAvgOn" Then
                    mnuIntAvgOn_Click()
                ElseIf objEA.Item.ID = "mnuIntTotOn" Then
                    mnuIntTotOn_Click()
                ElseIf objEA.Item.ID = "mnuIntAbsOn" Then
                    mnuIntAbsOn_Click()
                ElseIf objEA.Item.ID = "mnuAliasOn" Then 'jrs 2011/07/01
                    mnuAliasOn_Click()
                ElseIf objEA.Item.ID = "mnuIdxBase1On" Then 'jrs 2012/01/02
                    mnuIdxBase1On_Click()
                ElseIf objEA.Item.ID = "mnuUseStartYrDivby5On" Then 'jrs 2023/12/08 
                    mnuUseStartYrDivby5_Click()
                ElseIf objEA.Item.ID = "mnuShowExtraDecimalsOn" Then 'jrs 2023/12/08 
                    mnuShowExtraDecimals_Click()
                Else 'jrs 2008/06/26
                    mnuMovingAvgOn_Click()
                End If
            Case "Off"
                If objEA.Item.ID = "mnuCumOff" Then
                    mnuCumOff_Click()
                ElseIf objEA.Item.ID = "mnuDisOff" Then 'jrs 2009/01/22
                    mnuDisOff_Click()
                ElseIf objEA.Item.ID = "mnuCumDisOff" Then 'jrs 2008/08/29
                    mnuCumDisOff_Click()
                ElseIf objEA.Item.ID = "mnuIntAvgOff" Then
                    mnuIntAvgOff_Click()
                ElseIf objEA.Item.ID = "mnuIntTotOff" Then
                    mnuIntTotOff_Click()
                ElseIf objEA.Item.ID = "mnuIntAbsOff" Then
                    mnuIntAbsOff_Click()
                ElseIf objEA.Item.ID = "mnuAliasOff" Then 'jrs 2011/07/01
                    mnuAliasOff_Click() 'jrs 2021/02/08
                ElseIf objEA.Item.ID = "mnuIdxBase1Off" Then 'jrs 2012/01/02
                    mnuIdxBase1Off_Click()
                ElseIf objEA.Item.ID = "mnuUnitOff" Then 'jrs 2012/01/02
                    mnuUnitOff_Click()
                ElseIf objEA.Item.ID = "mnuUseStartYrDivby5Off" Then 'jrs 2023/12/08 
                    mnuUseStartYrDivby5_Click()
                ElseIf objEA.Item.ID = "mnuShowExtraDecimalsOff" Then 'jrs 2023/12/08 
                    mnuShowExtraDecimals_Click()
                Else 'jrs 2008/06/26
                    mnuMovingAvgOff_Click()
                End If
            Case "Rank" 'jrs 2008/06/09
                mnuRank_Click()
            Case "Select Rank Year"
                mnuRankYear_Click()
            Case "Ascending"
                mnuAscending_Click()
            Case "Descending"
                mnuDescending_Click()
            Case "No Rank" 'jrs 2011/07/04
                mnuNoRank_Click()
            Case "Percent of Total" 'jrs 2009/01/15
                mnuPctTotal_Click()
            Case "Ratio Comparison (a/b)"
                mnuCompRatAB_Click()
            Case "Ratio Comparison (b/a)"
                mnuCompRatBA_Click()
            Case "Difference Comparison (a-b)"
                mnuCompDiffAB_Click()
            Case "Difference Comparison (b-a)"
                mnuCompDiffBA_Click()
            Case "Scenario Ratio Comparison (a/(b...n))"
                mnuCompRatAOther_Click()
            Case "Scenario Ratio Comparison ((b...n)/a)"
                mnuCompRatOtherA_Click()
            Case "Scenario Difference Comparison (a-(b...n))"
                mnuCompDiffAOther_Click()
            Case "Scenario Difference Comparison ((b...n)-a)"
                mnuCompDiffOtherA_Click()
            Case "Percent Comparison (a-b)/b*100"
                mnuCompPctAB_Click()
            Case "Percent Comparison (b-a)/a*100"
                mnuCompPctBA_Click()
            Case "Reset"
                If objEA.Item.ID = "mnuPctTotReset" Then
                    mnuPctTotReset_Click()
                Else
                    mnuCompReset_Click()
                End If
            Case "Set Display Horizon"
                mnuDisplayRunHorizon_Click(sender, e)
            Case Else 'jrs 2013/02/21 for Convert Units
                If Left(objEA.Item.Caption, 7) = "Convert" Then
                    Dim idx As Integer
                    idx = CInt(Replace(objEA.Item.ID, "mnuUnitOn", ""))
                    mnuUnitOn_Click(idx)
                End If
        End Select
    End Sub

    'jrs 2005/05/10
    Private Sub mnuCumOff_Click()
        'jrs 2016/04/05
        If Session("bCumDis") Then
            mnuCumDisOff_Click()
        End If
        'jrs 2005/05/17
        If (Session("bCum") = True) Then
            'jrs 2009/01/30 
            ''jrs 2008/06/27
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = True
            ''jrs 2009/01/22
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = True
            If CType(Session("TblDispOpt"), Stack).Peek <> "Cumulative" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumOff").IconImage = "checkbox.gif"
            Session("bCum") = False

            'jrs 2009/01/30
            ''jrs 2005/05/23
            'Dim bPct As Boolean
            'bPct = CBool(Session("PercentOn"))
            'If bPct Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            Dim cc As Integer, rr As Integer, lastNonNull As Integer
            lastNonNull = -1
            If IsNumeric(Session("PLOT")(DisYear + 1, cc)) Then lastNonNull = DisYear + 1
            For cc = 1 To Session("COUNTE")
                For rr = DisYear + 1 To 2 Step -1
                    'jrs 2005/05/20
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        If lastNonNull > rr + 1 Then
                            Session("PLOT")(lastNonNull, cc) = Val(Session("PLOT")(lastNonNull, cc)) - Val(Session("PLOT")(rr, cc))
                            'jrs 2009/01/30 SPLOT(lastNonNull, cc) = Val(SPLOT(lastNonNull, cc)) - Val(SPLOT(rr, cc))
                        End If

                        If IsNumeric(Session("PLOT")(rr - 1, cc)) Then
                            Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) - Val(Session("PLOT")(rr - 1, cc))
                            'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) - Val(SPLOT(rr - 1, cc))
                        End If
                        lastNonNull = rr
                    End If
                Next rr
                'JRS 2005/08/04
                If IsNumeric(Session("PLOT")(1, cc)) Then
                    If lastNonNull > 2 Then
                        Session("PLOT")(lastNonNull, cc) = Val(Session("PLOT")(lastNonNull, cc)) - Val(Session("PLOT")(1, cc))
                        'jrs 2009/01/30 SPLOT(lastNonNull, cc) = Val(SPLOT(lastNonNull, cc)) - Val(SPLOT(1, cc))
                    End If
                End If
            Next cc

            'jrs 2009/01/30 
            'If bPct Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    'jrs 2005/05/10
    Private Sub mnuCumOn_Click()
        'jrs 2016/04/05
        If Session("bCumDis") Then
            mnuCumDisOff_Click()
        End If

        'jrs 2005/05/17
        If (Session("bCum") = False) Then
            'jrs 2009/01/30 
            ''jrs 2005/09/26 going from pct to cum is a problem
            'Dim bPct As Boolean
            'If Session("PercentOn") = "True" Then
            '    bPct = True
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            ''jrs 2008/06/27
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = False
            ''jrs 2009/01/22
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = False
            CType(Session("TblDispOpt"), Stack).Push("Cumulative")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumOff").IconImage = "blank.gif"
            Session("bCum") = True

            Dim cc As Integer, rr As Integer, lastNonNull As Integer
            For cc = 1 To Session("COUNTE")
                lastNonNull = -1
                If IsNumeric(Session("PLOT")(1, cc)) Then lastNonNull = 1
                For rr = 2 To DisYear + 1
                    'jrs 2005/05/20
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        If IsNumeric(Session("PLOT")(rr - 1, cc)) Then
                            Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) + Val(Session("PLOT")(rr - 1, cc))
                            'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) + Val(SPLOT(rr - 1, cc))
                        Else
                            If lastNonNull > 0 Then
                                Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) + Val(Session("PLOT")(lastNonNull, cc))
                                'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) + Val(SPLOT(lastNonNull, cc))
                            End If
                        End If
                        lastNonNull = rr
                    End If
                Next rr
            Next cc

            'jrs 2009/01/30 
            ''jrs 2005/09/26
            'If bPct Then
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    'jrs 2005/05/10
    Private Sub mnuFirstYearSave_Click()
        cmb_FY.Visible = True
        frm_FY.Visible = True
        Dim ICount As Integer
        For ICount = Session("FirstYear") To Session("LastYearForecast")
            cmb_FY.Items.Add(ICount)
        Next ICount
        cmb_FY.SelectedValue = iFirstYearSave
        'jrs2012/05/08 WinFormsToWeb_FocusControl.Focus("cmb_FY")
        'jrs 2012/05/08
        txtFY.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdFY.ClientID + "')")
        cswmWebMenu.Visible = False

    End Sub

    'jrs 2005/05/10
    Private Sub cmb_FY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_FY.SelectedIndexChanged
        iFirstYearSave = cmb_FY.SelectedValue
        frm_FY.Visible = False
        Session("iFirstYearSave") = iFirstYearSave
        'jrs 2012/05/08
        cswmWebMenu.Visible = True
    End Sub

    'jrs 2005/08/19
    'Subroutine to load decomposed computed variable across regions of a specific group
    'col is the index of the formula
    'GmemCount is the number of members of the group
    'sReg is the current Region
    Private Sub DecComp(ByVal col As Integer, ByVal sReg As String, ByVal sDimen2 As String, ByVal sDimen3 As String, ByVal f As Formula, ByVal bHF As Boolean, ByVal FileNum As Integer)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))
        'jrs 10/27/04 add percentage filtering
        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")
        'jrs 10/27/04 move to whole for loading
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If

        gLoadComp(Session("COUNTE") + 1, f.Formula_Renamed, f.Name, sReg, sDimen2, sDimen3, f.Unit, -1, False, FileNum, bHF, f.Formula2 > "", f, Session("bFillHis"),
        Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), False, Session("bComputation"), Session("FirstYear"),
        Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
        Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
        Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2023/09/26 added , dfCol, FilesCol
        FormatFormAfterLoad(FileNum, Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE") + 1),
        IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
        Session("MaxFinYearHis"))

        'jrs 10/27/04 move to pct for filtering
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If
        'jrs 2005/10/12
        Exit Sub

    End Sub

    'jrs 2005/10/12
    Public Sub DecCompHis(ByVal cc As Integer, ByVal sReg As String, ByVal f As Formula, ByRef Rpoints As Integer)
        Dim ly As Integer, fn As Integer
        ly = Session("LastYear")
        'jrs 2024/09/04
        Dim fy As Integer
        fy = Session("FirstYear")

        'jrs 2005/10/12 fn = frm_VarSel.FileNum
        Dim f2 As New Formula
        f2.init(f.Formula2)
        f2.Name = f.Name
        f2.Dimension = f.Dimension
        f2.Dimension2 = f.Dimension2
        f2.Unit = f.Unit
        'jrs 2005/12/20 RAM for Formulas
        Dim ii As Integer
        For ii = 1 To f.disVarHis.disSize
            f2.disVar.setDataDetS(f.disVarHis.display(ii), f.disVarHis.displayDet(ii))
            f2.disVar.setDataDet1S(f.disVarHis.display(ii), f.disVarHis.displayDet1(ii))
            f2.disVar.setDataDet2S(f.disVarHis.display(ii), f.disVarHis.displayDet2(ii))
            f2.disVar.setDataDet3S(f.disVarHis.display(ii), f.disVarHis.displayDet3(ii))
        Next ii

        Dim h As Integer
        h = Session("LastYear") - FirstYearForecast + 1
        Dim PlotCopy2()
        ReDim PlotCopy2(h)
        Dim m As Integer
        For m = 1 To h
            PlotCopy2(m) = Session("PLOT")(m, Session("COUNTE"))
        Next m
        Session("FirstYear") = FirstYearHistory
        Session("LastYear") = LastYearHistory
        DecComp(CInt(cc), sReg, Session("GR2N")(cc), Session("GR3N")(cc), f2, False, -1) 'jrs 2005/10/31
        'Copy Forecast back to Session("PLOT")
        Dim h2 As Integer
        h2 = LastYearHistory - FirstYearHistory + 1
        For m = 1 To h
            Session("PLOT")(m + h2 - 1, Session("COUNTE")) = PlotCopy2(m)
        Next m
        Session("LastYear") = ly
        'Session("FirstYear") = FirstYearForecast
        'jrs 2024/09/04
        Session("FirstYear") = fy
        'jrs 2005/10/12 frm_VarSel.FileNum = fn
        Rpoints = h + h2 - 1
        DisYear = Rpoints
        Session("FinYear")(Session("COUNTE")) = Rpoints
        'jrs 2005/10/12 frm_VarSel.DoingHistory = False
    End Sub

    'jrs 2005/10/14
    Private Sub mnuIntAvgOn_Click()
        If (Session("bIntAvg") = False) Then
            'jrs 2009/01/30
            ''jrs 2008/06/27
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = True
            ''jrs 2009/01/22
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = False
            ''This operation shouldn't be allowed when Cumulative On
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Enabled = False
            CType(Session("TblDispOpt"), Stack).Push("Interval Avg")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Group.Items("mnuIntAvgOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Group.Items("mnuIntAvgOff").IconImage = "blank.gif"
            Session("bIntAvg") = True

            Dim cc As Integer, rr As Integer, i As Integer, x As Object, counter As Integer
            For cc = 1 To Session("COUNTE")
                'jrs 2009/01/30
                'If Session("PercentOn") = "False" Then
                '    For rr = 1 To DisYear
                '        SPLOT(rr, cc) = Session("PLOT")(rr, cc)
                '    Next rr
                'End If
                For rr = 1 To DisYear
                    Session("IntervalAvg_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
                Next rr

                For rr = 1 + Session("Interval") To DisYear Step Session("Interval")
                    x = 0
                    counter = 0
                    For i = rr To rr - Session("Interval") + 1 Step -1
                        If IsNumeric(Session("PLOT")(i, cc)) Then
                            x = x + Session("PLOT")(i, cc)
                            counter = counter + 1
                        End If
                    Next i
                    If counter > 0 Then
                        Session("PLOT")(rr, cc) = x / counter
                    End If
                Next rr
                'jrs 2005/12/11
                If (DisYear - 1) Mod Session("Interval") > 1 Then
                    x = 0
                    counter = 0
                    For i = DisYear To DisYear - ((DisYear - 1) Mod Session("Interval")) + 1 Step -1
                        If IsNumeric(Session("PLOT")(i, cc)) Then
                            x = x + Session("PLOT")(i, cc)
                            counter = counter + 1
                        End If
                    Next
                    If counter > 0 Then
                        Session("PLOT")(DisYear, cc) = x / counter
                    End If
                End If
            Next cc

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    'jrs 2005/10/14
    Private Sub mnuIntAvgOff_Click()
        If (Session("bIntAvg") = True) Then
            'jrs 2009/01/30
            'Dim bPct As Boolean
            'If Session("PercentOn") = "True" Then
            '    bPct = True
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            ''jrs 2008/06/27
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = True
            ''jrs 2009/01/22
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Enabled = True
            If CType(Session("TblDispOpt"), Stack).Peek <> "Interval Avg" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Group.Items("mnuIntAvgOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Group.Items("mnuIntAvgOff").IconImage = "checkbox.gif"
            Session("bIntAvg") = False

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    'jrs 2009/01/30 Session("PLOT")(rr, cc) = SPLOT(rr, cc)
                    Session("PLOT")(rr, cc) = Session("IntervalAvg_PLOT")(rr, cc)
                Next rr
            Next cc

            'jrs 2009/01/30
            'If bPct Then
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            RefreshTable()
        End If
    End Sub

    'jrs 2005/10/31
    Private Sub RefreshCol(ByVal cc As Integer)
        Dim st As Integer, en As Integer, fn As Integer
        With grd_TableDisplay
            .Rows(0).Cells(cc).Text = RTrim(Session("VARN")(1, cc))
            .Rows(1).Cells(cc).Text = Session("GR1N")(cc)
            .Rows(2).Cells(cc).Text = Session("GR2N")(cc)
            .Rows(3).Cells(cc).Text = Session("GR3N")(cc)
            If Session("PercentOn") = "False" Then
                .Rows(4).Cells(cc).Text = Session("GR4N")(cc)
            Else
                .Rows(4).Cells(cc).Text = "% Change"
            End If
            st = InStr(Session("VARN")(1, cc), "[") + 1
            en = InStr(Session("VARN")(1, cc), "]") + 1
            If st > 0 And en > st Then
                If IsNumeric(Mid(Session("VARN")(1, cc), st, en - st - 1)) Then
                    fn = Int(Mid(Session("VARN")(1, cc), st, en - st - 1))
                    If fn <= UBound(Session("ShortSce")) And fn > -1 Then
                        .Rows(5).Cells(cc).Text = Session("ShortSce")(fn)
                    ElseIf fn = -1 Then
                        .Rows(5).Cells(cc).Text = "Historical"
                    End If
                End If
            Else
                st = InStr(Session("VARN")(0, cc), "[") + 1
                en = InStr(Session("VARN")(0, cc), "]") + 1
                If st > 0 And en > st Then
                    .Rows(5).Cells(cc).Text = Mid(Session("VARN")(0, cc), st, en - st - 1)
                End If
            End If
        End With
    End Sub

    'jrs 2005/10/31
    Private Sub MoveColLbl(ByVal cc As Integer, ByVal icol As Integer)
        'jrs 2005/10/31 when using more than 1 formula
        'the data moves to the last column in plot
        'but the display is not refreshed
        'refreshing grid for current column
        With grd_TableDisplay
            Dim ss As String
            Dim rr As Integer
            For rr = 0 To 5
                ss = .Rows(rr).Cells(cc).Text
                .Rows(rr).Cells(icol).Text = ss
            Next
        End With
    End Sub

    'jrs 2012/01/27 removed ByVal GraphOutputFileName As String, ByRef fNum As Integer to avoid creating file in server (concurrency problems)
    'jrs 2006/02/06
    Private Sub SaveDec(ByVal Interval As Integer, ByVal Rpoints As Integer)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'jrs 2006/04/20
        Dim iRowPosition As Integer, iFY As Integer
        iRowPosition = 1
        iFY = Session("FirstYear")
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
            iFY = FirstYearForecast
            iRowPosition = LastYearHistory - FirstYearHistory + 1
        End If

        'jrs 2006/05/29 dim3 'jrs 2005/07/18
        Dim pCopy As Object, VARNCopy As Object, gr1Copy As Object, gr2Copy As Object, gr3Copy As Object, gr4Copy As Object, fCopy As Object
        pCopy = VB6.CopyArray(Session("PLOT"))
        VARNCopy = VB6.CopyArray(Session("VARN"))
        gr1Copy = VB6.CopyArray(Session("GR1N"))
        gr2Copy = VB6.CopyArray(Session("GR2N"))
        gr3Copy = VB6.CopyArray(Session("GR3N"))
        gr4Copy = VB6.CopyArray(Session("GR4N"))
        fCopy = VB6.CopyArray(Session("aFormulas"))

        'jrs 2007/02/19
        Dim bPoverty As Boolean
        Dim iPos As Integer, iPos2 As Integer, iPos3 As Integer
        Dim level As Single
        Dim bPctPop As Boolean
        Dim bFind As Boolean
        'jrs 2010/05/14
        Dim bMortChl As Boolean, bMortAdl As Boolean
        'jrs 2014/10/21
        Dim b2005PPP As Boolean, iPos4 As Integer, iPos5 As Integer, level2 As Single, bIncRange As Boolean

        Dim sReg() As String
        Dim cc As Integer
        Dim f As Formula
        For cc = 0 To Session("COUNTE")
            'jrs 2005/07/18
            If UBound(Session("aFormulas")) >= cc Then
                If Not (Session("aFormulas")(cc) Is Nothing) Then
                    If Session("VARN")(1, cc) = Session("aFormulas")(cc).Name Then
                        bUsingComp = True
                        f = Session("aFormulas")(cc)
                    Else
                        bUsingComp = False
                    End If
                Else
                    bUsingComp = False
                End If
            Else
                bUsingComp = False
            End If
            'check if column is group
            sReg = fLoadCountries(Session("oGR1N")(cc), Session("IFs_MDB2Active"), Session("gCountGroupMembers")) 'jrs 2016/05/27 changed to oGR1N
            'jrs 2008/07/08 Groups by Groups can't be decomposed
            If Session("gCountGroupMembers") > 0 And Session("GR2N")(cc) > "" Then
                Dim sReg2 As String(), iCount As Integer
                sReg2 = fLoadCountries(Session("GR2N")(cc), Session("IFs_MDB2Active"), iCount)
                If iCount > 0 Then
                    Session("gCountGroupMembers") = 0
                End If
            End If

            If Session("gCountGroupMembers") > 0 Then
                'jrs 2007/02/19
                iPos = InStr(Session("VARN")(1, cc), "INCOMELT")
                iPos2 = InStr(Session("VARN")(1, cc), "LN")
                iPos3 = InStr(Session("VARN")(1, cc), "%")
                bPoverty = False
                bPctPop = False
                bFind = False
                b2005PPP = False
                bIncRange = False
                iPos4 = InStr(Session("VARN")(1, cc), "GT")
                iPos5 = InStr(Session("VARN")(1, cc), "2011")

                If iPos > 0 And iPos2 > 0 Then
                    bFind = (iPos3 > 0 And iPos3 < iPos2)
                    If bFind Then
                        level = Val(Mid(Session("VARN")(1, cc), 9, iPos2 - 10))
                    Else
                        'jrs 2014/10/21
                        If iPos4 > 0 Then
                            level2 = Val(Mid(Session("VARN")(1, cc), 9, iPos4 - 9))
                            'jrs 2013/03/12 level = Val(mid(sVar, iPos4 + 2, iPos2 - 11 - Len(CStr(level))))
                            level = Val(Mid(Session("VARN")(1, cc), iPos4 + 2, iPos2 - 11 - Len(CStr(level2))))
                            bIncRange = True
                            If InStr(Session("VARN")(1, cc), "%") > 0 Then
                                bPctPop = True
                            End If
                        Else
                            level = Val(Mid(Session("VARN")(1, cc), 9, iPos2 - 9))
                            'jrs 2020/04/06
                            'If level <> 1 And level <> 2 Then
                            If level <> 1.25 And level <> 2 And level <> 110 And level <> 190 And level <> 310 Then
                                bPoverty = True
                            End If
                            If InStr(Session("VARN")(1, cc), "%") > 0 Then
                                bPctPop = True
                            End If
                        End If
                    End If
                End If
                'jrs 2014/10/21
                If iPos5 > iPos2 Then
                    b2005PPP = True
                End If
                'jrs 2010/05/14
                bMortChl = (InStr(Session("VARN")(1, cc), "POPCHLDMOR") > 0)
                bMortAdl = (InStr(Session("VARN")(1, cc), "POPADLTMOR") > 0)

                Dim idx As Integer
                Dim sAgg As String
                Dim sVar As String
                Dim sDef As String
                Dim sUn As String
                Dim iFileNum As Integer
                Dim Factor As Single
                'jrs 2005/07/18
                If Not bUsingComp Then '**************** NO Computations
                    sVar = Mid(Session("VARN")(1, cc), 1, InStr(Session("VARN")(1, cc), "[") - 1)
                    iFileNum = Int(Mid(Session("VARN")(1, cc), InStr(Session("VARN")(1, cc), "[") + 1, InStr(Session("VARN")(1, cc), "]") - InStr(Session("VARN")(1, cc), "[") - 1))
                    If iFileNum >= 0 Then
                        idx = IfsVars.Item(sVar).Index
                        'jrs 2007/02/19
                        If bPoverty Or bFind Then
                            idx = IfsVars.Item("INCOMELT190LN").Index 'jrs 2016/10/12 changed to 190
                        ElseIf bPctPop Then
                            idx = IfsVars.Item(Replace(sVar, "%", "")).Index
                        ElseIf bMortChl Or bMortAdl Then
                            idx = IfsVars.Item("DEATHCAT").Index
                        End If

                        If bFind Then
                            sAgg = "POP"
                            Factor = CurrencyFct
                        Else
                            sAgg = VAggregation(idx)
                            If VCurrency(idx) Then
                                Factor = CurrencyFct
                            Else
                                Factor = 1
                            End If
                        End If

                    End If
                    'jrs 2005/10/31
                Else
                    iFileNum = getFileNum(f.disVar.displayDet(1), Session("ShortSce")) 'JRS 2006/02/02 session("VARN")(0, cc))
                    'JRS 2006/10/31
                    idx = 0
                End If '**************** NO Computations

                'JRS 10/19/03
                'jrs 2012/01/27 PrintLine(fNum, "")
                Context.Response.Write(Environment.NewLine)

                'JRS 01/15/04
                Dim reg As Integer, ii As Integer
                'Retrieve values for every member in group
                For ii = 0 To Session("gCountGroupMembers") - 1
                    reg = IFsRegs.Item(sReg(ii)).Index
                    'jrs 2005/07/18
                    If bUsingComp Then 'Using Computations
                        'jrs 2005/10/11
                        Dim bHF As Boolean
                        bHF = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory And f.Formula2 = "")
                        DecComp(CInt(cc), sReg(ii), Session("GR2N")(cc), Session("GR3N")(cc), f, bHF, iFileNum) 'jrs 2005/10/31
                        'jrs 2005/10/10
                        If f.Formula2 > "" Then
                            DecCompHis(cc, sReg(ii), f, Rpoints)
                        End If
                    Else 'No Computations
                        If iFileNum >= 0 Then
                            'JRS 2007/02/19
                            If bPoverty Or bPctPop Then
                                Session("COUNTE") = Session("COUNTE") + 1
                                gLoadPovertyDisp(level, sReg(ii), iFileNum, False, bPctPop, b2005PPP,
                                Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                Session("SensitivityFlag"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                                Session("COUNTE") = Session("COUNTE") - 1
                            ElseIf bFind Then
                                Session("COUNTE") = Session("COUNTE") + 1
                                gFindPovertyLevel(sReg(ii), iFileNum, False, level, b2005PPP,
                                Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/06
                                Session("COUNTE") = Session("COUNTE") - 1
                                'jrs 2010/05/14
                            ElseIf bMortChl Then
                                Session("COUNTE") = Session("COUNTE") + 1
                                'gLoadMortProbChldL5(COUNTE, "", Nothing, True, True, sReg(ii), reg, False, GR2N(cc), GR3N(cc), _
                                'PLOT, GR1N(COUNTE), GR1N(COUNTE), GR2N(COUNTE), GR3N(COUNTE), VARN(1, COUNTE), VARN(0, COUNTE), _
                                'GR4N(COUNTE), "", "", iFileNum, False, True)
                                Session("COUNTE") = Session("COUNTE") - 1
                            ElseIf bMortAdl Then
                                Session("COUNTE") = Session("COUNTE") + 1
                                'gLoadMortProbAdlt(COUNTE, "", Nothing, True, True, sReg(ii), reg, False, GR2N(cc), GR3N(cc), _
                                'PLOT, GR1N(COUNTE), GR1N(COUNTE), GR2N(COUNTE), GR3N(COUNTE), VARN(1, COUNTE), VARN(0, COUNTE), _
                                'GR4N(COUNTE), "", "", iFileNum, False, True)
                                Session("COUNTE") = Session("COUNTE") - 1
                            Else
                                gLoadForDisplayNET(sVar, idx, sReg(ii), reg, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1, "", iFileNum, True, True, False,
                                1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + CStr(iFileNum)), Session("df" + sVar + CStr(iFileNum))) 'jrs 2023/02/02 added file and data frames to optimize peformance
                                'gLoadForDisplay(sVar, idx, sReg(ii), reg, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1, "", iFileNum, True, True, False,
                                '1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                                'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                                gLoadForDisplaySaveLbls(Session("COUNTE") + 1, idx, sReg(ii), Session("GR2N")(cc), Session("GR3N")(cc), False, iFileNum, "",
                                Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                                '*******************************************
                                If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                    'jrs 2006/06/08 lastYearHistory - 1 in order to keep forecast value for year 2000 instead of historic
                                    gLoadForDisplayHis(sVar, sReg(ii), reg, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1,
                                    False, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1,
                                    False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                                    Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                    Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                End If
                                '*******************************************
                            End If
                        Else
                            gLoadForDisplayHis(sVar, sReg(ii), reg, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                            False, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, Session("LastYear"),
                            False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                            Session("LastYearForecast"), Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                            gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, sReg(ii), False, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                            Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                        End If
                    End If

                    'jrs 2005/09/26
                    Dim icol As Integer
                    icol = cc

                    'jrs 2005/07/18
                    If bUsingComp Then
                        icol = Session("COUNTE")
                        'jrs 2005/10/31
                        MoveColLbl(cc, Session("COUNTE"))
                    End If

                    'jrs 2012/01/27 removed fNum
                    LoadGroupMembers(sReg(ii), icol, Rpoints, Interval) 'jrs 2005/09/26 replace COUNTE + 1 with new variable iCol to avoid problems when is not a formula 'jrs 2005/09/07 change cc with COUNTE, because the formula is placed always at the end of the vector

                Next ii
            End If
            If Session("gCountGroupMembers") > 0 Then
                'jrs 2005/07/18
                If bUsingComp Then
                    Session("PLOT") = VB6.CopyArray(pCopy)
                    Session("VARN") = VB6.CopyArray(VARNCopy)
                    Session("GR1N") = VB6.CopyArray(gr1Copy)
                    Session("GR2N") = VB6.CopyArray(gr2Copy)
                    Session("GR3N") = VB6.CopyArray(gr3Copy)
                    Session("GR4N") = VB6.CopyArray(gr4Copy)
                    Session("aFormulas") = VB6.CopyArray(fCopy)
                    'jrs 2005/10/31 *************** BEGIN **********************
                    'JRS 2006/10/31 If cc = Session("COUNTE") - 1 Then
                    'JRS 2006/10/31 RefreshCol(cc + 1)
                    RefreshCol(Session("COUNTE"))
                    'JRS 2006/10/31 End If
                    'jrs 2005/10/31 *************** END **********************
                End If

                'jrs 2012/01/27 removed fNum
                LoadColumn(cc, Rpoints, Interval, True)
                'jrs 2012/01/27 PrintLine(fNum, "")
                Context.Response.Write(Environment.NewLine)
            Else
                'jrs 2012/01/27 removed fNum
                LoadColumn(cc, Rpoints, Interval, False)
            End If
        Next cc

        'jrs 2012/01/27 FileClose(fNum)

        'jrs 2005/10/31
        grd_TableDisplay.Visible = True

        'JAM 2012-04-03
        Call BuildTable()

        'JRS 2006/02/16
        RefreshCol(Session("COUNTE"))

    End Sub

    'jrs 2006/08/23
    'Procedure to Load Map
    Private Sub LoadMap()
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim sDef As String, sUn As String

        'Define filter variables
        Dim fileIdx As Integer
        fileIdx = InStr(1, Session("VARN")(1, 1), "[")
        If fileIdx > 0 Then
            Session("Fil_VarName") = Left(Session("VARN")(1, 1), fileIdx - 1)
            Session("Fil_File") = Val(Mid(Session("VARN")(1, 1), fileIdx + 1, Len(Session("VARN")(1, 1)) - fileIdx - 1))
        Else
            Session("Fil_VarName") = Session("VARN")(1, 1)
            'JRS 04/14/03
            bUsingComp = Not (Session("aFormulas")(1) Is Nothing)
            'jrs 2006/01/22
            If bUsingComp Then
                If Session("aFormulas")(1).Name <> Session("VARN")(1, 1) Then
                    bUsingComp = False
                End If
            End If
            'JRS 04/07/03
            If Session("FirstYear") = FirstYearHistory Then
                Session("Fil_File") = -1
            Else
                Session("Fil_File") = 0
                'jrs 2006/08/22 Handling Files with formulas
                If bUsingComp Then
                    fileIdx = InStr(1, Session("aFormulas")(1).disVar.displayDet(1), "[")
                    Session("Fil_File") = Val(Mid(Session("aFormulas")(1).disVar.displayDet(1), fileIdx + 1, Len(Session("aFormulas")(1).disVar.displayDet(1)) - fileIdx - 1))
                End If
            End If
        End If
        If Session("Fil_Region") = "" Then
            Session("Fil_Region") = Session("GR1N")(1)
            Session("Fil_Dim2") = Session("GR2N")(1)
            Session("Fil_Dim3") = Session("GR3N")(1)
        End If
        If Session("Fil_Region") = "" Then
            LoadMessage("Map only works for Variables where the first dimension is Region/Country or Group.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim i As Integer
        bFilter_Group = False

        'jrs 2018/08/15
        Dim fTmp As Formula
        If bUsingComp Then
            fTmp = Session("aFormulas")(1)
        End If

        'clear plot
        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        InitializePlot(Session("COUNTE"), Session("PLOT"), Session("FirstYear"), Session("LastYear"))

        Dim bPct As Boolean
        bPct = (Session("PercentOn") = "True")
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
        End If

        If bUsingComp Then
            'jrs 2018/08/15
            Session("aFormulas")(1) = fTmp

            'jrs 2006/02/19
            If Session("aFormulas")(1).Dimension = "" Then
                MsgBox("This Formula can't be used with Map.", vbCritical, "Map")
                Exit Sub
            End If

            Dim fCopy As New Formula
            fCopy = Session("aFormulas")(1)

            For i = 1 To NR
                If Session("aFormulas")(1).Formula2 > "" Then
                    gLoadCompHisFor(Nothing, GRPS(1, i), Session("Fil_Dim2"), Session("Fil_Dim3"), Session("Fil_File"), 1,
                    Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"),
                    Session("aFormulas"), Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me,
                    Session("FinYear"), Session("VARN"), Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                Else
                    Session("COUNTE") = Session("COUNTE") + 1
                    gLoadComp(Session("COUNTE"), Session("aFormulas")(1).Formula_Renamed, Session("aFormulas")(1).Name, GRPS(1, i), Session("Fil_Dim2"), Session("Fil_Dim3"),
                    Session("aFormulas")(1).Unit, -1, bFilter_Group, Session("Fil_File"), (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory),
                    False, fCopy, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), False, Session("bComputation"),
                    Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
                    Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                    Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
                End If
                FormatFormAfterLoad(Session("Fil_File"), Session("VARN"), Session("COUNTE"), Session("aFormulas")(Session("COUNTE")),
                IfsPars.vShortSce, Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("buseAllHis"),
                Session("MaxFinYearHis"))
            Next i

        Else
            'if working with forecast
            If Session("Fil_File") > -1 Then
                Dim idx As Integer
                idx = IfsVars.Item(Session("Fil_VarName")).Index
                'JRS 04/07/03
                If idx = 0 Then
                    idx = -1
                End If

                'jrs 2006/04/19
                Dim iRowPosition As Integer, iFY As Integer
                iRowPosition = 1
                iFY = Session("FirstYear")
                If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                    iFY = FirstYearForecast
                    iRowPosition = LastYearHistory - FirstYearHistory + 1
                End If

                Dim MyFileforDisplay As IStorage
                Dim MyDF As IDataframe

                For i = 1 To NR

                    Session("COUNTE") = Session("COUNTE") + 1
                    'reload plot
                    gLoadForDisplayNET(Session("Fil_VarName"), idx, GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), i = 1, i = NR, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay(Session("Fil_VarName"), idx, GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1, "", Session("Fil_File"), i = 1, i = NR, bFilter_Group, 1, Session("PLOT"), Session("COUNTE"),
                    'iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                    'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    gLoadForDisplaySaveLbls(Session("COUNTE"), idx, GRPS(1, i), Session("Fil_Dim2"), Session("Fil_Dim3"), bFilter_Group, Session("Fil_File"), "",
                    Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                    'jrs 2005/07/25
                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                        'jrs 2006/06/08 lastyearhistory -1 in order to keep forecast value for year 2000
                        gLoadForDisplayHis(Session("Fil_VarName"), GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                        bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                        Session("FinYear")(Session("COUNTE")) = Session("LastYear") - FirstYearHistory + 1
                    End If
                Next i

                'if working with history
            Else
                For i = 1 To NR

                    Session("COUNTE") = Session("COUNTE") + 1
                    'Set region
                    gLoadForDisplayHis(Session("Fil_VarName"), GRPS(1, i), i, Session("Fil_Dim2"), -1, Session("Fil_Dim3"), -1,
                    bFilter_Group, 1, Session("PLOT"), Session("COUNTE"), FirstYearHistory, Session("LastYear"),
                    False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                    Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                    Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                    gLoadForDisplayHisSaveLbls(Session("COUNTE"), Session("Fil_VarName"), GRPS(1, i), bFilter_Group, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                    Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
                Next i

            End If
        End If
        'check if empty
        If Session("COUNTE") = 0 Then
            LoadMessage("No regions found for Map", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        'JRS 10/27/04
        If bPct Then
            mnuPercent_Click(Me, New EventArgs)
            Session("PYear") = 6
        Else
            Session("PYear") = 1
        End If

        Session("RecordSourceDef") = "Map"
        Session("mapParent") = "frm_TableDisplay.aspx"

        Session("MapChosenTable") = "Map"
        Session("MapFieldName") = "StoreData"
        Session("MapVar") = Session("VARN")(1, 1)
        'NumBreaks = 6
        Support.FormManager.Show(Me, "frm_mapViewer.aspx")


    End Sub

    Private Sub mnuRank_Click()
        'jrs 2008/03/21
        'Create an array of columns
        Dim PlotRank() As DisplayColumn
        ReDim PlotRank(Session("COUNTE"))
        Dim i As Integer

        For i = 1 To Session("COUNTE")
            PlotRank(i) = New DisplayColumn
            PlotRank(i).Index = i
            'jrs 2023/11/29 adding fix to
            'Sorting values as text
            If IsNumeric(Session("PLOT")(Session("Rank_Year") - Session("FirstYear") + 1, i)) And Not IsDBNull(Session("PLOT")(Session("Rank_Year") - Session("FirstYear") + 1, i)) Then
                PlotRank(i).Value = CSng(Session("PLOT")(Session("Rank_Year") - Session("FirstYear") + 1, i))
            Else
                PlotRank(i).Value = DBNull.Value
            End If
            'jrs 2010/02/03 to break ties
            PlotRank(i).Value2 = Session("GR1N")(i) & Session("GR2N")(i) & Session("GR3N")(i) & Session("VARN")(1, i)
            'jrs 2011/07/04 save original order
            PlotRank(i).Index2 = Session("RestorePos")(i)
        Next i
        'Sort the array
        QuickSortObj(PlotRank, 1, CInt(Session("COUNTE")), Not Session("RankDescOrd"), True)

        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr1Copy As Object, gr2Copy As Object,
        gr3Copy As Object, gr4Copy As Object, fCopy As Object,
        FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object,
        FDNG1Copy As Object, FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object
        'splot and focus
        Dim PLOT_Pct_Tot_Copy As Object 'jrs 2009/01/30
        Dim Percent_PLOT_Copy As Object, IntervalAvg_PLOT_Copy As Object, MovingAvg_PLOT_Copy As Object
        Dim Validation_PLOT_Copy As Object, CompBackup_Copy As Object, CompLabelBackup_Copy As Object
        'jrs 2011/07/04
        Dim IntervalTot_PLOT_Copy As Object, IntervalAbs_PLOT_Copy As Object

        'jrs 2024/11/25 changed from vb6.copyaray to .DeepClone()
        'pCopy = VB6.CopyArray(Session("PLOT"))
        pCopy = Session("PLOT").Clone()
        'VARNCopy = VB6.CopyArray(Session("VARN"))
        VARNCopy = Session("VARN").Clone()
        gr1Copy = Session("GR1N").Clone()
        gr2Copy = Session("GR2N").Clone()
        gr3Copy = Session("GR3N").Clone()
        gr4Copy = Session("GR4N").Clone()
        fCopy = Session("aFormulas").Clone()

        FDNG1Copy = Session("FDNG1").Clone()
        FDNG2Copy = Session("FDNG2").Clone()
        FDNG3Copy = Session("FDNG3").Clone()
        FDNG4Copy = Session("FDNG4").Clone()

        FileNumbrCopy = Session("FileNumbr").Clone()
        FinYearCopy = Session("FinYear").Clone()
        FinYearHisCopy = Session("FinYearHis").Clone()

        Percent_PLOT_Copy = Session("Percent_PLOT").Clone()
        IntervalAvg_PLOT_Copy = Session("IntervalAvg_PLOT").Clone()
        MovingAvg_PLOT_Copy = Session("MovingAvg_PLOT").Clone()
        Validation_PLOT_Copy = Session("Validation_PLOT").Clone()
        CompBackup_Copy = Session("CompBackup").Clone()
        CompLabelBackup_Copy = Session("CompLabelBackup").Clone()

        PLOT_Pct_Tot_Copy = Session("PLOT_Pct_Tot").Clone()

        'jrs 2011/07/04
        IntervalTot_PLOT_Copy = Session("IntervalTot_PLOT").Clone()
        IntervalAbs_PLOT_Copy = Session("IntervalAbs_PLOT").Clone()

        'Reset Target
        Dim j As Integer, k As Integer
        'jrs 2008/03/24
        Dim focus As Integer
        For i = 1 To Session("COUNTE")
            'source index
            k = PlotRank(i).Index
            'jrs 2010/11/01
            'save position to restore
            Session("RestorePos")(i) = PlotRank(i).Index2
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, i) = pCopy(j, k)
                'jrs 2009/01/30 SPLOT(j, i) = sPLOTCopy(j, k)
                Session("Percent_PLOT")(j, i) = Percent_PLOT_Copy(j, k)
                Session("IntervalAvg_PLOT")(j, i) = IntervalAvg_PLOT_Copy(j, k)
                Session("MovingAvg_PLOT")(j, i) = MovingAvg_PLOT_Copy(j, k)
                Session("Validation_PLOT")(j, i) = Validation_PLOT_Copy(j, k)
                'jrs 2012/07/12
                'Session("CompBackup")(j, i) = CompBackup_Copy(j, k)
                If UBound(Session("CompBackup"), 2) >= Session("COUNTE") * 2 Then
                    Session("CompBackup")(j, 2 * i - 1) = CompBackup_Copy(j, 2 * k - 1)
                    Session("CompBackup")(j, 2 * i) = CompBackup_Copy(j, 2 * k)
                End If


                Session("PLOT_Pct_Tot")(j, i) = PLOT_Pct_Tot_Copy(j, k)
                'jrs 2011/07/04
                Session("IntervalTot_PLOT")(j, i) = IntervalTot_PLOT_Copy(j, k)
                Session("IntervalAbs_PLOT")(j, i) = IntervalAbs_PLOT_Copy(j, k)

            Next j
            'jrs 2012/07/12
            If UBound(Session("CompBackup"), 2) >= Session("COUNTE") * 2 Then
                'jrs 2009/01/30
                For j = 1 To 10
                    'jrs 2012/07/12 CompLabelBackup(j, i) = CompLabelBackup_Copy(j, k)
                    Session("CompLabelBackup")(j, 2 * i - 1) = CompLabelBackup_Copy(j, 2 * k - 1)
                    Session("CompLabelBackup")(j, 2 * i) = CompLabelBackup_Copy(j, 2 * k)
                Next j
            End If

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, i) = VARNCopy(j, k)
            Next j
            Session("GR1N")(i) = gr1Copy(k)
            Session("GR2N")(i) = gr2Copy(k)
            Session("GR3N")(i) = gr3Copy(k)
            Session("GR4N")(i) = gr4Copy(k)
            If k <= UBound(fCopy) Then
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(i)
                End If
                Session("aFormulas")(i) = fCopy(k)
            End If
            Session("FDNG1")(i) = FDNG1Copy(k)
            Session("FDNG2")(i) = FDNG2Copy(k)
            Session("FDNG3")(i) = FDNG3Copy(k)
            Session("FDNG4")(i) = FDNG4Copy(k)
            Session("FileNumbr")(i) = FileNumbrCopy(k)
            Session("FinYear")(i) = FinYearCopy(k)
            Session("FinYearHis")(i) = FinYearHisCopy(k)
        Next i
        'Refresh
        RefreshTable()
        'Put numbers
        'grd_TableDisplay.Rows
        With grd_TableDisplay
            'jrs 2024/11/25 For i = 1 To Session("COUNTE")
            For i = 1 To .Rows(1).Cells.Count - 1
                .Rows(1).Cells(i).Text = i & "-" & .Rows(1).Cells(i).Text
            Next i
            '    'jrs 2008/03/24
            '    If focus > 0 Then
            '        .col = focus
            '        .row = 0
            '        .RowSel = .FixedRows - 1
            '        If focus > 5 Then
            '            .LeftCol = focus - 5
            '        Else
            '            .LeftCol = 1
            '        End If
            '    End If
        End With
        'JAM 2012-05-06 Adding Rank Options
        Call BuildTable()

        'jrs 2011/07/04
        If Session("bRank") <> True Then
            Session("bRank") = True
            Dim cswmItem As Coalesys.WebMenu.Item
            cswmItem = cswmWebMenu.Groups("Rank Options").Items.Add()
            cswmItem.Caption = "No Rank"
        End If
    End Sub

    Private Sub mnuRankYear_Click()
        'jrs 2008/03/21
        cbo_RY.Visible = True
        frm_RankYear.Visible = True
        Dim ICount As Integer
        Dim ly As Integer
        If Session("DisplayLastYear") > 0 Then
            ly = Session("DisplayLastYear")
        Else
            ly = Session("LastYearForecast")
        End If
        cbo_RY.Items.Clear()
        For ICount% = Session("FirstYear") To ly Step Session("Interval")
            cbo_RY.Items.Add(ICount%)
        Next ICount%
        'jrs 2023/11/23
        If Session("Rank_Year") < cbo_RY.Items(0).Text Then
            cbo_RY.SelectedIndex = 0
        Else
            cbo_RY.Text = Session("Rank_Year")
        End If
        'jrs 2012/05/08
        txtRank.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdRank.ClientID + "')")
        cswmWebMenu.Visible = False

    End Sub

    Protected Sub cbo_RY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbo_RY.SelectedIndexChanged
        'jrs 2008/03/21
        Session("Rank_Year") = cbo_RY.SelectedValue
        frm_RankYear.Visible = False
        'jrs 2012/05/08
        cswmWebMenu.Visible = True
        'jrs 2013/05/22
        mnuRank_Click()
    End Sub

    Private Sub mnuAscending_Click()
        cswmWebMenu.Groups("RankOpt").Items("Order").Group.Items("Asc").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("RankOpt").Items("Order").Group.Items("Desc").IconImage = "blank.gif"
        Session("RankDescOrd") = False
    End Sub

    Private Sub mnuDescending_Click()
        cswmWebMenu.Groups("RankOpt").Items("Order").Group.Items("Asc").IconImage = "blank.gif"
        cswmWebMenu.Groups("RankOpt").Items("Order").Group.Items("Desc").IconImage = "checkbox.gif"
        Session("RankDescOrd") = True
    End Sub

    'jrs 2008/06/26
    Private Sub mnuMovingAvgOff_Click()
        If Session("bMovingAvg") = True Then
            'jrs 2009/01/30
            'Dim bPct As Boolean
            'If Session("PercentOn") = "True" Then
            '    bPct = True
            '    mnuPercent_Click(Me, New EventArgs)
            'End If
            ''jrs 2009/01/22
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = True
            If CType(Session("TblDispOpt"), Stack).Peek <> "Moving Avg" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Group.Items("mnuMovingAvgOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Group.Items("mnuMovingAvgOff").IconImage = "checkbox.gif"
            Session("bMovingAvg") = False

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    'jrs 2009/01/30 Session("PLOT")(rr, cc) = SPLOT(rr, cc)
                    Session("PLOT")(rr, cc) = Session("MovingAvg_PLOT")(rr, cc)
                Next rr
            Next cc
            'jrs 2009/01/30
            'If bPct Then
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            RefreshTable()
        End If
    End Sub

    'jrs 2008/06/26
    Private Sub mnuMovingAvgOn_Click()
        If Session("bMovingAvg") = False Then
            'jrs 2009/01/30
            ''jrs 2009/01/22
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = False
            ''This operation shouldn't be allowed when Cumulative On, or Interval On
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = False
            CType(Session("TblDispOpt"), Stack).Push("Moving Avg")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Group.Items("mnuMovingAvgOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Group.Items("mnuMovingAvgOff").IconImage = "blank.gif"
            Session("bMovingAvg") = True

            Dim cc As Integer, rr As Integer, i As Integer, X As Object, counter As Integer
            For cc = 1 To Session("COUNTE")
                'jrs 2009/01/30
                'If Session("PercentOn") = "False" Then
                '    For rr = 1 To DisYear
                '        SPLOT(rr, cc) = Session("PLOT")(rr, cc)
                '    Next rr
                'End If
                For rr = 1 To DisYear
                    Session("MovingAvg_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
                Next rr

                Dim ist As Integer, iEn As Integer
                For rr = 1 To DisYear
                    'jrs 2008/07/31
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        X = 0
                        counter = 0
                        ist = rr - CInt((txtMovAvgYrs.Text - 1) / 2)
                        'if the year to use is an even number use an extra year in the past
                        If (txtMovAvgYrs.Text Mod 2 = 0) Then ist = ist - 1
                        If ist < 1 Then ist = 1
                        iEn = rr + CInt((txtMovAvgYrs.Text - 1) / 2)
                        If iEn > DisYear Then iEn = DisYear

                        For i = ist To iEn
                            If IsNumeric(Session("PLOT")(i, cc)) Then
                                X = X + Session("PLOT")(i, cc)
                                counter = counter + 1
                            End If
                        Next i
                        If counter > 0 Then
                            Session("PLOT")(rr, cc) = X / counter
                        End If
                    End If
                Next rr
            Next cc

            RefreshTable()
        End If
    End Sub

    Protected Sub cmdMovAvgYrsOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdMovAvgYrsOk.Click
        If Not IsNumeric(txtMovAvgYrs.Text) Then
            txtMovAvgYrs.Text = 5
        Else
            If CInt(txtMovAvgYrs.Text) < 1 Then
                txtMovAvgYrs.Text = 5
            End If
            txtMovAvgYrs.Text = CInt(txtMovAvgYrs.Text)
        End If
        frm_MovingAvg.Visible = False
        'Refresh if necessary
        If Session("bMovingAvg") = True Then
            grd_TableDisplay.Visible = False
            mnuMovingAvgOff_Click()
            Session("MovAvgYrs") = txtMovAvgYrs.Text
            mnuMovingAvgOn_Click()
            grd_TableDisplay.Visible = True
            'JAM 2012-04-03
            Call BuildTable()
        End If
        Session("MovAvgYrs") = txtMovAvgYrs.Text
        cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Group.Items("mnuMovingAvgYrs").Caption = "Years in Avg(" & txtMovAvgYrs.Text & ")"
        'jrs 2012/05/08
        cswmWebMenu.Visible = True
    End Sub

    'jrs 2008/08/27
    Private Sub mnuCumDisOn_Click()
        'jrs 2016/04/05
        If Session("bCum") Then
            mnuCumOff_Click()
        End If
        'jrs 2005/05/17
        If (Session("bCumDis") = False) Then
            'jrs 2009/01/30
            'Dim bPct As Boolean
            'If Session("PercentOn") = "True" Then
            '    bPct = True
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = False 'jrs 2009/01/22

            CType(Session("TblDispOpt"), Stack).Push("Cumulative Discount")

            'jrs 2016/04/05
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Group.Items("mnuCumDisOn").IconImage = "checkbox.gif"
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Group.Items("mnuCumDisOff").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumDisOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumOff").IconImage = "blank.gif"
            Session("bCumDis") = True
            Session("bCum") = False

            Dim cc As Integer, rr As Integer, lastNonNull As Integer
            For cc = 1 To Session("COUNTE")
                lastNonNull = -1
                If IsNumeric(Session("PLOT")(1, cc)) Then lastNonNull = 1
                For rr = 2 To DisYear + 1
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        If IsNumeric(Session("PLOT")(rr - 1, cc)) Then
                            Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) /
                            (1 + Session("CumDisRatePct") / 100) ^ (rr - 1) +
                            Val(Session("PLOT")(rr - 1, cc))
                            'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) / _
                            '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1) + _
                            'Val(SPLOT(rr - 1, cc))
                        Else
                            If lastNonNull > 0 Then
                                Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) /
                                (1 + Session("CumDisRatePct") / 100) ^ (rr - 1) +
                                Val(Session("PLOT")(lastNonNull, cc))
                                'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) / _
                                '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1) + _
                                'Val(SPLOT(lastNonNull, cc))
                            End If
                        End If
                        lastNonNull = rr
                    End If
                Next rr
            Next cc

            'jrs 2009/01/30 
            ''jrs 2005/09/26
            'If bPct Then
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    'jrs 2008/08/29
    Private Sub mnuCumDisOff_Click()
        If (Session("bCumDis") = True) Then
            'jrs 2009/01/30
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Enabled = True 'jrs 2009/01/22
            If CType(Session("TblDispOpt"), Stack).Peek <> "Cumulative Discount" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            'jrs 2016/04/05
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Group.Items("mnuCumDisOn").IconImage = "blank.gif"
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Group.Items("mnuCumDisOff").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumDisOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumOff").IconImage = "checkbox.gif"
            Session("bCumDis") = False
            Session("bCum") = False

            'jrs 2009/01/30
            'Dim bPct As Boolean
            'bPct = CBool(Session("PercentOn"))
            'If bPct Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            Dim cc As Integer, rr As Integer, lastNonNull As Integer
            lastNonNull = -1
            If IsNumeric(Session("PLOT")(DisYear + 1, cc)) Then lastNonNull = DisYear + 1
            For cc = 1 To Session("COUNTE")
                For rr = DisYear + 1 To 2 Step -1
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        If lastNonNull > rr + 1 Then
                            Session("PLOT")(lastNonNull, cc) = (Val(Session("PLOT")(lastNonNull, cc)) - Val(Session("PLOT")(rr, cc))) *
                            (1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                            'jrs 2009/01/30 SPLOT(lastNonNull, cc) = (Val(SPLOT(lastNonNull, cc)) - Val(SPLOT(rr, cc))) * _
                            '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                        End If

                        If IsNumeric(Session("PLOT")(rr - 1, cc)) Then
                            Session("PLOT")(rr, cc) = (Val(Session("PLOT")(rr, cc)) - Val(Session("PLOT")(rr - 1, cc))) *
                            (1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                            'jrs 2009/01/30 SPLOT(rr, cc) = (Val(SPLOT(rr, cc)) - Val(SPLOT(rr - 1, cc))) * _
                            '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                        End If
                        lastNonNull = rr
                    End If
                Next rr
                If IsNumeric(Session("PLOT")(1, cc)) Then
                    If lastNonNull > 2 Then
                        Session("PLOT")(lastNonNull, cc) = (Val(Session("PLOT")(lastNonNull, cc)) - Val(Session("PLOT")(1, cc))) *
                        (1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                        'jrs 2009/01/30 SPLOT(lastNonNull, cc) = (Val(SPLOT(lastNonNull, cc)) - Val(SPLOT(1, cc))) * _
                        '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                    End If
                End If
            Next cc

            'jrs 2009/01/30 
            'If bPct Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    'jrs 2009/01/22
    Private Sub mnuDisOn_Click()
        If (Session("bDis") = False) Then
            'jrs 2009/01/30 
            'Dim bPct As Boolean
            'If Session("PercentOn") = "True" Then
            '    bPct = True
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = False
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = False

            CType(Session("TblDispOpt"), Stack).Push("Discount")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Group.Items("mnuDisOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Group.Items("mnuDisOff").IconImage = "blank.gif"
            Session("bDis") = True

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 2 To DisYear + 1
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) /
                            (1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                        'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) / _
                        '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                    End If
                Next rr
            Next cc

            'jrs 2009/01/30 
            'If bPct Then
            '    mnuPercent_Click(Me, New EventArgs)
            'End If

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    'jrs 2009/01/22
    Private Sub mnuDisOff_Click()
        If (Session("bDis") = True) Then
            'jrs 2009/01/30 
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuMovingAvg").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAvg").Enabled = True
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuCumDis").Enabled = True
            If CType(Session("TblDispOpt"), Stack).Peek <> "Discount" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Group.Items("mnuDisOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Group.Items("mnuDisOff").IconImage = "checkbox.gif"
            Session("bDis") = False

            'jrs 2009/01/30
            'Dim bPct As Boolean
            'bPct = CBool(Session("PercentOn"))
            'If bPct Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = DisYear + 1 To 2 Step -1
                    If IsNumeric(Session("PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = Val(Session("PLOT")(rr, cc)) *
                            (1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                        'jrs 2009/01/30 SPLOT(rr, cc) = Val(SPLOT(rr, cc)) * _
                        '(1 + Session("CumDisRatePct") / 100) ^ (rr - 1)
                    End If
                Next rr
            Next cc

            'jrs 2009/01/30 
            'If bPct Then
            '    mnuPercent_Click(Me, New System.EventArgs)
            'End If

            RefreshTable()
            'jrs 2009/01/30 Session("SPLOT") = SPLOT
        End If
    End Sub

    Protected Sub cmdCumDisRateOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCumDisRateOk.Click
        If Not IsNumeric(txtCumDisRate.Text) Then
            txtCumDisRate.Text = 3
        Else
            If CInt(txtCumDisRate.Text) < -5 Or CInt(txtCumDisRate.Text) > 20 Then
                txtCumDisRate.Text = 3
            End If
        End If
        frm_CumDisRate.Visible = False
        'Refresh if necessary
        Dim bCumDis As Boolean = Session("bCumDis")
        Dim bDis As Boolean = Session("bDis")

        Do While Session("bCumDis") Or Session("bDis")
            If Session("bCumDis") And CType(Session("TblDispOpt"), Stack).Peek <> "Discount" Then
                grd_TableDisplay.Visible = False
                mnuCumDisOff_Click()
            ElseIf Session("bDis") Then 'jrs 2009/01/22
                grd_TableDisplay.Visible = False
                mnuDisOff_Click()
            End If
        Loop

        Session("CumDisRatePct") = txtCumDisRate.Text

        cswmWebMenu.Groups("mnuDispOpt").Items("mnuCum").Group.Items("mnuCumDisRate").Caption = "Discount Rate (" & txtCumDisRate.Text & "%)"
        'jrs 2009/01/22
        cswmWebMenu.Groups("mnuDispOpt").Items("mnuDis").Group.Items("mnuDisRate").Caption = "Discount Rate (" & txtCumDisRate.Text & "%)"

        'jrs 2009/01/30
        Do While Session("bCumDis") <> bCumDis Or Session("bDis") <> bDis
            If bCumDis And Not Session("bCumDis") And bDis = Session("bDis") Then
                mnuCumDisOn_Click()
                grd_TableDisplay.Visible = True
                'JAM 2012-04-03
                Call BuildTable()
            ElseIf bDis And Not Session("bDis") Then 'jrs 2009/01/22
                mnuDisOn_Click()
                grd_TableDisplay.Visible = True
                'JAM 2012-04-03
                Call BuildTable()
            End If
        Loop
        'jrs 2012/05/08
        cswmWebMenu.Visible = True
    End Sub

    Private Sub mnuPctTotal_Click()
        'jrs 2009/01/15
        If Not Session("bPctTot") Then
            'jrs 2009/01/30 cswmWebMenu.Groups("mnuDispOpt").ShowMenuBarItem = False
            CType(Session("TblDispOpt"), Stack).Push("Percent of Total")
            'jrs 2008/03/24
            ReDim Session("PLOT_Pct_Tot")(UBound(Session("PLOT"), 1), Session("COUNTE"))
            Dim i As Integer, j As Integer
            Dim Tots() As Single 'jrs 2019/10/03 added as Single
            ReDim Tots(UBound(Session("PLOT"), 1))
            For i = 1 To UBound(Session("PLOT"), 1)
                'save original data
                For j = 1 To Session("COUNTE")
                    Session("PLOT_Pct_Tot")(i, j) = Session("PLOT")(i, j)
                    'jrs 2016/02/02
                    If IsNumeric(Session("PLOT")(i, j)) Then
                        Tots(i) = Tots(i) + CSng(Session("PLOT")(i, j)) 'jrs 2019/10/03 added CSng
                    End If
                Next j
                'compute percentage
                For j = 1 To Session("COUNTE")
                    If Tots(i) <> 0 And IsNumeric(Session("PLOT")(i, j)) Then
                        Session("PLOT")(i, j) = Session("PLOT")(i, j) / Tots(i) * 100
                    Else
                        Session("PLOT")(i, j) = DBNull.Value
                    End If
                Next j
            Next i
            'jrs 2023/12/01 moved up before RefreshTable so that label says Pct
            Session("bPctTot") = True
            'Refresh
            RefreshTable()
        End If
    End Sub

    Private Sub mnuPctTotReset_Click()
        'jrs 2009/01/15
        If Session("bPctTot") Then
            'jrs 2008/03/25
            'jrs 2009/01/30 cswmWebMenu.Groups("mnuDispOpt").ShowMenuBarItem = True
            If CType(Session("TblDispOpt"), Stack).Peek <> "Percent of Total" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            Dim i As Integer, j As Integer
            For i = 1 To UBound(Session("PLOT"), 1)
                For j = 1 To Session("COUNTE")
                    'Restore original data
                    Session("PLOT")(i, j) = Session("PLOT_Pct_Tot")(i, j)
                Next j
            Next i
            'jrs 2023/12/01
            Session("bPctTot") = False
            'Refresh
            RefreshTable()
        End If
    End Sub

    'jrs 2009/01/15
    Private Sub mnuCompPctAB_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Percentage"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") Mod 2 = 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"))
                'jam 2012/02/28
                If IsNumeric(Session("CompBackup")(i, 2 * j)) Then
                    If IsNumeric(Session("CompBackup")(i, 2 * j - 1)) Then
                        If Session("CompBackup")(i, 2 * j) <> 0 Then
                            Session("PLOT")(i, j) = (Session("CompBackup")(i, 2 * j - 1) - Session("CompBackup")(i, 2 * j)) / Session("CompBackup")(i, 2 * j) * 100
                        Else
                            Session("PLOT")(i, j) = DBNull.Value
                        End If
                    Else
                        Session("PLOT")(i, j) = DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j) = DBNull.Value
                End If
            Next i
            LabelComparison(j, "%", True)
        Next j
        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
        Session("sComparison") = "PctAB"

        RefreshTable()
    End Sub

    'jrs 2009/01/15
    Private Sub mnuCompPctBA_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Percentage"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") Mod 2 = 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"))
                'jam 2012/02/28
                If IsNumeric(Session("CompBackup")(i, 2 * j - 1)) Then
                    If IsNumeric(Session("CompBackup")(i, 2 * j)) Then
                        If Session("CompBackup")(i, 2 * j - 1) <> 0 Then
                            Session("PLOT")(i, j) = (Session("CompBackup")(i, 2 * j) - Session("CompBackup")(i, 2 * j - 1)) / Session("CompBackup")(i, 2 * j - 1) * 100
                        Else
                            Session("PLOT")(i, j) = DBNull.Value
                        End If
                    Else
                        Session("PLOT")(i, j) = DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j) = DBNull.Value
                End If
            Next i
            'jrs 2023/06/26
            LabelComparison(j, "%", False)
        Next j
        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
        Session("sComparison") = "PctBA"

        RefreshTable()
    End Sub

    'jrs 2009/01/15
    Private Sub mnuCompRatAB_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Ratio"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") Mod 2 = 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"))
                'jam 2012/02/28
                If IsNumeric(Session("CompBackup")(i, 2 * j)) Then
                    If IsNumeric(Session("CompBackup")(i, 2 * j - 1)) Then
                        If Session("CompBackup")(i, 2 * j) <> 0 Then
                            Session("PLOT")(i, j) = Session("CompBackup")(i, 2 * j - 1) / Session("CompBackup")(i, 2 * j)
                        Else
                            Session("PLOT")(i, j) = DBNull.Value
                        End If
                    Else
                        Session("PLOT")(i, j) = DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j) = DBNull.Value
                End If
            Next i
            LabelComparison(j, "/", True)
        Next j
        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
        Session("sComparison") = "RatAB"

        RefreshTable()
    End Sub

    'jrs 2009/01/15
    Private Sub mnuCompRatBA_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Ratio"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") Mod 2 = 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"))
                'jam 2012/02/28
                If IsNumeric(Session("CompBackup")(i, 2 * j - 1)) Then
                    If IsNumeric(Session("CompBackup")(i, 2 * j)) Then
                        If Session("CompBackup")(i, 2 * j - 1) <> 0 Then
                            Session("PLOT")(i, j) = Session("CompBackup")(i, 2 * j) / Session("CompBackup")(i, 2 * j - 1)
                        Else
                            Session("PLOT")(i, j) = DBNull.Value
                        End If
                    Else
                        Session("PLOT")(i, j) = DBNull.Value
                    End If
                Else
                    Session("PLOT")(i, j) = DBNull.Value
                End If
            Next i
            'jrs 2023/06/26
            LabelComparison(j, "/", False)
        Next j
        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
        Session("sComparison") = "RatBA"

        RefreshTable()
    End Sub

    'jrs 2009/01/15
    Private Sub mnuCompDiffAB_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        Session("YAxisLabel") = Session("YAxisLabelOrg")
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") Mod 2 = 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"))
                'jrs 2012/02/06
                If IsNumeric(Session("CompBackup")(i, 2 * j - 1)) And IsNumeric(Session("CompBackup")(i, 2 * j)) Then
                    Session("PLOT")(i, j) = Session("CompBackup")(i, 2 * j - 1) - Session("CompBackup")(i, 2 * j)
                Else
                    Session("PLOT")(i, j) = DBNull.Value
                End If
            Next i
            LabelComparison(j, "-", True)
        Next j
        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
        Session("sComparison") = "DiffAB"

        RefreshTable()
    End Sub

    'jrs 2009/01/15
    Private Sub mnuCompDiffBA_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        Session("YAxisLabel") = Session("YAxisLabelOrg")
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") Mod 2 = 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"))
                'jrs 2023/09/21
                If IsNumeric(Session("CompBackup")(i, 2 * j)) And IsNumeric(Session("CompBackup")(i, 2 * j - 1)) Then
                    Session("PLOT")(i, j) = Session("CompBackup")(i, 2 * j) - Session("CompBackup")(i, 2 * j - 1)
                Else
                    Session("PLOT")(i, j) = DBNull.Value
                End If
            Next i
            'jrs 2023/06/26
            LabelComparison(j, "-", False)
        Next j
        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
        Session("sComparison") = "DiffBA"

        RefreshTable()
    End Sub

    'jrs 2009/01/07
    Private Sub mnuCompReset_Click()
        If Session("bComparisonOn") Then
            'jrs 2019/07/29
            Session("YAxisLabel") = Session("YAxisLabelOrg")

            'jrs 2009/01/30
            ''check other display options, need to be off
            'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
            'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
            '    'MsgBox("All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options")
            '    Exit Sub
            'End If
            If CType(Session("TblDispOpt"), Stack).Peek <> "Comparison" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            Session("bComparisonOn") = False
            ResetForComparison()
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"
            'jrs 2012/02/06
            Session("sComparison") = ""
            RefreshTable()
        End If
    End Sub

    'jrs 2009/01/07
    Private Sub SaveForComparison(Optional ScenarioComparison = False)
        Dim i As Integer, j As Integer
        ReDim Session("CompBackup")(UBound(Session("PLOT"), 1), Session("COUNTE"))
        ReDim Session("CompLabelBackup")(11, Session("COUNTE")) 'jrs 2012/07/12
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"), 1)
                Session("CompBackup")(i, j) = Session("PLOT")(i, j)
            Next i
            Session("CompLabelBackup")(1, j) = Session("VARN")(0, j)
            Session("CompLabelBackup")(2, j) = Session("VARN")(1, j)
            Session("CompLabelBackup")(3, j) = Session("GR1N")(j)
            Session("CompLabelBackup")(4, j) = Session("GR2N")(j)
            Session("CompLabelBackup")(5, j) = Session("GR3N")(j)
            Session("CompLabelBackup")(6, j) = Session("GR4N")(j)
            Session("CompLabelBackup")(7, j) = Session("FDNG1")(j)
            Session("CompLabelBackup")(8, j) = Session("FDNG2")(j)
            Session("CompLabelBackup")(9, j) = Session("FDNG3")(j)
            Session("CompLabelBackup")(10, j) = Session("FDNG4")(j)
            'jrs 2012/07/12
            Session("CompLabelBackup")(11, j) = Session("RestorePos")(j)
        Next j
        If ScenarioComparison = False Then
            Session("COUNTE") = Session("COUNTE") / 2
        End If


        'jrs 2012/02/06
        If Session("bFilterVisible") Then
            cswmWebMenu.Groups("mnuFilter").ShowMenuBarItem = False
        End If
        Session("TableHideFilter") = True
        cswmWebMenu.Groups("mnuSave").Items("mnuSaveDec").Enabled = False
    End Sub
    'jrs 2009/01/07
    Private Sub LabelComparison(ByVal j As Integer, ByVal sOp As String, ByVal bOrder As Boolean)
        'j is the column to load
        'sOp is the operation to load - or /
        'bOrder is true if it's a sOp b, false if it's b sOp a
        Dim Pos As Integer
        Pos = -1
        Dim j1 As Integer, j2 As Integer
        If bOrder Then
            j1 = 2 * j - 1
            j2 = 2 * j
        Else
            j1 = 2 * j
            j2 = 2 * j - 1
        End If

        If Session("CompLabelBackup")(1, j1) = Session("CompLabelBackup")(1, j2) Then
            Session("VARN")(0, j) = Session("CompLabelBackup")(1, j2)
        Else
            Pos = InStr(Session("CompLabelBackup")(1, j1), "[")
            If Pos = -1 Then
                Pos = Len(Session("CompLabelBackup")(1, j1))
            End If
            If Mid(Session("CompLabelBackup")(1, j1), 1, Pos) = Mid(Session("CompLabelBackup")(1, j2), 1, Pos) Then
                Session("VARN")(0, j) = Replace(Session("CompLabelBackup")(1, j1), "]", sOp & Mid(Session("CompLabelBackup")(1, j2), Pos + 1))
            Else
                Session("VARN")(0, j) = Session("CompLabelBackup")(1, j1) & sOp & Session("CompLabelBackup")(1, j2)
            End If
        End If
        If Session("CompLabelBackup")(2, j1) = Session("CompLabelBackup")(2, j2) Then
            Session("VARN")(1, j) = Session("CompLabelBackup")(2, j2)
        Else
            If Pos > -1 Then
                Session("VARN")(1, j) = Mid(Session("CompLabelBackup")(2, j2), 1, Pos - 1)
            Else
                Session("VARN")(1, j) = Session("CompLabelBackup")(2, j1) & sOp & Session("CompLabelBackup")(2, j2)
            End If
        End If
        If Session("CompLabelBackup")(3, j1) = Session("CompLabelBackup")(3, j2) Then
            Session("GR1N")(j) = Session("CompLabelBackup")(3, j2)
        Else
            Session("GR1N")(j) = Session("CompLabelBackup")(3, j1) & sOp & Session("CompLabelBackup")(3, j2)
        End If
        If Session("CompLabelBackup")(4, j1) = Session("CompLabelBackup")(4, j2) Then
            Session("GR2N")(j) = Session("CompLabelBackup")(4, j2)
        Else
            Session("GR2N")(j) = Session("CompLabelBackup")(4, j1) & sOp & Session("CompLabelBackup")(4, j2)
        End If
        If Session("CompLabelBackup")(5, j1) = Session("CompLabelBackup")(5, j2) Then
            Session("GR3N")(j) = Session("CompLabelBackup")(5, j2)
        Else
            Session("GR3N")(j) = Session("CompLabelBackup")(5, j1) & sOp & Session("CompLabelBackup")(5, j2)
        End If
        If Session("CompLabelBackup")(6, j1) = Session("CompLabelBackup")(6, j2) Then
            Session("GR4N")(j) = Session("CompLabelBackup")(6, j2)
        Else
            Session("GR4N")(j) = Session("CompLabelBackup")(6, j1) & sOp & Session("CompLabelBackup")(6, j2)
        End If
        If Session("CompLabelBackup")(7, j1) = Session("CompLabelBackup")(7, j2) Then
            Session("FDNG1")(j) = Session("CompLabelBackup")(7, j2)
        Else
            Session("FDNG1")(j) = Session("CompLabelBackup")(7, j1) & sOp & Session("CompLabelBackup")(7, j2)
        End If
        If Session("CompLabelBackup")(8, j1) = Session("CompLabelBackup")(8, j2) Then
            Session("FDNG2")(j) = Session("CompLabelBackup")(8, j2)
        Else
            Session("FDNG2")(j) = Session("CompLabelBackup")(8, j1) & sOp & Session("CompLabelBackup")(8, j2)
        End If
        If Session("CompLabelBackup")(9, j1) = Session("CompLabelBackup")(9, j2) Then
            Session("FDNG3")(j) = Session("CompLabelBackup")(9, j2)
        Else
            Session("FDNG3")(j) = Session("CompLabelBackup")(9, j1) & sOp & Session("CompLabelBackup")(9, j2)
        End If
        If Session("CompLabelBackup")(10, j1) = Session("CompLabelBackup")(10, j2) Then
            Session("FDNG4")(j) = Session("CompLabelBackup")(10, j2)
        Else
            Session("FDNG4")(j) = Session("CompLabelBackup")(10, j1) & sOp & Session("CompLabelBackup")(10, j2)
        End If
        'jrs 2012/07/12
        If Session("CompLabelBackup")(11, j1) = Session("CompLabelBackup")(11, j2) Then
            Session("RestorePos")(j) = Session("CompLabelBackup")(11, j2)
        Else
            Session("RestorePos")(j) = j
        End If
    End Sub
    Private Sub LabelScenarioComparison(j As Integer, BaseColumn As Integer, ColumnToEdit As Integer, sOp As String, bOrder As Boolean)
        'j is the column to load
        'sOp is the operation to load - or / or %
        'bOrder is true if it's a sOp b, false if it's b sOp a
        Dim pos As Integer
        pos = -1
        Dim j1 As Integer, j2 As Integer
        If bOrder Then
            j1 = BaseColumn + 1
            j2 = j + 1
        Else
            j1 = j + 1
            j2 = BaseColumn + 1
        End If
        If Session("CompLabelBackup")(1, j1) = Session("CompLabelBackup")(1, j2) Then
            Session("VARN")(0, ColumnToEdit) = Session("CompLabelBackup")(1, j2)
        Else
            Pos = InStr(Session("CompLabelBackup")(1, j1), "[")
            If Pos = -1 Then
                Pos = Len(Session("CompLabelBackup")(1, j1))
            End If
            If Mid(Session("CompLabelBackup")(1, j1), 1, Pos) = Mid(Session("CompLabelBackup")(1, j2), 1, Pos) Then
                Session("VARN")(0, ColumnToEdit) = Replace(Session("CompLabelBackup")(1, j1), "]", sOp & Mid(Session("CompLabelBackup")(1, j2), pos + 1))
            Else
                Session("VARN")(0, ColumnToEdit) = Session("CompLabelBackup")(1, j1) & sOp & Session("CompLabelBackup")(1, j2)
            End If
        End If
        If Session("CompLabelBackup")(2, j1) = Session("CompLabelBackup")(2, j2) Then
            Session("VARN")(1, ColumnToEdit) = Session("CompLabelBackup")(2, j2)
        Else
            If pos > -1 Then
                Session("VARN")(1, ColumnToEdit) = Mid(Session("CompLabelBackup")(2, j2), 1, pos - 1)
            Else
                Session("VARN")(1, ColumnToEdit) = Session("CompLabelBackup")(2, j1) & sOp & Session("CompLabelBackup")(2, j2)
            End If
        End If
        If Session("CompLabelBackup")(3, j1) = Session("CompLabelBackup")(3, j2) Then
            Session("GR1N")(ColumnToEdit) = Session("CompLabelBackup")(3, j2)
        Else
            Session("GR1N")(ColumnToEdit) = Session("CompLabelBackup")(3, j1) & sOp & Session("CompLabelBackup")(3, j2)
        End If
        If Session("CompLabelBackup")(4, j1) = Session("CompLabelBackup")(4, j2) Then
            Session("GR2N")(ColumnToEdit) = Session("CompLabelBackup")(4, j2)
        Else
            Session("GR2N")(ColumnToEdit) = Session("CompLabelBackup")(4, j1) & sOp & Session("CompLabelBackup")(4, j2)
        End If
        If Session("CompLabelBackup")(5, j1) = Session("CompLabelBackup")(5, j2) Then
            Session("GR3N")(ColumnToEdit) = Session("CompLabelBackup")(5, j2)
        Else
            Session("GR3N")(ColumnToEdit) = Session("CompLabelBackup")(5, j1) & sOp & Session("CompLabelBackup")(5, j2)
        End If
        If Session("CompLabelBackup")(6, j1) = Session("CompLabelBackup")(6, j2) Then
            Session("GR4N")(ColumnToEdit) = Session("CompLabelBackup")(6, j2)
        Else
            Session("GR4N")(ColumnToEdit) = Session("CompLabelBackup")(6, j1) & sOp & Session("CompLabelBackup")(6, j2)
        End If
        If Session("CompLabelBackup")(7, j1) = Session("CompLabelBackup")(7, j2) Then
            Session("FDNG1")(ColumnToEdit) = Session("CompLabelBackup")(7, j2)
        Else
            Session("FDNG1")(ColumnToEdit) = Session("CompLabelBackup")(7, j1) & sOp & Session("CompLabelBackup")(7, j2)
        End If
        If Session("CompLabelBackup")(8, j1) = Session("CompLabelBackup")(8, j2) Then
            Session("FDNG2")(ColumnToEdit) = Session("CompLabelBackup")(8, j2)
        Else
            Session("FDNG2")(ColumnToEdit) = Session("CompLabelBackup")(8, j1) & sOp & Session("CompLabelBackup")(8, j2)
        End If
        If Session("CompLabelBackup")(9, j1) = Session("CompLabelBackup")(9, j2) Then
            Session("FDNG3")(ColumnToEdit) = Session("CompLabelBackup")(9, j2)
        Else
            Session("FDNG3")(ColumnToEdit) = Session("CompLabelBackup")(9, j1) & sOp & Session("CompLabelBackup")(9, j2)
        End If
        If Session("CompLabelBackup")(10, j1) = Session("CompLabelBackup")(10, j2) Then
            Session("FDNG4")(ColumnToEdit) = Session("CompLabelBackup")(10, j2)
        Else
            Session("FDNG4")(ColumnToEdit) = Session("CompLabelBackup")(10, j1) & sOp & Session("CompLabelBackup")(10, j2)
        End If
        'jrs 2012/07/12
        If Session("CompLabelBackup")(11, j1) = Session("CompLabelBackup")(11, j2) Then
            Session("RestorePos")(ColumnToEdit) = Session("CompLabelBackup")(11, j2)
        Else
            Session("RestorePos")(j) = j
        End If
    End Sub
    'jrs 2009/01/07
    Private Sub ResetForComparison(Optional ScenarioComparison = False, Optional VariableCount = 0)
        Session("COUNTE") = UBound(Session("CompLabelBackup"), 2)
        Dim i As Integer, j As Integer
        For j = 1 To UBound(Session("CompBackup"), 2) 'jrs 2009/02/24 Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"), 1)
                Session("PLOT")(i, j) = Session("CompBackup")(i, j)
            Next i
            Session("VARN")(0, j) = Session("CompLabelBackup")(1, j)
            Session("VARN")(1, j) = Session("CompLabelBackup")(2, j)
            Session("GR1N")(j) = Session("CompLabelBackup")(3, j)
            Session("GR2N")(j) = Session("CompLabelBackup")(4, j)
            Session("GR3N")(j) = Session("CompLabelBackup")(5, j)
            Session("GR4N")(j) = Session("CompLabelBackup")(6, j)
            Session("FDNG1")(j) = Session("CompLabelBackup")(7, j)
            Session("FDNG2")(j) = Session("CompLabelBackup")(8, j)
            Session("FDNG3")(j) = Session("CompLabelBackup")(9, j)
            Session("FDNG4")(j) = Session("CompLabelBackup")(10, j)
            'jrs 2012/07/12
            Session("RestorePos")(j) = Session("CompLabelBackup")(11, j)
        Next j
        'jrs 2012/02/06
        If Session("bFilterVisible") Then
            cswmWebMenu.Groups("mnuFilter").ShowMenuBarItem = True
        End If
        Session("TableHideFilter") = False
        cswmWebMenu.Groups("mnuSave").Items("mnuSaveDec").Enabled = True
    End Sub

    'jrs 2012/01/16
    Private Sub SaveForFilter()
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"), 1)
                Session("FilterBackup")(i, j) = Session("PLOT")(i, j)
            Next i
            Session("FilterLabelBackup")(1, j) = Session("VARN")(0, j)
            Session("FilterLabelBackup")(2, j) = Session("VARN")(1, j)
            Session("FilterLabelBackup")(3, j) = Session("GR1N")(j)
            Session("FilterLabelBackup")(4, j) = Session("GR2N")(j)
            Session("FilterLabelBackup")(5, j) = Session("GR3N")(j)
            Session("FilterLabelBackup")(6, j) = Session("GR4N")(j)
            Session("FilterLabelBackup")(7, j) = Session("FDNG1")(j)
            Session("FilterLabelBackup")(8, j) = Session("FDNG2")(j)
            Session("FilterLabelBackup")(9, j) = Session("FDNG3")(j)
            Session("FilterLabelBackup")(10, j) = Session("FDNG4")(j)
        Next j

    End Sub


    'jrs 2012/01/16
    Private Sub ResetForFilter()
        Dim i As Integer, j As Integer
        For j = 1 To UBound(Session("FilterBackup"), 2) 'jrs 2009/02/24 Session("COUNTE")
            For i = 1 To UBound(Session("PLOT"), 1)
                Session("PLOT")(i, j) = Session("FilterBackup")(i, j)
            Next i
            Session("VARN")(0, j) = Session("FilterLabelBackup")(1, j)
            Session("VARN")(1, j) = Session("FilterLabelBackup")(2, j)
            Session("GR1N")(j) = Session("FilterLabelBackup")(3, j)
            Session("GR2N")(j) = Session("FilterLabelBackup")(4, j)
            Session("GR3N")(j) = Session("FilterLabelBackup")(5, j)
            Session("GR4N")(j) = Session("FilterLabelBackup")(6, j)
            Session("FDNG1")(j) = Session("FilterLabelBackup")(7, j)
            Session("FDNG2")(j) = Session("FilterLabelBackup")(8, j)
            Session("FDNG3")(j) = Session("FilterLabelBackup")(9, j)
            Session("FDNG4")(j) = Session("FilterLabelBackup")(10, j)
        Next j
    End Sub

    'jrs 2011/07/01
    Private Sub mnuIntTotOff_Click()
        If (Session("bIntTot") = True) Then
            If CType(Session("TblDispOpt"), Stack).Peek <> "Interval Tot" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntTot").Group.Items("mnuIntTotOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntTot").Group.Items("mnuIntTotOff").IconImage = "checkbox.gif"
            Session("bIntTot") = False

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("PLOT")(rr, cc) = Session("IntervalTot_PLOT")(rr, cc)
                Next rr
            Next cc

            RefreshTable()
        End If
    End Sub

    'jrs 2011/07/01
    Private Sub mnuIntTotOn_Click()
        If (Session("bIntTot") = False) Then
            CType(Session("TblDispOpt"), Stack).Push("Interval Tot")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntTot").Group.Items("mnuIntTotOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntTot").Group.Items("mnuIntTotOff").IconImage = "blank.gif"
            Session("bIntTot") = True

            Dim cc As Integer, rr As Integer, i As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("IntervalTot_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
                Next rr

                i = 1
                For rr = 1 + Session("Interval") To DisYear Step Session("Interval")
                    If IsNumeric(Session("IntervalTot_PLOT")(i, cc)) And IsNumeric(Session("IntervalTot_PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = (Session("IntervalTot_PLOT")(rr, cc) / Session("IntervalTot_PLOT")(i, cc) - 1) * 100
                    Else
                        Session("PLOT")(rr, cc) = DBNull.Value
                    End If
                    i = rr
                Next rr

                If (DisYear - 1) Mod Session("Interval") > 1 Then
                    rr = DisYear
                    i = DisYear - ((DisYear - 1) Mod Session("Interval"))
                    If IsNumeric(Session("IntervalTot_PLOT")(i, cc)) And IsNumeric(Session("IntervalTot_PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = (Session("IntervalTot_PLOT")(rr, cc) / Session("IntervalTot_PLOT")(i, cc) - 1) * 100
                    Else
                        Session("PLOT")(rr, cc) = DBNull.Value
                    End If
                End If
            Next cc

            RefreshTable()
        End If
    End Sub


    'jrs 2011/07/01
    Private Sub mnuIntAbsOff_Click()
        If (Session("bIntAbs") = True) Then
            If CType(Session("TblDispOpt"), Stack).Peek <> "Interval Abs" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAbs").Group.Items("mnuIntAbsOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAbs").Group.Items("mnuIntAbsOff").IconImage = "checkbox.gif"
            Session("bIntAbs") = False

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("PLOT")(rr, cc) = Session("IntervalAbs_PLOT")(rr, cc)
                Next rr
            Next cc

            RefreshTable()
        End If
    End Sub

    'jrs 2011/07/01
    Private Sub mnuIntAbsOn_Click()
        If (Session("bIntAbs") = False) Then
            CType(Session("TblDispOpt"), Stack).Push("Interval Abs")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAbs").Group.Items("mnuIntAbsOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIntAbs").Group.Items("mnuIntAbsOff").IconImage = "blank.gif"
            Session("bIntAbs") = True

            Dim cc As Integer, rr As Integer, i As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("IntervalAbs_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
                Next rr

                i = 1
                For rr = 1 + Session("Interval") To DisYear Step Session("Interval")
                    If IsNumeric(Session("IntervalAbs_PLOT")(i, cc)) And IsNumeric(Session("IntervalAbs_PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = Session("IntervalAbs_PLOT")(rr, cc) - Session("IntervalAbs_PLOT")(i, cc)
                    Else
                        Session("PLOT")(rr, cc) = DBNull.Value
                    End If
                    i = rr
                Next rr

                If (DisYear - 1) Mod Session("Interval") > 1 Then
                    rr = DisYear
                    i = DisYear - ((DisYear - 1) Mod Session("Interval"))
                    If IsNumeric(Session("IntervalAbs_PLOT")(i, cc)) And IsNumeric(Session("IntervalAbs_PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = Session("IntervalAbs_PLOT")(rr, cc) - Session("IntervalAbs_PLOT")(i, cc)
                    Else
                        Session("PLOT")(rr, cc) = DBNull.Value
                    End If
                End If
            Next cc

            RefreshTable()
        End If
    End Sub

    'jrs 2012/01/02
    Private Sub mnuIdxBase1Off_Click()
        If (Session("bIdxBase1") = True) Then
            If CType(Session("TblDispOpt"), Stack).Peek <> "Index Base One" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIdxBase1").Group.Items("mnuIdxBase1On").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIdxBase1").Group.Items("mnuIdxBase1Off").IconImage = "checkbox.gif"
            Session("bIdxBase1") = False

            Dim cc As Integer, rr As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("PLOT")(rr, cc) = Session("RatioFstYr_PLOT")(rr, cc)
                Next rr
            Next cc

            RefreshTable()
        End If
    End Sub

    'jrs 2012/01/02
    Private Sub mnuIdxBase1On_Click()
        If (Session("bIdxBase1") = False) Then
            CType(Session("TblDispOpt"), Stack).Push("Index Base One")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIdxBase1").Group.Items("mnuIdxBase1On").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuIdxBase1").Group.Items("mnuIdxBase1Off").IconImage = "blank.gif"
            Session("bIdxBase1") = True

            Dim cc As Integer, rr As Integer, i As Integer

            'jrs 2012/01/04
            If Session("FirstYear") = FirstYearHistory And UCase(Session("ActiveBase")) <> "IFSBASEHIST.RUN" Then
                'History plus forecast
                i = FirstYearForecast - FirstYearHistory + 1
            Else
                i = 1
            End If

            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("RatioFstYr_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
                Next rr

                For rr = 1 To DisYear Step Session("Interval")
                    If IsNumeric(Session("RatioFstYr_PLOT")(i, cc)) And IsNumeric(Session("RatioFstYr_PLOT")(rr, cc)) Then
                        If Session("RatioFstYr_PLOT")(i, cc) <> 0 Then
                            Session("PLOT")(rr, cc) = Session("RatioFstYr_PLOT")(rr, cc) / Session("RatioFstYr_PLOT")(i, cc)
                        Else
                            Session("PLOT")(rr, cc) = DBNull.Value
                        End If
                    Else
                        Session("PLOT")(rr, cc) = DBNull.Value
                    End If
                Next rr

                If (DisYear - 1) Mod Session("Interval") > 1 Then
                    rr = DisYear
                    If IsNumeric(Session("RatioFstYr_PLOT")(i, cc)) And IsNumeric(Session("RatioFstYr_PLOT")(rr, cc)) And
                    Session("RatioFstYr_PLOT")(i, cc) <> 0 Then
                        Session("PLOT")(rr, cc) = Session("RatioFstYr_PLOT")(rr, cc) / Session("RatioFstYr_PLOT")(i, cc)
                    Else
                        Session("PLOT")(rr, cc) = DBNull.Value
                    End If
                End If
            Next cc

            RefreshTable()
        End If
    End Sub

    'jrs 2010/02/04
    Private Sub mnuAliasOn_Click()
        Dim i As Integer
        If Not Session("bAlias") Then
            Session("bAlias") = True
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuAlias").Group.Items("mnuAliasOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuAlias").Group.Items("mnuAliasOff").IconImage = "blank.gif"

            'Read and load all aliases
            Dim Tbl As DataTable, Tbl2 As DataTable 'jrs 2016/05/27
            Dim s As String
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT GroupName, Alias FROM " & GroupIDNew, "GroupName", Tbl)
            'jrs 2016/05/25
            Dim RegIdx As Integer
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT RegionNum, RegionAlias FROM " & RegionIDNew, "RegionNum", Tbl2)

            For i = 1 To Session("COUNTE")
                s = SeekValue(Tbl, Session("GR1N")(i), "Alias")
                If s > "" Then
                    Session("GR1N")(i) = s
                Else 'jrs 2016/05/27
                    RegIdx = IFsRegs.Item(Session("GR1N")(i)).Index
                    If RegIdx > 0 Then
                        s = SeekValue(Tbl2, RegIdx, "RegionAlias")
                        If s > "" Then
                            Session("GR1N")(i) = s
                        End If
                    End If
                End If
                'jrs 2021/02/08
                Session("FDNG1")(i) = Session("GR1N")(i)
            Next i
            RefreshTable()
        End If
    End Sub

    'jrs 2010/02/04
    Private Sub mnuAliasOff_Click()
        Dim i As Integer
        If Session("bAlias") Then
            Session("bAlias") = False
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuAlias").Group.Items("mnuAliasOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuAlias").Group.Items("mnuAliasOff").IconImage = "checkbox.gif"

            For i = 1 To Session("COUNTE")
                Session("GR1N")(i) = Session("oGR1N")(i)
                'jrs 2021/02/08
                Session("FDNG1")(i) = Session("GR1N")(i)
            Next i
            RefreshTable()
        End If
    End Sub

    'jrs 2011/07/01
    Protected Sub cmdInterval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdInterval.Click
        If IsNumeric(txtInterval.Text) Then
            If CInt(txtInterval.Text) > 0 Then
                Dim bReLoad As Boolean
                bReLoad = CBool(Session("PercentOn")) Or Session("bIntAvg") = True Or Session("bIntTot") = True Or Session("bIntAbs") = True 'jrs 2011/07/01
                Dim UnitIdx As Integer
                Dim TblDispOptCopy As Stack
                TblDispOptCopy = New Stack
                If bReLoad Then
                    'Restore original values
                    With CType(Session("TblDispOpt"), Stack)
                        Do While .Count > 0
                            TblDispOptCopy.Push(.Peek())
                            Select Case .Peek().ToString
                                Case "Percent"
                                    mnuPercent_Click(Me, New EventArgs)
                                Case "Cumulative"
                                    mnuCumOff_Click()
                                Case "Discount"
                                    mnuDisOff_Click()
                                Case "Cumulative Discount"
                                    mnuCumDisOff_Click()
                                Case "Interval Avg"
                                    mnuIntAvgOff_Click()
                                Case "Interval Tot"
                                    mnuIntTotOff_Click()
                                Case "Interval Abs" 'jrs 2011/07/01
                                    mnuIntAbsOff_Click()
                                Case "Moving Avg"
                                    mnuMovingAvgOff_Click()
                                Case "Validation"
                                    mnuValidation_Click(Me, New EventArgs)
                                Case "Percent of Total"
                                    mnuPctTotReset_Click()
                                Case "Comparison"
                                    mnuCompReset_Click()
                                Case "Index Base One" 'jrs 2012/01/02
                                    mnuIdxBase1Off_Click()
                                Case "Unit Conv" 'jrs 2013/02/20
                                    UnitIdx = Session("UnitConvSel")
                                    mnuUnitOff_Click()
                                Case Else
                                    LoadMessage("There's a problem with the implementation of " & .Peek.ToString & ". Please reload the variables.", Session("Message"))
                                    Support.FormManager.StateStore(Me)
                                    Support.FormManager.Show(Me, "frm_Message.aspx")
                            End Select
                        Loop
                    End With
                End If
                Session("Interval") = CInt(txtInterval.Text)
                If bReLoad Then
                    'Restore original values
                    With TblDispOptCopy
                        Do While .Count > 0
                            Select Case .Peek.ToString
                                Case "Percent"
                                    mnuPercent_Click(Me, New EventArgs)
                                Case "Cumulative"
                                    mnuCumOn_Click()
                                Case "Discount"
                                    mnuDisOn_Click()
                                Case "Cumulative Discount"
                                    mnuCumDisOn_Click()
                                Case "Interval Avg"
                                    mnuIntAvgOn_Click()
                                Case "Interval Tot"
                                    mnuIntTotOn_Click()
                                Case "Interval Abs" 'jrs 2011/07/01
                                    mnuIntAbsOn_Click()
                                Case "Moving Avg"
                                    mnuMovingAvgOn_Click()
                                Case "Validation"
                                    mnuValidation_Click(Me, New EventArgs)
                                Case "Percent of Total"
                                    mnuPctTotal_Click()
                                Case "Index Base One" 'jrs 2012/01/02
                                    mnuIdxBase1On_Click()
                                Case "Unit Conv" 'jrs 2013/02/20
                                    mnuUnitOn_Click(UnitIdx)
                            End Select
                            TblDispOptCopy.Pop()
                        Loop
                    End With
                End If
                RefreshTable()
                frm_Interval.Visible = False
                'jrs 2024/09/04
                Dim bCustom As Boolean = True
                If Session("Interval") = "1" Then
                    bCustom = False
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("1").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
                Else
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("1").IconImage = Page.ResolveUrl("~/Images/blank.gif")
                End If
                If Session("Interval") = "5" Then
                    bCustom = False
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("5").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
                Else
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("5").IconImage = Page.ResolveUrl("~/Images/blank.gif")
                End If
                If Session("Interval") = "10" Then
                    bCustom = False
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("10").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
                Else
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("10").IconImage = Page.ResolveUrl("~/Images/blank.gif")
                End If
                If bCustom Then
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("Custom Interval").IconImage = Page.ResolveUrl("~/Images/checkbox.gif")
                Else
                    cswmWebMenu.Groups("Display Options").Items("Change Display Interval").Group.Items("Custom Interval").IconImage = Page.ResolveUrl("~/Images/blank.gif")
                End If
            End If
        End If
        'jrs 2012/05/08
        cswmWebMenu.Visible = True
    End Sub

    'jrs 2011/07/04
    Private Sub mnuNoRank_Click()
        Dim i As Integer

        'Copy source
        Dim pCopy As Object, VARNCopy As Object, gr1Copy As Object, gr2Copy As Object,
        gr3Copy As Object, gr4Copy As Object, fCopy As Object,
        FileNumbrCopy As Object, FinYearCopy As Object, FinYearHisCopy As Object
        'splot and focus
        Dim PLOT_Pct_Tot_Copy As Object 'jrs 2009/01/30, sPLOTCopy As Variant
        Dim Percent_PLOT_Copy As Object, IntervalAvg_PLOT_Copy As Object, MovingAvg_PLOT_Copy As Object
        Dim Validation_PLOT_Copy As Object, CompBackup_Copy As Object, CompLabelBackup_Copy As Object
        'jrs 2011/07/04
        Dim IntervalTot_PLOT_Copy As Object, IntervalAbs_PLOT_Copy As Object
        'jrs 2012/01/02
        Dim RatioFstYr_PLOT_Copy As Object
        'jrs 2010/09/28
        Dim FDNG1Copy As Object, FDNG2Copy As Object, FDNG3Copy As Object, FDNG4Copy As Object

        pCopy = VB6.CopyArray(Session("PLOT"))
        VARNCopy = VB6.CopyArray(Session("VARN"))
        gr1Copy = VB6.CopyArray(Session("GR1N"))
        gr2Copy = VB6.CopyArray(Session("GR2N"))
        gr3Copy = VB6.CopyArray(Session("GR3N"))
        gr4Copy = VB6.CopyArray(Session("GR4N"))
        fCopy = VB6.CopyArray(Session("aFormulas"))

        FDNG1Copy = VB6.CopyArray(Session("FDNG1"))
        FDNG2Copy = VB6.CopyArray(Session("FDNG2"))
        FDNG3Copy = VB6.CopyArray(Session("FDNG3"))
        FDNG4Copy = VB6.CopyArray(Session("FDNG4"))

        FileNumbrCopy = VB6.CopyArray(Session("FileNumbr"))
        FinYearCopy = VB6.CopyArray(Session("FinYear"))
        FinYearHisCopy = VB6.CopyArray(Session("FinYearHis"))

        'jrs 2009/01/30 sPLOTCopy = SPLOT
        Percent_PLOT_Copy = VB6.CopyArray(Session("Percent_PLOT"))
        IntervalAvg_PLOT_Copy = VB6.CopyArray(Session("IntervalAvg_PLOT"))
        MovingAvg_PLOT_Copy = VB6.CopyArray(Session("MovingAvg_PLOT"))
        Validation_PLOT_Copy = VB6.CopyArray(Session("Validation_PLOT"))
        CompBackup_Copy = VB6.CopyArray(Session("CompBackup"))
        CompLabelBackup_Copy = VB6.CopyArray(Session("CompLabelBackup"))
        PLOT_Pct_Tot_Copy = VB6.CopyArray(Session("PLOT_Pct_Tot"))
        'jrs 2011/07/04
        IntervalTot_PLOT_Copy = VB6.CopyArray(Session("IntervalTot_PLOT"))
        IntervalAbs_PLOT_Copy = VB6.CopyArray(Session("IntervalAbs_PLOT"))
        'jrs 2012/01/02
        RatioFstYr_PLOT_Copy = VB6.CopyArray(Session("RatioFstYr_PLOT"))

        'Reset Target
        Dim j As Integer, k As Integer
        'Dim m As Integer
        'Dim focus As Integer
        For i = 1 To Session("COUNTE")
            'Restore position
            k = Session("RestorePos")(i)
            'jrs 2014/09/25
            Session("RestorePos")(i) = i
            'copy to target
            For j = 0 To UBound(Session("PLOT"), 1)
                Session("PLOT")(j, k) = pCopy(j, i)
                Session("Percent_PLOT")(j, k) = Percent_PLOT_Copy(j, i)
                Session("IntervalAvg_PLOT")(j, k) = IntervalAvg_PLOT_Copy(j, i)
                Session("MovingAvg_PLOT")(j, k) = MovingAvg_PLOT_Copy(j, i)
                Session("Validation_PLOT")(j, k) = Validation_PLOT_Copy(j, i)
                'jrs 2012/07/12
                'Session("CompBackup")(j, k) = CompBackup_Copy(j, i)
                If UBound(Session("CompBackup"), 2) >= Session("COUNTE") * 2 Then
                    Session("CompBackup")(j, 2 * k - 1) = CompBackup_Copy(j, 2 * i - 1)
                    Session("CompBackup")(j, 2 * k) = CompBackup_Copy(j, 2 * i)
                End If

                Session("PLOT_Pct_Tot")(j, k) = PLOT_Pct_Tot_Copy(j, i)
                Session("IntervalTot_PLOT")(j, k) = IntervalTot_PLOT_Copy(j, i)
                Session("IntervalAbs_PLOT")(j, k) = IntervalAbs_PLOT_Copy(j, i)
                'jrs 2012/01/02
                Session("RatioFstYr_PLOT")(j, k) = RatioFstYr_PLOT_Copy(j, i)
            Next j
            'jrs 2012/07/12
            If UBound(Session("CompBackup"), 2) >= Session("COUNTE") * 2 Then
                'jrs 2009/01/30
                For j = 1 To 10
                    'jrs 2012/07/12 Session("CompLabelBackup")(j, k) = CompLabelBackup_Copy(j, i)
                    Session("CompLabelBackup")(j, 2 * k - 1) = CompLabelBackup_Copy(j, 2 * i - 1)
                    Session("CompLabelBackup")(j, 2 * k) = CompLabelBackup_Copy(j, 2 * i)
                Next j
            End If

            For j = 0 To UBound(Session("VARN"), 1)
                Session("VARN")(j, k) = VARNCopy(j, i)
            Next j
            Session("GR1N")(k) = gr1Copy(i)
            Session("GR2N")(k) = gr2Copy(i)
            Session("GR3N")(k) = gr3Copy(i)
            Session("GR4N")(k) = gr4Copy(i)
            If i <= UBound(fCopy) Then
                If k > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(k)
                End If
                Session("aFormulas")(k) = fCopy(i)
            End If
            Session("FDNG1")(k) = FDNG1Copy(i)
            Session("FDNG2")(k) = FDNG2Copy(i)
            Session("FDNG3")(k) = FDNG3Copy(i)
            Session("FDNG4")(k) = FDNG4Copy(i)
            Session("FileNumbr")(k) = FileNumbrCopy(i)
            Session("FinYear")(k) = FinYearCopy(i)
            Session("FinYearHis")(k) = FinYearHisCopy(i)
        Next i
        'Refresh
        RefreshTable()

        'mnuNoRank.Visible = False
        cswmWebMenu.Groups("Rank Options").Items.Remove("No Rank")
        Session("bRank") = False
    End Sub

    'jrs 2012/03/09
    Protected Sub cmdHorizon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdHorizon.Click
        If Not IsNumeric(txtHorizon.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtHorizon.Text) > Session("LastYearForecast") Or CInt(txtHorizon.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        If Session("Interval") > 1 Then
            If (CInt(txtHorizon.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        cmb_Horizon.Text = txtHorizon.Text
        cmb_Horizon_SelectedIndexChanged(Me, New EventArgs)

    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' JAM 2012-04-03 Building table with scroll function based in a grid
    ''' 
    ''' 
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    'jrs 2024/11/25 Adding parameter ColNumber, so that we don't run out of memory when COUNTE = NR*NR*2
    Public Sub BuildTable()

        Dim ColsToDisplay As Integer
        If Session("COUNTE") > NR * 10 Then
            ColsToDisplay = NR * 10
        Else
            ColsToDisplay = Session("COUNTE")
        End If

        Dim dt As New DataTable()
        Dim dcol As New DataColumn()
        Dim i, j As Integer

        'jrs 2013/05/10
        'If (GridView1.Rows.Count > 0) Then
        '    ClientScript.RegisterStartupScript(Me.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'GridView1', 'HeaderDiv');</script>")
        'End If
        'ttt 2020/08/31
        Me.txtTableCols.Value = (ColsToDisplay + 1).ToString

        For j = 0 To ColsToDisplay

            dcol = New DataColumn(Str(j))

            dt.Columns.Add(dcol)

            'dt.Columns.Add("TextData")
            'dt.Columns.Add("OtherInfo")
        Next
        'ttt 2020-09-05 Add Header as ROW-------------------------------------
        Dim dr0 As DataRow = dt.NewRow
        For j = 0 To ColsToDisplay
            'Will write below
            dr0(Str(j)) = ""
        Next
        dt.Rows.Add(dr0)
        dt.AcceptChanges()
        'ENDttt 2020-09-05 Add Header as ROW--------------------------------------

        For i = 6 To (Session("Timepoints") + 5)
            Dim dr As DataRow = dt.NewRow

            For j = 0 To ColsToDisplay

                dr(Str(j)) = grd_TableDisplay.Rows(i).Cells(j).Text

            Next
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next

        GridView1.DataSource = dt
        GridView1.DataBind()
        'ttt 2020-09-05
        For j = 1 To ColsToDisplay
            GridView1.Rows(0).Cells(j).Text = Server.HtmlDecode(grd_TableDisplay.Rows(0).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(1).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(2).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(3).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(4).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(5).Cells(j).Text)
        Next
        GridView1.Rows(0).CssClass = "GridviewScrollHeader"

        'For j = 0 To Session("COUNTE")
        '    If j = 0 Then
        '        'JAM 2012-05-13	Adding blank space because cell borders had a different look
        '        GridView1.HeaderRow.Cells(j).Text = Server.HtmlDecode("&nbsp;</br>") '""
        '        ' GridView1.Columns(j).HeaderText = "Year"
        '    Else
        '        GridView1.HeaderRow.Cells(j).Text = Server.HtmlDecode(grd_TableDisplay.Rows(0).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(1).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(2).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(3).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(4).Cells(j).Text & "</br>" & grd_TableDisplay.Rows(5).Cells(j).Text)
        '    End If
        '    'JAM 2013-06-08 Adding ID cell in order to focus cell
        '    GridView1.HeaderRow.Cells(j).ID = "C" & j.ToString
        'Next j

        'JAM 2012-04-03 Adding Formatting for control
        'ttt 2020-09-05:  6 , +5 -> 7, +6 (because one row is now header), grd_TableDisplay.Rows(i)-> i-1
        For i = 7 To (Session("Timepoints") + 6)
            For j = 0 To ColsToDisplay
                'JAM 2012-04-04 
                If j = 0 Then
                    GridView1.Rows(i - 6).Cells(j).BackColor = Color.LightGray
                Else
                    'JAM 2013-06-08 Adding ID cell in order to focus cell
                    GridView1.Rows(i - 6).Cells(j).ID = "C" & j.ToString
                    If grd_TableDisplay.Rows(i - 1).Cells(j).Font.Bold Then

                        'JAM 2012-05-02 Blue color in historic plus forecast series
                        If grd_TableDisplay.Rows(i - 1).Cells(j).ForeColor = Color.Blue Then
                            GridView1.Rows(i - 6).Cells(j).Text = Server.HtmlDecode("<span style='color: #0000B8;'><b>" & grd_TableDisplay.Rows(i - 1).Cells(j).Text & "</b></span>")
                        Else
                            GridView1.Rows(i - 6).Cells(j).Text = Server.HtmlDecode("<span style='color: #000000;'><b>" & grd_TableDisplay.Rows(i - 1).Cells(j).Text & "</b></span>")
                        End If
                    Else
                        'JAM 2012-05-02 Blue color in historic plus forecast series
                        If grd_TableDisplay.Rows(i - 1).Cells(j).ForeColor = Color.Blue Then
                            GridView1.Rows(i - 6).Cells(j).Text = Server.HtmlDecode("<span style='color: #0000B8;'>" & grd_TableDisplay.Rows(i - 1).Cells(j).Text & "</span>")
                        End If

                    End If
                End If
            Next
        Next

        grd_TableDisplay.Visible = False

        'JAM 2012-05-13 Defining width for GridView Object
        If Session("COUNTE") <= 3 Then
            If Session("COUNTE") = 3 Then
                GridView1.Width = Unit.Percentage(35)
            Else
                GridView1.Width = Unit.Percentage(25)
            End If
        End If

        'jrs 2013/05/10 ClientScript.RegisterStartupScript(Me.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'GridView1', 'HeaderDiv');</script>")


    End Sub

    Protected Sub cmdFY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFY.Click
        If Not IsNumeric(txtFY.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtFY.Text) > Session("LastYearForecast") Or CInt(txtFY.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtFY.Text) > Session("LastYearForecast") Or CInt(txtFY.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        If Session("Interval") > 1 Then
            If (CInt(txtFY.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        cmb_FY.Text = txtFY.Text
        cmb_FY_SelectedIndexChanged(Me, New EventArgs)
    End Sub

    Protected Sub cmdRank_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRank.Click
        If Not IsNumeric(txtRank.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("LastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtRank.Text) > Session("LastYearForecast") Or CInt(txtRank.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearForecast) & " and " & Session("LastYearForecast"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtRank.Text) > Session("LastYearForecast") Or CInt(txtRank.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & CStr(FirstYearHistory) & " and " & CInt(LastYearHistory), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        If Session("Interval") > 1 Then
            If (CInt(txtRank.Text) - Session("FirstYear")) Mod Session("Interval") <> 0 Then
                LoadMessage("Invalid Year, year must follow the existing Interval, please use the combo box.", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If

        'jrs 2014/09/24
        If txtRank.Text > cbo_RY.Items(cbo_RY.Items.Count - 1).Text Then
            Session("DisplayLastYear") = txtRank.Text
            cbo_RY.Items.Add(txtRank.Text)
        End If
        cbo_RY.Text = txtRank.Text
        cbo_RY_SelectedIndexChanged(Me, New EventArgs)

    End Sub

    'jrs 2013/02/20
    Private Sub mnuUnitOff_Click()
        If Not (Session("UnitConvSel") Is Nothing) Then
            If CType(Session("TblDispOpt"), Stack).Peek <> "Unit Conv" Then
                LoadMessage("First you need to turn off option " & CType(Session("TblDispOpt"), Stack).Peek, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
            CType(Session("TblDispOpt"), Stack).Pop()

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUnit").Group.Items("mnuUnitOff").IconImage = "checkbox.gif"
            Session("UnitConvSel") = Nothing

            Dim i As Integer
            For i = 0 To Session("UnitCount") - 1
                cswmWebMenu.Groups("mnuDispOpt").Items("mnuUnit").Group.Items("mnuUnitOn" & CStr(i)).IconImage = "blank.gif"
            Next i

            Dim cc As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("PLOT")(rr, cc) = Session("Unit_PLOT")(rr, cc)
                Next rr
                Session("GR4N")(cc) = Session("oGR4N")(cc)
            Next cc

            'Restore Display Units
            Session("VDisplayUnitsName")(Session("UnitIdx")) = Session("oUnitDisplayName")

            RefreshTable()
        End If
    End Sub

    'jrs 2013/02/20
    Private Sub mnuUnitOn_Click(ByVal Index As Integer)
        If Not (Session("UnitConvSel") Is Nothing) And Session("UnitCount") > 1 Then
            mnuUnitOff_Click()
        End If

        If (Session("UnitConvSel") Is Nothing) Then
            If Session("bNotSameUnits") Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Note that not all variables loaded have the same units, conversion factor only valid for units in first column"
            End If

            CType(Session("TblDispOpt"), Stack).Push("Unit Conv")

            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUnit").Group.Items("mnuUnitOff").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUnit").Group.Items("mnuUnitOn" & CStr(Index)).IconImage = "checkbox.gif"
            Session("UnitConvSel") = Index

            Dim cc As Integer ', i As Integer, x As Object, counter As Integer
            For cc = 1 To Session("COUNTE")
                For rr = 1 To DisYear
                    Session("Unit_PLOT")(rr, cc) = Session("PLOT")(rr, cc)
                    Session("PLOT")(rr, cc) = Session("PLOT")(rr, cc) * Session("vUnitFactor")(Index)
                Next rr
                Session("GR4N")(cc) = Session("sUnitLbl")(Index)
            Next cc

            'jrs 2016/11/09 made this one (VDisplayUnitsName) to Session variable in order to change it on the fly
            'Replace Display Units
            Session("VDisplayUnitsName")(Session("UnitIdx")) = Session("sUnitLblLong")(Index)

            RefreshTable()
        End If

    End Sub

    'jrs 2013/02/21
    Protected Sub cmdDefineUnits_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDefineUnits.Click
        If txtUnitName.Text > "" And txtUnitFactor.Text <> "1" Then
            ReDim Preserve Session("sUnitLbl")(Session("UnitCount"))
            ReDim Preserve Session("sUnitLblLong")(Session("UnitCount"))
            ReDim Preserve Session("vUnitFactor")(Session("UnitCount"))
            ReDim Preserve Session("sUnitMenuLbl")(Session("UnitCount"))

            Session("sUnitLbl")(Session("UnitCount")) = txtUnitName.Text
            Session("sUnitLblLong")(Session("UnitCount")) = txtUnitName.Text
            Session("vUnitFactor")(Session("UnitCount")) = txtUnitFactor.Text
            Session("sUnitMenuLbl")(Session("UnitCount")) = "Convert to " & txtUnitName.Text

            Dim cswmItem As Coalesys.WebMenu.Item
            cswmItem = cswmWebMenu.Groups("mnuDispOpt").Items("mnuUnit").Group.Items.Add(Session("UnitCount") + 1)
            cswmItem.Caption = "Convert to " & txtUnitName.Text
            cswmItem.ID = "mnuUnitOn" & CStr(Session("UnitCount"))
            cswmItem.IconImage = "blank.gif"

            Session("UnitCount") = Session("UnitCount") + 1

            'JAM 2016-05-16
            mnuUnitOn_Click(Session("UnitCount") - 1)

        End If
        frm_DefineUnits.Visible = False
        cswmWebMenu.Visible = True
        Call BuildTable()
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '               JAM 2016-02-13      Checking for unit conversion from billions to trillions
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub Convert_Billions_To_Trillions()
        Dim i, j, II, jj As Integer
        'Dim VBillion As String
        'Dim VUnit, vplus10 As Boolean
        Dim cc As Integer ', x As Object, counter As Integer
        Dim convApplied As Boolean
        Dim rr As Short

        ReDim Session("ColumnsToConvert")(Session("COUNTE"))

        Dim a

        II = 0
        jj = 0
        convApplied = False 'JAM 2016-03-21
        For i = 1 To Session("COUNTE")
            a = Session("GR4N")(i)
            Session("ColumnsToConvert")(i) = False
            If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Then
                II = II + 1

                For j = 1 To DisYear
                    'jrs 2023/11/17 added not isNothing
                    'jrs 2016/09/30
                    If Not IsDBNull(Session("PLOT")(j, i)) And Not IsNothing(Session("PLOT")(j, i)) Then
                        'jrs 2023/10/11
                        If Session("PLOT")(j, i).ToString > "" Then
                            If Session("PLOT")(j, i) >= 100000 Then 'jrs 2016/04/11 changed to 100,000
                                jj = jj + 1
                                Session("ColumnsToConvert")(i) = True
                                convApplied = True
                                Exit For
                            End If
                        End If
                    End If
                Next j

            End If
        Next i


        'JAM 2016-03-21 If there are some trillions conversion
        For i = 1 To Session("COUNTE")
            If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 And convApplied And Not Session("ColumnsToConvert")(i) Then
                Session("ColumnsToConvert")(i) = True
            End If

        Next i



        'JAM 2016-02-13 If The same columns number are in Billions and have a value greater than 10K
        ' If II <> 0 And jj <> 0 And II = jj Then
        'JAM 2016-03-21 With one column in billions
        If II <> 0 And jj <> 0 Then

            For cc = 1 To Session("COUNTE")

                For rr = 1 To DisYear
                    Session("PLOTTrillions")(rr, cc) = Session("PLOT")(rr, cc) 'JAM 2016-05-16

                    'jrs 2019/07/29 added and isNumeric
                    If Session("ColumnsToConvert")(cc) And IsNumeric(Session("PLOT")(rr, cc)) Then
                        Session("PLOT")(rr, cc) = CDbl(Session("PLOT")(rr, cc)) / 1000 'Session("PLOT")(rr, cc) / 1000 JAM 2016-05-16 Adding CDBl to cast Double type
                    End If


                Next rr
                If Session("ColumnsToConvert")(cc) Then 'JAM 2016-05-16
                    Session("GR4N")(cc) = Replace(Session("GR4N")(cc), "Billion", "Trillion")
                End If
            Next cc



            'Call FillData(Session("COUNTE"), ry)

            grd_TableDisplay.Visible = True
            'JAM 2012-04-03
            'Call BuildTable()

            'JAM 2016-03-06
            RefreshTable()

            Session("bTrillionsOk") = True

        Else 'JAM 2016-03-22 If Trillions is applied

            If Not (Session("bTrillionsOk") Is Nothing) Then
                If Session("bTrillionsOk") Then
                    'JAM 2016-03-22 Units must be converted
                    For i = 1 To Session("COUNTE")
                        If InStr(UCase(Session("GR4N")(i)), "BILLION") > 0 Then
                            Session("GR4N")(i) = Replace(Session("GR4N")(i), "Billion", "Trillion")
                            a = Session("GR4N")(i)
                        End If

                    Next i


                    grd_TableDisplay.Visible = True

                    RefreshTable()

                End If
            End If


        End If

        Exit Sub

    End Sub

    'jrs 2025/07/18 copied from Graphical Display
    Protected Sub ResetTrillionsVars()
        Dim rr, cc As Integer

        If Not (Session("bTrillionsOk") Is Nothing) Then
            If Session("bTrillionsOk") Then

                If Session("bTrillionsOk") Then
                    For cc = 1 To Session("COUNTE")
                        For rr = 1 To DisYear
                            Session("PLOT")(rr, cc) = Session("PLOTTrillions")(rr, cc) 'jam 2016-05-16
                        Next rr
                        'jrs 2025/07/18
                        Session("GR4N")(cc) = Session("GR4N")(cc).replace("Trillion", "Billion")
                    Next cc
                End If

                Session("bTrillionsOk") = False
            End If
        End If
    End Sub


    'jrs 2016/03/30
    Private Sub mnuUseStartYrDivby5_Click()
        Session("mnuUseStartYrDivby5") = Not Session("mnuUseStartYrDivby5")
        If Session("mnuUseStartYrDivby5") Then
            'jrs 2023/12/08
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuUseStartYrDivby5").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUseStartYrDivby5").Group.Items("mnuUseStartYrDivby5On").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUseStartYrDivby5").Group.Items("mnuUseStartYrDivby5Off").IconImage = "blank.gif"
            Dim Diff As Integer
            Diff = Session("FirstYear") Mod 5
            Session("FirstYearTblOrg") = Session("FirstYear")
            Session("FirstYear") = Session("FirstYear") + 5 - Diff
            Session("StartYrIdx") = 5 - Diff + 1
        Else
            'jrs 2023/12/08
            'cswmWebMenu.Groups("mnuDispOpt").Items("mnuUseStartYrDivby5").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUseStartYrDivby5").Group.Items("mnuUseStartYrDivby5On").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuUseStartYrDivby5").Group.Items("mnuUseStartYrDivby5Off").IconImage = "checkbox.gif"
            Session("FirstYear") = Session("FirstYearTblOrg")
            Session("StartYrIdx") = 1
        End If
        RefreshTable()
    End Sub

    'jrs 2022/02/24
    Private Sub mnuShowExtraDecimals_Click()
        If Session("ShowExtraDecimals") = "YES" Then
            Session("ShowExtraDecimals") = "NO"
        Else
            Session("ShowExtraDecimals") = "YES"
        End If
        If Session("ShowExtraDecimals") = "YES" Then
            'jrs 2023/12/08 cswmWebMenu.Groups("mnuDispOpt").Items("mnuShowExtraDecimals").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuShowExtraDecimals").Group.Items("mnuShowExtraDecimalsOn").IconImage = "checkbox.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuShowExtraDecimals").Group.Items("mnuShowExtraDecimalsOff").IconImage = "blank.gif"
        Else
            'jrs 2023/12/08 cswmWebMenu.Groups("mnuDispOpt").Items("mnuShowExtraDecimals").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuShowExtraDecimals").Group.Items("mnuShowExtraDecimalsOn").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuDispOpt").Items("mnuShowExtraDecimals").Group.Items("mnuShowExtraDecimalsOff").IconImage = "checkbox.gif"
        End If
        RefreshTable()
    End Sub

    Private Sub mnuCompRatAOther_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Ratio"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") <= 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
        End If
        Dim i As Integer, j As Integer
        Dim Scenarios() As String
        ReDim Preserve Scenarios(0)
        SaveForComparison(ScenarioComparison:=True)
        'Find all Scenario Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim ScenarioStartPosition As Integer
            Dim ScenarioEndPosition As Integer

            ScenarioStartPosition = InStr(Session("CompLabelBackup")(1, i), "[")
            ScenarioEndPosition = InStr(Session("CompLabelBackup")(1, i), "]")

            Dim ScenarioText As String
            ScenarioText = Mid(Session("CompLabelBackup")(1, i), ScenarioStartPosition + 1, (ScenarioEndPosition - 1) - ScenarioStartPosition)
            If Len(Scenarios(0)) = 0 Then
                Scenarios(0) = ScenarioText
            Else
                If Scenarios.Contains(ScenarioText) = False Then
                    ReDim Preserve Scenarios(UBound(Scenarios) + 1)
                    Scenarios(UBound(Scenarios)) = ScenarioText
                End If
            End If
        Next
        If UBound(Scenarios) = 0 Then
            Exit Sub
        End If
        Dim NumberOfScenarios As Integer
        NumberOfScenarios = UBound(Scenarios) + 1
        Dim Variables() As String
        ReDim Preserve Variables(0)

        'Find all Variable Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim VariableEndPosition As Integer

            VariableEndPosition = InStr(Session("CompLabelBackup")(1, i), "[")

            Dim VariableText As String
            VariableText = Left(Session("CompLabelBackup")(1, i), VariableEndPosition - 1)
            If Len(Variables(0)) = 0 Then
                Variables(i - 1) = VariableText
            Else
                If Variables.Contains(VariableText) = False Then
                    ReDim Preserve Variables(UBound(Variables) + 1)
                    Variables(UBound(Variables)) = VariableText
                End If
            End If
        Next i

        Dim NumberOfVariables As Integer
        NumberOfVariables = UBound(Variables) + 1

        Session("COUNTE") = Session("COUNTE") - NumberOfVariables
        'Compute Comparison
        Dim ColumnToSet As Integer
        ColumnToSet = 1
        Dim v As Integer
        For v = 0 To NumberOfVariables - 1 'iterate each variable group
            For j = 1 + (v * NumberOfScenarios) To ((NumberOfScenarios * v) + (NumberOfScenarios - 1)) 'Iterate each scenario within the variable group
                For i = 1 To UBound(Session("PLOT"))
                    If Session("CompBackup")(i, j + 1) <> 0 Then
                        Session("PLOT")(i, ColumnToSet) = Session("CompBackup")(i, 1 + (v * NumberOfScenarios)) / Session("CompBackup")(i, j + 1) 'First Column of Variable Group / Selected Column of Variable Group
                    Else
                        Session("PLOT")(i, ColumnToSet) = DBNull.Value
                    End If
                Next
                ColumnToSet += 1
            Next
        Next
        Dim ColumnToEdit As Integer
        ColumnToEdit = 1
        For v = 0 To NumberOfVariables - 1
            For j = (v * NumberOfScenarios) + 1 To (NumberOfScenarios * v) + (NumberOfScenarios - 1)
                LabelScenarioComparison(j, NumberOfScenarios * v, ColumnToEdit, "/", True)
                ColumnToEdit = ColumnToEdit + 1
            Next j
        Next v

        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"

        Session("sComparison") = "RatAB"

        RefreshTable()
    End Sub

    Private Sub mnuCompRatOtherA_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Ratio"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") <= 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
        End If
        Dim i As Integer, j As Integer
        Dim Scenarios() As String
        ReDim Preserve Scenarios(0)
        SaveForComparison(ScenarioComparison:=True)
        'Find all Scenario Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim ScenarioStartPosition As Integer
            Dim ScenarioEndPosition As Integer

            ScenarioStartPosition = InStr(Session("CompLabelBackup")(1, i), "[")
            ScenarioEndPosition = InStr(Session("CompLabelBackup")(1, i), "]")

            Dim ScenarioText As String
            ScenarioText = Mid(Session("CompLabelBackup")(1, i), ScenarioStartPosition + 1, (ScenarioEndPosition - 1) - ScenarioStartPosition)
            If Len(Scenarios(0)) = 0 Then
                Scenarios(0) = ScenarioText
            Else
                If Scenarios.Contains(ScenarioText) = False Then
                    ReDim Preserve Scenarios(UBound(Scenarios) + 1)
                    Scenarios(UBound(Scenarios)) = ScenarioText
                End If
            End If
        Next
        If UBound(Scenarios) = 0 Then
            Exit Sub
        End If
        Dim NumberOfScenarios As Integer
        NumberOfScenarios = UBound(Scenarios) + 1
        Dim Variables() As String
        ReDim Preserve Variables(0)

        'Find all Variable Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim VariableEndPosition As Integer

            VariableEndPosition = InStr(Session("CompLabelBackup")(1, i), "[")

            Dim VariableText As String
            VariableText = Left(Session("CompLabelBackup")(1, i), VariableEndPosition - 1)
            If Len(Variables(0)) = 0 Then
                Variables(i - 1) = VariableText
            Else
                If Variables.Contains(VariableText) = False Then
                    ReDim Preserve Variables(UBound(Variables) + 1)
                    Variables(UBound(Variables)) = VariableText
                End If
            End If
        Next i

        Dim NumberOfVariables As Integer
        NumberOfVariables = UBound(Variables) + 1

        Session("COUNTE") = Session("COUNTE") - NumberOfVariables
        'Compute Comparison
        Dim ColumnToSet As Integer
        ColumnToSet = 1
        Dim v As Integer
        For v = 0 To NumberOfVariables - 1 'iterate each variable group
            For j = 1 + (v * NumberOfScenarios) To ((NumberOfScenarios * v) + (NumberOfScenarios - 1)) 'Iterate each scenario within the variable group
                For i = 1 To UBound(Session("PLOT"))
                    If Session("CompBackup")(i, j + 1) <> 0 Then
                        Session("PLOT")(i, ColumnToSet) = Session("CompBackup")(i, j + 1) / Session("CompBackup")(i, 1 + (v * NumberOfScenarios)) 'First Column of Variable Group / Selected Column of Variable Group
                    Else
                        Session("PLOT")(i, ColumnToSet) = DBNull.Value
                    End If
                Next
                ColumnToSet += 1
            Next
        Next
        Dim ColumnToEdit As Integer
        ColumnToEdit = 1
        For v = 0 To NumberOfVariables - 1
            For j = (v * NumberOfScenarios) + 1 To (NumberOfScenarios * v) + (NumberOfScenarios - 1)
                LabelScenarioComparison(j, NumberOfScenarios * v, ColumnToEdit, "/", False)
                ColumnToEdit = ColumnToEdit + 1
            Next j
        Next v

        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"

        Session("sComparison") = "RatAB"

        RefreshTable()
    End Sub

    Private Sub mnuCompDiffAOther_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Ratio"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") <= 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
        End If
        Dim i As Integer, j As Integer
        Dim Scenarios() As String
        ReDim Preserve Scenarios(0)
        SaveForComparison(ScenarioComparison:=True)
        'Find all Scenario Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim ScenarioStartPosition As Integer
            Dim ScenarioEndPosition As Integer

            ScenarioStartPosition = InStr(Session("CompLabelBackup")(1, i), "[")
            ScenarioEndPosition = InStr(Session("CompLabelBackup")(1, i), "]")

            Dim ScenarioText As String
            ScenarioText = Mid(Session("CompLabelBackup")(1, i), ScenarioStartPosition + 1, (ScenarioEndPosition - 1) - ScenarioStartPosition)
            If Len(Scenarios(0)) = 0 Then
                Scenarios(0) = ScenarioText
            Else
                If Scenarios.Contains(ScenarioText) = False Then
                    ReDim Preserve Scenarios(UBound(Scenarios) + 1)
                    Scenarios(UBound(Scenarios)) = ScenarioText
                End If
            End If
        Next
        If UBound(Scenarios) = 0 Then
            Exit Sub
        End If
        Dim NumberOfScenarios As Integer
        NumberOfScenarios = UBound(Scenarios) + 1
        Dim Variables() As String
        ReDim Preserve Variables(0)

        'Find all Variable Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim VariableEndPosition As Integer

            VariableEndPosition = InStr(Session("CompLabelBackup")(1, i), "[")

            Dim VariableText As String
            VariableText = Left(Session("CompLabelBackup")(1, i), VariableEndPosition - 1)
            If Len(Variables(0)) = 0 Then
                Variables(i - 1) = VariableText
            Else
                If Variables.Contains(VariableText) = False Then
                    ReDim Preserve Variables(UBound(Variables) + 1)
                    Variables(UBound(Variables)) = VariableText
                End If
            End If
        Next i

        Dim NumberOfVariables As Integer
        NumberOfVariables = UBound(Variables) + 1

        Session("COUNTE") = Session("COUNTE") - NumberOfVariables
        'Compute Comparison
        Dim ColumnToSet As Integer
        ColumnToSet = 1
        Dim v As Integer
        For v = 0 To NumberOfVariables - 1 'iterate each variable group
            For j = 1 + (v * NumberOfScenarios) To ((NumberOfScenarios * v) + (NumberOfScenarios - 1)) 'Iterate each scenario within the variable group
                For i = 1 To UBound(Session("PLOT"))
                    If Session("CompBackup")(i, j + 1) <> 0 Then
                        Session("PLOT")(i, ColumnToSet) = Session("CompBackup")(i, 1 + (v * NumberOfScenarios)) - Session("CompBackup")(i, j + 1) 'First Column of Variable Group / Selected Column of Variable Group
                    Else
                        Session("PLOT")(i, ColumnToSet) = DBNull.Value
                    End If
                Next
                ColumnToSet += 1
            Next
        Next
        Dim ColumnToEdit As Integer
        ColumnToEdit = 1
        For v = 0 To NumberOfVariables - 1
            For j = (v * NumberOfScenarios) + 1 To (NumberOfScenarios * v) + (NumberOfScenarios - 1)
                LabelScenarioComparison(j, NumberOfScenarios * v, ColumnToEdit, "-", False)
                ColumnToEdit = ColumnToEdit + 1
            Next j
        Next v

        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "blank.gif"

        Session("sComparison") = "RatAB"

        RefreshTable()
    End Sub

    Private Sub mnuCompDiffOtherA_Click()
        'jrs 2009/01/30
        ''check other display options, need to be off
        'If Session("PercentOn") = "True" Or Session("bCum") Or Session("bCumDis") Or Session("bDis") Or _
        'Session("bIntAvg") Or Session("bMovingAvg") Or Session("bPctTot") Then
        '    'MsgBox "All Display options need to be off in order to use comparison options.", vbInformation, "Comparison Options"
        '    Exit Sub
        'End If

        'jrs 2019/07/29
        If Session("bComparisonOn") = False Then Session("YAxisLabelOrg") = Session("YAxisLabel")
        Session("YAxisLabel") = "Ratio"
        If Session("bComparisonOn") Then
            mnuCompReset_Click()
        End If
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("COUNTE") <= 1 Then
                'MsgBox("Comparison is not possible with an odd number of variables loaded", vbInformation, "Comparison Options")
                Exit Sub
            End If
            'jrs 2009/01/30
            CType(Session("TblDispOpt"), Stack).Push("Comparison")
            'Save Original numbers
        End If
        Dim i As Integer, j As Integer
        Dim Scenarios() As String
        ReDim Preserve Scenarios(0)
        SaveForComparison(ScenarioComparison:=True)
        'Find all Scenario Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim ScenarioStartPosition As Integer
            Dim ScenarioEndPosition As Integer

            ScenarioStartPosition = InStr(Session("CompLabelBackup")(1, i), "[")
            ScenarioEndPosition = InStr(Session("CompLabelBackup")(1, i), "]")

            Dim ScenarioText As String
            ScenarioText = Mid(Session("CompLabelBackup")(1, i), ScenarioStartPosition + 1, (ScenarioEndPosition - 1) - ScenarioStartPosition)
            If Len(Scenarios(0)) = 0 Then
                Scenarios(0) = ScenarioText
            Else
                If Scenarios.Contains(ScenarioText) = False Then
                    ReDim Preserve Scenarios(UBound(Scenarios) + 1)
                    Scenarios(UBound(Scenarios)) = ScenarioText
                End If
            End If
        Next
        If UBound(Scenarios) = 0 Then
            Exit Sub
        End If
        Dim NumberOfScenarios As Integer
        NumberOfScenarios = UBound(Scenarios) + 1
        Dim Variables() As String
        ReDim Preserve Variables(0)

        'Find all Variable Groups
        For i = 1 To Session("COUNTE")
            Dim Found As Boolean
            Found = False
            Dim VariableEndPosition As Integer

            VariableEndPosition = InStr(Session("CompLabelBackup")(1, i), "[")

            Dim VariableText As String
            VariableText = Left(Session("CompLabelBackup")(1, i), VariableEndPosition - 1)
            If Len(Variables(0)) = 0 Then
                Variables(i - 1) = VariableText
            Else
                If Variables.Contains(VariableText) = False Then
                    ReDim Preserve Variables(UBound(Variables) + 1)
                    Variables(UBound(Variables)) = VariableText
                End If
            End If
        Next i

        Dim NumberOfVariables As Integer
        NumberOfVariables = UBound(Variables) + 1

        Session("COUNTE") = Session("COUNTE") - NumberOfVariables
        'Compute Comparison
        Dim ColumnToSet As Integer
        ColumnToSet = 1
        Dim v As Integer
        For v = 0 To NumberOfVariables - 1 'iterate each variable group
            For j = 1 + (v * NumberOfScenarios) To ((NumberOfScenarios * v) + (NumberOfScenarios - 1)) 'Iterate each scenario within the variable group
                For i = 1 To UBound(Session("PLOT"))
                    If Session("CompBackup")(i, j + 1) <> 0 Then
                        Session("PLOT")(i, ColumnToSet) = Session("CompBackup")(i, j + 1) - Session("CompBackup")(i, 1 + (v * NumberOfScenarios)) 'First Column of Variable Group / Selected Column of Variable Group
                    Else
                        Session("PLOT")(i, ColumnToSet) = DBNull.Value
                    End If
                Next
                ColumnToSet += 1
            Next
        Next
        Dim ColumnToEdit As Integer
        ColumnToEdit = 1
        For v = 0 To NumberOfVariables - 1
            For j = (v * NumberOfScenarios) + 1 To (NumberOfScenarios * v) + (NumberOfScenarios - 1)
                LabelScenarioComparison(j, NumberOfScenarios * v, ColumnToEdit, "-", False)
                ColumnToEdit = ColumnToEdit + 1
            Next j
        Next v

        Session("bComparisonOn") = True

        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompPctBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompRatOtherA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffAOther").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuCompOpt").Items("mnuCompDiffOtherA").IconImage = "checkbox.gif"

        Session("sComparison") = "RatAB"

        RefreshTable()
    End Sub
    Public Function NoNullDec(o As Object) As Decimal
        'ttt- function to convert null or empty to zero 
        Try
            Dim res = CDec(o)
            Return res
        Catch ex As Exception

        End Try
        Return 0
    End Function
    Public Shared Function NoNullDecStr(o As Object) As String
        'ttt- function to convert null to empty string
        Try
            ' Dim res = CDec(o).ToString
            Dim res = CStr(CDec(o))
            Return res
        Catch ex As Exception

        End Try
        Return ""
    End Function

    'cdv 2022/07/01 Need to guarantee LongVarTable set to false when the user leaves this form.
    Private Sub frm_TableDisplay_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        If IsPostBack Then
            Session("LongVarTable") = False
        End If
    End Sub


    'jrs 2010/12/07
    'Need to select list from Flex Displays
    'non-oecd, oecd
    'Base, 4 UNEP scenarios
    Private Sub mnuSaveGoogle_Click()

        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim f As Formula
        Dim bHF As Boolean

        'jrs 2023/09/25
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe


        'Find concepts to create dsmxl file
        'grd_TableDisplay.Visible = False
        'grd_TableDisplay.row = 0
        'Search for different Variables (Metrics), Topics, Scenarios in session("PLOT")
        Dim II As Integer, pos As Integer
        Dim T As Object, s As Object, g As Object, c As Object
        Dim sVar As String, idx As Integer, sSce As String, iSCE As Integer
        Dim Vars As New Collection, Tops As New Collection
        Dim Sces As New Collection, Gps As New Collection, cols As New Collection
        Dim sGroup As String
        Dim VarsG As New Collection, VarsET As New Collection, Vars2 As New Collection
        'jrs 2011/03/04
        Dim VarsD As New Collection
        Dim jjD As Integer, dd As Integer, tmpD As Object
        ReDim tmpD(Session("FinYear")(1))
        'jrs 2011/03/15
        'jrs 2012/09/27 Dim VarsWSA As New Collection
        'jrs 2012/09/27 Dim jjWSA As Integer, ww As Integer, tmpWSA As object
        'jrs 2012/09/27 ReDim tmpWSA(session("FinYear")(1))
        Dim bPOP As Boolean, PopColIdx As Integer

        Dim MaxVar2 As Integer
        Dim ss As Integer
        Dim jj As Integer, gg As Integer, jjET As Integer, jj2 As Integer
        Dim tmp As Object
        ReDim tmp(Session("FinYear")(1))
        'Dim bForm As Boolean,
        Dim sUnits As String
        Dim Grps2(,,,,)
        Dim sTopics As String

        For II = 1 To Session("COUNTE")
            'Generates error when Item not found, then add it
            'On Error Resume Next
            'Variables (Metrics)
            'vX = Nothing
            pos = InStr(Session("VARN")(1, II), "[")
            If pos > 0 Then
                sVar = Mid(Session("VARN")(1, II), 1, pos - 1)
            Else
                sVar = Session("VARN")(1, II)
            End If
            If Not Vars.Contains(sVar) Then
                Vars.Add(II, sVar)
            End If
            idx = IfsVars.Item(sVar).Index
            If idx < 0 Then
                'Go search for a variable to check if it uses gender
                idx = SearchVarIdxWGenderInForm(Session("aFormulas")(II))
            End If
            If sVar = "POP" Then
                bPOP = True
            End If
            'jrs 2011/03/04
            'Disease
            If VDim2(idx) = 27 Then
                If Not VarsD.Contains(sVar) Then
                    VarsD.Add(II, sVar)
                End If
            End If
            'Gender
            If VDim2(idx) = 23 Or VDim3(idx) = 23 Then
                If Not VarsG.Contains(sVar) Then
                    VarsG.Add(II, sVar)
                End If
            End If
            'Energy Type
            If VDim2(idx) = 5 Then
                If Not VarsET.Contains(sVar) Then
                    VarsET.Add(II, sVar)
                End If
            End If
            'jrs 2012/09/27
            'WatSan Access
            '        If VDim2(idx) = 28 Then
            '            vX = VarsWSA.Item(sVar)
            '            If IsEmpty(vX) Then
            '                VarsWSA.Add II, sVar
            '            End If
            '        End If
            'Vars with 2 Dims
            If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) > 0 _
            And VDim2(idx) <> 27 And VDim2(idx) <> 29 Then 'jrs 2017/10/04 added And VDim2(idx) <> 29  'jrs 2012/09/27 And VDim2(idx) <> 28
                If Not Vars2.Contains(sVar) Then
                    Vars2.Add(II, sVar)
                    If MaxVar2 < MaxVal(VDim2(idx)) - 1 Then
                        MaxVar2 = MaxVal(VDim2(idx)) - 1
                    End If
                End If
            End If

            'Topics
            If VCatName(idx) > "" Then
                sTopics = VCatName(idx)
                pos = InStr(sTopics, ",")
                If pos > 0 Then
                    sTopics = Mid(sTopics, 1, pos - 1)
                End If
                If Tops.Contains(sTopics) Then
                    T = Tops.Item(sTopics)
                Else
                    Tops.Add(II, sTopics)
                End If
            ElseIf Session("sUsrDefCats")(II) > "" Then
                If Tops.Contains(Session("sUsrDefCats")(II)) Then
                    T = Tops.Item(Session("sUsrDefCats")(II))
                Else
                    Tops.Add(II, Session("sUsrDefCats")(II))
                End If
            Else
                If Tops.Contains("General") Then
                    T = Tops.Item("General")
                Else
                    Tops.Add(II, "General")
                End If
            End If

            'Scenarios
            iSCE = getFileIdx(II)
            If iSCE < 0 Then
                grd_TableDisplay.Visible = True
                Response.Write("<script>alert('Please use this option only with Forecast.');</script>")
                Exit Sub
            End If
            'jrs 2025/09/04 handling 0 for Working file
            If iSCE = 0 Then
                sSce = "Working"
            Else
                sSce = Replace(Replace(Replace(Session("RunFiles")(iSCE), Session("PutFdrvRun"), ""), ".RUN.DB", ""), ".run.db", "")
            End If
            If UCase(sSce) = "IFSBASE" Then
                sSce = "Base"
            End If
            If Sces.Contains(sSce) Then
                s = Sces.Item(sSce)
            Else
                Sces.Add(II, sSce)
            End If
            'Groups
            If Session("bAlias") Then
                sGroup = Session("oGR1N")(II)
            Else
                sGroup = Session("GR1N")(II)
            End If
            If gGetGroupIdx(sGroup, False, Me) > 0 Then
                If Gps.Contains(Session("GR1N")(II)) Then
                    g = Gps.Item(Session("GR1N")(II))
                Else
                    Gps.Add(II, Session("GR1N")(II))
                End If

                'Columns
                If cols.Contains(CStr(iSCE) & Session("GR1N")(II) & sVar) Then
                    c = cols.Item(CStr(iSCE) & Session("GR1N")(II) & sVar)
                Else
                    cols.Add(II, CStr(iSCE) & Session("GR1N")(II) & sVar)
                End If
            Else
                grd_TableDisplay.Visible = True
                Response.Write("<script>alert('Please use this option only with Groups.');</script>")
                Exit Sub
            End If

        Next II

        Try
            'jrs 2011/03/17 age categories, only for population
            'POPLE15
            'POP15TO65
            'POPGT65
            Dim POPIdxs(3) As Integer
            POPIdxs(1) = IfsVars.Item("POPLE15").Index
            POPIdxs(2) = IfsVars.Item("POP15TO65").Index
            POPIdxs(3) = IfsVars.Item("POPGT65").Index

            Dim iRowPosition As Integer, iFY As Integer
            iRowPosition = 1
            iFY = Session("FirstYear")
            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                iFY = FirstYearForecast
                iRowPosition = LastYearHistory - FirstYearHistory + 1
            End If

            ReDim Grps2(Session("FinYear")(1), Sces.Count, Vars.Count, Gps.Count, 10) 'assuming max # of dim2 is 10

            'jrs 2011/01/06
            'Create xml file
            CreateDataSetXML(Tops, Vars, VarsG, VarsET, VarsD)
            'Handling doubly dimensioned variables
            Dim vv As Integer, vvTot As Integer, bDim2 As Boolean

            'Create Scenario file
            FileOpen(1, Session("PutFdrvRun") & "PDE\scenarios.csv", OpenMode.Output)
            PrintLine(1, "scenario, name")
            For II = 1 To Sces.Count
                idx = getFileIdx(Sces.Item(II))
                If idx > 0 Then
                    sSce = Replace(Replace(Replace(Session("RunFiles")(idx), Session("PutFdrvRun") & "", ""), ".RUN", ""), ".run", "")
                Else
                    sSce = "Working Scenario"
                End If
                If UCase(sSce) = "IFSBASE" Then
                    sSce = "Base Case"
                End If
                PrintLine(1, CStr(idx) & ", " & sSce)
            Next II
            FileClose(1)

            'Create Groups file
            FileOpen(1, Session("PutFdrvRun") & "PDE\groups.csv", OpenMode.Output)
            PrintLine(1, "group, name")
            For II = 1 To Gps.Count
                If Session("bAlias") Then
                    sGroup = Session("oGR1N")(Gps.Item(II))
                Else
                    sGroup = Session("GR1N")(Gps.Item(II))
                End If

                idx = gGetGroupIdx(sGroup, False, Me)
                If idx > 0 Then
                    PrintLine(1, CStr(idx) & ", " & Session("GR1N")(Gps.Item(II)))
                End If
            Next II
            FileClose(1)

            'Load UN Groups
            Dim rs As DataTable
            Dim UNGroupNames() As String, UNGroupAlias() As String, UNGroupCount As Integer

            SetDataTablefromSQLite(Session("IFs_MDB2Active"), "SELECT d.Region, f.Alias FROM GList d, GListId i, GroupIDNewFull f " &
            "Where i.Code = D.GListCode AND d.Region = f.GroupName AND i.Description = 'UN Regions' ORDER BY GroupNum", "", rs)

            UNGroupCount = rs.Rows.Count
            ReDim UNGroupNames(UNGroupCount), UNGroupAlias(UNGroupCount)
            For II = 1 To UNGroupCount
                UNGroupNames(II) = rs.Rows(II - 1).Item("Region")
                UNGroupAlias(II) = IIf(IsDBNull(rs.Rows(II - 1).Item("Alias")), "", rs.Rows(II - 1).Item("Alias")) 'jrs 2012/05/17
            Next II
            rs.Dispose()

            'Create UN Groups file
            FileOpen(1, Session("PutFdrvRun") & "PDE\ungroups.csv", OpenMode.Output)
            PrintLine(1, "ungroup, name")
            For II = 1 To UNGroupCount
                sGroup = UNGroupNames(II)

                idx = gGetGroupIdx(sGroup, False, Me)
                If idx > 0 Then
                    PrintLine(1, CStr(idx) & ", " & UNGroupAlias(II))
                End If
            Next II
            FileClose(1)

            'Save Group Slice
            Dim j As Integer, k As Integer, sFile As String, y As Integer, sYr As String
            Dim sDim1 As String
            FileOpen(1, Session("PutFdrvRun") & "PDE\group_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,group,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then 'jrs 2012/10/01 And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If
                    'jrs 2017/10/03 added the change from & to and for all occurrences of this string in the form
                    Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
                Next vv
            Next j
            PrintLine(1, "")

            With grd_TableDisplay
                'for each scenario
                For k = 1 To Sces.Count
                    sFile = CStr(getFileIdx(Sces.Item(k)))
                    'jrs 2023/09/25
                    MyFileforDisplay = FilesCol("FileForDisplay" + sFile.Trim)

                    'for each group
                    For II = 1 To Gps.Count
                        sDim1 = Session("GR1N")(Gps.Item(II))
                        'jrs 2010/12/13
                        If Session("bAlias") Then
                            gg = gGetGroupIdx(Session("oGR1N")(Gps.Item(II)), False, Me)
                        Else
                            gg = gGetGroupIdx(sDim1, False, Me)
                        End If
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                'Read Variable
                                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                                If pos > 0 Then
                                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                                Else
                                    sVar = Session("VARN")(1, Vars.Item(j))
                                End If

                                idx = AMAX(IfsVars.Item(sVar).Index, 0)
                                If idx = 0 Then
                                    sUnits = Session("aFormulas")(Vars.Item(j)).Unit
                                Else
                                    sUnits = VUnits(idx)
                                    'jrs 2023/09/25
                                    MyDF = dfCol("df" + sVar + sFile.Trim)
                                End If

                                bDim2 = False
                                vvTot = 1
                                If idx > 0 Then
                                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then 'jrs 2012/10/01 And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                                        If VDim2(idx) > 0 Then
                                            vvTot = MaxVal(VDim2(idx)) - 1
                                            bDim2 = True
                                        End If
                                    End If
                                    'jrs 2012/09/27
                                    '                            'jrs 2011/03/16
                                    '                            If VDim2(idx) = 28 Then
                                    '                                vvTot = 0
                                    '                            End If
                                End If

                                If Not bDim2 Then
                                    'jrs 2011/03/16
                                    If vvTot > 0 Then
                                        'find value to save
                                        Dim Mycol = cols.Item(sFile & sDim1 & sVar)
                                        'Continue saving the line
                                        'jrs 2010/12/13 printline(1, ", " & CStr(val(Format(.text, "#####################.#########")));
                                        If sUnits = "Mil People" Or sUnits = "Million Years" Then
                                            Print(1, ", " & CStr(Math.Round(Session("PLOT")(CInt(sYr) - Session("FirstYear") + 1, Mycol) * 1000000, 0)))
                                        ElseIf sUnits = "Thousand $" Then
                                            Print(1, ", " & CStr(Math.Round(Session("PLOT")(CInt(sYr) - Session("FirstYear") + 1, Mycol) * 1000, 0)))
                                        ElseIf sUnits = "Billion $" Or sUnits = "Bil Barrels OE" Or sUnits = "Billion Tons" Then
                                            Print(1, ", " & CStr(Math.Round(Session("PLOT")(CInt(sYr) - Session("FirstYear") + 1, Mycol) * 1000000000, 0)))
                                        Else
                                            'jrs 2023/09/25 Print(1, ", " & CStr(Val(Format(.Rows(y).Cells(Mycol).Text, "#####################.#########"))))
                                            Print(1, ", " & CStr(Val(.Rows(y).Cells(Mycol).Text)))
                                        End If
                                    End If
                                Else
                                    'Load all dimensions in the 1st year
                                    If y = 6 Then
                                        For vv = 1 To vvTot
                                            gLoadForDisplayNET(sVar, idx, "", gg, GRPS(VDim2(idx), vv), vv, "Total", -1, "", CInt(sFile), False, False, True, 1, Session("PLOT"), Session("COUNTE") + 1,
                                            iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)

                                            '*******************************************
                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                gLoadForDisplayHis(sVar, GroupNames$(II), gg, GRPS(VDim2(idx), vv), vv, "Total", -1,
                                                True, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1, False,
                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))

                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                            End If
                                            For yy = 1 To Session("FinYear")(1)
                                                Grps2(yy, k, j, II, vv) = Session("PLOT")(yy, Session("COUNTE") + 1)
                                            Next yy
                                        Next vv
                                    End If
                                    'Print results
                                    For vv = 1 To vvTot
                                        If sUnits = "Mil People" Or sUnits = "Million Years" Then
                                            Print(1, ", " & CStr(Math.Round(Grps2(CInt(sYr) - Session("FirstYear") + 1, k, j, II, vv) * 1000000, 0)))
                                        ElseIf sUnits = "Thousand $" Then
                                            Print(1, ", " & CStr(Math.Round(Grps2(CInt(sYr) - Session("FirstYear") + 1, k, j, II, vv) * 1000, 0)))
                                        ElseIf sUnits = "Billion $" Or sUnits = "Bil Barrels OE" Or sUnits = "Billion Tons" Then
                                            Print(1, ", " & CStr(Math.Round(Grps2(CInt(sYr) - Session("FirstYear") + 1, k, j, II, vv) * 1000000000, 0)))
                                        Else
                                            Print(1, ", " & CStr(Grps2(CInt(sYr) - Session("FirstYear") + 1, k, j, II, vv)))
                                        End If
                                    Next vv
                                End If

                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next II
                Next k

                FileClose(1)
            End With

            'Load ISO translation array
            Dim sql As String
            Dim ISO() As String, ISOlat() As Single, ISOlong() As Single, ISOname() As String, ISOcode() As String
            ReDim ISO(NR), ISOlat(NR), ISOlong(NR), ISOname(NR), ISOcode(NR)
            sql = "SELECT [Country Translation].[ISO3166-2 Codes], RegionIDNewFull.RegionNum," &
            " [Country Google].latitude, [Country Google].longitude, [Country Google].[name]" &
            " FROM [Country Translation], RegionalizationFull, RegionIDNewFull, [Country Google]" &
            " WHERE [Country Translation].Country = RegionalizationFull.Country" &
            " AND RegionalizationFull.NewRegion = RegionIDNewFull.RegionNum" &
            " AND [Country Translation].[ISO3166-2 Codes] = [Country Google].country" &
            " ORDER BY [Country Translation].[ISO3166-2 Codes]"

            SetDataTablefromSQLite(Session("IFs_MDB2Active"), sql, "", rs)
            idx = 0
            Dim rr As Integer = 0
            Do While rr < rs.Rows.Count
                'jrs 2023/09/25 removed [] for sqlite 
                If Not IsDBNull(rs.Rows(rr).Item("ISO3166-2 Codes")) Then
                    idx = idx + 1
                    ISOcode(idx) = rs.Rows(rr).Item("ISO3166-2 Codes")
                    ISOlat(idx) = rs.Rows(rr).Item("latitude")
                    ISOlong(idx) = rs.Rows(rr).Item("longitude")
                    ISOname(idx) = rs.Rows(rr).Item("name")
                End If
                rr = rr + 1
            Loop
            rs.Dispose()

            sql = "SELECT [Country Translation].[ISO3166-2 Codes], RegionIDNewFull.RegionNum" &
            " FROM [Country Translation], RegionalizationFull, RegionIDNewFull" &
            " WHERE [Country Translation].Country = RegionalizationFull.Country" &
            " AND RegionalizationFull.NewRegion = RegionIDNewFull.RegionNum"

            SetDataTablefromSQLite(Session("IFs_MDB2Active"), sql, "", rs)
            rr = 0
            Do While rr < rs.Rows.Count
                'jrs 2023/09/25 removed [] for sqlite 
                If Not IsDBNull(rs.Rows(rr).Item("ISO3166-2 Codes")) Then
                    ISO(rs.Rows(rr).Item("RegionNum")) = rs.Rows(rr).Item("ISO3166-2 Codes")
                End If
                rr = rr + 1
            Loop
            rs.Dispose()

            'Save country slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\country_scenario_slice.csv", OpenMode.Output)

            Dim m As Integer ', sReg() As String,
            Dim Vals(,,,) As Object
            Dim ValsG(,,,,) As Object, ValsET(,,,,) As Object
            Dim iPos As Integer, iPos2 As Integer, iPos3 As Integer, level As Single
            Dim bPoverty As Boolean, bPctPop As Boolean, bFind As Boolean
            'jrs 2014/10/21
            Dim b2005PPP As Boolean, iPos4 As Integer, iPos5 As Integer, level2 As Single, bIncRange As Boolean
            Dim bMortChl As Boolean, bMortAdl As Boolean
            Dim cc As Integer ', reg As Integer
            Dim RegGrps() As Integer, Vals2(,,,,) As Object
            ReDim Vals(Session("FinYear")(1), Vars.Count, Sces.Count, NR)
            ReDim ValsG(Session("FinYear")(1), VarsG.Count, Sces.Count, NR, 2)
            ReDim Vals2(Session("FinYear")(1), Vars2.Count, Sces.Count, NR, MaxVar2)
            ReDim ValsET(Session("FinYear")(1), VarsET.Count, Sces.Count, NR, 6)
            ReDim RegGrps(NR)
            'jrs 2011/03/08
            Dim ValsD(,,,,,) As Object
            ReDim ValsD(Session("FinYear")(1), VarsD.Count, Sces.Count, NR, 3, 3)
            Dim ddMap() As Integer
            ReDim ddMap(MAXCOM(27) - 1)
            '1 is Comm. Dis, 2 Non Comm Dis and 3 Injuries
            ddMap(1) = 1
            ddMap(2) = 2
            ddMap(3) = 2
            ddMap(4) = 2
            ddMap(5) = 2
            ddMap(6) = 2
            ddMap(7) = 3
            ddMap(8) = 3
            ddMap(9) = 3
            ddMap(10) = 2
            ddMap(11) = 1 'AIDS
            ddMap(12) = 1
            ddMap(13) = 1
            ddMap(14) = 1
            ddMap(15) = 2
            'jrs 2011/03/16
            Dim ValsP(,,,) As Object 'jrs 2012/09/28 ValsWSA() As object,
            'jrs 2012/09/28 ReDim ValsWSA(session("FinYear")(1), VarsWSA.Count, Sces.Count, NR, 3), ValsP(session("FinYear")(1), Sces.Count, NR, 3)
            ReDim ValsP(Session("FinYear")(1), Sces.Count, NR, 3)

            Print(1, "scenario,country,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 27 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29  'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If
                    Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
                Next vv
            Next j
            PrintLine(1, "")

            Dim GrpsD As Object, GrpsET As Object, GrpsG As Object, GrpsP As Object
            Dim UNGrps As Object, UNGrpsG As Object, UNGrps2 As Object, UNGrpsET As Object
            Dim Wrld As Object, WrldG As Object, Wrld2 As Object, WrldET As Object
            Dim WrldD As Object
            Dim UNGrpsD As Object
            Dim UNGrpsWSA As Object, UNGrpsP As Object
            Dim WrldP As Object

            With grd_TableDisplay
                'For each group
                'Do all countries instead
                'For m = 1 To Gps.Count
                '    If Session("bAlias") Then
                '        sDim1 = session("oGR1N")(Gps.Item(m))
                '    Else
                '        sDim1 = session("GR1N")(Gps.Item(m))
                '    End If
                '    'Decompose Group
                '    sReg = gLoadCountries(sDim1)
                'for each member of the group
                '    For II = 0 To gCountGroupMembers - 1
                For II = 1 To NR
                    'Save to country array
                    '                reg = IFsRegs.Item(sReg(II)).Index
                    '                RegGrps(reg) = gGetGroupIdx(sDim1, False)
                    'for each scenario
                    For k = 1 To Sces.Count
                        sFile = CStr(getFileIdx(Sces.Item(k)))
                        jj = 0
                        jjET = 0
                        jj2 = 0
                        jjD = 0
                        'jrs 2012/09/27 jjWSA = 0
                        'For each variable/metric
                        For j = 1 To Vars.Count
                            cc = Vars.Item(j)
                            If jj < VarsG.Count Then
                                If VarsG.Item(jj + 1) = cc Then
                                    jj = jj + 1
                                End If
                            End If
                            If jjET < VarsET.Count Then
                                If VarsET.Item(jjET + 1) = cc Then
                                    jjET = jjET + 1
                                End If
                            End If
                            If jj2 < Vars2.Count Then
                                If Vars2.Item(jj2 + 1) = cc Then
                                    jj2 = jj2 + 1
                                End If
                            End If
                            If jjD < VarsD.Count Then
                                If VarsD.Item(jjD + 1) = cc Then
                                    jjD = jjD + 1
                                End If
                            End If
                            'jrs 2012/09/27
                            '                    If jjWSA < VarsWSA.Count Then
                            '                        If VarsWSA.Item(jjWSA + 1) = cc Then
                            '                            jjWSA = jjWSA + 1
                            '                        End If
                            '                    End If
                            'Read Variable
                            pos = InStr(Session("VARN")(1, cc), "[")
                            If pos > 0 Then
                                sVar = Mid(Session("VARN")(1, cc), 1, pos - 1)
                            Else
                                sVar = Session("VARN")(1, cc)
                            End If
                            iPos = InStr(sVar, "INCOMELT")
                            iPos2 = InStr(sVar, "LN")
                            iPos3 = InStr(sVar, "%")
                            bPoverty = False
                            bPctPop = False
                            bFind = False
                            'jrs 2014/10/21
                            'jrs 2016/11/07 b2005PPP = True
                            b2005PPP = False
                            bIncRange = False
                            iPos4 = InStr(sVar, "GT")
                            'jrs 2016/11/07 iPos5 = InStr(sVar, "2011")
                            iPos5 = InStr(sVar, "2005")
                            If iPos > 0 And iPos2 > 0 Then
                                bFind = (iPos3 > 0 And iPos3 < iPos2)
                                If bFind Then
                                    level = Val(Mid(sVar, 9, iPos2 - 10))
                                Else
                                    'jrs 2014/10/21
                                    If iPos4 > 0 Then
                                        level2 = Val(Mid(sVar, 9, iPos4 - 9))
                                        'jrs 2013/03/12 level = Val(mid(sVar, iPos4 + 2, iPos2 - 11 - Len(CStr(level))))
                                        level = Val(Mid(sVar, iPos4 + 2, iPos2 - 11 - Len(CStr(level2))))
                                        bIncRange = True
                                        If InStr(sVar, "%") > 0 Then
                                            bPctPop = True
                                        End If
                                    Else
                                        level = Val(Mid(sVar, 9, iPos2 - 9))
                                        'jrs 2016/11/07
                                        If iPos5 > iPos2 Then
                                            'jrs 2016/11/07 If level <> 1 And level <> 2 Then
                                            If level <> 125 And level <> 200 Then
                                                bPoverty = True
                                            End If
                                        Else 'jrs 2016/11/07
                                            'jrs 2023/09/25 updated levels
                                            If level <> 215 And level <> 365 And level <> 580 Then
                                                bPoverty = True
                                            End If
                                        End If
                                        If Right(sVar, 1) = "%" Then
                                            bPctPop = True
                                        End If
                                    End If
                                End If
                            End If
                            'jrs 2014/10/21
                            If iPos5 > iPos2 Then
                                'jrs 2016/11/07 b2005PPP = False
                                b2005PPP = True
                            End If
                            bMortChl = (InStr(sVar, "POPCHLDMOR") > 0)
                            bMortAdl = (InStr(sVar, "POPADLTMOR") > 0)

                            If bPoverty Or bFind Then
                                'jrs 2023/09/25 updated variable
                                idx = IfsVars.Item("INCOMELT215LN").Index
                            ElseIf bPctPop Then
                                idx = IfsVars.Item(Replace(sVar, "%", "")).Index
                            ElseIf bMortChl Or bMortAdl Then
                                idx = IfsVars.Item("DEATHCAT").Index
                            Else
                                idx = IfsVars.Item(sVar).Index
                            End If

                            'Define if Var is Formula
                            If UBound(Session("aFormulas")) >= cc Then
                                If Not (Session("aFormulas")(cc) Is Nothing) Then
                                    If Session("VARN")(1, cc) = Session("aFormulas")(cc).Name Then
                                        bUsingComp = True
                                        f = Session("aFormulas")(cc)
                                    Else
                                        bUsingComp = False
                                    End If
                                Else
                                    bUsingComp = False
                                End If
                            Else
                                bUsingComp = False
                            End If

                            bDim2 = False
                            vvTot = 1
                            If idx > 0 Then
                                If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 27 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                                    If VDim2(idx) > 0 Then
                                        vvTot = MaxVal(VDim2(idx)) - 1
                                        bDim2 = True
                                    End If
                                End If
                                'jrs 2012/09/27
                                '                        'jrs 2011/03/16
                                '                        If VDim2(idx) = 28 Then
                                '                            vvTot = 0
                                '                        End If
                            End If

                            If bUsingComp Then 'Using Computations
                                bHF = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory And f.Formula2 = "")
                                'Gender
                                If jj > 0 Then
                                    If VarsG.Item(jj) = cc Then
                                        For gg = 1 To 2
                                            If Session("GR2N")(cc) = "Total" Then
                                                DecComp(CInt(cc), GRPS(1, II), GRPS(23, gg), Session("GR3N")(cc), f, bHF, CInt(sFile))
                                            Else
                                                DecComp(CInt(cc), GRPS(1, II), Session("GR2N")(cc), GRPS(23, gg), f, bHF, CInt(sFile))
                                            End If
                                            For y = 1 To Session("FinYear")(1)
                                                ValsG(y, jj, k, II, gg) = Session("PLOT")(y, Session("COUNTE") + 1)
                                                If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                                    ValsG(y, jj, k, II, gg) = Math.Round(ValsG(y, jj, k, II, gg) * 1000000, 0)
                                                ElseIf f.Unit = "Thousand $" Then
                                                    ValsG(y, jj, k, II, gg) = Math.Round(ValsG(y, jj, k, II, gg) * 1000, 0)
                                                ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                                    ValsG(y, jj, k, II, gg) = Math.Round(ValsG(y, jj, k, II, gg) * 1000000000, 0)
                                                End If
                                            Next y
                                        Next gg
                                    End If
                                End If
                                DecComp(CInt(cc), GRPS(1, II), Session("GR2N")(cc), Session("GR3N")(cc), f, bHF, CInt(sFile))
                                If f.Formula2 > "" Then
                                    Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                    Exit Sub

                                    'DecCompHis(cc, GRPS(1, II), f)
                                End If
                                'Move result to Vals
                                For y = 1 To Session("FinYear")(1)
                                    Vals(y, j, k, II) = Session("PLOT")(y, Session("COUNTE") + 1)
                                    If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                        Vals(y, j, k, II) = Math.Round(Vals(y, j, k, II) * 1000000, 0)
                                    ElseIf f.Unit = "Thousand $" Then
                                        Vals(y, j, k, II) = Math.Round(Vals(y, j, k, II) * 1000, 0)
                                    ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                        Vals(y, j, k, II) = Math.Round(Vals(y, j, k, II) * 1000000000, 0)
                                    End If
                                Next y
                            Else 'No Computations
                                If bPoverty Or bPctPop Then
                                    Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                    Exit Sub
                                    Session("COUNTE") = Session("COUNTE") + 1

                                    gLoadPovertyDisp(level, GRPS(1, II), CInt(sFile), False, bPctPop, b2005PPP,
                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                    Session("SensitivityFlag"), Session("mnuSQLite"))
                                    Session("COUNTE") = Session("COUNTE") - 1
                                ElseIf bFind Then
                                    Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                    Exit Sub
                                    Session("COUNTE") = Session("COUNTE") + 1

                                    gFindPovertyLevel(GRPS(1, II), CInt(sFile), False, level, b2005PPP,
                                    Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                    Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                                    Session("COUNTE") = Session("COUNTE") - 1
                                    'jrs 2010/05/14
                                ElseIf bMortChl Then
                                    Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                    Exit Sub
                                    Session("COUNTE") = Session("COUNTE") + 1
                                    '        gLoadMortProbChldL5 Session("COUNTE"), "", Nothing, True, True, GRPS(1, II), II, False, Session("GR2N")(cc), Session("GR3N")(cc),
                                    'Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")), Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")), Session("VARN")(0, Session("COUNTE")),
                                    'GR4N(Session("COUNTE")), "", "", CInt(sFile), False, True
                                    Session("COUNTE") = Session("COUNTE") - 1
                                ElseIf bMortAdl Then
                                    Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                    Exit Sub
                                    Session("COUNTE") = Session("COUNTE") + 1
                                    '        gLoadMortProbAdlt Session("COUNTE"), "", Nothing, True, True, GRPS(1, II), II, False, Session("GR2N")(cc), Session("GR3N")(cc),
                                    'Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")), Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")), Session("VARN")(0, Session("COUNTE")),
                                    'GR4N(Session("COUNTE")), "", "", CInt(sFile), False, True
                                    Session("COUNTE") = Session("COUNTE") - 1
                                Else
                                    'jrs 2023/09/25
                                    MyFileforDisplay = FilesCol("FileForDisplay" + sFile.Trim)
                                    MyDF = dfCol("df" + sVar + sFile.Trim)

                                    If Not bDim2 Then
                                        'jrs 2011/03/16
                                        If vvTot > 0 Then
                                            gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, False,
                                            1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                            '*******************************************
                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                gLoadForDisplayHis(sVar, GRPS(1, II), II, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1,
                                        False, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1,
                                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                            End If
                                            '*******************************************
                                            'Age Cat
                                            If sVar = "POP" Then
                                                'Read 3 age categories
                                                For gg = 1 To 3
                                                    'jrs 2023/09/26
                                                    Dim bNew As Boolean = False
                                                    If Not dfCol.Contains("df" + V(POPIdxs(gg)) + sFile.Trim) Then
                                                        Dim MyDF1 As IDataframe
                                                        dfCol.Add(MyDF1, "df" + V(POPIdxs(gg)) + sFile.Trim)
                                                        bNew = True
                                                    End If
                                                    MyDF = dfCol("df" + V(POPIdxs(gg)) + sFile.Trim)

                                                    gLoadForDisplayNET("", POPIdxs(gg), GRPS(1, II), II, "", -1, "", -1, "", CInt(sFile), False, False, False,
                                                    1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)

                                                    'jrs 2023/09/26
                                                    If bNew Then
                                                        dfCol.Remove("df" + V(POPIdxs(gg)) + sFile.Trim)
                                                        dfCol.Add(MyDF, "df" + V(POPIdxs(gg)) + sFile.Trim)
                                                    End If

                                                    '*******************************************
                                                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                        'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                        gLoadForDisplayHis(V(POPIdxs(gg)), GRPS(1, II), II, "", -1, "", -1,
                                                        False, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1,
                                                        True, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                        Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                    End If
                                                    '*******************************************
                                                    'If sVar = "POP" And k = 2 Then Stop
                                                    For y = 1 To Session("FinYear")(1)
                                                        ValsP(y, k, II, gg) = Math.Round(tmp(y) * 1000000, 0)
                                                    Next y
                                                Next gg
                                                'Move total to index 0
                                                For y = 1 To Session("FinYear")(1)
                                                    ValsP(y, k, II, 0) = Math.Round(Session("PLOT")(y, Session("COUNTE") + 1) * 1000000, 0)
                                                Next y
                                            End If
                                            'Gender
                                            If jj > 0 Then
                                                If VarsG.Item(jj) = cc Then
                                                    For gg = 1 To 2
                                                        'jrs 2011/03/08 If session("GR2N")(cc) = "Total" Then
                                                        If Session("GR2N")(cc) = "Total" And Session("GR3N")(cc) = "" Then
                                                            gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, GRPS(23, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, False,
                                                            1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                            '*******************************************
                                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                gLoadForDisplayHis(sVar, GRPS(1, II), II, GRPS(23, gg), gg, Session("GR3N")(cc), -1,
                                                                False, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                            End If
                                                            '*******************************************
                                                        Else
                                                            gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, Session("GR2N")(cc), -1, GRPS(23, gg), gg, "", CInt(sFile), False, False, False,
                                                            1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                            '*******************************************
                                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                gLoadForDisplayHis(sVar, GRPS(1, II), II, Session("GR2N")(cc), -1, GRPS(23, gg), gg,
                                                        False, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                        True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                            End If
                                                            '*******************************************
                                                            'jrs 2011/03/08
                                                            'Disease Type
                                                            If jjD > 0 Then
                                                                If VarsD.Item(jjD) = cc Then
                                                                    For dd = 1 To MAXCOM(27) - 1
                                                                        gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, GRPS(27, dd), dd, GRPS(23, gg), gg, "", CInt(sFile), False, False, False,
                                                                        1, tmpD, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                                        '*******************************************
                                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                            gLoadForDisplayHis(sVar, GRPS(1, II), II, GRPS(27, dd), dd, GRPS(23, gg), gg,
                                                                            False, 1, tmpD, 1, FirstYearHistory, LastYearHistory - 1, True, True,
                                                                            Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                                        End If
                                                                        '*******************************************
                                                                        For y = 1 To Session("FinYear")(1)
                                                                            ValsD(y, jjD, k, II, gg, ddMap(dd)) = ValsD(y, jjD, k, II, gg, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                            ValsD(y, jjD, k, II, 3, ddMap(dd)) = ValsD(y, jjD, k, II, 3, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                        Next y
                                                                    Next dd
                                                                End If
                                                            End If
                                                        End If
                                                        For y = 1 To Session("FinYear")(1)
                                                            ValsG(y, jj, k, II, gg) = tmp(y)
                                                            If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                ValsG(y, jj, k, II, gg) = Math.Round(ValsG(y, jj, k, II, gg) * 1000000, 0)
                                                            ElseIf VUnits(idx) = "Thousand $" Then
                                                                ValsG(y, jj, k, II, gg) = Math.Round(ValsG(y, jj, k, II, gg) * 1000, 0)
                                                            ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                ValsG(y, jj, k, II, gg) = Math.Round(ValsG(y, jj, k, II, gg) * 1000000000, 0)
                                                            End If
                                                        Next y
                                                    Next gg
                                                End If 'Loading Gender specific forecast
                                            End If 'haven't started with Gender specific forecast
                                            'Energy Type
                                            If jjET > 0 Then
                                                If VarsET.Item(jjET) = cc Then
                                                    For gg = 1 To 6
                                                        gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, GRPS(5, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, False,
                                                1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                        '*******************************************
                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                            gLoadForDisplayHis(sVar, GRPS(1, II), II, GRPS(5, gg), -1, Session("GR3N")(cc), -1,
                                                    False, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                    True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                        End If
                                                        '*******************************************
                                                        For y = 1 To Session("FinYear")(1)
                                                            ValsET(y, jjET, k, II, gg) = tmp(y)
                                                            If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                ValsET(y, jjET, k, II, gg) = Math.Round(ValsET(y, jjET, k, II, gg) * 1000000, 0)
                                                            ElseIf VUnits(idx) = "Thousand $" Then
                                                                ValsET(y, jjET, k, II, gg) = Math.Round(ValsET(y, jjET, k, II, gg) * 1000, 0)
                                                            ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                ValsET(y, jjET, k, II, gg) = Math.Round(ValsET(y, jjET, k, II, gg) * 1000000000, 0)
                                                            End If
                                                        Next y
                                                    Next gg
                                                End If 'Loading Energy type specific forecast
                                            End If 'haven't started with Energy type specific forecast
                                        Else 'Doing WatSan Access
                                            'jrs 2012/09/27
                                            '                                    'WatSan Access
                                            '                                    If jjWSA > 0 Then
                                            '                                        If VarsWSA.Item(jjWSA) = cc Then
                                            '                                            For gg = 1 To 3
                                            '                                                gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, GRPS(28, gg), gg, session("GR3N")(cc), -1, "", CInt(sFile), True, True, False, _
                                            '                                                1, tmp, 1, iRowPosition, iFY, session("LastYear"), 0, True, "", False
                                            '                                                '*******************************************
                                            '                                                If session("FirstYear") = FirstYearHistory And session("LastYear") > LastYearHistory Then
                                            '                                                    'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                            '                                                    gLoadForDisplayHis(sVar, GRPS(1, II), II, GRPS(28, gg), -1, session("GR3N")(cc), -1, _
                                            '                                                    True, True, False, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True, _
                                            '                                                    True, Session("bFillHis"), session("buseAllHis"), Session("FinYearHis")(session("COUNTE") + 1), Session("bFillHisOnlyHoles")
                                            '                                                    session("FinYear")(session("COUNTE") + 1) = session("LastYear") - FirstYearHistory + 1
                                            '                                                End If
                                            '                                                '*******************************************
                                            '                                                For y = 1 To session("FinYear")(1)
                                            '                                                    ValsWSA(y, jjWSA, k, II, gg) = tmp(y)
                                            '                                                    If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                            '                                                        ValsWSA(y, jjWSA, k, II, gg) = math.Round(ValsWSA(y, jjWSA, k, II, gg) * 1000000, 0)
                                            '                                                    ElseIf VUnits(idx) = "Thousand $" Then
                                            '                                                        ValsWSA(y, jjWSA, k, II, gg) = math.Round(ValsWSA(y, jjWSA, k, II, gg) * 1000, 0)
                                            '                                                    ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                            '                                                        ValsWSA(y, jjWSA, k, II, gg) = math.Round(ValsWSA(y, jjWSA, k, II, gg) * 1000000000, 0)
                                            '                                                    End If
                                            '                                                Next y
                                            '                                            Next gg
                                            '                                        End If 'Loading Energy type specific forecast
                                            '                                    End If 'haven't started with Energy type specific forecast
                                        End If 'if else end if vvTot > 0 (WatSan Accesss)
                                    Else 'Dobly dimensioned variables
                                        For vv = 1 To vvTot
                                            gLoadForDisplayNET(sVar, idx, GRPS(1, II), II, GRPS(VDim2(idx), vv), vv, "Total", -1, "", CInt(sFile), False, False, False,
                                    1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                            '*******************************************
                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                gLoadForDisplayHis(sVar, GRPS(1, II), II, GRPS(VDim2(idx), vv), vv, "Total", -1,
                                                False, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1, False,
                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                            End If
                                            '*******************************************
                                            For y = 1 To Session("FinYear")(1)
                                                Vals2(y, jj2, k, II, vv) = Session("PLOT")(y, Session("COUNTE") + 1)
                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                    Vals2(y, jj2, k, II, vv) = Math.Round(Vals2(y, jj2, k, II, vv) * 1000000, 0)
                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                    Vals2(y, jj2, k, II, vv) = Math.Round(Vals2(y, jj2, k, II, vv) * 1000, 0)
                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                    Vals2(y, jj2, k, II, vv) = Math.Round(Vals2(y, jj2, k, II, vv) * 1000000000, 0)
                                                End If
                                            Next y
                                        Next vv
                                    End If
                                End If 'Loading regular variables
                                'Move result to Vals
                                If Not bDim2 And vvTot > 0 Then
                                    For y = 1 To Session("FinYear")(1)
                                        Vals(y, j, k, II) = Session("PLOT")(y, Session("COUNTE") + 1)
                                        idx = AMAX(IfsVars.Item(sVar).Index, 0)
                                        If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                            Vals(y, j, k, II) = Math.Round(Vals(y, j, k, II) * 1000000, 0)
                                        ElseIf VUnits(idx) = "Thousand $" Then
                                            Vals(y, j, k, II) = Math.Round(Vals(y, j, k, II) * 1000, 0)
                                        ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                            Vals(y, j, k, II) = Math.Round(Vals(y, j, k, II) * 1000000000, 0)
                                        End If
                                    Next y
                                End If
                            End If
                        Next j 'next variable/metric
                    Next k 'next scenario
                Next II 'next member of the group
                'Next m 'next group

                'Save each scenario
                For j = 1 To Sces.Count
                    sFile = CStr(getFileIdx(Sces.Item(j)))
                    'for each country
                    For II = 1 To NR
                        'always doing all countries If RegGrps(II) > 0 Then
                        'Use ISO codes for countries
                        If ISO(II) > "" Then
                            sDim1 = ISO(II)
                        Else
                            sDim1 = GRPS(1, II)
                        End If
                        'each year
                        For k = 6 To .Rows.Count - 1
                            sYr = .Rows(k).Cells(0).Text

                            Print(1, sFile & ", " & sDim1 & ", " & sYr)
                            jj2 = 0
                            'for each variable
                            For m = 1 To Vars.Count
                                'Read Variable
                                pos = InStr(Session("VARN")(1, Vars.Item(m)), "[")
                                If pos > 0 Then
                                    sVar = Mid(Session("VARN")(1, Vars.Item(m)), 1, pos - 1)
                                Else
                                    sVar = Session("VARN")(1, Vars.Item(m))
                                End If
                                idx = IfsVars.Item(sVar).Index
                                bDim2 = False
                                vvTot = 1
                                If idx > 0 Then
                                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 27 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27
                                        If VDim2(idx) > 0 Then
                                            vvTot = MaxVal(VDim2(idx)) - 1
                                            bDim2 = True
                                            jj2 = jj2 + 1
                                        End If
                                    End If
                                    'jrs 2012/09/27
                                    '                            'jrs 2011/03/16
                                    '                            If VDim2(idx) = 28 Then
                                    '                                vvTot = 0
                                    '                            End If
                                End If

                                For vv = 1 To vvTot
                                    'Doubly dimensioned vars
                                    If Not bDim2 Then
                                        Print(1, ", " & Vals(CInt(sYr) - Session("FirstYear") + 1, m, j, II))
                                    Else
                                        Print(1, ", " & Vals2(CInt(sYr) - Session("FirstYear") + 1, jj2, j, II, vv))
                                    End If
                                Next vv
                            Next m
                            'end of line
                            PrintLine(1, "")
                        Next k 'next year
                        'End If 'if it's part of a group
                    Next II 'next country
                Next j 'next scenario

                FileClose(1)

                'jrs 2010/12/13
                'Load World values
                ReDim Wrld(Session("FinYear")(1), Vars.Count, Sces.Count), WrldG(Session("FinYear")(1), VarsG.Count, Sces.Count, 2)
                ReDim GrpsG(Session("FinYear")(1), VarsG.Count, Sces.Count, Gps.Count, 2), Wrld2(Session("FinYear")(1), Vars.Count, Sces.Count, 10)
                ReDim WrldET(Session("FinYear")(1), VarsET.Count, Sces.Count, 6), GrpsET(Session("FinYear")(1), VarsET.Count, Sces.Count, Gps.Count, 6)
                'jrs 2011/03/08
                ReDim WrldD(Session("FinYear")(1), VarsD.Count, Sces.Count, 3, 3), GrpsD(Session("FinYear")(1), VarsD.Count, Sces.Count, Gps.Count, 3, 3)
                'jrs 2012/09/28
                '        'jrs 2011/03/16
                '        Dim WrldWSA As object, GrpsWSA As object, WrldP As object, GrpsP As object
                '        ReDim WrldWSA(session("FinYear")(1), VarsWSA.Count, Sces.Count, 3), GrpsWSA(session("FinYear")(1), VarsWSA.Count, Sces.Count, Gps.Count, 3)
                '        ReDim WrldP(session("FinYear")(1), Sces.Count, 3), GrpsP(session("FinYear")(1), Sces.Count, Gps.Count, 3)
                ReDim WrldP(Session("FinYear")(1), Sces.Count, 3), GrpsP(Session("FinYear")(1), Sces.Count, Gps.Count, 3)

                'jrs 2011/01/05
                'Load UN Group Values
                ReDim UNGrps(Session("FinYear")(1), Vars.Count, Sces.Count, UNGroupCount)
                ReDim UNGrpsG(Session("FinYear")(1), Vars.Count, Sces.Count, UNGroupCount, 2)
                ReDim UNGrps2(Session("FinYear")(1), Vars.Count, Sces.Count, UNGroupCount, 10)
                ReDim UNGrpsET(Session("FinYear")(1), Vars.Count, Sces.Count, UNGroupCount, 6)
                'jrs 2011/03/08
                ReDim UNGrpsD(Session("FinYear")(1), Vars.Count, Sces.Count, UNGroupCount, 3, 3)
                'jrs 2011/03/16
                ReDim UNGrpsWSA(Session("FinYear")(1), Vars.Count, Sces.Count, UNGroupCount, 3), UNGrpsP(Session("FinYear")(1), Sces.Count, UNGroupCount, 3)

                'for each scenario
                For k = 1 To Sces.Count
                    sFile = CStr(getFileIdx(Sces.Item(k)))
                    'jrs 2023/09/25
                    MyFileforDisplay = FilesCol("FileForDisplay" + sFile.Trim)

                    'For each variable/metric
                    jj = 0
                    jjET = 0
                    jj2 = 0
                    jjD = 0
                    'jrs 2023/05/25 jjWSA = 0
                    For j = 1 To Vars.Count
                        cc = Vars.Item(j)
                        If jj < VarsG.Count Then
                            If VarsG.Item(jj + 1) = cc Then
                                jj = jj + 1
                            End If
                        End If
                        If jjET < VarsET.Count Then
                            If VarsET.Item(jjET + 1) = cc Then
                                jjET = jjET + 1
                            End If
                        End If
                        If jj2 < Vars2.Count Then
                            If Vars2.Item(jj2 + 1) = cc Then
                                jj2 = jj2 + 1
                            End If
                        End If
                        If jjD < VarsD.Count Then
                            If VarsD.Item(jjD + 1) = cc Then
                                jjD = jjD + 1
                            End If
                        End If
                        'jrs 2012/09/27
                        '                If jjWSA < VarsWSA.Count Then
                        '                    If VarsWSA.Item(jjWSA + 1) = cc Then
                        '                        jjWSA = jjWSA + 1
                        '                    End If
                        '                End If

                        'Read Variable
                        pos = InStr(Session("VARN")(1, cc), "[")
                        If pos > 0 Then
                            sVar = Mid(Session("VARN")(1, cc), 1, pos - 1)
                        Else
                            sVar = Session("VARN")(1, cc)
                        End If
                        iPos = InStr(sVar, "INCOMELT")
                        iPos2 = InStr(sVar, "LN")
                        iPos3 = InStr(sVar, "%")
                        bPoverty = False
                        bPctPop = False
                        bFind = False
                        'jrs 2016/11/07
                        b2005PPP = False
                        bIncRange = False
                        iPos4 = InStr(sVar, "GT")
                        iPos5 = InStr(sVar, "2005")

                        If iPos > 0 And iPos2 > 0 Then
                            bFind = (iPos3 > 0 And iPos3 < iPos2)
                            If bFind Then
                                level = Val(Mid(sVar, 9, iPos2 - 10))
                            Else
                                'jrs 2016/11/07
                                If iPos4 > 0 Then
                                    level2 = Val(Mid(sVar, 9, iPos4 - 9))
                                    level = Val(Mid(sVar, iPos4 + 2, iPos2 - 11 - Len(CStr(level2))))
                                    bIncRange = True
                                    If InStr(sVar, "%") > 0 Then
                                        bPctPop = True
                                    End If
                                Else
                                    level = Val(Mid(sVar, 9, iPos2 - 9))
                                    'jrs 2016/11/07
                                    If iPos5 > iPos2 Then
                                        'jrs 2016/11/07 If level <> 1 And level <> 2 Then
                                        If level <> 125 And level <> 200 Then
                                            bPoverty = True
                                        End If
                                    Else 'jrs 2016/11/07
                                        'jrs 2023/09/26 If level <> 110 And level <> 190 And level <> 310 Then
                                        If level <> 215 And level <> 365 And level <> 685 Then
                                            bPoverty = True
                                        End If
                                    End If
                                    If Right(sVar, 1) = "%" Then
                                        bPctPop = True
                                    End If
                                End If
                            End If
                        End If
                        bMortChl = (InStr(sVar, "POPCHLDMOR") > 0)
                        bMortAdl = (InStr(sVar, "POPADLTMOR") > 0)

                        If bPoverty Or bFind Then
                            idx = IfsVars.Item("INCOMELT125LN2005").Index
                        ElseIf bPctPop Then
                            idx = IfsVars.Item(Replace(sVar, "%", "")).Index
                        ElseIf bMortChl Or bMortAdl Then
                            idx = IfsVars.Item("DEATHCAT").Index
                        Else
                            idx = IfsVars.Item(sVar).Index
                        End If

                        'Define if Var is Formula
                        If UBound(Session("aFormulas")) >= cc Then
                            If Not (Session("aFormulas")(cc) Is Nothing) Then
                                If Session("VARN")(1, cc) = Session("aFormulas")(cc).Name Then
                                    bUsingComp = True
                                    f = Session("aFormulas")(cc)
                                Else
                                    bUsingComp = False
                                End If
                            Else
                                bUsingComp = False
                            End If
                        Else
                            bUsingComp = False
                        End If

                        bDim2 = False
                        vvTot = 1
                        If idx > 0 Then
                            If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                                If VDim2(idx) > 0 Then
                                    vvTot = MaxVal(VDim2(idx)) - 1
                                    bDim2 = True
                                End If
                            End If
                            'jrs 2012/09/27
                            '                    'jrs 2011/03/16
                            '                    If VDim2(idx) = 28 Then
                            '                        vvTot = 0
                            '                    End If
                        End If

                        If bUsingComp Then 'Using Computations
                            bHF = (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory And f.Formula2 = "")
                            Dim bPct As Boolean
                            bPct = (cswmWebMenu.Groups("mnuDispOpt").Items("mnuPercent").Caption = "Whole")
                            If bPct Then
                                mnuPercent_Click(Me, New EventArgs)
                            End If

                            'jrs 2011/01/05
                            For ss = 1 To UNGroupCount
                                gLoadComp(Session("COUNTE") + 1, f.Formula_Renamed, f.Name, UNGroupNames(ss), Session("GR2N")(cc), Session("GR3N")(cc), f.Unit, -1, True, CInt(sFile), bHF,
                                f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                                'Move results to target
                                For y = 1 To Session("FinYear")(1)
                                    UNGrps(y, j, k, ss) = Session("PLOT")(y, Session("COUNTE") + 1)
                                    If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                        UNGrps(y, j, k, ss) = Math.Round(UNGrps(y, j, k, ss) * 1000000, 0)
                                    ElseIf f.Unit = "Thousand $" Then
                                        UNGrps(y, j, k, ss) = Math.Round(UNGrps(y, j, k, ss) * 1000, 0)
                                    ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                        UNGrps(y, j, k, ss) = Math.Round(UNGrps(y, j, k, ss) * 1000000000, 0)
                                    End If
                                Next y
                            Next ss

                            gLoadComp(Session("COUNTE") + 1, f.Formula_Renamed, f.Name, "World", Session("GR2N")(cc), Session("GR3N")(cc), f.Unit, -1, True, CInt(sFile), bHF,
                            f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                            True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                            Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05

                            'Gender
                            If jj > 0 Then
                                If VarsG.Item(jj) = cc Then
                                    For gg = 1 To 2
                                        If Session("GR2N")(cc) = "Total" Then
                                            'jrs 2011/01/05
                                            For ss = 1 To UNGroupCount
                                                sGroup = UNGroupNames(ss)
                                                gLoadComp(Session("COUNTE") + 1 + gg, f.Formula_Renamed, f.Name, sGroup, GRPS(23, gg), Session("GR3N")(cc), f.Unit, -1, True, CInt(sFile), bHF,
                                                f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                                Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05

                                                For y = 1 To Session("FinYear")(1)
                                                    UNGrpsG(y, jj, k, ss, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                                    If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                                        UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                    ElseIf f.Unit = "Thousand $" Then
                                                        UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                    ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                                        UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                    End If
                                                Next y
                                            Next ss

                                            For ss = 1 To Gps.Count
                                                If Session("bAlias") Then
                                                    sGroup = Session("oGR1N")(Gps.Item(ss))
                                                Else
                                                    sGroup = Session("GR1N")(Gps.Item(ss))
                                                End If
                                                gLoadComp(Session("COUNTE") + 1 + gg, f.Formula_Renamed, f.Name, sGroup, GRPS(23, gg), Session("GR3N")(cc), f.Unit, -1, True, CInt(sFile), bHF,
                                                f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                                Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05

                                                For y = 1 To Session("FinYear")(1)
                                                    GrpsG(y, jj, k, ss, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                                    If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                                        GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                    ElseIf f.Unit = "Thousand $" Then
                                                        GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                    ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                                        GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                    End If
                                                Next y
                                            Next ss
                                            gLoadComp(Session("COUNTE") + 1 + gg, f.Formula_Renamed, f.Name, "World", GRPS(23, gg), Session("GR3N")(cc), f.Unit, -1, True, CInt(sFile), bHF,
                                            f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                            True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                            Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                                        Else
                                            'jrs 2011/01/05
                                            For ss = 1 To UNGroupCount
                                                sGroup = UNGroupNames(ss)
                                                gLoadComp(Session("COUNTE") + 1 + gg, f.Formula_Renamed, f.Name, sGroup, Session("GR2N")(cc), GRPS(23, gg), f.Unit, -1, True, CInt(sFile), bHF,
                                                f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                                Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05

                                                For y = 1 To Session("FinYear")(1)
                                                    UNGrpsG(y, jj, k, ss, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                                    If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                                        UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                    ElseIf f.Unit = "Thousand $" Then
                                                        UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                    ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                                        UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                    End If
                                                Next y
                                            Next ss

                                            For ss = 1 To Gps.Count
                                                If Session("bAlias") Then
                                                    sGroup = Session("oGR1N")(Gps.Item(ss))
                                                Else
                                                    sGroup = Session("GR1N")(Gps.Item(ss))
                                                End If
                                                gLoadComp(Session("COUNTE") + 1 + gg, f.Formula_Renamed, f.Name, sGroup, Session("GR2N")(cc), GRPS(23, gg), f.Unit, -1, True, CInt(sFile), bHF,
                                                f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                                Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05

                                                For y = 1 To Session("FinYear")(1)
                                                    GrpsG(y, jj, k, ss, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                                    If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                                        GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                    ElseIf f.Unit = "Thousand $" Then
                                                        GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                    ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                                        GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                    End If
                                                Next y
                                            Next ss
                                            gLoadComp(Session("COUNTE") + 1 + gg, f.Formula_Renamed, f.Name, "World", Session("GR2N")(cc), GRPS(23, gg), f.Unit, -1, True, CInt(sFile), bHF,
                                            f.Formula2 > "", f, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                            True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                            Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                            Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                                        End If
                                        For y = 1 To Session("FinYear")(1)
                                            WrldG(y, jj, k, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                            If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                                WrldG(y, jj, k, gg) = Math.Round(WrldG(y, jj, k, gg) * 1000000, 0)
                                            ElseIf f.Unit = "Thousand $" Then
                                                WrldG(y, jj, k, gg) = Math.Round(WrldG(y, jj, k, gg) * 1000, 0)
                                            ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                                WrldG(y, jj, k, gg) = Math.Round(WrldG(y, jj, k, gg) * 1000000000, 0)
                                            End If
                                        Next y
                                    Next gg
                                End If
                            End If

                            If bPct Then
                                mnuPercent_Click(Me, New EventArgs)
                            End If
                            If f.Formula2 > "" Then
                                Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                Exit Sub

                                Dim ly As Integer, fn As Integer, fY As Integer
                                fY = Session("FirstYear") 'jrs 2006/01/21
                                ly = Session("LastYear")
                                fn = Session("FileNum")
                                Dim f2 As New Formula
                                f2.init(f.Formula2)
                                f2.Name = f.Name
                                f2.Dimension = f.Dimension
                                f2.Dimension2 = f.Dimension2
                                f2.Unit = f.Unit
                                'jrs 2005/12/19 RAM for Formulas
                                For II = 1 To f.disVarHis.disSize
                                    f2.disVar.setDataDetS(f.disVarHis.display(II), f.disVarHis.displayDet(II))
                                    f2.disVar.setDataDet1S(f.disVarHis.display(II), f.disVarHis.displayDet1(II))
                                    f2.disVar.setDataDet2S(f.disVarHis.display(II), f.disVarHis.displayDet2(II))
                                    f2.disVar.setDataDet3S(f.disVarHis.display(II), f.disVarHis.displayDet3(II))
                                Next II

                                Dim h As Integer
                                h = Session("LastYear") - FirstYearForecast + 1
                                Dim PLOTCopy2() As Single
                                ReDim PLOTCopy2(h)
                                For m = 1 To h
                                    PLOTCopy2(m) = Session("PLOT")(m, Session("COUNTE"))
                                Next m

                                Session("FirstYear") = FirstYearHistory
                                Session("LastYear") = LastYearHistory
                                gLoadComp(Session("COUNTE") + 1, f2.Formula_Renamed, f2.Name, "World", Session("GR2N")(cc), Session("GR3N")(cc), f2.Unit, -1, True, CInt(sFile), bHF,
                                f2.Formula2 > "", f2, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                Session("StaYearHis")(Session("COUNTE") + 1), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"), dfCol, FilesCol) 'jrs 2021/05/05
                                'Copy Forecast back to session("PLOT")
                                Dim h2 As Integer
                                h2 = LastYearHistory - FirstYearHistory + 1
                                For m = 1 To h
                                    Session("PLOT")(m + h2 - 1, Session("COUNTE")) = PLOTCopy2(m)
                                Next m

                                Session("LastYear") = ly
                                Session("FirstYear") = fY 'jrs 2006/01/21
                                'session("FirstYear") = FirstYearForecast
                                Session("FileNum") = fn
                                Dim Rpoints = h + h2 - 1
                                DisYear = Rpoints
                                Session("FinYear")(Session("COUNTE")) = Rpoints
                                Session("DoingHistory") = False

                            End If

                            'Move result to Vals
                            For y = 1 To Session("FinYear")(1)
                                Wrld(y, j, k) = Session("PLOT")(y, Session("COUNTE") + 1)
                                If f.Unit = "Mil People" Or f.Unit = "Million Years" Then
                                    Wrld(y, j, k) = Math.Round(Wrld(y, j, k) * 1000000, 0)
                                ElseIf f.Unit = "Thousand $" Then
                                    Wrld(y, j, k) = Math.Round(Wrld(y, j, k) * 1000, 0)
                                ElseIf f.Unit = "Billion $" Or f.Unit = "Bil Barrels OE" Or f.Unit = "Billion Tons" Then
                                    Wrld(y, j, k) = Math.Round(Wrld(y, j, k) * 1000000000, 0)
                                End If
                            Next y

                        Else 'No Computations
                            If bPoverty Or bPctPop Then
                                Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                Exit Sub
                                Session("COUNTE") = Session("COUNTE") + 1
                                gLoadPovertyDisp(level, "World", CInt(sFile), True, bPctPop, b2005PPP,
                                Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                                Session("SensitivityFlag"), Session("mnuSQLite"))
                                Session("COUNTE") = Session("COUNTE") - 1
                            ElseIf bFind Then
                                Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                Exit Sub
                                Session("COUNTE") = Session("COUNTE") + 1
                                gFindPovertyLevel("World", CInt(sFile), True, level, b2005PPP,
                                Session("PLOT"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                                Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                                Session("COUNTE") = Session("COUNTE") - 1
                                'jrs 2010/05/14
                            ElseIf bMortChl Then
                                Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                Exit Sub

                                Session("COUNTE") = Session("COUNTE") + 1
                                '            gLoadMortProbChldL5 Session("COUNTE"), "", Nothing, True, True, "World", -1, True, Session("GR2N")(cc), Session("GR3N")(cc),
                                '    Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")), Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")), Session("VARN")(0, Session("COUNTE")),
                                '    GR4N(Session("COUNTE")), "", "", CInt(sFile), False, True
                                'Session("COUNTE") = Session("COUNTE") - 1
                            ElseIf bMortAdl Then
                                Response.Write("<script>alert('Sorry, not implemented for this format');</script>")
                                Exit Sub

                                '        Session("COUNTE") = Session("COUNTE") + 1
                                '        gLoadMortProbAdlt Session("COUNTE"), "", Nothing, True, True, "World", -1, True, Session("GR2N")(cc), Session("GR3N")(cc),
                                'Session("PLOT"), Session("GR1N")(Session("COUNTE")), Session("GR1N")(Session("COUNTE")), Session("GR2N")(Session("COUNTE")), Session("GR3N")(Session("COUNTE")), Session("VARN")(1, Session("COUNTE")), Session("VARN")(0, Session("COUNTE")),
                                'GR4N(Session("COUNTE")), "", "", CInt(sFile), False, True
                                Session("COUNTE") = Session("COUNTE") - 1
                            Else
                                'jrs 2023/09/25
                                MyDF = dfCol("df" + sVar + sFile.Trim)

                                If Not bDim2 Then
                                    'jrs 2011/03/16
                                    If vvTot > 0 Then
                                        'jrs 2011/01/05
                                        For ss = 1 To UNGroupCount
                                            gLoadForDisplayNET(sVar, idx, UNGroupNames(ss), -1, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                            1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                            '*******************************************
                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                gLoadForDisplayHis(sVar, UNGroupNames(ss), -1, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1,
                                                True, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1,
                                                False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                            End If
                                            '*******************************************
                                            For y = 1 To Session("FinYear")(1)
                                                UNGrps(y, j, k, ss) = Session("PLOT")(y, Session("COUNTE") + 1)
                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                    UNGrps(y, j, k, ss) = Math.Round(UNGrps(y, j, k, ss) * 1000000, 0)
                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                    UNGrps(y, j, k, ss) = Math.Round(UNGrps(y, j, k, ss) * 1000, 0)
                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                    UNGrps(y, j, k, ss) = Math.Round(UNGrps(y, j, k, ss) * 1000000000, 0)
                                                End If
                                            Next y
                                        Next ss

                                        gLoadForDisplayNET(sVar, idx, "World", -1, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                        1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                        '*******************************************
                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                            gLoadForDisplayHis(sVar, "World", -1, Session("GR2N")(cc), -1, Session("GR3N")(cc), -1,
                                            True, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1,
                                            False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                        End If
                                        '*******************************************
                                        'Age Category
                                        If sVar = "POP" Then
                                            'For each age category
                                            For gg = 1 To 3
                                                'jrs 2023/09/26
                                                MyDF = dfCol("df" + V(POPIdxs(gg)) + sFile.Trim)

                                                For ss = 1 To UNGroupCount
                                                    sGroup = UNGroupNames(ss)
                                                    gLoadForDisplayNET("", POPIdxs(gg), sGroup, -1, "", -1, "", -1, "", CInt(sFile), False, False, True,
                                                    1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                    '*******************************************
                                                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                        'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                        gLoadForDisplayHis(V(POPIdxs(gg)), sGroup, -1, "", -1, "", -1,
                                                        True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1,
                                                        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                        Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                    End If
                                                    '*******************************************
                                                    For y = 1 To Session("FinYear")(1)
                                                        UNGrpsP(y, k, ss, gg) = Math.Round(tmp(y) * 1000000, 0)
                                                    Next y
                                                    If gg = 1 Then
                                                        For y = 1 To Session("FinYear")(1)
                                                            UNGrpsP(y, k, ss, 0) = UNGrps(y, j, k, ss)
                                                        Next y
                                                    End If
                                                Next ss

                                                For ss = 1 To Gps.Count
                                                    If Session("bAlias") Then
                                                        sGroup = Session("oGR1N")(Gps.Item(ss))
                                                    Else
                                                        sGroup = Session("GR1N")(Gps.Item(ss))
                                                    End If
                                                    PopColIdx = cols.Item(sFile & Session("GR1N")(Gps.Item(ss)) & "POP")


                                                    gLoadForDisplayNET("", POPIdxs(gg), sGroup, -1, "", -1, "", -1, "", CInt(sFile), False, False, True,
                                                    1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                    '*******************************************
                                                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                        'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                        gLoadForDisplayHis(V(POPIdxs(gg)), sGroup, -1, "", -1, "", -1,
                                                        True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1,
                                                        True, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                        Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                    End If
                                                    '*******************************************
                                                    For y = 1 To Session("FinYear")(1)
                                                        GrpsP(y, k, ss, gg) = Math.Round(tmp(y) * 1000000, 0)
                                                    Next y
                                                    If gg = 1 Then
                                                        For y = 1 To Session("FinYear")(1)
                                                            GrpsP(y, k, ss, 0) = Math.Round(Session("PLOT")(y, PopColIdx) * 1000000, 0)
                                                        Next y
                                                    End If
                                                Next ss

                                                gLoadForDisplayNET("", POPIdxs(gg), "World", -1, "", -1, "", -1, "", CInt(sFile), False, False, True,
                                        1, Session("PLOT"), Session("COUNTE") + 1 + gg, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                '*******************************************
                                                If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                    'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                    gLoadForDisplayHis(V(POPIdxs(gg)), "World", -1, "", -1, "", -1,
                                                    True, 1, Session("PLOT"), Session("COUNTE") + 1 + gg, FirstYearHistory,
                                                    LastYearHistory - 1, False, True, Session("bFillHis"), Session("buseAllHis"),
                                                    Session("FinYearHis")(Session("COUNTE") + 1), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                    Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                    Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                End If
                                                '*******************************************
                                            Next gg
                                        End If
                                        'Gender
                                        If jj > 0 Then
                                            If VarsG.Item(jj) = cc Then
                                                For gg = 1 To 2
                                                    If Session("GR2N")(cc) = "Total" And Session("GR3N")(cc) = "" Then 'jrs 2011/03/08 added and session("GR3N")=""
                                                        'jrs 2011/01/05
                                                        For ss = 1 To UNGroupCount
                                                            sGroup = UNGroupNames(ss)
                                                            gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(23, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                                    1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                            '*******************************************
                                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                gLoadForDisplayHis(sVar, sGroup, -1, GRPS(23, gg), gg, Session("GR3N")(cc), -1,
                                                                True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                            End If
                                                            '*******************************************
                                                            For y = 1 To Session("FinYear")(1)
                                                                UNGrpsG(y, jj, k, ss, gg) = tmp(y)
                                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                    UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                                    UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                    UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                                End If
                                                            Next y
                                                        Next ss

                                                        For ss = 1 To Gps.Count
                                                            If Session("bAlias") Then
                                                                sGroup = Session("oGR1N")(Gps.Item(ss))
                                                            Else
                                                                sGroup = Session("GR1N")(Gps.Item(ss))
                                                            End If
                                                            gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(23, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                                    1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                            '*******************************************
                                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                gLoadForDisplayHis(sVar, sGroup, -1, GRPS(23, gg), gg, Session("GR3N")(cc), -1,
                                                                True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                            End If
                                                            '*******************************************
                                                            For y = 1 To Session("FinYear")(1)
                                                                GrpsG(y, jj, k, ss, gg) = tmp(y)
                                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                    GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                                    GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                    GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                                End If
                                                            Next y
                                                        Next ss

                                                        gLoadForDisplayNET(sVar, idx, "World", -1, GRPS(23, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                                        1, Session("PLOT"), Session("COUNTE") + 1 + gg, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                        '*******************************************
                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                            gLoadForDisplayHis(sVar, "World", -1, GRPS(23, gg), gg, Session("GR3N")(cc), -1,
                                                            True, 1, Session("PLOT"), Session("COUNTE") + 1 + gg, FirstYearHistory,
                                                            LastYearHistory - 1, False, True, Session("bFillHis"), Session("buseAllHis"),
                                                            Session("FinYearHis")(Session("COUNTE") + 1),
                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                        End If
                                                        '*******************************************
                                                    Else
                                                        'jrs 2011/01/05
                                                        For ss = 1 To UNGroupCount
                                                            sGroup = UNGroupNames(ss)
                                                            gLoadForDisplayNET(sVar, idx, sGroup, -1, Session("GR2N")(cc), -1, GRPS(23, gg), gg, "", CInt(sFile), False, False, True,
                                                            1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                            '*******************************************
                                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                gLoadForDisplayHis(sVar, sGroup, -1, Session("GR2N")(cc), -1, GRPS(23, gg), gg,
                                                                True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                            End If
                                                            '*******************************************
                                                            For y = 1 To Session("FinYear")(1)
                                                                UNGrpsG(y, jj, k, ss, gg) = tmp(y)
                                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                    UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                                    UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                    UNGrpsG(y, jj, k, ss, gg) = Math.Round(UNGrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                                End If
                                                            Next y
                                                            'jrs 2011/03/08
                                                            'Disease type
                                                            If jjD > 0 Then
                                                                If VarsD.Item(jjD) = cc Then
                                                                    For dd = 1 To MAXCOM(27) - 1
                                                                        gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(27, dd), dd, GRPS(23, gg), gg, "", CInt(sFile), False, False, True,
                                                                        1, tmpD, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                                        '*******************************************
                                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                            gLoadForDisplayHis(sVar, sGroup, -1, GRPS(27, dd), dd, GRPS(23, gg), gg,
                                                                            True, 1, tmpD, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                            True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                                        End If
                                                                        '*******************************************
                                                                        For y = 1 To Session("FinYear")(1)
                                                                            UNGrpsD(y, jjD, k, ss, gg, ddMap(dd)) = UNGrpsD(y, jjD, k, ss, gg, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                            UNGrpsD(y, jjD, k, ss, 3, ddMap(dd)) = UNGrpsD(y, jjD, k, ss, 3, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                        Next y
                                                                    Next dd
                                                                End If
                                                            End If
                                                        Next ss

                                                        For ss = 1 To Gps.Count
                                                            If Session("bAlias") Then
                                                                sGroup = Session("oGR1N")(Gps.Item(ss))
                                                            Else
                                                                sGroup = Session("GR1N")(Gps.Item(ss))
                                                            End If
                                                            gLoadForDisplayNET(sVar, idx, sGroup, -1, Session("GR2N")(cc), -1, GRPS(23, gg), gg, "", CInt(sFile), False, False, True,
                                                            1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                            Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                            '*******************************************
                                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                gLoadForDisplayHis(sVar, sGroup, -1, Session("GR2N")(cc), -1, GRPS(23, gg), gg,
                                                                True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                            End If
                                                            '*******************************************
                                                            For y = 1 To Session("FinYear")(1)
                                                                GrpsG(y, jj, k, ss, gg) = tmp(y)
                                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                    GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000, 0)
                                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                                    GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000, 0)
                                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                    GrpsG(y, jj, k, ss, gg) = Math.Round(GrpsG(y, jj, k, ss, gg) * 1000000000, 0)
                                                                End If
                                                            Next y

                                                            'jrs 2011/03/08
                                                            'Disease type
                                                            If jjD > 0 Then
                                                                If VarsD.Item(jjD) = cc Then
                                                                    For dd = 1 To MAXCOM(27) - 1
                                                                        gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(27, dd), dd, GRPS(23, gg), gg, "", CInt(sFile), False, False, True,
                                                                1, tmpD, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                                        '*******************************************
                                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                            gLoadForDisplayHis(sVar, sGroup, -1, GRPS(27, dd), dd, GRPS(23, gg), gg,
                                                                            True, 1, tmpD, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                            True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                                        End If
                                                                        '*******************************************
                                                                        For y = 1 To Session("FinYear")(1)
                                                                            GrpsD(y, jjD, k, ss, gg, ddMap(dd)) = GrpsD(y, jjD, k, ss, gg, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                            GrpsD(y, jjD, k, ss, 3, ddMap(dd)) = GrpsD(y, jjD, k, ss, 3, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                        Next y
                                                                    Next dd
                                                                End If
                                                            End If
                                                        Next ss

                                                        gLoadForDisplayNET(sVar, idx, "World", -1, Session("GR2N")(cc), -1, GRPS(23, gg), gg, "", CInt(sFile), False, False, True,
                                                        1, Session("PLOT"), Session("COUNTE") + 1 + gg, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                        '*******************************************
                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                            gLoadForDisplayHis(sVar, "World", -1, Session("GR2N")(cc), -1, GRPS(23, gg), gg,
                                                            True, 1, Session("PLOT"), Session("COUNTE") + 1 + gg, FirstYearHistory, LastYearHistory - 1,
                                                            False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                        End If
                                                        '*******************************************
                                                        'jrs 2011/03/08
                                                        'Disease type
                                                        If jjD > 0 Then
                                                            If VarsD.Item(jjD) = cc Then
                                                                For dd = 1 To MAXCOM(27) - 1
                                                                    gLoadForDisplayNET(sVar, idx, "World", -1, GRPS(27, dd), dd, GRPS(23, gg), gg, "", CInt(sFile), False, False, True,
                                                                    1, tmpD, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                                    '*******************************************
                                                                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                                        'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                                        gLoadForDisplayHis(sVar, "World", -1, GRPS(27, dd), dd, GRPS(23, gg), gg,
                                                                        True, 1, tmpD, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                                        True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                                        Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                                    End If
                                                                    For y = 1 To Session("FinYear")(1)
                                                                        WrldD(y, jjD, k, gg, ddMap(dd)) = WrldD(y, jjD, k, gg, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                        WrldD(y, jjD, k, 3, ddMap(dd)) = WrldD(y, jjD, k, 3, ddMap(dd)) + Math.Round(tmpD(y) * 1000000, 0)
                                                                    Next y
                                                                Next dd
                                                            End If
                                                        End If
                                                    End If
                                                Next gg
                                            End If 'loading gender variable
                                        End If 'first gender variable already loaded
                                        'Energy Type
                                        If jjET > 0 Then
                                            If VarsET.Item(jjET) = cc Then
                                                For gg = 1 To 6
                                                    'jrs 2011/01/05
                                                    For ss = 1 To UNGroupCount
                                                        sGroup = UNGroupNames(ss)
                                                        gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(5, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                                        1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                        '*******************************************
                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                            gLoadForDisplayHis(sVar, sGroup, -1, GRPS(5, gg), gg, Session("GR3N")(cc), -1,
                                                            True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1, True,
                                                            True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                        End If
                                                        '*******************************************
                                                        For y = 1 To Session("FinYear")(1)
                                                            UNGrpsET(y, jjET, k, ss, gg) = tmp(y)
                                                            If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                UNGrpsET(y, jjET, k, ss, gg) = Math.Round(UNGrpsET(y, jjET, k, ss, gg) * 1000000, 0)
                                                            ElseIf VUnits(idx) = "Thousand $" Then
                                                                UNGrpsET(y, jjET, k, ss, gg) = Math.Round(UNGrpsET(y, jjET, k, ss, gg) * 1000, 0)
                                                            ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                UNGrpsET(y, jjET, k, ss, gg) = Math.Round(UNGrpsET(y, jjET, k, ss, gg) * 1000000000, 0)
                                                            End If
                                                        Next y
                                                    Next ss

                                                    For ss = 1 To Gps.Count
                                                        If Session("bAlias") Then
                                                            sGroup = Session("oGR1N")(Gps.Item(ss))
                                                        Else
                                                            sGroup = Session("GR1N")(Gps.Item(ss))
                                                        End If
                                                        gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(5, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                                        1, tmp, 1, iRowPosition, iFY, Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                        Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                        '*******************************************
                                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                            gLoadForDisplayHis(sVar, sGroup, -1, GRPS(5, gg), gg, Session("GR3N")(cc), -1,
                                                            True, 1, tmp, 1, FirstYearHistory, LastYearHistory - 1,
                                                            True, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                            Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                        End If
                                                        '*******************************************
                                                        For y = 1 To Session("FinYear")(1)
                                                            GrpsET(y, jjET, k, ss, gg) = tmp(y)
                                                            If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                                GrpsET(y, jjET, k, ss, gg) = Math.Round(GrpsET(y, jjET, k, ss, gg) * 1000000, 0)
                                                            ElseIf VUnits(idx) = "Thousand $" Then
                                                                GrpsET(y, jjET, k, ss, gg) = Math.Round(GrpsET(y, jjET, k, ss, gg) * 1000, 0)
                                                            ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                                GrpsET(y, jjET, k, ss, gg) = Math.Round(GrpsET(y, jjET, k, ss, gg) * 1000000000, 0)
                                                            End If
                                                        Next y
                                                    Next ss

                                                    gLoadForDisplayNET(sVar, idx, "World", -1, GRPS(5, gg), gg, Session("GR3N")(cc), -1, "", CInt(sFile), False, False, True,
                                                    1, Session("PLOT"), Session("COUNTE") + 1 + gg, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                                    '*******************************************
                                                    If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                        'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                        gLoadForDisplayHis(sVar, "World", -1, GRPS(5, gg), gg, Session("GR3N")(cc), -1,
                                                        True, 1, Session("PLOT"), Session("COUNTE") + 1 + gg, FirstYearHistory,
                                                        LastYearHistory - 1, False, True, Session("bFillHis"), Session("buseAllHis"),
                                                        Session("FinYearHis")(Session("COUNTE") + 1),
                                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                        Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                                    End If
                                                    '*******************************************
                                                Next gg
                                            End If 'loading gender variable
                                        End If 'first gender variable already loaded
                                    Else 'if doing WatSan
                                        'jrs 2012/09/27
                                        '                                'WatSan Access
                                        '                                If jjWSA > 0 Then
                                        '                                    If VarsWSA.Item(jjWSA) = cc Then
                                        '                                        For gg = 1 To 3
                                        '                                            'jrs 2011/01/05
                                        '                                            For ss = 1 To UNGroupCount
                                        '                                                sGroup = UNGroupNames(ss)
                                        '                                                gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(28, gg), gg, session("GR3N")(cc), -1, "", CInt(sFile), True, True, True, _
                                        '                                                1, tmpWSA, 1, iRowPosition, iFY, session("LastYear"), 0, True, "", False
                                        '                                                '*******************************************
                                        '                                                If session("FirstYear") = FirstYearHistory And session("LastYear") > LastYearHistory Then
                                        '                                                    'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                        '                                                    gLoadForDisplayHis(sVar, sGroup, -1, GRPS(28, gg), gg, session("GR3N")(cc), -1, _
                                        '                                                    True, True, True, 1, tmpWSA, 1, FirstYearHistory, LastYearHistory - 1, _
                                        '                                                    True, True, Session("bFillHis"), session("buseAllHis"), Session("FinYearHis")(session("COUNTE") + 1), Session("bFillHisOnlyHoles")
                                        '                                                    session("FinYear")(session("COUNTE") + 1) = session("LastYear") - FirstYearHistory + 1
                                        '                                                End If
                                        '                                                '*******************************************
                                        '                                                For y = 1 To session("FinYear")(1)
                                        '                                                    UNGrpsWSA(y, jjWSA, k, ss, gg) = tmpWSA(y)
                                        '                                                    If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                        '                                                        UNGrpsWSA(y, jjWSA, k, ss, gg) = math.Round(UNGrpsWSA(y, jjWSA, k, ss, gg) * 1000000, 0)
                                        '                                                    ElseIf VUnits(idx) = "Thousand $" Then
                                        '                                                        UNGrpsWSA(y, jjWSA, k, ss, gg) = math.Round(UNGrpsWSA(y, jjWSA, k, ss, gg) * 1000, 0)
                                        '                                                    ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                        '                                                        UNGrpsWSA(y, jjWSA, k, ss, gg) = math.Round(UNGrpsWSA(y, jjWSA, k, ss, gg) * 1000000000, 0)
                                        '                                                    End If
                                        '                                                Next y
                                        '                                            Next ss
                                        '
                                        '                                            For ss = 1 To Gps.Count
                                        '                                                If Session("bAlias") Then
                                        '                                                    sGroup = session("oGR1N")(Gps.Item(ss))
                                        '                                                Else
                                        '                                                    sGroup = session("GR1N")(Gps.Item(ss))
                                        '                                                End If
                                        '                                                gLoadForDisplayNET(sVar, idx, sGroup, -1, GRPS(28, gg), gg, session("GR3N")(cc), -1, "", CInt(sFile), True, True, True, _
                                        '                                                1, tmpWSA, 1, iRowPosition, iFY, session("LastYear"), 0, True, "", False
                                        '                                                '*******************************************
                                        '                                                If session("FirstYear") = FirstYearHistory And session("LastYear") > LastYearHistory Then
                                        '                                                    'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                        '                                                    gLoadForDisplayHis(sVar, sGroup, -1, GRPS(28, gg), gg, session("GR3N")(cc), -1, _
                                        '                                                    True, True, True, 1, tmpWSA, 1, FirstYearHistory, LastYearHistory - 1, _
                                        '                                                    True, True, Session("bFillHis"), session("buseAllHis"), Session("FinYearHis")(session("COUNTE") + 1), Session("bFillHisOnlyHoles")
                                        '                                                    session("FinYear")(session("COUNTE") + 1) = session("LastYear") - FirstYearHistory + 1
                                        '                                                End If
                                        '                                                '*******************************************
                                        '                                                For y = 1 To session("FinYear")(1)
                                        '                                                    GrpsWSA(y, jjWSA, k, ss, gg) = tmpWSA(y)
                                        '                                                    If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                        '                                                        GrpsWSA(y, jjWSA, k, ss, gg) = math.Round(GrpsWSA(y, jjWSA, k, ss, gg) * 1000000, 0)
                                        '                                                    ElseIf VUnits(idx) = "Thousand $" Then
                                        '                                                        GrpsWSA(y, jjWSA, k, ss, gg) = math.Round(GrpsWSA(y, jjWSA, k, ss, gg) * 1000, 0)
                                        '                                                    ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                        '                                                        GrpsWSA(y, jjWSA, k, ss, gg) = math.Round(GrpsWSA(y, jjWSA, k, ss, gg) * 1000000000, 0)
                                        '                                                    End If
                                        '                                                Next y
                                        '                                            Next ss
                                        '
                                        '                                            gLoadForDisplayNET(sVar, idx, "World", -1, GRPS(28, gg), gg, session("GR3N")(cc), -1, "", CInt(sFile), True, True, True, _
                                        '                                            1, session("PLOT"), session("COUNTE") + 1 + gg, iRowPosition, iFY, session("LastYear"), 0, False, "", True
                                        '                                            '*******************************************
                                        '                                            If session("FirstYear") = FirstYearHistory And session("LastYear") > LastYearHistory Then
                                        '                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                        '                                                gLoadForDisplayHis(sVar, "World", -1, GRPS(28, gg), gg, session("GR3N")(cc), -1, _
                                        '                                                True, True, True, 1, session("PLOT"), session("COUNTE") + 1 + gg, FirstYearHistory, _
                                        '                                                LastYearHistory - 1, False, True, Session("bFillHis"), session("buseAllHis"), _
                                        '                                                Session("FinYearHis")(session("COUNTE") + 1), Session("bFillHisOnlyHoles")
                                        '                                                session("FinYear")(session("COUNTE") + 1) = session("LastYear") - FirstYearHistory + 1
                                        '                                            End If
                                        '                                            '*******************************************
                                        '                                        Next gg
                                        '                                    End If 'loading gender variable
                                        '                                End If 'first gender variable already loaded
                                    End If 'if else vvTot > 0 (WatSan)
                                Else 'for doubly dimension variables
                                    For vv = 1 To vvTot
                                        'jrs 2011/01/05
                                        For ss = 1 To UNGroupCount
                                            gLoadForDisplayNET(sVar, idx, UNGroupNames(ss), -1, GRPS(VDim2(idx), vv), vv, "Total", -1, "", CInt(sFile), False, False, True,
                                    1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                            '*******************************************
                                            If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                                'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                                gLoadForDisplayHis(sVar, UNGroupNames(ss), -1, GRPS(VDim2(idx), vv), vv, "Total", -1,
                                        True, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1, False, True,
                                        Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                                Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                            End If
                                            '*******************************************
                                            For y = 1 To Session("FinYear")(1)
                                                UNGrps2(y, jj2, k, ss, vv) = Session("PLOT")(y, Session("COUNTE") + 1)
                                                If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                    UNGrps2(y, jj2, k, ss, vv) = Math.Round(UNGrps2(y, jj2, k, ss, vv) * 1000000, 0)
                                                ElseIf VUnits(idx) = "Thousand $" Then
                                                    UNGrps2(y, jj2, k, ss, vv) = Math.Round(UNGrps2(y, jj2, k, ss, vv) * 1000, 0)
                                                ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                    UNGrps2(y, jj2, k, ss, vv) = Math.Round(UNGrps2(y, jj2, k, ss, vv) * 1000000000, 0)
                                                End If
                                            Next y
                                        Next ss

                                        gLoadForDisplayNET(sVar, idx, "World", -1, GRPS(VDim2(idx), vv), vv, "Total", -1, "", CInt(sFile), False, False, True,
                                1, Session("PLOT"), Session("COUNTE") + 1, iRowPosition, iFY, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                                                    Session("IFs_MDB2Active"), Session("CurrencyVal"), FileforDisplay:=MyFileforDisplay, dfVar:=MyDF)
                                        '*******************************************
                                        If Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory Then
                                            'jrs 2006/06/12 LastYearHistory - 1 to use forecast value in year 2000
                                            gLoadForDisplayHis(sVar, "World", -1, GRPS(VDim2(idx), vv), vv, "Total", -1,
                                    True, 1, Session("PLOT"), Session("COUNTE") + 1, FirstYearHistory, LastYearHistory - 1,
                                    False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1),
                                    Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                                                        Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                            Session("FinYear")(Session("COUNTE") + 1) = Session("LastYear") - FirstYearHistory + 1
                                        End If
                                        '*******************************************
                                        For y = 1 To Session("FinYear")(1)
                                            Wrld2(y, jj2, k, vv) = Session("PLOT")(y, Session("COUNTE") + 1)
                                            If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                Wrld2(y, jj2, k, vv) = Math.Round(Wrld2(y, jj2, k, vv) * 1000000, 0)
                                            ElseIf VUnits(idx) = "Thousand $" Then
                                                Wrld2(y, jj2, k, vv) = Math.Round(Wrld2(y, jj2, k, vv) * 1000, 0)
                                            ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                Wrld2(y, jj2, k, vv) = Math.Round(Wrld2(y, jj2, k, vv) * 1000000000, 0)
                                            End If
                                        Next y
                                    Next vv
                                End If
                            End If 'loading regular vars
                            'Move result to Vals
                            If Not bDim2 Then
                                If vvTot > 0 Then
                                    For y = 1 To Session("FinYear")(1)
                                        Wrld(y, j, k) = Session("PLOT")(y, Session("COUNTE") + 1)
                                        idx = AMAX(IfsVars.Item(sVar).Index, 0)
                                        If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                            Wrld(y, j, k) = Math.Round(Wrld(y, j, k) * 1000000, 0)
                                        ElseIf VUnits(idx) = "Thousand $" Then
                                            Wrld(y, j, k) = Math.Round(Wrld(y, j, k) * 1000, 0)
                                        ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                            Wrld(y, j, k) = Math.Round(Wrld(y, j, k) * 1000000000, 0)
                                        End If
                                    Next y
                                    'Age Cat
                                    If sVar = "POP" Then
                                        'Move result to Vals
                                        For gg = 1 To 3
                                            For y = 1 To Session("FinYear")(1)
                                                WrldP(y, k, gg) = Math.Round(Session("PLOT")(y, Session("COUNTE") + 1 + gg) * 1000000, 0)
                                            Next y
                                        Next gg
                                        For y = 1 To Session("FinYear")(1)
                                            WrldP(y, k, 0) = Wrld(y, j, k)
                                        Next y
                                    End If
                                    'Gender
                                    If jj > 0 Then
                                        If VarsG.Item(jj) = cc Then
                                            'Move result to Vals
                                            For gg = 1 To 2
                                                For y = 1 To Session("FinYear")(1)
                                                    WrldG(y, jj, k, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                                    idx = AMAX(IfsVars.Item(sVar).Index, 0)
                                                    If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                        WrldG(y, jj, k, gg) = Math.Round(WrldG(y, jj, k, gg) * 1000000, 0)
                                                    ElseIf VUnits(idx) = "Thousand $" Then
                                                        WrldG(y, jj, k, gg) = Math.Round(WrldG(y, jj, k, gg) * 1000, 0)
                                                    ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                        WrldG(y, jj, k, gg) = Math.Round(WrldG(y, jj, k, gg) * 1000000000, 0)
                                                    End If
                                                Next y
                                            Next gg
                                        End If 'Gender found
                                    End If 'Gender
                                    'Energy Type
                                    If jjET > 0 Then
                                        If VarsET.Item(jjET) = cc Then
                                            'Move result to Vals
                                            For gg = 1 To 6
                                                For y = 1 To Session("FinYear")(1)
                                                    WrldET(y, jjET, k, gg) = Session("PLOT")(y, Session("COUNTE") + 1 + gg)
                                                    idx = AMAX(IfsVars.Item(sVar).Index, 0)
                                                    If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                                        WrldET(y, jjET, k, gg) = Math.Round(WrldET(y, jjET, k, gg) * 1000000, 0)
                                                    ElseIf VUnits(idx) = "Thousand $" Then
                                                        WrldET(y, jjET, k, gg) = Math.Round(WrldET(y, jjET, k, gg) * 1000, 0)
                                                    ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                                        WrldET(y, jjET, k, gg) = Math.Round(WrldET(y, jjET, k, gg) * 1000000000, 0)
                                                    End If
                                                Next y
                                            Next gg
                                        End If 'Gender found
                                    End If 'Gender
                                Else 'Doing WatSan
                                    'jrs 2012/09/27
                                    '                            'WatSan Access
                                    '                            If jjWSA > 0 Then
                                    '                                If VarsWSA.Item(jjWSA) = cc Then
                                    '                                    'Move result to Vals
                                    '                                    For gg = 1 To 3
                                    '                                        For y = 1 To session("FinYear")(1)
                                    '                                            WrldWSA(y, jjWSA, k, gg) = session("PLOT")(y, session("COUNTE") + 1 + gg)
                                    '                                            idx = AMAX(IfsVars.Item(sVar).Index, 0)
                                    '                                            If VUnits(idx) = "Mil People" Or VUnits(idx) = "Million Years" Then
                                    '                                                WrldWSA(y, jjWSA, k, gg) = math.Round(WrldWSA(y, jjWSA, k, gg) * 1000000, 0)
                                    '                                            ElseIf VUnits(idx) = "Thousand $" Then
                                    '                                                WrldWSA(y, jjWSA, k, gg) = math.Round(WrldWSA(y, jjWSA, k, gg) * 1000, 0)
                                    '                                            ElseIf VUnits(idx) = "Billion $" Or VUnits(idx) = "Bil Barrels OE" Or VUnits(idx) = "Billion Tons" Then
                                    '                                                WrldWSA(y, jjWSA, k, gg) = math.Round(WrldWSA(y, jjWSA, k, gg) * 1000000000, 0)
                                    '                                            End If
                                    '                                        Next y
                                    '                                    Next gg
                                    '                                End If 'Gender found
                                    '                            End If 'Gender
                                End If 'if vvTot = 0 WatSan
                            End If 'not dim2
                        End If 'NO computations
                    Next j 'next variable/metric
                Next k 'next scenario

                'Save world slice
                FileOpen(1, Session("PutFdrvRun") & "PDE\world_scenario_slice.csv", OpenMode.Output)

                Print(1, "scenario,year")
                'For each variable/metric
                For j = 1 To Vars.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, Vars.Item(j))
                    End If
                    idx = IfsVars.Item(sVar).Index
                    bDim2 = False
                    vvTot = 1
                    If idx > 0 Then
                        If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                            If VDim2(idx) > 0 Then
                                vvTot = MaxVal(VDim2(idx)) - 1
                                bDim2 = True
                            End If
                        End If
                        'jrs 2012/09/27
                        '                'jrs 2011/03/16
                        '                If VDim2(idx) = 28 Then
                        '                    vvTot = 0
                        '                End If
                    End If
                    For vv = 1 To vvTot
                        If bDim2 Then
                            sVar = V(idx) & CStr(vv)
                        End If
                        Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
                    Next vv
                Next j
                PrintLine(1, "")

                'Save each scenario
                For j = 1 To Sces.Count
                    sFile = CStr(getFileIdx(Sces.Item(j)))
                    'each year
                    For k = 6 To .Rows.Count - 1
                        sYr = .Rows(k).Cells(0).Text

                        Print(1, sFile & ", " & sYr)
                        jj2 = 0
                        'for each variable
                        For m = 1 To Vars.Count
                            'Read Variable
                            pos = InStr(Session("VARN")(1, Vars.Item(m)), "[")
                            If pos > 0 Then
                                sVar = Mid(Session("VARN")(1, Vars.Item(m)), 1, pos - 1)
                            Else
                                sVar = Session("VARN")(1, Vars.Item(m))
                            End If
                            idx = IfsVars.Item(sVar).Index
                            bDim2 = False
                            vvTot = 1
                            If idx > 0 Then
                                If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 27 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2011/03/08 'jrs 2012/09/27 And VDim2(idx) <> 28
                                    If VDim2(idx) > 0 Then
                                        vvTot = MaxVal(VDim2(idx)) - 1
                                        bDim2 = True
                                        jj2 = jj2 + 1
                                    End If
                                End If
                                'jrs 2012/09/27
                                '                        'jrs 2011/03/16
                                '                        If VDim2(idx) = 28 Then
                                '                            vvTot = 0
                                '                        End If
                            End If
                            For vv = 1 To vvTot
                                If Not bDim2 Then
                                    Print(1, ", " & Wrld(CInt(sYr) - Session("FirstYear") + 1, m, j))
                                Else
                                    Print(1, ", " & Wrld2(CInt(sYr) - Session("FirstYear") + 1, jj2, j, vv))
                                End If
                            Next vv
                        Next m
                        'end of line
                        PrintLine(1, "")
                    Next k 'next year
                Next j 'next scenario

                FileClose(1)

                .Visible = True
            End With

            'jrs 2011/01/05 Save UN Group Slices
            SaveUNGroupSlices(grd_TableDisplay, Vars, Sces, UNGroupCount, UNGroupAlias, UNGroupNames, UNGrps, UNGrps2)

            If VarsG.Count > 0 Then
                SaveGenderSlices(grd_TableDisplay, VarsG, Sces, Gps, WrldG, GrpsG, ValsG, ISO, UNGroupCount, UNGroupAlias, UNGroupNames, UNGrpsG)
                'jrs 2011/03/08
                If VarsD.Count > 0 Then
                    SaveDtypeSlices(grd_TableDisplay, VarsD, Sces, Gps, WrldD, GrpsD, ValsD, ISO, UNGroupCount, UNGroupAlias, UNGroupNames, UNGrpsD)
                Else
                    Kill(Session("PutFdrvRun") & "PDE\dtype_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_country_scenario_slice.csv")

                    Kill(Session("PutFdrvRun") & "PDE\dtype_gender_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_gender_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_gender_country_scenario_slice.csv")
                End If
            Else
                If Dir(Session("PutFdrvRun") & "PDE\gender_world_scenario_slice.csv") > "" Then
                    Kill(Session("PutFdrvRun") & "PDE\gender_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\gender_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\gender_country_scenario_slice.csv")

                    Kill(Session("PutFdrvRun") & "PDE\dtype_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_country_scenario_slice.csv")

                    Kill(Session("PutFdrvRun") & "PDE\dtype_gender_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_gender_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\dtype_gender_country_scenario_slice.csv")
                End If
            End If

            If VarsET.Count > 0 Then
                SaveEtypeSlices(grd_TableDisplay, VarsET, Sces, Gps, WrldET, GrpsET, ValsET, ISO, UNGroupCount, UNGroupAlias, UNGroupNames, UNGrpsET)
            Else
                If Dir(Session("PutFdrvRun") & "PDE\etype_world_scenario_slice.csv") > "" Then
                    Kill(Session("PutFdrvRun") & "PDE\etype_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\etype_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\etype_country_scenario_slice.csv")
                End If
            End If

            'jrs 2012/09/27
            '    'jrs 2011/03/16
            '    If VarsWSA.Count > 0 Then
            '        SaveWatSanAccessSlices grd_TableDisplay, VarsWSA, Sces, Gps, WrldWSA, GrpsWSA, ValsWSA, ISO, UNGroupCount, UNGroupAlias, UNGroupNames, UNGrpsWSA
            '    Else
            '        If Dir(session("PutFdrvRun") & "PDE\watsanaccess_world_scenario_slice.csv") > "" Then
            '            Kill(session("PutFdrvRun") & "PDE\watsanaccess_world_scenario_slice.csv"
            '            Kill(session("PutFdrvRun") & "PDE\watsanaccess_group_scenario_slice.csv"
            '            Kill(session("PutFdrvRun") & "PDE\watsanaccess_country_scenario_slice.csv"
            '        End If
            '    End If

            If bPOP Then
                SaveAgeCatSlices(grd_TableDisplay, Sces, Gps, WrldP, GrpsP, ValsP, ISO, UNGroupCount, UNGroupAlias, UNGroupNames, UNGrpsP)
            Else
                If Dir(Session("PutFdrvRun") & "PDE\agecat_world_scenario_slice.csv") > "" Then
                    Kill(Session("PutFdrvRun") & "PDE\agecat_world_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\agecat_group_scenario_slice.csv")
                    Kill(Session("PutFdrvRun") & "PDE\agecat_country_scenario_slice.csv")
                End If
            End If

            'Create country file and
            FileOpen(1, Session("PutFdrvRun") & "PDE\countries.csv", OpenMode.Output)
            PrintLine(1, "country, latitude, longitude, name, group")
            Dim sName As String
            For II = 1 To NR
                'doing all groups without decomposing If RegGrps(II) > 0 Then
                sName = Replace(GRPS(1, II), "'", "\'")
                If InStr(GRPS(1, II), ",") > 0 Then
                    sName = "'" & sName & "'"
                End If
                If ISOcode(II) > "" Then
                    PrintLine(1, ISOcode(II) & ", " & CStr(ISOlat(II)) & ", " & CStr(ISOlong(II)) & ", " & ISOname(II) & ", " & 0) 'RegGrps(II)
                Else
                    PrintLine(1, sName & ", 0, 0, " & sName & ", " & 0) 'RegGrps(II)
                End If
                'End If
            Next II
            FileClose(1)

            Response.Write("<script>alert('Files successfully saved under " & Session("PutFdrvRun") & "\PDE directory');</script>")
        Catch ex As Exception
            grd_TableDisplay.Visible = True
            Response.Write("<script>alert('" & ex.Message & "');</script>")
            FileClose(1)
            'Resume

        End Try



    End Sub

    'jrs 2010/12/07
    Private Function getFileIdx(C1 As Integer) As Integer
        Dim st As Integer, en As Integer
        getFileIdx = 0
        st = InStr(Session("VARN")(1, C1%), "[") + 1
        en = InStr(Session("VARN")(1, C1%), "]") + 1

        If st > 0 And en > st Then
            If IsNumeric(Mid(Session("VARN")(1, C1%), st, en - st - 1)) Then
                Dim fn = Int(Mid(Session("VARN")(1, C1%), st, en - st - 1))
                getFileIdx = fn
            End If
        Else
            'look in formula
            If UBound(Session("aFormulas")) >= C1 Then
                If Not Session("aFormulas")(C1) Is Nothing Then
                    If Session("aFormulas")(C1).Name = Session("VARN")(1, C1) Then
                        st = InStr(Session("aFormulas")(C1).disVar.displayDet(1), "[")
                        If st > 0 Then
                            Dim fn = Int(Mid(Session("aFormulas")(C1).disVar.displayDet(1), st + 1, InStr(Session("aFormulas")(C1).disVar.displayDet(1), "]") - st - 1))
                            getFileIdx = fn
                        End If
                    End If
                End If
            End If
        End If
    End Function

    'jrs 2010/12/20
    Public Function SearchVarIdxWGenderInForm(f As Formula) As Integer
        If f Is Nothing Then Exit Function
        Dim i As Integer, idx As Integer, pos As Integer, sVar As String
        For i = 1 To f.disVar.disSize
            pos = InStr(f.disVar.displayDet(i), "[") - 1
            If pos > 0 Then
                sVar = Mid(f.disVar.displayDet(i), 1, pos)
            Else
                sVar = f.disVar.displayDet(i)
            End If
            idx = IfsVars.Item(sVar).Index
            If VDim2(idx) = 23 Or VDim3(idx) = 23 Then
                SearchVarIdxWGenderInForm = idx
                Exit Function
            End If
        Next i
        Return 0
        SearchVarIdxWGenderInForm = 0
    End Function

    'jrs 2011/01/05
    Private Sub SaveUNGroupSlices(grd_TableDisplay As Table, Vars As Object, Sces As Object,
    UNGroupCount As Integer, UNGroupAlias As Object, UNGroupNames As Object, UNGrps As Object,
    UNGrps2 As Object)
        Dim j As Integer, pos As Integer, sVar As String, idx As Integer, bDim2 As Boolean
        Dim vvTot As Integer, vv As Integer, sFile As String, k As Integer, sYr As String
        Dim jj2 As Integer, m As Integer, II As Integer
        With grd_TableDisplay
            .Visible = False
            'Save UN Group slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\ungroup_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,ungroup,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '                'jrs 2011/03/16
                    '                If VDim2(idx) = 28 Then
                    '                    vvTot = 0
                    '                End If
                End If
                For vv = 1 To vvTot
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If
                    Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
                Next vv
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for each group
                For II = 1 To UNGroupCount
                    Dim sDim1 = UNGroupAlias(II)
                    Dim gg = gGetGroupIdx(UNGroupNames(II), False, Me)
                    'each year
                    For k = 6 To .Rows.Count - 1
                        sYr = .Rows(k).Cells(0).Text

                        Print(1, sFile & ", " & CStr(gg) & ", " & sYr)
                        jj2 = 0
                        'for each variable
                        For m = 1 To Vars.Count
                            'Read Variable
                            pos = InStr(Session("VARN")(1, Vars.Item(m)), "[")
                            If pos > 0 Then
                                sVar = Mid(Session("VARN")(1, Vars.Item(m)), 1, pos - 1)
                            Else
                                sVar = Session("VARN")(1, Vars.Item(m))
                            End If
                            idx = IfsVars.Item(sVar).Index
                            bDim2 = False
                            vvTot = 1
                            If idx > 0 Then
                                If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                                    If VDim2(idx) > 0 Then
                                        vvTot = MaxVal(VDim2(idx)) - 1
                                        bDim2 = True
                                        jj2 = jj2 + 1
                                    End If
                                End If
                                'jrs 2012/09/27
                                '                            'jrs 2011/03/16
                                '                            If VDim2(idx) = 28 Then
                                '                                vvTot = 0
                                '                            End If
                            End If
                            For vv = 1 To vvTot
                                If Not bDim2 Then
                                    Print(1, ", " & UNGrps(CInt(sYr) - Session("FirstYear") + 1, m, j, II))
                                Else
                                    Print(1, ", " & UNGrps2(CInt(sYr) - Session("FirstYear") + 1, jj2, j, II, vv))
                                End If
                            Next vv
                        Next m
                        'end of line
                        PrintLine(1, "")
                    Next k 'next year
                Next II 'next UN Group
            Next j 'next scenario

            FileClose(1)
            .Visible = True
        End With

    End Sub

    'jrs 2010/12/17
    Private Sub SaveGenderSlices(grd As Table, Vars As Object, Sces As Object, Gps As Object,
            WrldG As Object, GrpsG As Object, Vals As Object, ISO As Object, UNGroupCount As Integer,
            UNGroupAlias As Object, UNGroupNames As Object, UNGrpsG As Object)
        Dim j As Integer, pos As Integer, sVar As String ', idx As Integer
        Dim sFile As String, gg As Integer, ss As Integer

        With grd
            .Visible = False
            'Save world slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\gender_world_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for males and femlaes
                For gg = 1 To 2
                    'each year
                    For k = 6 To .Rows.Count - 1
                        Dim sYr = .Rows(k).Cells(0).Text

                        Print(1, sFile & ", " & GRPS(23, gg) & ", " & sYr)
                        'for each variable
                        For m = 1 To Vars.Count
                            Print(1, ", " & WrldG(CInt(sYr) - Session("FirstYear") + 1, m, j, gg))
                        Next m
                        'end of line
                        PrintLine(1, "")
                    Next k 'next year
                Next gg 'next gender
            Next j 'next scenario

            FileClose(1)

            'Save Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\gender_group_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,group,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To Gps.Count
                    Dim sDim1 = Session("GR1N")(Gps.Item(II))
                    'jrs 2010/12/13
                    If Session("bAlias") Then
                        gg = gGetGroupIdx(Session("oGR1N")(Gps.Item(II)), False, Me)
                    Else
                        gg = gGetGroupIdx(sDim1, False, Me)
                    End If
                    'for males and females
                    For ss = 1 To 2
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & GRPS(23, ss) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                Print(1, ", " & GrpsG(CInt(sYr) - Session("FirstYear") + 1, j, k, II, ss))
                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save UN Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\gender_ungroup_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,ungroup,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each UN group
                For II = 1 To UNGroupCount
                    Dim sDim1 = UNGroupAlias(II)
                    gg = gGetGroupIdx(UNGroupNames(II), False, Me)
                    'for males and females
                    For ss = 1 To 2
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & GRPS(23, ss) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                Print(1, ", " & UNGrpsG(CInt(sYr) - Session("FirstYear") + 1, j, k, II, ss))
                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save country slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\gender_country_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,country,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                Dim sDim1 As String
                'for each country
                For II = 1 To NR
                    'always doing all countries If RegGrps(II) > 0 Then
                    'Use ISO codes for countries
                    If ISO(II) > "" Then
                        sDim1 = ISO(II)
                    Else
                        sDim1 = GRPS(1, II)
                    End If
                    'For males and females
                    For gg = 1 To 2
                        'each year
                        For k = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(k).Cells(0).Text

                            Print(1, sFile & ", " & sDim1 & ", " & GRPS(23, gg) & ", " & sYr)
                            'for each variable
                            For m = 1 To Vars.Count
                                Print(1, ", " & Vals(CInt(sYr) - Session("FirstYear") + 1, m, j, II, gg))
                            Next m
                            'end of line
                            PrintLine(1, "")
                        Next k 'next year
                    Next gg
                Next II 'next country
            Next j 'next scenario

            FileClose(1)

            .Visible = True
        End With
    End Sub

    'jrs 2010/12/21
    Private Sub SaveEtypeSlices(grd As Table, Vars As Object, Sces As Object, Gps As Object,
    WrldG As Object, GrpsG As Object, Vals As Object, ISO As Object, UNGroupCount As Integer,
    UNGroupAlias As Object, UNGroupNames As Object, UNGrpsG As Object)
        Dim j As Integer, pos As Integer, sVar As String ', idx As Integer
        Dim sFile As String, gg As Integer, ss As Integer
        Dim sDim1 As String

        With grd
            .Visible = False
            'Save world slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\etype_world_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,energytype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for all energy types
                For gg = 1 To 6
                    'each year
                    For k = 6 To .Rows.Count - 1
                        Dim sYr = .Rows(k).Cells(0).Text

                        Print(1, sFile & ", " & CStr(gg) & ", " & sYr)
                        'for each variable
                        For m = 1 To Vars.Count
                            Print(1, ", " & WrldG(CInt(sYr) - Session("FirstYear") + 1, m, j, gg))
                        Next m
                        'end of line
                        PrintLine(1, "")
                    Next k 'next year
                Next gg 'next gender
            Next j 'next scenario

            FileClose(1)

            'Save Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\etype_group_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,group,energytype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To Gps.Count
                    sDim1 = Session("GR1N")(Gps.Item(II))
                    'jrs 2010/12/13
                    If Session("bAlias") Then
                        gg = gGetGroupIdx(Session("oGR1N")(Gps.Item(II)), False, Me)
                    Else
                        gg = gGetGroupIdx(sDim1, False, Me)
                    End If
                    'for energy types
                    For ss = 1 To 6
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                Print(1, ", " & GrpsG(CInt(sYr) - Session("FirstYear") + 1, j, k, II, ss))
                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save UN Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\etype_ungroup_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,ungroup,energytype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To UNGroupCount
                    sDim1 = UNGroupAlias(II)
                    gg = gGetGroupIdx(UNGroupNames(II), False, Me)
                    'for energy types
                    For ss = 1 To 6
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                Print(1, ", " & UNGrpsG(CInt(sYr) - Session("FirstYear") + 1, j, k, II, ss))
                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save country slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\etype_country_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,country,energytype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for each country
                For II = 1 To NR
                    'always doing all countries If RegGrps(II) > 0 Then
                    'Use ISO codes for countries
                    If ISO(II) > "" Then
                        sDim1 = ISO(II)
                    Else
                        sDim1 = GRPS(1, II)
                    End If
                    'For all energy types
                    For gg = 1 To 6
                        'each year
                        For k = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(k).Cells(0).Text

                            Print(1, sFile & ", " & sDim1 & ", " & CStr(gg) & ", " & sYr)
                            'for each variable
                            For m = 1 To Vars.Count
                                Print(1, ", " & Vals(CInt(sYr) - Session("FirstYear") + 1, m, j, II, gg))
                            Next m
                            'end of line
                            PrintLine(1, "")
                        Next k 'next year
                    Next gg
                Next II 'next country
            Next j 'next scenario

            FileClose(1)

            .Visible = True
        End With
    End Sub

    'jrs 2011/03/08
    Private Sub SaveDtypeSlices(grd As Table, Vars As Object, Sces As Object, Gps As Object,
    WrldD As Object, GrpsD As Object, Vals As Object, ISO As Object, UNGroupCount As Integer,
    UNGroupAlias As Object, UNGroupNames As Object, UNGrpsD As Object)
        Dim j As Integer, pos As Integer, sVar As String ', idx As Integer
        Dim sFile As String, gg As Integer, ss As Integer, dd As Integer
        Dim sDim1 As String

        With grd
            .Visible = False
            'Save world slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_world_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,diseasetype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for all disease types
                For gg = 1 To 3
                    'each year
                    For k = 6 To .Rows.Count - 1
                        Dim sYr = .Rows(k).Cells(0).Text

                        Print(1, sFile & ", " & CStr(gg) & ", " & sYr)
                        'for each variable
                        For m = 1 To Vars.Count
                            Print(1, ", " & WrldD(CInt(sYr) - Session("FirstYear") + 1, m, j, 3, gg))
                        Next m
                        'end of line
                        PrintLine(1, "")
                    Next k 'next year
                Next gg 'next disease type
            Next j 'next scenario

            FileClose(1)

            'Save world, gender slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_gender_world_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,diseasetype,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for both genders
                For gg = 1 To 2
                    'for all disease types
                    For dd = 1 To 3
                        'each year
                        For k = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(k).Cells(0).Text

                            Print(1, sFile & ", " & CStr(dd) & ", " & GRPS(23, gg) & ", " & sYr)
                            'for each variable
                            For m = 1 To Vars.Count
                                Print(1, ", " & WrldD(CInt(sYr) - Session("FirstYear") + 1, m, j, gg, dd))
                            Next m
                            'end of line
                            PrintLine(1, "")
                        Next k 'next year
                    Next dd 'next disease type
                Next gg 'next gender
            Next j 'next scenario

            FileClose(1)

            'Save Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_group_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,group,diseasetype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To Gps.Count
                    sDim1 = Session("GR1N")(Gps.Item(II))
                    'jrs 2010/12/13
                    If Session("bAlias") Then
                        gg = gGetGroupIdx(Session("oGR1N")(Gps.Item(II)), False, Me)
                    Else
                        gg = gGetGroupIdx(sDim1, False, Me)
                    End If
                    'for disease types
                    For ss = 1 To 3
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                Print(1, ", " & GrpsD(CInt(sYr) - Session("FirstYear") + 1, j, k, II, 3, ss))
                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save Group, gender Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_gender_group_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,group,diseasetype,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To Gps.Count
                    sDim1 = Session("GR1N")(Gps.Item(II))
                    'jrs 2010/12/13
                    If Session("bAlias") Then
                        gg = gGetGroupIdx(Session("oGR1N")(Gps.Item(II)), False, Me)
                    Else
                        gg = gGetGroupIdx(sDim1, False, Me)
                    End If
                    'for disease types
                    For ss = 1 To 3
                        'for gender
                        For dd = 1 To 2
                            'For each year
                            For y = 6 To .Rows.Count - 1
                                Dim sYr = .Rows(y).Cells(0).Text
                                'Save
                                Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & GRPS(23, dd) & ", " & sYr)

                                'For each variable/metric
                                For j = 1 To Vars.Count
                                    Print(1, ", " & GrpsD(CInt(sYr) - Session("FirstYear") + 1, j, k, II, dd, ss))
                                Next j
                                'end of line
                                PrintLine(1, "")
                            Next y
                        Next dd
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save UN Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_ungroup_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,ungroup,diseasetype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To UNGroupCount
                    sDim1 = UNGroupAlias(II)
                    gg = gGetGroupIdx(UNGroupNames(II), False, Me)
                    'for disease types
                    For ss = 1 To 3
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & sYr)

                            'For each variable/metric
                            For j = 1 To Vars.Count
                                Print(1, ", " & UNGrpsD(CInt(sYr) - Session("FirstYear") + 1, j, k, II, 3, ss))
                            Next j
                            'end of line
                            PrintLine(1, "")
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save UN Group, gender Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_gender_ungroup_scenario_slice.csv", OpenMode.Output)
            Print(1, "scenario,ungroup,diseasetype,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To UNGroupCount
                    sDim1 = UNGroupAlias(II)
                    gg = gGetGroupIdx(UNGroupNames(II), False, Me)
                    'for disease types
                    For ss = 1 To 3
                        'for gender
                        For dd = 1 To 2
                            'For each year
                            For y = 6 To .Rows.Count - 1
                                Dim sYr = .Rows(y).Cells(0).Text
                                'Save
                                Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & GRPS(23, dd) & ", " & sYr)

                                'For each variable/metric
                                For j = 1 To Vars.Count
                                    Print(1, ", " & UNGrpsD(CInt(sYr) - Session("FirstYear") + 1, j, k, II, dd, ss))
                                Next j
                                'end of line
                                PrintLine(1, "")
                            Next y
                        Next dd
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save country slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_country_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,country,diseasetype,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for each country
                For II = 1 To NR
                    'always doing all countries If RegGrps(II) > 0 Then
                    'Use ISO codes for countries
                    If ISO(II) > "" Then
                        sDim1 = ISO(II)
                    Else
                        sDim1 = GRPS(1, II)
                    End If
                    'For all disease types
                    For gg = 1 To 3
                        'each year
                        For k = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(k).Cells(0).Text

                            Print(1, sFile & ", " & sDim1 & ", " & CStr(gg) & ", " & sYr)
                            'for each variable
                            For m = 1 To Vars.Count
                                Print(1, ", " & Vals(CInt(sYr) - Session("FirstYear") + 1, m, j, II, 3, gg))
                            Next m
                            'end of line
                            PrintLine(1, "")
                        Next k 'next year
                    Next gg
                Next II 'next country
            Next j 'next scenario

            FileClose(1)

            'Save country, gender slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\dtype_gender_country_scenario_slice.csv", OpenMode.Output)

            Print(1, "scenario,country,diseasetype,gender,year")
            'For each variable/metric
            For j = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(j)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(j)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(j))
                End If
                Print(1, "," & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and"))
            Next j
            PrintLine(1, "")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for each country
                For II = 1 To NR
                    'always doing all countries If RegGrps(II) > 0 Then
                    'Use ISO codes for countries
                    If ISO(II) > "" Then
                        sDim1 = ISO(II)
                    Else
                        sDim1 = GRPS(1, II)
                    End If
                    'For all disease types
                    For ss = 1 To 3
                        'for both genders
                        For gg = 1 To 2
                            'each year
                            For k = 6 To .Rows.Count - 1
                                Dim sYr = .Rows(k).Cells(0).Text

                                Print(1, sFile & ", " & sDim1 & ", " & CStr(ss) & ", " & GRPS(23, gg) & ", " & sYr)
                                'for each variable
                                For m = 1 To Vars.Count
                                    Print(1, ", " & Vals(CInt(sYr) - Session("FirstYear") + 1, m, j, II, gg, ss))
                                Next m
                                'end of line
                                PrintLine(1, "")
                            Next k 'next year
                        Next gg 'next gender
                    Next ss 'next disease type
                Next II 'next country
            Next j 'next scenario

            FileClose(1)

            .Visible = True
        End With
    End Sub

    'jrs 2011/03/17
    Private Sub SaveAgeCatSlices(grd As Table, Sces As Object, Gps As Object,
WrldG As Object, GrpsG As Object, Vals As Object, ISO As Object, UNGroupCount As Integer,
UNGroupAlias As Object, UNGroupNames As Object, UNGrpsG As Object)
        'Dim j As Integer, pos As Integer, sVar As String, idx As Integer
        Dim sFile As String, gg As Integer, ss As Integer
        Dim sDim1 As String

        With grd
            .Visible = False
            'Save world slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\agecat_world_scenario_slice.csv", OpenMode.Output)

            PrintLine(1, "scenario,agecat,year,POP,POP2")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for all ages
                For gg = 1 To 3
                    'each year
                    For k = 6 To .Rows.Count - 1
                        Dim sYr = .Rows(k).Cells(0).Text

                        Print(1, sFile & ", " & CStr(gg) & ", " & sYr)
                        'for each variable
                        Print(1, ", " & WrldG(CInt(sYr) - Session("FirstYear") + 1, j, gg))
                        PrintLine(1, ", " & Math.Round(WrldG(CInt(sYr) - Session("FirstYear") + 1, j, gg) / WrldG(CInt(sYr) - Session("FirstYear") + 1, j, 0) * 100, 2))
                    Next k 'next year
                Next gg 'next gender
            Next j 'next scenario

            FileClose(1)

            'Save Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\agecat_group_scenario_slice.csv", OpenMode.Output)
            PrintLine(1, "scenario,group,agecat,year,POP,POP2")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To Gps.Count
                    sDim1 = Session("GR1N")(Gps.Item(II))
                    'jrs 2010/12/13
                    If Session("bAlias") Then
                        gg = gGetGroupIdx(Session("oGR1N")(Gps.Item(II)), False, Me)
                    Else
                        gg = gGetGroupIdx(sDim1, False, Me)
                    End If
                    'for ages
                    For ss = 1 To 3
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & sYr)

                            'For each variable/metric
                            Print(1, ", " & GrpsG(CInt(sYr) - Session("FirstYear") + 1, k, II, ss))
                            PrintLine(1, ", " & Math.Round(GrpsG(CInt(sYr) - Session("FirstYear") + 1, k, II, ss) / GrpsG(CInt(sYr) - Session("FirstYear") + 1, k, II, 0) * 100, 2))
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save UN Group Slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\agecat_ungroup_scenario_slice.csv", OpenMode.Output)
            PrintLine(1, "scenario,ungroup,agecat,year,POP,POP2")

            'for each scenario
            For k = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(k)))
                'for each group
                For II = 1 To UNGroupCount
                    sDim1 = UNGroupAlias(II)
                    gg = gGetGroupIdx(UNGroupNames(II), False, Me)
                    'for ages
                    For ss = 1 To 3
                        'For each year
                        For y = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(y).Cells(0).Text
                            'Save
                            Print(1, sFile & ", " & CStr(gg) & ", " & CStr(ss) & ", " & sYr)

                            'For each variable/metric
                            Print(1, ", " & UNGrpsG(CInt(sYr) - Session("FirstYear") + 1, k, II, ss))
                            PrintLine(1, ", " & Math.Round(UNGrpsG(CInt(sYr) - Session("FirstYear") + 1, k, II, ss) / UNGrpsG(CInt(sYr) - Session("FirstYear") + 1, k, II, 0) * 100, 2))
                        Next y
                    Next ss
                Next II
            Next k

            FileClose(1)

            'Save country slice
            FileOpen(1, Session("PutFdrvRun") & "PDE\agecat_country_scenario_slice.csv", OpenMode.Output)

            PrintLine(1, "scenario,country,agecat,year,POP,POP2")

            'Save each scenario
            For j = 1 To Sces.Count
                sFile = CStr(getFileIdx(Sces.Item(j)))
                'for each country
                For II = 1 To NR
                    'always doing all countries If RegGrps(II) > 0 Then
                    'Use ISO codes for countries
                    If ISO(II) > "" Then
                        sDim1 = ISO(II)
                    Else
                        sDim1 = GRPS(1, II)
                    End If
                    'For all ages
                    For gg = 1 To 3
                        'each year
                        For k = 6 To .Rows.Count - 1
                            Dim sYr = .Rows(k).Cells(0).Text

                            Print(1, sFile & ", " & sDim1 & ", " & CStr(gg) & ", " & sYr)
                            'for each variable
                            Print(1, ", " & Vals(CInt(sYr) - Session("FirstYear") + 1, j, II, gg))
                            PrintLine(1, ", " & Math.Round(Vals(CInt(sYr) - Session("FirstYear") + 1, j, II, gg) / Vals(CInt(sYr) - Session("FirstYear") + 1, j, II, 0) * 100, 2))
                        Next k 'next year
                    Next gg
                Next II 'next country
            Next j 'next scenario

            FileClose(1)

            .Visible = True
        End With
    End Sub

    'jrs 2011/01/06
    Private Sub CreateDataSetXML(Tops As Object, Vars As Object, VarsG As Object, VarsET As Object, VarsD As Object)
        Dim pos As Integer, II As Integer, sVar As String, idx As Integer
        Dim sTopics As String, bForm As Boolean
        Dim sUnits As String
        'jrs 2012/09/28
        Try
            'Open vanilla dsxml file
            FileOpen(1, Session("PutFdrvRun") & "dataset.xml", OpenMode.Input)
            FileOpen(2, Session("PutFdrvRun") & "PDE\dataset.xml", OpenMode.Output)
            Dim sLine As String
            sLine = LineInput(1)
            'jrs 2023/05/26
            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "IFs Forecast")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop
            PrintLine(2, Mid(sLine, 1, pos - 1) & "IFs Forecast - " & ModelVersion$ & "</value>")

            'jrs 2023/05/26
            pos = -1
            sLine = LineInput(1)
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "  </topics>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop
            'jrs 2011/03/16
            Dim bPOP As Boolean

            'Topics section
            For II = 1 To Tops.Count
                pos = InStr(Session("VARN")(1, Tops.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Tops.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Tops.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                If idx < 0 Then
                    'Go search for a variable to check if it uses gender
                    idx = SearchVarIdxWGenderInForm(Session("aFormulas")(II))
                End If

                If VCatName(idx) > "" Then
                    sTopics = VCatName(idx)
                    pos = InStr(sTopics, ",")
                    If pos > 0 Then
                        sTopics = Mid(sTopics, 1, pos - 1)
                    End If
                ElseIf Session("sUsrDefCats")(Tops.Item(II)) > "" Then
                    sTopics = Session("sUsrDefCats")(Tops.Item(II))
                Else
                    sTopics = "General"
                End If
                'jrs 2010/12/13 print(2, "    <topic id=""" & sTopics & """>"
                PrintLine(2, "    <topic id=""" & Replace(Replace(sTopics, "/", "_"), " ", "_") & """>")
                PrintLine(2, "      <info>")
                PrintLine(2, "        <name><value xml:lang=""en"">" & sTopics & "</value></name>")
                PrintLine(2, "      </info>")
                PrintLine(2, "    </topic>")
            Next II

            PrintLine(2, sLine)
            sLine = LineInput(1)

            'Concepts section
            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "</concepts>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            'Handling doubly dimensioned variables
            Dim vv As Integer, vvTot As Integer, bDim2 As Boolean

            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "")
                    'jrs 2017/10/03 added replace & with and
                    PrintLine(2, "     <concept id=""" & Replace(Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_"), "&", "and") & """>")
                    PrintLine(2, "        <info>")
                    PrintLine(2, "           <name>")
                    'jrs 2010/12/13 print(2, "              <value>" & sVar & "</value>"

                    If sVar = "POP" Then
                        'jrs 2011/01/10 print(2, "              <value xml:lang=""en"">Estimated absolute population</value>"
                        'jrs 2011/03/17
                        PrintLine(2, "              <value xml:lang=""en"">Population (absolute)</value>")
                    ElseIf bDim2 Then
                        'If bDim2 Then
                        'jrs 2017/10/04 added replace & with and
                        PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx) & " " & Replace(GRPS(VDim2(idx), vv), "&", "and") & " (" & VUnits(idx), "<", "&lt;") & ")</value>")
                    ElseIf idx > 0 Then
                        If VUnits(idx) = "Mil People" Or VUnits(idx) = "Thousand $" Or VUnits(idx) = "Million Years" _
                        Or VUnits(idx) = "Billion $" Or InStr(UCase(VDef(idx)), "PERCENT") > 0 Then
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx), "<", "&lt;") & "</value>")
                        ElseIf VUnits(idx) = "Bil Barrels OE" Then
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx) & " (Barrels OE)", "<", "&lt;") & "</value>")
                        ElseIf VUnits(idx) = "Billion Tons" Then
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx) & " (Tons)", "<", "&lt;") & "</value>")
                        Else
                            PrintLine(2, "              <value xml:lang=""en"">" & VDef(idx) & " (" & VUnits(idx) & ")</value>")
                        End If
                    ElseIf Session("aFormulas")(Vars.Item(II)).Definition > "" Then
                        Print(2, "              <value xml:lang=""en"">" & Replace(Session("aFormulas")(Vars.Item(II)).Definition, """", "&quot;"))
                        If Session("aFormulas")(Vars.Item(II)).Unit > "" And Session("aFormulas")(Vars.Item(II)).Unit <> "Mil People" _
                        And Session("aFormulas")(Vars.Item(II)).Unit <> "Thousand $" And Session("aFormulas")(Vars.Item(II)).Unit <> "Million Years" _
                        And Session("aFormulas")(Vars.Item(II)).Unit <> "Billion $" And InStr(UCase(Session("aFormulas")(Vars.Item(II)).Definition), "PERCENT") = 0 Then
                            Print(2, " (" & Session("aFormulas")(Vars.Item(II)).Unit & ")")
                        End If
                        PrintLine(2, "</value>")
                    Else
                        PrintLine(2, "              <value xml:lang=""en"">" & sVar & "</value>")
                    End If
                    PrintLine(2, "           </name>")
                    PrintLine(2, "           <description>")
                    'jrs 2011/02/02
                    If bDim2 Then
                        'jrs 2017/10/04 replace & with and
                        PrintLine(2, "              <value xml:lang=""en"">" & Replace(Replace(VDef(idx) & " " & GRPS(VDim2(idx), vv) & " (" & VUnits(idx), "<", "&lt;"), "&", "and") & ")</value>")
                    ElseIf idx > 0 Then
                        'jrs 2010/12/13 print(2, "              <value>" & Replace(VDef(idx) & " - " & VUnits(idx), "<", "&lt;") & "</value>"

                        If sVar = "POP" Then
                            'jrs 2011/01/10 print(2, "              <value xml:lang=""en"">Estimated absolute population</value>"
                            PrintLine(2, "              <value xml:lang=""en"">Population (absolute)</value>")
                        ElseIf VUnits(idx) = "Mil People" Or VUnits(idx) = "Thousand $" Or VUnits(idx) = "Million Years" _
                        Or VUnits(idx) = "Billion $" Or InStr(UCase(VDef(idx)), "PERCENT") > 0 Then
                            'If VUnits(idx) = "Mil People" Or VUnits(idx) = "Thousand $" Or VUnits(idx) = "Million Years" _
                            'Or VUnits(idx) = "Billion $" Or InStr(UCase(VDef(idx)), "PERCENT") > 0 Then
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx), "<", "&lt;") & "</value>")
                        ElseIf VUnits(idx) = "Bil Barrels OE" Then
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx) & " - Barrels OE", "<", "&lt;") & "</value>")
                        ElseIf VUnits(idx) = "Billion Tons" Then
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx) & " - Tons", "<", "&lt;") & "</value>")
                        Else
                            PrintLine(2, "              <value xml:lang=""en"">" & Replace(VDef(idx) & " - " & VUnits(idx), "<", "&lt;") & "</value>")
                        End If
                    ElseIf Session("aFormulas")(Vars.Item(II)).Definition > "" Then
                        Print(2, "              <value xml:lang=""en"">" & Replace(Session("aFormulas")(Vars.Item(II)).Definition, """", "&quot;"))
                        If Session("aFormulas")(Vars.Item(II)).Unit > "" And Session("aFormulas")(Vars.Item(II)).Unit <> "Mil People" _
                        And Session("aFormulas")(Vars.Item(II)).Unit <> "Thousand $" And Session("aFormulas")(Vars.Item(II)).Unit <> "Million Years" _
                        And Session("aFormulas")(Vars.Item(II)).Unit <> "Billion $" And InStr(UCase(Session("aFormulas")(Vars.Item(II)).Definition), "PERCENT") = 0 Then
                            Print(2, " (" & Session("aFormulas")(Vars.Item(II)).Unit & ")")
                        End If
                        PrintLine(2, "</value>")
                    Else
                        PrintLine(2, "              <value xml:lang=""en"">" & sVar & "</value>")
                    End If
                    PrintLine(2, "           </description>")
                    PrintLine(2, "        </info>")
                    If idx <= 0 Then
                        idx = SearchVarIdxWGenderInForm(Session("aFormulas")(Vars.Item(II)))
                        bForm = True
                    ElseIf vv = 1 Then
                        bForm = False
                    End If
                    If idx > 0 Then
                        If VCatName(idx) > "" Then
                            pos = InStr(VCatName(idx), ",") - 1
                            If pos < 0 Then pos = Len(VCatName(idx))
                            PrintLine(2, "        <topic ref=""" & Replace(Replace(Left(VCatName(idx), pos), "/", "_"), " ", "_") & """/>")
                        ElseIf Session("sUsrDefCats")(Vars.Item(II)) > "" Then
                            PrintLine(2, "        <topic ref=""" & Replace(Replace(Session("sUsrDefCats")(Vars.Item(II)), "/", "_"), " ", "_") & """/>")
                        Else
                            PrintLine(2, "        <topic ref=""General""/>")
                        End If
                    ElseIf Session("sUsrDefCats")(Vars.Item(II)) > "" Then
                        'jrs 2010/12/13 print(2, "        <topic ref=""" & sUsrDefCats(Vars.Item(II)) & """/>"
                        PrintLine(2, "        <topic ref=""" & Replace(Replace(Session("sUsrDefCats")(Vars.Item(II)), "/", "_"), " ", "_") & """/>")
                    Else
                        PrintLine(2, "        <topic ref=""General""/>")
                    End If
                    PrintLine(2, "        <type ref=""float""/>") 'jrs 2011/02/17 changed from decimal to float

                    If bForm Then
                        sUnits = Session("aFormulas")(Vars.Item(II)).Unit
                    Else
                        sUnits = VUnits(idx)
                    End If
                    If sUnits = "Percent" Then
                        PrintLine(2, "        <attribute concept=""core:is_percentage"">")
                        PrintLine(2, "           <value>true</value>")
                        PrintLine(2, "        </attribute>")
                    ElseIf sUnits = "Mil People" Or sUnits = "Million Years" Or sUnits = "Bil Barrels OE" Or sUnits = "Billion Tons" Then
                        PrintLine(2, "        <attribute concept=""core:decimal_places"">")
                        PrintLine(2, "           <value>0</value>")
                        PrintLine(2, "        </attribute>")
                    ElseIf sVar = "GDPPCP" Or sVar = "GDPP" Then
                        PrintLine(2, "        <attribute concept=""core:unit_text"">")
                        PrintLine(2, "           <value xml:lang=""en"">US dollars at 2005$ PPP prices</value>")
                        PrintLine(2, "        </attribute>")
                        PrintLine(2, "        <attribute concept=""core:unit_symbol"">")
                        PrintLine(2, "           <value xml:lang=""en"">$</value>")
                        PrintLine(2, "        </attribute>")
                        PrintLine(2, "        <attribute concept=""core:unit_symbol_position"">")
                        PrintLine(2, "           <value xml:lang=""en"">START</value>")
                        PrintLine(2, "        </attribute>")
                        PrintLine(2, "        <attribute concept=""core:decimal_places"">")
                        PrintLine(2, "           <value>0</value>")
                        PrintLine(2, "        </attribute>")
                    ElseIf sUnits = "Thousand $" Or sUnits = "Billion $" Then
                        PrintLine(2, "        <attribute concept=""core:unit_text"">")
                        PrintLine(2, "           <value xml:lang=""en"">US dollars at 2011$ prices</value>")
                        PrintLine(2, "        </attribute>")
                        PrintLine(2, "        <attribute concept=""core:unit_symbol"">")
                        PrintLine(2, "           <value xml:lang=""en"">$</value>")
                        PrintLine(2, "        </attribute>")
                        PrintLine(2, "        <attribute concept=""core:unit_symbol_position"">")
                        PrintLine(2, "           <value xml:lang=""en"">START</value>")
                        PrintLine(2, "        </attribute>")
                        PrintLine(2, "        <attribute concept=""core:decimal_places"">")
                        PrintLine(2, "           <value>0</value>")
                        PrintLine(2, "        </attribute>")
                    End If

                    PrintLine(2, "     </concept>")
                Next vv
                'jrs 2011/03/17
                'Adding Age categories
                If V(idx) = "POP" Then
                    bPOP = True

                    PrintLine(2, "")
                    PrintLine(2, "     <concept id=""POP2"">")
                    PrintLine(2, "          <info>")
                    PrintLine(2, "               <name>")
                    PrintLine(2, "                    <value xml:lang=""en"">Population (percent of total)</value>")
                    PrintLine(2, "               </name>")
                    PrintLine(2, "               <description>")
                    PrintLine(2, "                    <value xml:lang=""en"">Population (percent of total)</value>")
                    PrintLine(2, "               </description>")
                    PrintLine(2, "          </info>")
                    PrintLine(2, "          <topic ref=""Population""/>")
                    PrintLine(2, "          <type ref=""float""/>")
                    PrintLine(2, "          <attribute concept=""core:is_percentage"">")
                    PrintLine(2, "               <value>true</value>")
                    PrintLine(2, "          </attribute>")
                    PrintLine(2, "     </concept>")
                End If

            Next II

            PrintLine(2, sLine)
            sLine = LineInput(1)

            'jrs 2010/12/13
            'Slices section for World
            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<metrics>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If

                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1

                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "          <metric concept=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """/>")
                Next vv

            Next II

            'Remove <metrics> tag from old format
            pos = -1
            sLine = LineInput(1)
            sLine = LineInput(1)
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<metrics>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            'Slices section for groups

            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If

                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If
                    PrintLine(2, "          <metric concept =""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """/>")
                Next vv
            Next II

            'Remove <metrics> tag from old format
            pos = -1
            sLine = LineInput(1)
            sLine = LineInput(1)
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<metrics>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            'Slices section for UN groups

            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If

                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If
                    PrintLine(2, "          <metric concept=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """/>")
                Next vv
            Next II

            'Remove <metrics> tag from old format
            pos = -1
            sLine = LineInput(1)
            sLine = LineInput(1)
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<metrics>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            'Slices section for countries
            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "          <metric concept=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """/>")
                Next vv
            Next II

            'Remove <metrics> tag from old format

            'Slices for gender
            '=================
            If VarsG.Count > 0 Then
                'Slices section for World
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for groups
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for UN groups
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for countries
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'jrs 2023/09/26
                'Remove <metrics> tag from old format
                'pos = -1
                'sLine = LineInput(1)
                'sLine = LineInput(1)
            Else
                'Skip Gender Slices
                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""gender_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""etype_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'Slices for Energy type
            '======================
            If VarsET.Count > 0 Then
                'Slices section for World
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for groups
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for UN groups
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for countries
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format
                sLine = LineInput(1)
                'jrs 2023/09/26 sLine = LineInput(1)
            Else
                'Skip Energy Type Slices
                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""etype_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""agecat_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'jrs 2012/09/27
            'Slices for WatSan Access
            '========================
            '    If VarsWSA.Count > 0 Then
            '        'Slices section for World
            '        pos = InStr(20, sLine, "          <metrics>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <metric concept=""" & sVar & """/>"
            '        Next II
            '
            '        'Remove <metrics> tag from old format
            '        pos = InStr(sLine, "</metrics>")
            '        sLine = mid(sLine, pos + 10)
            '
            '        'Slices section for groups
            '        pos = InStr(20, sLine, "          <metrics>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <metric concept=""" & sVar & """/>"
            '        Next II
            '
            '        'Remove <metrics> tag from old format
            '        pos = InStr(sLine, "</metrics>")
            '        sLine = mid(sLine, pos + 10)
            '
            '        'Slices section for UN groups
            '        pos = InStr(20, sLine, "          <metrics>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <metric concept=""" & sVar & """/>"
            '        Next II
            '
            '        'Remove <metrics> tag from old format
            '        pos = InStr(sLine, "</metrics>")
            '        sLine = mid(sLine, pos + 10)
            '
            '        'Slices section for countries
            '        pos = InStr(20, sLine, "          <metrics>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <metric concept=""" & sVar & """/>"
            '        Next II
            '
            '        'Remove <metrics> tag from old format
            '        pos = InStr(sLine, "</metrics>")
            '        sLine = mid(sLine, pos + 10)
            '
            '    Else
            '        'Skip WatSan Access Slices
            '        pos = InStr(sLine, "      <slice id=""watsanaccess_world_scenario_slice"">") - 1
            '        print(2, mid(sLine, 1, pos))
            '        pos = InStr(sLine, "      <slice id=""agecat_world_scenario_slice"">") - 1
            '        sLine = mid(sLine, pos + 1)
            '    End If

            'Slices for Age category
            '========================
            If bPOP Then
                'Slices section for World
                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""dtype_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

            Else
                'Skip Age Cat
                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""agecat_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""dtype_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'Slices for Disease type
            '======================
            If VarsD.Count > 0 Then
                'Slices section for World
                pos = -1
                'jrs 2023/09/26 sLine = LineInput(1)
                'jrs 2023/09/26 sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop


                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for World, gender
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for groups
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for groups, gender
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for UN groups
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for UN groups, gender
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for countries
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format

                'Slices section for countries, gender
                pos = -1
                sLine = LineInput(1)
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<metrics>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <metric concept=""" & sVar & """/>")
                Next II

                'Remove <metrics> tag from old format
                pos = -1
                sLine = LineInput(1)
                'jrs 2023/09/26 sLine = LineInput(1)
            Else
                'Skip Disease Type Slices
                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<slice id=""dtype_world_scenario_slice"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "</slices>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'jrs 2010/12/13
            'Table for World

            pos = -1
            sLine = LineInput(1)
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "world_scenario_slice_table")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<data>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop


            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next vv
            Next II

            'Table for groups
            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "group_scenario_slice_table")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop


            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<data>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop


            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next vv
            Next II

            'Table for UN groups
            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "ungroup_scenario_slice_table")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            pos = -1
            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<data>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next vv
            Next II

            'Table for countries
            pos = -1
            'jrs 2023/09/27
            PrintLine(2, sLine)
            sLine = LineInput(1)

            Do While pos < 1 OrElse sLine = ""
                pos = InStr(sLine, "<data>")
                If pos < 1 OrElse sLine = "" Then
                    pos = -1
                    PrintLine(2, sLine)
                    sLine = LineInput(1)
                End If
            Loop

            For II = 1 To Vars.Count
                'Read Variable
                pos = InStr(Session("VARN")(1, Vars.Item(II)), "[")
                If pos > 0 Then
                    sVar = Mid(Session("VARN")(1, Vars.Item(II)), 1, pos - 1)
                Else
                    sVar = Session("VARN")(1, Vars.Item(II))
                End If
                idx = IfsVars.Item(sVar).Index
                bDim2 = False
                vvTot = 1
                If idx > 0 Then
                    If VDim2(idx) <> 23 And VDim3(idx) <> 23 And VDim2(idx) <> 5 And VDim2(idx) <> 29 Then  'jrs 2012/10/01  And VDim2(idx) <> 29 'jrs 2012/09/27 And VDim2(idx) <> 28
                        If VDim2(idx) > 0 Then
                            vvTot = MaxVal(VDim2(idx)) - 1
                            bDim2 = True
                        End If
                    End If
                    'jrs 2012/09/27
                    '            'jrs 2011/03/16
                    '            If VDim2(idx) = 28 Then
                    '                vvTot = 0
                    '            End If
                End If

                For vv = 1 To vvTot
                    'Doubly dimensioned vars
                    If bDim2 Then
                        sVar = V(idx) & CStr(vv)
                    End If

                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next vv
            Next II

            'Gender Tables
            '=============
            If VarsG.Count > 0 Then
                'Table for World
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "gender_world_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for groups
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "gender_group_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop


                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for UN groups
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "gender_ungroup_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop


                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for countries
                pos = -1
                'jrs 2023/09/27
                PrintLine(2, sLine)
                sLine = LineInput(1)

                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsG.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsG.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsG.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsG.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II
            Else
                'Skip Gender Tables
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""gender_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""etype_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'Energy Type Tables
            '==================
            If VarsET.Count > 0 Then
                'Table for World
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "etype_world_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for groups
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "etype_group_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for UN groups
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "etype_ungroup_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop


                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for countries
                pos = -1
                'jrs 2023/09/27
                PrintLine(2, sLine)
                sLine = LineInput(1)

                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsET.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsET.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsET.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsET.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II
            Else
                'Skip Energy Type Tables
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""etype_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                sLine = LineInput(1)
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""agecat_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'jrs 2012/09/27
            '    'WatSan Access Tables
            '    '==================
            '    If VarsWSA.Count > 0 Then
            '        'Table for World
            '        pos = InStr(sLine, "watsanaccess_world_scenario_slice_table") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        pos = InStr(sLine, "          <data>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>" 'jrs 2011/02/17 changed from decimal to float
            '        Next II
            '
            '        'Table for groups
            '        pos = InStr(sLine, "watsanaccess_group_scenario_slice_table") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        pos = InStr(sLine, "          <data>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>" 'jrs 2011/02/17 changed from decimal to float
            '        Next II
            '
            '        'Table for UN groups
            '        pos = InStr(sLine, "watsanaccess_ungroup_scenario_slice_table") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        pos = InStr(sLine, "          <data>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>" 'jrs 2011/02/17 changed from decimal to float
            '        Next II
            '
            '        'Table for countries
            '        pos = InStr(16, sLine, "          <data>") - 1
            '        print(2, mid(sLine, 1, pos))
            '        'remove what has been printed
            '        sLine = mid(sLine, pos + 1)
            '
            '        For II = 1 To VarsWSA.Count
            '            'Read Variable
            '            pos = InStr(session("VARN")(1, VarsWSA.Item(II)), "[")
            '            If pos > 0 Then
            '                sVar = mid(session("VARN")(1, VarsWSA.Item(II)), 1, pos - 1)
            '            Else
            '                sVar = session("VARN")(1, VarsWSA.Item(II))
            '            End If
            '            print(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>" 'jrs 2011/02/17 changed from decimal to float
            '        Next II
            '    Else
            '        'Skip Energy Type Tables
            '        pos = InStr(sLine, "      <table id=""watsanaccess_world_scenario_slice_table"">") - 1
            '        print(2, mid(sLine, 1, pos))
            '        pos = InStr(sLine, "      <table id=""agecat_world_scenario_slice_table"">") - 1
            '        sLine = mid(sLine, pos + 1)
            '    End If

            'Age Cat
            '=======
            If bPOP Then
                'Tables section for World
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""dtype_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

            Else
                'Skip Energy Type Tables
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""agecat_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""dtype_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'Disease Type Tables
            '==================
            If VarsD.Count > 0 Then
                'jrs 2023/09/27
                ''Table for World
                'pos = -1
                'Do While pos < 1 OrElse sLine = ""
                '    pos = InStr(sLine, "dtype_world_scenario_slice_table")
                '    If pos < 1 OrElse sLine = "" Then
                '        pos = -1
                '        PrintLine(2, sLine)
                '        sLine = LineInput(1)
                '    End If
                'Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for World, gender
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "dtype_gender_world_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop


                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for groups
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "dtype_group_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for groups, gender
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "dtype_gender_group_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for UN groups
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "dtype_ungroup_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for UN groups, gender
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "dtype_gender_ungroup_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for countries
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "dtype_country_scenario_slice_table")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

                'Table for countries, gender
                pos = -1
                'jrs 2023/09/27
                PrintLine(2, sLine)
                sLine = LineInput(1)

                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<data>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                For II = 1 To VarsD.Count
                    'Read Variable
                    pos = InStr(Session("VARN")(1, VarsD.Item(II)), "[")
                    If pos > 0 Then
                        sVar = Mid(Session("VARN")(1, VarsD.Item(II)), 1, pos - 1)
                    Else
                        sVar = Session("VARN")(1, VarsD.Item(II))
                    End If
                    PrintLine(2, "          <column id=""" & Replace(Replace(Replace(Replace(sVar, "%", "Pct"), " ", "_"), "/", "_"), "*", "_") & """ type=""float""/>") 'jrs 2011/02/17 changed from decimal to float
                Next II

            Else
                'Skip Disease Type Tables
                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "<table id=""dtype_world_scenario_slice_table"">")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        PrintLine(2, sLine)
                        sLine = LineInput(1)
                    End If
                Loop

                pos = -1
                Do While pos < 1 OrElse sLine = ""
                    pos = InStr(sLine, "</tables>")
                    If pos < 1 OrElse sLine = "" Then
                        pos = -1
                        sLine = LineInput(1)
                    End If
                Loop

            End If

            'Finish until end of file
            Do While Not EOF(1)
                PrintLine(2, sLine)
                sLine = LineInput(1)
            Loop

            FileClose(1)
            FileClose(2)

        Catch ex As Exception
            Response.Write("<script>alert('" & ex.Message & "');</script>")
            'Resume

        End Try


    End Sub

End Class

