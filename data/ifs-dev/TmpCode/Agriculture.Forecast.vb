        Public Sub IfsAg1FirstYear(context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '
            '   Copyright by Barry B. Hughes 1988-2012
            '   Model code available for review and education only
            '   Reproduction or incorporation in other software by permission only
            '
            '
            '   AGRICULTURAL MODEL
            '
            'jrs 2007/03/23
            sErrorSub = "IFsAg1"

            '
            '   FIRST TIME STEP ONLY
            '-----------------------
            ReDim XKAVE(NR%, 3), mkave(NR%, 3), SFSTOCK(NR%, 3) ', agpp(NR%, 3), AGPPI(NR%, 3)
            ReDim satk(NR%), cD(NR%), INDEMK(NR%), CLdI(NR%), scld(NR%)
            ' wv 2021/03/08 made a set of land related variables, country-specific
            ReDim landsat(NR%), landsat2(NR%)
            'wv 2021/03/09
            ReDim SYIELDINT(NR%)

            'dsr 2015/07/24 xsf, msf, xsfi, and msfi need to have second dimension
            '    ReDim xsf(NR%), msf(NR%), ZSF(NR%), CSF(NR%), FSF(NR%)
            '    ReDim xsfi(NR%), msfi(NR%), ZSFI(NR%), CSFI(NR%), FSFI(NR%)
            ReDim xsf(NR%, 3), msf(NR%, 3), ZSF(NR%), CSF(NR%), FSF(NR%)
            ReDim xsfi(NR%, 3), msfi(NR%, 3), ZSFI(NR%), CSFI(NR%), FSFI(NR%)
            ReDim sZSFI(NR%) 'bbh 2021/09/30
            'sgh 2015/07/24 code for import and export values
            'dsr 2015/07/29 created AGMVALUE and AGMVALUE in common so these lines no longer needed
            '    ReDim AGXCropValue(NR%), AGXMeatValue(NR%), AGXFishValue(NR%)
            '    ReDim AGMCropValue(NR%), AGMMeatValue(NR%), AGMFishValue(NR%)
            ReDim SIALK(NR%), SRETR(NR%), SKAG(NR%)
            ReDim STGRYL(NR%) 'jrs 2019/10/22 , stgrld(NR%)
            ReDim sld(NR%, 5), LDI(NR%, 5), slvhrd(NR%), swapi(NR%)
            'jrs 2026/08/05
            ReDim sforestm(NR%)
            'jrs 2023/10/25
            ReDim ssld(NR%, 5)
            'wv 2020/12/11  agpy added the YLLim(NR%), SYLLim, YLLiminv and AGTECr, and made them country specific, in the next two lines
            ReDim byl(NR%), SFPRI(NR%, 3), SINAG(NR%), SINAGR(NR%), syl(NR%), iyl(NR%)
            ReDim iyieldint(NR%), YLLim(NR%), SYLLim(NR%), AGTECr(NR%)
            ReDim sclavf(NR%), SCLAVL(NR%), RETRI(NR%), tgryli(NR%), yladj(NR%)
            ReDim sbalance(NR%, 3), sbalancelv(NR%), AGReturn(3), AGReturnI(NR%, 3), MAFPRI(NR%)
            ReDim sfprofitr(NR%, 3), SGLDCAP(NR%), SINAGRI(NR%)
            ReDim SSFProfitR(NR%, 3), AGLOSSTRANSI(NR%, 3)  ' FORESTI(NR%)
            ReDim XPriceTermLag(NR%, 3), MPriceTermLag(NR%, 3), lvhrg(NR%)
            ReDim XKAVMAX(NR%, 3), MKAVMax(NR%, 3)
            ReDim WatUsePCI(NR%), CropPCI(NR%)
            ReDim MProdCostI(NR%), cumstk(NR%, 3), scumstk(NR%, 3), sho(NR%, 3)
            ReDim CumStkNegOK(NR%, 3), SCumStkNegOK(NR%, 3) 'bbh 2022/10/27
            ReDim YlShift(NR%), SBalanceForYl(NR%), YlGrowth(NR%) 'bbh 2022/11/28
            ReDim MARKUP(NR%), COSTI(NR%)
            ReDim sagdem(NR%, 3), labagi(NR%)
            'bbh 2020/05/07 next two lines
            ReDim alpha(NR%), SAGTEC(NR%)
            'ReDim agtec(NR%), alpha(NR%) 

            ReDim calactpredrat(NR%), meatactpredrat(NR%), lvcfr(NR%), fedreq(NR%), fedreqm(NR%)
            ReDim meatmaxr(NR%), slvhrdxbase(NR%), SAGP(NR%, 3) ', AGMRI(NR%) dsr 2015/04/17 AGMRI never used any more
            'jrs 2023/11/10
            ReDim SAGPTOT(NR%)
            'jrs 2023/12/01
            ReDim SFDEMTOT(NR%)

            ' wv 2021/08/06 ReDim caldgr(NR%), UrbLandI(NR%), YlMaxM(NR%), nlvherd(NR%)
            ReDim caldgr(NR%), UrbLandI(NR%), nlvherd(NR%) 'YlMaxM(NR%), 
            'jrs 2026/05/27
            ReDim snlvherd(NR%)

            ReDim mfprateagi(NR%), INAGI(NR%), mulst(NR%, 3)
            ReDim SLDGR(NR%), sagpcr(NR%), fishdemipc(NR%), GDPPCPI(NR%) ', sagdemm(NR%)
            'BBH 2009/12/10
            ReDim MaxLandExpansionI(NR%)
            ReDim GDPI(NR%)
            ReDim sGDPPCP(NR%)
            ReDim SENVYLCHG(NR%)
            'BBH 2011/7/30
            ReDim HHShrGDPI(NR%) 'bbh 2019/02/27 
            ReDim MILKEGGSTOMEATI(NR%), MeatToMeatFishRatI(NR%)
            ReDim spopi(NR%)
            'BBH 2013/1/21
            ReDim Saquacul(NR%), surpdef(NR%, 3), scountryextrasurdef(NR%, 3) ', agdemgrma(NR%, 3), AGPGRMA(NR%, 3)
            ReDim swxc(3), swmd(3)
            'jrs 2014/12/03
            'dsr 2015/03/17 Add MeatToMeatFishRatI(NR%), ProtToCalRatI(NR%, 3), GramsToCalRatI(NR%, 3) to next line
            ReDim ProtToCalRatI(NR%, 3), GramsToCalRatI(NR%, 3), CLPCShift(NR%, 2)
            'dsr 2015/03/18
            ReDim MeatAndFishActPredRat(NR%), FeedToFoodRatI(NR%, 3), IndToFoodRatI(NR%, 3), FManToFoodRatI(NR%, 3), AgLossTransToFoodRatI(NR%, 3)
            'jrs 2026/02/10
            ReDim SMeatAndFishPctPred(NR%)
            '   ReDim techgroag(NR%), techcorag(NR%)
            ReDim AgFishCatchI(NR%)
            'dsr 2015/06/19 Add pre-production loss variables
            ReDim AGPppl(NR%, 3), AGPMILKEGGSppl(NR%), AQUACULppl(NR%), AGFISHCATCHppl(NR%)
            ReDim aglossconsperci(NR%, 3)
            ReDim WM(3), WX(3) 'dsr 2015/07/29 Will need for balancing trade in value terms
            ReDim SMalnPopP(NR%)  'bbh 2016/02/17
            ReDim SMeatToMeatFishShift(NR%) 'bbh 2018/03/10
            ReDim GlobalSurDefRate(3), WTFoodType(3), SAGX(NR%, 3) 'bbh 2022/10/27
            ReDim SGlobalSurDefRate(3) 'bbh 2022/11/28

            'sgh 2016/02/14
            ReDim CLPCCUM(NR%)
            'sgh 2016/02/14

            'sgh 2018/01/04 - START
            ReDim IrrigationYieldAdj(NR%)
            ReDim IrrigatedCropland(NR%), NonIrrigatedCropland(NR%), IrrigatedYield(NR%), NonIrrigatedYield(NR%), IrrigatedPortionCropland(NR%)
            ReDim Ildc(NR%)
            'sgh 2018/01/04 - END
            'bbh 2019/04/02
            ReDim SYlImpactWatPrice(NR%) 'moved into common block: YLIMPACTWATPRICE(NR%)
            ReDim iMTARIFFTAXRAg(NR%) 'bbh 2019/10/12
            'jrs 2021/01/08
            ReDim smbaseAgg(NR%, 3), xabsmodAgg(NR%, 3), mbasediffAgg(NR%, 3), iAGM(NR%, 3)
            'jrs 2024/11/13
            ReDim mabsmodAgg(NR%, 3), xbasediffAgg(NR%, 3)

            'mti 2022/08/10 start
            ReDim sReductionFactor(NR%)
            'mti 2022/08/10 End

            Dim wkag = 0!
            Dim wagp = 0!
            Dim wldyl = 0!
            Dim wld = 0!
            Dim WAGDEM = 0!
            Dim WGDP = 0!

            'jrs 2007/01/15 remove not used
            '    WZS = 0
            '    WCS = 0
            '    WZSPhys = 0
            '    WCSPhys = 0
            Dim YlMaxFound = 0!


            'dsr 2015/07/29 created AGMVALUE and AGMVALUE in common so these lines no longer needed
            'sgh 2015/07/24 - START
            'Read in ag trade data in value terms from AgricultureBase
            'Must read this data in here since vatiables are not in common.
            'Dim db As Database
            'Set db = OpenDatabase(IFs_MDB2Active)
            'Dim valtbl As Recordset
            'Set valtbl = db.OpenRecordset("AgricultureBase")
            'valtbl.Index = "Primarykey"
            'valtbl.MoveFirst
            'For R% = 1 To NR%
            '    valtbl.Edit
            '    valtbl.Fields("Region") = RegName$(NR%)
            '    AGXCropValue(R%) = valtbl.Fields("AGXCropValue")
            '    AGXMeatValue(R%) = valtbl.Fields("AGXMeatValue")
            '    AGXFishValue(R%) = valtbl.Fields("AGXFishValue")
            '    AGMCropValue(R%) = valtbl.Fields("AGMCropValue")
            '    AGMMeatValue(R%) = valtbl.Fields("AGMMeatValue")
            '    AGMFishValue(R%) = valtbl.Fields("AGMFishValue")
            '    valtbl.Update
            '    valtbl.MoveNext
            'Next R%
            'valtbl.Close
            'db.Close
            'sgh 2015/06/25 - END

            'jrs 2019/10/22
            Array.Copy(TGRYL, STGRYL, NR% + 1)
            'jrs 2019/11/26 Array.Copy(tgrld, stgrld, NR% + 1)

            'jrs 2024/06/11
            TFNameFishProdLoss = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Fish Production Losses (Log)"
            TFNameFishProdLoss = getFunctionNameYrOrMstRct(TFNameFishProdLoss, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFCalDem = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Calorie Demand (fixed-effect)"
            TFCalDem = getFunctionNameYrOrMstRct(TFCalDem, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TfNameCLMeat = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus CLPC from MeatandFish (" & BaseYear & ") Log"
            TfNameCLMeat = getFunctionNameYrOrMstRct(TfNameCLMeat, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TfNameConsLoss = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Consumption Losses (Log)"
            TfNameConsLoss = getFunctionNameYrOrMstRct(TfNameConsLoss, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TfNameCropProdLoss = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Crop Production Losses (Linear fixed effect)"
            TfNameCropProdLoss = getFunctionNameYrOrMstRct(TfNameCropProdLoss, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameWasteFoodCrop = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Waste to Food Ratio Crops (" & BaseYear & ") Log"
            TFNameWasteFoodCrop = getFunctionNameYrOrMstRct(TFNameWasteFoodCrop, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameWasteFoodMeat = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Waste to Food Ratio Meat (" & BaseYear & ") Log"
            TFNameWasteFoodMeat = getFunctionNameYrOrMstRct(TFNameWasteFoodMeat, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            TFNameIndDemPC = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Industrial Ag Demand/Capita (" & BaseYear & ")"
            TFNameIndDemPC = getFunctionNameYrOrMstRct(TFNameIndDemPC, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            'jrs 2025/02/28
            TFNameMeatDem = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Meat Demand (" & BaseYear & ")"
            TFNameMeatDem = getFunctionNameYrOrMstRct(TFNameMeatDem, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")

            For R% = 1 To NR%
                '   Notes on food categories:
                '       Crops and meat are full categories, but fish is partially developed
                '       On the production side, fish are outside of AGP(2) for meat
                '       On the demand side, AGDEM(2) is for meat/fish protein
                '       In trade, AGX(3) and AGM(3) separately represent fish, but have no dynamics
                '       In pricing and stocks, FSTOCK(2) balances meat and fish supply/demand

                'mti 2022/08/10 start
                sReductionFactor(R%) = 1
                'mti 2022/08/10 End

                '   First Year:  Water Use
                '   ----------------------
                WATUSE(R%) = WATUSEPC(R%) * POP(R%)
                WatUsePCI(R%) = WATUSEPC(R%)
                'jrs 2023/11/10
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("AGP", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                ' dsr 2015/06/15 Need to move next line down after adjusting AGP(R%, 1)
                'CropPCI(R%) = AGP(R%, 1) / POP(R%)

                '   First Year:  Agricultural Yield, Meat Production, Fish Production
                '   -----------------------------------------------------------------
                'dsr 2015/06/15 Compute Pre-Production Loss Crop Production and Production Loss in first year
                'kbn 2017/08/28 Bring in production loss code here for the first year
                Dim aglossprodcropfunc = 0!
                'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Crop Production Losses (Linear fixed effect)", GDPPCP(R%), aglossprodcropfunc, context)
                Call XYTABL(TfNameCropProdLoss, GDPPCP(R%), aglossprodcropfunc, context)
                'jrs 2025/06/26
                aglossprodcropfunc = AMAX(aglossprodcropfunc, 2)
                'jrs 2025/06/26 added * aglossprodm(R%, 5) for Total
                'kbn 2017/09/13 Add multiplier aglossprodm below  'bbh 2017/09/23 revised location of multiplier
                Dim aglossprodcrop = Amin(AMAX(aglossprodcropfunc * aglossprodm(R%, 1) * aglossprodm(R%, 5), 2), 20) 'Losses capped here
                'kbn 2017/08/28 end
                AGPppl(R%, 1) = AGP(R%, 1) / (1 - (aglossprodcrop / 100))
                AGLOSSPROD(R%, 1) = AGPppl(R%, 1) * (aglossprodcrop / 100)
                'dsr 2015/06/15 Following line moved from above since need to use pre-production loss production
                CropPCI(R%) = AGPppl(R%, 1) / POP(R%)
                YL(R%) = AGPppl(R%, 1) / LD(R%, 1)

                ' wv 2020/12/17 agpy *** start updating the maximum yield***
                'based on data, and make it country specific, also added the YLLim, and SYLLim already in the first year

                iyl(R%) = YL(R%)
                ' add an initial variable on yield intensity, and limit the range (0-0.99)
                iyieldint(R%) = CSng(Amin(0.99, YIELDINT(R%)))
                ' calculate the yllim based on yield intensity for countries, and added a maximum yield of 70 ton/ha, to correct sugar cane production countries (Swaziland, Mauritius) and high irrigation countries (Egypt)
                If iyieldint(R%) > 0.01 Then
                    YLLim(R%) = Amin(70, iyl(R%) / iyieldint(R%))
                Else
                    YLLim(R%) = Amin(70, iyl(R%) * 3)
                    'jrs 2023/03/17 this produces run to run YIELDINT(R%) = iyl(R%) / YLLim(R%)
                End If
                'wv 2021/03/09
                SYIELDINT(R%) = YIELDINT(R%)

                ' added saved variable for yield limit
                SYLLim(R%) = YLLim(R%)

                'dsr 2015/06/29 Reset maximum yield to 100
                ' wv 2020/12/17 agpy reset maximum yield to make it country specific
                '        YL(R%) = AMAX(0.01, Amin(YL(R%), 20))
                '        YL(R%) = AMAX(0.01, Amin(YL(R%), 100))
                YL(R%) = AMAX(0.01, Amin(YL(R%), YLLim(R%)))
                ' wv 2020/12/17 agpy *** end updating the maximum yield***

                SYlImpactWatPrice(R%) = 1 'bbh 2019/04/02
                'jrs 2023/10/23
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("YL", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                syl(R%) = YL(R%)
                'wv 2021/03/01 calculate iyl(R%) earlier in the code
                'iyl(R%) = YL(R%)
                byl(R%) = YL(R%)
                If YL(R%) > YlMaxFound Then YlMaxFound = YL(R%)
                YlShift(R%) = 0 'bbh 2022/11/28

                'sgh 2018/01/04 - START
                'Initialize variables needed to calculate the effect of irrigation on yield in later years
                IrrigatedCropland(R%) = LANDIRAREAACTUAL(R%)
                NonIrrigatedCropland(R%) = (LD(R%, 1) * 1000) - IrrigatedCropland(R%)
                'bbh 2018/4/19 start replaced 2.35 with 3.25, Steve and I recalculated
                NonIrrigatedYield(R%) = CSng((LD(R%, 1) * 1000 * syl(R%)) / (NonIrrigatedCropland(R%) + (3.25 * IrrigatedCropland(R%))))
                IrrigatedYield(R%) = CSng(NonIrrigatedYield(R%) * 3.25)
                'bbh 2018/4/19 end replaced 2.35 with 3.25, Steve and I recalculated
                IrrigatedPortionCropland(R%) = IrrigatedCropland(R%) / (LD(R%, 1) * 1000)
                Ildc(R%) = LD(R%, 1)
                'sgh 2018/01/04 - END

                'dsr 2014/12/21 For some reason, wrong line was commented out
                'jrs 2014/12/03
                'dsr 2015/06/15 Compute Pre-Production Loss Production in first year
                'dsr 2015/06/15 Compute Production Loss in first year
                'dsr 2015/06/15 Compute Pre-Production Loss Non-Animal Production in first year
                'kbn 2017/09/13 As no data on meat losses are available, these are kept constant at 10 percent. Have added this temporary
                ' variable below so the value of the multiplier can be maintained.
                'jrs 2025/06/26 added * aglossprodm(R%, 5) for Total
                Dim aglossmeatperc = Amin(AMAX(10 * aglossprodm(R%, 2) * aglossprodm(R%, 5), 2), 20)
                AGPppl(R%, 2) = AGP(R%, 2) / (1 - (aglossmeatperc / 100))
                AGLOSSPROD(R%, 2) = AGPppl(R%, 2) * (aglossmeatperc / 100)
                AGPMILKEGGSppl(R%) = AGPMILKEGGS(R%) / (1 - aglossmeatperc / 100)
                '        LVHERDppl(R%) = LVHERD(R%) / (1 - aglossprodperc(R%, 2) / 100)
                'dsr 2015/06/25 Calculate livestock herd size from actual production numbers

                'bbh 2017/05/21 added miniumum value of LVHERD below; correction needed in preprocessor
                'to create nonzero LVHERD and create meat production different from milk and eggs only
                'jrs 2026/08/07 slr is now country specific
                LVHERD(R%) = AMAX(0.1 * AGPppl(R%, 2) / slr(R%), (AGPppl(R%, 2) - AGPMILKEGGSppl(R%)) / slr(R%))
                '        AGPppl(R%, 2) = LVHERDppl(R%) * slr + AGPMILKEGGSppl(R%)
                'dsr 2015/06/15 Compute share of non-animal production in meat production
                If AGPMILKEGGSppl(R%) > 0 Then 'bbh 2017/05/21 added amax below; correction needed in preprocessor
                    'to add nonzero LVHERD and create meat production different from milk and eggs only
                    MILKEGGSTOMEATI(R%) = AGPMILKEGGSppl(R%) / (AMAX(0.1 * AGPppl(R%, 2), AGPppl(R%, 2) - AGPMILKEGGSppl(R%)))
                End If
                'dsr 2015/06/25 Save Livestock herd size
                slvhrd(R%) = LVHERD(R%)
                slvhrdxbase(R%) = LVHERD(R%)

                '       BBH 7/29/04  Moved to pre-processor because recomp of Feddem setting up run-to-run
                '        If GLDCAP(R%) <= MinLDProd Then 'First reduce feed demand
                '            FEDDEM(R%,1) = AMAX(0.01, LVHERD(R%) * fedreq(R%) - MinLDProd * LD(R%, 2))
                '            GLDCAP(R%) = (LVHERD(R%) * fedreq(R%) - FEDDEM(R%,1)) / LD(R%, 2)
                '        End If
                '       BBH 7/29/04  End BBH
                'dsr 2015/06/25 Look at feed requirements and grazing land productivity
                Call XYTABL("GDP/Capita (PPP) Versus Feed Requirements", GDPPCP(R%), fedreq(R%), context)
                GLDCAP(R%) = (LVHERD(R%) * fedreq(R%) - FEDDEM(R%, 1)) / LD(R%, 2)
                Dim MinLDProd = 0.01! 'One-hundredth ton of meat per hectare (e.g. Saudi dessert)
                If GLDCAP(R%) <= MinLDProd Then 'Put in multiplier that basically increases land productivity
                    Dim FeedEquiv = LD(R%, 2) * MinLDProd
                    fedreqm(R%) = (FeedEquiv + FEDDEM(R%, 1)) / AMAX(0.001, LVHERD(R%) * fedreq(R%))
                    GLDCAP(R%) = (LVHERD(R%) * fedreq(R%) * fedreqm(R%) - FEDDEM(R%, 1)) / LD(R%, 2)
                    GLDCAP(R%) = AMAX(GLDCAP(R%), MinLDProd)
                Else
                    fedreqm(R%) = 1
                End If
                SGLDCAP(R%) = GLDCAP(R%)
                MProdCostI(R%) = (LD(R%, 2) * GLDCAP(R%) + FEDDEM(R%, 1)) * WAP(1)
                'dsr 2014/12/21 Use absolute catch
                'dsr 2015/06/09 Let FISH just equal fish caught
                'FISH(R%) = ofscth * rfssh(R%) + AQUACUL(R%)
                '        FISH(R%) = rfssh(R%) + AQUACUL(R%)
                '        AGP(R%, 3) = FISH(R%)
                '        FISH(R%) = rfssh(R%)

                'dsr 2015/06/15 Compute Pre-Production Loss Fish Production and Production Loss in first year
                'kbn 2017/08/28 Bring in production loss code for fish here for the first year
                Dim aglossaquaculfunc = 0!
                Dim aglossfishfunc = 0!
                'jrs 2024/06/11 
                'Call XYTABL("GDP/Capita (PPP 2011) Versus Fish Production Losses (Log)", GDPPCP(R%), aglossaquaculfunc, context)
                Call XYTABL(TFNameFishProdLoss, GDPPCP(R%), aglossaquaculfunc, context)
                'jrs 2025/06/26 added  * aglossprodm(R%, 5) for Total
                Dim aglossaquacul = Amin(AMAX(aglossaquaculfunc, 2) * aglossprodm(R%, 3) * aglossprodm(R%, 5), 20) 'Losses capped here
                'jrs 2024/06/11
                'Call XYTABL("GDP/Capita (PPP 2011) Versus Fish Production Losses (Log)", GDPPCP(R%), aglossfishfunc, context)
                Call XYTABL(TFNameFishProdLoss, GDPPCP(R%), aglossfishfunc, context)
                'jrs 2025/06/26 added  * aglossprodm(R%, 5) for Total
                Dim aglossfish = Amin(AMAX(aglossfishfunc, 2) * aglossprodm(R%, 4) * aglossprodm(R%, 5), 20) 'Losses capped here
                'kbn 2017/08/28 end
                'kbn 2017/09/13 Add production loss multipliers for fish below
                AQUACULppl(R%) = AQUACUL(R%) / (1 - (aglossaquacul / 100))
                AGFISHCATCHppl(R%) = AGFISHCATCH(R%) / (1 - (aglossfish / 100))
                'dsr 2015/12/12 Compute AGLOSSPROD(R%, 3) as sum of loss in aquaculture and catch
                'this helps to avoid confusion and makes Flex Displays work better
                'AGLOSSPROD(R%, 3) = AQUACULppl(R%) * (aglossprodperc(R%, 3) / 100)
                'AGLOSSPROD(R%, 4) = AGFISHCATCHppl(R%) * (aglossprodperc(R%, 4) / 100)
                AGLOSSPROD(R%, 3) = AQUACULppl(R%) * (aglossaquacul / 100) _
                                  + AGFISHCATCHppl(R%) * (aglossfish / 100)
                AGPppl(R%, 3) = AGFISHCATCHppl(R%) + AQUACULppl(R%)
                Saquacul(R%) = AQUACULppl(R%)
                AgFishCatchI(R%) = AGFISHCATCHppl(R%)
                '   First Year:  Save Prices
                '   ------------------------

                FPRI(R%, 1) = WAP(1)
                FPRI(R%, 2) = WAP(2)
                FPRI(R%, 3) = WAP(3)
                MAFPRI(R%) = WAP(1)
                iwep = WEP
                agdstl = CSng(dstl * 1.5)

                '   First Year:  Agricultural Demand - Apparent Consumption
                '   -------------------------------------------------------
                'jrs 2023/12/01
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("FDEM", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'dsr 2015/04/17 Produce estimate of initial stocks. Assume these are a given fraction of the sum of
                '             production and total domestic supply.
                For f% = 1 To 3
                    'dsr 2015/03/26 Losses now dealt with elsewhere
                    ''            LOSS(R%, f%) = 0
                    ''            LOSSI(R%, f%) = LOSS(R%, f%)
                    'dsr 2015/06/19 Change next line since AGP now is post-production loss
                    '          AGDEM(R%, f%) = AGP(R%, f%) * (1! - LOSS(R%, f%)) - AGX(R%, f%) + AGM(R%, f%)
                    AGDEM(R%, f%) = AGP(R%, f%) - AGX(R%, f%) + AGM(R%, f%)
                    '           If f% = 2 Then AGDEM(R%, f%) = AGDEM(R%, f%) + _
                    '(FISH(R%) - AGX(R%, 3) + AGM(R%, 3)) * (1! - LOSS(R%, f%)) 'more recent version
                    'If f% = 2 Then AGDEM(R%, f%) = AGDEM(R%, f%) + _
                    ' (FISH(R%) * (1! - LOSS(R%, f%) - AGX(R%, 3) + AGM(R%, 3)))
                    'This might be better; need to fix in preprocessor also
                    ''            AGDEM(R%, f%) = AGDEM(R%, f%) - agdstl * (AGDEM(R%, f%) + AGP(R%, f%)) * igdpr(R%)
                    'Estimated demand because stocks not yet known
                    FSTOCK(R%, f%) = CSng((AGP(R%, f%) + AGDEM(R%, f%)) * agdstl) 'Using estimated demand
                    ''            AGDEM(R%, f%) = AGDEM(R%, f%) + FSTOCK(R%, f%) * igdpr(R%) 'Actual demand
                    sagdem(R%, f%) = AGDEM(R%, f%)

                    'jrs 2015/11/13 causes run to run
                    ''dsr 2015/11/02 Adjust FDEM, FEEDEM, FMDEM, INDEM, and AGLOSSTRANS so sum to AGDEM
                    '            adjfactor = AGDEM(R%, f%) / (FDEM(R%, f%) + FEDDEM(R%, f%) + FMDEM(R%, f%) + INDEM(R%, f%) + AGLOSSTRANS(R%, f%))
                    '            FDEM(R%, f%) = FDEM(R%, f%) * adjfactor
                    '            FEDDEM(R%, f%) = FEDDEM(R%, f%) * adjfactor
                    '            FMDEM(R%, f%) = FMDEM(R%, f%) * adjfactor
                    '            INDEM(R%, f%) = INDEM(R%, f%) * adjfactor
                    '            AGLOSSTRANS(R%, f%) = AGLOSSTRANS(R%, f%) * adjfactor
                    '
                    ''dsr 2015/06/19 Save pre-loss production as potential production
                    ''               for later use
                    '            SAGP(R%, f%) = AGPppl(R%, f%) this line is not needed
                    '            agpp(R%, f%) = AGPppl(R%, f%)
                    '            AGPPI(R%, f%) = AGPppl(R%, f%) this parameter is never used
                    '            agdemgrma(R%, f%) = 0.03
                    '            AGPGRMA(R%, f%) = 0.03

                    'jrs 2021/01/08
                    'Save initial AGM value for bilateral trade adjustment
                    iAGM(R%, f%) = AGM(R%, f%)

                Next f%
                'dsr 2015/04/17 The following variable is never used, so comment out here
                '        AGMRI(R%) = AGM(R%, 1) / AMAX(0.01, AGDEM(R%, 1))

                'dsr 2015/06/19 Previously, calculated FDEM as residual. Now no need to do so as FDEM read from data and balance
                '               ensured in pre-processor
                'jrs 2014/11/25
                '        FDEM(R%) = AGDEM(R%, 1) - FEDDEM(R%,1) - INDEM(R%,1)
                '        FDEM(R%) = AMAX(0.01, Amin(FDEM(R%), 1000))
                'jrs 2014/12/01
                Dim aglossconspercpredifunc = 0!
                For f% = 1 To 3
                    'dsr 2015/04/17 Calculate GRAMSPC (grams per capita per day) from FDEM
                    GRAMSPC(R%, f%) = FDEM(R%, f%) / (POP(R%) * 365) * 1000000
                    'dsr 2015/06/15 Calculate consumption loss (MMT per year) from FDEM
                    'kbn 2017/08/28 Insert consumption loss code here
                    'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Consumption Losses (Log)", GDPPCP(R%), aglossconspercpredifunc, context)
                    Call XYTABL(TfNameConsLoss, GDPPCP(R%), aglossconspercpredifunc, context)
                    Dim aglossconspercpredi = Amin(AMAX(aglossconspercpredifunc * aglossconsm(R%, f%), 5), 18)
                    'kbn 2017/09/13 Add multiplier below
                    AGLOSSCONS(R%, f%) = FDEM(R%, f%) * (aglossconspercpredi / 100)
                    'kbn 2017/08/28 end
                    'dsr 2015/07/01 Save initial year value of aglossconsperc as need in later years
                    aglossconsperci(R%, f%) = aglossconspercpredi
                    'dsr 2015/06/21 The following was used to confirm that supply side and use sides sum to total demand
                    'dsr 2015/06/21 At this point, all looks good except for small differences in a few countries where
                    'dsr 2015/06/21 fish supply and demand is very small; differences are still less than 3% in each case
                    '             QDEM = FDEM(R%, f%) + FEDDEM(R%, f%) + INDEM(R%, f%) + FMDEM(R%, f%) + AGLOSSTRANS(R%, f%)
                    '             If AGDEM(R%, f%) > 0 AndAlso Abs(AGDEM(R%, f%) / QDEM - 1) > 0.01 Then _
                    '  Debug.Print RegName$(R%), R%, f%, AGP(R%, f%), AGM(R%, f%), AGX(R%, f%), FDEM(R%, f%), FEDDEM(R%, f%), FMDEM(R%, f%), INDEM(R%, f%), AGLOSSTRANS(R%, f%)
                Next f%

                'dsr 2015/03/27 fishdemipc no longer needed
                '        fishdemipc(R%) = AGDEM(R%, 3) / POP(R%)

                'If RegName$(R%) = "Sao Tome and Principe" Then Stop
                '   First Year:  Agricultural Demand - Predicted Calorie Demand used to calculate Shift Factors
                '   -----------------------------------------------------------

                'dsr 2015/03/18 Calculate Shift Factor related to Total Calories (calactpredrat) **** start
                Dim CalPerCap = 0!
                If context.HistoricMode Then
                    Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1961)",
                    GDPPCP(R%), CalPerCap, context)
                    '            Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1999)", _
                    'GDPPCP(R%), CalPerCap)
                Else
                    'dsr 2014/12/10 Update equation estimating Total Calorie Demand
                    'Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1999)", _
                    'GDPPCP(R%), CalperCap)
                    'jrs 2014/12/03 Call AnalFunc("Total Calories per Capita per Day (Time in logic)", Array(GDPPCP(R%), POPURBAN(R%) / POP(R%) * 100), CalPerCap, Array("GDPPCP", "POPURBAN%"))
                    'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Calorie Demand (fixed-effect)",
                    'GDPPCP(R%), CalPerCap, context)
                    Call XYTABL(TFCalDem, GDPPCP(R%), CalPerCap, context)
                End If

                CalPerCap = Amin(calmax, CalPerCap)
                Dim caldem = CalPerCap * POP(R%)
                calactpredrat(R%) = CLAVAL(R%) / caldem
                'dsr 2015/03/18 Calculate Shift Factor related to Total Calories (calactpredrat) **** end

                '   BBH 1/24/04 V5_0 set initial value of caldgr to 0
                caldgr(R%) = 0

                'sgh 2016/02/14 Set saved value to CLPC in first year
                CLPCCUM(R%) = 1
                SMalnPopP(R%) = MALNPOPP(R%) 'bbh 2016/02/17
                'sgh 2016/02/14

                'dsr 2015/03/18 Calculate Shift Factor related to Meat and Fish share of total calories (meatactpredrat2) **** start
                'dsr 2015/03/25 Equation was giving odd results, so new one created
                '       Call AnalFunc("Percentage of calories from meat and fish (combined)", _
                '            Array(GDPPCP(R%), POPURBAN(R%) / POP(R%) * 100), MeatAndFishPctPred, _
                '            Array("GDPPCP", "POPURBAN%"))

                'jrs 2019/05/03 *** BEGIN First Year ***
                If clpcexovals(R%, 1) > -100 Then
                    CLPC(R%, 1) = clpcexovals(R%, 1)
                End If
                If clpcexovals(R%, 2) > -100 Then
                    CLPC(R%, 2) = clpcexovals(R%, 2)
                End If
                If clpcexovals(R%, 3) > -100 Then
                    CLPC(R%, 3) = clpcexovals(R%, 3)
                End If
                If clpcexovals(R%, 4) > -100 Then
                    CLPC(R%, 4) = clpcexovals(R%, 4)
                Else
                    If clpcexovals(R%, 1) > -100 OrElse clpcexovals(R%, 2) > -100 OrElse clpcexovals(R%, 3) > -100 Then
                        CLPC(R%, 4) = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)
                    End If
                End If
                'jrs 2019/05/03 *** END ***
                Dim MeatAndFishPctPred = 0!
                'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus CLPC from MeatandFish (2010) Log", GDPPCP(R%), MeatAndFishPctPred, context)
                Call XYTABL(TfNameCLMeat, GDPPCP(R%), MeatAndFishPctPred, context)
                'jrs 2026/02/10
                SMeatAndFishPctPred(R%) = MeatAndFishPctPred

                MeatAndFishPctPred = CSng(Amin(AMAX(MeatAndFishPctPred / 100, 0.035), 1)) 'dsr 2015/03/17 Set Minimum based on historical data
                Dim MeatAndFishPctAct = Amin(AMAX(0, (CLPC(R%, 2) + CLPC(R%, 3)) / CLPC(R%, 4)), 1)
                MeatAndFishActPredRat(R%) = MeatAndFishPctAct / MeatAndFishPctPred
                'dsr 2015/03/18 Calculate Shift Factor related to Meat and Fish share of total calories (meatactpredrat2) **** end

                'dsr 2015/03/17 Calculate Meat to Meat plus Fish Calorie Ratio and ratios of protein and grams to calories **** start
                MeatToMeatFishRatI(R%) = CLPC(R%, 2) / (CLPC(R%, 2) + CLPC(R%, 3))
                'bbh 2018/03/10 Used global CLPC 1990-2013 to compute how much faster fish consumption/prod growing than meat

                SMeatToMeatFishShift(R%) = CLPC(R%, 3) * FishMeatCLPCGrDiff / 100 'bbh 2918/03/10
                For f% = 1 To 3
                    ProtToCalRatI(R%, f%) = PROTEINPC(R%, f%) / CLPC(R%, f%)
                    If ProtToCalRatI(R%, f%) = 0 Then
                        If f% = 1 Then ProtToCalRatI(R%, f%) = 0.0189 'dsr 2015/03/26 Median value across countries 2010
                        If f% = 2 Then ProtToCalRatI(R%, f%) = 0.06195 'dsr 2015/03/26 Median value across countries 2010
                        If f% = 3 Then ProtToCalRatI(R%, f%) = 0.015195 'dsr 2015/03/26 Median value across countries 2010
                    End If 'ProtToCalRatI(R%, f%) = 0
                    GramsToCalRatI(R%, f%) = GRAMSPC(R%, f%) / CLPC(R%, f%)
                    If GramsToCalRatI(R%, f%) = 0 Then
                        If f% = 1 Then GramsToCalRatI(R%, f%) = 0.5787 'dsr 2015/03/26 Median value across countries 2010
                        If f% = 2 Then GramsToCalRatI(R%, f%) = 0.94615 'dsr 2015/03/26 Median value across countries 2010
                        If f% = 3 Then GramsToCalRatI(R%, f%) = 1.45365 'dsr 2015/03/26 Median value across countries 2010
                    End If 'GramsToCalRatI(R%, f%) = 0
                Next f%
                'dsr 2015/03/17 Calculate Meat to Meat plus Fish Calorie Ratio and ratios of protein and grams to calories **** end


                'dsr 2015/03/18 Code below can be commented out since changing how we do meat demand and calorie content
                'dsr 2015/03/26 BEFORE DOING SO DEAL WITH PREDICTED MEAT DEMAND
                '   First Year:  Agricultural Demand - Predicted Meat Demand
                '   --------------------------------------------------------
                Dim tonspercap = 0!
                'jrs 2025/02/28 Call XYTABL("GDP/Capita (PPP) Versus Meat Demand (2010)", GDPPCP(R%), tonspercap, context)                
                Call XYTABL(TFNameMeatDem, GDPPCP(R%), tonspercap, context)
                meatmaxr(R%) = AMAX(AGDEM(R%, 2) / POP(R%), meatmax)
                tonspercap = Amin(meatmaxr(R%), tonspercap)
                Dim MeatDem = tonspercap * POP(R%)
                meatactpredrat(R%) = AGDEM(R%, 2) / MeatDem
                'If RegName(R%) = "Puerto Rico" Then Stop
                '   First Year:  Conversion Factor for Meat to Crop Calorie Content
                '   ---------------------------------------------------------------

                lvcfr(R%) = lvcf
                calmeatm = AMAX(0.01, calmeatm)
                'jrs 2014/11/25 If AGDEM(R%, 2) * lvcfr(R%) > _
                'calmeatm * (FDEM(R%) + AGDEM(R%, 2) * lvcfr(R%)) Then
                If AGDEM(R%, 2) * lvcfr(R%) >
            calmeatm * (FDEM(R%, 1) + AGDEM(R%, 2) * lvcfr(R%)) Then
                    'jrs 2014/11/25 While AGDEM(R%, 2) * lvcfr(R%) > _
                    'calmeatm * (FDEM(R%) + AGDEM(R%, 2) * lvcfr(R%))
                    Do While AGDEM(R%, 2) * lvcfr(R%) >
                calmeatm * (FDEM(R%, 1) + AGDEM(R%, 2) * lvcfr(R%))
                        lvcfr(R%) = CSng(lvcfr(R%) * 0.9)
                    Loop
                End If
                '   First Year:  Conversion Factor for Meat and Crop to Calorie Content
                '   -------------------------------------------------------------------
                'If GRPS(1, R) = "Belize" Then Stop

                'jrs 2014/11/25 sclavf(R%) = CLAVAL(R%) / (FDEM(R%) + AGDEM(R%, 2) * lvcfr(R%))+
                sclavf(R%) = CLAVAL(R%) / (FDEM(R%, 1) + AGDEM(R%, 2) * lvcfr(R%))
                'If RegName$(R%) = "Sao Tome and Principe" Then Stop
                'dsr 2015/03/18 Code above can be commented out since changing how we do meat demand and calorie content

                'dsr 2015/03/18 Calculate Ratios to Food demand of Feed, Industrial, Food Manufacturing, and Tranist Losses **** start
                For f% = 1 To 3
                    FeedToFoodRatI(R%, f%) = FEDDEM(R%, f%) / FDEM(R%, f%)
                    IndToFoodRatI(R%, f%) = INDEM(R%, f%) / FDEM(R%, f%)
                    FManToFoodRatI(R%, f%) = FMDEM(R%, f%) / FDEM(R%, f%)
                    AgLossTransToFoodRatI(R%, f%) = AGLOSSTRANS(R%, f%) / FDEM(R%, f%)
                Next f%
                'dsr 2015/03/18 Calculate Ratios to Food demand of Feed, Industrial, Food Manufacturing, and Transit Losses **** end


                '   First Year:  Expected Livestock Production/Consumption Growth
                '   -------------------------------------------------------------
                Dim TbName$ = ""
                Dim pppcomp1 = 0!
                Dim pppcomp2 = 0!
                If context.HistoricMode Then
                    TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (1980)"
                    Call XYTABL(TbName$, GDP(R%) / POP(R%), pppcomp1, context)
                    If GDP(R%) / POP(R%) > 14.29 Then pppcomp1 = GDP(R%) / POP(R%)
                Else
                    TbName$ = "GDP/Capita Versus GDP/Capita at Purchasing Power Parity (2000) Power Function"
                    Call XYTABL(TbName$, GDP(R%) / POP(R%), pppcomp1, context)
                    'bbh 2011/09/17
                    'If GDP(R%) / POP(R%) > 17.2 Then pppcomp1 = GDP(R%) / POP(R%)
                    If GDP(R%) / POP(R%) > 30.617 Then pppcomp1 = GDP(R%) / POP(R%)
                End If
                Dim epop = POP(R%) * (1 + POPR(R%) / 100)
                Dim EGDP = GDP(R%) * (1 + igdpr(R%))
                Dim egdppc = EGDP / epop
                Call XYTABL(TbName$, egdppc, pppcomp2, context)
                'bbh 2011/09/17
                'If frm_MainMenu.IFsGUIctrl1.cHistoricBase <> True AndAlso egdppc > 17.2 Then
                If (Not context.HistoricMode) AndAlso egdppc > 30.617 Then
                    pppcomp2 = egdppc
                End If
                Dim egdppcp = GDPPCP(R%) * pppcomp2 / pppcomp1
                Dim TonsPerCap2 = 0!
                'jrs 2025/02/28 Call XYTABL("GDP/Capita (PPP) Versus Meat Demand (2010)", egdppcp, TonsPerCap2, context)
                Call XYTABL(TFNameMeatDem, egdppcp, TonsPerCap2, context)
                TonsPerCap2 = Amin(meatmaxr(R%), TonsPerCap2)
                Dim MeatGr = (epop * TonsPerCap2) / (POP(R%) * tonspercap) - 1
                lvhrg(R%) = MeatGr
                lvhrg(R%) = CSng(AMAX(0.001, Amin(lvhrg(R%), 0.15)))
                Dim GLFeedEq = LD(R%, 2) * GLDCAP(R%) * (1 + tgrld(R%))
                'dsr 2015/06/22 Use pre-loss size of livestock herd
                Dim efeddem = (LVHERD(R%) * (1 + lvhrg(R%)) * fedreq(R%) * fedreqm(R%) - GLFeedEq)
                'If RegName$(R%) = "China" Then Stop
                '   First Year:  Expected Industrial Consumption Growth
                '   ---------------------------------------------------
                Dim DComp1 = 0!
                Dim DComp2 = 0!
                'jrs 2024/06/19 TbName$ = "GDP/Capita (PPP) Versus Industrial Ag Demand/Capita (2010)"
                Call XYTABL(TFNameIndDemPC, GDPPCP(R%), DComp1, context)
                INDEMK(R%) = INDEM(R%, 1) / (AMAX(0.001, POP(R%) * DComp1)) 'dsr 2015/03/04 change 0.01 to 0.001
                'If RegName$(R%) = "Kosovo" Then Stop
                'jrs 2024/07/26 need to use TFNameIndDemPC instead of TbName$
                Call XYTABL(TFNameIndDemPC, egdppcp, DComp2, context)
                Dim eindem = epop * DComp2 * INDEMK(R%)

                '   First Year:  Expected Food Consumption Growth and Growth in Crop Yield
                '   ----------------------------------------------------------------------
                Dim CalPerCap2 = 0!
                If context.HistoricMode Then
                    'Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1999)", egdppcp, CalPerCap2)
                    Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1961)",
                egdppcp, CalPerCap2, context)
                Else
                    'DSR 2014/12/10 Update equation estimating Total Calorie Demand
                    'Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1999)", egdppcp, CalPerCap2)
                    'jrs 2014/12/03 Call AnalFunc("Total Calories per Capita per Day (Time in logic)", Array(egdppcp, POPURBAN(R%) / POP(R%) * 100), CalPerCap2, Array("GDPPCP", "POPURBAN%"))
                    'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Calorie Demand (fixed-effect)",
                    'egdppcp, CalPerCap2, context)
                    Call XYTABL(TFCalDem, egdppcp, CalPerCap2, context)
                End If
                CalPerCap2 = Amin(calmax, CalPerCap2)
                Dim caldem2 = CalPerCap2 * epop * calactpredrat(R%)
                Dim calfrommeat2 = AGDEM(R%, 2) * (1 + lvhrg(R%)) * lvcfr(R%) * sclavf(R%)
                Dim calfromgrain2 = caldem2 - calfrommeat2
                If calfrommeat2 > calmeatm * caldem2 Then
                    calfromgrain2 = (1 - calmeatm) * caldem2
                End If
                Dim efdem = calfromgrain2 / sclavf(R%)
                Dim etdem = efeddem + eindem + efdem
                tgryli(R%) = etdem / AGDEM(R%, 1) - 1 - tgrld(R%)
                tgryli(R%) = CSng(AMAX(0.001, Amin(tgryli(R%), 0.15)))


                '   First Year:  Compute Capital and Labor Growth Rates
                '   ---------------------------------------------------
                Dim KGroComp = 0!
                Call XYTABL("Investment Ratio Versus Capital Growth Rate",
            I(R%) / GDP(R%), KGroComp, context)
                KGroComp = KGroComp / 100 / 2
                Dim lfgr = 0!  'In agricultural sector no obvious growth

                '   First Year:  Compute Capital
                '   ----------------------------

                INAG(R%) = IDS(R%, 1)
                INAGI(R%) = INAG(R%)
                SIALK(R%) = IALK(R%)
                'If RegName$(R%) = "Armenia" Then Stop
                SINAG(R%) = INAG(R%)
                SINAGR(R%) = SINAG(R%) / GDP(R%)
                SINAGRI(R%) = INAG(R%) / GDP(R%)
                KAG(R%) = CSng(IDS(R%, 1) *
            (1.0! - IALK(R%)) / AMAX(KGroComp + 1 / AMAX(lks(1), 0.001), 0.001))
                alpha(R%) = CDALF(R%) * salphas(R%, 1)
                alpha(R%) = AMAX(0, Amin(alpha(R%), 1))
                cD(R%) = CSng(YL(R%) / AMAX(KAG(R%) ^ alpha(R%) _
            * LABS(R%, 1) ^ (1 - alpha(R%)), 0.001))
                labagi(R%) = LABS(R%, 1)
                'If RegName$(R%) = "Japan" Then Stop
                '   First Year:  Predicted CDALF and ALPHAS
                '   ---------------------------------------
                egdppc = GDPP(R%) * (1 + igdpr(R%)) / (POP(R%) * (1 + POPR(R%) / 100))
                '        pcdalf = cdalfint + cdalfbet * Log(EGDPPC * 1000)

                TbName$ = "GDP/Capita (PPP) Versus Cobb-Douglas Alpha (GTAP 5)"
                Dim pcdalf = 0!
                Call XYTABL(TbName$, egdppc, pcdalf, context)
                'pcdalf = AMAX(0.05, Amin(pcdalf / 100, 1))
                pcdalf = AMAX(0.2, Amin(pcdalf / 100, 1))
                Dim palphas = pcdalf * salphas(R%, 1)
                palphas = AMAX(0, Amin(palphas, 1))

                '   First Year:  Compute Technological Growth Rate
                '   ----------------------------------------------
                Dim ylpred = YL(R%) * (1 + tgryli(R%))
                Dim KContrib = (KAG(R%) * (1 + KGroComp)) ^ palphas
                Dim LContrib = labagi(R%) ^ (1 - palphas)
                '       bbh 2011/8/5  'commented next 2 lines; removal doesn't change behavior, but
                '           presence causes transience in MFPCOR
                '        MFPGro(R%, 1) = ylpred / (cD(R%) * KContrib * LContrib) - 1
                '        MFPCorS(R%, 1) = MFPGro(R%, 1) - MFPRATE(R%, 1) - mfpadd(R%) - MFPHC(R%) / 100
                mfprateagi(R%) = MFPRATE(R%, 1)
                'MFPRATEAGI(R%) = MFPRATET(R%)

                '   First Year:  Trade Coefficients
                '   -------------------------------
                'dsr 2015/07/30 Note these related to trade in quantity, not value
                For f% = 1 To 3
                    '2015/06/19 All commodites use same formula now, so simplify
                    '            If f% = 1 Then
                    '              xbase = agpp(R%, f%) + AGDEM(R%, f%) 'Imports needed for entrepot economy
                    'like Singapore
                    '            ElseIf f% = 2 Then
                    'dsr 2015/04/13 Change LVHERD(R%) * slr to agpp(R%, f%)
                    '                xbase = LVHERD(R%) * slr + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) + AGDEM(R%, f%)
                    '            Else
                    'dsr 2015/06/09 Replace FISH with agpp(R%, f%)
                    '                xbase = FISH(R%) + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) + AGDEM(R%, f%)
                    '            End If
                    'dsr 2015/06/19 define xbase as post-loss production plus total demand
                    Dim xbase = AGP(R%, f%) + AGDEM(R%, f%)
                    'If xbase = 0 OrElse AGDEM(R%, f%) = 0 Then Stop
                    XKAVE(R%, f%) = AGX(R%, f%) / xbase
                    'bbh 2022/10/27 Added here AGP to AGDEM for mkave base; consistent with exports, prices, and YL as import-side base
                    mkave(R%, f%) = AGM(R%, f%) / (AGDEM(R%, f%) + AGP(R%, f%))
                    SAGX(R%, f%) = AGX(R%, f%) 'bbh 2022/10/27 Used to constrain growth of exports by largest exporters
                    'mkave(R%, f%) = AGM(R%, f%) / AGDEM(R%, f%)

                    If f% = 1 OrElse f% = 2 Then
                        XKAVMAX(R%, f%) = CSng(AMAX(0.7, XKAVE(R%, f%) * 1.1))
                        MKAVMax(R%, f%) = CSng(AMAX(0.7, mkave(R%, f%) * 1.5))
                    Else
                        XKAVMAX(R%, f%) = CSng(AMAX(0.95, XKAVE(R%, f%) * 1.1))
                        'MKAVMax(R%, f%) = AMAX(1.15, mkave(R%, f%) * 1.5)
                        MKAVMax(R%, f%) = CSng(AMAX(2, mkave(R%, f%) * 1.5))
                        ' More competitive capability needed relative to China
                    End If

                    XPriceTermLag(R%, f%) = 0
                    MPriceTermLag(R%, f%) = 0

                    surpdef(R%, f%) = 0 'bbh 2020/08/06 Was not being initilized
                Next f%
                iMTARIFFTAXRAg(R%) = MTARIFFTAXR(R%, Agri) 'bbh 2019/10/12 'bbh 2019/10/12

                'dsr 2105/06/19 Production value should reflect post-loss production
                'ProdVal = AGP(R%, 1) * WAP(1) + AGP(R%, 2) * WAP(2) + AGP(R%, 3) * WAP(3)
                'ProdVal = AGPppl(R%, 1) * WAP(1) + AGPppl(R%, 2) * WAP(2) + AGPppl(R%, 3) * WAP(3) 'bbh 2021/09/30 AGP inflates Ag VADD too much
                Dim ProdVal = YL(R%) * LD(R%, 1) * WAP(1) 'bbh 2021/09/30 but so did AGPppl, so went to YL; discussed with Willem
                'ProdVal = AGPppl(R%, 1) * WAP(1)
                ZSFI(R%) = ZS(R%, 1) / ProdVal
                sZSFI(R%) = ZSFI(R%) 'bbh 2021/09/30
                'jrs 2014/11/25 DemVal = FDEM(R%) * WAP(1) + AGDEM(R%, 2) * WAP(2) + AGDEM(R%, 3) * WAP(3)
                'bbh 2017/01/24 Food demand is now a subset of total meat and fish demand
                'DemVal = FDEM(R%, 1) * WAP(1) + AGDEM(R%, 2) * WAP(2) + AGDEM(R%, 3) * WAP(3)
                Dim DemVal = FDEM(R%, 1) * WAP(1) + FDEM(R%, 2) * WAP(2) + FDEM(R%, 3) * WAP(3)
                CSFI(R%) = CS(R%, 1) / DemVal
                Dim FSF_1 = AMAX((FSTOCK(R%, 1) * WAP(1) + FSTOCK(R%, 2) * WAP(2) + FSTOCK(R%, 3) * WAP(3)), 0.001)
                FSFI(R%) = CSng(ZS(R%, 1) * agdstl / FSF_1)
                'jrs 2007/01/15 remove not used
                '        WZS = WZS + ZS(R%, 1)
                '        WCS = WCS + CS(R%, 1)
                '        WZSPhys = WZSPhys + ProdVal
                '        WCSPhys = WCSPhys + DemVal
                '   First Year:  Cost of Land
                '   -------------------------

                'FORESTI(R%) = LD(R%, 3)
                CLD(R%) = CSng(IDS(R%, 1) * IALK(R%) / AMAX(LD(R%, 1) * (dkl + tgrld(R%)), 0.001))
                CLD(R%) = AMAX(0.01, CLD(R%))
                scld(R%) = CLD(R%) 'bbh 2018/03/08
                'jrs 2020/02/19 added Round to both Num and Den to match vb6
                CLdI(R%) = CSng(Math.Round(CLD(R%), 4) / Math.Round(LD(R%, 1), 4))
                'wv 2020/12/17 agpy update the calculation of satk based on the initial yield intensity, initial year always be equal to 1
                satk(R%) = CSng(AMAX(0.1, Amin((YLLim(R%) - syl(R%)) / (YLLim(R%) - iyl(R%)), 1)) ^ ylexp)
                AGTEC(R%) = 1
                SAGTEC(R%) = 1 'bbh 2020/05/07
                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("LD", "Crop", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                UrbLandI(R%) = LD(R%, 5) / AMAX(0.001, POP(R%))
                For Land% = 1 To 5
                    sld(R%, Land%) = LD(R%, Land%)
                    LDI(R%, Land%) = LD(R%, Land%)
                    'jrs 2023/11/10
                    ssld(R%, Land) = LD(R%, Land)
                Next Land%
                spopi(R%) = POP(R%)

                '   First Year:  Profits and Prices
                '   -------------------------------

                For f% = 1 To 3
                    FPROFITR(R%, f%) = 1
                    sfprofitr(R%, f%) = 1
                    SSFProfitR(R%, f%) = 1
                    sbalance(R%, f%) = 0
                    SBalanceForYl(R%) = 0 'bbh 2022/11/28
                    cumstk(R%, f%) = FSTOCK(R%, f%)
                    scumstk(R%, f%) = cumstk(R%, f%)
                    CumStkNegOK(R%, f%) = FSTOCK(R%, f%) 'bbh 2022/10/27 Added 2 lines allowing Negative Stocks
                    SCumStkNegOK(R%, f%) = CumStkNegOK(R%, f%)
                    scountryextrasurdef(R%, f%) = 0  'For trade adjustment
                Next f%
                MARKUP(R%) = 1
                COSTI(R%) = (LD(R%, 1) * CLD(R%) + KAG(R%)) / (YL(R%) * LD(R%, 1))
                'jrs 2007/01/12 no double counting
                If RegiontoSum(R%) Then
                    wkag = wkag + LD(R%, 1) * CLD(R%) + KAG(R%)
                    'dsr 2015/06/19 Since the following is used to calculate capital output ratio, use
                    '               pre-production loss production
                    wagp = wagp + AGPppl(R%, 1)
                    wld = wld + LD(R%, 1)
                    wldyl = wldyl + LD(R%, 1) * YL(R%)
                    WAGDEM = WAGDEM + AGDEM(R%, 1)
                    WGDP = WGDP + GDP(R%)
                End If
                GDPI(R%) = GDP(R%)
                GDPPCPI(R%) = GDPPCP(R%)

            Next R%
            '   First Year:  Economic Model Translation Coefficients
            '   ----------------------------------------------------
            'dsr 2015/07/30 Moved this down to deal with need to balance value of trade by commodity
            'dsr 2015/07/29 created AGMVALUE and AGMVALUE in common so change code here
            'sgh 2015/07/24 - START
            'Normalize crop, meat, and fish trade values so they sum to MS(R%, 1) and XS(R%, 1) from economic pre-processor
            '        AgTotalImportsSum = (AGMCropValue(R%) + AGMMeatValue(R%) + AGMFishValue(R%))
            '        AgTotalExportsSum = (AGXCropValue(R%) + AGXMeatValue(R%) + AGXFishValue(R%))
            '        AGMCropValue(R%) = AGMCropValue(R%) / AgTotalImportsSum * MS(R%, 1)
            '        AGXCropValue(R%) = AGXCropValue(R%) / AgTotalExportsSum * XS(R%, 1)
            '        AGMMeatValue(R%) = AGMMeatValue(R%) / AgTotalImportsSum * MS(R%, 1)
            '        AGXMeatValue(R%) = AGXMeatValue(R%) / AgTotalExportsSum * XS(R%, 1)
            '        AGMFishValue(R%) = AGMFishValue(R%) / AgTotalImportsSum * MS(R%, 1)
            '        AGXFishValue(R%) = AGXFishValue(R%) / AgTotalExportsSum * XS(R%, 1)


            'dsr 2015/07/30 This block: 1) makes an initial adjustment to country level values of
            '               imports and exports by commodity so that they sum to MS and XS from
            '               the Econ pre-processor and 2) calculates global sums of imports and
            '               exports by commodity
            For f% = 1 To 3
                WM(f%) = 0!
                WX(f%) = 0!
            Next f%
            Dim AgTotalImportsSum = 0!
            Dim AgTotalExportsSum = 0!
            For R% = 1 To NR%
                AgTotalImportsSum = (AGMVALUE(R%, 1) + AGMVALUE(R%, 2) + AGMVALUE(R%, 3))
                AgTotalExportsSum = (AGXVALUE(R%, 1) + AGXVALUE(R%, 2) + AGXVALUE(R%, 3))
                For f% = 1 To 3
                    'AGMVALUE(R%, f%) = AGMVALUE(R%, f%) / AgTotalImportsSum * MS(R%, 1)
                    'AGXVALUE(R%, f%) = AGXVALUE(R%, f%) / AgTotalExportsSum * XS(R%, 1)
                    WM(f%) = WM(f%) + AGMVALUE(R%, f%)
                    WX(f%) = WX(f%) + AGXVALUE(R%, f%)
                Next f%
            Next R%

            'dsr 2015/07/30 This block: 1) adjusts country level values of imports and exports by commodity
            '               so that global import and export values by commodity match and 2) recalclates
            '               MS and XS for agriculture in each country, and 3) calculates xsf and msf
            '               (factors to convert price indices to monetary values) for each commodity.
            'jrs 2015/11/13
            '    For R% = 1 To NR%
            '        MS(R%, 1) = 0!
            '        XS(R%, 1) = 0!
            '    Next R%
            Dim WTRA = 0!
            For f% = 1 To 3
                WTRA = (WX(f%) + WM(f%)) / 2
                For R% = 1 To NR%
                    '             XS(R%, 1) = XS(R%, 1) * WTRA / AMAX(WX, 0.001)
                    '             MS(R%, 1) = MS(R%, 1) * WTRA / AMAX(WM, 0.001)
                    'jrs 2015/11/13
                    'AGMVALUE(R%, f%) = AGMVALUE(R%, f%) * WTRA / AMAX(WM(f%), 0.001)
                    'AGXVALUE(R%, f%) = AGXVALUE(R%, f%) * WTRA / AMAX(WX(f%), 0.001)
                    'MS(R%, 1) = MS(R%, 1) + AGMVALUE(R%, f%)
                    'XS(R%, 1) = XS(R%, 1) + AGXVALUE(R%, f%)
                    msfi(R%, f%) = CSng(AGMVALUE(R%, f%) / AMAX((AGM(R%, f%) * WAP(f%)), 0.001))
                    xsfi(R%, f%) = CSng(AGXVALUE(R%, f%) / AMAX((AGX(R%, f%) * WAP(f%)), 0.001))
                Next R%
            Next f%

            'Note:  if switched to energy model approach (making physcial trade equal to
            'financial trade, would need to revisit process of normalization
            'sgh 2015/07/30 - END


            wbalance = 0!
            swbalance = 0!
            'jrs 2007/01/15 remove not used
            '    WTarConvF = WZS / WZSPhys
            '    WTarConvFCS = WCS / WCSPhys

            ' wv 2021/08/06 coded this section out. we now have updated the calculation on yllim using yield intensities and don't use this adjustment anymore because the development difference is already in the yield intensity data
            ''Assume developed countries have approached maximum yields proportionately;
            ''   LDCs have headroom; if ever get country-specific maximums, remove multiplier
            'For R% = 1 To NR%
            '    Dim DevWeight = Amin(1, GDPPCP(R%) / 30)
            '    'bbh 2018/3/7: YLMaxFound set to 0 in first year and then reset to YL, so below yields "1"
            '    YlMaxM(R%) = CSng(1 * (1 - DevWeight) + (YL(R%) / AMAX(0.001, YlMaxFound)) ^ 0.35 * DevWeight)
            'Next R%
            'wv 2021/08/06 coded out ylmaxm, not used in the code anymore

            Dim wyl = wldyl / wld
            'dsr 2015/06/30 Save initial values
            wyli = wyl
            Dim wylkagri = wyl / wkag
            wkagagpri = wkag / wagp
            Dim WAGDEMRI = WAGDEM / WGDP
            swylkagr = wyl / wkag
            swkagagpr = wkag / wagp
            swagdemr = WAGDEM / WGDP

            For f% = 1 To 3
                swapi(f%) = WAP(f%)
                swxc(f%) = 1
                swmd(f%) = 1
                GlobalSurDefRate(f%) = 0 'bbh 2022/10/27
                SGlobalSurDefRate(f%) = 0 'bbh 2022/11/28
                WTFoodType(f%) = 0 'bbh 2022/10/27 Initialize global trade by food type
                For R% = 1 To NR%
                    WTFoodType(f%) = WTFoodType(f%) + AGX(R%, f%)
                Next R%
            Next f%
            '    ico2 = AMAX(0.01, 1 + CO2PER / 100)
            ico2 = 1
            sco2 = 1
            ' GoTo AllTimeSteps:
            Call ifsAgAllYears(1, context)

        End Sub
        Public Sub IfsAg1AllButFirstYear(context As IForecastContext)
            'AllButFirst:


            'FirstCall:

            wdmbase = 0
            wbalance = 0
            'ttt 2019/10/08 TakeOut
            Array.Copy(xsfi, xsf, xsfi.Length)
            Array.Copy(msfi, msf, msfi.Length)
            Array.Copy(ZSFI, ZSF, NR + 1)
            Array.Copy(CSFI, CSF, NR + 1)
            Array.Copy(FSFI, FSF, NR + 1)

            For R% = 1 To NR%
                'XSF(R%) = ConvergeOverTime(XSFI(R%), WTarConvF, 50)
                'MSF(R%) = ConvergeOverTime(MSFI(R%), WTarConvF, 50)
                'ZSF(R%) = ConvergeOverTime(ZSFI(R%), WTarConvF, 50)
                'CSF(R%) = ConvergeOverTime(CSFI(R%), WTarConvF, 50)
                'For Eritrea, big push up in CS with converenge is problem
                'sgh 2015/07/24 - xsf and msf now have second dimension START
                'xsf and msf remain constant over time; i.e., no change in ratio of monetary value to price index
                '    xsf(R%) = xsfi(R%)
                '    msf(R%) = msfi(R%)
                'ttt 2019/10/08 TakenOut
                'For f% = 1 To 3
                '    xsf(R%, f%) = xsfi(R%, f%)
                '    msf(R%, f%) = msfi(R%, f%)
                'Next f%
                ''sgh 2015/07/24 - xsf and msf now have second dimension  END
                '        If gdprext(R%) > -90 Then 'bbh 2021/09/30 start; did not work, but leave block, may revisit at some point
                '            ZSFI(R%) = ZSFI(R%) * (1 + gdprext(R%) / 100) '- IGDPRCor(R%)) 'igdpr(R%))
                '        Else
                '            ZSFI(R%) = sZSFI(R%)
                '        End If 'bbh 2021/09/30 end
                'ZSF(R%) = ZSFI(R%)
                'CSF(R%) = CSFI(R%)
                ''FSF(R%) = ConvergeOverTime(FSFI(R%), WTarConvF, 50)
                'FSF(R%) = FSFI(R%) 'Stock targets different, physical, monetary
                'jrs 2007/01/12 no double counting
                'dsr 2015/07/01 Compute world crop demand base and balance, where balance equals stocks
                '    (including shortages) minus desired stocks, which are a fraction of the demand base
                If RegiontoSum(R%) Then
                    'Use post-loss production to compute demand base
                    wdmbase = wdmbase + sagdem(R%, 1) + SAGP(R%, 1)
                    dmbase = sagdem(R%, 1) + SAGP(R%, 1)
                    wbalance = wbalance + scumstk(R%, 1) - dmbase * agdstl
                End If
            Next R%

            'dsr 2015/07/01 Calculate adjustment factor based on world crop balance; this affects
            '   the technical contribution to crop yield growth in all countries
            Dim wmul = 0!
            Call ADJSTR(wdmbase, wbalance, wbalance - swbalance, elfdpr1, elfdpr2, wmul)
            'dsr 2015/07/01 Save world crop balance for next year's calculation
            swbalance = wbalance
            Dim capterm = 0!
            Dim labterm = 0!
            'ttt 2019/10/08 TakeOut
            Array.Copy(SKAG, KAG, NR + 1)
            Array.Copy(slvhrd, LVHERD, NR + 1)
            Array.Copy(SGLDCAP, GLDCAP, NR + 1)
            Array.Copy(SFPRI, FPRI, SFPRI.Length)
            Array.Copy(sfprofitr, FPROFITR, sfprofitr.Length)
            Array.Copy(sld, LD, sld.Length)
            'jrs 2019/10/22
            Array.Copy(STGRYL, TGRYL, NR% + 1)
            'jrs 2019/11/26 Array.Copy(stgrld, tgrld, NR% + 1)
            For R% = 1 To NR%
                'jrs 2023/10/25
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("LD", "Crop", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'ttt 2019/10/08 TakenOut
                'KAG(R%) = SKAG(R%)
                CLD(R%) = AMAX(0.01, scld(R%))
                'ttt 2019/10/08 TakenOut
                'LVHERD(R%) = slvhrd(R%)
                'GLDCAP(R%) = SGLDCAP(R%)
                ''        TGRYL(R%) = tgryli(R%)
                ''If RegName$(R%) = "United States of America" Then Stop
                'For f% = 1 To 3
                '    FPRI(R%, f%) = SFPRI(R%, f%)
                '    FPROFITR(R%, f%) = sfprofitr(R%, f%)
                'Next f%
                'For L% = 1 To 5
                '    LD(R%, L%) = sld(R%, L%)
                'Next L%

                '  All but First 1: Update CDALF and Sectoral Alphas
                '  -------------------------------------------------

                TbName$ = "GDP/Capita (PPP) Versus Cobb-Douglas Alpha (GTAP 5)"
                Call XYTABL(TbName$, sGDPPCP(R%) * (1 + igdpr(R%)), CDALF(R%), context)
                'GDPPCP not yet available from economic model
                'CDALF(R%) = AMAX(0.05, Amin(CDALF(R%) / 100, 1))
                CDALF(R%) = AMAX(0.2, Amin(CDALF(R%) / 100, 1))
                alpha(R%) = CDALF(R%) * salphas(R%, 1)
                alpha(R%) = AMAX(0, Amin(alpha(R%), 1))

                '  All but First 1: Technological Growth
                '  -------------------------------------

                ' wv 2020/12/17 agpy *** start updating the yield growth logic here ***
                If IY% = 2 Then 'Compute YL growth attributable to Capital, etc.
                    Dim YlGroTechTemp = (TGRYL(R%) +
                MFPRATE(R%, 1) - mfprateagi(R%)) * satk(R%)
                    ' wv 2020/12/17 agpy updated the technology growth to increase over time, not decrease. assumes knowledge cannot be lost
                    Dim agtectemp As Single
                    If YlGroTechTemp > 0 Then
                        agtectemp = 1 + YlGroTechTemp
                    Else
                        agtectemp = 1
                    End If
                    capterm = CSng(KAG(R%) ^ alpha(R%))
                    labterm = CSng(labagi(R%) ^ (1 - alpha(R%)))
                    ' wv 2020/12/12 agpy change the structure such that the saturation limits the change in yield not the yield itself
                    ' wv 2020/12/17 agpy  and do not allow for a drop in yields relative to the previous year
                    'old formulation byl(R%) = agtectemp(R%) * cD(R%) * capterm(R%) * labterm(R%) * satk(R%)
                    byl(R%) = agtectemp * cD(R%) * capterm * labterm
                    If byl(R%) > iyl(R%) Then
                        byl(R%) = iyl(R%) + ((byl(R%) - iyl(R%)) * satk(R%))
                    Else
                        byl(R%) = AMAX(syl(R%), iyl(R%))
                    End If
                    Dim YlGr = byl(R%) / iyl(R%) - 1
                    'wv 2020/12/12 agpy updated the calculation here to only allow for positive change, bounded by 0. otherwise countries have continuous negative draws on their techn yield growth
                    'yladj(R%) = YlGr(R%) - TGRYL(R%)
                    If YlGr > TGRYL(R%) And TGRYL(R%) > 0 Then
                        yladj(R%) = YlGr - TGRYL(R%)
                    Else
                        yladj(R%) = 0
                    End If
                End If
                Dim techcontrib = ConvergeOverTime(TGRYL(R%), CSng(0.5 * TGRYL(R%)), 100) * wmul
                Dim ylgrotech = AMAX(0, (techcontrib - yladj(R%) +
            MFPRATE(R%, 1) - mfprateagi(R%)) * satk(R%))
                'bbh 2020/05/07
                AGTEC(R%) = SAGTEC(R%) * (1 + ylgrotech)
                AGTEC(R%) = AMAX(0.01, Amin(AGTEC(R%), 100))
                'wv 2020/12/12 agpy added the growth rate in agtec, later used to drive increases in the yield limit. now assumed to be at 10% of agtec change, i.e. 10% goes to growth in yield limit
                AGTECr(R%) = AMAX(1, AGTEC(R%) / SAGTEC(R%))
                AGTECr(R%) = CSng(1 + (0.1 * (AGTECr(R%) - 1)))
                SAGTEC(R%) = AGTEC(R%)
                'wv 2020/12/17** end updating the yield growth logic here ***

                '  All but First 1: Crop Yield
                '  ---------------------------
                ' wv 2020/12/17 agpy  *** start updating the crop yield logic here to reflect above changes for iy%>2 ***
                capterm = CSng(KAG(R%) ^ alpha(R%))
                labterm = CSng(labagi(R%) ^ (1 - alpha(R%)))
                ' wv 2020/12/12 agpy updating the logic here again, satk should be limiting the change, not the actual yield
                byl(R%) = AGTEC(R%) * cD(R%) * capterm * labterm
                ' wv 2020/12/12 agpy make sure crop yields do not decline over time, and saturation limits growth
                If byl(R%) > iyl(R%) Then
                    byl(R%) = iyl(R%) + ((byl(R%) - iyl(R%)) * satk(R%))
                Else
                    'wv 2021/03/29 added ylm, to not have multiplier effect carry forward
                    'byl(R%) = AMAX((syl(R%) * (1 / AMAX(ylm(R%), 0.0001))), iyl(R%))
                    ' wv 2021/04/28 reverted back to old formulation. otherwise scenarios with ylm where messing up the forecasts
                    'byl(R%) = AMAX(syl(R%), iyl(R%))
                    byl(R%) = byl(R%) 'bbh 2022/11/28 Decreasing pop and labterm or other factors can push byl below iyl even though syl have been going up as
                    'a result of pressures from FSTOCK; in that situation the code line above was causing an upward transient, so just need to hold byl at byl
                    'Could simply remove Else line and the equivalency statement here, but some utility in having comment record in the code
                End If
                'old formulation byl(R%) = AGTEC(R%) * cD(R%) * capterm(R%) * labterm(R%) * satk(R%)
                ' wv 2020/12/17 agpy *** end updating the crop yield logic here to reflect changes in tech growth for iy%>2 ***

                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation
                'dsr 2015/06/30 Modify next set of adjustments to byl
                'bbh 2014/11/02 start; Chile was at YL about 8; aginvm was having no impact
                '        StartRestrict = 5

                Dim ylgrbound = 0!
                Dim StartRestrict = AMAX(iyl(R%) + 2, wyli + 2)
                If aginvm(R%) > 1 Then StartRestrict = CSng(Amin(10, StartRestrict * (aginvm(R%) ^ alpha(R%))))
                If syl(R%) > StartRestrict Then
                    ylgrbound = AMAX(0.01, ylmaxgr * AMAX(0, (StartRestrict + 3 - syl(R%))) / 3)
                    '        'bbh 2014/11/02 end
                Else
                    ylgrbound = ylmaxgr
                End If
                ylgrbound = AMAX(ylgrbound, tgryli(R%))
                'wv 2021/02/10 update the ylgrbound to allow for differences in development in agriculture, with countries with lower intensities being able to grow more on an annual basis
                'wv 2021/03/09 using SYIELDINT
                If iyieldint(R%) <= 0.65 Then ylgrbound = (1 - SYIELDINT(R%))
                'bbh 2022/11/28 A major challenge in forecasting yield is combining changes that come from the C-D like formulation for byl with the
                '   historical patterns of yield shifts from that byl value driven by stock levels and water/irrigation impacts; here have
                '   moved logic to simply start with the shift from past years and then modify that shift across time. But another big issue is
                '   around constraints that yields increasingly face as they reach limits set in the model.  The structure below allows those limits
                '   to constrain the dynamics driven by byl and the yield shift factor.  Initially the logic just took the minimum but that can set
                '   up a transient in moving from byl and shift to the constrained value (e.g. Burundi).  So I added a logic to phase in the constraint (now commented out
                '   below the code line below; that proved complicated and I went back to the code line below, but added constraints on the extent of year-to-year % change in yield
                '   rates further down.Let's keep the commented out code below that line
                YL(R%) = Amin(byl(R%) - YlShift(R%), syl(R%) * (1 + ylgrbound)) 'bbh 2022/11/28
                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation

                '        If Abs(((byl(R%) - YlShift(R%)) - (syl(R%) * (1 + ylgrbound))) / syl(R%)) < 0.05 * YL(R%) Then
                '            YL(R%) = YL(R%) + 0.05 * Abs((byl(R%) - YlShift(R%)) - (syl(R%) * (1 + ylgrbound)))
                '        End If
                'If IY% + 2016 >= 2094 And RegName$(R%) = "Gambia" Then Stop
                '        If Abs((byl(R%) - YlShift(R%)) - (syl(R%) * (1 + ylgrbound)) / syl(R%)) > 0.1 * syl(R%) Then
                '            YL(R%) = Amin(byl(R%) - YlShift(R%), syl(R%) * (1 + ylgrbound)) 'it is clear-cut that should use smaller calculation
                '        Else 'when the distance is less than 10 percent, use smaller but blend a bit toward the larger to slow down transitions when they are close
                '            YL(R%) = Amin(byl(R%) - YlShift(R%), syl(R%) * (1 + ylgrbound)) + 0.1 * Abs((byl(R%) - YlShift(R%)) - (syl(R%) * (1 + ylgrbound)))
                '        End If

                '        byl(R%) = Amin(byl(R%), syl(R%) * (1 + ylgrbound))
                'bbh 2022/10/27 The line above was preventing any adjustments upward of yield over time to persist going forward; line below allows
                'byl(R%) = Amin((byl(R%) + syl(R%)) / 2, syl(R%) * (1 + ylgrbound)) 'bbh 2022/11/28 Need different treatment given byl versus syl
                '        If byl(R%) > syl(R%) Then 'bbh 2022/11/28 Removed this if-then block when moved to YlShift approach
                '            byl(R%) = (byl(R%) + syl(R%)) / 2
                '        Else 'byl<=syl
                '            byl(R%) = Amin((byl(R%) + syl(R%)) / 2, syl(R%) * (1 + ylgrbound))
                '        End If 'bbh 2022/11/28 please keep this commented code up to YL equation

                'If IY% + 2016 >= 2067 And RegName$(R%) = "Burundi" Then Stop

                'If RegName$(R%) = "Ethiopia" Then Stop
                'dsr 2015/07/01 Calculate initial estimate of actual crop yield as base yield times a multiplier
                'YL(R%) = byl(R%) * ylm(R%)
                'dsr 2015/07/01 Use this initial yield estimate times crop land to compute potential crop production.
                '    This is used later in trade calculations
                '        agpp(R%, 1) = YL(R%) * LD(R%, 1)

                'dsr 2015/07/01 This block calculates an adjustment to actual crop yields based on crop balances
                'dsr 2015/06/26 Use crop demand and post loss production from previous year to calculate crop demand base
                dmbase = sagdem(R%, 1) + SAGP(R%, 1) 'bbh 2022/10/27 This had been deactivated for some reason
                'dsr 2015/07/2015 Use crop stocks, including shortages, to calculate crop balance
                'balance = CumStk(R%, 1) - dmbase * agdstl
                'balance = CumStkNegOK(R%, 1) - dmbase * agdstl 'bbh 2022/10/27 Allowing negative stocks to further boost yields
                Dim balance = Amin(SFSTOCK(R%, 1), CumStkNegOK(R%, 1)) - dmbase * agdstl 'bbh 2022/11/28 Eliminate positive carryover of stocks when FSTOCK goes to zero
                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation

                'If IY% + 2016 >= 2094 And RegName$(R%) = "Gambia" Then Stop
                'If IY% + 2016 = 2050 And RegName$(R%) = "Central African Republic" Then Stop
                'bbh 2022/10/27 Added if-then to change yields only for outliers (not CAR type countries); bbh 2022/11/28 removed again, was causing spikes
                '        If GlobalSurDefRate(1) > 0 And CumStkNegOK(R%, 1) / dmbase - agdstl > GlobalSurDefRate(1) Then 'global surplus stocks;
                '        'Stop
                '            balance = balance + ((CumStkNegOK(R%, 1) / dmbase - agdstl) - GlobalSurDefRate(1)) * dmbase / 6 '3 'bbh 2022/11/28
                '        ElseIf GlobalSurDefRate(1) < 0 And CumStkNegOK(R%, 1) / dmbase - agdstl < GlobalSurDefRate(1) Then
                '        'Stop
                '            balance = balance + ((CumStkNegOK(R%, 1) / dmbase - agdstl) - GlobalSurDefRate(1)) * dmbase / 6 '3 'bbh 2022/11/28
                '        End If
                'balance = balance + GlobalSurDefRate(1) * dmbase / 5 'bbh 2022/10/27 Using global surplus or deficit to influence yields everywhere

                'dsr 2015/07/01 Calculate adjustment factor based on domestic crop balances. This affects
                '    domestic crop yield
                'bbh 2018/03/2018 parameters for this and yield only change PID the same, -0.5 and -1.0
                '   This is "free" change and stock affect; probably should be lower, so cut in half
                Dim Mul = 0!, Mul2 = 0!
                'Call ADJSTR(dmbase, balance, balance - sbalance(R%, 1), elfdpr1, elfdpr2, Mul)
                'Call ADJSTR(dmbase, balance, balance - sbalance(R%, 1), elfdpr1, elfdpr2, Mul)
                'Call ADJSTR(dmbase * agdstl, balance, balance - sbalance(R%, 1), -0.2, -0.4, Mul)        
                Call ADJSTR(dmbase, balance, balance - SBalanceForYl(R%), -0.2, -0.4, Mul) 'bbh 2022/11/28 Replaced above line with this because sbalance was being used elsewhere also
                'Call ADJSTR(1.0#, GlobalSurDefRate(1), GlobalSurDefRate(1) - SGlobalSurDefRate(1), -0.2, -0.4, mul2) 'bbh 2022/11/28 Augment national balancing with global
                Call ADJSTR(wdmbase / 186, GlobalSurDefRate(1), GlobalSurDefRate(1) - SGlobalSurDefRate(1), -0.2, -0.4, Mul2) 'bbh 2022/12/13  fix PID demand base replacing above, #1 was not comparable to country PID
                If GDPPC(R%) < 1.0# Then Mul2 = AMAX(1, Mul2) 'bbh 2022/12/13 For very poor countries (e.g. CAR, Burundi) global stock levels should not decrease yields
                'Call ADJSTR(dmbase, GlobalSurDefRate(1), GlobalSurDefRate(1) - SGlobalSurDefRate(1), -0.2, -0.4, mul2) 'bbh 2022/12/13 Alternative fix PID demand base tried; almost as good as wdmbade/186 but a bit less smooth
                If GRPS(1, R%) = "Brazil" Or GRPS(1, R%) = "China" Then Mul2 = Mul2 + 3 * (Mul2 - 1) 'bbh 2022/11/18 Huge global market actors that need be swung most by global balancing process

                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation
                'dsr 2015/07/01 Save domestic crop balance for next year's calculation
                'sbalance(R%, 1) = balance 'bbh 2022/11/28 Commented out because sbalance(r%,1) is used later in prices calc and then saved; if changed here, will be wrong in prices
                SBalanceForYl(R%) = balance 'bbh 2022/11/28 Need to save balance at this point; in prices is advanced a year in logic
                'dsr 2015/07/01 Adjust actual yield to reflect domestic crop balance effect
                ' wv 2021/03/29 updated the yield adjustment to remain within 5% change. this is especially important to limit COVID-19 shock to agriculture dropping or increasing more than 10% between years
                ' wv 2021/03/29 only applied 5% limit on mul, so that scenarios can still change yields through ylm with more than 5% per year
                'YL(R%) = byl(R%) * ylm(R%) * Amin(AMAX(Mul, 0.95), 1.05)
                'YL(R%) = YL(R%) * ylm(R%) * Amin(AMAX(Mul, 0.95), 1.1) 'bbh 2022/10/27 Need bigger gains for CAR at least
                YL(R%) = CSng(YL(R%) * ylm(R%) * Amin(AMAX(Mul, 0.95), 1.1) * Amin(AMAX(Mul2, 0.95), 1.1)) 'bbh 2022/11/28 replace line above
                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation

                'bbh 2020/12/10 start Pass through of yield changes from stocks to technology term
                '  logic is upside shift in production raises longer-term productivity some portion of the boost
                '  tried similar logic on downside, but it clobbers yield and forces up land use
                '   0.8 vs 0.5 has limited impact upside
                'wv 2021/04/22 this was messing up a lot of scenarios related to the agricultural model, because it was driving up yields through technology feedbacks. therefore commented the below line out
                'SAGTEC(R%) = CSng(SAGTEC(R%) * AMAX(1.0#, Mul ^ 0.8))
                'bbh 2020/12/10 end

                'dsr 2015/07/01 This block adjusts actual crop yields to reflect climate change
                'If RegName$(R%) = "Russia" AndAlso IY% <= 3 Then Stop
                'jrs 2009/12/09
                '        elasac(R%) = -0.02
                '        YL(R%) = YL(R%) * (1 + (sco2 - ico2) / ico2 * elasac(R%))
                'BBH 2009/12/10
                ' wv 2021/03/01 moved this function of env yield change downwards, so that it only affects rainfed yields, not overall yields
                'YL(R%) = YL(R%) * (1 + SENVYLCHG(R%) / 100)

                'sgh 2018/01/04 - start
                'need to calculate differential yields before an exogenous multiplier is applied to irrigated land
                IrrigatedCropland(R%) = IrrigatedPortionCropland(R%) * (Ildc(R%) * 1000)
                NonIrrigatedCropland(R%) = (Ildc(R%) * 1000) - IrrigatedCropland(R%)

                'bbh 2018/4/19 start replaced 2.35 with 3.25, Steve and I recalculated                
                NonIrrigatedYield(R%) = CSng((Ildc(R%) * 1000 * YL(R%)) / (NonIrrigatedCropland(R%) + (3.25 * IrrigatedCropland(R%))))
                IrrigatedYield(R%) = CSng(NonIrrigatedYield(R%) * 3.25)
                ' wv 2021/03/01 added the SENVYLCHG effect here to only affect the NonIrrigatedYield, and not the IrrigatedYield
                Dim NonIrrigatedYieldNoClimImp = NonIrrigatedYield(R%) 'bbh 2022/12/13 Use this variable below so as not to accumulate impact of SENVYLCHG across time
                NonIrrigatedYield(R%) = NonIrrigatedYield(R%) * (1 + SENVYLCHG(R%) / 100)
                'bbh 2019/04/02  Adding multiplier linked back to water price
                If watertoyieldswitch(R%) > 0.5 Then
                    IrrigatedYield(R%) = IrrigatedYield(R%) * SYlImpactWatPrice(R%)
                End If
                'bbh 2018/4/19 end replaced 2.35 with 3.25, Steve and I recalculated
                YL(R%) = ((slandirareaactual(R%) * IrrigatedYield(R%)) + (NonIrrigatedCropland(R%) * NonIrrigatedYield(R%))) /
            (Ildc(R%) * 1000)
                Dim YlNoClimImpFac = ((slandirareaactual(R%) * IrrigatedYield(R%)) + (NonIrrigatedCropland(R%) * NonIrrigatedYieldNoClimImp)) /
            (Ildc(R%) * 1000) / YL(R%) 'bbh 2022/12/13 Back-out factor used below to not accumulate impact of SENVYLCHG
                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation
                'sgh 2018/01/04 - END


                'dsr 2015/07/01 This block imposes minimum and maximum values on actual crop yields
                '        YL(R%) = AMAX(0.2 * byl(R%), Amin(YL(R%), 50))
                ' wv 2021/03/01 updated this block to limit maximum yield to 70, based on data
                YL(R%) = AMAX(0.2 * byl(R%), Amin(YL(R%), 70))
                'dsr 2015/06/29 Impose maximum yield for each country
                'wv 2020/12/16 agpy  limit it by yllim
                'bbh 2022/11/28 Start of code area changed subsantially to moderate year-to-year changes in yields by limiting annual change in % change
                YL(R%) = Amin(YL(R%), YLLim(R%)) 'bbh 2012/07/26
                If ylm(R%) >= 1 Then YL(R%) = CSng(AMAX(YL(R%), syl(R%) * 0.98)) 'bbh 2022/11/28 forces equilibration when stocks are high into trade, unless ylm bringing yl down
                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculationp
                If IY% = 2 Then
                    YlGrowth(R%) = (YL(R%) / ylm(R%)) / syl(R%) - 1 'Variable to save last year's growth rate in yield (can be decline)
                Else 'years beyond 2nd
                    Dim CurrentYlGR = (YL(R%) / ylm(R%)) / syl(R%) - 1
                    Dim YearToYearDiff = CurrentYlGR - YlGrowth(R%)
                    If YearToYearDiff < -0.01 Then 'Model trying to reduce growth rate by more than 1% relative to last year (YlGr)
                        YL(R%) = CSng(syl(R%) * (1 + YlGrowth(R%) - 0.01) * ylm(R%))
                    End If
                    If YearToYearDiff > 0.01 Then 'Model trying to increase growth rate by more than 1%
                        YL(R%) = CSng(syl(R%) * (1 + YlGrowth(R%) + 0.01) * ylm(R%))
                    End If
                    YlGrowth(R%) = (YL(R%) / ylm(R%)) / syl(R%) - 1  'Protect ability of ylm to break through the 1% growth rate barrier
                End If

                'YlShift(R%) = CSng(0.8 * YlShift(R%) + 0.2 * (byl(R%) - YL(R%) / ylm(R%))) 'bbh 2022/11/28
                YlShift(R%) = CSng(0.8 * YlShift(R%) + 0.2 * (byl(R%) - YL(R%) * YlNoClimImpFac / ylm(R%))) 'bbh 2022/12/13 To not accumulate impact of SENVYLCHG (replaced above line)
                'jrs 2023/10/23
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("YL", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                syl(R%) = YL(R%) / ylm(R%) 'bbh 2022/11/28
                'bbh 2022/11/28 End of code area changed subsantially to moderate year-to-year changes in yields by limiting annual change in % change
                'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" Then Stop 'bbh 2022/12/13 Important debugging line in YL calculation

                '
                '   All but First 1:  Agricultural Production
                '   -----------------------------------------
                'dsr 2015/06/09 CROP PRODUCTION
                'dsr 2015/06/22 Compute pre loss production
                'dkb 2021/06/08 added * (1 - ldcropunusedshare(R%))
                AGPppl(R%, 1) = YL(R%) * LD(R%, 1) * (1 - ldcropunusedshare(R%))
                'dsr 2015/06/22 Calculated crop production loss
                'kbn 2017/08/28 Bring in production loss code here for forecast years
                Dim aglossprodcropfunc = 0!
                'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Crop Production Losses (Linear fixed effect)", GDPPCP(R%), aglossprodcropfunc, context)
                Call XYTABL(TfNameCropProdLoss, GDPPCP(R%), aglossprodcropfunc, context)
                'jrs 2025/06/26
                aglossprodcropfunc = AMAX(aglossprodcropfunc, 2) 'allows negatives to be increased by a multiplier, like USA
                'jrs 2025/06/26 added * aglossprodm(R%, 5) to include Total multiplier
                Dim aglossprodcrop = Amin(AMAX(aglossprodcropfunc * aglossprodm(R%, 1) * aglossprodm(R%, 5), 2), 20) 'Losses capped here
                'kbn 2017/08/28 end
                'kbn 2017/09/13 Add multiplier below for crop production
                AGLOSSPROD(R%, 1) = AGPppl(R%, 1) * (aglossprodcrop / 100)
                'dsr 2015/06/22 Calculated post loss production
                AGP(R%, 1) = AGPppl(R%, 1) - AGLOSSPROD(R%, 1)

                '        SAGP(R%, 1) = AGP(R%, 1)

                'dsr 2015/06/09 MEAT PRODUCTION
                'dsr 2015/05/01 Lower minimum value from 0.001 to 0.000001
                'dsr 2015/06/09 Calculate animal production, adjusting for losses
                'dsr 2015/06/09 Adjust initial value of percentage loss by multiplier; no longer
                '        aglossprodmeat = Amin(99, AMAX(0, aglossprodperc(R%, 2) * aglossprodm(R%, 2)))
                'dsr 2015/06/22 Compute pre loss animalproduction
                'jrs 2026/08/07 slr is now country specific
                Dim AnimalProd = AMAX(0.000001, LVHERD(R%) * slr(R%))
                '        AGP(R%, 2) = AMAX(0.000001, LVHERD(R%) * slr) ' + FISH(r%)
                'jrs 2014/12/03 Calculate pre loss production of meat, non-animal, products based on initial ratio
                AGPMILKEGGSppl(R%) = MILKEGGSTOMEATI(R%) * AnimalProd
                'jrs 2014/12/03 add to animal products to get total pre loss meat production
                AGPppl(R%, 2) = AnimalProd + AGPMILKEGGSppl(R%)
                'dsr 2015/06/22 Calculated meat production loss
                ' kbn 2017/09/13 No meat data available here. Set aglossmeat at 10% so that value of multiplier can be maintained below
                'jrs 2025/06/26 added  * aglossprodm(R%, 5) for Total
                Dim aglossmeatperc = Amin(AMAX(10 * aglossprodm(R%, 2) * aglossprodm(R%, 5), 2), 20)
                AGLOSSPROD(R%, 2) = AGPppl(R%, 2) * (aglossmeatperc / 100)
                'dsr 2015/06/22 Calculated post loss production of total meat and non-animals
                AGP(R%, 2) = AGPppl(R%, 2) - AGLOSSPROD(R%, 2)
                AGPMILKEGGS(R%) = AGPMILKEGGSppl(R%) * (1 - (aglossmeatperc / 100))
                'dsr 2015/06/09 Save total pre loss meat production as meat production potential for later use
                '        agpp(R%, 2) = AGPppl(R%, 2)
                '        SAGP(R%, 2) = AGP(R%, 2)

                'dsr 2015/06/09 FISH PRODUCTION
                'dsr 2015/06/09   AQUACULTURE
                'dsr 2015/06/09 Have growth rate decline from initial value to 0 over aquaculconv years
                Dim aquaculgrcomp = ConvergeOverTime(aquaculgr(R%) / 100, 0, aquaculconv)
                'bbh 2013/01/29  Vital Signs 2012: 72 said rate in 2009 was 4.5%
                'Aquacul now needs more extended model also representing feed to it
                'dsr 2015/06/22 Calculate pre loss aquaculture production, considering multiplier
                AQUACULppl(R%) = Saquacul(R%) * (1 + aquaculgrcomp) * aquaculm(R%)
                'dsr 2015/06/22 Calculate aquaculture production loss
                'dsr 2015/12/12 Change in next two lines needed with conversion of AGLOSSPROD(R%, 3)
                'to sum of loss in aquaculture and catch
                'AGLOSSPROD(R%, 3) = AQUACULppl(R%) * (aglossprodperc(R%, 3) / 100)
                'kbn 2017/08/28 Bring in production loss code for fish here for the first year
                Dim aglossfishfunc = 0!
                'jrs 2024/06/11 
                'Call XYTABL("GDP/Capita (PPP 2011) Versus Fish Production Losses (Log)", GDPPCP(R%), aglossfishfunc, context)
                Call XYTABL(TFNameFishProdLoss, GDPPCP(R%), aglossfishfunc, context)
                'jrs 2025/06/26 added  * aglossprodm(R%, 5) for Total
                Dim aglossaquacul = Amin(AMAX(aglossfishfunc, 2) * aglossprodm(R%, 3) * aglossprodm(R%, 5), 20) 'Losses capped here
                'jrs 2025/06/26 added  * aglossprodm(R%, 5) for Total
                Dim aglossfish = Amin(AMAX(aglossfishfunc * aglossprodm(R%, 4) * aglossprodm(R%, 5), 2), 20) 'Losses capped here
                Dim aquaculloss = AQUACULppl(R%) * (aglossfish / 100)
                'dsr 2015/06/22 Calculate post loss aquaculture production
                'AQUACUL(R%) = AQUACULppl(R%) - AGLOSSPROD(R%, 3)
                AQUACUL(R%) = AQUACULppl(R%) - aquaculloss

                'dsr 2015/06/09   WILD CATCH
                'dsr 2015/06/09 Calculate wild catch, adjusting for losses
                'dsr 2015/06/09 Adjust initial value of percentage loss by multiplier
                'dsr 2014/12/21 Use absolute catch
                'dsr 2015/06/09 Let FISH just represent caught fish
                '        FISH(R%) = rfssh(R%) * (1 / AMAX(0.5, aglossprodm(R%, 3))) + AQUACUL(R%)
                '        AGP(R%, 3) = FISH(R%)
                'dsr 2015/06/22 For now keep pre loss catch constant over time
                'dsr 2015/11/02 Added multiplier on fishcatch
                AGFISHCATCHppl(R%) = AgFishCatchI(R%) * fishcatchm(R%)
                'dsr 2015/06/09 Calculated fish catch production loss
                'dsr 2015/12/12 Change in next two lines needed with conversion of AGLOSSPROD(R%, 3)
                'to sum of loss in aquaculture and catch
                'AGLOSSPROD(R%, 4) = AGFISHCATCHppl(R%) * (aglossprodperc(R%, 4) / 100)
                'kbn 2017/08/28 Bring in catchloss here for forecast years
                'kbn 2017/08/28 end
                Dim catchloss = AGFISHCATCHppl(R%) * (aglossfish / 100)
                'dsr 2015/06/22 Calculate post loss fish catch
                'AGFISHCATCH(R%) = AGFISHCATCHppl(R%) - AGLOSSPROD(R%, 4)
                AGFISHCATCH(R%) = AGFISHCATCHppl(R%) - catchloss

                'dsr 2015/06/22   Sum aquaculture and wild catch for total fish production
                AGPppl(R%, 3) = AGFISHCATCHppl(R%) + AQUACULppl(R%)
                AGP(R%, 3) = AGFISHCATCH(R%) + AQUACUL(R%)
                'jrs 2023/11/10
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("AGP", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                'dsr 2015/12/12 Add line to calculate AGLOSSPROD(R%, 3)
                AGLOSSPROD(R%, 3) = aquaculloss + catchloss
                '        SAGP(R%, 3) = AGP(R%, 3)
                'dsr 2015/06/22 Save total pre loss fish production as fish production potential for later use
                '        agpp(R%, 3) = AGPppl(R%, 3)
                'dsr 2015/05/06 For next year, take out general multiplier to avoid compounding effects
                Saquacul(R%) = AQUACULppl(R%) / AMAX(0.01, aquaculm(R%))
                ' If RegName$(R%) = "China" Then Stop

                'dsr 2015/06/09 Convert post-loss production in quantities to post-loss production in monetary values
                'If R = 79 And IY = 18 Then Stop
                '        ZS(R%, 1) = ZSF(R%) * (AGP(R%, 1) * swapi(1) _
                '+ AGP(R%, 2) * swapi(2) _
                '+ AGP(R%, 3) * swapi(3))
                '        ZS(R%, 1) = ZSF(R%) * (AGPppl(R%, 1) * swapi(1) _
                '+ AGPppl(R%, 2) * swapi(2) _
                '+ AGPppl(R%, 3) * swapi(3)) 'bbh 2021/09/30 AGP inflates AG VADD growth too much; tried AGPppl, but went to YL below
                ZS(R%, 1) = ZSF(R%) * (YL(R%) * LDI(R%, 1)) * swapi(1) 'bbh 2021/09/30 AGP inflates AG VADD growth too much


                '   All but First 1:  Water Use
                '   ---------------------------
                'dsr 2015/06/15 Does not make sense to subtract production losses here
                Dim CropPC = AGP(R%, 1) / POP(R%)
                Dim WComp = 0!
                Dim WComp2 = 0!
                Call XYTABL("Crop Production/Capita Versus Water Use/Capita", CropPC, WComp, context)
                Call XYTABL("Crop Production/Capita Versus Water Use/Capita", CropPCI(R%), WComp2, context)
                WATUSEPC(R%) = WatUsePCI(R%) * WComp / WComp2
                WATUSE(R%) = WATUSEPC(R%) * POP(R%)
            Next R%
            '   All but First 1:  World Price Computation
            '   -----------------------------------

            For f% = 1 To 3
                Dim TAGP = 0!
                Dim TFPRI = 0!
                For R% = 1 To NR%
                    'jrs 2007/01/12 no double counting
                    If RegiontoSum(R%) Then
                        'dsr 2015/06/15 Use post-loss production
                        TAGP = TAGP + AGP(R%, f%)
                        '            TFPRI = TFPRI + FPRI(r%, f%) * EXRATE(r%) * AGP(r%, f%)
                        'dsr 2015/06/29 Simplify since same equation now used for all commodities
                        '                If f% < 3 Then
                        '                  TFPRI = TFPRI + FPRI(R%, f%) * AGP(R%, f%)
                        '                Else
                        '                  TFPRI = TFPRI + FPRI(R%, 3) * AGP(R%, f%)
                        '                End If '(if f% < 3)
                        TFPRI = TFPRI + FPRI(R%, f%) * AGP(R%, f%)
                    End If
                Next R%
                WAP(f%) = TFPRI / TAGP
            Next f%
        End Sub
        Public Sub IfsAg2AllbutFirstYear(context As IForecastContext)
            'SecondCall:
            Dim Diff1 = 0!
            Dim Diff2 = 0!
            Dim Mul = 0!
            For R% = 1 To NR%
                'dsr 2015/03/26 Comment out following since Feed Demand moved to after Food Demand
                '   All but First 2:  Loss
                '   ----------------------

                ''        Call XYTABL("GDP/Capita Versus Agricultural Loss", GDPPC(R%), lcomp)
                ''        Call XYTABL("GDP/Capita Versus Agricultural Loss", GDPPCI(R%), LComp2)
                ''        LossMul = lcomp / AMAX(0.01, LComp2)
                ''        For f% = 1 To 2
                ''            LOSS(R%, f%) = LOSSI(R%, f%) * LossMul * lossm(R%)
                ''            LOSS(R%, f%) = AMAX(0, Amin(LOSS(R%, f%), 0.8))
                ''        Next f%
                ''        LOSS(R%, 3) = LOSSI(R%, 3)

                'dsr 2015/03/26 Comment out following since Feed Demand moved to after Food Demand
                '   All but First 2:  Agricultural Demand - Feed
                '   --------------------------------------------

                '        GLFeedEq = LD(r%, 2) * GLDCAP(r%) * (FPRI(r%, 1) / SWAPI(1)) ^ elglinpr
                ''        GLFeedEq = LD(R%, 2) * GLDCAP(R%) * (WAP(1) / swapi(1)) ^ elglinpr
                ''        GLFeedEq = AMAX(GLFeedEq, LD(R%, 2) * GLDCAP(R%))
                ''        Call XYTABL("GDP/Capita (PPP) Versus Feed Requirements", GDPPCP(R%), fedreq(R%))
                ''        FEDDEM(R%, 1) = (LVHERD(R%) * fedreq(R%) * fedreqm(R%) _
                ''            * AMAX(0.5, (1 - livhdpro / 100) ^ (IY% - 1)) - GLFeedEq) * agdemm(R%, 1)

                ' If RegName$(R%) = "Somalia" Then Stop

                ''        If FEDDEM(R%, 1) < 0 Then FEDDEM(R%, 1) = 0

                'dsr 2015/03/26 Comment out following since Feed Demand moved to after Food Demand
                '   All but First 2:  Agricultural Demand - Industrial
                '   --------------------------------------------------

                ''        Call XYTABL("GDP/Capita (PPP) Versus Industrial Ag Demand/Capita (2010)", _
                ''            GDPPCP(R%), DComp)
                ''        INDEM(R%, 1) = INDEMK(R%) * DComp * POP(R%) * agdemm(R%, 1)
                ''        FoodforFuel = ConvergeOverTime(elagind, 0, 50)
                'Function takes use up heavily over time;
                '   technolgy like cellulosic fiber should phase out
                'Base, Target, Years
                ''        INDEM(R%, 1) = INDEM(R%, 1) * (1 + (WEP / IWEP - 1) * FoodforFuel)
                ''        INDEM(R%, 1) = INDEM(R%, 1) * (WAP(1) / swapi(1)) ^ elascd(R%) '-.15 on 2013/01/28
                'Indem has no crop price elasticity; consider adding
                ''        IndemCapperPop = 2 'ConvergeOverTime(0.17, 0.12, 50) DSR 2015/01/29 Relax this given change in how INDEM estimated
                'Denmark is highest in 2010 at 0.16
                ''        If INDEM(R%, 1) / POP(R%) > IndemCapperPop Then INDEM(R%, 1) = IndemCapperPop * POP(R%)

                '   All but First 2:  Agricultural Demand - Calories and Food Demand
                '   ------------------------------------------------------

                'dsr 2015/03/18 Begin with Total Calorie per Capita Demand
                ' dsr 2015/03/18 Calculate predicted total calorie per capita demand
                'bbh 2019/02/27 adjust GDPPCP fo changing share of unskilled income in GDP start
                If IY% = 2 Then
                    'jrs 2024/01/29 HHShrGDPI(R%) = HHINC(R%, 1) / GDP(R%)
                    HHShrGDPI(R%) = HHINCDIS(R%, 1) / GDP(R%)
                End If
                'jrs 2024/01/29 Dim GDPPCPIncAdj = (HHINC(R%, 1) / GDP(R%)) / HHShrGDPI(R%)
                'If (R = 32 Or R = 160) And IY > 3 Then Stop 'first HHINCDIS, but also HHShrGDPI
                Dim GDPPCPIncAdj = (HHINCDIS(R%, 1) / GDP(R%)) / HHShrGDPI(R%)
                Dim CalPerCap = 0!
                Dim CalPerCap2 = 0!
                If context.HistoricMode Then
                    Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1961)",
                    GDPPCP(R%) * GDPPCPIncAdj, CalPerCap, context)
                    'jrs 2014/12/03 Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1999)", _
                    'GDPPCP(R%), CalPerCap2)
                    'DSR 2014/12/10 Update equation estimating Total Calorie Demand
                    'Call AnalFunc("Total Calories per Capita per Day (Time in logic)", Array(GDPPCP(R%), POPURBAN(R%) / POP(R%) * 100), CalPerCap2, Array("GDPPCP", "POPURBAN%"))
                    Call XYTABL("GDP/Capita (PPP 2011) Versus Calorie Demand (fixed-effect)",
                    GDPPCP(R%) * GDPPCPIncAdj, CalPerCap2, context)
                    'jrs 2014/12/03 CalperCap = CalperCap + _
                    '(IY% - 1) / 40 * (CalperCap2 - CalperCap)
                    'bbh 2020/04/07 start
                    'CalPerCap = CalPerCap + _
                    '    (IY% - 1) / (FirstYearForecast - FirstYearHistory) * (CalPerCap2 - CalPerCap)
                    If context.LastYearHistory > context.FirstYearHistory Then  'should always be but do not risk division by 0
                        CalPerCap = CSng(CalPerCap +
                        (IY% - 1) / (context.LastYearHistory - context.FirstYearHistory) * (CalPerCap2 - CalPerCap))
                        'Was a significant shift of functon over time of calorie consumption at same GDPPCP levels
                        '    Surprisingly, PPP 2011 version is actually below 1961 version as GDPPCP rises
                    End If 'Else CapPerCap is unchanged from early year functional value
                    'bbh 2020/04/07 end

                Else
                    'DSR 2014/12/10 Update equation estimating Total Calorie Demand
                    'Call XYTABL("GDP/Capita (PPP) Versus Calorie Demand (1999)", _
                    'GDPPCP(R%), CalPerCap)
                    'jrs 2014/12/03 Call AnalFunc("Total Calories per Capita per Day (Time in logic)", Array(GDPPCP(R%), POPURBAN(R%) / POP(R%) * 100), CalperCap, Array("GDPPCP", "POPURBAN%"))
                    'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Calorie Demand (fixed-effect)",
                    'GDPPCP(R%) * GDPPCPIncAdj, CalPerCap, context)                    
                    Call XYTABL(TFCalDem, GDPPCP(R%) * GDPPCPIncAdj, CalPerCap, context)
                End If
                'bbh 2019/02/27 adjust GDPPCP fo changing share of unskilled income in GDP end
                'TimeFac = Amin(IY%, agconv) 'Gradually bring demand to predicted value
                'AdjustforInitialDevC = (calactpredrat(R%) * (agconv - TimeFac) _
                '+ 1 * TimeFac) / agconv

                '  dsr 2015/03/18 Adjust for initial shift factor and convergence to function; also
                '                 keep value below a maximum
                Dim adjustforinitialdevc = ConvergeOverTime(calactpredrat(R%), 1, agconv)
                'Base, Target, Years
                CalPerCap = Amin(calmax, CalPerCap)
                '  dsr 2015/03/18 Add in non-income related temporal shift
                '   BBH 7/24/04 Begin V5_02
                Dim calbase = Amin(3000, calmax - 300)
                'jrs 2014/12/03 caldgr(R%) = caldgr(R%) + _
                '4 * (calmax - AMAX(calbase, Amin(calmax, CalperCap))) / (calmax - calbase)
                caldgr(R%) = CSng(caldgr(R%) +
            7.638 * (calmax - AMAX(calbase, Amin(calmax, CalPerCap))) / (calmax - calbase))
                'Comparative cross sections show about 4 calories per year growth,
                'independent of income
                '  dsr 2015/03/18  New lines to replace caldem line below and prepare for separating calorie demand
                '                 into crops, meat, and fish; also ensure total calories do not exceed maximum
                '  dsr 2015/03/26 Change location of parenthesis so TotalCalPerCap does not exceed maximum even
                '                 after accounting for adjustforinitialdevc
                '        TotalCalPerCap = Amin(calmax, CalPerCap + caldgr(R%)) * adjustforinitialdevc
                'If (R = 32 Or R = 160) And IY > 3 Then Stop
                Dim TotalCalPerCap = Amin(calmax, (CalPerCap + caldgr(R%)) * adjustforinitialdevc)
                Dim caldem = TotalCalPerCap * POP(R%)
                '        caldem = (Amin(calmax, CalPerCap + caldgr(R%))) * POP(R%) * adjustforinitialdevc

                'dsr 2015/03/18 Separate Total Calorie per Capita Demand into Crops, Meat, and Fish **** start
                'dsr 2015/03/18 Compute predicted share of calories from meat and fish
                Dim MeatAndFishPctPred = 0!
                'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus CLPC from MeatandFish (2010) Log", GDPPCP(R%), MeatAndFishPctPred, context)
                Call XYTABL(TfNameCLMeat, GDPPCP(R%), MeatAndFishPctPred, context)
                'jrs 2026/02/10
                'don't allow drops that are more than 10% in Exogenous years
                If gdprext(R%) = -100 Then
                    'MeatAndFishPctPred = CSng(AMAX(Amin(MeatAndFishPctPred, SMeatAndFishPctPred(R%) * 1.05), SMeatAndFishPctPred(R%) * 0.95))
                End If
                SMeatAndFishPctPred(R%) = MeatAndFishPctPred 'save for use in next year to limit jumps

                'dsr 2015/03/18 Let actual share of calories from meat and fish reflect initial shift factor and
                '    convergence to predicted function except in predominantly Hindu countries, where no convergence
                Dim adjustforinitialdevm = 0!
                If CULTREG(R%) = 6 Then
                    adjustforinitialdevm = MeatAndFishActPredRat(R%)
                Else
                    adjustforinitialdevm = ConvergeOverTime(MeatAndFishActPredRat(R%), 1, agconv)
                End If
                Dim MeatAndFishPctAct = MeatAndFishPctPred * adjustforinitialdevm
                'dsr 2015/03/18 Keep actual share of calories from meat and fish within lower and upper bounds
                '    and convert from percentage to fraction
                MeatAndFishPctAct = CSng(Amin(AMAX(MeatAndFishPctAct / 100, 0.035), 1))
                'If (R = 32 Or R = 160) And IY > 3 Then Stop
                'dsr 2015/03/18 Now divide calorie demand into crops, meat, and fish
                CLPC(R%, 2) = TotalCalPerCap * MeatAndFishPctAct * MeatToMeatFishRatI(R%)
                CLPC(R%, 3) = TotalCalPerCap * MeatAndFishPctAct * (1 - MeatToMeatFishRatI(R%))
                CLPC(R%, 1) = TotalCalPerCap - CLPC(R%, 2) - CLPC(R%, 3)

                'bbh 2018/03/10 start adjust for faster historical growth of fish production/consumption
                CLPC(R%, 2) = CLPC(R%, 2) - SMeatToMeatFishShift(R%)
                CLPC(R%, 3) = CLPC(R%, 3) + SMeatToMeatFishShift(R%)
                Dim FishMeatCLPCGrDiffAdj = ConvergeOverTime(FishMeatCLPCGrDiff, 0, 50)
                SMeatToMeatFishShift(R%) = SMeatToMeatFishShift(R%) + CLPC(R%, 3) * FishMeatCLPCGrDiffAdj / 100
                'bbh 2018/03/10 end

                'dsr 2015/07/01 Now take into account price effects and recalculate total calorie demand
                'dsr 2015/07/01 May want to revisit used of multiplier in next line

                'sgh 2016/02/15 -START- No longer use clpcm here. Use that below AFTER eliminate hunger parameters are applied
                CLPC(R%, 1) = CSng(CLPC(R%, 1) * (WAP(1) / swapi(1)) ^ elascd(R%)) '* agdemm(R%, 1) 'bbh 2016/02/01
                CLPC(R%, 2) = CSng(CLPC(R%, 2) * (WAP(2) / swapi(2)) ^ elasmd(R%))  'bbh 2016/02/01
                'dsr 2015/06/08 Use newly added parameter for price elasticity of fish demand
                '        CLPC(R%, 3) = CLPC(R%, 3) * (WAP(3) / swapi(3)) ^ (-0.3) 'elasmd(R%)  which is -.3
                CLPC(R%, 3) = CSng(CLPC(R%, 3) * (WAP(3) / swapi(3)) ^ elasfd(R%)) 'bbh 2016/02/01
                TotalCalPerCap = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)
                'sgh 2016/02/15 -END-

                'sgh 2016/02/14 -START- Apply the eliminate hunger parameters here. This increases CLPC to the required
                'level, specific to each country, to eliminate (5 percent) hunger. If MALNPOPP does not decrease to 5 percent
                'by the target year, then adajust malnconv in the scenario tree as well.
                If malnelimstartyr(R%) <> 0 AndAlso malnelimtargetyr(R%) <> 0 AndAlso malnelimstartyr(R%) < malnelimtargetyr(R%) Then
                    'If RegName$(R%) = "Tanzania" Then Stop
                    Dim clcgr = 0!
                    Dim clpcgr = 0!
                    If (IY% + BaseYear - 1) >= malnelimstartyr(R%) AndAlso (IY% + BaseYear - 1) <= malnelimtargetyr(R%) Then
                        'jrs 2024/01/29 Dim malnelimtarget = Math.Exp((Math.Log(5) - 46.95226 + 0.18422 * Math.Log(HHINC(R%, 1) / labsups(R%))) / -5.643)
                        Dim malnelimtarget = Math.Exp((Math.Log(5) - 46.95226 + 0.18422 * Math.Log(HHINCDIS(R%, 1) / labsups(R%))) / -5.643)
                        Dim timeToTarget = malnelimtargetyr(R%) + 1 - (BaseYear + IY% - 1)
                        clpcgr = CSng((malnelimtarget / (TotalCalPerCap * CLPCCUM(R%))) ^
                    (1 / timeToTarget))
                        If TotalCalPerCap > malnelimtarget Then
                            CLPCCUM(R%) = 1
                        Else
                            CLPCCUM(R%) = CLPCCUM(R%) * clpcgr
                        End If
                    Else
                        clcgr = 1
                    End If
                End If

                'bbh 2016/02/17 start
                Dim CalTarSupCrop = 0!
                Dim CalTarSupMeat = 0!
                Dim CalTarSupFish = 0!
                If malelimprecisesw(R%) = 1 Then
                    Dim CalDefPC = AMAX(0, (Amin(55, SMalnPopP(R%)) - 5)) / 50 * 422
                    '1844 not malnourished person; 1000=worst maln and still alive; assume normal distrib; so 422=(1844-1000)/2
                    '55% maln generates 422 calorie shortage per undernourished person; 5% maln generates 0 calorie shortage
                    Dim MalnPop = SMalnPopP(R%) / 100 * POP(R%)
                    Dim CalTarSup = MalnPop * CalDefPC / POP(R%)
                    CalTarSupCrop = CalTarSup * CLPC(R%, 1) / TotalCalPerCap
                    CalTarSupMeat = CalTarSup * CLPC(R%, 2) / TotalCalPerCap
                    CalTarSupFish = CalTarSup * CLPC(R%, 3) / TotalCalPerCap
                Else
                    CalTarSupCrop = 0
                    CalTarSupMeat = 0
                    CalTarSupFish = 0
                End If
                'If RegName$(R%) = "Comoros" Then Stop
                'sgh 2016/02/14 -END-

                'bbh 2023/06/11 start Handle clpcm impacts before hunger elimination impacts
                '  Not clear to me why sgh comment earlier said to handle clpcm "AFTER eliminate hunger parameters are applied", and then put both in same
                '     code below that I commented out below to break them up and put clpcm first
                CLPC(R%, 1) = CLPC(R%, 1) * clpcm(R%, 1)
                CLPC(R%, 2) = CLPC(R%, 2) * clpcm(R%, 2)
                CLPC(R%, 3) = CLPC(R%, 3) * clpcm(R%, 3)
                Dim TotalCalPerCapRev = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)
                CLPC(R%, 1) = CLPC(R%, 1) * TotalCalPerCap / TotalCalPerCapRev '3 lines ofnormalization to total, allowing food-type shifts
                CLPC(R%, 2) = CLPC(R%, 2) * TotalCalPerCap / TotalCalPerCapRev
                CLPC(R%, 3) = CLPC(R%, 3) * TotalCalPerCap / TotalCalPerCapRev
                'Jose, please activate lines below when clpcm has food type 4 (Total) in common and Total values all countries are 1 in base case
                If clpcm(R%, 4) <> 1 Then
                    CLPC(R%, 1) = CLPC(R%, 1) * clpcm(R%, 4)
                    CLPC(R%, 2) = CLPC(R%, 2) * clpcm(R%, 4)
                    CLPC(R%, 3) = CLPC(R%, 3) * clpcm(R%, 4)
                End If

                'sgh 2016/02/15 -START- Use CLPCCUM as a multiplier. This is the cumulative increase in CLPC when using the
                'eliminate hunger parameters
                'bbh 2023/06/11 Removed clpcm multipliers from code of sgh (Steve Hedden)
                CLPC(R%, 1) = CLPC(R%, 1) * CLPCCUM(R%) + CalTarSupCrop '* agdemm(R%, 1) 'bbh 2016/02/01
                CLPC(R%, 2) = CLPC(R%, 2) * CLPCCUM(R%) + CalTarSupMeat  'bbh 2016/02/01
                'dsr 2015/06/08 Use newly added parameter for price elasticity of fish demand
                '        CLPC(R%, 3) = CLPC(R%, 3) * (WAP(3) / swapi(3)) ^ (-0.3) 'elasmd(R%)  which is -.3
                CLPC(R%, 3) = CLPC(R%, 3) * CLPCCUM(R%) + CalTarSupFish   'bbh 2016/02/01
                TotalCalPerCap = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)

                ''sgh 2016/02/15 -START- Use CLPCCUM as a multiplier. This is the cumulative increase in CLPC when using the
                ''eliminate hunger parameters
                '        CLPC(R%, 1) = CLPC(R%, 1) * clpcm(R%, 1) * CLPCCUM(R%) + CalTarSupCrop '* agdemm(R%, 1) 'bbh 2016/02/01
                '        CLPC(R%, 2) = CLPC(R%, 2) * clpcm(R%, 2) * CLPCCUM(R%) + CalTarSupMeat  'bbh 2016/02/01
                ''dsr 2015/06/08 Use newly added parameter for price elasticity of fish demand
                ''        CLPC(R%, 3) = CLPC(R%, 3) * (WAP(3) / swapi(3)) ^ (-0.3) 'elasmd(R%)  which is -.3
                '        CLPC(R%, 3) = CLPC(R%, 3) * clpcm(R%, 3) * CLPCCUM(R%) + CalTarSupFish  'bbh 2016/02/01
                '        TotalCalPerCap = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)
                ''sgh 2016/02/15 -END-
                'bbh 2023/06/11 end Handle clpcm impacts before hunger elimination impacts

                'dsr 2015/03/18 Separate Total Calorie per Capita Demand into Crops, Meat, and Fish **** end

                'jrs 2019/05/03 *** BEGIN All but First ***
                If clpcexovals(R%, 1) > -100 Then
                    CLPC(R%, 1) = clpcexovals(R%, 1)
                End If
                If clpcexovals(R%, 2) > -100 Then
                    CLPC(R%, 2) = clpcexovals(R%, 2)
                End If
                If clpcexovals(R%, 3) > -100 Then
                    CLPC(R%, 3) = clpcexovals(R%, 3)
                End If
                If clpcexovals(R%, 4) > -100 Then
                    CLPC(R%, 4) = clpcexovals(R%, 4)
                Else
                    If clpcexovals(R%, 1) > -100 OrElse clpcexovals(R%, 2) > -100 OrElse clpcexovals(R%, 3) > -100 Then
                        CLPC(R%, 4) = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)
                    End If
                End If
                'jrs 2019/05/03 *** END ***
                'dsr 2015/03/18 Convert Daily per Capita Calorie Demand to Initial Estimate of Annual Total Food Demand **** start
                For f% = 1 To 3
                    'dsr 2015/06/15 Decreases in waste from first year lead to fewer grams needed per calorie. Since
                    '    data used to calculate initial ratio of grams to calories accounted for initial waste rate
                    '    adjust with changes from first year.
                    'kbn 2017/08/28 Recompute consumption losses here
                    Dim aglossconspercpred = 0!
                    'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Consumption Losses (Log)", GDPPCP(R%), aglossconspercpred, context)
                    Call XYTABL(TfNameConsLoss, GDPPCP(R%), aglossconspercpred, context)
                    'kbn 2017/08/28 end
                    'bbh 2017/09/11 start; fixed bug in comp of GRAMSPC
                    'kbn 2017/09/13 add multipliers below
                    Dim aglossconsperc = Amin(AMAX(aglossconspercpred * aglossconsm(R%, f%), 5), 18)
                    '         GRAMSPC(R%, f%) = GramsToCalRatI(R%, f%) * CLPC(R%, f%) * (aglossconsperc / aglossconsperci(R%, f%))
                    GRAMSPC(R%, f%) = GramsToCalRatI(R%, f%) * CLPC(R%, f%) *
                    (1 - aglossconsperci(R%, f%) / 100) / (1 - aglossconsperc / 100)
                    'If (R = 32 Or R = 160) And IY > 3 And f% = 2 Then Stop
                    'bbh 2017/09/11 end
                    FDEM(R%, f%) = GRAMSPC(R%, f%) * POP(R%) * 365 / 1000000
                Next f%
                'jrs 2023/12/01
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("FDEM", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                ' dsr 2015/03/18 Convert Daily per Capita Calorie Demand to Initial Estimate of Annual Total Food Demand **** end

                ' dsr 2015/03/18 Move estimates of Loss in transit, Feed Demand, and Industrial Demand to after
                '                estimates of calorie and food demand. Also, add code for feed and industrial
                '                demand for meat and fish, as well as food manufacturing demand for all **** end

                'dsr 2015/03/26 New Loss in Transit Code **** start
                '    Based on actual data, so a a bit different from production and consumption loss
                '   All but First 2:  Loss in Transit
                '   ----------------------
                Dim predaglosstransI = 0!
                Dim predaglosstrans = 0!
                For f% = 1 To 3
                    If f% = 1 Then 'Decreases with increasing GDP/Capita for crops
                        'jrs 2024/06/17 Call XYTABL("GDP/Capita (PPP 2011) Versus Waste to Food Ratio Crops (2011) Log", GDPPCPI(R%), predaglosstransI, context)
                        Call XYTABL(TFNameWasteFoodCrop, GDPPCPI(R%), predaglosstransI, context)
                        'Call XYTABL("GDP/Capita (PPP 2011) Versus Waste to Food Ratio Crops (2011) Log", GDPPCP(R%), predaglosstrans, context)
                        Call XYTABL(TFNameWasteFoodCrop, GDPPCP(R%), predaglosstrans, context)
                    ElseIf f% = 2 Then 'Decreases with increasing GDP/Capita for meat
                        'jrs 2024/06/17 Call XYTABL("GDP/Capita (PPP 2011) Versus Waste to Food Ratio Meat (2011) Log", GDPPCPI(R%), predaglosstransI, context)
                        Call XYTABL(TFNameWasteFoodMeat, GDPPCPI(R%), predaglosstransI, context)
                        'jrs 2024/06/17 Call XYTABL("GDP/Capita (PPP 2011) Versus Waste to Food Ratio Meat (2011) Log", GDPPCP(R%), predaglosstrans, context)
                        Call XYTABL(TFNameWasteFoodMeat, GDPPCP(R%), predaglosstrans, context)
                    Else 'No clear pattern for fish
                        predaglosstransI = 1
                        predaglosstrans = 1
                    End If
                    'dsr 2015/05/06 Add aglosstransm to following equation; values less than 1 will reduce intermediate loss
                    AGLOSSTRANS(R%, f%) = AgLossTransToFoodRatI(R%, f%) * FDEM(R%, f%) * (predaglosstrans / predaglosstransI) * aglosstransm(R%, f%)
                Next f%
                'dsr 2015/03/26 New Loss in Transit Code **** end
                '   All but First 2:  Agricultural Demand - Feed
                '   --------------------------------------------
                'dsr 2015/07/01 Crop feed demand for livestock production; makes up for what cannot get from grazing
                Dim GLFeedEq = LD(R%, 2) * GLDCAP(R%) * (WAP(1) / swapi(1)) ^ elglinpr
                GLFeedEq = AMAX(GLFeedEq, LD(R%, 2) * GLDCAP(R%))
                Call XYTABL("GDP/Capita (PPP) Versus Feed Requirements", GDPPCP(R%), fedreq(R%), context)
                FEDDEM(R%, 1) = CSng((LVHERD(R%) * fedreq(R%) * fedreqm(R%) _
            * AMAX(0.5, (1 - livhdpro / 100) ^ (IY% - 1)) - GLFeedEq) * feddemm(R%)) '* agdemm(R%, 1) 'bbh 2016/02/01
                'If (GRPS(1, R%) = "Sudan South" Or GRPS(1, R%) = "Chad") And IY > 5 Then Stop
                If FEDDEM(R%, 1) < 0 Then FEDDEM(R%, 1) = 0
                'dsr 2015/03/18 Basic code for feed demand from meat and fish. For now assume
                '               constant ratio to FDEM
                For f% = 2 To 3
                    FEDDEM(R%, f%) = FeedToFoodRatI(R%, f%) * FDEM(R%, f%)
                Next f%

                '   All but First 2:  Agricultural Demand - Industrial
                '   --------------------------------------------------
                Dim DComp = 0!
                'jrs 2024/06/19 Call XYTABL("GDP/Capita (PPP) Versus Industrial Ag Demand/Capita (2010)",
                'GDPPCP(R%), DComp, context)
                Call XYTABL(TFNameIndDemPC, GDPPCP(R%), DComp, context)
                'dsr 2015/07/01 May want to revisit use of adgemm in next equation
                INDEM(R%, 1) = INDEMK(R%) * DComp * POP(R%) * indemm(R%) '* agdemm(R%, 1) 'bbh 2016/02/01
                'If RegName$(R%) = "United States of America" Then Stop
                Dim FoodforFuel = ConvergeOverTime(elagind, 0, 50)
                'Function takes use up heavily over time;
                '   technolgy like cellulosic fiber should phase out
                'Base, Target, Years
                INDEM(R%, 1) = INDEM(R%, 1) * (1 + (WEP / iwep - 1) * FoodforFuel)
                INDEM(R%, 1) = CSng(INDEM(R%, 1) * (WAP(1) / swapi(1)) ^ elascd(R%)) '-.15 on 2013/01/28
                'Indem has no crop price elasticity; consider adding
                Dim IndemCapperPop = 2 'ConvergeOverTime(0.17, 0.12, 50) DSR 2015/01/29 Relax this given change in how INDEM estimated
                'Denmark is highest in 2010 at 0.16
                If INDEM(R%, 1) / POP(R%) > IndemCapperPop Then INDEM(R%, 1) = IndemCapperPop * POP(R%)
                'dsr 2015/03/18 Basic code for industrial demand from meat and fish. For now assume
                '               constant ratio to FDEM
                For f% = 2 To 3
                    INDEM(R%, f%) = IndToFoodRatI(R%, f%) * FDEM(R%, f%)
                Next f%

                'dsr 2015/03/18 Basic code for food manufacturing demand. For now assume
                '               constant ratio to FDEM for all categories. Soon will need to change to equation for crops
                '   All but First 2:  Agricultural Demand - Food Manufacturing
                '   --------------------------------------------------
                For f% = 1 To 3
                    FMDEM(R%, f%) = FManToFoodRatI(R%, f%) * FDEM(R%, f%)
                Next f%

                'dsr 2015/03/18 Can comment out code below when activate new code **** start
                '   BBH 7/24/04 end V5_02

                'dsr 2015/03/25 comment out following code since values calculated above (use '')
                '   All but First 2:  Agricultural Demand - Meat and Calories from Meat
                '   --------------------------------------------------------------------------
                'dsr 2015/01/07
                ''        Call XYTABL("GDP/Capita (PPP) Versus Meat Demand (2010)", GDPPCP(R%), tonspercap)
                ''        tonspercap = Amin(meatmaxr(R%), tonspercap)
                'If RegName$(R%) = "Comoros" AndAlso IY% > 31 Then Stop

                ''        If CULTREG(R%) = 6 Then
                ''            adjustforinitialdevm = meatactpredrat(R%)
                ''        Else
                ''            adjustforinitialdevm = ConvergeOverTime(meatactpredrat(R%), 1, agconv)
                ''        End If
                'TimeFac = Amin(IY%, agconv)
                'adjustforinitialdevm = (MeatActPredRat(R%) * (agconv - TimeFac) _
                '+ 1 * TimeFac) / agconv
                ''        AGDEM(R%, 2) = tonspercap * POP(R%) * adjustforinitialdevm * _
                '(WAP(2) / swapi(2)) ^ elasmd(R%)
                'If RegName$(R%) = "Puerto Rico" Then Stop
                '            (FPRI(r%, 2) / SWAPI(2)) ^ elasmd(R%)
                ''        AGDEM(R%, 2) = Amin(AGDEM(R%, 2), meatmaxr(R%) * POP(R%)) * agdemm(R%, 2)
                'If RegName$(R%) = "Puerto Rico" Then Stop

                '       sagdemm(R%) = AGDEM(R%, 2) 'bbh 2012/02/16
                ''        calfrommeat = AGDEM(R%, 2) * lvcfr(R%) * sclavf(R%)
                ''        calmeatm = AMAX(0.01, calmeatm)
                ''        If calfrommeat > calmeatm * caldem Then 'Limit portion of calories from meat
                ''            calfrommeat = calmeatm * caldem
                ''            AGDEM(R%, 2) = calmeatm * caldem / (lvcfr(R%) * sclavf(R%))
                ''        End If
                'back out price effects

                'dsr 2015/03/25 comment out following code since values calculated above (use '')
                '   All but First 2:  Agricultural Demand - Fish
                '   --------------------------------------------
                ''        Call XYTABL("GDP/Capita (PPP) Versus Fish Demand per Capita", GDPPCPI(R%), tonspercapi)
                ''        Call XYTABL("GDP/Capita (PPP) Versus Fish Demand per Capita", GDPPCP(R%), tonspercap)
                ''        fishdempc = fishdemipc(R%) * tonspercap / AMAX(0.001, tonspercapi)
                '        DemMul = ConvergeOverTimeSmooth(0, 1, 20)
                ''        If fishdempc > fishdemipc(R%) * 1.5 Then ' allow some more increase, slowly
                'fishdempc = fishdemipc(R%) * 1.5 + Amin(DemMul * fishdemipc(R%), fishdempc - fishdemipc(R%) * 1.5)
                ''            fishdempc = Amin(fishdempc, ConvergeOverTimeSmooth(fishdempc, 2 * fishdemipc(R%), 50))
                'don't let China grow too much with income
                ''        End If
                '        fishdempc = Amin(fishdempc, AMAX(0.04, fishdemipc(R%) * 1.5))   'don't let China grow too much with income
                ''        fishdempc = Amin(fishdempc, AMAX(fishdemipc(R%), 0.1)) 'don't let Iceland grow with income (stops top 20)
                ''        AGDEM(R%, 3) = fishdempc * POP(R%) * (WAP(3) / swapi(3)) ^ (-0.6) 'elasmd(R%)  which is -.3

                'If RegName$(R%) = "China" Then Stop
                '   All but First 2:  Agricultural Demand - Crops for Food and Total
                '   ----------------------------------------------------------------
                'dsr 2015/03/25 comment out following code since FDEM(R%, 1) now calculated above (use '')
                ''        calfromgrain = caldem - calfrommeat
                'jrs 2014/11/25 FDEM(R%) = calfromgrain / sclavf(R%) * (WAP(1) / swapi(1)) ^ elascd(R%) * agdemm(R%, 1)
                ''       FDEM(R%, 1) = calfromgrain / sclavf(R%) * (WAP(1) / swapi(1)) ^ elascd(R%) * agdemm(R%, 1)
                'If RegName$(R%) = "Puerto Rico" Then Stop
                '        FDEM(r%) = CalFromGrain / SCLAVF(r%) * (FPRI(r%, 1) / SWAPI(1)) ^ elascd(R%) * agdemm(r%, 1)
                'jrs 2014/11/25 FDEM(R%) = Amin(FDEM(R%), calmax * POP(R%) * adjustforinitialdevc)
                ''        FDEM(R%, 1) = Amin(FDEM(R%, 1), calmax * POP(R%) * adjustforinitialdevc)
                '    If RegName$(R%) = "Belize" Then Stop

                'jrs 2014/11/25 AGDEM(R%, 1) = AMAX(0.01, FEDDEM(R%,1) + INDEM(R%,1) + FDEM(R%))
                'dsr 2015/02/23 Add FMDEM to calculation of AGDEM
                'dsr 2015/03/25 Move next line down to All but First 2:  Agricultural Demand - Reduce if Too High as Percent of GDP
                ''        AGDEM(R%, 1) = AMAX(0.01, FEDDEM(R%, 1) + INDEM(R%, 1) + FDEM(R%, 1) + FMDEM(R%, 1))
                '        SAGDEM(R%, 1) = AGDEM(R%, 1)


                'dsr 2015/03/25 comment out following code since dealt with calorie max above (use '')
                '   All but First 2:  Agricultural Demand - Reduce if Calories Exceed Maximum
                '   -------------------------------------------------------------------------

                'jrs 2014/11/25 computeddemandpcap = sclavf(R%) * (FDEM(R%) + AGDEM(R%, 2) * lvcfr(R%)) / POP(R%)
                ''        computeddemandpcap = sclavf(R%) * (FDEM(R%, 1) + AGDEM(R%, 2) * lvcfr(R%)) / POP(R%)
                ''        If computeddemandpcap > calmax Then
                'jrs 2014/11/25 FDEM(R%) = FDEM(R%) * calmax / computeddemandpcap
                ''            FDEM(R%, 1) = FDEM(R%, 1) * calmax / computeddemandpcap 'WHY IS THIS STATEMENT SO IMPORTANT?
                ''            AGDEM(R%, 2) = AGDEM(R%, 2) * calmax / computeddemandpcap
                'dsr 2015/03/19 need to add code to recompute AGDEM(R%, 1)
                ''        End If
                'dsr 2015/03/18 Can comment out code below when activate new code **** end



                '   All but First 2:  Agricultural Demand - Reduce some crop and meat demand
                '               if consumers spending too much on food as Percent of Consumption
                'dsr 2015/07/01 May want to revisit as this does not consider fish and other things
                '   ----------------------------------------------------------------------------

                'jrs 2014/11/25 csprelim = CSF(R%) * (FDEM(R%) * swapi(1) + AGDEM(R%, 2) * swapi(2))

                'bbh 2017/01/24 start Needed to redo Dale's logic here; was actually increasing Somalia's FDEM(r%,1)
                'so bbh removed reductions of feed, industrial demand for ag
                'the logic here is to keep food (not ag) consumption below 85% of total CS
                'Tried changing limit from 85% to 95% because Burundi being clobbered:  high trade deficit leads
                ' to high debt in 2025+, to decreased imports and C, to inadequate CS for food demand; hurts rather than helps

                'dsr 2015/07/01 Next line not needed
                '        AGDEM(R%, 1) = AMAX(0.01, FEDDEM(R%, 1) + INDEM(R%, 1) + FDEM(R%, 1) + FMDEM(R%, 1))
                '        AGDEM(R%, 1) = AMAX(0.01, FEDDEM(R%, 1) + INDEM(R%, 1) + FDEM(R%, 1) + FMDEM(R%, 1) + AGLOSSTRANS(R%, 1))
                'dsr 2015/03/25 Modify so only deal with FDEM(R%, 2) and not all of AGDEM(R%, 2)
                ''        csprelim = CSF(R%) * (FDEM(R%, 1) * swapi(1) + AGDEM(R%, 2) * swapi(2))
                'dsr 2015/07/01 Where are current prices?
                '        csprelim = CSF(R%) * (FDEM(R%, 1) * swapi(1) + FDEM(R%, 2) * swapi(2)) 'bbh 2017/01/24
                Dim csprelim = CSF(R%) * (FDEM(R%, 1) * swapi(1) + FDEM(R%, 2) * swapi(2) + FDEM(R%, 3) * swapi(3))
                If csprelim > 0.85 * C(R%) Then 'Need room for other consumption
                    Dim necreduc = csprelim - 0.85 * C(R%) 'Monetary terms
                    'FeedReduc = Amin(1, necreduc / csprelim * 2) * FEDDEM(R%, 1)
                    'Take more from feed, industry, meat
                    'FEDDEM(R%, 1) = FEDDEM(R%, 1) - FeedReduc 'Physcial terms

                    'InDemReduc = Amin(1, necreduc / csprelim * 2) * INDEM(R%, 1)
                    'INDEM(R%, 1) = INDEM(R%, 1) - InDemReduc 'Physcial terms
                    'dsr 2015/03/25 Modify so only deal with FDEM(R%, 2) and not all of AGDEM(R%, 2)
                    ''            meatreduc = Amin(1, necreduc / csprelim * 2) * AGDEM(R%, 2)
                    ''            AGDEM(R%, 2) = AGDEM(R%, 2) - meatreduc 'Physcial terms
                    '           The factor of 2 below disproportionatly directs reduction to meat and fish rather than crops
                    Dim meatreduc = Amin(1, necreduc / csprelim * 2) * FDEM(R%, 2)
                    Dim fishreduc = Amin(1, necreduc / csprelim * 2) * FDEM(R%, 3) 'bbh added fish 2017/07/11
                    Dim totfoodsavings = meatreduc * CSF(R%) * swapi(2) + fishreduc * CSF(R%) * swapi(3)
                    If totfoodsavings > necreduc Then 'cut back physical reductions and recompute monetary total
                        meatreduc = CSng(meatreduc * necreduc / totfoodsavings)
                        fishreduc = CSng(fishreduc * necreduc / totfoodsavings)
                        totfoodsavings = meatreduc * CSF(R%) * swapi(2) + fishreduc * CSF(R%) * swapi(3)
                    End If
                    FDEM(R%, 2) = CSng(FDEM(R%, 2) - meatreduc)  'Physcial terms
                    FDEM(R%, 3) = CSng(FDEM(R%, 3) - fishreduc)  'Physcial terms
                    'totsavings = (FeedReduc + InDemReduc) * CSF(R%) * swapi(1) + _
                    'meatreduc * CSF(R%) * swapi(2)  'Monetary terms
                    'totfoodsavings = meatreduc * CSF(R%) * swapi(2) + fishreduc * CSF(R%) * swapi(3)
                    'foodreduc = necreduc - totsavings 'Monetary Terms
                    'Compute needed monetary reduction in crop demand and apply physical reduction
                    Dim foodreduc = AMAX(0, necreduc - totfoodsavings) 'Monetary Terms
                    'jrs 2014/11/25 FDEM(R%) = AMAX(0.1 * FDEM(R%), FDEM(R%) - foodreduc / _
                    '(AMAX(0.001, CSF(R%) * swapi(1)))) 'Physical terms
                    FDEM(R%, 1) = CSng(AMAX(0.1 * FDEM(R%, 1), FDEM(R%, 1) - foodreduc /
                (AMAX(0.001, CSF(R%) * swapi(1))))) 'Physical terms
                    'jrs 2014/11/25 AGDEM(R%, 1) = AMAX(0.01, FEDDEM(R%,1) + INDEM(R%,1) + FDEM(R%))
                    'dsr 2015/07/01 Next line not needed
                    '            AGDEM(R%, 1) = AMAX(0.01, FEDDEM(R%, 1) + INDEM(R%, 1) + FDEM(R%, 1))
                    'bbh 2017/01/24 end
                End If

                'dsr 2015/03/25 comment out following code since FDEM(R%, 2 and 3) now calculated above (use '')
                'dsr 2015/03/04 *** Start
                '   All but First 2:  FDEM for Meat and Fish Calculate as residual
                '   ----------------------------------------------------------------------------
                ''        For f% = 2 To 3
                ''            FDEM(R%, f%) = AGDEM(R%, f%) - FEDDEM(R%, f%) - INDEM(R%, f%) - FMDEM(R%, f%) - LOSS(R%, f%)
                'jrs 2014/12/01
                ''            FDEM(R%, f%) = AMAX(0.01, Amin(FDEM(R%, f%), 1000))
                ''        Next f%
                'dsr 2015/03/04 *** End

                'dsr 2015/03/25 Calculate final values of AGDEM **** start
                For f% = 1 To 3
                    AGDEM(R%, f%) = FDEM(R%, f%) + FEDDEM(R%, f%) + INDEM(R%, f%) + FMDEM(R%, f%) + AGLOSSTRANS(R%, f%)
                    'dsr 2015/03/27 In next line, lower min to 0.000001; up max to 10000 from 1000 for China
                    AGDEM(R%, f%) = AMAX(0.000001, Amin(AGDEM(R%, f%), 10000))
                Next f%
                'dsr 2015/03/25 Calculate final values of AGDEM **** end

            Next R%
            '   All but First 2:  Trade Computation
            '   -----------------------------------
            'bbh 2022/10/27
            Dim agmbase As Single

            For f% = 1 To 3
                Dim wxc = 0!
                Dim wmd = 0!
                Dim wxcp = 0!
                Dim wmdp = 0!
                Dim totsurdef = 0!
                Dim totalstbase = 0!
                For R% = 1 To NR%
                    'dsr 2015/11/02 Simply define xbase as post-loss production plus demand
                    '            If f% = 1 Then
                    '                xbase = agpp(R%, f%) + AGDEM(R%, f%) 'For entrepot economies
                    '                xbase = agpp(R%, f%) - AGLOSSPROD(R%, f%) + AGDEM(R%, f%)
                    '            ElseIf f% = 2 Then
                    'dsr 2015/04/13 Change LVHERD(R%) * slr to agpp(R%, f%)
                    '                xbase = LVHERD(R%) * slr + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) - AGLOSSPROD(R%, f%) + AGDEM(R%, f%)
                    'Slow down base of export change; heavy exporters
                    'cannot shift market share that fast
                    '            Else
                    '                xbase = AGP(R%, 3) + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) - AGLOSSPROD(R%, 3) - AGLOSSPROD(R%, 4) + AGDEM(R%, f%)
                    '            End If
                    'If GRPS(1, R%) = "Congo, Dem. Republic of the" And FirstYear + IY - 1 >= 2067 And f% = 1 Then Stop
                    'bbh 2022/12/13 Exportbase needs to be non-zero for later use
                    'xbase = AGP(R%, f%) + AGDEM(R%, f%)
                    Dim xbase = AMAX(0.0001, AGP(R%, f%) + AGDEM(R%, f%))
                    Dim agxbase = XKAVE(R%, f%) * xbase
                    Mul = 1
                    AGX(R%, f%) = AMAX(0.0001, Amin(agxbase * Mul, XKAVMAX(R%, f%) * xbase))
                    Dim WAPM = WAP(f%) * protecm(R%, 1) 'jrs 2016/11/18 added Sector to protecm
                    'bbh 2022/10/27 Added here AGP to AGDEM for mkave base; consistent with exports, prices, and YL as import-side base
                    'bbh 2022/12/13 Importbase needs to be non-zero for later use
                    'agmbase = AGDEM(R%, f%) + AGP(R%, f%)
                    agmbase = AMAX(0.0001, AGDEM(R%, f%) + AGP(R%, f%))
                    'agmbase = mkave(R%, f%) * AGDEM(R%, f%)
                    'bbh 2022/10/27 Added here AGP to AGDEM for mkave base; consistent with exports, prices, and YL as import-side base
                    AGM(R%, f%) = AMAX(0.0001, Amin(mkave(R%, f%) * agmbase * Mul, MKAVMax(R%, f%) * agmbase))
                    'AGM(R%, f%) = AMAX(0.0001, Amin(agmbase * Mul, MKAVMax(R%, f%) * AGDEM(R%, f%)))
                    'bbh 2019/10/12 start
                    'iMTARIFFTAXR is computed locally in IFsEcoSAM, so need Ag value here
                    'Because AGM change also changes mkave over time, tariff level changes have somewhat compounding
                    '   impact, higher than elasticity
                    'jrs 2020/07/07 MTARIFFTAXR not computed yet (constant when bilateral mode is off)
                    'AGM(R%, f%) = CSng(AGM(R%, f%) * (1 + ((MTARIFFTAXR(R%, Agri) - iMTARIFFTAXRAg(R%)) / 100) * -1.0#))
                    'jrs 2020/10/23 start
                    'Implementing Barry's idea: differential approach
                    If SMTariffTaxR(R%, Agri) < iMTARIFFTAXRAg(R%) Then
                        'This line was the orignal approach, which was producing negatives when Tariffs grew more than 100% above initial values
                        AGM(R%, f%) = CSng(AGM(R%, f%) * (1 + ((SMTariffTaxR(R%, Agri) - iMTARIFFTAXRAg(R%)) / 100) * -1.0#))
                    Else
                        'jrs 2026/05/08 AGM(R%, f%) = CSng(AGM(R%, f%) * (1 + (SMTariffTaxR(R%, Agri) - iMTARIFFTAXRAg(R%)) / 100) ^ -1)
                        AGM(R%, f%) = CSng(AGM(R%, f%) * (1 + Amin(SMTariffTaxR(R%, Agri) - iMTARIFFTAXRAg(R%), mtarifftaxrgl(R%)) / 100) ^ -1)
                    End If
                    'jrs 2020/10/23 end
                    'bbh 2022/10/27 adding price and exchange rate responsiveness to crops; then removed but keep in case
                    ''            If f% = 1 Then
                    ''                AGX(R%, f%) = AGX(R%, f%) * (1 + (WP(1) - FPRI(R%, 1) / 100 * EXRATE(R%)) * 0.2) 'elastrpr(R%, 1))
                    ''                AGM(R%, f%) = AGM(R%, f%) * (1 + (FPRI(R%, 1) / 100 * EXRATE(R%) - WP(1)) * 0.2) 'elastrpr(R%, 1))
                    ''            'If RegName$(R%) = "Burundi" And IY% + 2016 >= 2025 Then Stop
                    ''            End If

                    'If RegName$(R%) = "Nigeria" Then Stop
                    'bbh 2019/10/12 end

                    'jrs 2020/06/23 *** begin ***
                    If gdprext(R%) > -99 Then
                        Dim factorm As Single
                        Dim factorx As Single
                        'jrs 2020/07/06 changed parameter name
                        factorm = AMAX(1 + (GDPR(R%) - SmoothGDPR(R%)) * elasmincshock(R%, 1), 0.00001F) 'jrs 2020/07/01 added AMAX(,0)
                        factorx = AMAX(1 + (GDPR(R%) - SmoothGDPR(R%)) * elasxincshock(R%, 1), 0.00001F) 'jrs 2020/07/01 added AMAX(,0)
                        AGM(R%, f%) = AGM(R%, f%) * factorm
                        AGX(R%, f%) = AGX(R%, f%) * factorx
                    End If
                    'jrs 2020/06/23 *** end ***

                    'bbh 2020/05/07 start 'bbh 2020/05/07 Am nervous that this is done after bounding, some project need?
                    'bbh 2020/06/21 start moved further down so multiplier would pass through
                    'AGM(R%, f%) = AGM(R%, f%) * agmm(R%, f%) 'added second dimension
                    'AGX(R%, f%) = AGX(R%, f%) * agxm(R%, f%)
                    'bbh 2020/06/21 end
                    'bbh 2020/05/07 end

                    'dsr 2015/06/15 Use post-loss production
                    '            StBase = AGDEM(R%, f%) + AGP(R%, f%) 'agpp(R%, f%) 2013/01/21 bbh

                    'bbh 2020/08/06 start Moved computation of surpdef to end of trade block when have AGM and AGX from same year
                    'as AGDEM and AGP
                    Dim StBase = AGDEM(R%, f%) + AGP(R%, f%)
                    '    Dim prodterm = AGP(R%, f%)

                    '    Dim WTMul = (swxc(f%) + swmd(f%)) / 2 'anticates subsequent adjustment for global imbalance
                    '    Dim TradeTerm = AGM(R%, f%) * WTMul / swmd(f%) - AGX(R%, f%) * WTMul / swxc(f%)

                    '    surpdef(R%, f%) = cumstk(R%, f%) +
                    'prodterm + TradeTerm - AGDEM(R%, f%) - agdstl * StBase
                    '    'after recomputation of trade, this same logic adjusts stocks

                    Dim tradebase = AGX(R%, f%) + AGM(R%, f%)
                    Dim trademul = surpdef(R%, f%) / tradebase 'bbh 2022/10/27 Note: Not currently used anywhere
                    'bbh 2020/08/06 end

                    'jrs 2007/01/12 no double counting
                    If RegiontoSum(R%) Then
                        wxcp = wxcp + AGX(R%, f%)
                        wmdp = wmdp + AGM(R%, f%)
                        totsurdef = totsurdef + surpdef(R%, f%)
                        totalstbase = totalstbase + StBase
                    End If
                Next R%
                SGlobalSurDefRate(f%) = GlobalSurDefRate(f%) 'bbh 2022/11/28
                GlobalSurDefRate(f%) = totsurdef / totalstbase
                For R% = 1 To NR%
                    'If GRPS(1, R%) = "Congo, Dem. Republic of the" And FirstYear + IY - 1 >= 2067 And f% = 1 Then Stop

                    Dim StBase = AGDEM(R%, f%) + AGP(R%, f%)
                    'bbh 2020/12/10 Willem and Barry thought second line below might be better; seems not
                    'countryextrasurdef = surpdef(R%, f%) - agstl * StBase
                    'bbh 2022/11/28 Tried replacing the line directly below with above;
                    'problem is that if global stocks are high or low relative to agstl target value, WXC and WMD get out of balance and can force perverse trade patterns (e.g. UAE)
                    Dim countryextrasurdef = surpdef(R%, f%) - GlobalSurDefRate(f%) * StBase
                    'Dim countryextrasurdef = cumstk(R%, f%) - Amin(agdstl, globalsurdefrate) * StBase

                    Dim SurDefChangeTerm = countryextrasurdef - scountryextrasurdef(R%, f%) 'bbh 2022/10/27 Note: Not currently used anywhere
                    Dim countryextrasurdefadj = countryextrasurdef / 5 '+ 2 * SurDefChangeTerm / 5
                    'Global stock cycle run about 20 years; delta term 2x absolute term
                    Dim exportshare = AGX(R%, f%) / AMAX(0.0001, AGX(R%, f%) + AGM(R%, f%))

                    '            defadjmul = 1
                    '            If countryextrasurdefadj < 0 AndAlso (AGX(R%, f%) > 0.5 * AGP(R%, f%) _
                    '                OrElse AGM(R%, f%) > 0.5 * AGDEM(R%, f%)) _
                    '                AndAlso cumstk(R%, f%) < 0.7 * agdstl * StBase Then
                    '                    'Hong Kong exports grow faster than adjustment, this speeds up adjustment
                    '                    'Qatar's imports do not grow fast enough relative to demand, this speeds up
                    '                defadjmul = 2
                    '            End If
                    '            If countryextrasurdefadj < 0 AndAlso cumstk(R%, f%) < 0.4 * agdstl * StBase Then
                    '                defadjmul = 2
                    '            End If


                    Dim defadjmul = 1.0!
                    If Math.Abs(countryextrasurdef / StBase) > 0.03 Then
                        If countryextrasurdefadj < 0 AndAlso (AGX(R%, f%) > 0.5 * AGP(R%, f%) _
                            OrElse AGM(R%, f%) > 0.5 * AGDEM(R%, f%)) Then
                            'Hong Kong exports grow faster than adjustment, this speeds up adjustment
                            'Qatar's imports do not grow fast enough relative to demand, this speeds up
                            'dsr 2015/04/13 Add code to adjust defadjmul **** start
                            Dim defadjmulmax = 3
                            'dsr 2015/06/15 Here and elsewhere, subtract production losses
                            '                    If (AGX(R%, f%) > 1 * AGP(R%, f%) OrElse AGM(R%, f%) > 1 * AGDEM(R%, f%)) Then
                            'dsr 2015/12/12 Can simply following with change of AGLOSSPROD(R%, 3) to include aquaculture and catch
                            '                    If f% < 3 Then
                            '                      If (AGX(R%, f%) > 1 * (AGP(R%, f%) - AGLOSSPROD(R%, f%)) OrElse AGM(R%, f%) > 1 * AGDEM(R%, f%)) _
                            '                        Then defadjmulmax = 6
                            '                    ElseIf (AGX(R%, f%) > 1 * (AGP(R%, f%) - AGLOSSPROD(R%, 3) - AGLOSSPROD(R%, 4)) OrElse AGM(R%, f%) > 1 * AGDEM(R%, f%)) _
                            '                        Then defadjmulmax = 6
                            '                    End If '(f% < 3)
                            If (AGX(R%, f%) > 1 * (AGP(R%, f%) - AGLOSSPROD(R%, f%)) OrElse AGM(R%, f%) > 1 * AGDEM(R%, f%)) _
                                Then defadjmulmax = 6
                            '                    defadjmul = 1 + Amin(3, (Abs(countryextrasurdef / StBase) - 0.03) * 20)
                            'bbh 2022/11/28 This nested if-then code block is very difficult to understand in terms of scaling
                            '   Its core purpose is to increase adjustments in AGX and/or AGM when stocks vary from target values more than 3% of the stock base
                            '     AND when AGX or AGM is large relative to production or demand); but the magnitude of defadjmul was causing oscillation,
                            '     e.g. in Gambia AGM near end of century; so scaled back the *20 to *10 as in other computational branches of it
                            'defadjmul = 1 + Amin(defadjmulmax, (Abs(countryextrasurdef / StBase) - 0.03) * 20)
                            defadjmul = CSng(1 + Amin(defadjmulmax, (Math.Abs(countryextrasurdef / StBase) - 0.03) * 10))
                            'dsr 2015/04/13 Add code to adjust defadjmul **** end

                        Else
                            defadjmul = CSng(1 + Amin(2, (Math.Abs(countryextrasurdef / StBase) - 0.03) * 10))
                        End If
                    Else
                        defadjmul = CSng(1 + Amin(2, (Math.Abs(countryextrasurdef / StBase) - 0.03) * 10))
                    End If

                    'If IY% >= 3 AndAlso RegName$(R%) = "Hong Kong" AndAlso f% = 2 Then Stop

                    Dim xbase = AMAX(0.0001, AGP(R%, f%) + AGDEM(R%, f%)) 'bbh 2022/12/19 xbase and agmbase lines added to prepare for use below
                    agmbase = AMAX(0.0001, AGP(R%, f%) + AGDEM(R%, f%))

                    Dim ImportBoost = 1.0!
                    ' wv 2022/08/22 add exportboost to allow for faster rise in exports
                    Dim exportboost = 1.0!

                    'bbh 2022/10/27 If-then below was causing oscillation in fish imports for countries like Sierra Leone
                    'If f% = 3 AndAlso ((AGM(R%, f%) + AGX(R%, f%)) < 0.15 * AGDEM(R%, f%)) Then
                    '    defadjmul = defadjmul * 4  'very low trade inadequate for adjustment
                    'ElseIf f% = 3 AndAlso ((AGM(R%, f%) + AGX(R%, f%)) < 0.25 * AGDEM(R%, f%)) Then
                    '    defadjmul = defadjmul * 3
                    'ElseIf f% = 3 AndAlso ((AGM(R%, f%) + AGX(R%, f%)) < 0.35 * AGDEM(R%, f%)) Then
                    '    defadjmul = defadjmul * 2
                    'End If
                    'dsr 2015/04/13 Modify next line so that high levels of imports can also lead to import boost (then took this out out)
                    'If (countryextrasurdefadj < 0) And (AGX(R%, f%) < 0.12 * AGDEM(R%, f%)) Then
                    'bbh 2022/10/27 Line below was wrong in 2 ways with respect to importboost rise: (1) used AGX; (2) did not include AGP
                    '            If (countryextrasurdefadj < 0) And (AGX(R%, f%) < 0.12 * AGDEM(R%, f%) Or AGM(R%, f%) > 1 * AGDEM(R%, f%)) Then

                    'bbh 2022/12/13 Start - sudden jumps betweeen 1 to 2 cause transients, phase transition elimination; 2 if-then blocks reworked
                    'If (countryextrasurdefadj < 0) And (AGM(R%, f%) < 0.12 * (AGDEM(R%, f%) + AGP(R%, f%))) Then
                    If (countryextrasurdefadj < 0) And (AGM(R%, f%) < 0.12 * agmbase) Then
                        'importboost = 2
                        ImportBoost = CSng(1 + (0.12 - AGM(R%, f%) / agmbase) / 0.12) 'AGM of 0 would generate importboost of 2,
                    End If
                    ' wv 2022/08/22 add exportboost to allow for faster rise in exports
                    '            If (countryextrasurdefadj > 0) And (exportshare < 0.1) Then
                    '                exportboost = 2
                    '            End If
                    'If (countryextrasurdefadj > 0) And (AGX(R%, f%) < 0.12 * agxbase) Then
                    If (countryextrasurdefadj > 0) And (AGX(R%, f%) < 0.12 * xbase) Then 'bbh 2022/12/19 Should be xbase here, not agxbase as in above line
                        exportboost = CSng(1 + (0.12 - AGX(R%, f%) / xbase) / 0.12)
                    End If
                    'bbh 2022/12/13 End - sudden jumps


                    'If IY% + 2016 >= 2036 And RegName$(R%) = "Central African Republic" And f% = 1 Then Stop                    '            If countryextrasurdefadj < 0 Then 'Deficit, reduce exports and increase imports
                    '                AGX(R%, f%) = AMAX(0.5 * AGX(R%, f%), _
                    '                    AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                    '                AGM(R%, f%) = Amin(2 * AGM(R%, f%), _
                    '                    AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul * ImportBoost)
                    '
                    '            Else   'Surplus, increase exports and decrease imports
                    '                AGX(R%, f%) = Amin(2 * AGX(R%, f%), _
                    '                    AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                    '                AGM(R%, f%) = AMAX(0.5 * AGM(R%, f%), _
                    '                    AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul)

                    'bbh 2019/03/15 and dkb 2019/03/06 Yemen START
                    If agtrdadjsw(R%) > 0.5 Then
                        '                If countryextrasurdefadj < 0 Then
                        AGX(R%, f%) = AGX(R%, f%)
                        AGM(R%, f%) = AGM(R%, f%)
                        '                Else
                        '                    AGX(R%, f%) = AGX(R%, f%)
                        '                    AGM(R%, f%) = AGM(R%, f%)
                        '                End If
                    Else
                        'bbh 2020/12/10 start Considered replacing use of countryextra both to control branch and to adjust trade levels with stock term
                        'Earlier code could increase AGM even when country had a surplus of stocks, etc.
                        'surpdef(r%,f%) blew up in IntlPol in 2021 Belize on negative imports in log function so bound agm and agx to be positive
                        'then surpdef(r%,f%) blew up in 2029 in IfsEconIntl2, overflow; tried substituting ld countryextrasurdefadj but no real impact by itself or times 5
                        '                If cumstk(R%, f%) < agdstl * (AGDEM(R%, f%) + AGP(R%, f%)) Then 'Deficit, reduce exports and increase imports
                        '                    AGX(R%, f%) = AMAX(0.5 * AGX(R%, f%), AGX(R%, f%) + surpdef(R%, f%) * 0.1 * exportshare * defadjmul)
                        '                    AGM(R%, f%) = Amin(2 * AGM(R%, f%), AGM(R%, f%) - surpdef(R%, f%) * 0.1 * (1 - exportshare) * defadjmul * importboost)
                        '                    AGM(R%, f%) = AMAX(0.00001, AGM(R%, f%))
                        '                Else 'Surplus, increase exports and decrease imports
                        '                    AGX(R%, f%) = Amin(2 * AGX(R%, f%), AGX(R%, f%) + surpdef(R%, f%) * 0.1 * exportshare * defadjmul)
                        '                    AGM(R%, f%) = AMAX(0.5 * AGM(R%, f%), AGM(R%, f%) - surpdef(R%, f%) * 0.1 * (1 - exportshare) * defadjmul)
                        '                    AGX(R%, f%) = AMAX(0.00001, AGX(R%, f%))
                        '                End If
                        '                AGM(R%, f%) = AMAX(0.00001, AGM(R%, f%))
                        '                AGX(R%, f%) = AMAX(0.00001, AGX(R%, f%))

                        'If IY% >= 47 And RegName$(R%) = "Equa Guinea" And f% = 1 Then Stop

                        'If IY% >= 13 And RegName$(R%) = "Pakistan" And f% = 1 Then Stop
                        'If (RegName$(R%) = "Congo, Democratic Republic of" Or RegName$(R%) = "China") And f% = 1 Then Stop

                        'Considered requiring both country surplus relative to global average surplus and to local desired;
                        '                If (countryextrasurdefadj < 0) And (cumstk(R%, f%) < agdstl * (AGDEM(R%, f%) + AGP(R%, f%))) Then 'Deficit, reduce exports and increase imports
                        '                    AGX(R%, f%) = AMAX(0.5 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                        '                    AGM(R%, f%) = Amin(2 * AGM(R%, f%), AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul * importboost)
                        ''If RegName$(R%) = "Congo, Democratic Republic of" And f% = 1 Then Stop
                        '                End If
                        '                If (countryextrasurdefadj > 0) And (cumstk(R%, f%) > agdstl * (AGDEM(R%, f%) + AGP(R%, f%))) Then  'Surplus, increase exports and decrease imports
                        '                    AGX(R%, f%) = Amin(2 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                        '                    AGM(R%, f%) = AMAX(0.5 * AGM(R%, f%), AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul)
                        ''If RegName$(R%) = "Congo, Democratic Republic of" And f% = 1 Then Stop
                        '                End If

                        'Looking at a comparison of local stock levels versus global avoids pushing up imports when the world is awash in production with high/wide ylm
                        If countryextrasurdefadj < 0 Then 'Deficit, reduce exports and increase imports
                            'wv 2022/08/22 change max min to 0.2 and 5 rather then 0.5 and 2 to allow for stronger feedback of stocks to export/import dynamics
                            'wv 2022/08/22 add exportboost to allow for faster rise in exports with surplus
                            AGX(R%, f%) = AMAX(0.2 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                            AGM(R%, f%) = Amin(5 * AGM(R%, f%), AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul * ImportBoost)
                            'If RegName$(R%) = "Congo, Democratic Republic of" And f% = 1 Then Stop
                        Else   'Surplus, increase exports and decrease imports
                            'bbh 2022/10/27 Start Boost exports when rate is low and stocks are in surplus
                            If AGX(R%, f%) < 0.05 * countryextrasurdef Then
                                AGX(R%, f%) = Amin(8 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul * exportboost)
                            ElseIf AGX(R%, f%) < 0.1 * countryextrasurdef Then
                                AGX(R%, f%) = Amin(5 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul * exportboost)
                            ElseIf AGX(R%, f%) < 0.2 * countryextrasurdef Then
                                AGX(R%, f%) = Amin(3 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul * exportboost)
                            Else
                                AGX(R%, f%) = Amin(2 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul * exportboost)
                            End If
                            'If IY% + 2016 >= 2029 And RegName$(R%) = "India" And f% = 2 Then Stop
                            'If IY% + 2016 >= 2022 And RegName$(R%) = "Micronesia" And f% = 3 Then Stop
                            'bbh 2022/10/27 End Boost exports
                            'AGX(R%, f%) = Amin(2 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul * exportboost)
                            'AGX(R%, f%) = Amin(2 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                            AGM(R%, f%) = AMAX(0.5 * AGM(R%, f%), AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul)
                            'If IY% + 2016 >= 2035 And RegName$(R%) = "Central African Republic" And f% = 1 Then Stop
                        End If
                        AGM(R%, f%) = AMAX(0.00001, AGM(R%, f%))
                        AGX(R%, f%) = AMAX(0.00001, AGX(R%, f%))
                        'bbh 2020/12/10 end
                        'bbh 2019/03/15 and dkb 2019/03/06  end
                        '            'dkb 2019/03/06 for Yemen - START
                        ''                AGM(R%, f%) = Amin(2 * AGM(R%, f%), _
                        '                    AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul * ImportBoost)
                        '                If RegName$(R%) = "Yemen" Then
                        '                    AGM(R%, f%) = AGM(R%, f%)
                        '                Else
                        '                    AGM(R%, f%) = Amin(2 * AGM(R%, f%), AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul * ImportBoost)
                        '                End If
                        '            'dkb 2019/03/06 for Yemen - END
                        '            'dkb 2019/03/06 for Yemen - START
                        ''               AGX(R%, f%) = Amin(2 * AGX(R%, f%), _
                        '                    AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                        ''               AGM(R%, f%) = AMAX(0.5 * AGM(R%, f%), _
                        '                    AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul)
                        '                If RegName$(R%) = "Yemen" Then
                        '                    AGM(R%, f%) = AGM(R%, f%)
                        '                Else
                        '                AGX(R%, f%) = Amin(2 * AGX(R%, f%), AGX(R%, f%) + countryextrasurdefadj * exportshare * defadjmul)
                        '                AGM(R%, f%) = AMAX(0.5 * AGM(R%, f%), AGM(R%, f%) - countryextrasurdefadj * (1 - exportshare) * defadjmul)
                        '                End If
                        '            'dkb 2019/03/06 for Yemen - END

                    End If

                    'bbh 2022/10/27 Begin two new f% if-then blocks to restrict ag imports and exports; Burundi issue on crop imports; India on meat exports
                    'bbh 2022/12/13 Only country at this date with initial crop import data showing value above 30% of GDP in base year is Djibouti, so constraint not imposed
                    If f% = 1 And msfi(R%, f%) * iAGM(R%, f%) * swapi(f%) / GDPI(R%) < 0.3 And AGM(R%, f%) * msf(R%, f%) * swapi(f%) > 0.3 * GDP(R%) Then
                        'AGM(R%, f%) = 0.3 * GDP(R%) / (SWAPI(f%) * msf(R%, f%)) 'bbh 2022/12/13 Phase in greater reductions to AGM as excess to 30% approaches 50%; next four lines
                        Dim ExcessAGM = AMAX(0, AGM(R%, f%) - 0.3 * GDP(R%) / (msf(R%, f%) * swapi(f%)))
                        AGM(R%, f%) = CSng(0.3 * GDP(R%) / (msf(R%, f%) * swapi(f%)) + 0.5 * ExcessAGM)
                    End If
                    If AGX(R%, f%) > 0.1 * WTFoodType(f%) And f% = 2 Then
                        AGX(R%, f%) = CSng(Amin(AGX(R%, f%), SAGX(R%, f%) * 1.01))
                    ElseIf AGX(R%, f%) > 0.05 * WTFoodType(f%) And f% = 2 Then
                        AGX(R%, f%) = CSng(Amin(AGX(R%, f%), SAGX(R%, f%) * 1.01))
                    End If

                    'bbh 2020/06/21 start moved from earlier becuase deficit adjustment logic was overriding impact
                    'If RegName$(R%) = "Nigeria" And f% = 1 And IY% >= 15 Then Stop
                    AGM(R%, f%) = AGM(R%, f%) * agmm(R%, f%) '
                    AGX(R%, f%) = AGX(R%, f%) * agxm(R%, f%)
                    'bbh 2020/06/21 end

                    'jrs 2021/01/08 *** begin ***
                    '---- Adjust Exports based on swing in Imports from main partners ----
                    Call Savemdbasediff(AGM(R%, f%), R, f)
                    'jrs 2021/01/08 *** end ***

                    'jrs 2007/01/12 no double counting
                    If RegiontoSum(R%) Then
                        wxc = wxc + AGX(R%, f%)
                        wmd = wmd + AGM(R%, f%)
                    End If
                    scountryextrasurdef(R%, f%) = countryextrasurdef

                    'If RegName$(R%) = "China" AndAlso f% = 1 Then Stop
                Next R%

                ''If IY% = 3 AndAlso f% = 2 Then
                ''    Open PutFdrv$ + "Runfiles\" + "Debug.dat" For Output As #6
                ''
                ''    For R% = 1 To NR%
                ''        'Print #6, IY%, R%, AGX(R%), AGM(R%), Str$(aiddon(R%)), Str$(aidrec(R%)), Str$(AID(R%)), RegName$(R%)
                ''        Print #6, R%; AGX(R%, f%); AGM(R%, f%); AGPP(R%, f%); AGDEM(R%, f%); _
                ''            cumstk(R%, f%); FPRI(R%, f%); EXRATE(R%); RegName$(R%)
                ''    Next R%
                ''    Close #6
                ''End If

                wxc = AMAX(0.001, wxc)
                wmd = AMAX(0.001, wmd)
                swxc(f%) = wxc
                swmd(f%) = wmd

                'jrs 2021/01/08
                Call UpdateXSPhysicalwithDyad(wxc, AGX, f)
                'jrs 2024/11/13
                Call UpdateMSPhysicalwithDyad(wmd, AGM, f)

                Dim WT = (wxc + wmd) / 2
                'bbh 2020/12/10 start  Possibly help keep WT from growing so much when ylm around world is driving up production and stocks
                If IY% = 2 And f% = 1 Then WTMul = WT / totalstbase
                If f% = 1 Then
                    WT = CSng(WT - (WT / totalstbase - WTMul) * totalstbase * 0.3)
                End If
                WTFoodType(f%) = WT 'bbh 2022/10/27 Save for next type step and possible export constraint
                'bbh 2020/12/10 end

                '   All but First 2:  Trade Coefficient Update
                '   ------------------------------------------

                For R% = 1 To NR%
                    '            If f% = 1 Then
                    'xbase = agpp(R%, f%) * (1 + AGPGRMA(R%, f%)) + AGDEM(R%, f%) * (1 + agdemgrma(R%, f%)) 'For entrepot economies
                    '                xbase = agpp(R%, f%) + AGDEM(R%, f%)   'For entrepot economies
                    '                xbase = agpp(R%, f%) - AGLOSSPROD(R%, f%) + AGDEM(R%, f%)
                    '            ElseIf f% = 2 Then
                    'xbase = slvhrd(R%) * slr * (1 + AGPGRMA(R%, f%)) + AGDEM(R%, f%) * (1 + agdemgrma(R%, f%))
                    'dsr 2015/04/13 Change LVHERD(R%) * slr to agpp(R%, f%)
                    '                xbase = slvhrd(R%) * slr + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) - AGLOSSPROD(R%, f%) + AGDEM(R%, f%)
                    '            Else  'f%=3
                    'xbase = AGP(R%, f%) * (1 + AGPGRMA(R%, f%)) + AGDEM(R%, f%) * (1 + agdemgrma(R%, f%))
                    '                 xbase = AGP(R%, f%) + AGDEM(R%, f%)
                    '                xbase = agpp(R%, f%) - AGLOSSPROD(R%, f%) - AGLOSSPROD(R%, 4) + AGDEM(R%, f%)
                    '           End If
                    'dsr 2015/11/02 Simply define xbase as post-loss production pluse demand
                    Dim xbase = AGP(R%, f%) + AGDEM(R%, f%)   'For entrepot economies
                    Dim xhwmod = 0!
                    'wv 2022/08/22 allow for even faster export adjustments 'allow faster adjustment for small exporters. changed 0.6 to 0.25
                    If AGX(R%, f%) < 0.1 * xbase Then
                        xhwmod = xhw * 0.25! ' 0.6
                    Else
                        xhwmod = xhw
                    End If
                    XKAVE(R%, f%) = XKAVE(R%, f%) * xhwmod +
                    (1.0! - xhwmod) * AGX(R%, f%) / AMAX(0.00001, xbase)
                    'bbh 2022/10/27 Add If-then block because
                    '   need to increase exports when crop stocks of countries like DRC and CAR start to rise
                    'If IY% + 2016 >= 2022 And RegName$(R%) = "Micronesia" And f% = 3 Then Stop
                    'If RegName$(R%) = "Congo, Dem. Republic of the" And IY% + 2016 >= 2040 And f% = 1 Then Stop
                    If f% = 1 And SFSTOCK(R%, f%) > 0.2 * xbase Then
                        XKAVE(R%, f%) = CSng(XKAVE(R%, f%) + 0.005)
                    End If
                    XKAVE(R%, f%) = AMAX(0.00001, Amin(XKAVMAX(R%, f%), XKAVE(R%, f%)))
                    'mkave(R%, f%) = mkave(R%, f%) * mhw + (1! - mhw) _
                    '* AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%) * (1 + agdemgrma(R%, f%)))
                    '            mkave(R%, f%) = mkave(R%, f%) * mhw + (1! - mhw) _
                    '* AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%))

                    'bbh 2019/03/15 START Yemen related
                    '            mkave(R%, f%) = mkave(R%, f%) * mhw + (1! - mhw) _
                    '* AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%))
                    'bbh 2022/10/27 Add If-then block because
                    '   need to increase imports when fish stocks of countries like Sierra Leone fall sharply
                    If f% = 3 And SFSTOCK(R%, f%) < 0.1 * agmbase Then
                        mkave(R%, f%) = CSng(mkave(R%, f%) + 0.0075)
                    End If

                    'bbh 2022/10/27 Added AGP to import base
                    'mkave(R%, f%) = mkave(R%, f%) * mhwag(R%) + (1! - mhwag(R%)) _
                    '       * AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%))
                    mkave(R%, f%) = mkave(R%, f%) * mhwag(R%) + (1.0! - mhwag(R%)) _
                   * AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%) + AGP(R%, f%))
                    '            If RegName$(R%) = "Yemen" Then
                    '                mkave(R%, f%) = mkave(R%, f%) * 0 + (1! - 0) _
                    '                    * AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%))
                    '            Else
                    '                mkave(R%, f%) = mkave(R%, f%) * mhw + (1! - mhw) _
                    '                    * AGM(R%, f%) / AMAX(0.001, AGDEM(R%, f%))
                    '            End If
                    'bbh 2019/03/05 for Yemen - END

                    If f% = 1 Then
                        'sgh and dsr 2015/07/24 Add dimension to msf
                        '                mkave(R%, f%) = Amin(mkave(R%, f%), 0.6 * GDPPOT(R%) / _
                        '                    (AMAX(0.001, AGDEM(R%, f%)) * msf(R%) * swapi(f%)))
                        'jrs 2018/12/18 added AMAX to msf
                        '                mkave(R%, f%) = Amin(mkave(R%, f%), 0.6 * GDPPOT(R%) / _
                        '                    (AMAX(0.001, AGDEM(R%, f%)) * msf(R%, 1) * swapi(f%)))
                        'bbh 2022/10/27 changed the denominator in the line below, then eliminated
                        'mkave(R%, f%) = Amin(mkave(R%, f%), 0.6 * GDPPOT(R%) / _
                        '(AMAX(0.001, AGDEM(R%, f%) + AGP(R%, f%)) * AMAX(msf(R%, 1), 0.001) * SWAPI(f%)))

                        'bbh 2022/10/27 Another restriction on crop imports; Burundi and CAR issue
                        'bbh 2022/12/13 wrapped mkave calc in if-then block so as not to impose the 30% of GDP import maximum on countries with base year values above that (Djibouti)
                        If f% = 1 And msfi(R%, f%) * iAGM(R%, f%) * swapi(f%) / GDPI(R%) < 0.3 Then
                            mkave(R%, f%) = CSng(Amin(mkave(R%, f%), 0.3 * GDPPOT(R%) /
                            (AMAX(0.001, AGDEM(R%, f%) + AGP(R%, f%)) * AMAX(msf(R%, 1), 0.001) * swapi(f%))))
                        End If
                        'If IY% + 2016 >= 2036 And RegName$(R%) = "Central African Republic" And f% = 1 Then Stop
                    End If
                    mkave(R%, f%) = AMAX(0.00001, Amin(MKAVMax(R%, f%), mkave(R%, f%)))
                    AGX(R%, f%) = AGX(R%, f%) * WT / wxc
                    SAGX(R%, f%) = AGX(R%, f%) 'bbh 2022/10/27 Save for use in constraining growth of very large food exporters
                    AGM(R%, f%) = AGM(R%, f%) * WT / wmd

                    'bbh 2020/08/06 start Moved block below from earlier because was being done with mix of this year's AGP and AGDEM
                    'with preliminary estimate of AGM and AGX; for fast-changing countries this was messing up adjustment
                    '   All but First 2:  Comnputation of Surplus-Deficit for Feedback to AGX and AGM in Next Year
                    '   ------------------------------------------------------------------------------------------

                    Dim StBase = AGDEM(R%, f%) + AGP(R%, f%)
                    Dim prodterm = AGP(R%, f%)

                    'bbh 2020/12/10 Replaced 2 lines below because wtmul terms were messing up badly in some situations
                    'Dim wtmul = (swxc(f%) + swmd(f%)) / 2 'anticates subsequent adjustment for global imbalance
                    'Dim TradeTerm = AGM(R%, f%) * wtmul / swmd(f%) - AGX(R%, f%) * wtmul / swxc(f%)
                    Dim TradeTerm = AGM(R%, f%) - AGX(R%, f%) 'bbh 2020/12/10

                    surpdef(R%, f%) = cumstk(R%, f%) +
                prodterm + TradeTerm - AGDEM(R%, f%) - agdstl * StBase

                Next R%
            Next f%
            'bbh 2020/08/06 end

            '   All but First 2:  Stocks, Shortages, Calorie Availability, and Global Production Costs
            '   --------------------------------------------------------------------------------------

            Dim wfstk = 0!
            Dim swfstk = 0!
            Dim wstbase = 0!
            Dim wkag = 0!
            Dim wagp = 0!

            For R% = 1 To NR%
                '   If RegName$(R%) = "Burundi" AndAlso IY% >= 22 Then Stop
                '        If GRPS(1, R) = "Belize" Then Stop
                For f% = 1 To 3
                    'dsr 2015/03/26 Change next line since AGP is now post-loss production
                    'prodterm = AGP(R%, f%) * (1! - LOSS(R%, f%))
                    Dim prodterm = AGP(R%, f%)
                    Dim TradeTerm = -AGX(R%, f%) + AGM(R%, f%)
                    Dim StkAdj = prodterm - AGDEM(R%, f%) + TradeTerm
                    CumStkNegOK(R%, f%) = SCumStkNegOK(R%, f%) + StkAdj 'bbh 2022/10/27
                    SCumStkNegOK(R%, f%) = CumStkNegOK(R%, f%) 'bbh 2022/10/27
                    ' wv 2021/04/28 added amax so that stkadj never exceeds the scumstk
                    If StkAdj <= 0 Then
                        StkAdj = AMAX(StkAdj, -1 * scumstk(R%, f%))
                    End If
                    'jrs 2007/01/12 no double counting If f% = 1 Then swfstk = swfstk + cumstk(R%, f%)
                    'dsr 2015/07/01 Compute carryover world crop stocks, including shortages
                    If f% = 1 AndAlso RegiontoSum(R%) Then swfstk = swfstk + scumstk(R%, f%)
                    'dsr 2015/07/01 Compute domestic stocks of crops, meat, and fish; note that this includes shortages
                    '    and can be < 0

                    'bbh 2019/03/15 and dkb 2019/03/06 for Yemen - START
                    ' wv 2021/04/28 added AMAX statement such that stkadj never exceeds the scumstk
                    If agstkadjsw(R%) > 0.5 Then
                        cumstk(R%, f%) = scumstk(R%, f%)
                    Else
                        cumstk(R%, f%) = AMAX(scumstk(R%, f%) + StkAdj, 0)
                    End If
                    'dkb 2019/03/06 for Yemen - END

                    '            cumstk(R%, f%) = scumstk(R%, f%) + StkAdj
                    'dsr 2015/07/01 Save domestic stocks, including shortages for next year's calculations
                    scumstk(R%, f%) = cumstk(R%, f%)

                    'dsr 2015/07/01 This block computes adjustment factors based on domestic stocks, including shortages
                    '    These are used later in computations of desired agriculture investment and livestock herds
                    'dsr 2015/06/22 Use post loss production to compute stock base
                    Dim StBase = AGDEM(R%, f%) + AGP(R%, f%)
                    'dsr 2015/07/01 Compute adjustment factors based on stocks
                    Diff1 = cumstk(R%, f%) - agdstl * StBase
                    Diff2 = cumstk(R%, f%) - scumstk(R%, f%) 'bbh 2022/11/28 Note:  Will always be 0 given sCumStk(r%,f%)=CumStkr%,f%) just above; consider review
                    'Call ADJSTR(wstbase, Diff1, Diff2, -0.4, -0.8, mulwst)
                    'Call ADJSTR(agdstl * stbase, Diff1, Diff2, -0.1, -0.2, mulst(R%))
                    If f% = 1 Then
                        'Call ADJSTR(agdstl * StBase, Diff1, Diff2, -0.2, -0.4, mulst(R%, f%))
                        'bbh 2018/03/09 Here and for mulwst, impacts on investment and KAG are slow,
                        '   so need lower first order and higher second order term to avoid overshoot
                        Call ADJSTR(agdstl * StBase, Diff1, Diff2, -0.1, -0.3, mulst(R%, f%))
                    Else
                        Call ADJSTR(agdstl * StBase, Diff1, Diff2, -0.05, -0.1, mulst(R%, f%))
                    End If

                    'dsr 2015/07/01 This block computes some intermediate values at the global level, which are used
                    '    below to calculate an adjustment factor based on global crop stocks, which affects investment later.
                    'jrs 2007/01/12 no double counting If f% = 1 Then
                    'dsr 2015/06/15 Compute current world crop stocks, including shortages, capital in agriculture
                    '    and crop production
                    If f% = 1 AndAlso RegiontoSum(R%) Then
                        wfstk = wfstk + cumstk(R%, f%)
                        'dsr 2015/06/15 Use post loss production to compute stock base
                        wstbase = wstbase + AGDEM(R%, f%) + AGP(R%, f%)
                        wkag = wkag + LD(R%, 1) * CLD(R%) + KAG(R%)
                        wagp = wagp + byl(R%) * LD(R%, 1)
                    End If
                    'dsr 2015/07/01 Move now to actual stocks and current shortages
                    '    Replace with cleaner code keep historic shortages distinct from current shortages
                    '    First block calculates stocks and shortages
                    '            If (cumstk(R%, f%) < 0!) Then
                    '                If StkAdj < 0 Then
                    '                    sho(R%, f%) = -StkAdj
                    '                Else
                    '                    sho(R%, f%) = 0
                    '                End If
                    '                FSTOCK(R%, f%) = 0!
                    '            Else
                    '                sho(R%, f%) = 0
                    '                FSTOCK(R%, f%) = cumstk(R%, f%)
                    '            End If
                    If SFSTOCK(R%, f%) + StkAdj < 0! Then
                        FSTOCK(R%, f%) = 0
                        sho(R%, f%) = -(StkAdj + SFSTOCK(R%, f%))
                    Else
                        FSTOCK(R%, f%) = SFSTOCK(R%, f%) + StkAdj
                        'bbh 2020/07/01  To avoid extreme values in scenarios and bad behavior in IFsEco of FSTOCK as ST
                        If f% = 1 Then FSTOCK(R%, f%) = Amin(FSTOCK(R%, f%), 6 * agdstl * StBase)
                        sho(R%, f%) = 0
                    End If

                    'bbh 2020/07/01 start Same logic as in IFsEco for manufactures and services
                    'probably not needed for manufactures and energy where stocks do persist
                    If FSTOCK(R%, f%) > StBase * agdstl Then
                        FSTOCK(R%, f%) = CSng(FSTOCK(R%, f%) - 0.1 * (FSTOCK(R%, f%) - StBase * agdstl))
                    End If
                    'bbh 2020/07/01 end

                    'bbh 2022/11/28 Any reductions in FSTOCK need to be reflected in cumulative variables - 4 lines added to asssure the cumulative variables
                    '   equal FSTOCK when above FSTOCK (and therefore positive); cumulative variables can go negative, however, and FSTOCK not allowed to do so
                    If cumstk(R%, f%) > FSTOCK(R%, f%) Then cumstk(R%, f%) = FSTOCK(R%, f%)
                    If scumstk(R%, f%) > FSTOCK(R%, f%) Then scumstk(R%, f%) = FSTOCK(R%, f%)
                    If CumStkNegOK(R%, f%) > FSTOCK(R%, f%) Then CumStkNegOK(R%, f%) = FSTOCK(R%, f%)
                    If SCumStkNegOK(R%, f%) > FSTOCK(R%, f%) Then SCumStkNegOK(R%, f%) = FSTOCK(R%, f%)

                    'dsr 2015/07/01 This block reduces use components due to shortages.
                    '    Note, do not recompute AGDEM total as this drives other things later in the model
                    'ReductionFactor = (AGDEM(R%, f%) - LOSS(R%, f%) - sho(R%, f%)) / (AGDEM(R%, f%) - LOSS(R%, f%)) OLD CODE

                    If sho(R%, f%) > 0 Then
                        'jrs 2021/04/16 ReductionFactor shouldn't go negative, added AMAX
                        'mti 2022/08/10 start
                        Dim ReductionFactor As Single = CSng(0.9 * sReductionFactor(R%) + 0.1 * AMAX((AGDEM(R%, f%) - sho(R%, f%)) / AGDEM(R%, f%), 0))
                        'dim ReductionFactor as single = csng(AMAX((AGDEM(R%, f%) - sho(R%, f%)) / AGDEM(R%, f%), 0))
                        'mti 2022/08/10 end
                        FDEM(R%, f%) = FDEM(R%, f%) * ReductionFactor
                        FEDDEM(R%, f%) = FEDDEM(R%, f%) * ReductionFactor
                        FMDEM(R%, f%) = FMDEM(R%, f%) * ReductionFactor
                        INDEM(R%, f%) = INDEM(R%, f%) * ReductionFactor
                        AGLOSSTRANS(R%, f%) = AGLOSSTRANS(R%, f%) * ReductionFactor
                        'mti 2022/08/10 start
                        sReductionFactor(R%) = ReductionFactor
                    Else
                        sReductionFactor(R%) = 1
                        'mti 2022/08/10 end
                    End If

                    'dsr 2015/07/01 Given final values of FDEM here calculate final values of AGLOSSCONS,
                    '    GRAMSPC, CLPC, and PROTEINOC
                    'kbn 2017/08/28 Recompute consumption losses here
                    Dim aglossconspercpredfunc = 0!
                    'jrs 2024/06/14 Call XYTABL("GDP/Capita (PPP 2011) Versus Consumption Losses (Log)", GDPPCP(R%), aglossconspercpredfunc, context)
                    Call XYTABL(TfNameConsLoss, GDPPCP(R%), aglossconspercpredfunc, context)
                    Dim aglossconspercpred = Amin(AMAX(aglossconspercpredfunc * aglossconsm(R%, f%), 5), 18)

                    AGLOSSCONS(R%, f%) = FDEM(R%, f%) * (aglossconspercpred / 100)
                    GRAMSPC(R%, f%) = (FDEM(R%, f%) * 1000000) / (365 * POP(R%))
                    'dsr 2015/06/15 Data used to calculate ratio already accounts for consumption loss rate in first year
                    '               so only adjust with changes from first year
                    'kbn 2017/10/19 Ratio in below code is reversed. This drops calories. Comment out and fix ratio. Also add Barry's fix to code
                    'below so that run does not crash when using multipliers.
                    'CLPC(R%, f%) = GRAMSPC(R%, f%) / (GramsToCalRatI(R%, f%) * (aglossconspercpred / aglossconsperci(R%, f%)))
                    'CLPC(R%, f%) = GRAMSPC(R%, f%) / (GramsToCalRatI(R%, f%) * (aglossconsperci(R%, f%) / aglossconspercpred))                   
                    CLPC(R%, f%) = (GRAMSPC(R%, f%) / GramsToCalRatI(R%, f%)) *
                    (1 - aglossconspercpred / 100) / (1 - aglossconsperci(R%, f%) / 100)
                    PROTEINPC(R%, f%) = ProtToCalRatI(R%, f%) * CLPC(R%, f%)

                Next f%

                'dsr 2015/06/26 Sum to get total calories and protein per capita
                CLPC(R%, 4) = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)

                'jrs 2019/05/03 *** BEGIN ***
                If clpcexovals(R%, 1) > -100 Then
                    CLPC(R%, 1) = clpcexovals(R%, 1)
                End If
                If clpcexovals(R%, 2) > -100 Then
                    CLPC(R%, 2) = clpcexovals(R%, 2)
                End If
                If clpcexovals(R%, 3) > -100 Then
                    CLPC(R%, 3) = clpcexovals(R%, 3)
                End If
                If clpcexovals(R%, 4) > -100 Then
                    CLPC(R%, 4) = clpcexovals(R%, 4)
                Else
                    If clpcexovals(R%, 1) > -100 OrElse clpcexovals(R%, 2) > -100 OrElse clpcexovals(R%, 3) > -100 Then
                        CLPC(R%, 4) = CLPC(R%, 1) + CLPC(R%, 2) + CLPC(R%, 3)
                    End If
                End If
                'jrs 2019/05/03 *** END ***

                PROTEINPC(R%, 4) = PROTEINPC(R%, 1) + PROTEINPC(R%, 2) + PROTEINPC(R%, 3)
                'jrs 2014/11/25 claval_1 = sho(R%, 1) * FDEM(R%) / AGDEM(R%, 1) + sho(R%, 2) * lvcfr(R%)

                'dsr 2015/03/26 Comment out following code since calories adjustments dealt with above
                ''        claval_1 = sho(R%, 1) * FDEM(R%, 1) / AGDEM(R%, 1) + sho(R%, 2) * lvcfr(R%)
                ''        claval_1 = AMAX(0, claval_1) ' Make sure shortages are positive
                'jrs 2014/11/25 CLAVAL(R%) = AMAX(0, sclavf(R%) * (FDEM(R%) + AGDEM(R%, 2) * lvcfr(R%) - claval_1))
                'dsr 2015/03/26 Change how CLAVAL is computed
                '        CLAVAL(R%) = AMAX(0, sclavf(R%) * (FDEM(R%, 1) + AGDEM(R%, 2) * lvcfr(R%) - claval_1))
                CLAVAL(R%) = AMAX(0, CLPC(R%, 4)) * POP(R%)
                '    If RegName$(R%) = "Somalia" Then Stop

            Next R%

            'jrs 2014/12/03 moved from Populat.bas
            '   All Years 2: Calories per Capita
            '   -------------------------------
            'dsr 2015/03/26 Next line not needed since deal with calories above
            '    Dim MeatAndFishPct As Single
            '    If IY > 1 Then
            '        Sum = 0
            '        For R% = 1 To NR%
            '            Sum = Sum + CLAVAL(R%)
            '            'jrs 2014/11/25 CLPC(R%) = AMAX(1500, CLAVAL(R%) / POP(R%))
            '            CLPC(R%, 4) = AMAX(1500, CLAVAL(R%) / POP(R%))
            '            'jrs 2014/12/05
            '            'Crop, Meat and Fish
            '            '-------------------
            '            Call AnalFunc("Percentage of calories from meat and fish (combined)", _
            '            Array(GDPPCP(R%), POPURBAN(R%) / POP(R%) * 100), MeatAndFishPct, _
            '            Array("GDPPCP", "POPURBAN%"))
            '            MeatAndFishPct = Amin(AMAX(MeatAndFishPct / 100, 0.035), 1) 'dsr 2015/03/17 Set Minimum based on historical data
            '
            '            CLPC(R%, 1) = (CLPC(R%, 4) * (1 - MeatAndFishPct)) * CLPCShift(R%, 1)
            '            CLPC(R%, 2) = (CLPC(R%, 4) - CLPC(R%, 1)) * CLPCShift(R%, 2)
            '            CLPC(R%, 3) = CLPC(R%, 4) - CLPC(R%, 1) - CLPC(R%, 2)
            '        Next R%
            '        WCLPC = Sum / WPOP
            '    Else
            '        'Compute Shift factors
            '        '---------------------
            '        For R% = 1 To NR%
            '            Call AnalFunc("Percentage of calories from meat and fish (combined)", _
            '            Array(GDPPCP(R%), POPURBAN(R%) / POP(R%) * 100), MeatAndFishPct, _
            '            Array("GDPPCP", "POPURBAN%"))
            '            MeatAndFishPct = Amin(AMAX(MeatAndFishPct / 100, 0.035), 1) 'dsr 2015/03/17 Set Minimum based on historical data
            '
            '            CLPCShift(R%, 1) = CLPC(R%, 1) / (CLPC(R%, 4) * (1 - MeatAndFishPct))
            '            If CLPC(R%, 2) + CLPC(R%, 3) > 0 Then
            '                CLPCShift(R%, 2) = CLPC(R%, 2) / (CLPC(R%, 2) + CLPC(R%, 3))
            '            End If
            '        Next R%
            '    End If

            'dsr 2015/07/02 Calculate adjustment factor based on global crop stocks. This affects
            '    agricultural investment later on.
            Diff1 = wfstk - agdstl * wstbase
            Diff2 = wfstk - swfstk
            'Call ADJSTR(wstbase, Diff1, Diff2, -0.4, -0.8, mulwst)
            'bbh 2018/03/09 Here and for mulst, impacts on investment and KAG are slow,
            '   so need lower first order and higher second order term to avoid overshoot
            Dim mulwst = 0!
            Call ADJSTR(agdstl * wstbase, Diff1, Diff2, -0.1, -0.3, mulwst)
            'Call ADJSTR(agdstl * wstbase, Diff1, Diff2, -0.3, -0.9, mulwst)
            'dsr 2015/07/02 DesStocks not used anywhere
            '    DesStocks = agdstl * wstbase
            ' mulstocks = (DesStocks / AMAX(0.5 * DesStocks, Amin(1.5 * DesStocks, wfstk)))
            'dsr 2015/07/02 mulstocks not used anywhere
            '    mulstocks = (DesStocks / AMAX(0.5 * DesStocks, Amin(2 * DesStocks, wfstk)))

            'dsr 2015/07/02 The following value, the ratio of global kapital in agriculture to global
            '    crop production, is used later as part of the crop price determination, so move to price section
            '    wkagagpr = wkag / WAGP

            '   All but First 2:  Prices
            '   ------------------------
            '   Compute regional prices crops by equilibrium
            '   Compute regional prices for meat from crops (major input and competitor);
            '       Adjust meat supply to demand via livestock
            '   Compute global prices for fish by global equilibrium
            Dim wkagagpr = wkag / wagp
            Dim WGDP = 0!
            Dim WAGDEM = 0!
            Dim wstkbasefish = 0!
            Dim wcumstkbalancefish = 0!
            Dim wcumstkbalancechangefish = 0!
            Dim balance = 0!
            For R% = 1 To NR%
                For f% = 1 To 3
                    If f% = 1 Then 'crop prices determined by local stocks
                        Dim ProdCost = LD(R%, 1) * CLD(R%) + KAG(R%) 'dsr 2015/06/29 Total Cost
                        Dim Cost = ProdCost / (byl(R%) * LD(R%, 1)) 'dsr 2015/06/29 Cost per ton
                        'dsr 2015/06/29 Use post-loss production for demand base
                        dmbase = AGDEM(R%, 1) + AGP(R%, 1)
                        'balance = CumStk(R%, 1) - dmbase * agdstl 'bbh 2022/11/28 replace with below in case CumStk carries positive when FSTOCK is negative
                        balance = Amin(SFSTOCK(R%, 1), CumStkNegOK(R%, 1)) - dmbase * agdstl
                        'bbh 2019/02/27
                        'Call ADJSTR(dmbase, balance, balance - sbalance(R%, 1), pricr1, fpricr2, Mul)
                        Mul = 0!
                        Call ADJSTR(dmbase, balance, balance - sbalance(R%, 1), fpricr1, fpricr2, Mul)
                        '            MARKUP(r%) = MARKUP(r%) * Mul
                        '            FPRI(r%, f%) = SWAPI(f%) * COST / COSTI(r%) * MARKUP(r%)
                        '            FPRI(r%, f%) = SWAPI(f%) * COST / COSTI(r%) * Mul
                        FPRI(R%, 1) = swapi(1) * wkagagpr / wkagagpri * Mul
                        'If RegName$(R%) = "United States of America" AndAlso IY% >= 67 Then Stop
                        'dsr 2015/06/29 Cap domestic price at 3x global price
                        '                FPRI(R%, f%) = AMAX(0.01, Amin(FPRI(R%, f%), 1000))
                        FPRI(R%, 1) = AMAX(0.01, Amin(FPRI(R%, 1), 3 * WAP(1)))
                        sbalance(R%, 1) = balance
                    ElseIf f% = 2 Then 'meat prices follow from world crop prices
                        '        MAFPRI(r%) = fprihw * MAFPRI(r%) + (1 - fprihw) * FPRI(r%, 1)
                        MAFPRI(R%) = fprihw * MAFPRI(R%) + (1 - fprihw) * WAP(1)
                        FPRI(R%, 2) = swapi(2) * MAFPRI(R%) / swapi(1) 'MProdCost / MProdCostI(r%)
                        'dsr 2015/06/29 Cap domestic price at 3x global price
                        FPRI(R%, 2) = AMAX(0.01, Amin(FPRI(R%, 2), 3 * WAP(2)))
                        '        FPRI(r%, 2) = SWAPI(2) * (MAFPRI(r%) * 0.4 + SWAPI(1) * 0.6) / SWAPI(1)
                        'MProdCost / MProdCostI(r%)
                        ' Pass through part of local crop price (feed) but not all
                    ElseIf f% = 3 AndAlso RegiontoSum(R%) Then 'fish prices determined by global stocks
                        'dsr 2015/06/29 Use post-loss production for demand base
                        dmbase = AGDEM(R%, 3) + AGP(R%, 3)
                        wstkbasefish = wstkbasefish + dmbase
                        balance = cumstk(R%, 3) - dmbase * agdstl
                        wcumstkbalancefish = wcumstkbalancefish + balance
                        wcumstkbalancechangefish = wcumstkbalancechangefish + balance - sbalance(R%, 3)
                        sbalance(R%, 3) = balance
                    End If
                    '            agdemgrma(R%, f%) = agdemgrma(R%, f%) * 0.6 + (AGDEM(R%, f%) - sagdem(R%, f%)) / AMAX(0.0001, sagdem(R%, f%)) * 0.4
                    '            AGPGRMA(R%, f%) = AGPGRMA(R%, f%) * 0.6 + (AGP(R%, f%) - SAGP(R%, f%)) / AMAX(0.0001, SAGP(R%, f%)) * 0.4
                Next f%
            Next R%
            'dsr 2015/06/29 Calculate global fish price index based on global stocks and assign same value to all countries
            'Call ADJSTR(wstkbasefish, wcumstkbalancefish, wcumstkbalancechangefish, _
            'fprimt1 * 1.5, fprimt2 * 1.5, Mul)
            Call ADJSTR(wstkbasefish, wcumstkbalancefish, wcumstkbalancechangefish,
        fprimt1 * 2.5!, fprimt2 * 2.5!, Mul)
            WAP(3) = WAP(3) * Mul
            For R% = 1 To NR%
                FPRI(R%, 3) = WAP(3)
            Next R%
            '   All but First 2:  Agricultural Returns
            '   --------------------------------------
            'dsr 2015/07/02 Revisit this section; some calculations, e.g., wkag, wagdem, and wagp, look repetitive
            wagp = 0
            wkag = 0
            Dim wfprofitr = 0!
            Dim wld = 0!
            Dim wldyl = 0!
            For R% = 1 To NR%
                For f% = 1 To 3
                    'dsr 2015/06/29 Next line only needed for crops, so move
                    '            ProdCost = LD(R%, 1) * CLD(R%) + KAG(R%)
                    If f% = 1 Then
                        Dim ProdCost = LD(R%, 1) * CLD(R%) + KAG(R%)
                        'jrs 2007/01/12 no double counting
                        If RegiontoSum(R%) Then
                            wkag = wkag + ProdCost
                            'dsr 2015/06/15 Use post-loss production
                            wagp = wagp + AGP(R%, 1)
                            WAGDEM = WAGDEM + AGDEM(R%, 1)
                            WGDP = WGDP + GDP(R%)
                            'jrs 2007/01/12 I moved these 2 lines from the line after the next
                            'to avoid having 2 if statements
                            wld = wld + LD(R%, 1)
                            wldyl = wldyl + LD(R%, 1) * byl(R%)
                        End If
                        'dsr 2015/06/29 Return should reflect post loss production, so adjust next equation
                        '                AGReturn(1) = (byl(R%) * LD(R%, 1) * FPRI(R%, 1)) / ProdCost
                        AGReturn(1) = (byl(R%) * LD(R%, 1) * FPRI(R%, 1) * AGLOSSPROD(R%, 1) / AGP(R%, 1)) / ProdCost
                        '                wld = wld + LD(R%, 1)
                        '                wldyl = wldyl + LD(R%, 1) * byl(R%)
                    ElseIf f% = 2 Then
                        'dsr 2015/06/15 Costs and returns appear to only deal with livestock, may want to change
                        Dim MProdCost = (LD(R%, 2) * GLDCAP(R%) + FEDDEM(R%, 1)) * MAFPRI(R%)
                        AGReturn(2) = (LVHERD(R%) * FPRI(R%, 2)) / AMAX(0.0001, MProdCost)
                    Else
                        'Placeholder; not clear what to do with fish cost; tie to price?
                        'dsr 2015/06/09 Replace FISH with AGP(R%, 3)
                        'dsr 2015/06/15 Use pre-loss production to calculate production costs
                        Dim MProdCost = AGP(R%, 3) * MAFPRI(R%)
                        'dsr 2015/06/15 Use post-loss production to calculate return
                        AGReturn(3) = (AGP(R%, 3) * FPRI(R%, 3)) / AMAX(0.0001, MProdCost)
                    End If
                    If IY% = 2 Then AGReturnI(R%, f%) = AGReturn(f%)
                Next f%

                '   All but First 2:  Profit Ratios
                '   -------------------------------

                For f% = 1 To 3
                    FPROFITR(R%, f%) = AGReturn(f%) / AGReturnI(R%, f%)
                Next f%
                'jrs 2007/01/12 no double counting
                If RegiontoSum(R%) Then
                    wfprofitr = wfprofitr + AGP(R%, 1) * FPROFITR(R%, 1)
                End If
            Next R%
            Dim wyl = wldyl / wld
            Dim wylkagr = wyl / wkag
            wkagagpr = wkag / wagp
            wfprofitr = wfprofitr / wagp

            '   All but First 2:  Investment
            '   ----------------------------

            'wkagagpmul = (wkag / wagp) / wkagagpri
            'wagdemrmul = (WAGDEM / WGDP) / WAGDEMRI
            'wylkagmul = wylkagr / wylkagri
            Dim wkagagpmul = (wkag / wagp) / swkagagpr
            Dim wagdemrmul = (WAGDEM / WGDP) / swagdemr
            Dim wylkagmul = wylkagr / swylkagr
            swylkagr = wyl / wkag
            swkagagpr = wkag / wagp
            swagdemr = WAGDEM / WGDP

            Dim mulrprof = 0!
            For R% = 1 To NR%
                Diff1 = FPROFITR(R%, 1) - wfprofitr '1
                Diff2 = FPROFITR(R%, 1) - sfprofitr(R%, 1)
                Call ADJSTR(1, Diff1, Diff2, elinag1, elinag2, mulrprof)

                'bbh 2017/05/02 start
                'INAG(R%) = GDP(R%) * SINAGR(R%) * mulwst * wagdemrmul * mulst(R%, 1) '* wylkagmul
                Dim TarINAG = GDP(R%) * SINAGR(R%) * mulwst * wagdemrmul * mulst(R%, 1) '* wylkagmul
                Dim TarSINAGR = TarINAG / GDP(R%)
                Dim SmSINAGR = SINAGR(R%) * 0.6 + TarSINAGR * 0.4
                'If IY = 11 And R = 79 Then Stop
                INAG(R%) = CSng(SmSINAGR * GDP(R%))
                'bbh 2017/05/02 end

                'INAG(R%) = GDP(R%) * SINAGR(R%) * mulwst * mulrprof
                INAG(R%) = CSng(AMAX(Amin(1.3 * SINAG(R%), INAG(R%)), 0.75 * SINAG(R%)))
                'If RegName$(R%) = "Turkmenistan" AndAlso IY% >= 15 Then Stop

                '        TKAG = (YL(r%) / byl(r%) * KAG(r%) ^ Alpha(r%)) ^ (1 / Alpha(r%))
                '        CINAG = TKAG - (KAG(r%) - KAG(r%) / AMAX(lks(1), 0.001))
                '        CINAG = CINAG / (1 - ialk(r%))
                '        INAG(r%) = 0.9 * INAG(r%) + 0.1 * AMAX(0.01, CINAG)

                INAG(R%) = CSng(AMAX(0.005 * KAG(R%), Amin(0.4 * KAG(R%), INAG(R%))))
                SINAG(R%) = INAG(R%)
                SINAGR(R%) = SINAG(R%) / GDP(R%) '* 0.995
                INAG(R%) = INAG(R%) * aginvm(R%)
                'INAG(R%) = INAG(R%) * mulstocks * wkagagpmul * wagdemrmul * wylkagmul * aginvm(R%)
                'If RegName$(R%) = "Lithuania" AndAlso IY% >= 35 Then Stop

                '  This is a key long-term adjustment dynamic subject to many forces
                '  Let base unit/GDP drift slowly with global stock levels (MulWSt) - residual term
                '   and with regionally-specific profit rates (MulRProf)
                '  If do not let drift with MulRProf, profit levels do not equilibrate across regions
                '  Adjust quickly in face of global stock levels (MulStocks)
                '  Multiply by changes in global agdem/GDP ratio over time (WAgDemRMul)
                '  Multiply by changes in global capital/ag production ratio (WKagAgPMul)
                'bbh 2018/03/07 changed from 0.99 to shift adjustment from stocks to yield to stocks to investment
                Dim MinInag = INAGI(R%) * (0.95) ^ IY%
                If GDP(R%) >= GDPI(R%) Then INAG(R%) = AMAX(MinInag, INAG(R%))

            Next R%
            '   All but First 2:  Translation for Economic Model
            '   ------------------------------------------------

            'dsr 2015/07/29 Need to balance value of trade by commodity and not just for agriculture as a whole
            For f% = 1 To 3
                '        WM = 0!
                '        WX = 0!
                WM(f%) = 0!
                WX(f%) = 0!
            Next f%

            For R% = 1 To NR%
                'sgh and dsr 2015/07/24 xsf and msf now have second dimension
                '        XS(R%, 1) = xsf(R%) * (AGX(R%, 1) * swapi(1) + AGX(R%, 2) * swapi(2) + AGX(R%, 3) * swapi(3))
                '        MS(R%, 1) = msf(R%) * (AGM(R%, 1) * swapi(1) + AGM(R%, 2) * swapi(2) + AGM(R%, 3) * swapi(3))
                'dsr 2015/07/29 Add code to calculate trade values by commodity
                MS(R%, 1) = 0
                XS(R%, 1) = 0
                For f% = 1 To 3
                    AGMVALUE(R%, f%) = msf(R%, f%) * AGM(R%, f%) * swapi(f%)
                    AGXVALUE(R%, f%) = xsf(R%, f%) * AGX(R%, f%) * swapi(f%)
                    'wv 2022/01/05 Added a multiplier on AGMVALUE and AGXVALUE
                    AGMVALUE(R%, f%) = AGMVALUE(R%, f%) * agmvaluem(R%, f%)
                    AGXVALUE(R%, f%) = AGXVALUE(R%, f%) * agxvaluem(R%, f%)
                Next f%
                '        XS(R%, 1) = xsf(R%, 1) * AGX(R%, 1) * swapi(1) + xsf(R%, 2) * AGX(R%, 2) * swapi(2) + _
                '            xsf(R%, 3) * AGX(R%, 3) * swapi(3)
                '        MS(R%, 1) = msf(R%, 1) * AGM(R%, 1) * swapi(1) + msf(R%, 2) * AGM(R%, 2) * swapi(2) + _
                '            msf(R%, 3) * AGM(R%, 3) * swapi(3)


                'jrs 2014/11/25 CS(R%, 1) = CSF(R%) * (FDEM(R%) * swapi(1) + AGDEM(R%, 2) * swapi(2) + AGDEM(R%, 3) * swapi(3))
                'bbh 2017/01/24
                'CS(R%, 1) = CSF(R%) * (FDEM(R%, 1) * swapi(1) + AGDEM(R%, 2) * swapi(2) + AGDEM(R%, 3) * swapi(3))
                CS(R%, 1) = CSF(R%) * (FDEM(R%, 1) * swapi(1) + FDEM(R%, 2) * swapi(2) + FDEM(R%, 3) * swapi(3))
                '   If RegName$(R%) = "Australia" Then Stop
                'dsr 2015/07/29 Need to balance value of trade by commodity and not just for agriculture as a whole
                'jrs 2007/01/12 no double counting
                If RegiontoSum(R%) Then
                    For f% = 1 To 3
                        '                WX = WX + XS(R%, 1)
                        '                WM = WM + MS(R%, 1)
                        WM(f%) = WM(f%) + AGMVALUE(R%, f%)
                        WX(f%) = WX(f%) + AGXVALUE(R%, f%)
                    Next f%
                End If

                ST(R%, 1) = FSF(R%) * (FSTOCK(R%, 1) * swapi(1) + FSTOCK(R%, 2) * swapi(2) + FSTOCK(R%, 3) * swapi(3))
                PRI(R%, 1) = FPRI(R%, 1) / swapi(1)
                IDS(R%, 1) = INAG(R%)
            Next R%
            'dsr 2015/07/29 Need to balance value of trade by commodity and not just for agriculture as a whole
            '    WTRA = (WX + WM) / 2
            For f% = 1 To 3
                Dim WTRA = (WX(f%) + WM(f%)) / 2
                For R% = 1 To NR%
                    '             XS(R%, 1) = XS(R%, 1) * WTRA / AMAX(WX, 0.001)
                    '             MS(R%, 1) = MS(R%, 1) * WTRA / AMAX(WM, 0.001)
                    AGMVALUE(R%, f%) = CSng(AGMVALUE(R%, f%) * WTRA / AMAX(WM(f%), 0.001))
                    AGXVALUE(R%, f%) = CSng(AGXVALUE(R%, f%) * WTRA / AMAX(WX(f%), 0.001))
                    MS(R%, 1) = MS(R%, 1) + AGMVALUE(R%, f%)
                    XS(R%, 1) = XS(R%, 1) + AGXVALUE(R%, f%)
                Next R%
            Next f%

            'ifsAgAllYears(2, context) 'ttt
        End Sub
        Public Sub ifsAgAllYears(fIY As Integer, context As IForecastContext)
            '   ALL TIME STEPS
            '   --------------------------------------------------------------

            'AllTimeSteps:

            'sco2 = 1 + CO2PER / 100
            If fIY >= 2 Then
                sco2 = CO2PER / 100
                If fIY = 2 Then ico2 = sco2
            End If

            '
            WFORST = 0
            Dim Mul = 0!
            Dim TabName$ = ""
            Dim Diff1 = 0!
            Dim Diff2 = 0!
            'ttt 2019/10/08 TakeOut
            Array.Copy(GDPPCP, sGDPPCP, NR + 1)
            'jrs 2024/06/14
            If IY = 1 Then
                TbNameBuiltLandPC = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Built Land/Capita (" & BaseYear & ") Linear"
                TbNameBuiltLandPC = getFunctionNameYrOrMstRct(TbNameBuiltLandPC, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")
            End If

            For R% = 1 To NR%
                'BBH 2009/12/10
                'bbh 2013/1/2 'Start do not need to save Senvylchg because done in Environment before this point
                If fIY = 1 Then
                    SENVYLCHG(R%) = 0
                    '        Else
                    '            SENVYLCHG(R%) = ENVYLCHG(R%)
                End If
                'bbh 2013/1/2 'end

                SCLAVL(R%) = CLAVAL(R%) * (1 - CIVDM(R%))
                'ttt 2019/10/08 TakenOut
                'sGDPPCP(R%) = GDPPCP(R%)
                For f% = 1 To 3
                    SFPRI(R%, f%) = FPRI(R%, f%)
                Next f%

                '   All Steps: Invesment Availability and Split to Capital and Land
                '   ---------------------------------------------------------------

                Dim iaval = AMAX(0.0001, IDS(R%, 1))
                'wv 2021/08/12 make the landarablepot scenario dependent. standard this is set to the high level, but in scenarios with stricter land policies this can be adjusted
                'landarablepotsw = 3
                If landarablepotsw = 3 Then
                    landarablepot(R%) = landarablepot(R%)
                ElseIf landarablepotsw = 2 Then
                    landarablepot(R%) = landarablepotmed(R%)
                Else
                    landarablepot(R%) = landarablepotlow(R%)
                End If

                'If RegName$(R%) = "Israel" Then Stop

                If (agon < 0.5) Then iaval = INAG(R%)
                If fIY <> 1 Then
                    'If RegName$(R%) = "United States of America" Then Stop
                    ' wv 2020/12/17 ***start updating investment routine***
                    'wv 2020/12/15 mirror the previous changes to yield, to have satk only limit the growth in yield, not the yield in total
                    Dim capterm = (KAG(R%) + 1) ^ alpha(R%)
                    Dim labterm = labagi(R%) ^ (1 - alpha(R%))
                    ' wv 2020/12/15 updated such that satk only limiy yhe growth in yield
                    Dim HypothYL, CompYL As Single
                    If IY% = 2 Then
                        HypothYL = CSng(AGTEC(R%) * cD(R%) * capterm * labterm)
                        HypothYL = AMAX(iyl(R%), iyl(R%) + ((HypothYL - iyl(R%)) * satk(R%)))
                        If R = 1 Then
                            ReDim SHypothYL(NR), SCompYL(NR)
                        End If
                        SHypothYL(R%) = HypothYL
                        'bbh 2016/11/22 start; added and used CompYl instead of byl
                        ' wv 2020/12/15 updated such that satk only limiy yhe growth in yield
                        CompYL = CSng(AGTEC(R%) * cD(R%) * KAG(R%) ^ alpha(R%) * labterm)
                        CompYL = AMAX(iyl(R%), iyl(R%) + ((CompYL - iyl(R%)) * satk(R%)))
                        SCompYL(R%) = CompYL
                    Else
                        HypothYL = CSng(AGTEC(R%) * cD(R%) * capterm * labterm)
                        ' wv 2020/12/15 updated such that satk only limiy yhe growth in yield
                        HypothYL = CSng(AMAX(SHypothYL(R%), SHypothYL(R%) + ((HypothYL - SHypothYL(R%)) * satk(R%))))
                        SHypothYL(R%) = HypothYL
                        CompYL = CSng(AGTEC(R%) * cD(R%) * KAG(R%) ^ alpha(R%) * labterm)
                        ' wv 2020/12/15 updated such that satk only limiy yhe growth in yield
                        CompYL = CSng(AMAX(SCompYL(R%), SCompYL(R%) + ((CompYL - SCompYL(R%)) * satk(R%))))
                        SCompYL(R%) = CompYL
                    End If
                    '   BBH 1/25/04 Begin V5_0
                    'RETCap = LD(R%, 1) * (HypothYL - byl(R%)) 'Yield Incr with Invest in Cap
                    'RETLD = SYL(R%) / CLD(R%)   'Yield Increase with Investment in Land
                    ' = SYL * (LD + 1/CLD) - SYL * LD
                    'RETRAT(R%) = RETLD / AMAX(RETCap, 0.001)
                    'RETCAP = LD(R%, 1) * (HypothYL - byl(R%)) 'Yield Incr with Invest in Cap
                    ' wv 2021/03/01 changed reference to the CompYL (capital L)
                    Dim RETCAP = LD(R%, 1) * (HypothYL - CompYL)
                    'bbh 2016/11/22 end
                    ' wv 2021/03/01 changed return on land to be a function of the syl, not the iyl
                    Dim RETLD = syl(R%) / CLD(R%)
                    'RETLD = iyl(R%) / CLD(R%)
                    'RETRAT(R%) = RETLD / AMAX(RETCAP, 0.001)
                    'Yield Increase with Investment in Land
                    ' wv 2020/12/17 ***end updating investment routine***
                    ' = SYL * (LD + 1/CLD) - SYL * LD
                    RETRAT(R%) = CSng(RETLD / AMAX(RETCAP, 0.001))
                    If IY = 2 Then
                        SRETR(R%) = RETRAT(R%)
                        RETRI(R%) = RETRAT(R%)
                    End If
                    Diff1 = RETRAT(R%) - RETRI(R%)
                    Diff2 = RETRAT(R%) - SRETR(R%)
                    Call ADJSTR(1, Diff1, Diff2, eliasp1, eliasp2, Mul)
                    IALK(R%) = SIALK(R%) * Mul

                    IALK(R%) = CSng(Amin(IALK(R%),
                        (0.04 * (LD(R%, 3) + LD(R%, 4)) + LD(R%, 1) * dkl) * CLD(R%) / iaval))
                    ' wv 2021/01/27 similar to satk on yield, add a land saturation term that changes investment in land based on development differences in land (0.7 reflects a land to potential land ratio)
                    ' wv 2021/03/08 made landsat and landsat country-specific
                    landsat(R%) = ((landarablepot(R%) - (LD(R%, Crop))) / landarablepot(R%))
                    landsat2(R%) = CSng(AMAX(0, landsat(R%) - 0.7))
                    ' wv 2021/01/27 change investment in land based on the level of land in use, relative to potential land. i.e. when much land is still availabe assumes to have a higher portion of investments going to land
                    ' wv 2021/01/27 added sialk to limit change in investment of land between years
                    ' wv 2021/03/08 made landsat2 country specific
                    IALK(R%) = CSng(AMAX((0.1 + (landsat2(R%))), Amin(0.8, IALK(R%))))
                    'SIALK(R%) = IALK(R%)
                    SIALK(R%) = CSng((0.8 * SIALK(R%) + 0.2 * IALK(R%)))
                    SRETR(R%) = RETRAT(R%)
                    ' wv 2021/01/27 change the calculation of syl to be based on YL(R%), and not on byl(R%)
                    ' wv 2021/03/29 above was a mistake because changing it to yl made the effect of ylm carry forward
                    ' wv 2021/03/29 temporary fix, now used SCompYl but want to change to byl(R%)
                    'syl(R%) = ylhw * SCompYL(R%) + (1 - ylhw) * syl(R%)
                    'bbh 2022/10/27 syl needs to be based on yl, not byl; can back out ylm effect
                    'If IY% + 2016 >= 2044 And RegName$(R%) = "Central African Republic" Then Stop
                    'bbh 2022/11/28 With movement to YlShift logic, syl computation easier and with Yl; commented next 4 lines
                    '            ylhwRev = ylhw 'bbh 2022/10/27 phase out historical memory when have very low stocks; 5 lines through syl(r%)=
                    '            If (CumStk(R%, 1) / (AGP(R%, 1) + AGDEM(R%, 1)) / agdstl) < 1 Then 'are below target stock level, give more weight to
                    '                ylhwRev = Amin(1, ylhw * agdstl / AMAX(0.05, CumStk(R%, 1) / (AGP(R%, 1) + AGDEM(R%, 1))))
                    '            End If
                    '            syl(R%) = ylhwRev * YL(R%) / ylm(R%) + (1 - ylhwRev) * syl(R%)
                    'syl(R%) = ylhw * YL(R%) + (1 - ylhw) * syl(R%)
                    'syl(R%) = ylhw * byl(R%) + (1 - ylhw) * syl(R%)
                    'bbh 2019/04/02
                    SYlImpactWatPrice(R%) = YLIMPACTWATPRICE(R%)
                    'wv 2020/12/11 agpy *** start updating YLLim calculation***
                    'wv 2020/12/11 agpy made YLLim -> YLLim(R%) country specific
                    'wv agpy 2020/12/09, recalculation of YLLim; major change with initial year is addition of else statement
                    If iyieldint(R%) > 0.01 Then
                        YLLim(R%) = Amin(70, iyl(R%) / iyieldint(R%))
                    Else
                        YLLim(R%) = Amin(70, 3 * iyl(R%))
                        'AMAX(ylmax(R%) * YlMaxM(R%), 1.5 * iyl(R%))
                    End If
                    ' wv 2020/12/12 agpy yllim grows with changes in agr technology
                    YLLim(R%) = Amin(70, AMAX(SYLLim(R%), SYLLim(R%) * AGTECr(R%)))
                    'YLLim(R%) = AMAX(70, YLLim(R%))
                    SYLLim(R%) = YLLim(R%)
                    ' wv 2020/12/11 agpy updated line to make satk(R%) and YLLim(R%) country specific, and to bound the change between satk between 0.1-1
                    ' to update the yield intensity in IFs based on changes in yield and YLLim
                    Dim satkhold = satk(R%) 'bbh 2022/11/28 Start: Satk-Yl were oscillating for some countries as approaches YLLim (e.g. Iceland); need to slow movement down
                    '            If IY% + 2016 >= 2095 And RegName$(R%) = "Gambia" Then Stop
                    satk(R%) = CSng(AMAX(0.1, Amin((YLLim(R%) - syl(R%)) / (YLLim(R%) - iyl(R%)), 1)) ^ ylexp)
                    If satk(R%) < satkhold Then
                        satk(R%) = CSng(AMAX(satk(R%), satkhold - 0.015))
                    Else
                        satk(R%) = CSng(Amin(satk(R%), satkhold + 0.015))
                    End If
                    satk(R%) = CSng(satkhold * 0.8 + satk(R%) * 0.2)
                    'bbh 2022/11/28 End: Satk-Yl

                    ' wv 2020/12/17 write out new variable
                    YIELDINT(R%) = YL(R%) / YLLim(R%)
                    'wv 2021/03/09
                    SYIELDINT(R%) = YIELDINT(R%)
                End If
                'wv 2020/12/11 agpy *** end updating YLLim calculation***

                '   All Steps: Land Development
                '   Land Categories:  1= Crop  2=Grazing  3=Forest  4=Other/Undev  5=Urb/Ind
                '   ------------------------------------------------------------------

                '   BBH 1/25/04 Begin V5_0

                '   Urban/Built Land Development
                '   ----------------------------

                Dim tld As Single = 0!
                'bbh 2011/7/30; Urba is part of total land because reduces Othe in preprocessor
                '   It comes from separate data sources; FAO gives us only categories 1-4
                'For L% = 1 To Othe
                For L% = 1 To Urba
                    tld = tld + LD(R%, L%)
                Next L%
                tld = AMAX(0.001, tld)
                Dim NonUrbanShr = (tld - LD(R%, Urba)) / tld
                Dim TLDI = 0!
                'bbh 2011/7/30
                'For L% = 1 To Othe
                For L% = 1 To Urba
                    TLDI = TLDI + LDI(R%, L%)
                Next L%
                TLDI = AMAX(0.001, TLDI)
                Dim NonUrbanShrI = (TLDI - LDI(R%, Urba)) / TLDI
                Dim NonUrbanShrR = (NonUrbanShr / AMAX(0.001, NonUrbanShrI)) ^ 2

                '       bbh 2017/05/02 start
                '        If GDPPCIP(R%) < 1.92 Then
                '            Call XYTABL("GDP/Capita (PPP) Versus Land/Capita Built Up (1992-93, Linear)", _
                '                GDPPCP(R%), landurbanr)
                ''            Call XYTABL("GDP/Capita (PPP) Versus Land/Capita Built Up (1992-93, Logarithmic)", _
                ''                GDPPCP(R%), landurbanrlog)
                ''            landurbanr = Amin(landurbanrlin, landurbanrlog)
                '        Else
                '            Call XYTABL("GDP/Capita (PPP) Versus Land/Capita Built Up (1992-93, Logarithmic)", _
                '                GDPPCP(R%), landurbanr)
                '        End If
                '        If GDPPCIP(R%) < 1.92 Then
                '            Call XYTABL("GDP/Capita (PPP) Versus Land/Capita Built Up (1992-93, Linear)", _
                '                GDPPCIP(R%), landurbanri)
                '        Else
                '            Call XYTABL("GDP/Capita (PPP) Versus Land/Capita Built Up (1992-93, Logarithmic)", _
                '                GDPPCIP(R%), landurbanri)
                '        End If

                'jrs 2024/06/14 TabName$ = "GDP/Capita (PPP 2011) Versus Built Land/Capita (2010) Linear"
                Dim landurbanr = 0!
                Dim landurbanri = 0!
                Call XYTABL(TbNameBuiltLandPC, Amin(50, GDPPCP(R%)), landurbanr, context)
                Call XYTABL(TbNameBuiltLandPC, Amin(50, GDPPCIP(R%)), landurbanri, context)
                Dim landurbanpred = UrbLandI(R%) * POP(R%) * landurbanr / landurbanri

                '        If GDPPCIP(R%) > 5 Then 'based on inspection of cross-sectional,
                '                'urban land per capita ceases to grow at about this level
                '                ' UrbLandI is initial urban land per capita
                '            'bbh 2011/7/30; if population is decreasing, slow down land withdrawal
                '            If POP(R%) < spopi(R%) Then
                '                mult = (POP(R%) + spopi(R%)) / 2
                '            Else
                '                mult = POP(R%)
                '            End If
                '            landurbanpred = UrbLandI(R%) * mult
                '            'LandUrbanPred = UrbLandI(R%) * POP(R%)
                '        Else
                '            landurbanpred = UrbLandI(R%) * POP(R%) * landurbanr / landurbanri
                '        End If
                '       bbh 2017/05/02 end
                Dim nurbLD = landurbanpred - sld(R%, Urba)
                Dim maxwthrate = 0.01 * NonUrbanShrR
                nurbLD = CSng(Amin(nurbLD, maxwthrate * (tld - LD(R%, Urba))))
                'NLD = POP(R%) * POPR(R%) / 100 * ldwf

                'If RegName$(R%) = "Trinidad" AndAlso fIY >= 21 Then Stop

                'bbh 2011/7/30 'Don't all shift to reduce urban below .01 because later is bound above .01
                '   and doing so would create new land for other uses (Barbados problem)
                'bbh 2017/07/11 changed - to + in line below and that stopped an oscillation in Barbados outyears;
                '       But when urban land initially is below 0.01 as in Trinidad, can have transiet up to 0.01
                'bbh 2022/11/28 Changed 0.01 in loop below to 0.001; problem was that multiple countries have urban land values below 0.01, so
                '   when nurbld turns negative above with decreasing POP (or potentially GDPPCP), the loop was recalculating a value for nurbld that was
                '   actually increasing urban land up to 0.01 rather than decreasing it from existing values.
                If nurbLD < 0 And (LD(R%, Urba) + nurbLD < 0.001) Then 'decreasing population can reduce urban land
                    nurbLD = CSng(0 - (LD(R%, Urba) - 0.001))
                End If
                '        If nurbld < 0 And (LD(R%, Urba) + nurbld < 0.01) Then 'decreasing population can reduce urban land
                '            nurbld = 0 - (LD(R%, Urba) - 0.01)
                '        End If

                Dim calcnurbld = 0!
                For L% = 1 To Othe 'Urban/Built taken from all other categories proportionally
                    Dim Reduc = nurbLD * LD(R%, L%) / (tld - LD(R%, Urba))
                    'jrs 2012/07/18 *** begin ***
                    '            If ((LD(R%, L%) - Reduc) < 0.01) Then
                    '                Reduc = 0 - (LD(R%, L%) - 0.01)
                    '            End If
                    If ((LD(R%, L%) - Reduc) < 0.001) Then
                        Reduc = CSng(0 - (LD(R%, L%) - 0.001))
                    End If
                    'jrs 2012/07/18 *** end ***
                    sld(R%, L%) = LD(R%, L%) - Reduc
                    calcnurbld = CSng(calcnurbld + Reduc)
                Next L%
                sld(R%, Urba) = LD(R%, Urba) + calcnurbld
                'If RegName$(R%) = "Trinidad" AndAlso fIY >= 21 Then Stop

                '   bbh 2018/03/09 start changes to crop land and resultant forest change
                '       Code needs to compute lddev for crop; cld and ialk which affect investment
                '       Before changes were many transients in cld as switched between blocks allowing
                '       growth in lddev for crops and not allowing
                '   Crop Land Development
                '   ---------------------

                Dim MaxLandExpansion As Double = Amin(LD(R%, Fors) + LD(R%, Othe), landarablepot(R%) * 1.1 - LD(R%, Crop))
                MaxLandExpansion = AMAX(MaxLandExpansion, 0.02 * LD(R%, Crop)) 'why max rather than min?
                If fIY = 1 Then MaxLandExpansionI(R%) = CSng(MaxLandExpansion)

                Dim DeltaExpandRat = MaxLandExpansionI(R%) /
                    AMAX(0.1 * MaxLandExpansionI(R%), MaxLandExpansion)
                'wv 2021/03/01 added the calculation of a landsat parameter
                ' wv 2021/03/08 made landsat country-specific
                landsat(R%) = ((landarablepot(R%) - (LD(R%, Crop))) / landarablepot(R%))
                'scld(R%) = CLdI(R%) * LD(R%, Crop) * DeltaExpandRat ^ 1.5
                'bbh 2018/03/09 added impact of GDPPCP on CLD because in 2015 there is clear difference in cost per
                '   hectare; 0.3 was attempt to replicate rough developmental level difference
                ' wv 2021/03/08
                scld(R%) = CSng(CLdI(R%) * LDI(R%, Crop) * DeltaExpandRat ^ 0.2 * (GDPPCP(R%) / GDPPCPI(R%)) ^ 0.3) ' ^1.5

                'SCLD(R%) = CLdI(R%) * LD(R%, Crop) * _
                '(LDI(R%, Fors) / AMAX(0.1 * LDI(R%, Fors), LD(R%, Fors))) ^ 1.5

                'If RegName$(R%) = "Burundi" AndAlso fIY >= 26 Then Stop
                'If (LD(R%, Fors) + LD(R%, Othe)) / AMAX(0.0001, LD(R%, Crop)) > 0.1 Then
                Dim lddev = 0!
                'If GRPS(1, R) = "Yemen" Then Stop
                ' wv 2022/08/22 restrict land expansion for SIDS as it was driving up production to much for those countries
                'jrs 2024/10/10 LD(*, Total) doesn't exist, Total = 3, use tld instead
                'Also added or ldcropm < 1 to use this to reduce crop land
                'If MaxLandExpansion / AMAX(0.0001, LD(R%, Crop)) > 0.1 And LD(R%, Total) > 2 Then  'Room for crop land expansion
                If (MaxLandExpansion / AMAX(0.0001, LD(R%, Crop)) > 0.1 And tld > 2) Or ldcropm(R%) < 1 Then  'Room for crop land expansion
                    'lddev = iaval * IALK(R%) / CLD(R%)  - LD(R%, Crop) * dkl
                    lddev = iaval * IALK(R%) / CLD(R%) * ldcropm(R%) - LD(R%, Crop) * dkl
                    'wv 2021/03/01 added a loop to increase lddev in countries with much land still availabe, and limit it in countries close to maxlandexpansion
                    'replicates a diminishing return function, similar to the saturation on yields
                    If lddev > 0 Then
                        If landsat(R%) <= 0.7 Then
                            lddev = CSng(lddev * (landsat(R%) / 0.7))
                            lddev = CSng(Amin(MaxLandExpansion, lddev))
                        Else
                            lddev = CSng(lddev * (1 + ((landsat(R%) - 0.7) / 0.3)))
                            'If ((landarablepot(R%) - (LD(R%, Crop) * 100)) / landarablepot(R%)) <= 0.35 Then
                            '        lddev = lddev * (0.65 + landsat)
                            '        lddev = Amin(MaxLandExpansion, lddev)
                            'Else
                            '    lddev = lddev * (2 - ((landarablepot(R%) - (LD(R%, Crop) * 100)) / landarablepot(R%)))
                            lddev = CSng(Amin(MaxLandExpansion, lddev))
                        End If
                    End If

                    'because ldcropm does not use iaval, increase is "free"; better to use aginvm
                    If lddev < 0 AndAlso lddev * (-1) > 0.1 * LD(R%, Crop) Then
                        lddev = CSng(-1 * 0.1 * LD(R%, Crop)) '10%/year maximum reduction
                    End If

                    If GDPPCP(R%) < 10 Then
                        'jrs 2024/10/10 this avoids reductions, so I'm avoiding it when using parameter
                        'If lddev < 0 Then 'Poor countries unlikely to phase out crop land
                        If lddev < 0 And ldcropm(R%) >= 1 Then 'Poor countries unlikely to phase out crop land
                            Dim DesShift = -CLD(R%) * lddev
                            Dim InvAvail = iaval * (1 - IALK(R%))
                            Dim Shift = Amin(InvAvail * 0.9, DesShift)
                            lddev = CSng(lddev + Shift / CLD(R%))
                            SIALK(R%) = CSng(Amin(1, IALK(R%) + Shift / AMAX(0.001, iaval)))
                        End If
                    End If
                    ' bbh 2017/05/01 start - removed this restriction
                    '           If GDPPCP(R%) > 10 Then  'Not taking more in richer countries
                    '               lddev = 0
                    '               SIALK(R%) = 0
                    '           End If
                    ' bbh 2017/05/01 end

                Else 'No room for crop land expansion

                    'bbh 2018/03/08 start; old code below kicked in in 2083 for India, boostd cld by 50%, and boosted
                    '   global capital costs by 10% with a transient in global and all country food prices
                    'CostGr = CLdI(R%) * LDI(R%, Crop) * (1 + fIY / 100 * 2)
                    'scld(R%) = AMAX(scld(R%), AMAX(CostGr, CLdI(R%) * LD(R%, Crop)))

                    '           scld(R%) = scld(R%) * (1 + igdpr(R%))

                    'bbh 2018/03/08 end
                    lddev = 0
                    SIALK(R%) = 0
                End If
                'If RegName$(R%) = "Burundi" AndAlso fIY >= 26 Then Stop
                '   Forest and Other Land Adjustment
                '   --------------------------------
                '   bbh 2018/03/09 end
                Dim DevBase = sld(R%, Fors) + sld(R%, Othe)
                Dim ForestShr = sld(R%, Fors) / AMAX(0.001, DevBase)
                Dim ForShrPar As Double = 0
                If lddev > 0 Then
                    'Take land from forests/other, more from forests if poorer
                    ' Need parameter >1 if poorer, below 1 if richer
                    ForShrPar = Amin(2, AMAX(0.5, 1 + (15 - GDPPCP(R%))) * 0.02)

                Else   'Give land to forests/other, more to forests if richer
                    ' Need parameter >1 if poorer, below 1 if richer
                    'Need parameter<1 if poorer
                    ForShrPar = Amin(2, AMAX(0.5, 1 + (GDPPCP(R%) - 15)) * 0.02)
                End If


                '        ForShrPar = AMAX(0, 0.25 - AMAX(0, (GDPPCP(R%) - 5)) * 0.02)
                '        ForShrPar = AMAX(0, 0.75 - AMAX(0, (GDPPCP(R%) - 5)) * 0.02) 'bbh 2016/02/21
                '        If GDPPCP(R%) > 15 AndAlso lddev < 0 Then
                '            ForShrPar = 0.25 'give more back to forests
                '        End If

                Dim LDDEVFor As Single = CSng(ForShrPar * lddev * ForestShr) 'Forests contrib less
                'LDDEVFor = 1 / 4 * LDDEV
                LDDEVFor = CSng(Amin(LDDEVFor, sld(R%, Fors) * 0.9))
                Dim LDDEVOth As Single = CSng(lddev - LDDEVFor)
                'If RegName$(R%) = "China" AndAlso fIY >= 25 Then Stop
                LDDEVOth = CSng(Amin(LDDEVOth, sld(R%, Othe) * 0.9))
                '       bbh 2011/7/30
                'jrs 2012/07/18 *** begin ***
                '        If ((SLD(R%, Fors) - LDDEVFor) < 0.01) Then
                '            LDDEVFor = 0 - (SLD(R%, Fors) - 0.01)
                '        End If
                '        If ((SLD(R%, Othe) - LDDEVOth) < 0.01) Then
                '            LDDEVOth = 0 - (SLD(R%, Othe) - 0.01)
                '        End If
                ' wv 2021/03/16 agld, added the amax statement with lddevfor. otherwise very small countries showed land oscilations
                If ((sld(R%, Fors) - LDDEVFor) < 0.001) Then
                    LDDEVFor = CSng(AMAX(LDDEVFor, 0 - (sld(R%, Fors) - 0.001)))
                End If
                ' wv 2021/03/16 agld, added the amax statement with lddevfor. otherwise very small countries showed land oscilations
                If ((sld(R%, Othe) - LDDEVOth) < 0.001) Then
                    LDDEVOth = CSng(AMAX(LDDEVOth, 0 - (sld(R%, Othe) - 0.001)))
                End If
                'jrs 2012/07/18 *** end ***

                '        'jrs 2012/04/10 *** land arable potential begin ***
                '        If lddev > AMAX(landarablepot(R%) - SLD(R%, Crop), 0) Then
                '            LDDEVFor = LDDEVFor * CSng(AMAX(landarablepot(R%) - SLD(R%, Crop), 0) / lddev)
                '            LDDEVOth = LDDEVOth * CSng(AMAX(landarablepot(R%) - SLD(R%, Crop), 0) / lddev)
                '            lddev = AMAX(landarablepot(R%) - SLD(R%, Crop), 0)
                '        End If
                '        'jrs 2012/04/10 *** land arable potential end ***
                lddev = CSng(LDDEVFor + LDDEVOth)
                sld(R%, Fors) = CSng(sld(R%, Fors) - LDDEVFor)
                sld(R%, Othe) = CSng(sld(R%, Othe) - LDDEVOth)
                sld(R%, Crop) = sld(R%, Crop) + lddev
                '   Grazing Land Development
                '   ------------------------
                'bbh 2019/11/14 start
                'If GDPPCP(R%) < 10 Then 'Grazing land developed only in poorer countries
                '    lddev = CSng(0.005 * LDI(R%, Graz))
                'ElseIf GDPPCP(R%) > 15 Then 'Rich countries retire grazing that can be for
                '    lddev = CSng(-0.002 * sld(R%, Graz))
                'Else
                '    lddev = 0
                'End If
                'jrs 2026/05/28
                'Yemen was specially problematic, causing LIVEHRD to jump, which translated to jumps in FEED and INTSFLOWS, VADD(Ag)
                'If GDPPCP(R%) < 10 Then 'Grazing land developed mostly in poorer countries
                '    lddev = CSng(0.005 * LDI(R%, Graz))
                'Else
                If GDPPCP(R%) > 15 Then 'Rich countries retire grazing land that can go to forest
                    lddev = CSng(-0.0005 * sld(R%, Graz))
                Else
                    lddev = 0
                End If
                'jrs 2026/05/28
                'If GRPS(1, R%) = "Yemen" Then
                '    lddev = 0
                'End If
                'jrs 2021/11/03 start
                'If GRPS(1, R%) = "Brazil" Then
                '    If GDPPCP(R%) < 35 Then lddev = CSng(0.005 * LDI(R%, Graz))
                '    If GDPPCP(R%) < 25 Then lddev = CSng(0.01 * LDI(R%, Graz))
                'End If
                'If GRPS(1, R%) = "Indonesia" Then
                '    If GDPPCP(R%) < 35 Then
                '        lddev = CSng(0.015 * LDI(R%, Graz))
                '    ElseIf GDPPCP(R%) < 45 Then
                '        lddev = CSng(0.0075 * LDI(R%, Graz))
                '    Else
                '        lddev = 0
                '    End If
                'End If
                'jrs 2021/11/03 end
                'bbh 2019/11/14 end
                DevBase = sld(R%, Fors) + sld(R%, Othe)
                ForestShr = sld(R%, Fors) / AMAX(0.001, DevBase)
                LDDEVFor = lddev * ForestShr 'Forests contrib less
                'LDDEVFor = 1 / 4 * LDDEV
                LDDEVFor = CSng(Amin(LDDEVFor, sld(R%, Fors) * 0.9))
                LDDEVOth = lddev - LDDEVFor
                LDDEVOth = CSng(Amin(LDDEVOth, sld(R%, Othe) * 0.9))

                'If RegName$(R%) = "Barbados" AndAlso fIY >= 85 Then Stop
                'bbh 2011/7/30
                'jrs 2012/07/18 *** begin ***
                '        If ((SLD(R%, Fors) - LDDEVFor) < 0.01) Then
                '            LDDEVFor = 0 - (SLD(R%, Fors) - 0.01)
                '        End If
                '        If ((SLD(R%, Othe) - LDDEVOth) < 0.01) Then
                '            LDDEVOth = 0 - (SLD(R%, Othe) - 0.01)
                '        End If
                If ((sld(R%, Fors) - LDDEVFor) < 0.001) Then
                    LDDEVFor = CSng(0 - (sld(R%, Fors) - 0.001))
                End If
                If ((sld(R%, Othe) - LDDEVOth) < 0.001) Then
                    LDDEVOth = CSng(0 - (sld(R%, Othe) - 0.001))
                End If
                'jrs 2012/07/18 *** end ***
                lddev = CSng(LDDEVFor + LDDEVOth)

                sld(R%, Fors) = CSng(sld(R%, Fors) - LDDEVFor)
                sld(R%, Othe) = CSng(sld(R%, Othe) - LDDEVOth)
                sld(R%, Graz) = sld(R%, Graz) + lddev

                '   BBH 1/25/04 End V5_0
                '   All Steps: Forest Scenario
                '   ------------------------------
                'jrs 2026/08/05 added AndAlso forestm(R%) <> sforestm(R%) 
                'Avoid compunding effect of multiplier on this stock
                If forestm(R%) <> 1 AndAlso fIY > 1 AndAlso forestm(R%) <> sforestm(R%) Then
                    'jrs 2026/08/05 Dim LDSHIFT = sld(R%, 3) * forestm(R%) - sld(R%, 3)
                    'Avoid compounding effect of multiplier on this stock
                    Dim LDSHIFT As Single
                    If sforestm(R%) > 0 Then
                        LDSHIFT = sld(R%, 3) * forestm(R%) / sforestm(R%) - sld(R%, 3)
                    Else
                        LDSHIFT = sld(R%, 3) * forestm(R%) - sld(R%, 3)
                    End If
                    If LDSHIFT > 0 Then 'bound growth to .2 other land
                        Dim MAXSHIFT = (sld(R%, 1) + sld(R%, 2)) / 5
                        LDSHIFT = Amin(LDSHIFT, MAXSHIFT)
                    Else 'bound loss to .2 of forest
                        LDSHIFT = AMAX(LDSHIFT, -sld(R%, 3) / 5)
                    End If
                    Dim CropShare = sld(R%, 1) / (sld(R%, 1) + sld(R%, 2))
                    sld(R%, 3) = sld(R%, 3) + LDSHIFT
                    sld(R%, 1) = sld(R%, 1) - LDSHIFT * CropShare
                    sld(R%, 2) = sld(R%, 2) - LDSHIFT * (1 - CropShare)
                End If
                'jrs 2026/08/05
                sforestm(R%) = forestm(R%)
                '   All Steps: Bound Land Magnitudes
                '   --------------------------------

                'bbh 2011/7/30
                Dim tldcalc As Single = 0!
                For L% = 1 To 5
                    'jrs 2012/07/18 SLD(R%, L%) = AMAX(0.01, Amin(SLD(R%, L%), 10000))
                    sld(R%, L%) = AMAX(0.001, Amin(sld(R%, L%), 10000))
                    tldcalc = tldcalc + sld(R%, L%)
                Next L%
                For L% = 1 To 5
                    sld(R%, L%) = sld(R%, L%) * tld / AMAX(0.001, tldcalc)
                Next L%
                'jrs 2007/01/12 no double counting
                If RegiontoSum(R%) Then
                    WFORST = WFORST + LD(R%, 3)
                End If
                'If RegName$(R%) = "Trinidad" AndAlso fIY >= 21 Then Stop

                '   All Steps: Agricultural Capital Investment
                '   ------------------------------------------

                Dim skag_1 = KAG(R%) - KAG(R%) / AMAX(lks(1), 0.001)
                'If IY = 11 And R = 79 Then Stop
                SKAG(R%) = CSng((skag_1 + iaval * (1 - IALK(R%))) * (1 - CIVDM(R%)))
                SKAG(R%) = AMAX(0.001, SKAG(R%)) 'bbh 2022/10/27 Lower limit was 0.01 but that was bad for Djibouti with value of .002 in base year

            Next R%

            '   All Steps: Livestock Herd Growth
            '   --------------------------------

            'dsr 2015/07/02 In this block, all that is needed is calculation of global meat production
            '    and global meat stocks. Can move down, use local variables, and get rid of WAPRO and WSTK
            '    For f% = 1 To 2
            '        WAPRO(f%) = 0!
            '        WSTK(f%) = 0!
            '        For R% = 1 To NR%
            '            SFSTOCK(R%, f%) = FSTOCK(R%, f%) 'dsr 2015/07/01 changed SFSTOK to SFSTOCK
            '            'jrs 2007/01/12 no double counting
            '            If RegiontoSum(R%) Then
            ''dsr 2015/06/19 Use post-loss production
            '                WAPRO(f%) = WAPRO(f%) + AGP(R%, f%)
            '                WSTK(f%) = WSTK(f%) + FSTOCK(R%, f%)
            '            End If
            '        Next R%
            '    Next f%
            'WAGPM = 0

            'dsr 2015/07/02 This block estimates growth in global livestock values based on global
            '    meat stocks and growth in global meat demand
            Dim wlvherd = 0!
            Dim wagpmeat = 0! 'dsr 2015/07/02 changed WAPRO(2) to wagpmeat for clarity here and below
            Dim wfstockmeat = 0! 'dsr 2015/07/02 changed WSTK(2) to wfstockmeat for clarity here and below
            Dim wagdemmeat = 0! 'dsr 2015/07/02 changed WAGDEMM to wagdemmeat for clarity here and below
            For R% = 1 To NR%
                'dsr 2015/07/02 Next few lines deal with domestic issues, so move down
                '        If fIY = 1 Then
                '            SLDGR(R%) = LD(R%, 2)
                'dsr 2015/06/19 Followings value never used
                '            sagpcr(R%) = AGPppl(R%, 1)
                '            sagdemm(R%) = AGDEM(R%, 2)  'bbh 2012/02/16
                '        End If
                'jrs 2007/01/12 no double counting
                If RegiontoSum(R%) Then
                    'dsr 2015/06/25 Sum current livestock herd to get global value
                    wlvherd = wlvherd + LVHERD(R%)
                    'dsr 2015/07/02 Sum meat demand to get global value
                    '            WAGDEMM = WAGDEMM + AGDEM(R%, 2)
                    'WAGPM = WAGPM + AGP(R%, 2)
                    wagdemmeat = CSng(wagdemmeat + AGDEM(R%, 2))
                    wagpmeat = wagpmeat + AGP(R%, 2)
                    wfstockmeat = wfstockmeat + FSTOCK(R%, 2)
                End If
            Next R%

            '    wstbase = WAGDEMM + WAPRO(2)
            Dim wstbase = wagdemmeat + wagpmeat
            If fIY = 1 Then
                '        SWFSTKMeat = WSTK(2)
                '        SWAGDEMM = WAGDEMM / (1.02)
                swfstockmeat = wfstockmeat            'dsr 2015/07/02 assume global meat stock in base year-1 was same as in base year
                swagdemmeat = wagdemmeat / 1.02!     'dsr 2015/04/13 assume 2% global meat demand growth from base year-1 to base year
            End If
            'dsr 2015/07/02 calculate growth factor related to global meat stocks
            '    Diff1 = WSTK(2) - agdstl * wstbase
            '    Diff2 = WSTK(2) - SWFSTKMeat
            Diff1 = wfstockmeat - agdstl * wstbase
            Diff2 = wfstockmeat - swfstockmeat
            Dim mulwst = 0!
            Call ADJSTR(agdstl * wstbase, Diff1, Diff2, -0.15, -0.3, mulwst)
            'dsr 2015/07/02 calculate growth factor related to growth in meat demand
            '    BDemGro = WAGDEMM / AMAX(0.001, SWAGDEMM)
            Dim BDemGro = wagdemmeat / AMAX(0.001, swagdemmeat)
            'dsr 2015/06/25 adjust world herd size based on demand growth and world stock effect
            'bbh 2022/10/27 Scenarios can make meat stocks swing widely, but AGP, meat history annual global growth has averaged below 2.5% and base case is below 2%
            '   So adding bounds to multst in new If-Then block
            If gdprext(1) < -99 Then 'growth is endogenous; in exogenous growth period can have bigger swings
                mulwst = CSng(Amin(mulwst, 1.02))
                BDemGro = CSng(Amin(BDemGro, 1.02))
            End If
            wlvherd = wlvherd * BDemGro * mulwst
            'dsr 2015/07/02 save global meat stocks and meat demand for next year's calculations
            '    SWFSTKMeat = WSTK(2)
            '    SWAGDEMM = WAGDEMM
            swfstockmeat = wfstockmeat
            swagdemmeat = wagdemmeat


            'dsr 2015/07/02 Calculate initial estimate of next year's domestic livestock herd size
            Dim wsum = 0!
            For R% = 1 To NR%
                If fIY = 1 Then
                    'dsr 2015/07/02 For first year let initial estimate of next year's domestic livestock herd size be same as present size
                    nlvherd(R%) = LVHERD(R%)
                    'jrs 2026/05/27
                    snlvherd(R%) = nlvherd(R%)
                    'dsr 2015/07/02 Assume grazing land in base year-1 was same as in base year
                    SLDGR(R%) = LD(R%, 2)
                Else
                    '            NLVHERD(R%) = SLVHRD(R%) * AGDEM(R%, 2) / AMAX(0.0001, SAGDEMM(R%)) * _
                    'AGP(R%, 1) / AMAX(0.0001, SAGPCR(R%)) * _
                    'LD(R%, 2) / AMAX(0.0001, SLDGR(R%)) * mulst(R%, 2)
                    ' nlvherd(R%) = slvhrd(R%) * AGDEM(R%, 2) / AMAX(0.0001, sagdemm(R%)) * _
                    'LD(R%, 2) / AMAX(0.0001, sldgr(R%)) * mulst(R%, 2)
                    'dsr 2015/06/25 Calculate domestic herd growth function of demand growth, grazing land growth, and domestic stock effect
                    'wv 2021/04/15 Control negative clpc in seychelles. This is a quick fix, and has to be addressed more fundamentally. The Seychelles was seeing a quick rise in livestock herds that resulted in a rapid rise in food stocks, exports and a long-term drop in calories per capita. i.e. they were exporting all the meat they were producing 
                    'jrs 2026/05/27 added Yemen
                    'If GRPS(1, R%) = "Seychelles" Or GRPS(1, R%) = "Yemen" Then
                    If GRPS(1, R%) = "Seychelles" Then
                        nlvherd(R%) = slvhrd(R%)
                    Else
                        nlvherd(R%) = slvhrd(R%) * AGDEM(R%, 2) / AMAX(0.0001, sagdem(R%, 2)) *
                            LD(R%, 2) / AMAX(0.0001, SLDGR(R%)) * mulst(R%, 2) * lvherdm(R%) 'jrs 2021/08/17 added lvherdm
                        'jrs 2026/05/27 added a limit so that growth stays between -10% and 10% per year
                        'nlvherd(R%) = CSng(AMAX(snlvherd(R%) * 0.9, Amin(snlvherd(R%) * 1.1, slvhrd(R%) * AGDEM(R%, 2) / AMAX(0.0001, sagdem(R%, 2)) *
                        '    LD(R%, 2) / AMAX(0.0001, SLDGR(R%)) * mulst(R%, 2) * lvherdm(R%)))) 'jrs 2021/08/17 added lvherdm
                        snlvherd(R%) = nlvherd(R%)
                    End If
                    'bbh 2012/02/16
                End If
                'jrs 2007/01/12 no double counting

                'dsr 2015/06/25 Sum initial estimates of updated livestock herd sizes to get global value
                If RegiontoSum(R%) Then
                    wsum = wsum + nlvherd(R%)
                End If

            Next R%
            'dsr 2015/06/25 Normalize domestic livestock herd sizes so they sum to global value from above
            For R% = 1 To NR%

                slvhrd(R%) = wlvherd * nlvherd(R%) / wsum
                SLDGR(R%) = LD(R%, 2)
                'dsr 2015/06/19 following value never used
                '        sagpcr(R%) = AGP(R%, 1)
                '        SAGDEM(R%, 2) = AGDEM(R%, 2)
            Next R%
            '        dmbase = AGDEM(R%, 2) + AGPPI(R%, 2)
            ''        balance = CumStk(r%, 2) - dmbase * agdstl
            '        balance = FSTOCK(R%, 2) - dmbase * agdstl
            '        If fIY = 1 Then sbalancelv(R%) = balance
            '        'Call ADJSTR(dmbase, balance, balance - sbalance(R%, 2), ellvhpr1, ellvhpr2, Mul)
            '        'Call ADJSTR(dmbase * agdstl, balance, balance - sbalance(R%, 2), -0.2, -0.4, Mul)
            '        Call ADJSTR(dmbase * agdstl, balance, balance - sbalancelv(R%), -0.05, -0.1, Mul)
            '        sbalancelv(R%) = balance
            '        MulMR = Amin(1#, (AGMRI(R%) / (AGM(R%, 1) / AMAX(0.01, AGDEM(R%, 1)))) ^ 0.3)
            '            'Used to prevent a country (like China) from becoming a huge livestock
            '            ' producer using grain imports as feed; rather crude formulation
            '        If lvhrg(R%) > 0 Then
            '            HerdGr = lvhrg(R%) * MulMR
            '        Else
            '            HerdGr = lvhrg(R%)
            '        End If
            '        NLVHERD = LVHERD(R%) * (1! + HerdGr) * Mul
            '        NLVHERD = AMAX(0.9 * SLVHRD(R%), Amin(1.2 * SLVHRD(R%), NLVHERD))
            '        lvhrg(R%) = (NLVHERD / LVHERD(R%) - 1) * 0.2 + lvhrg(R%) * 0.8
            ''        SLvHrdXBase(R%) = Amin(SLvHrdXBase(R%) * 1.02, SLvHrdXBase(R%) * 0.95 + NLVHERD * 0.05)
            '        slvhrdxbase(R%) = slvhrdxbase(R%) * (1 + lvhrg(R%)) * 0.8 + NLVHERD * 0.2
            ''If RegName$(R%) = "Sao Tome and Principe" Then Stop
            '
            '        SLVHRD(R%) = NLVHERD

            'dsr 2015/06/26 Save profit ratios, AGDEM, pre-loss production, and actual stocks for next year
            For R% = 1 To NR%
                'jrs 2023/11/10
                SAGPTOT(R%) = 0
                'jrs 2023/12/01
                SFDEMTOT(R%) = 0
                For f% = 1 To 3
                    SSFProfitR(R%, f%) = sfprofitr(R%, f%)
                    sfprofitr(R%, f%) = FPROFITR(R%, f%)
                    sagdem(R%, f%) = AGDEM(R%, f%)
                    SAGP(R%, f%) = AGP(R%, f%)
                    SFSTOCK(R%, f%) = FSTOCK(R%, f%) 'dsr 2015/07/01 changed SFSTOK to SFSTOCK
                    'jrs 2023/11/10
                    SAGPTOT(R%) = SAGPTOT(R%) + AGP(R%, f%)
                    'jrs 2023/12/01
                    SFDEMTOT(R%) = SFDEMTOT(R%) + FDEM(R%, f%)
                Next f%
                SMalnPopP(R%) = MALNPOPP(R%) 'bbh 2016/02/17
            Next R%

        End Sub
