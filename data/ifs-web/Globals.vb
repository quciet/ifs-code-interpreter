'Namespace IFsWeb

Public Module Globals
    'jrs 2022/07/14
    Public UseOnlySQLite As Boolean
    'jrs 2012/02/06
    'jrs 2006/07/24 Reload Table only if necessary
    Public IFsTables As New Collection

    Public Const MaxFullSessions As Integer = 20 'Session 100 is reserved for batch run
    Public Const MaxFarmFiles As Integer = 20 'jrs 2006/07/20 Maximum number of file copies in the FARM
    Public SessionList(MaxFullSessions) As Boolean
    'jrs 2006/07/20 Object to syncronize threads when allocating Farm Files
    Public FarmList As New Object
    Public DemandFile As String
    'jrs 2019/12/26
    Public StandAloneMode As Boolean
    'jrs 2005/01/27
    Public RunControl1 As New RunControl
    'JRS 03/14/04
    Public frm_Run_RunYears As String

    'DECLARE2 BEGIN ******************************************************
    '------------------------------------------Declare2.Bas-------------------------------------
    '------------------------------------------------------------------------------------------
    '

    '   Copyright by Barry B. Hughes 1988-2003
    '   Model code available for review and education only
    '   Reproduction or incorporation in other software by permission only
    '
    '   Last Changed:  August 21, 2000

    '   This Declare file can be modified by developer - DECLARE.BAS is created by the program
    '       and therefore should not be modified





    '   Path information
    '   ----------------
    Public PutFdrv, FDRV As String  'JRS 8/16/01 GUI , NewFdrv$
    Public DataFileDir, PutDataFileDir As String
    'JRS 8/16/01 GUI Global RecordSourceDef$, DataFieldDef$, DataBaseFile$
    Public StartDirec As String
    Public VCATFile As String
    'JRS 8/16/01 GUI Global SensitivityDispMode$

    '   Version control
    '   ---------------

    Public DevelopmentMode As String
    Public ESRIMap, BMMap As String
    Public Grouping, RegionIDNew, Regionalization, GroupIDNew As String
    Public RegionalIOCoefTabl As String

    '   Files management, subscript data
    '   --------------------------------

    Public MaxVariables, MaxYears As Object ' Values assigned in INIT
    'JRS 8/16/01 GUI Global LongFileOn
    Public filen As Object
    Public V() As String
    Public VScenario() As String
    Public VDef() As String
    Public VarPoint() As Object
    Public VCat() As Object
    Public VUnits() As Object
    'JRS 8/16/01 GUI Global Cur_File$
    Public VExpl() As Object
    'jrs 2011/04/01
    Public VDisplayName() As Object
    'jrs 2016/11/09 Public VDisplayUnitsName() As Object
    Public VNumDecimals() As Object

    'JRS Adding Declaration for Currency
    'JRS 8/16/01 GUI Global VCurrency()
    'JRS 8/16/01 GUI Global sOriginalSym As String 'Stores the previous currency symbol
    'JRS End Declaration for Currency
    Public VType() As Short
    Public VDim1() As Short
    Public VDim2() As Short
    Public VDim3() As Short
    Public VHelpID() As Short
    'JRS 8/16/01 GUI Global VAggregation(), VarCount
    Public VShort() As Object
    Public VMin() As Object
    Public VMax() As Object
    'JRS 8/16/01 GUI GRP$(), VPTR, LY
    Public NumbVars, KNT As Object
    Public MaxVal() As Object
    Public MAXCOM() As Object
    'JRS 8/16/01 GUI Global file$, OrigFlag%, CNTR$(), NC
    Public GVAR, GoalsBoolean As String
    'JRS 8/16/01 GUI Global IY%
    Public NR, MaxReg As Short
    'JRS 8/16/01 GUI Global NAges, NumbCulReg
    Public WVSCohorts, NCohorts, NWVSWaves As Object
    'JRS 8/16/01 GUI Global NER%, NEU%, NEP%, NET%
    Public NSec, NGovExp As Short
    'KIA 2008/03/05
    Public ModelRunningWith As String


    '   Time and Run Control
    '   --------------------
    Public FirstYearForecast As Short 'jrs 2011/03/23 LastYearForecast can't be global anymore, changes with parameter
    Public LastYearHistory, FirstYearHistory As Short
    Public PosModYear, PosModYearI As Object


    '   Table Function Data
    '   -------------------
    Public TFCount As Short

    Public FuncName() As String
    Public XVariable() As String
    Public YVariable() As String
    Public Analytic() As Short
    'jrs 2006/01/03 Public Session("BPoints")(0, 0) As Object

    'jrs 2006/01/03 Public Session("APoint")() As Object
    'jrs 2006/01/03 Public session("MinPoint")() As Object
    'jrs 2006/01/03 Public session("MaxPoint")() As Object


    '   Control of Change and Display
    '   -----------------------------
    Public Carret As Object
    Public carret2 As String
    Public WelcomeMessage As String

    '   Regionalization Related - Values assigned in initialization
    '                           - Values changed in regionalization change
    '   ------------------------------------------------------------------
    Public RegName() As String
    Public RegMembs() As Object
    Public RegHold() As Object
    Public RegWeight() As Object
    Public GRPS(0, 0) As String

    '   Preprocessing of data and rebuilding of base case
    '   -------------------------------------------------
    'UPGRADE_WARNING: Lower bound of array RegVal was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
    Public RegVal(10) As Object 'Original 10 regions; eliminate when upgrade demographics
    'UPGRADE_WARNING: Lower bound of array CountryName was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
    'jrs 2008/05/14 Public CountryName(250) As String 
    'JRS 8/17/01 GUI Global CounPop(200), CPop(200), CGDP(200), CGDPP(200)
    'UPGRADE_WARNING: Lower bound of array CounReg was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
    'jrs 2008/05/14 Public CounReg(250) As Object 
    'UPGRADE_WARNING: Lower bound of array CounBase was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1033"'
    'jrs 2008/05/14 Public CounBase(250) As Object 
    Public RCount As Object
    Public FillLocation As Object



    '   INI file location
    '   -----------------
    '    Global Const IFSINIFile$ = "C:\WINDOWS\IFSWIN.INI"
    Public IFSINIFile As String
    Public WinDir As String


    '   Global Locational Information
    '   -----------------------------
    Public DefaultDrive As String
    Public LocalDirectory As String
    Public DataFileSubDir As String
    Public IFs_MDB As String
    Public IFsHist_MDB As String
    Public IFsCoVaTrA_MDB As String

    '   Measurement units
    '   -----------------
    Public Const QuarterInch As Short = 1440 / 4
    Public Const EighthInch As Short = 1440 / 8
    Public Const HalfInch As Short = 1440 / 2
    Public Const Inch As Short = 1440


    Structure REGTYPE
        Dim AX As Short
        Dim BX As Short
        Dim CX As Short
        Dim DX As Short
        Dim BP As Short
        Dim SI As Short
        Dim DI As Short
        Dim FLAGS As Short
    End Structure

    '   Help engine declarations.

    'CONVERSION_TODO: The following code was converted according to the value of the conditional statement.  Click for more: 'ms-its:C:\Program Files\ArtinSoft\WinFormsToWeb\WinFormsToWeb.chm::1024.html'.
    'JRS 02/06/04 #If Win32 Then ' 32-bit VB uses this Declare
    ' Commands to pass WinHelp() - 32 bit version
    Public Const HELP_CONTEXT As Short = &H1S '  Display topic in ulTopic
    Public Const HELP_QUIT As Short = &H2S '  Terminate help
    Public Const HELP_FINDER As Short = &HBS 'Addition for Windows 95 file from MS Support
    Public Const HELP_INDEX As Short = &H3S '&H3            '  Display index
    Public Const HELP_CONTENTS As Integer = &H3
    Public Const HELP_HELPONHELP As Short = &H4S '  Display help on using help
    Public Const HELP_SETINDEX As Short = &H5S '  Set current Index for multi index help
    Public Const HELP_SETCONTENTS As Integer = &H5
    Public Const HELP_CONTEXTPOPUP As Integer = &H8
    Public Const HELP_FORCEFILE As Integer = &H9
    Public Const HELP_KEY As Short = &H101S '  Display topic for keyword in offabData
    Public Const HELP_COMMAND As Integer = &H102
    Public Const HELP_PARTIALKEY As Integer = &H105
    Public Const HELP_MULTIKEY As Integer = &H201
    Public Const HELP_SETWINPOS As Integer = &H203
    Declare Function WinHelp Lib "user32" Alias "WinHelpA" (ByVal HWND As Integer, ByVal lpHelpFile As String, ByVal wCommand As Integer, ByVal dwData As Integer) As Integer

    'Vensim Integration
    '   --------------------
    Public VenSimFileDir, VensimFileSubDir As String
    Public ExternalRun As String 'Denotes whether/what type of external model is running during IFs Run
    Public VensimOpened As Short
    'DECLARE2 END   ******************************************************
    'DECLARE3 BEGIN ******************************************************

    '   Path information
    '   ----------------
    Public StartFile As String

    '   Version control
    '   ---------------
    Public ModelLoad, AbbCodeMode As String
    Public RebuiltBase, RunModel As String
    Public HistoricOption As String
    Public ModelVersion, ModelName As String
    Public SupplementalDataFile, SupplementalHistoricDataFile As String
    'jrs 2023/05/19
    Public SubVersionNumber As Integer
    'jrs 2025/10/03
    'Poverty Levels
    Public PovLevelsByPPPYear As Dictionary(Of Integer, Dictionary(Of String, Single))
    Public ConstantDollarsYear As Integer
    'jrs 2025/10/06
    Public ExtraRegionSetsLoaded As Integer

    '   Data password
    '   -------------
    Public DataPW As String

    'jrs 2021/06/21
    Public sLanguage As String

    '   Files management, subscript data
    '   --------------------------------
    Public NumbCulReg, NAges, NIOMatrices As Object
    Public NWBTar As Short
    Public NET, NEU, NER, NEP, NHHTypes As Short



    '   Energy Types
    '   ------------

    Public Const Oil As Short = 1
    Public Const Gas As Short = 2
    Public Const Coal As Short = 3
    Public Const Hydro As Short = 4
    Public Const Nucl As Short = 5
    Public Const Renew As Short = 6
    Public Const Synth As Short = 7

    '   Government Spending Categories
    '   ------------------------------

    Public Const Mil As Short = 1
    Public Const Health As Short = 2
    Public Const Educ As Short = 3
    Public Const RandD As Short = 4
    Public Const Other As Short = 5
    Public Const FrAid As Short = 6

    '   Education Levels
    '   ----------------
    Public Const Primary As Short = 1
    Public Const Secondary As Short = 2
    Public Const Tertiary As Short = 3

    '   Labor Types
    '   -----------
    Public Const Unskilled As Short = 1
    Public Const Skilled As Short = 2


    '   Economic Sectors
    '   ----------------
    Public Const Agri As Short = 1
    Public Const Ener As Short = 2
    Public Const Mate As Short = 3
    Public Const Manu As Short = 4
    Public Const Serv As Short = 5
    Public Const ICT As Short = 6

    '   Land and Food Types
    '   -------------------
    Public Const Crop As Short = 1
    Public Const Graz As Short = 2
    Public Const Fors As Short = 3
    Public Const Urba As Short = 4
    Public Const Othe As Short = 5
    Public Const Meat As Short = 2

    '   Global Locational Information
    '   -----------------------------
    Public WorldMapLocation As String


    'Type DATAPT - Old system before size became too large
    '    elem(15591) As Single 'Maximum is 15,800 (about 32 regions)
    'End Type
    Structure DATAPT2 '- New system
        <VBFixedArray(5000)> Dim elem2() As Single

        'UPGRADE_TODO: "Initialize" must be called to initialize instances of this structure. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1026"'
        Public Sub Initialize()
            ReDim elem2(5000)
        End Sub
    End Structure
    Structure DATAPT3
        'JRS 8/20/01 elem3(2125) As Single
        Dim elem3() As Single
        '  elem3(2050) As Single
        '   Now up to above 72,000 for Professional Edition
        '   Above 10000 for Student Edition
        '   Dim BufElem in RebBase sets total size of buffer; may need to
        '   expand that also
    End Structure

    '   Unfortunately, above code cannot be changed so that dimensioned by filen
    '       Type elements cannot be variably dimensioned
    '   Now must change manually when add variable
    'Global FILEBUF As DATAPT
    Public FILEBUF2 As DATAPT2
    Public FILEBUF3 As DATAPT3

    '   Scenario Analysis
    '   -----------------------------
    'mti WebForm Changes From June 21, 2004  - start
    'Public Session("ScenarioName") As String
    ''mtiwebformsMarch2004 (2)ScenarioCreate
    'Public Session("LinePrefix")() As String 'Array
    ''mtiwebformsMarch2004 (2)ScenarioCreate end


    'Public Session("cur_file") As String
    'mti WebForm Changes From June 21, 2004  -end 

    '----Scenario Change August 2001----
    Public VScenCat() As Object
    Public VShiftUp() As Object
    Public VShiftDown() As Object
    Public VShiftYears() As Object
    Public VCATScenarioFile As String

    '   Weight Variables for Grouping
    '   -----------------------------

    Public iGDP As Short
    Public iPOP As Short

    'mti WebForm Changes From June 21, 2004 -start - variable no for run graph variables
    Public iWGDP As Short
    Public iWPOP As Short
    Public iWFORST As Short
    Public iCO2PPM As Short
    'mti WebForm Changes From June 21, 2004 - end

    'DECLARE3 END ********************************************************
    'DECLARE4 BEGIN ******************************************************

    '   Grouping Related
    '   ----------------
    'jrs 2008/12/08 make size flexible
    Public GroupNames() As String 'jrs 2008/01/18 changed from 200 to 250
    Public GCount As Object

    'cdv 2021/08/20
    Public CoreGroupNames() As String
    Public CGCount As Object
    'jrs 2008/12/08 make size flexible
    Public GroupCodes() As Short 'jrs 2008/01/18 changed from 200 to 250
    'jrs 2011/02/10
    Public GListNames() As String
    Public GListCount As Integer

    'jrs 2013/10/01
    Public GDimsCount As Integer 'This is the sum of all dims stored in GRPS

    'jrs 2005/10/24 Public DataFieldDef As String

    Public VCurrency() As Object
    Public sOriginalSym As String 'Stores the previous currency symbol
    Public VAggregation() As Object
    'jrs 2014/09/25
    Public VAggregation2() As Object
    'jrs 2014/10/08
    Public VNoTot2() As Boolean

    'JRS 12/12/02
    Public POPidx As Short
    Public GDPidx As Short

    'jrs 2009/01/23 Index of Dimensions for
    'Gender, and different age cohorts
    Public GenderIdx As Short
    Public Age18_65Idx As Short
    Public Age30_70Idx As Short
    Public Age15_80Idx As Short

    'cdv 2021/01/12 Added Variable for Tracking the Gender for Graphs
    Public bMaleFirst As Boolean

    Public bCoreGroupCheck As Boolean

    'DECLARE4 END ********************************************************

    Public VCatName() As String
    Public VHelpIDEqn() As Short
    Public VHistoricAnalog() As Object
    'jrs 2008/06/12
    Public VUsrDefCat() As String

    'mti Validation changes August, 2002
    '   String Handling
    '   -----------------
    Public Const Quote As String = """"




    'mtiwebform June 2004 Longitudinal - extrapolation
    'this was not merged in Windows forms (hence not converted by ArtinSoft) 
    'Mohammod .Net Win Forms Feb 2004
    'timeseries extrapolation  parameters
    Public DefYearsAhead As Short
    Public DefPOLY1 As Double
    'jrs 2005/02/25
    Public ShortSce() As String
    Public LongSce() As String
    'jrs 2005/11/18
    Public IfsVars As New MyVariables
    Public IFsRegs As New MyRegions
    'jrs 2006/06/13
    Public IFsHistAnalogs As New MyHistAnalogs
    'jrs 2006/09/27
    Public IFsAggRules As New myAggRules

    'jrs 2005/12/09
    Public rsCurr As DataTable
    'jrs 2011/09/29
    Public IFsCtrys As New MyRegions
    'jrs 2019/07/11
    Public bHidePrivateVars As Boolean
    Public VHidePrivateVars() As Boolean
    'jrs 2019/07/26
    Public DefaultFlexDisplayFilter As String

End Module
'End Namespace