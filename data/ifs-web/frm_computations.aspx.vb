Option Strict Off
Option Explicit On
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_computations
    Inherits Page
#Region " Web Form Designer Generated Code "
    Public Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init

        InitializeComponent()
        'jrs 2005/12/19 RAM for Formulas Me.OleDbConnection1.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Data Source=""" & Session("PutFdrvRun") & "IFsVar.mdb""" & ";Jet OLEDB:Engine Type=4;Provider=""" & "Microsoft.Jet.OLEDB.4.0""" & ";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=ReadWrite;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1"
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            'JRS 06/10/04 VARSEL *** BEGIN ***
            If Session("sOriginForm") = "frm_VarSel" AndAlso Not Session("bComputation") Then
                'jrs 2005/06/20
                RefreshForm()

                Dim i As Integer
                If Session("grd_VariableDisplay")(Session("VarSelGridCol"), 0) = "" Then
                    ResetComputationsFrm()
                Else
                    i = Session("VarSelGridCol") + 1
                    If i <= UBound(Session("aFormulas")) Then
                        If Not (Session("aFormulas")(i) Is Nothing) Then
                            If Session("grd_VariableDisplay")(Session("VarSelGridCol"), 0) = Session("aFormulas")(i).Name Then
                                'JRS 08/06/02 COPY FORMULA BEGIN
                                'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
                                'JRSVBNET WinFormsToWeb_MessageBox.Show("Do you want to make a copy of this formula?", MsgBoxStyle.YesNo, "Computations")
                                If Session("CopyFormula") Is Nothing Then 'jrs 2005/10/28 'JRSVBNET MsgBoxResult.No Then
                                    'JRS 01/28/04
                                    txtFormula.Text = Session("aFormulas")(i).Formula_Renamed
                                    lblFormulaName.Text = Session("aFormulas")(i).Name
                                    lblDimensionality.Text = Session("aFormulas")(i).Dimension
                                    lblUnits.Text = Session("aFormulas")(i).Unit
                                    LoadFormula(Session("aFormulas")(i))
                                    DeleteVar(i)
                                    'jrs 2005/12/20 RAM for Formulas UpdateFormulaIdx(i - 1)
                                Else
                                    ResetComputationsFrm()
                                    'JRS 01/28/04
                                    txtFormula.Text = Session("aFormulas")(i).Formula_Renamed
                                    'JRS 08/06/02 COPY FORMULA END
                                    'jrs 2005/10/28
                                    Session("CopyFormula") = Nothing
                                End If
                            Else
                                ResetComputationsFrm()
                            End If
                        Else
                            ResetComputationsFrm()
                        End If
                    Else
                        ResetComputationsFrm()
                    End If
                End If
            ElseIf Session("sOriginForm") = "frm_BilateralInteractionDisplay" AndAlso Not Session("bComputation") Then

                RefreshForm()

                Dim i As Integer

                Dim curlst = DirectCast(Session("curlstSelSeries"), ListBox)
                Dim sCurrentDisplay = If(curlst Is Nothing OrElse curlst.SelectedItem Is Nothing, "", curlst.SelectedItem.Text) ' lstSelSeries.List(lstSelSeries.ListIndex)
                If sCurrentDisplay = "" OrElse If(Session("BL_OtherNewVar"), False) = True Then
                    Session("BL_OtherNewVar") = False
                    ResetComputationsFrm()
                Else
                    i = curlst.SelectedIndex + 1 '= iCurrentIdx
                    If i <= UBound(Session("aFormulas")) Then
                        If Not (Session("aFormulas")(i) Is Nothing) Then
                            If sCurrentDisplay = Session("aFormulas")(i).Name Then
                                'JRS 08/06/02 COPY FORMULA BEGIN

                                txtFormula.Text = Session("aFormulas")(i).Formula_Renamed
                                lblFormulaName.Text = Session("aFormulas")(i).Name
                                lblDimensionality.Text = Session("aFormulas")(i).Dimension
                                lblUnits.Text = Session("aFormulas")(i).Unit
                                LoadFormula(Session("aFormulas")(i))
                                'ttt
                                f = Session("aFormulas")(i)
                                'DeleteVar(i)

                            Else
                                ResetComputationsFrm()
                            End If
                        Else
                            ResetComputationsFrm()
                        End If
                    Else
                        ResetComputationsFrm()
                    End If
                End If
                'ttt case edit 
                If (Not f Is Nothing) AndAlso (Not f.Name Is Nothing) Then
                    Inquiry() 'show grid
                End If


                '-----------------------------------
                Session("bNewFormula") = True
            End If
            'Bilat*** END ***

            'jrs 2006/02/22
            If Session("sOriginForm") = "frm_LongVarS" And Session("bComputation") Then
                idx = Session("idx")
            End If
        Else
            idx = Session("idx")
        End If
    End Sub

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
    'CONVERSION_NOTE: Class 'AxMSDBGrid.AxDBGrid' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1003.html'.
    'Protected WithEvents grdvars As AxMSDBGrid.AxDBGrid 
    'CONVERSION_NOTE: Following code was added for support purposes.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1016.html'.
    Protected WithEvents ToolTip1 As System.Web.UI.WebControls.Label

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_computationsPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_computations
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_computations()
    'm_InitializingDefInstance = False
    'End If 
    'DefInstance = m_vb6FormDefInstance
    'End Get 
    'Set 
    'm_vb6FormDefInstance = Value
    'End Set 
    'End Property 
    '#End Region  
    '**************************************************************
    '   Jose Roberto Solorzano
    '   September 2001
    '**************************************************************

    Dim f As Formula
    'jrs 2005/12/19 RAM for Formulas Dim rs As DataTable
    'Array of Array to store the numeric values from plot
    Dim data() As Object
    'If we use the dimension All, we have to use the same number of
    'dimensions for every variable. JRS 9/19/01
    Dim dataAll() As Object
    Dim dimAll() As String
    'Standarization variables
    Dim idx As Object
    Dim idxDataAll As Short
    'To edit the formula
    Dim bEdit As Boolean
    'To work in frm_LongVarS, indicating which variable is beeing computed
    Dim iVarComp As Short
    Public bCompVisible As Boolean
    'jrs 2006/06/28
    'Public bHistAnal As Boolean

    Public Sub cmdExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdExit.Click
        'jrs 2014/04/04
        If Session("buseAllHis") AndAlso Session("sOriginForm") = "frm_VarSel" Then
            If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                Session("FinYearHis")(Session("COUNTE") + 1) = Session("iMinLstYrUseAllHistory")
                ReLoadDataAll(Session("PLOTCompF"), Session("iMinLstYrUseAllHistory") - Session("iFirstYearH") + 2, Session("iMaxLstYrUseAllHistory") - Session("iFirstYearH") + 1, Session("bCompAll"), dataAll, f, data)
                Session("data") = data
                Session("dataAll") = dataAll
            End If
        End If

        'jrs 2008/05/22
        f.UseNullsAsZeros = chkNullsAsZeros.Checked

        'jrs 2005/06/17
        'If Not grdvars.Visible Or grdvars.Items.Count = 0 Then ShowOriginForm()

        Dim d1 As Short
        Dim start As Short
        Dim X As Short
        Dim CT As Object
        Dim V As Short
        'jrs 2005/12/19 RAM for Formulas Dim rsVP As DataTable
        'jrs 2005/12/19 RAM for Formulas Dim rsV As DataTable
        'jrs 10/24/04 If txtFormula.Enabled Then
        On Error GoTo CompHandler
        txtFormula.Enabled = True
        grdVars.Visible = False

        'jrs 2005/12/19 RAM for Formulas SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT COUNT(*) AS CT FROM [FORMULAS] WHERE [CODE]=" & idx & "AND Varname IS NULL", "", rsVP)
        'jrs 2005/12/19 RAM for Formulas SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT COUNT(*) AS V FROM [FORMULAS] WHERE [CODE]=" & idx, "", rsV)
        'CT = rsVP.Rows(0).Item("CT")
        'V = rsV.Rows(0).Item("V")
        'jrs 2005/12/19 RAM for Formulas
        If f.disVar Is Nothing Then
            V = 0
            CT = 0
        Else
            V = f.disVar.disSize
            Dim vv As Integer
            CT = 0
            For vv = 1 To V
                If f.disVar.displayDet(vv) = "" Then
                    CT = CT + 1
                End If
            Next vv
        End If

        If lblFormulaName.Text <> "Formula Name?" AndAlso CT = 0 AndAlso V > 0 Then
            'JRS 2006/01/19 VALIDATE DIMENSIONS
            ValidateDims(f)
            'JRS 10/9/01 COMPUTATIONS Check for the correct moment to load data
            If Session("sOriginForm") = "frm_LongVarS" AndAlso Me.Visible = True Then
                f.Name = lblFormulaName.Text
                f.Dimension = lblDimensionality.Text
                f.Unit = lblUnits.Text
                AddIdx()
                'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                If idx > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(idx)
                End If
                'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                Session("aFormulas")(idx) = f
                FormatFormAfterLoad()
                'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
                'Me.Hide() 
                Session("bComputation") = False
                SetVarComp()
                f = Nothing
                Session("f") = f
                ShowOriginForm()
                Exit Sub
            ElseIf Session("sOriginForm") = "frm_BilateralInteractionDisplay" AndAlso Me.Visible = True Then
                'like above
                'ttt
                f.Name = lblFormulaName.Text
                f.Dimension = lblDimensionality.Text
                f.Unit = lblUnits.Text
                AddIdx()

                If idx > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(idx)
                End If

                Session("aFormulas")(idx) = f
                FormatFormAfterLoad()

                Session("bComputation") = False
                f = Nothing
                Session("f") = f
                Session("BL_Exit_From_Computation") = True
                ShowOriginForm()
                Exit Sub
            End If
            'Sets the maximum number of data per variable
            PrepareTotVar()
            d1 = f.begAll
            Do While True
                If Session("bCompAll") Then
                    'This has to pass elements from dataAll to data
                    PrepareData((d1))
                End If
                'Loads numerical data into an internal array
                'but still without specifing to which node it belongs
                f.LoadNumData(data)
                AddIdx()
                d1 = d1 + 1
                'Computes formula and load result into its destination
                LoadData()
                FormatFormAfterLoad()
                'jrs 2015/01/21 If d1 > f.endAll Then
                If d1 > f.MaxendAll Then
                    Session("bCompAll") = False
                    Session("iDimAll") = 0
                End If
                If Not Session("bCompAll") Then Exit Do
            Loop
            'JRS 11/2/03
            If UBound(Session("aFormulas")) = 1 And (idx = NR Or idx = GCount) Then 'jrs 2006/02/02
                start = 1
            Else
                start = idx
            End If
            If idx > UBound(Session("aFormulas")) Then
                ReDim Preserve Session("aFormulas")(idx)
            End If
            f.Name = lblFormulaName.Text
            f.Dimension = lblDimensionality.Text
            f.Unit = lblUnits.Text
            'JRS 11/2/03
            'jrs 2006/04/12
            If Not Session("gHistoricAnalog") Then
                For X = start To idx
                    Session("aFormulas")(X) = f
                Next X
            End If
        End If
        'jrs 2006/04/12
        If Not Session("sOriginForm") = "Global" Then
            Session("bComputation") = False
            'ElseIf bHistAnal Then 'jrs 2006/06/28
            '    Dim i As Integer
            '    If Session("bCompAll") Then
            '        For i = 1 To LastYearHistory - FirstYearHistory 'jrs 2006/06/11 + 1
            '            Session("PLOTAll")(i, Session("gidxComp")) = Session("PLOT")(i, Session("gidxComp"))
            '        Next i
            '    Else
            '        For i = 1 To LastYearHistory - FirstYearHistory 'jrs 2006/06/11 + 1
            '            Session("PLOTComp")(i) = Session("PLOT")(i, Session("gidxComp"))
            '        Next i
            '    End If
        End If

        f = Nothing
        Session("f") = f

        cmdEdit.Enabled = False

        'JRS 10/17/01 COMPUTATIONS Check for the correct moment to load data
        If Session("sOriginForm") = "frm_LongVarS" Then
            If Not (CT = 0 And V > 0) Or txtFormula.Text = "" Then
                'jrs 10/25/04
                If CT <> V Then
                    Session("LV_lbl_Dep") = "NONE"
                End If
                'frm_computations.DefInstance.Hide() 

                'jrs 2008/05/19 check if no formulas being used
                If CT = 0 And V = 0 Then
                    Dim bStillFormulas As Boolean
                    If UBound(Session("aFormulas")) > 1 Then
                        If Not Session("aFormulas")(2) Is Nothing Then
                            If Session("aFormulas")(2).Name = Session("LV_lbl_Ind") Then
                                bStillFormulas = True
                            End If
                        End If
                    End If
                    If Not bStillFormulas Then
                        If UBound(Session("aFormulas")) > 0 Then
                            If Not Session("aFormulas")(1) Is Nothing Then
                                If Session("aFormulas")(1).Name = Session("LV_lbl_Dep") Then
                                    bStillFormulas = True
                                End If
                            End If
                        End If
                    End If
                    If Not bStillFormulas Then
                        Session("bComputationActive") = False
                    End If
                End If

                ShowOriginForm()
            End If
            Exit Sub
        End If

        'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.Hide' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Control.Hide().html'.
        'frm_computations.DefInstance.Hide() 
        If bCompVisible Then
            ShowOriginForm()
        End If

        'jrs 10/24/04 
        'Else
        '    txtFormula.Enabled = True
        '    grdvars.Visible = False
        'End If

        Exit Sub

