'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''          2009-05-23  Radial Form base in ProEssentials Form created by Mohammod and Jose Solorzano
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
'   Copyright by Barry B. Hughes 1988-2003
'   Model code available for review and education only
'   Reproduction or incorporation in other software by permission only
'   Last revision by BBH on July 17, 2002 - added MTI's validation code
Option Strict Off
Option Explicit On

Imports IFs.Data

Partial Class frm_BaseRadial
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

        'WinFormsToWeb_MenuManager.SetFormName("frm_ProEssential")
        If Not Page.IsPostBack Then
            'jrs 2012/05/17
            '*** Begin: logic to load with url ***
            Dim bReload As Boolean = False
            Dim sListNames As String = ""
            If Not (Request.QueryString("ListNames") Is Nothing) Then
                sListNames = Request.QueryString("ListNames")
                If Not (Session("ListLoaded") Is Nothing) Then
                    If Session("ListLoaded") <> sListNames Then
                        bReload = True
                    End If
                Else
                    bReload = True
                End If
                Session("ListLoaded") = sListNames
            End If

            'jrs 2011/04/05
            If Session("COUNTE") = 0 Or bReload Then
                'Read querystring
                Dim sHistFor As String, GrpOp As Integer
                sHistFor = Request.QueryString("HistFor")
                GrpOp = CInt(Request.QueryString("GrpOp"))
                Dim sDim1 As String, sDim2 As String, sDim3 As String, sFiles As String
                If Not Request.QueryString("Dim1") Is Nothing Then
                    sDim1 = Request.QueryString("Dim1")
                End If
                If Not Request.QueryString("Dim2") Is Nothing Then
                    sDim2 = Request.QueryString("Dim2")
                End If
                If Not Request.QueryString("Dim3") Is Nothing Then
                    sDim3 = Request.QueryString("Dim3")
                End If
                sFiles = Request.QueryString("File")
                If sFiles Is Nothing Then
                    sFiles = "1"
                End If

                Dim rs As DataTable
                rs = getVarLists(sListNames, sHistFor, Session("PutFdrvRun"))
                Session("PackagedDispRs") = rs
                Dim p As Integer
                p = InStr(sListNames, """,""")
                If p > 0 Then
                    'Multiple lists
                    Session("TableTitle") = sListNames
                Else
                    'Single list
                    Session("TableTitle") = Mid(sListNames, 2, Len(sListNames) - 2)
                End If

                Session("GType") = "LINE"
                Dim IfsPars As New myParameters
                IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
                    Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
                    Session("PutFdrvRun"), Session("CurrencySym"))
                Session("GraphDisplayParent") = "frm_PackagedDisplaysFlex.aspx"
                'Load list boxes
                Dim lstDim As New ListBox, lstDim2 As New ListBox, lstDim3 As New ListBox
                Dim lstFiles As New ListBox
                lstDim.ID = "lstDim"
                lstDim2.ID = "lstDim2"
                lstDim3.ID = "lstDim3"
                LoadAllDims(lstDim, Session("AllDimsIdx"))
                LoadAllDims(lstDim2, Session("AllDimsIdx"))
                LoadAllDims(lstDim3, Session("AllDimsIdx"))

                If sDim1 = "" Then
                    sDim1 = Trim(CStr(lstDim.Items.Count - 1))
                End If
                If sDim2 = "" Then
                    sDim2 = Trim(CStr(lstDim2.Items.Count - 1))
                End If
                If sDim3 = "" Then
                    sDim3 = Trim(CStr(lstDim3.Items.Count - 1))
                End If

                ReDim Preserve Session("DimIdxs")(lstDim.Items.Count - 1)
                ReDim Preserve Session("DimIdxs2")(lstDim2.Items.Count - 1)
                ReDim Preserve Session("DimIdxs3")(lstDim3.Items.Count - 1)

                LoadFiles(Session("VarSelCheck"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                    Session("ActiveBase"), Session("VarSelCheckHis"), Session("PutFdrvRun2"), Session("WorkingFile"),
                    Session("FilesSelected"), Session("IFsRunFiles"), Session("RunFiles"), Session("ShortSce"), lstFiles)
                If sFiles > "" Then
                    lstFiles.Items(0).Selected = False
                End If

                ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)

                'Set Region options
                Select Case GrpOp
                    Case 0 'Regions
                        Session("bUseReg") = True
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                    Case 1 'Groups
                        Session("bUseReg") = False
                        Session("bUseGroup") = True
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = False
                    Case 2 'Dec Groups
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = True
                        Session("bUseGList") = False
                    Case 3 'G-Lists
                        Session("bUseReg") = False
                        Session("bUseGroup") = False
                        Session("bUseGroupDec") = False
                        Session("bUseGList") = True
                End Select

                'Set List Boxes
                URLToDimLst(sDim1, lstDim)
                URLToDimLst(sDim2, lstDim2)
                URLToDimLst(sDim3, lstDim3)
                URLToFileLst(sFiles, lstFiles, Session("txtFiles"), Session("IFsRunFiles"), Session("PDFilesIdxs"), Session("FilesSelected"),
                    Session("RunFiles"), Session("PutFdrvRun2"), Session("ShortSce"), Session("ScenarioFileDir"))

                'Load Package
                gLoadPackage(rs, Session("COUNTE"), Me, Session("ActiveBase"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                    Session("oldFirstYear"), Session("oldLastYear"), Session("IFs_MDB2Active"), lstDim, lstDim2, lstDim3, lstFiles, Session("TableTitle"),
                    Session("DimIdxs"), Session("DimIdxs2"), Session("DimIdxs3"),
                    Session("bUseGroup"), Session("bUseGroupDec"), Session("bUseGList"), Session("bUseCtryAndGroup"), Session("bUseCtryAndGroupDec"),
                    Session("SelIdx"), Session("AllDimsIdx"), Session("bIntercept"),
                    Session("PDFilesIdxs"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"), Session("FinYearHis"), Session("buseAllHis"),
                    Session("bFillHis"), Session("bFillHisOnlyHoles"), Session("StaYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("aFormulas"), Session("bComputation"),
                    Session("bLoadForDisplaySuccess"), Session("FileforDisplay"), IfsPars, Session("PutFdrvRun"), Session("MaxFinYearHis"),
                    Session("FileNumbr"), Session("RunFiles"), Session("ShortSce"), Session("CurrencyVal"), Session("CurrencySym"), Session("SensitivityFlag"),
                    Session("PopMales"), Session("PopFemales"), sListNames, Session("sUsrDefCats"), Session("txtFiles"))
            End If

            'Year validation
            If Not IsNothing(Request.QueryString("Year")) Then
                If Not IsNumeric(Request.QueryString("Year")) Then
                    LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

                If Session("FirstYear") = FirstYearForecast Then
                    If CInt(Request.QueryString("Year")) > Session("LastYearForecast") Or CInt(Request.QueryString("Year")) < FirstYearHistory Then
                        LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If

                ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                    If CInt(Request.QueryString("Year")) > Session("LastYearForecast") Or CInt(Request.QueryString("Year")) < FirstYearHistory Then
                        LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                End If

                Session("PYear") = CInt(Request.QueryString("Year")) - Session("FirstYear") + 1
                If Session("PYear") < 1 Then Session("PYear") = 1
                If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then
                    If UCase(Session("ActiveBase")) = "IFSBASE.RUN" And Session("PYear") + Session("FirstYear") - 1 <= Session("LastYearForecast") Then
                        Session("DisplayLastYear") = Session("PYear") + Session("FirstYear") - 1
                    Else
                        Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
                    End If
                End If
                If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)
                Session("PE_CommonMenu") = "Display"
                If Session("YearsSel") Is Nothing Then
                    ReDim Session("YearsSel")(1)
                End If
                Session("YearsSel")(0) = Session("PYear")
            Else
                Session("PYear") = 1
                'jrs 2012/12/14
                If Session("NumYearsSel") = Nothing Then
                    Session("NumYearsSel") = 1
                End If
            End If

            Session("mnuOnScalingChecked") = False
            Session("mnuOffScalingChecked") = True
            If Not IsNothing(Request.QueryString("Scale")) Then
                pCOUNTE = Session("COUNTE")
                If Request.QueryString("Scale") = 1 Then
                    If Session("DisYear") Is Nothing Then
                        Session("DisYear") = Session("FinYear")(1)
                    End If

                    Session("mnuOnScalingChecked") = True
                    Session("mnuOffScalingChecked") = False
                    If Not (Session("CalcScaling") = True) Then
                        ReDim Session("LocalPlot")(MaxYears, MaxVariables)
                        Dim aa As Integer, ab As Integer
                        For aa = 0 To MaxYears - 1
                            For ab = 0 To MaxYears - 1
                                Session("LocalPlot")(aa, ab) = Session("PLOT")(aa, ab)
                            Next ab
                        Next aa

                        Scaling()
                    End If
                Else
                    If CheckNegsPLOT() Then
                        If Not (Session("CalcScaling") = True) Then
                            ReDim Session("LocalPlot")(MaxYears, MaxVariables)
                            Dim aa As Integer, ab As Integer
                            For aa = 0 To MaxYears - 1
                                For ab = 0 To MaxYears - 1
                                    Session("LocalPlot")(aa, ab) = Session("PLOT")(aa, ab)
                                Next ab
                            Next aa

                            Scaling()
                        End If
                        Session("mnuOffScalingCheked") = False
                        Session("mnuOffScalingEnabled") = False
                    Else
                        If Session("CalcScaling") = True Then
                            Call ResetScaling()
                            Session("CalcScaling") = False
                        End If
                    End If
                End If
            End If

            'jrs 2012/12/14
            If Not Request.QueryString("VarperG") Is Nothing Then
                Session("VarperG") = Request.QueryString("VarperG")
                Session("SourceRadial") = "VarperGraph"
            End If
            '*** End: logic to load with url ***

            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim ii As Short
            Dim sValidationVar As Object
            Dim j As Object
            '------------------------------------------------------------
            'JRS 11/03/03
            If Session("DisplayLastYear") = 0 Then
                Session("DisplayLastYear") = Session("LastYear")
            End If

            Dim i As Short

            'mti Validation changes 19 August, 2002
            sInterval = Session("Interval")
            'mti Validation changes 19 August, 2002

            'From ChartHist
            '--------------------------------

            '   Set form and graph dimensions
            '   -----------------------------

            'mti Validation changes
            If InStr(1, Session("GType"), "Validation") > 0 Then
                pCOUNTE = 0
                For j = 1 To Session("COUNTE") - 1 'the last one need to be forecast
                    'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object sValidationVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    sValidationVar = Trim(Session("VARN")(4, j))
                    'UPGRADE_WARNING: Couldn't resolve default property of object sValidationVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If Mid(sValidationVar, 1, 2) = "VH" Then 'asc("V") = 86
                        'UPGRADE_WARNING: Couldn't resolve default property of object sValidationVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        sValidationVar = "VF" & Mid(sValidationVar, 3)
                        'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        For ii = j + 1 To Session("COUNTE")
                            'UPGRADE_WARNING: Couldn't resolve default property of object sValidationVar. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            If Trim(Session("VARN")(4, ii)) = sValidationVar Then
                                pCOUNTE = pCOUNTE + 1
                                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'UPGRADE_WARNING: Couldn't resolve default property of object VH(pCOUNTE). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                VH(pCOUNTE) = j
                                'UPGRADE_WARNING: Couldn't resolve default property of object VF(pCOUNTE). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                VF(pCOUNTE) = ii
                                Exit For
                            End If
                        Next ii
                    End If
                Next j
                'pCOUNTE = pCOUNTE * 2
            Else
                pCOUNTE = Session("COUNTE")

            End If
            'mti Validation changes

            'mtiwebformsMarch (1) 
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Width' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Width.html'.
            'frm_ProEssential.DefInstance.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - HalfInch + 200)
            'CONVERSION_ISSUE: Property 'System.Windows.Forms.Form.Height' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Height.html'.
            'frm_ProEssential.DefInstance.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) - HalfInch / 2)
            'CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Left' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Left.html'.
            'frm_ProEssential.DefInstance.Left = 0 
            'CONVERSION_NOTE: Property 'System.Windows.Forms.Form.Top' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Top.html'.
            'frm_ProEssential.DefInstance.Top = 0 

            'mtiwebformsMarch (1)- comment because everything in the other page

            'End If 
            'mtiwebformsMarch (1) -end comment






            '   Choose Menu Options
            '   -------------------

            'jrs 2005/11/29
            'If Session("GType") = "PIE" Or Session("GType") = "COHORT" Then
            '    mnuAdvance.Visible = True
            '    mnuRegress.Visible = True
            '    mnuCommon.Visible = False
            '    mnuStatistics.Visible = False
            'ElseIf Session("GType") = "SCAT" Then
            '    mnuAdvance.Visible = True
            '    mnuRegress.Visible = True
            '    mnuCommon.Visible = False
            '    mnuStatistics.Visible = True
            'Else
            '    mnuAdvance.Visible = False
            '    mnuRegress.Visible = False
            '    mnuCommon.Visible = True
            '    mnuStatistics.Visible = False
            'End If

            '         'mtiwebformsMarch (1)  -commented
            '         'Call DisplayGraph()
            '         'mtiwebformsMarch (1)

            ''JRS 04-17-01 BEGIN
            'CommonPlot = "False"
            ''JRS 04-17-01 END

            'jrs 2008/01/18
            'save original name to display
            ReDim Session("oVARN0")(Session("COUNTE"))
            For i = 1 To Session("COUNTE")
                Session("oVARN0")(i) = Session("VARN")(0, i)
            Next i

            'JAM 2015-03-31 For resetting variables in third dimmension mode
            If Not Session("LabelChanged") Then

                ReDim Session("gthirdVARN0")(Session("COUNTE"))
                For i = 1 To Session("COUNTE")
                    Session("gthirdVARN0")(i) = Session("VARN")(0, i)
                Next i
            End If

            'jrs 2011/10/19
            Session("MultipleGraphs") = False
            'jrs 2012/05/02
            If Session("bAlias") = Nothing Then
                Session("bAlias") = False
            ElseIf Session("bAlias") = True Then
                Session("bAlias") = False
                mnuAlias_Click()
            End If
        Else 'jrs 2012/05/17
            pCOUNTE = Session("COUNTE")
        End If
        'JRS 09/03/04
        'jrs 2011/05/30 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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

    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pego' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents PegoNet1 As Gigasoft.ProEssentials.Pego 
    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pepco' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents PepcoNet1 As Gigasoft.ProEssentials.Pepco 
    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pesgo' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents PesgoNet1 As Gigasoft.ProEssentials.Pesgo 
    'CONVERSION_NOTE: Class 'Gigasoft.ProEssentials.Pe3do' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Friend WithEvents Pe3doNet1 As Gigasoft.ProEssentials.Pe3do 
    Protected WithEvents WinFormsToWeb_MessageBox As ArtinSoft.WinFormsToWeb.WebControls.MessageBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region

    Dim sInterval As Short

    Dim pCOUNTE As Short
    Dim VH(100) As Object
    Dim VF(100) As Object


    '-------------------------------JAM 2009-06-10-----------------------------------------------
    'Dim sInterval%

    'Dim CommonPlot As Boolean
    'Dim oVARN0() As String
    'Dim LocalPlot()
    'Dim Degree()
    'Dim YearsSel()
    'Dim NewIndex()
    '-----------------------------------------------------------------------------------------




    Private Sub frm_BaseRadial_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim i As Integer
        Dim vThirdDimmOk As Boolean


        If Not Page.IsPostBack Then
            ReDim Session("PE_MaxValC")(MaxVariables), Session("PE_MinValC")(MaxVariables), Session("PE_diffvalc")(MaxVariables) 'Used in Common Display
            Dim a As Integer
            Session("PE_CommonPlot") = "False"

            'jrs 2011/10/19
            If Session("MultipleYears") = Nothing Then
                Session("MultipleYears") = False
            End If
            'jrs 2012/12/14
            If Session("SourceRadial") = Nothing Then
                Session("SourceRadial") = "Package"
            End If

            'JAM 2009-06-20 Creating Session Variables required
            'JAM 2009-07-15 In case that Post is comming from a Form differents VarperGraph 
            If Not Session("MultipleYears") And Session("SourceRadial") = "Package" Then    'JAM 2009-06-27 Adding Option for Multiple Years
                'jrs 2012/05/22
                If IsNothing(Request.QueryString("Scale")) Then
                    Session("CalcScaling") = False
                End If

                Session("SplitGraph") = 0
                Session("ItemsbySet") = 0
                Session("NumYearsSel") = 1
                Session("MultipleYears") = False
                Session("Continuous") = False
                Session("ChangeSubLabels") = False
                Session("MaxData") = 0
                Session("Angle") = 0.0
                Session("CommDimm") = False
                'jrs 2012/01/26
                If IsNothing(Session("IsChangedNum")) Then
                    Session("IsChangedNum") = False
                End If


                Session("MultipleGraphs") = False

                Session("bMonochrome") = False
                'jrs 2012/05/17
                If IsNothing(Request.QueryString("Year")) Then
                    ReDim Session("YearsSel")(1)
                End If

                Session("MaxRadious") = 0   'JAM 2009-07-15 Used in Common Scaling 

                'jrs 2012/05/22
                If IsNothing(Request.QueryString("Scale")) Then
                    'JAM 2009-07-15
                    'JAM 2009-07-10     Flags are turn on only is the first time
                    If CheckNegsPLOT() Then
                        LoadMessage("There are negative values loaded. The scaling option will be turned on.", Session("Message"))
                        'Support.FormManager.StateStore(Me)
                        'Support.FormManager.Show(Me, "frm_Message.aspx")
                        Call mnuOnScaling_Click()
                        Session("mnuOffScalingCheked") = False
                        Session("mnuOffScalingEnabled") = False
                    Else
                        Session("mnuOnScalingChecked") = False
                        Session("mnuOffScalingChecked") = True
                    End If
                End If
            Else
                a = 0
            End If



            ReDim Session("oVARN0")(Session("COUNTE"))


            'JAM 2009-07-09 Moved for not reset Session("oVARN0")
            Dim j, aa, ab As Integer


            For j = 1 To Session("COUNTE")
                Session("oVARN0")(j) = Session("VARN")(0, j)
            Next j

            '-----------------------------------
            'JAM 2014-08-30 Preserving data for point labels
            ReDim Session("VNFirst")(Session("COUNTE"))
            ReDim Session("VNd1")(Session("COUNTE"))
            ReDim Session("VNd2")(Session("COUNTE"))
            ReDim Session("VNd3")(Session("COUNTE"))

            'JAM 2014-08-31
            Session("SelScenarioLabel") = False

            For j = 1 To Session("COUNTE")
                Session("VNFirst")(j) = LTrim(RTrim(Session("VARN")(0, j)))
                Session("VNd1")(j) = LTrim(RTrim(Session("FDNG1")(j)))
                Session("VNd2")(j) = LTrim(RTrim(Session("FDNG2")(j)))
                Session("VNd3")(j) = LTrim(RTrim(Session("FDNG3")(j)))
            Next j
            '----------------------------------


            'jrs 2012/05/22
            If Not Session("CalcScaling") Then
                'JAM 2009-06-20 Assigning LocalPlot Array
                ReDim Session("LocalPlot")(MaxYears, MaxVariables)
                For aa = 0 To MaxYears - 1
                    For ab = 0 To MaxYears - 1
                        Session("LocalPlot")(aa, ab) = Session("PLOT")(aa, ab)
                    Next ab
                Next aa
            End If

            'jrs 2011/10/14
            Dim MinYear As Short
            Dim MaxYear As Short
            Dim ii As Integer

            MaxYear = 1

            MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            For ii = 1 To pCOUNTE
                If Session("FinYear")(ii) > MaxYear Then MaxYear = Session("FinYear")(ii)
                If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
            Next ii

            If Session("Interval") > MaxYear Then
                Session("Interval") = MaxYear - 1
            End If
            If Session("DisplayTime") = 0 Then
                Session("DisYear") = MinYear
            Else

                If Session("DisplayLastYear") - Session("FirstYear") + 1 < MaxYear Then
                    Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
                Else
                    Session("DisYear") = MaxYear
                End If
            End If

            'jrs 2012/12/14 
            If Session("YearsSel") Is Nothing Then
                ReDim Session("YearsSel")(1)
            End If

            If Session("YearsSel")(0) = Nothing Then
                'RadialCtrl.PeUserInterface.Menu.LegendLocation() = MenuControl.Show
                If Not Session("MultipleYears") Then    'JAM 2009-06-27 Adding Option for Multiple Years
                    Session("YearsSel")(0) = 1
                End If
            Else
                'jrs 2012/05/17
                If lstYears.Items.Count > 0 Then
                    For i = 0 To Session("NumYearsSel") - 1
                        lstYears.Items(Session("YearsSel")(i) - 1).Selected = True
                    Next i
                End If
            End If


            lstYears.Items.Clear()
            For i = 1 To Session("DisYear")
                lstYears.Items.Add((Session("FirstYear") - 1) + i)
            Next i

            'JAM 2014-11-01 Identifying Dimmensions Patterns
            ''PlusThirdDimmAnalyze()

        End If

        'JAM 2009-05-30 Commented 
        'cswmWebMenu.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
        'cswmWebMenu.Groups("Scaling").Items("Scale").IconImage = "blank.gif"


        '-------------------------------------JAM 2009-06-14-------------------------------------------


        'JAM 2009-06-27 Deactivated Graph
        'RadialCtrl.Enabled = True
        'RadialCtrl.Visible = True

        If Session("DisplayLastYear") = 0 Then
            Session("DisplayLastYear") = Session("LastYear")
        End If

        sInterval = Session("Interval")

        'jrs 2011/10/17
        If Not Page.IsPostBack Then
            'jrs 2012/01/26 changed from or to and, so that it woul updated itemsbyItem when changed
            'even if SourceRadila = Package
            'JAM 2009-06-26     Code related with Session
            If Session("SourceRadial") = "Package" And Not Session("IsChangedNum") Then 'JAM 2009-07-15
                pCOUNTE = Session("COUNTE")

                If pCOUNTE <= 6 Then
                    Session("SplitGraph") = 1  'Normal Graphing
                    Session("ItemsbySet") = pCOUNTE
                    'RadialCtrl.PeData.Points = Session("ItemsbySet") + 1

                Else
                    If (pCOUNTE Mod 6) = 0 Then
                        Session("SplitGraph") = (pCOUNTE / 6) 'Multiple GRaphing
                        Session("ItemsbySet") = 6
                        'RadialCtrl.PeData.Points = Session("ItemsbySet") + 1

                    Else
                        Session("SplitGraph") = 1  'Normal Graphing
                        Session("ItemsbySet") = 6
                        'RadialCtrl.PeData.Points = Session("ItemsbySet") + 1

                    End If
                End If
            Else

                Call Complement_VarpGraph()


            End If


            'JAM 2014-11-20 Using continuous/discontinuous mode
            vThirdDimmOk = PlusThirdDimmAnalyze()
            ''Response.Write("Hello")


            Session("gThirdDimmOk") = vThirdDimmOk 'JAM 2014-12-07
            If Not vThirdDimmOk Then

                Call AnalyzePattern()
            Else
                PlusThirdDimPattern()

            End If



            lstYears.Visible = False
            cmdDraw.Visible = False

            lstYears.Enabled = False
            cmdDraw.Enabled = False

            'JAM 2014-11-20 Using continuos/discontinuos mode
            If Not vThirdDimmOk Then
                'JAM 2014-08-24 Moved outside Postback If clause
                '-------------------------------------------------------------------------------------------
                'jrs 2011/10/13
                Call DiscPattern()
            End If

        End If

        '-------------------------------------------------------------------------------------------
        'jrs 2011/10/13
        ''Call DiscPattern()

        Call ShowRelevantGraph()

        SaveWorkingSce()

        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"
        cswmWebMenu.AccessorKeyCode = 27

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_4a6c"


        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("ProEssentialParent") & "';"
        'cswmGroup1.OnMenuBarClick = "location.href='" & Session("BaseRadialParent") & "';"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        'jrs 2026/08/07
        If StandAloneMode Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Main Menu"
            cswmGroup1.ID = "mnuMainMenu"
            cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Time"
        cswmGroup1.ID = "Group_Time"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Advance"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Regress"

        'jrs 2012/05/02
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Go To Year"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Advance"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Regress"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Scaling"
        cswmGroup1.ID = "Group_Scaling"

        'JAM 2009-06-20 Adding Scaling Options
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "On"

        'JAM 2009-07-15 CheckBox Review
        If Session("mnuOnScalingChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Off"
        'JAM 2009-07-15 CheckBox Review
        If Session("mnuOffScalingChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Definition"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Common Scaling"

        'JAM 2009-07-18 Enabling if We're in Multiple Graphs mode
        If Session("MultipleGraphs") Then
            cswmItem.Enabled = True
            'JAM 2009-07-15 CheckBox Review
            If Session("mnuCommScalChecked") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        Else
            cswmItem.IconImage = "blank.gif"
            cswmItem.Enabled = False
        End If


        '''''''''''''''''''''''''''''''''''''''''''''''''''''''


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Format"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Set Title, Display Interval, or Year"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display/Run Horizon"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Alias for Group Names"
        If Session("bAlias") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'JAM 2009-06-27 
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Set SubSet Labels"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Colors On"
        'JAM 2009-07-15 CheckBox Review
        If Not Session("mnuColorsOnChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Colors Off"

        'JAM 2009-07-15 CheckBox Review
        If Session("mnuColorsOnChecked") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use Common Dimensions for Labels"
        cswmItem.IconImage = "blank.gif"

        'JAM 2009-07-10    Assigning The icon properly in every Form Load   
        If Session("CommDimm") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels First Dimension Only"
        cswmItem.IconImage = "blank.gif"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Second Dimension Only"
        cswmItem.IconImage = "blank.gif"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Third Dimension Only"
        cswmItem.IconImage = "blank.gif"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Dimensions Only"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels No Dimensions"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Scenario Names"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legend Labels Reset"
        cswmItem.IconImage = "blank.gif"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Multiple Graphs"
        cswmGroup1.ID = "mnuMulGraphs"
        cswmItem.IconImage = "blank.gif"
        'JAM 2009-07-15 CheckBox Review
        If Session("MultipleGraphs") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        If Not Session("gThirdDimmOk") Then 'JAM 2014-12-16 Only If there is a data pattern
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Continuous Mode"
            cswmItem.ID = "mnuContinuous"
            cswmItem.IconImage = "blank.gif"

            If Session("Continuous") Then
                cswmItem.IconImage = "checkbox.gif"
            Else
                cswmItem.IconImage = "blank.gif"
            End If
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Variables per Graph"
        cswmItem.IconImage = "blank.gif"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display Multiple Years"
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Display in Different Graphs"
        If Session("MultipleGraphs") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If




        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/03 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16031);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")


        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.IconHeight = 16
        'cswmItem.Caption = "Common"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Group', Icon, IconHover);"
        'cswmItem.IconImage = "blank.gif"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Scale"
        'cswmItem.IconHeight = 16
        ''cswmItem.OnClick = "ToggleGroupReg(wmMain, flaggroup, 'Region', Icon, IconHover);"
        'cswmItem.IconImage = "checkbox.gif"


        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        PackJS()

        'lblJSLine.Text = Replace(lblJS.Text, "AnyChartDataforCohorts.aspx", "AnyChartDataforMort.aspx")

    End Sub

    Private Sub PackJS()
        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        If Session("MultipleGraphs") Then
            For l = 1 To 4
                'This logic tries to avoid creating empty graphs
                If Not Session("gThirdDimmOk") Then
                    If Session("SplitGraph") * Session("NumYearsSel") >= l Then
                        sJscript2 = sJscript2 & PackJSContainer(l.ToString)
                    End If
                Else
                    If Session("SetLegends")(l) > "" Or Session("NumYearsSel") > 1 Then
                        sJscript2 = sJscript2 & PackJSContainer(l.ToString)
                    End If
                End If
            Next
        Else
            sJscript2 = sJscript2 & PackJSContainer("")
        End If

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function PackJSContainer(ByVal sContainterIdx As String) As String
        Dim gphIdx As Integer = 0
        If sContainterIdx > "" Then
            gphIdx = CInt(sContainterIdx)
        End If

        Dim sJscript2 As String

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container" & sContainterIdx & "').highcharts({" & vbCr

        'jam 2025/02/23 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "polar: true," & vbCr
        sJscript2 = sJscript2 & "type: 'line'," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        Dim sTitle As String
        If Session("TableTitle") > "" And Session("TableTitle") <> "Title" Then
            sTitle = Session("TableTitle")
        Else
            If Session("TableTitle") = "" Then
                sTitle = " " 'JAM 2015-02-03 Adding for empty data
            Else
                sTitle = LTrim$(RTrim$(Session("oVARN0")(1)))
            End If
        End If

        sJscript2 = sJscript2 & "text:       '" & sTitle & "'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Session("NumYearsSel") = 1 And gphIdx = 0 Then
            sJscript2 = sJscript2 & "subtitle: {" & vbCr
            sJscript2 = sJscript2 & "text:       ""Year = " & CStr(Session("FirstYear") + Session("YearsSel")(0) - 1) & """," & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize: '16px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
            sJscript2 = sJscript2 & "x: -20 //center" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "tickmarkPlacement: 'on'," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "categories: ["
        Dim i As Integer
        If Not Session("gThirdDimmOk") Then
            For i = 1 To Session("ItemsbySet")
                If i > 1 Then
                    sJscript2 = sJscript2 & ","
                End If
                sJscript2 = sJscript2 & "'" & getCommonPointName(Session("NewIndex")(i - 1), Session("NewIndex")(i - 1)) & "'"
            Next
        Else
            For i = 1 To Session("ItemsbySet")
                If i > 1 Then
                    sJscript2 = sJscript2 & ","
                End If
                sJscript2 = sJscript2 & "'" & Session("CGroupLabels")(i) & "'"
            Next
        End If

        sJscript2 = sJscript2 & "]," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize: '12px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "}"
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "gridLineInterpolation:'polygon'," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "min: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "legend: {" & vbCr
        If Session("SplitGraph") = 1 And Session("NumYearsSel") = 1 Then
            sJscript2 = sJscript2 & "enabled:     false" & vbCr
        Else
            sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
            sJscript2 = sJscript2 & "align:      'center'," & vbCr
            sJscript2 = sJscript2 & "verticalAlign:  'bottom'," & vbCr
            sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
            'sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "series: [" & vbCr

        If gphIdx > 0 Then
            Dim yrStart As Integer, gphStart As Integer
            If Session("NumYearsSel") = 1 Then
                yrStart = 1
                gphStart = gphIdx
            ElseIf Session("SplitGraph") = 1 Then
                yrStart = gphIdx
                gphStart = 1
            Else
                Select Case gphIdx
                    Case 1
                        yrStart = 1
                        gphStart = 1
                    Case 2
                        yrStart = 1
                        gphStart = 2
                    Case 3
                        yrStart = 2
                        gphStart = 1
                    Case 4
                        yrStart = 2
                        gphStart = 2
                End Select
            End If

            Dim bFirst As Boolean = True

            For y = yrStart To Session("NumYearsSel") Step 4
                For i = gphStart To Session("SplitGraph") Step 4
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript2 = sJscript2 & "," & vbCr
                    End If
                    sJscript2 = sJscript2 & PlotColToJSSeries(Session("YearsSel")(y - 1), i, y)
                Next i
            Next y
        Else
            For y = 1 To Session("NumYearsSel")
                For i = 1 To Session("SplitGraph")
                    If i > 1 Or y > 1 Then
                        sJscript2 = sJscript2 & "," & vbCr
                    End If
                    sJscript2 = sJscript2 & PlotColToJSSeries(Session("YearsSel")(y - 1), i, y)
                Next i
            Next y
        End If


        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
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

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 50;" & vbCr

        If sContainterIdx > "" Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.49;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.49;" & vbCr
        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        PackJSContainer = sJscript2

    End Function


    Private Function PlotColToJSSeries(ByVal i As Integer, ByVal GphIdx As Integer, ByVal YrIdx As Integer) As String
        Dim vPointName As String = ""
        Dim a As Decimal
        Dim value As String
        Dim vSetLabel As String
        Dim pos1, pos2 As Integer


        vSetLabel = ""
        Dim j As Integer, k As Integer
        If GphIdx = 1 And Session("SplitGraph") > 1 Then
            k = 2
        Else
            k = 1
        End If

        Dim sColor As String = ""
        If Session("bMonochrome") = True Then
            sColor = "Black"
        ElseIf GphIdx < 9 Then
            Dim idx As Integer
            If Session("NumYearsSel") = 1 Then
                idx = GphIdx
            ElseIf Session("SplitGraph") = 1 Then
                idx = YrIdx
            Else
                idx = (YrIdx - 1) * Session("SplitGraph") + GphIdx
            End If

            Select Case idx
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 5 'blue violet
                    sColor = "RGB(138,43,226)"
                Case 6 'aqua 
                    sColor = "RGB(0,255,255)"
                Case 7 'deep pink
                    sColor = "RGB(255,20,147)"
                Case 8 'medium spring green
                    sColor = "RGB(0,250,154)"
            End Select
        End If

        Dim sJscript As String = ""
        sJscript = sJscript & "{pointPlacement: 'on', color: '" & sColor & "'," & vbCr

        If Not Session("gThirdDimmOk") Then 'JAM 2014-12-07 Analyzing filling points
            Dim sSubLeg As String = ""
            sSubLeg = getSubsetLegend(i, GphIdx - 1, k - 1)
            sJscript = sJscript & "name: '" & sSubLeg & "', data: [" & vbCr

            For j = 1 To Session("ItemsbySet")
                If IsNumeric(Session("PLOT")(i, Session("NewIndex")((GphIdx - 1) * Session("ItemsbySet") + j - 1))) Then

                    a = Session("PLOT")(i, Session("NewIndex")((GphIdx - 1) * Session("ItemsbySet") + j - 1))
                    vPointName = getCommonPointName(Session("NewIndex")((GphIdx - 1) * Session("ItemsbySet") + j - 1), Session("NewIndex")((k - 1) * Session("ItemsbySet") + j - 1))
                    If vPointName = "" Then
                        vPointName = j.ToString
                    End If

                    If j > 1 Then
                        sJscript = sJscript & "," & vbCr
                    End If

                    sJscript = sJscript & "{name : """ & vPointName &
                                                   """, y:" & CStr(Session("PLOT")(i, Session("NewIndex")((GphIdx - 1) * Session("ItemsbySet") + j - 1))) & "}" & vbCr

                End If
            Next
        Else
            'JAM 2015-11-07 For only scenario labels in subsets
            vSetLabel = Session("SetLegends")(GphIdx).ToString

            If Session("SelScenarioLabel") Then
                pos1 = InStr(vSetLabel, "[")
                pos2 = InStr(vSetLabel, "]")
                If pos1 > 0 And pos2 > 0 Then
                    vSetLabel = Mid(vSetLabel, pos1 + 1, pos2 - pos1 - 1)
                End If

            End If



            'JAM 2015-11-07 Adding Years in Set Label
            If Not Session("MultipleYears") Then
                If vSetLabel.Substring(0, 2) = ",[" Then
                    'Session("SetLegends")(GphIdx) = Replace(Replace(Session("SetLegends")(GphIdx).ToString, ",[", ""), "]", "")

                    sJscript = sJscript & "name: '" & Replace(Replace(vSetLabel, ",[", ""), "]", "") & "', data: [" & vbCr
                Else
                    sJscript = sJscript & "name: '" & vSetLabel & "', data: [" & vbCr


                End If
            Else
                If vSetLabel.Substring(0, 2) = ",[" Then
                    'Session("SetLegends")(GphIdx) = Replace(Replace(Session("SetLegends")(GphIdx).ToString, ",[", ""), "]", "")

                    sJscript = sJscript & "name: '" & Replace(Replace(vSetLabel, ",[", ""), "]", "") & CStr(Session("FirstYear") + i - 1) & "', data: [" & vbCr
                Else
                    sJscript = sJscript & "name: '" & vSetLabel & CStr(Session("FirstYear") + i - 1) & "', data: [" & vbCr


                End If


            End If
            'sJscript = sJscript & "name: '" & Session("SetLegends")(GphIdx) & "', data: [" & vbCr

            For j = 1 To Session("ItemsbySet")
                If IsNumeric(Session("IndexbyGroup")(j, GphIdx, i)) Then

                    vPointName = Session("CGroupLabels")(j)

                    If vPointName = "" Then
                        vPointName = j.ToString
                    End If
                    value = CStr(Session("IndexbyGroup")(j, GphIdx, i))

                    If j > 1 Then
                        sJscript = sJscript & "," & vbCr
                    End If
                    sJscript = sJscript & "{name: """ & vPointName &
                                                   """, y:" & CStr(Session("IndexbyGroup")(j, GphIdx, i)) & "}" & vbCr


                End If
            Next
        End If

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToJSSeries = sJscript
    End Function


    Private Sub SaveWorkingSce()
        Session("SaveWorkingSce") = (Dir(Session("ScenarioFileDir") & "Working.Sce") <> "")
        If Session("SaveWorkingSce") Then
            FileCopyCheck(Session("ScenarioFileDir") & "Working.Sce", Session("ScenarioFileDir") & "Working.Sce.bkp")
        End If
    End Sub
    Private Function GraphScaleLabel(ByRef V As String, ByRef Rescaler As Object) As String
        Dim GSLabel As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Couldn't resolve default property of object Rescaler. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Rescaler = 0 Then
            GSLabel = ""
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Rescaler. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            GSLabel = "10^" & LTrim(Str(Rescaler))
        End If

        GraphScaleLabel = GSLabel

    End Function
    Private Sub ShowRelevantGraph()

        Session("PE_CommonMenu") = "Display"

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Enabled = True
        'RadialCtrl.Visible = True

    End Sub
    Public Sub mnuAdvance_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Session("PYear") = Session("PYear") + 5
        If Session("PYear") < 1 Then Session("PYear") = 1

        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1

        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        Session("PE_CommonMenu") = "Display"

        Session("YearsSel")(0) = Session("PYear")

        'JAM 2009-07-05
        'jrs 2012/05/22 Call ShowRightControl()
        'RadialCtrl.Visible = True
        Dim TargetUrl As String
        TargetUrl = "frm_BaseRadial.aspx"
        If Not IsNothing(Request.QueryString("ListNames")) Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(Request.QueryString("ListNames"))
            TargetUrl = TargetUrl & "&HistFor=" & Request.QueryString("HistFor")
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            TargetUrl = TargetUrl & "&Dim1=" & PrepareForURL(Request.QueryString("Dim1"))
            TargetUrl = TargetUrl & "&Dim2=" & PrepareForURL(Request.QueryString("Dim2"))
            TargetUrl = TargetUrl & "&Dim3=" & PrepareForURL(Request.QueryString("Dim3"))
            TargetUrl = TargetUrl & "&File=" & PrepareForURL(Request.QueryString("File"))
            TargetUrl = TargetUrl & "&Year=" & Session("FirstYear") + Session("PYear") - 1
            If Not IsNothing(Request.QueryString("Scale")) Then
                TargetUrl = TargetUrl & "&Scale=" & Request.QueryString("Scale")
            End If
            'jrs 2021/01/19 moved up here, it was outside the end if
            Response.Redirect(TargetUrl)
        Else 'jrs 2021/01/19
            PackJS()
        End If
    End Sub
    Public Sub mnuCommon_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)

        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = eventSender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = eventArgs

        Session("PE_CommonMenu") = "Common"


        If objEA.Item.Caption = "Common" Then
            objSender.Groups("Scaling").Items("Common").IconImage = "checkbox.gif"
            objSender.Groups("Scaling").Items("Scale").IconImage = "blank.gif"

        Else
            objSender.Groups("Scaling").Items("Scale").IconImage = "checkbox.gif"
            objSender.Groups("Scaling").Items("Common").IconImage = "blank.gif"
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub

    Public Sub mnuContinue_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim i, k As Integer
        For i = 1 To Session("COUNTE")
            Session("VARN")(0, i) = Session("oVARN0")(i)
            Session("FDNG1")(i) = Session("GR1N")(i)
            Session("FDNG2")(i) = Session("GR2N")(i)
            Session("FDNG3")(i) = Session("GR3N")(i)
        Next i

        'Flag
        'Call ResetScaling()
        Session("CalcScaling") = False

        ReDim Session("NewIndex")(pCOUNTE)


        For k = 1 To pCOUNTE
            Session("NewIndex")(k - 1) = k
        Next k

        'JAM 2014-09-27
        'Session("Continuous") = True
        Session.Remove("Continuous")
        Session("CommDimm") = False
        Session("MultipleGraphs") = False
        Session("mnuCommScalChecked") = False

        Session.Remove("CalcScaling")
        Session.Remove("SplitGraph")
        Session.Remove("ItemsbySet")
        Session.Remove("NumYearsSel")
        Session.Remove("MultipleYears")
        Session.Remove("Continuous")
        Session.Remove("ChangeSubLabels")
        Session.Remove("MaxData")
        Session.Remove("Angle")
        Session.Remove("CommDimm")
        Session.Remove("MultipleGraphs")
        Session.Remove("bMonochrome")
        Session.Remove("MultipleGraphs")
        Session.Remove("YearsSel")
        Session.Remove("LocalPlot")
        Session.Remove("YearsItemsCount")
        Session.Remove("mnuCommDimChecked")
        Session.Remove("PYear")
        Session.Remove("NewIndex")
        Session.Remove("mnuOnScalingChecked")
        Session.Remove("SubSetLabel")
        Session.Remove("mnuCommScalChecked")
        Session.Remove("SetPatt")
        Session.Remove("mnuOffScalingCheked")
        Session.Remove("mnuOffScalingEnabled")
        Session.Remove("Continuous")
        Session.Remove("ContinuousChecked")
        Session.Remove("mnuDiffGraphsChecked")
        Session.Remove("mnuVarpGraphEnabled")
        Session.Remove("MaxRadious")
        'JAM 2014-08-31
        Session.Remove("SelScenarioLabel")

        Session.Remove("SourceRadial")
        'jrs 2012/01/26
        Session.Remove("IsChangedNum")

        'JAM 2014-12-24 Removing plus two dimmensions arrays and variables
        Session.Remove("VarsCount")
        Session.Remove("Dim1Count")
        Session.Remove("Dim2Count")
        Session.Remove("Dim3Count")
        Session.Remove("ScenCount")
        Session.Remove("ScenarioArray")
        Session.Remove("StrVARN")
        Session.Remove("StrGR1N")
        Session.Remove("StrGR2N")
        Session.Remove("StrGR3N")
        Session.Remove("StrSce")
        Session.Remove("SetLegends")
        Session.Remove("StrSceName")
        'Session.Remove("NewIndex")
        Session.Remove("CGroupLabels")
        Session.Remove("IndexbyGroup")
        'JAM 2015-02-03 Removing Changing SubSet Labels
        Session.Remove("LabelChanged")


        'jrs 2011/10/19
        If Session("BaseRadialParent") = Nothing Then
            'jrs 2012/12/14
            If Request.QueryString("ListNames") Is Nothing Then
                Session("BaseRadialParent") = "frm_PackagedDisplaysFlex.aspx"
            Else
                Dim sLst As String
                sLst = Request.QueryString("ListNames")
                sLst = Replace(sLst, """,""", ",,")
                sLst = Mid(sLst, 2, Len(sLst) - 2)
                'jrs 2011/11/03 
                'Mark found that the list Foreign Reserves as Percent of GDP didn't work
                'there were 2 weird characters after the word Reserves, remove them
                sLst = Replace(sLst, Chr(127), " ")

                Session("BaseRadialParent") = "frm_PackagedDisplaysFlex.aspx?ListNames=" & sLst
            End If
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, Session("BaseRadialParent"))
        'Support.FormManager.Close(Me)

    End Sub

    Public Sub mnuHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        LoadMessage("Double-click or right-click on the graph to bring up various formatting/display options.", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Public Sub mnuRegress_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("PYear") = Session("PYear") - 5
        If Session("PYear") < 1 Then Session("PYear") = 1

        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1

        Session("YearsSel")(0) = Session("PYear")

        'jrs 2012/05/22
        Dim TargetUrl As String
        TargetUrl = "frm_BaseRadial.aspx"
        If Not IsNothing(Request.QueryString("ListNames")) Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(Request.QueryString("ListNames"))
            TargetUrl = TargetUrl & "&HistFor=" & Request.QueryString("HistFor")
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            TargetUrl = TargetUrl & "&Dim1=" & PrepareForURL(Request.QueryString("Dim1"))
            TargetUrl = TargetUrl & "&Dim2=" & PrepareForURL(Request.QueryString("Dim2"))
            TargetUrl = TargetUrl & "&Dim3=" & PrepareForURL(Request.QueryString("Dim3"))
            TargetUrl = TargetUrl & "&File=" & PrepareForURL(Request.QueryString("File"))
            TargetUrl = TargetUrl & "&Year=" & Session("FirstYear") + Session("PYear") - 1
            If Not IsNothing(Request.QueryString("Scale")) Then
                TargetUrl = TargetUrl & "&Scale=" & Request.QueryString("Scale")
            End If
            'jrs 2021/01/19 moved up here, it was outside the end if
            Response.Redirect(TargetUrl)
        Else 'jrs 2021/01/19
            PackJS()
        End If

    End Sub

    Public Sub mnuStatistics_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Session("Statistics_Type") = 0
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_statistics.aspx")

    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        Select Case objEA.Item.Caption

            Case "Save", "Print"
            Case "Common", "Scale"
                mnuCommon_Click(sender, e)
            Case "Advance"
                mnuAdvance_Click(sender, e)
            Case "Regress"
                mnuRegress_Click(sender, e)
            Case "Go To Year" 'jrs 2012/05/02
                mnuGoToYear_Click()
            Case "Set Title, Display Interval, or Year"
                'jrs 2012/12/14 Session("DisplayControlParent") = "frm_BaseRadial.aspx"
                Session("DisplayControlParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
            Case "Display/Run Horizon" 'jrs 2012/05/02
                mnuDisplayRunHorizon_Click(sender, e)
            Case "Use Alias for Group Names" 'jrs 2011/07/04
                mnuAlias_Click()
            Case "Set SubSet Labels"
                mnuSSetLabels_Click()
            Case "Format Legend Labels"
                'jrs 2012/12/14 Session("GraphLabelsParent") = "frm_BaseRadial.aspx"
                Session("GraphLabelsParent") = Request.Url.AbsoluteUri
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_GraphLabels.aspx")
            Case "Colors On"

                mnuColorsOn_Click()
                If Session("mnuColorsOnChecked") Then
                    objSender.Groups("Display Format").Items("Colors On").IconImage = "checkbox.gif"
                    objSender.Groups("Display Format").Items("Colors Off").IconImage = "blank.gif"
                Else
                    objSender.Groups("Display Format").Items("Colors Off").IconImage = "blank.gif"
                End If
            Case "Colors Off"
                mnuColorsOff_Click()
                If Not Session("mnuColorsOnChecked") Then
                    objSender.Groups("Display Format").Items("Colors On").IconImage = "blank.gif"
                    objSender.Groups("Display Format").Items("Colors Off").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Display Format").Items("Colors Off").IconImage = "blank.gif"
                End If
            Case "Use Common Dimensions for Labels"
                mnuCommDim_Click()
                If Session("CommDimm") Then
                    objSender.Groups("Display Format").Items("Use Common Dimensions for Labels").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Display Format").Items("Use Common Dimensions for Labels").IconImage = "blank.gif"
                End If
            Case "Format Legend Labels First Dimension Only"
                mnuFormLeg1stDim_Click()
            Case "Format Legend Labels Second Dimension Only"
                mnuFormLeg2ndDim_Click()
            Case "Format Legend Labels Third Dimension Only"
                mnuFormLeg3rdDim_Click()
            Case "Format Legend Labels Dimensions Only"
                mnuFormLegDims_Click()
            Case "Format Legend Labels No Dimensions"
                mnuFormLegNoDim_Click()
            Case "Format Legend Labels Scenario Names"
                mnuFormLegSce_Click()
            Case "Format Legend Labels Reset"
                mnuFormLegReset_Click()
            Case "On"

                mnuOnScaling_Click()
                If Session("mnuOnScalingChecked") Then
                    objSender.Groups("Scaling").Items("On").IconImage = "checkbox.gif"
                    objSender.Groups("Scaling").Items("Off").IconImage = "blank.gif"
                Else
                    objSender.Groups("Scaling").Items("Off").IconImage = "blank.gif"
                End If
            Case "Off"
                mnuOffScaling_Click()
                If Not Session("mnuOnScalingChecked") Then
                    objSender.Groups("Scaling").Items("On").IconImage = "blank.gif"
                    objSender.Groups("Scaling").Items("Off").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Scaling").Items("Off").IconImage = "blank.gif"
                End If

            Case "Definition"
                mnuDefinitionScaling_Click()
            Case "Common Scaling"
                mnuCommScal_Click()

                'JAM 2009-07-18 Enabling if We're in Multiple Graphs mode
                If Session("MultipleGraphs") Then
                    'JAM 2009-07-15 CheckBox Review
                    If Session("mnuCommScalChecked") Then
                        objSender.Groups("Scaling").Items("Common Scaling").IconImage = "checkbox.gif"
                    Else
                        objSender.Groups("Scaling").Items("Common Scaling").IconImage = "blank.gif"
                    End If
                End If

                    'If Session("mnuCommScalChecked") Then
                    '    objSender.Groups("Scaling").Items("Common Scaling").IconImage = "checkbox.gif"
                    'Else
                    '    objSender.Groups("Scaling").Items("Common Scaling").IconImage = "blank.gif"
                    'End If
            Case "Continuous Mode"
                mnuContinuous_Click()
            Case "Variables per Graph"
                mnuVarpGraph_Click()
            Case "Display Multiple Years"
                mnuDispMulYears_Click()
            Case "Display in Different Graphs"
                If Not Session("MultipleGraphs") Then
                    objSender.Groups("Multiple Graphs").Items("Display in Different Graphs").IconImage = "checkbox.gif"
                Else
                    objSender.Groups("Multiple Graphs").Items("Display in Different Graphs").IconImage = "blank.gif"
                End If
                mnuDiffGraphs_Click()
                'JAM 2009-07-18 Enabling if We're in Multiple Graphs mode
                If Session("MultipleGraphs") Then
                    objSender.Groups("Scaling").Items("Common Scaling").Enabled = True
                Else
                    objSender.Groups("Scaling").Items("Common Scaling").IconImage = "blank.gif"
                    objSender.Groups("Scaling").Items("Common Scaling").Enabled = False

                End If

            Case "Continue"
                mnuContinue_Click(Me, New EventArgs)
        End Select
    End Sub
    Private Sub mnuFormLeg2ndDim_Click()

        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR2N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR2N")(I)
                Session("FDNG1")(I) = ""
                Session("FDNG2")(I) = ""
                Session("FDNG3")(I) = ""
            End If
        Next I

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False
        'JAM 2015-04-19
        If Session("gThirdDimmOk") Then

            PlusThirdDimPattern() 'JAM 2015-04-19
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub
    Private Sub mnuFormLeg1stDim_Click()

        Dim I As Integer

        Dim a As String
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR1N")(I)) > "" Then
                a = Session("GR1N")(I)

                Session("VARN")(0, I) = Session("GR1N")(I)
                Session("FDNG1")(I) = ""
                Session("FDNG2")(I) = ""
                Session("FDNG3")(I) = ""
            End If
        Next I

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False

        'JAM 2015-04-19
        If Session("gThirdDimmOk") Then

            PlusThirdDimPattern() 'JAM 2015-04-19
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True
    End Sub
    Private Sub mnuFormLeg3rdDim_Click()

        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR3N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR3N")(I)
                Session("FDNG1")(I) = ""
                Session("FDNG2")(I) = ""
                Session("FDNG3")(I) = ""
            End If
        Next I

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False

        'JAM 2015-04-19
        If Session("gThirdDimmOk") Then

            PlusThirdDimPattern() 'JAM 2015-04-19
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub
    Private Sub mnuFormLegDims_Click()

        Dim I As Integer
        For I = 1 To Session("COUNTE")
            If Trim(Session("GR1N")(I)) > "" Then
                Session("VARN")(0, I) = Session("GR1N")(I)
                Session("FDNG1")(I) = Session("GR2N")(I)
                Session("FDNG2")(I) = Session("GR3N")(I)
                Session("FDNG3")(I) = ""
            End If
        Next I

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False

        'JAM 2015-04-19
        If Session("gThirdDimmOk") Then

            PlusThirdDimPattern() 'JAM 2015-04-19
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub
    Private Sub mnuFormLegNoDim_Click()

        Dim I As Integer
        For I = 1 To Session("COUNTE")
            Session("FDNG1")(I) = ""
            Session("FDNG2")(I) = ""
            Session("FDNG3")(I) = ""
        Next I

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False

        'JAM 2015-04-19
        If Session("gThirdDimmOk") Then

            PlusThirdDimPattern() 'JAM 2015-04-19
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub
    Private Sub mnuFormLegSce_Click()

        Dim i As Integer
        Dim pos1 As Integer, pos2 As Integer
        For i = 1 To Session("COUNTE")
            pos1 = InStr(Session("oVARN0")(i), "[")
            pos2 = InStr(Session("oVARN0")(i), "]")
            If pos1 > 0 And pos2 > 0 Then
                Session("VARN")(0, i) = Mid(Session("oVARN0")(i), pos1 + 1, pos2 - pos1 - 1)
            End If

            Session("FDNG1")(i) = ""
            Session("FDNG2")(i) = ""
            Session("FDNG3")(i) = ""
        Next i

        'JAM 2014-08-31
        Session("SelScenarioLabel") = True

        'JAM 2015-04-19
        If Session("gThirdDimmOk") Then

            PlusThirdDimPattern() 'JAM 2015-04-19
        End If

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub
    Private Sub mnuFormLegReset_Click()

        Dim i As Integer
        Dim a As String


        For i = 1 To Session("COUNTE")
            ''a = Session("oVARN0")(i)

            'JAM 2015-03-31
            If Session("gThirdDimmOk") And Session("LabelChanged") Then
                a = Session("gthirdVARN0")(i)
                Session("VARN")(0, i) = Session("gthirdVARN0")(i)
            Else
                Session("VARN")(0, i) = Session("oVARN0")(i)
            End If
            'Session("VARN")(0, i) = Session("oVARN0")(i)
            Session("FDNG1")(i) = Session("GR1N")(i)
            Session("FDNG2")(i) = Session("GR2N")(i)
            Session("FDNG3")(i) = Session("GR3N")(i)
        Next i

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False

        'JAM 2015-03-31 Resetting labels with Third Dimmension
        If Session("gThirdDimmOk") Then

            Session("LabelChanged") = False
            PlusThirdDimPattern() 'JAM 2015-04-19
        End If



        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub

    Protected Sub cmdDraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDraw.Click
        Dim j As Integer
        Dim i As Integer


        lstYears.Visible = False
        cmdDraw.Visible = False

        lstYears.Enabled = False
        cmdDraw.Enabled = False



        ReDim Preserve Session("YearsSel")(lstYears.Items.Count)

        j = 0
        For i = 0 To lstYears.Items.Count - 1

            'If lstYears.GetSelectedIndices(i) Then
            If lstYears.Items(i).Selected Then
                Session("YearsSel")(j) = i + 1 'lstYears.List(i)
                j = j + 1
            End If
        Next i
        Session("YearsItemsCount") = j
        'jrs 2011/10/14
        Session("NumYearsSel") = j
        Session("MultipleYears") = True

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

        'If j <> 0 Then
        '    Session("NumYearsSel") = j
        '    If j = 1 Then
        '        Session("MultipleYears") = True
        '        If Not Session("MultipleGraphs") Then
        '            RadialCtrl.PeData.Subsets = Session("SplitGraph")
        '            'Else
        '            '    For i = 0 To Session("SplitGraph") - 1
        '            '        If i < 4 Then
        '            '            grp_MulPolar(i).Subsets = 1
        '            '        Else
        '            '            Exit For
        '            '        End If
        '            '    Next i
        '        End If

        '        RadialCtrl.Visible = True
        '        'Flag 
        '        'mnuAdvance.Enabled = True
        '        'mnuRegress.Enabled = True

        '    Else

        '        If Not Session("MultipleGraphs") Then
        '            RadialCtrl.PeData.Subsets = Session("SplitGraph") * Session("NumYearsSel")
        '            RadialCtrl.PeData.Points = (Session("ItemsbySet") + 1)
        '            For i = 0 To Session("SplitGraph") * Session("NumYearsSel") - 1

        '                RadialCtrl.PePlot.SubsetLineTypes(i) = LineType.MediumSolid
        '                'For ii = 0 To pCOUNTE - 1
        '                '    RadialCtrl.PePlot.PointTypes(0, ii) = PointType.DotSolid
        '                'Next ii

        '                RadialCtrl.PeLegend.SubsetsToLegend(i) = -1
        '            Next i
        '            'Else
        '            '    For i = 0 To Session("SplitGraph") * Session("NumYearsSel") - 1
        '            '        If i < 4 Then
        '            '            grp_MulPolar(i).Subsets = 1
        '            '            grp_MulPolar(i).Points = (Session("ItemsbySet") + 1) '* Session("SplitGraph") * Session("NumYearsSel")
        '            '            grp_MulPolar(i).SubsetLineTypes(0) = PELT_MEDIUMSOLID
        '            '            grp_MulPolar(i).SubsetPointTypes(0) = PEPT_DOTSOLID
        '            '            grp_MulPolar(i).SubsetsToLegend(0) = -1
        '            '        Else
        '            '            Exit For
        '            '        End If
        '            '    Next i

        '        End If


        '        Session("MultipleYears") = True
        '        RadialCtrl.Visible = True

        '        'Flag
        '        'mnuAdvance.Enabled = False
        '        'mnuRegress.Enabled = False
        '    End If
        'End If

    End Sub


    Private Sub mnuColorsOff_Click()

        Session("bMonochrome") = True
        Session("mnuColorsOffChecked") = True
        Session("mnuColorsOnChecked") = False


        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True
    End Sub

    Private Sub mnuColorsOn_Click()
        Session("bMonochrome") = False
        Session("mnuColorsOffChecked") = False
        Session("mnuColorsOnChecked") = True


        'RadialCtrl.PeColor.ViewingStyle = ViewingStyle.Color

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True


    End Sub

    Private Sub mnuCommDim_Click()
        Dim k, i As Integer
        Dim CommonLegend As String
        Dim m As Integer


        If Session("SplitGraph") > 1 Or Session("NumYearsSel") > 1 Then


            If Not Session("mnuCommDimChecked") Then
                'If Not Session("MultipleGraphs") Then

                '    'RadialCtrl.PePlot.Option.AllowDataLabels = AllowDataLabels.PointLabels

                '    RadialCtrl.PeGrid.Option.ShowXAxis = ShowAxis.All
                '    RadialCtrl.PeGrid.LineControl = GridLineControl.Both

                'Else
                '    For i = 0 To 3
                '        If i = 0 Then
                '            grp_MulPolar1.PeGrid.Option.ShowXAxis = ShowAxis.All
                '            grp_MulPolar1.PeGrid.LineControl = GridLineControl.Both
                '        Else
                '            If i = 1 Then
                '                grp_MulPolar2.PeGrid.Option.ShowXAxis = ShowAxis.All
                '                grp_MulPolar2.PeGrid.LineControl = GridLineControl.Both
                '            Else
                '                If i = 2 Then
                '                    grp_MulPolar3.PeGrid.Option.ShowXAxis = ShowAxis.All
                '                    grp_MulPolar3.PeGrid.LineControl = GridLineControl.Both
                '                Else
                '                    grp_MulPolar4.PeGrid.Option.ShowXAxis = ShowAxis.All
                '                    grp_MulPolar4.PeGrid.LineControl = GridLineControl.Both
                '                End If
                '            End If
                '        End If
                '    Next i
                '    'RadialCtrl.Visible = True
                'End If

                ''JAM 2009-07-05
                ''Call ShowRightControl()

                'If Not Session("MultipleGraphs") Then
                '    RadialCtrl.PePlot.Option.GraphDataLabels = False
                'Else
                '    grp_MulPolar1.PePlot.Option.GraphDataLabels = False
                '    grp_MulPolar2.PePlot.Option.GraphDataLabels = False
                '    grp_MulPolar3.PePlot.Option.GraphDataLabels = False
                '    grp_MulPolar4.PePlot.Option.GraphDataLabels = False
                'End If



                'Dim yy As String
                'For k = 1 To Session("ItemsbySet")
                '    BuildCommonLegend(k)

                '    If Not Session("MultipleGraphs") Then
                '        If InStr(Left(Session("CommonLegend"), 18), "[") <> 0 And InStr(Left(Session("CommonLegend"), 18), "]") <> 0 Then
                '            yy = Left$(Session("CommonLegend"), 18) '+ ")"
                '            RadialCtrl.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), 18) '+ ")"
                '        Else
                '            RadialCtrl.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), InStr(Session("CommonLegend"), "[") - 1)
                '        End If
                '    Else
                '        For m = 0 To Session("SplitGraph") * Session("NumYearsSel") - 1
                '            If m < 4 Then
                '                If m = 0 Then
                '                    If InStr(Left(Session("CommonLegend"), 18), "[") <> 0 And InStr(Left(Session("CommonLegend"), 18), "]") <> 0 Then
                '                        grp_MulPolar1.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), 18) '+ ")"
                '                    Else
                '                        grp_MulPolar1.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), InStr(Session("CommonLegend"), "[") - 1)
                '                    End If
                '                Else
                '                    If m = 1 Then
                '                        If InStr(Left(Session("CommonLegend"), 18), "[") <> 0 And InStr(Left(Session("CommonLegend"), 18), "]") <> 0 Then
                '                            grp_MulPolar2.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), 18) '+ ")"
                '                        Else
                '                            grp_MulPolar2.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), InStr(Session("CommonLegend"), "[") - 1)
                '                        End If
                '                    Else
                '                        If m = 2 Then
                '                            If InStr(Left(Session("CommonLegend"), 18), "[") <> 0 And InStr(Left(Session("CommonLegend"), 18), "]") <> 0 Then
                '                                grp_MulPolar3.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), 18) '+ ")"
                '                            Else
                '                                grp_MulPolar3.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), InStr(Session("CommonLegend"), "[") - 1)
                '                            End If
                '                        Else
                '                            If InStr(Left(Session("CommonLegend"), 18), "[") <> 0 And InStr(Left(Session("CommonLegend"), 18), "]") <> 0 Then
                '                                grp_MulPolar4.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), 18) '+ ")"
                '                            Else
                '                                grp_MulPolar4.PeString.PointLabels(k - 1) = Left$(Session("CommonLegend"), InStr(Session("CommonLegend"), "[") - 1)
                '                            End If
                '                        End If
                '                    End If
                '                End If
                '            Else
                '                Exit For
                '            End If
                '        Next m
                '    End If

                'Next k


                'If Not Session("MultipleGraphs") Then

                '    RadialCtrl.PeGrid.LineControl = GridLineControl.Both


                '    'RadialCtrl.RLabels = True

                '    RadialCtrl.PeGrid.Configure.ManualXAxisTicknLine = True
                '    RadialCtrl.PeGrid.Configure.ManualXAxisLine = Session("Angle")
                '    RadialCtrl.PeGrid.Configure.ManualXAxisTick = 2


                '    RadialCtrl.PeUserInterface.HotSpot.Data = True
                'Else
                '    For m = 0 To Session("SplitGraph") * Session("NumYearsSel") - 1
                '        If m < 4 Then
                '            If m = 1 Then
                '                grp_MulPolar1.PeGrid.LineControl = GridLineControl.Both
                '                'RadialCtrl.RLabels = True
                '                grp_MulPolar1.PeGrid.Configure.ManualXAxisTicknLine = True
                '                grp_MulPolar1.PeGrid.Configure.ManualXAxisLine = Session("Angle")
                '                grp_MulPolar1.PeGrid.Configure.ManualXAxisTick = 2
                '                grp_MulPolar1.PeUserInterface.HotSpot.Data = True

                '            Else
                '                If m = 2 Then
                '                    grp_MulPolar2.PeGrid.LineControl = GridLineControl.Both
                '                    'RadialCtrl.RLabels = True
                '                    grp_MulPolar2.PeGrid.Configure.ManualXAxisTicknLine = True
                '                    grp_MulPolar2.PeGrid.Configure.ManualXAxisLine = Session("Angle")
                '                    grp_MulPolar2.PeGrid.Configure.ManualXAxisTick = 2
                '                    grp_MulPolar2.PeUserInterface.HotSpot.Data = True

                '                Else
                '                    If m = 3 Then
                '                        grp_MulPolar3.PeGrid.LineControl = GridLineControl.Both
                '                        'RadialCtrl.RLabels = True
                '                        grp_MulPolar3.PeGrid.Configure.ManualXAxisTicknLine = True
                '                        grp_MulPolar3.PeGrid.Configure.ManualXAxisLine = Session("Angle")
                '                        grp_MulPolar3.PeGrid.Configure.ManualXAxisTick = 2
                '                        grp_MulPolar3.PeUserInterface.HotSpot.Data = True

                '                    Else
                '                        grp_MulPolar4.PeGrid.LineControl = GridLineControl.Both
                '                        'RadialCtrl.RLabels = True
                '                        grp_MulPolar4.PeGrid.Configure.ManualXAxisTicknLine = True
                '                        grp_MulPolar4.PeGrid.Configure.ManualXAxisLine = Session("Angle")
                '                        grp_MulPolar4.PeGrid.Configure.ManualXAxisTick = 2
                '                        grp_MulPolar4.PeUserInterface.HotSpot.Data = True

                '                    End If
                '                End If
                '            End If
                '        Else
                '            Exit For
                '        End If


                '    Next m

                'End If

                Session("mnuCommDimChecked") = True
                Session("CommDimm") = True
                'Call LabelData()

            Else
                Session("mnuCommDimChecked") = False
                Session("CommDimm") = False
            End If
        End If

        '        If Not Session("MultipleGraphs") Then

        '            RadialCtrl.PePlot.Option.GraphDataLabels = True
        '            RadialCtrl.PePlot.Option.AllowDataLabels = AllowDataLabels.DataPointLabels

        '            RadialCtrl.PeGrid.Option.ShowXAxis = ShowAxis.Empty
        '            RadialCtrl.PeGrid.LineControl = GridLineControl.XAxis
        '            Session("mnuCommDimChecked") = False
        '            mnuFormLegReset_Click()
        '            Session("CommDimm") = False

        '            'RadialCtrl.RLabels = False

        '            RadialCtrl.PeUserInterface.HotSpot.Data = False

        '            'JAM 2009-07-05
        '            'RadialCtrl.Visible = True

        '        Else
        '            For m = 0 To Session("SplitGraph") * Session("NumYearsSel") - 1
        '                If m < 4 Then
        '                    If m = 1 Then
        '                        grp_MulPolar1.PePlot.Option.GraphDataLabels = True
        '                        grp_MulPolar1.PePlot.Option.AllowDataLabels = AllowDataLabels.DataPointLabels
        '                        grp_MulPolar1.PeGrid.Option.ShowXAxis = ShowAxis.Empty
        '                        grp_MulPolar1.PeGrid.LineControl = GridLineControl.XAxis
        '                        Session("mnuCommDimChecked") = False
        '                        mnuFormLegReset_Click()
        '                        Session("CommDimm") = False
        '                        'RadialCtrl.RLabels = False
        '                        grp_MulPolar1.PeUserInterface.HotSpot.Data = False

        '                    Else
        '                        If m = 2 Then
        '                            grp_MulPolar2.PePlot.Option.GraphDataLabels = True
        '                            grp_MulPolar2.PePlot.Option.AllowDataLabels = AllowDataLabels.DataPointLabels
        '                            grp_MulPolar2.PeGrid.Option.ShowXAxis = ShowAxis.Empty
        '                            grp_MulPolar2.PeGrid.LineControl = GridLineControl.XAxis
        '                            Session("mnuCommDimChecked") = False
        '                            mnuFormLegReset_Click()
        '                            Session("CommDimm") = False
        '                            'RadialCtrl.RLabels = False
        '                            grp_MulPolar2.PeUserInterface.HotSpot.Data = False

        '                        Else
        '                            If m = 3 Then
        '                                grp_MulPolar3.PePlot.Option.GraphDataLabels = True
        '                                grp_MulPolar3.PePlot.Option.AllowDataLabels = AllowDataLabels.DataPointLabels
        '                                grp_MulPolar3.PeGrid.Option.ShowXAxis = ShowAxis.Empty
        '                                grp_MulPolar3.PeGrid.LineControl = GridLineControl.XAxis
        '                                Session("mnuCommDimChecked") = False
        '                                mnuFormLegReset_Click()
        '                                Session("CommDimm") = False
        '                                'RadialCtrl.RLabels = False
        '                                grp_MulPolar3.PeUserInterface.HotSpot.Data = False
        '                            Else
        '                                grp_MulPolar4.PePlot.Option.GraphDataLabels = True
        '                                grp_MulPolar4.PePlot.Option.AllowDataLabels = AllowDataLabels.DataPointLabels
        '                                grp_MulPolar4.PeGrid.Option.ShowXAxis = ShowAxis.Empty
        '                                grp_MulPolar4.PeGrid.LineControl = GridLineControl.XAxis
        '                                Session("mnuCommDimChecked") = False
        '                                mnuFormLegReset_Click()
        '                                Session("CommDimm") = False
        '                                'RadialCtrl.RLabels = False
        '                                grp_MulPolar4.PeUserInterface.HotSpot.Data = False
        '                            End If
        '                        End If
        '                    End If


        '                Else
        '                    Exit For
        '                End If

        '            Next m
        '        End If



        '    End If
        'End If

        'JAM 2014-08-31
        Session("SelScenarioLabel") = False

        ''JAM 2009-07-05
        Call ShowRightControl()

    End Sub
    Private Sub AnalyzePattern()


        Dim CommonL1, CommonL2, CommonL3, CommonL4, CommonL5 As String
        Dim CommonValue, ContinuousValue As String
        Dim CheckSessionContinuous, ValidateSessionContinuous As Boolean
        Dim FirstPosition, SecondPosition As Integer
        Dim ScenaryOne, ScenaryTwo, ScenaryThree As Integer
        Dim FirstPositionA, SecondPositionA, FirstPositionB, SecondPositionB As Integer
        Dim i As Short


        CheckSessionContinuous = False
        ValidateSessionContinuous = False

        CommonL1 = ""
        CommonL2 = ""
        CommonL3 = ""
        CommonL4 = ""
        CommonL5 = ""

        'jrs 2011/10/14
        Dim pos As Integer, pos2 As Integer, pos3 As Integer = 0
        pos = InStr(Session("VARN")(1, 1), "[")
        If pos = 0 Then
            pos = Len(Session("VARN")(1, 1)) + 1
        End If
        pos2 = InStr(Session("VARN")(1, 2), "[")
        If pos2 = 0 Then
            pos2 = Len(Session("VARN")(1, 2)) + 1
        End If

        If pCOUNTE > Session("ItemsbySet") Then
            If Session("VARN")(1, 1) <> "" And Session("VARN")(1, 2) <> "" And Left(Session("VARN")(1, 1), pos - 1) = Left(Session("VARN")(1, 2), pos2 - 1) Then
                CommonL1 = Left(Session("VARN")(1, 1), pos - 1)
            End If


            If Session("VARN")(1, 1) <> "" And Session("VARN")(1, 2) <> "" Then
                FirstPosition = InStr(Session("VARN")(1, 1), "[")
                SecondPosition = InStr(Session("VARN")(1, 1), "]")
                If FirstPosition > 0 And SecondPosition > 0 Then
                    ScenaryOne = Mid(Session("VARN")(1, 1), FirstPosition + 1, SecondPosition - FirstPosition - 1)
                End If

                FirstPositionA = InStr(Session("VARN")(1, 2), "[")
                SecondPositionA = InStr(Session("VARN")(1, 2), "]")
                If FirstPositionA > 0 And SecondPositionA > 0 Then
                    ScenaryTwo = Mid(Session("VARN")(1, 2), FirstPositionA + 1, SecondPositionA - FirstPositionA - 1)
                End If

                If ScenaryOne = ScenaryTwo Then
                    CommonL2 = CType(ScenaryOne, Integer).ToString()

                End If
            End If



            If Session("GR1N")(1) <> "" And Session("GR1N")(2) <> "" And Session("GR1N")(1) = Session("GR1N")(2) Then
                CommonL3 = Session("GR1N")(1)
            End If


            If Session("GR2N")(1) <> "" And Session("GR2N")(2) <> "" And Session("GR2N")(1) = Session("GR2N")(2) Then
                CommonL4 = Session("GR2N")(1)
            End If

            If Session("GR3N")(1) <> "" And Session("GR3N")(2) <> "" And Session("GR3N")(1) = Session("GR3N")(2) Then
                CommonL5 = Session("GR3N")(1)
            End If

            For i = 3 To Session("ItemsbySet") + 1

                CommonValue = ""
                ContinuousValue = ""
                'jrs 2011/10/14
                pos3 = InStr(Session("VARN")(1, i), "[")
                If pos3 = 0 Then
                    pos3 = 1
                End If

                If CommonL1 <> "" Then
                    CommonValue = CommonValue + CommonL1
                    ContinuousValue = ContinuousValue + Left(Session("VARN")(1, i), pos3 - 1)
                End If


                If CommonL2 <> "" Then
                    FirstPositionB = InStr(Session("VARN")(1, i), "[")
                    SecondPositionB = InStr(Session("VARN")(1, i), "]")
                    If FirstPositionB > 0 And SecondPositionB > 0 Then
                        ' CType(Degree(Val(NumSetValues)), Double).ToString("0.000")
                        ScenaryThree = Mid(Session("VARN")(1, i), FirstPositionB + 1, SecondPositionB - FirstPositionB - 1)
                    End If

                    CommonValue = CommonValue + CommonL2
                    ContinuousValue = ContinuousValue + CType(ScenaryThree, Integer).ToString()
                End If


                If CommonL3 <> "" Then

                    CommonValue = CommonValue + CommonL3
                    ContinuousValue = ContinuousValue + Session("GR1N")(i)

                End If

                If CommonL4 <> "" Then
                    CommonValue = CommonValue + CommonL4
                    ContinuousValue = ContinuousValue + Session("GR2N")(i)
                End If


                If CommonL5 <> "" Then
                    CommonValue = CommonValue + CommonL5
                    ContinuousValue = ContinuousValue + Session("GR3N")(i)
                End If

                If i <= Session("ItemsbySet") Then
                    If CommonValue = ContinuousValue Then
                        CheckSessionContinuous = True

                    Else
                        CheckSessionContinuous = False
                        'jrs 2011/10/14
                        If Not Page.IsPostBack And Session("SourceRadial") <> "VarperGraph" Then
                            Dim k As Integer
                            k = SuggestItemsBySet()
                            If k > 0 Then
                                Session("ItemsbySet") = k
                                Session("VarperG") = k
                                Session("SplitGraph") = CInt(Session("COUNTE") / Session("ItemsbySet"))
                            End If
                        End If
                        Exit For
                    End If
                Else
                    If CommonValue <> ContinuousValue Then
                        ValidateSessionContinuous = True
                    Else
                        ValidateSessionContinuous = False
                        Exit For
                    End If

                End If

            Next i

        Else

            CheckSessionContinuous = True
            ValidateSessionContinuous = True


        End If


        If CheckSessionContinuous And ValidateSessionContinuous Then
            Dim a
            a = 0
            ''    Session("Continuous") = True
            ''    Session("ContinuousChecked") = True
            ''    'Flag
            ''    'ContinuousCaption = "Session("Continuous") Mode"
            ''Else
            ''    Session("Continuous") = False
            ''    Session("ContinuousChecked") = True
            ''    'Flag
            ''    'ContinuousCaption = "DisSession("Continuous") Mode"
        End If



    End Sub

    'jrs 2011/10/14
    'This routine assumes using Non-continuous mode
    Private Function SuggestItemsBySet() As Integer
        '1. Find difference between first 2 columns
        Dim Var As String, Sce As String, Sce3 As String
        Dim Var2 As String, Sce2 As String, Sce4 As String
        Dim bVar As Boolean, bSce As Boolean, bDim1 As Boolean, bDim2 As Boolean, bDim3 As Boolean
        Dim pos As Integer, pos2 As Integer
        'check Var
        pos = InStr(Session("VARN")(1, 1), "[")
        If pos = 0 Then
            pos = Len(Session("VARN")(1, 1)) + 1
        End If
        Var = Left(Session("VARN")(1, 1), pos - 1)
        pos2 = InStr(Session("VARN")(1, 2), "[")
        If pos2 = 0 Then
            pos2 = Len(Session("VARN")(1, 2)) + 1
        End If
        Var2 = Left(Session("VARN")(1, 2), pos2 - 1)
        bVar = (Var <> Var2)

        'check Sce
        If InStr(Session("VARN")(1, 1), "[") = 0 Then
            pos = InStr(Session("VARN")(0, 1), "[")
            If pos = 0 Then
                'look in formula
                If UBound(Session("aFormulas")) >= 1 Then
                    If Not Session("aFormulas")(1) Is Nothing Then
                        If Session("aFormulas")(1).Name = Session("VARN")(1, 1) Then
                            pos = InStr(Session("aFormulas")(1).disVar.displayDet(1), "[")
                            If pos > 0 Then
                                pos = Int(Mid(Session("aFormulas")(1).disVar.displayDet(1), pos + 1, InStr(Session("aFormulas")(1).disVar.displayDet(1), "]") - pos - 1))
                                If pos <= UBound(Session("vShortSce")) And pos > -1 Then
                                    Sce = Session("vShortSce")(pos)
                                ElseIf pos = -1 Then
                                    Sce = "Historical"
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                Sce = Mid(Session("VARN")(0, 1), pos + 1, InStr(Session("VARN")(0, 1), "]") - pos - 1)
            End If
        Else
            Sce = Mid(Session("VARN")(1, 1), pos + 1, InStr(Session("VARN")(1, 1), "]") - pos - 1)
        End If

        If InStr(Session("VARN")(1, 2), "[") = 0 Then
            pos2 = InStr(Session("VARN")(0, 2), "[")
            If pos2 = 0 Then
                'look in formula
                If UBound(Session("aFormulas")) >= 2 Then
                    If Not Session("aFormulas")(2) Is Nothing Then
                        If Session("aFormulas")(2).Name = Session("VARN")(1, 2) Then
                            pos2 = InStr(Session("aFormulas")(2).disVar.displayDet(1), "[")
                            If pos2 > 0 Then
                                pos2 = Int(Mid(Session("aFormulas")(2).disVar.displayDet(1), pos2 + 1, InStr(Session("aFormulas")(2).disVar.displayDet(1), "]") - pos2 - 1))
                                If pos2 <= UBound(Session("vShortSce")) And pos2 > -1 Then
                                    Sce2 = Session("vShortSce")(pos2)
                                ElseIf pos2 = -1 Then
                                    Sce2 = "Historical"
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                Sce2 = Mid(Session("VARN")(0, 2), pos2 + 1, InStr(Session("VARN")(0, 2), "]") - pos2 - 1)
            End If
        Else
            Sce2 = Mid(Session("VARN")(1, 2), pos2 + 1, InStr(Session("VARN")(1, 2), "]") - pos2 - 1)
        End If

        bSce = (Sce <> Sce2)

        'check Dim1
        bDim1 = (Session("GR1N")(1) <> Session("GR1N")(2))

        'check Dim2
        bDim2 = (Session("GR2N")(1) <> Session("GR2N")(2))
        'check Dim3
        bDim3 = (Session("GR3N")(1) <> Session("GR3N")(2))

        '2. Find next time this sequence is repeated
        Dim i As Integer, bReady As Boolean
        bReady = False
        For i = 3 To Session("COUNTE") - 1
            If bVar Then
                pos = InStr(Session("VARN")(1, i), "[")
                If pos = 0 Then
                    pos = Len(Session("VARN")(1, i)) + 1
                End If
                pos2 = InStr(Session("VARN")(1, i + 1), "[")
                If pos2 = 0 Then
                    pos2 = Len(Session("VARN")(1, i + 1)) + 1
                End If
                If Var = Left(Session("VARN")(1, i), pos - 1) And Var2 = Left(Session("VARN")(1, i + 1), pos2 - 1) Then
                    bReady = True
                End If
            End If
            If bSce Then
                If InStr(Session("VARN")(1, i), "[") = 0 Then
                    pos = InStr(Session("VARN")(0, i), "[")
                    If pos = 0 Then
                        'look in formula
                        If UBound(Session("aFormulas")) >= i Then
                            If Not Session("aFormulas")(i) Is Nothing Then
                                If Session("aFormulas")(i).Name = Session("VARN")(1, i) Then
                                    pos = InStr(Session("aFormulas")(i).disVar.displayDet(1), "[")
                                    If pos > 0 Then
                                        pos = Int(Mid(Session("aFormulas")(i).disVar.displayDet(1), pos + 1, InStr(Session("aFormulas")(i).disVar.displayDet(1), "]") - pos - 1))
                                        If pos <= UBound(Session("vShortSce")) And pos > -1 Then
                                            Sce3 = Session("vShortSce")(pos)
                                        ElseIf pos = -1 Then
                                            Sce3 = "Historical"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else
                        Sce3 = Mid(Session("VARN")(0, i), pos, InStr(Session("VARN")(0, i), "]") - pos - 1)
                    End If
                Else
                    pos = InStr(Session("VARN")(1, i), "[")
                    Sce3 = Mid(Session("VARN")(1, i), pos, InStr(Session("VARN")(1, i), "]") - pos - 1)
                End If
                If InStr(Session("VARN")(1, i + 1), "[") = 0 Then
                    pos2 = InStr(Session("VARN")(0, i + 1), "[")
                    If pos2 = 0 Then
                        'look in formula
                        If UBound(Session("aFormulas")) >= i + 1 Then
                            If Not Session("aFormulas")(i + 1) Is Nothing Then
                                If Session("aFormulas")(i + 1).Name = Session("VARN")(1, i + 1) Then
                                    pos2 = InStr(Session("aFormulas")(i + 1).disVar.displayDet(1), "[")
                                    If pos2 > 0 Then
                                        pos2 = Int(Mid(Session("aFormulas")(i + 1).disVar.displayDet(1), pos + 1, InStr(Session("aFormulas")(i + 1).disVar.displayDet(1), "]") - pos2 - 1))
                                        If pos2 <= UBound(Session("vShortSce")) And pos2 > -1 Then
                                            Sce4 = Session("vShortSce")(pos2)
                                        ElseIf pos2 = -1 Then
                                            Sce4 = "Historical"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else
                        Sce4 = Mid(Session("VARN")(0, i + 1), pos2, InStr(Session("VARN")(0, i + 1), "]") - pos2 - 1)
                    End If
                Else
                    pos2 = InStr(Session("VARN")(1, i + 1), "[")
                    Sce4 = Mid(Session("VARN")(1, i + 1), pos2, InStr(Session("VARN")(1, i + 1), "]") - pos2 - 1)
                End If
                If Sce3 = Sce And Sce4 = Sce2 Then
                    bReady = True
                Else
                    bReady = False
                End If
            End If

            If bDim1 Then
                If Session("GR1N")(1) = Session("GR1N")(i) And Session("GR1N")(2) = Session("GR1N")(i + 1) Then
                    bReady = True
                Else
                    bReady = False
                End If
            End If

            If bDim2 Then
                If Session("GR2N")(1) = Session("GR2N")(i) And Session("GR2N")(2) = Session("GR2N")(i + 1) Then
                    bReady = True
                Else
                    bReady = False
                End If
            End If

            If bDim3 Then
                If Session("GR3N")(1) = Session("GR3N")(i) And Session("GR3N")(2) = Session("GR3N")(i + 1) Then
                    bReady = True
                Else
                    bReady = False
                End If
            End If

            If bReady Then
                Exit For
            End If
        Next i

        If bReady Then
            SuggestItemsBySet = CInt(Session("COUNTE") / (i - 1))
        Else
            SuggestItemsBySet = 0
        End If
    End Function

    'JAM 2009-07-15 Common Scaling Correction
    Private Sub mnuCommScal_Click()
        Dim a, j As Short


        Session("MaxRadious") = 0
        If Not Session("mnuCommScalChecked") Then
            If Session("MultipleGraphs") Then

                For a = 1 To pCOUNTE
                    If j < 4 Then
                        If Session("PLOT")(Session("PYear"), a) > Session("MaxRadious") Then
                            Session("MaxRadious") = Session("PLOT")(Session("PYear"), a)
                        End If
                    Else
                        Exit For
                    End If

                Next a
                'JAM 200907-18 Only works in MultipleGraphs mode
                Session("mnuCommScalChecked") = True
            End If


        Else
            Session("mnuCommScalChecked") = False
        End If

        'JAM 2009-07-15
        Call ShowRightControl()


        'Session("MaxRadious") = 0
        'If Not Session("mnuCommScalChecked") Then
        '    If Session("MultipleGraphs") Then

        '        For a = 1 To pCOUNTE
        '            If j < 4 Then
        '                If Session("PLOT")(Session("PYear"), a) > Session("MaxRadious") Then
        '                    Session("MaxRadious") = Session("PLOT")(Session("PYear"), a)
        '                End If
        '            Else
        '                Exit For
        '            End If

        '        Next a


        '        For i = 0 To 3
        '            If i = 1 Then
        '                grp_MulPolar1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.Max
        '                grp_MulPolar1.PeGrid.Configure.ManualMaxY = Session("MaxRadious") * 1.1
        '            Else
        '                If i = 2 Then
        '                    grp_MulPolar2.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.Max
        '                    grp_MulPolar2.PeGrid.Configure.ManualMaxY = Session("MaxRadious") * 1.1
        '                Else
        '                    If i = 3 Then
        '                        grp_MulPolar3.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.Max
        '                        grp_MulPolar3.PeGrid.Configure.ManualMaxY = Session("MaxRadious") * 1.1
        '                    Else
        '                        grp_MulPolar4.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.Max
        '                        grp_MulPolar4.PeGrid.Configure.ManualMaxY = Session("MaxRadious") * 1.1
        '                    End If
        '                End If
        '            End If


        '        Next i
        '    End If
        '    Session("mnuCommScalChecked") = True

        'Else
        '    For i = 0 To 3
        '        If i = 1 Then
        '            grp_MulPolar1.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.None
        '        Else
        '            If i = 2 Then
        '                grp_MulPolar2.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.None
        '            Else
        '                If i = 3 Then
        '                    grp_MulPolar3.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.None
        '                Else
        '                    grp_MulPolar4.PeGrid.Configure.ManualScaleControlY = ManualScaleControl.None
        '                End If
        '            End If
        '        End If



        '    Next i
        '    Session("mnuCommScalChecked") = False
        'End If

    End Sub
    Private Sub mnuContinuous_Click()
        If Not Session("gThirdDimmOk") Then 'JAM 2014-12-16 Only If there is a data pattern
            If Session("Continuous") Then
                Session("Continuous") = False
                Session("ContinuousChecked") = True
                cswmWebMenu.Groups("mnuMulGraphs").Items("mnuContinuous").IconImage = "blank.gif"

            Else
                Session("Continuous") = True
                Session("ContinuousChecked") = True
                cswmWebMenu.Groups("mnuMulGraphs").Items("mnuContinuous").IconImage = "checkbox.gif"

            End If

            'JAM 2014-09-27
            Call DiscPattern()

            'JAM 2009-07-05
            Call ShowRightControl()
            'RadialCtrl.Visible = True
        End If
    End Sub

    Private Sub mnuDefinitionScaling_Click()
        LoadMessage("Scaling uses by default the maximum and minimum values for each variable accross all regions and time, producing values between 0 and 1", Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Sub mnuDiffGraphs_Click()
        Dim i As Short

        'JAM 2009-07-15
        'Session("SourceRadial") = "Package"

        If Session("SplitGraph") > 1 Or Session("NumYearsSel") > 1 Then
            If Not Session("MultipleGraphs") Then
                Session("MultipleGraphs") = True
                Session("mnuDiffGraphsChecked") = True


                'JAM 2009-04-07 Multiple Polar
                'RadialCtrl.Enabled = False
                'RadialCtrl.Visible = False


                'For i = 0 To Session("SplitGraph") * Session("NumYearsSel") - 1
                '    If i < 4 Then
                '        If i = 0 Then
                '            grp_MulPolar1.Enabled = True
                '            grp_MulPolar1.Visible = True

                '        Else
                '            If i = 1 Then
                '                grp_MulPolar2.Enabled = True
                '                grp_MulPolar2.Visible = True
                '            Else
                '                If i = 2 Then
                '                    grp_MulPolar3.Enabled = True
                '                    grp_MulPolar3.Visible = True
                '                Else
                '                    grp_MulPolar4.Enabled = True
                '                    grp_MulPolar4.Visible = True
                '                End If
                '            End If
                '        End If



                '    Else
                '        Exit For
                '    End If
                'Next i

                'lblJS.Text = Replace(lblJS.Text, "AnyChartDataforRadial.aspx", "AnyChartDashboardLayoutRadial.aspx")

            Else
                Session("MultipleGraphs") = False
                Session("mnuDiffGraphsChecked") = False
                'Call Graph_settings()
                'RadialCtrl.Enabled = True
                'RadialCtrl.Visible = True
                'lblJS.Text = Replace(lblJS.Text, "AnyChartDashboardLayoutRadial.aspx", "AnyChartDataforRadial.aspx")
            End If
            PackJS()
            'JAM 2009-07-05
            Call ShowRightControl()


            'JAM 2009-07-05 Multiple Polar
            'If Not Session("CommDimm") And Not Session("MultipleGraphs") Then
            '    RadialCtrl.Visible = True
            'Else
            '    Session("mnuCommDimChecked") = False
            '    Call mnuCommDim_Click()
            'End If
        End If
    End Sub
    Private Sub mnuDispFormTitIntYr_Click()
        'Flag
        'frm_DisplayControl.Show(vbModal)

        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub

    Private Sub mnuDispMulYears_Click()
        Dim a, b, i As Short

        a = Session("NumYearsSel")
        b = Session("YearsSel")(0)
        For i = 0 To Session("NumYearsSel") - 1
            'lstYears.GetSelectedIndices(Session("YearsSel")(i) - 1) = True
            'jrs 2011/10/14
            'a = Session("YearsSel")(i)
            'lstYears.Items(Session("YearsSel")(i) - 1).Selected = Session("YearsSel")(i)
            lstYears.Items(Session("YearsSel")(i) - 1).Selected = True
        Next i



        lstYears.Visible = True
        cmdDraw.Visible = True

        lstYears.Enabled = True
        cmdDraw.Enabled = True

        lblJS.Visible = False

    End Sub
    Private Sub mnuFormatLegends_Click()
        'Flag
        'frm_GraphLabels.Show(vbModal)
        'JAM 2009-07-05
        Call ShowRightControl()
        'RadialCtrl.Visible = True

    End Sub


    Private Sub mnuOffScaling_Click()
        'jrs 2012/05/22
        'If Session("mnuOnScalingChecked") And Not Session("mnuOffScalingChecked") Then
        '    Session("mnuOnScalingChecked") = False
        '    Session("mnuOffScalingChecked") = True
        '    If Session("CalcScaling") Then

        '        Call ResetScaling()
        '    End If
        '    'JAM 2009-07-05
        '    Call ShowRightControl()
        '    'RadialCtrl.Visible = True
        'End If
        Dim TargetUrl As String
        TargetUrl = "frm_BaseRadial.aspx"
        If Not IsNothing(Request.QueryString("ListNames")) Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(Request.QueryString("ListNames"))
            TargetUrl = TargetUrl & "&HistFor=" & Request.QueryString("HistFor")
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            TargetUrl = TargetUrl & "&Dim1=" & PrepareForURL(Request.QueryString("Dim1"))
            TargetUrl = TargetUrl & "&Dim2=" & PrepareForURL(Request.QueryString("Dim2"))
            TargetUrl = TargetUrl & "&Dim3=" & PrepareForURL(Request.QueryString("Dim3"))
            TargetUrl = TargetUrl & "&File=" & PrepareForURL(Request.QueryString("File"))
            'jrs 2015/11/03
            If Not IsNothing(Request.QueryString("Year")) Then
                TargetUrl = TargetUrl & "&Year=" & Request.QueryString("Year")
            End If
            TargetUrl = TargetUrl & "&Scale=0"
            'jrs 2021/01/19 moved up here, it was outside the end if
            Response.Redirect(TargetUrl)
        Else 'jrs 2021/01/19
            Session("mnuOnScalingChecked") = False
            Session("mnuOffScalingChecked") = True
            If Session("CalcScaling") Then
                Call ResetScaling()
            End If

            PackJS()
        End If

    End Sub

    Private Sub mnuOnScaling_Click()

        'jrs 2012/05/22
        'If Session("mnuOffScalingChecked") And Not Session("mnuOnScalingChecked") Then
        '    Session("mnuOnScalingChecked") = True
        '    Session("mnuOffScalingChecked") = False
        '    If Not Session("CalcScaling") Then
        '        Call Scaling()
        '    End If

        '    'JAM 2009-07-05
        '    Call ShowRightControl()
        '    'RadialCtrl.Visible = True
        'End If
        Dim TargetUrl As String
        TargetUrl = "frm_BaseRadial.aspx"
        If Not IsNothing(Request.QueryString("ListNames")) Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(Request.QueryString("ListNames"))
            TargetUrl = TargetUrl & "&HistFor=" & Request.QueryString("HistFor")
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            TargetUrl = TargetUrl & "&Dim1=" & PrepareForURL(Request.QueryString("Dim1"))
            TargetUrl = TargetUrl & "&Dim2=" & PrepareForURL(Request.QueryString("Dim2"))
            TargetUrl = TargetUrl & "&Dim3=" & PrepareForURL(Request.QueryString("Dim3"))
            TargetUrl = TargetUrl & "&File=" & PrepareForURL(Request.QueryString("File"))
            'jrs 2013/03/14
            If Not IsNothing(Request.QueryString("Year")) Then
                TargetUrl = TargetUrl & "&Year=" & Request.QueryString("Year")
            End If
            TargetUrl = TargetUrl & "&Scale=1"
            'jrs 2021/01/19 moved up here, it was outside the end if
            Response.Redirect(TargetUrl)
        Else 'jrs 2021/01/19
            Session("mnuOnScalingChecked") = True
            Session("mnuOffScalingChecked") = False
            If Not Session("CalcScaling") Then
                Call Scaling()
            End If

            PackJS()
        End If

    End Sub

    Private Function CheckNegsPLOT() As Boolean

        CheckNegsPLOT = False
        Dim i As Integer, j As Integer
        For j = 1 To Session("COUNTE")
            For i = 1 To Session("FinYear")(j)
                If IsNumeric(Session("PLOT")(i, j)) Then
                    If Session("PLOT")(i, j) < 0 Then
                        CheckNegsPLOT = True
                        Exit Function
                    End If
                End If
            Next i
        Next j
    End Function
    Private Sub BuildCommonLegend(ByVal II As Integer)
        Dim a As Integer
        Dim VN, d1, d2, d3 As String
        Dim x As String

        If Session("SplitGraph") > 1 And Not Session("Continuous") Then 'And FDNG1(1) <> FDNG1(2) Then
            a = 1 + (II - 1) * Session("SplitGraph")
        Else
            a = II
        End If




        VN = LTrim$(RTrim$(Session("VARN")(1, a)))
        d1 = LTrim$(RTrim$(Session("FDNG1")(a)))
        d2 = LTrim$(RTrim$(Session("FDNG2")(a)))
        d3 = LTrim$(RTrim$(Session("FDNG3")(a)))
        If Session("MultipleYears") Then

            x = VN
            If Len(x) < 18 Then
                If Len(d1) > 0 Then x = x + d1
                If Len(d2) > 0 Then
                    x = x + "," + d2

                    If d3 > "" Then
                        x = x + "," + d3
                    End If
                    x = x '+ ")"
                Else
                    If Len(d1) > 0 Then
                        Session("CommonLegend") = x


                        Session("CommonLegend") = x '+ ")"
                    End If
                End If
            End If
        Else
            x = VN
            If Len(x) < 18 Then

                If Len(d2) > 0 Then
                    x = x + d2

                    If d3 > "" Then
                        x = x + "," + d3
                    End If
                    Session("CommonLegend") = x '+ ")"
                Else
                    If Len(d1) > 0 Then
                        Session("CommonLegend") = x


                        Session("CommonLegend") = x '+ ")"
                    End If
                End If
            End If

        End If

        Session("CommonLegend") = x

    End Sub

    Private Sub mnuSSetLabels_Click()
        Dim i, NumSets As Integer

        If Not Session("MultipleGraphs") Then
            'ReDim Session("SubSetLabel")(RadialCtrl.PeData.Subsets)
            'NumSets = RadialCtrl.PeData.Subsets
            'For i = 0 To RadialCtrl.PeData.Subsets - 1
            '    Session("SubSetLabel")(i) = RadialCtrl.PeString.SubsetLabels(i)
            'Next i


            lblJS.Visible = True

        End If


    End Sub

    Private Sub mnuVarpGraph_Click()

        'VarperGraph = InputBox("Enter a value for number of variables per graph", "Variables per Graph", Session("ItemsbySet"))
        'jrs 2012/12/14 Session("VarperGraphParent") = "frm_BaseRadial.aspx"
        Session("VarperGraphParent") = Request.Url.AbsoluteUri
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_VarperGraph.aspx")

        ''JAM 2009-07-05
        'Call ShowRightControl()

    End Sub

    Private Sub Complement_VarpGraph()

        Dim i, m As Short
        Dim VarperGraph As Double




        VarperGraph = Session("VarperG")
        If IsNumeric(VarperGraph) Then

            If VarperGraph >= 3 And VarperGraph <= 3000 Then
                Session("ItemsbySet") = Math.Round(VarperGraph)


                If pCOUNTE <= 6 And (pCOUNTE Mod Session("ItemsbySet")) <> 0 Then
                    Session("SplitGraph") = 1  'Normal Graphing
                    Session("mnuVarpGraphEnabled") = True 'False

                Else
                    If (pCOUNTE Mod Session("ItemsbySet")) = 0 Then
                        Session("SplitGraph") = (pCOUNTE / Session("ItemsbySet")) 'Multiple GRaphing
                        Session("mnuVarpGraphEnabled") = True ' False

                    Else
                        Session("SplitGraph") = 1  'Normal Graphing
                        Session("ItemsbySet") = 6
                        Session("mnuVarpGraphEnabled") = True

                    End If
                End If




                lblJS.Enabled = True
                lblJS.Visible = True


                'RadialCtrl.Visible = True


            Else
                'JAM 2009-07-06
                Session("SourceRadial") = "Package"
                LoadMessage("You must enter a numeric value between 3 and 3000", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")


            End If
        Else
            'JAM 2009-07-06
            Session("SourceRadial") = "Package"
            LoadMessage("You must enter a numeric value between 3 and 3000", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")


        End If



    End Sub
    Public Sub FindScalingMinMax(ByVal i As Integer, ByRef Min As Single, ByRef Max As Single, ByVal iScalingHorizon As Integer)
        Dim j As Integer, k As Integer
        Dim bHistFor As Boolean
        Dim DoingHistory As Boolean
        Dim IfsPars As New myParameters
        Dim sDef As String, sUn As String

        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
            Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
            Session("PutFdrvRun"), Session("CurrencySym"))



        Dim sVar As String
        sVar = Session("VARN")(1, i)

        'JAM 2008-10-22
        If InStr(sVar, "[-1]") Then
            DoingHistory = True
        Else
            DoingHistory = False
        End If


        Dim iPos As Integer
        iPos = InStr(sVar, "[")
        If iPos > 0 Then
            sVar = Mid(sVar, 1, iPos - 1)
        End If



        Dim bFormula As Boolean
        If UBound(Session("aFormulas")) >= i Then
            bFormula = Not (Session("aFormulas")(i) Is Nothing)
            If bFormula Then
                If Session("aFormulas")(i).Name <> Session("VARN")(1, i) Then
                    bFormula = False
                End If
            End If
        Else
            bFormula = False
        End If
        'bFormulas(i) = bFormula
        Dim fCopy As New Formula
        If bFormula Then
            fCopy = Session("aFormulas")(i)
        End If

        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single
        'jrs 2014/10/21
        Dim b2005PPP As Boolean
        checkPovertyVarName(sVar, bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP)
        Dim iRowPosition As Integer, iFirstYear As Integer
        Dim itotYears As Integer



        If Session("FirstYear") = "1960" And UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
            bHistFor = True
        Else
            bHistFor = False
        End If

        If bHistFor Then
            iRowPosition = LastYearHistory - FirstYearHistory + 1
            iFirstYear = FirstYearForecast
            itotYears = LastYearHistory - FirstYearHistory + 1
        Else
            iRowPosition = 1
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                iFirstYear = FirstYearForecast
            Else
                iFirstYear = FirstYearHistory
            End If
            itotYears = 0
        End If
        itotYears = itotYears + iScalingHorizon

        '            Dim AllVals() As Object 'can't use as single b/c when getting a Null it blows up
        ReDim Session("AllVals")(AMAX(itotYears, Session("LastYear") - Session("FirstYear") + 1), NR)

        'jrs 2008/01/11
        'Handling Files with formulas
        Dim fileIdx As Integer, oldCounte As Integer
        fileIdx = 0
        oldCounte = Session("COUNTE")
        If bFormula Then
            fileIdx = InStr(1, Session("aFormulas")(i).disVar.displayDet(1), "[")
            fileIdx = Val(Mid(Session("aFormulas")(i).disVar.displayDet(1), fileIdx + 1, Len(Session("aFormulas")(i).disVar.displayDet(1)) - fileIdx - 1))
        End If

        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For j = 1 To NR
            If Not bFormula Then
                If DoingHistory Then
                    'JAM 2009-07-02
                    'gLoadForDisplayHis(sVar, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1, _
                    'j = 1, j = NR, False, 1, Session("AllVals"), j, LastYearHistory - iScalingHorizon + 1, LastYearHistory, _
                    'False, False, False, "", Session("bFillHis"), Session("buseAllHis"), 0)
                    gLoadForDisplayHis(sVar, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1,
                        False, 1, Session("AllVals"), j, FirstYearHistory, LastYearHistory,
                        False, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))


                Else
                    'jrs 2011/06/17
                    If bIncR Then
                        gLoadIncomeRange(PovLevel, PovLevel2, GRPS(1, j), fileIdx, False, bPovPct, b2005PPP,
                            Session("AllVals"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                            Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                            Session("SensitivityFlag"), Session("mnuSQLite"))
                    ElseIf bPov Or bPovPct Then
                        'JAM 2009-07-02
                        'gLoadPovertyDisp(PovLevel, GRPS(1, j), 1, False, bPovPct)
                        gLoadPovertyDisp(PovLevel, GRPS(1, j), fileIdx, False, bPovPct, b2005PPP,
                            Session("AllVals"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                            Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"),
                            Session("SensitivityFlag"), Session("mnuSQLite"))


                    ElseIf bPovFind Then
                        'JAM 2009-07-02
                        'gFindPovertyLevel(GRPS(1, j), 1, False, PovLevel)
                        gFindPovertyLevel(GRPS(1, j), fileIdx, False, PovLevel, b2005PPP,
                            Session("AllVals"), Session("COUNTE"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FirstYear"), Session("LastYear"),
                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"),
                            Session("FileforDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("CurrencySym"), Session("mnuSQLite"))
                    Else
                        'JAM 2009-07-02
                        'gLoadForDisplay(sVar, -1, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1, "", 1, _
                        'j = 1, j = NR, False, 1, Session("AllVals"), j, iRowPosition, iFirstYear, iFirstYear + iScalingHorizon - 1, 0, _
                        'False, "", False)
                        gLoadForDisplayNET(sVar, -1, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1, "", fileIdx, (j = 1), (j = NR), False, 1, Session("AllVals"), j,
                            iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                            Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                        'gLoadForDisplay(sVar, -1, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1, "", fileIdx, (j = 1), (j = NR), False, 1, Session("AllVals"), j,
                        'iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                        'Session("IFs_MDB2Active"), Session("CurrencyVal"))

                        'gLoadForDisplay(sVar, idxVar, dim1, DimIdx, Dim2, -1, Dim3, -1, "", fileIdx, (DimIdx = 1), (DimIdx = NR), bGrp, 1, Session("PLOT"), Session("COUNTE"), _
                        '                        iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), _
                        '                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                        'gLoadForDisplaySaveLbls(Session("COUNTE"), idxVar, dim1, Dim2, Dim3, bGrp, fileIdx, sTitle, _
                        'Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
                        'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))




                    End If
                    If bHistFor Then
                        Session("FinAll") = Session("FinYear")(j)
                        'JAM 2009-07-02
                        'gLoadForDisplayHis(sVar, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1, _
                        'j = 1, j = NR, False, 1, Session("AllVals"), j, FirstYearHistory, LastYearHistory - 1, _
                        'False, True, False, "", Session("bFillHis"), Session("buseAllHis"), 0)
                        'Session("FinYear")(j) = LastYearHistory - FirstYearHistory + Session("FinAll")

                        gLoadForDisplayHis(sVar, GRPS(1, j), j, Session("GR2N")(i), -1, Session("GR3N")(i), -1,
                            False, 1, Session("AllVals"), Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                            False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                            Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))


                    End If
                End If
            Else
                If Session("aFormulas")(i).Formula2 > "" Then
                    'JAM 2009-07-02
                    'gLoadCompHisFor(Nothing, GRPS(1, j), Session("GR2N")(i), Session("GR3N")(i), fileIdx, 1, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE") + 1))

                    gLoadCompHisFor(Nothing, GRPS(1, j), Session("GR2N")(i), Session("GR3N")(i), fileIdx, 1, Session("bFillHis"), Session("buseAllHis"),
                        Session("FinYearHis")(Session("COUNTE")), Session("COUNTE"), Session("aFormulas"),
                        Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("VARN"), Session("FileforDisplay"),
                        IfsPars, Session("PLOT"), Session("bFillHisOnlyHoles"), Nothing,
                        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                Else
                    Session("COUNTE") = Session("COUNTE") + 1
                    'JAM 2009-07-02
                    'gLoadComp(Session("COUNTE"), Session("aFormulas")(i).Formula, Session("aFormulas")(i).Name, GRPS(1, j), Session("GR2N")(i), Session("GR3N")(i), _
                    'Session("aFormulas")(i).Unit, -1, False, fileIdx, frm_VarSel.mnuVarSelOptHistoricPlusForecast.Checked, _
                    'False, fCopy, Session("bFillHis"), True, Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")))


                    gLoadComp(Session("COUNTE"), Session("aFormulas")(i).Formula, sVar, GRPS(1, j), Session("GR2N")(i), Session("GR3N")(i),
                       Session("aFormulas")(i).Unit, -1, False, fileIdx, bHistFor, False, fCopy, Session("bFillHis"), Session("buseAllHis"),
                       Session("FinYearHis")(Session("COUNTE")), True, Session("bComputation"), Session("FirstYear"), Session("LastYear"),
                       Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"),
                       IfsPars, Session("AllVals"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                       Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))


                End If
            End If
        Next j

        Min = 1.0E+20
        Max = -1.0E+20
        'JRS 2008/05/02
        'This offset is used when doing only partial years in History with Formulas
        'because we want to use the last years, and not the first
        Dim offSet As Integer
        offSet = 0
        If UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" And bFormula Then
            offSet = LastYearHistory - FirstYearHistory + 1 - itotYears
        End If

        For k = 1 To NR
            For j = 1 + offSet To itotYears + offSet 'jrs 2008/05/02 added + offset
                If Not bFormula Then
                    If IsNumeric(Session("AllVals")(j, k)) And Not (Session("AllVals")(j, k) Is Nothing) Then
                        If Min > Session("AllVals")(j, k) Then
                            Min = Session("AllVals")(j, k)
                        End If
                        If Max < Session("AllVals")(j, k) Then
                            Max = Session("AllVals")(j, k)
                        End If
                    End If
                Else
                    If IsNumeric(Session("PLOT")(j, oldCounte + k)) And Not (Session("PLOT")(j, oldCounte + k) Is Nothing) Then
                        If Min > Session("PLOT")(j, oldCounte + k) Then
                            Min = Session("PLOT")(j, oldCounte + k)
                        End If
                        If Max < Session("PLOT")(j, oldCounte + k) Then
                            Max = Session("PLOT")(j, oldCounte + k)
                        End If
                    End If
                End If
            Next j
        Next k

        'jrs 2015/11/03 removing rounding
        'sometimes the Min is just a little smaller
        'and rounding makes it a little bigger
        'when doing scaling produces a negative
        'If Max - Min < 0.003 Then
        '    Min = Math.Round(Min, 4)
        '    Max = Math.Round(Max, 4)
        'Else
        '    Select Case Math.Abs(Min)
        '        Case 100 To 1000
        '            Min = Math.Round(Min, 1)
        '        Case 10 To 100
        '            Min = Math.Round(Min, 2)
        '        Case 0 To 10
        '            Min = Math.Round(Min, 3)
        '        Case Else
        '            Min = Math.Round(Min, 0)
        '    End Select

        '    Select Case Math.Abs(Max)
        '        Case 100 To 1000
        '            Max = Math.Round(Max, 1)
        '        Case 10 To 100
        '            Max = Math.Round(Max, 2)
        '        Case 0 To 10
        '            Max = Math.Round(Max, 3)
        '        Case Else
        '            Max = Math.Round(Max, 0)
        '    End Select
        'End If

        If bFormula Then
            Session("COUNTE") = oldCounte
        End If

    End Sub

    Public Sub Scaling()
        Dim j As Integer
        Dim k As Integer
        Dim Min As Single, Max As Single
        Dim currentVariable As String

        currentVariable = ""
        For j = 1 To pCOUNTE
            If currentVariable <> (Session("VARN")(1, j) + (Session("GR2N")(j)) + (Session("GR3N")(j))) Then
                'flag
                FindScalingMinMax(j, Min, Max, Session("DisYear"))
                currentVariable = (Session("VARN")(1, j) + (Session("GR2N")(j)) + (Session("GR3N")(j)))
            End If

            For k = 1 To Session("DisYear")
                'JAM 2009-07-18 Validation required to calculate
                If IsNumeric(Session("PLOT")(k, j)) Then
                    Session("PLOT")(k, j) = (Session("PLOT")(k, j) - Min) / (Max - Min)
                End If
            Next k
        Next j




        Session("CalcScaling") = True


        'DoEvents()


    End Sub

    Public Sub ResetScaling()
        Dim aa, ab As Short

        For aa = 1 To Session("DisYear")
            For ab = 1 To pCOUNTE
                Session("PLOT")(aa, ab) = Session("LocalPlot")(aa, ab)
            Next ab
        Next aa

        'jrs 2021/01/19
        Session("CalcScaling") = False

    End Sub

    'JAM 2009-07-05
    Private Sub ShowRightControl()

        lblJS.Enabled = True
        lblJS.Visible = True
        PackJS()
    End Sub

    Private Sub DiscPattern()

        Dim firstdata As Integer
        Dim SetLegend As String
        Dim TotalGroups As Integer
        Dim NumSet As Integer
        Dim Distance As Integer
        Dim FirstValue
        Dim a, i, j, k As Integer


        If Not Session("Continuous") Then

            ReDim Session("SetPatt")(Session("SplitGraph"), Session("ItemsbySet"))



            For j = 1 To Session("SplitGraph")
                i = j
                For k = 1 To Session("ItemsbySet")
                    Session("SetPatt")(j - 1, k - 1) = i
                    i = i + Session("SplitGraph")
                Next k
            Next j



            ReDim Session("NewIndex")(pCOUNTE)

            a = 0
            For j = 1 To Session("SplitGraph")
                For k = 1 To Session("ItemsbySet")
                    Session("NewIndex")(a) = Session("SetPatt")(j - 1, k - 1)
                    a = a + 1

                Next k
            Next j



        Else

            ReDim Session("NewIndex")(pCOUNTE)
            For k = 1 To pCOUNTE
                Session("NewIndex")(k - 1) = k
            Next k

        End If


    End Sub

    'jrs 2012/05/02
    Protected Sub cmdGoToYear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGoToYear.Click

        If Not IsNumeric(txtGoToYear.Text) Then
            LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        If Session("FirstYear") = FirstYearForecast Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            If CInt(txtGoToYear.Text) > Session("LastYearForecast") Or CInt(txtGoToYear.Text) < FirstYearHistory Then
                LoadMessage("Invalid Year, year must be between " & Session("FirstYear") & " and " & Session("DisplayLastYear"), Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If

        End If

        Session("PYear") = CInt(txtGoToYear.Text) - Session("FirstYear") + 1
        If Session("PYear") < 1 Then Session("PYear") = 1
        If Session("PYear") > Session("DisplayLastYear") - Session("FirstYear") + 1 Then
            'jrs 2012/05/02
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" And Session("PYear") + Session("FirstYear") - 1 <= Session("LastYearForecast") Then
                Session("DisplayLastYear") = Session("PYear") + Session("FirstYear") - 1
            Else
                Session("PYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
            End If
        End If
        If Session("PYear") > Session("FinYear")(1) Then Session("PYear") = Session("FinYear")(1)

        Session("PE_CommonMenu") = "Display"

        Session("YearsSel")(0) = Session("PYear")

        frm_GoToYear.Visible = False
        lblJS.Visible = True
        cswmWebMenu.Visible = True
        'jrs 2012/05/17
        Dim TargetUrl As String
        TargetUrl = "frm_BaseRadial.aspx"
        If Not IsNothing(Request.QueryString("ListNames")) Then
            TargetUrl = TargetUrl & "?ListNames=" & PrepareForURL(Request.QueryString("ListNames"))
            TargetUrl = TargetUrl & "&HistFor=" & Request.QueryString("HistFor")
            'jrs 2011/04/11
            TargetUrl = TargetUrl & "&GrpOp="
            Select Case True
                Case Session("bUseGroup") 'Groups 1
                    TargetUrl = TargetUrl & "1"
                Case Session("bUseGroupDec") 'Dec Groups 2
                    TargetUrl = TargetUrl & "2"
                Case Session("bUseGList") 'G-List 3
                    TargetUrl = TargetUrl & "3"
                Case Else 'Regions 0
                    TargetUrl = TargetUrl & "0"
            End Select
            'parameter for dimensions
            TargetUrl = TargetUrl & "&Dim1=" & PrepareForURL(Request.QueryString("Dim1"))
            TargetUrl = TargetUrl & "&Dim2=" & PrepareForURL(Request.QueryString("Dim2"))
            TargetUrl = TargetUrl & "&Dim3=" & PrepareForURL(Request.QueryString("Dim3"))
            TargetUrl = TargetUrl & "&File=" & PrepareForURL(Request.QueryString("File"))
            TargetUrl = TargetUrl & "&Year=" & txtGoToYear.Text
            If Not IsNothing(Request.QueryString("Scale")) Then
                TargetUrl = TargetUrl & "&Scale=" & Request.QueryString("Scale")
            End If
            'jrs 2021/01/19 moved up here, it was outside the end if
            Response.Redirect(TargetUrl)
        Else 'jrs 2021/01/19
            PackJS()
        End If
    End Sub

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

    Protected Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        Dim Ind As Integer
        Ind = cmb_Horizon.SelectedIndex - 1
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If
        If Ind >= 1 Then
            Session("DisplayLastYear") = Session("FirstYear") + Ind * Session("Interval")
        Else
            Session("DisplayLastYear") = Val(cmb_Horizon.SelectedValue)
            If Session("FirstYear") = FirstYearForecast Then
                If Session("DisplayLastYear") > Session("LastYearForecast") Or Session("DisplayLastYear") < FirstYearHistory Then
                    Session("DisplayLastYear") = Session("LastYear")
                    LoadMessage("Invalid Horizon, reseting to " & Session("LastYear"), Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

            ElseIf UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
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
        'jrs 2011/08/26
        Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
        'jrs 2012/03/09
        lblJS.Visible = True
        cswmWebMenu.Visible = True

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
        cmb_Horizon.Items.Clear()
        cmb_Horizon.Items.Add("")
        cmb_Horizon.Items.Add(New ListItem(Session("FirstYear").ToString))

        'jrs 2012/08/17
        If Session("Interval") Is Nothing Or Session("Interval") = 0 Then
            Session("Interval") = 1
        End If

        For ICount = Session("FirstYear") + Session("Interval") To Session("LastYearForecast") Step Session("Interval") 'jrs 2005/12/11 added Session("Interval")
            cmb_Horizon.Items.Add(New ListItem(CStr(ICount)))
        Next ICount

        cmb_Horizon.Text = Session("DisplayLastYear")

        'jrs 2012/03/09
        txtHorizon.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdHorizon.ClientID + "')")

        lblJS.Visible = False
        cswmWebMenu.Visible = False

    End Sub

    Public Sub mnuGoToYear_Click()
        frm_GoToYear.Visible = True
        txtGoToYear.Text = ""

        txtGoToYear.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdGoToYear.ClientID + "')")

        lblJS.Visible = False
        cswmWebMenu.Visible = False
    End Sub

    Private Sub mnuAlias_Click()
        Dim i As Integer
        If Not Session("bAlias") Then
            Session("bAlias") = True
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group Names").IconImage = "checkbox.gif"
            End If

            'Read and load all aliases
            Dim Tbl As DataTable
            Dim s As String
            SetDataTablefromAccess(Session("IFs_MDB2"), "SELECT GroupName, Alias FROM " & GroupIDNew, "GroupName", Tbl)
            For i = 1 To Session("COUNTE")
                s = SeekValue(Tbl, Session("GR1N")(i), "Alias")
                If s > "" Then
                    Session("GR1N")(i) = s
                End If
            Next i
        Else
            Session("bAlias") = False
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("Display Format").Items("Use Alias for Group Names").IconImage = ""
            End If

            For i = 1 To Session("COUNTE")
                Session("GR1N")(i) = Session("oGR1N")(i)
            Next i
        End If
        mnuFormLegReset_Click()
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''            JAM 2014-11-01 Identifying dimmensions pattern
    ''' 
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>


    Private Function PlusThirdDimmAnalyze() As Boolean

        Dim MinLen As Single
        Dim SD1 As String
        Dim VNP As String
        Dim SVN As String
        Dim D2 As String
        Dim d1 As String
        Dim VN As String
        Dim ii As Short
        Dim Msg As String
        Dim MaxTwo As Single
        'JAM 2014-11-04 For Scenarios Analysis
        Dim FirstPosition, SecondPosition, ScenarioOne As String
        Dim FirstPositionA, SecondPositionA, ScenarioTwo As String


        If Session("COUNTE") = 0 Then
            Return False
        End If


        Dim Vars As New Collection
        Dim Dim1s As New Collection
        Dim Dim2s As New Collection
        Dim Dim3s As New Collection
        Dim Scen As New Collection

        'JAM 2015-01-25
        Dim VarsNotNull As New Collection
        Dim Dim1sNotNull As New Collection
        Dim Dim2sNotNull As New Collection
        Dim Dim3sNotNull As New Collection
        Dim ScenNotNull As New Collection


        Dim i, j, k, l, m, n As Integer
        Dim D As Object, v As Object, v2 As Object, v3 As Object, v4 As Object
        Dim vDim1, vDim2, vDim3, vDim4, vDim5 As Integer
        'JAM 2015-01-25
        Dim vDim1NotNull, vDim2NotNull, vDim3NotNull, vDim4NotNull, vDim5NotNull As Integer


        Dim sVar As String, pos As Integer
        Dim FirstCondition As Boolean

        On Error Resume Next

        FirstCondition = False
        For i = 1 To Session("COUNTE")
            pos = InStr(Session("VARN")(1, i), "[")
            If pos > 0 Then
                sVar = Mid(Session("VARN")(1, i), 1, pos - 1)
            Else
                sVar = Session("VARN")(1, i)
            End If
            v = Nothing
            v = Vars.Item(sVar)
            If (v Is Nothing) Then
                Vars.Add(i, sVar)
            End If

            'JAM 2015-01-25
            If (v Is Nothing And sVar <> "") Then
                VarsNotNull.Add(i, sVar)
            End If

            D = Nothing
            D = Dim1s.Item(Session("GR1N")(i))
            If (D Is Nothing) Then
                Dim1s.Add(i, Session("GR1N")(i))
            End If

            'JAM 2015-01-25
            If (D Is Nothing And Session("GR1N")(i) <> "") Then
                Dim1sNotNull.Add(i, Session("GR1N")(i))
            End If


            v2 = Nothing
            v2 = Dim2s.Item(Session("GR2N")(i))
            If (v2 Is Nothing) Then
                Dim2s.Add(i, Session("GR2N")(i))
            End If

            'JAM 2015-01-25
            If (v2 Is Nothing And Session("GR2N")(i) <> "") Then
                Dim2sNotNull.Add(i, Session("GR2N")(i))
            End If

            v3 = Nothing
            v3 = Dim3s.Item(Session("GR3N")(i))
            If (v3 Is Nothing) Then
                Dim3s.Add(i, Session("GR3N")(i))
            End If

            'JAM 2015-01-25
            If (v3 Is Nothing And Session("GR3N")(i) <> "") Then
                Dim3sNotNull.Add(i, Session("GR3N")(i))
            End If


            FirstPosition = InStr(Session("VARN")(1, i), "[")
            SecondPosition = InStr(Session("VARN")(1, i), "]")
            ScenarioOne = ""
            If FirstPosition > 0 And SecondPosition > 0 Then
                ScenarioOne = Mid(Session("VARN")(1, i), FirstPosition + 1, SecondPosition - FirstPosition - 1)
            End If

            v4 = Nothing
            v4 = Scen.Item(ScenarioOne)
            If (v4 Is Nothing) Then
                Scen.Add(i, ScenarioOne)
            End If

            'JAM 2015-01-25
            If (v4 Is Nothing And ScenarioOne <> "") Then
                ScenNotNull.Add(i, ScenarioOne)
            End If


        Next

        'JAM 2015-01-25
        ''Session("VarsCount") = Vars.Count
        ''Session("Dim1Count") = Dim1s.Count
        ''Session("Dim2Count") = Dim2s.Count
        ''Session("Dim3Count") = Dim3s.Count
        ''Session("ScenCount") = Scen.Count

        'JAM 2015-01-25
        Session("VarsCount") = IIf(VarsNotNull.Count = 0, 1, VarsNotNull.Count)
        Session("Dim1Count") = IIf(Dim1sNotNull.Count = 0, 1, Dim1sNotNull.Count)
        Session("Dim2Count") = IIf(Dim2sNotNull.Count = 0, 1, Dim2sNotNull.Count)
        Session("Dim3Count") = IIf(Dim3sNotNull.Count = 0, 1, Dim3sNotNull.Count)
        Session("ScenCount") = IIf(ScenNotNull.Count = 0, 1, ScenNotNull.Count)


        vDim1 = CInt(Vars.Count)
        vDim2 = CInt(Dim1s.Count)
        vDim3 = CInt(Dim2s.Count)
        vDim4 = CInt(Dim3s.Count)
        vDim5 = CInt(Scen.Count)

        'JAM 2015-01-25
        vDim1NotNull = CInt(VarsNotNull.Count)
        vDim2NotNull = CInt(Dim1sNotNull.Count)
        vDim3NotNull = CInt(Dim2sNotNull.Count)
        vDim4NotNull = CInt(Dim3sNotNull.Count)
        vDim5NotNull = CInt(ScenNotNull.Count)



        If vDim1 = 0 Then
            vDim1 = 1
        End If
        If vDim2 = 0 Then
            vDim2 = 1
        End If
        If vDim3 = 0 Then
            vDim3 = 1
        End If
        If vDim4 = 0 Then
            vDim4 = 1
        End If
        If vDim5 = 0 Then
            vDim5 = 1
        End If

        'JAM 2015-01-25
        If vDim1NotNull = 0 Then
            vDim1NotNull = 1
        End If
        If vDim2NotNull = 0 Then
            vDim2NotNull = 1
        End If
        If vDim3NotNull = 0 Then
            vDim3NotNull = 1
        End If
        If vDim4NotNull = 0 Then
            vDim4NotNull = 1
        End If
        If vDim5NotNull = 0 Then
            vDim5NotNull = 1
        End If


        'JAM 2014-12-24 Adding Counte conditions for dimmensions values
        If CInt(Session("COUNTE")) = (vDim1 * vDim2 * vDim3 * vDim4 * vDim5) Then
            FirstCondition = True
        Else 'JAM 2015-01-25 It's necessary for CLPC,LIT and Obesity options
            If CInt(Session("COUNTE")) = (vDim1NotNull * vDim2NotNull * vDim3NotNull * vDim4NotNull * vDim5NotNull) Then
                FirstCondition = True
            End If

        End If


        MaxTwo = 0
        If Session("VarsCount") > 1 Then
            MaxTwo = MaxTwo + 1
        End If

        If Session("Dim1Count") > 1 Then
            MaxTwo = MaxTwo + 1
        End If

        If Session("Dim2Count") > 1 Then
            MaxTwo = MaxTwo + 1
        End If

        If Session("Dim3Count") > 1 Then
            MaxTwo = MaxTwo + 1
        End If

        If Session("ScenCount") > 1 Then
            MaxTwo = MaxTwo + 1
        End If

        If MaxTwo > 1 And FirstCondition Then 'JAM 2014-12-24 Upgrading two dimmensions validation
            Return True

        Else
            Return False
        End If


        ''If Session("COUNTE") = (Session("VarsCount") * Session("Dim1Count") * Session("Dim2Count") * Session("Dim3Count") * Session("ScenCount")) Then
        ''    n = 1
        ''    For i = 1 To Vars.Count
        ''        For j = 1 To Dim1s.Count
        ''            For k = 1 To Dim2s.Count
        ''                For l = 1 To Dim3s.Count
        ''                    For m = 1 To Scen.Count
        ''                        FullDimm.Add(n, Vars(i) + Dim1s(j) + Dim2s(k) + Dim3s(l) + Scen(m))
        ''                        n = n + 1
        ''                    Next
        ''                Next
        ''            Next
        ''        Next
        ''    Next



        ''Else

        ''Exit Function
        ''End If



    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '''         
    '''         JAM 2014-11-20
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>


    Private Sub PlusThirdDimPattern()
        Dim i, pos, pos3, FirstPosition, SecondPosition As Integer
        Dim j, k, l, m, idx, pos1, pos2 As Integer
        Dim FullDimm As New Collection
        Dim PivotObject As Object
        Dim sVar, Scenario, Spivot, vSceName As String
        Dim MaxVar As Integer
        Dim DefMaxVar(5) As Integer
        Dim DefChooseVar(5) As Integer
        Dim GraphCount, GraphQty, IdxMax As Integer
        Dim Vars As New Collection
        Dim Vars3 As New Collection
        Dim Dim1s As New Collection
        Dim Dim2s As New Collection
        Dim Dim3s As New Collection
        Dim Sce As New Collection
        Dim SceName As New Collection 'JAM 2014-12-16
        '' Dim D As Object, v As Object, v2 As Object, v3 As Object, v4 As Object

        Dim xy, strdim1, strdim2 As String


        ReDim Session("ScenarioArray")(Session("COUNTE"))
        ReDim Session("StrVARN")(Session("COUNTE"))
        ReDim Session("StrGR1N")(Session("COUNTE"))
        ReDim Session("StrGR2N")(Session("COUNTE"))
        ReDim Session("StrGR3N")(Session("COUNTE"))
        ReDim Session("StrSce")(Session("COUNTE"))
        ReDim Session("SetLegends")(Session("COUNTE"))
        ReDim Session("StrSceName")(Session("COUNTE")) 'JAM 2014-12-16
        ReDim Session("StrVARN3")(Session("COUNTE"))




        Dim ivarn, igr1n, igr2n, igr3n, isce, iscename, ivarn3 As Integer

        'JRS 2015-01-26
        Dim sVar2 As String, sDim1 As String, sDim2 As String, sDim3 As String, idx2 As Integer
        'JAM 2015-04-19
        Dim sVar3 As String

        'JAM 2015-12-11
        Dim aa As Integer

        'JAM 2018-09-98
        Dim icigr2n As Integer
        Dim ici2gr2n As Integer
        Dim NoPattern2nd As Boolean
        Dim Fill2nd As String
        Dim icDim2s As New Collection
        Dim ic2Dim2s As New Collection


        ivarn = 0
        ivarn3 = 0
        igr1n = 0
        igr2n = 0
        igr3n = 0
        isce = 0
        iscename = 0
        sVar3 = ""


        '''''''''''''''JAM 2018-09-08 Checking 2nd Variable Pattern
        NoPattern2nd = False
        Fill2nd = ""
        icigr2n = 0
        ici2gr2n = 0
        For ic = 1 To Session("COUNTE")
            If Not Session("LabelChanged") Then
                If Not (icDim2s.Contains(Session("GR2N")(ic))) And Session("GR2N")(ic) <> "" Then
                    icigr2n = icigr2n + 1
                    icDim2s.Add(ic, Session("GR2N")(ic))
                    Fill2nd = Session("GR2N")(ic)
                Else
                    If Not (ic2Dim2s.Contains(Session("GR2N")(ic))) And Session("GR2N")(ic) = "" Then
                        ici2gr2n = ici2gr2n + 1
                        ic2Dim2s.Add(ic, Session("GR2N")(ic))
                    End If

                End If

            End If
        Next ic

        If icigr2n = 1 And ici2gr2n = 1 Then
            NoPattern2nd = True
        Else
            NoPattern2nd = False

        End If
        '''''''''''''''''''''''''''''''''''''''''''










        For i = 1 To Session("COUNTE")


            pos = InStr(Session("VARN")(1, i), "[")

            If Session("LabelChanged") Then 'JAM 2015-04-19 
                pos3 = InStr(Session("VARN")(0, i), "[")
            End If


            If pos > 0 Then
                sVar = Mid(Session("VARN")(1, i), 1, pos - 1)
            Else
                sVar = Session("VARN")(1, i)
            End If
            If Session("LabelChanged") Then 'JAM 2015-04-19 
                If pos3 > 0 Then
                    sVar3 = Mid(Session("VARN")(0, i), 1, pos3 - 1)
                Else
                    sVar3 = Session("VARN")(0, i)
                End If


            End If


            If Not (Vars.Contains(sVar)) And sVar <> "" Then 'JAM 2015-01-26
                ivarn = ivarn + 1
                Vars.Add(ivarn, sVar)
                Session("StrVARN")(ivarn) = sVar 'JAM 2014-12-07 Building different values array
            End If

            If Session("LabelChanged") Then 'JAM 2015-04-19 

                If Not (Vars3.Contains(sVar3)) And sVar3 <> "" Then 'JAM 2015-01-26
                    ivarn3 = ivarn3 + 1
                    Vars3.Add(ivarn3, sVar3)
                    Session("StrVARN3")(ivarn3) = sVar3 'JAM 2014-12-07 Building different values array
                End If
            End If

            If Not Session("LabelChanged") Then 'JAM 2015-04-19
                'JAM 2014-12-16 Collecting Scenario Names
                'pos1 = InStr(Session("oVARN0")(i), "[")
                'pos2 = InStr(Session("oVARN0")(i), "]")
                'JAM 2015-04-20
                pos1 = InStr(Session("gthirdVARN0")(i), "[")
                pos2 = InStr(Session("gthirdVARN0")(i), "]")
                If pos1 > 0 And pos2 > 0 Then
                    'vSceName = Mid(Session("oVARN0")(i), pos1 + 1, pos2 - pos1 - 1)
                    'JAM 2015-04-20
                    vSceName = Mid(Session("gthirdVARN0")(i), pos1 + 1, pos2 - pos1 - 1)

                    If Not (SceName.Contains(vSceName)) And vSceName <> "" Then 'JAM 2015-01-26
                        iscename = iscename + 1
                        SceName.Add(iscename, vSceName)
                        Session("StrSceName")(iscename) = "[" + vSceName + "]" 'JAM 2014-12-07 Building different values array
                    End If

                End If
            Else
                pos1 = InStr(Session("VARN")(0, i), "[")
                pos2 = InStr(Session("VARN")(0, i), "]")
                If pos1 > 0 And pos2 > 0 Then
                    vSceName = Mid(Session("VARN")(0, i), pos1 + 1, pos2 - pos1 - 1)


                    If Not (SceName.Contains(vSceName)) And vSceName <> "" Then 'JAM 2015-01-26
                        iscename = iscename + 1
                        SceName.Add(iscename, vSceName)
                        Session("StrSceName")(iscename) = "[" + vSceName + "]" 'JAM 2014-12-07 Building different values array
                    End If
                Else
                    vSceName = " "
                    If Not (SceName.Contains(vSceName)) Then 'JAM 2015-01-26
                        iscename = iscename + 1
                        SceName.Add(iscename, vSceName)
                        Session("StrSceName")(iscename) = vSceName  'JAM 2014-12-07 Building different values array
                    End If

                End If
            End If



            'JAM 2015-04-05 Adding label changed
            If Not Session("LabelChanged") Then
                If Not (Dim1s.Contains(Session("GR1N")(i).ToString)) And Session("GR1N")(i).ToString <> "" Then 'JAM 2015-01-26
                    igr1n = igr1n + 1
                    Dim1s.Add(i, Session("GR1N")(i).ToString)
                    Session("StrGR1N")(igr1n) = Session("GR1N")(i).ToString
                End If
            Else 'JAM 2015-04-05 Adding label changed
                strdim1 = Session("FDNG1")(i).ToString 'JAM 2015-04-13

                If Not (Dim1s.Contains(strdim1)) And strdim1 <> "" Then 'JAM 2015-01-26
                    igr1n = igr1n + 1
                    Dim1s.Add(i, strdim1)
                    Session("StrGR1N")(igr1n) = strdim1
                End If
            End If

            'JAM 2015-04-05 Adding label changed
            If Not Session("LabelChanged") Then
                If Not (Dim2s.Contains(Session("GR2N")(i).ToString)) And Session("GR2N")(i).ToString <> "" Then 'JAM 2015-01-26
                    igr2n = igr2n + 1
                    Dim2s.Add(i, Session("GR2N")(i).ToString)
                    Session("StrGR2N")(igr2n) = Session("GR2N")(i).ToString

                End If
            Else 'JAM 2015-04-05 Adding label changed
                strdim2 = Session("FDNG2")(i).ToString 'JAM 2015-04-13
                If Not (Dim2s.Contains(strdim2)) And strdim2 <> "" Then 'JAM 2015-01-26
                    igr2n = igr2n + 1
                    Dim2s.Add(i, strdim2)
                    Session("StrGR2N")(igr2n) = strdim2

                End If

            End If



            If Not (Dim3s.Contains(Session("GR3N")(i).ToString)) And Session("GR3N")(i).ToString <> "" Then 'JAM 2015-01-26
                igr3n = igr3n + 1
                Dim3s.Add(i, Session("GR3N")(i).ToString)
                Session("StrGR3N")(igr3n) = Session("GR3N")(i).ToString
            End If


            pos = InStr(Session("VARN")(1, i), "[")
            If pos > 0 Then
                sVar = Mid(Session("VARN")(1, i), 1, pos - 1)
            Else
                sVar = Session("VARN")(1, i)
            End If

            FirstPosition = InStr(Session("VARN")(1, i), "[")
            SecondPosition = InStr(Session("VARN")(1, i), "]")
            Scenario = ""
            If FirstPosition > 0 And SecondPosition > 0 Then
                Scenario = Mid(Session("VARN")(1, i), FirstPosition + 1, SecondPosition - FirstPosition - 1)

            End If
            Session("ScenarioArray")(i) = Scenario

            If Not (Sce.Contains(Session("ScenarioArray")(i))) Then
                isce = isce + 1
                Sce.Add(isce, Session("ScenarioArray")(i))
                Session("StrSce")(isce) = Session("ScenarioArray")(i)
            End If


            'JAM 2018-09-08
            xy = sVar + Session("GR1N")(i).ToString + IIf(Not NoPattern2nd, Session("GR2N")(i).ToString, Fill2nd) + Session("GR3N")(i).ToString + Session("ScenarioArray")(i).ToString

            ''PivotObject = Nothing
            'PivotObject = FullDimm.Contains(xy)
            If Not (FullDimm.Contains(xy)) Then
                'JAM 2015-04-05 Adding label changed
                If Not Session("LabelChanged") Then

                    'JAM 2018-09-08
                    'FullDimm.Add(i, sVar + Session("GR1N")(i) + Session("GR2N")(i) + Session("GR3N")(i) + Session("ScenarioArray")(i))
                    FullDimm.Add(i, sVar + Session("GR1N")(i) + IIf(Not NoPattern2nd, Session("GR2N")(i), Fill2nd) + Session("GR3N")(i) + Session("ScenarioArray")(i))
                Else 'JAM 2015-04-05 Adding label changed
                    FullDimm.Add(i, sVar + Session("FDNG1")(i) + Session("FDNG2")(i) + Session("GR3N")(i) + Session("ScenarioArray")(i))
                End If

            End If




        Next

        'JAM 2014-12-03 Review
        ReDim Session("NewIndex")(Session("COUNTE"))


        DefMaxVar(0) = CInt(Session("VarsCount"))
        DefMaxVar(1) = CInt(Session("Dim1Count"))
        DefMaxVar(2) = CInt(Session("Dim2Count"))
        DefMaxVar(3) = CInt(Session("Dim3Count"))
        DefMaxVar(4) = CInt(Session("ScenCount"))


        For i = 0 To 4
            DefChooseVar(i) = 0
        Next

        MaxVar = -1
        For i = 0 To 4
            If DefMaxVar(i) > MaxVar Then
                MaxVar = DefMaxVar(i)
                IdxMax = i
                For j = 0 To 4
                    If j = i Then
                        DefChooseVar(j) = 1
                    Else
                        DefChooseVar(j) = 0
                    End If
                Next j
            End If
        Next

        'JAM 2014-12-03 Getting Graphs Quantity
        GraphQty = 1
        For i = 0 To 4
            If DefChooseVar(i) = 0 Then
                GraphQty = GraphQty * DefMaxVar(i)
            End If
        Next


        'JAM 2014-11-29 Defining Points Graphics
        Session("ItemsbySet") = MaxVar
        Session("MultipleGraphs") = False
        Session("SplitGraph") = GraphQty

        ' ReDim Session("IndexbyGroup")(MaxVar + 1, GraphQty + 1)
        'JAM 2015-12-11 Adding third dimmension for years
        ReDim Session("IndexbyGroup")(MaxVar + 1, GraphQty + 1, MaxYears)

        'JAM 2014-11-06 Filling Bidimmensional array in order to graph values
        'Session("VarsCount") is the may value
        GraphCount = 0
        If IdxMax = 0 Then
            ReDim Session("CGroupLabels")(CInt(Session("VarsCount")) + 1)
            For m = 1 To CInt(Session("VarsCount"))
                If Not Session("LabelChanged") Then 'JAM 2015-04-19 
                    Session("CGroupLabels")(m) = Session("StrVARN")(m)

                Else
                    Session("CGroupLabels")(m) = Session("StrVARN3")(m)
                End If

            Next

            For i = 1 To CInt(Session("Dim1Count"))
                For j = 1 To CInt(Session("Dim2Count"))
                    For k = 1 To CInt(Session("Dim3Count"))
                        For l = 1 To CInt(Session("ScenCount"))
                            GraphCount = GraphCount + 1
                            'JAM 2015-10-25 Adding iif for first string
                            'JAM 2014-12-09 Adding Set Legends
                            Session("SetLegends")(GraphCount) = Session("StrGR1N")(i) + IIf(Session("StrGR2N")(j) = "", "", ",") + Session("StrGR2N")(j) + IIf(Session("StrGR3N")(k) = "", "", ",") + Session("StrGR3N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)

                            For m = 1 To CInt(Session("VarsCount"))


                                'JRS 2015-01-26
                                '''''''''''''''''''''''''''''''''''
                                sVar2 = Session("StrVARN")(m)


                                idx2 = IfsVars.Item(sVar2).Index

                                'JAM 2018-09-08, suggested code by Jose
                                If idx2 = -1 Then
                                    idx2 = 0
                                End If


                                If VDim1(idx2) > 0 Or idx2 = 0 Then
                                    sDim1 = Session("StrGR1N")(i)
                                Else
                                    sDim1 = ""
                                End If

                                If VDim2(idx2) > 0 Or idx2 = 0 Then
                                    sDim2 = Session("StrGR2N")(j)
                                Else
                                    sDim2 = ""
                                End If

                                If VDim3(idx2) > 0 Or idx2 = 0 Then
                                    sDim3 = Session("StrGR3N")(k)
                                Else
                                    sDim3 = ""
                                End If

                                Spivot = Session("StrVARN")(m) + sDim1 + sDim2 + sDim3 + Session("StrSce")(l)
                                ''''''''''''''''''''''''''''''''''''''



                                ''Spivot = Session("StrVARN")(m) + Session("StrGR1N")(i) + Session("StrGR2N")(j) + Session("StrGR3N")(k) + Session("StrSce")(l)



                                'JAM 2015-01-25 Checking that it's a valid index
                                If (FullDimm.Contains(Spivot)) Then
                                    idx = FullDimm(Spivot)

                                    For aa = 0 To MaxYears - 1  'JAM 2015-12-11 Adding third dimmension for years
                                        Session("IndexbyGroup")(m, GraphCount, aa) = Session("PLOT")(aa, idx) 'JAM 2014-12-07 Changing if there are more years selected
                                    Next
                                End If
                            Next


                        Next
                    Next

                Next
            Next
        Else
            If IdxMax = 1 Then
                ReDim Session("CGroupLabels")(CInt(Session("Dim1Count")) + 1)
                For m = 1 To CInt(Session("Dim1Count"))
                    Session("CGroupLabels")(m) = Session("StrGR1N")(m)

                Next

                For i = 1 To CInt(Session("VarsCount"))
                    For j = 1 To CInt(Session("Dim2Count"))
                        For k = 1 To CInt(Session("Dim3Count"))
                            For l = 1 To CInt(Session("ScenCount"))
                                GraphCount = GraphCount + 1

                                If Not Session("LabelChanged") Then 'JAM 2015-04-19 
                                    'JAM 2014-12-09 Adding Set Legends
                                    Session("SetLegends")(GraphCount) = Session("StrVARN")(i) + IIf(Session("StrGR2N")(j) = "", "", ",") + Session("StrGR2N")(j) + IIf(Session("StrGR3N")(k) = "", "", ",") + Session("StrGR3N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)
                                Else
                                    'JAM 2014-12-09 Adding Set Legends
                                    Session("SetLegends")(GraphCount) = Session("StrVARN3")(i) + IIf(Session("StrGR2N")(j) = "", "", ",") + Session("StrGR2N")(j) + IIf(Session("StrGR3N")(k) = "", "", ",") + Session("StrGR3N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)
                                End If


                                For m = 1 To CInt(Session("Dim1Count"))
                                    'JRS 2015-01-26
                                    '''''''''''''''''''''''''''''''''''
                                    sVar2 = Session("StrVARN")(i)
                                    idx2 = IfsVars.Item(sVar2).Index

                                    'JAM 2018-09-08, suggested code by Jose
                                    If idx2 = -1 Then
                                        idx2 = 0
                                    End If


                                    If VDim1(idx2) > 0 Or idx2 = 0 Then
                                        sDim1 = Session("StrGR1N")(m)
                                    Else
                                        sDim1 = ""
                                    End If

                                    If VDim2(idx2) > 0 Or idx2 = 0 Then
                                        sDim2 = Session("StrGR2N")(j)
                                    Else
                                        sDim2 = ""
                                    End If

                                    If VDim3(idx2) > 0 Or idx2 = 0 Then
                                        sDim3 = Session("StrGR3N")(k)
                                    Else
                                        sDim3 = ""
                                    End If

                                    Spivot = Session("StrVARN")(i) + sDim1 + sDim2 + sDim3 + Session("StrSce")(l)
                                    ''''''''''''''''''''''''''''''''''''''




                                    ''Spivot = Session("StrVARN")(i) + Session("StrGR1N")(m) + Session("StrGR2N")(j) + Session("StrGR3N")(k) + Session("StrSce")(l)


                                    'JAM 2015-01-25 Checking that it's a valid index
                                    If (FullDimm.Contains(Spivot)) Then
                                        idx = FullDimm(Spivot)
                                        For aa = 0 To MaxYears - 1  'JAM 2015-12-11 Adding third dimmension for years
                                            Session("IndexbyGroup")(m, GraphCount, aa) = Session("PLOT")(aa, idx) 'JAM 2014-12-07 Changing if there are more years selected
                                        Next
                                    End If
                                Next


                            Next
                        Next

                    Next
                Next



            Else
                If IdxMax = 2 Then
                    ReDim Session("CGroupLabels")(CInt(Session("Dim2Count")) + 1)
                    For m = 1 To CInt(Session("Dim2Count"))
                        Session("CGroupLabels")(m) = Session("StrGR2N")(m)

                    Next

                    For i = 1 To CInt(Session("VarsCount"))
                        For j = 1 To CInt(Session("Dim1Count"))
                            For k = 1 To CInt(Session("Dim3Count"))
                                For l = 1 To CInt(Session("ScenCount"))
                                    GraphCount = GraphCount + 1

                                    If Not Session("LabelChanged") Then 'JAM 2015-04-19 
                                        'JAM 2014-12-09 Adding Set Legends
                                        Session("SetLegends")(GraphCount) = Session("StrVARN")(i) + IIf(Session("StrGR1N")(j) = "", "", ",") + Session("StrGR1N")(j) + IIf(Session("StrGR3N")(k) = "", "", ",") + Session("StrGR3N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)
                                    Else
                                        Session("SetLegends")(GraphCount) = Session("StrVARN3")(i) + IIf(Session("StrGR1N")(j) = "", "", ",") + Session("StrGR1N")(j) + IIf(Session("StrGR3N")(k) = "", "", ",") + Session("StrGR3N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)
                                    End If


                                    For m = 1 To CInt(Session("Dim2Count"))

                                        'JRS 2015-01-26
                                        '''''''''''''''''''''''''''''''''''
                                        sVar2 = Session("StrVARN")(i)
                                        idx2 = IfsVars.Item(sVar2).Index

                                        'JAM 2018-09-08, suggested code by Jose
                                        If idx2 = -1 Then
                                            idx2 = 0
                                        End If



                                        If VDim1(idx2) > 0 Or idx2 = 0 Then
                                            sDim1 = Session("StrGR1N")(j)
                                        Else
                                            sDim1 = ""
                                        End If

                                        If VDim2(idx2) > 0 Or idx2 = 0 Then
                                            sDim2 = Session("StrGR2N")(m)
                                        Else
                                            sDim2 = ""
                                        End If

                                        If VDim3(idx2) > 0 Or idx2 = 0 Then
                                            sDim3 = Session("StrGR3N")(k)
                                        Else
                                            sDim3 = ""
                                        End If

                                        Spivot = Session("StrVARN")(i) + sDim1 + sDim2 + sDim3 + Session("StrSce")(l)
                                        ''''''''''''''''''''''''''''''''''''''


                                        ''Spivot = Session("StrVARN")(i) + Session("StrGR1N")(j) + Session("StrGR2N")(m) + Session("StrGR3N")(k) + Session("StrSce")(l)

                                        'JAM 2015-01-25 Checking that it's a valid index
                                        If (FullDimm.Contains(Spivot)) Then
                                            idx = FullDimm(Spivot)
                                            For aa = 0 To MaxYears - 1  'JAM 2015-12-11 Adding third dimmension for years
                                                Session("IndexbyGroup")(m, GraphCount, aa) = Session("PLOT")(aa, idx) 'JAM 2014-12-07 Changing if there are more years selected
                                            Next

                                        End If
                                    Next


                                Next
                            Next

                        Next
                    Next
                Else
                    If IdxMax = 3 Then
                        ReDim Session("CGroupLabels")(CInt(Session("Dim3Count")) + 1)
                        For m = 1 To CInt(Session("Dim3Count"))
                            Session("CGroupLabels")(m) = Session("StrGR3N")(m)

                        Next

                        For i = 1 To CInt(Session("VarsCount"))
                            For j = 1 To CInt(Session("Dim1Count"))
                                For k = 1 To CInt(Session("Dim2Count"))
                                    For l = 1 To CInt(Session("ScenCount"))
                                        GraphCount = GraphCount + 1
                                        If Not Session("LabelChanged") Then 'JAM 2015-04-19 
                                            'JAM 2014-12-09 Adding Set Legends
                                            Session("SetLegends")(GraphCount) = Session("StrVARN")(i) + IIf(Session("StrGR1N")(j) = "", "", ",") + Session("StrGR1N")(j) + IIf(Session("StrGR2N")(k) = "", "", ",") + Session("StrGR2N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)

                                        Else
                                            Session("SetLegends")(GraphCount) = Session("StrVARN3")(i) + IIf(Session("StrGR1N")(j) = "", "", ",") + Session("StrGR1N")(j) + IIf(Session("StrGR2N")(k) = "", "", ",") + Session("StrGR2N")(k) + IIf(Session("StrSceName")(l) = "", "", ",") + Session("StrSceName")(l)
                                        End If
                                        For m = 1 To CInt(Session("Dim3Count"))

                                            'JRS 2015-01-26
                                            '''''''''''''''''''''''''''''''''''
                                            sVar2 = Session("StrVARN")(i)
                                            idx2 = IfsVars.Item(sVar2).Index

                                            'JAM 2018-09-08, suggested code by Jose
                                            If idx2 = -1 Then
                                                idx2 = 0
                                            End If



                                            If VDim1(idx2) > 0 Or idx2 = 0 Then
                                                sDim1 = Session("StrGR1N")(j)
                                            Else
                                                sDim1 = ""
                                            End If

                                            If VDim2(idx2) > 0 Or idx2 = 0 Then
                                                sDim2 = Session("StrGR2N")(k)
                                            Else
                                                sDim2 = ""
                                            End If

                                            If VDim3(idx2) > 0 Or idx2 = 0 Then
                                                sDim3 = Session("StrGR3N")(m)
                                            Else
                                                sDim3 = ""
                                            End If

                                            Spivot = Session("StrVARN")(i) + sDim1 + sDim2 + sDim3 + Session("StrSce")(l)
                                            ''''''''''''''''''''''''''''''''''''''


                                            ''Spivot = Session("StrVARN")(i) + Session("StrGR1N")(j) + Session("StrGR2N")(k) + Session("StrGR3N")(m) + Session("StrSce")(l)

                                            'JAM 2015-01-25 Checking that it's a valid index
                                            If (FullDimm.Contains(Spivot)) Then
                                                idx = FullDimm(Spivot)
                                                For aa = 0 To MaxYears - 1  'JAM 2015-12-11 Adding third dimmension for years
                                                    Session("IndexbyGroup")(m, GraphCount, aa) = Session("PLOT")(aa, idx) 'JAM 2014-12-07 Changing if there are more years selected
                                                Next

                                            End If
                                        Next


                                    Next
                                Next

                            Next
                        Next
                    Else
                        If IdxMax = 4 Then
                            ReDim Session("CGroupLabels")(CInt(Session("ScenCount")) + 1)
                            For m = 1 To CInt(Session("ScenCount"))
                                'JAM 2015-06-12 Changing axis labels when graphed points refer to Scenarios
                                'Session("CGroupLabels")(m) = Session("StrSce")(m)
                                Session("CGroupLabels")(m) = Replace(Replace(Session("StrSceName")(m), "[", ""), "]", "")
                            Next

                            For i = 1 To CInt(Session("VarsCount"))
                                For j = 1 To CInt(Session("Dim1Count"))
                                    For k = 1 To CInt(Session("Dim2Count"))
                                        For l = 1 To CInt(Session("Dim3Count"))
                                            GraphCount = GraphCount + 1

                                            If Not Session("LabelChanged") Then 'JAM 2015-04-19 
                                                'JAM 2014-12-09 Adding Set Legends
                                                Session("SetLegends")(GraphCount) = Session("StrVARN")(i) + IIf(Session("StrGR1N")(j) = "", "", ",") + Session("StrGR1N")(j) + IIf(Session("StrGR2N")(k) = "", "", ",") + Session("StrGR2N")(k) + IIf(Session("StrGR3N")(l) = "", "", ",") + Session("StrGR3N")(l)
                                            Else
                                                Session("SetLegends")(GraphCount) = Session("StrVARN3")(i) + IIf(Session("StrGR1N")(j) = "", "", ",") + Session("StrGR1N")(j) + IIf(Session("StrGR2N")(k) = "", "", ",") + Session("StrGR2N")(k) + IIf(Session("StrGR3N")(l) = "", "", ",") + Session("StrGR3N")(l)
                                            End If


                                            For m = 1 To CInt(Session("ScenCount"))
                                                'JRS 2015-01-26
                                                '''''''''''''''''''''''''''''''''''
                                                sVar2 = Session("StrVARN")(i)
                                                idx2 = IfsVars.Item(sVar2).Index

                                                'JAM 2018-09-08, suggested code by Jose
                                                If idx2 = -1 Then
                                                    idx2 = 0
                                                End If




                                                If VDim1(idx2) > 0 Or idx2 = 0 Then
                                                    sDim1 = Session("StrGR1N")(j)
                                                Else
                                                    sDim1 = ""
                                                End If

                                                If VDim2(idx2) > 0 Or idx2 = 0 Then
                                                    sDim2 = Session("StrGR2N")(k)
                                                Else
                                                    sDim2 = ""
                                                End If

                                                If VDim3(idx2) > 0 Or idx2 = 0 Then
                                                    sDim3 = Session("StrGR3N")(l)
                                                Else
                                                    sDim3 = ""
                                                End If

                                                Spivot = Session("StrVARN")(i) + sDim1 + sDim2 + sDim3 + Session("StrSce")(m)
                                                ''''''''''''''''''''''''''''''''''''''

                                                ''Spivot = Session("StrVARN")(i) + Session("StrGR1N")(j) + Session("StrGR2N")(k) + Session("StrGR3N")(l) + Session("StrSce")(m)

                                                'JAM 2015-01-25 Checking that it's a valid index
                                                If (FullDimm.Contains(Spivot)) Then
                                                    idx = FullDimm(Spivot)
                                                    For aa = 0 To MaxYears - 1  'JAM 2015-12-11 Adding third dimmension for years
                                                        'Session("IndexbyGroup")(m, GraphCount) = Session("PLOT")(Session("PYear"), idx) 'JAM 2014-12-07 Changing if there are more years selected
                                                        Session("IndexbyGroup")(m, GraphCount, aa) = Session("PLOT")(aa, idx) 'JAM 2014-12-07 Changing if there are more years selected
                                                    Next

                                                End If
                                            Next


                                        Next
                                    Next

                                Next
                            Next
                        End If


                    End If




                End If



            End If


        End If



    End Sub

    Private Function getSubsetLegend(ByVal a As Integer, ByVal i As Integer, ByVal i2 As Integer) As String
        'a is the index for the year
        'i is the index for the variable

        Dim firstdata As Integer, firstdata2 As Integer
        Dim SetLegend As String
        Dim v, d1, d2, d3 As String
        SetLegend = ""
        If Session("SplitGraph") = 1 And Session("NumYearsSel") = 1 Then
            getSubsetLegend = ""
            Exit Function
        Else
            If i = 0 Then
                firstdata = 1
            Else
                firstdata = (Session("ItemsbySet") * i) + 1
            End If
            'jrs 2011/10/14
            firstdata2 = (Session("ItemsbySet") * i2) + 1

            If Not (Session("oVARN0")(Session("NewIndex")(firstdata - 1)) Is Nothing) And Not (Session("oVARN0")(Session("NewIndex")(firstdata)) Is Nothing) Then
                If Session("oVARN0")(Session("NewIndex")(firstdata - 1)) <> "" And Session("oVARN0")(Session("NewIndex")(firstdata - 1)) = Session("oVARN0")(Session("NewIndex")(firstdata)) Then
                    'jrs 2011/10/14
                    If Session("oVARN0")(Session("NewIndex")(firstdata - 1)) <> Session("oVARN0")(Session("NewIndex")(firstdata2 - 1)) Then
                        SetLegend = Session("oVARN0")(Session("NewIndex")(firstdata - 1))
                    Else
                        v = Session("oVARN0")(Session("NewIndex")(firstdata - 1))
                    End If
                End If
            End If

            If Not (Session("GR1N")(Session("NewIndex")(firstdata - 1)) Is Nothing) And Not (Session("GR1N")(Session("NewIndex")(firstdata)) Is Nothing) Then
                If Session("GR1N")(Session("NewIndex")(firstdata - 1)) <> "" And Session("GR1N")(Session("NewIndex")(firstdata - 1)) = Session("GR1N")(Session("NewIndex")(firstdata)) Then
                    'jrs 2011/10/14
                    If Session("GR1N")(Session("NewIndex")(firstdata - 1)) <> Session("GR1N")(Session("NewIndex")(firstdata2 - 1)) Then
                        SetLegend = SetLegend + Session("GR1N")(Session("NewIndex")(firstdata - 1))
                    Else
                        d1 = Session("GR1N")(Session("NewIndex")(firstdata - 1))
                    End If
                End If
            End If

            If Not (Session("GR2N")(Session("NewIndex")(firstdata - 1)) Is Nothing) And Not (Session("GR2N")(Session("NewIndex")(firstdata)) Is Nothing) Then
                If Session("GR2N")(Session("NewIndex")(firstdata - 1)) <> "" And Session("GR2N")(Session("NewIndex")(firstdata - 1)) = Session("GR2N")(Session("NewIndex")(firstdata)) Then
                    'jrs 2011/10/14
                    If Session("GR2N")(Session("NewIndex")(firstdata - 1)) <> Session("GR2N")(Session("NewIndex")(firstdata2 - 1)) Then
                        SetLegend = SetLegend + Session("GR2N")(Session("NewIndex")(firstdata - 1))
                    Else
                        d2 = Session("GR2N")(Session("NewIndex")(firstdata - 1))
                    End If
                End If
            End If

            'JAM 2009-01-19 Commented because the third dimension pattern will be defined
            If Not (Session("GR3N")(Session("NewIndex")(firstdata - 1)) Is Nothing) And Not (Session("GR3N")(Session("NewIndex")(firstdata - 1) + 1) Is Nothing) Then
                If Session("GR3N")(Session("NewIndex")(firstdata - 1)) <> "" And Session("GR3N")(Session("NewIndex")(firstdata - 1)) = Session("GR3N")(Session("NewIndex")(firstdata - 1) + 1) Then
                    'jrs 2011/10/14
                    If Session("GR3N")(Session("NewIndex")(firstdata - 1)) <> Session("GR3N")(Session("NewIndex")(firstdata2 - 1)) Then
                        SetLegend = SetLegend + Session("GR3N")(Session("NewIndex")(firstdata - 1))
                    Else
                        d3 = Session("GR3N")(Session("NewIndex")(firstdata - 1))
                    End If
                End If
            End If

            If Session("NumYearsSel") > 1 Then
                SetLegend = SetLegend & "(" & CStr(Session("FirstYear") + a - 1) & ")"
            End If

            If SetLegend = "" Then
                SetLegend = v & d1 & d2 & d3 & firstdata.ToString
            End If
            getSubsetLegend = SetLegend
            Exit Function
        End If
    End Function

    Private Function getCommonPointName(ByVal ii As Integer, ByVal ii2 As Integer) As String
        Dim VN, d1, d2, d3, Legend As String
        VN = LTrim(RTrim(Session("VARN")(0, ii)))
        d1 = LTrim(RTrim(Session("FDNG1")(ii)))
        d2 = LTrim(RTrim(Session("FDNG2")(ii)))
        d3 = LTrim(RTrim(Session("FDNG3")(ii)))

        If Session("SplitGraph") > 1 Then
            'remove un-common items to display in radial chart
            If VN <> LTrim(RTrim(Session("VARN")(0, ii2))) Then
                VN = ""
            End If
            If d1 <> LTrim(RTrim(Session("FDNG1")(ii2))) Then
                d1 = ""
            End If
            If d2 <> LTrim(RTrim(Session("FDNG2")(ii2))) Then
                d2 = ""
            End If
            If d3 <> LTrim(RTrim(Session("FDNG3")(ii2))) Then
                d3 = ""
            End If



            'JAM 2014-08-30 Resetting Values
            If (VN = "" And d1 = "" And d2 = "" And d3 = "") Or (Session("SelScenarioLabel") And d1 = "" And d2 = "" And d3 = "") Then

                VN = Session("VNFirst")(ii)
                d1 = Session("VNd1")(ii)
                d2 = Session("VNd2")(ii)
                d3 = Session("VNd3")(ii)

                'remove un-common items to display in radial chart
                If VN <> Session("VNFirst")(ii2) Then
                    VN = ""
                End If
                If d1 <> Session("VNd1")(ii2) Then
                    d1 = ""
                End If
                If d2 <> Session("VNd2")(ii2) Then
                    d2 = ""
                End If
                If d3 <> Session("VNd3")(ii2) Then
                    d3 = ""
                End If

            End If
        End If

        Legend = VN
        If Len(d1) > 0 Then
            If Len(Legend) > 0 Then
                Legend = Legend & "(" & d1
            Else
                Legend = d1
            End If
        End If

        If Len(d2) > 0 Then
            If Len(d1) > 0 Then
                Legend = Legend & "," & d2
            ElseIf Len(Legend) > 0 Then
                Legend = Legend & "(" & d2
            Else
                Legend = Legend & d2
            End If
        End If

        If Len(d3) > 0 Then
            If Len(d1) > 0 Or Len(d2) > 0 Then
                Legend = Legend & "," & d3
            ElseIf Len(Legend) > 0 Then
                Legend = Legend & "(" & d3
            Else
                Legend = Legend & d3
            End If
        End If

        If InStr(Legend, "(") > 0 Then
            Legend = Legend & ")"
        End If



        getCommonPointName = Legend



    End Function

End Class

