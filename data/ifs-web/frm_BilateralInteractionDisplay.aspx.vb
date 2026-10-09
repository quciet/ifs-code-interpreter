
Imports System.Drawing
Imports System.Data.SQLite
Imports IFs.Data

Partial Class frm_BilateralInteractionDisplay
    Inherits Page

#Region " Web Form Designer Generated Code "
    'JAM 2019-12-26
    Dim vSQLiteCon As SqliteConnection
    Dim vSQLiteCommand As SqliteCommand
    Dim vSQLiteReader As SqliteDataReader
    Dim vConnString As String
    Dim stitle As String = "Bilateral Interaction"



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
        Me.txtNewFormula.Attributes.Add("onkeyup", "return clickButton(event,'" + cmdRenameFormula.ClientID + "')")

    End Sub


#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If If(Session("bPlsExitForm"), False) Then
            Session("bPlsExitForm") = Nothing
            If Session("sOriginForm") = "frm_BilateralInteractionDisplay" Then
                Session("sOriginForm") = ""
            End If
            If Session("DisplayParent") = Nothing Then
                Session("DisplayParent") = "frm_MainMenu.aspx"
            End If
            Support.FormManager.Show(Me, Session("DisplayParent"))
        End If

        'If txtClickedOption.Value = "Y" Then
        '    txtClickedOption.Value = "N"
        '    Return
        'End If


        'jrs 2005/10/13
        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler

        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add() '==
        cswmGroup1.Caption = "Continue"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        cswmGroup1 = cswmWebMenu.Groups.Add() '=========
        cswmGroup1.Caption = "Series Type"
        cswmGroup1.ID = "mnuSerType"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Dyadic"
        cswmItem.ID = "mnuDyadic"
        If If(Session("OrigDyadic"), "") = "DY" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Monadic for Actor"
        cswmItem.ID = "mnuMonadicforActor"
        If If(Session("OrigDyadic"), "") = "MA" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Monadic for Partner"
        cswmItem.ID = "mnuMonadicforPartner"
        If If(Session("OrigDyadic"), "") = "MP" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Saved Formulas"
        cswmItem.ID = "mnuShowSavedFormulas"
        If If(Session("OrigDyadic"), "") = "SF" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Forecast"
        cswmItem.ID = "mnuForecast"
        If If(Session("OrigDyadic"), "") = "FC" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        'jrs 2021/02/04
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Forecast with Historical Analog"
        cswmItem.ID = "mnuForecastWHist"
        If If(Session("OrigDyadic"), "") = "FH" Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add() '=========
        cswmGroup1.Caption = "Save Table"
        cswmGroup1.ID = "mnuSaveHdr"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save to File"
        cswmItem.ID = "mnuSave"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save to Clipboard"
        cswmItem.ID = "mnuSaveClip"
        'cswmItem.OnClick = "hcopy(); return false;"
        cswmItem.OnClick = "tablecopy(); return false;"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save Transpose"
        cswmItem.ID = "mnuSaveTrans"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save in Panel Format"
        cswmItem.ID = "mnuSaveInPanel"
        '=====
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Save Formula"
        cswmGroup1.ID = "mnuSaveFormula"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save Formula"
        '=====
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Rename Formula"
        cswmGroup1.ID = "mnuRenameFormula"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Rename Formula"
        '=====
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Edit Formula"
        cswmGroup1.ID = "mnuEditFormula"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Edit Formula"
        '=====
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Edit Gravity"
        cswmGroup1.ID = "mnuEditGravity"
        'cswmGroup1.OnMenuBarClick = "PageMethods.mnuEditGravity_Click();"
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Edit Gravity"


        '====================
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Comparison Options" '=================
        cswmGroup1.ID = "mnuComp"
        If Session("bComparisonOn") Is Nothing Then
            Session("bComparisonOn") = False
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            Session("bCompPctAB") = False
            Session("bCompPctBA") = False

        ElseIf Session("bCompRatAB") Is Nothing Then
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            Session("bCompPctAB") = False
            Session("bCompPctBA") = False
        End If

        If Session("CompLabel") Is Nothing Then
            Dim mCompLabel(30, 30) As String
            Session("CompLabel") = mCompLabel
        End If
        If Session("CompBackup") Is Nothing Then
            Dim mCompBackup(30, 30) As Decimal
            Session("CompBackup") = mCompBackup
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Ratio Comparison (a/b)"
        cswmItem.ID = "mnuCompRatAB"
        If Session("bCompRatAB") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Ratio Comparison (b/a)"
        cswmItem.ID = "mnuCompRatBA"
        If Session("bCompRatBA") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Difference Comparison (a-b)"
        cswmItem.ID = "mnuCompDiffAB"
        If Session("bCompDiffAB") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Difference Comparison (b-a)"
        cswmItem.ID = "mnuCompDiffBA"
        If Session("bCompDiffBA") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        '---
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Comparison (a-b)/b*100"
        cswmItem.ID = "mnuCompPctAB"
        If Session("bCompPctAB") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Percent Comparison (b-a)/a*100"
        cswmItem.ID = "mnuCompPctBA"
        If Session("bCompPctBA") Then
            cswmItem.IconImage = "checkbox.gif"
        Else
            cswmItem.IconImage = "blank.gif"
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset"
        cswmItem.ID = "mnuCompReset"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Display Options" '=================
        cswmGroup1.ID = "mnuDisplay"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legends"
        cswmItem.ID = "mnuFormatLegends"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legends First Dimension Only"
        cswmItem.ID = "mnuFormatLeg1Dim"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legends Second Dimension Only"
        cswmItem.ID = "mnuFormatLeg2Dim"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legends Dimensions Only"
        cswmItem.ID = "mnuFormatLegDims"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legends No Dimensions"
        cswmItem.ID = "mnuFormatLegNoDims"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Format Legends Scenarios Only"
        cswmItem.ID = "mnuFormatSce"


        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Reset Legends"
        cswmItem.ID = "mnuResetLegends"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Scale Options" '===
        cswmGroup1.ID = "mnuScale"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "No Scale"
        cswmItem.ID = "mnuScaleNO"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Common Scale"
        cswmItem.ID = "mnuScaleCommon"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Index Base 1 Scale"
        cswmItem.ID = "mnuIndexBase1Scale"
        '====
        cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuEditGravity").ShowMenuBarItem = False

        'cswmWebMenu.Groups("mnuDisplay").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuScale").ShowMenuBarItem = False

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            '?HttpContext.Current.Request.UrlReferrer.AbsolutePath
            '"/i7/IFsWeb/frm_MainMenu.aspx"

            If (Not If(Session("SavedGravityVar"), False)) AndAlso
               (Not HttpContext.Current.Request.UrlReferrer.AbsolutePath.EndsWith("frm_BilateralInteractionDGravity.aspx")) AndAlso
               (Not HttpContext.Current.Request.UrlReferrer.AbsolutePath.EndsWith("frm_Confirm.aspx")) Then
                Dim InFile As String, OutFile As String
                'jam 2022/12/04
                'InFile = DataFileDir$ & "DataDictDyadic.mdb"
                'OutFile = Session("PutFdrvRun") & "DataDictDyadic.mdb"
                'FileCopyCheck(InFile, OutFile)

                'InFile = DataFileDir$ & "ifsHistSeriesDyadic.mdb"
                'OutFile = Session("PutFdrvRun") & "ifsHistSeriesDyadic.mdb"
                'FileCopyCheck(InFile, OutFile)

                'InFile = DataFileDir$ & "DataDictDyadicForecast.mdb"
                'OutFile = Session("PutFdrvRun") & "DataDictDyadicForecast.mdb"
                'FileCopyCheck(InFile, OutFile)

                'jam 2022/12/04
                If Not Session("mnuSQLite") Is Nothing Then
                    If Session("mnuSQLite") = "True" Then 'jam 2022/12/04 sqlite files
                        InFile = DataFileDir$ & "DataDictDyadic.db"
                        OutFile = Session("PutFdrvRun") & "DataDictDyadic.db"
                        FileCopyCheck(InFile, OutFile)

                        InFile = DataFileDir$ & "ifsHistSeriesDyadic.db"
                        OutFile = Session("PutFdrvRun") & "ifsHistSeriesDyadic.db"
                        FileCopyCheck(InFile, OutFile)

                        InFile = DataFileDir$ & "DataDictDyadicForecast.db"
                        OutFile = Session("PutFdrvRun") & "DataDictDyadicForecast.db"
                        FileCopyCheck(InFile, OutFile)
                    Else 'jam 2022/12/04 Access files
                        InFile = DataFileDir$ & "DataDictDyadic.mdb"
                        OutFile = Session("PutFdrvRun") & "DataDictDyadic.mdb"
                        FileCopyCheck(InFile, OutFile)

                        InFile = DataFileDir$ & "ifsHistSeriesDyadic.mdb"
                        OutFile = Session("PutFdrvRun") & "ifsHistSeriesDyadic.mdb"
                        FileCopyCheck(InFile, OutFile)

                        InFile = DataFileDir$ & "DataDictDyadicForecast.mdb"
                        OutFile = Session("PutFdrvRun") & "DataDictDyadicForecast.mdb"
                        FileCopyCheck(InFile, OutFile)


                    End If
                Else 'jam 2022/12/04 Access files
                    InFile = DataFileDir$ & "DataDictDyadic.mdb"
                    OutFile = Session("PutFdrvRun") & "DataDictDyadic.mdb"
                    FileCopyCheck(InFile, OutFile)

                    InFile = DataFileDir$ & "ifsHistSeriesDyadic.mdb"
                    OutFile = Session("PutFdrvRun") & "ifsHistSeriesDyadic.mdb"
                    FileCopyCheck(InFile, OutFile)

                    InFile = DataFileDir$ & "DataDictDyadicForecast.mdb"
                    OutFile = Session("PutFdrvRun") & "DataDictDyadicForecast.mdb"
                    FileCopyCheck(InFile, OutFile)

                End If


            End If

            '======
            'Redimension array to fit all cohorts, and as many region,gender,type combinations as possible
            ReDim Session("mLegends")(4, MaxVariables)
            'Load Geografic List
            LoadCountries()
            LoadSeries()
            LoadOtherSeries()
            '----
            '============
            LoadFiles()
            '===============
            Dim rs As DataTable = New DataTable
            Session("bComparisonOn") = False
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            Session("bCompPctAB") = False
            Session("bCompPctBA") = False
            If Not Session("txtFiles") Is Nothing Then
                txtFiles.Value = Session("txtFiles")
            End If
            '--Bila---
            If Session("OrigDyadic") Is Nothing Then
                Session("OrigDyadic") = "DY"
                cswmWebMenu.Groups("mnuSerType").Items("mnuDyadic").IconImage = "checkbox.gif"
                Dim gr = cswmWebMenu.Groups("mnuSerType")
                For Each it As Coalesys.WebMenu.Item In gr.Items
                    If it.ID <> "mnuDyadic" Then
                        it.IconImage = "blank.gif"
                    End If
                Next
            End If
            If Session("bDyadicSeries") Is Nothing Then
                Dim bArr(1000) As Boolean
                Session("bDyadicSeries") = bArr
            End If
            If Session("bMonadicActorSeries") Is Nothing Then
                Dim bArr(1000) As Boolean
                Session("bMonadicActorSeries") = bArr
            End If
            If Session("sAggRulesSelected") Is Nothing Then
                Dim sArr(1000) As String
                Session("sAggRulesSelected") = sArr
            End If
            If Session("iCOUNTE") Is Nothing Then
                Dim val As Integer = 0
                Session("iCOUNTE") = val
            End If
            'Session("SavedGravityVar") = False
            Session("AddedAdjData") = False
            'Session("CalledSaveContinue") = False
            Session("AddedMoreVars") = False
            Session("bAddGravity") = False
            Session("bAddGravityAdj") = False
            If Not (Session("optGeographyId") Is Nothing) Then
                Select Case Session("optGeographyId")
                    Case 1
                        optReg.Checked = True
                        optGroup.Checked = False
                        optGroupDec.Checked = False
                        optGList.Checked = False
                    Case 2
                        optGroup.Checked = True
                        optReg.Checked = False
                        optGroupDec.Checked = False
                        optGList.Checked = False
                    Case 3
                        optGroupDec.Checked = True
                        optGroup.Checked = False
                        optReg.Checked = False
                        optGList.Checked = False
                    Case 4
                        optGList.Checked = True
                        optGroupDec.Checked = False
                        optGroup.Checked = False
                        optReg.Checked = False
                End Select

            End If

            Session("FormatLegends") = Nothing
        End If

        cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = grdBilateral.Visible
        cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = grdBilateral.Visible
        cswmWebMenu.Groups("mnuDisplay").ShowMenuBarItem = lblJS.Visible

        If Not IsNothing(Session("LoadSearchVar")) Then
            LoadSearch(Session("LoadSearchVar"))
            Session("LoadSearchVar") = Nothing
        End If
        'Dim scriptMan As ScriptManager = ScriptManager.GetCurrent(Me)
        MainSM.RegisterAsyncPostBackControl(DirectCast(lstSeries, ListBox))
        MainSM.RegisterAsyncPostBackControl(DirectCast(lstSelSeries, ListBox))
        'jrs 2024/08/28
        MainSM.RegisterAsyncPostBackControl(DirectCast(lstVarNameMonadyc, ListBox))
        '----

        'Show correct lists
        SwitchSeriesType("", Session("OrigDyadic"))
        If If(Session("ContinueSaveNonGravity"), False) = True Then
            mnuSaveFormula_Click(bFromGravity:=False, bContinueSaveNonGravity:=True)
        End If

        'From Gravity Form--------------------------------------------------------
        If If(Session("SavedGravityVar"), False) = True Then
            'ttt From Gravity
            Session("SavedGravityVar") = False
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
            'cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuEditGravity").ShowMenuBarItem = True
            lstSelSeries.Items.Clear()
            lstSelSeries.Items.Add(Session("fName"))
            Dim var = mnuSaveFormula_Click()
            'ttt now we have just added a var into selected list, prepare so that can make table
            lstVarNameSavedFormulas_SelectedIndexChanged(Me, New EventArgs, True, var)
        End If
        If If(Session("AddedAdjData"), False) = True Then
            Session("AddedAdjData") = False
            Session("bAddGravityAdj") = True
            lstSelSeries.Visible = False
            lstSelDyads.Visible = False
            SwitchSeriesType("mnuDyadic")
        End If
        If If(Session("CalledSaveContinue"), False) = True Then
            Session("CalledSaveContinue") = False
            Session("bAddGravity") = False
            SwitchSeriesType("mnuDyadic")
            'LoadPreviousSelected()
        End If
        If If(Session("AddedMoreVars"), False) = True Then
            Session("AddedMoreVars") = False
            Session("bAddGravity") = True
            lstSelSeries.Visible = False
            lstSelDyads.Visible = False
            SwitchSeriesType("mnuDyadic")
        End If
        '?HttpContext.Current.Request.UrlReferrer.AbsolutePath
        '"/i7/IFsWeb/frm_MainMenu.aspx"
        Dim referrer = HttpContext.Current.Request.UrlReferrer.AbsolutePath
        If Not referrer.EndsWith("frm_MainMenu.aspx") Then
            Check2ShowEditFormula()
        End If
        If If(Session("BL_Exit_From_Computation"), False) = True Then
            Session("BL_Exit_From_Computation") = False
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True

            Dim f0 As Formula = Session("aFormulas")(Session("aFormulas").Length - 1)
            Dim f_1 As Formula = Nothing
            If Session("aFormulas").Length >= 2 AndAlso (Not Session("aFormulas")(Session("aFormulas").Length - 2) Is Nothing) Then
                f_1 = Session("aFormulas")(Session("aFormulas").Length - 2)
            End If

            Dim fs As List(Of Formula) = New List(Of Formula)
            fs.Add(Nothing)
            Dim fns As List(Of String) = New List(Of String)
            For i = 0 To Session("aFormulas").Length - 1
                If (Not Session("aFormulas")(i) Is Nothing) Then
                    If Not fns.Contains(Session("aFormulas")(i).Name) Then
                        fns.Add(Session("aFormulas")(i).Name)
                        fs.Add(Session("aFormulas")(i))
                    End If
                End If
            Next
            Session("aFormulas") = fs.ToArray
            'Add newly created formula
            Dim curlst = DirectCast(Session("curlstSelSeries"), ListBox)
            If (Not curlst Is Nothing) AndAlso (Not curlst.Items.Count = 0) Then
                Me.lstSelSeries.Items.Clear()
                For i = 0 To Session("aFormulas").Length - 1
                    If Not Session("aFormulas")(i) Is Nothing Then
                        Me.lstSelSeries.Items.Add(Session("aFormulas")(i).Name)
                    End If
                Next
            End If
        End If
        If If(Session("CP_Visiblibly_ForBILAT"), False) = True Then
            cswmWebMenu.Groups("mnuSerType").Items("mnuShowSavedFormulas").Enabled = True
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            Session("CP_Visiblibly_ForBILAT") = False
        End If
        If If(Session("BL_HasFormulaReport"), False) = True Then
            cswmWebMenu.Groups("mnuSerType").Items("mnuShowSavedFormulas").Enabled = True
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            Session("BL_HasFormulaReport") = False
        End If

        If If(Session("BL_SelectVar"), False) = True Then
            Me.TitleCell.Text = stitle & " (Computation- Select Variable)"
        Else
            Me.TitleCell.Text = stitle
        End If

        '
        'Session("bAddGravity") = True


    End Sub
    Protected Sub lstFiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        'jrs 2011/11/04
        'Session("txtFiles") = txtFiles.Value
        Session("ForeCastFileIdx") = lstFiles.SelectedIndex

        Dim FileSelCount As Integer = 0
        For Each it As ListItem In lstFiles.Items
            If it.Selected Then
                FileSelCount += 1
            End If
        Next
        ReDim Session("FCFilesIdxs")(lstFiles.Items.Count - 1)
        ReDim Session("dbDyFor")(FileSelCount) ' As String for DatabaseName
        Dim II As Integer, ss As Integer, sFile As String
        Dim pos As Integer
        ss = 0
        For II = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(II).Selected Then
                Session("FCFilesIdxs")(II) = True
                If II = 1 Then
                    Session("dbDyFor")(ss) = DataFileDir$ & "\ifsForDyadic.db"
                ElseIf II = 0 Then
                    Session("dbDyFor")(ss) = Session("PutFdrvRun") & "ifsForDyadicWork.db"
                Else
                    sFile = lstFiles.Items(II).Text
                    pos = InStr(sFile, " - ")
                    sFile = Mid(sFile, pos + 3)
                    Session("dbDyFor")(ss) = Session("PutFdrvRun") & sFile
                End If
                ss = ss + 1
            Else
                Session("FCFilesIdxs")(II) = False
            End If
        Next II

    End Sub
    Private Sub cmdRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemove.Click
        Dim sels As New List(Of ListItem)
        Dim ids As New List(Of Integer)
        Dim id As Integer = 0
        For Each it As ListItem In lstSelSeries.Items
            If it.Selected Then
                sels.Add(it)
                ids.Add(id)
            End If
            id += 1
        Next
        For k = 0 To sels.Count - 1
            If Not sels(k).Text.StartsWith("(") Then
                'Remove Formula
                If Not Session("aFormulas") Is Nothing Then
                    Dim lstF As New List(Of Formula)
                    For Each f As Formula In Session("aFormulas")
                        If f Is Nothing OrElse f.Name <> sels(k).Text Then
                            lstF.Add(f)
                        End If
                    Next
                    Session("aFormulas") = lstF.ToArray
                End If
            End If
            lstSelSeries.Items.Remove(sels(k))
            Session("curlstSelSeries") = lstSelSeries
            Dim i As Integer
            For i = ids(k) To lstSelSeries.Items.Count - 1
                Session("sAggRulesSelected")(i) = Session("sAggRulesSelected")(i + 1)
                Session("bDyadicSeries")(i) = Session("bDyadicSeries")(i + 1)
                Session("bMonadicActorSeries")(i) = Session("bMonadicActorSeries")(i + 1)
            Next i
            Session("sGravityDescription") = ""
            Session("sAggRulesSelected")(lstSelSeries.Items.Count) = ""
            Session("bDyadicSeries")(lstSelSeries.Items.Count) = False
            Session("bMonadicActorSeries")(lstSelSeries.Items.Count) = False
        Next

        Check2ShowEditFormula()

    End Sub

    Private Sub cmdTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdTable.Click
        If lstSelSeries.Items.Count = 0 OrElse lstSelDyads.Items.Count = 0 Then Return
        If Not lblJS.Visible Then
            'LoadData()
            LoadDataBILA()
        Else
            lblJS.Visible = False
        End If
        pnlMain.Visible = False
        pnlSelecteds.Visible = False
        cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
        cswmWebMenu.Groups("mnuDisplay").ShowMenuBarItem = False
        cswmWebMenu.Groups("mnuScale").ShowMenuBarItem = False
        If Session("TotalGraphs") > 0 Then
            LoadGrid()
            If Not Session("bComparisonOn") Then
                Dim mCompBackup(UBound(Session("mPLOT"), 1), Session("TotalGraphs")) As Decimal
                Session("CompBackup") = mCompBackup
                Dim mCompLabel(4, Session("TotalGraphs")) As String
                Session("CompLabel") = mCompLabel
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
                Session("bCompRatAB") = False
                Session("bCompRatBA") = False
                Session("bCompDiffAB") = False
                Session("bCompDiffBA") = False
                Session("bCompPctAB") = False
                Session("bCompPctBA") = False
            End If

            cmdDraw.Enabled = True
            cmdTable.Enabled = False
            cmdSearch.Enabled = False
            cmdGravity.Enabled = False
            cmdComputation.Enabled = False
            'ttt Allow save
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = True
        End If
    End Sub
    Private Sub LoadGrid()
        Dim j As Integer, i As Integer, r(5) As TableRow, ce As TableCell
        'Load Legends (Column Header)
        For j = 1 To Session("TotalGraphs")
            If j = 1 Then
                r(0) = New TableRow
                ce = New TableCell
                ce.Text = ""
                ce.BackColor = System.Drawing.Color.Gray
                r(0).Cells.Add(ce)
            End If
            ce = New TableCell
            ce.Text = Session("mLegends")(4, j)  '4 is Main Variable
            ce.BackColor = System.Drawing.Color.Gray
            r(0).Cells.Add(ce)
            '-----
            If j = 1 Then
                r(1) = New TableRow
                ce = New TableCell
                ce.Text = ""
                ce.BackColor = System.Drawing.Color.Gray
                r(1).Cells.Add(ce)
            End If
            ce = New TableCell
            ce.Text = Session("mLegends")(1, j)
            ce.BackColor = System.Drawing.Color.Gray
            r(1).Cells.Add(ce)
            '-----
            If j = 1 Then
                r(2) = New TableRow
                ce = New TableCell
                ce.Text = ""
                ce.BackColor = System.Drawing.Color.Gray
                r(2).Cells.Add(ce)
            End If
            ce = New TableCell
            ce.Text = Session("mLegends")(2, j)
            ce.BackColor = System.Drawing.Color.Gray
            r(2).Cells.Add(ce)
            '-----
            If j = 1 Then
                r(3) = New TableRow
                ce = New TableCell
                ce.Text = ""
                ce.BackColor = System.Drawing.Color.Gray
                r(3).Cells.Add(ce)
            End If
            ce = New TableCell
            ce.Text = Session("mLegends")(3, j)
            ce.BackColor = System.Drawing.Color.Gray
            r(3).Cells.Add(ce)
            '-----
            If j = 1 Then
                r(4) = New TableRow
                ce = New TableCell
                ce.Text = ""
                ce.BackColor = System.Drawing.Color.Gray
                r(4).Cells.Add(ce)
            End If
            ce = New TableCell
            ce.Text = Session("mLegends")(5, j)
            ce.BackColor = System.Drawing.Color.Gray
            r(4).Cells.Add(ce)

        Next j

        '   Fill in top of table
        '   --------------------
        grdHL.Rows.Clear()
        For i = 1 To 5
            grdHL.Rows.Add(If(r(i - 1), ""))
        Next
        'ttt Add prefix PLOT to Session("BilateralLastYear") , Session("BilateralFirstYear") 
        'Load Years
        Session("TotYears") = Session("PLOTBilateralLastYear") - Session("PLOTBilateralFirstYear") + 1 ' + 1
        ReDim Preserve r(Session("TotYears") + 5)
        'Row Header Texts
        j = 5
        For i = 0 To Session("PLOTBilateralLastYear") - Session("PLOTBilateralFirstYear")
            If j > Session("TotYears") + 5 Then Exit For
            r(j) = New TableRow
            ce = New TableCell
            ce.Text = CStr(Session("PLOTBilateralFirstYear") + i)
            ce.BackColor = System.Drawing.Color.Gray
            r(j).Cells.Add(ce)
            j += 1
        Next i
        'Add Years
        'Add Rows With First Col= Years
        For i = 1 + 5 To Session("TotYears") + 5
            grdHL.Rows.Add(r(i - 1))
        Next

        'End Row Header Texts
        'Load Growth Values
        'Add more Col of Value to rows above

        Dim di = DirectCast(Session("mPLOTDict"), Dictionary(Of String, Integer))
        For j = 1 To Session("TotalGraphs")
            For i = 1 To Session("TotYears")
                ce = New TableCell
                Dim yr As Integer = Session("PLOTBilateralFirstYear") + i - 1
                Dim fi = Findj(di, j, yr)
                If fi > -1 Then
                    If Session("mPLOT")(fi, j) Is Nothing OrElse Session("mPLOT")(fi, j) Is DBNull.Value Then
                        ce.Text = ""
                    Else
                        'jrs 2022/06/15
                        If Session("mPLOT")(fi, j) < 1 Then
                            ce.Text = Format(NoNullDec(Session("mPLOT")(fi, j)), "###,##0.00000")
                        Else
                            ce.Text = Format(NoNullDec(Session("mPLOT")(fi, j)), "###,##0.00")
                        End If
                    End If
                    'ce.Text = Format(NoNullDec(Session("mPLOT")(fi, j)), "###,##0.00")
                Else
                    ce.Text = ""
                    'ce.Text = "0.00"
                End If
                ce.HorizontalAlign = HorizontalAlign.Right
                r(i + 4).Cells.Add(ce)
            Next i
        Next j

        Dim dt As New DataTable()
        Dim dcol As New DataColumn()

        For j = 0 To Session("TotalGraphs")  'One for header, then for data
            dcol = New DataColumn(Str(j))
            dt.Columns.Add(dcol)
        Next j
        'ttt 2020-09-05 Add Header as ROW-------------------------------------
        Dim dr0 As DataRow = dt.NewRow
        For j = 0 To Session("TotalGraphs")
            'Will write below
            dr0(Str(j)) = ""
        Next
        dt.Rows.Add(dr0)
        dt.AcceptChanges()
        'ENDttt 2020-09-05 Add Header as ROW--------------------------------------

        For i = 5 To Session("TotYears") + 4
            Dim dr As DataRow = dt.NewRow
            For j = 0 To Session("TotalGraphs")
                If j = 0 Then
                    'ttt
                    dr(Str(j)) = CStr(Session("PLOTBilateralFirstYear") + (i - 5))
                Else
                    dr(Str(j)) = grdHL.Rows(i).Cells(j).Text
                End If
            Next
            dt.Rows.Add(dr)
            dt.AcceptChanges()
        Next i

        grdBilateral.DataSource = dt
        Session("DataForGrid") = dt
        grdBilateral.DataBind()
        'ttt 2020/08/31
        Me.txtTableCols.Value = (Session("TotalGraphs") + 1).ToString

        'ttt 2020-09-05
        'Header As Row
        For j = 1 To Session("TotalGraphs")
            grdBilateral.Rows(0).Cells(j).Text = grdHL.Rows(0).Cells(j).Text & "</br>" & grdHL.Rows(1).Cells(j).Text & "</br>" & grdHL.Rows(2).Cells(j).Text & "</br>" & grdHL.Rows(3).Cells(j).Text & "</br>" & grdHL.Rows(4).Cells(j).Text
        Next
        'Comm this For
        'For j = 0 To Session("TotalGraphs")
        '    If j = 0 Then
        '        'JAM 2012-05-13	Adding blank space because cell borders had a different look
        '        grdBilateral.HeaderRow.Cells(j).Text = Server.HtmlDecode("&nbsp;</br>") '""
        '        ' grdBilateral.Columns(j).HeaderText = "Year"
        '    Else
        '        grdBilateral.HeaderRow.Cells(j).Text = Server.HtmlDecode(grdHL.Rows(0).Cells(j).Text & "</br>" & grdHL.Rows(1).Cells(j).Text & "</br>" & grdHL.Rows(2).Cells(j).Text & "</br>" & grdHL.Rows(3).Cells(j).Text & "</br>" & grdHL.Rows(4).Cells(j).Text)
        '    End If
        'Next j
        grdBilateral.Rows(0).CssClass = "GridviewScrollHeader"
        'END ttt 2020-09-05
        'ttt 2020-09-05 5 , +4 -> 6, +5 (because one row is now header), grdHL.Rows(i)-> i-1
        For i = 6 To (Session("TotYears") + 5)
            'ttt 2020-09-05
            'grdBilateral.Rows(i - 5).CssClass = "GridviewScrollItem"
            'END ttt 2020-09-05
            grdBilateral.Rows(i - 5).Cells(0).BackColor = Color.LightGray
            'Bold for Row header
            grdBilateral.Rows(i - 5).Cells(0).Text = Server.HtmlDecode("<span style='color: #000000;'><b>" & grdHL.Rows(i - 1).Cells(0).Text & "</b></span>")

        Next i

        If Session("TotalGraphs") <= 3 Then
            If Session("TotalGraphs") = 3 Then
                grdBilateral.Width = Unit.Percentage(35)
            Else
                grdBilateral.Width = Unit.Percentage(25)
            End If
        End If
        pnlChartTable.Visible = True
        grdBilateral.Visible = True

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
                mnuContinue_Click()

            Case "Save to File"
                mnuSave_Click()
                '->tablecopy()
                'Case "Save to Clipboard"
                '    mnuSaveClip_Click()

            Case "Save Transpose"
                mnuSaveTranspose_Click()
            Case "Save in Panel Format"
                mnuSavePanel_Click()
            Case "Ratio Comparison (a/b)"
                mnuCompRatAB_Click()
            Case "Ratio Comparison (b/a)"
                mnuCompRatBA_Click()
            Case "Difference Comparison (a-b)"
                mnuCompDiffAB_Click()
            Case "Difference Comparison (b-a)"
                mnuCompDiffBA_Click()
            Case "Percent Comparison (a-b)/b*100"
                mnuCompPctAB_Click()
            Case "Percent Comparison (b-a)/a*100"
                mnuCompPctBA_Click()
            Case "Reset"
                mnuCompReset_Click()
            Case "Edit Gravity"
                mnuEditGravity_Click()
                'Case "Delete Gravity"
                '    DeleteGravityVar()
            Case "Edit Formula"
                mnuEditFormula_Click()
            Case "Save Formula"
                mnuSaveFormula_Click(bFromGravity:=False)
            Case "Rename Formula"
                mnuRenameFormula_Click()
            Case Else
                'jrs 2021/02/04 added mnuForecastWHist
                Dim mns = New String() {"mnuDyadic", "mnuMonadicforActor", "mnuMonadicforPartner", "mnuShowSavedFormulas", "mnuForecast", "mnuForecastWHist"}
                If mns.Contains(objEA.Item.ID) Then
                    SwitchSeriesType(objEA.Item.ID)
                End If
        End Select

        Select Case If(objEA.Item.ID, "")
            Case "mnuFormatLegends"
                mnuFormatLegends_Click()
            Case "mnuFormatLeg2Dim"
                Session("FormatLegends") = objEA.Item.ID
                mnuFormatLeg2Dim_Click()
            Case "mnuFormatLeg1Dim"
                Session("FormatLegends") = objEA.Item.ID
                mnuFormatLeg1Dim_Click()
            Case "mnuFormatLegDims"
                Session("FormatLegends") = objEA.Item.ID
                mnuFormatLegDims_Click()
            Case "mnuFormatLegNoDims"
                Session("FormatLegends") = objEA.Item.ID
                mnuFormatLegNoDims_Click()
            Case "mnuFormatSce"
                Session("FormatLegends") = objEA.Item.ID
                mnuFormatSce_Click()
            Case "mnuResetLegends"
                Session("FormatLegends") = Nothing
                mnuResetLegends_Click()
        End Select

        'If lblJS.Visible Then
        '    PackJScritpBuilder()
        'End If
    End Sub

    Private Sub mnuCompReset_Click()
        If Session("bComparisonOn") Then
            Session("bComparisonOn") = False
            ResetForComparison()
            cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
            cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
            Session("bCompRatAB") = False
            Session("bCompRatBA") = False
            Session("bCompDiffAB") = False
            Session("bCompDiffBA") = False
            Session("bCompPctAB") = False
            Session("bCompPctBA") = False
            If grdBilateral.Visible Then
                LoadGrid()
            ElseIf lblJS.Visible Then
                DrawGraph()
            End If
        End If

    End Sub

    Private Sub mnuCompDiffBA_Click()
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j) - Session("CompBackup")(i, 2 * j - 1)
            Next i

            LabelComparison(j, "-", False)

        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = True
        Session("bCompPctAB") = False
        Session("bCompPctBA") = False
        If grdBilateral.Visible Then
            LoadGrid()
        ElseIf lblJS.Visible Then
            DrawGraph()
        End If

    End Sub

    Private Sub mnuCompDiffAB_Click()
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If
        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j - 1) - Session("CompBackup")(i, 2 * j)
            Next i

            LabelComparison(j, "-", True)
        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = True
        Session("bCompDiffBA") = False
        Session("bCompPctAB") = False
        Session("bCompPctBA") = False
        If grdBilateral.Visible Then
            LoadGrid()
        ElseIf lblJS.Visible Then
            DrawGraph()
        End If

    End Sub
    Private Sub mnuCompPctAB_Click()
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If
        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                If Session("CompBackup")(i, 2 * j) <> 0 Then
                    Session("mPLOT")(i, j) = (Session("CompBackup")(i, 2 * j - 1) - Session("CompBackup")(i, 2 * j)) * 100 / Session("CompBackup")(i, 2 * j)
                Else
                    Session("mPLOT")(i, j) = Nothing
                End If
            Next i

            LabelComparison(j, "-", True)
        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False
        Session("bCompPctAB") = True
        Session("bCompPctBA") = False
        If grdBilateral.Visible Then
            LoadGrid()
        ElseIf lblJS.Visible Then
            DrawGraph()
        End If

    End Sub
    Private Sub mnuCompPctBA_Click()
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                If Session("CompBackup")(i, 2 * j - 1) <> 0 Then
                    Session("mPLOT")(i, j) = (Session("CompBackup")(i, 2 * j) - Session("CompBackup")(i, 2 * j - 1)) * 100 / Session("CompBackup")(i, 2 * j - 1)
                Else
                    Session("mPLOT")(i, j) = Nothing
                End If
            Next i

            LabelComparison(j, "-", False)

        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "checkbox.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False
        Session("bCompPctAB") = False
        Session("bCompPctBA") = True
        If grdBilateral.Visible Then
            LoadGrid()
        ElseIf lblJS.Visible Then
            DrawGraph()
        End If
    End Sub


    Private Sub mnuCompRatBA_Click()
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                If Session("CompBackup")(i, 2 * j - 1) <> 0 Then
                    Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j) / Session("CompBackup")(i, 2 * j - 1)
                Else
                    Session("mPLOT")(i, j) = Nothing
                End If
            Next i

            LabelComparison(j, "/", False)
        Next j

        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
        Session("bCompRatAB") = False
        Session("bCompRatBA") = True
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False
        Session("bCompPctAB") = False
        Session("bCompPctBA") = False
        If grdBilateral.Visible Then
            LoadGrid()
        ElseIf lblJS.Visible Then
            DrawGraph()
        End If

    End Sub


    Private Sub mnuCompRatAB_Click()
        If Not Session("bComparisonOn") Then
            'check if comparison is possible
            If Session("TotalGraphs") Mod 2 = 1 Then
                LoadMessage("Comparison is not possible with an odd number of variables loaded", Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

                Exit Sub
            End If
            'Save Original numbers
            SaveForComparison()
        End If

        'Compute Comparison
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                If Session("CompBackup")(i, 2 * j) <> 0 Then
                    Session("mPLOT")(i, j) = Session("CompBackup")(i, 2 * j - 1) / Session("CompBackup")(i, 2 * j)
                Else
                    Session("mPLOT")(i, j) = Nothing
                End If
            Next i

            LabelComparison(j, "/", True)
        Next j
        Session("bComparisonOn") = True
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "checkbox.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
        Session("bCompRatAB") = True
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False
        Session("bCompPctAB") = False
        Session("bCompPctBA") = False

        If grdBilateral.Visible Then
            LoadGrid()
        ElseIf lblJS.Visible Then
            DrawGraph()
        End If

    End Sub
    Private Sub LabelComparison(j As Integer, sOp As String, bOrder As Boolean)
        'j is the column to load
        'sOp is the operation to load - or /
        'bOrder is true if it's a sOp b, false if it's b sOp a
        Dim pos As Integer
        pos = -1
        Dim j1 As Integer, j2 As Integer
        If bOrder Then
            j1 = 2 * j - 1
            j2 = 2 * j
        Else
            j1 = 2 * j
            j2 = 2 * j - 1
        End If

        If Session("CompLabel")(1, j1) = Session("CompLabel")(1, j2) Then
            Session("mLegends")(1, j) = Session("CompLabel")(1, j2)
        Else
            pos = InStr(Session("CompLabel")(1, j1), "[")
            If pos = -1 Then
                pos = Len(Session("CompLabel")(1, j1))
            End If
            If Mid(Session("CompLabel")(1, j1), 1, pos) = Mid(Session("CompLabel")(1, j2), 1, pos) Then
                Session("mLegends")(1, j) = Replace(Session("CompLabel")(1, j1), "]", sOp & Mid(Session("CompLabel")(1, j2), pos + 1))
            Else
                Session("mLegends")(1, j) = Session("CompLabel")(1, j1) & sOp & Session("CompLabel")(1, j2)
            End If
        End If
        If Session("CompLabel")(2, j1) = Session("CompLabel")(2, j2) Then
            Session("mLegends")(2, j) = Session("CompLabel")(2, j2)
        Else
            If pos > -1 Then
                Session("mLegends")(2, j) = Mid(Session("CompLabel")(2, j2), 1, pos - 1)
            Else
                Session("mLegends")(2, j) = Session("CompLabel")(2, j1) & sOp & Session("CompLabel")(2, j2)
            End If
        End If
        If Session("CompLabel")(3, j1) = Session("CompLabel")(3, j2) Then
            Session("mLegends")(3, j) = Session("CompLabel")(3, j2)
        Else
            Session("mLegends")(3, j) = Session("CompLabel")(3, j1) & sOp & Session("CompLabel")(3, j2)
        End If
        If Session("CompLabel")(4, j1) = Session("CompLabel")(4, j2) Then
            Session("mLegends")(4, j) = Session("CompLabel")(4, j2)
        Else
            Session("mLegends")(4, j) = Session("CompLabel")(4, j1) & sOp & Session("CompLabel")(4, j2)
        End If
    End Sub

    Private Sub ResetForComparison()
        Session("TotalGraphs") = Session("TotalGraphs") * 2
        If Session("mPLOT") Is Nothing Then Return
        Dim i As Integer, j As Integer
        For j = 1 To UBound(Session("CompBackup"), 2) 'jrs 2009/02/24 COUNTE
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(i, j) = Session("CompBackup")(i, j)
            Next i
            Session("mLegends")(1, j) = Session("CompLabel")(1, j)
            Session("mLegends")(2, j) = Session("CompLabel")(2, j)
            Session("mLegends")(3, j) = Session("CompLabel")(3, j)
            Session("mLegends")(4, j) = Session("CompLabel")(4, j)
        Next j
    End Sub
    Private Sub SaveForComparison()
        Dim i As Integer, j As Integer
        For j = 1 To Session("TotalGraphs")
            For i = 0 To UBound(Session("mPLOT"), 1)
                Session("CompBackup")(i, j) = NoNullDec(Session("mPLOT")(i, j))
            Next i
            Session("CompLabel")(1, j) = Session("mLegends")(1, j)
            Session("CompLabel")(2, j) = Session("mLegends")(2, j)
            Session("CompLabel")(3, j) = Session("mLegends")(3, j)
            Session("CompLabel")(4, j) = Session("mLegends")(4, j)
        Next j
        Session("TotalGraphs") = Session("TotalGraphs") / 2
    End Sub

    Private Sub LoadDim1s(ByRef lst As ListBox)
        lst.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable = New DataTable
        Dim rr As Integer


        If (Session("bUseGroup") OrElse Session("bUseGroupDec")) Then

            'jam 2022/12/13
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                db = Session("IFs_MDB2Active")
                SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "", valtbl)
                rr = 0
                While Not valtbl.Rows.Count = rr
                    lst.Items.Add(valtbl.Rows(rr).Item("GroupName"))
                    rr = rr + 1
                End While

            Else
                'jam 2022/12/13
                db = Session("IFs_MDB2Active")

                vConnString = db & ";"

                vSQLiteCon = New SqliteConnection(vConnString)
                vSQLiteCon.Open()



                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                'rr = 0
                While (vSQLiteReader.Read())

                    lst.Items.Add(vSQLiteReader("GroupName"))
                    'rr = rr + 1



                End While
                vSQLiteCon.Close()


            End If

        ElseIf Session("bUseGList") Then

            'jam 2022/12/13
            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

                db = Session("IFs_MDB2Active")
                SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
                rr = 0
                While Not valtbl.Rows.Count = rr
                    lst.Items.Add(valtbl.Rows(rr).Item("Description"))
                    rr = rr + 1
                End While
            Else
                'jam 2022/12/13
                db = Session("IFs_MDB2Active")

                vConnString = db & ";"

                vSQLiteCon = New SqliteConnection(vConnString)
                vSQLiteCon.Open()



                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = "SELECT * FROM GListId ORDER BY Description"
                vSQLiteReader = vSQLiteCommand.ExecuteReader()

                'rr = 0
                While (vSQLiteReader.Read())

                    lst.Items.Add(vSQLiteReader("Description"))
                    'rr = rr + 1



                End While
                vSQLiteCon.Close()


            End If

        Else
            Dim i As Integer
            For i = 1 To MaxVal(1) - 1
                lst.Items.Add(GRPS$(1, i))
            Next i
        End If
        If lst.Items.Count > 0 Then
            lst.Items(0).Selected = True
        End If
    End Sub

    Protected Sub cmdDraw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDraw.Click
        If lstSelSeries.Items.Count = 0 OrElse lstSelDyads.Items.Count = 0 Then Return
        If Not grdBilateral.Visible Then
            'LoadSet()
            LoadDataBILA()
        Else
            grdBilateral.Visible = False
            cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False
        End If
        If Session("TotalGraphs") > 0 Then
            DrawGraph()
            pnlMain.Visible = False
            pnlSelecteds.Visible = False
            cmdDraw.Enabled = False

            If Not Session("bComparisonOn") Then
                Dim mCompBackup(UBound(Session("mPLOT"), 1), Session("TotalGraphs")) As Decimal
                Session("CompBackup") = mCompBackup
                Dim mCompLabel(4, Session("TotalGraphs")) As String
                Session("CompLabel") = mCompLabel
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
                cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"
                Session("bCompRatAB") = False
                Session("bCompRatBA") = False
                Session("bCompDiffAB") = False
                Session("bCompDiffBA") = False
                Session("bCompPctAB") = False
                Session("bCompPctBA") = False
            End If
        End If
        cmdTable.Enabled = True
        cmdSearch.Enabled = False
        cmdGravity.Enabled = False
        cmdComputation.Enabled = False
    End Sub
    Private Sub DrawGraph(Optional ByVal useFLegend As Boolean = False)
        pnlChartTable.Visible = True
        lblJS.Visible = True
        cswmWebMenu.Groups("mnuDisplay").ShowMenuBarItem = True
        PackJScritpBuilder(useFLegend)
    End Sub
    Private Sub PackJScritpBuilder(Optional ByVal useFLegend As Boolean = False)
        Dim graphtitle As String = ""
        Dim subtitle As String = ""
        'Loading Legends
        If Session("TotalGraphs") = 1 Then
            If useFLegend Then
                subtitle = Session("mFLegends")(4, 1) '4 is main Variable
                subtitle &= If(Session("mFLegends")(1, 1) Is Nothing OrElse Session("mFLegends")(1, 1).Trim = "", "", If(subtitle = "", "", ", ")) & Session("mFLegends")(1, 1)
                subtitle &= If(Session("mFLegends")(2, 1) Is Nothing OrElse Session("mFLegends")(2, 1).Trim = "", "", If(subtitle = "", "", ", ")) & Session("mFLegends")(2, 1)
                '3=Unit, no need
                'subtitle &= If(Session("mFLegends")(3, 1) Is Nothing OrElse Session("mFLegends")(3, 1).Trim = "", "", If(subtitle = "", "", ", ")) & Session("mFLegends")(3, 1)
                'subtitle &= If(Session("mFLegends")(4, 1) Is Nothing OrElse Session("mFLegends")(4, 1).Trim = "", "", If(subtitle = "", "", ", ")) & Session("mFLegends")(4, 1)
                subtitle &= If(Session("mFLegends")(5, 1) Is Nothing OrElse Session("mFLegends")(5, 1).Trim = "", "", If(subtitle = "", "", ", ")) & Session("mFLegends")(5, 1)
            Else
                subtitle = Session("mLegends")(4, 1) & If(Session("mLegends")(1, 1).Trim = "", "", ", ") &
               Session("mLegends")(1, 1) &
               If(Session("mLegends")(2, 1).Trim = "", "", If(Session("mLegends")(1, 1).Trim = "", "", ", ")) & Session("mLegends")(2, 1) &
                 If(Session("mLegends")(5, 1) Is Nothing OrElse Session("mLegends")(5, 1).Trim = "", "", ", ") & If(Session("mLegends")(5, 1), "")
                '3=Unit, no need  If(Session("mLegends")(3, 1).Trim = "", "", ", ") & Session("mLegends")(3, 1) &
            End If
        Else 'Many graphs
            Select Case If(Session("FormatLegends"), "")
                Case "mnuFormatLeg2Dim"
                    subtitle = Session("mLegends")(4, 1) & " from " & Session("mLegends")(1, 1)
                Case "mnuFormatLeg1Dim"
                    subtitle = Session("mLegends")(4, 1) & " to " & Session("mLegends")(2, 1)
                Case "mnuFormatLegDims"
                    subtitle = Session("mLegends")(4, 1)
                Case "mnuFormatLegNoDims"
                    subtitle = Session("mLegends")(4, 1)
                Case Else
                    subtitle = ""
            End Select
        End If

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr
        'jrs 2024/04/26 sJscript2 = sJscript2 & "            {" & vbCr

        sJscript2 = sJscript2 & "            var mycategories = [" & vbCr
        'ttt Add PLOT
        For l = 1 To Session("PLOTBilateralLastYear") - Session("PLOTBilateralFirstYear") + 1
            If l > 1 Then
                sJscript2 = sJscript2 & ","
            End If
            sJscript2 = sJscript2 & "'" & CStr(Session("PLOTBilateralFirstYear") + l - 1) & "'"
        Next
        sJscript2 = sJscript2 & "];" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        'jrs 2024/04/26 sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts({" & vbCr

        'jam 2025/03/18 New highcharts version libraries
        sJscript2 = sJscript2 & "exporting: {" & vbCr
        sJscript2 = sJscript2 & "fallbackToExportServer: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & graphtitle & "'," & vbCr
        sJscript2 = sJscript2 & "x: -20,  //center" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight:  'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If subtitle > "" Then
            sJscript2 = sJscript2 & "subtitle: {" & vbCr
            sJscript2 = sJscript2 & "text:       """ & subtitle & """," & vbCr
            sJscript2 = sJscript2 & "x: -20, //center" & vbCr
            sJscript2 = sJscript2 & "style: {" & vbCr
            sJscript2 = sJscript2 & "fontSize:   '14px'," & vbCr
            sJscript2 = sJscript2 & "fontWeight:  'bold'" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sAxisXLabel As String
        sAxisXLabel = "Year"

        Dim sAxisYLabel As String
        sAxisYLabel = If(Session("ChartUnits"), "") '"Million Years"

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & sAxisXLabel & "'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'Load Years
        '----------
        sJscript2 = sJscript2 & ", categories: mycategories"
        sJscript2 = sJscript2 & ", tickInterval: 5" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr

        sJscript2 = sJscript2 & "text:       '" & sAxisYLabel & "'" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr


        'jrs 2015/09/08
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr

        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        If Session("TotalGraphs") > 1 Then
            sJscript2 = sJscript2 & "legend: {" & vbCr
            sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
            sJscript2 = sJscript2 & "align:      'center'," & vbCr
            sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
            sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
            sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        Else
            sJscript2 = sJscript2 & "legend: {" & vbCr
            sJscript2 = sJscript2 & "enabled:     false" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
        End If
        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 1 To Session("TotalGraphs")
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToJSSeries(i, Session("mPLOT"))
        Next i

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

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

    End Sub

    Private Function PlotColToJSSeries(ByVal i As Integer, ByRef Vals As Object) As String
        Dim colIdx As Integer
        colIdx = i

        Dim sJscript As String = ""
        sJscript = sJscript & "{name:""" & getSeriesName(i) & """, color: " & getColor(i) & ", data:[" & vbCr

        Dim j As Integer
        Dim bFirst As Boolean = True
        Dim di = DirectCast(Session("mPLOTDict"), Dictionary(Of String, Integer))

        Dim newSeries As New Dictionary(Of Integer, Nullable(Of Decimal))

        For j = 1 To Session("PLOTBilateralLastYear") - Session("PLOTBilateralFirstYear") + 1
            Dim yr As Integer = Session("PLOTBilateralFirstYear") + j - 1
            Dim fj = Findj(di, i, yr)
            If fj <> -1 Then
                'newSeries.Add(yr, Math.Round(NoNullDec(Vals(fj, i)), 2))
                'newSeries.Add(yr, If(Vals(fj, i) Is Nothing OrElse Vals(fj, i) Is DBNull.Value, Nothing, Math.Round(NoNullDec(Vals(fj, i)), 2)))
                If Vals(fj, i) Is Nothing OrElse Vals(fj, i) Is DBNull.Value Then
                    newSeries.Add(yr, Nothing)
                Else
                    newSeries.Add(yr, Math.Round(NoNullDec(Vals(fj, i)), 2))
                End If
            Else
                newSeries.Add(yr, 0)
            End If
        Next
        Dim sortedDict = (From entry In newSeries Order By entry.Key Ascending).ToDictionary(Function(pair) pair.Key, Function(pair) pair.Value)
        Dim p As Integer = 1
        For Each k As Integer In sortedDict.Keys
            'sJscript = sJscript & If(sortedDict(k) = 0, "null", CStr(sortedDict(k))) & If(p = sortedDict.Keys.Count, "", ",")
            'sJscript = sJscript & If(sortedDict(k) = 0, "0", CStr(sortedDict(k))) & If(p = sortedDict.Keys.Count, "", ",")
            sJscript = sJscript & If(sortedDict(k) Is Nothing, "null", CStr(sortedDict(k))) & If(p = sortedDict.Keys.Count, "", ",")
            p += 1
        Next
        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Protected Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        If grdBilateral.Visible Then
            grdBilateral.Visible = False
            pnlChartTable.Visible = False
            cmdSearch.Enabled = True
            cmdGravity.Enabled = True
            cmdComputation.Enabled = True
            'mnuComp.Visible = False
            'With TblDispOpt
            '    Do While .Size > 0
            '        Select Case .Top
            '            Case "Comparison"
            '                mnuReset_Click
            '            Case Else
            '                MsgBox "There's a problem with the implementation of " & .Top & ". Please reload the variables.", vbCritical, "Table Display"
            '        Exit Do
            '        End Select
            '    Loop
            'End With
        ElseIf lblJS.Visible Then
            lblJS.Visible = False
            pnlChartTable.Visible = False
            cmdSearch.Enabled = True
            cmdGravity.Enabled = True
            cmdComputation.Enabled = True
        Else
            lstSeries.ClearSelection()
            'jrs 2024/08/28
            lstVarNameMonadyc.ClearSelection()

            lstDim1.ClearSelection()
            lstDim1p.ClearSelection()
            lstSelSeries.Items.Clear()
            lstSelDyads.Items.Clear()
            Session.Remove("sAggRulesSelected")
            Session.Remove("bDyadicSeries")
            Session.Remove("bMonadicActorSeries")
            Session("bAddGravity") = False
            Session("sGravityDescription") = ""
        End If

        If Not Session("mnuSQLite") Is Nothing Then
            If Session("mnuSQLite") AndAlso (Not vSQLiteCon Is Nothing) Then
                vSQLiteCon.Close()
            End If
        End If
        '-----
        cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False

        '-------------
        Session("bComparisonOn") = False
        cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = False

        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").IconImage = "blank.gif"
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").IconImage = "blank.gif"

        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatAB").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompRatBA").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffAB").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompDiffBA").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctAB").Enabled = False
        cswmWebMenu.Groups("mnuComp").Items("mnuCompPctBA").Enabled = False

        Session("bCompRatAB") = False
        Session("bCompRatBA") = False
        Session("bCompDiffAB") = False
        Session("bCompDiffBA") = False
        Session("bCompPctAB") = False
        Session("bCompPctBA") = False

        'If lblJS.Visible OrElse grdBilateral.Visible Then
        pnlChartTable.Visible = False
        lblJS.Visible = False
        grdBilateral.Visible = False
        cswmWebMenu.Groups("mnuSaveHdr").ShowMenuBarItem = False
        cmdDraw.Enabled = True
        cmdTable.Enabled = True
        pnlMain.Visible = True
        pnlSelecteds.Visible = True
        cswmWebMenu.Groups("mnuDisplay").ShowMenuBarItem = False
        Session("TotalGraphs") = 0
        'Else
        '    Session.Remove("mPLOT")
        '    Session.Remove("mLegends")
        '    If Session("DisplayParent") = Nothing Then
        '        Session("DisplayParent") = "frm_MainMenu.aspx"
        '    End If

        '    Support.FormManager.Show(Me, Session("DisplayParent"))
        'End If

        'pnlButtons.Visible = True
    End Sub
    Private Sub mnuSave_Click()
        'ttt 2020-06-08
        If (Not grdBilateral.Visible) OrElse (Session("DataForGrid") Is Nothing) OrElse (DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0) Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)
        Response.Clear()
        Response.ContentType = "application/octet-stream"

        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        '   Write Table to .CSV File
        '   ------------------------
        'Headers
        'ttt 2020-09-05 No header
        'For i = 0 To 3
        '    Dim aVal = ""
        '    For j = 0 To dt.Columns.Count - 1
        '        Dim sVal = grdBilateral.HeaderRow.Cells(j).Text
        '        If j = 0 Then
        '            aVal = "".PadLeft(15) & ","
        '        Else
        '            sVal = sVal.Replace("</br>", "|")
        '            aVal &= sVal.Split("|")(i)
        '            aVal &= If(j = dt.Columns.Count - 1, "", ",")
        '        End If
        '    Next
        '    Context.Response.Write(aVal)
        '    Context.Response.Write(Environment.NewLine)
        'Next
        'END ttt 2020-09-05 No header
        '------------------------------------------
        'ttt 2020-09-05 First Row as header
        For i = 0 To 3
            Dim aVal = ""
            For j = 0 To dt.Columns.Count - 1
                Dim sVal = grdBilateral.Rows(0).Cells(j).Text
                If j = 0 Then
                    aVal = "".PadLeft(15) & ","
                Else
                    sVal = sVal.Replace("</br>", "|")
                    'jrs 2025/06/11
                    sVal = sVal.Replace(",", ";")

                    aVal &= sVal.Split("|")(i)
                    aVal &= If(j = dt.Columns.Count - 1, "", ",")
                End If
            Next
            Context.Response.Write(aVal)
            Context.Response.Write(Environment.NewLine)
        Next
        'END ttt 2020-09-05 First Row as header
        'Data
        For r = 0 To dt.Rows.Count - 1
            For c = 0 To dt.Columns.Count - 1
                'ttt -> NoNullDecStr
                Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), NoNullDecStr(dt.Rows(r).Item(c)).PadLeft(15))
                'Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), CDec(dt.Rows(r).Item(c)).ToString.PadLeft(15))
                sVal &= If(c = dt.Columns.Count - 1, "", ",")
                Context.Response.Write(sVal)
            Next
            Context.Response.Write(Environment.NewLine)
        Next

        Response.End()

    End Sub
    Private Sub mnuSaveClip_ClickNOUSE()
        If (Not grdBilateral.Visible) OrElse (Session("DataForGrid") Is Nothing) OrElse (DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0) Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)
        Response.Clear()
        Response.ContentType = "application/octet-stream"

        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")

        '   Write Table to Clipboard
        '   ------------------------
        Dim sb As StringBuilder = New StringBuilder()
        'Headers
        For i = 0 To 3
            Dim aVal = ""
            For j = 0 To dt.Columns.Count - 1
                Dim sVal = grdBilateral.HeaderRow.Cells(j).Text
                If j = 0 Then
                    aVal = "".PadLeft(15) & ","
                Else
                    sVal = sVal.Replace("</br>", "|")
                    aVal &= sVal.Split("|")(i)
                    aVal &= If(j = dt.Columns.Count - 1, "", ",")
                End If
            Next
            Context.Response.Write(aVal)
            Context.Response.Write(Environment.NewLine)
        Next

        For r = 0 To dt.Rows.Count - 1
            For c = 0 To dt.Columns.Count - 1
                Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), CDec(dt.Rows(r).Item(c)).ToString.PadLeft(15))
                sVal &= If(c = dt.Columns.Count - 1, "", ",")
                Context.Response.Write(sVal)
            Next
            Context.Response.Write(Environment.NewLine)
        Next



    End Sub
    Private Sub mnuSaveClip_Click()
        Dim csname1 As String = "PopupScript"
        Dim cstype As Type = Me.[GetType]()
        Dim cs As ClientScriptManager = Page.ClientScript

        If Session("DataForGrid") Is Nothing OrElse DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0 Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)

        '   Write Table to Clipboard
        '   ------------------------
        Dim sb As StringBuilder = New StringBuilder()
        'Headers
        For i = 0 To 3
            Dim aVal = ""
            For j = 0 To dt.Columns.Count - 1
                Dim sVal = grdBilateral.HeaderRow.Cells(j).Text
                If j = 0 Then
                    aVal = "".PadLeft(15) & "\t"
                Else
                    sVal = sVal.Replace("</br>", "|")
                    aVal &= sVal.Split("|")(i)
                    aVal &= If(j = dt.Columns.Count - 1, "", "\t")
                End If
            Next
            sb.Append(aVal)
            sb.Append("\r\n")
        Next

        For r = 0 To dt.Rows.Count - 1
            For c = 0 To dt.Columns.Count - 1
                'Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), NoNullDec(dt.Rows(r).Item(c)).ToString.PadLeft(15))
                Dim sVal = If(dt.Rows(r).Item(c) Is Nothing, "".PadLeft(15), NoNullDecStr(dt.Rows(r).Item(c)).PadLeft(15))
                sVal &= If(c = dt.Columns.Count - 1, "", "\t")
                sb.Append(sVal)
            Next
            sb.Append("\r\n")
        Next

        Dim script As String = "<script type=text/javascript>gcopy('" & sb.ToString & "');</script>"

        If Not cs.IsStartupScriptRegistered(cstype, csname1) Then
            cs.RegisterStartupScript(cstype, csname1, script)
        End If

    End Sub
    Private Sub mnuSaveTranspose_Click()
        'ttt 2020-09-05 HeaderRow=> Rows(0)

        'ttt 2020-06-08
        If (Not grdBilateral.Visible) OrElse (Session("DataForGrid") Is Nothing) OrElse (DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0) Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)
        Response.Clear()
        Response.ContentType = "application/octet-stream"

        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        '   Write Table to .CSV File
        '   ------------------------
        '.......Mexico
        '.......US
        '1995   22
        '1996   33
        '=>
        '..........1995...1996
        'Mexico-US 22      33
        '=>
        'Series-Actor-Partner-Unit-1995
        'Expor-US-...
        'Check Max Col For case ForeCast =5
        Dim MaxCol As Integer = 4
        For j = 0 To dt.Columns.Count - 2
            'jrs 2025/06/11 Dim sVal = grdBilateral.Rows(0).Cells(j + 1).Text.Replace("</br>", ",").Trim
            Dim sVal = grdBilateral.Rows(0).Cells(j + 1).Text.Replace(",", ";").Trim
            sVal = sVal.Replace("</br>", ",").Trim
            MaxCol = Math.Max(MaxCol, sVal.Split(",").Count)
        Next

        Dim sr As StringBuilder = New StringBuilder("Series,Actor,Partner,Unit," & If(MaxCol = 4, "", "File,"))
        'New Headers
        For k = 0 To dt.Rows.Count - 1
            sr.Append(dt.Rows(k).Item(0).ToString & If(k = dt.Rows.Count - 1, "", ","))
        Next
        'New Rows
        For j = 0 To dt.Columns.Count - 2
            'jrs 2025/06/11 Dim sVal = grdBilateral.Rows(0).Cells(j + 1).Text.Replace("</br>", ",").Trim
            Dim sVal = grdBilateral.Rows(0).Cells(j + 1).Text.Replace(",", ";").Trim
            sVal = sVal.Replace("</br>", ",").Trim
            If sVal.Split(",").Count = 5 Then
                sVal &= ","
            ElseIf sVal.Split(",").Count = 4 AndAlso MaxCol = 5 Then
                sVal &= ",,"
            End If
            'If sVal.EndsWith(",") Then
            '    sVal = sVal.Substring(0, sVal.Length - 1)
            'End If
            sr.AppendLine.Append(sVal) ' & ",")
            For k = 0 To dt.Rows.Count - 1
                Dim cVal = If(dt.Rows(k).Item(j + 1) Is Nothing, "".PadLeft(15), NoNullDecStr(dt.Rows(k).Item(j + 1)).PadLeft(15))
                sr.Append(cVal & If(k = dt.Rows.Count - 1, "", ","))
            Next

        Next
        Context.Response.Write(sr.ToString)
        Response.End()

    End Sub
    Private Sub mnuSaveTranspose_ClickOLD()
        Dim csname1 As String = "PopupScript2"
        Dim cstype As Type = Me.[GetType]()
        Dim cs As ClientScriptManager = Page.ClientScript

        If Session("DataForGrid") Is Nothing OrElse DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0 Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)

        '   Write Table to Clipboard
        '   ------------------------
        '.......Mexico
        '.......US
        '1995   22
        '1996   33
        '=>
        '..........1995...1996
        'Mexico-US 22      33
        Dim sr As StringBuilder = New StringBuilder("SERIES" & "\t")
        'New Headers
        For k = 0 To dt.Rows.Count - 1
            sr.Append(dt.Rows(k).Item(0).ToString & If(k = dt.Rows.Count - 1, "", "\t"))
        Next
        'New Rows
        For j = 0 To dt.Columns.Count - 2
            Dim sVal = grdBilateral.HeaderRow.Cells(j + 1).Text.Replace("</br>", " - ").Trim
            If sVal.EndsWith("-") Then
                sVal = sVal.Substring(0, sVal.Length - 1)
            End If
            sr.Append("\r\n").Append(sVal & "\t")
            For k = 0 To dt.Rows.Count - 1
                sr.Append(dt.Rows(k).Item(j + 1).ToString & If(k = dt.Rows.Count - 1, "", "\t"))
            Next

        Next

        Dim script As String = "<script type=text/javascript>gcopy('" & sr.ToString & "');</script>"

        If Not cs.IsStartupScriptRegistered(cstype, csname1) Then
            cs.RegisterStartupScript(cstype, csname1, script)
        End If

    End Sub
    Private Sub mnuSavePanel_Click()
        'ttt 2020-09-05 HeaderRow= Rows(0)

        'The Panel Format is basically lots of rows with columns 
        'as: Dyad, Year, Variables1, Variable2, Variable3, etc.
        'ttt 2020-06-08
        If (Not grdBilateral.Visible) OrElse (Session("DataForGrid") Is Nothing) OrElse (DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0) Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)
        Response.Clear()
        Response.ContentType = "application/octet-stream"

        Response.AddHeader("Content-Disposition", "attachment; filename=ifs.csv")
        '   Write Table to Clipboard
        '   ------------------------
        '.......Export    Import
        '.......Mexico    Mexico
        '.......US        UK
        '1995   22        55
        '1996   33        66
        '=>
        '...Dyad,   Year, Export(+Sce), Import(+Sce)
        'Mexico-US 1995   22    
        'Mexico-US 1996   33
        'Mexico-UK 1995         55   
        'Mexico-UK 1996         66

        'Headers
        Dim hdrs As List(Of String) = New String() {"Dyad", "Year"}.ToList
        Dim dys As List(Of String) = New List(Of String)
        Dim dicCols As New Dictionary(Of String, Integer)
        For j = 1 To dt.Columns.Count - 1
            Dim Vals = grdBilateral.Rows(0).Cells(j).Text.Replace("</br>", "|").Split("|")
            Dim var = Vals(0) & If(Vals.Count = 5 AndAlso Vals(4).Trim <> "", "-" & Vals(4).Trim, "")
            If Not (hdrs.Contains(var)) Then
                hdrs.Add(var)
            End If
            Dim dy = Vals(1) & " - " & Vals(2)
            If Not (dys.Contains(dy)) Then
                dys.Add(dy)
            End If
            dicCols.Add(dy & "|" & var, j)
        Next
        Dim sb As StringBuilder = New StringBuilder()
        'headers
        For j = 0 To hdrs.Count - 1
            sb.Append(hdrs(j) & If(j = hdrs.Count - 1, "", ","))
        Next
        sb.AppendLine()
        'rows
        For Each dyad As String In dys
            For Each r As DataRow In dt.Rows
                'jrs 2025/01/02 replacing , with ; in dyad to improve readibility in csv files
                sb.Append(dyad.Replace(",", ";") & "," & r.Item(0).ToString) 'year
                For j = 2 To hdrs.Count - 1
                    Dim val As String = If(r.Item(FindCol(dicCols, dyad, hdrs(j))) Is Nothing, "".PadLeft(15), NoNullDecStr(r.Item(FindCol(dicCols, dyad, hdrs(j)))).PadLeft(15))
                    sb.Append("," & val) 'values
                Next
                sb.AppendLine()
            Next
        Next
        Context.Response.Write(sb.ToString)
        Response.End()

    End Sub
    Private Sub mnuSavePanel_ClickOLD()
        'The Panel Format is basically lots of rows with columns 
        'as: Dyad, Year, Variables1, Variable2, Variable3, etc.
        Dim csname1 As String = "PopupScript3"
        Dim cstype As Type = Me.[GetType]()
        Dim cs As ClientScriptManager = Page.ClientScript

        If Session("DataForGrid") Is Nothing OrElse DirectCast(Session("DataForGrid"), DataTable).Rows.Count = 0 Then
            Return
        End If
        Dim dt = DirectCast(Session("DataForGrid"), DataTable)

        '   Write Table to Clipboard
        '   ------------------------
        '.......Export    Import
        '.......Mexico    Mexico
        '.......US        UK
        '1995   22        55
        '1996   33        66
        '=>
        '...Dyad,   Year, Export(+Sce), Import(+Sce)
        'Mexico-US 1995   22    
        'Mexico-US 1996   33
        'Mexico-UK 1995         55   
        'Mexico-UK 1996         66

        'Headers
        Dim hdrs As List(Of String) = New String() {"Dyad", "Year"}.ToList
        Dim dys As List(Of String) = New List(Of String)
        Dim dicCols As New Dictionary(Of String, Integer)
        For j = 1 To dt.Columns.Count - 1
            Dim Vals = grdBilateral.HeaderRow.Cells(j).Text.Replace("</br>", "|").Split("|")
            Dim var = Vals(0)
            If Not (hdrs.Contains(var)) Then
                hdrs.Add(var)
            End If
            Dim dy = Vals(1) & " - " & Vals(2)
            If Not (dys.Contains(dy)) Then
                dys.Add(dy)
            End If
            dicCols.Add(dy & "|" & var, j)
        Next
        Dim sb As StringBuilder = New StringBuilder()
        'headers
        For j = 0 To hdrs.Count - 1
            sb.Append(hdrs(j) & If(j = hdrs.Count - 1, "", "\t"))
        Next
        sb.Append("\r\n")
        'rows
        For Each dyad As String In dys
            For Each r As DataRow In dt.Rows
                sb.Append(dyad & "\t" & r.Item(0).ToString) 'year
                For j = 2 To hdrs.Count - 1
                    sb.Append("\t" & r.Item(FindCol(dicCols, dyad, hdrs(j))).ToString) 'variables
                Next
                sb.Append("\r\n")
            Next
        Next
        Dim script As String = "<script type=text/javascript>gcopy('" & sb.ToString & "');</script>"

        If Not cs.IsStartupScriptRegistered(cstype, csname1) Then
            cs.RegisterStartupScript(cstype, csname1, script)
        End If

    End Sub
    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String
        Select Case If(Session("FormatLegends"), "")
            Case "mnuFormatLeg2Dim"
                Legend = Session("mLegends")(2, ii)
            Case "mnuFormatLeg1Dim"
                Legend = Session("mLegends")(1, ii)
            Case "mnuFormatLegDims"
                Legend = Session("mLegends")(1, ii) & ", " & Session("mLegends")(2, ii)
            Case "mnuFormatLegNoDims"
                Legend = Session("mLegends")(4, ii)
            Case Else
                Legend = Session("mLegends")(4, ii) & ", " & Session("mLegends")(1, ii) & ", " &
                   Session("mLegends")(2, ii) ' & ", " & Session("mLegends")(3, ii) UNIT-NO-NEED
        End Select
        If lstFiles.Visible Then
            'Forecast
            Legend &= ", " & Session("mLegends")(5, ii)
        End If


        'Legend = Session("mLegends")(1, ii) & ", " & Session("mLegends")(2, ii) & ", " &
        '            Session("mLegends")(3, ii) & ", " & Session("mLegends")(4, ii)

        getSeriesName = Legend
    End Function

    Private Function getColor(ByVal i As Integer) As String

        Dim sColor As String = ""

        'jrs 2020/07/08 If i < 9 Then

        Select Case i

            Case 1

                sColor = """Lime"""

            Case 2

                sColor = """Blue"""

            Case 3

                sColor = """Red"""

            Case 4 'blue violet

                sColor = """RGB(138,43,226)"""

            Case 5 'aqua

                sColor = """RGB(0,255,255)"""

            Case 6 'deep pink

                sColor = """RGB(255,20,147)"""

            Case 7 'medium spring green

                sColor = """RGB(0,250,154)"""

            Case Else

                'jrs 2020/06/07

                Dim rd As New Random(i)

                Dim r1, r2, r3 As Short

                r1 = CShort(rd.NextDouble * 1000)

                r2 = CShort(rd.NextDouble * 1000)

                r3 = CShort(rd.NextDouble * 1000)

                sColor = """RGB(" & (r1 Mod 256).ToString & "," & (r2 Mod 256).ToString & "," & (r3 Mod 256).ToString & ")"""

        End Select

        'jrs 2020/07/08 End If

        getColor = sColor

    End Function
    Private Sub DeleteCol(col As Integer)
        Dim j As Integer, i As Integer
        For j = col To Session("TotalGraphs") - 1
            For i = 0 To 4
                Session("mLegends")(i, j) = Session("mLegends")(i, j + 1)
            Next i
            For i = 1 To Session("TotYears")
                Session("mPLOT")(i, j) = Session("mPLOT")(i, j + 1)
            Next i
        Next j
        'Delete last column
        For i = 0 To 4
            Session("mLegends")(i, Session("TotalGraphs")) = ""
        Next i
        For i = 1 To Session("TotYears")
            Session("mPLOT")(i, j) = 0
        Next i
        Session("TotalGraphs") = Session("TotalGraphs") - 1
    End Sub
    Public Function NoNullDec(o As Object) As Decimal
        'ttt-Temp function to adjust values to viewable area
        'Please edit or delete this function
        Try
            Dim res = CDec(o)
            Return res ' If(Math.Abs(res) < 10, res, res Mod 10)
        Catch ex As Exception

        End Try
        Return 0
    End Function
    Private Sub LoadSeries()
        lstSeries.Items.Clear()
        Dim db As String
        Dim valtbl As DataTable = New DataTable
        Dim sUnits As String = ""

        'jam 2022/12/04
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            db = Session("PutFdrvRun") & "DataDictDyadic.mdb" '\RUNFILES\Data... Not copied to there!
            SetDataTablefromAccess(db, "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable", "", valtbl)
            ReDim Session("sAggRules")(valtbl.Rows.Count)
            For Each r As DataRow In valtbl.Rows
                If r.Item("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = r.Item("Units")
                End If
                If r.Item("Aggregation") Is DBNull.Value Then
                    Session("sAggRules")(lstSeries.Items.Count) = ""
                Else
                    Session("sAggRules")(lstSeries.Items.Count) = r.Item("Aggregation")
                End If

                lstSeries.Items.Add(r.Item("Variable") & " - " & r.Item("Definition") & " - " & sUnits)

            Next

        Else 'jam 2022/12/04
            db = Session("PutFdrvRun") & "DataDictDyadic.db"

            vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDictDyadic.db;"

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()



            vSQLiteCommand = vSQLiteCon.CreateCommand()
            'jrs 2022/12/20
            vSQLiteCommand.CommandText = "SELECT COUNT(*) FROM DataDict"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()
            'jrs 2024/04/25
            vSQLiteReader.Read()

            ReDim Session("sAggRules")(CInt(vSQLiteReader(0)))
            vSQLiteReader.Close()

            vSQLiteCommand.CommandText = "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            While (vSQLiteReader.Read())



                If vSQLiteReader("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = vSQLiteReader("Units")
                End If
                If vSQLiteReader("Aggregation") Is DBNull.Value Then
                    Session("sAggRules")(lstSeries.Items.Count) = ""
                Else
                    Session("sAggRules")(lstSeries.Items.Count) = vSQLiteReader("Aggregation")
                End If

                lstSeries.Items.Add(vSQLiteReader("Variable") & " - " & vSQLiteReader("Definition") & " - " & sUnits)



            End While
            vSQLiteCon.Close()

        End If


        LoadformulaVars(db)
        If lstSeries.Items.Count > 0 Then
            lstSeries.Items(0).Selected = True
        End If
    End Sub

    Private Sub LoadOtherSeries()
        lstVarNameMonadyc.Items.Clear()

        'jam 2022/12/18
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then

            Dim dbDD = Session("PutFdrvRun") & "DataDict.mdb"
            Dim rsDD As DataTable = New DataTable
            SetDataTablefromAccess(dbDD, "SELECT Variable, Definition, Units, Aggregation FROM DataDict WHERE Series = ""Yes"" ORDER BY Variable", "", rsDD)
            ReDim Session("sAggRulesMon")(rsDD.Rows.Count)
            Dim sUnits = ""
            For Each r As DataRow In rsDD.Rows
                If r.Item("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = r.Item("Units")
                End If
                If r.Item("Aggregation") Is DBNull.Value Then
                    Session("sAggRulesMon")(lstVarNameMonadyc.Items.Count) = ""
                Else
                    Session("sAggRulesMon")(lstVarNameMonadyc.Items.Count) = r.Item("Aggregation")
                End If
                lstVarNameMonadyc.Items.Add(r.Item("Variable") & " - " & r.Item("Definition") & " - " & sUnits)
            Next
            '-----------
            lstVarNameForecast.Items.Clear()
            dbDD = Session("PutFdrvRun") & "DataDictDyadicForecast.mdb"
            SetDataTablefromAccess(dbDD, "SELECT Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable", "", rsDD)
            ReDim Session("sAggRulesFor")(rsDD.Rows.Count)
            For Each r As DataRow In rsDD.Rows
                If r.Item("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = r.Item("Units")
                End If
                If r.Item("Aggregation") Is DBNull.Value Then
                    Session("sAggRulesFor")(lstVarNameForecast.Items.Count) = ""
                Else
                    Session("sAggRulesFor")(lstVarNameForecast.Items.Count) = r.Item("Aggregation")
                End If
                lstVarNameForecast.Items.Add(r.Item("Variable") & " - " & r.Item("Definition") & " - " & sUnits)
            Next
            'jrs 2021/02/04
            lstVarNameForecastWHist.Items.Clear()
            SetDataTablefromAccess(dbDD, "SELECT Variable, Definition, Units, Aggregation, [Historic Analog] FROM DataDict WHERE [Historic Analog]>'' ORDER BY Variable", "", rsDD)
            ReDim Session("sAggRulesForH")(rsDD.Rows.Count)
            ReDim Session("sHistAnalogs")(rsDD.Rows.Count)
            For Each r As DataRow In rsDD.Rows
                If r.Item("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = r.Item("Units")
                End If
                If r.Item("Aggregation") Is DBNull.Value Then
                    Session("sAggRulesForH")(lstVarNameForecastWHist.Items.Count) = ""
                Else
                    Session("sAggRulesForH")(lstVarNameForecastWHist.Items.Count) = r.Item("Aggregation")
                End If
                Session("sHistAnalogs")(lstVarNameForecastWHist.Items.Count) = r.Item("Historic Analog")
                lstVarNameForecastWHist.Items.Add(r.Item("Variable") & " - " & r.Item("Definition") & " - " & sUnits)
            Next

        Else

            Dim dbDD = Session("PutFdrvRun") & "DataDict.db"
            Dim vcountVariables As Integer

            vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDict.db;"

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Count(Variable) As rCountVariables FROM DataDict WHERE Series = ""Yes"""
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            vcountVariables = 0
            While (vSQLiteReader.Read())
                vcountVariables = vSQLiteReader("rCountVariables")

            End While
            vSQLiteCon.Close()



            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT Variable, Definition, Units, Aggregation FROM DataDict WHERE Series = ""Yes"" ORDER BY Variable"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            ReDim Session("sAggRulesMon")(vcountVariables)
            Dim sUnits = ""

            While (vSQLiteReader.Read())



                If vSQLiteReader("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = vSQLiteReader("Units")
                End If
                If vSQLiteReader("Aggregation") Is DBNull.Value Then
                    Session("sAggRulesMon")(lstVarNameMonadyc.Items.Count) = ""
                Else
                    Session("sAggRulesMon")(lstVarNameMonadyc.Items.Count) = vSQLiteReader("Aggregation")
                End If

                'jrs 2024/08/28 lstSeries.Items.Add(vSQLiteReader("Variable") & " - " & vSQLiteReader("Definition") & " - " & sUnits)
                lstVarNameMonadyc.Items.Add(vSQLiteReader("Variable") & " - " & vSQLiteReader("Definition") & " - " & sUnits)

            End While
            vSQLiteCon.Close()



            '-----------
            lstVarNameForecast.Items.Clear()

            dbDD = Session("PutFdrvRun") & "DataDictDyadicForecast.db"

            vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDictDyadicForecast.db;"

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "Select Count(Variable) As rCountVariables FROM DataDict ORDER BY Variable"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            vcountVariables = 0
            While (vSQLiteReader.Read())
                vcountVariables = vSQLiteReader("rCountVariables")

            End While
            vSQLiteCon.Close()


            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "Select Variable, Definition, Units, Aggregation FROM DataDict ORDER BY Variable"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            ReDim Session("sAggRulesFor")(vcountVariables)

            While (vSQLiteReader.Read())



                If vSQLiteReader("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = vSQLiteReader("Units")
                End If
                If vSQLiteReader("Aggregation") Is DBNull.Value Then
                    Session("sAggRulesFor")(lstVarNameForecast.Items.Count) = ""
                Else
                    Session("sAggRulesFor")(lstVarNameForecast.Items.Count) = vSQLiteReader("Aggregation")
                End If

                lstVarNameForecast.Items.Add(vSQLiteReader("Variable") & " - " & vSQLiteReader("Definition") & " - " & sUnits)


            End While
            vSQLiteCon.Close()



            '-----------------------------
            'jrs 2021/02/04
            lstVarNameForecastWHist.Items.Clear()

            dbDD = Session("PutFdrvRun") & "DataDictDyadicForecast.db"

            vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDictDyadicForecast.db;"

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "Select Count(Variable) As rCountVariables FROM DataDict WHERE [Historic Analog]>''"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            vcountVariables = 0
            While (vSQLiteReader.Read())
                vcountVariables = vSQLiteReader("rCountVariables")

            End While
            vSQLiteCon.Close()

            ReDim Session("sAggRulesForH")(vcountVariables)
            ReDim Session("sHistAnalogs")(vcountVariables)

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()



            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "Select Variable, Definition, Units, Aggregation, [Historic Analog] FROM DataDict WHERE [Historic Analog]>''"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()


            While (vSQLiteReader.Read())



                If vSQLiteReader("Units") Is DBNull.Value Then
                    sUnits = ""
                Else
                    sUnits = vSQLiteReader("Units")
                End If
                If vSQLiteReader("Aggregation") Is DBNull.Value Then
                    Session("sAggRulesForH")(lstVarNameForecastWHist.Items.Count) = ""
                Else
                    Session("sAggRulesForH")(lstVarNameForecastWHist.Items.Count) = vSQLiteReader("Aggregation")
                End If
                'jrs 2022/12/20
                Session("sHistAnalogs")(lstVarNameForecastWHist.Items.Count) = vSQLiteReader("Historic Analog")

                lstVarNameForecastWHist.Items.Add(vSQLiteReader("Variable") & " - " & vSQLiteReader("Definition") & " - " & sUnits)

            End While
            vSQLiteCon.Close()





        End If

    End Sub
    Protected Sub optReg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optReg.CheckedChanged
        If optReg.Checked Then
            'If txtCalledWhenLoaded.Value = "Y" Then
            '    txtCalledWhenLoaded.Value = "N"
            '    Return
            'End If
            'txtClickedOption.Value = "Y"
            Session("optGeographyId") = 1
            Session("bUseReg") = True
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            'jrs 2016/09/30
            'Session("bUseCtryAndGroup") = False
            'LoadDim1s(lstDim1)
            'LoadDim1s(lstDim1p)
            LoadCountries()
        End If
    End Sub

    Protected Sub optGList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGList.CheckedChanged
        If optGList.Checked Then
            'If txtCalledWhenLoaded.Value = "Y" Then
            '    txtCalledWhenLoaded.Value = "N"
            '    Return
            'End If
            'txtClickedOption.Value = "Y"
            Session("optGeographyId") = 4
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = False
            Session("bUseGList") = True
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            'LoadDim1s(lstDim1)
            'LoadDim1s(lstDim1p)
            LoadCountries()
        End If
    End Sub


    Protected Sub optGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroup.CheckedChanged
        If optGroup.Checked Then
            'If txtCalledWhenLoaded.Value = "Y" Then
            '    txtCalledWhenLoaded.Value = "N"
            '    Return
            'End If
            'txtClickedOption.Value = "Y"
            Session("optGeographyId") = 2
            Session("bUseReg") = False
            Session("bUseGroup") = True
            Session("bUseGroupDec") = False
            Session("bUseGList") = False
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            'LoadDim1s(lstDim1)
            'LoadDim1s(lstDim1p)
            LoadCountries()
        End If
    End Sub

    Protected Sub optGroupDec_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optGroupDec.CheckedChanged
        If optGroupDec.Checked Then
            'If txtCalledWhenLoaded.Value = "Y" Then
            '    txtCalledWhenLoaded.Value = "N"
            '    Return
            'End If
            'txtClickedOption.Value = "Y"
            Session("optGeographyId") = 3
            Session("bUseReg") = False
            Session("bUseGroup") = False
            Session("bUseGroupDec") = True
            Session("bUseGList") = False
            'jrs 2016/09/30
            Session("bUseCtryAndGroup") = False
            'LoadDim1s(lstDim1)
            'LoadDim1s(lstDim1p)
            LoadCountries()
        End If
    End Sub

    'jrs 2024/08/28 removed Handles lstSeries.SelectedIndexChanged so that it only adds with button
    Private Sub lstSeries_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lst = DirectCast(sender, ListBox)
        If lst.SelectedIndex = -1 Then Return
        Dim i As Integer = lst.SelectedIndex

        'cmb_VarName_Click()
        If If(Session("bAddGravityAdj"), False) Then
            AddGravityAdj(lst.Items(i).Text, "DY")
            Exit Sub
        ElseIf If(Session("bAddGravity"), False) Then
            AddGravityVar(lst.Items(i).Text, "DY")
        End If
        lstSelSeries.Items.Add("(DY) " & lst.Items(i).Text)
        If lst.Items(i).Text.Split("-").Count = 3 Then
            'only Name and Unit
            Session("BL_lbl_Var") = "(DY) " & lst.Items(i).Text.Split("-")(0).Trim & " - " & lst.Items(i).Text.Split("-")(2).Trim
        Else
            Session("BL_lbl_Var") = "(DY) " & lst.Items(i).Text '.Split("-")(0).Trim ' Gross Variable Name
        End If

        Session("curlstSelSeries") = lstSelSeries

        ReDim Preserve Session("sAggRulesSelected")(lstSelSeries.Items.Count)
        Session("sAggRulesSelected")(lstSelSeries.Items.Count - 1) = Session("sAggRules")(i)

        ReDim Preserve Session("bDyadicSeries")(lstSelSeries.Items.Count)
        ReDim Preserve Session("bMonadicActorSeries")(lstSelSeries.Items.Count)
        Session("bDyadicSeries")(lstSelSeries.Items.Count - 1) = True

        If If(Session("BL_SelectVar"), False) = True Then
            'Select then Continue at frm_computation
            Session("BL_SelectVar") = False
            cmdComputation_Click(sender, e)
        End If

    End Sub
    Private Sub SwitchSeriesType(CurrentItem As String, Optional ByVal SessionOrigDyadic As String = "")
        If SessionOrigDyadic <> "" Then
            Select Case SessionOrigDyadic
                Case "DY"
                    CurrentItem = "mnuDyadic"
                Case "MA"
                    CurrentItem = "mnuMonadicforActor"
                Case "MP"
                    CurrentItem = "mnuMonadicforPartner"
                Case "SF"
                    CurrentItem = "mnuShowSavedFormulas"
                Case "FC"
                    CurrentItem = "mnuForecast"
                Case "FH" 'jrs 2021/02/04
                    CurrentItem = "mnuForecastWHist"
            End Select
        End If

        Dim gr = cswmWebMenu.Groups("mnuSerType")
        gr.Items(CurrentItem).IconImage = "checkbox.gif"
        For Each it As Coalesys.WebMenu.Item In gr.Items
            If it.ID <> CurrentItem Then
                it.IconImage = "blank.gif"
            Else
                it.IconImage = "checkbox.gif"
                Select Case CurrentItem
                    Case "mnuDyadic"
                        Session("OrigDyadic") = "DY"
                        cmdComputation.Enabled = True
                        Me.lblHistSeries.Text = "Historical Series:"
                        lstVarNameMonadyc.Visible = False
                        lstSeries.Visible = True
                        lstVarNameSavedFormulas.Visible = False
                        cmdDeleteGravity.Visible = False
                        cmdEditGravity.Visible = False
                        lstVarNameForecast.Visible = False
                        lstVarNameForecastWHist.Visible = False 'jrs 2021/02/04
                        lstFiles.Visible = False
                        lblFiles.Visible = False
                        UpdatelstFiles.Visible = False
                    Case "mnuMonadicforActor"
                        Session("OrigDyadic") = "MA"
                        cmdComputation.Enabled = True
                        Me.lblHistSeries.Text = "Historical Monadic Actor:"
                        lstVarNameMonadyc.Visible = True
                        lstSeries.Visible = False
                        lstVarNameSavedFormulas.Visible = False
                        cmdDeleteGravity.Visible = False
                        cmdEditGravity.Visible = False
                        lstVarNameForecast.Visible = False
                        lstVarNameForecastWHist.Visible = False 'jrs 2021/02/04
                        lstFiles.Visible = False
                        lblFiles.Visible = False
                        UpdatelstFiles.Visible = False
                            'If Me.Visible Then cmb_VarNameMonadyc.SetFocus
                    Case "mnuMonadicforPartner"
                        Session("OrigDyadic") = "MP"
                        cmdComputation.Enabled = True
                        Me.lblHistSeries.Text = "Historical Monadic Partner:"
                        lstVarNameMonadyc.Visible = True
                        lstSeries.Visible = False
                        lstVarNameSavedFormulas.Visible = False
                        cmdDeleteGravity.Visible = False
                        cmdEditGravity.Visible = False
                        lstVarNameForecast.Visible = False
                        lstVarNameForecastWHist.Visible = False 'jrs 2021/02/04
                        lstFiles.Visible = False
                        lblFiles.Visible = False
                        UpdatelstFiles.Visible = False
                    Case "mnuShowSavedFormulas"
                        Session("OrigDyadic") = "SF"
                        lblHistSeries.Text = "Saved Formulas:"
                        cmdComputation.Enabled = False
                        lstVarNameMonadyc.Visible = False
                        lstSeries.Visible = False
                        lstVarNameSavedFormulas.Visible = True
                        If Not (lstVarNameSavedFormulas.Items.Count = 0 OrElse lstVarNameSavedFormulas.SelectedIndex = -1) Then
                            cmdDeleteGravity.Visible = True
                            If If(Session("lstSelSeriesSelectedIndex"), -1) >= 0 Then
                                cmdEditGravity.Visible = True
                            End If
                        End If
                        'ttt vb6 not have
                        lstVarNameForecast.Visible = False
                        lstVarNameForecastWHist.Visible = False 'jrs 2021/02/04
                        lstFiles.Visible = False
                        lblFiles.Visible = False
                        UpdatelstFiles.Visible = False
                    Case "mnuForecast"
                        Session("OrigDyadic") = "FC"
                        cmdComputation.Enabled = False
                        lstDim1p.Visible = True
                        'lblPartner.Visible = True
                        'lblActor.Caption = "Actor"
                        lblHistSeries.Text = "Forecast Variables:"
                        lstVarNameMonadyc.Visible = False
                        lstVarNameSavedFormulas.Visible = False
                        cmdDeleteGravity.Visible = False
                        cmdEditGravity.Visible = False
                        lstSeries.Visible = False
                        lstVarNameForecast.Visible = True
                        lstVarNameForecastWHist.Visible = False 'jrs 2021/02/04
                        lstFiles.Visible = True
                        lblFiles.Visible = True
                        UpdatelstFiles.Visible = True

                            'If Me.Visible Then cmb_VarNameForecast.SetFocus
                    Case "mnuForecastWHist" 'jrs 2021/02/04
                        Session("OrigDyadic") = "FH"
                        cmdComputation.Enabled = False
                        lstDim1p.Visible = True
                        'lblPartner.Visible = True
                        'lblActor.Caption = "Actor"
                        lblHistSeries.Text = "Forecast With Historical Analog Variables:"
                        lstVarNameMonadyc.Visible = False
                        lstVarNameSavedFormulas.Visible = False
                        cmdDeleteGravity.Visible = False
                        cmdEditGravity.Visible = False
                        lstSeries.Visible = False
                        lstVarNameForecast.Visible = False
                        lstVarNameForecastWHist.Visible = True

                        lstFiles.Visible = True
                        lblFiles.Visible = True
                        UpdatelstFiles.Visible = True
                End Select
            End If
        Next

    End Sub
    Private Sub lstSelSeries_dblclick()
        Dim curlst = DirectCast(Session("curlstSelSeries"), ListBox)
        'curlst.Items.RemoveAt(curlst.SelectedIndex)
        Dim c As IPostBackEventHandler = DirectCast(Me.cmdTable, IPostBackEventHandler)
        c.RaisePostBackEvent(String.Empty)

    End Sub
    Private Sub lstSelSeries_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelSeries.SelectedIndexChanged
        'ttt This works at next operation after selected
        Dim lst = DirectCast(sender, ListBox)
        Session("curlstSelSeries") = lst
        Session("lstSelSeriesSelectedValue") = lst.SelectedValue
        Session("lstSelSeriesSelectedIndex") = lst.SelectedIndex
        Check2ShowEditFormula()
    End Sub
    Private Sub lstSelDyads_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelDyads.SelectedIndexChanged
        'If lstSelDyads.ListIndex >= 0 Then
        '    lstSelDyads.RemoveItem(lstSelDyads.ListIndex)
        'End If
        If lstSelDyads.SelectedIndex >= 0 Then
            lstSelDyads.Items.RemoveAt(lstSelDyads.SelectedIndex)
        End If
    End Sub
    Private Sub LoadDataBILA()
        Session("BilateralFirstYear") = 0
        Session("BilateralLastYear") = 0
        Session("PLOTBilateralFirstYear") = 2200 ' New List(Of Integer)
        Session("PLOTBilateralLastYear") = 0 'New List(Of Integer)
        Session("BL_HasFormulaReport") = False
        Dim mPLOTDict As New Dictionary(Of String, Integer)
        '------------------
        Dim sa(lstSelDyads.Items.Count) As String
        Session("sActors") = sa
        Dim sp(lstSelDyads.Items.Count) As String
        Session("sPartners") = sp
        Dim MySQliteReader As SqliteDataReader
        Dim DBFields = New List(Of String)
        '----
        Dim FileSelCount As Integer = 0
        For Each it As ListItem In lstFiles.Items
            If it.Selected Then
                FileSelCount += 1
            End If
        Next
        ReDim Session("mPLOT")(300, lstSelSeries.Items.Count * lstSelDyads.Items.Count * If(lstFiles.Visible, FileSelCount, 1))
        ReDim Session("mPLOTFormula")(300, lstSelSeries.Items.Count * lstSelDyads.Items.Count * If(lstFiles.Visible, FileSelCount, 1))
        Session("TotalGraphs") = lstSelSeries.Items.Count * lstSelDyads.Items.Count * If(lstFiles.Visible, FileSelCount, 1)

        Dim PLOTAll = Session("mPLOT")
        ReDim Session("mLegends")(5, lstSelSeries.Items.Count * lstSelDyads.Items.Count * If(lstFiles.Visible, FileSelCount, 1))
        ReDim Session("mFLegends")(5, lstSelSeries.Items.Count * lstSelDyads.Items.Count * If(lstFiles.Visible, FileSelCount, 1))
        ReDim Session("sActors")(lstSelDyads.Items.Count)
        ReDim Session("sPartners")(lstSelDyads.Items.Count)
        Dim iStart As Integer, MoveYrs As Integer, LagYrs As Integer
        Dim val As Integer = 0
        'Dim BilateralFirstYear As Integer = 0
        'Dim BilateralFirstYear As Integer = 0
        Session("iCOUNTE") = val
        'Session("BilateralFirstYear") = 2001
        'Session("BilateralLastYear") = 2005
        'Dim FormuDataDoneI(100, 100, 100) As Boolean ' (No of Vars Of Formula, Col , i) 
        Dim FormuDataDone(300, 300) As Boolean
        Dim FirstYearFormu As New Dictionary(Of String, Integer)  '(No of Vars Of Formula-Graph Col) -> Year
        'This flag is used when too many fields in aggregation operations, say 200+ fields
        Dim bTooComplex As Boolean
        Dim sqlFields2 As String
        Dim iHalfWayIdx As Integer

        Dim sql2 As String = ""
        Dim rsDY2 As DataTable = New DataTable
        Dim iHalfWayStart As Integer
        '        Dim dbDy As Database, dbHis As Database, dbIFs As Database, dbDDDy As Database, dbDDMon As Database
        '        Dim rsDDDy As Recordset, rsDDMon As Recordset
        Dim rsDDDy As DataTable = New DataTable
        Dim rsDDMon As DataTable = New DataTable



        Dim dbDy As String = Session("PutFdrvRun") & "ifsHistSeriesDyadic.mdb"
        'Set dbHis = OpenDatabase(IFsHistSeries_MDB, False, False, DataPW$)
        'Dim dbHis = Session("IFsHistSeries_MDB")
        Dim dbHis As String = Session("PutFdrvRun") & "IFsHistSeries.MDB" '
        'Dim dbIFs = Session("IFs_MDB")

        'jam 2022/12/18
        If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") Then

            dbDy = Session("PutFdrvRun") & "ifsHistSeriesDyadic.db"

            dbHis = Session("PutFdrvRun") & "IFsHistSeries.db" '
        End If


        Dim dbIFs As String = Session("IFs_MDB2Active")
        'Dim dbDyFor() As Database
        ReDim Session("dbDyFor")(FileSelCount) ' As String for DatabaseName
        Dim II As Integer, ss As Integer, sFile As String
        Dim i As Integer, pos As Integer, j As Integer, k As Integer, m As Integer
        ss = 0
        Dim FirstFileSelectedIndex As Integer = 0
        For II = 0 To lstFiles.Items.Count - 1
            If lstFiles.Items(II).Selected Then
                If II = 1 Then
                    Session("dbDyFor")(ss) = DataFileDir$ & "\ifsForDyadic.db"
                ElseIf II = 0 Then
                    'Set dbDyFor(ss) = OpenDatabase(PutFdrvRun$ & "ifsForDyadicWork.mdb")
                    Session("dbDyFor")(ss) = Session("PutFdrvRun") & "ifsForDyadicWork.db"
                Else
                    sFile = lstFiles.Items(II).Text
                    pos = InStr(sFile, " - ")
                    sFile = Mid(sFile, pos + 3)
                    'Set dbDyFor(ss) = OpenDatabase(PutFdrvRun$ & sFile)
                    Session("dbDyFor")(ss) = Session("PutFdrvRun") & sFile
                End If
                If FirstFileSelectedIndex = 0 Then
                    FirstFileSelectedIndex = ss
                End If
                ss = ss + 1
            End If
        Next II
        ss = 0 ' ss - 1
        'For each selected variable
        Dim sVar As String, sUnits As String
        Dim sTbl As String = ""
        Dim sActor As String, sPartner As String
        Dim bGrpActor As Boolean, bGrpPartner As Boolean
        Dim sql As String, sqlFields As String, sqlWhere As String, sqlG As String
        'Dim sqlGroupBy, sqlWhereY As String 'jrs 2021/03/13
        Dim sqlWHEREA As String, sqlWHEREP As String
        Dim rsG As DataTable = New DataTable
        Dim rsDY As DataTable = New DataTable
        Dim bWeightAvg As Boolean
        Dim bFormula As Boolean, NumbCompVar As Integer, CurrNumbCompVar As Integer
        Dim sAggRule As String = ""
        Dim bDyadic As Boolean, iLast As Integer, bMonadicActor As Boolean
        Dim sEmpVar As String
        Dim tblAux As DataTable = New DataTable

        Dim bForecast As Boolean
        'jrs 2020/07/22
        Dim bForecastH As Boolean

        'For i = 0 To lstSelSeries.ListCount - 1
        Dim sq0 As String = ""
        Dim rs0 As DataTable = New DataTable
        Dim sq1 As String = ""
        Dim rs1 As DataTable = New DataTable
        Dim f As Formula
        Dim fm As String = ""
        Dim x0s As New List(Of String)
        Dim iBilateralFirstYear(300) As Integer
        'Biggest For, for lstSelSeries.Items.Count
        Dim ilstSelSeries As Integer = 1
        Dim IsFormula(lstSelSeries.Items.Count) As Boolean

        Dim mPLOTDictFormula As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
        For i = 0 To lstSelSeries.Items.Count - 1
            Dim dataAll() As Object
            Dim dataforFormula() As Object
            'Reset FileSelected to First
            ss = 0
            Dim bStartedNewSeries As Boolean = True
            ilstSelSeries += 1
            bFormula = False
            IsFormula(i) = bFormula

            bDyadic = Session("bDyadicSeries")(i)

            'jrs 2021/02/05 added or lstSelSeries.Items(i).Text.StartsWith("(FH)")
            'jrs 2020/10/19 added or lstSelSeries.Items(i).Text.StartsWith("(FC)")
            bDyadic = lstSelSeries.Items(i).Text.StartsWith("(DY)") Or lstSelSeries.Items(i).Text.StartsWith("(FC)") Or lstSelSeries.Items(i).Text.StartsWith("(FH)")
            bMonadicActor = lstSelSeries.Items(i).Text.StartsWith("(MA)")

            NumbCompVar = 0
            CurrNumbCompVar = 1
            pos = InStr(lstSelSeries.Items(i).Text, " - ")
            bForecast = False
            'jrs 2020/07/22
            bForecastH = False

            If pos > 0 Then
                sVar = Mid(lstSelSeries.Items(i).Text, 6, pos - 6)
                pos = InStr(pos + 3, lstSelSeries.Items(i).Text, " - ")
                sUnits = Mid(lstSelSeries.Items(i).Text, pos + 3)
                sAggRule = Session("sAggRulesSelected")(i)
                'jrs 2016/09/21
                If Mid(lstSelSeries.Items(i).Text, 1, 4) = "(FC)" Then
                    bForecast = True
                End If
                'jrs 2020/07/22
                If Mid(lstSelSeries.Items(i).Text, 1, 4) = "(FH)" Then
                    bForecastH = True
                End If
                If bForecast Then
                    sTbl = sVar
                ElseIf bForecastH Then
                    sTbl = "Series" & Session("sHistAnalogsSelected")(i)
                Else
                    sTbl = "Series" & sVar
                End If
            Else
                sVar = lstSelSeries.Items(i).Text
                If i <= UBound(Session("aFormulas")) Then
                    If sVar = Session("aFormulas")(i + 1).Name Then
                        bFormula = True
                        IsFormula(i) = bFormula
                        'Brought from below
                        f = Session("aFormulas")(i + 1)
                        'reset for each formula
                        NumbCompVar = 0
                        CurrNumbCompVar = 1
                        dataforFormula = Nothing
                        bStartedNewSeries = True
                        'End Brought up

                        Session("bCompAll") = True
                        Session("gidxComp") = i
                        Session("aFormulas")(i + 1).endAll = lstSelDyads.Items.Count
                        If Session("aFormulas")(i + 1).endAll < Session("aFormulas")(i + 1).MaxendAll Then
                            Session("aFormulas")(i + 1).MaxendAll = Session("aFormulas")(i + 1).endAll
                        End If
                        'Set frm_computations.Formula = Session("aFormulas")(i + 1)
                        NumbCompVar = Session("aFormulas")(i + 1).disVar.disSize()
                        If Session("aFormulas")(i + 1).disVar().displayDet(1).ToString.Contains("|") Then
                            'Gravity
                            sVar = Trim(Mid(Session("aFormulas")(i + 1).disVar().displayDet(1).Split("|")(0).Trim, 6))
                        Else
                            'Computation
                            sVar = Trim(Mid(Session("aFormulas")(i + 1).disVar().displayDet(1).Split("-")(0).Trim, 6))
                        End If

                        sTbl = "Series" & sVar
                        sUnits = Session("aFormulas")(i + 1).Unit
                        sAggRule = ""
                        'jam 2022/12/18
                        'If Not Session("mnuSQLite") Is Nothing Then
                        '    If Session("mnuSQLite") Then
                        '        vConnString = "DataSource=" + Session("PutFdrvRun") & "IFSForDyadic.db;"
                        '        vSQLiteCon = New SqliteConnection(vConnString)
                        '        vSQLiteCon.Open()
                        '    End If
                        'End If
                        ''Dyadic or Monadic
                        If Mid(Session("aFormulas")(i + 1).disVar().displayDet(1), 1, 4) = "(DY)" Then
                            bDyadic = True

                            'Set dbDDDy = OpenDatabase(PutFdrvRun$ + "DataDictDyadic.mdb")
                            'Set rsDDDy = dbDDDy.OpenRecordset("datadict")
                            'rsDDDy.Index = "Variable"

                            sql = "Select * FROM datadict"
                            'jam 2022/12/18
                            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                                SetDataTablefromAccess(Session("PutFdrvRun") & "DataDictDyadic.mdb", sql, "Variable", rsDDDy)
                                For Each r As DataRow In rsDDDy.Rows
                                    If sVar = r.Item("Variable") Then
                                        If Not (r.Item("Aggregation") Is DBNull.Value) Then
                                            sAggRule = r.Item("Aggregation")
                                        End If
                                        Exit For
                                    End If
                                Next
                            Else

                                vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDictDyadic.db;"

                                vSQLiteCon = New SqliteConnection(vConnString)
                                vSQLiteCon.Open()



                                vSQLiteCommand = vSQLiteCon.CreateCommand()
                                vSQLiteCommand.CommandText = sql
                                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                                While (vSQLiteReader.Read())
                                    If sVar = vSQLiteReader("Variable") Then
                                        If Not (vSQLiteReader("Aggregation") Is DBNull.Value) Then
                                            sAggRule = vSQLiteReader("Aggregation")
                                        End If
                                        Exit While
                                    End If

                                End While
                                vSQLiteCon.Close()



                            End If



                        Else
                            bDyadic = False
                            If Mid(Session("aFormulas")(i + 1).disVar().displayDet(1), 1, 4) = "(MA)" Then
                                bMonadicActor = True
                            Else
                                bMonadicActor = False
                            End If
                            'Set dbDDMon = OpenDatabase(PutFdrvRun$ + "DataDict.mdb")
                            'Set rsDDMon = dbDDMon.OpenRecordset("datadict")
                            'rsDDMon.Index = "Variable"
                            sql = "Select * FROM datadict"

                            'jam 2022/12/18
                            If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                                SetDataTablefromAccess(Session("PutFdrvRun") & "DataDict.mdb", sql, "Variable", rsDDMon)
                                For Each r As DataRow In rsDDMon.Rows
                                    If sVar = r.Item("Variable") Then
                                        If Not (r.Item("Aggregation") Is DBNull.Value) Then
                                            sAggRule = r.Item("Aggregation")
                                        End If
                                        Exit For
                                    End If
                                Next
                            Else
                                vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDict.db;"

                                vSQLiteCon = New SqliteConnection(vConnString)
                                vSQLiteCon.Open()



                                vSQLiteCommand = vSQLiteCon.CreateCommand()
                                vSQLiteCommand.CommandText = sql
                                vSQLiteReader = vSQLiteCommand.ExecuteReader()


                                While (vSQLiteReader.Read())
                                    If sVar = vSQLiteReader("Variable") Then
                                        If Not (vSQLiteReader("Aggregation") Is DBNull.Value) Then
                                            sAggRule = vSQLiteReader("Aggregation")
                                        End If
                                        Exit While
                                    End If

                                End While
                                vSQLiteCon.Close()

                            End If

                        End If
                    End If
                End If
            End If
            Dim iRSDY As Integer = 1
            Dim iWhile As Integer = 1
            Do While True
                'For each dyad
                iWhile += 1
                For j = 0 To lstSelDyads.Items.Count - 1
                    Dim graphno As Integer = lstSelDyads.Items.Count * i + (j + 1)
                    pos = InStr(lstSelDyads.Items(j).Text, " - ")
                    If pos > 0 Then
                        sActor = Mid(lstSelDyads.Items(j).Text, 1, pos - 1)
                        sPartner = Mid(lstSelDyads.Items(j).Text, pos + 3, Len(lstSelDyads.Items(j).Text) - pos)
                        bGrpActor = (gGetGroupIdx(sActor, False, Me) > 0)
                        bGrpPartner = (gGetGroupIdx(sPartner, False, Me) > 0)
                    Else
                        sActor = lstSelDyads.Items(j).Text
                        bGrpActor = (gGetGroupIdx(sActor, False, Me) > 0)
                        sPartner = ""
                        bGrpPartner = False
                    End If
                    Session("sActors")(j + 1) = sActor
                    Session("sPartners")(j + 1) = sPartner
                    bWeightAvg = False
                    'jrs 2016/11/28 reset for other variables
                    iHalfWayIdx = 0
                    bTooComplex = False
                    iHalfWayStart = 0

                    If (Not bGrpActor) AndAlso (Not bGrpPartner) Then
                        If bDyadic Then
                            'jrs 2016/09/21
                            If bForecast Then
                                'Need to change sActor and sPartner to RegionIdx
                                sActor = CStr(IFsRegs.Item(getRegionFromCtry(sActor)).Index)
                                sPartner = CStr(IFsRegs.Item(getRegionFromCtry(sPartner)).Index)
                                sql = "Select * FROM [" & sTbl & "] WHERE Actor = " & sActor & " And Partner = " & sPartner 'jrs 2019/10/03
                            Else
                                sql = "Select * FROM [" & sTbl & "] WHERE Actor = """ & sActor & """ And Partner = """ & sPartner & """"  'jrs 2019/10/03
                            End If
                        ElseIf bMonadicActor Then
                            sql = "Select * FROM [" & sTbl & "] WHERE Country = """ & sActor & """"  'jrs 2019/10/03
                        Else
                            sql = "Select * FROM [" & sTbl & "] WHERE Country = """ & sPartner & """"  'jrs 2019/10/03
                        End If
                    Else
                        sqlFields = ""
                        sqlFields2 = ""

                        If bDyadic Then

                            If bForecast Then
                                sq0 = "Select * FROM [" & sTbl & "] where 1=2"
                                'SetDataTablefromAccess(Session("dbDyFor")(ss), sq0, "", rs0)
                                'If rs0.Columns.Count - 2 > 150 Then
                                '    sq1 = "Select * FROM [" & sTbl & "] where 1=2"
                                '    SetDataTablefromAccess(dbDy, sq1, "", rs1)
                                '    iHalfWayIdx = rs1.Columns.Count / 2
                                '    bTooComplex = True
                                '    iHalfWayStart = 2
                                'End If
                                SqliteTable(sq0, Session("dbDyFor")(ss), MySQliteReader, DBFields)
                                If DBFields.Count - 2 > 150 Then
                                    sq1 = "Select * FROM [" & sTbl & "] where 1=2"
                                    SetDataTablefromAccess(dbDy, sq1, "", rs1)
                                    iHalfWayIdx = rs1.Columns.Count / 2
                                    bTooComplex = True
                                    iHalfWayStart = 2
                                End If

                            Else

                                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                                    sq1 = "Select * FROM [" & sTbl & "] where 1=2"
                                    SetDataTablefromAccess(dbDy, sq1, "", rs1)
                                    If rs1.Columns.Count - 6 > 150 Then
                                        iHalfWayIdx = rs1.Columns.Count / 2
                                        bTooComplex = True
                                        iHalfWayStart = 4
                                    End If
                                Else
                                    'jrs 2025/04/22 avoid check for HIstory plus forecast
                                    If Not bForecastH Then
                                        sq1 = "Select * FROM [" & sTbl & "] where 1=2"

                                        SqliteTable(sq1, Session("dbDyFor")(ss), MySQliteReader, DBFields)
                                        If DBFields.Count - 6 > 150 Then
                                            sq1 = "Select * FROM [" & sTbl & "] where 1=2"
                                            SetDataTablefromAccess(dbDy, sq1, "", rs1)
                                            iHalfWayIdx = rs1.Columns.Count / 2
                                            bTooComplex = True
                                            iHalfWayStart = 4
                                        End If
                                    End If
                                End If

                            End If

                            If bForecast Then
                                sq0 = "Select * FROM [" & sTbl & "] where 1=2"
                                'SetDataTablefromAccess(Session("dbDyFor")(ss), sq0, "", rs0)
                                SqliteTable(sq0, Session("dbDyFor")(ss), MySQliteReader, DBFields)
                                Select Case sAggRule
                                    Case "SUM"
                                        sqlFields = "Year, SUM([Val]) As YSUM"
                                    Case "AVG"
                                        sqlFields = "Year, AVG([Val]) As YAVG"
                                    Case Else
                                        bWeightAvg = True
                                        sEmpVar = IFsAggRules.Item(sAggRule).ForecastVar
                                        If sEmpVar > "" Then
                                            'MsgBox "Oooops, currently we don't have access to regular forecast variables to do the aggregation", vbCritical, "Bilateral Interaction Display"
                                            Exit Sub

                                        End If
                                        sqlFields = "*"
                                End Select
                                'jrs 2021/03/13
                                'sqlGroupBy = "GROUP BY Year"
                                'sqlWhereY = "Year >= " & Session("FirstYear") & " AND Year <= " & Session("LastYear")
                            Else '=======================HISTORIC==============
                                sq1 = "SELECT * FROM [" & sTbl & "] where 1=2"
                                SetDataTablefromAccess(dbDy, sq1, "", rs1)
                                Select Case sAggRule
                                    Case "SUM"
                                        For k = 4 To rs1.Columns.Count - 3
                                            If k < iHalfWayIdx Or Not bTooComplex Then
                                                If k > 4 Then
                                                    sqlFields = sqlFields & ", "
                                                End If
                                                sqlFields = sqlFields & "SUM([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                            Else
                                                If k > iHalfWayIdx Then
                                                    sqlFields2 = sqlFields2 & ", "
                                                End If
                                                sqlFields2 = sqlFields2 & "SUM([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                            End If
                                        Next k
                                    Case "AVG"
                                        For k = 4 To rs1.Columns.Count - 3
                                            If k < iHalfWayIdx Or Not bTooComplex Then
                                                If k > 4 Then
                                                    sqlFields = sqlFields & ", "
                                                End If
                                                sqlFields = sqlFields & "AVG([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                            Else
                                                If k > iHalfWayIdx Then
                                                    sqlFields2 = sqlFields2 & ", "
                                                End If
                                                sqlFields2 = sqlFields2 & "AVG([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                            End If
                                        Next k
                                    Case Else
                                        bWeightAvg = True
                                        sEmpVar = IFsAggRules.Item(sAggRule).HisEmpVar
                                        If sEmpVar > "" Then
                                            'Set tblAux = dbHis.OpenRecordset("Series" & sEmpVar) 
                                            'tblAux.Index = "Country"
                                            SetDataTablefromAccess(dbHis, "select * from [Series" & sEmpVar & "]", "Country", tblAux, DataPW)
                                        End If
                                        sqlFields = "*"
                                End Select
                            End If 'if is forecast else
                        Else 'not bDyadic -----
                            sq1 = "SELECT * FROM [" & sTbl & "] where 1=2"
                            SetDataTablefromAccess(dbHis, sq1, "", rs1)

                            If rs1.Columns.Count - 3 > 150 Then
                                bTooComplex = True
                                iHalfWayIdx = rs1.Columns.Count / 2
                                iHalfWayStart = 2
                            End If
                            Select Case sAggRule
                                Case "SUM"
                                    For k = 2 To rs1.Columns.Count - 3
                                        If k < iHalfWayIdx Or Not bTooComplex Then
                                            If k > 2 Then
                                                sqlFields = sqlFields & ", "
                                            End If
                                            sqlFields = sqlFields & "SUM([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                        Else
                                            If k > iHalfWayIdx Then
                                                sqlFields2 = sqlFields2 & ", "
                                            End If
                                            sqlFields2 = sqlFields2 & "SUM([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                        End If
                                    Next k
                                Case "AVG"
                                    For k = 2 To rs1.Columns.Count - 3
                                        If k < iHalfWayIdx Or Not bTooComplex Then
                                            If k > 2 Then
                                                sqlFields = sqlFields & ", "
                                            End If
                                            sqlFields = sqlFields & "AVG([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                        Else
                                            If k > iHalfWayIdx Then
                                                sqlFields2 = sqlFields2 & ", "
                                            End If
                                            sqlFields2 = sqlFields2 & "AVG([" & rs1.Columns(k).ColumnName & "]) as Y" & rs1.Columns(k).ColumnName
                                        End If
                                    Next k
                                Case Else
                                    bWeightAvg = True
                                    sEmpVar = IFsAggRules.Item(sAggRule).HisEmpVar
                                    If sEmpVar > "" Then
                                        'Set tblAux = dbHis.OpenRecordset("Series" & sEmpVar) 'jrs 2019/10/03
                                        'tblAux.Index = "Country"
                                        sq0 = "SELECT * FROM [" & "Series" & sEmpVar & "]"
                                        SetDataTablefromAccess(dbHis, sq0, "Country", tblAux, DataPW)
                                    End If
                                    sqlFields = "*"
                            End Select
                        End If
                        sqlWhere = ""
                        sqlWHEREA = ""
                        sqlWHEREP = ""
                        If bForecast Then
                            If bGrpActor Then
                                If sActor <> "World" Then
                                    sqlG = "SELECT G.RegionNo FROM " & Grouping & " AS G, " & GroupIDNew &
                                    " AS GI WHERE G.GroupNo = GI.GroupNum And GI.GroupName = """ & sActor & """"
                                    SetDataTablefromAccess(dbIFs, sqlG, "Country", rsG)
                                    If Not rsG Is Nothing Then
                                        For Each r As DataRow In rsG.Rows
                                            If sqlWHEREA > "" Then
                                                sqlWHEREA = sqlWHEREA & ", "
                                            End If
                                            sqlWHEREA = sqlWHEREA & r.Item("RegionNo")
                                        Next
                                    End If

                                    sqlWHEREA = "Actor IN (" & If(sqlWHEREA = "", "-1", sqlWHEREA) & ")"
                                    'sqlWHEREA = "Actor IN (" & sqlWHEREA & ")"
                                End If
                            Else
                                sActor = CStr(IFsRegs.Item(getRegionFromCtry(sActor)).Index)
                                sqlWHEREA = "Actor = " & sActor
                            End If

                            If bGrpPartner Then
                                If sPartner <> "World" Then
                                    sqlG = "SELECT G.RegionNo FROM " & Grouping & " AS G, " & GroupIDNew &
                                    " AS GI WHERE G.GroupNo = GI.GroupNum AND GI.GroupName = """ & sPartner & """"

                                    SetDataTablefromAccess(dbIFs, sqlG, "Country", rsG)

                                    If Not rsG Is Nothing Then
                                        For Each r As DataRow In rsG.Rows
                                            If sqlWHEREP > "" Then
                                                sqlWHEREP = sqlWHEREP & ", "
                                            End If
                                            sqlWHEREP = sqlWHEREP & r.Item("RegionNo")
                                        Next
                                    End If

                                    sqlWHEREP = "Partner IN (" & If(sqlWHEREP = "", "-1", sqlWHEREP) & ")"
                                    'sqlWHEREP = "Partner IN (" & sqlWHEREP & ")"
                                End If
                            Else
                                sPartner = CStr(IFsRegs.Item(getRegionFromCtry(sPartner)).Index)
                                sqlWHEREP = "Partner = " & sPartner
                            End If
                        Else 'Not Forecast, just historical data
                            If bGrpActor Then
                                If sActor <> "World" Then
                                    sqlG = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & sActor & """"

                                    SetDataTablefromAccess(dbIFs, sqlG, "Country", rsG)

                                    If Not rsG Is Nothing Then
                                        For Each r As DataRow In rsG.Rows
                                            If sqlWHEREA > "" Then
                                                sqlWHEREA = sqlWHEREA & ", "
                                            End If
                                            sqlWHEREA = sqlWHEREA & """" & r.Item("Country") & """"
                                        Next
                                    End If

                                    sqlWHEREA = "Actor IN (" & If(sqlWHEREA = "", """-1""", sqlWHEREA) & ")"
                                    'sqlWHEREA = "Actor IN (" & sqlWHEREA & ")"
                                End If

                            Else
                                sqlWHEREA = "Actor = """ & sActor & """"
                            End If

                            If bGrpPartner Then
                                If sPartner <> "World" Then
                                    sqlG = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & sPartner & """"

                                    SetDataTablefromAccess(dbIFs, sqlG, "Country", rsG)

                                    If Not rsG Is Nothing Then
                                        For Each r As DataRow In rsG.Rows
                                            If sqlWHEREP > "" Then
                                                sqlWHEREP = sqlWHEREP & ", "
                                            End If
                                            sqlWHEREP = sqlWHEREP & """" & r.Item("Country") & """"
                                        Next
                                    End If

                                    sqlWHEREP = "Partner IN (" & If(sqlWHEREP = "", """-1""", sqlWHEREP) & ")"
                                    'sqlWHEREP = "Partner IN (" & sqlWHEREP & ")"
                                End If
                            Else
                                sqlWHEREP = "Partner = """ & sPartner & """"
                            End If
                        End If
                        If bDyadic Then
                            If sqlWHEREA > "" And sqlWHEREP > "" Then
                                sqlWhere = sqlWHEREA & " AND " & sqlWHEREP
                                'jrs 2017/08/14
                                If bGrpActor Or bGrpPartner Then
                                    sqlWhere = sqlWhere & " AND Actor <> Partner"
                                End If
                            ElseIf sqlWHEREA > "" Then
                                sqlWhere = sqlWHEREA
                            ElseIf sqlWHEREP > "" Then
                                sqlWhere = sqlWHEREP
                            End If
                            'jrs 2021/03/13
                            'If sqlWhereY > "" Then
                            '    If sqlWhere > "" Then
                            '        sqlWhere = sqlWhere & " AND "
                            '    End If
                            '    sqlWhere = sqlWhere & sqlWhereY
                            'End If
                        ElseIf bMonadicActor Then
                            If sqlWHEREA > "" Then
                                sqlWhere = Replace(sqlWHEREA, "Actor", "Country")
                            End If
                        Else
                            If sqlWHEREP > "" Then
                                sqlWhere = Replace(sqlWHEREP, "Partner", "Country")
                            End If
                        End If
                        sql = "SELECT " & sqlFields & " FROM [" & sTbl & "]" 'jrs 2019/10/16 added []
                        If sqlWhere > "" Then
                            sql = sql & " WHERE " & sqlWhere
                        End If
                        'jrs 2021/03/13
                        'If sqlGroupBy > "" Then
                        '    sql = sql & " " & sqlGroupBy
                        '    sqlGroupBy = ""
                        'End If
                        If bTooComplex And sqlFields2 > "" Then 'jrs 2019/10/16 added and sqlFields2>""
                            sql2 = "SELECT " & sqlFields2 & " FROM [" & sTbl & "]" 'jrs 2019/10/16 added []
                            If sqlWhere > "" Then
                                sql2 = sql2 & " WHERE " & sqlWhere
                            End If
                        End If


                    End If 'Of If Not bGrpActor And Not bGrpPartner 
                    If Not ((bDyadic OrElse (Not bDyadic AndAlso Not bMonadicActor)) AndAlso sPartner = "") Then
                        If bDyadic Then
                            'jrs 2016/09/22
                            If bForecast Then
                                'SetDataTablefromAccess(Session("dbDyFor")(ss), sql, "Country", rsDY)
                                '"SELECT SUM([Year]) As YYear, SUM([Val]) As YVal FROM [XDYAD] WHERE Actor = 7 AND Partner IN (29, 57, 61, 80, 82, 177, 179) AND Actor <> Partner"
                                '=> SELECT [Year], SUM([Year]) As YYear, SUM([Val]) As YVal FROM [XDYAD] WHERE Actor = 7 AND Partner IN (29, 57, 61, 80, 82, 177, 179) AND Actor <> Partner Group By [Year]"
                                sql = "SELECT [Year]," & sql.Substring(7) & " Group By [Year]"
                                SqliteTable(sql, Session("dbDyFor")(ss), MySQliteReader, DBFields, rsDY)
                            Else
                                SetDataTablefromAccess(dbDy, sql, "Country", rsDY)
                            End If
                        Else
                            SetDataTablefromAccess(dbHis, sql, "Country", rsDY, DataPW)
                        End If
                        If bTooComplex AndAlso sql2 > "" Then 'jrs 2019/10/16 added and sql2>""
                            If bDyadic Then
                                'jrs 2016/09/22
                                If bForecast Then
                                    'SetDataTablefromAccess(Session("dbDyFor")(ss), sql2, "Country", rsDY2)
                                    SqliteTable(sql2, Session("dbDyFor")(ss), MySQliteReader, DBFields, rsDY2)
                                Else
                                    SetDataTablefromAccess(dbDy, sql2, "Country", rsDY2)
                                End If
                            Else
                                SetDataTablefromAccess(dbHis, sql2, "Country", rsDY2, DataPW)
                            End If
                        End If

                        Session("iCOUNTE") = Session("iCOUNTE") + 1
                        MoveYrs = 0
                        'If has data
                        If (Not rsDY Is Nothing) AndAlso rsDY.Rows.Count > 0 Then 'Not (rsDY.BOF And rsDY.EOF) Then
                            iRSDY += 1
                            If bWeightAvg Then
                                If bDyadic Then
                                    ComputeWeightAvg(rsDY, tblAux, bGrpActor, bGrpPartner, Session("mPLOT"), Session("iCOUNTE"))
                                Else
                                    ComputeWeightAvgMon(rsDY, tblAux, bGrpActor, Session("mPLOT"), Session("iCOUNTE"))
                                End If
                            Else 'Not bWeightAvg
                                If bDyadic Then
                                    If rsDY.Columns(0).ColumnName = "Actor" Then 'rsDY.Columns(0).ColumnName = "Actor" Then
                                        'jrs 2016/09/22
                                        If bForecast Then
                                            iStart = 2
                                        Else
                                            iStart = 4
                                        End If
                                    Else
                                        iStart = 0
                                    End If
                                Else
                                    If rsDY.Columns(0).ColumnName = "Country" Then
                                        iStart = 2
                                    Else
                                        iStart = 0
                                    End If
                                End If
                                If Session("BilateralFirstYear") = 0 Then
                                    LagYrs = 0
                                    If iStart > 0 Then
                                        If bForecast Then
                                            Session("BilateralFirstYear") = CInt(rsDY.Rows(0).Item(2))
                                            Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                                        Else
                                            Session("BilateralFirstYear") = CInt(rsDY.Columns(iStart).ColumnName)
                                            Session("BilateralLastYear") = CInt(rsDY.Columns(rsDY.Columns.Count - 3).ColumnName)
                                        End If
                                    Else
                                        If Not bTooComplex Then
                                            If bForecast Then
                                                Session("BilateralFirstYear") = CInt(rsDY.Rows(0).Item(2))
                                                Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                                            Else
                                                Session("BilateralFirstYear") = CInt(Mid(rsDY.Columns(iStart).ColumnName, 2))
                                                Session("BilateralLastYear") = CInt(Mid(rsDY.Columns(rsDY.Columns.Count - 1).ColumnName, 2))
                                            End If

                                        Else
                                            If bForecast Then
                                                Session("BilateralFirstYear") = CInt(rsDY.Rows(0).Item(2))
                                                Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                                            Else
                                                Session("BilateralFirstYear") = CInt(Mid(rsDY.Columns(iStart).ColumnName, 2))
                                                Session("BilateralLastYear") = CInt(Mid(rsDY2.Columns(rsDY2.Columns.Count - 1).ColumnName, 2))
                                            End If

                                        End If
                                        'End If

                                    End If

                                Else ' Next series...

                                    If bForecast Then 'Same as above
                                        Session("BilateralFirstYear") = CInt(rsDY.Rows(0).Item(2))
                                        Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                                    Else
                                        If iStart > 0 Then
                                            If Session("BilateralFirstYear") > CInt(rsDY.Columns(iStart).ColumnName) Then
                                                MoveYrs = Session("BilateralFirstYear") - CInt(rsDY.Columns(iStart).ColumnName)
                                                Session("BilateralFirstYear") = CInt(rsDY.Columns(iStart).ColumnName)
                                                LagYrs = 0
                                            Else
                                                LagYrs = CInt(rsDY.Columns(iStart).ColumnName) - Session("BilateralFirstYear")
                                            End If
                                            'jrs 2016/09/22
                                            If bForecast Then
                                                If Session("BilateralLastYear") < CInt(rsDY.Columns(rsDY.Columns.Count - 1).ColumnName) Then
                                                    Session("BilateralLastYear") = CInt(rsDY.Columns(rsDY.Columns.Count - 1).ColumnName)
                                                End If
                                            Else
                                                If Session("BilateralLastYear") < CInt(rsDY.Columns(rsDY.Columns.Count - 3).ColumnName) Then
                                                    Session("BilateralLastYear") = CInt(rsDY.Columns(rsDY.Columns.Count - 3).ColumnName)
                                                End If
                                            End If
                                        Else
                                            If Session("BilateralFirstYear") > CInt(Mid(rsDY.Columns(iStart).ColumnName, 2)) Then
                                                MoveYrs = Session("BilateralFirstYear") - CInt(Mid(rsDY.Columns(iStart).ColumnName, 2))
                                                Session("BilateralFirstYear") = CInt(Mid(rsDY.Columns(iStart).ColumnName, 2))
                                                LagYrs = 0
                                            Else
                                                LagYrs = CInt(Mid(rsDY.Columns(iStart).ColumnName, 2)) - Session("BilateralFirstYear")
                                            End If
                                            If Not bTooComplex Then
                                                If Session("BilateralLastYear") < CInt(Mid(rsDY.Columns(rsDY.Columns.Count - 1).ColumnName, 2)) Then
                                                    Session("BilateralLastYear") = CInt(Mid(rsDY.Columns(rsDY.Columns.Count - 1).ColumnName, 2))
                                                End If
                                            Else
                                                If Session("BilateralLastYear") < CInt(Mid(rsDY2.Columns(rsDY2.Columns.Count - 1).ColumnName, 2)) Then
                                                    Session("BilateralLastYear") = CInt(Mid(rsDY2.Columns(rsDY2.Columns.Count - 1).ColumnName, 2))
                                                End If
                                            End If
                                        End If
                                    End If
                                    iBilateralFirstYear(iRSDY - 2) = Session("PLOTBilateralFirstYear")
                                    iBilateralFirstYear(iRSDY - 1) = Session("BilateralFirstYear")

                                    Session("PLOTBilateralFirstYear") = Math.Min(Session("PLOTBilateralFirstYear"), Session("BilateralFirstYear"))
                                    Session("PLOTBilateralLastYear") = Math.Max(Session("PLOTBilateralLastYear"), Session("BilateralLastYear"))
                                End If

                                If bGrpActor Or bGrpPartner Then
                                    If bForecast Then
                                        iLast = rsDY.Rows.Count + 1
                                        If bTooComplex Then
                                            iLast = iLast + rsDY2.Rows.Count
                                        End If
                                    Else
                                        iLast = rsDY.Columns.Count - 1
                                        If bTooComplex Then
                                            iLast = iLast + rsDY2.Columns.Count
                                        End If
                                    End If
                                Else
                                    If bForecast Then
                                        'iLast = rsDY.Columns.Count - 1
                                        iLast = rsDY.Rows.Count + 1
                                    Else
                                        iLast = rsDY.Columns.Count - 3
                                    End If
                                End If
                                If bFormula Then
                                    'z24 brought from below
                                    f = Session("aFormulas")(i + 1)
                                    fm = f.Formula_Renamed
                                    x0s = Xs(fm)
                                    If bStartedNewSeries Then
                                        bStartedNewSeries = False
                                        ReDim dataAll(f.disVar.disSize())
                                        ReDim dataforFormula(f.disVar.disSize())
                                    Else
                                        ReDim Preserve dataAll(f.disVar.disSize())
                                    End If

                                    For k = iStart - LagYrs To iStart - 1
                                        PLOTAll(k - iStart + 1 + LagYrs, j + 1) = Nothing 'Null

                                    Next k
                                    For k = iStart To iLast
                                        Dim key = MakePlotKey(k - iStart + 1 + LagYrs, graphno)

                                        If k < iHalfWayIdx - iHalfWayStart Or Not bTooComplex Then
                                            PLOTAll(k - iStart + 1 + LagYrs, graphno) = rsDY.Rows(0).Item(k) 'rsDY.Columns(k).Value

                                            If Not mPLOTDictFormula.ContainsKey(key) Then
                                                mPLOTDictFormula.Add(key, rsDY.Columns(k).ColumnName.Replace("Y", ""))
                                            Else
                                                'set priority to older year
                                                If mPLOTDictFormula(key) > CInt(rsDY.Columns(k).ColumnName.Replace("Y", "")) Then
                                                    mPLOTDictFormula(key) = rsDY.Columns(k).ColumnName.Replace("Y", "")
                                                End If
                                            End If
                                            If key = graphno & "-" & (1 + LagYrs) Then
                                                FirstYearFormu.Add(CurrNumbCompVar & "-" & graphno, mPLOTDictFormula(key))
                                            End If

                                        Else
                                            PLOTAll(k - iStart + 1 + LagYrs, graphno) = rsDY2.Rows(0).Item(k - iHalfWayIdx + iHalfWayStart) ' rsDY2.Columns(k - iHalfWayIdx + iHalfWayStart).Value
                                            If Not mPLOTDictFormula.ContainsKey(key) Then
                                                mPLOTDictFormula.Add(key, rsDY.Columns(k - iHalfWayIdx + iHalfWayStart).ColumnName.Replace("Y", ""))
                                            Else
                                                'set priority to older year
                                                If mPLOTDictFormula(key) > CInt(rsDY.Columns(k - iHalfWayIdx + iHalfWayStart).ColumnName.Replace("Y", "")) Then
                                                    mPLOTDictFormula(key) = rsDY.Columns(k - iHalfWayIdx + iHalfWayStart).ColumnName.Replace("Y", "")
                                                End If
                                            End If

                                            If key = graphno & "-1" Then
                                                FirstYearFormu.Add(CurrNumbCompVar & "-" & graphno, mPLOTDictFormula(key))
                                            End If
                                        End If
                                    Next k

                                    For k = iLast + 1 To UBound(PLOTAll, 1) - 1 - LagYrs
                                        PLOTAll(k - iStart + 1 + LagYrs, graphno) = Nothing

                                    Next k
                                    'frm_computations.LoadIdxDataAll = CurrNumbCompVar
                                    'frm_computations.LoadDataAll = PLOTAll

                                    dataAll(CurrNumbCompVar) = PLOTAll

                                    If (dataforFormula Is Nothing OrElse dataforFormula(0) Is Nothing) Then
                                        ReDim dataforFormula(f.disVar.disSize())
                                        For h = 0 To dataforFormula.Length - 1
                                            ReDim dataforFormula(h)(300, lstSelSeries.Items.Count * lstSelDyads.Items.Count * If(lstFiles.Visible, FileSelCount, 1))
                                        Next
                                    End If
                                    For k = 1 To UBound(dataAll(CurrNumbCompVar), 1)
                                        dataforFormula(CurrNumbCompVar)(k, graphno) = dataAll(CurrNumbCompVar)(k, graphno)
                                    Next

                                    FormuDataDone(CurrNumbCompVar, graphno) = True
                                    Session("BL_HasFormulaReport") = True

                                    Session("dataAll") = dataAll

                                    'Calculate at Last
                                    Dim plsContinue As Boolean = True
                                    For g = 0 To x0s.Count - 1
                                        'Only last session
                                        Dim h As Integer = Session("iCOUNTE") - 1
                                        Dim curDataDone As Boolean = FormuDataDone(g + 1, h + 1)
                                        plsContinue = plsContinue AndAlso curDataDone
                                    Next g

                                    'ttt - set max year of report for each graph
                                    Dim MaxFirstYear As Integer = 0
                                    If plsContinue Then
                                        For m = 0 To x0s.Count - 1
                                            If FirstYearFormu.ContainsKey((m + 1) & "-" & Session("iCOUNTE")) AndAlso FirstYearFormu((m + 1) & "-" & Session("iCOUNTE")) > MaxFirstYear Then
                                                MaxFirstYear = FirstYearFormu((m + 1) & "-" & Session("iCOUNTE"))
                                            End If
                                        Next
                                    End If
                                    'END-set max year of report for each graph
                                    If plsContinue Then
                                        Dim rsFormu As DataTable = New DataTable
                                        fm = RealQuery(fm)
                                        Dim fm1 As String = fm
                                        Dim adjYear As Integer = 0
                                        For m = 0 To x0s.Count - 1
                                            If FirstYearFormu.ContainsKey((m + 1) & "-" & Session("iCOUNTE")) Then
                                                Dim fy0 = FirstYearFormu((m + 1) & "-" & Session("iCOUNTE"))
                                                adjYear = Math.Max(adjYear, MaxFirstYear - fy0)
                                            End If
                                        Next

                                        For i3 = 1 To Session("PLOTBilateralLastYear") - Session("PLOTBilateralFirstYear") + 1
                                            For m = 0 To x0s.Count - 1
                                                If m = 0 Then fm1 = fm
                                                Dim iPlus = If(iBilateralFirstYear(m + 1) = 2200, 0, iBilateralFirstYear(m + 1) - Session("PLOTBilateralFirstYear"))
                                                'ttt - difference in PLOT years between Vars inside Formula
                                                Dim fy As Integer = 0
                                                iPlus = adjYear
                                                Dim pl = MaxFirstYear - FirstYearFormu((m + 1) & "-" & Session("iCOUNTE"))
                                                If (i3 - iPlus + pl < 1) OrElse dataforFormula(m + 1)(i3 - iPlus + pl, Session("iCOUNTE")) Is Nothing OrElse dataforFormula(m + 1)(i3 - iPlus + pl, Session("iCOUNTE")) Is DBNull.Value Then
                                                    fm1 = If(f.UseNullsAsZeros, "0", "NULL")
                                                    Exit For
                                                End If
                                                Dim c = "(" & If(dataforFormula(m + 1)(i3 - iPlus + pl, Session("iCOUNTE")), If(f.UseNullsAsZeros, "0", "NULL")).ToString & ")" ' parenthese to enable minus sign
                                                fm1 = fm1.Replace(x0s(m), If(c.Trim = "()", If(f.UseNullsAsZeros, "(0)", "(NULL)"), c))
                                            Next
                                            'Select (0)/(0) as res-> Overflow
                                            'ttt- this check is not correct- so commented out
                                            'If fm1.Contains("/") Then
                                            '    Dim f1 As String = fm1.Split("/")(1)
                                            '    f1 = f1.Replace("NULL", "0")
                                            '    SetDataTablefromAccess(Session("PutFdrvRun") & "DataDictDyadic.mdb", "Select " & f1 & " As res", "", rsFormu)
                                            '    If rsFormu.Rows(0).Item(0) = 0 Then
                                            '        fm1 = If(f.UseNullsAsZeros, "0", "NULL")
                                            '    End If
                                            'End If
                                            Dim sf As String = "Select " & fm1 & " As res"
                                            SetDataTablefromAccess(Session("PutFdrvRun") & "DataDictDyadic.mdb", sf, "", rsFormu)
                                            Session("mPLOTFormula")(i3, Session("iCOUNTE")) = rsFormu.Rows(0).Item(0)
                                            Dim key = MakePlotKey(i3, Session("iCOUNTE"))

                                            If Not mPLOTDictFormula.ContainsKey(key) Then
                                                mPLOTDictFormula.Add(key, i3 + Session("PLOTBilateralFirstYear") - 1)
                                            End If
                                        Next
                                    End If

                                    If Session("aFormulas")(i + 1) Is Nothing Then
                                        Session("aFormulas")(i + 1) = Session("aFormulas")(i)
                                    End If
                                Else

                                    For k = iStart To iLast
                                        If k < iHalfWayIdx - iHalfWayStart OrElse Not bTooComplex Then
                                            If bForecast Then

                                                Session("mPLOT")(k - iStart + 1 + LagYrs, Session("iCOUNTE")) = rsDY.Rows(k - iStart + 1 + LagYrs - 1).Item(3) 'rsDY.Columns(k).Value
                                                mPLOTDict.Add(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE")), rsDY.Rows(k - iStart + 1 + LagYrs - 1).Item(2))
                                                Dim kk = MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE"))

                                            Else
                                                Session("mPLOT")(k - iStart + 1 + LagYrs, Session("iCOUNTE")) = rsDY.Rows(0).Item(k) 'rsDY.Columns(k).Value
                                                mPLOTDict.Add(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE")), rsDY.Columns(k).ColumnName.Replace("Y", ""))
                                                Dim kk = MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE"))

                                            End If
                                        Else
                                            If bForecast Then
                                                Session("mPLOT")(k - iStart + 1 + LagYrs, Session("iCOUNTE")) = rsDY2.Rows(k - iHalfWayIdx + iHalfWayStart - 1).Item(3)
                                                mPLOTDict.Add(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE")), rsDY2.Rows(k - iHalfWayIdx + iHalfWayStart - 1).Item(2))
                                                Dim kk = MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE"))
                                            Else
                                                Session("mPLOT")(k - iStart + 1 + LagYrs, Session("iCOUNTE")) = rsDY2.Rows(0).Item(k - iHalfWayIdx + iHalfWayStart) ' rsDY2.Columns(k - iHalfWayIdx + iHalfWayStart).Value
                                                mPLOTDict.Add(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE")), rsDY.Columns(k - iHalfWayIdx + iHalfWayStart).ColumnName.Replace("Y", ""))
                                                Dim kk = MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE"))
                                            End If
                                        End If
                                    Next k
                                End If
                            End If 'end of if using weight avg or else
                        Else
                            If bFormula Then
                                For k = 1 To UBound(PLOTAll, 1)
                                    PLOTAll(k, graphno) = Nothing
                                Next k
                                '
                                'frm_computations.LoadIdxDataAll = CurrNumbCompVar
                                'frm_computations.LoadDataAll = PLOTAll
                                '->Loading Data.....

                                fm = f.Formula_Renamed
                                x0s = Xs(fm)
                                If bStartedNewSeries Then
                                    bStartedNewSeries = False
                                    ReDim dataAll(f.disVar.disSize())
                                    ReDim dataforFormula(f.disVar.disSize())
                                Else
                                    ReDim Preserve dataAll(f.disVar.disSize())
                                End If

                                dataAll(CurrNumbCompVar) = PLOTAll
                                Session("dataAll") = dataAll
                                Session("bCompAll") = True
                            Else
                                For k = 1 To UBound(Session("mPLOT"), 1)
                                    Session("mPLOT")(k, Session("iCOUNTE")) = Nothing
                                Next k
                            End If
                        End If 'end of if recordset is not empty or else fill the plot with nulls

                        If bDyadic Then
                            'jrs 2016/09/22
                            If bForecast Then
                                If IsNumeric(sActor) Then
                                    Session("mLegends")(1, Session("iCOUNTE")) = GRPS(1, CInt(sActor))
                                Else
                                    Session("mLegends")(1, Session("iCOUNTE")) = sActor
                                End If
                                If IsNumeric(sPartner) Then
                                    Session("mLegends")(2, Session("iCOUNTE")) = GRPS(1, CInt(sPartner))
                                Else
                                    Session("mLegends")(2, Session("iCOUNTE")) = sPartner
                                End If
                                'jrs 2016/10/26
                                'ttt
                                Session("mLegends")(5, Session("iCOUNTE")) = ""

                                If Session("dbDyFor")(ss).ToLower = (DataFileDir$ & "\ifsForDyadic.db").ToLower Then
                                    Session("mLegends")(5, Session("iCOUNTE")) = "Base"
                                Else
                                    'Session("mLegends")(5, Session("iCOUNTE")) = Replace(Replace(Session("dbDyFor")(ss), Session("PutFdrvRun") & "IFsForDyadic", ""), ".db", "")
                                    Session("mLegends")(5, Session("iCOUNTE")) = Session("dbDyFor")(ss).ToString.Substring(Session("dbDyFor")(ss).ToString.LastIndexOf("\") + 1).Replace(".db", "")
                                End If
                            Else
                                Session("mLegends")(1, Session("iCOUNTE")) = sActor
                                Session("mLegends")(2, Session("iCOUNTE")) = sPartner
                            End If
                        ElseIf bMonadicActor Then
                            Session("mLegends")(1, Session("iCOUNTE")) = sActor
                            Session("mLegends")(2, Session("iCOUNTE")) = ""
                        Else
                            Session("mLegends")(1, Session("iCOUNTE")) = sPartner
                            Session("mLegends")(2, Session("iCOUNTE")) = ""
                        End If
                        Session("mLegends")(3, Session("iCOUNTE")) = sUnits
                        Session("ChartUnits") = sUnits
                        Session("mLegends")(4, Session("iCOUNTE")) = sVar
                    Else 'if it's a dyadic without partner, or a monadyc partner with no partner
                        If bFormula Then
                            For k = 1 To UBound(PLOTAll, 1)
                                PLOTAll(k, j + 1) = Nothing
                            Next k
                            'frm_computations.LoadIdxDataAll = CurrNumbCompVar
                            'frm_computations.LoadDataAll = PLOTAll
                            Session("iCOUNTE") = Session("iCOUNTE") + 1
                        End If
                    End If 'if it's not a dyadic without partner, or a monadyc partner with no partner
                    'jrs 2020/07/22
                    If bForecastH Then
                        'Load Forecast part
                        LoadForecast4FH(sVar, bGrpActor, bGrpPartner, sActor, sPartner, Session("dbDyFor"), ss, Session("BilateralFirstYear"), sAggRule, dbIFs, sUnits, mPLOTDict)
                    End If
                Next j
                If Not bFormula Then
                    'jrs 2021/02/04 added or bForecastH
                    'jrs 2016/10/26
                    If Not (bForecast Or bForecastH) OrElse ss = FileSelCount - 1 Then
                        'jrs 2020/07/22
                        If bForecastH Then
                            bForecastH = False
                        End If

                        Exit Do
                    Else
                        ss = ss + 1
                    End If
                Else
                    'Case bFormula -------------------

                    Session("iCOUNTE") = Session("iCOUNTE") - lstSelDyads.Items.Count
                    CurrNumbCompVar = CurrNumbCompVar + 1
                    If CurrNumbCompVar > NumbCompVar Then
                        '->Finish
                        Session("iCOUNTE") = Session("iCOUNTE") + lstSelDyads.Items.Count
                        Dim oldFirstYear As Integer
                        oldFirstYear = Session("FirstYear")
                        Session("FirstYear") = Session("BilateralFirstYear")
                        'frm_computations.Public_Exit
                        Session("FirstYear") = oldFirstYear
                        Dim ChartUnits As String = ""
                        Dim cUs As New List(Of String)
                        For k = Session("iCOUNTE") - lstSelDyads.Items.Count + 1 To Session("iCOUNTE")
                            Session("mLegends")(1, k) = Session("sActors")(k - Session("iCOUNTE") + lstSelDyads.Items.Count)
                            Session("mLegends")(2, k) = Session("sPartners")(k - Session("iCOUNTE") + lstSelDyads.Items.Count)
                            If Not Session("aFormulas")(i + 1) Is Nothing Then
                                Session("mLegends")(3, k) = Session("aFormulas")(i + 1).Unit
                            ElseIf Not Session("aFormulas")(i) Is Nothing Then
                                Session("mLegends")(3, k) = Session("aFormulas")(i).Unit
                            End If

                            If Not cUs.Contains(Session("mLegends")(3, k)) Then
                                ChartUnits &= If(ChartUnits = "", "", "-") & Session("mLegends")(3, k)
                                cUs.Add(Session("mLegends")(3, k))
                            End If

                            If Not Session("aFormulas")(i + 1) Is Nothing Then
                                Session("mLegends")(4, k) = Session("aFormulas")(i + 1).Name
                            ElseIf Not Session("aFormulas")(i) Is Nothing Then
                                Session("mLegends")(4, k) = Session("aFormulas")(i).Name
                            End If

                            If bForecast Then
                                'jrs 2016/10/26
                                'ttt
                                Session("mLegends")(5, k) = ""

                                'If Session("dbDyFor")(ss) = DataFileDir$ & "ifsForDyadic.mdb" Then
                                If Session("dbDyFor")(ss).ToLower = (DataFileDir$ & "\ifsForDyadic.db").ToLower Then
                                    Session("mLegends")(5, k) = "Base"
                                Else
                                    'Session("mLegends")(5, k) = Replace(Replace(Session("dbDyFor")(ss), Session("PutFdrvRun") & "IFsForDyadic", ""), ".db", "")
                                    Session("mLegends")(5, k) = Session("dbDyFor")(ss).ToString.Substring(Session("dbDyFor")(ss).ToString.LastIndexOf("\") + 1).Replace(".db", "")

                                End If
                            Else
                                Session("mLegends")(5, k) = ""
                            End If
                        Next k
                        Session("ChartUnits") = ChartUnits
                        'jrs 2016/10/26
                        If Not bForecast OrElse ss = FileSelCount - 1 Then
                            Exit Do
                        Else
                            ss = ss + 1
                        End If
                    Else
                        'For next series
                        If Session("aFormulas")(i + 1).disVar().displayDet(CurrNumbCompVar).ToString.Contains("|") Then
                            'case gravity
                            sVar = Session("aFormulas")(i + 1).disVar().displayDet(CurrNumbCompVar).Split("|")(0).Trim.SubString(5)
                        Else
                            'case computation
                            sVar = Session("aFormulas")(i + 1).disVar().displayDet(CurrNumbCompVar).Split("-")(0).Trim.SubString(5)
                        End If

                        bDyadic = (Mid(Session("aFormulas")(i + 1).disVar().displayDet(CurrNumbCompVar), 1, 4) = "(DY)")
                        bMonadicActor = (Mid(Session("aFormulas")(i + 1).disVar().displayDet(CurrNumbCompVar), 1, 4) = "(MA)")

                        sTbl = "Series" & sVar
                    End If
                End If

                If bFormula Then
                    Session("PLOTBilateralLastYear") = Math.Max(Session("PLOTBilateralLastYear"), Session("BilateralLastYear"))
                    Session("PLOTBilateralFirstYear") = Math.Min(Session("PLOTBilateralFirstYear"), Session("BilateralFirstYear"))
                End If

            Loop 'each term in the formula, 'jrs 2016/10/26 now using the loop for multiple scenarios

            Session("PLOTBilateralLastYear") = Math.Max(Session("PLOTBilateralLastYear"), Session("BilateralLastYear"))
            Session("PLOTBilateralFirstYear") = Math.Min(Session("PLOTBilateralFirstYear"), Session("BilateralFirstYear"))

            If bFormula Then
                For Each kk As String In mPLOTDictFormula.Keys
                    If Not (mPLOTDict.ContainsKey(kk)) Then
                        mPLOTDict.Add(kk, mPLOTDictFormula(kk))
                    End If
                Next
            End If
            '
            'dataAll : used only for one SELECTED VAR

        Next i 'i To lstSelSeries.Items.Count - 1

        Dim TotalFormuRep As Integer = 0

        If If(Session("BL_HasFormulaReport"), False) = True Then
            'has at least one Formula
            For h = 1 To Session("iCOUNTE")
                If FormuDataDone(1, h) = True Then
                    For k = 1 To UBound(Session("mPLOT"), 1)
                        Session("mPLOT")(k, h) = Session("mPLOTFormula")(k, h)
                    Next k
                End If
            Next
        End If

        Session("mPLOTDict") = mPLOTDict

        'ttt Remove NULLS at Beginning and End of Reported Years

        Dim NullBefore As Integer = 0
        For k = 1 To UBound(Session("mPLOT"), 1)
            Dim IsYearPLOTNull As Boolean = True
            For h = 1 To Session("iCOUNTE")
                'Must Check the year, Not the order
                If (Not Session("mPLOTDict").ContainsKey(h & "-1")) OrElse Session("mPLOTDict")(h & "-1") > Session("PLOTBilateralFirstYear") OrElse (Session("mPLOT")(k, h) Is Nothing OrElse Session("mPLOT")(k, h).ToString = "") Then
                    IsYearPLOTNull = True
                Else
                    IsYearPLOTNull = False
                    Exit For
                End If
            Next

            If Not IsYearPLOTNull Then
                NullBefore = k - 1
                Exit For
            End If
        Next

        'Apply NULLBefore===
        If NullBefore > 0 Then
            Dim Lags(Session("iCOUNTE")) As Integer
            For h = 1 To Session("iCOUNTE")
                If Session("mPLOTDict").ContainsKey(CStr(h) & "-1") Then
                    Lags(h) = Session("PLOTBilateralFirstYear") - Session("mPLOTDict")(CStr(h) & "-1")
                End If
            Next

            'ttt Brought down
            Session("PLOTBilateralFirstYear") = Session("PLOTBilateralFirstYear") + NullBefore
            Session("mPLOTDict") = New Dictionary(Of String, Integer)
            For k = 1 To UBound(Session("mPLOT"), 1)
                For h = 1 To Session("iCOUNTE")
                    If k + NullBefore + Lags(h) <= UBound(Session("mPLOT"), 1) Then
                        Session("mPLOT")(k, h) = Session("mPLOT")(k + NullBefore + Lags(h), h)
                    Else
                        Session("mPLOT")(k, h) = Nothing
                    End If

                    Dim key = MakePlotKey(k, h)
                    Session("mPLOTDict").Add(key, Session("PLOTBilateralFirstYear") + k - 1)

                Next
            Next

        End If


        Dim NullAfter As Integer = 0

        For k = (Session("PLOTBilateralLastYear") - Session("PLOTBilateralFirstYear") + 1) To 1 Step -1
            Dim IsYearPLOTNull As Boolean = True
            For h = 1 To Session("iCOUNTE")
                If (Session("mPLOT")(k, h) Is Nothing OrElse Session("mPLOT")(k, h).ToString = "") Then
                    IsYearPLOTNull = True
                Else
                    IsYearPLOTNull = False
                    Exit For
                End If
            Next
            If Not IsYearPLOTNull Then
                NullAfter = k - 1
                Exit For
            End If
        Next
        Session("PLOTBilateralLastYear") = Session("PLOTBilateralFirstYear") + NullAfter

        'END- Remove NULLS at Beginning and End of Reported Years
        ReDim Session("CompBackup")(UBound(Session("mPLOT"), 1), Session("iCOUNTE"))
        ReDim Session("CompLabelBackup")(4, Session("iCOUNTE"))

        'Save Labels for Format
        For i = 1 To Session("iCOUNTE")
            For k = 1 To 5
                Session("mFLegends(k, i)") = Session("mLegends")(k, i)
            Next k
        Next i


        CheckBilateralLastYearWData()

        If FileSelCount > 1 Then
            ReDim Session("RestorePos")(Session("iCOUNTE"))
            For i = 1 To Session("iCOUNTE")
                Session("RestorePos")(i) = i
            Next i
            'SortByDyad1stSce2nd
        End If
        'rr10 add lines



    End Sub
    Private Sub lstDim1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDim1.SelectedIndexChanged
        Dim lst = DirectCast(sender, ListBox)
        If lst.SelectedIndex < 0 Then Return
        Dim i As Integer, j As Integer
        Session("sActor") = lst.Items(lst.SelectedIndex).Value
        'If lst.SelectedIndex > 0 Then
        '    Session("sActor") = lst.Items(lst.SelectedIndex).Value
        'Else
        '    For i = 0 To lst.Items.Count - 1
        '        If UCase(lst.Items(lst.SelectedIndex).Value) = UCase(lst.Items(i).Value) Then
        '            Session("sActor") = lst.Items(i).Value
        '            cmb_DIM1.Text = Session("sActor")
        '            Exit For
        '        End If
        '    Next i
        'End If
        Session("bDecGrpActor") = False
        Session("bGListActor") = False
        If optGroupDec.Checked Then
            Session("bDecGrpActor") = True
        ElseIf optGList.Checked Then
            Session("bGListActor") = True
        End If
        Session("bDecGrpPartner") = False
        Session("bGListPartner") = False
        If lstDim1p.Visible = False Then
            Session("sPartner") = "-"
            Session("bDecGrpPartner") = False
            Session("bGListPartner") = False
        End If
        If Session("sActor") > "" AndAlso Session("sPartner") > "" Then
            If (Not Session("bDecGrpActor")) AndAlso (Not Session("bGListActor")) AndAlso (Not Session("bDecGrpPartner")) AndAlso (Not Session("bGListPartner")) Then
                If Session("sPartner") = "-" Then
                    lstSelDyads.Items.Add(Session("sActor"))
                Else
                    lstSelDyads.Items.Add(Session("sActor") & " - " & Session("sPartner"))
                End If
            Else
                '        Dim dbIFs As Database
                'Set dbIFs = OpenDatabase(IFs_MDB)
                Dim sActor2 As String, sPartner2 As String
                Dim ActorsCount As Integer, PartnersCount As Integer
                Dim rsActors As DataTable = New DataTable, rsPartners As DataTable = New DataTable
                Dim sql As String
                ActorsCount = 1
                PartnersCount = 1
                sActor2 = Session("sActor")
                sPartner2 = Session("sPartner")
                If Session("bDecGrpActor") Then

                    sql = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & Session("sActor") & """ ORDER BY R.Country"

                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    ActorsCount = rsActors.Rows.Count
                    'jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    '    ActorsCount = rsActors.Rows.Count
                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    ActorsCount = 0
                    '    While (vSQLiteReader.Read())
                    '        ActorsCount = ActorsCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If


                End If
                If Session("bGListActor") Then
                    sql = "SELECT G.Region as Ctry " &
                    "FROM GList AS G, GListID AS GI " &
                    "WHERE G.GListCode = GI.Code AND G.Group = True AND GI.Description = """ & Session("sActor") & """"
                    sql = sql & " UNION SELECT R.Country as Ctry " &
                    "FROM " & Regionalization & " AS R, " & RegionIDNew & " AS RI, GList AS G, GListID AS GI " &
                    "WHERE R.NewRegion = RI.RegionNum AND RI.RegionName = G.Region AND G.GListCode = GI.Code " &
                    "AND G.Group = False AND GI.Description = """ & Session("sActor") & """"

                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    ActorsCount = rsActors.Rows.Count
                    'jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    '    ActorsCount = rsActors.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    ActorsCount = 0
                    '    While (vSQLiteReader.Read())
                    '        ActorsCount = ActorsCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If


                End If
                If Session("bDecGrpPartner") Then
                    sql = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & Session("sPartner") & """ ORDER BY R.Country"

                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    PartnersCount = rsPartners.Rows.Count
                    'jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    '    PartnersCount = rsPartners.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    PartnersCount = 0
                    '    While (vSQLiteReader.Read())
                    '        PartnersCount = PartnersCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If




                End If
                If Session("bGListPartner") Then
                    sql = "SELECT G.Region as Ctry " &
                    "FROM GList AS G, GListID AS GI " &
                    "WHERE G.GListCode = GI.Code AND G.Group = True AND GI.Description = """ & Session("sPartner") & """"
                    sql = sql & " UNION SELECT R.Country as Ctry " &
                    "FROM " & Regionalization & " AS R, " & RegionIDNew & " AS RI, GList AS G, GListID AS GI " &
                    "WHERE R.NewRegion = RI.RegionNum AND RI.RegionName = G.Region AND G.GListCode = GI.Code " &
                    "AND G.Group = False AND GI.Description = """ & Session("sPartner") & """"

                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    PartnersCount = rsPartners.Rows.Count

                    'jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    '    PartnersCount = rsPartners.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    PartnersCount = 0
                    '    While (vSQLiteReader.Read())
                    '        PartnersCount = PartnersCount + 1

                    '    End While

                    '    vSQLiteCon.Close()

                    'End If




                End If
                For i = 1 To ActorsCount
                    If ActorsCount > 1 Then
                        sActor2 = rsActors.Rows(i - 1).Item(0) ' rsActors.Fields(0)
                        Continue For ' rsActors.MoveNext
                    End If
                    If lstDim1p.Visible = False Then
                        lstSelDyads.Items.Add(sActor2)
                    Else
                        For j = 1 To PartnersCount
                            If PartnersCount > 1 Then
                                sPartner2 = rsPartners.Rows(j - 1).Item(0) '.Fields(0)
                                Continue For 'rsPartners.MoveNext
                            End If
                            lstSelDyads.Items.Add(sActor2 & " - " & sPartner2)
                        Next j
                    End If
                Next i

            End If
            Session("sActor") = ""
            Session("sPartner") = ""
        End If
    End Sub
    Private Sub lstDim1p_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDim1p.SelectedIndexChanged
        'no event
        'lstDim1_SelectedIndexChanged(lstDim1, New System.EventArgs)
        '---
        Dim i As Integer, j As Integer
        Dim lst = DirectCast(sender, ListBox)
        If lst.SelectedIndex < 0 Then Return
        Session("sPartner") = lst.Items(lst.SelectedIndex).Value
        Session("bDecGrpPartner") = False
        Session("bGListPartner") = False
        If optGroupDec.Checked Then
            Session("bDecGrpPartner") = True
        ElseIf optGList.Checked Then
            Session("bGListPartner") = True
        End If

        If Session("sActor") > "" AndAlso Session("sPartner") > "" Then
            If (Not Session("bDecGrpActor")) AndAlso (Not Session("bGListActor")) AndAlso (Not Session("bDecGrpPartner")) AndAlso (Not Session("bGListPartner")) Then
                lstSelDyads.Items.Add(Session("sActor") & " - " & Session("sPartner"))
            Else

                Dim sActor2 As String, sPartner2 As String
                Dim ActorsCount As Integer, PartnersCount As Integer
                Dim rsActors As DataTable = New DataTable, rsPartners As DataTable = New DataTable
                Dim sql As String
                ActorsCount = 1
                PartnersCount = 1
                sActor2 = Session("sActor")
                sPartner2 = Session("sPartner")
                If Session("bDecGrpActor") Then
                    sql = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & Session("sActor") & """ ORDER BY R.Country"

                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    ActorsCount = rsActors.Rows.Count
                    ''jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    '    ActorsCount = rsActors.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    ActorsCount = 0
                    '    While (vSQLiteReader.Read())
                    '        ActorsCount = ActorsCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If





                End If
                If Session("bGListActor") Then
                    sql = "SELECT G.Region as Ctry " &
                    "FROM GList AS G, GListID AS GI " &
                    "WHERE G.GListCode = GI.Code AND G.Group = True AND GI.Description = """ & Session("sActor") & """"
                    sql = sql & " UNION SELECT R.Country as Ctry " &
                    "FROM " & Regionalization & " AS R, " & RegionIDNew & " AS RI, GList AS G, GListID AS GI " &
                    "WHERE R.NewRegion = RI.RegionNum AND RI.RegionName = G.Region AND G.GListCode = GI.Code " &
                    "AND G.Group = False AND GI.Description = """ & Session("sActor") & """"


                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    ActorsCount = rsActors.Rows.Count
                    ''jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                    '    ActorsCount = rsActors.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    ActorsCount = 0
                    '    While (vSQLiteReader.Read())
                    '        ActorsCount = ActorsCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If



                End If
                If Session("bDecGrpPartner") Then
                    sql = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & Session("sPartner") & """ ORDER BY R.Country"


                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    PartnersCount = rsPartners.Rows.Count
                    ''jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    '    PartnersCount = rsPartners.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    PartnersCount = 0
                    '    While (vSQLiteReader.Read())
                    '        PartnersCount = PartnersCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If



                End If
                If Session("bGListPartner") Then
                    sql = "SELECT G.Region as Ctry " &
                    "FROM GList AS G, GListID AS GI " &
                    "WHERE G.GListCode = GI.Code AND G.Group = True AND GI.Description = """ & Session("sPartner") & """"
                    sql = sql & " UNION SELECT R.Country as Ctry " &
                    "FROM " & Regionalization & " AS R, " & RegionIDNew & " AS RI, GList AS G, GListID AS GI " &
                    "WHERE R.NewRegion = RI.RegionNum AND RI.RegionName = G.Region AND G.GListCode = GI.Code " &
                    "AND G.Group = False AND GI.Description = """ & Session("sPartner") & """"


                    'jrs 2023/02/17
                    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    PartnersCount = rsPartners.Rows.Count
                    'jam 2022/12/19
                    'If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                    '    SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                    '    PartnersCount = rsPartners.Rows.Count

                    'Else

                    '    vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                    '    vSQLiteCon = New SqliteConnection(vConnString)
                    '    vSQLiteCon.Open()

                    '    vSQLiteCommand = vSQLiteCon.CreateCommand()
                    '    vSQLiteCommand.CommandText = sql
                    '    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                    '    PartnersCount = 0
                    '    While (vSQLiteReader.Read())
                    '        PartnersCount = PartnersCount + 1

                    '    End While
                    '    vSQLiteCon.Close()


                    'End If



                End If
                For i = 1 To ActorsCount
                    If ActorsCount > 1 Then
                        sActor2 = rsActors.Rows(i - 1).Item(0) '.Fields(0)
                        'Continue For
                    End If
                    For j = 1 To PartnersCount
                        If PartnersCount > 1 Then
                            sPartner2 = rsPartners.Rows(j - 1).Item(0)
                        End If
                        lstSelDyads.Items.Add(sActor2 & " - " & sPartner2)
                    Next j
                Next i
            End If
            'Session("sActor") = ""
            Session("sPartner") = ""
        End If
    End Sub
    Private Sub cmdSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        Session("SearchVarList") = "Variables"
        Session("DoingHistory") = Not lstFiles.Visible
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_SearchVarBilat.aspx")
    End Sub
    Public Sub LoadSearch(ByVal sVar As String)
        '---Same as lstSeries_SelectedIndexChanged----------------
        Dim sType As String = "MA" '/MP
        If Not (Session("OrigDyadic") Is Nothing) Then
            sType = Session("OrigDyadic")
        End If
        If lstSelSeries.Items.FindByText("(" & sType & ") " & sVar) Is Nothing Then
            'ttt VB6 use cmb_VarName (not cmb_VarNameMonadyc), maybe mistake
            If If(Session("bAddGravityAdj"), False) Then
                AddGravityAdj(sVar, sType)
                Exit Sub
            ElseIf If(Session("bAddGravity"), False) Then
                AddGravityVar(sVar, sType)
            End If
            lstSelSeries.Items.Add("(" & sType & ") " & sVar)
            Session("curlstSelSeries") = lstSelSeries

            ReDim Preserve Session("sAggRulesSelected")(lstSelSeries.Items.Count)

            ReDim Preserve Session("bDyadicSeries")(lstSelSeries.Items.Count)
            ReDim Preserve Session("bMonadicActorSeries")(lstSelSeries.Items.Count)
            Session("bDyadicSeries")(lstSelSeries.Items.Count - 1) = True
            If sType = "MA" Then
                Session("bMonadicActorSeries")(lstSelSeries.Items.Count - 1) = True
            End If
        End If
    End Sub
    Private Sub LoadPreviousSelected()
        Dim lst = DirectCast(Session("curlstSelSeries"), ListBox)
        For Each it As ListItem In lst.Items
            lstSelSeries.Items.Add(it)
        Next


    End Sub
    Private Sub cmdGravity_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGravity.Click
        Dim mCOUNTE As Integer = 0
        mCOUNTE = lstSelSeries.Items.Count
        If mCOUNTE = 0 Then
            'MsgBox "You need to select at least one variable to create a Gravity Variable", vbCritical, "Gravity Variable Creation"
            Exit Sub
        End If

        'Check that formulas are saved
        Dim i As Integer, pos As Integer
        Dim rs As DataTable = New DataTable

        Dim db = Session("PutFdrvRun") & "DataDictDyadic.mdb"
        'jam 2022/12/19
        If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") = "True" Then
            db = Session("PutFdrvRun") & "DataDictDyadic.db"

        End If
        If Not Session("aFormulas") Is Nothing Then
            For i = 1 To mCOUNTE

                pos = InStr(1, lstSelSeries.Items(i - 1).Text, " - ")
                If pos = 0 Then
                    If UBound(Session("aFormulas")) >= i Then
                        If Not (Session("aFormulas")(i) Is Nothing) Then
                            If Session("aFormulas")(i).Name = lstSelSeries.Items(i - 1).Text Then

                                'jam 2022/12/19
                                If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                                    SetDataTablefromAccess(db, "SELECT COUNT(*) FROM FormulaVars WHERE FormulaName = """ & Session("aFormulas")(i).Name & """", "", rs)
                                    If rs.Rows(0).Item(0) = 0 Then
                                        'MsgBox "You need to save any created formula before using it for a Gravity variable.", vbCritical, "Gravity Variable Creation"
                                        Exit Sub
                                    End If

                                Else
                                    Dim vCount As Integer
                                    vConnString = "DataSource=" + db & ";"

                                    vSQLiteCon = New SqliteConnection(vConnString)
                                    vSQLiteCon.Open()

                                    vSQLiteCommand = vSQLiteCon.CreateCommand()
                                    vSQLiteCommand.CommandText = "SELECT COUNT(*) as rCount FROM FormulaVars WHERE FormulaName = """ & Session("aFormulas")(i).Name & """"
                                    vSQLiteReader = vSQLiteCommand.ExecuteReader()


                                    While (vSQLiteReader.Read())
                                        vCount = vSQLiteReader("rCount")

                                    End While
                                    vSQLiteCon.Close()

                                    If vCount = 0 Then
                                        'MsgBox "You need to save any created formula before using it for a Gravity variable.", vbCritical, "Gravity Variable Creation"
                                        Exit Sub
                                    End If



                                End If


                            End If
                        End If
                    End If
                End If
                '(MA) Var...
                'SetFormulaType(lstSelSeries.Items(i - 1).Text.Substring(4).Trim, lstSelSeries.Items(i - 1).Text.Substring(0, 4).Trim, mCOUNTE)
            Next i
        End If

        Session("bEditGravity") = False
        Session("bSaved") = False
        Session("bAddGravity") = True
        Session("SelSeriesItems") = lstSelSeries.Items
        Session("GravityParas") = Nothing
        Session("bClickedDGravitySave") = Nothing
        'Load Index form where user will use default values to start creating Index
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionDGravity.aspx")
        Session("bNewFormula") = False
    End Sub
    Public Sub mnuEditGravity_Click()
        Dim lst As ListBox = DirectCast(Session("curlstSelSeries"), ListBox)
        'Check that index is loaded
        If lst Is Nothing OrElse lst.Items.Count = 0 Then
            'jrs 2009/08/04 added how to load index
            '        MsgBox "The Gravity variable should be loaded in order to be edited." & Chr(vbKeyReturn) &
            '"Please go to menu option " & Replace(mnuSerType.Caption, "&", "") & "\" & Replace(mnuShowSavedFormulas.Caption, "&", "") &
            '" and select a Gravity variable from the list to load it.",
            'vbCritical, "Edit Gravity variable"
            'Response.Write("<script>alert('Hello');</script>")
            Dim msg = "The Gravity variable should be loaded in order to be edited.\r" &
            "Please go to menu option Series Type\\ShowSavedFormulas" &
            " and select a Gravity variable from the list to load it."
            ShowAlert(msg)
            Exit Sub
        End If

        If lst.Items.Count > 1 Then
            'jrs 2009/08/04 added how to load index
            Dim msg = "The Gravity variable should be the only thing loaded in order to be edited.\r" &
            "Please Clear the Plot and go to menu option Series Type\\ShowSavedFormulas" &
            " and select a Gravity variable from the list to load it."
            ShowAlert(msg)
            Exit Sub
        End If
        Dim bValid As Boolean
        bValid = False
        If Session("aFormulas").Length > 1 AndAlso Not (Session("aFormulas")(1) Is Nothing) Then
            If Session("aFormulas")(1).Name = lst.Items(0).Text Then
                bValid = True
            End If
        End If

        If Not bValid Then
            ShowAlert("The selected variable is not a valid Gravity variable.")
            Exit Sub
        End If

        'Set Edit Gravity Flag On
        Session("bEditGravity") = True
        'jrs 2008/01/24
        'Prepare variables as if selected from menu
        Dim sVarName As String
        sVarName = Session("aFormulas")(1).Name
        Dim iYr As Integer, pos As Integer
        pos = InStr(Session("aFormulas")(1).Formula_Renamed, "Z?")
        Dim oldFirstYear As Integer
        oldFirstYear = Session("FirstYear")
        If pos > 0 Then
            iYr = CInt(Mid(Session("aFormulas")(1).Formula_Renamed, pos + 2, 4))
            Session("FirstYear") = iYr
        End If
        'Load frm_BilateralInteractionDisplayGravity
        'Load variables
        Dim rsDet As DataTable = New DataTable
        Dim dbDD = Session("PutFdrvRun") & "DataDictDyadic.mdb"
        Dim db = Session("PutFdrvRun") & "DataDictDyadic.mdb"

        'jam 2022/12/19
        If Not (Session("mnuSQLite") Is Nothing) Or Session("mnuSQLite") = "True" Then
            dbDD = Session("PutFdrvRun") & "DataDictDyadic.db"
            db = Session("PutFdrvRun") & "DataDictDyadic.db"

        End If

        Dim rs As DataTable = New DataTable

        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            SetDataTablefromAccess(db, "SELECT * FROM GravityDet WHERE GravityVarName = """ & sVarName & """", "", rs)
            If rs.Rows.Count = 0 Then
                ShowAlert("The current formula was not saved as a Gravity Variable, you will have create a new variable variable or edit this one as a regular formula.")
            End If
        Else
            Dim vCount As Integer
            vConnString = "DataSource=" + db & ";"

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "SELECT * FROM GravityDet WHERE GravityVarName = """ & sVarName & """"
            vSQLiteReader = vSQLiteCommand.ExecuteReader()

            vCount = 0
            While (vSQLiteReader.Read())
                vCount = vCount + 1

            End While
            vSQLiteCon.Close()

            If vCount = 0 Then
                ShowAlert("The current formula was not saved as a Gravity Variable, you will have create a new variable variable or edit this one as a regular formula.")
            End If



        End If

        Dim icol As Integer
        icol = rs.Rows.Count
        Dim bAdjRes As Boolean
        'ttt Prepare variable for frm_BilateralInteractionDisplayGravity
        ReDim Session("bFormulas")(icol)
        ReDim Session("sTypes")(icol)
        Dim i As Integer
        For i = 1 To icol
            setbFormulas(rs.Rows(i - 1).Item("Formula"), i)
            setsTypes(rs.Rows(i - 1).Item("VarType"), i)
            'if the variable is a formula needs to be loaded in position i
            If rs.Rows(i - 1).Item("Formula") Then
                'Read Formula
                Dim sql As String = "SELECT v.Formula, d.Code, d.VarName, v.Units FROM FormulaVars as v, FormulaVarsDet as d WHERE v.FormulaName = d.FormulaName AND d.FormulaName = """ & rs.Rows(i - 1).Item("VarName") & """"
                SetDataTablefromAccess(dbDD, sql, "", rsDet)
                'Load it into object
                Dim f As Formula = New Formula
                f.init(rsDet.Rows(0).Item("Formula"))
                f.Name = rs.Rows(i - 1).Item("VarName")
                f.Unit = rsDet.Rows(0).Item("Units")
                f.begAll = 1
                f.endAll = 1
                If InStr(rsDet.Rows(0).Item("Formula"), "-Z") > 0 Then
                    bAdjRes = True
                End If
                For j = 0 To rsDet.Rows.Count - 1
                    f.disVar.setDataDetS(rsDet.Rows(j).Item("Code"), rsDet.Rows(j).Item("VarName"))
                Next
                If i > UBound(Session("aFormulas")) Then
                    ReDim Preserve Session("aFormulas")(lst.Items.Count)
                End If
                Session("aFormulas")(i) = f
                LoadFormula(f)

            End If
        Next i

        Dim paras = New Dictionary(Of String, String)
        paras.Add("FormulaName", sVarName)
        Session("bSaved") = False
        Session("GravityParas") = paras
        Session("bClickedDGravitySave") = Nothing
        Session("SelSeriesItems") = lst.Items
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionDGravity.aspx")
        Session("NeedFileWrite") = True
        Session("FirstYear") = oldFirstYear
    End Sub
    Public Sub setsTypes(sVal As String, idx As Integer)
        Session("sTypes")(idx) = Trim(sVal)
    End Sub
    Public Sub setbFormulas(bVal As Boolean, idx As Integer)
        Session("bFormulas")(idx) = bVal
    End Sub
    Public Sub LoadFormula(ByRef X As Formula)
        'ttt copied from frm_computations
        Dim f = X
        Session("ComputationsDimensionSize") = f.DimensionSize
        Dim Data = f.RetriveNumData
        ReDim Preserve Data(f.disVar.disSize)
        'ReDim Preserve dataAll(f.disVar.disSize)
        Session("data") = Data
        Session("f") = f

    End Sub
    Private Sub mnuRenameFormula_Click()
        Dim i As Integer
        i = 0
        Dim bStartAt0 As Boolean, bFound As Boolean
        If lstSelSeries.SelectedIndex = -1 Then
            lstSelSeries.SelectedIndex = 0
            bStartAt0 = True
        Else
            i = lstSelSeries.SelectedIndex
            bStartAt0 = (i = 0)
        End If
        Do While i < lstSelSeries.Items.Count
            If i < UBound(Session("aFormulas")) Then
                If Not (Session("aFormulas")(i + 1) Is Nothing) Then
                    If Session("aFormulas")(i + 1).Name = lstSelSeries.Items(i).Text Then
                        lstSelSeries.SelectedIndex = i
                        bFound = True
                        Exit Do
                    End If
                End If
            End If
            If bStartAt0 Then
                i = i + 1
            Else
                i = 0
                bStartAt0 = True
            End If
        Loop

        If Not bFound Then
            ShowAlert("Formula to be renamed not found")
            Exit Sub
        End If

        pnlNewFormula.Visible = True
        pnlNewFormula.ToolTip = "Rename Formula"
        Me.lblNewFormula.Text = "Formula Name:"
        txtNewFormula.Text = Session("aFormulas")(i + 1).Name & "2"
        Session("iRenameFormula") = i
        ClientScript.RegisterStartupScript(Me.GetType(), "Set Focus", "document.getElementById('" & txtNewFormula.ClientID & "').focus();", True)
        'brought out
        'Session("aFormulas")(i + 1).Name = sNewName
        'lstSelSeries.Items(i).Text = sNewName
    End Sub
    Private Sub cmdCloseRenameFormula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCloseRenameFormula.Click
        pnlNewFormula.Visible = False
    End Sub
    Private Sub cmdRenameFormula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRenameFormula.Click
        'ttt must visible, this is continuation of Save/ Rename formula
        Session("CP_Visiblibly_ForBILAT") = True

        If pnlNewFormula.ToolTip = "Rename Formula" Then
            Dim i As Integer = If(Session("iRenameFormula"), -1)
            If i > -1 Then
                Dim sNewName = txtNewFormula.Text
                Session("aFormulas")(i + 1).Name = sNewName
                lstSelSeries.Items(i).Text = sNewName
                'ttt - For Session("CP_Visiblibly_ForBILAT") = True
                Session("curlstSelSeries") = lstSelSeries
                cswmWebMenu.Groups("mnuSerType").Items("mnuShowSavedFormulas").Enabled = True
                cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
                cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            End If
        Else
            '"Formula Description"
            mnuSaveFormula_Click(bFromGravity:=False, bNext:=True)
        End If

        pnlNewFormula.Visible = False

    End Sub
    Private Sub mnuEditFormula_Click()
        Dim IsValidForEditFormula As Boolean = False
        Dim curlst = DirectCast(Session("curlstSelSeries"), ListBox)
        Dim sCurrentDisplay = If(curlst Is Nothing OrElse curlst.SelectedItem Is Nothing, "", curlst.SelectedItem.Text) ' lstSelSeries.List(lstSelSeries.ListIndex)
        If sCurrentDisplay = "" Then
            'False
        Else
            If Not (Session("aFormulas") Is Nothing) Then
                For k = 1 To Session("aFormulas").Length - 1
                    If Not (Session("aFormulas")(k) Is Nothing) Then
                        If Session("aFormulas")(k).Name = sCurrentDisplay Then
                            Dim f As Formula = Session("aFormulas")(k)
                            If Not f.Formula_Renamed.Contains("X0") Then
                                IsValidForEditFormula = True
                            End If
                            Exit For
                        End If
                    End If
                Next
            Else
                'False
            End If
        End If
        If Not IsValidForEditFormula Then
            ShowAlert("This function only works with a selected computation variable.")
            Return
        End If
        Session("sOriginForm") = "frm_BilateralInteractionDisplay"

        Session("bNewFormula") = False
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_computations.aspx")

    End Sub
    Private Sub mnuContinue_Click()
        If lblJS.Visible Then 'JRS 2020/04/27 NOW GRAPH VISIBLE FIRST, In case we still have the grid visible behind
            lblJS.Visible = False
            'Pego1.PEreset
            cswmWebMenu.Groups("mnuDisplay").ShowMenuBarItem = False
            cswmWebMenu.Groups("mnuScale").ShowMenuBarItem = False
            'jrs 2020/04/27
            If grdBilateral.Visible Then
                cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = True
            End If
            pnlChartTable.Visible = False
            pnlMain.Visible = True
            pnlSelecteds.Visible = True
            cmdDraw.Enabled = True
            cmdTable.Enabled = True
            cmdSearch.Enabled = True
            cmdGravity.Enabled = True
        ElseIf grdBilateral.Visible Then
            cswmWebMenu.Groups("mnuComp").ShowMenuBarItem = False
            grdBilateral.Visible = False 'jrs 2020/04/27 moved down here, because now Reset is leaving it visible
            pnlChartTable.Visible = False
            pnlMain.Visible = True
            pnlSelecteds.Visible = True
            cmdDraw.Enabled = True
            cmdTable.Enabled = True
            cmdSearch.Enabled = True
            cmdGravity.Enabled = True
            'jrs 2023/09/14
            If Session("bComparisonOn") = True Then
                mnuCompReset_Click()
            End If
        Else
            Dim ShouldAskSave As Boolean = If(Session("NeedFileWrite"), False)
            If Not ShouldAskSave Then
                'ttt Compare file dates to confirm
                Dim FDT2 As Object
                Dim FDT1 As Object
                '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                Try
                    FDT1 = FileDateTime(Session("PutFdrvRun") & "DataDictDyadic.mdb")
                Catch ex As Exception

                End Try
                Try
                    FDT2 = FileDateTime(DataFileDir$ & "DataDictDyadic.mdb")
                Catch ex As Exception

                End Try

                If (Not FDT1 Is Nothing) AndAlso (Not FDT2 Is Nothing) Then
                    If FDT1 = FDT2 Then
                        ShouldAskSave = False
                    Else
                        ShouldAskSave = True
                    End If
                End If
            End If

            If ShouldAskSave Then
                LoadMessage("Do you want to save formulas to data?", Session("Message"))
                Session("ConFirmThen") = "SAVEFORMULATODATA"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Confirm.aspx")
            End If
            'ttt below: also brought to Form_Load for back from Confirm
            If Session("sOriginForm") = "frm_BilateralInteractionDisplay" Then
                Session("sOriginForm") = ""
            End If
            If Session("DisplayParent") = Nothing Then
                Session("DisplayParent") = "frm_MainMenu.aspx"
            End If
            Support.FormManager.Show(Me, Session("DisplayParent"))
        End If

    End Sub
    Public Function mnuSaveFormula_Click(Optional ByVal bFromGravity As Boolean = True,
                Optional ByVal bNext As Boolean = False, Optional ByVal bContinueSaveNonGravity As Boolean = False) As String 'out varname
        If Not bFromGravity Then
            Session("bEditGravity") = False
        End If
        Dim i As Integer
        i = 0
        Dim bStartAt0 As Boolean, bFound As Boolean

        Dim bEditExistingFormula As Boolean = False
        If If(Session("bEditExistingFormula"), False) = True Then
            bEditExistingFormula = True
            Session("bEditExistingFormula") = False
        End If

        If Not bNext Then
            If lstSelSeries.Items.Count = 0 Then Return ""
            If lstSelSeries.SelectedIndex = -1 Then
                lstSelSeries.SelectedIndex = 0
                bStartAt0 = True
            Else
                i = lstSelSeries.SelectedIndex
                bStartAt0 = (i = 0)
            End If
            Do While i < lstSelSeries.Items.Count
                If i < UBound(Session("aFormulas")) Then
                    If Not (Session("aFormulas")(i + 1) Is Nothing) Then
                        If Session("aFormulas")(i + 1).Name = lstSelSeries.Items(i).Text Then
                            lstSelSeries.SelectedIndex = i
                            bFound = True
                            Exit Do
                        End If
                    End If
                End If
                If bStartAt0 Then
                    i = i + 1
                Else
                    i = 0
                    bStartAt0 = True
                End If
            Loop
            Session("curlstSelSeries") = lstSelSeries
            If Not bFound Then
                ShowAlert("Formula to be saved not found")
                Return ""
            End If


            If Session("bNewFormula") AndAlso Not bFromGravity Then
                pnlNewFormula.Visible = True
                pnlNewFormula.ToolTip = "Formula Description"
                Me.lblNewFormula.Text = "Description:"

                Dim rsCheck0 As DataTable = New DataTable
                SetDataTablefromAccess(Session("PutFdrvRun") & "DataDictDyadic.mdb", "SELECT Description FROM FormulaVars WHERE FormulaName = """ & Session("aFormulas")(i + 1).Name & """", "", rsCheck0)
                If rsCheck0.Rows.Count > 0 Then
                    txtNewFormula.Text = rsCheck0.Rows(0).Item("Description")
                    Session("bEditExistingFormula") = True
                Else
                    txtNewFormula.Text = Session("aFormulas")(i + 1).Name
                End If

                ClientScript.RegisterStartupScript(Me.GetType(), "Set Focus", "document.getElementById('" & txtNewFormula.ClientID & "').focus();", True)
                Return ""
                'will continue with bNext=True
            End If

        End If
        'ttt====
        'After adding Description, continues here

        Dim db = Session("PutFdrvRun") & "DataDictDyadic.mdb"
        Dim sql As String
        Dim rsCheck As DataTable = New DataTable
        Dim sDescription As String = ""
        If Not bFromGravity Then
            sDescription = txtNewFormula.Text
        End If

        If (Not bEditExistingFormula) AndAlso Session("bNewFormula") Then
            'If Session("bNewFormula") Then
            SetDataTablefromAccess(db, "SELECT COUNT(*) FROM FormulaVars WHERE FormulaName = """ & Session("aFormulas")(i + 1).Name & """", "", rsCheck)
            If rsCheck.Rows(0).Item(0) > 0 Then
                ShowAlert("This variable name already exists, please rename your formula and try again.")
                Return ""
            End If
        Else
            SetDataTablefromAccess(db, "SELECT Description FROM FormulaVars WHERE FormulaName = """ & Session("aFormulas")(i + 1).Name & """", "", rsCheck)
            If rsCheck.Rows.Count > 0 Then
                sDescription = rsCheck.Rows(0).Item("Description")
            End If
            If (Not Session("bEditGravity")) AndAlso (Not bContinueSaveNonGravity) Then
                SetDataTablefromAccess(db, "SELECT Count(*) FROM Gravity WHERE GravityVarName = """ & Session("aFormulas")(i + 1).Name & """", "", rsCheck)

                If rsCheck.Rows(0).Item(0) > 0 Then
                    LoadMessage("This is a Gravity formula, if you save it using this option, you will not be able to edit it as a Gravity Variable. Are you sure you want to continue?", Session("Message"))
                    Session("ConFirmThen") = "SAVEFORMULA_NON_GRAVITY"
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_Confirm.aspx")
                    Return ""
                    'Dim ans As Integer = vbNo
                    'ans = MsgBox("This is a Gravity formula, if you save it using this option, you will not be able to edit it as a Gravity Variable. Are you sure you want to continue?", vbCritical, "Save Formula")
                    'If ans = vbNo Then

                    '    Exit Sub
                    'End If
                End If
            End If

            updateAccessWith1Command(db, "DELETE * FROM FormulaVars WHERE FormulaName = """ & Session("aFormulas")(i + 1).Name & """")
            updateAccessWith1Command(db, "DELETE * FROM FormulaVarsDet WHERE FormulaName = """ & Session("aFormulas")(i + 1).Name & """")


            updateAccessWith1Command(db, "DELETE * FROM Gravity WHERE GravityVarName = """ & Session("aFormulas")(i + 1).Name & """")
            updateAccessWith1Command(db, "DELETE * FROM  GravityDet WHERE GravityVarName = """ & Session("aFormulas")(i + 1).Name & """")
        End If
        If bFromGravity Then
            If Session("sGravityDescription") = "" Then
                sDescription = ""
                'sDescription = InputBox("Type a Description for your formula", "Save Formulas", sDescription)
            Else
                sDescription = Session("sGravityDescription")
            End If
        End If

        Dim sUnits As String = ""

        'From DGravity
        If Not Session("lstUnit") Is Nothing Then
            Dim lst = DirectCast(Session("lstUnit"), List(Of String))
            For x = 0 To lst.Count - 1
                sUnits &= lst(x) & If(x = lst.Count - 1, "", ",")
            Next
            'we should truncate to 250
            If sUnits.Length > 250 Then
                sUnits = sUnits.Substring(0, 250)
                Dim MoreCommas As Integer = lst.Count - sUnits.Split(",").Count
                If MoreCommas > 0 Then
                    sUnits = (sUnits.Substring(0, 250 - MoreCommas)).PadRight(250, ",")
                End If
            End If
            Session("lstUnit") = Nothing
        Else
            'ttt Units of computation ' after hyphen
            For p = 1 To Session("aFormulas")(i + 1).disVar.disSize
                Dim mVars As String = Session("aFormulas")(i + 1).disVar.displayDet(p)
                If mVars.Contains("-") Then
                    sUnits &= mVars.Split("-")(1).Trim & If(p = Session("aFormulas")(i + 1).disVar.disSize, "", ",")
                End If
                'we should truncate to 250
                If sUnits.Length > 250 Then
                    sUnits = sUnits.Substring(0, 250)
                    Dim MoreCommas As Integer = Session("aFormulas")(i + 1).disVar.disSize - sUnits.Split(",").Count
                    If MoreCommas > 0 Then
                        sUnits = (sUnits.Substring(0, 250 - MoreCommas)).PadRight(250, ",")
                    End If
                End If
            Next p
        End If

        sql = "INSERT INTO FormulaVars(FormulaName, Formula, Units, Description) Values(""" & Session("aFormulas")(i + 1).Name & """, """ &
        Session("aFormulas")(i + 1).Formula_Renamed & """, """ & sUnits & """, """ & sDescription & """)"
        updateAccessWith1Command(db, sql)

        Dim j As Integer
        For j = 1 To Session("aFormulas")(i + 1).disVar.disSize
            sql = "INSERT INTO FormulaVarsDet(FormulaName, Code, VarName) Values(""" & Session("aFormulas")(i + 1).Name & """, """ &
            Session("aFormulas")(i + 1).disVar.display(j) & """, """ & Session("aFormulas")(i + 1).disVar.displayDet(j).Split("-")(0).Trim & """)"
            updateAccessWith1Command(db, sql)
        Next j
        If Not Session("bEditGravity") Then
            ShowAlert("Formula " & Session("aFormulas")(i + 1).Name & " saved.")
            'ttt - Set visibility , like Session("CP_Visiblibly_ForBILAT") = True
            cswmWebMenu.Groups("mnuSerType").Items("mnuShowSavedFormulas").Enabled = True
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True

        End If
        Session("NeedFileWrite") = True
        LoadformulaVars(Session("PutFdrvRun") & "DataDictDyadic.mdb")
        Return Session("aFormulas")(i + 1).Name
    End Function
    Protected Sub ShowAlert(message As String)
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "alert", "alert('" & message.Replace("'", "\'") & "');", True)
        Return
        '-------------------
        Dim sb As New System.Text.StringBuilder()
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())
    End Sub
    Protected Sub ShowConfirm(message As String)
        Dim confirmValue As String = Request.Form("confirm_value")
        Dim sb As New System.Text.StringBuilder()
        sb.Append("return confirm('")
        sb.Append(message)
        sb.Append("');")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "confirm", "confirm('" & message.Replace("'", "\'") & "');", True)
        'ClientScript.RegisterOnSubmitStatement(Me.GetType(), "confirm", sb.ToString)
    End Sub
    Public Sub OnConfirm(ByVal sender As Object, ByVal e As EventArgs)
        If Not lstVarNameSavedFormulas.Visible Then Return
        If lstVarNameSavedFormulas.Items.Count = 0 OrElse lstVarNameSavedFormulas.SelectedIndex = -1 Then Return
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            DeleteGravityVar()
        Else
            'don't delete
        End If


    End Sub
    Public Sub OnConfirmSaveToData(ByVal sender As Object, ByVal e As EventArgs)
        Dim confirmValue As String = Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Stop
        Else
            'don't delete
        End If


    End Sub
    Protected Sub OnConfirmOLD(sender As Object, e As EventArgs)
        Dim message As String = "Your Request is being processed."
        Dim sb As New System.Text.StringBuilder()
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("');")
        ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString())

        'Dim confirmValue As String = Request.Form("confirm_value")
        'Dim message As String = "Do you want to Submit?"
        'Dim sb As New System.Text.StringBuilder()
        'sb.Append("return confirm('")
        'sb.Append(message)
        'sb.Append("');")
        'ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", sb.ToString)

        'If confirmValue = "Yes" Then
        '    'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked YES!')", True)
        '    ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", "YES")
        'Else
        '    'ClientScript.RegisterStartupScript(Me.[GetType](), "alert", "alert('You clicked NO!')", True)
        '    ClientScript.RegisterOnSubmitStatement(Me.GetType(), "alert", "NO")
        'End If
    End Sub
    Private Sub mnuResetLegends_Click()
        If Session("mLegends") Is Nothing Then Return
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            Session("mFLegends")(4, i) = Session("mLegends")(4, i)
            Session("mFLegends")(1, i) = Session("mLegends")(1, i)
            Session("mFLegends")(2, i) = Session("mLegends")(2, i)
            Session("mFLegends")(5, i) = Session("mLegends")(5, i)
            Session("mFLegends")(3, i) = Session("mLegends")(3, i)
        Next i

        If lblJS.Visible Then DrawGraph()

    End Sub
    Private Sub mnuFormatLegends_Click()
        For i = 1 To Session("iCOUNTE")
            If Session("mFLegends")(1, i) Is Nothing Then Session("mFLegends")(1, i) = Session("mLegends")(1, i)
            If Session("mFLegends")(2, i) Is Nothing Then Session("mFLegends")(2, i) = Session("mLegends")(2, i)
            If Session("mFLegends")(3, i) Is Nothing Then Session("mFLegends")(3, i) = Session("mLegends")(3, i)
            If Session("mFLegends")(4, i) Is Nothing Then Session("mFLegends")(4, i) = Session("mLegends")(4, i)
            If Session("mFLegends")(5, i) Is Nothing Then Session("mFLegends")(5, i) = If(Session("mLegends")(5, i), "")
        Next i
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionGraphLabel.aspx")
        If lblJS.Visible Then DrawGraph()
    End Sub
    Private Sub mnuFormatLegNoDims_Click()
        If Session("mLegends") Is Nothing Then Return
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            Session("mFLegends")(4, i) = Session("mLegends")(5, i)
            Session("mFLegends")(1, i) = ""
            Session("mFLegends")(2, i) = ""
            Session("mFLegends")(5, i) = ""
            Session("mFLegends")(3, i) = ""
        Next i
        If lblJS.Visible Then DrawGraph(useFLegend:=True)
    End Sub
    Private Sub mnuFormatLeg1Dim_Click()
        If Session("mLegends") Is Nothing Then Return
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            Session("mFLegends")(4, i) = Session("mLegends")(1, i)
            Session("mFLegends")(1, i) = ""
            Session("mFLegends")(2, i) = ""
            Session("mFLegends")(5, i) = ""
            Session("mFLegends")(3, i) = ""
        Next i
        If lblJS.Visible Then DrawGraph(useFLegend:=True)

    End Sub

    Private Sub mnuFormatLeg2Dim_Click()
        If Session("mLegends") Is Nothing Then Return
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            Session("mFLegends")(4, i) = Session("mLegends")(2, i)
            Session("mFLegends")(1, i) = ""
            Session("mFLegends")(2, i) = ""
            Session("mFLegends")(5, i) = ""
            Session("mFLegends")(3, i) = ""
        Next i
        If lblJS.Visible Then DrawGraph(useFLegend:=True)

    End Sub

    Private Sub mnuFormatLegDims_Click()
        If Session("mLegends") Is Nothing Then Return
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            Session("mFLegends")(4, i) = ""
            Session("mFLegends")(1, i) = Session("mLegends")(1, i)
            Session("mFLegends")(2, i) = Session("mLegends")(2, i)
            Session("mFLegends")(5, i) = ""
            Session("mFLegends")(3, i) = ""
        Next i
        If lblJS.Visible Then DrawGraph(useFLegend:=True)

    End Sub
    Private Sub mnuFormatSce_Click()
        If Session("mLegends") Is Nothing Then Return
        Dim i As Integer
        For i = 1 To Session("iCOUNTE")
            Session("mFLegends")(4, i) = Session("mLegends")(5, i)
            Session("mFLegends")(1, i) = ""
            Session("mFLegends")(2, i) = ""
            Session("mFLegends")(5, i) = ""
            Session("mFLegends")(3, i) = ""
        Next i
        If lblJS.Visible Then DrawGraph(useFLegend:=True)

    End Sub
    Public Sub setmFLegends(i As Integer, j As Integer, sVal As String)
        Session("mFLegends")(i, j) = sVal
    End Sub
    Private Sub AddGravityAdj(sVarName As String, sType As String)
        Dim pos As Integer
        pos = InStr(sVarName, " - ")
        Session("CalledAddGravityAdj") = True
        Session("sAdjustmentVar") = Mid(sVarName, 1, pos - 1)
        Session("sAdjustmentVarType") = sType
        Session("bClickedDGravitySave") = Nothing
        Session("GravityAdjustmentVariableName") = "Gravity Adjustment Variable Name: " & Mid(sVarName, 1, pos - 1)
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionDGravity.aspx")
        Session("bNewFormula") = False

        lstSelSeries.Visible = True
        lstSelDyads.Visible = True
        'cmb_DIM1.Enabled = True
        'cmb_DIM2.Enabled = True
        Session("bAddGravityAdj") = False
    End Sub
    Private Sub AddGravityVar(sVarName As String, sType As String)
        Session("CalledAddGravityVar") = True
        Session("sAdjustmentVar") = sVarName
        Session("sAdjustmentVarType") = sType
        Session("bClickedDGravitySave") = Nothing
        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_BilateralInteractionDGravity.aspx")
        lstSelSeries.Visible = True
        lstSelDyads.Visible = True
        'cmb_DIM1.Enabled = True
        'cmb_DIM2.Enabled = True
        Session("bAddGravityAdj") = False
    End Sub
    Private Sub SetFormulaType(sVarName As String, sType As String, colCounte As Integer)
        'ttt made
        ' colCounte  = 1 'tmp1
        If sType = "DY" Or sType = "MA" Or sType = "MP" Then
            setbFormulas(False, colCounte)
        Else
            setbFormulas(True, colCounte)
        End If
        setsTypes("(" & sType & ")", colCounte)

    End Sub
    Private Sub LoadCountries()
        lstDim1.Items.Clear()
        lstDim1p.Items.Clear()
        'Dim dbIFs As String = IFs_MDB
        'Dim rsCtry As DataTable = New DataTable
        'SetDataTablefromAccess(dbIFs, "SELECT Country FROM [Country Translation] ORDER BY Country", "", rsCtry)
        'For Each r As DataRow In rsCtry.Rows
        '    lstDim1.Items.Add(r.Item("Country"))
        '    lstDim1p.Items.Add(r.Item("Country"))
        'Next


        Dim db As String
        Dim valtbl As DataTable = New DataTable
        Dim rr As Integer
        If (Session("bUseGroup") OrElse Session("bUseGroupDec")) Then
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM " & GroupIDNew & " ORDER BY GroupName", "", valtbl)
            rr = 0
            While Not valtbl.Rows.Count = rr
                lstDim1.Items.Add(valtbl.Rows(rr).Item("GroupName"))
                lstDim1p.Items.Add(valtbl.Rows(rr).Item("GroupName"))
                rr = rr + 1
            End While
        ElseIf Session("bUseGList") Then
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT * FROM GListId ORDER BY Description", "", valtbl)
            rr = 0
            While Not valtbl.Rows.Count = rr
                lstDim1.Items.Add(valtbl.Rows(rr).Item("Description"))
                lstDim1p.Items.Add(valtbl.Rows(rr).Item("Description"))
                rr = rr + 1
            End While
        Else
            db = Session("IFs_MDB2Active")
            SetDataTablefromAccess(db, "SELECT Country FROM [Country Translation] ORDER BY Country", "", valtbl)
            For Each r As DataRow In valtbl.Rows
                lstDim1.Items.Add(r.Item("Country"))
                lstDim1p.Items.Add(r.Item("Country"))
            Next
        End If

    End Sub
    Private Sub ComputeWeightAvg(rsSource As DataTable, tblAux As DataTable, bActorGrp As Boolean, bPartnerGrp As Boolean,
TargetVector As Object, TargetCol As Integer)
        'rsSource.MoveFirst
        Dim Result() As Object, TotAux() As Object
        ReDim Result(rsSource.Rows.Count - 4), TotAux(rsSource.Rows.Count - 4)
        Dim TotVal As Single, i As Integer
        Dim iStart As Integer, iEnd As Integer
        'jrs 2019/10/16 If CInt(mid(rsSource.Fields(4).Name, 2)) >= tblAux.Fields(2).Name Then
        If CInt(rsSource.Columns(4).ColumnName) >= tblAux.Columns(2).ColumnName Then
            iStart = 4
        Else
            'jrs 2019/10/16 iStart = tblAux.Fields(2).Name - CInt(mid(rsSource.Fields(4).Name, 2)) + 4
            iStart = tblAux.Columns(2).ColumnName - CInt(rsSource.Columns(4).ColumnName) + 4
        End If
        'jrs 2019/10/16 it was checking last field instead of 3rd to last, not considering Earlist and MostRecent
        If CInt(rsSource.Columns(rsSource.Columns.Count - 3).ColumnName) <= tblAux.Columns(tblAux.Columns.Count - 3).ColumnName Then
            iEnd = rsSource.Columns.Count - 3
        Else
            iEnd = tblAux.Columns(tblAux.Columns.Count - 3).ColumnName - CInt(rsSource.Columns(4).ColumnName) + 4
        End If

        For Each r As DataRow In rsSource.Rows
            'Do While Not rsSource.EOF
            If bActorGrp And bPartnerGrp Then
                For i = iStart To iEnd
                    Dim rf = tblAux.Rows.FindIndices(Function(x As DataRow) x.Item("Country") = r.Item("Actor"))(0)

                    'tblAux.Seek "=", rsSource.Fields("Actor")

                    'If Not IsNull(tblAux.Fields(rsSource.Fields(i).Name)) Then
                    If rf >= 0 AndAlso (Not tblAux.Rows(rf).Item(rsSource.Columns(i).ColumnName) Is Nothing) Then
                        TotVal = tblAux.Rows(rf).Item(rsSource.Columns(i).ColumnName)
                        'tblAux.Seek "=", rsSource.Fields("Partner")
                        Dim rfp = tblAux.Rows.FindIndices(Function(x As DataRow) x.Item("Country") = r.Item("Partner"))(0)
                        If rfp >= 0 AndAlso (Not tblAux.Rows(rfp).Item(rsSource.Columns(i).ColumnName) Is Nothing) Then
                            TotVal = TotVal + tblAux.Rows(rfp).Item(rsSource.Columns(i).ColumnName)
                            'If Not IsNull(rsSource.Fields(i)) Then
                            If Not (r.Item(i) Is Nothing) Then
                                Result(i - iStart + 1) = Result(i - iStart + 1) + r.Item(i) * TotVal
                                TotAux(i - iStart + 1) = TotAux(i - iStart + 1) + TotVal
                            End If
                        End If
                    End If
                Next i
            ElseIf bActorGrp Then
                For i = iStart To iEnd
                    'tblAux.Seek "=", rsSource.Fields("Actor")
                    Dim rf = tblAux.Rows.FindIndices(Function(x As DataRow) x.Item("Country") = r.Item("Actor"))(0)
                    'If Not IsNull(tblAux.Fields(rsSource.Fields(i).Name)) Then
                    If rf >= 0 AndAlso (Not tblAux.Rows(rf).Item(rsSource.Columns(i).ColumnName) Is Nothing) Then
                        TotVal = tblAux.Rows(rf).Item(rsSource.Columns(i).ColumnName)
                        'If Not IsNull(rsSource.Fields(i)) Then
                        If Not (r.Item(i) Is Nothing) Then
                            Result(i - iStart + 1) = Result(i - iStart + 1) + r.Item(i) * TotVal
                            TotAux(i - iStart + 1) = TotAux(i - iStart + 1) + TotVal
                        End If
                    End If
                Next i
            ElseIf bPartnerGrp Then
                For i = iStart To iEnd
                    'tblAux.Seek "=", rsSource.Fields("Partner")
                    Dim rfp = tblAux.Rows.FindIndices(Function(x As DataRow) x.Item("Country") = r.Item("Partner"))(0)
                    'If Not IsNull(tblAux.Fields(rsSource.Fields(i).Name)) Then
                    If rfp >= 0 AndAlso (Not tblAux.Rows(rfp).Item(rsSource.Columns(i).ColumnName) Is Nothing) Then
                        TotVal = tblAux.Rows(rfp).Item(rsSource.Columns(i).ColumnName)
                        If Not (r.Item(i) Is Nothing) Then
                            Result(i - iStart + 1) = Result(i - iStart + 1) + r.Item(i) * TotVal
                            TotAux(i - iStart + 1) = TotAux(i - iStart + 1) + TotVal
                        End If
                    End If
                Next i
            End If

            'rsSource.MoveNext
            'Loop
        Next

        Dim MoveYrs As Integer
        MoveYrs = 0
        If Session("BilateralFirstYear") = 0 Then
            Session("BilateralFirstYear") = CInt(rsSource.Columns(iStart).ColumnName)
            Session("BilateralLastYear") = CInt(rsSource.Columns(iEnd).ColumnName)
        Else
            If Session("BilateralFirstYear") > CInt(rsSource.Columns(iStart).ColumnName) Then
                MoveYrs = Session("BilateralFirstYear") - CInt(rsSource.Columns(iStart).ColumnName)
                Session("BilateralFirstYear") = CInt(rsSource.Columns(iStart).ColumnName)
            End If
            If Session("BilateralLastYear") < CInt(rsSource.Columns(iEnd).ColumnName) Then
                Session("BilateralLastYear") = CInt(rsSource.Columns(iEnd).ColumnName)
            End If
        End If
        If MoveYrs > 0 Then
            Dim m As Integer, k As Integer
            For m = 1 To TargetCol - 1
                For k = 1 To Amin(Session("BilateralLastYear") - Session("BilateralFirstYear") + 1, 100 - MoveYrs)
                    TargetVector(k + MoveYrs, m) = TargetVector(k, m)
                    If k < MoveYrs + 1 Then
                        TargetVector(k, m) = Nothing
                    End If
                Next k
            Next m
        End If

        'Get the average
        For i = iStart To iEnd
            If TotAux(i - iStart + 1) <> 0 Then
                TargetVector(i - iStart + 1, TargetCol) = Result(i - iStart + 1) / TotAux(i - iStart + 1)
            Else
                TargetVector(i - iStart + 1, TargetCol) = Nothing
            End If
        Next i
    End Sub
    Private Sub ComputeWeightAvgMon(rsSource As DataTable, tblAux As DataTable, bActorGrp As Boolean,
TargetVector As Object, TargetCol As Integer)
        'rsSource.MoveFirst
        Dim Result() As Object, TotAux() As Object
        ReDim Result(rsSource.Columns.Count - 2), TotAux(rsSource.Columns.Count - 2)
        Dim TotVal As Single, i As Integer
        Dim iStart As Integer, iEnd As Integer
        If CInt(Mid(rsSource.Columns(2).ColumnName, 2)) >= tblAux.Columns(2).ColumnName Then
            iStart = 2
        Else
            iStart = tblAux.Columns(2).ColumnName - CInt(Mid(rsSource.Columns(2).ColumnName, 2)) + 2
        End If
        If CInt(rsSource.Columns(rsSource.Columns.Count - 3).ColumnName) <= tblAux.Columns(tblAux.Columns.Count - 3).ColumnName Then
            iEnd = rsSource.Columns.Count - 1
        Else
            iEnd = tblAux.Columns(tblAux.Columns.Count - 3).ColumnName - CInt(rsSource.Columns(2).ColumnName) + 2
        End If
        For Each r As DataRow In rsSource.Rows


            'Do While Not rsSource.EOF
            If bActorGrp Then
                For i = iStart To iEnd
                    'tblAux.Seek "=", rsSource.Fields("Actor")
                    Dim rf = tblAux.Rows.FindIndices(Function(x As DataRow) x.Item("Country") = r.Item("Actor"))(0)
                    'If Not IsNull(tblAux.Fields(rsSource.Fields(i).Name)) Then
                    If rf >= 0 AndAlso (Not tblAux.Rows(rf).Item(rsSource.Columns(i).ColumnName) Is Nothing) Then
                        TotVal = tblAux.Rows(rf).Item(rsSource.Columns(i).ColumnName)
                        'If Not IsNull(rsSource.Fields(i)) Then
                        If Not (r.Item(i) Is Nothing) Then
                            Result(i - iStart + 1) = Result(i - iStart + 1) + r.Item(i) * TotVal
                            TotAux(i - iStart + 1) = TotAux(i - iStart + 1) + TotVal
                        End If
                    End If
                Next i
            End If

            'rsSource.MoveNext
            'Loop
        Next

        Dim MoveYrs As Integer
        MoveYrs = 0
        If Session("BilateralFirstYear") = 0 Then
            Session("BilateralFirstYear") = CInt(Mid(rsSource.Columns(iStart).ColumnName, 2))
            Session("BilateralLastYear") = CInt(Mid(rsSource.Columns(iEnd).ColumnName, 2))
        Else
            If Session("BilateralFirstYear") > CInt(Mid(rsSource.Columns(iStart).ColumnName, 2)) Then
                MoveYrs = Session("BilateralFirstYear") - CInt(Mid(rsSource.Columns(iStart).ColumnName, 2))
                Session("BilateralFirstYear") = CInt(Mid(rsSource.Columns(iStart).ColumnName, 2))
            End If
            If Session("BilateralLastYear") < CInt(Mid(rsSource.Columns(iEnd).ColumnName, 2)) Then
                Session("BilateralLastYear") = CInt(Mid(rsSource.Columns(iEnd).ColumnName, 2))
            End If
        End If
        If MoveYrs > 0 Then
            Dim m As Integer, k As Integer
            For m = 1 To TargetCol - 1
                For k = 1 To Amin(Session("BilateralLastYear") - Session("BilateralFirstYear") + 1, 100 - MoveYrs)
                    TargetVector(k + MoveYrs, m) = TargetVector(k, m)
                    If k < MoveYrs + 1 Then
                        TargetVector(k, m) = Nothing
                    End If
                Next k
            Next m
        End If

        'Get the average
        For i = iStart To iEnd
            If TotAux(i - iStart + 1) <> 0 Then
                TargetVector(i - iStart + 1, TargetCol) = Result(i - iStart + 1) / TotAux(i - iStart + 1)
            Else
                TargetVector(i - iStart + 1, TargetCol) = Nothing
            End If
        Next i
    End Sub
    Private Sub LoadformulaVars(dbDD As String)
        lstVarNameSavedFormulas.Items.Clear()
        Dim sUnits = ""
        Dim rsDD As DataTable = New DataTable
        SetDataTablefromAccess(dbDD, "SELECT FormulaName, Description, Units FROM FormulaVars", "", rsDD)
        For Each r As DataRow In rsDD.Rows
            If r.Item("Units") Is DBNull.Value Then
                sUnits = ""
            Else
                sUnits = r.Item("Units")
            End If
            'If sUnits > "" Then
            '    lstVarNameSavedFormulas.Items.Add(r.Item("FormulaName") & " - " & r.Item("Description") & " - " & sUnits)
            'Else
            lstVarNameSavedFormulas.Items.Add(r.Item("FormulaName") & " - " & r.Item("Description"))
            'End If
        Next
    End Sub

    'jrs 2024/08/28 removed Handles lstVarNameMonadyc.SelectedIndexChanged, don't want it to refresh unless click button
    Private Sub lstVarNameMonadyc_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim lst = DirectCast(sender, ListBox)
        If lst.SelectedIndex = -1 Then Return
        Dim i As Integer = lst.SelectedIndex
        Dim sType As String = "MA" '/MP
        If Not (Session("OrigDyadic") Is Nothing) Then
            sType = Session("OrigDyadic")
        End If
        'ttt VB6 use cmb_VarName (not cmb_VarNameMonadyc), maybe mistake

        If If(Session("bAddGravityAdj"), False) Then
            AddGravityAdj(lst.Items(i).Text, sType)
            Exit Sub
        ElseIf If(Session("bAddGravity"), False) Then
            AddGravityVar(lst.Items(i).Text, sType)
        End If
        lstSelSeries.Items.Add("(" & sType & ") " & lst.Items(i).Text)
        If lst.Items(i).Text.Split("-").Count = 3 Then
            'only Name and Unit
            Session("BL_lbl_Var") = "(" & sType & ") " & lst.Items(i).Text.Split("-")(0).Trim & " - " & lst.Items(i).Text.Split("-")(2).Trim
        Else
            Session("BL_lbl_Var") = "(" & sType & ") " & lst.Items(i).Text '.Split("-")(0).Trim ' Gross Variable Name
        End If
        Session("curlstSelSeries") = lstSelSeries
        ReDim Preserve Session("sAggRulesSelected")(lstSelSeries.Items.Count)
        Session("sAggRulesSelected")(lstSelSeries.Items.Count - 1) = Session("sAggRulesMon")(i)

        ReDim Preserve Session("bDyadicSeries")(lstSelSeries.Items.Count)
        ReDim Preserve Session("bMonadicActorSeries")(lstSelSeries.Items.Count)
        If sType = "MA" Then
            Session("bMonadicActorSeries")(lstSelSeries.Items.Count - 1) = True
        End If
        If If(Session("BL_SelectVar"), False) = True Then
            'Select then Continue at frm_computation
            Session("BL_SelectVar") = False
            cmdComputation_Click(sender, e)
        End If
        'If bComputation Then
        '    Me.Hide
        '    frm_computations.Show
        '    frm_computations.SetFocus
        '    Exit Sub
        'End If
    End Sub

    Private Sub lstVarNameForecast_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstVarNameForecast.SelectedIndexChanged
        Dim lst = DirectCast(sender, ListBox)
        If lst.SelectedIndex = -1 Then Return
        Dim i As Integer = lst.SelectedIndex

        lstSelSeries.Items.Add("(FC) " & lst.Items(i).Text)

        Session("curlstSelSeries") = lstSelSeries
        ReDim Preserve Session("sAggRulesSelected")(lstSelSeries.Items.Count)
        Session("sAggRulesSelected")(lstSelSeries.Items.Count - 1) = Session("sAggRulesFor")(i)

        ReDim Preserve Session("bDyadicSeries")(lstSelSeries.Items.Count)
        ReDim Preserve Session("bMonadicActorSeries")(lstSelSeries.Items.Count)
        Session("bDyadicSeries")(lstSelSeries.Items.Count - 1) = True

        If Session("bComputation") Then
            'Me.Hide
            'frm_computations.Show
            'frm_computations.SetFocus
            Exit Sub
        End If

    End Sub

    Private Sub lstVarNameSavedFormulas_SelectedIndexChanged(sender As Object, e As EventArgs, Optional ByVal JustSaved As Boolean = False, Optional ByVal ForVar As String = "") Handles lstVarNameSavedFormulas.SelectedIndexChanged
        If lstVarNameSavedFormulas.Visible AndAlso lstVarNameSavedFormulas.SelectedIndex > -1 Then
            cmdDeleteGravity.Visible = True
            cmdEditGravity.Visible = True
        Else
            cmdDeleteGravity.Visible = False
            cmdEditGravity.Visible = False
        End If
        Dim sVar As String, pos As Integer, sUnits As String = ""
        If JustSaved Then
            sVar = ForVar
            If sVar.Trim = "" Then Return
        Else
            pos = InStr(1, lstVarNameSavedFormulas.Items(lstVarNameSavedFormulas.SelectedIndex).Text, " - ")
            sUnits = ""
            If pos > 0 Then
                sVar = Mid(lstVarNameSavedFormulas.Items(lstVarNameSavedFormulas.SelectedIndex).Text, 1, pos - 1)
                pos = InStr(pos + 1, lstVarNameSavedFormulas.Items(lstVarNameSavedFormulas.SelectedIndex).Text, " - ")
                If pos > 0 Then
                    sUnits = Trim(Mid(lstVarNameSavedFormulas.Items(lstVarNameSavedFormulas.SelectedIndex).Text, pos + 1))
                End If
            Else
                sVar = lstVarNameSavedFormulas.Items(lstVarNameSavedFormulas.SelectedIndex).Text
            End If
            If sVar.Trim = "" Then Return
            lstSelSeries.Items.Add(sVar)
            Session("curlstSelSeries") = lstSelSeries
        End If
        'ttt Continue here, for "Save And Continue" from Gravity
        ReDim Preserve Session("sAggRulesSelected")(lstSelSeries.Items.Count)
        ReDim Preserve Session("bDyadicSeries")(lstSelSeries.Items.Count)
        ReDim Preserve Session("bMonadicActorSeries")(lstSelSeries.Items.Count)

        'Open DataDict
        Dim dbDD As String = Session("PutFdrvRun") & "DataDictDyadic.mdb"
        'Read Formula
        Dim rsDet As DataTable = New DataTable
        Dim sql As String
        sql = "SELECT v.Formula, d.Code, d.VarName, v.Description FROM FormulaVars as v, FormulaVarsDet as d WHERE v.FormulaName = d.FormulaName AND d.FormulaName = """ & sVar & """"
        SetDataTablefromAccess(dbDD, sql, "", rsDet)
        Dim f As Formula = New Formula
        f.init(rsDet.Rows(0).Item("Formula"))
        f.Name = sVar
        f.Unit = sUnits
        f.begAll = 1
        f.endAll = 1

        Dim sDesc As String
        sDesc = rsDet.Rows(0).Item("Description")
        For Each r As DataRow In rsDet.Rows
            f.disVar.setDataDetS(r.Item("Code"), r.Item("VarName"))
        Next
        If lstSelSeries.Items.Count > UBound(Session("aFormulas")) Then
            ReDim Preserve Session("aFormulas")(lstSelSeries.Items.Count)
        End If
        Session("aFormulas")(lstSelSeries.Items.Count) = f
        LoadFormula(f)
        SetDataTablefromAccess(dbDD, "SELECT COUNT(*) FROM Gravity WHERE GravityVarName = """ & sVar & """", "", rsDet)
        If rsDet.Rows(0).Item(0) > 0 Then
            Session("sGravityDescription") = sDesc
        Else
            Session("sGravityDescription") = ""
        End If
        Session("bCompAll") = True
        Session("sOrigDyadic") = "SF"

        'Check if is Computed

        If Not f.Formula_Renamed.Contains("X") Then
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True
        End If

        cswmWebMenu.Groups("mnuEditGravity").ShowMenuBarItem = True
        Session("bNewFormula") = False
    End Sub
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
        lstFiles.Items.Clear()
        Session("FilesSelected") = 1
        sWorkingFile = Session("PutFdrvRun2") & "\IFsForDyadicWork.db"
        'jrs 2021/02/24 it was using RUNFILES instead of DATA directory
        BASE = fs.GetFile(DataFileDir$ & "\IFsForDyadic.db")
        lstFiles.Items.Add(New ListItem("0 - " & "Working File, based on IFsForDyadicWork.db"))
        lstFiles.Items.Add(New ListItem("1 - " & BASE.Name))
        Dim sRunFile As String
        '2 – any files you find in RUNFILES that start with the prefix IFsForDyadic And that 
        'End In .db, so basically: IFsForDyadic*.db(except For 0 And 1 which are already included).
        sRunFile = Dir(Session("PutFdrvRun2") & "\IFsForDyadic*.db")
        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) AndAlso UCase(sRunFile) <> UCase(sWorkingFile) AndAlso UCase(sRunFile) <> "IFSFORDYADICWORK.DB" Then 'JRS 11/08/02
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then 'jrs 2005/06/27 'jrs 2020/01/07 added or StandAlone
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
            End If
            '    ' Call Dir again without arguments to return the next *.INI file in the
            '    ' same directory.
            sRunFile = Dir()
        Loop
        'jrs 2006/09/06 Multiple Files
        'Show Current Selected Files
        ReDim Preserve Session("FCFilesIdxs")(lstFiles.Items.Count - 1)
        If Not (Session("FCFilesIdxs")(0) Is Nothing) Then
            Dim fileIdx As Integer
            For fileIdx = 0 To lstFiles.Items.Count - 1
                lstFiles.Items(fileIdx).Selected = If(Session("FCFilesIdxs")(fileIdx), False)
            Next fileIdx
        Else
            Session("FCFilesIdxs")(0) = True
            lstFiles.Items(0).Selected = True
        End If

        If Not Session("txtFiles") Is Nothing Then
            txtFiles.Value = Session("txtFiles")
        End If


    End Sub
    Private Sub SqliteTable(CommandText As String, DataName As String, ByRef MySqliteReader As SqliteDataReader, ByRef DbFields As List(Of String), Optional ByRef OutDT As DataTable = Nothing)
        Dim dt As New DataTable
        dt.Columns.Add("Actor", GetType(Integer))
        dt.Columns.Add("Partner", GetType(Integer))
        dt.Columns.Add("Year", GetType(Integer))
        dt.Columns.Add("Val", GetType(Double))
        'Actor, Partner, Year, Val
        vConnString = "DataSource=" & DataName ' Session("PutFdrvRun") & "IFSForDyadicBilatBase.db;"
        'Dim ada As SQLiteDataAdapter
        vSQLiteCon = New SqliteConnection(vConnString) '& "New=True;"
        vSQLiteCon.Open()
        Dim vSQLiteCommand = vSQLiteCon.CreateCommand()
        vSQLiteCommand.CommandText = CommandText '"SELECT * FROM XDYAD"
        MySqliteReader = vSQLiteCommand.ExecuteReader()
        DbFields = New List(Of String)
        Dim MadeDefs As Boolean = False
        Dim curYear As Integer = 0
        While MySqliteReader.Read()
            If Not MadeDefs Then
                For i = 0 To MySqliteReader.FieldCount - 1
                    DbFields.Add(MySqliteReader.GetName(i))
                Next
                MadeDefs = True
            End If
            If DbFields.Count = 4 Then
                If curYear <> MySqliteReader("Year") Then
                    Dim dr As DataRow = dt.NewRow()
                    'dr("Actor") = MySqliteReader(0)
                    'dr("Partner") = MySqliteReader(1)
                    'dr("Year") = MySqliteReader(2)
                    'dr("Val") = MySqliteReader(3)
                    dr(0) = MySqliteReader(0)
                    dr(1) = MySqliteReader(1)
                    dr(2) = MySqliteReader("Year")
                    dr(3) = MySqliteReader(3)
                    dt.Rows.Add(dr)
                    curYear = MySqliteReader("Year")
                End If
            ElseIf DbFields.Count = 3 Then 'FC - Group
                'AggreGate : Year, VYear,VVal
                If curYear <> MySqliteReader("Year") Then
                    Dim dr As DataRow = dt.NewRow()
                    dr(0) = 0
                    dr(1) = 0
                    dr(2) = MySqliteReader("Year")
                    dr(3) = MySqliteReader(2)
                    dt.Rows.Add(dr)
                    curYear = MySqliteReader("Year")
                End If
            Else ' 5 Cols 'FC - Decomp Group
                'SELECT [Year],* FROM [XDYAD] WHERE Actor = 5 And Partner = 57 Group By [Year]
                If curYear <> MySqliteReader("Year") Then
                    Dim dr As DataRow = dt.NewRow()
                    dr(0) = 0
                    dr(1) = 0
                    dr(2) = MySqliteReader("Year")
                    dr(3) = MySqliteReader(4)
                    dt.Rows.Add(dr)
                    curYear = MySqliteReader("Year")
                End If
            End If
        End While
        If DbFields.Count = 0 Then
            For k = 0 To dt.Columns.Count - 1
                DbFields.Add(dt.Columns(k).ColumnName)
            Next
        End If
        OutDT = dt
    End Sub
    Private Sub CheckBilateralLastYearWData()
        'ttt No need?
        Return
        '-----------------
        Dim y As Integer, v As Integer
        Dim BilateralLastYearWData As Integer
        BilateralLastYearWData = 0
        For v = 1 To Session("iCOUNTE")
            For y = Session("BilateralLastYear") - Session("BilateralFirstYear") + 1 To 1 Step -1
                If y + Session("BilateralFirstYear") - 1 < BilateralLastYearWData Then
                    Exit For
                End If
                If (Not Session("mPLOT")(y, v) Is Nothing) AndAlso (Not Session("mPLOT")(y, v).ToString = "") Then
                    If BilateralLastYearWData < y + Session("BilateralFirstYear") - 1 Then
                        BilateralLastYearWData = y + Session("BilateralFirstYear") - 1
                    End If
                    Exit For
                End If
            Next y
        Next v
        If BilateralLastYearWData > FirstYearForecast Then
            Session("BilateralLastYear") = BilateralLastYearWData
        End If
    End Sub
    Private Sub cmdRemoveAllDyads_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRemoveAllDyads.Click
        lstSelDyads.Items.Clear()
    End Sub
    Private Sub cmdCurrentPartner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCurrentPartner.Click
        lstDim1p_SelectedIndexChanged_NOPOSTBACK()
    End Sub
    Private Function SetPlotYear(a As Object, b As Object, y As Object) As KeyValuePair(Of String, Integer)
        'Return New KeyValuePair(Of String, Integer) With .Key = CInt(a).ToString.V
        Return New KeyValuePair(Of String, Integer)(CInt(a).ToString & "-" & CInt(b).ToString, CInt(y))
    End Function
    Private Function MakePlotKey(a As Object, b As Object) As String
        'Return CInt(a).ToString & "-" & CInt(b).ToString
        Return CInt(b).ToString & "-" & CInt(a).ToString
    End Function
    Private Function GetPlotYear(d As Dictionary(Of String, Integer), s As String) As Integer
        Return d(s)
    End Function
    Private Function Findj(d As Dictionary(Of String, Integer), i As Integer, y As Integer) As Integer
        For Each k As String In d.Keys
            Dim fi As String = k.Split("-")(0)
            Dim fj As String = k.Split("-")(1)
            If CInt(fi) = i AndAlso d(k) = y Then
                Return CInt(fj)
            End If
        Next
        Return -1
    End Function
    Private Function FindCol(d As Dictionary(Of String, Integer), dy As String, var As String) As Integer
        For Each k As String In d.Keys
            If k = dy & "|" & var Then
                Return d(k)
            End If
        Next
        Return -1
    End Function
    Private Sub lstDim1p_SelectedIndexChanged_NOPOSTBACK()
        'ttt COPY FROM lstDim1p_SelectedIndexChanged
        Dim i As Integer, j As Integer
        Dim lst = DirectCast(lstDim1p, ListBox)
        If lst.SelectedIndex <= 0 Then Return
        Session("sPartner") = lst.Items(lst.SelectedIndex).Value
        Session("bDecGrpPartner") = False
        Session("bGListPartner") = False
        If optGroupDec.Checked Then
            Session("bDecGrpPartner") = True
        ElseIf optGList.Checked Then
            Session("bGListPartner") = True
        End If

        If Session("sActor") > "" AndAlso Session("sPartner") > "" Then
            If (Not Session("bDecGrpActor")) AndAlso (Not Session("bGListActor")) AndAlso (Not Session("bDecGrpPartner")) AndAlso (Not Session("bGListPartner")) Then
                lstSelDyads.Items.Add(Session("sActor") & " - " & Session("sPartner"))
            Else

                Dim sActor2 As String, sPartner2 As String
                Dim ActorsCount As Integer, PartnersCount As Integer
                Dim rsActors As DataTable = New DataTable, rsPartners As DataTable = New DataTable
                Dim sql As String
                ActorsCount = 1
                PartnersCount = 1
                sActor2 = Session("sActor")
                sPartner2 = Session("sPartner")
                If Session("bDecGrpActor") Then
                    sql = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & Session("sActor") & """ ORDER BY R.Country"


                    'jam 2022/12/19
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                        ActorsCount = rsActors.Rows.Count

                    Else

                        vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                        vSQLiteCon = New SqliteConnection(vConnString)
                        vSQLiteCon.Open()

                        vSQLiteCommand = vSQLiteCon.CreateCommand()
                        vSQLiteCommand.CommandText = sql
                        vSQLiteReader = vSQLiteCommand.ExecuteReader()


                        ActorsCount = 0
                        While (vSQLiteReader.Read())
                            ActorsCount = ActorsCount + 1

                        End While
                        vSQLiteCon.Close()


                    End If




                End If
                If Session("bGListActor") Then
                    sql = "SELECT G.Region as Ctry " &
                    "FROM GList AS G, GListID AS GI " &
                    "WHERE G.GListCode = GI.Code AND G.Group = True AND GI.Description = """ & Session("sActor") & """"
                    sql = sql & " UNION SELECT R.Country as Ctry " &
                    "FROM " & Regionalization & " AS R, " & RegionIDNew & " AS RI, GList AS G, GListID AS GI " &
                    "WHERE R.NewRegion = RI.RegionNum AND RI.RegionName = G.Region AND G.GListCode = GI.Code " &
                    "AND G.Group = False AND GI.Description = """ & Session("sActor") & """"


                    'jam 2022/12/19
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsActors)
                        ActorsCount = rsActors.Rows.Count

                    Else

                        vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                        vSQLiteCon = New SqliteConnection(vConnString)
                        vSQLiteCon.Open()

                        vSQLiteCommand = vSQLiteCon.CreateCommand()
                        vSQLiteCommand.CommandText = sql
                        vSQLiteReader = vSQLiteCommand.ExecuteReader()


                        ActorsCount = 0
                        While (vSQLiteReader.Read())
                            ActorsCount = ActorsCount + 1

                        End While
                        vSQLiteCon.Close()


                    End If





                End If
                If Session("bDecGrpPartner") Then
                    sql = "SELECT R.Country FROM " & Regionalization & " AS R, " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE R.NewRegion = G.RegionNo AND G.GroupNo = GI.GroupNum AND GI.GroupName = """ & Session("sPartner") & """ ORDER BY R.Country"


                    'jam 2022/12/19
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                        PartnersCount = rsPartners.Rows.Count

                    Else

                        vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                        vSQLiteCon = New SqliteConnection(vConnString)
                        vSQLiteCon.Open()

                        vSQLiteCommand = vSQLiteCon.CreateCommand()
                        vSQLiteCommand.CommandText = sql
                        vSQLiteReader = vSQLiteCommand.ExecuteReader()


                        PartnersCount = 0
                        While (vSQLiteReader.Read())
                            PartnersCount = PartnersCount + 1

                        End While
                        vSQLiteCon.Close()


                    End If


                End If
                If Session("bGListPartner") Then
                    sql = "SELECT G.Region as Ctry " &
                    "FROM GList AS G, GListID AS GI " &
                    "WHERE G.GListCode = GI.Code AND G.Group = True AND GI.Description = """ & Session("sPartner") & """"
                    sql = sql & " UNION SELECT R.Country as Ctry " &
                    "FROM " & Regionalization & " AS R, " & RegionIDNew & " AS RI, GList AS G, GListID AS GI " &
                    "WHERE R.NewRegion = RI.RegionNum AND RI.RegionName = G.Region AND G.GListCode = GI.Code " &
                    "AND G.Group = False AND GI.Description = """ & Session("sPartner") & """"


                    'jam 2022/12/19
                    If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
                        SetDataTablefromAccess(Session("IFs_MDB2Active"), sql, "Country", rsPartners)
                        PartnersCount = rsPartners.Rows.Count

                    Else

                        vConnString = "DataSource=" + Session("IFs_MDB2Active") & ";"

                        vSQLiteCon = New SqliteConnection(vConnString)
                        vSQLiteCon.Open()

                        vSQLiteCommand = vSQLiteCon.CreateCommand()
                        vSQLiteCommand.CommandText = sql
                        vSQLiteReader = vSQLiteCommand.ExecuteReader()


                        PartnersCount = 0
                        While (vSQLiteReader.Read())
                            PartnersCount = PartnersCount + 1

                        End While
                        vSQLiteCon.Close()


                    End If



                End If
                For i = 1 To ActorsCount
                    If ActorsCount > 1 Then
                        sActor2 = rsActors.Rows(i - 1).Item(0) '.Fields(0)
                        'Continue For
                    End If
                    For j = 1 To PartnersCount
                        If PartnersCount > 1 Then
                            sPartner2 = rsPartners.Rows(j - 1).Item(0)
                        End If
                        lstSelDyads.Items.Add(sActor2 & " - " & sPartner2)
                    Next j
                Next i
            End If
            'Session("sActor") = ""
            Session("sPartner") = ""
        End If
    End Sub
    Public Sub LoadPlotBilat(ByVal i As Integer, ByVal j As Integer, v As Object)
        Session("mPLOT")(i, j) = v
    End Sub
    Private Function Xs(fm As String) As List(Of String)
        '"(2*(&(X01))+3*(&(X02)))"
        Dim res As New List(Of String)
        Dim s As String = fm
        'Case Computation A-B, no X01,X02
        If Not s.ToUpper.Contains("X") Then
            's = s.Replace("A", "X01").Replace("B", "X02")
            s = A2X(s)
        End If
        Do While True
            Dim p = s.IndexOf("X")
            If p = -1 Then Exit Do
            Dim s1 = s.Substring(p, 3)
            If Not res.Contains(s1) Then res.Add(s1)
            s = s.Substring(p + 3)
        Loop
        res.Sort()
        Return res

    End Function
    Private Function A2X(s As String) As String
        'K-A-B => X03-X01-X02, must be in alphabet order
        Dim res As String = s
        Dim Letters As List(Of String) = New List(Of String)
        For i = 1 To 20
            Dim lt = Char.ConvertFromUtf32(64 + i)
            If s.Contains(lt) Then
                If Not Letters.Contains(lt) Then
                    Letters.Add(lt)
                End If
            End If
        Next
        Letters.Sort()
        For k = 1 To Letters.Count
            res = res.Replace(Letters(k - 1), "X" & k.ToString.PadLeft(2, "0"))
        Next
        Return res
        'Dim AXs As New Dictionary(Of String, String)
        'For i = 1 To 20
        '    res = res.Replace(Char.ConvertFromUtf32(64 + i), "X" & i.ToString.PadLeft(2, "0"))
        '    'AXs.Add(Char.ConvertFromUtf32(64 + i), "X" & i.ToString.PadLeft(2, "0"))
        'Next
        'For i = 0 To s.Length - 1
        '    Dim c = s.Substring(i, 1)
        '    If AXs.ContainsKey(c) Then
        '        '   012A45
        '        res = res.Substring(0, i) & AXs(c) & res.Substring(i + 1)
        '    End If
        'Next
        'Dim numA As Integer = 65  ' letter A for instance
        ' Dim strValue As String = Char.ConvertFromUtf32(intValue)
        'Convert.ToByte("A") -> 65

    End Function
    Public Function RealQuery(s As String) As String
        If Not s.Contains("X") Then
            's = s.Replace("A", "X01").Replace("B", "X02")
            s = A2X(s)
        End If
        'Make real query from Formula, from RetrArr
        'To do: ?, #
        Return s.Replace("&", "Log").Replace("!", "(-1)*").Replace("%", "*100/")
    End Function
    Private Sub DeleteGravityVar()

        Dim sGravityVarName As String = lstVarNameSavedFormulas.SelectedItem.Text
        Dim p As Integer = sGravityVarName.IndexOf("-")
        If p > -1 Then
            sGravityVarName = sGravityVarName.Substring(0, p).Trim
        End If
        'no effect
        'ShowConfirm("Are you sure to Delete the Gravity variable name " & sGravityVarName & " ?")
        'remove selected list first
        lstSelSeries.ClearSelection()
        For j = 0 To lstSelSeries.Items.Count - 1
            If lstSelSeries.Items(j).Text = sGravityVarName Then
                lstSelSeries.Items(j).Selected = True
                Exit For
            End If
        Next
        cmdRemove_Click(cmdRemove, New EventArgs)
        '----

        '----COPIED FROM DGravity--------
        Dim db = Session("PutFdrvRun") + "DataDictDyadic.mdb"
        Dim sql As String

        'jam 2022/12/19
        If Session("mnuSQLite") Is Nothing Or Not Session("mnuSQLite") = "True" Then
            sql = "DELETE * FROM FormulaVars WHERE FormulaName = """ & sGravityVarName & """"
            updateAccessWith1Command(db, sql)

            sql = "DELETE * FROM FormulaVarsDet WHERE FormulaName = """ & sGravityVarName & """"
            updateAccessWith1Command(db, sql)

            sql = "DELETE * FROM Gravity WHERE GravityVarName = """ & sGravityVarName & """"
            updateAccessWith1Command(db, sql)

            sql = "DELETE * FROM  GravityDet WHERE GravityVarName = """ & sGravityVarName & """"
            updateAccessWith1Command(db, sql)
            '====RELOAD LIST
        Else

            vConnString = "DataSource=" + Session("PutFdrvRun") & "DataDictDyadic.db;"

            vSQLiteCon = New SqliteConnection(vConnString)
            vSQLiteCon.Open()


            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "DELETE * FROM FormulaVars WHERE FormulaName = """ & sGravityVarName & """"
            vSQLiteCommand.ExecuteNonQuery()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "DELETE * FROM FormulaVarsDet WHERE FormulaName = """ & sGravityVarName & """"
            vSQLiteCommand.ExecuteNonQuery()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "DELETE * FROM Gravity WHERE GravityVarName = """ & sGravityVarName & """"
            vSQLiteCommand.ExecuteNonQuery()

            vSQLiteCommand = vSQLiteCon.CreateCommand()
            vSQLiteCommand.CommandText = "DELETE * FROM  GravityDet WHERE GravityVarName = """ & sGravityVarName & """"
            vSQLiteCommand.ExecuteNonQuery()

            vSQLiteCon.Close()
        End If

        LoadformulaVars(db)
    End Sub

    Private Sub cmdEditGravity_Click(sender As Object, e As EventArgs) Handles cmdEditGravity.Click
        mnuEditGravity_Click()
    End Sub

    Private Sub cmdComputation_Click(sender As Object, e As EventArgs) Handles cmdComputation.Click
        Session("sOriginForm") = "frm_BilateralInteractionDisplay"
        Dim bHasComputedFormula As Boolean = False
        Dim bHasPlainVariable As Boolean = False
        If lstSelSeries.Items.Count > 0 Then
            'Check if is computed formulas
            'Then
            For t = 0 To lstSelSeries.Items.Count - 1
                If lstSelSeries.Items(t).Text.StartsWith("(") Then
                    bHasPlainVariable = True
                    Exit For
                End If
            Next
            If Not Session("aFormulas") Is Nothing Then
                For r = 0 To Session("aFormulas").Length - 1
                    If Not Session("aFormulas")(r) Is Nothing Then
                        For t = 0 To lstSelSeries.Items.Count - 1
                            If Session("aFormulas")(r).Name = lstSelSeries.Items(t).Text Then
                                bHasComputedFormula = True
                                Exit For
                            End If
                        Next
                    End If
                Next
            End If
        End If

        If lstSelSeries.Items.Count = 0 OrElse (bHasComputedFormula AndAlso (Not bHasPlainVariable)) Then
            'Make new/ another computed formula
            Session("BL_lbl_Var") = ""
            'Session("bComputation") = True
            Session("bComputation") = False
            If bHasComputedFormula AndAlso (Not bHasPlainVariable) Then
                'Make another computed formula
                Session("BL_OtherNewVar") = True
            Else
                'First new 
                ReDim Session("aFormulas")(1)
                Session("f") = Nothing
            End If
        Else
            If lstSelSeries.Items.Count = 0 Then
                ReDim Session("aFormulas")(1)
                Session("f") = Nothing
            End If
            'During declaration of new/another computed formula
            Session("bComputation") = True
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_computations.aspx")


    End Sub
    Private Sub Check2ShowEditFormula()
        Dim IsValidForEditFormula As Boolean = False
        Dim curlst = DirectCast(Session("curlstSelSeries"), ListBox)

        Dim sCurrentDisplay = If(curlst Is Nothing OrElse curlst.SelectedItem Is Nothing, "", curlst.SelectedItem.Text) ' lstSelSeries.List(lstSelSeries.ListIndex)
        If sCurrentDisplay = "" Then
            'False
        Else
            If Not (Session("aFormulas") Is Nothing) Then
                For k = 1 To Session("aFormulas").Length - 1
                    If Not (Session("aFormulas")(k) Is Nothing) Then
                        If Session("aFormulas")(k).Name = sCurrentDisplay Then
                            Dim f As Formula = Session("aFormulas")(k)
                            If Not f.Formula_Renamed.Contains("X0") Then
                                IsValidForEditFormula = True
                            End If
                            Exit For
                        End If
                    End If
                Next
            Else
                'False
            End If
        End If
        If Not IsValidForEditFormula Then
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = False
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = False
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = False
        Else
            cswmWebMenu.Groups("mnuRenameFormula").ShowMenuBarItem = False
            cswmWebMenu.Groups("mnuEditFormula").ShowMenuBarItem = True
            cswmWebMenu.Groups("mnuSaveFormula").ShowMenuBarItem = True
        End If
    End Sub
    Private Sub cmdSelectCurrent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSelectCurrent.Click
        If lstSeries.Visible Then
            lstSeries_SelectedIndexChanged(lstSeries, New EventArgs)
        ElseIf lstVarNameMonadyc.Visible Then
            lstVarNameMonadyc_SelectedIndexChanged(lstVarNameMonadyc, New EventArgs)
        ElseIf lstVarNameSavedFormulas.Visible Then
            lstVarNameSavedFormulas_SelectedIndexChanged(lstVarNameSavedFormulas, New EventArgs)
        ElseIf lstVarNameForecast.Visible Then
            lstVarNameForecast_SelectedIndexChanged(lstVarNameForecast, New EventArgs)
        ElseIf lstVarNameForecastWHist.Visible Then 'jrs 2021/02/04
            lstVarNameForecastWHist_SelectedIndexChanged(lstVarNameForecastWHist, New EventArgs)
        End If

    End Sub
    Public Function NoNullDecStr(o As Object) As String
        'ttt- function to convert null to empty string
        Try
            Dim res = CStr(CDec(o))
            Return res
        Catch ex As Exception

        End Try
        Return ""
    End Function

    'jrs 2021/03/04
    Private Sub lstVarNameForecastWHist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstVarNameForecastWHist.SelectedIndexChanged
        Dim lst = DirectCast(sender, ListBox)
        If lst.SelectedIndex = -1 Then Return
        Dim i As Integer = lst.SelectedIndex

        lstSelSeries.Items.Add("(FH) " & lst.Items(i).Text)

        Session("curlstSelSeries") = lstSelSeries
        ReDim Preserve Session("sAggRulesSelected")(lstSelSeries.Items.Count)
        Session("sAggRulesSelected")(lstSelSeries.Items.Count - 1) = Session("sAggRulesFor")(i)
        ReDim Preserve Session("sHistAnalogsSelected")(lstSelSeries.Items.Count)
        Session("sHistAnalogsSelected")(lstSelSeries.Items.Count - 1) = Session("sHistAnalogs")(i)

        ReDim Preserve Session("bDyadicSeries")(lstSelSeries.Items.Count)
        ReDim Preserve Session("bMonadicActorSeries")(lstSelSeries.Items.Count)
        Session("bDyadicSeries")(lstSelSeries.Items.Count - 1) = True

        If Session("bComputation") Then
            'Me.Hide
            'frm_computations.Show
            'frm_computations.SetFocus
            Exit Sub
        End If
    End Sub

    'jrs 2020/07/22
    Private Sub LoadForecast4FH(ByVal sVar As String, ByVal bGrpActor As Boolean, ByVal bGrpPartner As Boolean, ByVal sActorCtry As String, ByVal sPartnerCtry As String,
    ByRef dbDyFor As Object, ByVal ss As Integer, ByVal BilateralFirstYear As Integer, ByVal sAggRule As String, ByRef dbIFs As String, ByVal sUnits As String, ByRef mPLOTDict As Dictionary(Of String, Integer))
        Dim sTbl As String
        sTbl = sVar
        Dim sql As String, sqlFields As String, sqlFields2 As String, sqlWhere As String, sqlWHEREA As String, sqlWHEREP As String
        Dim MoveYrs As Integer
        Dim rsDY As DataTable = New DataTable, rsDY2 As DataTable = New DataTable, tblAux As DataTable = New DataTable
        Dim sActor As String, sPartner As String
        Dim bWeightAvg As Boolean
        bWeightAvg = False

        Dim sq0 As String = ""
        Dim DBFields = New List(Of String)
        Dim MySQliteReader As SqliteDataReader

        Dim iHalfWayIdx As Integer
        Dim bTooComplex As Boolean
        Dim iHalfWayStart As Integer
        Dim iStart As Integer, LagYrs As Integer, iLast As Integer

        Dim sEmpVar As String
        Dim sqlG As String
        Dim sql2 As String = ""

        Dim rsG As DataTable = New DataTable

        If Not bGrpActor And Not bGrpPartner Then
            'Need to change sActor and sPartner to RegionIdx
            sActor = CStr(IFsRegs.Item(getRegionFromCtry(sActorCtry)).Index)
            sPartner = CStr(IFsRegs.Item(getRegionFromCtry(sPartnerCtry)).Index)
            sql = "SELECT * FROM [" & sTbl & "] WHERE Actor = " & sActor & " AND Partner = " & sPartner
        Else
            sqlFields = ""
            sqlFields2 = ""
            sq0 = "Select * FROM [" & sTbl & "] where 1=2"
            SqliteTable(sq0, Session("dbDyFor")(ss), MySQliteReader, DBFields)
            If DBFields.Count - 2 > 150 Then
                iHalfWayIdx = DBFields.Count / 2
                bTooComplex = True
                iHalfWayStart = 2
            End If

            Select Case sAggRule
                Case "SUM"
                    'jrs 2022/05/30 For k = 2 To DBFields.Count - 1
                    'jrs 2020/07/16
                    'jrs 2022/05/30 If Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name >= Session("FirstYear") And Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name <= Session("LastYear") Then
                    'jrs 2022/05/30 If k < iHalfWayIdx Or Not bTooComplex Then
                    'If sqlFields > "" Then 'jrs 2020/07/16 If k > 2 Then
                    '                sqlFields = sqlFields & ", "
                    '            End If
                    sqlFields = sqlFields & "SUM(Val) as Y"
                                'Else
                                '    If sqlFields2 > "" Then 'jrs 2020/07/16 If k > iHalfWayIdx Then
                                '        sqlFields2 = sqlFields2 & ", "
                                '    End If
                                '    sqlFields2 = sqlFields2 & "SUM([" & Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name & "]) as Y" & Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name
                                'End If
                        'jrs 2022/05/30 End If
                        'jrs 2022/05/30 Next k
                Case "AVG"
                    'jrs 2022/05/30 For k = 2 To DBFields.Count - 1
                    'jrs 2020/07/16
                    'jrs 2022/05/30 If Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name >= Session("FirstYear") And Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name <= Session("LastYear") Then
                    'jrs 2022/05/30 If k < iHalfWayIdx Or Not bTooComplex Then
                    'If sqlFields > "" Then 'jrs 2020/07/16 k > 2
                    '    sqlFields = sqlFields & ", "
                    'End If
                    sqlFields = sqlFields & "AVG(Val) as Y"
                    'Else
                    '    If sqlFields2 > "" Then 'jrs 2020/07/16 If k > iHalfWayIdx Then
                    '        sqlFields2 = sqlFields2 & ", "
                    '    End If
                    '    sqlFields2 = sqlFields2 & "AVG([" & Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name & "]) as Y" & Session("dbDyFor")(ss).TableDefs(sTbl).Fields(k).Name
                    'End If
                    'jrs 2022/05/30 End If
                    'jrs 2022/05/30 Next k
                Case Else
                    bWeightAvg = True
                    sEmpVar = IFsAggRules.Item(sAggRule).ForecastVar
                    If sEmpVar > "" Then
                        'MsgBox "Oooops, currently we don't have access to regular forecast variables to do the aggregation", vbCritical, "Bilateral Interaction Display"
                        Exit Sub
                        'Oooops, currently we don't have access to regular forecast variables to do the aggregation
                        'Set tblAux = db.OpenRecordset("Series" & sEmpVar)
                        'tblAux.Index = "Country"
                    End If
                    sqlFields = "*"
            End Select
            sqlWhere = ""
            sqlWHEREA = ""
            sqlWHEREP = ""

            'jrs 2016/09/22 added if bForecast branch, else was there before
            If bGrpActor Then
                If sActor <> "World" Then
                    sqlG = "SELECT G.RegionNo FROM " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE G.GroupNo = GI.GroupNum AND GI.GroupName = """ & sActorCtry & """"
                    SetDataTablefromAccess(dbIFs, sqlG, "Country", rsG)

                    If Not rsG Is Nothing Then
                        For Each r As DataRow In rsG.Rows
                            If sqlWHEREA > "" Then
                                sqlWHEREA = sqlWHEREA & ", "
                            End If
                            sqlWHEREA = sqlWHEREA & r.Item("RegionNo")
                        Next
                    End If

                    sqlWHEREA = "Actor IN (" & sqlWHEREA & ")"
                End If
            Else
                sActor = CStr(IFsRegs.Item(getRegionFromCtry(sActorCtry)).Index)
                sqlWHEREA = "Actor = " & sActor
            End If

            If bGrpPartner Then
                If sPartnerCtry <> "World" Then
                    sqlG = "SELECT G.RegionNo FROM " & Grouping & " AS G, " & GroupIDNew &
                    " AS GI WHERE G.GroupNo = GI.GroupNum AND GI.GroupName = """ & sPartnerCtry & """"
                    SetDataTablefromAccess(dbIFs, sqlG, "Country", rsG)

                    If Not rsG Is Nothing Then
                        For Each r As DataRow In rsG.Rows
                            If sqlWHEREP > "" Then
                                sqlWHEREP = sqlWHEREP & ", "
                            End If
                            sqlWHEREP = sqlWHEREP & r.Item("RegionNo")
                        Next
                    End If
                    sqlWHEREP = "Partner IN (" & sqlWHEREP & ")"
                End If
            Else
                sPartner = CStr(IFsRegs.Item(getRegionFromCtry(sPartnerCtry)).Index)
                sqlWHEREP = "Partner = " & sPartner
            End If

            If sqlWHEREA > "" And sqlWHEREP > "" Then
                sqlWhere = sqlWHEREA & " AND " & sqlWHEREP
                'jrs 2017/08/14
                If bGrpActor Or bGrpPartner Then
                    sqlWhere = sqlWhere & " AND Actor <> Partner"
                End If
            ElseIf sqlWHEREA > "" Then
                sqlWhere = sqlWHEREA
            ElseIf sqlWHEREP > "" Then
                sqlWhere = sqlWHEREP
            End If

            'jrs 2022/05/30
            If bGrpActor Or bGrpPartner Then
                'Year twice to match logic for forecast only
                sqlFields = "[Year], Year, " & sqlFields
                sqlWhere = sqlWhere & " GROUP BY Year"
            End If


            sql = "SELECT " & sqlFields & " FROM [" & sTbl & "]" 'jrs 2019/10/16 added []
            If sqlWhere > "" Then
                sql = sql & " WHERE " & sqlWhere
            End If
            If bTooComplex And sqlFields2 > "" Then 'jrs 2019/10/16 added and sqlFields2>""
                sql2 = "SELECT " & sqlFields2 & " FROM [" & sTbl & "]" 'jrs 2019/10/16 added []
                If sqlWhere > "" Then
                    sql2 = sql2 & " WHERE " & sqlWhere
                End If
            End If
        End If

        SqliteTable(sql, Session("dbDyFor")(ss), MySQliteReader, DBFields, rsDY)

        If bTooComplex And sql2 > "" Then 'jrs 2019/10/16 added and sql2>""
            SqliteTable(sql2, Session("dbDyFor")(ss), MySQliteReader, DBFields, rsDY2)
        End If

        MoveYrs = 0

        If (Not rsDY Is Nothing) AndAlso rsDY.Rows.Count > 0 Then 'Not (rsDY.BOF And rsDY.EOF) Then
            If bWeightAvg Then
                ComputeWeightAvg(rsDY, tblAux, bGrpActor, bGrpPartner, Session("mPLOT"), Session("iCOUNTE"))
            Else
                If rsDY.Columns(0).ColumnName = "Actor" Then
                    iStart = 2 + Session("FirstYear") - CInt(rsDY.Rows(0).Item(2))
                Else
                    iStart = Session("FirstYear") - CInt(rsDY.Rows(0).Item(2))
                End If
                If iStart > 0 Then
                    If BilateralFirstYear > CInt(rsDY.Rows(0).Item(2)) Then
                        MoveYrs = BilateralFirstYear - CInt(rsDY.Rows(0).Item(2))
                        BilateralFirstYear = CInt(rsDY.Rows(0).Item(2))
                        LagYrs = 0
                    Else
                        LagYrs = CInt(rsDY.Rows(0).Item(2)) - BilateralFirstYear
                    End If
                    If Session("BilateralLastYear") < CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2)) Then
                        Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                    End If
                Else
                    If BilateralFirstYear > CInt(Mid(rsDY.Rows(0).Item(2), 2)) Then
                        MoveYrs = BilateralFirstYear - CInt(Mid(rsDY.Rows(0).Item(2), 2))
                        BilateralFirstYear = CInt(Mid(rsDY.Rows(0).Item(2), 2))
                        LagYrs = 0
                    Else
                        LagYrs = CInt(Mid(rsDY.Rows(0).Item(2), 2)) - BilateralFirstYear
                    End If
                    If Not bTooComplex Then
                        If Session("BilateralLastYear") < CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2)) Then
                            Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                        End If
                    Else
                        If Session("BilateralLastYear") < CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2)) Then
                            Session("BilateralLastYear") = CInt(rsDY.Rows(rsDY.Rows.Count - 1).Item(2))
                        End If
                    End If
                End If

                If MoveYrs > 0 Then
                    For m = 1 To Session("iCOUNTE") - 1
                        For k = Amin(Session("BilateralLastYear") - BilateralFirstYear + 1, 300 - MoveYrs) To 1 Step -1
                            Session("mPLOT")(k + MoveYrs, m) = Session("mPLOT")(k, m)
                            If k < MoveYrs + 1 Then
                                Session("mPLOT")(k, m) = Nothing
                            End If
                        Next k
                    Next m
                    '        If bFormula Then
                    '            frm_computations.LoadIdxDataAll = CurrNumbCompVar
                    '            frm_computations.MoveDataAll session("BilateralLastYear"), BilateralFirstYear, MoveYrs, j + 1
                    'End If
                End If

                If bGrpActor Or bGrpPartner Then
                    iLast = rsDY.Rows.Count + 1
                    If bTooComplex Then
                        iLast = iLast + rsDY2.Rows.Count
                    End If
                Else
                    iLast = rsDY.Rows.Count + 1
                End If


                For k = iStart To iLast
                    If k < iHalfWayIdx - iHalfWayStart Or Not bTooComplex Then
                        Session("mPLOT")(k - iStart + 1 + LagYrs, Session("iCOUNTE")) = rsDY.Rows(k - iStart).Item(3) 'rsDY.Columns(k).Value                            
                        If Not mPLOTDict.ContainsKey(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE"))) Then
                            mPLOTDict.Add(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE")), rsDY.Rows(k - iStart).Item(2))
                        End If
                    Else
                        Session("mPLOT")(k - iStart + 1 + LagYrs, Session("iCOUNTE")) = rsDY2.Rows(k - iHalfWayIdx + iHalfWayStart - 1).Item(3)
                        If Not mPLOTDict.ContainsKey(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE"))) Then
                            mPLOTDict.Add(MakePlotKey(k - iStart + 1 + LagYrs, Session("iCOUNTE")), rsDY.Rows(k - iStart).Item(2))
                        End If
                    End If
                Next k
            End If 'end of if using weight avg or else
        Else
            For k = 1 To UBound(Session("mPLOT"), 1)
                Session("mPLOT")(k, Session("iCOUNTE")) = Nothing
            Next k
        End If 'end of if recordset is not empty or else fill the plot with Nothings

        If IsNumeric(sActor) Then
            Session("mLegends")(1, Session("iCOUNTE")) = GRPS(1, CInt(sActor))
        Else
            Session("mLegends")(1, Session("iCOUNTE")) = sActorCtry
        End If
        If IsNumeric(sPartner) Then
            Session("mLegends")(2, Session("iCOUNTE")) = GRPS(1, CInt(sPartner))
        Else
            Session("mLegends")(2, Session("iCOUNTE")) = sPartnerCtry
        End If
        'jrs 2016/10/26
        If dbDyFor(ss) = DataFileDir$ & "IFsForDyadic.mdb" Then
            Session("mLegends")(5, Session("iCOUNTE")) = "Base"
        Else
            Session("mLegends")(5, Session("iCOUNTE")) = Replace(Replace(dbDyFor(ss), Session("PutFdrvRun") & "IFsForDyadic", ""), ".mdb", "")
        End If
        Session("mLegends")(3, Session("iCOUNTE")) = sUnits
        Session("mLegends")(4, Session("iCOUNTE")) = sVar

    End Sub

    'jrs 2024/08/28
    'Inputs: list of variables to load, list of dyads to load
    'Output: 1 csv file in Panel format, 1 row per dyad/year, all dyads are 35k, 30 years would mean about 1M rows    
    Private Sub ExportBigFileHist()
        Dim SelVars As String()
        ReDim SelVars(lstSelSeries.Items.Count)

        Dim SelVarTypes As String()
        ReDim SelVarTypes(lstSelSeries.Items.Count)

        Dim sType As String, sVarName As String
        Dim MonVals As Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, String))) = New Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, String)))
        Dim BilatVals As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, String)))) = New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Integer, String))))
        Dim pos As Integer, idx As Integer
        idx = 0

        'Open Monadic File
        'Open Bilateral File

        'Load the entire table for each variable, only check it it hasn't been loaded already
        For Each it In lstSelSeries.Items
            'Read variable name and type
            sType = it.ToString.Substring(1, 2)
            pos = it.ToString.IndexOf("-")
            sVarName = it.ToString.Substring(5, pos - 5)
            SelVars(idx) = sVarName
            SelVarTypes(idx) = sType
            'When loading each table, start creating a collection of years to use

            idx = idx + 1
        Next

        'close Monadic File
        'close Bilateral File

        'Create new/empty file
        'Start Writing a row for each dyad/year
    End Sub

    'jrs 2024/08/28
    Private Sub cmdExport_Click(sender As Object, e As EventArgs) Handles cmdExport.Click
        ExportBigFileHist()
    End Sub
End Class

