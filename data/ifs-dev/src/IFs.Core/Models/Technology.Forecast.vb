Imports IFs.Data

Namespace Models

    Partial Public Module Technology
        '
        '------------------------------------------InfraStr.Bas-------------------------------------
        '-------------------------------------------------------------------------------------------------------------------
        'International Futures System (IFs) - General Public License (GPL) Declaration
        '-------------------------------------------------------------------------------------------------------------------
        '
        '
        '    Copyright 1988-2009 Barry B. Hughes
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


        '
        '   Copyright by Barry B. Hughes 1988-2017
        '   Model code available for review and education only
        '   Reproduction or incorporation in other software by permission only
        '

        ' Technology made large populations possible; large populations now make technology indispensable.
        ' Joseph Wood Krutch
        'Created by
        'jrs 2017/03/28
        Dim SAITASK(), IAITASKGR(), IYEAR1(), IYEAR2() As Single

        Public Sub ifsTechFirstYear(context As IForecastContext)
            ReDim SAITASK(6), IAITASKGR(6), IYEAR1(6), IYEAR2(6)
            Dim i As Integer
            For i = 1 To 6
                'SAVE RESULTiNG VALUE
                SAITASK(i) = AITASK(i)
                IAITASKGR(i) = AITASKGR(i)
            Next i
            If AITASK(6) < 5 Then
                AIMACHIQ = 60 * AITASK(6) / 5
            Else
                AIMACHIQ = 60 + 140 * (AITASK(6) - 5) / 5
            End If
            'Apply multiplier
            AIMACHIQ = AIMACHIQ * aimachiqm
        End Sub

        Public Sub ifsTechAllbutFirstYear(context As IForecastContext)
            'All Years but First
            '-------------------
            Dim i As Integer
            For i = 1 To 6
                'Growth must start decreasing once level reaches 5
                If SAITASK(i) < 5 Then
                    AITASKGR(i) = IAITASKGR(i) * aitaskm(i)
                Else
                    AITASKGR(i) = CSng(IAITASKGR(i) * ((10 - SAITASK(i)) / 5) ^ 2 * aitaskm(i))
                End If
                'Apply multiplier
                'AITASK(i) = AITASK(i) * aitaskm(i)
                'Check limits
                AITASK(i) = AMAX(Amin(AITASK(i), 10), 0)
                'Apply Growth
                AITASK(i) = SAITASK(i) * (1 + AITASKGR(i) / 100)
                'SAVE RESULTING VALUE
                SAITASK(i) = AITASK(i)
                'SAITASKGR(i) = AITASKGR(i)
            Next i
            If AITASK(6) < 5 Then
                AIMACHIQ = 60 * AITASK(6) / 5
            Else
                AIMACHIQ = 60 + 140 * (AITASK(6) - 5) / 5
            End If
            'Apply multiplier
            AIMACHIQ = AIMACHIQ * aimachiqm

        End Sub
    End Module

End Namespace
