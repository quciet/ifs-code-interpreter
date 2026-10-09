Option Strict Off
Option Explicit On
Option Compare Text

Imports VB = Microsoft.VisualBasic
'jrs 2022/12/14
Imports System.Data.SQLite

'Namespace IFsWeb

Partial Class frm_VarSel
    Inherits Page
#Region " Web Form Designer Generated Code "
    Public Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        InitializeComponent()

        lstVarName.EnableViewState = False

        Dim AddName As String
        Dim ICount As Short

        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim TotalColWidth As Object
            Dim ColWidth As Short
            Dim RowHeight As Object
            Dim Msg As String


            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2005/08/18
            CalcType = "None"
            Session("CalcType") = CalcType

            'jrs 2014/03/20
            If Session("DoingHistory") = Nothing Then
                Session("DoingHistory") = False
            End If

            FrameProgress.Visible = False
            ReDim VTracker(NumbVars)


            'jrs 2005/10/26
            'jrs 2005/08/22
            If Not IsNothing(Session("LoadSearchVar")) Then
                FlagGroup = Session("FlagGroup")
                FlagGList = Session("FlagGList")
                GroupOn = Session("GroupOn")
                Ag_On_Group = FlagGroup
                'jrs 2008/09/03
                FlagGroupDec = Session("FlagGroupDec")
            Else
                If Session("FlagGroup") Or Session("FlagGList") Or Session("FlagGroupDec") Then
                    Select Case True
                        Case Session("FlagGroup")
                            optGroup.Checked = True
                                'JAM 2011-12-03
                                'optGroup_CheckedChanged(Me, New EventArgs)
                        Case Session("FlagGroupDec")
                            optGroupDec.Checked = True
                                'JAM 2011-12-03
                                'optGroupDec_CheckedChanged(Me, New EventArgs)
                        Case Session("FlagGList")
                            optGList.Checked = True
                            'JAM 2011-12-03
                            'optGList_CheckedChanged(Me, New EventArgs)
                    End Select
                Else
                    'jrs 2008/09/25
                    cmdComputations.Enabled = True

                    FlagGroup = False
                    Ag_On_Group = False
                    'JRS 03/13/04
                    Session("FlagGroup") = FlagGroup
                    'jrs 2005/08/18
                    GroupOn = False
                    Session("GroupOn") = GroupOn
                    FlagGList = False
                    Session("FlagGList") = FlagGList
                    'jrs 2008/09/03
                    FlagGroupDec = False
                    Session("FlagGroupDec") = FlagGroupDec
                End If
            End If

            Carret = "<br>" & Chr(10)

            If CBool(Session("SensitivityFlag")) = True Then
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Select Output Variable(s) for Sensitivity Analysis"
                lstFiles.Visible = False
                lbl_files.Visible = False
            Else
                WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Full Variable/Parameter Selection"
                'jrs 2008/09/04 but_GroupRegToggle.Visible = True
                lstFiles.Visible = True
                lbl_files.Visible = True
            End If
            '
            '   Form Procedure Description and Variable Definition
            '   --------------------------------------------------

            ' Variables Accessible, Controlled by Session("COUNTE") (dimensioned to maximum of 40)

            ' Session("VARN")$() contains the variable names chosen, and defns
            ' Session("GR1N")$() contains the first sub-dimensions
            ' Session("GR2N")$() contains the second sub-dimensions
            ' Session("GR3N")$() contains the units label
            ' VarPointer   contains the keys to the data file location
            ' FileNum contains the number of the data file
            ' Session("FinYear")() contains final year of the data file - used when display

            ' Variables Accessible by Session("SEL") (to maximum of NumbVars, numb of variables):
            ' V$() holds variable names
            ' VDef$() holds variable definitions

            '  Assorted
            ' GRPS$(sub, elem) holds all subdimension element names
            ' KNT carries total number of subdimensions
            ' MaxVal( ) holds maximum size of each subdimension, valid elements
            ' MaxCom( ) holds maximum size of each subdimension for COMMON

            ' the following is a description of the general procedure:

            '  cmd_Varname_Click reads variable name and determines if there are
            '     subdimensions
            '  cmb_Dim1_Click handles 1 or 2 subdimensions
            '  Sub DimLoad loads Combo Box with subdimension information
            '  Sub GridPut places the variable name, dimensions
            '     in the grid for user to see
            '  Sub RetrValue access data from file, especially upon unload of form
            '  Sub Form_Unload saves data in Session("PLOT") for display purposes

            'TableName$ = valtbl.Fields("Table")
            'GroupName$ = valtbl.Fields("Group")
            'VarName$ = valtbl.Fields("Variable")
            'Series$ = valtbl.Fields("Series")
            'Cohort$ = valtbl.Fields("Cohort")
            'Definition$ = valtbl.Fields("Definition")
            'If GroupName$ = GroupSelect$ Or cmb_VCAT.ListIndex = 0 Then
            '    If Series$ = "Yes" Or Cohort$ = "Yes" Then
            '        If ModelLoad$ = "LoadCoVaTrA" Or GroupName$ <> "Automobile" Then
            '            AddName$ = VarName$ + " - " + Definition$
            '            cmb_VarName.AddItem AddName$
            '        End If
            '    End If
            'End If
            'valtbl.MoveNext
            'If valtbl.EOF = True Then Exit Do


            '
            '   Load Variable Name Combo Box
            '   ----------------------------
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

            lstVarName.Items.Clear()
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRS 02/24/04
            txtVarSearch.Text = ""
            ICount = 0
            'JRS 03/13/04
            If Not Request.QueryString("selCommand") Is Nothing Then
                cmb_vcat_SelectedIndexChanged(Request.QueryString("selCommand"))
                sendAJAXResponse(dataString)
            Else
                If Not Session("VarSelVCat") Is Nothing Then
                    cmb_vcat_SelectedIndexChanged(Session("VarSelVcat"))
                Else
                    Session("VarSelVCat") = "All"
                    cmb_vcat_SelectedIndexChanged()
                End If

                '    For i2 = 1 To NumbVars
                '        AddName = V(i2)
                '        'JRS 08/26/03
                '        AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
                '        If Session("SensitivityFlag") <> "True" Or VType(i2) = 1 Then
                '            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                '                'JRS 1/18/02 DISPLAY If CoVaTrA$ = True Or VCat(i2) <> 9 Then

                '                lstVarName.Items.Add(New ListItem(AddName))
                '                ICount = ICount + 1
                '                VTracker(ICount) = i2
                '            End If
                '        End If
                '        'Strip out automobile variables except for GM
                '    Next i2
            End If

            '   Load Variable Session("Category") Combo Box
            '   --------------------------------
            lbl_vcat.Text = "To Show a Subset of Names:"
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRS 02/25/04
            'If cmb_VCAT.SelectedIndex >= 0 Then
            '    cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text = ""
            'End If
            'jrs 2005/04/29 cmb_VCAT.Items.Add(New ListItem(""))
            'jrs 2005/02/12
            'KIA 09/22/2009 comment until you find end comment. A simple question here. Even if the categories were loaded from the file why they were not saved in any array
            'so that they can be used later from that array? Wheneve the list box is needed to load the categories every time it is going
            'to server hard disk , opening file, closing it which I think is a really severe performance issue for a web application.
            'Dim fNum As Integer
            'fNum = FreeFile()
            'FileOpen(fNum, DataFileDir & VCATFile, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            'Input(fNum, nvcat)
            ''UPGRADE_WARNING: Couldn't resolve default property of object nvcat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'For i2 = 1 To nvcat
            '    Input(fNum, AddName)
            '    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or AddName <> "Transportation" Then cmb_VCAT.Items.Add(New ListItem(AddName))
            '    'JRS 1/18/02 DISPLAY If CoVaTrA$ = True Or _
            '    ''AddName$ <> "Transportation" Then cmb_VCAT.AddItem AddName$
            'Next i2

            'FileClose(fNum)
            'KIA 09/22/2009 end comment
            'KIA 09/22/2009 add the following line
            LoadCategories()
            '
            '
            '   Hide Dimension 1 Combo Box
            '   --------------------------
            cmb_DIM1.Visible = False
            lstDIM1.Visible = False
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRS 02/25/04
            If cmb_DIM1.SelectedIndex >= 0 Then
                cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = ""
            End If
            SaveDim1 = ""
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_DIM1.Items.Clear()
            lstDIM1.Items.Clear()

            '   Set up labels
            '   -------------
            lbl_Dim1.Visible = False
            Msg = "Type name or "
            Msg = Msg & "click on name; then touch Enter key"
            lbl_VarName.Text = Msg

            'mti Validation changes
            lbl_Dim1.ForeColor = System.Drawing.ColorTranslator.FromOle(QBColor(0))
            'mti Validation changes


            lbl_Dim1.Text = "Click arrow to activate dimension box; type of click name; touch enter"
            Msg = "Files Available for Display (the active file is in the Text Box):"
            lbl_files.Text = Msg

            '   Grid Setup
            '   ----------

            RowHeight = 230
            ColWidth = HalfInch
            TotalColWidth = ColWidth * 5
            'JRS 10/29/04
            grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

            '   Grid Setup
            '   ----------
            '      RowHeight = 230
            '      ColWidth% = HalfInch
            '      TotalColWidth = ColWidth% * 5
            '      grd_VariableDisplay.Rows = 4
            '      grd_VariableDisplay.Cols = MaxVariables
            '      grd_VariableDisplay.FixedCols = 0
            '      grd_VariableDisplay.FixedRows = 0
            '      grd_VariableDisplay.Visible = True
            '      For cc% = 0 To grd_VariableDisplay.Cols - 1
            '        For rr% = 0 To grd_VariableDisplay.Rows - 1
            '        grd_VariableDisplay.Col = cc%
            '        grd_VariableDisplay.row = rr%
            '        Next rr%
            '      Next cc%

            GridRefresh()

            GridFull = "False"

            '   Initialize selection
            '   --------------------
            SubDimDone = "False"
            SEL1 = ""

            '   Initialize calculations and sequences
            '   -------------------------------------
            CalcRunning = False
            'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CalcSave = "None"
            'UPGRADE_WARNING: Couldn't resolve default property of object SeqOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SeqOn = "False"
            'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SeqRepeat = 0

            '   Establish File Text Box
            '    -----------------
            Msg = Trim(Str(0)) & " Working File, based on "
            Msg = Msg & Session("WorkingFile")
            ' JRS 11/24/01 DISPLAY Txt_files.Text = msg$
            ' JRS 11/24/01 DISPLAY Txt_files.Enabled = False
            'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DefaultFile = 0

            'JRS Begin, Initialize flag for Currency Convertion
            bChkCurr = False
            CurrencyFct = 1
            '    If sOriginalSym = "" Then sOriginalSym = "$"
            'JRS End Initialize flag for Currency Convertion

            'JRS 10/08/02 frm_VarSel.Show
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRSVBNET If frm_VarSel.Visible Then WinFormsToWeb_FocusControl.Focus("cmb_VarName")
            'JRS 11/24/01 DISPLAY
            LoadFiles()
            'JRS 04/25/02
            NeedFileWrite = False

            bCompVisible = True
            'JRS 02/25/04 SAVE STATE *************************************
            Session("SubDimDone") = SubDimDone
            Session("DefaultFile") = DefaultFile
            If Session("VarSelCheck") Is Nothing Then
                Session("VarSelCheck") = "mnuVarSelOptShowVarPar"
            End If
            If Session("VarSelCheckHis") Is Nothing Then
                Session("VarSelCheckHis") = "mnuHisVarOptSelForHisVar"
            End If
            'jrs 2005/08/23
            If Session("ShowVarDispList") Is Nothing Then
                Session("ShowVarDispList") = False
            End If
            Session("VTracker") = VTracker

            Session("VarSelbCompVisible") = bCompVisible
            Session("SEL1") = SEL1

            'jrs 2009/01/27
            If Session("bExcTot") Is Nothing Then
                Session("bExcTot") = True
            End If
            '***************************************************************

            'JRS 06/14/04 COMPUTATIONS
            If Session("bComputation") Then
                SelectVar()
            End If
            'jrs 09/22/04
            Session("NameDispParent") = "frm_Varsel.aspx"
            'jrs 2005/08/22
            ''jrs 2005/05/05
            'If Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar" Then
            '    Session("VarSelCheckHis") = "mnuHisVarOptSelForHisVar"
            '    Session("lstVarNameSelectedIndex") = Nothing
            'End If
            'jrs 2005/08/23
            bLoadComp = False
            'jrs 2022/12/16
            Session("mnuGroupAggEnable") = False
        Else
            LoadCategories()
            'JRS 02/25/04 loading state
            SubDimNum = Session("SubDimNum")
            DimNum = Session("DimNum")
            SubDimDone = Session("SubDimDone")
            VPTR = Session("VPTR")
            DefaultFile = Session("DefaultFile")
            FileNum = Session("FileNum")
            'JRS 03/04/09
            Label1 = Session("Label1")
            'jrs 2005/05/23
            Label3 = Session("Label3")
            'JRS 03/13/04
            GridRefresh()
            FlagGroup = Session("FlagGroup")
            'jrs 2005/08/17
            FlagGList = Session("FlagGList")
            'jrs 2008/09/03
            FlagGroupDec = Session("FlagGroupDec")

            GroupOn = Session("GroupOn")
            CalcType = Session("CalcType")
            VTracker = Session("VTracker")

            GMAX1 = Session("GMAX1")
            'jrs 2014/03/20 DoingHistory = Session("DoingHistory")

            TableName = Session("TableName")
            VarName = Session("VarName")
            Ag_rule = Session("Ag_rule")
            Definition = Session("Definition")

            SDimNum = Session("SDimNum")
            SDimPoint = Session("SDimPoint")
            GroupPoint = Session("GroupPoint")

            bCompVisible = Session("VarSelbCompVisible")
            SEL1 = Session("SEL1")

            'jrs 2005/08/18
            CalcType = Session("CalcType")
            'jrs 2005/11/11
            rsGL1 = Session("rsGL1")
            sDim_1 = Session("sDim_1")
            sDim_2 = Session("sDim_2")
            sDim_3 = Session("sDim_3")
            idxDim1 = Session("idxDim1")
            idxDim2 = Session("idxDim2")
            idxDim3 = Session("idxDim3")
            'jrs 2006/09/19
            sVar = Session("sVar")
            bDim2 = Session("bDim2")
            bDim3 = Session("bDim3")
            bVList = Session("bVlist")

            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                'JAM 2014-05-18
                ''LoadVarName(("HistoricAnalog"))
                'JAM 2014-05-17
                FillSearchArray(("HistoricAnalog"))
            End If

            'JAM 2014-04-05
            'JAM 2014-05-15 Decommented
            ''cmb_vcat_SelectedIndexChanged()


            'JAM 2014-05-22
            'FillSearchArray(("Full"))

            'jam 2021/11/10 review
            'cdv 2021/05/06 If the user checks displist load that data by default
            If Session("VarSelCheck") = "mnuVarSelOptShowVarDispList" Then
                FillSearchArray(("Lists"))
            Else
                FillSearchArray(("Full"))
            End If


        End If

        'JAM 2011-11-18 Defining default button for txtVarSearch field
        txtVarSearch.Attributes.Add("onkeypress", "return clickButton(event,'" + btnInvs.ClientID + "')")


        'JAM 2014-04-04
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")

        'JAM 2014-05-18
        'If Not Session("VEmpOrForeMode") Is Nothing Then
        '    If Session("VEmpOrForeMode") = 0 Then
        '        cmb_VCAT.Attributes.Add("onchange", "Forcmb_VCAT_click();")
        '    Else
        '        cmb_VCAT.Attributes.Add("onchange", "Empcmb_VCAT_click();")

        '    End If
        'End If

        'JRS 06/08/04
        'cmb_VarName.Attributes.Add("onkeyup", "update(this,this);")
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub
    'CONVERSION_NOTE: Method 'Dispose' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'Public Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
    'If Disposing Then 
    'If Not components Is Nothing Then 
    'components.Dispose()
    'End If 
    'End If 
    'MyBase.Dispose(Disposing)
    'End Sub  
    'CONVERSION_NOTE: Field 'components' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1001.html'.
    'public components As System.ComponentModel.IContainer 
    'CONVERSION_NOTE: Class ''JRSVBNET System.Windows.Forms.ToolTip' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public ToolTip1 As 'JRSVBNET System.Windows.Forms.ToolTip 
    'CONVERSION_NOTE: Class 'AxMSComctlLib.AxProgressBar' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET ProgressBar1 As AxMSComctlLib.AxProgressBar 
    'CONVERSION_NOTE: Class 'AxMSComDlg.AxCommonDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Public WithEvents 'JRSVBNET CMDialog1 As AxMSComDlg.AxCommonDialog 
    Protected WithEvents lbtExplain As System.Web.UI.WebControls.LinkButton
    <System.Diagnostics.DebuggerStepThrough()> Public Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'public Shared m_vb6FormDefInstance As frm_VarSelPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_VarSel
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_VarSel()
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
    '------------------------------------------Varsel.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last changed by BBH on July 17, 20002 - added MTI's validation code

    'mti Validation changes
    Dim bPickingValidation As Boolean
    Dim sPickHRegion As String
    Dim iPickRegionCounter As Short
    'mti Validation changes

    Dim X As Short
    Dim tblGDP As DataTable
    Dim tblPOP As DataTable
    Dim testint As Object
    Dim i As Short, j As Short
    Dim Y As String
    'jrs 2013/03/20 Dim DoingHistory As Boolean

    Dim FlagGroup As Boolean
    'JRS 2005/07/08
    Dim FlagGList As Boolean
    'jrs 2008/09/03
    Dim FlagGroupDec As Boolean

    Dim i2, GMAX1 As Object
    'jrs 2005/08/19
    Public VPTR As Object
    Dim EleLoop As Short 'Session("SEL")
    Dim Code, SEL1 As String
    'jrs 2005/10/13
    Public SubDimDone As String
    Public SubDimNum As Short 'number of subdimensions
    Public DimNum As Short 'latest (and possibly 1st) subdimension number
    Dim SDimNum As Short 'first subdimension number where more than 1 dimension
    Dim DimPoint As Short 'latest (and possibly 1st) subdimension pointer (i.e. element #)
    Dim SDimPoint As Short 'first subdimension pointer (i.e, element #) when more than 1 dimension
    Dim GroupPoint As Short 'pointer to group number when regionalized variable is to be grouped
    Public FileNum As Object
    Dim GridFull, SaveDim1 As String
    Dim DefaultFile As Object
    Dim Label2, VarPointer, Label1, Label3 As Object
    Dim OperStr As String
    Dim CalcSave As Object
    Dim CalcRunning As Boolean 'True means that are running a calculation such as division
    Public CalcType As String 'Saves "None", "DivOn", "AddOn", etc.
    Dim SumOn As Boolean 'If true, are running a sum across all elements of a dimension
    Dim SeqOn, SeqRepeat As Object
    Dim GroupOn As Boolean 'If true, are running sums across all regions in groups
    Dim GroupDone As Boolean 'If true, have finished group and ready to finalize aggregation
    Dim GroupMemb(200) As Object
    Dim WeightVarPoint(200) As Object
    Dim GroupMembCount As Object
    Dim AgPoint As Object
    Dim VTracker() As Object
    Dim dataString As String
    Dim VarGroupNames(100) As String
    Dim Countries() As Object
    Dim Regions() As Object
    Dim Ag_On_Region As Object
    Dim Ag_On_Group As Boolean 'If true, doing history file
    Dim RegionTot As Object
    Dim GroupTot As Short
    Dim VarName, Definition As String
    'JRS 2005/11/16
    Public TableName As String
    Public Ag_rule As String
    'formula in history
    Dim NewVal As Object
    Dim SubOn, MulOn, DivOn, AddOn, NumVal As Object
    Dim AddOnPrev, DivOnPrev, MulOnPrev, SubOnPrev As Object
    Dim bChkCurr As Boolean 'True if Currency Conversion already Checked
    Dim bCnvrtCurr As Boolean 'True if Currency Conversion is ON
    Dim CurrencyFct As Single 'Stores the Currency Factor for Conversion
    'Declares and opens a RecordSet with configuration values
    'jrs 2005/12/09 Dim rsCurr As DataTable
    'JRS 12/04/01 DISPLAY
    Dim rsList As DataTable
    Dim iDimPointDim1 As Short
    'JRS 4/25/02
    Dim NeedFileWrite As Boolean
    'JRS 04/14/03
    Public bCompVisible As Boolean
    'JRS 01/28/04
    Dim seriesyears() As Object
    Dim valtbl As DataTable
    'jrs 2005/08/17
    Dim rsGL1 As DataTable
    Dim SaveDim2 As String
    Dim idxGL1 As Integer = 0
    'jrs 2005/08/23
    Public bLoadComp As Boolean
    'jrs 2005/11/28
    Dim bLoadCompHistAnal As Boolean
    'jrs 2005/12/06
    Dim CurrencyFctAll As Single
    'jrs 2006/04/07
    Dim sDim_1 As String, sDim_2 As String, sDim_3 As String 'jrs 2006/05/16 Dim3
    Dim idxDim1 As Integer, idxDim2 As Integer, idxDim3 As Integer 'jrs 2006/05/16 Dim3
    'jrs 2006/09/12
    Dim sVar As String
    Dim bDim2 As Boolean, bDim3 As Boolean 'flags to avoid loading dimensions when not necessary
    Dim bVList As Boolean 'identifies if it's a Variable list.



    '=============================VarSel.Frm==============================
    Public Sub but_File_Click()
        Dim Txt_files As Object
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/24/01 DISPLAY - REMOVED
        'UPGRADE_WARNING: Couldn't resolve default property of object Txt_files.Text. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If InStr(Txt_files.Text, "History") > 0 Then
            Msg = "You may only change files for forecasts."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

        '   Alter File

        'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If DefaultFile + 1 <= Session("FilesSelected") Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Txt_files.Text. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Txt_files.Text = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DefaultFile = DefaultFile + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object Txt_files.Text. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Txt_files.Text = Str(DefaultFile) & " " & Session("RunFiles")(DefaultFile)

            'Added by MTI on 24 May 00  'Disabled because DefaultFile<=0 gives error

            '        If InStr(StrConv(RunFiles$(DefaultFile), vbUpperCase), "IFSBASEHIST.RUN") > 0 Then
            '            Session("FirstYear")% = FirstYearHistory%
            '            Session("LastYear")% = LastYearHistory%
            '            HistoricBase$ = True
            '            ActiveBase$ = "IFSBASEHIST.RUN"  'IFsHist_MDB2
            '        Else
            '            Session("FirstYear")% = FirstYearForecast%
            '            Session("LastYear")% = LastYearForecast%
            '            HistoricBase$ = False
            '            ActiveBase$ = "IFSBASE.RUN" 'IFs_MDB2
            '        End If
            'Added by MTI on 24 May 00 -Upto this

        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Txt_files.Text. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Txt_files.Text = ""
            Msg = Str(0) & " Working File, based on "
            Msg = Msg & Session("WorkingFile")
            'UPGRADE_WARNING: Couldn't resolve default property of object Txt_files.Text. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Txt_files.Text = Msg
            'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DefaultFile = 0
        End If

        'JRS 02/25/04
        Session("DefaultFile") = DefaultFile
    End Sub

    'KIA 09/22/2009 take the following method
    Private Function LoadCategories()
        Dim catFound As Boolean
        Dim i, j, pos1 As Integer
        Dim groups As Array
        Dim tmpName As String
        cmb_VCAT.Items.Clear()

        For i = 1 To UBound(VCatName)
            catFound = False
            For j = 0 To cmb_VCAT.Items.Count - 1
                If VCatName$(i) = cmb_VCAT.Items(j).Value Then
                    catFound = True
                    Exit For
                End If
            Next j

            If Not catFound Then

                If InStr(VCatName$(i), ",") = 0 Then
                    cmb_VCAT.Items.Add(VCatName$(i))
                End If

                groups = Split(VCatName$(i), ",")
                pos1 = 0

                While pos1 <= UBound(groups)
                    tmpName = Trim(groups(pos1))
                    pos1 = pos1 + 1
                    catFound = False
                    For j = 0 To cmb_VCAT.Items.Count - 1
                        If UCase(tmpName) = UCase(cmb_VCAT.Items(j).Value) Then
                            catFound = True
                            Exit For
                        End If
                    Next j
                    If Not catFound Then
                        cmb_VCAT.Items.Add(tmpName)
                    End If
                End While
            End If
        Next i
        cmb_VCAT.Items.Insert(0, "All")
        SortCategories()
    End Function



    Private Sub lstFiles_SelectedIndexChanged(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'JRS 11/24/01 DISPLAY
        DefaultFile = lstFiles.SelectedIndex
        'jrs 2006/09/26
        ResetDisplay()
        'JRS 02/25/04
        Session("DefaultFile") = DefaultFile

        'jrs 2011/11/04
        Session("txtFiles") = txtFiles.Value

        'jrs 2006/09/20
        ''jrs 2005/04/28
        'If Not Session("lstVarNameSelectedIndex") Is Nothing Then
        '    Session("VarClick") = True
        'Else
        '    Session("VarClick") = False
        'End If
    End Sub

    'jrs 2006/09/26
    'this routine is needed because the combo triggers itself when other events are invoked (ex. lstfiles_click)
    Private Sub ResetDisplay()
        'JRS 2005/02/17
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lbl_VarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        'jrs 2006/09/20
        lstFiles.Visible = True
        lbl_files.Visible = True
        'jrs 2012/06/18
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    cmb_VCAT.Visible = False
        '    lbl_vcat.Visible = False
        'Else
        '    cmb_VCAT.Visible = True
        '    lbl_vcat.Visible = True
        'End If
    End Sub

    'JRS 01/28/04
    Public Sub DimError(ByVal Sel2 As String)
        Dim Msg As String
        Msg = Sel2 & " is an incorrect sub-dimension choice"

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '=============================VarSel.Frm==============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.KeyPress.html'.
    'Public Sub cmb_DIM1_KeyPress()
    '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    Dim KeyAscii As Short 'JRSVBNET = Asc(eventArgs.KeyChar)
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    If KeyAscii = 13 Then Call cmb_DIM1_SelectedIndexChanged(cmb_DIM1, New System.EventArgs)
    '    If KeyAscii >= 97 And KeyAscii <= 146 Then
    '        'UPGRADE_ISSUE: Assignment not supported: KeyAscii to a non-zero value Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1058"'
    '        KeyAscii = KeyAscii - 32
    '    End If
    '    'JRS 01/22/03
    '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Keys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    If True Then 'JRSVBNET KeyAscii = 'JRSVBNET System.Windows.Forms.Keys.Escape Then
    '        txtVarSearch.Enabled = True
    '        lstVarName.Enabled = True
    '        lstVarName.Visible = True

    '        cmb_DIM1.Visible = False
    '        lstDIM1.Visible = False
    '        lbl_Dim1.Visible = False
    '        lbl_vcat.Visible = True
    '        cmb_VCAT.Visible = True
    '        lbl_files.Visible = True
    '        lstFiles.Visible = True
    '        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
    '    End If
    '    If KeyAscii = 0 Then
    '        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '        'JRSVBNET eventArgs.Handled = True
    '    End If
    'End Sub




    '=============================VarSel.Frm==============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.ComboBox.KeyPress' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.KeyPress.html'.
    Public Sub cmb_VarName_KeyPress() 'JRSVBNET System.Windows.Forms.KeyPressEventArgs) Handles txtVarSearch.KeyPress
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        Dim KeyAscii As Short 'JRSVBNET = Asc(eventArgs.KeyChar)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If KeyAscii = 13 Then Call cmb_VarName_SelectedIndexChanged()
        If KeyAscii >= 97 And KeyAscii <= 146 Then
            'UPGRADE_ISSUE: Assignment not supported: KeyAscii to a non-zero value Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1058"'
            KeyAscii = KeyAscii - 32
        End If
        If KeyAscii = 27 Then 'Escape
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            Support.FormManager.Close(Me)
        End If
        If KeyAscii = 0 Then
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET eventArgs.Handled = True
        End If
    End Sub

    '=============================VarSel.Frm==============================
    'UPGRADE_WARNING: Event cmb_vcat.SelectedIndexChanged may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2075"
    'KIA 09/22/2009 REPLACE THE FOLLOWING METHOD. Lot of changes have been made due to the new category type and also two new ifs blocks added.
    Private Sub cmb_vcat_SelectedIndexChanged(Optional ByVal catname As String = "")
        Dim AddName As String
        Dim ICount As Short
        Dim Groups As Array
        Dim pos1 As Integer
        'jrs 2012/06/15
        Dim sList As String = "Parameters"
        If Not IsNothing(Session("LoadVarNameType")) Then
            sList = Session("LoadVarNameType")
        End If

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        If catname <> "" Then
            Session("VarSelVCat") = catname

        Else
            catname = Session("VarSelVCat")
        End If

        dataString = ""

        If Session("DoingHistory") Then
            Call History_vcatClick(catname)
        Else

            lstVarName.Items.Clear()
            Carret = "<br>" & Chr(10)
            ICount = 0
            If Session("VarSelVCat") = "All" Then
                For i2 = 1 To NumbVars
                    AddName = V(i2)
                    'JRS 10/13/03
                    AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
                    If (Session("SensitivityFlag") <> "True" Or VType(i2) = 1) And Not VHidePrivateVars(i2) Then 'jrs 2019/07/11 added and Not VHidePrivate
                        'jrs 2012/06/15
                        If (sList = "Variables" And VType(i2) <> 3) Or sList = "Parameters" Or
                        (sList = "HistoricAnalog" And Not IsDBNull(VHistoricAnalog(i2))) Then
                            'jrs 2012/02/27 If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                            dataString = dataString + AddName + "|"
                            lstVarName.Items.Add(New ListItem(AddName))
                            ICount = ICount + 1
                            VTracker(ICount) = i2
                            'jrs 2012/02/27 End If
                        End If
                    End If

                Next i2

            Else

                Dim vname As String = catname
                For i2 = 1 To NumbVars

                    Groups = Split(VCatName(i2), ",")
                    pos1 = 0
                    While (pos1 < Groups.Length)
                        If Trim(Groups(pos1)) = vname Then
                            AddName = V(i2)
                            'JRS 10/13/03
                            AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
                            If Session("SensitivityFlag") <> "True" Or VType(i2) = 1 Then
                                'jrs 2012/06/15
                                If (sList = "Variables" And VType(i2) <> 3) Or sList = "Parameters" Or
                                (sList = "HistoricAnalog" And Not IsDBNull(VHistoricAnalog(i2))) Then
                                    'jrs 2012/02/27 If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                                    dataString = dataString + AddName + "|"
                                    lstVarName.Items.Add(New ListItem(AddName))
                                    ICount = ICount + 1
                                    VTracker(ICount) = i2
                                    'jrs 2012/02/27 End If
                                End If
                            End If
                        End If
                        pos1 += 1
                    End While
                Next i2
            End If
        End If
        ' JRS 03/21/04
        Session("VTracker") = VTracker

        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        cmb_VCAT.Text = catname

    End Sub
    Private Sub sendAJAXResponse(ByVal data As String)

        Response.Clear()
        Response.Write(data)
        Response.ContentType = "text/txt"
        Response.End()
    End Sub
    '   VARVAL, Session("RECORD"), Session("COUNTE"), and VARNUM are in Global Declarations

    '=============================VarSel.Frm==============================
    Private Sub cmd_Clear_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmd_Clear.Click
        Dim rr As Short
        Dim cc As Short
        Dim TotalColWidth As Object
        Dim ColWidth As Short
        Dim RowHeight As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes
        'UPGRADE_ISSUE: ComboBox property cmb_DIM1.Locked was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET cmb_DIM1.Locked = False
        'mti Validation changes

        ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
        Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))


        '    Session("COUNTE") = 0
        '    For i2 = 1 To Session("LastYear")% - Session("FirstYear")% + 1
        '        For ICount% = 1 To MaxVariables
        '            Session("PLOT")(i2, ICount%) = 0
        '        Next ICount%
        '    Next i2
        '    For ILOOP% = 1 To MaxVariables
        '         Session("VARN")$(1, ILOOP%) = ""
        '         Session("VARN")$(2, ILOOP%) = ""
        '         Session("GR1N")$(ILOOP%) = ""
        '         Session("GR2N")$(ILOOP%) = ""
        '         Session("GR3N")$(ILOOP%) = ""
        '    Next ILOOP%

        'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label1 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label2 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label3 = ""
        'JRS 03/04/09
        Session("Label1") = Label1

        '
        '   Grid Setup
        '   ----------
        'UPGRADE_WARNING: Couldn't resolve default property of object RowHeight. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        RowHeight = 230
        ColWidth = Inch
        'UPGRADE_WARNING: Couldn't resolve default property of object TotalColWidth. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        TotalColWidth = ColWidth * 5
        'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Rows is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
        'JRSVBNET grd_VariableDisplay.Rows = 4
        'UPGRADE_WARNING: Couldn't resolve default property of object MaxVariables. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'CONVERSION_NOTE: The Set operation for property AxMSFlexGridLib.AxMSFlexGrid.Cols is invalid.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1011.html'.
        'JRSVBNET grd_VariableDisplay.Cols = MaxVariables
        'CONVERSION_TODO: The equivalent of Property 'AxMSFlexGridLib.AxMSFlexGrid.FixedCols' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET 'JRSVBNET Support.DataGridManager.manager.SetFixedCols(grd_VariableDisplay, 0)
        'CONVERSION_TODO: The equivalent of Property 'AxMSFlexGridLib.AxMSFlexGrid.FixedRows' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET 'JRSVBNET Support.DataGridManager.manager.SetFixedRows(grd_VariableDisplay, 0)
        grd_VariableDisplay.Visible = (Session("COUNTE"))
        'JRS 03/14/04
        GridRefresh()
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_VarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
        ' JRS 11/24/01 DISPLAY but_File.Visible = True

        lbl_files.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
        lstFiles.Visible = True
        'jrs 2012/06/18
        ''jrs 2006/09/20
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    cmb_VCAT.Visible = False
        '    lbl_vcat.Visible = False
        'Else
        '    cmb_VCAT.Visible = True
        '    lbl_vcat.Visible = True
        'End If

        '   Initialize selection
        '   --------------------
        SubDimDone = "False"
        SEL1 = ""
        Session("SEL1") = SEL1

        '   Initialize calculations and sequences
        '   -------------------------------------
        CalcRunning = False
        CalcSave = "None"
        SeqOn = "False"
        SeqRepeat = 0
        'JRS 02/25/04 SAVE STATE ****************************************
        Session("SubDimDone") = SubDimDone
        '****************************************************************
        'jrs 2005/06/07
        Session("TableTitle") = "Title" 'jrs 2008/09/23
        'jrs 2013/02/22
        Session.Remove("data")
        Session.Remove("dataAll")
        Session.Remove("dimAll")
        Session.Remove("grd_VariableDisplay")
        Session.Remove("f")
    End Sub


    '=============================VarSel.Frm==============================
    Public Sub DimLoad()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '       Make Combo Box and Label Visible for Subdimension(s)
        '       ----------------------------------------------------

        'jrs 2012/01/27 this is causing the keyreturn in dim1 not to respond
        cswmWebMenu.AccessorKeyCode = 0

        cmb_DIM1.Visible = True
        lstDIM1.Visible = True
        txtVarSearch.Enabled = False
        lstVarName.Enabled = False
        lstVarName.Visible = False
        lbl_Dim1.Visible = True
        lbl_VarName.Visible = False
        lbl_files.Visible = False
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = False
        lstFiles.Visible = False
        'jrs 2012/06/18
        'lbl_vcat.Visible = False
        'cmb_VCAT.Visible = False
        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = False
        ' JRS 11/24/01 DISPLAY but_File.Visible = False
        cmb_DIM1.Items.Clear()
        lstDIM1.Items.Clear()
        'JRS 02/25/04
        If cmb_DIM1.SelectedIndex >= 0 Then
            cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = ""
        End If

        'mti Validation changes
        lbl_Dim1.ForeColor = System.Drawing.ColorTranslator.FromOle(QBColor(0))
        'mti Validation changes

        If SubDimDone = "True" Then
            'jrs 2006/05/23
            If SubDimNum = 2 Then
                lbl_Dim1.Text = "Please Choose A SECOND Dimension:"
            Else
                lbl_Dim1.Text = "Please Choose A THIRD Dimension:"
            End If
        Else
            lbl_Dim1.Text = "Type or Select Dimension:"
        End If

        '       Load element names into Combo Box
        '       ---------------------------------

        'JRS 03/13/04
        cmb_DIM1.Items.Insert(0, "")

        'jrs 2008/09/03 added or FlagGroupDec
        'jrs 2005/08/17
        If (FlagGroup = True Or FlagGList Or FlagGroupDec) And DimNum = 1 Then 'Set up for groups
            Dim i As Integer
            If FlagGroup Or FlagGroupDec Then 'jrs 2008/09/03 added or FlagGroupDec
                'jrs 2023/07/12
                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM [" & GroupIDNew & "] ORDER BY upper(GroupName)", "GroupName", valtbl)
                'JRS 5/21/02
                For i = 0 To valtbl.Rows.Count - 1
                    cmb_DIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
                    lstDIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
                Next i
                cmb_DIM1.Items.Add(New ListItem("ALL"))
                lstDIM1.Items.Add(New ListItem("ALL"))
            Else
                'jrs 2023/07/12
                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM GListId ORDER BY upper(Description)", "GroupName", valtbl)
                For i = 0 To valtbl.Rows.Count - 1
                    cmb_DIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("Description")))
                    lstDIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("Description")))
                Next i
            End If
        Else 'Proceed with regions or secondary dimensions

            For EleLoop = 1 To MaxVal(DimNum) - 1 'jrs 2009/01/21 Total: added -1

                'jam 2021/11/10
                'cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
                'lstDIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))


                'cdv 2021/02/02 ****START**** 
                '1. Not Gendered, don't change
                '2. bMaleFirst, don't change
                '3. Otherwise, flip the order
                If GRPS(DimNum, EleLoop) <> "Male" And GRPS(DimNum, EleLoop) <> "Female" Then
                    cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
                    lstDIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
                ElseIf bMaleFirst Then
                    cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
                    lstDIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
                ElseIf GRPS(DimNum, EleLoop) = "Male" Then
                    cmb_DIM1.Items.Add(New ListItem("Female"))
                    lstDIM1.Items.Add(New ListItem("Female"))
                Else
                    cmb_DIM1.Items.Add(New ListItem("Male"))
                    lstDIM1.Items.Add(New ListItem("Male"))
                End If
                'cdv 2021/02/02 ****END****


            Next EleLoop

            'jrs 2013/02/26 added And VUnits(SEL) <> "Mul Base 1"
            'jrs 2009/01/21 Total
            If Not (UCase(cmb_DIM1.Items(cmb_DIM1.Items.Count - 1).Text) = "TOTAL" Or DimNum = 1) And VUnits(Session("SEL")) <> "Mul Base 1" _
            And Not VNoTot2(Session("SEL")) Then 'jrs 2014/10/08 added and not VNoTot2
                cmb_DIM1.Items.Add(New ListItem("Total"))
                lstDIM1.Items.Add(New ListItem("Total"))
            End If
            cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
            lstDIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))

        End If

        'mti Validation changes
        If bPickingValidation Then
            If DimNum = 1 Then 'dimnum 1 is region
                'only in case of regions
                cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = sPickHRegion
                'JRSVBNET cmb_DIM1.Locked = True
                lbl_Dim1.ForeColor = System.Drawing.ColorTranslator.FromOle(QBColor(12)) 'light red
                lbl_Dim1.Text = "Press Enter only to select:"
            Else
                'UPGRADE_ISSUE: ComboBox property cmb_DIM1.Locked was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                'JRSVBNET cmb_DIM1.Locked = False
                'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'JRSVBNET *** BEGIN ***
                'If VB6.GetItemString(cmb_DIM1, cmb_DIM1.Items.Count - 1) = "ALL" Then
                '    'Take "ALL" out from dimensions other than reg
                '    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                '    cmb_DIM1.Items.RemoveAt((cmb_DIM1.Items.Count - 1))
                '    'cmb_DIM1.Refresh
                'End If
                'JRSVBNET *** END ***
            End If
        Else
            'UPGRADE_ISSUE: ComboBox property cmb_DIM1.Locked was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'JRSVBNET cmb_DIM1.Locked = False
        End If
        'mti Validation changes


    End Sub

    '=============================VarSel.Frm==============================
    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 7/29/02
        Dim bDispMenu As Boolean
        bDispMenu = Not (Session("SensitivityFlag") = "True")

        'JAM 2014-04-04 Keeping chosen filter
        ''If fSubSetchosen.Value <> "" Then
        ''    Session("VarSelVCatName") = fSubSetchosen.Value
        ''Else
        ''    If Session("VarSelVCatName") Is Nothing Then
        ''        Session("VarSelVCatName") = 0
        ''    Else
        ''        fSubSetchosen.Value = Session("VarSelVCatName")
        ''    End If
        ''End If


        'JAM 2014-05-08 Keeping chosen filter
        If fSubSetchosen.Value <> "" Then
            Session("FiltVarSelVCat") = fSubSetchosen.Value
        Else
            If Session("FiltVarSelVCat") Is Nothing Then
                Session("FiltVarSelVCat") = 0
            Else
                fSubSetchosen.Value = Session("FiltVarSelVCat")
            End If
        End If



        'JAM 2014-04-19 Keeping forecasted or empirical variable set =>0:Forecasted 1:Empirical
        If fEmpOrForeMode.Value <> "" Then
            Session("VEmpOrForeMode") = fEmpOrForeMode.Value
        Else
            If Session("VEmpOrForeMode") Is Nothing Then
                Session("VEmpOrForeMode") = 0
            Else
                fEmpOrForeMode.Value = Session("VEmpOrForeMode")
            End If
        End If

        'JAM 2014-05-31 Keeping Historic plus analog selection 0: Selecte, 1: Not selected
        If fHistoricAnalogMode.Value <> "" Then
            Session("VHistoricAnalogMode") = fHistoricAnalogMode.Value
        Else
            If Session("VHistoricAnalogMode") Is Nothing Then
                Session("VHistoricAnalogMode") = 1
            Else
                fHistoricAnalogMode.Value = Session("VHistoricAnalogMode")
            End If
        End If



        'JRS 9/15/01 COMPUTATIONS
        If cmdComputations.Visible Then
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET Support.FormManager.Close(frm_computations.DefInstance)
            If Session("sOriginForm") = "frm_VarSel" Then
                Session("sOriginForm") = ""
            End If
            'JRS 07/29/02
            If bDispMenu Then
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                'Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")
                ''JRSVBNET frm_DisplayMenu.Show() 
            End If
        End If
        'JRS 9/18/01 COMPUTATIONS
        If Session("bComputation") Then
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.Hide().html'.
            'Me.Hide() 
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            'Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
            'frm_computations.DefInstance.Show() 
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.Activate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Activate().html'.
            'frm_computations.DefInstance.Activate() 
        Else
            'JRS 04/25/02
            If NeedFileWrite Then
                Msg = "IMPORTANT WARNING:  You must manually copy IFsVar.MDB from "
                Msg = Msg & "the local drive ('Runfiles' directory) to the Data directory "
                Msg = Msg & "after exiting IFs.  "
                Msg = Msg & "You may need help and/or permissions to do so.  "
                Msg = Msg & "Failure to do so will cause you to lose changes in "
                Msg = Msg & "Variable/Display Lists upon restarting IFs."

                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            'JRS 07/29/02
            If bDispMenu Then
                'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET frm_DisplayMenu.FillGrid()
            End If
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            Support.FormManager.Close(Me)
        End If

        '    frm_VarSel.Hide
        If bDispMenu Then
            Upd_Disp_Menu()
        End If

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub

    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor
        'JRS 7/29/02
        Dim bDispMenu As Boolean
        bDispMenu = Not (Session("SensitivityFlag") = "True")
        'JRS 9/15/01 COMPUTATIONS
        If cmdComputations.Visible Then
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET Support.FormManager.Close(frm_computations.DefInstance)
            If Session("sOriginForm") = "frm_VarSel" Then
                Session("sOriginForm") = ""
            End If
            'JRS 07/29/02
            If bDispMenu Then
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                'Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")
                ''JRSVBNET frm_DisplayMenu.Show() 
            End If
        End If
        'JRS 9/18/01 COMPUTATIONS
        If Session("bComputation") Then
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.Hide().html'.
            'Me.Hide() 
            'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
            'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
            'Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")
            'frm_computations.DefInstance.Show() 
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.Activate' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Activate().html'.
            'frm_computations.DefInstance.Activate() 
        Else
            'JRS 04/25/02
            If NeedFileWrite Then
                Msg = "IMPORTANT WARNING:  You must manually copy IFsVar.MDB from "
                Msg = Msg & "the local drive ('Runfiles' directory) to the Data directory "
                Msg = Msg & "after exiting IFs.  "
                Msg = Msg & "You may need help and/or permissions to do so.  "
                Msg = Msg & "Failure to do so will cause you to lose changes in "
                Msg = Msg & "Variable/Display Lists upon restarting IFs."

                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            'JRS 07/29/02
            If bDispMenu Then
                'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                'JRSVBNET frm_DisplayMenu.FillGrid()
            End If
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            Support.FormManager.Close(Me)
        End If

        '    frm_VarSel.Hide
        If bDispMenu Then
            Upd_Disp_Menu()
        End If

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub


    Private Sub cmdComputations_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdComputations.Click
        'jrs 2005/12/20 RAM for Formulas
        ''jrs 2004/11/27
        'SessionAllocator(Session("SessionIdx"), Me)

        'jrs 2005/06/20
        If Not pnlContext.Visible And Session("COUNTE") > 0 Then
            Session("VarSelGridCol") = Session("COUNTE")  'jrs 2005/08/05 replace increment of VarSelGridCol for COUNTE
        End If
        'JRS 9/25/01 COMPUTATIONS
        Dim i As Short
        Dim iAns As Short
        Dim sId As String
        sId = "frm_VarSel"
        'jrs 2006/06/27
        Session("ComputationsSelectedIndex") = -1

        If Session("sOriginForm") = "" Or Session("sOriginForm") = sId Then
            Session("sOriginForm") = sId
            Session("grd_VariableDisplay") = grd_VariableDisplay


            'JAM 2014-04-04 Keeping chosen filter
            If fSubSetchosen.Value <> "" Then
                Session("FiltVarSelVCat") = fSubSetchosen.Value
            Else
                If Session("FiltVarSelVCat") Is Nothing Then
                    Session("FiltVarSelVCat") = 0
                Else
                    fSubSetchosen.Value = Session("FiltVarSelVCat")
                End If
            End If

            'JAM 2014-04-19 Keeping forecasted or empirical variable set =>0:Forecasted 1:Empirical
            If fEmpOrForeMode.Value <> "" Then
                Session("VEmpOrForeMode") = fEmpOrForeMode.Value
            Else
                If Session("VEmpOrForeMode") Is Nothing Then
                    Session("VEmpOrForeMode") = 0
                Else
                    fEmpOrForeMode.Value = Session("VEmpOrForeMode")
                End If
            End If

            'JAM 2014-05-31 Keeping Historic plus analog selection 0: Selecte, 1: Not selected
            If fHistoricAnalogMode.Value <> "" Then
                Session("VHistoricAnalogMode") = fHistoricAnalogMode.Value
            Else
                If Session("VHistoricAnalogMode") Is Nothing Then
                    Session("VHistoricAnalogMode") = 1
                Else
                    fHistoricAnalogMode.Value = Session("VHistoricAnalogMode")
                End If
            End If


            'jrs 2005/10/28
            If Session("COUNTE") > 0 Then
                If UBound(Session("aFormulas")) >= Session("VarSelGridCol") + 1 Then
                    If Session("aFormulas")(Session("VarSelGridCol") + 1).Name = Session("VARN")(1, Session("VarSelGridCol") + 1) Then
                        If pnlContext.Visible And optCopyFor.Checked Then
                            Session("CopyFormula") = Session("aFormulas")(Session("VarSelGridCol") + 1).Formula_Renamed
                        End If
                    End If
                End If
            End If

            'jrs 2022/04/22 save files selected
            For ifl = 0 To lstFiles.Items.Count - 1
                Session("PDFilesIdxs")(ifl) = lstFiles.Items(ifl).Selected
            Next
            'Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_computations.aspx")

        Else

            LoadMessage("Another Form is using this option, please close forms that you are not using anymore", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If

    End Sub


    '=============================VarSel.Frm==============================
    'UPGRADE_WARNING: Form event frm_VarSel.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.Form.Activated' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Activated.html'.
    Public Sub frm_VarSel_Activated()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '    DoingHistory = False
        '    but_history.Caption = "Using &Forecast"

        '   Grid Setup
        '   ----------

        '    RowHeight = 230
        '    ColWidth% = HalfInch
        '    TotalColWidth = ColWidth% * 5
        '    grd_VariableDisplay.Rows = 4
        '    grd_VariableDisplay.Cols = MaxVariables
        '    grd_VariableDisplay.FixedCols = 0
        '    grd_VariableDisplay.FixedRows = 0
        '    grd_VariableDisplay.Visible = True
        '    For cc% = 0 To grd_VariableDisplay.Cols - 1
        '        For rr% = 0 To grd_VariableDisplay.Rows - 1
        '        grd_VariableDisplay.Col = cc%
        '        grd_VariableDisplay.row = rr%
        '        grd_VariableDisplay.Text = ""
        '        Next rr%
        '    Next cc%

        '   Fill Grid - especially important if have re-run the model
        '       and need to clear the grid
        '   ---------------------------------------------------------

        '    For ILOOP% = 1 To Session("COUNTE")
        '         grd_VariableDisplay.Col = ILOOP% - 1
        '         grd_VariableDisplay.row = 0
        '         grd_VariableDisplay.ColWidth(ILOOP% - 1) = Inch
        '         grd_VariableDisplay.Text = Session("VARN")$(1, ILOOP%)
        '         grd_VariableDisplay.Col = ILOOP% - 1
        '         grd_VariableDisplay.row = 1
        '         grd_VariableDisplay.Text = Session("GR1N")$(ILOOP%)
        '         grd_VariableDisplay.Col = ILOOP% - 1
        '         grd_VariableDisplay.row = 2
        '         grd_VariableDisplay.Text = Session("GR2N")$(ILOOP%)
        '    Next ILOOP%

        '    cmb_VarName.SetFocus
        '    cmb_VarName.Text = ""


        '   Establish File Text Box
        '    -----------------
        '    msg$ = Trim(Str$(0)) + " Working File, based on "
        '    msg$ = msg$ + WorkingFile$
        '    Txt_files.Text = msg$
        '    Txt_files.Enabled = False
        '    DefaultFile = 0

    End Sub

    '=============================VarSel.Frm==============================
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.Form.GotFocus' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.GotFocus.html'.
    'JRSVBNET *** BEGIN ***
    'Public Sub frm_VarSel_GotFocus(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) 'Handles frm_VarSel.GotFocus
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '    'JRS 9/10/01 COMPUTATIONS
    '    'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.Form.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
    '    'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '    If frm_VarSel.Visible Then WinFormsToWeb_FocusControl.Focus("cmb_VarName")

    'End Sub
    'JRSVBNET *** END ***

    '=============================VarSel.Frm==============================
    'CONVERSION_NOTE: The code of Event 'frm_VarSel.frm_VarSel_Load' was moved to Method 'frm_VarSel.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Public Sub frm_VarSel_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True

        'JAM 2014-05-18 Keeping chosen filter
        If fSubSetchosen.Value <> "" Then
            Session("FiltVarSelVCat") = fSubSetchosen.Value
        Else
            If Session("FiltVarSelVCat") Is Nothing Then
                Session("FiltVarSelVCat") = 0
            Else
                fSubSetchosen.Value = Session("FiltVarSelVCat")
            End If
        End If


        'JAM 2014-05-18 Keeping forecasted or empirical variable set =>0:Forecasted 1:Empirical
        If fEmpOrForeMode.Value <> "" Then
            Session("VEmpOrForeMode") = fEmpOrForeMode.Value
        Else
            If Session("VEmpOrForeMode") Is Nothing Then
                Session("VEmpOrForeMode") = 0
            Else
                fEmpOrForeMode.Value = Session("VEmpOrForeMode")
            End If
        End If


        'JAM 2014-05-31 Keeping Historic plus analog selection 0: Selected, 1: Not selected
        If fHistoricAnalogMode.Value <> "" Then
            Session("VHistoricAnalogMode") = fHistoricAnalogMode.Value
        Else
            If Session("VHistoricAnalogMode") Is Nothing Then
                Session("VHistoricAnalogMode") = 1
            Else
                fHistoricAnalogMode.Value = Session("VHistoricAnalogMode")
            End If
        End If



        'jrs 2012/01/17
        If Session("COUNTE") = 0 Then
            cswmWebMenu.AccessorKeyCode = 27
        End If

        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "mnuContinue"
        cswmGroup1.OnMenuBarClick = "continueFromVarSel();"
        'If Session("COUNTE") > 0 Then
        '    'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_DisplayMenu.aspx"
        '    cswmGroup1.OnMenuBarClick = "location.href='frm_DisplayMenu.aspx';"
        'Else
        '    'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MainMenu.aspx"
        '    cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        'End If


        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Scenario Output Files"
        cswmGroup1.ID = "mnuScenarioOutputFiles"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain File Numbering"
        cswmItem.ID = "mnuScenarioOutFilesExp"

        '*********************************************
        Dim Msg As String
        Carret = "\n"
        carret2 = Carret + Carret
        Msg = "When you start IFs, at least 2 files are available:" + Carret
        Msg = Msg & "   0. The \'Working File\' - identical to IFsBase until you:" + Carret
        Msg = Msg & "      a. Change a parameter" + Carret
        Msg = Msg & "          (changes affect the working file), or" + Carret
        Msg = Msg & "      b. Open a new file (Main Menu-File-Open)" + Carret
        Msg = Msg & "         (a newly opened file becomes the working file)" + Carret
        Msg = Msg & "   1. IFsBase (Base case, never changed)" & carret2
        Msg = Msg & "Other files may be available, numbered sequentially." + Carret
        cswmItem.OnClick = "alert('" & Msg & "');"
        '*********************************************

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Attach More Scenario Output Files"
        'cswmItem.ID = "mnuScenarioOutFilesAttach"
        '    'jrs 2008/04/25
        '    cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain Scenario Analysis"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Variable Selection Options"
        cswmGroup1.ID = "mnuVarSelOpt"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain Names"
        'jrs 2006/11/28
        cswmItem.IconImage = "blank.gif"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Variables"
        cswmItem.ID = "mnuVarSelOptShowVar"
        If Session("VarSelCheck") = "mnuVarSelOptShowVar" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Variables and Parameters"
        cswmItem.ID = "mnuVarSelOptShowVarPar"
        If Session("VarSelCheck") = "mnuVarSelOptShowVarPar" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2006/11/21
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show Variable and Display Lists"
        cswmItem.ID = "mnuVarSelOptShowVarDispList"
        If CBool(Session("ShowVarDispList")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "History plus Forecast"
        cswmItem.ID = "mnuVarSelOptHistoricPlusForecast"
        cswmItem.Enabled = Not (Session("HistoricBase") = CStr(True))
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2006/11/21
        'cdv 2022/05/19
        If StandAloneMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Create variable list from selection status box"
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2023/01/10
        If StandAloneMode Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Create G-list from selection status box"
            cswmItem.IconImage = "blank.gif"
            cswmItem.OnClick = "checkNewGListName();"

            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Edit G-list"
            cswmItem.IconImage = "blank.gif"
        End If

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Create Display Lists from Selection Status Box"
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use estimation to complete data"
        cswmItem.ID = "mnuEstimation"
        If CBool(Session("bFillHis")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem.Enabled = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Or Session("HistoricBase") = CStr(True))

        'jrs 2011/03/28
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use estimation to complete data (only holes no extrapolation)"
        cswmItem.ID = "mnuEstimationOnlyHoles"
        If CBool(Session("bFillHisOnlyHoles")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem.Enabled = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Or Session("HistoricBase") = CStr(True))

        'jrs 2006/09/26
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Use all available Historical Data"
        cswmItem.ID = "mnuuseAllHis"
        If CBool(Session("buseAllHis")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem.Enabled = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Or Session("HistoricBase") = CStr(True))

        'jrs 2009/01/27
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Exclude Total when using All Dimensions"
        cswmItem.ID = "mnuExcTot"
        If CBool(Session("bExcTot")) Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2022/12/14
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Group Aggregation"
        cswmItem.ID = "mnuGroupAgg"
        cswmItem.IconImage = "blank.gif"
        If IsNothing(Session("mnuGroupAggEnable")) Then
            Session("mnuGroupAggEnable") = False
        End If
        cswmItem.Enabled = Session("mnuGroupAggEnable")

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "History Variable Options"
        cswmGroup1.ID = "mnuHisVarOpt"
        cswmGroup1.ShowMenuBarItem = (Session("HistoricBase") = CStr(True))

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select from Forecasted Historical Variables"
        'jrs 10/29/04
        cswmItem.ID = "mnuHisVarOptSelForHisVar"
        If Session("VarSelCheckHis") = "mnuHisVarOptSelForHisVar" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select from Empirical Historical Variables"
        cswmItem.ID = "mnuHisVarOptSelEmpHisVar"
        If Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select for Validation"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Open for Validation"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.ID = "mnuOpen"
        If Session("HistoricBase") = CStr(True) Then
            cswmGroup1.Caption = "Open IFsBase"
        Else
            cswmGroup1.Caption = "Open IFsBaseHist"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Switch"

        'jrs 2005/08/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.ID = "mnuVarSearch"
        cswmGroup1.Caption = "Variable Search"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuSearch"
        cswmItem.Caption = "Search"
        If CBool(Session("ShowVarDispList")) Then
            cswmItem.Enabled = False
        End If

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16011);"
        'jrs 2013/09/05
        If pnlContext.Visible Then
            cswmGroup1.OnMenuBarClick = LoadHelpJS("View Code", "")
        Else
            cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")
        End If

        If CBool(Session("SensitivityFlag")) = True Then
            cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem = False
            cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesAttach").Enabled = False
            cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = False
        Else
            cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
            'jrs 2008/04/25 changed to false while we implement it or remove them.
            'jrs 2008/05/07 cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesAttach").Enabled = False
        End If
        'jrs 2005/01/10 cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem = (Session("FirstYear") = FirstYearHistory And Session("LastYear") - Session("FirstYear") < 100)
        cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem = (Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory)
        If cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem Then cswmWebMenu.Groups("mnuOpen").Caption = "Open IFsBase"
        If Session("VarSelCheck") <> "mnuVarSelOptHistoricPlusForecast" And cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").Enabled = False
        End If

        'JRS 05/13/03
        If Not Page.IsPostBack And Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            mnuVarSelOptHistoricPlusForecast_Click(Me, New System.EventArgs)
        End If

        'jrs 2023/06/08
        If Not Page.IsPostBack AndAlso Session("VarSelCheck") <> "mnuVarSelOptHistoricPlusForecast" Then
            If Session("FirstYear") < FirstYearForecast AndAlso Session("ActiveBase") = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
            End If
        End If

        'jrs 2005/08/22
        If Not Page.IsPostBack And Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar" Then
            mnuHisVarOptSelEmpHisVar_Click(Me, New EventArgs)
        End If

        ''JRS 10/05/04
        If Not Session("lstVarNameSelectedIndex") Is Nothing And lstVarName.SelectedIndex = -1 Then
            lstVarName.SelectedIndex = Session("lstVarNameSelectedIndex")
            'jrs 2023/07/12
            If Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar" Then
                'JAM 2016-03-25 Keeping chosen value for lstVarName combo
                fVarchosen.Value = Session("lstVarNameSelectedIndex")
            Else
                'JAM 2016-03-25 Keeping chosen value for lstVarName combo
                fVarchosen.Value = Session("lstVarNameSelectedIndex") - 1
            End If
        Else
            fVarchosen.Value = 0

        End If

        'jrs 2004/11/27
        cmdComputations.Attributes.Add("onClick", "showhg();")

        'jrs 2005/08/18
        If Not IsNothing(Session("LoadSearchVar")) Then
            LoadSearch(Session("LoadSearchVar"))
            Session("LoadSearchVar") = Nothing
        End If

        'jrs 2008/08/29
        'Check that at least one file is selected
        'if you click too fast in the groups button the selection was disapearing
        Dim i As Integer, bSelected As Boolean = False
        For i = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(i).Selected Then
                bSelected = True
                Exit For
            End If
        Next
        If Not bSelected Then
            If IsNothing(Session("DefaultFile")) Then
                lstFiles.Items(0).Selected = True
            Else
                lstFiles.Items(Session("DefaultFile")).Selected = True
            End If
        End If

        'JAM 2014-04-04 Keeping chosen filter
        If fSubSetchosen.Value <> "" Then
            Session("FiltVarSelVCat") = fSubSetchosen.Value
        Else
            If Session("FiltVarSelVCat") Is Nothing Then
                Session("FiltVarSelVCat") = 0
            Else
                fSubSetchosen.Value = Session("FiltVarSelVCat")
            End If
        End If


        'JAM 2014-04-19 Keeping forecasted or empirical variable set =>0:Forecasted 1:Empirical
        If fEmpOrForeMode.Value <> "" Then
            Session("VEmpOrForeMode") = fEmpOrForeMode.Value
        Else
            If Session("VEmpOrForeMode") Is Nothing Then
                Session("VEmpOrForeMode") = 0
            Else
                fEmpOrForeMode.Value = Session("VEmpOrForeMode")
            End If
        End If

        'JAM 2014-05-31 Keeping Historic plus analog selection 0: Selecte, 1: Not selected
        If fHistoricAnalogMode.Value <> "" Then
            Session("VHistoricAnalogMode") = fHistoricAnalogMode.Value
        Else
            If Session("VHistoricAnalogMode") Is Nothing Then
                Session("VHistoricAnalogMode") = 1
            Else
                fHistoricAnalogMode.Value = Session("VHistoricAnalogMode")
            End If
        End If

        'jrs 2011/11/03
        If Not Page.IsPostBack Then
            ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)
            If Not (Session("PDFilesIdxs")(0) Is Nothing) Then
                Dim fileIdx As Integer
                For fileIdx = 0 To lstFiles.Items.Count - 1
                    lstFiles.Items(fileIdx).Selected = Session("PDFilesIdxs")(fileIdx)
                Next fileIdx
            End If

            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If
        End If
        lstFiles.Attributes.Add("onclick", "lstFiles_Click(event);")
        lstFiles.Attributes.Add("onmousedown", "lstFiles_ClickDown(event);")
        lstFiles.Attributes.Add("onmouseup", "lstFiles_ClickUp(event);")
        If Request.Browser.Browser = "Chrome" Then
            lstFiles.Attributes.Add("onchange", "lstFiles_Change(event);")
        ElseIf Request.Browser.Browser = "Firefox" Then
            lstFiles.Attributes.Add("onchange", "lstFiles_Click(event);")
        End If

        'JAM 2014-04-04
        If Not CBool(DevelopmentMode) Then
            cmdGeneratejs.Visible = False
        End If

        'jrs 2024/01/17
        Session("YAxisMax") = Nothing
        Session("YAxisMin") = Nothing

        'jrs 2023/06/26
        PackJScritpBuilder()

    End Sub
    Private Function SortCategories()
        Dim arl As New ArrayList()
        Dim k As Integer = 1

        While k < cmb_VCAT.Items.Count

            arl.Add(cmb_VCAT.Items(k).Value)
            k += 1
        End While

        arl.Sort()
        arl.Insert(0, "All")
        cmb_VCAT.Items.Clear()
        cmb_VCAT.DataSource = arl

        'JAM 2014-06-1 Commented due lst_Varname doesn't have values
        cmb_VCAT.DataBind()
    End Function

    '=============================VarSel.Frm==============================
    'UPGRADE_WARNING: Form event frm_VarSel.Unload has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event ''JRSVBNET System.Windows.Forms.Form.Closed' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.Closed.html'.
    Public Sub frm_VarSel_Closed()
        Dim LineOut As String
        Dim finam2 As String
        Dim j As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Modify Menu added by Mohammod-Start
        If Session("COUNTE") > 0 Then
            For j = 1 To Session("COUNTE")
                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                FileNum = Val(Session("FileNumbr")(j))
                'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If FileNum = -1 Then
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.LineGraph.Visible = False
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.BarChart.Visible = False
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.PieChart.Visible = False
                    '                'JRSVBNET frm_DisplayMenu.Cohorts.Visible = False
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.ScatterPlot.Visible = False
                    '                'JRSVBNET frm_DisplayMenu.Report.Visible = False

                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.mnuHistory.Visible = True

                    Exit Sub
                End If
            Next j
        End If

        'JRS 07/29/02 BEGIN
        ''JRSVBNET frm_DisplayMenu.LineGraph.Visible = True
        ''JRSVBNET frm_DisplayMenu.BarChart.Visible = True
        ''JRSVBNET frm_DisplayMenu.PieChart.Visible = True
        ''JRSVBNET frm_DisplayMenu.Cohorts.Visible = True
        ''JRSVBNET frm_DisplayMenu.ScatterPlot.Visible = True
        ''JRSVBNET frm_DisplayMenu.Report.Visible = True

        ''JRSVBNET frm_DisplayMenu.mnuHistory.Visible = False
        'JRS 07/29/02 END
        'Modify Menu added by Mohammod-End
        'jrs 2005/02/12
        Dim fNum As Integer
        fNum = FreeFile()

        Dim sSQL As String
        If CBool(Session("SensitivityFlag")) = True Then
            'JRS 05/29/03 RAND PARDEE
            finam2 = Session("SensitivityFileDir") & "SensitivityOutputVars.dat"
            'Open finam2$ For Output As #2
            FileOpen(fNum, finam2, OpenMode.Append)
            For j = 1 To Session("COUNTE")
                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                LineOut = Session("VARN")(1, j) & ", "
                'JRS 05/29/03 RAND PARDEE
                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                sSQL = "INSERT INTO SensitivityOutputVars VALUES('" & Session("VARN")(1, j) & "','"
                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("GR1N")(j) <> "" Then
                    LineOut = LineOut & Session("GR1N")(j) & ", "
                    sSQL = sSQL & Session("GR1N")(j) & "','"
                Else
                    LineOut = LineOut & "NoDim1" & ", "
                    sSQL = sSQL & "NoDim1','"
                End If
                If Session("GR2N")(j) <> "" Then
                    LineOut = LineOut & Session("GR2N")(j) & ", "
                    sSQL = sSQL & Session("GR2N")(j) & "',"
                Else
                    LineOut = LineOut & "NoDim2" & ", "
                    sSQL = sSQL & "NoDim2',"
                End If
                If Session("GR3N")(j) <> "" Then
                    LineOut = LineOut & Session("GR3N")(j) & ", "
                    sSQL = sSQL & Session("GR3N")(j) & "',"
                Else
                    LineOut = LineOut & "NoDim3" & ", "
                    sSQL = sSQL & "NoDim3',"
                End If
                LineOut = LineOut & Str(Session("VL")(j))
                sSQL = sSQL & Trim(Str(Session("VL")(j))) & ")"
                updateAccessWith1Command(Session("SensitivityFileDir") & "SensitivityOutputVars.mdb", sSQL)
                PrintLine(fNum, LineOut)
            Next j
            FileClose(fNum)
            Session("SensitivityFlag") = CStr(False)
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
        Else
            'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET frm_DisplayMenu.LineGraph.Visible = True
            'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET frm_DisplayMenu.BarChart.Visible = True
            'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET frm_DisplayMenu.PieChart.Visible = True
            '        'JRSVBNET frm_DisplayMenu.Cohorts.Visible = True
            'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET frm_DisplayMenu.ScatterPlot.Visible = True
            '        'JRSVBNET frm_DisplayMenu.Report.Visible = True

            '        'JRSVBNET frm_DisplayMenu.mnuHistory.Visible = False
        End If

    End Sub


    '=============================VarSel.Frm==============================
    'JRS 10/05/02 WVS
    Public Sub Gridput()
        Dim EndYear As Object
        Dim VLN As Object
        Dim agrule As String
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Increment Count and Bound
        '   -------------------------
        If Session("COUNTE") = 0 Or CalcRunning = False Or SeqOn = "True" Then
            Session("COUNTE") = Session("COUNTE") + 1
            If Session("COUNTE") = 1 Then
                'mti August 2004 5
                If Not IsNothing(cswmWebMenu) Then
                    'jrs 2006/11/15 cswmWebMenu.Groups("mnuContinue").DownLevelURL = "frm_DisplayMenu.aspx"
                    cswmWebMenu.Groups("mnuContinue").OnMenuBarClick = "location.href='frm_DisplayMenu.aspx';"
                End If
            End If
            If SeqRepeat > 0 Then
                SeqRepeat = SeqRepeat - 1
                '        Else
                '            SeqOn = "False"
            End If
        End If

        If Session("COUNTE") > MaxVariables Then
            Session("COUNTE") = MaxVariables
            If GridFull <> "True" Then
                GridFull = "True"
                Msg = "Name list for display is full - exit and display"
                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            GridFull = "True"
        End If

        '   Get Last Runyear from this Particular File and Save in Session("FinYear") for Display
        '   ---------------------------------------------------------------------
        Session("VARNUM") = Session("COUNTE")

        'jrs 2006/04/07 Centrilized Data Load for Display
        Dim iRowPosition As Integer
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            iRowPosition = LastYearHistory - FirstYearHistory + 1
            Session("FirstYear") = FirstYearForecast
        Else
            iRowPosition = 1
        End If

        If Session("bComputation") Then
            If Session("bCompAll") Then
                gLoadForDisplay(Session("SE"), Session("SEL"), sDim_1, idxDim1, sDim_2, idxDim2, sDim_3, idxDim3, "", FileNum,
                True, True, FlagGroup, 1, Session("PLOTAll"), Session("COUNTE"), iRowPosition,
                Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay(Session("SE"), Session("SEL"), sDim_1, idxDim1, sDim_2, idxDim2, sDim_3, idxDim3, "", FileNum,
                True, True, FlagGroup, 1, Session("PLOTComp"), Session("COUNTE"), iRowPosition,
                Session("FirstYear"), Session("LastYear"), 0, True, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Else
            gLoadForDisplay(Session("SE"), Session("SEL"), sDim_1, idxDim1, sDim_2, idxDim2, sDim_3, idxDim3, "", FileNum,
            True, True, FlagGroup, 1, Session("PLOT"), Session("COUNTE"), iRowPosition,
            Session("FirstYear"), Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        gLoadForDisplaySaveLbls(Session("COUNTE"), Session("SEL"), sDim_1, sDim_2, sDim_3, FlagGroup, FileNum, "",
        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("FirstYear") = FirstYearHistory
        End If
        If Not Session("bLoadForDisplaySuccess") Then
            Session("COUNTE") = Session("COUNTE") - 1
            Exit Sub
        End If

        '   Fill Grid
        '   ---------

        'JRS 02/25/04
        Dim iloop As Short
        'mti August 2004 5
        If Not IsNothing(grd_VariableDisplay) Then
            grd_VariableDisplay.Columns.Clear()
            grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

            If CalcRunning = False Or SumOn = True Then
                'JRS 02/25/04
                For iloop = 1 To Session("COUNTE")

                    Dim myCol As New TMSWebControls.Column
                    'myCol.Type = TMSWebControls.Column.ColumnType.DataButton
                    'myCol.Type = TMSWebControls.Column.ColumnType.Button
                    'myCol.Type = TMSWebControls.Column.ColumnType.LinkField

                    myCol.WidthType = TMSWebControls.WType.Absolute
                    'myCol.ButtonWidth = 100
                    myCol.Width = 100
                    myCol.Color = System.Drawing.Color.White
                    grd_VariableDisplay.Columns.Add(myCol)

                    grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
                    grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)
                    grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
                    grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
                    grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop)

                Next iloop


            Else
                'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
                'JRSVBNET Support.DataGridManager.SetColWidth(grd_VariableDisplay, Session("COUNTE") - 1, Inch)
                'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("VARN")(1, Session("COUNTE")))
                'JRSVBNET Session("VARN")(0, Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
                'JRSVBNET Session("VARN")(1, Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
                'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 1)
                'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("GR1N")(Session("COUNTE")))
                'JRSVBNET Session("GR1N")(Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
                'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 2)
                'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("GR2N")(Session("COUNTE")))
                'JRSVBNET Session("GR2N")(Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
                'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
                'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 3)
                'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("GR3N")(Session("COUNTE")))
                'JRSVBNET Session("GR3N")(Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)

            End If
        End If
        'mti August 2004 5 -end 

        'jrs 10/27/04 Flexible Dimension Names for Graphs
        Session("FDNG1")(Session("COUNTE")) = Session("GR1N")(Session("COUNTE"))
        Session("FDNG2")(Session("COUNTE")) = Session("GR2N")(Session("COUNTE"))
        Session("FDNG3")(Session("COUNTE")) = Session("GR3N")(Session("COUNTE"))
        Session("FDNG4")(Session("COUNTE")) = Session("GR4N")(Session("COUNTE"))

        '   Recognize Beginning or End of Calculation - Save for Next Input
        '   ---------------------------------------------------------------
        If CalcRunning = True And (CalcType = "None" And SumOn = False) Then
            CalcRunning = False
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object SeqOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If CalcRunning = False And (CalcType <> "None" Or SumOn = True) And SeqOn <> "True" Then
            CalcRunning = True
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If SeqRepeat > 0 Then 'When sequence is repeated, it is for calculation
            CalcRunning = True
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CalcSave = CalcType 'Do not change CalcSave except when sequence over
        End If

        'mti Validation changes
        ChkPickingForecast()
        'mti Validation changes

        'cmb_VarName.SetFocus

        Exit Sub
        '
        '----------

    End Sub

    '=============================VarSel.Frm==============================
    Public Sub RetrValues()
        Dim Number As Single
        Dim finam As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '     Retrieve Values from Buffer
        '     ---------------------------

        'mti Validation changes
        Dim intFile As Short
        intFile = FreeFile()

        ''mti Validation changes 10 august 2002
        If bPickingValidation Then
            If Session("DoingHistory") Then
                'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                FileNum = -1
            Else
                'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                FileNum = 1
            End If
        End If
        'mti Validation changes 10 august 2002

        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If FileNum = 0 Then
            finam = Session("PutFdrvRun") & "working.run"
            'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        ElseIf FileNum = 1 Then
            finam = Session("PutFdrvRun") & Session("ActiveBase") '"IFSBASE.RUN"
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            finam = Session("RunFiles")(FileNum)
        End If

        FileOpen(intFile, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared) '#1
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("RECORD"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1041"'
        FileGet(intFile, Number, Session("RECORD"))
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("VarVal") = Number
        FileClose(intFile)

        'mti Validation changes



    End Sub


    '=============================VarSel.Frm==============================
    Public Sub CalcNumber()
        Dim tempcnt As Object
        Dim cnt_end As Object
        Dim cnt_init As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object cnt_init. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        cnt_init = Session("COUNTE")
        'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object cnt_end. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        cnt_end = Session("COUNTE") + IIf(SeqRepeat > 0, SeqRepeat - 1, 0)
        'UPGRADE_WARNING: Couldn't resolve default property of object cnt_end. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object cnt_init. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For tempcnt = cnt_init To cnt_end
            'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If SeqRepeat > 0 Then
                'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SeqRepeat = SeqRepeat - 1
                Session("COUNTE") = Session("COUNTE") + 1
                If Session("COUNTE") = 1 Then
                    'jrs 2006/11/15 cswmWebMenu.Groups("mnuContinue").DownLevelURL = "frm_DisplayMenu.aspx"
                    cswmWebMenu.Groups("mnuContinue").OnMenuBarClick = "location.href='frm_DisplayMenu.aspx';"
                End If
            End If
            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
            'JRSVBNET Support.DataGridManager.SetColWidth(grd_VariableDisplay, Session("COUNTE") - 1, 1.5 * Inch) 'increased from inch
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Str(Session("VarVal")))
            'JRSVBNET Session("VARN")(0, Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
            'JRSVBNET Session("VARN")(1, Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)

            For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1

                'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If (DivOnPrev Or CalcSave = "DivOn") And Session("VarVal") <> 0 Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("PLOT")(i2, Session("COUNTE")) = Session("PLOT")(i2, Session("COUNTE")) / Session("VarVal")
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If CalcSave = "PerOn" And Session("VarVal") <> 0 Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("PLOT")(i2, Session("COUNTE")) = Session("PLOT")(i2, Session("COUNTE")) / Session("VarVal") * 100
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If MulOnPrev Or CalcSave = "MulOn" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("PLOT")(i2, Session("COUNTE")) = Session("PLOT")(i2, Session("COUNTE")) * Session("VarVal")
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If AddOnPrev Or CalcSave = "AddOn" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("PLOT")(i2, Session("COUNTE")) = Session("PLOT")(i2, Session("COUNTE")) + Session("VarVal")
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If SubOnPrev Or CalcSave = "SubOn" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("PLOT")(i2, Session("COUNTE")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("PLOT")(i2, Session("COUNTE")) = Session("PLOT")(i2, Session("COUNTE")) - Session("VarVal")
                End If
            Next i2
            'xtemp = 5
            'MsgBox ("Test?")

        Next tempcnt
    End Sub
    '=============================VarSel.Frm==============================
    Public Sub History_VarName_Click()
        Dim SELN As String
        Dim FullName As String
        Dim GroupName As String
        Dim Msg As String
        Dim PosMul As Object
        Dim PosDiv As Object
        Dim SELNLen As Object
        Dim NVal As Object
        Dim ValSelection As String
        Dim SE As String
        Dim Cohort As String
        Dim Series As String
        'jrs 2006/04/10
        Dim sFullName() As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Variable Name Typed by User - Strip off Operator and Check Name Validity
        '   ------------------------------------------------------------------------
        On Error GoTo FileProblem
        DivOn = False
        MulOn = False
        AddOn = False
        SubOn = False
        NumVal = False
        Series = "No"
        Cohort = "No"
        OperStr = ""

        '   Initialize Dimensions, Reset Variable Pointer JRS 3/17/02
        '   ---------------------------------------------
        Label1 = ""
        Label2 = ""
        Label3 = ""
        'JRS 03/04/09
        Session("Label1") = Label1


        'JRS 1/19/02 DISPLAY
        If lstVarName.SelectedIndex = -1 Then
            SE = txtVarSearch.Text   'Name typed
        Else
            If CBool(Session("ShowVarDispList")) Then
                SE = VB.Left(txtVarSearch.Text, Len(txtVarSearch.Text) - 4)
            End If
        End If


        'JRS 12/04/01 DISPLAY
        If CBool(Session("ShowVarDispList")) Then
            SetDataTablefromAccess(Session("PutFdrvRun") & "IFSVAR.mdb", "SELECT * FROM VarLists WHERE ListName = """ & SE & """", "", rsList)
            If rsList.Rows.Count > 0 Then
                SE = rsList.Rows(0).Item("VarName")
            End If
        End If

        If lstVarName.SelectedIndex = -1 Then
            ValSelection = CStr(False)
            If IsNumeric(txtVarSearch.Text) Then 'Numeric scaling value
                NumVal = True
                NVal = Val(txtVarSearch.Text)
            End If
            SE = UCase(txtVarSearch.Text) 'SE$ picks up the Variable name typed
            SELNLen = Len(SE)
            PosDiv = InStr(1, SE, "/")
            If PosDiv = SELNLen Then 'Check for Division
                DivOn = True
                SE = VB.Left(SE, SELNLen - 1)
                OperStr = "/"
            End If
            PosMul = InStr(1, SE, "*")
            If PosMul = SELNLen Then ' Check for Multiplication
                MulOn = True
                SE = VB.Left(SE, SELNLen - 1)
                OperStr = "*"
            End If
            PosMul = InStr(1, SE, "+")
            If PosMul = SELNLen Then ' Check for Addition
                AddOn = True
                SE = VB.Left(SE, SELNLen - 1)
                OperStr = "+"
            End If
            PosMul = InStr(1, SE, "-")
            If PosMul = SELNLen Then ' Check for Subtraction
                SubOn = True
                SE = VB.Left(SE, SELNLen - 1)
                OperStr = "-"
            End If

            If NumVal <> True Then 'Make sure that variable
                'name is valid one
                '            Set db = OpenDatabase(FDRV$ + "Data\datadict.MDB")
                SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict", "Variable", valtbl)

                For i = 0 To valtbl.Rows.Count - 1
                    TableName = valtbl.Rows(i).Item("Table")
                    VarName = valtbl.Rows(i).Item("Variable")
                    Definition = valtbl.Rows(i).Item("Definition")
                    '                Label3 = "Unit"
                    Label3 = valtbl.Rows(i).Item("Units")
                    If IsDBNull(Label3) Then Label3 = "Unit"

                    'different from scatvars cmb_varname_click
                    Ag_rule = valtbl.Rows(i).Item("Aggregation")
                    If (SE = UCase(VarName & " - " & Definition)) Or (SE = UCase(VarName)) Then
                        ValSelection = CStr(True)
                        Exit For
                    End If

                Next i
            End If 'NumVal <> True

            If CBool(ValSelection) = False And NumVal = False Then 'If VarName$ not found
                Msg = txtVarSearch.Text & " is not a valid selection."
                Msg = Msg & "  Please try again."

                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            If NumVal = True Then
                VarName = Str(NVal)
            End If
        Else 'jrs 2006/06/29
            'jrs 2006/04/14
            If Not CBool(Session("ShowVarDispList")) Then
                sFullName = Split(txtVarSearch.Text, " - ")
                SE = sFullName(0)
            End If
        End If 'cmb_VarName.ListIndex = -1

        'Whether name typed or selected, both SE$ and VarName$ now carry variable name

        Session("SE") = SE

        'On Error GoTo FileProblem:

        '   Check if Using Groups
        '   --------------------------------------------------------------

        'different from scatvars

        If FlagGroup = True Then
            GroupOn = True
        Else
            GroupOn = False
        End If
        'JRS 03/13/04
        Session("GroupOn") = GroupOn

        'JRS 1/20/01 DISPLAY BEGIN
        Dim bFlag As Boolean
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
        If CBool(Session("ShowVarDispList")) Then
            If rsList.Rows.Count > 0 Then
                bFlag = (rsList.Rows(0).Item("Type") = "D")
            End If
        End If


        Dim iOldCount As Short
        Dim sPar1 As Object
        Dim sPar2 As String
        Do While True
            'yearload/waveload in scatvars

            If NumVal = True Then
                If DivOnPrev Or MulOnPrev Or AddOnPrev Or SubOnPrev Then

                    'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("VarVal") = NVal
                    'mti Validation changes : 'was wrong with all and Formula in History
                    'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Session("COUNTE") = Session("COUNTE") - SeqRepeat
                    'mti Validation changes

                    Call CalcNumber()
                    'SeqOn = False
                    'CalcSave = "None"
                    'CalcType = "None"
                    'UPGRADE_WARNING: Couldn't resolve default property of object DivOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    DivOnPrev = False
                    'UPGRADE_WARNING: Couldn't resolve default property of object MulOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    MulOnPrev = False
                    'UPGRADE_WARNING: Couldn't resolve default property of object AddOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    AddOnPrev = False
                    'UPGRADE_WARNING: Couldn't resolve default property of object SubOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    SubOnPrev = False
                    'mti Validation changes
                    ChkEndPickingHistory()
                    'mti Validation changes

                    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    txtVarSearch.Text = ""

                    'Exit Sub
                Else
                    'UPGRADE_ISSUE: GoSub statement is not supported. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1014"'
                    'JRS 01/28/04
                    BadVariable(NVal)

                    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    txtVarSearch.Text = ""

                    Exit Sub
                End If
            Else
                If Not bFlag Then
                    Call History_DimLoad()
                Else

                    'UPGRADE_WARNING: Couldn't resolve default property of object sPar1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    sPar1 = rsList.Rows(0).Item("Param1")
                    sPar2 = rsList.Rows(0).Item("Param2")
                    'UPGRADE_WARNING: Couldn't resolve default property of object sPar1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If sPar1 > "" Then
                        History_DimLoad()
                        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        iOldCount = cmb_DIM1.Items.Count
                        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                        For i = 0 To cmb_DIM1.Items.Count - 1
                            'UPGRADE_WARNING: Couldn't resolve default property of object sPar1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            'JRSVBNET *** BEGIN ***
                            'If VB6.GetItemString(cmb_DIM1, i) = sPar1 Then
                            '    cmb_DIM1.SelectedIndex = i
                            '    Exit For
                            'End If
                            'JRSVBNET *** END ***
                        Next i
                        'JRS 1/2/02 DISPLAY
                        If i = iOldCount Then
                            'jrs 2008/09/03 but_GroupRegToggle_Click(but_GroupRegToggle, New System.EventArgs)
                            History_DimLoad()
                            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                            For i = 0 To cmb_DIM1.Items.Count - 1
                                'UPGRADE_WARNING: Couldn't resolve default property of object sPar1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                'JRSVBNET *** BEGIN ***
                                'If VB6.GetItemString(cmb_DIM1, i) = sPar1 Then
                                '    cmb_DIM1.SelectedIndex = i
                                '    Exit For
                                'End If
                                'JRSVBNET *** END ***
                            Next i
                        End If

                        If sPar2 > "" Then
                            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                            For i = 0 To cmb_DIM1.Items.Count - 1
                                'JRSVBNET *** BEGIN ***
                                'If VB6.GetItemString(cmb_DIM1, i) = sPar2 Then
                                '    cmb_DIM1.SelectedIndex = i
                                '    Exit For
                                'End If
                                'JRSVBNET *** END ***
                            Next i
                            'JRS 1/2/02 DISPLAY
                            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                            If i = cmb_DIM1.Items.Count Then
                                'jrs 2008/09/03 but_GroupRegToggle_Click(but_GroupRegToggle, New System.EventArgs)
                                History_DimLoad()
                                'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                                For i = 0 To cmb_DIM1.Items.Count - 1
                                    'UPGRADE_WARNING: Couldn't resolve default property of object sPar1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                                    'JRSVBNET *** BEGIN ***
                                    'If VB6.GetItemString(cmb_DIM1, i) = sPar1 Then
                                    '    cmb_DIM1.SelectedIndex = i
                                    '    Exit For
                                    'End If
                                    'JRSVBNET *** END ***
                                Next i
                            End If

                        End If
                    End If

                End If
            End If
            'JRS 1/20/02 DISPLAY
            If Not bFlag Then
                Exit Do
            Else
                i = i + 1
                If i = rsList.Rows.Count Then Exit Do
                SE = rsList.Rows(i).Item("VarName")
                SELN = UCase(SE)

                SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict", "Variable", valtbl)
                Dim j As Integer
                For j = 0 To valtbl.Rows.Count - 1
                    TableName = valtbl.Rows(j).Item("Table")
                    GroupName = valtbl.Rows(j).Item("Group")
                    VarName = valtbl.Rows(j).Item("Variable")
                    Ag_rule = valtbl.Rows(j).Item("Aggregation")
                    Definition = valtbl.Rows(j).Item("Definition")
                    FullName = VarName & " - " & Definition
                    'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    Label3 = valtbl.Rows(j).Item("Units")
                    'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If IsDBNull(Label3) Then Label3 = "Unit"

                    'Label3 = "Unit"
                    'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
                    If CBool(Session("ShowVarDispList")) And SE = VarName Then
                        Exit For
                    End If

                Next j

            End If
        Loop



        'JRS 05/07/04
        Session("TableName") = TableName
        Session("VarName") = VarName
        Session("Ag_rule") = Ag_rule
        Session("Definition") = Definition
        Session("Label3") = Label3 'jrs 2005/05/23

        Exit Sub



FileProblem:
        Msg = "Sorry. An error has arisen during an attempt to read " & VarName
        Msg = Msg & " from " & TableName & " - the variable or data for it "
        Msg = Msg & "doesn't exist in the file.  Please try another variable."
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")



    End Sub

    'JRS 01/28/04
    Public Sub BadVariable(ByVal NVal As String)
        Dim Msg As String
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Str(NVal) & " is an incorrect variable name"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '=============================VarSel.Frm==============================
    Public Sub History_DimLoad()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '       Make Combo Box and Label Visible for Subdimension(s)
        '       ----------------------------------------------------

        '        On Error GoTo FileProblem:

        cmb_DIM1.Visible = True
        lstDIM1.Visible = True
        txtVarSearch.Enabled = False
        lstVarName.Enabled = False
        lstVarName.Visible = False
        lbl_Dim1.Visible = True
        lbl_VarName.Visible = False
        lbl_files.Visible = False
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = False
        lstFiles.Visible = False
        'jrs 2012/06/18
        'lbl_vcat.Visible = False
        'cmb_VCAT.Visible = False
        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = False
        ' JRS 11/24/01 DISPLAY but_File.Visible = False
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_DIM1.Items.Clear()
        lstDIM1.Items.Clear()
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRS 02/25/04
        If cmb_DIM1.SelectedIndex >= 0 Then
            cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = ""
        End If

        'mti Validation changes
        lbl_Dim1.ForeColor = System.Drawing.ColorTranslator.FromOle(QBColor(0))
        'mti Validation changes

        lbl_Dim1.Text = "Type or Select Country/Region:"
        'JRS 05/07/04
        cmb_DIM1.Items.Add(New ListItem(""))
        '       Load Country/Region names into Combo Box
        '       ---------------------------------
        DimNum = 1 'necessary to indicate Country/Region or Group(If Flaggroup)
        'jrs 2005/08/18
        If FlagGroup = True Or FlagGList Then 'Set up for groups
            Dim i As Integer
            If FlagGroup Then
                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM [" & GroupIDNew & "] ORDER BY GroupName", "GroupName", valtbl)
                For i = 0 To valtbl.Rows.Count - 1
                    cmb_DIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
                    lstDIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("GroupName")))
                Next i
                cmb_DIM1.Items.Add(New ListItem("ALL"))
                lstDIM1.Items.Add(New ListItem("ALL"))
                'valtbl.Close '(nothing, db = nothing)
            Else
                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM GListId ORDER BY Description", "", valtbl)
                For i = 0 To valtbl.Rows.Count - 1
                    cmb_DIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("Description")))
                    lstDIM1.Items.Add(New ListItem(valtbl.Rows(i).Item("Description")))
                Next i
            End If
        Else 'Proceed with regions (Dimnum = 1)
            'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For EleLoop = 1 To MaxVal(DimNum)
                'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                cmb_DIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
                lstDIM1.Items.Add(New ListItem(GRPS(DimNum, EleLoop)))
            Next EleLoop
        End If

        'mti Validation changes
        If Not bPickingValidation Then
            'UPGRADE_ISSUE: ComboBox property cmb_DIM1.Locked was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
            'JRSVBNET cmb_DIM1.Locked = False
        End If
        'mti Validation changes


        Exit Sub

        'FileProblem:
        '    msg$ = "Sorry. An error has arisen during an attempt to read " + VarName$
        '    msg$ = msg$ + " from " + TableName$ + " - the variable or data for it "
        '    msg$ = msg$ + "doesn't exist in the file.  Please try another variable."
        '    MsgBox msg$, vbCritical
        '    Screen.MousePointer = 0


    End Sub
    '=============================VarSel.Frm==============================
    'Public Sub History_Dim1_Click(ByRef FoundDim As String)
    '    Dim Msg As String
    '    Dim FullName As String
    '    Dim GroupName As String
    '    Dim SELN As String
    '    Dim SE As String
    '    Dim ii As Short
    '    Dim NullFound As Object
    '    Dim Found As String
    '    Dim Sel2 As String
    '    Dim Ind As Short
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    'mti Validation changes
    '    SeqRepeat = 0
    '    'mti Validation changes

    '    DimNum = 1 ' Dimnum 1 = Region
    '    FoundDim = CStr(True)

    '    Ind = cmb_DIM1.SelectedIndex

    '    If Ind >= 1 Then
    '        If FlagGroup = True And DimNum = 1 Then 'Set up for groups
    '            Sel2 = GroupNames(Ind)
    '        Else 'Regions or non-regionalized
    '            Sel2 = (GRPS(DimNum, Ind))
    '            'JRS 9/23/01 COMPUTATIONS
    '            '            If Session("bComputation") And Sel2$ = "ALL" Then
    '            '                Session("bCompAll") = True
    '            '            End If
    '        End If
    '    Else
    '        Sel2 = cmb_DIM1.SelectedValue
    '    End If

    '    'JRS 08/22/02 COMPUTATIONS
    '    If Session("bComputation") And Sel2 = "ALL" Then
    '        Session("bCompAll") = True
    '        'JRS 08/22/02 COMPUTATIONS
    '        If Session("ComputationsDimensionSize") = 0 Then
    '            Session("ComputationsDimensionSize") = MaxVal(DimNum)
    '            'jrs 2005/08/18
    '            Session("iDimAll") = 1
    '        Else
    '            If (Session("ComputationsDimensionSize") <> MaxVal(DimNum)) Then
    '                Session("bCompAll") = False
    '                'JRS 01/28/04
    '                RemoveDim1()

    '                Dim frm_msg As New frm_Message
    '                frm_msg.LoadMessage("This variable doesn't have the same dimension size chosen before")
    '                Support.FormManager.StateStore(Me)
    '                Support.FormManager.Show(Me, "frm_Message.aspx")

    '            End If
    '        End If
    '    ElseIf Session("bComputation") And Session("bCompAll") And Sel2 <> "ALL" Then
    '        Session("bCompAll") = False

    '        'JRS 01/28/04
    '        RemoveDim1()

    '        Dim frm_msg As New frm_Message
    '        frm_msg.LoadMessage("This variable doesn't have the same dimension size chosen before")
    '        Support.FormManager.StateStore(Me)
    '        Support.FormManager.Show(Me, "frm_Message.aspx")

    '    End If

    '    'jrs 2005/08/18
    '    Dim rsGL As DataTable
    '    Dim idxGL As Integer = 0
    '    If FlagGList And Ind > 0 And Not bLoadCompHistAnal Then 'jrs 2005/11/28 added and not bLoadCompHistAnal
    '        SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = '" & cmb_DIM1.SelectedValue & "'", "", rsGL)
    '    End If

    '    Do While True 'jrs 2005/07/08 GList loop
    '        If FlagGList And Ind > 0 And Not bLoadCompHistAnal Then 'jrs 2005/11/28 added and not bLoadCompHistAnal
    '            Sel2 = rsGL.Rows(idxGL).Item("Region")
    '            FlagGroup = rsGL.Rows(idxGL).Item("Group")
    '            GroupOn = FlagGroup
    '            VPTR = 0
    '        End If 'eof flagglist
    '        'jrs 2006/04/10
    '        sDim_1 = Sel2
    '        Session("sDim_1") = sDim_1

    '        'mti Validation changes
    '        sPickHRegion = Sel2
    '        'mti Validation changes

    '        NullFound = False
    '        Label1 = cmb_DIM1.SelectedValue
    '        If FlagGList Then
    '            Label1 = Sel2
    '        End If

    '        'JRS 03/04/09
    '        Session("Label1") = Label1
    '        'JRS 01/28/04
    '        RemoveDim1()
    '        If Sel2 = "ALL" Then
    '            'Me.Hide        'Visible = False

    '            'FrameProgress.BringToFront() 
    '            FrameProgress.Visible = True
    '            'Me.Refresh() 

    '            'JRSVBNET ProgressBar1.Min = 1
    '            'JRSVBNET ProgressBar1.Max = MaxVal(DimNum) - 1
    '            'JRSVBNET ProgressBar1.Visible = True
    '            'JRSVBNET ProgressBar1 = 'JRSVBNET ProgressBar1.Min
    '            If FlagGroup Then
    '                For ii = 1 To GCount
    '                    ''ALL' is allowed only in case of Regions here
    '                    Label1 = GroupNames(ii)
    '                    'JRS 03/04/09
    '                    Session("Label1") = Label1
    '                    Label2 = ""
    '                    VPTR = ii
    '                    ''GoSub RemoveDim1:
    '                    DimPoint = ii 'Points to Region Number (Dim 1)
    '                    'DimPoint% = EleLoop
    '                    'JRS 5/21/02
    '                    GroupPoint = GroupCodes(ii)
    '                    Session("GroupPoint") = GroupPoint
    '                    'mti Validation changes
    '                    SeqRepeat = SeqRepeat + 1
    '                    'mti Validation changes

    '                    Call History_GridPut()
    '                    'JRSVBNET ProgressBar1 = ii

    '                Next ii
    '            Else

    '                For ii = 1 To MaxVal(DimNum) - 1
    '                    ''ALL' is allowed only in case of Regions here
    '                    Label1 = GRPS(DimNum, ii)
    '                    'JRS 03/04/09
    '                    Session("Label1") = Label1
    '                    Label2 = ""
    '                    VPTR = ii
    '                    ''GoSub RemoveDim1:
    '                    DimPoint = ii 'Points to Region Number (Dim 1)

    '                    'mti Validation changes
    '                    SeqRepeat = SeqRepeat + 1
    '                    'mti Validation changes

    '                    Call History_GridPut()
    '                    'JRSVBNET ProgressBar1 = ii

    '                Next ii
    '            End If
    '            'JRSVBNET ProgressBar1.Visible = False
    '            'JRSVBNET ProgressBar1 = 'JRSVBNET ProgressBar1.Min

    '            FrameProgress.Visible = False
    '            'FrameProgress.SendToBack() 


    '        Else
    '            Do While True
    '                Call History_GridPut()
    '                If CBool(Session("ShowVarDispList")) Then
    '                    If rsList.Rows(i).Item("Type") = "D" Then
    '                        Exit Do
    '                    End If
    '                    i = i + 1
    '                    If i = rsList.Rows.Count Then Exit Do

    '                    SE = rsList.Rows(i).Item("VarName")
    '                    SELN = UCase(SE)

    '                    SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict", "Variable", valtbl)
    '                    Dim j As Integer
    '                    For j = 0 To valtbl.Rows.Count - 1
    '                        TableName = valtbl.Rows(j).Item("Table")
    '                        GroupName = valtbl.Rows(j).Item("Group")
    '                        VarName = valtbl.Rows(j).Item("Variable")
    '                        Ag_rule = valtbl.Rows(j).Item("Aggregation")
    '                        Definition = valtbl.Rows(j).Item("Definition")
    '                        FullName = VarName & " - " & Definition
    '                        Label3 = valtbl.Rows(j).Item("Units")
    '                        If IsDBNull(Label3) Then Label3 = "Unit"

    '                        'Label3 = "Unit"
    '                        If CBool(Session("ShowVarDispList")) And SE = VarName Then
    '                            Exit For
    '                        End If
    '                    Next j
    '                Else
    '                    Exit Do
    '                End If
    '            Loop

    '        End If

    '        If NullFound Then

    '            Dim frm_msg As New frm_Message
    '            frm_msg.LoadMessage("There are some NULL values in the Data.")
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")

    '        End If

    '        'jrs 2005/08/18
    '        If FlagGList Then
    '            If Ind > 0 And Not bLoadCompHistAnal Then 'jrs 2005/11/28 added and not bLoadCompHistAnal
    '                idxGL = idxGL + 1
    '                If rsGL.Rows.Count = idxGL Then
    '                    FlagGroup = False
    '                    GroupOn = FlagGroup
    '                    Session("FlagGroup") = FlagGroup
    '                    Session("GroupOn") = GroupOn
    '                    Exit Do
    '                End If
    '            Else
    '                Exit Do
    '            End If
    '        Else
    '            Exit Do
    '        End If

    '    Loop 'jrs 2005/08/18 eof flagglist loop

    '    Exit Sub

    '    '    'mti Validation changes
    '    '    If bPickingValidation Then
    '    '        If DivOnPrev = False And MulOnPrev = False _
    '    ''         And AddOnPrev = False And SubOnPrev = False Then
    '    '            MsgBox "Please choose the forecast variable to check against.", vbOKOnly, "IFS - Validation"
    '    '            mnuHisVarOptSelForHisVar_Click
    '    '            bPickingValidation = True
    '    '            mnuHisVarOptSelForHisVar.Checked = False
    '    '        End If
    '    '    End If
    '    '    'mti Validation changes



    'End Sub

    'JRS 01/28/04
    Public Sub DimError(ByVal Sel2 As String, ByRef FoundDim As String)
        Dim Msg As String
        Msg = Sel2 & " is an incorrect sub-dimension choice"

        FoundDim = CStr(False)

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    'JRS 01/28/04
    Public Sub RemoveDim1()
        ' Make Combo Box and Label Invisible for Subdimension(s)
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_Dim1.Visible = False
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_DIM1.Items.Clear()
        lstDIM1.Items.Clear()
        lbl_VarName.Visible = True
        lbl_files.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
        lstFiles.Visible = True
        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
        ' JRS 11/24/01 DISPLAY but_File.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Enabled = False

        'jrs 2012/06/18
        ''jrs 2006/09/20
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    cmb_VCAT.Visible = False
        '    lbl_vcat.Visible = False
        'Else
        '    cmb_VCAT.Visible = True
        '    lbl_vcat.Visible = True
        'End If

        Return

    End Sub

    '=============================VarSel.Frm==============================
    'Public Sub History_GridPut()
    '    Dim Msg As String
    '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

    '    '   Increment Count and Bound
    '    '   -------------------------
    '    If DivOnPrev = False And MulOnPrev = False And AddOnPrev = False And SubOnPrev = False Then
    '        'counte=0 is automatically covered
    '        Session("COUNTE") = Session("COUNTE") + 1
    '        If Session("COUNTE") = 1 Then
    '            cswmWebMenu.Groups("mnuContinue").DownLevelURL = "frm_DisplayMenu.aspx"
    '        End If
    '        '        'mti Validation changes
    '        '        If bPickingValidation Then
    '        '          'MsgBox "Please choose the forecast variable to check against.", vbOKOnly, "IFS - Validation"
    '        '          Session("VARN")$(4, Session("COUNTE")) = "VH" & Trim(Str(Session("COUNTE")))
    '        '          Debug.Print "VH" & Trim(Str(Session("COUNTE")))
    '        '        End If
    '        '        'mti Validation changes

    '    End If

    '    If Session("COUNTE") > MaxVariables Then
    '        Session("COUNTE") = MaxVariables
    '        If GridFull <> "True" Then
    '            Msg = "Name list for display is full - exit and display"
    '            GridFull = "True"
    '            Dim frm_msg As New frm_Message
    '            frm_msg.LoadMessage(Msg)
    '            Support.FormManager.StateStore(Me)
    '            Support.FormManager.Show(Me, "frm_Message.aspx")

    '        End If
    '        GridFull = "True"
    '    End If

    '    Dim sDef As String, sUn As String
    '    'jrs 2006/04/07 Centrilized Data Load for Display
    '    If Session("bComputation") Then
    '        If Session("bCompAll") Then
    '            gLoadForDisplayHis(Session("SE"), sDim_1, idxDim1, sDim_2, idxDim2, sDim_3, idxDim3, _
    '            FlagGroup, 1, Session("PLOTAll"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"), False, _
    '            False, Session("bFillHis"), Session("bLoadForDisplaySuccess"), Me, Session("PutFdrvRun"), Session("IFsHistSeries_MDB"), _
    '            Session("IfsCoVaTraSeries_MDB"), Session("FinYear"), Session("sOriginForm"), Session("bCompAll"), _
    '            Session("iDimAll"), Session("LastYear"), Session("gidxComp"), Session("bComputation"), _
    '            Session("IFs_MDB2Active"), sDef, sUn)
    '        Else
    '            gLoadForDisplayHis(Session("SE"), sDim_1, idxDim1, sDim_2, idxDim2, sDim_3, idxDim3, _
    '            FlagGroup, 1, Session("PLOTComp"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"), True, _
    '            False, Session("bFillHis"), Session("bLoadForDisplaySuccess"), Me, Session("PutFdrvRun"), Session("IFsHistSeries_MDB"), _
    '            Session("IfsCoVaTraSeries_MDB"), Session("FinYear"), Session("sOriginForm"), Session("bCompAll"), _
    '            Session("iDimAll"), Session("LastYear"), Session("gidxComp"), Session("bComputation"), _
    '            Session("IFs_MDB2Active"), sDef, sUn)
    '        End If
    '    Else
    '        gLoadForDisplayHis(Session("SE"), sDim_1, idxDim1, sDim_2, idxDim2, sDim_3, idxDim3, _
    '        FlagGroup, 1, Session("PLOT"), Session("COUNTE"), Session("FirstYear"), Session("LastYear"), False, _
    '        False, Session("bFillHis"), Session("bLoadForDisplaySuccess"), Me, Session("PutFdrvRun"), Session("IFsHistSeries_MDB"), _
    '        Session("IfsCoVaTraSeries_MDB"), Session("FinYear"), Session("sOriginForm"), Session("bCompAll"), _
    '        Session("iDimAll"), Session("LastYear"), Session("gidxComp"), Session("bComputation"), _
    '        Session("IFs_MDB2Active"), sDef, sUn)
    '    End If
    '    If Not Session("bLoadForDisplaySuccess") Then
    '        Session("COUNTE") = Session("COUNTE") - 1
    '        Exit Sub
    '    End If
    '    gLoadForDisplayHisSaveLbls(Session("COUNTE"), Session("SE"), sDim_1, FlagGroup, sDef, sUn, "", Session("FileNumbr"), Session("VARN"), _
    '    Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"), _
    '    Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

    '    '
    '    ' FILL Grid
    '    '   ---------
    '    'JRS 05/07/04
    '    Dim iloop As Short
    '    grd_VariableDisplay.Columns.Clear()
    '    grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

    '    If Not (DivOnPrev Or MulOnPrev Or AddOnPrev Or SubOnPrev) Then 'set condition here
    '        'JRS 02/25/04
    '        For iloop = 1 To Session("COUNTE")

    '            Dim myCol As New TMSWebControls.Column
    '            'myCol.Type = TMSWebControls.Column.ColumnType.DataButton
    '            'myCol.Type = TMSWebControls.Column.ColumnType.Button
    '            myCol.WidthType = TMSWebControls.WType.Absolute
    '            'myCol.ButtonWidth = 100
    '            myCol.Width = 100
    '            myCol.Color = System.Drawing.Color.White
    '            'myCol.ButtonText = "ma"
    '            grd_VariableDisplay.Columns.Add(myCol)

    '            grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
    '            grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)
    '            grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
    '            grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
    '            grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop)

    '        Next iloop

    '    Else
    '        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
    '        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
    '        'JRSVBNET Support.DataGridManager.SetColWidth(grd_VariableDisplay, Session("COUNTE") - 1, 1.5 * Inch) 'Inch
    '        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("VARN")(1, Session("COUNTE")))
    '        'JRSVBNET Session("VARN")(0, Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
    '        'JRSVBNET Session("VARN")(1, Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
    '        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
    '        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 1)
    '        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("GR1N")(Session("COUNTE")))
    '        'JRSVBNET Session("GR1N")(Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
    '        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
    '        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 2)
    '        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("GR2N")(Session("COUNTE")))
    '        'JRSVBNET Session("GR2N")(Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
    '        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE") - 1)
    '        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 3)
    '        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay) & Session("GR3N")(Session("COUNTE")))
    '        'JRSVBNET Session("GR3N")(Session("COUNTE")) = 'JRSVBNET Support.DataGridManager.GetText(grd_VariableDisplay)
    '    End If

    '    'jrs 10/27/04 Flexible Dimension Names for Graphs
    '    Session("FDNG1")(Session("COUNTE")) = Session("GR1N")(Session("COUNTE"))
    '    Session("FDNG2")(Session("COUNTE")) = Session("GR2N")(Session("COUNTE"))
    '    Session("FDNG3")(Session("COUNTE")) = Session("GR3N")(Session("COUNTE"))
    '    Session("FDNG4")(Session("COUNTE")) = Session("GR4N")(Session("COUNTE"))

    '    'mti Validation changes
    '    ChkEndPickingHistory()

    'End Sub
    '=============================VarSel.Frm==============================
    Public Sub History_vcatClick(Optional ByVal catname As String = "") 'jrs 2014/03/20 added parameter catname
        Dim AddName As String
        Dim CoVaTrA_Var As String
        Dim Series As String
        Dim GroupName As String
        Dim GroupSelect As String = ""
        'jrs 10/30/04
        Dim VarName As String

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '
        '   Reload Variable Name Combo Box Based on Group Selected by User
        '   --------------------------------------------------------------
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        txtVarSearch.Text = ""
        txtVarSearch.Text = ""

        'jrs 2014/03/20
        ''jrs 10/29/04
        'If cmb_VCAT.SelectedIndex > 0 Then
        '    GroupSelect = VarGroupNames(cmb_VCAT.SelectedIndex - 1)
        'End If
        If catname > "" And UCase(catname) <> "ALL" Then
            GroupSelect = catname
        End If

        '    Set db = OpenDatabase(FDRV$ + "Data\datadict.MDB")
        SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict ORDER BY upper(Variable)", "Variable", valtbl)

        Dim sUnits As String
        Dim i As Integer
        For i = 0 To valtbl.Rows.Count - 1
            'jrs 10/30/04
            'TableName = valtbl.Rows(i).Item("Table")
            GroupName = valtbl.Rows(i).Item("Group")
            Series = valtbl.Rows(i).Item("Series")
            CoVaTrA_Var = valtbl.Rows(i).Item("CovaTrA")
            'JRS 10/14/03
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            If IsDBNull(valtbl.Rows(i).Item("Units")) Then
                sUnits = ""
            Else
                sUnits = valtbl.Rows(i).Item("Units")
            End If

            '        If Series$ = "Yes" And CoVaTrA_Var$ = "No" Then
            If Series = "Yes" Then
                If CoVaTrA_Var = "No" Or (CoVaTrA_Var = "Yes" And UCase(ModelLoad) = UCase("LoadCoVaTrA")) Then
                    VarName = valtbl.Rows(i).Item("Variable")
                    'Series$ = valtbl.Rows(i).Item("Series")
                    Definition = valtbl.Rows(i).Item("Definition")
                    If GroupName = GroupSelect Or GroupSelect = "" Then 'jrs 2014/03/20 cmb_VCAT.SelectedIndex < 1 Then 'jrs 2010/05/06 changed from = -1, 0 is All types
                        'JRS 10/14/03
                        AddName = VarName & " - " & Definition & " - " & sUnits
                        'jrs 2010/05/06
                        dataString = dataString + AddName + "|"

                        lstVarName.Items.Add(New ListItem(AddName))
                    End If
                End If
            End If

        Next i

    End Sub
    '=============================VarSel.Frm==============================
    Public Sub Upd_Disp_Menu()
        Dim j As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If Session("COUNTE") > 0 Then
            For j = 1 To Session("COUNTE")
                'UPGRADE_WARNING: Couldn't resolve default property of object j. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                FileNum = Val(Session("FileNumbr")(j))
                'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If FileNum = -1 Then
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.LineGraph.Visible = False
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.BarChart.Visible = False
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.PieChart.Visible = False
                    '               'JRSVBNET frm_DisplayMenu.Cohorts.Visible = False
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.ScatterPlot.Visible = False
                    '                'JRSVBNET frm_DisplayMenu.Report.Visible = False

                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.mnuHistory.Visible = True

                    Exit Sub
                End If
            Next j
        End If

        'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET frm_DisplayMenu.LineGraph.Visible = True
        'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET frm_DisplayMenu.BarChart.Visible = True
        'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET frm_DisplayMenu.PieChart.Visible = True
        '    'JRSVBNET frm_DisplayMenu.Cohorts.Visible = True
        'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
        'JRSVBNET frm_DisplayMenu.ScatterPlot.Visible = True
        '    'JRSVBNET frm_DisplayMenu.Report.Visible = True

        '    'JRSVBNET frm_DisplayMenu.mnuHistory.Visible = False

        'Modify Menu added by Mohammod-End

    End Sub

    Public Sub mnuDefine_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        'Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_NameDisp.aspx")
        'frm_NameDisp.DefInstance.Show() 
    End Sub

    Public Sub MnuDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As Object
        Dim curr_col As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        curr_col = Session("VarSelGridCol") + 1
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Session("VARN")(1, curr_col) & "<br>" & Session("VARN")(2, curr_col) & "<br>"
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + Session("GR1N")(curr_col) + "-" + Session("GR2N")(curr_col) + "-" + Session("GR3N")(curr_col)
        'UPGRADE_WARNING: Couldn't resolve default property of object Msg. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg + "<br>" + Chr(10) + Chr(10) + "Delete this varaiable?"
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show(Msg, MsgBoxStyle.YesNo, "Variable Selected") = MsgBoxResult.Yes Then
            DeleteVar((curr_col))
            'jrs 2005/12/20 RAM for Formulas
            ''JRS 9/14/01 COMPUTATIONS
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "DELETE * FROM [Formulas] WHERE [Code] = " & Str(curr_col - 1))
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "UPDATE [Formulas] SET Code = Code - 1 WHERE [Code] >= " & Str(curr_col) & " AND Code < 1000")
            ''jrs 2005/11/17
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "DELETE * FROM [Formulas] WHERE [Code] = " & Str(curr_col + 999))
            'updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", "UPDATE [Formulas] SET Code = Code - 1 WHERE [Code] >= " & Str(curr_col + 1000) & " AND Code < 2000")
        End If

    End Sub

    Public Sub mnuDrivers_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN ***
        Dim curr_col As Integer
        ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim pos1 As Short

        curr_col = Session("VarSelGridCol") + 1

        pos1 = InStr(Session("VARN")(1, curr_col), "[")
        Session("myWidth") = txtWidth.Value
        Session("myHeight") = txtHeight.Value
        Session("DrvDim1") = Session("GR1N")(curr_col)
        Session("DrvDim2") = Session("GR2N")(curr_col)
        Session("DrvDim3") = Session("GR3N")(curr_col)
        If pos1 > 0 Then
            Session("DrvFl") = getFileNum(Session("VARN")(1, curr_col), Session("vShortSce"))
        Else
            Session("DrvFl") = 0
        End If
        If pos1 > 0 Then
            Support.FormManager.Show(Me, "frm_Drivers.aspx?VName=" & Mid(Session("VARN")(1, curr_col), 1, pos1 - 1))
        Else
            Support.FormManager.Show(Me, "frm_Drivers.aspx?VName=" & Session("VARN")(1, curr_col))
        End If

    End Sub

    Public Sub mnuExplain_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim curr_col As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        curr_col = Session("VarSelGridCol")
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_ISSUE: VBControlExtender property grd_VariableDisplay.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'On HelpProvider extender control; accessed by SetHelpKeyword method of HelpProvider control.
        'grd_VariableDisplay.HelpContextID = Val(Session("VARN")(3, curr_col))
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.SendKeys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.SendKeys.Send("{F1}")

    End Sub

    '=============================VarSel.Frm==============================
    'public Sub CurrencyCheck()
    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    '   JRS 5-1-01
    '- - - - - - - - - - - - - - By Jose Solorzano - - - - - - - - - - - - - - -
    '- - - - - - - - - - - - - - RA Working for Barry Hughes - - - - - - - - - - - - - - -
    '- - - - - - - - - - - - - - 5-01-01 - - - - - - - - - - - - - - -
    ' If it has not been already checked
    '    If Not bChkCurr Then
    'Checks if the flag for this variable is ON
    '        bCnvrtCurr = VCurrency(Session("SEL"))
    'If the option is ON procede
    '        If bCnvrtCurr Then
    'Opens the DataBase
    '            Set db = DBEngine.OpenDatabase(PutFdrv$ + "RUNFILES\IFS.MDB")
    'Declares and opens a RecordSet with configuration values
    '            Dim rs As Recordset
    '            Set rs = db.OpenRecordset("SELECT * FROM [Currency] WHERE [Currency Flag]=True")
    'If it returns information, load it to the variables
    '            If rs.RecordCount > 0 Then
    'Updates the Currency Factor
    '                CurrencyFct = rs.Rows(i).Item("Currency Value")
    'Update the name of the Units
    '                Dim MyPos As Long
    '                Dim S As String
    '                Dim s2 As String
    '                s2 = VUnits(Session("SEL"))
    '                MyPos = InStr(1, s2, sOriginalSym, vbTextCompare)
    '                S = RTrim(rs.Rows(i).Item("Symbol"))
    '                If MyPos > 0 Then
    '                    sOriginalSym = S
    '                    VUnits(Session("SEL")) = left(s2, MyPos - 1) + S + right(s2, Len(s2) - MyPos)
    '                End If
    '            Else
    '                CurrencyFct = 1
    '            End If
    'Closes the Database
    '            rs.Close
    '            db.Close
    '        Else
    '            CurrencyFct = 1
    '        End If
    'Sets the flag to true once it entered
    '        bChkCurr = True
    '    End If

    'End Sub

    '=============================VarSel.Frm==============================
    Public Sub CurrencyCheck()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   JRS 5-1-01
        '- - - - - - - - - - - - - - By Jose Solorzano - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - RA Working for Barry Hughes - - - - - - - - - - - - - - -
        '- - - - - - - - - - - - - - 5-01-01 - - - - - - - - - - - - - - -
        'Checks if the flag for this variable is ON
        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_WARNING: Couldn't resolve default property of object VCurrency(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        bCnvrtCurr = VCurrency(Session("SEL"))
        'If the option is ON procede
        If bCnvrtCurr Then
            'jrs 2005/12/09 ReadCurrency()
            'If it returns information, load it to the variables
            If rsCurr.Rows.Count > 0 Then
                'Updates the Currency Factor
                CurrencyFct = rsCurr.Rows(0).Item("Currency Value")
                'JRS 6/19/01 CurrencyLableSwitch
            Else
                CurrencyFct = 1
            End If
        Else
            CurrencyFct = 1
        End If
        'Sets the flag to true once it entered
        bChkCurr = True
        'jrs 2005/12/06
        CurrencyFctAll = CurrencyFct
    End Sub

    '=============================VarSel.Frm==============================
    Public Sub CurrencyLabelSwitch()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'If it returns information, load it to the variables
        Dim MyPos As Integer
        Dim S As String
        Dim s2 As String
        If rsCurr.Rows.Count > 0 Then
            'Update the name of the Units
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object VUnits(Session("SEL")). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            s2 = VUnits(Session("SEL"))
            'JRS 6/19/01 Using an Array for the Original Symbol
            'of every variable
            'MyPos = InStr(1, s2, VOriginalSym(Session("SEL")), vbTextCompare)
            MyPos = InStr(1, s2, "$", CompareMethod.Text)
            S = RTrim(rsCurr.Rows(0).Item("Symbol"))
            If MyPos > 0 Then
                'JRS 6/19/01 Using an Array for the Original Symbol
                'of every variable
                'VOriginalSym(Session("SEL")) = S
                'VUnits(Session("SEL")) = left(s2, MyPos - 1) + S + right(s2, Len(s2) - MyPos)
                Session("GR3N")(Session("COUNTE")) = VB.Left(s2, MyPos - 1) & S & VB.Right(s2, Len(s2) - MyPos)
            End If
        End If
    End Sub


    '=============================VarSel.Frm==============================
    Public Sub DeleteVar(ByVal curr_col As Short)
        Dim iloop As Short
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 9/10/01 CREATED FOR COMPUTATIONS
        ' Delete Variable
        Session("COUNTE") = Session("COUNTE") - 1 ''Do later?
        'jrs 2006/09/25 use all his
        Dim iLstYr As Integer
        iLstYr = Session("LastYear")
        If Session("buseAllHis") And UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
            For i2 = curr_col To Session("COUNTE") + 1
                If Session("FinYearHis")(i2) > iLstYr Then
                    iLstYr = Session("FinYearHis")(i2)
                End If
            Next i2
        End If

        If Session("COUNTE") = 0 And Not (cswmWebMenu Is Nothing) Then
            'jrs 2006/11/15 cswmWebMenu.Groups("mnuContinue").DownLevelURL = "frm_MainMenu.aspx"
            cswmWebMenu.Groups("mnuContinue").OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        End If
        For i2 = 1 To iLstYr - Session("FirstYear") + 1
            For ICount = curr_col To Session("COUNTE")
                Session("PLOT")(i2, ICount) = Session("PLOT")(i2, ICount + 1)
            Next ICount
            Session("PLOT")(i2, Session("COUNTE") + 1) = 0
        Next i2

        'JRS 9/24/01 COMPUTATIONS
        Dim j1 As Object
        Dim uaf As Short
        uaf = UBound(Session("aFormulas")) - 1
        If Session("COUNTE") + 1 < uaf Then uaf = Session("COUNTE") + 1
        For j1 = curr_col To uaf  'jrs 2005/08/18 remove + 1 from curr_col
            'JRS 04/15/03
            If Not (Session("aFormulas")(j1 + 1) Is Nothing) Then
                Session("aFormulas")(j1) = Session("aFormulas")(j1 + 1)
            End If
        Next j1

        ' Take care of Grid Display & Array Variables Shifting
        For iloop = curr_col To Session("COUNTE") Step 1
            Session("FileNumbr")(iloop) = Session("FileNumbr")(iloop + 1)
            Session("VARN")(0, iloop) = Session("VARN")(0, iloop + 1)
            Session("VARN")(1, iloop) = Session("VARN")(1, iloop + 1)
            Session("VARN")(2, iloop) = Session("VARN")(2, iloop + 1)
            Session("VARN")(3, iloop) = Session("VARN")(3, iloop + 1)
            Session("VARN")(6, iloop) = Session("VARN")(6, iloop + 1)
            'mti Validation changes
            Session("VARN")(4, iloop) = Session("VARN")(4, iloop + 1)
            'mti Validation changes
            'mti Validation changes 19 August, 2002
            Session("VARN")(5, iloop) = Session("VARN")(5, iloop + 1)
            'mti Validation changes 19 August, 2002

            Session("GR1N")(iloop) = Session("GR1N")(iloop + 1)
            Session("GR2N")(iloop) = Session("GR2N")(iloop + 1)
            Session("GR3N")(iloop) = Session("GR3N")(iloop + 1)
            Session("GR4N")(iloop) = Session("GR4N")(iloop + 1)

            'jrs 10/27/04 Flexible Dimension Names for Graphs
            Session("FDNG1")(iloop) = Session("FDNG1")(iloop + 1)
            Session("FDNG2")(iloop) = Session("FDNG2")(iloop + 1)
            Session("FDNG3")(iloop) = Session("FDNG3")(iloop + 1)
            Session("FDNG4")(iloop) = Session("FDNG4")(iloop + 1)
            'jrs 2006/09/25 Use all available history
            Session("FinYear")(iloop) = Session("FinYear")(iloop + 1)
            Session("FinYearHis")(iloop) = Session("FinYearHis")(iloop + 1)
        Next iloop
        Session("VARN")(0, Session("COUNTE") + 1) = ""
        Session("VARN")(1, Session("COUNTE") + 1) = ""
        Session("VARN")(2, Session("COUNTE") + 1) = ""
        Session("VARN")(3, Session("COUNTE") + 1) = ""
        Session("VARN")(6, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        Session("VARN")(4, Session("COUNTE") + 1) = ""
        'mti Validation changes July 28, 2002
        'mti Validation changes 19 August, 2002
        Session("VARN")(5, Session("COUNTE") + 1) = ""
        'mti Validation changes 19 August, 2002

        Session("GR1N")(Session("COUNTE") + 1) = ""
        Session("GR2N")(Session("COUNTE") + 1) = ""
        Session("GR3N")(Session("COUNTE") + 1) = ""
        Session("GR4N")(Session("COUNTE") + 1) = ""

        'jrs 10/27/04 Flexible Dimension Names for Graphs
        Session("FDNG1")(Session("COUNTE") + 1) = ""
        Session("FDNG2")(Session("COUNTE") + 1) = ""
        Session("FDNG3")(Session("COUNTE") + 1) = ""
        Session("FDNG4")(Session("COUNTE") + 1) = ""

        Label1 = ""
        'JRS 03/04/09
        Session("Label1") = Label1
        Label2 = ""
        Label3 = ""

        'jrs 2006/10/25
        If Not (grd_VariableDisplay Is Nothing) Then
            GridRefresh()
        End If
    End Sub

    '=============================VarSel.Frm==============================
    Public Sub LoadFiles()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/24/01 DISPLAY
        Dim fs 'As New Scripting.FileSystemObject
        Dim BASE 'As Scripting.File
        Dim currRun 'As Scripting.File
        ''Dim fRunFile As Folder
        fs = Server.CreateObject("Scripting.FileSystemObject")
        'Set fRunFile = fs.GetFolder(PutFdrvRun2$)
        'fRunFile.Files returns collection of files
        Dim sWorkingFile As String
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        lstFiles.Items.Clear()
        Session("FilesSelected") = 1
        sWorkingFile = VB.Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        Dim bHis As Boolean = False 'jrs 2020/06/30

        'JRS 05/20/03
        If Session("FirstYear") = FirstYearHistory And Session("LastYear") = LastYearHistory Then
            'jrs 2021/12/21
            If StandAloneMode = True Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBaseHist.run.db")
            End If
            bHis = True
        Else
            'jrs 2021/12/21
            If StandAloneMode = True Then
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run.db")
            Else
                BASE = fs.GetFile(Session("PutFdrvRun2") & "\IFsBase.run.db")
            End If
        End If

        ''CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        lstFiles.Items.Add(New ListItem("0 - " & "Working File, based on " & sWorkingFile))
        lstFiles.Items.Add(New ListItem("1 - " & BASE.Name))

        Dim sRunFile As String
        '' Returns filename with specified extension. If more than one *.ini
        '' file exists, the first file found is returned.
        'jrs 2021/12/21
        If StandAloneMode = True Then
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")
        Else
            sRunFile = Dir(Session("PutFdrvRun2") & "\*.run.db")
        End If


        Do While (sRunFile <> "")
            'jrs 2022/03/23 added And UCase(sRunFile) <> "WORKING.RUN.DB" 
            'jrs 2022/05/25 removed And UCase(sRunFile) <> UCase(sWorkingFile)
            If UCase(sRunFile) <> UCase(BASE.Name) And UCase(sRunFile) <> "WORKING.RUN" And
                UCase(sRunFile) <> "WORKING.RUN.DB" And UCase(sRunFile) <> "IFSBASEHIST.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") & "\" & sRunFile)
                'jrs 2022/01/12 If BASE.Size = currRun.Size Then
                If Session("IFsRunFiles").item(sRunFile).Visible Or (StandAloneMode And Not bHis) Then 'jrs 2005/06/27
                    lstFiles.Items.Add(New ListItem(Trim(Str(lstFiles.Items.Count)) & " - " & sRunFile))
                    Session("FilesSelected") = Session("FilesSelected") + 1
                    'JRS RUNFILES 5/11/02
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") & "\" & sRunFile
                    'jrs 2005/02/25
                    If Session("FilesSelected") > UBound(Session("ShortSce")) Then
                        ReDim Preserve Session("ShortSce")(Session("FilesSelected"))
                    End If
                    Dim k As Integer
                    k = getLongSceIdx(sRunFile)
                    If k > -1 Then
                        Session("ShortSce")(Session("FilesSelected")) = ShortSce(k)
                    Else
                        'jrs 2008/05/13 Session("ShortSce")(Session("FilesSelected")) = Left(sRunFile, 8)
                        Session("ShortSce")(Session("FilesSelected")) = TruncateSceFileName(sRunFile)
                    End If
                End If
                'jrs 2022/01/12 End If
            End If
            '    ' Call Dir again without arguments to return the next *.INI file in the
            '    ' same directory.
            sRunFile = Dir()
        Loop
        'jrs 2026/01/26 changed to base case
        'jrs 2006/09/06 Multiple Files
        lstFiles.Items(1).Selected = True
    End Sub

    Public Sub mnuHisVarOptSelEmpHisVar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 10/29/04
        Session("lstVarNameSelectedIndex") = Nothing


        'JAM 2014-04-19 chosen Empirical variables set
        Session("VEmpOrForeMode") = 1
        fEmpOrForeMode.Value = Session("VEmpOrForeMode")
        Session("VHistoricAnalogMode") = 1


        Dim NameFound As String
        Dim AddName As String
        Dim CoVaTrA_Var As String
        Dim Series As String
        Dim GroupName As String
        Dim GroupNameNum As Object
        Dim ICount As Short
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/25/01 DISPLAY
        Dim X As Short
        X = 1
        Dim sUnits As String
        'jrs 2023/07/12
        'it's possible that in display you changed the firstyear
        If Session("HistoricBase") = CStr(True) AndAlso Session("FirstYear") <> FirstYearHistory Then
            Session("FirstYear") = FirstYearHistory
        End If

        If Session("FirstYear") <> FirstYearHistory Then
            Msg = "You must open the History Base before you can select historical variables "
            Msg = Msg & "to compare with it; go to File-Open to do so. "

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        Else
            Session("DoingHistory") = True
            'JRS 11/25/01 DISPLAY but_history.Caption = "Using &History"
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuHisVarOptSelEmpHisVar.Checked = True 
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'Me.mnuHisVarOptSelForHisVar.Checked = False 
            cswmWebMenu.Groups("mnuHisVarOpt").Items("mnuHisVarOptSelForHisVar").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuHisVarOpt").Items("mnuHisVarOptSelEmpHisVar").IconImage = "checkbox.gif"
            Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar"

            ' JRS 11/24/01 DISPLAY Txt_files.Text = ""
            DefaultFile = -1
            ' JRS 11/24/01 DISPLAY Txt_files.Text = "-1 History Data from IFS_HistSeries.MDB"
            'LoadFiles
            lstFiles.Items.Clear()
            lstFiles.Items.Add(New ListItem("-1 History Data from IFS_HistSeries.MDB"))
            lstFiles.Items(0).Selected = True

            Session("FirstYear") = FirstYearHistory
            Session("LastYear") = LastYearHistory

            cmb_VCAT.Items.Clear()
            VarGroupNames(1) = "ALL"
            'jrs 2005/04/29 cmb_VCAT.Items.Add(New ListItem(""))


            'KIA 09/22/2009 comment THE FOLLOWING LINE
            'cmb_VCAT.Items.Add(New ListItem("ALL"))
            For ICount = 2 To 100
                VarGroupNames(ICount) = ""
            Next ICount
            Session("CCount") = 200
            DivOnPrev = False
            MulOnPrev = False
            AddOnPrev = False
            SubOnPrev = False

            '   Set up labels and options
            '   -------------------------
            'msg$ = "Select Independent Variable and Data Table (e.g. GDP - Economic):"
            'lbl_VarName.Caption = msg$
            lbl_vcat.Text = "To Show a Subset of Names:"


            '   Load Variable Group Name Combo Box
            '   ----------------------------
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRSVBNET cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text = ""
            'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            GroupNameNum = 0


            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

            lstVarName.Items.Clear()
            '    Set db = OpenDatabase(FDRV$ + "Data\datadict.MDB")
            SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict ORDER BY upper(Variable)", "Variable", valtbl)


            'JAM 2014-06-08
            ReDim Session("ArraySearch")(valtbl.Rows.Count)


            'jrs 2023/07/12
            Dim idx As Integer = 0
            'JRS 08/29/03
            Dim i As Integer
            For i = 0 To valtbl.Rows.Count - 1
                'jrs 2008/12/09
                If IsDBNull(valtbl.Rows(i).Item("Table")) Then
                    TableName = ""
                Else
                    TableName = valtbl.Rows(i).Item("Table")
                End If

                'jrs 2008/12/08
                If IsDBNull(valtbl.Rows(i).Item("Group")) Then
                    GroupName$ = "No Group"
                Else
                    GroupName = valtbl.Rows(i).Item("Group")
                End If

                'jrs 2008/12/09
                If IsDBNull(valtbl.Rows(i).Item("Series")) Then
                    Series = ""
                Else
                    Series = valtbl.Rows(i).Item("Series")
                End If
                If IsDBNull(valtbl.Rows(i).Item("CovaTrA")) Then
                    CoVaTrA_Var = ""
                Else
                    CoVaTrA_Var = valtbl.Rows(i).Item("CovaTrA")
                End If

                'JRS 08/29/03
                'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
                If IsDBNull(valtbl.Rows(i).Item("Units")) Then
                    sUnits = ""
                Else
                    sUnits = valtbl.Rows(i).Item("Units")
                End If
                '        If Series$ = "Yes" And CoVaTrA_Var$ = "No" Then
                If Series = "Yes" Then
                    If CoVaTrA_Var = "No" Or (CoVaTrA_Var = "Yes" And UCase(ModelLoad) = UCase("LoadCoVaTrA")) Then
                        If IsDBNull(valtbl.Rows(i).Item("Variable")) Then
                            VarName = ""
                        Else
                            VarName = valtbl.Rows(i).Item("Variable")
                        End If

                        'Series$ = valtbl.rows(i).Item("Series")
                        If IsDBNull(valtbl.Rows(i).Item("Definition")) Then
                            Definition = ""
                        Else
                            Definition = valtbl.Rows(i).Item("Definition")
                        End If


                        'JRS 08/29/03
                        AddName = VarName & " - " & Definition & " - " & sUnits

                        'JAM 2014-08-03 Removing extra characters
                        AddName = Replace(Replace(AddName, "   ", " "), "  ", " ")
                        AddName = Replace(Replace(Replace(AddName, Chr(10), ""), Chr(13), ""), """", "")

                        'AddName$ = VarName$ + " - " + GroupName$ + " (" + Definition$ + ")"
                        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

                        'JAM 2014-06-08
                        '' lstVarName.Items.Add(New ListItem(AddName))
                        'JAM 2014-06-08 Reloading ArraySearch
                        'jrs 2023/07/12 Session("ArraySearch")(i) = AddName
                        Session("ArraySearch")(idx) = AddName
                        idx = idx + 1

                        'VTracker(ICount%) = j 'don't need this
                        NameFound = CStr(False)
                        For ICount = 1 To 100
                            If GroupName = VarGroupNames(ICount) Then NameFound = CStr(True)
                        Next ICount
                        If CBool(NameFound) = False Then
                            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

                            'jrs 2014/03/20 if statement was missing
                            'KIA 08/11/2009 replace the cmb_VCAT.AddItem GroupName$ line with following if block.
                            If InStr(GroupName$, ",") = 0 Then
                                cmb_VCAT.Items.Add(New ListItem(GroupName)) 'Populate Session("Category") Combo If New Session("Category")
                            End If

                            'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            GroupNameNum = GroupNameNum + 1
                            'UPGRADE_WARNING: Couldn't resolve default property of object GroupNameNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                            VarGroupNames(GroupNameNum) = GroupName
                        End If
                    End If

                End If

            Next i

            'jrs 2014/03/20
            cmb_VCAT.Items.Insert(0, "All")
            SortCategories()

            'JAM 2014-06-08 Reloading ArraySearch
            Session("CountItems") = i

            'JAM 2014-06-08 Reloading ArraySearch
            ''FillSearchArray(("Full"))



        End If 'Checking Firstyear%

        'mti Validation changes
        bPickingValidation = False
        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        Session("lstVarNameSelectedIndex") = Nothing
        'jrs 2006/06/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = True
        'jrs 2012/06/15
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = True
        'jrs 2006/09/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = True
    End Sub

    Public Sub mnuHisVarOptSelForHisVar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 10/29/04
        Session("lstVarNameSelectedIndex") = Nothing

        'JAM 2014-04-19 chosen Forecasted variables set
        Session("VEmpOrForeMode") = 0
        fEmpOrForeMode.Value = Session("VEmpOrForeMode")


        Dim Msg As String
        Dim nvcat As Object
        Dim AddName As String
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/25/01 DISPLAY
        Dim X As Short
        X = 1
        'jrs 2005/11/08
        Session("DoingHistory") = False

        'JRS 11/25/01 DISPLAY but_history.Caption = "Using &Forecast"
        cswmWebMenu.Groups("mnuHisVarOpt").Items("mnuHisVarOptSelEmpHisVar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuHisVarOpt").Items("mnuHisVarOptSelForHisVar").IconImage = "checkbox.gif"
        Session("VarSelCheckHis") = "mnuHisVarOptSelForHisVar"

        '   Load Variable Name Combo Box
        '   ----------------------------

        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

        lstVarName.Items.Clear()
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRS 10/29/04 cmb_VarName.Items(cmb_VarName.SelectedIndex).Text = ""
        ICount = 0

        'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        For i2 = 1 To NumbVars
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            AddName = V(i2)
            'jrs 2005/11/08
            AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
            'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                'JRS 1/18/02 DISPLAY If CoVaTrA$ = True Or VCat(i2) <> 9 Then
                'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

                'JAM 2014-06-08
                ''lstVarName.Items.Add(New ListItem(AddName))
                ICount = ICount + 1
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                'UPGRADE_WARNING: Couldn't resolve default property of object VTracker(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                VTracker(ICount) = i2
            End If
            'Strip out automobile variables except for GM
        Next i2

        'JAM 2014-06-08 Reloading ArraySearch
        FillSearchArray(("Full"))


        '   Load Variable Session("Category") Combo Box
        '   --------------------------------
        lbl_vcat.Text = "To Show a Subset of Names:"
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRSVBNET cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text = ""
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_VCAT.Items.Clear()
        'jrs 2005/04/29 cmb_VCAT.Items.Add(New ListItem(""))
        'jrs 2005/02/12
        'KIA 09/22/2009 comment until find end
        'Dim fNum As Integer
        'fNum = FreeFile()
        'FileOpen(fNum, DataFileDir & VCATFile, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
        'Input(fNum, nvcat)
        ''UPGRADE_WARNING: Couldn't resolve default property of object nvcat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'For i2 = 1 To nvcat
        '    Input(fNum, AddName)
        '    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        '    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or AddName <> "Transportation" Then cmb_VCAT.Items.Add(New ListItem(AddName))
        '    'JRS 1/18/02 DISPLAY If CoVaTrA$ = True Or_
        '    'AddName$ <> "Transportation" Then cmb_VCAT.AddItem AddName$
        'Next i2
        'FileClose(fNum)
        'KIA 09/22/2009 END COMMENT
        'KIA 09/22/2009 add the following line


        LoadCategories()


        '
        '   Hide Dimension 1 Combo Box
        '   --------------------------
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'JRS 02/25/04
        If cmb_DIM1.SelectedIndex >= 0 Then
            cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = ""
        End If
        SaveDim1 = ""
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_DIM1.Items.Clear()
        lstDIM1.Items.Clear()

        '   Set up labels
        '   -------------
        lbl_Dim1.Visible = False
        Msg = "Touch arrow to activate selection box; type name or "
        Msg = Msg & "click on name; then touch Enter key"
        lbl_VarName.Text = Msg

        'mti Validation changes
        lbl_Dim1.ForeColor = System.Drawing.ColorTranslator.FromOle(QBColor(0))
        'mti Validation changes

        lbl_Dim1.Text = "Click arrow to activate dimension box; type of click name; touch enter"
        Msg = "Default File:"
        lbl_files.Text = Msg


        '   Initialize selection
        '   --------------------
        SubDimDone = "False"
        SEL1 = ""
        Session("SEL1") = SEL1
        '   Initialize calculations and sequences
        '   -------------------------------------
        CalcRunning = False
        'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        CalcSave = "None"
        'UPGRADE_WARNING: Couldn't resolve default property of object SeqOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        SeqOn = "False"
        'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        SeqRepeat = 0

        '   Establish List Box
        '    -----------------
        Msg = Str(0) & " Working File, based on "
        Msg = Msg & Session("WorkingFile")
        ' JRS 11/24/01 DISPLAY Txt_files.Text = ""
        ' JRS 11/24/01 DISPLAY Txt_files.Text = msg$
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        lstFiles.Items.Clear()
        LoadFiles()

        'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        DefaultFile = 0

        lbl_VarName.Visible = True
        lbl_files.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
        lstFiles.Visible = True
        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
        '' JRS 11/24/01 DISPLAY but_File.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Enabled = False
        lbl_vcat.Visible = True

        'frm_VarSel.Show
        cmb_VCAT.Visible = True
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True


        'mti Validation changes
        bPickingValidation = False

        'JRS 03/21/04
        Session("VTracker") = VTracker

        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        Session("lstVarNameSelectedIndex") = Nothing
        'jrs 2006/06/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = False
        'jrs 2012/06/15
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = False

        'jrs 2006/09/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = False
    End Sub


    Public Sub mnuOpen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2006/04/14
        Session("PDCategoriesIdxs") = Nothing
        Session("PDFlexCategoriesIdxs") = Nothing
        Session("PDFilesIdxs") = Nothing

        'jrs 2005/02/17
        Dim bFlag As Boolean
        bFlag = (Session("SessionIdx") = -1)
        'jam 2021/11/14
        'jrs 2004/11/23
        'SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"), _
        'Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"), _
        'Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"), _
        'Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory), _
        'Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"), _
        'Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"), _
        'Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"))
        'jam 2021/11/14
        SessionAllocator(Session("SessionIdx"), Me, Session("ResFileSubDir"), Session("ScenarioFileSubDir"),
        Session("SensitivityFileSubDir"), Session("FdrvRun"), Session("PutFdrvRun"), Session("PutFdrvRun2"),
        Session("valtblInit"), Session("IFs_MDB2"), Session("IFsHist_MDB2"), Session("IFsCoVaTrA_MDB2"),
        Session("ScenarioFileDir"), Session("SensitivityFileDir"), Session("Error"), Me.MapPathSecure(TemplateSourceDirectory),
        Session("IFsRunFiles"), Session("IFsHistSeries_MDB"), Session("IFsCoVaTrASeries_MDB"),
        Session("IFsWVSCohort_MDB"), Session("IFsStat_MDB"), Session("WorkingFile"), Session("FilesSelected"),
        Session("RunFiles"), Server, Session("HistoricBase"), Session("ActiveBase"), Session("IFs_MDB2Active"), Session("mnuSQLite"))

        'jrs 2005/02/17
        If bFlag Then
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = Replace(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "-1", Session("SessionIdx"))
        End If


        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 04/25/02
        Dim sFileName As String
        '   CancelError is True
        '   -------------------

        On Error GoTo ErrHandler3

        If cswmWebMenu.Groups("mnuOpen").Caption = "Open IFsBaseHist" Then
            'jrs 2023/01/24
            If UseOnlySQLite Then
                sFileName = Session("PutFdrvRun2") & "\IFsBaseHist.run.db"
            Else
                sFileName = Session("PutFdrvRun2") & "\IFsBaseHist.run"
            End If
            'jam 2021/11/14
            'Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            'Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
            'Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
            'Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"))
            'Session("WorkingFile") = sFileName
            'jam 2021/11/14
            Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
            Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
            Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))
            Session("WorkingFile") = sFileName


            'JRS 1/6/01 DISPLAY
            'Reload$ = "True"
            cswmWebMenu.Groups("mnuOpen").Caption = "Open IFsBase"
            'Session("COUNTE") = 0
            Session("VarSelCheckHis") = "mnuHisVarOptSelForHisVar"
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        Else
            'JRS 10/26/03
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
            If Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar" Then
                mnuHisVarOptSelForHisVar_Click(Me, New System.EventArgs)
            End If
            'jrs 2023/01/24
            If UseOnlySQLite Then
                sFileName = Session("PutFdrvRun2") & "\IFsBase.run.db"
            Else
                sFileName = Session("PutFdrvRun2") & "\IFsBase.run"
            End If
            'jam 2021/11/14
            'Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            'Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
            'Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
            'Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"))
            'jam 2021/11/14
            Function2.OpenFile(sFileName, Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("PutFdrvRun"), Session("WorkingFile"), Session("RunFiles"), Session("HistoricBase"), Session("ActiveBase"), Session("FirstYear"),
            Session("LastYear"), Session("LastYearForecast"), Session("FilesSelected"), Session("DisplayLastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"),
            Session("ScenarioFileDir"), Session("FinYear"), Session("SessionIdx"), Session("FinYearHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("mnuSQLite"), Session("sUsrDefCats"))
            Session("WorkingFile") = sFileName


            'JRS 1/6/01 DISPLAY
            'Reload$ = "True"
            cswmWebMenu.Groups("mnuOpen").Caption = "Open IFsBaseHist"
            'Session("COUNTE") = 0
            'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        End If

        cswmWebMenu.Groups("mnuHisVarOpt").ShowMenuBarItem = (cswmWebMenu.Groups("mnuOpen").Caption <> "Open IFsBaseHist")
        'jrs 2006/11/21
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = (Session("HistoricBase") = CStr(True))
        'jrs 2012/06/15 
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = (Session("HistoricBase") = CStr(True))
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = (Session("HistoricBase") = CStr(True))

        'JRS 05/13/03
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            'mnuVarSelOptHistoricPlusForecast.Checked = False
            mnuVarSelOptShowVarPar_Click(Me, New System.EventArgs)
        End If
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").Enabled = Not (cswmWebMenu.Groups("mnuOpen").Caption <> "Open IFsBaseHist")

        'JRS 11/24/01 DISPLAY
        LoadFiles()
        GridRefresh()

        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        Session("lstVarNameSelectedIndex") = Nothing
        'jrs 2006/09/20
        lstFiles.Visible = True
        lbl_files.Visible = True
        cmb_VCAT.Visible = True
        lbl_vcat.Visible = True
        'JRS 2005/11/16
        Session("PDPacksIdx") = Nothing
        'jrs 2006/03/07
        Session("PDFlexPacksIdxs") = Nothing
        Session("PDCategoriesIdxs") = Nothing
        Session("PDFlexCategoriesIdxs") = Nothing
        'jrs 2013/09/06
        ReDim Preserve Session("PDFilesIdxs")(lstFiles.Items.Count - 1)

        Session("PDFilesIdxs")(0) = Nothing

        Exit Sub

ErrHandler3:  ' User Pressed Cancel
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default
        Exit Sub

    End Sub

    Public Sub mnuOpenforValidation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Found As String
        Dim Lno As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes 19 August, 2002- take total procedure

        Dim sFlagGroup As Boolean
        Dim iFileNo As Short
        Dim VFVar, VHVar, VGroupOn, VRegion, VFDim2 As String
        Dim RLine As String
        sFlagGroup = FlagGroup

        'mti Validation changes 10 August, 2002
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuSelforValidation.Checked = False 
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuHisVarOptSelEmpHisVar.Checked = False 
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuHisVarOptSelForHisVar.Checked = False 
        'mti Validation changes 10 August, 2002


        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuOpenforValidation.Checked = True 
        bPickingValidation = True

        '    'reset at finish
        '    mnuHisVarOptSelForHisVar.Checked = False
        '    mnuHisVarOptSelEmpHisVar.Checked = False

        'JRSVBNET CMDialog1.FileName = ""

        '   CancelError is True
        '   -------------------

        On Error GoTo CancelError

        'JRSVBNET CMDialog1.InitDir = PutFdrv & "Validation" 'PutFdrvRun2$
        'JRSVBNET CMDialog1.DialogTitle = "Select Validation Set"
        'JRSVBNET CMDialog1.Filter = "Validation Files (*.val)|*.val"
        'JRSVBNET CMDialog1.Action = 1

        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.WaitCursor

        'Open file
        iFileNo = FreeFile()
        'JRSVBNET FileOpen(iFileNo, 'JRSVBNET CMDialog1.FileName, OpenMode.Input)
        On Error GoTo FileReadError
        'mti Validation changes 19 August, 2002
        'UPGRADE_WARNING: Couldn't resolve default property of object Lno. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Lno = 0
        'format GDPPC2000PPP/1000, USA, GDPPC, 2nd dim(if any) of forecast
        Do While Not EOF(iFileNo)
            Input(iFileNo, VGroupOn)
            Input(iFileNo, VHVar)
            Input(iFileNo, VRegion)
            Input(iFileNo, VFVar)
            Input(iFileNo, VFDim2)

            'mti Validation changes 19 August, 2002
            'UPGRADE_WARNING: Couldn't resolve default property of object Lno. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Lno = Lno + 1
            Found = "False"
            DimNum = 1
            'mti Validation changes 19 August, 2002

            Select Case UCase(VGroupOn)
                Case "REGION"
                    FlagGroup = False
                    'mti Validation changes 19 August, 2002
                    'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(DimNum). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    For EleLoop = 1 To MaxVal(DimNum)
                        If UCase(VRegion) = UCase(GRPS(DimNum, EleLoop)) Then
                            Found = "True"
                            Exit For
                        End If
                    Next EleLoop
                    If Found = "False" Then

                        LoadMessage("Region " & VRegion & " on line " & Trim(Str(Lno)), Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")

                    End If
                Case "GROUP"
                    FlagGroup = True
                    'mti Validation changes 19 August, 2002
                    'UPGRADE_WARNING: Couldn't resolve default property of object GCount. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    For EleLoop = 1 To GCount + 1
                        If UCase(VRegion) = UCase(GroupNames(EleLoop)) Then
                            Found = "True"
                            Exit For
                        End If
                    Next EleLoop
                    If Found = "False" Then

                        LoadMessage("Group " & VRegion & " on line " & Trim(Str(Lno)) & " is not a IFs Group.", Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")

                    End If
                    'mti Validation changes 19 August, 2002


                Case Else
                    LoadMessage("Group/Region Indicator Not Set for " & VHVar, Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")

            End Select

            'mti Validation changes 19 August, 2002
            If Found = "True" Then
                If Not InputHistoryVar(VHVar, VRegion) Then
                    LoadMessage("Failure Opening Validation File!", Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")

                End If
                If Not InputForeCastVar(VFVar, VRegion, VFDim2) Then

                    LoadMessage("Failure Opening Validation File!", Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")

                End If
            End If
            'mti Validation changes 19 August, 2002

        Loop

        FileClose(iFileNo)
        GoTo CancelError

FileReadError:
        FileClose(iFileNo)

CancelError:  ' User Pressed Cancel
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuOpenforValidation.Checked = False 
        bPickingValidation = False
        FlagGroup = sFlagGroup
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursor from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Cursors from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.Cursor.Current = 'JRSVBNET System.Windows.Forms.Cursors.Default

    End Sub


    Public Sub mnuScenarioOutFilesAttach_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        Dim nvcat As Object
        Dim AddName As String
        Dim ICount As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/24/01 DISPLAY
        'JRSVBNET CMDialog1.FileName = ""

        '   CancelError is True
        '   -------------------

        On Error GoTo CancelError

        'JRSVBNET CMDialog1.InitDir = PutFdrvRun2
        'JRSVBNET CMDialog1.DialogTitle = "Select for Display"
        'JRSVBNET CMDialog1.Filter = "Run Files (*.run)|*.run"
        'JRSVBNET CMDialog1.Action = 1
        Dim iOldFilesSelected As Short
        If False Then 'JRSVBNET CMDialog1.FileName <> "" Then
            iOldFilesSelected = Session("FilesSelected")
            'JRSVBNET SelectNewFile(('JRSVBNET CMDialog1.FileName))
            If iOldFilesSelected <> Session("FilesSelected") Then
                'JRS 1/6/02 DISPLAY
                'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'JRSVBNET lstFiles.Items.Add(New ListItem(Trim(Str(lstFiles.Items.Count))   'JRSVBNET & " - " & CMDialog1.FileName))
            End If
            '        msg$ = "Selection of " + 'JRSVBNET CMDialog1.Filename + " complete.  You may now "
            '        msg$ = msg$ + "choose variables from it for display."
            '        MsgBox msg$
        End If



        If Session("DoingHistory") Then
            'Reload Categories and Variables for Forecast

            Session("DoingHistory") = False
            'JRS 11/25/01 DISPLAY but_history.Caption = "Using &Forecast"
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuHisVarOptSelForHisVar.Checked = True 
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuHisVarOptSelEmpHisVar.Checked = False 
            '   Load Variable Name Combo Box
            '   ----------------------------

            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            lstVarName.Items.Clear()
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            txtVarSearch.Text = ""
            ICount = 0

            'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For i2 = 1 To NumbVars
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                AddName = V(i2)
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                AddName = AddName & " - " & VDef(i2)
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                    'VCat(i2) <> 9 Then
                    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

                    lstVarName.Items.Add(New ListItem(AddName))
                    ICount = ICount + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    'UPGRADE_WARNING: Couldn't resolve default property of object VTracker(ICount). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    VTracker(ICount) = i2
                End If
                'Strip out automobile variables except for GM
            Next i2
            'JRS 03/21/04
            Session("VTracker") = VTracker
            '   Load Variable Session("Category") Combo Box
            '   --------------------------------
            lbl_vcat.Text = "To Show a Subset of Names:"
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRSVBNET cmb_VCAT.Items(cmb_VCAT.SelectedIndex).Text = ""
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_VCAT.Items.Clear()
            'jrs 2005/04/29 cmb_VCAT.Items.Add(New ListItem(""))
            'jrs 2005/02/12
            'KIA 09/22/2009 COMMENT UNTILE FIND END
            'Dim fNum As Integer
            'fNum = FreeFile()
            'FileOpen(fNum, DataFileDir & VCATFile, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
            'Input(fNum, nvcat)
            ''UPGRADE_WARNING: Couldn't resolve default property of object nvcat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'For i2 = 1 To nvcat
            '    Input(fNum, AddName)
            '    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or AddName <> "Transportation" Then cmb_VCAT.Items.Add(New ListItem(AddName))
            'Next i2
            'FileClose(fNum)

            'KAI 09/22/2009 comment ends

            'KIA 09/22/2009 ADD THE FOLLOWING LINE
            LoadCategories()



            '
            '   Hide Dimension 1 Combo Box
            '   --------------------------
            cmb_DIM1.Visible = False
            lstDIM1.Visible = False
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'JRS 02/25/04
            If cmb_DIM1.SelectedIndex >= 0 Then
                cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = ""
            End If
            SaveDim1 = ""
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_DIM1.Items.Clear()
            lstDIM1.Items.Clear()

            '   Set up labels
            '   -------------
            lbl_Dim1.Visible = False
            Msg = "Touch arrow to activate selection box; type name or "
            Msg = Msg & "click on name; then touch Enter key"
            lbl_VarName.Text = Msg
            'mti Validation changes
            lbl_Dim1.ForeColor = System.Drawing.ColorTranslator.FromOle(QBColor(0))
            'mti Validation changes

            lbl_Dim1.Text = "Click arrow to activate dimension box; type of click name; touch enter"
            Msg = "Default File:"
            lbl_files.Text = Msg


            '   Initialize selection
            '   --------------------
            SubDimDone = "False"
            SEL1 = ""
            Session("SEL1") = SEL1

            '   Initialize calculations and sequences
            '   -------------------------------------
            CalcRunning = False
            'UPGRADE_WARNING: Couldn't resolve default property of object CalcSave. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            CalcSave = "None"
            'UPGRADE_WARNING: Couldn't resolve default property of object SeqOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SeqOn = "False"
            'UPGRADE_WARNING: Couldn't resolve default property of object SeqRepeat. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SeqRepeat = 0

            '   Establish List Box
            '    -----------------
            Msg = Trim(Str(0)) & " Working File, based on "
            Msg = Msg & Session("WorkingFile")
            ' JRS 11/24/01 DISPLAY Txt_files.Text = ""
            ' JRS 11/24/01 DISPLAY Txt_files.Text = msg$
            'UPGRADE_WARNING: Couldn't resolve default property of object DefaultFile. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DefaultFile = 0

            lbl_VarName.Visible = True
            lbl_files.Visible = True
            ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
            lstFiles.Visible = True
            cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
            ' JRS 11/24/01 DISPLAY but_File.Visible = True
            ' JRS 11/24/01 DISPLAY Txt_files.Enabled = False
            lbl_vcat.Visible = True



            'frm_VarSel.Show
            cmb_VCAT.Visible = True
            txtVarSearch.Visible = True
            lstVarName.Visible = True
            txtVarSearch.Enabled = True
            lstVarName.Enabled = True
            lstVarName.Visible = True
        End If



CancelError:  ' User Pressed Cancel
        Exit Sub

    End Sub


    Public Sub mnuScenarioOutFilesExpScen_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/24/01 DISPLAY
        Msg = "A scenario is a set of assumptions and a forecast based upon them.  Normal use of a forecasting model begins with exploration of the base case (when you first open IFs, the base case and the working file will be identical).  Then you might make a change in one or more parameters and rerun the model, thereby altering the working file (but not the base case).  At that point you would normally compare some variables in the working file with their original values in the base case.  You might save your new working file with a new name so that you can retrieve that scenario later."

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    '=============================VarSel.Frm==============================
    Public Sub LoadVarName(ByRef sList As String)
        Dim AddName As String
        Dim ICount As Short
        Dim Groups As Array
        Dim pos1 As Integer
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/25/01 DISPLAY
        'Variables is for type 1, 2
        'Parameters is for type 3
        'jrs 2005/02/24

        lstVarName.SelectedIndex = -1
        '   Load Variable Name Combo Box
        '   ----------------------------
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.

        lstVarName.Items.Clear()
        ICount = 0

        'TableName$ = valtbl.Rows(i).Item("Table")
        'GroupName$ = valtbl.Rows(i).Item("Group")
        'VarName$ = valtbl.Rows(i).Item("Variable")
        'Series$ = valtbl.Rows(i).Item("Series")
        'Cohort$ = valtbl.Rows(i).Item("Cohort")
        'Definition$ = valtbl.Rows(i).Item("Definition")
        'If GroupName$ = GroupSelect$ Or cmb_VCAT.ListIndex = 0 Then
        '    If Series$ = "Yes" Or Cohort$ = "Yes" Then
        '        If ModelLoad$ = "LoadCoVaTrA" Or GroupName$ <> "Automobile" Then
        '            AddName$ = VarName$ + " - " + Definition$
        '            cmb_VarName.AddItem AddName$
        '        End If
        '    End If
        'End If
        'valtbl.MoveNext
        'If valtbl.EOF = True Then Exit Do

        'JRS 03/21/04


        Dim rsL As DataTable
        Dim b As Boolean
        If sList <> "Lists" Then
            'UPGRADE_WARNING: Couldn't resolve default property of object NumbVars. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            For i2 = 1 To NumbVars
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                AddName = V(i2)
                'jrs 2005/02/24
                AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
                'UPGRADE_WARNING: Couldn't resolve default property of object i2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If Session("SensitivityFlag") <> "True" Or VType(i2) = 1 Then
                    'jrs 2020/07/02
                    'jrs 2016/01/11 this was originally commented out on 2012/02/27 in the load of the form
                    'but forgot to remove it from here.
                    'If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                    'jrs 2012/06/15
                    If Session("VarSelVCat") = "All" Then
                        Select Case sList
                            Case "Variables"
                                'jrs 2006/12/22 If VType(i2) = 1 Then
                                If VType(i2) <> 3 Then
                                    lstVarName.Items.Add(New ListItem(AddName))
                                    ICount = ICount + 1
                                    VTracker(ICount) = i2
                                End If
                            Case "Parameters"

                                lstVarName.Items.Add(New ListItem(AddName))
                                ICount = ICount + 1
                                VTracker(ICount) = i2
                                        'JRS 05/08/03 HISTORIC ANALOG
                            Case "HistoricAnalog"
                                If Not IsDBNull(VHistoricAnalog(i2)) Then

                                    lstVarName.Items.Add(New ListItem(AddName))
                                    ICount = ICount + 1
                                    VTracker(ICount) = i2
                                End If
                        End Select
                    Else
                        Dim vname As String = Session("VarSelVCat")
                        Groups = Split(VCatName(i2), ",")
                        pos1 = 0
                        While (pos1 < Groups.Length)
                            If Trim(Groups(pos1)) = vname Then
                                AddName = V(i2)
                                'JRS 10/13/03
                                AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
                                If Session("SensitivityFlag") <> "True" Or VType(i2) = 1 Then
                                    'jrs 2012/06/15
                                    If (sList = "Variables" And VType(i2) <> 3) Or sList = "Parameters" Or
                                        (sList = "HistoricAnalog" And Not IsDBNull(VHistoricAnalog(i2))) Then
                                        'jrs 2012/02/27 If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                                        dataString = dataString + AddName + "|"
                                        lstVarName.Items.Add(New ListItem(AddName))
                                        ICount = ICount + 1
                                        VTracker(ICount) = i2
                                        'jrs 2012/02/27 End If
                                    End If
                                End If
                            End If
                            pos1 += 1
                        End While
                    End If

                    'VCat(i2) <> 9 Then
                    'End If
                End If
                'Strip out automobile variables except for GM
            Next i2
        Else
            b = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
            'jrs 2005/08/18
            Dim bHF As Boolean
            bHF = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast")

            SetDataTablefromAccess(Session("PutFdrvRun") & "IFSVAR.mdb", "SELECT DISTINCT ListName, Type FROM VarLists WHERE History = " & Str(b) & " AND HistoryplusForecast = " & Str(bHF), "", rsL)
            Dim i As Integer
            i = 0
            Do While (Not i = rsL.Rows.Count)

                lstVarName.Items.Add(New ListItem(rsL.Rows(i).Item("ListName") + " - " + rsL.Rows(i).Item("Type")))
                ICount = ICount + 1
                i = i + 1
            Loop
        End If
        'JRS 03/21/04
        Session("VTracker") = VTracker
        'jrs 2012/06/15
        Session("LoadVarNameType") = sList
    End Sub

    'jrs 2005/08/18
    Public Sub mnuSearch()
        If Session("VarSelCheck") = "mnuVarSelOptShowVar" Then
            Session("SearchVarList") = "Variables"
        ElseIf Session("VarSelCheck") = "mnuVarSelOptShowVarPar" Then
            Session("SearchVarList") = "Parameters"
        ElseIf Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("SearchVarList") = "HistoricAnalog"
        End If
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SearchVar.aspx")
    End Sub

    Public Sub mnuSelforValidation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes - take whole procedure
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
        If False Then 'JRSVBNET mnuSelforValidation.Checked = False Then
            mnuHisVarOptSelEmpHisVar_Click(Me, New System.EventArgs)
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuSelforValidation.Checked = True 

            bPickingValidation = True
            sPickHRegion = ""
            iPickRegionCounter = 0

            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuHisVarOptSelForHisVar.Checked = False 
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuHisVarOptSelEmpHisVar.Checked = False 

        Else
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            If True Then 'JRSVBNET WinFormsToWeb_MessageBox.Show("Cancel picking variables for validation?", MsgBoxStyle.YesNo, "IFS - Validation") = MsgBoxResult.Yes Then
                mnuHisVarOptSelForHisVar_Click(Me, New System.EventArgs)
                'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuSelforValidation.Checked = False 
                bPickingValidation = False
            End If

        End If

    End Sub

    Public Sub mnuVarSelOptCreaDispList_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 12/01/01 DISPLAY
        Dim i As Short
        Dim sNameList As String
        If Session("COUNTE") > 0 Then
            'Check for same dimensions in all variables
            sNameList = InputBox("Ener List Name", "Create Display List")
            If sNameList = "" Then
                LoadMessage("You have to specify a Name", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.Hide().html'.
            'frm_CreaDispList.DefInstance.Hide() 
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET *** BEGIN ***
            'For i = 0 To frm_CreaDispList.DefInstance.lstListNames.Items.Count - 1
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    If VB6.GetItemString(frm_CreaDispList.DefInstance.lstListNames, i) = sNameList Then
            '        If Not CheckHistory(sNameList) Then
            '            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '            WinFormsToWeb_MessageBox.Show("The Type (Historic or Not) of Variables doesn't match, try other name for the list")
            '            Exit Sub
            '        End If
            '        'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '        frm_CreaDispList.DefInstance.lstListNames.SelectedIndex = i
            '        Exit For
            '    End If
            'Next i
            ''CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'If i = frm_CreaDispList.DefInstance.lstListNames.Items.Count Then
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    frm_CreaDispList.DefInstance.txtListName.Text = sNameList
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Keys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '    frm_CreaDispList.DefInstance.txtListName_KeyPress(frm_CreaDispList.DefInstance.txtListName, New 'JRSVBNET System.Windows.Forms.KeyPressEventArgs(Chr(('JRSVBNET System.Windows.Forms.Keys.Return))))
            'End If
            'For i = 1 To Session("COUNTE")
            '    'JRS 2/12/02 LABELS
            '    If InStr(Session("VARN")(1, i), "[") = 0 Then
            '        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        WinFormsToWeb_MessageBox.Show("There is a modified variable name, cann't save this list")
            '        Exit Sub
            '    Else
            '        'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaDispList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '        frm_CreaDispList.DefInstance.SelectVar(Trim(VB.Left(Session("VARN")(1, i), InStr(Session("VARN")(1, i), "[") - 1)), Session("GR1N")(i), Session("GR2N")(i))
            '    End If
            'Next i
            'JRSVBNET *** END ***
        End If
        'JRS 04/25/02
        NeedFileWrite = True
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        'Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_CreaDispList.aspx")
        'frm_CreaDispList.DefInstance.Show() 
    End Sub

    Public Sub mnuVarSelOptCreaVarList_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'cdv 2022/05/19 ****START****
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/28/01 DISPLAY
        Dim i As Short
        Dim sNameList As String
        Dim sDim1 As Object
        Dim sDim2 As String
            Dim sDim3 As String
            'jrs 2006/01/15
            Dim bDim2All As Boolean, bDim3All As Boolean
            bDim2All = True
            bDim3All = True

        If Session("COUNTE") > 0 Then
            'Check for same dimensions in all variables
            'UPGRADE_WARNING: Couldn't resolve default property of object sDim1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            sDim1 = Session("GR1N")(1)
            sDim2 = Session("GR2N")(1)
            sDim3 = Session("GR3N")(1)

            For i = 2 To Session("COUNTE")
                'UPGRADE_WARNING: Couldn't resolve default property of object sDim1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If sDim1 <> Session("GR1N")(i) And Session("GR1N")(i) > "" Then 'jrs 2007/11/07 added And GR1N$(i) > ""  'jrs 2006/01/22 added ucase 'jrs 2006/01/13  Or sDim2 <> Session("GR2N")(i) Then
                    LoadMessage("All Variables in Status Box must have the same dimension 1", Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")
                End If

                'jrs 2006/01/15
                If sDim2 <> Session("GR2N")(i) Then
                        'jrs 2010/09/24
                        If sDim2 = "" Then
                            sDim2 = Session("GR2N")(i)
                        ElseIf Session("GR2N")(i) > "" Then 'jrs 2013/11/11 added GR2N > ""
                            bDim2All = False
                        End If
                    End If
                    'jrs 2006/05/23
                    If sDim3 <> Session("GR3N")(i) Then
                        'jrs 2010/09/24
                        If sDim3 = "" Then
                            sDim3 = Session("GR3N")(i)
                            If bDim2All = False Then
                                bDim3All = False
                            End If
                        ElseIf Session("GR3N")(i) > "" Then 'jrs 2013/11/11 added GR3N > ""
                            bDim3All = False
                        End If
                    End If

            Next i


            Session("sNameList") = ""
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_CreateVarList.aspx")

            'cdv 2022/05/19 ****END****

            'CONVERSION_NOTE: Method ''JRSVBNET System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Control.Hide().html'.
            'frm_CreaVarList.DefInstance.Hide() 
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'JRSVBNET *** BEGIN ***
            'For i = 0 To frm_CreaVarList.DefInstance.lstListNames.Items.Count - 1
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    If VB6.GetItemString(frm_CreaVarList.DefInstance.lstListNames, i) = sNameList Then
            '        'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '        frm_CreaVarList.DefInstance.lstListNames.SelectedIndex = i
            '        Exit For
            '    End If
            'Next i
            ''CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            ''CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            'If i = frm_CreaVarList.DefInstance.lstListNames.Items.Count Then
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    frm_CreaVarList.DefInstance.txtListName.Text = sNameList
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Keys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '    frm_CreaVarList.DefInstance.txtListName_KeyPress(frm_CreaVarList.DefInstance.txtListName, New 'JRSVBNET System.Windows.Forms.KeyPressEventArgs(Chr(('JRSVBNET System.Windows.Forms.Keys.Return))))
            'End If
            'For i = 1 To Session("COUNTE")
            '    'JRS 2/12/2 LABELS
            '    If InStr(Session("VARN")(1, i), "[") = 0 Then
            '        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        WinFormsToWeb_MessageBox.Show("There is a modified variable name, cann't save this list")
            '        Exit Sub
            '    Else
            '        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '        frm_CreaVarList.DefInstance.cboVarName.Items(frm_CreaVarList.DefInstance.cboVarName.SelectedIndex).Text = Trim(VB.Left(Session("VARN")(1, i), InStr(Session("VARN")(1, i), "[") - 1))
            '    End If
            '    'CONVERSION_ISSUE: Property 'IFsGUI.frm_CreaVarList.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.KeyPressEventArgs from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '    'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.Keys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
            '    frm_CreaVarList.DefInstance.cboVarName_KeyPress(frm_CreaVarList.DefInstance.cboVarName, New 'JRSVBNET System.Windows.Forms.KeyPressEventArgs(Chr(('JRSVBNET System.Windows.Forms.Keys.Return))))
            'Next i
            'JRSVBNET *** END ***

        End If
        'JRS 04/25/02
        NeedFileWrite = True
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        'Support.FormManager.StateStore(Me)
        'sSupport.FormManager.Show(Me, "frm_CreateVarList.aspx")
        'frm_CreaVarList.DefInstance.Show() 
    End Sub

    Public Sub mnuVarSelOptExpName_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim Msg As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 11/25/01 DISPLAY
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Carret = "<br>" & Chr(10)
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        carret2 = Carret + Carret
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = "There are two categories of names for display:" + Carret
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg & "   1. Computed variables, in capitals (e.g., AGDEM)" + Carret
        'UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Msg = Msg & "   2. Parameters, shown in lower case (e.g., agdemm)" + Carret

        LoadMessage(Msg, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")


        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lstVarName.Enabled = True
        lstVarName.Visible = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
        '' JRS 11/24/01 DISPLAY but_File.Visible = True

        lbl_files.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
        lstFiles.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Enabled = False
        'jrs 2012/06/18
        ''jrs 2006/09/20
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    cmb_VCAT.Visible = False
        '    lbl_vcat.Visible = False
        'Else
        '    cmb_VCAT.Visible = True
        '    lbl_vcat.Visible = True
        'End If


    End Sub


    Public Sub mnuVarSelOptHistoricPlusForecast_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        'jrs 2006/09/20
        lbl_files.Visible = True
        lstFiles.Visible = True
        'jrs 2012/06/18 lbl_vcat.Visible = False
        'jrs 2012/06/18 cmb_VCAT.Visible = False

        Session("lstVarNameSelectedIndex") = Nothing

        'jrs 2005/08/23
        ''jrs 2005/08/18
        'Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast"
        'cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "checkbox.gif"
        If CBool(Session("ShowVarDispList")) Then
            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                Session("VarSelCheck") = ""
                cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "blank.gif"
                'jrs 2006/09/20
                lbl_vcat.Visible = True
                cmb_VCAT.Visible = True
            Else
                Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast"
                cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "checkbox.gif"
            End If
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            GridRefresh()
            'jrs 2005/08/23
            'JAM 2014-05-18
            ''LoadVarName(("Lists"))
            'JAM 2014-05-17
            FillSearchArray(("Lists"))

            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarPar").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVar").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarDispList").IconImage = "checkbox.gif"
            cmb_VCAT.Enabled = False
            'jrs 2008/09/04
            'but_GroupRegToggle.Enabled = False
            'but_GroupRegToggle.Text = "Using Countries/Regions"
            Ag_On_Group = False
            FlagGList = False
            FlagGroup = False
            Session("Ag_On_Group") = Ag_On_Group
            Session("FlagGList") = FlagGList
            Session("FlagGroup") = FlagGroup
            Session("ShowVarDispList") = True
            cswmWebMenu.Groups("mnuVarSearch").Items("mnuSearch").Enabled = False
            Exit Sub
        End If

        'JRS 05/08/03 HISTORIC ANALOG
        'JAM 2014-05-18
        ''LoadVarName(("HistoricAnalog"))
        'JAM 2014-05-17
        FillSearchArray(("HistoricAnalog"))
        'mnuVarSelOptShowVarDispList.Checked = False 
        'mnuVarSelOptShowVarPar.Checked = False 
        'mnuVarSelOptShowVar.Checked = False 
        'mnuVarSelOptHistoricPlusForecast.Checked = True 

        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarPar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarDispList").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "checkbox.gif"
        Session("ShowVarDispList") = False

        If Session("VarSelCheck") <> "mnuVarSelOptHistoricPlusForecast" Then
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            GridRefresh()
        End If
        'jrs 2006/04/18
        Session("FirstYear") = FirstYearHistory

        Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast"
        'jrs 2005/08/18
        cswmWebMenu.Groups("mnuVarSearch").Items("mnuSearch").Enabled = True
        'jrs 2006/06/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = True
        'jrs 2012/06/15 
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = True
        'jrs 2006/09/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = True
        'jrs 2006/09/20
        If Not Session("lstVarNameSelectedIndex") Is Nothing Then
            Session("VarClick") = True
        Else
            Session("VarClick") = False
        End If


        'JAM 2014-04-31 Keeping forecasted or empirical variable set =>0:Forecasted 1:Empirical
        Session("VEmpOrForeMode") = 0
        fEmpOrForeMode.Value = Session("VEmpOrForeMode")
        Session("VHistoricAnalogMode") = "0"
        fHistoricAnalogMode.Value = Session("VHistoricAnalogMode")


    End Sub

    Public Sub mnuVarSelOptShowVar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 12/04/01 DISPLAY
        'JAM 2014-05-18
        ''LoadVarName(("Variables"))

        'JAM 2014-05-17
        FillSearchArray(("Variables"))
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuVarSelOptShowVarDispList.Checked = False 
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuVarSelOptShowVarPar.Checked = False 
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuVarSelOptShowVar.Checked = True 

        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarDispList").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarPar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVar").IconImage = "checkbox.gif"
        Session("ShowVarDispList") = False
        'jrs 2005/08/18
        cmb_VCAT.Enabled = True
        'jrs 2008/09/04 but_GroupRegToggle.Enabled = True 'jrs 2005/08/23
        '
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuVarSelOptHistoricPlusForecast.Checked = False 
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "blank.gif"
            Session("FirstYear") = FirstYearForecast
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            GridRefresh()
        End If

        Session("VarSelCheck") = "mnuVarSelOptShowVar"
        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        'jrs 2012/06/15
        lbl_vcat.Visible = True
        cmb_VCAT.Visible = True

        Session("lstVarNameSelectedIndex") = Nothing
        'jrs 2005/08/18
        cswmWebMenu.Groups("mnuVarSearch").Items("mnuSearch").Enabled = True
        'jrs 2006/06/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
        'jrs 2012/06/15 
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")

        'jrs 2006/09/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
    End Sub

    Public Sub mnuVarSelOptShowVarDispList_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        Session("lstVarNameSelectedIndex") = Nothing
        'jrs 2005/08/23
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" And CBool(Session("ShowVarDispList")) Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarDispList").IconImage = "blank.gif"
            Session("ShowVarDispList") = False
            cmb_VCAT.Enabled = True
            'jrs 2008/09/04 but_GroupRegToggle.Enabled = True
            'JAM 2014-05-18
            ''LoadVarName("HistoricAnalog")

            'JAM 2014-05-17
            FillSearchArray("HistoricAnalog")

            cswmWebMenu.Groups("mnuVarSearch").Items("mnuSearch").Enabled = True
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = True
            'jrs 2012/06/15 
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = True
            'jrs 2006/09/26
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = True
            Exit Sub
        End If
        'JRS 12/04/01 DISPLAY
        'JAM 2014-05-18
        ''LoadVarName(("Lists"))

        'JAM 2014-05-17
        FillSearchArray(("Lists"))


        'mnuVarSelOptShowVarPar.Checked = False 
        'mnuVarSelOptShowVar.Checked = False 
        'mnuVarSelOptShowVarDispList.Checked = True 

        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarPar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarDispList").IconImage = "checkbox.gif"
        'jrs 2005/08/18
        cmb_VCAT.Enabled = False
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    'mnuVarSelOptHistoricPlusForecast.Checked = False 
        '    cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "blank.gif"
        '    Session("FirstYear") = FirstYearForecast
        '    ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("FirstYear"), Session("LastYear"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FinYear"))
        '    GridRefresh()
        'End If
        'jrs 2005/08/02
        'jrs 2008/09/04 but_GroupRegToggle.Enabled = False
        'jrs 2008/09/04 but_GroupRegToggle.Text = "Using Countries/Regions"
        Ag_On_Group = False
        FlagGList = False
        FlagGroup = False
        Session("Ag_On_Group") = Ag_On_Group
        Session("FlagGList") = FlagGList
        Session("FlagGroup") = FlagGroup

        'jrs 2005/08/23 Session("VarSelCheck") = "mnuVarSelOptShowVarDispList"
        Session("ShowVarDispList") = True


        'jrs 2005/08/18
        cswmWebMenu.Groups("mnuVarSearch").Items("mnuSearch").Enabled = False
        'jrs 2006/06/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Or Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
        'jrs 2012/06/15 
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Or Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
        'jrs 2006/09/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Or Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")

        'jrs 2023/05/18
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            Session("VEmpOrForeMode") = 5
        Else
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("VEmpOrForeMode") = 3
            Else
                Session("VEmpOrForeMode") = 4
            End If
        End If
        fEmpOrForeMode.Value = Session("VEmpOrForeMode")
        Session("VarSelCheck") = "mnuVarSelOptShowVarDispList"

    End Sub

    Public Sub mnuVarSelOptShowVarPar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 12/04/01 DISPLAY
        'JAM 2014-05-18
        ''LoadVarName(("Parameters"))

        'JAM 2014-05-17
        FillSearchArray(("Parameters"))

        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuVarSelOptShowVar.Checked = False 
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuVarSelOptShowVarDispList.Checked = False 
        'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
        'mnuVarSelOptShowVarPar.Checked = True 

        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVar").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarDispList").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptShowVarPar").IconImage = "checkbox.gif"
        Session("ShowVarDispList") = False

        cmb_VCAT.Enabled = True 'jrs 2005/07/21
        'jrs 2008/09/04 but_GroupRegToggle.Enabled = True 'jrs 2005/08/23

        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
            'mnuVarSelOptHistoricPlusForecast.Checked = False 
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuVarSelOptHistoricPlusForecast").IconImage = "blank.gif"
            'JRS 2005/02/21
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                Session("FirstYear") = FirstYearForecast
            Else
                Session("LastYear") = LastYearHistory
            End If
            ClearPlot(Session("COUNTE"), Session("PLOT"), Session("VARN"), Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"), Session("FinYear"),
            Session("FinYearHis"), Session("buseAllHis"), Session("YAxisLabel"), Session("XAxisLabel"), Session("StaYearHis"), Session("sUsrDefCats"))
            GridRefresh()
        End If

        Session("VarSelCheck") = "mnuVarSelOptShowVarPar"
        'jrs 2005/02/24
        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        lstVarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        'jrs 2012/06/15
        lbl_vcat.Visible = True
        cmb_VCAT.Visible = True

        Session("lstVarNameSelectedIndex") = Nothing
        'jrs 2005/08/18
        cswmWebMenu.Groups("mnuVarSearch").Items("mnuSearch").Enabled = True
        'jrs 2006/06/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").Enabled = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
        'jrs 2012/06/15 
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").Enabled = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")
        'jrs 2006/09/26
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").Enabled = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")


        'JAM 2014-04-31 Keeping forecasted or empirical variable set =>0:Forecasted 1:Empirical
        Session("VEmpOrForeMode") = 0
        fEmpOrForeMode.Value = Session("VEmpOrForeMode")
        Session("VHistoricAnalogMode") = "1"
        fHistoricAnalogMode.Value = Session("VHistoricAnalogMode")


    End Sub

    '=============================VarSel.Frm==============================
    Public Function CheckHistory(ByRef sListName As String) As Boolean
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 1/19/02 DISPLAY
        Dim rsH As DataTable
        Dim b As Boolean
        'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
        'JRSVBNET b = mnuHisVarOptSelEmpHisVar.Checked
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFSVAR.mdb", "SELECT * FROM VarLists WHERE ListName ='" & sListName & "' AND History =" & Str(b), "", rsH)
        CheckHistory = (rsH.Rows.Count > 0)

    End Function

    '=============================VarSel.Frm==============================
    Public Sub GridRefresh()

        'jrs 2024/11/25 Dim iloop As Short
        Dim iloop As Integer
        grd_VariableDisplay.Columns.Clear()
        grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

        If (Session("COUNTE") = 0) Then
            grd_VariableDisplay(0, 0) = ""
        End If

        'JRS 02/25/04
        For iloop = 1 To Session("COUNTE")
            Dim myCol As New TMSWebControls.Column
            'myCol.Type = TMSWebControls.Column.ColumnType.Button
            myCol.WidthType = TMSWebControls.WType.Absolute
            'myCol.ButtonWidth = 100
            myCol.Width = 100
            myCol.Color = System.Drawing.Color.White
            grd_VariableDisplay.Columns.Add(myCol)

            grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
            grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)
            grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
            grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
            grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop)

        Next iloop


    End Sub

    '=============================VarSel.Frm==============================
    Public Sub ChkEndPickingHistory()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes -whole proc
        Dim bEndPickingHistory As Boolean
        bEndPickingHistory = False

        If bPickingValidation Then
            'UPGRADE_WARNING: Couldn't resolve default property of object SubOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object AddOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MulOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object DivOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If DivOnPrev = False And MulOnPrev = False And AddOnPrev = False And SubOnPrev = False Then

                Session("VARN")(4, Session("COUNTE")) = "VH" & Trim(Str(Session("COUNTE")))
                System.Diagnostics.Debug.WriteLine("VH" & Trim(Str(Session("COUNTE"))))

                If sPickHRegion = "ALL" Then
                    'TVal% = MaxVal(1) - 1  'Region (i.e., dim 1) max - 1 (for "ALL")
                    iPickRegionCounter = iPickRegionCounter + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If iPickRegionCounter = MaxVal(1) - 1 Then 'All Regions Done
                        bEndPickingHistory = True
                    End If
                Else 'Single Region
                    bEndPickingHistory = True
                End If

            End If
        End If

        If bEndPickingHistory Then
            'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
            If False Then 'JRSVBNET Not mnuOpenforValidation.Checked Then
                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                'jrs 2005/11/03 WinFormsToWeb_MessageBox.Show("Please choose the forecast variable to check against.", MsgBoxStyle.OKOnly, "IFS - Validation")
                iPickRegionCounter = 0
                mnuHisVarOptSelForHisVar_Click(Me, New System.EventArgs)
                bPickingValidation = True
                'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                'mnuHisVarOptSelForHisVar.Checked = False 
            End If
        End If

    End Sub

    '=============================VarSel.Frm==============================
    Public Sub ChkPickingForecast()
        Dim Msg As String
        Dim TVal As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes -whole proc
        Dim bEndedSelforValidation As Boolean
        bEndedSelforValidation = False
        If bPickingValidation Then
            'MsgBox "Please choose the forecast variable to check against.", vbOKOnly, "IFS - Validation"
            'UPGRADE_WARNING: Couldn't resolve default property of object SubOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object AddOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object MulOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object DivOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If DivOnPrev = False And MulOnPrev = False And AddOnPrev = False And SubOnPrev = False Then

                If sPickHRegion = "ALL" Then
                    'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    TVal = MaxVal(1) - 1 'Region (i.e., dim 1) max - 1 (for "ALL")
                    iPickRegionCounter = iPickRegionCounter + 1
                    'UPGRADE_WARNING: Couldn't resolve default property of object MaxVal(1). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                    If iPickRegionCounter = MaxVal(1) - 1 Then
                        bEndedSelforValidation = True
                    End If
                Else
                    TVal = 1
                    bEndedSelforValidation = True
                End If

                Session("VARN")(4, Session("COUNTE")) = "VF" & Trim(Str(Session("COUNTE") - TVal))
                System.Diagnostics.Debug.WriteLine("VF" & Trim(Str(Session("COUNTE") - TVal)))
            End If
            If bEndedSelforValidation Then

                'CONVERSION_ISSUE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
                If False Then 'JRSVBNET Not mnuOpenforValidation.Checked Then
                    'UPGRADE_ISSUE: ComboBox property cmb_DIM1.Locked was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
                    'JRSVBNET cmb_DIM1.Locked = False

                    bPickingValidation = False
                    'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuSelforValidation.Checked = False 
                    'CONVERSION_NOTE: Property ''JRSVBNET System.Windows.Forms.MenuItem.Checked' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
                    'mnuHisVarOptSelForHisVar.Checked = True 'Session("GR2N")$(VF(c1% / 2))
                    Msg = "You have successfully selected empirical variable " & "<br>" & Chr(10) & RTrim(Session("VARN")(1, Session("COUNTE") - TVal)) & " for the region " & sPickHRegion & " for validation with" & "<br>" & Chr(10) & RTrim(Session("VARN")(1, Session("COUNTE"))) & "." & Session("GR2N")(Session("COUNTE"))

                    LoadMessage(Msg, Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")

                    'JRS 4/06/03
                    'CONVERSION_ISSUE: Property 'IFsGUI.'JRSVBNET frm_DisplayMenu' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
                    'JRSVBNET frm_DisplayMenu.mnuValidation.Visible = True
                End If
            End If
        End If

    End Sub

    '=============================VarSel.Frm==============================
    Public Function InputHistoryVar(ByRef SE As String, ByRef Sel2 As String) As Boolean
        Dim Msg As String
        Dim ValSelection As String
        Dim PosMul As Object
        Dim SELNLen As Object
        Dim NVal As Object
        Dim Cohort As String
        Dim Series As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes
        Dim bScaleInvalid As Boolean

        Dim sAg_On_Group As Boolean
        Dim sAg_On_Region As Boolean

        sAg_On_Group = Ag_On_Group
        'UPGRADE_WARNING: Couldn't resolve default property of object Ag_On_Region. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        sAg_On_Region = Ag_On_Region

        'On Error GoTo FileProblem:
        InputHistoryVar = False

        Ag_On_Group = False
        'UPGRADE_WARNING: Couldn't resolve default property of object Ag_On_Region. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Ag_On_Region = False
        Session("DoingHistory") = True

        sPickHRegion = Sel2
        iPickRegionCounter = 0

        'UPGRADE_WARNING: Couldn't resolve default property of object DivOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        DivOn = False
        'UPGRADE_WARNING: Couldn't resolve default property of object MulOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        MulOn = False
        'UPGRADE_WARNING: Couldn't resolve default property of object AddOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        AddOn = False
        'UPGRADE_WARNING: Couldn't resolve default property of object SubOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        SubOn = False
        'UPGRADE_WARNING: Couldn't resolve default property of object NumVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NumVal = False
        Series = "No"
        Cohort = "No"
        OperStr = ""

        '   Initialize Dimensions, Reset Variable Pointer JRS 3/17/02
        '   ---------------------------------------------
        'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label1 = ""
        'JRS 03/04/09
        Session("Label1") = Label1
        'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label2 = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label3 = ""

        '    If cmb_VarName.ListIndex = -1 Then
        '        ValSelection$ = False
        '        If IsNumeric(cmb_VarName.Text) Then  'Numeric scaling value
        '            NumVal = True
        '            NVal = Val(cmb_VarName.Text)
        '        End If
        '        SE$ = UCase$(cmb_VarName.Text) 'SE$ picks up the Variable name typed
        'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NVal = 0
        bScaleInvalid = False
        'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        SELNLen = Len(SE)
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "/")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'UPGRADE_WARNING: Couldn't resolve default property of object DivOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DivOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1)
                OperStr = "/"
            Else
                bScaleInvalid = True
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "*")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'UPGRADE_WARNING: Couldn't resolve default property of object MulOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MulOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1)
                OperStr = "*"
            Else
                bScaleInvalid = True
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "+")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'UPGRADE_WARNING: Couldn't resolve default property of object AddOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            AddOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1)
                OperStr = "+"
            Else
                bScaleInvalid = True
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "-")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'UPGRADE_WARNING: Couldn't resolve default property of object SubOn. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SubOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1)
                OperStr = "-"
            Else
                bScaleInvalid = True
            End If
        End If

        If bScaleInvalid = True Then

            LoadMessage("Scaling parameter not valid.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

            GoTo ResetFlags
        End If

        ValSelection = CStr(False)

        SetDataTablefromAccess(Session("PutFdrvRun") & "datadict.MDB", "SELECT * FROM datadict", "Variable", valtbl)

        Dim i As Integer
        For i = 0 To valtbl.Rows.Count - 1
            TableName = valtbl.Rows(i).Item("Table")
            VarName = valtbl.Rows(i).Item("Variable")
            Definition = valtbl.Rows(i).Item("Definition")
            'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label3 = valtbl.Rows(i).Item("Units")
            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Label3. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If IsDBNull(Label3) Then Label3 = "Unit"
            'Label3 = "Unit"
            Ag_rule = valtbl.Rows(i).Item("Aggregation")
            If (SE = UCase(VarName & " - " & Definition)) Or (UCase(SE) = UCase(VarName)) Then
                ValSelection = CStr(True)
                Exit For
            End If

        Next i

        'UPGRADE_WARNING: Couldn't resolve default property of object NumVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If CBool(ValSelection) = False And NumVal = False Then 'If VarName$ not found
            Msg = SE & " is not a valid selection."
            Msg = Msg & "  Please try again."

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

            GoTo ResetFlags 'get out / mti variable not found in database
            'revert picking validation to false here?
        End If

        'cmb_DIM1.Visible = False
        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = Sel2

        'cmb_DIM1.ListIndex = -1
        cmb_DIM1_SelectedIndexChanged(cmb_DIM1, New System.EventArgs)

        If DivOnPrev Or MulOnPrev Or AddOnPrev Or SubOnPrev Then
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object Session("VarVal"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("VarVal") = NVal
            Call CalcNumber()
            'SeqOn = False
            'CalcSave = "None"
            'CalcType = "None"
            'UPGRADE_WARNING: Couldn't resolve default property of object DivOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            DivOnPrev = False
            'UPGRADE_WARNING: Couldn't resolve default property of object MulOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            MulOnPrev = False
            'UPGRADE_WARNING: Couldn't resolve default property of object AddOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            AddOnPrev = False
            'UPGRADE_WARNING: Couldn't resolve default property of object SubOnPrev. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SubOnPrev = False

            'mti Validation changes
            ChkEndPickingHistory()
            'mti Validation changes

        End If

        InputHistoryVar = True

        '    Else 'cmb_VarName.ListIndex <> -1

ResetFlags:
        sPickHRegion = ""
        iPickRegionCounter = 0


        Ag_On_Group = sAg_On_Group
        'UPGRADE_WARNING: Couldn't resolve default property of object Ag_On_Region. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Ag_On_Region = sAg_On_Region
        Session("DoingHistory") = False

    End Function
    '=============================VarSel.Frm==============================
    Public Function InputForeCastVar(ByRef SE As String, ByRef Sel2 As String, ByRef VFDim2 As String) As Boolean
        Dim Msg As String
        Dim PosMul As Object
        Dim SELNLen As Object
        Dim NVal As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'mti Validation changes
        Dim bScaleInvalid As Boolean

        InputForeCastVar = False
        Session("DoingHistory") = False

        sPickHRegion = Sel2
        iPickRegionCounter = 0

        CalcType = "None"
        OperStr = ""
        'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        NVal = 0
        bScaleInvalid = False
        'UPGRADE_WARNING: Couldn't resolve default property of object SELNLen. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        SELNLen = Len(SE)
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "/")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'DivOn = true
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Mid(SE, PosMul + 1) 'Could be number or another var
            'If NVal > 0 Then 'NumVal OK
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            SE = VB.Left(SE, PosMul - 1) 'exclude Operator
            CalcType = "DivOn"
            OperStr = "/"
            'Else
            'bScaleInvalid = True
            'End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "%")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'MulOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1) 'include Operator
                CalcType = "PerOn"
                OperStr = "%"
            Else
                bScaleInvalid = True
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "*")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'MulOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1) 'include Operator
                CalcType = "MulOn"
                OperStr = "*"
            Else
                bScaleInvalid = True
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "+")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'AddOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1) 'include Operator
                CalcType = "AddOn"
                OperStr = "+"
            Else
                bScaleInvalid = True
            End If
        End If

        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        PosMul = InStr(1, SE, "-")
        'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If PosMul > 0 Then 'Check for Division
            'SubOn = True
            'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            NVal = Val(Mid(SE, PosMul + 1))
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If NVal > 0 Then 'NumVal OK
                'UPGRADE_WARNING: Couldn't resolve default property of object PosMul. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                SE = VB.Left(SE, PosMul - 1) 'include Operator
                CalcType = "SubOn"
                OperStr = "-"
            Else
                bScaleInvalid = True
            End If
        End If
        'JRS 03/14/04
        Session("CalcType") = CalcType

        If bScaleInvalid = True Then

            LoadMessage("Scaling parameter not valid.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

            GoTo ResetFlags
        End If


        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Session("SEL") = 0
        Session("SEL") = IfsVars.Item(SE).Index
        'Session("SEL") = chk

        'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If Session("SEL") <= 0 Then 'If VarName$ not found
            Msg = SE & " is not a valid selection."
            Msg = Msg & "  Please try again."
            'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")


            GoTo ResetFlags 'get out / mti variable not found in database
            'revert picking validation to false here?
        End If

        cmb_DIM1.Visible = False
        lstDIM1.Visible = False

        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        txtVarSearch.Text = SE & OperStr

        cmb_VarName_SelectedIndexChanged() 'need this to load all necessary info

        'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = Sel2

        cmb_DIM1_SelectedIndexChanged(cmb_DIM1, New System.EventArgs)

        If Len(VFDim2) > 0 Then
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = VFDim2
            cmb_DIM1_SelectedIndexChanged(cmb_DIM1, New System.EventArgs)
        End If

        If Len(OperStr) > 0 Then
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            txtVarSearch.Text = NVal
            cmb_VarName_SelectedIndexChanged() 'need this to load all necessary info
            'UPGRADE_WARNING: Couldn't resolve default property of object NVal. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            If Val(Str(NVal)) = 0 Then 'String : Var Name
                '            Session("SEL") = 0
                '            FindVarNumb Session("SEL"), SE$
                '            'Session("SEL") = chk
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If VDim1(Session("SEL")) > 0 Then
                    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = Sel2 'sel2$ holds region
                    cmb_DIM1_SelectedIndexChanged(cmb_DIM1, New System.EventArgs)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object Session("SEL"). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If VDim2(Session("SEL")) > 0 Then
                    'CONVERSION_TODO: The equivalent of Property ''JRSVBNET System.Windows.Forms.ComboBox.Text' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                    cmb_DIM1.Items(cmb_DIM1.SelectedIndex).Text = VFDim2 'sel2$ holds region
                    cmb_DIM1_SelectedIndexChanged(cmb_DIM1, New System.EventArgs)
                End If
            End If
        End If

        InputForeCastVar = True

        '    Else 'cmb_VarName.ListIndex <> -1

ResetFlags:
        sPickHRegion = ""
        iPickRegionCounter = 0

    End Function

    'jrs 2005/12/20 RAM for Formulas
    ''JRS 04/14/03
    'Public Function UpdateFormulaIdx(ByRef idx As Short) As Object
    '    Dim sUpdate As Object
    '    Dim sDelete As String
    '    'sDelete = "DELETE FROM Formulas WHERE Code=" + Str(idx)
    '    sDelete = "DELETE FROM Formulas WHERE Code>=" & Str(Session("COUNTE") + 1) & " AND Code < 1000"
    '    updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", sDelete)
    '    'UPGRADE_WARNING: Couldn't resolve default property of object sUpdate. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    sUpdate = "UPDATE Formulas SET Code = " & Str(Session("COUNTE") + 1) & " WHERE Code=" & Str(idx) & " AND Code < 1000"
    '    updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", sUpdate)
    '    'UPGRADE_WARNING: Couldn't resolve default property of object sUpdate. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    sUpdate = "UPDATE Formulas SET Code = Code-1 WHERE Code>" & Str(idx) & " AND Code < 1000"
    '    updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", sUpdate)
    '    'sDelete = "DELETE FROM Formulas WHERE Code>" + Str(Session("COUNTE"))
    '    'dbFor.Execute (sDelete)

    '    'jrs 2005/10/10 for code >= 1000 and < 2000
    '    sDelete = "DELETE FROM Formulas WHERE Code>=" + CStr(Session("COUNTE") + 1001) + " AND Code < 2000"
    '    updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", sDelete)
    '    sUpdate = "UPDATE Formulas SET Code = " + CStr(Session("COUNTE") + 1001) + " WHERE Code=" + CStr(idx + 1000) + " AND Code < 2000"
    '    updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", sUpdate)
    '    sUpdate = "UPDATE Formulas SET Code = Code-1 WHERE Code>" + CStr(idx + 1000) + " AND Code < 2000"
    '    updateAccessWith1Command(Session("PutFdrvRun") & "IFSVAR.MDB", sUpdate)
    'End Function

    'JRS 10/05/02 WVS
    Public Sub setVPTR(ByRef iValue As Integer)
        'UPGRADE_WARNING: Couldn't resolve default property of object VPTR. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        VPTR = iValue
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setLabels(ByRef i As Short)
        If SDimNum = 1 And FlagGroup = True Then
            'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label1 = GroupNames(SDimPoint)
        Else
            'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Label1 = GRPS(SDimNum, SDimPoint)
        End If
        'UPGRADE_WARNING: Couldn't resolve default property of object Label2. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        Label2 = GRPS(DimNum, i)
        'JRS 03/04/09
        Session("Label1") = Label1

    End Sub

    'JRS 10/05/02 WVS
    Public Sub setSDimNum(ByRef iValue As Integer)
        SDimNum = iValue
        Session("SDimNum") = SDimNum
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setSDimPoint(ByRef iValue As Integer)
        SDimPoint = iValue
        Session("SDimPoint") = SDimPoint
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setDimNum(ByRef iValue As Integer)
        DimNum = iValue
    End Sub
    Public Sub setFileNum(ByRef iValue As Integer)
        'UPGRADE_WARNING: Couldn't resolve default property of object FileNum. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        FileNum = iValue
        'JRS 02/25/04
        Session("FileNum") = FileNum
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setCalcRunningFalse()
        CalcRunning = False
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setFlagGroup(ByRef bValue As Boolean)
        FlagGroup = bValue
        'JRS 04/17/03
        GroupOn = bValue
        'JRS 03/13/04
        Session("FlagGroup") = FlagGroup
        Session("GroupOn") = GroupOn
        'jrs 2005/08/18
        Ag_On_Group = False
        Session("Ag_On_Group") = Ag_On_Group
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setGroupPoint(ByRef iValue As Integer)
        GroupPoint = iValue
        Session("GroupPoint") = GroupPoint
    End Sub

    'JRS 10/05/02 WVS
    Public Sub setDimPoint(ByRef iValue As Integer)
        DimPoint = iValue
    End Sub

    Public Sub mnuViewEquations_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim curr_col As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'JRSVBNET curr_col = 'JRSVBNET Support.DataGridManager.manager.GetCol(grd_VariableDisplay) + 1
        'UPGRADE_WARNING: Couldn't resolve default property of object curr_col. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'UPGRADE_ISSUE: VBControlExtender property grd_VariableDisplay.HelpContextID was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2064"'
        'JRSVBNET grd_VariableDisplay.HelpContextID = Val(Session("VARN")(6, curr_col))
        'CONVERSION_ISSUE: Class 'JRSVBNET System.Windows.Forms.SendKeys from 'JRSVBNET System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'JRSVBNET System.Windows.Forms.SendKeys.Send("{F1}")
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
            Case "Attach More Scenario Output Files"
                mnuScenarioOutFilesAttach_Click(sender, e)
            Case "Explain Scenario Analysis"
                mnuScenarioOutFilesExpScen_Click(sender, e)
            Case "Explain Names"
                mnuVarSelOptExpName_Click(sender, e)
            Case "Show Variables"
                mnuVarSelOptShowVar_Click(sender, e)
            Case "Show Variables and Parameters"
                mnuVarSelOptShowVarPar_Click(sender, e)
            Case "Show Variable and Display Lists"
                mnuVarSelOptShowVarDispList_Click(sender, e)
            Case "History plus Forecast"
                mnuVarSelOptHistoricPlusForecast_Click(sender, e)
            Case "Create Variable List from Selection Status Box"
                mnuVarSelOptCreaVarList_Click(sender, e)
            Case "Create G-List from Selection Status Box" 'jrs 2023/01/10
                Dim sErr As String = ""
                AddGList(sErr, txtGList.Value, Session("PutFdrvRun"), Response)
                If sErr > "" Then
                    Dim PairColor(,) As Integer
                    Call FullArrayColors(PairColor, Session("PairColor"))
                    AddGListDet(sErr, Session("GR1N"), Session("COUNTE"), Session("PutFdrvRun"), PairColor)
                    Session("ImportedGListCode") = sErr
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_CreaGList.aspx")
                End If
            Case "Edit G-List" 'jrs 2023/01/10
                Session("frm_CreaGLIstCaption") = "Edit G-Lists"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_CreaGList.aspx")
            Case "Create Display Lists from Selection Status Box"
                mnuVarSelOptCreaDispList_Click(sender, e)
            Case "Use estimation to complete data"
                mnuEstimation_Click()
            Case "Use estimation to complete data (only holes no extrapolation)"
                'jrs 2011/03/28
                mnuEstimationOnlyHoles_Click()
            Case "Use all available Historical Data" 'jrs 2006/09/26
                mnuUseallHis_Click()
            Case "Exclude Total when using All Dimensions" 'jrs 2009/01/27
                mnuExcTot_Click()
            Case "Group Aggregation" 'jrs 2022/12/14
                mnuGroupAgg_Click()
            Case "Select from Forecasted Historical Variables"
                mnuHisVarOptSelForHisVar_Click(sender, e)
            Case "Select from Empirical Historical Variables"
                mnuHisVarOptSelEmpHisVar_Click(sender, e)
            Case "Select for Validation"
                mnuSelforValidation_Click(sender, e)
            Case "Open for Validation"
                mnuOpenforValidation_Click(sender, e)
            Case "Switch"
                mnuOpen_Click(sender, e)
            Case "Display Menu"
                'Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_DisplayMenu.aspx")
            Case "Search" 'jrs 2005/08/18
                mnuSearch()
            Case "Continue"
                clearVariables()
        End Select
    End Sub
    Private Function clearVariables()
        Session.Remove("VarSelVCat")
        Session.Remove("lstVarNameSelectedIndex")
        Session.Remove("VTracker")
        Session.Remove("LoadVarNameType")
        'jrs 2023/06/26
        'cdv 2022/06/13
        'If Session("JSNeedsUpdate") = True Then
        'CreateJSforEmpVarSelVars()
        'CreateJSforForeVarSelVars()
        'End If
        If Session("COUNTE") > 0 Then
            'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_DisplayMenu.aspx"
            Response.Redirect("frm_DisplayMenu.aspx")
        Else
            'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_MainMenu.aspx"
            Response.Redirect("frm_MainMenu.aspx")
        End If

        'JAM 2014-04-04
        If Not (Session("VarSelVCatName") Is Nothing) Then
            Session.Remove("VarSelVCatName")
        End If

        'JAM 2014-04-04
        If Not (Session("VarSelVCatName") Is Nothing) Then
            Session.Remove("FiltVarSelVCat")
        End If

    End Function

    'cdv 2022/06/13 ****END****

    'Public Sub grd_VariableDisplay_ButtonClick(ByVal sender As System.Object, ByVal e As TMSWebControls.ModEventArgs)
    '    pnlContext.Visible = True
    '    lblRowCol.Text = grd_VariableDisplay(e.Column, 0) & ", " & grd_VariableDisplay(e.Column, 1)
    '    If grd_VariableDisplay(e.Column, 2) > "" Then
    '        lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 2)
    '    End If
    '    Session("VarSelGridCol") = e.Column
    'End Sub

    Private Sub lbtCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtCancel.Click
        pnlContext.Visible = False
        'jrs 10/20/04
        txtVarSearch.Enabled = True
        lstVarName.Visible = True
        lstVarName.Enabled = True
        'jrs 2013/09/05
        cswmWebMenu.Groups("Help").OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

    End Sub

    Private Sub lbtDefine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtDefine.Click
        pnlContext.Visible = False
        'jrs 10/20/04
        txtVarSearch.Enabled = True
        lstVarName.Visible = True
        lstVarName.Enabled = True

        mnuDefine_Click(sender, e)
    End Sub

    Private Sub lbtDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtDelete.Click
        MnuDelete_Click(sender, e)
        pnlContext.Visible = False
        'jrs 10/20/04
        txtVarSearch.Enabled = True
        lstVarName.Visible = True
        lstVarName.Enabled = True
        'jrs 2013/09/05
        cswmWebMenu.Groups("Help").OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")
    End Sub

    Private Sub lbtExplain_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtExplain.Click
        mnuExplain_Click(sender, e)
        pnlContext.Visible = False
        'jrs 10/20/04
        txtVarSearch.Enabled = True
        lstVarName.Visible = True
        lstVarName.Enabled = True
        'jrs 2013/09/05
        cswmWebMenu.Groups("Help").OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")
    End Sub


    Public Sub grd_VariableDisplay_CellClick(ByVal sender As System.Object, ByVal e As TMSWebControls.ModEventArgs) Handles grd_VariableDisplay.CellClick
        'jrs 10/20/04
        txtVarSearch.Enabled = False
        lstVarName.Visible = False
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        'jrs 2005/06/17
        lstFiles.Visible = True
        lbl_files.Visible = True
        'jrs 2012/06/18
        ''jrs 2006/09/20
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    cmb_VCAT.Visible = False
        '    lbl_vcat.Visible = False
        'Else
        '    cmb_VCAT.Visible = True
        '    lbl_vcat.Visible = True
        'End If

        pnlContext.Visible = True
        'jrs 2006/04/04
        lblRowCol.Visible = True
        lblRowCol.Text = grd_VariableDisplay(e.Column, 0)
        If grd_VariableDisplay(e.Column, 1) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 1)
        End If
        If grd_VariableDisplay(e.Column, 2) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 2)
        End If
        If grd_VariableDisplay(e.Column, 3) > "" Then
            lblRowCol.Text = lblRowCol.Text & ", " & grd_VariableDisplay(e.Column, 3)
        End If
        Session("VarSelGridCol") = e.Column
        'jrs 2006/04/04
        lblExplain.Visible = True
        lblExplain.Attributes.Clear()
        'lbtExplain.Attributes.Add("HREF", "javascript:RH_ShowHelp(0,""" & Me.MapPathSecure("") & "\WebHelp\ifshelp.htm""" & ", HH_HELP_CONTEXT, " & Session("VARN")(3, e.Column + 1) & ")")
        'jrs 2013/09/06 lblExplain.Attributes.Add("onclick", "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT," & Session("VARN")(3, e.Column + 1) & ");")
        lblExplain.Attributes.Add("onclick", "javascript:" & LoadIFsVarHelpJS(Session("VARN")(3, e.Column + 1)))
        'lblExplain.Attributes.Add("onclick", "showExplain(" & Session("VARN")(3, e.Column + 1) & ");")
        'lbtExplain.Attributes.Add("onclick", "alert('Hello');")

        'jrs 2006/04/04
        lblViewEquations.Visible = True
        'jrs 09/10/04
        lblViewEquations.Attributes.Clear()
        'jrs 2013/09/06 lblViewEquations.Attributes.Add("onclick", "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT," & Session("VARN")(6, e.Column + 1) & ");")
        lblViewEquations.Attributes.Add("onclick", "javascript:" & LoadIFsVarHelpEqnJS(Session("VARN")(6, e.Column + 1)))
        'jrs 2004/07/06 lblViewEquations.Attributes.Add("onclick", "showEquation(" & Session("VARN")(6, e.Column + 1) & ");")

        'jrs 2005/10/28
        Dim b As Boolean, idx As Integer
        idx = e.Column + 1
        b = idx <= UBound(Session("aFormulas"))
        If b Then
            b = Not (Session("aFormulas")(idx) Is Nothing)
            If b Then
                b = Session("VARN")(1, idx) = Session("aFormulas")(idx).Name
            End If
        End If
        lbtApplyGlist.Visible = b
        optEditFor.Visible = b
        optCopyFor.Visible = b

        'jrs 2013/09/05
        cswmWebMenu.Groups("Help").OnMenuBarClick = LoadHelpJS("View Code", "")
    End Sub

    Public Sub SelectVar()
        cmdComputations.Visible = False

        'jrs 2008/09/25
        optGList.Enabled = False
        optGroupDec.Enabled = False
    End Sub



    Private Sub lstDIM1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDIM1.SelectedIndexChanged
        'jrs 2006/10/27
        If lstDIM1.Items.Count > 0 Then
            Dim old As Integer
            old = cmb_DIM1.SelectedIndex
            cmb_DIM1.SelectedIndex = lstDIM1.SelectedIndex + 1
            If old <> cmb_DIM1.SelectedIndex Then
                Call cmb_DIM1_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub


    'jrs 2005/08/18
    Public Sub LoadSearch(ByVal sVar As String)
        txtVarSearch.Text = sVar
        'jrs 2011/10/18
        lstVarName.Text = sVar

        cmb_VarName_SelectedIndexChanged()
    End Sub

    Private Sub lbtApplyGlist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtApplyGlist.Click
        'jrs 2005/10/28
        Support.FormManager.Show(Me, "frm_ApplyGListComp.aspx")
    End Sub


    Private Sub lbtViewCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbtViewCode.Click
        'jrs 2006/04/04
        Dim valtbl As DataTable
        Dim sql As String
        Dim CurVar As String = Session("VARN")(1, Session("VarSelGridCol") + 1)
        Dim pos As Integer = InStr(CurVar, "[")
        If pos > 0 Then
            CurVar = Mid(CurVar, 1, pos - 1)
        End If
        sql = "SELECT * FROM IFsVar WHERE Name = '" & CurVar & "'"
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", sql, "Name", valtbl)

        Dim sCode As String
        Dim sDef As String = VDef(IfsVars.Item(CurVar).Index)

        sCode = CurVar + "<br>" + VB.StrDup(Len(CurVar) + 10, "-") + "<br>" +
        sDef + "<br>" + VB.StrDup(Len(sDef) + 20, "-") + "<br>"

        Dim iRow As Integer
        Dim svbCode As String

        With valtbl

            iRow = 0

            svbCode = SeekValue(valtbl, CurVar, "vbcode")
            If svbCode > "" Then
                svbCode = Replace(svbCode, VB.Chr(13), "<br>")
                sCode = sCode + "<br>" + svbCode
            Else
                If UCase(Mid(CurVar, 1, 1)) = "S" Then
                    iRow = 0
                    svbCode = SeekValue(valtbl, Mid(CurVar, 2), "vbcode")
                    If svbCode > "" Then
                        svbCode = Replace(svbCode, VB.Chr(13), "<br>")
                        sCode = sCode + "<br>" + svbCode
                    End If
                End If
                'Driver Fix - November 2, 2003
            End If
        End With

        pnlContext.Visible = False

        'Display Msg Form
        LoadMessage(sCode, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")
    End Sub

    'jrs 2006/05/19
    Private Function MaxDimCtr(ByVal sDim As String, ByVal vDim As Integer, ByVal bGroup As Boolean) As Integer
        If sDim = "ALL" Then
            If bGroup And vDim = 1 Then
                MaxDimCtr = GCount
            Else
                MaxDimCtr = MaxVal(vDim) - 1
            End If
        Else
            MaxDimCtr = 1
        End If
    End Function

    Private Sub mnuEstimation_Click()
        'jrs 2006/09/26
        ResetDisplay()

        'jrs 2006/06/09
        Session("bFillHis") = Not CBool(Session("bFillHis"))
        If CBool(Session("bFillHis")) Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").IconImage = "blank.gif"
        End If
        'jrs 2012/06/15
        If Session("bFillHis") Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").IconImage = "blank.gif"
            Session("bFillHisOnlyHoles") = False
        End If
    End Sub

    'jrs 2011/03/28
    Private Sub mnuEstimationOnlyHoles_Click()
        ResetDisplay()

        Session("bFillHisOnlyHoles") = Not CBool(Session("bFillHisOnlyHoles"))
        If CBool(Session("bFillHisOnlyHoles")) Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimationOnlyHoles").IconImage = "blank.gif"
        End If
        If Session("bFillHisOnlyHoles") Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuEstimation").IconImage = "blank.gif"
            Session("bFillHis") = False
        End If
    End Sub

    'jrs 2006/09/11 Clearing Spaghetti Code
    Private Sub cmb_VarName_SelectedIndexChanged()

        'JAM 2014-05-17
        Dim vlstVarNameIndex As Integer


        'jrs 2005/04/28
        If Session("VarClick") = True Then
            Session("VarClick") = False
            Exit Sub
        End If

        'jrs 2010/05/28 don't know why but this is being called twice (Imran?)
        'the second time for variables with no dimensions (WGDP) the varname is empty

        'JAM 2014-05-17 Searching in array not in combo    
        vlstVarNameIndex = FindItemIndex(txtVarSearch.Text)

        ''If lstVarName.SelectedIndex = -1 Then
        ''    Exit Sub
        ''End If

        'JAM 2014-05-17
        If vlstVarNameIndex = -1 Then
            Exit Sub
        End If

        Dim oldlstVarNameSelectedIndex As Integer = -1
        If Not Session("lstVarNameSelectedIndex") Is Nothing Then
            oldlstVarNameSelectedIndex = Session("lstVarNameSelectedIndex")
        End If

        ''Session("lstVarNameSelectedIndex") = lstVarName.SelectedIndex

        'JAM 2014-05-17
        Session("lstVarNameSelectedIndex") = vlstVarNameIndex

        'Read Variable Name
        Dim l As Long
        '' l = InStr(lstVarName.Text, " - ")

        'JAM 2014-05-17
        l = InStr(txtVarSearch.Text, " - ")


        If l > 0 Then
            ''sVar = RTrim(Left(lstVarName.Text, l - 1))

            sVar = RTrim(Left(txtVarSearch.Text, l - 1))
        Else
            ''sVar = lstVarName.Text

            sVar = txtVarSearch.Text
        End If
        sDim_1 = ""
        sDim_2 = ""
        sDim_3 = ""
        idxDim1 = -1
        idxDim2 = -1
        idxDim3 = -1
        bDim2 = True
        bDim3 = True
        bVList = False
        'Check special characters, old computations
        'Dim bContinue As Boolean
        'CheckSpecialCharacters(sVar, bContinue)
        'If Not bContinue Then Exit Sub
        'Check validity for empirical vars, and V-D Lists.
        'If DoingHistory Or Session("ShowVarDispList") Then
        '    bContinue = FindinList(sVar, 0, cmb_VarName.Items.Count - 1, cmb_VarName, -1)
        '    If Not bContinue Then
        '        frm_msg.LoadMessage("Variable " & sVar & " Not Found.")
        '        Support.FormManager.StateStore(Me)
        '        Support.FormManager.Show(Me, "frm_Message.aspx")
        '        Exit Sub
        '    End If
        'End If

        'Read Variable Index
        Session("SEL") = IfsVars.Item(sVar).Index
        If Session("SEL") < 0 Then
            If Session("DoingHistory") Then
                Session("SEL") = GDPidx
            Else
                Session("SEL") = 0
            End If
        End If
        'Find if Display List
        'jam 2021/11/13
        'If Session("ShowVarDispList") Then
        ''dbList = DBEngine.OpenDatabase(Session("PutFdrvRun") + "IFSVAR.mdb")
        ''bVList = (InStr(cmb_VarName.SelectedValue, " - V") > 0)
        ''Load Dimensions
        ''If bVList Then
        ''    'Find Variable to Load 1st Dimension
        ''    SearchDimstoLoadforVList(sVar, Session("SEL"), bDim2, bDim3)
        ''End If
        'ElseIf Session("SEL") = 0 And Not (CalcRunning And IsNumeric(sVar)) Then
        'Dim Msg As String
        'Msg$ = sVar & " is an incorrect variable name"
        'LoadMessage(Msg, Session("Message"))
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")
        'End If

        'jam 2021/11/13
        'cdv 2021/05/06 New logic and rebuilt functions to properly load the display lists and their dimensions
        If Session("ShowVarDispList") Then
            'jrs 2023/05/19 bVList = (InStr(lstVarName.SelectedValue, " - V") > 0)
            bVList = (InStr(txtVarSearch.Text, " - V") > 0)
            If bVList Then
                ''Load Dimensions
                '    'Find Variable to Load 1st Dimension
                SearchDimstoLoadforVList(sVar, Session("SEL"), bDim2, bDim3)
                If Session("SEL") = -1 And Session("DoingHistory") Then
                    Session("SEL") = GDPidx
                End If
            End If
        ElseIf Session("SEL") = 0 And Not (CalcRunning And IsNumeric(sVar)) Then
            Dim Msg As String
            Msg$ = sVar & " is an incorrect variable name"
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If



        'Either Load Dimensions or Load Variable
        If VDim1(Session("SEL")) = 0 Or SumOn Then
            'jrs 2020/07/02 Why not?
            ''if no dimensions don't remember index
            'If oldlstVarNameSelectedIndex = -1 Then
            '    Session("lstVarNameSelectedIndex") = Nothing

            '    lstVarName.SelectedIndex = -1
            'Else
            '    Session("lstVarNameSelectedIndex") = oldlstVarNameSelectedIndex

            '    lstVarName.SelectedIndex = oldlstVarNameSelectedIndex
            'End If
            'jrs 2020/07/02
            fVarchosen.Value = Session("lstVarNameSelectedIndex") - 1

            'Load Variable
            If Not Session("bComputation") Then
                LoadVar(sVar, "", "", "", Session("PLOT"), False, bVList)
            Else
                'jrs 2006/09/27
                Dim cc As Integer
                cc = Session("COUNTE") + 1
                If Session("buseAllHis") And Session("FinYearHis")(cc) > Session("MaxFinYearHis") Then
                    Session("MaxFinYearHis") = Session("FinYearHis")(cc)
                End If

                If Not Session("bCompAll") Then
                    LoadVar(sVar, "", "", "", Session("PLOTComp"), True, bVList)
                Else
                    LoadVar(sVar, "", "", "", Session("PLOTAll"), False, bVList)
                End If

            End If
        Else
            DimNum = VDim1(Session("SEL"))
            DimLoad()
        End If
        Session("sVar") = sVar
        Session("bDim2") = bDim2
        Session("bDim3") = bDim3
        Session("bVList") = bVList
        Session("sDim_1") = sDim_1
        Session("sDim_2") = sDim_2
        Session("sDim_3") = sDim_3
        Session("idxDim1") = idxDim1
        Session("idxDim2") = idxDim2
        Session("idxDim3") = idxDim3

        'jrs 2022/12/16
        Session("mnuGroupAggEnable") = True
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuGroupAgg").Enabled = True
    End Sub
    'jam 2021/11/13
    'cdv 2021/05/03 Full re-write of sub to work in VB.net
    Private Sub SearchDimstoLoadforVList(ByVal sVar As String, ByRef svIdx As Object, ByRef bDim2 As Boolean, ByRef bDim3 As Boolean)
        Dim idx As Integer
        Dim sDim2 As String
        Dim sDim3 As String
        Dim rsListComp As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM VarLists WHERE ListName = """ & sVar & """ ORDER BY ListOrder", "", rsList)
        bDim2 = True
        bDim3 = True
        svIdx = -1
        Dim MaxDimNum As Integer
        MaxDimNum = 0
        'jrs 2007/10/19
        Dim bPov As Boolean, bPovPct As Boolean, bPovFind As Boolean, PovLevel As Single
        'jrs 2014/10/21
        Dim b2005PPP As Boolean
        'jrs 2011/06/17
        Dim bIncR As Boolean, PovLevel2 As Single

        'jrs 2010/05/14
        Dim bMortChl As Boolean, bMortAdl As Boolean
        'loop each variable
        For Each row As DataRow In rsList.Rows
            'read index
            idx = ReadIdx(row("VarName"))
            'jrs 2008/03/25
            If MaxDimNum = 0 And svIdx < 0 And idx > 0 Then
                svIdx = idx
            End If
            'check dimensions
            If VDim1(idx) > 0 And MaxDimNum = 0 Then
                MaxDimNum = 1
                svIdx = idx
            End If
            CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(row("Param2")), "", row("Param2")), sDim2, MaxDimNum, svIdx, "", 2, False)
            CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(row("Param3")), "", row("Param3")), sDim3, MaxDimNum, svIdx, "", 3, False)
            'if formula loop each member
            If row("Formula") Then
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM VarListsFormulas WHERE FormulaId = " & row("FormulaId") & " ORDER BY [CodeVar]", "", rsListComp)
                For Each CompRow As DataRow In rsListComp.Rows
                    idx = ReadIdx(CompRow("VarName"))
                    If VDim1(idx) > 0 And MaxDimNum = 0 Then
                        MaxDimNum = 1
                        svIdx = idx
                    End If
                    CheckDims(bDim2, VDim2(idx), idx, IIf(IsDBNull(CompRow("Dim2")), "", CompRow("Dim2")),
                              sDim2, MaxDimNum, svIdx, IIf(IsDBNull(row("Param2")), "", row("Param2")), 2, True)
                    CheckDims(bDim3, VDim3(idx), idx, IIf(IsDBNull(CompRow("Dim3")), "", CompRow("Dim3")),
                    sDim3, MaxDimNum, svIdx, IIf(IsDBNull(row("Param3")), "", row("Param3")), 3, True)
                Next
            Else 'jrs 2007/10/19
                If idx < 1 Then
                    checkPovertyVarName(row("VarName"), bPov, bPovPct, bPovFind, PovLevel, bIncR, PovLevel2, b2005PPP) 'jrs 2014/10/21 'jrs 2011/06/17 added bIncR, PovLevel2
                    If bPov Or bPovPct Or bPovFind Or bIncR Then 'jrs 2011/06/17 added or bIncR
                        idx = IfsVars.Item("INCOMELT190LN").Index 'jrs 2016/10/12 changed to 190
                    Else 'jrs 2010/05/13 check Mortality Prob
                        checkMortProbName(row("VarName"), bMortChl, bMortAdl)
                        If bMortChl Or bMortAdl Then
                            idx = IfsVars.Item("DEATHCAT").Index
                        End If
                    End If

                    If idx > 0 Then
                        'check dimensions
                        If VDim1(idx) > 0 And MaxDimNum = 0 Then
                            MaxDimNum = 1
                            svIdx = idx
                        End If
                        CheckDims(bDim2, VDim2(idx), idx, IIf(IsNothing(row("Param2")), "", row("Param2")), sDim2, MaxDimNum, svIdx, "", 2, False)
                        CheckDims(bDim3, VDim3(idx), idx, IIf(IsNothing(row("Param3")), "", row("Param3")), sDim3, MaxDimNum, svIdx, "", 3, False)
                    End If
                End If
            End If
        Next
        'if dimensions are different don't Load them, use the ones saved (except on Dim1).
    End Sub


    'jrs 2006/09/08 New simplified Load Variable Routine
    'Consider
    'ALL
    'D-List
    'V-List
    'G-List
    'History plus Forecast
    'Multiple Files
    'Formulas
    'Old Formulas
    'History - Empirical
    Private Sub LoadVar(ByVal sVar As String, ByVal sDim1 As String, ByVal sDim2 As String, ByVal sDim3 As String,
    ByRef TargetV As Object, ByVal bTargetSglDim As Boolean, ByVal bVList As Boolean)
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        'total Variable Loop (sVar, D-List, V-List)
        Dim bAll1 As Boolean, bAll2 As Boolean, bAll3 As Boolean
        Dim bAll_1 As Boolean, bAll_2 As Boolean, bAll_3 As Boolean
        Dim iVarTot As Integer
        Dim bDList As Boolean
        Dim bFormula As Boolean
        Dim idx As Integer
        Dim bHistory As Boolean 'Empirical History
        Dim bGroup
        Dim iFirst As Integer
        Dim bContinue As Boolean
        Dim sName As String, sUn As String, sD1 As String, sD2 As String, sD3 As String
        Dim ff As Formula
        Dim sDef As String
        Dim FinAll As Integer

        bDList = False
        bFormula = False
        bHistory = Session("DoingHistory")
        iFirst = Session("COUNTE") + 1
        'CalcProcess(bContinue, bFormula, sName, sD1, sD2, sD3, sUn, ff)
        'If Not bContinue Then
        '    Exit Sub
        'End If
        'jam 2021/11/13
        'If Session("ShowVarDispList") Then
        ''Variable List was open when looking for Dimensions
        ''If Not bVList Then
        ''    rsList = dbList.OpenRecordset("SELECT * FROM VarLists WHERE ListName = '" + sVar + "' ORDER BY ListOrder")
        ''End If
        ''If Not (rsList.EOF And rsList.BOF) Then
        ''    rsList.MoveLast()
        ''    rsList.MoveFirst()
        ''    bDList = Not bVList
        ''    idx = ReadIdx(rsList.Fields("VarName"))
        ''    If idx < 1 And rsList.Fields("Formula") Then
        ''        'look for index inside formula
        ''        idx = getMaxIdxinFormula(dbList, rsList.Fields("FormulaId"))
        ''    End If
        ''End If
        ''iVarTot = rsList.RecordCount
        'Else
        '   If Not bFormula Then
        idx = Session("SEL")
        '  Else
        '     'idx = getMaxIdxinFormulaFF(ff, sDim1, sDim2, sDim3)
        ' End If
        ' iVarTot = 1
        'End If

        'jam 2021/11/13
        If Session("ShowVarDispList") Then
            'Variable List was open when looking for Dimensions
            'cdv 2021/05/06 Load the table differently if the user selected a dimension, as opposed to a variable
            If bVList Then
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM VarLists WHERE ListName = '" + sVar + "' AND (Type = 'V') ORDER BY ListOrder", "", rsList)
            Else
                SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM VarLists WHERE ListName = '" + sVar + "' AND (Type = 'D') ORDER BY ListOrder", "", rsList)
            End If

            For Each row As DataRow In rsList.Rows
                bDList = Not bVList
                idx = ReadIdx(row("VarName"))
                If idx < 1 And row("Formula") Then
                    'look for index inside formula
                    idx = getMaxIdxinFormula(row("FormulaId"))
                End If
            Next
            iVarTot = rsList.Rows.Count
        Else
            If Not bFormula Then
                idx = Session("SEL")
            Else
                'idx = getMaxIdxinFormulaFF(ff, sDim1, sDim2, sDim3)
            End If
            iVarTot = 1
        End If


        'total Dim1 Loop (sDim1, G-List)
        Dim iDim1Tot As Integer
        Dim sDim1s() As String
        iDim1Tot = getDimLoopSize(bDList, VDim1(idx), sDim1, sDim1s)
        If sDim1 = "ALL" Then
            bAll_1 = True
        End If
        'total Dim2 Loop (sDim2, G-List)
        Dim iDim2Tot As Integer
        Dim sDim2s() As String
        iDim2Tot = getDimLoopSize(bDList, VDim2(idx), sDim2, sDim2s)
        If sDim2 = "ALL" Then
            bAll_2 = True
        End If
        'total Dim3 Loop (sDim3)
        Dim iDim3Tot As Integer
        Dim sDim3s() As String
        iDim3Tot = getDimLoopSize(bDList, VDim3(idx), sDim3, sDim3s)
        If sDim3 = "ALL" Then
            bAll_3 = True
        End If

        'jrs 2008/08/29
        If CSng(iDim1Tot) * CSng(iDim2Tot) * CSng(iDim3Tot) + CSng(Session("COUNTE")) > MaxVariables Then
            LoadMessage("Maximum number of variables exceded, please select less variables.", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Dim iRowPosition As Integer, iFirstYear As Integer
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            iRowPosition = LastYearHistory - FirstYearHistory + 1
            iFirstYear = FirstYearForecast
        Else
            iRowPosition = 1
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                iFirstYear = FirstYearForecast
            Else
                iFirstYear = FirstYearHistory
            End If
        End If

        Dim iv As Integer, id1 As Integer, id2 As Integer, id3 As Integer, ifl As Integer

        'jrs 2011/11/04
        'Create Collection based on Order passed or listBox
        Dim SceOrdFiles As New Collection
        Dim sceFile As Object
        For ifl = 0 To lstFiles.Items.Count - 1
            Session("PDFilesIdxs")(ifl) = False
        Next
        If Trim(txtFiles.Value) > "" Then
            Dim Fs As Object
            Fs = Split(txtFiles.Value, ",")
            For ifl = 0 To UBound(Fs)
                SceOrdFiles.Add(CInt(Fs(ifl)), Fs(ifl).ToString)
            Next
        Else
            For ifl = 0 To lstFiles.Items.Count - 1
                If lstFiles.Items(ifl).Selected Then
                    SceOrdFiles.Add(ifl, ifl.ToString)
                End If
            Next
        End If

        'Variable loop
        For iv = 1 To iVarTot
            'jam 2021/11/13
            If Session("ShowVarDispList") Then
                'bFormula = rsList.Fields("Formula")
                'bHistory = rsList.Fields("History")
                'sVar = rsList.Fields("VarName")
                'idx = ReadIdx(rsList.Fields("VarName"))
                'sDim1 = IIf(IsNull(rsList.Fields("Param1")), "", rsList.Fields("Param1"))
                'sDim2 = IIf(IsNull(rsList.Fields("Param2")), "", rsList.Fields("Param2"))
                'sDim3 = IIf(IsNull(rsList.Fields("Param3")), "", rsList.Fields("Param3"))
            End If

            'cdv 2021/05/06 Pull data from the data table for gLoad
            If Session("ShowVarDispList") Then
                bFormula = rsList.Rows.Item(iv - 1)("Formula")
                bHistory = rsList.Rows.Item(iv - 1)("History")
                sVar = rsList.Rows.Item(iv - 1)("VarName")
                idx = IfsVars.Item(sVar).Index
                sDim1 = IIf(IsDBNull(rsList.Rows.Item(iv - 1)("Param1")), "", rsList.Rows.Item(iv - 1)("Param1"))
                sDim2 = IIf(IsDBNull(rsList.Rows.Item(iv - 1)("Param2")), "", rsList.Rows.Item(iv - 1)("Param2"))
                sDim3 = IIf(IsDBNull(rsList.Rows.Item(iv - 1)("Param3")), "", rsList.Rows.Item(iv - 1)("Param3"))
            End If




            'Dim1 loop
            For id1 = 1 To iDim1Tot
                'jrs 2006/10/08 combining groups and regions in a GList
                If FlagGList Or bDList Then
                    bGroup = Nothing
                Else
                    bGroup = FlagGroup
                End If
                'jrs 2025/07/10 the or bFormula should be first and use orElse
                'If Not bDList And (VDim1(idx) > 0 Or Session("DoingHistory") Or bFormula) Then 'jrs 2006/10/08 or bFormula
                If Not bDList And (bFormula OrElse VDim1(idx) > 0 OrElse Session("DoingHistory")) Then 'jrs 2006/10/08 or bFormula
                    sDim1 = sDim1s(id1)
                    If bAll_1 Then
                        idxDim1 = id1
                    End If
                End If
                'Dim2 loop
                For id2 = 1 To iDim2Tot
                    'jrs 2024/04/14
                    If bAll_2 AndAlso VDim2(idx) = 23 AndAlso Not bMaleFirst Then
                        'Reverse Males, Females
                        If id2 = 1 Then
                            id2 = 2
                        ElseIf id2 = 2 Then
                            id2 = 1
                        End If
                    End If
                    'jrs 2025/07/11 check bFormula first to avoid blow up
                    'If Not bDList And (VDim2(idx) > 0 Or bFormula) And bDim2 Then
                    If Not bDList And bDim2 And (bFormula OrElse VDim2(idx) > 0) Then
                        sDim2 = sDim2s(id2)
                        If bAll_2 Then
                            idxDim2 = id2
                        End If
                    End If
                    'Dim3 loop
                    For id3 = 1 To iDim3Tot
                        'jrs 2024/04/14
                        If bAll_3 AndAlso VDim3(idx) = 23 AndAlso Not bMaleFirst Then
                            'Reverse Males, Females
                            If id3 = 1 Then
                                id3 = 2
                            ElseIf id3 = 2 Then
                                id3 = 1
                            End If
                        End If
                        'jrs 2025/07/11 move bFormula to avoid blow up
                        'If Not bDList And (VDim3(idx) > 0 Or bFormula) And bDim3 Then
                        If Not bDList And bDim3 And (bFormula OrElse VDim3(idx) > 0) Then
                            sDim3 = sDim3s(id3)
                            If bAll_3 Then
                                idxDim3 = id3
                            End If
                        End If
                        'File Loop
                        'jrs 2011/11/11 For ifl = 0 To lstFiles.Items.Count - 1
                        For Each sceFile In SceOrdFiles
                            ifl = CInt(sceFile)
                            Session("PDFilesIdxs")(ifl) = lstFiles.Items(ifl).Selected





                            If lstFiles.Items(ifl).Selected Then
                                'jam 2021/11/13
                                'jrs 2021/05/31
                                Dim bIncompatibleRegionFile As Boolean = False
                                'jrs 2025/10/06
                                Dim IncompatibleRegionIdx As Integer = -1
                                Dim sRunFile As String
                                'jrs 2022/05/26
                                If ifl > 0 Then
                                    sRunFile = Replace(Session("RunFiles")(ifl), Session("PutFdrvRun2") & "\", "")
                                    bIncompatibleRegionFile = Session("IFsRunFiles").item(sRunFile).RegionsHaveChanged
                                    'jrs 2025/10/06
                                    IncompatibleRegionIdx = Session("IFsRunFiles").item(sRunFile).RegionsHaveChangedIdx
                                End If


                                If bFormula Then
                                    'jam 2021/11/21 
                                    Session("COUNTE") += 1
                                    'cdv 2021/05/06 Everything in the IF was implemented for Variable Display

                                    If Session("ShowVarDispList") Then
                                        'jam 2021/11/13
                                        ''If rsList.Fields("FormulaDetHis") > "" Then
                                        ''    gLoadCompHisFor(rsList, sDim1, sDim2, sDim3, ifl, 1, bFillHis)
                                        ''Else
                                        ''    Session("COUNTE") = Session("COUNTE") + 1
                                        ''    gLoadComp(Session("COUNTE"), rsList.Fields("FormulaDet"), sVar, sDim1, sDim2, sDim3, _
                                        ''    "", rsList.Fields("FormulaId"), Null, ifl, rsList.Fields("HistoryplusForecast"), _
                                        ''    rsList.Fields("FormulaDetHis") > "", Nothing, bFillHis, False)
                                        ''End If
                                        'jam 2021/11/13
                                        If IsDBNull(rsList.Rows.Item(iv - 1)("FormulaDetHis")) = False Then
                                            gLoadCompHisFor(rsList, sDim1, sDim2, sDim3, ifl, Session("COUNTE"), Session("bFillHIs"), Session("buseAllHistory"), Session("FinYearHis")(Session("COUNTE")),
                                            Session("COUNTE"), Session("aFormulas"), Session("bComputation"), iFirstYear, Session("LastYear"), Session("LastYearForecast"),
                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("VARN"), Session("FileForDisplay"), IfsPars, TargetV, Session("bFillHisOnly"), Nothing,
                                            Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("StaYearHis")(Session("COUNTE")), Session("mnuSQLite"))
                                        Else
                                            'Function from VB6 reformatted to work with .Net
                                            gLoadComp(Session("COUNTE"), rsList.Rows.Item(iv - 1)("FormulaDet"), sVar, sDim1, sDim2, sDim3,
                                            IIf(IsDBNull(rsList.Rows.Item(iv - 1)("DisplayUnits")), "", rsList.Rows.Item(iv - 1)("DisplayUnits")),
                                            rsList.Rows.Item(iv - 1)("FormulaId"), bGroup, ifl, (Session("FirstYear") = FirstYearHistory And Session("LastYear") > LastYearHistory),
                                            False, ff, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                            True, Session("bComputation"), Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"),
                                            Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("FileforDisplay"), IfsPars,
                                            TargetV, Session("aFormulas"), Session("bFillHisOnlyHoles"),
                                            Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))


                                            'Adds the selected variable to the possible grid data

                                            If Session("bLoadForDisplaySuccess") Then
                                                Session("VARN")(1, Session("COUNTE")) = sVar
                                                Session("VARN")(0, Session("COUNTE")) = sVar
                                                If Not (IsDBNull(sDim1)) Then
                                                    Session("GR1N")(Session("COUNTE")) = sDim1
                                                    Session("FDNG1")(Session("COUNTE")) = sDim1
                                                End If

                                                If Not (IsDBNull(sDim2)) Then
                                                    Session("GR2N")(Session("COUNTE")) = sDim2
                                                    Session("FDNG2")(Session("COUNTE")) = sDim2
                                                End If

                                                If Not (IsDBNull(sDim3)) Then
                                                    Session("GR3N")(Session("COUNTE")) = sDim3
                                                    Session("FDNG3")(Session("COUNTE")) = sDim3
                                                End If

                                                If Not (IsDBNull(rsList.Rows.Item(iv - 1)("DisplayUnits"))) Then
                                                    Session("GR4N")(Session("COUNTE")) = rsList.Rows.Item(iv - 1)("DisplayUnits")
                                                End If
                                            End If
                                        End If


                                    Else
                                        'old computations
                                        'Session("COUNTE") = Session("COUNTE") + 1
                                        ''if using ALL replace with sDim1
                                        'CheckSTRinFormulaReplace(ff, bAll1, "ALL", sDim1, 1)
                                        'CheckSTRinFormulaReplace(ff, bAll2, "ALL", sDim2, 2)
                                        'CheckSTRinFormulaReplace(ff, bAll3, "ALL", sDim3, 3)
                                        'gLoadComp(Session("COUNTE"), sFormulaDet, sName, "", "", "", sUn, -1, bGroup, _
                                        'ifl, Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast", False, ff, bFillHis, True)
                                        'If GR1N(Session("COUNTE")) = "" And sD1 > "" Then
                                        '    GR1N(Session("COUNTE")) = Replace(sD1, "ALL", sDim1)
                                        'End If
                                        'If GR2N(Session("COUNTE")) = "" And sD2 > "" Then
                                        '    GR2N(Session("COUNTE")) = Replace(sD2, "ALL", sDim2)
                                        'End If
                                        'If GR3N(Session("COUNTE")) = "" And sD3 > "" Then
                                        '    GR3N(Session("COUNTE")) = Replace(sD3, "ALL", sDim3)
                                        'End If

                                        'If bAll1 Then
                                        '    'restore all
                                        '    CheckSTRinFormulaReplace(ff, bAll1, sDim1, "ALL", 1)
                                        'End If
                                        'If bAll2 Then
                                        '    'restore all
                                        '    CheckSTRinFormulaReplace(ff, bAll2, sDim2, "ALL", 2)
                                        'End If
                                        'If bAll3 Then
                                        '    'restore all
                                        '    CheckSTRinFormulaReplace(ff, bAll3, sDim3, "ALL", 3)
                                        'End If
                                    End If
                                Else
                                    Session("COUNTE") = Session("COUNTE") + 1
                                    If bHistory Then
                                        'jam 2021/11/14
                                        'gLoadForDisplayHis(sVar, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3,
                                        'bGroup, 1, TargetV, Session("COUNTE"), FirstYearHistory, LastYearHistory,
                                        'bTargetSglDim, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                        'Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                                        'Session("LastYearForecast"), Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                                        'Session("bCompAll"), Session("dataAll"), Session("StaYearHis"))
                                        'gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, sDim1, bGroup, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                                        'Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

                                        'jam 2021/11/14
                                        gLoadForDisplayHis(sVar, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3,
                                       bGroup, 1, TargetV, Session("COUNTE"), FirstYearHistory, LastYearHistory,
                                       bTargetSglDim, False, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                       Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                                       Session("LastYearForecast"), Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                                       Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                        gLoadForDisplayHisSaveLbls(Session("COUNTE"), sVar, sDim1, bGroup, sDef, sUn, "", Session("FileNumbr"), Session("VARN"),
                                        Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

                                    Else
                                        'jam 2021/11/14
                                        'gLoadForDisplay(sVar, idx, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3, "", ifl, True, True, bGroup, 1, TargetV, Session("COUNTE"), _
                                        'iRowPosition, iFirstYear, Session("LastYear"), 0, bTargetSglDim, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), _
                                        'Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"), _
                                        'Session("IFs_MDB2Active"), Session("CurrencyVal"))
                                        'gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, bGroup, ifl, "",
                                        'Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"),
                                        'Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        'Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

                                        If Session("mnuSQLite") = "True" Then
                                            'jrs 2025/10/06 added IncompatibleRegionIdx
                                            gLoadForDisplayNET(sVar, idx, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3, "", ifl, False, False, bGroup, 1, TargetV, Session("COUNTE"),
                                            iRowPosition, iFirstYear, Session("LastYear"), 0, bTargetSglDim, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                                            Session("ShortSce"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), Session("FileForDisplay" + CStr(ifl)), Session("df" + sVar + CStr(ifl)),
                                            bIncompatibleRegionFile, IncompatibleRegionIdx)
                                        Else
                                            gLoadForDisplay(sVar, idx, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3, "", ifl, True, True, bGroup, 1, TargetV, Session("COUNTE"),
                                            iRowPosition, iFirstYear, Session("LastYear"), 0, bTargetSglDim, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
                                            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                                            Session("IFs_MDB2Active"), Session("CurrencyVal"))
                                        End If

                                        'jam 2021/11/14
                                        gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, bGroup, ifl, "",
                                        Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"),
                                        Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
                                        Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))

                                        'jrs 2022/04/22
                                        Session("FileNum") = ifl

                                    End If
                                    If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" And Not bFormula And Not bHistory Then
                                        FinAll = Session("FinYear")(Session("COUNTE"))
                                        'jrs 2012/11/09
                                        If VDim1(idx) = 0 And sDim1 = "" Then
                                            sDim1 = GRPS(1, 1)
                                        End If
                                        'jrs 2014/04/04
                                        If Session("buseAllHis") And Session("bComputation") Then
                                            SaveForecastPlot(iRowPosition, Session("iFirstYearH"), TargetV, bTargetSglDim, Session("COUNTE"), Session("LastYear"), Session("PLOTCompF"), Session("ComputationsSelectedIndex")) 'jrs 2014/05/05
                                        End If
                                        'jam 2021/11/14
                                        'jrs 2006/06/06 FirstYear = FirstYearHistory
                                        'jrs 2006/06/08 lastyearhistory - 1 in order to keep forecasted values in year 2000 instead of historic ones
                                        'gLoadForDisplayHis(sVar, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3, _
                                        'bGroup, 1, TargetV, Session("COUNTE"), FirstYearHistory, LastYearHistory - 1, _
                                        'bTargetSglDim, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")), _
                                        'Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"), _
                                        'Session("LastYearForecast"), Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"), _
                                        'Session("bCompAll"), Session("dataAll"), Session("StaYearHis"))
                                        'Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22
                                        'jam 2021/11/14

                                        gLoadForDisplayHis(sVar, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3,
                                       bGroup, 1, TargetV, Session("COUNTE"), FirstYearHistory, LastYearHistory - 1,
                                       bTargetSglDim, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                                       Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                                       Session("LastYearForecast"), Session("bComputation"), sDef, sUn, IfsPars, Session("bFillHisOnlyHoles"),
                                       Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))
                                        Session("FinYear")(Session("COUNTE")) = LastYearHistory - FirstYearHistory + FinAll 'jrs 2006/09/22


                                    End If
                                End If
                                'jrs 2022/06/24 added  and Not bIncompatibleRegionFile 
                                If Not Session("bLoadForDisplaySuccess") And Not bIncompatibleRegionFile Then
                                    Session("COUNTE") = Session("COUNTE") - 1
                                    LoadVarEnd(iFirst)
                                    Exit Sub
                                End If
                                If bTargetSglDim Then
                                    If UBound(TargetV) = Session("COUNTE") Then
                                        LoadMessage("The Target Array has reached its maximum capacity.", Session("Message"))
                                        Support.FormManager.StateStore(Me)
                                        Support.FormManager.Show(Me, "frm_Message.aspx")
                                    End If
                                Else
                                    If UBound(TargetV, 2) = Session("COUNTE") Then
                                        LoadMessage("The Target Array has reached its maximum capacity.", Session("Message"))
                                        Support.FormManager.StateStore(Me)
                                        Support.FormManager.Show(Me, "frm_Message.aspx")
                                    End If
                                End If
                            End If 'if file is selected.
                        Next 'jrs 2011/11/11 ifl

                        'jrs 2024/04/14
                        If bAll_3 AndAlso VDim3(idx) = 23 AndAlso Not bMaleFirst Then
                            'Reverse Males, Females
                            If id3 = 1 Then
                                id3 = 2
                            ElseIf id3 = 2 Then
                                id3 = 1
                            End If
                        End If

                    Next id3

                    'jrs 2024/04/14
                    If bAll_2 AndAlso VDim2(idx) = 23 AndAlso Not bMaleFirst Then
                        'Reverse Males, Females
                        If id2 = 1 Then
                            id2 = 2
                        ElseIf id2 = 2 Then
                            id2 = 1
                        End If
                    End If

                Next id2
            Next id1
            If Session("ShowVarDispList") Then
                'rsList.MoveNext()
            End If
        Next iv

        LoadVarEnd(iFirst)

        'jrs 2012/07/03
        Session.Remove("bVARN0Loaded")

        'jrs 2012/01/27 the 27 (Esc) messes up the return key on varselect
        cswmWebMenu.AccessorKeyCode = 0
    End Sub

    'jam 2021/11/13
    'cdv 2021/05/06 full rewrite of getMaxIdx to work for VB.net
    Private Function getMaxIdxinFormula(sFid As String) As Integer
        Dim rsComp As DataTable
        SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM VarListsFormulas WHERE FormulaId = " & sFid & " ORDER BY [CodeVar]", "", rsComp)
        Dim idx As Integer, MaxDimen As Integer
        MaxDimen = 0
        Dim svIdx As Integer

        For Each row In rsComp.Rows
            idx = ReadIdx(row("VarName"))
            If svIdx < 1 Then
                svIdx = idx
            End If
            If VDim2(idx) > 0 Then
                If MaxDimen < 2 Then
                    svIdx = idx
                    MaxDimen = 2
                End If
                If VDim3(idx) > 0 Then 'jrs 2006/05/24 Dim3
                    If MaxDimen < 3 Then
                        svIdx = idx
                    End If
                    MaxDimen = 3
                    Exit For
                End If
            End If
            If VDim1(idx) > 0 And MaxDimen = 0 Then
                MaxDimen = 1
                svIdx = idx
            End If
        Next
        getMaxIdxinFormula = svIdx
    End Function
    'jrs 2006/09/08
    'Fuction to establish the size of the loop for each dimension
    Private Function getDimLoopSize(ByVal bDList As Boolean, ByVal VDim As Integer, ByVal sDim As String, ByRef sDims() As String) As Integer
        Dim iDimTot As Integer
        Dim i As Integer
        If bDList Then
            iDimTot = 1
        Else
            If VDim = 1 Or Session("DoingHistory") Then
                If FlagGList Then
                    Dim rsGL As DataTable
                    'jrs 2025/07/29
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM GList d, GListId i WHERE i.Code = d.GListCode AND i.Description = """ & sDim & """ ORDER BY d.MemberOrder", "", rsGL)
                    iDimTot = rsGL.Rows.Count
                    ReDim sDims(iDimTot)
                    i = 0
                    Do While i < iDimTot
                        i = i + 1
                        sDims(i) = rsGL.Rows(i - 1).Item("Region")
                    Loop
                ElseIf FlagGroup Then
                    If sDim = "ALL" Then
                        iDimTot = GCount
                        ReDim sDims(iDimTot)
                        For i = 1 To GCount
                            sDims(i) = GroupNames(i)
                        Next i
                    End If
                ElseIf FlagGroupDec Then 'jrs 2008/09/03
                    Dim rsGD As DataTable
                    SetDataTablefromAccess(Session("IFs_MDB2Active"),
                    "SELECT * FROM " & GroupIDNew & " i, " &
                    Grouping & " d WHERE i.GroupNum = d.GroupNo AND i.GroupName = """ & sDim & """ ORDER BY Region",
                    "", rsGD)
                    iDimTot = rsGD.Rows.Count
                    ReDim sDims(iDimTot)
                    i = 0
                    Do While i < iDimTot
                        i = i + 1
                        sDims(i) = rsGD.Rows(i - 1).Item("Region")
                    Loop
                Else
                    If sDim = "ALL" Then
                        iDimTot = NR
                        ReDim sDims(iDimTot)
                        For i = 1 To NR
                            sDims(i) = GRPS(1, i)
                        Next i
                    End If
                End If
            Else
                If sDim = "ALL" Then
                    iDimTot = MaxVal(VDim) - 1
                    ReDim sDims(iDimTot)
                    For i = 1 To iDimTot
                        sDims(i) = GRPS(VDim, i)
                    Next i
                    'jrs 2009/01/21 Total
                    If UCase(sDims(iDimTot)) <> "TOTAL" Then
                        If Not Session("bExcTot") Then
                            iDimTot = iDimTot + 1
                            ReDim Preserve sDims(iDimTot)
                            sDims(iDimTot) = "Total"
                        End If
                    Else
                        If Session("bExcTot") Then
                            iDimTot = iDimTot - 1
                            ReDim Preserve sDims(iDimTot)
                        End If
                    End If

                End If
            End If
        End If
        If iDimTot = 0 Then
            iDimTot = 1
            ReDim sDims(1)
            sDims(1) = sDim
        End If
        getDimLoopSize = iDimTot
    End Function

    'jrs 2006/09/18
    'End of LoadVar routine
    Private Sub LoadVarEnd(ByVal iFirst As Integer)

        'If Not dbList Is Nothing Then
        '    rsList.Close()
        '    rsList = Nothing
        '    dbList.Close()
        '    dbList = Nothing
        'End If

        If Session("bComputation") Then
            'jrs 2006/09/27
            If Session("buseAllHis") And Session("MaxFinYearHis") > Session("FinYearHis")(iFirst) Then
                Session("FinYearHis")(iFirst) = Session("MaxFinYearHis")
                If Session("bCompAll") Then
                    Dim i As Integer
                    For i = iFirst + 1 To Session("COUNTE")
                        Session("FinYearHis")(i) = Session("MaxFinYearHis")
                    Next i
                End If
            End If
            If Session("bCompAll") And VDim1(Session("SEL")) = 0 Then
                'jrs 2011/04/13 need to copy result in all columns of vector in case is used in formula
                For i = Session("COUNTE") + 1 To Session("COUNTE") + NR - 1
                    For j = 1 To Session("FinYear")(1)
                        Session("PLOTAll")(j, i) = Session("PLOTAll")(j, Session("COUNTE"))
                    Next j
                Next i
            End If

            If bCompVisible Then
                Support.FormManager.Show(Me, "frm_computations.aspx")
            End If
        Else
            If iFirst = 1 And Not IsNothing(cswmWebMenu) Then
                'jrs 2006/11/15 cswmWebMenu.Groups("mnuContinue").DownLevelURL = "frm_DisplayMenu.aspx"
                cswmWebMenu.Groups("mnuContinue").OnMenuBarClick = "location.href='frm_DisplayMenu.aspx';"
            End If
            FillGrid(iFirst, Session("COUNTE"))
        End If

        'mti Validation changes
        If Session("DoingHistory") Then
            ChkEndPickingHistory()
        Else
            ChkPickingForecast()
        End If
        'mti Validation changes

        'jrs 2022/12/16
        Session("mnuGroupAggEnable") = False
        cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuGroupAgg").Enabled = False
    End Sub

    'jrs 2006/09/12
    Private Sub FillGrid(ByVal iFirst As Integer, ByVal iLast As Integer)
        'jrs 2024/11/25 Dim iloop As Short
        Dim iloop As Integer
        If Not IsNothing(grd_VariableDisplay) Then
            ''grd_VariableDisplay.Columns.Clear()
            'grd_VariableDisplay.Visible = (Session("COUNTE") > 0)
            '
            ''JRS 02/25/04
            'For iloop = iFirst To iLast

            'Dim myCol As New TMSWebControls.Column

            'myCol.WidthType = TMSWebControls.WType.Absolute
            ''myCol.ButtonWidth = 100
            'myCol.Width = 100
            'myCol.Color = System.Drawing.Color.White
            'grd_VariableDisplay.Columns.Add(myCol)

            'grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
            'grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)
            'grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
            'grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
            'grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop)
            grd_VariableDisplay.Visible = (Session("COUNTE") > 0)

            'JRS 02/25/04
            'cdv 2021/02/02 Boolean to see if the user selected All or anything else
            Dim bAll As Boolean
            Dim checkedAll As Boolean
            For iloop = iFirst To iLast
                If Session("COUNTE") > iloop And checkedAll = False Then
                    bAll = True
                    checkedAll = True
                ElseIf checkedAll = False Then
                    bAll = False
                    checkedAll = True
                End If
                'cdv 2021/02/02 ****END****
                Dim myCol As New TMSWebControls.Column

                myCol.WidthType = TMSWebControls.WType.Absolute
                'myCol.ButtonWidth = 100
                myCol.Width = 100
                myCol.Color = System.Drawing.Color.White
                grd_VariableDisplay.Columns.Add(myCol)

                grd_VariableDisplay(iloop - 1, 0) = Session("VARN")(1, iloop)
                grd_VariableDisplay(iloop - 1, 1) = Session("GR1N")(iloop)
                'cdv 2021/02/02 ****START****
                '1. if it's non-gendered, change nothing
                '2. if it's bMaleFirst, change nothing
                '3. if it's not an "ALL" selection, change nothing
                '4. otherwise, flip the order
                'jrs 2024/05/14 I don't think this is needed, it was giving problems when doing all (bAll=true, for Female or Male)
                'If Session("GR2N")(iloop) <> "Female" And Session("GR2N")(iloop) <> "Male" Then
                '    grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
                'ElseIf bMaleFirst Then
                '    grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
                'ElseIf bAll = False Then
                '    grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)
                'ElseIf Session("GR2N")(iloop) = "Male" Then
                '    grd_VariableDisplay(iloop - 1, 2) = "Female"
                'Else
                '    grd_VariableDisplay(iloop - 1, 2) = "Male"
                'End If
                grd_VariableDisplay(iloop - 1, 2) = Session("GR2N")(iloop)

                'jrs 2024/05/14 I don't think this is needed, it was giving problems when doing all (bAll=true, for Female or Male)
                'If Session("GR3N")(iloop) <> "Female" And Session("GR3N")(iloop) <> "Male" Then
                '    grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
                'ElseIf bMaleFirst Then
                '    grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
                'ElseIf bAll = False Then
                '    grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
                'ElseIf Session("GR3N")(iloop) = "Male" Then
                '    grd_VariableDisplay(iloop - 1, 3) = "Female"
                'Else
                '    grd_VariableDisplay(iloop - 1, 3) = "Male"
                'End If
                grd_VariableDisplay(iloop - 1, 3) = Session("GR3N")(iloop)
                'cdv 2021/02/02 ****END****
                grd_VariableDisplay(iloop - 1, 4) = Session("GR4N")(iloop)

            Next iloop
        End If

    End Sub

    'jrs 2006/09/20 Fixing Spaghetti Code
    Private Sub cmb_DIM1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_DIM1.SelectedIndexChanged
        'Setting flag for Computations for ALL
        If Session("bComputation") And cmb_DIM1.SelectedValue = "ALL" Then
            Session("bCompAll") = True
            Dim dimsize As Integer
            Select Case ""
                Case sDim_1
                    Session("iDimAll") = 1
                    dimsize = MaxVal(VDim1(Session("SEL"))) - 1
                Case sDim_2
                    If UCase(sDim_1) = "ALL" Then
                        LoadMessage("Formulas don't allow ALL in more than one dimension.", Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                    Session("iDimAll") = 2
                    dimsize = MaxVal(VDim2(Session("SEL"))) - 1
                Case sDim_3
                    If UCase(sDim_2) = "ALL" Or UCase(sDim_3) = "ALL" Then
                        LoadMessage("Formulas don't allow ALL in more than one dimension.", Session("Message"))
                        Support.FormManager.StateStore(Me)
                        Support.FormManager.Show(Me, "frm_Message.aspx")
                    End If
                    Session("iDimAll") = 3
                    dimsize = MaxVal(VDim3(Session("SEL"))) - 1
            End Select
            If Session("ComputationsDimensionSize") = 0 Then
                Session("ComputationsDimensionSize") = dimsize
            ElseIf Session("ComputationsDimensionSize") <> dimsize Then
                LoadMessage("This variable doesn't have the same dimension size chosen before", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")
            End If
        End If
        'Load Dimensions
        Select Case ""
            Case sDim_1
                sDim_1 = cmb_DIM1.SelectedValue
                Session("sDim_1") = sDim_1
                If Session("DoingHistory") Then
                    sPickHRegion = sDim_1
                End If
                If VDim2(Session("SEL")) > 0 And bDim2 Then
                    DimNum = VDim2(Session("SEL"))
                    DimLoad()
                    Exit Sub
                End If
            Case sDim_2
                sDim_2 = cmb_DIM1.SelectedValue
                Session("sDim_2") = sDim_2
                If VDim3(Session("SEL")) > 0 And bDim3 Then
                    DimNum = VDim3(Session("SEL"))
                    DimLoad()
                    Exit Sub
                End If
            Case sDim_3
                sDim_3 = cmb_DIM1.SelectedValue
                Session("sDim_3") = sDim_3
        End Select
        'Remove Dimension ComboBox
        RemoveDim1()

        'if no Dimension to load, then load variable
        If Not Session("bComputation") Then
            LoadVar(sVar, sDim_1, sDim_2, sDim_3, Session("PLOT"), False, bVList)
        Else
            'jrs 2006/09/27
            Dim cc As Integer
            cc = Session("COUNTE") + 1
            If Session("buseAllHis") And Session("FinYearHis")(cc) > Session("MaxFinYearHis") Then
                Session("MaxFinYearHis") = Session("FinYearHis")(cc)
            End If

            If Not Session("bCompAll") Then
                LoadVar(sVar, sDim_1, sDim_2, sDim_3, Session("PLOTComp"), True, bVList)
            Else
                LoadVar(sVar, sDim_1, sDim_2, sDim_3, Session("PLOTAll"), False, bVList)
            End If

        End If

    End Sub

    'jrs 2006/09/26
    Private Sub mnuUseallHis_Click()
        'jrs 2006/09/26
        ResetDisplay()

        Session("buseAllHis") = Not CBool(Session("buseAllHis"))
        If CBool(Session("buseAllHis")) Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuuseAllHis").IconImage = "blank.gif"
        End If

    End Sub

    'jrs 2008/09/03
    Private Sub ResetControlsforGrouping()
        Session("FlagGroup") = FlagGroup
        Session("FlagGList") = FlagGList
        Session("FlagGroupDec") = FlagGroupDec

        txtVarSearch.Visible = True
        lstVarName.Visible = True
        lstVarName.Enabled = True
        lbl_VarName.Visible = True
        txtVarSearch.Enabled = True
        lbl_VarName.Enabled = True
        cmb_DIM1.Visible = False
        lstDIM1.Visible = False
        lbl_Dim1.Visible = False
        'jrs 2005/10/27
        If Not (cswmWebMenu Is Nothing) Then
            If cswmWebMenu.Groups.Count > 0 Then
                cswmWebMenu.Groups("mnuScenarioOutputFiles").Items("mnuScenarioOutFilesExp").Enabled = True
            End If
        End If
        ' JRS 11/24/01 DISPLAY but_File.Visible = True

        lbl_files.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
        lstFiles.Visible = True
        'jrs 2012/06/18
        ''jrs 2006/09/20
        'If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
        '    cmb_VCAT.Visible = False
        '    lbl_vcat.Visible = False
        'Else
        '    cmb_VCAT.Visible = True
        '    lbl_vcat.Visible = True
        'End If

        'JRS 03/13/04

        'JAM 2011-12-03 Changing radio button behavior in front-end side
        'lstVarName.ClearSelection()



    End Sub

    'jrs 2012/11/19 enabling option buttons again 
    'jrs 2008/09/03
    'JAM 2011-12-03
    Protected Sub optGList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGList.CheckedChanged
        FlagGList = True
        FlagGroup = False
        FlagGroupDec = False
        cmdComputations.Enabled = False

        ResetControlsforGrouping()
    End Sub

    'JAM 2011-12-04 Manging selection in front-end side
    'jrs 2008/09/03
    Protected Sub optGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroup.CheckedChanged
        cmdComputations.Enabled = True
        Ag_On_Region = False
        FlagGroup = True
        FlagGList = False
        FlagGroupDec = False

        ResetControlsforGrouping()

    End Sub

    'JAM 2011-12-04 Manging selection in front-end side
    'jrs 2008/09/03
    Protected Sub optReg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReg.CheckedChanged
        cmdComputations.Enabled = True
        Ag_On_Group = False
        FlagGList = False
        FlagGroup = False 'jrs 2005/07/14
        FlagGroupDec = False

        ResetControlsforGrouping()
    End Sub
    'JAM 2011-12-04 Manging selection in front-end side
    'jrs 2008/09/03
    Protected Sub optGroupDec_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroupDec.CheckedChanged
        FlagGList = False
        FlagGroup = False
        FlagGroupDec = True
        cmdComputations.Enabled = False

        ResetControlsforGrouping()

    End Sub

    'jrs 2009/01/27
    Private Sub mnuExcTot_Click()
        Session("bExcTot") = Not Session("bExcTot")
        If Session("bExcTot") Then
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuExcTot").IconImage = "checkbox.gif"
        Else
            cswmWebMenu.Groups("mnuVarSelOpt").Items("mnuExcTot").IconImage = "blank.gif"
        End If
    End Sub

    Protected Sub btnCont_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCont.Click
        clearVariables()
    End Sub

    Protected Sub lstVarName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstVarName.SelectedIndexChanged

        Call cmb_VarName_SelectedIndexChanged()
    End Sub

    Protected Sub btnInvs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInvs.Click


        Call cmb_VarName_SelectedIndexChanged()

    End Sub

    Protected Sub txtVarSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVarSearch.TextChanged
        'JAM 2011-12-04 Managing radio buttons in front-end side
        If optGList.Checked = True Then
            FlagGList = True
            FlagGroup = False
            FlagGroupDec = False
            cmdComputations.Enabled = False

            ResetControlsforGrouping()
        Else
            If optReg.Checked Then
                cmdComputations.Enabled = True
                Ag_On_Group = False
                FlagGList = False
                FlagGroup = False 'jrs 2005/07/14
                FlagGroupDec = False

                ResetControlsforGrouping()

            Else
                If optGroup.Checked Then
                    cmdComputations.Enabled = True
                    Ag_On_Region = False
                    FlagGroup = True
                    FlagGList = False
                    FlagGroupDec = False

                    ResetControlsforGrouping()
                Else
                    If optGroupDec.Checked Then
                        FlagGList = False
                        FlagGroup = False
                        FlagGroupDec = True
                        cmdComputations.Enabled = False

                        ResetControlsforGrouping()
                    Else    'Default Regions 
                        cmdComputations.Enabled = True
                        Ag_On_Group = False
                        FlagGList = False
                        FlagGroup = False 'jrs 2005/07/14
                        FlagGroupDec = False

                        ResetControlsforGrouping()
                    End If


                End If


            End If

        End If


    End Sub


    'JAM 2011-09-29 Managing Focus after Variable selection
    'Protected Sub txtVarSearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVarSearch.TextChanged


    '    Dim a, b, c
    '    a = lstVarName.Text
    '    b = lstVarName.SelectedIndex
    '    c = sVar

    '    ' txtVarSearch.Text = lstVarName.Text  'JAM 2011-10-12 Cleaning chosen variable
    '    cmb_DIM1.Focus()


    'End Sub


    'JAM 2011-10-01 Managing Focus after Variable selection
    'Protected Sub lstDIM1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDIM1.TextChanged
    '    'Dim a, b
    '    'a = VDim2(Session("SEL"))
    '    'b = VDim3(Session("SEL"))
    '    'JAM 2011-10-12 Validating that exists second or third dimmension
    '    If VDim2(Session("SEL")) Or VDim3(Session("SEL")) Then

    '        cmb_DIM1.Focus()
    '    End If
    'End Sub




    Protected Sub lbtDrivers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtDrivers.Click
        Session("DriverParent") = "frm_Varsel.aspx"
        pnlContext.Visible = False
        txtVarSearch.Enabled = True
        lstVarName.Visible = True
        lstVarName.Enabled = True
        mnuDrivers_Click(sender, e)

    End Sub


    'jrs 2013/02/15
    Protected Sub lbtViewHistAnal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtViewHistAnal.Click
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_ShowHistAnalog.aspx")
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2014-04-14 Based on CreateJSforFlexDisplays developed by Jose
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''JAM 2014-04-05  Important Comment: Pending: Recovery data from database
    Public Sub CreateJSforEmpVarSelVars()
        ' ''Dim rsL As DataTable
        ' ''Dim sql As String
        ''Dim Rcnt As Short
        ''Dim Varname As String
        ''Dim VarDefinition As String
        ''Dim sUnits As String
        ''Dim Groupnames As String
        ''Dim divGroups As Array
        ''Dim idivGroups As Short
        ''Dim oneGroup As String
        Dim sJscript As String
        ''Dim FullName As String
        ''Dim sCoVaTra As String
        '' ''Dim sPreprocessor As String, sYears As String, bInclude As Boolean
        '' ''Dim vFilter As String
        ''Dim db As String 'JAM 2014-04-05
        ''Dim sPreprocessor As String, sYears As String 'JAM 2014-04-05




        ' '' ''JAM 2013-08-25 For other filter recovering
        '' ''vFilter = Session("ScatVarFiltName")



        ''db = Session("PutFdrvRun") & "DataDict.MDB"
        ''SetDataTablefromAccess(db, "SELECT * FROM datadict order by Variable", "", valtbl)



        ''sJscript = "   var VarListvse = [];" & vbCr
        ''sJscript = sJscript & "   var FullGroupsvse = [];" & vbCr
        ''sJscript = sJscript & "   var NumGroupsvse = [];" & vbCr
        ''sJscript = sJscript & "   var NumVarsvse = [];" & vbCr
        '' ''sJscript = sJscript & "   var jbInclude = [];" & vbCr
        ''sJscript = sJscript & "   var jModelLoadvse;" & vbCr
        ''sJscript = sJscript & "   var jVarSel_GroupNamesvse;" & vbCr



        ''sJscript = sJscript & "  jModelLoadvse = """ & UCase(ModelLoad) & """;" & vbCr



        '' ''If Not (Session("Scat_GroupNames") Is Nothing) Then
        '' ''    sJscript = sJscript & "  jScat_GroupNames = """ & Session("Scat_GroupNames") & """;" & vbCr
        '' ''Else
        ''sJscript = sJscript & "  jVarSel_GroupNamesvse = """"; " & vbCr
        '' ''End If

        ''For Rcnt = 0 To valtbl.Rows.Count - 1
        ''    sCoVaTra = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("CoVaTra")), "No", valtbl.Rows(Rcnt).Item("CoVaTra"))

        ''    If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(sCoVaTra) = "NO" Then
        ''        Varname = valtbl.Rows(Rcnt).Item("Variable")

        ''        VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")


        ''        VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

        ''        Groupnames = valtbl.Rows(Rcnt).Item("Group")
        ''        If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
        ''            sUnits = ""
        ''        Else
        ''            sUnits = valtbl.Rows(Rcnt).Item("Units")
        ''        End If

        ''        'JAM 2013-08-20
        ''        sPreprocessor = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("UsedinPreprocessor")), "", valtbl.Rows(Rcnt).Item("UsedinPreprocessor"))
        ''        sYears = IIf(IsDBNull(valtbl.Rows(Rcnt).Item("Years")), "", valtbl.Rows(Rcnt).Item("Years"))


        ''        ''If bInclude Then

        ''        FullName = Varname & " - " & VarDefinition & " - " & sUnits

        ''        sJscript = sJscript & "  VarListvse[" & Rcnt.ToString & "] = new Array(""" & FullName & """);" & vbCr

        ''        divGroups = Split(Groupnames, ",")

        ''        idivGroups = 0
        ''        sJscript = sJscript & "  FullGroupsvse[" & Rcnt.ToString & "]= new Array("""
        ''        While idivGroups <= UBound(divGroups)
        ''            oneGroup = Trim(divGroups(idivGroups))
        ''            idivGroups = idivGroups + 1

        ''            If idivGroups = 1 Then
        ''                sJscript = sJscript & oneGroup & """"

        ''            Else
        ''                sJscript = sJscript & "," & """" & oneGroup & """"
        ''            End If

        ''            'sJscript = sJscript & "    SingleGroup[" & idivGroups.ToString & "] = new Array(""" & oneGroup & """);" & vbCr

        ''        End While


        ''        sJscript = sJscript & ");" & vbCr


        ''        sJscript = sJscript & "  NumGroupsvse[" & Rcnt.ToString & "] = " & idivGroups.ToString & ";" & vbCr

        ''        'JAM 2013-07-25 Multidimensional arrays for group
        ''        'sJscript = sJscript & "    FullGroups[" & Rcnt.ToString & "] =SingleGroup;" & vbCr

        ''        'JAM 2013-08-18
        ''        ''End If
        ''    End If
        ''Next Rcnt

        ''sJscript = sJscript & "     NumVarsvse = " & valtbl.Rows.Count.ToString & ";" & vbCr


        ''Dim intfile As Short, sFileName As String
        ''intfile = FreeFile()
        ''sFileName = PutFdrv & "IFsWeb\Scripts\"
        ' ''If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
        ' ''    'write to historic file
        ' ''    sFileName = sFileName & "FlexDisplaysVarsH.js"
        ' ''Else
        ' ''    'write to forecast file
        ''sFileName = sFileName & "VarSel_Vars.js"
        ' '' End If

        ''FileOpen(intfile, sFileName, OpenMode.Output)
        ''PrintLine(intfile, sJscript)
        ''FileClose(intfile)

        'JAM 2014-07-20 For code concentration in Function2
        GenerateVarsforEmpVarSel(Session("PutFdrvRun"))


        sJscript = "   loadjsfile(""Scripts/VarSel_Vars.js"");" & vbCr

        'JAM 2014-05-18
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")
        ''cmb_VCAT.Attributes.Add("onchange", "Forcmb_VCAT_click();")

    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '   JAM 2014-04-14 Based on CreateJSforFlexDisplays developed by Jose
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Public Sub CreateJSforForeVarSelVars()
        ' ''Dim rsL As DataTable
        ' ''Dim sql As String
        ''Dim Rcnt As Short
        ''Dim Varname As String
        ''Dim VarDefinition As String
        ''Dim sUnits As String
        ''Dim Groupnames As String
        ''Dim divGroups As Array
        ''Dim idivGroups As Short
        ''Dim oneGroup As String
        Dim sJscript As String
        ''Dim FullName As String
        '' ''Dim sPreprocessor As String, sYears As String, bInclude As Boolean
        '' ''Dim vFilter As String
        ''Dim db As String







        ''db = Session("PutFdrvRun") & "IFSVAR.mdb"
        ''SetDataTablefromAccess(db, "SELECT * from IFSVAR ORDER BY NAME", "", valtbl)



        ''sJscript = "   var VarListvsf = [];" & vbCr
        ''sJscript = sJscript & "   var FullGroupsvsf = [];" & vbCr
        ''sJscript = sJscript & "   var NumGroupsvsf = [];" & vbCr
        ''sJscript = sJscript & "   var NumVarsvsf = [];" & vbCr
        '' ''sJscript = sJscript & "   var jbInclude = [];" & vbCr
        ''sJscript = sJscript & "   var jModelLoadvsf;" & vbCr
        ''sJscript = sJscript & "   var jFore_GroupNamesvsf;" & vbCr

        ''sJscript = sJscript & "   var HistoricAnalogsvsf = [];" & vbCr



        ''sJscript = sJscript & "  jModelLoadvsf = """ & UCase(ModelLoad) & """;" & vbCr




        ''sJscript = sJscript & "  jFore_GroupNamesvsf = """"; " & vbCr


        ''For Rcnt = 0 To valtbl.Rows.Count - 1


        ''    Varname = valtbl.Rows(Rcnt).Item("Name")

        ''    VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")

        ''    'JAM 2013-08-10 Remove new lines
        ''    VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

        ''    Groupnames = valtbl.Rows(Rcnt).Item("SUBMODULE NAME")
        ''    If IsDBNull(valtbl.Rows(Rcnt).Item("Units")) Then
        ''        sUnits = " No Units"
        ''    Else
        ''        sUnits = valtbl.Rows(Rcnt).Item("Units")
        ''    End If



        ''    FullName = Varname & " - " & VarDefinition & " - " & sUnits

        ''    sJscript = sJscript & "  VarListvsf[" & Rcnt.ToString & "] = new Array(""" & FullName & """);" & vbCr

        ''    'JAM 2014-05-31 Adding Historicplus Analog
        ''    sJscript = sJscript & "  HistoricAnalogsvsf[" & Rcnt.ToString & "] = new Array(""" & VHistoricAnalog(Rcnt.ToString) & """);" & vbCr




        ''    divGroups = Split(Groupnames, ",")

        ''    idivGroups = 0
        ''    sJscript = sJscript & "  FullGroupsvsf[" & Rcnt.ToString & "]= new Array("""
        ''    While idivGroups <= UBound(divGroups)
        ''        oneGroup = Trim(divGroups(idivGroups))
        ''        idivGroups = idivGroups + 1

        ''        If idivGroups = 1 Then
        ''            sJscript = sJscript & oneGroup & """"

        ''        Else
        ''            sJscript = sJscript & "," & """" & oneGroup & """"
        ''        End If

        ''        'sJscript = sJscript & "    SingleGroup[" & idivGroups.ToString & "] = new Array(""" & oneGroup & """);" & vbCr

        ''    End While
        ''    'JAM 2013-08-18

        ''    sJscript = sJscript & ");" & vbCr


        ''    sJscript = sJscript & "  NumGroupsvsf[" & Rcnt.ToString & "] = " & idivGroups.ToString & ";" & vbCr

        ''    'JAM 2013-07-25 Multidimensional arrays for group
        ''    'sJscript = sJscript & "    FullGroups[" & Rcnt.ToString & "] =SingleGroup;" & vbCr

        ''    'JAM 2013-08-18
        ''    ''End If

        ''Next Rcnt

        ''sJscript = sJscript & "     NumVarsvsf = " & valtbl.Rows.Count.ToString & ";" & vbCr


        ''Dim intfile As Short, sFileName As String
        ''intfile = FreeFile()
        ''sFileName = PutFdrv & "IFsWeb\Scripts\"
        ' ''If UCase(ActiveBase) = "IFSBASEHIST.RUN" Then
        ' ''    'write to historic file
        ' ''    sFileName = sFileName & "FlexDisplaysVarsH.js"
        ' ''Else
        ' ''    'write to forecast file
        ''sFileName = sFileName & "VarSel_ForeVars.js"
        ' '' End If

        ''FileOpen(intfile, sFileName, OpenMode.Output)
        ''PrintLine(intfile, sJscript)
        ''FileClose(intfile)

        GenerateVarsforForeVarSel(Session("PutFdrvRun"))


        sJscript = "   loadjsfile(""Scripts/VarSel_ForeVars.js"");" & vbCr
        cmb_VCAT.Attributes.Add("onchange", "cmb_VCAT_click();")

    End Sub

    'jrs 2023/05/18
    Public Sub CreateJSforVarSelLists()
        GenerateVarsforVarSelList(Session("PutFdrvRun"))
        GenerateVarsforVarSelListH(Session("PutFdrvRun"))
        GenerateVarsforVarSelListHF(Session("PutFdrvRun"))
    End Sub


    Protected Sub cmdGeneratejs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGeneratejs.Click
        CreateJSforEmpVarSelVars()
        CreateJSforForeVarSelVars()
        'jrs 2023/05/18
        CreateJSforVarSelLists()

        Session("JSNeedsUpdate") = False
    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''  JAM 2014-05-17  Filling Array for searching index
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub FillSearchArray(ByRef sList As String)
        Dim AddName As String
        Dim ICount As Short
        Dim Groups As Array
        Dim pos1 As Integer
        Dim i2 As Integer



        Dim rsL As DataTable
        Dim b As Boolean

        'JAM 2014-06-08 Array is loaded only it's forecasted mode
        If Session("VEmpOrForeMode") = 0 Or Session("VEmpOrForeMode") Is Nothing Then

            If sList <> "Lists" Then

                'JAM 2014-05-17
                ReDim Session("ArraySearch")(NumbVars + 1)


                For i2 = 1 To NumbVars

                    AddName = V(i2)
                    AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)


                    'JAM 2014-08-03 Removing extra characters
                    AddName = Replace(Replace(AddName, "   ", " "), "  ", " ")
                    AddName = Replace(Replace(Replace(AddName, Chr(10), ""), Chr(13), ""), """", "")


                    If Session("SensitivityFlag") <> "True" Or VType(i2) = 1 Then
                        'jrs 2020/07/02 this was originally commented out on 2012/02/27 in the load of the form
                        'but forgot to remove it from here.
                        'If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                        If Session("VarSelVCat") = "All" Then
                            Select Case sList
                                            'JAM 2014-05-22
                                Case "Full"
                                    ICount = ICount + 1
                                    Session("ArraySearch")(i2) = AddName
                                Case "Variables"
                                    'JAM 2014-05-22
                                    If VType(i2) <> 3 Then
                                        ''lstVarName.Items.Add(New ListItem(AddName))
                                        ICount = ICount + 1
                                        ''VTracker(ICount) = i2
                                        ''JAM 2014-05-17 
                                        Session("ArraySearch")(i2) = AddName
                                    End If
                                Case "Parameters"

                                    ''lstVarName.Items.Add(New ListItem(AddName))
                                    ICount = ICount + 1
                                    ''VTracker(ICount) = i2
                                    ''JAM 2014-05-17 
                                    Session("ArraySearch")(i2) = New ListItem(AddName)
                                Case "HistoricAnalog"
                                    If Not IsDBNull(VHistoricAnalog(i2)) Then

                                        ''lstVarName.Items.Add(New ListItem(AddName))
                                        ICount = ICount + 1
                                        ''VTracker(ICount) = i2
                                        ''JAM 2014-05-17 
                                        Session("ArraySearch")(i2) = AddName
                                    End If
                            End Select
                        Else
                            Dim vname As String = Session("VarSelVCat")
                            Groups = Split(VCatName(i2), ",")
                            pos1 = 0
                            While (pos1 < Groups.Length)
                                If Trim(Groups(pos1)) = vname Then
                                    AddName = V(i2)
                                    'JRS 10/13/03
                                    AddName = AddName & " - " & VDef(i2) & " - " + VUnits(i2)
                                    If Session("SensitivityFlag") <> "True" Or VType(i2) = 1 Then
                                        'jrs 2012/06/15
                                        If (sList = "Variables" And VType(i2) <> 3) Or sList = "Parameters" Or
                                                (sList = "HistoricAnalog" And Not IsDBNull(VHistoricAnalog(i2))) Then
                                            'jrs 2012/02/27 If UCase(ModelLoad) = UCase("LoadCoVaTrA") Or UCase(VCatName(i2)) <> UCase("Transportation") Then
                                            dataString = dataString + AddName + "|"
                                            ''lstVarName.Items.Add(New ListItem(AddName))
                                            ''JAM 2014-05-17 
                                            Session("ArraySearch")(i2) = AddName
                                            ICount = ICount + 1
                                            ''VTracker(ICount) = i2
                                        End If
                                    End If
                                End If
                                pos1 += 1
                            End While
                        End If
                        'End If
                    End If


                Next i2
            Else
                b = (Session("VarSelCheckHis") = "mnuHisVarOptSelEmpHisVar")

                Dim bHF As Boolean
                bHF = (Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast")

                SetDataTablefromAccess(Session("PutFdrvRun") & "IFSVAR.mdb", "SELECT DISTINCT ListName, Type FROM VarLists WHERE History = " & Str(b) & " AND HistoryplusForecast = " & Str(bHF) & " ORDER BY ListName", "", rsL)
                Dim i As Integer
                i = 0

                'JAM 2014-05-17
                ReDim Session("ArraySearch")(rsL.Rows.Count)

                Do While (Not i = rsL.Rows.Count)

                    ''lstVarName.Items.Add(New ListItem(rsL.Rows(i).Item("ListName") + " - " + rsL.Rows(i).Item("Type")))

                    'JAM 2014-05-17 
                    AddName = rsL.Rows(i).Item("ListName") + " - " + rsL.Rows(i).Item("Type")



                    'JAM 2014-08-03 Removing extra characters
                    AddName = Replace(Replace(AddName, "   ", " "), "  ", " ")
                    AddName = Replace(Replace(Replace(AddName, Chr(10), ""), Chr(13), ""), """", "")

                    Session("ArraySearch")(i) = AddName

                    ICount = ICount + 1
                    i = i + 1
                Loop
            End If

            ''JAM 2014-05-17
            Session("CountItems") = ICount
            ''Session("VTracker") = VTracker

            Session("LoadVarNameType") = sList
        End If

    End Sub
    ''' <summary>
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''  JAM 2014-05-17  Searching index
    ''' 
    ''' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    Protected Function FindItemIndex(ByVal vtxtSearch As String) As Integer
        Dim i As Integer
        Dim index As Integer

        Dim vari As String

        'JAM 2014-08-03
        vtxtSearch = Replace(Replace(Replace(Replace(Replace(vtxtSearch, "   ", " "), "  ", " "), Chr(10), ""), Chr(13), ""), """", "")
        'jrs 2023/11/17
        vtxtSearch = Replace(vtxtSearch, "&gt;", ">")
        index = -1
        For i = 1 To Session("CountItems")
            'JAM 2014-08-03
            vari = Replace(Replace(Replace(Replace(Replace(Session("ArraySearch")(i), "   ", " "), "  ", " "), Chr(10), ""), Chr(13), ""), """", "")

            If vtxtSearch = Session("ArraySearch")(i) Then
                index = i
                Exit For

            End If
        Next i

        FindItemIndex = index


    End Function

    Private Sub mnuGroupAgg_Click()
        If sVar = "" And (Session("SEL") = 0 Or Not Session("DoingHistory")) Then
            Dim Msg = "Please select a variable first."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

            Exit Sub
        End If

        Dim dbAgg As SqliteConnection, rsAgg As SqliteDataReader
        Dim dbAggCmd As SqliteCommand
        'Select current one
        Dim sAgg As String, sAggH As String
        Session("lblMixed") = False
        Session("lstHistVisible") = False
        If Session("SEL") > 0 And Not Session("DoingHistory") Then
            sAgg = VAggregation(Session("SEL"))
            'jrs 2011/01/18
            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                Dim sList As String
                Dim HistVar As String
                HistVar = VHistoricAnalog(Session("SEL"))
                'Read historic option too
                dbAgg = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") + "datadict.DB;", String))
                dbAgg.Open()
                dbAggCmd = dbAgg.CreateCommand
                'Single variable
                If UCase(HistVar) <> "IFSVARHISTANA" And UCase(HistVar) <> "FORMULA" And InStr(HistVar, ",") = 0 Then
                    dbAggCmd.CommandText = "SELECT Aggregation FROM DataDict WHERE Variable = """ & HistVar & """"
                    rsAgg = dbAggCmd.ExecuteReader
                ElseIf InStr(HistVar, ",") > 0 Then 'Using commas to separate multiple dimensions
                    Dim x
                    x = Split(HistVar, ",")
                    sList = ""
                    For i = 0 To UBound(x)
                        HistVar = Trim(x(i))
                        sList = sList & """" & HistVar & """"
                        If i < UBound(x) Then
                            sList = sList & ","
                        End If
                    Next i
                    dbAggCmd.CommandText = "SELECT DISTINCT Aggregation FROM DataDict WHERE Variable IN (" & sList & ")"
                    rsAgg = dbAggCmd.ExecuteReader

                ElseIf UCase(HistVar) = "IFSVARHISTANA" Then
                    Dim j As Integer, Total3 As Integer
                    If VDim3(Session("SEL")) > 0 Then
                        Total3 = MAXCOM(VDim3(Session("SEL"))) - 1
                    Else
                        Total3 = 1
                    End If
                    sList = ""
                    For i = 1 To MAXCOM(VDim2(Session("SEL"))) - 1
                        For j = 1 To Total3
                            HistVar = IFsHistAnalogs.Item(sVar, GRPS(VDim2(Session("SEL")), i), GRPS(VDim3(Session("SEL")), j)).sHistAna
                            sList = sList & """" & HistVar & """"
                            If i < MAXCOM(VDim2(Session("SEL"))) - 1 Or j < Total3 Then
                                sList = sList & ","
                            End If
                        Next j
                    Next i
                    dbAggCmd.CommandText = "SELECT DISTINCT Aggregation FROM DataDict WHERE Variable IN (" & sList & ")"
                    rsAgg = dbAggCmd.ExecuteReader

                ElseIf UCase(HistVar) = "FORMULA" Then
                    Dim db As SQLiteConnection
                    db = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") + "IFSVAR.DB;", String))
                    db.Open()

                    Dim dbCmd As SqliteCommand = db.CreateCommand

                    dbAggCmd.CommandText = "SELECT VarName FROM IFSVARHISTANAFORMULASDET WHERE ForVarName = """ & sVar & """"
                    rsAgg = dbAggCmd.ExecuteReader

                    sList = ""
                    Do While rsAgg.Read
                        HistVar = rsAgg("VarName")
                        If sList > "" Then
                            sList = sList & ","
                        End If
                        sList = sList & """" & HistVar & """"
                    Loop
                    rsAgg.Close()
                    db.Close()

                    dbAggCmd.CommandText = "SELECT DISTINCT Aggregation FROM DataDict WHERE Variable IN (" & sList & ")"
                    rsAgg = dbAggCmd.ExecuteReader
                End If
                Session("sListAgg") = sList

                rsAgg.Read()
                'Different Agg rules
                If rsAgg("Aggregation") = sAgg Then
                    sAggH = sAgg
                    Session("bMatchFH") = True
                Else
                    sAggH = rsAgg("Aggregation")
                End If

                If rsAgg.Read Then
                    Session("lblMixed") = True
                    Session("bMatchFH") = False
                    sAggH = ""
                End If

                Session("lstHistVisible") = True

                dbAgg.Close()
            End If 'Doing history plus forecast

        Else 'Doing History
            'Read ifsVar
            dbAgg = New SQLiteConnection(CType("DataSource=" & Session("PutFdrvRun") + "datadict.DB;", String))
            dbAgg.Open()
            dbAggCmd = dbAgg.CreateCommand
            dbAggCmd.CommandText = "SELECT Aggregation FROM DataDict WHERE Variable = """ & sVar & """"
            rsAgg = dbAggCmd.ExecuteReader
            rsAgg.Read()
            sAgg = rsAgg("Aggregation")
            rsAgg.Close()
            dbAgg.Close()
        End If


        If IFsAggRules.Item(sAgg).ForecastVar = "" Then
            Dim Msg = "Variable doesn't have any Aggregation rule associated with it."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        Session("sAgg") = sAgg
        Session("sAggH") = sAggH

        '.Show vbModal
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_VarSelAgg.aspx")

    End Sub

    'jrs 2023/06/26
    Private Sub PackJScritpBuilder()
        Dim sJscript As String, i As Integer

        'jrs 2023/05/19
        Dim sVerNum As String, sSubVerNum As String
        sVerNum = ModelVersion.Replace("Version ", "")
        'jrs 2023/09/11
        Dim pos As Integer
        pos = sVerNum.IndexOf(" ")
        If pos >= 0 Then
            sVerNum = sVerNum.Substring(0, pos)
        End If

        sSubVerNum = SubVersionNumber.ToString
        If SubVersionNumber > 0 Then
            sVerNum = sVerNum & "." & sSubVerNum
        End If
        sVerNum = sVerNum & ".js"

        sJscript = "   loadjsfile(""Scripts/VarSel_VarLists." & sVerNum & """);" & vbCr
        sJscript = sJscript & "   loadjsfile(""Scripts/VarSel_VarListsH." & sVerNum & """);" & vbCr
        sJscript = sJscript & "   loadjsfile(""Scripts/VarSel_VarListsHF." & sVerNum & """);" & vbCr

        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadVarSelLists",
        sJscript, True)
    End Sub
End Class
