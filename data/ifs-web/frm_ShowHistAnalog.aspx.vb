Option Strict Off
Option Explicit On
Option Compare Text

Partial Class frm_ShowHistAnalog
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'jrs 2023/08/04 dsHistAnalogFormulas.DataFile = Session("PutFdrvRun") & "ifsVar.mdb"
        Dim dbHistAnaFile As String = Session("PutFdrvRun") & "ifsVar.db"

        If Not Page.IsPostBack Then
            Dim bValid As Boolean
            Dim idx As Integer
            Dim CurVar As String
            Dim sHistAnal As String
            sHistAnal = ""
            bValid = False
            'jrs 2013/03/18
            Dim sDim2 As String, sDim3 As String

            Dim curr_col As Integer
            ''- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            Dim pos1 As Short

            curr_col = Session("VarSelGridCol") + 1

            pos1 = InStr(Session("VARN")(1, curr_col), "[")
            If pos1 > 0 Then
                CurVar = Mid(Session("VARN")(1, curr_col), 1, pos1 - 1)
            Else
                CurVar = Session("VARN")(1, curr_col)
            End If
            sDim2 = Session("GR2N")(curr_col)
            sDim3 = Session("GR3N")(curr_col)

            idx = IfsVars.Item(CurVar).Index
            Dim cc As Integer
            cc = curr_col
            If idx > 0 Then
                bValid = True
                sHistAnal = IIf(IsDBNull(VHistoricAnalog(idx)), "", VHistoricAnalog(idx))
                'jrs 2013/02/18
                If UCase(sHistAnal) = "IFSVARHISTANA" Then
                    Dim dbHA As String
                    dbHA = Session("PutFdrvRun") & "ifsVar.mdb"
                    Dim rsHA As DataTable
                    SetDataTablefromAccess(dbHA, "SELECT [Historic Analog] FROM IFSVARHISTANA WHERE VarName = """ & CurVar &
                    """ AND (Dim2 is Null or Dim2 = """ & sDim2 & """) AND (Dim3 is Null or Dim3 = """ & sDim3 & """)", "", rsHA)
                    If rsHA.Rows.Count > 0 Then
                        sHistAnal = rsHA.Rows(0).Item(0).ToString
                    Else
                        sHistAnal = ""
                    End If
                ElseIf InStr(sHistAnal, ",") > 0 Then
                    Dim k As Integer
                    k = gGetDimIdx(VDim2(idx), sDim2, False, Me)
                    Dim x As Object
                    x = Split(sHistAnal, ",")
                    If k > 0 Then
                        If k <= UBound(x) Then
                            sHistAnal = x(k - 1)
                        End If
                    End If
                End If
            Else 'check if it's a formula
                If UBound(Session("aFormulas")) >= cc Then
                    If Not Session("aFormulas")(cc) Is Nothing Then
                        If Session("aFormulas")(cc).Name = Session("VARN")(1, cc) Then
                            bValid = True
                            sHistAnal = "Formula"
                        End If
                    End If
                End If
            End If



            If bValid Then
                'Load(frm_ShowHistAnalog)
                If UCase(sHistAnal) <> "FORMULA" Then
                    If sHistAnal > "" Then
                        lblShowHistAnalog.Text = sHistAnal
                    Else
                        lblShowHistAnalog.Text = "No analog option for this variable"
                        Exit Sub
                    End If
                Else
                    Dim rr As Integer

                    'frm_ShowHistAnalog.lblHistoricAnalog.Caption = CStr(cc)
                    'frm_ShowHistAnalog.lblHistoricAnalog.Visible = False
                    lblShowHistAnalog.Text = ""
                    If idx > 0 Then
                        grdHistAnalogFormulas.Visible = True

                        Dim sql As String, sqlDet As String
                        sql = "SELECT 0&Dim2&Dim3 as ListOrder, Dim2 as CodeVar, '' as VarName, Formula FROM IFSVARHISTANAFORMULAS WHERE NAME = """ & CurVar & """"
                        sqlDet = "SELECT 0&F.Dim2&F.Dim3&CodeVar as ListOrder, CodeVar, VarName, '' as Formula FROM IFSVARHISTANAFORMULAS F, IFSVARHISTANAFORMULASDET D WHERE D.ForVarName = F.Name AND (F.Dim2 = D.Dim2 OR F.Dim2 is Null) AND (F.Dim3 = D.Dim3 OR F.Dim3 is Null) AND F.Name = """ & CurVar & """"
                        'jrs 2023/08/04 dsHistAnalogFormulas.SelectCommand = sql & " UNION " & sqlDet
                        Dim dt As DataTable
                        SetDataTablefromSQLite(dbHistAnaFile, sql & " UNION " & sqlDet, "", dt)
                        grdHistAnalogFormulas.DataSource = dt
                        grdHistAnalogFormulas.DataBind()
                        'grdHistAnalogFormulas.SelectedIndex = 1
                        'jrs 2025/06/16
                        If dt.Rows.Count > 0 Then
                            grdHistAnalogFormulas.SelectedIndex = 0
                        End If

                    Else
                        grdFormulas.Visible = True

                        Dim dt As DataTable = New DataTable()
                        dt.Columns.Add("Code", Type.GetType("System.String"))
                        dt.Columns.Add("VarName", Type.GetType("System.String"))
                        dt.Columns.Add("DEFINITION", Type.GetType("System.String"))
                        dt.Columns.Add("Historic_Analog", Type.GetType("System.String"))
                        dt.Columns.Add("DIM2", Type.GetType("System.String"))
                        dt.Columns.Add("DIM3", Type.GetType("System.String"))


                        Dim iPos As Integer, sVar As String, sDef As String, sHistAn As String
                        'frm_ShowHistAnalog.RedimFormulas(aFormulas(cc).disVar.disSize)
                        For rr = 1 To Session("aFormulas")(cc).disVar.disSize
                            iPos = InStr(Session("aFormulas")(cc).disVar.displayDet(rr), "[")
                            If iPos > 0 Then
                                sVar = Mid(Session("aFormulas")(cc).disVar.displayDet(rr), 1, iPos - 1)
                            Else
                                sVar = Session("aFormulas")(cc).disVar.displayDet(rr)
                            End If
                            idx = IfsVars.Item(sVar).Index
                            If idx > 0 Then
                                sDef = VDef(idx)
                                sHistAn = IIf(IsDBNull(VHistoricAnalog(idx)), "", VHistoricAnalog(idx))
                            Else
                                sDef = ""
                                sHistAn = ""
                            End If

                            dt.Rows.Add()
                            dt.Rows(dt.Rows.Count - 1)("Code") = Session("aFormulas")(cc).disVar.display(rr)
                            dt.Rows(dt.Rows.Count - 1)("VarName") = sVar
                            dt.Rows(dt.Rows.Count - 1)("DEFINITION") = sDef
                            dt.Rows(dt.Rows.Count - 1)("DIM2") = Session("GR2N")(cc)
                            dt.Rows(dt.Rows.Count - 1)("DIM3") = Session("GR3N")(cc)
                            dt.Rows(dt.Rows.Count - 1)("Historic_Analog") = sHistAn
                        Next rr

                        grdFormulas.DataSource = dt
                        grdFormulas.DataBind()
                        grdFormulas.SelectedIndex = 0
                        grdFormulas_SelectedIndexChanged(Me, New EventArgs)

                    End If
                End If

            Else
                lblShowHistAnalog.Text = "Sorry, this option is not available."
            End If

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cswmWebMenu.UserData = "jsolorza::976739975"

        cswmWebMenu.ClearPixelImage = "blank.gif"
        cswmWebMenu.PopupIcon = "./images/Popup.gif"
        cswmWebMenu.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        cswmWebMenu.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        cswmWebMenu.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        cswmWebMenu.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        cswmWebMenu.SelectedPopupIcon = "./images/SelectedPopup.gif"
        cswmWebMenu.ScrollEnabled = True
        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"
        cswmGroup1.ID = "Group_fa05"
        'jrs 2011/05/04
        'jrs 2006/11/15 cswmGroup1.DownLevelURL = Session("DisplayParent")
        cswmGroup1.OnMenuBarClick = "location.href='frm_VarSel.aspx';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Data Information"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Show"

    End Sub

    Protected Sub grdFormulas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFormulas.SelectedIndexChanged
        Dim row As GridViewRow = grdFormulas.SelectedRow
        'jrs 2011/07/11
        'For Variables inside Formulas with Formulas in Historic Analog
        If UCase(row.Cells(4).Text) = "FORMULA" Then
            Dim sql As String, sqlHAF As String, sqlHAFDet As String
            sqlHAF = "SELECT 0&Dim2&Dim3 as ListOrder, Dim2 as CodeVar, Dim3 as VarName, Formula FROM IFSVARHISTANAFORMULAS WHERE NAME = """
            sqlHAFDet = "SELECT 0&F.Dim2&F.Dim3&CodeVar as ListOrder, CodeVar, VarName, '' as Formula FROM IFSVARHISTANAFORMULAS F, IFSVARHISTANAFORMULASDET D WHERE D.ForVarName = F.Name AND (F.Dim2 = D.Dim2 OR F.Dim2 is Null) AND (F.Dim3 = D.Dim3 OR F.Dim3 is Null) AND F.Name = """

            Dim svar As String, pos As Integer
            svar = row.Cells(2).Text
            pos = InStr(svar, "[")
            If pos > 0 Then
                svar = Left(svar, pos - 1)
            End If
            sql = sqlHAF & svar & """"
            sql = sql & " UNION " & sqlHAFDet & svar & """"

            'jrs 2023/08/04 dsHistAnalogFormulas.SelectCommand = sql
            Dim dt As DataTable
            SetDataTablefromSQLite(Session("PutFdrvRun") & "ifsVar.db", sql, "", dt)
            grdHistAnalogFormulas.DataSource = dt

            grdHistAnalogFormulas.DataBind()
            grdHistAnalogFormulas.SelectedIndex = 1

            grdHistAnalogFormulas.Visible = True
        Else
            grdHistAnalogFormulas.Visible = False
        End If

    End Sub

    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Show"
                mnuDataInf_Click()
        End Select
    End Sub

    Private Sub mnuDataInf_Click()
        Dim row As GridViewRow
        Dim idx As Integer
        Session("AvailabilityParent") = "frm_ShowHistAnalog.aspx"
        Dim db As String, Valtbl As DataTable
        db = Session("PutFdrvRun") & "datadict.MDB"
        SetDataTablefromAccess(db, "Select * from DataDict order by Variable", "", Valtbl)
        Session("DataDictVars") = Valtbl

        If grdFormulas.Visible = False And grdHistAnalogFormulas.Visible = False Then
            'jrs 2011/07/14 Historic Analog
            If lblShowHistAnalog.Text > "" Then
                Dim sVar As String, pos As Integer
                pos = InStr(lblShowHistAnalog.Text, ",")
                If pos > 0 Then
                    sVar = Left(lblShowHistAnalog.Text, pos - 1)
                Else
                    sVar = lblShowHistAnalog.Text
                End If
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & sVar)
            End If
        Else
            'jrs 2025/06/16 added and also if index > -1
            If grdHistAnalogFormulas.Visible AndAlso grdHistAnalogFormulas.SelectedIndex > -1 Then
                row = grdHistAnalogFormulas.SelectedRow
                If row.Cells(3).Text = "" Then
                    idx = grdHistAnalogFormulas.SelectedIndex
                    grdHistAnalogFormulas.SelectedIndex = idx + 1
                    row = grdHistAnalogFormulas.SelectedRow
                End If
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & row.Cells(3).Text)

            ElseIf grdFormulas.Visible Then
                row = grdFormulas.SelectedRow
                If row.Cells(4).Text > "" Then
                    Support.FormManager.StateStore(Me)
                    Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & row.Cells(4).Text)
                End If
            End If
        End If
    End Sub

End Class
