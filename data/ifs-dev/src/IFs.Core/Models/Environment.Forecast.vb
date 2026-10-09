Imports IFs.Data

Namespace Models

    Partial Public Module Environment

        '------------------------------------------Environment.Bas-------------------------------------
        '-------------------------------------------------------------------------------------------------------------------
        'International Futures System (IFs) - General Public License (GPL) Declaration
        '-------------------------------------------------------------------------------------------------------------------
        '
        '
        '    Copyright 1988-2012 Barry B. Hughes
        '
        '
        '    This file is part of International Futures (IFs) developed by Barry B. Hughes, bhughes@du.edu
        '
        '    IFs is free software: you can redistribute it and/or modify
        '    it under the terms of the GNU General Public License as published by
        '    the Free Software Foundation, either version 3 of the License, or
        '    (at your option) any later version.
        '
        '    IFs is distributed in the hope that it will be useful,
        '    but WITHOUT ANY WARRANTY; without even the implied warranty of
        '    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
        '    GNU General Public License for more details.
        '
        '    You should have received a copy of the GNU General Public License
        '    along with International Futures System Development Version. If not, please see http://www.gnu.org/licenses/
        '
        '    Developers of International Futures System are required to send the modified code to the IFs core team
        '    Developers should send such code updates, if any, twice every year, on April 1st and on October 1st
        '    Code updates should be sent to Frederick S. Pardee Center for International Futures (contact: pardee@du.edu)
        '-------------------------------------------------------------------------------------------------------------------
        '------------------------------------------------------------------------------------------
        '
        'If we could first know where we are, and whither we are tending,
        '    we could then better judge what to do, and how to do it.  Abraham Lincoln.

        ' If you're not making mistakes, you're not doing anything.  John Wooden

        Dim CompEnDem As Single
        'jrs 2022/05/23 changed to public
        'jrs 2009/07/10
        Public SWTEMP As Single
        Dim SENVTEMP() As Single
        Dim TEMPChg() As Single
        'jrs 2009/10/29
        Dim ENVPM2PT5Shift() As Single
        Dim ForVal As Single
        Public ENVPM2PT5ConvFct() As Single
        'jrs 2009/12/07
        'sgh 2016/06/20
        'Dim SENVPREC() As Single
        Public SENVPREC() As Single
        'sgh 2016/06/20
        'sgh 2016/07/03
        'Dim PRECChg() As Single
        Public PRECChg() As Single
        Public SENVPRCHG() As Single
        'sgh 2016/07/03
        ' Dim SENVYLCHG() As Single'bbh 2013/1/2 not needed because Public and Redim 1st year in ag before env call
        'jrs 2014/03/19
        Dim IENVYLCHG() As Single
        'jrs 2010/01/06
        Public SENVPM2PT5() As Single, SENVPM2PT5DDF() As Single
        Dim ENVPM2PT5DDFShift() As Single
        'jrs 2010/03/02
        Dim CARANNShift() As Single
        'jrs 2023/10/23
        Public SCARANN() As Single
        'jrs 2013/12/04
        Public SCOOKSTOVES(,) As Single, COOKSTOVESDD(,) As Single, CookSToves_AddSTock(,) As Single, CookStoves_Remove(,,) As Single, ICOOKSTOVES(,) As Single
        'jrs 2023/12/05
        Public SCOOKSTOVESPHH(,) As Single
        Dim CookStovesObsLag As Integer
        'jrs 2022/03/21
        Public SENVTPCHG() As Single
        'jrs 2022/04/04
        Dim IENVTPCHG() As Single
        'jrs 2022/08/03
        Public SENVTEMP1(), SENVTEMP2() As Single
        'jrs 2022/11/02
        Public IENVTEMP() As Single
        'jrs 2023/05/02
        Public XENVTEMP() As Single
        Public XENVTEMPLOAD() As Boolean
        'jrs 2023/05/09
        Public TSCALED(), STSCALED(), XTSCALED() As Single


        '   The plan is nothing.  Planning is everything.  Military aphorism.
        '   Pessimists always sound smarter than optimists.  Anonymous

        '   My name is Ozymandias, King of Kings; Look on my works ye Mighty and despair!  Shelley.

        '
        '
        '   Developed by Barry B. Hughes 1988-2012
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '
        '
        '     Last changed by BBH: 2005
        'ttt More
        Dim Deforest() As Single, SForest() As Single
        Dim ASARawI(,) As Single, ASAGDPI(,) As Single, ASAPOPI(,) As Single, ASALABI(,) As Single
        Dim popi() As Single, GDPI() As Single, LABI() As Single
        Dim enpool() As Single, SumProd() As Single
        Dim SumCarb() As Single
        Dim ENVPREC() As Single 'jrs 2022/08/03 ENVTEMP(), 

        '---
        Dim swfor As Single = 10.0! 'Initial annual decline in world forest area, mha
        Dim sacarb As Single = 0
        Dim WCarbM As Single = 0
        Dim WTEMPI As Single = 0
        'End ttt More

        Public Sub IFsEnviron1FirstYear(context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '
            '
            '     Copyright by Barry B. Hughes 1988-2012
            '     Model code available for review and education only
            '     Reproduction or incorporation in other software by permission only
            '
            '
            '     ENVIRONMENT MODEL
            '
            '
            '     ASA CATEGORIES:  1=Fossil fuel use;
            '                      2=Annual CO2; 3= Cumulative CO2
            '                      4=Water
            '                      5=Annual Net deforestation; 6=Cumulative net deforestation

            'jrs 2007/03/23
            sErrorSub = "IFsEnviron1"

            ReDim Deforest(NR%), SForest(NR%)
            ReDim ASARawI(NR%, 4), ASAGDPI(NR%, 4), ASAPOPI(NR%, 4), ASALABI(NR%, 4)
            ReDim popi(NR%), GDPI(NR%), LABI(NR%)
            ReDim enpool(Renew), SumProd(NR%)
            'jrs 2009/07/10
            ReDim TEMPChg(NR%), SENVTEMP(NR%)
            ReDim SumCarb(NR%)
            'jrs 2009/12/07
            '      ReDim PRECChg(NR%), SENVYLCHG(NR%), SENVPREC(NR%)
            'jrs 2014/03/19
            ReDim IENVYLCHG(NR%)
            ReDim PRECChg(NR%), SENVPREC(NR%) ' bbh 2013/1/2 SENVYLCHG not needed; public in Ag
            ReDim ENVPREC(NR%) 'jrs 2022/08/03 ENVTEMP(NR%), 
            'jrs 2010/01/06
            ReDim SENVPM2PT5(NR%), SENVPM2PT5DDF(NR%), ENVPM2PT5DDFShift(NR%)
            'jrs 2010/03/02
            ReDim CARANNShift(NR%)
            'jrs 2023/10/23
            ReDim SCARANN(NR%)
            'jrs 2013/12/04
            CookStovesObsLag = 10
            ReDim SCOOKSTOVES(NR%, 3), COOKSTOVESDD(NR%, 3), ICOOKSTOVES(NR%, 3) 'jrs 2013/12/19
            'jrs 2023/12/05
            ReDim SCOOKSTOVESPHH(NR%, 3)
            ReDim CookSToves_AddSTock(NR%, 3), CookStoves_Remove(NR%, 3, CookStovesObsLag)

            'sgh 2016/07/04
            ReDim SENVPRCHG(NR%)
            'sgh 2016/07/04

            'jrs 2022/03/21
            ReDim SENVTPCHG(NR%)
            'jrs 2022/04/04
            ReDim IENVTPCHG(NR%)
            'jrs 2022/08/03
            ReDim SENVTEMP1(NR%), SENVTEMP2(NR%)
            'jrs 2022/11/02
            ReDim IENVTEMP(NR%)
            'jrs 2023/05/02
            ReDim XENVTEMP(NR%), XENVTEMPLOAD(NR%)
            'jrs 2023/05/09
            ReDim TSCALED(NR%), STSCALED(NR%), XTSCALED(NR%)

            Dim i As Integer = 1
            If Not context.HistoricMode Then
                Dim tbl As String = "SeriesEnvAvgAnnTempChg"
                Dim CENVTPCHG As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each item In (From s In context.GetSlice("SeriesEnvAvgAnnTempChg", 1999)
                                  Select Key = s.Key, Value = s.Value)
                    CENVTPCHG.Add(item.Key, If(item.Value, 0))
                Next
                'jrs 2009/12/08
                tbl = "SeriesEnvAvgAnnTemp"
                Dim CENVTEMP As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each item In (From s In context.GetSlice(tbl, 1999)
                                  Select Key = s.Key, Value = s.Value)
                    CENVTEMP.Add(item.Key, If(item.Value, 0))
                Next

                'jrs 2009/12/07
                'Average Annual Precipitation
                '----------------------------
                tbl = "SeriesEnvPrecipitationChg"

                Dim CENVPRCHG As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each item In (From s In context.GetSlice(tbl, 1999)
                                  Select Key = s.Key, Value = s.Value)
                    CENVPRCHG.Add(item.Key, If(item.Value, 0))
                Next

                tbl = "SeriesEnvPrecipitation"
                Dim CENVPREC As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each item In (From s In context.GetSlice(tbl, 1999)
                                  Select Key = s.Key, Value = s.Value)
                    CENVPREC.Add(item.Key, If(item.Value, 0))
                Next

                'Land Area for weighting
                '------------------------
                'LoadCountryLandArea db
                Dim CLANDAREA As Dictionary(Of String, Single?) = New Dictionary(Of String, Single?)
                For Each item In (From s In context.GetSlice("LANDAREA", "MostRecent")
                                  Select Key = s.Key, Value = s.Value)
                    CLANDAREA.Add(item.Key, CSng(If(item.Value, 0.0001)))
                Next


                'Regionalize Country Data
                '------------------------
                'ttt Skipped Please Check
                '      RegAvgOneDimInMem(CENVTPCHG, CLandArea, TEMPChg, "CENVTPCHG")
                For Each item In (From s In CENVTPCHG
                                  Join r In context.Regionalization On s.Key Equals r.Key
                                  Join w In CLANDAREA On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
               ).ToDictionary(
                     Function(grp) grp.RegionId,
                     Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )
                    TEMPChg(item.Key) = CSng(item.Value)
                Next

                '      'jrs 2009/12/07
                '      RegAvgOneDimInMem CENVPRCHG, CLandArea, PRECChg, "CENVPRCHG"
                For Each item In (From s In CENVPRCHG
                                  Join r In context.Regionalization On s.Key Equals r.Key
                                  Join w In CLANDAREA On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
               ).ToDictionary(
                     Function(grp) grp.RegionId,
                     Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )
                    PRECChg(item.Key) = CSng(item.Value)
                Next
                'RegAvgOneDimInMem CENVTEMP, CLandArea, SENVTEMP, "CENVTEMP"
                For Each item In (From s In CENVTEMP
                                  Join r In context.Regionalization On s.Key Equals r.Key
                                  Join w In CLANDAREA On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
               ).ToDictionary(
                     Function(grp) grp.RegionId,
                     Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )
                    SENVTEMP(item.Key) = CSng(item.Value)
                Next
                'RegAvgOneDimInMem CENVPREC, CLandArea, SENVPREC, "CENVPREC"
                For Each item In (From s In CENVPREC
                                  Join r In context.Regionalization On s.Key Equals r.Key
                                  Join w In CLANDAREA On s.Key Equals w.Key
                                  Group Value = s.Value, Weight = w.Value By RegionId = r.Value Into Data = Group, TotalWeight = Sum(Weight)
               ).ToDictionary(
                     Function(grp) grp.RegionId,
                     Function(grp) grp.Data.Sum(Function(v) v.Value * v.Weight) / grp.TotalWeight
               )
                    SENVPREC(item.Key) = CSng(item.Value)
                Next

            End If

            '
            '     FIRST TIME STEP ONLY - FIRST YEAR INITIALIZATIONS
            '     --------------------------------------------------
            '
            '   BBH 1/29/04 Begin V5_0
            'bbh 2023/06/12 Changed swfor from 10 to 4; Our World in Data says that annual loss in 2010 decade was 4.7 mha/year,
            '     but rate of decline is slowing; in IFs years post 2017 show LD,forest going down more like 2 mha/year so even
            '     value of 4 sets up transient in forest and CARFOROTH and we probably need to review land use change in ag model (too little growth in grazing?)
            'swfor = 10 'Initial annual decline in world forest area, mha
            swfor = 4.0! 'Initial annual decline in world forest area, mha
            Dim WCarbEm = 0!
            For R% = 1 To NR%
                swfor = swfor + LD(R%, Fors)
                SForest(R%) = LD(R%, Fors)
                WCarbEm = WCarbEm + ENP(R%, Oil) * carfuel1 +
                ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3
            Next R%
            sacarb = carinit
            'jrs 2023/02/16 sacarb = 802.98
            'bbh 2020/02/24 start
            'Atomic weight of carbon is 12, oxygen is 16; 12 + 2*16 =44
            '  Hence to move from carbon to CO2, multiply by 44/12 = 3.67; Carbon Project says 3.664
            '  To move from CO2 to carbon, divide by 3.664
            'In 2015 the CO2PPM value of 401.5 was associaed with 797.61 billion tons of carbon
            '  Hence to move from PPM to weight of carbon, multiply by 797.61/401.5=1.9866
            '  To move from weight of carbon (billion tons?) to PPM, divide by 1.9866
            '  With preindustrial PPM level of 280, preindustrial weight of carbon would be 280*1.9866=556.24
            '  Model was assuming 560 preindustrial carbon weight in atmosphere (carprein)
            'At https://cdiac.ess-dive.lbl.gov/pns/faq.html (great source) it says "1 ppmv of CO2= 2.13 Gt of carbon"
            '  Hence 280 ppm pre-industrial = 596.4 gigatons of carbon
            '  Hence 400.83 ppm (Mauna Loa) in 2015 = 853.77 gigatons of carbon = 3128.21 gigatons of CO2
            'bbh 2020/02/24 end

            'Moved value from 740 to 755 to make PPM in 2005 379.8; data from Mauna Loa
            'October 10, 2008 BBH
            'Moved value from 755 to 774.37 to make PPM in 2010 389.85 average ML value for 2010.
            'BBH 2013/05/27
            'Could have moved value from 774.37 to 774.37*401.5/389.85=797.61 to get CO2PPM value of 401.5 for 2015.
            'Went back to formulation in all years, whichis
            'CO2PER = (sacarb - carprein) / carprein * 100!
            'CO2PPM = co2prein + co2prein * CO2PER / 100
            'carprein=560 and CO2prein=280 in Global parameters
            'for CO2PPM=401.5 in 2015, CO2PER= (401.5-280)*100/280=43.39
            'carinit=CO2PER/100*carprein + carprein =43.39/100*560+560=802.98 'bbh 2017/04/26
            WCarbM = carannwi / AMAX(0.01, WCarbEm)
            'jrs 2023/02/16 
            'WCarbM = CSng(9.65 / AMAX(0.01, WCarbEm))
            'WCarbM = CSng(6.9 / AMAX(0.01, WCarbEm))

            '   BBH 1/29/04 End V5_0

            'jrs 2009/10/28
            'Forecast population weighted PM2.5 levels
            '-----------------------------------------
            'jrs 2009/10/28
            'First Year: Load convertion factors for PM 2.5
            '-----------------------------------------------
            'jrs 2012/06/15 Set db = OpenDatabase(IFs_MDB2Active)
            '
            LoadPM2pt5ConvFact()
            'jrs 2012/06/15 db.Close

            'First Year: Compute shift factor
            '--------------------------------
            ReDim ENVPM2PT5Shift(NR)
            For R = 1 To NR
                ForVal = CSng(Math.Exp(5.1238 + 0.2533 * Math.Log(GDPPCP(R%)) - 0.056957 * (Math.Log(GDPPCP(R%))) ^ 2 -
                0.4758 * Math.Log(EDYRSAG25(R%, 3)) - 0.0137 * (BaseYear - 1989) - 0.14 * GDS(R, Health) / GDP(R) * 100))

                ForVal = ForVal * ENVPM2PT5ConvFct(R%)

                ENVPM2PT5Shift(R%) = ENVPM2PT5(R%) / ForVal
                'jrs 2010/01/06
                SENVPM2PT5(R%) = ENVPM2PT5(R%)
                SENVPM2PT5DDF(R%) = ENVPM2PT5(R%)
                If hlpm2pt5sw > 0 Then
                    'Distal Driver Formulation for Outdoor air polution (PM10)
                    ForVal = CSng(Math.Exp(4.8929 + 0.2145 * Math.Log(GDPPCP(R%)) - 0.0995 * (Math.Log(GDPPCP(R%)) ^ 2) -
                    0.451 * Math.Log(EDYRSAG25(R%, 3)) - 0.0181 * (BaseYear - 1989)))
                    'Convert from pm10 to pm2.5
                    ForVal = ForVal * ENVPM2PT5ConvFct(R%)
                    ENVPM2PT5DDFShift(R%) = ENVPM2PT5(R%) / ForVal
                End If
            Next R
            'jrs 2013/12/04 *** begin ***
            'First Year: Cook Stoves
            '-----------------------

            For R = 1 To NR
                'jrs 2023/12/05
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("COOKSTOVES", "Traditional", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                For i = 1 To 3
                    'jrs 2023/12/05 SCOOKSTOVES(R, i) = COOKSTOVES(R, i) / HHTOTAL(R)
                    SCOOKSTOVESPHH(R, i) = COOKSTOVES(R, i) / HHTOTAL(R)
                    SCOOKSTOVES(R, i) = COOKSTOVES(R, i)

                    COOKSTOVESDD(R, i) = COOKSTOVES(R, i) / HHTOTAL(R)
                    'jrs 2023/12/05 ICOOKSTOVES(R, i) = SCOOKSTOVES(R, i)
                    ICOOKSTOVES(R, i) = SCOOKSTOVESPHH(R, i)
                Next i
            Next R
            'jrs 2013/12/04 *** end ***
            'GoTo AllYears:->
            Call AllYears(1, context)
        End Sub

        Public Sub IFsEnviron1AllbutFirstYear(context As IForecastContext)
            'AllButFirst:


            'jrs 2009/10/28
            '     All but First:  Forecast population weighted PM2.5 levels
            '-----------------------------------------------------------
            'jrs 2009/11/25
            'jrs 2019/08/18 Dim Pm25idx As Integer = 0
            'jrs 2019/08/18 FindVarNumb(Pm25idx, ENVPM2PT5)
            Dim Pm25Min As Single
            Pm25Min = CSng(context.GetVariable("ENVPM2PT5").Min)
            For R = 1 To NR
                'jrs 2010/01/08
                If envpm25hldsw = 0 Then
                    ForVal = CSng(Math.Exp(5.1238 + 0.2533 * Math.Log(GDPPCP(R%)) - 0.056957 * (Math.Log(GDPPCP(R%))) ^ 2 -
                    0.4758 * Math.Log(EDYRSAG25(R%, 3)) - 0.0137 * (BaseYear - 1989 + IY - 1) - 0.14 * GDS(R, Health) / GDP(R) * 100))

                    ENVPM2PT5(R%) = ENVPM2PT5Shift(R%) * ForVal
                    ENVPM2PT5(R%) = ENVPM2PT5(R%) * ENVPM2PT5ConvFct(R%)
                    ENVPM2PT5(R%) = ENVPM2PT5(R%) * envpm2pt5m(R%)
                    'jrs 2009/11/20
                    ENVPM2PT5(R%) = AMAX(ENVPM2PT5(R%), Pm25Min)

                    'jrs 2010/01/06
                    SENVPM2PT5(R%) = ENVPM2PT5(R%)
                Else 'if Outdoor air polution hold switch is On
                    ENVPM2PT5(R%) = SENVPM2PT5(R%)
                End If

                If hlpm2pt5sw > 0 Then
                    'Distal Driver Formulation for Outdoor air polution (PM10)
                    ForVal = CSng(Math.Exp(4.8929 + 0.2145 * Math.Log(GDPPCP(R%)) - 0.0995 * (Math.Log(GDPPCP(R%)) ^ 2) -
                    0.451 * Math.Log(EDYRSAG25(R%, 3)) - 0.0181 * (BaseYear - 1989 + IY - 1)))
                    'Convert from pm10 to pm2.5
                    SENVPM2PT5DDF(R%) = ForVal * ENVPM2PT5ConvFct(R%) * ENVPM2PT5DDFShift(R%)
                End If
            Next R
            'jrs 2013/12/04 *** begin ***
            'Other Years: Cook Stoves
            '------------------------
            Dim NotModern As Single = 0
            For R = 1 To NR
                'If GRPS(1, R) = "China" Then Stop
                'Convert from Percent to Millions
                COOKSTOVES(R, 3) = (100 - sENSOLFUEL(R)) * HHTOTAL(R) / 100
                If (ICOOKSTOVES(R, 1) + ICOOKSTOVES(R, 2)) > 0 Then
                    COOKSTOVES(R, 2) = sENSOLFUEL(R) * HHTOTAL(R) / 100 * (ICOOKSTOVES(R, 2) / (ICOOKSTOVES(R, 1) + ICOOKSTOVES(R, 2)))
                    COOKSTOVES(R, 1) = sENSOLFUEL(R) * HHTOTAL(R) / 100 * (ICOOKSTOVES(R, 1) / (ICOOKSTOVES(R, 1) + ICOOKSTOVES(R, 2)))
                Else
                    COOKSTOVES(R, 2) = 0
                    COOKSTOVES(R, 1) = sENSOLFUEL(R) * HHTOTAL(R) / 100
                End If

                'jrs 2013/12/19
                'Distal Driver Formulation
                '=========================
                Dim FormValue = 0!
                Call AnalFunc("Percent of People using Primarily Solid Fuels (Distal 2010)",
                New Single() {GDPPCP(R%), EDYRSAG25(R%, 3)}, FormValue,
                New String() {"GDPPCP", "Educ25"}, context) 'jrs 2014/10/09 added DepVar Names to check order

                COOKSTOVESDD(R, 3) = CSng(1 - (ENSOLFUELShift(R%) * (1 / (Math.Exp(Amin(-1 * FormValue, 709)) + 1))))
                If (ICOOKSTOVES(R, 1) + ICOOKSTOVES(R, 2)) > 0 Then
                    COOKSTOVESDD(R, 2) = CSng(ENSOLFUELShift(R%) * (1 / (Math.Exp(Amin(-1 * FormValue, 709)) + 1)) * (ICOOKSTOVES(R, 2) / (ICOOKSTOVES(R, 1) + ICOOKSTOVES(R, 2))))
                    COOKSTOVESDD(R, 1) = CSng(ENSOLFUELShift(R%) * (1 / (Math.Exp(Amin(-1 * FormValue, 709)) + 1)) * (ICOOKSTOVES(R, 1) / (ICOOKSTOVES(R, 1) + ICOOKSTOVES(R, 2))))
                Else
                    COOKSTOVESDD(R, 2) = 0
                    COOKSTOVESDD(R, 1) = CSng(ENSOLFUELShift(R%) * (1 / (Math.Exp(Amin(-1 * FormValue, 709)) + 1)))
                End If
                'Load the added STock
                'CookSToves_AddSTock(R, 1) = CookSToves_AddSTock(R, 1) + cookstovesadd(R, 1) - CookStoves_Remove(R, 1, CookStovesObsLag)
                'jrs 2026/07/06
                CookSToves_AddSTock(R, 1) = CookSToves_AddSTock(R, 1) + cookstovesadd(R, 1)

                CookSToves_AddSTock(R, 2) = CookSToves_AddSTock(R, 2) + cookstovesadd(R, 2) - CookStoves_Remove(R, 2, CookStovesObsLag)
                'jrs 2014/01/03 CookSToves_AddSTock(R, 3) = CookSToves_AddSTock(R, 3) + cookstovesadd(R, 3) - CookStoves_Remove(R, 3, CookStovesObsLag)
                'No Obsolescence for Moder Cook Stoves
                'so no Remove Term
                CookSToves_AddSTock(R, 3) = CookSToves_AddSTock(R, 3) + cookstovesadd(R, 3)

                'Cook Stoves Added must come from other category
                'jrs 2026/07/08 enabled again
                'From Improved back to Traditional
                COOKSTOVES(R, 1) = COOKSTOVES(R, 1) + CookSToves_AddSTock(R, 1)
                COOKSTOVES(R, 2) = COOKSTOVES(R, 2) - CookSToves_AddSTock(R, 1)
                If COOKSTOVES(R, 2) < 0 Then
                    COOKSTOVES(R, 2) = 0
                End If

                'From Traditional to Improved
                COOKSTOVES(R, 2) = COOKSTOVES(R, 2) + CookSToves_AddSTock(R, 2)
                COOKSTOVES(R, 1) = COOKSTOVES(R, 1) - CookSToves_AddSTock(R, 2)
                If COOKSTOVES(R, 1) < 0 Then
                    COOKSTOVES(R, 2) = COOKSTOVES(R, 2) + COOKSTOVES(R, 1)
                    COOKSTOVES(R, 1) = 0
                End If
                'FROM Improved to Modern Fuels
                NotModern = COOKSTOVES(R, 1) + COOKSTOVES(R, 2)
                If NotModern > 0 Then
                    'First try to use the same portion to compensate
                    COOKSTOVES(R, 3) = COOKSTOVES(R, 3) + CookSToves_AddSTock(R, 3)
                    COOKSTOVES(R, 2) = COOKSTOVES(R, 2) - CookSToves_AddSTock(R, 3) * COOKSTOVES(R, 2) / NotModern
                    COOKSTOVES(R, 1) = COOKSTOVES(R, 1) - CookSToves_AddSTock(R, 3) * COOKSTOVES(R, 1) / NotModern
                    'if there's no room assign it from Traditional first
                    If COOKSTOVES(R, 2) < 0 Then
                        COOKSTOVES(R, 1) = COOKSTOVES(R, 1) + COOKSTOVES(R, 2)
                        COOKSTOVES(R, 2) = 0
                        If COOKSTOVES(R, 1) < 0 Then
                            COOKSTOVES(R, 3) = COOKSTOVES(R, 3) + COOKSTOVES(R, 1)
                            COOKSTOVES(R, 1) = 0
                        End If
                    End If
                    If COOKSTOVES(R, 1) < 0 Then
                        COOKSTOVES(R, 2) = COOKSTOVES(R, 2) + COOKSTOVES(R, 1)
                        COOKSTOVES(R, 1) = 0
                        If COOKSTOVES(R, 2) < 0 Then
                            COOKSTOVES(R, 3) = COOKSTOVES(R, 3) + COOKSTOVES(R, 2)
                            COOKSTOVES(R, 2) = 0
                        End If
                    End If
                    'jrs 2024/02/29
                    If COOKSTOVES(R, 3) < 0 Then
                        COOKSTOVES(R, 3) = 0
                    End If
                End If
                'jrs 2023/12/05
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("COOKSTOVES", "Traditional", "", CStr(FirstYear + IY - 1), R%, context)
                End If

                Dim i As Integer = 0
                'jrs 2024/01/15 adding cap
                Dim CookStovesTot As Single = 0
                For i = 1 To 3
                    CookStovesTot = CookStovesTot + COOKSTOVES(R, i)
                Next i
                If CookStovesTot > HHTOTAL(R) And CookStovesTot > 0 Then
                    For i = 1 To 3
                        COOKSTOVES(R, i) = COOKSTOVES(R, i) * HHTOTAL(R) / CookStovesTot
                    Next i
                End If

                For i = 1 To 3
                    'jrs 2023/12/05 SCOOKSTOVES(R, i) = COOKSTOVES(R, i) / HHTOTAL(R)
                    SCOOKSTOVESPHH(R, i) = COOKSTOVES(R, i) / HHTOTAL(R)
                    SCOOKSTOVES(R, i) = COOKSTOVES(R, i)

                    'jrs 2014/01/03
                    'not necessary for Modern Cook stoves anymore
                    'no obsolescence
                    If i < 3 Then
                        For j = CookStovesObsLag To 2 Step -1
                            CookStoves_Remove(R, i, j) = CookStoves_Remove(R, i, j - 1)
                        Next j
                        CookStoves_Remove(R, i, 1) = cookstovesadd(R, i)
                    End If
                Next i
            Next R
            'jrs 2013/12/04 *** end ***
            Call AllYears(IY, context)
        End Sub

        Private Sub AllYears(fIY As Integer, context As IForecastContext)
            'AllYears:
            '     ALL YEARS
            '     ----------------------------------------------------------------
            'ttt SENVYLCHG Of Agriculture, but calculated here
            ReDim SENVYLCHG(NR%)
            Dim wfor = 0!
            For R% = 1 To NR%
                wfor = wfor + LD(R%, Fors)
            Next R%
            '
            '     All Years:  Indicators of Carbon Dioxide Build-up - Use Production Data
            '     -------------------------------------------------------------------------
            '     Ratio of carbon dioxide weight to carbon is 3.67

            'bbh 2020/02/24 start
            CARFOROTH = (swfor - wfor) * carforst
            'sacarb = sacarb + CARFOROTH - carabr
            Dim WCARANNSUPSide = 0!

            'jrs 2010/03/02
            For R% = 1 To NR%
                '   BBH 1/29/04 V5_0
                'jrs 2010/03/10
                '          CARANN(R%) = (ENP(R%, Oil) * carfuel1 + _
                '          ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM
                '          sacarb = sacarb + CARANN(R%)
                '          WCARANNSUPSide = WCARANNSUPSide + CARANN(R%)
                WCARANNSUPSide = WCARANNSUPSide + (ENP(R%, Oil) * carfuel1 +
                ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM
                'Emissions of carbon from each barrel of oil equivalent of 3 fossil fuels (carfuel1, 2, 3):
                'At https://www.epa.gov/energy/greenhouse-gases-equivalencies-calculator-calculations-and-references
                'the EPA says that a barrel of oil contains  0.43 metric tons of carbon (the Google blurb says 4.2961
                'kilograms per barrel).  Divide that by 3.664 (next entry below) and you have 0.11735 metric tons per barrel.
                'The old value in IFs was 0.0831, so replaced with this.
                'The EIA at https://www.eia.gov/tools/faqs/faq.php?id=73&t=11 gives relative CO2 content for various fossil fuel
                'BTU equivalents.  Using that can set gas at 0.0857 (was 0.0581) and coal at 0.15536 (was 0.01040).
                'When these are swapped into GlobalParameters the global fossil fuel production in IFs in 2015 times them
                'produces a total emissions very close to the CMIP project numbers; before they had to be scaled up
                'to exogenous annual emissions with a significant shift factor.
            Next R%
            'jrs 2010/03/10
            sacarb = sacarb + CSng((CARFOROTH - carabr + WCARANNSUPSide) / 2.13)
            'jrs 2023/02/16 sacarb = sacarb + WCARANNSUPSide
            '1 ppm is 2.13 GT carbon; so 1 GT carbon = 1/2.13 PPM

            swfor = wfor
            'bbh 2019/04/06
            ' CO2PER = (sacarb - carprein) / carprein * 100!
            'jrs 2023/02/16 it was 280 carprein = 560
            CO2PER = (sacarb - carprein) / carprein * 100.0! * co2perm
            'bbh 2020/02/24 end

            CO2PPM = co2prein + co2prein * CO2PER / 100

            'bbh 2022/04/26 start Move DeltaWT and WTEMP from late and bottom of procedure to here to replace old function use
            Dim DeltaWT As Single, CO2PPM1990 As Single
            If context.HistoricMode = False Then
                'source:
                'http://cdiac.ornl.gov/ftp/trends/co2/maunaloa.co2
                CO2PPM1990 = 354.19
                DeltaWT = CSng(envclimsens / Math.Log(2) * Math.Log(CO2PPM / CO2PPM1990))
                If IY = 1 Then
                    'finding WTEMP for 1990, based on CO2 levels of 350
                    'Call XYTABL("CO2 PPM Versus Global Temperature", CO2PPM1990, SWTEMP)
                    'using data from Historic Table temperatures (Dale)
                    'Source: http://www.ncdc.noaa.gov/oa/climate/research/anomalies/index.html#mean
                    SWTEMP = 14.2861  'In 2020 Dale Rothman suggested could be 14.43C in Climate change and impact.docx p6 comment
                End If

                'bbh 2019/04/06
                'WTEMP = SWTEMP + DeltaWT
                WTEMP = SWTEMP + DeltaWT + wtempadd
                'SWTEMP is actually an intial value not an accrued stock (DelatWT is from 1990)
            Else
                Call XYTABL("CO2 PPM Versus Global Temperature", CO2PPM, WTEMP, context)
                WTEMP = WTEMP + wtempadd
            End If
            'bbh 2022/04/26 end
            If fIY = 1 Then WTEMPI = WTEMP 'bbh 2017/10/05

            'jrs 2022/05/23 Exogenous Models
            If ifsexomodelid > 0 Then
                ReplaceVarWithExogenousForecastxReg("WTEMP", "", "", CStr(FirstYear + IY - 1), 1, context)
            End If


            For e% = 1 To Coal + 1
                enpool(e%) = 0
            Next e%

            For R% = 1 To NR%

                'bbh 2017/10/05 '+1 because we already have 1 degree C increase from pre-industrial
                'jrs 2022/04/04 CLIMECONIMP(R%) = CSng(climeconimplin * (WTEMP - WTEMPI + 1) + climeconimpsq * (WTEMP - WTEMPI + 1) ^ 2)
                '     If (fIY = 2 AndAlso RegName$(R%) = "United States of America") OrElse  (fIY = 85 AndAlso RegName$(R%) = "United States of America") Then Stop
                '
                '       All Years:  Environmental Stress - switch to demand side calculation
                '       Because fuel consumption pattern is estimated from production and global
                '       balances are not computed, accuracy is somewhat reduced
                '       ------------------------------------------------------------------------

                Dim SumNoCarb = 0!
                SumCarb(R%) = 0
                SumProd(R%) = 0
                For e% = Hydro To Renew
                    SumNoCarb = SumNoCarb + ENP(R%, e%)
                Next e%
                'If GRPS(1, R) = "Bosnia" And IY = 2 Then Stop
                For e% = Oil To Coal
                    SumCarb(R%) = SumCarb(R%) + ENP(R%, e%)
                Next e%
                SumProd(R%) = SumNoCarb + SumCarb(R%)
                If SumProd(R%) >= ENDEM(R%) Then
                    'jrs 2010/03/02
                    '                CARANN(R%) = ((ENP(R%, Oil) * carfuel1 + _
                    'ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM) * _
                    'ENDEM(R%) / SumProd(R%) 'Assume meet demand with mix of local production
                    If IY > 1 Then
                        CARANN(R%) = ((ENP(R%, Oil) * carfuel1 +
                        ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM) *
                        ENDEM(R%) / SumProd(R%) 'Assume meet demand with mix of local production
                    Else
                        CARANNShift(R%) = ((ENP(R%, Oil) * carfuel1 +
                        ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM) *
                        ENDEM(R%) / SumProd(R%)  'Assume meet demand with mix of local production
                    End If
                    'Rest goes into global pool
                    enpool(Oil) = enpool(Oil) + ENP(R%, Oil) * (SumProd(R%) - ENDEM(R%)) / ENDEM(R%)
                    enpool(Gas) = enpool(Gas) + ENP(R%, Gas) * (SumProd(R%) - ENDEM(R%)) / ENDEM(R%)
                    enpool(Coal) = enpool(Coal) + ENP(R%, Coal) * (SumProd(R%) - ENDEM(R%)) / ENDEM(R%)
                    'EnPool(Coal + 1) = EnPool(Coal + 1) + ENP(R%, Coal + 1) * (SumProd(R%) - ENDEM(R%)) / ENDEM(R%)
                    enpool(Coal + 1) = enpool(Coal + 1) + SumNoCarb * (SumProd(R%) - ENDEM(R%)) / ENDEM(R%)
                Else
                    'jrs 2010/03/02
                    'CARANN(R%) = ((ENP(R%, Oil) * carfuel1 + _
                    'ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM) 'Use all energy locally
                    If IY > 1 Then
                        CARANN(R%) = ((ENP(R%, Oil) * carfuel1 +
                        ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM) 'Use all energy locally
                    Else
                        CARANNShift(R%) = ((ENP(R%, Oil) * carfuel1 +
                        ENP(R%, Gas) * carfuel2 + ENP(R%, Coal) * carfuel3) * WCarbM) 'Use all energy locally
                    End If
                End If
                'jrs 2023/12/08
                ''jrs 2023/10/23
                'If ifsexomodelid > 0 Then
                '    ReplaceVarWithExogenousForecastxReg("CARANN", "", "", CStr(FirstYear + IY - 1), R%, context)
                'End If
                'SCARANN(R%) = CARANN(R%)
            Next R%

            Dim TotEnPool = 0!
            For e% = 1 To Coal + 1
                TotEnPool = TotEnPool + enpool(e%)
            Next e%
            Dim WCARANNDemSide = 0!
            For R% = 1 To NR%
                If SumProd(R%) < ENDEM(R%) Then 'Assume import for pool by energy shares in pool
                    Dim ImGap = ENDEM(R%) - SumProd(R%)
                    'jrs 2010/03/02
                    If IY > 1 Then
                        'jrs 2014/01/21
                        If TotEnPool > 0 Then
                            CARANN(R%) = CARANN(R%) + (ImGap * enpool(Oil) / TotEnPool * carfuel1 +
                            ImGap * enpool(Gas) / TotEnPool * carfuel2 +
                            ImGap * enpool(Coal) / TotEnPool * carfuel3) * WCarbM
                        End If
                    Else
                        CARANNShift(R%) = CARANNShift(R%) + (ImGap * enpool(Oil) / TotEnPool * carfuel1 +
                        ImGap * enpool(Gas) / TotEnPool * carfuel2 +
                        ImGap * enpool(Coal) / TotEnPool * carfuel3) * WCarbM
                    End If
                End If
                'jrs 2010/03/08
                If IY = 1 Then
                    WCARANNDemSide = WCARANNDemSide + CARANNShift(R%)
                Else
                    WCARANNDemSide = WCARANNDemSide + CARANN(R%)
                End If
            Next R%

            '     All Years:  Normalize demand side to supply side (more accurate for world total); compute ASA
            '     -----------------------------------------------------------------------------------------------

            For R% = 1 To NR%
                'jrs 2010/03/06
                If IY = 1 Then
                    CARANNShift(R%) = CARANN(R%) - CARANNShift(R%) * WCARANNSUPSide / WCARANNDemSide
                Else
                    CARANN(R%) = CARANN(R%) * WCARANNSUPSide / WCARANNDemSide
                    CARANN(R%) = CARANN(R%) + CARANNShift(R%)
                    'jrs 2026/06/02 added limit to avoid negatives
                    CARANN(R%) = CSng(AMAX(CARANN(R%), 0.00001))
                End If
                'jrs 2023/12/08
                If ifsexomodelid > 0 Then
                    ReplaceVarWithExogenousForecastxReg("CARANN", "", "", CStr(FirstYear + IY - 1), R%, context)
                End If
                SCARANN(R%) = CARANN(R%)

                TES(R%) = CARANN(R%) * 1000 / GDP(R%) 'kilograms of carbon/ unit of GDP                
                Deforest(R%) = SForest(R%) - LD(R%, Fors)
                SForest(R%) = LD(R%, Fors)

                '       All Years:  ASA
                '       -----------------
                ASARAW(R%, 1) = SumCarb(R%) 'Production side; could be switched to consumption side estimate
                ASAGDP(R%, 1) = SumCarb(R%) * 1000 / AMAX(0.001, GDP(R%)) 'barrels of oil equivalent/1000 currency
                ASAPOP(R%, 1) = SumCarb(R%) * 1000 / AMAX(0.001, POP(R%)) 'barrels of oil equivalent / capita
                ASALAB(R%, 1) = SumCarb(R%) * 1000 / AMAX(0.001, LAB(R%)) 'barrels of oil equivalent/ Lab

                ASARAW(R%, 2) = CARANN(R%)
                ASAGDP(R%, 2) = CARANN(R%) * 1000 / AMAX(0.001, GDP(R%)) 'tons/1000 currency
                ASAPOP(R%, 2) = CARANN(R%) * 1000 / AMAX(0.001, POP(R%))
                ASALAB(R%, 2) = CARANN(R%) * 1000 / AMAX(0.001, LAB(R%))

                ASARAW(R%, 3) = Deforest(R%)
                ASAGDP(R%, 3) = Deforest(R%) * 1000 / AMAX(0.001, GDP(R%))
                ASAPOP(R%, 3) = Deforest(R%) * 1000 / AMAX(0.001, POP(R%))
                ASALAB(R%, 3) = Deforest(R%) * 1000 / AMAX(0.001, LAB(R%))


                ASARAW(R%, 4) = WATUSE(R%)
                ASAGDP(R%, 4) = WATUSE(R%) * 1000 / AMAX(0.001, GDP(R%)) '
                ASAPOP(R%, 4) = WATUSE(R%) * 1000 / AMAX(0.001, POP(R%))
                ASALAB(R%, 4) = WATUSE(R%) * 1000 / AMAX(0.001, LAB(R%))

                'cumulative carbon not yet computed by region
                'cumulative deforestation not yet computed by region

                If fIY = 1 Then
                    For ICount% = 1 To 4
                        ASARawI(R%, ICount%) = ASARAW(R%, ICount%)
                        ASAGDPI(R%, ICount%) = ASAGDP(R%, ICount%)
                        ASAPOPI(R%, ICount%) = ASAPOP(R%, ICount%)
                        ASALABI(R%, ICount%) = ASALAB(R%, ICount%)
                        If ASARawI(R%, ICount%) < 0.0001 Then ASARawI(R%, ICount%) = 99999
                        If ASAGDPI(R%, ICount%) < 0.0001 Then ASAGDPI(R%, ICount%) = 99999
                        If ASAPOPI(R%, ICount%) < 0.0001 Then ASAPOPI(R%, ICount%) = 99999
                        If ASALABI(R%, ICount%) < 0.0001 Then ASALABI(R%, ICount%) = 99999
                        GDPI(R%) = GDP(R%)
                        popi(R%) = POP(R%)
                        LABI(R%) = LAB(R%)
                    Next ICount%
                End If

                ASAGDPCUMCHG(R%) = (GDP(R%) - GDPI(R%)) / AMAX(0.001, GDPI(R%)) * 100
                ASAPOPCUMCHG(R%) = (POP(R%) - popi(R%)) / AMAX(0.001, popi(R%)) * 100
                ASALABCUMCHG(R%) = (LAB(R%) - LABI(R%)) / AMAX(0.001, LABI(R%)) * 100

                For ICount% = 1 To 4
                    ASARAWDMAT(R%, ICount%) = (ASARAW(R%, ICount%) - ASARawI(R%, ICount%)) / ASARawI(R%, ICount%) * 100
                    ASAGDPDMAT(R%, ICount%) = (ASAGDP(R%, ICount%) - ASAGDPI(R%, ICount%)) / ASAGDPI(R%, ICount%) * 100
                    ASAPOPDMAT(R%, ICount%) = (ASAPOP(R%, ICount%) - ASAPOPI(R%, ICount%)) / ASAPOPI(R%, ICount%) * 100
                    ASALABDMAT(R%, ICount%) = (ASALAB(R%, ICount%) - ASALABI(R%, ICount%)) / ASALABI(R%, ICount%) * 100
                Next ICount%

                For ICount% = 1 To 4
                    ASAGDPGRRB(R%, ICount%) = ASAGDP(R%, ICount%) * (GDP(R%) - GDPI(R%)) / 1000
                    ASAPOPGRRB(R%, ICount%) = ASAPOP(R%, ICount%) * (POP(R%) - popi(R%)) / 1000
                    ASALABGRRB(R%, ICount%) = ASALAB(R%, ICount%) * (LAB(R%) - LABI(R%)) / 1000
                Next ICount%

            Next R%

            'jrs 2009/07/10
            'All Years: Climate Sensibility to Yield Crop
            '------------------------------------------------
            If Not context.HistoricMode Then
                'bbh 2022/04/26 start
                Dim DeltaP As Single, DeltaT As Single
                Dim CO2Fert As Single
                'source:
                'http://cdiac.ornl.gov/ftp/trends/co2/maunaloa.co2
                'CO2PPM1990 = 354.19
                'DeltaWT = envclimsens / Log(2) * Log(CO2PPM / CO2PPM1990)
                'bbh 2022/04/26 end
                CO2Fert = envco2fert * (CO2PPM / CO2PPM1990 - 1) * 100

                Dim T0 As Single, P0 As Single, Topt As Single, SigmaTsqd As Single
                Topt = 0.602
                SigmaTsqd = 309.809

                For R% = 1 To NR
                    T0 = SENVTEMP(R%)
                    P0 = SENVPREC(R%)

                    'jrs 2009/12/07
                    DeltaT = DeltaWT * TEMPChg(R%)
                    DeltaP = CSng(100 * ((1 + PRECChg(R%) / 100) ^ DeltaWT - 1))
                    'ENVPRCHG(R%) = SENVPRCHG(R%) * (1 + DeltaP / 100)
                    ENVPRCHG(R%) = DeltaP
                    'ENVTEMP(R%) = SENVTPCHG(R%) + DeltaT
                    ENVTPCHG(R%) = DeltaT

                    'jrs 2022/03/21 Exogenous Models
                    If ifsexomodelid > 0 Then
                        ReplaceVarWithExogenousForecastxReg("ENVTPCHG", "", "", CStr(FirstYear + IY - 1), R%, context)
                        DeltaT = ENVTPCHG(R%)
                    End If

                    'jrs 2022/10/17 Exogenous Models
                    If ifsexomodelid > 0 Then
                        ReplaceVarWithExogenousForecastxReg("ENVPRCHG", "", "", CStr(FirstYear + IY - 1), R%, context)
                        DeltaP = ENVPRCHG(R%)
                    End If

                    'jrs 2022/08/03  *** start ***
                    If IY = 1 Then
                        SENVTEMP1(R%) = ENVTEMP(R%)
                        'jrs 2022/11/02
                        IENVTEMP(R%) = ENVTEMP(R%)
                        'jrs 2023/05/09
                        TSCALED(R%) = WTEMP
                    Else
                        ENVTEMP(R%) = SENVTEMP1(R%) + ENVTPCHG(R%) - SENVTPCHG(R%)
                        SENVTEMP2(R%) = SENVTEMP1(R%)
                        SENVTEMP1(R%) = ENVTEMP(R%)
                        'jrs 2023/05/09
                        TSCALED(R%) = STSCALED(R%) + SENVTEMP1(R%) - SENVTEMP2(R%)
                    End If
                    'jrs 2022/08/03  *** end ***
                    'jrs 2023/05/09
                    STSCALED(R%) = TSCALED(R%)

                    'jrs 2023/05/02
                    If envtemptoginidomsw > 0 And Not XENVTEMPLOAD(R%) Then
                        XENVTEMP(R%) = ENVTEMP(R%)
                        XENVTEMPLOAD(R%) = True
                        'jrs 2023/05/09
                        XTSCALED(R%) = TSCALED(R%)
                    End If

                    'jrs 2022/03/21
                    SENVTPCHG(R%) = ENVTPCHG(R%)

                    'sgh 2016/07/04
                    SENVPRCHG(R%) = ENVPRCHG(R%)
                    'sgh 2016/07/04

                    'jrs 2023/02/28 adding amax to avoid blow up for log(precipitation)
                    ENVYLCHG(R%) = CSng(Math.Exp(-0.5 * ((T0 + DeltaT - Topt) ^ 2 / SigmaTsqd)) * Math.Log(AMAX(P0 * (DeltaP / 100 + 1), 0.0000000001)))
                    'jrs 2023/03/13 adding amax to avoid blow up for log(PO)
                    ENVYLCHG(R%) = CSng(ENVYLCHG(R%) / (Math.Exp(-0.5 * ((T0 - Topt) ^ 2 / SigmaTsqd)) * Math.Log(AMAX(P0, 0.0000000001))))
                    ENVYLCHG(R%) = (ENVYLCHG(R%) - 1) * 100
                    ENVYLCHG(R%) = ((CO2Fert / 100 + 1) * (ENVYLCHG(R%) / 100 + 1) - 1) * 100

                    If IY > 1 Then
                        'jrs 2014/03/19 ENVYLCHG(R%) = 100 * ((1 + ENVYLCHG(R%) / 100) / (1 + SENVYLCHG(R%) / 100) - 1)
                        ENVYLCHG(R%) = 100 * ((1 + ENVYLCHG(R%) / 100) / (1 + IENVYLCHG(R%) / 100) - 1)
                        ENVYLCHG(R%) = ENVYLCHG(R%) * envylchgm(R%)
                        ENVYLCHG(R%) = ENVYLCHG(R%) + envylchgadd
                        'jrs 2014/03/19
                        SENVYLCHG(R%) = ENVYLCHG(R%)
                    Else
                        'jrs 2014/03/19
                        IENVYLCHG(R%) = ENVYLCHG(R%)
                        SENVYLCHG(R%) = 0
                        ENVYLCHG(R%) = 0
                        'jrs 2022/04/04
                        IENVTPCHG(R%) = ENVTPCHG(R%)
                    End If

                    'jrs 2022/04/04
                    'Note: WTEMP is computed earlier using CO2 "CO2 PPM Versus Global Temperature"
                    'This means CLIMECONIMP will NOT be affected by any changes to wtempadd
                    If climeconimpperctrysw = 1 Then
                        CLIMECONIMP(R%) = CSng(climeconimplin * (ENVTPCHG(R%) - IENVTPCHG(R%) + 1) + climeconimpsq * (ENVTPCHG(R%) - IENVTPCHG(R%) + 1) ^ 2)
                    Else
                        'bbh 2017/10/05 '+1 because we already have 1 degree C increase from pre-industrial
                        CLIMECONIMP(R%) = CSng(climeconimplin * (WTEMP - WTEMPI + 1) + climeconimpsq * (WTEMP - WTEMPI + 1) ^ 2)
                    End If
                Next R%

                'bbh 2022/04/26 start Moved this up earlier to use for environmental impact
                '        If IY = 1 Then
                '            'finding WTEMP for 1990, based on CO2 levels of 350
                '            'Call XYTABL("CO2 PPM Versus Global Temperature", CO2PPM1990, SWTEMP)
                '            'using data from Historic Table temperatures (Dale)
                '            'Source: http://www.ncdc.noaa.gov/oa/climate/research/anomalies/index.html#mean
                '            SWTEMP = 14.2861
                '        End If
                '
                '        'bbh 2019/04/06
                '        'WTEMP = SWTEMP + DeltaWT
                '        WTEMP = SWTEMP + DeltaWT + wtempadd
                '            'SWTEMP is actually an intial value not an accrued stock (DelatWT is from 1990)
                'bbh 2022/04/26
            End If
        End Sub

        Public Sub LoadPM2pt5ConvFact() 'db As Database)
            'db is ..\RUNFILES\ifs.mdb
            'Load Convertion factors
            '-----------------------
            ReDim ENVPM2PT5ConvFct(NR)
            Using db As IFsContext = New IFsContext()
                Dim rsEurope = (From GI In db.GroupIDNewFull
                                Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                Where GI.GroupName = "Europe"
                                Select Key = G.RegionId).ToList
                Dim rsWorld = (From GI In db.GroupIDNewFull
                               Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                               Where GI.GroupName = "World"
                               Select Key = G.RegionId).ToList
                Dim rsDevCtries = (From GI In db.GroupIDNewFull
                                   Join G In db.GroupingFull On GI.GroupNum Equals G.GroupId
                                   Where GI.GroupName = "WB Developing Economies"
                                   Select Key = G.RegionId).ToList
                '    Dim sql As String
                '    sql = "SELECT GroupingFull.RegionNo, 0.5 AS ConvFct" &
                '" FROM (GroupIDNewFull INNER JOIN GroupingFull' ON GroupIDNewFull.GroupNum = GroupingFull.GroupNo)" &
                '" Where (((GroupIDNewFull.GroupName) = 'WB Developing Economies'))" &
                '" AND NOT EXISTS (SELECT * FROM GroupIDNewFull AS G2, GroupingFull AS F2" &
                '" WHERE G2.GroupNum = F2.GroupNo AND G2.GroupName = 'Europe' 
                'AND F2.RegionNo = GroupingFull.RegionNo)"
                For Each id In rsDevCtries
                    If Not rsEurope.Contains(id) Then
                        ENVPM2PT5ConvFct(id) = 0.5
                    End If
                Next
                '    sql = sql & " Union " &
                '" SELECT GroupingFull.RegionNo, 0.65 AS ConvFct" &
                '" FROM (GroupIDNewFull INNER JOIN GroupingFull ON GroupIDNewFull.GroupNum = GroupingFull.GroupNo)" &
                '" Where (((GroupIDNewFull.GroupName) = 'World'))" &
                '" AND NOT EXISTS (SELECT * FROM GroupIDNewFull AS G2, GroupingFull AS F2" &
                '" WHERE G2.GroupNum = F2.GroupNo AND G2.GroupName = 'Europe' 
                'AND F2.RegionNo = GroupingFull.RegionNo)" &
                '" AND NOT EXISTS (SELECT * FROM GroupIDNewFull AS G3, GroupingFull AS F3" &
                '" WHERE G3.GroupNum = F3.GroupNo AND G3.GroupName = 'WB Developing Economies' AND F3.RegionNo = GroupingFull.RegionNo)"
                For Each id In rsWorld
                    If (Not rsEurope.Contains(id)) AndAlso (Not rsDevCtries.Contains(id)) Then
                        ENVPM2PT5ConvFct(id) = 0.65
                    End If
                Next
                '    sql = sql & " Union " &
                '" SELECT GroupingFull.RegionNo, 0.73 AS ConvFct" &
                '" FROM (GroupIDNewFull INNER JOIN GroupingFull ON GroupIDNewFull.GroupNum = GroupingFull.GroupNo)" &
                '" WHERE (((GroupIDNewFull.GroupName)='Europe'));"
                For Each id In rsEurope
                    ENVPM2PT5ConvFct(id) = 0.73
                Next
            End Using
        End Sub

    End Module

End Namespace