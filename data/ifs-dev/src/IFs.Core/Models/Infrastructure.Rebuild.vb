Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data
Imports System.Math 'to avoid having to put Math in front of Log and Exp

Namespace Models
    Partial Public Module Infrastructure

        <RebuildsVariable("SANITATION", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATSAFE", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATERHH", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("SANITHH", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATERHHCOSTLOWER", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATERHHCOSTHIGHER", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("SANITHHCOSTLOWER", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("SANITHHCOSTHIGHER", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATERHHINVESTCAP", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATERHHINVESTMAINT", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("SANITHHINVESTCAP", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("SANITHHINVESTMAINT", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATERFILTERED", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATWASTE", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("WATWASTETREAT", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAELECACC", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAELECGENCAP", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAELEC", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAELECTRANLOSS", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAELECPROD", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ENELECSHRENDEM", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ENDEMSH", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAROAD", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAROADRAI", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAROADPAVEDPCNT", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("LANDIRAREAEQUIP", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("landirareasat", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRATELE", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRATELEOLD", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTMOBIL", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTBROAD", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTINDEX", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTCOMPUTERS", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTINTNETHH", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTSPEND", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ICTBROADMOBIL", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDTRAD", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDICT", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDTRAN", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDELEC", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDWATSAN", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDICTZ", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("ENSOLFUEL", AggregatorType:=GetType(INFRAAggregator))>
        <RebuildsVariable("INFRAINDWATSANCOMPSDI")>
        <RebuildsVariable("INFRAINDWATSANCOMPMEANI")>
        <RebuildsVariable("INFRAINDICTZCOMPSDI")>
        <RebuildsVariable("INFRAINDICTZCOMPMEANI")>
        <RebuildsVariable("INFRAINDELECCOMPSDI")>
        <RebuildsVariable("INFRAINDELECCOMPMEANI")>
        <RebuildsVariable("INFRAINDTRANCOMPSDI")>
        <RebuildsVariable("INFRAINDTRANCOMPMEANI")>
        <RebuildsVariable("INFRAINDTRADCOMPSDI")>
        <RebuildsVariable("INFRAINDTRADCOMPMEANI")>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CGDPPC")>
        <ImportsAsset("CPOP")>
        <ImportsAsset("CHHSIZE")>
        <ImportsAsset("CLandArea")>
        <ImportsAsset("CPOPURBAN")>
        <ImportsAsset("CEDYRSAGE25T")>
        <ImportsAsset("ENPOIL")>
        <ImportsAsset("ENPGAS")>
        <ImportsAsset("ENPCOAL")>
        <ImportsAsset("ENPHyd")>
        <ImportsAsset("ENPOthRen")>
        <ImportsAsset("LDCrop")>
        <ImportsAsset("CLIT")>
        <ImportsAsset("CEDSECENRGT")>
        <ImportsAsset("CEDTERENRGT")>
        <ImportsAsset("CGDP")>
        <ImportsAsset("CEDYRSAGE25TGR")>
        <ImportsAsset("CPOPURBGR")>
        Public Iterator Function RebuildInfrastructure(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Yield context.Query("SANITATION").Filter(BaseYearOnly).Execute()
            Yield context.Query("WATSAFE").Filter(BaseYearOnly).Execute()

            'mti 2022/06/21
            Yield context.Query("WATERHH").Filter(BaseYearOnly).Execute()
            Yield context.Query("SANITHH").Filter(BaseYearOnly).Execute()
            'jrs 2023/07/25 repeating values for all years 
            'WATERHHCOSTLOWER, WATERHHCOSTHIGHER
            'SANITHHCOSTLOWER, SANITHHCOSTLOWER

            'jrs 2019/12/05
            Dim dftmp As IDataframe
            Dim R As Short, Dim2 As Short, Dim3 As Short

            dftmp = context.Query("WATERHHCOSTLOWER").Filter(BaseYearOnly).Execute()
            Dim dfWATERHHCOSTLOWER As Dataframe = Dataframe.Create("WATERHHCOSTLOWER", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For Dim2 = 1 To 5
                        For Dim3 = 1 To 3
                            dfWATERHHCOSTLOWER(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3))
                        Next
                    Next
                Next
            Next
            Yield dfWATERHHCOSTLOWER

            dftmp = context.Query("WATERHHCOSTHIGHER").Filter(BaseYearOnly).Execute()
            Dim dfWATERHHCOSTHIGHER As Dataframe = Dataframe.Create("WATERHHCOSTHIGHER", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For Dim2 = 1 To 5
                        For Dim3 = 1 To 3
                            dfWATERHHCOSTHIGHER(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3))
                        Next
                    Next
                Next
            Next
            Yield dfWATERHHCOSTHIGHER

            dftmp = context.Query("SANITHHCOSTLOWER").Filter(BaseYearOnly).Execute()
            Dim dfSANITHHCOSTLOWER As Dataframe = Dataframe.Create("SANITHHCOSTLOWER", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For Dim2 = 1 To 5
                        For Dim3 = 1 To 3
                            dfSANITHHCOSTLOWER(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3))
                        Next
                    Next
                Next
            Next
            Yield dfSANITHHCOSTLOWER

            dftmp = context.Query("SANITHHCOSTHIGHER").Filter(BaseYearOnly).Execute()
            Dim dfSANITHHCOSTHIGHER As Dataframe = Dataframe.Create("SANITHHCOSTHIGHER", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For Dim2 = 1 To 5
                        For Dim3 = 1 To 3
                            dfSANITHHCOSTHIGHER(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 5, Dim3, 3))
                        Next
                    Next
                Next
            Next
            Yield dfSANITHHCOSTHIGHER

            Yield context.Query("WATERHHINVESTCAP").Filter(BaseYearOnly).Execute()
            Yield context.Query("WATERHHINVESTMAINT").Filter(BaseYearOnly).Execute()
            Yield context.Query("SANITHHINVESTCAP").Filter(BaseYearOnly).Execute()
            Yield context.Query("SANITHHINVESTMAINT").Filter(BaseYearOnly).Execute()


            dftmp = context.Query("WATERFILTERED").Filter(BaseYearOnly).Execute()
            Dim dfWATERFILTERED As Dataframe = Dataframe.Create("WATERFILTERED", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    For Dim2 = 1 To 3
                        dfWATERFILTERED(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR), Dim2, 3)) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR), Dim2, 3))
                    Next
                Next
            Next
            Yield dfWATERFILTERED

            Yield context.Query("WATWASTE").Filter(BaseYearOnly).Execute()
            Yield context.Query("WATWASTETREAT").Filter(BaseYearOnly).Execute()

            Yield context.Query("INFRAELECACC").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAELECGENCAP").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAELEC").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAELECTRANLOSS").Filter(BaseYearOnly).Execute()

            Yield context.Query("INFRAELECPROD").Filter(BaseYearOnly).Execute()
            Yield context.Query("ENELECSHRENDEM").Filter(BaseYearOnly).Execute()
            Yield context.Query("ENDEMSH").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAROAD").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAROADRAI").Filter(BaseYearOnly).Execute()

            Yield context.Query("INFRAROADPAVEDPCNT").Filter(BaseYearOnly).Execute()
            Yield context.Query("LANDIRAREAEQUIP").Filter(BaseYearOnly).Execute()
            'jrs 2019/11/26
            dftmp = context.Query("landirareasat").Filter(BaseYearOnly).Execute()
            Dim dflandirareasat As Dataframe = Dataframe.Create("landirareasat", context, context.FirstYear, context.LastYear, 0)
            For y = FirstYear To LastYear
                For R = 1 To CShort(NR)
                    dflandirareasat(getDataFrameIdxFromDims(y, FirstYear, R, CShort(NR))) = dftmp(getDataFrameIdxFromDims(FirstYear, FirstYear, R, CShort(NR)))
                Next
            Next
            Yield dflandirareasat
            Yield context.Query("INFRATELE").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRATELEOLD").Filter(BaseYearOnly).Execute()

            Yield context.Query("ICTMOBIL").Filter(BaseYearOnly).Execute()
            Yield context.Query("ICTBROAD").Filter(BaseYearOnly).Execute()
            Yield context.Query("ICTINDEX").Filter(BaseYearOnly).Execute()
            Yield context.Query("ICTCOMPUTERS").Filter(BaseYearOnly).Execute()
            Yield context.Query("ICTINTNETHH").Filter(BaseYearOnly).Execute()
            Yield context.Query("ICTSPEND").Filter(BaseYearOnly).Execute()

            'Yield context.Query("INFRAINVESTNEW").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRAINVESTMAINT").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRAROADUNPAVEDCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRAROADPAVEDCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRAELECGENCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRAELECACCRURALCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRAELECACCURBANCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("SANITATIONIMPCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("WATSAFEIMPCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("LANDIRCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("WATWASTETREATCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("INFRATELECOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("ICTMOBILCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("ICTBROADCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("SANITATIONCOST").Filter(BaseYearOnly).Execute()
            'Yield context.Query("WATSAFECOST").Filter(BaseYearOnly).Execute()

            Yield context.Query("ICTBROADMOBIL").Filter(BaseYearOnly).Execute()
            'Yield context.Query("ICTBROADMOBILCOST").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAINDTRAD").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAINDICT").Filter(BaseYearOnly).Execute()

            Yield context.Query("INFRAINDTRAN").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAINDELEC").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAINDWATSAN").Filter(BaseYearOnly).Execute()
            Yield context.Query("INFRAINDICTZ").Filter(BaseYearOnly).Execute()

            Yield context.Query("ENSOLFUEL").Filter(BaseYearOnly).Execute()

            'jrs 2019/10/22
            Dim dfINFRAINDWATSANCOMPSDI As Dataframe = Dataframe.Create("INFRAINDWATSANCOMPSDI", context, context.FirstYear, context.LastYear, Nothing)
            Dim dfINFRAINDWATSANCOMPMEANI As Dataframe = Dataframe.Create("INFRAINDWATSANCOMPMEANI", context, context.FirstYear, context.LastYear, Nothing)
            For IICCnt% = 1 To context.NInfraIndWatSanComp
                For y = FirstYear To LastYear
                    dfINFRAINDWATSANCOMPSDI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 3)) = INFRAINDWATSANCOMPSDI(IICCnt%)
                    dfINFRAINDWATSANCOMPMEANI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 3)) = INFRAINDWATSANCOMPMEANI(IICCnt%)
                Next
            Next
            Yield dfINFRAINDWATSANCOMPSDI
            Yield dfINFRAINDWATSANCOMPMEANI

            Dim dfINFRAINDICTZCOMPSDI As Dataframe = Dataframe.Create("INFRAINDICTZCOMPSDI", context, context.FirstYear, context.LastYear, Nothing)
            Dim dfINFRAINDICTZCOMPMEANI As Dataframe = Dataframe.Create("INFRAINDICTZCOMPMEANI", context, context.FirstYear, context.LastYear, Nothing)
            For IICCnt% = 1 To context.NInfraIndexICTComp
                For y = FirstYear To LastYear
                    dfINFRAINDICTZCOMPSDI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 4)) = INFRAINDICTZCOMPSDI(IICCnt%)
                    dfINFRAINDICTZCOMPMEANI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 4)) = INFRAINDICTZCOMPMEANI(IICCnt%)
                Next
            Next
            Yield dfINFRAINDICTZCOMPSDI
            Yield dfINFRAINDICTZCOMPMEANI

            Dim dfINFRAINDELECCOMPSDI As Dataframe = Dataframe.Create("INFRAINDELECCOMPSDI", context, context.FirstYear, context.LastYear, Nothing)
            Dim dfINFRAINDELECCOMPMEANI As Dataframe = Dataframe.Create("INFRAINDELECCOMPMEANI", context, context.FirstYear, context.LastYear, Nothing)
            For IICCnt% = 1 To context.NInfraIndElecComp
                For y = FirstYear To LastYear
                    dfINFRAINDELECCOMPSDI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 4)) = INFRAINDELECCOMPSDI(IICCnt%)
                    dfINFRAINDELECCOMPMEANI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 4)) = INFRAINDELECCOMPMEANI(IICCnt%)
                Next
            Next

            Yield dfINFRAINDELECCOMPSDI
            Yield dfINFRAINDELECCOMPMEANI

            Dim dfINFRAINDTRANCOMPSDI As Dataframe = Dataframe.Create("INFRAINDTRANCOMPSDI", context, context.FirstYear, context.LastYear, Nothing)
            Dim dfINFRAINDTRANCOMPMEANI As Dataframe = Dataframe.Create("INFRAINDTRANCOMPMEANI", context, context.FirstYear, context.LastYear, Nothing)
            For IICCnt% = 1 To context.NInfraIndElecComp
                For y = FirstYear To LastYear
                    dfINFRAINDTRANCOMPSDI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 4)) = INFRAINDTRANCOMPSDI(IICCnt%)
                    dfINFRAINDTRANCOMPMEANI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 4)) = INFRAINDTRANCOMPMEANI(IICCnt%)
                Next
            Next

            Yield dfINFRAINDTRANCOMPSDI
            Yield dfINFRAINDTRANCOMPMEANI

            Dim dfINFRAINDTRADCOMPSDI As Dataframe = Dataframe.Create("INFRAINDTRADCOMPSDI", context, context.FirstYear, context.LastYear, Nothing)
            Dim dfINFRAINDTRADCOMPMEANI As Dataframe = Dataframe.Create("INFRAINDTRADCOMPMEANI", context, context.FirstYear, context.LastYear, Nothing)
            For IICCnt% = 1 To context.NInfraIndexTradComp
                For y = FirstYear To LastYear
                    dfINFRAINDTRADCOMPSDI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 7)) = INFRAINDTRADCOMPSDI(IICCnt%)
                    dfINFRAINDTRADCOMPMEANI(getDataFrameIdxFromDims(y, FirstYear, CShort(IICCnt%), 7)) = INFRAINDTRADCOMPMEANI(IICCnt%)
                Next
            Next

            Yield dfINFRAINDTRADCOMPSDI
            Yield dfINFRAINDTRADCOMPMEANI
        End Function

        Class INFRAAggregator
            Inherits DefaultSumAggregator

            Dim infraroadunpavedlife As Single
            Dim infraroadunpavedcostlower As Single
            Dim infraroadunpavedcostupper As Single
            Dim infraroadpavedlife As Single
            Dim infraroadpavedcostlower As Single

            Dim infraroadpavedcostupper As Single
            Dim landirlife As Single
            Dim landircostlower As Single
            Dim landircostupper As Single
            Dim watwastetreatlife As Single

            Dim watwastetreatcostlower As Single
            Dim watwastetreatcostupper As Single
            Dim infraelecgenlife As Single
            Dim infraelecgencostlower As Single
            Dim infraelecgencostupper As Single

            Dim infraelecaccrurallife As Single
            Dim infraelecaccruralcostlower As Single
            Dim infraelecaccruralcostupper As Single
            Dim infraelecaccurbanlife As Single
            Dim infraelecaccurbancostlower As Single

            Dim infraelecaccurbancostupper As Single
            Dim watsafeimplife As Single
            Dim watsafeimpcostlower As Single
            Dim watsafeimpcostupper As Single
            Dim sanitationimplife As Single

            Dim sanitationimpcostlower As Single
            Dim sanitationimpcostupper As Single
            Dim sanitationlife As Single
            Dim sanitationcostlower As Single
            Dim sanitationcostupper As Single

            Dim watsafelife As Single
            Dim watsafecostlower As Single
            Dim watsafecostupper As Single
            Dim ictbroadlife As Single
            Dim ictbroadcostlower As Single

            Dim ictbroadcostupper As Single
            Dim ictbroadmobillife As Single
            Dim ictbroadmobilcostlower As Single
            Dim ictbroadmobilcostupper As Single
            Dim ictmobillife As Single

            Dim ictmobilcostlower As Single
            Dim ictmobilcostupper As Single
            Dim infratelelife As Single
            Dim infratelecostlower As Single
            Dim infratelecostupper As Single

            Dim infraindtradcompwt() As Single
            Dim infraindictcompwt() As Single
            Dim infraindictzcompwt() As Single
            Dim infraindtrancompwt() As Single
            Dim infraindeleccompwt() As Single
            Dim infraindwatsancompwt() As Single

            Private Property CGDPPCPGR As IDictionary(Of String, Single?)
            Private Property CGDPPCPBase As IDictionary(Of String, Single?)
            Private Property cPOPGR As IDictionary(Of String, Single?)
            Private Property CPOPBase As IDictionary(Of String, Single?)
            Private Property CHHSizeGR As IDictionary(Of String, Single?)
            Private Property CINCOMELT1 As IDictionary(Of String, Single?)
            Private Property CGovEffect As IDictionary(Of String, Single?)
            Private Property CGovRegQual As IDictionary(Of String, Single?)

            Private Property CSANITATION1 As IDictionary(Of String, Single?)
            Private Property CSANITATION2 As IDictionary(Of String, Single?)
            Private Property CSANITATION3 As IDictionary(Of String, Single?)

            'mti 2022/06/21 start
            '------------ Water Connection: 5 Category JMP ---------------
            Private Property CSANITHH() As IDictionary(Of String, Single?)(,)
            Private Property CWATERHH() As IDictionary(Of String, Single?)(,)
            Private Property CPOPRURAL As IDictionary(Of String, Single?)
            Private Property QPoverty As IDictionary(Of String, Single?)
            Private Property CWATERHHCOSTLOWER() As IDictionary(Of String, Single?)(,)
            Private Property CWATERHHCOSTHIGHER() As IDictionary(Of String, Single?)(,)
            Private Property CSANITHHCOSTLOWER() As IDictionary(Of String, Single?)(,)
            Private Property CSANITHHCOSTHIGHER() As IDictionary(Of String, Single?)(,)
            Private Property CWaterHHAnnGr() As IDictionary(Of String, Single?)(,)
            Private Property CWaterHHAnnChg() As IDictionary(Of String, Single?)(,)
            Private Property CSanitHHAnnGr() As IDictionary(Of String, Single?)(,)
            Private Property CSanitHHAnnChg() As IDictionary(Of String, Single?)(,)
            Private Property cWATERHHINVESTCAP() As IDictionary(Of String, Single?)(,)
            Private Property cWATERHHINVESTMAINT() As IDictionary(Of String, Single?)(,)
            Private Property cSANITHHINVESTCAP() As IDictionary(Of String, Single?)(,)
            Private Property cSANITHHINVESTMAINT() As IDictionary(Of String, Single?)(,)
            'mti 2022/06/21 end

            'mti 2022/10/04 start
            Private Property CWaterHHAnnGrUse() As IDictionary(Of String, Single?)(,)
            Private Property CSanitHHAnnGrUse() As IDictionary(Of String, Single?)(,)
            Private Property CWaterHHAnnGrALB() As IDictionary(Of String, Single?)()
            Private Property CSanitHHAnnGrALB() As IDictionary(Of String, Single?)()      'at least basic
            Private Property CWaterHHAnnGrPiped() As IDictionary(Of String, Single?)()
            'mti 2022/10/04 end

            'mti 2022/09/16 start
            Private Property WaterHHGR() As IDictionary(Of String, Single?)(,)
            Private Property SanitHHGR() As IDictionary(Of String, Single?)(,)
            'mti 2022/09/16 End

            'mti 2022/09/12 start
            Private Property CSanitSafeHH() As IDictionary(Of String, Single?)(,)
            Private Property CWaterSafeHH() As IDictionary(Of String, Single?)(,)
            Private Property CWaterPipedHH() As IDictionary(Of String, Single?)()
            'mti 2022/09/12 end

            'mti 2022/09/19 start
            Private Property CWaterAtLeastBasicHH() As IDictionary(Of String, Single?)()
            Private Property CSanitAtLeastBasicHH() As IDictionary(Of String, Single?)()
            'mti 2022/09/19 end

            Private Property CSanitationGR1 As IDictionary(Of String, Single?)
            Private Property CSanitationGR2 As IDictionary(Of String, Single?)
            Private Property CSanitationGR3 As IDictionary(Of String, Single?)

            Private Property cSANITATIONCOST As IDictionary(Of String, Single?)
            Private Property cSANITATIONIMPCOST As IDictionary(Of String, Single?)

            Private Property cINFRAINVESTNEW As IDictionary(Of String, Single?())
            Private Property cINFRAINVESTMAINT As IDictionary(Of String, Single?())

            '------------ Waste Water Treatment --------------------
            Private Property CWATWASTE As IDictionary(Of String, Single?)
            Private Property CWATWASTETREAT As IDictionary(Of String, Single?)
            Private Property CWATWASTEGR As IDictionary(Of String, Single?)
            Private Property CWATWASTETREATGR As IDictionary(Of String, Single?)
            Private Property cWATWASTETREATCOST As IDictionary(Of String, Single?)

            'Water Connections
            Private Property CWATSAFE1 As IDictionary(Of String, Single?)
            Private Property CWATSAFE2 As IDictionary(Of String, Single?)
            Private Property CWATSAFE3 As IDictionary(Of String, Single?)

            Private Property CWatSafeGR1 As IDictionary(Of String, Single?)
            Private Property CWatSafeGR2 As IDictionary(Of String, Single?)
            Private Property CWatSafeGR3 As IDictionary(Of String, Single?)

            Private Property cWATSAFECOST As IDictionary(Of String, Single?)
            Private Property cWATSAFEIMPCOST As IDictionary(Of String, Single?)

            'Water Filtered
            Private Property CWATERFILTERED1 As IDictionary(Of String, Single?)
            Private Property CWATERFILTERED2 As IDictionary(Of String, Single?)
            Private Property CWATERFILTERED3 As IDictionary(Of String, Single?)

            'Electricity
            Private Property CInfraElecGenCapGR As IDictionary(Of String, Single?)
            Private Property CInfraElecGenCap As IDictionary(Of String, Single?)
            Private Property CInfraElecGenCapGRAbs As IDictionary(Of String, Single?)

            Private Property cINFRAELECGENCOST As IDictionary(Of String, Single?)
            Private Property CInfraElecAccTotal As IDictionary(Of String, Single?)
            Private Property CInfraElecAccTotalGR As IDictionary(Of String, Single?)
            Private Property CInfraElecAccTotalGRAbs As IDictionary(Of String, Single?)
            Private Property CInfraElecAccUrban As IDictionary(Of String, Single?)
            Private Property CInfraElecAccRural As IDictionary(Of String, Single?)
            Private Property cINFRAELECACCRURALCOST As IDictionary(Of String, Single?)
            Private Property cINFRAELECACCURBANCOST As IDictionary(Of String, Single?)

            Private Property CInfraElec As IDictionary(Of String, Single?)
            Private Property CEnDemSH As IDictionary(Of String, Single?)
            Private Property CEnElecShrEnDem As IDictionary(Of String, Single?)
            Private Property CEnElecShrEnDemGR As IDictionary(Of String, Single?)

            Private Property CINFRAELECTRANLOSS As IDictionary(Of String, Single?)
            Private Property cInfraElecTranLossGR As IDictionary(Of String, Single?)
            Private Property CInfraElecProd As IDictionary(Of String, Single?)

            'Solid Fuel
            Private Property CENSOLFUEL As IDictionary(Of String, Single?)
            'Roads
            Private Property CRoadDensGR As IDictionary(Of String, Single?)
            Private Property CRoadDens As IDictionary(Of String, Single?)
            Private Property cRoadPavedPcntGR As IDictionary(Of String, Single?)
            Private Property cInfraRoadPavedPcnt As IDictionary(Of String, Single?)
            Private Property cINFRAROADPAVEDCOST As IDictionary(Of String, Single?)
            Private Property cINFRAROADUNPAVEDCOST As IDictionary(Of String, Single?)
            Private Property CRoadRAI As IDictionary(Of String, Single?)
            'Irrigation
            Private Property CLandIRAreaSat As IDictionary(Of String, Single?)
            Private Property CLandIrAreaEquip As IDictionary(Of String, Single?)
            Private Property CLandIrAreaEquipGR As IDictionary(Of String, Single?)
            Private Property cLANDIRCOST As IDictionary(Of String, Single?)
            'Telephones
            Private Property CTeleDens As IDictionary(Of String, Single?)
            Private Property CTeleDensGR As IDictionary(Of String, Single?)
            Private Property cINFRATELECOST As IDictionary(Of String, Single?)
            'ICT 
            Private Property CSpend As IDictionary(Of String, Single?)
            Private Property CMobil As IDictionary(Of String, Single?)
            Private Property CMobilGR As IDictionary(Of String, Single?)
            Private Property CBroadband As IDictionary(Of String, Single?)
            Private Property CBroadbandGR As IDictionary(Of String, Single?)
            Private Property CBroadMobil As IDictionary(Of String, Single?)
            Private Property CBroadMobilGR As IDictionary(Of String, Single?)
            Private Property CICTComputers As IDictionary(Of String, Single?)
            Private Property CICTINTNetHH As IDictionary(Of String, Single?)
            Private Property CICTIndex As IDictionary(Of String, Single?)
            Private Property cICTBROADCOST As IDictionary(Of String, Single?)
            Private Property cICTBROADMOBILCOST As IDictionary(Of String, Single?)
            Private Property cICTMOBILCOST As IDictionary(Of String, Single?)

            'jrs 2019/10/22
            'Private Property INFRAINDTRADCOMPMEANI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDTRADCOMPSDI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDICTZCOMPMEANI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDICTZCOMPSDI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDTRANCOMPMEANI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDTRANCOMPSDI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDELECCOMPMEANI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDELECCOMPSDI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDWATSANCOMPMEANI As IDictionary(Of Integer, Single?)
            'Private Property INFRAINDWATSANCOMPSDI As IDictionary(Of Integer, Single?)

            Private Property CInfraIndTranComp As IDictionary(Of String, Single?())
            Private Property CInfraIndElecComp As IDictionary(Of String, Single?())
            Private Property CInfraIndWatSanComp As IDictionary(Of String, Single?())
            Private Property CLNCompPerPop As IDictionary(Of String, Single?())
            Private Property CInfraIndICTZComp As IDictionary(Of String, Single?())
            Private Property CInfraICTIndComp As IDictionary(Of String, Single?())

            Private Property CINFRAINDICT As IDictionary(Of String, Single?)
            Private Property CINFRAINDICTZ As IDictionary(Of String, Single?)
            Private Property CINFRAINDTRAN As IDictionary(Of String, Single?)
            Private Property CINFRAINDELEC As IDictionary(Of String, Single?)
            Private Property CINFRAINDWATSAN As IDictionary(Of String, Single?)
            Private Property CINFRAINDTRAD As IDictionary(Of String, Single?)

            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CGDPPC As IDictionary(Of String, Single?)
            Private Property CPOP As IDictionary(Of String, Single?)
            Private Property CHHSIZE As IDictionary(Of String, Single?)
            Private Property CLandArea As IDictionary(Of String, Single?)
            Private Property CPOPURBAN As IDictionary(Of String, Single?)
            Private Property CEDYRSAGE25T As IDictionary(Of String, Single?)
            Private Property ENPOIL As IDictionary(Of String, Single?)
            Private Property ENPGAS As IDictionary(Of String, Single?)
            Private Property ENPCOAL As IDictionary(Of String, Single?)
            Private Property ENPHyd As IDictionary(Of String, Single?)
            Private Property ENPOthRen As IDictionary(Of String, Single?)
            Private Property LDCrop As IDictionary(Of String, Single?)
            Private Property CLIT As IDictionary(Of String, Single?)
            Private Property CEDSECENRGT As IDictionary(Of String, Single?)
            Private Property CEDTERENRGT As IDictionary(Of String, Single?)
            Private Property CGDP As IDictionary(Of String, Single?)
            Private Property CEDYRSAGE25TGR As IDictionary(Of String, Single?)
            Private Property CPOPURBGR As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            <ExportsAsset("cINFRAINVESTNEW")>
            <ExportsAsset("cINFRAINVESTMAINT")>
            <ExportsAsset("CInfraElecAccTotal")>
            <ExportsAsset("CInfraElecAccTotal")>
            <ExportsAsset("cINFRAROADUNPAVEDCOST")>
            <ExportsAsset("cINFRAROADPAVEDCOST")>
            <ExportsAsset("cINFRAELECGENCOST")>
            <ExportsAsset("cSANITATIONCOST")>
            <ExportsAsset("cWATSAFECOST")>
            <ExportsAsset("cINFRATELECOST")>
            <ExportsAsset("cICTMOBILCOST")>
            <ExportsAsset("cICTBROADCOST")>
            <ExportsAsset("cICTBROADMOBILCOST")>
            <ExportsAsset("cLANDIRCOST")>
            <ExportsAsset("cWATWASTETREATCOST")>
            <ExportsAsset("cSANITATIONIMPCOST")>
            <ExportsAsset("cWATSAFEIMPCOST")>
            <ExportsAsset("cINFRAELECACCRURALCOST")>
            <ExportsAsset("cINFRAELECACCURBANCOST")>
            <ExportsAsset("CSANITATION1")>
            <ExportsAsset("CLandIrAreaEquip")>
            <ExportsAsset("CWATSAFE3")>
            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Infrastructure - Please Wait")
                objWriter.Close()

                CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                CGDPPC = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPC")
                CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")
                CHHSIZE = cntx.Fetch(Of IDictionary(Of String, Single?))("CHHSIZE")
                CLandArea = cntx.Fetch(Of IDictionary(Of String, Single?))("CLandArea")
                CPOPURBAN = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOPURBAN")
                CEDYRSAGE25T = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDYRSAGE25T")
                ENPOIL = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPOIL")
                ENPGAS = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPGAS")
                ENPCOAL = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPCOAL")
                ENPHyd = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPHyd")
                ENPOthRen = cntx.Fetch(Of IDictionary(Of String, Single?))("ENPOthRen")
                LDCrop = cntx.Fetch(Of IDictionary(Of String, Single?))("LDCrop")
                CLIT = cntx.Fetch(Of IDictionary(Of String, Single?))("CLIT")
                CEDSECENRGT = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDSECENRGT")
                CEDTERENRGT = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDTERENRGT")
                CGDP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDP")
                CEDYRSAGE25TGR = cntx.Fetch(Of IDictionary(Of String, Single?))("CEDYRSAGE25TGR")
                CPOPURBGR = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOPURBGR")

                Me.CGDPPCPGR = New Dictionary(Of String, Single?)
                Me.CGDPPCPBase = New Dictionary(Of String, Single?)
                Me.cPOPGR = New Dictionary(Of String, Single?)
                Me.CPOPBase = New Dictionary(Of String, Single?)
                Me.CHHSizeGR = New Dictionary(Of String, Single?)
                Me.CINCOMELT1 = New Dictionary(Of String, Single?)
                Me.CGovEffect = New Dictionary(Of String, Single?)
                Me.CGovRegQual = New Dictionary(Of String, Single?)

                Me.CSANITATION1 = New Dictionary(Of String, Single?)
                Me.CSANITATION2 = New Dictionary(Of String, Single?)
                Me.CSANITATION3 = New Dictionary(Of String, Single?)

                Me.CSanitationGR1 = New Dictionary(Of String, Single?)
                Me.CSanitationGR2 = New Dictionary(Of String, Single?)
                Me.CSanitationGR3 = New Dictionary(Of String, Single?)

                Me.cSANITATIONCOST = New Dictionary(Of String, Single?)
                Me.cSANITATIONIMPCOST = New Dictionary(Of String, Single?)

                Me.cINFRAINVESTNEW = New Dictionary(Of String, Single?())
                Me.cINFRAINVESTMAINT = New Dictionary(Of String, Single?())

                '------------ Waste Water Treatment --------------------
                Me.CWATWASTE = New Dictionary(Of String, Single?)
                Me.CWATWASTETREAT = New Dictionary(Of String, Single?)
                Me.CWATWASTEGR = New Dictionary(Of String, Single?)
                Me.CWATWASTETREATGR = New Dictionary(Of String, Single?)
                Me.cWATWASTETREATCOST = New Dictionary(Of String, Single?)

                'Water connections
                Me.CWATSAFE1 = New Dictionary(Of String, Single?)
                Me.CWATSAFE2 = New Dictionary(Of String, Single?)
                Me.CWATSAFE3 = New Dictionary(Of String, Single?)

                Me.CWatSafeGR1 = New Dictionary(Of String, Single?)
                Me.CWatSafeGR2 = New Dictionary(Of String, Single?)
                Me.CWatSafeGR3 = New Dictionary(Of String, Single?)

                Me.cWATSAFECOST = New Dictionary(Of String, Single?)
                Me.cWATSAFEIMPCOST = New Dictionary(Of String, Single?)

                'Water Filtered
                Me.CWATERFILTERED1 = New Dictionary(Of String, Single?)
                Me.CWATERFILTERED2 = New Dictionary(Of String, Single?)
                Me.CWATERFILTERED3 = New Dictionary(Of String, Single?)

                'mti 2022/11/14 start
                'moved to PreprocessInfraWatSanNew
                'mti 2022/11/14 end

                Me.CInfraElecGenCapGR = New Dictionary(Of String, Single?)
                Me.CInfraElecGenCap = New Dictionary(Of String, Single?)
                Me.CInfraElecGenCapGRAbs = New Dictionary(Of String, Single?)

                Me.cINFRAELECGENCOST = New Dictionary(Of String, Single?)
                Me.CInfraElecAccTotal = New Dictionary(Of String, Single?)
                Me.CInfraElecAccTotalGR = New Dictionary(Of String, Single?)
                Me.CInfraElecAccTotalGRAbs = New Dictionary(Of String, Single?)
                Me.CInfraElecAccUrban = New Dictionary(Of String, Single?)
                Me.CInfraElecAccRural = New Dictionary(Of String, Single?)
                Me.cINFRAELECACCRURALCOST = New Dictionary(Of String, Single?)
                Me.cINFRAELECACCURBANCOST = New Dictionary(Of String, Single?)

                Me.CInfraElec = New Dictionary(Of String, Single?)
                Me.CEnDemSH = New Dictionary(Of String, Single?)
                Me.CEnElecShrEnDem = New Dictionary(Of String, Single?)
                Me.CEnElecShrEnDemGR = New Dictionary(Of String, Single?)

                Me.CINFRAELECTRANLOSS = New Dictionary(Of String, Single?)
                Me.cInfraElecTranLossGR = New Dictionary(Of String, Single?)
                Me.CInfraElecProd = New Dictionary(Of String, Single?)

                'Solid Fuel
                Me.CENSOLFUEL = New Dictionary(Of String, Single?)
                'Roads
                Me.CRoadDensGR = New Dictionary(Of String, Single?)
                Me.CRoadDens = New Dictionary(Of String, Single?)
                Me.cRoadPavedPcntGR = New Dictionary(Of String, Single?)
                Me.cInfraRoadPavedPcnt = New Dictionary(Of String, Single?)
                Me.cINFRAROADPAVEDCOST = New Dictionary(Of String, Single?)
                Me.cINFRAROADUNPAVEDCOST = New Dictionary(Of String, Single?)
                Me.CRoadRAI = New Dictionary(Of String, Single?)
                'Irrigation
                Me.CLandIRAreaSat = New Dictionary(Of String, Single?)
                Me.CLandIrAreaEquip = New Dictionary(Of String, Single?)
                Me.CLandIrAreaEquipGR = New Dictionary(Of String, Single?)
                Me.cLANDIRCOST = New Dictionary(Of String, Single?)
                'Telephones
                Me.CTeleDens = New Dictionary(Of String, Single?)
                Me.CTeleDensGR = New Dictionary(Of String, Single?)
                Me.cINFRATELECOST = New Dictionary(Of String, Single?)
                'ICT 
                Me.CSpend = New Dictionary(Of String, Single?)
                Me.CMobil = New Dictionary(Of String, Single?)
                Me.CMobilGR = New Dictionary(Of String, Single?)
                Me.CBroadband = New Dictionary(Of String, Single?)
                Me.CBroadbandGR = New Dictionary(Of String, Single?)
                Me.CBroadMobil = New Dictionary(Of String, Single?)
                Me.CBroadMobilGR = New Dictionary(Of String, Single?)
                Me.CICTComputers = New Dictionary(Of String, Single?)
                Me.CICTINTNetHH = New Dictionary(Of String, Single?)
                Me.CICTIndex = New Dictionary(Of String, Single?)
                Me.cICTBROADCOST = New Dictionary(Of String, Single?)
                Me.cICTBROADMOBILCOST = New Dictionary(Of String, Single?)
                Me.cICTMOBILCOST = New Dictionary(Of String, Single?)

                ReDim INFRAINDTRADCOMPMEANI(cntx.NInfraIndexTradComp)
                ReDim INFRAINDTRADCOMPSDI(cntx.NInfraIndexTradComp)
                ReDim INFRAINDICTZCOMPMEANI(cntx.NInfraIndexICTComp)
                ReDim INFRAINDICTZCOMPSDI(cntx.NInfraIndexICTComp)
                ReDim INFRAINDTRANCOMPMEANI(cntx.NInfraIndTranComp)
                ReDim INFRAINDTRANCOMPSDI(cntx.NInfraIndTranComp)
                ReDim INFRAINDELECCOMPMEANI(cntx.NInfraIndElecComp)
                ReDim INFRAINDELECCOMPSDI(cntx.NInfraIndElecComp)
                ReDim INFRAINDWATSANCOMPMEANI(cntx.NInfraIndWatSanComp)
                ReDim INFRAINDWATSANCOMPSDI(cntx.NInfraIndWatSanComp)

                Me.CInfraIndTranComp = New Dictionary(Of String, Single?())
                Me.CInfraIndElecComp = New Dictionary(Of String, Single?())
                Me.CInfraIndWatSanComp = New Dictionary(Of String, Single?())
                Me.CLNCompPerPop = New Dictionary(Of String, Single?())
                Me.CInfraIndICTZComp = New Dictionary(Of String, Single?())
                Me.CInfraICTIndComp = New Dictionary(Of String, Single?())

                Me.CINFRAINDICT = New Dictionary(Of String, Single?)
                Me.CINFRAINDICTZ = New Dictionary(Of String, Single?)
                Me.CINFRAINDTRAN = New Dictionary(Of String, Single?)
                Me.CINFRAINDELEC = New Dictionary(Of String, Single?)
                Me.CINFRAINDWATSAN = New Dictionary(Of String, Single?)
                Me.CINFRAINDTRAD = New Dictionary(Of String, Single?)

                'Infrastructure Cost and Maintenance Parameters
                ReadGlobalInfraParameters(cntx)

                Dim yrsG As Short() = cntx.GetYears("GDPPCP" & ConstantDollarsYear.ToString).OrderBy(Function(y) y).ToArray()
                Dim GDPPCPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("GDPPCP" & ConstantDollarsYear.ToString, yrsG)

                Dim CGDPCP_Curr As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CGDPCP_Prev As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim This_Yr As Short, CGDPCP_Val As Single?
                Dim Prev_Yr As Dictionary(Of String, Short), Curr_Yr As Dictionary(Of String, Short), TmpGDPPCPGR As Single?, CGDPR_ICount As Short
                Prev_Yr = New Dictionary(Of String, Short)
                Curr_Yr = New Dictionary(Of String, Short)

                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        CGDPPCPGR.Add(key, CSng(If(getAnnualGrowthRate(GDPPCPTbl, yrsG, key, cntx.FirstYear, 0.1, 0.005, 3, 10), 0.03)))
                        Prev_Yr.Add(key, 0)
                        Curr_Yr.Add(key, 0)
                        '----------------------------
                        'what do you do here for infrastructure physical stock change - 5 years ahead?
                        'needs to take GDPPCP Curr here
                        CGDPCP_Curr.Add(key, Nothing)
                        CGDPCP_Prev.Add(key, Nothing)
                        For Fct% = 0 To yrsG.Count - 1
                            This_Yr = yrsG(Fct%)
                            CGDPCP_Val = GDPPCPTbl(key)(Fct%)
                            If CGDPCP_Val.HasValue Then
                                If Not CGDPCP_Prev(key).HasValue Then
                                    If This_Yr > cntx.FirstYear + 10 Then Exit For 'too much ahead
                                    If This_Yr >= cntx.FirstYear - 5 Then 'not more than 5 years before firstyear history
                                        CGDPCP_Prev(key) = CGDPCP_Val / 1000
                                        Prev_Yr(key) = This_Yr
                                    End If
                                Else
                                    CGDPCP_Curr(key) = CGDPCP_Val / 1000
                                    Curr_Yr(key) = This_Yr
                                    If (Curr_Yr(key) - Prev_Yr(key)) >= 1 And CGDPCP_Prev(key) < CGDPCP_Curr(key) Then

                                        If Curr_Yr(key) - Prev_Yr(key) = 1 Then
                                            TmpGDPPCPGR = (CGDPCP_Curr(key) / CGDPCP_Prev(key)) - 1
                                            CGDPPCPGR(key) = Amin(CGDPPCPGR(key), TmpGDPPCPGR)
                                        End If

                                        Exit For

                                    End If

                                End If
                            End If
                        Next 'Next Year

                        If Not CGDPCP_Curr(key).HasValue Then
                            CGDPCP_Prev(key) = CGDPPCP(key)
                            Prev_Yr(key) = cntx.FirstYear
                            CGDPR_ICount = 3 'bring it from dataecon
                            CGDPCP_Curr(key) = CSng(CGDPCP_Prev(key) * ((1 + CGDPR_ICount / 100) ^ 5)) 'cgdpr in 0 to 100
                            Curr_Yr(key) = CShort(cntx.FirstYear + 5)
                        End If

                        CGDPPCPBase.Add(key, CGDPCP_Prev(key))
                    Next 'Next Country
                Else 'Forecast
                    For Each key In cntx.Regionalization.Keys
                        CGDPPCPGR.Add(key, CSng(If(getAnnualGrowthRate(GDPPCPTbl, yrsG, key, cntx.FirstYear, 0.1, 0.005, 6, 10), 0.03)))
                        Prev_Yr.Add(key, 0)
                        Curr_Yr.Add(key, 0)
                        '----------------------------
                        'what do you do here for infrastructure physical stock change - 5 years ahead?
                        'needs to take GDPPCP Curr here
                        CGDPCP_Curr(key) = Nothing
                        CGDPCP_Prev(key) = Nothing
                        For Fct% = yrsG.Count - 1 To 0 Step -1
                            This_Yr = yrsG(Fct%)
                            CGDPCP_Val = GDPPCPTbl(key)(Fct%)
                            If CGDPCP_Val.HasValue Then
                                'If there is base year data, use that as Curr_yr
                                If Not CGDPCP_Curr(key).HasValue Or Curr_Yr(key) > cntx.BaseYear Then
                                    If This_Yr < cntx.BaseYear - 10 Then Exit For 'too far back
                                    CGDPCP_Curr(key) = CGDPCP_Val / 1000
                                    Curr_Yr(key) = This_Yr
                                Else
                                    CGDPCP_Prev(key) = CGDPCP_Val / 1000
                                    Prev_Yr(key) = This_Yr
                                    If (Curr_Yr(key) - Prev_Yr(key)) >= 4 And CGDPCP_Prev(key) < CGDPCP_Curr(key) Then Exit For

                                End If
                            End If
                        Next 'Next Year

                        If Not CGDPCP_Prev(key).HasValue Then
                            CGDPCP_Curr(key) = CGDPPCP(key)
                            Curr_Yr(key) = cntx.BaseYear
                            CGDPR_ICount = 3 'bring it from dataecon
                            CGDPCP_Prev(key) = CSng(CGDPCP_Curr(key) / ((1 + CGDPR_ICount / 100) ^ 5)) 'cgdpr in 0 to 100
                            Prev_Yr(key) = CShort(cntx.BaseYear - 5)
                        End If

                        CGDPPCPBase.Add(key, CGDPCP_Curr(key))
                    Next 'Next Country
                End If 'if history else forecast

                '---------get the value of Population 5 or so years back ------------
                Dim yrs As Short() = cntx.GetYears("POP").OrderBy(Function(y) y).ToArray()
                Dim PopulationTbl As IDictionary(Of String, Single?()) = cntx.GetDice("POP", yrs)
                Dim TmpPopGR As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CPOP_Curr As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CPOP_Prev As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim yrIdx As Short?

                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        'population growth rate  - recent
                        cPOPGR.Add(key, CSng(If(getAnnualGrowthRate(PopulationTbl, yrs, key, cntx.FirstYear, , -0.05), -9999)))
                        TmpPopGR.Add(key, cPOPGR(key))
                        If TmpPopGR(key) = -9999 Then
                            TmpPopGR(key) = 0.015
                        End If
                        '------------------
                        'what do you do here for infrastructure physical stock change - 5 years ahead?

                        CPOP_Curr.Add(key, Nothing)
                        CPOP_Prev.Add(key, Nothing)

                        If yrs.Contains(Prev_Yr(key)) Then
                            yrIdx = getYearIdx(yrs, Prev_Yr(key))
                            CPOP_Prev(key) = PopulationTbl(key)(CShort(yrIdx))
                        End If

                        If Not CPOP_Prev(key).HasValue Or CPOP_Prev(key) = 0 Then
                            CPOP_Prev(key) = getYearOrEarliestWithinRange(PopulationTbl, yrs, key, cntx.FirstYear, 5)
                        End If

                        If Not CPOP_Prev(key).HasValue Then CPOP_Prev(key) = CPOP(key)

                        If yrs.Contains(Curr_Yr(key)) Then
                            yrIdx = getYearIdx(yrs, Curr_Yr(key))
                            CPOP_Curr(key) = PopulationTbl(key)(CShort(yrIdx))
                        End If

                        If Not CPOP_Curr(key).HasValue Then
                            CPOP_Curr(key) = CSng(CPOP_Prev(key) * ((1 + TmpPopGR(key)) ^ (Curr_Yr(key) - Prev_Yr(key))))
                        End If

                        CPOPBase.Add(key, CPOP_Prev(key))
                    Next
                Else 'Forecast
                    For Each key In cntx.Regionalization.Keys
                        'population growth rate  - recent
                        cPOPGR.Add(key, CSng(If(getAnnualGrowthRate(PopulationTbl, yrs, key, cntx.FirstYear, , -0.05), -9999)))
                        TmpPopGR.Add(key, cPOPGR(key))
                        If TmpPopGR(key) = -9999 Then
                            TmpPopGR(key) = 0.015
                        End If
                        '------------------
                        'what do you do here for infrastructure physical stock change - 5 years ahead?

                        CPOP_Curr.Add(key, Nothing)

                        If yrs.Contains(Curr_Yr(key)) Then
                            yrIdx = getYearIdx(yrs, Curr_Yr(key))
                            CPOP_Curr(key) = PopulationTbl(key)(CShort(yrIdx))
                        End If

                        If Not CPOP_Curr(key).HasValue Or (CPOP_Curr(key) = 0) Then
                            CPOP_Curr(key) = getMR(PopulationTbl, yrs, key)
                        End If

                        If yrs.Contains(Prev_Yr(key)) Then
                            yrIdx = getYearIdx(yrs, Prev_Yr(key))
                            CPOP_Prev(key) = PopulationTbl(key)(CShort(yrIdx))
                        End If

                        If Not CPOP_Prev(key).HasValue Then
                            CPOP_Prev(key) = CSng(CPOP_Curr(key) / ((1 + TmpPopGR(key)) ^ (Curr_Yr(key) - Prev_Yr(key))))
                        End If

                        CPOPBase.Add(key, CPOP_Curr(key))
                    Next
                End If 'if History else Forecast

                '---------Total Households in the BaseYear and the previous year -----------
                Dim yrsHH As Short() = cntx.GetYears("SeriesHouseholds").OrderBy(Function(y) y).ToArray()
                Dim hhtottbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesHouseholds", yrsHH)
                Dim CHHNo_Prev As Single?, CPOP4HH_Prev As Single?, CHHNo_Next As Single?, CPOP4HH_Next As Single?
                Dim CHHNo_Next_Year As Short, CHHSize_Next As Single?
                Dim CHHSize_Prev As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                For Each key In cntx.Regionalization.Keys
                    CHHNo_Prev = Nothing
                    CPOP4HH_Prev = Nothing

                    CHHNo_Next = Nothing
                    CPOP4HH_Next = Nothing

                    CHHSizeGR.Add(key, -9999)

                    If cntx.HistoricMode Then
                        CHHNo_Next = Nothing
                        CPOP4HH_Next = Nothing
                        For Fct% = 1 To yrsHH.Count - 1 'Omit first year hist
                            CHHNo_Next_Year = yrsHH(Fct%)
                            CHHNo_Next = hhtottbl(key)(Fct%)
                            If CHHNo_Next.HasValue Then Exit For
                            If CInt(CHHNo_Next_Year > cntx.FirstYear) >= 15 Then Exit For
                        Next

                        If CHHNo_Next.HasValue Then
                            If yrs.Contains(CHHNo_Next_Year) Then
                                yrIdx = getYearIdx(yrs, CHHNo_Next_Year)
                                CPOP4HH_Next = PopulationTbl(key)(CShort(yrIdx))
                            End If
                        End If
                        If CHHNo_Next.HasValue And CPOP4HH_Next.HasValue And CHHNo_Next_Year - cntx.FirstYear - 1 > 0 Then
                            'mti 2018/11/27 End
                            CHHSize_Next = CPOP4HH_Next / CHHNo_Next
                            'Debug.Print CountryName(key); CHHSIZE(key); CHHNo_Next_Year; CHHNo_Next_Year - FirstYearHistory - 1
                            CHHSizeGR(key) = ApplyLimits(CSng(((CHHSize_Next / CHHSIZE(key)) ^ (1 / (CHHNo_Next_Year - cntx.FirstYear - 1))) - 1), -0.05, 0.05) 'CHHSIZE(key) done in datapop and might have come from most recent
                            CHHSize_Prev.Add(key, CHHSIZE(key) / (1 + CHHSizeGR(key)))
                        Else
                            CHHSize_Prev.Add(key, CSng(CHHSIZE(key) / 0.995)) ' * 1.005 'a decrease in hhsize of .5% from 1959 to 1960
                        End If
                    Else 'Forecast

                        If yrsHH.Contains(CShort(cntx.BaseYear - 1)) Then
                            yrIdx = getYearIdx(yrsHH, CShort(cntx.BaseYear - 1))
                            CHHNo_Prev = hhtottbl(key)(CShort(yrIdx))
                        End If

                        If yrs.Contains(CShort(cntx.BaseYear - 1)) Then
                            yrIdx = getYearIdx(yrs, CShort(cntx.BaseYear - 1))
                            CPOP4HH_Prev = PopulationTbl(key)(CShort(yrIdx))
                        End If

                        If CHHNo_Prev.HasValue And CPOP4HH_Prev.HasValue Then
                            CHHSize_Prev.Add(key, CPOP4HH_Prev / CHHNo_Prev)
                            CHHSizeGR(key) = ApplyLimits(CHHSIZE(key) / CHHSize_Prev(key) - 1, -0.05, 0.05) 'CHHSIZE(key) done in datapop and might have come from most recent
                        Else
                            'mti 11/15/2013
                            CHHSize_Prev.Add(key, CSng(CHHSIZE(key) / 0.995)) ' * 1.005 'a decrease in hhsize of .5% from 2009 to 2010
                        End If
                    End If 'if hisotry else forecast
                Next

                '------------- Poverty -------------------------
                Dim TbName As String, ValHold As Single?
                Dim Estimate As SeriesEstimator
                If cntx.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2005) Versus Income Below One Dollar and 25 Cents per Day (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)  'baseyear (2010) required here, as it uses the function
                    Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                    For Each key In cntx.Regionalization.Keys
                        CINCOMELT1.Add(key, ApplyLimits(Estimate(key), 0, 70) / 100 * CPOP(key)) 'Convert from Percent to Index form
                    Next
                Else
                    'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2005) Versus Income Below One Dollar and 25 Cents per Day (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Income Below One Dollar and 25 Cents per Day (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    Dim yearsInc As Short() = cntx.GetYears("SeriesIncBelow1D25cWDI2011").OrderBy(Function(y) y).ToArray()
                    Dim incomelt1tbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesIncBelow1D25cWDI2011", yearsInc)

                    For Each key In cntx.Regionalization.Keys
                        ValHold = EstimateFromMostRecentRevBBH(CGDPPCP(key), cntx.BaseYear, incomelt1tbl, yearsInc,
                        GDPPCPTbl, yrsG, key, TbName$, cntx, 100, 0, "Div", 1000)
                        CINCOMELT1.Add(key, ValHold / 100 * CPOP(key)) 'Convert from Percent to Index form
                    Next
                End If
                '------------------ Poverty End ------------------

                'mti February 2011 Infarstructure - start
                '   Government Effectiveness - Reread for use in Infrastructure
                '   --------------------------------------------------------------------------------------------
                '*** Note: Any changes in preprocessing GovEffect in DataSociopol should be brought here ***
                If cntx.HistoricMode Then
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable = "SeriesGovernanceEffect"
                    Dim yrsGE = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl = cntx.GetDice(SeriesTable, yrsGE)
                    For Each key In cntx.Regionalization.Keys
                        CGovEffect.Add(key, getClosestYrVal(DictTbl, yrsGE, key, cntx.FirstYear, HistDist))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesGovernanceEffect", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CGovEffect.Add(item.Key, item.Value)
                    Next
                End If

                'jrs 2024/06/17
                TbName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Governance Effectiveness (" & BaseYear & ") Linear"
                TbName = getFunctionNameYrOrMstRct(TbName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'Estimate = cntx.GetRegression("GDP/Capita (PPP 2000) Versus Governance Effectiveness (2002) Linear").ToEstimator(CGDPPCP)
                Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                For Each key In cntx.Regionalization.Keys
                    If Not CGovEffect(key).HasValue Then
                        CGovEffect(key) = Estimate(key)
                    End If
                    CGovEffect(key) = ApplyLimits(CSng(CGovEffect(key) + 2.5), 0, 5)
                Next

                '   Government Regulation Quality - Reread for use in Infrastructure
                '   --------------------------------------------------------------------------------------------
                '*** Note: Any changes in preprocessing GovRegQual in DataSociopol should be brought here ***
                If cntx.HistoricMode Then
                    Estimate = cntx.GetRegression("GDP/Capita (PPP 2000) Versus Governance Reg Quality (2002) Linear").ToEstimator(CGDPPCP)
                    'mti 2019/04/29  Flex Hist Base Start
                    Dim SeriesTable = "SeriesGovernanceRegQual"
                    Dim yrsGR = cntx.GetYears(SeriesTable).OrderBy(Function(y) y).ToArray()
                    Dim DictTbl = cntx.GetDice(SeriesTable, yrsGR)
                    For Each key In cntx.Regionalization.Keys
                        Dim cValHold = getClosestYrVal(DictTbl, yrsGR, key, cntx.FirstYear, HistDist)
                        CGovRegQual.Add(key, ApplyLimits(CSng(If(cValHold, Estimate(key)) + 2.5), 0, 5))
                    Next
                    'mti 2019/04/29  Flex Hist Base End	
                Else
                    'jrs 2024/06/21
                    Dim TFName As String
                    TFName = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Governance Reg Quality (" & BaseYear & ") Linear"
                    TFName = getFunctionNameYrOrMstRct(TFName, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    'Estimate = cntx.GetRegression("GDP/Capita (PPP 2000) Versus Governance Reg Quality (2006) Linear").ToEstimator(CGDPPCP)
                    Estimate = cntx.GetRegression(TFName).ToEstimator(CGDPPCP)
                    For Each item In (From s In cntx.GetSlice("SeriesGovernanceRegQual", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Select Key = s.Key, Value = s.Value)
                        CGovRegQual.Add(item.Key, ApplyLimits(CSng(If(item.Value, Estimate(item.Key)) + 2.5), 0, 5))
                    Next
                End If

                '-------Water and sanitation   --------------
                '------- Sanitation   --------------
                Dim yrsSanHC As Short() = cntx.GetYears("SeriesWSSJMPSanitationTotalImproved").OrderBy(Function(y) y).ToArray()
                Dim SanitationCatHCTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPSanitationTotalImproved", yrsSanHC)
                Dim yrsSanIm As Short() = cntx.GetYears("SeriesWSSJMPSanitationTotalShared").OrderBy(Function(y) y).ToArray()
                Dim SanitationCatImprovedTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPSanitationTotalShared", yrsSanIm)
                Dim yrsSanNoneA As Short() = cntx.GetYears("SeriesWSSJMPSanitationTotalOpenDefecation").OrderBy(Function(y) y).ToArray()
                Dim SanitationCatNoneAOdTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPSanitationTotalOpenDefecation", yrsSanNoneA)
                Dim yrsSanNoneB As Short() = cntx.GetYears("SeriesWSSJMPSanitationTotalOtherUnimproved").OrderBy(Function(y) y).ToArray()
                Dim SanitationCatNoneBOuTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPSanitationTotalOtherUnimproved", yrsSanNoneB)
                Dim CSANITATIONNoneA As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CSANITATIONNoneB As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        CSANITATION3.Add(key, getYearOrEarliestWithinRange(SanitationCatHCTbl, yrsSanHC, key, cntx.FirstYear, 10))
                        CSANITATION2.Add(key, getYearOrEarliestWithinRange(SanitationCatImprovedTbl, yrsSanIm, key, cntx.FirstYear, 10))
                        CSANITATIONNoneA.Add(key, getYearOrEarliestWithinRange(SanitationCatNoneAOdTbl, yrsSanNoneA, key, cntx.FirstYear, 10))
                        CSANITATIONNoneB.Add(key, getYearOrEarliestWithinRange(SanitationCatNoneBOuTbl, yrsSanNoneB, key, cntx.FirstYear, 10))
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesWSSJMPSanitationTotalImproved", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Join s2 In cntx.GetSlice("SeriesWSSJMPSanitationTotalShared", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesWSSJMPSanitationTotalOpenDefecation", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesWSSJMPSanitationTotalOtherUnimproved", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s4.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value)
                        CSANITATION3.Add(item.Key, item.Value)
                        CSANITATION2.Add(item.Key, item.Val2)
                        CSANITATIONNoneA.Add(item.Key, item.Val3)
                        CSANITATIONNoneB.Add(item.Key, item.Val4)
                    Next
                End If

                Dim GDPPCPR As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim EDYRS25R As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each Key In cntx.Regionalization.Keys
                    GDPPCPR.Add(Key, CGDPPCP(Key) / (1 + CGDPPCPGR(Key)))
                    EDYRS25R.Add(Key, CEDYRSAGE25T(Key) / (1 + CEDYRSAGE25TGR(Key)))
                Next
                Estimate = cntx.GetRegression("Percent of People with No Access to Sanitation").Normalize("GDPPCP", "Educ25").ToEstimator(GDPPCPR, EDYRS25R)
                Dim Estimate2 As SeriesEstimator = cntx.GetRegression("Percent of People with Access to Improved Sanitation but no HC").Normalize("GDPPCP", "Educ25").ToEstimator(GDPPCPR, EDYRS25R)
                Dim cVal As Single?, ss1 As Single?, ss2 As Single?, EstimatedTotImproved As Single?, EstimatedHHCon As Single?, HCPlusImp As Single?
                Dim CSanitationGRHH As Dictionary(Of String, Single?), CSanitationImprovedGR As Dictionary(Of String, Single?), CSanitation_Curr As Single?, CPOP_Last As Single?, CSanitation_Prev As Single?
                CSanitationGRHH = New Dictionary(Of String, Single?)
                CSanitationImprovedGR = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    If CSANITATIONNoneA(key).HasValue And CSANITATIONNoneB(key).HasValue Then
                        CSANITATION1.Add(key, CSANITATIONNoneA(key) + CSANITATIONNoneB(key))
                    Else
                        If CSANITATION3(key).HasValue And CSANITATION2(key).HasValue Then
                            CSANITATION1.Add(key, 100 - (CSANITATION2(key) + CSANITATION3(key)))
                        Else
                            If CSANITATIONNoneA(key).HasValue Then
                                CSANITATION1.Add(key, CSANITATIONNoneA(key))
                            End If
                            If CSANITATIONNoneB(key).HasValue Then
                                CSANITATION1.Add(key, CSANITATIONNoneB(key))
                            End If
                        End If
                    End If

                    If Not CSANITATION3(key).HasValue And CSANITATION2(key).HasValue _
                    And CSANITATIONNoneA(key).HasValue And CSANITATIONNoneB(key).HasValue Then
                        CSANITATION3(key) = 100 - (CSANITATION2(key) + CSANITATIONNoneA(key) + CSANITATIONNoneB(key))
                    End If


                    If Not CSANITATION2(key).HasValue And CSANITATION3(key).HasValue _
                    And CSANITATIONNoneA(key).HasValue And CSANITATIONNoneB(key).HasValue Then
                        CSANITATION2(key) = 100 - (CSANITATION3(key) + CSANITATIONNoneA(key) + CSANITATIONNoneB(key))
                    End If

                    If Not CSANITATION2(key).HasValue Or Not CSANITATION3(key).HasValue Then
                        cVal = Estimate(key)
                        ss1 = CSng(Math.Exp(CSng(cVal)))
                        cVal = Estimate2(key)
                        ss2 = CSng(Math.Exp(CSng(cVal)))

                        EstimatedTotImproved = (ss2 + 1) / (1 + ss1 + ss2) * 100
                        EstimatedTotImproved = AMAX(Amin(100, EstimatedTotImproved), 0)

                        EstimatedHHCon = 1 / (1 + ss1 + ss2) * 100
                        EstimatedHHCon = AMAX(Amin(100.0F, EstimatedHHCon), 0.001F)

                        If Not CSANITATION3(key).HasValue Then
                            CSANITATION3(key) = EstimatedHHCon
                        End If

                        If Not CSANITATION2(key).HasValue Then
                            CSANITATION2(key) = AMAX(0, EstimatedTotImproved - CSANITATION3(key)) 'EstimatedHHCon
                        End If
                    End If

                    CSANITATION1(key) = 100 - CSANITATION3(key) - CSANITATION2(key)

                    'Sometime even if the other two adds up to 100 we get a tiny negative nuumber raising warning flag (e.g., Palestine) - to avoid
                    If CSANITATION3(key) + CSANITATION2(key) = 100 Then CSANITATION1(key) = 0

                    If CSANITATION1(key) < 0 Then
                        'MsgBox("Sanitaion (none) negative for " + CountryName(key) + " None value is " + CStr(CSANITATION(key, 1))) + ". Will be set to 0."
                        Debug.Print("Sanitaion (none) negative for " + key + " None value is " + CSANITATION1(key).ToString + ". Will be set to 0.")
                        CSANITATION1(key) = 0
                        HCPlusImp = CSANITATION3(key) + CSANITATION2(key)
                        CSANITATION3(key) = 100 * CSANITATION3(key) / HCPlusImp
                        CSANITATION2(key) = 100 * CSANITATION2(key) / HCPlusImp
                    End If

                    If cntx.HistoricMode Then
                        CSanitationGR3.Add(key, getAnnualGrowthRate(SanitationCatHCTbl, yrsSanHC, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))
                        CSanitationGR2.Add(key, getAnnualGrowthRate(SanitationCatImprovedTbl, yrsSanIm, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))
                    Else
                        CSanitationGR3.Add(key, getAnnualGrowthRate(SanitationCatHCTbl, yrsSanHC, key, cntx.BaseYear, 0.1, -0.02, 4, 5, 10, 10))
                        CSanitationGR2.Add(key, getAnnualGrowthRate(SanitationCatImprovedTbl, yrsSanIm, key, cntx.BaseYear, 0.1, -0.02, 4, 5, 10, 10))
                    End If

                    If Not CSanitationGR3(key).HasValue Then
                        CSanitationGR3(key) = -9999
                    End If
                    If Not CSanitationGR2(key).HasValue Then
                        CSanitationGR2(key) = -9999
                    End If
                    CSanitationGR1.Add(key, -9999)

                    If cntx.HistoricMode Then
                        'calculate annual change
                        'Percentage of people with household connection:  absolute annual change
                        CSanitationGRHH.Add(key, getAnnualGrowthRate(SanitationCatHCTbl, yrsSanHC, key, cntx.FirstYear, 10, 0.0005, 4, 5, 10, 10, True))
                        'SanitationCatHCTbl; SanitationCatImprovedTbl
                        CSanitationImprovedGR.Add(key, getAnnualGrowthRate(SanitationCatImprovedTbl, yrsSanIm, key, cntx.FirstYear, 10, 0.0005, 4, 5, 10, 10, True))
                    Else
                        'calculate annual change
                        'Percentage of people with household connection:  absolute annual change
                        CSanitationGRHH.Add(key, getAnnualGrowthRate(SanitationCatHCTbl, yrsSanHC, key, cntx.BaseYear, 10, 0.0005, 4, 5, 10, 10, True))

                        'SanitationCatHCTbl; SanitationCatImprovedTbl
                        CSanitationImprovedGR.Add(key, getAnnualGrowthRate(SanitationCatImprovedTbl, yrsSanIm, key, cntx.BaseYear, 10, 0.0005, 4, 5, 10, 10, True))
                    End If
                Next key

                'jrs 2024/06/06 TbName$ = "GDP/Capita (PPP 2000) Versus Sanitation % with Access to HC (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Sanitation % with Access to HC (" & CStr(cntx.BaseYear) & ") Log"
                'jrs 2024/06/06 TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                Estimate2 = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                Dim ValHold_Curr As Single?, ValHold_Prev As Single?, YrDiff As Single?, StkDiff As Single?, AnnDiff As Single?
                Dim CSanitationAdded As Single?, DeppcntSanit As Single?, CSanitationImproved_Curr As Single?
                Dim CSanitationImproved_Prev As Single?, CSanitationImprovedAdded As Single?, DeppcntSanitImp As Single?

                For Each key In cntx.Regionalization.Keys
                    If CSanitationGRHH(key).HasValue Then  'change from -9999

                        'mti August 22, 2011 - start
                        'constant percentage point change
                        CSanitation_Curr = CSANITATION3(key) * CPOP(key) / 100 / CHHSIZE(key)
                        CPOP_Last = CPOP(key) / (1 + TmpPopGR(key))
                        CSanitation_Prev = (CSANITATION3(key) - CSanitationGRHH(key)) * CPOP_Last / 100 / CHHSIZE(key)

                    Else  'null returned from growth rate of HH calculation
                        'mti - revise function

                        ValHold_Curr = Estimate(key)
                        ValHold_Prev = Estimate2(key)
                        CSanitation_Curr = ValHold_Curr * CPOP_Curr(key) / 100 / CHHSIZE(key) 'millions of connections
                        CSanitation_Prev = ValHold_Prev * CPOP_Prev(key) / 100 / CHHSIZE(key) 'millions of connections

                        YrDiff = Curr_Yr(key) - Prev_Yr(key)
                        StkDiff = CSanitation_Curr - CSanitation_Prev
                        AnnDiff = StkDiff / YrDiff

                        CSanitation_Prev = CSanitation_Curr - AnnDiff
                    End If


                    CSanitationAdded = AMAX(CSanitation_Curr - CSanitation_Prev, 0) 'million connections

                    If CSANITATION3(key) = 100 Then
                        CSanitationAdded = 0
                    End If

                    'mti August 22, 2011 -start
                    'readjust maintenance stock using current stock (from data or estimation)
                    CSanitation_Curr = CSANITATION3(key) * CPOP(key) / 100 / CHHSIZE(key)
                    CSanitation_Prev = CSanitation_Curr - CSanitationAdded

                    cSANITATIONCOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, sanitationcostupper, sanitationcostlower, CGDPPCP(key), cSANITATIONCOST(key))
                    Dim InfraVals(16) As Single?
                    cINFRAINVESTNEW.Add(key, InfraVals)
                    cINFRAINVESTNEW(key)(SanitationHH) = CSanitationAdded * cSANITATIONCOST(key) 'million connection * (1000 $/conn)= billion $
                    DeppcntSanit = CSng(Math.Exp(Math.Log(2) / sanitationlife) - 1)
                    Dim InfraVals2(16) As Single?
                    cINFRAINVESTMAINT.Add(key, InfraVals2)
                    cINFRAINVESTMAINT(key)(SanitationHH) = CSanitation_Prev * DeppcntSanit * cSANITATIONCOST(key) 'million connection * (1000 $/conn)= billion $

                    'Improved Sanitation Finance
                    If CSanitationImprovedGR(key).HasValue Then  'change from -9999
                        CSanitationImproved_Curr = CSANITATION2(key) * CPOP(key) / 100 '/ CHHSIZE(key) 'million pop
                        CPOP_Last = CPOP(key) / (1 + TmpPopGR(key))
                        CSanitationImproved_Prev = (CSANITATION2(key) - CSanitationImprovedGR(key)) * CPOP_Last / 100 '/ CHHSIZE(key)
                        CSanitationImprovedAdded = AMAX(CSanitationImproved_Curr - CSanitationImproved_Prev, 0) 'million people

                        If CSANITATION3(key) = 100 Then CSanitationImprovedAdded = 0

                    Else  'null returned from growth rate of improved calculation

                        If CSANITATION3(key) = 100 Then
                            CSanitationImprovedAdded = 0
                        Else
                            'If CSANITATION(key, 3) < 70 Then

                            'mti - can use a funaction now
                            'For low HH connection countries: add changes to improved using the same growth rate as HH Connection
                            CSanitationImprovedAdded = CSanitationAdded * CHHSIZE(key) * CSANITATION2(key) / CSANITATION3(key)
                            '                    Else
                            '                        'For high HH connection countries: new hh connection will most likely mean reduction aka 0 growth for improved
                            '                        CSanitationImprovedAdded = 0
                            '                    End If
                        End If
                        'readjust maintenance stock using current stock (from data or estimation)
                        CSanitationImproved_Curr = CSANITATION2(key) * CPOP(key) / 100 '/ CHHSIZE(key)
                        CSanitationImproved_Prev = AMAX(0, CSanitationImproved_Curr - CSanitationImprovedAdded)

                    End If

                    cSANITATIONIMPCOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, sanitationimpcostupper, sanitationimpcostlower, CGDPPCP(key), cSANITATIONIMPCOST(key))
                    cINFRAINVESTNEW(key)(SanitationImproved) = CSanitationImprovedAdded * cSANITATIONIMPCOST(key) 'note: HH conn cost is by hh, improved by ? (ask DSR)  'million connection * (1000 $/conn)= billion $
                    DeppcntSanitImp = CSng(Math.Exp(Math.Log(2) / sanitationimplife) - 1)
                    cINFRAINVESTMAINT(key)(SanitationImproved) = CSanitationImproved_Prev * DeppcntSanitImp * cSANITATIONIMPCOST(key) 'million connection * (1000 $/conn)= billion $

                    '------------ Sanitation Connection Costs End ---------------

                Next

                '------------ Waste Water Treatment --------------------
                Dim yrsWaste As Short() = cntx.GetYears("SeriesWasteWaterColConnect").OrderBy(Function(y) y).ToArray()
                Dim WasteColConTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWasteWaterColConnect", yrsWaste)
                Dim yrsWasteTreat As Short() = cntx.GetYears("SeriesWasteWaterTreatConnect").OrderBy(Function(y) y).ToArray()
                Dim WasteTreatColTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWasteWaterTreatConnect", yrsWasteTreat)

                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        CWATWASTE.Add(key, getYearOrEarliestWithinRange(WasteColConTbl, yrsSanHC, key, cntx.FirstYear, 10))
                        CWATWASTETREAT.Add(key, getYearOrEarliestWithinRange(WasteTreatColTbl, yrsSanIm, key, cntx.FirstYear, 10))
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesWasteWaterColConnect", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Join s2 In cntx.GetSlice("SeriesWasteWaterTreatConnect", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s2.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value)
                        CWATWASTE.Add(item.Key, item.Value)
                        CWATWASTETREAT.Add(item.Key, item.Val2)
                    Next
                End If

                Dim PopUrbanTerm As Single?, wastetreat_Curr As Single?, wastetreat_Prev As Single?, TmpGDPCPGR As Single?
                Dim CGDPCP_Last As Single?, WATWASTEPrev_rate As Single?, WATWASTECurr_rate As Single?, wastetreat_Curr_rate As Single?
                Dim wastetreat_Prev_rate As Single?, wastetreat_added As Single?, DeppcntWWTreat As Single?

                For Each key In cntx.Regionalization.Keys
                    'mti August 17, 2011 - start
                    If Not CWATWASTE(key).HasValue Then
                        PopUrbanTerm = 100 * CPOPURBAN(key) / CPOPBase(key)
                        CWATWASTE(key) = CSng(CSANITATION3(key) / (1 + Math.Exp(CDbl(2.339779 - 0.042719 * CGDPPCPBase(key) - 0.041923 * PopUrbanTerm))))
                    End If
                    'mti August 17, 2011 - end

                    CWATWASTE(key) = AMAX(0.001F, Amin(CWATWASTE(key), CSANITATION3(key)))

                    If Not CWATWASTETREAT(key).HasValue Then
                        'new model- watwastetreat as % of population
                        CWATWASTETREAT(key) = CSng(100 / (1 + Math.Exp(CSng(2.4821024086192 - 0.0382380937051429 * CGDPPCPBase(key) - 0.0288342684215034 * CWATWASTE(key)))))
                    End If

                    CWATWASTETREAT(key) = Amin(CWATWASTE(key), AMAX(0.001F, CWATWASTETREAT(key)))

                    'Growth rate in installing wastewater and wastewater treatment
                    If cntx.HistoricMode Then
                        CWATWASTEGR.Add(key, getAnnualGrowthRate(WasteColConTbl, yrsWaste, key, cntx.FirstYear, 0.1, 0.001, 3, 5, 10, 10))
                        CWATWASTETREATGR.Add(key, getAnnualGrowthRate(WasteTreatColTbl, yrsWasteTreat, key, cntx.FirstYear, 0.05, 0.001, 3, 5, 10, 10))
                    Else
                        CWATWASTEGR.Add(key, getAnnualGrowthRate(WasteColConTbl, yrsWaste, key, cntx.BaseYear, 0.1, 0.001, 3, 5, 20, 10))
                        CWATWASTETREATGR.Add(key, getAnnualGrowthRate(WasteTreatColTbl, yrsWasteTreat, key, cntx.BaseYear, 0.05, 0.001, 3, 5, 20, 10))
                    End If

                    If Not CWATWASTEGR(key).HasValue Then
                        CWATWASTEGR(key) = -9999 '0.001
                    End If

                    If Not CWATWASTETREATGR(key).HasValue Then
                        CWATWASTETREATGR(key) = -9999 '0.001
                    End If

                    '------------ Waste Water Connection Costs ---------------
                    wastetreat_Curr = (CWATWASTETREAT(key) / 100) * CPOP(key)  'million people - cost data as per person
                    If CWATWASTETREATGR(key) <> -9999 Then
                        wastetreat_Prev = CWATWASTETREAT(key) * CPOP(key) / (1 + CWATWASTETREATGR(key)) / (1 + TmpPopGR(key)) / 100 'million
                    Else

                        YrDiff = Curr_Yr(key) - Prev_Yr(key)
                        If YrDiff > 1 Then
                            TmpGDPCPGR = CSng((CGDPPCPBase(key) / CGDPCP_Prev(key)) ^ (1 / (Curr_Yr(key) - Prev_Yr(key) - 1)) - 1)
                            CGDPCP_Last = CGDPPCPBase(key) / (1 + TmpGDPCPGR)
                        Else
                            CGDPCP_Last = CGDPCP_Prev(key)
                        End If

                        'mti January 17, 2012 - start

                        'mti 2018/07/23 Start
                        If CWATWASTEGR(key) <> -9999 Then
                            WATWASTEPrev_rate = CWATWASTE(key) / (1 + CWATWASTEGR(key))
                            WATWASTECurr_rate = CWATWASTE(key)
                        Else
                            PopUrbanTerm = 100 * CPOPURBAN(key) / CPOPBase(key)
                            WATWASTEPrev_rate = CSng(CSANITATION3(key) / (1 + Math.Exp(CSng(2.339779 - 0.042719 * CGDPCP_Last - 0.041923 * PopUrbanTerm / (1 + TmpPopGR(key))))))

                            WATWASTECurr_rate = CSng(CSANITATION3(key) / (1 + Math.Exp(CSng(2.339779 - 0.042719 * CGDPPCPBase(key) - 0.041923 * PopUrbanTerm))))
                            WATWASTEPrev_rate = CWATWASTE(key) - (WATWASTECurr_rate - WATWASTEPrev_rate)
                            WATWASTECurr_rate = CWATWASTE(key)

                        End If
                        'calculate both current and previous from model (could save some steps by flagging model computation above)

                        '% connected with sanitation household = % connected with wastewater collection (same as sanitation HH)
                        'CSanitation_Curr_Rate = 100 * CHHSIZE(key) * CSanitation_Curr / CPOP_Curr
                        'WATWASTECurr_rate = CWATWASTE(key) ' 100 * CHHSIZE(key) * CSanitation_Curr / CPop(key)
                        'mti 2018/07/23 End


                        '% connected with wastewater collection (same as sanitation HH)
                        'WATWASTECurr_rate = CSanitation_Curr_Rate ' / (1 + Exp(2.339779 - 0.042719 * CGDPCP_Prev - 0.041923 * PopUrbanTerm))

                        '% of collection that is treated
                        'mti Sep 12, 2011 - start
                        'wastetreat_Curr_col_rate = WATWASTECurr_rate / (1 + Exp(1.036711 - 0.42925 * WATWASTECurr_rate))
                        'wastetreat_Curr_col_rate = 100 / (1 + Exp(2.4821024086192 - 3.82380937051429E-02 * CGDPPCPBase(key) - 2.88342684215034E-02 * WATWASTECurr_rate))

                        '% of population that is treated
                        wastetreat_Curr_rate = CSng(100 / (1 + Math.Exp(CSng(2.4821024086192 - 0.0382380937051429 * CGDPPCPBase(key) - 0.0288342684215034 * WATWASTECurr_rate))))

                        '% of population that is treated
                        'wastetreat_Curr_rate = wastetreat_Curr_col_rate * WATWASTECurr_rate / 100

                        'number of people that is treated (cost is per person)
                        wastetreat_Curr = (wastetreat_Curr_rate / 100) * CPOP_Curr(key) 'million people - cost data as per person


                        CPOP_Last = CPOP(key) / (1 + TmpPopGR(key))


                        'CSanitation_Prev_Rate = 100 * CHHSIZE(key) * CSanitation_Prev / cpop_prev
                        'WATWASTEPrev_rate = CSanitation_Prev_Rate ' / (1 + Exp(2.339779 - 0.042719 * CGDPCP_Prev - 0.041923 * PopUrbanTerm))

                        'WATWASTEPrev_rate = 100 * CHHSIZE(key) * CSanitation_Prev / CPOP_Last

                        'wastetreat_Prev_col_rate = WATWASTEPrev_rate / (1 + Exp(1.036711 - 0.42925 * WATWASTEPrev_rate))
                        'wastetreat_Prev_rate = 100 / (1 + Exp(2.4821024086192 - 3.82380937051429E-02 * CGDPCP_Prev - 2.88342684215034E-02 * WATWASTEPrev_rate))
                        'mti Sep 12, 2011 - end


                        'wastetreat_Prev_rate = wastetreat_Prev_col_rate * WATWASTEPrev_rate / 100
                        wastetreat_Prev_rate = CSng(100 / (1 + Math.Exp(CSng(2.4821024086192 - 0.0382380937051429 * CGDPCP_Last - 0.0288342684215034 * WATWASTEPrev_rate))))

                        'wastetreat_Prev = (wastetreat_Prev_rate / 100) * cpop_prev 'million people - cost data as per person
                        wastetreat_Prev = (wastetreat_Prev_rate / 100) * CPOP_Last 'million people - cost data as per person
                        '
                        '                YrDiff = Curr_Yr - Prev_Yr
                        '                StkDiff = wastetreat_Curr - wastetreat_Prev
                        '                AnnDiff = StkDiff / YrDiff
                        '
                        '                wastetreat_Prev = wastetreat_Curr - AnnDiff
                        'mti January 17, 2012 - end
                    End If

                    'mti August 17, 2011 - end
                    wastetreat_added = wastetreat_Curr - wastetreat_Prev 'million people

                    'End If


                    'readjust maintenance stock using current stock (from data or estimation)
                    wastetreat_Curr = (CWATWASTETREAT(key) / 100) * CPOP(key)  'million people - cost data as per person
                    wastetreat_Prev = wastetreat_Curr - wastetreat_added

                    'watwastetreatcostsingle - single global cost for waste water treatment

                    cWATWASTETREATCOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, watwastetreatcostupper, watwastetreatcostlower, CGDPPCP(key), cWATWASTETREATCOST(key))
                    cINFRAINVESTNEW(key)(WasteWater) = AMAX(wastetreat_added, 0) * cWATWASTETREATCOST(key) 'million connection * (1000 $/conn)= billion $
                    DeppcntWWTreat = CSng(Math.Exp(Math.Log(2) / watwastetreatlife) - 1)
                    cINFRAINVESTMAINT(key)(WasteWater) = wastetreat_Prev * DeppcntWWTreat * cWATWASTETREATCOST(key) 'million connection * (1000 $/conn)= billion $
                    'mti February 28, 2012 - end


                    '------------ Waste Water Treatment Costs End ---------------

                    '- Waste Water Treatment End ---------------
                Next

                '------------water connections-------------------
                Dim yrsWat As Short() = cntx.GetYears("SeriesWSSJMPWaterTotalPiped").OrderBy(Function(y) y).ToArray()
                Dim WatSafeTotalHCTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPWaterTotalPiped", yrsWat)
                Dim yrsWatImp As Short() = cntx.GetYears("SeriesWSSJMPWaterTotalOtherImproved").OrderBy(Function(y) y).ToArray()
                Dim WatSafeImprovedTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPWaterTotalOtherImproved", yrsWatImp)
                Dim yrsWatA As Short() = cntx.GetYears("SeriesWSSJMPWaterTotalOtherUnImproved").OrderBy(Function(y) y).ToArray()
                Dim WatSafeNoneAOuTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPWaterTotalOtherUnImproved", yrsWatA)
                Dim yrsWatB As Short() = cntx.GetYears("SeriesWSSJMPWaterTotalSurface").OrderBy(Function(y) y).ToArray()
                Dim WatSafeNoneBSurTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesWSSJMPWaterTotalSurface", yrsWatB)

                Dim CWATSAFENoneA As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim CWATSAFENoneB As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        CWATSAFE3.Add(key, getYearOrEarliestWithinRange(WatSafeTotalHCTbl, yrsWat, key, cntx.FirstYear, 10))
                        CWATSAFE2.Add(key, getYearOrEarliestWithinRange(WatSafeImprovedTbl, yrsWatImp, key, cntx.FirstYear, 10))
                        CWATSAFENoneA.Add(key, getYearOrEarliestWithinRange(WatSafeNoneAOuTbl, yrsWat, key, cntx.FirstYear, 10))
                        CWATSAFENoneB.Add(key, getYearOrEarliestWithinRange(WatSafeNoneBSurTbl, yrsWatImp, key, cntx.FirstYear, 10))
                    Next
                Else
                    For Each item In (From s In cntx.GetSlice("SeriesWSSJMPWaterTotalPiped", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Join s2 In cntx.GetSlice("SeriesWSSJMPWaterTotalOtherImproved", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesWSSJMPWaterTotalOtherUnImproved", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesWSSJMPWaterTotalSurface", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s4.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value)
                        CWATSAFE3.Add(item.Key, item.Value)
                        CWATSAFE2.Add(item.Key, item.Val2)
                        CWATSAFENoneA.Add(item.Key, item.Val3)
                        CWATSAFENoneB.Add(item.Key, item.Val4)
                    Next
                End If

                Estimate = cntx.GetRegression("Percent of People with No Access to Clean Water").Normalize("GDPPCP", "Educ25").ToEstimator(CGDPPCP, CEDYRSAGE25T)
                Estimate2 = cntx.GetRegression("Percent of People with Improved Access to Clean Water but No HC").Normalize("GDPPCP", "Educ25").ToEstimator(CGDPPCP, CEDYRSAGE25T)

                Dim calWATSAFE As Single?, CWatSafeGRHH As Single?, CWatSafeImprovedGR As Single?
                Dim CWatSafe_Curr As Single?, CWatsafe_Prev As Single?, CWatSafeAdded As Single?
                Dim DeppcntWater As Single?, CWatSafeImproved_Curr As Single?, CWatSafeImproved_Prev As Single?
                Dim CWatSafeImprovedAdded As Single?, DeppcntWaterImp As Single?

                'jrs 2024/06/14 TbName$ = "GDP/Capita (PPP 2000) Versus Water Safe % with Access to HC (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Water Safe % with Access to HC (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim EstimateWS As SeriesEstimator = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                Dim EstimateWS2 As SeriesEstimator = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                For Each key In cntx.Regionalization.Keys

                    CWATSAFE1.Add(key, Nothing)
                    If CWATSAFENoneA(key).HasValue And CWATSAFENoneB(key).HasValue Then
                        CWATSAFE1(key) = CWATSAFENoneA(key) + CWATSAFENoneB(key)
                    Else
                        If CWATSAFE3(key).HasValue And CWATSAFE2(key).HasValue Then
                            CWATSAFE1(key) = 100 - (CWATSAFE2(key) + CWATSAFE3(key))
                        Else
                            If CWATSAFENoneA(key).HasValue Then
                                CWATSAFE1(key) = CWATSAFENoneA(key)
                            End If
                            If CWATSAFENoneB(key).HasValue Then
                                CWATSAFE1(key) = CWATSAFENoneB(key)
                            End If
                        End If
                    End If

                    If Not CWATSAFE3(key).HasValue And CWATSAFE2(key).HasValue _
                And CWATSAFENoneA(key).HasValue And CWATSAFENoneB(key).HasValue Then
                        CWATSAFE3(key) = 100 - (CWATSAFE2(key) + CWATSAFENoneA(key) + CWATSAFENoneB(key))
                    End If


                    If Not CWATSAFE2(key).HasValue And CWATSAFE3(key).HasValue _
                And CWATSAFENoneA(key).HasValue And CWATSAFENoneB(key).HasValue Then
                        CWATSAFE2(key) = 100 - (CWATSAFE3(key) + CWATSAFENoneA(key) + CWATSAFENoneB(key))
                    End If

                    'mti July 18, 2011 - change watsan to new data - estimates from old tables yet - end

                    If Not CWATSAFE2(key).HasValue Or CWATSAFE3(key).HasValue Then
                        cVal = Estimate(key)
                        ss1 = CSng(Math.Exp(CSng(cVal)))

                        cVal = Estimate2(key)
                        ss2 = CSng(Math.Exp(CSng(cVal)))

                        If Not CWATSAFE2(key).HasValue Then

                            'mti August 22, 2011 - start
                            'added +1 because this is really p2 + p3, later it's separated
                            'CWATSAFE2(key) = (s2 + 1) / (1 + s1 + s2) * 100
                            'do not need to add 1 now
                            CWATSAFE2(key) = ss2 / (1 + ss1 + ss2) * 100
                            'mti August 22, 2011 - end
                        End If

                        If Not CWATSAFE3(key).HasValue Then
                            CWATSAFE3(key) = 1 / (1 + ss1 + ss2) * 100
                        End If


                    End If

                    CWATSAFE3(key) = AMAX(Amin(100.0F, CWATSAFE3(key)), 0.001F)
                    CWATSAFE2(key) = AMAX(Amin(100, CWATSAFE2(key)), 0)


                    If Not CWATSAFE1(key).HasValue Then
                        'check if the other two crosses 100; if yes, normalize the two to 100
                        If (CWATSAFE2(key) + CWATSAFE3(key)) > 100 Then
                            calWATSAFE = CWATSAFE3(key) + CWATSAFE2(key)
                            CWATSAFE3(key) = CWATSAFE3(key) / calWATSAFE * 100
                            CWATSAFE2(key) = CWATSAFE2(key) / calWATSAFE * 100
                        End If
                    Else
                        'normalize the three to 100
                        CWATSAFE1(key) = AMAX(Amin(100, CWATSAFE1(key)), 0)
                        'need to normalize to 100% now as data coming from three tables
                        calWATSAFE = CWATSAFE1(key) + CWATSAFE2(key) + CWATSAFE3(key)
                        CWATSAFE3(key) = CWATSAFE3(key) / calWATSAFE * 100
                        CWATSAFE2(key) = CWATSAFE2(key) / calWATSAFE * 100
                    End If

                    CWATSAFE1(key) = 100 - CWATSAFE2(key) - CWATSAFE3(key)

                    '------------ Water Connection Costs ---------------
                    If cntx.HistoricMode Then
                        CWatSafeGR3.Add(key, getAnnualGrowthRate(WatSafeTotalHCTbl, yrsWat, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))
                        CWatSafeGR2.Add(key, getAnnualGrowthRate(WatSafeImprovedTbl, yrsWatImp, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))
                        'Percentage of people with improved/HH/no connections to safe water: annual change
                        CWatSafeGRHH = getAnnualGrowthRate(WatSafeTotalHCTbl, yrsWat, key, cntx.FirstYear, 10, 0.0005, 4, 5, 10, 10, True)
                        CWatSafeImprovedGR = getAnnualGrowthRate(WatSafeImprovedTbl, yrsWatImp, key, cntx.FirstYear, 10, 0.0005, 4, 5, 10, 10, True)
                    Else
                        CWatSafeGR3.Add(key, getAnnualGrowthRate(WatSafeTotalHCTbl, yrsWat, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))
                        CWatSafeGR2.Add(key, getAnnualGrowthRate(WatSafeImprovedTbl, yrsWatImp, key, cntx.FirstYear, 0.1, -0.02, 4, 5, 10, 10))
                        'Percentage of people with improved/HH/no connections to safe water: annual change
                        CWatSafeGRHH = getAnnualGrowthRate(WatSafeTotalHCTbl, yrsWat, key, cntx.FirstYear, 10, 0.0005, 4, 5, 10, 10, True)
                        CWatSafeImprovedGR = getAnnualGrowthRate(WatSafeImprovedTbl, yrsWatImp, key, cntx.FirstYear, 10, 0.0005, 4, 5, 10, 10, True)
                    End If

                    If Not CWatSafeGR3(key).HasValue Then
                        CWatSafeGR3(key) = -9999
                    End If
                    If Not CWatSafeGR2(key).HasValue Then
                        CWatSafeGR2(key) = -9999
                    End If
                    CWatSafeGR1.Add(key, -9999)

                    If CWatSafeGRHH.HasValue Then
                        'mti August 22, 2011 - start
                        'constant percentage point change
                        CWatSafe_Curr = CWATSAFE3(key) * CPOP(key) / 100 / CHHSIZE(key)
                        CPOP_Last = CPOP(key) / (1 + TmpPopGR(key))
                        CWatsafe_Prev = (CWATSAFE3(key) - CWatSafeGRHH) * CPOP_Last / 100 / CHHSIZE(key)



                    Else  'null returned from growth rate calculation
                        ValHold_Curr = EstimateWS(key)
                        ValHold_Prev = EstimateWS2(key)
                        CWatSafe_Curr = ValHold_Curr * CPOP_Curr(key) / 100 / CHHSIZE(key) 'millions of connections
                        CWatsafe_Prev = ValHold_Prev * CPOP_Prev(key) / 100 / CHHSIZE(key) 'millions of connections

                        YrDiff = Curr_Yr(key) - Prev_Yr(key)
                        StkDiff = CWatSafe_Curr - CWatsafe_Prev
                        AnnDiff = StkDiff / YrDiff

                        CWatsafe_Prev = CWatSafe_Curr - AnnDiff
                    End If

                    CWatSafeAdded = AMAX(CWatSafe_Curr - CWatsafe_Prev, 0) 'million connections

                    If CWATSAFE3(key) = 100 Then CWatSafeAdded = 0

                    'mti August 22, 2011 -start
                    'readjust maintenance stock using current stock (from data or estimation)
                    CWatSafe_Curr = (CWATSAFE3(key) / 100) * CPOP(key) / CHHSIZE(key) 'million connections
                    CWatsafe_Prev = CWatSafe_Curr - CWatSafeAdded

                    cWATSAFECOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, watsafecostupper, watsafecostlower, CGDPPCP(key), cWATSAFECOST(key))
                    cINFRAINVESTNEW(key)(SafeWaterHH) = CWatSafeAdded * cWATSAFECOST(key) 'million connection * (1000 $/conn)= billion $
                    DeppcntWater = CSng(Math.Exp(Math.Log(2) / watsafelife) - 1)
                    cINFRAINVESTMAINT(key)(SafeWaterHH) = CWatsafe_Prev * DeppcntWater * cWATSAFECOST(key) 'million connection * (1000 $/conn)= billion $

                    'Improved Safe Water Finance
                    If CWatSafeImprovedGR.HasValue Then  'change from -9999
                        CWatSafeImproved_Curr = CWATSAFE2(key) * CPOP(key) / 100 '/ CHHSIZE(key) 'million pop
                        CPOP_Last = CPOP(key) / (1 + TmpPopGR(key))
                        CWatSafeImproved_Prev = (CWATSAFE2(key) - CWatSafeImprovedGR) * CPOP_Last / 100 '/ CHHSIZE(key)
                        CWatSafeImprovedAdded = AMAX(CWatSafeImproved_Curr - CWatSafeImproved_Prev, 0) 'million people
                        If CWATSAFE3(key) = 100 Then CWatSafeImprovedAdded = 0
                    Else  'null returned from growth rate of improved calculation

                        If CWATSAFE3(key) = 100 Then
                            CWatSafeImprovedAdded = 0
                        Else
                            'If CWATSAFE3(key) < 70 Then
                            'For low HH connection countries: add changes to improved using the same growth rate as HH Connection
                            CWatSafeImprovedAdded = CWatSafeAdded * CHHSIZE(key) * CWATSAFE2(key) / CWATSAFE3(key)
                            '                    Else
                            '                        'For high HH connection countries: new hh connection will most likely mean reduction aka 0 growth for improved
                            '                        CWatSafeImprovedAdded = 0
                            '                    End If
                        End If

                        'readjust maintenance stock using current stock (from data or estimation)
                        CWatSafeImproved_Curr = CWATSAFE2(key) * CPOP(key) / 100 '/ CHHSIZE(key)
                        CWatSafeImproved_Prev = AMAX(0, CWatSafeImproved_Curr - CWatSafeImprovedAdded)
                    End If

                    'mti February 28, 2012 - start

                    cWATSAFEIMPCOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, watsafeimpcostupper, watsafeimpcostlower, CGDPPCP(key), cWATSAFEIMPCOST(key))
                    cINFRAINVESTNEW(key)(SafeWaterImproved) = CWatSafeImprovedAdded * cWATSAFEIMPCOST(key) 'note: HH conn cost is by hh, improved by ? (ask DSR)  'million connection * (1000 $/conn)= billion $
                    DeppcntWaterImp = CSng(Math.Exp(Math.Log(2) / watsafeimplife) - 1)
                    cINFRAINVESTMAINT(key)(SafeWaterImproved) = CWatSafeImproved_Prev * DeppcntWaterImp * cWATSAFEIMPCOST(key) 'million connection * (1000 $/conn)= billion $
                    'mti February 28, 2012 -end

                    'mti February 28, 2012 - end

                    '------------ Water Connection Costs End ---------------
                Next

                '------------water filtered-------------------
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys
                        CWATERFILTERED1.Add(key, 0)
                        CWATERFILTERED2.Add(key, 0)
                        CWATERFILTERED3.Add(key, 0)
                    Next
                Else
                    Dim WatUnImpFilt As Single?, WatUnImpNoFilt As Single?, WatOthImpFilt As Single?, WatOthImpNoFilt As Single?, WatPipedFilt As Single?, WatPipedNoFilt As Single?
                    For Each item In (From s In cntx.GetSlice("SeriesWHO2012WaterUnImpFiltered", cntx.BaseYear, CoalescePolicy.MostRecent)
                                      Join s2 In cntx.GetSlice("SeriesWHO2012WaterUnImpNoFilter", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s2.Key
                                      Join s3 In cntx.GetSlice("SeriesWHO2012WaterOthImpFiltered", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s3.Key
                                      Join s4 In cntx.GetSlice("SeriesWHO2012WaterOthImpNoFilter", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s4.Key
                                      Join s5 In cntx.GetSlice("SeriesWHO2012WaterPipedFiltered", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s5.Key
                                      Join s6 In cntx.GetSlice("SeriesWHO2012WaterPipedNoFilter", cntx.BaseYear, CoalescePolicy.MostRecent) On s.Key Equals s6.Key
                                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value, Val4 = s4.Value, Val5 = s5.Value, Val6 = s6.Value)
                        WatUnImpFilt = item.Value
                        WatUnImpNoFilt = item.Val2
                        WatOthImpFilt = item.Val3
                        WatOthImpNoFilt = item.Val4
                        WatPipedFilt = item.Val5
                        WatPipedNoFilt = item.Val6
                        If WatUnImpFilt.HasValue And WatUnImpNoFilt.HasValue And WatUnImpFilt + WatUnImpNoFilt <> 0 Then
                            CWATERFILTERED1.Add(item.Key, ApplyLimits(100 * WatUnImpFilt / (WatUnImpFilt + WatUnImpNoFilt), 0, 100))
                        Else
                            CWATERFILTERED1.Add(item.Key, 0)
                        End If

                        If WatOthImpFilt.HasValue And WatOthImpNoFilt.HasValue And WatOthImpFilt + WatOthImpNoFilt <> 0 Then
                            CWATERFILTERED2.Add(item.Key, ApplyLimits(100 * WatOthImpFilt / (WatOthImpFilt + WatOthImpNoFilt), 0, 100))
                        Else
                            CWATERFILTERED2.Add(item.Key, 0)
                        End If
                        If WatPipedFilt.HasValue And WatPipedNoFilt.HasValue And WatPipedFilt + WatPipedNoFilt <> 0 Then
                            CWATERFILTERED3.Add(item.Key, ApplyLimits(100 * WatPipedFilt / (WatPipedFilt + WatPipedNoFilt), 0, 100))
                        Else
                            CWATERFILTERED3.Add(item.Key, 0)
                        End If
                    Next
                End If

                'mti electricity generation August 2010 - start
                Dim yrsEl As Short() = cntx.GetYears("SeriesEnElecTotalCapacityEIA").OrderBy(Function(y) y).ToArray()
                Dim InfraElecGenCapTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecTotalCapacityEIA", yrsEl)

                Dim iYr1 As Integer, iYr1Idx As Integer, iBaseYrIdx As Integer

                If cntx.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2000) Vs Electricity Gen Capacity per capita (" & CStr(cntx.FirstYear) & ") Logit"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.FirstYear, cntx)

                    For Each key In cntx.Regionalization.Keys
                        CInfraElecGenCapGR.Add(key, getAnnualGrowthRate(InfraElecGenCapTbl, yrsEl, key, cntx.FirstYear, 0.1, -0.02, 3, 10, 10, 3))

                        ValHold = Nothing

                        'will read total capacity and population data; and calculate or estimate generation capacity
                        ValHold = ReadOrEstimateDataPoint(CGDPPCP(key), cntx.FirstYear, InfraElecGenCapTbl, yrsEl,
                        GDPPCPTbl, yrsG, key, TbName$, cntx, , , PopulationTbl, yrs, "/", "/1000")

                        'convert back to total capacity: to make it consistent with subsequent calculation
                        If ValHold.HasValue Then
                            CInfraElecGenCap.Add(key, ValHold * CPOP(key))
                        Else
                            CInfraElecGenCap.Add(key, Nothing)
                        End If

                        CInfraElecGenCapGRAbs.Add(key, Nothing)
                        CInfraElecGenCapGRAbs(key) = getAnnualGrowthRate(InfraElecGenCapTbl, yrsEl, key, cntx.FirstYear, 0.1, -0.02, 5, 10, 10, 3, True)

                    Next
                Else

                    For Each key In cntx.Regionalization.Keys
                        CInfraElecGenCapGR.Add(key, getAnnualGrowthRate(InfraElecGenCapTbl, yrsEl, key, cntx.FirstYear, 0.1, -0.02, 3, 10, 15, 3))

                        CInfraElecGenCapGRAbs.Add(key, Nothing)
                        'mti 2019/11/01 CInfraElecGenCapGRAbs(key) = getAnnualGrowthRate(InfraElecGenCapTbl, yrsEl, key, cntx.FirstYear, 0.1, -0.02, 5, 10, 15, 3, True)
                        CInfraElecGenCapGRAbs(key) = getAnnualGrowthRate(InfraElecGenCapTbl, yrsEl, key, cntx.FirstYear, 0.1, -0.02, 3, 10, 15, 3, True)

                        '-------------------------------------------------
                        'note: Electricity data ends at 2008: We are using 2008 data to estimate 2010
                        'The following code needs to be modified when we get more recent data
                        '------------------------------------------------

                        iYr1 = getMstRctYr(InfraElecGenCapTbl, yrsEl, key)
                        CInfraElecGenCap.Add(key, Nothing)

                        'mti 2016/04/05 - start with a 2014 base and last data point at 2009 it was not reading any data

                        'If iYr1 > BaseYear - 5 Then
                        iYr1Idx = getYearIdx(yrsEl, CShort(iYr1))
                        If iYr1 > cntx.BaseYear - 10 Then
                            CInfraElecGenCap(key) = InfraElecGenCapTbl(key)(iYr1Idx)
                        End If
                        'mti 2016/04/05 - end

                        If CInfraElecGenCap(key).HasValue And CInfraElecGenCapGRAbs(key).HasValue Then

                            '                'estimate 2010 from 2009 data and growth rate
                            '                CInfraElecGenCap(key) = CInfraElecGenCap(key) * (1 + CInfraElecGenCapGR(key)) ^ (BaseYear - iYr1)

                            'estimate 2010 from 2009 data and absolute change
                            CInfraElecGenCap(key) = CInfraElecGenCap(key) + CInfraElecGenCapGRAbs(key)

                        Else
                            iBaseYrIdx = getYearIdx(yrsEl, cntx.BaseYear)
                            If iBaseYrIdx >= 0 Then
                                CInfraElecGenCap(key) = InfraElecGenCapTbl(key)(iBaseYrIdx)
                            End If
                            If Not CInfraElecGenCap(key).HasValue And iYr1Idx >= 0 Then CInfraElecGenCap(key) = InfraElecGenCapTbl(key)(iYr1Idx)
                        End If
                    Next
                End If

                'jrs 2024/06/14 TbName$ = "GDP/Capita (PPP 2000) Vs Electricity Gen Capacity per capita (" & CStr(cntx.BaseYear) & ") Logit"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Vs Electricity Gen Capacity per capita (" & CStr(cntx.BaseYear) & ") Logit"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim XYTABL As SeriesEstimator
                XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                For Each key In cntx.Regionalization.Keys
                    If Not CInfraElecGenCapGR(key).HasValue Then
                        CInfraElecGenCapGR(key) = -9999
                    End If

                    If Not CInfraElecGenCap(key).HasValue Then
                        CInfraElecGenCap(key) = XYTABL(key)
                    Else
                        CInfraElecGenCap(key) = CInfraElecGenCap(key) / CPOP(key)
                    End If

                    CInfraElecGenCap(key) = AMAX(0.0001F, Amin(20.0F, CInfraElecGenCap(key)))
                Next

                '------------ Electricity Generation Capacity Costs ---------------
                Dim cElecGenCap_Curr As Single?, cElecGenCap_Prev As Single?
                Dim TmpInfraElecGenCapGR As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)

                'jrs 2024/06/14
                'TbName$ = "GDP/Capita (PPP 2000) Vs Electricity Gen Capacity per capita (" & CStr(cntx.BaseYear) & ") Logit"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)

                Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                Estimate2 = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                Dim RatChange As Single?, DiffTime As Single?
                Dim cElecGenCapAdded As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim DeppcntElec As Single?

                For Each key In cntx.Regionalization.Keys
                    cElecGenCap_Curr = CInfraElecGenCap(key) * CPOP(key) 'CPOP_Curr
                    If CInfraElecGenCapGRAbs(key).HasValue And CInfraElecGenCapGR(key) > -9999 Then
                        TmpInfraElecGenCapGR.Add(key, CInfraElecGenCapGR(key))  'not changing this now
                        'do not need population growth rate as gross stock data and growth rate are available
                        'cElecGenCap_Prev = cElecGenCap_Curr / (1 + TmpInfraElecGenCapGR)  'million kw
                        cElecGenCap_Prev = cElecGenCap_Curr - CInfraElecGenCapGRAbs(key) 'million kw
                        'mti May 04, 2012 - end
                    Else  'null retured from growth rate calculation

                        '            End If
                        '
                        '            cElecGenCapAdded = getAnnualFlow(InfraElecGenCapTbl, BaseYear) 'in million kw
                        '            If cElecGenCapAdded <= 0 Then

                        '                ValHold = EstimateFromMostRecentRevBBH(CGDPPCP(key), BaseYear, InfraElecGenCapTbl, _
                        '                GDPPCPTbl, CountryName$(key), TbName$, 100, 0, "Div", 1000)
                        '                ValHold_Prev = EstimateFromMostRecentRevBBH(CGDPPCP(key), BaseYear - 5, InfraElecGenCapTbl, _
                        '                GDPPCPTbl, CountryName$(key), TbName$, 100, 0, "Div", 1000)
                        '                cElecGenCapAdded = AMAX(0, ((ValHold - ValHold_Prev) / 5)) * CPop(key) 'in million kw

                        ValHold_Curr = Estimate(key)
                        ValHold_Prev = Estimate2(key)

                        cElecGenCap_Curr = ValHold_Curr * CPOP_Curr(key)   'million kw
                        cElecGenCap_Prev = ValHold_Prev * CPOP_Prev(key)   'million kw

                        RatChange = cElecGenCap_Curr / cElecGenCap_Prev
                        DiffTime = AMAX(1, Curr_Yr(key) - Prev_Yr(key))
                        TmpInfraElecGenCapGR.Add(key, AMAX(0.0005, CSng(RatChange ^ (1 / DiffTime) - 1)))

                        ''mti June 14, 2012 - start
                        If CInfraElecGenCapGR(key) = -9999 Then
                            CInfraElecGenCapGR(key) = TmpInfraElecGenCapGR(key)
                        End If
                        'mti June 14, 2012 -end

                        'since estimations are used, use both stock growth and population growth rate

                        'mti 2019/11/01 start - absolute level - needs one growth rate only; take off population growth factor
                        cElecGenCap_Prev = cElecGenCap_Curr / (1 + TmpInfraElecGenCapGR(key)) '/ (1 + TmpPopGR(key)) 'million kw
                        'mti 2019/11/01 End

                    End If

                    cElecGenCapAdded.Add(key, cElecGenCap_Curr - cElecGenCap_Prev) 'million kw

                    cINFRAELECGENCOST.Add(key, Nothing)

                    CalcInfraUnitCost(13, 3, infraelecgencostupper, infraelecgencostlower, CGDPPCP(key), cINFRAELECGENCOST(key))
                    cINFRAINVESTNEW(key)(ElectricityGen) = AMAX(cElecGenCapAdded(key), 0) * cINFRAELECGENCOST(key) 'million kw * (1000 $/kw)= billion $
                    DeppcntElec = CSng(Math.Exp(Math.Log(2) / infraelecgenlife) - 1)
                    cINFRAINVESTMAINT(key)(ElectricityGen) = cElecGenCap_Prev * DeppcntElec * cINFRAELECGENCOST(key) 'million kw * (1000 $/kw)= billion $

                Next
                '------------ Electricity Generation Capacity Costs End ---------------

                'mti 03/18/10 Electricity Access - start
                Dim yrsElAcc As Short() = cntx.GetYears("SeriesEnElecAccessNational").OrderBy(Function(y) y).ToArray()
                Dim InfraElecAccTotalTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecAccessNational", yrsElAcc)

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CInfraElecAccTotal.Add(key, getYearOrEarliestWithinRange(InfraElecAccTotalTbl, yrsElAcc, key, cntx.FirstYear, 10)) 'InfraElecAccTotalTbl.Fields("Earliest")

                        CInfraElecAccTotalGR.Add(key, getAnnualGrowthRate(InfraElecAccTotalTbl, yrsElAcc, key, cntx.FirstYear, 0.1, 0.0001, 2, 10, 10, 3))
                        CInfraElecAccTotalGRAbs.Add(key, getAnnualGrowthRate(InfraElecAccTotalTbl, yrsElAcc, key, cntx.FirstYear, 10, 0.0001, 2, 10, 10, 3, True))
                    Else
                        CInfraElecAccTotalGR.Add(key, getAnnualGrowthRate(InfraElecAccTotalTbl, yrsElAcc, key, cntx.BaseYear, 0.1, 0.0001, 2, 10, 10, 3))

                        CInfraElecAccTotalGRAbs.Add(key, getAnnualGrowthRate(InfraElecAccTotalTbl, yrsElAcc, key, cntx.BaseYear, 10, 0.0001, 2, 10, 10, 3, True))

                        'mti 2021/05/10 start
                        'See if data for base year is available
                        CInfraElecAccTotal.Add(key, Nothing)
                        Dim MRval As Single? = getMR(InfraElecAccTotalTbl, yrsElAcc, key)
                        CInfraElecAccTotal(key) = getYearAroundOrRecent(InfraElecAccTotalTbl, yrsElAcc, key, MRval, cntx.BaseYear, False, 0)

                        If Not CInfraElecAccTotal(key).HasValue Then
                            'use growth rate
                            iYr1 = getMstRctYr(InfraElecAccTotalTbl, yrsElAcc, key)
                            iYr1Idx = getYearIdx(yrsElAcc, CShort(iYr1))

                            If iYr1 > BaseYear - 5 Then
                                CInfraElecAccTotal(key) = InfraElecAccTotalTbl(key)(iYr1Idx)
                                'estimate 2010 from 2008 data and growth rate
                                If CInfraElecAccTotalGR(key).HasValue Then
                                    CInfraElecAccTotal(key) = CSng(CInfraElecAccTotal(key) * (1 + CInfraElecAccTotalGR(key)) ^ (cntx.BaseYear - iYr1))
                                End If

                            End If
                        End If
                        'mti 2021/05/10 End


                        If Not CInfraElecAccTotal(key).HasValue Then
                            iBaseYrIdx = getYearIdx(yrsElAcc, cntx.BaseYear)
                            If iBaseYrIdx >= 0 Then
                                CInfraElecAccTotal(key) = InfraElecAccTotalTbl(key)(iBaseYrIdx)
                            End If
                            If Not CInfraElecAccTotal(key).HasValue And iYr1Idx >= 0 Then CInfraElecAccTotal(key) = InfraElecAccTotalTbl(key)(iYr1Idx)
                        End If
                    End If
                Next

                Dim yrsElAccUrb As Short() = cntx.GetYears("SeriesEnElecAccessUrban").OrderBy(Function(y) y).ToArray()
                Dim InfraElecAccUrbanTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecAccessUrban", yrsElAccUrb)

                Dim MaxGrowth As Single?

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CInfraElecAccUrban.Add(key, getYearOrEarliestWithinRange(InfraElecAccUrbanTbl, yrsElAccUrb, key, cntx.FirstYear, 10)) ' InfraElecAccUrbanTbl.Fields("Earliest")
                    Else
                        'mti 2021/05/10 start
                        CInfraElecAccUrban.Add(key, Nothing)
                        'See if data for base year is available
                        Dim MRval As Single? = getMR(InfraElecAccUrbanTbl, yrsElAccUrb, key)
                        CInfraElecAccUrban(key) = getYearAroundOrRecent(InfraElecAccUrbanTbl, yrsElAccUrb, key, MRval, BaseYear, False, 0)

                        If Not CInfraElecAccUrban(key).HasValue Then

                            iYr1 = getMstRctYr(InfraElecAccUrbanTbl, yrsElAccUrb, key)
                            iYr1Idx = getYearIdx(yrsElAccUrb, CShort(iYr1))

                            If iYr1 > BaseYear - 5 Then  'allow estimation (using total country growth rate) only for countries having data within last 5 years
                                CInfraElecAccUrban(key) = InfraElecAccUrbanTbl(key)(iYr1Idx)
                                '                    'estimate 2010 from 2008 data and growth rate
                                If CInfraElecAccTotalGR(key).HasValue Then
                                    If CInfraElecAccUrban(key) < 90 Then

                                        'mti April 05, 2012 - start
                                        'CInfraElecAccUrban(ICount%) = CInfraElecAccUrban(ICount%) * (1 + CInfraElecAccTotalGR) ^ (BaseYear - iYr1)
                                        MaxGrowth = CInfraElecAccTotalGRAbs(key) * (cntx.BaseYear - iYr1) 'limit to check too much growth (e.g., Bangladesh) coming from growth rate formulation
                                        CInfraElecAccUrban(key) = Amin(CInfraElecAccUrban(key) + MaxGrowth, CSng(CInfraElecAccUrban(key) * (1 + CInfraElecAccTotalGR(key)) ^ (cntx.BaseYear - iYr1)))
                                        'mti April 05, 2012 - end

                                    Else
                                        'don't allow more than 1% growth for high electricity access countries
                                        CInfraElecAccUrban(key) = Amin(CInfraElecAccUrban(key) + 1, CSng(CInfraElecAccUrban(key) * (1 + CInfraElecAccTotalGR(key)) ^ (cntx.BaseYear - iYr1)))
                                    End If
                                End If


                            End If

                        End If
                        'mti 2021/05/10 End


                        If Not CInfraElecAccUrban(key).HasValue Then
                            iBaseYrIdx = getYearIdx(yrsElAccUrb, cntx.BaseYear)
                            If iBaseYrIdx >= 0 Then
                                CInfraElecAccUrban(key) = InfraElecAccUrbanTbl(key)(iBaseYrIdx)
                            End If
                            If Not CInfraElecAccUrban(key).HasValue And iYr1Idx >= 0 Then CInfraElecAccUrban(key) = InfraElecAccUrbanTbl(key)(iYr1Idx)
                        End If

                    End If 'if historic else forecast
                Next

                Dim yrsElAccRur As Short() = cntx.GetYears("SeriesEnElecAccessRural").OrderBy(Function(y) y).ToArray()
                Dim InfraElecAccRuralTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecAccessRural", yrsElAccRur)
                Dim bEst As Boolean, tPOPRural As Single?

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CInfraElecAccRural.Add(key, getYearOrEarliestWithinRange(InfraElecAccRuralTbl, yrsElAccRur, key, cntx.FirstYear, 10)) ' InfraElecAccRuralTbl.Fields("Earliest")
                    Else
                        'mti 2021/05/10 start
                        'See if data for base year is available
                        CInfraElecAccRural.Add(key, Nothing)
                        Dim MRval As Single? = getMR(InfraElecAccRuralTbl, yrsElAccRur, key)
                        CInfraElecAccRural(key) = getYearAroundOrRecent(InfraElecAccRuralTbl, yrsElAccRur, key, MRval, BaseYear, False, 0)

                        If Not CInfraElecAccRural(key).HasValue Then

                            iYr1 = getMstRctYr(InfraElecAccRuralTbl, yrsElAccRur, key)
                            iYr1Idx = getYearIdx(yrsElAccRur, CShort(iYr1))


                            If iYr1 > BaseYear - 5 Then
                                CInfraElecAccRural(key) = InfraElecAccRuralTbl(key)(iYr1Idx)
                                'estimate 2010 from 2008 data and growth rate - no check of growth on rural, as high rural means high total
                                If CInfraElecAccTotalGR(key).HasValue Then
                                    CInfraElecAccRural(key) = CSng(CInfraElecAccRural(key) * (1 + CInfraElecAccTotalGR(key)) ^ (cntx.BaseYear - iYr1))
                                End If
                            End If

                        End If

                        'mti 2021/05/10 End

                        If Not CInfraElecAccRural(key).HasValue Then
                            iBaseYrIdx = getYearIdx(yrsElAccRur, cntx.BaseYear)
                            If iBaseYrIdx >= 0 Then
                                CInfraElecAccRural(key) = InfraElecAccRuralTbl(key)(iBaseYrIdx)
                            End If
                            If Not CInfraElecAccRural(key).HasValue And iYr1Idx >= 0 Then CInfraElecAccRural(key) = InfraElecAccRuralTbl(key)(iYr1Idx)
                        End If

                    End If 'if historic else forecast

                    '   Read and Fill Electricity Access  - Fill Missing Data
                    '   ---------------------------------------------------
                    If Not CInfraElecAccUrban(key).HasValue Then
                        'mti Sep 12, 2011 - start
                        bEst = False
                        If CInfraElecAccRural(key).HasValue And CInfraElecAccTotal(key).HasValue Then
                            tPOPRural = CPOPBase(key) - CPOPURBAN(key)
                            CInfraElecAccUrban(key) = (CInfraElecAccTotal(key) * CPOPBase(key) - CInfraElecAccRural(key) * tPOPRural) / CPOPURBAN(key)
                            If CInfraElecAccUrban(key) <= 0 Then
                                bEst = True
                            End If

                        Else
                            bEst = True
                        End If

                        If bEst Then
                            CInfraElecAccUrban(key) = CSng(100 / (1 + Math.Exp(CDbl(-(1.14416457907482 - 4.85840701059291 * CINCOMELT1(key) / CPOPBase(key) + 0.837209311546776 * CGovEffect(key))))))
                        End If
                    End If
                    CInfraElecAccUrban(key) = AMAX(1, Amin(100, CInfraElecAccUrban(key)))

                    '   Read and Fill Electricity Access  - Rural
                    '   -----------------------------------------
                    If Not CInfraElecAccRural(key).HasValue Then
                        'mti April 2011
                        bEst = False
                        If CInfraElecAccUrban(key).HasValue And CInfraElecAccTotal(key).HasValue Then
                            tPOPRural = CPOPBase(key) - CPOPURBAN(key)
                            CInfraElecAccRural(key) = (CInfraElecAccTotal(key) * CPOPBase(key) - CInfraElecAccUrban(key) * CPOPURBAN(key)) / tPOPRural
                            If CInfraElecAccRural(key) <= 0 Then
                                bEst = True
                            End If
                        Else
                            bEst = True
                        End If


                        If bEst Then
                            CInfraElecAccRural(key) = CSng(100 / (1 +
                            Math.Exp(CDbl(-(-0.499710126002115 - 6.92532112045222 * CINCOMELT1(key) / CPOPBase(key) + 0.958215164433785 * CGovEffect(key))))))

                        End If
                    End If
                    CInfraElecAccRural(key) = AMAX(1, Amin(CInfraElecAccUrban(key), CInfraElecAccRural(key)))

                    CInfraElecAccTotal(key) = (CInfraElecAccUrban(key) * CPOPURBAN(key) + CInfraElecAccRural(key) * (CPOPBase(key) - CPOPURBAN(key))) / CPOPBase(key)
                    CInfraElecAccTotal(key) = AMAX(1, Amin(100, CInfraElecAccTotal(key)))

                Next

                'finance for electricity access
                Dim PopUrban_Base As Single?, PopUrban_Last As Single?, PopTotal_Last As Single?
                Dim PopRural_Base As Single?, PopRural_Last As Single?
                Dim CInfraElecAccRural_Curr As Single?, CInfraElecAccRural_Prev As Single?
                Dim CInfraElecAccUrban_Curr As Single?, CInfraElecAccUrban_Prev As Single?
                Dim PopRural_Prop As Single?, PopUrban_Prop As Single?
                Dim CInfraElecAccRuralAdded As Single?, CInfraElecAccUrbanAdded As Single?
                Dim DeppcntElecAccRural As Single?, DeppcntElecAccUrban As Single?

                'jrs 2024/06/27 
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Rural Electricity Access (" & CStr(cntx.BaseYear) & ") Logistic"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim XYTABLRC As SeriesEstimator, XYTABLRP As SeriesEstimator
                XYTABLRC = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                XYTABLRP = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                'jrs 2024/06/27
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Urban Electricity Access (" & CStr(cntx.BaseYear) & ") Log"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim XYTABLUC As SeriesEstimator, XYTABLUP As SeriesEstimator
                XYTABLUC = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                XYTABLUP = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                For Each key In cntx.Regionalization.Keys
                    PopUrban_Base = CPOPURBAN(key)
                    PopUrban_Last = CSng(PopUrban_Base / (1 + 0.01 * CPOPURBGR(key)))

                    PopTotal_Last = CPOPBase(key) / (1 + TmpPopGR(key))

                    PopRural_Base = CPOPBase(key) - CPOPURBAN(key)
                    PopRural_Last = PopTotal_Last - PopUrban_Last 'PopRural_Base / (1 + TmpPopGR)

                    If CInfraElecAccTotalGR(key).HasValue Then  'change from -9999
                        '            If Not IsNull(CInfraElecAccTotalGRAbs) Then  'change from -9999


                        '                'Rural Electricity Access
                        CInfraElecAccRural_Curr = CInfraElecAccRural(key) * PopRural_Base / 100 / CHHSIZE(key)
                        CInfraElecAccRural_Prev = CInfraElecAccRural(key) * PopRural_Last / (1 + CInfraElecAccTotalGR(key)) / 100 / CHHSize_Prev(key) 'CHHSIZE(key)
                        '
                        '                'Urban Electricity Access
                        If CInfraElecAccUrban(key) < 90 Then
                            CInfraElecAccUrban_Curr = CInfraElecAccUrban(key) * PopUrban_Base / 100 / CHHSIZE(key)
                            CInfraElecAccUrban_Prev = CInfraElecAccUrban(key) * PopUrban_Last / (1 + CInfraElecAccTotalGR(key)) / 100 / CHHSize_Prev(key) 'CHHSIZE(key)
                        Else
                            CInfraElecAccUrban_Curr = CInfraElecAccUrban(key) * PopUrban_Base / 100 / CHHSIZE(key)
                            CInfraElecAccUrban_Prev = AMAX((CInfraElecAccUrban(key) / (1 + CInfraElecAccTotalGR(key))), (CInfraElecAccUrban(key) - 1)) * PopUrban_Last / 100 / CHHSize_Prev(key) 'CHHSIZE(key)
                        End If

                        '                If Not IsNull(InfraElecAccRuralPcnt_Added) Then
                        '                    CInfraElecAccRural_Curr = CInfraElecAccRural(key) * PopRural_Base / 100 / CHHSIZE(key)
                        '                    CInfraElecAccRural_Prev = CInfraElecAccRural_Curr - InfraElecAccRural_Added
                        '                End If
                        '
                        '                If Not IsNull(InfraElecAccUrbanPcnt_Added) Then
                        '                    CInfraElecAccUrban_Curr = CInfraElecAccUrban(key) * PopUrban_Base / 100 / CHHSIZE(key)
                        '                    CInfraElecAccUrban_Prev = CInfraElecAccUrban_Curr - InfraElecAccUrban_Added
                        '                End If



                    Else  'null returned from growth rate of HH calculation
                        'Rural Electricity Access
                        ValHold_Curr = XYTABLRC(key)
                        ValHold_Prev = XYTABLRP(key)

                        '                CInfraElecAccRural_Curr = ValHold_Curr * CPOP_Curr / 100 / CHHSIZE(key)  'millions of connections
                        '                CInfraElecAccRural_Prev = ValHold_Prev * CPOP_Prev / 100 / CHHSIZE(key) 'millions of connections

                        If Curr_Yr(key) = cntx.BaseYear And Curr_Yr(key) - Prev_Yr(key) = 1 Then
                            CInfraElecAccRural_Curr = ValHold_Curr * PopRural_Base / 100 / CHHSIZE(key)  'millions of connections
                            CInfraElecAccRural_Prev = ValHold_Prev * PopRural_Last / 100 / CHHSize_Prev(key) 'CHHSIZE(key) 'millions of connections
                        Else
                            PopRural_Prop = (CPOPBase(key) - CPOPURBAN(key)) / CPOPBase(key)
                            CInfraElecAccRural_Curr = ValHold_Curr * PopRural_Prop * CPOP_Curr(key) / 100 / CHHSIZE(key) 'millions of connections
                            CInfraElecAccRural_Prev = ValHold_Prev * PopRural_Prop * CPOP_Prev(key) / 100 / CHHSize_Prev(key) 'CHHSIZE(key) 'millions of connections
                        End If

                        YrDiff = Curr_Yr(key) - Prev_Yr(key)
                        StkDiff = CInfraElecAccRural_Curr - CInfraElecAccRural_Prev
                        AnnDiff = StkDiff / YrDiff

                        CInfraElecAccRural_Prev = CInfraElecAccRural_Curr - AnnDiff

                        'Urban Electricity Access
                        ValHold_Curr = XYTABLUC(key)
                        ValHold_Prev = XYTABLUP(key)

                        If Curr_Yr(key) = cntx.BaseYear And Curr_Yr(key) - Prev_Yr(key) = 1 Then
                            CInfraElecAccUrban_Curr = ValHold_Curr * PopUrban_Base / 100 / CHHSIZE(key)  'millions of connections
                            CInfraElecAccUrban_Prev = ValHold_Prev * PopUrban_Last / 100 / CHHSize_Prev(key) 'CHHSIZE(key) 'millions of connections
                        Else
                            PopUrban_Prop = CPOPURBAN(key) / CPOPBase(key)
                            CInfraElecAccUrban_Curr = ValHold_Curr * PopUrban_Prop * CPOP_Curr(key) / 100 / CHHSIZE(key) 'millions of connections
                            CInfraElecAccUrban_Prev = ValHold_Prev * PopUrban_Prop * CPOP_Prev(key) / 100 / CHHSize_Prev(key) 'CHHSIZE(key) 'millions of connections
                        End If

                        YrDiff = Curr_Yr(key) - Prev_Yr(key)
                        StkDiff = CInfraElecAccUrban_Curr - CInfraElecAccUrban_Prev
                        AnnDiff = StkDiff / YrDiff

                        CInfraElecAccUrban_Prev = CInfraElecAccUrban_Curr - AnnDiff

                    End If
                    CInfraElecAccRuralAdded = AMAX(CInfraElecAccRural_Curr - CInfraElecAccRural_Prev, 0) 'million connections
                    'readjust maintenance stock using current stock (from data or estimation)
                    CInfraElecAccRural_Curr = CInfraElecAccRural(key) * PopRural_Base / 100 / CHHSIZE(key)
                    CInfraElecAccRural_Prev = CInfraElecAccRural_Curr - CInfraElecAccRuralAdded


                    CInfraElecAccUrbanAdded = AMAX(CInfraElecAccUrban_Curr - CInfraElecAccUrban_Prev, 0) 'million connections
                    'readjust maintenance stock using current stock (from data or estimation)
                    CInfraElecAccUrban_Curr = CInfraElecAccUrban(key) * PopUrban_Base / 100 / CHHSIZE(key)
                    CInfraElecAccUrban_Prev = CInfraElecAccUrban_Curr - CInfraElecAccUrbanAdded

                    cINFRAELECACCRURALCOST.Add(key, Nothing)
                    CalcInfraUnitCost(13, 3, infraelecaccruralcostupper, infraelecaccruralcostlower, CGDPPCP(key), cINFRAELECACCRURALCOST(key))
                    cINFRAINVESTNEW(key)(ElectricityAccRural) = AMAX(CInfraElecAccRuralAdded, 0) * cINFRAELECACCRURALCOST(key) 'million kw * (1000 $/kw)= billion $
                    DeppcntElecAccRural = CSng(Math.Exp(Math.Log(2) / infraelecaccrurallife) - 1)
                    cINFRAINVESTMAINT(key)(ElectricityAccRural) = CInfraElecAccRural_Prev * DeppcntElecAccRural * cINFRAELECACCRURALCOST(key) 'million kw * (1000 $/kw)= billion $

                    cINFRAELECACCURBANCOST.Add(key, Nothing)
                    CalcInfraUnitCost(13, 3, infraelecaccurbancostupper, infraelecaccurbancostlower, CGDPPCP(key), cINFRAELECACCURBANCOST(key))
                    cINFRAINVESTNEW(key)(ElectricityAccUrban) = AMAX(CInfraElecAccUrbanAdded, 0) * cINFRAELECACCURBANCOST(key) 'million kw * (1000 $/kw)= billion $
                    DeppcntElecAccUrban = CSng(Math.Exp(Math.Log(2) / infraelecaccurbanlife) - 1)
                    cINFRAINVESTMAINT(key)(ElectricityAccUrban) = CInfraElecAccUrban_Prev * DeppcntElecAccUrban * cINFRAELECACCURBANCOST(key) 'million kw * (1000 $/kw)= billion $

                Next

                '-----------------Electricity Consumption Per Person : INFRAELEC ---------------------
                Dim yrsEle As Short() = cntx.GetYears("SeriesEnElecConsPerCap").OrderBy(Function(y) y).ToArray()
                Dim EnInfraElecTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecConsPerCap", yrsEle)

                TbName = "GDP/Capita (PPP 2000) Versus Kilowatt-Hours per Capita (2000) Linear"
                'jrs 2024/06/06 we have a 2001, and a MOST Recent version that we're not using, why not? this make it hard to use the Function to search, they would be picked up
                'TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Kilowatt-Hours per Capita (" & BaseYear.ToString & ") Linear"
                'TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                Dim CGDPPCPLT40 As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    CGDPPCPLT40.Add(key, Amin(40, CGDPPCP(key)))
                Next
                XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCPLT40)

                Dim CInfraElecGR As Single?

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CInfraElec.Add(key, getYearOrEarliestWithinRange(EnInfraElecTbl, yrsEle, key, cntx.FirstYear, 10)) 'EnInfraElecTbl.Fields("Earliest")
                        CInfraElecGR = getAnnualGrowthRate(EnInfraElecTbl, yrsEle, key, cntx.FirstYear, 0.05, -0.02, 4, 10, 10, 3)
                    Else
                        CInfraElecGR = getAnnualGrowthRate(EnInfraElecTbl, yrsEle, key, cntx.BaseYear, 0.05, -0.02, 4, 10, 10, 3)

                        '-------------------------------------------------
                        'note: Electricity data ends at 2008: We are using 2008 data to estimate 2010
                        'The following code needs to be modified when we get more recent data
                        CInfraElec.Add(key, Nothing)
                        If yrsEle.Contains(CShort(cntx.BaseYear - 2)) Then
                            yrIdx = getYearIdx(yrsEle, CShort(cntx.BaseYear - 2))
                            CInfraElec(key) = EnInfraElecTbl(key)(CShort(yrIdx))
                        End If

                        'jrs 2016/05/11 added and not isEmpty(CInfraElec(key)) Empty is not null so it was going in for Jamaica
                        If CInfraElec(key).HasValue And CInfraElecGR.HasValue Then
                            'estimate 2010 from 2008 data and growth rate
                            CInfraElec(key) = CSng(CInfraElec(key) * (1 + CInfraElecGR) ^ 2)
                        Else
                            ValHold = Nothing
                            iBaseYrIdx = getYearIdx(yrsEle, cntx.BaseYear)
                            If iBaseYrIdx >= 0 Then
                                ValHold = EnInfraElecTbl(key)(iBaseYrIdx)
                            End If
                            If Not ValHold.HasValue Then
                                yrIdx = getYearIdxMstRctWData(EnInfraElecTbl, key, yrsEle)
                                If yrIdx.HasValue Then
                                    If yrIdx >= 0 Then
                                        ValHold = EnInfraElecTbl(key)(CShort(yrIdx))
                                    End If
                                End If
                            End If
                            If ValHold.HasValue Then
                                CInfraElec(key) = ValHold
                            End If
                        End If
                    End If 'if historic mode else forecast

                    If Not CInfraElec(key).HasValue Then
                        CInfraElec(key) = XYTABL(key)
                    End If
                Next

                '------------ Shadow Energy Demand ---------------
                Dim yrsSH As Short() = cntx.GetYears("SeriesEnConTotalWDI").OrderBy(Function(y) y).ToArray()
                Dim EnDemSHTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnConTotalWDI", yrsSH)

                'jrs 2024/06/12 TbName$ = "GDP/Capita (PPP) Versus Energy Demand per GDP Dollar PPP (" & CStr(cntx.BaseYear) & ") Logarithmic"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Energy Demand per GDP Dollar PPP (" & CStr(cntx.BaseYear) & ") Logarithmic"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                Dim CGDPPCPLT50 As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    CGDPPCPLT50.Add(key, Amin(CGDPPCP(key), 50))
                Next

                XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCPLT50)

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CEnDemSH.Add(key, getYearOrEarliestWithinRange(EnDemSHTbl, yrsSH, key, cntx.FirstYear, 11))  'EnDemSHTbl.Fields("Earliest")
                    Else
                        CEnDemSH.Add(key, getYearOrMstRct(EnDemSHTbl, yrsSH, key, cntx.BaseYear))
                    End If 'if historic else forecast

                    If Not CEnDemSH(key).HasValue Then
                        'mti 2015/10/01 - start
                        'Call XYTABL(TbName$, CGDPPCP(key), ValHold)
                        ValHold = XYTABL(key)
                        ValHold = AMAX(0.0001F, ValHold)
                        'mti 2015/10/01 - end

                        CEnDemSH(key) = ValHold * CGDPPCP(key) * CPOP(key)
                    End If
                Next

                '----------Electricity Share of Energy------------
                Dim yrsEnElecShr As Short() = cntx.GetYears("SeriesEnElecShrEnDem").OrderBy(Function(y) y).ToArray()
                Dim InfraEnElecShrTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecShrEnDem", yrsEnElecShr)

                Dim ProjYears As Integer, sCEnElecShrEnDem As Single?, Fossilshare As Single?, NonFossilShare As Single?

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CEnElecShrEnDem.Add(key, getYearOrEarliestWithinRange(InfraEnElecShrTbl, yrsEnElecShr, key, cntx.FirstYear, 10)) ' InfraEnElecShrTbl.Fields("Earliest")
                        CEnElecShrEnDemGR.Add(key, getAnnualGrowthRate(InfraEnElecShrTbl, yrsEnElecShr, key, cntx.FirstYear, 0.05, , 5, 5, 10))
                    Else
                        CEnElecShrEnDemGR.Add(key, getAnnualGrowthRate(InfraEnElecShrTbl, yrsEnElecShr, key, cntx.BaseYear, 0.05, , 5, 5, 10))

                        '-------------------------------------------------
                        'note: Electricity data ends at 2008: We are using 2008 data to estimate 2010
                        'The following code needs to be modified when we get more recent data
                        CEnElecShrEnDem.Add(key, Nothing)
                        'sgh 2016/08/23 - Change from "BaseYear" to 2012 because the data ends in 2011
                        If yrsEnElecShr.Contains(CShort(cntx.BaseYear - 1)) Then
                            yrIdx = getYearIdx(yrsEnElecShr, 2011)
                            CEnElecShrEnDem(key) = InfraEnElecShrTbl(key)(CShort(yrIdx))
                            ProjYears% = 1
                        End If

                        If Not CEnElecShrEnDem(key).HasValue Then
                            If yrsEnElecShr.Contains(CShort(cntx.BaseYear - 2)) Then
                                yrIdx = getYearIdx(yrsEnElecShr, 2010)
                                CEnElecShrEnDem(key) = InfraEnElecShrTbl(key)(CShort(yrIdx))
                                ProjYears% = 2
                            End If
                        End If

                        If CEnElecShrEnDem(key).HasValue And CEnElecShrEnDemGR(key).HasValue Then
                            'estimate 2010 from 2008 data and growth rate
                            'mti June 24, 2012 - new cap growth to 1% per year and recalc growth rate
                            sCEnElecShrEnDem = CEnElecShrEnDem(key)
                            CEnElecShrEnDem(key) = Amin(sCEnElecShrEnDem + 2, CSng(CEnElecShrEnDem(key) * (1 + CEnElecShrEnDemGR(key)) ^ ProjYears%))
                            CEnElecShrEnDemGR(key) = (CEnElecShrEnDem(key) / sCEnElecShrEnDem - 1) / ProjYears% 'straight divide by 2 for 2 years

                        Else
                            CEnElecShrEnDem(key) = getYearOrMstRct(InfraEnElecShrTbl, yrsEnElecShr, key, cntx.BaseYear)
                        End If
                    End If 'if historic else forecast

                    If Not CEnElecShrEnDemGR(key).HasValue Then
                        CEnElecShrEnDemGR(key) = -9999
                    End If

                    If Not CEnElecShrEnDem(key).HasValue Then

                        Fossilshare = (ENPOIL(key) + ENPGAS(key) + ENPCOAL(key)) / CEnDemSH(key)
                        NonFossilShare = (ENPHyd(key) + ENPOthRen(key)) / CEnDemSH(key)

                        'jrs 2022/04/11
                        If Fossilshare = 0 And NonFossilShare = 0 Then
                            Fossilshare = 3 / 5
                            NonFossilShare = 2 / 5
                        End If

                        CEnElecShrEnDem(key) = CSng(Math.Round(Math.Exp(-0.021399 + 0.2753483 * Math.Log(CDbl(CGDPPCPBase(key))) + 0.4923844 * Math.Log(CDbl(CInfraElecAccTotal(key))) - 0.077159 * Math.Log(CDbl(Fossilshare)) + 0.1227198 * Math.Log(CDbl(NonFossilShare))), 6))
                    End If

                    If cntx.HistoricMode Then
                        CEnElecShrEnDem(key) = AMAX(0.2F, Amin(40, CEnElecShrEnDem(key)))
                    Else
                        CEnElecShrEnDem(key) = AMAX(0.5F, Amin(60, CEnElecShrEnDem(key)))
                    End If

                    If CEnElecShrEnDem(key) > 70 Then
                        Debug.Print("Warning: electricity use exceeds 70% of total energy use for " + key)
                    End If

                Next

                '   Read and Fill Electricity Transmission Loss
                '   ------------------------------------------------
                Dim yrsElecTranLoss As Short() = cntx.GetYears("SeriesEnElecTransLoss").OrderBy(Function(y) y).ToArray()
                Dim INFRAELECTRANLOSSTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnElecTransLoss", yrsElecTranLoss)

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CINFRAELECTRANLOSS.Add(key, getYearOrEarliestWithinRange(INFRAELECTRANLOSSTbl, yrsElecTranLoss, key, cntx.FirstYear, 10))  'INFRAELECTRANLOSSTbl.Fields("Earliest")
                        cInfraElecTranLossGR.Add(key, getAnnualGrowthRate(INFRAELECTRANLOSSTbl, yrsElecTranLoss, key, cntx.FirstYear, 0.1, -0.1, 3, 5, 10, 10))
                    Else 'forecast
                        CINFRAELECTRANLOSS.Add(key, getYearOrMstRct(INFRAELECTRANLOSSTbl, yrsElecTranLoss, key, cntx.BaseYear))
                        cInfraElecTranLossGR.Add(key, getAnnualGrowthRate(INFRAELECTRANLOSSTbl, yrsElecTranLoss, key, cntx.BaseYear, 0.1, -0.1, 3, 5, 10, 10))
                    End If 'if history else forecast

                    If Not cInfraElecTranLossGR(key).HasValue Then
                        cInfraElecTranLossGR(key) = -9999
                    End If

                    If Not CINFRAELECTRANLOSS(key).HasValue Then
                        CINFRAELECTRANLOSS(key) = CSng(Math.Exp(CDbl(3.1253418 - 0.124777 * CGovRegQual(key) - 0.025779 * CGDPPCP(key))))
                    End If

                    'Congo, DR(?) 92.3: 2007
                    'data shows 3% min for all but a few data unreliable countries
                    CINFRAELECTRANLOSS(key) = ApplyLimits(CINFRAELECTRANLOSS(key), 3, 90)
                Next

                '   Read and Fill Electricity Production
                '   ------------------------------------------
                Dim yrsElecProd As Short() = cntx.GetYears("SeriesEnProdElec").OrderBy(Function(y) y).ToArray()
                Dim InfraElecProdTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnProdElec", yrsElecProd)
                Dim yrsSolFuel As Short() = cntx.GetYears("SeriesEnvSolidFuels").OrderBy(Function(y) y).ToArray()
                Dim EnSolFuelTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesEnvSolidFuels", yrsSolFuel)

                Dim CInfraElecProdGR As Single?

                Dim POPURBNPCT As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    POPURBNPCT.Add(key, 100 * CPOPURBAN(key) / CPOP(key))
                Next

                Dim AnalFunc As SeriesEstimator
                AnalFunc = cntx.GetRegression("Percent of People using Primarily Solid Fuels (Full Model 2010)").Normalize("GDPPCP", "INFRAELECACC", "PopUrban%").ToEstimator(CGDPPCP, CInfraElecAccTotal, POPURBNPCT)

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CInfraElecProd.Add(key, getYearOrEarliestWithinRange(InfraElecProdTbl, yrsElecProd, key, cntx.FirstYear, 10)) ' InfraElecProdTbl.Fields("Earliest")
                        CInfraElecProdGR = getAnnualGrowthRate(InfraElecProdTbl, yrsElecProd, key, cntx.FirstYear, 0.05, -0.02, 4, 10, 10, 3)
                    Else 'forecast
                        CInfraElecProdGR = getAnnualGrowthRate(InfraElecProdTbl, yrsElecProd, key, cntx.BaseYear, 0.05, -0.02, 4, 10, 10, 3)
                        CInfraElecProd.Add(key, getYearOrMstRct(InfraElecProdTbl, yrsElecProd, key, cntx.BaseYear))
                    End If 'if historic else forecast

                    If Not CInfraElecProd(key).HasValue Then
                        '8766 = (365*24) + 6 ; 0.55 is the limiting value of the adjustment factor
                        'CInfraElecProd(key) = CInfraElecGenCap(key) * 8766 * 0.55
                        'the growth rate TmpInfraElecGenCapGR is on total gen capacity , i.e., on (per capita generation capacity*pop)
                        CInfraElecProd(key) = CSng(CInfraElecGenCap(key) * CPOP(key) * 8766 * 0.55 * 1000000 / (1 + TmpInfraElecGenCapGR(key))) 'kilowatt
                    End If

                    'convert from kw-hr to Gw-hr to conform with estimation equations
                    CInfraElecProd(key) = CInfraElecProd(key) / 1000000

                    CInfraElecProd(key) = AMAX(0.001F, CInfraElecProd(key))

                    '-----------Solid Fuel Use ------------
                    If cntx.HistoricMode Then
                        CENSOLFUEL.Add(key, getYearOrEarliestWithinRange(EnSolFuelTbl, yrsSolFuel, key, cntx.FirstYear, 10))  'EnSolFuelTbl.Fields("Earliest")
                    Else
                        CENSOLFUEL.Add(key, getYearOrMstRct(EnSolFuelTbl, yrsSolFuel, key, cntx.BaseYear))
                    End If

                    If Not CENSOLFUEL(key).HasValue Then
                        'jrs 2013/12/19 *** begin ***
                        'Call AnalFunc("Percent of People using Primarily Solid Fuels Revised", _
                        'Array(CGDPPCP(key), CInfraElecAccTotal(key)), cVal)
                        'DSR 2014/02/12 corrected to use urban percentage not urban population
                        cVal = AnalFunc(key)

                        'jrs 2013/12/19 CENSOLFUEL(key) = 100 / (Exp(cVal) + 1)
                        CENSOLFUEL(key) = CSng(100 / (Math.Exp(CDbl(-1 * cVal)) + 1))
                        'jrs 2013/12/19 *** end ***
                    End If

                Next

                '   Road Network
                '   ------------
                Dim yrsRoadDens As Short() = cntx.GetYears("SeriesRoadsTotalNetwork").OrderBy(Function(y) y).ToArray()
                Dim RoadDensTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesRoadsTotalNetwork", yrsRoadDens)

                Dim yrsRoadPaved As Short() = cntx.GetYears("SeriesRoadsPaved").OrderBy(Function(y) y).ToArray()
                Dim RoadPavedPcntTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesRoadsPaved", yrsRoadPaved)

                Dim yrsLandArea As Short() = cntx.GetYears("LandTotal").OrderBy(Function(y) y).ToArray()
                Dim LandAreaTbl As IDictionary(Of String, Single?()) = cntx.GetDice("LandTotal", yrsLandArea)

                Dim yrsRoadRAI As Short() = cntx.GetYears("SeriesRoadRuralAccessIndex").OrderBy(Function(y) y).ToArray()
                Dim RoadRAITbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesRoadRuralAccessIndex", yrsRoadRAI)

                If cntx.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2005) Versus RoadDensity (" & CStr(cntx.FirstYear) & ") Logit"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.FirstYear, cntx)
                Else

                End If

                Dim IncDensity As Single?, PopDensity As Single?, LndArea As Single?
                Dim IncTerm As Single?, PopTerm As Single?, LandTerm As Single?, RoadDensTerm As Single?

                Dim TmpRoadDensGR As Single?, CRoadDens_Curr As Single?, CRoadDens_Prev As Single?
                Dim TmpRoadPavedPcntGR As Single?, cInfraRoadPavedPcnt_Curr As Single?, cInfraRoadPavedPcnt_Prev As Single?
                Dim RoadPaved_Curr As Single?, RoadPaved_Prev As Single?, cPavedRoadsAdded As Single?
                Dim RoadTotal_Curr As Single?, RoadTotal_Prev As Single?, RoadUnPaved_Curr As Single?
                Dim RoadTotal_Added As Single?, cUnPavedRoadsAdded As Single?, RoadUnPaved_Prev As Single?
                Dim DeppcntRoad As Single?, DeppcntUnPavedRoad As Single?
                Dim IncomeDensityLand As Single?, TotalRoadsDensityPop As Single?

                Dim Func As String
                Func$ = "GDP/Capita (PPP 2000) Versus Rural Road Access Index (" & CStr(cntx.BaseYear) & ") Log"
                Func$ = getFunctionNameYrOrMstRct(Func$, cntx.BaseYear, cntx)

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CRoadDensGR.Add(key, Nothing)
                        CRoadDensGR(key) = getAnnualGrowthRate(RoadDensTbl, yrsRoadDens, key, cntx.FirstYear, 0.03, , , 10, , 40) 'getAnnualGrowthRate(RoadDensTbl, BaseYear)

                        ValHold = Nothing
                        'will read road and land data; and calculate or estimate road density
                        If CRoadDensGR(key).HasValue Then

                            ValHold = ReadOrEstimateDataPoint(CGDPPCP(key), cntx.FirstYear, RoadDensTbl, yrsRoadDens,
                            GDPPCPTbl, yrsG, key, TbName$, cntx, , , LandAreaTbl, yrsLandArea, "//1000", "/1000", , , CSng(CRoadDensGR(key)))
                        Else
                            ValHold = ReadOrEstimateDataPoint(CGDPPCP(key), cntx.FirstYear, RoadDensTbl, yrsRoadDens,
                            GDPPCPTbl, yrsG, key, TbName$, cntx, , , LandAreaTbl, yrsLandArea, "//1000", "/1000")
                            'first / divides roadstotal by landarea, second /1000 divides the quotient by 1000
                        End If

                        CRoadDens.Add(key, ValHold)
                    Else
                        CRoadDensGR.Add(key, getAnnualGrowthRate(RoadDensTbl, yrsRoadDens, key, cntx.BaseYear, 0.05, , 5, 10, 15))
                        iYr1 = getMstRctYr(RoadDensTbl, yrsRoadDens, key)  'most recent year with data for the current row
                        CRoadDens.Add(key, Nothing)

                        If iYr1 > cntx.BaseYear - 5 Then
                            iYr1Idx = getYearIdx(yrsRoadDens, CShort(iYr1))
                            CRoadDens(key) = RoadDensTbl(key)(iYr1Idx)
                        End If

                        If CRoadDens(key).HasValue And CRoadDensGR(key).HasValue Then

                            '                'estimate 2010 from 2009 data and growth rate
                            CRoadDens(key) = CSng(CRoadDens(key) * (1 + CRoadDensGR(key)) ^ (cntx.BaseYear - iYr1))


                            'estimate 2010 from 2009 data and absolute change
                            '                CRoadDens(key) = CRoadDens(key) + CRoadDensGRAbs

                        Else
                            CRoadDens(key) = getYearOrMstRct(RoadDensTbl, yrsRoadDens, key, cntx.BaseYear)
                        End If
                    End If

                    If Not CRoadDensGR(key).HasValue Then
                        CRoadDensGR(key) = -9999
                    End If

                    If Not CRoadDens(key).HasValue Then
                        'LandPerCap = CLandArea(key) / CPop(key)
                        'cap at 2000000 where IncDensity ^ 0.4834545 is little more than 1100
                        IncDensity = Amin(100000.0F, 1000 * CGDPPCPBase(key) * CPOPBase(key) / AMAX(CLandArea(key), 0.0001F)) '$/hecatare; GDPP in billion $;  Landrea in 10000 sq km = 1 mil hectare, converted to 1000 hecatare
                        PopDensity = CPOPBase(key) / (AMAX(0.00001F, CLandArea(key))) 'person/ha
                        LndArea = CLandArea(key)

                        CRoadDens(key) = CSng(0.0789105773431179 * (IncDensity ^ 0.4834545) * (PopDensity ^ 0.1827084) * (LndArea ^ -0.102367))

                    Else
                        'data table has total roads in km; convert to km/1000 ha
                        'mti 2014/05/28 - conversion already done

                        'mti 2014/08/20 - start
                        If cntx.HistoricMode Then
                            'conversion already done
                        Else
                            CRoadDens(key) = CRoadDens(key) / AMAX(0.01F, CLandArea(key)) / 1000
                        End If
                        'mti 2014/08/20 - end

                    End If

                    CRoadDens(key) = Amin(100, CRoadDens(key))

                    '   Road Paved
                    '   ------------
                    If cntx.HistoricMode Then
                        cRoadPavedPcntGR.Add(key, getAnnualGrowthRate(RoadPavedPcntTbl, yrsRoadPaved, key, cntx.FirstYear, 0.03, 0.0005, 4, 5, 10, 10))
                        'hard coding year to avoid taking later years, please revise if earlier data is available
                        cInfraRoadPavedPcnt.Add(key, getYearOrEarliestWithinRange(RoadPavedPcntTbl, yrsRoadPaved, key, cntx.FirstYear, 10)) ' RoadPavedPcntTbl.Fields("1990")
                    Else
                        cRoadPavedPcntGR.Add(key, getAnnualGrowthRate(RoadPavedPcntTbl, yrsRoadPaved, key, cntx.BaseYear, , 0.0005, 4, 5, 10, 10))
                        cInfraRoadPavedPcnt.Add(key, getYearOrMstRct(RoadPavedPcntTbl, yrsRoadPaved, key, cntx.BaseYear))
                    End If

                    If Not cRoadPavedPcntGR(key).HasValue Then
                        cRoadPavedPcntGR(key) = -9999
                    End If


                    If Not cInfraRoadPavedPcnt(key).HasValue Then
                        'formula: % of RoadsPaved = 100 * 1/(1 + e^1.0219486 * GDPperCapita^-0.832949 * Population^-0.756058 * LandArea^0.7260397 * TotalRoadDensity^0.2669518)
                        IncTerm = CSng(CGDPPCPBase(key) ^ -0.832949)
                        PopTerm = CSng(CPOPBase(key) ^ -0.756058)
                        LandTerm = CSng(CLandArea(key) ^ 0.7260397)
                        RoadDensTerm = CSng(CRoadDens(key) ^ 0.2669518)
                        cInfraRoadPavedPcnt(key) = CSng(100 / (1 + 2.77860387967124 * IncTerm * PopTerm * LandTerm * RoadDensTerm))

                    End If

                    cInfraRoadPavedPcnt(key) = AMAX(1, Amin(100, cInfraRoadPavedPcnt(key)))

                    '------------ Road costs ---------------
                    '------------Paved and Unpaved Road costs ---------------
                    If CRoadDensGR(key) > -9999 Then
                        TmpRoadDensGR = CRoadDensGR(key)
                    Else
                        LndArea = CLandArea(key)

                        'cap at 2000000 where IncDensity ^ 0.4834545 is little more than 1100
                        IncDensity = Amin(100000.0F, 1000 * CGDPCP_Curr(key) * CPOP_Curr(key) / AMAX(0.0001F, LndArea))  '$/hecatare; GDPP in billion $;  Landrea in 10000 sq km = 1 mil hectare, converted to 1000 hecatare
                        PopDensity = CPOP_Curr(key) / (AMAX(0.00001F, LndArea)) 'person/ha
                        CRoadDens_Curr = CSng(0.0789105773431179 * (IncDensity ^ 0.4834545) * (PopDensity ^ 0.1827084) * (LndArea ^ -0.102367))


                        IncDensity = Amin(100000.0F, 1000 * CGDPCP_Prev(key) * CPOP_Prev(key) / AMAX(0.0001F, LndArea))  '$/hecatare; GDPP in billion $;  Landrea in 10000 sq km = 1 mil hectare, converted to 1000 hecatare
                        PopDensity = CPOP_Prev(key) / (AMAX(0.00001F, LndArea)) 'person/ha
                        CRoadDens_Prev = CSng(0.0789105773431179 * (IncDensity ^ 0.4834545) * (PopDensity ^ 0.1827084) * (LndArea ^ -0.102367))

                        RatChange = CRoadDens_Curr / CRoadDens_Prev
                        DiffTime = AMAX(1, Curr_Yr(key) - Prev_Yr(key))
                        TmpRoadDensGR = AMAX(0.0005, CSng(RatChange ^ (1 / DiffTime) - 1))

                        '                If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                        '                    TmpRoadDensGR = Amin(0.03, TmpRoadDensGR)
                        '                End If

                    End If

                    CRoadDens_Curr = Nothing
                    'mti June 14, 2012 - end

                    If yrsRoadDens.Contains(Curr_Yr(key)) Then
                        iYr1Idx = getYearIdx(yrsRoadDens, Curr_Yr(key))
                        CRoadDens_Curr = RoadDensTbl(key)(iYr1Idx)
                    End If
                    'mti July 05, 2011 - end

                    If Not CRoadDens_Curr.HasValue Then
                        CRoadDens_Curr = CRoadDens(key)
                    Else
                        CRoadDens_Curr = CRoadDens_Curr / AMAX(0.01F, CLandArea(key)) / 1000
                    End If

                    'mti July 05, 2011 - Start

                    'mti June 14, 2012 -start
                    CRoadDens_Prev = Nothing
                    'mti June 14, 2012 - end

                    'CRoadDens_Prev = RoadDensTbl.Fields(CStr(Prev_Yr))
                    If yrsRoadDens.Contains(Prev_Yr(key)) Then
                        iYr1Idx = getYearIdx(yrsRoadDens, Prev_Yr(key))
                        CRoadDens_Prev = RoadDensTbl(key)(iYr1Idx)
                    End If
                    'mti July 05, 2011 - end

                    If Not (CRoadDens_Prev.HasValue) Then
                        CRoadDens_Prev = CRoadDens_Curr / (1 + TmpRoadDensGR) 'CRoadDens(key)
                    Else
                        CRoadDens_Prev = CRoadDens_Prev / AMAX(0.01F, CLandArea(key)) / 1000
                    End If

                    If cRoadPavedPcntGR(key) > -9999 Then
                        TmpRoadPavedPcntGR = cRoadPavedPcntGR(key)

                    Else
                        'formula: % of RoadsPaved = 100 * 1/(1 + e^1.0219486 * GDPperCapita^-0.832949 * Population^-0.756058 * LandArea^0.7260397 * TotalRoadDensity^0.2669518)
                        LandTerm = CSng(CLandArea(key) ^ 0.7260397)
                        'infrahist 2013/11/21 start here
                        IncTerm = CSng(CGDPCP_Curr(key) ^ -0.832949)
                        PopTerm = CSng(CPOP_Curr(key) ^ -0.756058)
                        RoadDensTerm = CSng(CRoadDens_Curr ^ 0.2669518)
                        cInfraRoadPavedPcnt_Curr = Amin(100, AMAX(1, CSng(100 / (1 + 2.77860387967124 * IncTerm * PopTerm * LandTerm * RoadDensTerm))))

                        IncTerm = CSng(CGDPCP_Prev(key) ^ -0.832949)
                        PopTerm = CSng(CPOP_Prev(key) ^ -0.756058)
                        RoadDensTerm = CSng(CRoadDens_Prev ^ 0.2669518)
                        cInfraRoadPavedPcnt_Prev = Amin(100, AMAX(1, CSng(100 / (1 + 2.77860387967124 * IncTerm * PopTerm * LandTerm * RoadDensTerm))))

                        RatChange = cInfraRoadPavedPcnt_Curr / cInfraRoadPavedPcnt_Prev
                        DiffTime = AMAX(1, Curr_Yr(key) - Prev_Yr(key))

                        TmpRoadPavedPcntGR = AMAX(0.0005, CSng(RatChange ^ (1 / DiffTime) - 1))


                        '                    If frm_MainMenu.IFsGUIctrl1.cHistoricBase = True Then
                        '                        TmpRoadPavedPcntGR = Amin(0.03, TmpRoadPavedPcntGR)
                        '                    End If


                    End If

                    If CRoadDensGR(key) = -9999 Then CRoadDensGR(key) = TmpRoadDensGR
                    If cRoadPavedPcntGR(key) = -9999 Then cRoadPavedPcntGR(key) = TmpRoadPavedPcntGR

                    'mti February 2011 Infarstructure - end
                    '--Paved Roads ---
                    RoadPaved_Curr = CRoadDens(key) * CLandArea(key) * cInfraRoadPavedPcnt(key) / 100 '1000 km
                    RoadPaved_Prev = CRoadDens(key) * CLandArea(key) * cInfraRoadPavedPcnt(key) / 100 _
                                / (1 + TmpRoadDensGR) / (1 + TmpRoadPavedPcntGR) 'in 1000 km
                    cPavedRoadsAdded = RoadPaved_Curr - RoadPaved_Prev 'in 1000 km

                    '---Unpaved roads----
                    RoadTotal_Curr = CRoadDens(key) * CLandArea(key)  '1000 km
                    RoadUnPaved_Curr = RoadTotal_Curr - RoadPaved_Curr '1000 km
                    RoadTotal_Prev = CRoadDens(key) * CLandArea(key) / (1 + TmpRoadDensGR)  'in 1000 km
                    RoadTotal_Added = RoadTotal_Curr - RoadTotal_Prev
                    'mti 2019/11/01 start
                    cPavedRoadsAdded = Amin(AMAX(0.95F, cInfraRoadPavedPcnt(key) / 100) * RoadTotal_Added, cPavedRoadsAdded) 'there could be a country with no unpaved roads
                    'mti 2019/11/01 End
                    cUnPavedRoadsAdded = RoadTotal_Added - cPavedRoadsAdded
                    RoadUnPaved_Prev = RoadTotal_Prev - RoadPaved_Prev

                    cINFRAROADPAVEDCOST.Add(key, Nothing)
                    CalcInfraUnitCost(13, 3, infraroadpavedcostupper, infraroadpavedcostlower, CGDPPCP(key), cINFRAROADPAVEDCOST(key))
                    cINFRAINVESTNEW(key)(RoadPaved) = AMAX(cPavedRoadsAdded, 0) * cINFRAROADPAVEDCOST(key) '(million $/km) * 1000 km = billion $
                    DeppcntRoad = CSng(Math.Exp(Math.Log(2) / infraroadpavedlife) - 1)
                    cINFRAINVESTMAINT(key)(RoadPaved) = RoadPaved_Prev * DeppcntRoad * cINFRAROADPAVEDCOST(key) '(million $/km) * 1000 km = billion $

                    '---Unpaved roads financing
                    cINFRAROADUNPAVEDCOST.Add(key, Nothing)
                    CalcInfraUnitCost(13, 3, infraroadunpavedcostupper, infraroadunpavedcostlower, CGDPPCP(key), cINFRAROADUNPAVEDCOST(key))
                    cINFRAINVESTNEW(key)(RoadUnpaved) = AMAX(cUnPavedRoadsAdded, 0) * cINFRAROADUNPAVEDCOST(key) '(million $/km) * 1000 km = billion $
                    DeppcntUnPavedRoad = CSng(Math.Exp(Math.Log(2) / infraroadunpavedlife) - 1)
                    cINFRAINVESTMAINT(key)(RoadUnpaved) = RoadUnPaved_Prev * DeppcntUnPavedRoad * cINFRAROADUNPAVEDCOST(key) '(million $/km) * 1000 km = billion $

                    '------------Rural Road Access Index      --------------------
                    If cntx.HistoricMode Then
                        CRoadRAI.Add(key, getYearOrEarliestWithinRange(RoadRAITbl, yrsRoadRAI, key, cntx.FirstYear, 10))
                    Else
                        CRoadRAI.Add(key, getYearOrMstRct(RoadRAITbl, yrsRoadRAI, key, cntx.BaseYear))
                    End If

                    If Not CRoadRAI(key).HasValue Or CRoadRAI(key) < 90 Then
                        '            iYr1 = getMstRctYr(RoadRAITbl)


                        CRoadRAI(key) = Nothing
                        '            If iYr1 > BaseYear - 15 Then 'most data is 1999 and we want to keep it (?)
                        '                CRoadRAI(key) = getYearOrMstRct(RoadRAITbl, BaseYear)
                        '            End If

                        CRoadRAI(key) = EstimateFromMostRecentRevBBH(CGDPPCP(key), cntx.BaseYear, RoadRAITbl, yrsRoadRAI,
                    GDPPCPTbl, yrsG, key, Func$, cntx, 100, 0, "Div", 1000, 15)

                        ''mti February 08, 2012 - end
                    End If

                    If Not CRoadRAI(key).HasValue Then

                        IncomeDensityLand = (1000 * CGDPPCPBase(key) * CPOPBase(key) / AMAX(CLandArea(key), 0.0001F))  '$Intl/ha
                        TotalRoadsDensityPop = CRoadDens(key) * CLandArea(key) * 1000 / AMAX(CPOPBase(key), 0.000001F) ' INFRAROAD(R%) [km/1000 ha] ; LANDAREA(R%) [1000000 ha] ; POP [million ppeople]
                        CRoadRAI(key) = CSng(100 * Math.Exp(-3.55820835391394 + 0.132781679654008 * Math.Log(CDbl(IncomeDensityLand)) + 0.239067182377656 * Math.Log(CDbl(TotalRoadsDensityPop))))
                        'mti April 05, 2012 - new RAI model - end

                    End If

                    'mti February 28, 2012 - end
                    CRoadRAI(key) = AMAX(1, Amin(100, CRoadRAI(key)))
                Next

                '------------ Irrigation      --------------------
                Dim yrsLandAgri As Short() = cntx.GetYears("SeriesLandAgri").OrderBy(Function(y) y).ToArray()
                Dim LandAgriTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandAgri", yrsLandAgri)
                Dim yrsLandIrPot As Short() = cntx.GetYears("SeriesLandIrPotential").OrderBy(Function(y) y).ToArray()
                Dim LandIrPotTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandIrPotential", yrsLandIrPot)
                Dim yrsLandIrPotReached As Short() = cntx.GetYears("SeriesLandIrPotentialReached").OrderBy(Function(y) y).ToArray()
                Dim LandIrPotReachedTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandIrPotentialReached", yrsLandIrPotReached)
                Dim yrsLandIrAreaEquip As Short() = cntx.GetYears("SeriesLandIrAreaEquip").OrderBy(Function(y) y).ToArray()
                Dim LandIrAreaEquipTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesLandIrAreaEquip", yrsLandIrAreaEquip)

                Dim cLandAgri As Single?, iMRYrIdx As Short?
                Dim CLandIrPotReached As Single?, TLANDIRAREAEQUIP As Single?, LandIrArea_Prev As Single?
                Dim ConvTime As Single?, cGrowthRatePosition As Single?, cLandIrAreaAdded As Single?
                Dim DeppcntIrri As Single?

                For Each key In cntx.Regionalization.Keys
                    cLandAgri = Nothing
                    If cntx.HistoricMode Then
                        cLandAgri = getYearOrEarliestWithinRange(LandAgriTbl, yrsLandAgri, key, cntx.FirstYear, 10) 'LandAgriTbl.Fields("Earliest")
                    Else
                        iMRYrIdx = getYearIdxMstRctWDataAsc(LandAgriTbl, key, yrsLandAgri)
                        If iMRYrIdx.HasValue Then
                            If iMRYrIdx >= 0 Then
                                cLandAgri = LandAgriTbl(key)(CShort(iMRYrIdx))
                            End If
                        End If
                    End If

                    If cntx.HistoricMode Then
                        'sgh 2016/11/18 - read potentially irrigable land as the saturation level
                        '            CLandIrPot = getYearOrEarliestWithinRange(LandIrPotTbl, FirstYearHistory, 10) 'LandIrPotTbl.Fields("Earliest")
                        CLandIRAreaSat.Add(key, getYearOrEarliestWithinRange(LandIrPotTbl, yrsLandIrPot, key, cntx.FirstYear, 10)) 'LandIrPotTbl.Fields("Earliest")
                        CLandIrPotReached = Nothing 'LandIrPotReachedTbl.Fields("Earliest")
                    Else
                        CLandIRAreaSat.Add(key, Nothing)
                        iMRYrIdx = getYearIdxMstRctWDataAsc(LandIrPotTbl, key, yrsLandIrPot)
                        If iMRYrIdx.HasValue Then
                            If iMRYrIdx >= 0 Then
                                CLandIRAreaSat(key) = LandIrPotTbl(key)(CInt(iMRYrIdx))
                            End If
                        End If
                        CLandIrPotReached = Nothing
                        iMRYrIdx = getYearIdxMstRctWDataAsc(LandIrPotReachedTbl, key, yrsLandIrPotReached)
                        If iMRYrIdx.HasValue Then
                            If iMRYrIdx >= 0 Then
                                CLandIrPotReached = LandIrPotReachedTbl(key)(CInt(iMRYrIdx))
                            End If
                        End If
                    End If
                    TLANDIRAREAEQUIP = Nothing
                    If cntx.HistoricMode Then
                        TLANDIRAREAEQUIP = getYearOrEarliestWithinRange(LandIrAreaEquipTbl, yrsLandIrAreaEquip, key, cntx.FirstYear, 10) ' LandIrAreaEquipTbl.Fields("Earliest")
                    Else
                        iMRYrIdx = getYearIdxMstRctWDataAsc(LandIrAreaEquipTbl, key, yrsLandIrAreaEquip)
                        If iMRYrIdx.HasValue Then
                            If iMRYrIdx >= 0 Then
                                TLANDIRAREAEQUIP = LandIrAreaEquipTbl(key)(CInt(iMRYrIdx))
                            End If
                        End If
                    End If

                    If Not (cLandAgri.HasValue) Then
                        'just in case land agri. is still empty
                        'CLandArea(key) is in million hectares
                        cLandAgri = CSng(CLandArea(key) * 1000 * 0.05)
                    End If

                    If Not CLandIRAreaSat(key).HasValue Then
                        If Not (TLANDIRAREAEQUIP.HasValue) Then
                            CLandIRAreaSat(key) = CSng(cLandAgri * 0.1)
                            TLANDIRAREAEQUIP = CSng(CLandIRAreaSat(key) * 0.33)
                        Else
                            If TLANDIRAREAEQUIP < cLandAgri * 0.9 And TLANDIRAREAEQUIP > cLandAgri * 0.1 Then
                                CLandIRAreaSat(key) = CSng(TLANDIRAREAEQUIP * 1.1)
                            ElseIf TLANDIRAREAEQUIP <= cLandAgri * 0.1 Then
                                CLandIRAreaSat(key) = CSng(cLandAgri * 0.1)
                            ElseIf TLANDIRAREAEQUIP >= cLandAgri * 0.9 Then
                                CLandIRAreaSat(key) = cLandAgri
                            End If
                        End If
                    Else
                        If Not (TLANDIRAREAEQUIP.HasValue) Then
                            TLANDIRAREAEQUIP = CSng(CLandIRAreaSat(key) * 0.33)
                        End If
                        'sgh 2018/04/10 - START
                        '                CLandIRAreaSat(key) = Amin(cLandAgri, AMAX(TLANDIRAREAEQUIP * 1.01, 0.01))
                        CLandIRAreaSat(key) = Amin(cLandAgri, AMAX(CSng(TLANDIRAREAEQUIP * 1.01), CLandIRAreaSat(key)))
                        'sgh 2018/04/10 - END
                    End If

                    'sgh 2016/11/18 - Luxembourg, Maldives, and Papua New Guinea have 0 irrigated land so there's a "division by zero" problem
                    'if this isn't here
                    If TLANDIRAREAEQUIP = 0 Then
                        TLANDIRAREAEQUIP = 0.001
                    End If
                    'sgh 2018/01/04 - START
                    'Make sure land equipped for irrigation does not exceed crop land
                    '        CLandIrAreaEquip(key) = TLANDIRAREAEQUIP
                    CLandIrAreaEquip.Add(key, Amin(TLANDIRAREAEQUIP, CLandIRAreaSat(key)))
                    'jrs 2018/12/21 CLandIrAreaEquip(key) = Amin(TLANDIRAREAEQUIP, (LDCrop(key) * 1000))
                    CLandIrAreaEquip(key) = Amin(CLandIrAreaEquip(key), (LDCrop(key) * 1000))
                    'sgh 2018/01/04 - END
                    'If key = "United States of America" Then Stop
                    CLandIrAreaEquipGR.Add(key, getAnnualGrowthRate(LandIrAreaEquipTbl, yrsLandIrAreaEquip, key, cntx.BaseYear, 0.08, -0.08, 3, 5, 15, 10))
                    ' If CountryName$(key) = "South Africa" Then Stop
                    'sgh 2016/11/18 - END

                    If Not (CLandIrAreaEquipGR(key).HasValue) Then
                        'mti August 22, 2011 - start
                        CLandIrAreaEquipGR(key) = -9999 '0.001
                        'mti August 22, 2011 - end
                    End If

                    '------------ Irrigation Costs ---------------
                    If cntx.HistoricMode Then
                        'historic?
                        LandIrArea_Prev = 0
                    Else
                        If CLandIrAreaEquipGR(key) <> -9999 Then
                            LandIrArea_Prev = CLandIrAreaEquip(key) / (1 + CLandIrAreaEquipGR(key)) '1000 ha
                        Else

                            If CLandIrAreaEquip(key) > CLandIRAreaSat(key) Then
                                RatChange = CLandIRAreaSat(key) / AMAX(0.001F, CLandIrAreaEquip(key))
                                ConvTime = 30  'years
                                cGrowthRatePosition = CSng(RatChange ^ (1 / ConvTime) - 1)
                            Else
                                cGrowthRatePosition = 0.000005 'negligible growth
                            End If

                            LandIrArea_Prev = CLandIrAreaEquip(key) / (1 + cGrowthRatePosition) '1000 ha
                        End If

                        cLandIrAreaAdded = CLandIrAreaEquip(key) - LandIrArea_Prev   '1000 ha
                    End If

                    cLANDIRCOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, landircostupper, landircostlower, CGDPPCP(key), cLANDIRCOST(key))
                    cINFRAINVESTNEW(key)(Irrigation) = AMAX(cLandIrAreaAdded, 0) * cLANDIRCOST(key) / 1000 '(1000 ha * 1000 $ per hectare)/1000 = billion $
                    DeppcntIrri = CSng(Math.Exp(Math.Log(2) / landirlife) - 1)
                    cINFRAINVESTMAINT(key)(Irrigation) = LandIrArea_Prev * DeppcntIrri * cLANDIRCOST(key) / 1000 '(1000 ha * 1000 $ per hectare)/1000 = billion $s

                Next

                '------------- Telephone ------------
                Dim yrsTeleDens As Short() = cntx.GetYears("SeriesICTTelephoneLinesPer100").OrderBy(Function(y) y).ToArray()
                Dim TeleDensTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTTelephoneLinesPer100", yrsTeleDens)

                'jrs 2024/07/04 TbName$ = "GDP/Capita (PPP) Versus Telephones per 100 (" & CStr(cntx.BaseYear) & ") Parabolic"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Telephones per 100 (" & CStr(cntx.BaseYear) & ") Parabolic"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                XYTABLRC = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                XYTABLRP = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                Dim TmpInfraTeleGR As Single?, cTele_Curr As Single?, cTele_Prev As Single?, cTeleAdded As Single?
                Dim DeppcntTele As Single?

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CTeleDens.Add(key, getYearOrEarliestWithinRange(TeleDensTbl, yrsTeleDens, key, cntx.FirstYear, 10)) ' TeleDensTbl.Fields("Earliest")
                        'getYearOrEarliestWithinRange(LandIrAreaEquipTbl, FirstYearHistory, 10)
                        If Not (CTeleDens(key).HasValue) Then
                            'jrs 2006/12/15 Call XYTABL("GDP/Capita (PPP) Versus Telephones per 1000 (2000)", _
                            'CGDPPCP(key), CTeleDens(key))
                            'TbName$ = "GDP/Capita (PPP) Versus Telephones per 1000 (" & CStr(BaseYear) & ")"
                            CTeleDens(key) = XYTABL(key)
                        End If

                        CTeleDensGR.Add(key, getAnnualGrowthRate(TeleDensTbl, yrsTeleDens, key, cntx.FirstYear, 0.1, 0.0005, 4, 5, 10, 10))
                    Else
                        CTeleDens.Add(key, getYearOrMstRct(TeleDensTbl, yrsTeleDens, key, cntx.BaseYear))
                        If Not (CTeleDens(key).HasValue) Then

                            'jrs 2006/12/15 Call XYTABL("GDP/Capita (PPP) Versus Telephones per 1000 (2000)", _
                            'CGDPPCP(key), CTeleDens(key))
                            'TbName$ = "GDP/Capita (PPP) Versus Telephones per 1000 (" & CStr(BaseYear) & ")"
                            CTeleDens(key) = XYTABL(key)

                        End If
                        CTeleDensGR.Add(key, getAnnualGrowthRate(TeleDensTbl, yrsTeleDens, key, cntx.BaseYear, 0.1, 0.0005, 4, 5, 10, 10))
                    End If

                    CTeleDens(key) = AMAX(0.05F, Amin(100.0F, CTeleDens(key)))


                    If Not (CTeleDensGR(key).HasValue) Then
                        CTeleDensGR(key) = -9999
                    End If

                    '------------ Telephone Connection Costs ---------------
                    If CTeleDensGR(key) > -9999 Then
                        TmpInfraTeleGR = CTeleDensGR(key)

                    Else  'null retured from growth rate calculation
                        ValHold_Curr = XYTABLRC(key)
                        ValHold_Prev = XYTABLRP(key)
                        cTele_Curr = AMAX(ValHold_Curr, 0) * CPOP_Curr(key) / 100 'million lines
                        cTele_Prev = AMAX(ValHold_Prev, 0) * CPOP_Prev(key) / 100 'million lines

                        If cTele_Prev <> 0 Then
                            RatChange = cTele_Curr / cTele_Prev
                            DiffTime = AMAX(1, Curr_Yr(key) - Prev_Yr(key))
                            TmpInfraTeleGR = AMAX(0.0005, CSng(RatChange ^ (1 / DiffTime) - 1))
                        Else
                            TmpInfraTeleGR = 0
                        End If


                    End If
                    cTele_Prev = (CTeleDens(key) * CPOP_Curr(key) / 100) / (1 + TmpInfraTeleGR) / (1 + TmpPopGR(key)) 'million kw
                    cTele_Curr = CTeleDens(key) * CPOP_Curr(key) / 100
                    cTeleAdded = AMAX(0, cTele_Curr - cTele_Prev) 'million lines

                    '        End If
                    'mti February 2011 Infarstructure - end
                    cINFRATELECOST.Add(key, Nothing)
                    CalcInfraUnitCost(10, 5, infratelecostupper, infratelecostlower, CGDPPCP(key), cINFRATELECOST(key), False)
                    cINFRAINVESTNEW(key)(Telephone) = cTeleAdded * cINFRATELECOST(key) 'million connection * (1000 $/conn)= billion $
                    DeppcntTele = CSng(Math.Exp(Math.Log(2) / infratelelife) - 1)
                    cINFRAINVESTMAINT(key)(Telephone) = cTele_Prev * DeppcntTele * cINFRATELECOST(key) 'million connection * (1000 $/conn)= billion $

                    '------------ Telephone Connection Cost End ---------------
                Next

                'ICT 
                Dim yrsSpend As Short() = cntx.GetYears("SeriesICTExpendGDP").OrderBy(Function(y) y).ToArray()
                Dim SpendTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTExpendGDP", yrsSpend)

                If cntx.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (2005) Linear"
                    XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)
                Else
                    'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Expenditures % GDP (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus ICT Expenditures % GDP (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                    XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)
                End If

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        CSpend.Add(key, getYearOrEarliestWithinRange(SpendTbl, yrsSpend, key, cntx.FirstYear, 10)) ' SpendTbl.Fields("Earliest")
                        'getYearOrEarliestWithinRange(LandIrAreaEquipTbl, FirstYearHistory, 10)
                        If Not (CSpend(key).HasValue) Then
                            CSpend(key) = XYTABL(key)
                            CSpend(key) = AMAX(Amin(CSpend(key), 100), 0)
                        End If
                    Else
                        CSpend.Add(key, getYearOrMstRct(SpendTbl, yrsSpend, key, cntx.BaseYear))
                        If Not (CSpend(key).HasValue) Then
                            CSpend(key) = XYTABL(key)
                            CSpend(key) = AMAX(Amin(CSpend(key), 100), 0)
                        End If
                    End If
                Next

                Dim yrsMobil As Short() = cntx.GetYears("SeriesICTTelephoneCellSubscribersPer100").OrderBy(Function(y) y).ToArray()
                Dim Mobiltbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTTelephoneCellSubscribersPer100", yrsMobil)

                Dim yrsBroadBand As Short() = cntx.GetYears("SeriesICTBroadbandSubscribersPer100ITU").OrderBy(Function(y) y).ToArray()
                Dim BroadBandtbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTBroadbandSubscribersPer100ITU", yrsBroadBand)

                Dim yrsBroadBandMobil As Short() = cntx.GetYears("SeriesICTBroadbandMobileSubsPer100").OrderBy(Function(y) y).ToArray()
                Dim BroadBandMobiltbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTBroadbandMobileSubsPer100", yrsBroadBandMobil)

                Dim yrsICTComps As Short() = cntx.GetYears("SeriesICTComputerHousehold").OrderBy(Function(y) y).ToArray()
                Dim ICTComputersTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTComputerHousehold", yrsICTComps)

                Dim yrsICTIntNetHH As Short() = cntx.GetYears("SeriesICTInternetHousehold").OrderBy(Function(y) y).ToArray()
                Dim ICTIntNetHHTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTInternetHousehold", yrsICTIntNetHH)

                Dim yrsICTindex As Short() = cntx.GetYears("SeriesICTCYBICTDevelopmentIndexITU").OrderBy(Function(y) y).ToArray()
                Dim ICTindexTbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesICTCYBICTDevelopmentIndexITU", yrsICTindex)

                Dim CGDPPCPDiv10 As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each key In cntx.Regionalization.Keys
                    CGDPPCPDiv10.Add(key, CGDPPCP(key))
                Next

                Dim TbNameMobil As String, TbNameBroadband As String, TbNameBroadMobil As String

                If cntx.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Cell Subscribers Per 100 (2001) Log"
                    XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPPCPDiv10)

                    TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 ITU (2001) Linear"
                    XYTABLRC = cntx.GetRegression(TbName).ToEstimator(CGDPPCPDiv10)

                    TbName$ = "GDP/Capita (PPP 2005) Versus Mobile Broadband Per 100 (MostRecent) Log"
                    XYTABLRP = cntx.GetRegression(TbName).ToEstimator(CGDPPCPDiv10)

                    TbName$ = "GDP/Capita (PPP 2011) Versus Computer Owning Households(MOSTRECENT) Log"  'function for earlier years yet to be estimated
                    XYTABLUC = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                    TbName$ = "GDP/Capita (PPP 2011) Versus Internet Connected Households(MOSTRECENT) Log"  'function for earlier years yet to be estimated
                    XYTABLUP = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                    TbName$ = "GDP/Capita (PPP 2011) Versus ITU ICT Development Index (MOSTRECENT) Log"
                    Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPPCPDiv10)

                Else
                    'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Cell Subscribers Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Telephone Cell Subscribers Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbNameMobil = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                    'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 ITU (" & CStr(cntx.BaseYear) & ") Linear"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus ICT Broadband Subs per 100 ITU (" & CStr(cntx.BaseYear) & ") Linear"
                    TbNameBroadband$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                    'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2005) Versus Mobile Broadband Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Mobile Broadband Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbNameBroadMobil$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                    'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2011) Versus Computer Owning Households(" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Computer Owning Households(" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    XYTABLRP = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)

                    'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2011) Versus Internet Connected Households(" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Internet Connected Households(" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    XYTABLUC = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)
                End If

                Dim TMobil As Single?, TComputerHH As Single?, TintNetHH As Single?, TICTAcc As Single?
                Dim TBroad As Single?, TBroadMobil As Single?, TICTUse As Single?, TLit As Single?
                Dim TEdSecEnrg As Single?, TEdTerEnrg As Single?, TICTSkill As Single?

                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        'Barry recommended to chop down the feed by a factor of 10
                        'because the regressions were too far into the future
                        If cntx.FirstYear < 1990 Then
                            CMobil.Add(key, 0)
                            CMobilGR.Add(key, 0)
                        Else
                            CMobilGR.Add(key, getAnnualGrowthRate(Mobiltbl, yrsMobil, key, cntx.BaseYear, 1.5, 0.0005, 4, 5, 10, 10))
                            CMobil.Add(key, getYearOrEarliestWithinRange(Mobiltbl, yrsMobil, key, cntx.FirstYear, 5)) ' MobilTbl.Fields("Earliest")
                            If Not (CMobil(key).HasValue) Then
                                'jrs 2009/04/02 TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Mobile Cov% (2001) Log"
                                CMobil(key) = XYTABL(key)
                                'jrs 2009/04/02
                                CMobil(key) = Amin(CMobil(key), 100)
                            End If
                            If Not (CMobilGR(key).HasValue) Then
                                CMobilGR(key) = -9999
                            End If
                        End If


                        If cntx.FirstYear < 1990 Then
                            CBroadband.Add(key, 0)
                            CBroadbandGR.Add(key, 0)
                        Else
                            CBroadband.Add(key, getYearOrEarliestWithinRange(BroadBandtbl, yrsBroadBand, key, cntx.FirstYear, 5))  'BroadBandtbl.Fields("Earliest")
                            If Not (CBroadband(key).HasValue) Then
                                'jrs 2009/05/07 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 (2001) Log"
                                CBroadband(key) = XYTABLRC(key)
                            End If

                            CBroadbandGR.Add(key, getAnnualGrowthRate(BroadBandtbl, yrsBroadBand, key, cntx.FirstYear, 5, 0.0005, 4, 5, 10, 10, True))
                            If Not (CBroadbandGR(key).HasValue) Then
                                CBroadbandGR(key) = -9999
                            End If
                        End If

                        'mti April 2011 -start
                        If cntx.FirstYear < 1990 Then
                            CBroadMobil.Add(key, 0)
                            CBroadMobilGR.Add(key, 0)
                        Else
                            CBroadMobil.Add(key, getYearOrEarliestWithinRange(BroadBandMobiltbl, yrsBroadBandMobil, key, cntx.FirstYear, 5))  ' BroadBandMobiltbl.Fields("Earliest")
                            If Not (CBroadMobil(key).HasValue) Then
                                'jrs 2009/05/07 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 (2001) Log"
                                CBroadMobil(key) = XYTABLRP(key)
                            End If
                            'mti April 2011 -end
                            CBroadMobilGR.Add(key, getAnnualGrowthRate(BroadBandMobiltbl, yrsBroadBandMobil, key, cntx.FirstYear, 5, 0.0005, 3, 5, 10, 10, True))
                            If Not (CBroadMobilGR(key).HasValue) Then
                                CBroadMobilGR(key) = -9999
                            End If
                        End If





                        'mti 2015/05/06 - start

                        'mti December 2009 - Knowledge Indices -start
                        If cntx.FirstYear < 1990 Then
                            CICTComputers.Add(key, 0)
                        Else
                            CICTComputers.Add(key, getYearOrEarliestWithinRange(ICTComputersTbl, yrsICTComps, key, cntx.FirstYear, 10)) ' ICTComputersTbl.Fields("Earliest")
                            If Not (CICTComputers(key).HasValue) Then

                                'mti 2015/04/29 - start
                                'TbName$ = "GDP/Capita (PPP 2000) Versus ICTComputersper100 Logit (MOSTRECENT)"  'function for earlier years yet to be estimated
                                'mti 2015/04/29 - end
                                CICTComputers(key) = XYTABLUC(key)
                            End If
                        End If
                        CICTComputers(key) = AMAX(0, Amin(100, CICTComputers(key)))

                        'mti 2015/04/29 - start
                        If cntx.FirstYear < 1990 Then
                            CICTINTNetHH.Add(key, 0)
                        Else
                            CICTINTNetHH.Add(key, getYearOrEarliestWithinRange(ICTIntNetHHTbl, yrsICTIntNetHH, key, cntx.FirstYear, 10)) ' ICTComputersTbl.Fields("Earliest")
                            If Not (CICTINTNetHH(key).HasValue) Then
                                CICTINTNetHH(key) = XYTABLUP(key)
                            End If
                        End If
                        CICTINTNetHH(key) = AMAX(0, Amin(100, CICTINTNetHH(key)))
                        'mti 2015/04/29 - end


                        If cntx.FirstYear < 1990 Then
                            CICTIndex.Add(key, 0)

                        Else

                            'mti 2015/09/08 - start
                            CICTIndex.Add(key, Nothing)
                            'mti 2015/09/08 -end

                            CICTIndex(key) = getYearOrEarliestWithinRange(ICTindexTbl, yrsICTindex, key, cntx.FirstYear, 5) 'ICTindexTbl.Fields("Earliest")
                            If Not (CICTIndex(key).HasValue) Then
                                'mti April 12, 2010
                                'TbName$ = "TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)"

                                'mti 2015/04/29 - start
                                'TbName$ = "GDP/Capita (PPP 2000) Versus ICT Index (" & CStr(BaseYear) & ") Log"
                                'mti April 12, 2010
                                CICTIndex(key) = Estimate(key)
                            End If
                        End If

                        'mti 2015/05/06 - end

                        CMobil(key) = AMAX(0, Amin(CMobil(key), 150))
                        CBroadband(key) = AMAX(0, Amin(CBroadband(key), 100))
                        CBroadMobil(key) = AMAX(0, Amin(CBroadMobil(key), 100))
                        CICTComputers(key) = AMAX(0, Amin(100, CICTComputers(key)))

                    Else 'FORECAST
                        'jrs 2010/12/06 *** begin: Using Annual Growth Rate ***
                        'mti July 18, 2011 - Rebase 2010 ICT - start
                        'CMobilGR(key) = getAnnualGrowthRate(MobilTbl, BaseYear, 100, 0.0005, 4, 5, 10, 10, True)

                        'MTI: changing the growth rate calc (from absolute change to rate) will require changes in Economy.bas : MFPPC first year
                        'annual growth rate (not absolute percent change)
                        CMobilGR.Add(key, getAnnualGrowthRate(Mobiltbl, yrsMobil, key, cntx.BaseYear, 1.5, 0.0005, 4, 5, 10, 10))
                        'mti July 18, 2011 - Rebase 2010 ICT - end

                        If Not (CMobilGR(key).HasValue) Then
                            CMobilGR(key) = -9999
                        End If


                        'Most recent year with data foe this country/region
                        iYr1 = getMstRctYr(Mobiltbl, yrsMobil, key)
                        CMobil.Add(key, Nothing)

                        iYr1Idx = getYearIdx(yrsMobil, CShort(iYr1))
                        If iYr1 = cntx.BaseYear Then
                            CMobil(key) = Mobiltbl(key)(iYr1Idx)
                        Else
                            If iYr1 > cntx.BaseYear - 5 And CMobilGR(key) <> -9999 Then
                                CMobil(key) = Mobiltbl(key)(iYr1Idx)
                                CMobil(key) = CSng(CMobil(key) * (1 + CMobilGR(key)) ^ (cntx.BaseYear - iYr1))
                            End If
                        End If

                        If Not (CMobil(key).HasValue) Then
                            'jrs 2009/04/02 TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Mobile Cov% (" & CStr(BaseYear) & ") Log"
                            'Call XYTABL(TbName$, CGDPPCP(key), CMobil(key))
                            Func = TbNameMobil
                            CMobil(key) = EstimateFromMostRecentRevBBH(CGDPPCP(key), cntx.BaseYear, Mobiltbl, yrsMobil,
                        GDPPCPTbl, yrsG, key, Func$, cntx, 120, 0, "Div", 1000, 15) '15
                        End If
                        CMobil(key) = AMAX(1, Amin(CMobil(key), 200))




                        'Fixed Broadband
                        '----------------

                        'absolute percent change
                        'MTI: changing the growth rate calc (from absolute change to rate) will require changes in Economy.bas : MFPPC first year
                        CBroadbandGR.Add(key, getAnnualGrowthRate(BroadBandtbl, yrsBroadBand, key, cntx.BaseYear, 5, 0.0005, 4, 5, 10, 10, True))
                        'mti October 20, 2011 - end
                        'mti July 18, 2011 - Rebase 2010 ICT - end

                        If Not (CBroadbandGR(key).HasValue) Then
                            CBroadbandGR(key) = -9999
                        End If
                        'jrs 2010/12/06 *** end: Using Annual Growth Rate ***


                        'Most recent year with data foe this country/region
                        iYr1 = getMstRctYr(BroadBandtbl, yrsBroadBand, key)
                        CBroadband.Add(key, Nothing)

                        iYr1Idx = getYearIdx(yrsBroadBand, CShort(iYr1))
                        If iYr1 = cntx.BaseYear Then
                            CBroadband(key) = BroadBandtbl(key)(iYr1Idx)
                        Else
                            If iYr1 > cntx.BaseYear - 5 And CBroadbandGR(key) <> -9999 Then
                                CBroadband(key) = BroadBandtbl(key)(iYr1Idx)
                                CBroadband(key) = CBroadband(key) + CBroadbandGR(key) * (cntx.BaseYear - iYr1)
                            End If
                        End If


                        If Not (CBroadband(key).HasValue) Then
                            'jrs 2009/05/07 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 (" & CStr(BaseYear) & ") Linear"
                            Func = TbNameBroadband
                            CBroadband(key) = EstimateFromMostRecentRevBBH(CGDPPCP(key), cntx.BaseYear, BroadBandtbl, yrsBroadBand,
                        GDPPCPTbl, yrsG, key, Func$, cntx, 120, 0, "Div", 1000, 15) '15
                        End If

                        CBroadband(key) = AMAX(1, Amin(CBroadband(key), 100))

                        'Mobile Broadband
                        '--------------------


                        'Absolute pcnt point growth rate
                        CBroadMobilGR.Add(key, getAnnualGrowthRate(BroadBandMobiltbl, yrsBroadBandMobil, key, cntx.BaseYear, 5, 0.0005, 3, 5, 10, 10, True))

                        If Not (CBroadMobilGR(key).HasValue) Then
                            CBroadMobilGR(key) = -9999
                        End If

                        'If CountryName(key) = "Argentina" Then Stop

                        'Most recent year with data foe this country/region
                        iYr1 = getMstRctYr(BroadBandMobiltbl, yrsBroadBandMobil, key)
                        iYr1Idx = getYearIdx(yrsBroadBandMobil, CShort(iYr1))
                        CBroadMobil.Add(key, Nothing)
                        If iYr1 = cntx.BaseYear Then
                            CBroadMobil(key) = BroadBandMobiltbl(key)(iYr1Idx)
                        Else
                            If iYr1 > cntx.BaseYear - 5 And CBroadMobilGR(key) <> -9999 Then
                                CBroadMobil(key) = BroadBandMobiltbl(key)(iYr1Idx)
                                CBroadMobil(key) = CBroadMobil(key) + CBroadMobilGR(key) * (cntx.BaseYear - iYr1)
                            End If
                        End If


                        If Not (CBroadMobil(key).HasValue) Then
                            Func = TbNameBroadMobil
                            CBroadMobil(key) = EstimateFromMostRecentRevBBH(CGDPPCP(key), cntx.BaseYear, BroadBandMobiltbl, yrsBroadBandMobil,
                        GDPPCPTbl, yrsG, key, Func$, cntx, 120, 0, "Div", 1000, 15) '15
                        End If

                        'Broadband mobil is capped at total mobile subscription rate
                        CBroadMobil(key) = AMAX(0.0001F, Amin(CBroadMobil(key), CMobil(key)))


                        'mti 2016/04/22 end

                        'mti April 2011 - end

                        'mti 2015/05/06 - start

                        'If CountryName(key) = "Equatorial Guinea" Then Stop

                        CICTComputers.Add(key, getYearOrMstRct(ICTComputersTbl, yrsICTComps, key, cntx.BaseYear))
                        If Not (CICTComputers(key).HasValue) Then

                            'mti 2015/04/29 - start
                            'TbName$ = "GDP/Capita (PPP 2000) Versus ICTComputersper100 Logit (" & CStr(BaseYear) & ")"
                            CICTComputers(key) = XYTABLRP(key)
                        End If
                        CICTComputers(key) = AMAX(0, Amin(100, CICTComputers(key)))

                        CICTINTNetHH.Add(key, getYearOrMstRct(ICTIntNetHHTbl, yrsICTIntNetHH, key, cntx.BaseYear)) ' ICTComputersTbl.Fields("Earliest")
                        If Not (CICTINTNetHH(key).HasValue) Then
                            CICTINTNetHH(key) = XYTABLUC(key)
                        End If
                        CICTINTNetHH(key) = AMAX(0, Amin(100, CICTINTNetHH(key)))



                        CMobil(key) = AMAX(0.01F, Amin(CMobil(key), 200.0F))
                        CBroadband(key) = AMAX(0.01F, Amin(CBroadband(key), 200.0F))
                        CBroadMobil(key) = AMAX(0.001F, Amin(CBroadMobil(key), 200.0F))
                        CICTComputers(key) = AMAX(0.01F, Amin(100.0F, CICTComputers(key)))


                        'mti 2015/09/08 - start
                        CICTIndex.Add(key, Nothing)
                        'mti 2015/09/08 -end

                        'CICTIndex(key) = getYearOrMstRct(ICTindexTbl, BaseYear)
                        If yrsICTindex.Contains(cntx.BaseYear) Then
                            iYr1Idx = getYearIdx(yrsICTindex, cntx.BaseYear)
                            CICTIndex(key) = ICTindexTbl(key)(iYr1Idx)
                        End If


                        If Not (CICTIndex(key).HasValue) Then
                            If yrsICTindex.Contains(CShort(cntx.BaseYear + 1)) Then
                                iYr1Idx = getYearIdx(yrsICTindex, CShort(cntx.BaseYear + 1))
                                CICTIndex(key) = ICTindexTbl(key)(iYr1Idx)
                            End If
                        End If

                        If Not (CICTIndex(key).HasValue) Then
                            CICTIndex(key) = getYearOrMstRct(ICTindexTbl, yrsICTindex, key, cntx.BaseYear)
                        End If

                        If Not (CICTIndex(key).HasValue) Then

                            'If some variables are estimated from GDPPC; those will have high values if GDPPC is high: Equa Guinea
                            TMobil = Amin(1, CMobil(key) / 120)
                            TComputerHH = Amin(1, CICTComputers(key) / 100)
                            TintNetHH = Amin(1, CICTINTNetHH(key) / 100)
                            'Note: International Bandwidth Per Internet User is not included yet
                            TICTAcc = (TMobil + TComputerHH + TintNetHH) / 3


                            TBroad = Amin(1, CBroadband(key) / 50)
                            TBroadMobil = Amin(1, CBroadMobil(key) / 100)

                            'TIntNetIndiv = Amin(1, ICTINTNET(R%) / 100)
                            TICTUse = (TBroad + TBroadMobil) / 2
                            'mti 2015/05/06 - end

                            TLit = Amin(1, CLIT(key) / 100)
                            TEdSecEnrg = Amin(1, CEDSECENRGT(key) / 100)
                            TEdTerEnrg = Amin(1, CEDTERENRGT(key) / 100)
                            TICTSkill = (TLit + TEdSecEnrg + TEdTerEnrg) / 3

                            CICTIndex(key) = CSng(10 * (0.4 * TICTAcc + 0.4 * TICTUse + 0.2 * TICTSkill))



                            '                'mti 2015/04/29 - start
                            '                'TbName$ = "GDP/Capita (PPP 2000) Versus ICT Index (" & CStr(BaseYear) & ") Log"
                            '                TbName$ = "GDP/Capita (PPP 2011) Versus ITU ICT Development Index (" & CStr(BaseYear) & ") Log"
                            '                'mti 2015/04/29 - end
                            '
                            '                TbName$ = getFunctionNameYrOrMstRct(TbName$, BaseYear)
                            '                Call XYTABL(TbName$, CGDPPCP(key), CICTIndex(key))
                        End If
                        'mti 2015/05/06 - end
                    End If 'IF HISTORY ELSE FORECAST
                Next

                '------------ Broadband Costs ---------------
                Dim CBroadBand_Curr As Single?, CBroadBand_Prev As Single?, CBroadBandAdded As Single?
                Dim DeppcntBroad As Single?

                'jrs 2024/06/27 TbName$ = "GDP/Capita (PPP 2000) Versus ICT Broadband Subs per 100 ITU (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus ICT Broadband Subs per 100 ITU (" & CStr(cntx.BaseYear) & ") Linear"
                TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                XYTABLRC = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)

                For Each key In cntx.Regionalization.Keys
                    If CBroadbandGR(key) > -9999 Then
                        'constant percentage point change
                        CBroadBand_Curr = CBroadband(key) * CPOP_Curr(key) / 100
                        CPOP_Last = CPOP_Curr(key) / (1 + TmpPopGR(key))

                        'mti January 17, 2012 - start
                        '                CBroadBand_Prev = (CBroadband(key) / (1 + CBroadbandGR(key))) * CPOP_Last / 100
                        CBroadBand_Prev = (CBroadband(key) - CBroadbandGR(key)) * CPOP_Last / 100
                        'mti August 17, 2011 - end

                    Else  'null returned from growth rate calculation


                        'TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Cell Subscribers Per 100 (" & CStr(BaseYear) & ") Log"
                        ValHold_Curr = XYTABL(key)
                        ValHold_Prev = XYTABLRC(key)
                        CBroadBand_Curr = ValHold_Curr * CPOP_Curr(key) / 100 'millions of connections
                        CBroadBand_Prev = ValHold_Prev * CPOP_Prev(key) / 100 'millions of connections

                        YrDiff = Curr_Yr(key) - Prev_Yr(key)
                        StkDiff = CBroadBand_Curr - CBroadBand_Prev
                        AnnDiff = StkDiff / YrDiff

                        CBroadBand_Prev = CBroadBand_Curr - AnnDiff
                    End If

                    CBroadBandAdded = AMAX(CBroadBand_Curr - CBroadBand_Prev, 0) 'million connections

                    'readjust maintenance stock using current stock (from data or estimation)
                    CBroadBand_Curr = CBroadband(key) * CPOP_Curr(key) / 100 'million connections
                    CBroadBand_Prev = CBroadBand_Curr - CBroadBandAdded

                    If cntx.HistoricMode And cntx.FirstYear < 1990 Then
                        cICTBROADCOST.Add(key, 0)
                        cINFRAINVESTNEW(key)(Broadband) = 0
                        cINFRAINVESTMAINT(key)(Broadband) = 0
                    Else
                        cICTBROADCOST.Add(key, Nothing)
                        CalcInfraUnitCost(10, 5, ictbroadcostupper, ictbroadcostlower, CGDPPCP(key), cICTBROADCOST(key))
                        cINFRAINVESTNEW(key)(Broadband) = CBroadBandAdded * cICTBROADCOST(key) 'million connection * (1000 $/conn)= billion $
                        DeppcntBroad = CSng(Math.Exp(Math.Log(2) / ictbroadlife) - 1)
                        cINFRAINVESTMAINT(key)(Broadband) = CBroadBand_Prev * DeppcntBroad * cICTBROADCOST(key) 'million connection * (1000 $/conn)= billion $
                    End If
                    '------------ Broadband Costs End ---------------
                Next

                '------------ Mobile Broadband Costs ---------------
                Dim CBroadMobil_Curr As Single?, CBroadMobil_Prev As Single?, CBroadMobilAdded As Single?
                Dim DeppcntBroadmobil As Single?

                If Not cntx.HistoricMode Then
                    'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2005) Versus Mobile Broadband Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Mobile Broadband Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                    XYTABLRP = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)
                End If


                For Each key In cntx.Regionalization.Keys
                    If cntx.HistoricMode Then
                        'hard coding year to avoid taking later years, please revise if earlier data is available
                        'cInfraRoadPavedPcnt(key) = RoadPavedPcntTbl.Fields("1990")

                        'historic?
                    Else
                        If CBroadMobilGR(key) > -9999 Then
                            'constant percentage point change
                            CBroadMobil_Curr = CBroadMobil(key) * CPOP_Curr(key) / 100
                            CPOP_Last = CPOP_Curr(key) / (1 + TmpPopGR(key))

                            'mti January 17, 2012 - start
                            'mti August 17, 2011 - start
                            'For absolute % change
                            CBroadMobil_Prev = (CBroadMobil(key) - CBroadMobilGR(key)) * CPOP_Last / 100
                            'For growth rate
                            'CBroadMobil_Prev = (CBroadMobil(key) / (1 + CBroadMobilGR(key))) * CPOP_Last / 100
                            'mti August 17, 2011 - end

                        Else  'null returned from growth rate calculation

                            'TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Cell Subscribers Per 100 (" & CStr(BaseYear) & ") Log"
                            ValHold_Curr = XYTABL(key)
                            ValHold_Prev = XYTABLRP(key)
                            CBroadMobil_Curr = ValHold_Curr * CPOP_Curr(key) / 100 'millions of connections
                            CBroadMobil_Prev = ValHold_Prev * CPOP_Prev(key) / 100 'millions of connections

                            YrDiff = Curr_Yr(key) - Prev_Yr(key)
                            StkDiff = CBroadMobil_Curr - CBroadMobil_Prev
                            AnnDiff = StkDiff / YrDiff

                            CBroadMobil_Prev = CBroadMobil_Curr - AnnDiff
                        End If


                        CBroadMobilAdded = AMAX(CBroadMobil_Curr - CBroadMobil_Prev, 0) 'million connections

                    End If

                    'readjust maintenance stock using current stock (from data or estimation)
                    CBroadMobil_Curr = CBroadMobil(key) * CPOP_Curr(key) / 100 'million connections
                    CBroadMobil_Prev = CBroadMobil_Curr - CBroadMobilAdded

                    If cntx.HistoricMode And cntx.FirstYear < 1990 Then
                        cICTBROADMOBILCOST.Add(key, 0)
                        cINFRAINVESTNEW(key)(BroadbandMobile) = 0
                        cINFRAINVESTMAINT(key)(BroadbandMobile) = 0

                    Else
                        cICTBROADMOBILCOST.Add(key, Nothing)
                        CalcInfraUnitCost(10, 5, ictbroadmobilcostupper, ictbroadmobilcostlower, CGDPPCP(key), cICTBROADMOBILCOST(key))
                        cINFRAINVESTNEW(key)(BroadbandMobile) = CBroadMobilAdded * cICTBROADMOBILCOST(key) 'million connection * (1000 $/conn)= billion $
                        DeppcntBroadmobil = CSng(Math.Exp(Math.Log(2) / ictbroadmobillife) - 1)
                        cINFRAINVESTMAINT(key)(BroadbandMobile) = CBroadMobil_Prev * DeppcntBroadmobil * cICTBROADMOBILCOST(key) 'million connection * (1000 $/conn)= billion $
                    End If
                    '------------ Broadband Mobile Costs End ---------------
                Next

                '------------ Mobile Subscription Capacity Costs ---------------
                Dim Mobil_Current As Single?, CMobil_Curr As Single?, CMobil_Prev As Single?, CMobilAdded As Single?
                Dim DeppcntMobil As Single?

                If Not cntx.HistoricMode Then
                    'jrs 2024/06/13 TbName$ = "GDP/Capita (PPP 2000) Versus Telephone Cell Subscribers Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Telephone Cell Subscribers Per 100 (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    XYTABL = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Curr)
                    XYTABLRP = cntx.GetRegression(TbName).ToEstimator(CGDPCP_Prev)
                End If

                For Each key In cntx.Regionalization.Keys
                    Mobil_Current = CMobil(key) * CPOP(key) / 100 'millions of subscriptions
                    If cntx.HistoricMode Then
                        'hard coding year to avoid taking later years, please revise if earlier data is available
                        'cInfraRoadPavedPcnt(key) = RoadPavedPcntTbl.Fields("1990")

                        'historic?
                        'cMobil_Prev = Mobil_Current
                    Else
                        If CMobilGR(key) > -9999 Then
                            'constant percentage point change
                            CMobil_Curr = CMobil(key) * CPOP_Curr(key) / 100
                            CPOP_Last = CPOP_Curr(key) / (1 + TmpPopGR(key))

                            'mti August 17, 2011 - start
                            'cMobil_Prev = (CMobil(key) - CMobilGR(key)) * CPOP_Last / 100
                            CMobil_Prev = (CMobil(key) / (1 + CMobilGR(key))) * CPOP_Last / 100
                            'mti August 17, 2011 - end

                        Else  'null returned from growth rate calculation
                            ValHold_Curr = XYTABL(key)
                            ValHold_Prev = XYTABLRP(key)
                            CMobil_Curr = ValHold_Curr * CPOP_Curr(key) / 100 'millions of connections
                            CMobil_Prev = ValHold_Prev * CPOP_Prev(key) / 100 'millions of connections

                            YrDiff = Curr_Yr(key) - Prev_Yr(key)
                            StkDiff = CMobil_Curr - CMobil_Prev
                            AnnDiff = StkDiff / YrDiff

                            CMobil_Prev = CMobil_Curr - AnnDiff
                        End If


                        CMobilAdded = AMAX(CMobil_Curr - CMobil_Prev, 0) 'million connections

                    End If

                    'readjust maintenance stock using current stock (from data or estimation)
                    CMobil_Curr = CMobil(key) * CPOP_Curr(key) / 100 'million connections
                    CMobil_Prev = CMobil_Curr - CMobilAdded

                    If cntx.HistoricMode And cntx.FirstYear < 1990 Then
                        cICTMOBILCOST.Add(key, 0)
                        cINFRAINVESTNEW(key)(Mobile) = 0
                        cINFRAINVESTMAINT(key)(Mobile) = 0

                    Else
                        cICTMOBILCOST.Add(key, Nothing)
                        CalcInfraUnitCost(10, 5, ictmobilcostupper, ictmobilcostlower, CGDPPCP(key), cICTMOBILCOST(key), False)
                        cINFRAINVESTNEW(key)(Mobile) = CMobilAdded * cICTMOBILCOST(key) 'million connection * (1000 $/conn)= billion $
                        DeppcntMobil = CSng(Exp(Log(2) / ictmobillife) - 1)

                        'cINFRAINVESTmaint(key,Mobile) = Mobil_Current * DeppcntMobil * cICTMOBILCOST(key) 'million connection * (1000 $/conn)= billion $
                        cINFRAINVESTMAINT(key)(Mobile) = CMobil_Prev * DeppcntMobil * cICTMOBILCOST(key) 'million connection * (1000 $/conn)= billion $

                    End If
                    'mti June 08, 2011 end

                    '------------ Mobile  Subscription Capacity Cost End ---------------
                Next


                Me.processed = True

                'mti 2022/06/21
                PreProcessInfraWatSanNew(cntx)

                'mti 2022/08/17 start
                PreProcessInfraIndices(cntx)
                'mti 2022/08/17 End

            End Sub

            'mti 2022/11/01 'mti 2022/06/21
            Private Sub PreProcessInfraWatSanNew(cntx As IRebuildFacade)

                'mti 2022/08/13 Start
                'Infrastructure Maintenance Parameters; switch
                ReadGlobalInfraParametersNew()
                'mti 2022/08/13 End

                'mti 2022/06/21 start
                '------------ Water Connection: 5 Category JMP ---------------
                ReDim CSANITHH(5, 3)
                Dim i As Integer
                For i = 1 To 5
                    For j = 1 To 3
                        CSANITHH(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CWATERHH(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CWATERHH(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next

                CPOPRURAL = New Dictionary(Of String, Single?)
                QPoverty = New Dictionary(Of String, Single?)

                ReDim CWATERHHCOSTLOWER(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CWATERHHCOSTLOWER(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CWATERHHCOSTHIGHER(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CWATERHHCOSTHIGHER(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CSANITHHCOSTLOWER(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CSANITHHCOSTLOWER(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CSANITHHCOSTHIGHER(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CSANITHHCOSTHIGHER(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next

                ReDim CWaterHHAnnGr(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CWaterHHAnnGr(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CWaterHHAnnChg(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CWaterHHAnnChg(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CSanitHHAnnGr(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CSanitHHAnnGr(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CSanitHHAnnChg(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CSanitHHAnnChg(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next

                'mti 2022/10/04 start
                'use in invest computation
                ReDim CWaterHHAnnGrUse(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CWaterHHAnnGrUse(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CSanitHHAnnGrUse(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        CSanitHHAnnGrUse(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CWaterHHAnnGrPiped(3)
                For j = 1 To 3
                    CWaterHHAnnGrPiped(j) = New Dictionary(Of String, Single?)
                Next
                ReDim CWaterHHAnnGrALB(3)
                For j = 1 To 3
                    CWaterHHAnnGrALB(j) = New Dictionary(Of String, Single?)
                Next
                ReDim CSanitHHAnnGrALB(3)  'at least basic
                For j = 1 To 3
                    CSanitHHAnnGrALB(j) = New Dictionary(Of String, Single?)
                Next
                'mti 2022/10/04 end

                'mti 2022/09/16 start
                ReDim WaterHHGR(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        WaterHHGR(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim SanitHHGR(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        SanitHHGR(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                'mti 2022/09/16 End

                'Dim cWATERHHINVESTCAP(), cWATERHHINVESTMAINT(), cSANITHHINVESTCAP(), cSANITHHINVESTMAINT()
                ReDim cWATERHHINVESTCAP(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        cWATERHHINVESTCAP(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim cWATERHHINVESTMAINT(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        cWATERHHINVESTMAINT(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim cSANITHHINVESTCAP(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        cSANITHHINVESTCAP(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim cSANITHHINVESTMAINT(5, 3)
                For i = 1 To 5
                    For j = 1 To 3
                        cSANITHHINVESTMAINT(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                'mti 2022/06/21 end

                'mti 2022/09/12 start
                ReDim CWaterSafeHH(3, 3)
                For i = 1 To 3
                    For j = 1 To 3
                        CWaterSafeHH(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CSanitSafeHH(3, 3)
                For i = 1 To 3
                    For j = 1 To 3
                        CSanitSafeHH(i, j) = New Dictionary(Of String, Single?)
                    Next
                Next
                ReDim CWaterPipedHH(3)
                For i = 1 To 3
                    CWaterPipedHH(i) = New Dictionary(Of String, Single?)
                Next

                'mti 2022/09/12 end

                'mti 2022/09/19 start
                ReDim CWaterAtLeastBasicHH(3)
                For i = 1 To 3
                    CWaterAtLeastBasicHH(i) = New Dictionary(Of String, Single?)
                Next
                ReDim CSanitAtLeastBasicHH(3)
                For i = 1 To 3
                    CSanitAtLeastBasicHH(i) = New Dictionary(Of String, Single?)
                Next
                'mti 2022/09/19 end

                'mti 2022/06/21 start
                'Collection of data series 5 WASH category, 3 locations
                Dim WaterHHDataTables(5, 3) As IDictionary(Of String, Single?())
                Dim SanitHHDataTables(5, 3) As IDictionary(Of String, Single?())


                Dim WaterHHCostTablesLow(5, 3) As IDictionary(Of String, Single?())
                Dim WaterHHCostTablesHigh(5, 3) As IDictionary(Of String, Single?())
                Dim SanitHHCostTablesLow(5, 3) As IDictionary(Of String, Single?())
                Dim SanitHHCostTablesHigh(5, 3) As IDictionary(Of String, Single?())

                Dim WaterHHSafelyManagedTables(3, 3) As IDictionary(Of String, Single?())
                Dim SanitHHSafelyManagedTables(3, 3) As IDictionary(Of String, Single?())

                Dim WaterHHPipedTable(3) As IDictionary(Of String, Single?())

                Dim WaterHHAtLeastBasicTable(3) As IDictionary(Of String, Single?())
                Dim SanitHHAtLeastBasicTable(3) As IDictionary(Of String, Single?())

                'For holding yearlists of the tables
                Dim yrsWaterHHDataTables As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))
                Dim yrsSanitHHDataTables As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))

                Dim yrsWaterHHCostTablesLow As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))
                Dim yrsSanitHHCostTablesLow As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))
                Dim yrsWaterHHCostTablesHigh As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))
                Dim yrsSanitHHCostTablesHigh As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))

                Dim yrsWaterHHSafelyManagedTables As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))
                Dim yrsSanitHHSafelyManagedTables As New Dictionary(Of Integer, Dictionary(Of Integer, Short()))

                Dim yrsWaterHHPipedTable As New Dictionary(Of Integer, Short())
                Dim yrsWaterHHAtLeastBasicTable As New Dictionary(Of Integer, Short())
                Dim yrsSanitHHAtLeastBasicTable As New Dictionary(Of Integer, Short())


                'mti 2022/04 - declare variables
                Dim Loc As Integer
                Dim Srvc As Integer
                Dim OldRat(5) As Integer
                Dim SmCnt As Integer

                Dim TbNameWaterLoc As String
                Dim TbNameSanitLoc As String

                Dim TbNameWaterCost As String
                Dim TbNameSanitCost As String

                Dim TbNameWater As String
                Dim TbNameSanit As String


                Dim TbNameWaterCostLow As String
                Dim TbNameWaterCostHigh As String

                Dim TbNameSanitCostLow As String
                Dim TbNameSanitCostHigh As String

                'GDP and poverty tables - should already be open

                For Loc = 1 To 3
                    'TbName$ = ""
                    Select Case Loc
                        Case Rural

                            TbNameSanitLoc$ = "SeriesWSSJMPSanitationRural"     '% in IFsHist Table name, but not in .net object name
                            TbNameWaterLoc$ = "SeriesWSSJMPWaterRural"

                            TbNameSanitCost$ = "SeriesWSSJMPSanitationCostRural"
                            TbNameWaterCost$ = "SeriesWSSJMPWaterCostRural"


                        Case Urban

                            TbNameSanitLoc$ = "SeriesWSSJMPSanitationUrban"    '% in IFsHist Table name, but not in .net object name
                            TbNameWaterLoc$ = "SeriesWSSJMPWaterUrban"

                            TbNameSanitCost$ = "SeriesWSSJMPSanitationCostUrban"
                            TbNameWaterCost$ = "SeriesWSSJMPWaterCostUrban"

                        Case Total

                            TbNameSanitLoc$ = "SeriesWSSJMPSanitationTotal"     '% in IFsHist Table name, but not in .net object name
                            TbNameWaterLoc$ = "SeriesWSSJMPWaterTotal"

                            TbNameSanitCost$ = ""
                            TbNameWaterCost$ = ""

                        Case Else
                            TbNameSanitLoc$ = ""
                            TbNameWaterLoc$ = ""

                            TbNameSanitCost$ = ""
                            TbNameWaterCost$ = ""

                    End Select

                    'mti 2022/08/12 start
                    For Srvc% = 1 To 5
                        TbNameSanitCostLow$ = ""
                        TbNameSanitCostHigh$ = ""

                        TbNameWaterCostLow$ = ""
                        TbNameWaterCostHigh$ = ""

                        'For Srvc% = 1 To 5
                        'mti 2022/08/12 end
                        Select Case Srvc
                            Case Surface, OpenDefecation
                                TbNameWater$ = TbNameWaterLoc$ + "Surface"
                                TbNameSanit$ = TbNameSanitLoc$ + "OpenDefecation"



                            Case Unimproved

                                TbNameWater$ = TbNameWaterLoc$ + "Unimproved"
                                TbNameSanit$ = TbNameSanitLoc$ + "Unimproved"

                                If Loc = Rural Then TbNameSanitCostLow$ = TbNameSanitCost + "UnimprovedPitLatrine"

                            Case Limited

                                TbNameWater$ = TbNameWaterLoc$ + "Limited"
                                TbNameSanit$ = TbNameSanitLoc$ + "Limited"

                            Case Basic

                                TbNameWater$ = TbNameWaterLoc$ + "Basic"
                                TbNameSanit$ = TbNameSanitLoc$ + "Basic"

                                If Loc = Rural Then
                                    TbNameSanitCostLow$ = TbNameSanitCost + "DryPitLatrine"
                                    TbNameSanitCostHigh$ = TbNameSanitCost + "WetPitLatrine"
                                End If
                                If Loc = Urban Then
                                    TbNameSanitCostLow$ = TbNameSanitCost + "WetPitLatrine"
                                    TbNameSanitCostHigh$ = TbNameSanitCost + "LatrineWithSepticTank"
                                End If

                                If Loc = Rural Or Loc = Urban Then
                                    TbNameWaterCostLow$ = TbNameWaterCost + "Dugwell"
                                    TbNameWaterCostHigh$ = TbNameWaterCost + "TubewellBorehole"
                                End If


                            Case SafelyManaged

                                TbNameWater$ = TbNameWaterLoc$ + "SafelyManaged"
                                TbNameSanit$ = TbNameSanitLoc$ + "SafelyManaged"

                                If Loc = Rural Then
                                    TbNameSanitCostLow$ = TbNameSanitCost + "PitLatrineWithFSM"
                                    TbNameSanitCostHigh$ = TbNameSanitCost + "PitLatrineWST"
                                End If

                                If Loc = Urban Then
                                    TbNameSanitCostLow$ = TbNameSanitCost + "SepticTankWithFSM"
                                    TbNameSanitCostHigh$ = TbNameSanitCost + "SewerageWithTreatment"
                                End If

                                If Loc = Rural Or Loc = Urban Then
                                    TbNameWaterCostLow$ = TbNameWaterCost + "PipedOnPlot"
                                End If

                            Case Else
                                TbNameWater$ = TbNameWaterLoc$ + ""
                                TbNameSanit$ = TbNameSanitLoc$ + ""
                        End Select



                        If TbNameWater$ <> "" Then
                            'Set WaterHHDataTables(Srvc, Loc) = db.OpenRecordset(TbNameWater$)
                            'WaterHHDataTables(Srvc, Loc).Index = "Country"
                            'WaterHHDataTables(Srvc, Loc).MoveFirst
                            Dim yrs = cntx.GetYears(TbNameWater$).OrderBy(Function(y) y).ToArray()  'this array contains years
                            If Loc% = 1 Then yrsWaterHHDataTables.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsWaterHHDataTables(Srvc%).Add(Loc%, yrs)
                            WaterHHDataTables(Srvc, Loc) = cntx.GetDice(TbNameWater, yrs)   'this contains the data
                        End If


                        If TbNameSanit$ <> "" Then
                            Dim yrs = cntx.GetYears(TbNameSanit).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsSanitHHDataTables.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsSanitHHDataTables(Srvc%).Add(Loc%, yrs)
                            SanitHHDataTables(Srvc, Loc) = cntx.GetDice(TbNameSanit, yrs)
                        End If


                        'Water cost
                        If TbNameWaterCostLow <> "" Then
                            Dim yrs = cntx.GetYears(TbNameWaterCostLow).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsWaterHHCostTablesLow.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsWaterHHCostTablesLow(Srvc%).Add(Loc%, yrs)
                            WaterHHCostTablesLow(Srvc, Loc) = cntx.GetDice(TbNameWaterCostLow, yrs)
                        Else
                            'empty years collection
                            If Loc% = 1 Then yrsWaterHHCostTablesLow.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsWaterHHCostTablesLow(Srvc%).Add(Loc%, Nothing)
                            WaterHHCostTablesLow(Srvc, Loc) = Nothing 'doublecheck with JRS
                        End If

                        If TbNameWaterCostHigh <> "" Then
                            Dim yrs = cntx.GetYears(TbNameWaterCostHigh).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsWaterHHCostTablesHigh.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsWaterHHCostTablesHigh(Srvc%).Add(Loc%, yrs)
                            WaterHHCostTablesHigh(Srvc, Loc) = cntx.GetDice(TbNameWaterCostHigh, yrs)
                        Else
                            'empty years collection
                            If Loc% = 1 Then yrsWaterHHCostTablesHigh.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsWaterHHCostTablesHigh(Srvc%).Add(Loc%, Nothing)
                            WaterHHCostTablesHigh(Srvc, Loc) = Nothing 'doublecheck with JRS
                        End If

                        'Sanitation cost
                        If TbNameSanitCostLow <> "" Then
                            Dim yrs = cntx.GetYears(TbNameSanitCostLow).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsSanitHHCostTablesLow.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsSanitHHCostTablesLow(Srvc%).Add(Loc%, yrs)
                            SanitHHCostTablesLow(Srvc, Loc) = cntx.GetDice(TbNameSanitCostLow, yrs)
                        Else
                            'empty years collection
                            If Loc% = 1 Then yrsSanitHHCostTablesLow.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsSanitHHCostTablesLow(Srvc%).Add(Loc%, Nothing)
                            SanitHHCostTablesLow(Srvc, Loc) = Nothing 'doublecheck with JRS
                        End If

                        If TbNameSanitCostHigh <> "" Then
                            Dim yrs = cntx.GetYears(TbNameSanitCostHigh).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsSanitHHCostTablesHigh.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsSanitHHCostTablesHigh(Srvc%).Add(Loc%, yrs)
                            SanitHHCostTablesHigh(Srvc, Loc) = cntx.GetDice(TbNameSanitCostHigh, yrs)
                        Else
                            'empty years collection
                            If Loc% = 1 Then yrsSanitHHCostTablesHigh.Add(Srvc%, New Dictionary(Of Integer, Short()))
                            yrsSanitHHCostTablesHigh(Srvc%).Add(Loc%, Nothing)
                            SanitHHCostTablesHigh(Srvc, Loc) = Nothing 'doublecheck with JRS
                        End If

                    Next Srvc

                    'mti 2022/09/12 start
                    'Series with access rate to the Safely Managed criteria
                    For SmCnt% = 1 To 3
                        Select Case SmCnt%

                            Case 1
                                TbNameWater$ = TbNameWaterLoc$ + "SMOnPremise"
                                TbNameSanit$ = TbNameSanitLoc$ + "SMDisposedInSitu"
                            Case 2
                                TbNameWater$ = TbNameWaterLoc$ + "SMAsNeeded"
                                TbNameSanit$ = TbNameSanitLoc$ + "SMEmpTreated"
                            Case 3
                                TbNameWater$ = TbNameWaterLoc$ + "SMNotContam"
                                TbNameSanit$ = TbNameSanitLoc$ + "SMWastewaterTreated"
                            Case Else
                                TbNameWater$ = ""
                                TbNameSanit$ = ""

                        End Select

                        If TbNameWater$ <> "" Then
                            Dim yrs = cntx.GetYears(TbNameWater).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsWaterHHSafelyManagedTables.Add(SmCnt, New Dictionary(Of Integer, Short()))
                            yrsWaterHHSafelyManagedTables(SmCnt).Add(Loc%, yrs)
                            WaterHHSafelyManagedTables(SmCnt, Loc) = cntx.GetDice(TbNameWater, yrs)
                        End If

                        If TbNameSanit$ <> "" Then
                            Dim yrs = cntx.GetYears(TbNameSanit).OrderBy(Function(y) y).ToArray()
                            If Loc% = 1 Then yrsSanitHHSafelyManagedTables.Add(SmCnt, New Dictionary(Of Integer, Short()))
                            yrsSanitHHSafelyManagedTables(SmCnt).Add(Loc%, yrs)
                            SanitHHSafelyManagedTables(SmCnt, Loc) = cntx.GetDice(TbNameSanit, yrs)
                        End If

                    Next SmCnt%

                    '% piped
                    TbNameWater$ = TbNameWaterLoc$ + "Piped"
                    Dim yrsPiped = cntx.GetYears(TbNameWater).OrderBy(Function(y) y).ToArray()
                    yrsWaterHHPipedTable.Add(Loc, yrsPiped)      'confirm with JRS
                    WaterHHPipedTable(Loc) = cntx.GetDice(TbNameWater, yrsPiped)

                    'mti 2022/09/12 end

                    'mti 2022/09/19 start
                    '% At Least Basic
                    TbNameWater$ = TbNameWaterLoc$ + "AtLeastBasic"
                    Dim yrsALBWater = cntx.GetYears(TbNameWater).OrderBy(Function(y) y).ToArray()
                    yrsWaterHHAtLeastBasicTable.Add(Loc, yrsALBWater)
                    WaterHHAtLeastBasicTable(Loc) = cntx.GetDice(TbNameWater, yrsALBWater)

                    TbNameSanit$ = TbNameSanitLoc$ + "AtLeastBasic"
                    Dim yrsALBSanit = cntx.GetYears(TbNameSanit).OrderBy(Function(y) y).ToArray()
                    yrsSanitHHAtLeastBasicTable.Add(Loc, yrsALBSanit)
                    SanitHHAtLeastBasicTable(Loc) = cntx.GetDice(TbNameSanit, yrsALBSanit)

                Next    'Loc%

                Dim yrsG As Short() = cntx.GetYears("GDPPCP" & ConstantDollarsYear.ToString).OrderBy(Function(yy) yy).ToArray()
                Dim GDPPCPTbl As IDictionary(Of String, Single?()) = cntx.GetDice("GDPPCP" & ConstantDollarsYear.ToString, yrsG)
                Dim FirstYear As Integer = cntx.FirstYear

                '------------- Poverty -------------------------
                Dim TbName As String, ValHold As Single?
                Dim Estimate As SeriesEstimator

                If cntx.HistoricMode Then
                    TbName$ = "GDP/Capita (PPP 2011) Versus Income Below One Dollar and 90 Cents per Day (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx)  'baseyear (2010) required here, as it uses the function
                    Estimate = cntx.GetRegression(TbName).ToEstimator(CGDPPCP)      'estimates for all countries

                    For Each key In cntx.Regionalization.Keys
                        CINCOMELT1(key) = ApplyLimits(Estimate(key), 0, 70) / 100 * CPOP(key) 'Convert from Percent to Index form
                        QPoverty(key) = CINCOMELT1(key)
                        CPOPRURAL(key) = AMAX(0.00001, CSng(CPOP(key)) - CSng(CPOPURBAN(key)))

                    Next
                Else
                    'jrs 2024/06/28 TbName$ = "GDP/Capita (PPP 2011) Versus Income Below One Dollar and 90 Cents per Day (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Income Below One Dollar and 90 Cents per Day (" & CStr(cntx.BaseYear) & ") Log"
                    TbName$ = getFunctionNameYrOrMstRct(TbName$, cntx.BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                    Dim yearsInc As Short() = cntx.GetYears("SeriesIncBelow1D90cPovCal").OrderBy(Function(y) y).ToArray()
                    Dim incomelt1tbl As IDictionary(Of String, Single?()) = cntx.GetDice("SeriesIncBelow1D90cPovCal", yearsInc)

                    For Each key In cntx.Regionalization.Keys
                        ValHold = EstimateFromMostRecentRevBBH(CGDPPCP(key), cntx.BaseYear, incomelt1tbl, yearsInc,
                        GDPPCPTbl, yrsG, key, TbName$, cntx, 100, 0, "Div", 1000)
                        CINCOMELT1(key) = ValHold / 100 * CPOP(key) 'Convert from Percent to Index form
                        QPoverty(key) = CINCOMELT1(key)
                        CPOPRURAL(key) = AMAX(0.00001, CSng(CPOP(key)) - CSng(CPOPURBAN(key)))
                    Next
                End If
                '------------------ Poverty End ------------------

                '------------ Water Connection: 5 Category JMP ---------------
                If cntx.HistoricMode Then
                    For Each key In cntx.Regionalization.Keys

                        'mti 2022/09/12 start
                        For Loc% = 1 To 3       'Srvc%
                            CWaterPipedHH(Loc%)(key) = getYearOrEarliestWithinRange(WaterHHPipedTable(Loc), yrsWaterHHPipedTable(Loc), key, FirstYear, 10)

                            'mti 2022/09/19 start
                            CWaterAtLeastBasicHH(Loc%)(key) = getYearOrEarliestWithinRange(WaterHHAtLeastBasicTable(Loc), yrsWaterHHAtLeastBasicTable(Loc), key, FirstYear, 10)
                            'mti 2022/09/19 end

                            'mti 2022/10/04 Start
                            Dim DatStartYear = AMAX(2000, FirstYear)
                            CWaterHHAnnGrPiped(Loc)(key) = getAnnualGrowthRate(WaterHHPipedTable(Loc), yrsWaterHHPipedTable(Loc), key, DatStartYear, 0.1, -0.2, 4, 5, 10, 10)
                            CWaterHHAnnGrALB(Loc)(key) = getAnnualGrowthRate(WaterHHAtLeastBasicTable(Loc), yrsWaterHHAtLeastBasicTable(Loc), key, DatStartYear, 0.1, -0.2, 4, 5, 10, 10)
                            'mti 2022/10/04 End

                            For SmCnt% = 1 To 3
                                CWaterSafeHH(SmCnt%, Loc%)(key) = getYearOrEarliestWithinRange(WaterHHSafelyManagedTables(SmCnt%, Loc), yrsWaterHHSafelyManagedTables(SmCnt%)(Loc), key, FirstYear, 10)
                            Next

                            For Srvc% = 1 To 5      'Loc%

                                'mti 2022/09/12 End
                                CWATERHH(Srvc%, Loc%)(key) = getYearOrEarliestWithinRange(WaterHHDataTables(Srvc, Loc), yrsWaterHHDataTables(Srvc)(Loc), key, FirstYear, 10)
                                'mti 2022/10/04 Start
                                'DatStartYear = AMAX(2000, FirstYearHistory)   'mti 2022/08/13 start
                                'CWaterHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(WaterHHDataTables(Srvc, Loc), DatStartYear, 0.1, -0.02, 4, 5, 10, 10)
                                'CWaterHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(WaterHHDataTables(Srvc, Loc), DatStartYear, 10, 0.0005, 4, 5, 10, 10, True)

                                CWaterHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(WaterHHDataTables(Srvc, Loc), yrsWaterHHDataTables(Srvc)(Loc), key, DatStartYear, 0.1, -0.2, 4, 5, 10, 10)
                                CWaterHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(WaterHHDataTables(Srvc, Loc), yrsWaterHHDataTables(Srvc)(Loc), key, DatStartYear, 10, -2, 4, 5, 10, 10, True)
                                'If Not IsNull(CWaterHHAnnGr(Srvc%, Loc)(key)) Then CWaterHHAnnGrUse(Srvc%, Loc)(key) = CWaterHHAnnGr(Srvc%, Loc)(key)
                                'mti 2022/10/04 End
                            Next


                        Next

                    Next
                Else
                    For Each key In cntx.Regionalization.Keys
                        'mti 2022/09/12 start
                        For Loc% = 1 To 3
                            'piped
                            CWaterPipedHH(Loc)(key) = getYearOrMstRct(WaterHHPipedTable(Loc), yrsWaterHHPipedTable(Loc), key, cntx.BaseYear)

                            'mti 2022/09/19 start 'at least basic
                            CWaterAtLeastBasicHH(Loc)(key) = getYearOrMstRct(WaterHHAtLeastBasicTable(Loc), yrsWaterHHAtLeastBasicTable(Loc), key, cntx.BaseYear)
                            'mti 2022/09/19 end

                            'mti 2022/10/04 Start growth rate of piped and at least basic
                            CWaterHHAnnGrPiped(Loc).Add(key, getAnnualGrowthRate(WaterHHPipedTable(Loc), yrsWaterHHPipedTable(Loc), key, cntx.BaseYear, 0.2, -0.2, 4, 5, 10, 10))
                            CWaterHHAnnGrALB(Loc).Add(key, getAnnualGrowthRate(WaterHHAtLeastBasicTable(Loc), yrsWaterHHAtLeastBasicTable(Loc), key, cntx.BaseYear, 0.2, -0.2, 4, 5, 10, 10))
                            'mti 2022/10/04 End

                            'safely,managed sub-criteria - 3
                            For SmCnt = 1 To 3
                                CWaterSafeHH(SmCnt, Loc)(key) = getYearOrMstRct(WaterHHSafelyManagedTables(SmCnt, Loc), yrsWaterHHSafelyManagedTables(SmCnt)(Loc), key, cntx.BaseYear)
                            Next

                            'JMP 5 category - Access Rates and Growth in Access Rates
                            For Srvc = 1 To 5
                                CWATERHH(Srvc, Loc)(key) = getYearOrMstRct(WaterHHDataTables(Srvc, Loc), yrsWaterHHDataTables(Srvc)(Loc), key, cntx.BaseYear)
                                CWaterHHAnnGr(Srvc, Loc).Add(key, getAnnualGrowthRate(WaterHHDataTables(Srvc, Loc), yrsWaterHHDataTables(Srvc)(Loc), key, cntx.BaseYear, 0.2, -0.2, 4, 5, 10, 10))
                                CWaterHHAnnChg(Srvc, Loc).Add(key, getAnnualGrowthRate(WaterHHDataTables(Srvc, Loc), yrsWaterHHDataTables(Srvc)(Loc), key, cntx.BaseYear, 10, -2, 4, 5, 10, 10, True))
                            Next

                        Next
                    Next
                End If

                'mti 2022/10/04 Start

                'Fill in missing growth rates from related series
                For Each key In cntx.Regionalization.Keys
                    'Safely Managed
                    For Loc% = 1 To 3
                        If Not CWaterHHAnnGr(SafelyManaged, Loc)(key).HasValue Then CWaterHHAnnGr(SafelyManaged, Loc)(key) = CWaterHHAnnGrPiped(Loc)(key)
                    Next

                    'Fill in Urban/Rural missing growth rates with national (first choice), or rural/urban (next choice), if available
                    For Srvc% = 1 To 5
                        For Loc% = 1 To 2
                            If Not CWaterHHAnnGr(Srvc, Loc)(key).HasValue Then
                                For TLoc% = 3 To 1 Step -1
                                    If TLoc% <> Loc% And CWaterHHAnnGr(Srvc%, TLoc%)(key).HasValue Then
                                        CWaterHHAnnGr(Srvc, Loc)(key) = CWaterHHAnnGr(Srvc%, TLoc%)(key)
                                        Exit For
                                    End If
                                Next
                            End If
                        Next Loc%
                    Next Srvc%

                    'Fill in basic/safely managed growth rates with piped/at leastbasic
                    'Basic
                    For Loc% = 1 To 2
                        If Not CWaterHHAnnGr(Basic, Loc)(key).HasValue Then CWaterHHAnnGr(Basic, Loc)(key) = CWaterHHAnnGrALB(Loc)(key)
                        If Not CWaterHHAnnGr(Basic, Loc)(key).HasValue Then CWaterHHAnnGr(Basic, Loc)(key) = CWaterHHAnnGrALB(National)(key)
                    Next

                    'Growth rate to be used for investment computation
                    For Srvc% = 1 To 5
                        For Loc% = 1 To 3
                            CWaterHHAnnGrUse(Srvc%, Loc)(key) = Nothing
                            If CWaterHHAnnGr(Srvc%, Loc)(key).HasValue Then CWaterHHAnnGrUse(Srvc%, Loc)(key) = CWaterHHAnnGr(Srvc%, Loc)(key)
                        Next Loc%
                    Next Srvc%
                    'mti 2022/10/04 End

                    '  If CountryName(key) = "Moldova, Republic of" Then Stop
                    ''  If CountryName(key) = "Lebanon" Then Stop
                    ''  If CountryName(key) = "Grenada" Then Stop
                    '  If CountryName(key) = "Zimbabwe" Then Stop

                    'mti 2022/09/12 start
                    'Fill in missing safely managed (urban, rural) from national (safelymanaged). population and/or and piped
                    If Not CWATERHH(SafelyManaged, Rural)(key).HasValue Then

                        If CWATERHH(SafelyManaged, Urban)(key).HasValue And CWATERHH(SafelyManaged, National)(key).HasValue Then
                            'CPOPRURAL(key) = AMAX(0.00001, CPop(key) - CPOPURBAN(key))
                            If CPOPRURAL(key) > 0 Then
                                CWATERHH(SafelyManaged, Rural)(key) = (CWATERHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CWATERHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)
                            Else

                                'mti 2022/09/19 Start
                                CWATERHH(SafelyManaged, Rural)(key) = CWATERHH(SafelyManaged, Urban)(key)
                                'mti 2022/09/19 End

                            End If
                        ElseIf CWATERHH(SafelyManaged, National)(key).HasValue Then

                            'mti 2022/09/20 Start
                            'First try to use national (SM) with at least basic
                            If CWATERHH(SafelyManaged, National)(key).HasValue And CWaterAtLeastBasicHH(National)(key).HasValue Then
                                ComputeFromAtLeastBasic(key, CWaterAtLeastBasicHH, CWATERHH)
                            End If
                            If Not CWATERHH(SafelyManaged, Rural)(key).HasValue Then 'still null; use piped
                                If CWaterPipedHH(Rural)(key).HasValue And CWaterPipedHH(National)(key).HasValue And CWaterPipedHH(National)(key) > 0 Then
                                    CWATERHH(SafelyManaged, Rural)(key) = CWaterPipedHH(Rural)(key) * CWATERHH(SafelyManaged, National)(key) / CWaterPipedHH(National)(key)
                                    CWATERHH(SafelyManaged, Urban)(key) = (CWATERHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CWATERHH(SafelyManaged, Rural)(key) * CPOPRURAL(key)) / CPOPURBAN(key)
                                End If
                            End If
                            'mti 2022/09/20 End


                        ElseIf CWATERHH(SafelyManaged, Urban)(key).HasValue Then 'has urban SM data, but no national SM; this is a less likely branch
                            'mti 2022/09/19 Start
                            If CWaterPipedHH(Rural)(key).HasValue And CWaterPipedHH(Urban)(key) > 0 Then
                                'If Not IsNull(CWaterPipedHH(Urban)(key)) And CWaterPipedHH(Urban)(key) > 0 Then
                                CWATERHH(SafelyManaged, Rural)(key) = CWaterPipedHH(Rural)(key) * CWATERHH(SafelyManaged, Urban)(key) / CWaterPipedHH(Urban)(key)
                            ElseIf CWATERHH(Basic, Rural)(key).hasvalue And CWATERHH(Basic, Urban)(key) > 0 Then
                                CWATERHH(SafelyManaged, Rural)(key) = CWATERHH(Basic, Rural)(key) * CWATERHH(SafelyManaged, Urban)(key) / CWATERHH(Basic, Urban)(key)
                                'mti 2022/09/19 End
                            End If
                        End If

                        'mti 2022/09/15 start
                        'mti 2022/09/19 start
                        'CWATERHH(SafelyManaged, Rural)(key) = AMAX(0, Amin(100, CWATERHH(SafelyManaged, Rural)(key)))
                        'mti 2022/09/19 End
                        'mti 2022/09/15 end

                    ElseIf Not CWATERHH(SafelyManaged, Urban)(key).HasValue Then

                        If CWATERHH(SafelyManaged, Rural)(key).HasValue And CWATERHH(SafelyManaged, National)(key).HasValue Then
                            'CPOPUrban(key) = AMAX(0.00001, CPop(key) - CPOPRural(key))
                            If CPOPURBAN(key) > 0 Then
                                CWATERHH(SafelyManaged, Urban)(key) = (CWATERHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CWATERHH(SafelyManaged, Rural)(key) * CPOPRURAL(key)) / CPOPURBAN(key)
                            Else
                                'mti 2022/09/19 start
                                CWATERHH(SafelyManaged, Urban)(key) = CWATERHH(SafelyManaged, Rural)(key)   'very unlikely
                                'mti 2022/09/19 end
                            End If
                        ElseIf CWATERHH(SafelyManaged, National)(key).HasValue Then

                            'mti 2022/09/20 Start
                            'First try to use national (SM) with at least basic
                            If CWATERHH(SafelyManaged, National)(key).HasValue And CWaterAtLeastBasicHH(National)(key).HasValue Then
                                ComputeFromAtLeastBasic(key, CWaterAtLeastBasicHH, CWATERHH)
                            End If
                            If Not CWATERHH(SafelyManaged, Urban)(key).HasValue Then 'still null; use piped
                                If CWaterPipedHH(Urban)(key).HasValue And CWaterPipedHH(National)(key).HasValue And CWaterPipedHH(National)(key) > 0 Then
                                    CWATERHH(SafelyManaged, Urban)(key) = CWaterPipedHH(Urban)(key) * CWATERHH(SafelyManaged, National)(key) / CWaterPipedHH(National)(key)
                                    CWATERHH(SafelyManaged, Rural)(key) = (CWATERHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CWATERHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)
                                End If
                            End If
                            'mti 2022/09/20 End

                        ElseIf CWATERHH(SafelyManaged, Rural)(key).HasValue Then
                            'mti 2022/09/19 Start
                            If CWaterPipedHH(Urban)(key).HasValue And CWaterPipedHH(Rural)(key) > 0 Then
                                'If Not IsNull(CWaterPipedHH(Rural)(key)) And CWaterPipedHH(Rural)(key) > 0 Then
                                CWATERHH(SafelyManaged, Urban)(key) = CWaterPipedHH(Urban)(key) * CWATERHH(SafelyManaged, Rural)(key) / CWaterPipedHH(Rural)(key)
                            ElseIf CWATERHH(Basic, Urban)(key).HasValue And CWATERHH(Basic, Rural)(key) > 0 Then
                                CWATERHH(SafelyManaged, Urban)(key) = CWATERHH(Basic, Urban)(key) * CWATERHH(SafelyManaged, Rural)(key) / CWATERHH(Basic, Rural)(key)
                                'mti 2022/09/19 End
                            End If

                        End If

                        'mti 2022/09/20 - deleted comments

                    End If
                    'mti 2022/09/12 End

                    'mti 2022/09/19 start

                    'mti 2022/09/20 start
                    For Srvc% = 1 To 5
                        If Not CWATERHH(Srvc%, Rural)(key).HasValue And Not CWATERHH(Srvc%, Urban)(key).HasValue And CWATERHH(Srvc%, National)(key).HasValue Then
                            '                Stop
                            CWATERHH(Srvc%, Rural)(key) = CWATERHH(Srvc%, National)(key)
                            CWATERHH(Srvc%, Urban)(key) = CWATERHH(Srvc%, National)(key)
                        End If
                        For Loc% = 1 To 2
                            If CWATERHH(Srvc%, Loc%)(key).HasValue Then CWATERHH(Srvc%, Loc%)(key) = AMAX(0, Amin(100, CWATERHH(Srvc%, Loc%)(key)))
                        Next
                    Next
                    '        'mti 2022/09/20 End
                    '        For Loc% = 1 To 2
                    '            For Srvc% = 4 To 5
                    '                If Not IsNull(CWATERHH(Srvc%, Loc%)(key)) Then CWATERHH(Srvc%, Loc%)(key) = AMAX(0, Amin(100, CWATERHH(Srvc%, Loc%)(key)))
                    '            Next
                    '        Next
                    '        'mti 2022/09/19 end

                    'mti 2022/09/20 end

                    For Loc% = 1 To 2   '(later) use 3, national data, if available to fill in data, if rural or urban is missing -

                        'mti 2022/09/15 Start
                        If Loc% = Rural And CPOP(key) = CPOPURBAN(key) Then       'Rural = 2
                            'rural pop is 0 (e.g., Hong Kong, Kuwait), access rate put as same as urban, in case there is reverse migration
                            For Srvc% = 1 To 5
                                CWATERHH(Srvc%, Rural)(key) = CWATERHH(Srvc%, Urban)(key)
                            Next
                            Exit For
                        End If
                        'mti 2022/09/15 End


                        Dim NullCount = 0

                        Select Case Loc
                            Case Rural
                                Loc_Str = "Rural"
                            Case Urban
                                Loc_Str = "Urban"
                            Case Total
                                Loc_Str = "Total"
                            Case Else
                                Loc_Str = "Total"
                        End Select

                        For Srvc% = 1 To 5
                            If Not CWATERHH(Srvc%, Loc%)(key).HasValue Then
                                NullCount = NullCount + 1
                            End If
                        Next

                        If NullCount = 1 Then


                            For Srvc% = 1 To 5
                                If Not CWATERHH(Srvc%, Loc%)(key).HasValue Then
                                    Dim SumOthers As Single? = 0
                                    For OtherSrvc% = 1 To 5
                                        If OtherSrvc% <> Srvc% Then SumOthers = SumOthers + CWATERHH(OtherSrvc%, Loc%)(key)
                                    Next
                                    CWATERHH(Srvc%, Loc%)(key) = AMAX(0, 100 - SumOthers)
                                    Exit For
                                End If
                            Next

                        ElseIf NullCount > 1 Then

                            For Srvc% = 1 To 4
                                OddRat(Srvc) = Nothing
                            Next



                            Call AnalFunc("Water Services, Fractional Logit, Surface Water, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Surface) = CSng(Exp(cVal))

                            Call AnalFunc("Water Services, Fractional Logit, Unimproved Source, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Unimproved) = CSng(Exp(cVal))

                            Call AnalFunc("Water Services, Fractional Logit, Limited Service, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Limited) = CSng(Exp(cVal))

                            Call AnalFunc("Water Services, Fractional Logit, Basic Service, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Basic) = CSng(Exp(cVal))

                            Dim denom As Single = 1
                            For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                                denom = denom + CSng(OddRat(Srvc%))
                            Next
                            denom = AMAX(0.001, denom)

                            'mti 2022/09/15 Start
                            OddRat(SafelyManaged) = 1

                            'Whwn Safely Managed is missing for rural and/or urban but available for national;
                            'make sure data/estimations of rural/urban are consistent with national
                            If Not CWATERHH(SafelyManaged, Loc%)(key).HasValue And CWATERHH(SafelyManaged, National)(key).HasValue Then
                                'national data available, use it and estimated urban (Loc%=1) to compute Rural
                                If Loc% = Urban Then
                                    'make sure estimated urban access rate do not result in a headcount over national headcount as per data
                                    Dim SM_UrbanShr_Max As Single
                                    If CPOPRURAL(key) > 0 Then
                                        SM_UrbanShr_Max = CSng(AMAX(0.0001, Amin(0.995, CPOPURBAN(key) / CPOP(key))))
                                    Else
                                        SM_UrbanShr_Max = 1
                                    End If
                                    Dim SM_NationalHC = 0.01 * CPOP(key) * CWATERHH(SafelyManaged, National)(key)
                                    Dim SM_UrbanHC = 0.01 * CPOPURBAN(key) * (1 / denom * 100)
                                    If SM_UrbanHC > SM_NationalHC * SM_UrbanShr_Max Then
                                        SM_UrbanHC = SM_NationalHC * SM_UrbanShr_Max
                                        CWATERHH(SafelyManaged, Urban)(key) = CSng(Amin(100, 100 * SM_UrbanHC / CPOPURBAN(key)))
                                    Else
                                        'take the higher of estimated or national, but keep it within 5% of national
                                        CWATERHH(SafelyManaged, Urban)(key) = CSng(Amin(99.5, Amin(CWATERHH(SafelyManaged, National)(key) + 5, AMAX(CWATERHH(SafelyManaged, National)(key), AMAX(Amin(100, 1 / denom * 100), 0.001)))))
                                    End If
                                End If
                                If Loc% = Rural Then        'Rural = 2, comes later in the loop
                                    'compute from urban and national and population
                                    CWATERHH(SafelyManaged, Rural)(key) = (CWATERHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CWATERHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)
                                End If
                            End If

                            Dim SumData As Single? = 0
                            Dim SumEst As Single? = 0
                            For Srvc% = 1 To 5
                                If CWATERHH(Srvc%, Loc%)(key).HasValue Then
                                    SumData = SumData + CWATERHH(Srvc%, Loc%)(key)
                                Else
                                    SumEst = SumEst + OddRat(Srvc) / denom * 100
                                End If
                            Next
                            If SumEst > 0 Then
                                'mti 2022/09/19 start
                                'NormFct = (100 - SumData) / SumEst  'normalization factor
                                Dim NormFct = AMAX(0, (100 - SumData) / SumEst) 'normalization factor
                                'mti 2022/09/19 End
                                For Srvc% = 1 To 5
                                    'estimations used only for missing categories; normalized to 100 minus sum of data-filled categories
                                    If Not CWATERHH(Srvc%, Loc%)(key).HasValue Then CWATERHH(Srvc%, Loc%)(key) = NormFct * OddRat(Srvc) / denom * 100
                                Next
                            Else    'this is a very unlikely branch; putting in just to make the code failsafe

                                If SumEst < 0 Then
                                    'negative values came out of estimation; give warning (use warning format?)
                                    Debug.Print("negative values came out of estimation water access rates for country " + key)
                                End If

                                'divide the residual (100-data) equally; will be 0 when SumEst is 0 and available data take up 100%
                                Dim NullCount1 = 0
                                For Srvc% = 1 To 5
                                    If Not CWATERHH(Srvc%, Loc%)(key).HasValue Then NullCount1 = NullCount1 + 1
                                Next
                                For Srvc% = 1 To 5
                                    If Not CWATERHH(Srvc%, Loc%)(key).HasValue Then CWATERHH(Srvc%, Loc%)(key) = (100 - SumData) / NullCount1
                                Next
                            End If
                            'mti 2022/09/15 End

                        End If

                        Dim SumAll As Single? = 0
                        For Srvc% = 1 To 5
                            SumAll = SumAll + CWATERHH(Srvc%, Loc%)(key)
                        Next

                        'normalize
                        For Srvc% = 1 To 5
                            CWATERHH(Srvc%, Loc%)(key) = CWATERHH(Srvc%, Loc%)(key) / SumAll * 100
                        Next

                    Next        'Loc%

                    'If ICount% = 18 Then Stop

                    '------------ Sanitation Connection: 5 Category JMP ---------------
                    If cntx.HistoricMode = True Then
                        For Srvc% = 1 To 5
                            For Loc% = 1 To 3

                                'mti 2022/09/19 start
                                CSanitAtLeastBasicHH(Loc%)(key) = getYearOrEarliestWithinRange(SanitHHAtLeastBasicTable(Loc), yrsSanitHHAtLeastBasicTable(Loc), key, FirstYear, 10)
                                'mti 2022/09/19 end

                                CSANITHH(Srvc%, Loc%)(key) = getYearOrEarliestWithinRange(SanitHHDataTables(Srvc, Loc), yrsSanitHHDataTables(Srvc)(Loc), key, FirstYear, 10)

                                'mti 2022/08/13 start
                                'CSanitHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), FirstYearHistory, 0.1, -0.02, 4, 5, 10, 10)
                                'CSanitHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), FirstYearHistory, 10, 0.0005, 4, 5, 10, 10, True)

                                Dim DatStartYear = AMAX(2000, FirstYear)

                                'mti 2022/10/04 Start
                                CSanitHHAnnGrALB(Loc)(key) = getAnnualGrowthRate(SanitHHAtLeastBasicTable(Loc), yrsSanitHHAtLeastBasicTable(Loc), key, DatStartYear, 0.1, -0.2, 4, 5, 10, 10)

                                'CSanitHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), DatStartYear, 0.1, -0.02, 4, 5, 10, 10)
                                'CSanitHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), DatStartYear, 10, 0.0005, 4, 5, 10, 10, True)

                                CSanitHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), yrsSanitHHDataTables(Srvc)(Loc), key, DatStartYear, 0.1, -0.2, 4, 5, 10, 10)
                                CSanitHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), yrsSanitHHDataTables(Srvc)(Loc), key, DatStartYear, 10, -2, 4, 5, 10, 10, True)

                                'If Not IsNull(CSanitHHAnnGr(Srvc%, Loc)(key)) Then CSanitHHAnnGrUse(Srvc%, Loc)(key) = CSanitHHAnnGr(Srvc%, Loc)(key)

                                'mti 2022/10/04 End

                                'mti 2022/08/13 end


                            Next
                        Next
                    Else
                        For Srvc% = 1 To 5
                            For Loc% = 1 To 3

                                'mti 2022/09/19 start
                                CSanitAtLeastBasicHH(Loc%)(key) = getYearOrMstRct(SanitHHAtLeastBasicTable(Loc), yrsSanitHHAtLeastBasicTable(Loc), key, BaseYear)
                                'mti 2022/09/19 end
                                CSANITHH(Srvc%, Loc)(key) = getYearOrMstRct(SanitHHDataTables(Srvc, Loc), yrsSanitHHDataTables(Srvc)(Loc), key, BaseYear)

                                'mti 2022/10/04 Start
                                CSanitHHAnnGrALB(Loc)(key) = getAnnualGrowthRate(SanitHHAtLeastBasicTable(Loc), yrsSanitHHAtLeastBasicTable(Loc), key, BaseYear, 0.2, -0.2, 4, 5, 10, 10)

                                'CSanitHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), BaseYear, 0.1, -0.02, 4, 5, 10, 10)
                                'CSanitHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), BaseYear, 10, 0.0005, 4, 5, 10, 10, True)

                                CSanitHHAnnGr(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), yrsSanitHHDataTables(Srvc)(Loc), key, BaseYear, 0.2, -0.4, 4, 5, 10, 10)
                                CSanitHHAnnChg(Srvc%, Loc)(key) = getAnnualGrowthRate(SanitHHDataTables(Srvc, Loc), yrsSanitHHDataTables(Srvc)(Loc), key, BaseYear, 10, -2, 4, 5, 10, 10, True)

                                'If Not IsNull(CSanitHHAnnGr(Srvc%, Loc)(key)) Then CSanitHHAnnGrUse(Srvc%, Loc)(key) = CSanitHHAnnGr(Srvc%, Loc)(key)

                                'mti 2022/10/04 End


                            Next
                        Next
                    End If


                    'mti 2022/10/04 Start

                    'Fill in Urban/Rural missing growth rates with national (first choice), or rural/urban (next choice), if available
                    For Srvc% = 1 To 5
                        For Loc% = 1 To 2
                            If Not CSanitHHAnnGr(Srvc%, Loc%)(key).HasValue Then
                                For TLoc% = 3 To 1 Step -1
                                    If TLoc% <> Loc% And CSanitHHAnnGr(Srvc%, TLoc%)(key).HasValue Then
                                        CSanitHHAnnGr(Srvc%, Loc%)(key) = CSanitHHAnnGr(Srvc%, TLoc%)(key)
                                        Exit For
                                    End If
                                Next
                            End If
                        Next Loc%
                    Next Srvc%

                    'Fill in basic/safely managed growth rates with piped/at leastbasic
                    'Basic
                    If Not CSanitHHAnnGr(Basic, National)(key).HasValue Then CSanitHHAnnGr(Basic, National)(key) = CSanitHHAnnGrALB(National)(key)
                    If Not CSanitHHAnnGr(SafelyManaged, National)(key).HasValue Then CSanitHHAnnGr(SafelyManaged, National)(key) = CSanitHHAnnGrALB(National)(key)

                    For Loc% = 1 To 2
                        If Not CSanitHHAnnGr(Basic, Loc)(key).HasValue Then CSanitHHAnnGr(Basic, Loc)(key) = CSanitHHAnnGrALB(Loc)(key)
                        If Not CSanitHHAnnGr(SafelyManaged, Loc)(key).HasValue Then CSanitHHAnnGr(SafelyManaged, Loc)(key) = CSanitHHAnnGrALB(Loc)(key)

                        If Not CSanitHHAnnGr(Basic, Loc)(key).HasValue Then CSanitHHAnnGr(Basic, Loc)(key) = CSanitHHAnnGrALB(National)(key)
                        If Not CSanitHHAnnGr(SafelyManaged, Loc)(key).HasValue Then CSanitHHAnnGr(SafelyManaged, Loc)(key) = CSanitHHAnnGrALB(National)(key)

                    Next

                    'Growth rate to be used for investment computation
                    For Srvc% = 1 To 5
                        For Loc% = 1 To 3
                            CSanitHHAnnGrUse(Srvc%, Loc)(key) = Nothing
                            If CSanitHHAnnGr(Srvc%, Loc)(key).HasValue Then CSanitHHAnnGrUse(Srvc%, Loc)(key) = CSanitHHAnnGr(Srvc%, Loc)(key)
                        Next Loc%
                    Next Srvc%
                    'mti 2022/10/04 End


                    'If CountryName(key) = "Romania" Then Stop
                    'mti 2022/09/12 start
                    'Fill in missing safely managed (urban, rural) from national (safelymanaged). population and/or and piped
                    If Not CSANITHH(SafelyManaged, Rural)(key).HasValue Then


                        If CSANITHH(SafelyManaged, Urban)(key).HasValue And CSANITHH(SafelyManaged, National)(key).HasValue Then
                            'CPOPRURAL(key) = AMAX(0.00001, CPop(key) - CPOPURBAN(key))
                            If CPOPRURAL(key) > 0 Then


                                CSANITHH(SafelyManaged, Rural)(key) = (CSANITHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CSANITHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)
                                'mti 2022/09/15 start
                                CSANITHH(SafelyManaged, Rural)(key) = AMAX(0, Amin(100, CSANITHH(SafelyManaged, Rural)(key)))
                                'mti 2022/09/15 end

                            Else
                                'mti 2022/09/19 start
                                'CSANITHH(SafelyManaged, Rural)(key) = 0
                                CSANITHH(SafelyManaged, Rural)(key) = CSANITHH(SafelyManaged, Urban)(key)
                                'mti 2022/09/19 End

                            End If
                            'mti 2022/09/19 Start
                            'Use At least basic national and SM national ratio, if available
                        ElseIf CSANITHH(SafelyManaged, National)(key).HasValue And CSanitAtLeastBasicHH(National)(key).HasValue Then
                            ComputeFromAtLeastBasic(key, CSanitAtLeastBasicHH, CSANITHH)
                            '                If Not IsNull(CSanitAtLeastBasicHH(Urban)(key)) Then
                            '                    'Urban safely managed not available - compute from ALB - Urban and SM, ALB Ratio national
                            '                    CSANITHH(SafelyManaged, Urban)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Urban)(key) * CSANITHH(SafelyManaged, National)(key) / CSanitAtLeastBasicHH(National)(key)))
                            '                    CSANITHH(Basic, Urban)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Urban)(key) - CSANITHH(SafelyManaged, Urban)(key)))
                            '                    CSANITHH(SafelyManaged, Rural)(key) = AMAX(0, Amin(100, (CSANITHH(SafelyManaged, National)(key) * CPop(key) - _
                            '                                                                   CSANITHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)))
                            '                    If Not IsNull(CSanitAtLeastBasicHH(Rural)(key)) Then CSANITHH(Basic, Rural)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Rural)(key) - CSANITHH(SafelyManaged, Rural)(key)))
                            '
                            '                ElseIf Not IsNull(CSanitAtLeastBasicHH(Rural)(key)) Then
                            '
                            '                    CSANITHH(SafelyManaged, Rural)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Rural)(key) * CSANITHH(SafelyManaged, National)(key) / CSanitAtLeastBasicHH(National)(key)))
                            '                    CSANITHH(Basic, Rural)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Rural)(key) - CSANITHH(SafelyManaged, Rural)(key)))
                            '                    CSANITHH(SafelyManaged, Urban)(key) = AMAX(0, Amin(100, (CSANITHH(SafelyManaged, National)(key) * CPop(key) - _
                            '                                                                   CSANITHH(SafelyManaged, Rural)(key) * CPOPRURAL(key)) / CPOPURBAN(key)))
                            '                    If Not IsNull(CSanitAtLeastBasicHH(Urban)(key)) Then CSANITHH(Basic, Urban)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Urban)(key) - CSANITHH(SafelyManaged, Urban)(key)))
                            '
                            '                End If
                            'mti 2022/09/19 End
                        End If

                    ElseIf Not CSANITHH(SafelyManaged, Urban)(key).HasValue Then

                        If CSANITHH(SafelyManaged, Rural)(key).HasValue And CSANITHH(SafelyManaged, National)(key).HasValue Then
                            'compute from rural and national SM and population if avialable
                            If CPOPURBAN(key) > 0 Then
                                CSANITHH(SafelyManaged, Urban)(key) = (CSANITHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CSANITHH(SafelyManaged, Rural)(key) * CPOPRURAL(key)) / CPOPURBAN(key)
                                'mti 2022/09/15 start
                                CSANITHH(SafelyManaged, Urban)(key) = AMAX(0, Amin(100, CSANITHH(SafelyManaged, Urban)(key)))
                                'mti 2022/09/15 end
                            Else
                                CSANITHH(SafelyManaged, Urban)(key) = 0
                            End If
                            '            ElseIf Not IsNull(CSANITHH(SafelyManaged, National)(key)) Then
                            '                   'needs to add the three criteria (as opposed to taking the min as in Water); adding less than threee will underestimate; so not doing
                            '            ElseIf Not IsNull(CSANITHH(SafelyManaged, Rural)(key)) Then
                            'mti 2022/09/19 Start
                            'Use At least basic national and SM national ratio, if available
                        ElseIf CSANITHH(SafelyManaged, National)(key).HasValue And CSanitAtLeastBasicHH(National)(key).HasValue Then
                            ComputeFromAtLeastBasic(key, CSanitAtLeastBasicHH, CSANITHH)
                            '                If Not IsNull(CSanitAtLeastBasicHH(Urban)(key)) Then
                            '                    'Urban safely managed not available - compute from ALB - Urban and SM, ALB Ratio national
                            '                    CSANITHH(SafelyManaged, Urban)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Urban)(key) * CSANITHH(SafelyManaged, National)(key) / CSanitAtLeastBasicHH(National)(key)))
                            '                    CSANITHH(Basic, Urban)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Urban)(key) - CSANITHH(SafelyManaged, Urban)(key)))
                            '                    CSANITHH(SafelyManaged, Rural)(key) = AMAX(0, Amin(100, (CSANITHH(SafelyManaged, National)(key) * CPop(key) - _
                            '                                                                   CSANITHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)))
                            '                    If Not IsNull(CSanitAtLeastBasicHH(Rural)(key)) Then CSANITHH(Basic, Rural)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Rural)(key) - CSANITHH(SafelyManaged, Rural)(key)))
                            '
                            '                ElseIf Not IsNull(CSanitAtLeastBasicHH(Rural)(key)) Then
                            '
                            '                    CSANITHH(SafelyManaged, Rural)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Rural)(key) * CSANITHH(SafelyManaged, National)(key) / CSanitAtLeastBasicHH(National)(key)))
                            '                    CSANITHH(Basic, Rural)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Rural)(key) - CSANITHH(SafelyManaged, Rural)(key)))
                            '                    CSANITHH(SafelyManaged, Urban)(key) = AMAX(0, Amin(100, (CSANITHH(SafelyManaged, National)(key) * CPop(key) - _
                            '                                                                   CSANITHH(SafelyManaged, Rural)(key) * CPOPRURAL(key)) / CPOPURBAN(key)))
                            '                    If Not IsNull(CSanitAtLeastBasicHH(Urban)(key)) Then CSANITHH(Basic, Urban)(key) = AMAX(0, Amin(100, CSanitAtLeastBasicHH(Urban)(key) - CSANITHH(SafelyManaged, Urban)(key)))
                            '
                            '                End If
                            'mti 2022/09/19 End
                        End If

                    End If
                    'mti 2022/09/12 End


                    'mti 2022/09/20 start
                    For Srvc% = 1 To 5
                        If Not CSANITHH(Srvc%, Rural)(key).HasValue And Not CSANITHH(Srvc%, Urban)(key).HasValue And CSANITHH(Srvc%, National)(key).HasValue Then
                            CSANITHH(Srvc%, Rural)(key) = CSANITHH(Srvc%, National)(key)
                            CSANITHH(Srvc%, Urban)(key) = CSANITHH(Srvc%, National)(key)
                        End If
                        For Loc% = 1 To 2
                            If CSANITHH(Srvc%, Loc%)(key).HasValue Then CSANITHH(Srvc%, Loc%)(key) = AMAX(0, Amin(100, CSANITHH(Srvc%, Loc%)(key)))
                        Next
                    Next

                    '        'mti 2022/09/19 start
                    '        For Loc% = 1 To 2
                    '            For Srvc% = 4 To 5      'basic and safely managed
                    '                If Not IsNull(CSANITHH(Srvc%, Loc%)(key)) Then CSANITHH(Srvc%, Loc%)(key) = AMAX(0, Amin(100, CSANITHH(Srvc%, Loc%)(key)))
                    '            Next
                    '        Next
                    '        'mti 2022/09/19 end
                    'mti 2022/09/20 End

                    For Loc% = 1 To 2   '(later) use 3, national data, if available to fill in data, if rural or urban is missing -

                        'mti 2022/09/15 Start
                        If Loc% = Rural And CPOP(key) = CPOPURBAN(key) Then       'Rural = 2
                            'rural pop is 0 (e.g., Hong Kong, Kuwait), access rate put as same as urban, in case there is reverse migration
                            For Srvc% = 1 To 5
                                CSANITHH(Srvc%, Rural)(key) = CSANITHH(Srvc%, Urban)(key)
                            Next
                            Exit For
                        End If
                        'mti 2022/09/15 End

                        Dim NullCount = 0
                        Select Case Loc
                            Case Rural
                                Loc_Str = "Rural"
                            Case Urban
                                Loc_Str = "Urban"
                            Case Total
                                Loc_Str = "Total"
                            Case Else
                                Loc_Str = "Total"
                        End Select

                        For Srvc% = 1 To 5
                            If Not CSANITHH(Srvc%, Loc%)(key).HasValue Then
                                NullCount = NullCount + 1
                            End If
                        Next
                        '    If CountryName(key) = "Germany" Then Stop
                        '    If CountryName(key) = "Kuwait" Then Stop
                        '    If CountryName(key) = "Puerto Rico" Then Stop
                        If NullCount = 1 Then


                            For Srvc% = 1 To 5
                                If Not CSANITHH(Srvc%, Loc%)(key).HasValue Then
                                    Dim SumOthers As Single? = 0
                                    For OtherSrvc% = 1 To 5
                                        If OtherSrvc% <> Srvc% Then SumOthers = SumOthers + CSANITHH(OtherSrvc%, Loc%)(key)
                                    Next
                                    CSANITHH(Srvc%, Loc%)(key) = AMAX(0, 100 - SumOthers)
                                    Exit For
                                End If
                            Next

                        ElseIf NullCount > 1 Then

                            For Srvc% = 1 To 4
                                OddRat(Srvc) = Nothing
                            Next

                            'mti 2022/08/13 start

                            'mti 2022/09/21 start
                            'If sanithhmodel2sw = 0 Then
                            'mti 2022/09/21 End

                            Call AnalFunc("Sanitation Services, Fractional Logit, Open Defecation, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Surface) = CSng(Exp(cVal))

                            Call AnalFunc("Sanitation Services, Fractional Logit, Unimproved Services, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Unimproved) = CSng(Exp(cVal))

                            Call AnalFunc("Sanitation Services, Fractional Logit, Limited Service, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Limited) = CSng(Exp(cVal))

                            Call AnalFunc("Sanitation Services, Fractional Logit, Basic Service, " + Loc_Str,
                            New Single() {CSng(CGDPPCP(key)), CSng(CEDYRSAGE25T(key)), CSng(QPoverty(key))}, cVal,
                            New String() {"GDPPCP", "Educ25", "Poverty"}, cntx)
                            OddRat(Basic) = CSng(Exp(cVal))

                            'mti 2022/09/21 start
                            '                Else
                            '                    Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Open Defecation, " + Loc_Str, _
                            '                    Array(CGDPPCP(key), CEDYRSAGE25T(key)), cVal, _
                            '                    Array("GDPPCP", "Educ25"))
                            '                    OddRat(Surface) = Exp(cVal)
                            '
                            '                    Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Unimproved Services, " + Loc_Str, _
                            '                    Array(CGDPPCP(key), CEDYRSAGE25T(key)), cVal, _
                            '                    Array("GDPPCP", "Educ25"))
                            '                    OddRat(Unimproved) = Exp(cVal)
                            '
                            '                    Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Limited Service, " + Loc_Str, _
                            '                    Array(CGDPPCP(key), CEDYRSAGE25T(key)), cVal, _
                            '                    Array("GDPPCP", "Educ25"))
                            '                    OddRat(Limited) = Exp(cVal)
                            '
                            '                    Call AnalFunc("Sanitation Services Model 2, Fractional Logit, Basic Service, " + Loc_Str, _
                            '                    Array(CGDPPCP(key), CEDYRSAGE25T(key)), cVal, _
                            '                    Array("GDPPCP", "Educ25"))
                            '                    OddRat(Basic) = Exp(cVal)
                            '                End If
                            '                'mti 2022/08/13 End
                            'mti 2022/09/21 End
                            Dim denom As Single? = 1

                            For Srvc% = 1 To 4  '#5, safely managed is the base category of the logistic regresiion
                                denom = denom + OddRat(Srvc%)
                            Next
                            denom = CSng(AMAX(0.001, denom))

                            'mti 2022/09/15 Start
                            OddRat(SafelyManaged) = 1



                            'Whwn Safely Managed is missing for rural and/or urban but available for national;
                            'make sure data/estimations of rural/urban are consistent with national
                            If Not CSANITHH(SafelyManaged, Loc%)(key).HasValue And CSANITHH(SafelyManaged, National)(key).HasValue Then
                                'national data available, use it and estimated urban (Loc%=1) to compute Rural
                                If Loc% = Urban Then
                                    'make sure estimated urban access rate do not result in a headcount over national headcount as per data
                                    Dim SM_UrbanShr_Max As Single
                                    If CPOPRURAL(key) > 0 Then
                                        SM_UrbanShr_Max = CSng(AMAX(0.0001, Amin(0.995, CPOPURBAN(key) / CPOP(key))))
                                    Else
                                        SM_UrbanShr_Max = 1
                                    End If
                                    Dim SM_NationalHC = 0.01 * CPOP(key) * CSANITHH(SafelyManaged, National)(key)
                                    Dim SM_UrbanHC = 0.01 * CPOPURBAN(key) * (1 / denom * 100)
                                    If SM_UrbanHC > SM_NationalHC * SM_UrbanShr_Max Then
                                        SM_UrbanHC = SM_NationalHC * SM_UrbanShr_Max
                                        CSANITHH(SafelyManaged, Urban)(key) = CSng(Amin(100, 100 * SM_UrbanHC / CPOPURBAN(key)))
                                    Else
                                        'take the higher of estimated or national, but keep it within 5% of national
                                        CSANITHH(SafelyManaged, Urban)(key) = CSng(Amin(99.5, Amin(CSANITHH(SafelyManaged, National)(key) + 5, AMAX(CSANITHH(SafelyManaged, National)(key), AMAX(Amin(100, 1 / denom * 100), 0.001)))))
                                    End If
                                End If
                                If Loc% = Rural Then        'Rural = 2, comes later in the loop
                                    'compute from urban and national and population
                                    If CPOPRURAL(key) > 0 Then
                                        CSANITHH(SafelyManaged, Rural)(key) = (CSANITHH(SafelyManaged, National)(key) * CPOP(key) -
                                                                   CSANITHH(SafelyManaged, Urban)(key) * CPOPURBAN(key)) / CPOPRURAL(key)
                                    Else
                                        'rural pop is 0, access rate put as same as urban, in case there is reverse migration
                                        CSANITHH(SafelyManaged, Rural)(key) = CSANITHH(SafelyManaged, Urban)(key)
                                    End If
                                End If
                            End If

                            Dim SumData As Single? = 0
                            Dim SumEst As Single? = 0
                            For Srvc% = 1 To 5
                                If CSANITHH(Srvc%, Loc%)(key).HasValue Then
                                    SumData = SumData + CSANITHH(Srvc%, Loc%)(key)
                                Else
                                    SumEst = SumEst + OddRat(Srvc) / denom * 100
                                End If
                            Next
                            If SumEst > 0 Then
                                'mti 2022/09/19 start
                                'NormFct = (100 - SumData) / SumEst  'normalization factor
                                Dim NormFct = AMAX(0, (100 - SumData) / SumEst) 'normalization factor
                                'mti 2022/09/19 End
                                For Srvc% = 1 To 5
                                    'estimations used only for missing categories; normalized to 100 minus sum of data-filled categories
                                    If Not CSANITHH(Srvc%, Loc%)(key).HasValue Then CSANITHH(Srvc%, Loc%)(key) = NormFct * OddRat(Srvc) / denom * 100
                                Next
                            Else    'this is a very unlikely branch; putting in just to make the code failsafe

                                If SumEst < 0 Then
                                    'negative values came out of estimation; give warning (use warning format?)
                                    Debug.Print("negative values came out of estimation Sanit access rates for country " + key)
                                End If

                                'divide the residual (100-data) equally; will be 0 when SumEst is 0 and available data take up 100%
                                Dim NullCount1 = 0
                                For Srvc% = 1 To 5
                                    If Not CSANITHH(Srvc%, Loc%)(key).HasValue Then NullCount1 = NullCount1 + 1
                                Next
                                For Srvc% = 1 To 5
                                    If Not CSANITHH(Srvc%, Loc%)(key).HasValue Then CSANITHH(Srvc%, Loc%)(key) = (100 - SumData) / NullCount1
                                Next
                            End If
                            'mti 2022/09/15 End




                        End If

                        Dim SumAll As Single? = 0
                        For Srvc% = 1 To 5
                            SumAll = SumAll + CSANITHH(Srvc%, Loc%)(key)
                        Next

                        'jrs 2025/02/25
                        If SumAll > 0 Then
                            'normalize
                            For Srvc% = 1 To 5
                                CSANITHH(Srvc%, Loc%)(key) = CSANITHH(Srvc%, Loc%)(key) / SumAll * 100
                            Next
                        End If

                    Next        'Loc%

                    '--------------------End Sanitation -----------------------


                    '------------ Water and Sanitation Cost : 5 Category JMP: High and Low: First Pass - Read data ---------------
                    'ReDim CSANITHH(Size, 5, 3)


                    For Srvc% = 1 To 5
                        For Loc% = 1 To 3

                            '    If CountryName(key) = "Australia" And Srvc = 5 Then Stop

                            If Not WaterHHCostTablesLow(Srvc, Loc) Is Nothing Then
                                CWATERHHCOSTLOWER(Srvc%, Loc)(key) = getYearOrMstRct(WaterHHCostTablesLow(Srvc, Loc), yrsWaterHHCostTablesLow(Srvc)(Loc), key, BaseYear)
                            Else
                                CWATERHHCOSTLOWER(Srvc%, Loc)(key) = 0
                            End If

                            If Not WaterHHCostTablesHigh(Srvc, Loc) Is Nothing Then
                                'mti 2022/08/12 start
                                CWATERHHCOSTHIGHER(Srvc%, Loc)(key) = getYearOrMstRct(WaterHHCostTablesHigh(Srvc, Loc), yrsWaterHHCostTablesHigh(Srvc)(Loc), key, BaseYear)
                                'CWATERHHCOSTHIGHER(Srvc%, Loc)(key) = getYearOrMstRct(WaterHHCostTablesLow(Srvc, Loc), BaseYear)
                                'mti 2022/08/12 End
                            Else
                                'mti 2022/08/12 start
                                'CWATERHHCOSTLOWER(Srvc%, Loc)(key) = 0
                                CWATERHHCOSTHIGHER(Srvc%, Loc)(key) = 0
                                'mti 2022/08/12 End
                            End If

                            If Not SanitHHCostTablesLow(Srvc, Loc) Is Nothing Then
                                CSANITHHCOSTLOWER(Srvc%, Loc)(key) = getYearOrMstRct(SanitHHCostTablesLow(Srvc, Loc), yrsSanitHHCostTablesLow(Srvc)(Loc), key, BaseYear)
                            Else
                                CSANITHHCOSTLOWER(Srvc%, Loc)(key) = 0
                            End If

                            If Not SanitHHCostTablesHigh(Srvc, Loc) Is Nothing Then
                                'mti 2022/08/12 start
                                'CSANITHHCOSTHIGHER(Srvc%, Loc)(key) = getYearOrMstRct(SanitHHCostTablesLow(Srvc, Loc), BaseYear)
                                CSANITHHCOSTHIGHER(Srvc%, Loc)(key) = getYearOrMstRct(SanitHHCostTablesHigh(Srvc, Loc), yrsSanitHHCostTablesHigh(Srvc)(Loc), key, BaseYear)
                                'mti 2022/08/12 End
                            Else
                                CSANITHHCOSTHIGHER(Srvc%, Loc)(key) = 0
                            End If



                        Next
                    Next



                    'mti 2022/09/21 start
                    For Srvc% = 1 To 5
                        'Natonal Access Rates - After Funding - here  'check if popurban is computed right
                        CWATERHH(Srvc%, National)(key) = CSng((CWATERHH(Srvc%, Rural)(key) * CPOPRURAL(key) + CWATERHH(Srvc%, Urban)(key) * CPOPURBAN(key)) / AMAX(0.0001, CPOP(key)))
                        CSANITHH(Srvc%, National)(key) = CSng((CSANITHH(Srvc%, Rural)(key) * CPOPRURAL(key) + CSANITHH(Srvc%, Urban)(key) * CPOPURBAN(key)) / AMAX(0.0001, CPOP(key)))

                    Next
                    'mti 2022/09/21 End



                Next

                For Srvc% = 1 To 5
                    For Loc% = 1 To 2
                        For Each key In cntx.Regionalization.Keys

                            '    If CountryName(key) = "Australia" And Srvc = 5 And Loc = 1 Then Stop
                            If Not CWATERHHCOSTLOWER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WASHCostWB Groups", Srvc%, Loc%, CWATERHHCOSTLOWER)
                            If Not CWATERHHCOSTLOWER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WB Income Level (High to Low)", Srvc%, Loc%, CWATERHHCOSTLOWER)

                            If Not CWATERHHCOSTHIGHER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WASHCostWB Groups", Srvc%, Loc%, CWATERHHCOSTHIGHER)
                            If Not CWATERHHCOSTHIGHER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WB Income Level (High to Low)", Srvc%, Loc%, CWATERHHCOSTHIGHER)

                            If Not CSANITHHCOSTLOWER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WASHCostWB Groups", Srvc%, Loc%, CSANITHHCOSTLOWER)
                            If Not CSANITHHCOSTLOWER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WB Income Level (High to Low)", Srvc%, Loc%, CSANITHHCOSTLOWER)

                            If Not CSANITHHCOSTHIGHER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WASHCostWB Groups", Srvc%, Loc%, CSANITHHCOSTHIGHER)
                            If Not CSANITHHCOSTHIGHER(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WB Income Level (High to Low)", Srvc%, Loc%, CSANITHHCOSTHIGHER)

                        Next
                    Next
                Next

                'Limited
                For Each key In cntx.Regionalization.Keys
                    For Loc% = 1 To 2
                        CWATERHHCOSTLOWER(Limited, Loc)(key) = CWATERHHCOSTLOWER(Basic, Loc)(key)   'dug well
                        CSANITHHCOSTLOWER(Limited, Loc)(key) = CSANITHHCOSTLOWER(Basic, Loc)(key)       'dry/wet pit
                    Next
                Next


                'mti 2022/10/04 Start
                'Fill in missing growth rates with similar countries, in a ...GrUse variable (..Gr is still null) so it can be used for investment computation -------------
                For Srvc% = 1 To 5
                    For Loc% = 1 To 2
                        For Each key In cntx.Regionalization.Keys

                            If CGDPPCP(key) <= 15 Then
                                If Not CWaterHHAnnGrUse(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WASHCostWB Groups", Srvc%, Loc%, CWaterHHAnnGrUse)
                                If Not CSanitHHAnnGrUse(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WASHCostWB Groups", Srvc%, Loc%, CSanitHHAnnGrUse)
                            Else
                                If Not CWaterHHAnnGrUse(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WB Income Level (High to Low)", Srvc%, Loc%, CWaterHHAnnGrUse)
                                If Not CSanitHHAnnGrUse(Srvc%, Loc)(key).HasValue Then EstimateClosestFromGroup(key, "WB Income Level (High to Low)", Srvc%, Loc%, CSanitHHAnnGrUse)
                            End If

                        Next
                    Next Loc%
                Next Srvc%
                'mti 2022/10/04 End


                'Fill in missing growth rates with similar countries: Do not fill in absolute changes with similar countries -------------
                For Each key In cntx.Regionalization.Keys

                    '  If CountryName(key) = "Australia" Then Stop


                    For Srvc% = 1 To 5
                        cWATERHHINVESTCAP(Srvc%, 3)(key) = 0
                        cWATERHHINVESTMAINT(Srvc%, 3)(key) = 0

                        cSANITHHINVESTCAP(Srvc%, 3)(key) = 0
                        cSANITHHINVESTMAINT(Srvc%, 3)(key) = 0

                    Next Srvc%

                    'mti 2022/08/17 start
                    If watsafenewsw = 1 Then 'new mode
                        cINFRAINVESTNEW(key)(SanitationHH) = 0
                        cINFRAINVESTMAINT(key)(SanitationHH) = 0

                        cINFRAINVESTNEW(key)(SafeWaterHH) = 0
                        cINFRAINVESTMAINT(key)(SafeWaterHH) = 0
                    End If


                    For Loc% = 1 To 2
                        Dim PopTot, sPopTot As Single
                        If Loc% = Rural Then
                            PopTot = CSng(AMAX(0.0001, CPOPRURAL(key)))
                            sPopTot = CSng(AMAX(0.0001, CPOP(key) / (1 + cPOPGR(key) / 100) - CPOPURBAN(key) / (1 + CPOPURBGR(key) / 100)))
                        Else
                            PopTot = CSng(AMAX(0.0001, CPOPURBAN(key)))
                            sPopTot = CSng(AMAX(0.0001, CPOPURBAN(key) / (1 + CPOPURBGR(key) / 100)))
                        End If
                        'mti 2022/08/17 end

                        'Water --- Total Investment
                        For Srvc% = 1 To 5
                            'If CountryName(key) = "Australia" And Srvc = 5 And Loc = 1 Then Stop
                            '--- Compute investment and maintenance costs for the base year ----------------
                            If CWATERHHCOSTLOWER(Srvc%, Loc%)(key) > 0 Or CWATERHHCOSTHIGHER(Srvc%, Loc%)(key) > 0 Then
                                Dim CWaterhh_Curr = CWATERHH(Srvc%, Loc)(key) * PopTot / 100
                                Dim CWaterhh_Prev, lc_shr As Single?
                                If CWaterHHAnnChg(Srvc%, Loc)(key).HasValue Then  ' has data on absolute change
                                    CWaterhh_Prev = (CWATERHH(Srvc%, Loc)(key) - CWaterHHAnnChg(Srvc%, Loc)(key)) * sPopTot / 100
                                Else  'no data on absolute change; filled in growth rate from similar country
                                    'mti 2022/10/04 Start
                                    CWaterhh_Prev = (CWATERHH(Srvc%, Loc)(key) / (1 + CWaterHHAnnGrUse(Srvc%, Loc)(key))) * sPopTot / 100
                                    'mti 2022/10/04 End
                                End If
                                Dim ReqdAddition = AMAX(0, (CWaterhh_Curr - CWaterhh_Prev)) 'Addition ----------

                                '--- Share using lower/higher cost options ---
                                If CWATERHHCOSTLOWER(Srvc%, Loc%)(key) > 0 And CWATERHHCOSTHIGHER(Srvc%, Loc%)(key) > 0 Then
                                    lc_shr = 0.5    'sanithhcostlowershr(R%, Srvc%, Loc%)
                                Else
                                    If CWATERHHCOSTLOWER(Srvc%, Loc%)(key) > 0 Then
                                        lc_shr = 1
                                    Else
                                        lc_shr = 0
                                    End If
                                End If

                                '--- Capital cost --------------
                                cWATERHHINVESTCAP(Srvc%, Loc%)(key) = ReqdAddition * lc_shr * CWATERHHCOSTLOWER(Srvc%, Loc%)(key) * CGDPPC(key) / 100
                                cWATERHHINVESTCAP(Srvc%, Loc%)(key) = cWATERHHINVESTCAP(Srvc%, Loc%)(key) + ReqdAddition * (1 - lc_shr) * CWATERHHCOSTHIGHER(Srvc%, Loc%)(key) * CGDPPC(key) / 100

                                'Maintenance Cost
                                If CWATERHHCOSTLOWER(Srvc%, Loc%)(key) > 0 Then

                                    'mti 2022/08/13 start
                                    Dim Deppcnt1 = Exp(Log(2) / 20) - 1       'waterhhlifelowcost(Srvc%); 20 is a placeholder for now
                                    'gvdb 2024/04/05
            						'unused variable
                                    'Dim lifetime As Single?
                                    If Srvc% = SafelyManaged Then Deppcnt1 = Exp(Log(2) / waterhhlifepiped) - 1
                                    If Srvc% = Basic Or Srvc% = Limited Then Deppcnt1 = Exp(Log(2) / waterhhlifedugwell) - 1
                                    'mti 2022/08/13 End

                                    If Srvc% = Basic Then Deppcnt1 = Exp(Log(2) / 10) - 1        'dug well is low cost basic - will be taken through parameter later
                                    cWATERHHINVESTMAINT(Srvc%, Loc%)(key) = CSng(CWaterhh_Curr * lc_shr * Deppcnt1 * CWATERHHCOSTLOWER(Srvc%, Loc%)(key) * CGDPPC(key) / 100) 'in billion $
                                End If

                                If CWATERHHCOSTHIGHER(Srvc%, Loc%)(key) > 0 Then
                                    'mti 2022/08/13 start
                                    Dim Deppcnt1 = Exp(Log(2) / 20) - 1       'waterhhlifelowcost(Srvc%); 20 is a placeholder for now
                                    If Srvc% = SafelyManaged Then Deppcnt1 = Exp(Log(2) / waterhhlifepiped) - 1
                                    If Srvc% = Basic Or Srvc% = Limited Then Deppcnt1 = Exp(Log(2) / waterhhlifeboreholetubewell) - 1
                                    'mti 2022/08/13 End
                                    cWATERHHINVESTMAINT(Srvc%, Loc%)(key) = CSng(cWATERHHINVESTMAINT(Srvc%, Loc%)(key) +
                                                CWaterhh_Curr * (1 - lc_shr) * Deppcnt1 * CWATERHHCOSTHIGHER(Srvc%, Loc%)(key) * CGDPPC(key) / 100) 'in billion $
                                End If

                            Else    'cost is zero
                                cWATERHHINVESTCAP(Srvc%, Loc%)(key) = 0
                                cWATERHHINVESTMAINT(Srvc%, Loc%)(key) = 0
                            End If

                            'national
                            cWATERHHINVESTCAP(Srvc%, 3)(key) = cWATERHHINVESTCAP(Srvc%, 3)(key) + cWATERHHINVESTCAP(Srvc%, Loc%)(key)
                            cWATERHHINVESTMAINT(Srvc%, 3)(key) = cWATERHHINVESTMAINT(Srvc%, 3)(key) + cWATERHHINVESTMAINT(Srvc%, Loc%)(key)

                            'Sanitation  --- Total Investment
                            '--- Compute investment and maintenance costs for the base year ----------------
                            If CSANITHHCOSTLOWER(Srvc%, Loc%)(key) > 0 Or CSANITHHCOSTHIGHER(Srvc%, Loc%)(key) > 0 Then
                                Dim CSANIThh_Curr = CSANITHH(Srvc%, Loc)(key) * PopTot / 100
                                Dim CSANIThh_Prev, lc_shr As Single?
                                If CSanitHHAnnChg(Srvc%, Loc)(key).HasValue Then  ' has data on absolute change
                                    CSANIThh_Prev = (CSANITHH(Srvc%, Loc)(key) - CSanitHHAnnChg(Srvc%, Loc)(key)) * sPopTot / 100
                                Else  'no data on absolute change; filled in growth rate from similar country
                                    'mti 2022/10/04 Start
                                    CSANIThh_Prev = (CSANITHH(Srvc%, Loc)(key) / (1 + CSanitHHAnnGrUse(Srvc%, Loc)(key))) * sPopTot / 100
                                    'mti 2022/10/04 End
                                End If
                                Dim ReqdAddition = AMAX(0, (CSANIThh_Curr - CSANIThh_Prev)) 'Addition ----------

                                '--- Share using lower/higher cost options ---
                                If CSANITHHCOSTLOWER(Srvc%, Loc%)(key) > 0 And CSANITHHCOSTHIGHER(Srvc%, Loc%)(key) > 0 Then
                                    lc_shr = 0.5    'sanithhcostlowershr(R%, Srvc%, Loc%)
                                Else
                                    If CSANITHHCOSTLOWER(Srvc%, Loc%)(key) > 0 Then
                                        lc_shr = 1
                                    Else
                                        lc_shr = 0
                                    End If
                                End If

                                '--- Capital cost --------------
                                cSANITHHINVESTCAP(Srvc%, Loc%)(key) = ReqdAddition * lc_shr * CSANITHHCOSTLOWER(Srvc%, Loc%)(key) * CGDPPC(key) / 100
                                cSANITHHINVESTCAP(Srvc%, Loc%)(key) = cSANITHHINVESTCAP(Srvc%, Loc%)(key) + ReqdAddition * (1 - lc_shr) * CSANITHHCOSTHIGHER(Srvc%, Loc%)(key) * CGDPPC(key) / 100

                                'Maintenance Cost
                                If CSANITHHCOSTLOWER(Srvc%, Loc%)(key) > 0 Then
                                    'mti 2022/08/13 start
                                    Dim Deppcnt1 = Exp(Log(2) / 20) - 1       'SANIThhlifelowcost(Srvc%); 20 is a placeholder for now
                                    'If Srvc% = Basic Then Deppcnt = Exp(Log(2) / 8) - 1        'pit latrine is low cost basic - parameter later
                                    'If Srvc% = Unimproved Then Deppcnt = Exp(Log(2) / 2) - 1        'ODF  will be taken through parameter later

                                    If Srvc% = SafelyManaged And Loc = Urban Then Deppcnt1 = Exp(Log(2) / sanithhlifesst) - 1
                                    If Srvc% = SafelyManaged And Loc = Rural Then Deppcnt1 = Exp(Log(2) / sanithhlifepitlatrinebasic) - 1
                                    If Srvc% = Basic Or Srvc% = Limited Then Deppcnt1 = Exp(Log(2) / sanithhlifepitlatrinebasic) - 1
                                    If Srvc% = Unimproved And Loc = Rural Then Deppcnt1 = Exp(Log(2) / sanithhlifepitlatrinetrad) - 1
                                    'Deppcnt = Exp(Log(2) / 20) - 1       'waterhhlifelowcost(Srvc%); 20 is a placeholder for now
                                    'mti 2022/08/13 End
                                    cSANITHHINVESTMAINT(Srvc%, Loc%)(key) = CSng(CSANIThh_Curr * lc_shr * Deppcnt1 * CSANITHHCOSTLOWER(Srvc%, Loc%)(key) * CGDPPC(key) / 100) 'in billion $
                                End If

                                If CSANITHHCOSTHIGHER(Srvc%, Loc%)(key) > 0 Then
                                    'mti 2022/08/13 start
                                    Dim Deppcnt1 = Exp(Log(2) / 20) - 1      'SANIThhlifehighcost(Srvc%)
                                    'If Srvc% = SafelyManaged Then Deppcnt1 = Exp(Log(2) / sanithhlifesst) - 1

                                    If Srvc% = SafelyManaged And Loc = Urban Then Deppcnt1 = Exp(Log(2) / sanithhlifesst) - 1
                                    If Srvc% = SafelyManaged And Loc = Rural Then Deppcnt1 = Exp(Log(2) / sanithhlifepitlatrinebasic) - 1
                                    If Srvc% = Basic Or Srvc% = Limited Then Deppcnt1 = Exp(Log(2) / sanithhlifepitlatrinebasic) - 1
                                    'mti 2022/08/13 End

                                    cSANITHHINVESTMAINT(Srvc%, Loc%)(key) = CSng(cSANITHHINVESTMAINT(Srvc%, Loc%)(key) +
                                                CSANIThh_Curr * (1 - lc_shr) * Deppcnt1 * CSANITHHCOSTHIGHER(Srvc%, Loc%)(key) * CGDPPC(key) / 100) 'in billion $
                                End If

                            Else    'cost is zero
                                cSANITHHINVESTCAP(Srvc%, Loc%)(key) = 0
                                cSANITHHINVESTMAINT(Srvc%, Loc%)(key) = 0
                            End If

                            'national
                            cSANITHHINVESTCAP(Srvc%, 3)(key) = cSANITHHINVESTCAP(Srvc%, 3)(key) + cSANITHHINVESTCAP(Srvc%, Loc%)(key)
                            cSANITHHINVESTMAINT(Srvc%, 3)(key) = cSANITHHINVESTMAINT(Srvc%, 3)(key) + cSANITHHINVESTMAINT(Srvc%, Loc%)(key)

                            'mti 2022/08/17 start
                            If watsafenewsw = 1 Then 'new mode
                                cINFRAINVESTNEW(key)(SanitationHH) = cINFRAINVESTNEW(key)(SanitationHH) + cSANITHHINVESTCAP(Srvc%, Loc%)(key)
                                cINFRAINVESTMAINT(key)(SanitationHH) = cINFRAINVESTMAINT(key)(SanitationHH) + cSANITHHINVESTMAINT(Srvc%, Loc%)(key)

                                cINFRAINVESTNEW(key)(SafeWaterHH) = cINFRAINVESTNEW(key)(SafeWaterHH) + cWATERHHINVESTCAP(Srvc%, Loc%)(key)
                                cINFRAINVESTMAINT(key)(SafeWaterHH) = cINFRAINVESTMAINT(key)(SafeWaterHH) + cWATERHHINVESTMAINT(Srvc%, Loc%)(key)
                            End If
                            'mti 2022/08/17 end

                        Next Srvc%



                    Next Loc%

                    'mti 2022/09/16 start
                    For Loc% = 1 To 3
                        For Srvc% = 1 To 5
                            If Not CWaterHHAnnGr(Srvc%, Loc)(key).HasValue Then CWaterHHAnnGr(Srvc%, Loc)(key) = -9999
                            If Not CSanitHHAnnGr(Srvc%, Loc)(key).HasValue Then CSanitHHAnnGr(Srvc%, Loc)(key) = -9999
                        Next
                    Next
                    'mti 2022/09/16 end

                Next


                'mti 2022/08/17 start If watsafenewsw = 1 Then



                '    Next




            End Sub

            'mti 2022/11/01
            Private Sub ReadGlobalInfraParametersNew()
                'mti 2022/08/13  New Proc
                Using db As IFsContext = New IFsContext()
                    Dim gp As GlobalParameters

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "sanithhmodel2sw").Single()
                    sanithhmodel2sw = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "waterhhlifepiped").Single()
                    waterhhlifepiped = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "waterhhlifeboreholetubewell").Single()
                    waterhhlifeboreholetubewell = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "waterhhlifedugwell").Single()
                    waterhhlifedugwell = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "sanithhlifesst").Single()
                    sanithhlifesst = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "sanithhlifepitlatrinebasic").Single()
                    sanithhlifepitlatrinebasic = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "sanithhlifepitlatrinetrad").Single()
                    sanithhlifepitlatrinetrad = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "watsafenewsw").Single()
                    watsafenewsw = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "watsafefwdsw").Single()
                    watsafefwdsw = CSng(gp.Value)

                End Using


            End Sub

            'mti 2022/08/17
            Private Sub PreProcessInfraIndices(cntx As IRebuildFacade)
                '-------- Infrastructure Index -------------
                For IICCnt% = 1 To cntx.NInfraIndexTradComp
                    INFRAINDTRADCOMPMEANI(IICCnt%) = 0
                    INFRAINDTRADCOMPSDI(IICCnt%) = 0
                Next

                For IICCnt% = 1 To cntx.NInfraIndexICTComp
                    INFRAINDICTZCOMPMEANI(IICCnt%) = 0
                    INFRAINDICTZCOMPSDI(IICCnt%) = 0
                Next
                'mti February 28, 2012 -end


                For IICCnt% = 1 To cntx.NInfraIndTranComp
                    INFRAINDTRANCOMPMEANI(IICCnt%) = 0
                    INFRAINDTRANCOMPSDI(IICCnt%) = 0
                Next

                For IICCnt% = 1 To cntx.NInfraIndElecComp
                    INFRAINDELECCOMPMEANI(IICCnt%) = 0
                    INFRAINDELECCOMPSDI(IICCnt%) = 0
                Next

                For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                    INFRAINDWATSANCOMPMEANI(IICCnt%) = 0
                    INFRAINDWATSANCOMPSDI(IICCnt%) = 0
                Next
                'mti Sep 12, 2011 - end

                'mti 2013/09/16 - Infra Fix - start
                Dim CBroadMobilSum As Single, CBroadSum As Single, CMobilSum As Single
                CBroadMobilSum = 0
                CBroadSum = 0
                CMobilSum = 0
                'mti 2013/09/16 - Infra Fix -end

                For Each key In cntx.Regionalization.Keys
                    If CPOP(key) <= 0 Then
                        Debug.Print("Population cannot be 0 or negative")
                        Exit Sub
                    End If

                    Dim InfraVals(4) As Single?
                    CInfraIndTranComp.Add(key, InfraVals)

                    'mti Sep 12, 2011 - start
                    CInfraIndTranComp(key)(Road_PerCapita) = CSng(Log(CDbl(CRoadDens(key) * CLandArea(key) / CPOP(key))))  'km per 1000 person
                    CInfraIndTranComp(key)(Road_PerLand) = CSng(Log(CSng(CRoadDens(key))))   'km per 1000 hectare
                    CInfraIndTranComp(key)(Road_Access) = CSng(Log(CSng(AMAX(0.0001, CSng(CRoadRAI(key)))))) 'Road access index, lower bound set at one to avoid negative
                    CInfraIndTranComp(key)(Road_Paved) = CSng(Log(CSng(AMAX(0.0001, cInfraRoadPavedPcnt(key))))) 'Road paved percent, lower bound set at one to avoid negative

                    For IICCnt% = 1 To cntx.NInfraIndTranComp
                        INFRAINDTRANCOMPMEANI(IICCnt%) = CSng(INFRAINDTRANCOMPMEANI(IICCnt%) + CInfraIndTranComp(key)(IICCnt%))
                    Next

                    Dim InfraVals2(4) As Single?
                    CInfraIndElecComp.Add(key, InfraVals2)

                    CInfraIndElecComp(key)(Elec_Gen) = CSng(Log(CSng(CInfraElecGenCap(key))))  'electricity generation capacity per capita
                    CInfraIndElecComp(key)(Elec_Acc) = CSng(Log(CSng(AMAX(0.0001, CInfraElecAccTotal(key)))))  'electricity access, percentage, lower bound set at one to avoid negative

                    'for transmission loss; lower is better: value bound between 3 to 90; 91-90 = 1 (trying to avoid values < 1, as it is logged) ; 91-3=88;
                    CInfraIndElecComp(key)(Elec_Tran) = CSng(Log(CSng(Amin(88, AMAX(1, 91 - CINFRAELECTRANLOSS(key)))))) 'electricity transmission loss, percentage, lower bound set at one to avoid negative

                    CInfraIndElecComp(key)(Elec_Share) = CSng(Log(CSng(CEnElecShrEnDem(key))))   'electricity share of total energy

                    For IICCnt% = 1 To cntx.NInfraIndElecComp
                        INFRAINDELECCOMPMEANI(IICCnt%) = CSng(INFRAINDELECCOMPMEANI(IICCnt%) + CInfraIndElecComp(key)(IICCnt%))
                    Next

                    Dim WatVals(3) As Single?
                    CInfraIndWatSanComp.Add(key, WatVals)

                    'mti 2022/08/17 start If watsafenewsw = 1 Then
                    'CInfraIndWatSanComp(ICount%, Watsan_Wat) = Log(AMAX(0.0001, CWATSAFE(ICount%, 3))) 'safe water access, household connection percentage, lower bound set at one to avoid negative
                    'CInfraIndWatSanComp(ICount%, Watsan_San) = Log(AMAX(0.0001, CSANITATION(ICount%, 3))) 'sanitation access, household connection percentage, lower bound set at one to avoid negative
                    Dim Water_Safe_Rate As Single?, Sanit_Safe_Rate As Single?
                    'mti 2022/10/20 start 'watsafefwdsw
                    'If watsafenewsw = 1 Then
                    If watsafenewsw = 1 And watsafefwdsw = 1 Then
                        'mti 2022/10/20 End
                        'newer 5 category model
                        Water_Safe_Rate = CWATERHH(5, National)(key)
                        Sanit_Safe_Rate = CSANITHH(5, National)(key)
                    Else
                        'older 3 category model
                        Water_Safe_Rate = CWATSAFE3(key)
                        Sanit_Safe_Rate = CSANITATION3(key)
                    End If
                    CInfraIndWatSanComp(key)(Watsan_Wat) = CSng(Log(CSng(AMAX(0.0001, Water_Safe_Rate)))) 'safe water access, household connection percentage, lower bound set at one to avoid negative
                    CInfraIndWatSanComp(key)(Watsan_San) = CSng(Log(CSng(AMAX(0.0001, Sanit_Safe_Rate)))) 'sanitation access, household connection percentage, lower bound set at one to avoid negative
                    'mti 2022/08/17 End
                    CInfraIndWatSanComp(key)(Watsan_WasteTreat) = CSng(Log(CSng(AMAX(0.0001, CWATWASTETREAT(key))))) 'waste water treatment access, household connection percentage, lower bound set at one to avoid negative

                    For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                        INFRAINDWATSANCOMPMEANI(IICCnt%) = CSng(INFRAINDWATSANCOMPMEANI(IICCnt%) + CInfraIndWatSanComp(key)(IICCnt%))
                    Next

                    'mti Sep 12, 2011 - end

                    Dim InfIdxVals(9) As Single?
                    CLNCompPerPop.Add(key, InfIdxVals)
                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(TotalRoad) = CSng(Log(CSng(CRoadDens(key) * CLandArea(key) / CPOP(key) / 1000)))
                    '        CLNCompPerPop(key)(TotalRoad) = Log(CRoadDens(key) / CPop(key))
                    'mti July 18, 2011 -end

                    'mti October 20, 2011 -start
                    '        INFRAIMODCOMPMEANI(TotalRoad) = INFRAIMODCOMPMEANI(TotalRoad) + CLNCompPerPop(key)(TotalRoad)


                    INFRAINDTRADCOMPMEANI(TotalRoad) = CSng(INFRAINDTRADCOMPMEANI(TotalRoad) + CLNCompPerPop(key)(TotalRoad))

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(ElecGen) = CSng(Log(CDbl(CInfraElecGenCap(key))))
                    '        CLNCompPerPop(ICount%, ElecGen) = Log(CInfraElecGenCap(ICount%) / CGDP(ICount%))
                    'mti July 18, 2011 - end

                    '        INFRAIMODCOMPMEANI(ElecGen) = INFRAIMODCOMPMEANI(ElecGen) + CLNCompPerPop(ICount%, ElecGen)
                    INFRAINDTRADCOMPMEANI(ElecGen) = INFRAINDTRADCOMPMEANI(ElecGen) + CSng(CLNCompPerPop(key)(ElecGen))

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(ElecAcc) = CSng(Log(CDbl(AMAX(0.0001, CInfraElecAccTotal(key)) / 100)))
                    '        CLNCompPerPop(ICount%, ElecAcc) = Log(AMAX(1, CInfraElecAccTotal(ICount%)))
                    'mti July 18, 2011 - end
                    '        INFRAIMODCOMPMEANI(ElecAcc) = INFRAIMODCOMPMEANI(ElecAcc) + CLNCompPerPop(ICount%, ElecAcc)
                    'mti Sep 12, 2011 - start
                    INFRAINDTRADCOMPMEANI(ElecAcc) = INFRAINDTRADCOMPMEANI(ElecAcc) + CSng(CLNCompPerPop(key)(ElecAcc))
                    'mti Sep 12, 2011 - end

                    'mti 2022/08/17 start
                    'mti 2022/10/20 start 'watsafefwdsw
                    'If watsafenewsw = 1 Then
                    If watsafenewsw = 1 And watsafefwdsw = 1 Then
                        'mti 2022/10/20 end
                        'newer 5 category model
                        CLNCompPerPop(key)(WaterCon) = CSng(Log(CSng(AMAX(0.0001, CWATERHH(5, National)(key)) / 100)))
                        CLNCompPerPop(key)(SanitCon) = CSng(Log(CSng(AMAX(0.0001, CSANITHH(5, National)(key)) / 100)))
                    Else
                        'older 3 category model
                        CLNCompPerPop(key)(WaterCon) = CSng(Log(CSng(AMAX(0.0001, CWATSAFE3(key)) / 100)))
                        CLNCompPerPop(key)(SanitCon) = CSng(Log(CSng(AMAX(0.0001, CSANITATION3(key)) / 100)))
                    End If
                    INFRAINDTRADCOMPMEANI(WaterCon) = INFRAINDTRADCOMPMEANI(WaterCon) + CSng(CLNCompPerPop(key)(WaterCon))
                    INFRAINDTRADCOMPMEANI(SanitCon) = INFRAINDTRADCOMPMEANI(SanitCon) + CSng(CLNCompPerPop(key)(SanitCon))
                    'mti 2022/08/17 End

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(WaterCon) = CSng(Log(CSng(AMAX(0.0001, CWATSAFE3(key)) / 100)))
                    '        CLNCompPerPop(key)(WaterCon) = Log(AMAX(1, CWATSAFE3(key)))
                    'mti July 18, 2011 - end
                    '        INFRAIMODCOMPMEANI(WaterCon) = INFRAIMODCOMPMEANI(WaterCon) + CLNCompPerPop(key)(WaterCon)
                    INFRAINDTRADCOMPMEANI(WaterCon) = CSng(INFRAINDTRADCOMPMEANI(WaterCon) + CLNCompPerPop(key)(WaterCon))

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(SanitCon) = CSng(Log(CSng(AMAX(0.0001, CSANITATION3(key)) / 100)))
                    '        CLNCompPerPop(key)(SanitCon) = Log(AMAX(1, CSANITATION3(key)))
                    'mti July 18, 2011 - end
                    '        INFRAIMODCOMPMEANI(SanitCon) = INFRAIMODCOMPMEANI(SanitCon) + CLNCompPerPop(key)(SanitCon)
                    INFRAINDTRADCOMPMEANI(SanitCon) = CSng(INFRAINDTRADCOMPMEANI(SanitCon) + CLNCompPerPop(key)(SanitCon))

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(FixedPhone) = CSng(Log(CSng(AMAX(0.0001, CTeleDens(key)) / 100)))

                    '        CLNCompPerPop(key)(FixedPhone) = Log(AMAX(1, CTeleDens(key)))
                    'mti July 18, 2011 - end
                    '        INFRAIMODCOMPMEANI(FixedPhone) = INFRAIMODCOMPMEANI(FixedPhone) + CLNCompPerPop(key)(FixedPhone)

                    'mti June 08, 2011 -start
                    INFRAINDTRADCOMPMEANI(FixedPhone) = CSng(INFRAINDTRADCOMPMEANI(FixedPhone) + CLNCompPerPop(key)(FixedPhone))

                    Dim InfraVals3(4) As Single?
                    CInfraIndICTZComp.Add(key, InfraVals3)
                    'mti February 28, 2012 -start
                    CInfraIndICTZComp(key)(Fixed_Phone) = CLNCompPerPop(key)(FixedPhone)
                    INFRAINDICTZCOMPMEANI(Fixed_Phone) = CSng(INFRAINDICTZCOMPMEANI(Fixed_Phone) + CInfraIndICTZComp(key)(Fixed_Phone))
                    'mti February 28, 2012 - end

                    'mti June 08, 2011 -end

                    'mti Sep 12, 2011 - start
                    'INFRAINDICTCOMPMEANI(Fixed_Phone) = INFRAINDICTCOMPMEANI(Fixed_Phone) + CLNCompPerPop(key)(FixedPhone)
                    'mti Sep 12, 2011 - end

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(FixedBroadband) = CSng(Log(CSng(AMAX(0.0001, CBroadband(key)) / 100)))

                    'mti February 28, 2012 -start
                    CInfraIndICTZComp(key)(Fixed_Broadband) = CLNCompPerPop(key)(FixedBroadband)
                    INFRAINDICTZCOMPMEANI(Fixed_Broadband) = CSng(INFRAINDICTZCOMPMEANI(Fixed_Broadband) + CInfraIndICTZComp(key)(Fixed_Broadband))
                    'mti February 28, 2012 - end


                    '        CLNCompPerPop(key)(FixedBroadband) = Log(AMAX(1, CBroadband(key)))
                    'mti July 18, 2011 -end
                    '        INFRAIMODCOMPMEANI(FixedBroadband) = INFRAIMODCOMPMEANI(FixedBroadband) + CLNCompPerPop(key)(FixedBroadband)

                    'mti June 08, 2011 -start
                    INFRAINDTRADCOMPMEANI(FixedBroadband) = CSng(INFRAINDTRADCOMPMEANI(FixedBroadband) + CLNCompPerPop(key)(FixedBroadband))
                    'mti June 08, 2011 -end

                    'mti Sep 12, 2011 - start
                    'INFRAINDICTCOMPMEANI(Fixed_Broadband) = INFRAINDICTCOMPMEANI(Fixed_Broadband) + CLNCompPerPop(key)(FixedBroadband)
                    'mti Sep 12, 2011 - end

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(MobilePhone) = CSng(Log(CSng(AMAX(0.0001, Amin(CMobil(key), 100)) / 100))) 'was .5 low bound for all ICT

                    'mti February 28, 2012 -start
                    CInfraIndICTZComp(key)(Mobile_Phone) = CLNCompPerPop(key)(MobilePhone)
                    INFRAINDICTZCOMPMEANI(Mobile_Phone) = CSng(INFRAINDICTZCOMPMEANI(Mobile_Phone) + CInfraIndICTZComp(key)(Mobile_Phone))
                    'mti February 28, 2012 - end

                    '        CLNCompPerPop(key)(MobilePhone) = Log(AMAX(1, Amin(CMobil(key), 100)))
                    'mti July 18, 2011 -end
                    '        INFRAIMODCOMPMEANI(MobilePhone) = INFRAIMODCOMPMEANI(MobilePhone) + CLNCompPerPop(key)(MobilePhone)

                    'mti Sep 12, 2011 - start
                    'INFRAINDICTCOMPMEANI(Mobile_Phone) = INFRAINDICTCOMPMEANI(Mobile_Phone) + CLNCompPerPop(key)(MobilePhone)
                    'mti Sep 12, 2011 - end

                    'mti July 18, 2011 -start
                    CLNCompPerPop(key)(MobileBroadband) = CSng(Log(CSng(AMAX(0.0001, CBroadMobil(key)) / 100)))

                    'mti 2013/09/16 - Infra Fix - start
                    CBroadMobilSum = CBroadMobilSum + CSng(CBroadMobil(key))
                    CBroadSum = CBroadSum + CSng(CBroadband(key))
                    CMobilSum = CMobilSum + CSng(CMobil(key))
                    'mti 2013/09/16 - Infra Fix -end

                    '        CLNCompPerPop(key)(MobileBroadband) = Log(AMAX(1, CBroadMobil(key)))
                    'mti July 18, 2011 - end
                    '        INFRAIMODCOMPMEANI(MobileBroadband) = INFRAIMODCOMPMEANI(MobileBroadband) + CLNCompPerPop(key)(MobileBroadband)

                    'mti February 28, 2012 -start
                    CInfraIndICTZComp(key)(Mobile_Broadband) = CLNCompPerPop(key)(MobileBroadband)
                    INFRAINDICTZCOMPMEANI(Mobile_Broadband) = CSng(INFRAINDICTZCOMPMEANI(Mobile_Broadband) + CInfraIndICTZComp(key)(Mobile_Broadband))
                    'mti February 28, 2012 - end



                    'mti Sep 12, 2011 - start
                    'INFRAINDICTCOMPMEANI(Mobile_Broadband) = INFRAINDICTCOMPMEANI(Mobile_Broadband) + CLNCompPerPop(key)(MobileBroadband)
                    'mti Sep 12, 2011 - end
                    Dim InfraVals4(4) As Single?
                    CInfraICTIndComp.Add(key, InfraVals4)
                    CInfraICTIndComp(key)(Fixed_Phone) = Amin(100, AMAX(0, CTeleDens(key)))
                    CInfraICTIndComp(key)(Fixed_Broadband) = Amin(100, AMAX(0, CBroadband(key)))

                    'mti July 18, 2011 -start - change to 150; should really not be hard coded
                    '        'assuming a 120 saturation in the base case; cannot read ictmobilsaturation at this point
                    '        CInfraICTIndComp(key)( Mobile_Phone) = Amin(120, AMAX(0, CMobil(key))) * 5 / 6
                    '        CInfraICTIndComp(key)( Mobile_Broadband) = Amin(120, AMAX(0, CBroadMobil(key))) * 5 / 6

                    'assuming a 150 saturation in the base case; cannot read ictmobilsaturation at this point
                    CInfraICTIndComp(key)(Mobile_Phone) = Amin(150, AMAX(0, CMobil(key))) * 2 / 3
                    CInfraICTIndComp(key)(Mobile_Broadband) = Amin(150, AMAX(0, CBroadMobil(key))) * 2 / 3
                    'mti July 18, 2011 -end - change to 150; should really not be hard coded

                Next

                For IICCnt% = 1 To cntx.NInfraIndexTradComp
                    INFRAINDTRADCOMPMEANI(IICCnt%) = INFRAINDTRADCOMPMEANI(IICCnt%) / cntx.Regionalization.Count
                Next

                'mti February 28, 2012 -start
                'mti Sep 12, 2011 - start
                For IICCnt% = 1 To cntx.NInfraIndexICTComp
                    INFRAINDICTZCOMPMEANI(IICCnt%) = INFRAINDICTZCOMPMEANI(IICCnt%) / cntx.Regionalization.Count
                Next
                'mti February 28, 2012 -end



                For IICCnt% = 1 To cntx.NInfraIndTranComp
                    INFRAINDTRANCOMPMEANI(IICCnt%) = INFRAINDTRANCOMPMEANI(IICCnt%) / cntx.Regionalization.Count
                Next

                For IICCnt% = 1 To cntx.NInfraIndElecComp
                    INFRAINDELECCOMPMEANI(IICCnt%) = INFRAINDELECCOMPMEANI(IICCnt%) / cntx.Regionalization.Count
                Next

                For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                    INFRAINDWATSANCOMPMEANI(IICCnt%) = INFRAINDWATSANCOMPMEANI(IICCnt%) / cntx.Regionalization.Count
                Next

                'variance
                For Each key In cntx.Regionalization.Keys

                    For IICCnt% = 1 To cntx.NInfraIndexTradComp
                        INFRAINDTRADCOMPSDI(IICCnt%) = CSng(INFRAINDTRADCOMPSDI(IICCnt%) + (CLNCompPerPop(key)(IICCnt%) - INFRAINDTRADCOMPMEANI(IICCnt%)) ^ 2)
                    Next

                    'mti February 28, 2012 -start
                    For IICCnt% = 1 To cntx.NInfraIndexICTComp
                        INFRAINDICTZCOMPSDI(IICCnt%) = CSng(INFRAINDICTZCOMPSDI(IICCnt%) + (CInfraIndICTZComp(key)(IICCnt%) - INFRAINDICTZCOMPMEANI(IICCnt%)) ^ 2)
                    Next


                    'mti February 28, 2012 - end

                    '        'mti 2013/09/16 - Infra Fix - start
                    If CBroadMobilSum = 0 Then INFRAINDICTZCOMPSDI(Mobile_Broadband) = 0
                    If CBroadSum = 0 Then INFRAINDICTZCOMPSDI(Fixed_Broadband) = 0
                    If CMobilSum = 0 Then INFRAINDICTZCOMPSDI(Mobile_Phone) = 0
                    '        'mti 2013/09/16 - Infra Fix -end

                    'mti Sep 12, 2011 - start
                    For IICCnt% = 1 To cntx.NInfraIndTranComp
                        INFRAINDTRANCOMPSDI(IICCnt%) = CSng(INFRAINDTRANCOMPSDI(IICCnt%) + (CInfraIndTranComp(key)(IICCnt%) - INFRAINDTRANCOMPMEANI(IICCnt%)) ^ 2)
                    Next

                    For IICCnt% = 1 To cntx.NInfraIndElecComp
                        INFRAINDELECCOMPSDI(IICCnt%) = CSng(INFRAINDELECCOMPSDI(IICCnt%) + (CInfraIndElecComp(key)(IICCnt%) - INFRAINDELECCOMPMEANI(IICCnt%)) ^ 2)
                    Next

                    For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                        INFRAINDWATSANCOMPSDI(IICCnt%) = CSng(INFRAINDWATSANCOMPSDI(IICCnt%) + (CInfraIndWatSanComp(key)(IICCnt%) - INFRAINDWATSANCOMPMEANI(IICCnt%)) ^ 2)
                    Next

                Next

                For IICCnt% = 1 To cntx.NInfraIndexTradComp
                    INFRAINDTRADCOMPSDI(IICCnt%) = CSng(Sqrt(CSng(INFRAINDTRADCOMPSDI(IICCnt%) / cntx.Regionalization.Count)))
                Next

                'mti February 28, 2012 -start
                'mti Sep 12, 2011 - start
                For IICCnt% = 1 To cntx.NInfraIndexICTComp
                    INFRAINDICTZCOMPSDI(IICCnt%) = CSng(Sqrt(CSng(INFRAINDICTZCOMPSDI(IICCnt%) / cntx.Regionalization.Count)))
                Next
                'mti February 28, 2012 - end

                For IICCnt% = 1 To cntx.NInfraIndTranComp
                    INFRAINDTRANCOMPSDI(IICCnt%) = CSng(Sqrt(CSng(INFRAINDTRANCOMPSDI(IICCnt%) / cntx.Regionalization.Count)))
                Next

                For IICCnt% = 1 To cntx.NInfraIndElecComp
                    INFRAINDELECCOMPSDI(IICCnt%) = CSng(Sqrt(CSng(INFRAINDELECCOMPSDI(IICCnt%) / cntx.Regionalization.Count)))
                Next

                For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                    INFRAINDWATSANCOMPSDI(IICCnt%) = CSng(Sqrt(CSng(INFRAINDWATSANCOMPSDI(IICCnt%) / cntx.Regionalization.Count)))
                Next

                Dim TotComp As Single, WtTradPerComp As Single, WtICTPerComp As Single
                Dim WtICTzPerComp As Single

                TotComp = 0
                For IICCnt% = 1 To cntx.NInfraIndexTradComp
                    TotComp = TotComp + infraindtradcompwt(IICCnt%)
                Next
                WtTradPerComp = 1 / TotComp

                TotComp = 0
                For IICCnt% = 1 To cntx.NInfraIndexICTComp
                    TotComp = TotComp + infraindictcompwt(IICCnt%)
                Next
                WtICTPerComp = 1 / TotComp

                'mti February 28, 2012 -start
                TotComp = 0
                For IICCnt% = 1 To cntx.NInfraIndexICTComp
                    TotComp = TotComp + infraindictzcompwt(IICCnt%)
                Next
                WtICTzPerComp = 1 / TotComp
                'mti February 28, 2012 -end

                'mti Sep 12, 2011 - start
                TotComp = 0
                For IICCnt% = 1 To cntx.NInfraIndTranComp
                    TotComp = TotComp + infraindtrancompwt(IICCnt%)
                Next
                Dim WtTranPerComp As Single
                WtTranPerComp = 1 / TotComp

                TotComp = 0
                For IICCnt% = 1 To cntx.NInfraIndElecComp
                    TotComp = TotComp + infraindeleccompwt(IICCnt%)
                Next
                Dim WtelecPerComp As Single
                WtelecPerComp = 1 / TotComp

                TotComp = 0
                For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                    TotComp = TotComp + infraindwatsancompwt(IICCnt%)
                Next
                Dim WtWatSanPerComp As Single
                WtWatSanPerComp = 1 / TotComp

                Dim zcomp As Single?
                For Each key In cntx.Regionalization.Keys
                    CINFRAINDICT.Add(key, 0)
                    For IICCnt% = 1 To cntx.NInfraIndexICTComp
                        'mti May 2011 - start
                        'Total components 9; 1 to 5 traditional; 6 to 9 ICT ; their are 3 separate sub-dimensions though
                        '            ZComp = (CLNCompPerPop(key, NInfraIndexTradComp% + IICCnt%) - INFRAINDICTCOMPMEANI(IICCnt%)) / INFRAINDICTCOMPSDI(IICCnt%)
                        '            CINFRAINDICT(key) = CINFRAINDICT(key) + WtICTPerComp * infraindictcompwt(IICCnt%) * ZComp

                        CINFRAINDICT(key) = CINFRAINDICT(key) + WtICTPerComp * infraindictcompwt(IICCnt%) * CInfraICTIndComp(key)(IICCnt%)
                        'mti May 2011 - end
                    Next

                    'mti February 28, 2012 - start
                    CINFRAINDICTZ(key) = 0
                    For IICCnt% = 1 To cntx.NInfraIndexICTComp
                        If INFRAINDICTZCOMPSDI(IICCnt%) <> 0 Then
                            zcomp = (CInfraIndICTZComp(key)(IICCnt%) - INFRAINDICTZCOMPMEANI(IICCnt%)) / INFRAINDICTZCOMPSDI(IICCnt%)
                        Else
                            zcomp = 0
                        End If

                        CINFRAINDICTZ(key) = CINFRAINDICTZ(key) + WtICTzPerComp * infraindictzcompwt(IICCnt%) * zcomp
                    Next
                    'mti February 28, 2012 - end

                    'mti Sep 12, 2011 - start
                    CINFRAINDTRAN.Add(key, 0)
                    For IICCnt% = 1 To cntx.NInfraIndTranComp
                        If INFRAINDTRANCOMPSDI(IICCnt%) <> 0 Then
                            zcomp = (CInfraIndTranComp(key)(IICCnt%) - INFRAINDTRANCOMPMEANI(IICCnt%)) / INFRAINDTRANCOMPSDI(IICCnt%)
                        Else
                            zcomp = 0
                        End If
                        CINFRAINDTRAN(key) = CINFRAINDTRAN(key) + WtTranPerComp * infraindtrancompwt(IICCnt%) * zcomp
                    Next

                    CINFRAINDELEC.Add(key, 0)
                    For IICCnt% = 1 To cntx.NInfraIndElecComp
                        If INFRAINDELECCOMPSDI(IICCnt%) <> 0 Then
                            zcomp = (CInfraIndElecComp(key)(IICCnt%) - INFRAINDELECCOMPMEANI(IICCnt%)) / INFRAINDELECCOMPSDI(IICCnt%)
                        Else
                            zcomp = 0
                        End If
                        CINFRAINDELEC(key) = CINFRAINDELEC(key) + WtelecPerComp * infraindeleccompwt(IICCnt%) * zcomp
                    Next


                    CINFRAINDWATSAN.Add(key, 0)
                    For IICCnt% = 1 To cntx.NInfraIndWatSanComp
                        If INFRAINDWATSANCOMPSDI(IICCnt%) <> 0 Then
                            zcomp = (CInfraIndWatSanComp(key)(IICCnt%) - INFRAINDWATSANCOMPMEANI(IICCnt%)) / INFRAINDWATSANCOMPSDI(IICCnt%)
                        Else
                            zcomp = 0
                        End If
                        CINFRAINDWATSAN(key) = CINFRAINDWATSAN(key) + WtWatSanPerComp * infraindwatsancompwt(IICCnt%) * zcomp
                    Next


                    CINFRAINDTRAD(key) = (CINFRAINDTRAN(key) + CINFRAINDELEC(key) + CINFRAINDWATSAN(key)) / 3

                Next

                cntx.Store("cINFRAINVESTNEW", cINFRAINVESTNEW)
                cntx.Store("cINFRAINVESTMAINT", cINFRAINVESTMAINT)
                cntx.Store("CInfraElecAccTotal", CInfraElecAccTotal)
                cntx.Store("cINFRAROADPAVEDCOST", cINFRAROADPAVEDCOST)
                cntx.Store("cINFRAROADUNPAVEDCOST", cINFRAROADUNPAVEDCOST)
                cntx.Store("cINFRAELECGENCOST", cINFRAELECGENCOST)
                cntx.Store("cSANITATIONCOST", cSANITATIONCOST)

                cntx.Store("cWATSAFECOST", cWATSAFECOST)
                cntx.Store("cINFRATELECOST", cINFRATELECOST)
                cntx.Store("cICTMOBILCOST", cICTMOBILCOST)
                cntx.Store("cICTBROADCOST", cICTBROADCOST)
                cntx.Store("cICTBROADMOBILCOST", cICTBROADMOBILCOST)

                cntx.Store("cLANDIRCOST", cLANDIRCOST)
                cntx.Store("cWATWASTETREATCOST", cWATWASTETREATCOST)
                cntx.Store("cSANITATIONIMPCOST", cSANITATIONIMPCOST)
                cntx.Store("cWATSAFEIMPCOST", cWATSAFEIMPCOST)
                cntx.Store("cINFRAELECACCRURALCOST", cINFRAELECACCRURALCOST)
                cntx.Store("cINFRAELECACCURBANCOST", cINFRAELECACCURBANCOST)
                cntx.Store("CSANITATION1", CSANITATION1)
                cntx.Store("CLandIrAreaEquip", CLandIrAreaEquip)
                cntx.Store("CWATSAFE3", CWATSAFE3)
                ' mark pre-processed for all three series
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = False

                Dim Vals As IDictionary(Of Integer, Single?)

                Select Case seriesName
                    Case "SANITATION_OthUnImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitationGR1

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthTwoDim("SanitationGR", Vals, cntx, "OthUnImproved")

                        series = CSANITATION1
                    Case "SANITATION_Shared"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitationGR2

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthTwoDim("SanitationGR", Vals, cntx, "Shared")

                        series = CSANITATION2
                    Case "SANITATION_Improved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitationGR3

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthTwoDim("SanitationGR", Vals, cntx, "Improved")

                        series = CSANITATION3
                    Case "WATSAFE_UnImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWatSafeGR1

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthTwoDim("WatSafeGR", Vals, cntx, "UnImproved")

                        series = CWATSAFE1
                    Case "WATSAFE_OthImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWatSafeGR2

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthTwoDim("WatSafeGR", Vals, cntx, "OthImproved")

                        series = CWATSAFE2
                    Case "WATSAFE_Piped"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWatSafeGR3

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthTwoDim("WatSafeGR", Vals, cntx, "Piped")

                        series = CWATSAFE3
                    Case "WATERHH_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(1, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Surface", "Urban")

                        series = CWATERHH(1, 1)
                    Case "WATERHH_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(1, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Surface", "Rural")

                        series = CWATERHH(1, 2)
                    Case "WATERHH_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(1, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Surface", "Total")

                        series = CWATERHH(1, 3)
                    Case "WATERHH_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(2, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Unimproved", "Urban")

                        series = CWATERHH(2, 1)
                    Case "WATERHH_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(2, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Unimproved", "Rural")

                        series = CWATERHH(2, 2)
                    Case "WATERHH_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(2, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Unimproved", "Total")

                        series = CWATERHH(2, 3)
                    Case "WATERHH_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(3, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Limited", "Urban")

                        series = CWATERHH(3, 1)
                    Case "WATERHH_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(3, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Limited", "Rural")

                        series = CWATERHH(3, 2)
                    Case "WATERHH_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(3, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Limited", "Total")

                        series = CWATERHH(3, 3)
                    Case "WATERHH_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(4, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Basic", "Urban")

                        series = CWATERHH(4, 1)
                    Case "WATERHH_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(4, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Basic", "Rural")

                        series = CWATERHH(4, 2)
                    Case "WATERHH_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(4, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "Basic", "Total")

                        series = CWATERHH(4, 3)
                    Case "WATERHH_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(5, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "SafelyManaged", "Urban")

                        series = CWATERHH(5, 1)
                    Case "WATERHH_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(5, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "SafelyManaged", "Rural")

                        series = CWATERHH(5, 2)
                    Case "WATERHH_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CWaterHHAnnGr(5, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("WaterHHGR", Vals, cntx, "SafelyManaged", "Total")

                        series = CWATERHH(5, 3)
                    Case "SANITHH_OpenDefecation_Urban" 'mti 2022/06/21 'jrs 2023/06/24  it said Surface
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(1, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "OpenDefecation", "Urban")

                        series = CSANITHH(1, 1)
                    Case "SANITHH_OpenDefecation_Rural" 'mti 2022/06/21 'jrs 2023/06/24 it said Surface
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(1, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "OpenDefecation", "Rural")

                        series = CSANITHH(1, 2)
                    Case "SANITHH_OpenDefecation_Total" 'mti 2022/06/21 'jrs 2023/06/24 it said Surface
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(1, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "OpenDefecation", "Total")

                        series = CSANITHH(1, 3)
                    Case "SANITHH_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(2, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Unimproved", "Urban")

                        series = CSANITHH(2, 1)
                    Case "SANITHH_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(2, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Unimproved", "Rural")

                        series = CSANITHH(2, 2)
                    Case "SANITHH_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(2, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Unimproved", "Total")

                        series = CSANITHH(2, 3)
                    Case "SANITHH_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(3, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Limited", "Urban")

                        series = CSANITHH(3, 1)
                    Case "SANITHH_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(3, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Limited", "Rural")

                        series = CSANITHH(3, 2)
                    Case "SANITHH_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(3, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Limited", "Total")

                        series = CSANITHH(3, 3)
                    Case "SANITHH_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(4, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Basic", "Urban")

                        series = CSANITHH(4, 1)
                    Case "SANITHH_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(4, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Basic", "Rural")

                        series = CSANITHH(4, 2)
                    Case "SANITHH_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(4, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "Basic", "Total")

                        series = CSANITHH(4, 3)
                    Case "SANITHH_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(5, 1)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "SafelyManaged", "Urban")

                        series = CSANITHH(5, 1)
                    Case "SANITHH_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(5, 2)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "SafelyManaged", "Rural")

                        series = CSANITHH(5, 2)
                    Case "SANITHH_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CSanitHHAnnGr(5, 3)

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthThreeDim("SanitHHGR", Vals, cntx, "SafelyManaged", "Total")

                        series = CSANITHH(5, 3)
                    Case "WATERHHCOSTLOWER_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(1, 1)
                    Case "WATERHHCOSTLOWER_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(1, 2)
                    Case "WATERHHCOSTLOWER_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(1, 3)
                    Case "WATERHHCOSTLOWER_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(2, 1)
                    Case "WATERHHCOSTLOWER_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(2, 2)
                    Case "WATERHHCOSTLOWER_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(2, 3)
                    Case "WATERHHCOSTLOWER_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(3, 1)
                    Case "WATERHHCOSTLOWER_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(3, 2)
                    Case "WATERHHCOSTLOWER_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(3, 3)
                    Case "WATERHHCOSTLOWER_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(4, 1)
                    Case "WATERHHCOSTLOWER_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(4, 2)
                    Case "WATERHHCOSTLOWER_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(4, 3)
                    Case "WATERHHCOSTLOWER_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(5, 1)
                    Case "WATERHHCOSTLOWER_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(5, 2)
                    Case "WATERHHCOSTLOWER_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTLOWER(5, 3)
                    Case "WATERHHCOSTHIGHER_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(1, 1)
                    Case "WATERHHCOSTHIGHER_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(1, 2)
                    Case "WATERHHCOSTHIGHER_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(1, 3)
                    Case "WATERHHCOSTHIGHER_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(2, 1)
                    Case "WATERHHCOSTHIGHER_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(2, 2)
                    Case "WATERHHCOSTHIGHER_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(2, 3)
                    Case "WATERHHCOSTHIGHER_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(3, 1)
                    Case "WATERHHCOSTHIGHER_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(3, 2)
                    Case "WATERHHCOSTHIGHER_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(3, 3)
                    Case "WATERHHCOSTHIGHER_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(4, 1)
                    Case "WATERHHCOSTHIGHER_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(4, 2)
                    Case "WATERHHCOSTHIGHER_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(4, 3)
                    Case "WATERHHCOSTHIGHER_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(5, 1)
                    Case "WATERHHCOSTHIGHER_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(5, 2)
                    Case "WATERHHCOSTHIGHER_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERHHCOSTHIGHER(5, 3)
                    Case "SANITHHCOSTLOWER_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(1, 1)
                    Case "SANITHHCOSTLOWER_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(1, 2)
                    Case "SANITHHCOSTLOWER_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(1, 3)
                    Case "SANITHHCOSTLOWER_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(2, 1)
                    Case "SANITHHCOSTLOWER_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(2, 2)
                    Case "SANITHHCOSTLOWER_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(2, 3)
                    Case "SANITHHCOSTLOWER_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(3, 1)
                    Case "SANITHHCOSTLOWER_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(3, 2)
                    Case "SANITHHCOSTLOWER_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(3, 3)
                    Case "SANITHHCOSTLOWER_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(4, 1)
                    Case "SANITHHCOSTLOWER_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(4, 2)
                    Case "SANITHHCOSTLOWER_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(4, 3)
                    Case "SANITHHCOSTLOWER_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(5, 1)
                    Case "SANITHHCOSTLOWER_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(5, 2)
                    Case "SANITHHCOSTLOWER_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTLOWER(5, 3)
                    Case "SANITHHCOSTHIGHER_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(1, 1)
                    Case "SANITHHCOSTHIGHER_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(1, 2)
                    Case "SANITHHCOSTHIGHER_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(1, 3)
                    Case "SANITHHCOSTHIGHER_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(2, 1)
                    Case "SANITHHCOSTHIGHER_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(2, 2)
                    Case "SANITHHCOSTHIGHER_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(2, 3)
                    Case "SANITHHCOSTHIGHER_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(3, 1)
                    Case "SANITHHCOSTHIGHER_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(3, 2)
                    Case "SANITHHCOSTHIGHER_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(3, 3)
                    Case "SANITHHCOSTHIGHER_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(4, 1)
                    Case "SANITHHCOSTHIGHER_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(4, 2)
                    Case "SANITHHCOSTHIGHER_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(4, 3)
                    Case "SANITHHCOSTHIGHER_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(5, 1)
                    Case "SANITHHCOSTHIGHER_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(5, 2)
                    Case "SANITHHCOSTHIGHER_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CSANITHHCOSTHIGHER(5, 3)
                    Case "WATERHHINVESTCAP_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(1, 1)
                    Case "WATERHHINVESTCAP_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(1, 2)
                    Case "WATERHHINVESTCAP_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(1, 3)
                    Case "WATERHHINVESTCAP_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(2, 1)
                    Case "WATERHHINVESTCAP_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(2, 2)
                    Case "WATERHHINVESTCAP_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(2, 3)
                    Case "WATERHHINVESTCAP_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(3, 1)
                    Case "WATERHHINVESTCAP_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(3, 2)
                    Case "WATERHHINVESTCAP_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(3, 3)
                    Case "WATERHHINVESTCAP_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(4, 1)
                    Case "WATERHHINVESTCAP_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(4, 2)
                    Case "WATERHHINVESTCAP_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(4, 3)
                    Case "WATERHHINVESTCAP_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(5, 1)
                    Case "WATERHHINVESTCAP_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(5, 2)
                    Case "WATERHHINVESTCAP_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTCAP(5, 3)
                    Case "WATERHHINVESTMAINT_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(1, 1)
                    Case "WATERHHINVESTMAINT_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(1, 2)
                    Case "WATERHHINVESTMAINT_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(1, 3)
                    Case "WATERHHINVESTMAINT_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(2, 1)
                    Case "WATERHHINVESTMAINT_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(1, 2)
                    Case "WATERHHINVESTMAINT_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(2, 3)
                    Case "WATERHHINVESTMAINT_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(3, 1)
                    Case "WATERHHINVESTMAINT_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(3, 2)
                    Case "WATERHHINVESTMAINT_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(3, 3)
                    Case "WATERHHINVESTMAINT_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(4, 1)
                    Case "WATERHHINVESTMAINT_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(4, 2)
                    Case "WATERHHINVESTMAINT_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(4, 3)
                    Case "WATERHHINVESTMAINT_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(5, 1)
                    Case "WATERHHINVESTMAINT_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(5, 2)
                    Case "WATERHHINVESTMAINT_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cWATERHHINVESTMAINT(5, 3)
                    Case "SANITHHINVESTCAP_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(1, 1)
                    Case "SANITHHINVESTCAP_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(1, 2)
                    Case "SANITHHINVESTCAP_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(1, 3)
                    Case "SANITHHINVESTCAP_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(2, 1)
                    Case "SANITHHINVESTCAP_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(2, 2)
                    Case "SANITHHINVESTCAP_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(2, 3)
                    Case "SANITHHINVESTCAP_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(3, 1)
                    Case "SANITHHINVESTCAP_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(3, 2)
                    Case "SANITHHINVESTCAP_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(3, 3)
                    Case "SANITHHINVESTCAP_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(4, 1)
                    Case "SANITHHINVESTCAP_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(4, 2)
                    Case "SANITHHINVESTCAP_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(4, 3)
                    Case "SANITHHINVESTCAP_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(5, 1)
                    Case "SANITHHINVESTCAP_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(5, 2)
                    Case "SANITHHINVESTCAP_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTCAP(5, 3)
                    Case "SANITHHINVESTMAINT_Surface_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(1, 1)
                    Case "SANITHHINVESTMAINT_Surface_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(1, 2)
                    Case "SANITHHINVESTMAINT_Surface_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(1, 3)
                    Case "SANITHHINVESTMAINT_Unimproved_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(2, 1)
                    Case "SANITHHINVESTMAINT_Unimproved_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(2, 2)
                    Case "SANITHHINVESTMAINT_Unimproved_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(2, 3)
                    Case "SANITHHINVESTMAINT_Limited_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(3, 1)
                    Case "SANITHHINVESTMAINT_Limited_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(3, 2)
                    Case "SANITHHINVESTMAINT_Limited_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(3, 3)
                    Case "SANITHHINVESTMAINT_Basic_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(4, 1)
                    Case "SANITHHINVESTMAINT_Basic_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(4, 2)
                    Case "SANITHHINVESTMAINT_Basic_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(4, 3)
                    Case "SANITHHINVESTMAINT_SafelyManaged_Urban" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(5, 1)
                    Case "SANITHHINVESTMAINT_SafelyManaged_Rural" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(5, 2)
                    Case "SANITHHINVESTMAINT_SafelyManaged_Total" 'mti 2022/06/21
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cSANITHHINVESTMAINT(5, 3)
                    Case "WATERFILTERED_UnImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERFILTERED1
                    Case "WATERFILTERED_OthImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERFILTERED2
                    Case "WATERFILTERED_Piped"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATERFILTERED3
                    Case "WATWASTE"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATWASTEGR
                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("WATWASTEGR", Vals, cntx)

                        series = CWATWASTE
                    Case "WATWASTETREAT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CWATWASTETREATGR
                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("WATWASTETREATGR", Vals, cntx)

                        series = CWATWASTETREAT
                    Case "INFRAELECACC_Urban"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 100
                        series = CInfraElecAccUrban
                    Case "INFRAELECACC_Rural"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 100
                        series = CInfraElecAccRural
                    Case "INFRAELECACC_Total"
                        bLimits = True
                        MinVal = 1
                        MaxVal = 100
                        series = CInfraElecAccTotal
                    Case "INFRAELECGENCAP"
                        bLimits = True
                        MinVal = 0.0001
                        MaxVal = 20
                        series = CInfraElecGenCapGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("InfraElecGenCapGR", Vals, cntx)

                        series = CInfraElecGenCap
                    Case "INFRAELEC"
                        series = CInfraElec
                    Case "INFRAELECTRANLOSS"
                        bLimits = True
                        MinVal = 0.01
                        MaxVal = 100
                        series = cInfraElecTranLossGR
                        weightSeries = CGDP

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("InfraElecTranLossGR", Vals, cntx)

                        series = CINFRAELECTRANLOSS
                    Case "INFRAELECPROD"
                        series = CInfraElecProd
                        bSum = True
                    Case "ENELECSHRENDEM"
                        bLimits = True
                        MinVal = 0.001
                        MaxVal = 100
                        series = CEnElecShrEnDemGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("EnElecShrEnDemGR", Vals, cntx)

                        series = CEnElecShrEnDem
                        weightSeries = CGDP
                    Case "ENDEMSH"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        series = CEnDemSH
                        weightSeries = CGDP
                    Case "INFRAROAD"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 500
                        series = CRoadDensGR
                        weightSeries = CLandArea

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("InfraRoadGR", Vals, cntx)

                        series = CRoadDens
                    Case "INFRAROADRAI"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = CRoadRAI
                        weightSeries = CLandArea
                    Case "INFRAROADPAVEDPCNT"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100
                        series = cRoadPavedPcntGR
                        weightSeries = CLandArea

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("InfraRoadPavedPcntGR", Vals, cntx)

                        series = cInfraRoadPavedPcnt
                    Case "LANDIRAREAEQUIP"
                        series = CLandIrAreaEquipGR
                        weightSeries = CLandArea

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("LandIrAreaEquipGR", Vals, cntx)

                        series = CLandIrAreaEquip
                    Case "landirareasat"
                        series = CLandIRAreaSat
                        weightSeries = CLandArea
                    Case "INFRATELE"
                        bLimits = True
                        MinVal = 0.05
                        MaxVal = 100
                        series = CTeleDensGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("InfraTeleGR", Vals, cntx)

                        series = CTeleDens
                    Case "INFRATELEOLD"
                        bLimits = True
                        MinVal = 0.05
                        MaxVal = 100
                        series = CTeleDens
                    Case "ICTMOBIL"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 200

                        series = CMobilGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("ICTMobilGR", Vals, cntx)

                        series = CMobil
                    Case "ICTBROAD"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100

                        series = CBroadbandGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("ICTBroadGR", Vals, cntx)

                        series = CBroadband
                    Case "ICTINDEX"
                        series = CHHSizeGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("HHSizeGR", Vals, cntx)

                        series = CICTIndex
                    Case "ICTCOMPUTERS"
                        series = CICTComputers
                    Case "ICTINTNETHH"
                        series = CICTINTNetHH
                    Case "ICTSPEND"
                        series = CSpend
                    Case "INFRAINVESTNEW_RoadPaved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000

                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(RoadPaved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_RoadUnPaved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000

                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(RoadUnpaved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_ElectricityGen"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(ElectricityGen))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_ElectricityAccRural"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(ElectricityAccRural))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_ElectricityAccUrban"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(ElectricityAccUrban))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_Irrigation"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(Irrigation))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_SafeWaterHH"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(SafeWaterHH))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_SafeWaterImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(SafeWaterImproved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_SanitationHH"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(SanitationHH))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_SanitationImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(SanitationImproved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_WasteWater"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(WasteWater))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_Telephone"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(Telephone))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_Mobile"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(Mobile))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_Broadband"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(Broadband))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTNEW_BroadbandMobile"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTNEW(key)(BroadbandMobile))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_RoadPaved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(RoadPaved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_RoadUnpaved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(RoadUnpaved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_ElectricityGen"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(ElectricityGen))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_ElectricityAccRural"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(ElectricityAccRural))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_ElectricityAccUrban"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(ElectricityAccUrban))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_Irrigation"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(Irrigation))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_SafeWaterHH"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(SafeWaterHH))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_SafeWaterImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(SafeWaterImproved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_SanitationHH"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(SanitationHH))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_SanitationImproved"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(SanitationImproved))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_WasteWater"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(WasteWater))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_Telephone"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(Telephone))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_Mobile"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(Mobile))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_Broadband"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(Broadband))
                        Next
                        weightSeries = CGDP
                    Case "INFRAINVESTMAINT_BroadbandMobile"
                        bLimits = True
                        MinVal = 0
                        MaxVal = 100000000000000
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, cINFRAINVESTMAINT(key)(BroadbandMobile))
                        Next
                        weightSeries = CGDP
                    Case "INFRAROADUNPAVEDCOST"
                        series = cINFRAROADUNPAVEDCOST
                        weightSeries = CGDP
                    Case "INFRAROADPAVEDCOST"
                        series = cINFRAROADPAVEDCOST
                        weightSeries = CGDP
                    Case "INFRAELECGENCOST"
                        series = cINFRAELECGENCOST
                        weightSeries = CGDP
                    Case "INFRAELECACCRURALCOST"
                        series = cINFRAELECACCRURALCOST
                        weightSeries = CGDP
                    Case "INFRAELECACCURBANCOST"
                        series = cINFRAELECACCURBANCOST
                        weightSeries = CGDP
                    Case "SANITATIONIMPCOST"
                        series = cSANITATIONIMPCOST
                        weightSeries = CGDP
                    Case "WATSAFEIMPCOST"
                        series = cWATSAFEIMPCOST
                        weightSeries = CGDP
                    Case "LANDIRCOST"
                        series = cLANDIRCOST
                        weightSeries = CGDP
                    Case "WATWASTETREATCOST"
                        series = cWATWASTETREATCOST
                        weightSeries = CGDP
                    Case "INFRATELECOST"
                        series = cINFRATELECOST
                        weightSeries = CGDP
                    Case "ICTMOBILCOST"
                        series = cICTMOBILCOST
                        weightSeries = CGDP
                    Case "ICTBROADCOST"
                        series = cICTBROADCOST
                        weightSeries = CGDP
                    Case "SANITATIONCOST"
                        series = cSANITATIONCOST
                        weightSeries = CGDP
                    Case "WATSAFECOST"
                        series = cWATSAFECOST
                        weightSeries = CGDP
                    Case "ICTBROADMOBIL"
                        series = CBroadMobilGR

                        Vals = (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value
                By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
            ).ToDictionary(
            Function(grp) grp.RegionId,
            Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight)

                        BasePutAnnualGrowthOneDim("ICTBroadMobilGR", Vals, cntx)

                        series = CBroadMobil
                    Case "ICTBROADMOBILCOST"
                        series = cICTBROADMOBILCOST
                        weightSeries = CGDP
                    Case "INFRAINDTRAD"
                        series = CINFRAINDTRAD
                    Case "INFRAINDICT"
                        series = CINFRAINDICT
                    Case "INFRAINDTRAN"
                        series = CINFRAINDTRAN
                    Case "INFRAINDELEC"
                        series = CINFRAINDELEC
                    Case "INFRAINDWATSAN"
                        series = CINFRAINDWATSAN
                    Case "INFRAINDICTZ"
                        series = CINFRAINDICTZ
                    Case "ENSOLFUEL"
                        series = CENSOLFUEL
                    Case Else
                        'Throw New ArgumentException($"Aggregator cannot be applied on series {seriesName}.")
                        series = New Dictionary(Of String, Single?)
                        For Each key In cntx.Regionalization.Keys
                            series.Add(key, Nothing)
                        Next
                End Select

                If bLimits Then
                    If bSum Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.TotalVal, MinVal, MaxVal)
                           )
                    Else
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, SumProd = Sum(Value * Weight), TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.SumProd / grp.TotalWeight, MinVal, MaxVal)
                           )
                    End If
                Else
                    If bSum Then
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Group Value = s.Value By RegionId = r.Value Into Data = Group, TotalVal = Sum(Value)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.TotalVal
                           )
                    Else
                        Return (From s In series
                                Join r In cntx.Regionalization On s.Key Equals r.Key
                                Join w In weightSeries On s.Key Equals w.Key
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
                           )
                    End If
                End If

            End Function

            Private Sub ReadGlobalInfraParameters(cntx As IRebuildFacade)
                Using db As IFsContext = New IFsContext()

                    Dim gp As GlobalParameters
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAROADUNPAVEDLIFE").Single()
                    infraroadunpavedlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAROADUNPAVEDCOSTLOWER").Single()
                    infraroadunpavedcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAROADUNPAVEDCOSTUPPER").Single()
                    infraroadunpavedcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAROADPAVEDLIFE").Single()
                    infraroadpavedlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAROADPAVEDCOSTLOWER").Single()
                    infraroadpavedcostlower = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAROADPAVEDCOSTUPPER").Single()
                    infraroadpavedcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "LANDIRLIFE").Single()
                    landirlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "LANDIRCOSTLOWER").Single()
                    landircostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "LANDIRCOSTUPPER").Single()
                    landircostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATWASTETREATLIFE").Single()
                    watwastetreatlife = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATWASTETREATCOSTLOWER").Single()
                    watwastetreatcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATWASTETREATCOSTUPPER").Single()
                    watwastetreatcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECGENLIFE").Single()
                    infraelecgenlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECGENCOSTLOWER").Single()
                    infraelecgencostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECGENCOSTUPPER").Single()
                    infraelecgencostupper = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECACCRURALLIFE").Single()
                    infraelecaccrurallife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECACCRURALCOSTLOWER").Single()
                    infraelecaccruralcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECACCRURALCOSTUPPER").Single()
                    infraelecaccruralcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECACCURBANLIFE").Single()
                    infraelecaccurbanlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECACCURBANCOSTLOWER").Single()
                    infraelecaccurbancostlower = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRAELECACCURBANCOSTUPPER").Single()
                    infraelecaccurbancostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATSAFEIMPLIFE").Single()
                    watsafeimplife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATSAFEIMPCOSTLOWER").Single()
                    watsafeimpcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATSAFEIMPCOSTUPPER").Single()
                    watsafeimpcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "SANITATIONIMPLIFE").Single()
                    sanitationimplife = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "SANITATIONIMPCOSTLOWER").Single()
                    sanitationimpcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "SANITATIONIMPCOSTUPPER").Single()
                    sanitationimpcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "SANITATIONLIFE").Single()
                    sanitationlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "SANITATIONCOSTLOWER").Single()
                    sanitationcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "SANITATIONCOSTUPPER").Single()
                    sanitationcostupper = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATSAFELIFE").Single()
                    watsafelife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATSAFECOSTLOWER").Single()
                    watsafecostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "WATSAFECOSTUPPER").Single()
                    watsafecostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTBROADLIFE").Single()
                    ictbroadlife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTBROADCOSTLOWER").Single()
                    ictbroadcostlower = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTBROADCOSTUPPER").Single()
                    ictbroadcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTBROADMOBILLIFE").Single()
                    ictbroadmobillife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTBROADMOBILCOSTLOWER").Single()
                    ictbroadmobilcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTBROADMOBILCOSTUPPER").Single()
                    ictbroadmobilcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTMOBILLIFE").Single()
                    ictmobillife = CSng(gp.Value)

                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTMOBILCOSTLOWER").Single()
                    ictmobilcostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "ICTMOBILCOSTUPPER").Single()
                    ictmobilcostupper = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRATELELIFE").Single()
                    infratelelife = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRATELECOSTLOWER").Single()
                    infratelecostlower = CSng(gp.Value)
                    gp = db.GlobalParameters.Where(Function(g) g.ParameterName = "INFRATELECOSTUPPER").Single()
                    infratelecostupper = CSng(gp.Value)

                    'Parameters for Infrastructure Indices:  Component Weights
                    If cntx.HistoricMode Then
                        Dim gp2 As IEnumerable(Of GlobalParametersMultHist)

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDTRADCOMPWT").AsEnumerable()
                        ReDim infraindtradcompwt(gp2.Count)
                        For Each par As GlobalParametersMultHist In gp2
                            infraindtradcompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDICTCOMPWT").AsEnumerable()
                        ReDim infraindictcompwt(gp2.Count)
                        For Each par As GlobalParametersMultHist In gp2
                            infraindictcompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDICTZCOMPWT").AsEnumerable()
                        ReDim infraindictzcompwt(gp2.Count)
                        For Each par As GlobalParametersMultHist In gp2
                            infraindictzcompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDTRANCOMPWT").AsEnumerable()
                        ReDim infraindtrancompwt(gp2.Count)
                        For Each par As GlobalParametersMultHist In gp2
                            infraindtrancompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDELECCOMPWT").AsEnumerable()
                        ReDim infraindeleccompwt(gp2.Count)
                        For Each par As GlobalParametersMultHist In gp2
                            infraindeleccompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDWATSANCOMPWT").AsEnumerable()
                        ReDim infraindwatsancompwt(gp2.Count)
                        For Each par As GlobalParametersMultHist In gp2
                            infraindwatsancompwt(par.Dimension) = CSng(par.Value)
                        Next

                    Else
                        Dim gp2 As IEnumerable(Of GlobalParametersMult)

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDTRADCOMPWT").AsEnumerable()
                        ReDim infraindtradcompwt(gp2.Count)
                        For Each par As GlobalParametersMult In gp2
                            infraindtradcompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDICTCOMPWT").AsEnumerable()
                        ReDim infraindictcompwt(gp2.Count)
                        For Each par As GlobalParametersMult In gp2
                            infraindictcompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDICTZCOMPWT").AsEnumerable()
                        ReDim infraindictzcompwt(gp2.Count)
                        For Each par As GlobalParametersMult In gp2
                            infraindictzcompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDTRANCOMPWT").AsEnumerable()
                        ReDim infraindtrancompwt(gp2.Count)
                        For Each par As GlobalParametersMult In gp2
                            infraindtrancompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDELECCOMPWT").AsEnumerable()
                        ReDim infraindeleccompwt(gp2.Count)
                        For Each par As GlobalParametersMult In gp2
                            infraindeleccompwt(par.Dimension) = CSng(par.Value)
                        Next

						'gvdb 2024/04/05
            			'update for new EF syntax
                        gp2 = db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDWATSANCOMPWT").AsEnumerable()
                        ReDim infraindwatsancompwt(gp2.Count)
                        For Each par As GlobalParametersMult In gp2
                            infraindwatsancompwt(par.Dimension) = CSng(par.Value)
                        Next
                    End If
                End Using

            End Sub

            Private Sub CalcInfraUnitCost(Drvr_UpVal As Single?, Drvr_LowVal As Single?, UCost_UpVal As Single?, UCost_LowVal As Single?, Drvr_CurVal As Single?, ByRef UCost_CurVal As Single?, Optional Increasing As Boolean = True)
                'mti Infrastructure Cost August 2010 - new procedure
                'generic procedure for calculating unit cost - starts with a lower value
                'and merges linearly to a upper value as the driver (e.g., GDPPCP) merges to a upper value

                If Increasing Then
                    If Drvr_CurVal <= Drvr_LowVal Then 'lower value of driver/GDPPPCP for example
                        UCost_CurVal = UCost_LowVal
                    Else  '5000 to 10000 - linear path
                        UCost_CurVal = UCost_LowVal + (UCost_UpVal - UCost_LowVal) * (Amin(Drvr_CurVal, Drvr_UpVal) - Drvr_LowVal) / (Drvr_UpVal - Drvr_LowVal)
                    End If
                Else 'decreasing cost
                    If Drvr_CurVal <= Drvr_LowVal Then 'lower value of driver/GDPPPCP for example
                        UCost_CurVal = UCost_UpVal
                    Else  '5000 to 10000 - linear path
                        UCost_CurVal = UCost_UpVal - (UCost_UpVal - UCost_LowVal) * (Amin(Drvr_CurVal, Drvr_UpVal) - Drvr_LowVal) / (Drvr_UpVal - Drvr_LowVal)
                    End If
                End If
            End Sub

            Private Sub EstimateClosestFromGroup(sCountry As String, GListName$, Dim2%, Dim3%, ByRef CVarVal As IDictionary(Of String, Single?)(,))

                Using dbIFs As IFs.Data.IFsContext = New IFsContext()
                    Dim rsGrpNo = (From GLI In dbIFs.GListID
                                   Join GL In dbIFs.GList On GLI.Code Equals GL.GListCode
                                   Join GI In dbIFs.GroupIDNewFull On GL.RegionName Equals GI.GroupName
                                   Join G In dbIFs.GroupingFull On GI.GroupNum Equals G.GroupId
                                   Join R In dbIFs.RegionalizationFull On G.RegionId Equals R.RegionId
                                   Where GLI.Description = GListName$ AndAlso R.CountryName = sCountry
                                   Select Key = R.CountryName, Value = G.GroupId).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )
                    Dim GrpNo = 0
                    For Each item In rsGrpNo
                        GrpNo = item.Value
                    Next

                    If GrpNo > 0 Then
                        Dim rsGrpCntry = (From G In dbIFs.GroupingFull
                                          Join R In dbIFs.RegionalizationFull On G.RegionId Equals R.RegionId
                                          Where G.GroupId = GrpNo
                                          Select Key = R.CountryName, Value = R.RegionId).ToDictionary(
                           Function(x) x.Key,
                           Function(x) x.Value
                           )

                        Dim Dist As Single = 9999999
                        For Each item In rsGrpCntry
                            Dim PeerCtry As String
                            PeerCtry = item.Key
                            Dim DistThis = Math.Abs(CSng(CGDPPCP(sCountry) - CGDPPCP(PeerCtry)))
                            If DistThis < Dist And CVarVal(Dim2, Dim3)(PeerCtry).HasValue And sCountry <> PeerCtry Then
                                Dist = DistThis
                                CVarVal(Dim2, Dim3)(sCountry) = CVarVal(Dim2, Dim3)(PeerCtry)
                            End If
                        Next
                    End If

                End Using

            End Sub

            Private Sub ComputeFromAtLeastBasic(sCountry As String, CAtLeastBasic As IDictionary(Of String, Single?)(), CWatSanHH As IDictionary(Of String, Single?)(,))
                '------------------------------------------------------------------
                'mti 2022/09/19 End  New Proc

                If CAtLeastBasic(Urban)(sCountry).HasValue Then
                    'Urban safely managed not available - compute from ALB - Urban and SM, ALB Ratio national
                    CWatSanHH(SafelyManaged, Urban)(sCountry) = AMAX(0, Amin(100, CAtLeastBasic(Urban)(sCountry) * CWatSanHH(SafelyManaged, National)(sCountry) / CAtLeastBasic(National)(sCountry)))
                    CWatSanHH(Basic, Urban)(sCountry) = AMAX(0, Amin(100, CAtLeastBasic(Urban)(sCountry) - CWatSanHH(SafelyManaged, Urban)(sCountry)))
                    CWatSanHH(SafelyManaged, Rural)(sCountry) = AMAX(0, Amin(100, (CWatSanHH(SafelyManaged, National)(sCountry) * CPOP(sCountry) -
                                                           CWatSanHH(SafelyManaged, Urban)(sCountry) * CPOPURBAN(sCountry)) / CPOPRURAL(sCountry)))
                    If CAtLeastBasic(Rural)(sCountry).HasValue Then CWatSanHH(Basic, Rural)(sCountry) = AMAX(0, Amin(100, CAtLeastBasic(Rural)(sCountry) - CWatSanHH(SafelyManaged, Rural)(sCountry)))

                ElseIf CAtLeastBasic(Rural)(sCountry).HasValue Then

                    CWatSanHH(SafelyManaged, Rural)(sCountry) = AMAX(0, Amin(100, CAtLeastBasic(Rural)(sCountry) * CWatSanHH(SafelyManaged, National)(sCountry) / CAtLeastBasic(National)(sCountry)))
                    CWatSanHH(Basic, Rural)(sCountry) = AMAX(0, Amin(100, CAtLeastBasic(Rural)(sCountry) - CWatSanHH(SafelyManaged, Rural)(sCountry)))
                    CWatSanHH(SafelyManaged, Urban)(sCountry) = AMAX(0, Amin(100, (CWatSanHH(SafelyManaged, National)(sCountry) * CPOP(sCountry) -
                                                           CWatSanHH(SafelyManaged, Rural)(sCountry) * CPOPRURAL(sCountry)) / CPOPURBAN(sCountry)))
                    If CAtLeastBasic(Urban)(sCountry).HasValue Then CWatSanHH(Basic, Urban)(sCountry) = AMAX(0, Amin(100, CAtLeastBasic(Urban)(sCountry) - CWatSanHH(SafelyManaged, Urban)(sCountry)))

                End If

            End Sub

        End Class

    End Module
End Namespace

