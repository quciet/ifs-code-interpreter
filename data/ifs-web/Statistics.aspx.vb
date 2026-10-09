Option Strict Off
Option Explicit On

Partial Class frm_statistics
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
            If Support.FormManager.LoadState Then
                Support.FormManager.StateRestore(Me)
                Support.FormManager.LoadState = False
            End If
            Dim aloop As Short
            Dim Msg As String
            Dim FieldNameLong As String
            Dim IndLabel As String
            Dim TransformStr As String
            Dim jloop As Short

            Dim tRow As Integer
            Dim nRow As DataRow
            Dim CreateQuery As String
            '-------------------------------------------------------------------------------------

            '   Size the window according to the screen
            '   ----------------------------------------
            Variables.Items.Clear()
            Primary.Items.Clear()
            Secondary.Items.Clear()
            WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = "Statistics"

            '    InFile$ = "IFsStat_MDB"
            '    OutFile$ = "IFsStat_MDB"
            '    FileCopy DataFileDir$ + "IFsStat.mdb", PutFdrvRun$ + "IFsStat.mdb"

            'JAM 2009-12-01
            If (Session("Transformation")) <> "" And Not IsNothing(Session("Transformation")) And Not IsDBNull(Session("Transformation")) Then
                Session("transtype") = Session("Transformation")
            Else
                Session("transtype") = ""
            End If

            'JAM 2009-12-28 Defining Transformation Type
            If Session("transtype") = "" Then
                If (Session("vTransformation")) <> "" And Not IsNothing(Session("vTransformation")) And Not IsDBNull(Session("vTransformation")) Then
                    Session("transtype") = Session("vTransformation")
                Else
                    Session("transtype") = ""
                End If
            End If

            If Not Session("bBubble") Or Session("GType") <> "SCAT" Then
                Session("pCOUNTE") = Session("COUNTE")
            Else
                'jrs 2011/04/07 session("pCOUNTE") = COUNTE / 3 * 2
                Session("pCOUNTE") = Session("COUNTE") / (Session("Total_Var") + 1) * (Session("Total_Var"))
            End If

            'jrs 2011/04/07 If Session("Statistics_Type") = 1 Then 'Displays the statistics across countries

            '   Set the scenario according to the number of independent variables
            '   ------------------------------------------------------------------
            'db8 = DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
            db8 = Session("IFsStat_MDB")

            'db8.Execute("Delete * from usysTStatFields")
            updateAccessWith1Command(db8, "Delete * from usysTStatFields")

            'tbl8 = db8.OpenRecordset("usysTStatFields")
            SetDataTablefromAccess(db8, "Select * from usysTStatFields", "", tbl8)
            'tbl8.AddNew()
            nRow = tbl8.NewRow
            nRow.BeginEdit()

            'nRow.Item("Scenario_ID") = Session("Total_Var")
            If Session("transtype") = "SEC" Then 'JAM 2009-12-28 Quadratic Scenario
                nRow.Item("Scenario_ID") = 10
            Else
                If Session("transtype") = "THI" Then   'JAM 2009-12-28 Third Dimension Scenario
                    nRow.Item("Scenario_ID") = 11
                Else
                    nRow.Item("Scenario_ID") = Session("Total_Var")
                End If
            End If

            nRow.Item("FieldDef_Name") = "Dependent"
            nRow.Item("FieldDef_Ordinal_Position") = 1
            nRow.Item("FieldDef_Choice") = "Y"
            nRow.EndEdit()
            tbl8.Rows.Add(nRow)
            'tbl8.Update()
            'tbl8.AcceptChanges()

            For jloop = 1 To Session("Total_Var")
                'tbl8.AddNew()
                nRow = tbl8.NewRow
                nRow.BeginEdit()
                'nRow.Item("Scenario_ID") = Session("Total_Var")
                If Session("transtype") = "SEC" Then   'JAM 2009-12-28 Quadratic Scenario
                    nRow.Item("Scenario_ID") = 10
                Else
                    If Session("transtype") = "THI" Then   'JAM 2009-12-28 Third Dimension Scenario
                        nRow.Item("Scenario_ID") = 11
                    Else
                        nRow.Item("Scenario_ID") = Session("Total_Var")
                    End If
                End If

                nRow.Item("FieldDef_Name") = "Independent" & Trim(Str(jloop))
                nRow.Item("FieldDef_Ordinal_Position") = jloop
                nRow.Item("FieldDef_Choice") = "X"
                nRow.EndEdit()
                tbl8.Rows.Add(nRow)
                'tbl8.Update()
                'tbl8.AcceptChanges()
            Next jloop
            WriteDataTable2Access(db8, "Select * from usysTStatFields", tbl8, "Insert")
            'tbl8.Close()
            'db8.Close()

            '   Run the set Scenario
            '   --------------------
            SampleRunScenarioRoutine()

            '   Prepare to show the results
            '   ----------------------------
            'db8 = DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
            'tbl8 = db8.OpenRecordset("TVBS_Regression")
            SetDataTablefromAccess(db8, "Select * from TVBS_Regression", "", tbl8)
            'If tbl8.BOF = False Then
            If tbl8.Rows.Count > 0 Then
                'tbl9 = db8.OpenRecordset("Var_Names")
                SetDataTablefromAccess(db8, "Select * from Var_Names", "", tbl9)
                'tbl9.MoveFirst()
                'tbl8.MoveFirst()
                tRow = 0

                '   Print Variable Names
                '   --------------------

                Select Case tbl9.Rows(tRow).Item("Transformation") '.Value
                    Case 0
                        TransformStr = "(LN)" 'jrs 2008/01/14 changed from Log to LN
                    Case 1
                        TransformStr = "(X^2)"
                    Case 2
                        TransformStr = "(X^" & Str(tbl9.Rows(tRow).Item("ValueB")) & ")"
                    Case 3
                        TransformStr = "(e^X)"
                    Case 4
                        TransformStr = "(None)"
                    Case 5  'JAM 2009-08-26 For Logit Option
                        TransformStr$ = "(Logit)"
                    Case 6  'JAM 2009-12-28 For Second Polynomial Option
                        TransformStr$ = "(X^2_Polynomial)"
                    Case 7  'JAM 2009-12-28 For Third Polynomial
                        TransformStr$ = "(X^3_Polynomial)"
                End Select

                Variables.Items.Add(New ListItem("Dependent Variable = " + tbl9.Rows(tRow).Item("Var_Name") + " " + TransformStr))
                For jloop = 1 To Session("Total_Var")
                    'tbl9.MoveNext()
                    tRow = tRow + 1
                    Select Case tbl9.Rows(tRow).Item("Transformation")
                        Case 0
                            TransformStr = "(LN)" 'jrs 2008/01/14 changed from Log to LN
                        Case 1
                            TransformStr = "(X^2)"
                        Case 2
                            TransformStr = "(X^" & Str(tbl9.Rows(tRow).Item("ValueB")) & ")"
                        Case 3
                            TransformStr = "(e^X)"
                        Case 4
                            TransformStr = "(None)"
                        Case 5  'JAM 2009-08-26 For Logit Option
                            TransformStr$ = "(Logit)"
                        Case 6  'JAM 2009-12-27 For Second Polynomial Option
                            TransformStr$ = "(X^2_Polynomial)"
                        Case 7  'JAM 2009-12-27 For Third Polynomial
                            TransformStr$ = "(X^3_Polynomial)"
                    End Select
                    IndLabel = "Independent" & Trim(Str(jloop)) & " Variable = "
                    Variables.Items.Add(New ListItem(IndLabel + tbl9.Rows(tRow).Item("Var_Name") + " " + TransformStr))
                Next jloop

                If (Session("transtype") = "SEC" Or Session("transtype") = "THI") Then    'JAM 2009-12-28 Quadratic and Third Grade Scenario
                    '   Print Primary Statistics
                    '   ------------------------
                    For jloop = 1 To Session("Total_Var")
                        'jrs 2012/06/13 FieldNameLong = "Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))
                        FieldNameLong$ = "Coef_of_X^" + Trim(Str(jloop))
                        If IsDBNull(tbl8.Rows(0).Item(FieldNameLong)) Then
                            Msg = "It is not possible to compute this regression.  There may "
                            Msg = Msg & "be no variation in an independent variable or "
                            Msg = Msg & "too much correlation among independent variables. "
                            Msg = Msg & "Please reformulate the regression."

                            'tbl8.Close()
                            'tbl9.Close()
                            'db8.Close()
                            LoadMessage(Msg, Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")
                        End If
                        If IsNumeric(tbl8.Rows(0).Item(FieldNameLong)) Then
                            Primary.Items.Add(New ListItem("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Rows(0).Item(FieldNameLong))))
                        End If
                    Next jloop
                    'JAM 2010-02-09
                    If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^2")) Then
                        Primary.Items.Add("Coef_of_X1^2 = " & Str(tbl8.Rows(0).Item("Coef_of_X^2")))
                    End If
                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-09
                        If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^3")) Then
                            Primary.Items.Add("Coef_of_X1^3 = " + Str(tbl8.Rows(0).Item("Coef_of_X^3")))
                        End If
                    End If

                    If IsNumeric(tbl8.Rows(0).Item("Y-Intercept")) Then
                        Primary.Items.Add(New ListItem("Y_Intercept = " & Str(tbl8.Rows(0).Item("Y-Intercept"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("R-Square")) Then
                        Primary.Items.Add(New ListItem("R-Square = " & Str(tbl8.Rows(0).Item("R-Square"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("Adj_R-Square")) Then
                        Primary.Items.Add(New ListItem("Adj_R-Square = " & Str(tbl8.Rows(0).Item("Adj_R-Square"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("F-Value")) Then
                        Primary.Items.Add(New ListItem("F-Value = " & Str(tbl8.Rows(0).Item("F-Value"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("Prob")) Then
                        Primary.Items.Add(New ListItem("Prob = " & Str(tbl8.Rows(0).Item("Prob"))))
                    End If

                    '   Print Secondary Statistics
                    '   --------------------------
                    If IsNumeric(tbl8.Rows(0).Item("SE_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("SE_of_X^1")) Then
                            Secondary.Items.Add(New ListItem("SE_of_X^1 = " &
                            Str(tbl8.Rows(0).Item("SE_of_X^1"))))
                        End If
                    Next jloop
                    'JAM 2010-02-09
                    If IsNumeric(tbl8.Rows(0).Item("SE_of_X^2")) Then
                        Secondary.Items.Add("SE_of_X1^2 = " +
                        Str(tbl8.Rows(0).Item("SE_of_X^2")))
                    End If
                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-09
                        If IsNumeric(tbl8.Rows(0).Item("SE_of_X^3")) Then
                            Secondary.Items.Add("SE_of_X1^3 = " +
                            Str(tbl8.Rows(0).Item("SE_of_X^3")))
                        End If
                    End If

                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^1")) Then
                            Secondary.Items.Add(New ListItem("Beta_of_X^1 = " &
                            Str(tbl8.Rows(0).Item("Beta_of_X^1"))))
                        End If
                    Next jloop

                    'JAM 2010-02-10
                    If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^2")) Then
                        Secondary.Items.Add("Beta_of_X1^2 = " +
                                Str(tbl8.Rows(0).Item("Beta_of_X^2")))
                    End If
                    If TransformStr$ = "THI" Then
                        'JAM 2010-02-10
                        If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^3")) Then
                            Secondary.Items.Add("Beta_of_X1^3 = " +
                                Str(tbl8.Rows(0).Item("Beta_of_X^3")))
                        End If
                    End If

                    If IsNumeric(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^1")) Then
                            Secondary.Items.Add(New ListItem("t-Value_of_X^1 = " &
                            Str(tbl8.Rows(0).Item("t-Value_of_X^1"))))
                        End If
                    Next jloop
                    'JAM 2010-02-10
                    If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^2")) Then
                        Secondary.Items.Add("t-Value_of_X1^2 = " +
                        Str(tbl8.Rows(0).Item("t-Value_of_X^2")))
                    End If

                    If TransformStr$ = "THI" Then
                        'JAM 2010-02-10
                        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^3")) Then
                            Secondary.Items.Add("t-Value_of_X1^3 = " +
                                Str(tbl8.Rows(0).Item("t-Value_of_X^3")))
                        End If
                    End If

                    If IsNumeric(tbl8.Rows(0).Item("Prob_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("Prob_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("Prob_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^1")) Then
                            Secondary.Items.Add(New ListItem("Prob_of_X^1 = " &
                            Str(tbl8.Rows(0).Item("Prob_of_X^1"))))
                        End If
                    Next jloop

                    'JAM 2010-02-10
                    If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^2")) Then
                        Secondary.Items.Add("Prob_of_X1^2 = " +
                            Str(tbl8.Rows(0).Item("Prob_of_X^2")))
                    End If

                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-10
                        If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^3")) Then
                            Secondary.Items.Add("Prob_of_X1^3 = " +
                                Str(tbl8.Rows(0).Item("Prob_of_X^3")))
                        End If
                    End If

                    If IsNumeric(tbl8.Rows(0).Item("Multiple_R")) Then
                        Secondary.Items.Add(New ListItem("Multiple_R = " & Str(tbl8.Rows(0).Item("Multiple_R"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("StdError_of_Estimate")) Then
                        Secondary.Items.Add(New ListItem("StdError_of_Estimate = " & Str(tbl8.Rows(0).Item("StdError_of_Estimate"))))
                    End If

                Else
                    '   Print Primary Statistics
                    '   ------------------------
                    For jloop = 1 To Session("Total_Var")
                        FieldNameLong = "Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))
                        If IsDBNull(tbl8.Rows(0).Item(FieldNameLong)) Then
                            Msg = "It is not possible to compute this regression.  There may "
                            Msg = Msg & "be no variation in an independent variable or "
                            Msg = Msg & "too much correlation among independent variables. "
                            Msg = Msg & "Please reformulate the regression."

                            'tbl8.Close()
                            'tbl9.Close()
                            'db8.Close()
                            LoadMessage(Msg, Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")
                        End If
                        If IsNumeric(tbl8.Rows(0).Item(FieldNameLong)) Then
                            Primary.Items.Add(New ListItem("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Rows(0).Item(FieldNameLong))))
                        End If
                    Next jloop
                    If IsNumeric(tbl8.Rows(0).Item("Y-Intercept")) Then
                        Primary.Items.Add(New ListItem("Y_Intercept = " & Str(tbl8.Rows(0).Item("Y-Intercept"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("R-Square")) Then
                        Primary.Items.Add(New ListItem("R-Square = " & Str(tbl8.Rows(0).Item("R-Square"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("Adj_R-Square")) Then
                        Primary.Items.Add(New ListItem("Adj_R-Square = " & Str(tbl8.Rows(0).Item("Adj_R-Square"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("F-Value")) Then
                        Primary.Items.Add(New ListItem("F-Value = " & Str(tbl8.Rows(0).Item("F-Value"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("Prob")) Then
                        Primary.Items.Add(New ListItem("Prob = " & Str(tbl8.Rows(0).Item("Prob"))))
                    End If

                    '   Print Secondary Statistics
                    '   --------------------------
                    If IsNumeric(tbl8.Rows(0).Item("SE_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(tbl8.Rows(0).Item("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(tbl8.Rows(0).Item("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop
                    If IsNumeric(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(tbl8.Rows(0).Item("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop

                    If IsNumeric(tbl8.Rows(0).Item("Prob_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("Prob_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("Prob_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(tbl8.Rows(0).Item("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(tbl8.Rows(0).Item("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop

                    If IsNumeric(tbl8.Rows(0).Item("Multiple_R")) Then
                        Secondary.Items.Add(New ListItem("Multiple_R = " & Str(tbl8.Rows(0).Item("Multiple_R"))))
                    End If
                    If IsNumeric(tbl8.Rows(0).Item("StdError_of_Estimate")) Then
                        Secondary.Items.Add(New ListItem("StdError_of_Estimate = " & Str(tbl8.Rows(0).Item("StdError_of_Estimate"))))
                    End If
                End If

                'JAM 2009-12-07 Adding Secondary Values for Dependent Variables
                If tbl9.Rows(tRow).Item("Transformation") >= 0 And tbl9.Rows(tRow).Item("Transformation") <= 7 Then
                    'vAverage,vStdDev,vCoefVar,vErrRatio
                    vAverage = FAverage()
                    'JAM 2010-02- 10
                    If IsNumeric(vAverage) Then
                        Secondary.Items.Add("Dependent Variable Average = " +
                        Str(vAverage))
                    End If

                    vStdDev = FStDev()

                    'JAM 2010-02-10
                    If IsNumeric(vStdDev) Then
                        Secondary.Items.Add("Dependent Variable Standard Deviation = " +
                       Str(vStdDev))
                    End If



                    If vAverage <> 0 And IsNumeric(vAverage) Then
                        vCoefVar = vStdDev / vAverage

                        'JAM 2010-02-10
                        If IsNumeric(vCoefVar) Then
                            Secondary.Items.Add("Dependent Variable Coefficient of Variation = " +
                            Str(vCoefVar))
                        End If
                    End If

                    Dim StdError As Single
                    StdError = tbl8.Rows(0).Item("StdError_of_Estimate")

                    If vAverage <> 0 And IsNumeric(vAverage) Then
                        vErrRatio = StdError / vAverage
                        'JAM 2010-02-10
                        If IsNumeric(vErrRatio) Then
                            Secondary.Items.Add("Ratio SE to mean = " +
                            Str(vErrRatio))
                        End If
                    End If
                End If

            Else
                'jrs 09/27/04 mnuPrint.Visible = False
                Msg = "There was an overflow while computing the statistics. Please "
                Msg = Msg & "choose different transformations for this set of variables."
                LoadMessage(Msg, Session("Message"))
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_Message.aspx")

            End If
            'tbl8.Close()
            'JRS 11/03/03
            'If Not (tbl9 Is Nothing) Then
            'tbl9.Close()
            'End If
            'db8.Close()

            'jrs 2011/04/07 End If

            'jrs 2011/04/07
            '    If Session("Statistics_Type") = 0 Then 'Display the statistics across regions (from Scatterplot Data)

            '        '   Determine the Total Number of Independent Variables
            '        '   ---------------------------------------------------

            '        Session("Total_Var") = Session("COUNTE") / NR - 1

            '        '   Open the database Statistics
            '        '   ----------------------------
            '        db8 = Session("IFsStat_MDB") 'DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
            '        'db8.TableDefs.Delete("Statistics")
            '        updateAccessWith1Command(db8, "Drop Table Statistics")

            '        '   Create a table called Statistics
            '        '   --------------------------------

            '        'table8 = db8.CreateTableDef("Statistics")
            '        'With table8
            '        '    .Fields.Append(.CreateField("Region", DAO.DataTypeEnum.dbText))
            '        '    .Fields.Append(.CreateField("Dependent", DAO.DataTypeEnum.dbInteger))
            '        '    For jloop = 1 To Session("Total_Var")
            '        '        .Fields.Append(.CreateField("Independent" & Trim(Str(jloop)), DAO.DataTypeEnum.dbInteger))
            '        '    Next jloop
            '        'End With

            '        'db8.TableDefs.Append(table8)
            '        'db8.Close()
            '        CreateQuery = "Create table Statistics ([Region] Text, [Dependent] Double"
            '        For jloop = 1 To Session("Total_Var")
            '            CreateQuery = CreateQuery + ", [Independent" + Trim(Str(jloop)) + "] Double"
            '        Next
            '        CreateQuery = CreateQuery + ")"
            '        updateAccessWith1Command(db8, CreateQuery)

            '        '   Open the table again and add the data from PLOT
            '        '   -----------------------------------------------
            '        'db8 = DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
            '        'db8.Execute("Delete * from Statistics")
            '        updateAccessWith1Command(db8, "Delete * from Statistics")

            '        'tbl8 = db8.OpenRecordset("Statistics")
            '        SetDataTablefromAccess(db8, "Select * from Statistics", "", tbl8)

            '        For aloop = 1 To Session("COUNTE") / (Session("Total_Var") + 1)
            '            'tbl8.AddNew()
            '            nRow = tbl8.NewRow
            '            nRow.BeginEdit()
            '            nRow.Item("Region") = GRPS(1, aloop)
            '            'jrs 2010/11/01
            '            If IsNumeric(Session("PLOT")(Session("PYear"), aloop)) Then
            '                nRow.Item("Dependent") = Session("PLOT")(Session("PYear"), aloop)
            '            End If
            '            For jloop = 1 To Session("Total_Var")
            '                If IsNumeric(Session("PLOT")(Session("PYear"), aloop + NR * jloop)) Then
            '                    nRow.Item("Independent" & Trim(Str(jloop))) = Session("PLOT")(Session("PYear"), aloop + NR * jloop)
            '                End If
            '            Next jloop
            '            nRow.EndEdit()
            '            tbl8.Rows.Add(nRow)
            '            'tbl8.Update()
            '        Next aloop
            '        'tbl8.AcceptChanges()
            '        WriteDataTable2Access(db8, "Select * from Statistics", tbl8, "Insert")
            '        'tbl8.Close()
            '        'db8.Close()

            '        '   Set the scenario according to the number of independent variables
            '        '   ------------------------------------------------------------------
            '        'db8 = Session("IFsStat_MDB") 'DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
            '        'db8.Execute("Delete * from usysTStatFields")
            '        updateAccessWith1Command(db8, "Delete * from usysTStatFields")
            '        'tbl8 = db8.OpenRecordset("usysTStatFields")
            '        SetDataTablefromAccess(db8, "Select * from usysTStatFields", "", tbl8)

            '        'tbl8.AddNew()
            '        nRow = tbl8.NewRow
            '        nRow.BeginEdit()
            '        nRow.Item("Scenario_ID") = Session("Total_Var")
            '        nRow.Item("FieldDef_Name") = "Dependent"
            '        nRow.Item("FieldDef_Ordinal_Position") = 1
            '        nRow.Item("FieldDef_Choice") = "Y"
            '        'tbl8.Update()
            '        nRow.EndEdit()
            '        tbl8.Rows.Add(nRow)


            '        For jloop = 1 To Session("Total_Var")
            '            'tbl8.AddNew()
            '            nRow = tbl8.NewRow
            '            nRow.BeginEdit()
            '            nRow.Item("Scenario_ID") = Session("Total_Var")
            '            nRow.Item("FieldDef_Name") = "Independent" & Trim(Str(jloop))
            '            nRow.Item("FieldDef_Ordinal_Position") = jloop
            '            nRow.Item("FieldDef_Choice") = "X"
            '            'tbl8.Update()
            '            nRow.EndEdit()
            '            tbl8.Rows.Add(nRow)
            '        Next jloop
            '        'db8.Close()
            '        WriteDataTable2Access(db8, "Select * from usysTStatFields", tbl8, "Insert")

            '        '   Run the set Scenario
            '        '   --------------------
            '        SampleRunScenarioRoutine()

            '        '   Prepare to show the results
            '        '   ----------------------------
            '        'db8 = DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
            '        'tbl8 = db8.OpenRecordset("TVBS_Regression")
            '        SetDataTablefromAccess(db8, "Select * from TVBS_Regression", "", tbl8)
            '        'tbl8.MoveFirst()
            '        tRow = 0

            '        '   Print Variable Names
            '        '   --------------------

            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Variables.Items.Add(New ListItem("DEPENDENT VARIABLE   = " & Session("VARN")(1, 1) & " " & Session("GR2N")(1)))
            '        For jloop = 1 To Session("Total_Var")
            '            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '            Variables.Items.Add(New ListItem("INDEPENDENT" & Trim(Str(jloop)) & " VARIABLE = " & Session("VARN")(1, 1 + NR * jloop) & " " & Session("GR2N")(1 + NR * jloop)))
            '        Next jloop

            '        '   Print Primary Statistics
            '        '   ------------------------
            '        For jloop = 1 To Session("Total_Var")
            '            FieldNameLong = "Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))
            '            'UPGRADE_WARNING: Use of Null/IsNull() detected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1049"'
            '            If IsDBNull(tbl8.Rows(tRow).Item(FieldNameLong)) Then
            '                Msg = "It is not possible to compute this regression.  There may "
            '                Msg = Msg & "be no variation in an independent variable or "
            '                Msg = Msg & "too much correlation among independent variables. "
            '                Msg = Msg & "Please reformulate the regression."

            '                LoadMessage(Msg, Session("Message"))
            '                Support.FormManager.StateStore(Me)
            '                Support.FormManager.Show(Me, "frm_Message.aspx")

            '            End If
            '            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '            Primary.Items.Add(New ListItem("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Rows(0).Item(FieldNameLong))))
            '        Next jloop
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Primary.Items.Add(New ListItem("Y_Intercept = " & Str(tbl8.Rows(tRow).Item("Y-Intercept"))))
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Primary.Items.Add(New ListItem("R-Square = " & Str(tbl8.Rows(tRow).Item("R-Square"))))
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Primary.Items.Add(New ListItem("F-Value = " & Str(tbl8.Rows(tRow).Item("F-Value"))))

            '        '   Print Secondary Statistics
            '        '   --------------------------
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Secondary.Items.Add(New ListItem("SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept"))))
            '        For jloop = 1 To Session("Total_Var")
            '            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '            Secondary.Items.Add(New ListItem("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Rows(0).Item("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
            '        Next jloop
            '        For jloop = 1 To Session("Total_Var")
            '            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '            Secondary.Items.Add(New ListItem("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Rows(0).Item("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
            '        Next jloop
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Secondary.Items.Add(New ListItem("t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept"))))
            '        For jloop = 1 To Session("Total_Var")
            '            'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '            Secondary.Items.Add(New ListItem("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Rows(0).Item("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
            '        Next jloop
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Secondary.Items.Add(New ListItem("Multiple_R = " & Str(tbl8.Rows(tRow).Item("Multiple_R"))))
            '        'CONVERSION_TODO: The equivalent of Property 'System.Windows.Forms.ListBox.Items' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
            '        Secondary.Items.Add(New ListItem("StdError_of_Estimate = " & Str(tbl8.Rows(tRow).Item("StdError_of_Estimate"))))

            '        'tbl8.Close()
            '        'db8.Close()

            '    End If

        End If

        'jrs 09/20/04
        'WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = WinFormsToWeb_PageBar.Rows(0).Cells(0).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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
    Public mnuPrevious As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuPrint As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    Public mnuSave As ArtinSoft.WinFormsToWeb.WebControls.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
#End Region
    'CONVERSION_NOTE: Region 'Upgrade Support' can be removed from source code.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1002.html'.
    '#Region "Upgrade Support "
    'Private Shared m_vb6FormDefInstance As frm_statisticsPrivate Shared m_InitializingDefInstance As Boolean
    'Public Shared Property DefInstance() As frm_statistics
    'Get 
    'If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then 
    'm_InitializingDefInstance = True
    'm_vb6FormDefInstance = New frm_statistics()
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
    '------------------------------------------Statistics.Frm-------------------------------
    '---------------------------------------------------------------------------------------
    '
    '
    '
    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '   Last modified by BBH on 9/11/99

    Dim table8 As DataTable 'DAO.TableDef
    Dim tbl8 As DataTable 'DAO.Recordset
    Dim tbl9 As DataTable 'DAO.Recordset
    Dim db8 As String 'DataTable 'DAO.Database
    'jrs 2012/06/05
    Dim vAverage, vStdDev, vCoefVar, vErrRatio As Double

    '==========================frm_statistics=============================================
    Private Sub frm_statistics_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
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
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Previous Menu"
        cswmGroup1.ID = "Group_a624"
        If Session("Statistics_Type") = 1 Or (Session("Statistics_Type") = 0 And Session("pCOUNTE") > 0) Then
            'Support.FormManager.StateStore(Me)
            'jrs 2006/11/15 cswmGroup1.DownLevelURL = "Transform.aspx"
            cswmGroup1.OnMenuBarClick = "location.href='TransformPy.aspx';"
            'frm_Transform.DefInstance.Show() 
        Else
            If Session("pCOUNTE") = 0 Then
                cswmGroup1.OnMenuBarClick = "location.href='frm_LongPlot.aspx';"
            Else
                'JAM 2009-12-04 Returning to Proessential Form
                If Session("Statistics_Type") = 0 Then
                    cswmGroup1.OnMenuBarClick = "location.href='frm_GraphicalDisplay.aspx';"
                Else
                    cswmGroup1.OnMenuBarClick = "location.href='frm_DisplayMenu.aspx';"
                End If
            End If
            'Support.FormManager.StateStore(Me)
            'jrs 2006/11/15 cswmGroup1.DownLevelURL = "frm_DisplayMenu.aspx" ' "DISPMENU.aspx"
            'frm_DisplayMenu.DefInstance.Show() 
        End If


        'mti commenting save and print 
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Save"
        cswmGroup1.ID = "Group_8830"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Save"

        'jrs 2006/12/18
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/04 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16051);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
    End Sub

    '==========================frm_statistics=============================================
    Public Function SampleRunScenarioRoutine() As Boolean
        '-------------------------------------------------------------------------------------

        ' Comments  : Runs the Total VB Statistics analysis
        '             Scenario Named : []
        '             Scenario Number: Session("Total_Var")
        ' Parameters: None
        ' Returns   : True if successful, else False
        ' Source    : Generated by Total VB Statistics 6.0

        Dim stats As FMSSTA65.TotalVBStatistics
        Dim fOK As Boolean

        stats = New FMSSTA65.TotalVBStatistics

        With stats
            .ScenarioDatabase = Session("IFsStat_MDB") 'IFsStat_MDB
            .SourceDatabase = Session("IFsStat_MDB") 'IFsStat_MDB
            .OutputDatabase = Session("IFsStat_MDB") 'IFsStat_MDB
            .ScenarioTbl = "usysTStatScenarios"
            .OptionsTbl = "usysTStatOptions"
            .FieldTbl = "usysTStatFields"
            .SourceTableName = "Statistics"
            .OutputTableName = "TVBS_Regression"
            .OutputTableName2 = "TVBS_Residual"
            .ShowErrors = False
            .ShowProgress = False
            'jrs 2012/06/04 fOK = .RunScenario(Session("Total_Var"))
            'JAM 2009-12-17
            If Session("transtype") = "SEC" Then
                fOK = .RunScenario(10)
            Else   'JAM 2009-12-19
                If Session("transtype") = "THI" Then
                    fOK = .RunScenario(11)
                Else
                    fOK = .RunScenario(Session("Total_Var"))
                End If
            End If

        End With

    End Function

    '==========================frm_statistics=============================================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuPrevious_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPrevious.Popup
    '    mnuPrevious_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuPrevious_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        '-------------------------------------------------------------------------------------

        'Support.FormManager.Close(Me)
        If Session("Statistics_Type") = 1 Or (Session("Statistics_Type") = 0 And Session("COUNTE") > 0) Then
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Transform.aspx")
            'frm_Transform.DefInstance.Show() 
        Else
            If Session("COUNTE") = 0 Then
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_LongPlot.aspx")
            Else
                'JAM 2009-12-04 Returning to Proessential Form
                If Session("Statistics_Type") = 0 Then
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "frm_GraphicalDisplay.aspx")
                Else
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "DISPMENU.aspx")
                End If
            End If
            'frm_DisplayMenu.DefInstance.Show() 
        End If
    End Sub

    '==========================frm_statistics=============================================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuPrint_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPrint.Popup
    '    mnuPrint_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        'JRSVBNET *** BEGIN *** 
        '      Dim IndLabel As String
        'Dim jloop As Short
        'Dim TransformStr As String
        ''-------------------------------------------------------------------------------------

        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'Carret = Chr(13) & Chr(10)
        ''UPGRADE_WARNING: Couldn't resolve default property of object Carret. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        'carret2 = Carret + Carret

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''   Prepare to show the results
        ''   ----------------------------
        'db8 = DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
        'tbl8 = db8.OpenRecordset("TVBS_Regression")
        ''    If tbl8.BOF = False Then
        'tbl9 = db8.OpenRecordset("Var_Names")
        'tbl9.MoveFirst()
        'tbl8.MoveFirst()

        ''   Print Variable Names
        ''   --------------------

        'Select Case tbl9.Fields("Transformation").Value
        '	Case 0
        '		TransformStr = "(Log)"
        '	Case 1
        '		TransformStr = "(X^2)"
        '	Case 2
        '		TransformStr = "(X^" & Str(tbl9.Fields("ValueB").Value) & ")"
        '	Case 3
        '		TransformStr = "(e^X)"
        '	Case 4
        '		TransformStr = "(None)"
        'End Select

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("Dependent Variable = " + tbl9.Fields("Var_Name").Value + " " + TransformStr)
        'For jloop = 1 To Session("Total_Var")
        '	tbl9.MoveNext()
        '	Select Case tbl9.Fields("Transformation").Value
        '		Case 0
        '			TransformStr = "(Log)"
        '		Case 1
        '			TransformStr = "(X^2)"
        '		Case 2
        '			TransformStr = "(X^" & Str(tbl9.Fields("ValueB").Value) & ")"
        '		Case 3
        '			TransformStr = "(e^X)"
        '		Case 4
        '			TransformStr = "(None)"
        '	End Select
        '	IndLabel = "Independent" & Trim(Str(jloop)) & " Variable = "
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print(IndLabel + tbl9.Fields("Var_Name").Value + " " + TransformStr)
        'Next jloop

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''   Print Primary Statistics
        ''   ------------------------
        'For jloop = 1 To Session("Total_Var")
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Fields("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))).Value))
        'Next jloop
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("Y_Intercept = " & Str(tbl8.Fields("Y-Intercept").Value))
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("R-Square = " & Str(tbl8.Fields("R-Square").Value))
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("Adj R-Square = " & Str(tbl8.Fields("Adj_R-Square").Value))
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("F-Value = " & Str(tbl8.Fields("F-Value").Value))
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("Probability of zero coefs = " & Str(tbl8.Fields("Prob").Value))


        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print()

        ''   Print Secondary Statistics
        ''   --------------------------
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("SE_of_Y-Intercept = " & Str(tbl8.Fields("SE_of_Y-Intercept").Value))
        'For jloop = 1 To Session("Total_Var")
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Fields("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))).Value))
        'Next jloop
        'For jloop = 1 To Session("Total_Var")
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Fields("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))).Value))
        'Next jloop

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("t-Value_of_Y-Intercept = " & Str(tbl8.Fields("t-Value_of_Y-Intercept").Value))
        'For jloop = 1 To Session("Total_Var")
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Fields("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))).Value))
        'Next jloop

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("Prob_of_Y-Intercept = " & Str(tbl8.Fields("Prob_of_Y-Intercept").Value))
        'For jloop = 1 To Session("Total_Var")
        '	'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        '	'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        '	Printer.Print("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(tbl8.Fields("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))).Value))
        'Next jloop

        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("Multiple_R = " & Str(tbl8.Fields("Multiple_R").Value))
        ''UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2068"'
        ''UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.Print("StdError_of_Estimate = " & Str(tbl8.Fields("StdError_of_Estimate").Value))
        ''    End If

        'tbl8.Close()
        'tbl9.Close()
        'db8.Close()

        ''UPGRADE_ISSUE: Printer method Printer.NewPage was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.NewPage()
        ''UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2069"'
        'Printer.EndDoc()
        ''UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'JRSVBNET *** END *** 
    End Sub

    '==========================frm_statistics=============================================
    'CONVERSION_ISSUE: Event 'System.Windows.Forms.MenuItem.Popup' is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1000.html'.
    'Public Sub mnuSave_Popup(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuSave.Popup
    '    mnuSave_Click(eventSender, eventArgs)
    'End Sub
    Public Sub mnuSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
        Dim IndLabel As String
        Dim jloop As Short
        Dim TransformStr As String
        Dim fs As Object
        Dim tRow As Integer
        '-------------------------------------------------------------------------------------

        '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Carret = Chr(13) & Chr(10)
        carret2 = Carret + Carret

        Dim FileNumber As Short
        FileNumber = FreeFile() + 1
        fs = CreateObject("Scripting.FileSystemObject")
        'If it already exists
        'UPGRADE_WARNING: Couldn't resolve default property of object fs.FileExists. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
        If fs.FileExists(Session("FdrvRun") & "statistics.dat") Then
            Kill(Session("FdrvRun") & "statistics.dat")
        End If
        FileOpen(FileNumber, Session("FdrvRun") & "statistics.dat", OpenMode.Append)

        '   Prepare to show the results
        '   ----------------------------
        db8 = Session("IFsStat_MDB") 'DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
        SetDataTablefromAccess(db8, "Select * from TVBS_Regression", "", tbl8)
        'tbl8 = db8.OpenRecordset("TVBS_Regression")
        '    If tbl8.BOF = False Then
        'tbl9 = db8.OpenRecordset("Var_Names")
        SetDataTablefromAccess(db8, "Select * from Var_Names", "", tbl9)
        'tbl9.MoveFirst()
        'tbl8.MoveFirst()
        tRow = 0

        '   Print Variable Names
        '   --------------------

        Select Case tbl9.Rows(tRow).Item("Transformation")
            Case 0
                TransformStr = "(Log)"
            Case 1
                TransformStr = "(X^2)"
            Case 2
                TransformStr = "(X^" & Str(tbl9.Rows(tRow).Item("ValueB")) & ")"
            Case 3
                TransformStr = "(e^X)"
            Case 4
                TransformStr = "(None)"
            Case 5  'JAM 2009-08-26 For Logit Option
                TransformStr$ = "(Logit)"
            Case 6  'JAM 2009-12-28 For Second Grade Polynomial
                TransformStr$ = "(X^2_Polynomial)"
            Case 7  'JAM 2009-12-28 For Second Grade Polynomial
                TransformStr$ = "(X^3_Polynomial)"
        End Select

        PrintLine(FileNumber, TAB, "Dependent Variable = " + tbl9.Rows(tRow).Item("Var_Name") + " " + TransformStr)
        For jloop = 1 To Session("Total_Var")
            'tbl9.MoveNext()
            tRow = tRow + 1
            Select Case tbl9.Rows(tRow).Item("Transformation")
                Case 0
                    TransformStr = "(LN)"
                Case 1
                    TransformStr = "(X^2)"
                Case 2
                    TransformStr = "(X^" & Str(tbl9.Rows(tRow).Item("ValueB")) & ")"
                Case 3
                    TransformStr = "(e^X)"
                Case 4
                    TransformStr = "(None)"
                Case 5  'JAM 2009-08-26 For Logit Option
                    TransformStr$ = "(Logit)"
                Case 6  'JAM 2009-12-28 For Second Grade Polynomial
                    TransformStr$ = "(X^2_Polynomial)"
                Case 7  'JAM 2009-12-28 For Second Grade Polynomial
                    TransformStr$ = "(X^3_Polynomial)"
            End Select

            'JAM 2009-12-02     Transformation from DisplyaMenu Form
            If Session("transtype") <> "" Then
                Select Case Session("transtype")   'JAM 2009-12-01
                    Case "LOG"
                        TransformStr$ = "(LN)"
                    Case "SEC"
                        TransformStr$ = "(X^2)"
                    Case "THI"
                        TransformStr$ = "(X^3)"
                    Case "POW"
                        TransformStr$ = "(X^" + Str(Session("PowerDegree")) + ")"
                    Case "EXP"
                        TransformStr$ = "(e^X)"
                    Case "LIN"
                        TransformStr$ = "(None)"
                    Case "SAT"
                        TransformStr$ = "(Logit)"
                End Select
                '            Else
                '                TransformStr$ = ""
            End If

            IndLabel = "Independent" & Trim(Str(jloop)) & " Variable = "
            PrintLine(FileNumber, TAB, IndLabel + tbl9.Rows(tRow).Item("Var_Name") + " " + TransformStr)
        Next jloop

        PrintLine(FileNumber, TAB, "")
        PrintLine(FileNumber, TAB, "")

        If (Session("transtype") = "SEC" Or Session("transtype") = "THI") Then  'JAM 2009-12-19 Quadratic and Third Dimension Scenarios
            '   Print Primary Statistics
            '   ------------------------
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^" & Trim(Str(jloop)))) Then
                    PrintLine(FileNumber, TAB, "Coef_of_X" & Trim(Str(jloop)) & "^1 = " & Str(tbl8.Rows(0).Item("Coef_of_X^" & Trim(Str(jloop)))))
                End If
            Next jloop
            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^2")) Then
                PrintLine(FileNumber, TAB, "Coef_of_X1^2 = " + Str(tbl8.Rows(0).Item("Coef_of_X^2")))
            End If
            If Session("transtype") = "THI" Or TransformStr$ = "(X^3)" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "Coef_of_X1^3 = " + Str(tbl8.Rows(0).Item("Coef_of_X^3")))
                End If
            End If

            If IsNumeric(tbl8.Rows(0).Item("Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "Y_Intercept = " & Str(tbl8.Rows(0).Item("Y-Intercept")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("R-Square")) Then
                PrintLine(FileNumber, TAB, "R-Square = " & Str(tbl8.Rows(0).Item("R-Square")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("Adj_R-Square")) Then
                PrintLine(FileNumber, TAB, "Adj R-Square = " & Str(tbl8.Rows(0).Item("Adj_R-Square")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("F-Value")) Then
                PrintLine(FileNumber, TAB, "F-Value = " & Str(tbl8.Rows(0).Item("F-Value")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("Prob")) Then
                PrintLine(FileNumber, TAB, "Probability of zero coefs = " & Str(tbl8.Rows(0).Item("Prob")))
            End If

            PrintLine(FileNumber, TAB, "")
            PrintLine(FileNumber, TAB, "")

            '   Print Secondary Statistics
            '   --------------------------
            If IsNumeric(tbl8.Rows(0).Item("SE_of_Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept")))
            End If
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("SE_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "SE_of_X" & Trim(Str(jloop)) & "^1 = " &
                    Str(tbl8.Rows(0).Item("SE_of_X^1")))
                End If
            Next jloop
            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("SE_of_X^2")) Then
                PrintLine(FileNumber, TAB, "SE_of_X1^2 = " +
                        Str(tbl8.Rows(0).Item("SE_of_X^2")))
            End If
            If Session("transtype") = "THI" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("SE_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "SE_of_X1^3 = " +
                        Str(tbl8.Rows(0).Item("SE_of_X^3")))
                End If
            End If

            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "Beta_of_X" & Trim(Str(jloop)) & "^1 = " &
                    Str(tbl8.Rows(0).Item("Beta_of_X^1")))
                End If
            Next jloop
            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^2")) Then
                PrintLine(FileNumber, TAB, "Beta_of_X1^2 = " +
                Str(tbl8.Rows(0).Item("Beta_of_X^2")))
            End If


            If Session("transtype") = "THI" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "Beta_of_X1^3 = " +
                        Str(tbl8.Rows(0).Item("Beta_of_X^3")))
                End If
            End If

            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")))
            End If
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "t-Value_of_X" & Trim(Str(jloop)) & "^1 = " &
                    Str(tbl8.Rows(0).Item("t-Value_of_X^1")))
                End If
            Next jloop
            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^2")) Then
                PrintLine(FileNumber, TAB, "t-Value_of_X1^2 = " +
                Str(tbl8.Rows(0).Item("t-Value_of_X^2")))
            End If


            If Session("transtype") = "THI" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "t-Value_of_X1^3 = " +
                        Str(tbl8.Rows(0).Item("t-Value_of_X^3")))
                End If
            End If

            If IsNumeric(tbl8.Rows(0).Item("Prob_of_Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "Prob_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("Prob_of_Y-Intercept")))
            End If
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "Prob_of_X" & Trim(Str(jloop)) & "^1 = " &
                    Str(tbl8.Rows(0).Item("Prob_of_X^1")))
                End If
            Next jloop

            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^2")) Then
                PrintLine(FileNumber, TAB, "Prob_of_X1^2 = " +
                Str(tbl8.Rows(0).Item("Prob_of_X^2")))
            End If
            If Session("transtype") = "THI" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "Prob_of_X1^3 = " +
                       Str(tbl8.Rows(0).Item("Prob_of_X^3")))
                End If
            End If

            If IsNumeric(tbl8.Rows(0).Item("Multiple_R")) Then
                PrintLine(FileNumber, TAB, "Multiple_R = " & Str(tbl8.Rows(0).Item("Multiple_R")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("StdError_of_Estimate")) Then
                PrintLine(FileNumber, TAB, "StdError_of_Estimate = " & Str(tbl8.Rows(0).Item("StdError_of_Estimate")))
            End If

        Else
            '   Print Primary Statistics
            '   ------------------------
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^" + Trim(Str(jloop)))) Then
                    PrintLine(FileNumber, TAB, "Coef_of_X" & Trim(Str(jloop)) & "^1 = " & Str(tbl8.Rows(0).Item("Coef_of_X^" & Trim(Str(jloop)))))
                End If
            Next jloop
            If IsNumeric(tbl8.Rows(0).Item("Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "Y_Intercept = " & Str(tbl8.Rows(0).Item("Y-Intercept")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("R-Square")) Then
                PrintLine(FileNumber, TAB, "R-Square = " & Str(tbl8.Rows(0).Item("R-Square")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("Adj_R-Square")) Then
                PrintLine(FileNumber, TAB, "Adj R-Square = " & Str(tbl8.Rows(0).Item("Adj_R-Square")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("F-Value")) Then
                PrintLine(FileNumber, TAB, "F-Value = " & Str(tbl8.Rows(0).Item("F-Value")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("Prob")) Then
                PrintLine(FileNumber, TAB, "Probability of zero coefs = " & Str(tbl8.Rows(0).Item("Prob")))
            End If

            PrintLine(FileNumber, TAB, "")
            PrintLine(FileNumber, TAB, "")

            '   Print Secondary Statistics
            '   --------------------------
            If IsNumeric(tbl8.Rows(0).Item("SE_of_Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept")))
            End If
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("SE_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "SE_of_X" & Trim(Str(jloop)) & "^1=" &
                    Str(tbl8.Rows(0).Item("SE_of_X^1")))
                End If
            Next jloop
            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("SE_of_X^2")) Then
                PrintLine(FileNumber, TAB, "SE_of_X1^2 = " +
                        Str(tbl8.Rows(0).Item("SE_of_X^2")))
            End If
            If Session("transtype") = "THI" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("SE_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "SE_of_X1^3 = " +
                        Str(tbl8.Rows(0).Item("SE_of_X^3")))
                End If
            End If

            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "Beta_of_X" & Trim(Str(jloop)) & "^1 = " &
                    Str(tbl8.Rows(0).Item("Beta_of_X^1")))
                End If
            Next jloop

            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")))
            End If
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "t-Value_of_X^1=" &
                    Str(tbl8.Rows(0).Item("t-Value_of_X^1")))
                End If
            Next jloop
            'JAM 2010-02-10
            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^2")) Then
                PrintLine(FileNumber, TAB, "t-Value_of_X1^2 = " +
                Str(tbl8.Rows(0).Item("t-Value_of_X^2")))
            End If


            If Session("transtype") = "THI" Then
                'JAM 2010-02-10
                If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^3")) Then
                    PrintLine(FileNumber, TAB, "t-Value_of_X1^3 = " +
                        Str(tbl8.Rows(0).Item("t-Value_of_X^3")))
                End If
            End If

            If IsNumeric(tbl8.Rows(0).Item("Prob_of_Y-Intercept")) Then
                PrintLine(FileNumber, TAB, "Prob_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("Prob_of_Y-Intercept")))
            End If
            For jloop = 1 To Session("Total_Var")
                If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^1")) Then
                    PrintLine(FileNumber, TAB, "Prob_of_X^1 = " &
                    Str(tbl8.Rows(0).Item("Prob_of_X^1")))
                End If
            Next jloop

            If IsNumeric(tbl8.Rows(0).Item("Multiple_R")) Then
                PrintLine(FileNumber, TAB, "Multiple_R = " & Str(tbl8.Rows(0).Item("Multiple_R")))
            End If
            If IsNumeric(tbl8.Rows(0).Item("StdError_of_Estimate")) Then
                PrintLine(FileNumber, TAB, "StdError_of_Estimate = " & Str(tbl8.Rows(0).Item("StdError_of_Estimate")))
            End If

        End If

        'JAM 2009-12-02 Writing to DAT File new parameters
        If Session("transtype") <> "" Or (tbl9.Rows(tRow).Item("Transformation") >= 0 And tbl9.Rows(tRow).Item("Transformation") <= 7) Then
            'vAverage,vStdDev,vCoefVar,vErrRatio
            vAverage = FAverage()
            'JAM 2010-02-10
            If IsNumeric(vAverage) Then
                PrintLine(FileNumber, TAB, "Dependent Variable Average = " +
                    Str(vAverage))
            End If

            vStdDev = FStDev()
            'JAM 2010-02-10
            If IsNumeric(vStdDev) Then
                PrintLine(FileNumber, TAB, "Dependent Variable Standard Deviation = " +
                    Str(vStdDev))
            End If

            If vAverage <> 0 And IsNumeric(vAverage) Then
                vCoefVar = vStdDev / vAverage
                'JAM 2010-02-10
                If IsNumeric(vCoefVar) Then
                    PrintLine(FileNumber, TAB, "Dependent Variable Coefficient of Variation = " +
                        Str(vCoefVar))
                End If
            End If

            Dim StdError As Single
            StdError = tbl8.Rows(0).Item("StdError_of_Estimate")


            If vAverage <> 0 And IsNumeric(vAverage) Then
                vErrRatio = StdError / vAverage
                'JAM 2010-02-10
                If IsNumeric(vErrRatio) Then
                    PrintLine(FileNumber, TAB, "Ratio SE to mean = " +
                        Str(vErrRatio))
                End If
            End If


        End If


        '    End If

        'tbl8.Close()
        'tbl9.Close()
        'db8.Close()

        FileClose(FileNumber)
        'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
        'jrs 10/01/04 WinFormsToWeb_MessageBox.Show("File statistics.txt saved in Runfile directory", MsgBoxStyle.OKOnly, "Statistics")
        'JRS 10/01/04 SAVE FILE IN CLIENT *****
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-Disposition", "attachment; filename=statistics.txt")
        Response.Flush()
        Response.WriteFile(Session("FdrvRun") & "statistics.dat")
        Response.End()
        '**************************************
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e



        'Choose the correct function
        Select Case objEA.Item.Caption
                'Case "Continue"
                '    mnuContinue_Click(sender, e)
            Case "Save"
                mnuSave_Click(sender, e)

        End Select
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2009-12-02  Function to calculate Average for Dependent Variable
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function FAverage() As Double
        Dim average As Double
        Dim valTotal As Integer
        Dim i As Integer

        average = 0
        valTotal = 0
        If Session("Statistics_Type") = 0 Then
            For i = 1 To Session("pCOUNTE") / 2
                If IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                    average = average + Session("PLOT")(Session("PYear"), i)
                    valTotal = valTotal + 1
                End If
            Next i
        Else    'JAM 2009-12-07  Average calculation from Scatter
            For i = 1 To Session("CCount")
                If IsNumeric(Session("CVarD")(i)) Then
                    average = average + Session("CVarD")(i)
                    valTotal = valTotal + 1
                End If
            Next i

        End If

        average = average / valTotal

        FAverage = average

    End Function
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    '
    '       JAM 2009-12-02  Function to calculate Average for Dependent Variable
    '
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function FStDev() As Double
        Dim Stdev As Double
        Dim valTotal As Integer
        Dim i As Integer

        valTotal = 0
        If Session("Statistics_Type") = 0 Then
            For i = 1 To Session("pCOUNTE") / 2
                If IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                    valTotal = valTotal + 1
                End If
            Next i

            Stdev = 0
            If (valTotal - 1) <> 0 Then
                For i = 1 To Session("pCOUNTE") / 2
                    If IsNumeric(Session("PLOT")(Session("PYear"), i)) Then
                        Stdev = Stdev + ((Session("PLOT")(Session("PYear"), i) - vAverage) ^ 2) / (valTotal - 1)
                    End If
                Next i
                Stdev = Math.Sqrt(Stdev)
            End If
        Else    'JAM 2009-12-07 Standard Deviation for Scatvars
            For i = 1 To Session("CCount")
                If IsNumeric(Session("CVarD")(i)) Then
                    valTotal = valTotal + 1
                End If
            Next i

            Stdev = 0
            If (valTotal - 1) <> 0 Then
                For i = 1 To Session("CCount")
                    If IsNumeric(Session("CVarD")(i)) Then
                        Stdev = Stdev + ((Session("CVarD")(i) - vAverage) ^ 2) / (valTotal - 1)
                    End If
                Next i
                Stdev = Math.Sqrt(Stdev)
            End If

        End If

        FStDev = Stdev

    End Function


End Class
