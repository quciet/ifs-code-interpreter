Imports IFs.Data

Namespace Models

    Partial Public Module Automobile
        '
        '------------------------------------------Automobile.Bas-------------------------------------
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

        '
        '   Developed by Barry B. Hughes 1988-2012
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '
        '   Last changed by BBH on December 13, 1999
        'jrs 2009/09/23
        Dim DEATHTRPVShift() As Single
        'jrs 2009/09/25
        Dim DEATHTRPVI() As Single
        'ttt More
        Dim carstoti(), Truckstoti() As Single
        Dim carstotpci(), truckstotpci(), VehicFlpcRI() As Double
        Dim carsi(,), trucksi(,), popi() As Single
        Dim STRUCKSFL(), TRADSRATI(), SURVSEI() As Single 'jrs 2009/07/07 SCARSFL()
        Dim CarsCompI(), TrucksCompI(), ValueBaseCars(), ValueBaseTrucks() As Single
        Dim ValueBaseCarsOn(), ValueBaseTrucksOn() As Boolean
        Dim SVEHICFLPC(NR%) As Single
        Dim USidx As Integer
        Dim GrowthLimit As Single
        'jrs 2025/05/15
        Dim TFNameTotCarsPC As String

        'End ttt More
        Public Sub ifsAuto1FirstYear(context As IForecastContext)
            '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            '
            '   AUTOMOBILE MODEL

            '
            '   FIRST TIME STEP ONLY
            '   -----------------------------------------------------------------------

            ReDim carstoti(NR%), Truckstoti(NR%), carstotpci(NR%), truckstotpci(NR%), VehicFlpcRI(NR%)
            ReDim carsi(NR%, 11), trucksi(NR%, 12), popi(NR%)
            ReDim STRUCKSFL(NR%), TRADSRATI(NR%), SURVSEI(NR%) 'jrs 2009/07/07 SCARSFL(NR%)
            ReDim CarsCompI(NR%), TrucksCompI(NR%), ValueBaseCars(NR%), ValueBaseTrucks(NR%)
            ReDim ValueBaseCarsOn(NR%), ValueBaseTrucksOn(NR%)
            'jrs 2009/09/23
            ReDim DEATHTRPVShift(NR%)
            'JRS 2009/09/25
            ReDim DEATHTRPVI(NR%)
            'jrs 2010/07/15
            ReDim SVEHICFLPC(NR%)


            'jrs 2007/10/17
            'Reduction factor for Car saturation level
            Dim RdcFc As Single

            'USidx = frm_MainMenu.IFsGUIctrl1.getDimIdx(1, "United States of America", False)
            Dim v As New KeyValuePair(Of Short, String)
            'jrs 2026/03/04
            If gRegionIdx("United States of America") > 0 Then
                v = context.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("United States of America"))
                USidx = v.Key
            Else
                USidx = 0
            End If
            'jrs 2014/11/06
            If USidx <= 0 Then
                v = context.GetDimension(1).Buckets.First(Function(S) S.Value.Equals("US-California"))
                USidx = AMAX(v.Key, 0)
            End If

            'jrs 2025/05/15
            TFNameTotCarsPC = "GDP/Capita (PPP " & ConstantDollarsYear & ") Versus Total Cars/Capita"
            TFNameTotCarsPC = getFunctionNameYrOrMstRct(TFNameTotCarsPC, BaseYear, context, "(PPP " & ConstantDollarsYear & ")")


            '
            '   First Year: Initial Cars and Trucks Total and PerCapita
            '   -------------------------------------------------------
            Dim carscomp As Double = 0
            Dim truckscomp As Double = 0
            'ttt 2019/10/08 Take Out
            Array.Copy(CARSTOT, carstoti, NR + 1)
            Array.Copy(TRUCKSTOT, Truckstoti, NR + 1)
            Array.Copy(POP, popi, NR + 1)

            For R% = 1 To NR%
                'ttt 2019/10/08
                'carstoti(R%) = CARSTOT(R%)
                'Truckstoti(R%) = TRUCKSTOT(R%)
                carstotpci(R%) = CARSTOT(R%) / AMAX(0.001, POP(R%)) / 1000000
                truckstotpci(R%) = TRUCKSTOT(R%) / AMAX(0.001, POP(R%)) / 1000000
                'ttt 2019/10/08 popi(R%) = POP(R%)
                TRADSRATI(R%) = TRADSRAT(R%, WVSCohorts)
                SURVSEI(R%) = SURVSE(R%, WVSCohorts)
                If vehfuncsw = 4 Then
                    If GDPPCIP(R%) < 2.315 Then
                        carscomp = -0.0000007485 + 0.000044 * GDPPCIP(R%)
                    ElseIf GDPPCIP(R%) >= 2.315 And GDPPCIP(R%) < 4.305 Then
                        carscomp = -0.0008709 + 0.0004199 * GDPPCIP(R%)
                    ElseIf GDPPCIP(R%) >= 4.305 And GDPPCIP(R%) < 25 Then
                        carscomp = -0.006807 + 0.001799 * GDPPCIP(R%)
                    Else
                        carscomp = -0.006807 + 0.001799 * 25 + 0.0001 * (GDPPCIP(R%) - 25)
                    End If
                    '            ValueBaseCars(r%) = -0.003865 * SURVSEI(r%) + 0.0002075 * TRADSRATI(r%)
                    If carscomp <= 0.0000001 Then
                        carscomp = 0.0000001
                    End If
                    CarsCompI(R%) = CSng(carscomp)
                    ValueBaseCarsOn(R%) = False

                    If GDPPCIP(R%) < 1.6518 Then
                        truckscomp = 0.00002848 + 0.00004695 * GDPPCIP(R%)
                    ElseIf GDPPCIP(R%) >= 1.6518 And GDPPCIP(R%) < 3.205 Then
                        truckscomp = -0.0004303 + 0.0003246 * GDPPCIP(R%)
                    ElseIf GDPPCIP(R%) >= 3.205 And GDPPCIP(R%) < 25 Then
                        truckscomp = -0.001928 + 0.00079189 * GDPPCIP(R%)
                    Else
                        truckscomp = -0.001928 + 0.00079189 * 25 + 0.0001 * (GDPPCIP(R%) - 25)
                    End If
                    '            ValueBaseTrucks(r%) = -0.000123 * SURVSEI(r%) - 0.001974 * TRADSRATI(r%)
                    If truckscomp <= 0.00000001 Then
                        truckscomp = 0.00000001
                    End If
                    TrucksCompI(R%) = CSng(truckscomp)
                    ValueBaseTrucksOn(R%) = False

                End If
                Dim SumCars As Double = 0
                Dim SumTrucks As Double = 0
                For cc% = 1 To 11
                    carsi(R%, cc%) = CARS(R%, cc%) / AMAX(1, CARSTOT(R%))
                    SumCars = SumCars + carsi(R%, cc%)
                Next cc%
                For cc% = 1 To 11 'normalize
                    carsi(R%, cc%) = CSng(carsi(R%, cc%) / AMAX(0.1, SumCars))
                Next cc%

                For cc% = 1 To 12
                    trucksi(R%, cc%) = TRUCKS(R%, cc%) / AMAX(1, TRUCKSTOT(R%))
                    SumTrucks = SumTrucks + trucksi(R%, cc%)
                Next cc%
                For cc% = 1 To 12 'normalize
                    trucksi(R%, cc%) = CSng(trucksi(R%, cc%) / AMAX(0.1, SumTrucks))
                Next cc%

                'jrs 2009/07/07 CARSFL(R%) = carstoti(R%) * Amin(15, 5 + GDPPCP(R%))
                TRUCKSFL(R%) = Truckstoti(R%) * Amin(15, 5 + GDPPCP(R%))
                'jrs 2009/07/07 CARSFLPC(R%) = CARSFL(R%) / AMAX(0.001, POP(R%)) / 1000
                TRUCKSFLPC(R%) = TRUCKSFL(R%) / AMAX(0.001, POP(R%)) / 1000
                '
                '   First Year: Total Vehicle Fleet per Capita (/1000)
                '   --------------------------------------------------

                'jrs 2007/10/17
                '852 is the saturation level for the US
                'other countries with higher density have a lower saturation level
                'Reduction factor comes from a paper by Dargay, Gately, and Sommer 2007
                If POP(R) / LANDAREA(R) > POP(USidx) / LANDAREA(USidx) Then
                    RdcFc = CSng(38.8 * (POP(R) / LANDAREA(R) - POP(USidx) / LANDAREA(USidx)))
                    'jrs 2010/07/12
                    'set a maximum of 852/2 for the reduction factor
                    'Singapore is going crazy trying to reduce the factor by 2700
                    RdcFc = CSng(Amin(RdcFc, 852 / 2))
                Else
                    RdcFc = 0
                End If
                Dim CalcVehicFlpc As Double = (852 - RdcFc) * Math.Exp(CDbl(-5.987 * Math.Exp(CDbl(-0.2 * CDbl(GDPPCP(R%).ToString())))))
                VehicFlpcRI(R%) = Math.Round(VEHICFLPC(R%) / CalcVehicFlpc, 6)
                'jrs 2009/07/07
                VEHICFLPC(R%) = VEHICFLPC(R%) * vehicflpcm(R%)
                VEHICFLPC(R%) = AMAX(1, Amin(1000, VEHICFLPC(R%)))
                'jrs 2010/07/15
                SVEHICFLPC(R%) = VEHICFLPC(R%)
            Next R%
            'GoTo AllSteps
            Call AllSteps()
        End Sub

        Public Sub ifsAuto1AllbutFirstYear(context As IForecastContext)
            '   **************
            '
            '   ALL TIME STEPS BUT FIRST
            '
            '   **************
            'AllButFirst:
            '
            '   All but First:  Total Car and Truck Calculations
            '   ------------------------------------------------
            Dim Title$ = ""
            Dim carscomp = 0!
            Dim carscomp2 = 0!
            Dim truckscomp = 0!
            Dim truckscomp2 = 0!
            Dim TimeFAc = 0!
            For R% = 1 To NR%
                'Notes on scaling of raw data to compute functions used here:
                'GDPPCP was divided by 1000 to put it in thousand dollars
                'Vehicles(CARSTOT or TRUCKSTOT)/Population was divided by 1000000
                'to put it in millions
                'Vehicles computed based on GDP are multiplied by 1000 to go from
                'billions to millions
                If vehfuncsw = 1 Then 'Vehicles/Capita as function of GDP/Capita

                    'jrs 2025/05/15 Title$ = "GDP/Capita (PPP) Versus Total Cars/Capita"
                    Title$ = TFNameTotCarsPC
                    Call XYTABL(Title$, GDPPCP(R%), carscomp, context)
                    Call XYTABL(Title$, GDPPCIP(R%), carscomp2, context)

                    If carscomp <= 0 Then
                        carscomp = 0
                    End If
                    If carscomp2 <= 0 Then
                        carscomp2 = 0
                    End If

                    CARSTOT(R%) = carstoti(R%) * POP(R%) / popi(R%) +
                        (carscomp - carscomp2) * POP(R%) * 1000000
                    TimeFAc = Amin(IY%, carsconv)
                    CARSTOT(R%) = (CARSTOT(R%) *
                        (carsconv - TimeFAc) + carscomp * TimeFAc) / carsconv
                    CARSTOT(R%) = AMAX(1, CARSTOT(R%) * carstotm(R%))

                    Title$ = "GDP/Capita (PPP) Versus Total Trucks/Capita"
                    Call XYTABL(Title$, GDPPCP(R%), truckscomp, context)
                    Call XYTABL(Title$, GDPPCIP(R%), truckscomp2, context)

                    If truckscomp <= 0 Then
                        truckscomp = 0
                    End If
                    If truckscomp2 <= 0 Then
                        truckscomp2 = 0
                    End If
                    TRUCKSTOT(R%) = Truckstoti(R%) * POP(R%) / popi(R%) +
                        (truckscomp - truckscomp2) * POP(R%) * 1000000

                    TimeFAc = Amin(IY%, trucksconv)
                    TRUCKSTOT(R%) = (TRUCKSTOT(R%) *
                        (trucksconv - TimeFAc) + truckscomp * TimeFAc) / trucksconv
                    TRUCKSTOT(R%) = AMAX(1, TRUCKSTOT(R%) * truckstotm(R%))

                ElseIf vehfuncsw = 4 Then 'Vehicles/Capita as function of GDP/Capita,
                    'TradSecRat, and SurvSelfExp

                    If GDPPCP(R%) < 2.315 Then
                        carscomp = CSng(-0.0000007485 + 0.000044 * GDPPCP(R%))
                    ElseIf GDPPCP(R%) >= 2.315 And GDPPCP(R%) < 4.305 Then
                        carscomp = CSng(-0.0008709 + 0.0004199 * GDPPCP(R%))
                    ElseIf GDPPCP(R%) >= 4.305 And GDPPCP(R%) < 25 Then
                        carscomp = CSng(-0.006807 + 0.001799 * GDPPCP(R%))
                    Else
                        carscomp = CSng(-0.006807 + 0.001799 * 25 + 0.0001 * (GDPPCP(R%) - 25))
                    End If

                    If ValueBaseCarsOn(R%) = False And GDPPCP(R%) >= 4.305 Then
                        ValueBaseCars(R%) = CSng(-0.003865 * SURVSE(R%, WVSCohorts) +
                            0.0002075 * TRADSRAT(R%, WVSCohorts))
                        ValueBaseCarsOn(R%) = True
                    End If
                    Dim ValueFac = -0.003865 * SURVSE(R%, WVSCohorts) +
                        0.0002075 * TRADSRAT(R%, WVSCohorts)

                    If GDPPCP(R%) >= 4.305 Then
                        carscomp = CSng(carscomp + ValueFac - ValueBaseCars(R%))
                    End If

                    If carscomp <= 0.0000001 Then
                        carscomp = 0.0000001
                    End If

                    CARSTOT(R%) = carstoti(R%) * POP(R%) / popi(R%) *
                        (carscomp / CarsCompI(R%))

                    '            TimeFac = Amin(IY%, carsconv)
                    '            CARSTOT(r%) = (CARSTOT(r%) * _
                    '                (carsconv - TimeFac) + carscomp * TimeFac) / carsconv
                    CARSTOT(R%) = AMAX(1, CARSTOT(R%) * carstotm(R%))

                    If GDPPCP(R%) < 1.6518 Then
                        truckscomp = CSng(0.00002848 + 0.00004695 * GDPPCP(R%))
                    ElseIf GDPPCP(R%) >= 1.6518 And GDPPCP(R%) < 3.205 Then
                        truckscomp = CSng(-0.0004303 + 0.0003246 * GDPPCP(R%))
                    ElseIf GDPPCP(R%) >= 3.205 And GDPPCP(R%) < 25 Then
                        truckscomp = CSng(-0.001928 + 0.00079189 * GDPPCP(R%))
                    Else
                        truckscomp = CSng(-0.001928 + 0.00079189 * 25 + 0.0001 * (GDPPCIP(R%) - 25))
                    End If

                    If ValueBaseTrucksOn(R%) = False And GDPPCP(R%) >= 3.205 Then
                        ValueBaseTrucks(R%) = CSng(-0.000123 * SURVSE(R%, WVSCohorts) -
                            0.001974 * TRADSRAT(R%, WVSCohorts))
                        ValueBaseTrucksOn(R%) = True
                    End If
                    ValueFac = -0.000123 * SURVSE(R%, WVSCohorts) -
                        0.001974 * TRADSRAT(R%, WVSCohorts)
                    If GDPPCP(R%) >= 3.205 Then
                        truckscomp = CSng(truckscomp + ValueFac - ValueBaseTrucks(R%))
                    End If

                    If truckscomp <= 0.00000001 Then
                        truckscomp = 0.00000001
                    End If

                    'jrs 2020/02/19 rounding to match vb6
                    Dim L As Integer
                    L = CStr(CInt(Truckstoti(R%))).Length
                    L = AMAX(7 - L, 0)

                    TRUCKSTOT(R%) = CSng(Math.Round(Truckstoti(R%) * POP(R%) / popi(R%) *
                        (truckscomp / TrucksCompI(R%)), L))

                    '            TimeFac = Amin(IY%, trucksconv)
                    '            TRUCKSTOT(r%) = (TRUCKSTOT(r%) * _
                    '                (trucksconv - TimeFac) + TrucksComp * TimeFac) / trucksconv
                    TRUCKSTOT(R%) = AMAX(1, TRUCKSTOT(R%) * truckstotm(R%))

                ElseIf vehfuncsw = 2 Then 'Vehicles as a function of GDP

                    Title$ = "GDP Versus Total Car Sales"
                    Call XYTABL(Title$, GDP(R%), carscomp, context)
                    'ttt GDPI should be Public at Economy
                    Call XYTABL(Title$, GDPI(R%), carscomp2, context)

                    If carscomp <= 0 Then
                        carscomp = 0
                    End If
                    If carscomp2 <= 0 Then
                        carscomp2 = 0
                    End If
                    carscomp = carscomp * 1000
                    carscomp2 = carscomp2 * 1000

                    CARSTOT(R%) = carstoti(R%) +
                        (carscomp - carscomp2)
                    TimeFAc = Amin(IY%, carsconv)
                    CARSTOT(R%) = (CARSTOT(R%) *
                        (carsconv - TimeFAc) + carscomp * TimeFAc) / carsconv
                    CARSTOT(R%) = AMAX(1, CARSTOT(R%) * carstotm(R%))

                    Title$ = "GDP Versus Total Truck Sales"
                    Call XYTABL(Title$, GDP(R%), truckscomp, context)
                    Call XYTABL(Title$, GDPI(R%), truckscomp2, context)

                    If truckscomp <= 0 Then
                        truckscomp = 0
                    End If
                    If truckscomp2 <= 0 Then
                        truckscomp2 = 0
                    End If
                    truckscomp = truckscomp * 1000
                    truckscomp2 = truckscomp2 * 1000

                    TRUCKSTOT(R%) = Truckstoti(R%) +
                        (truckscomp - truckscomp2)
                    TimeFAc = Amin(IY%, trucksconv)
                    TRUCKSTOT(R%) = (TRUCKSTOT(R%) *
                        (trucksconv - TimeFAc) + truckscomp * TimeFAc) / trucksconv
                    TRUCKSTOT(R%) = AMAX(1, TRUCKSTOT(R%) * truckstotm(R%))

                ElseIf vehfuncsw = 3 Then 'Vehicles as function of GDPPC and TradSRat

                    carscomp = CSng(0.095 + 0.3735 * Math.Log(GDPPCP(R%)) - 0.3388 * TRADSRAT(R%, WVSCohorts))
                    carscomp2 = CSng(0.095 + 0.3735 * Math.Log(GDPPCIP(R%)) - 0.3388 * TRADSRATI(R%))
                    carscomp = CSng(carscomp * GDP(R%) * 1000)
                    carscomp2 = carscomp2 * GDPI(R%) * 1000

                    If carscomp <= 0 Then
                        carscomp = 0
                    End If
                    If carscomp2 <= 0 Then
                        carscomp2 = 0
                    End If

                    CARSTOT(R%) = carstoti(R%) +
                        (carscomp - carscomp2)
                    TimeFAc = Amin(IY%, carsconv)
                    CARSTOT(R%) = (CARSTOT(R%) *
                        (carsconv - TimeFAc) + carscomp * TimeFAc) / carsconv
                    CARSTOT(R%) = AMAX(1, CARSTOT(R%) * carstotm(R%))

                    truckscomp = CSng(0.095 + 0.0075886 * GDPPCP(R%) - 0.25333 * TRADSRAT(R%, WVSCohorts))
                    truckscomp2 = CSng(0.095 + 0.0075886 * GDPPCIP(R%) - 0.25333 * TRADSRATI(R%))
                    truckscomp = truckscomp * GDP(R%) * 1000
                    truckscomp2 = truckscomp2 * GDPI(R%) * 1000

                    If truckscomp <= 0 Then
                        truckscomp = 0
                    End If
                    If truckscomp2 <= 0 Then
                        truckscomp2 = 0
                    End If

                    TRUCKSTOT(R%) = Truckstoti(R%) +
                        (truckscomp - truckscomp2)
                    TimeFAc = Amin(IY%, trucksconv)
                    TRUCKSTOT(R%) = (TRUCKSTOT(R%) *
                        (trucksconv - TimeFAc) + truckscomp * TimeFAc) / trucksconv
                    TRUCKSTOT(R%) = AMAX(1, TRUCKSTOT(R%) * truckstotm(R%))

                End If
            Next R%
            '   All but First:  Types of Cars and Trucks
            '   ----------------------------------------
            Dim CarsTotCalc = 0!
            Dim TrucksTotCalc = 0!
            For R% = 1 To NR%
                CarsTotCalc = 0
                For cc% = 1 To 11 'Could use MaxVal(11) from IFs.grp
                    '            If cc% = 11 Then Stop
                    Title$ = "CarsTot Versus Cars" + GRPS$(11, cc%)
                    Call XYTABL(Title$, CARSTOT(R%) / AMAX(0.001, POP(R%)) / 1000000, carscomp, context)
                    Call XYTABL(Title$, CSng(carstotpci(R%)), carscomp2, context)
                    If carscomp < 0.9 * carscomp2 Then
                        carscomp = CSng(0.9 * carscomp2)
                    End If

                    If carscomp <= 0 Or carscomp2 <= 0 Then
                        carscomp = 1
                        carscomp2 = 1
                    End If
                    Dim carsmul = carscomp / AMAX(0.000001, carscomp2)
                    TimeFAc = Amin(IY%, carsconv)
                    Dim carsbase = (carsi(R%, cc%) *
               (carsconv - TimeFAc) + carscomp * TimeFAc) / AMAX(1, carsconv)
                    CARS(R%, cc%) = AMAX(0, carsbase * carsmul) * CARSTOT(R%) * carsm(R%, cc%)
                    CarsTotCalc = CarsTotCalc + CARS(R%, cc%)
                Next cc%
                For cc% = 1 To 11
                    CARS(R%, cc%) = CARS(R%, cc%) * CARSTOT(R%) / AMAX(1, CarsTotCalc)
                Next cc%

                TrucksTotCalc = 0
                For cc% = 1 To 12 'Could use MaxVal(12) from IFs.grp
                    Title$ = "TrucksTot Versus Trucks" + GRPS$(12, cc%)
                    Call XYTABL(Title$, TRUCKSTOT(R%) / AMAX(0.001, POP(R%)) / 1000000, truckscomp, context)
                    Call XYTABL(Title$, CSng(truckstotpci(R%)), truckscomp2, context)
                    If truckscomp < 0.9 * truckscomp2 Then
                        truckscomp = CSng(0.9 * truckscomp2)
                    End If
                    If truckscomp <= 0 Or truckscomp2 <= 0 Then
                        truckscomp = 1
                        truckscomp2 = 1
                    End If
                    Dim TrucksMul = truckscomp / AMAX(0.000001, truckscomp2)
                    TimeFAc = Amin(IY%, trucksconv)
                    Dim trucksbase = (trucksi(R%, cc%) *
               (trucksconv - TimeFAc) + truckscomp * TimeFAc) / AMAX(1, trucksconv)
                    TRUCKS(R%, cc%) = AMAX(0, trucksbase * TrucksMul) * TRUCKSTOT(R%) * trucksm(R%, cc%)
                    TrucksTotCalc = TrucksTotCalc + TRUCKS(R%, cc%)
                Next cc%
                For cc% = 1 To 12
                    TRUCKS(R%, cc%) = TRUCKS(R%, cc%) * TRUCKSTOT(R%) / AMAX(1, TrucksTotCalc)
                Next cc%
                '
                '   All but First: Total Vehicle Fleet per Capita (/1000)
                '   -----------------------------------------------------

                'jrs 2007/10/17
                '852 is the saturation level for the US
                'other countries with higher density have a lower saturation level
                'Reduction factor comes from a paper by Dargay, Gately, and Sommer 2007
                Dim RdcFc = 0!
                If POP(R) / LANDAREA(R) > POP(USidx) / LANDAREA(USidx) Then
                    RdcFc = CSng(38.8 * (POP(R) / LANDAREA(R) - POP(USidx) / LANDAREA(USidx)))
                    'jrs 2010/07/12
                    'set a maximum of 852/2 for the reduction factor
                    'Singapore is going crazy trying to reduce the factor by 2700
                    RdcFc = CSng(Amin(RdcFc, 852 / 2))
                Else
                    RdcFc = 0
                End If
                Dim CalcVehicFlpc = (852 - RdcFc) * Math.Exp(-5.987 * Math.Exp(-0.2 * GDPPCP(R%)))
                VEHICFLPC(R%) = CSng(VehicFlpcRI(R%) * CalcVehicFlpc)
                'jrs 2010/07/16
                '        If VehicFlpcRI(R%) > 1 Then
                '            VEHICFLPC(R%) = ConvergeOverTime(VEHICFLPC(R%), CalcVehicFlpc, 50)
                '        Else
                '            VEHICFLPC(R%) = ConvergeOverTime(VEHICFLPC(R%), CalcVehicFlpc, 100)
                '        End If
                If VehicFlpcRI(R%) <= 1 Then
                    VEHICFLPC(R%) = ConvergeOverTime(VEHICFLPC(R%), CSng(CalcVehicFlpc), 100.0!)
                End If
                'jrs 2009/07/07
                VEHICFLPC(R%) = VEHICFLPC(R%) * vehicflpcm(R%)
                'Base, Target, Years
                'jrs 2010/07/15 VEHICFLPC(R%) = AMAX(1, Amin(1000, VEHICFLPC(R%)))
                'VEHICFLPC(R%) = AMAX(1, Amin(SVEHICFLPC(R%) + 12, VEHICFLPC(R%)))
                'jrs 2010/07/16
                If VehicFlpcRI(R%) > 1 Then
                    RdcFc = 0
                End If

                If SVEHICFLPC(R%) < 300 - RdcFc Then
                    GrowthLimit = 3 + (20 - 3) * (300 - RdcFc - SVEHICFLPC(R%)) / (300 - RdcFc - 1)
                ElseIf SVEHICFLPC(R%) < 600 - RdcFc Then
                    GrowthLimit = 1 + (3 - 1) * (600 - RdcFc - SVEHICFLPC(R%)) / (600 - 300)
                Else
                    GrowthLimit = (852 - RdcFc - SVEHICFLPC(R%)) / (852 - 600)
                End If
                GrowthLimit = GrowthLimit / 100 + 1
                VEHICFLPC(R%) = AMAX(1, Amin(SVEHICFLPC(R%) * GrowthLimit, VEHICFLPC(R%)))

                SVEHICFLPC(R%) = VEHICFLPC(R%)
            Next R%

            AllSteps()
        End Sub

        Private Sub AllSteps()
            '   **********************
            '
            '   ALL TIME STEPS
            '
            '   ***********************
            '
            'AllSteps:

            'jrs 2007/10/17
            Dim TechImpr As Single

            '
            '   Carry fleet forward; calculate annual sales
            '   -------------------------------------------

            For R% = 1 To NR%
                CARSALPC(R%) = CARSTOT(R%) / AMAX(0.001, POP(R%) * 1000000)
                TRUCKSALPC(R%) = TRUCKSTOT(R%) / AMAX(0.001, POP(R%) * 1000000)
                'jrs 2009/07/07 SCARSFL(R%) = CARSFL(R%)
                STRUCKSFL(R%) = TRUCKSFL(R%)

                'jrs 2007/10/18, in units
                VEHICLESTOT(R%) = VEHICFLPC(R%) * POP(R%) * 1000

                'jrs 2007/10/08
                'Smeed's Law
                '===========
                'D = .0003(np^2)^(1/3)

                'D is annual road deaths
                'n is number of registered vehicles
                'p is population
                '-----------

                'jrs 2009/09/23
                Dim DeathPV As Single
                If VEHICFLPC(R%) > 0 Then
                    'jrs 2007/10/17
                    'Technological Improvement of 1% per year
                    TechImpr = CSng((0.99) ^ (IY - 1))
                    'This is Deaths per 1,000 vehicles, so D/n*1,000, 1E+15 = 1k * 1M^2
                    DeathPV = CSng((0.0003 * (VEHICFLPC(R%) * 1.0E+15 * POP(R%) ^ 3) ^ (1 / 3)) / (VEHICFLPC(R%) * POP(R%)))

                    'jrs 2007/10/17
                    'Add Technological Improvement Factor
                    DeathPV = DeathPV * TechImpr

                End If
                'pop is multiplied by 1000 because it's in millions

                'JRS 2009/09/25
                Dim LowLimit As Single
                'jrs 2011/11/01
                Dim EffectSE As Single, SE As Single
                SE = 9.49
                'jrs 2009/09/23
                'ttt why if IY
                If IY = 1 Then
                    If DEATHTRPV(R%) = 0 Then
                        DEATHTRPV(R%) = DeathPV
                        DeathPV = 0
                    End If
                    If DeathPV <> 0 Then
                        'DEATHTRPVShift(R%) = DEATHTRPV(R%) - DeathPV
                        DEATHTRPVShift(R%) = DEATHTRPV(R%) / DeathPV
                    Else
                        DEATHTRPVShift(R%) = 1
                    End If
                    'JRS 2009/09/25
                    DEATHTRPVI(R%) = DEATHTRPV(R%)
                Else
                    'jrs 2009/09/25
                    '            If DEATHTRPVI(R%) < 0.1 Then
                    '                LowLimit = DEATHTRPVI(R%)
                    '            Else
                    '                LowLimit = 0.1
                    '            End If
                    'jrs 2009/09/28
                    LowLimit = 0
                    'jrs 2011/11/01
                    If deathtrpvsetar(R%) <> 0 Then
                        EffectSE = StdErrorMultShift(SE, CSng(DeathPV), CSng(DEATHTRPVShift(R%)), CSng(DEATHTRPVShift(R%)),
                        CSng(deathtrpvsetar(R%)), CInt(FirstYear + deathtrpvseyrtar(R%)), FirstYear + IY - 1, CSng(DEATHTRPVI(R%)), FirstYear)
                    Else
                        EffectSE = 1
                    End If

                    'jrs 2009/07/07
                    'Minimum is set to half of the minimum of the lowest country
                    'Slovak Replublic at the time 2009/09/23
                    'DEATHTRPV(R%) = Amin(AMAX((DeathPV + DEATHTRPVShift(R%)) * deathtrpvm(R%), LowLimit), 1000)
                    'jrs 2011/11/01 DEATHTRPV(R%) = Amin(AMAX((DeathPV * DEATHTRPVShift(R%)) * deathtrpvm(R%), LowLimit), 1000)
                    'DEATHTRPV(R%) = Amin(AMAX((DeathPV * DEATHTRPVShift(R%)) * deathtrpvm(R%), 0.045 / 2), 1000)
                    'DEATHTRPV(R%) = Amin(AMAX((DeathPV + DEATHTRPVShift(R%)) * deathtrpvm(R%), 0), 1000)
                    DEATHTRPV(R%) = Amin(AMAX((DeathPV * DEATHTRPVShift(R%)) * deathtrpvm(R%) * EffectSE, LowLimit), 1000)
                End If
            Next
        End Sub
    End Module

End Namespace
