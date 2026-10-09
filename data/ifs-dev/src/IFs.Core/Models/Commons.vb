Imports System
Imports System.Collections.Generic
'jrs 2022/09/15
Imports System.Data
Imports System.Data.Common
Imports System.Diagnostics
Imports System.IO
Imports System.Math
Imports System.Net
Imports System.Threading.Tasks
Imports System.Threading.Tasks.Dataflow
Imports IFs.Data
Imports Microsoft.Data.Sqlite
Imports Microsoft.EntityFrameworkCore
Imports NLog

Namespace Models

    <IFsModel("Commons", Description:="Provides utility functions, global parameters, and other facilities.")>
    Public Module Commons

        Dim _logger As Logger = LogManager.GetCurrentClassLogger()

        '   Energy Types
        '   ------------

        Public Const Oil = 1
        Public Const Gas = 2
        Public Const Coal = 3
        Public Const Hydro = 4
        Public Const Nucl = 5

        'mti 2024/07/23 start
        Public Const Solar = 6
        Public Const Wind = 7
        Public Const Geothermal = 8

        'Public Const Renew = 6
        'Public Const Synth = 7
        Public Const Renew = 9
        Public Const Synth = 10

        'mti 2024/07/23 ed



        '   Education Genders
        '   ------------------

        Public Const Male = 1
        Public Const Female = 2
        Public Const Total = 3

        'mti 03/18/10 Electricity Access -start

        '   Electricity Access Rates
        '   -----------------------------

        Public Const Urban = 1
        Public Const Rural = 2
        'mti 2022/06/21 start
        Public Const National = 3
        'mti 2022/06/21 end

        'Global Const Total = 3 'total already declared above

        'mti 03/18/10 Electricity Access -end

        'mti 2022/06/21 start
        '   Water and Sanitation Service Categories
        '   ---------------------------------------

        Public Const Surface = 1
        Public Const OpenDefecation = 1
        Public Const Unimproved = 2
        Public Const Limited = 3
        Public Const Basic = 4
        Public Const SafelyManaged = 5

        'mti 2022/06/21 End

        '   Infrastructure Types
        '   --------------------

        'mti April 2011 -start

        'mti February 28, 2012 -start

        Public Const RoadPaved = 1
        Public Const RoadUnpaved = 2
        Public Const ElectricityGen = 3
        Public Const ElectricityAccRural = 4
        Public Const ElectricityAccUrban = 5
        Public Const Irrigation = 6
        Public Const SafeWaterHH = 7
        Public Const SafeWaterImproved = 8
        Public Const SanitationHH = 9
        Public Const SanitationImproved = 10
        Public Const WasteWater = 11
        Public Const Telephone = 12
        Public Const Mobile = 13
        Public Const Broadband = 14
        Public Const BroadbandMobile = 15
        Public Const InfraTot = 16
        'mti February 28, 2012 -end

        Public Const Road_PerCapita = 1
        Public Const Road_PerLand = 2
        Public Const Road_Access = 3
        Public Const Road_Paved = 4

        Public Const Elec_Gen = 1
        Public Const Elec_Acc = 2
        Public Const Elec_Tran = 3
        Public Const Elec_Share = 4

        Public Const Watsan_Wat = 1
        Public Const Watsan_San = 2
        Public Const Watsan_WasteTreat = 3

        '   Infrastructure index (and trad index) Components
        '   ------------------------------

        Public Const TotalRoad = 1
        Public Const ElecGen = 2
        Public Const ElecAcc = 3
        Public Const WaterCon = 4
        Public Const SanitCon = 5
        'mti May 2011 -start
        Public Const FixedPhone = 6
        Public Const FixedBroadband = 7
        Public Const MobilePhone = 8
        Public Const MobileBroadband = 9

        Public Const Fixed_Phone = 1
        Public Const Fixed_Broadband = 2
        Public Const Mobile_Phone = 3
        Public Const Mobile_Broadband = 4


        '   Economic Sectors
        '   ----------------
        Public Const Agri = 1
        Public Const Ener = 2
        Public Const Mate = 3
        Public Const Manu = 4
        Public Const Serv = 5
        Public Const ICT = 6

        '   Land and Food Types
        '   -------------------
        Public Const Crop = 1
        Public Const Graz = 2
        Public Const Fors = 3
        '   BBH 1/25/04 Begin V5_0
        Public Const Othe = 4
        Public Const Urba = 5
        '   BBH 1/25/04 Begin V5_0

        Public Const Meat = 2


        '   Labor Types
        '   -----------
        Public Const Unskilled = 1
        Public Const Skilled = 2

        '   Government Spending Categories
        '   ------------------------------

        Public Const Mil = 1
        Public Const Health = 2
        Public Const Educ = 3
        Public Const RandD = 4
        Public Const Infra = 5
        Public Const InfraOther = 6
        Public Const Other = 7

        '   Education Levels
        '   ----------------

        'mti Education April 07, 2008
        'mti 2014 / 10 / 6 - START

        '    Global Const Primary = 1
        '    Global Const Secondary = 2
        '    Global Const SecLowr = 3
        '    Global Const SecUppr = 4
        '    Global Const Tertiary = 5


        Public Const Primary = 1
        Public Const SecLowr = 2
        Public Const SecUppr = 3
        Public Const Tertiary = 4
        'mti 2014 / 10 / 6 - START

        'mti Education April 07, 2008 end

        'mti 2021/12/09 start
        'mti 2020/01/14 Start
        '    Global Const TotPovAGCount = 9 ' number of agegroups in poverty data
        '    Global Const AG_0_14 = 1
        '    Global Const AG_15_24 = 2
        '    Global Const AG_25_34 = 3
        '    Global Const AG_35_39 = 4
        '    Global Const AG_40_49 = 5
        '    Global Const AG_50_54 = 6
        '    Global Const AG_55_59 = 7
        '    Global Const AG_60_Over = 8
        '    Global Const AG_AllAges = 9
        'mti 2020/01/14 End

        Public Const TotPovAGCount = 11 ' number of agegroups in poverty data
        Public Const AG_0_4 = 1
        Public Const AG_5_9 = 2
        Public Const AG_10_14 = 3
        Public Const AG_15_24 = 4
        Public Const AG_25_34 = 5
        Public Const AG_35_39 = 6
        Public Const AG_40_49 = 7
        Public Const AG_50_54 = 8
        Public Const AG_55_59 = 9
        Public Const AG_60_Over = 10
        Public Const AG_AllAges = 11
        'mti 2021/12/09 End

        'jrs 2022/11/28 start
        Public Const Age15_19 = 1
        Public Const Age20_24 = 2
        'jrs 2022/11/28 end

        ' default coalesing policy
        Public DefaultPolicy As CoalescePolicy

        ' default query predicate for the base-year
        Public BaseYearOnly As IQueryFilter

        Public IFsVarsExTbls As Dictionary(Of String, IDictionary(Of String, Single?()))
        Public IFsVarsExTblsYrs As Dictionary(Of String, Short())

        Public bShowMsg As Boolean

        Public GRPS(,) As String
        Public MaxVal() As Short, MAXCOM() As Short

        'jrs 2022/05/02
        Public sEnergyNoProductionCountry As String

        <IFsModelInit(ExecutionStage.Rebuild)>
        Public Sub Configure(context As IRebuildContext)

            Debug.WriteLine("- global variables for rebuild.")
            'Console.WriteLine("- global variables for rebuild.")
            ' coalescing policy
            If context.HistoricMode Then
                DefaultPolicy = CoalescePolicy.Earliest
            Else
                DefaultPolicy = CoalescePolicy.MostRecent
            End If
            BaseYearOnly = context.GetDimension(0).EQ(context.FirstYear)
            BaseYear = context.BaseYear
            Debug.WriteLine("- default settings for context.")
            context.DefaultPolicy = DefaultPolicy
            context.DefaultAggregator = ContextService.SUM(DefaultPolicy)
            'jrs 2019/02/15
            NR = context.GetDimension(1).Buckets.Count
            IFsWVSCohort_MDB = context.PutFdrvRun + "IFsWVSCohort.mdb"
            PutFdrvRun$ = context.PutFdrvRun
            'jrs 2022/09/15
            DataFileDir = context.DataFileDir

            'mti 2019/04/29  Flex Hist Base
            HistDist = 100
            NSec = context.NSec
            'jrs 2022/05/17
            NCohorts = context.NCohorts
            'jrs 2019/09/24
            RegCoeffs = New Dictionary(Of String, Dictionary(Of String, Double))
            'jrs 2019/11/12
            RegPoints = New Dictionary(Of String, IPoint(Of Double)())
            For Each reg In context.RegressionNames
                RegCoeffs.Add(reg, context.GetRegressionCoefficients(reg).ToDictionary(Function(grp) grp.RegressionSeq.ToString() + "-" + grp.Name,
                           Function(grp) grp.Value))
                'jrs 2019/11/12
                'RegPoints.Add(reg, context.GetRegressionPoints(reg).ToArray())
            Next
            'jrs 2019/09/27
            SupplementalDataFile$ = context.SupplementalDataFile$
            SupplementalHistoricDataFile = context.SupplementalHistoricDataFile
            FirstYear = context.FirstYear
            LastYear = context.LastYear
            'jrs 2023/01/25
            FirstYearHistory = context.FirstYearHistory
            'jrs 2022/11/01
            ConstantDollarsYear = context.ConstantDollarsYear
            'jrs 2024/06/06
            PastConstantDollarsYears = New List(Of Integer)
            PastConstantDollarsYears.Add(2011)
            PastConstantDollarsYears.Add(2005)
            PastConstantDollarsYears.Add(2000)

            'jrs 2023/10/03
            NGender = context.NGender

            NPriGr = context.NPriGr 'mti 2023/10/15

            ReDim MaxVal(context.Dimensions.Count), MAXCOM(context.Dimensions.Count)
            MaxVal(1) = CShort(context.Dimensions(1).Buckets.Count + 1)  'Add room for "ALL"
            MAXCOM(1) = CShort(context.Dimensions(1).Buckets.Count + 1)
            'jrs 2022/11/16
            NR = MaxVal(1) - 1
            ReDim GRPS$(context.Dimensions.Count, AMAX(20, MaxVal(1)))
            Dim i As Integer, j As Integer
            For i = 1 To context.Dimensions.Count - 1
                For j = 1 To context.Dimensions(i).Buckets.Count
                    GRPS(i, j) = context.Dimensions(i).Buckets(CShort(j))
                Next
            Next

            'jrs 2020/01/03 added (StringComparer.OrdinalIgnoreCase) to avoid case sensitivity
            'jrs 2019/09/30
            'Load Groups
            IFsGroups = New Dictionary(Of String, List(Of Integer))(StringComparer.OrdinalIgnoreCase)
            Dim tmpIFsGroups As Dictionary(Of Integer, List(Of Integer)) = New Dictionary(Of Integer, List(Of Integer))
            Using db As IFsContext = New IFsContext()
                For Each item In db.GroupingFull
                    If Not tmpIFsGroups.ContainsKey(item.GroupId) Then
                        tmpIFsGroups.Add(item.GroupId, New List(Of Integer))
                    End If
                    tmpIFsGroups(item.GroupId).Add(item.RegionId)
                Next

                For Each item In db.GroupIDNewFull
                    IFsGroups.Add(item.GroupName, New List(Of Integer))
                    For Each item2 In tmpIFsGroups(item.GroupNum)
                        IFsGroups(item.GroupName).Add(item2)
                    Next
                Next

                CurrencyVal = CSng(db.Currency.Where(Function(g) g.CurrencyFlag = True).Single().CurrencyValue)
            End Using

            'Load Aggregation Rules
            IFsAggRules = New Dictionary(Of String, String)
            Using db As IFsVarContext = New IFsVarContext()
                For Each item In db.AggRules
                    IFsAggRules.Add(item.AggName, item.ForecastVar)
                Next
            End Using

        End Sub

        ' total number of regions
        Public NR As Integer
        Public IFsWVSCohort_MDB As String
        'mti 2019/04/29  Flex Hist Base
        Public HistDist As Short
        'jrs 2019/06/17
        Public NCohorts As Integer
        Public NAges As Integer
        Public NIOMatrices As Integer
        Public WVSCohorts As Integer
        Public NGender As Integer
        Public NPriGr As Integer 'mti 2020/09/01
        Public NGovExp As Integer
        Public bCancelMsgMatchMortLE As Boolean
        Public LastCohortFert As Integer
        Public NSec As Integer
        Public NSkill As Integer
        Public FirstYear As Short
        Public LastYear As Short
        Public BaseYear As Short
        Public FirstYearHistory As Short
        Public sErrorSub As String
        Public IY As Short
        Public PutFdrvRun As String
        Public NHHTypes As Integer
        Public NWBTar As Integer
        Public NInfraIndexICTComp As Integer
        Public NInfraType As Integer

        'ttt 2019/08/12
        Public NInfraIndexComp As Integer
        Public NInfraIndWatSanComp As Integer
        Public NInfraIndElecComp As Integer
        Public NInfraIndTranComp As Integer
        Public NInfraIndexTradComp As Integer
        Public NLocation As Integer
        'end ttt

        'ttt 2019/08/13
        Public NET As Integer
        Public NEP As Integer
        Public NER As Integer
        'ttt 2019/08/13

        'jrs 2022/11/01
        'Constant Dollars
        '-----------------
        Public ConstantDollarsYear As Integer
        'jrs 2024/06/06
        Public PastConstantDollarsYears As List(Of Integer)

        '   Global for Model
        '   ----------------
        Public salphas(,) As Single 'Set in initialization of model

        Public GiniMatrix(700), PopMatrix(700) As Single

        Public GDPPCI(), GDPPCIP() As Single

        Public MFPCorSI(,) As Single

        'jrs 2007/01/11
        Public RegiontoSum() As Boolean
        Public bAllProvinces As Boolean

        Public conn3 As DbConnection

        'jrs 2019/09/16
        Public RegCoeffs As Dictionary(Of String, Dictionary(Of String, Double))

        'ttt 2019/10/08 
        Public RegArrays As Dictionary(Of String, IRegression())
        Public RegPoints As Dictionary(Of String, IPoint(Of Double)())

        'jrs 2019/09/17
        'jrs 2019/11/18        Public ifsPopWorkDB As IBasecase
        'jrs 2019/11/19 
        Friend ifsPopWorkDB As BasecaseContext
        'jrs 2019/11/20
        Friend ifs_buffer As BufferBlock(Of Action)

        'jrs 2019/09/30
        Public IFsGroups As Dictionary(Of String, List(Of Integer))
        Public gCountryGroup() As String
        Public gCountryGroupIdx() As Integer
        Public gGroupTot As Integer
        Public CurrencyVal As Single
        Public IFsAggRules As Dictionary(Of String, String)

        'jrs 2020/02/18
        Public dfBaseGDPADJ As IDataframe
        'jrs 2021/06/01
        Public dfBaseWPOP As IDataframe
        Public dfBaseigdpr As IDataframe
        Public dfBaseXWORKREMIT As IDataframe
        Public dfBasePOPWORK As IDataframe
        Public dfBaseHLYLDWORK As IDataframe


        'jrs 2020/04/03
        Public sSAMFile As String = ""
        Public sSmokingFile As String = ""
        Public sIFsPopFile As String = ""

        Public MaxMIGRATEIN As Single = 0

        'jrs 2024/01/24
        Public MultiThreading As Boolean = True

        <IFsModelInit(ExecutionStage.Forecast)>
        Public Sub Configure(context As IForecastContext)
            Debug.WriteLine("- global variables for forecast.")
            NR = context.GetDimension(1).Buckets.Count
            NCohorts = context.NCohorts
            LastCohortFert = context.LastCohortFert
            NSec = context.NSec
            bShowMsg = context.ShowMsg
            FirstYear = context.FirstYear
            LastYear = context.LastYear
            NAges = context.NAges
            PutFdrvRun$ = context.PutFdrvRun
            'jrs 2020/04/06 BaseYear = context.BaseYear
            BaseYear = context.FirstYearForecast
            'jrs 2022/08/29
            FirstYearHistory = context.FirstYearHistory
            'jrs 2022/11/01
            ConstantDollarsYear = context.ConstantDollarsYear
            'jrs 2024/06/06
            PastConstantDollarsYears = New List(Of Integer)
            'jrs 2025/09/16
            PastConstantDollarsYears.Add(2021)
            PastConstantDollarsYears.Add(2017)
            PastConstantDollarsYears.Add(2011)
            PastConstantDollarsYears.Add(2005)
            PastConstantDollarsYears.Add(2000)

            NGender = context.NGender
            NPriGr = context.NPriGr 'mti 2020/09/01
            NIOMatrices = context.NIOMatrices
            NGovExp = context.NGovExp
            NSkill = context.NSkill
            NHHTypes = context.NHHTypes
            NWBTar = context.NWBTar
            WVSCohorts = context.WVSCohorts
            NInfraIndexICTComp = context.NInfraIndexICTComp
            NInfraType = context.NInfraType
            'ttt 2019/08/12
            NInfraIndexComp = context.NInfraIndexComp
            NInfraIndWatSanComp = context.NInfraIndWatSanComp
            NInfraIndElecComp = context.NInfraIndElecComp
            NInfraIndTranComp = context.NInfraIndTranComp
            NInfraIndexTradComp = context.NInfraIndexTradComp
            NLocation = context.NLocation
            ScenarioFileDir = context.ScenarioFileDir
            'jrs 2022/09/15
            DataFileDir = context.DataFileDir
            'jrs 2022/11/01
            ConstantDollarsYear = context.ConstantDollarsYear
            'end ttt
            'ttt 2019/08/13
            NET = context.NET
            NEP = context.NEP
            NER = context.NER
            'ttt
            'jrs 2024/01/24
            MultiThreading = context.MultiThreading

            ReDim salphas(NR + 1, NSec%)

            ReDim MaxVal(context.Dimensions.Count), MAXCOM(context.Dimensions.Count)
            MaxVal(1) = CShort(context.Dimensions(1).Buckets.Count + 1)  'Add room for "ALL"
            MAXCOM(1) = CShort(context.Dimensions(1).Buckets.Count + 1)
            ReDim GRPS$(context.Dimensions.Count, AMAX(20, MaxVal(1)))
            Dim i As Integer, j As Integer
            For i = 1 To context.Dimensions.Count - 1
                For j = 1 To context.Dimensions(i).Buckets.Count
                    GRPS(i, j) = context.Dimensions(i).Buckets(CShort(j))
                Next
            Next

            ReDim MFPCorSI(NR, 10)

            ReDim RegiontoSum(NR)

            LoadProvinces(context)

            'jrs 2019/09/16
            RegCoeffs = New Dictionary(Of String, Dictionary(Of String, Double))
            'ttt 2019/10/08
            RegArrays = New Dictionary(Of String, IRegression())
            RegPoints = New Dictionary(Of String, IPoint(Of Double)())

            For Each reg In context.RegressionNames

                RegCoeffs.Add(reg, context.GetRegressionCoefficients(reg).ToDictionary(Function(grp) grp.RegressionSeq.ToString() + "-" + grp.Name,
                           Function(grp) grp.Value))
                    'ttt 2019/10/08 
                    RegArrays.Add(reg, context.GetRegression(reg).ToArray())
                    RegPoints.Add(reg, context.GetRegressionPoints(reg).ToArray())
            Next

            'jrs 2020/01/03 added (StringComparer.OrdinalIgnoreCase) to avoid casesensitivity
            'jrs 2019/09/30
            IFsGroups = New Dictionary(Of String, List(Of Integer))(StringComparer.OrdinalIgnoreCase)
            Dim tmpIFsGroups As Dictionary(Of Integer, List(Of Integer)) = New Dictionary(Of Integer, List(Of Integer))
            Using db As IFsContext = New IFsContext()
                For Each item In db.GroupingFull
                    If Not tmpIFsGroups.ContainsKey(item.GroupId) Then
                        tmpIFsGroups.Add(item.GroupId, New List(Of Integer))
                    End If
                    tmpIFsGroups(item.GroupId).Add(item.RegionId)
                Next

                For Each item In db.GroupIDNewFull
                    IFsGroups.Add(item.GroupName, New List(Of Integer))
                    For Each item2 In tmpIFsGroups(item.GroupNum)
                        IFsGroups(item.GroupName).Add(item2)
                    Next
                Next
                Debug.WriteLine("After Load GroupIDNewFull")
                Try
                    CurrencyVal = CSng(db.Currency.Where(Function(g) g.CurrencyFlag = True).Single().CurrencyValue)
                    'For Each val As Currency In db.Currency
                    '    If val.CurrencyFlag = True Then
                    '        If Not val.CurrencyValue.HasValue Then
                    '            CurrencyVal = 0
                    '        Else
                    '            CurrencyVal = CSng(val.CurrencyValue)
                    '        End If
                    '        Exit For
                    '    End If
                    'Next
                Catch ex As Exception
                    Console.WriteLine(ex.ToString)
                    Console.WriteLine("---")
                    Console.WriteLine(ex.Message)
                End Try
                Debug.WriteLine("After CurrencyVal")
            End Using
            'Load Aggregation Rules
            IFsAggRules = New Dictionary(Of String, String)
            Using db As IFsVarContext = New IFsVarContext()
                For Each item In db.AggRules
                    IFsAggRules.Add(item.AggName, item.ForecastVar)
                Next
            End Using

            'jrs 2020/04/22
            MaxMIGRATEIN = CSng(context.GetVariable("MIGRATEIN").Max)
            ' TODO
            Debug.WriteLine("- default settings for context.")
            ' TODO
        End Sub

        Public Class DefaultSumAggregator
            Inherits SumAggregator

            ' wire aggregator-specific policy to the global variable
            Public Overrides ReadOnly Property Policy As CoalescePolicy
                Get
                    Return DefaultPolicy
                End Get
            End Property

        End Class

        Public Class PopWeightedAverageAggregator
            Inherits WeightedAverageAggregator

            ' wire aggregator-specific policy to the global variable
            Public Overrides ReadOnly Property Policy As CoalescePolicy
                Get
                    Return DefaultPolicy
                End Get
            End Property

            Public Overrides ReadOnly Property Weights As IDictionary(Of String, Single?)
                Get
                    Return Me.CPOP
                End Get
            End Property

            ' CPOP will be assigned to this property by the dependency framework
            <ImportsAsset("CPOP")>
            Public Property CPOP As IDictionary(Of String, Single?)

        End Class

        Public Class GdpWeightedAverageAggregator
            Inherits WeightedAverageAggregator

            Public Overrides ReadOnly Property Policy As CoalescePolicy
                Get
                    Return DefaultPolicy
                End Get
            End Property

            Public Overrides ReadOnly Property Weights As IDictionary(Of String, Single?)
                Get
                    Return Me.CGDP
                End Get
            End Property

            ' CGDP will be assigned to this property by the dependency framework
            <ImportsAsset("CGDP")>
            Public Property CGDP As IDictionary(Of String, Single?)

        End Class

        Public Function ApplyUpperBound(value As Single?, maxValue As Single) As Single?
            If value > maxValue Then
                Return maxValue
            Else
                Return value
            End If
        End Function

        Public Function ApplyLowerBound(value As Single?, minValue As Single) As Single?
            If value < minValue Then
                Return minValue
            Else
                Return value
            End If
        End Function

        Public Function ApplyBounds(value As Single?, minValue As Single, maxValue As Single) As Single?
            If value < minValue Then
                Return minValue
            ElseIf value > maxValue Then
                Return maxValue
            Else
                Return value
            End If
        End Function

        Public Function ApplyLimits(MyValue As Single?, Min As Single, Max As Single) As Single?
            If MyValue < Min Then
                Return Min
            ElseIf MyValue > Max Then
                Return Max
            Else
                Return MyValue
            End If
        End Function


        Public Function Amin(Value As Single, limit As Single) As Single
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            If Value > limit Then
                Return limit
            Else
                Return Value
            End If

        End Function

        Public Function Amin(Value As Double?, limit As Double?) As Double?
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            If Not Value.HasValue Then
                Return limit
            End If

            If Value > limit Then
                Return limit
            Else
                Return Value
            End If

        End Function

        Public Function Amin(Value As Double, limit As Double) As Double
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            If Value > limit Then
                Return limit
            Else
                Return Value
            End If

        End Function

        Public Function Amin(Value As Single?, limit As Single?) As Single?
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            If Not Value.HasValue Then
                Return limit
            End If

            If Value > limit Then
                Return limit
            Else
                Return Value
            End If
        End Function

        Public Function Amin(Value As Integer, limit As Integer) As Integer
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            If Value > limit Then
                Return limit
            Else
                Return Value
            End If

        End Function

        Public Function AMAX(MyValue As Integer, Limit As Integer) As Integer
            If MyValue < Limit Then
                Return Limit
            Else
                Return MyValue
            End If
        End Function

        Public Function AMAX(MyValue As Single?, Limit As Single?) As Single?
            If Not MyValue.HasValue Then
                Return Limit
            End If
            If MyValue < Limit Then
                Return Limit
            Else
                Return MyValue
            End If
        End Function

        Public Function AMAX(MyValue As Double?, Limit As Double?) As Double?
            If Not MyValue.HasValue Or Not Limit.HasValue Then Return Nothing
            If MyValue < Limit Then
                Return Limit
            Else
                Return MyValue
            End If
        End Function

        Public Function AMAX(MyValue As Double, Limit As Double) As Double
            If MyValue < Limit Then
                Return Limit
            Else
                Return MyValue
            End If
        End Function

        Public Function AMAX(MyValue As Double, Limit As Single) As Single
            If MyValue < Limit Then
                Return CSng(Limit)
            Else
                Return CSng(MyValue)
            End If
        End Function

        Public Function AMAX(MyValue As Single, Limit As Single) As Single
            If MyValue < Limit Then
                Return CSng(Limit)
            Else
                Return CSng(MyValue)
            End If
        End Function

        'jrs 2010/11/26
        'Originally created by Mohammod Irfan, moved here to be publicly accessible
        'calculates annual (compounding) growth rate (0 to 1) of a variable over an interval using an IFsHistSeries table
        Public Function getAnnualGrowthRate(ByRef tblPhys As IDictionary(Of String, Single?()), ByRef years As Short(), ByVal sCtry As String,
                    ByVal iYr As Integer, Optional ByVal MaxGrowthRate As Single = 0.1, Optional ByVal MinGrowthRate As Single = 0.0005,
                    Optional ByVal grintrvalMin As Integer = 2, Optional ByVal grintrvalMax As Integer = 5,
                    Optional ByVal MaxBackUp As Integer = 10, Optional ByVal MaxForward As Integer = 3, Optional ByVal IsAbsolute As Boolean = False) As Single?
            'mti May 05, 2011
            'mti Dec 01, 2010 - start

            'Parameters:
            '1. tblPhy is the table to be used to find the Growth rate
            '1.1 years is the list of years retrieved from tblPhy
            '1.2 sCtry is the name of the country for which to compute the annual growth
            '2. iYr is the Pivot year, from here we will read only a given # of years in the past and the future
            '3. MaxGrowthRate is the Maximum value this function can return
            '4. MinGrowthRate is the Minimum value this function can return, added by jrs
            '5. grintrvalMin is the minimum number of years in the interval to compute growth
            '(using 2 avoids using the previous year from the first year with data)
            '5.2 grintrvalMax is the maximum number of years in the interval to compute growth
            'avoids using too distant points (if iYr is null then we use 2*grintrvalMax)
            '6. MaxBackUp is the maximum number of years to go back from iYr
            '(currenctly iYr - MaxBackUp is not being used but exits the function)
            '7. MaxForward is the maximum number of years to go forward from iYr
            '(Years > iYr + MaxForward are ignored)
            '8. IsAbsolute, when true, makes the function calaculate the average annual absolute growth (as opposed to the growthrate) over the interval



            Dim i As Integer

            Dim Val_Current As Single? = Nothing
            Dim val_prev As Single? = Nothing
            getAnnualGrowthRate = Nothing

            'jrs 2010/11/26
            'Validations
            '-----------
            'MaxGrowthRate has to be higher than MinGrowthRate
            If MinGrowthRate > MaxGrowthRate Then
                Debug.Print("Max Growth rate has to be higher than Min Growth rate") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If
            If grintrvalMin > grintrvalMax Then
                Debug.Print("Max Growth rate interval has to be higher than Min Growth rate interval") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If

            'Limits need to be positive
            If grintrvalMin < 1 Then
                Debug.Print("Growth Interval needs to be higher than 0") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If
            If MaxBackUp < 2 Then
                Debug.Print("Max number of years to look before " & iYr & " needs to be higher than 1") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If
            If MaxForward < 0 Then
                Debug.Print("Max number of years to look after " & iYr & " can't be negative") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If

            'Trapping General Errors
            '-----------------------
            Try

                'mti 2022/10/04 Start
                Dim GrComputed = False
                Do While Not GrComputed
                    'mti 2022/10/04 End

                    With tblPhys
                        Dim DataVal As Single?, Yr_No As Integer, Yr_Current As Integer, yr_prev As Integer
                        Dim RatChange As Single?, DiffTime As Integer
                        i = 0
                        'years are supposed to be sorted in descending order
                        If years(0) < years(years.Count - 1) Then
                            For i = years.Count - 1 To 0 Step -1
                                DataVal = tblPhys(sCtry)(i)
                                Yr_No = CInt(years(i))

                                'Stop reading further back
                                If Yr_No <= iYr - MaxBackUp Then Exit For

                                If DataVal.HasValue Then
                                    'mti June 08, 2011 start
                                    'allow 0 when calculating absolute change
                                    If DataVal <> 0 Or IsAbsolute Then
                                        'mti June 08, 2011 -end
                                        If Yr_No <= (iYr + MaxForward) Then 'mti - replace Int(.Fields(i).Name) with Yr_No
                                            If Not Val_Current.HasValue Then
                                                Val_Current = DataVal
                                                Yr_Current = Yr_No
                                            Else
                                                'Only when data increases, not when there are drops
                                                'this avoids returning negatives

                                                'mti June 08, 2011 start - allow negative growth rates
                                                'If DataVal <= Val_Current Then
                                                If DataVal <= Val_Current Or MinGrowthRate < 0 Then
                                                    'mti June 08, 2011 end - allow negative growth rates

                                                    val_prev = DataVal
                                                    yr_prev = Yr_No

                                                    If Yr_Current - yr_prev >= grintrvalMin Then
                                                        'jrs 2010/11/26
                                                        If Yr_Current - yr_prev > grintrvalMax And Yr_Current >= iYr Then Exit Function
                                                        If Yr_Current - yr_prev > 2 * grintrvalMax Then Exit Function

                                                        'mti May 05, 2011 - start
                                                        If IsAbsolute Then

                                                            'average absolute growth over the interval (as opposed to growth rate)
                                                            RatChange = Val_Current - val_prev
                                                            DiffTime = Yr_Current - yr_prev

                                                            getAnnualGrowthRate = RatChange / DiffTime

                                                            'mti Sep 12, 2011 - start
                                                            '0 or - should be OK for absolute change (at least for pre-processor
                                                            'If getAnnualGrowthRate < 0 Then
                                                            '    getAnnualGrowthRate = Null
                                                            'End If
                                                            'mti Sep 12, 2011 - end
                                                            Exit For
                                                        Else
                                                            'mti May 05, 2011  -end

                                                            RatChange = Val_Current / val_prev
                                                            DiffTime = Yr_Current - yr_prev

                                                            'compounded growth rate
                                                            'jrs 2010/11/26 getAnnualGrowthRate = AMAX(0.0005, RatChange ^ (1 / DiffTime) - 1)

                                                            'mti June 14, 2012 - start
                                                            getAnnualGrowthRate = CSng(RatChange ^ (1 / DiffTime) - 1)

                                                            If getAnnualGrowthRate > MinGrowthRate Or Yr_No <= iYr - MaxBackUp + 1 Then 'proceed

                                                                'getAnnualGrowthRate = AMAX(MinGrowthRate, RatChange ^ (1 / DiffTime) - 1)
                                                                getAnnualGrowthRate = AMAX(MinGrowthRate, CSng(getAnnualGrowthRate))


                                                                If getAnnualGrowthRate < MaxGrowthRate * 1.5 Or Yr_No <= iYr - MaxBackUp + 1 Then
                                                                    'let go if not very far from Maxgrowthrate or all years read
                                                                    getAnnualGrowthRate = Amin(getAnnualGrowthRate, MaxGrowthRate)
                                                                    Exit For
                                                                Else
                                                                    getAnnualGrowthRate = Amin(getAnnualGrowthRate, MaxGrowthRate)
                                                                    'continue to read data
                                                                End If
                                                            Else
                                                                getAnnualGrowthRate = AMAX(MinGrowthRate, CSng(getAnnualGrowthRate))
                                                                'continue to read data
                                                            End If
                                                            'mti June 14, 2012 - end
                                                            'mti May 05, 2011 - start
                                                        End If
                                                        'mti May 05, 2011  -end


                                                    End If 'Yr_Current - Yr_Prev >= grintrval Then
                                                End If 'Dataval < Val_Current
                                            End If ' IsNull(Val_Current) Then
                                        End If 'Int(.Fields(i).Name) <= (iYr + MaxForward) Then
                                    End If 'dataval<>0
                                End If  'Not IsNull(Dataval) Then
                                'End If 'baseyear and back
                            Next
                        Else
                            For Each This_Yr In years
                                DataVal = tblPhys(sCtry)(i)
                                Yr_No = CInt(This_Yr)

                                'Stop reading further back
                                If Yr_No <= iYr - MaxBackUp Then Exit For

                                If DataVal.HasValue Then
                                    'mti June 08, 2011 start
                                    'allow 0 when calculating absolute change
                                    If DataVal <> 0 Or IsAbsolute Then
                                        'mti June 08, 2011 -end
                                        If Yr_No <= (iYr + MaxForward) Then 'mti - replace Int(.Fields(i).Name) with Yr_No
                                            If Not Val_Current.HasValue Then
                                                Val_Current = DataVal
                                                Yr_Current = Yr_No
                                            Else
                                                'Only when data increases, not when there are drops
                                                'this avoids returning negatives

                                                'mti June 08, 2011 start - allow negative growth rates
                                                'If DataVal <= Val_Current Then
                                                If DataVal <= Val_Current Or MinGrowthRate < 0 Then
                                                    'mti June 08, 2011 end - allow negative growth rates

                                                    val_prev = DataVal
                                                    yr_prev = Yr_No

                                                    If Yr_Current - yr_prev >= grintrvalMin Then
                                                        'jrs 2010/11/26
                                                        If Yr_Current - yr_prev > grintrvalMax And Yr_Current >= iYr Then Exit Function
                                                        If Yr_Current - yr_prev > 2 * grintrvalMax Then Exit Function

                                                        'mti May 05, 2011 - start
                                                        If IsAbsolute Then

                                                            'average absolute growth over the interval (as opposed to growth rate)
                                                            RatChange = Val_Current - val_prev
                                                            DiffTime = Yr_Current - yr_prev

                                                            getAnnualGrowthRate = RatChange / DiffTime

                                                            'mti Sep 12, 2011 - start
                                                            '0 or - should be OK for absolute change (at least for pre-processor
                                                            'If getAnnualGrowthRate < 0 Then
                                                            '    getAnnualGrowthRate = Null
                                                            'End If
                                                            'mti Sep 12, 2011 - end
                                                            Exit For
                                                        Else
                                                            'mti May 05, 2011  -end

                                                            'jrs 2022/12/02
                                                            If val_prev < 0 And Val_Current > 0 Then
                                                                RatChange = (Val_Current + Abs(CSng(val_prev))) / Abs(CSng(val_prev))
                                                            Else
                                                                RatChange = Val_Current / val_prev
                                                            End If
                                                            DiffTime = Yr_Current - yr_prev

                                                            'compounded growth rate
                                                            'jrs 2010/11/26 getAnnualGrowthRate = AMAX(0.0005, RatChange ^ (1 / DiffTime) - 1)

                                                            'mti June 14, 2012 - start
                                                            getAnnualGrowthRate = CSng(RatChange ^ (1 / DiffTime) - 1)

                                                            If getAnnualGrowthRate > MinGrowthRate Or Yr_No <= iYr - MaxBackUp + 1 Then 'proceed

                                                                'getAnnualGrowthRate = AMAX(MinGrowthRate, RatChange ^ (1 / DiffTime) - 1)
                                                                getAnnualGrowthRate = AMAX(MinGrowthRate, CSng(getAnnualGrowthRate))


                                                                If getAnnualGrowthRate < MaxGrowthRate * 1.5 Or Yr_No <= iYr - MaxBackUp + 1 Then
                                                                    'let go if not very far from Maxgrowthrate or all years read
                                                                    getAnnualGrowthRate = Amin(getAnnualGrowthRate, MaxGrowthRate)
                                                                    Exit For
                                                                Else
                                                                    getAnnualGrowthRate = Amin(getAnnualGrowthRate, MaxGrowthRate)
                                                                    'continue to read data
                                                                End If
                                                            Else
                                                                getAnnualGrowthRate = AMAX(MinGrowthRate, CSng(getAnnualGrowthRate))
                                                                'continue to read data
                                                            End If
                                                            'mti June 14, 2012 - end
                                                            'mti May 05, 2011 - start
                                                        End If
                                                        'mti May 05, 2011  -end


                                                    End If 'Yr_Current - Yr_Prev >= grintrval Then
                                                End If 'Dataval < Val_Current
                                            End If ' IsNull(Val_Current) Then
                                        End If 'Int(.Fields(i).Name) <= (iYr + MaxForward) Then
                                    End If 'dataval<>0
                                End If  'Not IsNull(Dataval) Then
                                'End If 'baseyear and back
                                i = i + 1S
                            Next

                        End If

                    End With

                    'mti 2022/10/04 Start
                    GrComputed = True
                    If getAnnualGrowthRate = 0 Then
                        Dim Yrs_Cnt% = tblPhys(sCtry).Length '- 4     'year, FIPS, earliest, most recent
                        If grintrvalMin < 10 And Yrs_Cnt% >= 10 Then
                            grintrvalMin = 10
                            grintrvalMax = 10
                            MaxBackUp = 15
                            getAnnualGrowthRate = -9999
                            'jrs 2023/05/16
                            'jrs 2023/09/18 Val_Current = -9999
                            Val_Current = Nothing
                            'Val_Current = Nothing
                            GrComputed = False  'redo loop
                        End If
                    End If

                Loop
                'mti 2022/10/04 Start

            Catch ex As Exception
                'MsgBox err.Description, vbCritical, "get Annual Growth Rate"
            End Try

        End Function

        'jrs 2010/11/26
        'Originally created by Mohammod Irfan, moved here to be publicly accessible
        'calculates annual (compounding) growth rate (0 to 1) of a variable over an interval using an IFsHistSeries table
        Public Function getAnnualGrowthRateFwd(ByRef tblPhys As IDictionary(Of String, Single?()), ByRef years As Short(), ByVal sCtry As String,
                    ByVal iYr As Integer, Optional ByVal MaxGrowthRate As Single = 0.1, Optional ByVal MinGrowthRate As Single = 0.0005,
                    Optional ByVal grintrvalMin As Integer = 2, Optional ByVal grintrvalMax As Integer = 5,
                    Optional ByVal MaxBackUp As Integer = 10, Optional ByVal MaxForward As Integer = 3, Optional ByVal IsAbsolute As Boolean = False) As Single?

            'mti 2019/05/14 - New proc (for historical base growth rate, go forward
            'mti May 05, 2011
            'mti Dec 01, 2010 - start

            'Parameters:
            '1. tblPhy is the table to be used to find the Growth rate
            '1.1 years is the list of years retrieved from tblPhy
            '1.2 sCtry is the name of the country for which to compute the annual growth
            '2. iYr is the Pivot year, from here we will read only a given # of years in the past and the future
            '3. MaxGrowthRate is the Maximum value this function can return
            '4. MinGrowthRate is the Minimum value this function can return, added by jrs
            '5. grintrvalMin is the minimum number of years in the interval to compute growth
            '(using 2 avoids using the previous year from the first year with data)
            '5. grintrvalMax is the maximum number of years in the interval to compute growth
            'avoids using too distant points (if iYr is null then we use 2*grintrvalMax)
            '6. MaxBackUp is the maximum number of years to go back from iYr
            '(currenctly iYr - MaxBackUp is not being used but exits the function)
            '7. MaxForward is the maximum number of years to go forward from iYr
            '(Years > iYr + MaxForward are ignored)
            '8. IsAbsolute, when true, makes the function calaculate the average annual absolute growth (as opposed to the growthrate) over the interval



            Dim i As Integer

            Dim Val_Current As Single? = Nothing
            Dim val_next As Single? = Nothing
            getAnnualGrowthRateFwd = Nothing

            'jrs 2010/11/26
            'Validations
            '-----------
            'MaxGrowthRate has to be higher than MinGrowthRate
            If MinGrowthRate > MaxGrowthRate Then
                Debug.Print("Max Growth rate has to be higher than Min Growth rate") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If
            If grintrvalMin > grintrvalMax Then
                Debug.Print("Max Growth rate interval has to be higher than Min Growth rate interval") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If

            'Limits need to be positive
            If grintrvalMin < 1 Then
                Debug.Print("Growth Interval needs to be higher than 0") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If
            If MaxBackUp < 2 Then
                Debug.Print("Max number of years to look before " & iYr & " needs to be higher than 1") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If
            If MaxForward < 0 Then
                Debug.Print("Max number of years to look after " & iYr & " can't be negative") ', vbCritical, "get Annual Growth Rate"
                Exit Function
            End If

            'Trapping General Errors
            '-----------------------
            Try
                With tblPhys
                    Dim DataVal As Single?, Yr_No As Integer, Yr_Current As Integer, yr_next As Integer
                    Dim RatChange As Single?, DiffTime As Integer
                    i = 0
                    'years are supposed to be sorted in ascending order
                    If years(0) < years(years.Count - 1) Then
                        For i = 0 To years.Count - 1
                            DataVal = tblPhys(sCtry)(i)
                            Yr_No = CInt(years(i))

                            'Stop reading further back
                            If Yr_No <= iYr - MaxBackUp Then
                                DataVal = Nothing
                                Yr_No = 0
                            End If

                            If DataVal.HasValue Then
                                'mti June 08, 2011 start
                                'allow 0 when calculating absolute change
                                If DataVal <> 0 Or IsAbsolute Then
                                    'mti June 08, 2011 -end
                                    If Yr_No <= (iYr + MaxForward) Then 'mti - replace Int(.Fields(i).Name) with Yr_No
                                        If Not Val_Current.HasValue Then
                                            Val_Current = DataVal
                                            Yr_Current = Yr_No
                                        Else
                                            'Only when data increases, not when there are drops
                                            'this avoids returning negatives

                                            'mti June 08, 2011 start - allow negative growth rates
                                            'If DataVal <= Val_Current Then
                                            If DataVal <= Val_Current Or MinGrowthRate < 0 Then
                                                'mti June 08, 2011 end - allow negative growth rates

                                                val_next = DataVal
                                                yr_next = Yr_No

                                                If yr_next - Yr_Current >= grintrvalMin Then
                                                    'jrs 2010/11/26
                                                    If yr_next - Yr_Current > grintrvalMax And Yr_Current >= iYr Then Exit Function
                                                    If yr_next - Yr_Current > 2 * grintrvalMax Then Exit Function

                                                    'mti May 05, 2011 - start
                                                    If IsAbsolute Then

                                                        'average absolute growth over the interval (as opposed to growth rate)
                                                        RatChange = val_next - Val_Current
                                                        DiffTime = yr_next - Yr_Current

                                                        getAnnualGrowthRateFwd = RatChange / DiffTime

                                                        'mti Sep 12, 2011 - start
                                                        '0 or - should be OK for absolute change (at least for pre-processor
                                                        'If getAnnualGrowthRateFwd < 0 Then
                                                        '    getAnnualGrowthRateFwd = Null
                                                        'End If
                                                        'mti Sep 12, 2011 - end
                                                        Exit For
                                                    Else
                                                        'mti May 05, 2011  -end

                                                        RatChange = val_next / Val_Current
                                                        DiffTime = yr_next - Yr_Current

                                                        'compounded growth rate
                                                        'jrs 2010/11/26 getAnnualGrowthRateFwd = AMAX(0.0005, RatChange ^ (1 / DiffTime) - 1)

                                                        'mti June 14, 2012 - start
                                                        getAnnualGrowthRateFwd = CSng(RatChange ^ (1 / DiffTime) - 1)

                                                        If getAnnualGrowthRateFwd > MinGrowthRate Or Yr_No <= iYr - MaxBackUp + 1 Then '<= 'proceed

                                                            'getAnnualGrowthRateFwd = AMAX(MinGrowthRate, RatChange ^ (1 / DiffTime) - 1)
                                                            getAnnualGrowthRateFwd = AMAX(MinGrowthRate, getAnnualGrowthRateFwd)


                                                            If getAnnualGrowthRateFwd < MaxGrowthRate * 1.5 Or Yr_No <= iYr - MaxBackUp + 1 Then
                                                                'let go if not very far from Maxgrowthrate or all years read
                                                                getAnnualGrowthRateFwd = Amin(getAnnualGrowthRateFwd, MaxGrowthRate)
                                                                Exit For

                                                            Else
                                                                getAnnualGrowthRateFwd = Amin(getAnnualGrowthRateFwd, MaxGrowthRate)
                                                                'continue to read data
                                                            End If
                                                        Else
                                                            getAnnualGrowthRateFwd = AMAX(MinGrowthRate, getAnnualGrowthRateFwd)
                                                            'continue to read data
                                                        End If
                                                        'mti June 14, 2012 - end
                                                        'mti May 05, 2011 - start
                                                    End If
                                                    'mti May 05, 2011  -end


                                                End If 'Yr_Current - Yr_Prev >= grintrval Then
                                            End If 'Dataval < Val_Current
                                        End If ' IsNull(Val_Current) Then
                                    End If 'Int(.Fields(i).Name) <= (iYr + MaxForward) Then
                                End If 'dataval<>0
                            End If  'Not IsNull(Dataval) Then
                            'End If 'baseyear and back
                            'End If  'IsNumeric(.Fields(i).Name) Then
                        Next i
                    Else
                        Debug.Print("Years are supposed to be in ascending order")
                    End If

                End With
            Catch ex As Exception
                'MsgBox err.Description, vbCritical, "get Annual Growth Rate"
                Debug.Print(ex.Message)
            End Try

        End Function

        Public Sub BasePutAnnualGrowthOneDim(sVar As String, Vals As IDictionary(Of Integer, Single?), cntx As IRebuildFacade)

            Using db As IFs2Context = New IFs2Context()
                'db.Clear(GetType(AnnualGrowthRates))
                Dim NR As Short = CShort(cntx.GetDimension(1).Buckets.Count)
                Dim i As Short
                If cntx.HistoricMode Then
                    For i = 1 To NR
                        Dim row As AnnualGrowthRatesHist = New AnnualGrowthRatesHist
                        row.VarName = sVar
                        row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                        row.Dim2 = ""
                        row.Dim3 = ""
                        row.GrowthRate = Vals(i)
                        db.AnnualGrowthRatesHist.Add(row)
                    Next
                Else
                    For i = 1 To NR
                        Dim row As AnnualGrowthRates = New AnnualGrowthRates
                        row.VarName = sVar
                        row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                        row.Dim2 = ""
                        row.Dim3 = ""
                        row.GrowthRate = Vals(i)
                        db.AnnualGrowthRates.Add(row)
                    Next
                End If
                db.Flush()
            End Using
        End Sub

        Public Sub BasePutAnnualGrowthOneDimExt(sVar As String, Vals As IDictionary(Of Integer, Single?), cntx As IRebuildFacade,
                                                initval As Single?, InitYr As Single?, Optional FinYr As Single? = Nothing)

            Using db As IFs2Context = New IFs2Context()
                'db.Clear(GetType(AnnualGrowthRates))
                Dim NR As Short = CShort(cntx.GetDimension(1).Buckets.Count)
                Dim i As Short
                For i = 1 To NR
                    Dim row As AnnualGrowthRatesExt = New AnnualGrowthRatesExt
                    row.VarName = sVar
                    row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                    row.Dim2 = ""
                    row.Dim3 = ""
                    row.InitVal = initval
                    row.GrowthRate = Vals(i)
                    row.InitYear = InitYr
                    row.FinalYear = FinYr
                    db.AnnualGrowthRatesExt.Add(row)
                Next
                db.Flush()
            End Using
        End Sub

        Friend Sub ReadPopCohortDistCtry(ByRef cagedstM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef cagedstF As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef cntx As IRebuildFacade, ByRef db As IFsContext)
            Dim cohortval As Single?
            If cntx.HistoricMode Then
                'Dim ctrys As List(Of PopAgeCohortCountryMaleHist) = db.PopAgeCohortCountryMaleHist.ToList
                For Each ctry As PopAgeCohortCountryMaleHist In db.PopAgeCohortCountryMaleHist
                    cagedstM.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstM(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    'jrs 2020/03/18 a few cohorts (18 to 21 are not present)
                    For j = cntx.NCohorts To 17 Step -1
                        If cagedstM(ctry.CountryName).ContainsKey("Cohort" + j.ToString) Then
                            Exit For
                        Else
                            cagedstM(ctry.CountryName).Add("Cohort" + j.ToString, CSng(Math.Round(CSng(0.001 / 1000.0), 6)))
                        End If
                    Next
                Next

                'Dim ctrysF As List(Of PopAgeCohortCountryFemaleHist) = db.PopAgeCohortCountryFemaleHist.ToList
                For Each ctry As PopAgeCohortCountryFemaleHist In db.PopAgeCohortCountryFemaleHist
                    cagedstF.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstF(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    'jrs 2020/03/18 a few cohorts (18 to 21 are not present)
                    For j = cntx.NCohorts To 17 Step -1
                        If cagedstF(ctry.CountryName).ContainsKey("Cohort" + j.ToString) Then
                            Exit For
                        Else
                            cagedstF(ctry.CountryName).Add("Cohort" + j.ToString, CSng(Math.Round(CSng(0.001 / 1000.0), 6)))
                        End If
                    Next
                Next
            Else
                'Dim ctrys As List(Of PopAgeCohortCountryMale) = db.PopAgeCohortCountryMale.ToList
                For Each ctry As PopAgeCohortCountryMale In db.PopAgeCohortCountryMale
                    cagedstM.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        'j = j + 1
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstM(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next

                    'jrs 2024/12/20
                    'Need to fill the rest with 0.001
                    For j = cagedstM(ctry.CountryName).Count + 1 To NCohorts
                        cohortval = 0.001
                        cagedstM(ctry.CountryName).Add("Cohort" + j.ToString, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next

                Next

                'Dim ctrysF As List(Of PopAgeCohortCountryFemale) = db.PopAgeCohortCountryFemale.ToList
                For Each ctry As PopAgeCohortCountryFemale In db.PopAgeCohortCountryFemale
                    cagedstF.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstF(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next

                    'jrs 2024/12/20
                    'Need to fill the rest with 0.001
                    For j = cagedstF(ctry.CountryName).Count + 1 To NCohorts
                        cohortval = 0.001
                        cagedstF(ctry.CountryName).Add("Cohort" + j.ToString, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next

                Next
            End If
        End Sub

        'ttt 2019/03/29
        Friend Sub ReadPopCohortDistCtry(ByRef cagedstM As Dictionary(Of String, Dictionary(Of String, Single?)),
                                         ByRef cagedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                         ByRef cagedstNM As Dictionary(Of Integer, Dictionary(Of String, Single?)),
                                         ByRef cagedstNF As Dictionary(Of Integer, Dictionary(Of String, Single?)),
                                         ByRef cntx As IRebuildFacade, ByRef db As IFsContext)
            'ttt Add an OverLoad
            Dim cohortval As Single?
            Dim N As Integer = 1
            If cntx.HistoricMode Then
                'Dim ctrys As List(Of PopAgeCohortCountryMaleHist) = db.PopAgeCohortCountryMaleHist.ToList
                N = 1
                For Each ctry As PopAgeCohortCountryMaleHist In db.PopAgeCohortCountryMaleHist
                    cagedstM.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstM(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    cagedstNM.Add(N, cagedstM(ctry.CountryName))
                    N += 1
                Next

                'Dim ctrysF As List(Of PopAgeCohortCountryFemaleHist) = db.PopAgeCohortCountryFemaleHist.ToList
                N = 1
                For Each ctry As PopAgeCohortCountryFemaleHist In db.PopAgeCohortCountryFemaleHist
                    cagedstF.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstF(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    cagedstNF.Add(N, cagedstF(ctry.CountryName))
                    N += 1
                Next
            Else
                'Dim ctrys As List(Of PopAgeCohortCountryMale) = db.PopAgeCohortCountryMale.ToList
                N = 1
                For Each ctry As PopAgeCohortCountryMale In db.PopAgeCohortCountryMale
                    cagedstM.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        'j = j + 1
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstM(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    cagedstNM.Add(N, cagedstM(ctry.CountryName))
                    N += 1
                Next

                'Dim ctrysF As List(Of PopAgeCohortCountryFemale) = db.PopAgeCohortCountryFemale.ToList
                N = 1
                For Each ctry As PopAgeCohortCountryFemale In db.PopAgeCohortCountryFemale
                    cagedstF.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        cagedstF(ctry.CountryName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    cagedstNF.Add(N, cagedstF(ctry.CountryName))
                    N += 1
                Next
            End If
        End Sub

        'jrs 2019/04/10
        Friend Sub ReadPopCohortDist(ByRef agedstM As Dictionary(Of String, Dictionary(Of String, Single?)),
                                         ByRef agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                         ByRef cntx As IRebuildFacade, ByRef db As IFs2Context)
            'ttt Add an OverLoad
            Dim cohortval As Single?
            Dim N As Integer = 1
            If cntx.HistoricMode Then
                'Dim ctrys As List(Of PopAgeCohortCountryMaleHist) = db.PopAgeCohortCountryMaleHist.ToList
                N = 1
                For Each ctry As PopAgeCohortRegionMaleHist In db.PopAgeCohortRegionMaleHist
                    agedstM.Add(ctry.RegionName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        agedstM(ctry.RegionName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    N += 1
                Next

                'Dim ctrysF As List(Of PopAgeCohortCountryFemaleHist) = db.PopAgeCohortCountryFemaleHist.ToList
                N = 1
                For Each ctry As PopAgeCohortRegionFemaleHist In db.PopAgeCohortRegionFemaleHist
                    agedstF.Add(ctry.RegionName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        agedstF(ctry.RegionName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    N += 1
                Next
            Else
                'Dim ctrys As List(Of PopAgeCohortCountryMale) = db.PopAgeCohortCountryMale.ToList
                N = 1
                For Each ctry As PopAgeCohortRegionMale In db.PopAgeCohortRegionMale
                    agedstM.Add(ctry.RegionName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        'j = j + 1
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        agedstM(ctry.RegionName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    N += 1
                Next

                'Dim ctrysF As List(Of PopAgeCohortCountryFemale) = db.PopAgeCohortCountryFemale.ToList
                N = 1
                For Each ctry As PopAgeCohortRegionFemale In db.PopAgeCohortRegionFemale
                    agedstF.Add(ctry.RegionName, New Dictionary(Of String, Single?))
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        If Not cohortval.HasValue Then cohortval = 0.001
                        agedstF(ctry.RegionName).Add(cohort.Key, CSng(Math.Round(CSng(cohortval / 1000.0), 6)))
                    Next
                    N += 1
                Next
            End If
        End Sub

        'ttt 2019/10/08  Add Male-FeMale byref
        'jrs 2019/06/13
        Friend Sub ReadPopCohortDist(ByRef agedst(,,) As Double,
                                     ByRef cntx As IForecastContext, ByRef db As IFs2Context,
                                     ByRef POPMale() As Double, ByRef POPFemale() As Double)
            'ttt Add an OverLoad
            Dim cohortval As Double?
            Dim j As Integer, R As Integer
            If cntx.HistoricMode Then
                For Each ctry As PopAgeCohortRegionMaleHist In db.PopAgeCohortRegionMaleHist
                    'ttt 2019/10/08
                    Dim val As Double = 0!
                    R = ctry.RegionId
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = cohort.Value
                        If Not cohortval.HasValue Then cohortval = 0.001
                        j = CInt(cohort.Key.Replace("Cohort", ""))
                        If j <= NCohorts Then
                            agedst(j, R, 1) = Math.Round(CSng(cohortval / 1000.0), 6)
                            'ttt 2019/10/08
                            val += agedst(j, R, 1)
                        End If
                    Next
                    'ttt 2019/10/08
                    POPMale(R) = val
                Next

                For Each ctry As PopAgeCohortRegionFemaleHist In db.PopAgeCohortRegionFemaleHist
                    'ttt 2019/10/08
                    Dim val As Double = 0!
                    R = ctry.RegionId
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = cohort.Value
                        If Not cohortval.HasValue Then cohortval = 0.001
                        j = CInt(cohort.Key.Replace("Cohort", ""))
                        agedst(j, R, 2) = Math.Round(CSng(cohortval / 1000.0), 6)
                        'ttt 2019/10/08
                        val += agedst(j, R, 2)
                    Next
                    'ttt 2019/10/08
                    POPFemale(R) = val
                Next
            Else
                For Each ctry As PopAgeCohortRegionMale In db.PopAgeCohortRegionMale
                    'ttt 2019/10/08
                    Dim val As Double = 0!
                    R = ctry.RegionId
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        'j = j + 1
                        cohortval = cohort.Value
                        If Not cohortval.HasValue Then cohortval = 0.001
                        j = CInt(cohort.Key.Replace("Cohort", ""))
                        agedst(j, R, 1) = Math.Round(CSng(cohortval / 1000.0), 6)
                        'ttt 2019/10/08
                        'z191 remade from : POPMale = POPMale + agedst(j%, R%, 1) etc
                        val += agedst(j, R, 1)
                    Next
                    'ttt 2019/10/08
                    POPMale(R) = val
                Next

                For Each ctry As PopAgeCohortRegionFemale In db.PopAgeCohortRegionFemale
                    'ttt 2019/10/08
                    Dim val As Double = 0!
                    R = ctry.RegionId
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = cohort.Value
                        If Not cohortval.HasValue Then cohortval = 0.001
                        j = CInt(cohort.Key.Replace("Cohort", ""))
                        agedst(j, R, 2) = Math.Round(CSng(cohortval / 1000.0), 6)
                        'ttt 2019/10/08
                        val += agedst(j, R, 2)
                    Next
                    'ttt 2019/10/08
                    POPFemale(R) = val
                Next
            End If
        End Sub


        'jrs 2025/01/22
        Friend Sub ReadPopSingleAgeDist(ByRef fagedst(,,) As Double,
                                     ByRef cntx As IForecastContext, ByRef db As IFs2Context)
            Dim cohortval As Double?
            Dim j As Integer, R As Integer
            If cntx.HistoricMode Then
                Exit Sub
            Else
                For Each ctry As PopAgeSingleRegionMale In db.PopAgeSingleRegionMale
                    R = ctry.RegionId
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Ages
                        cohortval = cohort.Value
                        If Not cohortval.HasValue Then cohortval = 0.001
                        j = CInt(cohort.Key.Replace("Age", ""))
                        fagedst(j, R, 1) = Math.Round(CSng(cohortval / 1000.0), 6)
                    Next
                Next

                For Each ctry As PopAgeSingleRegionFemale In db.PopAgeSingleRegionFemale
                    R = ctry.RegionId
                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Ages
                        cohortval = cohort.Value
                        If Not cohortval.HasValue Then cohortval = 0.001
                        j = CInt(cohort.Key.Replace("Age", ""))
                        fagedst(j, R, 2) = Math.Round(CSng(cohortval / 1000.0), 6)
                    Next
                Next
            End If
        End Sub

        Friend Sub AdjustCohPop(ByRef cagedstM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef cagedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                       ByRef CPOP As IDictionary(Of String, Single?), ByVal sCtry As String, ByRef cntx As IRebuildFacade)
            Dim P As Single, tmpVal As Single
            P = 0
            For Each cohort As KeyValuePair(Of String, Single?) In cagedstM(sCtry)
                P = P + CSng(cohort.Value)
            Next
            For Each cohort As KeyValuePair(Of String, Single?) In cagedstF(sCtry)
                P = P + CSng(cohort.Value)
            Next
            Dim cohort2 As KeyValuePair(Of String, Single?)
            Dim i As Integer
            For i = 0 To cagedstM(sCtry).Count - 1
                cohort2 = cagedstM(sCtry).ElementAt(i)
                tmpVal = CSng(cohort2.Value)
                cagedstM(sCtry)(cohort2.Key) = tmpVal * CPOP(sCtry) / P
            Next
            For i = 0 To cagedstF(sCtry).Count - 1
                cohort2 = cagedstF(sCtry).ElementAt(i)
                tmpVal = CSng(cohort2.Value)
                cagedstF(sCtry)(cohort2.Key) = tmpVal * CPOP(sCtry) / P
            Next
        End Sub

        Friend Sub AdjustCohPop(ByRef agedstM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef agedstF As Dictionary(Of String, Dictionary(Of String, Single?)),
                                       ByRef POP As IDictionary(Of Integer, Single?), ByVal R As Integer, ByRef cntx As IRebuildFacade)
            Dim P As Single, tmpVal As Single, sReg As String
            sReg = cntx.GetDimension(1).Buckets.Item(CShort(R))

            P = 0
            For Each cohort As KeyValuePair(Of String, Single?) In agedstM(sReg)
                P = P + CSng(cohort.Value)
            Next
            For Each cohort As KeyValuePair(Of String, Single?) In agedstF(sReg)
                P = P + CSng(cohort.Value)
            Next
            Dim cohort2 As KeyValuePair(Of String, Single?)
            Dim i As Integer
            For i = 0 To agedstM(sReg).Count - 1
                cohort2 = agedstM(sReg).ElementAt(i)
                tmpVal = CSng(cohort2.Value)
                agedstM(sReg)(cohort2.Key) = tmpVal * POP(R) / P
            Next
            For i = 0 To agedstF(sReg).Count - 1
                cohort2 = agedstF(sReg).ElementAt(i)
                tmpVal = CSng(cohort2.Value)
                agedstF(sReg)(cohort2.Key) = tmpVal * POP(R) / P
            Next
        End Sub

        Friend Sub AdjustCohPop(ByRef agedst As Double(,,), ByRef POP As Single(), ByVal R As Integer)
            Dim P As Double
            P = 0
            For j% = 1 To NCohorts
                P = P + agedst(j%, R%, 1) + agedst(j%, R%, 2)
            Next j%
            For j% = 1 To NCohorts
                agedst(j%, R%, 1) = agedst(j%, R%, 1) * POP(R%) / P
                agedst(j%, R%, 2) = agedst(j%, R%, 2) * POP(R%) / P
            Next j%
        End Sub

        Friend Sub RegDistribution(Of T As {New, ICohortRegion, CohortRegion})(VarCtry As IEnumerable(Of CohortCountry), cntx As IRebuildFacade, VarReg As Microsoft.EntityFrameworkCore.DbSet(Of T))
            Dim CohortDistrib As Dictionary(Of String, Dictionary(Of String, Double?)) = New Dictionary(Of String, Dictionary(Of String, Double?))
            '   Read Cohort Distribution Data - "COUNTRY" Data
            '   ----------------------------------------------
            Dim cohortval As Double?
            For Each ctry As CohortCountry In VarCtry
                CohortDistrib.Add(ctry.CountryName, New Dictionary(Of String, Double?))
                For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                    cohortval = cohort.Value
                    If Not cohortval.HasValue Then cohortval = 0.001
                    CohortDistrib(ctry.CountryName).Add(cohort.Key, AMAX(cohortval, 0.001F))
                Next
            Next
            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim CoHold As Dictionary(Of String, Double?)
            Dim CohortName As String
            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                CoHold = New Dictionary(Of String, Double?)
                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    For cocount% = 0 To cntx.NCohorts
                        CohortName = "Cohort" & cocount.ToString
                        If CoHold.ContainsKey(CohortName) Then
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold(CohortName) = CoHold(CohortName) + CohortDistrib(ctry.Key)(CohortName)
                            End If
                        Else
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold.Add(CohortName, CohortDistrib(ctry.Key)(CohortName))
                            Else
                                CoHold.Add(CohortName, 0.001)
                            End If
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                'Dim row As CohortRegion = New CohortRegion ' New cannot be used on a class that is MustInherit
                'Dim row As PopAgeCohortRegionMale = New PopAgeCohortRegionMale
                Dim row As T = New T()
                row.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(regcount%))
                row.RegionId = regcount%

                For cocount% = 0 To cntx.NCohorts
                    CohortName = "Cohort" & cocount.ToString
                    Select Case cocount
                        Case 0
                            row.Cohort0 = CoHold(CohortName)
                        Case 1
                            row.Cohort1 = CoHold(CohortName)
                        Case 2
                            row.Cohort2 = CoHold(CohortName)
                        Case 3
                            row.Cohort3 = CoHold(CohortName)
                        Case 4
                            row.Cohort4 = CoHold(CohortName)
                        Case 5
                            row.Cohort5 = CoHold(CohortName)
                        Case 6
                            row.Cohort6 = CoHold(CohortName)
                        Case 7
                            row.Cohort7 = CoHold(CohortName)
                        Case 8
                            row.Cohort8 = CoHold(CohortName)
                        Case 9
                            row.Cohort9 = CoHold(CohortName)
                        Case 10
                            row.Cohort10 = CoHold(CohortName)
                        Case 11
                            row.Cohort11 = CoHold(CohortName)
                        Case 12
                            row.Cohort12 = CoHold(CohortName)
                        Case 13
                            row.Cohort13 = CoHold(CohortName)
                        Case 14
                            row.Cohort14 = CoHold(CohortName)
                        Case 15
                            row.Cohort15 = CoHold(CohortName)
                        Case 16
                            row.Cohort16 = CoHold(CohortName)
                        Case 17
                            row.Cohort17 = CoHold(CohortName)
                        Case 18
                            row.Cohort18 = CoHold(CohortName)
                        Case 19
                            row.Cohort19 = CoHold(CohortName)
                        Case 20
                            row.Cohort20 = CoHold(CohortName)
                        Case 21
                            row.Cohort21 = CoHold(CohortName)
                        Case 22
                            row.Cohort22 = CoHold(CohortName)
                        Case 23
                            row.Cohort23 = CoHold(CohortName)
                        Case 24
                            row.Cohort24 = CoHold(CohortName)
                        Case 25
                            row.Cohort25 = CoHold(CohortName)
                        Case 26
                            row.Cohort26 = CoHold(CohortName)
                        Case 27
                            row.Cohort27 = CoHold(CohortName)
                        Case 28
                            row.Cohort28 = CoHold(CohortName)
                        Case 29
                            row.Cohort29 = CoHold(CohortName)
                        Case 30
                            row.Cohort30 = CoHold(CohortName)
                        Case 31
                            row.Cohort31 = CoHold(CohortName)
                        Case 32
                            row.Cohort32 = CoHold(CohortName)
                        Case 33
                            row.Cohort33 = CoHold(CohortName)
                        Case 34
                            row.Cohort34 = CoHold(CohortName)
                        Case 35
                            row.Cohort35 = CoHold(CohortName)
                        Case 36
                            row.Cohort36 = CoHold(CohortName)
                        Case 37
                            row.Cohort37 = CoHold(CohortName)
                        Case 38
                            row.Cohort38 = CoHold(CohortName)
                        Case 39
                            row.Cohort39 = CoHold(CohortName)
                        Case 40
                            row.Cohort40 = CoHold(CohortName)
                        Case 41
                            row.Cohort41 = CoHold(CohortName)
                    End Select
                Next
                VarReg.Add(row)
            Next

        End Sub

        Friend Sub RegDistributionWAvg(Of T As {New, ICohortRegion, CohortRegion})(VarCtry As IEnumerable(Of CohortCountry), cntx As IRebuildFacade, VarReg As Microsoft.EntityFrameworkCore.DbSet(Of T), bMortality As Boolean)
            Dim CPOP As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Dim CohortDistrib As Dictionary(Of String, Dictionary(Of String, Double?)) = New Dictionary(Of String, Dictionary(Of String, Double?))
            '   Read Cohort Distribution Data - "COUNTRY" Data
            '   ----------------------------------------------
            Dim cohortval As Double?
            For Each ctry As CohortCountry In VarCtry
                CohortDistrib.Add(ctry.CountryName, New Dictionary(Of String, Double?))
                For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                    cohortval = cohort.Value
                    CohortDistrib(ctry.CountryName).Add(cohort.Key, cohortval)

                    If Not CohortDistrib(ctry.CountryName)(cohort.Key).HasValue And cohort.Key = "Cohort5" Then
                        CohortDistrib(ctry.CountryName).Remove(cohort.Key)
                        CohortDistrib(ctry.CountryName).Add(cohort.Key, 0.001)
                    ElseIf Not CohortDistrib(ctry.CountryName)(cohort.Key).HasValue Then
                        CohortDistrib(ctry.CountryName).Remove(cohort.Key)
                        CohortDistrib(ctry.CountryName).Add(cohort.Key, 0.0001)
                    End If
                Next
            Next
            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim CoHold As Dictionary(Of String, Double?)
            Dim CoWeight As Dictionary(Of String, Double?)
            Dim CoValue As Dictionary(Of String, Double?)
            Dim CohortName As String
            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                CoHold = New Dictionary(Of String, Double?)
                CoWeight = New Dictionary(Of String, Double?)
                CoValue = New Dictionary(Of String, Double?)
                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    For cocount% = 0 To cntx.NCohorts
                        CohortName = "Cohort" & cocount.ToString
                        If CoHold.ContainsKey(CohortName) Then
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold(CohortName) = CoHold(CohortName) + CohortDistrib(ctry.Key)(CohortName) * CPOP(ctry.Key)
                                CoWeight(CohortName) = CoWeight(CohortName) + CPOP(ctry.Key)
                            End If
                        Else
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold.Add(CohortName, CohortDistrib(ctry.Key)(CohortName) * CPOP(ctry.Key))
                                CoWeight.Add(CohortName, CPOP(ctry.Key))
                            Else
                                CoHold.Add(CohortName, 0.001)
                                CoWeight.Add(CohortName, 1)
                            End If
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                'Dim row As CohortRegion = New CohortRegion ' New cannot be used on a class that is MustInherit
                'Dim row As PopMortalityCohortRegionMale = New PopMortalityCohortRegionMale
                Dim row As T = New T()
                row.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(regcount%))
                row.RegionId = regcount%

                For cocount% = 0 To cntx.NCohorts
                    CohortName = "Cohort" & cocount.ToString
                    If CoWeight(CohortName) > 0 Then
                        CoValue.Add(CohortName, CoHold(CohortName) / CoWeight(CohortName))
                    Else
                        CoValue.Add(CohortName, Nothing)
                    End If
                Next

                If bMortality Then
                    '   Compute Mortality Rates from Survivor Tables
                    '   --------------------------------------------

                    'jrs 2009/05/21
                    '--------------
                    'explanation by Randall Kuhn
                    'When we do (Survivor(t)-Survivor(t-1))/Survivor(t-1) we're computing nqx, n = t, and x = t-1
                    'we were using nmx = nqx/n
                    'nqx is probability of ever dying between age x and x+n
                    'we should be using mqx
                    'mqx is the probability that someone age x to x+n dies in the current year
                    'nmx = ( 1 - ( ( 1 - nqx ) ^ 1/n ) )
                    'we multiply times 1000 to convert to deaths per 1,000 people
                    Dim nqx As Double, msg As String, tmpVal As Double, CohortM1Name As String, tmpM1Val As Double
                    For cocount% = cntx.NCohorts - 1 To 0 Step -1
                        CohortName = "Cohort" & cocount.ToString
                        CohortM1Name = "Cohort" & (cocount - 1).ToString
                        If Not CoValue(CohortName).HasValue Then
                            msg$ = "Fatal error:  No non-null members of Region "
                            msg$ = msg$ + cntx.GetDimension(1).Buckets(CShort(regcount%)) + " for population cohort "
                            msg$ = msg$ + CohortName + " - cannot compute mortality"
                            'MsgBox msg$
                            Stop
                        End If
                        tmpVal = CDbl(CoValue(CohortName))
                        If cocount% > 0 Then
                            tmpM1Val = CDbl(CoValue(CohortM1Name))
                        End If
                        CoValue.Remove(CohortName)
                        If cocount% = 0 Then
                            If cntx.HistoricMode Then
                                'CoValue(cocount%) = INFMOR(regcount%)
                            Else
                                CoValue.Add(CohortName, (100000 - tmpVal) / 100000 * 1000)
                            End If
                            'Note:  UN Pop Rev does not have survival tables for 1960, so all
                            '   mortality data are actually from 2000; this is a problem in
                            '   general but especially for infant mortality and there are data
                            '   for inf mort; thus swapped in here
                        ElseIf cocount% = 1 Then
                            nqx = (tmpM1Val - tmpVal) / tmpM1Val
                            CoValue.Add(CohortName, (1 - ((1 - nqx) ^ (1 / 4))) * 1000)
                        Else
                            nqx = (tmpM1Val - tmpVal) / tmpM1Val
                            CoValue.Add(CohortName, (1 - ((1 - nqx) ^ (1 / 5))) * 1000)
                        End If
                    Next
                    'jrs 2012/09/03
                    Dim CohortM2Name As String, tmpM2Val As Double
                    CohortName = "Cohort" & cntx.NCohorts.ToString
                    CohortM1Name = "Cohort" & (cntx.NCohorts - 1).ToString
                    CohortM2Name = "Cohort" & (cntx.NCohorts - 2).ToString
                    tmpVal = CDbl(CoValue(CohortName))
                    tmpM1Val = CDbl(CoValue(CohortM1Name))
                    tmpM2Val = CDbl(CoValue(CohortM2Name))
                    CoValue.Remove(CohortName)

                    If CoValue(CohortM1Name) = 0 Then
                        CoValue.Add(CohortName, 0)
                    Else
                        CoValue.Add(CohortName, Amin(tmpM1Val +
                    AMAX(45, (tmpM1Val - tmpM2Val)) * 10, 1000)) 'jrs 2009/05/26 adding limit of 1000
                    End If
                    ' UN data does not include mortality above 100; developed this to
                    ' produce reasonable forecast for US in 2050; ie like UN scenario

                End If

                For cocount% = 0 To cntx.NCohorts
                    CohortName = "Cohort" & cocount.ToString
                    Select Case cocount
                        Case 0
                            row.Cohort0 = CoValue(CohortName)
                        Case 1
                            row.Cohort1 = CoValue(CohortName)
                        Case 2
                            row.Cohort2 = CoValue(CohortName)
                        Case 3
                            row.Cohort3 = CoValue(CohortName)
                        Case 4
                            row.Cohort4 = CoValue(CohortName)
                        Case 5
                            row.Cohort5 = CoValue(CohortName)
                        Case 6
                            row.Cohort6 = CoValue(CohortName)
                        Case 7
                            row.Cohort7 = CoValue(CohortName)
                        Case 8
                            row.Cohort8 = CoValue(CohortName)
                        Case 9
                            row.Cohort9 = CoValue(CohortName)
                        Case 10
                            row.Cohort10 = CoValue(CohortName)
                        Case 11
                            row.Cohort11 = CoValue(CohortName)
                        Case 12
                            row.Cohort12 = CoValue(CohortName)
                        Case 13
                            row.Cohort13 = CoValue(CohortName)
                        Case 14
                            row.Cohort14 = CoValue(CohortName)
                        Case 15
                            row.Cohort15 = CoValue(CohortName)
                        Case 16
                            row.Cohort16 = CoValue(CohortName)
                        Case 17
                            row.Cohort17 = CoValue(CohortName)
                        Case 18
                            row.Cohort18 = CoValue(CohortName)
                        Case 19
                            row.Cohort19 = CoValue(CohortName)
                        Case 20
                            row.Cohort20 = CoValue(CohortName)
                        Case 21
                            row.Cohort21 = CoValue(CohortName)
                        Case 22
                            row.Cohort22 = CoValue(CohortName)
                        Case 23
                            row.Cohort23 = CoValue(CohortName)
                        Case 24
                            row.Cohort24 = CoValue(CohortName)
                        Case 25
                            row.Cohort25 = CoValue(CohortName)
                        Case 26
                            row.Cohort26 = CoValue(CohortName)
                        Case 27
                            row.Cohort27 = CoValue(CohortName)
                        Case 28
                            row.Cohort28 = CoValue(CohortName)
                        Case 29
                            row.Cohort29 = CoValue(CohortName)
                        Case 30
                            row.Cohort30 = CoValue(CohortName)
                        Case 31
                            row.Cohort31 = CoValue(CohortName)
                        Case 32
                            row.Cohort32 = CoValue(CohortName)
                        Case 33
                            row.Cohort33 = CoValue(CohortName)
                        Case 34
                            row.Cohort34 = CoValue(CohortName)
                        Case 35
                            row.Cohort35 = CoValue(CohortName)
                        Case 36
                            row.Cohort36 = CoValue(CohortName)
                        Case 37
                            row.Cohort37 = CoValue(CohortName)
                        Case 38
                            row.Cohort38 = CoValue(CohortName)
                        Case 39
                            row.Cohort39 = CoValue(CohortName)
                        Case 40
                            row.Cohort40 = CoValue(CohortName)
                        Case 41
                            row.Cohort41 = CoValue(CohortName)
                    End Select
                Next

                VarReg.Add(row)
            Next


        End Sub

        Friend Sub RegDistributionFert(Of T As {New, ICohortRegion4, CohortRegion4})(VarCtry As IEnumerable(Of FertCohortCountry), cntx As IRebuildFacade, VarReg As Microsoft.EntityFrameworkCore.DbSet(Of T))
            Dim CPOP As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Dim CohortDistrib As Dictionary(Of String, Dictionary(Of String, Double?)) = New Dictionary(Of String, Dictionary(Of String, Double?))
            '   Read Cohort Distribution Data - "COUNTRY" Data
            '   ----------------------------------------------
            Dim cohortval As Double?, cohortNo As Integer
            For Each ctry As FertCohortCountry In VarCtry
                CohortDistrib.Add(ctry.CountryName, New Dictionary(Of String, Double?))
                For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                    cohortval = cohort.Value
                    cohortNo = CInt(cohort.Key.Replace("Cohort", ""))
                    'jrs 2023/12/05 changed > 3 to > 2 to include Cohort3
                    If cohortNo > 2 And cohortNo <= cntx.LastCohortFert Then
                        CohortDistrib(ctry.CountryName).Add(cohort.Key, If(cohortval, 0.001))
                    End If
                Next
            Next
            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim CoHold As Dictionary(Of String, Double?)
            Dim CoWeight As Dictionary(Of String, Double?)
            Dim CoValue As Dictionary(Of String, Double?)
            Dim CohortName As String
            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                CoHold = New Dictionary(Of String, Double?)
                CoWeight = New Dictionary(Of String, Double?)
                CoValue = New Dictionary(Of String, Double?)
                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    'jrs 2023/12/05 changed 4 to 3 to start at cohort 3
                    For cocount% = 3 To cntx.LastCohortFert
                        CohortName = "Cohort" & cocount.ToString
                        If CoHold.ContainsKey(CohortName) Then
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold(CohortName) = CoHold(CohortName) + CohortDistrib(ctry.Key)(CohortName) * CPOP(ctry.Key)
                                CoWeight(CohortName) = CoWeight(CohortName) + CPOP(ctry.Key)
                            End If
                        Else
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold.Add(CohortName, CohortDistrib(ctry.Key)(CohortName) * CPOP(ctry.Key))
                                CoWeight.Add(CohortName, CPOP(ctry.Key))
                            Else
                                CoHold.Add(CohortName, 0.001)
                                CoWeight.Add(CohortName, 1)
                            End If
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                'Dim row As CohortRegion = New CohortRegion ' New cannot be used on a class that is MustInherit
                'Dim row As PopFertilityCohortRegionFemale = New PopFertilityCohortRegionFemale
                Dim row As T = New T()
                row.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(regcount%))
                row.RegionId = regcount%

                'jrs 2023/12/05 changed from 3 to 4 to include Cohort 3
                For cocount% = 3 To cntx.LastCohortFert
                    CohortName = "Cohort" & cocount.ToString
                    If CoWeight(CohortName) > 0 Then
                        CoValue.Add(CohortName, CoHold(CohortName) / CoWeight(CohortName))
                    Else
                        CoValue.Add(CohortName, Nothing)
                    End If
                Next

                'jrs 2023/12/05 changed from 4 to 3 to include cohort 3
                For cocount% = 3 To cntx.LastCohortFert
                    CohortName = "Cohort" & cocount.ToString
                    Select Case cocount
                        Case 3 'jrs 2023/12/05
                            row.Cohort3 = CoValue(CohortName)
                        Case 4
                            row.Cohort4 = CoValue(CohortName)
                        Case 5
                            row.Cohort5 = CoValue(CohortName)
                        Case 6
                            row.Cohort6 = CoValue(CohortName)
                        Case 7
                            row.Cohort7 = CoValue(CohortName)
                        Case 8
                            row.Cohort8 = CoValue(CohortName)
                        Case 9
                            row.Cohort9 = CoValue(CohortName)
                        Case 10
                            row.Cohort10 = CoValue(CohortName)
                        Case 11
                            row.Cohort11 = CoValue(CohortName)
                        Case 12
                            row.Cohort12 = CoValue(CohortName)
                        Case 13
                            row.Cohort13 = CoValue(CohortName)
                        Case 14
                            row.Cohort14 = CoValue(CohortName)
                        Case 15
                            row.Cohort15 = CoValue(CohortName)
                        Case 16
                            row.Cohort16 = CoValue(CohortName)
                        Case 17
                            row.Cohort17 = CoValue(CohortName)
                        Case 18
                            row.Cohort18 = CoValue(CohortName)
                        Case 19
                            row.Cohort19 = CoValue(CohortName)
                        Case 20
                            row.Cohort20 = CoValue(CohortName)
                        Case 21
                            row.Cohort21 = CoValue(CohortName)
                        Case 22
                            row.Cohort22 = CoValue(CohortName)
                        Case 23
                            row.Cohort23 = CoValue(CohortName)
                        Case 24
                            row.Cohort24 = CoValue(CohortName)
                        Case 25
                            row.Cohort25 = CoValue(CohortName)
                        Case 26
                            row.Cohort26 = CoValue(CohortName)
                        Case 27
                            row.Cohort27 = CoValue(CohortName)
                        Case 28
                            row.Cohort28 = CoValue(CohortName)
                        Case 29
                            row.Cohort29 = CoValue(CohortName)
                        Case 30
                            row.Cohort30 = CoValue(CohortName)
                        Case 31
                            row.Cohort31 = CoValue(CohortName)
                        Case 32
                            row.Cohort32 = CoValue(CohortName)
                        Case 33
                            row.Cohort33 = CoValue(CohortName)
                        Case 34
                            row.Cohort34 = CoValue(CohortName)
                        Case 35
                            row.Cohort35 = CoValue(CohortName)
                        Case 36
                            row.Cohort36 = CoValue(CohortName)
                        Case 37
                            row.Cohort37 = CoValue(CohortName)
                        Case 38
                            row.Cohort38 = CoValue(CohortName)
                        Case 39
                            row.Cohort39 = CoValue(CohortName)
                        Case 40
                            row.Cohort40 = CoValue(CohortName)
                        Case 41
                            row.Cohort41 = CoValue(CohortName)
                    End Select
                Next

                VarReg.Add(row)
            Next

        End Sub

        Friend Sub RegionalizeAgegroupWgt(Of T As {New, IEduCohortRegion, EduCohortRegion})(VarCtry As IEnumerable(Of EduCohortCountry),
                                                                                      cntx As IRebuildFacade,
                                                                                      VarReg As Microsoft.EntityFrameworkCore.DbSet(Of T),
                                                                                      CWeightByAge As Dictionary(Of String, Dictionary(Of String, Single?)))

            Dim CohortDistrib As Dictionary(Of String, Dictionary(Of String, Double?)) = New Dictionary(Of String, Dictionary(Of String, Double?))
            'mti 2019/11/01 New proc
            ' this is based on JRS' regdistribution in datapop; however this allosw weighting through age-group data
            ' VarCtry is the name of table containing data by age-group, for a particular year
            ' VarReg is the region table that will hold (country->region) aggregated values
            ' CWeightByAge is the table containing weight values (for example, population by age-groups)
            ' Tblweight1 will contain the second table with weighths for aggregation (e.g., male and female population, both tables are needed to compute total)
            ' Colprefix.. function inputs are used for indicating the Column names of the Access tables
            '..AGNo parameters are for age-group numbers

            '   Read Cohort Distribution Data - "COUNTRY" Data
            '   ----------------------------------------------
            Dim cohortval As Double?
            For Each ctry As EduCohortCountry In VarCtry
                CohortDistrib.Add(ctry.CountryName, New Dictionary(Of String, Double?))
                For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                    cohortval = cohort.Value
                    CohortDistrib(ctry.CountryName).Add(cohort.Key, cohortval)  'cohort.Key = "AgeGroup4" etc

                    If Not CohortDistrib(ctry.CountryName)(cohort.Key).HasValue And cohort.Key = "Cohort5" Then
                        CohortDistrib(ctry.CountryName).Remove(cohort.Key)
                        CohortDistrib(ctry.CountryName).Add(cohort.Key, 0.001)
                    ElseIf Not CohortDistrib(ctry.CountryName)(cohort.Key).HasValue Then
                        CohortDistrib(ctry.CountryName).Remove(cohort.Key)
                        CohortDistrib(ctry.CountryName).Add(cohort.Key, 0.0001)
                    End If
                Next
            Next

            'Save Serbia into Kosovo
            For Each cohort As KeyValuePair(Of String, Double?) In CohortDistrib("Serbia")
                CohortDistrib("Kosovo")(cohort.Key) = cohort.Value
            Next

            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim CoHold As Dictionary(Of String, Double?)
            Dim CoWeight As Dictionary(Of String, Double?)
            Dim CoValue As Dictionary(Of String, Double?)
            Dim CohortName As String
            Dim EdCohortName As String 'mti 2024/05/23 
            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                CoHold = New Dictionary(Of String, Double?)
                CoWeight = New Dictionary(Of String, Double?)
                CoValue = New Dictionary(Of String, Double?)
                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    For cocount% = 0 To cntx.NCohorts
                        EdCohortName = "AgeGroup" & cocount.ToString   'mti 2024/05/23 "Cohort"  - reverted changing to agegroup as field name
                        CohortName = "Cohort" & cocount.ToString
                        If CoHold.ContainsKey(CohortName) Then
                            'mti 2024/05/23 start

                            If CohortDistrib(ctry.Key).ContainsKey(EdCohortName) Then
                                CoHold(CohortName) = CoHold(CohortName) + CohortDistrib(ctry.Key)(EdCohortName) * CWeightByAge(ctry.Key)(CohortName)
                                CoWeight(CohortName) = CoWeight(CohortName) + CWeightByAge(ctry.Key)(CohortName)
                            End If
                            'If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                            '    CoHold(CohortName) = CoHold(CohortName) + CohortDistrib(ctry.Key)(CohortName) * CWeightByAge(ctry.Key)(CohortName)
                            '    CoWeight(CohortName) = CoWeight(CohortName) + CWeightByAge(ctry.Key)(CohortName)
                            'End If
                            'mti 2024/05/23 end
                        Else
                            'mti 2024/05/23 start
                            If CohortDistrib(ctry.Key).ContainsKey(EdCohortName) Then
                                If CohortDistrib(ctry.Key)(EdCohortName).HasValue Then
                                    CoHold.Add(CohortName, CohortDistrib(ctry.Key)(EdCohortName) * CWeightByAge(ctry.Key)(CohortName))
                                    CoWeight.Add(CohortName, CWeightByAge(ctry.Key)(CohortName))
                                Else
                                    CoHold.Add(CohortName, 0.001)
                                    CoWeight.Add(CohortName, 1)
                                End If
                            End If

                            'original from JRS
                            'If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                            '    CoHold.Add(CohortName, CohortDistrib(ctry.Key)(CohortName) * CWeightByAge(ctry.Key)(CohortName))
                            '    CoWeight.Add(CohortName, CWeightByAge(ctry.Key)(CohortName))
                            'Else
                            '    CoHold.Add(CohortName, 0.001)
                            '    CoWeight.Add(CohortName, 1)
                            'End If
                            'mti 2024/05/23 end
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                'Dim row As CohortRegion = New CohortRegion ' New cannot be used on a class that is MustInherit
                'Dim row As PopMortalityCohortRegionMale = New PopMortalityCohortRegionMale
                Dim row As T = New T()
                row.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(regcount%))
                row.RegionId = regcount%

                For cocount% = 0 To cntx.NCohorts
                    CohortName = "Cohort" & cocount.ToString

                    If CoWeight.ContainsKey(CohortName) Then 'mti 2024/05/23 
                        If CoWeight(CohortName) > 0 Then
                            CoValue.Add(CohortName, CoHold(CohortName) / CoWeight(CohortName))
                        Else
                            CoValue.Add(CohortName, Nothing)
                        End If
                    End If  'mti 2024/05/23
                Next

                For cocount% = 0 To cntx.NCohorts
                    CohortName = "Cohort" & cocount.ToString

                    'jrs 2024/05/31
                    If CoValue.ContainsKey(CohortName) Then
                        Select Case cocount
                            Case 4
                                row.Cohort4 = CoValue(CohortName)
                            Case 5
                                row.Cohort5 = CoValue(CohortName)
                            Case 6
                                row.Cohort6 = CoValue(CohortName)
                            Case 7
                                row.Cohort7 = CoValue(CohortName)
                            Case 8
                                row.Cohort8 = CoValue(CohortName)
                            Case 9
                                row.Cohort9 = CoValue(CohortName)
                            Case 10
                                row.Cohort10 = CoValue(CohortName)
                            Case 11
                                row.Cohort11 = CoValue(CohortName)
                            Case 12
                                row.Cohort12 = CoValue(CohortName)
                            Case 13
                                row.Cohort13 = CoValue(CohortName)
                            Case 14
                                row.Cohort14 = CoValue(CohortName)
                            Case 15
                                row.Cohort15 = CoValue(CohortName)
                            Case 16
                                row.Cohort16 = CoValue(CohortName)
                            Case 17
                                row.Cohort17 = CoValue(CohortName)
                            Case 18
                                row.Cohort18 = CoValue(CohortName)
                            Case 19
                                row.Cohort19 = CoValue(CohortName)
                            Case 20
                                row.Cohort20 = CoValue(CohortName)
                            Case 21
                                row.Cohort21 = CoValue(CohortName)
                        End Select
                    End If
                Next

                VarReg.Add(row)
            Next


        End Sub

        'jrs 2019/06/13
        Friend Sub ReadMortCohortDist(ByRef BasMordst(,,) As Single, cntx As IForecastContext)
            'BasMordst As Variant, coname As Variant, db As Database)
            Dim msg$ = ""
            Using db2 As IFs2Context = New IFs2Context()
                If cntx.HistoricMode Then
                    '  Set valtbl = db.OpenRecordset("PopMortalityCohortRegionMaleHist")
                    'gvdb 2024/04/05
                    ' update for new EF version syntax
                    'Dim qr = (From p In db2.PopMortalityCohortRegionMaleHist Select p).DistinctBy(Function(p) p.RegionId)
                    Dim qr = db2.PopMortalityCohortRegionMaleHist.AsEnumerable().DistinctBy(Function(p) p.RegionId)
                    For Each valrec As PopMortalityCohortRegionMaleHist In qr ' db2.PopMortalityCohortRegionMaleHist.OrderBy(Function(y) y.RegionId)
                        Dim rec = New Dictionary(Of String, Single?)
                        For j% = 0 To cntx.NCohorts
                            ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                            'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                            Select Case j
                                Case 0
                                    If Not valrec.Cohort0.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort0 / 1000)
                                    End If
                                Case 1
                                    If Not valrec.Cohort1.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort1 / 1000)
                                    End If
                                Case 2
                                    If Not valrec.Cohort2.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort2 / 1000)
                                    End If
                                Case 3
                                    If Not valrec.Cohort3.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort3 / 1000)
                                    End If
                                Case 4
                                    If Not valrec.Cohort4.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort4 / 1000)
                                    End If
                                Case 5
                                    If Not valrec.Cohort5.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort5 / 1000)
                                    End If
                                Case 6
                                    If Not valrec.Cohort6.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort6 / 1000)
                                    End If
                                Case 7
                                    If Not valrec.Cohort7.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort7 / 1000)
                                    End If
                                Case 8
                                    If Not valrec.Cohort8.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort8 / 1000)
                                    End If
                                Case 9
                                    If Not valrec.Cohort9.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort9 / 1000)
                                    End If
                                Case 10
                                    If Not valrec.Cohort10.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort10 / 1000)
                                    End If
                                Case 11
                                    If Not valrec.Cohort11.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort11 / 1000)
                                    End If
                                Case 12
                                    If Not valrec.Cohort12.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort12 / 1000)
                                    End If
                                Case 13
                                    If Not valrec.Cohort13.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort13 / 1000)
                                    End If
                                Case 14
                                    If Not valrec.Cohort14.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort14 / 1000)
                                    End If
                                Case 15
                                    If Not valrec.Cohort15.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort15 / 1000)
                                    End If
                                Case 16
                                    If Not valrec.Cohort16.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort16 / 1000)
                                    End If
                                Case 17
                                    If Not valrec.Cohort17.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort17 / 1000)
                                    End If
                                Case 18
                                    If Not valrec.Cohort18.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort18 / 1000)
                                    End If
                                Case 19
                                    If Not valrec.Cohort19.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort19 / 1000)
                                    End If
                                Case 20
                                    If Not valrec.Cohort20.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort20 / 1000)
                                    End If
                                Case 21
                                    If Not valrec.Cohort21.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort21 / 1000)
                                    End If
                                Case 22
                                    If Not valrec.Cohort22.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort22 / 1000)
                                    End If
                                Case 23
                                    If Not valrec.Cohort23.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort23 / 1000)
                                    End If
                                Case 24
                                    If Not valrec.Cohort24.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort24 / 1000)
                                    End If
                                Case 25
                                    If Not valrec.Cohort25.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort25 / 1000)
                                    End If
                                Case 26
                                    If Not valrec.Cohort26.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort26 / 1000)
                                    End If
                                Case 27
                                    If Not valrec.Cohort27.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort27 / 1000)
                                    End If
                                Case 28
                                    If Not valrec.Cohort28.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort28 / 1000)
                                    End If
                                Case 29
                                    If Not valrec.Cohort29.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort29 / 1000)
                                    End If
                                Case 30
                                    If Not valrec.Cohort30.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort30 / 1000)
                                    End If
                                Case 31
                                    If Not valrec.Cohort31.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort31 / 1000)
                                    End If
                                Case 32
                                    If Not valrec.Cohort32.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort32 / 1000)
                                    End If
                                Case 33
                                    If Not valrec.Cohort33.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort33 / 1000)
                                    End If
                                Case 34
                                    If Not valrec.Cohort34.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort34 / 1000)
                                    End If
                                Case 35
                                    If Not valrec.Cohort35.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)
                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort35 / 1000)
                                    End If
                                Case 36
                                    If Not valrec.Cohort36.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort36 / 1000)
                                    End If
                                Case 37
                                    If Not valrec.Cohort37.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort37 / 1000)
                                    End If
                                Case 38
                                    If Not valrec.Cohort38.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort38 / 1000)
                                    End If
                                Case 39
                                    If Not valrec.Cohort39.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort39 / 1000)
                                    End If
                                Case 40
                                    If Not valrec.Cohort40.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort40 / 1000)
                                    End If
                                Case 41
                                    If Not valrec.Cohort41.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, male, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort41 / 1000)
                                    End If
                            End Select
                        Next
                    Next valrec
                    'gvdb 2024/04/05
                    ' update for new EF version syntax
                    'Dim qr2 = (From p In db2.PopMortalityCohortRegionFemaleHist Select p).DistinctBy(Function(p) p.RegionName)
                    Dim qr2 = db2.PopMortalityCohortRegionFemaleHist.AsEnumerable().DistinctBy(Function(p) p.RegionName)
                    For Each valrec As PopMortalityCohortRegionFemaleHist In qr2 'db2.PopMortalityCohortRegionFemaleHist.OrderBy(Function(y) y.RegionId)
                        Dim rec = New Dictionary(Of String, Single?)
                        For j% = 0 To cntx.NCohorts
                            ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                            'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                            Select Case j
                                Case 0
                                    If Not valrec.Cohort0.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort0 / 1000)
                                    End If
                                Case 1
                                    If Not valrec.Cohort1.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort1 / 1000)
                                    End If
                                Case 2
                                    If Not valrec.Cohort2.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort2 / 1000)
                                    End If
                                Case 3
                                    If Not valrec.Cohort3.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort3 / 1000)
                                    End If
                                Case 4
                                    If Not valrec.Cohort4.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort4 / 1000)
                                    End If
                                Case 5
                                    If Not valrec.Cohort5.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort5 / 1000)
                                    End If
                                Case 6
                                    If Not valrec.Cohort6.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort6 / 1000)
                                    End If
                                Case 7
                                    If Not valrec.Cohort7.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort7 / 1000)
                                    End If
                                Case 8
                                    If Not valrec.Cohort8.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort8 / 1000)
                                    End If
                                Case 9
                                    If Not valrec.Cohort9.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort9 / 1000)
                                    End If
                                Case 10
                                    If Not valrec.Cohort10.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort10 / 1000)
                                    End If
                                Case 11
                                    If Not valrec.Cohort11.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort11 / 1000)
                                    End If
                                Case 12
                                    If Not valrec.Cohort12.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort12 / 1000)
                                    End If
                                Case 13
                                    If Not valrec.Cohort13.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort13 / 1000)
                                    End If
                                Case 14
                                    If Not valrec.Cohort14.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort14 / 1000)
                                    End If
                                Case 15
                                    If Not valrec.Cohort15.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort15 / 1000)
                                    End If
                                Case 16
                                    If Not valrec.Cohort16.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort16 / 1000)
                                    End If
                                Case 17
                                    If Not valrec.Cohort17.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort17 / 1000)
                                    End If
                                Case 18
                                    If Not valrec.Cohort18.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort18 / 1000)
                                    End If
                                Case 19
                                    If Not valrec.Cohort19.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort19 / 1000)
                                    End If
                                Case 20
                                    If Not valrec.Cohort20.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort20 / 1000)
                                    End If
                                Case 21
                                    If Not valrec.Cohort21.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort21 / 1000)
                                    End If
                                Case 22
                                    If Not valrec.Cohort22.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort22 / 1000)
                                    End If
                                Case 23
                                    If Not valrec.Cohort23.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort23 / 1000)
                                    End If
                                Case 24
                                    If Not valrec.Cohort24.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort24 / 1000)
                                    End If
                                Case 25
                                    If Not valrec.Cohort25.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort25 / 1000)
                                    End If
                                Case 26
                                    If Not valrec.Cohort26.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort26 / 1000)
                                    End If
                                Case 27
                                    If Not valrec.Cohort27.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort27 / 1000)
                                    End If
                                Case 28
                                    If Not valrec.Cohort28.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort28 / 1000)
                                    End If
                                Case 29
                                    If Not valrec.Cohort29.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort29 / 1000)
                                    End If
                                Case 30
                                    If Not valrec.Cohort30.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort30 / 1000)
                                    End If
                                Case 31
                                    If Not valrec.Cohort31.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort31 / 1000)
                                    End If
                                Case 32
                                    If Not valrec.Cohort32.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort32 / 1000)
                                    End If
                                Case 33
                                    If Not valrec.Cohort33.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort33 / 1000)
                                    End If
                                Case 34
                                    If Not valrec.Cohort34.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort34 / 1000)
                                    End If
                                Case 35
                                    If Not valrec.Cohort35.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort35 / 1000)
                                    End If
                                Case 36
                                    If Not valrec.Cohort36.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort36 / 1000)
                                    End If
                                Case 37
                                    If Not valrec.Cohort37.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort37 / 1000)
                                    End If
                                Case 38
                                    If Not valrec.Cohort38.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort38 / 1000)
                                    End If
                                Case 39
                                    If Not valrec.Cohort39.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort39 / 1000)
                                    End If
                                Case 40
                                    If Not valrec.Cohort40.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort40 / 1000)
                                    End If
                                Case 41
                                    If Not valrec.Cohort41.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort41 / 1000)
                                    End If
                            End Select
                        Next
                    Next valrec
                Else
                    '   Set valtbl = db.OpenRecordset("PopMortalityCohortRegionMale")
                    'gvdb 2024/04/05
                    ' update for new EF version syntax
                    'Dim qr = (From p In db2.PopMortalityCohortRegionMale Select p).DistinctBy(Function(p) p.RegionName)
                    Dim qr = db2.PopMortalityCohortRegionMale.AsEnumerable().DistinctBy(Function(p) p.RegionName)
                    For Each valrec As PopMortalityCohortRegionMale In qr ' db2.PopMortalityCohortRegionMale.Select(Of PopMortalityCohortRegionMale).Distinct().OrderBy(Function(y) y.RegionId)
                        Dim rec = New Dictionary(Of String, Single?)
                        For j% = 0 To cntx.NCohorts
                            ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                            'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                            Select Case j
                                Case 0
                                    If Not valrec.Cohort0.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort0 / 1000)
                                    End If
                                Case 1
                                    If Not valrec.Cohort1.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort1 / 1000)
                                    End If
                                Case 2
                                    If Not valrec.Cohort2.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort2 / 1000)
                                    End If
                                Case 3
                                    If Not valrec.Cohort3.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort3 / 1000)
                                    End If
                                Case 4
                                    If Not valrec.Cohort4.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort4 / 1000)
                                    End If
                                Case 5
                                    If Not valrec.Cohort5.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort5 / 1000)
                                    End If
                                Case 6
                                    If Not valrec.Cohort6.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort6 / 1000)
                                    End If
                                Case 7
                                    If Not valrec.Cohort7.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort7 / 1000)
                                    End If
                                Case 8
                                    If Not valrec.Cohort8.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort8 / 1000)
                                    End If
                                Case 9
                                    If Not valrec.Cohort9.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort9 / 1000)
                                    End If
                                Case 10
                                    If Not valrec.Cohort10.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort10 / 1000)
                                    End If
                                Case 11
                                    If Not valrec.Cohort11.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort11 / 1000)
                                    End If
                                Case 12
                                    If Not valrec.Cohort12.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort12 / 1000)
                                    End If
                                Case 13
                                    If Not valrec.Cohort13.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort13 / 1000)
                                    End If
                                Case 14
                                    If Not valrec.Cohort14.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort14 / 1000)
                                    End If
                                Case 15
                                    If Not valrec.Cohort15.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort15 / 1000)
                                    End If
                                Case 16
                                    If Not valrec.Cohort16.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort16 / 1000)
                                    End If
                                Case 17
                                    If Not valrec.Cohort17.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort17 / 1000)
                                    End If
                                Case 18
                                    If Not valrec.Cohort18.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort18 / 1000)
                                    End If
                                Case 19
                                    If Not valrec.Cohort19.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort19 / 1000)
                                    End If
                                Case 20
                                    If Not valrec.Cohort20.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort20 / 1000)
                                    End If
                                Case 21
                                    If Not valrec.Cohort21.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort21 / 1000)
                                    End If
                                Case 22
                                    If Not valrec.Cohort22.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort22 / 1000)
                                    End If
                                Case 23
                                    If Not valrec.Cohort23.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort23 / 1000)
                                    End If
                                Case 24
                                    If Not valrec.Cohort24.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort24 / 1000)
                                    End If
                                Case 25
                                    If Not valrec.Cohort25.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort25 / 1000)
                                    End If
                                Case 26
                                    If Not valrec.Cohort26.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort26 / 1000)
                                    End If
                                Case 27
                                    If Not valrec.Cohort27.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort27 / 1000)
                                    End If
                                Case 28
                                    If Not valrec.Cohort28.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort28 / 1000)
                                    End If
                                Case 29
                                    If Not valrec.Cohort29.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort29 / 1000)
                                    End If
                                Case 30
                                    If Not valrec.Cohort30.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort30 / 1000)
                                    End If
                                Case 31
                                    If Not valrec.Cohort31.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort31 / 1000)
                                    End If
                                Case 32
                                    If Not valrec.Cohort32.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort32 / 1000)
                                    End If
                                Case 33
                                    If Not valrec.Cohort33.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort33 / 1000)
                                    End If
                                Case 34
                                    If Not valrec.Cohort34.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort34 / 1000)
                                    End If
                                Case 35
                                    If Not valrec.Cohort35.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort35 / 1000)
                                    End If
                                Case 36
                                    If Not valrec.Cohort36.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort36 / 1000)
                                    End If
                                Case 37
                                    If Not valrec.Cohort37.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort37 / 1000)
                                    End If
                                Case 38
                                    If Not valrec.Cohort38.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort38 / 1000)
                                    End If
                                Case 39
                                    If Not valrec.Cohort39.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort39 / 1000)
                                    End If
                                Case 40
                                    If Not valrec.Cohort40.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort40 / 1000)
                                    End If
                                Case 41
                                    If Not valrec.Cohort41.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 1) = CSng(valrec.Cohort41 / 1000)
                                    End If
                            End Select
                        Next
                    Next valrec
                    'gvdb 2024/04/05
                    ' update for new EF version syntax
                    'Dim qr2 = (From p In db2.PopMortalityCohortRegionFemale Select p).DistinctBy(Function(p) p.RegionName)
                    Dim qr2 = db2.PopMortalityCohortRegionFemale.AsEnumerable().DistinctBy(Function(p) p.RegionName)
                    For Each valrec As PopMortalityCohortRegionFemale In qr2 'db2.PopMortalityCohortRegionFemale.OrderBy(Function(y) y.RegionId)
                        Dim rec = New Dictionary(Of String, Single?)
                        For j% = 0 To cntx.NCohorts
                            ' msg$ = "Fatal Error: BasMorDst, male, is null for " + RegName$(R%)
                            'valrec.GetType().GetProperty("Cohort" & j.ToString).GetValue(valrec, Nothing)
                            Select Case j
                                Case 0
                                    If Not valrec.Cohort0.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort0 / 1000)
                                    End If
                                Case 1
                                    If Not valrec.Cohort1.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort1 / 1000)
                                    End If
                                Case 2
                                    If Not valrec.Cohort2.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort2 / 1000)
                                    End If
                                Case 3
                                    If Not valrec.Cohort3.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort3 / 1000)
                                    End If
                                Case 4
                                    If Not valrec.Cohort4.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort4 / 1000)
                                    End If
                                Case 5
                                    If Not valrec.Cohort5.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort5 / 1000)
                                    End If
                                Case 6
                                    If Not valrec.Cohort6.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort6 / 1000)
                                    End If
                                Case 7
                                    If Not valrec.Cohort7.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort7 / 1000)
                                    End If
                                Case 8
                                    If Not valrec.Cohort8.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort8 / 1000)
                                    End If
                                Case 9
                                    If Not valrec.Cohort9.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort9 / 1000)
                                    End If
                                Case 10
                                    If Not valrec.Cohort10.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort10 / 1000)
                                    End If
                                Case 11
                                    If Not valrec.Cohort11.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort11 / 1000)
                                    End If
                                Case 12
                                    If Not valrec.Cohort12.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort12 / 1000)
                                    End If
                                Case 13
                                    If Not valrec.Cohort13.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort13 / 1000)
                                    End If
                                Case 14
                                    If Not valrec.Cohort14.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort14 / 1000)
                                    End If
                                Case 15
                                    If Not valrec.Cohort15.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort15 / 1000)
                                    End If
                                Case 16
                                    If Not valrec.Cohort16.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort16 / 1000)
                                    End If
                                Case 17
                                    If Not valrec.Cohort17.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort17 / 1000)
                                    End If
                                Case 18
                                    If Not valrec.Cohort18.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort18 / 1000)
                                    End If
                                Case 19
                                    If Not valrec.Cohort19.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort19 / 1000)
                                    End If
                                Case 20
                                    If Not valrec.Cohort20.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort20 / 1000)
                                    End If
                                Case 21
                                    If Not valrec.Cohort21.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort21 / 1000)
                                    End If
                                Case 22
                                    If Not valrec.Cohort22.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort22 / 1000)
                                    End If
                                Case 23
                                    If Not valrec.Cohort23.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort23 / 1000)
                                    End If
                                Case 24
                                    If Not valrec.Cohort24.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort24 / 1000)
                                    End If
                                Case 25
                                    If Not valrec.Cohort25.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort25 / 1000)
                                    End If
                                Case 26
                                    If Not valrec.Cohort26.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort26 / 1000)
                                    End If
                                Case 27
                                    If Not valrec.Cohort27.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort27 / 1000)
                                    End If
                                Case 28
                                    If Not valrec.Cohort28.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort28 / 1000)
                                    End If
                                Case 29
                                    If Not valrec.Cohort29.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort29 / 1000)
                                    End If
                                Case 30
                                    If Not valrec.Cohort30.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort30 / 1000)
                                    End If
                                Case 31
                                    If Not valrec.Cohort31.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort31 / 1000)
                                    End If
                                Case 32
                                    If Not valrec.Cohort32.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort32 / 1000)
                                    End If
                                Case 33
                                    If Not valrec.Cohort33.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort33 / 1000)
                                    End If
                                Case 34
                                    If Not valrec.Cohort34.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort34 / 1000)
                                    End If
                                Case 35
                                    If Not valrec.Cohort35.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort35 / 1000)
                                    End If
                                Case 36
                                    If Not valrec.Cohort36.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort36 / 1000)
                                    End If
                                Case 37
                                    If Not valrec.Cohort37.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort37 / 1000)
                                    End If
                                Case 38
                                    If Not valrec.Cohort38.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort38 / 1000)
                                    End If
                                Case 39
                                    If Not valrec.Cohort39.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort39 / 1000)
                                    End If
                                Case 40
                                    If Not valrec.Cohort40.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort40 / 1000)
                                    End If
                                Case 41
                                    If Not valrec.Cohort41.HasValue Then
                                        msg$ = "Fatal Error: BasMorDst, female, is null for " & valrec.RegionName & ",Cohort" & j%.ToString
                                        Debug.WriteLine(msg)

                                    Else
                                        BasMordst(j, valrec.RegionId, 2) = CSng(valrec.Cohort41 / 1000)
                                    End If
                            End Select
                        Next
                    Next valrec
                End If

            End Using
            'ttt: vb6: Region, vb.net:Country (Table Names)


        End Sub

        'jrs 2019/05/22
        Friend Sub RegDistributionIHMEMort(Of T As {New, ICohortRegion, CohortRegion})(VarCtry As IEnumerable(Of HealthDetailedDeathsCtry), cntx As IRebuildFacade, VarReg As Microsoft.EntityFrameworkCore.DbSet(Of T), SexId As Integer)
            Dim CPOP As IDictionary(Of String, Single?) = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
            Dim CohortDistrib As Dictionary(Of String, Dictionary(Of String, Double?)) = New Dictionary(Of String, Dictionary(Of String, Double?))
            '   Read Cohort Distribution Data - "COUNTRY" Data
            '   ----------------------------------------------
            Dim cohortval As Single?
            Dim CohortName As String
            Dim CohortIdx As Integer
            'jrs 2025/02/26
            Dim CohortsWData As Integer
            CohortsWData = VarCtry.First.Cohorts.Count

            For Each ctry As HealthDetailedDeathsCtry In VarCtry
                If ctry.sex = SexId Then
                    If Not CohortDistrib.ContainsKey(ctry.Country) Then
                        CohortDistrib.Add(ctry.Country, New Dictionary(Of String, Double?))
                        If ctry.Country = "Singapore" Then
                            CohortDistrib.Add("Hong Kong", New Dictionary(Of String, Double?))
                        ElseIf ctry.Country = "Serbia" Then
                            CohortDistrib.Add("Kosovo", New Dictionary(Of String, Double?))
                            'jrs 2024/03/13 Included starting now
                            'ElseIf ctry.Country = "Tonga" Then 'jrs 2023/04/14
                            '    CohortDistrib.Add("Kiribati", New Dictionary(Of String, Double?))
                        ElseIf ctry.Country = "Mali" Then 'jrs 2023/04/14
                            CohortDistrib.Add("Sahrawi Arab Democratic Republic", New Dictionary(Of String, Double?))
                        End If
                    End If

                    For Each cohort As KeyValuePair(Of String, Double) In ctry.Cohorts
                        cohortval = CSng(cohort.Value)
                        'jrs 2026/03/17 CohortName = cohort.Key.Replace("p", "Cohort")
                        CohortIdx = CInt(cohort.Key.Replace("p", ""))
                        'jrs 2026/03/17 need to start with Cohort0 for PopMortalityCohortRegion Tables
                        CohortName = "Cohort" & (CohortIdx - 1).ToString

                        If Not cohortval.HasValue Then
                            If CohortIdx = 5 Then
                                cohortval = 0.001
                            Else
                                cohortval = 0.0001
                            End If
                        End If

                        If Not CohortDistrib(ctry.Country).ContainsKey(CohortName) Then
                            CohortDistrib(ctry.Country).Add(CohortName, If(cohortval, 0.00001F))
                            If ctry.Country = "Singapore" Then
                                CohortDistrib("Hong Kong").Add(CohortName, If(cohortval, 0.00001F))
                            ElseIf ctry.Country = "Serbia" Then
                                CohortDistrib("Kosovo").Add(CohortName, If(cohortval, 0.00001F))
                                'jrs 2024/03/13
                                'ElseIf ctry.Country = "Tonga" Then 'jrs 2023/04/14
                                '    CohortDistrib("Kiribati").Add(CohortName, If(cohortval, 0.00001F))
                            ElseIf ctry.Country = "Mali" Then 'jrs 2023/04/14
                                CohortDistrib("Sahrawi Arab Democratic Republic").Add(CohortName, If(cohortval, 0.00001F))
                            End If
                        Else
                            CohortDistrib(ctry.Country)(CohortName) = CohortDistrib(ctry.Country)(CohortName) + If(cohortval, 0.00001F)
                            If ctry.Country = "Singapore" Then
                                CohortDistrib("Hong Kong")(CohortName) = CohortDistrib("Hong Kong")(CohortName) + If(cohortval, 0.00001F)
                            ElseIf ctry.Country = "Serbia" Then
                                CohortDistrib("Kosovo")(CohortName) = CohortDistrib("Kosovo")(CohortName) + If(cohortval, 0.00001F)
                                'jrs 2024/03/13
                                'ElseIf ctry.Country = "Tonga" Then 'jrs 2023/04/14
                                '    CohortDistrib("Kiribati")(CohortName) = CohortDistrib("Kiribati")(CohortName) + If(cohortval, 0.00001F)
                            ElseIf ctry.Country = "Mali" Then 'jrs 2023/04/14
                                CohortDistrib("Sahrawi Arab Democratic Republic")(CohortName) = CohortDistrib("Sahrawi Arab Democratic Republic")(CohortName) + If(cohortval, 0.00001F)
                            End If
                        End If
                    Next
                End If
            Next
            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim CoHold As Dictionary(Of String, Double?)
            Dim CoWeight As Dictionary(Of String, Double?)
            Dim CoValue As Dictionary(Of String, Double?)
            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                CoHold = New Dictionary(Of String, Double?)
                CoWeight = New Dictionary(Of String, Double?)
                CoValue = New Dictionary(Of String, Double?)
                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    For cocount% = 0 To cntx.NCohorts
                        CohortName = "Cohort" & cocount.ToString
                        If CoHold.ContainsKey(CohortName) Then
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold(CohortName) = CoHold(CohortName) + CohortDistrib(ctry.Key)(CohortName) * CPOP(ctry.Key)
                                CoWeight(CohortName) = CoWeight(CohortName) + CPOP(ctry.Key)
                            End If
                        Else
                            If CohortDistrib(ctry.Key).ContainsKey(CohortName) Then
                                CoHold.Add(CohortName, CohortDistrib(ctry.Key)(CohortName) * CPOP(ctry.Key))
                                CoWeight.Add(CohortName, CPOP(ctry.Key))
                            Else
                                CoHold.Add(CohortName, 0.001)
                                CoWeight.Add(CohortName, 1)
                            End If
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                'Dim row As CohortRegion = New CohortRegion ' New cannot be used on a class that is MustInherit
                'Dim row As PopAgeCohortRegionMale = New PopAgeCohortRegionMale
                Dim row As T = New T()
                row.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(regcount%))
                row.RegionId = regcount%

                For cocount% = 0 To cntx.NCohorts
                    CohortName = "Cohort" & cocount.ToString
                    If CoWeight(CohortName) > 0 Then
                        CoValue.Add(CohortName, CoHold(CohortName) / CoWeight(CohortName))
                    Else
                        CoValue.Add(CohortName, Nothing)
                    End If
                Next

                For cocount% = 0 To cntx.NCohorts
                    'jrs 2026/03/17 CohortName = "Cohort" & (cocount + 1).ToString
                    CohortName = "Cohort" & (cocount).ToString
                    If cocount% = cntx.NCohorts Then
                        'jrs 2012/09/03
                        Dim CohortNameM1 As String = "Cohort" & cocount.ToString
                        If CoValue(CohortNameM1) = 0 Then
                            'jrs 2026/03/17 CoValue.Add(CohortName, 0)
                            CoValue(CohortName) = 0
                        Else
                            Dim CohortNameM2 As String = "Cohort" & (cocount - 1).ToString
                            'jrs 2026/03/17 CoValue.Add(CohortName, Amin(CoValue(CohortNameM1) +
                            'AMAX(45, (CoValue(CohortNameM1) - CoValue(CohortNameM2))) * 10, 1000)) 'jrs 2009/05/26 adding limit of 1000
                            CoValue(CohortName) = Amin(CoValue(CohortNameM1) +
                            AMAX(45, (CoValue(CohortNameM1) - CoValue(CohortNameM2))) * 10, 1000)
                        End If
                    ElseIf cocount% > CohortsWData - 1 Then 'jrs 2025/02/26
                        Dim CohortNameM1 As String = "Cohort" & cocount.ToString
                        If CoValue(CohortNameM1) = 0 Then
                            CoValue(CohortName) = 0
                        Else
                            Dim CohortNameM2 As String = "Cohort" & (cocount - 1).ToString
                            CoValue(CohortName) = Amin(CoValue(CohortNameM1) +
                            AMAX(45, (CoValue(CohortNameM1) - CoValue(CohortNameM2))) * 10, 1000) 'jrs 2009/05/26 adding limit of 1000
                        End If
                    End If
                    Select Case cocount
                        Case 0
                            row.Cohort0 = CoValue(CohortName)
                        Case 1
                            row.Cohort1 = CoValue(CohortName)
                        Case 2
                            row.Cohort2 = CoValue(CohortName)
                        Case 3
                            row.Cohort3 = CoValue(CohortName)
                        Case 4
                            row.Cohort4 = CoValue(CohortName)
                        Case 5
                            row.Cohort5 = CoValue(CohortName)
                        Case 6
                            row.Cohort6 = CoValue(CohortName)
                        Case 7
                            row.Cohort7 = CoValue(CohortName)
                        Case 8
                            row.Cohort8 = CoValue(CohortName)
                        Case 9
                            row.Cohort9 = CoValue(CohortName)
                        Case 10
                            row.Cohort10 = CoValue(CohortName)
                        Case 11
                            row.Cohort11 = CoValue(CohortName)
                        Case 12
                            row.Cohort12 = CoValue(CohortName)
                        Case 13
                            row.Cohort13 = CoValue(CohortName)
                        Case 14
                            row.Cohort14 = CoValue(CohortName)
                        Case 15
                            row.Cohort15 = CoValue(CohortName)
                        Case 16
                            row.Cohort16 = CoValue(CohortName)
                        Case 17
                            row.Cohort17 = CoValue(CohortName)
                        Case 18
                            row.Cohort18 = CoValue(CohortName)
                        Case 19
                            row.Cohort19 = CoValue(CohortName)
                        Case 20
                            row.Cohort20 = CoValue(CohortName)
                        Case 21
                            row.Cohort21 = CoValue(CohortName)
                        Case 22
                            row.Cohort22 = CoValue(CohortName)
                        Case 23
                            row.Cohort23 = CoValue(CohortName)
                        Case 24
                            row.Cohort24 = CoValue(CohortName)
                        Case 25
                            row.Cohort25 = CoValue(CohortName)
                        Case 26
                            row.Cohort26 = CoValue(CohortName)
                        Case 27
                            row.Cohort27 = CoValue(CohortName)
                        Case 28
                            row.Cohort28 = CoValue(CohortName)
                        Case 29
                            row.Cohort29 = CoValue(CohortName)
                        Case 30
                            row.Cohort30 = CoValue(CohortName)
                        Case 31
                            row.Cohort31 = CoValue(CohortName)
                        Case 32
                            row.Cohort32 = CoValue(CohortName)
                        Case 33
                            row.Cohort33 = CoValue(CohortName)
                        Case 34
                            row.Cohort34 = CoValue(CohortName)
                        Case 35
                            row.Cohort35 = CoValue(CohortName)
                        Case 36
                            row.Cohort36 = CoValue(CohortName)
                        Case 37
                            row.Cohort37 = CoValue(CohortName)
                        Case 38
                            row.Cohort38 = CoValue(CohortName)
                        Case 39
                            row.Cohort39 = CoValue(CohortName)
                        Case 40
                            row.Cohort40 = CoValue(CohortName)
                        Case 41
                            row.Cohort41 = CoValue(CohortName)
                    End Select
                Next

                VarReg.Add(row)
            Next

        End Sub

        'mti 2020/09/01
        Friend Sub RegPopSumSingleYear(Of T As {New, IAgeSingleRegion, AgeSingleRegion})(VarCtry As IEnumerable(Of AgeSingleCountry), cntx As IRebuildFacade, VarReg As Microsoft.EntityFrameworkCore.DbSet(Of T), TotCohorts As Integer)
            Dim AgeSingleDistrib As Dictionary(Of String, Dictionary(Of String, Double?)) = New Dictionary(Of String, Dictionary(Of String, Double?))

            Dim ageval As Single?
            For Each ctry As AgeSingleCountry In VarCtry
                AgeSingleDistrib.Add(ctry.CountryName, New Dictionary(Of String, Double?))
                For Each age As KeyValuePair(Of String, Double) In ctry.Ages
                    ageval = CSng(age.Value)
                    If Not ageval.HasValue Then ageval = 0.001
                    AgeSingleDistrib(ctry.CountryName).Add(age.Key, CSng(AMAX(ageval, 0.001)))
                Next
            Next

            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim CoHoldSingle As Dictionary(Of String, Double?)
            Dim CoWeightSingle As Dictionary(Of String, Double?)
            Dim CoValue As Dictionary(Of String, Double?)
            Dim AgeName As String

            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                CoHoldSingle = New Dictionary(Of String, Double?)
                CoWeightSingle = New Dictionary(Of String, Double?)
                CoValue = New Dictionary(Of String, Double?)

                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    For cocount% = 0 To TotCohorts
                        AgeName = "Age" & cocount.ToString
                        If CoHoldSingle.ContainsKey(AgeName) Then
                            If AgeSingleDistrib(ctry.Key).ContainsKey(AgeName) Then
                                CoHoldSingle(AgeName) = CoHoldSingle(AgeName) + AgeSingleDistrib(ctry.Key)(AgeName)
                                CoWeightSingle(AgeName) = CoWeightSingle(AgeName) + 1
                            End If
                        Else
                            If AgeSingleDistrib(ctry.Key).ContainsKey(AgeName) Then
                                CoHoldSingle.Add(AgeName, AgeSingleDistrib(ctry.Key)(AgeName))
                                CoWeightSingle.Add(AgeName, 1)
                            Else
                                CoHoldSingle.Add(AgeName, 0.001)
                                CoWeightSingle.Add(AgeName, 1)
                            End If
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                'Dim row As CohortRegion = New CohortRegion ' New cannot be used on a class that is MustInherit
                'Dim row As PopMortalityCohortRegionMale = New PopMortalityCohortRegionMale
                Dim row As T = New T()
                row.RegionName = cntx.GetDimension(1).Buckets.Item(CShort(regcount%))
                row.RegionId = regcount%

                For cocount% = 0 To TotCohorts
                    AgeName = "Age" & cocount.ToString
                    If CoWeightSingle(AgeName) > 0 Then
                        CoValue.Add(AgeName, CoHoldSingle(AgeName) / CoWeightSingle(AgeName))
                    Else
                        CoValue.Add(AgeName, Nothing)
                    End If
                Next


                For cocount% = 0 To TotCohorts
                    AgeName = "Age" & cocount.ToString
                    Select Case cocount
                        Case 0
                            row.Age0 = CoValue(AgeName)
                        Case 1
                            row.Age1 = CoValue(AgeName)
                        Case 2
                            row.Age2 = CoValue(AgeName)
                        Case 3
                            row.Age3 = CoValue(AgeName)
                        Case 4
                            row.Age4 = CoValue(AgeName)
                        Case 5
                            row.Age5 = CoValue(AgeName)
                        Case 6
                            row.Age6 = CoValue(AgeName)
                        Case 7
                            row.Age7 = CoValue(AgeName)
                        Case 8
                            row.Age8 = CoValue(AgeName)
                        Case 9
                            row.Age9 = CoValue(AgeName)
                        Case 10
                            row.Age10 = CoValue(AgeName)
                        Case 11
                            row.Age11 = CoValue(AgeName)
                        Case 12
                            row.Age12 = CoValue(AgeName)
                        Case 13
                            row.Age13 = CoValue(AgeName)
                        Case 14
                            row.Age14 = CoValue(AgeName)
                        Case 15
                            row.Age15 = CoValue(AgeName)
                        Case 16
                            row.Age16 = CoValue(AgeName)
                        Case 17
                            row.Age17 = CoValue(AgeName)
                        Case 18
                            row.Age18 = CoValue(AgeName)
                        Case 19
                            row.Age19 = CoValue(AgeName)
                        Case 20
                            row.Age20 = CoValue(AgeName)
                        Case 21
                            row.Age21 = CoValue(AgeName)
                        Case 22
                            row.Age22 = CoValue(AgeName)
                        Case 23
                            row.Age23 = CoValue(AgeName)
                        Case 24
                            row.Age24 = CoValue(AgeName)
                        Case 25
                            row.Age25 = CoValue(AgeName)
                        Case 26
                            row.Age26 = CoValue(AgeName)
                        Case 27
                            row.Age27 = CoValue(AgeName)
                        Case 28
                            row.Age28 = CoValue(AgeName)
                        Case 29
                            row.Age29 = CoValue(AgeName)
                        Case 30
                            row.Age30 = CoValue(AgeName)
                        Case 31
                            row.Age31 = CoValue(AgeName)
                        Case 32
                            row.Age32 = CoValue(AgeName)
                        Case 33
                            row.Age33 = CoValue(AgeName)
                        Case 34
                            row.Age34 = CoValue(AgeName)
                        Case 35
                            row.Age35 = CoValue(AgeName)
                        Case 36
                            row.Age36 = CoValue(AgeName)
                        Case 37
                            row.Age37 = CoValue(AgeName)
                        Case 38
                            row.Age38 = CoValue(AgeName)
                        Case 39
                            row.Age39 = CoValue(AgeName)
                        Case 40
                            row.Age40 = CoValue(AgeName)
                        Case 41
                            row.Age41 = CoValue(AgeName)
                        Case 42
                            row.Age42 = CoValue(AgeName)
                        Case 43
                            row.Age43 = CoValue(AgeName)
                        Case 44
                            row.Age44 = CoValue(AgeName)
                        Case 45
                            row.Age45 = CoValue(AgeName)
                        Case 46
                            row.Age46 = CoValue(AgeName)
                        Case 47
                            row.Age47 = CoValue(AgeName)
                        Case 48
                            row.Age48 = CoValue(AgeName)
                        Case 49
                            row.Age49 = CoValue(AgeName)
                        Case 50
                            row.Age50 = CoValue(AgeName)
                        Case 51
                            row.Age51 = CoValue(AgeName)
                        Case 52
                            row.Age52 = CoValue(AgeName)
                        Case 53
                            row.Age53 = CoValue(AgeName)
                        Case 54
                            row.Age54 = CoValue(AgeName)
                        Case 55
                            row.Age55 = CoValue(AgeName)
                        Case 56
                            row.Age56 = CoValue(AgeName)
                        Case 57
                            row.Age57 = CoValue(AgeName)
                        Case 58
                            row.Age58 = CoValue(AgeName)
                        Case 59
                            row.Age59 = CoValue(AgeName)
                        Case 60
                            row.Age60 = CoValue(AgeName)
                        Case 61
                            row.Age61 = CoValue(AgeName)
                        Case 62
                            row.Age62 = CoValue(AgeName)
                        Case 63
                            row.Age63 = CoValue(AgeName)
                        Case 64
                            row.Age64 = CoValue(AgeName)
                        Case 65
                            row.Age65 = CoValue(AgeName)
                        Case 66
                            row.Age66 = CoValue(AgeName)
                        Case 67
                            row.Age67 = CoValue(AgeName)
                        Case 68
                            row.Age68 = CoValue(AgeName)
                        Case 69
                            row.Age69 = CoValue(AgeName)
                        Case 70
                            row.Age70 = CoValue(AgeName)
                        Case 71
                            row.Age71 = CoValue(AgeName)
                        Case 72
                            row.Age72 = CoValue(AgeName)
                        Case 73
                            row.Age73 = CoValue(AgeName)
                        Case 74
                            row.Age74 = CoValue(AgeName)
                        Case 75
                            row.Age75 = CoValue(AgeName)
                        Case 76
                            row.Age76 = CoValue(AgeName)
                        Case 77
                            row.Age77 = CoValue(AgeName)
                        Case 78
                            row.Age78 = CoValue(AgeName)
                        Case 79
                            row.Age79 = CoValue(AgeName)
                        Case 80
                            row.Age80 = CoValue(AgeName)
                        Case 81
                            row.Age81 = CoValue(AgeName)
                        Case 82
                            row.Age82 = CoValue(AgeName)
                        Case 83
                            row.Age83 = CoValue(AgeName)
                        Case 84
                            row.Age84 = CoValue(AgeName)
                        Case 85
                            row.Age85 = CoValue(AgeName)
                        Case 86
                            row.Age86 = CoValue(AgeName)
                        Case 87
                            row.Age87 = CoValue(AgeName)
                        Case 88
                            row.Age88 = CoValue(AgeName)
                        Case 89
                            row.Age89 = CoValue(AgeName)
                        Case 90
                            row.Age90 = CoValue(AgeName)
                        Case 91
                            row.Age91 = CoValue(AgeName)
                        Case 92
                            row.Age92 = CoValue(AgeName)
                        Case 93
                            row.Age93 = CoValue(AgeName)
                        Case 94
                            row.Age94 = CoValue(AgeName)
                        Case 95
                            row.Age95 = CoValue(AgeName)
                        Case 96
                            row.Age96 = CoValue(AgeName)
                        Case 97
                            row.Age97 = CoValue(AgeName)
                        Case 98
                            row.Age98 = CoValue(AgeName)
                        Case 99
                            row.Age99 = CoValue(AgeName)
                        Case 100
                            row.Age100 = CoValue(AgeName)
                    End Select
                Next

                VarReg.Add(row)
            Next


        End Sub

        'mti 2020/09/01
        Friend Sub ReadPopAgeSingleDistCtry(ByRef cagedstM As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef cagedstF As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef cntx As IRebuildFacade, ByRef db As IFsContext)
            Dim Singleval As Single?
            If cntx.HistoricMode Then
                'Dim ctrys As List(Of PopAgeSingleCountryMaleHist) = db.PopAgeSingleCountryMaleHist.ToList
                'For Each ctry As PopAgeSingleCountryMaleHist In db.PopAgeSingleCountryMaleHist
                For Each ctry As PopAgeSingleCountryMale In db.PopAgeSingleCountryMale
                    cagedstM.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each age As KeyValuePair(Of String, Double) In ctry.Ages
                        Singleval = CSng(age.Value)
                        If Not Singleval.HasValue Then Singleval = 0.001
                        cagedstM(ctry.CountryName).Add(age.Key, CSng(Math.Round(CSng(Singleval / 1000.0), 6)))
                    Next
                Next

                'Dim ctrysF As List(Of PopAgeSingleCountryFemaleHist) = db.PopAgeSingleCountryFemaleHist.ToList
                'For Each ctry As PopAgeSingleCountryFemaleHist In db.PopAgeSingleCountryFemaleHist
                For Each ctry As PopAgeSingleCountryFemale In db.PopAgeSingleCountryFemale
                    cagedstF.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each age As KeyValuePair(Of String, Double) In ctry.Ages
                        Singleval = CSng(age.Value)
                        If Not Singleval.HasValue Then Singleval = 0.001
                        cagedstF(ctry.CountryName).Add(age.Key, CSng(Math.Round(CSng(Singleval / 1000.0), 6)))
                    Next
                Next
            Else
                'Dim ctrys As List(Of PopAgeSingleCountryMale) = db.PopAgeSingleCountryMale.ToList
                For Each ctry As PopAgeSingleCountryMale In db.PopAgeSingleCountryMale
                    cagedstM.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each age As KeyValuePair(Of String, Double) In ctry.Ages
                        'j = j + 1
                        Singleval = CSng(age.Value)
                        If Not Singleval.HasValue Then Singleval = 0.001
                        cagedstM(ctry.CountryName).Add(age.Key, CSng(Math.Round(CSng(Singleval / 1000.0), 6)))
                    Next
                Next

                'Dim ctrysF As List(Of PopAgeSingleCountryFemale) = db.PopAgeSingleCountryFemale.ToList
                For Each ctry As PopAgeSingleCountryFemale In db.PopAgeSingleCountryFemale
                    cagedstF.Add(ctry.CountryName, New Dictionary(Of String, Single?))
                    For Each age As KeyValuePair(Of String, Double) In ctry.Ages
                        Singleval = CSng(age.Value)
                        If Not Singleval.HasValue Then Singleval = 0.001
                        cagedstF(ctry.CountryName).Add(age.Key, CSng(Math.Round(CSng(Singleval / 1000.0), 6)))
                    Next
                Next
            End If
        End Sub

        Friend Function LReg(valtbl As IDictionary(Of String, Single?()), yrs As Short(), drivertblname As String, Country As String,
                                ByRef FuncName As String, IFsYear As Short, cntx As IRebuildFacade,
    Optional MaxV As Double? = Nothing, Optional MinV? As Double = Nothing, Optional Conversion$ = "",
    Optional ConversionNumber As Double? = Nothing, Optional ExtrapolationRange As Short = 7) As Single?
            'mti November 20, 2011 - last Optional parameter added and code changed for that parameter

            'mti July 2006  - whole proc; maxv minv; check
            'mti June 2006 fix LREG for History  -whole proc
            'mti Nov 2004 - added for MDG consistency - new proc

			'gvdb 2024/04/05
            ' unused variable
            Dim MDGBaseYear As String ', Baseval As Single?

            Dim reg As New RegressionObject
            Dim X#, Y#
            'gvdb 2024/04/05
            ' unused variable
            Dim pt%, M% ' k%,
            Dim YearVal%, YearRangeStart%


            MDGBaseYear = "1990"
            If cntx.HistoricMode Then
                YearRangeStart% = (IFsYear - 5) 'to include 1950 data in EDYRSAg25
            Else
                YearRangeStart% = CInt(MDGBaseYear)
            End If

            'jrs 2008/07/16
            Dim iYr1 As Integer, iYr2 As Integer
            iYr1 = 0
            iYr2 = 0

            Dim valtblIdx As Integer = 0, VarVal As Single?, onlyyear As Integer, onlyval As Single?
            With reg
                .Init()
                .Degree = 1 'Degree
                pt = 0
                For j = 0 To yrs.Count - 1
                    YearVal = yrs(j)
                    Select Case YearVal
                        Case Is > 0 'field is an year
                            VarVal = valtbl(Country)(j)
                        Case Else
                            VarVal = Nothing
                    End Select
                    If VarVal.HasValue Then
                        'regression with data after 1985 only
                        'If YearVal >= Val(MDGBaseYear) - 5 Then
                        If YearVal >= YearRangeStart% - 5 Then
                            X# = YearVal
                            Y# = CDbl(VarVal)  '1
                            .XYAdd(X#, Y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                            onlyyear = YearVal
                            onlyval = VarVal 'onlyval will be most recent value found
                            pt = pt + 1
                            'jrs 2008/07/16
                            If iYr1 = 0 Then
                                iYr1 = YearVal
                            ElseIf YearVal > iYr2 Then
                                iYr2 = YearVal
                            End If
                        End If
                    End If
                Next j

                'jrs 2008/07/16
                'Check extrapolation base period
                'I'm not checking interpolation
                Dim bValidBasePeriod As Boolean
                bValidBasePeriod = True
                If pt > 1 Then
                    Dim BasePeriod As Integer
                    BasePeriod = 2 * (iYr2 - iYr1)

                    'mti November 20, 2011 - start
                    'BasePeriod = Amin(BasePeriod, 7)
                    BasePeriod = Amin(BasePeriod, ExtrapolationRange)
                    'mti November 20, 2011 - end

                    If IFsYear > iYr2 Then
                        If IFsYear - iYr2 > BasePeriod Then
                            bValidBasePeriod = False
                        End If
                    ElseIf IFsYear < iYr1 Then
                        If iYr1 - IFsYear > BasePeriod Then
                            bValidBasePeriod = False
                        End If
                    End If
                End If

                Dim ShiftVal As Single

                If Not cntx.HistoricMode Then
                    'not history/ current run
                    Dim SingleVal As Boolean, DrvVal As Single?
                    SingleVal = False
                    If pt = 1 Then
                        SingleVal = True
                    End If

                    Dim MDGBaseVal As Single?
                    Dim IFsYearVal As Single?

                    If pt < 2 Then
                        'obtain (from data table) or estimate 1990 and mostrecent value
                        'then run regression with those two values
                        'for pre-proc - find the year 2000 value from that regression; (to match MDG)

                        .Init()  'initialize to avoid double inclusion of 1990 value

                        Dim years As Short() = cntx.GetYears(drivertblname).OrderByDescending(Function(yy) yy).ToArray()
                        Dim tbldriver As IDictionary(Of String, Single?()) = cntx.GetDice(drivertblname, years)

                        'DrvVal = tblDriver.Fields(DrvFld$)  'GDP of that particular year
                        For fld% = 0 To years.Count - 1
                            If years(fld%) = CInt(MDGBaseYear) Then
                                DrvVal = tbldriver(Country)(fld)
                                Exit For
                            End If
                        Next


                        Dim DrvFld As Integer
                        DrvFld = CInt(MDGBaseYear$)
                        If Not DrvVal.HasValue Then
                            DrvFld = CInt(MDGBaseYear) + 1 'go one more year, 1991
                            'DrvVal = tblDriver.Fields(DrvFld$)
                            For fld% = 0 To years.Count - 1
                                If years(fld%) = DrvFld Then
                                    DrvVal = tbldriver(Country)(fld)
                                    Exit For
                                End If
                            Next

                        End If

                        If Not DrvVal.HasValue Then
                            DrvFld = DrvFld + 2 'go two more year, 1992
                            'DrvVal = tblDriver.Fields(DrvFld$)
                            For fld% = 0 To years.Count - 1
                                If years(fld%) = DrvFld Then
                                    DrvVal = tbldriver(Country)(fld%)
                                    Exit For
                                End If
                            Next

                        End If


                        If Not DrvVal.HasValue Then
                            DrvFld = CInt(MDGBaseYear$)
                            DrvFld = DrvFld - 1 'go back one year, 1989
                            'DrvVal = tblDriver.Fields(DrvFld$)
                            For fld% = 0 To years.Count - 1
                                If years(fld%) = DrvFld Then
                                    DrvVal = tbldriver(Country)(fld)
                                    Exit For
                                End If
                            Next

                        End If

                        If DrvVal.HasValue Then
                            If Conversion = "Div" Then
                                DrvVal = CSng(DrvVal / ConversionNumber)
                            End If
                            'use MDG database for tblfunction name
                            'Call XYTABL("GDP/Capita (PPP) Versus Income Below One Dollar per Day (2000)", GDPval, grCntryBaseVal(k))
                            Dim Estimate As SeriesEstimator
                            Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                            DrvVals.Add("DrvVal", DrvVal)
                            Estimate = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                            MDGBaseVal = Estimate("DrvVal")

                            'mti July 2006
                            If MDGBaseVal.HasValue And MaxV.HasValue And MinV.HasValue Then
                                MDGBaseVal = ApplyLimits(MDGBaseVal, CSng(MinV), CSng(MaxV))
                            End If

                            If MDGBaseVal.HasValue Then
                                X# = CDbl(DrvFld)
                                Y# = CDbl(MDGBaseVal)
                                .XYAdd(X#, Y#)
                                pt = pt + 1
                            End If

                        End If

                        'DrvFld$ = "MostRecent"
                        For M = 0 To years.Count - 1
                            YearVal = years(M)
                            If YearVal = IFsYear Then
                                DrvFld = IFsYear
                                Exit For
                            End If
                        Next M
                        'jrs 2018/12/05 look for MR
                        If M = years.Count Then
                            For M = 0 To years.Count - 1
                                If tbldriver(Country)(M).HasValue Then Exit For
                            Next
                            If M = years.Count Then M = 0
                        End If

                        DrvVal = tbldriver(Country)(M)  'GDP of that particular year

                        'DrvVal = DrvVal / 1000
                        If Conversion = "Div" And DrvVal.HasValue Then
                            DrvVal = CSng(DrvVal / ConversionNumber)
                        End If


                        If DrvVal.HasValue Then

                            'Call XYTABL("GDP/Capita (PPP) Versus Income Below One Dollar per Day (2000)", GDPval, grCntryLYVal(k))
                            Dim Estimate As SeriesEstimator
                            Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                            DrvVals.Add("DrvVal", DrvVal)
                            Estimate = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                            IFsYearVal = Estimate("DrvVal")

                            If IFsYearVal.HasValue And MaxV.HasValue And MinV.HasValue Then
                                IFsYearVal = ApplyLimits(IFsYearVal, CSng(MinV), CSng(MaxV))
                            End If

                            If IFsYearVal.HasValue Then
                                X# = CDbl(IFsYear)
                                Y# = CDbl(IFsYearVal)
                                .XYAdd(X#, Y#)
                                pt = pt + 1
                            End If


                        End If

                        tbldriver = Nothing

                        FuncName$ = "Estimate"

                    End If 'pt<2

                    ShiftVal = 0
                    '---------if one point exists priviledge data----------
                    If SingleVal And MDGBaseVal.HasValue And IFsYearVal.HasValue Then

                        'the singleval (or onlyval) is not going into regression? since, initialization of regression object done if pt<2
                        'shouldn't it, esp. if it is not from 1990 or mostrecent year?
                        'no..doesn't matter with shiftval logic (onlyval coming from years between 1985 and 2000/mostrecent)
                        FuncName$ = "Data"
                        X# = CDbl(onlyyear)
                        Y# = .RegVal(CDbl(X#))
                        ShiftVal = CSng(onlyval - Y#) '.RegVal(CDbl(X#))

                    End If

                End If 'current year

                'jrs 2008/07/24 added And bValidBasePeriod
                If pt >= 2 And bValidBasePeriod Then 'regression

                    'mti November 20, 2011 - start
                    FuncName$ = "Regression"
                    'mti November 20, 2011 -end

                    X# = CDbl(IFsYear)
                    Y# = reg.RegVal(CDbl(X#))
                    LReg = CSng(Y# + ShiftVal)
                Else
                    LReg = Nothing
                End If

            End With
        End Function

        'jrs 2017/04/24 *** new procedure to create a fit line to estimate a given year ***
        'Linear Regression based in LReg code created by Mohammod
        Friend Sub LinReg(ByRef valtbl As IDictionary(Of String, Single?()), ByRef XYears As Short(), ByVal sCtry As String, ByRef A As Single, ByRef B As Single)
            '1. valtbl is the table that contains the historic data
            '2. XYears is the number of years to use in the regression
            '3. a in y = a+b*(x)
            '4. b in y = a+b*(x)

			'gvdb 2024/04/05
            ' unused variable
            'Dim j As Integer,
            Dim YearVal As Integer
            Dim VarVal As Single?
            Dim X As Double, Y As Double
            Dim reg As New RegressionObject
            'gvdb 2024/04/05
            ' unused variable
            'Dim i As Integer
            Dim pt As Integer

            With reg
                .Init()
                .Degree = 1 'Degree
                pt = 0
                Dim yrIdx As Integer = 0

                For Each yr In XYears
                    YearVal = yr
                    VarVal = valtbl(sCtry)(yrIdx)
                    yrIdx = yrIdx + 1
                    If VarVal.HasValue Then
                        X# = CDbl(YearVal)
                        Y# = CDbl(VarVal)
                        .XYAdd(X#, Y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                        pt = pt + 1
                    End If
                Next

                A = -9999
                B = -9999
                If pt > 1 Then
                    A = CSng(reg.Coeff(0))
                    B = CSng(reg.Coeff(1))
                End If
            End With
        End Sub

        Friend Sub LinReg(ByRef valtbl As IDictionary(Of String, Single?()), ByRef XYears As Short(), ByVal sCtry As String, ByRef A As Double, ByRef B As Double)
            '1. valtbl is the table that contains the historic data
            '2. XYears is the number of years to use in the regression
            '3. a in y = a+b*(x)
            '4. b in y = a+b*(x)

			'gvdb 2024/04/05
            ' unused variable
            'Dim j As Integer,
            Dim YearVal As Integer
            Dim VarVal As Single?
            Dim X As Double, Y As Double
            Dim reg As New RegressionObject
            'gvdb 2024/04/05
            ' unused variable
            'Dim i As Integer
            Dim pt As Integer

            With reg
                .Init()
                .Degree = 1 'Degree
                pt = 0
                Dim yrIdx As Integer = 0

                For Each yr In XYears
                    YearVal = yr
                    VarVal = valtbl(sCtry)(yrIdx)
                    yrIdx = yrIdx + 1
                    If VarVal.HasValue Then
                        X# = CDbl(YearVal)
                        Y# = CDbl(VarVal.ToString)
                        .XYAdd(X#, Y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                        pt = pt + 1
                    End If
                Next

                A = -9999
                B = -9999
                If pt > 1 Then
                    A = reg.Coeff(0)
                    B = reg.Coeff(1)
                End If
            End With
        End Sub

        'Logarithmic Regression based in LReg code
        Public Sub LogRegRun(XArr() As Single, YArr() As Single, ByRef A As Single, ByRef B As Single)
            '1. XArr is X variable for all countries
            '2.  YArr is Y variable for all countries
            '3. a in y = a+b*ln(x)
            '4. b in y = a+b*ln(x)

            'mti 2015/05/13 - new proc
			
			'gvdb 2024/04/05
            ' unused variable
            Dim j As Integer ', YearVal As Integer
            'Dim VarVal As Single
            Dim X As Double, Y As Double
            Dim reg As New RegressionObject

            'valtbl.MoveFirst
            With reg
                .Init()
                .Degree = 1 'Degree
                For j = 1 To NR%
                    X# = Log(CDbl(XArr(j)))
                    Y# = CDbl(YArr(j))
                    .XYAdd(X#, Y#) 'Ln(rr%), Format(PlotLong(rr%, 1), "0.000")
                Next

                A = -9999
                B = -9999
                A = CSng(reg.Coeff(0))
                B = CSng(reg.Coeff(1))
            End With
        End Sub

        'jrs 2017/04/24
        'Function to Estimate value for a given year in Empirical History
        'look for data in XYrs in the past to build a fit line and use it to estimate
        Friend Function estimateWLineFitXyrs(ByRef valtbl As IDictionary(Of String, Single?()), ByVal iYear As Integer,
        ByVal XYrs As Short(), ByVal sCtry As String) As Single?
            '1. valtbl is the table that contains the historic data
            '3. iYear is the year to be estimated YYYY
            '4. XYrs is the number of years to go back and use to fit the line
            estimateWLineFitXyrs = Nothing

            Dim A As Single, B As Single
            Call LinReg(valtbl, XYrs, sCtry, A, B)
            If A <> -9999 And B <> -9999 Then
                'jrs 2020/03/09 impossilbe to get 3 decimals for AGXCropValueTrade and Zimbabwe
                'weird that the immediate window thos get them
                'jrs 2020/03/24 same problem with AGXCropQuantTrade, Cuba, but this time, I think I got it with CDbl(iYear)
                estimateWLineFitXyrs = CSng(A + B * CDbl(iYear))
                If estimateWLineFitXyrs < 1000 Then
                    'Redo for better precision
                    estimateWLineFitXyrs = CDec(A) + CDec(B * iYear)
                End If
            End If
        End Function

        'jrs 2011/06/10 *** Procedure to regionalize full tables ***
        '- Procedure assumes target table is stored in ifs.mdb with the same name
        'removing Country and FIPS field and inserting RegionId instead
        '- Procedure assumes Tbl has an Index on Country, and it has been set to order the table
        Friend Sub RegionalizeFullTableSUM(VarCtry As IDictionary(Of String, Single?()), YrsCtry As Short(), cntx As IRebuildFacade, VarReg As Microsoft.EntityFrameworkCore.DbSet(Of SeriesRegGDP2011))

            '   Compute Regional Distribution Data
            '   ----------------------------------
            Dim RegVals As Dictionary(Of Short, Single?)
            For regcount% = 1 To cntx.GetDimension(1).Buckets.Count
                RegVals = New Dictionary(Of Short, Single?)
                For Each ctry As KeyValuePair(Of String, Integer) In (From r In cntx.Regionalization Where r.Value = regcount%)
                    For yrcount% = 0 To YrsCtry.Count - 1
                        If RegVals.ContainsKey(YrsCtry(yrcount)) Then
                            RegVals(YrsCtry(yrcount)) = RegVals(YrsCtry(yrcount)) + VarCtry(ctry.Key)(yrcount%)
                        Else
                            RegVals.Add(YrsCtry(yrcount), VarCtry(ctry.Key)(yrcount%))
                        End If
                    Next
                Next

                '   Put Into Cohort Field of Region Record
                '   --------------------------------------------------------------------------
                Dim row As SeriesRegGDP2011 = New SeriesRegGDP2011
                row.RegionId = regcount%

                For yrcount% = 0 To YrsCtry.Count - 1
                    Select Case YrsCtry(yrcount)
                        Case 1960
                            row.Val1960 = RegVals(YrsCtry(yrcount))
                        Case 1961
                            row.Val1961 = RegVals(YrsCtry(yrcount))
                        Case 1962
                            row.Val1962 = RegVals(YrsCtry(yrcount))
                        Case 1963
                            row.Val1963 = RegVals(YrsCtry(yrcount))
                        Case 1964
                            row.Val1964 = RegVals(YrsCtry(yrcount))
                        Case 1965
                            row.Val1965 = RegVals(YrsCtry(yrcount))
                        Case 1966
                            row.Val1966 = RegVals(YrsCtry(yrcount))
                        Case 1967
                            row.Val1967 = RegVals(YrsCtry(yrcount))
                        Case 1968
                            row.Val1968 = RegVals(YrsCtry(yrcount))
                        Case 1969
                            row.Val1969 = RegVals(YrsCtry(yrcount))
                        Case 1970
                            row.Val1970 = RegVals(YrsCtry(yrcount))
                        Case 1971
                            row.Val1971 = RegVals(YrsCtry(yrcount))
                        Case 1972
                            row.Val1972 = RegVals(YrsCtry(yrcount))
                        Case 1973
                            row.Val1973 = RegVals(YrsCtry(yrcount))
                        Case 1974
                            row.Val1974 = RegVals(YrsCtry(yrcount))
                        Case 1975
                            row.Val1975 = RegVals(YrsCtry(yrcount))
                        Case 1976
                            row.Val1976 = RegVals(YrsCtry(yrcount))
                        Case 1977
                            row.Val1977 = RegVals(YrsCtry(yrcount))
                        Case 1978
                            row.Val1978 = RegVals(YrsCtry(yrcount))
                        Case 1979
                            row.Val1979 = RegVals(YrsCtry(yrcount))
                        Case 1980
                            row.Val1980 = RegVals(YrsCtry(yrcount))
                        Case 1981
                            row.Val1981 = RegVals(YrsCtry(yrcount))
                        Case 1982
                            row.Val1982 = RegVals(YrsCtry(yrcount))
                        Case 1983
                            row.Val1983 = RegVals(YrsCtry(yrcount))
                        Case 1984
                            row.Val1984 = RegVals(YrsCtry(yrcount))
                        Case 1985
                            row.Val1985 = RegVals(YrsCtry(yrcount))
                        Case 1986
                            row.Val1986 = RegVals(YrsCtry(yrcount))
                        Case 1987
                            row.Val1987 = RegVals(YrsCtry(yrcount))
                        Case 1988
                            row.Val1988 = RegVals(YrsCtry(yrcount))
                        Case 1989
                            row.Val1989 = RegVals(YrsCtry(yrcount))
                        Case 1990
                            row.Val1990 = RegVals(YrsCtry(yrcount))
                        Case 1991
                            row.Val1991 = RegVals(YrsCtry(yrcount))
                        Case 1992
                            row.Val1992 = RegVals(YrsCtry(yrcount))
                        Case 1993
                            row.Val1993 = RegVals(YrsCtry(yrcount))
                        Case 1994
                            row.Val1994 = RegVals(YrsCtry(yrcount))
                        Case 1995
                            row.Val1995 = RegVals(YrsCtry(yrcount))
                        Case 1996
                            row.Val1996 = RegVals(YrsCtry(yrcount))
                        Case 1997
                            row.Val1997 = RegVals(YrsCtry(yrcount))
                        Case 1998
                            row.Val1998 = RegVals(YrsCtry(yrcount))
                        Case 1999
                            row.Val1999 = RegVals(YrsCtry(yrcount))
                        Case 2000
                            row.Val2000 = RegVals(YrsCtry(yrcount))
                        Case 2001
                            row.Val2001 = RegVals(YrsCtry(yrcount))
                        Case 2002
                            row.Val2002 = RegVals(YrsCtry(yrcount))
                        Case 2003
                            row.Val2003 = RegVals(YrsCtry(yrcount))
                        Case 2004
                            row.Val2004 = RegVals(YrsCtry(yrcount))
                        Case 2005
                            row.Val2005 = RegVals(YrsCtry(yrcount))
                        Case 2006
                            row.Val2006 = RegVals(YrsCtry(yrcount))
                        Case 2007
                            row.Val2007 = RegVals(YrsCtry(yrcount))
                        Case 2008
                            row.Val2008 = RegVals(YrsCtry(yrcount))
                        Case 2009
                            row.Val2009 = RegVals(YrsCtry(yrcount))
                        Case 2010
                            row.Val2010 = RegVals(YrsCtry(yrcount))
                        Case 2011
                            row.Val2011 = RegVals(YrsCtry(yrcount))
                        Case 2012
                            row.Val2012 = RegVals(YrsCtry(yrcount))
                        Case 2013
                            row.Val2013 = RegVals(YrsCtry(yrcount))
                        Case 2014
                            row.Val2014 = RegVals(YrsCtry(yrcount))
                        Case 2015
                            row.Val2015 = RegVals(YrsCtry(yrcount))
                        Case 2016
                            row.Val2016 = RegVals(YrsCtry(yrcount))
                        Case 2017
                            row.Val2017 = RegVals(YrsCtry(yrcount))
                        Case 2018
                            row.Val2018 = RegVals(YrsCtry(yrcount))
                        Case 2019
                            row.Val2019 = RegVals(YrsCtry(yrcount))
                        Case 2020
                            row.Val2020 = RegVals(YrsCtry(yrcount))
                        Case 2021
                            row.Val2021 = RegVals(YrsCtry(yrcount))
                        Case 2022
                            row.Val2022 = RegVals(YrsCtry(yrcount))
                    End Select
                    If Not row.Earliest.HasValue Then
                        row.Earliest = RegVals(YrsCtry(yrcount))
                    End If
                    If RegVals(YrsCtry(yrcount)).HasValue Then
                        row.MostRecent = RegVals(YrsCtry(yrcount))
                    End If
                Next
                VarReg.Add(row)
            Next


        End Sub

        'jrs 2024/06/06 added sYrConstantDollars
        'jrs 2006/12/14
        'function to check for function name, looks for a given year if not found changes the name to the most recent year
        Friend Function getFunctionNameYrOrMstRct(sFuncName As String, iYr As Integer, cntx As IContext, Optional sYrConstantDollars As String = "") As String
            'If cntx.GetRegression(sFuncName).Count > 0 Then
            If RegCoeffs.ContainsKey(sFuncName) Then
                'jrs 2026/03/25 
                Debug.WriteLine("Function found: " & sFuncName)
                Return sFuncName
            Else
                'jrs 2024/06/06
                Dim PastCstYrIdx As Integer = 0
                Dim PrevConstantYear As Integer = ConstantDollarsYear

                Do While True
                    Dim sFuncNameSearch As String, iYrSearch As Integer
                    'searching forward
                    iYrSearch = iYr + 1
                    Do While iYrSearch <= DateTime.Now.Year
                        sFuncNameSearch = sFuncName.Replace("(" & iYr.ToString & ")", "(" & iYrSearch.ToString & ")")
                        'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                        If RegCoeffs.ContainsKey(sFuncNameSearch) Then
                            'jrs 2026/03/25 
                            Debug.WriteLine("Function found: " & sFuncNameSearch)

                            Return sFuncNameSearch
                        End If
                        iYrSearch = iYrSearch + 1
                    Loop

                    'jrs 2026/09/18 added () in case PPP year is the same as iYr
                    sFuncNameSearch = sFuncName.Replace("(" & iYr.ToString & ")", "(MostRecent)")
                    'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                    If RegCoeffs.ContainsKey(sFuncNameSearch) Then
                        'jrs 2026/03/25 
                        Debug.WriteLine("Function found: " & sFuncNameSearch)

                        Return sFuncNameSearch
                    End If
                    'jrs 2026/09/18 added () in case PPP year is the same as iYr
                    sFuncNameSearch = sFuncName.Replace("(" & iYr.ToString & ")", "(MOSTRECENT)")
                    'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                    If RegCoeffs.ContainsKey(sFuncNameSearch) Then
                        'jrs 2026/03/25 
                        Debug.WriteLine("Function found: " & sFuncNameSearch)

                        Return sFuncNameSearch
                    End If

                    'searching backward
                    iYrSearch = iYr - 1
                    Dim iMax As Integer
                    'jrs 2022/03/01 Need to go further back when rebasing to 2020 (GDP/Capita (PPP) Versus Primary Net Enrollment Male (1999)) iMax = AMAX(iYr - 20, 1960)
                    'jrs 2025/02/07 Need to go further back when rebasing to 2021 (GDP/Capita (PPP) Versus Female Share of Labor Force (1995)) iMax = AMAX(iYr - 25, 1960)
                    iMax = AMAX(iYr - 30, 1960)
                    Do While iYrSearch >= iMax
                        sFuncNameSearch = sFuncName.Replace("(" & iYr.ToString & ")", "(" & iYrSearch.ToString & ")")
                        'If cntx.GetRegression(sFuncNameSearch).Count > 0 Then
                        If RegCoeffs.ContainsKey(sFuncNameSearch) Then
                            'jrs 2026/03/25 
                            Debug.WriteLine("Function found: " & sFuncNameSearch)

                            Return sFuncNameSearch
                        End If
                        iYrSearch = iYrSearch - 1
                    Loop

                    If sYrConstantDollars = "" Then
                        Exit Do
                    Else
                        If PastCstYrIdx >= PastConstantDollarsYears.Count Then
                            If PastCstYrIdx > PastConstantDollarsYears.Count Then
                                Exit Do
                            Else
                                'Checking option (PPP)
                                Dim sYrConstantDollarsNew As String = sYrConstantDollars.Replace(" " & PrevConstantYear.ToString, "")
                                sFuncName = sFuncName.Replace(sYrConstantDollars, sYrConstantDollarsNew)
                                PastCstYrIdx = PastCstYrIdx + 1
                                If RegCoeffs.ContainsKey(sFuncName) Then
                                    'jrs 2026/03/25 
                                    Debug.WriteLine("Function found: " & sFuncName)

                                    Return sFuncName
                                End If
                            End If
                        Else
                            Dim sYrConstantDollarsNew As String = sYrConstantDollars.Replace(PrevConstantYear.ToString, PastConstantDollarsYears(PastCstYrIdx).ToString)
                            sFuncName = sFuncName.Replace(sYrConstantDollars, sYrConstantDollarsNew)
                            sYrConstantDollars = sYrConstantDollarsNew
                            PrevConstantYear = PastConstantDollarsYears(PastCstYrIdx)
                            PastCstYrIdx = PastCstYrIdx + 1
                            If RegCoeffs.ContainsKey(sFuncName) Then
                                'jrs 2026/03/25 
                                Debug.WriteLine("Function found: " & sFuncName)

                                Return sFuncName
                            End If
                        End If
                    End If
                Loop 'jrs 2024/06/06

            End If
            Return ""
        End Function

        Friend Function getYearNameMstRctWData(dice As IDictionary(Of String, Single?()), ctryName As String, Yrs As Short(), Optional StartYr As Integer = 2100) As Short?
            Dim i As Integer = 0
            getYearNameMstRctWData = Nothing
            'jrs 2023/05/05
            'This procedure assumes Yrs in sorted in descending order, which is not always the case
            Dim bAscending = False
            If Yrs.Length > 1 Then
                If Yrs(0) < Yrs(Yrs.Length - 1) Then
                    bAscending = True
                End If
            End If

            If bAscending Then
                For i = Yrs.Length - 1 To 0 Step -1
                    Dim yr = Yrs(i)
                    If yr <= StartYr Then
                        If dice(ctryName)(i).HasValue Then
                            getYearNameMstRctWData = yr
                            Exit Function
                        End If
                    End If
                Next
            Else
                For Each yr In Yrs
                    If yr <= StartYr Then
                        If dice(ctryName)(i).HasValue Then
                            getYearNameMstRctWData = yr
                            Exit Function
                        End If
                    End If
                    i = i + 1
                Next
            End If
        End Function

        Friend Function getYearIdxMstRctWData(dice As IDictionary(Of String, Single?()), ctryName As String, Yrs As Short(), Optional StartYr As Integer = 2100) As Short?
            'This procedure assumes yrs in descending order
            If Yrs.Count > 1 Then
                If Yrs(0) < Yrs(1) Then
                    'if it's in ascending order call alternative procedure
                    'Debug.Print("Make sure the array is ordered ")
                    getYearIdxMstRctWData = getYearIdxMstRctWDataAsc(dice, ctryName, Yrs, StartYr)
                    Exit Function
                End If
            End If

            'Dim Years As Short() = (From element In Yrs Order By element Descending Select element).ToArray
            Dim i As Short = 0
            getYearIdxMstRctWData = Nothing
            For Each yr In Yrs
                If yr <= StartYr Then
                    If dice(ctryName)(i).HasValue Then
                        getYearIdxMstRctWData = i
                        Exit Function
                    End If
                End If
                i = i + 1S
            Next
        End Function

        Friend Function getYearIdxMstRctWDataAsc(dice As IDictionary(Of String, Single?()), ctryName As String, Yrs As Short(), Optional StartYr As Integer = 2100) As Short?
            'This procedure assumes yrs in ascending order

            'Dim Years As Short() = (From element In Yrs Order By element Descending Select element).ToArray
            Dim i As Short
            i = CShort(Yrs.Count - 1)
            getYearIdxMstRctWDataAsc = Nothing
            Do While i >= 0
                If Yrs(i) <= StartYr Then
                    If dice(ctryName)(i).HasValue Then
                        getYearIdxMstRctWDataAsc = i
                        Exit Function
                    End If
                End If
                i = i - 1S
            Loop
        End Function

        Friend Function getYearAroundOrRecent(ByRef DatTbl As IDictionary(Of String, Single?()), ByRef Yrs As Short(), ByVal sCtry As String, ByVal MRval As Single?,
                                          ByVal DYear As Integer, Optional ByRef TakeMostRecent As Boolean = True, Optional ByVal Max_yr_adv_bkup As Integer = 5, Optional ByRef UsedYear As Integer = 0) As Single?
            'mti 2019/11/01 Take whole function
            'Brought in from dataeducate

            getYearAroundOrRecent = Nothing
            Dim NYear As Integer

            Dim yrIdx As Integer = 0
            For Each yr In Yrs
                If yr = DYear Then
                    getYearAroundOrRecent = DatTbl(sCtry)(yrIdx)
                    If getYearAroundOrRecent.HasValue Then
                        UsedYear = yr
                        Exit Function
                    End If
                    Exit For
                End If
                yrIdx = yrIdx + 1
            Next
            'jrs 2011/07/05 - end

            'mti 2016/04/05 -start
            For yr_adv_bkup = 1 To Max_yr_adv_bkup '10

                If Not getYearAroundOrRecent.HasValue Then
                    NYear = DYear - yr_adv_bkup
                    yrIdx = 0
                    For Each yr In Yrs
                        If yr = NYear Then
                            getYearAroundOrRecent = DatTbl(sCtry)(yrIdx)
                            UsedYear = NYear
                            Exit For
                        End If
                        yrIdx = yrIdx + 1
                    Next
                End If
                If Not getYearAroundOrRecent.HasValue Then
                    NYear = DYear + yr_adv_bkup
                    yrIdx = 0
                    For Each yr In Yrs
                        If yr = NYear Then
                            getYearAroundOrRecent = DatTbl(sCtry)(yrIdx)
                            UsedYear = NYear
                            Exit For
                        End If
                        yrIdx = yrIdx + 1
                    Next
                End If
                'If Not IsNull(getYearAroundOrRecent) Then Exit Function 'For
                If getYearAroundOrRecent.HasValue Then Exit Function
            Next yr_adv_bkup

            'mti 2016/04/05 - end

            If Not getYearAroundOrRecent.HasValue Then
                If TakeMostRecent Then
                    getYearAroundOrRecent = MRval
                    If getYearAroundOrRecent.HasValue Then UsedYear = getMstRctYr(DatTbl, Yrs, sCtry) ' Int(NYear$)
                Else
                    UsedYear = 0
                End If
            End If
        End Function

        Friend Function getYearMaxAmongRecent(ByRef DatTbl As IDictionary(Of String, Single?()), ByRef Yrs As Short(), ByVal sCtry As String, ByVal MRval As Single?,
        ByRef DYear As Integer, Optional ByRef TakeMostRecent As Boolean = True, Optional ByRef Max_yr_adv_bkup As Integer = 5, Optional ByRef Yr_bkup_For_MaxVal As Integer = 5) As Single?
            'mti 2017/04/04 New Proc

            'mti 2016/04/22 Take whole function
            'Brought in from dataeducate

            Dim NYear As Integer
            getYearMaxAmongRecent = Nothing

            'Year for which data is sought, e.g., base year
            NYear = DYear

            'Is data available for custom/base year
            Dim yrIdx As Integer = 0
            For Each yr In Yrs
                If yr = NYear Then
                    getYearMaxAmongRecent = DatTbl(sCtry)(yrIdx)
                    Exit For
                End If
                yrIdx = yrIdx + 1
            Next

            'if not, look for the most recent
            If Not getYearMaxAmongRecent.HasValue And TakeMostRecent Then
                getYearMaxAmongRecent = MRval
                If getYearMaxAmongRecent.HasValue Then NYear = getMstRctYr(DatTbl, Yrs, sCtry)
            End If

            'Still not found, go back (and forward till you find data
            For yr_adv_bkup = 1 To Max_yr_adv_bkup '10

                If Not getYearMaxAmongRecent.HasValue Then
                    'getYearAroundOrRecent = DatTbl.Fields(Trim(Str(DYear - 1)))
                    NYear = DYear - yr_adv_bkup
                    yrIdx = 0
                    For Each yr In Yrs
                        If yr = NYear Then
                            getYearMaxAmongRecent = DatTbl(sCtry)(yrIdx)
                            Exit For
                        End If
                        yrIdx = yrIdx + 1
                    Next
                End If
                If Not getYearMaxAmongRecent.HasValue Then
                    NYear = DYear + yr_adv_bkup
                    yrIdx = 0
                    For Each yr In Yrs
                        If yr = NYear Then
                            getYearMaxAmongRecent = DatTbl(sCtry)(yrIdx)
                            Exit For
                        End If
                        yrIdx = yrIdx + 1
                    Next
                End If
                If getYearMaxAmongRecent.HasValue Then Exit For

            Next yr_adv_bkup


            'Data found, find the max among a certain number of (say,10) values prior to it
            If getYearMaxAmongRecent.HasValue Then
                Dim SDYear As Integer, ValtoCheck As Single?
                SDYear% = NYear
                For yr_adv_bkup = 1 To Yr_bkup_For_MaxVal '10
                    'getYearAroundOrRecent = DatTbl.Fields(Trim(Str(DYear - 1)))
                    NYear = SDYear - yr_adv_bkup
                    yrIdx = 0
                    For Each yr In Yrs
                        If yr = NYear Then
                            ValtoCheck = DatTbl(sCtry)(yrIdx)
                            If ValtoCheck.HasValue Then getYearMaxAmongRecent = AMAX(ValtoCheck, getYearMaxAmongRecent)
                        End If
                        yrIdx = yrIdx + 1
                    Next
                Next yr_adv_bkup

            End If
        End Function

        'jrs 2018/12/12
        Friend Function getMR(ByRef DatTbl As IDictionary(Of String, Single?()), ByRef Yrs As Short(), ByVal sCtry As String) As Single?
            getMR = Nothing

            Dim yrIdx As Integer = 0
            'Descending order
            If Yrs(0) > Yrs(Yrs.Count - 1) Then
                For yrIdx = 0 To Yrs.Count - 1
                    If DatTbl(sCtry)(yrIdx).HasValue Then
                        getMR = DatTbl(sCtry)(yrIdx)
                        Exit Function
                    End If
                Next
            Else 'Ascending Order
                For yrIdx = Yrs.Count - 1 To 0 Step -1
                    If DatTbl(sCtry)(yrIdx).HasValue Then
                        getMR = DatTbl(sCtry)(yrIdx)
                        Exit Function
                    End If
                Next
            End If
        End Function

        'jrs 2022/01/05
        Friend Function getEarliest(ByRef DatTbl As IDictionary(Of String, Single?()), ByRef Yrs As Short(), ByVal sCtry As String) As Single?
            getEarliest = Nothing

            Dim yrIdx As Integer = 0
            'Descending order
            If Yrs(0) > Yrs(Yrs.Count - 1) Then
                For yrIdx = Yrs.Count - 1 To 0 Step -1
                    If DatTbl(sCtry)(yrIdx).HasValue Then
                        getEarliest = DatTbl(sCtry)(yrIdx)
                        Exit Function
                    End If
                Next
            Else 'Ascending Order
                For yrIdx = 0 To Yrs.Count - 1
                    If DatTbl(sCtry)(yrIdx).HasValue Then
                        getEarliest = DatTbl(sCtry)(yrIdx)
                        Exit Function
                    End If
                Next
            End If
        End Function

        'jrs 2008/08/08
        'This function returns the year which has the most recent data for a given table
        'the function assumes years are sorted in descending order 'jrs 2019/10/10 not anymore, handle both
        Friend Function getMstRctYr(ByVal tbl As IDictionary(Of String, Single?()), Years As Short(), sCtry As String) As Integer
            Dim i As Integer
            getMstRctYr = 0
            'the last 2 columns should be Earliest and MostRecent
            'If .Fields(.Fields.Count - 1).Name <> "MostRecent" Then
            '    MsgBox "The last 2 columns should be Earliest and MostRecent", vbCritical, "Get Most Recent Year"
            '        Exit Function
            'End If

            'jrs 2019/10/10
            'Check for descending order
            If Years(0) > Years(Years.Count - 1) Then
                For i = 0 To Years.Count - 1
                    If tbl(sCtry)(i).HasValue Then
                        getMstRctYr = Years(i)
                        Exit Function
                    End If
                Next i
            Else
                For i = Years.Count - 1 To 0 Step -1
                    If tbl(sCtry)(i).HasValue Then
                        getMstRctYr = Years(i)
                        Exit Function
                    End If
                Next i
            End If
        End Function

        Friend Function EstimateFromMostRecentRevBBH(GDPPCPBaseYear As Single?, Year2Estimate As Short, valtbl As IDictionary(Of String, Single?()),
    years As Short(), tbldriver As IDictionary(Of String, Single?()), yearsDrv As Short(), Country As String, FuncName As String,
    ByRef cntx As IRebuildFacade, Optional MaxV As Single? = Nothing, Optional MinV As Single? = Nothing, Optional Conversion As String = "",
    Optional ConversionNumber As Double? = Nothing, Optional Proximity As Integer = 100, Optional EstimateWhenNull As Boolean = True) As Single?
            'mti 2021/09/08 - Take function - optional parameter EstimateWhenNull  added to the call

            'mti April 12, 2007 - added proximity take whole
            '
            '   Read from bottom up to top of Income data table
            '   Leave block with value for Year2Estimate (base year) if available
            '   Leave block with previous year closest to Year2Estimate, if available
            '   Leave block with later/post year closest to Year2Estimate, if available
            '
            EstimateFromMostRecentRevBBH = Nothing
            Dim PrevYearWithData As Short, PrevYearData As Single?, PostYearData As Single?
            Dim Year2EstimateData As Single?, YearVal As Short, VarVal As Single?, PostYearWithData As Single?
            Dim YearGap As Single?, YearShr As Short
            PrevYearWithData = 0
            PrevYearData = Nothing
            PostYearWithData = 0
            PostYearData = Nothing
            Year2EstimateData = Nothing
            For j = 0 To years.Count - 1
                YearVal = years(j)
                If YearVal > 0 Then 'field is an year
                    VarVal = valtbl(Country)(j)
                    If VarVal.HasValue Then
                        If YearVal = Year2Estimate Then
                            Year2EstimateData = VarVal
                            Exit For
                        ElseIf YearVal < Year2Estimate Then 'earlier than Year2Estimate



                            If Year2Estimate - YearVal < Proximity Then 'not too far back
                                PrevYearWithData = YearVal
                                PrevYearData = VarVal
                            End If



                        Else 'later than Year2Estimate
                            PostYearWithData = YearVal
                            PostYearData = VarVal
                            Exit For
                        End If
                    End If
                End If
            Next j

            '
            '   Use survey from base year if possible
            '   If not, try to interpolate
            '
            If Year2EstimateData.HasValue Then 'Have data base year, can quit

                EstimateFromMostRecentRevBBH = Year2EstimateData
                Exit Function

            ElseIf PrevYearData.HasValue And
        PostYearData.HasValue Then 'Can Interpolate
                YearGap = PostYearWithData - PrevYearWithData
                YearShr = Year2Estimate - PrevYearWithData
                EstimateFromMostRecentRevBBH = PrevYearData +
                (PostYearData - PrevYearData) * YearShr / YearGap
                Exit Function
            End If

            '
            '   If arrive here do not have data for year or values for interpolation
            '   Pull out GDP per capita at PPP from separate file for
            '       Year2Estimate and LastestYearWithData
            '   If data for Year2Estimate are not available, will use to
            '       shift latest year for data to Year2Estimate
            '
            Dim LatestYearWithData As Single?, LatestData As Single?
            If PostYearData.HasValue Then
                LatestYearWithData = PostYearWithData
                LatestData = PostYearData
            Else
                LatestYearWithData = PrevYearWithData
                LatestData = PrevYearData

            End If

            Dim DrvVAl_EstYear As Single?, DrvVal_LatestYear As Single?
            DrvVAl_EstYear = Nothing
            DrvVal_LatestYear = Nothing

            For fld% = 0 To yearsDrv.Count - 1
                If yearsDrv(fld%) = LatestYearWithData Then
                    DrvVal_LatestYear = tbldriver(Country)(fld)
                End If
                If yearsDrv(fld%) = Year2Estimate Then
                    DrvVAl_EstYear = tbldriver(Country)(fld)
                End If
            Next fld%

            '
            '   Use data from base year (Year2EstimateData) if available
            '   Else Use GDPPC at PPP data from base year and latest data year, plus
            '       Cross-Sectional function to shift base year calculation

            If LatestYearWithData > 0 And DrvVAl_EstYear.HasValue And
        DrvVal_LatestYear.HasValue Then
                'Have data other year and drivers both base, other year
                If Conversion = "Div" And ConversionNumber <> 0 Then
                    DrvVal_LatestYear = CSng(DrvVal_LatestYear / ConversionNumber)
                    DrvVAl_EstYear = CSng(DrvVAl_EstYear / ConversionNumber)
                End If

                Dim Estimate As SeriesEstimator
                Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                DrvVals.Add("DrvVAl_EstYear", DrvVAl_EstYear)
                DrvVals.Add("DrvVal_LatestYear", DrvVal_LatestYear)
                Estimate = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                Dim Val_EstYear As Single?, Val_latestYear As Single?, ValDiff As Single?
                Val_EstYear = Estimate("DrvVAl_EstYear")
                Val_latestYear = Estimate("DrvVal_LatestYear")

                ValDiff = Val_latestYear - Val_EstYear
                EstimateFromMostRecentRevBBH = LatestData - ValDiff

                'jrs 2008/10/10
                'Don't allow estimation to drop bellow 10% of LatestData
                EstimateFromMostRecentRevBBH = AMAX(EstimateFromMostRecentRevBBH, CSng(0.1 * LatestData))

            Else 'Do not have data base year or 3 before, after; use Cross-Sectional
                If EstimateWhenNull Then        'mti 2021/09/08
                    Dim Estimate As SeriesEstimator
                    Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                    DrvVals.Add("GDPPCPBaseYear", GDPPCPBaseYear)
                    Estimate = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                    Dim CalculatedVal As Single?
                    CalculatedVal = Estimate("GDPPCPBaseYear")
                    EstimateFromMostRecentRevBBH = CalculatedVal
                End If
            End If

                '
                '   Bound Value between 0 and 100 (or other values specified_
                '
                '    EstimateFromMostRecentRevBBH = Amin(100, AMAX(0, EstimateFromMostRecentRevBBH))
                If EstimateFromMostRecentRevBBH.HasValue And (MaxV.HasValue Or MinV.HasValue) Then
                EstimateFromMostRecentRevBBH = ApplyLimits(EstimateFromMostRecentRevBBH, CSng(MinV), CSng(MaxV))
            End If



        End Function

        Friend Function EstimateFromMostRecentRevMTI(GDPPCPBaseYear As Single?, Year2Estimate As Short, valtbl As IDictionary(Of String, Single?()),
    years As Short(), tbldriver As IDictionary(Of String, Single?()), yearsDrv As Short(), Country As String, FuncName As String,
    ByRef cntx As IRebuildFacade, Optional MaxV As Single? = Nothing, Optional MinV As Single? = Nothing, Optional Conversion As String = "",
    Optional ConversionNumber As Double? = Nothing, Optional Proximity As Integer = 100,
    Optional ReturnNullWhenNoData As Boolean = False, Optional UseInterpolation As Boolean = True) As Single?
            'mti April 12, 2007 - added proximity take whole
            '
            '   Read from bottom up to top of Income data table
            '   Leave block with value for Year2Estimate (base year) if available
            '   Leave block with previous year closest to Year2Estimate, if available
            '   Leave block with later/post year closest to Year2Estimate, if available
            '
            EstimateFromMostRecentRevMTI = Nothing
            Dim PrevYearWithData As Short?, PrevYearData As Single?, PostYearData As Single?
            Dim Year2EstimateData As Single?, YearVal As Short, VarVal As Single?, PostYearWithData As Short?
            Dim YearGap As Single?, YearShr As Short
            PrevYearWithData = 0
            PrevYearData = Nothing
            PostYearWithData = 0
            PostYearData = Nothing
            Year2EstimateData = Nothing
            For j = 0 To years.Count - 1
                YearVal = years(j)
                If YearVal > 0 Then 'field is an year
                    VarVal = valtbl(Country)(j)
                    If VarVal.HasValue Then
                        If YearVal = Year2Estimate Then
                            Year2EstimateData = VarVal
                            Exit For
                        ElseIf Math.Abs(Year2Estimate - YearVal) <= Proximity Then   'earlier than Year2Estimate

                            If Not PrevYearWithData.HasValue Then
                                'both prev and post is empty
                                PrevYearWithData = YearVal
                                PrevYearData = VarVal
                            ElseIf Not PostYearWithData.HasValue Then
                                PostYearWithData = YearVal
                                PostYearData = VarVal

                            Else 'both filled

                                PrevYearWithData = PostYearWithData
                                PrevYearData = PostYearData

                                PostYearWithData = YearVal
                                PostYearData = VarVal
                            End If
                        End If
                    End If
                End If
            Next j

            '
            '   Use survey from base year if possible
            '   If not, try to interpolate
            '
            If Year2EstimateData.HasValue Then 'Have data base year, can quit

                EstimateFromMostRecentRevMTI = Year2EstimateData
                Exit Function

            ElseIf UseInterpolation And PrevYearData.HasValue And
        PostYearData.HasValue Then 'Can Interpolate
                YearGap = PostYearWithData - PrevYearWithData
                YearShr = Year2Estimate - CShort(PrevYearWithData)
                EstimateFromMostRecentRevMTI = PrevYearData +
                (PostYearData - PrevYearData) * YearShr / YearGap
                Exit Function
            End If

            '
            '   If arrive here do not have data for year or values for interpolation
            '   Pull out GDP per capita at PPP from separate file for
            '       Year2Estimate and LastestYearWithData
            '   If data for Year2Estimate are not available, will use to
            '       shift latest year for data to Year2Estimate
            '
            Dim LatestYearWithData As Single?, LatestData As Single?
            If PostYearData.HasValue Then
                LatestYearWithData = PostYearWithData
                LatestData = PostYearData
            Else
                LatestYearWithData = PrevYearWithData
                LatestData = PrevYearData

            End If

            Dim DrvVAl_EstYear As Single?, DrvVal_LatestYear As Single?
            DrvVAl_EstYear = Nothing
            DrvVal_LatestYear = Nothing

            For fld% = 0 To yearsDrv.Count - 1
                If yearsDrv(fld%) = LatestYearWithData Then
                    DrvVal_LatestYear = tbldriver(Country)(fld)
                End If
                If yearsDrv(fld%) = Year2Estimate Then
                    DrvVAl_EstYear = tbldriver(Country)(fld)
                End If
            Next fld%

            '
            '   Use data from base year (Year2EstimateData) if available
            '   Else Use GDPPC at PPP data from base year and latest data year, plus
            '       Cross-Sectional function to shift base year calculation

            If LatestYearWithData > 0 And DrvVAl_EstYear.HasValue And
        DrvVal_LatestYear.HasValue Then
                'Have data other year and drivers both base, other year
                If Conversion = "Div" And ConversionNumber <> 0 Then
                    DrvVal_LatestYear = CSng(DrvVal_LatestYear / ConversionNumber)
                    DrvVAl_EstYear = CSng(DrvVAl_EstYear / ConversionNumber)
                End If

                Dim Estimate As SeriesEstimator
                Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                DrvVals.Add("DrvVAl_EstYear", DrvVAl_EstYear)
                DrvVals.Add("DrvVal_LatestYear", DrvVal_LatestYear)
                Estimate = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                Dim Val_EstYear As Single?, Val_latestYear As Single?, ValDiff As Single?
                Val_EstYear = Estimate("DrvVAl_EstYear")
                Val_latestYear = Estimate("DrvVal_LatestYear")

                ValDiff = Val_latestYear - Val_EstYear
                EstimateFromMostRecentRevMTI = LatestData - ValDiff

                'jrs 2008/10/10
                'Don't allow estimation to drop bellow 10% of LatestData
                EstimateFromMostRecentRevMTI = AMAX(EstimateFromMostRecentRevMTI, CSng(0.1 * LatestData))

            Else 'Do not have data base year or 3 before, after; use Cross-Sectional
                If ReturnNullWhenNoData Then
                    EstimateFromMostRecentRevMTI = Nothing
                Else
                    Dim Estimate As SeriesEstimator
                    Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                    DrvVals.Add("GDPPCPBaseYear", GDPPCPBaseYear)
                    Estimate = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                    Dim CalculatedVal As Single?
                    CalculatedVal = Estimate("GDPPCPBaseYear")
                    EstimateFromMostRecentRevMTI = CalculatedVal

                    If LatestYearWithData > 0 And Math.Abs(Year2Estimate - CShort(LatestYearWithData)) <= Proximity Then
                        'there is one data point; however there is no data on driver for that point; use that point - when the point is within proximity
                        EstimateFromMostRecentRevMTI = LatestData
                    End If
                End If

            End If

            '
            '   Bound Value between 0 and 100 (or other values specified_
            '
            '    EstimateFromMostRecentRevBBH = Amin(100, AMAX(0, EstimateFromMostRecentRevBBH))
            If EstimateFromMostRecentRevMTI.HasValue And (MaxV.HasValue Or MinV.HasValue) Then
                EstimateFromMostRecentRevMTI = ApplyLimits(EstimateFromMostRecentRevMTI, CSng(MinV), CSng(MaxV))
            End If



        End Function

        Friend Function ReadOrEstimateDataPoint(GDPPCPBaseYear As Single?, Year2Estimate As Short, valtbl As IDictionary(Of String, Single?()),
    years As Short(), tbldriver As IDictionary(Of String, Single?()), yearsDrv As Short(), Country As String, FuncName As String,
    ByRef cntx As IRebuildFacade, Optional MaxV As Single? = Nothing, Optional MinV As Single? = Nothing, Optional ValTbl1 As IDictionary(Of String, Single?()) = Nothing,
    Optional years1 As Short() = Nothing, Optional ValTbl1Formula$ = "",
    Optional DrvFormula$ = "", Optional Proximity As Integer = 100, Optional SlopeDir As String = "Upward", Optional Grate As Single = 0) As Single?
            '-----------------------------------------------------------------------------------------------------
            'mti 2014/03/27 - new proc (comments added)

            ' This function is a modification of EstimateFromMostRecent. It reads or estimates data from a data table
            ' It can use two data tables and use simple formula between them (these feature can be extended later)
            ' If data is not found for the target year (Year2Estimate) attempts are made to get the data from adjacent years
            ' When the above attempts fail data is estimated through interpolation if two points, one prior and one post the target year, are found
            ' When only one point is found, data is estimated using an estimation function and the estimation differential between the point and the target year
            ' When no data point is found, data is estimated using the estimation function

            ' Valtbl and Valtbl1 contains data tables (recordset), ValTbl1Formula$ is the conversion formula formatted as one character operator and numeric factor, e.g., "/1000"
            ' FuncName$ is the estimation function, Country$ the name of the country for which data is being read/estimated
            ' TblDriver contains data for the independent variable of the estimation function; DrvFormula contains any data conversion for the driver data (i.e., independent variabl)
            ' Proximity is the difference in years used to limi data search (for dependent variable)
            ' Slopedir is used only in a special case. It is mostly needed for historical pre-processor
            ' for a country (like Afghanistan) with no data to make sure estimations don't get inconsistent with available data
            ' MaxV and MinV are upper and lower bounds for read/estimated data

            ReadOrEstimateDataPoint = Nothing
            Dim PrevYearWithData As Single = 0
            Dim PrevYearData As Single? = Nothing
            Dim PostYearWithDat As Single = 0
            Dim PostYearData As Single? = Nothing
            Dim Year2EstimateData As Single? = Nothing

            Dim Year2EstimateDataAfter1 As Single? = Nothing
            Dim Year2EstimateDataAfter2 As Single? = Nothing

            Dim Year2EstimateDataBefor1 As Single? = Nothing
            Dim Year2EstimateDataBefore2 As Single? = Nothing

            Dim YearVal As Single?, VarVal As Single?

            Dim Year2EstimateDataBefore1 As Single?, PostYearWithData As Single?
            Dim YearGap As Single?, YearShr As Single?
            Dim NearestYearWithData As Single?, NearestData As Single?

            For j = 0 To years.Count - 1
                YearVal = years(j)
                If YearVal > 0 Then 'field is an year
                    VarVal = valtbl(Country)(j)
                    If VarVal.HasValue Then

                        If YearVal = Year2Estimate Then
                            ''Data for exact year is available

                            'convert data using a second series, if it is required to do so
                            ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)

                            If VarVal.HasValue Then
                                Year2EstimateData = VarVal
                                Exit For
                            End If

                        ElseIf YearVal = Year2Estimate + 1 Then
                            'did not find exact point; but found the next year

                            'convert data using a second series, if it is required to do so
                            ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)

                            If VarVal.HasValue Then
                                Year2EstimateDataAfter1 = VarVal
                                Exit For
                            End If

                        ElseIf YearVal = Year2Estimate + 2 Then
                            'did not find exact point; but found the point two years later

                            'convert data using a second series, if it is required to do so
                            ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)

                            If VarVal.HasValue Then
                                Year2EstimateDataAfter2 = VarVal
                                Exit For
                            End If

                        ElseIf YearVal = Year2Estimate - 1 Then
                            'did not find exact point; but found the year before

                            'convert data using a second series, if it is required to do so
                            ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)

                            If VarVal.HasValue Then Year2EstimateDataBefore1 = VarVal

                        ElseIf YearVal = Year2Estimate - 2 Then

                            'did not find data for the exact point; but found data two years prior

                            'convert data using a second series, if it is required to do so
                            ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)

                            If VarVal.HasValue Then Year2EstimateDataBefore2 = VarVal

                        ElseIf YearVal < Year2Estimate Then 'earlier than Year2Estimate

                            If Year2Estimate - YearVal < Proximity Then 'not too far back

                                'convert data using a second series, if it is required to do so
                                ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)

                                If VarVal.HasValue Then
                                    PrevYearWithData = CSng(YearVal)
                                    PrevYearData = VarVal
                                End If

                            End If

                        Else 'later than Year2Estimate



                            'convert data using a second series, if it is required to do so
                            ValueConversion(VarVal, CShort(YearVal), ValTbl1, years1, Country, ValTbl1Formula$)




                            If VarVal.HasValue Then

                                PostYearWithData = YearVal
                                PostYearData = VarVal
                                Exit For


                            End If



                        End If
                    End If
                End If
            Next j


            If Year2EstimateData.HasValue Then 'Have data base year, can quit
                'Data avaialble for exact year

                ReadOrEstimateDataPoint = Year2EstimateData
                Exit Function

            ElseIf Year2EstimateDataAfter1.HasValue Then 'Have data base year, can quit
                'Use the data avaialble for next year

                ReadOrEstimateDataPoint = Year2EstimateDataAfter1
                Exit Function

            ElseIf Year2EstimateDataBefore1.HasValue Then 'Have data base year, can quit
                'Use the data avaialble for prior year

                ReadOrEstimateDataPoint = Year2EstimateDataBefore1
                Exit Function

            ElseIf Year2EstimateDataAfter2.HasValue Then 'Have data base year, can quit
                'Use the data avaialble for two years after

                ReadOrEstimateDataPoint = Year2EstimateDataAfter2
                Exit Function

            ElseIf Year2EstimateDataBefore2.HasValue Then 'Have data base year, can quit
                'Use the data avaialble for two years before

                ReadOrEstimateDataPoint = Year2EstimateDataBefore2
                Exit Function

            ElseIf PrevYearData.HasValue And
        PostYearData.HasValue Then
                'Data Points both before and after target year are available
                'interpolate data for the target year

                YearGap = PostYearWithData - PrevYearWithData
                YearShr = Year2Estimate - PrevYearWithData
                ReadOrEstimateDataPoint = PrevYearData +
                (PostYearData - PrevYearData) * YearShr / YearGap
                Exit Function
            End If

            '
            ' Data available only after or only before the target year
            ' So plain interpolation is not possible
            ' If at least one data point is available estimation function differential will be used
            ' When no point is available estimation function will be used

            'Available points were read as post or prior (to target year) data
            'consolidate those as nearest data (prioritize post)

            If PostYearData.HasValue Then
                NearestYearWithData = PostYearWithData
                NearestData = PostYearData
            Else
                NearestYearWithData = PrevYearWithData
                NearestData = PrevYearData

            End If

            'Read data for the driver variable, i.e., independent variable of the estimation function
            Dim DrvVAl_EstYear As Single? = Nothing
            Dim DrvVal_NearestYear As Single? = Nothing
            Dim FldName As String
            Dim ConversionNumber As Single?

            For fld% = 0 To yearsDrv.Count - 1
                FldName$ = yearsDrv(fld%).ToString
                If FldName.Trim = NearestYearWithData.ToString.Trim Then
                    DrvVal_NearestYear = tbldriver(Country)(fld%)
                End If
                If FldName$.Trim = Year2Estimate.ToString.Trim Then
                    DrvVAl_EstYear = tbldriver(Country)(fld%)
                End If

            Next fld%


            'Use either estimation differential (between nearest and target year or just estimation for target year
            If NearestYearWithData > 0 And DrvVAl_EstYear.HasValue And
        DrvVal_NearestYear.HasValue Then

                'Have driver variable (or, IV) values for both the target (of estimation) year _
                'and the nearest year with data for dependent variable

                'Proceed to estimate dependent variable values for these two years using estimation function

                'See if driver (or, IV) data needs to be converted - e.g. GDPPC/1000 etc.
                If DrvFormula.Length > 0 Then  'need to convert value of driver data using a formula
                    ConversionNumber = CSng(DrvFormula.Remove(0, 1))
                    Select Case DrvFormula.Substring(0, 1)
                        Case "/"
                            If ConversionNumber <> 0 Then
                                DrvVal_NearestYear = DrvVal_NearestYear / ConversionNumber
                                DrvVAl_EstYear = DrvVAl_EstYear / ConversionNumber
                            End If
                        Case "*"
                            DrvVal_NearestYear = DrvVal_NearestYear * ConversionNumber
                            DrvVAl_EstYear = DrvVAl_EstYear * ConversionNumber
                        Case "+"
                            DrvVal_NearestYear = DrvVal_NearestYear + ConversionNumber
                            DrvVAl_EstYear = DrvVAl_EstYear + ConversionNumber
                        Case "-"
                            DrvVal_NearestYear = DrvVal_NearestYear - ConversionNumber
                            DrvVAl_EstYear = DrvVAl_EstYear - ConversionNumber
                        Case "^"
                            DrvVal_NearestYear = CSng(DrvVal_NearestYear ^ ConversionNumber)
                            DrvVAl_EstYear = CSng(DrvVAl_EstYear ^ ConversionNumber)
                    End Select
                End If


                'Estimate target (dependent) variable values for target year and the nearest year with (DV) data
                Dim Val_EstYear As Single?, Val_NearestYear As Single?, ValDiff As Single?, Val_NearestYear_From_GRate As Single?
                Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                DrvVals.Add("DrvVAl_EstYear", DrvVAl_EstYear)
                DrvVals.Add("DrvVal_NearestYear", DrvVal_NearestYear)
                Dim XYTABL As SeriesEstimator
                XYTABL = cntx.GetRegression(FuncName).ToEstimator(DrvVals)
                Val_EstYear = XYTABL("DrvVAl_EstYear")
                Val_NearestYear = XYTABL("DrvVal_NearestYear")

                'Calculate difference in the two estimations
                ValDiff = Val_NearestYear - Val_EstYear

                'mti 2014/05/28 -start
                If Grate <> 0 Then

                    Val_NearestYear_From_GRate = CSng(Val_EstYear * (1 + Grate) ^ (NearestYearWithData - Year2Estimate - 1))

                    ValDiff = Amin(ValDiff, Val_NearestYear_From_GRate - Val_EstYear)
                End If
                'mti 2014/05/28 -end

                'Subtract (or add) estimation difference to nearest data to get the estimated value for the target year
                ReadOrEstimateDataPoint = NearestData - ValDiff

                'jrs 2008/10/10
                'Don't allow estimation to drop bellow 10% of NearestData
                ReadOrEstimateDataPoint = AMAX(ReadOrEstimateDataPoint, CSng(0.1 * NearestData))

            Else 'Do not have data base year or 3 before, after; use Cross-Sectional
                Dim DrvVals As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                DrvVals.Add("GDPPCPBaseYear", GDPPCPBaseYear)
                Dim XYTABL As SeriesEstimator
                XYTABL = cntx.GetRegression(FuncName).ToEstimator(DrvVals)

                Dim CalculatedVal As Single?
                CalculatedVal = XYTABL("GDPPCPBaseYear")
                ReadOrEstimateDataPoint = CalculatedVal

                'If at least one point is available; but driver data not available for the target point; estimation differential won't work
                'So plain estimation is done
                'However make sure estimation is consistent with the value of any available (dependent variable) value
                If NearestData.HasValue Then
                    If NearestYearWithData > Year2Estimate Then
                        'Data for an earlier year cannot be greater; won't work if slope of XYTABL is negative
                        If SlopeDir = "Upward" Then ReadOrEstimateDataPoint = Amin(ReadOrEstimateDataPoint, NearestData)
                    Else
                        'Data for a later year cannot be less; won't work if slope of XYTABL is negative
                        If SlopeDir = "Upward" Then ReadOrEstimateDataPoint = AMAX(ReadOrEstimateDataPoint, NearestData)
                    End If
                End If

            End If

            '
            '   Bound Value between Max and Minimum passed to this procedure
            '
            If ReadOrEstimateDataPoint.HasValue Then
                If MaxV.HasValue Then
                    ReadOrEstimateDataPoint = Amin(MaxV, ReadOrEstimateDataPoint)
                End If
                If MinV.HasValue Then
                    ReadOrEstimateDataPoint = AMAX(MinV, ReadOrEstimateDataPoint)
                End If
            End If



        End Function

        Friend Sub ValueConversion(ByRef Value2Convert As Single?, YearVal As Short, ValTblConvert As IDictionary(Of String, Single?()),
    years As Short(), CtryName As String, FormulaConvert$)
            'mti 2014/03/27 - new proc
            ' Takes a value read from a timeseries
            ' And converts it using data from another times series

            If FormulaConvert$ = "" Then
                Exit Sub
            End If

            Dim VarValConvert As Single?, VarValBefore1 As Single?, VarValBefore2 As Single?, VarValAfter1 As Single?, VarValAfter2 As Single?
            For fld = 0 To years.Count - 1
                If years(fld) = YearVal Then VarValConvert = ValTblConvert(CtryName)(fld)
                If years(fld) = YearVal - 1 Then VarValBefore1 = ValTblConvert(CtryName)(fld)
                If years(fld) = YearVal - 2 Then VarValBefore2 = ValTblConvert(CtryName)(fld)
                If years(fld) = YearVal + 1 Then VarValAfter1 = ValTblConvert(CtryName)(fld)
                If years(fld) = YearVal + 2 Then VarValAfter1 = ValTblConvert(CtryName)(fld)
            Next

            'Take adjacent values if exact year value is not found
            If Not VarValConvert.HasValue Then VarValConvert = VarValAfter1
            If Not VarValConvert.HasValue Then VarValConvert = VarValBefore1
            If Not VarValConvert.HasValue Then VarValConvert = VarValAfter2
            If Not VarValConvert.HasValue Then VarValConvert = VarValBefore2


            If VarValConvert.HasValue Then
                Select Case FormulaConvert$.Substring(0, 1)
                    Case "/"
                        If VarValConvert <> 0 Then
                            Value2Convert = Value2Convert / VarValConvert
                        Else
                            Value2Convert = Nothing
                        End If
                    Case "*"
                        Value2Convert = Value2Convert * VarValConvert
                    Case "+"
                        Value2Convert = Value2Convert + VarValConvert
                    Case "-"
                        Value2Convert = Value2Convert - VarValConvert
                    Case "^"
                        Value2Convert = CSng(Value2Convert ^ VarValConvert)
                    Case Else
                        Value2Convert = Nothing
                End Select

                'mti 2014/05/28 - start
                Dim RemainderFactor As Single?
                If FormulaConvert$.Length > 2 Then
                    RemainderFactor = CSng(FormulaConvert$.Remove(0, 2))
                End If

                If FormulaConvert$.Length > 1 Then
                    Select Case FormulaConvert$.Substring(1, 1)
                        Case "/"
                            If RemainderFactor <> 0 Then
                                Value2Convert = Value2Convert / RemainderFactor
                            Else
                                Value2Convert = Nothing
                            End If
                        Case "*"
                            Value2Convert = Value2Convert * RemainderFactor
                        Case "+"
                            Value2Convert = Value2Convert + RemainderFactor
                        Case "-"
                            Value2Convert = Value2Convert - RemainderFactor
                        Case "^"
                            Value2Convert = CSng(Value2Convert ^ RemainderFactor)
                        Case Else
                            Value2Convert = Nothing
                    End Select
                Else
                    Value2Convert = Nothing
                End If
                'mti 2014/05/28 - end
            Else
                Value2Convert = Nothing
            End If

        End Sub

        Public Function EstimateUsingElasticity(Yr_Target As Integer, tblDep As IDictionary(Of String, Single?()), yearsDep As Short(), tblIndep As IDictionary(Of String, Single?()), yearsIndep As Short(), Country$, Optional Interval As Integer = 10, Optional MinimumDistance As Integer = 1) As Single?
            'mti 2020/09/01 Whole Proc _the final one

            'This procedure returns data for a target year when available
            'When target point is missing, it returns estimate using (country) elasticity of the variable with an independent variable
            'Interval is used as the default period over which elasticity is computed, the period includes the target year, but can go forward or backward few years depending on data availability


            EstimateUsingElasticity = Nothing

            Dim DepIdx As Integer = 0

            Dim Yr_Earlier As Integer = 0
            Dim Val_Earlier As Single? = Nothing
            Dim Yr_Later As Integer = 0 'jrs 2018/12/10 this variable was missing an "a", it was PostYearWithDat
            Dim Val_Later As Single? = Nothing
            Dim Yr_TargetData As Single? = Nothing

            'tblDep.Seek "=", Country$


            For j = 0 To yearsDep.Count - 1    'forward
                Dim YearVal = yearsDep(j)
                If Abs(Yr_Target - YearVal) <= Interval Then
                    If YearVal > 0 Then 'field is an year
                        Dim VarVal As Single? = tblDep(Country$)(j)
                        If VarVal.HasValue Then

                            If YearVal = Yr_Target Then
                                EstimateUsingElasticity = VarVal
                                Exit Function
                            Else        'If Abs(Yr_Target - YearVal) <= Interval Then   'earlier than Yr_Target
                                If Yr_Earlier = 0 Then
                                    'both prev and post is empty
                                    Yr_Earlier = YearVal
                                    Val_Earlier = VarVal
                                Else 'both filled

                                    Yr_Later = YearVal
                                    Val_Later = VarVal
                                    If Yr_Later - Yr_Earlier >= Interval Then Exit For

                                End If

                            End If

                        End If
                    End If
                End If
            Next j



            '
            '   If arrive here do not have data for year or values for interpolation
            '   Pull out GDP per capita at PPP from separate file for
            '       Yr_Target and LastestYearWithData
            '   If data for Yr_Target are not available, will use to
            '       shift latest year for data to Yr_Target
            '
            If Not Val_Later.HasValue Then
                Exit Function
            End If

            If Not Val_Earlier.HasValue Then
                Exit Function
            End If

            If Val_Earlier = 0 Then
                Exit Function
            End If

            If Yr_Later - Yr_Earlier < MinimumDistance Then
                Exit Function
            End If

            '    LatestYearWithData = Yr_Later
            '    LatestData = Val_Later

            'tblIndep.Seek "=", Country
            Dim Val_Indep_Target As Single? = Nothing
            Dim DrvVal_LatestYear As Single? = Nothing
            Dim Val_indep_Initial As Single?
            Dim Val_indep_Final As Single?

            'tblIndep.Seek "=", Country$
            For fld% = 0 To yearsIndep.Count - 1
                Dim FldName$ = CStr(yearsIndep(fld%))
                Select Case FldName$.Trim
                    Case Yr_Earlier.ToString.Trim
                        Val_indep_Initial = tblIndep(Country$)(fld%)
                    Case Yr_Later.ToString.Trim
                        Val_indep_Final = tblIndep(Country$)(fld%)
                    Case Yr_Target.ToString.Trim
                        Val_Indep_Target = tblIndep(Country$)(fld%)
                End Select
            Next fld%

            If Val_indep_Initial = 0 Or Val_indep_Final = 0 Or Val_indep_Initial = Val_indep_Final Then
                Exit Function
            End If

            '    pcnt_chng_dep = (Val_Later - Val_Earlier) / Val_Earlier
            '    pcnt_chng_indep = (Val_indep_Final - Val_indep_Initial) / Val_indep_Initial
            '    elas_dep_indep = pcnt_chng_dep / pcnt_chng_indep
            '    pcnt_chng_indep_target = (Val_Indep_Target / Val_indep_Final) - 1
            '    EstimateUsingElasticity = Val_Later * (1 + pcnt_chng_indep_target * elas_dep_indep)

            Dim ExpTime = 1 / (Yr_Later - Yr_Earlier)
            Dim pcnt_chng_ann_dep = (Val_Later / Val_Earlier) ^ ExpTime - 1
            Dim pcnt_chng_ann_indep = (Val_indep_Final / Val_indep_Initial) ^ ExpTime - 1
            Dim elas_dep_indep = pcnt_chng_ann_dep / pcnt_chng_ann_indep

            ExpTime = Yr_Target - Yr_Later
            Dim pcnt_chng_ann_indep_target = (Val_Indep_Target / Val_indep_Final) ^ (1 / ExpTime) - 1
            EstimateUsingElasticity = CSng(Val_Later * (1 + pcnt_chng_ann_indep_target * elas_dep_indep) ^ ExpTime)

        End Function


        'jrs 2008/07/07
        'function to retrieve a given year if it exists, if not returns estimation
        Friend Function getYearOrEstimation(ByVal tbl As IDictionary(Of String, Single?()), Years As Short(), sCtry As String,
                                               ByVal valMR As Single?, ByVal iYr As Integer, ByVal MinRange As Integer, MaxRange As Integer) As Single?
            'MinRange and MaxRange are used for the estimation process
            Dim i As Integer
            'the last 2 columns should be Earliest and MostRecent
            'gvdb 2024/04/05
            ' unused variable
            'Dim bAlert As Boolean
            'If .Fields(.Fields.Count - 1).Name <> "MostRecent" Then
            '    bAlert = True
            'End If

            'Years are supposed to be in ascending order
            For i = Years.Count - 1 To 0 Step -1
                If iYr >= Years(0) Then
                    If Years(i) = iYr Then
                        getYearOrEstimation = tbl(sCtry)(i)
                        If Not getYearOrEstimation.HasValue Then
                            'If bAlert Then
                            '    MsgBox "The last column of table " & tbl.Name & " should be MostRecent."
                            '        getYearOrEstimation = Null
                            'Else
                            getYearOrEstimation = estimateHistory(tbl, Years, sCtry, valMR, i, iYr, MinRange, MaxRange)
                            'End If
                        End If
                        Exit Function
                    ElseIf Years(i) < iYr Then
                        'If bAlert Then
                        '    MsgBox "The last column of table " & tbl.Name & " should be MostRecent."
                        '            getYearOrEstimation = Null
                        'Else
                        getYearOrEstimation = estimateHistory(tbl, Years, sCtry, valMR, -1, iYr, MinRange, MaxRange)
                        'End If
                        Exit Function
                    End If
                Else 'when iYr is lower than the first year in the table
                    getYearOrEstimation = estimateHistory(tbl, Years, sCtry, valMR, -1, iYr, MinRange, MaxRange)
                End If
            Next i
        End Function

        'jrs 2008/07/07
        'Function to Estimate value for a given year in Empirical History
        'slight modification from fillHistoryHoles in the GUI
        'Rules: ***********************************************************
        '1. Interpolation should be preferred to extrapolation.  That is, if the base year is bracketed by data, use them.
        '2. When interpolation is possible, the minimum years rule should be overruled and the two nearest should be used, even if only 2 years apart, e.g. data for 2004, 2006.
        '3. When extrapolation is pursued and the earliest value is more than the years specified for extrapolation (like 15), then we ought to fall back to Most Recent when available.
        '******************************************************************
        Friend Function estimateHistory(ByRef valtbl As IDictionary(Of String, Single?()), Years As Short(), sCtry As String,
                                           ByVal valMR As Single?, ByVal idx As Integer, ByVal iYear As Integer,
                                            ByVal MinRange As Integer, ByVal MaxRange As Integer) As Single?
            '1. valtbl is the table that contains the historic data
            '2. idx is the index of the value to be filled (-1 is when the column is not in table)
            '3. iYear is the year to be read
            '4. MinRange is the minimum # of years between the 2 points to extrapolate
            '5. MaxRange is the max # of years between the 2 points to extrapolate
            estimateHistory = Nothing
            'We need to find 2 points to interpolate or extrapolate
            Dim P1 As Integer, P2 As Integer
            Dim Y1 As Integer, y2 As Integer
            Dim Val1 As Single?, Val2 As Single?
            Dim i As Integer
            P1 = -1
            P2 = -1

            'If idx is not provided assume it doesn't exist
            '(already checked in getYearOrEstimation)
            'and find closest smaller year
            If idx < 0 Then
                Dim lst As Integer
                lst = Years.Count - 1
                'Do While Not IsNumeric(valtbl.Fields(lst).Name)
                '    lst = lst - 1
                '    If lst < 2 Then
                '        Exit Do
                '    End If
                'Loop

                'Find closest smaller year
                idx = findClosestSmallerYearIdx(0, lst, valtbl, Years, sCtry, iYear, lst)
                If idx = -1 Then
                    estimateHistory = Nothing
                    Exit Function
                End If
                P1 = idx
                Y1 = Years(P1)
                Val1 = valtbl(sCtry)(P1)
                'If the point found is not the last point
                If P1 < lst Then
                    Do While Not valtbl(sCtry)(idx + 1).HasValue _
            Or Years(idx + 1) - Y1 < MinRange
                        'if doing interpolation overrule MinRange
                        'checking valid year
                        If valtbl(sCtry)(idx + 1).HasValue Then
                            'checking interpolation
                            If Y1 < iYear And Years(idx + 1) > iYear Then
                                Exit Do
                            End If
                        End If

                        idx = idx + 1
                        If idx + 1 > lst Then 'jrs 2006/07/31 added + 1 (hist+for. POVGap, af. sub-sah)
                            Exit Do
                        ElseIf Years(idx + 1) - Y1 > MaxRange Then
                            idx = lst
                            Exit Do
                        End If
                    Loop
                    P2 = idx + 1
                    If P2 > lst Then
                        'look in the past
                        P2 = P1
                        y2 = Y1
                        Val2 = Val1
                        idx = P1
                        'jrs 2025/12/01 removed the >= now just >
                        'jrs 2011/07/25
                        If idx > 0 Then
                            Do While Not valtbl(sCtry)(idx - 1).HasValue _
                    Or y2 - Years(idx - 1) < MinRange
                                'if doing interpolation overrule MinRange
                                'checking valid year
                                If valtbl(sCtry)(idx - 1).HasValue Then
                                    'checking interpolation
                                    If y2 > iYear And Years(idx - 1) < iYear Then
                                        Exit Do
                                    End If
                                End If

                                idx = idx - 1
                                'jrs 2025/12/01 changed the check from < 0 to < 1
                                If idx < 1 Then
                                    Exit Do
                                ElseIf y2 - Years(idx - 1) > MaxRange Then
                                    idx = 0
                                    Exit Do
                                End If
                            Loop
                        End If
                        P1 = idx - 1
                        If P1 < 2 Then
                            estimateHistory = valMR
                            Exit Function
                        End If
                        Y1 = Years(P1)
                        Val1 = valtbl(sCtry)(P1)
                        estimateHistory = Val1 + (Val2 - Val1) / (y2 - Y1) * (iYear - Y1)
                        Exit Function
                    End If
                    y2 = Years(P2)
                    Val2 = valtbl(sCtry)(P2)
                    estimateHistory = Val1 + (Val2 - Val1) / (y2 - Y1) * (iYear - Y1)
                    Exit Function
                Else 'if it's the last point, we have to look for smaller points
                    P2 = P1
                    y2 = Y1
                    Val2 = Val1
                    Do While Not valtbl(sCtry)(idx - 1).HasValue _
            Or y2 - Years(idx - 1) < MinRange
                        'if doing interpolation overrule MinRange
                        'checking valid year
                        If valtbl(sCtry)(idx - 1).HasValue Then
                            'checking interpolation
                            If y2 > iYear And Years(idx - 1) < iYear Then
                                Exit Do
                            End If
                        End If

                        idx = idx - 1
                        If idx < 1 Then
                            Exit Do
                        ElseIf y2 - Years(idx - 1) > MaxRange Then
                            idx = 0
                            Exit Do
                        End If
                    Loop
                    P1 = idx - 1
                    If P1 < 0 Then
                        estimateHistory = valMR
                        Exit Function
                    End If
                    Y1 = Years(P1)
                    Val1 = valtbl(sCtry)(P1)
                    estimateHistory = Val1 + (Val2 - Val1) / (y2 - Y1) * (iYear - Y1)
                    Exit Function
                End If
            End If

            'look for a point in the past
            For i = idx - 1 To 0 Step -1
                If valtbl(sCtry)(i).HasValue Then
                    P1 = i
                    Y1 = Years(i)
                    Val1 = valtbl(sCtry)(i)
                    Exit For
                End If
            Next i
            'look for a point in the future
            For i = idx + 1 To Years.Count - 1
                'If Not IsNumeric(valtbl.Fields(i).Name) Then
                '    Exit For
                'End If
                'check MaxRange
                If Y1 > 0 And Years(i) - Y1 > MaxRange Then
                    Exit For
                End If

                If valtbl(sCtry)(i).HasValue Then
                    P2 = i
                    y2 = Years(i)
                    Val2 = valtbl(sCtry)(i)
                    Exit For
                End If
            Next i
            'If we have 2 points we can interpolate
            If P1 > 0 And P2 > 0 Then
                estimateHistory = Val1 + (Val2 - Val1) / (y2 - Y1) * (iYear - Y1)
                Exit Function
            End If
            'If we have NO points there's nothing we can do
            If P1 < 0 And P2 < 0 Then
                Exit Function
            End If
            'If we don't have p1 we have to extrapolate using the future
            If P1 < 0 Then
                P1 = P2
                Y1 = y2
                Val1 = Val2
                P2 = -1
                'look for a point in the future
                For i = P1 + 1 To Years.Count - 1
                    'If Not IsNumeric(valtbl.Fields(i).Name) Then
                    '    Exit For
                    'End If
                    'check MaxRange
                    If Years(i) - Y1 > MaxRange Then
                        Exit For
                    End If

                    If valtbl(sCtry)(i).HasValue _
            And Years(i) - Y1 >= MinRange Then
                        P2 = i
                        y2 = Years(i)
                        Val2 = valtbl(sCtry)(i)
                        Exit For
                    End If
                Next i
                'If we have 2 points we can extrapolate
                If P2 > 0 Then
                    estimateHistory = Val1 + (Val2 - Val1) / (y2 - Y1) * (iYear - Y1)
                Else
                    estimateHistory = valMR
                End If 'if the second point is not found there's nothing we can do
                Exit Function
            End If
            'If we don't have p2 we have to extrapolate using the past
            If P2 < 0 Then
                P2 = P1
                y2 = Y1
                Val2 = Val1
                P1 = -1
                'look for a point in the past
                For i = P2 - 1 To 0 Step -1
                    'check MaxRange
                    If y2 - Years(i) > MaxRange Then
                        Exit For
                    End If

                    If valtbl(sCtry)(i).HasValue _
            And y2 - Years(i) >= MinRange Then
                        P1 = i
                        Y1 = Years(i)
                        Val1 = valtbl(sCtry)(i)
                        Exit For
                    End If
                Next i
                'If we have 2 points we can extrapolate
                If P1 > 0 Then
                    estimateHistory = Val1 + (Val2 - Val1) / (y2 - Y1) * (iYear - Y1)
                Else
                    estimateHistory = valMR
                End If 'if the second point is not found there's nothing we can do
                Exit Function
            End If
        End Function

        'jrs 2008/07/07 copied from the GUI
        'Function to find closest smaller year field in table (where fields are sorted)
        'this function assumes that sYr doesn't exist in the table
        Friend Function findClosestSmallerYearIdx(ByVal firstYidx As Integer, ByVal lastYidx As Integer, ByRef valtbl As IDictionary(Of String, Single?()),
    ByRef Years As Short(), ByVal sCtry As String, ByRef iYr As Integer, lastYidxTbl As Integer) As Integer 'jrs 2008/05/13 added lastYidxTbl
            'value not found
            If firstYidx > lastYidx Then
                findClosestSmallerYearIdx = -1
                Exit Function
            End If

            Dim idx As Integer

            'if value out of range assign the extremes
            If iYr < Years(0) Then 'jrs 2008/05/13 change from firtYidx to 2
                idx = 0
                Do While Not valtbl(sCtry)(idx).HasValue
                    idx = idx + 1
                    If idx > lastYidxTbl Then 'jrs 2008/05/13 from lastYidx to lastYidxTbl
                        idx = -1
                        Exit Do
                    End If
                Loop
                findClosestSmallerYearIdx = idx
                Exit Function
            ElseIf iYr > Years(lastYidxTbl) Then 'jrs 2008/05/13 from lastYidx to lastYidxTbl
                idx = lastYidxTbl
                Do While Not valtbl(sCtry)(idx).HasValue
                    idx = idx - 1
                    If idx < 2 Then
                        idx = -1
                        Exit Do
                    End If
                Loop
                findClosestSmallerYearIdx = idx
                Exit Function
            End If

            'divide and conquer
            Dim midPt As Integer
            midPt = CInt((lastYidx + firstYidx) / 2)
            If iYr < Years(midPt) Then
                findClosestSmallerYearIdx = findClosestSmallerYearIdx(firstYidx, midPt - 1, valtbl, Years, sCtry, iYr, lastYidxTbl)
                Exit Function
            End If
            If iYr > Years(midPt) Then
                If midPt + 1 <= lastYidx Then
                    If iYr < Years(midPt + 1) Then
                        idx = midPt
                        Do While Not valtbl(sCtry)(idx).HasValue
                            idx = idx - 1
                            If idx < 2 Then
                                'start looking forward
                                idx = midPt + 1
                                Do While Not valtbl(sCtry)(idx).HasValue
                                    idx = idx + 1
                                    'jrs 2008/05/12 lastYidx should be last year in the full table not in this divide and conquer portion
                                    'If idx > lastYidx Then
                                    If idx > lastYidxTbl Then
                                        idx = -1
                                        Exit Do
                                    End If
                                Loop
                                Exit Do
                            End If
                        Loop
                        findClosestSmallerYearIdx = idx
                        Exit Function
                    End If
                Else
                    idx = midPt
                    Do While Not valtbl(sCtry)(idx).HasValue
                        idx = idx - 1
                        If idx < 2 Then
                            'start looking forward
                            idx = midPt + 1
                            Do While Not valtbl(sCtry)(idx).HasValue
                                idx = idx + 1
                                'jrs 2008/05/12 lastYidx should be last year in the full table not in this divide and conquer portion
                                'If idx > lastYidx Then
                                If idx > lastYidxTbl Then
                                    idx = -1
                                    Exit Do
                                End If
                            Loop
                            Exit Do
                        End If
                    Loop
                    findClosestSmallerYearIdx = idx
                    Exit Function
                End If
                findClosestSmallerYearIdx = findClosestSmallerYearIdx(midPt + 1, lastYidx, valtbl, Years, sCtry, iYr, lastYidxTbl)
                Exit Function
            End If
            findClosestSmallerYearIdx = -1 'value not found
        End Function

        'jrs 2010 / 11 / 26
        'Originally created by Mohammod Irfan, moved here to be publicly accessible
        'calculates annual (compounding) growth rate (0 to 1) of a variable over an interval using an IFsHistSeries table
        Friend Function getAnnualGrowthRatePeriodAvgExtended(ByRef tblPhys As IDictionary(Of String, Single?()), ByRef Yrs As Short(),
                                                                ByVal sCtry As String, ByVal iPeriodEnd As Integer,
        ByRef iInitYrUsed As Short?, ByRef ifinYrUsed As Short?, ByRef ValUsed As Single?,
        Optional ByVal MaxGrowthRate As Single = 0.1, Optional ByVal MinGrowthRate As Single = 0.0005,
        Optional ByVal grintrvalMin As Integer = 2, Optional ByVal grintrvalMax As Integer = 5,
        Optional ByVal MaxBackUp As Integer = 10, Optional ByVal MaxForward As Integer = 3, Optional ByVal IsAbsolute As Boolean = False,
        Optional ContinueBackUp As Boolean = False,
        Optional ByVal iPeriodInit As Integer = 9999,
        Optional ByVal MaxBackUpEnd As Integer = 3, Optional ByVal MaxForwardEnd As Integer = 3,
        Optional ByVal MaxBackUpInit As Integer = 3, Optional ByVal MaxForwardInit As Integer = 3) As Single?


            'mti 2016/11/02 New Proc



            'Parameters:
            '1. tblPhy is the table to be used to find the Growth rate
            '2. iPeriodEnd is the end of the period for which growth is calculated
            '3. MaxGrowthRate is the Maximum value this function can return
            '4. MinGrowthRate is the Minimum value this function can return, added by jrs
            '5. grintrvalMin is the minimum number of years in the interval to compute growth
            '(using 2 avoids using the previous year from the first year with data)
            '5. grintrvalMax is the maximum number of years in the interval to compute growth
            'avoids using too distant points (if iPeriodEnd is Nothing then we use 2*grintrvalMax)
            '6. MaxBackUp is the maximum number of years to go back from iPeriodEnd
            '(currenctly iPeriodEnd - MaxBackUp is not being used but exits the function)
            '7. MaxForward is the maximum number of years to go forward from iPeriodEnd
            '(Years > iPeriodEnd + MaxForward are ignored)
            '8. IsAbsolute, when true, makes the function calaculate the average annual absolute change (as opposed to a growth rate) over the interval


            Dim i As Integer
            Dim Val_Current As Single?, val_prev As Single?, Yr_Current As Short?, yr_prev As Short?
            Val_Current = Nothing
            val_prev = Nothing

            Yr_Current = Nothing
            yr_prev = Nothing

            getAnnualGrowthRatePeriodAvgExtended = Nothing

            iInitYrUsed = Nothing
            ifinYrUsed = Nothing
            ValUsed = Nothing


            'jrs 2010/11/26
            'Validations
            '-----------
            'MaxGrowthRate has to be higher than MinGrowthRate
            If MinGrowthRate > MaxGrowthRate Then
                'MsgBox "Max Growth rate has to be higher than Min Growth rate", vbCritical, "get Annual Growth Rate"
                Debug.Write("Max Growth rate has to be higher than Min Growth rate")
                Exit Function
            End If
            If grintrvalMin > grintrvalMax Then
                'MsgBox "Max Growth rate interval has to be higher than Min Growth rate interval", vbCritical, "get Annual Growth Rate"
                Debug.Write("Max Growth rate interval has to be higher than Min Growth rate interval")
                Exit Function
            End If

            'Limits need to be positive
            If grintrvalMin < 1 Then
                'MsgBox "Growth Interval needs to be higher than 0", vbCritical, "get Annual Growth Rate"
                Debug.Write("Growth Interval needs to be higher than 0")
                Exit Function
            End If
            If MaxBackUp < 2 Then
                'MsgBox "Max number of years to look before " & iPeriodEnd & " needs to be higher than 1", vbCritical, "get Annual Growth Rate"
                Debug.Write("Max number of years to look before " & iPeriodEnd & " needs to be higher than 1")
                Exit Function
            End If
            If MaxForward < 0 Then
                'MsgBox "Max number of years to look after " & iPeriodEnd & " can't be negative", vbCritical, "get Annual Growth Rate"
                Debug.Write("Max number of years to look after " & iPeriodEnd & " can't be negative")
                Exit Function
            End If

            'Trapping General Errors
            '-----------------------
            Dim This_Yr As Short, DataVal As Single?, Yr_No As Short
            Dim RatChange As Single?, DiffTime As Short?
            For i = 0 To Yrs.Count - 1
                This_Yr = Yrs(i)
                'If This_Yr <= BaseYear Then
                DataVal = tblPhys(sCtry)(i)
                Yr_No = This_Yr

                'Stop readinging further forward
                '                    If Yr_No >= iPeriodEnd + MaxForward Then Exit For

                'Stop reading further back
                If Yr_No <= iPeriodEnd - MaxBackUp Then Exit For

                '                    If IsNull(DataVal) Then Stop

                If DataVal.HasValue Then
                    'mti June 08, 2011 start
                    'allow 0 when calculating absolute change
                    If DataVal <> 0 Or IsAbsolute Then
                        'mti June 08, 2011 -end
                        If Yrs(i) <= (iPeriodEnd + MaxForward) Then 'mti - replace Int(.Fields(i).Name) with Yr_No
                            If Not Val_Current.HasValue Then
                                If Yr_No < iPeriodEnd - MaxBackUpEnd Then Exit For  'end value too far back in time
                                If Yr_No < iPeriodEnd + MaxForwardEnd Then
                                    Val_Current = DataVal  'End value
                                    Yr_Current = Yr_No
                                End If
                            Else
                                'Only when data increases, not when there are drops
                                'this avoids returning negatives

                                'mti June 08, 2011 start - allow negative growth rates
                                'If DataVal <= Val_Current Then
                                If DataVal <= Val_Current Or MinGrowthRate < 0 Then
                                    'mti June 08, 2011 end - allow negative growth rates


                                    If Yr_No >= iPeriodInit - MaxBackUpInit And Yr_No <= iPeriodInit + MaxForwardInit Then
                                        val_prev = DataVal 'initial value
                                        yr_prev = Yr_No
                                    End If

                                    If Yr_Current - yr_prev >= grintrvalMin Then

                                        'jrs 2010/11/26
                                        'MTI - this is JSR code - what's the purpose?
                                        If Yr_Current - yr_prev > grintrvalMax And Yr_Current >= iPeriodEnd Then Exit Function
                                        If Yr_Current - yr_prev > 2 * grintrvalMax Then Exit Function

                                        'mti May 05, 2011 - start
                                        If IsAbsolute Then


                                            'If YR_current >= iPeriodInit - MaxBackUpInit And YR_current <= iPeriodInit + MaxForwardInit Then

                                            'average absolute growth over the interval (as opposed to growth rate)
                                            RatChange = Val_Current - val_prev
                                            DiffTime = Yr_Current - yr_prev

                                            getAnnualGrowthRatePeriodAvgExtended = RatChange / DiffTime


                                            iInitYrUsed = yr_prev
                                            ifinYrUsed = Yr_Current
                                            ValUsed = val_prev

                                            If iInitYrUsed = iPeriodInit Then Exit For  'found the initial year

                                            'End If

                                            'mti Sep 12, 2011 - start
                                            '0 or - should be OK for absolute change (at least for pre-processor
                                            'If getAnnualGrowthRatePeriodAvg < 0 Then
                                            '    getAnnualGrowthRatePeriodAvg = Nothing
                                            'End If
                                            'mti Sep 12, 2011 - end

                                            'mti 2016/06/14
                                            'Exit For
                                            If Not ContinueBackUp Then Exit For

                                        Else
                                            'mti May 05, 2011  -end

                                            'If YR_current >= iPeriodInit - MaxBackUpInit And YR_current <= iPeriodInit + MaxForwardInit Then
                                            RatChange = Val_Current / val_prev
                                            DiffTime = Yr_Current - yr_prev

                                            'compounded growth rate
                                            'jrs 2010/11/26 getAnnualGrowthRatePeriodAvg = AMAX(0.0005, RatChange ^ (1 / DiffTime) - 1)

                                            'mti June 14, 2012 - start
                                            getAnnualGrowthRatePeriodAvgExtended = CSng(RatChange ^ (1 / DiffTime) - 1)

                                            If getAnnualGrowthRatePeriodAvgExtended > MinGrowthRate Or Yr_No <= iPeriodEnd - MaxBackUp + 1 Then 'proceed

                                                'getAnnualGrowthRatePeriodAvg = AMAX(MinGrowthRate, RatChange ^ (1 / DiffTime) - 1)
                                                getAnnualGrowthRatePeriodAvgExtended = AMAX(MinGrowthRate, getAnnualGrowthRatePeriodAvgExtended)


                                                If getAnnualGrowthRatePeriodAvgExtended < MaxGrowthRate * 1.5 Or Yr_No <= iPeriodEnd - MaxBackUp + 1 Then
                                                    'let go if not very far from Maxgrowthrate or all years read
                                                    getAnnualGrowthRatePeriodAvgExtended = Amin(getAnnualGrowthRatePeriodAvgExtended, MaxGrowthRate)
                                                    Exit For
                                                Else
                                                    getAnnualGrowthRatePeriodAvgExtended = Amin(getAnnualGrowthRatePeriodAvgExtended, MaxGrowthRate)
                                                    'continue to read data
                                                End If
                                            Else
                                                getAnnualGrowthRatePeriodAvgExtended = AMAX(MinGrowthRate, getAnnualGrowthRatePeriodAvgExtended)
                                                'continue to read data
                                            End If

                                            iInitYrUsed = yr_prev
                                            ifinYrUsed = Yr_Current
                                            ValUsed = val_prev

                                            If iInitYrUsed = iPeriodInit Then Exit For  'found the initial year

                                            'End If

                                            If Not ContinueBackUp Then Exit For

                                        End If



                                    End If 'Yr_Current - Yr_Prev >= grintrval Then
                                End If 'Dataval < Val_Current
                            End If ' IsNull(Val_Current) Then
                        End If 'Int(.Fields(i).Name) <= (iPeriodEnd + MaxForward) Then
                    End If 'dataval<>0
                End If  'Not IsNull(Dataval) Then
                'End If 'baseyear and back
            Next i
        End Function

        'mti 2013/11/14
        'function to retrieve a given year if it exists, if not returns mostrecent
        Friend Function getYearOrEarliestWithinRange(ByRef tbl As IDictionary(Of String, Single?()), ByRef years As Short(), ByVal sCtry As String, ByVal iYr As Integer, ByVal iYrRange As Integer) As Single?
            Dim i As Integer
            Dim CurYearVal As Short
            Dim ReadVal As Single?

            getYearOrEarliestWithinRange = Nothing
            'the last 2 columns should be Earliest and MostRecent

            For i = 0 To years.Count - 1
                CurYearVal = years(i)
                If CurYearVal > iYr - iYrRange And CurYearVal < iYr Then 'do nothing
                    'pick non-null values earlier than target year, but within range
                    ReadVal = tbl(sCtry)(i)
                    If ReadVal.HasValue Then getYearOrEarliestWithinRange = ReadVal
                ElseIf CurYearVal <= iYr + iYrRange Then
                    ReadVal = tbl(sCtry)(i)
                    If ReadVal.HasValue Then
                        getYearOrEarliestWithinRange = ReadVal
                        'found non-null values at target year or a later year within range
                        Exit Function
                    End If
                Else
                    'beyond range, e.g., 1960+10 - so exit function
                    Exit Function
                End If
            Next i
        End Function

        'jrs 2018/12/17
        Friend Function getYearIdx(ByRef years As Short(), ByVal YrName As Short) As Short
            getYearIdx = -1
            Dim i As Short
            For i = CShort(years.Count - 1) To 0 Step -1
                If years(i) = YrName Then
                    getYearIdx = i
                    Exit Function
                End If
            Next
        End Function

        Friend Function getYearOrMstRct(ByRef tbl As IDictionary(Of String, Single?()), ByRef years As Short(), ByVal sCtry As String, ByVal iYr As Integer) As Single?
        	'gvdb 2024/04/05
            ' unused variables
            'Dim i As Integer
            'Dim CurYearVal As Short
            'Dim ReadVal As Single?

            getYearOrMstRct = Nothing
            'the last 2 columns should be Earliest and MostRecent

            Dim YrIdx As Short
            YrIdx = getYearIdx(years, CShort(iYr))
            If YrIdx >= 0 Then
                getYearOrMstRct = tbl(sCtry)(YrIdx)
                If getYearOrMstRct.HasValue Then
                    Exit Function
                End If
            End If

            Dim MRYrIdx As Short?
            MRYrIdx = getYearIdxMstRctWData(tbl, sCtry, years)
            If MRYrIdx.HasValue Then
                YrIdx = CShort(MRYrIdx)
                If YrIdx >= 0 Then
                    getYearOrMstRct = tbl(sCtry)(YrIdx)
                End If
            End If
        End Function

        Friend Sub BasePutAnnualGrowthTwoDim(sVar As String, Vals As IDictionary(Of Integer, Single?), cntx As IRebuildFacade, sDim2 As String)

            Using db As IFs2Context = New IFs2Context()
                'db.Clear(GetType(AnnualGrowthRates))
                Dim NR As Short = CShort(cntx.GetDimension(1).Buckets.Count)
                Dim i As Short
                If cntx.HistoricMode Then
                    For i = 1 To NR
                        Dim row As AnnualGrowthRatesHist = New AnnualGrowthRatesHist
                        row.VarName = sVar
                        row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                        row.Dim2 = sDim2
                        row.Dim3 = ""
                        row.GrowthRate = CSng(Vals(i))
                        db.AnnualGrowthRatesHist.Add(row)
                    Next
                Else
                    For i = 1 To NR
                        Dim row As AnnualGrowthRates = New AnnualGrowthRates
                        row.VarName = sVar
                        row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                        row.Dim2 = sDim2
                        row.Dim3 = ""
                        row.GrowthRate = CSng(Vals(i))
                        db.AnnualGrowthRates.Add(row)
                    Next
                End If
                db.Flush()
            End Using
        End Sub

        Friend Sub BasePutAnnualGrowthThreeDim(sVar As String, Vals As IDictionary(Of Integer, Single?), cntx As IRebuildFacade, sDim2 As String, sDim3 As String)
            'jrs 2022/10/27
            If Vals.Count = 0 Then Exit Sub
            Using db As IFs2Context = New IFs2Context()
                'db.Clear(GetType(AnnualGrowthRates))
                Dim NR As Short = CShort(cntx.GetDimension(1).Buckets.Count)
                Dim i As Short
                If cntx.HistoricMode Then
                    For i = 1 To NR
                        Dim row As AnnualGrowthRatesHist = New AnnualGrowthRatesHist
                        row.VarName = sVar
                        row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                        row.Dim2 = sDim2
                        row.Dim3 = sDim3
                        row.GrowthRate = CSng(Vals(i))
                        db.AnnualGrowthRatesHist.Add(row)
                    Next
                Else
                    For i = 1 To NR
                        Dim row As AnnualGrowthRates = New AnnualGrowthRates
                        row.VarName = sVar
                        row.RegName = cntx.GetDimension(1).Buckets.Item(i)
                        row.Dim2 = sDim2
                        row.Dim3 = sDim3
                        row.GrowthRate = CSng(Vals(i))
                        db.AnnualGrowthRates.Add(row)
                    Next
                End If
                db.Flush()
            End Using
        End Sub

        'mti Sept 04, 2009
        Public Function CalcNormalCDF(X As Double, Optional M As Double = 0, Optional S As Double = 1) As Double
            'Calculate Cumulative Distribution Function of a normal distribution (mean m, standard deviation s) at the point x


            'CDF (x) = (1 / (s * Sqrt(2 * Pi))) * Integ(Exp(-(X - m) ^ 2 / (2 * s ^ 2)) * dx)

            ' The above formula can be converted to the error function formulation (please
            '  see the wikipedia article at http://en.wikipedia.org/wiki/Normal_distribution

            ' CDF (x) = .5 * (1# + erf((x - m) / (Sqr(2) * s)))

            Try
                If S <= 0 Then
                    CalcNormalCDF = Nothing
                    Exit Function
                End If

                X = CDbl(X)
                M = CDbl(M)
                S = CDbl(S)

                'use DERF (double precision error function) calculation from Slatec library
                CalcNormalCDF = 0.5 * (1.0# + DERF((X - M) / (Sqrt(2) * S)))

            Catch ex As Exception
                CalcNormalCDF = Nothing
            End Try


        End Function

        Public Function CalcLogNormalCDF(X As Single, M As Single?, S As Single?) As Single?
            'Calculate Cumulative Distribution Function where the logarithm of the variable is normally distributed

            'Log of x is normally distributed with a mean m and standard deviation s
            'We find the CDF integral at log (x) using CalcNormalCDF routine

            Try
                CalcLogNormalCDF = CSng(CalcNormalCDF(Log(CDbl(X)), CDbl(M), CDbl(S)))
            Catch ex As Exception
                CalcLogNormalCDF = Nothing
            End Try

        End Function

        Friend Function CalcNormalInverse(P As Single?, Optional M As Single = 0, Optional S As Single = 1) As Single
            'Inverse Normal Cumulative Distribution Function
            'mti Oct 01, 2009 - take whole proc

            Try
                Dim LowVal As Double, HighVal As Double, Epsilon As Double, MidVal As Double, itir_cnt%

                CalcNormalInverse = Nothing

                'make sure probability and standard deviation has valid values
                If P < 0 Or P > 1 Or S <= 0 Then
                    Exit Function
                End If

                LowVal = -S * 10.0# + M
                HighVal = S * 10.0# + M

                'range of precision
                Epsilon = D1MACH(4)

                For itir_cnt = 1 To 256
                    'midpoint
                    MidVal = (LowVal + HighVal) / 2.0#

                    'test midpoint
                    If CalcNormalCDF(MidVal, M, S) < P Then
                        LowVal = MidVal
                    Else
                        HighVal = MidVal
                    End If


                    'exit if end of precision range reached
                    If HighVal = 0 Then
                        If HighVal - LowVal < Epsilon Then
                            CalcNormalInverse = CSng(MidVal)
                            Exit Function
                        End If
                    Else
                        If Abs((HighVal - LowVal) / HighVal) < Epsilon Then
                            CalcNormalInverse = CSng(MidVal)
                            Exit Function
                        End If
                    End If
                Next itir_cnt

            Catch ex As Exception
                CalcNormalInverse = Nothing
            End Try


        End Function

        'Support statistical functions from SLATEC library.

        ''Wrapped
        Public Function DERF(
    ByRef X As Double
    ) As Double

            '  DECK DERF
            '  ***BEGIN PROLOGUE  DERF
            '  ***PURPOSE  Compute the error function.
            '  ***LIBRARY   SLATEC (FNLIB)
            '  ***CATEGORY  C8A, L5A1E
            '  ***TYPE      DOUBLE PRECISION (ERF-S, DERF-D)
            '  ***KEYWORDS  ERF, ERROR FUNCTION, FNLIB, SPECIAL FUNCTIONS
            '  ***AUTHOR  Fullerton, W., (LANL)
            '  ***DESCRIPTION
            '
            '   DERF(X) calculates the double precision error function for double
            '   precision argument X.
            '
            '   Series for ERF        on the interval  0.          to  1.00000E+00
            '                                          with weighted error   1.28E-32
            '                                           log weighted error  31.89
            '                                 significant figures required  31.05
            '                                      decimal places required  32.55
            '
            '  ***REFERENCES  (NONE)
            '  ***ROUTINES CALLED  D1MACH, DCSEVL, DERFC, INITDS
            '  ***REVISION HISTORY  (YYMMDD)
            '     770701  DATE WRITTEN
            '     890531  Changed all specific intrinsics to generic.  (WRB)
            '     890531  REVISION DATE from Version 3.2
            '     891214  Prologue converted to Version 4.0 format.  (BAB)
            '     900727  Added EXTERNAL statement.  (WRB)
            '     920618  Removed space from variable name.  (RWC, WRB)
            '  ***END PROLOGUE  DERF
            '  ***FIRST EXECUTABLE STATEMENT  DERF

            Static ERFCS#(0 To 21), SQEPS#, SQRTPI#, XBIG#, Y#
            Static NTERF&
            Dim RTN_VAL#

            Static bInitDone As Boolean
            If Not bInitDone Then
                ERFCS(1) = -0.0490461212346918
                ERFCS(2) = -0.142261205103714
                ERFCS(3) = 0.0100355821875998
                ERFCS(4) = -0.000576876469976749
                ERFCS(5) = 0.0000274199312521961
                ERFCS(6) = -0.00000110431755073445
                ERFCS(7) = 0.000000038488755420345
                ERFCS(8) = -0.00000000118085825338755
                ERFCS(9) = 0.0000000000323342158260509
                ERFCS(10) = -0.000000000000799101594700455
                ERFCS(11) = 0.0000000000000179907251139615
                ERFCS(12) = -0.000000000000000371863548781869
                ERFCS(13) = 7.10359900371425E-18
                ERFCS(14) = -1.26124551191552E-19
                ERFCS(15) = 2.09164069417693E-21
                ERFCS(16) = -3.25397310293141E-23
                ERFCS(17) = 4.76686720979767E-25
                ERFCS(18) = -6.59801207828513E-27
                ERFCS(19) = 8.65501146996376E-29
                ERFCS(20) = -1.07889251774981E-30
                ERFCS(21) = 1.2811883993017E-32

                SQRTPI = 1.77245385090552

                NTERF = INITDS(ERFCS, 21, CSng(0.1 * REAL(CSng(D1MACH(3)))))
                XBIG = SQRT_(-LOG_(SQRTPI * D1MACH(3)))
                SQEPS = SQRT_(2.0# * D1MACH(3))

                bInitDone = True
            End If

            Y = ABS_(X)
            If Y > 1.0# Then GoTo 20
            ''
            ''   ERF(X) = 1.0 - ERFC(X)  FOR  -1.0 .LE. X .LE. 1.0
            ''
            If Y <= SQEPS Then RTN_VAL = 2.0# * X * X / SQRTPI
            If Y > SQEPS Then RTN_VAL = X * (1.0# + DCSEVL(2.0# * X * X - 1.0#, ERFCS, NTERF))
            DERF = RTN_VAL
            Exit Function
            ''
            ''   ERF(X) = 1.0 - ERFC(X) FOR ABS(X) .GT. 1.0
            ''
20:
            If Y <= XBIG Then RTN_VAL = SIGN_(1.0# - DERFC(Y), X)
            If Y > XBIG Then RTN_VAL = SIGN_(1.0#, X)
            ''
            DERF = RTN_VAL
        End Function

        Public Function D1MACH(
    ByVal vI As Byte
    ) As Double
            'Return floating point machine dependent constants.


            'D1MACH can be used to obtain machine-dependent parameters for the
            ' local machine environment.  It is a function subprogram with one
            ' (input) argument, and can be referenced as follows:
            '     D = D1Mach(I)
            'where I=1,...,5.  The (output) value of D above is determined by
            ' the (input) value of I.  The results for various values of I are
            ' discussed below.

            '  D1MACH( 1) = B**(EMIN-1), the smallest positive magnitude.
            '  D1MACH( 2) = B**EMAX*(1 - B**(-T)), the largest magnitude.
            '  D1MACH( 3) = B**(-T), the smallest relative spacing.
            '  D1MACH( 4) = B**(1-T), the largest relative spacing.
            '  D1MACH( 5) = LOG10(B)

            Static bInitDone As Boolean
            Static DMach(0 To 5) As Double
            If Not bInitDone Then

                DMach(1) = I1MACH(10) ^ (I1MACH(15) - 1)
                DMach(2) = 1.79769313486231E+308
                DMach(3) = I1MACH(10) ^ (-I1MACH(14))
                DMach(4) = I1MACH(10) ^ (1 - I1MACH(14))
                DMach(5) = Log(I1MACH(10)) / Log(10)

                bInitDone = True
            End If

            D1MACH = DMach(vI)

            'Assume double precision numbers are represented in the T-digit, base-B form
            '     sign (B**E)*( (X(1)/B) + ... + (X(T)/B**T) )
            'where 0 <= X(I) < B for I=1,...,T, 0 .LT. X(1), and EMIN <= E <= EMAX.

            'The values of B, T, EMIN and EMAX are provided in I1MACH as follows:
            '  I1MACH(10) = B, the base.
            '  I1MACH(14) = T, the number of base-B digits.
            '  I1MACH(15) = EMIN, the smallest exponent E.
            '  I1MACH(16) = EMAX, the largest exponent E.

            'References:
            ' P. A. Fox, A. D. Hall and N. L. Schryer, Framework for a portable library,
            ' ACM Transactions on Mathematical Software 4, 2 (June 1978), pp. 177-188.
            'Authors:
            '  Fox, P. A., (Bell Labs)
            '  Hall, A. D., (Bell Labs)
            '  Schryer, N. L., (Bell Labs)
        End Function



        Public Function DCSEVL(
      ByRef X As Double _
    , ByRef CS() As Double _
    , ByRef N As Long
    ) As Double

            '  DECK DCSEVL
            '  ***BEGIN PROLOGUE  DCSEVL
            '  ***PURPOSE  Evaluate a Chebyshev series.
            '  ***LIBRARY   SLATEC (FNLIB)
            '  ***CATEGORY  C3A2
            '  ***TYPE      DOUBLE PRECISION (CSEVL-S, DCSEVL-D)
            '  ***KEYWORDS  CHEBYSHEV SERIES, FNLIB, SPECIAL FUNCTIONS
            '  ***AUTHOR  Fullerton, W., (LANL)
            '  ***DESCRIPTION
            '
            '    Evaluate the N-term Chebyshev series CS at X.  Adapted from
            '    a method presented in the paper by Broucke referenced below.
            '
            '         Input Arguments --
            '    X    value at which the series is to be evaluated.
            '    CS   array of N terms of a Chebyshev series.  In evaluating
            '         CS, only half the first coefficient is summed.
            '    N    number of terms in array CS.
            '
            '  ***REFERENCES  R. Broucke, Ten subroutines for the manipulation of
            '                   Chebyshev series, Algorithm 446, Communications of
            '                   the A.C.M. 16, (1973) pp. 254-256.
            '                 L. Fox and I. B. Parker, Chebyshev Polynomials in
            '                   Numerical Analysis, Oxford University Press, 1968,
            '                   page 56.
            '  ***ROUTINES CALLED  D1MACH, RaiseError
            '  ***REVISION HISTORY  (YYMMDD)
            '     770401  DATE WRITTEN
            '     890831  Modified array declarations.  (WRB)
            '     890831  REVISION DATE from Version 3.2
            '     891214  Prologue converted to Version 4.0 format.  (BAB)
            '     900315  CALLs to XERROR changed to CALLs to RaiseError.  (THJ)
            '     900329  Prologued revised extensively and code rewritten to allow
            '             X to be slightly outside interval (-1,+1).  (WRB)
            '     920501  Reformatted the REFERENCES section.  (WRB)
            '  ***END PROLOGUE  DCSEVL
            '  ***FIRST EXECUTABLE STATEMENT  DCSEVL

            Static B0#, B1#, B2#, ONEPL#, TWOX#
            Dim RTN_VAL#

            Static bInitDone As Boolean
            If Not bInitDone Then
                ONEPL = 1.0# + D1MACH(4)

                bInitDone = True
            End If

            If N < 1 Then RaiseError(30090, "SLATEC", "DCSEVL", "NUMBER OF TERMS .LE. 0", 2, 2)
            If N > 1000 Then RaiseError(30091, "SLATEC", "DCSEVL", "NUMBER OF TERMS .GT. 1000", 3, 2)
            If ABS_(CSng(X)) > ONEPL Then RaiseError(30092, "SLATEC", "DCSEVL", "X OUTSIDE THE INTERVAL (-1,+1)", 1, 1)
            ''
            B1 = 0#
            B0 = 0#
            TWOX = 2.0# * X
            Static i&
            For i = 1 To N
                B2 = B1
                B1 = B0
                Static NI&
                NI = N + 1 - i
                B0 = TWOX * B1 - B2 + CS(CInt(NI))
10:
            Next i
            ''
            RTN_VAL = 0.5 * (B0 - B2)
            ''
            DCSEVL = RTN_VAL
        End Function



        Public Function DERFC(
    ByRef X As Double
    ) As Double

            '  DECK DERFC
            '  ***BEGIN PROLOGUE  DERFC
            '  ***PURPOSE  Compute the complementary error function.
            '  ***LIBRARY   SLATEC (FNLIB)
            '  ***CATEGORY  C8A, L5A1E
            '  ***TYPE      DOUBLE PRECISION (ERFC-S, DERFC-D)
            '  ***KEYWORDS  COMPLEMENTARY ERROR FUNCTION, ERFC, FNLIB,
            '               SPECIAL FUNCTIONS
            '  ***AUTHOR  Fullerton, W., (LANL)
            '  ***DESCRIPTION
            '
            '   DERFC(X) calculates the double precision complementary error function
            '   for double precision argument X.
            '
            '   Series for ERF        on the interval  0.          to  1.00000E+00
            '                                          with weighted Error   1.28E-32
            '                                           log weighted Error  31.89
            '                                 significant figures required  31.05
            '                                      decimal places required  32.55
            '
            '   Series for ERC2       on the interval  2.50000E-01 to  1.00000E+00
            '                                          with weighted Error   2.67E-32
            '                                           log weighted Error  31.57
            '                                 significant figures required  30.31
            '                                      decimal places required  32.42
            '
            '   Series for ERFC       on the interval  0.          to  2.50000E-01
            '                                          with weighted error   1.53E-31
            '                                           log weighted error  30.82
            '                                 significant figures required  29.47
            '                                      decimal places required  31.70
            '
            '  ***REFERENCES  (NONE)
            '  ***ROUTINES CALLED  D1MACH, DCSEVL, INITDS, RaiseError
            '  ***REVISION HISTORY  (YYMMDD)
            '     770701  DATE WRITTEN
            '     890531  Changed all specific intrinsics to generic.  (WRB)
            '     890531  REVISION DATE from Version 3.2
            '     891214  Prologue converted to Version 4.0 format.  (BAB)
            '     900315  CALLs to XERROR changed to CALLs to RaiseError.  (THJ)
            '     920618  Removed space from variable names.  (RWC, WRB)
            '  ***END PROLOGUE  DERFC
            '  ***FIRST EXECUTABLE STATEMENT  DERFC

            Static ERFCS#(0 To 21), ERFCCS#(0 To 59), ERC2CS#(0 To 49), SQEPS#, SQRTPI#, XMAX#, TXMAX#, XSML#, Y#
            Static NTERF&, NTERFC&, NTERC2&
            Dim RTN_VAL#

            Static bInitDone As Boolean
            If Not bInitDone Then
                ERFCS(1) = -0.0490461212346918
                ERFCS(2) = -0.142261205103714
                ERFCS(3) = 0.0100355821875998
                ERFCS(4) = -0.000576876469976749
                ERFCS(5) = 0.0000274199312521961
                ERFCS(6) = -0.00000110431755073445
                ERFCS(7) = 0.000000038488755420345
                ERFCS(8) = -0.00000000118085825338755
                ERFCS(9) = 0.0000000000323342158260509
                ERFCS(10) = -0.000000000000799101594700455
                ERFCS(11) = 0.0000000000000179907251139615
                ERFCS(12) = -0.000000000000000371863548781869
                ERFCS(13) = 7.10359900371425E-18
                ERFCS(14) = -1.26124551191552E-19
                ERFCS(15) = 2.09164069417693E-21
                ERFCS(16) = -3.25397310293141E-23
                ERFCS(17) = 4.76686720979767E-25
                ERFCS(18) = -6.59801207828513E-27
                ERFCS(19) = 8.65501146996376E-29
                ERFCS(20) = -1.07889251774981E-30
                ERFCS(21) = 1.2811883993017E-32

                ERC2CS(1) = -0.0696013466023095
                ERC2CS(2) = -0.0411013393626209
                ERC2CS(3) = 0.00391449586668963
                ERC2CS(4) = -0.000490639565054898
                ERC2CS(5) = 0.0000715747900137704
                ERC2CS(6) = -0.0000115307163413123
                ERC2CS(7) = 0.000001994670590202
                ERC2CS(8) = -0.000000364266647159922
                ERC2CS(9) = 0.0000000694437261000501
                ERC2CS(10) = -0.0000000137122090210437
                ERC2CS(11) = 0.00000000278838966100714
                ERC2CS(12) = -0.000000000581416472433116
                ERC2CS(13) = 0.000000000123892049175275
                ERC2CS(14) = -0.0000000000269063914530674
                ERC2CS(15) = 0.00000000000594261435084791
                ERC2CS(16) = -0.00000000000133238673575812
                ERC2CS(17) = 0.000000000000302804680617713
                ERC2CS(18) = -0.0000000000000696664881494103
                ERC2CS(19) = 0.0000000000000162085454105392
                ERC2CS(20) = -0.00000000000000380993446525049
                ERC2CS(21) = 0.000000000000000904048781597883
                ERC2CS(22) = -0.000000000000000216400619508961
                ERC2CS(23) = 5.22210223399586E-17
                ERC2CS(24) = -1.26972960236456E-17
                ERC2CS(25) = 3.1091455042762E-18
                ERC2CS(26) = -7.6637629203203895E-19
                ERC2CS(27) = 1.90081925136275E-19
                ERC2CS(28) = -4.74220727906904E-20
                ERC2CS(29) = 1.18964920007653E-20
                ERC2CS(30) = -3.00003559032578E-21
                ERC2CS(31) = 7.60299345304325E-22
                ERC2CS(32) = -1.93590944760687E-22
                ERC2CS(33) = 4.95139912477334E-23
                ERC2CS(34) = -1.27180748133637E-23
                ERC2CS(35) = 3.28004960046951E-24
                ERC2CS(36) = -8.4923201768229E-25
                ERC2CS(37) = 2.20691789280756E-25
                ERC2CS(38) = -5.75561724569653E-26
                ERC2CS(39) = 1.50619153363923E-26
                ERC2CS(40) = -3.9545029590188E-27
                ERC2CS(41) = 1.0415297041515E-27
                ERC2CS(42) = -2.75148779527877E-28
                ERC2CS(43) = 7.29005820549756E-29
                ERC2CS(44) = -1.93693964591595E-29
                ERC2CS(45) = 5.16035711205149E-30
                ERC2CS(46) = -1.37841932219309E-30
                ERC2CS(47) = 3.69132679310707E-31
                ERC2CS(48) = -9.90938959062437E-32
                ERC2CS(49) = 2.66649170519539E-32

                ERFCCS(1) = 0.0715179310202925
                ERFCCS(2) = -0.0265324343376067
                ERFCCS(3) = 0.00171115397792086
                ERFCCS(4) = -0.000163751663458518
                ERFCCS(5) = 0.000019871293500552
                ERFCCS(6) = -0.00000284371241276656
                ERFCCS(7) = 0.000000460616130896313
                ERFCCS(8) = -0.0000000822775302587921
                ERFCCS(9) = 0.000000015921418727709
                ERFCCS(10) = -0.00000000329507136225284
                ERFCCS(11) = 0.000000000722343976040056
                ERFCCS(12) = -0.000000000166485581339873
                ERFCCS(13) = 0.0000000000401039258823766
                ERFCCS(14) = -0.0000000000100481621442573
                ERFCCS(15) = 0.00000000000260827591330033
                ERFCCS(16) = -0.000000000000699111056040402
                ERFCCS(17) = 0.000000000000192949233326171
                ERFCCS(18) = -0.0000000000000547013118875433
                ERFCCS(19) = 0.000000000000015896633097627
                ERFCCS(20) = -0.00000000000000472689398019756
                ERFCCS(21) = 0.00000000000000143587337678498
                ERFCCS(22) = -0.000000000000000444951056181736
                ERFCCS(23) = 0.000000000000000140481088476823
                ERFCCS(24) = -4.51381838776421E-17
                ERFCCS(25) = 1.47452154104513E-17
                ERFCCS(26) = -4.89262140694578E-18
                ERFCCS(27) = 1.64761214141065E-18
                ERFCCS(28) = -5.62681717632941E-19
                ERFCCS(29) = 1.94744338223208E-19
                ERFCCS(30) = -6.82630564294842E-20
                ERFCCS(31) = 2.42198888729865E-20
                ERFCCS(32) = -8.69341413350307E-21
                ERFCCS(33) = 3.15518034622809E-21
                ERFCCS(34) = -1.15737232404961E-21
                ERFCCS(35) = 4.28894716160565E-22
                ERFCCS(36) = -1.60503074205762E-22
                ERFCCS(37) = 6.0632987574538E-23
                ERFCCS(38) = -2.31140425169796E-23
                ERFCCS(39) = 8.88877854066189E-24
                ERFCCS(40) = -3.44726057665138E-24
                ERFCCS(41) = 1.34786546020697E-24
                ERFCCS(42) = -5.31179407112502E-25
                ERFCCS(43) = 2.10934105861978E-25
                ERFCCS(44) = -8.43836558792379E-26
                ERFCCS(45) = 3.39998252494521E-26
                ERFCCS(46) = -1.37945238807324E-26
                ERFCCS(47) = 5.63449031183325E-27
                ERFCCS(48) = -2.31649043447707E-27
                ERFCCS(49) = 9.58446284460181E-28
                ERFCCS(50) = -3.99072288033011E-28
                ERFCCS(51) = 1.67212922594448E-28
                ERFCCS(52) = -7.04599152276601E-29
                ERFCCS(53) = 2.97976840286421E-29
                ERFCCS(54) = -1.26252246646062E-29
                ERFCCS(55) = 5.39543870454249E-30
                ERFCCS(56) = -2.38099288253146E-30
                ERFCCS(57) = 1.09905283010276E-30
                ERFCCS(58) = -4.86771374164497E-31
                ERFCCS(59) = 1.52587726411036E-31

                SQRTPI = 1.77245385090552

                Static ETA!
                ETA = CSng(0.1 * REAL(CSng(D1MACH(3))))
                NTERF = INITDS(ERFCS, 21, ETA)
                NTERFC = INITDS(ERFCCS, 59, ETA)
                NTERC2 = INITDS(ERC2CS, 49, ETA)
                ''
                XSML = -SQRT_(-LOG_(SQRTPI * D1MACH(3)))
                TXMAX = SQRT_(-LOG_(SQRTPI * D1MACH(1)))
                XMAX = TXMAX - 0.5 * LOG_(TXMAX) / TXMAX - 0.01
                SQEPS = SQRT_(2.0# * D1MACH(3))

                bInitDone = True
            End If

            If X > XSML Then GoTo 20
            ''
            ''   ERFC(X) = 1.0 - ERF(X)  FOR  X .LT. XSML
            ''
            RTN_VAL = 2.0#
            DERFC = RTN_VAL
            Exit Function
            ''
20:
            If X > XMAX Then GoTo 40
            Y = ABS_(X) 'jrs 2020/08/21 CSng(X) using only X now
            If Y > 1.0# Then GoTo 30
            ''
            ''   ERFC(X) = 1.0 - ERF(X)  FOR ABS(X) .LE. 1.0
            ''
            If Y < SQEPS Then RTN_VAL = 1.0# - 2.0# * X / SQRTPI
            If Y >= SQEPS Then RTN_VAL = 1.0# - X * (1.0# + DCSEVL(2.0# * X * X - 1.0#, ERFCS, NTERF))
            DERFC = RTN_VAL
            Exit Function
            ''
            ''   ERFC(X) = 1.0 - ERF(X)  FOR  1.0 .LT. ABS(X) .LE. XMAX
            ''
30:
            Y = Y * Y
            If Y <= 4.0# Then RTN_VAL = EXP_(-Y) / ABS_(CSng(X)) * (0.5 + DCSEVL((8.0# / Y - 5.0#) / 3.0#, ERC2CS, NTERC2))
            If Y > 4.0# Then RTN_VAL = EXP_(-Y) / ABS_(CSng(X)) * (0.5 + DCSEVL(8.0# / Y - 1.0#, ERFCCS, NTERFC))
            If X < 0# Then RTN_VAL = 2.0# - RTN_VAL
            DERFC = RTN_VAL
            Exit Function
            ''
40:
            RaiseError(30096, "SLATEC", "DERFC", "X SO BIG ERFC UNDERFLOWS", 1, 1)
            RTN_VAL = 0#
            DERFC = RTN_VAL
        End Function




        Public Function INITDS(
      ByRef OS() As Double _
    , ByRef NOS As Long _
    , ByRef ETA As Single
    ) As Long

            '  DECK INITDS
            '  ***BEGIN PROLOGUE  INITDS
            '  ***PURPOSE  Determine the number of terms needed in an orthogonal
            '              polynomial series so that it meets a specified accuracy.
            '  ***LIBRARY   SLATEC (FNLIB)
            '  ***CATEGORY  C3A2
            '  ***TYPE      DOUBLE PRECISION (INITS-S, INITDS-D)
            '  ***KEYWORDS  CHEBYSHEV, FNLIB, INITIALIZE, ORTHOGONAL POLYNOMIAL,
            '               ORTHOGONAL SERIES, SPECIAL FUNCTIONS
            '  ***AUTHOR  Fullerton, W., (LANL)
            '  ***DESCRIPTION
            '
            '    Initialize the orthogonal series, represented by the array OS, so
            '    that INITDS is the number of terms needed to insure the error is no
            '    larger than ETA.  Ordinarily, ETA will be chosen to be one-tenth
            '    machine precision.
            '
            '               Input Arguments --
            '     OS     double precision array of NOS coefficients in an orthogonal
            '            series.
            '     NOS    number of coefficients in OS.
            '     ETA    single precision scalar containing requested accuracy of
            '            series.
            '
            '  ***REFERENCES  (NONE)
            '  ***ROUTINES CALLED  RaiseError
            '  ***REVISION HISTORY  (YYMMDD)
            '     770601  DATE WRITTEN
            '     890531  Changed all specific intrinsics to generic.  (WRB)
            '     890831  Modified array declarations.  (WRB)
            '     891115  Modified error message.  (WRB)
            '     891115  REVISION DATE from Version 3.2
            '     891214  Prologue converted to Version 4.0 format.  (BAB)
            '     900315  CALLs to XERROR changed to CALLs to RaiseError.  (THJ)
            '  ***END PROLOGUE  INITDS
            '  ***FIRST EXECUTABLE STATEMENT  INITDS

            Dim RTN_VAL&
            If NOS < 1 Then RaiseError(30128, "SLATEC", "INITDS", "Number of coefficients is less than 1", 2, 1)
            ''
            Static ERR_ As Double 'jrs 2020/03/14
            ERR_ = 0#
            Static II&
            Static i2&
            For II = 1 To NOS
                Static i&
                i = NOS + 1 - II
                i2 = i
                ERR_ = ERR_ + ABS_(REAL(OS(CInt(i))))
                If ERR_ > ETA Then GoTo 20
10:
            Next II
            ''
20:
            If i2 = NOS Then RaiseError(30129, "SLATEC", "INITDS", "Chebyshev series too short for specified accuracy", 1, 1)
            RTN_VAL = i2
            ''
            INITDS = RTN_VAL
        End Function






        Public Function I1MACH(
    ByVal vI As Byte
    ) As Long
            ' Return integer machine dependent constants.


            'I1MACH can be used to obtain machine-dependent parameters for the
            ' local machine environment.  It is a function subprogram with one
            ' (input) argument and can be referenced as follows:
            '     K = I1Mach(I)
            'where I=1,...,16.  The (output) value of K above is determined by
            ' the (input) value of I.  The results for various values of I are
            ' discussed below.

            Static bInitDone As Boolean
            Static IMach(0 To 16) As Long
            If Not bInitDone Then
                bInitDone = True


                '  I/O unit numbers:
                '     I1MACH( 1) = the standard input unit.
                '     I1MACH( 2) = the standard output unit.
                '     I1MACH( 3) = the standard punch unit.
                '     I1MACH( 4) = the standard error message unit.
                IMach(1) = 5
                IMach(2) = 6
                IMach(3) = 0
                IMach(4) = 0

                '  Words:
                '     I1MACH( 5) = the number of bits per integer storage unit.
                '     I1MACH( 6) = the number of characters per integer storage unit.
                IMach(5) = 32
                IMach(6) = 4

                '  Integers:
                '     assume integers are represented in the S-digit, base-A form
                '        sign ( X(S-1)*A**(S-1) + ... + X(1)*A + X(0) )
                '     where 0 <= X(I) < A for I=0,...,S-1.
                '
                '     I1MACH( 7) = A, the base.
                '     I1MACH( 8) = S, the number of base-A digits.
                '     I1MACH( 9) = A**S - 1, the largest magnitude.
                IMach(7) = 2
                IMach(8) = 31
                IMach(9) = 2147483647

                '  Floating-Point Numbers:
                '     Assume floating-point numbers are represented in the T-digit, base-B form
                '        sign (B**E)*( (X(1)/B) + ... + (X(T)/B**T) )
                '     where 0 <= X(I) < B for I=1,...,T, 0 < X(1), and EMIN <= E <= EMAX.
                '
                '     I1MACH(10) = B, the base.
                IMach(10) = 2

                '  Single-Precision:
                '     I1MACH(11) = T, the number of base-B digits.
                '     I1MACH(12) = EMIN, the smallest exponent E.
                '     I1MACH(13) = EMAX, the largest exponent E.
                IMach(11) = 24
                IMach(12) = -125
                IMach(13) = 128

                '  Double-Precision:
                '     I1MACH(14) = T, the number of base-B digits.
                '     I1MACH(15) = EMIN, the smallest exponent E.
                '     I1MACH(16) = EMAX, the largest exponent E.
                IMach(14) = 53
                IMach(15) = -1021
                IMach(16) = 1024
            End If

            I1MACH = IMach(vI)

            'References:
            ' P. A. Fox, A. D. Hall and N. L. Schryer, Framework for a portable library,
            ' ACM Transactions on Mathematical Software 4, 2 (June 1978), pp. 177-188.
            'Authors:
            '  Fox, P. A., (Bell Labs)
            '  Hall, A. D., (Bell Labs)
            '  Schryer, N. L., (Bell Labs)
        End Function

        'raising error- we'll replace it with ours
        Public Sub RaiseError(
      ByRef ErrNum As Long _
    , ByRef ErrLibrary As String _
    , ByRef ErrRoutine As String _
    , ByRef ErrMessage As String _
    , ByRef vErrorNumber As Long _
    , ByRef ErrLevel As Long
    )
            'Debug.Print vLibrary & ": " & vSubroutine & ": Error #" & vErrorNumber & " Severity " & vLevel & ": " & vMessage



            If ErrLevel >= 1 Then
                'err.Raise(ErrNum, ErrLibrary & "." & ErrRoutine, ErrMessage)
                Throw New ApplicationException(ErrNum & ", " & ErrLibrary & "." & ErrRoutine & ", " & ErrMessage)
            End If
        End Sub



        Friend Function EXP_(ByRef vX As Double) As Double
            ''Generic

            EXP_ = Exp(vX)
        End Function



        Friend Function REAL(ByRef vX As Double) As Double
            ''Generic
            REAL = vX
        End Function

        Friend Function ABS_(ByRef vX As Double) As Double
            ABS_ = Abs(vX)
        End Function



        Friend Function LOG_(ByRef vX As Double) As Double
            LOG_ = Log(vX)
        End Function



        Friend Function SQRT_(ByRef vX As Double) As Double
            ''Generic
            SQRT_ = Sqrt(vX)
        End Function

        Friend Function SIGN_(ByRef vX As Double, ByRef vY As Double) As Double

            'Transfer of Sign
            ''Generic

            'Definition:
            '   Abs(X) if Y >= 0
            '  -Abs(X) if Y < 0

            If vY >= 0 Then
                If vX >= 0 Then
                    SIGN_ = vX
                Else
                    SIGN_ = -vX
                End If
            Else
                If vX >= 0 Then
                    SIGN_ = -vX
                Else
                    SIGN_ = vX
                End If
            End If

        End Function

        Friend Sub FileCopyCheck(InFile$, OutFile$)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'System.IO.File.co(context.DataFileDir$ + "IFsForDyadic.mdb")
            Dim FDT1 As Date, FDT2 As Date

            Try
                FDT1 = System.IO.File.GetCreationTime(OutFile$)
                FDT2 = System.IO.File.GetCreationTime(InFile$)

                If FDT1 = FDT2 Then
                    Exit Sub
                End If
            Catch ex As Exception
            Finally
                System.IO.File.Copy(InFile$, OutFile$, True)
            End Try

        End Sub

        'jrs 2018/10/17
        Sub RegXRegSUMTwoDimInMem(CVars As Dictionary(Of String, Dictionary(Of String, Single?)), ByRef DumTwo(,) As Single?, Sec As String, VarName$, Warned As Boolean, cntx As IRebuildFacade)
            '-----------------------------------------------------------------

            'CVars() is the doubly dimensioned array holding the country specific data
            'Weight is the variable used for weighting regional averages,
            '   normally CGDP or CPOP
            'DumTwo is the two dimensional output variable used to return values
            '   to the calling routine; normally the IFs variable name for display
            'Sec is the specific economic sector or other secondary dimension for
            '   this particular call of the subroutine


            '   Initialize Sum by Region
            '   ------------------------
            Dim RegHold(NR) As Single
            Dim reg As Integer
            For reg% = 1 To NR
                RegHold(reg%) = 0
            Next reg%

            '   Weighted Sum Across Countries and Sum Weights
            '   ---------------------------------------------

            'Warned = False
            Dim ValHold As Single?, msg As String
            For Each sActor In cntx.Regionalization.Keys
                ValHold = Nothing
                If CVars.ContainsKey(sActor) Then
                    If CVars(sActor).ContainsKey(Sec) Then
                        ValHold = CVars(sActor)(Sec)
                    End If
                End If
                If Not ValHold.HasValue And Warned = False And sActor <> Sec Then
                    msg$ = "Warning:  Value for " + VarName$
                    msg$ = msg$ + " of " + sActor + ", " + Sec + " is "
                    msg$ = msg$ + "Null (Missing Data) - Not including in average.  "
                    msg$ = msg$ + "You will not be warned about other null values "
                    msg$ = msg$ + "for this variable."
                    Debug.Print(msg$)
                    Warned = True
                ElseIf ValHold.HasValue Then
                    For Each item In (From s In cntx.Regionalization Where s.Key = sActor Select Key = s.Key, Value = s.Value)
                        reg% = item.Value
                    Next
                    RegHold(reg%) = RegHold(reg%) + CSng(ValHold)
                End If
            Next

            '   Compute Weighted Regional Average and Print to File
            '   ---------------------------------------------------
            Dim Reg2 As Integer
            For reg% = 1 To NR
                For Each item In (From s In cntx.Regionalization Where s.Key = Sec Select Key = s.Key, Value = s.Value)
                    Reg2 = item.Value
                Next
                DumTwo(reg%, Reg2%) = RegHold(reg%)
            Next reg%

        End Sub

        'jrs 2016/09/23
        'Regionalize Variable without saving it to database
        Sub RegXRegAvgTwoDimInMem(CVars As Dictionary(Of String, Dictionary(Of String, Single?)), Weight As IDictionary(Of String, Single?), ByRef DumTwo(,) As Single?, Sec As String, VarName$, Warned As Boolean, cntx As IRebuildFacade)
            '-----------------------------------------------------------------

            'CVars() is the doubly dimensioned array holding the country specific data
            'Weight is the variable used for weighting regional averages,
            '   normally CGDP or CPOP
            'DumTwo is the two dimensional output variable used to return values
            '   to the calling routine; normally the IFs variable name for display
            'Sec is the specific economic sector or other secondary dimension for
            '   this particular call of the subroutine


            '   Initialize Sum by Region
            '   ------------------------
            Dim RegHold(NR) As Single, RegWeight(NR) As Single
            Dim reg As Integer
            For reg% = 1 To NR
                RegHold(reg%) = 0
                RegWeight(reg%) = 0
            Next reg%

            '   Weighted Sum Across Countries and Sum Weights
            '   ---------------------------------------------

            'Warned = False
            Dim ValHold As Single?, msg As String
            For Each sActor In cntx.Regionalization.Keys
                ValHold = Nothing
                If CVars.ContainsKey(sActor) Then
                    If CVars(sActor).ContainsKey(Sec) Then
                        ValHold = CVars(sActor)(Sec)
                    End If
                End If
                If Not ValHold.HasValue And Warned = False And sActor <> Sec Then
                    msg$ = "Warning:  Value for " + VarName$
                    msg$ = msg$ + " of " + sActor + ", " + Sec + " is "
                    msg$ = msg$ + "Null (Missing Data) - Not including in average.  "
                    msg$ = msg$ + "You will not be warned about other null values "
                    msg$ = msg$ + "for this variable."
                    Debug.Print(msg$)
                    Warned = True
                ElseIf ValHold.HasValue Then
                    For Each item In (From s In cntx.Regionalization Where s.Key = sActor Select Key = s.Key, Value = s.Value)
                        reg% = item.Value
                    Next
                    RegHold(reg%) = RegHold(reg%) + CSng(ValHold * Weight(sActor))
                    RegWeight(reg%) = RegWeight(reg%) + CSng(Weight(sActor))
                End If
            Next

            '   Compute Weighted Regional Average and Print to File
            '   ---------------------------------------------------
            Dim Reg2 As Integer
            For reg% = 1 To NR
                For Each item In (From s In cntx.Regionalization Where s.Key = Sec Select Key = s.Key, Value = s.Value)
                    Reg2 = item.Value
                Next
                If RegWeight(reg%) > 0 Then
                    DumTwo(reg%, Reg2%) = RegHold(reg%) / RegWeight(reg%)
                Else
                    If reg% <> Reg2% And Warned = False Then  'jrs 2017/12/05 added And Warned = False
                        'jrs 2012/02/17
                        'msg$ = "Fatal error:  No non-null members of Region "
                        msg$ = "Important Warning:  No non-null members of Region "
                        msg$ = msg$ + cntx.GetDimension(1).Buckets(CShort(reg%)) + " for variable "
                        'msg$ = msg$ + VarName$ + "(" + Sec + ")" + " - cannot compute average"
                        msg$ = msg$ + VarName$ + " - average set to zero"
                        Debug.Print(msg$)
                    End If
                    'Stop
                    'jrs 2017/12/06 DumTwo(reg%, Sec) = 0
                    DumTwo(reg%, Reg2%) = 0
                End If
            Next reg%

        End Sub

        'jrs 2019/08/14
        'Regionalize Variable without saving it to database
        Sub RegXRegAvgTwoDimInMem(CVars As Dictionary(Of String, Dictionary(Of String, Single?)), Weight As IDictionary(Of String, Single?), ByRef DumTwo(,) As Single, Sec As String, VarName$, Warned As Boolean, cntx As IRebuildFacade)
            '-----------------------------------------------------------------

            'CVars() is the doubly dimensioned array holding the country specific data
            'Weight is the variable used for weighting regional averages,
            '   normally CGDP or CPOP
            'DumTwo is the two dimensional output variable used to return values
            '   to the calling routine; normally the IFs variable name for display
            'Sec is the specific economic sector or other secondary dimension for
            '   this particular call of the subroutine


            '   Initialize Sum by Region
            '   ------------------------
            Dim RegHold(NR) As Single, RegWeight(NR) As Single
            Dim reg As Integer
            For reg% = 1 To NR
                RegHold(reg%) = 0
                RegWeight(reg%) = 0
            Next reg%

            '   Weighted Sum Across Countries and Sum Weights
            '   ---------------------------------------------

            'Warned = False
            Dim ValHold As Single?, msg As String
            For Each sActor In cntx.Regionalization.Keys
                ValHold = Nothing
                If CVars.ContainsKey(sActor) Then
                    If CVars(sActor).ContainsKey(Sec) Then
                        ValHold = CVars(sActor)(Sec)
                    End If
                End If
                If Not ValHold.HasValue And Warned = False And sActor <> Sec Then
                    msg$ = "Warning:  Value for " + VarName$
                    msg$ = msg$ + " of " + sActor + ", " + Sec + " is "
                    msg$ = msg$ + "Null (Missing Data) - Not including in average.  "
                    msg$ = msg$ + "You will not be warned about other null values "
                    msg$ = msg$ + "for this variable."
                    Debug.Print(msg$)
                    Warned = True
                ElseIf ValHold.HasValue Then
                    For Each item In (From s In cntx.Regionalization Where s.Key = sActor Select Key = s.Key, Value = s.Value)
                        reg% = item.Value
                    Next
                    RegHold(reg%) = RegHold(reg%) + CSng(ValHold * Weight(sActor))
                    RegWeight(reg%) = RegWeight(reg%) + CSng(Weight(sActor))
                End If
            Next

            '   Compute Weighted Regional Average and Print to File
            '   ---------------------------------------------------
            Dim Reg2 As Integer
            For reg% = 1 To NR
                For Each item In (From s In cntx.Regionalization Where s.Key = Sec Select Key = s.Key, Value = s.Value)
                    Reg2 = item.Value
                Next
                If RegWeight(reg%) > 0 Then
                    DumTwo(reg%, Reg2%) = RegHold(reg%) / RegWeight(reg%)
                Else
                    If reg% <> Reg2% And Warned = False Then  'jrs 2017/12/05 added And Warned = False
                        'jrs 2012/02/17
                        'msg$ = "Fatal error:  No non-null members of Region "
                        msg$ = "Important Warning:  No non-null members of Region "
                        msg$ = msg$ + cntx.GetDimension(1).Buckets(CShort(reg%)) + " for variable "
                        'msg$ = msg$ + VarName$ + "(" + Sec + ")" + " - cannot compute average"
                        msg$ = msg$ + VarName$ + " - average set to zero"
                        Debug.Print(msg$)
                    End If
                    'Stop
                    'jrs 2017/12/06 DumTwo(reg%, Sec) = 0
                    DumTwo(reg%, Reg2%) = 0
                End If
            Next reg%

        End Sub

        'ttt 2019/03/29
        Sub RegAvgCohorts(CountryOrg As Dictionary(Of String, Single?()), ByRef RegionTarget(,) As Single?, Weight As IDictionary(Of String, Single?), VarName$, Warned As Boolean, cntx As IRebuildFacade)  '
            Dim cc As Integer
            Dim RegWeightCoh(NR, cntx.NCohorts) As Single?
            For reg% = 1 To NR
                For cc = 0 To cntx.NCohorts
                    RegionTarget(reg%, cc) = 0
                    RegWeightCoh(reg%, cc) = 0
                Next cc
            Next reg%

            '   Weighted Sum Across Countries and Sum Weights
            '   ---------------------------------------------
            Warned = False
            Dim ValHold As Single?, msg As String
            For Each Ctry In cntx.Regionalization.Keys
                For cc = 0 To cntx.NCohorts
                    ValHold = CountryOrg(Ctry)(cc)
                    If Not ValHold.HasValue AndAlso Warned = False Then
                        msg$ = "Important Warning:  Value for " + VarName$
                        msg$ = msg$ + " of " + Ctry + " is "
                        msg$ = msg$ + "Null (Missing Data) - Not including in average.  "
                        msg$ = msg$ + "You will not be warned about other null values "
                        msg$ = msg$ + "for this variable."
                        Debug.Print(msg$)
                        Warned = True
                    Else
                        Dim reg As Integer
                        For Each item In (From s In cntx.Regionalization Where s.Key = Ctry Select Key = s.Key, Value = s.Value)
                            reg% = item.Value 'RegionId
                        Next
                        RegionTarget(reg%, cc) = RegionTarget(reg%, cc) + CSng(If(ValHold, 0) * Weight(Ctry))
                        RegWeightCoh(reg%, cc) = RegWeightCoh(reg%, cc) + CSng(Weight(Ctry))
                    End If
                Next cc
            Next
            '   Compute Weighted Regional Average and Print to File
            '   ---------------------------------------------------
            For reg% = 1 To NR
                For cc = 0 To cntx.NCohorts
                    If RegWeightCoh(reg%, cc) > 0 Then
                        RegionTarget(reg%, cc) = RegionTarget(reg%, cc) / RegWeightCoh(reg%, cc)
                    Else
                        msg$ = "Important Warning:  No non-null members of Region "
                        msg$ = msg$ + cntx.GetDimension(1).Buckets(CShort(reg%)) + " for variable "
                        msg$ = msg$ + VarName$ + " - average set to zero"
                        Debug.Print(msg$)
                        RegionTarget(reg%, cc) = 0
                    End If
                Next cc
            Next reg%
        End Sub

        Sub RegAvgCohortWeight(CountryOrg As Dictionary(Of String, Dictionary(Of Integer, Single?)), ByRef RegionTarget(,) As Single, Weight As Dictionary(Of String, Dictionary(Of Integer, Single?)), VarName$, Warned As Boolean, cntx As IRebuildFacade, Optional StartCohort As Integer = 0, Optional TotCohorts As Integer = 9999)  '
            '-----------------------------------------------------------------
            'mti 2020/01/14 new procedure
            'the other one do not use cohort (or broad age group) population as weights, rather uses total pop
            'This also allows flexible age-group numbers (i.e., 21 collapsed into 9 as in poverty data)

            '   Initialize Sum by Region
            '   ------------------------

            Dim cc As Integer
            Dim RegWeightCoh(NR, cntx.NCohorts) As Single?
            For reg% = 1 To NR
                For cc = StartCohort To TotCohorts
                    RegionTarget(reg%, cc) = 0
                    RegWeightCoh(reg%, cc) = 0
                Next cc
            Next reg%

            '   Weighted Sum Across Countries and Sum Weights
            '   ---------------------------------------------
            Warned = False
            Dim ValHold As Single?, msg As String
            For Each Ctry In cntx.Regionalization.Keys
                For cc = StartCohort To TotCohorts
                    ValHold = CountryOrg(Ctry)(cc)
                    If Not ValHold.HasValue AndAlso Warned = False Then
                        msg$ = "Important Warning:  Value for " + VarName$
                        msg$ = msg$ + " of " + Ctry + " is "
                        msg$ = msg$ + "Null (Missing Data) - Not including in average.  "
                        msg$ = msg$ + "You will not be warned about other null values "
                        msg$ = msg$ + "for this variable."
                        Debug.Print(msg$)
                        Warned = True
                    Else
                        Dim reg As Integer
                        For Each item In (From s In cntx.Regionalization Where s.Key = Ctry Select Key = s.Key, Value = s.Value)
                            reg% = item.Value 'RegionId
                        Next
                        RegionTarget(reg%, cc) = RegionTarget(reg%, cc) + CSng(If(ValHold, 0) * Weight(Ctry)(cc))
                        RegWeightCoh(reg%, cc) = RegWeightCoh(reg%, cc) + CSng(Weight(Ctry)(cc))
                    End If
                Next cc
            Next
            '   Compute Weighted Regional Average and Print to File
            '   ---------------------------------------------------
            For reg% = 1 To NR
                For cc = StartCohort To TotCohorts
                    If RegWeightCoh(reg%, cc) > 0 Then
                        RegionTarget(reg%, cc) = CSng(RegionTarget(reg%, cc) / RegWeightCoh(reg%, cc))
                    Else
                        msg$ = "Important Warning:  No non-null members of Region "
                        msg$ = msg$ + cntx.GetDimension(1).Buckets(CShort(reg%)) + " for variable "
                        msg$ = msg$ + VarName$ + " - average set to zero"
                        Debug.Print(msg$)
                        RegionTarget(reg%, cc) = 0
                    End If
                Next cc
            Next reg%
        End Sub

        Class RegressionObject
            Private Const MaxO% = 25
            Private GlobalO% '"Ordnung" = degree of the polynom expected
            Private Finished As Boolean

            Private SumX#(0 To 2 * MaxO)
            Private SumYX#(0 To MaxO)
            Private M#(0 To MaxO, 0 To MaxO + 1)
            Private C#(0 To MaxO) 'coefficients in: Y = C(0)*X^0 + C(1)*X^1 + C(2)*X^2 + ...

            Public Sub Init()
                Dim i As Integer
                Finished = False
                For i = 0 To MaxO
                    SumX(i) = 0#
                    SumX(i + MaxO) = 0#
                    SumYX(i) = 0#
                    C(i) = 0#
                Next i
            End Sub

            Public Sub New()
                Init()
                GlobalO = 2
            End Sub

            Private Sub GaussSolve(O%)
                'gauss algorithm implementation,
                'following R.Sedgewick's "Algorithms in C", Addison-Wesley, with minor modifications
                Dim i%, j%, k%, iMax%, T#, O1%
                O1 = O + 1
                'first triangulize the matrix
                For i = 0 To O
                    iMax = i : T = Abs(M(iMax, i))
                    For j = i + 1 To O 'find the line with the largest absvalue in this row
                        If T < Abs(M(j, i)) Then iMax = j : T = Abs(M(iMax, i))
                    Next j
                    If i < iMax Then 'exchange the two lines
                        For k = i To O1
                            T = M(i, k)
                            M(i, k) = M(iMax, k)
                            M(iMax, k) = T
                        Next k
                    End If
                    For j = i + 1 To O 'scale all following lines to have a leading zero
                        T = M(j, i) / M(i, i)
                        M(j, i) = 0#
                        For k = i + 1 To O1
                            M(j, k) = M(j, k) - M(i, k) * T
                        Next k
                    Next j
                Next i
                'then substitute the coefficients
                For j = O To 0 Step -1
                    T = M(j, O1)
                    For k = j + 1 To O
                        T = T - M(j, k) * C(k)
                    Next k
                    C(j) = T / M(j, j)
                Next j
                Finished = True
            End Sub

            Private Sub BuildMatrix(O%)
                Dim i%, k%, O1%
                O1 = O + 1
                For i = 0 To O
                    For k = 0 To O
                        M(i, k) = SumX(i + k)
                    Next k
                    M(i, O1) = SumYX(i)
                Next i
            End Sub

            Private Sub FinalizeMatrix(O%)
                Dim i%, O1%
                O1 = O + 1
                For i = 0 To O
                    M(i, O1) = SumYX(i)
                Next i
            End Sub

            Public Property XYCount%()
                Get
                    XYCount = CInt(SumX(0))
                End Get
                Set(value%)

                End Set
            End Property

            Private Sub Solve()
                Dim O%
                O = GlobalO
                If XYCount <= O Then O = XYCount - 1
                If O < 0 Then Exit Sub
                BuildMatrix(O)

                Try
                    GaussSolve(O)
                Catch ex As Exception
                    If (1 < O) Then
                        C(0) = 0#
                        O = O - 1
                        FinalizeMatrix(O)
                    End If
                End Try
                'On Error Resume Next
                'GaussSolve(O)
                'While (err.number <> 0) And (1 < O)
                '    err.Clear
                '    C(0) = 0#
                '    O = O - 1
                '    FinalizeMatrix(O)
                'End While
                'On Error GoTo 0
            End Sub

            Public Property Coeff#(Exponent%)
                Get
                    Dim Ex%, O%
                    If Not Finished Then Solve()
                    Ex = Abs(Exponent)
                    O = GlobalO
                    If XYCount <= O Then O = XYCount - 1
                    If O < Ex Then Coeff = 0# Else Coeff = C(Ex)
                End Get
                Set(value#)

                End Set
            End Property

            Public Property Degree%()
                Get
                    Degree = GlobalO
                End Get
                Set(NewVal%)
                    If NewVal < 0 Or MaxO < NewVal Then
                        'Error 6000, "RegressionObject", NewVal & " is an invalid property value! Use 0<= Degree <= " & MaxO)
                        Throw (New Exception(NewVal & " is an invalid property value! Use 0<= Degree <= " & MaxO))
                        Exit Property
                    End If
                    Init()
                    GlobalO = NewVal

                End Set
            End Property

            Public Sub XYAdd(ByVal NewX#, ByVal NewY#)
            	'gvdb 2024/04/05
            	' unused variable	
                Dim i%, TX#, Max2O% ' j%,
                Finished = False
                Max2O = 2 * GlobalO
                TX = 1.0#
                SumX(0) = SumX(0) + 1
                SumYX(0) = SumYX(0) + NewY
                For i = 1 To GlobalO
                    TX = TX * NewX
                    SumX(i) = SumX(i) + TX
                    SumYX(i) = SumYX(i) + NewY * TX
                Next i
                For i = GlobalO + 1 To Max2O
                    TX = TX * NewX
                    SumX(i) = SumX(i) + TX
                Next i
            End Sub

            Public Function RegVal#(X#)
                Dim i%, O%
                If Not Finished Then Solve()
                RegVal = 0#
                O = GlobalO
                If XYCount <= O Then O = XYCount - 1
                For i = 0 To O
                    RegVal = RegVal + C(i) * X ^ i
                Next i
            End Function
        End Class

        Friend Sub SaveDyadicTableBaseYear(Of T As {New, IForecastDyad, ForecastDyad})(RegDyadVals(,) As Single?, cntx As IRebuildFacade, DyadicTbl As Microsoft.EntityFrameworkCore.DbSet(Of T))

            For ActorId% = 1 To cntx.GetDimension(1).Buckets.Count
                For PartnerId% = 1 To cntx.GetDimension(1).Buckets.Count
                    '   Put Into Cohort Field of Region Record
                    '   --------------------------------------------------------------------------
                    Dim row As T = New T
                    row.Actor = ActorId%
                    row.Partner = PartnerId%
                    row.Year = cntx.FirstYear

                    If RegDyadVals(ActorId%, PartnerId%).HasValue Then
                        'jrs 2019/05/03 replace BaseYear with FirstYear in entire procedure to make it work for Historical Rebuild Base
                        row.Val = CSng(RegDyadVals(ActorId%, PartnerId%))

                        DyadicTbl.Add(row)
                    End If
                Next
            Next

        End Sub

        Friend Sub SaveDyadicTableBaseYearInv(Of T As {New, IForecastDyad, ForecastDyad})(RegDyadVals(,) As Single?, cntx As IRebuildFacade, DyadicTbl As Microsoft.EntityFrameworkCore.DbSet(Of T))

            For ActorId% = 1 To cntx.GetDimension(1).Buckets.Count
                For PartnerId% = 1 To cntx.GetDimension(1).Buckets.Count
                    '   Put Into Cohort Field of Region Record
                    '   --------------------------------------------------------------------------
                    Dim row As T = New T
                    row.Actor = PartnerId%
                    row.Partner = ActorId%
                    row.Year = cntx.FirstYear

                    If RegDyadVals(ActorId%, PartnerId%).HasValue Then
                        row.Val = CSng(RegDyadVals(ActorId%, PartnerId%))

                        DyadicTbl.Add(row)
                    End If
                Next
            Next

        End Sub

        'Friend Sub SaveDyadicTableBaseYearW(Of T As {New, IForecastDyad, ForecastDyad})(RegDyadVals(,) As Single?, cntx As IRebuildFacade, DyadicTbl As Microsoft.EntityFrameworkCore.DbSet(Of T))

        '    For ActorId% = 1 To cntx.GetDimension(1).Buckets.Count
        '        For PartnerId% = 1 To cntx.GetDimension(1).Buckets.Count
        '            '   Put Into Cohort Field of Region Record
        '            '   --------------------------------------------------------------------------
        '            Dim row As T = New T
        '            row.Actor = ActorId%
        '            row.Partner = PartnerId%

        '            If RegDyadVals(ActorId%, PartnerId%).HasValue Then
        '                'jrs 2019/05/03 replace BaseYear with FirstYear in entire procedure to make it work for Historical Rebuild Base
        '                Select Case cntx.FirstYear
        '                    Case 1960
        '                        row.Val1960 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1965
        '                        row.Val1965 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1970
        '                        row.Val1970 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1975
        '                        row.Val1975 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1980
        '                        row.Val1980 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1985
        '                        row.Val1985 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1990
        '                        row.Val1990 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 1995
        '                        row.Val1995 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2000
        '                        row.Val2000 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2005
        '                        row.Val2005 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2010
        '                        row.Val2010 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2015
        '                        row.Val2015 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2016
        '                        row.Val2016 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2017
        '                        row.Val2017 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2018
        '                        row.Val2018 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2019
        '                        row.Val2019 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2020
        '                        row.Val2020 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2021
        '                        row.Val2021 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2022
        '                        row.Val2022 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2023
        '                        row.Val2023 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2024
        '                        row.Val2024 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2025
        '                        row.Val2025 = RegDyadVals(ActorId%, PartnerId%)
        '                End Select

        '                DyadicTbl.Add(row)
        '            End If
        '        Next
        '    Next

        'End Sub

        'Friend Sub SaveDyadicTableBaseYearInvW(Of T As {New, IForecastDyad, ForecastDyad})(RegDyadVals(,) As Single?, cntx As IRebuildFacade, DyadicTbl As Microsoft.EntityFrameworkCore.DbSet(Of T))

        '    For ActorId% = 1 To cntx.GetDimension(1).Buckets.Count
        '        For PartnerId% = 1 To cntx.GetDimension(1).Buckets.Count
        '            '   Put Into Cohort Field of Region Record
        '            '   --------------------------------------------------------------------------
        '            Dim row As T = New T
        '            row.Actor = PartnerId%
        '            row.Partner = ActorId%

        '            If RegDyadVals(ActorId%, PartnerId%).HasValue Then
        '                Select Case cntx.BaseYear
        '                    Case 2015
        '                        row.Val2015 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2016
        '                        row.Val2016 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2017
        '                        row.Val2017 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2018
        '                        row.Val2018 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2019
        '                        row.Val2019 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2020
        '                        row.Val2020 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2021
        '                        row.Val2021 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2022
        '                        row.Val2022 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2023
        '                        row.Val2023 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2024
        '                        row.Val2024 = RegDyadVals(ActorId%, PartnerId%)
        '                    Case 2025
        '                        row.Val2025 = RegDyadVals(ActorId%, PartnerId%)
        '                End Select

        '                DyadicTbl.Add(row)
        '            End If
        '        Next
        '    Next

        'End Sub

        'jrs 2019/03/21
        Friend Sub SaveMortCohortDist(Of T As {New, ICohortRegion, CohortRegion})(BasMordst As Dictionary(Of String, Dictionary(Of String, Single?)), valtbl As Microsoft.EntityFrameworkCore.DbSet(Of T), cntx As IRebuildFacade)
            For Each R In valtbl
                R.Cohort0 = BasMordst(R.RegionName)("Cohort0") * 1000
                R.Cohort1 = BasMordst(R.RegionName)("Cohort1") * 1000
            Next
        End Sub

        'jrs 2010/12/06 *** new procedure: Using Annual Growth Rate ***
        Public Sub LoadGrowthRatesOneDim(sVar As String, ByRef Vals() As Single?, context As IForecastContext)
        	'gvdb 2024/04/05
            ' unused variable
            'Dim sql As String, 
            Dim i As Integer
            'Call FindVarNumb(idx, mid(sVar, 1, Len(sVar) - 2))
            'jrs 2011/03/11
            Using db As IFs2Context = New IFs2Context()
                i = 0
                If context.HistoricMode = False Then
                    For Each item In (From g In db.AnnualGrowthRates
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper)
                        Do
                            i = i + 1
                            Vals(i) = Nothing
                        Loop Until item.RegName = context.GetDimension(1).Buckets(CShort(i)) OrElse i = context.GetDimension(1).Buckets.Count 'ttt 2019/10/08 added  OrElse i = context.GetDimension(1).Buckets.Count
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            Vals(i) = CSng(item.GrowthRate)
                        End If
                        'mti April 2011 - end
                    Next
                Else
                    For Each item In (From g In db.AnnualGrowthRatesHist
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper)
                        Do
                            i = i + 1
                            Vals(i) = Nothing
                        Loop Until item.RegName = context.GetDimension(1).Buckets(CShort(i)) OrElse i = context.GetDimension(1).Buckets.Count 'ttt 2019/10/08 added  OrElse i = context.GetDimension(1).Buckets.Count
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            Vals(i) = CSng(item.GrowthRate)
                        End If
                        'mti April 2011 - end
                    Next
                End If
            End Using
        End Sub

        Public Sub LoadGrowthRatesOneDim(sVar As String, ByRef Vals() As Single, context As IForecastContext)
        	'gvdb 2024/04/05
            ' unused variable
            'Dim sql As String, 
            Dim i As Integer
            'Call FindVarNumb(idx, mid(sVar, 1, Len(sVar) - 2))
            'jrs 2011/03/11
            Using db As IFs2Context = New IFs2Context()
                i = 0
                If context.HistoricMode = False Then
                    For Each item In (From g In db.AnnualGrowthRates
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper)
                        Do
                            i = i + 1
                            Vals(i) = -9999
                        Loop Until item.RegName = context.GetDimension(1).Buckets(CShort(i)) Or i = context.GetDimension(1).Buckets.Count
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            Vals(i) = CSng(item.GrowthRate)
                        End If
                        'mti April 2011 - end
                    Next
                Else
                    For Each item In (From g In db.AnnualGrowthRatesHist
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper)
                        Do
                            i = i + 1
                            Vals(i) = -9999
                        Loop Until item.RegName = context.GetDimension(1).Buckets(CShort(i)) Or i = context.GetDimension(i).Buckets.Count
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            Vals(i) = CSng(item.GrowthRate)
                        End If
                        'mti April 2011 - end
                    Next
                End If
            End Using
        End Sub

        'mti May 04, 2012 *** new procedure: Loading Annual Growth Rate Two dim ***
        Public Sub LoadGrowthRatesTwoDim(sVar As String, ByRef Vals(,) As Single, context As IForecastContext)
            'gvdb 2024/04/05
            ' unused variable
            'Dim sql As String, 
            Dim i As Integer, j As Integer, dim2Idx As Integer
            Dim sVarPlain As String = sVar.Substring(0, sVar.Length - 2)
            dim2Idx = context.GetVariable(sVarPlain).Dimensions(2)
            'Call FindVarNumb(idx, mid(sVar, 1, Len(sVar) - 2))
            Dim dimList As List(Of Integer) = New List(Of Integer)
            'jrs 2011/03/11
            Using db As IFs2Context = New IFs2Context()
                i = 0
                If context.HistoricMode = False Then
                    For Each item In (From g In db.AnnualGrowthRates
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper, g.Dim2)
                        Do
                            If i = 0 OrElse item.RegName <> context.GetDimension(1).Buckets(CShort(i)) Then
                                i = i + 1
                                dimList.Clear()
                            End If
                            j = 0
                            Do
                                j = j + 1
                                If Not dimList.Contains(j) Then
                                    Vals(i, j) = -9999
                                End If
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count
                        Loop Until i = context.GetDimension(1).Buckets.Count OrElse item.RegName = context.GetDimension(1).Buckets(CShort(i))
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            j = 0
                            Do
                                j = j + 1
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count OrElse item.Dim2 = context.GetDimension(dim2Idx).Buckets(CShort(j))
                            Vals(i, j) = CSng(item.GrowthRate)
                            dimList.Add(j)
                        End If
                        'mti April 2011 - end
                    Next
                Else
                    For Each item In (From g In db.AnnualGrowthRatesHist
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper, g.Dim2)
                        Do
                            If i = 0 OrElse item.RegName <> context.GetDimension(1).Buckets(CShort(i)) Then
                                i = i + 1
                                dimList.Clear()
                            End If
                            j = 0
                            Do
                                j = j + 1
                                If Not dimList.Contains(j) Then
                                    Vals(i, j) = -9999
                                End If
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count
                        Loop Until i = context.GetDimension(1).Buckets.Count OrElse item.RegName = context.GetDimension(1).Buckets(CShort(i))
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            j = 0
                            Do
                                j = j + 1
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count OrElse item.Dim2 = context.GetDimension(dim2Idx).Buckets(CShort(j))
                            Vals(i, j) = CSng(item.GrowthRate)
                            dimList.Add(j)
                        End If
                        'mti April 2011 - end
                    Next
                End If
            End Using
        End Sub

        'mti 2022/09/27 *** new procedure: Loading Annual Growth Rate Three dim ***
        Public Sub LoadGrowthRatesThreeDim(sVar As String, Vals(,,) As Single, context As IForecastContext)
            'gvdb 2024/04/05
            ' unused variable
            'Dim sql As String
            Dim i As Integer, j As Integer, k As Integer, dim2Idx As Integer, dim3Idx As Integer
            Dim sVarPlain As String = sVar.Substring(0, sVar.Length - 2)
            dim2Idx = context.GetVariable(sVarPlain).Dimensions(2)
            dim3Idx = context.GetVariable(sVarPlain).Dimensions(3)
            'Call FindVarNumb(idx, mid(sVar, 1, Len(sVar) - 2))
            'Dim dimList2 As List(Of Integer) = New List(Of Integer)
            'Dim dimList3 As List(Of Integer) = New List(Of Integer)
            'jrs 2011/03/11
            Using db As IFs2Context = New IFs2Context()
                i = 0
                If context.HistoricMode = False Then
                    For Each item In (From g In db.AnnualGrowthRates
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper, g.Dim2, g.Dim3)
                        Do
                            If i = 0 OrElse item.RegName <> context.GetDimension(1).Buckets(CShort(i)) Then
                                i = i + 1
                                'dimList2.Clear()
                            End If
                            j = 0
                            Do
                                j = j + 1

                                k = 0
                                Do
                                    k = k + 1
                                    Vals(i, j, k) = -9999
                                Loop Until k = context.GetDimension(dim3Idx).Buckets.Count
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count
                        Loop Until i = context.GetDimension(1).Buckets.Count OrElse item.RegName = context.GetDimension(1).Buckets(CShort(i))
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            j = 0
                            Do
                                j = j + 1
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count OrElse item.Dim2 = context.GetDimension(dim2Idx).Buckets(CShort(j))
                            k = 0
                            Do
                                k = k + 1
                            Loop Until k = context.GetDimension(dim3Idx).Buckets.Count OrElse item.Dim3 = context.GetDimension(dim3Idx).Buckets(CShort(k))
                            Vals(i, j, k) = CSng(item.GrowthRate)
                        End If
                        'mti April 2011 - end
                    Next
                Else
                    For Each item In (From g In db.AnnualGrowthRatesHist
                                      Where g.VarName.ToUpper = sVar.ToUpper And If(g.GrowthRate, -9999) > -9999
                                      Order By g.RegName.ToUpper, g.Dim2, g.Dim3)
                        Do
                            If i = 0 OrElse item.RegName <> context.GetDimension(1).Buckets(CShort(i)) Then
                                i = i + 1
                            End If
                            j = 0
                            Do
                                j = j + 1
                                k = 0
                                Do
                                    k = k + 1
                                    Vals(i, j, k) = -9999
                                Loop Until k = context.GetDimension(dim3Idx).Buckets.Count
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count
                        Loop Until i = context.GetDimension(i).Buckets.Count OrElse item.RegName = context.GetDimension(1).Buckets(CShort(i))
                        'mti April 2011 - start
                        If CInt(If(item.GrowthRate, -9999)) > -9999 Then
                            j = 0
                            Do
                                j = j + 1
                            Loop Until j = context.GetDimension(dim2Idx).Buckets.Count OrElse item.Dim2 = context.GetDimension(dim2Idx).Buckets(CShort(j))
                            k = 0
                            Do
                                k = k + 1
                            Loop Until k = context.GetDimension(dim3Idx).Buckets.Count OrElse item.Dim3 = context.GetDimension(dim3Idx).Buckets(CShort(k))
                            Vals(i, j, k) = CSng(item.GrowthRate)
                        End If
                        'mti April 2011 - end
                    Next
                End If
            End Using
        End Sub

        'jrs 2018/06/22
        Public Function getAverageOfLastXYrs(ByRef tbl As IDictionary(Of String, Single?()), ByRef years As Short(), ByVal sCtry As String, ByVal iXyrs As Integer) As Single?
            Dim i As Integer
            Dim SumVals As Single? = 0, ICount As Integer = 0
            If iXyrs = 0 Then Exit Function
            'This procedures assumes assending order
            If years(0) > years(1) Then Exit Function
            If tbl.ContainsKey(sCtry) Then
                For i = years.Count - 1 To AMAX(0, years.Count - 1 - iXyrs) Step -1
                    If tbl(sCtry)(i).HasValue Then
                        SumVals = SumVals + tbl(sCtry)(i)
                        ICount = ICount + 1
                        If ICount = iXyrs Then
                            Exit For
                        End If
                    End If
                Next i

                If iXyrs > 0 Then
                    getAverageOfLastXYrs = SumVals / iXyrs
                End If
            End If
        End Function

        Public Function getClosestYrVal(ByRef DatTbl As IDictionary(Of String, Single?()), ByRef DatYrs As Short(), sCtry As String, DYear As Integer, Optional MaxDist As Short = 9999, ByRef Optional UsedYear% = 0) As Single?
            'mti 2019/03/31 New Function
            'read data from a target year (DYear); if not found, reads from the closest year as long as the year is within range (MaxDist)
            'returns Null when there is no data

            Dim FldName%
            Dim FldVal As Single? 'MYrFldNo%, EYrFldNo%   ', EYrFldNo%
            Dim YrFldPrior%, YrFld%, i%

            getClosestYrVal = Nothing
            Dim getClosestYrNo As Short = 0
            Dim getClosestYrIdx As Short = 0


            '    MYrFldNo = 0
            '    For i = DatTbl.Fields.Count - 1 To 0 Step -1
            '        If IsNumeric(DatTbl.Fields(i).Name) Then
            '            MYrFldNo = i  'field number for mostrecent year with data
            '            Exit For
            '        End If
            '    Next
            '
            '    For i = 0 To DatTbl.Fields.Count - 1
            '        If IsNumeric(DatTbl.Fields(i).Name) Then
            '            EYrFldNo = i  'field number for earliest year with data
            '            Exit For
            '        End If
            '    Next

            YrFld = 0  'Val(DatTbl.Fields(0).Name)
            For i = 0 To DatYrs.Count - 1
                FldName% = DatYrs(i)
                FldVal = DatTbl(sCtry)(i)
                If FldName > 0 And FldName < Date.Today.Year + 1 Then 'year column; cannot be forecast
                    If FldVal.HasValue Then
                        YrFldPrior = YrFld
                        YrFld = FldName%
                        If YrFld >= DYear Then
                            If DYear - YrFldPrior <= YrFld - DYear Then
                                getClosestYrNo = CShort(YrFldPrior)
                            Else
                                getClosestYrNo = CShort(YrFld)
                                getClosestYrIdx = CShort(i)
                            End If
                            Exit For
                        Else   'If i = MYrFldNo Then  'reached the latest year column; and the latest year is earlier than year sought
                            getClosestYrNo = CShort(YrFld)  'Val(DatTbl.Fields(i).Name)
                            getClosestYrIdx = CShort(i)
                            'Exit For
                        End If
                    End If
                End If
            Next

            If getClosestYrNo > 0 Then
                If Abs(DYear - getClosestYrNo) < MaxDist Then
                    'i = getYearIdx(DatYrs, getClosestYrNo)
                    getClosestYrVal = DatTbl(sCtry)(getClosestYrIdx)
                    UsedYear% = getClosestYrNo
                End If
            End If


        End Function

        'jrs 2019/06/13
        Public Sub LoadPopCohortGlobalVar(ByRef vals As Double(), ByRef pc As PopCohortGlobal, ByRef context As IForecastContext)
            vals(0) = CDbl(pc.Cohort0)
            vals(1) = CDbl(pc.Cohort1)
            vals(2) = CDbl(pc.Cohort2)
            vals(3) = CDbl(pc.Cohort3)
            vals(4) = CDbl(pc.Cohort4)
            vals(5) = CDbl(pc.Cohort5)
            vals(6) = CDbl(pc.Cohort6)
            vals(7) = CDbl(pc.Cohort7)
            vals(8) = CDbl(pc.Cohort8)
            vals(9) = CDbl(pc.Cohort9)
            vals(10) = CDbl(pc.Cohort10)
            vals(11) = CDbl(pc.Cohort11)
            vals(12) = CDbl(pc.Cohort12)
            vals(13) = CDbl(pc.Cohort13)
            vals(14) = CDbl(pc.Cohort14)
            vals(15) = CDbl(pc.Cohort15)
            vals(16) = CDbl(pc.Cohort16)
            vals(17) = CDbl(pc.Cohort17)
            vals(18) = CDbl(pc.Cohort18)
            vals(19) = CDbl(pc.Cohort19)
            vals(20) = CDbl(pc.Cohort20)
            vals(21) = CDbl(pc.Cohort21)
            'jrs 2024/12/20 added and pc.Cohort22.hasvalue
            If NCohorts > 21 AndAlso pc.Cohort22.HasValue Then
                vals(22) = CDbl(pc.Cohort22)
                vals(23) = CDbl(pc.Cohort23)
                vals(24) = CDbl(pc.Cohort24)
                vals(25) = CDbl(pc.Cohort25)
                'jrs 2026/01/26
                If NCohorts > 25 AndAlso pc.Cohort26.HasValue Then
                    vals(26) = CDbl(pc.Cohort26)
                    vals(27) = CDbl(pc.Cohort27)
                    vals(28) = CDbl(pc.Cohort28)
                    vals(29) = CDbl(pc.Cohort29)
                    vals(30) = CDbl(pc.Cohort30)
                    vals(31) = CDbl(pc.Cohort31)
                    'jrs 2024/12/23
                    If NCohorts > 31 AndAlso pc.Cohort32.HasValue Then
                        vals(32) = CDbl(pc.Cohort32)
                        vals(33) = CDbl(pc.Cohort33)
                        vals(34) = CDbl(pc.Cohort34)
                        vals(35) = CDbl(pc.Cohort35)
                        vals(36) = CDbl(pc.Cohort36)
                        vals(37) = CDbl(pc.Cohort37)
                        vals(38) = CDbl(pc.Cohort38)
                        vals(39) = CDbl(pc.Cohort39)
                        vals(40) = CDbl(pc.Cohort40)
                        vals(41) = CDbl(pc.Cohort41)
                    End If
                End If
            End If
        End Sub

        'jrs 2019/06/13
        Public Sub LoadPopCohortGlobalVar(ByRef vals As Single?(,), ByRef pc As PopCohortGlobal, ByRef context As IForecastContext, ByVal Dim2 As Short)
            vals(0, Dim2) = CSng(pc.Cohort0)
            vals(1, Dim2) = CSng(pc.Cohort1)
            vals(2, Dim2) = CSng(pc.Cohort2)
            vals(3, Dim2) = CSng(pc.Cohort3)
            vals(4, Dim2) = CSng(pc.Cohort4)
            vals(5, Dim2) = CSng(pc.Cohort5)
            vals(6, Dim2) = CSng(pc.Cohort6)
            vals(7, Dim2) = CSng(pc.Cohort7)
            vals(8, Dim2) = CSng(pc.Cohort8)
            vals(9, Dim2) = CSng(pc.Cohort9)
            vals(10, Dim2) = CSng(pc.Cohort10)
            vals(11, Dim2) = CSng(pc.Cohort11)
            vals(12, Dim2) = CSng(pc.Cohort12)
            vals(13, Dim2) = CSng(pc.Cohort13)
            vals(14, Dim2) = CSng(pc.Cohort14)
            vals(15, Dim2) = CSng(pc.Cohort15)
            vals(16, Dim2) = CSng(pc.Cohort16)
            vals(17, Dim2) = CSng(pc.Cohort17)
            vals(18, Dim2) = CSng(pc.Cohort18)
            vals(19, Dim2) = CSng(pc.Cohort19)
            vals(20, Dim2) = CSng(pc.Cohort20)
            vals(21, Dim2) = CSng(pc.Cohort21)
            'jrs 2024/12/20 added andalso pc.cohort22.hasvalue
            If NCohorts > 21 AndAlso pc.Cohort22.HasValue Then
                vals(22, Dim2) = CSng(pc.Cohort22)
                vals(23, Dim2) = CSng(pc.Cohort23)
                vals(24, Dim2) = CSng(pc.Cohort24)
                vals(25, Dim2) = CSng(pc.Cohort25)
                vals(26, Dim2) = CSng(pc.Cohort26)
                vals(27, Dim2) = CSng(pc.Cohort27)
                vals(28, Dim2) = CSng(pc.Cohort28)
                vals(29, Dim2) = CSng(pc.Cohort29)
                vals(30, Dim2) = CSng(pc.Cohort30)
                vals(31, Dim2) = CSng(pc.Cohort31)
                'jrs 2024/12/23
                'jrs 2024/12/23
                If NCohorts > 31 AndAlso pc.Cohort32.HasValue Then
                    vals(32, Dim2) = CSng(pc.Cohort32)
                    vals(33, Dim2) = CSng(pc.Cohort33)
                    vals(34, Dim2) = CSng(pc.Cohort34)
                    vals(35, Dim2) = CSng(pc.Cohort35)
                    vals(36, Dim2) = CSng(pc.Cohort36)
                    vals(37, Dim2) = CSng(pc.Cohort37)
                    vals(38, Dim2) = CSng(pc.Cohort38)
                    vals(39, Dim2) = CSng(pc.Cohort39)
                    vals(40, Dim2) = CSng(pc.Cohort40)
                    vals(41, Dim2) = CSng(pc.Cohort41)
                End If
            End If

        End Sub

        'jrs 2019/06/13
        Public Sub LoadPopCohortAIDSRegion(ByRef vals As Single(,,), ByRef pc As PopCohortAIDSRegion, ByRef context As IForecastContext)
            vals(0, pc.RegionId, pc.Sex) = CSng(pc.Cohort0)
            vals(1, pc.RegionId, pc.Sex) = CSng(pc.Cohort1)
            vals(2, pc.RegionId, pc.Sex) = CSng(pc.Cohort2)
            vals(3, pc.RegionId, pc.Sex) = CSng(pc.Cohort3)
            vals(4, pc.RegionId, pc.Sex) = CSng(pc.Cohort4)
            vals(5, pc.RegionId, pc.Sex) = CSng(pc.Cohort5)
            vals(6, pc.RegionId, pc.Sex) = CSng(pc.Cohort6)
            vals(7, pc.RegionId, pc.Sex) = CSng(pc.Cohort7)
            vals(8, pc.RegionId, pc.Sex) = CSng(pc.Cohort8)
            vals(9, pc.RegionId, pc.Sex) = CSng(pc.Cohort9)
            vals(10, pc.RegionId, pc.Sex) = CSng(pc.Cohort10)
            vals(11, pc.RegionId, pc.Sex) = CSng(pc.Cohort11)
            vals(12, pc.RegionId, pc.Sex) = CSng(pc.Cohort12)
            vals(13, pc.RegionId, pc.Sex) = CSng(pc.Cohort13)
            vals(14, pc.RegionId, pc.Sex) = CSng(pc.Cohort14)
            vals(15, pc.RegionId, pc.Sex) = CSng(pc.Cohort15)
            vals(16, pc.RegionId, pc.Sex) = CSng(pc.Cohort16)
            vals(17, pc.RegionId, pc.Sex) = CSng(pc.Cohort17)
            vals(18, pc.RegionId, pc.Sex) = CSng(pc.Cohort18)
            vals(19, pc.RegionId, pc.Sex) = CSng(pc.Cohort19)
            vals(20, pc.RegionId, pc.Sex) = CSng(pc.Cohort20)
            vals(21, pc.RegionId, pc.Sex) = CSng(pc.Cohort21)
            'jrs 2025/01/03  AndAlso pc.Cohort22.HasValue 
            If NCohorts > 21 AndAlso pc.Cohort22.HasValue Then
                vals(22, pc.RegionId, pc.Sex) = CSng(pc.Cohort22)
                vals(23, pc.RegionId, pc.Sex) = CSng(pc.Cohort23)
                vals(24, pc.RegionId, pc.Sex) = CSng(pc.Cohort24)
                vals(25, pc.RegionId, pc.Sex) = CSng(pc.Cohort25)
                'jrs 2026/01/26
                If NCohorts > 25 AndAlso pc.Cohort26.HasValue Then
                    vals(26, pc.RegionId, pc.Sex) = CSng(pc.Cohort26)
                    vals(27, pc.RegionId, pc.Sex) = CSng(pc.Cohort27)
                    vals(28, pc.RegionId, pc.Sex) = CSng(pc.Cohort28)
                    vals(29, pc.RegionId, pc.Sex) = CSng(pc.Cohort29)
                    vals(30, pc.RegionId, pc.Sex) = CSng(pc.Cohort30)
                    vals(31, pc.RegionId, pc.Sex) = CSng(pc.Cohort31)
                    'jrs 2024/12/23
                    If NCohorts > 31 AndAlso pc.Cohort32.HasValue Then
                        vals(32, pc.RegionId, pc.Sex) = CSng(pc.Cohort32)
                        vals(33, pc.RegionId, pc.Sex) = CSng(pc.Cohort33)
                        vals(34, pc.RegionId, pc.Sex) = CSng(pc.Cohort34)
                        vals(35, pc.RegionId, pc.Sex) = CSng(pc.Cohort35)
                        vals(36, pc.RegionId, pc.Sex) = CSng(pc.Cohort36)
                        vals(37, pc.RegionId, pc.Sex) = CSng(pc.Cohort37)
                        vals(38, pc.RegionId, pc.Sex) = CSng(pc.Cohort38)
                        vals(39, pc.RegionId, pc.Sex) = CSng(pc.Cohort39)
                        vals(40, pc.RegionId, pc.Sex) = CSng(pc.Cohort40)
                        vals(41, pc.RegionId, pc.Sex) = CSng(pc.Cohort41)
                    End If
                End If
            End If

        End Sub

        'jrs 2019/06/19
        Public Sub LoadFertCohReg(ByRef vals(,) As Double, ByVal Reg As CohortRegion4)
            Dim j As Integer
            For j = 0 To 2 'jrs 2023/12/05
                vals(j, Reg.RegionId) = 0
            Next
            vals(3, Reg.RegionId) = CDbl(Reg.Cohort3 / 1000) 'jrs 2023/12/05
            vals(4, Reg.RegionId) = CDbl(Reg.Cohort4 / 1000)
            vals(5, Reg.RegionId) = CDbl(Reg.Cohort5 / 1000)
            vals(6, Reg.RegionId) = CDbl(Reg.Cohort6 / 1000)
            vals(7, Reg.RegionId) = CDbl(Reg.Cohort7 / 1000)
            vals(8, Reg.RegionId) = CDbl(Reg.Cohort8 / 1000)
            vals(9, Reg.RegionId) = CDbl(Reg.Cohort9 / 1000)
            vals(10, Reg.RegionId) = CDbl(Reg.Cohort10 / 1000)
            If Reg.Cohort11.HasValue Then
                vals(11, Reg.RegionId) = CDbl(Reg.Cohort11 / 1000)
            End If
            If Reg.Cohort12.HasValue Then
                vals(12, Reg.RegionId) = CDbl(Reg.Cohort12 / 1000)
            End If
            If Reg.Cohort13.HasValue Then
                vals(13, Reg.RegionId) = CDbl(Reg.Cohort13 / 1000)
            End If
            If Reg.Cohort14.HasValue Then
                vals(14, Reg.RegionId) = CDbl(Reg.Cohort14 / 1000)
            End If
            If Reg.Cohort15.HasValue Then
                vals(15, Reg.RegionId) = CDbl(Reg.Cohort15 / 1000)
            End If
            If Reg.Cohort16.HasValue Then
                vals(16, Reg.RegionId) = CDbl(Reg.Cohort16 / 1000)
            End If
            If Reg.Cohort17.HasValue Then
                vals(17, Reg.RegionId) = CDbl(Reg.Cohort17 / 1000)
            End If
            If Reg.Cohort18.HasValue Then
                vals(18, Reg.RegionId) = CDbl(Reg.Cohort18 / 1000)
            End If
            If Reg.Cohort19.HasValue Then
                vals(19, Reg.RegionId) = CDbl(Reg.Cohort19 / 1000)
            End If
            If Reg.Cohort20.HasValue Then
                vals(20, Reg.RegionId) = CDbl(Reg.Cohort20 / 1000)
            End If
            If Reg.Cohort21.HasValue Then
                vals(21, Reg.RegionId) = CDbl(Reg.Cohort21 / 1000)
            End If
            If NCohorts > 21 Then
                If Reg.Cohort22.HasValue Then
                    vals(22, Reg.RegionId) = CDbl(Reg.Cohort22 / 1000)
                End If
                If Reg.Cohort23.HasValue Then
                    vals(23, Reg.RegionId) = CDbl(Reg.Cohort23 / 1000)
                End If
                If Reg.Cohort24.HasValue Then
                    vals(24, Reg.RegionId) = CDbl(Reg.Cohort24 / 1000)
                End If
                If Reg.Cohort25.HasValue Then
                    vals(25, Reg.RegionId) = CDbl(Reg.Cohort25 / 1000)
                End If
                If Reg.Cohort26.HasValue Then
                    vals(26, Reg.RegionId) = CDbl(Reg.Cohort26 / 1000)
                End If
                If Reg.Cohort27.HasValue Then
                    vals(27, Reg.RegionId) = CDbl(Reg.Cohort27 / 1000)
                End If
                If Reg.Cohort28.HasValue Then
                    vals(28, Reg.RegionId) = CDbl(Reg.Cohort28 / 1000)
                End If
                If Reg.Cohort29.HasValue Then
                    vals(29, Reg.RegionId) = CDbl(Reg.Cohort29 / 1000)
                End If
                If Reg.Cohort30.HasValue Then
                    vals(30, Reg.RegionId) = CDbl(Reg.Cohort30 / 1000)
                End If
                If Reg.Cohort31.HasValue Then
                    vals(31, Reg.RegionId) = CDbl(Reg.Cohort31 / 1000)
                End If
                If Reg.Cohort32.HasValue Then
                    vals(32, Reg.RegionId) = CDbl(Reg.Cohort32 / 1000)
                End If
                If Reg.Cohort33.HasValue Then
                    vals(33, Reg.RegionId) = CDbl(Reg.Cohort33 / 1000)
                End If
                If Reg.Cohort34.HasValue Then
                    vals(34, Reg.RegionId) = CDbl(Reg.Cohort34 / 1000)
                End If
                If Reg.Cohort35.HasValue Then
                    vals(35, Reg.RegionId) = CDbl(Reg.Cohort35 / 1000)
                End If
                If Reg.Cohort36.HasValue Then
                    vals(36, Reg.RegionId) = CDbl(Reg.Cohort36 / 1000)
                End If
                If Reg.Cohort37.HasValue Then
                    vals(37, Reg.RegionId) = CDbl(Reg.Cohort37 / 1000)
                End If
                If Reg.Cohort38.HasValue Then
                    vals(38, Reg.RegionId) = CDbl(Reg.Cohort38 / 1000)
                End If
                If Reg.Cohort39.HasValue Then
                    vals(39, Reg.RegionId) = CDbl(Reg.Cohort39 / 1000)
                End If
                If Reg.Cohort40.HasValue Then
                    vals(40, Reg.RegionId) = CDbl(Reg.Cohort40 / 1000)
                End If
                If Reg.Cohort41.HasValue Then
                    vals(41, Reg.RegionId) = CDbl(Reg.Cohort41 / 1000)
                End If
            End If

            For j = LastCohortFert + 1 To NCohorts
                vals(j, Reg.RegionId) = 0
            Next

        End Sub

        'jrs 2023/10/16
        Dim SEPOP() As Single
        Dim SEGDPPOT() As Single
        Dim SEDEMOCPOLYARCHY() As Single
        Dim TMPVal As Single

        'jrs 2023/10/12
        'Implementing new ifsexomodeltype = 3 to try and load all available history and continue with endogenous growth after
        'jrs 2014/08/26
        'mti 2023/10/27
        Public Sub ReplaceVarWithExogenousForecastxReg(sVarName As String, sDim2 As String, sDim3 As String, sYr As String, iReg As Integer,
        context As IForecastContext, Optional ByRef bGDPPCPExogenous As Boolean = False, Optional SGDPR As Single = 0, Optional ByRef ExogenousYrNo As Integer = -5)
            'Check if table exist otherwise leave
            If Not IFsVarsExTbls.ContainsKey(sVarName & sDim2 & sDim3) Then Exit Sub
            'Load exogenous forecast table
            Dim tbl As IDictionary(Of String, Single?())
            tbl = IFsVarsExTbls.Item(sVarName & sDim2 & sDim3)
            Dim yrs As Short()
            yrs = IFsVarsExTblsYrs.Item(sVarName & sDim2 & sDim3)

            'dsr 2016/05/04 Need to read in Exogenous POP to calculate crude birth and death rates when using annual changes
            Dim tbl2 As IDictionary(Of String, Single?())
            Dim yrs2 As Short()
            Dim YrIdx2 As Short
            If CInt(ifsexomodeltype) = 2 AndAlso (sVarName = "BIRTHS" OrElse sVarName = "DEATHS") Then 'ttt 2019/10/08 changed to AndAlso and OrElse
                tbl2 = IFsVarsExTbls.Item("POP" & sDim2 & sDim3)
                yrs2 = IFsVarsExTblsYrs.Item("POP" & sDim2 & sDim3)
                YrIdx2 = getYearIdx(yrs2, CShort(sYr))
            End If
            'dsr 2016/05/04 Dimension GDPorg and ii here to avoid duplication
            Dim GDPorg As Single, ii As Integer
            Dim YrIdx As Short
            YrIdx = getYearIdx(yrs, CShort(sYr))
            'jrs 2023/10/16 added and CInt(ifsexomodeltype) <> 3
            If YrIdx = -1 And CInt(ifsexomodeltype) <> 3 Then
                'jrs 2024/03/12
                If sVarName = "GDPPCP" Then
                    'jrs 2023/10/24
                    'If ifsexomodeltype = 2 And sVarName = "GDPPCP" Then
                    If ifsexomodeltype = 2 Then
                        sGDPPCP(iReg) = GDPPCP(iReg)
                    End If
                    bGDPPCPExogenous = True
                End If

                Exit Sub
            End If

            'jrs 2024/05/31 *** start ***
            If sVarName = "GDPPCP" Then
                If Not tbl(iReg.ToString)(YrIdx).HasValue Then
                    Exit Sub
                End If
                'jrs 2025/04/15
                If IFsVarsExTbls.ContainsKey("GDP") Then
                    tbl2 = IFsVarsExTbls.Item("GDP")
                    yrs2 = IFsVarsExTblsYrs.Item("GDP")
                    YrIdx2 = getYearIdx(yrs2, CShort(sYr))
                End If
            End If
            'jrs 2024/05/31 *** end ***
            'mti 2023/10/27 start
            ExogenousYrNo = YrIdx
            'If YrIdx = 0 AndAlso ifsexomodeltype = 2 Then       'growth rtae based exogenous; first exogenous point
            '    If sVarName = "GDPPCP" Then
            '        sGDPPCP(iReg) = GDPPCP(iReg)
            '        bGDPPCPExogenous = False
            '    End If
            'End If
            'mti 2023/10/27 end

            Dim sCtry As String
            'jrs 2019/06/19
            'sCtry = need to implement function to read first country in Region iReg
            '***************************************************************************
            '***************************************************************************
            '***************************************************************************
            'ttt 2019/10/08 
            'jrs 2020/07/14 sCtry = context.GetDimension(1).Buckets.Item(CShort(iReg))
            sCtry = iReg.ToString

            'dsr 2016/05/04 Use existing code when using absolute values from exogenous series
            If CInt(ifsexomodeltype) = 1 Then
                If tbl(sCtry)(YrIdx).HasValue Then
                    If sVarName = "TFR" Then
                        TFR(iReg) = CSng(tbl(sCtry)(YrIdx))
                        'dsr 2016/03/24 Next two lines added to read in total population; only done in first year
                    ElseIf sVarName = "POP" Then
                        POP(iReg) = CSng(tbl(sCtry)(YrIdx))
                        'jrs 2022/01/25
                        AdjustCohPop(agedst, POP, iReg)
                        IFsPOPSpreadAgeDst(iReg, context)
                    ElseIf sVarName = "POPURBAN" Then
                        POPURBAN(iReg) = CSng(tbl(sCtry)(YrIdx) * POP(iReg) / 100) 'jrs 2014/09/11
                        'dsr 2016/03/24 Next four lines added to read in total deaths and births
                    ElseIf sVarName = "DEATHS" Then
                        DEATHS(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "BIRTHS" Then
                        BIRTHS(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "EDYRSAG15" Then
                        If sDim2 = "Male" Then
                            EDYRSAG15(iReg, 1) = CSng(tbl(sCtry)(YrIdx))
                        ElseIf sDim2 = "Female" Then
                            EDYRSAG15(iReg, 2) = CSng(tbl(sCtry)(YrIdx))
                        Else
                            EDYRSAG15(iReg, 3) = CSng(tbl(sCtry)(YrIdx))
                        End If
                    ElseIf sVarName = "EDYRSAG25" Then
                        If sDim2 = "Male" Then
                            EDYRSAG25(iReg, 1) = CSng(tbl(sCtry)(YrIdx))
                        ElseIf sDim2 = "Female" Then
                            EDYRSAG25(iReg, 2) = CSng(tbl(sCtry)(YrIdx))
                        Else
                            EDYRSAG25(iReg, 3) = CSng(tbl(sCtry)(YrIdx))
                        End If
                    ElseIf sVarName = "GDPPCP" Then
                        GDPPCP(iReg) = CSng(tbl(sCtry)(YrIdx) / 1000)
                        GDPP(iReg) = GDPPCP(iReg) * POP(iReg)
                        'jrs 2025/04/15
                        If IFsVarsExTbls.ContainsKey("GDP") Then
                            If CSng(tbl2(sCtry)(YrIdx2)) > 0 Then
                                PPPCONV(iReg) = GDPP(iReg) / CSng(tbl2(sCtry)(YrIdx2))
                            End If
                        End If
                        GDPPC(iReg) = GDPPCP(iReg) / PPPCONV(iReg)
                        'Dim GDPorg As Single, ii As Integer
                        GDPorg = GDP(iReg)
                        GDP(iReg) = GDPPC(iReg) * POP(iReg)
                        If CInt(sYr) > context.FirstYear Then
                            gdprext(iReg) = GDP(iReg) / SGDPR * 100 - 100
                            GDP(iReg) = 0
                        Else
                            bGDPPCPExogenous = True
                            For ii = 1 To NSec
                                VADD(iReg, ii) = VADD(iReg, ii) * GDP(iReg) / GDPorg
                                ZS(iReg, ii) = ZS(iReg, ii) * GDP(iReg) / GDPorg
                            Next ii
                        End If
                    ElseIf sVarName = "migrater" Then 'jrs 2017/11/06
                        migrater(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "MIGRATEIN" Then 'jrs 2020/05/11
                        MIGRATEIN(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "MIGRATEOUT" Then 'jrs 2020/05/11
                        MIGRATEOUT(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "MIGRANTS" Then 'jrs 2017/11/06
                        MIGRANTS(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "MIGRANTSIN" Then 'jrs 2020/04/13
                        MIGRANTSIN(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "MIGRANTSOUT" Then 'jrs 2017/11/06
                        MIGRANTSOUT(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "C" Then 'jrs 2019/05/03
                        C(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "SFINTLWARALL" Then 'jrs 2021/08/04
                        SFINTLWARALL(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "SFCIVILWARUCDP" Then 'jrs 2024/05/20
                        SFCIVILWARUCDP(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "ENVTPCHG" Then 'jrs 2022/03/21
                        ENVTPCHG(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "ENVPRCHG" Then 'jrs 2022/10/17
                        ENVPRCHG(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "WTEMP" Then 'jrs 2022/05/23
                        WTEMP = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "GINIDOM" Then 'jrs 2022/03/29
                        GINIDOM(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "DEMOCPOLYARCHY" Then 'jrs 2023/10/23
                        DEMOCPOLYARCHY(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "CARANN" Then 'jrs 2023/10/23
                        CARANN(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "YL" Then 'jrs 2023/10/23
                        YL(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "I" Then 'jrs 2023/10/23
                        I(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "ENDEM" Then 'jrs 2023/10/24
                        ENDEM(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "INFRAELEC" Then 'jrs 2023/10/24
                        INFRAELEC(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "INFRAELECACC" Then 'jrs 2023/11/10
                        Dim DiffPCT As Single
                        If INFRAELECACC(iReg, Total) <> 0 Then
                            DiffPCT = CSng(tbl(sCtry)(YrIdx) / INFRAELECACC(iReg, Total))
                        Else
                            DiffPCT = 1
                        End If

                        For ii = 1 To Total
                            INFRAELECACC(iReg, ii) = INFRAELECACC(iReg, ii) * DiffPCT
                        Next
                    ElseIf sVarName = "LD" Then 'jrs 2023/10/24 crop
                        If sDim2 = "Crop" Then
                            Dim Diff As Single, TotLandMinusCrop As Single
                            Diff = CSng(tbl(sCtry)(YrIdx)) - LD(iReg, Crop)
                            TotLandMinusCrop = 0
                            For ii = 2 To 5
                                TotLandMinusCrop = TotLandMinusCrop + LD(iReg, ii)
                            Next

                            LD(iReg, Crop) = CSng(tbl(sCtry)(YrIdx))
                            If TotLandMinusCrop > 0 Then
                                For ii = 2 To 5
                                    LD(iReg, ii) = LD(iReg, ii) - Diff * LD(iReg, ii) / TotLandMinusCrop
                                Next
                            End If
                            For ii = 1 To 5
                                sld(iReg, ii) = LD(iReg, ii)
                            Next
                        End If
                    ElseIf sVarName = "ENP" Then 'jrs 2023/10/24 
                        If sDim2 = "Total" Then
                            Dim Diff As Single, ENP_TOT As Single
                            ENP_TOT = 0
                            For ii = 1 To Renew
                                ENP_TOT = ENP_TOT + ENP(iReg, ii)
                            Next
                            Diff = CSng(tbl(sCtry)(YrIdx)) - ENP_TOT

                            If ENP_TOT > 0 Then
                                For ii = 1 To Renew
                                    ENP(iReg, ii) = ENP(iReg, ii) + Diff * ENP(iReg, ii) / ENP_TOT
                                Next
                            End If
                        ElseIf sDim2 = "OthRenew" Then 'jrs 2023/11/10
                            ENP(iReg, Renew) = CSng(tbl(sCtry)(YrIdx))
                        End If
                    ElseIf sVarName = "SANITATION" Then 'jrs 2023/10/30
                        If sDim2 = "Improved" Then
                            Dim Diff As Single, Tot As Single
                            Diff = CSng(tbl(sCtry)(YrIdx)) - SANITATION(iReg, 3)
                            Tot = 0
                            For ii = 1 To 2
                                Tot = Tot + SANITATION(iReg, ii)
                            Next

                            SANITATION(iReg, 3) = CSng(tbl(sCtry)(YrIdx))
                            If Tot > 0 Then
                                For ii = 1 To 2
                                    SANITATION(iReg, ii) = SANITATION(iReg, ii) - Diff * SANITATION(iReg, ii) / Tot
                                Next
                            End If
                        End If
                    ElseIf sVarName = "WATSAFE" Then 'jrs 2023/10/30
                        If sDim2 = "Piped" Then
                            Dim Diff As Single, Tot As Single
                            Diff = CSng(tbl(sCtry)(YrIdx)) - WATSAFE(iReg, 3)
                            Tot = 0
                            For ii = 1 To 2
                                Tot = Tot + WATSAFE(iReg, ii)
                            Next

                            WATSAFE(iReg, 3) = CSng(tbl(sCtry)(YrIdx))
                            If Tot > 0 Then
                                For ii = 1 To 2
                                    WATSAFE(iReg, ii) = WATSAFE(iReg, ii) - Diff * WATSAFE(iReg, ii) / Tot
                                Next
                            End If
                        End If
                    ElseIf sVarName = "AGP" Then 'jrs 2023/11/10
                        Dim Diff As Single, AGP_TOT As Single
                        AGP_TOT = 0
                        For ii = 1 To 3
                            AGP_TOT = AGP_TOT + AGP(iReg, ii)
                        Next
                        Diff = CSng(tbl(sCtry)(YrIdx)) - AGP_TOT

                        If AGP_TOT > 0 Then
                            For ii = 1 To 3
                                AGP(iReg, ii) = AGP(iReg, ii) + Diff * AGP(iReg, ii) / AGP_TOT
                            Next
                        End If
                    ElseIf sVarName = "FDEM" Then 'jrs 2023/12/01
                        Dim Diff As Single, FDEM_TOT As Single
                        FDEM_TOT = 0
                        For ii = 1 To 3
                            FDEM_TOT = FDEM_TOT + FDEM(iReg, ii)
                        Next
                        Diff = CSng(tbl(sCtry)(YrIdx)) - FDEM_TOT

                        If FDEM_TOT > 0 Then
                            For ii = 1 To 3
                                FDEM(iReg, ii) = FDEM(iReg, ii) + Diff * FDEM(iReg, ii) / FDEM_TOT
                            Next
                        End If
                    ElseIf sVarName = "COOKSTOVES" Then 'jrs 2023/12/05
                        If sDim2 = "Traditional" Then
                            COOKSTOVES(iReg, 1) = CSng(tbl(sCtry)(YrIdx))
                        End If
                    ElseIf sVarName = "GOVCORRUPT" Then 'jrs 2024/02/29
                        GOVCORRUPT(iReg) = CSng(tbl(sCtry)(YrIdx))
                    ElseIf sVarName = "GOVEFFECT" Then 'jrs 2024/02/29
                        GOVEFFECT(iReg) = CSng(tbl(sCtry)(YrIdx))
                    End If
                End If

                'dsr 2016/05/04 Main block of new code for when use annual percentage changes from exogenous series - start
            ElseIf CInt(ifsexomodeltype) = 2 Then
                If tbl(sCtry)(YrIdx).HasValue Then
                    Dim popexogprev As Single, popexogpres As Single, cdrexogprev As Single, cdrexogpres As Single, newCDR As Single
                    Dim cbrexogprev As Single, cbrexogpres As Single, newCBR As Single
                    If CInt(sYr) = context.FirstYear And sVarName = "GDPPCP" Then
                        sGDPPCP(iReg) = GDPPCP(iReg)
                        bGDPPCPExogenous = True
                        'jrs 2023/09/25 added  AndAlso YrIdx > 0 AndAlso tbl(sCtry)(YrIdx-1).HasValue 
                    ElseIf CInt(sYr) > context.FirstYear AndAlso YrIdx > 0 AndAlso tbl(sCtry)(YrIdx - 1).HasValue Then
                        If sVarName = "TFR" Then
                            TFR(iReg) = CSng(stfr(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "POPURBAN" Then
                            POPURBAN(iReg) = CSng(SPOPURBAN(iReg) * (tbl(sCtry)(YrIdx) * POP(iReg) / 100) _
                               / AMAX(0.000001, (tbl(sCtry)(YrIdx - 1) * sPOPTotal(iReg) / 100)))
                        ElseIf sVarName = "DEATHS" Then
                            popexogprev = CSng(tbl2(sCtry)(YrIdx2 - 1))
                            popexogpres = CSng(tbl2(sCtry)(YrIdx2))
                            cdrexogprev = CSng(tbl(sCtry)(YrIdx - 1) / AMAX(0.000001, popexogprev))
                            cdrexogpres = CSng(tbl(sCtry)(YrIdx) / AMAX(0.000001, popexogpres))
                            newCDR = SCDR(iReg) * cdrexogpres / AMAX(0.000001, cdrexogprev)
                            DEATHS(iReg) = newCDR / 1000.0! * POP(iReg)
                        ElseIf sVarName = "BIRTHS" Then
                            popexogprev = CSng(tbl2(sCtry)(YrIdx2 - 1))
                            popexogpres = CSng(tbl2(sCtry)(YrIdx2))
                            cbrexogprev = CSng(tbl(sCtry)(YrIdx - 1) / AMAX(0.000001, popexogprev))
                            cbrexogpres = CSng(tbl(sCtry)(YrIdx) / AMAX(0.000001, popexogpres))
                            newCBR = SCBR(iReg) * cbrexogpres / AMAX(0.000001, cbrexogprev)
                            BIRTHS(iReg) = newCBR / 1000.0! * POP(iReg)
                        ElseIf sVarName = "EDYRSAG15" Then
                            If sDim2 = "Male" Then
                                EDYRSAG15(iReg, 1) = CSng(SEDYRSAG15v2(iReg, 1) *
                                     tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            ElseIf sDim2 = "Female" Then
                                EDYRSAG15(iReg, 2) = CSng(SEDYRSAG15v2(iReg, 2) *
                                     tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            Else
                                EDYRSAG15(iReg, 3) = CSng(SEDYRSAG15v2(iReg, 3) *
                                     tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "EDYRSAG25" Then
                            If sDim2 = "Male" Then
                                EDYRSAG25(iReg, 1) = CSng(SEDYRSAG25(iReg, 1) *
                                     tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            ElseIf sDim2 = "Female" Then
                                EDYRSAG25(iReg, 2) = CSng(SEDYRSAG25(iReg, 2) *
                                     tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            Else
                                EDYRSAG25(iReg, 3) = CSng(SEDYRSAG25(iReg, 3) *
                                     tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "GDPPCP" Then
                            'mti 2022/01/13
                            If gdpadjsw = 0 Or gdprext(iReg) < -99 Then
                                'If GRPS(1, iReg) = "Georgia" And IY = 3 Then Stop
                                GDPPCP(iReg) = CSng(sGDPPCP(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                                GDPP(iReg) = GDPPCP(iReg) * POP(iReg)
                                'jrs 2025/04/15
                                If IFsVarsExTbls.ContainsKey("GDP") Then
                                    If CSng(tbl2(sCtry)(YrIdx2)) > 0 Then
                                        PPPCONV(iReg) = GDPP(iReg) / CSng(tbl2(sCtry)(YrIdx2))
                                    End If
                                End If
                                GDPPC(iReg) = GDPPCP(iReg) / AMAX(0.000001, PPPCONV(iReg))
                                sGDPPCP(iReg) = GDPPCP(iReg)
                                GDPorg = GDP(iReg)
                                GDP(iReg) = GDPPC(iReg) * POP(iReg)
                                gdprext(iReg) = GDP(iReg) / AMAX(0.000001, SGDPR) * 100 - 100
                                'jrs 2025/04/15
                                'update VADD
                                If IFsVarsExTbls.ContainsKey("GDP") Then
                                    If CSng(tbl2(sCtry)(YrIdx2)) > 0 Then
                                        'Adjust the physical sectors
                                        VADD(iReg, 1) = sVADD(iReg, 1) * (1 + gdprext(iReg) / 100)
                                        VADD(iReg, 2) = sVADD(iReg, 2) * (1 + gdprext(iReg) / 100)
                                    End If
                                End If
                                GDP(iReg) = 0
                            Else
                                'exogenous growth rate; implement without adjustment; SGDPR -> previouys years GDP MER sent as an argument to this code routine
                                GDP(iReg) = SGDPR * (1 + gdprext(iReg) / 100)
                                GDPPC(iReg) = GDP(iReg) / POP(iReg)
                                GDPorg = GDP(iReg)
                                GDPPCP(iReg) = GDPPC(iReg) * PPPCONV(iReg)
                                sGDPPCP(iReg) = GDPPCP(iReg)
                                GDPP(iReg) = GDPPCP(iReg) * POP(iReg)
                                GDP(iReg) = 0
                            End If
                        ElseIf sVarName = "migrater" Then 'jrs 2017/11/06
                            'jrs 2017/11/15
                            If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(tbl(sCtry)(YrIdx - 1))) Then
                                'when there's a change of sign keep constant
                                If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(smigrater(iReg))) Then
                                    migrater(iReg) = -1 * smigrater(iReg)
                                Else
                                    migrater(iReg) = smigrater(iReg)
                                End If
                            Else
                                migrater(iReg) = CSng(smigrater(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "MIGRATEIN" Then 'jrs 2020/05/11
                            'jrs 2017/11/15
                            If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(tbl(sCtry)(YrIdx - 1))) Then
                                'when there's a change of sign keep constant
                                If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(smigrater(iReg))) Then
                                    MIGRATEIN(iReg) = -1 * SMigrateIn(iReg)
                                Else
                                    MIGRATEIN(iReg) = SMigrateIn(iReg)
                                End If
                            Else
                                MIGRATEIN(iReg) = CSng(SMigrateIn(iReg) * tbl(sCtry)(YrIdx) / AMAX(tbl(sCtry)(YrIdx - 1), 0.000001))
                            End If
                        ElseIf sVarName = "MIGRATEOUT" Then 'jrs 2020/05/11
                            'jrs 2017/11/15
                            If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(tbl(sCtry)(YrIdx - 1))) Then
                                'when there's a change of sign keep constant
                                If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(smigrater(iReg))) Then
                                    MIGRATEOUT(iReg) = -1 * SMigrateOut(iReg)
                                Else
                                    MIGRATEOUT(iReg) = SMigrateOut(iReg)
                                End If
                            Else
                                MIGRATEOUT(iReg) = CSng(SMigrateOut(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "MIGRANTS" Then 'jrs 2017/11/06
                            'If GRPS(1, iReg) = "Korea, Republic of" Then Stop
                            'jrs 2017/11/15
                            If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(tbl(sCtry)(YrIdx - 1))) Then
                                'when there's a change of sign keep constant
                                If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(smigrants(iReg)) Then
                                    MIGRANTS(iReg) = -1 * smigrants(iReg)
                                Else
                                    MIGRANTS(iReg) = smigrants(iReg)
                                End If
                            Else
                                MIGRANTS(iReg) = CSng(smigrants(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "MIGRANTSIN" Then 'jrs 2020/04/13
                            If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(tbl(sCtry)(YrIdx - 1))) Then
                                'when there's a change of sign keep constant
                                If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(SMigrantsIn(iReg)) Then
                                    MIGRANTSIN(iReg) = -1 * SMigrantsIn(iReg)
                                Else
                                    MIGRANTSIN(iReg) = SMigrantsIn(iReg)
                                End If
                            Else
                                MIGRANTSIN(iReg) = CSng(SMigrantsIn(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "MIGRANTSOUT" Then 'jrs 2020/04/13
                            If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(CSng(tbl(sCtry)(YrIdx - 1))) Then
                                'when there's a change of sign keep constant
                                If Sign(CSng(tbl(sCtry)(YrIdx))) <> Sign(SMigrantsOut(iReg)) Then
                                    MIGRANTSOUT(iReg) = -1 * SMigrantsOut(iReg)
                                Else
                                    MIGRANTSOUT(iReg) = SMigrantsOut(iReg)
                                End If
                            Else
                                MIGRANTSOUT(iReg) = CSng(SMigrantsOut(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "POP" Then 'jrs 2017/11/06
                            POP(iReg) = CSng(sPOPTotal(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            'jrs 2022/01/25
                            AdjustCohPop(agedst, POP, iReg)
                            IFsPOPSpreadAgeDst(iReg, context)
                        ElseIf sVarName = "C" Then 'jrs 2019/05/03
                            C(iReg) = CSng(sc(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "SFINTLWARALL" Then 'jrs 2021/08/04
                            SFINTLWARALL(iReg) = CSng(ssfintlwarall(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "SFCIVILWARUCDP" Then 'jrs 2024/05/20
                            SFCIVILWARUCDP(iReg) = CSng(sSFCIVILWARUCDP(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "ENVTPCHG" Then 'jrs 2022/03/21
                            ENVTPCHG(iReg) = CSng(SENVTPCHG(iReg) * tbl(sCtry)(YrIdx) / tbl(sCtry)(YrIdx - 1))
                        ElseIf sVarName = "ENVPRCHG" Then 'jrs 2022/10/17
                            ENVPRCHG(iReg) = CSng(SENVPRCHG(iReg) * tbl(sCtry)(YrIdx) / tbl(sCtry)(YrIdx - 1))
                        ElseIf sVarName = "WTEMP" Then 'jrs 2022/05/23
                            WTEMP = CSng(SWTEMP * tbl(sCtry)(YrIdx) / tbl(sCtry)(YrIdx - 1))
                        ElseIf sVarName = "GINIDOM" Then 'jrs 2022/03/29
                            GINIDOM(iReg) = CSng(sGINIDOM(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "DEMOCPOLYARCHY" Then 'jrs 2023/10/23
                            DEMOCPOLYARCHY(iReg) = CSng(SDEMOCPOLYARCHY(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "CARANN" Then 'jrs 2023/10/23
                            CARANN(iReg) = CSng(SCARANN(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "YL" Then 'jrs 2023/10/23
                            YL(iReg) = CSng(syl(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "I" Then 'jrs 2023/10/23
                            I(iReg) = CSng(SI(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "ENDEM" Then 'jrs 2023/10/24
                            ENDEM(iReg) = CSng(SEnDem(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "INFRAELEC" Then 'jrs 2023/10/24
                            INFRAELEC(iReg) = CSng(SINFRAELEC(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "INFRAELECACC" Then 'jrs 2023/11/10
                            Dim DiffPCT As Single
                            If INFRAELECACC(iReg, Total) <> 0 Then
                                DiffPCT = CSng(sINFRAELECACC(iReg, Total) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) / INFRAELECACC(iReg, Total)
                            Else
                                DiffPCT = 1
                            End If

                            For ii = 1 To Total
                                INFRAELECACC(iReg, ii) = INFRAELECACC(iReg, ii) * DiffPCT
                            Next
                        ElseIf sVarName = "LD" Then 'jrs 2023/10/24 crop
                            If sDim2 = "Crop" Then
                                Dim Diff As Single, TotLandMinusCrop As Single
                                Diff = CSng(ssld(iReg, Crop) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) - LD(iReg, Crop)
                                TotLandMinusCrop = 0
                                For ii = 2 To 5
                                    TotLandMinusCrop = TotLandMinusCrop + LD(iReg, ii)
                                Next

                                LD(iReg, Crop) = CSng(ssld(iReg, Crop) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))

                                If TotLandMinusCrop > 0 Then
                                    For ii = 2 To 5
                                        LD(iReg, ii) = LD(iReg, ii) - Diff * LD(iReg, ii) / TotLandMinusCrop
                                        'jrs 2023/12/15 avoid negatives
                                        If LD(iReg, ii) < 0.001 Then
                                            For JJ = 1 To ii
                                                LD(iReg, JJ) = sld(iReg, JJ)
                                            Next
                                            Exit For
                                        End If
                                    Next
                                End If
                                For ii = 1 To 5
                                    sld(iReg, ii) = LD(iReg, ii)
                                    ssld(iReg, ii) = LD(iReg, ii)
                                Next
                            End If
                        ElseIf sVarName = "ENP" Then 'jrs 2023/10/24 
                            If sDim2 = "Total" Then
                                Dim Diff As Single, ENP_TOT As Single
                                ENP_TOT = 0
                                For ii = 1 To Renew
                                    ENP_TOT = ENP_TOT + ENP(iReg, ii)
                                Next
                                Diff = CSng(SENPTOT(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) - ENP_TOT

                                If ENP_TOT > 0 Then
                                    For ii = 1 To Renew
                                        ENP(iReg, ii) = ENP(iReg, ii) + Diff * ENP(iReg, ii) / ENP_TOT
                                    Next
                                End If
                            ElseIf sDim2 = "OthRenew" Then 'jrs 2023/11/10
                                ENP(iReg, Renew) = CSng(SENPRENEW(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "SANITATION" Then 'jrs 2023/10/26
                            If sDim2 = "Improved" Then
                                Dim Diff As Single, Tot As Single
                                Diff = CSng(SSANITATION(iReg, 3) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) - SANITATION(iReg, 3)
                                Tot = 0
                                For ii = 1 To 2
                                    Tot = Tot + SANITATION(iReg, ii)
                                Next

                                SANITATION(iReg, 3) = CSng(SSANITATION(iReg, 3) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                                If Tot > 0 Then
                                    For ii = 1 To 2
                                        SANITATION(iReg, ii) = SANITATION(iReg, ii) - Diff * SANITATION(iReg, ii) / Tot
                                    Next
                                End If
                            End If
                        ElseIf sVarName = "WATSAFE" Then 'jrs 2023/10/26
                            If sDim2 = "Piped" Then
                                Dim Diff As Single, Tot As Single
                                Diff = CSng(SWATSAFE(iReg, 3) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) - WATSAFE(iReg, 3)
                                Tot = 0
                                For ii = 1 To 2
                                    Tot = Tot + WATSAFE(iReg, ii)
                                Next

                                WATSAFE(iReg, 3) = CSng(SWATSAFE(iReg, 3) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                                If Tot > 0 Then
                                    For ii = 1 To 2
                                        WATSAFE(iReg, ii) = WATSAFE(iReg, ii) - Diff * WATSAFE(iReg, ii) / Tot
                                    Next
                                End If
                            End If
                        ElseIf sVarName = "AGP" Then 'jrs 2023/11/10
                            Dim Diff As Single, AGP_TOT As Single
                            AGP_TOT = 0
                            For ii = 1 To 3
                                AGP_TOT = AGP_TOT + AGP(iReg, ii)
                            Next
                            Diff = CSng(SAGPTOT(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) - AGP_TOT

                            If AGP_TOT > 0 Then
                                For ii = 1 To 3
                                    AGP(iReg, ii) = AGP(iReg, ii) + Diff * AGP(iReg, ii) / AGP_TOT
                                Next
                            End If
                        ElseIf sVarName = "FDEM" Then 'jrs 2023/12/01
                            Dim Diff As Single, FDEM_TOT As Single
                            FDEM_TOT = 0
                            For ii = 1 To 3
                                FDEM_TOT = FDEM_TOT + FDEM(iReg, ii)
                            Next
                            Diff = CSng(SFDEMTOT(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1))) - FDEM_TOT

                            If FDEM_TOT > 0 Then
                                For ii = 1 To 3
                                    FDEM(iReg, ii) = FDEM(iReg, ii) + Diff * FDEM(iReg, ii) / FDEM_TOT
                                Next
                            End If
                        ElseIf sVarName = "COOKSTOVES" Then 'jrs 2023/12/05
                            If sDim2 = "Traditional" Then
                                COOKSTOVES(iReg, 1) = CSng(SCOOKSTOVES(iReg, 1) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                            End If
                        ElseIf sVarName = "GOVCORRUPT" Then 'jrs 2024/02/29
                            GOVCORRUPT(iReg) = CSng(sgovcorrupt(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        ElseIf sVarName = "GOVEFFECT" Then 'jrs 2024/02/29
                            GOVEFFECT(iReg) = CSng(SGoveffect(iReg) * tbl(sCtry)(YrIdx) / AMAX(0.000001, tbl(sCtry)(YrIdx - 1)))
                        End If
                    End If
                End If
            ElseIf CInt(ifsexomodeltype) = 3 Then 'jrs 2023/10/12 load all available history and continue with endogenous growth after
                If IY = 1 And iReg = 1 Then
                    ReDim SEPOP(NR)
                    ReDim SEGDPPOT(NR)
                    ReDim SEDEMOCPOLYARCHY(NR)
                End If
                If YrIdx >= 0 AndAlso tbl(sCtry)(YrIdx).HasValue Then
                    If sVarName = "POP" Then
                        SEPOP(iReg) = POP(iReg)
                        POP(iReg) = CSng(tbl(sCtry)(YrIdx))
                        AdjustCohPop(agedst, POP, iReg)
                        IFsPOPSpreadAgeDst(iReg, context)
                    ElseIf sVarName = "GDPPCP" Then
                        SEGDPPOT(iReg) = GDPPOT(iReg)
                        GDPPCP(iReg) = CSng(tbl(sCtry)(YrIdx) / 1000)
                        GDPP(iReg) = GDPPCP(iReg) * POP(iReg)
                        GDPPC(iReg) = GDPPCP(iReg) / PPPCONV(iReg)
                        'Dim GDPorg As Single, ii As Integer
                        GDPorg = GDP(iReg)
                        GDP(iReg) = GDPPC(iReg) * POP(iReg)
                        If CInt(sYr) > context.FirstYear Then
                            gdprext(iReg) = GDP(iReg) / SGDPR * 100 - 100
                            GDP(iReg) = 0
                        Else
                            bGDPPCPExogenous = True
                            For ii = 1 To NSec
                                VADD(iReg, ii) = VADD(iReg, ii) * GDP(iReg) / GDPorg
                                ZS(iReg, ii) = ZS(iReg, ii) * GDP(iReg) / GDPorg
                            Next ii
                        End If
                    ElseIf sVarName = "DEMOCPOLYARCHY" Then
                        SEDEMOCPOLYARCHY(iReg) = DEMOCPOLYARCHY(iReg)
                        DEMOCPOLYARCHY(iReg) = CSng(tbl(sCtry)(YrIdx))
                    End If
                Else
                    If sVarName = "POP" Then
                        'This doesn't work for POP, because it's a stock that gets accumulated over time
                        'Thus POP already considers the replaced value in previous years
                        'TMPVal = CSng(sPOPTotal(iReg) * POP(iReg) / SEPOP(iReg))
                        'SEPOP(iReg) = POP(iReg)
                        'POP(iReg) = TMPVal
                        'AdjustCohPop(agedst, POP, iReg)
                        'IFsPOPSpreadAgeDst(iReg)
                    ElseIf sVarName = "GDPPCP" Then
                        'Can't do this approach with GDPPCP, not a stock but using stocks for growth
                        'If gdpadjsw = 0 Or gdprext(iReg) < -99 Then
                        '    'If GRPS(1, iReg) = "United States of America" Then Stop
                        '    If gdprext(iReg) > -65 Then
                        '        TMPVal = CSng(sGDPPCP(iReg) * (1 + gdprext(iReg) / 100))
                        '    Else
                        '        TMPVal = CSng(sGDPPCP(iReg) * GDPPOT(iReg) / SEGDPPOT(iReg))
                        '    End If

                        '    SEGDPPOT(iReg) = GDPPOT(iReg)
                        '    GDPPCP(iReg) = TMPVal

                        '    GDPP(iReg) = GDPPCP(iReg) * POP(iReg)
                        '    GDPPC(iReg) = GDPPCP(iReg) / AMAX(0.000001, PPPCONV(iReg))
                        '    sGDPPCP(iReg) = GDPPCP(iReg)
                        '    GDPorg = GDP(iReg)
                        '    GDP(iReg) = GDPPC(iReg) * POP(iReg)
                        '    gdprext(iReg) = GDP(iReg) / AMAX(0.000001, SGDPR) * 100 - 100
                        '    GDP(iReg) = 0
                        'Else
                        '    'exogenous growth rate; implement without adjustment; SGDPR -> previouys years GDP MER sent as an argument to this code routine
                        '    GDP(iReg) = SGDPR * (1 + gdprext(iReg) / 100)
                        '    GDPPC(iReg) = GDP(iReg) / POP(iReg)
                        '    GDPorg = GDP(iReg)
                        '    GDPPCP(iReg) = GDPPC(iReg) * PPPCONV(iReg)
                        '    sGDPPCP(iReg) = GDPPCP(iReg)
                        '    GDPP(iReg) = GDPPCP(iReg) * POP(iReg)
                        '    GDP(iReg) = 0
                        'End If
                    ElseIf sVarName = "DEMOCPOLYARCHY" Then
                        TMPVal = CSng(SDEMOCPOLYARCHY(iReg) * DEMOCPOLYARCHY(iReg) / SEDEMOCPOLYARCHY(iReg))
                        SEDEMOCPOLYARCHY(iReg) = DEMOCPOLYARCHY(iReg)
                        DEMOCPOLYARCHY(iReg) = TMPVal
                    End If
                End If
            End If
            'dsr 2016/05/04 Main block of new code for when use annual percentage changes from exogenous series - end

        End Sub


        'jrs 2019/06/13
        'Public Sub Retriev(IFsResults As Dictionary(Of String, IDataframe), Year As Short, context As IForecastContext)
        '    'Load all variables
        '    birthmshr = CSng(IFsResults("birthmshr")(Year))
        '    If IFsResults("ifsexomodelid").RowCount > 0 Then
        '        ifsexomodelid = CSng(IFsResults("ifsexomodelid")(Year))
        '        ifsexomodeltype = CSng(IFsResults("ifsexomodeltype")(Year))
        '    Else
        '        ifsexomodelid = 0
        '        ifsexomodeltype = 0
        '    End If

        '    Dim R As Short
        '    ReDim BIRTHS(NR), C(NR), DEATHS(NR), GDP(NR), GDPP(NR), GDPPC(NR), GDPPCP(NR), gdprext(NR)
        '    ReDim migrater(NR), MIGRANTS(NR), HHSIZE(NR)
        '    ReDim POP(NR), POPLE15(NR), POPGT65(NR), POP15TO65(NR), POPPREWORK(NR), POPURBAN(NR), TFR(NR)

        '    If Year = context.FirstYear Then
        '        For R = 1 To CShort(context.GetDimension(1).Buckets.Count)
        '            GDP(R) = CSng(IFsResults("GDP")(Year, R))
        '            GDPPCP(R) = CSng(IFsResults("GDPPCP")(Year, R))
        '            HHSIZE(R) = CSng(IFsResults("HHSIZE")(Year, R))
        '            If IFsResults("MIGRANTS").RowCount > 0 Then
        '                MIGRANTS(R) = CSng(IFsResults("MIGRANTS")(Year, R))
        '            End If
        '            POP(R) = CSng(IFsResults("POP")(Year, R))
        '            POPURBAN(R) = CSng(IFsResults("POPURBAN")(Year, R))
        '            TFR(R) = CSng(IFsResults("TFR")(Year, R))
        '        Next R
        '    End If

        '    For R = 1 To CShort(NR)
        '        If IFsResults("gdprext").RowCount > 0 Then
        '            gdprext(R) = CSng(IFsResults("gdprext")(Year, R))
        '        End If
        '        If IFsResults("migrater").RowCount > 0 Then
        '            migrater(R) = CSng(IFsResults("migrater")(Year, R))
        '        End If
        '    Next R

        '    Dim j As Short
        '    ReDim EDYRSAG15(NR, 3), EDYRSAG25(NR, 3)
        '    If Year = context.FirstYear Then
        '        For R = 1 To CShort(NR)
        '            For j = 1 To 3
        '                EDYRSAG15(R, j) = CSng(IFsResults("EDYRSAG15")(Year, R, j))
        '                EDYRSAG25(R, j) = CSng(IFsResults("EDYRSAG25")(Year, R, j))
        '            Next j
        '        Next R
        '    End If

        '    ReDim VADD(NR, NSec), ZS(NR, NSec)
        '    If Year = context.FirstYear Then
        '        For R = 1 To CShort(NR)
        '            For j = 1 To CShort(NSec)
        '                VADD(R, j) = CSng(IFsResults("VADD")(Year, R, j))
        '                ZS(R, j) = CSng(IFsResults("ZS")(Year, R, j))
        '            Next j
        '        Next R
        '    End If
        'End Sub

        'jrs 2019/06/19
        'Public Sub Fillit(ByRef IFsResults As Dictionary(Of String, IDataframe), Year As Short, context As IForecastContext)
        '    'Save all variables

        '    Dim R As Short

        '    For R = 1 To CShort(NR)
        '        IFsResults("POPLE15") = IFsResults("POPLE15").Append(Year, R, POPLE15(R))
        '    Next

        'End Sub

        'jrs 2019/07/10
        Public Function getDataFrameIdxFromDims(Year As Short, FirstYear As Short,
                                                Optional Dim1 As Short = 0, Optional Dim1Size As Short = 1,
                                                Optional Dim2 As Short = 0, Optional Dim2Size As Short = 1,
                                                Optional Dim3 As Short = 0, Optional Dim3Size As Short = 1) As Integer
            Dim idx As Integer
            idx = (Year - FirstYear) * CInt(Dim1Size) * CInt(Dim2Size) * Dim3Size
            If Dim1 > 0 Then
                idx = idx + Dim2Size * Dim3Size * (Dim1 - 1)
                If Dim2 > 0 Then
                    idx = idx + Dim3Size * (Dim2 - 1)
                    If Dim3 > 0 Then
                        idx = idx + (Dim3 - 1)
                    End If
                End If
            End If

            Return idx
        End Function

        Public Sub XYTABL(TFName$, IndVar As Single, ByRef DepVar As Single, context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '   Identify Bivariate Function Accessed (no longer only table functions)
            '   --------------------------------------------------------------------

            Try
                'Dim regArray() As IRegression
                'regArray = context.GetRegression(TFName$).ToArray()
                'jrs 2019/07/22 from LIU
                'context.Entry(regArray(0)).Collection(regArray(0) >= regArray(0).Coefficients).Load()

                'general case: compose a composite estimator that sums up the results of per-input estimators
                'jrs 2019/09/16
                'ttt 2019/10/08 Dim initial As IReadOnlyDictionary(Of String, Double)
                'initial = context.GetRegressionCoefficients(TFName$).ToDictionary(Function(grp) grp.Name,
                '           Function(grp) grp.Value)
                'ttt 2019/10/08
                Dim initial = RegCoeffs(TFName$)
                'Dim initial As IEnumerable(Of IRegressionCoefficients)
                'initial = context.GetRegressionCoefficients(TFName$)
                'initial = regArray.First().Coefficients

                Dim bPoints As Boolean = True


                '   Analytic function - compute dependent
                '   -------------------------------------
                'ttt 2019/10/08 
                'Dim b9 = initial.FirstOrDefault(Function(x) x.Key = "1-b9").Value
                Dim b9 As Double?
                If initial.ContainsKey("1-b9") Then
                    b9 = initial("1-b9")
                Else
                    b9 = 0
                End If
                If Not b9.HasValue Then
                    b9 = 0.0
                End If
                'ttt 2019/10/08
                'Dim a = initial.FirstOrDefault(Function(x) x.Key = "1-a").Value
                Dim a As Double? = 0.0
                If initial.ContainsKey("1-a") Then
                    a = initial("1-a")
                    If Not a.HasValue Then
                        a = 0.0
                    End If
                End If
                Dim b(8) As Double?
                Dim i As Integer
                For i = 1 To 8
                    'ttt 2019/10/08 
                    'b(i) = initial.FirstOrDefault(Function(x) x.Key = "1-b" & i.ToString).Value
                    If initial.ContainsKey("1-b" & i.ToString) Then
                        b(i) = initial("1-b" & i.ToString)
                        If Not b(i).HasValue Then b(i) = 0
                        If bPoints AndAlso b(i) <> 0 Then bPoints = False
                    Else
                        b(i) = 0.0
                    End If
                Next i

                If Not bPoints Then
                    DepVar = CSng(a + b(1) * IndVar + b(2) * Math.Pow(IndVar, 2) + b(3) * Math.Pow(IndVar, 3) + b(4) * Math.Log(If(IndVar <= 0, 1, IndVar), Math.E) + b(5) * Math.Pow(IndVar, CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar))

                    If b9 <> 0 Then
                        'jrs 2025 / 5 / 9 changed signs so that interpretation Of coefficients Is standard For logistic, positive means increase in probability
                        'DepVar = CSng(b9 / (1 + Math.Exp(DepVar)))
                        DepVar = CSng(b9 / (1 + Math.Exp(-1 * DepVar)))
                    End If

                Else

                    '   Table function - compute dependent variable
                    '   -------------------------------------------
                    'ttt 2019/10/08 
                    Dim Points = RegPoints(TFName$)
                    'ttt 2019/10/08 
                    If Points.Count = 0 Then
                        Exit Sub
                    End If
                    'Dim Points() As IPoint(Of Double)
                    'Points = context.GetRegressionPoints(TFName$).ToArray()

                    'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, 1)) <= 0 Then
                    If (IndVar - Points(0).X) <= 0 Then
                        'JRS 10/20/03 DepVar = YPoints(FuncInd%, 1)
                        DepVar = CSng(Points(0).Y)
                        Exit Sub 'If below range, set to first point
                    End If
                    'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
                    'jrs 2017/11/07 If (IndVar - Points.getXPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
                    If (IndVar - Points(Points.Count - 1).X) >= 0 Then
                        'JRS 10/20/03 DepVar = YPoints(FuncInd%, TFuncPoints(FuncInd%))
                        'jrs 2017/11/07 DepVar = Points.getYPoints(FuncInd%, TFuncPoints(FuncInd%))
                        DepVar = CSng(Points(Points.Count - 1).Y)
                        Exit Sub 'If above range, set to top point
                    End If

                    Dim Numer As Single, denom As Single
                    'jrs 2017/11/07 For IXY = 2 To TFuncPoints(FuncInd%)
                    For IXY = 1 To Points.Count - 1
                        'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, IXY)) <= 0 Then
                        If (IndVar - Points(IXY).X) <= 0 Then
                            'JRS 10/20/03 Numer = (YPoints(FuncInd%, IXY) - YPoints(FuncInd%, IXY - 1))
                            Numer = CSng(Points(IXY).Y - Points(IXY - 1).Y)
                            'JRS 10/20/03 Numer = Numer * (IndVar - XPoints(FuncInd%, IXY - 1))
                            Numer = CSng(Numer * (IndVar - Points(IXY - 1).X))
                            'JRS 10/20/03 Denom = (XPoints(FuncInd%, IXY) - XPoints(FuncInd%, IXY - 1))
                            denom = CSng((Points(IXY).X - Points(IXY - 1).X))
                            'JRS 10/20/03 DepVar = YPoints(FuncInd%, IXY - 1)
                            DepVar = CSng(Points(IXY - 1).Y) + Numer / denom 'This sequence preserves extended digits
                            'ttt 2019/10/08 moved + Number/denom to line above DepVar = DepVar + Numer / denom 'This sequence preserves extended digits
                            Exit Sub
                        End If
                    Next IXY

                End If
            Catch ex As Exception
                Dim msg As String
                msg$ = "Error occurred processing XYTABL, using Function '" + TFName
                msg$ = msg$ + "'.  You will need to reverse changes made in bivariate "
                msg$ = msg$ + "functions or reload the original TablFunc.mdb."
                Debug.Print(msg$)
                _logger.Error(ex, msg$)

            End Try

        End Sub

        'Public Sub XYTABL(TFName$, IndVar As Single, ByRef DepVar As Single)
        '    '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        '    '   Identify Bivariate Function Accessed (no longer only table functions)
        '    '   --------------------------------------------------------------------

        '    Try
        '        'Dim regArray() As IRegression
        '        'regArray = context.GetRegression(TFName$).ToArray()
        '        'jrs 2019/07/22 from LIU
        '        'context.Entry(regArray(0)).Collection(regArray(0) >= regArray(0).Coefficients).Load()

        '        'general case: compose a composite estimator that sums up the results of per-input estimators
        '        'jrs 2019/09/16
        '        'ttt 2019/10/08 Dim initial As IReadOnlyDictionary(Of String, Double)
        '        'initial = context.GetRegressionCoefficients(TFName$).ToDictionary(Function(grp) grp.Name,
        '        '           Function(grp) grp.Value)
        '        'ttt 2019/10/08
        '        Dim initial = RegCoeffs(TFName$)
        '        'Dim initial As IEnumerable(Of IRegressionCoefficients)
        '        'initial = context.GetRegressionCoefficients(TFName$)
        '        'initial = regArray.First().Coefficients

        '        Dim bPoints As Boolean = True


        '        '   Analytic function - compute dependent
        '        '   -------------------------------------
        '        'ttt 2019/10/08 
        '        'Dim b9 = initial.FirstOrDefault(Function(x) x.Key = "1-b9").Value
        '        Dim b9 As Double?
        '        If initial.ContainsKey("1-b9") Then
        '            b9 = initial("1-b9")
        '        Else
        '            b9 = 0
        '        End If
        '        If Not b9.HasValue Then
        '            b9 = 0.0
        '        End If
        '        'ttt 2019/10/08
        '        'Dim a = initial.FirstOrDefault(Function(x) x.Key = "1-a").Value
        '        Dim a As Double? = 0.0
        '        If initial.ContainsKey("1-a") Then
        '            a = initial("1-a")
        '            If Not a.HasValue Then
        '                a = 0.0
        '            End If
        '        End If
        '        Dim b(8) As Double?
        '        Dim i As Integer
        '        For i = 1 To 8
        '            'ttt 2019/10/08 
        '            'b(i) = initial.FirstOrDefault(Function(x) x.Key = "1-b" & i.ToString).Value
        '            If initial.ContainsKey("1-b" & i.ToString) Then
        '                b(i) = initial("1-b" & i.ToString)
        '                If Not b(i).HasValue Then b(i) = 0
        '                If bPoints AndAlso b(i) <> 0 Then bPoints = False
        '            Else
        '                b(i) = 0.0
        '            End If
        '        Next i

        '        If Not bPoints Then
        '            DepVar = CSng(a + b(1) * IndVar + b(2) * Math.Pow(IndVar, 2) + b(3) * Math.Pow(IndVar, 3) + b(4) * Math.Log(IndVar, Math.E) + b(5) * Math.Pow(IndVar, CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar))

        '            If b9 <> 0 Then
        '                DepVar = CSng(b9 / (1 + Math.Exp(DepVar)))
        '            End If

        '        Else

        '            '   Table function - compute dependent variable
        '            '   -------------------------------------------
        '            'ttt 2019/10/08 
        '            Dim Points = RegPoints(TFName$)
        '            'ttt 2019/10/08 
        '            If Points.Count = 0 Then
        '                Exit Sub
        '            End If
        '            'Dim Points() As IPoint(Of Double)
        '            'Points = context.GetRegressionPoints(TFName$).ToArray()

        '            'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, 1)) <= 0 Then
        '            If (IndVar - Points(0).X) <= 0 Then
        '                'JRS 10/20/03 DepVar = YPoints(FuncInd%, 1)
        '                DepVar = CSng(Points(0).Y)
        '                Exit Sub 'If below range, set to first point
        '            End If
        '            'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
        '            'jrs 2017/11/07 If (IndVar - Points.getXPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
        '            If (IndVar - Points(Points.Count - 1).X) >= 0 Then
        '                'JRS 10/20/03 DepVar = YPoints(FuncInd%, TFuncPoints(FuncInd%))
        '                'jrs 2017/11/07 DepVar = Points.getYPoints(FuncInd%, TFuncPoints(FuncInd%))
        '                DepVar = CSng(Points(Points.Count - 1).Y)
        '                Exit Sub 'If above range, set to top point
        '            End If

        '            Dim Numer As Single, denom As Single
        '            'jrs 2017/11/07 For IXY = 2 To TFuncPoints(FuncInd%)
        '            For IXY = 1 To Points.Count - 1
        '                'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, IXY)) <= 0 Then
        '                If (IndVar - Points(IXY).X) <= 0 Then
        '                    'JRS 10/20/03 Numer = (YPoints(FuncInd%, IXY) - YPoints(FuncInd%, IXY - 1))
        '                    Numer = CSng(Points(IXY).Y - Points(IXY - 1).Y)
        '                    'JRS 10/20/03 Numer = Numer * (IndVar - XPoints(FuncInd%, IXY - 1))
        '                    Numer = CSng(Numer * (IndVar - Points(IXY - 1).X))
        '                    'JRS 10/20/03 Denom = (XPoints(FuncInd%, IXY) - XPoints(FuncInd%, IXY - 1))
        '                    denom = CSng((Points(IXY).X - Points(IXY - 1).X))
        '                    'JRS 10/20/03 DepVar = YPoints(FuncInd%, IXY - 1)
        '                    DepVar = CSng(Points(IXY - 1).Y) + Numer / denom 'This sequence preserves extended digits
        '                    'ttt 2019/10/08 moved + Number/denom to line above DepVar = DepVar + Numer / denom 'This sequence preserves extended digits
        '                    Exit Sub
        '                End If
        '            Next IXY

        '        End If
        '    Catch ex As Exception
        '        Dim msg As String
        '        msg$ = "Error occurred processing XYTABL, using Function " + TFName
        '        msg$ = msg$ + ".  You will need to reverse changes made in bivariate "
        '        msg$ = msg$ + "functions or reload the original TablFunc.mdb."
        '        Debug.Print(msg$)


        '    End Try

        'End Sub

        Public Sub XYTABL(TFName$, IndVar As Single, ByRef DepVar As Single, context As IRebuildFacade)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '   Identify Bivariate Function Accessed (no longer only table functions)
            '   --------------------------------------------------------------------

            Try
                Dim regArray() As IRegression
                regArray = context.GetRegression(TFName$).ToArray()
                'jrs 2019/07/22 from LIU
                'context.Entry(regArray(0)).Collection(regArray(0) >= regArray(0).Coefficients).Load()

                'general case: compose a composite estimator that sums up the results of per-input estimators
                'jrs 2019/09/16
                'ttt 2019/10/08 Dim initial As IReadOnlyDictionary(Of String, Double)
                'initial = context.GetRegressionCoefficients(TFName$).ToDictionary(Function(grp) grp.Name,
                '           Function(grp) grp.Value)
                'ttt 2019/10/08
                Dim initial = regArray(0).Coefficients
                'Dim initial As IEnumerable(Of IRegressionCoefficients)
                'initial = context.GetRegressionCoefficients(TFName$)
                'initial = regArray.First().Coefficients

                Dim bPoints As Boolean = True


                '   Analytic function - compute dependent
                '   -------------------------------------
                'ttt 2019/10/08 
                'Dim b9 = initial.FirstOrDefault(Function(x) x.Key = "1-b9").Value
                Dim b9 As Double?
                If initial.ContainsKey("b9") Then
                    b9 = initial("b9")
                Else
                    b9 = 0
                End If
                If Not b9.HasValue Then
                    b9 = 0.0
                End If
                'ttt 2019/10/08
                'Dim a = initial.FirstOrDefault(Function(x) x.Key = "1-a").Value
                Dim a As Double? = 0.0
                If initial.ContainsKey("a") Then
                    a = initial("a")
                    If Not a.HasValue Then
                        a = 0.0
                    End If
                End If
                Dim b(8) As Double?
                Dim i As Integer
                For i = 1 To 8
                    'ttt 2019/10/08 
                    'b(i) = initial.FirstOrDefault(Function(x) x.Key = "1-b" & i.ToString).Value
                    If initial.ContainsKey("b" & i.ToString) Then
                        b(i) = initial("b" & i.ToString)
                        If Not b(i).HasValue Then b(i) = 0
                        If bPoints AndAlso b(i) <> 0 Then bPoints = False
                    Else
                        b(i) = 0.0
                    End If
                Next i

                If Not bPoints Then
                    DepVar = CSng(a + b(1) * IndVar + b(2) * Math.Pow(IndVar, 2) + b(3) * Math.Pow(IndVar, 3) + b(4) * Math.Log(If(IndVar <= 0, 1, IndVar), Math.E) + b(5) * Math.Pow(IndVar, CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar))

                    If b9 <> 0 Then
                        DepVar = CSng(b9 / (1 + Math.Exp(DepVar)))
                    End If

                Else

                    '   Table function - compute dependent variable
                    '   -------------------------------------------
                    'ttt 2019/10/08 
                    Dim Points = RegPoints(TFName$)
                    'ttt 2019/10/08 
                    If Points.Count = 0 Then
                        Exit Sub
                    End If
                    'Dim Points() As IPoint(Of Double)
                    'Points = context.GetRegressionPoints(TFName$).ToArray()

                    'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, 1)) <= 0 Then
                    If (IndVar - Points(0).X) <= 0 Then
                        'JRS 10/20/03 DepVar = YPoints(FuncInd%, 1)
                        DepVar = CSng(Points(0).Y)
                        Exit Sub 'If below range, set to first point
                    End If
                    'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
                    'jrs 2017/11/07 If (IndVar - Points.getXPoints(FuncInd%, TFuncPoints(FuncInd%))) >= 0 Then
                    If (IndVar - Points(Points.Count - 1).X) >= 0 Then
                        'JRS 10/20/03 DepVar = YPoints(FuncInd%, TFuncPoints(FuncInd%))
                        'jrs 2017/11/07 DepVar = Points.getYPoints(FuncInd%, TFuncPoints(FuncInd%))
                        DepVar = CSng(Points(Points.Count - 1).Y)
                        Exit Sub 'If above range, set to top point
                    End If

                    Dim Numer As Single, denom As Single
                    'jrs 2017/11/07 For IXY = 2 To TFuncPoints(FuncInd%)
                    For IXY = 1 To Points.Count - 1
                        'JRS 10/20/03 If (IndVar - XPoints(FuncInd%, IXY)) <= 0 Then
                        If (IndVar - Points(IXY).X) <= 0 Then
                            'JRS 10/20/03 Numer = (YPoints(FuncInd%, IXY) - YPoints(FuncInd%, IXY - 1))
                            Numer = CSng(Points(IXY).Y - Points(IXY - 1).Y)
                            'JRS 10/20/03 Numer = Numer * (IndVar - XPoints(FuncInd%, IXY - 1))
                            Numer = CSng(Numer * (IndVar - Points(IXY - 1).X))
                            'JRS 10/20/03 Denom = (XPoints(FuncInd%, IXY) - XPoints(FuncInd%, IXY - 1))
                            denom = CSng((Points(IXY).X - Points(IXY - 1).X))
                            'JRS 10/20/03 DepVar = YPoints(FuncInd%, IXY - 1)
                            DepVar = CSng(Points(IXY - 1).Y) + Numer / denom 'This sequence preserves extended digits
                            'ttt 2019/10/08 moved + Number/denom to line above DepVar = DepVar + Numer / denom 'This sequence preserves extended digits
                            Exit Sub
                        End If
                    Next IXY

                End If
            Catch ex As Exception
                Dim msg As String
                msg$ = "Error occurred processing XYTABL, using Function '" + TFName
                msg$ = msg$ + "'.  You will need to reverse changes made in bivariate "
                msg$ = msg$ + "functions or reload the original TablFunc.mdb."
                Debug.Print(msg$)
                _logger.Error(ex, msg$)

            End Try

        End Sub

        Public Sub AnalFunc(TFName$, IndVar() As Single, ByRef DepVar As Single, IndVarNames() As String, context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'JRS 6/18/02 Originally was XYTABL, modified to handle several independant variables
            'IndVar is an array of values, corresponding to independent variables used in this function
            'be carefull with the order of this values, it has to be the same order in which the
            'variables are stored in AnalFunc.mdb
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2014/10/09
            Dim bNotOrder As Boolean = False 'ttt 2019/10/08 moved to a single line

            Try
                'ttt 2019/10/08
                Dim regArray = RegArrays(TFName$)
                'Dim regArray() As IRegression
                'regArray = context.GetRegression(TFName$).ToArray()

                'jrs 2019/07/22 from LIU
                'context.Entry(regArray(0)).Collection(regArray(0) >= regArray(0).Coefficients).Load()

                'general case: compose a composite estimator that sums up the results of per-input estimators
                'ttt 2019/10/08 Dim initial As IReadOnlyDictionary(Of String, Double)
                'jrs 2019/09/16
                'initial = context.GetRegressionCoefficients(TFName$).ToDictionary(Function(grp) grp.RegressionSeq.ToString() + "-" + grp.Name,
                '           Function(grp) grp.Value)
                Dim initial = RegCoeffs(TFName$)
                'initial = regArray.First().Coefficients

                '   Analytic function - compute dependent
                '   -------------------------------------
                'ttt 2019/10/08
                'Dim b9 = initial.FirstOrDefault(Function(x) x.Key = "1-b9").Value
                Dim b9 As Double?
                If initial.ContainsKey("1-b9") Then
                    b9 = initial("1-b9")
                Else
                    b9 = 0
                End If
                If Not b9.HasValue Then
                    b9 = 0.0
                End If

                'ttt 2019/10/08 
                'Dim a = initial.FirstOrDefault(Function(x) x.Key = "1-a").Value
                Dim a As Double? = 0.0
                If initial.ContainsKey("1-a") Then
                    a = initial("1-a")
                    If Not a.HasValue Then
                        a = 0.0
                    End If
                End If

                Dim b(8) As Double?
                Dim i As Integer, j As Integer

                DepVar = CSng(a)

                Dim IndVarIdx As Integer = 0
                Dim IndVarIdxCount As Integer = 0

                'ttt 2019/10/08  ->AndAlso
                For j = 0 To regArray.Length - 1
                    IndVarIdxCount = 0
                    Do While regArray(j).InputName <> IndVarNames(IndVarIdx) AndAlso IndVarIdxCount < IndVarNames.Length
                        'ttt 2019/10/08 IndVarIdx = IndVarIdx + 1
                        IndVarIdx += 1
                        If IndVarIdx = IndVarNames.Length Then IndVarIdx = 0
                        'ttt 2019/10/08 IndVarIdxCount = IndVarIdxCount + 1
                        IndVarIdxCount += 1
                    Loop

                    If IndVarIdxCount = IndVarNames.Length Then
                        Debug.Print("InputVariable " & regArray(j).InputName & " not found in " & TFName$)
                        Stop
                    End If

                    For i = 1 To 8
                        'ttt 2019/10/08 
                        'b(i) = initial.FirstOrDefault(Function(x) x.Key = regArray(j).Seq.ToString() & "-b" & i.ToString).Value
                        If initial.ContainsKey(regArray(j).Seq.ToString() & "-b" & i.ToString) Then
                            b(i) = initial(regArray(j).Seq.ToString() & "-b" & i.ToString)
                            If Not b(i).HasValue Then b(i) = 0
                        Else
                            b(i) = 0.0
                        End If
                    Next i
                    'ttt 2019/10/08 DepVar = DepVar + CSng(b(1) * IndVar(IndVarIdx) + b(2) * Math.Pow(IndVar(IndVarIdx), 2) + b(3) * Math.Pow(IndVar(IndVarIdx), 3) + b(4) * Math.Log(If(IndVar(IndVarIdx) <= 0, 1, IndVar(IndVarIdx)), Math.E) + b(5) * Math.Pow(IndVar(IndVarIdx), CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar(IndVarIdx)))
                    DepVar += CSng(b(1) * IndVar(IndVarIdx) + b(2) * Math.Pow(IndVar(IndVarIdx), 2) + b(3) * Math.Pow(IndVar(IndVarIdx), 3) + b(4) * Math.Log(If(IndVar(IndVarIdx) <= 0, 1, IndVar(IndVarIdx)), Math.E) + b(5) * Math.Pow(IndVar(IndVarIdx), CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar(IndVarIdx)))
                Next j

                If b9 <> 0 Then
                    DepVar = CSng(b9 / (1 + Math.Exp(DepVar)))
                End If

            Catch ex As Exception
                Dim msg As String
                msg$ = "Error occurred processing AnalFunc, using Function " + TFName
                msg$ = msg$ + ".  You will need to reverse changes made in multivariate "
                msg$ = msg$ + "functions or reload the original AnalFunc.mdb."
                Debug.Print(msg$)


            End Try

        End Sub

        Public Sub AnalFunc(TFName$, IndVar() As Single, ByRef DepVar As Single, IndVarNames() As String, context As IRebuildFacade)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'JRS 6/18/02 Originally was XYTABL, modified to handle several independant variables
            'IndVar is an array of values, corresponding to independent variables used in this function
            'be carefull with the order of this values, it has to be the same order in which the
            'variables are stored in AnalFunc.mdb
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2014/10/09
            Dim bNotOrder As Boolean
            bNotOrder = False

            Try
                Dim regArray() As IRegression
                regArray = context.GetRegression(TFName$).ToArray()
                'jrs 2019/07/22 from LIU
                'context.Entry(regArray(0)).Collection(regArray(0) >= regArray(0).Coefficients).Load()

                'general case: compose a composite estimator that sums up the results of per-input estimators
                Dim initial As IReadOnlyDictionary(Of String, Double)
                'initial = context.GetRegressionCoefficients(TFName$).ToDictionary(Function(grp) grp.RegressionSeq.ToString() + "-" + grp.Name,
                '          Function(grp) grp.Value)
                initial = regArray.First().Coefficients

                '   Analytic function - compute dependent
                '   -------------------------------------
                Dim b9 As Double?
                If initial.ContainsKey("b9") Then
                    b9 = initial("b9")
                Else
                    b9 = 0
                End If
                If Not b9.HasValue Then
                    b9 = 0.0
                End If
                Dim a As Double? = 0.0
                If initial.ContainsKey("a") Then
                    a = initial("a")
                    If Not a.HasValue Then
                        a = 0.0
                    End If
                End If
                Dim b(8) As Double?
                Dim i As Integer, j As Integer

                DepVar = CSng(a)

                Dim IndVarIdx As Integer = 0
                Dim IndVarIdxCount As Integer = 0

                For j = 0 To regArray.Length - 1
                    IndVarIdxCount = 0
                    Do While regArray(j).InputName <> IndVarNames(IndVarIdx) And IndVarIdxCount < IndVarNames.Length
                        IndVarIdx = IndVarIdx + 1
                        If IndVarIdx = IndVarNames.Length Then IndVarIdx = 0
                        IndVarIdxCount = IndVarIdxCount + 1
                    Loop

                    If IndVarIdxCount = IndVarNames.Length Then
                        Debug.Print("InputVariable " & regArray(j).InputName & "not found in " & TFName$)
                        Stop
                    End If

                    If j > 0 Then
                        initial = regArray(j).Coefficients
                    End If

                    For i = 1 To 8
                        If initial.ContainsKey("b" & i.ToString) Then
                            b(i) = initial("b" & i.ToString)
                            If Not b(i).HasValue Then b(i) = 0
                        Else
                            b(i) = 0.0
                        End If
                    Next i
                    DepVar = DepVar + CSng(b(1) * IndVar(IndVarIdx) + b(2) * Math.Pow(IndVar(IndVarIdx), 2) + b(3) * Math.Pow(IndVar(IndVarIdx), 3) + b(4) * Math.Log(If(IndVar(IndVarIdx) <= 0, 1, IndVar(IndVarIdx)), Math.E) + b(5) * Math.Pow(IndVar(IndVarIdx), CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar(IndVarIdx)))
                Next j

                If b9 <> 0 Then
                    DepVar = CSng(b9 / (1 + Math.Exp(DepVar)))
                End If

            Catch ex As Exception
                Dim msg As String
                msg$ = "Error occurred processing AnalFunc, using Function " + TFName
                msg$ = msg$ + ".  You will need to reverse changes made in multivariate "
                msg$ = msg$ + "functions or reload the original AnalFunc.mdb."
                Debug.Print(msg$)


            End Try

        End Sub

        'jrs 2014/10/09 added IndVarNames
        'jrs 2011/10/04
        'Computes the 3rd parameter DepVar, using the formula TFName, with the array of parameters in IndVar
        'Retrieves the Standard Error of the Function
        Public Sub AnalFuncSE(TFName$, IndVar() As Single, ByRef DepVar As Single, ByRef StandardError As Single, IndVarNames() As String, context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'JRS 6/18/02 Originally was XYTABL, modified to handle several independant variables
            'IndVar is an array of values, corresponding to independent variables used in this function
            'be carefull with the order of this values, it has to be the same order in which the
            'variables are stored in AnalFunc.mdb
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2014/10/09
            Dim bNotOrder As Boolean
            bNotOrder = False

            Try
                Dim regArray() As IRegression
                regArray = context.GetRegression(TFName$).ToArray()
                'jrs 2019/07/22 from LIU
                'context.Entry(regArray(0)).Collection(regArray(0) >= regArray(0).Coefficients).Load()

                'general case: compose a composite estimator that sums up the results of per-input estimators
                Dim initial As IReadOnlyDictionary(Of String, Double)
                'initial = context.GetRegressionCoefficients(TFName$).ToDictionary(Function(grp) grp.RegressionSeq.ToString() + "-" + grp.Name,
                '           Function(grp) grp.Value)
                initial = RegCoeffs(TFName$)
                'initial = regArray.First().Coefficients

                '   Analytic function - compute dependent
                '   -------------------------------------
                Dim b9 As Double?
                If initial.ContainsKey("1-b9") Then
                    b9 = initial("1-b9")
                Else
                    b9 = 0
                End If
                If Not b9.HasValue Then
                    b9 = 0.0
                End If
                Dim a As Double? = 0.0
                If initial.ContainsKey("1-a") Then
                    a = initial("1-a")
                    If Not a.HasValue Then
                        a = 0.0
                    End If
                End If

                'jrs 2011/10/04
                StandardError = 0
                If regArray(0).StdError.HasValue Then
                    StandardError = CSng(regArray(0).StdError)
                End If


                Dim b(8) As Double?
                Dim i As Integer, j As Integer

                DepVar = CSng(a)

                Dim IndVarIdx As Integer = 0
                Dim IndVarIdxCount As Integer = 0

                For j = 0 To regArray.Length - 1
                    IndVarIdxCount = 0
                    Do While regArray(j).InputName <> IndVarNames(IndVarIdx) And IndVarIdxCount < IndVarNames.Length
                        IndVarIdx = IndVarIdx + 1
                        If IndVarIdx = IndVarNames.Length Then IndVarIdx = 0
                        IndVarIdxCount = IndVarIdxCount + 1
                    Loop

                    If IndVarIdxCount = IndVarNames.Length Then
                        Debug.Print("InputVariable " & regArray(j).InputName & "not found in " & TFName$)
                        Stop
                    End If

                    For i = 1 To 8
                        If initial.ContainsKey(regArray(j).Seq.ToString() & "-b" & i.ToString) Then
                            b(i) = initial(regArray(j).Seq.ToString() & "-b" & i.ToString)
                            If Not b(i).HasValue Then b(i) = 0
                        Else
                            b(i) = 0.0
                        End If
                    Next i
                    DepVar = DepVar + CSng(b(1) * IndVar(IndVarIdx) + b(2) * Math.Pow(IndVar(IndVarIdx), 2) + b(3) * Math.Pow(IndVar(IndVarIdx), 3) + b(4) * Math.Log(If(IndVar(IndVarIdx) <= 0, 1, IndVar(IndVarIdx)), Math.E) + b(5) * Math.Pow(IndVar(IndVarIdx), CDbl(b(6))) + b(7) * Math.Exp(CDbl(b(8)) * IndVar(IndVarIdx)))
                Next j

                If b9 <> 0 Then
                    DepVar = CSng(b9 / (1 + Math.Exp(DepVar)))
                End If

            Catch ex As Exception
                Dim msg As String
                msg$ = "Error occurred processing AnalFunc, using Function " + TFName
                msg$ = msg$ + ".  You will need to reverse changes made in multivariate "
                msg$ = msg$ + "functions or reload the original AnalFunc.mdb."
                Debug.Print(msg$)


            End Try

        End Sub

        'jrs 2019/03/21 Adding INFMOR data and CHILDDTHR data to match too
        'jrs 2009/05/22
        'Adjustment factor to match mortality with Life Expectancy Data
        Public Function matchMortWLifeExp(ByRef MortDist As Single(,,), ByVal R As Integer, ByVal G As Integer,
        ByVal TargetLE As Single, ByVal mFactorMin As Single, ByVal mFactorMax As Single, ByRef bCancelMsg As Boolean,
        Optional INFMORdata As Single = 0, Optional CHILDDTHRData As Single = 0) As Single
            '1. MortDist is the array (3 dimensioned), 1- cohorts, 2-regions, 3-gender
            '2. R is the Region index
            '3. g is the gender index, 1 for males, 2 for females
            '4. TargetLE is the Life expectancy value to match
            '5. Minimum Value for mFactor
            '6. Max Value for mFactor
            '7. bCancelMsg flag to cancel warning message
            '8. INFMORData Infant Mortality Data
            '9. CHILDDTHRData Child Mortality Probability Data
            Dim infMort As Single, LE As Single, lx As Single
            Dim j As Integer, N As Integer, cohMort As Single, cohMort2 As Single
            Dim nax As Single, nqx As Single, nax0 As Single
            Dim mFactor1 As Single

            mFactor1 = (mFactorMax + mFactorMin) / 2

            'jrs 2019/03/21
            If INFMORdata > 0 And mFactor1 > 0 Then
                MortDist(0, R, G) = INFMORdata / 1000 / mFactor1
            End If

            infMort = MortDist(0, R, G) * mFactor1
            LE = 0
            lx = 1
            For j = 0 To NCohorts
                'Years in cohort
                If j = 0 Then
                    N = 1
                ElseIf j = 1 Then
                    N = 4
                Else
                    N = 5
                End If

                cohMort = MortDist(j, R, G) * mFactor1

                'Average years lived by those who die (per Keyfitz)
                If j = 0 Then
                    If cohMort >= 0.107 Then
                        nax = 0.34
                    Else
                        nax = CSng(0.049 + 2.742 * cohMort)
                    End If
                    nax0 = nax
                ElseIf j = 1 Then
                    If infMort >= 0.107 Then
                        nax = 1.356
                    Else
                        nax = CSng(1.587 - 2.167 * infMort)
                    End If
                    'jrs 2019/03/21
                    If CHILDDTHRData > 0 And mFactor1 > 0 Then
                        'CHILDDTHRData/1000 = 1 - lx1
                        '= 1 - lx0 * (1 - nqx1)
                        '= 1 - 1 * (1 - nqx0) * (1 - nqx1)
                        '= 1 - (1 - infMort/(1+(1 - nax0)*InfMort)) * (1 - 4 * MortDist/(1+(4-nax1)MortDist))

                        '(1 - infMort/(1+(1 - nax0)*InfMort)) * (1 - 4 * MortDist/(1+(4-nax1)MortDist)) = 1 - CHILDDTHRData/1000
                        '(1 - 4 * MortDist/(1+(4-nax1)MortDist)) = (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))
                        '4 * MortDist/(1+(4-nax1)MortDist) = 1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))
                        '4 * MortDist = (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))) * (1+(4-nax1)MortDist)
                        '4 * MortDist - (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))) * (4-nax1) MortDist = (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort)))
                        'MortDist = ((1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort)))) / (4 - (1 - (1 - CHILDDTHRData/1000) / (1 - infMort/(1+(1 - nax0)*InfMort))) * (4-nax1))
                        cohMort = ((1 - (1 - CHILDDTHRData / 1000) / (1 - infMort / (1 + (1 - nax0) * infMort))))
                        cohMort2 = (4 - (1 - (1 - CHILDDTHRData / 1000) / (1 - infMort / (1 + (1 - nax0) * infMort))) * (4 - nax))
                        cohMort = cohMort / cohMort2
                        If cohMort < 0 Then
                            cohMort = CSng(0.02 * infMort)
                        End If
                        MortDist(j, R, G) = cohMort / mFactor1
                    End If
                Else
                    nax = 2.5
                End If

                'Probability of death using period data
                If j = NCohorts Then
                    nqx = 1
                Else
                    nqx = (N * cohMort) / (1 + ((N - nax) * cohMort))
                    'jrs 2012/09/04
                    If nqx > 1 Then
                        nqx = 1
                    End If
                End If

                'Adding N years for survivors, and nax for deaths
                If j < NCohorts Then
                    LE = LE + (lx * (1 - nqx) * N) + (lx * nqx * nax)
                Else
                    'jrs 2012/09/03
                    If cohMort > 0 Then
                        LE = LE + lx / cohMort
                    End If
                End If
                'Probability of surviving until age x
                lx = lx * (1 - nqx)
            Next j

            Dim portion As Single
            portion = (TargetLE - LE) / LE

            If Abs(portion) < 0.001 Then
                matchMortWLifeExp = mFactor1
            ElseIf mFactorMin >= mFactorMax Then
                'jrs 2011/03/01 *** begin ****
                If Not bCancelMsg Then
                    Debug.Print("Not possible to find matching factor between Mortality and Life Expectancy for " & GRPS(1, R))
                    bCancelMsg = True
                End If
                If mFactorMin = mFactorMax Then
                    matchMortWLifeExp = mFactorMin
                Else
                    matchMortWLifeExp = 1
                End If
                'jrs 2011/03/01 *** end ***
            Else 'Try another mFactor
                'more lifeexpectancy, less mortality
                If portion > 0 Then
                    matchMortWLifeExp = matchMortWLifeExp(MortDist, R, G, TargetLE, mFactorMin, mFactor1, bCancelMsg, INFMORdata, CHILDDTHRData)
                Else 'less lifeexpectancy, more mortality
                    matchMortWLifeExp = matchMortWLifeExp(MortDist, R, G, TargetLE, mFactor1, mFactorMax, bCancelMsg, INFMORdata, CHILDDTHRData)
                End If
            End If
        End Function

        '=============================Function.BAS  =======================
        Function ConvergeOverTime(Base As Single, Target As Single, YearsToConverge As Single) As Single
            ' converges to target (often predicted) value over specified years
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            YearsToConverge = AMAX(YearsToConverge, 1)

            'TimeFac = Amin(IY%, YearsToConverge)
            Dim TimeFAc As Single
            TimeFAc = Amin(IY%, YearsToConverge)

            ConvergeOverTime = (Base * (YearsToConverge - TimeFAc) + Target * TimeFAc) _
        / YearsToConverge

        End Function

        'jrs 2021/01/26 adding comments:
        'This is the PID controller: is used lots of places for chasing equilibrium
        'PID: Proportional Integral Derivative
        'https://www.du.edu/ifs/help/understand/equations/specialized/adjustment.html
        'new help system
        'https://pardeewiki.du.edu/index.php?title=Understand_IFs#Understanding_the_Equations

        '1. Diff1 is the distance between the current year's value and the target
        '2. Diff2 is the change between this year's value than last year's.  Diff2 can either reinforce or contradict the deviation from the target's contradiction to the multiplier.
        'For example, when Diff1 is positive, if Diff2 is positive there is movement away from the target and the adjustment should be larger than when Diff2 is negative, suggesting that changes already made over time are moving the variable of interest toward the target already, so slow down changes.
        '3. Par1 Elasticity on target
        '4. par2 Elasticity on time

        Sub ADJSTR(BaseNum As Single, Diff1 As Single, Diff2 As Single, Par1 As Single, Par2 As Single, ByRef Mul As Single)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            'jrs 2007/03/23
            'these 2 lines are temporary, add about 8% to the run
            '    Par1 = AMAX(-3, Amin(Par1, 3))
            '    Par2 = AMAX(-3, Amin(Par2, 3))
            Dim FirstOrder, SecondOrder As Single
            FirstOrder = 1 + Diff1 / BaseNum
            FirstOrder = CSng(AMAX(0.5, Amin(FirstOrder, 1.5)) ^ Par1)

            SecondOrder = 1 + Diff2 / BaseNum
            SecondOrder = CSng(AMAX(0.5, Amin(SecondOrder, 1.5)) ^ Par2)

            Mul = FirstOrder * SecondOrder

        End Sub

        'jrs 2011/10/04
        'This function will return an additive term, that will be applied before the multiplier.
        'mti July 25, 2012 - start
        Public Function StdErrorAdditiveShift(SE As Single, Prediction As Single, DataAddShift As Single,
        IDataAddShift As Single, TargetSEs As Single, TargetYr As Integer, currentYr As Integer,
        FirstYr As Integer, Optional UpperBound As Single = -9999) As Single

            'Public Function StdErrorAdditiveShift(SE As Single, Prediction As Single, DataAddShift As Single, _
            'IDataAddShift As Single, TargetSEs As Single, TargetYr As Integer, currentYr As Integer,
            'FirstYr As Integer) As Single
            'mti July 25, 2012 - end


            '1. SE is the Standard Error
            '2. Prediction is the result from the analytical formula
            '3. DataAddShift is the additive shift factor used to match data in the first year (with Convergence)
            '4. IDataAddShift is the additive shift in the base year, no convergence considered
            '4. TargetSEs is the numer of SEs to be added (or subtracted) to the Prediction
            '5. TargetYr is the year in which to reach TargetSEs
            '6. CurrentYr is the current year
            '7. firstYr is the first year to start the transition

            If currentYr <= FirstYr Then
                StdErrorAdditiveShift = 0
                Exit Function
            End If

            Dim TargetVal As Single

            'mti February 28, 2012 -start

            '    'old code
            '    If (SE * TargetSEs > IDataAddShift And TargetSEs >= 0) Or _
            '    (SE * TargetSEs < IDataAddShift And TargetSEs <= 0) Then
            '        TargetVal = Prediction + SE * TargetSEs
            '    Else
            '        TargetVal = Prediction + IDataAddShift 'DataAddShift
            '    End If

            'mti - new code

            If (Prediction + SE * TargetSEs > Prediction + DataAddShift And TargetSEs >= 0) Or
            (Prediction + SE * TargetSEs < Prediction + DataAddShift And TargetSEs <= 0) Then
                TargetVal = Prediction + SE * TargetSEs
            Else
                TargetVal = Prediction + DataAddShift
            End If

            'mti July 25, 2012 - start

            If UpperBound <> -9999 Then
                TargetVal = Amin(TargetVal, UpperBound)
            End If
            'did not do anything for downward target (e.g., Sanitation; none)
            'mti July 25, 2012 - end


            'mti February 28, 2012 - end

            Dim CurrVal As Single
            CurrVal = Prediction + DataAddShift

            'mti July 25, 2012 - start
            'currval has to be bound also etse, there can be negative increments;
            'note MTI: the net effect of binding both target and currval might wash out each other
            If UpperBound <> -9999 Then
                CurrVal = Amin(CurrVal, UpperBound)
            End If
            'did not do anything for downward target (e.g., Sanitation; none)
            'mti July 25, 2012 - end

            If currentYr < TargetYr Then
                'if TargetYr = FirstYear, then currentYr will not enter here, currentYr will always be greater
                StdErrorAdditiveShift = (TargetVal - CurrVal) * (currentYr - FirstYr) / (TargetYr - FirstYr)
            Else
                StdErrorAdditiveShift = TargetVal - CurrVal
            End If
        End Function

        'jrs 2011/10/05
        'This function will return a multiplicative term, that will be applied before the multiplier.
        Public Function StdErrorMultShift(SE As Single, Prediction As Single, DataMultShift As Single,
        IDataMultShift As Single, TargetSEs As Single, TargetYr As Integer, currentYr As Integer,
        IData As Single, FirstYr As Integer) As Single
            '1. SE is the Standard Error
            '2. Prediction is the result from the analytical formula
            '3. DataMultShift is the multiplicative shift factor used to match data in the first year (with Convergence)
            '4. IDataAddShift is the multiplicative shift in the base year, no convergence considered
            '4. TargetSEs is the numer of SEs to be added (or subtracted) to the Prediction
            '5. TargetYr is the year in which to reach TargetSEs
            '6. CurrentYr is the current year
            '7. IData is the data in the base year
            '8. firstYr is the first year to start the transition

            If currentYr <= FirstYr Then
                StdErrorMultShift = 1
                Exit Function
            End If

            Dim TargetVal As Single

            'jrs 2012/09/27 I think Mohammod's fix on the additive function makes sense
            'Accordingly we should use the current mult shift and not the initial
            'jrs 2012/09/27 *** begin ***

            'mti February 28, 2012 - start

            'old code
            'jrs 2012/03/08 (IDataMultShift - 1) is the reduction portion of the initial prediction
            'jrs 2012/03/08 initial prediction = IData / IDataMultShift (added / IDataMultShift)
            '    If (SE * TargetSEs > (IDataMultShift - 1) * IData / IDataMultShift And TargetSEs >= 0) Or _
            '    (SE * TargetSEs < (IDataMultShift - 1) * IData / IDataMultShift And TargetSEs <= 0) Then
            '
            ''    If (SE * TargetSEs > (IDataMultShift - 1) * IData And TargetSEs >= 0) Or _
            ''    (SE * TargetSEs < (IDataMultShift - 1) * IData And TargetSEs <= 0) Then
            '
            '        TargetVal = Prediction + SE * TargetSEs
            '    Else
            '        TargetVal = Prediction * IDataMultShift
            '    End If

            '    'mti new code
            '    If (Prediction + SE * TargetSEs > Prediction * IDataMultShift And TargetSEs >= 0) Or _
            '    (Prediction + SE * TargetSEs < Prediction * IDataMultShift And TargetSEs <= 0) Then
            '
            ''    If (SE * TargetSEs > (IDataMultShift - 1) * IData And TargetSEs >= 0) Or _
            ''    (SE * TargetSEs < (IDataMultShift - 1) * IData And TargetSEs <= 0) Then
            '
            '        TargetVal = Prediction + SE * TargetSEs
            '    Else
            '        TargetVal = Prediction * DataMultShift
            '    End If
            'mti February 28, 2012 - end

            If (Prediction + SE * TargetSEs > Prediction * DataMultShift And TargetSEs >= 0) Or
            (Prediction + SE * TargetSEs < Prediction * DataMultShift And TargetSEs <= 0) Then
                TargetVal = Prediction + SE * TargetSEs
            Else
                TargetVal = Prediction * DataMultShift
            End If
            'jrs 2012/09/27 *** end ***


            Dim CurrVal As Single
            CurrVal = Prediction * DataMultShift
            If CurrVal = 0 Then
                StdErrorMultShift = 1
                Exit Function
            End If

            If currentYr < TargetYr Then
                'if TargetYr = FirstYear, then currentYr will not enter here, currentYr will always be greater
                StdErrorMultShift = 1 + (TargetVal / CurrVal - 1) * (currentYr - FirstYr) / (TargetYr - FirstYr)
            Else
                StdErrorMultShift = TargetVal / CurrVal
            End If
        End Function

        Function ConvergeOverTime(Base As Single, Target As Single, YearsToConverge As Integer) As Single
            ' converges to target (often predicted) value over specified years
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            YearsToConverge = AMAX(YearsToConverge, 1)

            'TimeFac = Amin(IY%, YearsToConverge)
            Dim TimeFAc As Integer
            'jrs 2026/08/20 TimeFAc = Amin(IY%, YearsToConverge)
            TimeFAc = Amin(IY% - 1, YearsToConverge)

            ConvergeOverTime = (Base * (YearsToConverge - TimeFAc) + Target * TimeFAc) _
                 / YearsToConverge

        End Function

        Function ConvergeOverTimeFromStartYear(ByVal Base As Single, ByVal Target As Single, ByVal YearsToConverge As Integer, Optional StartYr As Integer = 2) As Single
            '-------------------------------------------------------------
            'mti 2022/11/23 new procedure - target/convergence that can start later in the horizon
            'StartYear is the year when the intervention starts,
            'for both startyear and target year (yearstoconverge) the year counter needs to start from 1, the base year
            'so, if base year is 2017, for an intervention start in 2023 and met by 2030, StartYr needs to be 7, and YearsToConverge needs to be 14

            ' converges to target (often predicted) value over specified years starting from any year
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -----------------------------------

            StartYr = AMAX(2, StartYr)

            YearsToConverge = AMAX(YearsToConverge - StartYr + 1, 1)

            Dim TimeFAc = AMAX(0, Amin(IY% - StartYr + 1, YearsToConverge))

            ConvergeOverTimeFromStartYear = (Base * (YearsToConverge - TimeFAc) + Target * TimeFAc) _
            / YearsToConverge

        End Function

        'jrs 2011/06/01 *** new procedure created for Smoking in stages ***
        Function ConvergeOverTimeStartX(ByVal Base As Single, ByVal Target As Single, ByVal YearsToConverge As Integer, ByVal START As Integer) As Single
            ' converges to target (often predicted) value over specified years
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            YearsToConverge = AMAX(YearsToConverge - START + 1, 1)

            'TimeFac = Amin(IY%, YearsToConverge)
            Dim TimeFAc As Integer
            TimeFAc = Amin(IY% - START + 1, YearsToConverge)
            'jrs 2018/04/21
            If IY% < START Then
                TimeFAc = 0
            End If

            ConvergeOverTimeStartX = (Base * (YearsToConverge - TimeFAc) + Target * TimeFAc) _
                 / YearsToConverge

        End Function

        '=============================Function.BAS  =======================
        Function ConvergeOverTimeSlowing(Base As Single, Target As Single, YearsToConverge As Integer, MidwayPercShare As Single) As Single
            ' converges to target (often predicted) value over specified years, faster at first
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            YearsToConverge = AMAX(YearsToConverge, 3)
            Dim Halfway As Integer, TargetHalf As Single
            Halfway = CInt(YearsToConverge / 2)
            TargetHalf = Base + (Target - Base) * MidwayPercShare / 100
            Dim TimeFAc As Integer
            If IY% <= Halfway Then
                TimeFAc = Amin(IY%, Halfway)
                ConvergeOverTimeSlowing = (Base * (Halfway - TimeFAc) + TargetHalf * TimeFAc) _
                     / Halfway
            Else
                TimeFAc = Amin(IY% - Halfway, Halfway)
                ConvergeOverTimeSlowing = (TargetHalf * (Halfway - TimeFAc) + Target * TimeFAc) _
                     / Halfway
            End If

        End Function


        '=============================Function.BAS  =======================
        Function ConvergeOverTimeRate(Base As Single, Target As Single, YearsToConverge As Integer) As Single
            ' converges to target (often predicted) value over specified years
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            If Base <= 0 Or Target <= 0 Then
                ConvergeOverTimeRate = ConvergeOverTime(Base, Target, YearsToConverge)
                Exit Function
            End If
            YearsToConverge = AMAX(YearsToConverge, 1)
            Dim Gap, RatetoConverge As Single
            Gap = Target - Base
            RatetoConverge = CSng((Target / Base) ^ (1 / YearsToConverge))
            ConvergeOverTimeRate = Base * RatetoConverge

        End Function

        Function ConvergeOverTime1(Base As Single, Target As Single, YearsToConverge As Integer) As Single
            ' converges to target (often predicted) value over specified years
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'mti Education secondary lower and upper October 2007

            YearsToConverge = AMAX(YearsToConverge, 1)
            Dim TimeFAc As Integer
            TimeFAc = Amin(IY% - 1, YearsToConverge)
            ConvergeOverTime1 = (Base * (YearsToConverge - TimeFAc) + Target * TimeFAc) _
                 / YearsToConverge

        End Function

        '=============================Function.BAS  =======================
        Function ConvergeOverTimeSmooth(ByRef Base As Single, Target As Single, YearsToConverge As Integer, Optional Changebase As Boolean = True) As Single
            ' converges to target (often predicted) value over specified years
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'mti February 10, 2009
            'calculate gap
            ConvergeOverTimeSmooth = Base + (Target - Base) / YearsToConverge
            'Move the base; In case the function is called twice by the same variable in the same time step- do not move the base twice
            If Changebase Then Base = ConvergeOverTimeSmooth
        End Function

        Function ConvergeOverPath(ByRef Base As Single, Target As Single, path_cur_val As Single, path_init_val As Single, path_final_val As Single) As Single
            'mti 2020/09/01 New Function
            ' converges to target (often predicted) value by fraction of 1
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'mti 2021/01/11 Whole proc
            If path_final_val = path_init_val Then
                ConvergeOverPath = Base
                Exit Function
            End If

            Dim PathFrac = (path_cur_val - path_init_val) / (path_final_val - path_init_val)

            'mti 2021/04/06 start
            'PathFrac = AMAX(0, Amin(1, PathFrac))

            PathFrac = AMAX(-0.3, Amin(1, PathFrac))    'the above prevents downshift! what if scenario drops survival rate and that needs to go to other variables
            'mti 2021/04/06 End

            ConvergeOverPath = Base * (1 - PathFrac) + Target * PathFrac

        End Function

        'jrs 2019/05/31
        Public Sub gLoadWBGroupIds(ByRef GroupIDs() As Integer)
            ReDim GroupIDs(NR)
            'Load all countries and their membership
            'WB Income Level (High to Low), 54
            'High Income is 4
            'Low Income is 1
            '485 WB UpMidIncome Economies
            '486 WB LowMidIncome Economies
            '487 WB Low-Income Economies
            '488 WB High-Income Economies

            Using db As IFsContext = New IFsContext()
                Dim rs = (From GLI In db.GListID
                          Join GL In db.GList On GLI.Code Equals GL.GListCode
                          Join GIF In db.GroupIDNewFull On GL.RegionName Equals GIF.GroupName
                          Join GF In db.GroupingFull On GIF.GroupNum Equals GF.GroupId
                          Where GLI.Description = "WB Income Level (High to Low)"
                          Select Key = GF.RegionId, Value = GIF.GroupName).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )

                For Each item In rs
                    Select Case item.Value
                        'jrs 2024/12/02 updating group names
                        Case "WB High Income"
                            GroupIDs(item.Key) = 4
                            'jrs 2025/11/19 updating group names again
                        Case "WB Upper-Middle Income"
                            GroupIDs(item.Key) = 3
                            'jrs 2025/11/19 updating group names again
                        Case "WB Lower-Middle Income"
                            GroupIDs(item.Key) = 2
                        Case "WB Low Income"
                            GroupIDs(item.Key) = 1
                    End Select
                Next
            End Using
        End Sub

        'jrs 2007/01/30
        'Read Provinces and Determine which Regions are to be summed
        Public Sub LoadProvinces(context As IForecastContext)
            Dim II As Integer
            For II = 1 To MaxVal(1) - 1
                RegiontoSum(II) = True
            Next II

            Dim Regions As Dictionary(Of String, String) = New Dictionary(Of String, String)
            Using db As ProvincesContext = New ProvincesContext()
                'jrs 2007/05/25 adde AllProvinces
                Dim valtbl = (From AP In db.AllProvinces Join R In db.Regionalization On AP.Country Equals R.Country
                              Order By R.Region
                              Select Key = R.Region + ";" + AP.Country, Value = AP.AllProvs)

                Dim regcount As Integer
                regcount% = 1
                Dim sReg As String, sCtry As String, pos As Integer
                For Each item In valtbl
                    If regcount% < MaxVal(1) Then
                        pos = item.Key.IndexOf(";")
                        sReg = item.Key.Substring(0, pos)
                        sCtry = item.Key.Substring(pos + 1)
                        Do While sReg > context.GetDimension(1).Buckets(CShort(regcount%))
                            regcount% = regcount% + 1
                        Loop
                        If sReg = context.GetDimension(1).Buckets(CShort(regcount%)) Then
                            RegiontoSum(regcount%) = item.Value
                            sCtry = item.Key.Replace(context.GetDimension(1).Buckets(CShort(regcount%)) + ";", "")
                            If Not Regions.ContainsKey(sCtry) Then
                                Regions.Add(sCtry, sCtry)
                            End If
                        End If
                    End If
                Next
            End Using

            Dim sRegions As String = ""
            'ttt 2019/10/08
            'z191 Improve
            For Each ke In Regions.Keys
                sRegions &= "'" & ke & "', "
            Next
            If sRegions.EndsWith("', ") Then
                sRegions = sRegions.Substring(0, sRegions.Length - 2)
            End If
            'End z191 Improve
            'For Each rr In Regions
            '    If sRegions = "" Then
            '        sRegions = "'" & rr.Key & "'"
            '    Else
            '        sRegions = sRegions & ", '" & rr.Key & "'"
            '    End If
            'Next

            'jrs 2007/02/14
            bAllProvinces = False

            If sRegions > "" Then
                Using db As ProvincesContext = New ProvincesContext()
                    'jrs 2026/03/04
                    'Dim valtbl = (From AP In db.AllProvinces Join R In Regions On AP.Country Equals R.Key
                    '              Order By AP.Country
                    '              Select Key = AP.Country, Value = AP.AllProvs)

                    Dim provinces = db.AllProvinces.ToList()

                    Dim valtbl = From AP In provinces
                                 Join R In Regions On AP.Country Equals R.Key
                                 Order By AP.Country
                                 Select New With {
                 .Key = AP.Country,
                 .Value = AP.AllProvs
             }

                    For Each item In valtbl
                        If item.Value Then
                            'jrs 2026/03/04
                            If getRegionFromCtry(item.Key, context) > "" Then
                                RegiontoSum(gRegionIdx(getRegionFromCtry(item.Key, context))) = False
                            End If


                            'jrs 2007/01/12
                            bAllProvinces = True
                        End If
                    Next
                End Using
            End If


        End Sub

        'jrs 2014/11/06
        'Function to Retrieve a Region based on a Country Name
        Public Function getRegionFromCtry(sCtry As String, context As IForecastContext) As String
            'jrs 2026/03/04
            getRegionFromCtry = ""

            Dim sTCtry As String = sCtry
            Using dbR As IFsContext = New IFsContext()
                'jrs 2008/02/11
                'Try a translation first
                Dim valtblR = (From CT In dbR.CountryTranslation Where CT.ESRICountries = sCtry
                               Select Key = CT.Country, Value = CT.Country)
                For Each item In valtblR
                    sTCtry = item.Key
                Next
            End Using

            Dim valtbl = (From R In context.Regionalization Where R.Key = sTCtry
                          Select Key = R.Key, Value = R.Value)

            For Each item In valtbl
                getRegionFromCtry = context.GetDimension(1).Buckets(CShort(item.Value))
                Exit Function
            Next

            '2008/02/11
            'check for groups (in the case of provinces loaded)
            If bAllProvinces Then
                Using dbR As IFsContext = New IFsContext()
                    'check the forecast name
                    Dim valtblR = (From GI In dbR.GroupIDNewFull Where GI.GroupName = sTCtry
                                   Select Key = GI.GroupNum, Value = GI.GroupName)
                    If valtblR.Count > 0 Then
                        getRegionFromCtry = sTCtry
                    End If

                    'jrs 2014/11/06 when adding provinces and country <> than region, like the case of USA
                    If getRegionFromCtry = "" Then
                        'check the forecast name
                        Dim valtblR2 = (From CT In dbR.CountryTranslation Where CT.Country = sTCtry
                                        Select Key = CT.Country, Value = CT.IFsCountriesForecast)
                        For Each item In valtblR2
                            getRegionFromCtry = If(item.Value, sCtry)
                        Next
                    End If
                End Using
            End If

        End Function

        Public Function gRegionIdx(sReg As String) As Integer
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'jrs 2014/11/06
            'This procedured is used before Main Menu is loaded, so can't use the GUI
            'gRegionIdx = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, sReg, False)
            'JRS 12/03/01 GROUPING
            Dim eleloop As Integer
            'jrs 2026/03/04 added -1
            For eleloop = 1 To MaxVal(1) - 1 'Up to number of regions
                If sReg.ToUpper = GRPS$(1, eleloop).ToUpper Then
                    gRegionIdx = eleloop
                    Exit For
                End If
            Next eleloop
        End Function

        Public Function pop_wt_avg(R%, male_val As Single, fem_val As Single, agedst(,,) As Double, from_cohort As Integer, to_cohort As Integer) As Double
            'mti 2017/11/06 New Procedure
            Dim j%
            pop_wt_avg = 0
            Dim POPMale, POPFemale As Double
            POPMale = 0
            POPFemale = 0
            For j% = from_cohort To to_cohort  '15 plus
                POPMale = POPMale + agedst(j%, R%, 1)
                POPFemale = POPFemale + agedst(j%, R%, 2)
            Next j%

            pop_wt_avg = (male_val * POPMale + fem_val * POPFemale) / (POPMale + POPFemale)

        End Function

        Public Function pop_by_cohort(R%, agedst(,,) As Double, from_cohort As Integer, Optional to_cohort As Integer = 0) As Double
            'mti 2017/11/06 New Procedure
            Dim j%
            pop_by_cohort = 0
            If to_cohort = 0 Then to_cohort = NCohorts
            For j% = from_cohort To to_cohort
                pop_by_cohort = pop_by_cohort + agedst(j%, R%, 1) + agedst(j%, R%, 2)
            Next j%

        End Function

        'jrs 2019/09/27
        'Sensitivity Not being translated
        Public Scenarioflag As Boolean
        Public ScenarioFileDir, SupplementalDataFile, SupplementalHistoricDataFile As String
        'jrs 2022/09/15
        Public DataFileDir As String

        Sub ProcessTimeSeries(SensitivityNumber As Short, StartYear As Short, context As IContext,
                              ByRef IFsPreResults As Dictionary(Of String, IDataframe), ByRef IFsResults As Dictionary(Of String, Dataframe)) 'jrs 2016/10/28

            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            'mti 2019/05/14 - take entire procedure (the changes were commented out later)

            'jrs 2008/05/05
            Dim bCT As Boolean
            'jrs 2008/06/03
            Dim CurrFact As Single
            Dim VLine2 As String
            'jrs 2014/03/07
            Dim dim2idx As Integer

            'Target Year, Target Value, FirstYear of change towards target
            Dim TgYr As Integer, TgVal As Single, TgFstYr As Integer

            'JRS 07/15/03
            'gvdb 2024/04/05
            ' unused variable
            'Dim GroupLines() As String
            'Dim GroupVals() As Single

            'jrs 2019/09/27 ReDim N(filen) As Single

            'JRS 09/04/03
            Dim RegIdx As Integer

            '    'mti 2019/05/14 start
            '    'For flex historical rebase; reading .dat should start at 1960 irrespective of firstyearhistory; SeriesOffSet = Amax(0,FirstYearHistory - 1960)
            '    Dim HistRebuild As Boolean
            '    Dim SeriesOffset As Integer
            '    HistRebuild = False
            '    SeriesOffset = 0
            '    'mti 2019/05/14 End

            '   Clear plot vector because may change elements in it
            '   ---------------------------------------------------

            'jrs 2019/09/27
            'COUNTE = 0
            'frm_MainMenu.IFsGUIctrl1.XClearPlot

            'jrs 2019/09/27
            ''JRS 06/09/03 RAND PARDEE
            'Dim dbSenInput As Database
            'Dim rsSenInput As Recordset


            '   Check for existence of IFs.Dat
            '   ------------------------------
            Dim msg, VLine, Var, X(), P1, StringHold, StringHold1, StringHold2, StringHold3 As String
            Dim xIdx, P1Val, Dim1No, Dim1Max, Dim2Max, Dim3Max As Integer
            Dim nvalue, prevvalue As Single
            Dim currVar As IVariable
            Dim bValidDim As Boolean
            Dim dfWeight As IDataframe = Nothing
            Dim dfNidx As Integer
            Dim Dim1Count, Dim2Count, Dim3Count As Short
            Dim sAgg As String
            Dim myValue As Single
            Dim factor As Single
            Dim Maximum As Short
            Dim P2 As String, P2Val As Integer
            Dim P3 As String, P3Val As Integer
            Dim DimNo, DimMax As Integer
            Dim myValue2_1 As Single
            Dim factor2_1 As Single
            Dim sAgg2_1 As String
            Dim myValue3_1 As Single
            Dim factor3_1 As Single
            Dim sAgg3_1 As String

            Try
                'jrs 2019/09/27
                '                If frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                '                    Open SensitivityFileDir$ + "SensitivityInput.dat" For Input As #1
                '                ElseIf Scenarioflag Then
                '                    Open ScenarioFileDir$ + "Working.sce" For Input As #1
                '                Else
                '                    If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                '                        Open DataFileDir$ + SupplementalHistoricDataFile$ For Input As #1
                ''            'mti 2019/05/14 start
                '                        '            HistRebuild = True
                '                        '            SeriesOffset = AMAX(0, FirstYearHistory - 1960)
                '                        '            'mti 2019/05/14 End
                '                    Else
                '                        Open DataFileDir$ + SupplementalDataFile$ For Input As #1
                '        End If
                '                End If
                '                Close #1

                Dim RTime As Short
                If False Then 'jrs 2019/09/27 frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                    'jrs 2019/09/27 RTime = SenRunHorizon% - frm_MainMenu.IFsGUIctrl1.cFirstYear + 1
                Else
                    RTime = CShort(context.LastYear - context.FirstYear + 1)
                End If

                'jrs 2019/09/27 On Error GoTo 0
                Dim Filename As String
                If Scenarioflag = True Then 'jrs 2019/09/27 frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Or Scenarioflag = True Then
                    Filename$ = PutFdrvRun$ + "working.run"
                Else
                    Filename$ = PutFdrvRun$ + context.ActiveBase '"IFsBase.RUN"
                End If

                'jrs 2019/09/27 Open Filename$ For Binary As #2

                'jrs 2019/09/27
                '    If Scenarioflag Then
                '                    With frm_Progress
                '                        '.ZOrder 0
                '                        .Caption = "Processing Scenario Parameters. Please Wait..."
                '                        .ProgressBar1.MIN = 1
                '                        .ProgressBar1.Max = RTime
                '                        .ProgressBar1.Value = .ProgressBar1.MIN
                '                    End With
                '                    frm_Progress.Show 0 'JRS 8/25/01 GUI , frm_ScenarioCreate
                '                End If

                'Open database once to read Group members
                '----------------------------------------
                '                'jrs 2012/05/11
                '                Dim dbG As Database
                '    Set dbG = OpenDatabase(IFs_MDB2Active)

                'jrs 2017/01/06
                'Bilateral Trade
                '---------------
                'jrs 2017/01/04
                Dim ifsVarDyadic As Dictionary(Of String, Single) = New Dictionary(Of String, Single)
                Dim ifsVarDyadicDim3 As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
                If System.IO.File.Exists(PutFdrvRun$ + "IFsForDyadicWork.db") Then
                    '                    Dim PrevVal As Single
                    '                    Dim dbBT As Database
                    '        Set dbBT = OpenDatabase(PutFdrvRun$ + "IFsForDyadicWork.mdb")
                    '        Dim dbBTBase As Database
                    '        Set dbBTBase = OpenDatabase(DataFileDir + "IFsForDyadic.mdb")

                    Using dbBT As IFsForDyadicContext = New IFsForDyadicContext()
                        Dim conn = dbBT.Database.GetDbConnection
                        If conn.State = 0 Then 'closed
                            conn.Open()
                        End If

                        'jrs 2019/11/18 *** begin ***
                        Using cmd2 = conn.CreateCommand()
                            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                            cmd2.ExecuteNonQuery()
                        End Using
                        dbBT.Database.AutoTransactionsEnabled = False
                        dbBT.ChangeTracker.AutoDetectChangesEnabled = False
                        dbBT.ChangeTracker.LazyLoadingEnabled = False

                        Dim pd As ParametersDefault
                        For Each pd In dbBT.ParametersDefault
                            ifsVarDyadic.Add(pd.ParameterName, pd.DefaultValue)
                            If pd.Dim3.HasValue Then
                                If pd.Dim3 > 0 Then
                                    ifsVarDyadicDim3.Add(pd.ParameterName, CInt(pd.Dim3))
                                End If
                            End If
                        Next
                    End Using

                    '        Dim rsBT As Recordset, rsDef As Recordset, rsDet As Recordset
                    '                    If orgMaxVars = 0 Then
                    '                        orgMaxVars = NumbVars

                    '            Set rsBT = dbBT.OpenRecordset("SELECT * FROM ParametersDefault ORDER BY Description")
                    '            i2 = NumbVars
                    '                        NumbVars = NumbVars + rsBT.RecordCount
                    '                        ReDim Preserve VScenCat(NumbVars)
                    '                        ReDim Preserve VScenario(NumbVars)
                    '                        ReDim Preserve V(NumbVars)
                    '                        ReDim Preserve VDim1(NumbVars)
                    '                        ReDim Preserve VDim2(NumbVars)
                    '                        ReDim Preserve VDim3(NumbVars)
                    '                        ReDim Preserve VNoTot2(NumbVars)
                    '                        ReDim Preserve VDef(NumbVars)
                    '                        ReDim Preserve VHelpID(NumbVars)
                    '                        ReDim Preserve VUnits(NumbVars)
                    '                        ReDim Preserve VarPoint(NumbVars)
                    '                        ReDim Preserve VShiftUp(NumbVars)
                    '                        ReDim Preserve VShiftDown(NumbVars)
                    '                        ReDim Preserve VShiftYears(NumbVars)
                    '                        ReDim Preserve VType(NumbVars)
                    '                        ReDim Preserve VMax(NumbVars)
                    '                        ReDim Preserve VMin(NumbVars)
                    '                        ReDim Preserve VExpl(NumbVars)
                    '                        'jrs 2019/09/11
                    '                        ReDim Preserve VHidePrivateVars(NumbVars)

                    '                        Do While Not rsBT.EOF
                    '                            i2 = i2 + 1
                    '                            VScenCat(i2) = 810
                    '                            VScenario(i2) = rsBT.Fields("Description")
                    '                            VDef(i2) = rsBT.Fields("Description")
                    '                            VExpl(i2) = rsBT.Fields("Description")
                    '                            V(i2) = rsBT.Fields("ParameterName")
                    '                            VDim1(i2) = 1
                    '                            VDim2(i2) = 1
                    '                            'jrs 2018/01/06
                    '                            If Not IsNull(rsBT.Fields("Dim3")) Then
                    '                                VDim3(i2) = rsBT.Fields("Dim3")
                    '                            Else
                    '                                VDim3(i2) = 0
                    '                            End If
                    '                            VNoTot2(i2) = False
                    '                            VShiftUp(i2) = 0
                    '                            VShiftDown(i2) = 0
                    '                            VShiftYears(i2) = 0
                    '                            VType(i2) = 3 '2 is only initial conditions, doesn't allow setting a year to start the intervention
                    '                            VarPoint(i2) = rsBT.Fields("DefaultValue")
                    '                            VMax(i2) = 100
                    '                            VMin(i2) = -100
                    '                            IFsVars.Add V(i2), i2
                    '                rsBT.MoveNext
                    '                        Loop
                    '                        rsBT.Close
                    '                    End If
                    '        Set rsBT = dbBT.OpenRecordset("ParametersDetail")
                    '        Set rsDef = dbBTBase.OpenRecordset("ParametersDefault")
                    '        rsDef.Index = "PrimaryKey"
                    '        Set rsDet = dbBTBase.OpenRecordset("ParametersDetail")
                    '        rsDet.Index = "Paps"
                End If


                '   Do this for all years
                '   ---------------------

                '                BadDatFile = False
                '                'jrs 2008/05/05 moving it out of the year loop, it was just before the recordset open
                '                If frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                '        Set dbSenInput = OpenDatabase(SensitivityFileDir$ + "SensitivityInput.mdb")
                '    End If

                'jrs 2019/12/13
                Dim IFsResultsBase As Dictionary(Of String, IDataframe) = New Dictionary(Of String, IDataframe)
                Dim storageSvc As IStorageService = New StorageService(Nothing, "IFsBase.run.db")
                Dim BaseFile As IStorage = storageSvc.Open(False)

                'jrs 2016/10/28 For iYear% = 1 To RTime
                Dim LastVar As String
                Dim No() As Single
                For iYear% = StartYear To RTime

                    '                    If Scenarioflag Then
                    '                        frm_Progress.ProgressBar1.Value = iYear%
                    '                        frm_Progress.Refresh
                    '                    End If

                    '                    If frm_MainMenu.IFsGUIctrl1.cSensitivityFlag <> True And Not Scenarioflag Then
                    '                        txt_year = Str$(frm_MainMenu.IFsGUIctrl1.cFirstYear + iYear% - 1)
                    '                        frm_RebBase.lbl_curyear.Caption = txt_year
                    '                        frm_RebBase.lbl_curyear.Visible = True
                    '                        frm_RebBase.lbl_curyear.Refresh
                    '                        msg$ = "Adding Information from " + "IFs.dat to Base File - Please Wait"
                    '                        frm_RebBase.lbl_Status.Caption = msg$
                    '                        frm_RebBase.lbl_Status.Visible = True
                    '                        frm_RebBase.lbl_Status.Refresh
                    '                    End If

                    If False Then 'jrs 2019/09/27 frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                        '                        Open SensitivityFileDir$ + "SensitivityInput.dat" For Input As #1
                        '            'JRS 06/09/03 RAND PARDEE
                        '            Set rsSenInput = dbSenInput.OpenRecordset("SELECT * FROM SensitivityInput " + _
                        '            "WHERE Run=" + Str(SensitivityNumber) + " AND Year = " + _
                        '            Str(frm_MainMenu.IFsGUIctrl1.cFirstYear + iYear% - 1))
                        '            msg$ = "Reading SensitivityInput.dat for year " +
                        '                Str$(frm_MainMenu.IFsGUIctrl1.cFirstYear + iYear% - 1) + " - Please Wait"
                        '                        frm_Sensitivity.lbl_Status.Caption = msg$
                        '                        frm_Sensitivity.lbl_Status.Refresh
                    ElseIf Scenarioflag = True Then
                        '                        Open ScenarioFileDir$ + "Working.sce" For Input As #1
                        '            'debug.Print "Opened Scenario.sce"
                        Filename = ScenarioFileDir + "Working.sce"
                    Else
                        If context.HistoricMode Then
                            '                            Open DataFileDir$ + SupplementalHistoricDataFile$ For Input As #1
                            Filename = context.DataFileDir + SupplementalHistoricDataFile$
                        Else
                            '                            Open DataFileDir$ + SupplementalDataFile$ For Input As #1
                            Filename = context.DataFileDir + SupplementalDataFile$
                        End If
                    End If
                    LastVar$ = ""

                    Using reader As StreamReader = New StreamReader(Filename)
                        ' Read one line from file
                        VLine = reader.ReadLine

                        '                    '     Filen is calculated in INIT from .Var file
                        '                    '     ------------------------------------------

                        '                    rps = (iYear% - 1) * filen * 4 + 1
                        '                    rps2 = rps
                        '                    LoopNumb = filen \ 5000
                        '                    LoopRem = filen Mod 5000
                        '                    If LoopNumb >= 1 Then
                        '                        For LoopCnt% = 1 To LoopNumb
                        '                            LoopCntReal = LoopCnt%
                        '              Get #2, rps2, FILEBUF2
                        '              NCntAdd = 5000 * (LoopCntReal - 1)
                        '                            For icnt% = 1 To 5000
                        '                                icntm% = icnt% - 1
                        '                                N(icnt% + NCntAdd) = FILEBUF2.elem2(icntm%)
                        '                            Next icnt%
                        '                            rps2 = rps2 + 20000
                        '                        Next LoopCnt%
                        '                    End If
                        '                    If LoopRem > 0 Then
                        '          'JRS 8/20/01 Get #2, rps2, FILEBUF3
                        '          Get #2, rps2, FILEBUF3.elem3
                        '          NCntAdd = 5000 * LoopNumb
                        '                        ICntTar = filen - 5000 * LoopNumb
                        '                        For icnt% = 1 To ICntTar
                        '                            icntm% = icnt% - 1
                        '                            N(icnt% + NCntAdd) = FILEBUF3.elem3(icntm%)
                        '                        Next icnt%
                        '                    End If

                        '     Read the variable file (could be Supplemental.dat or Scenario.sce file)
                        '     -----------------------------------------------------------------------
                        'mti 19 Nov 01
                        Do While Not VLine = Nothing '07/10/03 Or SensitivityFlag = True
                            'JRS 06/10/03 RAND PARDEE
                            '                        If (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                            '                            If (rsSenInput.EOF) Then Exit Do
                            '                        End If
                            '                        SensitivityRunSkip = False

                            If False Then 'frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                '                            'JRS 06/09/03 RAND PARDEE Input #1, FileSensitivityNumber, Var$
                                '                            'JRS 06/09/03 RAND PARDEE If FileSensitivityNumber <> SensitivityNumber Then
                                '                            'JRS 06/09/03 RAND PARDEE Line Input #1, VLine$ 'Read and ignore rest of line
                                '                            'JRS 06/09/03 RAND PARDEE SensitivityRunSkip$ = True
                                '                            'JRS 06/09/03 RAND PARDEE End If
                                '                            'JRS 06/09/03 RAND PARDEE
                                '                            If Not rsSenInput.EOF Then
                                '                                FileSensitivityNumber = rsSenInput.Fields("Run")
                                '                                Var$ = rsSenInput.Fields("VarName")
                                '                            End If
                            ElseIf Scenarioflag = True Then
                                '                            Input #1, Var$, Var$ 'Discard scenariotype = High, Low, etc.
                                X = VLine.Split(New Char() {","c})
                                Var$ = X(1).Replace("""", "")
                                xIdx = 2
                                'mti 19 Nov 01
                                If Var$ = "START" Then 'Comment,Start
                                    Exit Do
                                End If
                                'mti 19 Nov 01
                            Else
                                'start reading .dat
                                X = VLine.Split(New Char() {","c})
                                Var$ = X(0).Replace("""", "")
                                xIdx = 1
                                'jrs 2012/09/24
                                If Var$ = "" Then Exit Do
                                'jrs 2006/06/07
                                Do While Var.ToUpper = "COMMENT" 'Comment,Start
                                    VLine = reader.ReadLine
                                    X = VLine.Split(New Char() {","c})
                                    Var$ = X(1).Replace("""", "")
                                Loop
                                '                            msg$ = "Adding Information from " + StartFile$
                                '                            msg$ = msg$ + ".dat to Base File - Variable " + Var$
                                '                            frm_RebBase.lbl_Status.Caption = msg$
                                '                            frm_RebBase.lbl_Status.Visible = True
                                '                            frm_RebBase.lbl_Status.Refresh
                            End If
                            '                        If Var$ <> LastVar$ And SensitivityRunSkip <> True Then Call FindVarNumb(chk, Var$)
                            'search to match variable num

                            '        'mti 2019/05/14 start
                            '        If VType(chk) <> 3 And HistRebuild = True And SeriesOffset > 0 Then
                            '            'processing non-series variable from .dat for a historical base rebuild with first year later than 1960
                            '            chk = 0
                            '            Var$ = ""
                            '            LastVar$ = ""
                            '        End If
                            '
                            ''        If Trim(UCase(Var$)) = "GDSM" Then Stop
                            '
                            '        'mti 2019/05/14 start


                            'Now that know the number of the variable, process it as either non-dimensional,
                            '  simple variable, or as singly or doubly-dimenionsed
                            '                        If chk <> 0 And SensitivityRunSkip <> True Then
                            If context.VariableNames.Contains(Var$) Or context.VariableNames.Contains(Var$.ToUpper) Or context.VariableNames.Contains(Var$.ToLower) Then
                                Dim pos As Integer
                                pos = VLine.IndexOf(Var$)
                                VLine = VLine.Substring(pos)
                                VLine = VLine.Replace(Var$ + ",", "")
                                VLine = VLine.Replace("CUSTOM,", "")
                                VLine = VLine.Replace("MEDIUM,", "")
                                VLine = VLine.Replace("HIGH0010,", "")
                                VLine = VLine.Replace("LOW0010,", "")
                                VLine = VLine.Replace("LOW0020,", "")
                                If Not context.VariableNames.Contains(Var$) Then
                                    Var$ = Var$.ToUpper
                                End If
                                If Not context.VariableNames.Contains(Var$) Then
                                    Var$ = Var$.ToLower
                                End If
                                currVar = context.GetVariable(Var$)

                                If currVar.Aggregation Is Nothing Then
                                    dfWeight = Nothing
                                Else
                                    Select Case currVar.Aggregation
                                        Case "GDP"
                                            dfWeight = IFsPreResults("GDP")
                                        Case "POP"
                                            dfWeight = IFsPreResults("POP")
                                        Case Else
                                            If IFsAggRules.ContainsKey(currVar.Aggregation) AndAlso IFsPreResults.ContainsKey(IFsAggRules(currVar.Aggregation)) Then
                                                dfWeight = IFsPreResults(IFsAggRules(currVar.Aggregation))
                                            Else
                                                dfWeight = Nothing
                                            End If
                                    End Select
                                End If

                                'Only on FirstYear
                                If iYear% = StartYear And Not IFsResults.ContainsKey(Var) Then
                                    If IFsPreResults.ContainsKey(Var$) Then
                                        Dim FirstRowSize As Integer = 1
                                        Dim DimCount As Integer = currVar.Dimensions.Count
                                        Dim Dim1Size, Dim2Size, Dim3Size As Short

                                        If DimCount >= 2 Then
                                            Dim1Size = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                            FirstRowSize = FirstRowSize * Dim1Size
                                        End If
                                        If DimCount >= 3 Then
                                            Dim2Size = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                                            FirstRowSize = FirstRowSize * Dim2Size
                                        End If
                                        If DimCount >= 4 Then
                                            Dim3Size = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)
                                            FirstRowSize = FirstRowSize * Dim3Size
                                        End If
                                        If IFsPreResults(Var).RowCount > FirstRowSize Then
                                            IFsResults.Add(Var$, CType(IFsPreResults(Var$), Dataframe))
                                        Else
                                            IFsResults.Add(Var$, Dataframe.Create(Var$, context, context.FirstYear, context.LastYear, 0))

                                            'jrs 2020/01/10 added if >0 then
                                            'if there's data in first year
                                            If IFsPreResults(Var).RowCount > 0 Then
                                                'Copy Results from first year
                                                Dim i, j, k As Short
                                                Dim dfIdx As Integer
                                                Select Case DimCount
                                                    Case 1
                                                        dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear)
                                                        IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                    Case 2
                                                        For i = 1 To Dim1Size
                                                            dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, i, Dim1Size)
                                                            IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                        Next i
                                                    Case 3
                                                        For i = 1 To Dim1Size
                                                            For j = 1 To Dim2Size
                                                                dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, i, Dim1Size, j, Dim2Size)
                                                                IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                            Next j
                                                        Next i
                                                    Case 4
                                                        For i = 1 To Dim1Size
                                                            For j = 1 To Dim2Size
                                                                For k = 1 To Dim3Size
                                                                    dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, i, Dim1Size, j, Dim2Size, k, Dim3Size)
                                                                    IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                                Next k
                                                            Next j
                                                        Next i
                                                End Select
                                            End If
                                        End If
                                    Else
                                        IFsResults.Add(Var$, Dataframe.Create(Var$, context, context.FirstYear, context.LastYear, 0))
                                    End If
                                End If

                                'If VDim1%(chk) = 0 And VDim2%(chk) = 0 Then  'routine for simple variables
                                If currVar.Dimensions.Count = 1 Then
                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                    'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    'Line Input #1, VLine$
                                    If X(xIdx).Replace("""", "").Contains(":") Then
                                        nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                    Else
                                        nvalue = CSng(X(xIdx).Replace("""", ""))
                                    End If

                                    'mti 2019/05/14 start
                                    'revert to the earlier code (but save new code)
                                    'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then
                                    If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year                                    
                                        Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                        'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                        'mti 2019/05/14 End
                                    End If
                                    'End If 'Not Sensitivity

                                    'JRS 06/09/03 RAND PARDEE
                                    '                                If frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    '                                    nvalue = rsSenInput.Fields("Value")
                                    '                                    rsSenInput.MoveNext
                                    '                                    If rsSenInput.EOF Then
                                    '                                        Do While Not EOF(1)
                                    '                                            Line Input #1, VLine$
                                    '                        Loop
                                    '                                    End If
                                    '                                End If
                                    IFsResults(Var)(getDataFrameIdxFromDims(CShort(iYear% + FirstYear - 1), FirstYear)) = nvalue
                                    'ElseIf VDim1%(chk) > 0 And VDim2%(chk) = 0 Then 'routine for single dim. vars.
                                ElseIf currVar.Dimensions.Count = 2 Then
                                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                    '                                'JRS 06/09/03 RAND PARDEE
                                    '                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    'Input #1, P1  'Could be string pointer or numerical pointer to
                                    P1 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1

                                    '                                    'element number; if numerical, ok as is
                                    '                                Else
                                    '                                    P1 = rsSenInput.Fields("Dim")
                                    '                                End If
                                    'If Val(P1) = 0 Then  'is string and must determine numerical
                                    'IsNumeric = Integer.TryParse(P1, P1Val)
                                    If Not Integer.TryParse(P1, P1Val) Then
                                        'jrs 2021/04/13
                                        'Logic to check "Congo, Dem. Rep of the"
                                        If VLine.Substring(0, 1) = """" Then
                                            Dim pos2 As Integer
                                            pos2 = VLine.IndexOf("""", 1) - 1
                                            P1 = VLine.Substring(1, pos2)
                                            If P1.IndexOf(",") >= 0 Then
                                                xIdx = xIdx + 1
                                            End If
                                        ElseIf VLine.IndexOf("Congo, Dem. Republic of the") = 0 Then
                                            P1 = "Congo, Dem. Republic of the"
                                            xIdx = xIdx + 1
                                        End If
                                        VLine = VLine.Replace("""" + P1 + """,", "")
                                        VLine = VLine.Replace(P1 + ",", "")
                                        VLine = VLine.Replace(P1 + " ,", "") 'jrs 2020/07/14
                                        Dim1No = currVar.Dimensions(1)
                                        Dim1Max = context.GetDimension(Dim1No).Buckets.Count
                                        'Dim1Max = MaxVal(VDim1%(chk))
                                        'Dim1No = VDim1%(chk)
                                        StringHold$ = P1
                                        bValidDim = True
                                        For eleloop = 1 To Dim1Max 'Up to number of elements on dimension
                                            'jrs 2014/03/07
                                            If StringHold$ = "Total" And GRPS(Dim1No, Dim1Max - 1) <> "Total" Then
                                                P1Val = 1
                                                Exit For
                                            End If

                                            If StringHold$.ToUpper = GRPS$(Dim1No, eleloop).ToUpper Then
                                                P1Val = eleloop
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            If GRPS(Dim1No, eleloop).Contains(",") Then
                                                If StringHold$.ToUpper = GRPS(Dim1No, eleloop).Substring(0, GRPS(Dim1No, eleloop).IndexOf(",")).ToUpper Then
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    StringHold = StringHold + ", " + P1
                                                    If StringHold$.ToUpper = GRPS$(Dim1No, eleloop).ToUpper Then
                                                        VLine = VLine.Replace("""" + StringHold + """,", "")
                                                        VLine = VLine.Replace(""" " + P1 + """,", "")
                                                        VLine = VLine.Replace(P1 + ",", "")
                                                        P1Val = eleloop
                                                        Exit For
                                                    Else
                                                        StringHold = StringHold.Replace(", " + P1, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= Dim1Max Then 'Not Found
                                                'JRS 12/03/01 GROUPING
                                                gLoadCountries(StringHold$)
                                                If gGroupTot > 0 Then
                                                    P1Val = gCountryGroupIdx(1)
                                                    P1 = P1Val.ToString
                                                End If
                                                If Not Integer.TryParse(P1, P1Val) Then
                                                    msg$ = StringHold$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    bValidDim = False
                                                    'JRS 04/07/03
                                                    If iYear% = 1 And bShowMsg Then 'jrs 2008/04/02 added bShowMsg
                                                        Debug.Print(msg$)
                                                    End If
                                                    'JRS 09/06/02 Close #1
                                                    'JRS 09/06/02 Close #2
                                                    'JRS 06/09/03 RAND PARDEE
                                                    'jrs 2019/09/30 If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                                    'Line Input #1, VLine$ 'read rest of line
                                                    'Else
                                                    '    rsSenInput.MoveNext
                                                    '    If rsSenInput.EOF Then
                                                    '        Do While Not EOF(1)
                                                    '            Line Input #1, VLine$
                                                    '                                    Loop
                                                    '    End If
                                                    'End If
                                                    'GoTo ReadAnother
                                                End If
                                            End If
                                        Next eleloop
                                    Else
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P1.Length + 1)
                                    End If

                                    If bValidDim Then
                                        'JRS 06/09/03 RAND PARDEE IF END IF
                                        'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                        'Line Input #1, VLine$ 'read rest of line
                                        If X(xIdx).Replace("""", "").Contains(":") Then
                                            nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                        Else
                                            nvalue = CSng(X(xIdx).Replace("""", "")) 'first and possibly only value (unless is series)
                                        End If
                                        'JRS 07/14/03 BEGIN
                                        If gGroupTot > 0 Then
                                            'jrs 2008/04/10
                                            bCT = If(currVar.CommonTarget, False)
                                            If bCT Then
                                                'jrs 2008/06/03
                                                'Currency Handling
                                                CurrFact = 1
                                                If Not currVar.Currency = Nothing Then
                                                    If currVar.Currency.ToUpper = "TRUE" Then
                                                        CurrFact = CurrencyVal
                                                    End If
                                                End If

                                                'Find Target Year, Target Value, FirstYear of change towards target
                                                'Need entire VLine before parsing
                                                FindCommonTargetVars(TgYr, TgVal, TgFstYr, VLine)
                                                'Retrieve N for TgFstYr (No)
                                                'there might be different TgFstYrs per parameter
                                                If iYear >= TgFstYr And iYear < TgYr Then
                                                    'jrs 2019/12/13
                                                    If iYear = TgFstYr AndAlso Not IFsResultsBase.ContainsKey(Var) Then
                                                        IFsResultsBase.Add(Var, BaseFile.LoadDataframe(Var))
                                                    End If

                                                    ReDim No(gGroupTot)
                                                    For eleloop = 1 To gGroupTot
                                                        'reading base file
                                                        'jrs 2008/05/05 changed from TgFstYr-1 back to 1
                                                        No(eleloop) = getSingleValue(currVar, CType(IFsResultsBase(Var), IDataframe), CShort(iYear + FirstYear - 1), context, CShort(gCountryGroupIdx(eleloop)))
                                                    Next eleloop
                                                End If
                                            End If
                                            If iYear > 1 Then
                                                'mti 2019/05/14 start
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                                'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                                'mti 2019/05/14 End
                                            End If
                                            'jrs 2008/04/11
                                            If Not bCT Then
                                                If IFsPreResults.ContainsKey(Var) Then
                                                    'jrs 2020/07/28
                                                    If (currVar.Aggregation.ToUpper = "NONE" Or currVar.Aggregation = "") And iYear% = StartYear Then
                                                        Debug.Print("The aggregation rule for " & currVar.Name & " is """ & currVar.Aggregation & """. There could be problems setting parameters for groups.")
                                                    End If

                                                    myValue = getGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(iYear + FirstYear - 1), context, dfWeight)       'JRS 08/11/03 begin
                                                Else
                                                    myValue = 0
                                                End If

                                                'jrs 2005/04/29
                                                sAgg = currVar.Aggregation

                                                If sAgg = "SUM" Then
                                                    If myValue = 0 Then
                                                        factor = nvalue / gGroupTot  'jrs 2006/05/05 instead of 0
                                                    Else
                                                        factor = nvalue / myValue
                                                    End If
                                                Else
                                                    'JRS 08/11/03 end
                                                    'If myValue <> 0 Then
                                                    factor = nvalue - myValue
                                                    'Else
                                                    '    factor = 1
                                                    'End If
                                                End If
                                            End If 'if not bct

                                            Dim CtryIdx As Integer
                                            'jrs 2006/05/04 If factor <> 0 Then changes where sAgg = "SUM" and nvalue = 0 where not applied, enpoilmax
                                            For CtryIdx = 1 To gGroupTot
                                                RegIdx = gCountryGroupIdx(CtryIdx)
                                                'jrs 2008/04/10
                                                'if it's a Common Target parameter
                                                dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count)
                                                If bCT Then
                                                    'if Year is less than Target Year
                                                    If iYear < TgYr Then
                                                        'Year is already equal or greater than when the change starts
                                                        If iYear >= TgFstYr Then
                                                            'No is the value of N in year TgFstYr for Region RegIdx

                                                            'Common Target logic
                                                            IFsResults(Var)(dfNidx) = No(CtryIdx) + (TgVal - No(CtryIdx)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                                        End If
                                                    Else
                                                        'else just assign group value to all members (converged already)
                                                        IFsResults(Var)(dfNidx) = nvalue
                                                    End If
                                                Else
                                                    'jrs 2020/12/14
                                                    If Var$ = "gdprext" And nvalue = -100 Then
                                                        'Assign -100 to all members
                                                        IFsResults(Var)(dfNidx) = -100
                                                        'jrs 2005/05/02
                                                    ElseIf sAgg = "SUM" And myValue <> 0 Then 'jrs 2006/05/05 added And MyValue <> 0
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor
                                                    End If
                                                    'jrs 2011/09/12
                                                    If IFsResults(Var)(dfNidx) > currVar.Max Then
                                                        IFsResults(Var)(dfNidx) = CSng(currVar.Max)
                                                    ElseIf IFsResults(Var)(dfNidx) < currVar.Min Then
                                                        IFsResults(Var)(dfNidx) = CSng(currVar.Min)
                                                    End If
                                                End If
                                            Next CtryIdx
                                        End If
                                        'mti 2019/05/14 start
                                        'JRS 07/14/03 END
                                        If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year
                                            'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then
                                            'mti 2019/05/14 End
                                            'JRS 07/15/03
                                            If gGroupTot = 0 Then
                                                'For idx = 1 To gGroupTot
                                                'Call ParseLine(GroupLines(idx), IYear%, RTime, nvalue)
                                                'GroupVals(idx) = nvalue
                                                'Next idx
                                                'nvalue = GroupVals(1)
                                                'Else
                                                'mti 2019/05/14 Start                                                
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                                'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                                'mti 2019/05/14 End
                                            End If
                                        End If
                                        '                                Else 'Sensitivity
                                        '                                    nvalue = rsSenInput.Fields("Value")
                                        '                                    '                    'JRS 11/25/03 *********************
                                        '                                    '                    If gGroupTot > 0 Then
                                        '                                    '                        For idx = 1 To gGroupTot
                                        '                                    '                            N(VarPoint(chk) + (gRegionIdx(gCountryGroup(idx)) - 1)) = nvalue
                                        '                                    '                        Next idx
                                        '                                    '                    End If
                                        '                                    '                    '**********************************

                                        '                                    'JRS 11/25/03 *********************
                                        '                                    If gGroupTot > 0 Then
                                        '                                        'jrs 2008/05/05
                                        '                                        bCT = frm_MainMenu.IFsGUIctrl1.get_CommonTarget(CInt(chk))
                                        '                                        If bCT Then
                                        '                                            'Find Target Year, Target Value, FirstYear of change towards target
                                        '                                            'Need copy of rsSenInput filtered by this variable
                                        '                                            FindCommonTargetVarsSens TgYr, TgVal, TgFstYr, Var, rsSenInput.Fields("Dim"),
                                        '                            dbSenInput, CInt(SensitivityNumber), "", ""
                                        '                            'Retrieve N for TgFstYr (No)
                                        '                                            'there might be different TgFstYrs per parameter
                                        '                                            If iYear >= TgFstYr And iYear < TgYr Then
                                        '                                                ReDim No(gGroupTot) As Single
                                        '                                For eleloop = 1 To gGroupTot
                                        '                                                    frm_MainMenu.IFsGUIctrl1.sLoadVarSingle Var, gCountryGroup(eleloop),
                                        '                                    "", "", 1, eleloop = 1, eleloop = gGroupTot, False, 1, TgFstYr - 1, eleloop = 1
                                        '                                    No(eleloop) = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(CInt(eleloop), 1) 'jrs 2008/05/05 changed from TgFstYr-1 back to 1
                                        '                                                Next eleloop
                                        '                                            End If

                                        '                                            For idx = 1 To gGroupTot
                                        '                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx))
                                        '                                                RegIdx = AMAX(frm_MainMenu.IFsGUIctrl1.getDimIdx(1, gCountryGroup(idx), False), 0)
                                        '                                                'if Year is less than Target Year
                                        '                                                If iYear < TgYr Then
                                        '                                                    'Year is already equal or greater than when the change starts
                                        '                                                    If iYear >= TgFstYr Then
                                        '                                                        'No is the value of N in year TgFstYr for Region RegIdx

                                        '                                                        'Common Target logic
                                        '                                                        N(VarPoint(chk) + RegIdx - 1) = No(idx) + (TgVal - No(idx)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                        '                                                    End If
                                        '                                                Else
                                        '                                                    'else just assign group value to all members (converged already)
                                        '                                                    N(VarPoint(chk) + RegIdx - 1) = nvalue
                                        '                                                End If
                                        '                                            Next idx
                                        '                                        Else
                                        '                                            Dim myValue3 As Single
                                        '                                            Dim factor3 As Single
                                        '                                            myValue3 = getGroupValue(chk, N)
                                        '                                            'jrs 2005/05/02
                                        '                                            Dim sAgg3 As String
                                        '                                            sAgg3 = frm_MainMenu.IFsGUIctrl1.get_Aggregation(CInt(chk))

                                        '                                            If sAgg3 = "SUM" Then
                                        '                                                If myValue3 = 0 Then
                                        '                                                    factor3 = nvalue / gGroupTot 'jrs 2006/05/31 0
                                        '                                                Else
                                        '                                                    factor3 = nvalue / myValue3
                                        '                                                End If
                                        '                                            Else
                                        '                                                factor3 = nvalue - myValue3
                                        '                                            End If
                                        '                                            'If factor3 <> 0 Then
                                        '                                            For idx = 1 To gGroupTot
                                        '                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx))
                                        '                                                RegIdx = AMAX(frm_MainMenu.IFsGUIctrl1.getDimIdx(1, gCountryGroup(idx), False), 0)
                                        '                                                If sAgg3 = "SUM" Then
                                        '                                                    N(VarPoint(chk) + RegIdx - 1) = N(VarPoint(chk) + RegIdx - 1) * factor3
                                        '                                                Else
                                        '                                                    N(VarPoint(chk) + RegIdx - 1) = N(VarPoint(chk) + RegIdx - 1) + factor3
                                        '                                                End If
                                        '                                            Next idx
                                        '                                            'End If
                                        '                                        End If 'if bct
                                        '                                    End If
                                        '                                    '**********************************

                                        '                                    rsSenInput.MoveNext
                                        '                                    If rsSenInput.EOF Then
                                        '                                        Do While Not EOF(1)
                                        '                                            Line Input #1, VLine$
                                        '                        Loop
                                        '                                    End If
                                        '                                End If

                                        Dim1Max = Dim1Count + 1  'to handle "All" regions
                                        If P1Val = Dim1Max Then
                                            For ICount% = 1 To Dim1Max - 1
                                                dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(ICount), Dim1Count)
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Next ICount%
                                        Else
                                            If gGroupTot = 0 Then
                                                dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count)
                                                IFsResults(Var)(dfNidx) = nvalue
                                                'jrs 2014/03/07 loop through all other Dimensions
                                                If P1Val = 1 And StringHold = "Total" Then
                                                    For dim2idx = 2 To Dim1Max - 1
                                                        dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(dim2idx), Dim1Count)
                                                        IFsResults(Var)(dfNidx) = nvalue
                                                    Next dim2idx
                                                End If
                                            End If
                                            'JRS 09/04/03
                                            '                  'JRS 12/03/01 GROUPING
                                            '                  For idx = 2 To gGroupTot
                                            '                    'N(VarPoint(chk) + (gRegionIdx(gCountryGroup(idx)) - 1)) = GroupVals(idx)
                                            '                    N(VarPoint(chk) + (gRegionIdx(gCountryGroup(idx)) - 1)) = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(idx + 1, IYear)
                                            '                  Next idx
                                            gGroupTot = 0
                                        End If

                                        'N(VarPoint(chk) + (p1 - 1)) = nvalue

                                    End If

                                ElseIf currVar.Dimensions.Count = 3 Then 'routine for double dim. vars.
                                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                                    Maximum = Dim1Count

                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                    'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    '   Input #1, P1, P2  'Either could be string pointer or numerical pointer to
                                    P1 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1
                                    P2 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1

                                    'jrs 2021/04/13
                                    If P1 = "Congo" And P2 = "Dem. Republic of the" Then
                                        P1 = "Congo, Dem. Republic of the"
                                        P2 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1
                                    End If

                                    '   'element number; if numerical, ok as is
                                    'Else
                                    '   P1 = rsSenInput.Fields("Dim")
                                    '   P2 = rsSenInput.Fields("Dim2")
                                    'End If

                                    If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical                                        
                                        'jrs 2022/07/06
                                        'VLine = VLine.Replace("""" + P1 + """,", "")
                                        'VLine = VLine.Replace(P1 + ",", "")
                                        VLine = ReplaceFirst(VLine, """" + P1 + """,", "")
                                        VLine = ReplaceFirst(VLine, P1 + ",", "")
                                        StringHold1$ = P1
                                        For eleloop = 1 To Dim1Count 'Up to number of elements on dimension
                                            'jrs 2014/08/20 *** begin ***
                                            If StringHold1$ = "Total" And GRPS(currVar.Dimensions(1), Dim1Count) <> "Total" Then
                                                P1 = "1"
                                                P1Val = 1
                                                Exit For
                                            End If
                                            'jrs 2014/08/20 *** end ***

                                            If StringHold1$.ToUpper = GRPS$(currVar.Dimensions(1), eleloop).ToUpper Then
                                                P1 = eleloop.ToString
                                                P1Val = eleloop
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            If GRPS(currVar.Dimensions(1), eleloop).Contains(",") Then
                                                If StringHold1$.ToUpper = GRPS(currVar.Dimensions(1), eleloop).Substring(0, GRPS(currVar.Dimensions(1), eleloop).IndexOf(",")).ToUpper Then
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    'jrs 2020/07/03 VLine = VLine.Substring(P1.Length + 1)
                                                    StringHold1 = StringHold1 + ", " + P2
                                                    If StringHold1$.ToUpper = GRPS$(currVar.Dimensions(1), eleloop).ToUpper Then
                                                        'jrs 2020/07/03
                                                        VLine = VLine.Replace(P2 + ",", "")
                                                        'jrs 2022/05/25
                                                        VLine = VLine.Replace(""" " + P2 + """,", "")

                                                        P2 = P1
                                                        P1 = eleloop.ToString
                                                        P1Val = eleloop
                                                        VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                        Exit For
                                                    Else
                                                        StringHold1 = StringHold1.Replace(", " + P2, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= Dim1Count Then 'Not Found
                                                'JRS 12/08/01 GROUPING
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then
                                                    'jrs 2014/11/07 P1 = gRegionIdx(gCountryGroup(1))
                                                    P1Val = gCountryGroupIdx(1)
                                                    P1 = P1Val.ToString
                                                Else 'jrs 2010/10/12, maybe the group has a comma
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    VLine = VLine.Substring(P1.Length + 1)
                                                    StringHold1$ = StringHold1$ & ", " & P2
                                                    P2 = P1
                                                    gLoadCountries(StringHold1$)
                                                    If gGroupTot > 0 Then
                                                        P1Val = gCountryGroupIdx(1)
                                                        P1 = P1Val.ToString
                                                    Else
                                                        P1 = StringHold1$
                                                    End If
                                                End If
                                                If Not Integer.TryParse(P1, P1Val) Then
                                                    msg$ = StringHold1$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                    '                                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                                    '                                                    Line Input #1, VLine$ 'read rest of line
                                                    '                                                Else
                                                    '                                                    rsSenInput.MoveNext
                                                    '                                                    If rsSenInput.EOF Then
                                                    '                                                        Do While Not EOF(1)
                                                    '                                                            Line Input #1, VLine$
                                                    '                                        Loop
                                                    '                                                    End If
                                                    '                                                End If
                                                    '                                                GoTo ReadAnother
                                                End If

                                            End If
                                        Next eleloop
                                    Else
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P1.Length + 1)
                                    End If
                                    DimMax = Dim2Count + 1
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P2.Length + 1)
                                    If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                        StringHold2$ = P2
                                        For eleloop = 1 To Dim2Count 'Up to number of elements on dimension
                                            'jrs 2014/03/07
                                            If StringHold2$ = "Total" And GRPS(currVar.Dimensions(2), Dim2Count) <> "Total" Then
                                                P2Val = 1
                                                P2 = "1"
                                                Exit For
                                            End If

                                            If StringHold2$.ToUpper = GRPS$(currVar.Dimensions(2), eleloop).ToUpper Then
                                                P2Val = eleloop
                                                P2 = P2Val.ToString
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            DimNo = currVar.Dimensions(2)
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P2 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    StringHold2 = StringHold2 + ", " + P2
                                                    If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P2Val = eleloop
                                                        P2 = P2Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                        Exit For
                                                    Else
                                                        StringHold2 = StringHold2.Replace(", " + P2, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If
                                            'jrs 2022/07/06
                                            'If eleloop >= DimMax Then 'Not Found
                                            If eleloop >= Dim2Count Then 'Not Found
                                                'jrs 2005/06/11
                                                gLoadCountries(StringHold2$)
                                                If gGroupTot > 0 Then
                                                    P2Val = gCountryGroupIdx(1)
                                                    P2 = P2Val.ToString
                                                Else
                                                    msg$ = StringHold2$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If

                                                    '                                                'JRS 09/06/02 Close #1
                                                    '                                                'JRS 09/06/02 Close #2
                                                    '                                                'JRS 06/09/03 RAND PARDEE IF END IF
                                                    '                                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                                    '                                                    Line Input #1, VLine$ 'read rest of line
                                                    '                                                Else
                                                    '                                                    rsSenInput.MoveNext
                                                    '                                                    If rsSenInput.EOF Then
                                                    '                                                        Do While Not EOF(1)
                                                    '                                                            Line Input #1, VLine$
                                                    '                                        Loop
                                                    '                                                    End If
                                                    '                                                End If
                                                    '                                                GoTo ReadAnother
                                                End If
                                            End If
                                        Next eleloop
                                        'JRS 09/04/03
                                    Else
                                        P2Val = CInt(P2)
                                        StringHold2$ = GRPS(currVar.Dimensions(2), P2Val)  'subdim name
                                    End If

                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                    'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    '   Line Input #1, VLine$
                                    If X(xIdx).Replace("""", "").Contains(":") Then
                                        nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                    Else
                                        nvalue = CSng(X(xIdx).Replace("""", ""))
                                    End If
                                    'jrs 2008/06/03
                                    VLine2 = VLine
                                    'mti 2019/05/14 Start
                                    If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year
                                        'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then 'time series plus advanced year
                                        'should go in even in the first year when there is a time offset, e.g., hist rebuild with first year layter than 1960
                                        Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                    End If
                                    '                                Else
                                    '                                    nvalue = rsSenInput.Fields("Value")
                                    '                                    rsSenInput.MoveNext
                                    '                                    If rsSenInput.EOF Then
                                    '                                        Do While Not EOF(1)
                                    '                                            Line Input #1, VLine$
                                    '                        Loop
                                    '                                    End If
                                    '                                End If

                                    'JRS 09/03/03 BEGIN **************************************
                                    If gGroupTot > 0 Then
                                        bCT = If(currVar.CommonTarget, False)
                                        If bCT Then
                                            'jrs 2008/06/03
                                            'Currency Handling
                                            CurrFact = 1
                                            If currVar.Currency.ToUpper = "TRUE" Then
                                                CurrFact = CurrencyVal
                                            End If

                                            'Find Target Year, Target Value, FirstYear of change towards target
                                            'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                            'Need entire VLine before parsing
                                            FindCommonTargetVars(TgYr, TgVal, TgFstYr, VLine2)
                                            'Else
                                            '                                            rsSenInput.MovePrevious
                                            '                                            'Need copy of rsSenInput filtered by this variable
                                            '                                            FindCommonTargetVarsSens TgYr, TgVal, TgFstYr, Var,
                                            '                            rsSenInput.Fields("Dim"), dbSenInput, CInt(SensitivityNumber),
                                            '                            rsSenInput.Fields("Dim2"), ""
                                            '                            rsSenInput.MoveNext
                                            'End If
                                            'Retrieve N for TgFstYr (No)
                                            'there might be different TgFstYrs per parameter
                                            If iYear >= TgFstYr And iYear < TgYr Then
                                                'jrs 2019/12/13
                                                If iYear = TgFstYr AndAlso Not IFsResultsBase.ContainsKey(Var) Then
                                                    IFsResultsBase.Add(Var, BaseFile.LoadDataframe(Var))
                                                End If

                                                ReDim No(gGroupTot)
                                                For eleloop = 1 To gGroupTot
                                                    No(eleloop) = getSingleValue(currVar, CType(IFsResultsBase(Var), IDataframe), CShort(iYear + FirstYear - 1), context, CShort(gCountryGroupIdx(eleloop)), CShort(P2Val))
                                                Next eleloop
                                            End If
                                        Else
                                            'JRS 09/03/03
                                            'jrs 10/28/04 frm_MainMenu.IFsGUIctrl1.sLoadVarSingle Var, 1, True, True, StringHold1$, 0, IYear, StringHold2$, False

                                            If currVar.Dimensions(2) = 1 Then
                                                myValue2_1 = getGroupXGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(iYear + FirstYear - 1), context, dfWeight, StringHold1$, StringHold2$, 0)
                                            Else
                                                'jrs 2020/07/28
                                                If (currVar.Aggregation.ToUpper = "NONE" Or currVar.Aggregation = "") And iYear% = StartYear Then
                                                    Debug.Print("The aggregation rule for " & currVar.Name & " is """ & currVar.Aggregation & """. There could be problems setting parameters for groups.")
                                                End If

                                                'jrs 10/28/04
                                                myValue2_1 = getGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(iYear + FirstYear - 1), context, dfWeight, CShort(P2Val))
                                            End If
                                            'Else
                                            'myValue2_1 = getGroupValueBilateral(chk, StringHold1$, StringHold2$, CInt(P3), dbG, rsDef, rsDet, N, iYear)
                                            'End If
                                        End If 'eof if bCT else


                                        'JRS 08/11/03 begin
                                        If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                            'mti 2019/05/14 Start
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                            'mti 2019/05/14 End
                                        End If

                                        Dim CtryIdx2 As Integer
                                        'jrs 2008/05/05
                                        If bCT Then
                                            For CtryIdx2 = 1 To gGroupTot
                                                RegIdx = gCountryGroupIdx(CtryIdx2)
                                                dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count)
                                                'if Year is less than Target Year
                                                If iYear < TgYr Then
                                                    'Year is already equal or greater than when the change starts
                                                    If iYear >= TgFstYr Then
                                                        'No is the value of N in year TgFstYr for Region RegIdx

                                                        'Common Target logic
                                                        IFsResults(Var)(dfNidx) = No(CtryIdx2) + (TgVal - No(CtryIdx2)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                                    End If
                                                Else
                                                    'else just assign group value to all members (converged already)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                End If
                                            Next CtryIdx2

                                        Else
                                            'jrs 2005/04/29
                                            'jrs 2018/01/06
                                            sAgg2_1 = currVar.Aggregation

                                            If sAgg2_1 = "SUM" Then
                                                If myValue2_1 = 0 Then
                                                    factor2_1 = nvalue / gGroupTot 'jrs 2006/05/31 related to error fixed on 'jrs 2006/05/04
                                                Else
                                                    factor2_1 = nvalue / myValue2_1
                                                End If
                                            Else
                                                'JRS 08/11/03 end
                                                'If myValue2 <> 0 Then
                                                factor2_1 = nvalue - myValue2_1
                                                'Else
                                                '    factor2 = 1
                                                'End If
                                            End If

                                            If P2Val <> DimMax And factor2_1 <> 0 Then 'jrs 2014/03/07 was using Dim2Max, but that doesn't exist
                                                'jrs 2006/12/29
                                                If currVar.Dimensions(2) = 1 Then
                                                    UpdateGroupbyGroup(currVar, IFsResults(Var), CShort(iYear + FirstYear - 1), context, StringHold1$, StringHold2$, 0, factor2_1)
                                                Else
                                                    For CtryIdx2 = 1 To gGroupTot
                                                        'JRS 09/03/03
                                                        'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(ctryIdx2))
                                                        RegIdx = gCountryGroupIdx(CtryIdx2)
                                                        dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(P2Val), Dim2Count)
                                                        'jrs 2005/04/29
                                                        If sAgg2_1 = "SUM" Then
                                                            'jrs 2013/12/06 *** begin ***
                                                            '                                        N(VarPoint(chk) + (RegIdx - 1) + ((P2 - 1) * Maximum)) = _
                                                            '                                        N(VarPoint(chk) + (RegIdx - 1) + ((P2 - 1) * Maximum)) * factor2_1
                                                            If myValue2_1 <> 0 Then
                                                                IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor2_1
                                                            Else
                                                                'jrs 2013/12/06
                                                                'When all values in the group are 0 multiplication doesn't work
                                                                IFsResults(Var)(dfNidx) = factor2_1
                                                            End If
                                                            'jrs 2013/12/06 *** end ***
                                                        Else
                                                            IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1

                                                            'jrs 2014/03/07 loop through all other Dimensions
                                                            If P2Val = 1 And StringHold2 = "Total" Then
                                                                For dim2idx = 2 To DimMax - 1
                                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(dim2idx), Dim2Count)
                                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1
                                                                Next dim2idx
                                                            End If
                                                        End If
                                                    Next CtryIdx2
                                                End If
                                            End If
                                            'JRS 09/04/03 nvalue = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(2, IYear)
                                            'nvalue = Val(GroupLines(1))
                                        End If 'eof if bCT else
                                    End If 'eof If gGroupTot > 0
                                    'JRS 09/03/03 END ****************************************

                                    Dim1Max = Dim1Count + 1 'to handle "All" in dimensionality
                                    Dim2Max = Dim2Count + 1
                                    '"ALL" is not Allowed in Both Subdims
                                    'at the same time
                                    If P1Val = Dim1Max Then
                                        For ICount% = 1 To Dim1Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(ICount%), Dim1Count, CShort(P2Val), Dim2Count)
                                            IFsResults(Var)(dfNidx) = nvalue
                                        Next ICount%
                                    ElseIf P2Val = Dim2Max Then
                                        For ICount% = 1 To Dim2Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(ICount%), Dim2Count)
                                            If gGroupTot = 0 Then
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Else
                                                'jrs 2005/05/02
                                                If sAgg2_1 = "SUM" Then
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor2_1
                                                Else
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1
                                                End If
                                            End If
                                        Next ICount%
                                        'JRS 03/18/03 GROUPS AND ALL
                                        Dim idx3 As Integer
                                        If factor2_1 <> 0 Then
                                            For idx3 = 2 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx3))
                                                RegIdx = gCountryGroupIdx(idx3)
                                                For ICount% = 1 To Dim2Max - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(ICount%), Dim2Count)
                                                    'jrs 2005/05/02
                                                    If sAgg2_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor2_1
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1
                                                    End If
                                                Next ICount%
                                            Next idx3
                                        End If
                                    Else
                                        'JRS 09/04/03
                                        If gGroupTot = 0 Then
                                            'jrs 2018/01/06
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(P2Val), Dim2Count)
                                            IFsResults(Var)(dfNidx) = nvalue

                                            'jrs 2014/03/07 loop through all other Dimensions
                                            If P2Val = 1 And StringHold2 = "Total" Then
                                                For dim2idx = 2 To DimMax - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(dim2idx), Dim2Count)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                Next dim2idx
                                            End If
                                            'jrs 2014/08/20 loop through all other Dimensions
                                            If P1Val = 1 And StringHold1 = "Total" Then
                                                For ICount = 2 To DimMax - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(ICount), Dim1Count, CShort(P2Val), Dim2Count)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                Next ICount
                                            End If
                                        End If
                                        'JRS 12/06/01 GROUPING

                                        gGroupTot = 0

                                    End If

                                    'N(VarPoint(chk) + (p1 - 1) + ((p2 - 1) * Maximum)) = nvalue

                                ElseIf currVar.Dimensions.Count = 4 Then 'routine for triple dim. vars.
                                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                                    Dim3Count = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)

                                    Maximum = Dim1Count

                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                    '                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    'Input #1, P1, P2, P3  'Either could be string pointer or numerical pointer to
                                    P1 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1
                                    P2 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1
                                    P3 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1
                                    'element number; if numerical, ok as is
                                    '                                Else
                                    '                                    P1 = rsSenInput.Fields("Dim")
                                    '                                    P2 = rsSenInput.Fields("Dim2")
                                    '                                    p3 = rsSenInput.Fields("Dim3")
                                    '                                End If

                                    'jrs 2017/01/06
                                    Dim1Max = Dim1Count + 1
                                    Dim2Max = Dim2Count + 1
                                    Dim3Max = Dim3Count + 1

                                    If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical
                                        'jrs 2021/04/13
                                        If P1 = "Congo" And P2 = "Dem. Republic of the" Then
                                            P1 = "Congo, Dem. Republic of the"
                                            P2 = P3
                                            P3 = X(xIdx).Replace("""", "").Trim
                                            xIdx = xIdx + 1
                                        End If

                                        VLine = VLine.Replace("""" + P1 + """,", "")
                                        VLine = VLine.Replace(P1 + ",", "")

                                        DimMax = Dim1Count + 1
                                        DimNo = currVar.Dimensions(1)
                                        StringHold1$ = P1
                                        For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                            If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                P1Val = eleloop
                                                P1 = P1Val.ToString
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold1$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    VLine = VLine.Substring(P1.Length + 1)
                                                    StringHold1 = StringHold1 + ", " + P2
                                                    If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P1Val = eleloop
                                                        P1 = P1Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                        P2 = P3
                                                        P3 = P1
                                                        Exit For
                                                    Else
                                                        StringHold1 = StringHold1.Replace(", " + P2, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= DimMax Then 'Not Found

                                                'JRS 12/08/01 GROUPING
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then P1Val = gCountryGroupIdx(1) 'jrs 2014/11/07 P1 = gRegionIdx(gCountryGroup(1))
                                                If Not Integer.TryParse(P1, P1Val) Then
                                                    msg$ = StringHold1$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                                    '                                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                                    '                                                    Line Input #1, VLine$ 'read rest of line
                                                    '                                                Else
                                                    '                                                    rsSenInput.MoveNext
                                                    '                                                    If rsSenInput.EOF Then
                                                    '                                                        Do While Not EOF(1)
                                                    '                                                            Line Input #1, VLine$
                                                    '                                        Loop
                                                    '                                                    End If
                                                    '                                                End If
                                                    '                                                GoTo ReadAnother
                                                End If

                                            End If
                                        Next eleloop
                                    Else
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P1.Length + 1)
                                    End If
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P2.Length + 1)
                                    If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                        DimMax = Dim2Count + 1
                                        DimNo = currVar.Dimensions(2)
                                        StringHold2$ = P2
                                        For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                            'jrs 2014/03/07
                                            If StringHold2$ = "Total" And GRPS(DimNo, DimMax - 1) <> "Total" Then
                                                P2Val = 1
                                                P2 = P2Val.ToString
                                                Exit For
                                            End If

                                            If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                P2Val = eleloop
                                                P2 = P2Val.ToString
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P2 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    VLine = VLine.Substring(P2.Length + 1)

                                                    StringHold2 = StringHold2 + ", " + P3
                                                    If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P2Val = eleloop
                                                        P2 = P2Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                        P3 = P2
                                                        Exit For
                                                    Else
                                                        StringHold2 = StringHold2.Replace(", " + P3, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= DimMax Then 'Not Found
                                                'jrs 2005/06/11
                                                gLoadCountries(StringHold2$)
                                                If gGroupTot > 0 Then
                                                    'jrs 2014/11/07 P2 = gRegionIdx(gCountryGroup(1))
                                                    P2Val = gCountryGroupIdx(1)
                                                    P2 = P2Val.ToString
                                                Else
                                                    msg$ = StringHold2$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                                    '                                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                                    '                                                    Line Input #1, VLine$ 'read rest of line
                                                    '                                                Else
                                                    '                                                    rsSenInput.MoveNext
                                                    '                                                    If rsSenInput.EOF Then
                                                    '                                                        Do While Not EOF(1)
                                                    '                                                            Line Input #1, VLine$
                                                    '                                        Loop
                                                    '                                                    End If
                                                    '                                                End If
                                                    '                                                GoTo ReadAnother
                                                End If
                                            End If
                                        Next eleloop
                                        'JRS 09/04/03
                                    Else
                                        StringHold2$ = GRPS(currVar.Dimensions(2), P2Val)
                                    End If
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P3.Length + 1)
                                    If Not Integer.TryParse(P3, P3Val) Then  'is string and must determine numerical
                                        DimMax = Dim3Count + 1
                                        DimNo = currVar.Dimensions(3)
                                        StringHold3$ = P3
                                        For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                            If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                P3Val = eleloop
                                                P3 = P3Val.ToString
                                                Exit For
                                            End If
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold3$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P3 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    StringHold3 = StringHold3 + ", " + P3
                                                    If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P3Val = eleloop
                                                        P3 = P3Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                        Exit For
                                                    Else
                                                        StringHold3 = StringHold3.Replace(", " + P3, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= DimMax Then 'Not Found
                                                'jrs 2005/06/11
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then
                                                    'jrs 2014/11/07 p3 = gRegionIdx(gCountryGroup(1))
                                                    P3Val = gCountryGroupIdx(1)
                                                    P3 = P3Val.ToString
                                                Else
                                                    msg$ = StringHold3$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                                    '                                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                                    '                                                    Line Input #1, VLine$ 'read rest of line
                                                    '                                                Else
                                                    '                                                    rsSenInput.MoveNext
                                                    '                                                    If rsSenInput.EOF Then
                                                    '                                                        Do While Not EOF(1)
                                                    '                                                            Line Input #1, VLine$
                                                    '                                        Loop
                                                    '                                                    End If
                                                    '                                                End If
                                                    '                                                GoTo ReadAnother
                                                End If
                                            End If
                                        Next eleloop
                                        'JRS 09/04/03
                                    Else
                                        StringHold3$ = GRPS(currVar.Dimensions(3), P3Val)
                                    End If

                                    'JRS 06/09/03 RAND PARDEE IF END IF
                                    '                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                    'Line Input #1, VLine$
                                    If X(xIdx).Replace("""", "").Contains(":") Then
                                        nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                    Else
                                        nvalue = CSng(X(xIdx).Replace("""", ""))
                                    End If

                                    'jrs 2008/06/03
                                    VLine2 = VLine

                                    'mti 2019/05/14 Start
                                    If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year
                                        'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then


                                        Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                        'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                        'mti 2019/05/14 End

                                    End If
                                    '                                Else
                                    '                                    nvalue = rsSenInput.Fields("Value")
                                    '                                    rsSenInput.MoveNext
                                    '                                    If rsSenInput.EOF Then
                                    '                                        Do While Not EOF(1)
                                    '                                            Line Input #1, VLine$
                                    '                        Loop
                                    '                                    End If
                                    '                                End If

                                    'JRS 09/03/03 BEGIN **************************************
                                    If gGroupTot > 0 Then
                                        'jrs 2017/01/06
                                        'jrs 2008/05/05
                                        bCT = If(currVar.CommonTarget, False)
                                        '                                    Else
                                        '                                        bCT = False
                                        '                                    End If
                                        If bCT Then
                                            'jrs 2008/06/03
                                            'Currency Handling
                                            CurrFact = 1
                                            If currVar.Currency.ToUpper = "TRUE" Then
                                                CurrFact = CurrencyVal
                                            End If

                                            'Find Target Year, Target Value, FirstYear of change towards target
                                            'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                            'Need entire VLine before parsing
                                            FindCommonTargetVars(TgYr, TgVal, TgFstYr, VLine2)
                                            'Else
                                            '                                            rsSenInput.MovePrevious
                                            '                                            'Need copy of rsSenInput filtered by this variable
                                            '                                            FindCommonTargetVarsSens TgYr, TgVal, TgFstYr, Var,
                                            '                            rsSenInput.Fields("Dim"), dbSenInput, CInt(SensitivityNumber),
                                            '                            rsSenInput.Fields("Dim2"), rsSenInput.Fields("Dim3")
                                            '                            rsSenInput.MoveNext
                                            'End If

                                            'Retrieve N for TgFstYr (No)
                                            'there might be different TgFstYrs per parameter
                                            If iYear >= TgFstYr And iYear < TgYr Then
                                                'jrs 2019/12/13
                                                If iYear = TgFstYr AndAlso Not IFsResultsBase.ContainsKey(Var) Then
                                                    IFsResultsBase.Add(Var, BaseFile.LoadDataframe(Var))
                                                End If

                                                ReDim No(gGroupTot)
                                                For eleloop = 1 To gGroupTot
                                                    No(eleloop) = getSingleValue(currVar, CType(IFsResultsBase(Var), IDataframe), CShort(iYear + FirstYear - 1), context, CShort(gCountryGroupIdx(eleloop)))
                                                Next eleloop
                                            End If
                                        Else
                                            'JRS 09/03/03
                                            'jrs 10/28/04 frm_MainMenu.IFsGUIctrl1.sLoadVarSingle Var, 1, True, True, StringHold1$, 0, IYear, StringHold2$, False

                                            'jrs 2017/01/06
                                            'jrs 10/28/04 myValue2 = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(1, IYear)
                                            'jrs 10/28/04

                                            'jrs 2020/07/28
                                            If (currVar.Aggregation.ToUpper = "NONE" Or currVar.Aggregation = "") And iYear% = StartYear Then
                                                Debug.Print("The aggregation rule for " & currVar.Name & " is """ & currVar.Aggregation & """. There could be problems setting parameters for groups.")
                                            End If

                                            myValue3_1 = getGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(iYear + FirstYear - 1), context, dfWeight, CShort(P2Val), CShort(P3Val))       'JRS 08/11/03 begin
                                            '                                        Else
                                            '                                            myValue3_1 = getGroupValueBilateral(chk, StringHold1$, StringHold2$, CInt(p3), dbG, rsDef, rsDet, N, iYear)
                                            '                                        End If
                                        End If

                                        'JRS 08/11/03 begin
                                        If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                            'mti 2019/05/14 Start
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                            'mti 2019/05/14 End
                                        End If

                                        Dim ctryIdx3 As Integer
                                        'jrs 2008/05/05
                                        If bCT Then
                                            For ctryIdx3 = 1 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(ctryIdx3))
                                                RegIdx = gCountryGroupIdx(ctryIdx3)
                                                dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, 1, Dim2Count, 1, Dim3Count)
                                                'if Year is less than Target Year
                                                If iYear < TgYr Then
                                                    'Year is already equal or greater than when the change starts
                                                    If iYear >= TgFstYr Then
                                                        'No is the value of N in year TgFstYr for Region RegIdx

                                                        'Common Target logic
                                                        IFsResults(Var)(dfNidx) = No(ctryIdx3) + (TgVal - No(ctryIdx3)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                                    End If
                                                Else
                                                    'else just assign group value to all members (converged already)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                End If
                                            Next ctryIdx3

                                        Else
                                            'jrs 2017/01/06
                                            sAgg3_1 = currVar.Aggregation
                                            '                                        Else
                                            '                                            sAgg3_1 = "GDP"
                                            '                                        End If

                                            If sAgg3_1 = "SUM" Then
                                                If myValue3_1 = 0 Then
                                                    factor3_1 = nvalue / gGroupTot 'jrs 2006/05/31 related to error fixed on 'jrs 2006/05/04
                                                Else
                                                    factor3_1 = nvalue / myValue3_1
                                                End If
                                            Else
                                                factor3_1 = nvalue - myValue3_1
                                            End If

                                            If P2Val <> Dim2Max And factor3_1 <> 0 Then
                                                'jrs 2017/01/06
                                                For ctryIdx3 = 1 To gGroupTot
                                                    'JRS 09/03/03
                                                    'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(ctryIdx3))
                                                    RegIdx = gCountryGroupIdx(ctryIdx3)
                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(P2Val), Dim2Count, CShort(P3Val), Dim3Count)
                                                    'jrs 2005/04/29
                                                    If sAgg3_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                        'jrs 2014/08/04 loop through all other Dimensions
                                                        If P2Val = 1 And StringHold2 = "Total" Then
                                                            For dim2idx = 2 To DimMax - 1
                                                                IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                            Next dim2idx
                                                        End If
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                        'jrs 2014/08/04 loop through all other Dimensions
                                                        If P2Val = 1 And StringHold2 = "Total" Then
                                                            For dim2idx = 2 To DimMax - 1
                                                                dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(dim2idx), Dim2Count, CShort(P3Val), Dim3Count)
                                                                IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                            Next dim2idx
                                                        End If
                                                    End If
                                                Next ctryIdx3
                                                '                                            Else
                                                '                                                'jrs 2017/01/06
                                                '                                                'Save values in group members
                                                '                                                If iYear > 1 And nvalue <> prevvalue Then
                                                '                                                    setGroupValueBilateral chk, StringHold1$, StringHold2$, CInt(p3), dbG, rsDef, rsDet, N, rsBT, factor3_1, iYear
                                                '                                End If
                                                '                                            End If
                                            End If
                                            'JRS 09/04/03 nvalue = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(2, IYear)
                                            'nvalue = Val(GroupLines(1))
                                        End If 'eof if bCT then else
                                    End If
                                    'JRS 09/03/03 END ****************************************
                                    'jrs 2019/10/01 BEGIN TRANSLATION

                                    Dim1Max = Dim1Count + 1  'to handle "All" in dimensionality
                                    Dim2Max = Dim2Count + 1
                                    Dim3Max = Dim3Count + 1
                                    '"ALL" is not Allowed in Both Subdims
                                    'at the same time
                                    If P1Val = Dim1Max Then
                                        For ICount% = 1 To Dim1Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(ICount), Dim1Count, CShort(P2Val), Dim2Count, CShort(P3Val), Dim3Count)
                                            IFsResults(Var)(dfNidx) = nvalue
                                        Next ICount%

                                    ElseIf P2Val = Dim2Max Then
                                        For ICount% = 1 To Dim2Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(ICount), Dim2Count, CShort(P3Val), Dim3Count)
                                            If gGroupTot = 0 Then
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Else
                                                'jrs 2005/05/02
                                                If sAgg3_1 = "SUM" Then
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                Else
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                End If
                                            End If
                                        Next ICount%
                                        'JRS 03/18/03 GROUPS AND ALL
                                        Dim idx3_1 As Integer
                                        If factor3_1 <> 0 Then
                                            For idx3_1 = 2 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx3_1))
                                                RegIdx = gCountryGroupIdx(idx3_1)
                                                For ICount% = 1 To Dim2Max - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(ICount), Dim2Count, CShort(P3Val), Dim3Count)
                                                    'jrs 2005/05/02
                                                    If sAgg3_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                    End If
                                                Next ICount%
                                            Next idx3_1
                                        End If
                                    ElseIf P3Val = Dim3Max Then
                                        For ICount% = 1 To Dim3Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(P2Val), Dim2Count, CShort(ICount), Dim3Count)
                                            If gGroupTot = 0 Then
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Else
                                                'jrs 2005/05/02
                                                If sAgg3_1 = "SUM" Then
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                Else
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                End If
                                            End If
                                        Next ICount%
                                        'JRS 03/18/03 GROUPS AND ALL
                                        Dim idx3_2 As Integer
                                        If factor3_1 <> 0 Then
                                            For idx3_2 = 2 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx3_2))
                                                RegIdx = gCountryGroupIdx(idx3_2)
                                                For ICount% = 1 To Dim3Max - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(P2Val), Dim2Count, CShort(ICount), Dim3Count)
                                                    'jrs 2005/05/02
                                                    If sAgg3_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                    End If
                                                Next ICount%
                                            Next idx3_2
                                        End If
                                    Else
                                        If gGroupTot = 0 Then
                                            'jrs 2017/01/06
                                            dfNidx = getDataFrameIdxFromDims(CShort(iYear + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(P2Val), Dim2Count, CShort(P3Val), Dim3Count)
                                            IFsResults(Var)(dfNidx) = nvalue
                                            '                                        Else 'Save bilateral trade parameters
                                            '                                            If iYear > 1 And nvalue <> prevvalue Then
                                            '                                                With rsBT
                                            '                                                    .AddNew
                                            '                                                    .Fields("ParameterName") = V(chk)
                                            '                                                    .Fields("Actor") = P1
                                            '                                                    .Fields("Partner") = P2
                                            '                                                    .Fields("Sector") = p3
                                            '                                                    .Fields("Year") = iYear + FirstYear - 1
                                            '                                                    .Fields("ParameterValue") = nvalue
                                            '                                                    .Update
                                            '                                                End With
                                            '                                            End If
                                            '                                        End If
                                        End If
                                        gGroupTot = 0
                                    End If

                                End If
                                'ReadAnother:    'Jump here if find unintelligible subscript (e.g. from another load)
                                LastVar$ = Var$
                                'jrs 2005/06/23
                                'Else 'ElseIf chk = 0 And frm_MainMenu.IFsGUIctrl1.cSensitivityFlag = False Then
                                'VLine = reader.ReadLine
                            Else 'jrs 2019/12/13 put the Dyadic logic in here
                                If ifsVarDyadic.ContainsKey(Var$) Then
                                    Dim pos As Integer
                                    pos = VLine.IndexOf(Var$)
                                    VLine = VLine.Substring(pos)
                                    VLine = VLine.Replace(Var$ + ",", "")
                                    VLine = VLine.Replace("CUSTOM,", "")
                                    VLine = VLine.Replace("MEDIUM,", "")
                                    VLine = VLine.Replace("HIGH0010,", "")
                                    VLine = VLine.Replace("LOW0010,", "")
                                    VLine = VLine.Replace("LOW0020,", "")

                                    'if Variable only has 2 dimension
                                    If Not ifsVarDyadicDim3.ContainsKey(Var$) Then
                                        Dim1Count = CShort(NR)
                                        Dim2Count = CShort(NR)
                                        Maximum = Dim1Count

                                        P1 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1
                                        P2 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1

                                        If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical
                                            'jrs 2022/07/06
                                            'VLine = VLine.Replace("""" + P1 + """,", "")
                                            'jrs 2022/02/04
                                            'VLine = VLine.Replace(P1 + ",", "")
                                            VLine = ReplaceFirst(VLine, """" + P1 + """,", "")
                                            VLine = ReplaceFirst(VLine, P1 + ",", "")

                                            StringHold1$ = P1
                                            For eleloop = 1 To Dim1Count 'Up to number of elements on dimension
                                                If StringHold1$ = "Total" And GRPS(1, Dim1Count) <> "Total" Then
                                                    P1 = "1"
                                                    P1Val = 1
                                                    Exit For
                                                End If

                                                If StringHold1$.ToUpper = GRPS$(1, eleloop).ToUpper Then
                                                    P1 = eleloop.ToString
                                                    P1Val = eleloop
                                                    Exit For
                                                End If
                                                If GRPS(1, eleloop).Contains(",") Then
                                                    If StringHold1$.ToUpper = GRPS(1, eleloop).Substring(0, GRPS(1, eleloop).IndexOf(",")).ToUpper Then
                                                        P1 = X(xIdx).Replace("""", "").Trim
                                                        xIdx = xIdx + 1
                                                        'Remove blanks jrs 2020/01/10
                                                        Do While VLine.Substring(0, 1) = " "
                                                            VLine = VLine.Substring(1)
                                                        Loop
                                                        VLine = VLine.Substring(P1.Length + 1)
                                                        StringHold1 = StringHold1 + ", " + P2
                                                        If StringHold1$.ToUpper = GRPS$(1, eleloop).ToUpper Then
                                                            P2 = P1
                                                            P1 = eleloop.ToString
                                                            P1Val = eleloop
                                                            VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                            Exit For
                                                        Else
                                                            StringHold1 = StringHold1.Replace(", " + P2, "")
                                                            xIdx = xIdx - 1
                                                        End If
                                                    End If
                                                End If

                                                If eleloop >= Dim1Count Then 'Not Found
                                                    gLoadCountries(StringHold1$)
                                                    If gGroupTot > 0 Then
                                                        P1Val = gCountryGroupIdx(1)
                                                        P1 = P1Val.ToString
                                                    Else 'jrs 2010/10/12, maybe the group has a comma
                                                        P1 = X(xIdx).Replace("""", "").Trim
                                                        xIdx = xIdx + 1
                                                        'Remove blanks jrs 2020/01/10
                                                        Do While VLine.Substring(0, 1) = " "
                                                            VLine = VLine.Substring(1)
                                                        Loop
                                                        VLine = VLine.Substring(P1.Length + 1)
                                                        StringHold1$ = StringHold1$ & ", " & P2
                                                        P2 = P1
                                                        gLoadCountries(StringHold1$)
                                                        If gGroupTot > 0 Then
                                                            P1Val = gCountryGroupIdx(1)
                                                            P1 = P1Val.ToString
                                                        Else
                                                            P1 = StringHold1$
                                                        End If
                                                    End If
                                                    If Not Integer.TryParse(P1, P1Val) Then
                                                        msg$ = StringHold1$ + " not found among valid dimensions."
                                                        msg$ = msg$ + "  Will skip .dat entry."
                                                        If bShowMsg Then
                                                            Debug.Print(msg$)
                                                        End If
                                                    End If

                                                End If
                                            Next eleloop
                                        Else
                                            'Remove blanks jrs 2020/01/10
                                            Do While VLine.Substring(0, 1) = " "
                                                VLine = VLine.Substring(1)
                                            Loop
                                            VLine = VLine.Substring(P1.Length + 1)
                                        End If
                                        DimMax = Dim2Count + 1
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P2.Length + 1)
                                        If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                            StringHold2$ = P2
                                            For eleloop = 1 To Dim2Count 'Up to number of elements on dimension
                                                If StringHold2$ = "Total" And GRPS(1, Dim2Count) <> "Total" Then
                                                    P2Val = 1
                                                    P2 = "1"
                                                    Exit For
                                                End If

                                                If StringHold2$.ToUpper = GRPS$(1, eleloop).ToUpper Then
                                                    P2Val = eleloop
                                                    P2 = P2Val.ToString
                                                    Exit For
                                                End If
                                                DimNo = 1
                                                If GRPS(DimNo, eleloop).Contains(",") Then
                                                    If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                        P2 = X(xIdx).Replace("""", "").Trim
                                                        xIdx = xIdx + 1
                                                        StringHold2 = StringHold2 + ", " + P2
                                                        If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                            P2Val = eleloop
                                                            P2 = P2Val.ToString
                                                            VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                            Exit For
                                                        Else
                                                            StringHold2 = StringHold2.Replace(", " + P2, "")
                                                            xIdx = xIdx - 1
                                                        End If
                                                    End If
                                                End If
                                                If eleloop >= DimMax Then 'Not Found
                                                    gLoadCountries(StringHold2$)
                                                    If gGroupTot > 0 Then
                                                        P2Val = gCountryGroupIdx(1)
                                                        P2 = P2Val.ToString
                                                    Else
                                                        msg$ = StringHold2$ + " not found among valid dimensions."
                                                        msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                        If bShowMsg Then
                                                            Debug.Print(msg$)
                                                        End If
                                                    End If
                                                End If
                                            Next eleloop
                                            'JRS 09/04/03
                                        Else
                                            P2Val = CInt(P2)
                                            StringHold2$ = GRPS(1, P2Val)  'subdim name
                                        End If

                                        If X(xIdx).Replace("""", "").Contains(":") Then
                                            nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                        Else
                                            nvalue = CSng(X(xIdx).Replace("""", ""))
                                        End If
                                        VLine2 = VLine
                                        If iYear% > 1 Then  'time series plus advanced year
                                            'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then 'time series plus advanced year
                                            'should go in even in the first year when there is a time offset, e.g., hist rebuild with first year layter than 1960
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            VLine = VLine2
                                            If iYear = 2 Then
                                                prevvalue = -9999
                                            Else
                                                Call ParseLine(VLine$, iYear% - 1, RTime, prevvalue)
                                            End If
                                        End If

                                        'JRS 09/03/03 BEGIN **************************************
                                        If gGroupTot > 0 Then

                                            myValue2_1 = ifsVarDyadic(Var)

                                            If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            End If

											'gvdb 2024/04/05
            								' unused variable
                                            'Dim CtryIdx2 As Integer
                                            'jrs 2008/05/05

                                            'jrs 2005/04/29
                                            'jrs 2018/01/06
                                            sAgg2_1 = "GDP"

                                            factor2_1 = nvalue - myValue2_1

                                            If P2Val <> DimMax And factor2_1 <> 0 Then 'jrs 2014/03/07 was using Dim2Max, but that doesn't exist
                                                'Save values in group members
                                                If iYear > 1 And nvalue <> prevvalue Then
                                                    setGroupValueBilateral(Var, StringHold1$, StringHold2$, 0, factor2_1, iYear, context, ifsVarDyadic(Var))
                                                End If
                                            End If
                                            'JRS 09/04/03 nvalue = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(2, IYear)
                                            'nvalue = Val(GroupLines(1))

                                        End If 'eof If gGroupTot > 0
                                        'JRS 09/03/03 END ****************************************

                                        Dim1Max = Dim1Count + 1 'to handle "All" in dimensionality
                                        Dim2Max = Dim2Count + 1

                                        'JRS 09/04/03
                                        If gGroupTot = 0 Then

                                            'Save bilateral trade parameters
                                            If iYear > 1 And nvalue <> prevvalue Then
                                                Using db As IFsForDyadicContext = New IFsForDyadicContext()
                                                    Dim conn = db.Database.GetDbConnection
                                                    If conn.State = 0 Then 'closed
                                                        conn.Open()
                                                    End If

                                                    'jrs 2019/11/18 *** begin ***
                                                    Using cmd2 = conn.CreateCommand()
                                                        cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                                                        PRAGMA ignore_check_constraints = ON;
                                                        PRAGMA journal_mode = MEMORY;
                                                        PRAGMA synchronous = OFF;"
                                                        cmd2.ExecuteNonQuery()
                                                    End Using
                                                    db.Database.AutoTransactionsEnabled = False
                                                    db.ChangeTracker.AutoDetectChangesEnabled = False
                                                    db.ChangeTracker.LazyLoadingEnabled = False
                                                    'jrs 2019/11/18 *** end ***

                                                    db.SingleInsertParametersDetail(nvalue, P1Val, P2Val, iYear + FirstYear - 1, Var, 0)
                                                End Using
                                            End If
                                        End If
                                        'JRS 12/06/01 GROUPING

                                        gGroupTot = 0


                                    Else 'if Variable has 3 dimensions 2019/12/17

                                        Dim1Count = CShort(NR)
                                        Dim2Count = CShort(NR)
                                        Dim3Count = CShort(context.GetDimension(ifsVarDyadicDim3(Var)).Buckets.Count)

                                        Maximum = Dim1Count

                                        'JRS 06/09/03 RAND PARDEE IF END IF
                                        '                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                        'Input #1, P1, P2, P3  'Either could be string pointer or numerical pointer to
                                        P1 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1
                                        P2 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1
                                        P3 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1
                                        'element number; if numerical, ok as is
                                        '                                Else
                                        '                                    P1 = rsSenInput.Fields("Dim")
                                        '                                    P2 = rsSenInput.Fields("Dim2")
                                        '                                    p3 = rsSenInput.Fields("Dim3")
                                        '                                End If

                                        'jrs 2017/01/06
                                        Dim1Max = Dim1Count + 1
                                        Dim2Max = Dim2Count + 1
                                        Dim3Max = Dim3Count + 1

                                        Dim eleloop As Integer

                                        If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical
                                            VLine = VLine.Replace("""" + P1 + """,", "")
                                            'no double quotes
                                            VLine = VLine.Replace(P1 + ",", "")

                                            DimMax = Dim1Count + 1
                                            DimNo = 1
                                            StringHold1$ = P1
                                            For eleloop = 1 To Dim1Count 'Up to number of elements on dimension
                                                If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P1Val = eleloop
                                                    P1 = P1Val.ToString
                                                    Exit For
                                                End If
                                                'jrs 2005/06/11
                                                If GRPS(DimNo, eleloop).Contains(",") Then
                                                    If StringHold1$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                        P1 = X(xIdx).Replace("""", "").Trim
                                                        xIdx = xIdx + 1
                                                        'Remove blanks jrs 2020/01/10
                                                        Do While VLine.Substring(0, 1) = " "
                                                            VLine = VLine.Substring(1)
                                                        Loop
                                                        VLine = VLine.Substring(P1.Length + 1)
                                                        StringHold1 = StringHold1 + ", " + P2
                                                        If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                            P1Val = eleloop
                                                            P1 = P1Val.ToString
                                                            VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                            P2 = P3
                                                            P3 = P1
                                                            Exit For
                                                        Else
                                                            StringHold1 = StringHold1.Replace(", " + P2, "")
                                                            xIdx = xIdx - 1
                                                        End If
                                                    End If
                                                End If
                                            Next eleloop

                                            If eleloop >= DimMax Then 'Not Found

                                                'JRS 12/08/01 GROUPING
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then P1Val = gCountryGroupIdx(1) 'jrs 2014/11/07 P1 = gRegionIdx(gCountryGroup(1))
                                                P1 = P1Val.ToString
                                                If Not Integer.TryParse(P1, P1Val) Then
                                                    msg$ = StringHold1$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                End If

                                            End If

                                        Else
                                            'Remove blanks jrs 2020/01/10
                                            Do While VLine.Substring(0, 1) = " "
                                                VLine = VLine.Substring(1)
                                            Loop
                                            VLine = VLine.Substring(P1.Length + 1)
                                        End If
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P2.Length + 1)
                                        If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                            DimMax = Dim2Count + 1
                                            DimNo = 1
                                            StringHold2$ = P2
                                            For eleloop = 1 To Dim2Count 'Up to number of elements on dimension
                                                'jrs 2014/03/07
                                                If StringHold2$ = "Total" And GRPS(DimNo, DimMax - 1) <> "Total" Then
                                                    P2Val = 1
                                                    P2 = P2Val.ToString
                                                    Exit For
                                                End If

                                                If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P2Val = eleloop
                                                    P2 = P2Val.ToString
                                                    Exit For
                                                End If
                                                'jrs 2005/06/11
                                                If GRPS(DimNo, eleloop).Contains(",") Then
                                                    If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                        P2 = X(xIdx).Replace("""", "").Trim
                                                        xIdx = xIdx + 1
                                                        'Remove blanks jrs 2020/01/10
                                                        Do While VLine.Substring(0, 1) = " "
                                                            VLine = VLine.Substring(1)
                                                        Loop
                                                        VLine = VLine.Substring(P2.Length + 1)

                                                        StringHold2 = StringHold2 + ", " + P3
                                                        If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                            P2Val = eleloop
                                                            P2 = P2Val.ToString
                                                            VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                            P3 = P2
                                                            Exit For
                                                        Else
                                                            StringHold2 = StringHold2.Replace(", " + P3, "")
                                                            xIdx = xIdx - 1
                                                        End If
                                                    End If
                                                End If

                                            Next eleloop

                                            'JRS 09/04/03
                                            If eleloop >= DimMax Then 'Not Found
                                                'jrs 2005/06/11
                                                gLoadCountries(StringHold2$)
                                                If gGroupTot > 0 Then
                                                    'jrs 2014/11/07 P2 = gRegionIdx(gCountryGroup(1))
                                                    P2Val = gCountryGroupIdx(1)
                                                    P2 = P2Val.ToString
                                                Else
                                                    msg$ = StringHold2$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                End If
                                            End If

                                        Else
                                            StringHold2$ = GRPS(1, P2Val)
                                        End If
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P3.Length + 1)
                                        If Not Integer.TryParse(P3, P3Val) Then  'is string and must determine numerical
                                            DimMax = Dim3Count + 1
                                            DimNo = ifsVarDyadicDim3(Var)
                                            StringHold3$ = P3
                                            For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                                If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P3Val = eleloop
                                                    P3 = P3Val.ToString
                                                    Exit For
                                                End If
                                                If GRPS(DimNo, eleloop).Contains(",") Then
                                                    If StringHold3$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                        P3 = X(xIdx).Replace("""", "").Trim
                                                        xIdx = xIdx + 1
                                                        StringHold3 = StringHold3 + ", " + P3
                                                        If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                            P3Val = eleloop
                                                            P3 = P3Val.ToString
                                                            VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                            Exit For
                                                        Else
                                                            StringHold3 = StringHold3.Replace(", " + P3, "")
                                                            xIdx = xIdx - 1
                                                        End If
                                                    End If
                                                End If

                                                If eleloop >= DimMax Then 'Not Found
                                                    'jrs 2005/06/11
                                                    gLoadCountries(StringHold1$)
                                                    If gGroupTot > 0 Then
                                                        'jrs 2014/11/07 p3 = gRegionIdx(gCountryGroup(1))
                                                        P3Val = gCountryGroupIdx(1)
                                                        P3 = P3Val.ToString
                                                    Else
                                                        msg$ = StringHold3$ + " not found among valid dimensions."
                                                        msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                        'jrs 2008/04/02
                                                        If bShowMsg Then
                                                            Debug.Print(msg$)
                                                        End If
                                                    End If
                                                End If
                                            Next eleloop
                                            'JRS 09/04/03
                                        Else
                                            StringHold3$ = GRPS(ifsVarDyadicDim3(Var), P3Val)
                                        End If

                                        'JRS 06/09/03 RAND PARDEE IF END IF
                                        '                                If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                        'Line Input #1, VLine$
                                        If X(xIdx).Replace("""", "").Contains(":") Then
                                            nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                        Else
                                            nvalue = CSng(X(xIdx).Replace("""", ""))
                                        End If

                                        'jrs 2008/06/03
                                        VLine2 = VLine

                                        'mti 2019/05/14 Start
                                        If iYear% > 1 Then  'time series plus advanced year
                                            'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then


                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                            'mti 2019/05/14 End

                                            VLine = VLine2
                                            If iYear = 2 Then
                                                prevvalue = -9999
                                            Else
                                                'mti 2019/05/14 Start
                                                Call ParseLine(VLine$, iYear% - 1, RTime, prevvalue)
                                                'Call ParseLine(VLine$, iYear% - 1 + SeriesOffset, RTime, prevvalue)
                                                'mti 2019/05/14 End
                                            End If

                                        End If

                                        'JRS 09/03/03 BEGIN **************************************
                                        If gGroupTot > 0 Then

                                            'jrs 2017/01/06
                                            'jrs 10/28/04 myValue2 = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(1, IYear)
                                            'jrs 10/28/04
                                            myValue3_1 = ifsVarDyadic(Var)
                                            '                                        Else
                                            '                                            myValue3_1 = getGroupValueBilateral(chk, StringHold1$, StringHold2$, CInt(p3), dbG, rsDef, rsDet, N, iYear)
                                            '                                        End If

                                            'JRS 08/11/03 begin
                                            If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                                'mti 2019/05/14 Start
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                                'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                                'mti 2019/05/14 End
                                            End If
											'gvdb 2024/04/05
            								' unused variable
                                            'Dim ctryIdx3 As Integer

                                            'jrs 2017/01/06
                                            sAgg3_1 = "GDP"
                                            '                                        Else
                                            '                                            sAgg3_1 = "GDP"
                                            '                                        End If

                                            factor3_1 = nvalue - myValue3_1

                                            If P2Val <> Dim2Max And factor3_1 <> 0 Then
                                                '                                                'Save values in group members
                                                If iYear > 1 And nvalue <> prevvalue Then
                                                    setGroupValueBilateral(Var, StringHold1$, StringHold2$, P3Val, factor3_1, iYear, context, ifsVarDyadic(Var))
                                                End If
                                            End If
                                            'JRS 09/04/03 nvalue = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(2, IYear)
                                            'nvalue = Val(GroupLines(1))
                                        End If
                                        'JRS 09/03/03 END ****************************************
                                        'jrs 2019/10/01 BEGIN TRANSLATION

                                        Dim1Max = Dim1Count + 1  'to handle "All" in dimensionality
                                        Dim2Max = Dim2Count + 1
                                        Dim3Max = Dim3Count + 1

                                        If gGroupTot = 0 Then
                                            'Save bilateral trade parameters
                                            If iYear > 1 And nvalue <> prevvalue Then
                                                Using db As IFsForDyadicContext = New IFsForDyadicContext
                                                    'jrs 2021/03/01
                                                    Dim conn = db.Database.GetDbConnection
                                                    If conn.State = 0 Then 'closed
                                                        conn.Open()
                                                    End If

                                                    db.SingleInsertParametersDetail(nvalue, P1Val, P2Val, iYear + FirstYear - 1, Var, P3Val)
                                                End Using
                                            End If
                                        End If
                                        gGroupTot = 0

                                    End If

                                End If
                            End If

                            VLine = reader.ReadLine
                        Loop ' Wend

                        '                    N(VarPoint(PosModYear)) = frm_MainMenu.IFsGUIctrl1.cFirstYear + RTime - 1
                        '                    rps2 = rps
                        '                    LoopNumb = filen \ 5000
                        '                    LoopRem = filen Mod 5000
                        '                    If LoopNumb >= 1 Then
                        '                        For LoopCnt% = 1 To LoopNumb
                        '                            LoopCntReal = LoopCnt%
                        '                            NCntAdd = 5000 * (LoopCntReal - 1)
                        '                            For icnt% = 1 To 5000
                        '                                icntm% = icnt% - 1
                        '                                FILEBUF2.elem2(icntm%) = N(icnt% + NCntAdd)
                        '                            Next icnt%
                        '                            Put #2, rps2, FILEBUF2
                        '                rps2 = rps2 + 20000
                        '                        Next LoopCnt%
                        '                    End If
                        '                    If LoopRem > 0 Then
                        '                        NCntAdd = 5000 * LoopNumb
                        '                        ICntTar = filen - 5000 * LoopNumb
                        '                        For icnt% = 1 To ICntTar
                        '                            icntm% = icnt% - 1
                        '                            FILEBUF3.elem3(icntm%) = N(icnt% + NCntAdd)
                        '                        Next icnt%
                        '                        'JRS 8/20/01 Put #2, rps2, FILEBUF3
                        '                        Put #2, rps2, FILEBUF3.elem3
                        '     End If
                        '                    Close #1
                        '   Close #10
                        '   'JRS 08/11/03
                        '   'jrs 09/04/03 frm_MainMenu.IFsGUIctrl1.XClearPlot
                        '   'JRS 06/09/03 RAND PARDEE
                        '   If frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                        '                        rsSenInput.Close
                        '                    End If

                    End Using
                Next iYear%

                '                'jrs 2016/10/28 Reload First Year
                '                If StartYear > 1 Then
                '                    'Read Vector from First Year
                '                    rps = 1
                '                    rps2 = rps
                '                    LoopNumb = filen \ 5000
                '                    LoopRem = filen Mod 5000
                '                    If LoopNumb >= 1 Then
                '                        For LoopCnt% = 1 To LoopNumb
                '                            LoopCntReal = LoopCnt%
                '                Get #2, rps2, FILEBUF2
                '                NCntAdd = 5000 * (LoopCntReal - 1)
                '                            For icnt% = 1 To 5000
                '                                icntm% = icnt% - 1
                '                                N(icnt% + NCntAdd) = FILEBUF2.elem2(icntm%)
                '                            Next icnt%
                '                            rps2 = rps2 + 20000
                '                        Next LoopCnt%
                '                    End If
                '                    If LoopRem > 0 Then
                '            'JRS 8/20/01 Get #2, rps2, FILEBUF3
                '            Get #2, rps2, FILEBUF3.elem3
                '            NCntAdd = 5000 * LoopNumb
                '                        ICntTar = filen - 5000 * LoopNumb
                '                        For icnt% = 1 To ICntTar
                '                            icntm% = icnt% - 1
                '                            N(icnt% + NCntAdd) = FILEBUF3.elem3(icntm%)
                '                        Next icnt%
                '                    End If

                '                    'Change PosModYear
                '                    N(VarPoint(PosModYear)) = frm_MainMenu.IFsGUIctrl1.cFirstYear + RTime - 1
                '                    'Save Vector
                '                    rps2 = rps
                '                    LoopNumb = filen \ 5000
                '                    LoopRem = filen Mod 5000
                '                    If LoopNumb >= 1 Then
                '                        For LoopCnt% = 1 To LoopNumb
                '                            LoopCntReal = LoopCnt%
                '                            NCntAdd = 5000 * (LoopCntReal - 1)
                '                            For icnt% = 1 To 5000
                '                                icntm% = icnt% - 1
                '                                FILEBUF2.elem2(icntm%) = N(icnt% + NCntAdd)
                '                            Next icnt%
                '                            Put #2, rps2, FILEBUF2
                '                   rps2 = rps2 + 20000
                '                        Next LoopCnt%
                '                    End If
                '                    If LoopRem > 0 Then
                '                        NCntAdd = 5000 * LoopNumb
                '                        ICntTar = filen - 5000 * LoopNumb
                '                        For icnt% = 1 To ICntTar
                '                            icntm% = icnt% - 1
                '                            FILEBUF3.elem3(icntm%) = N(icnt% + NCntAdd)
                '                        Next icnt%
                '                        'JRS 8/20/01 Put #2, rps2, FILEBUF3
                '                        Put #2, rps2, FILEBUF3.elem3
                '        End If
                '                End If

                '                'jrs 2008/05/05 moving it out of the year loop (it was just after the close of the recordset)
                '                If frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                '                    dbSenInput.Close
                '                End If

                '                frm_MainMenu.IFsGUIctrl1.XClearPlot

                '                If Scenarioflag Then
                '                    Unload frm_Progress
                '   End If

                '                'jrs 2012/05/11
                '                dbG.Close

                '                'jrs 2017/01/06
                '                'Bilateral Trade
                '                '---------------
                '                'jrs 2017/01/04
                '                If Dir(PutFdrvRun$ + "IFsForDyadicWork.mdb") <> "" Then
                '                    rsBT.Close
                '                    dbBT.Close

                '                    rsDet.Close
                '                    rsDef.Close
                '                    dbBTBase.Close
                '                End If

                '                Close

            Catch ex As Exception
                'BadLine:
                '        msg$ = "There appears to be a bad or blank line in your SensitivityInput.dat or "
                '        msg$ = msg$ + "Working.sce file.  Processing will now cease.  Look at Line" _
                '+ " (where 0 suggests blank line): " _
                '+ Str$(FileSensitivityNumber) + Var$ + VLine$
                '        'jrs 2008/04/02
                '        If bShowMsg Then
                '            Debug.Print(msg$)
                '        End If
                '        BadDatFile = True
                'jrs 2019/09/27 Close

                Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "ErrorSce.txt", append:=True)
                objWriter.WriteLine(ex.Message)
                objWriter.Close()

            End Try


        End Sub '=============================Function2.Bas=======================

        'jrs 2023/05/16 New procedure optimized for .NET
        Sub ProcessTimeSeriesNew(SensitivityNumber As Short, StartYear As Short, context As IContext,
                              ByRef IFsPreResults As Dictionary(Of String, IDataframe), ByRef IFsResults As Dictionary(Of String, Dataframe)) 'jrs 2016/10/28

            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            Dim bCT As Boolean
            Dim CurrFact As Single
            Dim VLine2 As String
            Dim dim2idx As Integer

            'Target Year, Target Value, FirstYear of change towards target
            Dim TgYr As Integer, TgVal As Single, TgFstYr As Integer
			
			'gvdb 2024/04/05
            ' unused variable
            'Dim GroupLines() As String
            'Dim GroupVals() As Single

            'jrs 2019/09/27 ReDim N(filen) As Single

            'JRS 09/04/03
            Dim RegIdx As Integer

            '   Check for existence of IFs.Dat
            '   ------------------------------
            Dim msg, VLine, Var, X(), P1, StringHold, StringHold1, StringHold2, StringHold3 As String
            Dim xIdx, P1Val, Dim1No, Dim1Max, Dim2Max, Dim3Max As Integer
            Dim nvalue, prevvalue As Single
            Dim currVar As IVariable
            Dim bValidDim As Boolean
            Dim dfWeight As IDataframe = Nothing
            Dim dfNidx As Integer
            Dim Dim1Count, Dim2Count, Dim3Count As Short
            Dim sAgg As String
            Dim myValue As Single
            Dim factor As Single
            Dim Maximum As Short
            Dim P2 As String, P2Val As Integer
            Dim P3 As String, P3Val As Integer
            Dim DimNo, DimMax As Integer
            Dim myValue2_1 As Single
            Dim factor2_1 As Single
            Dim sAgg2_1 As String
            Dim myValue3_1 As Single
            Dim factor3_1 As Single
            Dim sAgg3_1 As String

            Try

                Dim RTime As Short
                RTime = CShort(context.LastYear - context.FirstYear + 1)

                Dim Filename As String
                If Scenarioflag = True Then 'jrs 2019/09/27 frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Or Scenarioflag = True Then
                    Filename$ = PutFdrvRun$ + "working.run"
                Else
                    Filename$ = PutFdrvRun$ + context.ActiveBase '"IFsBase.RUN"
                End If


                'Bilateral Trade
                '---------------
                Dim ifsVarDyadic As Dictionary(Of String, Single) = New Dictionary(Of String, Single)
                Dim ifsVarDyadicDim3 As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)
                If System.IO.File.Exists(PutFdrvRun$ + "IFsForDyadicWork.db") Then

                    Using dbBT As IFsForDyadicContext = New IFsForDyadicContext()
                        Dim conn = dbBT.Database.GetDbConnection
                        If conn.State = 0 Then 'closed
                            conn.Open()
                        End If

                        Using cmd2 = conn.CreateCommand()
                            cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                            cmd2.ExecuteNonQuery()
                        End Using
                        dbBT.Database.AutoTransactionsEnabled = False
                        dbBT.ChangeTracker.AutoDetectChangesEnabled = False
                        dbBT.ChangeTracker.LazyLoadingEnabled = False

                        Dim pd As ParametersDefault
                        For Each pd In dbBT.ParametersDefault
                            ifsVarDyadic.Add(pd.ParameterName, pd.DefaultValue)
                            If pd.Dim3.HasValue Then
                                If pd.Dim3 > 0 Then
                                    ifsVarDyadicDim3.Add(pd.ParameterName, CInt(pd.Dim3))
                                End If
                            End If
                        Next

                        'jrs 2026/05/06
                        Using cmd = conn.CreateCommand()
                            cmd.CommandText = "DELETE FROM ParametersDetail"
                            cmd.ExecuteNonQuery()
                        End Using
                        dbBT.Flush()

                    End Using

                End If


                '   Do this for all years
                '   ---------------------

                Dim IFsResultsBase As Dictionary(Of String, IDataframe) = New Dictionary(Of String, IDataframe)
                Dim storageSvc As IStorageService = New StorageService(Nothing, "IFsBase.run.db")
                Dim BaseFile As IStorage = storageSvc.Open(False)

                Dim LastVar As String
                Dim No() As Single

                'jrs 2023/05/16 For iYear% = StartYear To RTime
                If Scenarioflag = True Then
                    Filename = ScenarioFileDir + "Working.sce"
                Else
                    If context.HistoricMode Then
                        Filename = context.DataFileDir + SupplementalHistoricDataFile$
                    Else
                        Filename = context.DataFileDir + SupplementalDataFile$
                    End If
                End If
                LastVar$ = ""

                'jrs 2024/02/27
                Dim SceBaseYear As Integer = BaseYear
                Dim Lag As Short = 0

                Using reader As StreamReader = New StreamReader(Filename)
                    ' Read one line from file
                    VLine = reader.ReadLine


                    '     Read the variable file (could be Supplemental.dat or Scenario.sce file)
                    '     -----------------------------------------------------------------------
                    Do While Not VLine = Nothing '07/10/03 Or SensitivityFlag = True

                        If Scenarioflag = True Then
                            X = VLine.Split(New Char() {","c})
                            Var$ = X(1).Replace("""", "")

                            xIdx = 2
                            'mti 19 Nov 01
                            If Var$ = "START" Then 'Comment,Start
                                Exit Do
                            End If
                            'mti 19 Nov 01

                            'jrs 2024/02/27
                            If X(0).ToUpper = "BASEYEAR" Then
                                SceBaseYear = CInt(Var)
                                Lag = CShort(SceBaseYear - BaseYear)
                            End If
                        Else
                            'start reading .dat
                            X = VLine.Split(New Char() {","c})
                            Var$ = X(0).Replace("""", "")
                            xIdx = 1
                            'jrs 2026/01/19
                            If X(0).ToUpper = "BASEYEAR" Then
                                SceBaseYear = CInt(X(1))
                                Lag = CShort(SceBaseYear - BaseYear)
                            End If

                            If Var$ = "" Then Exit Do
                            Do While Var.ToUpper = "COMMENT" 'Comment,Start
                                VLine = reader.ReadLine
                                X = VLine.Split(New Char() {","c})
                                Var$ = X(1).Replace("""", "")
                            Loop
                        End If

                        'Now that know the number of the variable, process it as either non-dimensional,
                        '  simple variable, or as singly or doubly-dimenionsed
                        If context.VariableNames.Contains(Var$) Or context.VariableNames.Contains(Var$.ToUpper) Or context.VariableNames.Contains(Var$.ToLower) Then
                            Dim pos As Integer
                            pos = VLine.IndexOf(Var$)
                            VLine = VLine.Substring(pos)
                            VLine = VLine.Replace(Var$ + ",", "")
                            VLine = VLine.Replace("CUSTOM,", "")
                            VLine = VLine.Replace("MEDIUM,", "")
                            VLine = VLine.Replace("HIGH0010,", "")
                            VLine = VLine.Replace("LOW0010,", "")
                            VLine = VLine.Replace("LOW0020,", "")
                            If Not context.VariableNames.Contains(Var$) Then
                                Var$ = Var$.ToUpper
                            End If
                            If Not context.VariableNames.Contains(Var$) Then
                                Var$ = Var$.ToLower
                            End If
                            currVar = context.GetVariable(Var$)

                            If currVar.Aggregation Is Nothing Then
                                dfWeight = Nothing
                            Else
                                Select Case currVar.Aggregation
                                    Case "GDP"
                                        dfWeight = IFsPreResults("GDP")
                                    Case "POP"
                                        dfWeight = IFsPreResults("POP")
                                    Case Else
                                        If IFsAggRules.ContainsKey(currVar.Aggregation) AndAlso IFsPreResults.ContainsKey(IFsAggRules(currVar.Aggregation)) Then
                                            dfWeight = IFsPreResults(IFsAggRules(currVar.Aggregation))
                                        Else
                                            dfWeight = Nothing
                                        End If
                                End Select
                            End If
                            If Not IFsResults.ContainsKey(Var) Then
                                If IFsPreResults.ContainsKey(Var$) Then
                                    Dim FirstRowSize As Integer = 1
                                    Dim DimCount As Integer = currVar.Dimensions.Count
                                    Dim Dim1Size, Dim2Size, Dim3Size As Short

                                    If DimCount >= 2 Then
                                        Dim1Size = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                        FirstRowSize = FirstRowSize * Dim1Size
                                    End If
                                    If DimCount >= 3 Then
                                        Dim2Size = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                                        FirstRowSize = FirstRowSize * Dim2Size
                                    End If
                                    If DimCount >= 4 Then
                                        Dim3Size = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)
                                        FirstRowSize = FirstRowSize * Dim3Size
                                    End If
                                    If IFsPreResults(Var).RowCount > FirstRowSize Then
                                        IFsResults.Add(Var$, CType(IFsPreResults(Var$), Dataframe))
                                    Else
                                        IFsResults.Add(Var$, Dataframe.Create(Var$, context, context.FirstYear, context.LastYear, 0))

                                        'if there's data in first year
                                        If IFsPreResults(Var).RowCount > 0 Then
                                            'Copy Results from first year
                                            Dim i, j, k As Short
                                            Dim dfIdx As Integer
                                            Select Case DimCount
                                                Case 1
                                                    dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear)
                                                    IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                Case 2
                                                    For i = 1 To Dim1Size
                                                        dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, i, Dim1Size)
                                                        IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                    Next i
                                                Case 3
                                                    For i = 1 To Dim1Size
                                                        For j = 1 To Dim2Size
                                                            dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, i, Dim1Size, j, Dim2Size)
                                                            IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                        Next j
                                                    Next i
                                                Case 4
                                                    For i = 1 To Dim1Size
                                                        For j = 1 To Dim2Size
                                                            For k = 1 To Dim3Size
                                                                dfIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, i, Dim1Size, j, Dim2Size, k, Dim3Size)
                                                                IFsResults(Var$)(dfIdx) = IFsPreResults(Var$)(dfIdx)
                                                            Next k
                                                        Next j
                                                    Next i
                                            End Select
                                        End If
                                    End If
                                Else
                                    IFsResults.Add(Var$, Dataframe.Create(Var$, context, context.FirstYear, context.LastYear, 0))
                                End If
                            End If

                            'If VDim1%(chk) = 0 And VDim2%(chk) = 0 Then  'routine for simple variables
                            If currVar.Dimensions.Count = 1 Then
                                If X(xIdx).Replace("""", "").Contains(":") Then
                                    nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                Else
                                    'jrs 2024/02/27
                                    If Lag < 0 Then
                                        'jrs 2026/01/19
                                        If X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Contains(":") Then
                                            nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Substring(0, X(Min(xIdx - Lag, UBound(X))).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                        Else
                                            nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", ""))
                                        End If
                                    Else
                                        nvalue = CSng(X(xIdx).Replace("""", ""))
                                    End If
                                End If

                                IFsResults(Var)(getDataFrameIdxFromDims(CShort(FirstYear), FirstYear)) = nvalue

                                'revert to the earlier code (but save new code)
                                If currVar.Type = 3 Then
                                    'jrs 2024/02/27 added - Lag and if IYear > 1
                                    For iYear% = StartYear + 1 - Lag To RTime
                                        If iYear% > 1 Then
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)

                                            IFsResults(Var)(getDataFrameIdxFromDims(CShort(iYear% + FirstYear - 1), FirstYear)) = nvalue
                                        End If
                                    Next
                                End If
                                'ElseIf VDim1%(chk) > 0 And VDim2%(chk) = 0 Then 'routine for single dim. vars.
                            ElseIf currVar.Dimensions.Count = 2 Then
                                Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                P1 = X(xIdx).Replace("""", "").Trim
                                xIdx = xIdx + 1

                                'If Val(P1) = 0 Then  'is string and must determine numerical
                                'IsNumeric = Integer.TryParse(P1, P1Val)
                                If Not Integer.TryParse(P1, P1Val) Then
                                    'Logic to check "Congo, Dem. Rep of the"
                                    If VLine.Substring(0, 1) = """" Then
                                        Dim pos2 As Integer
                                        pos2 = VLine.IndexOf("""", 1) - 1
                                        P1 = VLine.Substring(1, pos2)
                                        If P1.IndexOf(",") >= 0 Then
                                            xIdx = xIdx + 1
                                        End If
                                    ElseIf VLine.IndexOf("Congo, Dem. Republic of the") = 0 Then
                                        P1 = "Congo, Dem. Republic of the"
                                        xIdx = xIdx + 1
                                    End If
                                    VLine = VLine.Replace("""" + P1 + """,", "")
                                    VLine = VLine.Replace(P1 + ",", "")
                                    VLine = VLine.Replace(P1 + " ,", "") 'jrs 2020/07/14
                                    Dim1No = currVar.Dimensions(1)
                                    Dim1Max = context.GetDimension(Dim1No).Buckets.Count
                                    'Dim1Max = MaxVal(VDim1%(chk))
                                    'Dim1No = VDim1%(chk)
                                    StringHold$ = P1
                                    bValidDim = True
                                    For eleloop = 1 To Dim1Max 'Up to number of elements on dimension
                                        If StringHold$ = "Total" And GRPS(Dim1No, Dim1Max - 1) <> "Total" Then
                                            P1Val = 1
                                            Exit For
                                        End If

                                        If StringHold$.ToUpper = GRPS$(Dim1No, eleloop).ToUpper Then
                                            P1Val = eleloop
                                            Exit For
                                        End If
                                        If GRPS(Dim1No, eleloop).Contains(",") Then
                                            If StringHold$.ToUpper = GRPS(Dim1No, eleloop).Substring(0, GRPS(Dim1No, eleloop).IndexOf(",")).ToUpper Then
                                                P1 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                StringHold = StringHold + ", " + P1
                                                If StringHold$.ToUpper = GRPS$(Dim1No, eleloop).ToUpper Then
                                                    VLine = VLine.Replace("""" + StringHold + """,", "")
                                                    VLine = VLine.Replace(""" " + P1 + """,", "")
                                                    VLine = VLine.Replace(P1 + ",", "")
                                                    P1Val = eleloop
                                                    Exit For
                                                Else
                                                    StringHold = StringHold.Replace(", " + P1, "")
                                                    xIdx = xIdx - 1
                                                End If
                                            End If
                                        End If

                                        If eleloop >= Dim1Max Then 'Not Found
                                            gLoadCountries(StringHold$)
                                            If gGroupTot > 0 Then
                                                P1Val = gCountryGroupIdx(1)
                                                P1 = P1Val.ToString
                                            End If
                                            If Not Integer.TryParse(P1, P1Val) Then
                                                msg$ = StringHold$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                bValidDim = False
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If
                                        End If
                                    Next eleloop
                                Else
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P1.Length + 1)
                                End If
                                If bValidDim Then
                                    If X(xIdx).Replace("""", "").Contains(":") Then
                                        nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                    Else
                                        'jrs 2024/02/27
                                        If Lag < 0 Then
                                            'jrs 2026/01/19
                                            If X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Contains(":") Then
                                                nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Substring(0, X(Min(xIdx - Lag, UBound(X))).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                            Else
                                                nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "")) 'first and possibly only value (unless is series)
                                            End If
                                        Else
                                            nvalue = CSng(X(xIdx).Replace("""", "")) 'first and possibly only value (unless is series)
                                        End If
                                    End If
                                    If gGroupTot > 0 Then
                                        bCT = If(currVar.CommonTarget, False)
                                        'jrs 2024/12/03
                                        If bCT And Scenarioflag = False Then
                                            'Only allowed for scenarios
                                            bCT = False
                                        End If
                                        If bCT Then
                                            'Currency Handling
                                            CurrFact = 1
                                            If Not currVar.Currency = Nothing Then
                                                If currVar.Currency.ToUpper = "TRUE" Then
                                                    CurrFact = CurrencyVal
                                                End If
                                            End If

                                            'Find Target Year, Target Value, FirstYear of change towards target
                                            'Need entire VLine before parsing
                                            FindCommonTargetVars(TgYr, TgVal, TgFstYr, VLine)
                                            'Retrieve N for TgFstYr (No)
                                            'there might be different TgFstYrs per parameter
                                            If Not IFsResultsBase.ContainsKey(Var) Then
                                                IFsResultsBase.Add(Var, BaseFile.LoadDataframe(Var))
                                            End If

                                        End If

                                        'jrs 2024/02/27 added - Lag
                                        For iYear% = StartYear - Lag To RTime
                                            If bCT Then
                                                If iYear >= TgFstYr And iYear < TgYr Then
                                                    ReDim No(gGroupTot)
                                                    For eleloop = 1 To gGroupTot
                                                        'reading base file
                                                        'jrs 2008/05/05 changed from TgFstYr-1 back to 1
                                                        No(eleloop) = getSingleValue(currVar, CType(IFsResultsBase(Var), IDataframe), CShort(iYear + FirstYear - 1), context, CShort(gCountryGroupIdx(eleloop)))
                                                    Next eleloop
                                                End If
                                            End If
                                            If iYear > 1 Then
                                                'mti 2019/05/14 start
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                                'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                                'mti 2019/05/14 End
                                            End If
                                            If Not bCT Then
                                                If IFsPreResults.ContainsKey(Var) Then
                                                    'jrs 2020/07/28
                                                    If (currVar.Aggregation.ToUpper = "NONE" Or currVar.Aggregation = "") And iYear% = StartYear Then
                                                        Debug.Print("The aggregation rule for " & currVar.Name & " is """ & currVar.Aggregation & """. There could be problems setting parameters for groups.")
                                                    End If

                                                    'jrs 2024/02/27 added max(+ lag, 1)
                                                    myValue = getGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(Max(iYear + Lag, 1) + FirstYear - 1), context, dfWeight)       'JRS 08/11/03 begin
                                                Else
                                                    myValue = 0
                                                End If

                                                'jrs 2005/04/29
                                                sAgg = currVar.Aggregation

                                                If sAgg = "SUM" Then
                                                    If myValue = 0 Then
                                                        factor = nvalue / gGroupTot  'jrs 2006/05/05 instead of 0
                                                    Else
                                                        factor = nvalue / myValue
                                                    End If
                                                Else
                                                    factor = nvalue - myValue
                                                End If
                                            End If 'if not bct

                                            Dim CtryIdx As Integer
                                            'jrs 2006/05/04 If factor <> 0 Then changes where sAgg = "SUM" and nvalue = 0 where not applied, enpoilmax
                                            For CtryIdx = 1 To gGroupTot
                                                RegIdx = gCountryGroupIdx(CtryIdx)
                                                'jrs 2008/04/10
                                                'if it's a Common Target parameter
                                                'jrs 2024/02/27 added Max( + lag, 1)
                                                dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count)
                                                If bCT Then
                                                    'if Year is less than Target Year
                                                    If iYear < TgYr Then
                                                        'Year is already equal or greater than when the change starts
                                                        If iYear >= TgFstYr Then
                                                            'No is the value of N in year TgFstYr for Region RegIdx

                                                            'Common Target logic
                                                            IFsResults(Var)(dfNidx) = No(CtryIdx) + (TgVal - No(CtryIdx)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                                        End If
                                                    Else
                                                        'else just assign group value to all members (converged already)
                                                        IFsResults(Var)(dfNidx) = nvalue
                                                    End If
                                                Else
                                                    If Var$ = "gdprext" And nvalue = -100 Then
                                                        'Assign -100 to all members
                                                        IFsResults(Var)(dfNidx) = -100
                                                        'jrs 2005/05/02
                                                    ElseIf sAgg = "SUM" And myValue <> 0 Then 'jrs 2006/05/05 added And MyValue <> 0
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor
                                                    End If

                                                    If IFsResults(Var)(dfNidx) > currVar.Max Then
                                                        IFsResults(Var)(dfNidx) = CSng(currVar.Max)
                                                    ElseIf IFsResults(Var)(dfNidx) < currVar.Min Then
                                                        IFsResults(Var)(dfNidx) = CSng(currVar.Min)
                                                    End If
                                                End If
                                            Next CtryIdx

                                        Next iYear%
                                    End If 'If gGroupTot > 0 Then

                                    'jrs 2024/02/27 added - lag
                                    For iYear% = StartYear - Lag To RTime
                                        If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year
                                            If gGroupTot = 0 Then
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            End If
                                        End If

                                        Dim1Max = Dim1Count + 1  'to handle "All" regions
                                        If P1Val = Dim1Max Then
                                            For ICount% = 1 To Dim1Max - 1
                                                dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(ICount), Dim1Count)
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Next ICount%
                                        Else
                                            If gGroupTot = 0 Then
                                                dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count)
                                                IFsResults(Var)(dfNidx) = nvalue
                                                'jrs 2014/03/07 loop through all other Dimensions
                                                If P1Val = 1 And StringHold = "Total" Then
                                                    For dim2idx = 2 To Dim1Max - 1
                                                        dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(dim2idx), Dim1Count)
                                                        IFsResults(Var)(dfNidx) = nvalue
                                                    Next dim2idx
                                                End If
                                            End If
                                            'jrs 2023/06/07 gGroupTot = 0
                                        End If
                                    Next
                                    'jrs 2023/06/07
                                    gGroupTot = 0
                                End If 'If bValidDim Then

                            ElseIf currVar.Dimensions.Count = 3 Then 'routine for double dim. vars.
                                Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                                Maximum = Dim1Count

                                'JRS 06/09/03 RAND PARDEE IF END IF
                                'If Not frm_MainMenu.IFsGUIctrl1.cSensitivityFlag Then
                                '   Input #1, P1, P2  'Either could be string pointer or numerical pointer to
                                P1 = X(xIdx).Replace("""", "").Trim
                                xIdx = xIdx + 1
                                P2 = X(xIdx).Replace("""", "").Trim
                                xIdx = xIdx + 1

                                'jrs 2021/04/13
                                If P1 = "Congo" And P2 = "Dem. Republic of the" Then
                                    P1 = "Congo, Dem. Republic of the"
                                    P2 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1
                                End If

                                '   'element number; if numerical, ok as is
                                'Else
                                '   P1 = rsSenInput.Fields("Dim")
                                '   P2 = rsSenInput.Fields("Dim2")
                                'End If

                                'jrs 2025/08/11
                                Dim GroupTot1, GroupTot2 As Integer

                                If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical                                        
                                    'jrs 2022/07/06
                                    'VLine = VLine.Replace("""" + P1 + """,", "")
                                    'VLine = VLine.Replace(P1 + ",", "")
                                    VLine = ReplaceFirst(VLine, """" + P1 + """,", "")
                                    VLine = ReplaceFirst(VLine, P1 + ",", "")
                                    StringHold1$ = P1
                                    For eleloop = 1 To Dim1Count 'Up to number of elements on dimension
                                        If StringHold1$ = "Total" And GRPS(currVar.Dimensions(1), Dim1Count) <> "Total" Then
                                            P1 = "1"
                                            P1Val = 1
                                            Exit For
                                        End If

                                        If StringHold1$.ToUpper = GRPS$(currVar.Dimensions(1), eleloop).ToUpper Then
                                            P1 = eleloop.ToString
                                            P1Val = eleloop
                                            Exit For
                                        End If
                                        If GRPS(currVar.Dimensions(1), eleloop).Contains(",") Then
                                            If StringHold1$.ToUpper = GRPS(currVar.Dimensions(1), eleloop).Substring(0, GRPS(currVar.Dimensions(1), eleloop).IndexOf(",")).ToUpper Then
                                                P1 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                'Remove blanks jrs 2020/01/10
                                                Do While VLine.Substring(0, 1) = " "
                                                    VLine = VLine.Substring(1)
                                                Loop
                                                'jrs 2020/07/03 VLine = VLine.Substring(P1.Length + 1)
                                                StringHold1 = StringHold1 + ", " + P2
                                                If StringHold1$.ToUpper = GRPS$(currVar.Dimensions(1), eleloop).ToUpper Then
                                                    'jrs 2020/07/03
                                                    VLine = VLine.Replace(P2 + ",", "")
                                                    VLine = VLine.Replace(""" " + P2 + """,", "")

                                                    P2 = P1
                                                    P1 = eleloop.ToString
                                                    P1Val = eleloop
                                                    VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                    Exit For
                                                Else
                                                    StringHold1 = StringHold1.Replace(", " + P2, "")
                                                    xIdx = xIdx - 1
                                                End If
                                            End If
                                        End If

                                        If eleloop >= Dim1Count Then 'Not Found
                                            'JRS 12/08/01 GROUPING
                                            gLoadCountries(StringHold1$)
                                            If gGroupTot > 0 Then
                                                'jrs 2025/08/11
                                                GroupTot1 = gGroupTot
                                                'jrs 2014/11/07 P1 = gRegionIdx(gCountryGroup(1))
                                                P1Val = gCountryGroupIdx(1)
                                                P1 = P1Val.ToString
                                            Else 'jrs 2010/10/12, maybe the group has a comma
                                                P1 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                'Remove blanks jrs 2020/01/10
                                                Do While VLine.Substring(0, 1) = " "
                                                    VLine = VLine.Substring(1)
                                                Loop
                                                VLine = VLine.Substring(P1.Length + 1)
                                                StringHold1$ = StringHold1$ & ", " & P2
                                                P2 = P1
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then
                                                    'jrs 2025/08/11
                                                    GroupTot1 = gGroupTot

                                                    P1Val = gCountryGroupIdx(1)
                                                    P1 = P1Val.ToString
                                                Else
                                                    P1 = StringHold1$
                                                End If
                                            End If
                                            If Not Integer.TryParse(P1, P1Val) Then
                                                msg$ = StringHold1$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry."
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If

                                        End If
                                    Next eleloop
                                Else 'Not a string, but numeric instead
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P1.Length + 1)
                                End If
                                DimMax = Dim2Count + 1
                                'Remove blanks jrs 2020/01/10
                                Do While VLine.Substring(0, 1) = " "
                                    VLine = VLine.Substring(1)
                                Loop
                                VLine = VLine.Substring(P2.Length + 1)
                                If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                    StringHold2$ = P2
                                    For eleloop = 1 To Dim2Count 'Up to number of elements on dimension
                                        If StringHold2$ = "Total" And GRPS(currVar.Dimensions(2), Dim2Count) <> "Total" Then
                                            P2Val = 1
                                            P2 = "1"
                                            Exit For
                                        End If

                                        If StringHold2$.ToUpper = GRPS$(currVar.Dimensions(2), eleloop).ToUpper Then
                                            P2Val = eleloop
                                            P2 = P2Val.ToString
                                            Exit For
                                        End If
                                        DimNo = currVar.Dimensions(2)
                                        If GRPS(DimNo, eleloop).Contains(",") Then
                                            If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                P2 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                StringHold2 = StringHold2 + ", " + P2
                                                If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P2Val = eleloop
                                                    P2 = P2Val.ToString
                                                    VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                    Exit For
                                                Else
                                                    StringHold2 = StringHold2.Replace(", " + P2, "")
                                                    xIdx = xIdx - 1
                                                End If
                                            End If
                                        End If
                                        If eleloop >= Dim2Count Then 'Not Found
                                            gLoadCountries(StringHold2$)
                                            If gGroupTot > 0 Then
                                                'jrs 2025/08/11
                                                GroupTot2 = gGroupTot

                                                P2Val = gCountryGroupIdx(1)
                                                P2 = P2Val.ToString
                                            Else
                                                msg$ = StringHold2$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If

                                            End If
                                        End If
                                    Next eleloop
                                Else
                                    P2Val = CInt(P2)
                                    StringHold2$ = GRPS(currVar.Dimensions(2), P2Val)  'subdim name
                                End If

                                If X(xIdx).Replace("""", "").Contains(":") Then
                                    nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                Else
                                    'jrs 2024/02/27
                                    If Lag < 0 Then
                                        'jrs 2026/01/19
                                        If X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Contains(":") Then
                                            nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Substring(0, X(Min(xIdx - Lag, UBound(X))).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                        Else
                                            nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", ""))
                                        End If
                                    Else
                                        nvalue = CSng(X(xIdx).Replace("""", ""))
                                    End If
                                End If
                                VLine2 = VLine

                                'jrs 2024/02/27 adding Lag
                                For iYear% = StartYear - Lag To RTime
                                    If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year
                                        'should go in even in the first year when there is a time offset, e.g., hist rebuild with first year layter than 1960
                                        Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                    End If

                                    If gGroupTot > 0 Then
                                        bCT = If(currVar.CommonTarget, False)
                                        If bCT Then
                                            'Currency Handling
                                            CurrFact = 1
                                            If currVar.Currency.ToUpper = "TRUE" Then
                                                CurrFact = CurrencyVal
                                            End If

                                            'Find Target Year, Target Value, FirstYear of change towards target
                                            'Need entire VLine before parsing
                                            FindCommonTargetVars(TgYr, TgVal, TgFstYr, VLine2)
                                            'there might be different TgFstYrs per parameter
                                            If iYear >= TgFstYr And iYear < TgYr Then
                                                'jrs 2019/12/13
                                                If iYear = TgFstYr AndAlso Not IFsResultsBase.ContainsKey(Var) Then
                                                    IFsResultsBase.Add(Var, BaseFile.LoadDataframe(Var))
                                                End If

                                                ReDim No(gGroupTot)
                                                For eleloop = 1 To gGroupTot
                                                    No(eleloop) = getSingleValue(currVar, CType(IFsResultsBase(Var), IDataframe), CShort(iYear + FirstYear - 1), context, CShort(gCountryGroupIdx(eleloop)), CShort(P2Val))
                                                Next eleloop
                                            End If
                                        Else
                                            If currVar.Dimensions(2) = 1 Then
                                                'jrs 2024/02/27 added Max(,1) to handle negative yrs from the lag
                                                myValue2_1 = getGroupXGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(Max(iYear + Lag, 1) + FirstYear - 1), context, dfWeight, StringHold1$, StringHold2$, 0)
                                            Else
                                                If (currVar.Aggregation.ToUpper = "NONE" Or currVar.Aggregation = "") And iYear% = StartYear Then
                                                    Debug.Print("The aggregation rule for " & currVar.Name & " is """ & currVar.Aggregation & """. There could be problems setting parameters for groups.")
                                                End If

                                                'jrs 2024/02/27 added Max(,1) to handle negative yrs from the lag
                                                myValue2_1 = getGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(Max(iYear + Lag, 1) + FirstYear - 1), context, dfWeight, CShort(P2Val))
                                            End If
                                        End If 'eof if bCT else


                                        If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                        End If

                                        Dim CtryIdx2 As Integer
                                        If bCT Then
                                            For CtryIdx2 = 1 To gGroupTot
                                                RegIdx = gCountryGroupIdx(CtryIdx2)
                                                'jrs 2025/08/11
                                                For CtryIdx = 1 To GroupTot1
                                                    Dim RegIdx1 = gCountryGroupIdx(CtryIdx)
                                                    'jrs 2025/08/11 dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count)
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx1), Dim1Count, CShort(RegIdx), Dim2Count)
                                                    'if Year is less than Target Year
                                                    If iYear < TgYr Then
                                                        'Year is already equal or greater than when the change starts
                                                        If iYear >= TgFstYr Then
                                                            'No is the value of N in year TgFstYr for Region RegIdx

                                                            'Common Target logic
                                                            IFsResults(Var)(dfNidx) = No(CtryIdx2) + (TgVal - No(CtryIdx2)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                                        End If
                                                    Else
                                                        'else just assign group value to all members (converged already)
                                                        IFsResults(Var)(dfNidx) = nvalue
                                                    End If

                                                Next CtryIdx

                                            Next CtryIdx2

                                        Else
                                            sAgg2_1 = currVar.Aggregation

                                            If sAgg2_1 = "SUM" Then
                                                If myValue2_1 = 0 Then
                                                    factor2_1 = nvalue / gGroupTot 'jrs 2006/05/31 related to error fixed on 'jrs 2006/05/04
                                                Else
                                                    factor2_1 = nvalue / myValue2_1
                                                End If
                                            Else
                                                factor2_1 = nvalue - myValue2_1
                                            End If

                                            If P2Val <> DimMax And factor2_1 <> 0 Then 'jrs 2014/03/07 was using Dim2Max, but that doesn't exist
                                                If currVar.Dimensions(2) = 1 Then
                                                    UpdateGroupbyGroup(currVar, IFsResults(Var), CShort(Max(iYear + Lag, 1) + FirstYear - 1), context, StringHold1$, StringHold2$, 0, factor2_1)
                                                Else
                                                    For CtryIdx2 = 1 To gGroupTot
                                                        RegIdx = gCountryGroupIdx(CtryIdx2)
                                                        dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(P2Val), Dim2Count)
                                                        If sAgg2_1 = "SUM" Then
                                                            If myValue2_1 <> 0 Then
                                                                IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor2_1
                                                            Else
                                                                'When all values in the group are 0 multiplication doesn't work
                                                                IFsResults(Var)(dfNidx) = factor2_1
                                                            End If
                                                        Else
                                                            IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1

                                                            'jrs 2014/03/07 loop through all other Dimensions
                                                            If P2Val = 1 And StringHold2 = "Total" Then
                                                                For dim2idx = 2 To DimMax - 1
                                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(dim2idx), Dim2Count)
                                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1
                                                                Next dim2idx
                                                            End If
                                                        End If
                                                    Next CtryIdx2
                                                End If
                                            End If
                                        End If 'eof if bCT else
                                    End If 'eof If gGroupTot > 0

                                    Dim1Max = Dim1Count + 1 'to handle "All" in dimensionality
                                    Dim2Max = Dim2Count + 1
                                    '"ALL" is not Allowed in Both Subdims
                                    'at the same time
                                    If P1Val = Dim1Max Then
                                        For ICount% = 1 To Dim1Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(ICount%), Dim1Count, CShort(P2Val), Dim2Count)
                                            IFsResults(Var)(dfNidx) = nvalue
                                        Next ICount%
                                    ElseIf P2Val = Dim2Max Then
                                        For ICount% = 1 To Dim2Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(ICount%), Dim2Count)
                                            If gGroupTot = 0 Then
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Else
                                                'jrs 2005/05/02
                                                If sAgg2_1 = "SUM" Then
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor2_1
                                                Else
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1
                                                End If
                                            End If
                                        Next ICount%
                                        'JRS 03/18/03 GROUPS AND ALL
                                        Dim idx3 As Integer
                                        If factor2_1 <> 0 Then
                                            For idx3 = 2 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx3))
                                                RegIdx = gCountryGroupIdx(idx3)
                                                For ICount% = 1 To Dim2Max - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(ICount%), Dim2Count)
                                                    'jrs 2005/05/02
                                                    If sAgg2_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor2_1
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor2_1
                                                    End If
                                                Next ICount%
                                            Next idx3
                                        End If
                                    Else
                                        If gGroupTot = 0 Then
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(P2Val), Dim2Count)
                                            IFsResults(Var)(dfNidx) = nvalue

                                            'jrs 2014/03/07 loop through all other Dimensions
                                            If P2Val = 1 And StringHold2 = "Total" Then
                                                For dim2idx = 2 To DimMax - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(dim2idx), Dim2Count)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                Next dim2idx
                                            End If
                                            'jrs 2014/08/20 loop through all other Dimensions
                                            If P1Val = 1 And StringHold1 = "Total" Then
                                                For ICount = 2 To DimMax - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(ICount), Dim1Count, CShort(P2Val), Dim2Count)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                Next ICount
                                            End If
                                        End If
                                        'JRS 12/06/01 GROUPING

                                        'jrs 2023/06/07 gGroupTot = 0

                                    End If

                                Next

                                'jrs 2023/06/07
                                gGroupTot = 0

                            ElseIf currVar.Dimensions.Count = 4 Then 'routine for triple dim. vars.
                                Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                                Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                                Dim3Count = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)

                                Maximum = Dim1Count

                                P1 = X(xIdx).Replace("""", "").Trim
                                xIdx = xIdx + 1
                                P2 = X(xIdx).Replace("""", "").Trim
                                xIdx = xIdx + 1
                                P3 = X(xIdx).Replace("""", "").Trim
                                xIdx = xIdx + 1

                                Dim1Max = Dim1Count + 1
                                Dim2Max = Dim2Count + 1
                                Dim3Max = Dim3Count + 1

                                If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical
                                    If P1 = "Congo" And P2 = "Dem. Republic of the" Then
                                        P1 = "Congo, Dem. Republic of the"
                                        P2 = P3
                                        P3 = X(xIdx).Replace("""", "").Trim
                                        xIdx = xIdx + 1
                                    End If

                                    VLine = VLine.Replace("""" + P1 + """,", "")
                                    VLine = VLine.Replace(P1 + ",", "")

                                    DimMax = Dim1Count + 1
                                    DimNo = currVar.Dimensions(1)
                                    StringHold1$ = P1
                                    For eleloop = 1 To DimMax - 1 'jrs 2023/06/08 added -1, otherwise error using grups 'Up to number of elements on dimension
                                        If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                            P1Val = eleloop
                                            P1 = P1Val.ToString
                                            Exit For
                                        End If
                                        'jrs 2005/06/11
                                        If GRPS(DimNo, eleloop).Contains(",") Then
                                            If StringHold1$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                P1 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                'Remove blanks jrs 2020/01/10
                                                Do While VLine.Substring(0, 1) = " "
                                                    VLine = VLine.Substring(1)
                                                Loop
                                                VLine = VLine.Substring(P1.Length + 1)
                                                StringHold1 = StringHold1 + ", " + P2
                                                If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P1Val = eleloop
                                                    P1 = P1Val.ToString
                                                    VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                    P2 = P3
                                                    P3 = P1
                                                    Exit For
                                                Else
                                                    StringHold1 = StringHold1.Replace(", " + P2, "")
                                                    xIdx = xIdx - 1
                                                End If
                                            End If
                                        End If

                                        If eleloop >= DimMax - 1 Then 'jrs 2023/06/08 added - 1 'Not Found

                                            'JRS 12/08/01 GROUPING
                                            gLoadCountries(StringHold1$)
                                            If gGroupTot > 0 Then
                                                P1Val = gCountryGroupIdx(1) 'jrs 2014/11/07 P1 = gRegionIdx(gCountryGroup(1))
                                                'jrs 2023/06/08
                                                P1 = P1Val.ToString
                                            End If
                                            If Not Integer.TryParse(P1, P1Val) Then
                                                msg$ = StringHold1$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry."
                                                'jrs 2008/04/02
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If

                                        End If
                                    Next eleloop
                                Else
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P1.Length + 1)
                                End If
                                'Remove blanks jrs 2020/01/10
                                Do While VLine.Substring(0, 1) = " "
                                    VLine = VLine.Substring(1)
                                Loop
                                VLine = VLine.Substring(P2.Length + 1)
                                If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                    DimMax = Dim2Count + 1
                                    DimNo = currVar.Dimensions(2)
                                    StringHold2$ = P2
                                    For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                        If StringHold2$ = "Total" And GRPS(DimNo, DimMax - 1) <> "Total" Then
                                            P2Val = 1
                                            P2 = P2Val.ToString
                                            Exit For
                                        End If

                                        If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                            P2Val = eleloop
                                            P2 = P2Val.ToString
                                            Exit For
                                        End If
                                        If GRPS(DimNo, eleloop).Contains(",") Then
                                            If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                P2 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                'Remove blanks jrs 2020/01/10
                                                Do While VLine.Substring(0, 1) = " "
                                                    VLine = VLine.Substring(1)
                                                Loop
                                                VLine = VLine.Substring(P2.Length + 1)

                                                StringHold2 = StringHold2 + ", " + P3
                                                If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P2Val = eleloop
                                                    P2 = P2Val.ToString
                                                    VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                    P3 = P2
                                                    Exit For
                                                Else
                                                    StringHold2 = StringHold2.Replace(", " + P3, "")
                                                    xIdx = xIdx - 1
                                                End If
                                            End If
                                        End If

                                        If eleloop >= DimMax Then 'Not Found
                                            gLoadCountries(StringHold2$)
                                            If gGroupTot > 0 Then
                                                'jrs 2014/11/07 P2 = gRegionIdx(gCountryGroup(1))
                                                P2Val = gCountryGroupIdx(1)
                                                P2 = P2Val.ToString
                                            Else
                                                msg$ = StringHold2$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If
                                        End If
                                    Next eleloop
                                Else
                                    StringHold2$ = GRPS(currVar.Dimensions(2), P2Val)
                                End If
                                'Remove blanks jrs 2020/01/10
                                Do While VLine.Substring(0, 1) = " "
                                    VLine = VLine.Substring(1)
                                Loop
                                VLine = VLine.Substring(P3.Length + 1)
                                If Not Integer.TryParse(P3, P3Val) Then  'is string and must determine numerical
                                    DimMax = Dim3Count + 1
                                    DimNo = currVar.Dimensions(3)
                                    StringHold3$ = P3
                                    For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                        If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                            P3Val = eleloop
                                            P3 = P3Val.ToString
                                            Exit For
                                        End If
                                        If GRPS(DimNo, eleloop).Contains(",") Then
                                            If StringHold3$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                P3 = X(xIdx).Replace("""", "").Trim
                                                xIdx = xIdx + 1
                                                StringHold3 = StringHold3 + ", " + P3
                                                If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                    P3Val = eleloop
                                                    P3 = P3Val.ToString
                                                    VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                    Exit For
                                                Else
                                                    StringHold3 = StringHold3.Replace(", " + P3, "")
                                                    xIdx = xIdx - 1
                                                End If
                                            End If
                                        End If

                                        If eleloop >= DimMax Then 'Not Found
                                            gLoadCountries(StringHold1$)
                                            If gGroupTot > 0 Then
                                                'jrs 2014/11/07 p3 = gRegionIdx(gCountryGroup(1))
                                                P3Val = gCountryGroupIdx(1)
                                                P3 = P3Val.ToString
                                            Else
                                                msg$ = StringHold3$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If
                                        End If
                                    Next eleloop
                                Else
                                    StringHold3$ = GRPS(currVar.Dimensions(3), P3Val)
                                End If

                                If X(xIdx).Replace("""", "").Contains(":") Then
                                    nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                Else
                                    'jrs 2024/02/27
                                    If Lag < 0 Then
                                        'jrs 2026/01/19
                                        If X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Contains(":") Then
                                            nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Substring(0, X(Min(xIdx - Lag, UBound(X))).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                        Else
                                            nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", ""))
                                        End If
                                    Else
                                        nvalue = CSng(X(xIdx).Replace("""", ""))
                                    End If
                                End If

                                VLine2 = VLine

                                'jrs 2024/02/27
                                For iYear% = StartYear - Lag To RTime
                                    If currVar.Type = 3 And iYear% > 1 Then  'time series plus advanced year
                                        Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                    End If

                                    If gGroupTot > 0 Then
                                        bCT = If(currVar.CommonTarget, False)

                                        If bCT Then
                                            'Currency Handling
                                            CurrFact = 1
                                            If currVar.Currency.ToUpper = "TRUE" Then
                                                CurrFact = CurrencyVal
                                            End If

                                            'Find Target Year, Target Value, FirstYear of change towards target
                                            'Need entire VLine before parsing
                                            FindCommonTargetVars(TgYr, TgVal, TgFstYr, VLine2)

                                            'Retrieve N for TgFstYr (No)
                                            'there might be different TgFstYrs per parameter
                                            If iYear >= TgFstYr And iYear < TgYr Then
                                                'jrs 2019/12/13
                                                If iYear = TgFstYr AndAlso Not IFsResultsBase.ContainsKey(Var) Then
                                                    IFsResultsBase.Add(Var, BaseFile.LoadDataframe(Var))
                                                End If

                                                ReDim No(gGroupTot)
                                                For eleloop = 1 To gGroupTot
                                                    No(eleloop) = getSingleValue(currVar, CType(IFsResultsBase(Var), IDataframe), CShort(iYear + FirstYear - 1), context, CShort(gCountryGroupIdx(eleloop)))
                                                Next eleloop
                                            End If
                                        Else
                                            If (currVar.Aggregation.ToUpper = "NONE" Or currVar.Aggregation = "") And iYear% = StartYear Then
                                                Debug.Print("The aggregation rule for " & currVar.Name & " is """ & currVar.Aggregation & """. There could be problems setting parameters for groups.")
                                            End If

                                            myValue3_1 = getGroupValue(currVar, CType(IFsResults(Var), IDataframe), CShort(iYear + FirstYear - 1), context, dfWeight, CShort(P2Val), CShort(P3Val))       'JRS 08/11/03 begin
                                        End If

                                        If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                        End If

                                        Dim ctryIdx3 As Integer
                                        If bCT Then
                                            For ctryIdx3 = 1 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(ctryIdx3))
                                                RegIdx = gCountryGroupIdx(ctryIdx3)
                                                dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, 1, Dim2Count, 1, Dim3Count)
                                                'if Year is less than Target Year
                                                If iYear < TgYr Then
                                                    'Year is already equal or greater than when the change starts
                                                    If iYear >= TgFstYr Then
                                                        'No is the value of N in year TgFstYr for Region RegIdx

                                                        'Common Target logic
                                                        IFsResults(Var)(dfNidx) = No(ctryIdx3) + (TgVal - No(ctryIdx3)) / (TgYr - TgFstYr + 1) * (iYear - TgFstYr + 1)
                                                    End If
                                                Else
                                                    'else just assign group value to all members (converged already)
                                                    IFsResults(Var)(dfNidx) = nvalue
                                                End If
                                            Next ctryIdx3

                                        Else
                                            'jrs 2017/01/06
                                            sAgg3_1 = currVar.Aggregation

                                            If sAgg3_1 = "SUM" Then
                                                If myValue3_1 = 0 Then
                                                    factor3_1 = nvalue / gGroupTot 'jrs 2006/05/31 related to error fixed on 'jrs 2006/05/04
                                                Else
                                                    factor3_1 = nvalue / myValue3_1
                                                End If
                                            Else
                                                factor3_1 = nvalue - myValue3_1
                                            End If

                                            If P2Val <> Dim2Max And factor3_1 <> 0 Then
                                                For ctryIdx3 = 1 To gGroupTot
                                                    'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(ctryIdx3))
                                                    RegIdx = gCountryGroupIdx(ctryIdx3)
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(P2Val), Dim2Count, CShort(P3Val), Dim3Count)
                                                    If sAgg3_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                        'jrs 2014/08/04 loop through all other Dimensions
                                                        If P2Val = 1 And StringHold2 = "Total" Then
                                                            For dim2idx = 2 To DimMax - 1
                                                                IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                            Next dim2idx
                                                        End If
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                        'jrs 2014/08/04 loop through all other Dimensions
                                                        If P2Val = 1 And StringHold2 = "Total" Then
                                                            For dim2idx = 2 To DimMax - 1
                                                                dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(dim2idx), Dim2Count, CShort(P3Val), Dim3Count)
                                                                IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                            Next dim2idx
                                                        End If
                                                    End If
                                                Next ctryIdx3
                                            End If
                                        End If 'eof if bCT then else
                                    End If

                                    Dim1Max = Dim1Count + 1  'to handle "All" in dimensionality
                                    Dim2Max = Dim2Count + 1
                                    Dim3Max = Dim3Count + 1
                                    '"ALL" is not Allowed in Both Subdims
                                    'at the same time
                                    If P1Val = Dim1Max Then
                                        For ICount% = 1 To Dim1Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(ICount), Dim1Count, CShort(P2Val), Dim2Count, CShort(P3Val), Dim3Count)
                                            IFsResults(Var)(dfNidx) = nvalue
                                        Next ICount%

                                    ElseIf P2Val = Dim2Max Then
                                        For ICount% = 1 To Dim2Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(ICount), Dim2Count, CShort(P3Val), Dim3Count)
                                            If gGroupTot = 0 Then
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Else
                                                'jrs 2005/05/02
                                                If sAgg3_1 = "SUM" Then
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                Else
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                End If
                                            End If
                                        Next ICount%
                                        'JRS 03/18/03 GROUPS AND ALL
                                        Dim idx3_1 As Integer
                                        If factor3_1 <> 0 Then
                                            For idx3_1 = 2 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx3_1))
                                                RegIdx = gCountryGroupIdx(idx3_1)
                                                For ICount% = 1 To Dim2Max - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(ICount), Dim2Count, CShort(P3Val), Dim3Count)
                                                    'jrs 2005/05/02
                                                    If sAgg3_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                    End If
                                                Next ICount%
                                            Next idx3_1
                                        End If
                                    ElseIf P3Val = Dim3Max Then
                                        For ICount% = 1 To Dim3Max - 1
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(P2Val), Dim2Count, CShort(ICount), Dim3Count)
                                            If gGroupTot = 0 Then
                                                IFsResults(Var)(dfNidx) = nvalue
                                            Else
                                                'jrs 2005/05/02
                                                If sAgg3_1 = "SUM" Then
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                Else
                                                    IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                End If
                                            End If
                                        Next ICount%
                                        'JRS 03/18/03 GROUPS AND ALL
                                        Dim idx3_2 As Integer
                                        If factor3_1 <> 0 Then
                                            For idx3_2 = 2 To gGroupTot
                                                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(idx3_2))
                                                RegIdx = gCountryGroupIdx(idx3_2)
                                                For ICount% = 1 To Dim3Max - 1
                                                    dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(RegIdx), Dim1Count, CShort(P2Val), Dim2Count, CShort(ICount), Dim3Count)
                                                    'jrs 2005/05/02
                                                    If sAgg3_1 = "SUM" Then
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) * factor3_1
                                                    Else
                                                        IFsResults(Var)(dfNidx) = IFsResults(Var)(dfNidx) + factor3_1
                                                    End If
                                                Next ICount%
                                            Next idx3_2
                                        End If
                                    Else
                                        If gGroupTot = 0 Then
                                            dfNidx = getDataFrameIdxFromDims(CShort(Max(iYear + Lag, 1) + FirstYear - 1), FirstYear, CShort(P1Val), Dim1Count, CShort(P2Val), Dim2Count, CShort(P3Val), Dim3Count)
                                            IFsResults(Var)(dfNidx) = nvalue
                                        End If
                                        'jrs 2023/06/07 gGroupTot = 0
                                    End If
                                Next
                                'jrs 2023/06/07
                                gGroupTot = 0
                            End If 'End of routine for variables with 3 dimensions
                            'ReadAnother:    'Jump here if find unintelligible subscript (e.g. from another load)
                            LastVar$ = Var$
                        Else 'jrs 2019/12/13 put the Dyadic logic in here
                            If ifsVarDyadic.ContainsKey(Var$) Then
                                Dim pos As Integer
                                pos = VLine.IndexOf(Var$)
                                VLine = VLine.Substring(pos)
                                VLine = VLine.Replace(Var$ + ",", "")
                                VLine = VLine.Replace("CUSTOM,", "")
                                VLine = VLine.Replace("MEDIUM,", "")
                                VLine = VLine.Replace("HIGH0010,", "")
                                VLine = VLine.Replace("LOW0010,", "")
                                VLine = VLine.Replace("LOW0020,", "")

                                'if Variable only has 2 dimension
                                If Not ifsVarDyadicDim3.ContainsKey(Var$) Then
                                    Dim1Count = CShort(NR)
                                    Dim2Count = CShort(NR)
                                    Maximum = Dim1Count

                                    P1 = X(xIdx).Replace("""", "").Trim
                                    'jrs 2023/08/15
                                    Dim pos2, pos3 As Integer
                                    If VLine.Substring(0, 1) = """" Then
                                        pos2 = VLine.IndexOf(",")
                                        pos3 = VLine.Substring(1).IndexOf("""")
                                        If pos2 < pos3 Then
                                            xIdx = xIdx + 1
                                            P1 = P1 & ", " & X(xIdx).Replace("""", "").Trim
                                        End If
                                    End If
                                    xIdx = xIdx + 1
                                    P2 = X(xIdx).Replace("""", "").Trim
                                    'jrs 2023/08/15
                                    If X(xIdx).Substring(0, 1) = """" Then
                                        pos2 = X(xIdx).Substring(1).IndexOf("""")
                                        If pos2 = -1 Then
                                            xIdx = xIdx + 1
                                            P2 = P2 & ", " & X(xIdx).Replace("""", "").Trim
                                        End If
                                    End If
                                    xIdx = xIdx + 1

                                    If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical
                                        VLine = ReplaceFirst(VLine, """" + P1 + """,", "")
                                        VLine = ReplaceFirst(VLine, P1 + ",", "")

                                        StringHold1$ = P1
                                        For eleloop = 1 To Dim1Count 'Up to number of elements on dimension
                                            If StringHold1$ = "Total" And GRPS(1, Dim1Count) <> "Total" Then
                                                P1 = "1"
                                                P1Val = 1
                                                Exit For
                                            End If

                                            If StringHold1$.ToUpper = GRPS$(1, eleloop).ToUpper Then
                                                P1 = eleloop.ToString
                                                P1Val = eleloop
                                                Exit For
                                            End If
                                            If GRPS(1, eleloop).Contains(",") Then
                                                If StringHold1$.ToUpper = GRPS(1, eleloop).Substring(0, GRPS(1, eleloop).IndexOf(",")).ToUpper Then
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    VLine = VLine.Substring(P1.Length + 1)
                                                    StringHold1 = StringHold1 + ", " + P2
                                                    If StringHold1$.ToUpper = GRPS$(1, eleloop).ToUpper Then
                                                        P2 = P1
                                                        P1 = eleloop.ToString
                                                        P1Val = eleloop
                                                        VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                        'jrs 2023/08/15
                                                        VLine = VLine.Replace(StringHold1 + ",", "")
                                                        Exit For
                                                    Else
                                                        StringHold1 = StringHold1.Replace(", " + P2, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= Dim1Count Then 'Not Found
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then
                                                    P1Val = gCountryGroupIdx(1)
                                                    P1 = P1Val.ToString
                                                Else 'jrs 2010/10/12, maybe the group has a comma
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    VLine = VLine.Substring(P1.Length + 1)
                                                    StringHold1$ = StringHold1$ & ", " & P2
                                                    P2 = P1
                                                    gLoadCountries(StringHold1$)
                                                    If gGroupTot > 0 Then
                                                        P1Val = gCountryGroupIdx(1)
                                                        P1 = P1Val.ToString
                                                    Else
                                                        P1 = StringHold1$
                                                    End If
                                                End If
                                                If Not Integer.TryParse(P1, P1Val) Then
                                                    msg$ = StringHold1$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry."
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                End If

                                            End If
                                        Next eleloop
                                    Else
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P1.Length + 1)
                                    End If
                                    DimMax = Dim2Count + 1
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P2.Length + 1)
                                    If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                        StringHold2$ = P2
                                        For eleloop = 1 To Dim2Count 'Up to number of elements on dimension
                                            If StringHold2$ = "Total" And GRPS(1, Dim2Count) <> "Total" Then
                                                P2Val = 1
                                                P2 = "1"
                                                Exit For
                                            End If

                                            If StringHold2$.ToUpper = GRPS$(1, eleloop).ToUpper Then
                                                P2Val = eleloop
                                                P2 = P2Val.ToString
                                                Exit For
                                            End If
                                            DimNo = 1
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P2 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    StringHold2 = StringHold2 + ", " + P2
                                                    If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P2Val = eleloop
                                                        P2 = P2Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                        'jrs 2023/08/15
                                                        VLine = VLine.Replace(StringHold2 + ",", "")
                                                        Exit For
                                                    Else
                                                        StringHold2 = StringHold2.Replace(", " + P2, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If
                                            If eleloop >= Dim2Count Then 'jrs 2023/08/15 DimMax Then 'Not Found
                                                gLoadCountries(StringHold2$)
                                                If gGroupTot > 0 Then
                                                    P2Val = gCountryGroupIdx(1)
                                                    P2 = P2Val.ToString
                                                Else
                                                    msg$ = StringHold2$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                End If
                                            End If
                                        Next eleloop
                                        'JRS 09/04/03
                                    Else
                                        P2Val = CInt(P2)
                                        StringHold2$ = GRPS(1, P2Val)  'subdim name
                                    End If

                                    If X(xIdx).Replace("""", "").Contains(":") Then
                                        nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                    Else
                                        'jrs 2024/02/27
                                        If Lag < 0 Then
                                            'jrs 2026/01/19
                                            If X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Contains(":") Then
                                                nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Substring(0, X(Min(xIdx - Lag, UBound(X))).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                            Else
                                                nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", ""))
                                            End If
                                        Else
                                            nvalue = CSng(X(xIdx).Replace("""", ""))
                                        End If
                                    End If
                                    VLine2 = VLine

                                    'jrs 2024/02/27
                                    For iYear% = StartYear - Lag To RTime
                                        If iYear% > 1 Then  'time series plus advanced year
                                            'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then 'time series plus advanced year
                                            'should go in even in the first year when there is a time offset, e.g., hist rebuild with first year layter than 1960
                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            VLine = VLine2
                                            If iYear = 2 Then
                                                prevvalue = -9999
                                            Else
                                                Call ParseLine(VLine$, iYear% - 1, RTime, prevvalue)
                                            End If
                                        End If

                                        'JRS 09/03/03 BEGIN **************************************
                                        If gGroupTot > 0 Then

                                            myValue2_1 = ifsVarDyadic(Var)

                                            If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            End If
											
											'gvdb 2024/04/05
            								' unused variable
                                            'Dim CtryIdx2 As Integer
                                            'jrs 2008/05/05

                                            'jrs 2005/04/29
                                            'jrs 2018/01/06
                                            sAgg2_1 = "GDP"

                                            factor2_1 = nvalue - myValue2_1

                                            If P2Val <> DimMax And factor2_1 <> 0 Then 'jrs 2014/03/07 was using Dim2Max, but that doesn't exist
                                                'Save values in group members
                                                If iYear > 1 And nvalue <> prevvalue Then
                                                    setGroupValueBilateral(Var, StringHold1$, StringHold2$, 0, factor2_1, iYear, context, ifsVarDyadic(Var))
                                                End If
                                            End If
                                            'JRS 09/04/03 nvalue = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(2, IYear)
                                            'nvalue = Val(GroupLines(1))

                                        End If 'eof If gGroupTot > 0
                                        'JRS 09/03/03 END ****************************************

                                        Dim1Max = Dim1Count + 1 'to handle "All" in dimensionality
                                        Dim2Max = Dim2Count + 1

                                        'JRS 09/04/03
                                        If gGroupTot = 0 Then

                                            'Save bilateral trade parameters
                                            If iYear > 1 And nvalue <> prevvalue Then
                                                Using db As IFsForDyadicContext = New IFsForDyadicContext()
                                                    Dim conn = db.Database.GetDbConnection
                                                    If conn.State = 0 Then 'closed
                                                        conn.Open()
                                                    End If

                                                    'jrs 2019/11/18 *** begin ***
                                                    Using cmd2 = conn.CreateCommand()
                                                        cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                                                        PRAGMA ignore_check_constraints = ON;
                                                        PRAGMA journal_mode = MEMORY;
                                                        PRAGMA synchronous = OFF;"
                                                        cmd2.ExecuteNonQuery()
                                                    End Using
                                                    db.Database.AutoTransactionsEnabled = False
                                                    db.ChangeTracker.AutoDetectChangesEnabled = False
                                                    db.ChangeTracker.LazyLoadingEnabled = False
                                                    'jrs 2019/11/18 *** end ***

                                                    db.SingleInsertParametersDetail(nvalue, P1Val, P2Val, Max(iYear + Lag, 1) + FirstYear - 1, Var, 0)
                                                End Using
                                            End If
                                        End If
                                        'JRS 12/06/01 GROUPING
                                    Next

                                    gGroupTot = 0

                                Else 'if Variable has 3 dimensions 2019/12/17

                                    Dim1Count = CShort(NR)
                                    Dim2Count = CShort(NR)
                                    Dim3Count = CShort(context.GetDimension(ifsVarDyadicDim3(Var)).Buckets.Count)

                                    Maximum = Dim1Count

                                    P1 = X(xIdx).Replace("""", "").Trim
                                    'jrs 2023/08/15
                                    Dim pos2, pos3 As Integer
                                    If VLine.Substring(0, 1) = """" Then
                                        pos2 = VLine.IndexOf(",")
                                        pos3 = VLine.Substring(1).IndexOf("""")
                                        If pos2 < pos3 Then
                                            xIdx = xIdx + 1
                                            P1 = P1 & ", " & X(xIdx).Replace("""", "").Trim
                                        End If
                                    End If

                                    xIdx = xIdx + 1
                                    P2 = X(xIdx).Replace("""", "").Trim
                                    'jrs 2023/08/15
                                    If X(xIdx).Substring(0, 1) = """" Then
                                        pos2 = X(xIdx).Substring(1).IndexOf("""")
                                        If pos2 = -1 Then
                                            xIdx = xIdx + 1
                                            P2 = P2 & ", " & X(xIdx).Replace("""", "").Trim
                                        End If
                                    End If


                                    xIdx = xIdx + 1
                                    P3 = X(xIdx).Replace("""", "").Trim
                                    xIdx = xIdx + 1

                                    Dim1Max = Dim1Count + 1
                                    Dim2Max = Dim2Count + 1
                                    Dim3Max = Dim3Count + 1

                                    Dim eleloop As Integer

                                    If Not Integer.TryParse(P1, P1Val) Then 'is string and must determine numerical
                                        'jrs 2023/08/15
                                        'Check for repeated dimensions, like Africa, Africa in dyadic trade
                                        Dim bRep As Boolean = False
                                        bRep = VLine.Contains("""" + P1 + """," + """" + P1 + """,")
                                        VLine = VLine.Replace("""" + P1 + """,", "")
                                        If bRep Then
                                            VLine = """" + P1 + """," + VLine
                                        End If
                                        'no double quotes
                                        'jrs 2023/08/15
                                        'Check for repeated dimensions, like Africa, Africa in dyadic trade
                                        bRep = VLine.Contains(P1 + "," + P1 + ",")
                                        'jrs 2023/10/27 OECD,non-OECD is problematic, is removing OECD from both leaving just non-
                                        If bRep Then
                                            VLine = VLine.Replace(P1 + ",", "")
                                            VLine = P1 + "," + VLine
                                        Else
                                            If P1 = VLine.Substring(0, P1.Length) Then
                                                VLine = VLine.Substring(P1.Length + 1)
                                            End If
                                        End If

                                        DimMax = Dim1Count + 1
                                        DimNo = 1
                                        StringHold1$ = P1
                                        For eleloop = 1 To Dim1Count 'Up to number of elements on dimension
                                            If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                P1Val = eleloop
                                                P1 = P1Val.ToString
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold1$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P1 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    'jrs 2023/08/15 use P2 instead of P1 to remove
                                                    VLine = VLine.Substring(P2.Length + 1)
                                                    StringHold1 = StringHold1 + ", " + P2
                                                    If StringHold1$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        VLine = VLine.Replace("""" + StringHold1 + """,", "")
                                                        P2 = P3
                                                        P3 = P1
                                                        'jrs 2023/08/15
                                                        P1Val = eleloop
                                                        P1 = P1Val.ToString

                                                        Exit For
                                                    Else
                                                        StringHold1 = StringHold1.Replace(", " + P2, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If
                                        Next eleloop

                                        If eleloop >= Dim1Count Then 'jrs 2023/08/15 DimMax Then 'Not Found

                                            'JRS 12/08/01 GROUPING
                                            gLoadCountries(StringHold1$)
                                            If gGroupTot > 0 Then P1Val = gCountryGroupIdx(1) 'jrs 2014/11/07 P1 = gRegionIdx(gCountryGroup(1))
                                            P1 = P1Val.ToString
                                            If Not Integer.TryParse(P1, P1Val) Then
                                                msg$ = StringHold1$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry."
                                                'jrs 2008/04/02
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If

                                        End If

                                    Else
                                        'Remove blanks jrs 2020/01/10
                                        Do While VLine.Substring(0, 1) = " "
                                            VLine = VLine.Substring(1)
                                        Loop
                                        VLine = VLine.Substring(P1.Length + 1)
                                    End If
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P2.Length + 1)
                                    If Not Integer.TryParse(P2, P2Val) Then  'is string and must determine numerical
                                        DimMax = Dim2Count + 1
                                        DimNo = 1
                                        StringHold2$ = P2
                                        For eleloop = 1 To Dim2Count 'Up to number of elements on dimension
                                            'jrs 2014/03/07
                                            If StringHold2$ = "Total" And GRPS(DimNo, DimMax - 1) <> "Total" Then
                                                P2Val = 1
                                                P2 = P2Val.ToString
                                                Exit For
                                            End If

                                            If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                P2Val = eleloop
                                                P2 = P2Val.ToString
                                                Exit For
                                            End If
                                            'jrs 2005/06/11
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold2$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P2 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    'Remove blanks jrs 2020/01/10
                                                    Do While VLine.Substring(0, 1) = " "
                                                        VLine = VLine.Substring(1)
                                                    Loop
                                                    VLine = VLine.Substring(P2.Length + 1)

                                                    StringHold2 = StringHold2 + ", " + P3
                                                    If StringHold2$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P2Val = eleloop
                                                        P2 = P2Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                        P3 = P2
                                                        Exit For
                                                    Else
                                                        StringHold2 = StringHold2.Replace(", " + P3, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                        Next eleloop

                                        'JRS 09/04/03
                                        If eleloop >= Dim2Count Then 'jrs 2023/08/15 DimMax Then 'Not Found
                                            'jrs 2005/06/11
                                            gLoadCountries(StringHold2$)
                                            If gGroupTot > 0 Then
                                                'jrs 2014/11/07 P2 = gRegionIdx(gCountryGroup(1))
                                                P2Val = gCountryGroupIdx(1)
                                                P2 = P2Val.ToString
                                            Else
                                                msg$ = StringHold2$ + " not found among valid dimensions."
                                                msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                'jrs 2008/04/02
                                                If bShowMsg Then
                                                    Debug.Print(msg$)
                                                End If
                                            End If
                                        End If

                                    Else
                                        StringHold2$ = GRPS(1, P2Val)
                                    End If
                                    'Remove blanks jrs 2020/01/10
                                    Do While VLine.Substring(0, 1) = " "
                                        VLine = VLine.Substring(1)
                                    Loop
                                    VLine = VLine.Substring(P3.Length + 1)
                                    If Not Integer.TryParse(P3, P3Val) Then  'is string and must determine numerical
                                        DimMax = Dim3Count + 1
                                        DimNo = ifsVarDyadicDim3(Var)
                                        StringHold3$ = P3
                                        For eleloop = 1 To DimMax 'Up to number of elements on dimension
                                            If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                P3Val = eleloop
                                                P3 = P3Val.ToString
                                                Exit For
                                            End If
                                            If GRPS(DimNo, eleloop).Contains(",") Then
                                                If StringHold3$.ToUpper = GRPS(DimNo, eleloop).Substring(0, GRPS(DimNo, eleloop).IndexOf(",")).ToUpper Then
                                                    P3 = X(xIdx).Replace("""", "").Trim
                                                    xIdx = xIdx + 1
                                                    StringHold3 = StringHold3 + ", " + P3
                                                    If StringHold3$.ToUpper = GRPS$(DimNo, eleloop).ToUpper Then
                                                        P3Val = eleloop
                                                        P3 = P3Val.ToString
                                                        VLine = VLine.Replace("""" + StringHold2 + """,", "")
                                                        Exit For
                                                    Else
                                                        StringHold3 = StringHold3.Replace(", " + P3, "")
                                                        xIdx = xIdx - 1
                                                    End If
                                                End If
                                            End If

                                            If eleloop >= DimMax Then 'Not Found
                                                'jrs 2005/06/11
                                                gLoadCountries(StringHold1$)
                                                If gGroupTot > 0 Then
                                                    'jrs 2014/11/07 p3 = gRegionIdx(gCountryGroup(1))
                                                    P3Val = gCountryGroupIdx(1)
                                                    P3 = P3Val.ToString
                                                Else
                                                    msg$ = StringHold3$ + " not found among valid dimensions."
                                                    msg$ = msg$ + "  Will skip .dat entry but continue processing file."
                                                    'jrs 2008/04/02
                                                    If bShowMsg Then
                                                        Debug.Print(msg$)
                                                    End If
                                                End If
                                            End If
                                        Next eleloop
                                        'JRS 09/04/03
                                    Else
                                        StringHold3$ = GRPS(ifsVarDyadicDim3(Var), P3Val)
                                    End If

                                    If X(xIdx).Replace("""", "").Contains(":") Then
                                        nvalue = CSng(X(xIdx).Replace("""", "").Substring(0, X(xIdx).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                    Else
                                        'jrs 2024/02/27
                                        If Lag < 0 Then
                                            'jrs 2026/01/19
                                            If X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Contains(":") Then
                                                nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", "").Substring(0, X(Min(xIdx - Lag, UBound(X))).Replace("""", "").IndexOf(":"))) 'first and possibly only value (unless is series)
                                            Else
                                                nvalue = CSng(X(Min(xIdx - Lag, UBound(X))).Replace("""", ""))
                                            End If
                                        Else
                                            nvalue = CSng(X(xIdx).Replace("""", ""))
                                        End If
                                    End If

                                    VLine2 = VLine

                                    'jrs 2024/02/27
                                    For iYear% = StartYear - Lag To RTime
                                        If iYear% > 1 Then  'time series plus advanced year
                                            'If VType%(chk) = 3 And (iYear% > 1 Or SeriesOffset > 0) Then


                                            Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                            'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                            'mti 2019/05/14 End

                                            VLine = VLine2
                                            If iYear = 2 Then
                                                prevvalue = -9999
                                            Else
                                                'mti 2019/05/14 Start
                                                Call ParseLine(VLine$, iYear% - 1, RTime, prevvalue)
                                                'Call ParseLine(VLine$, iYear% - 1 + SeriesOffset, RTime, prevvalue)
                                                'mti 2019/05/14 End
                                            End If

                                        End If

                                        'JRS 09/03/03 BEGIN **************************************
                                        If gGroupTot > 0 Then

                                            'jrs 2017/01/06
                                            'jrs 10/28/04 myValue2 = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(1, IYear)
                                            'jrs 10/28/04
                                            myValue3_1 = ifsVarDyadic(Var)
                                            '                                        Else
                                            '                                            myValue3_1 = getGroupValueBilateral(chk, StringHold1$, StringHold2$, CInt(p3), dbG, rsDef, rsDet, N, iYear)
                                            '                                        End If

                                            'JRS 08/11/03 begin
                                            If iYear > 1 Then 'And Not (frm_MainMenu.IFsGUIctrl1.cSensitivityFlag) Then
                                                'mti 2019/05/14 Start
                                                Call ParseLine(VLine$, iYear%, RTime, nvalue)
                                                'Call ParseLine(VLine$, iYear% + SeriesOffset, RTime, nvalue)
                                                'mti 2019/05/14 End
                                            End If
											
											'gvdb 2024/04/05
            								' unused variable
                                            'Dim ctryIdx3 As Integer

                                            'jrs 2017/01/06
                                            sAgg3_1 = "GDP"
                                            '                                        Else
                                            '                                            sAgg3_1 = "GDP"
                                            '                                        End If

                                            factor3_1 = nvalue - myValue3_1

                                            If P2Val <> Dim2Max And factor3_1 <> 0 Then
                                                '                                                'Save values in group members
                                                If iYear > 1 And nvalue <> prevvalue Then
                                                    setGroupValueBilateral(Var, StringHold1$, StringHold2$, P3Val, factor3_1, iYear, context, ifsVarDyadic(Var))
                                                End If
                                            End If
                                            'JRS 09/04/03 nvalue = frm_MainMenu.IFsGUIctrl1.gRetrPlotVal(2, IYear)
                                            'nvalue = Val(GroupLines(1))
                                        End If
                                        'JRS 09/03/03 END ****************************************
                                        'jrs 2019/10/01 BEGIN TRANSLATION

                                        Dim1Max = Dim1Count + 1  'to handle "All" in dimensionality
                                        Dim2Max = Dim2Count + 1
                                        Dim3Max = Dim3Count + 1

                                        If gGroupTot = 0 Then
                                            'Save bilateral trade parameters
                                            If iYear > 1 And nvalue <> prevvalue Then
                                                Using db As IFsForDyadicContext = New IFsForDyadicContext
                                                    'jrs 2021/03/01
                                                    Dim conn = db.Database.GetDbConnection
                                                    If conn.State = 0 Then 'closed
                                                        conn.Open()
                                                    End If

                                                    db.SingleInsertParametersDetail(nvalue, P1Val, P2Val, iYear + FirstYear - 1, Var, P3Val)
                                                End Using
                                            End If
                                        End If

                                    Next iYear%

                                    gGroupTot = 0

                                End If

                            End If
                        End If

                        VLine = reader.ReadLine
                    Loop ' Wend

                End Using
                'jrs 2023/05/16 Next iYear%


            Catch ex As Exception
                'BadLine:
                '        msg$ = "There appears to be a bad or blank line in your SensitivityInput.dat or "
                '        msg$ = msg$ + "Working.sce file.  Processing will now cease.  Look at Line" _
                '+ " (where 0 suggests blank line): " _
                '+ Str$(FileSensitivityNumber) + Var$ + VLine$
                '        'jrs 2008/04/02
                '        If bShowMsg Then
                '            Debug.Print(msg$)
                '        End If
                '        BadDatFile = True
                'jrs 2019/09/27 Close

                Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "ErrorSce.txt", append:=True)
                objWriter.WriteLine(ex.Message)
                objWriter.Close()

            End Try


        End Sub '=============================Function2.Bas=======================

        Sub ParseLine(VLine$, iYear%, RTime As Integer, ByRef nvalue As Single)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            Dim LCount, mult As Integer
            LCount = 0

            Dim Value As String
            Dim FVAl As Single

            While VLine$.Length > 0  'Keep going as long as non-zero line
                Value$ = VLine$
                If VLine$.Contains(",") Then 'If commas in line
                    Value$ = VLine.Substring(0, VLine.IndexOf(","))
                    VLine$ = VLine.Substring(VLine.IndexOf(",") + 1)
                End If

                If Value$.Contains(":") Then  'Any repeats in current string?
                    Dim pos As Integer = Value$.IndexOf(":")
                    Dim pos2 As Integer = Value$.IndexOf(" ")
                    If pos2 > 0 And pos2 < pos Then
                        pos2 = Value$.IndexOf(" ", pos)
                    End If
                    If pos2 > 0 Then
                        mult = CInt(Value$.Substring(pos + 1, pos2 - pos - 1))
                    Else
                        mult = CInt(Value$.Substring(pos + 1))
                    End If
                    LCount = LCount + mult
                    nvalue = CSng(Value$.Substring(0, pos))
                ElseIf Value$.Contains("@") Then 'Any interpolations?
                    Dim pos As Integer = Value$.IndexOf("@")
                    Dim pos2 As Integer = Value$.IndexOf(" ")

                    If pos2 > 0 Then
                        mult = CInt(Value$.Substring(pos + 1, pos2 - pos - 1))
                    Else
                        mult = CInt(Value$.Substring(pos + 1))
                    End If
                    LCount = LCount + mult
                    nvalue = CSng(Value$.Substring(0, pos))
                    If iYear% < LCount Then 'Year falls within range for interp
                        nvalue = FVAl + (((nvalue - FVAl) / mult) * (mult - (LCount - iYear%)))
                    End If
                Else   'Then must only be single value
                    LCount = LCount + 1
                    nvalue = CSng(Value$)
                End If

                FVAl = nvalue
                If LCount >= iYear% Then  'Have we counted up to year yet?
                    VLine$ = ""
                End If
            End While 'Wend

            '    If LCount < RTime Then Line Input #1, nulvar$ 'Read and clear any remaining line

        End Sub

        Public Sub gLoadCountries(GroupName As String)

            Dim iTotal As Integer
            iTotal = 0

            If IFsGroups.ContainsKey(GroupName) Then
                Dim lst As List(Of Integer) = IFsGroups(GroupName)
                ReDim gCountryGroup(lst.Count), gCountryGroupIdx(lst.Count)

                '   Load Group Member Names List Box
                If lst.Count > 0 Then
                    For Each item In lst
                        iTotal = iTotal + 1
                        gCountryGroup(iTotal) = GRPS(1, item)
                        'JRS 09/09/02
                        gCountryGroupIdx(iTotal) = item
                    Next
                End If
            End If
            'jrs 2012/05/11 dbG.Close
            gGroupTot = iTotal

        End Sub

        'jrs 2008/04/11
        'READ: This code is not considering changes starting in the first year.
        'it only considers flat lines
        'or progressive values that start at the base value
        Private Sub FindCommonTargetVars(ByRef TgYr As Integer, ByRef TgVal As Single,
        ByRef TgFstYr As Integer, sLine As String)
            'Procedure to find common target scenario parameters
            '1. TgYr is the target year where we want the group to converge
            '2. TgVal is the target value at which the group will converge
            '3. TgFstYr is the first year where the groups starts to move towards convergence
            '4. sLine is the string to analyze

            Dim xLine() As String
            xLine = sLine.Split(New Char() {","c})

            Dim i As Integer
            Dim AbsChg1 As Single, AbsChg2 As Single
            'jrs 2012/02/04
            Dim sLineVal As String, sLineYrs As String, iYrCount As Integer, PrevVal As String
            For i = 0 To xLine.Length - 2 'jrs 2023/11/14 changed from -1 to -2 to avoid blow ups when the scenario is flat and has to go till the end
                'jrs 2012/02/04
                If xLine(i + 1).Contains("@") Then 'Interpolation
                    sLineVal = xLine(i + 1).Substring(0, xLine(i + 1).IndexOf("@"))
                    sLineYrs = xLine(i + 1).Substring(xLine(i + 1).IndexOf("@") + 1)
                    If AbsChg1 = 0 Then
                        TgFstYr = iYrCount + 2
                        TgYr = CInt(sLineYrs) + iYrCount + 1
                        TgVal = CSng(sLineVal)
                    Else
                        TgYr = iYrCount + 1
                        TgVal = CSng(PrevVal)
                    End If
                    AbsChg1 = 1 'doesn't matter the value
                    Exit For
                ElseIf xLine(i + 1).Contains(":") Then 'Constant
                    sLineVal = xLine(i + 1).Substring(0, xLine(i + 1).IndexOf(":"))
                    sLineYrs = xLine(i + 1).Substring(xLine(i + 1).IndexOf(":") + 1)
                    If AbsChg1 = 0 Then
                        iYrCount = iYrCount + CInt(sLineYrs)
                        PrevVal = sLineVal
                    Else
                        TgYr = iYrCount + 1
                        TgVal = CSng(PrevVal)
                        AbsChg1 = 1 'doesn't matter the value
                        Exit For
                    End If
                Else
                    AbsChg2 = CSng(xLine(i + 1)) - CSng(xLine(i))
                    PrevVal = xLine(i + 1)
                    iYrCount = iYrCount + 1
                End If
                If Abs(AbsChg2 - AbsChg1) > 0.001 Then '0.0001 was showing a change in slope when using currency qe, NAFTA, high to 40000 in 10 years
                    If AbsChg1 = 0 Then
                        AbsChg1 = AbsChg2
                        TgFstYr = iYrCount + 1
                    Else
                        TgYr = iYrCount
                        TgVal = CSng(xLine(i))
                        Exit For
                    End If
                End If
            Next i

            'if it's all constant
            If AbsChg1 = 0 Then
                TgFstYr = 1
                TgYr = 1
                'jrs 2012/08/01
                If Single.TryParse(xLine(0), TgVal) Then
                    'TgVal = xLine(0)
                ElseIf xLine(0).Contains(":") Then
                    TgVal = CSng(xLine(0).Substring(0, xLine(0).IndexOf(":")))
                End If
            End If

            'if it ends in constant change
            If TgYr = 0 Then
                TgYr = xLine.Length
                TgVal = CSng(xLine(xLine.Length - 1))
            End If
        End Sub

        'JRS 11/14/03 Calculates aggregate value
        Private Function getGroupValue(ByRef currVar As IVariable, ByRef N As IDataframe, Year As Short, ByRef context As IContext,
                                       ByRef Nweight As IDataframe, Optional Dim2Idx As Short = 0, Optional Dim3Idx As Short = 0) As Single 'use chk
            Dim sAggregation As String
            sAggregation = currVar.Aggregation

            Dim GroupValue As Single
            Dim GroupValGDP As Single
            Dim GroupValPOP As Single
            GroupValue = 0
            GroupValGDP = 0
            GroupValPOP = 0
            'Read auxiliary pointers
            'Dim idxGDP As Integer
            'Dim idxPOP As Integer
            'jrs 2010/03/31
            Dim iAgIdx As Integer
            Dim GroupValAux As Single
            GroupValAux = 0
            Dim sAgVar As String

            If sAggregation = "GDP" Then
                'Call FindVarNumb(idxGDP, "GDP")
            ElseIf sAggregation = "POP" Then
                'Call FindVarNumb(idxPOP, "POP")
            ElseIf sAggregation > "" Then 'jrs 2010/03/31
                If IFsAggRules.ContainsKey(sAggregation) Then
                    sAgVar = IFsAggRules(sAggregation)
                End If
                'jrs 2010/04/01
                If sAgVar > "" Then
                    'Call FindVarNumb(iAgIdx, sAgVar)
                    'jrs 2025/03/10 just to take the weights
                    iAgIdx = 1
                End If
            End If

            Dim weightVal As Single
            'loop for all countries
            Dim CtryIdx As Short
            Dim RegIdx As Short
            'jrs 10/28/04 add second dimension pointers
            'If dim2idx = 0 Then
            '    dim2idx = 1
            'End If
            'jrs 2006/05/31 dim3
            'If Dim3Idx = 0 Then
            '    Dim3Idx = 1
            'End If
            'jrs 2005/05/02
            'I changed MaxVal(1)- 1, and replaced it with MAXCOM(1) - 1, for the current set of data
            'the first one was 182, and the second one was 184
            Dim dfIdx, dfWeightIdx As Integer
            Dim Dim1Count As Short, Dim2Count As Short, Dim3Count As Short
            Select Case currVar.Dimensions.Count
                Case 2
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                Case 3
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                Case 4
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                    Dim3Count = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)
            End Select
            For CtryIdx = 1 To CShort(gGroupTot)
                'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(CtryIdx))
                RegIdx = CShort(gCountryGroupIdx(CtryIdx))
                If Not Nweight Is Nothing AndAlso Nweight.RowCount > Dim1Count Then
                    dfWeightIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count)
                Else
                    dfWeightIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, RegIdx, Dim1Count)
                End If

                Select Case currVar.Dimensions.Count
                    Case 1
                        dfIdx = getDataFrameIdxFromDims(Year, FirstYear)
                    Case 2
                        dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count)
                    Case 3
                        dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count, Dim2Idx, Dim2Count)
                    Case 4
                        dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count, Dim2Idx, Dim2Count, Dim3Idx, Dim3Count)
                End Select
                Select Case sAggregation
                    Case "SUM", "AVG"
                        GroupValue = GroupValue + CSng(N(dfIdx))
                    Case "GDP"
                        weightVal = CSng(Nweight(dfWeightIdx))
                        GroupValue = GroupValue + weightVal * CSng(N(dfIdx))
                        GroupValGDP = GroupValGDP + weightVal
                    Case "POP"
                        weightVal = CSng(Nweight(dfWeightIdx))
                        GroupValue = GroupValue + weightVal * CSng(N(dfIdx))
                        GroupValPOP = GroupValPOP + weightVal
                    Case "MAX"
                        If GroupValue < N(dfIdx) Then
                            GroupValue = CSng(N(dfIdx))
                        End If
                    Case "MIN"
                        If GroupValue > N(dfIdx) Then
                            GroupValue = CSng(N(dfIdx))
                        End If
                    Case Else 'jrs 2010/03/31
                        If iAgIdx > 0 Then
                            weightVal = CSng(Nweight(dfWeightIdx))
                            GroupValue = GroupValue + weightVal * CSng(N(dfIdx))
                            GroupValAux = GroupValAux + weightVal
                        End If
                End Select
            Next CtryIdx
            If sAggregation = "AVG" Then
                GroupValue = GroupValue / gGroupTot
            ElseIf sAggregation = "GDP" Then
                GroupValue = GroupValue / GroupValGDP
            ElseIf sAggregation = "POP" Then
                GroupValue = GroupValue / GroupValPOP
            ElseIf iAgIdx > 0 Then 'jrs 2010/03/31
                GroupValue = GroupValue / GroupValAux
            End If
            getGroupValue = GroupValue
        End Function

        'jrs 2019/12/16
        Private Function getGroupXGroupValue(ByRef currVar As IVariable, ByRef N As IDataframe, Year As Short, ByRef context As IContext,
                                       ByRef Nweight As IDataframe, ByVal sDim1 As String, ByVal sDim2 As String, ByVal Dim3Idx As Integer) As Single 'use chk
            Dim sAggregation As String
            sAggregation = currVar.Aggregation

            Dim GroupValue As Single
            Dim GroupValGDP As Single
            Dim GroupValPOP As Single
            GroupValue = 0
            GroupValGDP = 0
            GroupValPOP = 0
            'Read auxiliary pointers
            'Dim idxGDP As Integer
            'Dim idxPOP As Integer
            'jrs 2010/03/31
            Dim iAgIdx As Integer
            Dim GroupValAux As Single
            GroupValAux = 0
            Dim sAgVar As String

            If sAggregation = "GDP" Then
                'Call FindVarNumb(idxGDP, "GDP")
            ElseIf sAggregation = "POP" Then
                'Call FindVarNumb(idxPOP, "POP")
            ElseIf sAggregation > "" Then 'jrs 2010/03/31
                If IFsAggRules.ContainsKey(sAggregation) Then
                    sAgVar = IFsAggRules(sAggregation)
                End If
                'jrs 2010/04/01
                If sAgVar > "" Then
                    'Call FindVarNumb(iAgIdx, sAgVar)
                End If
            End If

            Dim weightVal As Single
            'loop for all countries
            'gvdb 2024/04/05
            ' unused variable
            'Dim CtryIdx As Short
            Dim RegIdx As Short
            'jrs 10/28/04 add second dimension pointers
            'If dim2idx = 0 Then
            '    dim2idx = 1
            'End If
            'jrs 2006/05/31 dim3
            'If Dim3Idx = 0 Then
            '    Dim3Idx = 1
            'End If
            'jrs 2005/05/02
            'I changed MaxVal(1)- 1, and replaced it with MAXCOM(1) - 1, for the current set of data
            'the first one was 182, and the second one was 184
            Dim dfIdx, dfWeightIdx As Integer
            Dim Dim1Count As Short, Dim2Count As Short, Dim3Count As Short
            Select Case currVar.Dimensions.Count
                Case 2
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                Case 3
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                Case 4
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                    Dim3Count = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)
            End Select

            gLoadCountries(sDim1)
            Dim Dim1Tot As Integer
            Dim gCountryGroupIdx1 As Integer()
            If gGroupTot > 0 Then
                Dim1Tot = gGroupTot
                gCountryGroupIdx1 = gCountryGroupIdx.ToArray
            Else
                Dim1Tot = 1
                ReDim gCountryGroupIdx1(1)
                gCountryGroupIdx1(1) = 1
            End If

            gLoadCountries(sDim2)
            Dim Dim2Tot As Integer
            Dim gCountryGroupIdx2 As Integer()
            If gGroupTot > 0 Then
                Dim2Tot = gGroupTot
                gCountryGroupIdx2 = gCountryGroupIdx.ToArray
            Else
                Dim2Tot = 1
                ReDim gCountryGroupIdx2(1)
                gCountryGroupIdx2(1) = 1
            End If

            Dim i As Integer, j As Integer, Dim2idx As Integer

            If Dim3Idx = 0 Then
                Dim3Idx = 1
            End If
            'jrs 2005/05/02
            'I changed MaxVal(1)- 1, and replaced it with MAXCOM(1) - 1, for the current set of data
            'the first one was 182, and the second one was 184

            For i = 1 To Dim1Tot
                For j = 1 To Dim2Tot
                    'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(CtryIdx))
                    RegIdx = CShort(gCountryGroupIdx1(i))
                    Dim2idx = CShort(gCountryGroupIdx2(j))
                    If Not Nweight Is Nothing AndAlso Nweight.RowCount > Dim1Count Then
                        dfWeightIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count)
                    Else
                        dfWeightIdx = getDataFrameIdxFromDims(FirstYear, FirstYear, RegIdx, Dim1Count)
                    End If

                    dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count, CShort(Dim2idx), Dim2Count)

                    Select Case sAggregation
                        Case "SUM", "AVG"
                            GroupValue = GroupValue + CSng(N(dfIdx))
                        Case "GDP"
                            weightVal = CSng(Nweight(dfWeightIdx))
                            GroupValue = GroupValue + weightVal * CSng(N(dfIdx))
                            GroupValGDP = GroupValGDP + weightVal
                        Case "POP"
                            weightVal = CSng(Nweight(dfWeightIdx))
                            GroupValue = GroupValue + weightVal * CSng(N(dfIdx))
                            GroupValPOP = GroupValPOP + weightVal
                        Case "MAX"
                            If GroupValue < N(dfIdx) Then
                                GroupValue = CSng(N(dfIdx))
                            End If
                        Case "MIN"
                            If GroupValue > N(dfIdx) Then
                                GroupValue = CSng(N(dfIdx))
                            End If
                        Case Else 'jrs 2010/03/31
                            If iAgIdx > 0 Then
                                weightVal = CSng(Nweight(dfWeightIdx))
                                GroupValue = GroupValue + weightVal * CSng(N(dfIdx))
                                GroupValAux = GroupValAux + weightVal
                            End If
                    End Select
                Next j
            Next i
            If sAggregation = "AVG" Then
                'jrs 2025/08/11 GroupValue = GroupValue / gGroupTot
                GroupValue = GroupValue / (Dim1Tot * Dim2Tot)
            ElseIf sAggregation = "GDP" Then
                GroupValue = GroupValue / GroupValGDP
            ElseIf sAggregation = "POP" Then
                GroupValue = GroupValue / GroupValPOP
            ElseIf iAgIdx > 0 Then 'jrs 2010/03/31
                GroupValue = GroupValue / GroupValAux
            End If
            getGroupXGroupValue = GroupValue
        End Function

        'jrs 2019/12/13
        Private Function getSingleValue(ByRef currVar As IVariable, ByRef N As IDataframe, Year As Short, ByRef context As IContext,
                                       RegIdx As Short, Optional Dim2Idx As Short = 0, Optional Dim3Idx As Short = 0) As Single 'use chk

            Dim SingleValue As Single
            SingleValue = 0

            Dim dfIdx As Integer
            Dim Dim1Count As Short, Dim2Count As Short, Dim3Count As Short
            Select Case currVar.Dimensions.Count
                Case 2
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                Case 3
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                Case 4
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                    Dim3Count = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)
            End Select

            Select Case currVar.Dimensions.Count
                Case 1
                    dfIdx = getDataFrameIdxFromDims(Year, FirstYear)
                Case 2
                    dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count)
                Case 3
                    dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count, Dim2Idx, Dim2Count)
                Case 4
                    dfIdx = getDataFrameIdxFromDims(Year, FirstYear, RegIdx, Dim1Count, Dim2Idx, Dim2Count, Dim3Idx, Dim3Count)
            End Select
            SingleValue = CSng(N(dfIdx))

            getSingleValue = SingleValue
        End Function

        Public Function getHistAverage(DatTbl As IDictionary(Of String, Single?()), ByRef Yrs As Short(), ByVal sCtry As String, YrStrt%, YrBkUp%, Optional NoYrs% = 0) As Single?
            'mti 2019/11/01 Take entire Function


            Dim NYear%
            getHistAverage = 0
            NYear = 0
            Dim DatVal As Single?

            Dim yrIdx As Integer = 0
            For Each yr In Yrs
                DatVal = Nothing
                If yr > YrStrt% - YrBkUp% And yr <= YrStrt% Then
                    DatVal = DatTbl(sCtry)(yrIdx)
                    If DatVal.HasValue Then
                        getHistAverage = getHistAverage + DatVal
                        NYear = NYear + 1
                    End If
                End If
                yrIdx = yrIdx + 1
            Next

            If NYear > 1 Then
                getHistAverage = getHistAverage / NYear
                NoYrs% = NYear
            Else
                getHistAverage = Nothing
            End If

        End Function

        'jrs 2019/12/16
        Private Sub UpdateGroupbyGroup(ByRef currVar As IVariable, ByRef N As Dataframe, Year As Short, ByRef context As IContext,
                                       ByVal sDim1 As String, ByVal sDim2 As String, ByVal Dim3Idx As Integer, ByVal factor2_1 As Single)
            Dim sAggregation As String
            sAggregation = currVar.Aggregation

            Dim GroupValue As Single
            Dim GroupValGDP As Single
            Dim GroupValPOP As Single
            GroupValue = 0
            GroupValGDP = 0
            GroupValPOP = 0
            'Read auxiliary pointers
            'Dim idxGDP As Integer
            'Dim idxPOP As Integer
            'jrs 2010/03/31
            'gvdb 2024/04/05
            ' unused variable
            'Dim iAgIdx As Integer
            Dim GroupValAux As Single
            GroupValAux = 0
            Dim sAgVar As String

            If sAggregation = "GDP" Then
                'Call FindVarNumb(idxGDP, "GDP")
            ElseIf sAggregation = "POP" Then
                'Call FindVarNumb(idxPOP, "POP")
            ElseIf sAggregation > "" Then 'jrs 2010/03/31
                If IFsAggRules.ContainsKey(sAggregation) Then
                    sAgVar = IFsAggRules(sAggregation)
                End If
                'jrs 2010/04/01
                If sAgVar > "" Then
                    'Call FindVarNumb(iAgIdx, sAgVar)
                End If
            End If
			
			'gvdb 2024/04/05
            ' unused variable
            'Dim weightVal As Single
            'loop for all countries
            'Dim CtryIdx As Short
            Dim RegIdx As Short
            'jrs 10/28/04 add second dimension pointers
            'If dim2idx = 0 Then
            '    dim2idx = 1
            'End If
            'jrs 2006/05/31 dim3
            'If Dim3Idx = 0 Then
            '    Dim3Idx = 1
            'End If
            'jrs 2005/05/02
            'I changed MaxVal(1)- 1, and replaced it with MAXCOM(1) - 1, for the current set of data
            'the first one was 182, and the second one was 184
            Dim dfIdx As Integer
            Dim Dim1Count As Short, Dim2Count As Short, Dim3Count As Short
            Select Case currVar.Dimensions.Count
                Case 2
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                Case 3
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                Case 4
                    Dim1Count = CShort(context.GetDimension(currVar.Dimensions(1)).Buckets.Count)
                    Dim2Count = CShort(context.GetDimension(currVar.Dimensions(2)).Buckets.Count)
                    Dim3Count = CShort(context.GetDimension(currVar.Dimensions(3)).Buckets.Count)
            End Select

            gLoadCountries(sDim1)
            Dim Dim1Tot As Integer
            Dim gCountryGroupIdx1 As Integer()
            If gGroupTot > 0 Then
                Dim1Tot = gGroupTot
                gCountryGroupIdx1 = gCountryGroupIdx.ToArray
            Else
                Dim1Tot = 1
                ReDim gCountryGroupIdx1(1)
                gCountryGroupIdx1(1) = 1
            End If

            gLoadCountries(sDim2)
            Dim Dim2Tot As Integer
            Dim gCountryGroupIdx2 As Integer()
            If gGroupTot > 0 Then
                Dim2Tot = gGroupTot
                gCountryGroupIdx2 = gCountryGroupIdx.ToArray
            Else
                Dim2Tot = 1
                ReDim gCountryGroupIdx2(1)
                gCountryGroupIdx2(1) = 1
            End If

            Dim i As Integer, j As Integer, Dim2idx As Integer

            If Dim3Idx = 0 Then
                Dim3Idx = 1
            End If
            'jrs 2005/05/02
            'I changed MaxVal(1)- 1, and replaced it with MAXCOM(1) - 1, for the current set of data
            'the first one was 182, and the second one was 184

            For i = 1 To Dim1Tot
                For j = 1 To Dim2Tot
                    'jrs 2014/11/07 RegIdx = gRegionIdx(gCountryGroup(CtryIdx))
                    RegIdx = CShort(gCountryGroupIdx1(i))
                    Dim2idx = CShort(gCountryGroupIdx2(j))

                    dfIdx = getDataFrameIdxFromDims(Year, FirstYear, CShort(i), Dim1Count, CShort(j), Dim2Count)

                    'jrs 2005/04/29
                    If sAggregation = "SUM" Then
                        N(dfIdx) = N(dfIdx) * factor2_1
                    Else
                        N(dfIdx) = N(dfIdx) + factor2_1
                    End If
                Next j
            Next i
        End Sub

        'jrs 2017/01/06
        Private Sub setGroupValueBilateral(ByVal sVar As String, ByVal sDim1 As String, ByVal sDim2 As String,
        ByVal Dim3Idx As Integer, factor As Single, iYear As Integer, context As IContext, DefaultValue As Single)


            Dim Val As Single
            'loop for all countries
            Dim CtryIdx As Integer, CtryIdx2 As Integer
            Dim RegIdx As Integer, RegIdx2 As Integer

            Dim bGroup1 As Boolean, bGroup2 As Boolean
            Dim GroupTot1 As Integer, GroupTot2 As Integer
            bGroup1 = IFsGroups.ContainsKey(sDim1)
            bGroup2 = IFsGroups.ContainsKey(sDim2)

            Dim GroupRegIdx1() As Integer, GroupRegIdx2() As Integer

            If bGroup1 And Not bGroup2 Then
                GroupTot1 = gGroupTot
                GroupTot2 = 1
                ReDim GroupRegIdx1(GroupTot1), GroupRegIdx2(GroupTot2)
                For CtryIdx = 1 To GroupTot1
                    GroupRegIdx1(CtryIdx) = gCountryGroupIdx(CtryIdx)
                Next CtryIdx
                GroupRegIdx2(1) = AMAX(getDimIdx(1, sDim2, False, context), 0)
            ElseIf Not bGroup1 And bGroup2 Then
                GroupTot1 = 1
                GroupTot2 = gGroupTot
                ReDim GroupRegIdx1(GroupTot1), GroupRegIdx2(GroupTot2)
                GroupRegIdx1(1) = AMAX(getDimIdx(1, sDim1, False, context), 0)
                For CtryIdx = 1 To GroupTot2
                    GroupRegIdx2(CtryIdx) = gCountryGroupIdx(CtryIdx)
                Next CtryIdx
            Else
                gLoadCountries(sDim1)
                GroupTot1 = gGroupTot
                ReDim GroupRegIdx1(GroupTot1)
                For CtryIdx = 1 To GroupTot1
                    GroupRegIdx1(CtryIdx) = gCountryGroupIdx(CtryIdx)
                Next CtryIdx
                gLoadCountries(sDim2)
                GroupTot2 = gGroupTot
                ReDim GroupRegIdx2(GroupTot2)
                For CtryIdx = 1 To GroupTot2
                    GroupRegIdx2(CtryIdx) = gCountryGroupIdx(CtryIdx)
                Next CtryIdx
            End If


            'jrs 2005/05/02
            'I changed MaxVal(1)- 1, and replaced it with MAXCOM(1) - 1, for the current set of data
            'the first one was 182, and the second one was 184
            Dim Results(,) As Single, ResultsIdx(), ResultsIdx2() As Integer
            ReDim Results(GroupTot1, GroupTot2), ResultsIdx(GroupTot1), ResultsIdx2(GroupTot2)
            For CtryIdx = 1 To GroupTot1
                For CtryIdx2 = 1 To GroupTot2
                    RegIdx = GroupRegIdx1(CtryIdx)
                    RegIdx2 = GroupRegIdx2(CtryIdx2)

                    Val = DefaultValue
                    '        rsDet.Seek "=", V(idx), RegIdx, RegIdx2, Dim3Idx
                    'Do While Not rsDet.NoMatch
                    '            If rsDet.Fields("Year") <= FirstYear + iYear - 1 Then
                    '                Val = DefaultValue
                    '            End If
                    '            rsDet.Seek "=", V(idx), RegIdx, RegIdx2, Dim3Idx
                    'Loop

                    Results(CtryIdx, CtryIdx2) = Val + factor
                    ResultsIdx(CtryIdx) = RegIdx
                    ResultsIdx2(CtryIdx2) = RegIdx2
                Next CtryIdx2
            Next CtryIdx

            Using db As IFsForDyadicContext = New IFsForDyadicContext
                Dim conn = db.Database.GetDbConnection
                If conn.State = 0 Then 'closed
                    conn.Open()
                End If

                'jrs 2019/11/18 *** begin ***
                Using cmd2 = conn.CreateCommand()
                    cmd2.CommandText = "PRAGMA foreign_keys = OFF;
                        PRAGMA ignore_check_constraints = ON;
                        PRAGMA journal_mode = MEMORY;
                        PRAGMA synchronous = OFF;"
                    cmd2.ExecuteNonQuery()
                End Using
                db.Database.AutoTransactionsEnabled = False
                db.ChangeTracker.AutoDetectChangesEnabled = False
                db.ChangeTracker.LazyLoadingEnabled = False
                'jrs 2019/11/18 *** end ***

                db.BulkInsertParametersDetail(Results, ResultsIdx, ResultsIdx2, GroupTot1, GroupTot2, iYear + FirstYear - 1, sVar, Dim3Idx)
            End Using

        End Sub

        Public Function getDimIdx(DimNum As Integer, sDimVal As String, bMsgFlag As Boolean, context As IContext) As Integer
            getDimIdx = SearchDimIdx(1, context.GetDimension(DimNum).Buckets.Count, sDimVal, DimNum)
        End Function

        Private Function SearchDimIdx(StartIdx As Integer, EndIdx As Integer, sDimVal As String, DimNum As Integer) As Integer
            Dim midIdx As Integer = CInt((StartIdx + EndIdx) / 2)
            SearchDimIdx = -1
            If GRPS(DimNum, midIdx) = sDimVal Then
                SearchDimIdx = midIdx
            ElseIf StartIdx >= EndIdx Then
                SearchDimIdx = -1
            ElseIf GRPS(DimNum, midIdx) > sDimVal Then
                SearchDimIdx = SearchDimIdx(1, midIdx - 1, sDimVal, DimNum)
            ElseIf GRPS(DimNum, midIdx) < sDimVal Then
                SearchDimIdx = SearchDimIdx(midIdx + 1, EndIdx, sDimVal, DimNum)
            End If
        End Function

        'mti 2018/05/22 new proc
        'Regionalize Variable (agg: sum) without saving it to database - three dimensions
        '=============================REGSUBS.BAS  =======================
        Sub RegSumTwoDimInMem(CVars As Dictionary(Of String, Dictionary(Of Integer, Single?)), DumTwo(,) As Single, Sec As Integer, VarName$, cntx As IRebuildFacade) '
            '-----------------------------------------------------------------
            'CVars() is the two dimensioned array holding the country specific data
            'DumTwo is the two dimensional output variable used to return values
            '   to the calling routine; normally the IFs variable name for display
            'Sec is the number of the second dimension element of DumTwo, (on which the function will work, one element only)
            'Varname$ is the name of the variable as a string value (for throwing message boxes)
            '   this particular call of the subroutine


            '   Initialize Sum by Region
            '   ------------------------
            Dim reg As Integer
            For reg% = 1 To NR
                DumTwo(reg%, Sec) = 0
            Next reg%

            '   Sum by Region
            '   -------------
            Dim ValHold As Single?
            Dim msg As String
            For Each item In CVars
                ValHold = CVars(item.Key)(Sec)
                If Not ValHold.HasValue Then
                    msg$ = "Warning:  Value for " + VarName$
                    msg$ = msg$ + " of " + item.Key + " is "
                    Debug.Print(msg$ + "Null - Reassigning 0")
                    ValHold = 0
                End If
                For Each item2 In (From s In cntx.Regionalization Where s.Key = item.Key Select Key = s.Key, Value = s.Value)
                    reg% = item2.Value
                Next
                DumTwo(reg%, Sec) = DumTwo(reg%, Sec) + CSng(ValHold)
            Next
        End Sub

        'jrs 2020/03/19
        Public Function ConvertArrayToDict(Arr() As Single, Dim1Count As Integer) As Dictionary(Of Integer, Single?)
            Dim i As Integer
            Dim tmpDict As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
            For i = 1 To Dim1Count
                tmpDict.Add(i, Arr(i))
            Next
            Return tmpDict
        End Function

        'mti 2019/05/14
        'This function returns the data coverage interval for a series
        'when CompareWithFirstYearHist is true, the later of the firstyearhistory and firstyear in the data table will be used for the interval
        Public Function getInterval(ByVal tbl As IDictionary(Of String, Single?()), ByRef years As Short(), ByRef context As IRebuildFacade, Optional CompareWithFirstYearHist As Boolean = False) As Integer
            'mti 2019/05/14 - new procedure
            Dim i As Integer
            getInterval = 0
            Dim FYCov% = 0
            Dim LYCov% = 0
            With tbl

                For i = 0 To years.Count - 1
                    Dim FldYr% = years(i)
                    If FYCov% = 0 Then
                        If CompareWithFirstYearHist Then
                            If FldYr% < context.FirstYear Then
                                FYCov% = context.FirstYear
                            Else
                                FYCov% = FldYr%
                            End If
                        Else
                            FYCov% = FldYr%
                        End If
                    End If
                    If FldYr% > LYCov% Then LYCov% = FldYr%
                Next i
            End With
            getInterval = LYCov% - FYCov%
            If CompareWithFirstYearHist Then
                If LYCov% - context.FirstYear < getInterval Then
                End If
            End If

        End Function

        'jrs 2014/08/26
        Public Sub LoadTableSET4ExogenousForecast(ExogenousModelId As Integer, context As IForecastContext)
            If ExogenousModelId > 0 Then
                IFsVarsExTbls = New Dictionary(Of String, IDictionary(Of String, Single?()))
                IFsVarsExTblsYrs = New Dictionary(Of String, Short())

                'jrs 2024/02/20
                Dim dbIFsHist As SqliteConnection, cmdIFsHist As SqliteCommand
                dbIFsHist = New SqliteConnection("DataSource=" & PutFdrvRun & "ifsHistSeries.db;")
                dbIFsHist.Open()
                cmdIFsHist = dbIFsHist.CreateCommand

                Using db As IFsContext = New IFsContext()
                    'jrs 2024/02/20 added IFsExogenousVarsGral
                    'Load all variables to read
                    Dim rsTbls = (From EV In db.IFsExogenousVars
                                  Join GR In db.IFsExogenousVarsGral On EV.VarName Equals GR.VarName And EV.Dim2 Equals GR.Dim2 And EV.Dim3 Equals GR.Dim3
                                  Where EV.ExogenousModelId = ExogenousModelId
                                  Select VarName = EV.VarName, Dim2 = EV.Dim2, Dim3 = EV.Dim3, TableName = GR.TableName, TableNameOrg = EV.TableName, AdjustmentTable = EV.AdjustmentTable, Adjustment = EV.Adjustment)


                    'Dim rsRegs As Recordset
                    'Set rsRegs = dbIFs.OpenRecordset(Regionalization)
                    'if aggregation is needed load Pop
                    Dim CPOPs As Dictionary(Of String, Single?)
                    If NR <> context.Regionalization.Count Then
                        CPOPs = New Dictionary(Of String, Single?)
                        For Each item In (From s In context.GetSlice("POP", BaseYear)
                                          Select Key = s.Key, Value = s.Value)
                            CPOPs.Add(item.Key, item.Value)
                        Next
                    End If

                    'For each table found
                    Dim sDim2 As String, sDim3 As String
                    'jrs 2024/02/20
                    Dim sTableName As String
                    For Each item In rsTbls
                        sDim2 = item.Dim2
                        sDim3 = item.Dim3
                        sTableName = item.TableName

                        'jrs 2024/02/20
                        'Rename Original Table to avoid Declaration in code
                        Try
                            cmdIFsHist.CommandText = "ALTER TABLE [" & item.TableNameOrg & "] RENAME TO [" & item.TableName & "]"
                            cmdIFsHist.ExecuteNonQuery()
                        Catch ex As Exception
                            sTableName = item.TableNameOrg
                        End Try


                        LoadTable4ExogenousForecast(item.VarName, sDim2, sDim3, sTableName, CPOPs, context,
                        item.Adjustment, item.AdjustmentTable)

                        'jrs 2024/02/20
                        'Restore Original Table
                        Try
                            If sTableName <> item.TableNameOrg Then
                                cmdIFsHist.CommandText = "ALTER TABLE [" & item.TableName & "] RENAME TO [" & item.TableNameOrg & "]"
                                cmdIFsHist.ExecuteNonQuery()
                            End If
                        Catch ex As Exception
                            Debug.Print("There was an error restoring Exogenous Table " & sTableName)
                        End Try

                    Next
                End Using

                'jrs 2024/02/20
                dbIFsHist.Close()
            End If
        End Sub

        'jrs 2014/08/26
        Public Sub LoadTable4ExogenousForecast(sVarName As String, sDim2 As String, sDim3 As String, sTblName As String,
                CPopWg As Dictionary(Of String, Single?), context As IForecastContext, sAdjFactor As String, sAdjTable As String)

            If sDim2 = Nothing Then
                sDim2 = ""
            End If
            If sDim3 = Nothing Then
                sDim3 = ""
            End If

            'Create temporal table to regionalize
            Dim yrs As Short() = context.GetYears(sTblName).OrderBy(Function(y) y).ToArray()
            Dim Tbl As IDictionary(Of String, Single?()) = context.GetDice(sTblName, yrs)
            Dim TblReg As IDictionary(Of String, Single?()) = New Dictionary(Of String, Single?())

            Dim yrsAdj As Short()
            Dim TblAdj As IDictionary(Of String, Single?())
            If sAdjTable > "" Then
                yrsAdj = context.GetYears(sAdjTable).OrderBy(Function(y) y).ToArray()
                TblAdj = context.GetDice(sAdjTable, yrsAdj)
            End If

            Select Case sAdjFactor
                Case "%ToAbs"
                    Dim YrIdx As Short
                    YrIdx = getYearIdx(yrsAdj, yrs(0))
                    For Each key In context.Regionalization.Keys
                        For y = 0 To CShort(yrs.Count - 1)
                            Tbl(key)(y) = Tbl(key)(y) * TblAdj(key)(YrIdx) / 100
                            YrIdx = CShort(YrIdx + 1)
                        Next
                    Next

                Case "AbsTo%"
                    Dim YrIdx As Short
                    YrIdx = getYearIdx(yrsAdj, yrs(0))
                    For Each key In context.Regionalization.Keys
                        For y = 0 To CShort(yrs.Count - 1)
                            Tbl(key)(y) = Tbl(key)(y) / TblAdj(key)(YrIdx) * 100
                            YrIdx = CShort(YrIdx + 1)
                        Next
                    Next
                Case Else
                    'Already loaded
            End Select

            'Regionalize
            If NR = context.Regionalization.Count Then
                For Each item In (From s In Tbl
                                  Join r In context.Regionalization On s.Key Equals r.Key)
                    TblReg.Add(item.r.Value.ToString, item.s.Value)
                Next
            Else
                Dim Var As IVariable
                Var = context.GetVariable(sVarName)
                Dim y As Short
                'Need to do some groupings
                If Var.Aggregation = "SUM" Then
                    For Each item In (From s In Tbl
                                      Join r In context.Regionalization On s.Key Equals r.Key)
                        If Not TblReg.ContainsKey(item.r.Value.ToString) Then
                            TblReg.Add(item.r.Value.ToString, item.s.Value)
                        Else
                            For y = 0 To CShort(yrs.Count - 1)
                                TblReg(item.r.Value.ToString)(y) = If(TblReg(item.r.Value.ToString)(y), 0) +
                                    If(item.s.Value(y), 0)
                            Next
                        End If
                    Next
                Else
                    Dim POPReg As Dictionary(Of Integer, Single) = New Dictionary(Of Integer, Single)
                    For Each item In (From s In Tbl
                                      Join r In context.Regionalization On s.Key Equals r.Key
                                      Join w In CPopWg On s.Key Equals w.Key)
                        If Not TblReg.ContainsKey(item.r.Value.ToString) Then
                            TblReg.Add(item.r.Value.ToString, item.s.Value)
                            'Reset to 0
                            For y = 0 To CShort(yrs.Count - 1)
                                TblReg(item.r.Value.ToString)(y) = 0
                            Next
                            POPReg.Add(item.r.Value, 0)
                        End If

                        For y = 0 To CShort(yrs.Count - 1)
                            TblReg(item.r.Value.ToString)(y) = TblReg(item.r.Value.ToString)(y) +
                                If(item.s.Value(y), 0) * If(item.w.Value, 0)
                            If y = 0 Then
                                POPReg(item.r.Value) = POPReg(item.r.Value) + If(item.w.Value, 0)
                            End If
                        Next
                    Next
                    'Divide by Total POP
                    For R = 1 To NR
                        For y = 0 To CShort(yrs.Count - 1)
                            TblReg(R.ToString)(y) = TblReg(R.ToString)(y) / POPReg(R)
                        Next
                    Next
                End If
            End If

            IFsVarsExTbls.Add(sVarName & sDim2 & sDim3, TblReg)
            IFsVarsExTblsYrs.Add(sVarName & sDim2 & sDim3, yrs)
        End Sub

        'jrs 2022/07/06
        'Replaces only the fisrt instance of a match
        'String.Replace replaces all instances
        'problematic when in scenarios we have groups x groups, where both are the same, like World x World
        Public Function ReplaceFirst(text As String, search As String, replace As String) As String
            Dim pos As Integer = text.IndexOf(search)
            If (pos < 0) Then
                Return text
            End If

            Return text.Substring(0, pos) + replace + text.Substring(pos + search.Length)
        End Function

        'jrs 2014/10/14
        Public Sub LoadAugmentFile()
            Dim CountryCount As Integer, i As Integer, j As Integer, k As Integer
            Dim CountryList() As String
            'Open Ifs Init to read which countries to load
            Using dbInit As IFsInitContext = New IFsInitContext()
                'Dim rsCtry = (From PD In dbInit.ProjectDataCountryLoad)
                Dim rsCtry = dbInit.ProjectDataCountryLoad
                CountryCount = rsCtry.Count
                If CountryCount > 0 Then
                    ReDim CountryList(CountryCount)
                    i = 1
                    For Each cc In rsCtry
                        CountryList(i) = cc.Country
                        i = i + 1
                    Next
                End If
            End Using

            If CountryCount = 0 Then
                'Screen.MousePointer = vbNormal
                Exit Sub
            End If
            'Check if Augment file exist
            If Not System.IO.File.Exists(DataFileDir$ & "ifsHistSeriesAugment.db") Then
                'if not send a message and exit
                Debug.Print("ifsHistSeriesAugment.db is not found in the DATA folder, please check with your system administrator.")
                Exit Sub
            End If
            'Rebuild Base message
            '--------------------
            'If Not (lbl_Status Is Nothing) Then
            'lbl_Status.Caption = "Processing Data External Projects - Please Wait"
            '    lbl_Status.Visible = True
            'lbl_Status.Refresh
            'End If
            Dim objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
            objWriter.WriteLine("Processing Data External Projects - Please Wait")
            objWriter.Close()


            'Check if Backup file already exists
            '-----------------------------------
            If Not System.IO.File.Exists(DataFileDir$ & "ifsHistSeriesOriginalNoProjectData.db") Then
                'if not, then create one
                FileCopyCheck(DataFileDir$ & "ifsHistSeries.db", DataFileDir$ & "ifsHistSeriesOriginalNoProjectData.db")
            End If
            'Open Augment File
            '-----------------
            Dim dbSource As SqliteConnection, rdSource As SqliteDataReader, cmdSource As SqliteCommand
            dbSource = New SqliteConnection("DataSource=" & DataFileDir$ & "ifsHistSeriesAugment.db;")
            dbSource.Open()

            cmdSource = dbSource.CreateCommand()
            cmdSource.CommandText = "SELECT name as TABLE_NAME FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%'"
            rdSource = cmdSource.ExecuteReader()
            Dim rsSourceAll As DataTable = New DataTable
            Dim dsAll As DataSet = New DataSet
            dsAll.Tables.Add(rsSourceAll)
            rsSourceAll.DataSet.EnforceConstraints = False
            rsSourceAll.Load(rdSource)
            Dim rsSource As DataTable

            'Open target file
            '----------------
            Dim dbTarget As SqliteConnection, rsTarget As DataTable, cmdTarget As SqliteCommand, rdTarget As SqliteDataReader
            dbTarget = New SqliteConnection("DataSource=" & DataFileDir$ & "ifsHistSeries.db;")
            dbTarget.Open()

            cmdTarget = dbTarget.CreateCommand

            'Open DataDict file
            '------------------
            Dim rsDataDict As DataTable = New DataTable
            dsAll.Tables.Add(rsDataDict)
            SetDataTablefromSQLite(DataFileDir$ & "DataDict.db", "SELECT * FROM DataDict", "", rsDataDict)

            'Loop through each table
            '-----------------------
            Dim DataProjectNewFields As List(Of String)
            For i = 0 To rsSourceAll.Rows.Count - 1
                'jrs 2023/10/03
                'objWriter = New System.IO.StreamWriter(PutFdrvRun + "progress.txt", append:=True)
                'objWriter.WriteLine("B4 Processing Table: " & rsSourceAll.Rows(i).Item(0).ToString)
                'objWriter.Close()
                'Check if table exist in DataDict
                If rsDataDict.Select("[Table] = '" & rsSourceAll.Rows(i).Item(0).ToString & "'").Count > 0 Then

                    'Close previous Source Reader
                    rdSource.Close()
                    'Open Source
                    cmdSource.CommandText = "SELECT * FROM [" & rsSourceAll.Rows(i).Item(0).ToString & "]"
                    rdSource = cmdSource.ExecuteReader
                    rsSource = New DataTable
                    dsAll.Tables.Add(rsSource)
                    rsSource.Load(rdSource)

                    'Clear new fields to add
                    DataProjectNewFields = New List(Of String)
                    'Open Target Table
                    cmdTarget.CommandText = "SELECT * FROM [" & rsSourceAll.Rows(i).Item(0).ToString & "]"
                    rdTarget = cmdTarget.ExecuteReader
                    rsTarget = New DataTable
                    dsAll.Tables.Add(rsTarget)
                    rsTarget.Load(rdTarget)
                    'Check if all years in source already exist in target
                    '-----------------------------------------------------
                    For j = 2 To rsSource.Columns.Count - 1
                        'if not then add to list to create extra years
                        If Not rsTarget.Columns.Contains(rsSource.Columns(j).ColumnName) Then
                            DataProjectNewFields.Add(rsSource.Columns(j).ColumnName)
                        End If
                    Next j
                    'Create extra years
                    '------------------
                    Dim yr As Integer

                    If DataProjectNewFields.Count > 0 Then

                        Dim sCreate As String = "CREATE TABLE """ & rsSourceAll.Rows(i).Item(0).ToString & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"
                        Dim sIns As String = "INSERT INTO [" & rsSourceAll.Rows(i).Item(0).ToString & "]("
                        Dim sFields As String = "Country, FIPS_CODE"

                        'Dim fld As New Field
                        j = 2
                        For Each f In DataProjectNewFields
                            'Add them to the Target Table
                            yr = 0
                            Do While f > rsTarget.Columns(j).ColumnName And
                            Integer.TryParse(rsTarget.Columns(j).ColumnName, yr)
                                sCreate = sCreate & ", """ & rsTarget.Columns(j).ColumnName & """ DOUBLE(53)"
                                sFields = sFields & ", [" & rsTarget.Columns(j).ColumnName & "]"
                                j = j + 1
                                If j >= rsTarget.Columns.Count Then Exit Do
                            Loop

                            sCreate = sCreate & ", """ & f & """ DOUBLE(53)"
                        Next

                        Do While j < rsTarget.Columns.Count
                            sCreate = sCreate & ", """ & rsTarget.Columns(j).ColumnName & """ DOUBLE(53)"
                            sFields = sFields & ", [" & rsTarget.Columns(j).ColumnName & "]"
                            j = j + 1
                        Loop
                        sCreate = sCreate & ")"

                        rdTarget.Close()
                        rsTarget = Nothing

                        cmdTarget.CommandText = "ALTER TABLE [" & rsSourceAll.Rows(i).Item(0).ToString & "] RENAME TO [tmp" & rsSourceAll.Rows(i).Item(0).ToString & "]"
                        cmdTarget.ExecuteNonQuery()

                        cmdTarget.CommandText = sCreate
                        cmdTarget.ExecuteNonQuery()

                        cmdTarget.CommandText = sIns & sFields & ") SELECT " & sFields & " FROM [tmp" & rsSourceAll.Rows(i).Item(0).ToString & "]"
                        cmdTarget.ExecuteNonQuery()

                        cmdTarget.CommandText = "DROP TABLE [tmp" & rsSourceAll.Rows(i).Item(0).ToString & "]"
                        cmdTarget.ExecuteNonQuery()
                    End If
                    'Search for countries to load
                    '----------------------------
                    Dim sUpd As String = "UPDATE [" & rsSourceAll.Rows(i).Item(0).ToString & "] SET "
                    Dim sFld As String = ""
                    Dim sWHERE As String
                    For j = 1 To CountryCount
                        'jrs 2025/05/21 Dim SourceRows = rsSource.Select("Country = '" & CountryList(j) & "'")
                        Dim SourceRows = rsSource.Select("Country = '" & CountryList(j).Replace("'", "''") & "'")
                        'jrs 2021/10/12 Allow for different tables not to have all countries, makes it easier in merging files
                        If SourceRows.Count > 0 Then
                            'jrs 2025/05/21 sWHERE = " WHERE Country = '" & CountryList(j) & "'"
                            sWHERE = " WHERE Country = '" & CountryList(j).Replace("'", "''") & "'"
                            sFld = ""
                            'Replace data in target with source
                            For k = 2 To rsSource.Columns.Count - 1
                                If Integer.TryParse(rsSource.Columns(k).ColumnName, yr) And
                                    Not Convert.IsDBNull(SourceRows(0).Item(k)) Then
                                    If sFld > "" Then
                                        sFld = sFld & ", "
                                    End If
                                    sFld = sFld & "[" & rsSource.Columns(k).ColumnName & "] = " & SourceRows(0).Item(k).ToString
                                End If
                            Next k
                            If sFld > "" Then
                                cmdTarget.CommandText = sUpd & sFld & sWHERE
                                cmdTarget.ExecuteNonQuery()
                            End If
                            'Update earliest and Most Recent for the country
                            rdTarget.Close()
                            cmdTarget.CommandText = "SELECT * FROM [" & rsSourceAll.Rows(i).Item(0).ToString & "]" & sWHERE
                            rdTarget = cmdTarget.ExecuteReader
                            rsTarget = New DataTable
                            dsAll.Tables.Add(rsTarget)
                            'jrs 2025/07/24 for some reason when reader is loaded into data table it gets empty, or at least .hasRows becomes false
                            Dim brsTargetHasRows As Boolean = rdTarget.HasRows
                            rsTarget.Load(rdTarget)
                            'jrs 2025/07/24 If rdTarget.HasRows Then
                            If brsTargetHasRows Then
                                sFld = ""
                                For k = 2 To rsTarget.Columns.Count - 3
                                    If Not Convert.IsDBNull(rsTarget.Rows(0).Item(k)) Then
                                        sFld = "Earliest = " & rsTarget.Rows(0).Item(k).ToString
                                        Exit For
                                    End If
                                Next k
                                For k = rsTarget.Columns.Count - 3 To 2 Step -1
                                    If Not Convert.IsDBNull(rsTarget.Rows(0).Item(k)) Then
                                        sFld = sFld & ", MostRecent = " & rsTarget.Rows(0).Item(k).ToString
                                        Exit For
                                    End If
                                Next k
                                'Update target table
                                If sFld > "" Then
                                    cmdTarget.CommandText = sUpd & sFld & sWHERE
                                    cmdTarget.ExecuteNonQuery()
                                End If
                            End If
                        End If 'jrs 2021/10/12 only added the if statement, inside logic was already there
                    Next j
                    'Close Source Table
                    rsSource = Nothing
                    'Close Target Table
                    rsTarget = Nothing
                End If
            Next i
            'Close Data Dict File
            'rsDataDict.Close
            'dbDataDict.Close
            'Close Augment File
            dbSource.Close()
            'Close target file
            dbTarget.Close()
            'Copy target file to RUNFILES
            FileCopyCheck(DataFileDir$ & "ifsHistSeries.db", PutFdrvRun$ & "ifsHistSeries.db")
        End Sub

        'jrs 2022/09/15
        Sub SetDataTablefromSQLite(ByVal DBPath As String, ByVal strSQL As String, ByVal IndexCol As String, ByRef DT As DataTable)

            Dim cnnStr As String
            cnnStr = "DataSource=" & DBPath & ";"

            'Dim objAdapter As New OleDb.OleDbDataAdapter(strSQL, cnnStr)
            Dim vSQLiteCon As SqliteConnection
            Dim vSQLiteCommand As SqliteCommand
            Dim vSQLiteReader As SqliteDataReader

            vSQLiteCon = New SqliteConnection(cnnStr)
            vSQLiteCon.Open()

            'DT = New DataTable
            Try
                vSQLiteCommand = vSQLiteCon.CreateCommand()
                vSQLiteCommand.CommandText = strSQL
                vSQLiteReader = vSQLiteCommand.ExecuteReader()
                DT.Load(vSQLiteReader)
                vSQLiteReader.Close()


                'DT = objDataSet.Tables(0)
                'mti August 2004 5 end

                If IndexCol <> "" Then
                    DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
                End If
            Catch ex As Exception
                Try
                    'jrs 2024/09/12
                    Dim str As String
                    str = ex.Message

                    If DT.IsInitialized Then
                        If IndexCol <> "" Then
                            DT.PrimaryKey = New DataColumn() {DT.Columns(IndexCol)}
                        End If
                    End If
                Catch ex2 As Exception
                    Dim str As String
                    str = ex2.Message
                    DT = Nothing
                End Try
            End Try


        End Sub

        Public Sub AddYearFields(cmd As SqliteCommand, Tblname As String, currYrs As Short(), YearStart%, YearEnd%)
            'mti 2015/06/25 - New proc
            Dim sql As String = "ALTER TABLE [" & Tblname & "] RENAME TO [tmp" & Tblname & "]"

            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            Dim sCreate As String = "CREATE TABLE """ & Tblname & """ (""Country"" VARCHAR(255), ""FIPS_CODE"" VARCHAR(255)"
            Dim sFields As String = "Country, FIPS_CODE"
            For j = 0 To currYrs.Length - 1
                sFields = sFields & ", [" & currYrs(j).ToString & "]"
                sCreate = sCreate & ", """ & currYrs(j).ToString & """ DOUBLE(53)"
            Next

            For Ycnt% = YearStart% To YearEnd%
                sCreate = sCreate & ", """ & Ycnt.ToString & """ DOUBLE(53)"
            Next

            sCreate = sCreate & ", Earliest DOUBLE(53)"
            sCreate = sCreate & ", MostRecent DOUBLE(53))"

            cmd.CommandText = sCreate
            cmd.ExecuteNonQuery()

            'Move content
            sql = "INSERT INTO [" & Tblname & "](" & sFields & ") SELECT " & sFields & " FROM [tmp" & Tblname & "]"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            'Update Earliest and Most Recent
            sql = "UPDATE [" & Tblname & "] SET Earliest = (SELECT Earliest FROM [tmp" & Tblname & "] AS t WHERE t.Country = Country)"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            sql = "UPDATE [" & Tblname & "] SET MostRecent = (SELECT MostRecent FROM [tmp" & Tblname & "] AS t WHERE t.Country = Country)"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

            'jrs 2024/02/02
            sql = "DROP TABLE [tmp" & Tblname & "]"
            cmd.CommandText = sql
            cmd.ExecuteNonQuery()

        End Sub

        'jrs 2024/12/11
        Public Sub SaveBilatToCommon(BilatVar(,) As Single, CommonVar(,) As Single)
            Dim i As Integer, j As Integer
            For i = 1 To NR
                For j = 1 To NR
                    CommonVar(i, j) = BilatVar(i, j)
                Next
            Next
        End Sub

        'jrs 2024/12/16 needed for bilateral trade 
        Public Sub SaveBilatToCommon(BilatVar(,,) As Single, idx As Integer, CommonVar(,) As Single)
            Dim i As Integer, j As Integer
            For i = 1 To NR
                For j = 1 To NR
                    'converting to Billions
                    CommonVar(i, j) = CSng(BilatVar(i, j, idx) / 1000000000)
                Next
            Next
        End Sub

        'jrs 2025/07/01
        Public Sub SaveBilatRateToCommon(BilatVar(,,) As Single, idx As Integer, CommonVar(,) As Single)
            Dim i As Integer, j As Integer
            For i = 1 To NR
                For j = 1 To NR
                    'Rates or Indices don't need conversion
                    CommonVar(i, j) = CSng(BilatVar(i, j, idx))
                Next
            Next
        End Sub

        'jrs 2025/09/08
        Public Sub SaveBilatToCommon(BilatVar(,) As Single, CommonVar(,) As Single, ConvFactor As Single)
            If ConvFactor = 0 Then Exit Sub

            Dim i As Integer, j As Integer
            For i = 1 To NR
                For j = 1 To NR
                    CommonVar(i, j) = BilatVar(i, j) / ConvFactor
                Next
            Next
        End Sub


        'jrs 2025/02/24 Function to detect NaN values
        Public Sub Check4Nan(ByRef IFsResults As Dictionary(Of String, Dataframe), Year As Short, FirstYear As Short)

            For Each item In IFsResults

                Dim df As Dataframe
                df = item.Value

                Dim Dim1 As Short, Dim2 As Short, Dim3 As Short
                Dim Dim1Count As Short, Dim2Count As Short, Dim3Count As Short
                Dim Val As Single
                If df.Dimensions.Count > 3 Then
                    Dim1Count = CShort(df.Dimensions(1).Buckets.Count)
                    Dim2Count = CShort(df.Dimensions(2).Buckets.Count)
                    Dim3Count = CShort(df.Dimensions(3).Buckets.Count)
                    For Dim1 = 1 To Dim1Count
                        For Dim2 = 1 To Dim2Count
                            For Dim3 = 1 To Dim3Count
                                Val = CSng(df(getDataFrameIdxFromDims(Year, FirstYear, Dim1, Dim1Count, Dim2, Dim2Count, Dim3, Dim3Count)))
                                If Single.IsNaN(Val) Then
                                    Stop
                                End If
                            Next
                        Next
                    Next
                ElseIf df.Dimensions.Count > 2 Then
                    Dim1Count = CShort(df.Dimensions(1).Buckets.Count)
                    Dim2Count = CShort(df.Dimensions(2).Buckets.Count)
                    For Dim1 = 1 To Dim1Count
                        For Dim2 = 1 To Dim2Count
                            Val = CSng(df(getDataFrameIdxFromDims(Year, FirstYear, Dim1, Dim1Count, Dim2, Dim2Count)))
                            If Single.IsNaN(Val) Then
                                Stop
                            End If
                        Next
                    Next
                ElseIf df.Dimensions.Count > 1 Then
                    Dim1Count = CShort(df.Dimensions(1).Buckets.Count)
                    For Dim1 = 1 To Dim1Count
                        Val = CSng(df(getDataFrameIdxFromDims(Year, FirstYear, Dim1, Dim1Count)))
                        If Single.IsNaN(Val) Then
                            Stop
                        End If
                    Next
                Else
                    Val = CSng(df(getDataFrameIdxFromDims(Year, FirstYear)))
                    If Single.IsNaN(Val) Then
                        Stop
                    End If
                End If
            Next
        End Sub
    End Module

End Namespace
