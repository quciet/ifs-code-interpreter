Option Strict Off
Option Explicit On

Partial Class frm_MotionChart
    Inherits Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'WinFormsToWeb_PageBar.Rows(0).Cells(1).Text = WinFormsToWeb_PageBar.Rows(0).Cells(1).Text & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Session: " & Session("SessionIdx")
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

        'AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        'Dim cswmItem As Coalesys.WebMenu.Item

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Continue"

        cswmGroup1.OnMenuBarClick = "location.href='" & Session("GraphDisplayParent") & "';"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.ID = "mnuMainMenu"
        cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"

        If Not Page.IsPostBack Then
            ClientScript.RegisterStartupScript(Me.[GetType](), "BrowserSize",
            JscriptSaveBrowserSize, True)

            Dim Inc As HtmlGenericControl
            Inc = New HtmlGenericControl("script")
            Inc.Attributes.Add("type", "text/javascript")
            Inc.InnerHtml = CreateMotionChart()
            Page.Header.Controls.Add(Inc)
        End If

    End Sub

    Private Function CreateMotionChart() As String
        'Using Google Visualization API
        CreateMotionChart = "" &
            "google.load('visualization', '1', {packages:['motionchart']});" &
            "google.setOnLoadCallback(drawChart);      " &
            "function drawChart() {" &
                    "var data = new google.visualization.DataTable();        " &
                    LoadData() &
                    "var chart = new google.visualization.MotionChart(document.getElementById('chart_div'));        " &
                    "chart.draw(data, {width:myWidth, height:myHeight});      " &
              "}    "
    End Function

    Private Function LoadData() As String
        'Years to display
        Dim i As Integer
        If Session("DisplayLastYear") = 0 Then
            Session("DisplayLastYear") = Session("LastYear")
            If Session("buseAllHis") And UCase(Session("ActiveBase")) = "IFSBASEHIST.RUN" Then
                For i = 1 To Session("COUNTE")
                    If Session("FinYearHis")(i) > Session("DisplayLastYear") Then
                        Session("DisplayLastYear") = Session("FinYearHis")(i)
                    End If
                Next i
            End If

        End If

        '   Set rows of Chart
        '   ----------------
        Dim MaxYear As Integer, MinYear As Integer
        'make it true when loading historic data from Longitudinal
        'flag to load values before first year history ex. PolityDemoc
        Dim bLoadLongitudinalBFYH As Boolean = False
        Dim iFirstYearBFYH As Integer = 0
        Dim iFirstYear As Integer

        MaxYear = 1
        If bLoadLongitudinalBFYH Then
            MinYear = Session("DisplayLastYear") - iFirstYearBFYH + 1
            iFirstYear = iFirstYearBFYH
        Else
            MinYear = Session("DisplayLastYear") - Session("FirstYear") + 1
            iFirstYear = Session("FirstYear")
        End If

        'jrs 2008/08/26
        'Find out how many regions loaded
        Dim TotRegs As Integer = 0
        Dim TotVars As Integer = 0 '2 or 3
        Dim bContinuous As Boolean = False 'True=Same regions next to each other, as opossed to same variable next to each other
        Dim bKeepCounting As Boolean = True
        If Session("GR1N")(1) = Session("GR1N")(2) Then
            bContinuous = True
            bKeepCounting = False
            If Session("COUNTE") > 2 Then
                If Session("GR1N")(1) = Session("GR1N")(3) Then
                    TotVars = 3
                Else
                    TotVars = 2
                End If
            Else
                TotVars = 2
            End If
            TotRegs = Session("COUNTE") / TotVars
        End If

        Dim ii As Integer
        If IsNumeric(Session("FinYear")(1)) Then
            For ii = 1 To Session("COUNTE")
                If Session("FinYear")(ii) > MaxYear Then
                    If Session("FinYear")(ii) > Session("DisplayLastYear") - Session("FirstYear") + 1 And Not bLoadLongitudinalBFYH Then
                        MaxYear = Session("DisplayLastYear") - Session("FirstYear") + 1
                    ElseIf Session("FinYear")(ii) > Session("DisplayLastYear") - iFirstYearBFYH + 1 And bLoadLongitudinalBFYH Then
                        MaxYear = Session("DisplayLastYear") - iFirstYearBFYH + 1
                    Else
                        MaxYear = Session("FinYear")(ii)
                    End If
                End If
                If Session("FinYear")(ii) < MinYear Then MinYear = Session("FinYear")(ii)
                'jrs 2008/08/26
                If bKeepCounting Then
                    TotRegs = TotRegs + 1
                    If ii < Session("COUNTE") Then
                        If Session("GR1N")(1) = Session("GR1N")(ii + 1) Then
                            bKeepCounting = False
                        End If
                    End If
                End If
            Next ii
        End If

        Dim DisYear As Integer, Resid As Integer, Timepoints As Integer
        If Session("DisplayTime") = 0 Then
            DisYear = MinYear
        Else
            DisYear = MaxYear
        End If


        Resid = (DisYear - 1) Mod Session("Interval")
        Timepoints = 1 + (DisYear - 1) \ Session("Interval") 'Truncating Division
        If Resid > 0 Then Timepoints = Timepoints + 1 'Show Final Year Always

        '    Timepoints% = DisYear% / Session("Interval")% + 1
        If Timepoints < 1 Then Timepoints = 1



        Dim TotRows As Integer
        'use all countries x all years loaded
        'jrs 2008/08/26 TotRows = NR * Timepoints
        TotRows = TotRegs * Timepoints
        LoadData = "data.addRows(" & CStr(TotRows) & ");"

        'Column 0: Country
        'Column 1: Year
        LoadData = LoadData & "data.addColumn('string', 'Country');" &
                   "data.addColumn('number', 'Year');"

        Dim j As Integer
        'Column 2: Independent variable (X)
        'Column 3: Dependent Variable (Y)
        'jrs 2009/12/16 For j = 1 To 2
        For j = 2 To 1 Step -1
            'JRS 2008/08/26 LoadData = LoadData & "data.addColumn('number', '" & Session("VARN")(1, (j - 1) * NR + 1) & "');"
            If Not bContinuous Then
                LoadData = LoadData & "data.addColumn('number', '" & Session("VARN")(1, (j - 1) * TotRegs + 1)
                'jrs 2008/10/21
                If Session("GR2N")((j - 1) * TotRegs + 1) > "" Then
                    LoadData = LoadData & " " & Session("GR2N")((j - 1) * TotRegs + 1)
                End If
            Else
                LoadData = LoadData & "data.addColumn('number', '" & Session("VARN")(1, j)
                'jrs 2008/10/21
                If Session("GR2N")(j) > "" Then
                    LoadData = LoadData & " " & Session("GR2N")(j)
                End If
            End If
            LoadData = LoadData & "');"
        Next
        'Column 5: Group for the Color
        LoadData = LoadData & "data.addColumn('string', 'Group');"
        'Load Groups
        Dim Groups(NR) As String
        'JRS 2008/08/26
        If TotRegs = NR Then
            LoadGroups(Groups)
        Else
            For i = 1 To TotRegs
                If Not bContinuous Then
                    Groups(i) = Session("GR1N")(((i - 1) Mod TotRegs) + 1)
                Else
                    Groups(i) = Session("GR1N")(((i - 1) Mod TotRegs) * TotVars + 1)
                End If
            Next
        End If

        'Column 6: Bubble Size
        Dim bBubble As Boolean
        'JRS 2008/08/26 bBubble = (Session("COUNTE") = 3 * NR)
        If Not bContinuous Then
            TotVars = Session("COUNTE") / TotRegs
        End If
        bBubble = (TotVars = 3)
        If bBubble Then
            'JRS 2008/08/26 LoadData = LoadData & "data.addColumn('number', '" & Session("VARN")(1, 2 * NR + 1) & "');"
            If Not bContinuous Then
                LoadData = LoadData & "data.addColumn('number', '" & Session("VARN")(1, 2 * TotRegs + 1) & "');"
            Else
                LoadData = LoadData & "data.addColumn('number', '" & Session("VARN")(1, 3) & "');"
            End If
        End If

        'Load Data for Columns 0 to 3
        For i = 1 To TotRows
            'JRS 2008/08/26
            If Not bContinuous Then
                'Country
                LoadData = LoadData & Chr(13) & "data.setValue(" & CStr(i - 1) & ", 0, """ &
                Session("GR1N")(((i - 1) Mod TotRegs) + 1) & """);"
                'Year
                LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", 1, " &
                CStr(iFirstYear + ((i - 1) \ TotRegs)) & ");"
                'Data
                'jrs 2009/12/16 For j = 1 To 2
                For j = 2 To 1 Step -1
                    If IsNumeric(Session("PLOT")((i - 1) \ TotRegs + 1, (i - 1) Mod TotRegs + (j - 1) * TotRegs + 1)) Then
                        'jrs 2009/12/16 column position used to be j + 1 (2, 3)
                        LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", " & CStr((j Mod 2) + 2) & ", " &
                        CStr(Session("PLOT")((i - 1) \ TotRegs + 1, (i - 1) Mod TotRegs + (j - 1) * TotRegs + 1)) & ");"
                    End If
                Next
                'Group
                LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", 4, '" &
                Groups(((i - 1) Mod TotRegs) + 1) & "');"
                'Bubble Size
                If bBubble Then
                    If IsNumeric(Session("PLOT")((i - 1) \ TotRegs + 1, (i - 1) Mod TotRegs + 2 * TotRegs + 1)) Then
                        LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", 5, " &
                        CStr(Session("PLOT")((i - 1) \ TotRegs + 1, (i - 1) Mod TotRegs + 2 * TotRegs + 1)) & ");"
                    End If
                End If
            Else 'if Continuous
                'Country
                LoadData = LoadData & Chr(13) & "data.setValue(" & CStr(i - 1) & ", 0, """ &
                Session("GR1N")(((i - 1) Mod TotRegs) * TotVars + 1) & """);"
                'Year
                LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", 1, " &
                CStr(iFirstYear + ((i - 1) \ TotRegs)) & ");"
                'Data
                'jrs 2009/12/16 For j = 1 To 2
                For j = 2 To 1 Step -1
                    If IsNumeric(Session("PLOT")((i - 1) \ TotRegs + 1, ((i - 1) Mod TotRegs) * TotVars + j)) Then
                        'jrs 2009/12/16 column used to be j + 1, now j Mod 2 +2
                        LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", " & CStr((j Mod 2) + 2) & ", " &
                        CStr(Session("PLOT")((i - 1) \ TotRegs + 1, ((i - 1) Mod TotRegs) * TotVars + j)) & ");"
                    End If
                Next
                'Group
                LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", 4, '" &
                Groups((i - 1) Mod TotRegs + 1) & "');"
                'Bubble Size
                If bBubble Then
                    If IsNumeric(Session("PLOT")((i - 1) \ TotRegs + 1, ((i - 1) Mod TotRegs) * TotVars + 3)) Then
                        LoadData = LoadData & "data.setValue(" & CStr(i - 1) & ", 5, " &
                        CStr(Session("PLOT")((i - 1) \ TotRegs + 1, ((i - 1) Mod TotRegs) * TotVars + 3)) & ");"
                    End If
                End If
            End If
        Next
        LoadData = LoadData & Chr(13)

    End Function

    Private Sub LoadGroups(ByRef Groups() As String)
        Dim dt As DataTable
        Dim Sql As String
        Sql = "SELECT GroupIDNewFull.GroupName, GroupingFull.Region " &
              "FROM GListId, GList, GroupIDNewFull, GroupingFull " &
                "WHERE GListId.Code = GList.GListCode And " &
                "GList.Region = GroupIDNewFull.GroupName AND " &
                "GroupIDNewFull.GroupNum = GroupingFull.GroupNo AND " &
                "GListId.Description = 'UN Regions' " &
                "ORDER BY GroupingFull.Region;"
        SetDataTablefromAccess(Session("PutFdrvRun") & "ifs.mdb", Sql, "", dt)
        Dim i As Integer, rr As Integer
        rr = 0
        For i = 1 To NR 'rr = 0 To dt.Rows.Count - 1
            If dt.Rows(rr).Item(1) = GRPS(1, i) Then
                Groups(i) = dt.Rows(rr).Item(0)
                'Advance to the next group
                rr = rr + 1
            End If
        Next
    End Sub
End Class
