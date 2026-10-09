
'Namespace IFsWeb

Public Class DiagramRenderer

        Public Sub Render()


            Init_Drivers()


        End Sub

        Private Sub Init_Drivers()
            '----------------------------------------------------------------------------------------
            Dim vName As String = HttpContext.Current.Session("VName")

            Dim VN_depth = If(String.IsNullOrEmpty(vName), "LIFEXP", vName)

            DrawMain(VN_depth, True, True)

        End Sub

        Private Sub DrawMain(ByVal VName As String, ByVal Causes As Boolean, ByVal Uses As Boolean)
            '----------------------------------------------------------------------------------------
            Dim db1 As String
            Dim valtbl As DataTable
            Dim i%, j%, k%, l%, chk%, Vno%

            'mti 23 jan, 2003
            Dim TStr$, LastVar$
            Dim Savevar As Boolean

            TStr = VName 'valtbl.Fields(0).Value
            LastVar$ = ""


            chk = IfsVars.Item(TStr).Index

            If chk = -1 And UCase(Mid(TStr, 1, 1)) = "S" Then
                chk = IfsVars.Item(Mid(TStr, 2, Len(TStr) - 1)).Index
                Savevar = True
            Else
                Savevar = False
            End If

            If chk <= 0 Then
                Exit Sub
            Else
                Vno = chk
            End If

            i = 0
            j = 0
            k = 0
            l = 0

            Dim rr As Integer = 0

            If HttpContext.Current.Session("bShowDefs") Is Nothing Then
                VarDef = False
            Else
                VarDef = True
            End If

            If CType(HttpContext.Current.Session("DrvSelVars2"), Collection).Contains(V(chk)) Then
                HttpContext.Current.Session("VSelected") = "y"
            Else
                HttpContext.Current.Session("VSelected") = "n"
            End If
            'ttt
            HttpContext.Current.Session("VDef") = VDef$(chk)

            db1 = HttpContext.Current.Session("PutFdrvRun") & "IFsVar.mdb"
            Dim sql As String
            valtbl = New DataTable
            If Causes Then
                NoCauses = False
                'tttAdd DefText, IsSelected
                sql = "select 1, MIN(Distance), [UsesVariable],'' as DefText,'n' as IsSelected from [VarLink], ifsVar where [name] = [UsesVariable] and [variable] = '" &
                VName & "' and submodule = " & CStr(VCat(Vno)) & " GROUP BY [UsesVariable]"
                sql = sql & " UNION select 2, MIN(Distance), [UsesVariable],'' as DefText,'n' as IsSelected from [VarLink], ifsVar where [name] = [UsesVariable] and [variable] = '" &
                VName & "' and submodule <> " & CStr(VCat(Vno)) & " GROUP BY [UsesVariable]"
                SetDataTablefromAccess(db1, sql, "", valtbl)
                For r = 0 To valtbl.Rows.Count - 1
                    chk = 0
                    TStr = valtbl.Rows(r).Item("UsesVariable").ToString
                    chk = IfsVars.Item(TStr).Index
                    If chk = -1 And UCase(Mid(TStr, 1, 1)) = "S" Then
                        chk = IfsVars.Item(Mid(TStr, 2, Len(TStr) - 1)).Index

                    End If
                    If VDef$(chk) Is Nothing Then
                        valtbl.Rows(r).Item("DefText") = TStr
                    Else
                        valtbl.Rows(r).Item("DefText") = If(VDef$(chk) = "", TStr, VDef$(chk))
                    End If
                    If CType(HttpContext.Current.Session("DrvSelVars"), Collection).Contains(TStr) Then
                        valtbl.Rows(r).Item("IsSelected") = "y"
                    Else
                        valtbl.Rows(r).Item("IsSelected") = "n"
                    End If
                    'Debug.WriteLine(TStr & "=" & valtbl.Rows(r).Item("DefText").ToString)
                Next
                HttpContext.Current.Session("DataForDGraphCause") = valtbl

            End If 'causes
            valtbl = New DataTable
            rr = 0
            If Uses Then
                NoUses = False
                sql = "select 1, MIN(Distance), [Variable],'' as DefText,'n' as IsSelected from [VarLink], ifsvar where [name] = [Variable] and [Usesvariable] = '" &
                VName & "' and submodule = " & VCat(Vno) & " GROUP BY [Variable]"
                sql = sql & " UNION select 2, MIN(Distance), [Variable],'' as DefText,'n' as IsSelected from [VarLink], ifsvar where [name] = [Variable] and [Usesvariable] = '" &
                VName & "' and submodule <> " & VCat(Vno) & " GROUP BY [Variable]"
                SetDataTablefromAccess(db1, sql, "", valtbl)
                For r = 0 To valtbl.Rows.Count - 1
                    chk = 0
                    TStr = valtbl.Rows(r).Item("Variable").ToString
                    chk = IfsVars.Item(TStr).Index
                    If chk = -1 And UCase(Mid(TStr, 1, 1)) = "S" Then
                        chk = IfsVars.Item(Mid(TStr, 2, Len(TStr) - 1)).Index

                    End If
                    If VDef$(chk) Is Nothing Then
                        valtbl.Rows(r).Item("DefText") = TStr
                    Else
                        valtbl.Rows(r).Item("DefText") = If(VDef$(chk) = "", TStr, VDef$(chk))
                    End If
                    If CType(HttpContext.Current.Session("DrvSelVars2"), Collection).Contains(TStr) Then
                        valtbl.Rows(r).Item("IsSelected") = "y"
                    Else
                        valtbl.Rows(r).Item("IsSelected") = "n"
                    End If

                Next
                HttpContext.Current.Session("DataForDGraphUse") = valtbl


                'NoUses = True

                'End Uses----------------------



                'Mainvar.top not needed?


            End If 'uses

        End Sub





#Region "Fields"

        Dim NoCauses As Boolean
        Dim NoUses As Boolean
        Dim VarDef As Boolean



#End Region



    End Class
'End Namespace
