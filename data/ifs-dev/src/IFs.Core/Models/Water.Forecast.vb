Imports IFs.Data

Namespace Models

    Partial Public Module Water
        '
        '------------------------------------------WATER.Bas-------------------------------------

        '"To affect the quality of the day, that is the highest of arts." -Henry David Thoreau



        '----------------------------------------------------------------------------------------
        'sgh 2015/04/26
        'Make demand-supply ratio public so it can be used in other modules
        Public iDemandSupplyRatioWater() As Single
        Public DemandSupplyRatioWater() As Single
        Public ChangeinDSRatioWater() As Single
        'sgh 2015/04/26

        'sgh 2018/01/04 - START
        Public swatprice() As Single
        Public slandirareaactual() As Single
        'ttt More
        Dim ResidMun() As Single
        Dim ResidIndElec() As Single, residindman() As Single
        Dim EnpTot() As Single
        Dim ExpectedMunWatDemand() As Single
        Dim ExpectedIndWatDemandElec() As Single
        Dim expectedindwatdemandman() As Single
        Dim ExpectedWastewaterProduced() As Single
        Dim ResidWastewaterProduced() As Single
        Dim TreatedPortionProduced() As Single
        Dim ExpectedTreated() As Single
        Dim ResidTreated() As Single
        Dim ReusedPortionTreated() As Single
        Dim FOSSIL() As Single, swaterresfossil() As Single
        Dim swatwithdsurface() As Single, swatwithdground() As Single, swatwithdfossilground() As Single
        Dim swaterdesalinated() As Single
        Dim swatresexploitrenewsurface() As Single, swatresexploitrenewground() As Single
        Dim iwatwithdsurface() As Single, iwatwithdground() As Single, iwatwithdfossilground() As Single
        Dim iwatresexploitrenewsurface() As Single, iwatresexploitrenewground() As Single
        Dim drawdown() As Single
        Dim ManfVadd() As Single
        Dim WATERTOTALDEMAND() As Single
        Dim secondarywater() As Single, sSecondaryWater() As Single
        Dim MaxExploitableSurface() As Single, RemainingExploitableSurface() As Single
        Dim MaxExploitableGround() As Single, RemainingExploitableGround() As Single
        Dim ENPRenew() As Single, ElecWithoutRenew() As Single, elecrenew() As Single
        Dim iDesal() As Single, iDemand() As Single, isharedemanddesal() As Single, ShareDemandDesal() As Single, remainingdesalpot() As Single
        Dim sTotalWaterSupplyNoDesal() As Single, sWaterDemandInd() As Single
        Dim iFossilWithdrawal() As Single, swastewatertreatedreused() As Single, FossilGrowthRateAdjust() As Single

        Dim municipalwatdemandpc() As Single, ExpectedMunicipalWatDemandPC() As Single, ResidMunPC() As Single, WATDEMANDTWO() As Single
        Dim UrbanPercent() As Single, Urbanpercentsmooth() As Single
        Dim WatDemandIndMan() As Single, watdemandindelec() As Single
        Dim swaterdemandindman() As Single, swaterdemandindelec() As Single
        Dim ResidConverge() As Single, ManufacturingAdjustment() As Single
        Dim WaterTotalSupplyPC() As Single, DesiredWaterUsePerkWh() As Single
        Dim AbilityToAffordElecEfficiency() As Single, GDPPCPAdjustedShiftFactor() As Single
        Dim ActualWaterUsePerkWh() As Single, ElecWatDemand() As Single
        Dim sVaddMan() As Single
        Dim slandirareaactualpercent() As Single
        Dim demandsupplyadjfactor() As Single
        Dim sdemandsupplyratiowater() As Single
        Dim swatdemandmun() As Single, swatdemandind() As Single, swatdemandag() As Single
        Dim swastewatertreated() As Single
        Dim waterperha() As Single
        Dim WatDemAgRatIrAreaI() As Single, WatDemAgRatIrAreaDrift() As Single 'bbh 2019/04/02
        Dim swatertotalsupply() As Single

        'End ttt More

        'sgh 2018/01/04 - END


        Public Sub IfsWater1FirstYear(context As IForecastContext)
            ''sgh 2014/01/20
            ReDim ResidMun(NR%)
            ReDim ResidIndElec(NR%), residindman(NR%)
            ' ReDim residag(NR%) 'bbh 2019/04/02 deleted because never used
            ReDim EnpTot(NR%)
            ' ReDim AgStep(NR%) 'bbh 2019/04/02 deleted because never used
            ReDim ExpectedMunWatDemand(NR%)
            ReDim ExpectedIndWatDemandElec(NR%)
            ReDim expectedindwatdemandman(NR%)
            'ReDim ExpectedAgWatDemand(NR%) 'bbh 2019/04/02 deleted because never used
            ReDim ExpectedWastewaterProduced(NR%)
            ReDim ResidWastewaterProduced(NR%)
            ReDim TreatedPortionProduced(NR%)
            ReDim ExpectedTreated(NR%)
            ReDim ResidTreated(NR%)
            ReDim ReusedPortionTreated(NR%)
            ReDim FOSSIL(NR%), swaterresfossil(NR%)
            ReDim swatwithdsurface(NR%), swatwithdground(NR%), swatwithdfossilground(NR%)
            ReDim swaterdesalinated(NR%)
            ReDim swatresexploitrenewsurface(NR%), swatresexploitrenewground(NR%)
            ReDim iwatwithdsurface(NR%), iwatwithdground(NR%), iwatwithdfossilground(NR%)
            ReDim iwatresexploitrenewsurface(NR%), iwatresexploitrenewground(NR%)
            ReDim drawdown(NR%)
            ReDim ManfVadd(NR%)
            ReDim WATERTOTALDEMAND(NR%), iDemandSupplyRatioWater(NR%), DemandSupplyRatioWater(NR%), ChangeinDSRatioWater(NR%)
            ReDim secondarywater(NR%), sSecondaryWater(NR%)
            ReDim MaxExploitableSurface(NR%), RemainingExploitableSurface(NR%)
            ReDim MaxExploitableGround(NR%), RemainingExploitableGround(NR%)
            ReDim ENPRenew(NR%), ElecWithoutRenew(NR%), elecrenew(NR%)
            ReDim iDesal(NR%), iDemand(NR%), isharedemanddesal(NR%), ShareDemandDesal(NR%), remainingdesalpot(NR%)
            ReDim sTotalWaterSupplyNoDesal(NR%), sWaterDemandInd(NR%)
            ReDim iFossilWithdrawal(NR%), swastewatertreatedreused(NR%), FossilGrowthRateAdjust(NR%)

            ReDim municipalwatdemandpc(NR%), ExpectedMunicipalWatDemandPC(NR%), ResidMunPC(NR%), WATDEMANDTWO(NR%)
            ReDim UrbanPercent(NR%), Urbanpercentsmooth(NR%)
            ReDim WatDemandIndMan(NR%), watdemandindelec(NR%)
            ReDim swaterdemandindman(NR%), swaterdemandindelec(NR%)
            ReDim ResidConverge(NR%), ManufacturingAdjustment(NR%)
            ReDim WaterTotalSupplyPC(NR%), DesiredWaterUsePerkWh(NR%)
            ReDim AbilityToAffordElecEfficiency(NR%), GDPPCPAdjustedShiftFactor(NR%)
            ReDim ActualWaterUsePerkWh(NR%), ElecWatDemand(NR%)
            ReDim sVaddMan(NR%)
            ReDim slandirareaactualpercent(NR%)
            'sgh 2017/01/23
            ReDim demandsupplyadjfactor(NR%)
            'sgh 2017/01/23
            'ReDim WastewaterProduced(NR%)
            ''sgh 2014/01/20
            ReDim sdemandsupplyratiowater(NR%), swatprice(NR%)
            ReDim swatdemandmun(NR%), swatdemandind(NR%), swatdemandag(NR%)
            ReDim swastewatertreated(NR%)

            'sgh 2018/01/04 - START
            ReDim slandirareaactual(NR%), waterperha(NR%)
            'sgh 2018/01/04 - END

            ReDim WatDemAgRatIrAreaI(NR%), WatDemAgRatIrAreaDrift(NR%) 'bbh 2019/04/02
            'ReDim YLIMPACTWATPRICE(NR%) 'bbh 2019/04/02 Put here and then moved to common block

            'sgh 2018/05/07
            ReDim swatertotalsupply(NR%)
            '
            '   First Year:  Water Demand (including irrigated area)
            '   ---------------------------------------------------
            '1=Municipal
            '2=Industrial (energy+manufacturing)
            '3=Agriculture

            ''sgh 2014/01/17
            For R% = 1 To NR%
                'sgh 2014/09/04
                iDemand(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                isharedemanddesal(R%) = WATERDESALINATED(R%) / iDemand(R%)
                iDesal(R%) = WATERDESALINATED(R%)
                iFossilWithdrawal(R%) = WATERWITHDRAWAL(R%, 3)

                swatdemandmun(R%) = WATDEMAND(R%, 1)
                swatdemandind(R%) = WATDEMAND(R%, 2)
                swatdemandag(R%) = WATDEMAND(R%, 3)
                'sgh 2014/09/04

                'jrs 2018/12/21 shouldn't be changing LANDIRAREAACTUAL IN YEAR 1, IT causes run to run problems *** begin ***
                'sgh 2018/01/04 - START
                If LANDIRAREAACTUAL(R%) > LANDIRAREAEQUIP(R%) Then
                    'LANDIRAREAACTUAL(R%) = LANDIRAREAEQUIP(R%)
                    'slandirareaactual(R%) = LANDIRAREAACTUAL(R%)
                    Debug.WriteLine("Need to fix LANDIRAREAACTUAL > LANDIRAREAEQUIP in Preprocessor for " & GRPS(1, R%) & "- Water Module")
                End If
                'jrs 2018/12/21 shouldn't be changing LANDIRAREAACTUAL IN YEAR 1, IT causes run to run problems *** end ***
                slandirareaactual(R%) = LANDIRAREAACTUAL(R%)
                'sgh 2018/01/04 - END
                'bbh 2019/04/02
                WatDemAgRatIrAreaI(R%) = (WATDEMAND(R%, 3) * 1000000 / LANDIRAREAACTUAL(R%))
                WatDemAgRatIrAreaDrift(R%) = (WATDEMAND(R%, 3) * 1000000 / LANDIRAREAACTUAL(R%))

                'If RegName$(R%) = "Iceland" Then Stop
                '    Call XYTABL("Water Withdrawal Municipal vs Population Urban (most recent) (linear)", SPOPURBAN(R%), ExpectedMunWatDemand(R%))
                '    ResidMun(R%) = ExpectedMunWatDemand(R%) - WATDEMAND(R%, 1)
                'If RegName$(R%) = "South Africa" Then Stop
                'First Year:  Water demand, municipal
                '   ---------------------------------

                'sgh 2015/09/06
                'Municipal water demand
                municipalwatdemandpc(R%) = WATDEMAND(R%, 1) / SPOPURBAN(R%)
                UrbanPercent(R%) = SPOPURBAN(R%) / POP(R%)
                Urbanpercentsmooth(R%) = UrbanPercent(R%)
                'If RegName$(R%) = "Taiwan" Then Stop
                'Call AnalFunc("MunWatdPC (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log) versus PopulationUrban%", _
                'Array(GDPPCP(R%), UrbanPercent), Prediction)
                Dim Prediction = 0!
                Call AnalFunc("MunDemandPC (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log) AND Connections",
    New Single() {GDPPCP(R%), UrbanPercent(R%), WATSAFE(R%, 3)}, Prediction,
    New String() {"GDP2011PCPPP(MOSTRECENT) (Log)", "PopulationUrban%(MOSTRECENT) (Log)", "WSSJMPWaterTotal%Piped(MOSTRECENT) (Log)"}, context)
                'If RegName$(R%) = "China" Then Stop
                ResidMunPC(R%) = Prediction - municipalwatdemandpc(R%)
                'sgh 2015/09/06

                'First Year:  Water withdrawal/supply and balance with demand
                '   ---------------------------------------------------------
                '1=Surface
                '2=Ground
                '3=Fossil Ground

                'sgh 2017/01/23
                WATERTOTALSUPPLY(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WATERWITHDRAWAL(R%, 3) +
        WASTEWATERSUPPLY(R%, 3) + WATERDESALINATED(R%)
                demandsupplyadjfactor(R%) = iDemand(R%) - WATERTOTALSUPPLY(R%)
                WATERTOTALSUPPLY(R%) = WATERTOTALSUPPLY(R%) + demandsupplyadjfactor(R%)
                'sgh 2018/05/07
                swatertotalsupply(R%) = WATERTOTALSUPPLY(R%)
                'If RegName$(R%) = "Qatar" Then Stop
                'If RegName$(R%) = "Nicaragua" Then Stop
                'sgh 2017/01/23

                'Oil = 1, Gas = 2, Coal = 3, Hydro = 4, Nucl = 5, Renew = 6, Synth = 7
                '    EnpTot(R%) = 0
                '    EnpTot(R%) = EnpTot(R%) + ENP(R%, Oil) + ENP(R%, Gas) + ENP(R%, Coal) + _
                '        ENP(R%, Hydro) + ENP(R%, Nucl) + ENP(R%, Renew) - ENX(R%) + ENM(R%)
                '    ENPRenew(R%) = ENP(R%, Renew) + ENP(R%, Hydro)
                '
                ''sgh 2014/05/29
                ''All electricity in Paraguay comes from Hydropower but most of it is exported. Paraguay is the only
                ''country where this calculation results in ENPRenew(R%) > EnpTot(R%).
                '    If ENPRenew(R%) / EnpTot(R%) > 1 Then
                '        ElecWithoutRenew(R%) = 0
                '    Else
                '    ElecWithoutRenew(R%) = (1 - (ENPRenew(R%) / EnpTot(R%))) * INFRAELECPROD(R%)
                '    If ElecWithoutRenew(R%) < 0 Then
                '        ElecWithoutRenew(R%) = 0
                '    End If
                '    End If
                'sgh 2014/05/29

                'Electrical portion of industrial water demand
                '    Call XYTABL("WaterWithdIndustrial(MOSTRECENT) (Linear) versus Elec (Linear)", _
                '        ElecWithoutRenew(R%), ExpectedIndWatDemandElec(R%))
                '    ResidIndElec(R%) = ExpectedIndWatDemandElec(R%) - WATDEMAND(R%, 2)

                'sgh 2015/11/22
                'Use water required per kWh to calculate electrical portion of industrial water demand

                'First Year:  Water Demand, Electricity portion of industrial
                '   ---------------------------------------------------------

                WaterTotalSupplyPC(R%) = WATERTOTALSUPPLY(R%) / POP(R%)

                'The following equation is created by mapping the range of water supply per capita (0.08822 - 345.545)
                'onto a new range of water use (in liters) per kWh (0.4 - 2) i.e. 0.00462 = ((2 - 0.4) / (345.545))
                DesiredWaterUsePerkWh(R%) = (WaterTotalSupplyPC(R%) * 0.00462!) + 0.4!

                'The following equation is created by mapping the range of GDPPCP (0.6318 - 127.215) onto the range of 0 to 1.
                'i.e. 0.0079 = 1 / (127.215 - 0.6318)
                AbilityToAffordElecEfficiency(R%) = GDPPCP(R%) * 0.0079!

                'The following equation factors both water scarcity (WaterTotalSupplyPC) and ability for the country to afford
                'efficiency technology (GDPPCP) to create a new shift factor
                GDPPCPAdjustedShiftFactor(R%) = AbilityToAffordElecEfficiency(R%) * (2 - DesiredWaterUsePerkWh(R%))

                'Since 2 liters/kWh is the max, we deduct the shift factor from 2
                ActualWaterUsePerkWh(R%) = 2 - GDPPCPAdjustedShiftFactor(R%)

                ENPRenew(R%) = ENP(R%, Renew) + ENP(R%, Hydro)
                'Convert to GwH
                elecrenew(R%) = ENPRenew(R%) * 1699.41! * 1000.0!
                ElecWithoutRenew(R%) = INFRAELECPROD(R%) - elecrenew(R%)

                'Avoid negatives for countries with a lot of renewables
                If ElecWithoutRenew(R%) < (0.1! * INFRAELECPROD(R%)) Then
                    ElecWithoutRenew(R%) = 0.1! * INFRAELECPROD(R%)
                End If

                'Convert to required water in cubic kilometers
                'GwH to kWh: must multiply by 1,000,000 and then liters to cubic km must divide by 1,000,000,000,000
                'so just divide by 1,000,000
                watdemandindelec(R%) = ElecWithoutRenew(R%) * ActualWaterUsePerkWh(R%) / 1000 / 1000
                If (WATDEMAND(R%, 2) - watdemandindelec(R%)) < (0.1 * WATDEMAND(R%, 2)) Then
                    watdemandindelec(R%) = 0.1! * WATDEMAND(R%, 2)
                End If
                swaterdemandindelec(R%) = watdemandindelec(R%)
                'sgh 2015/11/22
                'First Year:  Manufacturing portion of industrial water demand
                '   ---------------------------------------------------------

                'Use industrial water demand minus electrical water demand (external calculation) for dependent variable
                Call XYTABL("WaterWithdIndustrial(MOSTRECENT minus elec) (Linear) versus Man (Linear)", VADD(R%, 4),
        expectedindwatdemandman(R%), context)

                sVaddMan(R%) = VADD(R%, 4)

                If expectedindwatdemandman(R%) < 0 Then
                    expectedindwatdemandman(R%) = 0.005
                End If
                'Calculate residual based off manufacturing water demand i.e. industrial minus electricity
                residindman(R%) = expectedindwatdemandman(R%) - (WATDEMAND(R%, 2) - watdemandindelec(R%))
                WatDemandIndMan(R%) = WATDEMAND(R%, 2) - watdemandindelec(R%)
                swaterdemandindman(R%) = WatDemandIndMan(R%)
                'If RegName$(R%) = "South Africa" Then Stop
                'sgh 2015/10/21

                ManufacturingAdjustment(R%) = CSng(Amin(AMAX(0.3, ManufacturingAdjustment(R%)), 0.9))
                'If RegName$(R%) = "South Africa" Then ManufacturingAdjustment(R%) = 0.75
                'WATDEMANDMAN(R%) = WATDEMAND(R%, 2) * ManufacturingAdjustment(R%)
                'If RegName$(R%) = "South Africa" Then Stop
                'End If
                'sgh 2015/10/21

                '    Call XYTABL("WaterWithdAg v LandIrEqip(Most Recent) (Linear)", (LANDIRAREAEQUIP(R%)), ExpectedAgWatDemand(R%))
                'sgh 2016/10/26 - Use land actually irrigated, not land equipped for irrigation
                'First Year:  Agricultural water demand
                '   -----------------------------------

                '   Call XYTABL("WaterWithdAgriculture(MOSTRECENT) (Linear) versus Irrigated land (Linear)", _
                'LANDIRAREAACTUAL(R%), ExpectedAgWatDemand(R%)) 'bbh 2019/04/02 deleted because never used
                'sgh 2016/10/26

                'sgh 2018/01/04 - START - use multiplicative resid
                'ttt VB6: If Not IsNull(WATDEMAND(R%, 3)) And Not IsNull(LANDIRAREAACTUAL(R%)) Then
                If WATDEMAND(R%, 3) <> 0 AndAlso LANDIRAREAACTUAL(R%) <> 0 Then
                    waterperha(R%) = WATDEMAND(R%, 3) * 1000000 / LANDIRAREAACTUAL(R%)
                Else
                    waterperha(R%) = 9167
                End If
                'If RegName$(R%) = "Uzbekistan" Then Stop
                'residag(R%) = ExpectedAgWatDemand(R%) / WATDEMAND(R%, 3)  'bbh 2019/04/02 deleted because never used
                'sgh 2018/01/04 - END
                'If RegName$(R%) = "Libya" Then Stop
                '    AgStep(R%) = residag(R%) / 40 'bbh 2019/04/02 deleted because never used

                'First Year:  Waste water production/treatment
                '   -------------------------------------------

                Call XYTABL("WastewaterProduced v MunicipalDemand", WATDEMAND(R%, 1), ExpectedWastewaterProduced(R%), context)
                ResidWastewaterProduced(R%) = ExpectedWastewaterProduced(R%) - WASTEWATERSUPPLY(R%, 1)

                'sgh 2014/10/23
                'Updated equation to include 2011 GDP
                '    Call XYTABL("WastewaterTreated pct of produced (Linear) versus GDP2005PCPPP(MOSTRECENT) (Log)", GDPPCP95(R%) * _
                '        1000, TreatedPortionProduced(R%))
                Call XYTABL("WastewaterTreated pct of produced (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log)", GDPPCP(R%) *
        1000, TreatedPortionProduced(R%), context)
                'sgh 2014/10/23

                ExpectedTreated(R%) = TreatedPortionProduced(R%) * WASTEWATERSUPPLY(R%, 1)
                ResidTreated(R%) = ExpectedTreated(R%) - WASTEWATERSUPPLY(R%, 2)
                'If RegName$(R%) = "China" Then Stop
                'If RegName$(R%) = "United States of America" Then Stop

                'First Year:  Saved variables for next year, initial condition saves
                '   ----------------------------------------------------------------
                swatwithdsurface(R%) = WATERWITHDRAWAL(R%, 1)
                swatwithdground(R%) = WATERWITHDRAWAL(R%, 2)
                'If RegName$(R%) = "Nicaragua" Then Stop

                swatwithdfossilground(R%) = WATERWITHDRAWAL(R%, 3)

                swaterdesalinated(R%) = WATERDESALINATED(R%)
                swatresexploitrenewsurface(R%) = WATRESEXPLOITRENEW(R%, 1) - (WASTEWATERSUPPLY(R%, 2) -
        WASTEWATERSUPPLY(R%, 3))

                swatresexploitrenewground(R%) = WATRESEXPLOITRENEW(R%, 2)
                swaterresfossil(R%) = WATERRESFOSSIL(R%)
                '    sTotalWaterSupplyNoDesal(R%) = WATRESEXPLOITRENEW(R%, 1) + WATRESEXPLOITRENEW(R%, 2) + WASTEWATERSUPPLY(R%, 3) + _
                '        WATERWITHDRAWAL(R%, 3)
                sTotalWaterSupplyNoDesal(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WASTEWATERSUPPLY(R%, 3) +
        WATERWITHDRAWAL(R%, 3)
                sWaterDemandInd(R%) = WATDEMAND(R%, 2)

                'sgh 2016/11/17
                slandirareaactualpercent(R%) = (LANDIRAREAACTUAL(R%) / LANDIRAREAEQUIP(R%))


                'sgh 2016/11/17
                'sgh 2015/10/21
                '    sWaterDemandIndElec(R%) = WATDEMAND(R%, 2)
                '    swaterdemandindman(R%) = WATDEMAND(R%, 2)
                'sgh 2015/10/21

                swastewatertreatedreused(R%) = WASTEWATERSUPPLY(R%, 3)
                swastewatertreated(R%) = WASTEWATERSUPPLY(R%, 2)

                'If RegName$(R%) = "United States of America" Then Stop

                'sgh 2014/05/05
                'If RegName$(R%) = "Afghanistan" Then Stop
                iwatresexploitrenewsurface(R%) = swatresexploitrenewsurface(R%)
                iwatresexploitrenewground(R%) = WATRESEXPLOITRENEW(R%, 2)
                'sgh 2014/05/05

                iwatwithdsurface(R%) = WATERWITHDRAWAL(R%, 1)
                iwatwithdground(R%) = WATERWITHDRAWAL(R%, 2)

                iwatwithdfossilground(R%) = WATERWITHDRAWAL(R%, 3)
                'If RegName$(R%) = "Iran" Then Stop
                'sgh 2014/03/09
                ReusedPortionTreated(R%) = WASTEWATERSUPPLY(R%, 3) / WASTEWATERSUPPLY(R%, 2)
                '   FOSSIL(R%) = WATERRESFOSSIL(R%)
                'sgh 2014/03/09

                sSecondaryWater(R%) = WASTEWATERSUPPLY(R%, 2) - WASTEWATERSUPPLY(R%, 3)

                drawdown(R%) = 1

                'If RegName$(R%) = "United States of America" Then Stop
                '    WATERTOTALSUPPLY(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WASTEWATERSUPPLY(R%, 3) + _
                '    WATERDESALINATED(R%) + WATERWITHDRAWAL(R%, 3)

                'sgh 2014/04/30
                'Use exploitable water resources to determine total supply
                'WATERTOTALSUPPLY(R%) = WATRESEXPLOITRENEW(R%, 1) + WATRESEXPLOITRENEW(R%, 2) + WASTEWATERSUPPLY(R%, 3) + _
                '    WATERDESALINATED(R%) + WATERWITHDRAWAL(R%, 3)
                'sgh 2017/01/17 - change total supply
                'WATERTOTALSUPPLY(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WATERWITHDRAWAL(R%, 3) + _
                '    WATERDESALINATED(R%) + WASTEWATERSUPPLY(R%, 3)
                'If RegName$(R%) = "China" Then Stop

                'sgh 2014/05/05
                WATERTOTALDEMAND(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                'DemandSupplyRatio(R%) = WATERTOTALDEMAND(R%) / WATERTOTALSUPPLY(R%)
                'sgh 2014/05/05

                'sgh 2015/04/26
                'Initialize demand-supply ratio
                iDemandSupplyRatioWater(R%) = WATERTOTALDEMAND(R%) / WATERTOTALSUPPLY(R%)
                DemandSupplyRatioWater(R%) = iDemandSupplyRatioWater(R%)
                sdemandsupplyratiowater(R%) = iDemandSupplyRatioWater(R%)
                ChangeinDSRatioWater(R%) = 1

                swatprice(R%) = WATPRICE(R%)
                'sgh 2015/04/26
                'If RegName$(R%) = "Nicaragua" Then Stop
            Next R%
        End Sub

        Public Sub IfsWater1AllbutFirstYear(context As IForecastContext)
            'AllButFirst:

            'All But First: Wraps from previous years
            '----------------------------------------
            'ttt 2019/10/08 TakeOut
            Array.Copy(swatprice, WATPRICE, NR + 1)
            Array.Copy(sdemandsupplyratiowater, DemandSupplyRatioWater, NR + 1)
            Array.Copy(swatertotalsupply, WATERTOTALSUPPLY, NR + 1)

            For R% = 1 To NR%
                'If RegName$(R%) = "Jordan" Then Stop
                'If RegName$(R%) = "Saudi Arabia" Then Stop
                'ttt 2019/10/08 TakenOut
                'WATPRICE(R%) = swatprice(R%)
                Dim proportionchangeinwatprice = (swatprice(R%) - 100.0!) / 100.0!
                WATERWITHDRAWAL(R%, 3) = swatwithdfossilground(R%)
                WATDEMAND(R%, 1) = swatdemandmun(R%)
                WATDEMAND(R%, 2) = swatdemandind(R%)
                WATDEMAND(R%, 3) = swatdemandag(R%)
                WASTEWATERSUPPLY(R%, 3) = swastewatertreatedreused(R%)
                WASTEWATERSUPPLY(R%, 2) = swastewatertreated(R%)
                'ttt 2019/10/08 TakenOut
                'DemandSupplyRatioWater(R%) = sdemandsupplyratiowater(R%)
                WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%)
                WATERWITHDRAWAL(R%, 2) = swatwithdground(R%)
                WATRESEXPLOITRENEW(R%, 1) = swatresexploitrenewsurface(R%)
                WATRESEXPLOITRENEW(R%, 2) = swatresexploitrenewground(R%)
                'sgh 2018/05/07
                'ttt 2019/10/08 TakenOut
                'WATERTOTALSUPPLY(R%) = swatertotalsupply(R%)


                'All But First: Water withdrawals/demand, municipal
                '-------------------------------------------------

                'If RegName$(R%) = "China" Then Stop
                'sgh 2014/01/17
                'municipal demand
                Call XYTABL("Water Withdrawal Municipal vs Population Urban (most recent) (linear)", SPOPURBAN(R%), ExpectedMunWatDemand(R%), context)
                'If RegName$(R%) = "South Africa" Then Stop

                'sgh 2017/01/23
                If WATERWITHDRAWAL(R%, 3) <= 0 Then
                    WATERWITHDRAWAL(R%, 3) = 0.01
                End If
                'sg
                'sgh 2015/09/06
                Dim UrbanPercentGR = (POPURBAN(R%) / POP(R%)) / Urbanpercentsmooth(R%)
                If UrbanPercentGR > 1.01 Then
                    UrbanPercent(R%) = POPURBAN(R%) / POP(R%)
                    Dim UrbanPercentSmoothAdjustment = ((92 - UrbanPercent(R%)) / 92) ^ (1 / 1.5)
                    Urbanpercentsmooth(R%) = CSng(Urbanpercentsmooth(R%) * 1.01 * UrbanPercentSmoothAdjustment)
                Else
                    Urbanpercentsmooth(R%) = POPURBAN(R%) / POP(R%)
                End If

                'If IY% > 60 AndAlso RegName$(R%) = "China" Then Stop
                Dim Prediction = 0!
                Call AnalFunc("MunDemandPC (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log) AND Connections",
                  New Single() {GDPPCP(R%), Urbanpercentsmooth(R%), WATSAFE(R%, 3)}, Prediction,
                   New String() {"GDP2011PCPPP(MOSTRECENT) (Log)", "PopulationUrban%(MOSTRECENT) (Log)", "WSSJMPWaterTotal%Piped(MOSTRECENT) (Log)"}, context)

                'If IY% > 60 AndAlso RegName$(R%) = "China" Then Stop

                municipalwatdemandpc(R%) = (Prediction - ResidMunPC(R%))
                If municipalwatdemandpc(R%) < 0.01 Then
                    municipalwatdemandpc(R%) = 0.01
                End If
                WATDEMANDTWO(R%) = municipalwatdemandpc(R%) * SPOPURBAN(R%)
                ''sgh 2015/09/06
                '
                ''sgh 2015/09/06
                'WATDEMAND(R%, 1) = (ExpectedMunWatDemand(R%) - ResidMun(R%)) * waterdemandm(R%, 1)
                'sgh 2018/01/04 - START
                'Include parameter on demand
                WATDEMAND(R%, 1) = WATDEMANDTWO(R%) * waterdemandm(R%, 1)
                'sgh 2018/01/04
                ''sgh 2015/09/06
                'If RegName$(R%) = "Congo, Republic of" AndAlso IY% > 26 Then Stop

                If WATDEMAND(R%, 1) < 0 Then
                    WATDEMAND(R%, 1) = 0
                End If
                'If RegName$(R%) = "South Africa" Then Stop
                'sgh 2014/01/17

                'sgh 2014/01/17
                'Industrial demand
                'Oil = 1, Gas = 2, Coal = 3, Hydro = 4, Nucl = 5, Renew = 6, Synth = 7
                '    EnpTot(R%) = 0
                '    EnpTot(R%) = EnpTot(R%) + ENP(R%, Oil) + ENP(R%, Gas) + ENP(R%, Coal) + ENP(R%, Nucl) + _
                '        ENP(R%, Hydro) + ENP(R%, Renew) - ENX(R%) + ENM(R%)
                '    ENPRenew(R%) = ENP(R%, Renew) + ENP(R%, Hydro)
                ''If RegName$(R%) = "Qatar" Then Stop
                ''sgh 2014/05/29
                ''Since all of Paraguay's electricity comes from hydro and most of this is exported this means ENPRenew(R%) is
                ''greater than ENPTot(R%). This is the only country where this is the case.
                '    If (ENPRenew(R%) / EnpTot(R%)) > 1 Then
                '        ElecWithoutRenew(R%) = 0
                ''If RegName$(R%) = "Qatar" Then Stop
                '    Else
                ''If RegName$(R%) = "Qatar" Then Stop
                '    ElecWithoutRenew(R%) = (1 - (ENPRenew(R%) / EnpTot(R%))) * INFRAELECPROD(R%)
                '    If ElecWithoutRenew(R%) < 0 Then
                '        ElecWithoutRenew(R%) = 0
                '    End If
                '    End If
                '
                '    If (ENPRenew(R%) / EnpTot(R%)) < 0 Then
                '        ElecWithoutRenew(R%) = 0
                '    End If
                'sgh 2014/05/29

                'sgh 2015/10/21
                'If RegName$(R%) = "Cyprus" Then Stop
                'Call XYTABL("WaterWithdIndustrial(MOSTRECENT) (Linear) versus Elec (Linear)", _
                '        ElecWithoutRenew(R%), ExpectedIndWatDemandElec(R%))
                'Call XYTABL("WaterWithdIndustrial(MOSTRECENT minus elec) (Linear) versus Man (Linear)", VADD(R%, 4), _
                '        expectedindwatdemandman(R%))
                'sgh 2015/10/21

                'sgh 2015/11/22
                'Electricity portion of industrial water demand
                'Use water required per kWh to calculate electrical portion of industrial water demand



                WaterTotalSupplyPC(R%) = WATERTOTALSUPPLY(R%) / POP(R%)

                'The following equation is created by mapping the range of water supply per capita (0.08822 - 345.545)
                'onto a new range of water use (in liters) per kWh (0.4 - 2) i.e. 0.00462 = ((2 - 0.4) / (345.545))
                'All But First: Water demand - electricity portion of industrial
                '---------------------------------------------------------------

                DesiredWaterUsePerkWh(R%) = (WaterTotalSupplyPC(R%) * 0.00462!) + 0.4!

                'The following equation is created by mapping the range of GDPPCP (0.6318 - 127.215) onto the range of 0 to 1.
                'i.e. 0.0079 = 1 / (127.215 - 0.6318)
                AbilityToAffordElecEfficiency(R%) = GDPPCP(R%) * 0.0079!

                'The following equation factors both water scarcity (WaterTotalSupplyPC) and ability for the country to afford
                'efficiency technology (GDPPCP) to create a new shift factor
                GDPPCPAdjustedShiftFactor(R%) = AbilityToAffordElecEfficiency(R%) * (2 - DesiredWaterUsePerkWh(R%))

                'Since 2 liters/kWh is the max, we deduct the shift factor from 2
                ActualWaterUsePerkWh(R%) = 2 - GDPPCPAdjustedShiftFactor(R%)

                ENPRenew(R%) = ENP(R%, Renew) + ENP(R%, Hydro)
                'Convert to GwH
                elecrenew(R%) = ENPRenew(R%) * 1699.41! * 1000.0!
                ElecWithoutRenew(R%) = INFRAELECPROD(R%) - elecrenew(R%)
                'Make sure non renewable electricity generation doesn't go to zero
                If ElecWithoutRenew(R%) < (0.1 * INFRAELECPROD(R%)) Then
                    ElecWithoutRenew(R%) = 0.1! * INFRAELECPROD(R%)
                End If
                'Convert to required water in cubic kilometers
                'sgh 2018/04/26 - START
                'Use swaterdemandindelec because that's what's used below for the AMAX statement
                '    WatDemandIndElec(R%) = ElecWithoutRenew(R%) * ActualWaterUsePerkWh(R%) / 1000 / 1000
                swaterdemandindelec(R%) = ElecWithoutRenew(R%) * ActualWaterUsePerkWh(R%) / 1000 / 1000
                'sgh 2018/04/26 - END
                'If RegName$(R%) = "Egypt" Then Stop
                'Make sure electricity water demand doesn't decrease too fast as renewables come online
                'sgh 2018/05/07 - use swaterdemandindelec instead
                '    WatDemandIndElec(R%) = AMAX(swaterdemandindelec(R%) * 0.98, swaterdemandindelec(R%) * waterdemandm(R%, 2))
                swaterdemandindelec(R%) = AMAX(swaterdemandindelec(R%) * 0.98, swaterdemandindelec(R%) * waterdemandm(R%, 2))
                'sgh 2018/05/07 - save value
                '    swaterdemandindelec(R%) = WatDemandIndElec(R%)
                watdemandindelec(R%) = swaterdemandindelec(R%)


                'All But First: Water demand, Manufacturing portion of industrial water demand
                '-----------------------------------------------------------------------------

                Call XYTABL("WaterWithdIndustrial(MOSTRECENT minus elec) (Linear) versus Man (Linear)", VADD(R%, 4),
        expectedindwatdemandman(R%), context)

                WatDemandIndMan(R%) = AMAX(swaterdemandindman(R%) * 0.98, (expectedindwatdemandman(R%) - residindman(R%)) _
        * waterdemandm(R%, 2))

                '    WatDemandIndMan(R%) = Amin(WatDemandIndMan(R%), swaterdemandindman(R%) * 1.005)
                'If GRPS(1, R%) = "Libya" AndAlso IY + FirstYear - 1 = 2014 Then Stop
                WatDemandIndMan(R%) = CSng(swaterdemandindman(R%) * ((VADD(R%, 4) / sVaddMan(R%)) ^ (0.45)))
                'If RegName$(R%) = "South Africa" Then
                '        WatDemandIndMan(R%) = swaterdemandindman(R%) * ((VADD(R%, 4) / sVaddMan(R%)) ^ (1.9))
                'End If
                'If RegName$(R%) = "Rwanda" Then Stop


                swaterdemandindman(R%) = WatDemandIndMan(R%)
                sVaddMan(R%) = VADD(R%, 4)

                'All But First: Water demand, industrial wrap up
                '-----------------------------------------------

                WATDEMAND(R%, 2) = watdemandindelec(R%) + WatDemandIndMan(R%)
                'If RegName$(R%) = "Qatar" Then Stop
                'WATDEMANDMAN(R%) = WatDemandIndMan(R%)
                'sgh 2015/10/21

                If WATDEMAND(R%, 2) < 0 Then
                    WATDEMAND(R%, 2) = 0
                End If
                sWaterDemandInd(R%) = WATDEMAND(R%, 2)
                'If RegName$(R%) = "Bulgaria" Then Stop
                'If RegName$(R%) = "Qatar" AndAlso IY% > 8 Then Stop
                'sgh 2014/01/17

                'sgh 2014/01/17
                'Agricultural demand
                'If RegName$(R%) = "Luxembourg" Then Stop
                'sgh 2016/10/26 - Use actually irrigated land instead of land equipped for irrigation
                '    Call XYTABL("WaterWithdAg v LandIrEqip(Most Recent) (Linear)", (LANDIRAREAEQUIP(R%)), ExpectedAgWatDemand(R%))

                'All But First: Water demand, agricultural
                '------------------------------------------

                'sgh 2018/04/26 - START
                '    If LANDIRAREAEQUIP(R%) = 0 Then
                If LANDIRAREAEQUIP(R%) <= 0 Then
                    'sgh 2018/04/26
                    LANDIRAREAEQUIP(R%) = 0.001
                End If
                LANDIRAREAACTUAL(R%) = slandirareaactualpercent(R%) * LANDIRAREAEQUIP(R%) * landirareaactualm(R%)
                If LANDIRAREAACTUAL(R%) > LANDIRAREAEQUIP(R%) Then
                    LANDIRAREAACTUAL(R%) = LANDIRAREAEQUIP(R%)
                End If
                '   Call XYTABL("WaterWithdAgriculture(MOSTRECENT) (Linear) versus Irrigated land (Linear)", _
                'LANDIRAREAACTUAL(R%), ExpectedAgWatDemand(R%)) 'bbh 2019/04/02 deleted because never used
                'sgh 2016/10/26

                'sgh 2018/01/04 - START
                'Just use the waterPerHA variable to calculate ag water demand. waterPerHA is adjusted later in the code
                'to account for price changes.
                '    WATDEMAND(R%, 3) = (ExpectedAgWatDemand(R%) - residag(R%)) * waterdemandm(R%, 3)
                '    WATDEMAND(R%, 3) = (ExpectedAgWatDemand(R%) / residag(R%)) * waterdemandm(R%, 3)

                'sgh 2018/02/13 - START
                'Apply parameter later on in code
                '    WATDEMAND(R%, 3) = waterperha(R%) * LANDIRAREAACTUAL(R%) / 1000000 * waterdemandm(R%, 3)
                'sgh 2018/02/13 - END

                'If RegName$(R%) = "Libya" Then Stop
                'sgh 2018/01/04

                If WATDEMAND(R%, 3) < 0 Then
                    WATDEMAND(R%, 3) = 0
                End If
                'save the value w/o the multiplier so it doesn't grow exponentially
                slandirareaactualpercent(R%) = (LANDIRAREAACTUAL(R%) / LANDIRAREAEQUIP(R%)) / landirareaactualm(R%)
                'If RegName$(R%) = "South Africa" Then Stop
                'sgh 2014/01/17

                WATERTOTALDEMAND(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                'All But First: Water supply, wastewater, including treated portion
                '------------------------------------------------------------------
                '     1=Produced
                '     2=Treated
                '     3=Treated and Reused

                'sgh 2014/03/11
                'calculate produced wastewater as function of municipal demand. Calculate the portion of produced wastewater that
                'is treated as a function of GDPPC. Calculate the reused wastewater as portion of treated initialized in first year.

                Call XYTABL("WastewaterProduced v MunicipalDemand", WATDEMAND(R%, 1), ExpectedWastewaterProduced(R%), context)
                WASTEWATERSUPPLY(R%, 1) = ExpectedWastewaterProduced(R%) - ResidWastewaterProduced(R%)

                Call XYTABL("WastewaterTreated pct of produced (Linear) versus GDP2011PCPPP(MOSTRECENT) (Log)",
        GDPPCP(R%) * 1000, TreatedPortionProduced(R%), context)

                'sgh 2014/05/09
                'if user enters wastewaterportiontreated then use that portion instead
                ExpectedTreated(R%) = TreatedPortionProduced(R%) * WASTEWATERSUPPLY(R%, 1)

                If wastewaterportiontreated(R%) <> 0 Then
                    WASTEWATERSUPPLY(R%, 2) = wastewaterportiontreated(R%) * WASTEWATERSUPPLY(R%, 1)
                Else
                    WASTEWATERSUPPLY(R%, 2) = (ExpectedTreated(R%) - ResidTreated(R%)) * wastewatertreatedm(R%)
                    'Resid(ual)Treated is absolute amount computed in first year, not changed over time
                End If

                'bbh 2019/04/02 start
                '    If WASTEWATERSUPPLY(R%, 2) < 0 Then
                '        WASTEWATERSUPPLY(R%, 2) = 0
                '    End If
                WASTEWATERSUPPLY(R%, 2) = AMAX(0, Amin(WASTEWATERSUPPLY(R%, 1), WASTEWATERSUPPLY(R%, 2)))

                '    WASTEWATERSUPPLY(R%, 3) = WASTEWATERSUPPLY(R%, 2) * ReusedPortionTreated(R%) * (1 + (0.005 * _
                '        DemandSupplyRatio(R%)))

                'bbh 2019/04/02
                WASTEWATERSUPPLY(R%, 3) = WASTEWATERSUPPLY(R%, 2) * ReusedPortionTreated(R%) * wastewatportreatreusedm(R%)
                WASTEWATERSUPPLY(R%, 3) = AMAX(0, Amin(WASTEWATERSUPPLY(R%, 2), WASTEWATERSUPPLY(R%, 3)))
                'bbh 2019/04/02 end
                'WASTEWATERSUPPLY(R%, 3) = WASTEWATERSUPPLY(R%, 2) * ReusedPortionTreated(R%)
                'ReusedPortionTreated is ratio computed in first year, not changed over time

                '    swastewatertreatedreused(R%) = WASTEWATERSUPPLY(R%, 3)

                'If RegName$(R%) = "Iran" Then Stop
                'sgh 2014/03/11

                'sgh 2017/01/03
                'If RegName$(R%) = "South Africa" Then Stop
                'surfaceWithDGR = waterwithdrawalgr(R%, 1)
                'WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%)
                'If RegName$(R%) = "South Africa" Then Stop
                'sgh 2014/03/11
                'Waterwithdrawal for surface and ground do not change over time yet. They are initialized in preprocessor
                'Waterwithdrawal for fossil water increases at either 1% or 1/7 of remaining resources, whichever is smaller.
                'Desalinated grows at the growth rate specified (waterdesalinatedgr) which is set at 8% by default.

                'If RegName$(R%) = "Bulgaria" Then Stop
                '    WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%)
                '    swatwithdsurface(R%) = WATERWITHDRAWAL(R%, 1)
                '    WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%) * waterwithdrawalm(R%, 1)
                '
                '    WATERWITHDRAWAL(R%, 2) = swatwithdground(R%)
                '    swatwithdground(R%) = WATERWITHDRAWAL(R%, 2)
                '    WATERWITHDRAWAL(R%, 2) = swatwithdground(R%) * waterwithdrawalm(R%, 2)
                '
                ''If RegName$(R%) = "Oman" Then Stop
                'If RegName$(R%) = "Turkmenistan" Then Stop

                'All But First: Water supply, renewable ground withdrawals
                '---------------------------------------------------------
                If WATERWITHDRAWAL(R%, 1) <= WATRESEXPLOITRENEW(R%, 1) Then
                    WATERWITHDRAWAL(R%, 1) = CSng(Amin(swatwithdsurface(R%) * (1 + 0.2 * igdpr(R%)),
            ((WATRESEXPLOITRENEW(R%, 1) - swatwithdsurface(R%)) / 7) + swatwithdsurface(R%)))
                Else
                    '        WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%)
                    Dim SurfaceOverExploit = WATERWITHDRAWAL(R%, 1) - WATRESEXPLOITRENEW(R%, 1)
                    WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%) - (SurfaceOverExploit / waterrenewconverge(R%, 1))
                End If

                If WATERWITHDRAWAL(R%, 2) <= WATRESEXPLOITRENEW(R%, 2) Then
                    WATERWITHDRAWAL(R%, 2) = CSng(Amin(swatwithdground(R%) * (1 + 0.2 * igdpr(R%)),
            ((WATRESEXPLOITRENEW(R%, 2) - swatwithdground(R%)) / 7) + swatwithdground(R%)))
                Else
                    '        WATERWITHDRAWAL(R%, 2) = swatwithdground(R%)
                    Dim GroundOverExploit = WATERWITHDRAWAL(R%, 2) - WATRESEXPLOITRENEW(R%, 2)
                    WATERWITHDRAWAL(R%, 2) = swatwithdground(R%) - (GroundOverExploit / waterrenewconverge(R%, 2))
                End If
                'If RegName$(R%) = "Turkmenistan" Then Stop
                'If RegName$(R%) = "Nicaragua" Then Stop

                'All But First: Water supply, fossil withdrawals
                '-------------------------------------------------

                'If RegName$(R%) = "Saudi Arabia" Then Stop
                If swatwithdfossilground(R%) = 0 Then
                    WATERWITHDRAWAL(R%, 3) = 0
                Else
                    'jrs 2014/09/12 added convert to single, couldn't evaluate for Egypt (0.908999998022943)^(1/10)
                    'sgh 2014/09/19

                    FossilGrowthRateAdjust(R%) = CSng(CSng(iFossilWithdrawal(R%) / swatwithdfossilground(R%)) ^ (1 / 10)) * 1.01!
                    '    WATERWITHDRAWAL(R%, 3) = Amin(swatwithdfossilground(R%) * FossilGrowthRateAdjust(R%), _
                    '        (swaterresfossil(R%) - swatwithdfossilground(R%)) / 7)
                    'sgh 2014/09/19
                    '    WATERWITHDRAWAL(R%, 3) = Amin(CSng(CSng(swatwithdfossilground(R%) * 1.01 * _
                    '        (iFossilWithdrawal(R%) / swatwithdfossilground(R%))) ^ (1 / 10)), _
                    '        ((swaterresfossil(R%) - swatwithdfossilground(R%)) / 7))
                    WATERWITHDRAWAL(R%, 3) = swatwithdfossilground(R%)
                    'If RegName$(R%) = "Saudi Arabia" Then Stop
                End If

                'sgh 2018/01/04 - START
                WATERWITHDRAWAL(R%, 3) = Amin(swatwithdfossilground(R%), (swaterresfossil(R%) / 25))
                'If RegName$(R%) = "UAE" Then Stop
                'sgh 2018/01/04 - END

                'sgh 2017/01/23
                If WATERWITHDRAWAL(R%, 3) <= 0 Then
                    WATERWITHDRAWAL(R%, 3) = 0.001
                End If

                swatwithdfossilground(R%) = WATERWITHDRAWAL(R%, 3)
                WATERWITHDRAWAL(R%, 3) = swatwithdfossilground(R%) * waterwithdrawalm(R%, 3)

                'sgh 2018/01/04 - START
                '    swaterresfossil(R%) = swaterresfossil(R%) - swatwithdfossilground(R%)
                swaterresfossil(R%) = swaterresfossil(R%) - WATERWITHDRAWAL(R%, 3)
                'sgh 2018/01/04

                WATERRESFOSSIL(R%) = swaterresfossil(R%)

                'If RegName$(R%) = "UAE" Then Stop

                'sgh 2014/09/04
                'For countries where supply exceeds demand, remaining desalination potential is set
                'at 20% higher than the initial value of desalinated water. For countries where demand exceeds supply,
                'remaining potential is equivalent to the gap between demand and supply.
                'Desalinated water grows at an inital growth rate of 8%, diminishing over time,
                'and approaching the limit. Desalinated water cannot decrease faster than 2%.
                '    WATERDESALINATED(R%) = swaterdesalinated(R%) * (1 + (waterdesalinatedgr(R%) / 100))
                '    watertotaldemand(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)

                'sgh 2017/01/23
                'If RegName$(R%) = "SierraLeo" Then Stop
                '    WATERTOTALSUPPLY(R%) = swatresexploitrenewsurface(R%) + swatresexploitrenewground(R%) + sWastewaterTreatedReused(R%) + _
                '        swatwithdfossilground(R%) + swaterdesalinated(R%) + demandsupplyadjfactor(R%)

                '    WATERTOTALSUPPLY(R%) = swatwithdsurface(R%) + swatwithdground(R%) + sWastewaterTreatedReused(R%) + _
                '        swatwithdfossilground(R%) + swaterDesalinated(R%) + demandsupplyadjfactor(R%)
                '    swatwithdsurface(R%) = WATERWITHDRAWAL(R%, 1)
                '    swatwithdground(R%) = WATERWITHDRAWAL(R%, 2)

                'sgh 2017/01/23

                'All But First: Water supply, desalinated
                '----------------------------------------

                sTotalWaterSupplyNoDesal(R%) = WATERTOTALSUPPLY(R%) - swaterdesalinated(R%)
                ShareDemandDesal(R%) = swaterdesalinated(R%) / WATERTOTALDEMAND(R%)
                'If RegName$(R%) = "Israel" Then Stop
                If WATERTOTALSUPPLY(R%) / WATERTOTALDEMAND(R%) > 1 Then
                    remainingdesalpot(R%) = iDesal(R%) * 0.2!
                Else
                    remainingdesalpot(R%) = (WATERTOTALDEMAND(R%) - WATERTOTALSUPPLY(R%))
                End If
                'If RegName$(R%) = "Saudi Arabia" Then Stop
                '    WATERDESALINATED(R%) = Amin(swaterDesalinated(R%) * (1 + (waterdesalinatedgr(R%) / 100) * _
                '        (isharedemanddesal(R%) / ShareDemandDesal(R%)) ^ (1 / 20)), (remainingdesalpot(R%) / 20) + swaterDesalinated(R%))

                'sgh 2018/01/18 - START
                'do that after price is adjusted
                '    WATERDESALINATED(R%) = Amin(swaterdesalinated(R%) * (1 + (proportionchangeinwatprice * 0.5)), _
                '        swaterdesalinated(R%) * (1 + (waterdesalinatedgr(R%) / 100)))
                'sgh 2018/01/18 - END

                'If RegName$(R%) = "Bahrain" AndAlso IY% > 10 Then Stop
                'sgh 2014/09/04


                'sgh 2014/05/29
                'Do not let desalinted water force total water supply higher than total water demand.
                'If watertotaldemand(R%) < WATERTOTALSUPPLY(R%) AndAlso RegName$(R%) = "Saudi Arabia" Then Stop
                '    WATERTOTALDEMAND(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                '    If WATERTOTALSUPPLY(R%) > watertotaldemand(R%) AndAlso _
                '        WATERDESALINATED(R%) > watertotaldemand(R%) - sTotalWaterSupplyNoDesal(R%) _
                '            AndAlso watertotaldemand(R%) - sTotalWaterSupplyNoDesal(R%) > 0 Then
                ''If RegName$(R%) = "United States of America" Then Stop
                '            WATERDESALINATED(R%) = AMAX((watertotaldemand(R%) - sTotalWaterSupplyNoDesal(R%)), swaterDesalinated(R%) * 0.98)
                '    End If
                'sgh 2014/05/29

                'sgh 2014/05/29
                'Limit exploitable surface water growth to .5% or 1/40 of remaining potential, whichever
                'is smaller. The maximum exploitable surface water (MaxExploitSurface) is 20% higher
                'than the initial exploitable surface water resources (iwatresexploitrenewsurface). This growth
                'in exploitable surface water does not include secondary water however i.e. treated wastewater that
                'is not directly reused. Thus, this must be subtracted from surface exploitable first.
                'If RegName$(R%) = "Saudi Arabia" AndAlso IY% > 10 Then Stop
                '    swaterdesalinated(R%) = WATERDESALINATED(R%)
                WATERDESALINATED(R%) = swaterdesalinated(R%) * waterdesalinatedm(R%)

                'All But First: Water supply, renewable surface withdrawals, including treated for re-use
                '----------------------------------------------------------------------------------------

                'swatresexploitrenewsurface(R%) = swatresexploitrenewsurface(R%) - sSecondaryWater(R%)
                MaxExploitableSurface(R%) = iwatresexploitrenewsurface(R%) * 1.2!
                RemainingExploitableSurface(R%) = MaxExploitableSurface(R%) - swatresexploitrenewsurface(R%)

                'If RegName$(R%) = "South Africa" Then Stop
                swatresexploitrenewsurface(R%) = CSng(Amin((swatresexploitrenewsurface(R%) * 1.005),
        (((RemainingExploitableSurface(R%)) / 40) + swatresexploitrenewsurface(R%))))
                'If RegName$(R%) = "Jordan" Then Stop
                'sgh 2014/05/29

                'sgh 2014/05/08
                'Define secondary water as the volume of treated water that is not directly reused. This is added to surface
                'exploitable. Exploitable surface water multiplier (watresexploitrenewm) is applied first though.
                'This is because the watresexploitrenewm parameter is meant to represent increases in infrastructure
                'and not increased secondary water. Secondary water will increase exploitable surface water as well
                'but the parameter affecting this is the portion of wastewater that is treated.
                'If RegName$(R%) = "India" Then Stop
                secondarywater(R%) = swastewatertreated(R%) - swastewatertreatedreused(R%)
                WATRESEXPLOITRENEW(R%, 1) = (iwatresexploitrenewsurface(R%) * watresexploitrenewm(R%, 1)) +
        secondarywater(R%)

                '    secondarywater(R%) = WASTEWATERSUPPLY(R%, 2) - WASTEWATERSUPPLY(R%, 3)
                '    sSecondaryWater(R%) = secondarywater(R%)
                'If RegName$(R%) = "India" Then Stop
                'sgh 2014/05/08

                'sgh 2014/05/13
                'Exploitable groundwater resources cannot exceed 20% of the initial exploitable resources. Limit this
                'to an increase of 1/40 of remaining potential per year or .5%.
                MaxExploitableGround(R%) = iwatresexploitrenewground(R%) * 1.2!
                RemainingExploitableGround(R%) = MaxExploitableGround(R%) - swatresexploitrenewground(R%)
                WATRESEXPLOITRENEW(R%, 2) = CSng(Amin(swatresexploitrenewground(R%) * 1.005,
        (((RemainingExploitableGround(R%)) / 40) + swatresexploitrenewground(R%))))

                swatresexploitrenewground(R%) = WATRESEXPLOITRENEW(R%, 2)
                WATRESEXPLOITRENEW(R, 2) = iwatresexploitrenewground(R%) * watresexploitrenewm(R%, 2)
                'sgh 2014/05/13

                '    WATERTOTALSUPPLY(R%) = WATRESEXPLOITRENEW(R%, 1) + WATRESEXPLOITRENEW(R%, 2) + WASTEWATERSUPPLY(R%, 3) + _
                '    WATERDESALINATED(R%) + WATERWITHDRAWAL(R%, 3)
                'sgh 2017/01/18

                'All But First: Water supply, totals
                '-----------------------------------

                WATERTOTALSUPPLY(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WASTEWATERSUPPLY(R%, 3) +
    WATERDESALINATED(R%) + WATERWITHDRAWAL(R%, 3) + demandsupplyadjfactor(R%)
                'sgh 2014/09/05
                '    sTotalWaterSupplyNoDesal(R%) = WATRESEXPLOITRENEW(R%, 1) = WATRESEXPLOITRENEW(R%, 2) + WASTEWATERSUPPLY(R%, 3) + _
                '        WATERWITHDRAWAL(R%, 3)
                sTotalWaterSupplyNoDesal(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WASTEWATERSUPPLY(R%, 3) +
        WATERWITHDRAWAL(R%, 3)
                'sgh 2014/09/05

                'sgh 2014/05/05 Happy Cinco de Mayo
                'calculate the drawdown of surface water resources. This is not yet used in the code.
                'If RegName$(R%) = "South Africa" Then Stop
                '    If iwatresexploitrenewsurface(R%) = 0 Then Stop
                '    drawdown(R%) = iwatresexploitrenewsurface(R%) / WATRESEXPLOITRENEW(R%, 1)

                '    watertotaldemand(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                '    DemandSupplyRatioWater(R%) = watertotaldemand(R%) / WATERTOTALSUPPLY(R%)
                '    If DemandSupplyRatioWater(R%) < 0 Then DemandSupplyRatioWater(R%) = 0.01
                ''If RegName$(R%) = "South Africa" Then Stop
                'sgh 2014/05/05

                'sgh 2015/04/26
                'Calculate change in demand supply ratio
                'ChangeinDSRatioWater(R%) = DemandSupplyRatioWater(R%) / iDemandSupplyRatioWater(R%)
                'If ChangeinDSRatioWater(R%) = 0 Then
                '    ChangeinDSRatioWater(R%) = 1
                'End If
                'sgh 2015/04/26


                'sgh 2018/01/30 - START
                'If (WATRESEXPLOITRENEW(R%, 1) - iwatwithdsurface(R%)) = 0 Then
                'WATRESEXPLOITRENEW(R%, 1) = 0.05
                'End If
                'sgh 2018/01/30 - END

                'If RegName$(R%) = "South Africa" Then Stop

                '    WATERTOTALDEMAND(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                '    DemandSupplyRatioWater(R%) = WATERTOTALDEMAND(R%) / WATERTOTALSUPPLY(R%)
                '    If DemandSupplyRatioWater(R%) < 0 Then DemandSupplyRatioWater(R%) = 0.01
                '
                '    Call ADJSTR(1, DemandSupplyRatioWater(R%) - 1, (DemandSupplyRatioWater(R%) / sDemandSupplyRatioWater(R%)), _
                '        watprii, watprid, Mul)
                '
                '    WATPRICE(R%) = sWATPRICE(R%) * Mul
                '    sWATPRICE(R%) = WATPRICE(R%)
                '    sDemandSupplyRatioWater(R%) = DemandSupplyRatioWater(R%)
                'If RegName$(R%) = "China" Then Stop
                '    proportionchangeinwatprice = (swatPrice(R%) - 100) / 100
                'If RegName$(R%) = "Congo, Democratic Republic of" Then Stop
                'If RegName$(R%) = "Qatar" Then Stop
                '    Call XYTABL("WaterWithdAgriculture versus Irrigated land", _
                '        WATDEMAND(R%, 3) + residag(R%), adjustedirrigation)

                'If RegName$(R%) = "Qatar" Then Stop
                '
                'All But First: Water demand response to prices
                '---------------------------------------------

                'bbh 2019/04/02 Note:  proportionchangeinwatprice is absolute from year 1, not annual change
                'So structure has possibility of compounding price change over time
                'But in spite of this saving of demand and wrap in next time step, appears no impact in
                '     future years of saved municipal (1) or industrial (2) water demand on that this year
                '     Appears determined fully by analytical functions and then the price effect below
                'In sharp contrast, it appears that WATDEM ag (3) below is fully determined by the wrap
                '     of the saved value year to year until it reaches the price impact logic below

                WATDEMAND(R%, 1) = WATDEMAND(R%, 1) * (1 + (proportionchangeinwatprice * watdemandelas(R%, 1)))
                WATDEMAND(R%, 2) = WATDEMAND(R%, 2) * (1 + (proportionchangeinwatprice * watdemandelas(R%, 2)))
                swatdemandmun(R%) = WATDEMAND(R%, 1)
                swatdemandind(R%) = WATDEMAND(R%, 2)

                'sgh 2018/01/04 - START
                'Have water price directly impact water demand per HA of irrigated land rather than ag water demand.
                'Multiply ag wat demand (WATDEMAND(R%,3)) by 1,000,000 so that the units are in cubic meters per HA
                'rather than cubic kilometers per thousand hectares.
                '    WATDEMAND(R%, 3) = WATDEMAND(R%, 3) * (1 + (proportionchangeinwatprice * -0.1))


                'bbh 2019/04/02 start of new ag water logic to anchor rather than having drift
                '   No anchor function is used as for municipal/industrial
                '   With float rather than anchor, raises problems for PID and equilibration

                Dim WatDemandBase = WatDemAgRatIrAreaDrift(R%) / 1000000.0! * LANDIRAREAACTUAL(R%)
                WATDEMAND(R%, 3) = WatDemAgRatIrAreaDrift(R%) / 1000000 * LANDIRAREAACTUAL(R%) _
        * waterdemandm(R%, 3) * (1 + (proportionchangeinwatprice * watdemandelas(R%, 3)))
                Dim WatDemandMoving = 0.9! * WatDemandBase + 0.1! * WATDEMAND(R%, 3)
                WatDemAgRatIrAreaDrift(R%) = (WatDemandMoving * 1000000.0! / LANDIRAREAACTUAL(R%))
                WATDEMPERHA(R%) = WATDEMAND(R%, 3) / LANDIRAREAACTUAL(R%) * 1000000

                swatdemandag(R%) = WATDEMAND(R%, 3)  'In wrap is used for initial WaterDemandTotal for desalination

                '
                '    swaterperHA = (WATDEMAND(R%, 3) * 1000000 / LANDIRAREAACTUAL(R%))
                '    waterperha(R%) = (WATDEMAND(R%, 3) * 1000000 / LANDIRAREAACTUAL(R%)) * (1 + (proportionchangeinwatprice * watdemandelas(R%, 3)))
                '    changeInWaterPerHA = (waterperha(R%) - swaterperHA) / swaterperHA
                '    If changeInWaterPerHA < -0.02 Then
                '        waterperha(R%) = 0.98 * swaterperHA  'Does not let water price increase drive down demand by more than 2%
                '    End If
                '
                ''sgh 2018/02/13 - START
                ''save ag water demand before multiplier is used so that it doesn't grow exponentially
                '    WATDEMAND(R%, 3) = waterperha(R%) * LANDIRAREAACTUAL(R%) / 1000000   'price impact adjusted
                '    swatdemandag(R%) = WATDEMAND(R%, 3)    'price impact adjusted
                '    WATDEMAND(R%, 3) = WATDEMAND(R%, 3) * waterdemandm(R%, 3)
                ''sgh 2018/02/13 - END
                '
                '
                '
                ''bbh 2019/04/01 the expression below needed /1000000 or inside switch multiplier was  9.99999999917733E-07
                '    'that fix now needs to be explored with yield switch on
                '    'adjustedWaterPerHA = WATDEMAND(R%, 3) / LANDIRAREAACTUAL(R%)
                '    adjustedWaterPerHA = WATDEMAND(R%, 3) * 1000000 / LANDIRAREAACTUAL(R%)

                'bbh 2019/04/02 start Line below was basically comparing two copies of the same number, not working
                'syl(R%) = syl(R%) * ((1 + ((adjustedWaterPerHA - waterperha(R%)) / (waterperha(R%)))) ^ 1)
                '        IrrigationWatDepend = LANDIRAREAACTUAL(R%) / (LD(R%, 1) * 1000)
                '        YlImpactWatPrice(R%) = Amin(1.5, AMAX(0.5, 1 + (proportionchangeinwatprice * IrrigationWatDepend) * watpricetoylelas(R%)))
                '    If watertoyieldswitch(R%) > 0.5 Then
                '        YLIMPACTWATPRICE(R%) = Amin(1.5, AMAX(0.5, 1 + proportionchangeinwatprice * watpricetoylelas(R%)))
                '    Else
                '        YLIMPACTWATPRICE(R%) = 1
                '        'bbh 2019/04/02 end
                '    End If
                YLIMPACTWATPRICE(R%) = CSng(Amin(1.5, AMAX(0.5, 1 + proportionchangeinwatprice * watpricetoylelas(R%))))
                'NOTE:  The above line does not actually affect YL in ag model unless watertoyieldswitch(R%) is turned on
                '  Earlier had computed above as 1 if switch not thrown, but is useful to see the value even when not using

                'If RegName$(R%) = "Egypt" AndAlso IY% = 10 Then Stop
                'bbh 2019/04/02 end

                'If RegName$(R%) = "South Africa" Then Stop

                '    increaseInAgWaterDemand = (1 + (((WATDEMAND(R%, 3) - swatdemandag(R%)) / swatdemandag(R%)) * 0.5))
                '    If increaseInAgWaterDemand > (1.075) Then
                '        increaseInAgWaterDemand = (1.075)
                '    End If
                ''If RegName$(R%) = "Yemen" Then Stop
                '    syl(R%) = syl(R%) * increaseInAgWaterDemand
                'If RegName$(R%) = "Yemen" Then Stop

                'sgh 2018/02/13 - START
                'If RegName$(R%) = "South Africa" Then Stop
                'If waterdemandm(R%, 3) > 0 Then
                '    swatdemandag(R%) = WATDEMAND(R%, 3) / waterdemandm(R%, 3)
                'Else
                '    swatdemandag(R%) = WATDEMAND(R%, 3)
                'End If
                'If RegName$(R%) = "South Africa" Then Stop
                'sgh 2018/02/13 - END
                'If RegName$(R%) = "Qatar" Then Stop

                'All But First: Water supply, response to prices
                '-----------------------------------------------

                WATERWITHDRAWAL(R%, 1) = WATERWITHDRAWAL(R%, 1) * waterwithdrawalm(R%, 1)
                If WATERWITHDRAWAL(R%, 1) <= WATRESEXPLOITRENEW(R%, 1) Then
                    WATERWITHDRAWAL(R%, 1) = CSng(Amin(WATERWITHDRAWAL(R%, 1) * (1 + (proportionchangeinwatprice * 0.5)),
                ((WATRESEXPLOITRENEW(R%, 1) - WATERWITHDRAWAL(R%, 1)) / 7) + WATERWITHDRAWAL(R%, 1)))
                End If
                'sgh 2018/02/14 - START
                'Make sure doesn't go negative
                If WATERWITHDRAWAL(R%, 1) < 0 Then
                    WATERWITHDRAWAL(R%, 1) = 0.0001
                End If
                'sgh 2018/02/14 - END
                '    swatwithdsurface(R%) = WATERWITHDRAWAL(R%, 1)
                '    swatwithdsurface(R%) = WATRESEXPLOITRENEW(R%, 1)
                'If RegName$(R%) = "India" Then Stop
                '    WATERWITHDRAWAL(R%, 1) = swatwithdsurface(R%)

                WATERWITHDRAWAL(R%, 2) = WATERWITHDRAWAL(R%, 2) * waterwithdrawalm(R%, 2)
                If WATERWITHDRAWAL(R%, 2) <= WATRESEXPLOITRENEW(R%, 2) Then
                    WATERWITHDRAWAL(R%, 2) = CSng(Amin(WATERWITHDRAWAL(R%, 2) * (1 + (proportionchangeinwatprice * 0.5)),
                ((WATRESEXPLOITRENEW(R%, 2) - WATERWITHDRAWAL(R%, 2)) / 7) + WATERWITHDRAWAL(R%, 2)))
                End If
                'sgh 2018/02/14 - START
                'Make sure doesn't go negative
                If WATERWITHDRAWAL(R%, 2) < 0 Then
                    WATERWITHDRAWAL(R%, 2) = 0.0001
                End If
                'sgh 2018/02/14 - END
                '    swatwithdground(R%) = WATERWITHDRAWAL(R%, 2)
                '    swatwithdground(R%) = WATRESEXPLOITRENEW(R%, 2)
                '    WATERWITHDRAWAL(R%, 2) = swatwithdground(R%)
                'If RegName$(R%) = "Nicaragua" Then Stop

                '    WASTEWATERSUPPLY(R%, 3) = Amin(WASTEWATERSUPPLY(R%, 2), WASTEWATERSUPPLY(R%, 3) * (1 + (proportionchangeinwatprice * 0.5)))
                '    sWastewaterTreatedReused(R%) = WASTEWATERSUPPLY(R%, 3)

                'Constrain treated wastewater by price
                WASTEWATERSUPPLY(R%, 2) = CSng(Amin(WASTEWATERSUPPLY(R%, 1), WASTEWATERSUPPLY(R%, 2) * (1 + (proportionchangeinwatprice * 0.5))))
                swastewatertreatedreused(R%) = WASTEWATERSUPPLY(R%, 3)
                'Re-estimate direct use of treated wastewater based on constrained treatment. This is what is used as "secondary"
                'water in the next year. i.e (WASTEWATERSUPPLY(R% ,2) - WASTEWATERSUPPLY(R%,3)) = SecondaryWater, which is
                'added to exploitable surface water.

                swastewatertreated(R%) = WASTEWATERSUPPLY(R%, 2)
                swastewatertreatedreused(R%) = WASTEWATERSUPPLY(R%, 2) * ReusedPortionTreated(R%)

                swatwithdsurface(R%) = WATERWITHDRAWAL(R%, 1)
                swatwithdground(R%) = WATERWITHDRAWAL(R%, 2)

                WATERTOTALDEMAND(R%) = WATDEMAND(R%, 1) + WATDEMAND(R%, 2) + WATDEMAND(R%, 3)
                WATERTOTALSUPPLY(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WASTEWATERSUPPLY(R%, 3) +
    WATERDESALINATED(R%) + WATERWITHDRAWAL(R%, 3) + demandsupplyadjfactor(R%)
                'If RegName$(R%) = "Bulgaria" Then Stop
                DemandSupplyRatioWater(R%) = WATERTOTALDEMAND(R%) / WATERTOTALSUPPLY(R%)
                If DemandSupplyRatioWater(R%) < 0 Then DemandSupplyRatioWater(R%) = 0.01
                'If RegName$(R%) = "Saudi Arabia" Then Stop
                'If RegName$(R%) = "Nicaragua" Then Stop

                'All But First: Water price response to supply and demand balance
                '---------------------------------------------------------------
                Dim Mul = 0!, mul2 = 0!
                Call ADJSTR(1, DemandSupplyRatioWater(R%) - 1, (DemandSupplyRatioWater(R%) - sdemandsupplyratiowater(R%)),
        watprii, watprid, Mul)
                'If RegName$(R%) = "Nicaragua" Then Stop
                'If RegName$(R%) = "Rwanda" Then Stop

                'sgh 2018/01/18 - START
                'If RegName$(R%) = "Saudi Arabia" Then Stop
                'If RegName$(R%) = "Egypt" AndAlso IY% >= 21 Then Stop

                Call ADJSTR(100, (swatprice(R%) * Mul) - 100, ((swatprice(R%) * Mul) - swatprice(R%)),
                desali, desald, mul2)
                WATERDESALINATED(R%) = Amin(swaterdesalinated(R%) * mul2,
                swaterdesalinated(R%) * (1 + (waterdesalinatedgr(R%) / 100)))
                '    WATERDESALINATED(R%) = Amin(swaterdesalinated(R%) * (1 + (proportionchangeinwatprice * 0.5)), _
                '        swaterdesalinated(R%) * (1 + (waterdesalinatedgr(R%) / 100)))
                WATERTOTALSUPPLY(R%) = WATERWITHDRAWAL(R%, 1) + WATERWITHDRAWAL(R%, 2) + WASTEWATERSUPPLY(R%, 3) +
                WATERDESALINATED(R%) + WATERWITHDRAWAL(R%, 3) + demandsupplyadjfactor(R%)
                'If RegName$(R%) = "Saudi Arabia" AndAlso IY% > 10 Then Stop
                'sgh 2018/01/18 - END
                'bbh 2022/11/28 WATPRICE for some countries extreme and for NZ and Kenya oscillating; Slovakia collapsing
                '   Badly needs structural attention but also messing up water feedback to ag yields for such countries, so temporary constraints here, next 3 lines
                Mul = CSng(Amin(1.02, AMAX(0.99, Mul))) 'water price more likely to rise than fall, constraints added surpassed by few countries without them
                WATPRICE(R%) = swatprice(R%) * Mul 'This might usefully be augmented with scenario multiplier on WATPRICE or ability to exogenous specifiy or switch to turn off
                WATPRICE(R%) = Amin(250, AMAX(50, WATPRICE(R%)))
                'bbh 2022/11/28 End WATPRICE code changes; tried also with WATPRICE locked at 100 and it helps YL computation avoid major YL collapses, as in  UAE;
                'but for now leaving it to vary without these constraints; equilibration of water model needs attention
                'If RegName$(R%) = "Rwanda" Then Stop

                'All But First: Wrap variables for next teims step
                '-------------------------------------------------

                swatprice(R%) = WATPRICE(R%)
                sdemandsupplyratiowater(R%) = DemandSupplyRatioWater(R%)
                'If RegName$(R%) = "Niger" Then Stop
                swatresexploitrenewsurface(R%) = WATRESEXPLOITRENEW(R%, 1) - (WASTEWATERSUPPLY(R%, 2) - WASTEWATERSUPPLY(R%, 3))
                swatresexploitrenewground(R%) = WATRESEXPLOITRENEW(R%, 2)
                'If RegName$(R%) = "Bulgaria" Then Stop
                'If RegName$(R%) = "Niger" Then Stop
                'sgh 2018/01/04 - START
                slandirareaactual(R%) = LANDIRAREAACTUAL(R%)
                'sgh 2018/01/04 - END
                swaterdesalinated(R%) = WATERDESALINATED(R%)
                'sgh 2018/05/07
                swatertotalsupply(R%) = WATERTOTALSUPPLY(R%)
            Next
        End Sub
    End Module

End Namespace
