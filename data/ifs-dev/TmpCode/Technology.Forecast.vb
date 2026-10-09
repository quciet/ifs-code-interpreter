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