CompHandler:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Sub cmdSelect_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmdSelect.Click

        'jrs 11/17/04
        On Error GoTo CompHandler
        Dim i As Short
        If txtFormula.Enabled Then

            If lblFormulaName.Text = "Formula Name?" Then
                'JRS 06/17/03
                If txtFormula.Text = "" Then
                    LoadMessage("Please type a formula.", Session("Message"))
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Message.aspx")

                End If
                'CONVERSION_NOTE: Method 'System.Windows.Forms.Form.ShowDialog' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.ShowDialog().html'.
                Panel1.Visible = True
                bEdit = False
                WinFormsToWeb_FocusControl.Focus("txtFormulaName")
                'jrs 2013/02/22
                txtFormulaName.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdOk.ClientID + "')")
                txtDimensionality.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdOk.ClientID + "')")
                txtUnits.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdOk.ClientID + "')")
            Else
                bEdit = True '(Session("sOriginForm") <> "frm_VarSel")
                'jrs 2005/12/19 RAM for Formulas
                Dim f_Vars(f.disVar.disSize) As String, f_VarsDet(f.disVar.disSize) As String,
                f_VarsDet1(f.disVar.disSize) As String, f_VarsDet2(f.disVar.disSize) As String,
                f_VarsDet3(f.disVar.disSize) As String
                Dim ii As Integer
                For ii = 1 To f.disVar.disSize
                    f_Vars(ii) = f.disVar.display(ii)
                    f_VarsDet(ii) = f.disVar.displayDet(ii)
                    f_VarsDet1(ii) = f.disVar.displayDet1(ii)
                    f_VarsDet2(ii) = f.disVar.displayDet2(ii)
                    f_VarsDet3(ii) = f.disVar.displayDet3(ii)
                Next ii
                'jrs 2005/06/17
                cmdEdit.Enabled = True
                'f is beeing initialize in the form Activate method
                'be careful if your are debuging and don't Activate the form
                f.init((txtFormula.Text))
                Session("f") = f

                If Session("sOriginForm") = "frm_LongVarS" Then
                    'JRS 11/26/02 idx = idx - 1
                    If Session("LV_Chk_Indep") = 1 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        idx = 0
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        idx = idx - 1
                    End If
                    Session("idx") = idx
                    'ElseIf Session("sOriginForm") = "frm_VarSel" Then
                    '    idx = frm_VarSel.grd_VariableDisplay.Col
                End If
                ResizeData(f_Vars, f_VarsDet, f_VarsDet1, f_VarsDet2, f_VarsDet3) 'jrs 2005/12/19 RAM for Formulas

                'jrs 2014/04/04
                If Session("buseAllHis") AndAlso Session("sOriginForm") = "frm_VarSel" Then
                    'jrs 2014/03/31
                    If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
                        ReDim Session("PLOTCompF")(f.disVar.disSize, MaxYears)
                        Session("iMinLstYrUseAllHistory") = 5000
                    End If
                End If

                grdVars.Visible = True 'jrs 2005/12/19 grdvars.Visible = True
                Inquiry()
                txtFormula.Enabled = False
                WinFormsToWeb_FocusControl.Focus("grdvars")
            End If


        Else
            'JRS 06/20/04 VERIFY IF ITEM SELECTED
            If Session("ComputationsSelectedIndex") < 0 Then
                'jrs 2005/12/21 RAM for formulas
                If grdVars.ActiveRow < 0 Then
                    Exit Sub
                Else
                    Session("ComputationsSelectedIndex") = grdVars.ActiveRow
                End If
            End If
            Session("bComputation") = True
            'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            Session("OLDIdx") = idx
            'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            f.begAll = idx + 1

            f.Name = lblFormulaName.Text
            f.Dimension = lblDimensionality.Text
            f.Unit = lblUnits.Text

            Session("f") = f
            'jrs 2006/01/04
            Session("ComptxtFormula") = txtFormula.Text
            Session("ComplblFormulaName") = lblFormulaName.Text
            Session("ComplblDimensionality") = lblDimensionality.Text
            Session("ComplblUnits") = lblUnits.Text
            'jrs 2008/05/23
            Session("CompchkUseNullsAsZeros") = chkNullsAsZeros.Checked
            'Calls the form to select the variable
            SelectVar()

        End If
        'jrs 11/17/04
        Exit Sub

