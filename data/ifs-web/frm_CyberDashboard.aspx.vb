Imports IFs.Data
Partial Class frm_CyberDashboard
    Inherits Page

    Private Sub PackJS()
        Dim bReLoad As Boolean = False
        Dim bReLoadVar As Boolean = False
        Dim bLoadFirst As Boolean = False
        '*********** Map *************
        Dim VarIdx As Integer
        If Session("CDVarName") Is Nothing Then
            Session("CDVarName") = "ICTCYBBENEFITNET"
            Session("CDDim1") = "World"
            Session("CDDim2") = ""
            Session("CDDim3") = ""
            'Session("CDVarNameLoaded") = "ICTCOMPUTERS"
            'Load Values
            ReDim Session("CDValues")(Session("LastYear") - Session("FirstYear") + 1, NR)
            ReDim Session("CDValuesAG")(Session("LastYear") - Session("FirstYear") + 1, 5)
            'jrs 2015/08/25
            ReDim Session("CDValuesAGCO")(Session("LastYear") - Session("FirstYear") + 1, 5)
            ReDim Session("CDValuesAGSL")(Session("LastYear") - Session("FirstYear") + 1, 5)

            ReDim Session("CDValuesLine")(Session("LastYear") - Session("FirstYear") + 1, MaxVariables)
            ReDim Session("CDValuesLineDim1")(MaxVariables)
            ReDim Session("CDValuesLineSce")(MaxVariables)
            'Session("CDValuesLineCOUNTE") = 0
            bReLoad = True
            bReLoadVar = True
            bLoadFirst = True
            cboVariables.SelectedValue = "ICT Cyber Net Benefit, as a percentage of GDP"
            cboVariables2.SelectedValue = "ICT Cyber Net Benefit, as a percentage of GDP"
            Session("CDVarDesc") = cboVariables.Text
            Session("optBenefits") = True
            Session("optCosts") = True
            Session("optCumulative") = False
            'lstRegionsMultiple.Text = Session("CDDim1")
            Session("CDRegs")(1) = True

            Session("CDSceName") = "Independent Internet"
            cboScenarioSingle2.Text = Session("CDSceName")
            cboScenarioSingle3.Text = Session("CDSceName")
            cboRegionSingle.Text = Session("CDDim1")
            Session("CDSceIdx") = 7
            'Dim ii As Integer
            'For ii = 2 To lstSceMultiple.Items.Count
            '    lstSceMultiple.Items(ii - 1).Selected = True
            'Next
            For i = 0 To 4
                Session("CDSces")(i) = True
            Next

        ElseIf (Session("CDVarDesc") <> cboVariables.SelectedValue And cboVariables.SelectedValue > "") Or
                (Session("CDVarDesc") <> cboVariables2.SelectedValue And cboVariables2.SelectedValue > "") Then
            ReDim Session("CDValues")(Session("LastYear") - Session("FirstYear") + 1, NR)

            If Session("CDVarDesc") <> cboVariables.SelectedValue And cboVariables.SelectedValue > "" Then
                Session("CDVarDesc") = cboVariables.SelectedValue
                cboVariables2.SelectedValue = cboVariables.SelectedValue
                'JAM 2015-05-24 Setting Variable Name
                Session("CDVarName") = LoadVarNameDsh(Session("PutFdrvRun"), cboVariables.SelectedValue)
            Else
                Session("CDVarDesc") = cboVariables2.SelectedValue
                cboVariables.SelectedValue = cboVariables2.SelectedValue
                'JAM 2015-05-24 Setting Variable Name
                Session("CDVarName") = LoadVarNameDsh(Session("PutFdrvRun"), cboVariables2.SelectedValue)
            End If
            'Session("CDValuesLineCOUNTE") = 0
            If cboVariables.SelectedValue > "" Then
                Session("CDVarDesc") = cboVariables.SelectedValue
            Else
                cboVariables.SelectedValue = Session("CDVarDesc")
            End If
            'If lstRegionsMultiple .Text > "" Then
            '    Session("CDDim1") = lstRegionsMultiple .Text
            'Else
            '    lstRegionsMultiple .Text = Session("CDDim1")
            'End If
            If cboRegionSingle.Text > "" Then
                Session("CDDim1") = cboRegionSingle.Text
            Else
                cboRegionSingle.Text = Session("CDDim1")
            End If
            bReLoadVar = True
        Else
            VarIdx = IfsVars.Item(Session("CDVarName")).Index
            cboVariables.SelectedValue = Session("CDVarDesc")
            cboVariables2.SelectedValue = Session("CDVarDesc")
        End If

        'Scneario
        If Session("CDSceName") <> cboScenarioSingle2.Text Or
                Session("CDSceName") <> cboScenarioSingle3.Text Then
            If cboScenarioSingle2.Text > "" And cboScenarioSingle3.Text > "" Then
                bReLoad = True
                If Session("CDSceName") <> cboScenarioSingle2.Text Then
                    Session("CDSceName") = cboScenarioSingle2.Text
                    cboScenarioSingle3.Text = Session("CDSceName")
                Else
                    Session("CDSceName") = cboScenarioSingle3.Text
                    cboScenarioSingle2.Text = Session("CDSceName")
                End If
            Else
                cboScenarioSingle2.Text = Session("CDSceName")
                cboScenarioSingle3.Text = Session("CDSceName")
            End If
            Dim ss As Integer
            If Session("CDSceName") = "Base Case" Then
                Session("CDSceIdx") = 1
            Else
                For ss = 1 To 4
                    If Session("CDSceFileNames")(ss) = Session("PutFdrvRun2") + "\" & Session("CDSceName") & ".run" Then
                        Session("CDSceIdx") = 5 + ss
                        Exit For
                    End If
                Next
            End If
        End If

        'Region
        Dim bReloadReg As Boolean = False
        If cboRegionSingle.Text <> Session("CDDim1") And cboRegionSingle.Text > "" Then
            Session("CDDim1") = cboRegionSingle.Text
            bReloadReg = True
        Else
            cboRegionSingle.Text = Session("CDDim1")
        End If

        'jrs 2015/06/22 
        'Cumulative vs Annualized
        If Session("optCumulative") <> optCumulative.Checked Then
            Session("optCumulative") = optCumulative.Checked
            optAnnualized.Checked = Not optCumulative.Checked
        End If
        'Benefits or Costs
        If optBoth.Checked Then
            If (Session("optBenefits") = False Or Session("optCosts") = False) Then
                Session("optBenefits") = True
                Session("optCosts") = True
            End If
        Else
            If Session("optBenefits") <> optBenefits.Checked Or Session("optCosts") <> optCosts.Checked Then
                Session("optBenefits") = optBenefits.Checked
                Session("optCosts") = optCosts.Checked
            End If
        End If


        '*********** Map *************
        If bReLoad Or bReLoadVar Then
            LoadCD4Map(VarIdx)
        End If
        '*********** Lines *************
        If bReLoadVar Then
            LoadCD4Lines(VarIdx)
        End If
        '*** bubble chart ***
        If bLoadFirst Then
            LoadSetCXP()
        End If
        '*** area chart ***
        If bReLoad Or bReloadReg Then
            LoadSetCB()
        End If


        Dim sJscript2 As String
        '************* map chart **********************
        sJscript2 = PackJSMap(VarIdx) & vbCr
        lblJSMap.Text = sJscript2

        '************* bubble chart **********************
        sJscript2 = ""
        sJscript2 = sJscript2 & PackJSBubble() & vbCr
        lblJSBubble.Text = sJscript2

        '************* area chart **********************
        sJscript2 = ""
        sJscript2 = sJscript2 & PackJSArea() & vbCr
        lblJSArea.Text = sJscript2

        '************* line chart **********************
        sJscript2 = ""
        sJscript2 = sJscript2 & PackJSLine(VarIdx) & vbCr
        lblJSline.Text = sJscript2

        'jrs 2015/08/10 lblJS.Text = ""
        sJscript2 = "<script type=""text/javascript"">" & vbCr
        sJscript2 = sJscript2 & "$(document).ready(function () {" & vbCr
        sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox({ source: SceListNames, multiSelect: true, width: 690, height: 35, autoOpen: true});" & vbCr
        sJscript2 = sJscript2 & "$(""#lstRegionsMultiple"").jqxComboBox({ source: VarRegions, multiSelect: true, width: 470, height: 35, autoOpen: true});" & vbCr
        If bLoadFirst Then
            sJscript2 = sJscript2 & "$(""#lstRegionsMultiple"").jqxComboBox('selectItem', 'World');" & vbCr

            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', 'Base Case');" & vbCr
            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', 'Clockwork Orange');" & vbCr
            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', 'Independent Internet');" & vbCr
            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', 'Leviathan');" & vbCr
            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', 'Shangri-La');" & vbCr
        Else
            If Not (Session("CDRegs")(1) Is Nothing) Then
                For i = 2 To NR + 12
                    If Session("CDRegs")(i - 1) Then
                        If i > 12 Then
                            sJscript2 = sJscript2 & "$(""#lstRegionsMultiple"").jqxComboBox('selectItem', '" & GRPS(1, i - 12) & "');" & vbCr
                        ElseIf i = 2 Then
                            sJscript2 = sJscript2 & "$(""#lstRegionsMultiple"").jqxComboBox('selectItem', 'World');" & vbCr
                        Else
                            sJscript2 = sJscript2 & "$(""#lstRegionsMultiple"").jqxComboBox('selectItem', '" & CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Keys(i - 3) & "');" & vbCr
                        End If
                    End If
                Next

                For i = 1 To 5
                    If Session("CDSces")(i - 1) Then
                        If i = 1 Then
                            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', 'Base Case');" & vbCr
                        Else
                            sJscript2 = sJscript2 & "$(""#lstSceMultiple"").jqxComboBox('selectItem', '" & Replace(Replace(Session("CDSceFileNames")(i - 1), Session("PutFdrvRun2") & "\", ""), ".run", "") & "');" & vbCr
                        End If
                    End If
                Next
            End If
        End If
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "</script>" & vbCr
        lblJS.Text = sJscript2

    End Sub

    Private Function PackJSMap(VarIdx As Integer) As String

        Dim sJscript2 As String

        sJscript2 = "<script type=""text/javascript"">" & vbCr

        'JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        ''sJscript2 = sJscript2 & "jQuery.noConflict();" & vbCr
        sJscript2 = sJscript2 & "var myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myHeight == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & "myHeight = myHeight * 0.75;" & vbCr

        sJscript2 = sJscript2 & "var example = 'all-maps', " & vbCr
        sJscript2 = sJscript2 & "theme = 'default';" & vbCr

        'JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        sJscript2 = sJscript2 & "function BuildMaps(){ // encapsulate jQuery" & vbCr
        ''sJscript2 = sJscript2 & "            $(function () {" & vbCr

        'jrs 2015/05/08 Prepare data
        sJscript2 = sJscript2 & "                // Prepare data" & vbCr
        sJscript2 = sJscript2 & "    var YrIdx = 0;" & vbCr
        sJscript2 = sJscript2 & "    var MaxYrIdx = " & CStr(Session("DisplayLastYear") - Session("FirstYear")) & ";" & vbCr
        sJscript2 = sJscript2 & "    var myPlayer;" & vbCr
        sJscript2 = sJscript2 & "    var FirstYear = " & Session("FirstYear") & ";" & vbCr
        sJscript2 = sJscript2 & "    var CountryVal = 0;" & vbCr
        sJscript2 = sJscript2 & "    var dataAllYrs = [];" & vbCr

        Dim i As Integer
        For i = 1 To Session("DisplayLastYear") - Session("FirstYear") + 1
            sJscript2 = sJscript2 & "    var data" & i.ToString & " = [" & vbCr
            sJscript2 = sJscript2 & PrepareDataForMap(i)
            sJscript2 = sJscript2 & "];" & vbCr
            sJscript2 = sJscript2 & "    dataAllYrs[" & (i - 1).ToString & "] = data" & i.ToString & ";" & vbCr
        Next

        sJscript2 = sJscript2 & "    var dataAllYrs2 = jQuery.extend(true, {}, dataAllYrs);" & vbCr

        sJscript2 = sJscript2 & "    // Base path to maps" & vbCr
        sJscript2 = sJscript2 & "var baseMapPath = ""http://code.highcharts.com/mapdata/""," & vbCr
        sJscript2 = sJscript2 & "showDataLabels = false, // Switch for data labels enabled/disabled" & vbCr
        sJscript2 = sJscript2 & "mapCount = 0," & vbCr
        sJscript2 = sJscript2 & "searchText," & vbCr
        sJscript2 = sJscript2 & "mapOptions = '';" & vbCr

        sJscript2 = sJscript2 & "// Populate dropdown menus and turn into jQuery UI widgets" & vbCr
        sJscript2 = sJscript2 & "$.each(Highcharts.mapDataIndex, function (mapGroup, maps) {" & vbCr
        sJscript2 = sJscript2 & "if (mapGroup !== ""version"") {" & vbCr
        sJscript2 = sJscript2 & "mapOptions += '<option class=""option-header"">' + mapGroup + '</option>';" & vbCr
        sJscript2 = sJscript2 & "$.each(maps, function (desc, path) {" & vbCr
        sJscript2 = sJscript2 & "mapOptions += '<option value=""' + path + '"">' + desc + '</option>';" & vbCr
        sJscript2 = sJscript2 & "mapCount += 1;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "searchText = 'Search ' + mapCount + ' maps';" & vbCr
        sJscript2 = sJscript2 & "mapOptions = '<option value=""custom/world.js"">' + searchText + '</option>' + mapOptions;" & vbCr
        sJscript2 = sJscript2 & "$(""#mapDropdown"").append(mapOptions).combobox();" & vbCr

        sJscript2 = sJscript2 & "// Change map when item selected in dropdown" & vbCr
        sJscript2 = sJscript2 & "$(""#mapDropdown"").change(function () {" & vbCr
        sJscript2 = sJscript2 & "var $selectedItem = $(""option:selected"", this)," & vbCr
        sJscript2 = sJscript2 & "mapDesc = $selectedItem.text()," & vbCr
        sJscript2 = sJscript2 & "mapKey = this.value.trim().slice(0, -3)," & vbCr
        sJscript2 = sJscript2 & "svgPath = baseMapPath + mapKey + '.svg'," & vbCr
        sJscript2 = sJscript2 & "geojsonPath = baseMapPath + mapKey + '.geo.json'," & vbCr
        sJscript2 = sJscript2 & "javascriptPath = baseMapPath + this.value.trim()," & vbCr
        sJscript2 = sJscript2 & "isHeader = $selectedItem.hasClass('option-header');" & vbCr

        'jrs 2015/05/18
        'Slow Map/ours good definition
        sJscript2 = sJscript2 & "if (mapKey == 'custom/world') {" & vbCr
        'sJscript2 = sJscript2 & "svgPath = 'Maps/country.svg';" & vbCr
        sJscript2 = sJscript2 & "geojsonPath = 'Maps/country.geo.json';" & vbCr
        sJscript2 = sJscript2 & "javascriptPath = 'Maps/country.js';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        'sJscript2 = sJscript2 & "        // Dim or highlight search box" & vbCr
        'sJscript2 = sJscript2 & "if (mapDesc === searchText || isHeader) {" & vbCr
        'sJscript2 = sJscript2 & "$('.custom-combobox-input').removeClass('valid');" & vbCr
        'sJscript2 = sJscript2 & "location.hash = '';" & vbCr
        'sJscript2 = sJscript2 & "} else {" & vbCr
        'sJscript2 = sJscript2 & "$('.custom-combobox-input').addClass('valid');" & vbCr
        'sJscript2 = sJscript2 & "location.hash = mapKey;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "        if (isHeader) {" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "        // Show loading" & vbCr
        sJscript2 = sJscript2 & "if ($(""#containermap"").highcharts()) {" & vbCr
        sJscript2 = sJscript2 & "$(""#containermap"").highcharts().showLoading('<i class=""fa fa-spinner fa-spin fa-2x""></i>');" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr


        sJscript2 = sJscript2 & "        // When the map is loaded or ready from cache..." & vbCr
        sJscript2 = sJscript2 & "function mapReady() {" & vbCr

        sJscript2 = sJscript2 & "            var mapGeoJSON = Highcharts.maps[mapKey]," & vbCr
        'jrs 2015/05/13 
        sJscript2 = sJscript2 & "data2 = []," & vbCr
        sJscript2 = sJscript2 & "dataX = []," & vbCr
        sJscript2 = sJscript2 & "joinXmap," & vbCr
        'sJscript2 = sJscript2 & "joinXdata," & vbCr
        sJscript2 = sJscript2 & "parent," & vbCr
        sJscript2 = sJscript2 & "match;" & vbCr

        'jrs 2015/05/13
        sJscript2 = sJscript2 & "    // Generate non-random data for the map" & vbCr
        sJscript2 = sJscript2 & "if (mapKey !== 'custom/world') {" & vbCr
        sJscript2 = sJscript2 & "$.each(mapGeoJSON.features, function (index, feature) {" & vbCr
        sJscript2 = sJscript2 & "data2.push({" & vbCr
        sJscript2 = sJscript2 & "key: feature.properties['hc-key']," & vbCr
        'sJscript2 = sJscript2 & "code: feature.properties['iso-a2']," & vbCr
        'sJscript2 = sJscript2 & "code: feature.properties['hc-key']," & vbCr
        'sJscript2 = sJscript2 & "value:  index" & vbCr
        sJscript2 = sJscript2 & "value:  CountryVal" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        'jrs 2015/05/13
        sJscript2 = sJscript2 & "            //Set appropiate data" & vbCr
        sJscript2 = sJscript2 & "if (mapKey !== 'custom/world') {" & vbCr
        sJscript2 = sJscript2 & "dataX = data2;" & vbCr
        sJscript2 = sJscript2 & "joinXmap = 'hc-key';" & vbCr
        'sJscript2 = sJscript2 & "joinXdata = 'key';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "else {" & vbCr
        sJscript2 = sJscript2 & "dataX = dataAllYrs2[0];" & vbCr
        'jrs 2015/05/27 
        'Fast Map/original highcharts
        'sJscript2 = sJscript2 & "joinXmap = 'iso-a2';" & vbCr
        'Slow Map/ours good definition
        sJscript2 = sJscript2 & "joinXmap = 'ISO_2DIGIT';" & vbCr

        'sJscript2 = sJscript2 & "joinXdata = 'code';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr


        sJscript2 = sJscript2 & "            // Show arrows the first time a real map is shown" & vbCr
        sJscript2 = sJscript2 & "if (mapDesc !== searchText) {" & vbCr
        sJscript2 = sJscript2 & "$('.selector .prev-next').show();" & vbCr
        sJscript2 = sJscript2 & "$('#sideBox').show();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "            // Is there a layer above this?" & vbCr
        sJscript2 = sJscript2 & "match = mapKey.match(/^(countries\/[a-z]{2}\/[a-z]{2})-[a-z0-9]+-all$/);" & vbCr
        sJscript2 = sJscript2 & "if (/^countries\/[a-z]{2}\/[a-z]{2}-all$/.test(mapKey)) { // country" & vbCr
        sJscript2 = sJscript2 & "parent = {" & vbCr
        sJscript2 = sJscript2 & "desc: 'World'," & vbCr
        sJscript2 = sJscript2 & "key: 'custom/world'" & vbCr
        sJscript2 = sJscript2 & "};" & vbCr
        sJscript2 = sJscript2 & "} else if (match) { // admin1" & vbCr
        sJscript2 = sJscript2 & "parent = {" & vbCr
        sJscript2 = sJscript2 & "desc: $('option[value=""' + match[1] + '-all.js""]').text()," & vbCr
        sJscript2 = sJscript2 & "key: match[1] + '-all'" & vbCr
        sJscript2 = sJscript2 & "};" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "$('#up').html('');" & vbCr
        sJscript2 = sJscript2 & "if (parent) {" & vbCr
        sJscript2 = sJscript2 & "$('#up').append(" & vbCr
        sJscript2 = sJscript2 & "$('<a><i class=""fa fa-angle-up""></i> ' + parent.desc + '</a>')" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "title:  parent.key" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".click(function () {" & vbCr
        sJscript2 = sJscript2 & "$('#mapDropdown').val(parent.key + '.js').change();" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ");" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr


        sJscript2 = sJscript2 & "    // Instantiate chart" & vbCr
        sJscript2 = sJscript2 & "$(""#containermap"").highcharts('Map', {" & vbCr

        'this works, but shows a big logo, right behind the map, no chance to configure size and position
        sJscript2 = sJscript2 & "chart: {" & vbCr
        'sJscript2 = sJscript2 & "plotBackgroundImage:   './Images/pcl.jpg'" & vbCr
        sJscript2 = sJscript2 & "height:   myHeight-205" & vbCr

        If Session("CDhash") = "imp" Then
            sJscript2 = sJscript2 & ", events: {"
            sJscript2 = sJscript2 & "load: function(event) {"
            sJscript2 = sJscript2 & "window.location.hash = ""imp"";"
            sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
            sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
            sJscript2 = sJscript2 & "}}"
        Else
            sJscript2 = sJscript2 & ", events: {"
            sJscript2 = sJscript2 & "load: function(event) {"
            sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
            sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
            sJscript2 = sJscript2 & "}}"
        End If

        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:   FirstYear," & vbCr
        sJscript2 = sJscript2 & "verticalAlign: 'bottom'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "subtitle: {" & vbCr
        sJscript2 = sJscript2 & "text:   '" & cboVariables.Text & " - " & cboScenarioSingle3.Text & "', " & vbCr
        sJscript2 = sJscript2 & "verticalAlign: 'top'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontFamily:  'Raleway'," & vbCr
        sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "        mapNavigation: {" & vbCr
        sJscript2 = sJscript2 & "enabled: true," & vbCr
        sJscript2 = sJscript2 & "enableMouseWheelZoom: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "                legend: {" & vbCr
        sJscript2 = sJscript2 & "layout:  'vertical'," & vbCr
        sJscript2 = sJscript2 & "align:  'left'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'bottom'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'If Session("MinCDValues") < 0 Then
        Dim CDValLimits(5) As Single, j As Integer
        Dim CDValRange As Single = (Session("MaxCDValues") - Session("MinCDValues")) / 6
        CDValLimits(3) = Math.Round((Session("MinCDValues") + Session("MaxCDValues")) / 2, Session("iRound"))
        CDValLimits(2) = Math.Round(CDValLimits(3) - CDValRange, Session("iRound"))
        CDValLimits(1) = Math.Round(CDValLimits(2) - CDValRange, Session("iRound"))
        CDValLimits(4) = Math.Round(CDValLimits(3) + CDValRange, Session("iRound"))
        CDValLimits(5) = Math.Round(CDValLimits(4) + CDValRange, Session("iRound"))

        sJscript2 = sJscript2 & "                colorAxis: {" & vbCr
        sJscript2 = sJscript2 & "                dataClasses: [" & vbCr
        sJscript2 = sJscript2 & "                {to: " & CDValLimits(1).ToString & "}," & vbCr
        For j = 2 To 5
            sJscript2 = sJscript2 & "                {from: " & CDValLimits(j - 1).ToString & ", to: " & CDValLimits(j).ToString & "}," & vbCr
        Next
        sJscript2 = sJscript2 & "                {from: " & CDValLimits(5).ToString & "}" & vbCr
        sJscript2 = sJscript2 & "]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        'Else
        'sJscript2 = sJscript2 & "                colorAxis: {" & vbCr
        'sJscript2 = sJscript2 & "min: 0," & vbCr
        'sJscript2 = sJscript2 & "stops: [" & vbCr
        'sJscript2 = sJscript2 & "[0, '#EFEFFF']," & vbCr
        'sJscript2 = sJscript2 & "[0.5, Highcharts.getOptions().colors[0]]," & vbCr
        'sJscript2 = sJscript2 & "[1, Highcharts.Color(Highcharts.getOptions().colors[0]).brighten(-0.5).get()]" & vbCr
        'sJscript2 = sJscript2 & "]" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        'End If

        sJscript2 = sJscript2 & "series: [{" & vbCr
        sJscript2 = sJscript2 & "mapData: mapGeoJSON},{" & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "data: data2," & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "data: dataAllYrs[0]," & vbCr
        sJscript2 = sJscript2 & "data: dataX," & vbCr
        sJscript2 = sJscript2 & "mapData: mapGeoJSON," & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "joinBy: ['hc-key', 'key']," & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "joinBy: ['iso-a2', 'code']," & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "joinBy: [joinXmap, joinXdata]," & vbCr
        sJscript2 = sJscript2 & "joinBy: [joinXmap, 'key']," & vbCr
        sJscript2 = sJscript2 & "name:   '" & VDef(VarIdx) & "'," & vbCr
        sJscript2 = sJscript2 & "states: {" & vbCr
        sJscript2 = sJscript2 & "hover: {" & vbCr
        sJscript2 = sJscript2 & "color: Highcharts.getOptions().colors[2]" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "dataLabels: {" & vbCr
        sJscript2 = sJscript2 & "enabled: showDataLabels," & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return mapKey === 'custom/world' || mapKey === 'countries/us/us-all' ?" & vbCr
        'jrs 2015/05/27 sJscript2 = sJscript2 & "(this.point.properties && this.point.properties['hc-a2']) :" & vbCr
        sJscript2 = sJscript2 & "(this.point.properties && this.point.properties['ISO_2DIGIT']) :" & vbCr
        sJscript2 = sJscript2 & "this.point.name;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jrs 2015/05/27
        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "pointFormatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return this.properties['UNSHRTNAM']+': '+this.value;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'jrs 2015/06/22 *** begin *** remove point click
        'sJscript2 = sJscript2 & "point: {" & vbCr
        'sJscript2 = sJscript2 & "events: {" & vbCr
        'sJscript2 = sJscript2 & "// On click, look for a detailed map" & vbCr
        'sJscript2 = sJscript2 & "click: function () {" & vbCr
        ''jrs 2015/05/13 sJscript2 = sJscript2 & "var key = this.key;" & vbCr
        'sJscript2 = sJscript2 & "var key = this.key.toLowerCase();" & vbCr
        'sJscript2 = sJscript2 & "CountryVal = this.value;" & vbCr
        'sJscript2 = sJscript2 & "$('#mapDropdown option').each(function () {" & vbCr
        'sJscript2 = sJscript2 & "if (this.value === 'countries/' + key.substr(0, 2) + '/' + key + '-all.js') {" & vbCr
        'sJscript2 = sJscript2 & "$('#mapDropdown').val(this.value).change();" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "});" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'jrs 2015/06/22 *** end *** remove point click
        sJscript2 = sJscript2 & "}, {" & vbCr
        sJscript2 = sJscript2 & "type:   'mapline'," & vbCr
        sJscript2 = sJscript2 & "name: ""Separators""," & vbCr
        sJscript2 = sJscript2 & "data: Highcharts.geojson(mapGeoJSON, 'mapline')," & vbCr
        sJscript2 = sJscript2 & "nullColor:  'gray'," & vbCr
        sJscript2 = sJscript2 & "showInLegend: false," & vbCr
        sJscript2 = sJscript2 & "enableMouseTracking: false" & vbCr
        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "            showDataLabels = $(""#chkDataLabels"").attr('checked');" & vbCr

        sJscript2 = sJscript2 & "        }" & vbCr

        sJscript2 = sJscript2 & "// Check whether the map is already loaded, else load it and" & vbCr
        sJscript2 = sJscript2 & "// then show it async" & vbCr
        sJscript2 = sJscript2 & "if (Highcharts.maps[mapKey]) {" & vbCr
        sJscript2 = sJscript2 & "mapReady();" & vbCr
        sJscript2 = sJscript2 & "} else {" & vbCr
        sJscript2 = sJscript2 & "$.getScript(javascriptPath, mapReady);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the variables combo" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables').change(function () {" & vbCr
        'sJscript2 = sJscript2 & "alert('inside cboVariables.change');" & vbCr
        sJscript2 = sJscript2 & "getMapValues(this.value, $('#cboScenarioSingle3').val());" & vbCr
        'sJscript2 = sJscript2 & "return false;" & vbCr
        'Check if other Variable combo has changed
        sJscript2 = sJscript2 & "if ($('#cboVariables2').val() != this.value) {" & vbCr
        'sJscript2 = sJscript2 & "$('#cboVariables2')[0].value = this.value;" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables2').val(this.value);" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables2').trigger('chosen:updated');" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables2').change();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the scenario combo" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle3').change(function () {" & vbCr
        'sJscript2 = sJscript2 & "alert('inside cboVariables2.change');" & vbCr
        sJscript2 = sJscript2 & "getMapValues($('#cboVariables').val(), this.value);" & vbCr
        'sJscript2 = sJscript2 & "return false;" & vbCr
        'Check if other scenario combo has changed
        sJscript2 = sJscript2 & "if ($('#cboScenarioSingle2').val() != this.value) {" & vbCr
        'sJscript2 = sJscript2 & "$('#cboVariables2')[0].value = this.value;" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle2').val(this.value);" & vbCr
        'sJscript2 = sJscript2 & "$('#cboScenarioSingle2').trigger('chosen:updated');" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle2').change();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the play button" & vbCr
        sJscript2 = sJscript2 & "$('#play').click(function () {" & vbCr
        sJscript2 = sJscript2 & "if (YrIdx == MaxYrIdx ){" & vbCr
        sJscript2 = sJscript2 & "YrIdx = 0;" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs2[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "$('#slider').slider({value: FirstYear + YrIdx});" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "$(""#slider"").slider(""slide"") = '';" & vbCr
        sJscript2 = sJscript2 & "myPlayer = setInterval(function () {AdvanceYear()}, 10);" & vbCr
        'sJscript2 = sJscript2 & "AdvanceYear();" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the pause button" & vbCr
        sJscript2 = sJscript2 & "$('#pause').click(function () {" & vbCr
        sJscript2 = sJscript2 & "clearInterval(myPlayer);" & vbCr

        'sJscript2 = sJscript2 & "$(""#slider"").slider(""slide"") = " & vbCr
        'sJscript2 = sJscript2 & "function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear + 1;" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs2[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the advance button" & vbCr
        sJscript2 = sJscript2 & "$('#advance').click(function () {" & vbCr
        sJscript2 = sJscript2 & "AdvanceYear();" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the regress button" & vbCr
        sJscript2 = sJscript2 & "$('#regress').click(function () {" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "YrIdx = YrIdx - 1;" & vbCr
        sJscript2 = sJscript2 & "if (YrIdx < 0 ){" & vbCr
        sJscript2 = sJscript2 & "YrIdx = 0;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs2[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "$('#slider').slider({value: FirstYear + YrIdx});" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Advance Year" & vbCr
        sJscript2 = sJscript2 & "function AdvanceYear() {" & vbCr

        'sJscript2 = sJscript2 & "if (YrIdx == MaxYrIdx - 1){" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = MaxYrIdx - 1;" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        'jrs 2015/06/16 I had to add this extra line, after the update the map would go invisible without it
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "YrIdx = YrIdx + 1;" & vbCr
        sJscript2 = sJscript2 & "if (YrIdx > MaxYrIdx ){" & vbCr
        sJscript2 = sJscript2 & "YrIdx = MaxYrIdx;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "var mydata = dataAllYrs[YrIdx];" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(mydata);" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs2[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "$('#slider').slider({value: FirstYear + YrIdx});" & vbCr
        sJscript2 = sJscript2 & "if (YrIdx == MaxYrIdx ){" & vbCr
        sJscript2 = sJscript2 & "clearInterval(myPlayer);" & vbCr
        sJscript2 = sJscript2 & "dataAllYrs2 = jQuery.extend(true, {}, dataAllYrs);" & vbCr
        'sJscript2 = sJscript2 & "$(""#slider"").slider(""slide"") = " & vbCr
        'sJscript2 = sJscript2 & "function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear + 1;" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[0].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "// Load Map Values" & vbCr
        sJscript2 = sJscript2 & "function getMapValues(sVarDesc, sSceName) {" & vbCr
        sJscript2 = sJscript2 & "$.getScript(" & vbCr
        sJscript2 = sJscript2 & "'frm_CyberDashboardData4Map.aspx?VarDesc=' + sVarDesc + '&SceName=' + sSceName ," & vbCr
        sJscript2 = sJscript2 & "function (data) {" & vbCr
        'sJscript2 = sJscript2 & "alert('hello');" & vbCr
        sJscript2 = sJscript2 & "dataAllYrs = jQuery.extend(true, {}, dataAllYrs3);" & vbCr
        sJscript2 = sJscript2 & "dataAllYrs2 = jQuery.extend(true, {}, dataAllYrs);" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs2[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "var axis = $('#containermap').highcharts().colorAxis[0];" & vbCr
        sJscript2 = sJscript2 & "axis.update({" & vbCr
        sJscript2 = sJscript2 & "dataClasses: [{" & vbCr
        sJscript2 = sJscript2 & "to: CDValLimits[1]" & vbCr
        sJscript2 = sJscript2 & "}, {"
        sJscript2 = sJscript2 & "from: CDValLimits[1],"
        sJscript2 = sJscript2 & "to: CDValLimits[2]"
        sJscript2 = sJscript2 & "}, {"
        sJscript2 = sJscript2 & "from: CDValLimits[2],"
        sJscript2 = sJscript2 & "to: CDValLimits[3]"
        sJscript2 = sJscript2 & "}, {"
        sJscript2 = sJscript2 & "from: CDValLimits[3],"
        sJscript2 = sJscript2 & "to: CDValLimits[4]"
        sJscript2 = sJscript2 & "}, {"
        sJscript2 = sJscript2 & "from: CDValLimits[4],"
        sJscript2 = sJscript2 & "to: CDValLimits[5]"
        sJscript2 = sJscript2 & "}, {"
        sJscript2 = sJscript2 & "from: CDValLimits[5]"
        sJscript2 = sJscript2 & "}]"
        sJscript2 = sJscript2 & "});"
        sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle(null, {text: sVarDesc + ' - ' + sSceName});" & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & ").fail(function(xhr, status, error){alert(error);});" & vbCr
        'sJscript2 = sJscript2 & ");" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr


        sJscript2 = sJscript2 & "// Toggle data labels - Note: Reloads map with new random data" & vbCr
        sJscript2 = sJscript2 & "$(""#chkDataLabels"").change(function () {" & vbCr
        sJscript2 = sJscript2 & "showDataLabels = $(""#chkDataLabels"").attr('checked');" & vbCr
        sJscript2 = sJscript2 & "$(""#mapDropdown"").change();" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "    // Switch to previous map on button click" & vbCr
        sJscript2 = sJscript2 & "$(""#btn-prev-map"").click(function () {" & vbCr
        sJscript2 = sJscript2 & "$(""#mapDropdown option:selected"").prev(""option"").prop(""selected"", true).change();" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "    // Switch to next map on button click" & vbCr
        sJscript2 = sJscript2 & "$(""#btn-next-map"").click(function () {" & vbCr
        sJscript2 = sJscript2 & "$(""#mapDropdown option:selected"").next(""option"").prop(""selected"", true).change();" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        'jrs 2015/05/14
        'sJscript2 = sJscript2 & "$('#containermap').height = 1400;" & vbCr
        'sJscript2 = sJscript2 & "mychartmap.renderer.image('./Images/pcl.jpg', 0, 0, 60, 60)" & vbCr
        'sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        'sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".add();" & vbCr
        'sJscript2 = sJscript2 & "var myWidth = 700, myHeight = 700;" & vbCr
        'sJscript2 = sJscript2 & "mychartmap.setSize(myWidth, myHeight);" & vbCr
        'sJscript2 = sJscript2 & "mychartmap.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "    // Trigger change event to load map on startup" & vbCr
        'sJscript2 = sJscript2 & "if (location.hash) {" & vbCr
        'sJscript2 = sJscript2 & "$('#mapDropdown').val(location.hash.substr(1) + '.js');" & vbCr
        'sJscript2 = sJscript2 & "} else { // for IE9" & vbCr
        'sJscript2 = sJscript2 & "$($('#mapDropdown option')[0]).attr('selected', 'selected');" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "$('#mapDropdown').change();" & vbCr

        'jrs 2015/05/19
        sJscript2 = sJscript2 & "function Sliderutility() {" & vbCr
        sJscript2 = sJscript2 & "$(""#slider"").slider(" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "value: " & Session("FirstYear") & "," & vbCr
        sJscript2 = sJscript2 & "min: " & Session("FirstYear") & "," & vbCr
        sJscript2 = sJscript2 & "max: " & Session("DisplayLastYear") & "," & vbCr
        sJscript2 = sJscript2 & "step: 1," & vbCr
        'sJscript2 = sJscript2 & "slide: function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear;" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        'sJscript2 = sJscript2 & "start: function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear;" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "stop: function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear;" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        sJscript2 = sJscript2 & "$('#containermap').highcharts().series[1].setData(dataAllYrs2[YrIdx]);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & ");" & vbCr
        sJscript2 = sJscript2 & "}"
        sJscript2 = sJscript2 & "Sliderutility();" & vbCr

        'JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        ''sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "};" & vbCr



        'JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        'sJscript2 = sJscript2 & "}(jQuery);" & vbCr
        sJscript2 = sJscript2 & "</script>"


        PackJSMap = sJscript2

        'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadCyberProfile", _
        '    sJscript2, False)
    End Function

    Private Function PackJSBubble() As String

        Dim sJscript2 As String

        '************* bubble chart **********************
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "function BuildBubbles(){ // encapsulate jQuery" & vbCr
        'sJscript2 = sJscript2 & "            $(function () {" & vbCr
        sJscript2 = sJscript2 & "$('#containerbubble').highcharts({" & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "type:       'bubble'," & vbCr
        sJscript2 = sJscript2 & "plotBorderWidth: 1," & vbCr
        sJscript2 = sJscript2 & "height: myHeight-185," & vbCr
        'sJscript2 = sJscript2 & "height: 508," & vbCr
        'sJscript2 = sJscript2 & "width: myHeight*1," & vbCr
        sJscript2 = sJscript2 & "zoomType:   'xy'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontFamily:  'Raleway'," & vbCr
        sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        'jrs 2015/08/24 sJscript2 = sJscript2 & "return '<span style=""color:' + this.series.color + '"">●</span>' + ' ' + this.series.name + '<br>(' + this.point.x + ', ' + this.point.y + ')';" & vbCr
        sJscript2 = sJscript2 & "return '<span style=""color:' + this.series.color + '"">●</span>' + ' ' + this.series.name;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Cyber Benefit'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 1," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "if (this.value == 15) {" & vbCr
        sJscript2 = sJscript2 & "return 'high';" & vbCr
        sJscript2 = sJscript2 & "} else if (this.value == 5) {" & vbCr
        sJscript2 = sJscript2 & "return 'low';" & vbCr
        sJscript2 = sJscript2 & "} else {" & vbCr
        sJscript2 = sJscript2 & "return '';" & vbCr
        sJscript2 = sJscript2 & " }" & vbCr
        sJscript2 = sJscript2 & " }" & vbCr
        sJscript2 = sJscript2 & " }" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Cyber Risk'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "startOnTick: false," & vbCr
        sJscript2 = sJscript2 & "endOnTick: false," & vbCr
        sJscript2 = sJscript2 & "labels: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "if (this.value == 8) {" & vbCr
        sJscript2 = sJscript2 & "return 'high';" & vbCr
        sJscript2 = sJscript2 & "} else if (this.value == 2) {" & vbCr
        sJscript2 = sJscript2 & "return 'low';" & vbCr
        sJscript2 = sJscript2 & "} else {" & vbCr
        sJscript2 = sJscript2 & "return '';" & vbCr
        sJscript2 = sJscript2 & " }" & vbCr
        sJscript2 = sJscript2 & " }" & vbCr
        sJscript2 = sJscript2 & " }" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        sJscript2 = sJscript2 & "bubble: {" & vbCr
        sJscript2 = sJscript2 & "dataLabels: {" & vbCr
        sJscript2 = sJscript2 & "enabled: true," & vbCr
        sJscript2 = sJscript2 & "formatter: function() {return this.point.name;}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "enableMouseTracking: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "series: {" & vbCr
        sJscript2 = sJscript2 & "events: {" & vbCr
        sJscript2 = sJscript2 & "click: function () {" & vbCr
        sJscript2 = sJscript2 & "showSceDesc(this.name);" & vbCr
        'Check if other scenario combo has changed
        sJscript2 = sJscript2 & "if ($('#cboScenarioSingle2').val() != this.name && this.name > '') {" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle2').val(this.name);" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle2').change();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "mouseOver: function () {" & vbCr
        sJscript2 = sJscript2 & "showSceDesc(this.name);" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "legendItemClick: function () {" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 0 To 4
            If i > 0 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToHCSeriesBB(i)
        Next

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        'sJscript2 = sJscript2 & "function(chart){" & vbCr

        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 0, 0, 60, 60)" & vbCr
        'sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        'sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".add();" & vbCr

        ''sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        ''sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        'sJscript2 = sJscript2 & "}" & vbCr
        'JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        'sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "</script>"


        PackJSBubble = sJscript2

    End Function

    Private Function PackJSArea() As String
        Dim sJscript2 As String

        '************* area chart **********************
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "var Max4Areas = [];" & vbCr
        Dim i As Integer
        For i = 1 To 6
            sJscript2 = sJscript2 & "Max4Areas[" & i.ToString & "]=" & Math.Round(Session("MaxVals4Area")(i), 2) & ";" & vbCr
        Next
        sJscript2 = sJscript2 & "   var maxidx = " & getMaxIdx() & ";" & vbCr

        sJscript2 = sJscript2 & "function BuildArea(){ // encapsulate jQuery" & vbCr
        'sJscript2 = sJscript2 & "            $(function () {" & vbCr
        sJscript2 = sJscript2 & "$('#containerarea').highcharts({" & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        If Session("optCumulative") Then
            sJscript2 = sJscript2 & "type:       'area'," & vbCr
        Else
            sJscript2 = sJscript2 & "type:       'line'," & vbCr
        End If
        sJscript2 = sJscript2 & "plotBorderWidth: 1," & vbCr
        sJscript2 = sJscript2 & "height: myHeight-235," & vbCr
        'sJscript2 = sJscript2 & "width: myHeight*1," & vbCr
        sJscript2 = sJscript2 & "zoomType:   'xy'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontFamily:  'Raleway'," & vbCr
        sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        'sJscript2 = sJscript2 & "tooltip: {" & vbCr
        'sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        'sJscript2 = sJscript2 & "return '<span style=""color:' + this.series.color + '"">●</span>' + ' ' + this.series.name + '<br>(' + this.point.x + ', ' + this.point.y + ')';" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Time'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 1" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        If Session("optCumulative") Then
            sJscript2 = sJscript2 & "text:       'Billion $'" & vbCr
        Else
            sJscript2 = sJscript2 & "text:       '% of GDP'" & vbCr
        End If
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "startOnTick: false," & vbCr
        sJscript2 = sJscript2 & "endOnTick: false," & vbCr
        sJscript2 = sJscript2 & "max: " & getMaxScale() & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        sJscript2 = sJscript2 & "area: {" & vbCr
        sJscript2 = sJscript2 & "pointStart: " & Session("FirstYear") & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "line: {" & vbCr
        sJscript2 = sJscript2 & "pointStart: " & Session("FirstYear") & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "series: {" & vbCr
        sJscript2 = sJscript2 & "events: {" & vbCr
        sJscript2 = sJscript2 & "legendItemClick: function () {" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 1 To 5
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            'jrs 2015/05/27 sJscript2 = sJscript2 & PlotColToHCSeriesAG(Session("MapHighToLow4Areas")(i))
            sJscript2 = sJscript2 & PlotColToHCSeriesAG(i)
        Next

        'Totals
        sJscript2 = sJscript2 & ", " & vbCr & PlotColToHCSeriesAGTot(1)
        sJscript2 = sJscript2 & ", " & vbCr & PlotColToHCSeriesAGTot(2)

        'Cumulatives
        For i = 1 To 5
            sJscript2 = sJscript2 & "," & vbCr
            'jrs 2015/05/27 sJscript2 = sJscript2 & PlotColToHCSeriesAG(Session("MapHighToLow4Areas")(i))
            sJscript2 = sJscript2 & PlotColToHCSeriesAGCum(i)
        Next

        'Totals
        sJscript2 = sJscript2 & ", " & vbCr & PlotColToHCSeriesAGTotCum(2)
        sJscript2 = sJscript2 & ", " & vbCr & PlotColToHCSeriesAGTotCum(1)

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        'sJscript2 = sJscript2 & "function(chart){" & vbCr

        'sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 0, 0, 60, 60)" & vbCr
        'sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        'sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".add();" & vbCr

        ''sJscript2 = sJscript2 & "chart.setSize(myWidth, myHeight);" & vbCr
        ''sJscript2 = sJscript2 & "chart.hasUserSize = null;" & vbCr


        'sJscript2 = sJscript2 & "}" & vbCr
        'JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        'sJscript2 = sJscript2 & "});" & vbCr

        'jrs 2015/05/28
        'sJscript2 = sJscript2 & "// Change the Stack option" & vbCr
        'sJscript2 = sJscript2 & "function clickCosts() {" & vbCr
        'sJscript2 = sJscript2 & "if (chkCosts.checked && chkBenefits.checked){" & vbCr
        'sJscript2 = sJscript2 & "$('#containerbubble').highcharts().options({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr
        ''chart.series[0].update({
        ''type:       Type()
        ''});
        ''             chart.series[0].update({
        ''stacking:   'normal'
        ''        });
        'sJscript2 = sJscript2 & "$('#container').highcharts().series[0].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "// Activate Cumulative option" & vbCr
        sJscript2 = sJscript2 & "$('#optCumulative').click(function () {" & vbCr
        sJscript2 = sJscript2 & "   var startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   var endidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   if ($('#optBoth').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 12;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 13;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 6;" & vbCr
        sJscript2 = sJscript2 & "   } else if ($('#optCosts').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 7;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 9;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 4;" & vbCr
        sJscript2 = sJscript2 & "   } else {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 10;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 11;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 5;" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   for (i=0; i <= 13; i++) {" & vbCr
        sJscript2 = sJscript2 & "       if (i >= startidx && i <= endidx) {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: true, showInLegend: true});" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].show();" & vbCr
        sJscript2 = sJscript2 & "       } else {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: false, showInLegend: false});" & vbCr
        sJscript2 = sJscript2 & "       }" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].axisTitle.attr({text:       'Billion $'});" & vbCr
        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].setExtremes(null, Max4Areas[maxidx]);" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate Annualized option" & vbCr
        sJscript2 = sJscript2 & "$('#optAnnualized').click(function () {" & vbCr
        sJscript2 = sJscript2 & "   var startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   var endidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   if ($('#optBoth').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 5;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 6;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 3;" & vbCr
        sJscript2 = sJscript2 & "   } else if ($('#optCosts').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 2;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 1;" & vbCr
        sJscript2 = sJscript2 & "   } else {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 3;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 4;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 2;" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   for (i=0; i <= 13; i++) {" & vbCr
        sJscript2 = sJscript2 & "       if (i >= startidx && i <= endidx) {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: true, showInLegend: true});" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].show();" & vbCr
        sJscript2 = sJscript2 & "       } else {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: false, showInLegend: false});" & vbCr
        sJscript2 = sJscript2 & "       }" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].axisTitle.attr({text:       '% of GDP'});" & vbCr
        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].setExtremes(null, Max4Areas[maxidx]);" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate Cost option" & vbCr
        sJscript2 = sJscript2 & "$('#optCosts').click(function () {" & vbCr
        sJscript2 = sJscript2 & "   var startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   var endidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   if ($('#optCumulative').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 7;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 9;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 4;" & vbCr
        sJscript2 = sJscript2 & "   } else {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 2;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 1;" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   for (i=0; i <= 13; i++) {" & vbCr
        sJscript2 = sJscript2 & "       if (i >= startidx && i <= endidx) {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: true, showInLegend: true});" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].show();" & vbCr
        sJscript2 = sJscript2 & "       } else {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: false, showInLegend: false});" & vbCr
        sJscript2 = sJscript2 & "       }" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].setExtremes(null, Max4Areas[maxidx]);" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate Benefits option" & vbCr
        sJscript2 = sJscript2 & "$('#optBenefits').click(function () {" & vbCr
        sJscript2 = sJscript2 & "   var startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   var endidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   if ($('#optCumulative').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 10;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 11;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 5;" & vbCr
        sJscript2 = sJscript2 & "   } else {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 3;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 4;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 2;" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   for (i=0; i <= 13; i++) {" & vbCr
        sJscript2 = sJscript2 & "       if (i >= startidx && i <= endidx) {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: true, showInLegend: true});" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].show();" & vbCr
        sJscript2 = sJscript2 & "       } else {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: false, showInLegend: false});" & vbCr
        sJscript2 = sJscript2 & "       }" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].setExtremes(null, Max4Areas[maxidx]);" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate Both option" & vbCr
        sJscript2 = sJscript2 & "$('#optBoth').click(function () {" & vbCr
        sJscript2 = sJscript2 & "   var startidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   var endidx = 0;" & vbCr
        sJscript2 = sJscript2 & "   if ($('#optCumulative').is(':checked')) {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 12;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 13;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 6;" & vbCr
        sJscript2 = sJscript2 & "   } else  {" & vbCr
        sJscript2 = sJscript2 & "       startidx = 5;" & vbCr
        sJscript2 = sJscript2 & "       endidx = 6;" & vbCr
        sJscript2 = sJscript2 & "       maxidx = 3;" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   for (i=0; i <= 13; i++) {" & vbCr
        sJscript2 = sJscript2 & "       if (i >= startidx && i <= endidx) {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: true, showInLegend: true});" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].show();" & vbCr
        sJscript2 = sJscript2 & "       } else {" & vbCr
        sJscript2 = sJscript2 & "           $('#containerarea').highcharts().series[i].update({visible: false, showInLegend: false});" & vbCr
        sJscript2 = sJscript2 & "       }" & vbCr
        sJscript2 = sJscript2 & "   }" & vbCr

        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].setExtremes(null, Max4Areas[maxidx]);" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        'jrs 2015/08/07 ajax begins ***
        sJscript2 = sJscript2 & "// Load Area Values" & vbCr
        sJscript2 = sJscript2 & "function getAreaValues(SceName, RegName) {" & vbCr
        sJscript2 = sJscript2 & "$.getScript(" & vbCr
        sJscript2 = sJscript2 & "'frm_CyberDashboardData4Area.aspx?SceName=' + SceName + ' &RegName=' + RegName," & vbCr
        sJscript2 = sJscript2 & "function (data) {" & vbCr
        'sJscript2 = sJscript2 & "alert('hello');" & vbCr
        sJscript2 = sJscript2 & "for (i=1; i<15; i++) {" & vbCr
        sJscript2 = sJscript2 & "$('#containerarea').highcharts().series[i-1].setData(dataAllSeriesArea[i].data);" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        'jrs 2015/08/25 update max values
        sJscript2 = sJscript2 & "for (i=1; i<7; i++) {" & vbCr
        sJscript2 = sJscript2 & "Max4Areas[i]=Max4Areas2[i];" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "   $('#containerarea').highcharts().yAxis[0].setExtremes(null, Max4Areas[maxidx]);" & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & ").fail(function(xhr, status, error){alert(error);});" & vbCr
        'sJscript2 = sJscript2 & ");" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "// Activate the variables combo" & vbCr
        sJscript2 = sJscript2 & "$('#cboRegionSingle').change(function () {" & vbCr
        'sJscript2 = sJscript2 & "alert('inside cboVariables2.change');" & vbCr
        sJscript2 = sJscript2 & "getAreaValues($('#cboScenarioSingle2').val(), this.value);" & vbCr
        'sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the scenario combo" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle2').change(function () {" & vbCr
        'sJscript2 = sJscript2 & "alert('inside cboVariables2.change');" & vbCr
        sJscript2 = sJscript2 & "getAreaValues(this.value, $('#cboRegionSingle').val());" & vbCr
        'sJscript2 = sJscript2 & "return false;" & vbCr
        'Check if other scenario combo has changed
        sJscript2 = sJscript2 & "if ($('#cboScenarioSingle3').val() != this.value) {" & vbCr
        'sJscript2 = sJscript2 & "$('#cboVariables2')[0].value = this.value;" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle3').val(this.value);" & vbCr
        'sJscript2 = sJscript2 & "$('#cboScenarioSingle3').trigger('chosen:updated');" & vbCr
        sJscript2 = sJscript2 & "$('#cboScenarioSingle3').change();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        'jrs 2015/08/07 ajax ends ***


        sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "</script>"

        PackJSArea = sJscript2

    End Function

    Private Function PackJSLine(ByVal idx As Integer) As String
        Dim sJscript2 As String

        '************* line chart **********************
        sJscript2 = "<script>" & vbCr
        sJscript2 = sJscript2 & "function BuildLine(){ // encapsulate jQuery" & vbCr
        'sJscript2 = sJscript2 & "            $(function () {" & vbCr
        sJscript2 = sJscript2 & "$('#containerline').highcharts({" & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "plotBorderWidth: 1," & vbCr
        sJscript2 = sJscript2 & "height: myHeight-265," & vbCr
        'sJscript2 = sJscript2 & "width: myHeight*1," & vbCr
        sJscript2 = sJscript2 & "zoomType:   'xy'," & vbCr
        sJscript2 = sJscript2 & "style: {" & vbCr
        sJscript2 = sJscript2 & "fontFamily:  'Raleway'," & vbCr
        sJscript2 = sJscript2 & "fontSize:   '18px'," & vbCr
        sJscript2 = sJscript2 & "fontWeight: 'bold'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Year'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 1" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       '" & getYAxisLabel(idx) & "'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "startOnTick: false," & vbCr
        sJscript2 = sJscript2 & "endOnTick: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        sJscript2 = sJscript2 & "line: {" & vbCr
        sJscript2 = sJscript2 & "pointStart: " & Session("FirstYear") & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        Dim sReg As String, sSce As String
        For i = 1 To Session("CDValuesLineCOUNTE")
            sReg = Session("CDValuesLineDim1")(i)
            sSce = Session("CDValuesLineSce")(i)
            If i > 1 Then
                sJscript2 = sJscript2 & "," & vbCr
            End If
            sJscript2 = sJscript2 & PlotColToHCSeries(sReg, sSce, i)
        Next

        sJscript2 = sJscript2 & "]" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr

        'jrs 2015/08/07 ajax begins ***
        sJscript2 = sJscript2 & "// Load Line Values" & vbCr
        sJscript2 = sJscript2 & "function getLineValues(sVarDesc, SceIDs, RegIDs) {" & vbCr
        sJscript2 = sJscript2 & "$.getScript(" & vbCr
        sJscript2 = sJscript2 & "'frm_CyberDashboardData4Line.aspx?VarDesc=' + sVarDesc + '&SceIDs=' + SceIDs + ' &RegIDs=' + RegIDs," & vbCr
        sJscript2 = sJscript2 & "function (data) {" & vbCr
        'sJscript2 = sJscript2 & "alert('hello');" & vbCr
        sJscript2 = sJscript2 & "while($('#containerline').highcharts().series.length > 0)" & vbCr
        sJscript2 = sJscript2 & "$('#containerline').highcharts().series[0].remove(true);" & vbCr

        sJscript2 = sJscript2 & "for (i=0; i<LineCounte; i++) {" & vbCr
        sJscript2 = sJscript2 & "$('#containerline').highcharts().addSeries({" & vbCr
        sJscript2 = sJscript2 & "name: dataAllSeriesLine[i][0].name," & vbCr
        sJscript2 = sJscript2 & "color: dataAllSeriesLine[i][0].color," & vbCr
        sJscript2 = sJscript2 & "data: dataAllSeriesLine[i][0].data" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "$('#containerline').highcharts().yAxis[0].axisTitle.attr({text: VarUnits});" & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & ").fail(function(xhr, status, error){alert(error);});" & vbCr
        'sJscript2 = sJscript2 & ");" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "// Activate the variables combo" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables2').change(function () {" & vbCr
        'sJscript2 = sJscript2 & "alert('inside cboVariables2.change');" & vbCr
        sJscript2 = sJscript2 & "var xSceIDs = $(""#lstSceMultiple"").jqxComboBox('getSelectedItems');" & vbCr
        sJscript2 = sJscript2 & "var SceIDs='';" & vbCr
        sJscript2 = sJscript2 & "$.each(xSceIDs, function (index) {" & vbCr
        sJscript2 = sJscript2 & "SceIDs += this.label;" & vbCr
        sJscript2 = sJscript2 & "if (xSceIDs.length - 1 != index) {" & vbCr
        sJscript2 = sJscript2 & "SceIDs += ',';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "var xRegIDs = $(""#lstRegionsMultiple"").jqxComboBox('getSelectedItems');" & vbCr
        sJscript2 = sJscript2 & "var RegIDs='';" & vbCr
        sJscript2 = sJscript2 & "$.each(xRegIDs, function (index) {" & vbCr
        sJscript2 = sJscript2 & "RegIDs += this.label;" & vbCr
        sJscript2 = sJscript2 & "if (xRegIDs.length - 1 != index) {" & vbCr
        sJscript2 = sJscript2 & "RegIDs += ',';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "getLineValues(this.value, SceIDs, RegIDs);" & vbCr
        'sJscript2 = sJscript2 & "return false;" & vbCr
        'Check if other Variable combo has changed
        sJscript2 = sJscript2 & "if ($('#cboVariables').val() != this.value) {" & vbCr
        'sJscript2 = sJscript2 & "$('#cboVariables')[0].value = this.value;" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables').val(this.value);" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables').trigger('chosen:updated');" & vbCr
        sJscript2 = sJscript2 & "$('#cboVariables').change();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr


        sJscript2 = sJscript2 & "// Activate the refresh button" & vbCr
        sJscript2 = sJscript2 & "$('#cmdRefresh').click(function () {" & vbCr
        'sJscript2 = sJscript2 & "alert('inside cboVariables2.change');" & vbCr
        sJscript2 = sJscript2 & "var xSceIDs = $(""#lstSceMultiple"").jqxComboBox('getSelectedItems');" & vbCr
        sJscript2 = sJscript2 & "var SceIDs='';" & vbCr
        sJscript2 = sJscript2 & "$.each(xSceIDs, function (index) {" & vbCr
        sJscript2 = sJscript2 & "SceIDs += this.label;" & vbCr
        sJscript2 = sJscript2 & "if (xSceIDs.length - 1 != index) {" & vbCr
        sJscript2 = sJscript2 & "SceIDs += ',';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "if (SceIDs == '') {" & vbCr
        sJscript2 = sJscript2 & "alert('You must select at least one scenario.');" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "var xRegIDs = $(""#lstRegionsMultiple"").jqxComboBox('getSelectedItems');" & vbCr
        sJscript2 = sJscript2 & "var RegIDs='';" & vbCr
        sJscript2 = sJscript2 & "$.each(xRegIDs, function (index) {" & vbCr
        sJscript2 = sJscript2 & "RegIDs += this.label;" & vbCr
        sJscript2 = sJscript2 & "if (xRegIDs.length - 1 != index) {" & vbCr
        sJscript2 = sJscript2 & "RegIDs += ',';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "if (RegIDs == '') {" & vbCr
        sJscript2 = sJscript2 & "alert('You must select at least one country or Group.');" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "getLineValues($('#cboVariables2').val(), SceIDs, RegIDs);" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        'jrs 2015/08/07 ajax ends ***

        sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "</script>"

        PackJSLine = sJscript2

    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Or Session("CDVarName") Is Nothing Then
            Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
            'jrs 2015/05/20 Temporary, we'll remove it once we have the file combo box
            'Dim sFile As String = Session("PutFdrvRun2") & "\DBtest.run"
            'Session("IFsRunFiles").Add(sFile, True)

            ReDim Session("CostBenefitIdx")(5)
            Session("CostBenefitIdx")(1) = "ICTCYBSECSPEND"
            Session("CostBenefitIdx")(2) = "ICTCYBEVTCOST"
            Session("CostBenefitIdx")(3) = "ICTCYBOPCOST"
            Session("CostBenefitIdx")(4) = "ICTCYBBENEFIT"
            Session("CostBenefitIdx")(5) = "ICTCONSURPLUS"

            Dim SceShortNames(4) As String, i As Integer
            SceShortNames(0) = "Base Case"
            SceShortNames(1) = "Clockwork Orange"
            SceShortNames(2) = "Independent Internet"
            SceShortNames(3) = "Leviathan"
            SceShortNames(4) = "Shangri-La"

            'lstSceMultiple.Items.Clear()
            cboScenarioSingle2.Items.Clear()
            cboScenarioSingle3.Items.Clear()

            ReDim Session("CDSceFileNames")(4)
            'lstSceMultiple.Items.Add("")
            If Session("mnuSQLite") = "True" Then
                Session("CDSceFileNames")(0) = Session("PutFdrvRun2") & "\ifsBase.run.db"
            Else
                Session("CDSceFileNames")(0) = Session("PutFdrvRun2") & "\ifsBase.run"
            End If
            For i = 0 To 4
                If i > 0 Then
                    If Session("mnuSQLite") = "True" Then
                        Session("CDSceFileNames")(i) = Session("PutFdrvRun2") & "\" & SceShortNames(i) & ".run.db"
                    Else
                        Session("CDSceFileNames")(i) = Session("PutFdrvRun2") & "\" & SceShortNames(i) & ".run"
                    End If
                End If
                'lstSceMultiple.Items.Add(SceShortNames(i))
                cboScenarioSingle2.Items.Add(SceShortNames(i))
                cboScenarioSingle3.Items.Add(SceShortNames(i))
            Next

            If Not (Session("CDSceName") Is Nothing) Then
                cboScenarioSingle2.SelectedValue = Session("CDSceName")
                cboScenarioSingle3.SelectedValue = Session("CDSceName")
            End If

            ReDim Preserve Session("CDSces")(4)
            'If Not (Session("CDSces")(0) Is Nothing) Then
            '    For i = 1 To lstSceMultiple.Items.Count
            '        lstSceMultiple.Items(i - 1).Selected = Session("CDSces")(i - 1)
            '    Next
            'End If

            ReDim Preserve Session("vShortSce")(9)
            ReDim Preserve Session("RunFiles")(9)
            For i = 1 To 4
                Session("RunFiles")(i + 5) = Session("CDSceFileNames")(i)
                Session("vShortSce")(i + 5) = Replace(Replace(Session("CDSceFileNames")(i), Session("PutFdrvRun2") & "\", ""), ".run", "")
            Next

            'jrs 2015/06/18
            'Load Groups and Countries
            'lstRegionsMultiple.Items.Clear()
            'lstRegionsMultiple.Items.Add("")
            'lstRegionsMultiple.Items.Add("World")

            'lstRegionsMultiple.Items.Add("High-income countries")
            'lstRegionsMultiple.Items.Add("Upper-middle-income countries")
            'lstRegionsMultiple.Items.Add("Lower-middle-income countries")
            'lstRegionsMultiple.Items.Add("Low-income countries")

            'lstRegionsMultiple.Items.Add("East Asia and Pacific")
            'lstRegionsMultiple.Items.Add("Europe and Central Asia")
            'lstRegionsMultiple.Items.Add("Latin America and the Caribbean")
            'lstRegionsMultiple.Items.Add("Middle East and North Africa")
            'lstRegionsMultiple.Items.Add("South Asia")
            'lstRegionsMultiple.Items.Add("Sub-Saharan Africa")

            'For i = 1 To NR
            '    lstRegionsMultiple.Items.Add(GRPS(1, i))
            'Next

            'ReDim Preserve Session("CDRegs")(lstRegionsMultiple.Items.Count - 1)
            ReDim Preserve Session("CDRegs")(NR + 11)
            'If Not (Session("CDRegs")(0) Is Nothing) Then
            '    For i = 1 To lstRegionsMultiple.Items.Count
            '        lstRegionsMultiple.Items(i - 1).Selected = Session("CDRegs")(i - 1)
            '    Next
            'End If

            cboRegionSingle.Items.Clear()
            cboRegionSingle.Items.Add("")
            cboRegionSingle.Items.Add("World")

            cboRegionSingle.Items.Add("High-income countries")
            cboRegionSingle.Items.Add("Upper-middle-income countries")
            cboRegionSingle.Items.Add("Lower-middle-income countries")
            cboRegionSingle.Items.Add("Low-income countries")

            cboRegionSingle.Items.Add("East Asia and Pacific")
            cboRegionSingle.Items.Add("Europe and Central Asia")
            cboRegionSingle.Items.Add("Latin America and the Caribbean")
            cboRegionSingle.Items.Add("Middle East and North Africa")
            cboRegionSingle.Items.Add("South Asia")
            cboRegionSingle.Items.Add("Sub-Saharan Africa")

            For i = 1 To NR
                cboRegionSingle.Items.Add(GRPS(1, i))
            Next

            Dim IFsGroupAlias As Dictionary(Of String, String) = New Dictionary(Of String, String)
            IFsGroupAlias.Add("High-income countries", "WB High-Income Economies")
            IFsGroupAlias.Add("Upper-middle-income countries", "WB UpMidIncome Economies")
            IFsGroupAlias.Add("Lower-middle-income countries", "WB LowMidIncome Economies")
            IFsGroupAlias.Add("Low-income countries", "WB Low-Income Economies")

            IFsGroupAlias.Add("East Asia and Pacific", "WB E Asia&Pac Developing")
            IFsGroupAlias.Add("Europe and Central Asia", "WB Eur&CenAsia Developing")
            IFsGroupAlias.Add("Latin America and the Caribbean", "WB LatAm&Car Developing")
            IFsGroupAlias.Add("Middle East and North Africa", "WB MdEast&NAf Developing")
            IFsGroupAlias.Add("South Asia", "WB South Asia")
            IFsGroupAlias.Add("Sub-Saharan Africa", "WB Afr-SubSahar")

            Session("IFsGroupAlias") = IFsGroupAlias

            'ReDim Preserve Session("CDRegs")(lstRegionsMultiple.Items.Count - 1)

            'If Not (Session("CDRegs")(0) Is Nothing) Then
            '    For i = 1 To lstRegionsMultiple.Items.Count
            '        lstRegionsMultiple.Items(i - 1).Selected = Session("CDRegs")(i - 1)
            '    Next
            'End If

            'Load Variables
            Dim db As String, Sql As String, valtbl As DataTable, VarDefinition As String
            db = Session("PutFdrvRun") & "IFSVAR.mdb"
            Sql = "SELECT NAME, DEFINITION, [Parameter Explanation] FROM IFSVAR WHERE TYPE=1 AND NAME IN ('ICTCONSURPLUS', 'ICTCYBBENEFIT', 'ICTCYBBENEFITNET', 'ICTCYBBENTOT'," &
                "'ICTCYBBENTOTCUM', 'ICTCYBOPCOST', 'ICTCYBSECSPEND', 'ICTCYBEVTCOST', 'ICTCYBRISKTOT', 'ICTCYBRISKTOTCUM', 'ICTCYBBENEFITCUMNET')" &
                "ORDER BY DEFINITION"
            SetDataTablefromAccess(db, Sql, "", valtbl)
            cboVariables.Items.Add("")
            cboVariables2.Items.Add("")
            For i = 0 To valtbl.Rows.Count - 1
                VarDefinition = Replace(Replace(valtbl.Rows(i).Item("Definition"), "   ", " "), "  ", " ")
                VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")
                cboVariables.Items.Add(VarDefinition)
                cboVariables2.Items.Add(VarDefinition)
            Next

        End If

        PackJS()

        'JAM 2015-07-11 Adding Javascript generation
        If Not CBool(DevelopmentMode) Then
            cmdGeneratejs.Visible = False
        End If
    End Sub

    Private Sub LoadCD4Map(ByRef VarIdx)

        Session("MaxCDValues") = -5000
        Session("MinCDValues") = 5000
        Dim Dim2Idx As Integer = -1, Dim3Idx As Integer = -1
        Dim i As Integer
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For i = 1 To NR
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(Session("CDVarName"), VarIdx, GRPS(1, i), i, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                Session("PutFdrvRun") & Session("CDSceName") & ".run.db", Session("CDSceIdx"), i = 1, i = NR, False, 1, Session("CDValues"), i, 1, Session("FirstYear"), Session("DisplayLastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            Else
                gLoadForDisplay(Session("CDVarName"), VarIdx, GRPS(1, i), i, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                    Session("PutFdrvRun") & Session("CDSceName") & ".run", Session("CDSceIdx"), i = 1, i = NR, False, 1, Session("CDValues"), i, 1, Session("FirstYear"), Session("DisplayLastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
            'jrs 2015/05/10 check only first and last values
            'Max
            If Session("CDValues")(1, i) > Session("MaxCDValues") Then
                Session("MaxCDValues") = Session("CDValues")(1, i)
            End If
            If Session("CDValues")(Session("FinYear")(i), i) > Session("MaxCDValues") Then
                Session("MaxCDValues") = Session("CDValues")(Session("FinYear")(i), i)
            End If
            'Min
            If Session("CDValues")(1, i) < Session("MinCDValues") Then
                Session("MinCDValues") = Session("CDValues")(1, i)
            End If
            If Session("CDValues")(Session("FinYear")(i), i) < Session("MinCDValues") Then
                Session("MinCDValues") = Session("CDValues")(Session("FinYear")(i), i)
            End If
        Next
        Session("VarIdx") = VarIdx
    End Sub

    Private Sub LoadCD4Lines(ByRef VarIdx As Integer)
        Dim Dim2Idx As Integer = -1, Dim3Idx As Integer = -1
        Dim sDim1 As String
        'Need to handle
        'Multiple Regions and Multiple Scenarios
        Dim j As Integer, k As Integer, m As Integer, i As Integer
        Dim bGrp As Boolean = False
        Session("CDValuesLineCOUNTE") = 0
        For j = 1 To 5 'Number of Scenarios
            'Check if scenario is selected
            If Session("CDSces")(j - 1) Then
                For i = 2 To NR + 12 'for each Region/Group selected
                    If Session("CDRegs")(i - 1) Then
                        If i > 12 Then
                            k = i - 12
                            If k < 0 Then
                                bGrp = True
                            Else
                                bGrp = False
                            End If
                            sDim1 = GRPS(1, k)
                        Else
                            k = -1
                            bGrp = True
                            If i = 2 Then
                                sDim1 = "World"
                            Else
                                sDim1 = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Values(i - 3)
                            End If
                        End If

                        Session("CDValuesLineCOUNTE") = Session("CDValuesLineCOUNTE") + 1
                        If i > 12 Or i = 2 Then
                            Session("CDValuesLineDim1")(Session("CDValuesLineCOUNTE")) = sDim1
                        Else
                            Session("CDValuesLineDim1")(Session("CDValuesLineCOUNTE")) = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Keys(i - 3)
                        End If
                        If j = 1 Then
                            Session("CDValuesLineSce")(Session("CDValuesLineCOUNTE")) = "Base Case"
                        Else
                            Session("CDValuesLineSce")(Session("CDValuesLineCOUNTE")) = Session("vShortSce")(j + 4)
                        End If

                        'Check if this is the preselected scenario and it's a region 
                        If (j + 4 = Session("CDSceIdx") Or (j = 1 And Session("CDSceIdx") = 1)) And k > 0 Then
                            'Load from Session("CDValues")
                            For m = 1 To Session("LastYear") - Session("FirstYear") + 1
                                Session("CDValuesLine")(m, Session("CDValuesLineCOUNTE")) = Session("CDValues")(m, k)
                            Next
                        Else
                            Dim iFile As Integer
                            If j = 1 Then
                                iFile = 1
                            Else
                                iFile = j + 4
                            End If
                            If Session("mnuSQLite") = "True" Then
                                gLoadForDisplayNET(Session("CDVarName"), VarIdx, sDim1, k, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                                Session("CDSceFileNames")(j - 1), iFile, True, True, bGrp, 1, Session("CDValuesLine"), Session("CDValuesLineCOUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                            Else
                                gLoadForDisplay(Session("CDVarName"), VarIdx, sDim1, k, Session("CDDim2"), Dim2Idx, Session("CDDim3"), Dim3Idx,
                                    Session("CDSceFileNames")(j - 1), iFile, True, True, bGrp, 1, Session("CDValuesLine"), Session("CDValuesLineCOUNTE"), 1, Session("FirstYear"), Session("LastYear"), 0, False,
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                            End If
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub LoadSetCB()
        'jrs 2015/05/19
        'Dim cybGDP(0, 0) As Single
        ReDim Session("cybGDP")(Session("LastYear") - Session("FirstYear") + 1)
        'jrs 2015/08/25
        ReDim Session("cybGDPCO")(Session("LastYear") - Session("FirstYear") + 1)
        ReDim Session("cybGDPSL")(Session("LastYear") - Session("FirstYear") + 1)
        Dim GDPCount As Integer
        GDPCount = 0

        Dim r As Integer, T As Integer, g As Integer, y As Integer, v As Integer
        'Dim dbM As Database
        Dim bGrp As Boolean = False
        Dim sDim1 As String
        Dim iDim1 As Integer

        Dim cf As Integer
        cf = 0


        'just for one region
        If Not CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).ContainsKey(Session("CDDim1")) Then
            r = IFsRegs.Item(Session("CDDim1")).Index
            If r = -1 Then
                bGrp = True
                r = gGetGroupIdx(Session("CDDim1"), True, Me)
            End If
            sDim1 = Session("CDDim1") 'lstDim1.List(r)
            iDim1 = r '+ 1
        Else
            sDim1 = CType(Session("IFsGroupAlias"), Dictionary(Of String, String)).Item(Session("CDDim1"))
            r = gGetGroupIdx(sDim1, True, Me)
            iDim1 = r
            bGrp = True
        End If

        'for later: consider g-lists and decomposed groups
        'Select Case True
        '    Case mnuReg.Checked
        '        bGrp = False
        '    Case mnuGroups.Checked
        '        bGrp = True
        'End Select

        'only one file
        Dim f As Integer = Session("CDSceIdx")
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(1), 1)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(2), 2)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(3), 3)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(4), 4)
        LoadCyberVar(f, sDim1, iDim1, bGrp, Session("CostBenefitIdx")(5), 5)

        'cf = cf + 1
        'ReDim Preserve CountryFileNames(cf)
        'CountryFileNames(cf) = sDim1 & "[" & getShortFileName(COUNTE) & "]"

        ''jrs 2015/05/19
        'GDPCount = GDPCount + 1
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
            Session("PutFdrvRun") & Session("CDSceName") & ".run.db", f, True, True, bGrp, 1, Session("cybGDP"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
            Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Else
            gLoadForDisplay("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                Session("PutFdrvRun") & Session("CDSceName") & ".run", f, True, True, bGrp, 1, Session("cybGDP"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        'jrs 2015/08/25
        Dim i As Integer
        If f <> 6 Then
            'jam 2023/10/28 When Sqlite flag is True
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                Session("PutFdrvRun") & "Clockwork Orange.run.db", 6, True, True, bGrp, 1, Session("cybGDPCO"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                    Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("cybGDPCO"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Else 'simply copy
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("cybGDPCO")(i) = Session("cybGDP")(i)
            Next
        End If

        If f <> 9 Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                Session("PutFdrvRun") & "Shangri-La.run.db", 9, True, True, bGrp, 1, Session("cybGDPSL"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay("GDP", GDPidx, sDim1, iDim1, "", -1, "", -1,
                    Session("PutFdrvRun") & "Shangri-La.run", 9, True, True, bGrp, 1, Session("cybGDPSL"), 1, 1, Session("FirstYear"), Session("LastYear"), 0, True,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Else 'simply copy
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("cybGDPSL")(i) = Session("cybGDP")(i)
            Next
        End If

        'Save Map to order from higher to lower on last year
        'Create an array of columns
        'Dim PlotRank() As DisplayColumn
        'ReDim PlotRank(5)
        'Dim i As Integer

        'For i = 1 To 5
        '    PlotRank(i) = New DisplayColumn
        '    PlotRank(i).Index = i
        '    PlotRank(i).Value = Session("CDValuesAG")(Session("DisYear"), i)
        '    'jrs 2010/02/03 to break ties
        '    PlotRank(i).Value2 = Session("CDValuesAG")(1, i)
        '    'jrs 2011/07/04 save original order
        '    PlotRank(i).Index2 = i
        'Next i
        ''Sort the array
        'QuickSortObj(PlotRank, 1, 5, False, False)
        'ReDim Session("MapHighToLow4Areas")(5)
        'For i = 1 To 5
        '    Session("MapHighToLow4Areas")(i) = PlotRank(i).Index
        'Next

        'jrs 2015/08/25
        LoadMaxScale()
    End Sub

    Private Sub LoadSetCXP()
        Dim iFile As Integer
        Dim r As Integer, T As Integer, v As Integer
        Dim dbM As String
        Dim sDim1 As String
        Dim iDim1 As Integer

        Dim cf As Integer
        cf = 0


        Dim idx As Integer, sVar As String

        'jrs 2015/02/17
        Dim y As Integer
        Dim COUNTE As Integer = 0
        ReDim Session("CDValuesBB")(Session("LastYear") - Session("FirstYear") + 1, 10)
        'If lstFiles.Selected(0) Then
        '    LoadSetCXPSCE()
        'End If

        'loop through variables
        For v = 1 To 2
            If v = 1 Then
                sVar = "ICTCYBRISKTOT"
            Else
                sVar = "ICTINDEX"
            End If
            idx = IfsVars.Item(sVar).Index
            'just one file
            'For f = 0 To lstFiles.ListCount - 1
            iFile = 1
            'loop through Scenarios
            iDim1 = -1
            For r = 0 To 4
                sDim1 = "World"
                COUNTE = COUNTE + 1
                If r = 0 Then
                    iFile = 1
                Else
                    iFile = r + 5
                End If
                If Session("mnuSQLite") = "True" Then
                    gLoadForDisplayNET(sVar, idx, sDim1, iDim1, "", -1, "", -1,
                    Session("CDSceFileNames")(r), iFile, True, True,
                    True, 1, Session("CDValuesBB"), COUNTE, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                Else
                    gLoadForDisplay(sVar, idx, sDim1, iDim1, "", -1, "", -1,
                                            Session("CDSceFileNames")(r), iFile, True, True,
                                            True, 1, Session("CDValuesBB"), COUNTE, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                                            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                                            Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
            Next r
            'Next f
        Next v
    End Sub

    Private Sub LoadCyberVar(ByVal iFile As Integer,
ByVal sReg As String, ByVal iReg As Integer, ByVal bGrp As Boolean, ByVal sVar As String, ByVal iCol As Integer)
        Dim i As Integer, j As Integer
        'COUNTE = COUNTE + 1
        Dim VarIdx As Integer
        VarIdx = IfsVars.Item(sVar).Index

        'If sVar = "ICTCYBEVTCOST" And iFile = 0 Then
        '    'Need to compute prob x cost across the 12 cats
        '    EstimateEVTCOST()
        '    GR1N(COUNTE) = sReg
        '    FDNG1(COUNTE) = GR1N$(COUNTE)
        'ElseIf sVar = "ictcybevprob" Or sVar = "ictcybevcost" Then  'Add logic for Prob or Cost
        '    'Load all dimensions
        '    COUNTE = COUNTE - 1
        '    If iFile = 0 Then
        '        LoadSetSCE sVar
        '    Else
        '        For i = 1 To MAXCOM(VDim1(VarIdx)) - 1
        '            For j = 1 To MAXCOM(VDim2(VarIdx)) - 1
        '                COUNTE = COUNTE + 1
        '                gLoadForDisplay(sVar, VarIdx, "", i, "", j, "", -1, "", iFile, (i = 1 And j = 1), (i = MAXCOM(VDim1(VarIdx)) - 1 And j = MAXCOM(VDim2(VarIdx)) - 1), False, 1, PLOT, COUNTE, _
        '                1, FirstYear, LastYear, 0, False, "", True)
        '            Next j
        '        Next i
        '    End If
        'Else
        Dim sDim2 As String
        sDim2 = ""
        If Session("mnuSQLite") = "True" Then
            gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
            Session("PutFdrvRun") & Session("CDSceName") & ".run.db", iFile, True, True, bGrp, 1, Session("CDValuesAG"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
            Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
            Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Else
            gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                Session("PutFdrvRun") & Session("CDSceName") & ".run", iFile, True, True, bGrp, 1, Session("CDValuesAG"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        End If
        If iFile <> 6 Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                Session("PutFdrvRun") & "Clockwork Orange.run.db", 6, True, True, bGrp, 1, Session("CDValuesAGCO"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                    Session("PutFdrvRun") & "Clockwork Orange.run", 6, True, True, bGrp, 1, Session("CDValuesAGCO"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Else
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("CDValuesAGCO")(i, iCol) = Session("CDValuesAG")(i, iCol)
            Next
        End If

        If iFile <> 9 Then
            If Session("mnuSQLite") = "True" Then
                gLoadForDisplayNET(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                Session("PutFdrvRun") & "Shangri-La.run.db", 9, True, True, bGrp, 1, Session("CDValuesAGSL"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            Else
                gLoadForDisplay(sVar, VarIdx, sReg, iReg, sDim2, -1, "", -1,
                                    Session("PutFdrvRun") & "Shangri-La.run", 9, True, True, bGrp, 1, Session("CDValuesAGSL"), iCol, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                                    Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                                    Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
            End If
        Else
            For i = 1 To Session("LastYear") - Session("FirstYear") + 1
                Session("CDValuesAGSL")(i, iCol) = Session("CDValuesAG")(i, iCol)
            Next
        End If

        'End If
    End Sub

    Private Function PrepareDataForMap(ByVal yrIdx As Integer) As String
        Dim jScript As String = "", i As Integer
        Dim iRound As Integer = 4
        Select Case ABS_(Session("MaxCDValues"))
            Case Is > 1000
                iRound = 0
            Case Is > 100
                iRound = 1
            Case Is > 10
                iRound = 2
            Case Is > 1
                iRound = 3
            Case Else
                iRound = 4
        End Select
        Session("iRound") = iRound
        For i = 1 To NR
            If i > 1 Then
                jScript = jScript & "," & vbCr
            End If
            jScript = jScript & "{" & vbCr
            'jrs 2015/05/13 jScript = jScript & """code"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """key"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """value"": " & Math.Round(Session("CDValues")(yrIdx, i), iRound) & vbCr
            jScript = jScript & "}"
        Next
        jScript = jScript & vbCr
        PrepareDataForMap = jScript
    End Function

    Private Function PlotColToHCSeriesBB(ByVal i As Integer) As String

        Dim Interval As Integer = 21
        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & getSeriesName(i) & "',"
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = "", sColor2 As String
        If i < 9 Then
            Select Case i
                Case 0
                    sColor = "#ffffb2" '"Yellow"
                    sColor2 = "#ffff00"
                Case 1
                    sColor = "#ffcccc" '"Red"
                    sColor2 = "#ff0000"
                Case 2
                    sColor = "#ccccff" '"Blue"
                    sColor2 = "#0000ff"
                Case 3
                    'blue violet
                    sColor = "#dbbff6" '"rgba(138,43,226,0.5)"
                    sColor2 = "#8A2BE2"
                Case 4
                    sColor = "#b2ffb2" '"Lime"
                    sColor2 = "#00ff00"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor2 & "'," & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear") Step Interval
            If Not IsDBNull(Session("CDValuesBB")(j, i + 1)) Then
                If IsNumeric(Session("CDValuesBB")(j, i + 1)) And IsNumeric(Session("CDValuesBB")(j, i + 6)) Then
                    sJscript = sJscript & "{name: '" & CStr(Session("FirstYear") + j - 1) & "', x: " & CStr(Math.Round(Session("CDValuesBB")(j, i + 6), iNumDec)) & ", y: "
                    sJscript = sJscript & CStr(Math.Round(Session("CDValuesBB")(j, i + 1), iNumDec))
                    'jrs 2015/05/21
                    If j = 1 Then
                        sJscript = sJscript & ", color: '#D3D3D3'"
                    ElseIf j < Session("DisYear") Then
                        sJscript = sJscript & ", color: '" & sColor & "'"
                    End If
                    sJscript = sJscript & ", z: 1}"

                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        Next

        If j - Interval <> Session("DisYear") Then
            j = Session("DisYear")
            If Not IsDBNull(Session("CDValuesBB")(j, i + 1)) Then
                If IsNumeric(Session("CDValuesBB")(j, i + 1)) Then
                    sJscript = sJscript & "{name: '" & CStr(Session("FirstYear") + j - 1) & "', x: " & CStr(Session("CDValuesBB")(j, i + 6)) & ", y: "
                    sJscript = sJscript & CStr(Session("CDValuesBB")(j, i + 1))
                    sJscript = sJscript & ", color: '" & sColor2 & "'"
                    sJscript = sJscript & ", z: 1}"
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        End If
        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr
        'jrs 2015/05/21
        'Add a 2nd series for the line
        Dim sName2 As String
        sName2 = Replace(sName, "',", "2',")
        sName2 = sName2 & vbCr & "type:       'line',"
        sName2 = sName2 & vbCr & "showInLegend: false,"
        sName2 = sName2 & vbCr & "dashStyle: 'Dash',"
        sName2 = sName2 & vbCr & "enableMouseTracking: false,"
        'sName2 = sName2 & vbCr & "zIndex: " & (i + 10).ToString & ","

        'sJscript = Replace(sJscript, sName, sName & vbCr & "zIndex: " & (i).ToString & ",")

        sJscript = sJscript & "," & Replace(sJscript, sName, sName2)

        PlotColToHCSeriesBB = sJscript
    End Function

    Private Function PlotColToHCSeriesAG(ByVal i As Integer) As String

        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & getSeriesNameAG(i) & "',"
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "#ffcccc" '"Red"
                Case 2
                    sColor = "#ff6a6a" '"Red"
                Case 3
                    sColor = "#ff4c4c"
                Case 4 'blue 
                    sColor = "#c4c4ff"
                Case 5 'blue 
                    sColor = "#4c4cff"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr
        sJscript = sJscript & "stacking: 'normal'," & vbCr
        If ((Session("optCosts") And Not Session("optBenefits") And i < 4) Or (Session("optBenefits") And Not Session("optCosts") And i > 3)) And Not Session("optCumulative") Then
            sJscript = sJscript & "visible: true," & vbCr
            sJscript = sJscript & "showInLegend: true," & vbCr
        Else
            sJscript = sJscript & "visible: false," & vbCr
            sJscript = sJscript & "showInLegend: false," & vbCr
        End If

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear")
            If Not IsDBNull(Session("CDValuesAG")(j, i)) Then
                If IsNumeric(Session("CDValuesAG")(j, i)) Then
                    sJscript = sJscript & CStr(Math.Round(Session("CDValuesAG")(j, i), iNumDec))

                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAG = sJscript
    End Function


    Private Function PlotColToHCSeries(ByVal sReg As String, ByVal sSce As String, ByVal i As Integer) As String

        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & sReg & ", " & sSce & "',"
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "Lime"
                Case 2
                    sColor = "Blue"
                Case 3
                    sColor = "Red"
                Case 4 'blue violet
                    sColor = "RGB(138,43,226)"
                Case 5 'aqua 
                    sColor = "RGB(0,255,255)"
                Case 6 'deep pink
                    sColor = "RGB(255,20,147)"
                Case 7 'medium spring green
                    sColor = "RGB(0,250,154)"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear")
            If Not IsDBNull(Session("CDValuesLine")(j, i)) Then
                If IsNumeric(Session("CDValuesLine")(j, i)) Then
                    sJscript = sJscript & CStr(Math.Round(Session("CDValuesLine")(j, i), iNumDec))

                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeries = sJscript
    End Function

    Private Function PlotColToHCSeriesAGTot(ByVal i As Integer) As String

        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & getSeriesNameAGTot(i) & "',"
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "#ff4c4c"
                Case 2 'blue 
                    sColor = "#4c4cff"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr
        sJscript = sJscript & "stacking: null," & vbCr
        If Session("optCosts") And Session("optBenefits") And Not Session("optCumulative") Then
            sJscript = sJscript & "visible: true," & vbCr
            sJscript = sJscript & "showInLegend: true," & vbCr
        Else
            sJscript = sJscript & "visible: false," & vbCr
            sJscript = sJscript & "showInLegend: false," & vbCr
        End If
        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2
        'jrs 2015/05/28
        Dim colStart As Integer, colEnd As Integer, k As Integer
        Dim yrVal As Single
        If i = 1 Then
            colStart = 1
            colEnd = 3
        Else
            colStart = 4
            colEnd = 5
        End If

        For j = 1 To Session("DisYear")
            yrVal = 0
            For k = colStart To colEnd
                If Not IsDBNull(Session("CDValuesAG")(j, k)) Then
                    If IsNumeric(Session("CDValuesAG")(j, k)) Then
                        yrVal = yrVal + CStr(Math.Round(Session("CDValuesAG")(j, k), iNumDec))
                    End If
                End If
            Next
            sJscript = sJscript & CStr(Math.Round(yrVal, iNumDec))

            If j < Session("DisYear") Then
                sJscript = sJscript & ", "
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAGTot = sJscript
    End Function

    Private Function PlotColToHCSeriesAGCum(ByVal i As Integer) As String

        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & getSeriesNameAG(i) & " Cumulative', type:'area', "
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "#ffcccc" '"Red"
                Case 2
                    sColor = "#ff6a6a" '"Red"
                Case 3
                    sColor = "#ff4c4c"
                Case 4 'blue 
                    sColor = "#c4c4ff"
                Case 5 'blue 
                    sColor = "#4c4cff"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr
        sJscript = sJscript & "stacking: 'normal'," & vbCr
        If ((Session("optCosts") And Not Session("optBenefits") And i < 4) Or (Session("optBenefits") And Not Session("optCosts") And i > 3)) And Session("optCumulative") Then
            sJscript = sJscript & "visible: true," & vbCr
            sJscript = sJscript & "showInLegend: true," & vbCr
        Else
            sJscript = sJscript & "visible: false," & vbCr
            sJscript = sJscript & "showInLegend: false," & vbCr
        End If

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear")
            If Not IsDBNull(Session("CDValuesAG")(j, i)) Then
                If IsNumeric(Session("CDValuesAG")(j, i)) Then
                    If i < 4 Then
                        sJscript = sJscript & CStr(Math.Round(CompoundAddVal(j, i, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                    Else
                        sJscript = sJscript & CStr(Math.Round(CompoundVal(j, i, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                    End If


                    If j < Session("DisYear") Then
                        sJscript = sJscript & ", "
                    End If
                    'ElseIf Session("GType") = "BAR" Then
                    '    sJscript = sJscript & "<point name = """ & CStr(Session("FirstYear") + j - 1) & _
                    '    """ y=""0"" x=""" & CStr(Session("FirstYear") + j - 1) & """ enabled = ""False"" />" & vbCr
                End If
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAGCum = sJscript
    End Function


    Private Function PlotColToHCSeriesAGTotCum(ByVal i As Integer) As String

        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & getSeriesNameAGTot(i) & " Cumulative', type: 'area', "
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = ""
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "#ff4c4c"
                Case 2 'blue 
                    sColor = "#4c4cff"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr
        sJscript = sJscript & "stacking: null," & vbCr
        If Session("optCosts") And Session("optBenefits") And Session("optCumulative") Then
            sJscript = sJscript & "visible: true," & vbCr
            sJscript = sJscript & "showInLegend: true," & vbCr
        Else
            sJscript = sJscript & "visible: false," & vbCr
            sJscript = sJscript & "showInLegend: false," & vbCr
        End If
        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2
        'jrs 2015/05/28
        Dim colStart As Integer, colEnd As Integer, k As Integer
        Dim yrVal As Single
        If i = 1 Then
            colStart = 1
            colEnd = 3
        Else
            colStart = 4
            colEnd = 5
        End If

        For j = 1 To Session("DisYear")
            yrVal = 0
            For k = colStart To colEnd
                If Not IsDBNull(Session("CDValuesAG")(j, k)) Then
                    If IsNumeric(Session("CDValuesAG")(j, k)) Then
                        If k < 4 Then
                            yrVal = yrVal + CStr(Math.Round(CompoundAddVal(j, k, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                        Else
                            yrVal = yrVal + CStr(Math.Round(CompoundVal(j, k, Session("CDValuesAG"), Session("cybGDP")), iNumDec))
                        End If
                    End If
                End If
            Next
            sJscript = sJscript & CStr(Math.Round(yrVal, iNumDec))

            If j < Session("DisYear") Then
                sJscript = sJscript & ", "
            End If
        Next

        sJscript = sJscript & "]" & vbCr
        sJscript = sJscript & "} " & vbCr

        PlotColToHCSeriesAGTotCum = sJscript
    End Function


    'jrs 2015/08/25 made CDValuesAG and cybGDP parameters
    Private Function CompoundAddVal(ByVal yr As Integer, ByVal col As Integer, ByRef CDValuesAG As Object, ByRef cybGDP As Object) As Single
        Dim i As Integer, Val As Single
        'jrs 2015/05/19
        Dim colGDP As Integer
        colGDP = (col - 1) \ 5 + 1
        If Not IsDBNull(CDValuesAG(1, col)) Then
            'jrs 2015/05/19 CompoundAddVal = Round(PLOT(1, col), 4)
            CompoundAddVal = Math.Round(CDValuesAG(1, col) / 100 * Session("cybGDP")(1), 4)
        Else
            CompoundAddVal = -9999
            Exit Function
        End If

        For i = 2 To yr
            If Not IsDBNull(CDValuesAG(i, col)) Then
                'jrs 2015/05/19 CompoundAddVal = Round(CompoundAddVal + PLOT(i, col), 4)
                CompoundAddVal = Math.Round(CompoundAddVal + CDValuesAG(i, col) / 100 * cybGDP(i), 4)
            Else
                CompoundAddVal = -9999
                Exit Function
            End If
        Next i
    End Function

    Private Function CompoundVal(ByVal yr As Integer, ByVal col As Integer, ByRef CDValuesAG As Object, ByRef cybGDP As Object) As Single
        Dim i As Integer, MultVal As Single
        'jrs 2015/05/19
        Dim colGDP As Integer
        colGDP = (col - 1) \ 5 + 1
        If Not IsDBNull(CDValuesAG(1, col)) Then
            'jrs 2015/05/19
            '        CompoundVal = Round(PLOT(1, col) / 100 + 1, 6)
            MultVal = Math.Round(CDValuesAG(1, col) / 100 + 1, 6)
            CompoundVal = Math.Round(CDValuesAG(1, col) / 100 * cybGDP(1), 6)
        Else
            CompoundVal = -9999
            Exit Function
        End If
        For i = 2 To yr
            If Not IsDBNull(CDValuesAG(i, col)) Then
                'jrs 2015/05/19
                MultVal = Math.Round(MultVal * (CDValuesAG(i, col) / 100 + 1), 6)
                'jrs 2015/05/21 MultVal = Round((PLOT(i, col) / 100 + 1) ^ i * PLOT(i, col) / 100 * frm_Cyber.getGDP(i, colGDP), 6)
                CompoundVal = CompoundVal + (MultVal - 1) * cybGDP(1)
            Else
                CompoundVal = -9999
                Exit Function
            End If
        Next i

        'jrs 2015/05/19 CompoundVal = (CompoundVal - yr) * 100
    End Function


    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String = ""
        If ii = 0 Then
            Legend = "Base Case"
        Else
            Legend = Replace(Replace(Session("CDSceFileNames")(ii), Session("PutFdrvRun2") + "\", ""), ".run", "")

            'jam 2023/11/02 Replacing Scenario Name
            Legend = Replace(Legend, ".db", "")

        End If
        getSeriesName = Legend
    End Function

    Private Function getSeriesNameAG(ByVal ii As Integer) As String
        Dim Legend As String = ""
        Select Case ii
            Case 0
                Legend = "Base Case"
            Case 1
                Legend = "Security Spend"
            Case 2
                Legend = "Event Cost"
            Case 3
                Legend = "Opport Cost"
            Case 4
                Legend = "Economic Increment"
            Case 5
                Legend = "Consumer surplus"
        End Select
        getSeriesNameAG = Legend
    End Function

    Private Function getSeriesNameAGTot(ByVal ii As Integer) As String
        Dim Legend As String = ""
        Select Case ii
            Case 1
                Legend = "Costs"
            Case 2
                Legend = "Benefits"
        End Select
        getSeriesNameAGTot = Legend
    End Function

    'JAM 2015-05-24 Loading Variable Name
    Private Function LoadVarNameDsh(ByVal gPutFdrvRun As String, ByVal VarDefinition As String) As String
        Dim Rcnt As Short
        Dim Varname As String
        Dim valtbl As DataTable
        Dim db As String
        Dim idx As Integer
        Dim sql As String




        db = gPutFdrvRun & "IFSVAR.mdb"
        sql = "SELECT NAME FROM IFSVAR WHERE TYPE=1 AND DEFINITION LIKE  " & "'" & VarDefinition & "'"
        SetDataTablefromAccess(db, sql, "", valtbl)



        idx = 0
        Varname = Session("CDVarName")
        For Rcnt = 0 To valtbl.Rows.Count - 1
            Varname = valtbl.Rows(Rcnt).Item("NAME")

            'VarDefinition = Replace(Replace(valtbl.Rows(Rcnt).Item("Definition"), "   ", " "), "  ", " ")
            'VarDefinition = Replace(Replace(Replace(VarDefinition, Chr(10), ""), Chr(13), ""), """", "")

            idx = idx + 1

        Next Rcnt

        LoadVarNameDsh = Varname
    End Function

    Protected Sub optAnnualized_CheckedChanged(sender As Object, e As EventArgs) Handles optAnnualized.CheckedChanged
        Session("optCumulative") = Not optAnnualized.Checked
    End Sub

    Protected Sub optCumulative_CheckedChanged(sender As Object, e As EventArgs) Handles optCumulative.CheckedChanged
        Session("optCumulative") = optCumulative.Checked
    End Sub

    Protected Sub optBenefits_CheckedChanged(sender As Object, e As EventArgs) Handles optBenefits.CheckedChanged
        Session("optBenefits") = optBenefits.Checked Or optBoth.Checked
    End Sub

    Protected Sub optCosts_CheckedChanged(sender As Object, e As EventArgs) Handles optCosts.CheckedChanged
        Session("optCosts") = optCosts.Checked Or optBoth.Checked
    End Sub

    Private Function getYAxisLabel(ByRef idx As Integer) As String
        getYAxisLabel = ""
        If idx > 0 Then
            If Session("VDisplayUnitsName")(idx) > "" Then
                getYAxisLabel = getDisplayUnitsWCurrency(idx, Session("CurrencyName"), Session("VDisplayUnitsName"))
            End If
        End If
    End Function

    'Protected Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
    '    Session("CDhash") = "imp"
    '    LoadCD4Lines(Session("VarIdx"))

    '    Dim sJscript2 As String
    '    sJscript2 = PackJSLine(Session("VarIdx")) & vbCr
    '    lblJSline.Text = sJscript2

    '    sJscript2 = PackJSMap(Session("VarIdx")) & vbCr
    '    lblJSMap.Text = sJscript2
    'End Sub

    'Protected Sub cboVariables2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVariables2.SelectedIndexChanged
    '    Session("CDhash") = "imp"

    '    Dim sJscript2 As String
    '    sJscript2 = PackJSMap(Session("VarIdx")) & vbCr
    '    lblJSMap.Text = sJscript2
    'End Sub

    'Protected Sub cboRegionSingle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboRegionSingle.SelectedIndexChanged
    '    Dim bFlagClean As Boolean = False
    '    If Session("CDhash") = "imp" Then
    '        bFlagClean = True
    '    End If

    '    Session("CDhash") = "pro"

    '    Dim sjscript2 As String
    '    sjscript2 = "<script type=""text/javascript"">" & vbCr
    '    sjscript2 = sjscript2 & "window.location.hash = ""pro"";" & vbCr
    '    sjscript2 = sjscript2 & "</script>" & vbCr
    '    lblJS.Text = sjscript2

    '    If bFlagClean Then
    '        sjscript2 = PackJSMap(Session("VarIdx")) & vbCr
    '        lblJSMap.Text = sjscript2
    '    End If
    'End Sub

    'Protected Sub cboVariables_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVariables.SelectedIndexChanged
    '    Dim bFlagClean As Boolean = False
    '    If Session("CDhash") = "imp" Then
    '        bFlagClean = True
    '    End If

    '    Session("CDhash") = "glo"

    '    Dim sjscript2 As String
    '    sjscript2 = "<script type=""text/javascript"">" & vbCr
    '    sjscript2 = sjscript2 & "window.location.hash = ""glo"";" & vbCr
    '    sjscript2 = sjscript2 & "</script>" & vbCr
    '    lblJS.Text = sjscript2

    '    If bFlagClean Then
    '        sjscript2 = PackJSMap(Session("VarIdx")) & vbCr
    '        lblJSMap.Text = sjscript2
    '    End If
    'End Sub

    'Protected Sub cboScenarioSingle2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboScenarioSingle2.SelectedIndexChanged
    '    Dim bFlagClean As Boolean = False
    '    If Session("CDhash") = "imp" Then
    '        bFlagClean = True
    '    End If

    '    Session("CDhash") = "pro"

    '    Dim sjscript2 As String
    '    sjscript2 = "<script type=""text/javascript"">" & vbCr
    '    sjscript2 = sjscript2 & "window.location.hash = ""pro"";" & vbCr
    '    sjscript2 = sjscript2 & "</script>" & vbCr
    '    lblJS.Text = sjscript2

    '    If bFlagClean Then
    '        sjscript2 = PackJSMap(Session("VarIdx")) & vbCr
    '        lblJSMap.Text = sjscript2
    '    End If
    'End Sub

    'Protected Sub cboScenarioSingle3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboScenarioSingle3.SelectedIndexChanged
    '    Dim bFlagClean As Boolean = False
    '    If Session("CDhash") = "imp" Then
    '        bFlagClean = True
    '    End If

    '    Session("CDhash") = "glo"

    '    Dim sjscript2 As String
    '    sjscript2 = "<script type=""text/javascript"">" & vbCr
    '    sjscript2 = sjscript2 & "window.location.hash = ""glo"";" & vbCr
    '    sjscript2 = sjscript2 & "</script>" & vbCr
    '    lblJS.Text = sjscript2

    '    If bFlagClean Then
    '        sjscript2 = PackJSMap(Session("VarIdx")) & vbCr
    '        lblJSMap.Text = sjscript2
    '    End If
    'End Sub

    'JAM 2015-07-11 Adding Javascript generation
    Protected Sub cmdGeneratejs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGeneratejs.Click
        GenerateVarsforCyberDasboard(Session("PutFdrvRun"), Session("ScenarioFileDir"))
    End Sub

    'jrs 2015/08/25
    Private Function getMaxScale() As String
        Dim Max As Single
        'Only Costs annualized
        If Session("optCosts") And Not Session("optBenefits") And Not Session("optCumulative") Then
            Max = Session("MaxVals4Area")(1)
        ElseIf Session("optBenefits") And Not Session("optCosts") And Not Session("optCumulative") Then
            'Only Benefits annualized
            Max = Session("MaxVals4Area")(2)
        ElseIf Session("optCosts") And Session("optBenefits") And Not Session("optCumulative") Then
            'Both Costs and Benefits annualized
            Max = Session("MaxVals4Area")(3)
        ElseIf Session("optCosts") And Not Session("optBenefits") And Session("optCumulative") Then
            'Only Cost Cumulative
            Max = Session("MaxVals4Area")(4)
        ElseIf Not Session("optCosts") And Session("optBenefits") And Session("optCumulative") Then
            'Only Benefits Cumulative
            Max = Session("MaxVals4Area")(5)
        ElseIf Session("optCosts") And Session("optBenefits") And Session("optCumulative") Then
            'Both Costs and Benefits Cumulative
            Max = Session("MaxVals4Area")(6)
        End If

        getMaxScale = CStr(Max)
    End Function

    'jrs 2015/08/25
    Private Sub LoadMaxScale()
        ReDim Session("MaxVals4Area")(6)
        Dim Max As Single, Max2 As Single
        'Only Costs annualized
        Max = Session("CDValuesAGCO")(Session("DisYear"), 1) +
                Session("CDValuesAGCO")(Session("DisYear"), 2) +
                Session("CDValuesAGCO")(Session("DisYear"), 3)
        Session("MaxVals4Area")(1) = Math.Round(Max * 1.1, 2)

        'Only Benefits annualized
        Max = Session("CDValuesAGSL")(Session("DisYear"), 4) +
            Session("CDValuesAGSL")(Session("DisYear"), 5)
        Session("MaxVals4Area")(2) = Math.Round(Max * 1.1, 2)

        'Both Costs and Benefits annualized
        'Clockwork Orange
        If Session("CDValuesAGCO")(Session("DisYear"), 1) + Session("CDValuesAGCO")(Session("DisYear"), 2) + Session("CDValuesAGCO")(Session("DisYear"), 3) >
                Session("CDValuesAGCO")(Session("DisYear"), 4) + Session("CDValuesAGCO")(Session("DisYear"), 5) Then
            Max = Session("CDValuesAGCO")(Session("DisYear"), 1) + Session("CDValuesAGCO")(Session("DisYear"), 2) + Session("CDValuesAGCO")(Session("DisYear"), 3)
        Else
            Max = Session("CDValuesAGCO")(Session("DisYear"), 4) + Session("CDValuesAGCO")(Session("DisYear"), 5)
        End If
        'Shangri-La
        If Session("CDValuesAGSL")(Session("DisYear"), 1) + Session("CDValuesAGSL")(Session("DisYear"), 2) + Session("CDValuesAGSL")(Session("DisYear"), 3) >
                Session("CDValuesAGSL")(Session("DisYear"), 4) + Session("CDValuesAGSL")(Session("DisYear"), 5) Then
            If Session("CDValuesAGSL")(Session("DisYear"), 1) + Session("CDValuesAGSL")(Session("DisYear"), 2) + Session("CDValuesAGSL")(Session("DisYear"), 3) > Max Then
                Max = Session("CDValuesAGSL")(Session("DisYear"), 1) + Session("CDValuesAGSL")(Session("DisYear"), 2) + Session("CDValuesAGSL")(Session("DisYear"), 3)
            End If
        Else
            If Session("CDValuesAGSL")(Session("DisYear"), 4) + Session("CDValuesAGSL")(Session("DisYear"), 5) > Max Then
                Max = Session("CDValuesAGSL")(Session("DisYear"), 4) + Session("CDValuesAGSL")(Session("DisYear"), 5)
            End If
        End If
        Session("MaxVals4Area")(3) = Math.Round(Max * 1.1, 2)

        'Only Cost Cumulative
        Max = CompoundAddVal(Session("DisYear"), 1, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 2, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 3, Session("CDValuesAGCO"), Session("cybGDPCO"))
        Session("MaxVals4Area")(4) = Math.Round(Max * 1.1, 2)

        'Only Benefits Cumulative
        Max = CompoundVal(Session("DisYear"), 4, Session("CDValuesAGSL"), Session("cybGDPSL")) +
            CompoundVal(Session("DisYear"), 5, Session("CDValuesAGSL"), Session("cybGDPSL"))
        Session("MaxVals4Area")(5) = Math.Round(Max * 1.1, 2)

        'Both Costs and Benefits Cumulative
        Max = CompoundAddVal(Session("DisYear"), 1, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 2, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundAddVal(Session("DisYear"), 3, Session("CDValuesAGCO"), Session("cybGDPCO"))
        Max2 = CompoundVal(Session("DisYear"), 4, Session("CDValuesAGCO"), Session("cybGDPCO")) +
                CompoundVal(Session("DisYear"), 5, Session("CDValuesAGCO"), Session("cybGDPCO"))
        If Max < Max2 Then
            Max = Max2
        End If
        Max2 = CompoundAddVal(Session("DisYear"), 1, Session("CDValuesAGSL"), Session("cybGDPSL")) +
                CompoundAddVal(Session("DisYear"), 2, Session("CDValuesAGSL"), Session("cybGDPSL")) +
                CompoundAddVal(Session("DisYear"), 3, Session("CDValuesAGSL"), Session("cybGDPSL"))
        If Max < Max2 Then
            Max = Max2
        End If
        Max2 = CompoundVal(Session("DisYear"), 4, Session("CDValuesAGSL"), Session("cybGDPSL")) +
                CompoundVal(Session("DisYear"), 5, Session("CDValuesAGSL"), Session("cybGDPSL"))
        If Max < Max2 Then
            Max = Max2
        End If
        Session("MaxVals4Area")(6) = Math.Round(Max * 1.1, 2)
    End Sub

    'jrs 2015/08/25
    Private Function getMaxIdx() As String
        Dim Max As Single
        'Only Costs annualized
        If Session("optCosts") And Not Session("optBenefits") And Not Session("optCumulative") Then
            Max = 1
        ElseIf Session("optBenefits") And Not Session("optCosts") And Not Session("optCumulative") Then
            'Only Benefits annualized
            Max = 2
        ElseIf Session("optCosts") And Session("optBenefits") And Not Session("optCumulative") Then
            'Both Costs and Benefits annualized
            Max = 3
        ElseIf Session("optCosts") And Not Session("optBenefits") And Session("optCumulative") Then
            'Only Cost Cumulative
            Max = 4
        ElseIf Not Session("optCosts") And Session("optBenefits") And Session("optCumulative") Then
            'Only Benefits Cumulative
            Max = 5
        ElseIf Session("optCosts") And Session("optBenefits") And Session("optCumulative") Then
            'Both Costs and Benefits Cumulative
            Max = 6
        End If

        getMaxIdx = CStr(Max)
    End Function


End Class



