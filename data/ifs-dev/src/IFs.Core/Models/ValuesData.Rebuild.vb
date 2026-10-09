Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports IFs.Data

Namespace Models
    Partial Public Module ValuesData

        <RebuildsVariable("CULTREG", AggregatorType:=GetType(ValDatAggregator))>
        <RebuildsVariable("SURVSELD", AggregatorType:=GetType(ValDatAggregator))>
        <RebuildsVariable("TRADSRATLD", AggregatorType:=GetType(ValDatAggregator))>
        <RebuildsVariable("MATPOSTR", AggregatorType:=GetType(ValDatAggregator))>
        <RebuildsVariable("SURVSE", AggregatorType:=GetType(ValDatAggregator))>
        <RebuildsVariable("TRADSRAT", AggregatorType:=GetType(ValDatAggregator))>
        <RebuildsVariable("cultshmp")>
        <RebuildsVariable("cultshseload")>
        <RebuildsVariable("cultshse")>
        <RebuildsVariable("cultshtsload")>
        <RebuildsVariable("cultshts")>
        <RebuildsVariable("hivculf")>
        <RebuildsVariable("engel")>
        <RebuildsVariable("salpha")>
        <RebuildsVariable("MODYEAR")>
        <RebuildsVariable("MODYEARI")>
        <ImportsAsset("CGDPPCP")>
        <ImportsAsset("CGDPPC")>
        <ImportsAsset("CPOP")>
        Public Iterator Function RebuildValuesData(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Dim dfCULTREGtmp As IDataframe = context.Query("CULTREG").Filter(BaseYearOnly).Execute()
            Dim dfCULTREG As Dataframe = Dataframe.Create("CULTREG", context, context.FirstYear, context.LastYear, Nothing)
            For y = FirstYear To LastYear
                For R = 1 To NR
                    dfCULTREG(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfCULTREGtmp(getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR)))
                Next
            Next
            Yield dfCULTREG

            Dim dfSURVSELDtmp As IDataframe = context.Query("SURVSELD").Filter(BaseYearOnly).Execute()
            Dim dfSURVSELD As Dataframe = Dataframe.Create("SURVSELD", context, context.FirstYear, context.LastYear, Nothing)
            For y = FirstYear To LastYear
                For R = 1 To NR
                    dfSURVSELD(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfSURVSELDtmp(getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR)))
                Next
            Next
            Yield dfSURVSELD

            Dim dfTRADSRATLDtmp As IDataframe = context.Query("TRADSRATLD").Filter(BaseYearOnly).Execute()
            Dim dfTRADSRATLD As Dataframe = Dataframe.Create("TRADSRATLD", context, context.FirstYear, context.LastYear, Nothing)
            For y = FirstYear To LastYear
                For R = 1 To NR
                    dfTRADSRATLD(getDataFrameIdxFromDims(y, FirstYear, CShort(R), CShort(NR))) = dfTRADSRATLDtmp(getDataFrameIdxFromDims(FirstYear, FirstYear, CShort(R), CShort(NR)))
                Next
            Next
            Yield dfTRADSRATLD

            Yield context.Query("MATPOSTR").Filter(BaseYearOnly).Execute()
            Yield context.Query("SURVSE").Filter(BaseYearOnly).Execute()
            Yield context.Query("TRADSRAT").Filter(BaseYearOnly).Execute()

            Using db As IFs2Context = New IFs2Context()
                Dim dfcultshmp As Dataframe = Dataframe.Create("cultshmp", context, context.FirstYear, context.LastYear, Nothing)
                Dim dfcultshseload As Dataframe = Dataframe.Create("cultshseload", context, context.FirstYear, context.LastYear, Nothing)
                Dim dfcultshse As Dataframe = Dataframe.Create("cultshse", context, context.FirstYear, context.LastYear, Nothing)
                Dim dfcultshtsload As Dataframe = Dataframe.Create("cultshtsload", context, context.FirstYear, context.LastYear, Nothing)
                Dim dfcultshts As Dataframe = Dataframe.Create("cultshts", context, context.FirstYear, context.LastYear, Nothing)

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.CulturalParameters.Where(Function(g) g.ParameterName = "CULTSHMP").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 1, 11)) = item.Region1
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 2, 11)) = item.Region2
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 3, 11)) = item.Region3
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 4, 11)) = item.Region4
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 5, 11)) = item.Region5
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 6, 11)) = item.Region6
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 7, 11)) = item.Region7
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 8, 11)) = item.Region8
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 9, 11)) = item.Region9
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 10, 11)) = item.Region10
                        dfcultshmp(getDataFrameIdxFromDims(y, FirstYear, 11, 11)) = item.Region11
                    Next
                Next

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.CulturalParameters.Where(Function(g) g.ParameterName = "CULTSHSELOAD").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 1, 11)) = item.Region1
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 2, 11)) = item.Region2
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 3, 11)) = item.Region3
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 4, 11)) = item.Region4
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 5, 11)) = item.Region5
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 6, 11)) = item.Region6
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 7, 11)) = item.Region7
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 8, 11)) = item.Region8
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 9, 11)) = item.Region9
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 10, 11)) = item.Region10
                        dfcultshseload(getDataFrameIdxFromDims(y, FirstYear, 11, 11)) = item.Region11
                    Next
                Next

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.CulturalParameters.Where(Function(g) g.ParameterName = "CULTSHSE").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 1, 11)) = item.Region1
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 2, 11)) = item.Region2
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 3, 11)) = item.Region3
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 4, 11)) = item.Region4
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 5, 11)) = item.Region5
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 6, 11)) = item.Region6
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 7, 11)) = item.Region7
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 8, 11)) = item.Region8
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 9, 11)) = item.Region9
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 10, 11)) = item.Region10
                        dfcultshse(getDataFrameIdxFromDims(y, FirstYear, 11, 11)) = item.Region11
                    Next
                Next

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.CulturalParameters.Where(Function(g) g.ParameterName = "CULTSHTSLOAD").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 1, 11)) = item.Region1
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 2, 11)) = item.Region2
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 3, 11)) = item.Region3
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 4, 11)) = item.Region4
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 5, 11)) = item.Region5
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 6, 11)) = item.Region6
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 7, 11)) = item.Region7
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 8, 11)) = item.Region8
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 9, 11)) = item.Region9
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 10, 11)) = item.Region10
                        dfcultshtsload(getDataFrameIdxFromDims(y, FirstYear, 11, 11)) = item.Region11
                    Next
                Next

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.CulturalParameters.Where(Function(g) g.ParameterName = "CULTSHTS").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 1, 11)) = item.Region1
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 2, 11)) = item.Region2
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 3, 11)) = item.Region3
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 4, 11)) = item.Region4
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 5, 11)) = item.Region5
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 6, 11)) = item.Region6
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 7, 11)) = item.Region7
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 8, 11)) = item.Region8
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 9, 11)) = item.Region9
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 10, 11)) = item.Region10
                        dfcultshts(getDataFrameIdxFromDims(y, FirstYear, 11, 11)) = item.Region11
                    Next
                Next

                Yield dfcultshmp
                Yield dfcultshseload
                Yield dfcultshse
                Yield dfcultshtsload
                Yield dfcultshts
            End Using

            Using db As IFsContext = New IFsContext()
                Dim dfhivculf As Dataframe = Dataframe.Create("hivculf", context, context.FirstYear, context.LastYear, Nothing)
                Dim dfengel As Dataframe = Dataframe.Create("engel", context, context.FirstYear, context.LastYear, Nothing)
                Dim dfsalpha As Dataframe = Dataframe.Create("salpha", context, context.FirstYear, context.LastYear, Nothing)

                'jrs 2019/03/25 **************************************************************************
                'READ: This variables is never recomputed in this table, so the values in .mdb file need to be preserved in a different table
                'jrs 2022/01/31 For Each item In db.CulturalParameters2.Where(Function(g) g.ParameterName = "HIVCULF").AsEnumerable()
                For j = 1 To 11
                    For y = FirstYear To LastYear
                        dfhivculf(getDataFrameIdxFromDims(y, FirstYear, CShort(j), 11)) = hivculf(j)
                    Next
                Next
                'jrs 2022/01/31 Next

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.IOEngelAlphaCoefs.Where(Function(g) g.Coefficient = "Engel").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfengel(getDataFrameIdxFromDims(y, FirstYear, 1, 6)) = item.AGSector
                        dfengel(getDataFrameIdxFromDims(y, FirstYear, 2, 6)) = item.PESector
                        dfengel(getDataFrameIdxFromDims(y, FirstYear, 3, 6)) = item.RMSector
                        dfengel(getDataFrameIdxFromDims(y, FirstYear, 4, 6)) = item.MNSector
                        dfengel(getDataFrameIdxFromDims(y, FirstYear, 5, 6)) = item.SRSector
                        dfengel(getDataFrameIdxFromDims(y, FirstYear, 6, 6)) = item.ICSector
                    Next
                Next

                'gvdb 2024/04/05
            	'update for new EF syntax
                For Each item In db.IOEngelAlphaCoefs.Where(Function(g) g.Coefficient = "Alpha").AsEnumerable()
                    For y = FirstYear To LastYear
                        dfsalpha(getDataFrameIdxFromDims(y, FirstYear, 1, 6)) = item.AGSector
                        dfsalpha(getDataFrameIdxFromDims(y, FirstYear, 2, 6)) = item.PESector
                        dfsalpha(getDataFrameIdxFromDims(y, FirstYear, 3, 6)) = item.RMSector
                        dfsalpha(getDataFrameIdxFromDims(y, FirstYear, 4, 6)) = item.MNSector
                        dfsalpha(getDataFrameIdxFromDims(y, FirstYear, 5, 6)) = item.SRSector
                        dfsalpha(getDataFrameIdxFromDims(y, FirstYear, 6, 6)) = item.ICSector
                    Next
                Next

                Yield dfhivculf
                Yield dfengel
                Yield dfsalpha

                Dim dfMODYEAR As Dataframe = Dataframe.Create("MODYEAR", context, context.FirstYear, context.LastYear, context.LastYear)
                Dim dfMODYEARI As Dataframe = Dataframe.Create("MODYEARI", context, context.FirstYear, context.LastYear, context.FirstYear)
                Yield dfMODYEAR
                Yield dfMODYEARI
            End Using
        End Function

        <RebuildsVariable("etechadv")>
        <RebuildsVariable("gdsbudgetconv")>
        <RebuildsVariable("gdsbudgetprotec")>
        <RebuildsVariable("govriskweight")>
        <RebuildsVariable("hdiedslemaxun")>
        <RebuildsVariable("hdiedyrsmaxun")>
        <RebuildsVariable("hdigdppcmaxun")>
        <RebuildsVariable("hdigdppcminun")>
        <RebuildsVariable("hdilifemaxun")>
        <RebuildsVariable("hdilifeminun")>
        <RebuildsVariable("hllongvtystartage")>
        <RebuildsVariable("hlmorbtomortgthport")>
        <RebuildsVariable("hlsmokingceiling")>
        <RebuildsVariable("hlsmokingfloor")>
        <RebuildsVariable("ictcybevcost")>
        <RebuildsVariable("ictcybevprob")>
        <RebuildsVariable("infraindeleccompwt")>
        <RebuildsVariable("infraindictcompwt")>
        <RebuildsVariable("infraindictzcompwt")>
        <RebuildsVariable("infraindtradcompwt")>
        <RebuildsVariable("infraindtrancompwt")>
        <RebuildsVariable("infraindwatsancompwt")>
        <RebuildsVariable("infrainvmaintpubshrm")>
        <RebuildsVariable("infrainvnewpubshrm")>
        <RebuildsVariable("lke")>
        <RebuildsVariable("lks")>
        <RebuildsVariable("mfpleadr")>
        <RebuildsVariable("rdi")>
        <RebuildsVariable("rdinr")>
        <RebuildsVariable("techconv")>
        <RebuildsVariable("wpwghtpow")>
        <RebuildsVariable("xwbsectar")>
        Public Iterator Function RebuildParametersMult(context As IRebuildContext) As IEnumerable(Of IDataframe)
            Using db As IFsContext = New IFsContext()
                Dim dfETECHADV As Dataframe = Dataframe.Create("etechadv", context, context.FirstYear, context.LastYear, 0)
                Dim dfGDSBUDGETCONV As Dataframe = Dataframe.Create("gdsbudgetconv", context, context.FirstYear, context.LastYear, 0)
                Dim dfGDSBUDGETPROTEC As Dataframe = Dataframe.Create("gdsbudgetprotec", context, context.FirstYear, context.LastYear, 0)
                Dim dfgovriskweight As Dataframe = Dataframe.Create("govriskweight", context, context.FirstYear, context.LastYear, 0)
                Dim dfhdiedslemaxun As Dataframe = Dataframe.Create("hdiedslemaxun", context, context.FirstYear, context.LastYear, 0)
                Dim dfhdiedyrsmaxun As Dataframe = Dataframe.Create("hdiedyrsmaxun", context, context.FirstYear, context.LastYear, 0)
                Dim dfhdigdppcmaxun As Dataframe = Dataframe.Create("hdigdppcmaxun", context, context.FirstYear, context.LastYear, 0)
                Dim dfhdigdppcminun As Dataframe = Dataframe.Create("hdigdppcminun", context, context.FirstYear, context.LastYear, 0)
                Dim dfhdilifemaxun As Dataframe = Dataframe.Create("hdilifemaxun", context, context.FirstYear, context.LastYear, 0)
                Dim dfhdilifeminun As Dataframe = Dataframe.Create("hdilifeminun", context, context.FirstYear, context.LastYear, 0)
                Dim dfhllongvtystartage As Dataframe = Dataframe.Create("hllongvtystartage", context, context.FirstYear, context.LastYear, 0)
                Dim dfhlmorbtomortgthport As Dataframe = Dataframe.Create("hlmorbtomortgthport", context, context.FirstYear, context.LastYear, 0)
                Dim dfhlsmokingceiling As Dataframe = Dataframe.Create("hlsmokingceiling", context, context.FirstYear, context.LastYear, 0)
                Dim dfhlsmokingfloor As Dataframe = Dataframe.Create("hlsmokingfloor", context, context.FirstYear, context.LastYear, 0)
                Dim dfICTCYBEVCOST As Dataframe = Dataframe.Create("ictcybevcost", context, context.FirstYear, context.LastYear, 0)
                Dim dfICTCYBEVPROB As Dataframe = Dataframe.Create("ictcybevprob", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINDELECCOMPWT As Dataframe = Dataframe.Create("infraindeleccompwt", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINDICTCOMPWT As Dataframe = Dataframe.Create("infraindictcompwt", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINDICTZCOMPWT As Dataframe = Dataframe.Create("infraindictzcompwt", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINDTRADCOMPWT As Dataframe = Dataframe.Create("infraindtradcompwt", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINDTRANCOMPWT As Dataframe = Dataframe.Create("infraindtrancompwt", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINDWATSANCOMPWT As Dataframe = Dataframe.Create("infraindwatsancompwt", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINVMAINTPUBSHRM As Dataframe = Dataframe.Create("infrainvmaintpubshrm", context, context.FirstYear, context.LastYear, 0)
                Dim dfINFRAINVNEWPUBSHRM As Dataframe = Dataframe.Create("infrainvnewpubshrm", context, context.FirstYear, context.LastYear, 0)
                Dim dfLKE As Dataframe = Dataframe.Create("lke", context, context.FirstYear, context.LastYear, 0)
                Dim dfLKS As Dataframe = Dataframe.Create("lks", context, context.FirstYear, context.LastYear, 0)
                Dim dfMFPLEADR As Dataframe = Dataframe.Create("mfpleadr", context, context.FirstYear, context.LastYear, 0)
                Dim dfRDI As Dataframe = Dataframe.Create("rdi", context, context.FirstYear, context.LastYear, 0)
                Dim dfRDINR As Dataframe = Dataframe.Create("rdinr", context, context.FirstYear, context.LastYear, 0)
                Dim dfTECHCONV As Dataframe = Dataframe.Create("techconv", context, context.FirstYear, context.LastYear, 0)
                'Dim dfwatdemandelas As Dataframe = Dataframe.Create("watdemandelas", context, context.FirstYear, Nothing)
                Dim dfWPWGHTPOW As Dataframe = Dataframe.Create("wpwghtpow", context, context.FirstYear, context.LastYear, 0)
                Dim dfXWBSECTAR As Dataframe = Dataframe.Create("xwbsectar", context, context.FirstYear, context.LastYear, 0)
                'jrs 2020/06/17
                Dim dfMFPECONINTS As Dataframe = Dataframe.Create("mfpeconints", context, context.FirstYear, context.LastYear, 0)

                Dim limitetechadv As Integer
                limitetechadv = context.GetDimension(context.GetVariable("etechadv").Dimensions(2)).Buckets.Count
                Dim i As Integer
                Dim y, Dim1Size, Dim2Size As Short
                If context.HistoricMode Then
                    Dim2Size = CShort(limitetechadv)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "ETECHADV").AsEnumerable()
                        If item.Dimension2 <= limitetechadv Then
                            For i = 1 To NR
                                For y = context.FirstYear To context.LastYear
                                    dfETECHADV(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                                Next
                            Next
                        End If
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("gdsbudgetconv").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
            		For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "GDSBUDGETCONV").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfGDSBUDGETCONV(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("gdsbudgetprotec").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "GDSBUDGETPROTEC").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfGDSBUDGETPROTEC(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("govriskweight").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "govriskweight").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfgovriskweight(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdiedslemaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "HDIEDSLEMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdiedslemaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdiedyrsmaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "HDIEDYRSMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdiedyrsmaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdigdppcmaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "HDIGDPPCMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdigdppcmaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdigdppcminun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "HDIGDPPCMINUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdigdppcminun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdilifemaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "HDILIFEMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdilifemaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdilifeminun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "HDILIFEMINUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdilifeminun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hllongvtystartage").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "hllongvtystartage").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhllongvtystartage(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hlmorbtomortgthport").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "hlmorbtomortgthport").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhlmorbtomortgthport(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hlsmokingceiling").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "hlsmokingceiling").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhlsmokingceiling(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hlsmokingfloor").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "hlsmokingfloor").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhlsmokingfloor(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("ictcybevcost").Dimensions(1)).Buckets.Count)
                    Dim2Size = CShort(context.GetDimension(context.GetVariable("ictcybevcost").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "ICTCYBEVCOST").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfICTCYBEVCOST(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size, CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("ictcybevprob").Dimensions(1)).Buckets.Count)
                    Dim2Size = CShort(context.GetDimension(context.GetVariable("ictcybevprob").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "ICTCYBEVPROB").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfICTCYBEVPROB(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size, CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindeleccompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDELECCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDELECCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindictcompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDICTCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDICTCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindictzcompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDICTZCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDICTZCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindtradcompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDTRADCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDTRADCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindtrancompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDTRANCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDTRANCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindwatsancompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINDWATSANCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDWATSANCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("infrainvmaintpubshrm").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINVMAINTPUBSHRM").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfINFRAINVMAINTPUBSHRM(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("infrainvnewpubshrm").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "INFRAINVNEWPUBSHRM").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfINFRAINVNEWPUBSHRM(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("lke").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "LKE").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfLKE(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("lks").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "LKS").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfLKS(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("mfpleadr").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "MFPLEADR").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfMFPLEADR(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    'jrs 2020/06/17
                    'jrs 2022/01/17 Dim1Size = CShort(context.GetDimension(context.GetVariable("mfpeconints").Dimensions(1)).Buckets.Count)
                    Dim2Size = CShort(context.GetDimension(context.GetVariable("mfpeconints").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "mfpeconints").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                'jrs 2022/01/17
                                dfMFPECONINTS(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next


                    Dim1Size = CShort(context.GetDimension(context.GetVariable("rdi").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "RDI").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfRDI(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("rdinr").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "RDINR").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfRDINR(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("techconv").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "TECHCONV").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfTECHCONV(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    'For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "watdemandelas").AsEnumerable()
                    '    dfwatdemandelas(context.FirstYear, CShort(item.Dimension), CShort(item.Dimension)) = CSng(item.Value)
                    'Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("wpwghtpow").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "WPWGHTPOW").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfWPWGHTPOW(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("xwbsectar").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMultHist.Where(Function(g) g.ParameterName = "XWBSECTAR").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfXWBSECTAR(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next
                Else 'Forecast Mode
                    Dim2Size = CShort(limitetechadv)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "ETECHADV").AsEnumerable()
                        If item.Dimension2 <= limitetechadv Then
                            For i = 1 To NR
                                For y = context.FirstYear To context.LastYear
                                    dfETECHADV(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                                Next
                            Next
                        End If
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("gdsbudgetconv").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "GDSBUDGETCONV").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfGDSBUDGETCONV(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("gdsbudgetprotec").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "GDSBUDGETPROTEC").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfGDSBUDGETPROTEC(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("govriskweight").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "govriskweight").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfgovriskweight(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdiedslemaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "HDIEDSLEMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdiedslemaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdiedyrsmaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "HDIEDYRSMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdiedyrsmaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdigdppcmaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "HDIGDPPCMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdigdppcmaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdigdppcminun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "HDIGDPPCMINUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdigdppcminun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdilifemaxun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "HDILIFEMAXUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdilifemaxun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hdilifeminun").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "HDILIFEMINUN").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhdilifeminun(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hllongvtystartage").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "hllongvtystartage").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhllongvtystartage(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hlmorbtomortgthport").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "hlmorbtomortgthport").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhlmorbtomortgthport(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hlsmokingceiling").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "hlsmokingceiling").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhlsmokingceiling(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("hlsmokingfloor").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "hlsmokingfloor").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfhlsmokingfloor(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("ictcybevcost").Dimensions(1)).Buckets.Count)
                    Dim2Size = CShort(context.GetDimension(context.GetVariable("ictcybevcost").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "ICTCYBEVCOST").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfICTCYBEVCOST(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size, CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("ictcybevprob").Dimensions(1)).Buckets.Count)
                    Dim2Size = CShort(context.GetDimension(context.GetVariable("ictcybevprob").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "ICTCYBEVPROB").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfICTCYBEVPROB(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size, CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindeleccompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDELECCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDELECCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindictcompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDICTCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDICTCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindictzcompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDICTZCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDICTZCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindtradcompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDTRADCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDTRADCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindtrancompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDTRANCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDTRANCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("infraindwatsancompwt").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINDWATSANCOMPWT").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfINFRAINDWATSANCOMPWT(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("infrainvmaintpubshrm").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINVMAINTPUBSHRM").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfINFRAINVMAINTPUBSHRM(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim2Size = CShort(context.GetDimension(context.GetVariable("infrainvnewpubshrm").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "INFRAINVNEWPUBSHRM").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                dfINFRAINVNEWPUBSHRM(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("lke").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "LKE").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfLKE(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("lks").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "LKS").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfLKS(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("mfpleadr").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "MFPLEADR").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfMFPLEADR(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    'jrs 2020/06/17
                    'jrs 2022/01/17 Dim1Size = CShort(context.GetDimension(context.GetVariable("mfpeconints").Dimensions(1)).Buckets.Count)
                    Dim2Size = CShort(context.GetDimension(context.GetVariable("mfpeconints").Dimensions(2)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "mfpeconints").AsEnumerable()
                        For i = 1 To NR
                            For y = context.FirstYear To context.LastYear
                                'jrs 2022/01/17 dfMFPECONINTS(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                                dfMFPECONINTS(getDataFrameIdxFromDims(y, FirstYear, CShort(i), CShort(NR), CShort(item.Dimension2), Dim2Size)) = CSng(item.Value)
                            Next
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("rdi").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "RDI").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfRDI(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("rdinr").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "RDINR").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfRDINR(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("techconv").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "TECHCONV").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfTECHCONV(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    'For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "watdemandelas").AsEnumerable()
                    '    dfwatdemandelas(context.FirstYear, CShort(item.Dimension), CShort(item.Dimension)) = CSng(item.Value)
                    'Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("wpwghtpow").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "WPWGHTPOW").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfWPWGHTPOW(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next

                    Dim1Size = CShort(context.GetDimension(context.GetVariable("xwbsectar").Dimensions(1)).Buckets.Count)
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.GlobalParametersMult.Where(Function(g) g.ParameterName = "XWBSECTAR").AsEnumerable()
                        For y = context.FirstYear To context.LastYear
                            dfXWBSECTAR(getDataFrameIdxFromDims(y, FirstYear, CShort(item.Dimension), Dim1Size)) = CSng(item.Value)
                        Next
                    Next
                End If

                Yield dfETECHADV
                Yield dfGDSBUDGETCONV
                Yield dfGDSBUDGETPROTEC
                Yield dfgovriskweight
                Yield dfhdiedslemaxun
                Yield dfhdiedyrsmaxun
                Yield dfhdigdppcmaxun
                Yield dfhdigdppcminun
                Yield dfhdilifemaxun
                Yield dfhdilifeminun
                Yield dfhllongvtystartage
                Yield dfhlmorbtomortgthport
                Yield dfhlsmokingceiling
                Yield dfhlsmokingfloor
                Yield dfICTCYBEVCOST
                Yield dfICTCYBEVPROB
                Yield dfINFRAINDELECCOMPWT
                Yield dfINFRAINDICTCOMPWT
                Yield dfINFRAINDICTZCOMPWT
                Yield dfINFRAINDTRADCOMPWT
                Yield dfINFRAINDTRANCOMPWT
                Yield dfINFRAINDWATSANCOMPWT
                Yield dfINFRAINVMAINTPUBSHRM
                Yield dfINFRAINVNEWPUBSHRM
                Yield dfLKE
                Yield dfLKS
                Yield dfMFPLEADR
                Yield dfRDI
                Yield dfRDINR
                Yield dfTECHCONV
                'Yield dfwatdemandelas
                Yield dfWPWGHTPOW
                Yield dfXWBSECTAR
                'jrs 2020/06/19
                Yield dfMFPECONINTS
            End Using
        End Function

        Class ValDatAggregator
            Inherits DefaultSumAggregator

            Private Property CGDPPCP As IDictionary(Of String, Single?)
            Private Property CGDPPC As IDictionary(Of String, Single?)
            Private Property CPOP As IDictionary(Of String, Single?)

            Dim Culcultshseload(12) As Single
            Dim Culcultshtsload(12) As Single

            Private Property CActVeryHappy As IDictionary(Of String, Single?)
            Private Property CActTrustPeople As IDictionary(Of String, Single?)
            Private Property CActRespectAuth As IDictionary(Of String, Single?)
            Private Property CActSignPetit As IDictionary(Of String, Single?)
            Private Property CActGodImport As IDictionary(Of String, Single?)
            Private Property CActHomoNevJus As IDictionary(Of String, Single?)
            Private Property CActAbortNevJus As IDictionary(Of String, Single?)
            Private Property CActProudNat As IDictionary(Of String, Single?)
            Private Property CActPostMinusMat As IDictionary(Of String, Single?)
            Private Property CActHighAut As IDictionary(Of String, Single?)

            Private Property CCultReg As IDictionary(Of String, Single?)
            Private Property CCultRegData As IDictionary(Of String, Single?)

            Dim CultRegCNonNull(12) As Integer

            Private Property CMPRCo1 As IDictionary(Of String, Single?)
            Private Property CMPRCo2 As IDictionary(Of String, Single?)
            Private Property CMPRCo3 As IDictionary(Of String, Single?)
            Private Property CMPRCo4 As IDictionary(Of String, Single?)
            Private Property CMPRCo5 As IDictionary(Of String, Single?)
            Private Property CMPRCo6 As IDictionary(Of String, Single?)
            Private Property CMPRCoTotal As IDictionary(Of String, Single?)

            Dim cultshmp(12) As Single

            Private Property SURVSECo1 As IDictionary(Of String, Single?)
            Private Property SURVSECo2 As IDictionary(Of String, Single?)
            Private Property SURVSECo3 As IDictionary(Of String, Single?)
            Private Property SURVSECo4 As IDictionary(Of String, Single?)
            Private Property SURVSECo5 As IDictionary(Of String, Single?)
            Private Property SURVSECo6 As IDictionary(Of String, Single?)
            Private Property SURVSECoTotal As IDictionary(Of String, Single?)

            Private Property CSurvSELd As IDictionary(Of String, Single?)
            Private Property ActSurvSELd As IDictionary(Of String, Single?)
            Private Property CPredictedSS As IDictionary(Of String, Single?)
            Private Property CCulPredictedSS As IDictionary(Of String, Single?)

            Dim cultshse(12) As Single
            Dim cultshseload(12) As Single

            Private Property TradSecRCo1 As IDictionary(Of String, Single?)
            Private Property TradSecRCo2 As IDictionary(Of String, Single?)
            Private Property TradSecRCo3 As IDictionary(Of String, Single?)
            Private Property TradSecRCo4 As IDictionary(Of String, Single?)
            Private Property TradSecRCo5 As IDictionary(Of String, Single?)
            Private Property TradSecRCo6 As IDictionary(Of String, Single?)
            Private Property TradSecRCoTotal As IDictionary(Of String, Single?)

            Private Property CTradSRatLd As IDictionary(Of String, Single?)
            Private Property ActTradSRatLd As IDictionary(Of String, Single?)
            Private Property CPredictedTS As IDictionary(Of String, Single?)
            Private Property CCulPredictedTS As IDictionary(Of String, Single?)

            Dim cultshts(12) As Single
            Dim cultshtsload(12) As Single

            Private Property ComSurvSELd As IDictionary(Of String, Single?)
            Private Property ComCulSurvSELd As IDictionary(Of String, Single?)
            Private Property ComTradSRatLd As IDictionary(Of String, Single?)
            Private Property ComCulTradSRatLd As IDictionary(Of String, Single?)

            Private Property processed As Boolean

            Private Sub PreProcess(cntx As IRebuildFacade)
                If Me.processed Then
                    Return
                End If

                Dim objWriter = New System.IO.StreamWriter(cntx.PutFdrvRun + "progress.txt", append:=True)
                objWriter.WriteLine("Processing Data on Values Data - Please Wait")
                objWriter.Close()

                CActVeryHappy = New Dictionary(Of String, Single?)
                CActTrustPeople = New Dictionary(Of String, Single?)
                CActRespectAuth = New Dictionary(Of String, Single?)
                CActSignPetit = New Dictionary(Of String, Single?)
                CActGodImport = New Dictionary(Of String, Single?)
                CActHomoNevJus = New Dictionary(Of String, Single?)
                CActAbortNevJus = New Dictionary(Of String, Single?)
                CActProudNat = New Dictionary(Of String, Single?)
                CActPostMinusMat = New Dictionary(Of String, Single?)
                CActHighAut = New Dictionary(Of String, Single?)

                CCultReg = New Dictionary(Of String, Single?)
                CCultRegData = New Dictionary(Of String, Single?)
                CMPRCo1 = New Dictionary(Of String, Single?)
                CMPRCo2 = New Dictionary(Of String, Single?)
                CMPRCo3 = New Dictionary(Of String, Single?)
                CMPRCo4 = New Dictionary(Of String, Single?)
                CMPRCo5 = New Dictionary(Of String, Single?)
                CMPRCo6 = New Dictionary(Of String, Single?)
                CMPRCoTotal = New Dictionary(Of String, Single?)

                CGDPPCP = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPCP")
                CGDPPC = cntx.Fetch(Of IDictionary(Of String, Single?))("CGDPPC")
                CPOP = cntx.Fetch(Of IDictionary(Of String, Single?))("CPOP")

                SURVSECo1 = New Dictionary(Of String, Single?)
                SURVSECo2 = New Dictionary(Of String, Single?)
                SURVSECo3 = New Dictionary(Of String, Single?)
                SURVSECo4 = New Dictionary(Of String, Single?)
                SURVSECo5 = New Dictionary(Of String, Single?)
                SURVSECo6 = New Dictionary(Of String, Single?)
                SURVSECoTotal = New Dictionary(Of String, Single?)

                CSurvSELd = New Dictionary(Of String, Single?)
                ActSurvSELd = New Dictionary(Of String, Single?)
                CPredictedSS = New Dictionary(Of String, Single?)
                CCulPredictedSS = New Dictionary(Of String, Single?)

                TradSecRCo1 = New Dictionary(Of String, Single?)
                TradSecRCo2 = New Dictionary(Of String, Single?)
                TradSecRCo3 = New Dictionary(Of String, Single?)
                TradSecRCo4 = New Dictionary(Of String, Single?)
                TradSecRCo5 = New Dictionary(Of String, Single?)
                TradSecRCo6 = New Dictionary(Of String, Single?)
                TradSecRCoTotal = New Dictionary(Of String, Single?)

                CTradSRatLd = New Dictionary(Of String, Single?)
                ActTradSRatLd = New Dictionary(Of String, Single?)
                CPredictedTS = New Dictionary(Of String, Single?)
                CCulPredictedTS = New Dictionary(Of String, Single?)

                ComSurvSELd = New Dictionary(Of String, Single?)
                ComCulSurvSELd = New Dictionary(Of String, Single?)
                ComTradSRatLd = New Dictionary(Of String, Single?)
                ComCulTradSRatLd = New Dictionary(Of String, Single?)

                Dim i As Integer

                '   Cultural Region - read and keep count
                '   -------------------------------------
                Using db As IFsContext = New IFsContext()
                    'Cultural regions specified
                    If cntx.HistoricMode Then
                        For Each item In db.SocialHist
                            CCultReg.Add(item.Country, item.CultReg)
                            If Not item.CultReg.HasValue Then
                                CCultReg(item.Country) = cntx.NumbCulReg 'Assign "Other"
                            ElseIf item.CultReg = 0 Then
                                CCultReg(item.Country) = cntx.NumbCulReg 'Assign "Other"
                            End If
                        Next
                    Else
                        For Each item In db.CountryParameters
                            CCultReg.Add(item.Country, item.CultReg)
                            If Not item.CultReg.HasValue Then
                                CCultReg(item.Country) = cntx.NumbCulReg 'Assign "Other"
                            ElseIf item.CultReg = 0 Then
                                CCultReg(item.Country) = cntx.NumbCulReg 'Assign "Other"
                            End If
                        Next
                    End If
                End Using

                Using db As IFsWVSCohortContext = New IFsWVSCohortContext()
                    '   Actual WVS Value Question Responses
                    '   -----------------------------------
                    For Each item In db.HappyPercentW3
                        CActVeryHappy.Add(item.Country, item.Total)
                    Next

                    For Each item In db.TrustPeoplePercentW3
                        CActTrustPeople.Add(item.Country, item.Total)
                    Next

                    For Each item In db.RespectAuthPercentW3
                        CActRespectAuth.Add(item.Country, item.Total)
                    Next

                    For Each item In db.SignedPetitPercentW3
                        CActSignPetit.Add(item.Country, item.Total)
                    Next

                    For Each item In db.GodImprtPercentW3
                        CActGodImport.Add(item.Country, item.Total)
                    Next

                    For Each item In db.HomoJustifPercentLatest
                        CActHomoNevJus.Add(item.Country, item.Total)
                    Next

                    For Each item In db.AbortJustifPercentW3
                        CActAbortNevJus.Add(item.Country, item.Total)
                    Next

                    For Each item In db.NationProudPercentW3
                        CActProudNat.Add(item.Country, item.Total)
                    Next

                    For Each item In db.PMMinMatPercentW3
                        CActPostMinusMat.Add(item.Country, item.Total)
                    Next

                    For Each item In db.AutonPercentW3
                        CActHighAut.Add(item.Country, item.Total)
                    Next

                    Dim loadtemp As Single?
                    For Each item In db.SurvSelfExpLoadsLatestW3
                        loadtemp = item.Total
                        CCultRegData.Add(item.Country, Nothing)
                        If loadtemp.HasValue Then
                            CultRegCNonNull(CInt(CCultReg(item.Country))) = CultRegCNonNull(CInt(CCultReg(item.Country))) + 1
                            CCultRegData(item.Country) = CCultReg(item.Country) 'Keep track only of countries with data
                        Else
                            CCultRegData(item.Country) = cntx.NumbCulReg 'Assign "Other" if no data for a country
                        End If
                    Next

                End Using

                Debug.Print("After: Actual WVS Value Question Responses")

                '       Read and Process WVS - Materialism/Postmaterialism
                '       --------------------------------------------------
                Dim XYTBL As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 1").ToEstimator(CGDPPCP)
                Dim XYTBL2 As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 2").ToEstimator(CGDPPCP)
                Dim XYTBL3 As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 3").ToEstimator(CGDPPCP)
                Dim XYTBL4 As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 4").ToEstimator(CGDPPCP)
                Dim XYTBL5 As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 5").ToEstimator(CGDPPCP)
                Dim XYTBL6 As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Cohort 6").ToEstimator(CGDPPCP)
                Dim XYTBLT As SeriesEstimator = cntx.GetRegression("GDP/Capita (PPP) Versus Materialism/Postmaterialism Total").ToEstimator(CGDPPCP)

                Debug.Print("After: Read and Process WVS - Materialism/Postmaterialism, loading Estimators")

                Using db As IFsContext = New IFsContext()
                    For Each item In db.WVSCohortWave2
                        CMPRCo1.Add(item.Country, item.MPRCo1)
                        'Assume missing data
                        If Not item.MPRCo1.HasValue Then
                            CMPRCo1(item.Country) = ApplyLimits(XYTBL(item.Country), 1, 5)
                        ElseIf CMPRCo1(item.Country) < 0.01 Then
                            CMPRCo1(item.Country) = ApplyLimits(XYTBL(item.Country), 1, 5)
                        End If

                        CMPRCo2.Add(item.Country, item.MPRCo2)
                        'Assume missing data
                        If Not item.MPRCo2.HasValue Then
                            CMPRCo2(item.Country) = ApplyLimits(XYTBL2(item.Country), 1, 5)
                        ElseIf CMPRCo2(item.Country) < 0.01 Then
                            CMPRCo2(item.Country) = ApplyLimits(XYTBL2(item.Country), 1, 5)
                        End If

                        CMPRCo3.Add(item.Country, item.MPRCo3)
                        'Assume missing data
                        If Not item.MPRCo3.HasValue Then
                            CMPRCo3(item.Country) = ApplyLimits(XYTBL3(item.Country), 1, 5)
                        ElseIf CMPRCo3(item.Country) < 0.01 Then
                            CMPRCo3(item.Country) = ApplyLimits(XYTBL3(item.Country), 1, 5)
                        End If

                        'Debug.Print("After: Read and Process WVS - Materialism/Postmaterialism, loading CMPRCo3 " & item.Country & " " & CMPRCo3.Count.ToString)

                        CMPRCo4.Add(item.Country, item.MPRCo4)
                        'Assume missing data
                        If Not item.MPRCo4.HasValue Then
                            CMPRCo4(item.Country) = ApplyLimits(XYTBL4(item.Country), 1, 5)
                        ElseIf CMPRCo4(item.Country) < 0.01 Then
                            CMPRCo4(item.Country) = ApplyLimits(XYTBL4(item.Country), 1, 5)
                        End If

                        CMPRCo5.Add(item.Country, item.MPRCo5)
                        'Assume missing data
                        If Not item.MPRCo5.HasValue Then
                            CMPRCo5(item.Country) = ApplyLimits(XYTBL5(item.Country), 1, 5)
                        ElseIf CMPRCo5(item.Country) < 0.01 Then
                            CMPRCo5(item.Country) = ApplyLimits(XYTBL5(item.Country), 1, 5)
                        End If

                        CMPRCo6.Add(item.Country, item.MPRCo6)
                        'Assume missing data
                        If Not item.MPRCo6.HasValue Then
                            CMPRCo6(item.Country) = ApplyLimits(XYTBL6(item.Country), 1, 5)
                        ElseIf CMPRCo6(item.Country) < 0.01 Then
                            CMPRCo6(item.Country) = ApplyLimits(XYTBL6(item.Country), 1, 5)
                        End If

                        CMPRCoTotal.Add(item.Country, item.MPRTotal)
                        'Assume missing data
                        If Not item.MPRTotal.HasValue Then
                            CMPRCoTotal(item.Country) = ApplyLimits(XYTBLT(item.Country), 1, 5)
                        ElseIf CMPRCoTotal(item.Country) < 0.01 Then
                            CMPRCoTotal(item.Country) = ApplyLimits(XYTBLT(item.Country), 1, 5)
                        End If
                    Next
                End Using

                Debug.Print("After: Read and Process WVS - Materialism/Postmaterialism")

                '       Keep track of deviations from predicted for cultural regions - Mat/PostMat
                '       --------------------------------------------------------------------------
                Dim predicted As Single?
                Dim count As Integer = 0
                For Each key In cntx.Regionalization.Keys
                    count = count + 1
                    'Debug.Print(count.ToString & " " & key & " " & CCultRegData(key).ToString & " " & cntx.NumbCulReg)
                    'If key = "United Arab Emirates" Then Stop
                    If CCultRegData(key) > 0 And CCultRegData(key) <= cntx.NumbCulReg - 1 Then
                        predicted = XYTBL(key)
                        predicted = AMAX(1, Amin(predicted, 5))
                        cultshmp(CInt(CCultRegData(key))) = cultshmp(CInt(CCultRegData(key))) + CSng(CMPRCo1(key) - predicted)
                    End If
                Next

                Debug.Print("After: Keep track of deviations from predicted for cultural regions - Mat/PostMat")

                '       Read and Process WVS - Survival/Self-Expression
                '       ------------------------------------------
                'jrs 2025/05/08
                Dim TFNameSurvSelfExpCoh1 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 1 Log"
                TFNameSurvSelfExpCoh1 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh1, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameSurvSelfExpCoh2 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 2 Log"
                TFNameSurvSelfExpCoh2 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh2, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameSurvSelfExpCoh3 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 3 Log"
                TFNameSurvSelfExpCoh3 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh3, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameSurvSelfExpCoh4 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 4 Log"
                TFNameSurvSelfExpCoh4 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh4, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameSurvSelfExpCoh5 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 5 Log"
                TFNameSurvSelfExpCoh5 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh5, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameSurvSelfExpCoh6 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Survival/Self-Expression Cohort 6 Log"
                TFNameSurvSelfExpCoh6 = getFunctionNameYrOrMstRct(TFNameSurvSelfExpCoh6, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'jrs 2025/05/12
                Dim TFNameTradSecRatCoh1 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 1 Log"
                TFNameTradSecRatCoh1 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh1, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameTradSecRatCoh2 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 2 Log"
                TFNameTradSecRatCoh2 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh2, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameTradSecRatCoh3 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 3 Log"
                TFNameTradSecRatCoh3 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh3, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameTradSecRatCoh4 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 4 Log"
                TFNameTradSecRatCoh4 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh4, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameTradSecRatCoh5 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 5 Log"
                TFNameTradSecRatCoh5 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh5, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                Dim TFNameTradSecRatCoh6 = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Cohort 6 Log"
                TFNameTradSecRatCoh6 = getFunctionNameYrOrMstRct(TFNameTradSecRatCoh6, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")
                'jrs 2025/05/15
                Dim TFNameTradSecRatTotal = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Traditional/Secular-Rational Total Log"
                TFNameTradSecRatTotal = getFunctionNameYrOrMstRct(TFNameTradSecRatTotal, BaseYear, cntx, "(PPP " & ConstantDollarsYear & ")")

                Using db As IFsWVSCohortContext = New IFsWVSCohortContext()
                    'jrs 2025/05/08 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 1").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh1).ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Cohort1.HasValue Then
                            SURVSECo1.Add(item.Country, item.Cohort1)
                        Else
                            SURVSECo1.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Cohort1)
                        End If
                        If Not SURVSECo1(item.Country).HasValue Then
                            SURVSECo1(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/08 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 2").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh2).ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Cohort2.HasValue Then
                            SURVSECo2.Add(item.Country, item.Cohort2)
                        Else
                            SURVSECo2.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Cohort2)
                        End If
                        If Not SURVSECo2(item.Country).HasValue Then
                            SURVSECo2(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/08 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 3").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh3).ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Cohort3.HasValue Then
                            SURVSECo3.Add(item.Country, item.Cohort3)
                        Else
                            SURVSECo3.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Cohort3)
                        End If
                        If Not SURVSECo3(item.Country).HasValue Then
                            SURVSECo3(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/08 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 4").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh4).ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Cohort4.HasValue Then
                            SURVSECo4.Add(item.Country, item.Cohort4)
                        Else
                            SURVSECo4.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Cohort4)
                        End If
                        If Not SURVSECo4(item.Country).HasValue Then
                            SURVSECo4(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/08 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 5").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh5).ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Cohort5.HasValue Then
                            SURVSECo5.Add(item.Country, item.Cohort5)
                        Else
                            SURVSECo5.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Cohort5)
                        End If
                        If Not SURVSECo5(item.Country).HasValue Then
                            SURVSECo5(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/08 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 6").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh6).ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Cohort6.HasValue Then
                            SURVSECo6.Add(item.Country, item.Cohort6)
                        Else
                            SURVSECo6.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Cohort6)
                        End If
                        If Not SURVSECo6(item.Country).HasValue Then
                            SURVSECo6(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Survival/Self-Expression Total").ToEstimator(CGDPPCP)
                    For Each item In db.SurvSelfExpW4
                        If item.Total.HasValue Then
                            SURVSECoTotal.Add(item.Country, item.Total)
                        Else
                            SURVSECoTotal.Add(item.Country, db.SurvSelfExpW3.Where(Function(g) g.Country = item.Country).Single().Total)
                        End If
                        If Not SURVSECoTotal(item.Country).HasValue Then
                            SURVSECoTotal(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    '       Read data to predict factor loading for survival/self-expression
                    '       ----------------------------------------------------------------
                    For Each item In db.SurvSelfExpLoadsLatestW3
                        CSurvSELd.Add(item.Country, item.Total) 'Factor loadings
                        ActSurvSELd.Add(item.Country, item.Total) 'Actual Factor loadings
                        If Not ActSurvSELd(item.Country).HasValue Then ActSurvSELd(item.Country) = 0
                    Next
                End Using

                'Using db As IFsContext = New IFsContext()
                '    Dim LaborIndPer As Single?, laborserper As Single?, culexcomdum As Single?, higdpr As Single?
                '    For Each item In (From s In cntx.GetSlice("SeriesLaborSecIndTot", 1990)
                '                      Join s2 In cntx.GetSlice("SeriesLaborSecSerTot", 1990) On s.Key Equals s2.Key
                '                      Join s3 In cntx.GetSlice("SeriesCulExComDum ", 1990) On s.Key Equals s3.Key
                '                      Select Key = s.Key, Value = s.Value, Val2 = s2.Value, Val3 = s3.Value)
                '        LaborIndPer = item.Value
                '        If Not LaborIndPer.HasValue Then LaborIndPer = 25
                '        laborserper = item.Val2
                '        If Not laborserper.HasValue Then laborserper = 50
                '        culexcomdum = item.Val3
                '        If Not culexcomdum.HasValue Then culexcomdum = 0
                '        higdpr = db.Economic.Where(Function(g) g.Country = item.Key).Single().IGDPR
                '        If Not higdpr.HasValue Then higdpr = 3 'percentage in Economic table; ratio in EcoOutput
                '    Next
                'End Using

                '       Predict factor loading for survival/self-expression
                '       ---------------------------------------------------
                Dim Title As String
                'jrs 2025/05/08 Title$ = "GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 1"
                'jrs 2025/05/08 XYTBL = cntx.GetRegression(Title).ToEstimator(CGDPPCP)  'jrs 2020/02/21 changed from GDPPC to GDPPCP
                XYTBL = cntx.GetRegression(TFNameSurvSelfExpCoh1).ToEstimator(CGDPPCP)  'jrs 2020/02/21 changed from GDPPC to GDPPCP

                Dim TempSurvSELoad As Single?, CulTempSurvSELoad As Single?

                For Each key In cntx.Regionalization.Keys
                    CPredictedSS.Add(key, ApplyLimits(XYTBL(key), -2, 2))
                    CCulPredictedSS.Add(key, -0.058067918) 'Predicted only by mean

                    If Not CSurvSELd(key).HasValue Then
                        'missing data from WVS
                        TempSurvSELoad = CPredictedSS(key)
                        CulTempSurvSELoad = CCulPredictedSS(key)
                    Else
                        ' bbh 2019/01/26 start
                        cultshse(CInt(CCultRegData(key))) = cultshse(CInt(CCultRegData(key))) + CSng(SURVSECo1(key) - CPredictedSS(key))
                        ' bbh 2019/01/26 end
                        TempSurvSELoad = CSurvSELd(key)
                        CulTempSurvSELoad = CSurvSELd(key)
                    End If

                    '       Keep track of deviations from predicted for cultural regions - Surv/Self-Expression
                    '       -----------------------------------------------------------------------------------
                    If CCultRegData(key) > 0 And CCultRegData(key) <= cntx.NumbCulReg - 1 Then
                        ' Index forecast - Old logic for computing shift when was using only the
                        ' GDP/Capita function to predict before shift
                        '            Call XYTABL("GDP/Capita (PPP) Versus Survival/Self-Expression Cohort 1", _
                        '                CGDPPCP(key), predicted)
                        '            predicted = AMAX(-2, Amin(predicted, 2))
                        '            cultshse(CCultRegData(key)) = cultshse(CCultRegData(key)) + _
                        '                SURVSECo1(key) - predicted
                        ' Factor loading forecast
                        cultshseload(CInt(CCultRegData(key))) = cultshseload(CInt(CCultRegData(key))) + CSng(TempSurvSELoad - CPredictedSS(key))
                        Culcultshseload(CInt(CCultRegData(key))) = Culcultshseload(CInt(CCultRegData(key))) + CSng(CulTempSurvSELoad - CCulPredictedSS(key))
                    End If
                Next
                Debug.Print("After: Keep track of deviations from predicted for cultural regions - Surv/Self-Expression")

                '       Read and Process WVS - Traditionalism/Secular-Rational
                '       -------------------------------------------------------
                Using db As IFsWVSCohortContext = New IFsWVSCohortContext()
                    'jrs 2025/05/12 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 1").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh1).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Cohort1.HasValue Then
                            TradSecRCo1.Add(item.Country, item.Cohort1)
                        Else
                            TradSecRCo1.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Cohort1)
                        End If
                        If Not TradSecRCo1(item.Country).HasValue Then
                            TradSecRCo1(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Cohort1
                        End If
                        If Not TradSecRCo1(item.Country).HasValue Then
                            TradSecRCo1(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/12 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 2").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh2).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Cohort2.HasValue Then
                            TradSecRCo2.Add(item.Country, item.Cohort2)
                        Else
                            TradSecRCo2.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Cohort2)
                        End If
                        If Not TradSecRCo2(item.Country).HasValue Then
                            TradSecRCo2(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Cohort2
                        End If
                        If Not TradSecRCo2(item.Country).HasValue Then
                            TradSecRCo2(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/12 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 3").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh3).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Cohort3.HasValue Then
                            TradSecRCo3.Add(item.Country, item.Cohort3)
                        Else
                            TradSecRCo3.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Cohort3)
                        End If
                        If Not TradSecRCo3(item.Country).HasValue Then
                            TradSecRCo3(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Cohort3
                        End If
                        If Not TradSecRCo3(item.Country).HasValue Then
                            TradSecRCo3(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/12 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 4").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh4).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Cohort4.HasValue Then
                            TradSecRCo4.Add(item.Country, item.Cohort4)
                        Else
                            TradSecRCo4.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Cohort4)
                        End If
                        If Not TradSecRCo4(item.Country).HasValue Then
                            TradSecRCo4(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Cohort4
                        End If
                        If Not TradSecRCo4(item.Country).HasValue Then
                            TradSecRCo4(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/12 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 5").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh5).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Cohort5.HasValue Then
                            TradSecRCo5.Add(item.Country, item.Cohort5)
                        Else
                            TradSecRCo5.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Cohort5)
                        End If
                        If Not TradSecRCo5(item.Country).HasValue Then
                            TradSecRCo5(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Cohort5
                        End If
                        If Not TradSecRCo5(item.Country).HasValue Then
                            TradSecRCo5(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/12 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 6").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh6).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Cohort6.HasValue Then
                            TradSecRCo6.Add(item.Country, item.Cohort6)
                        Else
                            TradSecRCo6.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Cohort6)
                        End If
                        If Not TradSecRCo6(item.Country).HasValue Then
                            TradSecRCo6(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Cohort6
                        End If
                        If Not TradSecRCo6(item.Country).HasValue Then
                            TradSecRCo6(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    'jrs 2025/05/15 XYTBL = cntx.GetRegression("GDP/Capita (PPP) Versus Traditional/Secular-Rational Total").ToEstimator(CGDPPCP)
                    XYTBL = cntx.GetRegression(TFNameTradSecRatTotal).ToEstimator(CGDPPCP)
                    For Each item In db.TradSecRatW5
                        If item.Total.HasValue Then
                            TradSecRCoTotal.Add(item.Country, item.Total)
                        Else
                            TradSecRCoTotal.Add(item.Country, db.TradSecRatW4.Where(Function(g) g.Country = item.Country).Single().Total)
                        End If
                        If Not TradSecRCoTotal(item.Country).HasValue Then
                            TradSecRCoTotal(item.Country) = db.TradSecRatW3.Where(Function(g) g.Country = item.Country).Single().Total
                        End If
                        If Not TradSecRCoTotal(item.Country).HasValue Then
                            TradSecRCoTotal(item.Country) = ApplyLimits(XYTBL(item.Country), -2, 2)
                        End If
                    Next

                    '       Predict factor loading for traditional/secular-rational
                    '       ---------------------------------------------------
                    'jrs 2025/05/12 Title$ = "GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 1"
                    'jrs 2025/05/12 XYTBL = cntx.GetRegression(Title).ToEstimator(CGDPPCP) 'jrs 2020/02/21 changed from GDPPC to GDPPCP
                    XYTBL = cntx.GetRegression(TFNameTradSecRatCoh1).ToEstimator(CGDPPCP) 'jrs 2020/02/21 changed from GDPPC to GDPPCP

                    Dim TempTradSecRLoad As Single?, CulTempTradSecRLoad As Single?

                    For Each item In db.TradSecRatLoadsLatestW3
                        CTradSRatLd.Add(item.Country, item.Total) 'Factor loadings
                        ActTradSRatLd.Add(item.Country, item.Total) 'Actual factor loadings
                        If Not ActTradSRatLd(item.Country).HasValue Then ActTradSRatLd(item.Country) = 0
                        '        Version 1
                        '        CPredictedTS(item.Country) = -2.10887003334109 + 0.0598320187 * CGDPPC(item.Country) + _
                        '                0.02805482 * LaborIndPer + 0.0420082665 * higdpr + 1.5492058 * culexcomdum
                        ' bbh 2019/01/26 start
                        '       Version 2
                        '        CPredictedTS(item.Country) = -1.8346784855 + 0.059598347 * CGDPPC(item.Country) + _
                        '0.021852669 * LaborIndPer + 1.450810364 * culexcomdum
                        CPredictedTS.Add(item.Country, XYTBL(item.Country))

                        CPredictedTS(item.Country) = AMAX(-2, Amin(CPredictedTS(item.Country), 2))
                        CCulPredictedTS.Add(item.Country, -0.039510212) 'Predicted only by mean

                        If Not CTradSRatLd(item.Country).HasValue Then
                            'missing data from WVS
                            TempTradSecRLoad = CPredictedTS(item.Country)
                            CulTempTradSecRLoad = CCulPredictedTS(item.Country)
                        Else
                            ' bbh 2019/01/26 start
                            'cultshts(CCultRegData(item.Country)) = cultshts(CCultRegData(item.Country)) + _
                            'CTradSRatLd(item.Country) - CPredictedTS(item.Country)
                            cultshts(CInt(CCultRegData(item.Country))) = cultshts(CInt(CCultRegData(item.Country))) + CSng(If(TradSecRCo1(item.Country), 0) - CPredictedTS(item.Country))
                            ' bbh 2019/01/26 end
                            TempTradSecRLoad = CTradSRatLd(item.Country)
                            CulTempTradSecRLoad = CTradSRatLd(item.Country)
                        End If

                        '       Keep track of deviations from predicted for cultural regions - Trad/Sec-Rat
                        '       --------------------------------------------------------------------------
                        If CCultRegData(item.Country) > 0 And CCultRegData(item.Country) <= cntx.NumbCulReg - 1 Then
                            ' Index forecast - Old logic for computing shift when was using only the
                            ' GDP/Capita function to predict before shift
                            '            Call XYTABL("GDP/Capita (PPP) Versus Traditional/Secular-Rational Cohort 1", _
                            '                CGDPPCP(item.Country), predicted)
                            '            predicted = AMAX(-2, Amin(predicted, 2))
                            '            cultshts(CCultRegData(item.Country)) = cultshts(CCultRegData(item.Country)) + _
                            '                TradSecRCo1(item.Country) - predicted
                            ' Load forecast
                            cultshtsload(CInt(CCultRegData(item.Country))) = cultshtsload(CInt(CCultRegData(item.Country))) + CSng(TempTradSecRLoad - CPredictedTS(item.Country))
                            Culcultshtsload(CInt(CCultRegData(item.Country))) = Culcultshtsload(CInt(CCultRegData(item.Country))) + CSng(CulTempTradSecRLoad - CCulPredictedTS(item.Country))
                        End If
                    Next
                End Using

                '   Compute cutural region deviations (average) from predicted
                '   -----------------------------------------------------------
                For i = 1 To cntx.NumbCulReg - 1
                    '        cultshmp(i) = cultshmp(i) / AMAX(0.001, CultRegCNonNull(i))
                    '        cultshse(i) = cultshse(i) / AMAX(0.001, CultRegCNonNull(i))
                    '        cultshts(i) = cultshts(i) / AMAX(0.001, CultRegCNonNull(i))
                    If CultRegCNonNull(i) = 0 Then

                        cultshmp(i) = 0
                        cultshse(i) = 0
                        cultshts(i) = 0

                        cultshseload(i) = 0
                        Culcultshseload(i) = 0
                        cultshtsload(i) = 0
                        Culcultshtsload(i) = 0
                    Else
                        cultshmp(i) = CSng(cultshmp(i) / AMAX(0.001, CultRegCNonNull(i)))
                        cultshse(i) = CSng(cultshse(i) / AMAX(0.001, CultRegCNonNull(i)))
                        cultshts(i) = CSng(cultshts(i) / AMAX(0.001, CultRegCNonNull(i)))

                        cultshseload(i) = CSng(cultshseload(i) / AMAX(0.001, CultRegCNonNull(i)))
                        Culcultshseload(i) = CSng(Culcultshseload(i) / AMAX(0.001, CultRegCNonNull(i)))
                        cultshtsload(i) = CSng(cultshtsload(i) / AMAX(0.001, CultRegCNonNull(i)))
                        Culcultshtsload(i) = CSng(Culcultshtsload(i) / AMAX(0.001, CultRegCNonNull(i)))
                    End If
                Next i
                cultshmp(cntx.NumbCulReg) = 0  'No shifts for "other" region
                cultshse(cntx.NumbCulReg) = 0
                cultshseload(cntx.NumbCulReg) = 0
                cultshts(cntx.NumbCulReg) = 0
                cultshtsload(cntx.NumbCulReg) = 0

                '   Compute cultural loads using cultural regional deviations
                '   --------------------------------------------------------
                For Each key In cntx.Regionalization.Keys
                    ComSurvSELd.Add(key, cultshseload(CInt(CCultReg(key))) + CPredictedSS(key)) 'Computed for all countries, even with data
                    ComSurvSELd(key) = AMAX(-2, Amin(ComSurvSELd(key), 2))
                    ComCulSurvSELd.Add(key, Culcultshseload(CInt(CCultReg(key))) + CCulPredictedSS(key)) 'Computed for all countries, even with data
                    ComCulSurvSELd(key) = AMAX(-2, Amin(ComCulSurvSELd(key), 2))
                    If Not CSurvSELd(key).HasValue Then 'leave data unchanged; fill in
                        CSurvSELd(key) = cultshseload(CInt(CCultReg(key))) + CPredictedSS(key)
                        CSurvSELd(key) = AMAX(-2, Amin(CSurvSELd(key), 2))
                    End If
                    ComTradSRatLd.Add(key, cultshtsload(CInt(CCultReg(key))) + CPredictedTS(key)) 'Computed for all countries, even with data
                    ComTradSRatLd(key) = AMAX(-2, Amin(ComTradSRatLd(key), 2))
                    ComCulTradSRatLd.Add(key, Culcultshtsload(CInt(CCultReg(key))) + CCulPredictedTS(key)) 'Computed for all countries, even with data
                    ComCulTradSRatLd(key) = AMAX(-2, Amin(ComCulTradSRatLd(key), 2))
                    If Not CTradSRatLd(key).HasValue Then 'leave data unchanged; fill in
                        CTradSRatLd(key) = cultshtsload(CInt(CCultReg(key))) + CPredictedTS(key)
                        CTradSRatLd(key) = AMAX(-2, Amin(CTradSRatLd(key), 2))
                    End If
                Next

                'jrs 2022/01/31
                ReDim hivculf(11)
                Using db As IFsContext = New IFsContext()
                    'gvdb 2024/04/05
            		'update for new EF syntax
                    For Each item In db.CulturalParameters2.Where(Function(g) g.ParameterName = "HIVCULF").AsEnumerable()
                        hivculf(1) = CSng(Math.Round(CSng(item.Region1), 1))
                        hivculf(2) = CSng(Math.Round(CSng(item.Region2), 1))
                        hivculf(3) = CSng(Math.Round(CSng(item.Region3), 1))
                        hivculf(4) = CSng(Math.Round(CSng(item.Region4), 1))
                        hivculf(5) = CSng(Math.Round(CSng(item.Region5), 1))
                        hivculf(6) = CSng(Math.Round(CSng(item.Region6), 1))
                        hivculf(7) = CSng(Math.Round(CSng(item.Region7), 1))
                        hivculf(8) = CSng(Math.Round(CSng(item.Region8), 1))
                        hivculf(9) = CSng(Math.Round(CSng(item.Region9), 1))
                        hivculf(10) = CSng(Math.Round(CSng(item.Region10), 1))
                        hivculf(11) = CSng(Math.Round(CSng(item.Region11), 1))
                    Next
                End Using


                Using db2 As IFs2Context = New IFs2Context()
                    db2.Clear(GetType(CulturalParameters))
                    Dim row As CulturalParameters = New CulturalParameters
                    row.ParameterName = "CULTSHMP"
                    row.Region1 = cultshmp(1)
                    row.Region2 = cultshmp(2)
                    row.Region3 = cultshmp(3)
                    row.Region4 = cultshmp(4)
                    row.Region5 = cultshmp(5)
                    row.Region6 = cultshmp(6)
                    row.Region7 = cultshmp(7)
                    row.Region8 = cultshmp(8)
                    row.Region9 = cultshmp(9)
                    row.Region10 = cultshmp(10)
                    row.Region11 = cultshmp(11)
                    db2.CulturalParameters.Add(row)

                    Dim row2 As CulturalParameters = New CulturalParameters
                    row2.ParameterName = "CULTSHSE"
                    row2.Region1 = cultshse(1)
                    row2.Region2 = cultshse(2)
                    row2.Region3 = cultshse(3)
                    row2.Region4 = cultshse(4)
                    row2.Region5 = cultshse(5)
                    row2.Region6 = cultshse(6)
                    row2.Region7 = cultshse(7)
                    row2.Region8 = cultshse(8)
                    row2.Region9 = cultshse(9)
                    row2.Region10 = cultshse(10)
                    row2.Region11 = cultshse(11)
                    db2.CulturalParameters.Add(row2)

                    Dim row3 As CulturalParameters = New CulturalParameters
                    row3.ParameterName = "CULTSHSELOAD"
                    row3.Region1 = cultshseload(1)
                    row3.Region2 = cultshseload(2)
                    row3.Region3 = cultshseload(3)
                    row3.Region4 = cultshseload(4)
                    row3.Region5 = cultshseload(5)
                    row3.Region6 = cultshseload(6)
                    row3.Region7 = cultshseload(7)
                    row3.Region8 = cultshseload(8)
                    row3.Region9 = cultshseload(9)
                    row3.Region10 = cultshseload(10)
                    row3.Region11 = cultshseload(11)
                    db2.CulturalParameters.Add(row3)

                    Dim row4 As CulturalParameters = New CulturalParameters
                    row4.ParameterName = "CULTSHTS"
                    row4.Region1 = cultshts(1)
                    row4.Region2 = cultshts(2)
                    row4.Region3 = cultshts(3)
                    row4.Region4 = cultshts(4)
                    row4.Region5 = cultshts(5)
                    row4.Region6 = cultshts(6)
                    row4.Region7 = cultshts(7)
                    row4.Region8 = cultshts(8)
                    row4.Region9 = cultshts(9)
                    row4.Region10 = cultshts(10)
                    row4.Region11 = cultshts(11)
                    db2.CulturalParameters.Add(row4)

                    Dim row5 As CulturalParameters = New CulturalParameters
                    row5.ParameterName = "CULTSHTSLOAD"
                    row5.Region1 = cultshtsload(1)
                    row5.Region2 = cultshtsload(2)
                    row5.Region3 = cultshtsload(3)
                    row5.Region4 = cultshtsload(4)
                    row5.Region5 = cultshtsload(5)
                    row5.Region6 = cultshtsload(6)
                    row5.Region7 = cultshtsload(7)
                    row5.Region8 = cultshtsload(8)
                    row5.Region9 = cultshtsload(9)
                    row5.Region10 = cultshtsload(10)
                    row5.Region11 = cultshtsload(11)
                    db2.CulturalParameters.Add(row5)

                    'jrs 2022/01/31
                    Dim row6 As CulturalParameters = New CulturalParameters
                    row6.ParameterName = "HIVCULF"
                    row6.Region1 = CSng(Math.Round(hivculf(1), 1))
                    row6.Region2 = CSng(Math.Round(hivculf(2), 1))
                    row6.Region3 = CSng(Math.Round(hivculf(3), 1))
                    row6.Region4 = CSng(Math.Round(hivculf(4), 1))
                    row6.Region5 = CSng(Math.Round(hivculf(5), 1))
                    row6.Region6 = CSng(Math.Round(hivculf(6), 1))
                    row6.Region7 = CSng(Math.Round(hivculf(7), 1))
                    row6.Region8 = CSng(Math.Round(hivculf(8), 1))
                    row6.Region9 = CSng(Math.Round(hivculf(9), 1))
                    row6.Region10 = CSng(Math.Round(hivculf(10), 1))
                    row6.Region11 = CSng(Math.Round(hivculf(11), 1))
                    db2.CulturalParameters.Add(row6)

                    db2.Flush()
                End Using
                Debug.Print("End Of ValuesData.Preprocessor")

                Me.processed = True
            End Sub

            Public Overrides Function Regionalize(seriesName As String, cntx As IRebuildFacade) As IEnumerable(Of KeyValuePair(Of Integer, Single?))
                Me.PreProcess(cntx)

                Dim series As IDictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                Dim weightSeries As IDictionary(Of String, Single?)
                weightSeries = CPOP
                Dim MinVal As Single, MaxVal As Single, bLimits As Boolean = False
                Dim bSum As Boolean = False
                Dim i As Integer

                Select Case seriesName
                    Case "CULTREG"
                        Dim CULTREG As Dictionary(Of Integer, Single?) = New Dictionary(Of Integer, Single?)
                        For i = 1 To NR
                            CULTREG.Add(i, 0)
                        Next

                        Dim reg As Integer, culhold As Single?
                        For Each item In cntx.Regionalization
                            reg% = item.Value
                            culhold = CCultReg(item.Key)
                            If Not culhold.HasValue Then
                                CULTREG(reg%) = cntx.NumbCulReg  'Missing Data; put into other
                            ElseIf CULTREG(reg%) = 0 Then
                                CULTREG(reg%) = culhold 'New region specification
                            ElseIf CULTREG(reg%) <> culhold Then
                                CULTREG(reg%) = cntx.NumbCulReg  'Mixed region; put into other
                            End If
                            CULTREG(reg%) = ApplyLimits(CULTREG(reg%), 0, cntx.NumbCulReg)
                        Next

                        Return CULTREG
                    Case "SURVSELD"
                        series = CSurvSELd
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRATLD"
                        series = CTradSRatLd
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "MATPOSTR_Age18_24"
                        series = CMPRCo1
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "MATPOSTR_Age25_34"
                        series = CMPRCo2
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "MATPOSTR_Age35_44"
                        series = CMPRCo3
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "MATPOSTR_Age45_54"
                        series = CMPRCo4
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "MATPOSTR_Age55_64"
                        series = CMPRCo5
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "MATPOSTR_Age65Plus"
                        series = CMPRCo6
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "MATPOSTR_Total"
                        series = CMPRCoTotal
                        bLimits = True
                        MinVal = 1
                        MaxVal = 5
                    Case "SURVSE_Age18_24"
                        series = SURVSECo1
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "SURVSE_Age25_34"
                        series = SURVSECo2
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "SURVSE_Age35_44"
                        series = SURVSECo3
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "SURVSE_Age45_54"
                        series = SURVSECo4
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "SURVSE_Age55_64"
                        series = SURVSECo5
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "SURVSE_Age65Plus"
                        series = SURVSECo6
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "SURVSE_Total"
                        series = SURVSECoTotal
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Age18_24"
                        series = TradSecRCo1
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Age25_34"
                        series = TradSecRCo2
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Age35_44"
                        series = TradSecRCo3
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Age45_54"
                        series = TradSecRCo4
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Age55_64"
                        series = TradSecRCo5
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Age65Plus"
                        series = TradSecRCo6
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
                    Case "TRADSRAT_Total"
                        series = TradSecRCoTotal
                        bLimits = True
                        MinVal = -2
                        MaxVal = 2
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
                                Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
                        ).ToDictionary(
                           Function(grp) grp.RegionId,
                           Function(grp) ApplyLimits(grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight, MinVal, MaxVal)
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
        End Class

    End Module
End Namespace
