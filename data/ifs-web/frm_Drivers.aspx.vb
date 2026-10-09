'jam 2022/06/01
Imports Microsoft.AspNet.FriendlyUrls

Partial Public Class frm_Drivers
    Inherits Page

    Dim NoCauses As Boolean
    Dim NoUses As Boolean
    Dim VarDef As Boolean



    'Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    '    If Not Page.IsPostBack Then
    '        If Support.FormManager.LoadState Then
    '            Support.FormManager.StateRestore(Me)
    '            Support.FormManager.LoadState = False
    '        End If
    '    End If
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'jam 2022/06/01	
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String
        Dim othVName As String
        'Dim qtyParam As Integer
        Dim strParam(10) As String
        'Dim seqParam(8) As Integer




        cswmWebMenu.UserData = "jsolorza::976739975"
        cswmWebMenu.MenuBar.ShowOnMouseOver = True

        'jrs 2012/01/17
        cswmWebMenu.AccessorKeyCode = 27
        'jrs 2012/01/24
        cswmWebMenu.ButtonFont.Size = "12px"
        cswmWebMenu.ItemFont.Size = "12px"

        Dim cswmGroup1 As Coalesys.WebMenu.Group
        Dim cswmItem As Coalesys.WebMenu.Item

        If Not (Session("DriverParent") Is Nothing) Then
            cswmGroup1 = cswmWebMenu.Groups.Add()
            cswmGroup1.Caption = "Continue"
            'jam 2022/06/01
            cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl(Session("DriverParent")) & "';"
        End If

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Main Menu"
        cswmGroup1.ID = "mnuMainMenu"
        'jam 2022/06/01
        'cswmGroup1.OnMenuBarClick = "location.href='frm_MainMenu.aspx';"
        cswmGroup1.OnMenuBarClick = "location.href='" & Page.ResolveUrl("frm_MainMenu.aspx") & "';"



        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Variables"
        cswmGroup1.ID = "mnuVariables"

        cswmItem = cswmGroup1.Items.Add()
        If Session("bShowDefs") Is Nothing Then
            cswmItem.Caption = "Show Definitions"
        Else
            cswmItem.Caption = "Hide Definitions"
        End If
        cswmItem.ID = "mnuShowDefs"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Load Selected for Analysis"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Delete Selected from Display"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select All Drivers"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select All Driven"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Select All"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Unselect All"

        cswmGroup1 = cswmWebMenu.Groups.Add()
        cswmGroup1.Caption = "Zoom"
        cswmGroup1.ID = "mnuZoom"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Zoom +"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Zoom -"


        'jam 2022/06/01
        If Not IsNothing(Request.QueryString("VName")) Then

            '0-> othVName


            'jam 2022/06/01
            ReDim Session("seqParam")(10)
            Session("qtyParam") = 0
            ReDim Session("RetParam")(10)


            If Not IsNothing(Request.QueryString("VName")) And Not Page.IsPostBack Then

                othVName = Request.QueryString("VName")


                strParam(Session("qtyParam")) = othVName

                Session("seqParam")(Session("qtyParam")) = 0


                Session("qtyParam") = Session("qtyPAram") + 1
            Else
                othVName = Nothing

            End If




            Vpath = Request.GetFriendlyUrlFileVirtualPath

            Vext = Request.GetFriendlyUrlFileExtension

            vfriendlyurl = FriendlyUrl.Resolve("~/frm_Drivers.aspx")

            'JAM 2022/01/06 Managing "/" exception in URL parameters
            For i = 0 To Session("qtyParam") - 1
                strParam(i) = strParam(i).Replace("/", "™") 'jam 2021-01-30 using TradeMark character like pivot
            Next i


            'jam 2022/06/01
            If othVName.Length > 200 Then

                vurl = FriendlyUrl.Href("~/frm_Drivers")



            Else
                vurl = FriendlyUrl.Href("~/frm_Drivers", DriversprepareForFriendlyURL(othVName))
            End If



            Response.Redirect(vurl)




        End If



        'jam 2022/06/01

        Dim segments As IList(Of String)
        segments = Request.GetFriendlyUrlSegments


        Dim vName As String

        'jam 2022/06/01
        If segments.Count = 0 Then

            vName = Session("VName")

        Else

            If segments.Count = 1 Then

                vName = segments(0)


            Else
                vName = segments(0)
                Session("vNamecall") = vName

            End If

        End If


        Session("vNamecall") = vName








        If (Not IsPostBack) Then
            'Collection of selected vars
            Session("DrvSelVars") = New Collection
            Session("DrvSelVars2") = New Collection
            Session("DataForDGraphCause") = New DataTable
            Session("DataForDGraphUse") = New DataTable
            'jrs 2012/05/23
            'If Not IsNothing(Request.QueryString("VName")) Then

            'jam 2022/06/01
            If Not IsNothing(Request.QueryString("VName")) Or Not IsNothing(Session("vNamecall")) Then

                'Session("VName") = Request.QueryString("VName")
                Session("VName") = Session("vNamecall")

            End If
        End If
        AddHandler cswmWebMenu.ServerItemClick, AddressOf ItemClickHandler
        'lblJS.Visible = True - always true

        Dim diagramRenderer As New DiagramRenderer()
        diagramRenderer.Render()

        PackJS(If(Session("bShowDefs"), False))


    End Sub
    Sub ItemClickHandler(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles cswmWebMenu.ServerItemClick
        Dim objSender As Coalesys.WebMenu.WebMenu
        objSender = sender

        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e

        'Choose the correct function
        Select Case objEA.Item.Caption
            Case "Show Definitions", "Hide Definitions"
                mnuShowVarDef_Click()
            Case "Load Selected for Analysis"
                mnuLoadVar_Click()
            Case "Delete Selected from Display"
                mnuDelVar_Click()
            Case "Select All Drivers"
                mnuSelAllDrivers_Click()
            Case "Select All Driven"
                mnuSelAllDriven_Click()
            Case "Select All"
                mnuSelAll_Click()
            Case "Unselect All"
                mnuUnSelAll_Click()
            Case "Zoom +"
                Zoom("+")
            Case "Zoom -"
                Zoom("-")
        End Select
    End Sub

    Private Sub mnuLoadVar_Click()
        'Load Selected Variables for Analysis
        Dim i As Integer
        Dim sVar As String, idx As Integer = -1, sDim1 As String, idxDim1 As Integer
        Dim sDim2 As String, idxDim2 As Integer = 1, sDim3 As String, idxDim3 As Integer = 1

        'jam 2022/06/01	
        Dim Vpath As String
        Dim Vext As String
        Dim vfriendlyurl As String
        Dim vurl As String


        Dim ifl As Integer, bGroup As Object
        If Session("DrvDim1") Is Nothing Then
            sDim1 = ""
            idxDim1 = 1
            ifl = 0
            sDim2 = ""
            idxDim2 = 1
            sDim3 = ""
            idxDim3 = 1
        Else
            sDim1 = Session("DrvDim1")
            idxDim1 = -1
            sDim2 = Session("DrvDim2")
            If sDim2 > "" Then
                idxDim2 = -1
            Else
                idxDim2 = 1
            End If
            sDim3 = Session("DrvDim3")
            If sDim3 > "" Then
                idxDim3 = -1
            Else
                idxDim3 = 1
            End If
            ifl = Session("DrvFl")
        End If

        Dim iRowPosition As Integer, iFirstYear As Integer
        If Session("VarSelCheck") = "mnuVarSelOptHistoricPlusForecast" Then
            iRowPosition = LastYearHistory - FirstYearHistory + 1
            iFirstYear = FirstYearForecast
        Else
            iRowPosition = 1
            If UCase(Session("ActiveBase")) = "IFSBASE.RUN" Then
                iFirstYear = FirstYearForecast
            Else
                iFirstYear = FirstYearHistory
            End If
        End If

        For i = 1 To CType(Session("DrvSelVars"), Collection).Count
            sVar = CType(Session("DrvSelVars"), Collection).Item(i).ToString
            'Load Variable
            Session("COUNTE") = Session("COUNTE") + 1
            idx = -1
            sDim2 = Session("DrvDim2")
            sDim3 = Session("DrvDim3")
            gLoadForDisplay(sVar, idx, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3, "", ifl, True, True, bGroup, 1, Session("PLOT"), Session("COUNTE"),
            iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, bGroup, ifl, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"),
            Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Next

        For i = 1 To CType(Session("DrvSelVars2"), Collection).Count
            sVar = CType(Session("DrvSelVars2"), Collection).Item(i).ToString
            'Load Variable
            Session("COUNTE") = Session("COUNTE") + 1
            idx = -1
            sDim2 = ""
            sDim3 = ""
            gLoadForDisplay(sVar, idx, sDim1, idxDim1, sDim2, idxDim2, sDim3, idxDim3, "", ifl, True, True, bGroup, 1, Session("PLOT"), Session("COUNTE"),
            iRowPosition, iFirstYear, Session("LastYear"), 0, False, Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"),
            Session("ShortSce"), Session("FileforDisplay"), Session("FinYear"),
            Session("IFs_MDB2Active"), Session("CurrencyVal"))
            gLoadForDisplaySaveLbls(Session("COUNTE"), idx, sDim1, sDim2, sDim3, bGroup, ifl, "",
            Session("SensitivityFlag"), Session("ShortSce"), Session("CurrencySym"), Session("VARN"),
            Session("GR1N"), Session("GR2N"), Session("GR3N"), Session("GR4N"),
            Session("FDNG1"), Session("FDNG2"), Session("FDNG3"), Session("FDNG4"))
        Next

        If Session("COUNTE") > 0 Then
            'Support.FormManager.Show(Me, "frm_VarSel.aspx")

            Vpath = Request.GetFriendlyUrlFileVirtualPath
            Vext = Request.GetFriendlyUrlFileExtension
            vfriendlyurl = FriendlyUrl.Resolve("~/frm_VarSel.aspx")
            vurl = FriendlyUrl.Href("~/frm_VarSel")
            Response.Redirect(vurl)


        End If
    End Sub

    Private Sub mnuDelVar_Click()
        PackJS(If(Session("bShowDefs"), False), RemoveSelected:=True)
    End Sub

    Private Sub mnuSelAllDrivers_Click()
        Dim dtcause = DirectCast(Session("DataForDGraphCause"), DataTable)

        '   'Expr1000	Distance	UsesVariable  DefText IsSelected
        For Each r As DataRow In dtcause.Rows
            r.Item(4) = "y"
            Dim sVar = r.Item(2).ToString
            If Not CType(Session("DrvSelVars"), Collection).Contains(sVar) Then
                CType(Session("DrvSelVars"), Collection).Add(sVar, sVar)
            End If
        Next
        PackJS(If(Session("bShowDefs"), False))

    End Sub

    Private Sub mnuSelAllDriven_Click()
        Dim dtuse = DirectCast(Session("DataForDGraphUse"), DataTable)
        '   'Expr1000	Distance	UsesVariable  DefText IsSelected
        For Each r As DataRow In dtuse.Rows
            r.Item(4) = "y"
            Dim sVar = r.Item(2).ToString
            If Not CType(Session("DrvSelVars2"), Collection).Contains(sVar) Then
                CType(Session("DrvSelVars2"), Collection).Add(sVar, sVar)
            End If
        Next
        PackJS(If(Session("bShowDefs"), False))
    End Sub

    Private Sub mnuSelAll_Click()
        Dim dtcause = DirectCast(Session("DataForDGraphCause"), DataTable)
        Dim dtuse = DirectCast(Session("DataForDGraphUse"), DataTable)
        '   'Expr1000	Distance	UsesVariable  DefText IsSelected
        For Each r As DataRow In dtcause.Rows
            r.Item(4) = "y"
            Dim sVar = r.Item(2).ToString
            If Not CType(Session("DrvSelVars"), Collection).Contains(sVar) Then
                CType(Session("DrvSelVars"), Collection).Add(sVar, sVar)
            End If
        Next
        For Each r As DataRow In dtuse.Rows
            r.Item(4) = "y"
            Dim sVar = r.Item(2).ToString
            If Not CType(Session("DrvSelVars2"), Collection).Contains(sVar) Then
                CType(Session("DrvSelVars2"), Collection).Add(sVar, sVar)
            End If
        Next
        PackJS(If(Session("bShowDefs"), False))
    End Sub

    Private Sub mnuUnSelAll_Click()
        Dim dtcause = DirectCast(Session("DataForDGraphCause"), DataTable)
        Dim dtuse = DirectCast(Session("DataForDGraphUse"), DataTable)
        '   'Expr1000	Distance	UsesVariable  DefText IsSelected
        For Each r As DataRow In dtcause.Rows
            r.Item(4) = "n"
        Next
        For Each r As DataRow In dtuse.Rows
            r.Item(4) = "n"
        Next
        CType(Session("DrvSelVars"), Collection).Clear()
        CType(Session("DrvSelVars2"), Collection).Clear()
        PackJS(If(Session("bShowDefs"), False))
    End Sub

    Private Sub mnuShowVarDef_Click()
        If Session("bShowDefs") Is Nothing Then
            Session("bShowDefs") = True
            cswmWebMenu.Groups("mnuVariables").Items("mnuShowDefs").Caption = "Hide Definitions"
        Else
            Session("bshowDefs") = Nothing
            cswmWebMenu.Groups("mnuVariables").Items("mnuShowDefs").Caption = "Show Definitions"
        End If
        PackJS(If(Session("bShowDefs"), False))

    End Sub
    Private Sub PackJS(ByVal ShowDef As Boolean, Optional ByVal RemoveSelected As Boolean = False)
        'ttt 2020/06/12
        Dim vn = Session("VName")
        Dim MainDefText As String = vn
        If (Not Session("VDef") Is Nothing) Then
            MainDefText = Session("VDef")
        End If
        If ShowDef Then
            vn = Session("VName") & " - " & MainDefText
        End If
        Dim Gray_Red As String = If(Session("VSelected") = "y", "'gray'", "'red'")
        Dim dtcause = DirectCast(Session("DataForDGraphCause"), DataTable)
        Dim dtuse = DirectCast(Session("DataForDGraphUse"), DataTable)

        Dim sbnodes = New StringBuilder()
        sbnodes.AppendLine.Append(",nodes: [")
        Dim sb As StringBuilder = New StringBuilder()
        sb.AppendLine()
        sb.AppendLine.Append("<script type=""text/javascript"">")
        sb.AppendLine.Append("Highcharts.chart('container', {")
        sb.AppendLine.Append("    chart: {")
        sb.AppendLine.Append("        type: 'networkgraph',")
        sb.AppendLine.Append("   height: (9 / 16 * 100 - 12 ) + '%'")  '// 16:9 ratio - adjust
        'sb.AppendLine.Append("        height: '*'") 'old=100-70%
        'sb.AppendLine.Append("        height: '45%'") 'old=100-70%
        sb.AppendLine.Append("    },")
        Dim st As String = Session("VName") & " - " & escq(MainDefText) 'jrs 2024/03/27
        sb.AppendLine.Append("    title: {")
        sb.AppendLine.Append("        text: '" & st & "'")
        sb.AppendLine.Append("    },")
        sb.AppendLine.Append("    subtitle: {")
        Dim st2 As String = "Blue and green lines: drivers, Yellow and orange lines: driven"
        sb.AppendLine.Append("        text: '" & st2 & "'")
        sb.AppendLine.Append("    },")
        sb.AppendLine.Append("    plotOptions: {")
        sb.AppendLine.Append("        networkgraph: {")
        sb.AppendLine.Append("            keys: ['from', 'to','color','width','dashStyle'],")
        sb.AppendLine.Append("            layoutAlgorithm: {")
        'sb.AppendLine.Append("                enableSimulation: true,")
        'sb.AppendLine.Append("                friction: -0.9")
        sb.AppendLine.Append("                linkLength: 50")
        sb.AppendLine.Append("            }")
        sb.AppendLine.Append("    },")
        'series option
        sb.AppendLine.Append("                                series: {")
        sb.AppendLine.Append("                                       allowPointSelect: true,")
        sb.AppendLine.Append("                                    point: {")
        sb.AppendLine.Append("            	                    events: {")
        sb.AppendLine.Append("              	                    dblclick: function(e) {")
        sb.AppendLine.Append("                                    var k = e.toElement.point.id;")
        sb.AppendLine.Append("                                    var k2 = k;")
        sb.AppendLine.Append("                                    if (k.indexOf(' -') >= 0) {")
        sb.AppendLine.Append("                                        k2 = k.substring(0, k.indexOf(' -'));")
        sb.AppendLine.Append("                                    }")
        sb.AppendLine.Append("                                    window.location.href = 'frm_Drivers.aspx?VName=' + k2.replace('*','');")
        sb.AppendLine.Append("                                    }")
        sb.AppendLine.Append("                                  }}}},")
        sb.AppendLine.Append("    series: [{!_ALGO_!,")
        sb.AppendLine.Append("        link: {width:4},")
        sb.AppendLine.Append("        dataLabels: {")
        sb.AppendLine.Append("            enabled: true,")
        sb.AppendLine.Append("            linkFormat: '',")
        sb.AppendLine.Append("            format: '{point.id}'")
        sb.AppendLine.Append("        },")
        sb.AppendLine.Append("        id: 'lang-tree',")
        sb.AppendLine.Append("        data: [")
        'Expr1000	Distance	UsesVariable  DefText IsSelected
        Dim HasNearCause As Boolean = False
        Dim HasFarCause As Boolean = False
        Dim HasNearUse As Boolean = False
        Dim HasFarUse As Boolean = False
        Dim TotalNearCause As Integer = 0
        Dim TotalFarCause As Integer = 0
        Dim TotalNearUse As Integer = 0
        Dim TotalFarUse As Integer = 0
        For i = 0 To dtcause.Rows.Count - 1
            If RemoveSelected AndAlso dtcause.Rows(i).Item(4).ToString = "y" Then Continue For
            If CInt(dtcause.Rows(i).Item(0)) Mod 2 = 1 Then
                HasNearCause = True
                TotalNearCause += 1
                'Exit For
            End If
        Next
        For i = 0 To dtcause.Rows.Count - 1
            If RemoveSelected AndAlso dtcause.Rows(i).Item(4).ToString = "y" Then Continue For
            If CInt(dtcause.Rows(i).Item(0)) Mod 2 <> 1 Then
                HasFarCause = True
                TotalFarCause += 1
                'Exit For
            End If
        Next
        For i = 0 To dtuse.Rows.Count - 1
            If RemoveSelected AndAlso dtuse.Rows(i).Item(4).ToString = "y" Then Continue For
            If CInt(dtuse.Rows(i).Item(0)) Mod 2 = 1 Then
                HasNearUse = True
                TotalNearUse += 1
                'Exit For
            End If
        Next
        For i = 0 To dtuse.Rows.Count - 1
            If RemoveSelected AndAlso dtuse.Rows(i).Item(4).ToString = "y" Then Continue For
            If CInt(dtuse.Rows(i).Item(0)) Mod 2 <> 1 Then
                HasFarUse = True
                TotalFarUse += 1
                'Exit For
            End If
        Next
        Dim ShouldMoveLeft As Boolean = (Not HasNearCause) AndAlso (Not HasFarCause)
        Dim ShouldMoveRight As Boolean = (Not HasNearUse) AndAlso (Not HasFarUse)
        Dim AdjLeft As String = If(ShouldMoveLeft, " - (width / 6) ", "")
        Dim AdjRight As String = If(ShouldMoveRight, " + (width / 6) ", "")
        '1 main link
        If HasNearCause Then
            sb.AppendLine.Append("            ['" & vn & "','DRIVERS>','green',4,'ShortDot'],")
        End If
        If HasNearUse Then
            sb.AppendLine.Append("            ['" & vn & "','>DRIVEN', 'yellow', 4, 'Dash'],")
        End If
        '1 for far DRIVERS
        If HasFarCause Then
            'sb.AppendLine.Append("            ,['DRIVERS>','DRIVERS>>'," & Gray_Red & ",16]")
            sb.AppendLine.Append("            ,['" & vn & "','DRIVERS>>', 'blue',4, 'Dot'],")
        End If
        '1 for far DRIVEN
        If HasFarUse Then
            sb.AppendLine.Append("            ,['" & vn & "','>>DRIVEN', 'orange', 4,'LongDash'],")
        End If
        Dim sbAlgo As New StringBuilder("")
        sbAlgo.AppendLine.Append("layoutAlgorithm: {")
        sbAlgo.AppendLine.Append("enableSimulation: false,")
        sbAlgo.AppendLine.Append("attractiveForce: function () {")
        sbAlgo.AppendLine.Append("               return 0;")
        sbAlgo.AppendLine.Append("          },")
        sbAlgo.AppendLine.Append("        repulsiveForce: function () {")
        sbAlgo.AppendLine.Append("              return 0;")
        sbAlgo.AppendLine.Append("          },")
        sbAlgo.AppendLine.Append("          integration: 'euler',")
        sbAlgo.AppendLine.Append("           gravitationalConstant: 0,")
        sbAlgo.AppendLine.Append("initialPositions: function () {")
        sbAlgo.AppendLine.Append("var chart = this.series[0].chart,")
        sbAlgo.AppendLine.Append("width = chart.plotWidth,")
        sbAlgo.AppendLine.Append("height = chart.plotHeight;")
        sbAlgo.AppendLine.Append("var causeX =  (2 * width / 6) " & AdjRight & ";")
        sbAlgo.AppendLine.Append("var causeY = height / 2;")
        sbAlgo.AppendLine.Append("var causefX = (1 * width / 6) " & AdjRight & ";")
        sbAlgo.AppendLine.Append("var useX = (4 * width / 6) " & AdjLeft & ";")
        sbAlgo.AppendLine.Append("var usefX =  (5 * width / 6) " & AdjLeft & ";")
        sbAlgo.AppendLine.Append("var pi2 =Math.PI * 2;")
        sbAlgo.AppendLine.Append("var CauseArray = [];")
        'jrs 2020/08/04
        If TotalNearCause >= 27 Then
            sbAlgo.AppendLine.Append("var CauseRange = Math.PI * 1.5;")
        Else
            Dim r As Short = (TotalNearCause - 1)
            sbAlgo.AppendLine.Append("var CauseRange = Math.PI * " + r.ToString + " / 18;")
        End If
        'sbAlgo.AppendLine.Append("var CauseAn = Math.random() * pi2;")
        sbAlgo.AppendLine.Append("var CauseAn = (pi2 - CauseRange) / 2;")

        sbAlgo.AppendLine.Append("var fCauseArray = [];")
        'sbAlgo.AppendLine.Append("var fCauseAn = Math.random() * pi2;")
        sbAlgo.AppendLine.Append("var fCauseAn = (7 / 6) + 0.087266;") ' // + 5 degrees
        sbAlgo.AppendLine.Append("var fCauseRange = pi2 - 2 * (7 / 6 + 0.087266);")

        sbAlgo.AppendLine.Append("var UseArray = [];")
        'jrs 2020/08/04
        If TotalNearUse >= 27 Then
            sbAlgo.AppendLine.Append("var UseRange = Math.PI * 1.5;")
        Else
            Dim r As Short = (TotalNearUse - 1)
            sbAlgo.AppendLine.Append("var UseRange = Math.PI * " + r.ToString + " / 18;")
        End If
        'sbAlgo.AppendLine.Append("var UseAn = Math.random() * pi2;")
        sbAlgo.AppendLine.Append("var UseAn = Math.PI + (pi2 - UseRange) / 2;")

        sbAlgo.AppendLine.Append("var fUseArray = [];")
        sbAlgo.AppendLine.Append("var fUseAn = Math.random() * pi2;")
        sbAlgo.AppendLine.Append("var fUseAn = Math.PI - (7 / 6) - 0.087266;")
        sbAlgo.AppendLine.Append("var fUseRange = 2 * (Math.PI - 7 / 6 - 0.087266);")
        'sbAlgo.AppendLine.Append("fUseArray.push('TFR';")
        Dim NodeNames As New List(Of String)
        'sbAlgo.AppendLine.Append("this.nodes.forEach(Function (node) {")
        For i = 0 To dtcause.Rows.Count - 1
            If RemoveSelected AndAlso dtcause.Rows(i).Item(4).ToString = "y" Then Continue For
            Dim val0 = dtcause.Rows(i).Item(2).ToString
            Dim val = val0
            If NodeNames.Contains(val) Then
                val = val & "*"
                If NodeNames.Contains(val) Then
                    val = val & "*"
                    If NodeNames.Contains(val) Then
                        val = val & "*"
                        If NodeNames.Contains(val) Then
                            val = val & "*"
                        End If
                    End If
                End If
            End If
            If ShowDef Then
                val &= " - " & escq(dtcause.Rows(i).Item(3).ToString)
            End If
            If CInt(dtcause.Rows(i).Item(0)) Mod 2 = 1 Then
                If dtcause.Rows(i).Item(4).ToString = "y" Then
                    sb.AppendLine.Append("            [ 'DRIVERS>','" & val & "','gray',4],")
                    sbnodes.AppendLine.Append("            {id: '" & val & "',marker: {radius: 6}, selected:true, color: 'gray',name: '" & escq(dtcause.Rows(i).Item(3).ToString) & "'},")
                Else
                    sb.AppendLine.Append("            [ 'DRIVERS>','" & val & "','green',4],")
                    sbnodes.AppendLine.Append("            {id: '" & val & "',events: { click: function () { PageMethods.SetSelSession('C'+(this.selected===true?'-':'') + this.id); } },marker: {radius: 6}, color: 'orange',name: '" & escq(dtcause.Rows(i).Item(3).ToString) & "'},")
                End If
                sbAlgo.AppendLine.Append("CauseArray.push('" & val & "');")
            Else
                If dtcause.Rows(i).Item(4).ToString = "y" Then
                    sb.AppendLine.Append("            ['DRIVERS>>','" & val & "', 'gray',4],")
                    sbnodes.AppendLine.Append("            {id: '" & val & "',marker: {radius: 6}, selected:true, color: 'gray',name: '" & escq(dtcause.Rows(i).Item(3).ToString) & "'},") 'default color
                Else
                    sb.AppendLine.Append("            ['DRIVERS>>','" & val & "', 'blue',4],")
                    sbnodes.AppendLine.Append("            {id: '" & val & "',events: { click: function () { PageMethods.SetSelSession('C'+(this.selected===true?'-':'') + this.id); } },marker: {radius: 6},name: '" & escq(dtcause.Rows(i).Item(3).ToString) & "'},") 'default color
                End If
                sbAlgo.AppendLine.Append("fCauseArray.push('" & val & "');")
            End If
            NodeNames.Add(val)
        Next
        'Forward (DRIVEN)
        'Expr1000	Distance Variable  DefText IsSelected
        For i = 0 To dtuse.Rows.Count - 1
            If RemoveSelected AndAlso dtuse.Rows(i).Item(4).ToString = "y" Then Continue For
            Dim val0 = dtuse.Rows(i).Item(2).ToString
            Dim val = val0
            If NodeNames.Contains(val) Then
                val = val & "*"
                If NodeNames.Contains(val) Then
                    val = val & "*"
                    If NodeNames.Contains(val) Then
                        val = val & "*"
                        If NodeNames.Contains(val) Then
                            val = val & "*"
                        End If
                    End If
                End If
            End If
            If ShowDef Then
                val &= " - " & escq(dtuse.Rows(i).Item(3).ToString)
            End If
            If CInt(dtuse.Rows(i).Item(0)) Mod 2 = 1 Then
                If dtuse.Rows(i).Item(4).ToString = "y" Then
                    sb.AppendLine.Append("            [ '>DRIVEN','" & val & "','gray',4]" & If(i = dtuse.Rows.Count - 1, "", ","))
                    sbnodes.AppendLine.Append("            {id: '" & val & "',marker: {radius: 6}, selected:true, color: 'gray',name: '" & escq(dtuse.Rows(i).Item(3).ToString) & "'},")
                Else
                    sb.AppendLine.Append("            [ '>DRIVEN','" & val & "','yellow',4]" & If(i = dtuse.Rows.Count - 1, "", ","))
                    sbnodes.AppendLine.Append("            {id: '" & val & "',events: { click: function () { PageMethods.SetSelSession('U'+(this.selected===true?'-':'') + this.id); } },marker: {radius: 6}, color: 'black',name: '" & escq(dtuse.Rows(i).Item(3).ToString) & "'},")
                End If
                sbAlgo.AppendLine.Append("UseArray.push('" & val & "');")
            Else
                If dtuse.Rows(i).Item(4).ToString = "y" Then
                    sb.AppendLine.Append("            [ '>>DRIVEN','" & val & "','gray',4]" & If(i = dtuse.Rows.Count - 1, "", ","))
                    sbnodes.AppendLine.Append("            {id: '" & val & "',marker: {radius: 6}, selected:true, color: 'gray',name: '" & escq(dtuse.Rows(i).Item(3).ToString) & "'},")
                Else
                    sb.AppendLine.Append("            [ '>>DRIVEN','" & val & "','orange',4]" & If(i = dtuse.Rows.Count - 1, "", ","))
                    sbnodes.AppendLine.Append("            {id: '" & val & "',events: { click: function () { PageMethods.SetSelSession('U'+(this.selected===true?'-':'') + this.id); } },marker: {radius: 6}, color: 'red',name: '" & escq(dtuse.Rows(i).Item(3).ToString) & "'},")
                End If
                sbAlgo.AppendLine.Append("fUseArray.push('" & val & "');")
            End If
            NodeNames.Add(val)
        Next
        'End of data:
        sb.AppendLine.Append("              ]")
        '-----
        sbAlgo.AppendLine.Append("this.nodes.forEach(function (node) {")
        sbAlgo.AppendLine.Append("if (node.id === 'DRIVERS>>') {")
        sbAlgo.AppendLine.Append("node.plotX = causefX;")
        sbAlgo.AppendLine.Append("node.plotY = causeY;")
        sbAlgo.AppendLine.Append("}")
        sbAlgo.AppendLine.Append("else if (node.id === 'DRIVERS>') {")
        sbAlgo.AppendLine.Append("node.plotX = causeX;")
        sbAlgo.AppendLine.Append("node.plotY = causeY;")
        sbAlgo.AppendLine.Append("}")
        sbAlgo.AppendLine.Append("else if (node.id === '" & vn & "') {")
        sbAlgo.AppendLine.Append("node.plotX = (3 * width / 6) " & AdjLeft & AdjRight & ";")
        sbAlgo.AppendLine.Append("node.plotY = causeY;")
        sbAlgo.AppendLine.Append("}")
        sbAlgo.AppendLine.Append("else if (node.id === '>>DRIVEN') {")
        sbAlgo.AppendLine.Append("node.plotX = usefX;")
        sbAlgo.AppendLine.Append("node.plotY = causeY;")
        sbAlgo.AppendLine.Append("}")
        sbAlgo.AppendLine.Append("else if (node.id === '>DRIVEN') {")
        sbAlgo.AppendLine.Append("node.plotX = useX;")
        sbAlgo.AppendLine.Append("node.plotY = causeY;")
        sbAlgo.AppendLine.Append("}")
        '=============
        'X-Ys
        Dim iCause As Integer = 0
        Dim iFarCause As Integer = 0
        NodeNames = New List(Of String)
        For i = 0 To dtcause.Rows.Count - 1
            If RemoveSelected AndAlso dtcause.Rows(i).Item(4).ToString = "y" Then Continue For
            Dim val0 = dtcause.Rows(i).Item(2).ToString
            Dim val = val0
            'anf 23 START
            If (Session("bShowDefs") = True) Then
                val = val + " - " + dtcause.Rows(i).Item(3)
            End If
            'anf 23 END

            If NodeNames.Contains(val) Then
                val = val & "*"
                If NodeNames.Contains(val) Then
                    val = val & "*"
                    If NodeNames.Contains(val) Then
                        val = val & "*"
                        If NodeNames.Contains(val) Then
                            val = val & "*"
                        End If
                    End If
                End If
            End If
            If CInt(dtcause.Rows(i).Item(0)) Mod 2 = 1 Then
                'NearCause
                If TotalNearCause = 2 OrElse TotalNearCause = 1 Then
                    'If there’s only 2 nodes in the category, DRIVEN an angle of 90 and -90
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    sbAlgo.AppendLine.Append("node.plotX =  causeX + Math.cos(pi2*" & If(TotalNearCause = 1, 3, iCause + 1) & "/4) * width/7; ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(pi2*" & If(TotalNearCause = 1, 3, iCause + 1) & "/4) * width / 7;")
                    sbAlgo.AppendLine.Append("}")
                    iCause += 2
                Else
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    'jrs2020/08/03
                    'sbAlgo.AppendLine.Append("node.plotX =  causeX + Math.cos(CauseAn + pi2*" & iCause & "/" & TotalNearCause & ") * width/7; ")
                    'sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(CauseAn + pi2*" & iCause & "/" & TotalNearCause & ") * width / 7;")
                    sbAlgo.AppendLine.Append("node.plotX =  causeX + Math.cos(CauseAn + CauseRange*" & iCause & "/" & TotalNearCause & ") * width/7; ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(CauseAn + CauseRange*" & iCause & "/" & TotalNearCause & ") * width / 7;")
                    sbAlgo.AppendLine.Append("}")
                    iCause += 1
                End If
            Else 'Far DRIVERS
                If TotalFarCause = 2 OrElse TotalFarCause = 1 Then
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    sbAlgo.AppendLine.Append("node.plotX =  causefX + 1.5*(Math.cos(pi2*" & If(TotalFarCause = 1, 3, iFarCause + 1) & "/4) * width/7); ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(pi2*" & If(TotalFarCause = 1, 3, iFarCause + 1) & "/4) * width / 7);")
                    sbAlgo.AppendLine.Append("}")
                    iFarCause += 2
                Else
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    'node.plotX =  causefX + 1.5*(Math.cos(fCauseAn + fCauseRange*7/7) * width/7); 
                    sbAlgo.AppendLine.Append("node.plotX =  causefX + 1.5*(Math.cos(fCauseAn + fCauseRange*" & iFarCause & "/" & (TotalFarCause - 1) & ") * width/7); ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(fCauseAn + fCauseRange*" & iFarCause & "/" & (TotalFarCause - 1) & ") * width / 7);")
                    'sbAlgo.AppendLine.Append("node.plotX =  causefX + 1.5*(Math.cos(fCauseAn + pi2*" & iFarCause & "/" & TotalFarCause & ") * width/7); ")
                    'sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(fCauseAn + pi2*" & iFarCause & "/" & TotalFarCause & ") * width / 7);")
                    sbAlgo.AppendLine.Append("}")
                    iFarCause += 1
                End If
            End If
            NodeNames.Add(val)
        Next
        Dim iUse As Integer = 0
        Dim iFarUse As Integer = 0
        For i = 0 To dtuse.Rows.Count - 1
            If RemoveSelected AndAlso dtuse.Rows(i).Item(4).ToString = "y" Then Continue For
            Dim val0 = dtuse.Rows(i).Item(2).ToString
            Dim val = val0
            'anf 23 START
            If (Session("bShowDefs") = True) Then
                val = val + " - " + dtuse.Rows(i).Item(3)
            End If
            'anf 23 END

            If NodeNames.Contains(val) Then
                val = val & "*"
                If NodeNames.Contains(val) Then
                    val = val & "*"
                    If NodeNames.Contains(val) Then
                        val = val & "*"
                        If NodeNames.Contains(val) Then
                            val = val & "*"
                        End If
                    End If
                End If
            End If
            If CInt(dtuse.Rows(i).Item(0)) Mod 2 = 1 Then
                'NearUse
                If TotalNearUse = 2 OrElse TotalNearUse = 1 Then
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    sbAlgo.AppendLine.Append("node.plotX =  useX + Math.cos(pi2*" & If(TotalNearUse = 1, 3, iUse + 1) & "/4) * width/7; ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(pi2*" & If(TotalNearUse = 1, 3, iUse + 1) & "/4) * width / 7;")
                    sbAlgo.AppendLine.Append("}")
                    iUse += 2
                Else
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    'jrs 2020/08/04
                    'sbAlgo.AppendLine.Append("node.plotX =  useX + Math.cos(UseAn + pi2*" & iUse & "/" & TotalNearUse & ") * width/7; ")
                    'sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(UseAn + pi2*" & iUse & "/" & TotalNearUse & ") * width / 7;")
                    sbAlgo.AppendLine.Append("node.plotX =  useX + Math.cos(UseAn + UseRange*" & iUse & "/" & TotalNearUse & ") * width/7; ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(UseAn + UseRange*" & iUse & "/" & TotalNearUse & ") * width / 7;")
                    sbAlgo.AppendLine.Append("}")
                    iUse += 1
                End If
            Else 'Far DRIVEN
                If TotalFarUse = 2 OrElse TotalFarUse = 1 Then
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    sbAlgo.AppendLine.Append("node.plotX =  usefX + 1.5*(Math.cos(pi2*" & If(TotalFarUse = 1, 3, iFarUse + 1) & "/4) * width/7); ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(pi2*" & If(TotalFarUse = 1, 3, iFarUse + 1) & "/4) * width / 7);")
                    sbAlgo.AppendLine.Append("}")
                    iFarUse += 2
                Else
                    sbAlgo.AppendLine.Append("else if (node.id === '" & val & "') {")
                    sbAlgo.AppendLine.Append("node.plotX =  usefX + 1.5*(Math.cos(fUseAn - fUseRange*" & (TotalFarUse - 1 - iFarUse) & "/" & (TotalFarUse - 1) & ") * width/7); ")
                    sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(fUseAn - fUseRange*" & (TotalFarUse - 1 - iFarUse) & "/" & (TotalFarUse - 1) & ") * width / 7);")
                    'sbAlgo.AppendLine.Append("node.plotX =  usefX + 1.5*(Math.cos(fUseAn + pi2*" & iFarUse & "/" & TotalFarUse & ") * width/7); ")
                    'sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5 * (Math.Sin(fUseAn + pi2 * " & iFarUse & " / " & TotalFarUse & ") * width / 7);")
                    sbAlgo.AppendLine.Append("}")
                    iFarUse += 1
                End If
            End If
            NodeNames.Add(val)
        Next
        sbAlgo.AppendLine.Append("});")
        sbAlgo.AppendLine.Append("}")
        sbAlgo.AppendLine.Append("}")
        '=============
        'sbAlgo.AppendLine.Append("Else If (CauseArray.indexOf(node.id)>-1) {")
        ''sbAlgo.AppendLine.Append("var r = Math.sqrt(Math.random());")
        'sbAlgo.AppendLine.Append("var angle = Math.random() * Math.PI * 2;")
        'sbAlgo.AppendLine.Append("node.plotX = causeX + Math.cos(angle)*width/7;")
        'sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(angle) * width / 7;")
        'sbAlgo.AppendLine.Append("} ")
        'sbAlgo.AppendLine.Append("Else If (fCauseArray.indexOf(node.id)>-1) {")
        ''sbAlgo.AppendLine.Append("var r = Math.sqrt(Math.random());")
        'sbAlgo.AppendLine.Append("var angle = Math.random() * Math.PI * 2;")
        'sbAlgo.AppendLine.Append("node.plotX = causefX + 1.5*(Math.cos(angle)*width/7);")
        'sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(angle) * width / 7);")
        'sbAlgo.AppendLine.Append("} ")
        'sbAlgo.AppendLine.Append("Else If (UseArray.indexOf(node.id)>-1) {")
        ''sbAlgo.AppendLine.Append("var r = Math.sqrt(Math.random());")
        'sbAlgo.AppendLine.Append("var angle = Math.random() * Math.PI * 2;")
        'sbAlgo.AppendLine.Append("node.plotX = useX + Math.cos(angle)*width/7;")
        'sbAlgo.AppendLine.Append("node.plotY = causeY + Math.sin(angle) * width / 7;")
        'sbAlgo.AppendLine.Append("} ")
        'sbAlgo.AppendLine.Append("Else If (fUseArray.indexOf(node.id)>-1) {")
        ''sbAlgo.AppendLine.Append("var r = Math.sqrt(Math.random());")
        'sbAlgo.AppendLine.Append("var angle = Math.random() * Math.PI * 2;")
        'sbAlgo.AppendLine.Append("node.plotX = usefX + 1.5*(Math.cos(angle)*width/7);")
        'sbAlgo.AppendLine.Append("node.plotY = causeY + 1.5*(Math.sin(angle) * width / 7);")
        'sbAlgo.AppendLine.Append("} ")
        'sbAlgo.AppendLine.Append("});")
        'sbAlgo.AppendLine.Append("}")
        'sbAlgo.AppendLine.Append("}")

        sb.Replace("!_ALGO_!", sbAlgo.ToString)
        '--------
        '4 Nodes Of Main

        sbnodes.AppendLine.Append("          {id: '" & vn & "',")
        sbnodes.AppendLine.Append("            color: " & Gray_Red & ",")
        'name = tooltip
        sbnodes.AppendLine.Append("         name: '" & escq(MainDefText) & "',")
        sbnodes.AppendLine.Append("            marker: {")
        'only last one is square
        'sbnodes.AppendLine.Append("                symbol: 'square',")
        sbnodes.AppendLine.Append("                       radius: 12}}")
        '------------------------
        If HasNearCause Then
            sbnodes.AppendLine.Append("          ,{id: 'DRIVERS>',")
            sbnodes.AppendLine.Append("            color: " & Gray_Red & ",")
            '
            sbnodes.AppendLine.Append("         name: '" & escq(MainDefText) & "',")
            sbnodes.AppendLine.Append("            marker: {")
            sbnodes.AppendLine.Append("                       symbol: 'square',radius: 6}}")
        End If
        '-----------------------------Far DRIVERS
        If HasFarCause Then
            sbnodes.AppendLine.Append("          ,{id: 'DRIVERS>>',")
            sbnodes.AppendLine.Append("            color: " & Gray_Red & ",")
            sbnodes.AppendLine.Append("         name: '" & escq(MainDefText) & "',") 'jrs 2024/03/27 added escq(
            sbnodes.AppendLine.Append("            marker: {")
            sbnodes.AppendLine.Append("                   symbol: 'square', radius: 6}}")
        End If
        '------------------------
        If HasNearUse Then
            sbnodes.AppendLine.Append("          ,{id: '>DRIVEN',")
            sbnodes.AppendLine.Append("            color: " & Gray_Red & ",")
            '
            sbnodes.AppendLine.Append("         name: '" & escq(MainDefText) & "',")
            sbnodes.AppendLine.Append("            marker: {")
            sbnodes.AppendLine.Append("                       symbol: 'triangle',radius: 6}}")
        End If
        '-----------------------------Far DRIVEN
        If HasFarUse Then
            sbnodes.AppendLine.Append("          ,{id: '>>DRIVEN',")
            sbnodes.AppendLine.Append("            color: " & Gray_Red & ",")
            sbnodes.AppendLine.Append("         name: '" & escq(MainDefText) & "',") 'jrs 2024/03/27 added escq(
            sbnodes.AppendLine.Append("            marker: {")
            sbnodes.AppendLine.Append("                   symbol: 'triangle',radius: 6}}")
        End If
        '----------------------
        sbnodes.AppendLine.Append("         ]")
        '------
        sb = sb.Append(sbnodes)
        '-----
        sb.AppendLine.Append("    }]")
        sb.AppendLine.Append("});")
        sb.AppendLine.Append("  </script>")
        sb.AppendLine()
        lblJS.Text = sb.ToString

    End Sub
    Private Sub Zoom(ByVal PlusMinus As String)
        'lblJS.Text = lblJS.Text.Replace("style='width:90%'", "style='width:50%""")
        '            ? container.Style.Item("width")
        '"90%"
        'container.Style.Add("width", "50%")
        If PlusMinus = "+" Then
            Dim curWidth = CInt(container.Style.Item("width").Replace("%", ""))
            'If curWidth > 70 Then Return
            container.Style.Add("width", (curWidth + 20).ToString & "%")
        Else
            Dim curWidth = CInt(container.Style.Item("width").Replace("%", ""))
            If curWidth < 20 Then Return
            container.Style.Add("width", (curWidth - 20).ToString & "%")
        End If

    End Sub
    <System.Web.Services.WebMethod>
    Public Shared Sub SetSelSession(ByVal sVar0 As String)
        'C-TFR, CTFR, or U-TFR, UTFR + Definition
        Dim p = sVar0.IndexOf(" -")
        If p > 0 Then
            sVar0 = sVar0.Substring(0, p)
        End If

        Dim sVar As String = ""
        If sVar0.StartsWith("C-") OrElse sVar0.StartsWith("U-") Then
            sVar = sVar0.Substring(2)
        Else
            sVar = sVar0.Substring(1)
        End If

        If sVar0.StartsWith("C") Then 'Cause
            Dim dtcause = DirectCast(HttpContext.Current.Session("DataForDGraphCause"), DataTable)
            If Not sVar0.StartsWith("C-") Then 'select
                If Not CType(HttpContext.Current.Session("DrvSelVars"), Collection).Contains(sVar) Then
                    CType(HttpContext.Current.Session("DrvSelVars"), Collection).Add(sVar, sVar)
                End If
                '   'Expr1000	Distance	UsesVariable  DefText IsSelected
                Dim found As Boolean = False
                For Each r As DataRow In dtcause.Rows
                    If sVar = r.Item(2).ToString Then
                        r.Item(4) = "y"
                        found = True
                        Exit For
                    End If
                Next
            Else ' - = Unselect
                If CType(HttpContext.Current.Session("DrvSelVars"), Collection).Contains(sVar) Then
                    CType(HttpContext.Current.Session("DrvSelVars"), Collection).Remove(sVar)
                End If
                Dim found As Boolean = False
                For Each r As DataRow In dtcause.Rows
                    If sVar = r.Item(2).ToString Then
                        r.Item(4) = "n"
                        found = True
                        Exit For
                    End If
                Next
            End If
        Else 'U = Use
            Dim dtuse = DirectCast(HttpContext.Current.Session("DataForDGraphUse"), DataTable)
            If Not sVar0.StartsWith("U-") Then 'select
                If Not CType(HttpContext.Current.Session("DrvSelVars2"), Collection).Contains(sVar) Then
                    CType(HttpContext.Current.Session("DrvSelVars2"), Collection).Add(sVar, sVar)
                End If
                Dim found As Boolean = False
                For Each r As DataRow In dtuse.Rows
                    If sVar = r.Item(2).ToString Then
                        r.Item(4) = "y"
                        found = True
                        Exit For
                    End If
                Next
            Else ' - = Unselect
                If CType(HttpContext.Current.Session("DrvSelVars2"), Collection).Contains(sVar) Then
                    CType(HttpContext.Current.Session("DrvSelVars2"), Collection).Remove(sVar)
                End If
                Dim found As Boolean = False
                For Each r As DataRow In dtuse.Rows
                    If sVar = r.Item(2).ToString Then
                        r.Item(4) = "n"
                        found = True
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Function escq(s As String) As String
        Return s.Replace("'", "\'")

    End Function

    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    ''' 
    '''      jam 2022/06/01
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sParameter">URL Parameters</param>
    ''' <returns>Parameters transformed in a valid URL format</returns>
    Public Function DriversprepareForFriendlyURL(ByVal sParameter As String) As String

        Dim tmp As String
        tmp = sParameter
        tmp = Replace(tmp, "%", "%25")
        tmp = Replace(tmp, "&", "%26")
        tmp = Replace(tmp, "+", "%2B")
        tmp = Replace(tmp, "<", "%3C")

        If tmp.IndexOf("=") >= 0 Then

            tmp = tmp.Substring(tmp.IndexOf("=") + 1, Len(tmp) - (tmp.IndexOf("=") + 1))
        End If



        DriversprepareForFriendlyURL = tmp
    End Function


End Class

