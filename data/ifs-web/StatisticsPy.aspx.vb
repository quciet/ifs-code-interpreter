Option Strict Off
Option Explicit On

Partial Class frm_statisticsPy
    Inherits Page
#Region " Web Form Designer Generated Code "
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'Imports Python.Included
        'Imports System.Threading.Tasks

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

            'nRow.Item("Scenario_ID") = Session("Total_Var")
            If Session("transtype") = "SEC" Then 'JAM 2009-12-28 Quadratic Scenario
                'nRow.Item("Scenario_ID") = 10
            Else
                If Session("transtype") = "THI" Then   'JAM 2009-12-28 Third Dimension Scenario
                    'nRow.Item("Scenario_ID") = 11
                Else
                    'nRow.Item("Scenario_ID") = Session("Total_Var")
                End If
            End If


            '   Run the set Scenario
            '   --------------------
            SampleRunScenarioRoutine()

            If Session("R-Square") > -1 Then
                '   Prepare to show the results
                '   ----------------------------
                tRow = 0

                '   Print Variable Names
                '   --------------------

                Select Case Session("TransformationDep") '.Value
                    Case 0
                        TransformStr = "(LN)" 'jrs 2008/01/14 changed from Log to LN
                    Case 1
                        TransformStr = "(X^2)"
                    Case 2
                        TransformStr = "(X^" & Str(Session("ValueB_Dep")) & ")"
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

                Variables.Items.Add(New ListItem("Dependent Variable = " + Session("VarNameD") + " " + TransformStr))
                For jloop = 1 To Session("Total_Var")
                    'tbl9.MoveNext()
                    tRow = tRow + 1
                    Select Case Session("TransformationInd" + jloop.ToString)
                        Case 0
                            TransformStr = "(LN)" 'jrs 2008/01/14 changed from Log to LN
                        Case 1
                            TransformStr = "(X^2)"
                        Case 2
                            TransformStr = "(X^" & Str(Session("ValueB_Ind" + jloop.ToString)) & ")"
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
                    Variables.Items.Add(New ListItem(IndLabel + Session("VarNameI")(jloop - 1) + " " + TransformStr))
                Next jloop

                If (Session("transtype") = "SEC" Or Session("transtype") = "THI") Then    'JAM 2009-12-28 Quadratic and Third Grade Scenario
                    '   Print Primary Statistics
                    '   ------------------------
                    For jloop = 1 To Session("Total_Var")
                        'jrs 2012/06/13 FieldNameLong = "Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))
                        FieldNameLong$ = "Coef_of_X1^" + Trim(Str(jloop))
                        If IsDBNull(Session(FieldNameLong)) Then
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
                        If IsNumeric(Session(FieldNameLong)) Then
                            Primary.Items.Add(New ListItem("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(Session(FieldNameLong))))
                        End If
                    Next jloop
                    'JAM 2010-02-09
                    If IsNumeric(Session("Coef_of_X1^2")) Then
                        Primary.Items.Add("Coef_of_X1^2 = " & Str(Session("Coef_of_X1^2")))
                    End If
                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-09
                        If IsNumeric(Session("Coef_of_X1^3")) Then
                            Primary.Items.Add("Coef_of_X1^3 = " + Str(Session("Coef_of_X1^3")))
                        End If
                    End If

                    If IsNumeric(Session("Y-Intercept")) Then
                        Primary.Items.Add(New ListItem("Y_Intercept = " & Str(Session("Y-Intercept"))))
                    End If
                    If IsNumeric(Session("R-Square")) Then
                        Primary.Items.Add(New ListItem("R-Square = " & Str(Session("R-Square"))))
                    End If
                    If IsNumeric(Session("Adj_R-Square")) Then
                        Primary.Items.Add(New ListItem("Adj_R-Square = " & Str(Session("Adj_R-Square"))))
                    End If
                    If IsNumeric(Session("F-Value")) Then
                        Primary.Items.Add(New ListItem("F-Value = " & Str(Session("F-Value"))))
                    End If
                    If IsNumeric(Session("Prob")) Then
                        Primary.Items.Add(New ListItem("Prob = " & Str(Session("Prob"))))
                    End If

                    '   Print Secondary Statistics
                    '   --------------------------
                    If IsNumeric(Session("SE_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("SE_of_Y-Intercept = " & Str(Session("SE_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("SE_of_X1^1")) Then
                            Secondary.Items.Add(New ListItem("SE_of_X1^1 = " &
                                Str(Session("SE_of_X1^1"))))
                        End If
                    Next jloop
                    'JAM 2010-02-09
                    If IsNumeric(Session("SE_of_X1^2")) Then
                        Secondary.Items.Add("SE_of_X1^2 = " +
                            Str(Session("SE_of_X1^2")))
                    End If

                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-09
                        If IsNumeric(Session("SE_of_X1^3")) Then
                            Secondary.Items.Add("SE_of_X1^3 = " +
                                Str(Session("SE_of_X1^3")))
                        End If
                    End If


                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("Beta_of_X1^1")) Then
                            Secondary.Items.Add(New ListItem("Beta_of_X1^1 = " &
                                Str(Session("Beta_of_X1^1"))))
                        End If
                    Next jloop


                    'JAM 2010-02-10
                    If IsNumeric(Session("Beta_of_X1^2")) Then
                        Secondary.Items.Add("Beta_of_X1^2 = " +
                                    Str(Session("Beta_of_X1^2")))
                    End If
                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-10
                        If IsNumeric(Session("Beta_of_X1^3")) Then
                            Secondary.Items.Add("Beta_of_X1^3 = " +
                                Str(Session("Beta_of_X1^3")))
                        End If
                    End If

                    If IsNumeric(Session("t-Value_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("t-Value_of_Y-Intercept = " & Str(Session("t-Value_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("t-Value_of_X1^1")) Then
                            Secondary.Items.Add(New ListItem("t-Value_of_X1^1 = " &
                                Str(Session("t-Value_of_X1^1"))))
                        End If
                    Next jloop
                    'JAM 2010-02-10
                    If IsNumeric(Session("t-Value_of_X1^2")) Then
                        Secondary.Items.Add("t-Value_of_X1^2 = " +
                            Str(Session("t-Value_of_X1^2")))
                    End If

                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-10
                        If IsNumeric(Session("t-Value_of_X1^3")) Then
                            Secondary.Items.Add("t-Value_of_X1^3 = " +
                                    Str(Session("t-Value_of_X1^3")))
                        End If
                    End If

                    If IsNumeric(Session("Prob_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("Prob_of_Y-Intercept = " & Str(Session("Prob_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("Prob_of_X1^1")) Then
                            Secondary.Items.Add(New ListItem("Prob_of_X1^1 = " &
                            Str(Session("Prob_of_X1^1"))))
                        End If
                    Next jloop

                    'JAM 2010-02-10
                    If IsNumeric(Session("Prob_of_X1^2")) Then
                        Secondary.Items.Add("Prob_of_X1^2 = " +
                                Str(Session("Prob_of_X1^2")))
                    End If

                    If Session("transtype") = "THI" Then
                        'JAM 2010-02-10
                        If IsNumeric(Session("Prob_of_X1^3")) Then
                            Secondary.Items.Add("Prob_of_X1^3 = " +
                                    Str(Session("Prob_of_X1^3")))
                        End If
                    End If

                    If IsNumeric(Session("Multiple_R")) Then
                        Secondary.Items.Add(New ListItem("Multiple_R = " & Str(Session("Multiple_R"))))
                    End If
                    If IsNumeric(Session("StdError_of_Estimate")) Then
                        Secondary.Items.Add(New ListItem("StdError_of_Estimate = " & Str(Session("StdError_of_Estimate"))))
                    End If

                Else
                    '   Print Primary Statistics
                    '   ------------------------
                    For jloop = 1 To Session("Total_Var")
                        FieldNameLong = "Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))
                        If IsDBNull(Session(FieldNameLong)) Then
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
                        If IsNumeric(Session(FieldNameLong)) Then
                            Primary.Items.Add(New ListItem("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " & Str(Session(FieldNameLong))))
                        End If
                    Next jloop
                    If IsNumeric(Session("Y-Intercept")) Then
                        Primary.Items.Add(New ListItem("Y_Intercept = " & Str(Session("Y-Intercept"))))
                    End If
                    If IsNumeric(Session("R-Square")) Then
                        Primary.Items.Add(New ListItem("R-Square = " & Str(Session("R-Square"))))
                    End If
                    If IsNumeric(Session("Adj_R-Square")) Then
                        Primary.Items.Add(New ListItem("Adj_R-Square = " & Str(Session("Adj_R-Square"))))
                    End If
                    If IsNumeric(Session("F-Value")) Then
                        Primary.Items.Add(New ListItem("F-Value = " & Str(Session("F-Value"))))
                    End If
                    If IsNumeric(Session("Prob")) Then
                        Primary.Items.Add(New ListItem("Prob = " & Str(Session("Prob"))))
                    End If

                    '   Print Secondary Statistics
                    '   --------------------------
                    If IsNumeric(Session("SE_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("SE_of_Y-Intercept = " & Str(Session("SE_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(Session("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(Session("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop
                    If IsNumeric(Session("t-Value_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("t-Value_of_Y-Intercept = " & Str(Session("t-Value_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(Session("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop

                    If IsNumeric(Session("Prob_of_Y-Intercept")) Then
                        Secondary.Items.Add(New ListItem("Prob_of_Y-Intercept = " & Str(Session("Prob_of_Y-Intercept"))))
                    End If
                    For jloop = 1 To Session("Total_Var")
                        If IsNumeric(Session("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)))) Then
                            Secondary.Items.Add(New ListItem("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop)) & " = " &
                            Str(Session("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))))))
                        End If
                    Next jloop

                    If IsNumeric(Session("Multiple_R")) Then
                        Secondary.Items.Add(New ListItem("Multiple_R = " & Str(Session("Multiple_R"))))
                    End If
                    If IsNumeric(Session("StdError_of_Estimate")) Then
                        Secondary.Items.Add(New ListItem("StdError_of_Estimate = " & Str(Session("StdError_of_Estimate"))))
                    End If
                End If

                'JAM 2009-12-07 Adding Secondary Values for Dependent Variables
                If Session("TransformationDep") >= 0 And Session("TransformationDep") <= 7 Then
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
                    StdError = Session("StdError_of_Estimate")

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
    Public Sub SampleRunScenarioRoutine()
        '-------------------------------------------------------------------------------------

        ' Comments  : Runs the Total VB Statistics analysis
        '             Scenario Named : []
        '             Scenario Number: Total_Var
        ' Parameters: None
        ' Returns   : True if successful, else False
        ' Source    : Generated by Total VB Statistics 6.0            

        'Python.Runtime.PythonEngine.Initialize()

        'Using Python.Runtime.Py.GIL

        '    Dim gs = Python.Runtime.PythonEngine.AcquireLock()

        '    'import sys
        '    'import os
        '    'import numpy as np
        '    'From scipy import stats

        '    'f_path = sys.argv[1]
        '    'With open(f_path, mode = "r") As f
        '    '    f_lines = f.read().split("\n")
        '    '                    assert f.closed

        '    ''''
        '    '                    print(len(f_lines))
        '    '                    For l in f_lines
        '    '    print(l)
        '    '                        '''

        '    'Dependent Variable
        '    'var2 = [float(i) for i in f_lines[2].split(",")[1:]]

        '    'Independent Variable
        '    'var1 = [float(i) for i in f_lines[1].split(",")[1:]]

        '    'slope, intercept, r_value, p_value, std_err = stats.linregress(x = var1, y = var2)
        '    'output_str = f"Slope:{slope}\nIntercept:{intercept}\nR-Value:{r_value}\nP-Value:{p_value}\nStd-Err:{std_err}"

        '    'Dim sk1 = Python.Runtime.Py.Import("sklearn.linear_model")
        '    'sk1.LinearRegression().fit(x, y)

        '    'From scipy import stats
        '    'Dim sk2 = Python.Runtime.Py.Import("stats")
        '    'sk2.Invoke()

        '    'Dim np = Python.Runtime.Py.Import("numpy")
        '    'Dim sys = Python.Runtime.Py.Import("sys")
        '    'Dim pp = Python.Runtime.Py.Import("pprint")
        '    'pp.pprint(np.cos(np.pi * 2))

        '    'create a Python scope
        '    Using scope As Python.Runtime.PyScope = Python.Runtime.Py.CreateScope()
        '        ' convert the Person object to a PyObject
        '        'PyObject pyPerson = person.ToPython();

        '        ' create a Python variable "person"
        '        'scope.Set("person", pyPerson)

        '        ' the person object may now be used in Python
        '        Dim code As String

        '        Dim y_Line As String = ""
        '        For i = 0 To Session("AllValidPoints") - 1
        '            y_Line = y_Line & Session("TransformedData")(i, 1).ToString
        '            If i < Session("AllValidPoints") - 1 Then
        '                y_Line = y_Line & ","
        '            End If
        '        Next
        '        code = "f_lines = " & y_Line
        '        scope.Exec(code)
        '        code = "var2 = [float(i) for i in f_lines.split("","")[1:]]"
        '        scope.Exec(code)

        '        Dim x_Line As String = ""
        '        For i = 0 To Session("AllValidPoints") - 1
        '            x_Line = x_Line & Session("TransformedData")(i, 2).ToString
        '            If i < Session("AllValidPoints") - 1 Then
        '                x_Line = x_Line & ","
        '            End If
        '        Next

        '        code = "f_lines = " & x_Line
        '        scope.Exec(code)
        '        code = "var1 = [float(i) for i in f_lines.split("","")[1:]]"
        '        scope.Exec(code)

        '        code = "slope, intercept, r_value, p_value, std_err = stats.linregress(x = var1, y = var2)"
        '        scope.Exec(code)

        '        Dim slope As Python.Runtime.PyFloat = scope.Get("slope")
        '        Dim txtslope As String = slope.ToString
        '    End Using

        '    'print(output_str)
        '    Python.Runtime.PythonEngine.ReleaseLock(gs)
        'End Using

        'We will use Ordinary Least Squares to create a
        ' linear regression model with an intercept term
        'Dim ols = New Accord.Statistics.Models.Regression.Linear.OrdinaryLeastSquares()
        'ols.UseIntercept = True

        Dim CompletePoints As Integer
        CompletePoints = Session("AllValidPoints") - 1
        Dim DepVar() As Double
        ReDim DepVar(CompletePoints)

        Dim IndVars = New Double(Session("Total_Var") - 1)() {
            }
        'ReDim IndVars(Session("Total_Var") - 1)(CompletePoints)
        Dim j As Integer = 0
        Dim k As Integer
        For k = 1 To Session("Total_Var")
            Dim Inds(CompletePoints) As Double
            IndVars(k - 1) = Inds
        Next

        Dim bValidInd As Boolean
        For i = 0 To Session("AllValidPoints") - 1
            bValidInd = True
            For k = 1 To Session("Total_Var")
                If Not IsNumeric(Session("TransformedData")(i, k + 1)) Then
                    bValidInd = False
                    Exit For
                End If
            Next
            If IsNumeric(Session("TransformedData")(i, 1)) And bValidInd Then
                DepVar(j) = Session("TransformedData")(i, 1)
                For k = 1 To Session("Total_Var")
                    IndVars(k - 1)(j) = Session("TransformedData")(i, k + 1)
                Next
                j = j + 1
            Else
                CompletePoints = CompletePoints - 1
                ReDim Preserve DepVar(CompletePoints)
                'ReDim Preserve IndVars(Session("Total_Var") - 1)(CompletePoints)
                For k = 1 To Session("Total_Var")
                    Dim Inds(CompletePoints + 1) As Double
                    Inds = IndVars(k - 1)
                    ReDim Preserve Inds(CompletePoints)
                    IndVars(k - 1) = Inds
                Next
            End If
        Next

        'Use Ordinary Least Squares to estimate a regression model
        'Dim regression As Accord.Statistics.Models.Regression.Linear.SimpleLinearRegression = ols.Learn(IndVars, DepVar)
        'Session("Coef_of_X^1") = regression.Slope
        'Session("Y-Intercept") = regression.Intercept
        'Session("R-Square") = regression.CoefficientOfDetermination(IndVars, DepVar)
        'Session("Adj_R-Square") = regression.CoefficientOfDetermination(IndVars, DepVar, True)

        If CompletePoints > 0 Then
            'Transpose Inputs
            Dim tIndVars = New Double(CompletePoints)() {
            }
            Dim wtIndVars = New Double(CompletePoints)() {
            }
            Dim wDepVar() As Double
            ReDim wDepVar(CompletePoints)
            Dim DVar = New Double(CompletePoints)() {
            }

            'Session("transtype") = "SEC" Or Session("transtype") = "THI"
            'These 2 are only allowed for 1 independent variable
            Dim Total_Var As Integer = Session("Total_Var")
            If Session("transtype") = "SEC" Then
                Total_Var = Total_Var + 1
            ElseIf Session("transtype") = "THI" Then
                Total_Var = Total_Var + 2
            End If

            For i = 0 To CompletePoints
                Dim Inds(Total_Var - 1) As Double
                tIndVars(i) = Inds
                Dim wInds(Total_Var - 1) As Double
                wtIndVars(i) = wInds
                For k = 1 To Session("Total_Var")
                    tIndVars(i)(k - 1) = IndVars(k - 1)(i)
                    If Session("transtype") = "SEC" Then
                        tIndVars(i)(k) = IndVars(k - 1)(i) ^ 2
                    ElseIf Session("transtype") = "THI" Then
                        tIndVars(i)(k) = IndVars(k - 1)(i) ^ 2
                        tIndVars(i)(k + 1) = IndVars(k - 1)(i) ^ 3
                    End If
                Next
                DVar(i) = New Double(0) {}
                DVar(i)(0) = DepVar(i)
            Next

            'Create the descriptive analysis
            Dim AnalysisI = New Accord.Statistics.Analysis.DescriptiveAnalysis()
            AnalysisI.Learn(tIndVars)

            Dim AnalysisD = New Accord.Statistics.Analysis.DescriptiveAnalysis()
            AnalysisD.Learn(DVar)

            For i = 0 To CompletePoints
                For k = 1 To Total_Var
                    wtIndVars(i)(k - 1) = (tIndVars(i)(k - 1) - AnalysisI.Means(k - 1)) / AnalysisI.StandardDeviations(k - 1)
                    If k = 1 Then
                        wDepVar(i) = (DepVar(i) - AnalysisD.Means(k - 1)) / AnalysisD.StandardDeviations(k - 1)
                    End If
                Next
            Next

            Dim mlra = New Accord.Statistics.Analysis.MultipleLinearRegressionAnalysis(True)
            mlra.Learn(tIndVars, DepVar)

            Dim wmlra = New Accord.Statistics.Analysis.MultipleLinearRegressionAnalysis(True)
            wmlra.Learn(wtIndVars, wDepVar)

            Session("Y-Intercept") = mlra.CoefficientValues(Total_Var)
            Session("R-Square") = mlra.RSquared
            Session("Adj_R-Square") = mlra.RSquareAdjusted
            Session("F-Value") = mlra.FTest.Statistic
            Session("Prob") = mlra.FTest.PValue

            Session("SE_of_Y-Intercept") = mlra.StandardErrors(Total_Var)
            Session("t-Value_of_Y-Intercept") = mlra.Coefficients(Total_Var).TTest.Statistic
            Session("Prob_of_Y-Intercept") = mlra.Coefficients(Total_Var).TTest.PValue

            If (Session("transtype") = "SEC" Or Session("transtype") = "THI") Then

                Session("Coef_of_X1^1") = mlra.CoefficientValues(0)
                Session("SE_of_X1^1") = mlra.StandardErrors(0)
                Session("Beta_of_X1^1") = wmlra.CoefficientValues(0)
                Session("t-Value_of_X1^1") = mlra.Coefficients(0).TTest.Statistic
                Session("Prob_of_X1^1") = mlra.Coefficients(0).TTest.PValue

                Session("Coef_of_X1^2") = mlra.CoefficientValues(1)
                Session("SE_of_X1^2") = mlra.StandardErrors(1)
                Session("Beta_of_X1^2") = wmlra.CoefficientValues(1)
                Session("t-Value_of_X1^2") = mlra.Coefficients(1).TTest.Statistic
                Session("Prob_of_X1^2") = mlra.Coefficients(1).TTest.PValue

                If (Session("transtype") = "THI") Then
                    Session("Coef_of_X1^3") = mlra.CoefficientValues(2)
                    Session("SE_of_X1^3") = mlra.StandardErrors(2)
                    Session("Beta_of_X1^3") = wmlra.CoefficientValues(2)
                    Session("t-Value_of_X1^3") = mlra.Coefficients(2).TTest.Statistic
                    Session("Prob_of_X1^3") = mlra.Coefficients(2).TTest.PValue
                End If

            Else
                For jloop = 1 To Total_Var
                    Session("Coef_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))) = mlra.CoefficientValues(jloop - 1)
                    Session("SE_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))) = mlra.StandardErrors(jloop - 1)
                    Session("Beta_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))) = wmlra.CoefficientValues(jloop - 1)
                    Session("t-Value_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))) = mlra.Coefficients(jloop - 1).TTest.Statistic
                    Session("Prob_of_X" & Trim(Str(jloop)) & ": Independent" & Trim(Str(jloop))) = mlra.Coefficients(jloop - 1).TTest.PValue
                Next
            End If
            'In Excel, when there's 1 IV, it seems to be (R^2)^2
            'Wikipedia says when there's 1 IV, it should be R^2
            Session("Multiple_R") = Math.Sqrt(Session("R-Square"))
            Session("StdError_of_Estimate") = mlra.StandardError

        Else
            Session("R-Square") = -1
        End If

        Session("ChangeSatPoint") = Nothing

    End Sub

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
            Support.FormManager.Show(Me, "frm_TransformPy.aspx")
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
    'Public Sub mnuSave_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs)
    '    Dim IndLabel As String
    '    Dim jloop As Short
    '    Dim TransformStr As String
    '    Dim fs As Object
    '    Dim tRow As Integer
    '    '-------------------------------------------------------------------------------------

    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '    Carret = Chr(13) & Chr(10)
    '    carret2 = Carret + Carret

    '    Dim FileNumber As Short
    '    FileNumber = FreeFile() + 1
    '    fs = CreateObject("Scripting.FileSystemObject")
    '    'If it already exists
    '    'UPGRADE_WARNING: Couldn't resolve default property of object fs.FileExists. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"'
    '    If fs.FileExists(Session("FdrvRun") & "statistics.dat") Then
    '        Kill(Session("FdrvRun") & "statistics.dat")
    '    End If
    '    FileOpen(FileNumber, Session("FdrvRun") & "statistics.dat", OpenMode.Append)

    '    '   Prepare to show the results
    '    '   ----------------------------
    '    db8 = Session("IFsStat_MDB") 'DAODBEngine_definst.OpenDatabase(IFsStat_MDB)
    '    SetDataTablefromAccess(db8, "Select * from TVBS_Regression", "", tbl8)
    '    'tbl8 = db8.OpenRecordset("TVBS_Regression")
    '    '    If tbl8.BOF = False Then
    '    'tbl9 = db8.OpenRecordset("Var_Names")
    '    SetDataTablefromAccess(db8, "Select * from Var_Names", "", tbl9)
    '    'tbl9.MoveFirst()
    '    'tbl8.MoveFirst()
    '    tRow = 0

    '    '   Print Variable Names
    '    '   --------------------

    '    Select Case tbl9.Rows(tRow).Item("Transformation")
    '        Case 0
    '            TransformStr = "(Log)"
    '        Case 1
    '            TransformStr = "(X^2)"
    '        Case 2
    '            TransformStr = "(X^" & Str(tbl9.Rows(tRow).Item("ValueB")) & ")"
    '        Case 3
    '            TransformStr = "(e^X)"
    '        Case 4
    '            TransformStr = "(None)"
    '        Case 5  'JAM 2009-08-26 For Logit Option
    '            TransformStr$ = "(Logit)"
    '        Case 6  'JAM 2009-12-28 For Second Grade Polynomial
    '            TransformStr$ = "(X^2_Polynomial)"
    '        Case 7  'JAM 2009-12-28 For Second Grade Polynomial
    '            TransformStr$ = "(X^3_Polynomial)"
    '    End Select

    '    PrintLine(FileNumber, TAB, "Dependent Variable = " + tbl9.Rows(tRow).Item("Var_Name") + " " + TransformStr)
    '    For jloop = 1 To Session("Total_Var")
    '        'tbl9.MoveNext()
    '        tRow = tRow + 1
    '        Select Case tbl9.Rows(tRow).Item("Transformation")
    '            Case 0
    '                TransformStr = "(LN)"
    '            Case 1
    '                TransformStr = "(X^2)"
    '            Case 2
    '                TransformStr = "(X^" & Str(tbl9.Rows(tRow).Item("ValueB")) & ")"
    '            Case 3
    '                TransformStr = "(e^X)"
    '            Case 4
    '                TransformStr = "(None)"
    '            Case 5  'JAM 2009-08-26 For Logit Option
    '                TransformStr$ = "(Logit)"
    '            Case 6  'JAM 2009-12-28 For Second Grade Polynomial
    '                TransformStr$ = "(X^2_Polynomial)"
    '            Case 7  'JAM 2009-12-28 For Second Grade Polynomial
    '                TransformStr$ = "(X^3_Polynomial)"
    '        End Select

    '        'JAM 2009-12-02     Transformation from DisplyaMenu Form
    '        If Session("transtype") <> "" Then
    '            Select Case Session("transtype")   'JAM 2009-12-01
    '                Case "LOG"
    '                    TransformStr$ = "(LN)"
    '                Case "SEC"
    '                    TransformStr$ = "(X^2)"
    '                Case "THI"
    '                    TransformStr$ = "(X^3)"
    '                Case "POW"
    '                    TransformStr$ = "(X^" + Str(Session("PowerDegree")) + ")"
    '                Case "EXP"
    '                    TransformStr$ = "(e^X)"
    '                Case "LIN"
    '                    TransformStr$ = "(None)"
    '                Case "SAT"
    '                    TransformStr$ = "(Logit)"
    '            End Select
    '            '            Else
    '            '                TransformStr$ = ""
    '        End If

    '        IndLabel = "Independent" & Trim(Str(jloop)) & " Variable = "
    '        PrintLine(FileNumber, TAB, IndLabel + tbl9.Rows(tRow).Item("Var_Name") + " " + TransformStr)
    '    Next jloop

    '    PrintLine(FileNumber, TAB, "")
    '    PrintLine(FileNumber, TAB, "")

    '    If (Session("transtype") = "SEC" Or Session("transtype") = "THI") Then  'JAM 2009-12-19 Quadratic and Third Dimension Scenarios
    '        '   Print Primary Statistics
    '        '   ------------------------
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^" & Trim(Str(jloop)))) Then
    '                PrintLine(FileNumber, TAB, "Coef_of_X" & Trim(Str(jloop)) & "^1 = " & Str(tbl8.Rows(0).Item("Coef_of_X^" & Trim(Str(jloop)))))
    '            End If
    '        Next jloop
    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "Coef_of_X1^2 = " + Str(tbl8.Rows(0).Item("Coef_of_X^2")))
    '        End If
    '        If Session("transtype") = "THI" Or TransformStr$ = "(X^3)" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "Coef_of_X1^3 = " + Str(tbl8.Rows(0).Item("Coef_of_X^3")))
    '            End If
    '        End If

    '        If IsNumeric(tbl8.Rows(0).Item("Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "Y_Intercept = " & Str(tbl8.Rows(0).Item("Y-Intercept")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("R-Square")) Then
    '            PrintLine(FileNumber, TAB, "R-Square = " & Str(tbl8.Rows(0).Item("R-Square")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("Adj_R-Square")) Then
    '            PrintLine(FileNumber, TAB, "Adj R-Square = " & Str(tbl8.Rows(0).Item("Adj_R-Square")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("F-Value")) Then
    '            PrintLine(FileNumber, TAB, "F-Value = " & Str(tbl8.Rows(0).Item("F-Value")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("Prob")) Then
    '            PrintLine(FileNumber, TAB, "Probability of zero coefs = " & Str(tbl8.Rows(0).Item("Prob")))
    '        End If

    '        PrintLine(FileNumber, TAB, "")
    '        PrintLine(FileNumber, TAB, "")

    '        '   Print Secondary Statistics
    '        '   --------------------------
    '        If IsNumeric(tbl8.Rows(0).Item("SE_of_Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept")))
    '        End If
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("SE_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "SE_of_X" & Trim(Str(jloop)) & "^1 = " &
    '                Str(tbl8.Rows(0).Item("SE_of_X^1")))
    '            End If
    '        Next jloop
    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("SE_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "SE_of_X1^2 = " +
    '                    Str(tbl8.Rows(0).Item("SE_of_X^2")))
    '        End If
    '        If Session("transtype") = "THI" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("SE_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "SE_of_X1^3 = " +
    '                    Str(tbl8.Rows(0).Item("SE_of_X^3")))
    '            End If
    '        End If

    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "Beta_of_X" & Trim(Str(jloop)) & "^1 = " &
    '                Str(tbl8.Rows(0).Item("Beta_of_X^1")))
    '            End If
    '        Next jloop
    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "Beta_of_X1^2 = " +
    '            Str(tbl8.Rows(0).Item("Beta_of_X^2")))
    '        End If


    '        If Session("transtype") = "THI" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "Beta_of_X1^3 = " +
    '                    Str(tbl8.Rows(0).Item("Beta_of_X^3")))
    '            End If
    '        End If

    '        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")))
    '        End If
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "t-Value_of_X" & Trim(Str(jloop)) & "^1 = " &
    '                Str(tbl8.Rows(0).Item("t-Value_of_X^1")))
    '            End If
    '        Next jloop
    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "t-Value_of_X1^2 = " +
    '            Str(tbl8.Rows(0).Item("t-Value_of_X^2")))
    '        End If


    '        If Session("transtype") = "THI" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "t-Value_of_X1^3 = " +
    '                    Str(tbl8.Rows(0).Item("t-Value_of_X^3")))
    '            End If
    '        End If

    '        If IsNumeric(tbl8.Rows(0).Item("Prob_of_Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "Prob_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("Prob_of_Y-Intercept")))
    '        End If
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "Prob_of_X" & Trim(Str(jloop)) & "^1 = " &
    '                Str(tbl8.Rows(0).Item("Prob_of_X^1")))
    '            End If
    '        Next jloop

    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "Prob_of_X1^2 = " +
    '            Str(tbl8.Rows(0).Item("Prob_of_X^2")))
    '        End If
    '        If Session("transtype") = "THI" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "Prob_of_X1^3 = " +
    '                   Str(tbl8.Rows(0).Item("Prob_of_X^3")))
    '            End If
    '        End If

    '        If IsNumeric(tbl8.Rows(0).Item("Multiple_R")) Then
    '            PrintLine(FileNumber, TAB, "Multiple_R = " & Str(tbl8.Rows(0).Item("Multiple_R")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("StdError_of_Estimate")) Then
    '            PrintLine(FileNumber, TAB, "StdError_of_Estimate = " & Str(tbl8.Rows(0).Item("StdError_of_Estimate")))
    '        End If

    '    Else
    '        '   Print Primary Statistics
    '        '   ------------------------
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("Coef_of_X^" + Trim(Str(jloop)))) Then
    '                PrintLine(FileNumber, TAB, "Coef_of_X" & Trim(Str(jloop)) & "^1 = " & Str(tbl8.Rows(0).Item("Coef_of_X^" & Trim(Str(jloop)))))
    '            End If
    '        Next jloop
    '        If IsNumeric(tbl8.Rows(0).Item("Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "Y_Intercept = " & Str(tbl8.Rows(0).Item("Y-Intercept")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("R-Square")) Then
    '            PrintLine(FileNumber, TAB, "R-Square = " & Str(tbl8.Rows(0).Item("R-Square")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("Adj_R-Square")) Then
    '            PrintLine(FileNumber, TAB, "Adj R-Square = " & Str(tbl8.Rows(0).Item("Adj_R-Square")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("F-Value")) Then
    '            PrintLine(FileNumber, TAB, "F-Value = " & Str(tbl8.Rows(0).Item("F-Value")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("Prob")) Then
    '            PrintLine(FileNumber, TAB, "Probability of zero coefs = " & Str(tbl8.Rows(0).Item("Prob")))
    '        End If

    '        PrintLine(FileNumber, TAB, "")
    '        PrintLine(FileNumber, TAB, "")

    '        '   Print Secondary Statistics
    '        '   --------------------------
    '        If IsNumeric(tbl8.Rows(0).Item("SE_of_Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "SE_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("SE_of_Y-Intercept")))
    '        End If
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("SE_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "SE_of_X" & Trim(Str(jloop)) & "^1=" &
    '                Str(tbl8.Rows(0).Item("SE_of_X^1")))
    '            End If
    '        Next jloop
    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("SE_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "SE_of_X1^2 = " +
    '                    Str(tbl8.Rows(0).Item("SE_of_X^2")))
    '        End If
    '        If Session("transtype") = "THI" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("SE_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "SE_of_X1^3 = " +
    '                    Str(tbl8.Rows(0).Item("SE_of_X^3")))
    '            End If
    '        End If

    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("Beta_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "Beta_of_X" & Trim(Str(jloop)) & "^1 = " &
    '                Str(tbl8.Rows(0).Item("Beta_of_X^1")))
    '            End If
    '        Next jloop

    '        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "t-Value_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("t-Value_of_Y-Intercept")))
    '        End If
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "t-Value_of_X^1=" &
    '                Str(tbl8.Rows(0).Item("t-Value_of_X^1")))
    '            End If
    '        Next jloop
    '        'JAM 2010-02-10
    '        If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^2")) Then
    '            PrintLine(FileNumber, TAB, "t-Value_of_X1^2 = " +
    '            Str(tbl8.Rows(0).Item("t-Value_of_X^2")))
    '        End If


    '        If Session("transtype") = "THI" Then
    '            'JAM 2010-02-10
    '            If IsNumeric(tbl8.Rows(0).Item("t-Value_of_X^3")) Then
    '                PrintLine(FileNumber, TAB, "t-Value_of_X1^3 = " +
    '                    Str(tbl8.Rows(0).Item("t-Value_of_X^3")))
    '            End If
    '        End If

    '        If IsNumeric(tbl8.Rows(0).Item("Prob_of_Y-Intercept")) Then
    '            PrintLine(FileNumber, TAB, "Prob_of_Y-Intercept = " & Str(tbl8.Rows(0).Item("Prob_of_Y-Intercept")))
    '        End If
    '        For jloop = 1 To Session("Total_Var")
    '            If IsNumeric(tbl8.Rows(0).Item("Prob_of_X^1")) Then
    '                PrintLine(FileNumber, TAB, "Prob_of_X^1 = " &
    '                Str(tbl8.Rows(0).Item("Prob_of_X^1")))
    '            End If
    '        Next jloop

    '        If IsNumeric(tbl8.Rows(0).Item("Multiple_R")) Then
    '            PrintLine(FileNumber, TAB, "Multiple_R = " & Str(tbl8.Rows(0).Item("Multiple_R")))
    '        End If
    '        If IsNumeric(tbl8.Rows(0).Item("StdError_of_Estimate")) Then
    '            PrintLine(FileNumber, TAB, "StdError_of_Estimate = " & Str(tbl8.Rows(0).Item("StdError_of_Estimate")))
    '        End If

    '    End If

    '    'JAM 2009-12-02 Writing to DAT File new parameters
    '    If Session("transtype") <> "" Or (tbl9.Rows(tRow).Item("Transformation") >= 0 And tbl9.Rows(tRow).Item("Transformation") <= 7) Then
    '        'vAverage,vStdDev,vCoefVar,vErrRatio
    '        vAverage = FAverage()
    '        'JAM 2010-02-10
    '        If IsNumeric(vAverage) Then
    '            PrintLine(FileNumber, TAB, "Dependent Variable Average = " +
    '                Str(vAverage))
    '        End If

    '        vStdDev = FStDev()
    '        'JAM 2010-02-10
    '        If IsNumeric(vStdDev) Then
    '            PrintLine(FileNumber, TAB, "Dependent Variable Standard Deviation = " +
    '                Str(vStdDev))
    '        End If

    '        If vAverage <> 0 And IsNumeric(vAverage) Then
    '            vCoefVar = vStdDev / vAverage
    '            'JAM 2010-02-10
    '            If IsNumeric(vCoefVar) Then
    '                PrintLine(FileNumber, TAB, "Dependent Variable Coefficient of Variation = " +
    '                    Str(vCoefVar))
    '            End If
    '        End If

    '        Dim StdError As Single
    '        StdError = tbl8.Rows(0).Item("StdError_of_Estimate")


    '        If vAverage <> 0 And IsNumeric(vAverage) Then
    '            vErrRatio = StdError / vAverage
    '            'JAM 2010-02-10
    '            If IsNumeric(vErrRatio) Then
    '                PrintLine(FileNumber, TAB, "Ratio SE to mean = " +
    '                    Str(vErrRatio))
    '            End If
    '        End If


    '    End If


    '    '    End If

    '    'tbl8.Close()
    '    'tbl9.Close()
    '    'db8.Close()

    '    FileClose(FileNumber)
    '    'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup2065"'
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.Cursor from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    'CONVERSION_ISSUE: Class System.Windows.Forms.Cursors from System.Windows.Forms is not supported.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1007.html'.
    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '    'CONVERSION_TODO: The equivalent of Method 'MsgBox()' may have a different behavior in Webforms.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1004.html'.
    '    'jrs 10/01/04 WinFormsToWeb_MessageBox.Show("File statistics.txt saved in Runfile directory", MsgBoxStyle.OKOnly, "Statistics")
    '    'JRS 10/01/04 SAVE FILE IN CLIENT *****
    '    Response.Clear()
    '    Response.ContentType = "application/octet-stream"
    '    Response.AddHeader("Content-Disposition", "attachment; filename=statistics.txt")
    '    Response.Flush()
    '    Response.WriteFile(Session("FdrvRun") & "statistics.dat")
    '    Response.End()
    '    '**************************************
    'End Sub

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
                'mnuSave_Click(sender, e)

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

    'Private Sub frm_statisticsPy_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    '    Dim i As Task(Of Integer) = InstallPython()
    'End Sub

    'Async Function InstallPython() As Task(Of Integer)
    '    Await Installer.SetupPython()
    '    Return 1
    'End Function
End Class