CompHandler:
        LoadMessage(Err.Description, Session("Message"))
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_Message.aspx")

    End Sub

    Private Sub ContinueSelect()
        'jrs 2005/08/18
        If Session("sOriginForm") = "frm_VarSel" Then
            If f.Dimension = "" And (Not CBool(Session("bCompAll")) Or Session("iDimAll") <> 1) Then 'jrs 2006/09/15
                f.Dimension = Session("GR1N")(idx)
                lblDimensionality.Text = f.Dimension
            End If
            If f.Dimension2 = "" And (Not CBool(Session("bCompAll")) Or Session("iDimAll") <> 2) Then 'jrs 2006/09/15
                f.Dimension2 = Session("GR2N")(idx)
            End If
            If f.Dimension3 = "" And (Not CBool(Session("bCompAll")) Or Session("iDimAll") <> 3) Then 'jrs 2006/09/15
                f.Dimension3 = Session("GR3N")(idx)
            End If
        End If

        Dim j As Short
        'tt3======================
        If Session("sOriginForm") = "frm_BilateralInteractionDisplay" AndAlso Session("BL_lbl_Var") <> "" Then
            If idx Is Nothing Then idx = 0

            j = Session("ComputationsSelectedIndex") + 1 'jrs 2005/12/19 RAM for Formulas grdvars.SelectedIndex + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            f = Session("f")
            f.endAll = idx

            txtFormula.Text = f.Formula_Renamed
            txtFormula.Enabled = False
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            lblFormulaName.Text = f.Name
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            lblDimensionality.Text = f.Dimension
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            lblUnits.Text = f.Unit

            If Session("bCompAll") Then
                dataAll(j) = VB6.Support.CopyArray(Session("PLOTAll"))
                Session("dataAll") = dataAll
                'Loads the names of the different dimensions
                If (j = 1) Then LoadDim()
            Else
                data(j) = VB6.Support.CopyArray(Session("PLOTComp"))
                Session("data") = data
            End If

            Dim CurrIdx As Integer
            CurrIdx = idx
            idx = Session("OLDIdx")
            Inquiry()
            idx = CurrIdx
            UpdateVarname(Session("OLDIdx"))

            DeleteVar()
            UpdateIdx()
            Inquiry()
            Exit Sub
        End If


        '==================END tt3=========
        'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If idx <> Session("OLDIdx") Then
            j = Session("ComputationsSelectedIndex") + 1 'jrs 2005/12/19 RAM for Formulas grdvars.SelectedIndex + 1
            'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            f = Session("f")
            f.endAll = idx

            txtFormula.Text = f.Formula_Renamed
            txtFormula.Enabled = False
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            lblFormulaName.Text = f.Name
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            lblDimensionality.Text = f.Dimension
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.'JRSVBNET System.Windows.Forms.Form.DefInstance.html'.
            lblUnits.Text = f.Unit

            If Session("bCompAll") Then
                dataAll(j) = VB6.Support.CopyArray(Session("PLOTAll"))
                Session("dataAll") = dataAll
                'Loads the names of the different dimensions
                If (j = 1) Then LoadDim()
            Else
                data(j) = VB6.Support.CopyArray(Session("PLOTComp"))
                Session("data") = data
            End If

            Dim CurrIdx As Integer
            CurrIdx = idx
            idx = Session("OLDIdx")
            Inquiry()
            idx = CurrIdx
            UpdateVarname(Session("OLDIdx"))

            DeleteVar()
            UpdateIdx()
            Inquiry()
        ElseIf Not f.disVar Is Nothing Then 'jrs 2006/01/04
            txtFormula.Enabled = False
            Inquiry()
        End If
    End Sub


    'UPGRADE_WARNING: Form event frm_computations.Activate has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.Form.Activated' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.Activated.html'.
    Private Sub frm_computations_Activated(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        RefreshForm()
    End Sub

    'CONVERSION_NOTE: The code of Event 'frm_computations.frm_computations_Load' was moved to Method 'frm_computations.Page_Init'  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1006.html'.
    Private Sub frm_computations_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        bCompVisible = True
        RefreshForm()
        If Not Page.IsPostBack Then
            If Not IsNothing(Session("ComputationsSelectedIndex")) Then
                grdVars.ActiveRow = Session("ComputationsSelectedIndex")
            End If
            If Session("bComputation") Then
                ContinueSelect()
                'jrs 2005/12/21 RAM for Formulas
                Session("ComputationsSelectedIndex") = grdVars.ActiveRow
            End If
        End If
        'jrs 2013/09/03
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
        ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)

    End Sub


    '==========================frm_AddDeleteRawData========================
    Private Sub Inquiry()
        '--------------------------------------------------------------

        'jrs 2005/12/20 RAM for Formulas
        'OleDbDataAdapter1.SelectCommand.Parameters(0).Value = idx
        'DsFormula1.Clear()
        'OleDbDataAdapter1.Fill(DsFormula1)
        'grdVars.DataBind()
        Dim rr As Integer
        Dim rEmp As Integer = -1
        With grdVars
            .TotalRows = f.disVar.disSize
            .RowCount = f.disVar.disSize

            For rr = 1 To f.disVar.disSize
                grdVars(0, rr - 1) = f.disVar.display(rr)
                grdVars(1, rr - 1) = f.disVar.displayDet(rr) & " " & f.disVar.displayDet1(rr) & " " & f.disVar.displayDet2(rr) & " " & f.disVar.displayDet3(rr)
                If rEmp = -1 And Trim(grdVars(1, rr - 1)) = "" Then
                    rEmp = rr - 1
                End If
            Next rr
            If rEmp > -1 Then
                .ActiveRow = rEmp
            Else 'jrs 2006/06/27
                If .TotalRows > 0 Then
                    .ActiveRow = 0
                End If
                'jrs 2010/09/06
                WinFormsToWeb_FocusControl.Focus("cmdExit")
            End If
            .Visible = True
        End With
    End Sub

    Private Sub PrepareData(ByRef dim1 As Short)
        'This has to pass elements from dataAll to data
        Dim i As Object
        Dim j As Short
        Dim arrTemp() As Object
        ReDim arrTemp(f.itotYears)
        'jrs 2015/01/20 problems when trying to do DEATHCAT(Eq Guinea, All, Females)/POP(Eq. Guinea)
        Dim DimTmp As Integer

        For j = 1 To f.disVar.disSize()
            'jrs 2015/01/20
            If IsNothing(dataAll(j)(1, dim1)) And Session("FirstYear") = FirstYearForecast Then
                DimTmp = 1
            Else
                DimTmp = dim1
            End If

            For i = 1 To f.itotYears
                arrTemp(i) = dataAll(j)(i, DimTmp) 'jrs 2015/01/21 dim1
            Next i
            data(j) = VB6.Support.CopyArray(arrTemp)
        Next j
        Session("data") = data
    End Sub

    Public Sub LoadDim()
        ReDim Preserve dimAll(f.endAll)
        Dim i As Short
        For i = f.begAll To f.endAll
            'JRS 07/03/03
            If Session("iDimAll") = 1 Then
                dimAll(i) = Session("GR1N")(i)
            Else
                dimAll(i) = Session("GR2N")(i)
            End If
        Next i
        Session("dimAll") = dimAll
    End Sub

    Private Sub ResizeData(ByRef f_Vars() As String, ByRef f_VarsDet() As String,
    ByRef f_VarsDet1() As String, ByRef f_VarsDet2() As String, ByRef f_VarsDet3() As String)
        Dim tmp As String
        'jrs 2005/12/19 RAM for Formulas
        'SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM [Formulas] WHERE [Code] = " & idx & " ORDER BY [CodeVar]", "", rs)
        'If rs.Rows.Count = 0 Then Exit Sub
        Dim i, j As Object
        Dim k As Short
        Dim sIns As String
        Dim sDel As String
        Dim ii As Integer
        ii = 1

        For i = 1 To f.disVar().disSize()
            'jrs 2005/12/19 RAM for Formulas
            If f.disVar().display(i) < f_Vars(ii) Then 'rs.Rows(l).Item("CodeVar") Then
                'jrs 2005/12/19 RAM for Formulas
                'sIns = "INSERT INTO Formulas VALUES(" & idx & ", '" & lblFormulaName.Text & "', '" & f.disVar.display(i) & "', Null)"
                'updateAccessWith1Command(Session("PutFdrvRun") + "IFSVAR.MDB", sIns)

                ''To reorder the recordset after the insertion
                ''and relocate the cursor to its  original position
                'tmp = rs.Rows(l).Item("CodeVar")
                'SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM [Formulas] WHERE [Code] = " & idx & " ORDER BY [CodeVar]", "", rs)
                'Do While (rs.Rows(l).Item("CodeVar") <> tmp)
                '    l = l + 1
                'Loop

                k = UBound(data) + 1
                ReDim Preserve data(k)
                ReDim Preserve dataAll(k)
                For j = k - 1 To i Step -1
                    data(j + 1) = data(j)
                    dataAll(j + 1) = dataAll(j)
                Next j
            ElseIf f.disVar().display(i) > f_Vars(ii) Then 'rs.Rows(l).Item("CodeVar") Then
                'jrs 2005/12/19 RAM for Formulas
                'sDel = "DELETE * FROM [Formulas] WHERE [Code] = " & rs.Rows(l).Item("Code") & " AND [CodeVar] = " & rs.Rows(l).Item("CodeVar")
                'updateAccessWith1Command(Session("PutFdrvRun") + "IFSVAR.MDB", sDel)
                'SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM [Formulas] WHERE [Code] = " & idx & " ORDER BY [CodeVar]", "", rs)
                'l = l + 1
                ii = ii + 1
                k = UBound(data) - 1
                For j = i To k
                    data(j) = data(j + 1)
                    dataAll(j) = dataAll(j + 1)
                Next j
                If ii > UBound(f_Vars) Then 'jrs 2005/12/19 RAM for Formulas If l = rs.Rows.Count Then
                    AppendData((i))
                    i = f.disVar().disSize()
                Else
                    ReDim Preserve data(k)
                    ReDim Preserve dataAll(k)
                    i = i - 1
                End If
            Else
                'jrs 2005/12/17 RAM for Formulas
                f.disVar.setDataDetS(f_Vars(ii), f_VarsDet(ii))
                f.disVar.setDataDet1S(f_Vars(ii), f_VarsDet1(ii))
                f.disVar.setDataDet2S(f_Vars(ii), f_VarsDet2(ii))
                f.disVar.setDataDet3S(f_Vars(ii), f_VarsDet3(ii))

                ii = ii + 1 'jrs 2005/12/19 RAM for Formulas l = l + 1
                'jrs 2005/12/15 RAM for Formulas If l = rs.Rows.Count And i < f.disVar().disSize() Then
                If ii > UBound(f_Vars) And i < f.disVar().disSize() Then
                    AppendData((i + 1))
                    i = f.disVar().disSize()
                End If
            End If
        Next i
        'jrs 2005/12/15 RAM for Formulas
        'Do While (Not l = rs.Rows.Count)
        '    sDel = "DELETE * FROM [Formulas] WHERE [Code] = " & rs.Rows(l).Item("Code") & " AND [CodeVar] = " & rs.Rows(l).Item("CodeVar")
        '    updateAccessWith1Command(Session("PutFdrvRun") + "IFSVAR.MDB", sDel)
        '    SetDataTablefromAccess(Session("PutFdrvRun") & "IFsVar.mdb", "SELECT * FROM [Formulas] WHERE [Code] = " & idx & " ORDER BY [CodeVar]", "", rs)

        '    l = l + 1
        'Loop

        Session("data") = data
        Session("dataAll") = dataAll

    End Sub



    Private Sub AppendData(ByVal iStart As Short)
        Dim i As Object
        Dim k As Short
        Dim sIns As String
        k = f.disVar().disSize()
        'jrs 2005/12/19 RAM for Formulas
        'For i = iStart To k
        '    sIns = "INSERT INTO Formulas VALUES(" & idx & ", '" & lblFormulaName.Text & "', '" & f.disVar.display(i) & "', Null)"
        '    updateAccessWith1Command(Session("PutFdrvRun") + "IFSVAR.MDB", sIns)
        'Next i
        ReDim Preserve data(k)
        ReDim Preserve dataAll(k)
        Session("data") = data
        Session("dataAll") = dataAll

    End Sub

    Public Sub LoadFormula(ByRef X As Formula)
        f = X
        Session("ComputationsDimensionSize") = f.DimensionSize
        'UPGRADE_WARNING: Couldn't resolve default property of object f.RetriveNumData. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        data = f.RetriveNumData
        'jrs 2006/02/01
        ReDim Preserve data(f.disVar.disSize)
        ReDim Preserve dataAll(f.disVar.disSize)
        Session("data") = data
        'jrs 2005/06/20
        Session("f") = f
        'jrs 2008/05/23
        chkNullsAsZeros.Checked = f.UseNullsAsZeros
    End Sub


    Public WriteOnly Property LoadIdxDataAll() As Short
        Set(ByVal Value As Short)
            idxDataAll = Value
        End Set
    End Property

    Public WriteOnly Property LoadDataAll() As Object
        Set(ByVal Value As Object)
            'UPGRADE_WARNING: Couldn't resolve default property of object p. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            'UPGRADE_WARNING: Couldn't resolve default property of object dataAll(idxDataAll). Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
            dataAll(idxDataAll) = Value
            Session("dataAll") = dataAll
        End Set
    End Property

    'jrs 2005/08/18
    Public WriteOnly Property LoadDataVec() As Object
        Set(ByVal j As Object)
            'jrs 2005/08/23 data(j) = Session("PLOTComp")
            data(j) = VB6.Support.CopyArray(Session("PLOTComp"))
            Session("data") = data
        End Set

    End Property

    'jrs 2006/06/28
    Public Sub LoadPlotComp(ByRef x As Object)
        Session("PLOTComp") = VB6.Support.CopyArray(x)
    End Sub

    Public Property VarComp() As Short
        Get
            VarComp = iVarComp
        End Get
        Set(ByVal Value As Short)
            iVarComp = Value
            'jrs 2005/06/06
            Session("VarComp") = Value
        End Set
    End Property


    Public Property Formula() As Formula
        Get
            Formula = f
        End Get
        Set(ByVal Value As Formula)
            f = Value
            'JRS 01/29/04
            txtFormula.Text = f.Formula_Renamed
            lblFormulaName.Text = f.Name
            lblDimensionality.Text = f.Dimension
            lblUnits.Text = f.Unit
        End Set
    End Property

    Public Sub Public_Exit()
        cmdExit_Click(cmdExit, New System.EventArgs)
    End Sub

    '******************************************************************************************
    '******************************************************************************************
    'Stadarization SubPrograms
    Private Sub AddIdx()
        Select Case Session("sOriginForm")
            Case "frm_VarSel"
                Session("COUNTE") = Session("COUNTE") + 1
            Case "frm_LongVarS"
                Session("LongVarSTag") = Str(idx + 1)
            Case "frm_BilateralInteractionDisplay"
                'ttt
                Session("gidxComp") = If(Session("gidxComp"), 0) + 1
        End Select
        UpdateIdx()
    End Sub

    Private Sub LoadData()
        'jrs 2008/01/29 set up first year for Reference operation
        f.FirstYear = Session("FirstYear")

        Dim k As Short

        Dim finCalc As Object
        finCalc = f.RetrArr(f.disRoot)
        Dim NotIdx As Short
        If idx = 1 Then
            NotIdx = 2
        Else
            NotIdx = 1
        End If

        'jrs 2006/02/22
        If Session("sOriginForm") = "frm_ScatVarS" Then
            Select Case True
                Case Session("frm_ScatVarSopt_Independent0")
                    Session("Scat_IndIndex") = 0
                Case Session("frm_ScatVarSopt_Independent1")
                    Session("Scat_IndIndex") = 1
                Case Session("frm_ScatVarSopt_Independent2")
                    Session("Scat_IndIndex") = 2
                Case Session("frm_ScatVarSopt_Independent3")
                    Session("Scat_IndIndex") = 3
                Case Session("frm_ScatVarSopt_Independent4")
                    Session("Scat_IndIndex") = 4
            End Select
        End If

        For k = 1 To f.itotYears
            Select Case Session("sOriginForm")
                Case "frm_VarSel", "Global" 'jrs 2006/04/12
                    Session("PLOT")(k, idx) = finCalc(k)
                Case "frm_ScatVarS"
                    Select Case True
                        Case Session("frm_ScatVarSopt_DependentChecked")
                            Session("CVarD")(k) = finCalc(k)
                            Session("VarNameD") = lblFormulaName.Text
                        Case Session("frm_ScatVarSopt_Independent0"), Session("frm_ScatVarSopt_Independent1"), Session("frm_ScatVarSopt_Independent2"), Session("frm_ScatVarSopt_Independent3"), Session("frm_ScatVarSopt_Independent4")
                            Session("CVarI")(k) = finCalc(k)
                            Session("VarNameI")(Session("Scat_IndIndex")) = lblFormulaName.Text
                        Case Session("frm_ScatVarSopt_Control")
                            Session("CVarC")(k) = finCalc(k)
                            Session("VarNameC") = lblFormulaName.Text
                    End Select
                    ' Commented out because the type does not or no longer exists
                    ' gvdb 2024-03-06
                    'Dim frm_Sc As New frm_ScatVarS
                    'frm_Sc.FillLocalData()
                    'jrs 2024/05/13 still need it, so I moved the logic here
                    FillLocalData()
                Case "frm_LongVarS"
                    If Session("LV_Chk_Points") = 1 Then
                        If Session("LongVarSopt_Independent") Then
                            'JRS 11/19/02 PLOT(1, k) = finCalc(k)
                            'jrs 2005/06/23 frm_LongVarS.LoadPlotLong(1, k, finCalc(k))
                            Session("PlotLong")(1, k) = finCalc(k)
                        Else
                            'JRS 11/19/02 PLOT(1, k + f.itotYears) = finCalc(k)
                            'jrs 2005/06/23 frm_LongVarS.LoadPlotLong(1, k + f.itotYears, finCalc(k))
                            Session("PlotLong")(1, k + f.itotYears) = finCalc(k)
                        End If
                    Else
                        'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongVarS.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                        If Session("LV_Chk_Indep") = 1 Then
                            'JRS 11/19/02 PLOT(k, idx) = finCalc(k)
                            'jrs 2005/06/23 frm_LongVarS.LoadPlotLong(k, idx, finCalc(k))
                            Session("PlotLong")(k, idx) = finCalc(k)
                        Else
                            'JRS 11/19/02 PLOT(k, NotIdx) = finCalc(k)
                            'jrs 2005/06/23 frm_LongVarS.LoadPlotLong(k, NotIdx, finCalc(k))
                            Session("PlotLong")(k, NotIdx) = finCalc(k)
                        End If
                    End If
            End Select

        Next k

    End Sub

    'jrs 2024/05/13
    Public Sub FillLocalData()
        Dim ICount As Short

        'JAM 2010-03-18
        ReDim Session("SCVarIGlob")(Session("CCount"), 4) 'jrs 2012/07/20
        ReDim Session("SCVarDGlob")(Session("CCount"))


        'JRS 9/16/01 COMPUTATIONS
        For ICount = 1 To Session("CCount")
            If Session("Scat_IndIndex") = 1 Then
                Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex")) = Session("CVarISec")(ICount)
            Else
                Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex")) = Session("CVarI")(ICount)
            End If
            Session("Scat_SCVarD")(ICount) = Session("CVarD")(ICount)

            If Session("IsaGlist") Then

                'jrs 2012/07/20 Session("SCVarIGlob")(ICount, Session("Scat_IndIndex")) = Session("SCVarI")(ICount, Session("Scat_IndIndex"))
                Session("SCVarIGlob")(ICount, Session("Scat_IndIndex")) = Session("Scat_SCVarI")(ICount, Session("Scat_IndIndex"))
                'jrs 2012/07/20 Session("SCVarDGlob")(ICount) = Session("SCVarD")(ICount)
                Session("SCVarDGlob")(ICount) = Session("Scat_SCVarD")(ICount)
            End If

        Next ICount
    End Sub


    Public Sub UpdateIdx()
        Select Case Session("sOriginForm")
            Case "Global" 'jrs 2006/04/12
                idx = Session("gidxComp")
            Case "frm_VarSel"
                idx = Session("COUNTE")
            Case "frm_ScatVarS"
                Select Case True
                    Case Session("frm_ScatVarSopt_DependentChecked")
                        idx = 1
                    Case Session("frm_ScatVarSopt_Independent0")
                        idx = 2
                    Case Session("frm_ScatVarSopt_Independent1")
                        idx = 3
                    Case Session("frm_ScatVarSopt_Independent2")
                        idx = 4
                    Case Session("frm_ScatVarSopt_Independent3")
                        idx = 5
                    Case Session("frm_ScatVarSopt_Independent4")
                        idx = 6
                    Case Session("frm_ScatVarSopt_Control")
                        idx = 7
                    Case Else
                        idx = 0
                End Select
            Case "frm_LongVarS"
                If Session("LongVarScmb_Varname") > "" Then
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_LongVarS.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    Select Case True
                        Case Session("LongVarSopt_Dependent")
                            idx = idx + 1 'Number of Contries already selected
                                'Needed idx + 1 to store the name of the variable
                        Case Session("LongVarSopt_Independent")
                            idx = idx + 1 'Number of Contries already selected
                            'Needed idx + 1 to store the name of the variable
                    End Select
                Else
                    If Session("LongVarSTag") = "" Then
                        If Session("LongVarSopt_Dependent") Then
                            idx = 0
                        Else
                            idx = 1
                        End If
                    Else
                        idx = Int(CDbl(Session("LongVarSTag")))
                    End If
                End If
            Case "frm_BilateralInteractionDisplay"
                idx = Session("gidxComp")
        End Select
        Session("idx") = idx
    End Sub

    Private Sub FormatFormAfterLoad()
        Select Case Session("sOriginForm")
            Case "frm_VarSel", "Global"
                'jrs 2006/07/03
                If Not Session("gHistoricAnalog") Then
                    'JRS 2/13/03
                    'jrs 2005/08/18
                    If Session("FileNum") >= 0 Then
                        Session("VARN")(0, idx) = lblFormulaName.Text & "[" & LTrim$(CStr(Session("ShortSce")(Session("FileNum")))) & "]"
                    Else
                        Session("VARN")(0, idx) = lblFormulaName.Text & "[Historic]"
                    End If
                    Session("VARN")(1, idx) = lblFormulaName.Text

                    If Session("bCompAll") And Session("iDimAll") = 1 Then 'jrs 2006/09/20
                        Session("GR1N")(idx) = dimAll(idx)
                    Else
                        Session("GR1N")(idx) = lblDimensionality.Text
                    End If

                    If Session("bCompAll") And Session("iDimAll") = 2 Then 'jrs 2006/09/15
                        Session("GR2N")(idx) = dimAll(idx)
                    Else
                        'jrs 2005/07/18
                        Session("GR2N")(idx) = f.Dimension2
                    End If

                    If Session("bCompAll") And Session("iDimAll") = 3 Then 'jrs 2006/09/15
                        Session("GR3N")(idx) = dimAll(idx)
                    Else
                        'jrs 2006/06/27
                        Session("GR3N")(idx) = f.Dimension3
                    End If

                    'jrs 2005/07/14
                    Session("GR4N")(idx) = lblUnits.Text
                    'jrs 10/27/04 Flexible Dimension Names for Graphs
                    Session("FDNG1")(idx) = Session("GR1N")(idx)
                    Session("FDNG2")(idx) = Session("GR2N")(idx)
                    Session("FDNG3")(idx) = Session("GR3N")(idx)
                    Session("FDNG4")(idx) = Session("GR4N")(idx)

                End If
                'jrs 2006/09/27
                If Session("buseAllHis") Then
                    Session("MaxFinYearHis") = 0
                End If

            Case "frm_ScatVarS"
                Session("frm_ScatVarSCtrlsLd") = True
                Select Case True
                    Case Session("frm_ScatVarSopt_Independent0")
                        Session("frm_ScatVarSlbl_Ind0") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_Independent1") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent0") = False
                    Case Session("frm_ScatVarSopt_Independent1")
                        Session("frm_ScatVarSlbl_Ind1") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_Independent2") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent1") = False
                    Case Session("frm_ScatVarSopt_Independent2")
                        Session("frm_ScatVarSlbl_Ind2") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_Independent3") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent2") = False
                    Case Session("frm_ScatVarSopt_Independent3")
                        Session("frm_ScatVarSlbl_Ind3") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_Independent4") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent3") = False
                    Case Session("frm_ScatVarSopt_Independent4")
                        Session("frm_ScatVarSlbl_Ind4") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_Control") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent4") = False
                    Case Session("frm_ScatVarSopt_Control")
                        Session("frm_ScatVarSlbl_Cont") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_DependentChecked") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Control") = False
                    Case Else
                        Session("frm_ScatVarSlbl_Dep") = lblFormulaName.Text
                        Session("frm_ScatVarSopt_Independent0") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_DependentChecked") = False
                End Select

            Case "frm_LongVarS"
                If Session("LongVarSopt_Independent") Then
                    Session("LV_lbl_Ind") = f.Name
                Else
                    Session("LV_lbl_Dep") = lblFormulaName.Text
                End If
                If Me.Visible Then
                    If Session("LongVarSopt_Independent") Then
                        Session("LongVarSTag") = "1"
                        Session("LongVarSopt_Dependent") = True
                        'jrs 2006/02/23
                        Session("LongVarSopt_Independent") = False
                    Else
                        Session("LongVarSTag") = "0"
                    End If
                    UpdateIdx()
                End If

            Case "frm_BilateralInteractionDisplay" 'jrs 2016/07/08
                'tt3--------------
                Dim curlst = DirectCast(Session("curlstSelSeries"), ListBox)
                If curlst.Items.Count = 0 Then
                    curlst.Items.Add(f.Name)
                Else
                    Dim i As Integer, bAdd As Boolean
                    bAdd = True
                    For i = 0 To curlst.Items.Count - 1
                        If curlst.Items(i).Text = f.Name Then
                            bAdd = False
                            Exit For
                        End If
                    Next i
                    If bAdd Then
                        curlst.Items.Add(f.Name)
                    End If
                End If
                UpdateIdx()
                'zz15
                Session("CP_Visiblibly_ForBILAT") = True
                '???

                'vb6
                '        If frm_BilateralInteractionDisplay.lstSelSeries.ListCount = 0 Then
                '            frm_BilateralInteractionDisplay.lstSelSeries.AddItem f.Name
                'Else
                '            Dim i As Integer, bAdd As Boolean
                '            bAdd = True
                '            For i = 0 To frm_BilateralInteractionDisplay.lstSelSeries.ListCount - 1
                '                If frm_BilateralInteractionDisplay.lstSelSeries.List(i) = f.Name Then
                '                    bAdd = False
                '                    Exit For
                '                End If
                '            Next i
                '            If bAdd Then
                '                frm_BilateralInteractionDisplay.lstSelSeries.AddItem f.Name
                '            End If
                'End If
                '        UpdateIdx()

                '        If frm_BilateralInteractionDisplay.sOrigDyadic = "DY" Then
                '            frm_BilateralInteractionDisplay.mnuDyadic_Click
                '        ElseIf frm_BilateralInteractionDisplay.sOrigDyadic = "MA" Then
                '            frm_BilateralInteractionDisplay.mnuMonadicActor_Click
                '        ElseIf frm_BilateralInteractionDisplay.sOrigDyadic = "MP" Then
                '            frm_BilateralInteractionDisplay.mnuMonadicPartner_Click
                '        Else
                '            frm_BilateralInteractionDisplay.mnuDyadic_Click
                '            frm_BilateralInteractionDisplay.mnuShowSavedFormulas_Click
                '        End If
                '        frm_BilateralInteractionDisplay.mnuShowSavedFormulas.Visible = True
                '        frm_BilateralInteractionDisplay.mnuSaveFormula.Visible = True
                '        frm_BilateralInteractionDisplay.mnuRenameFormula.Visible = True
                '        frm_BilateralInteractionDisplay.mnuEditFormula.Visible = True
        End Select

    End Sub

    Private Sub ShowOriginForm()
        'jrs 2006/01/04
        Dim sOrgFrm As String
        sOrgFrm = Session("sOriginForm")
        Session("sOriginForm") = ""

        Select Case sOrgFrm
            Case "frm_VarSel"
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_VarSel.aspx")
                    'frm_VarSel.DefInstance.Show() 
            Case "frm_ScatVarS"
                'jrs 2008/09/02
                If Session("CCount") = Session("TotCountries") Then
                    If UBound(Session("FullaFormulas")) < idx Then
                        ReDim Preserve Session("FullaFormulas")(idx)
                    End If
                    If UBound(Session("aFormulas")) >= idx Then
                        Session("FullaFormulas")(idx) = Session("aFormulas")(idx)
                    End If
                End If
                If idx < 4 Then
                    If UBound(Session("aFormulas")) < idx Then
                        Session("bCompSVtmp")(idx) = False
                    ElseIf Session("aFormulas")(idx) Is Nothing Then
                        Session("bCompSVtmp")(idx) = False
                    End If
                End If

                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "ScatVarS.aspx")
                    'frm_ScatVarS.DefInstance.Show() 
            Case "frm_LongVarS"
                'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
                'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "LongVarS.aspx")
                    'frm_LongVarS.DefInstance.Show() 
            Case "frm_BilateralInteractionDisplay"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")

        End Select
    End Sub

    Private Sub SelectVar()
        Dim OLDIdx As Short
        Select Case Session("sOriginForm")
            Case "frm_VarSel"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_VarSel.aspx")
            Case "frm_BilateralInteractionDisplay"
                Session("BL_SelectVar") = True
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")
            Case "frm_ScatVarS"
                OLDIdx = idx
                'jrs 2006/02/22
                Session("frm_ScatVarSCtrlsLd") = True
                'jrs 2006/01/02
                'frm_ScatVarS.DefInstance.cmdComputations.Visible = False
                'frm_ScatVarS.DefInstance.opt_Dependent.Enabled = False
                'frm_ScatVarS.DefInstance.opt_Independent(0).Enabled = False
                'frm_ScatVarS.DefInstance.opt_Independent(1).Enabled = False
                'frm_ScatVarS.DefInstance.opt_Independent(2).Enabled = False
                'frm_ScatVarS.DefInstance.opt_Independent(3).Enabled = False
                'frm_ScatVarS.DefInstance.opt_Independent(4).Enabled = False
                'frm_ScatVarS.DefInstance.opt_Control.Enabled = False
                'frm_ScatVarS.DefInstance.cmb_Varname.Items(frm_ScatVarS.DefInstance.cmb_Varname.SelectedIndex).Text = ""
                'frm_ScatVarS.DefInstance.cmdComputations.Visible = True
                'frm_ScatVarS.DefInstance.opt_Dependent.Enabled = True
                'frm_ScatVarS.DefInstance.opt_Independent(0).Enabled = True
                'frm_ScatVarS.DefInstance.opt_Independent(1).Enabled = True
                'frm_ScatVarS.DefInstance.opt_Independent(2).Enabled = True
                'frm_ScatVarS.DefInstance.opt_Independent(3).Enabled = True
                'frm_ScatVarS.DefInstance.opt_Independent(4).Enabled = True
                'frm_ScatVarS.DefInstance.opt_Control.Enabled = True
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "ScatVarS.aspx")

            Case "frm_LongVarS"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "LongVarS.aspx")

        End Select
        'CONVERSION_NOTE: Web Form page type expected.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1019.html'.
        'CONVERSION_NOTE: The following code could not be executed.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1020.html'.
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_computations.aspx")
        'frm_computations.DefInstance.Show() 
        'UPGRADE_WARNING: Form event frm_computations.Activated has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        frm_computations_Activated(Me, New System.EventArgs)

    End Sub

    Private Sub UpdateVarname(ByVal code As Integer)
        'jrs 2005/12/19 RAM for Formulas
        Select Case Session("sOriginForm")
            Case "frm_VarSel"
                If Session("bCompAll") Then
                    'JRS 07/03/03
                    If Session("iDimAll") = 1 Then
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("VARN")(1, idx))
                        f.disVar.setDataDet1(Session("ComputationsSelectedIndex") + 1, "ALL")
                        f.disVar.setDataDet2(Session("ComputationsSelectedIndex") + 1, Session("GR2N")(idx))
                        f.disVar.setDataDet3(Session("ComputationsSelectedIndex") + 1, Session("GR3N")(idx))
                    ElseIf Session("iDimAll") = 2 Then
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("VARN")(1, idx))
                        f.disVar.setDataDet1(Session("ComputationsSelectedIndex") + 1, Session("GR1N")(idx))
                        f.disVar.setDataDet2(Session("ComputationsSelectedIndex") + 1, "ALL")
                        f.disVar.setDataDet3(Session("ComputationsSelectedIndex") + 1, Session("GR3N")(idx))
                    Else
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("VARN")(1, idx))
                        f.disVar.setDataDet1(Session("ComputationsSelectedIndex") + 1, Session("GR1N")(idx))
                        f.disVar.setDataDet2(Session("ComputationsSelectedIndex") + 1, Session("GR2N")(idx))
                        f.disVar.setDataDet3(Session("ComputationsSelectedIndex") + 1, Session("GR3N")(idx))
                    End If
                Else
                    f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("VARN")(1, idx))
                    f.disVar.setDataDet1(Session("ComputationsSelectedIndex") + 1, Session("GR1N")(idx))
                    f.disVar.setDataDet2(Session("ComputationsSelectedIndex") + 1, Session("GR2N")(idx))
                    f.disVar.setDataDet3(Session("ComputationsSelectedIndex") + 1, Session("GR3N")(idx))
                End If
            Case "frm_ScatVarS"
                Select Case True
                    Case Session("frm_ScatVarSopt_Independent0")
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Dep"))
                    Case Session("frm_ScatVarSopt_Independent1")
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Ind0"))
                    Case Session("frm_ScatVarSopt_Independent2")
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Ind1"))
                    Case Session("frm_ScatVarSopt_Independent3")
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Ind2"))
                    Case Session("frm_ScatVarSopt_Independent4")
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Ind3"))
                    Case Session("frm_ScatVarSopt_Control")
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Ind4"))
                    Case Else
                        f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("frm_ScatVarSlbl_Cont"))
                End Select
            Case "frm_LongVarS"
                If Session("LV_Chk_Indep") = 0 And Session("LongVarSopt_Dependent") And Session("bLastIndep") Then
                    f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("LV_lbl_Ind"))
                Else
                    f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("LV_lbl_Dep"))

                End If
            Case "frm_BilateralInteractionDisplay"
                f.disVar.setDataDet(Session("ComputationsSelectedIndex") + 1, Session("BL_lbl_Var"))
        End Select
    End Sub

    Public Sub DeleteVar()
        Dim i As Short
        Select Case Session("sOriginForm")
            Case "frm_VarSel"
                For i = f.endAll To f.begAll Step -1
                    'CONVERSION_ISSUE: Property 'IFsGUI.frm_VarSel.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
                    DeleteVar(i)
                Next i
            Case "frm_ScatVarS"

                Select Case True
                    Case Session("frm_ScatVarSopt_Independent0")
                        Session("frm_ScatVarSlbl_Dep") = ""
                        Session("frm_ScatVarSopt_DependentChecked") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent0") = False
                    Case Session("frm_ScatVarSopt_Independent1")
                        Session("frm_ScatVarSlbl_Ind0") = ""
                        Session("frm_ScatVarSopt_Independent0") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent1") = False
                    Case Session("frm_ScatVarSopt_Independent2")
                        Session("frm_ScatVarSlbl_Ind1") = ""
                        Session("frm_ScatVarSopt_Independent1") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent2") = False
                    Case Session("frm_ScatVarSopt_Independent3")
                        Session("frm_ScatVarSlbl_Ind2") = ""
                        Session("frm_ScatVarSopt_Independent2") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent3") = False
                    Case Session("frm_ScatVarSopt_Independent4")
                        Session("frm_ScatVarSlbl_Ind3") = ""
                        Session("frm_ScatVarSopt_Independent3") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Independent4") = False
                    Case Session("frm_ScatVarSopt_Control")
                        Session("frm_ScatVarSlbl_Ind4") = ""
                        Session("frm_ScatVarSopt_Independent4") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_Control") = False
                    Case Else
                        Session("frm_ScatVarSlbl_Cont") = ""
                        Session("frm_ScatVarSopt_Control") = True
                        'jrs 2006/01/03
                        Session("frm_ScatVarSopt_DependentChecked") = False
                End Select
            Case "frm_LongVarS"
                '        frm_LongVarS.DefInstance.cmb_Varname.Items(frm_LongVarS.DefInstance.cmb_Varname.SelectedIndex).Text = ""
                Session("LongVarScmb_Varname") = ""
                If Session("LV_Chk_Indep") = 0 And Session("LongVarSopt_Dependent") And Session("bLastIndep") Then
                    Session("LongVarSopt_Independent") = True
                    'jrs 2006/02/23
                    Session("LongVarSopt_Dependent") = False
                End If
                'jrs 2006/10/24
                If Session("LV_Chk_Indep") = 0 And Not Session("bLastIndep") And Session("LongVarSopt_Independent") Then
                    Session("LongVarSopt_Independent") = False
                    Session("LongVarSopt_Dependent") = True
                End If
        End Select

    End Sub


    Private Sub PrepareTotVar()
        Select Case Session("sOriginForm")
            Case "Global" 'jrs 2006/04/12
                'jrs 2008/07/09 f.itotYears = Session("LastYear") - Session("FirstYear") + 1
                f.itotYears = Session("FinYear")(idx)
            Case "frm_VarSel"
                f.itotYears = Session("FinYear")(idx + 1)
            Case "frm_ScatVarS"
                f.itotYears = 200
            Case "frm_LongVarS"
                If Session("LV_Chk_Indep") = 0 Then
                    f.itotYears = Session("DisYear") * Session("CountryTot")
                Else
                    f.itotYears = Session("DisYear")
                End If
        End Select
    End Sub

    Private Sub SetVarComp()
        Dim bIndepCond As Boolean
        If Session("sOriginForm") = "frm_LongVarS" Then
            bIndepCond = (Session("LV_Chk_Indep") = 0 And Session("LongVarSopt_Dependent") And Session("bLastIndep"))
            If iVarComp = 0 Then
                If bIndepCond Then
                    iVarComp = 2
                Else
                    iVarComp = 1
                End If
            Else
                If (bIndepCond And iVarComp = 1) Or (Not bIndepCond And iVarComp = 2) Then
                    iVarComp = 3
                End If
            End If
        End If
        'jrs 2005/06/16
        Session("VarComp") = iVarComp
    End Sub

    Public Sub RefreshForm()
        If Not (Session("f") Is Nothing) Then
            f = Session("f")
        End If
        If Not (Session("data") Is Nothing) Then
            data = Session("data")
        End If
        If Not (Session("dataAll") Is Nothing) Then
            dataAll = Session("dataAll")
        End If
        If Not (Session("dimAll") Is Nothing) Then
            dimAll = Session("dimAll")
        End If
        'jrs 2005/07/14
        If Not (Session("ComptxtFormula") Is Nothing) Then
            txtFormula.Text = Session("ComptxtFormula")
            Session("ComptxtFormula") = Nothing
        End If
        If Not (Session("ComplblFormulaName") Is Nothing) Then
            lblFormulaName.Text = Session("ComplblFormulaName")
            Session("ComplblFormulaName") = Nothing
        End If
        If Not (Session("ComplblDimensionality") Is Nothing) Then
            lblDimensionality.Text = Session("ComplblDimensionality")
            Session("ComplblDimensionality") = Nothing
        End If
        If Not (Session("ComplblUnits") Is Nothing) Then
            lblUnits.Text = Session("ComplblUnits")
            Session("ComplblUnits") = Nothing
        End If
        If Not (Session("CompLoadFormula") Is Nothing) Then
            LoadFormula(Session("CompLoadFormula"))
            Session("CompLoadFormula") = Nothing
        End If
        'jrs 2006/10/24
        If Not (Session("VarComp") Is Nothing) Then
            iVarComp = Session("VarComp")
        End If
        'jrs 2008/05/23
        If Not (Session("CompchkUseNullsAsZeros") Is Nothing) Then
            chkNullsAsZeros.Checked = Session("CompchkUseNullsAsZeros")
            Session("CompchkUseNullsAsZeros") = Nothing
        End If

        'Subroutine to update index and create a formula
        If (f Is Nothing) Then f = New Formula
        UpdateIdx()
    End Sub

    Private Sub ResetComputationsFrm()
        'JRS 9/25/01 COMPUTATIONS
        txtFormula.Text = ""
        lblFormulaName.Text = "Formula Name?"
        lblDimensionality.Text = "Dimensionality?"
        lblUnits.Text = "Units?"
        WinFormsToWeb_FocusControl.Focus("txtFormula")
        Session("ComputationsDimensionSize") = 0
        Session("ComputationsSelectedIndex") = -1
        'jrs 2013/02/22
        txtFormula.Attributes.Add("onkeypress", "return clickButton(event,'" + cmdSelect.ClientID + "')")
    End Sub

    'jrs 2005/12/20 RAM for Formulas
    'Public Function UpdateFormulaIdx(ByRef idx As Short) As Object
    '    'jrs 2005/10/28
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

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If txtFormulaName.Text > "" Then

            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            lblFormulaName.Text = txtFormulaName.Text
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            lblDimensionality.Text = txtDimensionality.Text
            'CONVERSION_ISSUE: Property 'IFsGUI.frm_computations.DefInstance' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::WinFormsToWeb.System.Windows.Forms.Form.DefInstance.html'.
            lblUnits.Text = txtUnits.Text
            Panel1.Visible = False

            cmdEdit.Enabled = True
            'f is beeing initialize in the form Activate method
            'be careful if your are debuging and don't Activate the form
            f.init((txtFormula.Text))
            Session("f") = f

            If bEdit Then
                If Session("sOriginForm") = "frm_LongVarS" Then
                    'JRS 11/26/02 idx = idx - 1
                    If Session("LV_Chk_Indep") = 1 Then
                        'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        idx = 0
                    Else
                        'UPGRADE_WARNING: Couldn't resolve default property of object idx. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
                        idx = idx - 1
                    End If
                    Session("idx") = idx
                    'ElseIf Session("sOriginForm") = "frm_VarSel" Then
                    '    idx = frm_VarSel.grd_VariableDisplay.Col
                End If
                ResizeData(Nothing, Nothing, Nothing, Nothing, Nothing) 'jrs 2005/12/19 RAM for Formulas
            Else
                ReDim Preserve data(f.disVar.disSize())
                ReDim Preserve dataAll(f.disVar.disSize())
                'jrs 2005/12/19 RAM for Formulas
                'updateAccessWith1Command(Session("PutFdrvRun") + "IFSVAR.MDB", "DELETE * FROM [Formulas] WHERE [Code] = " & idx)
                'Dim sIns As String
                'Dim i As Integer
                'For i = 1 To f.disVar.disSize()
                '    sIns = "INSERT INTO Formulas VALUES(" & idx & ", '" & lblFormulaName.Text & "', '" & f.disVar.display(i) & "', Null)"
                '    'jrs 10/24/04
                '    Dim sCodeVar As String
                '    If Len(f.disVar.display(i)) > 10 Then
                '        sCodeVar = Mid(f.disVar.display(i), 1, 10)
                '        Dim frm_msg As New frm_Message
                '        frm_msg.LoadMessage("Variable Name " & f.disVar.display(i) & " is too long (10 chars. max), we will truncate the name.")
                '        Support.FormManager.StateStore(Me)
                '        Support.FormManager.Show(Me, "frm_Message.aspx")

                '    Else
                '        sCodeVar = f.disVar.display(i)
                '    End If
                '    sIns = "INSERT INTO Formulas VALUES(" & idx & ", '" & lblFormulaName.Text & "', '" & sCodeVar & "', Null)"
                '    updateAccessWith1Command(Session("PutFdrvRun") + "IFSVAR.MDB", sIns)
                'Next i
                Session("data") = data
                Session("dataAll") = dataAll
            End If



            grdVars.Visible = True
            Inquiry()
            'jrs 2006/01/04
            Session("ComputationsSelectedIndex") = grdVars.ActiveRow

            txtFormula.Enabled = False
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            WinFormsToWeb_FocusControl.Focus("grdvars")


        Else
            'CONVERSION_TODO: The equivalent of Method 'Focus()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            WinFormsToWeb_FocusControl.Focus("txtFormulaName")

            LoadMessage("You must type a Formula Name", Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

        End If
    End Sub


    Public Sub DeleteVar(ByRef curr_col As Short)
        Dim iloop As Short
        Dim ICount As Short
        Dim i2 As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'JRS 9/10/01 CREATED FOR COMPUTATIONS
        ' Delete Variable
        Session("COUNTE") = Session("COUNTE") - 1 ''Do later?
        For i2 = 1 To Session("LastYear") - Session("FirstYear") + 1
            For ICount = curr_col To Session("COUNTE")
                Session("PLOT")(i2, ICount) = Session("PLOT")(i2, ICount + 1)
            Next ICount
            Session("PLOT")(i2, Session("COUNTE") + 1) = 0
        Next i2

        'jrs 2015/01/21
        If Session("bCompAll") Then
            ReDim Session("PLOTAll")(MaxYears, MaxVariables)
        End If

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
            'jrs 2005/12/21 RAM for Formulas
            Session("FDNG1")(iloop) = Session("FDNG1")(iloop + 1)
            Session("FDNG2")(iloop) = Session("FDNG2")(iloop + 1)
            Session("FDNG3")(iloop) = Session("FDNG3")(iloop + 1)

            'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, iloop - 1)
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
            'JRSVBNET Support.DataGridManager.SetColWidth(grd_VariableDisplay, iloop - 1, Inch)
            'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("VARN")(1, iloop))
            'grd_VariableDisplay.Col = Session("COUNTE") - 1
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 1)
            'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR1N")(iloop))
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 2)
            'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR2N")(iloop))
            'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 3)
            'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR3N")(iloop))
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
        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, Session("COUNTE")) 'columns starts from zero
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
        'JRSVBNET Support.DataGridManager.SetColWidth(grd_VariableDisplay, Session("COUNTE"), Inch)
        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("VARN")(1, Session("COUNTE") + 1))
        'grd_VariableDisplay.Col = Session("COUNTE") - 1
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 1)
        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR1N")(Session("COUNTE") + 1))
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 2)
        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR2N")(Session("COUNTE") + 1))
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 3)
        'JRSVBNET Support.DataGridManager.SetText(grd_VariableDisplay, Session("GR3N")(Session("COUNTE") + 1))
        'JRSVBNET Support.DataGridManager.manager.SetCol(grd_VariableDisplay, curr_col - 1) 'reposition the cursor
        'grd_VariableDisplay.ColWidth(Curr_Col) = Inch
        'JRSVBNET Support.DataGridManager.manager.SetRow(grd_VariableDisplay, 0)
        'UPGRADE_WARNING: Couldn't resolve default property of object Label1. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'jrs 2005/08/23
        Session("FDNG1")(Session("COUNTE") + 1) = ""
        Session("FDNG2")(Session("COUNTE") + 1) = ""
        Session("FDNG3")(Session("COUNTE") + 1) = ""
    End Sub

    'jrs 2005/08/17
    Public Sub RedimData()
        ReDim Preserve data(f.disVar.disSize())
        ReDim Preserve dataAll(f.disVar.disSize())
    End Sub

    Private Sub grdVars_CellClick(ByVal sender As System.Object, ByVal e As TMSWebControls.ModEventArgs) Handles grdVars.CellClick
        Session("ComputationsSelectedIndex") = e.Row
        If e.Row >= 0 Then
            cmdSelect_Click(sender, e)
        End If
    End Sub

    Private Sub ValidateDims(ByRef f As Formula)
        Dim iii As Integer
        If f.Dimension > "" Then
            For iii = 1 To f.disVar.disSize
                'jrs 2025/12/03 added And f.disVar.displayDet1(iii) <> f.Dimension2 to allow XBILAT(US,China)/GDP(China)
                'jrs 2017/05/15 added And f.disVar.displayDet1(iii) <> "World"
                'jrs 2012/11/09 added And f.disVar.displayDet1(iii) > ""
                If InStr(f.disVar.displayDet1(iii), f.Dimension) = 0 And f.disVar.displayDet1(iii) > "" And
                    f.disVar.displayDet1(iii) <> "World" And f.disVar.displayDet1(iii) <> f.Dimension2 Then
                    f.Dimension = ""
                    lblDimensionality.Text = ""
                    Exit For
                End If
            Next iii
        End If
        If f.Dimension2 > "" Then
            For iii = 1 To f.disVar.disSize
                'jrs 2006/08/21 XS, USA, Energy/GDP, USA * 1000 should keep Energy as sec dimension
                'only remove dim when there's to different values, don't consider blancs
                If f.disVar.displayDet2(iii) > "" Then
                    'jrs 2025/12/10 added and f.Dimension = 0 to allow XBILAT(US,China)+XBILAT(China,US)
                    If InStr(f.disVar.displayDet2(iii), f.Dimension2) = 0 And
                        InStr(f.disVar.displayDet2(iii), f.Dimension) = 0 Then
                        f.Dimension2 = ""
                        Exit For
                    End If
                End If
            Next iii
        End If
        If f.Dimension3 > "" Then 'jrs 2006/05/24 Dim3
            For iii = 1 To f.disVar.disSize
                'jrs 2006/08/21
                'only remove dim when there's to different values, don't consider blancs
                If f.disVar.displayDet3(iii) > "" Then
                    If InStr(f.disVar.displayDet3(iii), f.Dimension3) = 0 Then
                        f.Dimension3 = ""
                        Exit For
                    End If
                End If
            Next iii
        End If
    End Sub

    'jrs 2006/06/28
    Public Sub setgHistoricAnalog(ByVal b As Boolean)
        Session("gHistoricAnalog") = b
    End Sub

    'jrs 2006/06/30
    Public Sub setsOriginForm(ByVal s As String)
        Session("sOriginForm") = s
    End Sub

    Public Sub setgidxComp(ByVal i As Integer)
        Session("gidxComp") = i
    End Sub

    'jrs 2006/07/03
    Public Sub setLastYear(ByVal i As Integer)
        Session("LastYear") = i
    End Sub

    'jrs 2006/07/18
    Public Sub setFileNum(ByVal i As Integer)
        Session("FileNum") = i
    End Sub

    'jrs 2006/07/21
    Public Sub LoadTargetVector(ByRef TargetVector As Object, ByVal iLstYr As Integer)
        Dim i As Integer
        If Session("bCompAll") Then
            For i = 1 To iLstYr - FirstYearHistory 'jrs 2006/06/11 + 1
                Session("PLOTAll")(i, Session("gidxComp")) = Session("PLOT")(i, Session("gidxComp"))
            Next i
        Else
            For i = 1 To iLstYr - FirstYearHistory 'jrs 2006/06/11 + 1
                TargetVector(i) = Session("PLOT")(i, Session("gidxComp"))
            Next i
        End If
    End Sub

    'jrs 2006/07/21
    Dim formBckp As Formula
    'jrs 2006/09/20
    Dim dataBckp() As Object

    Public Sub SaveSessionFormula()
        formBckp = Session("f")
        dataBckp = Session("data")
    End Sub

    Public Sub ResetSessionFormula()
        Session("f") = formBckp
        Session("data") = dataBckp
    End Sub

    Public Sub LoadDataVecT(ByVal j As Object, ByRef T As Object)
        data(j) = VB6.CopyArray(T)
        Session("data") = data
    End Sub

    'jrs 2006/09/21
    Public Sub setbCompAll(ByVal b As Boolean)
        Session("bCompAll") = b
    End Sub

    Protected Sub txtFormula_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFormula.TextChanged
        'jrs 2010/09/06
        'trigger the button
        cmdSelect_Click(sender, e)
    End Sub

    Protected Sub txtFormulaName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFormulaName.TextChanged
        'jrs 2010/09/06
        'trigger the button
        cmdOk_Click(sender, e)
    End Sub
End Class
