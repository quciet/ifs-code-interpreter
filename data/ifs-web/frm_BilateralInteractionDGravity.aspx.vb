Imports System.Data.SQLite
Imports IFs.Data

Partial Class frm_BilateralInteractionDGravity
    Inherits Page

#Region " Web Form Designer Generated Code "
    'JAM 2019-12-26
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    'Dim TotalGraphs As Integer
    'jrs 2009/07/02
    Dim bSuccess As Boolean
    'Dim totYears As Integer
    'Dim bComparisonOn As Boolean
    Dim salphas(1, NSec) As Single
    Private EditMode As Boolean = False

    'Private forCopyValue As String
    'Public Property ForCopy() As String
    '    Get
    '        Return forCopyValue
    '    End Get
    '    Set(ByVal value As String)
    '        forCopyValue = value
    '    End Set
    'End Property
    'Dim Session("CompBackup")(30, 30) As Integer
    'Dim CompLabel(30, 30) As Integer

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        'jrs 2005/04/22
        If Not Page.IsPostBack Then
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
        End If
        Me.EditMode = (Not (Session("GravityParas") Is Nothing))
        'If (Not Request("__EVENTARGUMENT") Is Nothing) AndAlso Request("__EVENTARGUMENT") = "lstSelSeries_dblclick" Then
        '    lstSelSeries_DblClick()
        'End If
        'lstSelSeries.EnableViewState = False
        'lstSelSeries.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(lstSelSeries, "lstSelSeries_dblclick"))
        'lstSelSeries.ClearSelection()
        'jrs 11/08/04
        'jrs 2011/05/04 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
    End Sub


