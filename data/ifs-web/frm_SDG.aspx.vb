Imports VB = Microsoft.VisualBasic
Imports System.Math
Imports IFs.Data
'jam 2021/12/12
Imports Microsoft.AspNet.FriendlyUrls
Imports Microsoft.VisualBasic.Compatibility

Partial Class frm_SDG
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'jrs 2026/08/07
        If Not StandAloneMode Then
            Support.FormManager.Show(Me, "frm_PackagedDisplaysFlex.aspx")
        End If

        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

    '
    '------------------------------------------frm_SDG.Frm-------------------------------------
    '------------------------------------------------------------------------------------------
    '
    ' note for web implementation: a file is being written and read
    '
    '
    'take HistoryPlusForecast=True section off of filldata
    'take historic aggregation rule from datadict : done
    'take aggregation logic for ag_rule = GDP, SUM etc. (done for POP now) :: done
    'priviledge data for crossectional, done in country, take in group :: done
    'refresh history data on new run file :: done

    '    ReDim GR1N$(MaxVariables + 1), GR2N$(MaxVariables + 1), GR3N$(MaxVariables + 1)
    '    ReDim PLOT(MaxYears, MaxVariables)
    '    ReDim PLOTComp(MaxYears)
    '    ReDim PLOTAll(MaxYears, MaxVariables)


    Const LasYear_is_LowCommonYear$ = "YES"

    'mti July 2006 -start new variables to fix time horizon after opening hoistory plus forecast
    Dim FileNamesSDG(50) As String
    Dim FinYearSDG(50) As Integer
    Dim SDGGrpMmbrRun$(300) 'number of countries
    'mti July 2006 -end

    Dim HorizonSelection$
    Dim DisplayLastYearSDG%
    Dim DisplayEarliestYearWithData%



    'mti June 12 2005, SDG conflict with varsel
    Dim FirstYearSDG%
    Dim PlotSDG(0, 0)
    Dim SELSDG
    Dim COUNTESDG As Integer
    Dim FileNumbrSDG$(), VARNSDG$(0, 0) 'was 4
    Dim GR1NSDG$(), GR2NSDG$(), GR3NSDG$(), GR4NSDG$() 'jrs 2006/08/02 dim3
    Dim PLOTCompSDG()
    Dim PLOTAllSDG(0, 0)



    'mti June 2005, SDG Multiple Graph Data
    Dim PlotMulti(0, 0, 0)
    Dim PlotSDGHMulti(0, 0, 0)
    Dim MultiGraphsShown As Integer
    Dim MultiGraphLabels(6) As String
    Dim multiActualProgress()
    Dim multiReqdProgress()
    Dim multiProgressStr()
    Dim multiBaseVal()
    Dim multiCurval()
    Dim multiGoalVal()


    Dim sGoalDesc$, sTargetDesc$, sIndicatorDesc
    Dim sGText$, sTText$
    Dim sGIndex%, sTIndex%
    Dim GIndex As Integer

    Dim IFsLowCommonYear%

    Dim MapFormSeries$


    Dim sFirstYear%
    Dim sLastYear%
    Dim Current_Country_Group$

    Dim Current_Contry_Group_Number%

    Dim ScenarioFilesSelected$
    Dim Offset_Earliest%, Offset_SDGbase%, SDGBase2IFsBase%, SDG_Base2Target%, Forecast2Target%, SDG_Base2Current%

    Dim LatestYear%
    Dim EarliestYearWithData%
    Dim sEarliestYearWithData%
    Dim sgrpEarliestYearWithData%

    Dim TrendCalc()
    Dim BaseVal()
    Dim Curval()
    Dim GoalVal()

    Dim SDGBase2LatestYear%, OnlyYear2SDGTargetYear%, OnlyYear%
    Dim ActualProgress()
    Dim ReqdProgress()
    Dim ProgressStr()
    Dim cRunfile()
    Dim CRName()
    Dim GraphMSG$
    Dim PLOTSDGH(0, 0)
    Dim SDGHasData(0, 0)
    Dim SDGMsg()
    Dim HistoricPlusForecast As Boolean
    'jrs 2017/07/24
    Dim PLOTSDGHR(0, 0)

    Dim GoalDesc$()
    Dim Target2Goal%()
    Dim TargetName$()
    Dim TargetDesc$()
    Dim Indicator2Goal() As Integer
    Dim IndicatorDesc() As String
    Dim IndicatorName() As String
    Dim Indicator2Target() As String
    Dim Indicator_IfsVar() As String
    Dim Indicator_IfsVarDim2() As String
    Dim Indicator_IfsVarDim3() As String 'jrs 2020/06/25
    Dim Indicator_IfsVarNo() As Integer
    Dim Indicator_Transform() As String
    Dim Indicator_Var2() As String
    Dim Indicator_Var2Dim2() As String
    Dim Indicator_Var2Dim3() As String 'jrs 2020/06/25
    Dim Indicator_Unit() As String
    Dim Indicator_BaseYear() As Integer
    Dim Indicator_TargetYear() As Integer
    Dim Indicator_InterimYear() As Integer
    Dim Indicator_TargetValue() As Object
    Dim Indicator_Function() As String
    Dim Indicator_SDGHistAnalog() As String
    Dim Indicator_BubbleGraph() As String
    Dim Indicator_Min() As Double
    Dim Indicator_Max() As Double
    'jrs 2017/06/14
    Dim Indicator_TargetValueIsRelative() As Boolean
    Dim Indicator_Valence() As Boolean

    'first driver table
    Dim Indicator_DriverTbl() As String
    Dim Indicator_Conversion() As String

    'mti July 2006 -start -2nd driver table to fill up GDP2003PCPPP holes from GDP2000PCPPP
    Dim Indicator_DriverTbl1() As String
    'mti July 2006 -end    

    Dim Indicator_ConversionNumber() As Double

    'jrs 2017/02/28
    Dim Indicator_TransFunction() As String
    Dim Indicator_TransFunctionVar() As String


    Dim FlagGroup As Boolean
    Dim VPTR, i2, GMAX1, EleLoop As Integer 'SEL
    Dim Code$, SubDimDone$, SEL1$
    Dim SubDimNum% 'number of subdimensions
    Dim DimNum As Integer 'latest (and possibly 1st) subdimension number
    Dim SDimNum% 'first subdimension number where more than 1 dimension
    Dim DimPoint% 'latest (and possibly 1st) subdimension pointer (i.e. element #)
    Dim SDimPoint% 'first subdimension pointer (i.e, element #) when more than 1 dimension
    Dim GroupPoint% 'pointer to group number when regionalized variable is to be grouped
    Dim FileNum, GridFull$, SaveDim1$
    Dim SaveVCat, DefaultFile
    Dim VarPointer, Label1, Label2, Label3
    Dim OperStr$, CalcSave
    'jrs 2017/02/03
    Dim OperStr2 As String
    Dim CalcRunning As Boolean 'True means that are running a calculation such as division
    Dim CalcType As String  'Saves "None", "DivOn", "AddOn", etc.
    Dim SumOn As Boolean  'If true, are running a sum across all elements of a dimension
    Dim SeqOn, SeqRepeat
    Dim GroupOn As Boolean 'If true, are running sums across all regions in groups
    Dim GroupDone As Boolean 'If true, have finished group and ready to finalize aggregation
    Dim GroupMemb(200), WeightVarPoint(200), GroupMembCount, AgPoint, VTracker()
    Dim VarGroupNames$(100), Countries(), Regions()
    Dim grCntryBaseVal()
    Dim grCntryLYVal()

    'mti 10/27/05
    Dim regIntercept()
    Dim regSlope()
    Dim regShift()
    'mti 10/27/05

    Dim SDGBubbleSize(0, 0)
    Dim SDGBubbleData(0, 0)

    Dim agcount

    Dim Ag_On_Region, Ag_On_Group As Boolean 'If true, doing history file
    Dim RegionTot, GroupTot As Integer
    Dim TableName$, Ag_rule$, VarName$, Definition$
    'formula in history
    Dim NewVal
    Dim DivOn, MulOn, AddOn, SubOn, NumVal
    Dim DivOnPrev, MulOnPrev, AddOnPrev, SubOnPrev


    Dim bChkCurr As Boolean 'True if Currency Conversion already Checked
    Dim bCnvrtCurr As Boolean 'True if Currency Conversion is ON
    Dim CurrencyFct As Single 'Stores the Currency Factor for Conversion
    'Declares and opens a RecordSet with configuration values
    'jrs 2005/12/09 Dim rsCurr As DataTable
    'JRS 12/04/01 DISPLAY
    Dim dbList As String
    Dim rsList As DataTable
    Dim iDimPointDim1 As Integer
    'JRS 4/25/02
    Dim NeedFileWrite As Boolean
    'JRS 04/14/03
    Public bCompVisible As Boolean


    Dim GoalMax%, TargetMax%, IndicatorMax%

    Dim IIndex As Integer

    Dim SDGBaseYear%, SDGTargetYear%, GoalSetName$, GoalSetID%, GoalSetDesc$

    'jrs 2005/12/05
    ''jrs 2005/11/23
    'Dim TrickFlag As Boolean

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'jrs 2017/07/26
        If Session("EstBaseYrReg") = Nothing Then
            Session("EstBaseYrReg") = "LIN"
            Session("ShowRegVals") = False
        End If

        Dim sIIndex As Integer

        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othCountry As String
        Dim qtyPAram As Integer



        'jam 2021/12/12
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

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDG.aspx")


        If othCountry <> "" Then
            vurl = FriendlyUrl.Href("~/frm_SDG", SDGPrepareForFriendlyURL(othCountry))

            Response.Redirect(vurl)

        Else

            vurl = FriendlyUrl.Href("~/frm_SDG")

        End If


        'jam 2021/12/12
        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vCountry As String

        'jam 2021/12/12
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
            If Session("DisplayParent") = Nothing Then
                Session("DisplayParent") = "frm_MainMenu.aspx"
            End If
            FirstYearSDG = FirstYearHistory 'FirstYearForecast
            Session("FirstYearSDG") = FirstYearSDG
            Session("LastYear") = Session("LastYearForecast")

            IFsLowCommonYear% = Session("LastYear")
            Session("SDGIFsLowCommonYear") = IFsLowCommonYear%

            IIndex = 0
            sIIndex = 0

            ReDim BaseVal(200)
            ReDim Curval(200)
            ReDim GoalVal(200)
            ReDim SDGMsg(200)
            'jrs 2005/08/29
            Session("SDGMsg") = SDGMsg
            Session("SDGCurval") = Curval

            ReDim multiActualProgress(6)
            ReDim multiReqdProgress(6)
            ReDim multiProgressStr(6)

            ReDim multiBaseVal(6)
            ReDim multiCurval(6)
            ReDim multiGoalVal(6)

            Current_Country_Group$ = ""
            GoalMax = 17
            TargetMax = 79
            IndicatorMax = 306
            Session("SDGGoalMax") = GoalMax
            Session("SDGTargetMax") = TargetMax
            Session("SDGIndicatorMax") = IndicatorMax

            ReDim CRName(200)

            'mti June 12 2005, SDG conflict with varsel
            ReDim PlotSDG(MaxYears, MaxVariables)
            ReDim FileNumbrSDG$(MaxVariables), VARNSDG$(6, MaxVariables)  'was 4
            ReDim GR1NSDG$(MaxVariables + 1), GR2NSDG$(MaxVariables + 1), GR3NSDG$(MaxVariables + 1), GR4NSDG$(MaxVariables + 1) 'jrs 2006/08/02 dim3
            ReDim PLOTCompSDG(MaxYears)
            ReDim PLOTAllSDG(MaxYears, MaxVariables)

            'mti June 2005, SDG Multiple Graph Data
            ReDim PlotMulti(MaxYears, MaxVariables, 6) 'for six compare graphs
            ReDim PlotSDGHMulti(MaxYears, MaxVariables, 6)


            ReDim PLOTSDGH(MaxYears, MaxVariables)
            ReDim SDGHasData(MaxYears, MaxVariables)
            'jrs 2017/07/24
            ReDim PLOTSDGHR(MaxYears, MaxVariables)

            ReDim GoalDesc$(GoalMax)
            ReDim Target2Goal%(TargetMax)
            ReDim TargetName(TargetMax)
            ReDim TargetDesc(TargetMax)
            ReDim Indicator2Goal(IndicatorMax) 'As Integer
            ReDim IndicatorDesc(IndicatorMax) 'As String
            ReDim IndicatorName(IndicatorMax) 'As String
            ReDim Indicator2Target(IndicatorMax) 'As Integer
            ReDim Indicator_IfsVar(IndicatorMax) 'As String
            ReDim Indicator_IfsVarDim2(IndicatorMax) 'As String
            ReDim Indicator_IfsVarDim3(IndicatorMax) 'jrs 2020/06/25

            ReDim Indicator_IfsVarNo(IndicatorMax) 'As Integer
            ReDim Indicator_Transform(IndicatorMax) 'As String
            ReDim Indicator_Var2(IndicatorMax) 'As String
            ReDim Indicator_Var2Dim2(IndicatorMax) 'As String
            ReDim Indicator_Var2Dim3(IndicatorMax) 'jrs 2020/06/25
            ReDim Indicator_Unit(IndicatorMax) 'As String
            ReDim Indicator_Function(IndicatorMax) 'As String
            ReDim Indicator_TargetValue(IndicatorMax) 'As Double

            ReDim Indicator_BaseYear(IndicatorMax) 'As Integer
            ReDim Indicator_TargetYear(IndicatorMax) 'As Integer
            ReDim Indicator_InterimYear(IndicatorMax) 'As Integer
            ReDim Indicator_SDGHistAnalog(IndicatorMax) 'As String
            ReDim Indicator_BubbleGraph(IndicatorMax) 'As String
            ReDim Indicator_Min(IndicatorMax) 'As Double
            ReDim Indicator_Max(IndicatorMax) 'As Double
            'jrs 2017/06/14
            ReDim Indicator_TargetValueIsRelative(IndicatorMax)
            ReDim Indicator_Valence(IndicatorMax)


            ReDim Indicator_DriverTbl(IndicatorMax) 'As String
            'mti July 2006 -start
            ReDim Indicator_DriverTbl1(IndicatorMax) 'As String
            'mti July 2006 -end
            ReDim Indicator_Conversion(IndicatorMax) 'As String
            ReDim Indicator_ConversionNumber(IndicatorMax) 'As Double
            'jrs 2017/05/31
            'jrs 2017/02/28
            ReDim Indicator_TransFunction(IndicatorMax)
            ReDim Indicator_TransFunctionVar(IndicatorMax)


            Dim dbSDG As String

            Dim AddName$
            Dim TotGoal%
            Dim TotTarget%
            Dim SDGDataFile$
            Dim GoalBaseYear, GoalTargetYear
            Dim chk%


            ReDim PlotSDG(MaxYears, MaxVariables)

            ReDim Session("LastHistVal")(MaxVariables)
            ReDim Session("LastHistYear")(MaxVariables)





            FileNum = 0
            ScenarioFilesSelected = ""

            SDGDataFile$ = DataFileDir$ + "IFsMDG.mdb"

            cmbGoal.Items.Clear()
            cmbTarget.Items.Clear()
            cmbIndicator.Items.Clear()
            cmb_Dim1.Items.Clear()

            LoadFiles()


            TotGoal = 0  ''ll start from 2, since 1 is root or filen
            dbSDG = SDGDataFile
            GoalSetName$ = "SDG"
            SetDataTablefromAccess(dbSDG, "Select *  From Goalsets where Goalset= """ & GoalSetName$ & """", "", Session("valtbl"))
            If Session("valtbl").Rows.count > 0 Then
                SDGBaseYear% = Session("valtbl").rows(0).item("Base Year")
                SDGTargetYear% = Session("valtbl").rows(0).item("Target Year")
                GoalSetDesc = Session("valtbl").rows(0).item("Description")
                GoalSetID = Session("valtbl").rows(0).item("ID")
            End If

            SetDataTablefromAccess(dbSDG, "Select *  From Goals where GoalsetID = " + Str(GoalSetID) + " order by GoalSetID,ID", "", Session("valtbl"))
            Dim idx As Integer = 0
            While Not Session("valtbl").rows.count = idx
                TotGoal = TotGoal + 1
                cmbGoal.Items.Add(Session("valtbl").rows(idx).item("Name"))
                GoalDesc$(TotGoal) = Session("valtbl").rows(idx).item("Name") + " - " + Session("valtbl").rows(idx).item("Description")
                idx = idx + 1
            End While
            Session("SDGGoalDesc") = GoalDesc

            TotTarget = 0
            SetDataTablefromAccess(dbSDG, "Select *  From Targets where GoalsetID = " + Str(GoalSetID) + " order by GoalSetID,GoalID,ID", "", Session("valtbl"))
            idx = 0
            While Not Session("valtbl").rows.count = idx
                TotTarget = TotTarget + 1
                Target2Goal(TotTarget) = Session("valtbl").rows(idx).item("GoalID")
                TargetName(TotTarget) = Session("valtbl").rows(idx).item("Name")
                TargetDesc(TotTarget) = Session("valtbl").rows(idx).item("Name") + " - " + Session("valtbl").rows(idx).item("Description")

                idx = idx + 1
            End While
            Session("SDGTarget2Goal") = Target2Goal
            Session("SDGTargetName") = TargetName
            Session("SDGTargetDesc") = TargetDesc

            If Session("SDGIndicator2Goal") Is Nothing Then
                Dim TotIndicator As Integer
                TotIndicator = 0
                SetDataTablefromAccess(dbSDG, "Select *  From Indicators where GoalsetID = " + Str(GoalSetID) + " order by GoalSetID,GoalID,TargetID,ID", "", Session("valtbl"))
                idx = 0
                While Not Session("valtbl").rows.count = idx
                    TotIndicator = TotIndicator + 1
                    If TotIndicator = 11 Or TotIndicator = 37 Then
                        TotIndicator = TotIndicator
                    End If
                    Indicator2Goal(TotIndicator) = Session("valtbl").rows(idx).item("GoalID")
                    Indicator2Target(TotIndicator) = Session("valtbl").rows(idx).item("TargetID")
                    IndicatorName(TotIndicator) = "" & Session("valtbl").rows(idx).item("Name")
                    IndicatorDesc(TotIndicator) = "" & Session("valtbl").rows(idx).item("Name") & " - " & Session("valtbl").rows(idx).item("Description")
                    Indicator_IfsVar(TotIndicator) = "" & Session("valtbl").rows(idx).item("IFsVar")
                    Indicator_IfsVarDim2(TotIndicator) = "" & Session("valtbl").rows(idx).item("IFsVar Dim2")
                    Indicator_IfsVarDim3(TotIndicator) = "" & Session("valtbl").rows(idx).item("IFsVar Dim3") 'jrs 2020/06/25
                    Indicator_Transform(TotIndicator) = "" & Session("valtbl").rows(idx).item("Transform")


                    Indicator_Var2(TotIndicator) = "" & Session("valtbl").rows(idx).item("Var2")
                    Indicator_Var2Dim2(TotIndicator) = "" & Session("valtbl").rows(idx).item("Var2Dim2")
                    Indicator_Var2Dim3(TotIndicator) = "" & Session("valtbl").rows(idx).item("Var2Dim3") 'jrs 2020/06/25

                    Indicator_Unit(TotIndicator) = "" & Session("valtbl").rows(idx).item("Unit")
                    Indicator_Function(TotIndicator) = "" & Session("valtbl").rows(idx).item("Function Name")
                    Indicator_TargetValue(TotIndicator) = Session("valtbl").rows(idx).item("TargetValue")

                    Indicator_BaseYear(TotIndicator) = Val("" & Session("valtbl").rows(idx).item("Base Year"))
                    If Indicator_BaseYear(TotIndicator) = 0 Then
                        Indicator_BaseYear(TotIndicator) = SDGBaseYear
                    End If

                    Indicator_TargetYear(TotIndicator) = Val("" & Session("valtbl").rows(idx).item("Target Year"))
                    If Indicator_TargetYear(TotIndicator) = 0 Then
                        Indicator_TargetYear(TotIndicator) = SDGTargetYear
                    End If

                    Indicator_InterimYear(TotIndicator) = Val("" & Session("valtbl").rows(idx).item("Interim Year"))

                    Indicator_SDGHistAnalog(TotIndicator) = "" & Session("valtbl").rows(idx).item("Historic Analog")
                    Indicator_BubbleGraph(TotIndicator) = "" & Session("valtbl").rows(idx).item("Bubblegraph")

                    Indicator_Min(TotIndicator) = "" & Session("valtbl").rows(idx).item("Minimum")
                    Indicator_Max(TotIndicator) = "" & Session("valtbl").rows(idx).item("Maximum")

                    Indicator_DriverTbl(TotIndicator) = "" & Session("valtbl").rows(idx).item("DriverTbl")
                    'mti July 2006 -start
                    Indicator_DriverTbl1(TotIndicator) = "" & Session("valtbl").rows(idx).item("DriverTbl1")
                    'mti July 2006 -end
                    Indicator_Conversion(TotIndicator) = "" & Session("valtbl").rows(idx).item("Conversion")
                    Indicator_ConversionNumber(TotIndicator) = "" & Session("valtbl").rows(idx).item("ConversionNumber")

                    'jrs 2017/02/28
                    Indicator_TransFunction(TotIndicator) = "" & Session("valtbl").rows(idx).item("TransFunction")
                    Indicator_TransFunctionVar(TotIndicator) = "" & Session("valtbl").rows(idx).item("TransFunctionVar")

                    'jrs 2017/06/14
                    If Not IsDBNull(Session("valtbl").rows(idx).item("TargetValueIsRelative")) Then
                        Indicator_TargetValueIsRelative(TotIndicator) = CBool(Session("valtbl").rows(idx).item("TargetValueIsRelative"))
                    Else
                        Indicator_TargetValueIsRelative(TotIndicator) = False
                    End If
                    If Not IsDBNull(Session("valtbl").rows(idx).item("Valence")) Then
                        Indicator_Valence(TotIndicator) = CBool(Session("valtbl").rows(idx).item("Valence"))
                    Else
                        Indicator_Valence(TotIndicator) = False
                    End If

                    '        If Indicator_InterimYear(TotIndicator) = 0 Then
                    '            Indicator_InterimYear(TotIndicator) = MDInterimYear
                    '        End If


                    chk = 0
                    If Indicator_IfsVar(TotIndicator) <> "" Then
                        chk = IfsVars.Item(Indicator_IfsVar(TotIndicator)).Index
                    End If
                    If chk > 0 Then
                        Indicator_IfsVarNo(TotIndicator) = chk
                    End If
                    idx = idx + 1
                End While

                Session("SDGIndicator2Goal") = Indicator2Goal
                Session("SDGIndicator2Target") = Indicator2Target
                Session("SDGIndicatorName") = IndicatorName
                Session("SDGIndicatorDesc") = IndicatorDesc
                Session("SDGIndicator_IfsVar") = Indicator_IfsVar
                Session("SDGIndicator_IfsVarDim2") = Indicator_IfsVarDim2
                Session("SDGIndicator_IfsVarDim3") = Indicator_IfsVarDim3 'jrs 2020/06/25
                Session("SDGIndicator_Transform") = Indicator_Transform
                Session("SDGIndicator_Var2") = Indicator_Var2
                Session("SDGIndicator_Var2Dim2") = Indicator_Var2Dim2
                Session("SDGIndicator_Var2Dim3") = Indicator_Var2Dim3 'jrs 2020/06/25
                Session("SDGIndicator_Unit") = Indicator_Unit
                Session("SDGIndicator_Function") = Indicator_Function
                Session("SDGIndicator_TargetValue") = Indicator_TargetValue
                Session("SDGIndicator_BaseYear") = Indicator_BaseYear
                Session("SDGIndicator_TargetYear") = Indicator_TargetYear
                Session("SDGIndicator_InterimYear") = Indicator_InterimYear
                Session("SDGIndicator_SDGHistAnalog") = Indicator_SDGHistAnalog
                Session("SDGIndicator_BubbleGraph") = Indicator_BubbleGraph
                Session("SDGIndicator_Max") = Indicator_Max
                Session("SDGIndicator_Min") = Indicator_Min
                Session("SDGIndicator_DriverTbl") = Indicator_DriverTbl
                Session("SDGIndicator_DriverTbl1") = Indicator_DriverTbl1
                Session("SDGIndicator_Conversion") = Indicator_Conversion
                Session("SDGIndicator_ConversionNumber") = Indicator_ConversionNumber
                Session("SDGIndicator_IfsVarNo") = Indicator_IfsVarNo
                'jrs 2017/05/31
                Session("SDGIndicator_TransFunction") = Indicator_TransFunction
                Session("SDGIndicator_TransFunctionVar") = Indicator_TransFunctionVar
                'jrs 2017/06/16
                Session("SDGIndicator_TargetValueIsRelative") = Indicator_TargetValueIsRelative
                Session("SDGIndicator_Valence") = Indicator_Valence
            Else
                Indicator2Goal = Session("SDGIndicator2Goal")
                Indicator2Target = Session("SDGIndicator2Target")
                IndicatorName = Session("SDGIndicatorName")
                IndicatorDesc = Session("SDGIndicatorDesc")
                Indicator_IfsVar = Session("SDGIndicator_IfsVar")
                Indicator_IfsVarDim2 = Session("SDGIndicator_IfsVarDim2")
                Indicator_IfsVarDim3 = Session("SDGIndicator_IfsVarDim3") 'jrs 2020/06/25
                Indicator_Transform = Session("SDGIndicator_Transform")
                Indicator_Var2 = Session("SDGIndicator_Var2")
                Indicator_Var2Dim2 = Session("SDGIndicator_Var2Dim2")
                Indicator_Var2Dim3 = Session("SDGIndicator_Var2Dim3") 'jrs 2020/06/25
                Indicator_Unit = Session("SDGIndicator_Unit")
                Indicator_Function = Session("SDGIndicator_Function")
                Indicator_TargetValue = Session("SDGIndicator_TargetValue")
                Indicator_BaseYear = Session("SDGIndicator_BaseYear")
                Indicator_TargetYear = Session("SDGIndicator_TargetYear")
                Indicator_InterimYear = Session("SDGIndicator_InterimYear")
                Indicator_SDGHistAnalog = Session("SDGIndicator_SDGHistAnalog")
                Indicator_BubbleGraph = Session("SDGIndicator_BubbleGraph")
                Indicator_Max = Session("SDGIndicator_Max")
                Indicator_Min = Session("SDGIndicator_Min")
                Indicator_DriverTbl = Session("SDGIndicator_DriverTbl")
                Indicator_DriverTbl1 = Session("SDGIndicator_DriverTbl1")
                Indicator_Conversion = Session("SDGIndicator_Conversion")
                Indicator_ConversionNumber = Session("SDGIndicator_ConversionNumber")
                Indicator_IfsVarNo = Session("SDGIndicator_IfsVarNo")
                'jrs 2017/05/31
                Indicator_TransFunction = Session("SDGIndicator_TransFunction")
                Indicator_TransFunctionVar = Session("SDGIndicator_TransFunctionVar")
                'jrs 2017/06/16
                Indicator_TargetValueIsRelative = Session("SDGIndicator_TargetValueIsRelative")
                Indicator_Valence = Session("SDGIndicator_Valence")
            End If



            ClearGrid()

            '    DimLoad

            'jrs 2011/05/06
            If Session("SDGGroups") = Nothing Then
                FlagGroup = False
            Else
                FlagGroup = Session("SDGGroups")
            End If

            DimLoad()

            Current_Contry_Group_Number = 0
            cmb_Dim1.SelectedValue = Trim(cmb_Dim1.Items(0).Value)
            Current_Country_Group$ = cmb_Dim1.SelectedValue

            For i2 = 0 To cmb_Dim1.Items.Count - 1
                If UCase(Trim(cmb_Dim1.Items(i2).Value)) = "KENYA" Then
                    Current_Contry_Group_Number = i2
                    cmb_Dim1.SelectedValue = "Kenya"
                    Current_Country_Group$ = cmb_Dim1.SelectedValue
                End If
            Next

            'jrs 2005/10/24
            If Session("SDGGoalidx") = Nothing Then
                cmbGoal.SelectedIndex = 0
            Else
                cmbGoal.SelectedIndex = Session("SDGGoalidx")
            End If
            cmbGoal_SelectedIndexChanged(Me, New EventArgs)

            'mti July 29, 2005
            Dim tFontName$
            tFontName$ = "Arial"
            'jrs 2005/08/25 Proessentials
            'jrs 2005/08/29 Pego1.PeFont.MainTitle.Font = tFontName$
            'jrs 2005/08/29 Pego1.PeFont.SubTitle.Font = tFontName$
            'Pesgo1.MainTitleFont = tFontName$
            'Pesgo1.SubTitleFont = tFontName$
            'PegoMulti.MainTitleFont = tFontName$
            'PegoMulti.SubTitleFont = tFontName$
            'mti July 29, 2005


            '    Pego1.AllowPopup = False
            '    Pesgo1.AllowPopup = False

            Session("SDGHorizonVisible") = True
            Session("SDGGroups") = FlagGroup
            Session("SDGCompare") = False
            'jrs 2005/11/23
            If Session("SDGEstimates") Is Nothing Then
                Session("SDGEstimates") = FlagGroup 'jrs 2011/11/23
            End If
            'jrs 2005/11/24
            If Session("DisplayLastYear") Is Nothing Then
                Session("DisplayLastYear") = 0
            End If
            'jrs 2006/08/03
            If Session("SDGEstimatesMRandC") Is Nothing Then
                Session("SDGEstimatesMRandC") = False
            End If
            'jrs 2011/11/23
            If Session("SDGEstimateForInit") Is Nothing Then
                Session("SDGEstimateForInit") = True
            End If

            Session("SDGCurval") = Curval
            Session("SDGPLOTSDGH") = PLOTSDGH
            Session("SDGSDGHasData") = SDGHasData
            'jrs 2017/07/24
            Session("SDGPLOTSDGHR") = PLOTSDGHR

            'jrs 2017/07/26
            Session("SDGVARNSDG") = VARNSDG
            Session("SDGGR1NSDG") = GR1NSDG
            Session("SDGGR2NSDG") = GR2NSDG
            Session("SDGGR3NSDG") = GR3NSDG
            Session("SDGGR4NSDG") = GR4NSDG
            Session("SDGFileNumbrSDG") = FileNumbrSDG
            Session("SDGCOUNTESDG") = COUNTESDG

            Session("FinYearSDG") = FinYearSDG
            Session("SDGGrpMmbrRun") = SDGGrpMmbrRun

        Else
            Target2Goal = Session("SDGTarget2Goal")
            TargetName = Session("SDGTargetName")
            TargetDesc = Session("SDGTargetDesc")

            TargetMax = Session("SDGTargetMax")
            GoalMax = Session("SDGGoalMax")
            IndicatorMax = Session("SDGIndicatorMax")

            Indicator2Goal = Session("SDGIndicator2Goal")
            Indicator2Target = Session("SDGIndicator2Target")
            IndicatorName = Session("SDGIndicatorName")
            IndicatorDesc = Session("SDGIndicatorDesc")
            Indicator_IfsVar = Session("SDGIndicator_IfsVar")
            Indicator_IfsVarDim2 = Session("SDGIndicator_IfsVarDim2")
            Indicator_IfsVarDim3 = Session("SDGIndicator_IfsVarDim3") 'jrs 2020/06/25
            Indicator_Transform = Session("SDGIndicator_Transform")
            Indicator_Var2 = Session("SDGIndicator_Var2")
            Indicator_Var2Dim2 = Session("SDGIndicator_Var2Dim2")
            Indicator_Var2Dim3 = Session("SDGIndicator_Var2Dim3") 'jrs 2020/06/25
            Indicator_Unit = Session("SDGIndicator_Unit")
            Indicator_Function = Session("SDGIndicator_Function")
            Indicator_TargetValue = Session("SDGIndicator_TargetValue")
            Indicator_BaseYear = Session("SDGIndicator_BaseYear")
            Indicator_TargetYear = Session("SDGIndicator_TargetYear")
            Indicator_InterimYear = Session("SDGIndicator_InterimYear")
            Indicator_SDGHistAnalog = Session("SDGIndicator_SDGHistAnalog")
            Indicator_BubbleGraph = Session("SDGIndicator_BubbleGraph")
            Indicator_Max = Session("SDGIndicator_Max")
            Indicator_Min = Session("SDGIndicator_Min")
            Indicator_DriverTbl = Session("SDGIndicator_DriverTbl")
            Indicator_DriverTbl1 = Session("SDGIndicator_DriverTbl1")
            Indicator_Conversion = Session("SDGIndicator_Conversion")
            Indicator_ConversionNumber = Session("SDGIndicator_ConversionNumber")
            Indicator_IfsVarNo = Session("SDGIndicator_IfsVarNo")
            'jrs 2017/05/31
            Indicator_TransFunction = Session("SDGIndicator_TransFunction")
            Indicator_TransFunctionVar = Session("SDGIndicator_TransFunctionVar")
            'jrs 2017/06/16
            Indicator_TargetValueIsRelative = Session("SDGIndicator_TargetValueIsRelative")
            Indicator_Valence = Session("SDGIndicator_Valence")

            GoalDesc = Session("SDGGoalDesc")
            cRunfile = Session("SDGcRunfile")
            Curval = Session("SDGCurval")
            CRName = Session("SDGCRName")

            PlotSDG = Session("SDGPlotSDG")
            PLOTSDGH = Session("SDGPLOTSDGH")
            'jrs 2017/07/24
            PLOTSDGHR = Session("SDGPLOTSDGHR")
            SDGHasData = Session("SDGSDGHasData")
            VARNSDG = Session("SDGVARNSDG")
            GR1NSDG = Session("SDGGR1NSDG")
            GR2NSDG = Session("SDGGR2NSDG")
            GR3NSDG = Session("SDGGR3NSDG")
            GR4NSDG = Session("SDGGR4NSDG")

            FileNumbrSDG = Session("SDGFileNumbrSDG")
            IFsLowCommonYear = Session("SDGIFsLowCommonYear")
            GMAX1 = Session("SDGGMAX1")
            IIndex = Session("SDGIIndex")
            COUNTESDG = Session("SDGCOUNTESDG")
            DisplayLastYearSDG = Session("SDGDisplayLastYearSDG")
            HorizonSelection$ = Session("SDGHorizonSelection")

            Offset_SDGbase = Session("SDGOffset_SDGbase")
            SDG_Base2Target = Session("SDG_Base2Target")
            SDGBase2LatestYear = Session("SDGBase2LatestYear")
            SDGBase2IFsBase = Session("SDGBase2IFsBase")
            PlotMulti = Session("SDGPlotMulti")
            PlotSDGHMulti = Session("SDGPlotSDGHMulti")

            FlagGroup = Session("SDGGroups")

            ActualProgress = Session("SDGActualProgress")
            BaseVal = Session("SDGBaseVal")
            EarliestYearWithData = Session("SDGEarliestYearWithData")
            FirstYearSDG = Session("FirstYearSDG")
            GoalVal = Session("SDGGoalVal")
            GraphMSG = Session("SDGGraphMSG")
            LatestYear = Session("SDGLatestYear")
            SDGMsg = Session("SDGMsg")
            multiActualProgress = Session("SDGmultiActualProgress")
            multiBaseVal = Session("SDGmultiBaseVal")
            multiCurval = Session("SDGmultiCurval")
            multiGoalVal = Session("SDGmultiGoalVal")
            MultiGraphLabels = Session("SDGMultiGraphLabels")
            MultiGraphsShown = Session("SDGMultiGraphsShown")
            multiProgressStr = Session("SDGmultiProgressStr")
            multiReqdProgress = Session("SDGmultiReqdProgress")
            Offset_Earliest = Session("SDGOffset_Earliest")
            PlotMulti = Session("SDGPlotMulti")
            PlotSDGHMulti = Session("SDGPlotSDGHMulti")
            ProgressStr = Session("SDGProgressStr")
            ReqdProgress = Session("SDGReqdProgress")
            sGText = Session("sGText")
            sGIndex = Session("sGIndex")
            GIndex = Session("GIndex")
            sTText = Session("sTText")
            TableName = Session("SDGTableName")
            TrendCalc = Session("SDGTrendCalc")

            'jrs 2005/10/24
            MapFormSeries = Session("MapFormSeries")
            'jrs 2006/08/02
            FileNamesSDG = Session("FileNamesSDG")
            FinYearSDG = Session("FinYearSDG")
            SDGGrpMmbrRun = Session("SDGGrpMmbrRun")
        End If

        'Put user code to initialize the page here
        cswmWebMenu.UserData = "jsolorza::976739975"
        'jam 2021/12/12
        'cswmWebMenu.ClearPixelImage = "blank.gif"
        'cswmWebMenu.PopupIcon = "./images/Popup.gif"
        'cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        'cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        'cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        'cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        'cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"

        'jam 2021/12/12
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
        cswmGroup1.ID = "Group_4dfc"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Continue"

        If Session("DisplayParent") = "frm_SDGset.aspx" Then
            cswmItem = cswmGroup1.Items.Add()
            cswmItem.Caption = "Main Menu"
        End If

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "File"
        'cswmGroup1.ID = "Group_217b"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Export Graph"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Print Graph"
        'cswmItem.Enabled = False

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Global Picture"
        cswmGroup1.ID = "Group_f4fa"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Situation Map"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Causality"
        cswmGroup1.ID = "mnuCausality"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Causality"

        'cswmGroup1 = cswmWebMenu.Groups.Add()
        'cswmGroup1.Caption = "Graphics"
        'cswmGroup1.ID = "Group_9bf8"

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Show Title"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Color"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Monochrome"
        'cswmItem.Enabled = False

        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.Caption = "Customize"
        'cswmItem.Enabled = False

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Switch"
        cswmGroup1.ID = "mnuSwitch"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuGroups"
        If Session("SDGGroups") Then
            cswmItem.Caption = "Use Countries"
        Else
            cswmItem.Caption = "Use Groups"
        End If
        'jrs 2005/12/05
        If Session("SDGCompare") Then
            cswmItem.Enabled = False
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuCompare"
        If Session("SDGCompare") Then
            cswmItem.Caption = "Single"
        Else
            cswmItem.Caption = "Compare"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        'jrs 2006/08/03 cswmGroup1.Caption = "Time Horizon"
        cswmGroup1.Caption = "Display Options"
        cswmGroup1.ID = "mnuHorizon"
        'jrs 2005/12/05
        cswmGroup1.ShowMenuBarItem = (Not Session("SDGCompare"))

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Earliest Year to Display"
        cswmItem.Enabled = Session("SDGHorizonVisible")

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Latest Year to Display"
        cswmItem.Enabled = Session("SDGHorizonVisible")

        'jrs 2005/11/23
        'jrs 2006/0804 If Session("SDGGroups") Then
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuEstimates"
        cswmItem.Enabled = (Session("SDGGroups")) 'jrs 2006/0804 
        'jrs 2005/12/05
        cswmGroup1.ShowMenuBarItem = (Not Session("SDGCompare"))
        If Session("SDGEstimates") Then
            cswmItem.Caption = "Using estimation to fill holes in group data"
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            cswmItem.Caption = "Not filling holes in group data"
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
            Session("SDGEstimatesMRandC") = False
        End If
        'jrs 2006/0804 End If

        'jrs 2006/08/03
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuEstMRandC"
        If Session("SDGEstimatesMRandC") Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
            cswmItem.Caption = "Using closest data or cross-sectional estimation to fill holes"
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
            cswmItem.Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
            If Not Session("SDGEstimates") Or Indicator_DriverTbl(Session("SDGIIndex")) = "" Then 'jrs 2011/11/23 added Or Indicator_DriverTbl(Session("SDGIIndex")) = ""
                cswmItem.Enabled = False
            End If
        End If

        'jrs 2017/06/05
        ''jrs 2011/11/23
        'cswmItem = cswmGroup1.Items.Add()
        'cswmItem.ID = "mnuEstForInit"
        'If Session("SDGEstimateForInit") Then
        '    cswmItem.Caption = "Overriding the base year value historical line with forecast value"
        '    cswmItem.IconImage = "checkbox.gif"
        'Else
        '    cswmItem.Caption = "Using estimated historical line that may not meet forecast line"
        '    cswmItem.IconImage = "blank.gif"
        'End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuEstBaseYrReg"
        cswmItem.Caption = "Estimation with Regression"

        Dim cswmGroup2 As Coalesys.WebMenu.Group
        cswmGroup2 = cswmItem.AddGroup()

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Linear"
        cswmItem.ID = "Linear"
        If Session("EstBaseYrReg") = "LIN" Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Log"
        cswmItem.ID = "Log"
        If Session("EstBaseYrReg") = "LOG" Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Logistic"
        cswmItem.ID = "Logistic"
        If Session("EstBaseYrReg") = "LOGISTIC" Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Quadratic"
        cswmItem.ID = "Quad"
        If Session("EstBaseYrReg") = "QUAD" Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Cubic"
        cswmItem.ID = "Cubic"
        If Session("EstBaseYrReg") = "CUBIC" Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmItem = cswmGroup2.Items.Add()
        cswmItem.Caption = "Exponential"
        cswmItem.ID = "Exponential"
        If Session("EstBaseYrReg") = "EXP" Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.ID = "mnuShowRegVals"
        cswmItem.Caption = "Show Regression Values"
        If Session("ShowRegVals") Then
            'cswmItem.IconImage = "checkbox.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12
        Else
            'cswmItem.IconImage = "blank.gif"
            cswmItem.IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Edit Target"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Edit"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "About SDG"
        cswmGroup1.ID = "Group_ea00"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Goals"
        Dim sURL As String = HttpContext.Current.Request.Url.AbsoluteUri
        Dim pos As Integer = InStr(sURL, "frm_SDG")
        sURL = sURL.Substring(0, pos - 1)
        cswmItem.OnClick = "window.open('" & sURL & "SDG.htm');"
        'cswmItem.Enabled = False

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Explain"
        cswmItem.OnClick = "window.open('" & sURL & "SDGIFs.htm');"
        'cswmItem.Enabled = False

        'jrs 2005/12/05
        ''jrs 2005/11/22
        'If Session("SDGCompare") Then
        '    mnucomparelist_Click()
        'End If

        'jrs 2006/12/21
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Help"
        'jrs 2013/09/03 cswmGroup1.OnMenuBarClick = "javascript:RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16010);"
        cswmGroup1.OnMenuBarClick = LoadHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, "")

        'jam 2021/12/12
        'jrs 2011/05/06
        'If Not IsNothing(Request.QueryString("Country")) Then
        If (Not IsNothing(Request.QueryString("Country")) Or Not IsNothing(Session("vCountry"))) And Not Session("SDGGroups") Then
            'jam 2021/12/12
            'If Session("MMCountry") <> Request.QueryString("Country") Or Not Page.IsPostBack Then
            If Session("MMCountry") <> Session("vCountry") Or Not Page.IsPostBack Then
                'jam 2021/12/12
                'Session("MMCountry") = Request.QueryString("Country")
                Session("MMCountry") = Session("vCountry")
                'jrs 2012/08/15 Session("SDGGroups") = False
                If Session("SDGGroups") = True Then
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Enabled = False
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Caption = "Not filling holes in group data"
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 '"blank.gif"

                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = False
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                    Session("SDGEstimatesMRandC") = False
                    'jrs 2014/04/14
                    cswmWebMenu.Groups("mnuSwitch").Items("mnuGroups").Caption = "Use Groups"

                    Session("SDGGroups") = False
                    FlagGroup = False
                    DimLoad()
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
        ElseIf (Not IsNothing(Request.QueryString("Group")) Or Not IsNothing(Session("vCountry"))) And Session("SDGGroups") Then 'jrs 2021/12/12
            'jam 2021/12/12
            'If cmb_Dim1.SelectedValue <> Request.QueryString("Group") Then
            If cmb_Dim1.SelectedValue <> Session("vCountry") Then
                Session.Remove("cNumber")
                Dim gIdx As Integer
                'jam 2021/12/12
                'gIdx = gGetGroupIdx(Request.QueryString("Group"), False, Me)
                gIdx = gGetGroupIdx(Session("vCountry"), False, Me)
                If gIdx > 0 Then
                    cmb_Dim1.SelectedIndex = gIdx - 1
                    Session("SDGGroupIdx") = cmb_Dim1.SelectedIndex
                    'jam 2021/12/12
                    'Session("MMGroup") = Request.QueryString("Group")
                    Session("MMGroup") = Session("vCountry")
                Else
                    Session.Remove("MMGroup")
                End If
                If Session("SDGGroups") = True Then
                    cmb_Dim1_SelectedIndexChanged(cmb_Dim1, New System.EventArgs)
                Else
                    mnuGroupToggle_Click()
                End If
            End If
        End If

        'KIA 63008 ADD THE FOLLOWING IF BLOCK
        If Not Session("cNumber") Is Nothing Then

            cmb_Dim1.Visible = True
            cmb_Dim1.SelectedIndex = CInt(Session("cNumber")) - 1
            Session.Remove("cNumber")
            cmb_Dim1_SelectedIndexChanged(Me, New System.EventArgs)
        End If
        'jrs 2014/04/14
        If lstcountries.SelectedIndex = -1 Then
            If cmb_Dim1.SelectedIndex > 0 Then
                lstcountries.SelectedIndex = 0
            Else
                lstcountries.SelectedIndex = 1
            End If
        End If

        PackJScritpBuilder()
    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Continue"
                Session("COUNTE") = 0

                'jam 2023/10/02
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/" & Session("DisplayParent"))

                vurl = FriendlyUrl.Href("~/" & Session("DisplayParent"))

                Response.Redirect(vurl)



                    'Support.FormManager.Show(Me, Session("DisplayParent"))
            Case "Main Menu"
                Session("COUNTE") = 0

                'jam 2023/10/02
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/" & "frm_MainMenu.aspx")

                vurl = FriendlyUrl.Href("~/" & "frm_MainMenu.aspx")

                Response.Redirect(vurl)

                    'Support.FormManager.Show(Me, "frm_MainMenu.aspx")
            Case "Situation Map"
                SituationMap()
            Case "Earliest Year to Display"
                mnuHorizonEarliest_Click()
            Case "Latest Year to Display"
                mnuHorizonLatest_Click()
            Case "Use Groups", "Use Countries"
                mnuGroupToggle_Click()
            Case "Compare", "Single"
                mnucomparelist_Click()
            Case "Using estimation to fill holes in group data", "Not filling holes in group data" '"Use Estimates for Group History" 'jrs 2005/11/23
                mnuUseEstimates_Click()
            Case "Causality"
                mnuCausality_Click()
                    'Case "Goals"
                    '    Session("SDGAboutFile") = DataFileDir + "SDG.htm"
                    '    Support.FormManager.StateStore(Me)
                    '    Support.FormManager.Show(Me, "AboutSDG.aspx")
                    'Case "Explain"
                    '    Session("SDGAboutFile") = DataFileDir + "SDGIFs.htm"
                    '    Support.FormManager.StateStore(Me)
                    '    Support.FormManager.Show(Me, "AboutSDG.aspx")
            Case "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes", "Using closest data or cross-sectional estimation to fill holes" '"Use most recent and closest estimation in all possible cases" 'jrs 2006/08/03
                mnuEstMRandC_Click()
            Case "Overriding the base year value historical line with forecast value", "Using estimated historical line that may not meet forecast line"
                mnuEstForInit_Click()
            Case "Edit"
                If Session("TargetisRelative") Is Nothing Then
                    LoadTargets()
                End If
                'jrs 2017/09/07
                If Indicator_IfsVar(Session("SDGiIndex")) > "" Then
                    Session("rowidx") = FindRowidx(Session("SDGiIndex"))
                Else
                    Session("rowidx") = FindRowidxH(Session("SDGiIndex"))
                End If
                If Session("SDGGroups") Then
                    Session("SDGEditTargetParent") = "frm_SDG.aspx?Group=" & cmb_Dim1.SelectedValue
                Else
                    If Session("MMCountry") = Nothing Then
                        Session("MMCountry") = IFsRegs.Item(cmb_Dim1.SelectedValue).ISO3166_2Code
                    End If
                    Session("SDGEditTargetParent") = "frm_SDG.aspx?Country=" & Session("MMCountry")
                End If
                'jam 2021/12/12
                'Support.FormManager.Show(Me, "frm_SDGEditTarget.aspx")

                'jam 2021/12/12
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDGEditTarget.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDGEditTarget.aspx")

                Response.Redirect(vurl)


            Case "Linear" 'jrs 2017/07/26
                Session("EstBaseYrReg") = "LIN"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Log").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Quad").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Cubic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If

            Case "Log" 'jrs 2017/07/26
                Session("EstBaseYrReg") = "LOG"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Log").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Quad").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Cubic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If

            Case "Logistic" 'jrs 2017/07/31
                Session("EstBaseYrReg") = "LOGISTIC"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Log").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Quad").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Cubic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"

                Session("ScatterLogistic") = "frm_SDG.aspx"
                Support.FormManager.StateStore(Me)
                'jam 2021/12/12
                'Support.FormManager.Show(Me, "frm_SaturParam.aspx")

                'jam 2021/12/12
                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SaturParam.aspx")


                vurl = FriendlyUrl.Href("~/frm_SaturParam.aspx")

                Response.Redirect(vurl)


                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If

            Case "Quadratic" 'jrs 2017/07/26
                Session("EstBaseYrReg") = "QUAD"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Log").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Quad").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Cubic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If

            Case "Cubic" 'jrs 2017/07/26
                Session("EstBaseYrReg") = "CUBIC"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Log").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Quad").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Cubic").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If

            Case "Exponential" 'jrs 2017/07/26
                Session("EstBaseYrReg") = "EXP"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Linear").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Log").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Logistic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Quad").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Cubic").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                cswmWebMenu.Groups("mnuHorizon").Items("mnuEstBaseYrReg").Group.Items("Exponential").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If

            Case "Show Regression Values"
                Session("ShowRegVals") = Not Session("ShowRegVals")
                If Session("ShowRegVals") Then
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuShowRegVals").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
                Else
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuShowRegVals").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
                End If
                If Not Session("SDGCompare") Then  'on single graph
                    RefreshForm()
                Else
                    RefreshFrameMultiple()
                End If
        End Select
    End Sub

    Private Function FindRowidx(idx As Integer) As Integer
        Dim i As Integer
        For i = 0 To UBound(Session("sName"))
            If Session("sName")(i) = Session("SDGIndicatorName")(idx) Then
                FindRowidx = i
                Exit For
            End If
        Next
    End Function

    'jrs 2017/09/07
    Private Function FindRowidxH(idx As Integer) As Integer
        Dim i As Integer
        For i = 0 To UBound(Session("sNameH"))
            If Session("sNameH")(i) = Session("SDGIndicatorName")(idx) Then
                FindRowidxH = i
                Exit For
            End If
        Next
    End Function

    Private Sub mnuCausality_Click()
        'jam 2026/01/14 Changing to friendly URL
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session("DriverParent") = "frm_SDG.aspx"
        'Support.FormManager.Show(Me, "frm_Drivers.aspx?vName=" & Indicator_IfsVar(IIndex))

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Drivers")


        vurl = FriendlyUrl.Href("~/frm_Drivers", Indicator_IfsVar(IIndex))

        Response.Redirect(vurl)

    End Sub

    Private Sub LoadFiles()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim fs 'As New FileSystemObject
        Dim BASE 'As file
        Dim currRun 'As file
        fs = CreateObject("Scripting.FileSystemObject")
        Dim sWorkingFile As String
        sWorkingFile = Right(Session("WorkingFile"), Len(Session("WorkingFile")) - InStrRev(Session("WorkingFile"), "\"))

        'mti June 2005
        'JRS 05/20/03
        '    If FirstYear% = FirstYearHistory% And LastYear% - FirstYear% < 100 Then
        '        Set BASE = fs.GetFile(PutFdrvRun2$ + "\IFsBaseHist.run")
        '    Else
        'cdv 2021/03/17 Fix File not found error when SQLite is false
        If Session("mnuSQLite") = "True" Then
            BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run.db")
        Else
            BASE = fs.GetFile(Session("PutFdrvRun2") + "\IFsBase.run")
        End If
        '    End If

        lstFiles.Items.Add("0 - " + "Working File, based on " + sWorkingFile)
        lstFiles.Items.Add("1 - " + BASE.Name)
        Session("FilesSelected") = 1

        'mti July 2006 start
        FileNamesSDG(0) = Session("WorkingFile")
        If Session("mnuSQLite") = "True" Then
            FileNamesSDG(1) = Session("PutFdrvRun2") + "\IFsBase.run.db"
        Else
            FileNamesSDG(1) = Session("PutFdrvRun2") + "\IFsBase.run"
        End If
        Dim SDGFCnt As Integer = 1
        'Mti July 2006 End

        Dim sRunFile As String
        ' Returns filename with specified extension. If more than one *.ini
        ' file exists, the first file found is returned.
        sRunFile = Dir(Session("PutFdrvRun2") + "\*.run.db")

        Do While (sRunFile <> "")
            If UCase(sRunFile) <> UCase(BASE.Name) And
            UCase(sRunFile) <> UCase(sWorkingFile) And
            UCase(sRunFile) <> "WORKING.RUN.DB" Then 'JRS 11/08/02
                currRun = fs.GetFile(Session("PutFdrvRun2") + "\" + sRunFile)
                'jrs 2022/04/08 added Or StandAloneMode
                'jrs 2017/06/29
                If Session("IFsRunFiles").item(sRunFile).Visible Or StandAloneMode Then
                    lstFiles.Items.Add(Trim(Str(lstFiles.Items.Count)) + " - " + sRunFile)
                    Session("FilesSelected") = Session("FilesSelected") + 1
                    ReDim Preserve Session("RunFiles")(Session("FilesSelected"))
                    Session("RunFiles")(Session("FilesSelected")) = Session("PutFdrvRun2") + "\" + sRunFile
                    'mti July 2006 start
                    SDGFCnt = SDGFCnt + 1
                    'jrs 2012/07/02
                    If SDGFCnt > UBound(FileNamesSDG) Then
                        ReDim Preserve FileNamesSDG(SDGFCnt)
                    End If
                    FileNamesSDG(SDGFCnt) = Session("PutFdrvRun2") + "\" + sRunFile
                    'Mti July 2006 End
                End If
            End If
            ' Call Dir again without arguments to return the next *.INI file in the
            ' same directory.
            sRunFile = Dir()
        Loop

        ReDim cRunfile(lstFiles.Items.Count * NR) 'holds filenumber for individual counte

        'jrs 2026/01/26 changed Default to Base
        lstFiles.SelectedIndex = 1
        If Session("SDGSetFileIdx") Is Nothing Then
            lstFiles.Items(1).Selected = True
        Else
            lstFiles.SelectedIndex = Session("SDGSetFileIdx")
        End If
        If Not (Session("SDGSetFileIdx2") Is Nothing) Then
            lstFiles.Items(Session("SDGSetFileIdx2")).Selected = True
        End If


        Session("FileNamesSDG") = FileNamesSDG

    End Sub

    Private Sub ClearGrid()
        '   Grid Setup
        '   ----------

        SDimPoint% = 0
        DimPoint% = 0


        ClearPlot()

        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow
        Dim ce1 As New TableCell, ce2 As New TableCell, ce3 As New TableCell, ce4 As New TableCell
        Dim c1 As Integer

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

        For c1 = 1 To MaxVariables
            Dim cet1 As New TableCell, cet2 As New TableCell, cet3 As New TableCell, cet4 As New TableCell

            cet1.Text = ""
            cet1.BackColor = System.Drawing.Color.Gray
            r1.Cells.Add(cet1)
            cet2.Text = ""
            cet2.BackColor = System.Drawing.Color.Gray
            r2.Cells.Add(cet2)
            cet3.Text = ""
            cet3.BackColor = System.Drawing.Color.Gray
            r3.Cells.Add(cet3)
            cet4.Text = ""
            cet4.BackColor = System.Drawing.Color.Gray
            r4.Cells.Add(cet4)

        Next c1

        grd_VariableDisplay.Rows.Clear()
        grd_VariableDisplay.Rows.Add(r1)
        grd_VariableDisplay.Rows.Add(r2)
        grd_VariableDisplay.Rows.Add(r3)
        grd_VariableDisplay.Rows.Add(r4)
    End Sub

    Sub ClearPlot()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        For i2 = 1 To lstFiles.Items.Count
            cRunfile(i2) = ""
        Next

        For i2 = 1 To 200
            BaseVal(i2) = Nothing
            Curval(i2) = Nothing
            SDGMsg(i2) = ""
            CRName(i2) = Nothing
            'jrs 2017/07/12
            GoalVal(i2) = Nothing
        Next

        'jrs 2017/07/12
        For i2 = 1 To 6
            multiGoalVal(i2) = Nothing
        Next i2

        COUNTESDG = 0
        Session("COUNTE") = COUNTESDG
        Dim ICount As Integer, iloop As Integer
        For i2 = 1 To Session("LastYear") - FirstYearSDG% + 1
            For ICount% = 1 To MaxVariables
                PlotSDG(i2, ICount%) = Nothing
                PLOTSDGH(i2, ICount%) = Nothing
                SDGHasData(i2, ICount%) = Nothing
                'jrs 2017/07/24
                PLOTSDGHR(i2, ICount%) = Nothing
            Next ICount%
        Next i2
        For iloop% = 1 To MaxVariables
            VARNSDG$(1, iloop%) = ""
            VARNSDG$(2, iloop%) = ""
            VARNSDG$(3, iloop%) = ""  'Help ID code
            VARNSDG$(6, iloop%) = ""  'Help ID Eqn code
            'mti Validation changes July 28, 2002
            VARNSDG$(4, iloop%) = ""  'Validation Marker
            'mti Validation changes July 28, 2002
            'mti Validation changes 19 August, 2002
            VARNSDG$(5, iloop%) = ""
            'mti Validation changes 19 August, 2002

            GR1NSDG$(iloop%) = ""
            GR2NSDG$(iloop%) = ""
            GR3NSDG$(iloop%) = ""
        Next iloop%

        Session("SDGCOUNTESDG") = COUNTESDG
        ReDim Session("LastHistVal")(MaxVariables)
        ReDim Session("LastHistYear")(MaxVariables)
    End Sub

    Private Sub DimLoad()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        '       Make Combo Box and Label Visible for Subdimension(s)
        '       ----------------------------------------------------
        '        frm_dim1.Visible = True
        '        frm_dim1.ZOrder 0
        '        frm_varname.Visible = False
        cmb_Dim1.Items.Clear()
        '        cmb_Dim1.SetFocus

        lstcountries.Items.Clear()

        '       Load element names into Combo Box
        '       ---------------------------------
        DimNum = 1
        If FlagGroup = True And DimNum = 1 Then  'Set up for groups
            'why not take from groupnames$() -mti
            SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & GroupIDNew$ & " ORDER BY GroupName", "GroupName", Session("valtbl"))
            Dim idx As Integer = 0
            While Not Session("valtbl").rows.count = idx
                '                'JRS 07/29/02 GUIDEDUSE BEGIN
                '                If Guided_use And UCase(Select_name) = UCase(valtbl.Fields("GroupName")) Then
                '                    cmb_Dim1.Text = Select_name
                '                End If
                '                'JRS 07/29/02 GUIDEDUSE END
                cmb_Dim1.Items.Add(Session("valtbl").rows(idx).item("GroupName"))
                lstcountries.Items.Add(Session("valtbl").rows(idx).item("GroupName"))

                'jrs 2017/06/05
                If Session("SDGGroupIdx") = Nothing Then
                    If Session("valtbl").rows(idx).item("GroupName") = "World" Then
                        Session("SDGGroupIdx") = idx
                    End If
                End If
                idx = idx + 1
            End While

            'no ALL for SDG for now
            'cmb_Dim1.AddItem "ALL"
        Else  'Proceed with regions or secondary dimensions
            'no ALL for SDG for now, so - 1
            For EleLoop = 1 To MaxVal(DimNum) - 1
                'JRS 07/29/02 GUIDEDUSE BEGIN
                If Guided_use And UCase(Select_name) = UCase(GRPS$(DimNum, EleLoop)) Then
                    cmb_Dim1.SelectedValue = Select_name
                End If
                'JRS 07/29/02 GUIDEDUSE END
                cmb_Dim1.Items.Add(GRPS$(DimNum, EleLoop))
                lstcountries.Items.Add(GRPS$(DimNum, EleLoop))
            Next EleLoop
        End If

    End Sub

    Private Sub cmbGoal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGoal.SelectedIndexChanged
        'jrs 2005/10/24
        Session("SDGGoalidx") = cmbGoal.SelectedIndex

        Dim AddName$
        Dim PosDash%
        Dim TargetFound As Boolean

        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'Dim sGoalDesc$, sTargetDesc$, sIndicatorDesc, sTargetText$
        Dim TargetIdx As Integer = 0

        GIndex = cmbGoal.SelectedIndex + 1

        For i2 = 1 To TargetMax
            If Target2Goal(i2) = GIndex Then
                TargetFound = True
                Exit For
                '            lblDescribe.Caption = lblDescribe.Caption + Chr(13) + TargetDesc(i2)
            End If
        Next

        If Not TargetFound Then
            Dim Msg As String
            Msg = "None of the indicators associated with " + cmbGoal.SelectedValue + " could be matched with any IFs variable."
            LoadMessage(Msg, Session("Message"))

            'cmbGoal.SelectedValue = sGText
            'cmbTarget.SelectedValue = sTText
            sGText = cmbGoal.Items(0).Text
            sTText = TargetName(1)
            cmbGoal.SelectedValue = sGText
            cmbGoal_SelectedIndexChanged(Me, New EventArgs)
            cmbTarget.SelectedValue = sTText

            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")

            'jam 2021/12/12
            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


            vurl = FriendlyUrl.Href("~/frm_Message.aspx")

            Response.Redirect(vurl)

        End If


        '    sGoalDesc = lblGoal.Caption
        '    sTargetDesc = lblTarget.Caption
        '    sIndicatorDesc = lblIndicator.Caption
        '    sTargetText = cmbTarget.Text

        '    lblGoal.Caption = GoalDesc(GIndex)
        '    lblTarget.Caption = ""
        '    lblIndicator.Caption = ""


        cmbTarget.Items.Clear()
        '    lblDescribe.Caption = ""

        For i2 = 1 To TargetMax
            If Target2Goal(i2) = GIndex Then
                AddName = TargetName(i2)
                cmbTarget.Items.Add(AddName)

                If Not (Session("SDGsetTargetName") = Nothing) Then
                    If AddName = Session("SDGsetTargetName") Then
                        TargetIdx = cmbTarget.Items.Count - 1
                    End If
                End If
                '            lblDescribe.Caption = lblDescribe.Caption + Chr(13) + TargetDesc(i2)
            End If
        Next

        '    lblDescribe.Visible = True
        '    Pego1.Visible = False
        '    Pesgo1.Visible = False
        '    grd_TableDisplay.Visible = False


        '    If cmbTarget.ListCount > 0 Then

        cmbTarget.SelectedIndex = TargetIdx
        cmbTarget_SelectedIndexChanged(Me, New EventArgs)
        sGText = cmbGoal.SelectedValue
        sGIndex = GIndex
        Session("sGText") = sGText
        Session("sGIndex") = sGIndex
        Session("GIndex") = GIndex

        '    Else
        '        MsgBox "None of the targets associated with this goal could be matched with any IFs variable."
        '        cmbGoal.Text = sGText
        '        lblGoal.Caption = sGoalDesc
        '        lblTarget.Caption = sTargetDesc
        '        lblIndicator.Caption = sIndicatorDesc
        '        GIndex = sGIndex
        '        For i2 = 1 To TargetMax
        '            If Target2Goal(i2) = GIndex Then
        '                AddName = TargetName(i2)
        '                cmbTarget.AddItem AddName
        '            End If
        '        Next
        '        cmbTarget.Text = sTargetText
        '
        '    End If

    End Sub

    Private Sub cmbTarget_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTarget.SelectedIndexChanged
        Dim AddName$
        Dim PosDash%
        Dim chk%
        Dim IndFound As Boolean
        Dim Tindex As Integer
        Dim sTIndex As String = ""
        Dim pos As Integer
        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Dim sGoalDesc$, sTargetDesc$, sIndicatorDesc, sIndicatorText$

        Dim IndicatorIdx As Integer = 0

        '    GIndex = cmbGoal.ListIndex + 1

        Tindex = FindIndex(cmbTarget, TargetName)
        If Tindex > 0 Then
            pos = InStr(TargetName(Tindex), " ")
            sTIndex = Mid(TargetName(Tindex), pos + 1)
        End If

        For i2 = 1 To IndicatorMax
            If Indicator2Target(i2) = sTIndex Then
                IndFound = True
                Exit For
            End If
        Next

        If Not IndFound Then
            Dim Msg As String
            Msg = "None of the indicators associated with " + cmbGoal.SelectedValue + " could be matched with any IFs variable."

            'sGText = cmbGoal.Items(0).Text
            'sTText = TargetName(1)
            'cmbGoal.SelectedValue = sGText
            'cmbGoal_SelectedIndexChanged(Me, New EventArgs)
            'cmbTarget.SelectedValue = sTText

            LoadMessage(Msg, Session("Message"))
            'Support.FormManager.StateStore(Me)
            'Support.FormManager.Show(Me, "frm_Message.aspx")

            'jam 2021/12/12
            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


            vurl = FriendlyUrl.Href("~/frm_Message.aspx")

            Response.Redirect(vurl)

        End If


        '    sGoalDesc = lblGoal.Caption
        '    sTargetDesc = lblTarget.Caption
        '    sIndicatorDesc = lblIndicator.Caption
        sIndicatorText = cmbIndicator.SelectedValue

        lblTarget.Text = ""
        lblIndicator.Text = ""

        '    Tindex = FindIndex(cmbTarget, TargetName())
        lblGoal.Text = GoalDesc(GIndex)
        lblTarget.Text = TargetDesc(Tindex)
        Session("SDGsetTargetName") = cmbTarget.SelectedValue

        cmbIndicator.Items.Clear()
        For i2 = 1 To IndicatorMax
            If Indicator2Target(i2) = sTIndex Then
                'AddName = VarName(Indicator_IfsVarNo())
                cmbIndicator.Items.Add(IndicatorName(i2))
                If Not (Session("SDGsetIndicatorName") = Nothing) Then
                    If Session("SDGsetIndicatorName") = IndicatorName(i2) Then
                        IndicatorIdx = cmbIndicator.Items.Count - 1
                    End If
                End If
            End If
        Next

        '    If cmbIndicator.ListCount > 0 Then
        cmbIndicator.SelectedIndex = IndicatorIdx
        cmbIndicator_SelectedIndexChanged(Me, New EventArgs)
        sTText = cmbTarget.SelectedValue
        Session("sTText") = sTText
        '    Else
        '
        '        MsgBox "None of the indicators associated with this target could be matched with any IFs variable."
        '
        '        cmbTarget.Text = sTText
        '        lblGoal.Caption = sGText
        '        lblTarget.Caption = sTargetDesc
        '        lblIndicator.Caption = sIndicatorDesc
        '
        '        Tindex = FindIndex(cmbTarget, TargetName())
        '        lblTarget.Caption = TargetDesc(Tindex)
        '        cmbIndicator.Clear
        '        For i2 = 1 To 50
        '            If Indicator2Target(i2) = Tindex Then
        '                'AddName = VarName(Indicator_IfsVarNo())
        '                cmbIndicator.AddItem IndicatorName(i2)
        '            End If
        '        Next
        '
        '        cmbIndicator.Text = sIndicatorText
        '    End If
    End Sub

    Private Function FindIndex(ByVal cmb As DropDownList, ByVal Arr() As String) As Integer
        Dim Cnt%
        FindIndex = 0
        'X = UBound(Arr)

        For Cnt = 1 To UBound(Arr) 'cmb.ListCount
            If Arr(Cnt) = cmb.SelectedValue Then 'cmb.List(Cnt - 1) Then
                FindIndex = Cnt
                Exit For
            End If
        Next
    End Function

    Private Sub cmbIndicator_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbIndicator.SelectedIndexChanged

        Dim sIIndex As Integer
        Dim sRegIndex As Integer

        'IIndex = cmbIndicator.ListIndex + 1
        sIIndex = IIndex

        IIndex = FindIndex(cmbIndicator, IndicatorName)
        '    For i2 = 1 To cmbIndicator.ListCount '20
        '        If IndicatorName() = cmbIndicator.List(i2 - 1) Then
        '            IIndex = i2
        '        End If
        '    Next
        Session("SDGIIndex") = IIndex

        lblIndicator.Text = IndicatorDesc(IIndex)


        If sIIndex <> IIndex Then

            'mti commented March 26, 2005
            'mnuGroupToggle.Caption = "Use Groups"

            ''        FlagGroup = False
            ''        DimLoad
            ''        Option1(2).Visible = False
            ''        cmb_DIM1.ListIndex = 11

            '        sRegIndex = cmb_Dim1.ListIndex
            '        cmb_Dim1.ListIndex = 9 'bangladeshd
            '        If sRegIndex = cmb_Dim1.ListIndex Then
            ''            Current_Country_Group$ = ""
            ''            cmb_DIM1_Click
            '        End If

            'jrs 2008/10/28
            'mti July 2006 -start - enable/disable new estimation option (Most recent/closest)
            'If IndicatorName(IIndex) = "Indicator 1" Or IndicatorName(IIndex) = "Indicator 1a" Then
            '    Session("SDGEstimatesMRandC") = True
            '    If cswmWebMenu.Groups.Count > 0 Then
            '        cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = "checkbox.gif"
            '    End If
            'Else
            '    Session("SDGEstimatesMRandC") = False
            '    If cswmWebMenu.Groups.Count > 0 Then
            '        cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = "blank.gif"
            '    End If
            'End If

            'jrs 2009/02/09 added or not UseEstimates
            If Indicator_DriverTbl(IIndex) = "" Or Not Session("SDGEstimates") Then
                If cswmWebMenu.Groups.Count > 0 Then
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = False
                End If
                'Session("SDGEstimatesMRandC") = False
                'If cswmWebMenu.Groups.Count > 0 Then
                '    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = "blank.gif"
                '    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
                'End If
            Else
                If cswmWebMenu.Groups.Count > 0 Then
                    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = True
                End If
                'Session("SDGEstimatesMRandC") = True
                'If cswmWebMenu.Groups.Count > 0 Then
                '    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = "checkbox.gif"
                '    cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using closest data or cross-sectional estimation to fill holes"
                'End If
            End If
            Session("SDGsetIndicatorName") = cmbIndicator.SelectedValue
            'mti July 2006 -end

            RefreshForm()

            'Current_Country_Group$ = cmb_DIM1.Text

            sGoalDesc = lblGoal.Text
            sTargetDesc = lblTarget.Text
            sIndicatorDesc = lblIndicator.Text
        End If

    End Sub

    Private Sub RefreshForm()
        Dim VarName$, VarNO%, CountryOrGroup$, SecondDim$, ThirdDim 'jrs 2006/05/17 Dim3

        ClearGrid()


        VarName$ = Indicator_IfsVar(IIndex)
        'jrs 2017/08/28
        If Not FlagGroup Then
            Session("SDGbackToRegIdx") = cmb_Dim1.SelectedIndex
            Session("MMCountry") = IFsRegs.Item(cmb_Dim1.SelectedValue).ISO3166_2Code
        End If

        If VarName$ = "" Then
            'no IFsVar equivalent of SDG Indicator- History Only

            Dim Region As String
            Region = cmb_Dim1.SelectedValue
            '    If Region$ = "" Then Region$ = Sel2$
            COUNTESDG = 1
            Session("COUNTE") = COUNTESDG
            CRName(COUNTESDG) = Region$
            Dim sVar As String
            TableName$ = "Series" + Trim(Indicator_SDGHistAnalog(IIndex))
            sVar = Trim(Indicator_SDGHistAnalog(IIndex))

            '   Check Validity of Group or Country
            '   ----------------------------------

            If Not FlagGroup Then
                DimPoint = gGetDimIdx(1, cmb_Dim1.SelectedValue, True, Me)
            Else
                GroupPoint = gGetGroupIdx(cmb_Dim1.SelectedValue, True, Me)
            End If

            CRName(COUNTESDG) = Region$
            Session("SDGCRName") = CRName
            Session("SDGCOUNTESDG") = COUNTESDG

            '    If Trim(Indicator_SDGHistAnalog(IIndex)) = "" Then
            '        TableName$ = "Series" + VHistoricAnalog(SEL)
            '    Else
            Session("SDGTableName") = TableName
            'End If
            '    FirstYear = FirstYearHistory
            FirstYearSDG = FirstYearHistory
            Session("FirstYearSDG") = FirstYearSDG
            Session("LastYear") = LastYearHistory
            Call History_Filldata()

            '    FirstYear = FirstYearForecast
            Session("LastYear") = Session("LastYearForecast")
            DrawSDGgraph()

            'if calc in history
            'check aggregation; take agrule from datadict

        Else
            Dim chk As Integer
            Select Case Indicator_Transform(IIndex)
                Case "Division"
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        'check the validity of variable 2 in ifsvar
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "/" + Indicator_Var2(IIndex)
                        End If

                    End If

                Case "Percent"
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "%" + Indicator_Var2(IIndex)
                        End If

                    End If

                Case "Absolute" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "*" + Indicator_Var2(IIndex) + "/100"
                        End If

                    End If
                Case "Per1000" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "/" + Indicator_Var2(IIndex) + "*1000"
                        End If
                    End If
                Case "InvCDALF" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "*(1-CDALF)+" + Indicator_Var2(IIndex)
                        End If
                    End If
                Case "Sum" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "+" + Indicator_Var2(IIndex)
                        End If
                    End If
                Case "Sub" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "-" + Indicator_Var2(IIndex)
                        End If
                    End If
                Case "LossIndex" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "+" + Indicator_Var2(IIndex)
                        End If
                    End If

                Case Else
            End Select
            SecondDim$ = Trim(Indicator_IfsVarDim2(IIndex)) '""
            VarNO = Indicator_IfsVarNo(IIndex)  'do not need it
            'jrs 2020/06/25
            ThirdDim = Trim(Indicator_IfsVarDim3(IIndex))

            'ClearGrid

            If InputSDGForecast(VarName, cmb_Dim1.SelectedValue, SecondDim$, ThirdDim) Then

                DrawSDGgraph()
                'lblDescribe.Visible = False
            End If

        End If

        If GraphMSG$ <> "" Then  'there is some message, e.g., no data
            MsgBox(GraphMSG$)

            cmb_Dim1.SelectedValue = Current_Country_Group$
        Else
            If FlagGroup = True Then
                lblGraphNote.Text = "Blue points are aggregation on available data only and might not always represent true aggregation."
                lblGraphNote.Visible = True
            Else
                lblGraphNote.Text = ""
                lblGraphNote.Visible = False
            End If

        End If


        'jrs 2005/08/ 29 Pego1.Visible = True

        optChart.Checked = True
        optData.Checked = False
        optBubble.Checked = False

        'grd_TableDisplay.Visible = False
        'Pego1.Visible = True

    End Sub

    Public Sub History_Filldata()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim GDPFld As String
        Dim POPFld As String
        Dim LNDFld As String
        Dim Auxfld As String
        Dim NVal As Object 'jrs 2005/08/25 where is NVal getting its value?
        Dim NullFound As Boolean
        Dim totalyearfields As Integer
        Dim seriesyears()
        Dim i As Integer
        Dim j As Integer
        Dim tblGDP As DataTable, tblPOP As DataTable, tblLND As DataTable, tblWeight As DataTable
        'jrs 2005/11/23
        Dim IsEstimate As Boolean

        Dim l%, YearVal%
        Dim Ag_rule_History$
        Dim hCOUNTE%


        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'mti July 2006 -start
        Ag_On_Group = False
        Ag_On_Region = False
        'mti July 2006 -end

        'jrs 2017/07/20 added if ="", blowing up with variables with no forecast
        'If Indicator_IfsVar(IIndex) = "" Then 'Not Session("SDGFilesSelected") Is Nothing Then
        'hCOUNTE = 1
        'Else
        hCOUNTE = COUNTESDG / Session("SDGFilesSelected")  'jrs 2005/09/05 1
        'End If

        'jrs 2011/09/28
        'Historic Analog formulas not implemented
        If UCase(TableName) = "SERIESFORMULA" Then
            Exit Sub
        End If

        'Dim saveAg_rule$
        On Error GoTo FileProblem

        '    saveAg_rule$ = Ag_rule$

        'for history
        '    If TableName$ = "SeriesIncBelow1Dollar%" Then '% already in history
        '          Ag_rule$ = "POP"
        '    End If


        Dim TotCountries, TotGroups, RegionNo As Integer

        Dim totregs As Integer, idx As Integer

        If FlagGroup = True Then 'Grouping turned on for regionalized variable

            '       Read and save pointers to group members and count of them
            '       ---------------------------------------------------------
            GroupMembCount = 0
            SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Grouping$ & " WHERE GroupNo = " & GroupCodes(GroupPoint%), "", Session("valtbl"))
            'JRS 2/2/02
            totregs = Session("valtbl").rows.count
            ReDim Regions(totregs)
            idx = 0
            'JRS 2/15/02 For ICount% = 1 To totregs 'Search all Countries to see if group members
            Do While Not Session("valtbl").rows.count = idx
                'JRS 2/15/02 TotGroups = valtbl.Fields("TotalGroups")
                'JRS 2/15/02 For JCount% = 1 To TotGroups 'Check each region across all groups
                'JRS 2/15/02 TmpStr$ = "Group" + Trim(Str(JCount%))
                'JRS 2/15/02 If valtbl.Fields(TmpStr$) = GroupPoint% Then
                'mti July 2006 -start
                'If Session("valtbl").rows(idx).item("GroupNo") = GroupPoint% Then
                'jrs 2006/08/03 If Session("valtbl").rows(idx).item("GroupNo") = GroupCodes(GroupPoint%) Then
                Regions(GroupMembCount) = Session("valtbl").rows(idx).item("Region")
                GroupMembCount = GroupMembCount + 1
                'End If
                'JRS 2/15/02 Next JCount%
                idx = idx + 1
                'JRS 2/15/02 Next ICount%
            Loop

            '       If there are group members, they have been saved by name in Regions; now pull
            '           each region number out of RegionIDNew$ and take those region numbers to
            '           the Regionalization table/recordset and find all countries in that region.
            '           That will allow them to be pulled by name into Countries and used to collect
            '           and sum values for the entire group
            '       --------------------------------------------------------------------------------

            If GroupMembCount >= 1 Then
                'jrs 2006/08/03
                SetDataTablefromAccess(IFs_MDB, "SELECT * FROM " & RegionIDNew$ & ", " & Grouping$ &
                ", " & Regionalization$ & " WHERE NewRegion = RegionNum AND RegionName = Region AND GroupNo = " &
                GroupCodes(GroupPoint%) & " Order by Country", "", Session("valtbl"))

                'jrs 2007/04/06 changed from 200 to 250 because of add provinces
                ReDim Countries(250) 'Change to Countrycount
                ReDim grCntryBaseVal(250)
                ReDim grCntryLYVal(250)

                'mti 10/27/05
                ReDim regIntercept(250)
                ReDim regSlope(250)
                ReDim regShift(250)
                'mti 10/27/05

                Dim ICount As Integer
                Dim valtbl1 As DataTable

                GroupTot = 0
                'JRS 2006/08/03 For ICount% = 1 To GroupMembCount
                idx = 0

                Do While Not Session("valtbl").rows.count = idx
                    'If Regions(ICount% - 1) = Session("valtbl").rows(idx).item("RegionName") Then
                    'JRS 2006/08/03 RegionNo = Session("valtbl").rows(idx).item("RegionNum")
                    'JRS 2006/08/03 SetDataTablefromAccess(IFs_MDB, "SELECT * FROM " + Regionalization$ + " Where NewRegion = " + Str(RegionNo) + " Order by Country", "", valtbl1)
                    'JRS 2006/08/03 Dim idx1 As Integer
                    'Dimpoint% holds the region number
                    'JRS 2006/08/03 RegionTot = valtbl1.Rows.Count
                    'JRS 2006/08/03 idx1 = 0
                    'JRS 2006/08/03 For EleLoop = 1 To RegionTot
                    Countries(GroupTot) = Session("valtbl").Rows(idx).Item("Country")
                    GroupTot = GroupTot + 1
                    'JRS 2006/08/03 idx1 = idx1 + 1
                    'JRS 2006/08/03 Next EleLoop
                    'JRS 2006/08/03 Exit Do
                    'End If
                    idx = idx + 1
                Loop
                'JRS 2006/08/03 Next ICount%

                If GroupTot > 1 Then
                    Ag_On_Group = True
                Else
                    Ag_On_Group = False
                End If

            End If


        Else 'No Grouping, Countries or Region

            'mti SDG bug
            'mti update for BBH December 2004 '2nd dim for hostoric analog
            If SDimPoint% = 0 Then 'no second dim
                SetDataTablefromAccess(IFs_MDB, "SELECT * FROM " + Regionalization$ + " Where NewRegion = " + Str(DimPoint%) + " Order by Country", "", Session("valtbl"))
                'Dimpoint% holds the region number
            Else  'second dim
                SetDataTablefromAccess(IFs_MDB, "SELECT * FROM " + Regionalization$ + " Where NewRegion = " + Str(SDimPoint%) + " Order by Country", "", Session("valtbl"))
                'sDimpoint% holds the region number from 1st dim

            End If
            'mti update for BBH December 2004 '2nd dim for hostoric analog

            'Dimpoint% holds the region number
            RegionTot = Session("valtbl").Rows.Count
            ReDim Countries(RegionTot)
            If RegionTot > 1 Then
                Ag_On_Region = True
                'ReDim Countries(RegionTot)
                idx = 0
                For EleLoop = 1 To RegionTot
                    Countries(EleLoop - 1) = Session("valtbl").Rows(idx).Item("Country")
                    idx = idx + 1
                Next EleLoop
            Else
                Ag_On_Region = False
                Countries(0) = Session("valtbl").Rows(0).Item("Country") '??
            End If

        End If  'grouping test

        Dim VarValSave As String

        If UCase$(Regionalization$) <> UCase$("RegionalizationCoVaTrA") Then
            'JRS 11/30/02 PASSWORD
            'Set db = OpenDatabase(IFsHistSeries_MDB, False, True, "; pwd=[REDACTED]")
            SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.MDB", "SELECT * FROM DataDict", "Table", Session("valtbl"))
            idx = 0
            Ag_rule_History$ = SeekValue(Session("valtbl"), TableName, "Aggregation")
            MapFormSeries$ = SeekValue(Session("valtbl"), TableName, "Variable") + " - " + SeekValue(Session("valtbl"), TableName, "Definition") + " - " + SeekValue(Session("valtbl"), TableName, "Units")
            Session("MapFormSeries") = MapFormSeries

            Session("dbSDG") = Session("IFsHistSeries_MDB")
            Session("sPwdSDG") = DataPW$
        Else
            SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.MDB", "SELECT * FROM DataDict", "Table", Session("valtbl"))
            idx = 0
            VarValSave$ = SeekValue(Session("valtbl"), TableName, "CoVaTrA")
            Ag_rule_History$ = SeekValue(Session("valtbl"), TableName, "Aggregation")
            If VarValSave$ = "Yes" Then
                Session("dbSDG") = Session("IFsCoVaTrASeries_MDB")
                Session("sPwdSDG") = ""
            Else
                'Set db = OpenDatabase(IFsHistSeries_MDB)
                Session("dbSDG") = Session("IFsHistSeries_MDB")
                Session("sPwdSDG") = DataPW$
            End If
        End If


        If Ag_On_Region Or Ag_On_Group Then

            'jrs 2007/04/06 changed from 200 to 250 because of provinces
            ReDim SDGBubbleSize(MaxYears, 250)
            ReDim SDGBubbleData(MaxYears, 250)

            If Ag_On_Region Then
                agcount = RegionTot
            Else
                agcount = GroupTot
            End If

            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM [" & TableName & "]", "Country", Session("valtbl"), Session("sPwdSDG"))


            Dim idxGDP As Integer = 0, idxPOP As Integer = 0, idxLND As Integer = 0
            'jrs 2011/11/23
            'SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM SeriesGDP95", "Country", tblGDP, Session("sPwdSDG"))
            'SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM SeriesPopulation", "Country", tblPOP, Session("sPwdSDG"))
            'SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM SeriesLandTotal", "Country", tblLND, Session("sPwdSDG"))
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM Series" & IFsAggRules.Item("GDP").HisEmpVar, "Country", tblGDP, Session("sPwdSDG"))
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM Series" & IFsAggRules.Item("POP").HisEmpVar, "Country", tblPOP, Session("sPwdSDG"))
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM Series" & IFsAggRules.Item("LND").HisEmpVar, "Country", tblLND, Session("sPwdSDG"))
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM Series" & IFsAggRules.Item(Ag_rule_History$).HisEmpVar, "Country", tblWeight, Session("sPwdSDG"))

            totalyearfields = Session("valtbl").Columns.Count - 2


            ReDim seriesyears(totalyearfields)
            For i = 0 To totalyearfields - 1
                seriesyears(i) = Val(Session("valtbl").Columns(i + 2).Caption)
                ' Check here fields() start from 0 or 1
                '            If Trim(seriesyears(i)) = "1990" Then
                '                i1990 = i
                '            End If
                'mti July 2006 -start
                If seriesyears(i) > 0 Then LatestYear = seriesyears(i)
                'mti July 2006 -end
            Next i

            'mti July 2006 -start
            If Session("SDGEstimatesMRandC") Then
                Find_Group_Base_Earliest_MostRecent(Session("valtbl"), CInt(agcount))
            Else
                Find_Group_Base_Latest(Session("valtbl"), CInt(agcount))
            End If
            'mti July 2006 - end

            'mti July 2006 start
            Dim tblDriver1 As DataTable
            'mti July 2006 - end
            'mti 10/23/05
            Dim tblDriver As DataTable
            If Indicator_DriverTbl(IIndex) <> "" Then
                SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM " & Indicator_DriverTbl(IIndex), "Country", tblDriver, Session("sPwdSDG"))
                'mti July 2006 start
                If Indicator_DriverTbl1(IIndex) <> "" Then
                    SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM " & Indicator_DriverTbl1(IIndex), "Country", tblDriver1, Session("sPwdSDG"))
                End If
                'mti July 2006 end
            End If
            'mti 10/23/05 -end

            Dim SumplotB As Object, SumWeightB As Object, SumNumB As Object
            Dim SumplotLY As Object, SumWeightLy As Object, SumNumLY As Object
            Dim Sumplot As Double, SumWeight As Double, SumNum As Double, LNDval As Object, Auxval As Object
            Dim k As Integer
            Dim varvally As Object, GDPvally As Object, POPvally As Object, LNDvally As Object
            Dim GDPVal As Object, POPVal As Object

            'SDG Base and curval initialization
            SumplotB = 0
            SumWeightB = 0
            SumNumB = 0

            SumplotLY = 0
            SumWeightLy = 0
            SumNumLY = 0

            'mti August 29, 2006 start
            'open run file for pop, 2000
            Dim sFileName As String, fNum As Integer
            sFileName = Session("PutFdrvRun") + "IFSBASE.RUN"
            'jrs 2007/04/07 this is not working, the file is getting closed somewhere in the code
            'or is not accesible in other subroutines if opened here
            'fNum = FreeFile()
            'FileOpen(fNum, sFileName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            'Session("FileforDisplay") = fNum
            'mti August 29, 2006 end

            'open regionalization for matching regions
            Dim valtblG As DataTable
            SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM " & Regionalization, "Country", valtblG)
            'mti August 2006 end
            'jrs 2006/08/29
            Dim rpointer As Integer

            'SDG base and curval initialization

            j = 0
            For i2 = 1 To LatestYear - FirstYearSDG% + 1 'LastYear% - FirstYear% + 1  ' LastYearHistory% - FirstYearHistory%
                SDGHasData(i2, hCOUNTE) = ""
                NullFound = False
                'JRS 06/25/02
                'Code to start reading data from first year
                Do While i2 + FirstYearSDG% - 1 > seriesyears(j) And j < totalyearfields
                    j = j + 1
                Loop
                If j < totalyearfields And i2 + FirstYearSDG% - 1 = seriesyears(j) Then
                    'data for this year is in Table
                    Session("VarVal") = 0
                    Sumplot = 0
                    SumWeight = 0
                    SumNum = 0


                    idx = 0

                    Select Case Ag_rule_History$

                        Case "SUM"

                            For k = 0 To agcount - 1
                                'mti July 2006 -start
                                'If seriesYears(j) = LatestYear Then
                                If seriesyears(j) = FirstYearForecast Or seriesyears(j) = LatestYear Then
                                    'mti July 2006 end
                                    varvally = grCntryLYVal(k)
                                    If (IsNumeric(varvally)) Then
                                        SumplotLY = SumplotLY + varvally
                                    End If
                                    'mti July 2006
                                    grCntryLYVal(k) = DBNull.Value 'so that it cannot be used twice if latestyear<FirstYearForecast
                                    'mti July 2006 end
                                End If
                                Session("VarVal") = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j + 2).Caption)
                                'mti 10/23/05
                                IsEstimate = False
                                If Not IsNumeric(Session("VarVal")) Then
                                    If Session("SDGEstimates") Then
                                        'mti July 2006 -start
                                        'If mnuEstMRandC.Checked Or IndicatorName(iIndex) = "Indicator 1" Or IndicatorName(iIndex) = "Indicator 1a" Then
                                        If Session("SDGEstimatesMRandC") Then
                                            Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            If IsDBNull(Session("VarVal")) And Indicator_DriverTbl1(IIndex) <> "" Then
                                                Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver1, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            End If

                                        Else
                                            Session("VarVal") = Find_Country_Year_Estimate4Group(tblDriver, CStr(Countries(k)), CStr(seriesyears(j)), regIntercept(k), regSlope(k), regShift(k))
                                        End If
                                        'mti July 2006 -end

                                        IsEstimate = True
                                    End If
                                End If
                                'mti 10/23/05 -end

                                '1st and second field = Country and Fips
                                If (IsNumeric(Session("VarVal"))) Then
                                    Sumplot = Sumplot + Session("VarVal")
                                    'mti 10/23/05
                                    SDGHasData(i2, hCOUNTE) = SDGHasData(i2, hCOUNTE) + IIf(SDGHasData(i2, hCOUNTE) <> "", ",", "") + Countries(k) + "(" + CStr(Math.Round(Val(Session("VarVal")), 2)) + IIf(IsEstimate, "*", "") + ")"
                                    'mti 10/23/05 -end
                                Else
                                    'There are Nulls in the set
                                    Session("VarVal") = 0
                                    NullFound = True
                                End If
                                idx = idx + 1
                            Next k
                            Session("VarVal") = Sumplot   'PLOT(i2, hCOUNTE)

                        Case "AVG"
                            For k = 0 To agcount - 1
                                'mti July 2006 start
                                'If seriesYears(j) = LatestYear Then
                                If seriesyears(j) = FirstYearForecast Or seriesyears(j) = LatestYear Then
                                    'mti July 2006 end

                                    varvally = grCntryLYVal(k)
                                    If (IsNumeric(varvally)) Then
                                        SumplotLY = SumplotLY + varvally
                                        SumNumLY = SumNumLY + 1
                                    End If
                                    'mti July 2006 start
                                    grCntryLYVal(k) = DBNull.Value 'so that it cannot be used twice if latestyear<FirstYearForecast
                                    'mti July 2006 end
                                End If
                                Session("VarVal") = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j + 2).Caption)
                                'mti 10/23/05
                                IsEstimate = False
                                If Not IsNumeric(Session("VarVal")) Then
                                    If Session("SDGEstimates") Then
                                        'mti July 2006 -start
                                        'If mnuEstMRandC.Checked Or IndicatorName(iIndex) = "Indicator 1" Or IndicatorName(iIndex) = "Indicator 1a" Then
                                        If Session("SDGEstimatesMRandC") Then
                                            Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            If IsDBNull(Session("VarVal")) And Indicator_DriverTbl1(IIndex) <> "" Then
                                                Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver1, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            End If

                                        Else
                                            Session("VarVal") = Find_Country_Year_Estimate4Group(tblDriver, CStr(Countries(k)), CStr(seriesyears(j)), regIntercept(k), regSlope(k), regShift(k))
                                        End If
                                        'mti July 2006 -end

                                        IsEstimate = True
                                    End If
                                End If
                                'mti 10/23/05 -end
                                '1st and second field = Country and Fips
                                If (IsNumeric(Session("VarVal"))) Then
                                    Sumplot = Sumplot + Session("VarVal") 'JRS 06/25/02
                                    SumNum = SumNum + 1
                                    'mti 10/23/05
                                    SDGHasData(i2, hCOUNTE) = SDGHasData(i2, hCOUNTE) + IIf(SDGHasData(i2, hCOUNTE) <> "", ",", "") + Countries(k) + "(" + CStr(Math.Round(Val(Session("VarVal")), 2)) + IIf(IsEstimate, "*", "") + ")"
                                    'mti 10/23/05 -end
                                Else
                                    Session("VarVal") = 0
                                    NullFound = True
                                End If
                                idx = 0
                            Next k
                            If SumNum <> 0 Then
                                Session("VarVal") = Sumplot / SumNum  'PLOT(i2, hCOUNTE)
                            Else
                                Session("VarVal") = 0 ' o in denom  'PLOT(i2, hCOUNTE)
                            End If

                        Case "GDP"
                            idxGDP = 0
                            For k = 0 To agcount - 1
                                'VarVal = valtbl.Fields(j + 2)
                                'mti August 2006 start
                                rpointer = IFsRegs.Item(Countries(k)).Index
                                If rpointer = -1 Then
                                    rpointer = SeekValue(valtblG, Countries(k), "NewRegion")
                                End If
                                'Ag_Rule_Idx = POPidx
                                'mti August 2006 end

                                '1st and second field = Country and Fips
                                GDPFld$ = Trim(Str(seriesyears(j))) 'take most recent' see Find_Group_Base_Latest
                                'mti July 2006 start
                                'If seriesYears(j) = LatestYear Then
                                If seriesyears(j) = FirstYearForecast Or seriesyears(j) = LatestYear Then
                                    'VarVal = grCntryLYVal(k)
                                    'GDPFld$ = "MostRecent"
                                    'For l = 0 To tblGDP.Columns.Count - 1
                                    '    YearVal = Val(tblGDP.Columns(l).Caption)
                                    '    If YearVal = LatestYear Then
                                    '        GDPFld$ = Trim(Str(LatestYear))
                                    '        Exit For
                                    '    End If
                                    'Next
                                    varvally = grCntryLYVal(k)
                                    'GDPvally = SeekValue(tblGDP, Countries(k), GDPFld)
                                    GDPvally = FindVarValForCountryYear(Ag_rule_History$, rpointer, FirstYearForecast)
                                    If (IsNumeric(varvally)) And (IsNumeric(GDPvally)) Then
                                        SumplotLY = SumplotLY + varvally * GDPvally 'Convert from Millions
                                        SumWeightLy = SumWeightLy + GDPvally   'if Null?
                                    End If
                                    grCntryLYVal(k) = DBNull.Value 'so that it cannot be used twice if latestyear<FirstYearForecast
                                End If

                                If Val(GDPFld$) >= FirstYearForecast Then
                                    GDPVal = FindVarValForCountryYear(Ag_rule_History$, rpointer, Val(GDPFld$))
                                Else
                                    'jrs 2011/11/23
                                    If checkField(tblGDP, CInt(GDPFld$)) Then
                                        GDPVal = SeekValue(tblGDP, Countries(k), GDPFld) 'jrs 2006/08/02 tblGDP.Fields(GDPFld$)  'POP of that particular year
                                    Else
                                        GDPVal = System.DBNull.Value
                                    End If


                                End If

                                'mti July 2006 - end

                                Session("VarVal") = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j + 2).Caption)
                                'mti 10/23/05
                                IsEstimate = False
                                If Not IsNumeric(Session("VarVal")) Then
                                    If Session("SDGEstimates") Then
                                        'mti July 2006 -start
                                        'If mnuEstMRandC.Checked Or IndicatorName(iIndex) = "Indicator 1" Or IndicatorName(iIndex) = "Indicator 1a" Then
                                        If Session("SDGEstimatesMRandC") Then
                                            Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            If IsDBNull(Session("VarVal")) And Indicator_DriverTbl1(IIndex) <> "" Then
                                                Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver1, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            End If

                                        Else
                                            Session("VarVal") = Find_Country_Year_Estimate4Group(tblDriver, CStr(Countries(k)), CStr(seriesyears(j)), regIntercept(k), regSlope(k), regShift(k))
                                        End If
                                        'mti July 2006 -end

                                        IsEstimate = True
                                    End If
                                End If
                                'mti 10/23/05 -end
                                'jrs 2011/11/23
                                If checkField(tblGDP, CInt(GDPFld$)) Then
                                    GDPVal = SeekValue(tblGDP, Countries(k), GDPFld) 'GDP of that particular year
                                Else
                                    GDPVal = System.DBNull.Value
                                End If
                                If (IsNumeric(Session("VarVal"))) And (IsNumeric(GDPVal)) Then
                                    Sumplot = Sumplot + Session("VarVal") * GDPVal 'Convert from Millions
                                    SumWeight = SumWeight + GDPVal   'if Null?
                                    'mti 10/23/05
                                    SDGHasData(i2, hCOUNTE) = SDGHasData(i2, hCOUNTE) + IIf(SDGHasData(i2, hCOUNTE) <> "", ",", "") + Countries(k) + "(" + CStr(Math.Round(Val(Session("VarVal")), 2)) + IIf(IsEstimate, "*", "") + ")"
                                    'mti 10/23/05 -end
                                Else
                                    NullFound = True
                                    Session("VarVal") = 0
                                    GDPVal = 0
                                End If
                                idx = 0
                                idxGDP = 0
                            Next k
                            If SumWeight <> 0 Then
                                Session("VarVal") = Sumplot / SumWeight  ' PLOT(i2, hCOUNTE)
                            Else
                                Session("VarVal") = 0  'Zero In Denominator 'PLOT(i2, hCOUNTE)
                            End If

                                'tblGDP.Close '??

                        Case "POP"

                            idxPOP = 0
                            For k = 0 To agcount - 1
                                'VarVal = valtbl.Fields(j + 2)
                                'mti August 2006 start
                                rpointer = IFsRegs.Item(Countries(k)).Index
                                If rpointer = -1 Then
                                    rpointer = SeekValue(valtblG, Countries(k), "NewRegion")
                                End If
                                'Ag_Rule_Idx = POPidx
                                'mti August 2006 end

                                POPFld$ = Trim(Str(seriesyears(j)))
                                'mti July 2006
                                'If seriesYears(j) = LatestYear Then
                                If seriesyears(j) = FirstYearForecast Or seriesyears(j) = LatestYear Then
                                    'VarVal = grCntryLYVal(k)
                                    'POPFld$ = "MostRecent"
                                    'For l = 0 To tblPOP.Columns.Count - 1
                                    '    YearVal = Val(tblPOP.Columns(l).Caption)
                                    '    If YearVal = LatestYear Then
                                    '        POPFld$ = Trim(Str(LatestYear))
                                    '        Exit For
                                    '    End If
                                    'Next
                                    varvally = grCntryLYVal(k)
                                    'POPvally = SeekValue(tblPOP, Countries(k), POPFld)
                                    POPvally = FindVarValForCountryYear(Ag_rule_History$, rpointer, FirstYearForecast)
                                    If (IsNumeric(varvally)) And (IsNumeric(POPvally)) Then
                                        SumplotLY = SumplotLY + varvally * POPvally 'Convert from Millions
                                        SumWeightLy = SumWeightLy + POPvally   'if Null?
                                    End If
                                    grCntryLYVal(k) = DBNull.Value 'so that it cannot be used twice if latestyear<FirstYearForecast
                                End If

                                If Val(POPFld$) >= FirstYearForecast Then
                                    POPVal = FindVarValForCountryYear(Ag_rule_History$, rpointer, Val(POPFld$))
                                Else
                                    POPVal = SeekValue(tblPOP, Countries(k), POPFld) 'jrs 2006/08/02 tblPOP.Fields(POPFld$)  'POP of that particular year
                                End If

                                'mti July 2006 - end

                                Session("VarVal") = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j + 2).Caption)
                                'End If
                                'mti 10/23/05
                                IsEstimate = False
                                If Not IsNumeric(Session("VarVal")) Then
                                    If Session("SDGEstimates") Then
                                        'mti July 2006 -start
                                        'If mnuEstMRandC.Checked Or IndicatorName(iIndex) = "Indicator 1" Or IndicatorName(iIndex) = "Indicator 1a" Then
                                        If Session("SDGEstimatesMRandC") Then
                                            Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            If IsDBNull(Session("VarVal")) And Indicator_DriverTbl1(IIndex) <> "" Then
                                                Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver1, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            End If

                                        Else
                                            Session("VarVal") = Find_Country_Year_Estimate4Group(tblDriver, CStr(Countries(k)), CStr(seriesyears(j)), regIntercept(k), regSlope(k), regShift(k))
                                        End If
                                        'mti July 2006 -end

                                        IsEstimate = True
                                    End If
                                End If
                                'mti 10/23/05 -end
                                '1st and second field = Country and Fips

                                'jrs 2007/04/07 blowing up for groups, POP doesn't have year 2003 yet
                                'this line is being computed above in line 1952
                                'POPVal = SeekValue(tblPOP, Countries(k), POPFld$)  'POP of that particular year

                                If (IsNumeric(Session("VarVal"))) And (IsNumeric(POPVal)) Then
                                    Sumplot = Sumplot + Session("VarVal") * POPVal 'Convert from Millions
                                    SumWeight = SumWeight + POPVal   'if Null?
                                    'mti 10/23/05
                                    SDGHasData(i2, hCOUNTE) = SDGHasData(i2, hCOUNTE) + IIf(SDGHasData(i2, hCOUNTE) <> "", ",", "") + Countries(k) + "(" + CStr(Math.Round(Val(Session("VarVal")), 2)) + IIf(IsEstimate, "*", "") + ")"
                                    'mti 10/23/05 -end
                                    SDGBubbleSize(i2, k) = (Session("VarVal") / 100) * POPVal
                                    SDGBubbleData(i2, k) = Session("VarVal")

                                Else
                                    NullFound = True
                                    Session("VarVal") = 0
                                    POPVal = 0
                                End If
                                idx = 0
                                idxPOP = 0
                            Next k
                            If SumWeight <> 0 Then
                                Session("VarVal") = Sumplot / SumWeight ' PLOT(i2, hCOUNTE)
                            Else
                                Session("VarVal") = 0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                            End If

                        Case "LND"
                            idxLND = 0
                            For k = 0 To agcount - 1
                                'VarVal = valtbl.Fields(j + 2)

                                '1st and second field = Country and Fips
                                LNDFld$ = "MostRecent" 'Trim(str(seriesyears(j))) 'take most recent' see Find_Group_Base_Latest
                                'mti July 2006 start
                                'If seriesYears(j) = LatestYear Then
                                If seriesyears(j) = FirstYearForecast Or seriesyears(j) = LatestYear Then
                                    'mti July 2006 end

                                    'VarVal = grCntryLYVal(k)
                                    LNDFld$ = "MostRecent"
                                    For l = 0 To tblLND.Columns.Count - 1
                                        YearVal = Val(tblLND.Columns(l).Caption)
                                        If YearVal = LatestYear Then
                                            LNDFld$ = Trim(Str(LatestYear))
                                            Exit For
                                        End If
                                    Next
                                    varvally = grCntryLYVal(k)
                                    LNDvally = SeekValue(tblLND, Countries(k), LNDFld$)
                                    If (IsNumeric(varvally)) And (IsNumeric(LNDvally)) Then
                                        SumplotLY = SumplotLY + varvally * LNDvally 'Convert from Millions
                                        SumWeightLy = SumWeightLy + LNDvally   'if Null?
                                    End If
                                    'mti July 2006 start
                                    grCntryLYVal(k) = DBNull.Value 'so that it cannot be used twice if latestyear<FirstYearForecast
                                    'mti July 2006 end

                                End If

                                Session("VarVal") = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j + 2).caption)

                                'mti 10/23/05
                                IsEstimate = False
                                If Not IsNumeric(Session("VarVal")) Then
                                    If Session("SDGEstimates") Then
                                        'mti July 2006 -start
                                        'If mnuEstMRandC.Checked Or IndicatorName(iIndex) = "Indicator 1" Or IndicatorName(iIndex) = "Indicator 1a" Then
                                        If Session("SDGEstimatesMRandC") Then
                                            Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            If IsDBNull(Session("VarVal")) And Indicator_DriverTbl1(IIndex) <> "" Then
                                                Session("VarVal") = EstimateFromClosest(seriesyears(j), Session("valtbl"), tblDriver1, Countries(k),
                                                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                                            End If

                                        Else
                                            Session("VarVal") = Find_Country_Year_Estimate4Group(tblDriver, CStr(Countries(k)), CStr(seriesyears(j)), regIntercept(k), regSlope(k), regShift(k))
                                        End If
                                        'mti July 2006 -end

                                        IsEstimate = True
                                    End If
                                End If
                                'mti 10/23/05 -end

                                LNDval = SeekValue(tblLND, Countries(k), LNDFld$)  'LND of that particular year
                                If (IsNumeric(Session("VarVal"))) And (IsNumeric(LNDval)) Then
                                    Sumplot = Sumplot + Session("VarVal") * LNDval 'Convert from Millions
                                    SumWeight = SumWeight + LNDval   'if Null?
                                    'mti 10/23/05
                                    SDGHasData(i2, hCOUNTE) = SDGHasData(i2, hCOUNTE) + IIf(SDGHasData(i2, hCOUNTE) <> "", ",", "") + Countries(k) + "(" + CStr(Math.Round(Val(Session("VarVal")), 2)) + IIf(IsEstimate, "*", "") + ")"
                                    'mti 10/23/05 -end
                                Else
                                    NullFound = True
                                    Session("VarVal") = 0
                                    LNDval = 0
                                End If
                                idx = 0
                                idxLND = 0
                            Next k
                            If SumWeight <> 0 Then
                                Session("VarVal") = Sumplot / SumWeight  ' PLOT(i2, hCOUNTE)
                            Else
                                Session("VarVal") = 0  'Zero In Denominator 'PLOT(i2, hCOUNTE)
                            End If

                        Case Else 'jrs 2006/09/28
                            If Ag_rule_History$ > "" Then
                                SumWeight = 0
                                For kk = 1 To agcount
                                    Session("VarVal") = SeekValue(Session("valtbl"), Countries(kk - 1), Session("valtbl").Columns(j + 2).Caption)

                                    '1st and second field = Country and Fips
                                    If Ag_rule_History$ = "LND" Then
                                        Auxfld$ = "MostRecent"
                                    Else
                                        'jrs 2015/06/04
                                        If seriesyears(j) < FirstYearHistory Then
                                            Auxfld$ = "Earliest"
                                        Else
                                            Auxfld$ = Trim(Str(seriesyears(j)))
                                        End If
                                    End If
                                    Auxval = SeekValue(tblWeight, Countries(kk - 1), Auxfld)

                                    If (IsNumeric(Session("VarVal"))) And (IsNumeric(Auxval)) Then
                                        'jrs 2007/07/20
                                        If IsDBNull(Sumplot) Then
                                            Sumplot = Session("VarVal") * Auxval
                                        Else
                                            Sumplot = Sumplot + Session("VarVal") * Auxval
                                        End If
                                        SumWeight = SumWeight + Auxval
                                    Else
                                        NullFound = True
                                        Session("VarVal") = 0
                                        Auxval = 0
                                    End If
                                Next kk
                                If SumWeight <> 0 Then
                                    Session("VarVal") = Sumplot / SumWeight
                                ElseIf IsDBNull(Sumplot) Then 'jrs 2007/07/20 in case all values are null
                                    Session("VarVal") = System.DBNull.Value
                                Else
                                    Session("VarVal") = 0  'Zero In Denominator
                                End If
                            End If
                    End Select


                    'formula in history
                    If NumVal = True Then
                        NewVal = NVal
                    Else
                        NewVal = Session("VarVal")  'PLOT(i2, hCOUNTE)  'valtbl.Fields(j + 2)
                    End If

                    'don't need the following
                    If Not IsNumeric(NewVal) Then
                        'jrs 2008/10/23 NewVal = 0
                        NullFound = True
                    End If

                    'JRS 9/23/01 Changes to add Computations
                    If Session("bComputation") Then
                        If Session("bCompAll") Then
                            PLOTAllSDG(i2, hCOUNTE) = NewVal
                        Else
                            PLOTCompSDG(i2) = NewVal
                        End If
                    Else
                        If DivOnPrev = False And MulOnPrev = False _
                            And AddOnPrev = False And SubOnPrev = False Then
                            'PLOT(i2, hCOUNTE) = NewVal
                            PLOTSDGH(i2, hCOUNTE) = NewVal 'PLOT(i2, hCOUNTE)
                        ElseIf DivOnPrev = True Then
                            If NewVal <> 0 Then
                                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) / NewVal
                                PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) / NewVal 'PLOT(i2, hCOUNTE)
                            End If
                            'what if NewVal = 0, ask BBH
                        ElseIf MulOnPrev = True Then
                            'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) * NewVal
                            PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) * NewVal 'PLOT(i2, hCOUNTE)
                        ElseIf AddOnPrev = True Then
                            'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) + NewVal
                            PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) + NewVal 'PLOT(i2, hCOUNTE)
                        ElseIf SubOnPrev = True Then
                            'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) - NewVal
                            PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) - NewVal 'PLOT(i2, hCOUNTE)
                        End If
                    End If

                    'formula in history


                    j = j + 1
                Else
                    'JRS 9/23/01 Changes to add Computations
                    If Session("bComputation") Then
                        If Session("bCompAll") Then
                            PLOTAllSDG(i2, hCOUNTE) = Nothing
                        Else
                            PLOTCompSDG(i2) = Nothing
                        End If
                    Else
                        'PLOT(i2, hCOUNTE) = Null
                        PLOTSDGH(i2, hCOUNTE) = Nothing 'PLOT(i2, hCOUNTE)

                    End If
                End If

            Next i2

            'SDG Base and Curval aggregate done here
            'SDG Base Year Aggregation
            Dim VarValb As Object

            Select Case Ag_rule_History$
                Case "SUM"
                    For k = 0 To agcount - 1
                        VarValb = grCntryBaseVal(k)
                        If (IsNumeric(VarValb)) Then
                            SumplotB = SumplotB + VarValb
                            SumNumB = SumNumB + 1
                        End If
                    Next k
                    If SumNumB <> 0 Then
                        VarValb = SumplotB
                    Else
                        VarValb = Nothing
                    End If
                Case "AVG"
                    For k = 0 To agcount - 1
                        VarValb = grCntryBaseVal(k)
                        If (IsNumeric(VarValb)) Then
                            SumplotB = SumplotB + VarValb
                            SumNumB = SumNumB + 1
                        End If
                    Next k

                    If SumNumB <> 0 Then
                        VarValb = SumplotB / SumNumB ' PLOT(i2, hCOUNTE)
                    Else
                        VarValb = Nothing '0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                    End If

                Case "POP"
                    idxPOP = 0
                    For k = 0 To agcount - 1
                        VarValb = grCntryBaseVal(k)
                        Dim POPvalb As Object
                        POPFld$ = Trim(Str(Indicator_BaseYear(IIndex)))
                        POPvalb = SeekValue(tblPOP, Countries(k), POPFld$)

                        If (IsNumeric(VarValb)) And (IsNumeric(POPvalb)) Then
                            SumplotB = SumplotB + VarValb * POPvalb 'Convert from Millions
                            SumWeightB = SumWeightB + POPvalb   'if Null?
                        End If
                        idxPOP = 0
                    Next k

                    If SumWeightB <> 0 Then
                        VarValb = SumplotB / SumWeightB ' PLOT(i2, hCOUNTE)
                    Else
                        VarValb = Nothing '0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                    End If
                Case "GDP"
                    idxGDP = 0
                    For k = 0 To agcount - 1
                        VarValb = grCntryBaseVal(k)

                        GDPFld$ = Trim(Str(Indicator_BaseYear(IIndex)))
                        Dim GDPvalb As Object
                        GDPvalb = SeekValue(tblGDP, Countries(k), GDPFld$)
                        If (IsNumeric(VarValb)) And (IsNumeric(GDPvalb)) Then
                            SumplotB = SumplotB + VarValb * GDPvalb
                            SumWeightB = SumWeightB + GDPvalb   'if Null?
                        End If
                        idxGDP = 0
                    Next k

                    If SumWeightB <> 0 Then
                        VarValb = SumplotB / SumWeightB ' PLOT(i2, hCOUNTE)
                    Else
                        VarValb = Nothing '0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                    End If
                Case "LND"
                    idxLND = 0
                    For k = 0 To agcount - 1
                        VarValb = grCntryBaseVal(k)
                        LNDFld$ = "Earliest" 'Trim(str(Indicator_BaseYear(IIndex)))
                        Dim LNDvalb As Object
                        LNDvalb = SeekValue(tblLND, Countries(k), LNDFld$)
                        If (IsNumeric(VarValb)) And (IsNumeric(LNDvalb)) Then
                            SumplotB = SumplotB + VarValb * LNDvalb
                            SumWeightB = SumWeightB + LNDvalb   'if Null?
                        End If
                        idxLND = 0
                    Next k

                    If SumWeightB <> 0 Then
                        VarValb = SumplotB / SumWeightB ' PLOT(i2, hCOUNTE)
                    Else
                        VarValb = Nothing '0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                    End If


            End Select
            'formula in history
            If NumVal = True Then
                NewVal = NVal  'pure number
            Else
                NewVal = VarValb  'PLOT(i2, hCOUNTE)  'valtbl.Fields(j + 2)
            End If

            'don't need the following
            If Not IsNumeric(NewVal) Then
                NewVal = Nothing '0
                NullFound = True
            End If

            If DivOnPrev = False And MulOnPrev = False _
                And AddOnPrev = False And SubOnPrev = False Then
                'PLOT(i2, hCOUNTE) = NewVal
                BaseVal(hCOUNTE) = NewVal 'PLOT(i2, hCOUNTE)
            ElseIf DivOnPrev = True Then
                If NewVal <> 0 Then
                    'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) / NewVal
                    BaseVal(hCOUNTE) = BaseVal(hCOUNTE) / NewVal 'PLOT(i2, hCOUNTE)
                End If
                'what if NewVal = 0, ask BBH
            ElseIf MulOnPrev = True Then
                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) * NewVal
                BaseVal(hCOUNTE) = BaseVal(hCOUNTE) * NewVal 'PLOT(i2, hCOUNTE)
            ElseIf AddOnPrev = True Then
                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) + NewVal
                BaseVal(hCOUNTE) = BaseVal(hCOUNTE) + NewVal 'PLOT(i2, hCOUNTE)
            ElseIf SubOnPrev = True Then
                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) - NewVal
                BaseVal(hCOUNTE) = BaseVal(hCOUNTE) - NewVal 'PLOT(i2, hCOUNTE)
            End If

            '                        End If

            'SDG Latest Year
            '                        If seriesyears(j) = LatestYear Then

            Select Case Ag_rule_History$
                Case "SUM"
                    varvally = SumplotLY
                Case "AVG"
                    If SumNumLY <> 0 Then
                        varvally = SumplotLY / SumNumLY ' PLOT(i2, hCOUNTE)
                    Else
                        varvally = 0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                    End If

                Case "POP", "GDP", "LND"
                    If SumWeightLy <> 0 Then
                        varvally = SumplotLY / SumWeightLy ' PLOT(i2, hCOUNTE)
                    Else
                        varvally = 0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
                    End If

            End Select

            '                            If SumWeightLY <> 0 Then
            '                                varvally = SumplotLY / SumWeightLY ' PLOT(i2, hCOUNTE)
            '                            Else
            '                                varvally = 0   'Zero in Denom.  'PLOT(i2, hCOUNTE)
            '                            End If

            'formula in history
            If NumVal = True Then
                NewVal = NVal
            Else
                NewVal = varvally  'PLOT(i2, hCOUNTE)  'valtbl.Fields(j + 2)
            End If

            'don't need the following
            If Not IsNumeric(NewVal) Then
                'jrs 2008/10/23 NewVal = 0
                NullFound = True
            End If

            If DivOnPrev = False And MulOnPrev = False _
                And AddOnPrev = False And SubOnPrev = False Then
                'PLOT(i2, hCOUNTE) = NewVal
                Curval(hCOUNTE) = NewVal 'PLOT(i2, hCOUNTE)
            ElseIf DivOnPrev = True Then
                If NewVal <> 0 Then
                    'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) / NewVal
                    Curval(hCOUNTE) = Curval(hCOUNTE) / NewVal 'PLOT(i2, hCOUNTE)
                End If
                'what if NewVal = 0, ask BBH
            ElseIf MulOnPrev = True Then
                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) * NewVal
                Curval(hCOUNTE) = Curval(hCOUNTE) * NewVal 'PLOT(i2, hCOUNTE)
            ElseIf AddOnPrev = True Then
                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) + NewVal
                Curval(hCOUNTE) = Curval(hCOUNTE) + NewVal 'PLOT(i2, hCOUNTE)
            ElseIf SubOnPrev = True Then
                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) - NewVal
                Curval(hCOUNTE) = Curval(hCOUNTE) - NewVal 'PLOT(i2, hCOUNTE)
            End If

            'jrs 2007/04/07 this is not working, the file is getting closed somewhere in the code
            'or is not accesible in other subroutines if opened here
            ''jrs 2006/08/29
            'FileClose(fNum)

            '                        End If
            '
            '        'end SDG Base abd Curval aggegate done here
        Else  ' 1 country in the region, aggregation not needed
            'y = "SELECT * FROM " + TableName$ + " Where Country='" + Countries(0) + "'"
            NullFound = False
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM [" & TableName & "]", "Country", Session("valtbl"), Session("sPwdSDG"))
            '"SELECT * FROM " + TableName$ + _ " Where Country = '" + Countries(0) + "'")

            totalyearfields = Session("valtbl").Columns.Count - 2
            ReDim seriesyears(totalyearfields)
            For i = 0 To totalyearfields - 1
                seriesyears(i) = Val(Session("valtbl").Columns(i + 2).Caption)
                ' Check here fields() start from 0 or 1
                If seriesyears(i) > 0 Then LatestYear = seriesyears(i)
            Next i
            Session("SDGLatestYear") = LatestYear
            j = 0
            For i2 = 1 To LatestYear - FirstYearSDG% + 1 'LastYear% - FirstYear% + 1  ' LastYearHistory% - FirstYearHistory%
                'JRS 2/4/02
                If seriesyears(j) < FirstYearSDG% Then
                    Do While j < totalyearfields - 1
                        j = j + 1
                        If seriesyears(j) = FirstYearSDG% Then Exit Do
                    Loop
                End If
                If j < totalyearfields And i2 + FirstYearSDG% - 1 = seriesyears(j) Then
                    'data for this year is in Table

                    'formula in history



                    If NumVal = True Then
                        NewVal = NVal
                    Else
                        NewVal = SeekValue(Session("valtbl"), Countries(0), Session("valtbl").columns(j + 2).caption)
                    End If

                    'jrs 2005/08/29
                    'If Not IsNumeric(NewVal) Then
                    '    NewVal = 0
                    '    NullFound = True
                    'End If

                    'JRS 9/23/01 Changes to add Computations
                    If Session("bComputation") Then
                        If Session("bCompAll") Then
                            PLOTAllSDG(i2, hCOUNTE) = NewVal
                        Else
                            PLOTCompSDG(i2) = NewVal
                        End If
                    Else
                        If DivOnPrev = False And MulOnPrev = False _
                            And AddOnPrev = False And SubOnPrev = False Then
                            'PLOT(i2, hCOUNTE) = NewVal
                            PLOTSDGH(i2, hCOUNTE) = NewVal 'PLOT(i2, hCOUNTE)
                        ElseIf DivOnPrev = True Then
                            If NewVal <> 0 Then
                                'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) / NewVal
                                PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) / NewVal 'PLOT(i2, hCOUNTE)
                            End If
                            'what if NewVal = 0, ask BBH
                        ElseIf MulOnPrev = True Then
                            'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) * NewVal
                            PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) * NewVal 'PLOT(i2, hCOUNTE)
                        ElseIf AddOnPrev = True Then
                            'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) + NewVal
                            PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) + NewVal 'PLOT(i2, hCOUNTE)
                        ElseIf SubOnPrev = True Then
                            'PLOT(i2, hCOUNTE) = PLOT(i2, hCOUNTE) - NewVal
                            PLOTSDGH(i2, hCOUNTE) = PLOTSDGH(i2, hCOUNTE) - NewVal 'PLOT(i2, hCOUNTE)
                        End If
                    End If

                    'formula in history

                    'formula in history - uncomment if formula does not work
                    'PLOT(i2, hCOUNTE) = valtbl.Fields(j + 2)
                    'If IsNull(PLOT(i2, hCOUNTE)) Then
                    '    PLOT(i2, hCOUNTE) = 0
                    '    NullFound = True
                    'End If
                    'uncomment upto this

                    j = j + 1
                Else
                    'JRS 9/23/01 Changes to add Computations
                    If Session("bComputation") Then
                        If Session("bCompAll") Then
                            PLOTAllSDG(i2, hCOUNTE) = Nothing
                        Else
                            PLOTCompSDG(i2) = Nothing
                        End If
                    Else
                        'PLOT(i2, hCOUNTE) = Null
                        PLOTSDGH(i2, hCOUNTE) = Nothing  'PLOT(i2, hCOUNTE)
                    End If
                End If
            Next i2

        End If

        Session("PercentOn") = "False"

        'commented mti SDG
        '    FinYear(hCOUNTE) = LastYear% - FirstYear% + 1

        Session("valtbl") = Nothing
        tblGDP = Nothing
        tblPOP = Nothing
        Session("dbSDG") = Nothing
        Session("SDGCurval") = Curval
        Session("SDGPLOTSDGH") = PLOTSDGH
        Session("SDGSDGHasData") = SDGHasData

        'formula in history

        '    If DivOn = True Then
        '        DivOnPrev = True
        '    Else
        '        DivOnPrev = False
        '    End If
        '    If MulOn = True Then
        '        MulOnPrev = True
        '    Else
        '        MulOnPrev = False
        '    End If
        '    If AddOn = True Then
        '        AddOnPrev = True
        '    Else
        '        AddOnPrev = False
        '    End If
        '    If SubOn = True Then
        '        SubOnPrev = True
        '    Else
        '        SubOnPrev = False
        '    End If
        '
        '    'formula in history

        'mti Validation changes      'was wrong with all and Formula in History

        Dim ResetCalcFlag As Boolean
        ResetCalcFlag = False
        If SeqRepeat = 0 Then
            ResetCalcFlag = True
        Else
            If Not FlagGroup And SeqRepeat = MaxVal(DimNum) - 1 Then
                ResetCalcFlag = True
            End If
            If FlagGroup And SeqRepeat = GCount Then
                ResetCalcFlag = True
            End If
        End If

        If DivOn = True And ResetCalcFlag Then
            DivOnPrev = True
        Else
            DivOnPrev = False
        End If
        If MulOn = True And ResetCalcFlag Then
            MulOnPrev = True
        Else
            MulOnPrev = False
        End If
        If AddOn = True And ResetCalcFlag Then
            AddOnPrev = True
        Else
            AddOnPrev = False
        End If
        If SubOn = True And ResetCalcFlag Then
            SubOnPrev = True
        Else
            SubOnPrev = False
        End If


        'formula in history

        'end mti Validation changes


        '     Ag_rule$ = saveAg_rule$


        'JRS 08/12/02

        'jrs 2017/08/28 added And (Indicator_IfsVar(iIndex) > "" Or Indicator_TargetValue(iIndex) = -9999)
        'jrs 2008/10/28
        If Session("SDGEstimateForInit") And (Indicator_IfsVar(IIndex) > "" Or IsDBNull(Indicator_TargetValue(IIndex))) Then
            Curval(hCOUNTE) = PlotSDG(1, hCOUNTE)
            Session("SDGCurval") = Curval
        End If

        'jrs 2011/11/24
        If Not Ag_On_Group Then
            ReDim Preserve Session("MultCountries")(hCOUNTE)
            Session("MultCountries")(hCOUNTE) = Countries(0)
        End If

        'jrs 2017/07/12
        'jrs 2017/07/20
        Dim iLst As Integer
        If Indicator_IfsVar(IIndex) = "" Then
            iLst = LastYearHistory - FirstYearHistory + 1
        Else
            iLst = LastYearHistory - FirstYearHistory
        End If
        For j = iLst To 1 Step -1
            If IsNumeric(PLOTSDGH(j, hCOUNTE)) Then
                Session("LastHistVal")(hCOUNTE) = PLOTSDGH(j, hCOUNTE)
                Session("LastHistYear")(hCOUNTE) = j + FirstYearHistory - 1
                Exit For
            End If
        Next


        Exit Sub

FileProblem:
        Dim Msg As String
        Msg$ = "Sorry. An error has arisen during an attempt to read " + IndicatorDesc(IIndex) 'VarName$
        Msg$ = Msg$ + " from " + TableName$ + " - the variable or data for it "
        Msg$ = Msg$ + "doesn't exist in the file.  Please try another variable."
        'jrs 2011/09/28
        Session("MsgParent") = "frm_SDG.aspx"
        If Session("SDGGroups") = True Then
            Session("MsgParent") = Session("MsgParent") & "?Group=" & cmb_Dim1.SelectedValue
        Else
            If Not IsNothing(Request.QueryString("Country")) Then
                Dim C As String
                C = Request.QueryString("Country")
                Session("MsgParent") = Session("MsgParent") & "?Country=" & C
            End If
        End If

        LoadMessage(Msg, Session("Message"))
        'jam 2021/12/12
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")

        'jam 2021/12/12
        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


        vurl = FriendlyUrl.Href("~/frm__Message.aspx")

        Response.Redirect(vurl)



    End Sub

    'jrs 2013/09/12
    Public Sub History_FilldataComp(ByVal sVar As String, ByVal sDim1 As String, ByVal sDim2 As String, ByVal sDim3 As String)
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        gLoadForDisplayHis(sVar, sDim1, -1, sDim2, -1, sDim3, -1,
        FlagGroup, 1, PLOTSDGH, 1, FirstYearSDG, LatestYear,
        False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
        Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
        Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
        Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite"))

        Session("SDGPLOTSDGH") = PLOTSDGH
    End Sub

    Private Sub Find_Group_Base_Latest(ByVal valtbl As DataTable, ByVal agcount%)

        Dim reg As New RegressionObject
        Dim x#, y#

        Dim tblDriver As DataTable, idx As Integer, j As Integer, VarVal As Object
        Dim DrvFld As String, DrvVal As Object

        Dim OnlyYear%, OnlyVal, ShiftVal

        Dim pt%, k%

        Dim YearVal%
        Dim VarValBase, VarValLatest

        'mti SDG june 28, 2005 data
        'EarliestYearWithData = 9999
        sgrpEarliestYearWithData% = 9999

        'LatestYear = FirstYearForecast  'comment this line after discussion, if necessary 'mti March 23, 2005

        If Indicator_DriverTbl(IIndex) <> "" Then
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM " & Indicator_DriverTbl(IIndex), "Country", tblDriver, Session("sPwdSDG"))
        End If

        For k = 0 To agcount - 1
            grCntryBaseVal(k) = Nothing
            grCntryLYVal(k) = Nothing
            idx = 0
            With reg
                .Init()
                .Degree = 1 'Degree
                pt = 0
                For j = 0 To Session("valtbl").Columns.Count - 1
                    YearVal = Val(Session("valtbl").Columns(j).Caption)
                    Select Case YearVal
                        Case Indicator_BaseYear(IIndex)
                            VarValBase = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j).Caption) 'not being used, only regression used
                            VarVal = VarValBase

                        Case Is > 0
                            LatestYear = YearVal
                            VarValLatest = SeekValue(Session("valtbl"), Countries(k), Session("valtbl").Columns(j).Caption)
                            VarVal = VarValLatest
                            'mti June 2005; Data for 2000
                            'If YearVal = FirstYearForecast Then grCntryLYVal(k) = VarVal
                        Case Else
                            VarVal = Nothing
                    End Select

                    If IsNumeric(VarVal) Then
                        'mti SDG june 28, 2005 data
                        If YearVal < sgrpEarliestYearWithData Then
                            'EarliestYearWithData = YearVal
                            sgrpEarliestYearWithData = YearVal
                        End If

                        If YearVal > Indicator_BaseYear(IIndex) - 5 Then
                            x# = YearVal - FirstYearSDG + 1
                            y# = CDbl(VarVal)  '1
                            'jrs 2017/07/24
                            Select Case Session("EstBaseYrReg")
                                Case "LIN"
                                    pt = pt + 1
                                Case "LOG"
                                    x# = Log(x#)
                                    pt = pt + 1
                                Case "LOGISTIC"
                                    If VarVal <> 0 Then
                                        If Session("SaturationValue") / VarVal - 1 > 0 Then
                                            y# = Log(CDbl(Session("SaturationValue") / VarVal - 1))
                                            pt = pt + 1
                                        End If
                                    End If
                                Case "QUAD"
                                    x# = x# ^ 2
                                    pt = pt + 1
                                Case "CUBIC"
                                    x# = x# ^ 3
                                    pt = pt + 1
                                Case "EXP"
                                    x# = Exp(x#)
                                    pt = pt + 1
                            End Select

                            .XYAdd(x#, y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                            OnlyYear = YearVal
                            OnlyVal = VarVal
                            'jrs 2017/07/24 pt = pt + 1
                        End If
                        'mti SDG june 28, 2005 data end add condition
                    End If

                Next j

                '            If Not (IsNull(VarValBase) And VarValBase = Empty) Then
                '                grCntryBaseVal(k) = VarValBase
                '            Else
                If pt >= 2 Then 'regression
                    x# = CDbl(Indicator_BaseYear(IIndex)) - FirstYearSDG + 1
                    'jrs 2017/07/24
                    Select Case Session("EstBaseYrReg")
                        Case "LIN"
                            grCntryBaseVal(k) = reg.RegVal(CDbl(x#))
                        Case "LOG"
                            grCntryBaseVal(k) = reg.RegVal(CDbl(Log(x#)))
                        Case "LOGISTIC"
                            grCntryBaseVal(k) = Session("SaturationValue") / (Exp(reg.RegVal(CDbl(x#))) + 1)
                        Case "QUAD"
                            grCntryBaseVal(k) = reg.RegVal(CDbl(x# ^ 2))
                        Case "CUBIC"
                            grCntryBaseVal(k) = reg.RegVal(CDbl(x# ^ 3))
                        Case "EXP"
                            grCntryBaseVal(k) = reg.RegVal(CDbl(Exp(x#)))
                    End Select

                    'mti 10/27/05
                    regIntercept(k) = reg.Coeff(0)
                    regSlope(k) = reg.Coeff(1)
                    'mti 10/27/05
                Else 'cross-sectional
                    If Indicator_DriverTbl(IIndex) <> "" Then
                        DrvFld$ = Trim(Str(Indicator_BaseYear(IIndex)))
                        DrvVal = SeekValue(tblDriver, Countries(k), DrvFld$)  'GDP of that particular year

                        If Not (IsNumeric(DrvVal)) Then
                            DrvFld$ = Trim(Str((Val(DrvFld$) + 1))) 'go one more year, 1991
                            DrvVal = SeekValue(tblDriver, Countries(k), DrvFld$)
                        End If

                        If Not (IsNumeric(DrvVal)) Then
                            DrvFld$ = Trim(Str((Val(DrvFld$) + 2))) 'go two more year, 1992
                            DrvVal = SeekValue(tblDriver, Countries(k), DrvFld$)
                        End If


                        If Not (IsNumeric(DrvVal)) Then
                            DrvFld$ = Trim(Str(Indicator_BaseYear(IIndex)))
                            DrvFld$ = Trim(Str((Val(DrvFld$) - 1))) 'go back one year, 1989
                            DrvVal = SeekValue(tblDriver, Countries(k), DrvFld$)
                        End If


                        'mti January 05, 2005 (2)
                        If IsNumeric(DrvVal) And Indicator_Function(IIndex) <> "" Then
                            '                        DrvVal = DrvVal / 1000
                            If Indicator_Conversion(IIndex) = "Div" And Indicator_ConversionNumber(IIndex) <> 0 Then
                                DrvVal = DrvVal / Indicator_ConversionNumber(IIndex)
                            End If

                            'use SDG database for tblfunction name
                            'Call XYTABL("GDP/Capita (PPP) Versus Income Below One Dollar per Day (2000)", GDPval, grCntryBaseVal(k))
                            Call XYTABL(Indicator_Function(IIndex), DrvVal, grCntryBaseVal(k))

                            If IsNumeric(grCntryBaseVal(k)) Then
                                grCntryBaseVal(k) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), grCntryBaseVal(k)))
                            End If


                        End If
                    End If  'Indicator_DriverTbl(IIndex) <> ""
                End If
                '            End If

                '            If Not (IsNull(VarValLatest) And VarValLatest = Empty) Then
                '                grCntryLYVal(k) = VarValLatest
                '            Else
                'latest year
                If pt >= 2 Then 'regression
                    'mti June 2005- comment these - data for 2000
                    x# = CDbl(FirstYearForecast) - FirstYearSDG + 1 'LatestYear
                    'jrs 2017/07/24
                    Select Case Session("EstBaseYrReg")
                        Case "LIN"
                            grCntryLYVal(k) = reg.RegVal(CDbl(x#))
                        Case "LOG"
                            grCntryLYVal(k) = reg.RegVal(CDbl(Log(x#)))
                        Case "LOGISTIC"
                            grCntryLYVal(k) = Session("SaturationValue") / (Exp(reg.RegVal(CDbl(x#))) + 1)
                        Case "QUAD"
                            grCntryLYVal(k) = reg.RegVal(CDbl(x# ^ 2))
                        Case "CUBIC"
                            grCntryLYVal(k) = reg.RegVal(CDbl(x# ^ 3))
                        Case "EXP"
                            grCntryLYVal(k) = reg.RegVal(CDbl(Exp(x#)))
                    End Select
                Else 'cross-sectional
                    If Indicator_DriverTbl(IIndex) <> "" Then
                        DrvFld$ = "MostRecent"
                        For j = 0 To tblDriver.Columns.Count - 1
                            YearVal = Val(tblDriver.Columns(j).Caption)
                            If YearVal = LatestYear Then
                                DrvFld$ = Trim(Str(LatestYear))
                                Exit For
                            End If
                        Next j
                        DrvVal = SeekValue(tblDriver, Countries(k), DrvFld$)  'GDP of that particular year
                        '                    DrvVal = DrvVal / 1000
                        If IsNumeric(DrvVal) And Indicator_Conversion(IIndex) = "Div" And Indicator_ConversionNumber(IIndex) <> 0 Then
                            DrvVal = DrvVal / Indicator_ConversionNumber(IIndex)
                        End If

                        'mti January 05, 2005 (2)
                        If IsNumeric(DrvVal) And Indicator_Function(IIndex) <> "" Then

                            'Call XYTABL("GDP/Capita (PPP) Versus Income Below One Dollar per Day (2000)", GDPval, grCntryLYVal(k))
                            Call XYTABL(Indicator_Function(IIndex), DrvVal, grCntryLYVal(k))

                            If IsNumeric(grCntryLYVal(k)) Then
                                grCntryLYVal(k) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), grCntryLYVal(k)))
                            End If


                        End If
                    End If 'Indicator_DriverTbl(IIndex) <> ""
                End If
                '            End If

                'mti 10/27/05
                regShift(k) = 0
                'mti 10/27/05 end

                '---------if one point exists priviledge data----------
                If pt = 1 And IsNumeric(grCntryBaseVal(k)) And
                    IsNumeric(grCntryLYVal(k)) Then

                    .Init() 'mti 26 March 2005

                    x# = CDbl(Indicator_BaseYear(IIndex))
                    y# = CDbl(grCntryBaseVal(k))
                    .XYAdd(x#, y#)

                    x# = CDbl(LatestYear)  'LatestYear
                    y# = CDbl(grCntryLYVal(k))
                    .XYAdd(x#, y#)

                    x# = CDbl(OnlyYear)
                    y# = .RegVal(CDbl(x#))
                    ShiftVal = OnlyVal - y# 'ShiftVal '.RegVal(CDbl(X#))
                    'mti 10/27/05
                    regShift(k) = ShiftVal
                    'mti 10/27/05 end
                    '                ShiftVal = OnlyVal - .RegVal(CDbl(X#))

                    'mti March 26, 2005
                    'X# = CDbl(Indicator_BaseYear(IIndex))
                    'BaseVal(j) = grCntryBaseVal(k) + ShiftVal '.RegVal(CDbl(X#))
                    grCntryBaseVal(k) = grCntryBaseVal(k) + ShiftVal '.RegVal(CDbl(X#))

                    'X# = CDbl(LatestYear)
                    'Curval(j) = grCntryLYVal(k) + ShiftVal '.RegVal(CDbl(X#))
                    grCntryLYVal(k) = grCntryLYVal(k) + ShiftVal '.RegVal(CDbl(X#))


                End If
                '---------end if one point exists priviledge data----------

                'jrs 2017/07/24
                If Session("ShowRegVals") And pt > 1 Then
                    'jrs 2017/07/24
                    Session("sRegFormula") = "y = " & CStr(Round(.Coeff(0), 2))
                    Select Case Session("EstBaseYrReg")
                        Case "LIN"
                            If .Coeff(1) > 0 Then
                                Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                            Else
                                Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                            End If
                            For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                                x# = CDbl(rr%)
                                PLOTSDGHR(rr%, 1) = .RegVal(CDbl(x#))  '+ TC1
                            Next
                        Case "LOG"
                            If .Coeff(1) > 0 Then
                                Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " LN(Year-1959)"
                            Else
                                Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " LN(Year-1959)"
                            End If
                            For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                                x# = CDbl(rr%)
                                PLOTSDGHR(rr%, 1) = .RegVal(CDbl(Log(x#)))
                            Next
                        Case "LOGISTIC"
                            Session("sRegFormula") = CStr(Round(.Coeff(0), 2))

                            If .Coeff(1) > 0 Then
                                Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                            Else
                                Session("sRegFormula") = Session("sRegFormula") & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                            End If
                            Session("sRegFormula") = "y = " & CStr(Session("SaturationValue")) & "/(exp(" & Session("sRegFormula") & ") + 1)"
                            For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                                x# = CDbl(rr%)
                                PLOTSDGHR(rr%, 1) = Session("SaturationValue") / (Exp(.RegVal(CDbl(x#))) + 1)
                            Next
                        Case "QUAD"
                            If .Coeff(1) > 0 Then
                                Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^2"
                            Else
                                Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^2"
                            End If
                            For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                                x# = CDbl(rr%)
                                PLOTSDGHR(rr%, 1) = .RegVal(CDbl(x# ^ 2))  '+ TC1
                            Next
                        Case "CUBIC"
                            If .Coeff(1) > 0 Then
                                Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^3"
                            Else
                                Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^3"
                            End If
                            For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                                x# = CDbl(rr%)
                                PLOTSDGHR(rr%, 1) = .RegVal(CDbl(x# ^ 3))  '+ TC1
                            Next
                        Case "EXP"
                            If .Coeff(1) > 0 Then
                                Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " EXP(Year-1959)"
                            Else
                                Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " EXP(Year-1959)"
                            End If
                            For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                                x# = CDbl(rr%)
                                PLOTSDGHR(rr%, 1) = .RegVal(CDbl(Exp(x#)))  '+ TC1
                                If PLOTSDGHR(1, 1) > 0 And PLOTSDGHR(rr%, 1) < 0 Then
                                    PLOTSDGHR(rr%, 1) = DBNull.Value
                                End If
                            Next
                    End Select
                Else
                    For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                        PLOTSDGHR(rr, 1) = DBNull.Value
                    Next
                    Session("sRegFormula") = ""
                End If

                Session("SDGPLOTSDGHR") = PLOTSDGHR


            End With

            If IsNumeric(grCntryBaseVal(k)) Then
                grCntryBaseVal(k) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), grCntryBaseVal(k)))
            End If

            If IsNumeric(grCntryLYVal(k)) Then
                grCntryLYVal(k) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), grCntryLYVal(k)))
            End If


        Next k

    End Sub

    Private Function InputSDGForecast(ByVal IFsVarNOper$, ByVal Country_Group$, ByVal Dim2Name$, ByVal Dim3Name$) As Boolean 'jrs 2006/05/17 Dim3
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'jrs 2006/09/25
        Dim IfsPars As New myParameters
        IfsPars.LoadPars(Session("currencyVal"), Session("ShortSce"), Session("RunFiles"),
        Session("Ifs_MDB2Active"), Session("IFsCoVaTrASeries_MDB"), Session("IFsHistSeries_MDB"),
        Session("PutFdrvRun"), Session("CurrencySym"))

        Dim DoingHistory As Boolean, sPickHRegion As String, iPickRegionCounter As Integer
        Dim NVal As Object, SELNLen As Integer, PosMul As Integer
        Dim MainSEL As Object, fileIdx As Integer, SE As String
        Dim VarVal As Object

        'jrs 2017/06/19
        Dim sDim2 As String

        'mti June 01, 2005 - Changed Arguments in the definition (added Byval to IFsVarNOper$

        'mti Validation changes
        Dim bScaleInvalid As Boolean
        Dim AfterOper$
        InputSDGForecast = False
        DoingHistory = False

        sPickHRegion = Country_Group$
        iPickRegionCounter = 0

        'jrs 2017/02/28
        If Not FlagGroup Then
            ReDim Countries(1)
            Countries(0) = getFirstCountryInRegion(IFsRegs.Item(Country_Group$).Index, Session("IFs_MDB2Active"))

            'jrs 2017/08/30
            ReDim Preserve Session("MultCountries")(1)
            Session("MultCountries")(1) = Countries(0)
        End If

        CalcType = "None"
        OperStr$ = ""
        'jrs 2017/02/06
        OperStr2 = ""
        NVal = 0
        bScaleInvalid = False
        SELNLen = Len(IFsVarNOper$)
        PosMul = InStr(1, IFsVarNOper$, "/")
        If PosMul > 0 Then 'Check for Division
            'DivOn = true
            AfterOper$ = Mid(IFsVarNOper$, PosMul + 1)
            NVal = Val(Mid(IFsVarNOper$, PosMul + 1))  'Could be number or another var
            'If NVal > 0 Then 'NumVal OK
            IFsVarNOper$ = Left$(IFsVarNOper$, PosMul - 1) 'exclude Operator
            CalcType = "DivOn"
            OperStr$ = "/"
            'Else
            'bScaleInvalid = True
            'End If
        Else 'jrs 2017/02/06
            PosMul = InStr(1, Dim2Name$, "+")
            If PosMul > 0 Then 'Check for Sum with other dimension
                OperStr2 = "+"
            End If
        End If

        PosMul = InStr(1, IFsVarNOper$, "%")
        If PosMul > 0 Then 'Check for Division
            'MulOn = True
            AfterOper$ = Mid(IFsVarNOper$, PosMul + 1)
            NVal = Val(Mid(IFsVarNOper$, PosMul + 1))
            '        If NVal > 0 Then 'NumVal OK
            IFsVarNOper$ = Left$(IFsVarNOper$, PosMul - 1) 'include Operator
            CalcType = "PerOn"
            OperStr$ = "%"
            '        Else
            '            bScaleInvalid = True
            '        End If
        End If

        PosMul = InStr(1, IFsVarNOper$, "*")
        If PosMul > 0 Then 'Check for Division
            'MulOn = True
            AfterOper$ = Mid(IFsVarNOper$, PosMul + 1)
            NVal = Val(Mid(IFsVarNOper$, PosMul + 1))
            '        If NVal > 0 Then 'NumVal OK
            IFsVarNOper$ = Left$(IFsVarNOper$, PosMul - 1) 'include Operator
            CalcType = "MulOn"
            OperStr$ = "*"
            '        Else
            '            bScaleInvalid = True
            '        End If
        End If

        PosMul = InStr(1, IFsVarNOper$, "+")
        If PosMul > 0 Then 'Check for Division
            'AddOn = True
            AfterOper$ = Mid(IFsVarNOper$, PosMul + 1)
            NVal = Val(Mid(IFsVarNOper$, PosMul + 1))
            '        If NVal > 0 Then 'NumVal OK
            IFsVarNOper$ = Left$(IFsVarNOper$, PosMul - 1) 'include Operator
            CalcType = "AddOn"
            OperStr$ = "+"
            '        Else
            '            bScaleInvalid = True
            '        End If
        End If

        PosMul = InStr(1, IFsVarNOper$, "-")
        If PosMul > 0 Then 'Check for Division
            'SubOn = True
            AfterOper$ = Mid(IFsVarNOper$, PosMul + 1)
            NVal = Val(Mid(IFsVarNOper$, PosMul + 1))
            '        If NVal > 0 Then 'NumVal OK
            IFsVarNOper$ = Left$(IFsVarNOper$, PosMul - 1) 'include Operator
            CalcType = "SubOn"
            OperStr$ = "-"
            '        Else
            '            bScaleInvalid = True
            '        End If
        End If

        Dim Msg As String
        If bScaleInvalid = True Then
            Msg = "Scaling parameter not valid."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If


        SELSDG = 0
        SELSDG = IfsVars.Item(IFsVarNOper$).Index
        'SEL = chk


        If SELSDG <= 0 Then 'If VarName$ not found
            Msg$ = IFsVarNOper$ + " is not a valid selection."
            Msg$ = Msg$ + "  Please try again."

            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")

            'revert picking validation to false here?
        End If

        MainSEL = SELSDG

        'jrs 2017/02/06
        If VDim3(SELSDG) > 0 And Dim3Name = "" Then
            Dim3Name = "Total"
        End If

        '    cmb_DIM1.Visible = False
        '
        '    cmb_VarName.Text = IFsVarNOper$ + OperStr$
        '
        '    cmb_VarName_Click  'need this to load all necessary info

        '    cmb_DIM1.Text = Country_Group$

        FirstYearSDG% = FirstYearForecast
        'jrs 2005/09/05
        Session("SDGFilesSelected") = 0
        For fileIdx = 0 To lstFiles.Items.Count - 1

            SELSDG = MainSEL
            If lstFiles.Items(fileIdx).Selected Then
                'jrs 2017/06/19
                sDim2 = Dim2Name

                FileNum = fileIdx
                COUNTESDG = COUNTESDG + 1
                Session("COUNTE") = COUNTESDG
                'mti July 2006 start - to fix the years after history plus forecast -> SDG
                FinYearSDG(COUNTESDG) = FindFinYear(FileNamesSDG(FileNum))
                'mti July 2006 end

                Session("SDGFilesSelected") = Session("SDGFilesSelected") + 1

                'FileNum = fileIdx
                'HistoricPlusForecast = False 'True
                'CalcRunning = False
                'Ag_On_Group = False
                'Ag_On_Region = False

                'If VDim2(SELSDG) > 0 Then
                '    FillData(Dim2Name$, Country_Group$) 'to match with cmb_dim1.select in varsel
                'Else 'single dim: assumed no nodim
                '    FillData(Country_Group$, Dim2Name$) 'VFDim2$ 'Dim2Name$ 'VFDim2$
                'End If


                If Len(OperStr$) > 0 Then
                    Dim ff As New Formula
                    Dim sFormulaDet As String
                    If OperStr = "%" Then
                        If OperStr2 = "" Then
                            sFormulaDet = "a/b*100"
                        Else 'jrs 2017/02/06
                            sFormulaDet = "(a+c)/b*100"
                        End If
                    Else
                        'jrs 2017/02/06
                        If Indicator_Transform(IIndex) = "Absolute" Then
                            sFormulaDet = "a*b/100"
                        ElseIf Indicator_Transform(IIndex) = "Per1000" Then
                            sFormulaDet = "a/b*1000"
                            PosMul = InStr(1, AfterOper$, "*")
                            If PosMul > 0 Then
                                AfterOper$ = Trim(Mid(AfterOper$, 1, PosMul - 1))
                            End If
                        ElseIf Indicator_Transform(IIndex) = "InvCDALF" Then
                            sFormulaDet = "a" & Trim(OperStr) & "(1-c)+b"
                            PosMul = InStr(AfterOper$, "+")
                            If PosMul > 0 Then
                                AfterOper$ = Mid(AfterOper, PosMul + 1)
                            End If
                        ElseIf Indicator_Transform(IIndex) = "LossIndex" Then
                            sFormulaDet = "(a+b+c)/3"
                        Else
                            sFormulaDet = "a" & Trim(OperStr) & "b"
                        End If
                    End If
                    ff.init(sFormulaDet)
                    ff.disVar.setDataDetS("a", IFsVarNOper$ & "[" & Trim(Str(FileNum)) & "] ") 'jrs 2006/05/30 separate dims from vars
                    ff.disVar.setDataDetS("b", AfterOper$ & "[" & Trim(Str(FileNum)) & "] ")

                    'jrs 2017/02/06
                    If OperStr2 > "" Then
                        ff.disVar.setDataDetS("c", IFsVarNOper$ & "[" & Trim(Str(FileNum)) & "] ")
                        PosMul = InStr(1, Dim2Name$, "+")
                        ff.disVar.setDataDet2S("c", Trim(Mid(Indicator_IfsVarDim2(IIndex), 1, PosMul - 1)))
                        ff.disVar.setDataDet2S("a", Trim(Mid(Indicator_IfsVarDim2(IIndex), PosMul + 1)))
                        'jrs 2017/06/19 Dim2Name$ = ""
                        sDim2 = ""
                    End If
                    'jrs 2017/02/07
                    If Indicator_Transform(IIndex) = "InvCDALF" Then
                        ff.disVar.setDataDetS("c", "CDALF[" & Trim(Str(FileNum)) & "] ")
                    End If
                    If Indicator_Transform(IIndex) = "LossIndex" Then
                        ff.disVar.setDataDetS("c", "AGLOSSTRANS[" & Trim(Str(FileNum)) & "] ")
                        ff.disVar.setDataDet1S("c", Country_Group$)
                        ff.disVar.setDataDet2S("c", Indicator_IfsVarDim2(IIndex))
                    End If

                    'mti July 2006 -start - to accomodate formulas with second dimensions , e.g., in gender goal
                    If Indicator_IfsVarDim2(IIndex) <> "" And Indicator_Var2Dim2(IIndex) <> "" Then
                        ff.disVar.setDataDet1S("a", Country_Group$)
                        ff.disVar.setDataDet2S("a", Indicator_IfsVarDim2(IIndex))
                        ff.disVar.setDataDet1S("b", Country_Group$)
                        ff.disVar.setDataDet2S("b", Indicator_Var2Dim2(IIndex))
                        'jrs 2020/06/25
                        If Indicator_IfsVarDim3(IIndex) <> "" Then
                            ff.disVar.setDataDet3S("a", Indicator_IfsVarDim3(IIndex))
                            ff.disVar.setDataDet3S("b", Indicator_Var2Dim3(IIndex))
                        End If

                        gLoadComp(COUNTESDG, sFormulaDet, IFsVarNOper$ & Trim(OperStr) & AfterOper$, "", "",
                        "", "", -1, FlagGroup, FileNum, True, False, ff, False, False, -1, False, Session("bComputation"),
                        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
                        Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                        Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
                        'jrs 2013/09/13 change parameter History Plus Forecast to True
                    Else
                        'jrs 2017/02/07
                        If Indicator_Var2Dim2(IIndex) = "World" Then
                            ff.disVar.setDataDet1S("a", Country_Group$)
                            ff.disVar.setDataDet1S("b", Indicator_Var2Dim2(IIndex))
                            Country_Group$ = ""
                        End If
                        'jrs 2017/06/19 changed from Dim2Name to sDim2
                        gLoadComp(COUNTESDG, sFormulaDet, IFsVarNOper$ & Trim(OperStr) & AfterOper$, Country_Group$, sDim2,
                        Dim3Name$, "", -1, FlagGroup, FileNum, True, False, ff, False, False, -1, False, Session("bComputation"),
                        Session("FirstYear"), Session("LastYear"), Session("LastYearForecast"), Session("bLoadForDisplaySuccess"), Me, Session("FinYear"),
                        Session("FileforDisplay"), IfsPars, Session("PLOT"), Session("aFormulas"), Session("bFillHisOnlyHoles"),
                        Session("StaYearHis")(Session("COUNTE")), Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("ActiveBase"), Session("mnuSQLite"))
                        'jrs 2013/09/13 change parameter History Plus Forecast to True

                        'jrs 2017/02/07
                        If Indicator_Var2Dim2(IIndex) = "World" Then
                            Country_Group$ = cmb_Dim1.Text
                            FlagGroup = (Session("SDGGroups"))
                        End If

                    End If



                    'gLoadComp COUNTESDG, sFormulaDet, IFsVarNOper$ & Trim(OperStr) & AfterOper$, Country_Group$, Dim2Name$, _
                    'Dim3Name$(, "", -1, FlagGroup, FileNum, False, False, ff, False, False)

                    'mti July 2006 - end
                    Dim i As Integer
                    For i = 1 To Session("LastYear") - FirstYearSDG + 1
                        PlotSDG(i, COUNTESDG) = Session("PLOT")(i + LastYearHistory - FirstYearHistory, COUNTESDG) 'jrs 2013/09/13 added + LastYr - FirstYr
                    Next i
                    'jrs 2013/09/13
                    If Trim(Indicator_SDGHistAnalog(IIndex)) = "NO" Or Trim(Indicator_SDGHistAnalog(IIndex)) = "" Then 'jrs 2017/02/06 added or xxx = ""
                        For i = 1 To LastYearHistory - FirstYearHistory
                            PLOTSDGH(i, COUNTESDG) = Session("PLOT")(i, COUNTESDG)
                        Next i
                        If FlagGroup Then
                            FirstYearSDG = FirstYearHistory
                            'jrs 2017/08/30 in second file LastYear was set to 2015, it should be 2100 Session("LastYear") = LastYearHistory
                        End If
                    End If

                Else
                    'jrs 2017/06/19 changed from Dim2Name to sDim2
                    If Session("mnuSQLite") = "True" Then
                        gLoadForDisplayNET("", SELSDG, Country_Group$, -1, sDim2, -1, Dim3Name$, -1, "", FileNum, True, True,
                    FlagGroup, 1, PlotSDG, COUNTESDG, 1, FirstYearSDG, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"),
                    Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
                    Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    Else
                        gLoadForDisplay("", SELSDG, Country_Group$, -1, sDim2, -1, Dim3Name$, -1, "", FileNum, True, True,
                        FlagGroup, 1, PlotSDG, COUNTESDG, 1, FirstYearSDG, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"),
                        Me, Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
                        Session("IFs_MDB2Active"), Session("CurrencyVal"))
                    End If
                End If
                Gridput()
            End If ' file selected
        Next

        SELSDG = MainSEL

        VarName$ = IFsVarNOper$
        Dim VName As String

        If Trim(Indicator_SDGHistAnalog(IIndex)) = "" And Indicator_Transform(IIndex) = "" And Not IsDBNull(VHistoricAnalog(SELSDG)) Then 'jrs 2017/02/06 added and trans = "" and not isnull
            TableName$ = "Series" + VHistoricAnalog(SELSDG)
            VName = V(SELSDG)

            'jrs 2017/02/06
            If VHistoricAnalog(SELSDG) = "IFSVARHISTANA" Then
                'JRS 2017/07/17 CHANGED FROM COUNTEMDG to 1, history always on 1 position
                'jrs 2017/06/19 changed from Dim2Name to sDim2
                gLoadForDisplayHis(V(SELSDG), Country_Group$, -1, sDim2, -1, Dim3Name$, -1, FlagGroup, 1, PLOTSDGH, 1,
                FirstYearHistory, Session("LastYear"), False, True, Session("bFillHis"), Session("buseAllHis"), Session("FinYearHis")(Session("COUNTE")),
                Session("bLoadForDisplaySuccess"), Me, Session("FinYear"), Session("LastYear"),
                Session("LastYearForecast"), Session("bComputation"), "", "", IfsPars, Session("bFillHisOnlyHoles"),
                Session("bCompAll"), Session("dataAll"), Session("StaYearHis"), Session("mnuSQLite")) 'False, True, False, "", False, False, -1, False, -1)
                TableName$ = "Series"
            ElseIf InStr(TableName$, ",") > 0 Then
                Dim tblS() As String
                tblS = Split(TableName$, ",")
                For i = 0 To UBound(tblS)
                    If InStr(tblS(i), Dim2Name$) > 0 Or i = UBound(tblS) Then
                        TableName$ = "Series" & tblS(i)
                        Exit For
                    End If
                Next i
            End If

        Else
            TableName$ = "Series" + Trim(Indicator_SDGHistAnalog(IIndex))
            VName = Trim(Indicator_SDGHistAnalog(IIndex))
        End If
        Session("SDGTableName") = TableName

        '    FirstYear = FirstYearHistory
        FirstYearSDG = FirstYearHistory
        Session("LastYear") = LastYearHistory
        If Not FlagGroup Then
            DimPoint = gGetDimIdx(1, Country_Group$, True, Me)
        Else
            GroupPoint = gGetGroupIdx(Country_Group$, True, Me)
            'mti July 2006 -start
            'save the forecast group member names in an SDG form array (they might be different from history member names)
            'index starts as 0 like Countries Array in this form
            Dim gmcnt As Integer
            For gmcnt = 0 To Session("gCountGroupMembers") - 1
                SDGGrpMmbrRun(gmcnt) = Session("gGroupMembrs")(gmcnt + 1)
            Next gmcnt
            gLoadGroupMembersHis(Country_Group$, GroupCodes(GroupPoint%), Session("gCountGroupMembers"), Session("gGroupMembrs"), Session("IFs_MDB2Active"))
            'mti July 2006 - end
        End If
        'jrs 2013/09/13
        If TableName$ = "SeriesFORMULA" Then
            History_FilldataComp(VName, Country_Group$, Dim2Name$, Dim3Name$)
        ElseIf TableName$ <> "SeriesNO" And TableName$ <> "SeriesALLNULLS" And TableName$ <> "Series" Then  'Already loaded 'jrs 2017/02/03 added And Tablename <> SeriesALLNULLS and tablename <> Series
            History_Filldata()
        End If

        '    FirstYear = FirstYearForecast
        Session("LastYear") = Session("LastYearForecast")

        'jrs 2017/05/31
        Dim hCOUNTE As Integer
        hCOUNTE = COUNTESDG / Session("SDGFilesSelected")
        For j = LastYearHistory - FirstYearHistory To 1 Step -1
            If IsNumeric(PLOTSDGH(j, hCOUNTE)) Then
                Session("LastHistVal")(hCOUNTE) = PLOTSDGH(j, hCOUNTE)
                Session("LastHistYear")(hCOUNTE) = j + FirstYearHistory - 1
                Exit For
            End If
        Next

        'if calc in history
        'check aggregation; take agrule from datadict

        InputSDGForecast = True

        '    Else 'cmb_VarName.ListIndex <> -1

        'jrs 2006/08/03
        Session("FinYearSDG") = FinYearSDG
        Session("SDGGrpMmbrRun") = SDGGrpMmbrRun

    End Function

    Public Sub XYTABL(ByVal TFName, ByVal IndVar, ByRef DepVar)
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '   Identify Bivariate Function Accessed (no longer only table functions)
        '   --------------------------------------------------------------------

        On Error GoTo BivariateFuncError
        Dim Msg As String, FuncInd As Integer, iloop As Integer, IXY As Integer, Numer As Object, Denom As Object
        If Not IsNumeric(IndVar) Then
            Msg$ = "In a call to XYTABL for the computation of " + TFName +
                " the Independent Variable is NOT Numeric.  We will exit the subroutine."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        FuncInd% = 0
        For iloop = 1 To TFCount
            '        If TFunName$(ILoop) = TFName$ Then
            If FuncName$(iloop) = TFName Then
                FuncInd% = iloop
                Exit For
            End If
        Next iloop
        If FuncInd% = 0 Then
            Msg$ = "Fatal Error:  Table function " + TFName + " not found."
            LoadMessage(Msg, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If

        '   Analytic function - compute dependent
        '   -------------------------------------

        If Session("TFuncPoints")(FuncInd%) = 0 Then
            DepVar = Session("APoint")(FuncInd%) + Session("BPoints")(FuncInd%, 1) * IndVar _
                    + Session("BPoints")(FuncInd%, 2) * IndVar ^ 2 + Session("BPoints")(FuncInd%, 3) * IndVar ^ 3 _
                    + Session("BPoints")(FuncInd%, 4) * System.Math.Log(IIf(IndVar <= 0, 1, IndVar)) _
                    + Session("BPoints")(FuncInd%, 5) * IndVar ^ Session("BPoints")(FuncInd%, 6) _
                    + Session("BPoints")(FuncInd%, 7) * System.Math.Exp(Session("BPoints")(FuncInd%, 8) * IndVar)
            Exit Sub
        End If

        '   Table function - compute dependent variable
        '   -------------------------------------------

        'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, 1)) <= 0 Then
        If (IndVar - Session("XPoints")(FuncInd%, 1)) <= 0 Then
            'JRS 10/20/03 DepVar = YPoints(FuncInd%, 1)
            DepVar = Session("YPoints")(FuncInd%, 1)
            Exit Sub 'If below range, set to first point
        End If
        'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
        If (IndVar - Session("XPoints")(FuncInd%, Session("TFuncPoints")(FuncInd%))) >= 0 Then
            'JRS 10/20/03 DepVar = YPoints(FuncInd%, TFuncPoints(FuncInd%))
            DepVar = Session("YPoints")(FuncInd%, Session("TFuncPoints")(FuncInd%))
            Exit Sub 'If above range, set to top point
        End If
        For IXY = 2 To Session("TFuncPoints")(FuncInd%)
            'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, IXY)) <= 0 Then
            If (IndVar - Session("XPoints")(FuncInd%, IXY)) <= 0 Then
                'JRS 10/20/03 Numer = (YPoints(FuncInd%, IXY) - YPoints(FuncInd%, IXY - 1))
                Numer = (Session("YPoints")(FuncInd%, IXY) - Session("YPoints")(FuncInd%, IXY - 1))
                'JRS 10/20/03 Numer = Numer * (IndVar - XPoints(FuncInd%, IXY - 1))
                Numer = Numer * (IndVar - Session("XPoints")(FuncInd%, IXY - 1))
                'JRS 10/20/03 Denom = (XPoints(FuncInd%, IXY) - XPoints(FuncInd%, IXY - 1))
                Denom = (Session("XPoints")(FuncInd%, IXY) - Session("XPoints")(FuncInd%, IXY - 1))
                'JRS 10/20/03 DepVar = YPoints(FuncInd%, IXY - 1)
                DepVar = Session("YPoints")(FuncInd%, IXY - 1)
                DepVar = DepVar + Numer / Denom 'This sequence preserves extended digits
                Exit Sub
            End If
        Next IXY


        Exit Sub
BivariateFuncError:
        Msg$ = "Error occurred processing XYTABL, using Function " + TFName
        Msg$ = Msg$ + ".  You will need to reverse changes made in bivariate "
        Msg$ = Msg$ + "functions or reload the original TablFunc.mdb."
        MsgBox(Msg$)
        Stop
        Exit Sub


    End Sub

    Public Sub FillData(ByVal Sel2$, ByVal SEL1$) 'jrs 2005/08/26 this was static
        Dim found As String, RunCount As Integer, ii As Integer, i As Integer
        Dim SE As String, SELN As String, TMP1 As String, DoingHistory As Boolean
        'Code modified from VarSel:Cmb_Dim1_Click
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'sel2=current (last) selection
        'For single dimension only, sel2$=Country or Region, Sel1=""
        'For doubly dimensioned, sel2$=Dimension2 name, Sel1$= Country or Region
        'to conform with previous code

        Dim Region$

        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        SubDimNum% = 0
        If VDim1(SELSDG) > 0 Then
            SubDimNum% = 1
        End If
        If VDim2(SELSDG) > 0 Then
            SubDimNum% = 2
        End If



        Region$ = SEL1
        If Region$ = "" Then Region$ = Sel2$

        '   Check Validity of Group or Country
        '   ----------------------------------
        found$ = "False"

        If FlagGroup = True Then 'Set up for groups
            For EleLoop = 1 To GCount + 1
                If UCase$(Region$) = UCase$(GroupNames$(EleLoop)) Then
                    found$ = "True"
                    DimPoint% = EleLoop
                    'JRS 5/21/02
                    GroupPoint% = GroupCodes(EleLoop)
                    Exit For
                End If
            Next EleLoop
        Else
            For EleLoop = 1 To MaxVal(1)
                If UCase$(Region$) = UCase$(GRPS$(1, EleLoop)) Then
                    found$ = "True"
                    DimPoint% = EleLoop
                    Exit For
                End If
            Next EleLoop
        End If

        If found$ = "False" Then
            DimError()
        End If

        '   Check Validity of Second Dim, if any
        '   ----------------------------------

        If SEL1 <> "" Then 'Sel2 contains second dim
            'mti SDG bug
            SDimPoint% = DimPoint% 'SDG mti brought it up, since both dims done together

            found$ = "False"
            DimNum = VDim2(SELSDG)
            For EleLoop = 1 To MaxVal(DimNum)
                If UCase$(Sel2$) = UCase$(GRPS$(DimNum, EleLoop)) Then
                    found$ = "True"
                    DimPoint% = EleLoop
                    Exit For
                End If
            Next EleLoop

            If found$ = "False" Then
                DimError()
            End If

        Else
            DimNum = VDim1(SELSDG)
        End If

        '   Explanation of Logic For Dealing With Dimensions
        '   ------------------------------------------------

        '   The rest of this procedure has two major logic options:

        '   A.  All is selected on one dimension, so it is possible immediately, or after
        '       the selection of a second dimension to go into a repetition logic, using
        '       Gridput to fill plot for all elements on the dimension.
        '
        '   B.  All is not selected as a dimension, meaning that after selection of one or
        '       more dimensions we use Gridput to fill the plot for a single selection

        '   There is the possibility in either major logic that the variable is regionalized
        '   and that the Group Flag is set to true.  In this case, it is necessary to carry to
        '   Gridput the pointer for the variable and the instruction (via GroupOn = True) to
        '   sum across all regional members for all groups for for a single group.

        '   Logic A.  If ALL Selected (May not know until selection of 2nd dimension)
        '   -------------------------------------------------------------------------

        If ((SubDimNum% = 1 Or SubDimDone$ = "False") And Sel2$ = "ALL") Then SEL1$ = "ALL"
        If SEL1$ = "ALL" Or Sel2$ = "ALL" Then
            'JRS 12/06/01 DISPLAY BEGIN
            Do While True
                'JRS 9/19/01 COMPUTATIONS
                If Session("bComputation") Then
                    Session("bCompAll") = True
                    'JRS 9/28/01 COMPUTATIONS
                    If Session("ComputationsDimensionSize") = 0 Then
                        If SubDimNum% = 1 Then
                            Session("ComputationsDimensionSize") = MaxVal(VDim1(SELSDG))
                            'JRS 07/03/03
                            Session("iDimAll") = 1
                        ElseIf SubDimNum% = 2 Then
                            Session("ComputationsDimensionSize") = 2000 + MaxVal(VDim2(SELSDG))
                            'JRS 07/03/03
                            Session("iDimAll") = 2
                        End If
                    Else
                        If (SubDimNum% = 1 And Session("ComputationsDimensionSize") <> MaxVal(VDim1(SELSDG))) Or
                            (SubDimNum% = 2 And Session("ComputationsDimensionSize") <> 2000 + MaxVal(VDim2(SELSDG))) Then
                            Session("bCompAll") = False
                            Dim Msg As String
                            Msg = "This variable doesn't have the same dimension size chosen before"
                            LoadMessage(Msg, Session("Message"))
                            Support.FormManager.StateStore(Me)
                            Support.FormManager.Show(Me, "frm_Message.aspx")
                        End If
                    End If
                End If

                If SumOn <> True Then SeqOn = "True"
                If SubDimNum% = 1 Then ' Do Gridput for Single-Dimensioned Variable, All
                    If FlagGroup Then RunCount% = GCount Else RunCount% = MaxVal(DimNum) - 1
                    For ii% = 1 To RunCount% 'MaxVal(DimNum) - 1
                        If DimNum = 1 And FlagGroup = True Then
                            'Label1 = GroupNames$(GroupPoint%)
                            Label1 = GroupNames$(ii%)
                            'JRS 5/21/02
                            GroupPoint% = GroupCodes(ii%)
                        Else
                            Label1 = GRPS$(DimNum, ii%)
                        End If
                        Label2 = ""
                        If SumOn = True Then
                            Label1 = "ALL"
                            Label2 = ""
                        End If
                        VPTR = ii%
                        Call Gridput()
                        If SumOn = True And ii% = MaxVal(DimNum) - 1 Then
                            SumOn = False
                            CalcRunning = False
                        End If

                        If CalcType <> "None" And SumOn <> True _
                            And ii% = RunCount% Then 'MaxVal(DimNum) - 1 Then
                            SeqRepeat = RunCount% 'MaxVal(DimNum) - 1
                            COUNTESDG = COUNTESDG - SeqRepeat
                            Session("COUNTE") = COUNTESDG
                            SeqOn = "False"
                            CalcRunning = True
                        End If

                    Next ii%
                    SEL1$ = ""

                    'JRS 1/25/02 DISPLAY ALL
                    If CBool(Session("ShowVarDispList")) Then
                        If rsList.Rows(i).Item("Type") = "D" Then
                            Exit Do
                        End If
                        i = i + 1
                        If i = rsList.Rows.Count Then
                            'JRS 9/20/01 COMPUTATIONS
                            If Session("bComputation") Then

                                'JRS 04/14/03
                                If bCompVisible Then

                                    'Support.FormManager.Show(Me, "frm_computations.aspx")

                                    'jam 2021/12/12
                                    Vpath = Request.GetFriendlyUrlFileVirtualPath

                                    Vext = Request.GetFriendlyUrlFileExtension

                                    vfriendlyurl = FriendlyUrl.Resolve("~/frm_computations.aspx")


                                    vurl = FriendlyUrl.Href("~/frm_computations.aspx")

                                    Response.Redirect(vurl)


                                End If
                            End If
                            Exit Sub
                        End If
                        SE$ = rsList.Rows(i).Item("VarName")
                        SELN$ = UCase$(SE$)
                        If SubDimNum% = 0 Then
                            VPTR = 1
                        ElseIf SubDimNum% = 1 Then
                            VPTR = 0
                        Else
                            VPTR = iDimPointDim1 'DimPoint%
                        End If
                        '
                        '   Identify pointer(SEL) to variable number
                        '   ----------------------------------------
                        SELSDG = 0
                        For i2 = 1 To NumbVars
                            TMP1$ = V$(i2)
                            If SELN$ = UCase$(TMP1$) Then SELSDG = i2 : Exit For
                        Next i2

                    Else
                        'JRS 9/20/01 COMPUTATIONS
                        If Session("bComputation") Then
                            If bCompVisible Then
                                'jam 2021/12/12
                                'Support.FormManager.Show(Me, "frm_computations.aspx")
                                'jam 2021/12/12
                                Vpath = Request.GetFriendlyUrlFileVirtualPath

                                Vext = Request.GetFriendlyUrlFileExtension

                                vfriendlyurl = FriendlyUrl.Resolve("~/frm_computations.aspx")


                                vurl = FriendlyUrl.Href("~/frm_computations.aspx")

                                Response.Redirect(vurl)
                            End If
                        End If
                        'JRS 05/10/03 HISTORY ANALOG
                        '                    If HistoricPlusForecast Then
                        '                        Dim PlotCopyAll()
                        '                        ReDim PlotCopyAll(LastYear - FirstYear + 1, COUNTE)
                        '                        Dim FinAll As Integer
                        '                        FinAll = FinYear(COUNTE)
                        '                        For i = 1 To FinAll
                        '                            For j = 1 To COUNTE
                        '                                If bComputation Then
                        '                                    PlotCopyAll(i, j) = PLOTAll(i, j)
                        '                                Else
                        '                                    PlotCopyAll(i, j) = PLOT(i, j)
                        '                                End If
                        '                            Next j
                        '                        Next i
                        '                        TableName$ = "Series" + VHistoricAnalog(SEL)
                        '                        FirstYear = FirstYearHistory
                        '                        LastYear = LastYearHistory
                        '                        COUNTE = 0
                        '                        'JRS 1/13/04
                        '                        If FlagGroup = True Then
                        '                            For ii% = 1 To GCount
                        '                                COUNTE = COUNTE + 1
                        '                                'JRS 1/15/04
                        '                                GroupPoint% = GroupCodes(ii%)
                        '                                Call History_Filldata
                        '                            Next ii%
                        '                        Else
                        '                            For ii% = 1 To MaxVal(1) - 1
                        '                                COUNTE = COUNTE + 1
                        '                                DimPoint% = ii%
                        '                                Call History_Filldata
                        '                            Next ii%
                        '                        End If
                        '                        For j = 1 To COUNTE
                        '                            For i = 1 To FinAll
                        '                                If bComputation Then
                        '                                    PLOTAll(i + FinYear(COUNTE) - 1, j) = PlotCopyAll(i, j)
                        '                                Else
                        '                                    PLOT(i + FinYear(COUNTE) - 1, j) = PlotCopyAll(i, j)
                        '                                End If
                        '                            Next i
                        '                            FinYear(j) = FinYear(j) + FinAll - 1
                        '                        Next j
                        '                        'FirstYear = FirstYearForecast
                        '                        LastYear = LastYearForecast
                        '                    End If
                        Exit Sub
                    End If



                ElseIf SubDimNum% = 2 And SubDimDone$ = "False" Then ' Get Second Dim

                    SubDimDone$ = "True"
                    SDimNum% = DimNum
                    '                SDimPoint% = DimPoint% 'SDG mti took up
                    '            DimNum = Val(Mid$(Code$, 3, 1))
                    DimNum = VDim2(SELSDG)
                    '                Call DimLoad  'SDG mti
                    Exit Sub

                ElseIf SubDimNum% = 2 And SubDimDone$ = "True" Then ' Do Gridput for 2-dimensioned

                    If SEL1$ = "ALL" And Sel2$ = "ALL" Then DimPoint% = 1
                    If SEL1$ = "ALL" Then  ' All on 1st dimension
                        If FlagGroup Then RunCount% = GCount Else RunCount% = MaxVal(SDimNum) - 1

                        For ii% = 1 To RunCount%   ' MaxVal(SDimNum) - 1
                            If SDimNum% = 1 And FlagGroup = True Then
                                Label1 = GroupNames$(ii%) 'GroupNames$(SDimPoint%)
                                'JRS 5/21/02
                                GroupPoint% = GroupCodes(ii%)
                            Else
                                Label1 = GRPS$(SDimNum%, ii%)
                            End If
                            Label2 = GRPS$(DimNum, DimPoint%)
                            If SumOn = True Then
                                Label1 = "ALL"
                                Label2 = GRPS$(DimNum, DimPoint%)
                            End If
                            VPTR = ii% + (DimPoint% - 1) * (MAXCOM(SDimNum%) - 1)
                            Call Gridput()
                            If SumOn = True And ii% = RunCount% Then 'MaxVal(SDimNum) - 1 Then
                                SumOn = False
                                CalcRunning = False
                            End If
                            If CalcType <> "None" And SumOn <> True _
                            And ii% = RunCount% Then
                                SeqRepeat = RunCount% 'MaxVal(SDimNum) - 1
                                COUNTESDG = COUNTESDG - SeqRepeat
                                Session("COUNTE") = COUNTESDG
                                SeqOn = "False"
                                CalcRunning = True
                            End If
                        Next ii%

                    Else    ' All on 2nd dimension?

                        For ii% = 1 To MaxVal(DimNum) - 1
                            If SDimNum% = 1 And FlagGroup = True Then
                                Label1 = GroupNames$(SDimPoint%)
                            Else
                                Label1 = GRPS$(SDimNum%, SDimPoint%)
                            End If
                            Label2 = GRPS$(DimNum, ii%)
                            If SumOn = True Then
                                Label1 = GRPS$(SDimNum%, SDimPoint%)
                                Label2 = "ALL"
                            End If
                            VPTR = SDimPoint% + (ii% - 1) * (MAXCOM(SDimNum%) - 1)
                            DimPoint% = ii% 'Needed for group pointer calculation in Gridput
                            Call Gridput()
                            If SumOn = True And ii% = MaxVal(DimNum) - 1 Then
                                CalcType = "None"
                                CalcRunning = False
                            End If
                            If CalcType <> "None" And SumOn <> True _
                            And ii% = MaxVal(DimNum) - 1 Then
                                SeqRepeat = MaxVal(DimNum) - 1
                                COUNTESDG = COUNTESDG - SeqRepeat
                                Session("COUNTE") = COUNTESDG
                                SeqOn = "False"
                                CalcRunning = True
                            End If
                        Next ii%
                    End If
                    SubDimDone$ = "False"
                    'JRS 07/03/03
                    'if you use all, and then old calculation
                    'it doesn't work properly because the flag
                    'SeqOn is still true.
                    If Not CalcRunning And SeqOn = "True" Then
                        SeqOn = "False"
                    End If

                    'JRS 9/27/01 COMPUTATIONS
                    If Session("bComputation") Then
                        'Support.FormManager.Show(Me, "frm_computations.aspx")
                        'jam 2021/12/12
                        Vpath = Request.GetFriendlyUrlFileVirtualPath

                        Vext = Request.GetFriendlyUrlFileExtension

                        vfriendlyurl = FriendlyUrl.Resolve("~/frm_computations.aspx")


                        vurl = FriendlyUrl.Href("~/frm_computations.aspx")

                        Response.Redirect(vurl)
                    Else
                        Exit Sub
                    End If

                End If
            Loop

        End If ' End of ALL Section

        '
        '   Logic B (or, selection of 1st dimension prior to knowing that ALL will be
        '       selected on the second).  Load Subdimension names and Pointers
        '   -------------------------------------------------------------------------
        'JRS 1/25/02 DISPLAY
        Do While True

            If SubDimNum% = 1 Or SubDimDone$ = "False" Then 'SubDimNum% is the number of subdimensions (0,1 or 2) that the IFsVar variable has
                If DimNum = 1 And FlagGroup = True Then
                    Label1 = GroupNames$(DimPoint%)
                Else
                    Label1 = Sel2$
                End If
                VPTR = 0 'SDG mti
                VPTR = VPTR + DimPoint%  'initial and possibly final calculation of
                'variable pointer shift
                GMAX1 = MAXCOM(DimNum) - 1
                Session("SDGGMAX1") = GMAX1
                'JRS 12/06/01 DISPLAY
                iDimPointDim1 = VPTR
            Else 'two dims and both done together for SDG
                Label1 = SEL1 'this is different from varsel here: since both dim done together: Sel2$
                Label2 = Sel2$
                VPTR = 0 'SDG mti both dim done together
                VPTR = VPTR + SDimPoint%  'initial and possibly final calculation of

                VPTR = VPTR + ((DimPoint% - 1) * GMAX1)  'final pointer shift calculation
            End If

            '
            '   Determine whether to return to form for another variable
            '       or to get another subdimension element
            '   --------------------------------------------------------

            If SubDimNum% = 1 Or SubDimDone$ = "True" Then  'Go to Gridput only when have
                'all pointer calculations done

                '            GoSub RemoveDim1:  'SDG mti

                'JRS 10/05/01 COMPUTATIONS
                If Session("bComputation") Then
                    If Session("ComputationsDimensionSize") > 0 Then
                        Dim Msg As String
                        Msg = "This variable doesn't have the same dimension size chosen before"
                        LoadMessage(Msg, Session("Message"))
                        'jam 2021/12/12
                        'Support.FormManager.StateStore(Me)
                        'Support.FormManager.Show(Me, "frm_Message.aspx")

                        'jam 2021/12/12
                        Vpath = Request.GetFriendlyUrlFileVirtualPath

                        Vext = Request.GetFriendlyUrlFileExtension

                        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


                        vurl = FriendlyUrl.Href("~/frm_Message.aspx")

                        Response.Redirect(vurl)

                    End If
                End If

                Call Gridput()

            Else

                '       Load Combo Box with new elements
                '       --------------------------------
                SubDimDone$ = "True"
                SDimNum% = DimNum
                '            SDimPoint% = DimPoint% 'SDG mti take it up
                DimNum = VDim2(SELSDG)
                '            Call DimLoad  'SDG mti
                'JRS 12/06/01 DISPLAY

                Call Gridput() 'SDG mti, need this for double dimensioned var

                Exit Do
            End If

            '        If mnuVarSelOptShowVarDispList.Checked = True Then
            '            If rsList.Fields("Type") = "D" Then
            '                Exit Do
            '            End If
            '            rsList.MoveNext
            '            If rsList.EOF Then Exit Do
            '            SE$ = rsList.Fields("VarName")
            '            SELN$ = UCase$(SE$)
            '            If SubDimNum% = 0 Then
            '                VPTR = 1
            '            ElseIf SubDimNum% = 1 Then
            '                VPTR = 0
            '            Else
            '                VPTR = iDimPointDim1 'DimPoint%
            '            End If
            '        '
            '        '   Identify pointer(SEL) to variable number
            '        '   ----------------------------------------
            '            SEL = 0
            '            For i2 = 1 To NumbVars
            '                TMP1$ = V$(i2)
            '                If SELN$ = UCase$(TMP1$) Then SEL = i2: Exit For
            '            Next i2
            '
            '        Else
            'JRS 05/10/03 HISTORY ANALOG
            '            If HistoricPlusForecast Then
            '                Dim PlotCopy()
            '                ReDim PlotCopy(LastYear - FirstYear + 1)
            '                Dim Fin As Integer
            '                Fin = FinYear(COUNTE)
            '                For i = 1 To Fin
            '                    If bComputation Then
            '                        PlotCopy(i) = PLOTComp(i)
            '                    Else
            '                        PlotCopy(i) = PLOT(i, COUNTE)
            '                    End If
            '                Next i
            '                TableName$ = "Series" + VHistoricAnalog(SEL)
            '                FirstYear = FirstYearHistory
            '                LastYear = LastYearHistory
            '                Call History_Filldata
            '                For i = 1 To Fin
            '                    If bComputation Then
            '                        PLOTComp(i + FinYear(COUNTE) - 1) = PlotCopy(i)
            '                    Else
            '                        PLOT(i + FinYear(COUNTE) - 1, COUNTE) = PlotCopy(i)
            '                    End If
            '                Next i
            '                FinYear(COUNTE) = FinYear(COUNTE) + Fin - 1
            '                'FirstYear = FirstYearForecast
            '                LastYear = LastYearForecast
            '            End If
            Exit Do
            '        End If

        Loop 'JRS 12/06/01 DISPLAY END

        '   End If  'If Doing History

        Session("SDGCOUNTESDG") = COUNTESDG

        'JRS 9/10/01 COMPUTATIONS
        If Session("bComputation") And ((SubDimNum% = 1 Or Label2 <> "") Or DoingHistory) Then
            'JRS 9/28/01 COMPUTATIONS
            If Session("ComputationsDimensionSize") = 0 Then
                Session("ComputationsDimensionSize") = -1
            End If
            'Support.FormManager.Show(Me, "frm_computations.aspx")

            'jam 2021/12/12
            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_computations.aspx")


            vurl = FriendlyUrl.Href("~/frm_computations.aspx")

            Response.Redirect(vurl)

        Else
            Exit Sub
        End If

        '
        '--------

RemoveDim1:  ' Make Combo Box and Label Invisible for Subdimension(s)
        '        cmb_Dim1.Visible = False
        '        cmb_VarName.Visible = True
        '        cmb_VarName.Enabled = True
        '        lbl_Dim1.Visible = False
        '        cmb_Dim1.Clear
        '        lbl_VarName.Visible = True
        '        lbl_files.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Visible = True
        '        cboFiles.Visible = True
        '        mnuScenarioOutFilesExp.Visible = True
        '' JRS 11/24/01 DISPLAY but_File.Visible = True
        ' JRS 11/24/01 DISPLAY Txt_files.Enabled = False
        '        lbl_vcat.Visible = True
        '        cmb_VCAT.Text = ""
        '        cmb_VCAT.Visible = True

        Return

    End Sub

    Private Sub DimError()
        Dim Msg As String
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Msg$ = sel2$ & " is an incorrect sub-dimension choice"
        LoadMessage(Msg, Session("Message"))
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_Message.aspx")

        'jam 2021/12/12
        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_Message.aspx")


        vurl = FriendlyUrl.Href("~/frm_Message.aspx")

        Response.Redirect(vurl)

    End Sub

    Private Sub CalcNumber()
        Dim cnt_init As Object, cnt_end As Object, tempcnt As Object

        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        cnt_init = COUNTESDG
        cnt_end = COUNTESDG + IIf(SeqRepeat > 0, SeqRepeat - 1, 0)
        For tempcnt = cnt_init To cnt_end
            If SeqRepeat > 0 Then
                SeqRepeat = SeqRepeat - 1
                COUNTESDG = COUNTESDG + 1
                Session("COUNTE") = COUNTESDG
            End If
            grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(1.5 * Inch))
            grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text = grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text & Str(Session("VarVal"))
            VARNSDG$(0, COUNTESDG) = grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text
            VARNSDG$(1, COUNTESDG) = grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text

            For i2 = 1 To Session("LastYear") - FirstYearSDG% + 1

                If (DivOnPrev Or CalcSave = "DivOn") And Session("VarVal") <> 0 Then
                    PlotSDG(i2, COUNTESDG) = PlotSDG(i2, COUNTESDG) / Session("VarVal")
                End If
                If CalcSave = "PerOn" And Session("VarVal") <> 0 Then
                    PlotSDG(i2, COUNTESDG) = PlotSDG(i2, COUNTESDG) / Session("VarVal") * 100
                End If
                If MulOnPrev Or CalcSave = "MulOn" Then
                    PlotSDG(i2, COUNTESDG) = PlotSDG(i2, COUNTESDG) * Session("VarVal")
                End If
                If AddOnPrev Or CalcSave = "AddOn" Then
                    PlotSDG(i2, COUNTESDG) = PlotSDG(i2, COUNTESDG) + Session("VarVal")
                End If
                If SubOnPrev Or CalcSave = "SubOn" Then
                    PlotSDG(i2, COUNTESDG) = PlotSDG(i2, COUNTESDG) - Session("VarVal")
                End If
            Next i2
            'xtemp = 5
            'MsgBox ("Test?")

        Next tempcnt

        Session("SDGCOUNTESDG") = COUNTESDG
    End Sub

    Public Sub Gridput()
        Dim idx As Integer, ICount As Integer, agrule As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        If FileNum = 0 Then
            cRunfile(COUNTESDG) = "[" & LTrim$(Str$(FileNum)) & "] " + "Working"
        Else
            cRunfile(COUNTESDG) = "[" & LTrim$(Str$(FileNum)) & "] " + Replace(Replace(Replace(Session("RunFiles")(FileNum), Session("PutFdrvRun"), ""), ".RUN", ""), ".run", "")
        End If
        Session("SDGcRunfile") = cRunfile

        '   Save variable name
        '   ------------------
        'jrs 2017/06/05 replaced SELMDG with COUNTEMDG
        VARNSDG$(0, COUNTESDG) = Session("VARN")(0, COUNTESDG)
        VARNSDG$(1, COUNTESDG) = Session("VARN")(1, COUNTESDG)
        VARNSDG$(2, COUNTESDG) = Session("VARN")(2, COUNTESDG)
        VARNSDG$(3, COUNTESDG) = Session("VARN")(3, COUNTESDG)
        VARNSDG$(5, COUNTESDG) = Session("VARN")(5, COUNTESDG)
        VARNSDG$(6, COUNTESDG) = Session("VARN")(6, COUNTESDG)


        'mti SDG take Country Name
        If Not CalcRunning Then
            CRName(COUNTESDG) = Label1
        End If
        Session("SDGCRName") = CRName

        '   Add Operator String, if any, to grid
        '   ------------------------------------
        GR1NSDG$(COUNTESDG) = Session("GR1N")(COUNTESDG)
        GR2NSDG$(COUNTESDG) = Session("GR2N")(COUNTESDG)
        GR3NSDG$(COUNTESDG) = Session("GR3N")(COUNTESDG)
        GR4NSDG$(COUNTESDG) = Session("GR4N")(COUNTESDG) 'jrs 2006/05/30 dim3


        '   Fill Grid
        '   ---------
        If CalcRunning = False Or SumOn = True Then

            grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(Inch * 1.5))
            grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text = VARNSDG$(1, COUNTESDG)
            grd_VariableDisplay.Rows(1).Cells(COUNTESDG - 1).Text = CRName(COUNTESDG) 'GR1N$(COUNTE)
            grd_VariableDisplay.Rows(2).Cells(COUNTESDG - 1).Text = GR2NSDG$(COUNTESDG)
            grd_VariableDisplay.Rows(3).Cells(COUNTESDG - 1).Text = GR3NSDG$(COUNTESDG)
        Else
            grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(Inch * 1.5))
            grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text = grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text & VARNSDG$(1, COUNTESDG)
            VARNSDG$(0, COUNTESDG) = grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text
            VARNSDG$(1, COUNTESDG) = grd_VariableDisplay.Rows(0).Cells(COUNTESDG - 1).Text
            grd_VariableDisplay.Rows(1).Cells(COUNTESDG - 1).Text = CRName(COUNTESDG) 'grd_VariableDisplay.Text & GR1N$(COUNTESDG)
            'GR1N$(COUNTE) = grd_VariableDisplay.Text
            grd_VariableDisplay.Rows(2).Cells(COUNTESDG - 1).Text = grd_VariableDisplay.Rows(2).Cells(COUNTESDG - 1).Text & GR2NSDG$(COUNTESDG)
            GR2NSDG$(COUNTESDG) = grd_VariableDisplay.Rows(2).Cells(COUNTESDG - 1).Text
            grd_VariableDisplay.Rows(3).Cells(COUNTESDG - 1).Text = grd_VariableDisplay.Rows(3).Cells(COUNTESDG - 1).Text & GR3NSDG$(COUNTESDG)
            GR3NSDG$(COUNTESDG) = grd_VariableDisplay.Rows(3).Cells(COUNTESDG - 1).Text

        End If

        '   Recognize Beginning or End of Calculation - Save for Next Input
        '   ---------------------------------------------------------------
        If CalcRunning = True And (CalcType = "None" And SumOn = False) Then
            CalcRunning = False
        End If

        If CalcRunning = False And (CalcType <> "None" Or SumOn = True) _
            And SeqOn <> "True" Then
            CalcRunning = True
        End If

        If SeqRepeat > 0 Then 'When sequence is repeated, it is for calculation
            CalcRunning = True
        Else
            CalcSave = CalcType  'Do not change CalcSave except when sequence over
        End If

        'mti Validation changes
        '    ChkPickingForecast  'SDG mti
        'mti Validation changes

        'cmb_VarName.SetFocus
        '    If Me.Visible Then cmb_VarName.SetFocus

        Session("SDGVARNSDG") = VARNSDG
        Session("SDGGR1NSDG") = GR1NSDG
        Session("SDGGR2NSDG") = GR2NSDG
        Session("SDGGR3NSDG") = GR3NSDG
        Session("SDGGR4NSDG") = GR4NSDG
        Session("SDGFileNumbrSDG") = FileNumbrSDG
        Session("SDGCOUNTESDG") = COUNTESDG
    End Sub
    '
    '----------
    Private Sub LowComYear()
        Dim VLN As Object, EndYear As Object
        '   Check the Files to be Used for Their End Years.  If more than 1 file
        '       is speciifed then the lowest year is used for display purposes.
        VLN = VarPoint(PosModYear)
        Session("RECORD") = (VLN + (0 * filen)) * 4 - 3

        Call RetrValues()

        'FinYear(VARNUM) = VarVal - FirstYear% + 1

        'JRS 10/22/02 FINYEAR
        If CalcRunning Or Session("bComputation") Then
            If (Session("VarVal") - FirstYearSDG% + 1 < Session("FinYear")(Session("VARNUM")) Or Session("FinYear")(Session("VARNUM")) = Nothing) Then
                Session("FinYear")(Session("VARNUM")) = Session("VarVal") - FirstYearSDG% + 1
            End If
        Else
            Session("FinYear")(Session("VARNUM")) = Session("VarVal") - FirstYearSDG% + 1
        End If
        EndYear = IFsLowCommonYear% 'mti SDG
        If (Session("VarVal") <= EndYear) Then
            EndYear = Session("VarVal")
            IFsLowCommonYear% = EndYear
        End If

        'JRS 05/12/03
        Ag_rule$ = VAggregation(SELSDG)

        Session("SDGIFsLowCommonYear") = IFsLowCommonYear
        Return

    End Sub

    Private Sub PlotLoad()
        Dim finam As String
        Dim Number As Single
        Dim agrule As String
        Dim Sumplot As Object
        Dim SumWeight As Object
        Dim ICount As Integer
        Dim varval2 As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim Offset4Calc
        'mti Validation changes
        Dim intFile%
        intFile = FreeFile()
        'mti Validation changes : replace all #1 with #intFile for this procedure


        Session("VARNUM") = COUNTESDG

        '    If FileNum = 0 Then
        '        finam$ = PutFdrvRun$ + "working.run"
        '    ElseIf FileNum = 1 Then
        '        finam$ = PutFdrvRun$ + ActiveBase$ '"IFSBASE.RUN"
        '    Else
        '        finam$ = ScenarioFilesSelected$ 'RunFiles$(FileNum)
        '    End If

        If FileNum = 0 Then
            finam$ = Session("PutFdrvRun") + "working.run.db"
            'JRS 11/17/02 ElseIf FileNum = 1 Then
            'JRS 11/17/02   finam$ = PutFdrvRun$ + "IFSBASE.RUN"
        Else
            finam$ = Session("RunFiles")(FileNum)
        End If


        'FileOpen(intFile, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

        Offset4Calc = 0
        If Session("LastYear") - FirstYearSDG% > 101 Then
            Offset4Calc = FirstYearForecast - FirstYearHistory
        End If



        For i2 = 1 To Session("LastYearForecast") - FirstYearSDG% + 1
            If i2 > Session("FinYear")(COUNTESDG) Then Exit For 'Beyond run year

            '   If No group, calculate normal single value, otherwise sum values across regions
            '   -------------------------------------------------------------------------------

            'If GroupOn = False Then
            If FlagGroup = False Then
                Session("RECORD") = (VarPointer + ((i2 - 1) * filen)) * 4 - 3
                FileGet(intFile, Number, Session("RECORD"))
                Session("VarVal") = Number!
            Else
                agrule$ = VAggregation(SELSDG)
                Session("MinValue") = 1000000000
                Session("MaxValue") = -100000000.0#
                Sumplot = 0
                SumWeight = 0
                For ICount% = 1 To GroupMembCount
                    VarPointer = GroupMemb(ICount%)
                    Session("RECORD") = (VarPointer + ((i2 - 1) * filen)) * 4 - 3
                    FileGet(intFile, Number, Session("RECORD"))
                    Session("VarVal") = Number!
                    If agrule$ = "GDP" Then
                        VarPointer = AgPoint + WeightVarPoint(ICount%)
                        Session("RECORD") = (VarPointer + ((i2 - 1) * filen)) * 4 - 3
                        FileGet(intFile, Number, Session("RECORD"))
                        varval2 = Number!
                        SumWeight = SumWeight + varval2
                    End If
                    If agrule$ = "POP" Then
                        VarPointer = AgPoint + WeightVarPoint(ICount%)
                        Session("RECORD") = (VarPointer + ((i2 - 1) * filen)) * 4 - 3
                        FileGet(intFile, Number, Session("RECORD"))
                        varval2 = Number!
                        SumWeight = SumWeight + varval2
                    End If
                    If agrule$ = "GDP" Or agrule$ = "POP" Then
                        Sumplot = Sumplot + Session("VarVal") * varval2
                    Else
                        Sumplot = Sumplot + Session("VarVal")
                    End If
                    If agrule$ = "MIN" And Session("VarVal") < Session("MinValue") Then Session("MinValue") = Session("VarVal")
                    If agrule$ = "MAX" And Session("VarVal") > Session("MaxValue") Then Session("MaxValue") = Session("VarVal")
                Next ICount%
                If agrule$ = "SUM" Then
                    Session("VarVal") = Sumplot
                ElseIf agrule$ = "AVG" Then
                    Session("VarVal") = Sumplot / GroupMembCount
                ElseIf agrule$ = "MIN" Then
                    Session("VarVal") = Session("MinValue")
                ElseIf agrule$ = "MAX" Then
                    Session("VarVal") = Session("MaxValue")
                ElseIf agrule$ = "GDP" Or agrule$ = "POP" Then
                    Session("VarVal") = Sumplot / AMAX(0.001, SumWeight)
                End If
            End If

            'JRS Begin Calling Function to check Currency Conversion
            '        CurrencyCheck
            ' If it has not been already checked
            If Not bChkCurr Then 'JRS 6/19/01
                CurrencyCheck() 'SDG mti
            End If

            Session("VarVal") = Session("VarVal") / CurrencyFct
            'JRS End Calling Function to check Currency Conversion

            '       Now that have VARVAL, put it into single value, sum, or running calculation
            '       ---------------------------------------------------------------------------
            'JRS 9/9/01 Changes to add Computations
            If Session("bComputation") Then
                If Session("bCompAll") Then
                    PLOTAllSDG(i2, COUNTESDG) = Session("VarVal")
                Else
                    PLOTCompSDG(i2) = Session("VarVal")
                End If
            Else
                If CalcRunning = False Then
                    '           Normal Variable Insert
                    '           ----------------------
                    PlotSDG(i2, COUNTESDG) = Session("VarVal")
                ElseIf SumOn = True Then
                    'Running Calculation, not first sum
                    PlotSDG(i2, COUNTESDG) = PlotSDG(i2, COUNTESDG) + Session("VarVal")
                Else 'Running Calculation, not First Value
                    '           Calculate arithmetic operation
                    '           ------------------------------
                    If CalcSave = "DivOn" And Session("VarVal") <> 0 Then
                        PlotSDG(i2 + Offset4Calc, COUNTESDG) = PlotSDG(i2 + Offset4Calc, COUNTESDG) / Session("VarVal")
                    End If
                    If CalcSave = "PerOn" And Session("VarVal") <> 0 Then
                        PlotSDG(i2 + Offset4Calc, COUNTESDG) = PlotSDG(i2 + Offset4Calc, COUNTESDG) / Session("VarVal") * 100
                    End If
                    If CalcSave = "MulOn" Then
                        PlotSDG(i2 + Offset4Calc, COUNTESDG) = PlotSDG(i2 + Offset4Calc, COUNTESDG) * Session("VarVal")
                    End If
                    If CalcSave = "AddOn" Then
                        PlotSDG(i2 + Offset4Calc, COUNTESDG) = PlotSDG(i2 + Offset4Calc, COUNTESDG) + Session("VarVal")
                    End If
                    If CalcSave = "SubOn" Then
                        PlotSDG(i2 + Offset4Calc, COUNTESDG) = PlotSDG(i2 + Offset4Calc, COUNTESDG) - Session("VarVal")
                    End If
                End If
            End If
        Next i2
        FileClose(intFile)
        Session("PercentOn") = "False" 'Setting Table Display Option
        'JRS Begin Turning off the flag for checking
        bChkCurr = False 'So it will check again for other variables
        CurrencyFct = 1 'And it will not affect any computation
        'JRS End Turning off the flag for checking
        Session("SDGPlotSDG") = PlotSDG

    End Sub

    Public Sub ReadCurrency()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        'Opens the DataBase
        SetDataTablefromAccess(Session("FdrvRun") & "IFS.MDB", "SELECT * FROM [Currency] WHERE [Currency Flag]=True", "", rsCurr)
    End Sub

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

    Private Sub RetrValues()
        Dim bPickingValidation As Boolean 'jrs 2005/08/25 useless variable
        Dim DoingHistory As Boolean 'jrs 2005/08/25 useless variable
        Dim finam As String
        Dim Number As Single
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '     Retrieve Values from Buffer
        '     ---------------------------

        'mti Validation changes
        Dim intFile%
        intFile% = FreeFile()

        ''mti Validation changes 10 august 2002
        If bPickingValidation Then
            If DoingHistory Then
                FileNum = -1
            Else
                FileNum = 1
            End If
        End If
        'mti Validation changes 10 august 2002

        If FileNum = 0 Then
            finam$ = Session("PutFdrvRun") + "working.run"
            'JRS 11/17/02 ElseIf FileNum = 1 Then
            'JRS 11/17/02   finam$ = PutFdrvRun$ + "IFSBASE.RUN"
        Else
            finam$ = Session("RunFiles")(FileNum)
        End If


        '      If FileNum = 0 Then
        '         finam$ = PutFdrvRun$ + "working.run"
        '      ElseIf FileNum = 1 Then
        '         finam$ = PutFdrvRun$ + ActiveBase$ '"IFSBASE.RUN"
        '      Else
        '         finam$ = ScenarioFilesSelected$ 'RunFiles$(FileNum)
        '      End If

        FileOpen(intFile, finam, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared) '#1
        FileGet(intFile, Number, Session("RECORD"))
        Session("VarVal") = Number!
        FileClose(intFile)

        'mti Validation changes



    End Sub

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

    End Sub

    Private Sub DrawSDGgraph()
        ReDim ProgressStr(COUNTESDG)
        ReDim TrendCalc(COUNTESDG)
        ReDim ActualProgress(COUNTESDG)
        ReDim ReqdProgress(COUNTESDG)

        Session("SDGlblIndicator") = lblIndicator.Text
        Session("SDGcmb_Dim1") = cmb_Dim1.SelectedValue

        Session("SDGLasYear_is_LowCommonYear") = LasYear_is_LowCommonYear$
        Session("SDGIFsLowCommonYear") = IFsLowCommonYear
        Session("SDGIndicator_TargetYear") = Indicator_TargetYear
        Session("SDGIndicator_BaseYear") = Indicator_BaseYear
        Session("SDGIIndex") = IIndex
        Session("SDGDisplayLastYearSDG") = DisplayLastYearSDG
        Session("SDGCOUNTESDG") = COUNTESDG
        Session("SDGFlagGroup") = FlagGroup

        Session("SDGEarliestYearWithData") = EarliestYearWithData
        Session("SDGTrendCalc") = TrendCalc
        Session("SDGsgrpEarliestYearWithData") = sgrpEarliestYearWithData
        Session("SDGFirstYearSDG") = FirstYearSDG

        Session("SDGPLOTSDGH") = PLOTSDGH
        'jrs 2017/07/24
        Session("SDGPLOTSDGHR") = PLOTSDGHR
        Session("SDGIndicator_Function") = Indicator_Function
        Session("SDGIndicator_DriverTbl") = Indicator_DriverTbl
        Session("SDGCountries") = Countries
        Session("SDGIndicator_Conversion") = Indicator_Conversion

        Session("SDGBaseVal") = BaseVal
        Session("SDGIndicator_Max") = Indicator_Max
        Session("SDGIndicator_Min") = Indicator_Min
        Session("SDGIndicator_ConversionNumber") = Indicator_ConversionNumber
        'jrs 2017/05/31
        Session("SDGIndicator_TransFunction") = Indicator_TransFunction
        Session("SDGIndicator_TransFunctionVar") = Indicator_TransFunctionVar

        Session("SDGTrendCalc") = TrendCalc
        Session("SDGDisplayEarliestYearWithData") = DisplayEarliestYearWithData
        Session("SDGOffset_Earliest") = Offset_Earliest

        Session("SDGGraphMSG") = GraphMSG
        Session("SDGMsg") = SDGMsg
        Session("SDGProgressStr") = ProgressStr
        Session("SDGActualProgress") = ActualProgress
        Session("SDGReqdProgress") = ReqdProgress

        Session("SDGIndicator_TargetValue") = Indicator_TargetValue
        Session("SDGGoalVal") = GoalVal
        Session("SDGcRunfile") = cRunfile
        'jrs 2011/11/24
        MultiGraphsShown = 1
        Session("SDGMultiGraphsShown") = MultiGraphsShown
        MultiGraphLabels(1) = cmb_Dim1.Text
        Session("SDGMultiGraphLabels") = MultiGraphLabels

        Session("SDGPlotMulti") = PlotMulti
        Session("SDGPlotSDGHMulti") = PlotSDGHMulti
        Session("SDGmultiProgressStr") = multiProgressStr
        Session("SDGmultiActualProgress") = multiActualProgress
        Session("SDGmultiReqdProgress") = multiReqdProgress
        Session("SDGmultiBaseVal") = multiBaseVal
        Session("SDGmultiCurval") = multiCurval
        Session("SDGmultiGoalVal") = multiGoalVal
        Session("SDGPLOTSDG") = PlotSDG

        lblJS.Visible = True
        PackJScritpBuilder()
    End Sub

    Private Sub cmb_Dim1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Dim1.SelectedIndexChanged
        RefreshForm()
    End Sub

    Private Sub optChart_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optChart.CheckedChanged
        grd_TableDisplay.Visible = False
        'jrs 2005/08/30 Pesgo1.Visible = False

        lblJS.Visible = True

        'mnuSaveDisplay.Caption = "Export Graph"
        'mnuPrint.Caption = "Print Graph"

        cswmWebMenu.Groups("mnuHorizon").Items(0).Enabled = True
        cswmWebMenu.Groups("mnuHorizon").Items(1).Enabled = True
        Session("SDGHorizonVisible") = True

        'mti July 2006 -start
        lblGraphNote.Visible = True
        lstFiles.Enabled = True
        lstcountries.Enabled = True
        'mti July 2006 -end
    End Sub

    Private Sub optData_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optData.CheckedChanged
        cswmWebMenu.Groups("mnuHorizon").Items(0).Enabled = False
        cswmWebMenu.Groups("mnuHorizon").Items(1).Enabled = False
        Session("SDGHorizonVisible") = False

        RefreshTableMulti() 'using same routine 'RefreshTable

        grd_TableDisplay.Visible = True
        'JRS 2005/08/31
        'Pesgo1.Visible = False
        lblJS.Visible = False
        'cmdScaleMulti.Visible = False
        'mnuSaveDisplay.Caption = "Save Data"
        'mnuPrint.Caption = "Print Data"

        'mti July 2006 -start
        lblGraphNote.Visible = False
        lstFiles.Enabled = False
        lstcountries.Enabled = False
        'mti July 2006 -end
    End Sub

    Private Sub mnuHorizonEarliest_Click()
        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        'For ICount% = FirstYear% + 1 To LastYearForecast%
        cmb_Horizon.Items.Clear()
        'jrs 2014/04/14
        cmb_Horizon.Items.Add("")
        cmb_Horizon.Items.Add("Earliest Data Point")
        Dim ICount As Integer
        For ICount% = FirstYearSDG + 1 To Indicator_BaseYear(IIndex)
            cmb_Horizon.Items.Add(ICount%)
        Next ICount%
        HorizonSelection$ = "Earliest"
        Session("SDGHorizonSelection") = HorizonSelection$
    End Sub

    '========================== frm_SDG ==================
    Private Sub mnuHorizonLatest_Click()
        '    If HistoricBase$ = True Then
        '        Msg$ = "You cannot change the horizon for historic analysis."
        '        MsgBox Msg$
        '        Exit Sub
        '    End If
        cmb_Horizon.Visible = True
        frm_Horizon.Visible = True
        cmb_Horizon.Items.Clear()
        'For ICount% = FirstYear% + 1 To LastYearForecast%
        'jrs 2014/04/14
        cmb_Horizon.Items.Add("")
        cmb_Horizon.Items.Add("Use Display menu latest")
        Dim ICount As Integer
        For ICount% = Indicator_TargetYear(IIndex) + 1 To Session("LastYearForecast")
            cmb_Horizon.Items.Add(ICount%)
        Next ICount%
        HorizonSelection$ = "Latest"
        Session("SDGHorizonSelection") = HorizonSelection$
    End Sub

    Private Sub cmb_Horizon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmb_Horizon.SelectedIndexChanged
        Dim Ind As Integer
        Ind% = cmb_Horizon.SelectedIndex + 1
        If HorizonSelection$ = "Latest" Then
            DisplayLastYearSDG = Val(cmb_Horizon.SelectedValue)
            If DisplayLastYearSDG > 0 Then
                If DisplayLastYearSDG > Session("LastYearForecast") Or DisplayLastYearSDG < Indicator_TargetYear(IIndex) + 1 Then Exit Sub
            End If
        Else  'Earliest]
            DisplayEarliestYearWithData = Val(cmb_Horizon.SelectedValue)
            If DisplayEarliestYearWithData > 0 Then
                If DisplayEarliestYearWithData > Indicator_BaseYear(IIndex) Or DisplayEarliestYearWithData < FirstYearSDG + 1 Then Exit Sub
            End If
        End If
        cmb_Horizon.Visible = False
        frm_Horizon.Visible = False

        If Not Session("SDGCompare") Then  'on single graph
            DrawSDGgraph()
        Else
            RefreshFrameMultiple()
        End If
    End Sub

    Public Sub RefreshTableMulti()
        '------------------------------------------------------------
        Dim DataValtoShow As String 'jrs 2005/08/30 * 15
        Dim CurValtoShow As String 'jrs 2005/08/30 * 15
        Dim BaseValtoShow As String 'jrs 2005/08/30 * 15
        Dim GoalValtoShow As String 'jrs 2005/08/30 * 15
        Dim gg%


        Session("PercentOn") = "False"
        'JRS 1/8/03
        grd_TableDisplay.Visible = False

        If Session("Interval") = 0 Then Session("Interval") = 1


        '   Set colwidth 'and left margin of form
        '   ---------------------------------
        Dim ColWidth As Integer
        ColWidth% = Inch * 1.2 'Inch



        '   Set rows of grid
        '   ----------------
        'grd_TableDisplay.rows = Indicator_TargetYear(IIndex) - FirstYear + 10 '5 'Timepoints% + 5 'was 6 ?? 'was 3

        Dim r1 As New TableRow, r2 As New TableRow, r3 As New TableRow, r4 As New TableRow
        Dim r5 As New TableRow, r6 As New TableRow, r7 As New TableRow, r8 As New TableRow
        Dim c1 As Integer

        'jrs 2011/11/24 For c1 = 1 To MultiGraphsShown * (COUNTESDG / Session("SDGFilesSelected") + 2) + 1
        For c1 = 1 To MultiGraphsShown * (Session("SDGFilesSelected") + 2) + 1
            Dim cet1 As New TableCell, cet2 As New TableCell, cet3 As New TableCell, cet4 As New TableCell
            Dim cet5 As New TableCell, cet6 As New TableCell, cet7 As New TableCell, cet8 As New TableCell

            cet1.Text = ""
            cet1.BackColor = System.Drawing.Color.Gray
            r1.Cells.Add(cet1)
            cet2.Text = ""
            cet2.BackColor = System.Drawing.Color.Gray
            r2.Cells.Add(cet2)
            cet3.Text = ""
            cet3.BackColor = System.Drawing.Color.Gray
            r3.Cells.Add(cet3)
            cet4.Text = ""
            cet4.BackColor = System.Drawing.Color.Gray
            r4.Cells.Add(cet4)
            cet5.Text = ""
            cet5.BackColor = System.Drawing.Color.Gray
            r5.Cells.Add(cet5)
            cet6.Text = ""
            cet6.BackColor = System.Drawing.Color.Gray
            r6.Cells.Add(cet6)
            cet7.Text = ""
            cet7.BackColor = System.Drawing.Color.Gray
            r7.Cells.Add(cet7)
            cet8.Text = ""
            cet8.BackColor = System.Drawing.Color.Gray
            r8.Cells.Add(cet8)

        Next c1

        grd_TableDisplay.Rows.Clear()
        grd_TableDisplay.Rows.Add(r1)
        grd_TableDisplay.Rows.Add(r2)
        grd_TableDisplay.Rows.Add(r3)
        grd_TableDisplay.Rows.Add(r4)
        grd_TableDisplay.Rows.Add(r5)
        grd_TableDisplay.Rows.Add(r6)
        grd_TableDisplay.Rows.Add(r7)
        grd_TableDisplay.Rows.Add(r8)

        '   Set number of columns
        '   ---------------------
        'mti June 2005, SDG Multiple Graph Data

        '   Fill in top of table/ column labels
        '   ------------------------------------
        Dim cno As Integer
        Dim cc As Integer

        'jrs 2017/07/26
        If Session("SDGFilesSelected") = Nothing Then
            Session("SDGFilesSelected") = 1
        End If

        'mti June 2005, SDG Multiple Graph Data -start
        For gg% = 1 To MultiGraphsShown
            'For c1% = 1 To COUNTE  'only column 1?
            c1% = 1
            cno% = (gg% - 1) * (COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown) + 2) + c1%
            grd_TableDisplay.Rows(0).Cells(cno%).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(ColWidth% * 1)) '1.7
            grd_TableDisplay.Rows(0).Cells(cno%).HorizontalAlign = HorizontalAlign.Center
            If Val(FileNumbrSDG$(c1%)) > 1 Then
                grd_TableDisplay.Rows(0).Cells(cno%).Text = TableName 'RTrim$(VARNSDG$(1, c1%))
            Else
                grd_TableDisplay.Rows(0).Cells(cno%).Text = TableName 'RTrim$(VARNSDG$(1, c1%))
            End If

            grd_TableDisplay.Rows(1).Cells(cno%).Text = Session("SDGMultiGraphLabels")(gg%) 'country/region name 'CRName(c1%) 'GR1N$(c1%)
            grd_TableDisplay.Rows(2).Cells(cno%).Text = GR2NSDG$(c1%)
            grd_TableDisplay.Rows(3).Cells(cno%).Text = Session("SDGmultiActualProgress")(gg%)
            grd_TableDisplay.Rows(4).Cells(cno%).Text = Session("SDGmultiReqdProgress")(gg%)
            grd_TableDisplay.Rows(5).Cells(cno%).Text = Session("SDGmultiProgressStr")(gg%)


            grd_TableDisplay.Rows(7).Cells(cno%).Text = "History"

            If Session("PercentOn") = "False" Then
                grd_TableDisplay.Rows(6).Cells(cno%).Text = Indicator_Unit(IIndex) 'GR3N$(c1%)
            Else
                grd_TableDisplay.Rows(6).Cells(cno%).Text = "% Change"
            End If


            '    Next c1%

            cno% = cno% + 1
            grd_TableDisplay.Rows(0).Cells(cno%).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(ColWidth% * 1))
            grd_TableDisplay.Rows(0).Cells(cno%).HorizontalAlign = HorizontalAlign.Center
            If Val(FileNumbrSDG$(c1%)) > 1 Then
                grd_TableDisplay.Rows(0).Cells(cno%).Text = "Regression" 'RTrim$(VARNSDG$(1, c1%))
            Else
                grd_TableDisplay.Rows(0).Cells(cno%).Text = "Regression" 'RTrim$(VARNSDG$(1, c1%))
            End If

            grd_TableDisplay.Rows(1).Cells(cno%).Text = MultiGraphLabels(gg%) 'country/region 'CRName(c1%) 'GR1N$(c1%)
            grd_TableDisplay.Rows(2).Cells(cno%).Text = GR2NSDG$(c1%)
            grd_TableDisplay.Rows(3).Cells(cno%).Text = ""
            grd_TableDisplay.Rows(4).Cells(cno%).Text = ""
            grd_TableDisplay.Rows(5).Cells(cno%).Text = ""

            'mti June 2005, SDG Multiple Graph Data
            grd_TableDisplay.Rows(7).Cells(cno%).Text = "SDG Estimates"

            If Session("PercentOn") = "False" Then
                grd_TableDisplay.Rows(6).Cells(cno%).Text = Indicator_Unit(IIndex) 'GR3N$(c1%)
            Else
                grd_TableDisplay.Rows(6).Cells(cno%).Text = "% Change"
            End If


            For cc% = 1 To COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown)
                '(MultiGraphsShown - 1) * (COUNTE + 2) + cc%
                cno% = (gg% - 1) * COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown) + gg% * 2 + cc%
                grd_TableDisplay.Rows(0).Cells(cno%).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(ColWidth% * 1)) '1.7
                grd_TableDisplay.Rows(0).Cells(cno%).HorizontalAlign = HorizontalAlign.Center
                If Val(FileNumbrSDG$(cc%)) > 1 Then
                    grd_TableDisplay.Rows(0).Cells(cno%).Text = RTrim$(VARNSDG$(1, cc%))
                Else
                    grd_TableDisplay.Rows(0).Cells(cno%).Text = RTrim$(VARNSDG$(1, cc%))
                End If

                grd_TableDisplay.Rows(1).Cells(cno%).Text = MultiGraphLabels(gg%) 'country/group 'CRName(cc% - 2) 'GR1N$(cc%)
                grd_TableDisplay.Rows(2).Cells(cno%).Text = GR2NSDG$(cc%)
                grd_TableDisplay.Rows(3).Cells(cno%).Text = "" 'ActualProgress(cc%)
                grd_TableDisplay.Rows(4).Cells(cno%).Text = "" 'ReqdProgress(cc%)
                grd_TableDisplay.Rows(5).Cells(cno%).Text = "" 'ProgressStr(cc%)

                grd_TableDisplay.Rows(7).Cells(cno%).Text = "IFs " + cRunfile(cc%) '"Data"

                If Session("PercentOn") = "False" Then
                    grd_TableDisplay.Rows(6).Cells(cno%).Text = Indicator_Unit(IIndex) 'GR3N$(cc%)
                Else
                    grd_TableDisplay.Rows(6).Cells(cno%).Text = "% Change"
                End If


            Next cc%

        Next gg%
        'mti June 2005, SDG Multiple Graph Data -end

        '   Fill in left column of table
        '   ----------------------------

        grd_TableDisplay.Rows(0).Cells(0).Width = System.Web.UI.WebControls.Unit.Pixel(VB6.TwipsToPixelsX(Inch))  'HalfInch

        'jrs 2011/11/24
        'grd_TableDisplay.Rows(3).Cells(0).Text = "Actual Progress"
        'grd_TableDisplay.Rows(4).Cells(0).Text = "Required Progress"
        'grd_TableDisplay.Rows(5).Cells(0).Text = "Rate of Progress"

        grd_TableDisplay.Rows(7).Cells(0).Text = "Year"

        Dim rr As Integer
        Dim nry As New TableRow
        Dim cel1 As New TableCell
        nry.Cells.Add(cel1)
        grd_TableDisplay.Rows.Add(nry)
        'For cc% = Offset_SDGbase To Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
        'For rr% = 1 To Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
        For rr% = 1 To Session("LastYear") - FirstYearSDG + 1
            Dim nry2 As New TableRow
            For c1 = 1 To MultiGraphsShown * (COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown) + 2) + 1
                Dim cel2 As New TableCell
                nry2.Cells.Add(cel2)
            Next
            nry2.Cells(0).Text = Str(FirstYearSDG% + rr% - 1)
            grd_TableDisplay.Rows.Add(nry2)
        Next rr%

        '   Fill in data
        '   ------------

        Dim dataval As Object, IFsVal As Object, Value As Object

        'Fill in Data from IFsHistSeries
        For gg% = 1 To MultiGraphsShown
            cc% = gg 'jrs 2005/09/05 1
            cno% = (gg% - 1) * (COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown) + 2) + 1 'jrs 2005/09/05 cc%

            ' data column, 0=year labels cc%
            For rr% = 1 To Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
                grd_TableDisplay.Rows(rr% + 8).Cells(cno%).Text = ""
                dataval = Nothing
                IFsVal = Nothing
                Value = Nothing
                '            DataValue = False

                'jrs 2011/11/24
                'If IsNumeric(PlotSDGHMulti(rr%, cc%, gg%)) Then
                If IsNumeric(Session("SDGPLOTSDGH")(rr%, cc%)) Then
                    'jrs 2011/11/24 
                    'dataval = PlotSDGHMulti(rr%, cc%, gg%)
                    dataval = Session("SDGPLOTSDGH")(rr%, cc%)
                    '                DataValue = True
                Else

                End If

                'mti July 2006 start
                Value = dataval
                DataValtoShow$ = ""
                DV2Show(Value, DataValtoShow$)

                'If IsNumeric(Value) Then
                '    Select Case Value
                '        Case 100 To 1000
                '            DataValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                '        Case 10 To 100
                '            DataValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                '        Case -10 To 10
                '            DataValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
                '            'mti Validation changes  'for % value below 10
                '            If Asc(Right(DataValtoShow, 1)) = 48 Then  '48 = "0"
                '                DataValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                '            End If
                '            'mti Validation changes
                '        Case -100 To -10
                '            DataValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                '        Case -1000 To -100
                '            DataValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                '        Case Else
                '            DataValtoShow = RSet(VB6.Format(Value, "###,###,###,###"), 15)
                '    End Select
                'Else
                '    DataValtoShow = Nothing
                'End If
                'mti July 2006  end

                If IsNumeric(DataValtoShow) Then
                    grd_TableDisplay.Rows(rr% + 8).Cells(cno%).Text = Trim(DataValtoShow)
                End If


            Next rr%



            'Fill in base/current/goal value from Regression Estimate and Calculation in IFs SDG Form



            'For rr% = Offset_SDGbase To Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
            For rr% = 1 To Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
                grd_TableDisplay.Rows(rr% + 8).Cells(cno% + 1).Text = ""
                Value = Nothing

                If rr% = Offset_SDGbase Then
                    'jrs 2011/11/24 Value = multiBaseVal(gg%)
                    Value = Session("SDGBaseVal")(gg%)
                    'mti July 2006 start
                    BaseValtoShow = ""
                    DV2Show(Value, BaseValtoShow)
                    'Select Case Value
                    '    Case 100 To 1000
                    '        BaseValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                    '    Case 10 To 100
                    '        BaseValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '    Case -10 To 10
                    '        BaseValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
                    '        'mti Validation changes  'for % value below 10
                    '        If Asc(Right(BaseValtoShow, 1)) = 48 Then  '48 = "0"
                    '            BaseValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '        End If
                    '        'mti Validation changes
                    '    Case -100 To -10
                    '        BaseValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '    Case -1000 To -100
                    '        BaseValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                    '    Case Else
                    '        BaseValtoShow = RSet(VB6.Format(Value, "###,###,###,###"), 15)
                    'End Select
                    'mti July 2006  end
                    If Trim(BaseValtoShow) <> "" Then
                        grd_TableDisplay.Rows(rr% + 8).Cells(cno% + 1).Text = Trim(BaseValtoShow)  '+ "(" + TrendCalc(cc%) + ")"
                    End If
                End If
                If rr% = Offset_SDGbase + SDGBase2LatestYear Then
                    'jrs 2011/11/24 Value = multiCurval(gg%)
                    Value = Session("SDGCurval")(gg%)
                    'mti July 2006 start
                    CurValtoShow$ = ""
                    DV2Show(Value, CurValtoShow$)
                    'Select Case Value
                    '    Case 100 To 1000
                    '        CurValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                    '    Case 10 To 100
                    '        CurValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '    Case -10 To 10
                    '        CurValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
                    '        'mti Validation changes  'for % value below 10
                    '        If Asc(Right(CurValtoShow, 1)) = 48 Then  '48 = "0"
                    '            CurValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '        End If
                    '        'mti Validation changes
                    '    Case -100 To -10
                    '        CurValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '    Case -1000 To -100
                    '        CurValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                    '    Case Else
                    '        CurValtoShow = RSet(VB6.Format(Value, "###,###,###,###"), 15)
                    'End Select
                    'mti July 2006  end
                    If Trim(CurValtoShow) <> "" Then
                        grd_TableDisplay.Rows(rr% + 8).Cells(cno% + 1).Text = Trim(CurValtoShow)  '+ "(" + TrendCalc(cc%) + ")"
                    End If
                End If
                'SDG_Base2Target + (Offset_SDGbase - Offset_Earliest)
                If rr% = Offset_SDGbase + SDG_Base2Target Then
                    'SDGBase2LatestYear + (Offset_SDGbase - Offset_Earliest)
                    'jrs 2011/11/24 Value = multiGoalVal(gg%)
                    Value = Session("SDGGoalVal")(gg%)
                    'mti July 2006 start
                    GoalValtoShow$ = ""
                    DV2Show(Value, GoalValtoShow$)
                    'Select Case Value
                    '    Case 100 To 1000
                    '        GoalValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                    '    Case 10 To 100
                    '        GoalValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '    Case -10 To 10
                    '        GoalValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
                    '        'mti Validation changes  'for % value below 10
                    '        If Asc(Right(GoalValtoShow, 1)) = 48 Then  '48 = "0"
                    '            GoalValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '        End If
                    '        'mti Validation changes
                    '    Case -100 To -10
                    '        GoalValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                    '    Case -1000 To -100
                    '        GoalValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                    '    Case Else
                    '        GoalValtoShow = RSet(VB6.Format(Value, "###,###,###,###"), 15)
                    'End Select
                    'mti July 2006  end
                    If Trim(GoalValtoShow) <> "" Then
                        grd_TableDisplay.Rows(rr% + 8).Cells(cno% + 1).Text = Trim(GoalValtoShow)  '+ "(" + TrendCalc(cc%) + ")"
                    End If
                End If


            Next rr%


            'ForeCasts
            Dim ValtoShow As Object

            For cc% = 1 To COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown)
                cno% = (gg% - 1) * COUNTESDG / (Session("SDGFilesSelected") * MultiGraphsShown) + gg% * 2 + cc%

                'For rr% = Offset_SDGbase To Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
                For rr% = 1 To Session("LastYear") - FirstYearSDG + 1 'Offset_SDGbase + SDG_Base2Target '1 To Timepoints%
                    'grd_TableDisplay.col = 0
                    'grd_TableDisplay.row = (rr% - Offset_SDGbase) + 8 '4 '3
                    'grd_TableDisplay.col = cc%
                    grd_TableDisplay.Rows(rr% + 8).Cells(cno%).Text = ""
                    dataval = Nothing
                    IFsVal = Nothing
                    Value = Nothing
                    If rr% >= Offset_SDGbase + SDGBase2IFsBase Then
                        'jrs 2011/11/24 IFsVal = PlotMulti(rr% - (Offset_SDGbase + SDGBase2IFsBase) + 1, cc% + (gg - 1) * COUNTESDG / Session("SDGFilesSelected"), gg%)
                        IFsVal = Session("SDGPLOTSDG")(rr% - (Offset_SDGbase + SDGBase2IFsBase) + 1, gg)
                    End If


                    Value = IFsVal

                    If IsNumeric(Value) Then
                        'mti July 2006 start
                        DataValtoShow$ = ""
                        DV2Show(Value, ValtoShow)
                        'Select Case Value
                        '    Case 100 To 1000
                        '        ValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                        '    Case 10 To 100
                        '        ValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                        '    Case -10 To 10
                        '        ValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
                        '        'mti Validation changes  'for % value below 10
                        '        If Asc(Right(ValtoShow, 1)) = 48 Then  '48 = "0"
                        '            ValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                        '        End If
                        '        'mti Validation changes
                        '    Case -100 To -10
                        '        ValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
                        '    Case -1000 To -100
                        '        ValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
                        '    Case Else
                        '        ValtoShow = RSet(VB6.Format(Value, "###,###,###,###"), 15)
                        'End Select
                        'mti July 2006 end
                    Else
                        ValtoShow = Nothing
                    End If

                    If IsNumeric(ValtoShow) Then 'IFs Value
                        grd_TableDisplay.Rows(rr% + 8).Cells(cno%).Text = ValtoShow
                    End If

                Next rr%
            Next cc%
        Next gg%



    End Sub

    Private Sub mnuGroupToggle_Click()
        FlagGroup = Not FlagGroup
        Session("SDGGroups") = FlagGroup
        Session("SDGEstimates") = FlagGroup

        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        'jrs 2005/11/24
        If FlagGroup Then
            'jrs 2006/08/04
            'Dim cswmItem As Coalesys.WebMenu.Item
            'cswmItem = cswmWebMenu.Groups("mnuHorizon").Items.Add()
            'cswmItem.ID = "mnuEstimates"
            'cswmItem.Caption = "Use Estimates for Group History"
            'If Session("SDGEstimates") Then
            '    cswmItem.IconImage = "checkbox.gif"
            'Else
            '    cswmItem.IconImage = "blank.gif"
            'End If
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Enabled = True
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Caption = "Using estimation to fill holes in group data"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"

            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = True
        Else
            'jrs 2006/08/04 cswmWebMenu.Groups("mnuHorizon").Items.Remove("mnuEstimates")
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Enabled = False
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Caption = "Not filling holes in group data"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"

            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = False
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
            Session("SDGEstimatesMRandC") = False
        End If

        'mti June 01, 2005
        cmb_Dim1.Enabled = True
        Session("SDGCompare") = False
        cswmWebMenu.Groups("mnuSwitch").Items("mnuCompare").Caption = "Compare"
        'jrs 2005/08/31 cmdScaleMulti.Visible = False

        DimLoad()
        If FlagGroup Then
            If Indicator_BubbleGraph(IIndex) = "YES" Then
                optBubble.Visible = True
            Else
                optBubble.Visible = False
            End If
            cswmWebMenu.Groups("Switch").Items("mnuGroups").Caption = "Use Countries"
            If Session("SDGGroupIdx") = Nothing Then
                cmb_Dim1.SelectedIndex = 0
            Else
                cmb_Dim1.SelectedIndex = Session("SDGGroupIdx")
                Session.Remove("SDGGroupIdx")
            End If
        Else
            optBubble.Visible = False
            cswmWebMenu.Groups("Switch").Items("mnuGroups").Caption = "Use Groups"
            'jrs 2017/08/28
            If Not (Session("SDGbackToRegIdx") Is Nothing) Then
                cmb_Dim1.SelectedIndex = Session("SDGbackToRegIdx")
            Else
                cmb_Dim1.SelectedIndex = 85
            End If
        End If
        cmb_Dim1_SelectedIndexChanged(Me, New EventArgs)

        'jrs 2005/08/31 mnuTitle.Caption = "Show Title"

        'cmb_Dim1.ListIndex = 0
        'jrs 2011/05/06
        If Session("SDGGroups") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                'jam 2021/12/12
                ' Support.FormManager.Show(Me, "frm_SDG.aspx?Group=" & cmb_Dim1.SelectedValue)

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDG.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDG", cmb_Dim1.SelectedValue)

                Response.Redirect(vurl)

            Else
                'jam 2021/12/12
                'Support.FormManager.Show(Me, "frm_SDG.aspx?Group=" & Session("MMGroup"))


                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDG.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDG", Session("MMGroup"))

                Response.Redirect(vurl)

            End If
        Else
            'jrs 2017/08/28 Support.FormManager.Show(Me, "frm_SDG.aspx?Country=KE")
            If Session("MMCountry") Is Nothing Then
                'jam 2021/12/12
                'Support.FormManager.Show(Me, "frm_SDG.aspx?Country=KE")

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDG.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDG", "KE")

                Response.Redirect(vurl)


            Else
                'jam 2021/12/12
                'Support.FormManager.Show(Me, "frm_SDG.aspx?Country=" & Session("MMCountry"))

                Vpath = Request.GetFriendlyUrlFileVirtualPath

                Vext = Request.GetFriendlyUrlFileExtension

                vfriendlyurl = FriendlyUrl.Resolve("~/frm_SDG.aspx")


                vurl = FriendlyUrl.Href("~/frm_SDG", Session("MMCountry"))

                Response.Redirect(vurl)

            End If
        End If

    End Sub

    Private Sub mnucomparelist_Click()
        'jrs 2005/12/05
        ''jrs 2005/11/23 this flag allows to click on single without going back into compare
        'If TrickFlag Then
        '    Exit Sub
        'Else
        '    TrickFlag = True
        'End If

        If Not Session("SDGCompare") Then

            'jrs 2005/09/01 lblFrameCompareTitle.Caption = ""
            'jrs 2005/09/01 lblFrameCompareTitle1.Caption = ""
            'jrs 2005/09/01 lblFrameCompareTitle2.Caption = ""
            'jrs 2005/09/01 mnuTitle.Caption = "Show Title"

            RefreshFrameMultiple()

            'Frame2.Visible = False  'chart/data/bubble options
            Frame2.Visible = True 'chart/data/bubble options

            Session("SDGCompare") = True
            cswmWebMenu.Groups("mnuSwitch").Items("mnuCompare").Caption = "Single"
            cmb_Dim1.Enabled = False
            'lblJS.Visible = False 'later
            'jrs 2005/09/01 Pesgo1.Visible = False
            grd_TableDisplay.Visible = False

            'hide single progress label
            ProgressStr(1) = ""
            'jrs 2005/09/01 lblProgressRate.Caption = ""
            'jrs 2005/09/01 lblProgressRate.BackColor = QBColor(15)

            'jrs 2005/09/01 cmdScaleMulti.Caption = "Use Common Scale"
            'jrs 2005/09/01 cmdScaleMulti.Visible = True
            'show combined legends


            'For i2 = 1 To COUNTESDG

            '    lblmultilegendGr(i2 + 2).BackColor = Pego2.SubsetColors(i2 + 2)
            '    lblmultilegendTxt(i2 + 2).Caption = Pego2.SubsetLabels(i2 + 2)
            '    lblmultilegendGr(i2 + 2).Visible = True
            '    lblmultilegendTxt(i2 + 2).Visible = True
            '    If i2 + 2 = 4 Then Exit For
            'Next i2

            '    mnuFile.Enabled = False

            'jrs 2005/11/15
            lstcountries.Enabled = False
            'jrs 2005/11/22 lstFiles.Enabled = False
            'jrs 2005/12/05
            lstFiles.Enabled = False
            cmbIndicator.Enabled = False
            cmbTarget.Enabled = False
            cmbGoal.Enabled = False
            cswmWebMenu.Groups("Switch").Items("mnuGroups").Enabled = False
            cswmWebMenu.Groups("mnuHorizon").ShowMenuBarItem = False
            'jrs 2006/08/03
            'If Session("SDGGroups") Then
            '    cswmWebMenu.Groups("mnuEstimates").ShowMenuBarItem = False
            'End If
        Else

            Session("SDGCompare") = False
            cswmWebMenu.Groups("mnuSwitch").Items("mnuCompare").Caption = "Compare"
            'jrs 2005/09/01 mnuTitle.Caption = "Show Title"
            Frame2.Visible = True 'chart/data/bubble options

            cmb_Dim1.Enabled = True
            RefreshForm()
            'jrs 2005/09/01 cmdScaleMulti.Visible = False
            '    mnuFile.Enabled = True

            'jrs 2005/11/15
            lstcountries.Enabled = True
            lstFiles.Enabled = True
            'jrs 2005/12/05
            cmbIndicator.Enabled = True
            cmbTarget.Enabled = True
            cmbGoal.Enabled = True
            cswmWebMenu.Groups("Switch").Items("mnuGroups").Enabled = True
            cswmWebMenu.Groups("mnuHorizon").ShowMenuBarItem = True
            'jrs 2006/08/05
            'If Session("SDGGroups") Then
            '    cswmWebMenu.Groups("mnuEstimates").ShowMenuBarItem = True
            'End If
            'jrs 2011/10/17
            'lblJS.Text = Replace(lblJS.Text, "AnyChartDashboardLayoutSDG.aspx", "AnyChartDataforSDG.aspx")
        End If
        PackJScritpBuilder()
    End Sub

    Private Sub RefreshFrameMultiple()
        Dim tgcount%

        'mti June 01, 2005
        Dim VarName$, VarNO%, CountryOrGroup$, SecondDim$, ThirdDim 'jrs 2006/05/17 Dim3
        'Dim VarName$, VarNO%, CountryOrGroup$, SecondDim$

        'jrs 2005/11/24
        Dim Countries2(5) As String

        MultiGraphsShown = 0


        VarName$ = Indicator_IfsVar(IIndex)

        Dim Region As String, found As String, Msg As String
        Dim CGIdx As Integer

        If VarName$ = "" Then
            'no IFsVar equivalent of SDG Indicator- History Only

            Region$ = cmb_Dim1.SelectedValue
            '    If Region$ = "" Then Region$ = Sel2$

            '   Check Validity of Group or Country
            '   ----------------------------------
            ClearGrid()
            found$ = "False"

            If FlagGroup = True Then 'Set up for groups
                GroupPoint = gGetGroupIdx(Region, True, Me)
            Else
                DimPoint = gGetDimIdx(1, Region, True, Me)
            End If

            COUNTESDG = 1
            Session("COUNTE") = COUNTESDG
            CRName(COUNTESDG) = Region$

            '    If Trim(Indicator_SDGHistAnalog(IIndex)) = "" Then
            '        TableName$ = "Series" + VHistoricAnalog(SEL)
            '    Else
            TableName$ = "Series" + Trim(Indicator_SDGHistAnalog(IIndex))
            'End If
            '    FirstYear = FirstYearHistory
            FirstYearSDG = FirstYearHistory
            Session("LastYear") = LastYearHistory
            Call History_Filldata()

            '    FirstYear = FirstYearForecast
            Session("LastYear") = Session("LastYearForecast")
            '    DrawSDGgraph
            'mti SDG Multi to one Proc June 28, 2005
            'DrawSDGgraphMulti2 '(Pego2)
            DrawSDGgraphMulti(1)  '(Pego2)

            'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
            'jrs 2005/09/01 Pego2.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")

            'if calc in history
            'check aggregation; take agrule from datadict

            tgcount = 0
            For CGIdx = 0 To lstcountries.Items.Count - 1


                If lstcountries.Items(CGIdx).Selected Then


                    Region$ = lstcountries.Items(CGIdx).Value 'cmb_Dim1.Text
                    'JRS 2017/08/01 ClearGrid()
                    found$ = "False"

                    If FlagGroup = True Then 'Set up for groups
                        GroupPoint = gGetGroupIdx(Region, True, Me)
                    Else
                        DimPoint = gGetDimIdx(1, Region, True, Me)
                    End If

                    COUNTESDG = COUNTESDG + 1 'JRS 2017/08/01
                    Session("COUNTE") = COUNTESDG
                    CRName(COUNTESDG) = Region$
                    TableName$ = "Series" + Trim(Indicator_SDGHistAnalog(IIndex))
                    FirstYearSDG = FirstYearHistory
                    Session("LastYear") = LastYearHistory
                    Call History_Filldata()
                    Session("LastYear") = Session("LastYearForecast")

                    Select Case tgcount
                        Case 0
                            'DrawSDGgraphMulti3

                            'mti SDG Multi to one Proc June 28, 2005
                            'DrawSDGgraphMulti3 '(Pego2)
                            'DrawSDGgraphMulti(2) '(Pego2)

                            'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                            'jrs 2005/09/01 Pego3.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                            MultiGraphLabels(2) = lstcountries.Items(CGIdx).Value

                        Case 1
                            'mti SDG Multi to one Proc June 28, 2005
                            'DrawSDGgraphMulti4 '(Pego2)
                            'DrawSDGgraphMulti(3)  '(Pego2)

                            'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                            'jrs 2005/09/01 Pego4.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")

                            MultiGraphLabels(3) = lstcountries.Items(CGIdx).Value

                        Case 2

                            'mti SDG Multi to one Proc June 28, 2005
                            'DrawSDGgraphMulti5 '(Pego2)
                            'DrawSDGgraphMulti(4) '(Pego2)

                            'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                            'jrs 2005/09/01 Pego5.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")

                            MultiGraphLabels(4) = lstcountries.Items(CGIdx).Value

                        Case 3
                            'mti SDG Multi to one Proc June 28, 2005
                            'DrawSDGgraphMulti6 '(Pego2)
                            'DrawSDGgraphMulti(5)  '(Pego2)

                            'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                            'jrs 2005/09/01 Pego6.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                            MultiGraphLabels(5) = lstcountries.Items(CGIdx).Value

                        Case 4
                            'mti SDG Multi to one Proc June 28, 2005
                            'DrawSDGgraphMulti7 '(Pego2)
                            'DrawSDGgraphMulti(6) '(Pego2)

                            'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                            'jrs 2005/09/01 Pego7.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                            MultiGraphLabels(6) = lstcountries.Items(CGIdx).Value


                        Case Else
                    End Select
                    tgcount = tgcount + 1
                    DrawSDGgraphMulti(tgcount + 1)

                    'lblDescribe.Visible = False
                    If GraphMSG$ <> "" Then
                        MsgBox(GraphMSG$)
                        tgcount = tgcount - 1
                        '            cmb_Dim1.Text = Current_Country_Group$
                    End If
                End If
                If tgcount = 5 Then Exit For

            Next



        Else
            Dim chk As Integer
            '--------------------
            Select Case Indicator_Transform(IIndex)
                Case "Division"
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "/" + Indicator_Var2(IIndex)
                        End If

                    End If

                Case "Percent"
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        'check the validity of variable 2 in ifsvar
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "%" + Indicator_Var2(IIndex)
                        End If

                    End If
                Case "Absolute" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "*" + Indicator_Var2(IIndex) + "/100"
                        End If

                    End If
                Case "Per1000" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "/" + Indicator_Var2(IIndex) + "*1000"
                        End If
                    End If
                Case "InvCDALF" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "*(1-CDALF)+" + Indicator_Var2(IIndex)
                        End If
                    End If
                Case "Sum" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "+" + Indicator_Var2(IIndex)
                        End If
                    End If
                Case "Sub" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "-" + Indicator_Var2(IIndex)
                        End If
                    End If
                Case "LossIndex" 'jrs 2017/02/02
                    If Indicator_Var2(IIndex) <> "" Then
                        chk = 0
                        chk = IfsVars.Item(Indicator_Var2(IIndex)).Index
                        If chk > 0 Then
                            VarName$ = VarName$ + "+" + Indicator_Var2(IIndex)
                        End If
                    End If

                Case Else

            End Select
            SecondDim$ = Trim(Indicator_IfsVarDim2(IIndex)) '""
            VarNO = Indicator_IfsVarNo(IIndex)  'do not need it
            'jrs 2020/06/25
            ThirdDim = Trim(Indicator_IfsVarDim3(IIndex)) '""


            'draw cmb_dim1
            ClearGrid()
            If InputSDGForecast(VarName, cmb_Dim1.SelectedValue, SecondDim$, ThirdDim) Then
                'jrs 2005/11/24
                Countries2(0) = Countries(0)

                '            DrawSDGgraph
                '            Dim pg As Pego
                '            pg=PECREATE(PECONTROL_GRAPH ,0,
                '            pg = CreateObject(Pego2)
                'mti SDG Multi to one Proc June 28, 2005
                'DrawSDGgraphMulti2 '(Pego2)
                DrawSDGgraphMulti(1)  '(Pego2)

                'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                'jrs 2005/09/01 Pego2.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")

            End If
            'If GraphMSG$ <> "" Then
            '    MsgBox GraphMSG$
            '
            ''            cmb_Dim1.Text = Current_Country_Group$
            'End If



            tgcount = 0
            For CGIdx = 0 To lstcountries.Items.Count - 1


                If lstcountries.Items(CGIdx).Selected Then
                    'jrs 2005/09/05 ClearGrid()
                    If InputSDGForecast(VarName, lstcountries.Items(CGIdx).Value, SecondDim$, ThirdDim) Then


                        '            DrawSDGgraph
                        Select Case tgcount
                            Case 0
                                'mti SDG Multi to one Proc June 28, 2005
                                'DrawSDGgraphMulti3 '(Pego2)
                                'DrawSDGgraphMulti(2)  '(Pego2)

                                'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                                'jrs 2005/09/01 Pego3.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                                MultiGraphLabels(2) = lstcountries.Items(CGIdx).Value

                            Case 1
                                'mti SDG Multi to one Proc June 28, 2005
                                'DrawSDGgraphMulti4 '(Pego2)
                                'DrawSDGgraphMulti(3) '(Pego2)

                                'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                                'jrs 2005/09/01 Pego4.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                                MultiGraphLabels(3) = lstcountries.Items(CGIdx).Value


                            Case 2
                                'mti SDG Multi to one Proc June 28, 2005
                                'DrawSDGgraphMulti5 '(Pego2)
                                'DrawSDGgraphMulti(4) '(Pego2)

                                'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                                'jrs 2005/09/01 Pego5.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                                MultiGraphLabels(4) = lstcountries.Items(CGIdx).Value


                            Case 3
                                'mti SDG Multi to one Proc June 28, 2005
                                'DrawSDGgraphMulti6 '(Pego2)
                                'DrawSDGgraphMulti(5) '(Pego2)

                                'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                                'jrs 2005/09/01 Pego6.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                                MultiGraphLabels(5) = lstcountries.Items(CGIdx).Value

                            Case 4
                                'mti SDG Multi to one Proc June 28, 2005
                                'DrawSDGgraphMulti7 '(Pego2)
                                'DrawSDGgraphMulti(6) '(Pego2)

                                'jrs 2005/09/01 PegoMulti.PEsavetofile(DataFileDir$ + "PegoMulti.pg")
                                'jrs 2005/09/01 Pego7.PEloadfromfile(DataFileDir$ + "PegoMulti.pg")
                                MultiGraphLabels(6) = lstcountries.Items(CGIdx).Value

                            Case Else
                        End Select
                        tgcount = tgcount + 1
                        'lblDescribe.Visible = False
                        DrawSDGgraphMulti(tgcount + 1)
                        'jrs 2005/11/24
                        Countries2(tgcount) = Countries(0)
                    End If
                    If GraphMSG$ <> "" Then
                        MsgBox(GraphMSG$)
                        tgcount = tgcount - 1
                        '            cmb_Dim1.Text = Current_Country_Group$
                    End If
                End If
                If tgcount = 5 Then Exit For

            Next

        End If  'varname=""

        MultiGraphsShown = tgcount + 1 '+1 for the country being compared
        Session("SDGMultiGraphsShown") = MultiGraphsShown

        'jrs 2005/09/01 
        MultiGraphLabels(1) = cmb_Dim1.Text
        'MultiGraphLabels(2) = lblPego3.Text
        'MultiGraphLabels(3) = lblPego4.Text
        'MultiGraphLabels(4) = lblPego5.Text
        'MultiGraphLabels(5) = lblPego6.Text
        'MultiGraphLabels(6) = lblPego7.Text

        'jrs 2011/10/17
        'lblJS.Text = Replace(lblJS.Text, "AnyChartDataforSDG.aspx", "AnyChartDashboardLayoutSDG.aspx")

        'jrs 2005/09/01 Line1.Visible = Pego5.Visible
        'Option1(0).Value = True
        'grd_TableDisplay.Visible = False
        'Pego1.Visible = True

        'jrs 2005/11/24
        Session("SDGCountries") = Countries2
    End Sub

    Private Sub DrawSDGgraphMulti(ByVal mgno%) '(PegoMulti As Pego) '(PegoMulti As PEGO32CLib.Pego)
        'first mini graph(or the graph being compared) in the compare list
        ReDim ProgressStr(COUNTESDG)
        Dim j%
        Dim TSlope
        Dim TStart%, TEnd%
        ReDim TrendCalc(COUNTESDG)

        ReDim ActualProgress(COUNTESDG)
        ReDim ReqdProgress(COUNTESDG)


        'ReDim BaseVal(COUNTE)
        'ReDim Curval(COUNTE)
        'ReDim SDGMsg(COUNTE)

        Session("SDGLasYear_is_LowCommonYear") = LasYear_is_LowCommonYear$
        Session("SDGIFsLowCommonYear") = IFsLowCommonYear
        Session("SDGIndicator_TargetYear") = Indicator_TargetYear
        Session("SDGIndicator_BaseYear") = Indicator_BaseYear
        Session("SDGIIndex") = IIndex
        Session("SDGDisplayLastYearSDG") = DisplayLastYearSDG
        Session("SDGCOUNTESDG") = COUNTESDG
        Session("SDGFlagGroup") = FlagGroup

        Session("SDGEarliestYearWithData") = EarliestYearWithData
        Session("SDGTrendCalc") = TrendCalc
        Session("SDGsgrpEarliestYearWithData") = sgrpEarliestYearWithData

        Session("SDGDisplayEarliestYearWithData") = DisplayEarliestYearWithData
        Session("SDGOffset_Earliest") = Offset_Earliest

        Session("SDGGraphMSG") = GraphMSG
        Session("SDGMsg") = SDGMsg
        Session("SDGProgressStr") = ProgressStr
        Session("SDGActualProgress") = ActualProgress
        Session("SDGReqdProgress") = ReqdProgress

        Session("SDGIndicator_TargetValue") = Indicator_TargetValue
        Session("SDGGoalVal") = GoalVal

        Session("SDGBaseVal") = BaseVal

        'jrs 2017/07/24
        Session("SDGPLOTSDGHR") = PLOTSDGHR

        Session("SDGcRunfile") = cRunfile

        Session("SDGPLOTSDG") = PlotSDG

        Session("SDGPlotMulti") = PlotMulti
        Session("SDGPlotSDGHMulti") = PlotSDGHMulti
        Session("SDGmultiProgressStr") = multiProgressStr
        Session("SDGmultiActualProgress") = multiActualProgress
        Session("SDGmultiReqdProgress") = multiReqdProgress
        Session("SDGmultiBaseVal") = multiBaseVal
        Session("SDGmultiCurval") = multiCurval
        Session("SDGmultiGoalVal") = multiGoalVal

        Session("SDGMultiGraphsShown") = MultiGraphsShown

        'jrs 2017/07/31
        'If mgno% = 1 Then
        '    For rr% = 1 To LastYearHistory - FirstYearHistory
        '        PLOTSDGH(rr%, 0) = PLOTSDGH(rr%, 1)
        '    Next
        'Else
        '    For rr% = 1 To LastYearHistory - FirstYearHistory
        '        PLOTSDGH(rr%, mgno%) = PLOTSDGH(rr%, 1)
        '        PLOTSDGH(rr%, 1) = PLOTSDGH(rr%, 0)
        '    Next
        'End If
        Session("SDGPLOTSDGH") = PLOTSDGH

        'If mgno% = 1 Then
        '    Session("LastHistVal")(0) = Session("LastHistVal")(1)
        '    Session("LastHistYear")(0) = Session("LastHistVal")(1)
        'Else
        '    Session("LastHistVal")(mgno%) = Session("LastHistVal")(1)
        '    Session("LastHistYear")(mgno%) = Session("LastHistVal")(1)
        '    Session("LastHistVal")(1) = Session("LastHistVal")(0)
        '    Session("LastHistYear")(1) = Session("LastHistVal")(0)
        'End If

    End Sub


    Private Sub lstFiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFiles.SelectedIndexChanged
        If cmbGoal.Items.Count > 0 Then 'avoid refereshing during initial load and setting of file list box
            RefreshForm()
        End If
    End Sub

    'jrs 2005/10/24
    Private Sub SituationMap()

        'jam 2021/12/12
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String

        Session("MapChosenTable") = "TimeSeries"
        Session("RecordSourceDef") = "Social"

        'jrs 2005/10/25 DataFieldDef$ = "Freedom"

        If MapFormSeries$ <> "" Then
            Session("MDGMapFieldName") = MapFormSeries$
        Else
            Exit Sub
        End If
        'jrs 2017/09/15 Session("mapParent") = "frm_SDG.aspx"
        If Session("SDGGroups") = True Then
            Session.Remove("MMCountry")
            If Session("MMGroup") = Nothing Then
                Session("mapParent") = "frm_SDG.aspx?Group=" & cmb_Dim1.SelectedValue
            Else
                Session("mapParent") = "frm_SDG.aspx?Group=" & Session("MMGroup")
            End If
        Else
            If Session("MMCountry") Is Nothing Then
                Session("mapParent") = "frm_SDG.aspx?Country=KE"
            Else
                Session("mapParent") = "frm_SDG.aspx?Country=" & Session("MMCountry")
            End If
        End If

        'jam 2021/12/12
        'Support.FormManager.StateStore(Me)
        'Support.FormManager.Show(Me, "frm_mapViewer.aspx")

        Vpath = Request.GetFriendlyUrlFileVirtualPath

        Vext = Request.GetFriendlyUrlFileExtension

        vfriendlyurl = FriendlyUrl.Resolve("~/frm_mapViewer.aspx")


        vurl = FriendlyUrl.Href("~/frm_mapViewer")

        Response.Redirect(vurl)


    End Sub

    'jrs 2005/11/23
    Private Sub mnuUseEstimates_Click()
        'mti 10/23/05 new proc
        Session("SDGEstimates") = Not Session("SDGEstimates")
        If Session("SDGEstimates") Then
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Caption = "Using estimation to fill holes in group data"

            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = True
        Else
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstimates").Caption = "Not filling holes in group data"

            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Enabled = False
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
            Session("SDGEstimatesMRandC") = False
        End If
        RefreshForm()
    End Sub

    Private Function Find_Country_Year_Estimate4Group(ByVal tblDriver As DataTable, ByVal Cntry$, ByVal yr$, ByVal regIntrcpt As Single, ByVal regSlop As Single, ByVal regShft As Single)
        Dim DrvFld As String, DrvVal As String
        'mti 10/23/05 - new sub
        'If yr$ = "1990" Then Stop
        Find_Country_Year_Estimate4Group = Nothing
        'mti 11/09/05
        'If regIntrcpt <> 0 And regSlop <> 0 And val(yr$) > Indicator_BaseYear(iIndex) - 5 Then
        If regIntrcpt <> 0 And regSlop <> 0 And Val(yr$) >= Indicator_BaseYear(IIndex) - 5 Then
            Find_Country_Year_Estimate4Group = regIntrcpt + Val(yr$) * regSlop
            Find_Country_Year_Estimate4Group = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), Find_Country_Year_Estimate4Group))
        Else
            If Indicator_DriverTbl(IIndex) <> "" Then
                DrvFld$ = Trim(CStr(yr$))
                'jrs 2014/04/14
                'If Val(yr$) > Session("FirstYear") Then
                '    DrvFld$ = "MostRecent"
                'End If

                'jrs 2011/11/23
                If checkField(tblDriver, CInt(DrvFld$)) Then
                    DrvVal = SeekValue(tblDriver, Cntry$, DrvFld$) 'GDP of that particular year
                End If
            End If
            If IsNumeric(DrvVal) And Indicator_Function(IIndex) <> "" Then
                If Indicator_Conversion(IIndex) = "Div" And Indicator_ConversionNumber(IIndex) <> 0 Then
                    DrvVal = DrvVal / Indicator_ConversionNumber(IIndex)
                End If
                Call XYTABL(Indicator_Function(IIndex), DrvVal, Find_Country_Year_Estimate4Group)
                Find_Country_Year_Estimate4Group = Find_Country_Year_Estimate4Group + regShft
                Find_Country_Year_Estimate4Group = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), Find_Country_Year_Estimate4Group))
            End If
        End If



    End Function

    Private Function FindFinYear(ByVal sFileName$) As Integer
        'mti July 2006 New proc
        'Open File if necessary

        Dim fNum As Integer

        'Low Common Year
        Dim VLN As Object, Number As Single, RECORD As Object, VarVal As Object

        If Session("mnuSQLite") = "True" Then
            Dim working As IStorage
            Dim storageSvc As IStorageService = New StorageService(sFileName$)
            working = storageSvc.Open()
            VarVal = working.LastYear
        Else
            fNum = FreeFile()
            FileOpen(fNum, sFileName, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)
            VLN = VarPoint(PosModYear)
            RECORD = VLN * 4 - 3
            FileGet(fNum, Number, RECORD)
            VarVal = Number!
        End If
        FindFinYear = VarVal - FirstYearSDG + 1 '- (iFirstYear - iFirstYearOffSet) + 1

        FileClose(fNum)
    End Function

    Private Sub Find_Group_Base_Earliest_MostRecent(ByVal valtbl As DataTable, ByVal agcount%)
        Dim rpointer As Integer
        Dim valtblG As DataTable
        Dim GDPPCPBase As Object

        'mti July 2006 -new proc
        Dim reg As New RegressionObject
        Dim x#, y#

        Dim OnlyYear%, OnlyVal, ShiftVal

        Dim pt%, k%

        Dim YearVal%
        Dim VarValBase, VarValLatest
        Dim tblDriver As DataTable, tblDriver1 As DataTable

        If Indicator_DriverTbl(IIndex) <> "" Then
            SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM [" & Indicator_DriverTbl(IIndex) & "]", "Country", tblDriver, Session("sPwdSDG"))
            If Indicator_DriverTbl1(IIndex) <> "" Then
                SetDataTablefromAccess(Session("dbSDG"), "SELECT * FROM [" & Indicator_DriverTbl1(IIndex) & "]", "Country", tblDriver1, Session("sPwdSDG"))
            End If


        Else
            Find_Group_Base_Latest(valtbl, CInt(agcount))
            'mti 25 July, 2006
            Exit Sub
        End If

        'mti SDG june 28, 2005 data
        'EarliestYearWithData = 9999
        sgrpEarliestYearWithData% = 9999

        'LatestYear = FirstYearForecast  'comment this line after discussion, if necessary 'mti March 23, 2005



        For k = 0 To agcount - 1
            grCntryBaseVal(k) = DBNull.Value
            grCntryLYVal(k) = DBNull.Value

            grCntryBaseVal(k) = EstimateFromClosest(Indicator_BaseYear(IIndex), valtbl, tblDriver, Countries(k),
                         Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))

            If IsDBNull(grCntryBaseVal(k)) Then
                If Indicator_DriverTbl1(IIndex) <> "" Then
                    grCntryBaseVal(k) = EstimateFromClosest(Indicator_BaseYear(IIndex), valtbl, tblDriver1, Countries(k),
                          Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
                End If
            End If

            'jrs 2011/09/28 old update not taken
            'mti November 2006 start 16
            'grCntryLYVal(k) = EstimateFromMostRecent(FirstYearForecast, valtbl, tblDriver, Countries(k), _
            '             Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))

            rpointer = IFsRegs.Item(getRegionFromCtry(Countries(k))).Index

            GDPPCPBase = FindVarValForCountryYear("GDPPCP", rpointer, FirstYearForecast)


            grCntryLYVal(k) = EstimateFromMostRecentRevBBH(GDPPCPBase, FirstYearForecast, valtbl, tblDriver, Countries(k),
                        Indicator_Function(IIndex), Session, Me, Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))

            'If IsDBNull(grCntryLYVal(k)) Then
            '    If Indicator_DriverTbl1(IIndex) <> "" Then
            '        grCntryLYVal(k) = EstimateFromMostRecent(FirstYearForecast, valtbl, tblDriver1, Countries(k), _
            '                     Indicator_Function(IIndex), Indicator_Max(IIndex), Indicator_Min(IIndex), Indicator_Conversion(IIndex), Indicator_ConversionNumber(IIndex))
            '    End If
            'End If

            'mti November 2006

            If Not IsDBNull(grCntryBaseVal(k)) Then
                grCntryBaseVal(k) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), grCntryBaseVal(k)))
            End If

            If Not IsDBNull(grCntryLYVal(k)) Then
                grCntryLYVal(k) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), grCntryLYVal(k)))
            End If


            '        Debug.Print "est", k, gGroupMembrs(k + 1), grCntryBaseVal(k), Countries(k)
        Next k

        'mti 10/23/05
        If Indicator_DriverTbl(IIndex) <> "" Then
            tblDriver = Nothing
            If Indicator_DriverTbl1(IIndex) <> "" Then
                tblDriver1 = Nothing
            End If

        End If
        'mti 10/23/05 end

    End Sub


    Private Function FindVarValForCountryYear(ByVal IFsVar, ByVal rpointer, ByVal YearVal)
        'mti August 29, 2006 whole procedure
        Dim PlotPop(1) 'PlotPop(1, 1)

        FindVarValForCountryYear = DBNull.Value

        Dim valtblG As DataTable
        'Dim rpointer As Object
        'rpointer = SeekValue(valtblG, Country, "NewRegion")
        'rpointer = IFsRegs.Item(Country).Index 'jrs 2006/08/04
        'If rpointer < 0 Then
        '    SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT * FROM [" & Regionalization & "] WHERE Country = """ & Country & """", "", valtblG)
        '    rpointer = valtblG.Rows(0).Item("NewRegion")
        'End If
        Dim VarIdx As Integer = IfsVars.Item(IFsVar).Index

        gLoadForDisplayNET(IFsVar, VarIdx, "", rpointer, "", -1, "", -1, "", 1, True, True, False, 1, PlotPop, 1, 1,
        YearVal, YearVal, YearVal - FirstYearForecast, True, Session("bLoadForDisplaySuccess"), Me,
        Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FinYear"),
        Session("IFs_MDB2Active"), Session("CurrencyVal"))
        'gLoadForDisplay(IFsVar, VarIdx, "", rpointer, "", -1, "", -1, "", 1, True, True, False, 1, PlotPop, 1, 1,
        'YearVal, YearVal, YearVal - FirstYearForecast, True, Session("bLoadForDisplaySuccess"), Me,
        'Session("RunFiles"), Session("PutFdrvRun"), Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
        'Session("IFs_MDB2Active"), Session("CurrencyVal"))

        'Dim YearIdx As Integer
        'YearIdx = YearVal - FirstYearForecast + 1
        FindVarValForCountryYear = PlotPop(1)


    End Function

    Private Function EstimateFromMostRecent(ByVal Year2Estimate As Integer, ByRef valtbl As DataTable, ByRef tblDriver As DataTable,
    ByVal Country As String, ByVal FuncName$,
    Optional ByVal MaxV As Object = Nothing, Optional ByVal MinV As Object = Nothing, Optional ByVal Conversion As String = "",
    Optional ByVal ConversionNumber As Double = 0)
        'mti July 2006 -new proc

        'Dim db1 As Database
        'Dim YearVal%

        EstimateFromMostRecent = DBNull.Value

        Dim idxVal As Integer = 0
        Dim LatestYearWithData As Integer = 0
        Dim Year2EstimateData As Object = DBNull.Value
        Dim valRow As DataRow
        valRow = SeekRow(valtbl, Country)
        Dim j As Integer
        Dim YearVal As Integer
        Dim Latestdata As Object
        For j = 0 To valtbl.Columns.Count - 1
            YearVal = Val(valtbl.Columns(j).Caption)
            If YearVal > 0 Then 'field is an year
                Session("VarVal") = valRow.Item(j)
                If IsNumeric(Session("VarVal")) _
                    And YearVal > LatestYearWithData Then
                    LatestYearWithData = YearVal
                    Latestdata = Session("VarVal")
                    If YearVal = Year2Estimate Then
                        Year2EstimateData = Session("VarVal")
                    End If
                End If
            End If
        Next j


        '        Set db1 = OpenDatabase(IFsHistSeries_MDB, False, False, DataPW$)
        '        Set tbldriver = db1.OpenRecordset(drivertblname)
        '        tbldriver.Index = "Country"
        '        tbldriver.MoveFirst
        Dim drvRow As DataRow
        drvRow = SeekRow(tblDriver, Country)
        Dim DrvVal_EstYear As Object = DBNull.Value
        Dim DrvVal_LatestYear As Object = DBNull.Value
        Dim fld As Integer
        Dim fldName As String
        Dim DrvVal_EstYearP1, DrvVal_EstYearP2 As Object
        Dim DrvVal_EstYearM1, DrvVal_EstYearM2 As Object
        For fld% = 0 To tblDriver.Columns.Count - 1
            fldName$ = tblDriver.Columns(fld%).Caption
            If Trim(fldName$) = Trim(Str(LatestYearWithData)) Then
                DrvVal_LatestYear = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate)) Then
                DrvVal_EstYear = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate + 1)) Then
                DrvVal_EstYearP1 = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate + 2)) Then
                DrvVal_EstYearP2 = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate - 1)) Then
                DrvVal_EstYearM1 = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate - 2)) Then
                DrvVal_EstYearM2 = drvRow.Item(fldName$)
            End If

        Next fld%

        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearP1
        End If
        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearP2
        End If
        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearM1
        End If
        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearM2
        End If

        If Conversion = "Div" And ConversionNumber <> 0 Then
            If IsNumeric(DrvVal_LatestYear) Then
                DrvVal_LatestYear = DrvVal_LatestYear / ConversionNumber
            End If
            If IsNumeric(DrvVal_EstYear) Then
                DrvVal_EstYear = DrvVal_EstYear / ConversionNumber
            End If
        End If

        Dim Val_EstYear As Object
        Dim Val_LatestYear As Object
        Dim valdiff As Object

        If LatestYearWithData > 0 Then


            If (IsNumeric(DrvVal_EstYear)) And
                (IsNumeric(DrvVal_LatestYear)) Then
                Call XYTABL(FuncName, DrvVal_EstYear, Val_EstYear)
                Call XYTABL(FuncName, DrvVal_LatestYear, Val_LatestYear)

                valdiff = Val_LatestYear - Val_EstYear
                If Val_LatestYear > 0 Then
                    valdiff = (valdiff / Val_LatestYear) * (AMAX(0.00001, Latestdata))
                End If
                EstimateFromMostRecent = Latestdata - valdiff
            Else 'add to doc

                If IsNumeric(Year2EstimateData) Then
                    EstimateFromMostRecent = Year2EstimateData
                End If

            End If
        End If


        If Not IsNumeric(EstimateFromMostRecent) Then  'no value yet
            If (IsNumeric(DrvVal_EstYear)) Then
                Call XYTABL(FuncName, DrvVal_EstYear, EstimateFromMostRecent)
            End If
        End If



        If IsNumeric(EstimateFromMostRecent) And (IsNumeric(MaxV) And IsNumeric(MinV)) Then
            EstimateFromMostRecent = Amin(MaxV, AMAX(MinV, EstimateFromMostRecent))
        End If

        '    Set tbldriver = Nothing

    End Function

    Public Sub DV2Show(ByVal Value, ByRef DataValtoShow)
        'jrs 2011/11/24
        If Not IsNumeric(Value) Then
            Exit Sub
        End If
        'mti July 2006 - new proc
        Select Case Value
            Case 100 To 1000
                DataValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
            Case 10 To 100
                DataValtoShow = RSet(VB6.Format(Value, "###,###.00"), 15)
            Case -10 To 10
                DataValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
                'mti Validation changes  'for % value below 10
                If Asc(Right(DataValtoShow, 1)) = 48 Then  '48 = "0"
                    DataValtoShow = RSet(VB6.Format(Value, "###,###.0000"), 15)
                End If
                    'mti Validation changes
            Case -100 To -10
                DataValtoShow = RSet(VB6.Format(Value, "###,###.000"), 15)
            Case -1000 To -100
                DataValtoShow = RSet(VB6.Format(Value, "###,###.0"), 15)
            Case Else
                DataValtoShow = RSet(VB6.Format(Value, "###,###,###,###"), 15)
        End Select


    End Sub

    Private Function EstimateFromClosest(ByVal Year2Estimate As Integer, ByRef valtbl As DataTable, ByRef tblDriver As DataTable, ByVal Country As String, ByVal FuncName$,
    Optional ByVal MaxV As Object = Nothing, Optional ByVal MinV As Object = Nothing, Optional ByVal Conversion$ = "",
    Optional ByVal ConversionNumber As Double = 0)
        'mti July 2006 -new proc

        'Dim db1 As Database
        'Dim YearVal%

        EstimateFromClosest = DBNull.Value

        Dim idxTbl As Integer = 0
        Dim ClosestYearWithData As Integer = 0
        Dim valRow As DataRow
        valRow = SeekRow(valtbl, Country)
        Dim j As Integer
        Dim YearVal As Integer
        Dim ClosestData As Object

        For j = 0 To valtbl.Columns.Count - 1
            YearVal = Val(valtbl.Columns(j).Caption)
            If YearVal > 0 Then 'field is an year
                Session("VarVal") = valRow.Item(j)
                If IsNumeric(Session("VarVal")) Then
                    If Abs(YearVal - Val(Year2Estimate)) <=
                        Abs(ClosestYearWithData - Val(Year2Estimate)) Then
                        'equality will ensure later year ony when
                        'years are traversed from earliest to latest
                        If YearVal < sgrpEarliestYearWithData Then
                            'EarliestYearWithData = YearVal
                            sgrpEarliestYearWithData = YearVal
                        End If
                        If EarliestYearWithData = 0 Then
                            EarliestYearWithData = YearVal
                            If EarliestYearWithData >= Indicator_BaseYear(IIndex) Then
                                EarliestYearWithData = 0
                            End If
                        End If

                        ClosestYearWithData = YearVal
                        ClosestData = Session("VarVal")
                    End If
                End If
            End If
        Next j

        Dim drvRow As DataRow
        drvRow = SeekRow(tblDriver, Country)

        Dim DrvVal_EstYear As Object = DBNull.Value
        Dim DrvVal_ClosestYear As Object = DBNull.Value
        Dim fld As Integer
        Dim fldName As String
        Dim DrvVal_EstYearP1 As Object
        Dim DrvVal_EstYearP2 As Object
        Dim DrvVal_EstYearM1 As Object
        Dim DrvVal_EstYearM2 As Object
        For fld = 0 To tblDriver.Columns.Count - 1
            fldName$ = tblDriver.Columns(fld%).Caption
            If Trim(fldName$) = Trim(Str(ClosestYearWithData)) Then
                DrvVal_ClosestYear = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate)) Then
                DrvVal_EstYear = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate + 1)) Then
                DrvVal_EstYearP1 = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate + 2)) Then
                DrvVal_EstYearP2 = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate - 1)) Then
                DrvVal_EstYearM1 = drvRow.Item(fldName$)
            End If
            If Trim(fldName$) = Trim(Str(Year2Estimate - 2)) Then
                DrvVal_EstYearM2 = drvRow.Item(fldName$)
            End If

        Next fld%

        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearP1
        End If
        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearP2
        End If
        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearM1
        End If
        If Not IsNumeric(DrvVal_EstYear) Then
            DrvVal_EstYear = DrvVal_EstYearM2
        End If

        If Conversion = "Div" And ConversionNumber <> 0 Then
            If IsNumeric(DrvVal_ClosestYear) Then
                DrvVal_ClosestYear = DrvVal_ClosestYear / ConversionNumber
            End If
            If IsNumeric(DrvVal_EstYear) Then
                DrvVal_EstYear = DrvVal_EstYear / ConversionNumber
            End If
        End If


        Dim Val_EstYear As Object
        Dim Val_ClosestYear As Object
        Dim valdiff As Object
        If ClosestYearWithData > 0 Then
            If Year2Estimate = ClosestYearWithData Then
                EstimateFromClosest = ClosestData
            Else
                If (IsNumeric(DrvVal_EstYear)) And
                    (IsNumeric(DrvVal_ClosestYear)) Then
                    Call XYTABL(FuncName, DrvVal_EstYear, Val_EstYear)
                    Call XYTABL(FuncName, DrvVal_ClosestYear, Val_ClosestYear)
                    valdiff = Val_ClosestYear - Val_EstYear
                    If Val_ClosestYear > 0 Then
                        valdiff = (valdiff / Val_ClosestYear) * AMAX(0.00001, ClosestData)
                    End If
                    EstimateFromClosest = ClosestData - valdiff
                End If
            End If
        End If

        If Not IsNumeric(EstimateFromClosest) Then  'no value yet]
            If (IsNumeric(DrvVal_EstYear)) Then
                Call XYTABL(FuncName, DrvVal_EstYear, EstimateFromClosest)
            End If
        End If

        If IsNumeric(EstimateFromClosest) And (IsNumeric(MaxV) And IsNumeric(MinV)) Then
            EstimateFromClosest = Amin(MaxV, AMAX(MinV, EstimateFromClosest))
        End If

        '    Set tbldriver = Nothing

    End Function

    Private Sub mnuEstMRandC_Click()

        'mti July 2006 -take full -  new menu for new (most recent/closest) estimation technique
        Session("SDGEstimatesMRandC") = Not Session("SDGEstimatesMRandC")
        If Session("SDGEstimatesMRandC") Then
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using closest data or cross-sectional estimation to fill holes"
        Else
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstMRandC").Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
        End If

        RefreshForm()
    End Sub

    'jrs 2011/11/23
    Private Sub mnuEstForInit_Click()
        'jrs 2008/10/29
        Session("SDGEstimateForInit") = Not Session("SDGEstimateForInit")
        If Session("SDGEstimateForInit") Then
            'jrs 2009/02/10 mnuEstMRandC.Checked = False
            'jrs 2009/02/09
            'jrs 2009/02/10 mnuEstMRandC.Caption = "Using Cross-Sectionally enhanced Longitudinal Regression to fill holes"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstForInit").Caption = "Overriding the base year value historical line with forecast value"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstForInit").IconImage = Page.ResolveUrl("~/Images/checkbox.gif") 'jam 2021/12/12 "checkbox.gif"
        Else 'jrs 2009/02/09
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstForInit").Caption = "Using estimated historical line that may not meet forecast line"
            cswmWebMenu.Groups("mnuHorizon").Items("mnuEstForInit").IconImage = Page.ResolveUrl("~/Images/blank.gif") 'jam 2021/12/12 "blank.gif"
        End If
        RefreshForm()
    End Sub

    'jrs 2011/05/02
    'Procedure to build array with codes to be saved in url
    Private Sub PackJScritpBuilder()
        If Session("SDGGroups") = False Then
            Dim sJscript As String, i As Integer
            sJscript = "    var RegCode = [];" & vbCr
            For i = 1 To NR
                sJscript = sJscript & "    RegCode[" & i.ToString & "] = """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """;" & vbCr
            Next

            ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadRegCode", sJscript, True)

            cmb_Dim1.Attributes.Add("onchange", "cntrylist_click();")
        Else
            cmb_Dim1.Attributes.Add("onchange", "lstGroups_click();")
        End If

        Dim sJscript2 As String
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "            $(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jam 2025/10/07 Increasing font size for legends
        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "itemStyle: {" & vbCr
        'sJscript2 = sJscript2 & "fontFamily: 'Arial, sans-serif'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '25px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        'sJscript2 = sJscript2 & "color: '#333333' " & vbCr
        sJscript2 = sJscript2 & "}, " & vbCr
        sJscript2 = sJscript2 & "} " & vbCr
        '''''''''''''''''''''''



        sJscript2 = sJscript2 & "});" & vbCr

        If Session("SDGCompare") = False Then
            sJscript2 = sJscript2 & PackJScriptBuilderContainer("")
        Else
            Dim i As Integer
            For i = 1 To 4
                If Session("SDGMultiGraphLabels")(i) > "" Then
                    sJscript2 = sJscript2 & PackJScriptBuilderContainer(i.ToString)
                Else
                    Exit For
                End If
            Next
        End If

        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2
    End Sub

    Private Function PackJScriptBuilderContainer(ByVal sContainterIdx As String) As String
        Dim sJscript2 As String = ""
        sJscript2 = sJscript2 & "$('#container" & sContainterIdx & "').highcharts({" & vbCr

        'jam 2025/03/11 New highcharts version libraries
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

        'Chart Title
        Dim sTitle As String
        If sContainterIdx = "" Then
            sTitle = Session("SDGcmb_Dim1")
        Else
            sTitle = Session("SDGMultiGraphLabels")(CInt(sContainterIdx))
        End If
        sJscript2 = sJscript2 & "text:       '" & sTitle & "'," & vbCr

        'jam 2025/10/07  Increasing font size for legends
        sJscript2 = sJscript2 & "style: {fontWeight: 'bold', fontSize: '30px'}," & vbCr
        ''''''''''''''''''''''''''''



        sJscript2 = sJscript2 & "x: -20 //center" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        Dim sMax As String, sMin As String
        If Session("SDGDisplayLastYearSDG") > 0 Then
            sMax = Session("SDGDisplayLastYearSDG")
        Else
            sMax = CStr(Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) + 15)
        End If

        If Session("SDGDisplayEarliestYearWithData") > 0 Then
            sMin = Session("SDGDisplayEarliestYearWithData")
        ElseIf Session("SDGEarliestYearWithData") > 0 Then
            sMin = Session("SDGEarliestYearWithData")
        Else
            sMin = Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))
        End If

        sJscript2 = sJscript2 & "xAxis: {" & vbCr

        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr

        sJscript2 = sJscript2 & "tickInterval: 10" & vbCr
        sJscript2 = sJscript2 & ", min: " & sMin & vbCr
        sJscript2 = sJscript2 & ", max: " & sMax & vbCr

        sJscript2 = sJscript2 & ",title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "yAxis: {" & vbCr

        'jam 2025/10/13 Increasing font for y axis
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "color: 'black'," & vbCr
        sJscript2 = sJscript2 & "fontSize: '20px'" & vbCr
        sJscript2 = sJscript2 & "}}," & vbCr


        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        'jam 2025/10/13 Increasing font for x axis
        sJscript2 = sJscript2 & "fontSize: '24px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "}," & vbCr



        sJscript2 = sJscript2 & "gridLineWidth: 0," & vbCr
        sJscript2 = sJscript2 & "lineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "minPadding: 0," & vbCr
        sJscript2 = sJscript2 & "plotLines: [{" & vbCr
        sJscript2 = sJscript2 & "value: 0," & vbCr
        sJscript2 = sJscript2 & "width: 1," & vbCr
        sJscript2 = sJscript2 & "color:      '#808080'" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "legend: {" & vbCr
        sJscript2 = sJscript2 & "layout:     'horizontal'," & vbCr
        sJscript2 = sJscript2 & "align:      'center'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'top'," & vbCr
        sJscript2 = sJscript2 & "x: -20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "y: 20, " & vbCr 'avoid overlap
        sJscript2 = sJscript2 & "borderWidth: 0" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim i As Integer, sZeros As String = "", count As Integer, gphIdx As Integer = 0
        If sContainterIdx > "" Then
            gphIdx = CInt(sContainterIdx)
        End If
        If Session("SDGMultiGraphsShown") > 0 And Session("SDGCompare") = True Then
            count = Session("SDGCOUNTESDG") / Session("SDGMultiGraphsShown")
        Else
            count = Session("SDGCOUNTESDG")
        End If

        'jrs 2017/07/24
        If Session("ShowRegVals") Then
            'Regression
            count = count + 1
        End If

        For i = 1 To 3 + count
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToJSSeries(i, gphIdx)
        Next
        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "}, function(chart){" & vbCr



        'jam 2024/08/06 Bug fix related to GET parameters
        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('" & Page.ResolveUrl("~/Images/pcl.png") & "', 10, 10, 60, 60)" & vbCr


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

        If sContainterIdx = "" Then
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.9;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.55;" & vbCr
        Else
            sJscript2 = sJscript2 & "myWidth = myWidth * 0.35;" & vbCr
            sJscript2 = sJscript2 & "myHeight = myHeight * 0.30;" & vbCr
        End If

        sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "});" & vbCr

        PackJScriptBuilderContainer = sJscript2

    End Function

    Private Function PlotColToJSSeries(ByVal i As Integer, ByVal gphIdx As Integer) As String
        Dim OnlyVal As Object
        Dim k As Integer
        If gphIdx > 0 Then
            k = gphIdx
        Else
            k = 1
        End If

        Dim sJscript As String = ""
        Dim sColor As String = ""
        If i < 5 Then
            Select Case i
                Case 1
                    sColor = " color:""Blue"""
                Case 2 'blue
                    sColor = " color:""Blue"""
                Case 3
                    ''jrs 2013/09/13
                    'If Session("SDGIIndex") = 13 Then
                    '    PlotColToJSSeries = ""
                    '    Exit Function
                    'End If
                    sColor = " color:""Lime"""
                Case 4
                    'blue violet
                    sColor = " color:""RGB(138,43,226)"""
            End Select
            sColor = sColor & ","
        End If

        'jrs 2017/05/31 If i = 2 Then
        'sJscript = sJscript & "{name:""" & getSeriesName(i) & """," & sColor & ", type: ""column"", data: [" & vbCr
        'Else
        'sJscript = sJscript & "{name:""" & getSeriesName(i) & """," & sColor & ", data: [" & vbCr
        'End If
        If i = 1 Then
            sJscript = sJscript & "{name:""" & getSeriesName(i) & """," & sColor & " dashStyle: ""Dash"", showInLegend: false, data: [" & vbCr
        Else
            sJscript = sJscript & "{name:""" & getSeriesName(i) & """," & sColor & " data: [" & vbCr
        End If

        Dim j As Integer, TStart As Integer, TEnd As Integer, colIdx As Integer
        If Session("SDGLasYear_is_LowCommonYear") = "YES" Then
            TEnd = Session("SDGIFsLowCommonYear") 'lastyear%
        Else
            TEnd = Session("LastYear")
        End If

        If TEnd > Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) + 15 Then
            TEnd = Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) + 15
        End If

        If Session("SDGDisplayLastYearSDG") = 0 Then
            Session("SDGDisplayLastYearSDG") = Session("DisplayLastYear")
            If Session("SDGDisplayLastYearSDG") > Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) + 15 Then
                Session("SDGDisplayLastYearSDG") = Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) + 15
            End If
        End If

        If Session("SDGDisplayLastYearSDG") > 0 Then
            If Session("SDGDisplayLastYearSDG") <> TEnd Then 'disply horizon chosen
                TEnd = Session("SDGDisplayLastYearSDG")
            End If
        End If

        If TEnd < Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) Then
            TEnd = Session("SDGIndicator_TargetYear")(Session("SDGIIndex"))
        End If

        Session("DisYear") = Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) - Session("SDGIndicator_BaseYear")(Session("SDGIIndex")) + (TEnd - Session("SDGIndicator_TargetYear")(Session("SDGIIndex"))) + 1     '+ 5


        Session("SDG_Base2Target") = Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) - Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))
        'SDG_Base2Current = Year(Now()) - Session("SDGIndicator_BaseYear")(Session("SDGIIndex")) - 1

        Session("SDGOffset_SDGbase") = Session("SDGIndicator_BaseYear")(Session("SDGIIndex")) - FirstYearHistory + 1

        'Forecast2Target = Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) - FirstYearForecast

        Session("SDGBase2IFsBase") = FirstYearForecast - Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))

        If Not Session("SDGFlagGroup") Then
            sEarliestYearWithData = Session("SDGEarliestYearWithData")
            Session("SDGEarliestYearWithData") = 0

            Find_Country_Base_Latest(k)
            'jrs 2017/07/24 added And (Indicator_IfsVar(iIndex) > "" Or IsDBNull(Indicator_TargetValue(IIndex)))
            'jrs 2008/10/28
            If Session("SDGEstimateForInit") And (i = 1 Or i = 3) And (Indicator_IfsVar(IIndex) > "" Or IsDBNull(Indicator_TargetValue(IIndex))) Then
                Session("SDGCurval")(k) = Session("SDGPLOTSDG")(1, 1 + (Session("SDGFilesSelected") * (k - 1)))
            End If
        Else
            '            For j = 1 COUNTE 'used for other purpose now
            Session("SDGTrendCalc")(1) = "Regres." 'type of calculation for base and current (latest) values
            '            Next j
            'mti SDG june 28, 2005 data
            Session("SDGEarliestYearWithData") = Session("SDGsgrpEarliestYearWithData")
            If Session("SDGEarliestYearWithData") >= Session("SDGIndicator_BaseYear")(Session("SDGIIndex")) Then Session("SDGEarliestYearWithData") = 0
            'EarliestYearWithData = 0
            'jrs 2017/07/26
            If (Indicator_IfsVar(IIndex) > "" Or IsDBNull(Indicator_TargetValue(IIndex))) Then
                Session("SDGCurval")(k) = Session("SDGPLOTSDG")(1, 1 + (Session("SDGFilesSelected") * (k - 1)))
            End If
        End If

        If Session("SDGFlagGroup") And Session("SDGEarliestYearWithData") < Indicator_BaseYear(IIndex) - 5 Then
            Session("SDGEarliestYearWithData") = Indicator_BaseYear(IIndex) - 5
        End If

        Dim BsVal As Object, AnnualRate As Object

        If i > 3 Then
            'jrs 2017/07/24
            If Session("ShowRegVals") And i = Session("SDGCOUNTESDG") + 4 Then
                If Session("SDGEarliestYearWithData") > 0 Then
                    TStart = Session("SDGEarliestYearWithData")
                Else
                    TStart = Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))
                End If
                colIdx = 1
            Else
                TStart = FirstYearForecast 'Session("SDGOffset_SDGbase") + Session("SDGBase2IFsBase")
                If gphIdx = 1 Then
                    colIdx = i - 3
                Else
                    colIdx = i - 3 + (Session("SDGFilesSelected") * (k - 1))
                End If
            End If
            'TEnd = Session("SDGOffset_SDGbase") + Session("SDGBase2IFsBase") + Session("FinYearSDG")(i)
        Else
            If Session("SDGEarliestYearWithData") > 0 And i = 2 Then
                TStart = Session("SDGEarliestYearWithData")
            Else
                TStart = Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))
            End If
            If gphIdx = 1 Then
                colIdx = i
            Else
                colIdx = k
            End If
            If i = 2 Then
                'JRS 2017/06/02 TEnd = TStart + Session("SDGOffset_SDGbase") - 1
                TEnd = Session("LastHistYear")(k)
            Else
                TEnd = TStart
            End If


            If i = 3 Then
                'jrs 2017/07/20 added isNothing, apparently the round function converts nothing to 0
                If Session("SDGCurval")(k) Is Nothing Then
                    BsVal = Nothing
                Else
                    BsVal = Round(Session("SDGCurval")(k), 2)
                End If
                'jrs 2017/06/14 not being used since BsVal is taken from base year forecast
                'If Not IsNumeric(BsVal) Then
                '    If IsNumeric(OnlyVal) Then
                '        'mti March 30, 2005
                '        '                        BsVal = OnlyVal
                '        'pego1.PeData.Y(2, OnlyPoint) = OnlyVal
                '        '                        OnlyValMult = OnlyYear2SDGTargetYear / session("SDGBase2LatestYear")

                '        Select Case Session("SDGIndicator_TargetValue")(Session("SDGIIndex"))
                '            Case 0.5
                '                AnnualRate = (0.5 ^ 0.04 - 1)
                '                Session("SDGGoalVal")(k) = OnlyVal * (1 - AnnualRate) ^ OnlyYear2SDGTargetYear 'BsVal * 2
                '            Case -0.5
                '                AnnualRate = (1 - 0.5 ^ 0.04)
                '                Session("SDGGoalVal")(k) = OnlyVal * (1 - AnnualRate) ^ OnlyYear2SDGTargetYear 'BsVal / 2
                '            Case -0.33
                '                AnnualRate = (1 - 0.667 ^ 0.04)
                '                Session("SDGGoalVal")(k) = OnlyVal * (1 - AnnualRate) ^ OnlyYear2SDGTargetYear 'BsVal * 2 / 3
                '            Case -0.66
                '                AnnualRate = (1 - 0.333 ^ 0.04)
                '                Session("SDGGoalVal")(k) = OnlyVal * (1 - AnnualRate) ^ OnlyYear2SDGTargetYear 'BsVal * 1 / 3
                '            Case -0.75
                '                AnnualRate = (1 - 0.25 ^ 0.04)
                '                Session("SDGGoalVal")(k) = OnlyVal * (1 - AnnualRate) ^ OnlyYear2SDGTargetYear 'BsVal * 1 / 4
                '            Case 100
                '                Session("SDGGoalVal")(k) = 100
                '            Case 1
                '                Session("SDGGoalVal")(k) = 1
                '                'mti January 05, 2005 (2)
                '            Case -1
                '                Session("SDGGoalVal")(k) = 0 '1
                '            Case Else  'jrs 2017/02/14
                '                'jrs 2017/02/15
                '                If Indicator_TargetValue(IIndex) = 3 And OnlyVal < 3 Then
                '                    Session("SDGGoalVal")(k) = OnlyVal
                '                Else
                '                    If Indicator_TargetValue(IIndex) = -0.7 And BsVal >= 0 Then
                '                        Session("SDGGoalVal")(k) = System.DBNull.Value
                '                    Else
                '                        Session("SDGGoalVal")(k) = Indicator_TargetValue(IIndex)
                '                    End If
                '                End If

                '                '                Case -100  'gap
                '                '                    Pego1.PEDATA.Y(2, SESSION("SDG_Base2Target") + (SESSION("SDGOffset_SDGbase") - Session("SDGOffset_Earliest"))) = Session("SDGBaseVal")(j) + (Indicator_Max(Session("SDGIIndex")) - Session("SDGBaseVal")(j)) / 2

                '        End Select
                '    Else
                '        Session("SDGGoalVal")(k) = System.DBNull.Value
                '    End If
                'End If

                'Target Year
                If IsNumeric(BsVal) And IsNumeric(Session("SDGIndicator_TargetValue")(Session("SDGIIndex"))) Then
                    'jrs 2017/06/14
                    If Session("SDGIndicator_TargetValueIsRelative")(Session("SDGIIndex")) Then
                        Session("SDGGoalVal")(k) = BsVal * Session("SDGIndicator_TargetValue")(Session("SDGIIndex"))
                    Else 'Absolute
                        'jrs 2017/08/28 added other side: Or (Valence(VarIdx) = True And Val(Baseval(VarIdx)) > Val(Target(VarIdx)))
                        'YrIdx = MDGBase2LatestYear + (Offset_MDGbase - Offset_Earliest)
                        'If Indicator_TargetValue(iIndex) = 3 And Pego1.YData(2, YrIdx) < 3 Then
                        If Session("SDGIndicator_Valence")(Session("SDGIIndex")) = False And BsVal < Session("SDGIndicator_TargetValue")(Session("SDGIIndex")) Or
                            Session("SDGIndicator_Valence")(Session("SDGIIndex")) = True And BsVal > Session("SDGIndicator_TargetValue")(Session("SDGIIndex")) Then
                            Session("SDGGoalVal")(k) = BsVal
                        Else
                            'If Indicator_TargetValue(iIndex) = -0.7 And BsVal >= 0 Then
                            If Session("SDGIndicator_TargetValue")(Session("SDGIIndex")) < 0 And BsVal >= 0 Then
                                Session("SDGGoalVal")(k) = System.DBNull.Value
                            Else
                                Session("SDGGoalVal")(k) = Session("SDGIndicator_TargetValue")(Session("SDGIIndex"))
                            End If
                        End If
                    End If

                    'Select Case Session("SDGIndicator_TargetValue")(Session("SDGIIndex"))
                    '    Case 0.5
                    '        Session("SDGGoalVal")(k) = BsVal * 2
                    '    Case -0.5
                    '        Session("SDGGoalVal")(k) = BsVal / 2
                    '    Case -0.33
                    '        Session("SDGGoalVal")(k) = BsVal * 2 / 3
                    '    Case -0.66
                    '        Session("SDGGoalVal")(k) = BsVal * 1 / 3
                    '        'Pego1.PEDATA.Y(2, SESSION("SDG_Base2Target") + (SESSION("SDGOffset_SDGbase") - Session("SDGOffset_Earliest"))) = BsVal * 1 / 3
                    '    Case -0.75
                    '        Session("SDGGoalVal")(k) = BsVal * 1 / 4
                    '    Case 0.7  'ODA/aid %
                    '        Session("SDGGoalVal")(k) = 0.7
                    '    Case 15 'jrs 2017/02/06
                    '        Session("SDGGoalVal")(k) = 15
                    '    Case 2 'jrs 2017/02/07
                    '        Session("SDGGoalVal")(k) = 2 * BsVal
                    '    Case 100
                    '        Session("SDGGoalVal")(k) = 100
                    '    Case 1
                    '        Session("SDGGoalVal")(k) = 1
                    '        'mti January 05, 2005 (2)
                    '    Case -1
                    '        Session("SDGGoalVal")(k) = 0 '1
                    '    Case Else
                    '        If Indicator_TargetValue(IIndex) = 3 And BsVal < 3 Then
                    '            Session("SDGGoalVal")(k) = BsVal
                    '        Else
                    '            If Indicator_TargetValue(IIndex) = -0.7 And BsVal >= 0 Then
                    '                Session("SDGGoalVal")(k) = System.DBNull.Value
                    '            Else
                    '                Session("SDGGoalVal")(k) = Indicator_TargetValue(IIndex)
                    '            End If
                    '        End If

                    '        '                Case -100  'gap
                    '        '                    Pego1.PEDATA.Y(2, SESSION("SDG_Base2Target") + (SESSION("SDGOffset_SDGbase") - Session("SDGOffset_Earliest"))) = Session("SDGBaseVal")(j) + (Indicator_Max(Session("SDGIIndex")) - Session("SDGBaseVal")(j)) / 2

                    'End Select
                Else
                    Session("SDGGoalVal")(k) = System.DBNull.Value
                End If

            End If
        End If

        'jrs 2011/11/23
        If i = 1 Then
            OnlyVal = System.DBNull.Value
        End If
        Dim jj As Integer
        Dim bFirst As Boolean = True
        For j = TStart To TEnd
            If i > 3 Then
                'jrs 2017/07/24
                If Session("ShowRegVals") And i = Session("SDGCOUNTESDG") + 4 Then
                    If IsNumeric(Session("SDGPLOTSDGHR")(j - FirstYearHistory + 1, 1)) Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & ","
                        End If
                        sJscript = sJscript & "{x: " & j.ToString & ", y: " & CStr(Math.Round(Val(Session("SDGPLOTSDGHR")(j - FirstYearHistory + 1, colIdx)), 1)) & "}" & vbCr
                    End If
                Else
                    If IsNumeric(Session("SDGPLOTSDG")(j - TStart + 1, colIdx)) Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & ","
                        End If
                        sJscript = sJscript & "{x: " & j.ToString & ", y: " & CStr(Math.Round(Val(Session("SDGPLOTSDG")(j - TStart + 1, colIdx)), 2)) & "}" & vbCr
                    End If
                End If
            ElseIf i = 1 Or i = 3 Then
                'If IsNumeric(Session("SDGBaseVal")(k)) Then
                '    If bFirst Then
                '        bFirst = False
                '    Else
                '        sJscript = sJscript & ","
                '    End If
                '    sJscript = sJscript & "{x: " & j.ToString & ", y: " & CStr(Math.Round(Val(Session("SDGBaseVal")(k)), 2)) & "}" & vbCr
                'Else
                If i = 3 And IsNumeric(BsVal) Then
                    'jj = FirstYearHistory + Session("SDGOffset_SDGbase") + Session("SDG_Base2Target") - OnlyYear2SDGTargetYear - 1
                    jj = FirstYearForecast
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & ","
                    End If
                    sJscript = sJscript & "{x: " & jj.ToString & ", y: " & CStr(BsVal) & "}" & vbCr
                End If
                If i = 1 Then
                    If IsNumeric(Session("LastHistVal")(colIdx)) Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & ","
                        End If
                        sJscript = sJscript & "{x: " & Session("LastHistYear")(colIdx) & ", y: " & CStr(Math.Round(Val(Session("LastHistVal")(colIdx)), 2)) & "}" & vbCr
                    End If
                    If IsNumeric(Session("SDGCurval")(colIdx)) Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & ","
                        End If
                        sJscript = sJscript & "{x: " & FirstYearForecast.ToString & ", y: " & CStr(Math.Round(Val(Session("SDGCurval")(colIdx)), 2)) & "}" & vbCr
                    End If
                Else 'i=2
                    If IsNumeric(Session("SDGGoalVal")(k)) Then
                        If bFirst Then
                            bFirst = False
                        Else
                            sJscript = sJscript & ","
                        End If
                        sJscript = sJscript & "{x: " & Session("SDGIndicator_TargetYear")(Session("SDGIIndex")) & ", y: " & CStr(Math.Round(Val(Session("SDGGoalVal")(k)), 2)) & "}" & vbCr
                    End If
                End If
            Else
                If IsNumeric(Session("SDGPLOTSDGH")(j - FirstYearHistory + 1, k)) Then
                    If bFirst Then
                        bFirst = False
                    Else
                        sJscript = sJscript & ","
                    End If
                    sJscript = sJscript & "{x: " & j.ToString & ", y: " & CStr(Math.Round(Val(Session("SDGPLOTSDGH")(j - FirstYearHistory + 1, k)), 2)) & "}" & vbCr
                    OnlyVal = Math.Round(Val(Session("SDGPLOTSDGH")(j - FirstYearHistory + 1, k)), 2)
                    OnlyYear2SDGTargetYear = Session("SDG_Base2Target") - (j - FirstYearHistory + 1 - Session("SDGOffset_SDGbase")) 'j
                    'Else
                    '    If bFirst Then
                    '        bFirst = False
                    '    Else
                    '        sJscript = sJscript & ","
                    '    End If
                    'sJscript = sJscript & "{x: " & j.ToString & ", y: 0}" & vbCr
                End If
            End If
        Next


        sJscript = sJscript & "]}" & vbCr

        PlotColToJSSeries = sJscript
    End Function

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String
        Select Case ii
            Case 1
                Legend = "" '"SDG Current Path"  'GR1N$(j)
            Case 2
                Legend = "History"
            Case 3
                Legend = "SDG Goal"
            Case Else
                'jrs 2017/07/24
                If Session("ShowRegVals") And ii = Session("SDGCOUNTESDG") + 4 Then
                    Legend = Session("sRegFormula")
                Else
                    Legend = "IFs" + Session("SDGcRunfile")(ii - 3)
                End If
        End Select

        getSeriesName = Legend
    End Function

    Private Sub Find_Country_Base_Latest(ByVal mgno As Integer)

        'if more than 2 points exist, regression only for base and current (latest)
        'if no points exist, cross-sectional function for base and current (latest)
        'if 1 point exists, cross-sectional function then privilesdge the point (not done yet) for base and current (latest)

        Dim reg As New RegressionObject, rr As Integer
        Dim DrvFld As String, DrvVal As Object, YearVal As Object, tblDriver As DataTable, tblDriver1 As DataTable

        Dim DataYears As Integer
        Dim hCOUNTE%

        hCOUNTE = 1


        Dim x#, y#

        Dim j%, pt%, M%
        Dim OnlyVal, OnlyYear%, ShiftVal

        Session("SDGLatestYear") = FirstYearForecast  'comment this line after discussion, if necessary 'mti March 23, 2005

        DataYears = Session("SDGLatestYear") - Session("SDGFirstYearSDG") + 1 'year(Date) - FirstYear '+ 1
        'FirstYearForecast - FirstYear + 1 'year(Date) - FirstYear '+ 1

        Session("SDGBase2LatestYear") = DataYears - Session("SDGOffset_SDGbase")

        j = mgno

        'jrs 2011/11/24
        Session("SDGCurval")(j) = Nothing
        Session("SDGBaseVal")(j) = Nothing

        With reg
            .Init()
            .Degree = 1 'Degree
            pt = 0
            'mti SDG june 28, 2005 data
            For rr% = 1 To Year(Now) - Session("SDGFirstYearSDG") + 1 'DataYears 'LastYear% - FirstYear% + 1
                'For rr% = SESSION("SDGOffset_SDGbase") - 5 To year(Date) - FirstYearSDG + 1 'DataYears 'LastYear% - FirstYear% + 1

                'Format(PlotLong(PYear%, j), "0.000")
                x# = CDbl(rr%)
                If IsNumeric(Session("SDGPLOTSDGH")(rr%, j)) Then

                    If Session("SDGEarliestYearWithData") = 0 Then
                        Session("SDGEarliestYearWithData") = Session("SDGFirstYearSDG") + rr% - 1
                        OnlyYear = Session("SDGEarliestYearWithData")

                        'mti March 30, 2005
                        OnlyYear2SDGTargetYear = Session("SDG_Base2Target") - (rr% - Session("SDGOffset_SDGbase"))

                        If Session("SDGEarliestYearWithData") >= Session("SDGIndicator_BaseYear")(Session("SDGIIndex")) Then
                            Session("SDGEarliestYearWithData") = 0
                        End If
                    End If

                    If rr% >= Session("SDGOffset_SDGbase") - 5 Then ''mti SDG june 28, 2005 data - added condition
                        'take 1985 or later points only
                        y# = CDbl(Session("SDGPLOTSDGH")(rr%, j))  '1
                        'jrs 2017/07/24
                        Select Case Session("EstBaseYrReg")
                            Case "LIN"
                                pt = pt + 1
                            Case "LOG"
                                x# = CDbl(Log(rr%))
                                pt = pt + 1
                            Case "LOGISTIC"
                                If Session("SDGPLOTSDGH")(rr%, j) <> 0 Then
                                    If ((Session("SaturationValue") / PLOTSDGH(rr%, j)) - 1) > 0 Then
                                        y# = Log(CDbl((Session("SaturationValue") / PLOTSDGH(rr%, j)) - 1))
                                        pt = pt + 1
                                    End If
                                End If
                            Case "QUAD"
                                x# = CDbl(rr% ^ 2)
                                pt = pt + 1
                            Case "CUBIC"
                                x# = CDbl(rr% ^ 3)
                                pt = pt + 1
                            Case "EXP"
                                x# = CDbl(Exp(rr%))
                                pt = pt + 1
                        End Select

                        .XYAdd(x#, y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                        OnlyVal = Session("SDGPLOTSDGH")(rr%, j)

                        'jrs 2017/07/24 pt = pt + 1
                    End If
                End If
            Next rr%

            If pt < 2 Then

                'mti January 05, 2005 (2)
                If Session("SDGIndicator_Function")(Session("SDGIIndex")) <> "" Then

                    'might be able to mit if Find_Country_Base_Latest taken within history_fildata - mti SDG

                    'Set tblGDPPC = db.OpenRecordset("SeriesGDP2000PCPPP")
                    'Indicator_DriverTbl

                    SetDataTablefromAccess(Session("IFsHistSeries_MDB"), "SELECT * FROM " & Session("SDGIndicator_DriverTbl")(Session("SDGIIndex")), "Country", tblDriver, DataPW)

                    DrvFld$ = Trim(Str(Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))))
                    DrvVal = SeekValue(tblDriver, Session("MultCountries")(j), DrvFld)   'GDP of that particular year

                    If Not IsNumeric(DrvVal) Then
                        DrvFld$ = Trim(Str((Val(DrvFld$) + 1))) 'go one more year, 1991
                        DrvVal = SeekValue(tblDriver, Session("MultCountries")(j), DrvFld)
                    End If

                    If Not IsNumeric(DrvVal) Then
                        DrvFld$ = Trim(Str((Val(DrvFld$) + 1))) 'go two more year, 1992
                        DrvVal = SeekValue(tblDriver, Session("MultCountries")(j), DrvFld)
                    End If


                    If Not IsNumeric(DrvVal) Then
                        DrvFld$ = Trim(Str(Session("SDGIndicator_BaseYear")(Session("SDGIIndex"))))
                        DrvFld$ = Trim(Str((Val(DrvFld$) - 1))) 'go back one year, 1989
                        DrvVal = SeekValue(tblDriver, Session("MultCountries")(j), DrvFld)
                    End If


                    If IsNumeric(DrvVal) Then
                        If Session("SDGIndicator_Conversion")(Session("SDGIIndex")) = "Div" And Session("SDGIndicator_ConversionNumber")(Session("SDGIIndex")) <> 0 Then
                            DrvVal = DrvVal / Session("SDGIndicator_ConversionNumber")(Session("SDGIIndex"))
                        End If
                        'use SDG database for tblfunction name
                        'Call XYTABL("GDP/Capita (PPP) Versus Income Below One Dollar per Day (2000)", GDPval, grCntryBaseVal(k))
                        Call XYTABL(Session("SDGIndicator_Function")(Session("SDGIIndex")), DrvVal, Session("SDGBaseVal")(j))

                        If IsNumeric(Session("SDGBaseVal")(j)) Then
                            Session("SDGBaseVal")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGBaseVal")(j)))
                        End If


                    End If



                    DrvFld$ = "MostRecent"
                    For M = 0 To tblDriver.Columns.Count - 1
                        YearVal = Val(tblDriver.Columns(M).Caption)
                        If YearVal = Session("SDGLatestYear") Then
                            DrvFld$ = Trim(Str(Session("SDGLatestYear")))
                            Exit For
                        End If
                    Next M
                    DrvVal = SeekValue(tblDriver, Session("MultCountries")(j), DrvFld)  'GDP of that particular year

                    'DrvVal = DrvVal / 1000
                    If Session("SDGIndicator_Conversion")(Session("SDGIIndex")) = "Div" And Session("SDGIndicator_ConversionNumber")(Session("SDGIIndex")) <> 0 And IsNumeric(DrvVal) Then 'jrs 2005/11/24 added And IsNumeric(DrvVal) 
                        DrvVal = DrvVal / Session("SDGIndicator_ConversionNumber")(Session("SDGIIndex"))
                    End If


                    If IsNumeric(DrvVal) Then

                        'Call XYTABL("GDP/Capita (PPP) Versus Income Below One Dollar per Day (2000)", GDPval, grCntryLYVal(k))
                        Call XYTABL(Session("SDGIndicator_Function")(Session("SDGIIndex")), DrvVal, Session("SDGCurval")(j))
                        If IsNumeric(Session("SDGCurval")(j)) Then
                            Session("SDGCurval")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGCurval")(j)))
                        End If

                    End If

                    tblDriver = Nothing
                    'JRS 2005/08/25 db = Nothing


                    '---------if one point exists priviledge data----------
                    If pt = 1 And IsNumeric(Session("SDGBaseVal")(j)) And
                        IsNumeric(Session("SDGCurval")(j)) Then

                        .Init()

                        Session("SDGBaseVal")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGBaseVal")(j)))
                        Session("SDGCurval")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGCurval")(j)))


                        'X# = CDbl(Indicator_BaseYear(IIndex))
                        x# = CDbl(Session("SDGIndicator_BaseYear")(Session("SDGIIndex")) - Session("SDGFirstYearSDG") + 1)

                        y# = CDbl(Session("SDGBaseVal")(j))
                        .XYAdd(x#, y#)

                        'X# = CDbl(LatestYear) 'actually last GDPFld$ should be taken, all data available so..
                        x# = CDbl(Session("SDGLatestYear") - Session("SDGFirstYearSDG") + 1) 'actually last GDPFld$ should be taken, all data available so..
                        'X# = CDbl(FirstYearForecast)

                        y# = CDbl(Session("SDGCurval")(j))
                        .XYAdd(x#, y#)


                        'X# = CDbl(OnlyYear)
                        x# = CDbl(OnlyYear - Session("SDGFirstYearSDG") + 1)
                        y# = .RegVal(CDbl(x#))
                        ShiftVal = OnlyVal - y# 'ShiftVal '.RegVal(CDbl(X#))


                        'mti SDG june 28, 2005 data
                        'If OnlyYear + 5 >= Indicator_BaseYear(IIndex) Then
                        'data for 1985 or later priviledged only

                        'X# = CDbl(Indicator_BaseYear(IIndex))
                        Session("SDGBaseVal")(j) = Session("SDGBaseVal")(j) + ShiftVal '.RegVal(CDbl(X#))

                        'X# = CDbl(LatestYear)
                        Session("SDGCurval")(j) = Session("SDGCurval")(j) + ShiftVal '.RegVal(CDbl(X#))
                        'mti March 30 2005
                        '            Else
                        '                x# = CDbl(SESSION("SDGOffset_SDGbase"))
                        '                BaseVal(j) = AMAX(0.001, .RegVal(CDbl(x#)))          '+ TC1)
                        '                x# = CDbl(DataYears)
                        '                Curval(j) = AMAX(0.001, .RegVal(CDbl(x#)))   '+ TC1
                        '
                        '            End If
                        '            BaseVal(j) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), BaseVal(j)))
                        '            Curval(j) = Amin(Indicator_Max(IIndex), AMAX(Indicator_Min(IIndex), Curval(j)))

                    End If 'pt=1
                    '---------end if one point exists priviledge data----------

                    'mti January 05, 2005 (2)
                End If 'Indicator_Function(IIndex) <> ""


                'in case empty after all ateempts
                If Not IsNumeric(Session("SDGBaseVal")(j)) Or
                    Not IsNumeric(Session("SDGCurval")(j)) Then
                    '            SDGMsg(j) = "Not enough data for " + CRName(j)
                    '            BaseVal(j) = Null
                    '            Curval(j) = Null '""
                    '            TrendCalc(j) = ""
                    '            EarliestYearWithData = sEarliestYearWithData

                    'jrs 2017/07/24
                    If pt = 1 Then
                        Session("SDGCurval")(j) = y
                    End If

                Else
                    ''bind values between 0 and 100% - for % values (change for others)
                    Session("SDGBaseVal")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGBaseVal")(j)))
                    Session("SDGCurval")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGCurval")(j)))
                    'Session("SDGTrendCalc")(j) = "Function"
                End If


            Else  'If pt < 2 Then Else
                Session("SDGMsg")(j) = ""
                '        If Not IsNull(PLOTSDGH(SESSION("SDGOffset_SDGbase"), j)) And Not (PLOTSDGH(SESSION("SDGOffset_SDGbase"), j) = Empty) Then
                '            BaseVal(j) = PLOTSDGH(SESSION("SDGOffset_SDGbase"), j)
                '        Else
                x# = CDbl(Session("SDGOffset_SDGbase"))

                'jrs 2017/07/24
                Select Case Session("EstBaseYrReg")
                    Case "LIN"
                        Session("SDGBaseVal")(j) = .RegVal(CDbl(x#))  '+ TC1
                    Case "LOG"
                        Session("SDGBaseVal")(j) = .RegVal(CDbl(Log(x#)))  '+ TC1
                    Case "LOGISTIC"
                        Session("SDGBaseVal")(j) = Session("SaturationValue") / (Exp(.RegVal(CDbl(x#))) + 1) '+ TC1
                    Case "QUAD"
                        Session("SDGBaseVal")(j) = .RegVal(CDbl(x# ^ 2))  '+ TC1
                    Case "CUBIC"
                        Session("SDGBaseVal")(j) = .RegVal(CDbl(x# ^ 3))  '+ TC1
                    Case "EXP"
                        Session("SDGBaseVal")(j) = .RegVal(CDbl(Exp(x#)))  '+ TC1
                End Select

                '        End If
                'jrs 2013/09/13 Session("SDGBaseVal")(j) = AMAX(0.001, Session("SDGBaseVal")(j))

                'jrs 2017/09/07
                If IsNumeric(Session("SDGPLOTSDGH")(DataYears, j)) Then
                    Session("SDGCurval")(j) = Val(Session("SDGPLOTSDGH")(DataYears, j))
                Else
                    '        If IsNull(Curval(j)) Then
                    x# = CDbl(DataYears)
                    'X# = CDbl(FirstYearForecast - FirstYear + 1)
                    'FirstYearForecast - FirstYear + 1
                    'jrs 2017/07/24
                    Select Case Session("EstBaseYrReg")
                        Case "LIN"
                            Session("SDGCurval")(j) = .RegVal(CDbl(x#))   '+ TC1
                        Case "LOG"
                            Session("SDGCurval")(j) = .RegVal(CDbl(Log(x#)))   '+ TC1
                        Case "LOGISTIC"
                            Session("SDGCurval")(j) = Session("SaturationValue") / (Exp(.RegVal(CDbl(x#))) + 1)
                        Case "QUAD"
                            Session("SDGCurval")(j) = .RegVal(CDbl(x# ^ 2))
                        Case "CUBIC"
                            Session("SDGCurval")(j) = .RegVal(CDbl(x# ^ 3))
                        Case "EXP"
                            Session("SDGCurval")(j) = .RegVal(CDbl(Exp(x#)))
                    End Select
                End If

                '        End If

                Session("SDGBaseVal")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGBaseVal")(j)))
                Session("SDGCurval")(j) = Amin(Session("SDGIndicator_Max")(Session("SDGIIndex")), AMAX(Session("SDGIndicator_Min")(Session("SDGIIndex")), Session("SDGCurval")(j)))
                'Session("SDGTrendCalc")(j) = "Regres."
                '        Curval(j) = AMAX(0.001, Curval(j))

                'low bound current to target
                '        Curval(j) = AMAX(BaseVal(j) / 2, Curval(j))

            End If
            'jrs 2017/07/24
            If Session("ShowRegVals") And pt > 1 Then
                'jrs 2017/07/24
                Session("sRegFormula") = "y = " & CStr(Round(.Coeff(0), 2))
                Select Case Session("EstBaseYrReg")
                    Case "LIN"
                        If .Coeff(1) > 0 Then
                            Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                        Else
                            Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                        End If
                        For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                            x# = CDbl(rr%)
                            PLOTSDGHR(rr%, 1) = .RegVal(CDbl(x#))  '+ TC1
                        Next
                    Case "LOG"
                        If .Coeff(1) > 0 Then
                            Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " LN(Year-1959)"
                        Else
                            Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " LN(Year-1959)"
                        End If
                        For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                            x# = CDbl(rr%)
                            PLOTSDGHR(rr%, 1) = .RegVal(CDbl(Log(x#)))
                        Next
                    Case "LOGISTIC"
                        Session("sRegFormula") = CStr(Round(Abs(.Coeff(0)), 2))
                        If .Coeff(1) > 0 Then
                            Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                        Else
                            Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)"
                        End If
                        Session("sRegFormula") = "y = " & CStr(Session("SaturationValue")) & "/(exp(" & Session("sRegFormula") & ") + 1)"
                        For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                            x# = CDbl(rr%)
                            PLOTSDGHR(rr%, 1) = Session("SaturationValue") / (Exp(.RegVal(CDbl(x#))) + 1)
                        Next
                    Case "QUAD"
                        If .Coeff(1) > 0 Then
                            Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^2"
                        Else
                            Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^2"
                        End If
                        For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                            x# = CDbl(rr%)
                            PLOTSDGHR(rr%, 1) = .RegVal(CDbl(x# ^ 2))  '+ TC1
                        Next
                    Case "CUBIC"
                        If .Coeff(1) > 0 Then
                            Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^3"
                        Else
                            Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " (Year-1959)^3"
                        End If
                        For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                            x# = CDbl(rr%)
                            PLOTSDGHR(rr%, 1) = .RegVal(CDbl(x# ^ 3))  '+ TC1
                        Next
                    Case "EXP"
                        If .Coeff(1) > 0 Then
                            Session("sRegFormula") = Session("sRegFormula") & " + " & CStr(Round(.Coeff(1), 2)) & " EXP(Year-1959)"
                        Else
                            Session("sRegFormula") = Session("sRegFormula") & " " & CStr(Round(.Coeff(1), 2)) & " EXP(Year-1959)"
                        End If
                        For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                            x# = CDbl(rr%)
                            PLOTSDGHR(rr%, 1) = .RegVal(CDbl(Exp(x#)))  '+ TC1
                            If PLOTSDGHR(1, 1) > 0 And PLOTSDGHR(rr%, 1) < 0 Then
                                PLOTSDGHR(rr%, 1) = DBNull.Value
                            End If

                        Next
                End Select
            Else
                For rr% = 1 To Session("LastYearForecast") - Session("SDGFirstYearSDG") + 1
                    PLOTSDGHR(rr, 1) = DBNull.Value
                Next
                Session("sRegFormula") = ""
            End If

            Session("SDGPLOTSDGHR") = PLOTSDGHR

        End With


    End Sub

    Private Sub LoadTargets()
        'This procedure loads a regression for each member of the variable list vs GDPPCP (linear and log are tested for best fit)
        Dim dbSDG As String
        dbSDG = DataFileDir & "IFsMDG.mdb"
        Dim rsTargets As DataTable
        SetDataTablefromAccess(dbSDG, "SELECT * FROM Indicators WHERE GoalSetId=2 ORDER BY ID", "", rsTargets)

        'Load data for each other variable in given year
        ReDim Session("GoalId")(rsTargets.Rows.Count)
        ReDim Session("TargetId")(rsTargets.Rows.Count)
        ReDim Session("sName")(rsTargets.Rows.Count)
        ReDim Session("Description")(rsTargets.Rows.Count)
        ReDim Session("IFsVarName")(rsTargets.Rows.Count)
        ReDim Session("Dim2")(rsTargets.Rows.Count)
        ReDim Session("Transform")(rsTargets.Rows.Count)
        ReDim Session("IFsVarName2")(rsTargets.Rows.Count)
        ReDim Session("Var2Dim2")(rsTargets.Rows.Count)
        ReDim Session("Target")(rsTargets.Rows.Count)
        ReDim Session("VarMaxs")(rsTargets.Rows.Count)
        ReDim Session("VarMins")(rsTargets.Rows.Count)
        ReDim Session("TargetYear")(rsTargets.Rows.Count)
        'jrs 2017/06/15
        ReDim Session("TargetIsRelative")(rsTargets.Rows.Count)
        ReDim Session("Valence")(rsTargets.Rows.Count)

        Dim j As Integer
        j = 0

        'list of all variables
        Do While j < rsTargets.Rows.Count

            j = j + 1
            Session("GoalId")(j) = rsTargets.Rows(j - 1).Item("GoalId")
            Session("TargetId")(j) = rsTargets.Rows(j - 1).Item("TargetId")
            Session("sName")(j) = rsTargets.Rows(j - 1).Item("Name")
            Session("Description")(j) = rsTargets.Rows(j - 1).Item("Description")
            Session("IFsVarName")(j) = rsTargets.Rows(j - 1).Item("IFsVar")
            Session("Dim2")(j) = rsTargets.Rows(j - 1).Item("IFsVar Dim2")
            Session("Transform")(j) = rsTargets.Rows(j - 1).Item("Transform")
            Session("IFsVarName2")(j) = rsTargets.Rows(j - 1).Item("Var2")
            Session("Var2Dim2")(j) = rsTargets.Rows(j - 1).Item("Var2Dim2")
            Session("Target")(j) = rsTargets.Rows(j - 1).Item("TargetValue")
            Session("VarMaxs")(j) = rsTargets.Rows(j - 1).Item("Maximum")
            Session("VarMins")(j) = rsTargets.Rows(j - 1).Item("Minimum")
            Session("TargetYear")(j) = rsTargets.Rows(j - 1).Item("Target Year")
            'jrs 2017/06/15
            If IsDBNull(rsTargets.Rows(j - 1).Item("TargetValueIsRelative")) Then
                Session("TargetIsRelative")(j) = False
            Else
                Session("TargetIsRelative")(j) = rsTargets.Rows(j - 1).Item("TargetValueIsRelative")
            End If
            If IsDBNull(rsTargets.Rows(j - 1).Item("Valence")) Then
                Session("Valence")(j) = False
            Else
                Session("Valence")(j) = rsTargets.Rows(j - 1).Item("Valence")
            End If
        Loop
    End Sub
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2021/12/12
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function SDGPrepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        SDGPrepareForFriendlyURL = tmp
    End Function


End Class