#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Don't Load in EditMode, except first time
        'If Session("bEditGravity") AndAlso Session("GravityParas") Is Nothing Then Return

        'jrs 2005/10/13
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"
        '===More for 2 menus
        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        cswmWebMenu.GeneratedPrefix = "cswmWebMenu"

        'jrs 2012/01/17

        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        cswmWebMenu.MenuBar.AbsoluteDockEnabled = False
        cswmWebMenu.MenuBar.AbsoluteDragEnabled = False
        cswmWebMenu.MenuBar.BorderSize = 1
        cswmWebMenu.MenuBar.OuterHighlightColor = "#ffffff"
        cswmWebMenu.MenuBar.OuterShadowColor = "#808080"
        cswmWebMenu.MenuBar.SelectedColor = "navy"
        cswmWebMenu.MenuBar.SelectedHighlightColor = "navy"
        cswmWebMenu.MenuBar.SelectedShadowColor = "navy"
        cswmWebMenu.MenuBar.SelectedTextColor = "white"
        cswmWebMenu.ButtonFont.PaddingLeft = 10
        cswmWebMenu.ButtonFont.PaddingRight = 10
        cswmWebMenu.ItemFont.PaddingLeft = 4



        '===========
        '==Begin ConText Menu==========
        wmContext.UserData = "jsolorza::976739975"
        AddHandler wmContext.ServerItemClick, AddressOf ItemClickHandler
        wmContext.ClearPixelImage = "blank.gif"
        wmContext.PopupIcon = "./images/Popup.gif"
        wmContext.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmContext.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmContext.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmContext.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmContext.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmContext.ScrollEnabled = True
        wmContext.Visible = True
        Dim cswmGroup0 As Coalesys.WebMenu.Group
        Dim cswmItem0 As Coalesys.WebMenu.Item
        cswmGroup0 = wmContext.Groups.Add()
        cswmGroup0.ID = "Group_0bd4"

        'cswmItem0 = cswmGroup0.Items.Add()
        'cswmItem0.Caption = "Define"
        'cswmItem0.OnClick() = "showDefinition()"

        'cswmItem0 = cswmGroup0.Items.Add()
        'cswmItem0.Caption = "New Item"
        'cswmItem0.Style = Coalesys.WebMenu.StyleConstants.Divider

        'cswmItem0 = cswmGroup0.Items.Add()
        'cswmItem0.Caption = "Block Diagram (Pop-Up)"
        'cswmItem0.OnClick() = "showExplain()"

        'cswmItem0 = cswmGroup0.Items.Add()
        'cswmItem0.Caption = "New Item"
        'cswmItem0.Style = Coalesys.WebMenu.StyleConstants.Divider

        'cswmItem0 = cswmGroup0.Items.Add()
        'cswmItem0.Caption = "Equations (Pop-Up)"
        'cswmItem0.OnClick = "showEquation()"

        'cswmItem0 = cswmGroup0.Items.Add()
        'cswmItem0.Caption = "New Item"
        'cswmItem0.Style = Coalesys.WebMenu.StyleConstants.Divider

        '==End ConText Menu==========
        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item
        cswmGroup1 = cswmWebMenu.Groups.Add() '==
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add() '=========
        cswmGroup1.Caption = "Save Options"
        cswmGroup1.ID = "mnuSaveOpt"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save and Continue"
        cswmItem.ID = "mnuSaveContinue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save"
        cswmItem.ID = "mnuSave"
        cswmGroup1 = cswmWebMenu.Groups.Add() '=================
        cswmGroup1.Caption = "Transformation Rules"
        cswmGroup1.ID = "mnuTrans"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Linear"
        cswmItem.ID = "mnuTransLinear"
        cswmItem0 = cswmGroup0.Items.Add()
        cswmItem0.Caption = "Linear"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Natural Logarithm"
        cswmItem.ID = "mnuTransLog"
        cswmItem0 = cswmGroup0.Items.Add()
        cswmItem0.Caption = "Natural Logarithm"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Base 10 Logarithm"
        cswmItem.ID = "mnuTransLogBase10"
        cswmItem0 = cswmGroup0.Items.Add()
        cswmItem0.Caption = "Base 10 Logarithm"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Exponential"
        cswmItem.ID = "mnuTransExp"
        cswmItem0 = cswmGroup0.Items.Add()
        cswmItem0.Caption = "Exponential"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Logistic"
        cswmItem.ID = "mnuTransLogistic"
        cswmItem0 = cswmGroup0.Items.Add()
        cswmItem0.Caption = "Logistic"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Manual"
        'cswmItem.ID = "mnuTransManual"
        cswmGroup1 = cswmWebMenu.Groups.Add() '=================
        cswmGroup1.Caption = "Equation Options"
        cswmGroup1.ID = "mnuEqu"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Update Equation"
        cswmItem.ID = "mnuUpdateEquation"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Add More Variables"
        cswmItem.ID = "mnuAddMoreVars"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Add Adjustment Using Data"
        cswmItem.ID = "mnuAddAdjData"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Change Adjustment Year(2015)"
        cswmItem.ID = "mnuChgAdjYr"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Create Residual formula from Gravity variable"
        cswmItem.ID = "mnuCreateResiduals"
        '===
        If Session("GravityParas") Is Nothing Then
            ResetGrids()
        Else
            EditMode = True
        End If

        If Not Page.IsPostBack Then

            'GridRefresh() =>
            'Click by Code
            If Session("GravityParas") Is Nothing Then
                Dim c As IPostBackEventHandler = DirectCast(Me.cmdRenderList, IPostBackEventHandler)
                c.RaisePostBackEvent(String.Empty)
            End If
            cswmWebMenu.Groups("mnuEqu").Items("mnuAddAdjData").IconImage = "blank.gif"
        Else

        End If

        If Not If(Session("bEditGravity"), False) Then

            LoadWeightFactors()

            LoadTransformationRules()
        Else

        End If

        If Not Session("GravityParas") Is Nothing Then
            Dim paras = DirectCast(Session("GravityParas"), Dictionary(Of String, String))
            Me.txtFormulaName.Text = paras("FormulaName")
            Session("sVarNameGravity") = paras("FormulaName")
            Dim db = Session("PutFdrvRun") & "DataDictDyadic.mdb"
            Dim rs As DataTable = New DataTable
            Dim rsGraDet As DataTable = New DataTable
            SetDataTablefromAccess(db, "SELECT * FROM Gravity  WHERE GravityVarName = """ & paras("FormulaName") & """", "", rs)
            SetDataTablefromAccess(db, "SELECT * FROM GravityDet  WHERE GravityVarName = """ & paras("FormulaName") & """", "", rsGraDet)
            Dim rsDet As DataTable = New DataTable
            Dim sql As String = "SELECT v.Formula, d.Code, d.VarName, v.Units,v.Description FROM FormulaVars as v, FormulaVarsDet as d WHERE v.FormulaName = d.FormulaName AND d.FormulaName = """ & paras("FormulaName") & """"
            SetDataTablefromAccess(db, sql, "", rsDet)
            Dim lstVars As New List(Of String)
            Dim lstCoefs As New List(Of String)
            Dim lstTrans As New List(Of String)
            Dim lstUnit As New List(Of String)
            Dim sUnits As String = ""
            For Each r As DataRow In rsDet.Rows
                Me.txtFormula.Text = DisplayFormula(r.Item("Formula").ToString)
                Me.txtGravityDescription.Text = r.Item("Description")
                sUnits = NoNullStr(r.Item("Units"))
            Next
            For Each r As DataRow In rsGraDet.Rows
                'lstVars.Add(r.Item("VarType") & " " & r.Item("VarName") & " - UNIT1")
                lstVars.Add(r.Item("VarName"))
                lstCoefs.Add(r.Item("Coefficient"))
                lstTrans.Add(r.Item("Transformation"))

            Next
            If sUnits <> "" Then
                lstUnit = sUnits.Split(",").ToList
            End If
            GridRefreshEDIT(lstVars, lstUnit)
            LoadWeightFactorsEDIT(lstCoefs)
            LoadTransformationRulesEDIT(lstTrans)


            For Each r As DataRow In rs.Rows

                '    paras.Add("FormulaName", r.Item("GravityVarName"))
                'Me.txtGravityDescription
            Next

            '.grd_Weight.col = .grd_Weight.cols - 1
            '.grd_Weight.Text = rs.Fields("Coefficient")

            'frm_BilateralInteractionDisplayGravity.grd_Transformation2 = rs.Fields("Transformation")
            'If rs.Fields("AdjustmentVarName") > "" Then
            '    frm_BilateralInteractionDisplayGravity.sAdjustmentVar = rs.Fields("AdjustmentVarName")
            '    frm_BilateralInteractionDisplayGravity.sAdjustmentVarType = rs.Fields("AdjustmentVarType")
            '    frm_BilateralInteractionDisplayGravity.lblAdjVarName.Caption = "Gravity Adjustment Variable Name: " & rs.Fields("AdjustmentVarName")
            '    frm_BilateralInteractionDisplayGravity.lblAdjVarName.Visible = True
            '    frm_BilateralInteractionDisplayGravity.mnuAddAdjData.Checked = True

            'End If
            'frm_BilateralInteractionDisplayGravity.txtFormulaName.Text = rs.Fields("GravityVarName")
            'frm_BilateralInteractionDisplayGravity.txtGravityDescription.Text = sGravityDescription
            'frm_BilateralInteractionDisplayGravity.bAdjustemntResiduals = bAdjRes
            ''End With - With frm_BilateralInteractionDisplayGravity

            ''db.Close

            'frm_BilateralInteractionDisplayGravity.cmdUpdEq_Click
            'bEditGravity = True
            'frm_BilateralInteractionDisplayGravity.bSaved = True

            ''Go to Index Form
            'frm_BilateralInteractionDisplayGravity.Show
            'jrs 2008/10/30

        End If

        '----
        If Session("GravityParas") Is Nothing Then
            UpdateEquation()
        Else
            'Session("GravityParas") = Nothing
        End If


        Session("bExitForm") = True


        Session("iAdjustmentYr") = Session("FirstYear")

        cswmWebMenu.Groups("mnuEqu").Items("mnuChgAdjYr").Caption = "Change Adjustment Year(" & Session("FirstYear") & ")"
        'called from ..Display

        If If(Session("CalledAddGravityAdj"), False) = True Then
            Session("CalledAddGravityAdj") = False
            lblAdjVarName.Text = Session("GravityAdjustmentVariableName")
            lblAdjVarName.Visible = True
            cmdUpdEq_Click(Me, New EventArgs)
        End If
        If If(Session("CalledAddGravityVar"), False) = True Then
            Session("CalledAddGravityVar") = False
            AddGravityVar(Session("sAdjustmentVar"), Session("sAdjustmentVarType"))
        End If



    End Sub
    Private Sub RenderList()

    End Sub


    'jrs 2005/10/13
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                Support.FormManager.StateStore(Me)
                Session("bAddGravity") = False
                Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")
            Case "Save and Continue"
                mnuSaveContinue_Click()

            Case "Save"
                mnuSave_Click()

            Case "Linear"
                mnuTransLinear_Click()
                    'Session("DisplayControlParent") = Request.Url.AbsoluteUri
                    'Support.FormManager.StateStore(Me)
                    'Support.FormManager.Show(Me, "frm_DisplayControl.aspx")
            Case "Natural Logarithm"
                mnuTransLog_Click()
            Case "Base 10 Logarithm"
                mnuTransLogBase10_Click()
            Case "Exponential"
                mnuTransExp_Click()
            Case "Logistic"
                mnuTransLogistic_Click()
                'Case "Manual"
                '    ManualTraChange()
            Case "Update Equation"
                UpdateEquation()
            Case "Add More Variables"
                mnuAddMoreVars_Click()
            Case "Add Adjustment Using Data"
                mnuAddAdjData_Click()
            Case "Create Residual formula from Gravity variable"
                mnuCreateResiduals_Click()
            Case Else
                If objEA.Item.ID = "mnuChgAdjYr" Then
                    mnuChgAdjYr_Click()
                End If
        End Select

    End Sub
    Private Sub lstVariableDisplay_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstVariableDisplay.SelectedIndexChanged
        'ttt Remove Variable
        Dim c As IPostBackEventHandler = DirectCast(Me.cmdRemoveVariable, IPostBackEventHandler)
        c.RaisePostBackEvent(String.Empty)
    End Sub
    Public Sub cmdRemoveVariable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveVariable.Click
        If lstVariableDisplay.SelectedIndex > -1 Then
            'ShowConfirm("Are you sure you want to remove variable")
            Dim ans As Integer
            'ans = MsgBox("Are you sure you want to remove variable " & .text & " from this set?", vbYesNo, "Create Gravity Variable")
            'If ans = vbYes Then
            Dim idx = lstVariableDisplay.SelectedIndex
            lstVariableDisplay.Items.RemoveAt(idx)
            Session("lstVariableDisplayItemsCount") = Session("lstVariableDisplayItemsCount") - 1
            UpdateEquation()
            'End If
        End If
    End Sub
    Private Sub SwitchSeriesType(CurrentItem As String)
        Dim gr = cswmWebMenu.Groups("mnuSerType") ' .Items("mnuDyadic").IconImage = "checkbox.gif"
        gr.Items(CurrentItem).IconImage = "checkbox.gif"
        For Each it As Coalesys.WebMenu.Item In gr.Items
            If it.ID <> CurrentItem Then
                it.IconImage = "blank.gif"
            Else
                it.IconImage = "checkbox.gif"
                Select Case CurrentItem
                    Case "mnuDyadic"
                        Session("OrigDyadic") = "DY"
                    Case "mnuMonadicforActor"
                        Session("OrigDyadic") = "MA"
                    Case "mnuMonadicforPartner"
                        Session("OrigDyadic") = "MP"
                    Case "mnuShowSavedFormulas"
                        Session("OrigDyadic") = "SF"
                    Case "mnuForecast"
                        Session("OrigDyadic") = "FC"
                End Select
            End If
        Next

    End Sub

    Private Sub ResetGrids()
        Dim mCOUNTE As Integer
        Dim items = DirectCast(Session("SelSeriesItems"), ListItemCollection)
        mCOUNTE = items.Count

        If mCOUNTE = 0 Then Exit Sub

        If Session("bFormulas") Is Nothing Then
            Dim b(mCOUNTE) As Boolean
            Session("bFormulas") = b
        Else
            ReDim Preserve Session("bFormulas")(mCOUNTE)
        End If
        If Session("sTypes") Is Nothing Then
            Dim s(mCOUNTE) As String
            Session("sTypes") = s
        Else
            ReDim Preserve Session("sTypes")(mCOUNTE)
        End If


    End Sub
    Public Sub GridRefresh()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'ttt-Load Selected Variables
        lstVariableDisplay.Items.Clear()
        Session("lstVariableDisplayItemsCount") = 0
        Dim mCOUNTE As Integer
        Dim its = DirectCast(Session("SelSeriesItems"), ListItemCollection)
        mCOUNTE = its.Count
        'ttt-Always 2 rows (name and unit) -> change to 2 Columns (Or hyphen)
        Dim bFormula As Boolean
        Dim pos As Integer, sVar As String, sUnits As String = ""
        Dim lstUnit As New List(Of String)
        For iloop% = 1 To mCOUNTE
            pos = InStr(its.Item(iloop% - 1).Text, " - ")
            If pos > 0 Then
                sVar = Mid(its.Item(iloop% - 1).Text, 6, pos - 6)
                pos = InStr(pos + 3, its.Item(iloop% - 1).Text, " - ")
                sUnits = Mid(its.Item(iloop% - 1).Text, pos + 3)
                Session("bFormulas")(iloop%) = False
                Session("sTypes")(iloop%) = Mid(its.Item(iloop% - 1).Text, 1, 4)
            Else
                sVar = its.Item(iloop% - 1).Text

                If UBound(Session("aFormulas")) >= iloop% Then
                    bFormula = Not (Session("aFormulas")(iloop%) Is Nothing)
                    If bFormula Then
                        If Session("aFormulas")(iloop%).Name = sVar Then
                            bFormula = True
                            sUnits = Session("aFormulas")(iloop%).Unit
                        Else
                            bFormula = False
                            sUnits = ""
                        End If
                    End If
                Else
                    bFormula = False
                    sUnits = ""
                End If
                Session("bFormulas")(iloop%) = bFormula
            End If
            lstVariableDisplay.Items.Add(sVar + " | " & sUnits)
            lstUnit.Add(sUnits)
            Session("lstVariableDisplayItemsCount") = Session("lstVariableDisplayItemsCount") + 1
        Next iloop%
        Session("lstUnit") = lstUnit
    End Sub
    Private Sub SetListUnit()
        Dim lstUnit As New List(Of String)
        For Each it As ListItem In lstVariableDisplay.Items
            If it.Text.Contains("|") Then
                lstUnit.Add(it.Text.Split("|")(1).Trim)
            Else
                lstUnit.Add("")
            End If
        Next
        Session("lstUnit") = lstUnit
    End Sub
    Public Sub GridRefreshEDIT(its As List(Of String), units As List(Of String))
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'ttt-Load Selected Variables
        lstVariableDisplay.Items.Clear()
        Session("lstVariableDisplayItemsCount") = 0
        Dim bFormula As Boolean = False
        Dim mCOUNTE As Integer
        'Dim its = DirectCast(Session("SelSeriesItems"), ListItemCollection)
        mCOUNTE = its.Count
        'ttt-Always 2 rows (name and unit) -> change to 2 Columns (Or hyphen)
        'Dim bFormula As Boolean
        Dim pos As Integer, sVar As String = "", sUnits As String = ""

        For iloop% = 1 To mCOUNTE
            pos = InStr(its(iloop% - 1), " - ")
            If pos > 0 Then
                sVar = Mid(its(iloop% - 1), 6, pos - 6)
                sUnits = its(iloop% - 1).Split("-")(1).Trim
                Session("bFormulas")(iloop%) = False
                Session("sTypes")(iloop%) = its(iloop% - 1).Substring(0, 4)
            Else
                sVar = its(iloop% - 1)

                If UBound(Session("aFormulas")) >= iloop% Then
                    bFormula = Not (Session("aFormulas")(iloop%) Is Nothing)
                    If bFormula Then
                        If Session("aFormulas")(iloop%).Name = sVar Then
                            bFormula = True
                            sUnits = Session("aFormulas")(iloop%).Unit
                        Else
                            bFormula = False
                            sUnits = ""
                        End If
                    End If
                Else
                    bFormula = False
                    sUnits = ""
                End If
                Session("bFormulas")(iloop%) = bFormula
            End If
            If units.Count = mCOUNTE Then
                sUnits = units(iloop% - 1)
            End If
            lstVariableDisplay.Items.Add(sVar + " | " & sUnits)
            Session("lstVariableDisplayItemsCount") = Session("lstVariableDisplayItemsCount") + 1
        Next iloop%
    End Sub
    Private Sub LoadWeightFactors()
        Dim mCOUNTE As Integer
        Dim its = DirectCast(Session("SelSeriesItems"), ListItemCollection)
        mCOUNTE = its.Count
        Session("pnlWeightTextBoxCount") = 0
        Dim txtbox As TextBox = New TextBox
        For i = 1 To mCOUNTE
            txtbox = New TextBox
            txtbox.Text = "1"
            txtbox.ID = "WText" & (i - 1)
            pnlWeight.Controls.Add(txtbox)
            Session("pnlWeightTextBoxCount") = Session("pnlWeightTextBoxCount") + 1
            'pnlWeight.Controls.Add(New LiteralControl("&nbsp;")) '"<br>"))
        Next
        txtbox = New TextBox
        txtbox.Text = "0"
        txtbox.ID = "WText" & mCOUNTE
        pnlWeight.Controls.Add(txtbox)
        Session("pnlWeightTextBoxCount") = Session("pnlWeightTextBoxCount") + 1

    End Sub
    Private Sub LoadWeightFactorsEDIT(its As List(Of String))
        Dim mCOUNTE As Integer
        mCOUNTE = its.Count
        Session("pnlWeightTextBoxCount") = 0
        Dim txtbox As TextBox = New TextBox
        For i = 1 To mCOUNTE
            txtbox = New TextBox
            txtbox.Text = its(i - 1) '"1"
            txtbox.ID = "WText" & (i - 1)
            pnlWeight.Controls.Add(txtbox)
            Session("pnlWeightTextBoxCount") = Session("pnlWeightTextBoxCount") + 1
            'pnlWeight.Controls.Add(New LiteralControl("&nbsp;")) '"<br>"))
        Next
        txtbox = New TextBox
        txtbox.Text = "0"
        txtbox.ID = "WText" & mCOUNTE
        pnlWeight.Controls.Add(txtbox)
        Session("pnlWeightTextBoxCount") = Session("pnlWeightTextBoxCount") + 1

    End Sub
    Private Sub LoadTransformationRules()
        Dim mCOUNTE As Integer
        Dim its = DirectCast(Session("SelSeriesItems"), ListItemCollection)
        mCOUNTE = its.Count
        Session("pnlTransformationTextBoxCount") = 0
        Dim txtbox As TextBox = New TextBox
        With pnlTransformation.Controls
            Dim i As Integer
            For i = 1 To mCOUNTE
                Dim text = "X"
                If i < 10 Then
                    text = text & "0"
                End If
                text = text & Trim(CStr(i))
                txtbox = New TextBox
                txtbox.Text = text
                txtbox.ID = "T1Text" & (i - 1)
                'AddHandler txtbox., AddressOf 
                .Add(txtbox)
                Session("pnlTransformationTextBoxCount") = Session("pnlTransformationTextBoxCount") + 1
            Next i

        End With

        With pnlTransformation2.Controls
            txtbox = New TextBox
            txtbox.Text = "Y"
            txtbox.ID = "T2Text0"
            .Add(txtbox)

        End With
    End Sub
    Private Sub LoadTransformationRulesEDIT(its As List(Of String))
        Dim mCOUNTE As Integer
        'Dim its = DirectCast(Session("SelSeriesItems"), ListItemCollection)
        mCOUNTE = its.Count
        Session("pnlTransformationTextBoxCount") = 0
        Dim txtbox As TextBox = New TextBox
        With pnlTransformation.Controls
            Dim i As Integer
            For i = 1 To mCOUNTE
                'Dim text = "X"
                'If i < 10 Then
                '    text = text & "0"
                'End If
                'text = text & Trim(CStr(i))
                Dim text As String = its(i - 1)
                txtbox = New TextBox
                txtbox.Text = text
                txtbox.ID = "T1Text" & (i - 1)
                'AddHandler txtbox., AddressOf 
                .Add(txtbox)
                Session("pnlTransformationTextBoxCount") = Session("pnlTransformationTextBoxCount") + 1
            Next i

        End With

        With pnlTransformation2.Controls
            txtbox = New TextBox
            txtbox.Text = "Y"
            txtbox.ID = "T2Text0"
            .Add(txtbox)

        End With
    End Sub

    Private Sub UpdateEquation()
        Dim mCOUNTE As Integer
        mCOUNTE = lstVariableDisplay.Items.Count

        Dim sEq As String
        Dim i As Integer
        Dim sMin As String
        Dim sMax As String
        Dim sFormula As String

        ReDim Session("sFormulas")(mCOUNTE)
        Dim j As Integer
        Dim k As Integer

        sEq = "("
        Dim id As Integer = 0
        Dim wt As String = ""
        Dim t1 As String = ""
        For i = 1 To mCOUNTE
            id = pnlWeight.Controls.FindIndices(Function(x As Control) (x.ID = "WText" & (i - 1))).FirstOrDefault

            If pnlWeight.Controls(id).ID <> "WText" & (i - 1) Then Continue For
            wt = DirectCast(pnlWeight.Controls(id), TextBox).Text
            'Weight
            If Val(wt) <> 1 Then
                sEq = sEq & wt & " * "
            End If
            'Transformation
            'jrs 2013/02/27 added extra set of () to surround Transformation in case is addition or sustraction like 100-X01
            id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = "T1Text" & (i - 1))).FirstOrDefault
            t1 = DirectCast(pnlTransformation.Controls(id), TextBox).Text
            If InStr(t1, "+") > 0 Or InStr(t1, "-") > 0 Then
                sEq = sEq & "((" & t1 & ")"
            Else
                sEq = sEq & "(" & t1
            End If
            'Transformaion Index
            k = i  'id

            'Formulas
            If Session("bFormulas")(i) Then
                sFormula = Session("aFormulas")(i).Formula_Renamed
                For j = 1 To Session("aFormulas")(i).disVar.disSize
                    If k < 10 Then
                        sFormula = Replace(sFormula, Session("aFormulas")(i).disVar.display(j), "X0" & CStr(k) & Session("aFormulas")(i).disVar.display(j))
                    Else
                        sFormula = Replace(sFormula, Session("aFormulas")(i).disVar.display(j), "X" & CStr(k) & Session("aFormulas")(i).disVar.display(j))
                    End If
                Next j
                sFormula = "(" & sFormula & ")"
                If k < 10 Then
                    sEq = Replace(sEq, "X0" & CStr(k), sFormula)
                Else
                    sEq = Replace(sEq, "X" & CStr(k), sFormula)
                End If
                Session("sFormulas")(i) = sFormula
            End If
            sEq = sEq & ")"
            'Addition
            If i < mCOUNTE Then
                sEq = sEq & " + "
            End If
        Next i
        id = pnlWeight.Controls.FindIndices(Function(x As Control) (x.ID = "WText" & mCOUNTE)).FirstOrDefault

        If pnlWeight.Controls(id).ID <> "WText" & mCOUNTE Then Return
        wt = DirectCast(pnlWeight.Controls(id), TextBox).Text

        If Val(wt) > 0 Then
            sEq = sEq & " + " & wt
        ElseIf Val(wt) < 0 Then
            sEq = sEq & " - " & Math.Abs(Val(wt))
        End If

        sEq = sEq & ")"

        id = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
        Dim t2 = DirectCast(pnlTransformation2.Controls(id), TextBox).Text

        If t2 <> "Y" Then
            sEq = Replace(t2, "Y", "(" & sEq & ")")
        End If

        'Add Adjustment Factor for base year
        '-----------------------------------
        If If(Session("sAdjustmentVar"), "") > "" Then
            'Adjustment factor is Data(FirstYearForecast)/(Original Equation)(FirstYearForecast)
            Dim sfactor As String
            sfactor = "(Z?" & Session("iAdjustmentYr") & ")/(" & sEq & ")"
            'Need to force original Equation to Fixed year in Factor
            For i = 1 To mCOUNTE
                id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = "T1Text" & (i - 1))).FirstOrDefault
                t1 = DirectCast(pnlTransformation.Controls(id), TextBox).Text
                'Transformaion Index
                k = i  'k = id 'frm_BilateralInteractionDisplay.ReturnTransfIdx(grd_Transformation.text)
                'Formulas
                If Session("bFormulas")(i) Then
                    sFormula = Session("aFormulas")(i).Formula_Renamed
                    For j = 1 To Session("aFormulas")(i).disVar.disSize
                        If k < 10 Then
                            sfactor = Replace(sfactor, "X0" & CStr(k) & Session("aFormulas")(i).disVar.display(j), "(X0" & CStr(k) & Session("aFormulas")(i).disVar.display(j) & "?" & Session("iAdjustmentYr") & ")")
                        Else
                            sfactor = Replace(sfactor, "X" & CStr(k) & Session("aFormulas")(i).disVar.display(j), "(X" & CStr(k) & Session("aFormulas")(i).disVar.display(j) & "?" & Session("iAdjustmentYr") & ")")
                        End If
                    Next j
                Else
                    If k < 10 Then
                        sfactor = Replace(sfactor, "X0" & CStr(k), "(X0" & CStr(k) & "?" & Session("iAdjustmentYr") & ")")
                    Else
                        sfactor = Replace(sfactor, "X" & CStr(k), "(X" & CStr(k) & "?" & Session("iAdjustmentYr") & ")")
                    End If
                End If
            Next i

            'Adjust Formula using shift factor
            'Adjusted Formula is F*factor
            sEq = "(" & sEq & ")*" & sfactor

            'Create Residuals
            If If(Session("bAdjustemntResiduals"), False) Then
                sEq = sEq & "-Z"
            End If
        End If

        txtFormula.Text = sEq
    End Sub
    Public Sub cmdUpdEq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpdEq.Click
        UpdateEquation()
    End Sub
    Public Sub cmdRenderList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRenderList.Click
        GridRefresh()
        'UpdateEquation()
    End Sub
    Private Sub mnuSaveContinue_Click()
        If Session("bSaved") Then
            Session("bEditGravity") = True
        Else
            Session("bNewFormula") = True
        End If
        Dim f = SaveGravityVar()

        If Session("bSaved") Then
            'Leave
            Session("bAddGravity") = False
            If If(Session("bClickedDGravitySave"), False) = True Then
                'tt If has saved before in this form , then save to FormulaVars at this form too
                ' no action at main form
                SaveFormula(f)
                Session("SavedGravityVar") = False
            End If
            Session("CalledSaveContinue") = True

            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")
        End If
    End Sub
    Private Sub mnuSave_Click()
        If Session("bSaved") Then
            Session("bEditGravity") = True
        Else
            Session("bNewFormula") = True
        End If
        Dim f = SaveGravityVar()

        SaveFormula(f)

        Session("bClickedDGravitySave") = True
        'ttt No need for more action at main form, if user clicks continue
        Session("SavedGravityVar") = False
    End Sub
    Private Sub mnuTransLinear_Click()
        If Transformation2Tag.Value = "" Then
            Dim i As Integer = 1

            Dim k As Integer
            Dim id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = T1TextSelected.Value)).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation.Controls(id), TextBox)
            tb.Text = "X" & CInt(T1TextSelected.Value.Replace("T1Text", "") + 1).ToString.PadLeft(2, "0")

        Else
            Dim id = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation2.Controls(id), TextBox)
            tb.Text = "Y"
        End If

    End Sub
    Private Sub mnuTransLog_Click()
        If Transformation2Tag.Value = "" Then
            Dim k As Integer
            Dim id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = T1TextSelected.Value)).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation.Controls(id), TextBox)
            tb.Text = "LN(X" & CInt(T1TextSelected.Value.Replace("T1Text", "") + 1).ToString.PadLeft(2, "0") & ")"

        Else
            Dim id = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation2.Controls(id), TextBox)
            tb.Text = "LN(Y)"
        End If

    End Sub
    Private Sub mnuTransLogBase10_Click()
        If Transformation2Tag.Value = "" Then
            Dim id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = T1TextSelected.Value)).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation.Controls(id), TextBox)
            tb.Text = "LOG(X" & CInt(T1TextSelected.Value.Replace("T1Text", "") + 1).ToString.PadLeft(2, "0") & ")"

        Else
            Dim id = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation2.Controls(id), TextBox)
            tb.Text = "LOG(Y)"
        End If

    End Sub
    Private Sub mnuTransExp_Click()
        If Transformation2Tag.Value = "" Then
            Dim id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = T1TextSelected.Value)).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation.Controls(id), TextBox)
            tb.Text = "2.718^(X" & CInt(T1TextSelected.Value.Replace("T1Text", "") + 1).ToString.PadLeft(2, "0") & ")"

        Else
            Dim id = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation2.Controls(id), TextBox)
            tb.Text = "2.718^(Y)"
        End If

    End Sub
    Private Sub mnuTransLogistic_Click()
        If Transformation2Tag.Value = "" Then
            Dim id = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = T1TextSelected.Value)).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation.Controls(id), TextBox)
            tb.Text = "1/(1+2.718^(-1*X" & CInt(T1TextSelected.Value.Replace("T1Text", "") + 1).ToString.PadLeft(2, "0") & "))"

        Else
            Dim id = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
            Dim tb = DirectCast(pnlTransformation2.Controls(id), TextBox)
            tb.Text = "1/(1+2.718^(-1*Y))"
        End If

    End Sub
    Private Sub ManualTraChange()
        'ttt NOTHING TODO, user just types in

    End Sub
    Private Sub mnuAddMoreVars_Click()
        If Session("bSaved") Then
            Session("bEditGravity") = True
        End If
        SaveGravityVar()
        'Go get historical variable to match formula
        ReDim Preserve Session("bFormulas")(Session("lstVariableDisplayItemsCount") + 1)
        ReDim Preserve Session("sTypes")(Session("lstVariableDisplayItemsCount") + 1)
        Session("AddedMoreVars") = True
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")


    End Sub
    Private Sub mnuAddAdjData_Click()
        If cswmWebMenu.Groups("mnuEqu").Items("mnuAddAdjData").IconImage = "checkbox.gif" Then
            Session("sAdjustmentVar") = ""
            Session("sAdjustmentVarType") = ""
            lblAdjVarName.Visible = False
            cmdUpdEq_Click(Me, New EventArgs)
        Else
            If Session("bSaved") Then
                Session("bEditGravity") = True
            End If
            SaveGravityVar()
            'Go get historical variable to match formula
            Session("AddedAdjData") = True
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_BilateralInteractionDisplay.aspx")
        End If

        If cswmWebMenu.Groups("mnuEqu").Items("mnuAddAdjData").IconImage = "checkbox.gif" Then
            cswmWebMenu.Groups("mnuEqu").Items("mnuAddAdjData").IconImage = "blank.gif"
        Else
            cswmWebMenu.Groups("mnuEqu").Items("mnuAddAdjData").IconImage = "checkbox.gif"
        End If

    End Sub
    Private Sub mnuChgAdjYr_Click()
        'Dim sTmp As String
        'sTmp = InputBox("Enter adjustment base year", "Gravity Variable", iAdjustmentYr)
        'If IsNumeric(sTmp) Then
        '    iAdjustmentYr = CInt(sTmp)
        '    mnuChgAdjYr.Caption = "Change Adjustment &Year(" & iAdjustmentYr & ")"
        '    UpdateEquation()
        'End If
    End Sub
    Private Sub mnuCreateResiduals_Click()
        If Not Session("bAdjustemntResiduals") AndAlso Session("sAdjustmentVar") > "" Then
            Session("bAdjustemntResiduals") = True
            Session("bEditGravity") = False
            'Change the name of the formula
            txtFormulaName.Text = txtFormulaName.Text & "Res"
            txtGravityDescription.Text = txtGravityDescription.Text & " Residuals"
            SaveGravityVar()
        End If
    End Sub
    Private Sub AddGravityVar(sVarName As String, sType As String)
        Dim pos As Integer
        pos = InStr(sVarName, " - ")
        Dim colCounte As Integer
        colCounte = lstVariableDisplay.Items.Count + 1
        ReDim Preserve Session("bFormulas")(colCounte)
        ReDim Preserve Session("sTypes")(colCounte)
        Dim VarOnly = Mid(sVarName, 1, pos - 1)
        pos = InStr(pos + 1, sVarName, " - ")
        Dim sUnit = Mid(sVarName, pos + 3)
        lstVariableDisplay.Items.Add(VarOnly & " | " & sUnit)
        Session("lstVariableDisplayItemsCount") = Session("lstVariableDisplayItemsCount") + 1
        With pnlTransformation.Controls
            Dim text = "X"
            If colCounte > 10 Then
                text = "X" & CStr(colCounte)
            Else
                text = "X0" & CStr(colCounte)
            End If
            Dim txtbox2 = New TextBox
            txtbox2.Text = text
            txtbox2.ID = "T1Text" & (colCounte - 1)
            .Add(txtbox2)
        End With
        Dim id = pnlWeight.Controls.FindIndices(Function(x As Control) (x.ID = "WText" & (colCounte - 1))).FirstOrDefault
        Dim a = DirectCast(pnlWeight.Controls(id), TextBox).Text
        DirectCast(pnlWeight.Controls(id), TextBox).Text = "1"
        Dim txtbox = New TextBox
        txtbox.Text = a
        txtbox.ID = "WText" & colCounte
        pnlWeight.Controls.Add(txtbox)
        If sType = "DY" Or sType = "MA" Or sType = "MP" Then
            setbFormulas(False, colCounte)
        Else
            setbFormulas(True, colCounte)
        End If
        setsTypes("(" & sType & ")", colCounte)

        cmdUpdEq_Click(Me, New EventArgs)

    End Sub
    Public Sub setbFormulas(bVal As Boolean, idx As Integer)
        Session("bFormulas")(idx) = bVal
    End Sub

    Public Sub RedimsTypes(iCount As Integer)
        ReDim Session("sTypes")(iCount)
    End Sub

    Public Sub setsTypes(sVal As String, idx As Integer)
        Session("sTypes")(idx) = Trim(sVal)
    End Sub
    Protected Sub ShowConfirm(message As String)

        Dim sb As New System.Text.StringBuilder()
        sb.Append("return confirm('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "confirm", sb.ToString)
    End Sub
    Private Function SaveGravityVar() As Formula
        UpdateEquation()

        If Session("bEditGravity") Then
            'Delete existing data
            DeleteGravityVar(Session("sVarNameGravity"))

        Else 'Set Name for index, variable list and detailed variable list
            'ttt NO NEED
            'If txtFormulaName.Text = "Gravity1" Then
            '    ShowAlert("Please enter Gravity Variable name")
            '    'Dim tmp As String
            '    'tmp = InputBox("Please enter Gravity Variable name", "Gravity Variable creation", "Gravity1")
            '    'If tmp = "" Then Exit Sub
            '    'txtFormulaName.Text = tmp
            '    txtFormulaName.Focus()
            '    Return Nothing
            'End If
            'jrs 2010/08/20
            'Check that the Index Name doesn't exist
            If CheckGravityVarNameExist(txtFormulaName.Text) Then
                ShowAlert("That name already exists, please select a new one or Delete the Gravity variable name " & txtFormulaName.Text)
                Return Nothing
            End If

            Session("sVarNameGravity") = txtFormulaName.Text
        End If

        SetListUnit()

        'Save Variable
        Dim f As Formula = New Formula

        f.init(txtFormula.Text)
        f.Name = txtFormulaName.Text
        f.Unit = ""
        f.begAll = 1
        f.endAll = 1

        Dim i As Integer, j As Integer
        With lstVariableDisplay
            For i = 1 To Session("pnlTransformationTextBoxCount") 'grd_Transformation.cols
                Dim col = i - 1
                If Session("bFormulas")(i) Then
                    For j = 1 To Session("aFormulas(i)").disVar.disSize
                        If i < 10 Then
                            f.disVar.setDataDetS("X0" & CStr(i) & Session("aFormulas")(i).disVar.display(j), Session("aFormulas")(i).disVar.displayDet(j))
                        Else
                            f.disVar.setDataDetS("X" & CStr(i) & Session("aFormulas")(i).disVar.display(j), Session("aFormulas")(i).disVar.displayDet(j))
                        End If
                    Next j
                Else
                    If i < 10 Then
                        '? lstVariableDisplay.Items(col).Text
                        '"ExportsEn | USD (constant 2011)"
                        f.disVar.setDataDetS("X0" & CStr(i), Session("sTypes")(i) & " " & .Items(col).Text.Split("|")(0).Trim)
                    Else
                        f.disVar.setDataDetS("X" & CStr(i), Session("sTypes")(i) & " " & .Items(col).Text.Split("|")(0).Trim)
                    End If
                End If
            Next i
        End With

        'Add Adjustment Factor for base year
        '-----------------------------------
        If Session("sAdjustmentVar") > "" Then
            f.disVar.setDataDetS("Z", "(" & Session("sAdjustmentVarType") & ") " & Session("sAdjustmentVar"))
        End If

        If 1 > UBound(Session("aFormulas")) Then
            ReDim Preserve Session("aFormulas")(1)
        End If
        'ttt Set Session("aFormulas") , only one
        Session("aFormulas")(1) = f

        Session("bCompAll") = True
        'sOriginForm = "frm_BilateralInteractionDisplay"
        Session("sOrigDyadic") = "SF"
        Session("SavedGravityVar") = True 'Continue at frm_BilateralInteractionDisplay
        Session("sGravityDescription") = txtGravityDescription.Text
        Session("fName") = f.Name

        SaveGravityDet()

        'jrs 2010/07/19
        Session("NeedFileWrite") = True
        Session("bSaved") = True
        Return f


    End Function
    Private Sub DeleteGravityVar(sGravityVarName As String)
        Dim db = Session("PutFdrvRun") + "DataDictDyadic.mdb"
        Dim sql As String
        sql = "DELETE * FROM FormulaVars WHERE FormulaName = """ & sGravityVarName & """"
        updateAccessWith1Command(db, sql)

        sql = "DELETE * FROM FormulaVarsDet WHERE FormulaName = """ & sGravityVarName & """"
        updateAccessWith1Command(db, sql)

        sql = "DELETE * FROM Gravity WHERE GravityVarName = """ & sGravityVarName & """"
        updateAccessWith1Command(db, sql)

        sql = "DELETE * FROM  GravityDet WHERE GravityVarName = """ & sGravityVarName & """"
        updateAccessWith1Command(db, sql)

    End Sub
    Private Function CheckGravityVarNameExist(sGravityVarName As String) As Boolean
        Dim rs As DataTable = New DataTable
        Dim db As String = Session("PutFdrvRun") + "DataDictDyadic.mdb"
        Dim sql As String
        Dim res As Boolean = False
        sql = "SELECT COUNT(*) FROM FormulaVars WHERE FormulaName = """ & sGravityVarName & """"
        SetDataTablefromAccess(db, sql, "", rs)
        If rs.Rows(0).Item(0) > 0 Then
            Return True
        End If
        sql = "SELECT COUNT(*) FROM FormulaVarsDet WHERE FormulaName = """ & sGravityVarName & """"
        SetDataTablefromAccess(db, sql, "", rs)
        If rs.Rows(0).Item(0) > 0 Then
            Return True
        End If
        sql = "SELECT COUNT(*) FROM Gravity WHERE GravityVarName = """ & sGravityVarName & """"
        SetDataTablefromAccess(db, sql, "", rs)
        If rs.Rows(0).Item(0) > 0 Then
            Return True
        End If
        sql = "SELECT COUNT(*) FROM GravityDet WHERE GravityVarName = """ & sGravityVarName & """"
        SetDataTablefromAccess(db, sql, "", rs)
        If rs.Rows(0).Item(0) > 0 Then
            Return True
        End If

        Return res
    End Function
    Protected Sub ShowAlert(message As String)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "alert('" & message.Replace("'", "\'") & "');", True)
        Return
        '=======
        Dim sb As New System.Text.StringBuilder()
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())
    End Sub
    Private Sub SaveGravityDet()
        Dim db As String = Session("PutFdrvRun") & "DataDictDyadic.mdb"
        Dim i As Integer, sql As String, sql2 As String

        Dim idT2 = pnlTransformation2.Controls.FindIndices(Function(x As Control) (x.ID = "T2Text0")).FirstOrDefault
        Dim idW = pnlWeight.Controls.FindIndices(Function(x As Control) (x.ID = "WText" & (Session("pnlWeightTextBoxCount") - 1))).FirstOrDefault

        sql = "INSERT INTO Gravity(GravityVarName, AdjustmentVarName, AdjustmentVarType, Transformation, Coefficient)"
        sql2 = " VALUES(""" & txtFormulaName.Text & """,""" & Session("sAdjustmentVar") & """,""" & Session("sAdjustmentVarType") & """,""" & DirectCast(pnlTransformation2.Controls(idT2), TextBox).Text & """," & DirectCast(pnlWeight.Controls(idW), TextBox).Text & ")"
        updateAccessWith1Command(db, sql & sql2)
        sql = "INSERT INTO GravityDet(GravityVarName, VarName, VarType, Formula, Transformation, Coefficient)"

        For i = 0 To Session("lstVariableDisplayItemsCount") - 1
            Dim idT = pnlTransformation.Controls.FindIndices(Function(x As Control) (x.ID = "T1Text" & i)).FirstOrDefault
            idW = pnlWeight.Controls.FindIndices(Function(x As Control) (x.ID = "WText" & i)).FirstOrDefault
            Dim vText As String = lstVariableDisplay.Items(i).Text.Split("|")(0).Trim
            sql2 = " VALUES(""" & txtFormulaName.Text & """,""" & vText & """,""" & Session("sTypes")(i + 1) & """," & Session("bFormulas")(i + 1) & ",""" & DirectCast(pnlTransformation.Controls(idT), TextBox).Text & """," & DirectCast(pnlWeight.Controls(idW), TextBox).Text & ")"
            updateAccessWith1Command(db, sql & sql2)
        Next i
        If txtGravityDescription.Text = "" Then
            Dim rs As DataTable = New DataTable
            SetDataTablefromAccess(db, "SELECT Description FROM FormulaVars WHERE FormulaName = """ & txtFormulaName.Text & """", "", rs)
            If rs.Rows.Count > 0 Then
                txtGravityDescription.Text = rs.Rows(0).Item(0)
            End If
        End If
    End Sub

    Public Function NoNullStr(o As Object) As String
        If o Is Nothing OrElse o Is DBNull.Value Then
            Return ""
        End If

        Try
            Dim res = CStr(o)
            Return res ' If(Math.Abs(res) < 10, res, res Mod 10)
        Catch ex As Exception

        End Try
        Return ""
    End Function
    Public Function DisplayFormula(sFormulaG As String) As String
        'ttt Reverse Function Checking() at Formula.vb
        'to display at DGravity TextBox
        '**************** remove blanks ************************
        sFormulaG = Replace(sFormulaG, " ", "")

        'jrs 2014/02/26
        'Consider exponential power exp(1) = 2.71828182846
        sFormulaG = Replace(UCase(sFormulaG), "2.7182^", "E^")

        '**************** replace regular format with scientific format , 0.2 ->  2E-1 

        '**************** mono operand "-" **********************
        ' Looks for a "!" to turn it into "-"
        sFormulaG = sFormulaG.Replace("!", "-")
        '**************** look for Log 10**********************
        ' Looks for a "LOG base 10" and change it into &
        sFormulaG = sFormulaG.Replace("1/&(10)*&", "LOG")

        '**************** look for LOG **********************
        ' Looks for & and change it into "LOG"  
        sFormulaG = sFormulaG.Replace("&", "LN")

        '**************** look for reference operation ******
        sFormulaG = sFormulaG.Replace("?", "REF")

        '**************** implicit * with parenthesis **********************
        'Checking--
        ' Looks for a "(" with a previous operand to insert a *
        'no need
        ' Looks for a ")" with a following operand to insert a *
        'no need


        Return sFormulaG
    End Function
    Private Sub SaveFormula(Fm As Formula)
        'Partial copied from  InteractionDisplay
        Dim db = Session("PutFdrvRun") & "DataDictDyadic.mdb"
        Dim sql As String
        Dim rsCheck As DataTable = New DataTable
        Dim sDescription As String = ""
        If Session("bNewFormula") Then
            SetDataTablefromAccess(db, "SELECT COUNT(*) FROM FormulaVars WHERE FormulaName = """ & Fm.Name & """", "", rsCheck)
            If rsCheck.Rows(0).Item(0) > 0 Then
                ShowAlert("This variable name already exists, please rename your formula and try again.")
                Return
            End If
        Else
            SetDataTablefromAccess(db, "SELECT Description FROM FormulaVars WHERE FormulaName = """ & Fm.Name & """", "", rsCheck)
            If rsCheck.Rows.Count > 0 Then
                sDescription = rsCheck.Rows(0).Item("Description")
            End If
            If Not Session("bEditGravity") Then
                SetDataTablefromAccess(db, "SELECT Count(*) FROM Gravity WHERE GravityVarName = """ & Fm.Name & """", "", rsCheck)

                If rsCheck.Rows(0).Item(0) > 0 Then
                    'Dim ans As Integer = vbNo
                    'ans = MsgBox("This is a Gravity formula, if you save it using this option, you will not be able to edit it as a Gravity Variable. Are you sure you want to continue?", vbCritical, "Save Formula")
                    'If ans = vbNo Then

                    '    Exit Sub
                    'End If
                End If
            End If

            updateAccessWith1Command(db, "DELETE * FROM FormulaVars WHERE FormulaName = """ & Fm.Name & """")
            updateAccessWith1Command(db, "DELETE * FROM FormulaVarsDet WHERE FormulaName = """ & Fm.Name & """")


            updateAccessWith1Command(db, "DELETE * FROM Gravity WHERE GravityVarName = """ & Fm.Name & """")
            updateAccessWith1Command(db, "DELETE * FROM  GravityDet WHERE GravityVarName = """ & Fm.Name & """")
        End If

        If Session("sGravityDescription") = "" Then
            sDescription = ""
            'sDescription = InputBox("Type a Description for your formula", "Save Formulas", sDescription)
        Else
            sDescription = Session("sGravityDescription")
        End If
        Dim sUnits As String = ""
        'From DGravity
        If Not Session("lstUnit") Is Nothing Then
            Dim lst = DirectCast(Session("lstUnit"), List(Of String))
            For x = 0 To lst.Count - 1
                sUnits &= lst(x) & If(x = lst.Count - 1, "", ",")
            Next
            Session("lstUnit") = Nothing
        End If

        sql = "INSERT INTO FormulaVars(FormulaName, Formula, Units, Description) Values(""" & Fm.Name & """, """ &
            Fm.Formula_Renamed & """, """ & sUnits & """, """ & sDescription & """)"
        updateAccessWith1Command(db, sql)

        Dim j As Integer
        For j = 1 To Fm.disVar.disSize
            sql = "INSERT INTO FormulaVarsDet(FormulaName, Code, VarName) Values(""" & Fm.Name & """, """ &
                Fm.disVar.display(j) & """, """ & Fm.disVar.displayDet(j) & """)"
            updateAccessWith1Command(db, sql)
        Next j
        If Not Session("bEditGravity") Then
            ShowAlert("Formula " & Fm.Name & " saved.")
        End If
        Session("NeedFileWrite") = True
        'LoadformulaVars(Session("PutFdrvRun") & "DataDictDyadic.mdb")
    End Sub
End Class

