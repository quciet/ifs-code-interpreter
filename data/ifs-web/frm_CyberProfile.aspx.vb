Imports IFs.Data

Partial Class frm_CyberProfile
    Inherits Page

    Private Sub PackJS()
        Dim bFirstTime As Boolean = False
        '*********** Map *************
        Dim VarIdx As Integer
        If Session("CPVarName") Is Nothing Then
            Session("CPVarName") = "ICTCOMPUTERS"
            Session("CPDim2") = ""
            Session("CPDim3") = ""
            Session("CPVarNameLoaded") = "ICTCOMPUTERS"
            'Load Values
            ReDim Session("CPValues")(Session("LastYear") - Session("FirstYear") + 1, NR)
            LoadCPValues(VarIdx)
            bFirstTime = True
        ElseIf Session("CPVarName") <> Session("CPVarNameLoaded") Then
            ReDim Session("CPValues")(Session("LastYear") - Session("FirstYear") + 1, NR)
            Session("CPVarNameLoaded") = Session("CPVarName")
            LoadCPValues(VarIdx)
        Else
            VarIdx = IfsVars.Item(Session("CPVarName")).Index
        End If
        '*********** Map *************
        If bFirstTime Then
            LoadSetCXP()
        End If


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

        sJscript2 = sJscript2 & "myHeight = myHeight * 0.85;" & vbCr

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
        sJscript2 = sJscript2 & "    var dataAllYrs = []" & vbCr



        Dim i As Integer
        For i = 1 To Session("DisplayLastYear") - Session("FirstYear") + 1
            sJscript2 = sJscript2 & "    var data = [" & vbCr
            sJscript2 = sJscript2 & PrepareDataForMap(i)
            sJscript2 = sJscript2 & "];" & vbCr
            sJscript2 = sJscript2 & "    dataAllYrs[" & (i - 1).ToString & "] = data;" & vbCr
        Next

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
        'sJscript2 = sJscript2 & "if (mapKey == 'custom/world') {" & vbCr
        'sJscript2 = sJscript2 & "svgPath = 'Maps/country.svg';" & vbCr
        'sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "        // Dim or highlight search box" & vbCr
        sJscript2 = sJscript2 & "if (mapDesc === searchText || isHeader) {" & vbCr
        sJscript2 = sJscript2 & "$('.custom-combobox-input').removeClass('valid');" & vbCr
        sJscript2 = sJscript2 & "location.hash = '';" & vbCr
        sJscript2 = sJscript2 & "} else {" & vbCr
        sJscript2 = sJscript2 & "$('.custom-combobox-input').addClass('valid');" & vbCr
        sJscript2 = sJscript2 & "location.hash = mapKey;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "        if (isHeader) {" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "        // Show loading" & vbCr
        sJscript2 = sJscript2 & "if ($(""#container"").highcharts()) {" & vbCr
        sJscript2 = sJscript2 & "$(""#container"").highcharts().showLoading('<i class=""fa fa-spinner fa-spin fa-2x""></i>');" & vbCr
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
        sJscript2 = sJscript2 & "$.each(mapGeoJSON.features, function (index, feature) {" & vbCr
        sJscript2 = sJscript2 & "data2.push({" & vbCr
        sJscript2 = sJscript2 & "key: feature.properties['hc-key']," & vbCr
        'sJscript2 = sJscript2 & "code: feature.properties['iso-a2']," & vbCr
        'sJscript2 = sJscript2 & "code: feature.properties['hc-key']," & vbCr
        'sJscript2 = sJscript2 & "value:  index" & vbCr
        sJscript2 = sJscript2 & "value:  CountryVal" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        'jrs 2015/05/13
        sJscript2 = sJscript2 & "            //Set appropiate data" & vbCr
        sJscript2 = sJscript2 & "if (mapKey !== 'custom/world') {" & vbCr
        sJscript2 = sJscript2 & "dataX = data2;" & vbCr
        sJscript2 = sJscript2 & "joinXmap = 'hc-key';" & vbCr
        'sJscript2 = sJscript2 & "joinXdata = 'key';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "else {" & vbCr
        sJscript2 = sJscript2 & "dataX = dataAllYrs[0];" & vbCr
        sJscript2 = sJscript2 & "joinXmap = 'iso-a2';" & vbCr
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
        sJscript2 = sJscript2 & "$(""#container"").highcharts('Map', {" & vbCr

        'this works, but shows a big logo, right behind the map, no chance to configure size and position
        sJscript2 = sJscript2 & "chart: {" & vbCr
        'sJscript2 = sJscript2 & "plotBackgroundImage:   './Images/pcl.jpg'" & vbCr
        sJscript2 = sJscript2 & "height:   myHeight" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:   FirstYear," & vbCr
        sJscript2 = sJscript2 & "verticalAlign: 'bottom'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "        mapNavigation: {" & vbCr
        sJscript2 = sJscript2 & "enabled: true" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "                colorAxis: {" & vbCr
        sJscript2 = sJscript2 & "min: 0," & vbCr
        sJscript2 = sJscript2 & "stops: [" & vbCr
        sJscript2 = sJscript2 & "[0, '#EFEFFF']," & vbCr
        sJscript2 = sJscript2 & "[0.5, Highcharts.getOptions().colors[0]]," & vbCr
        sJscript2 = sJscript2 & "[1, Highcharts.Color(Highcharts.getOptions().colors[0]).brighten(-0.5).get()]" & vbCr
        sJscript2 = sJscript2 & "]" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "                legend: {" & vbCr
        sJscript2 = sJscript2 & "layout:  'vertical'," & vbCr
        sJscript2 = sJscript2 & "align:  'left'," & vbCr
        sJscript2 = sJscript2 & "verticalAlign:  'bottom'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [{" & vbCr
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
        sJscript2 = sJscript2 & "(this.point.properties && this.point.properties['hc-a2']) :" & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "(this.point.properties && this.point.properties['iso-a2']) :" & vbCr
        sJscript2 = sJscript2 & "this.point.name;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "point: {" & vbCr
        sJscript2 = sJscript2 & "events: {" & vbCr
        sJscript2 = sJscript2 & "// On click, look for a detailed map" & vbCr
        sJscript2 = sJscript2 & "click: function () {" & vbCr
        'jrs 2015/05/13 sJscript2 = sJscript2 & "var key = this.key;" & vbCr
        sJscript2 = sJscript2 & "var key = this.key.toLowerCase();" & vbCr
        sJscript2 = sJscript2 & "CountryVal = this.value;" & vbCr
        sJscript2 = sJscript2 & "$('#mapDropdown option').each(function () {" & vbCr
        sJscript2 = sJscript2 & "if (this.value === 'countries/' + key.substr(0, 2) + '/' + key + '-all.js') {" & vbCr
        sJscript2 = sJscript2 & "$('#mapDropdown').val(this.value).change();" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
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

        sJscript2 = sJscript2 & "// Activate the play button" & vbCr
        sJscript2 = sJscript2 & "$('#play').click(function () {" & vbCr
        'sJscript2 = sJscript2 & "$(""#slider"").slider(""slide"") = '';" & vbCr
        sJscript2 = sJscript2 & "myPlayer = setInterval(function () {AdvanceYear()}, 500);" & vbCr
        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Activate the pause button" & vbCr
        sJscript2 = sJscript2 & "$('#pause').click(function () {" & vbCr
        sJscript2 = sJscript2 & "clearInterval(myPlayer);" & vbCr

        'sJscript2 = sJscript2 & "$(""#slider"").slider(""slide"") = " & vbCr
        'sJscript2 = sJscript2 & "function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear + 1;" & vbCr
        'sJscript2 = sJscript2 & "$('#container').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "$('#container').highcharts().series[0].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "return false;" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "// Advance Year" & vbCr
        sJscript2 = sJscript2 & "function AdvanceYear() {" & vbCr
        sJscript2 = sJscript2 & "YrIdx = YrIdx + 1;" & vbCr
        sJscript2 = sJscript2 & "$('#container').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        sJscript2 = sJscript2 & "$('#container').highcharts().series[0].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "$(""#slider"").slider(""value"") = FirstYear + YrIdx;" & vbCr
        sJscript2 = sJscript2 & "if (YrIdx == MaxYrIdx ){" & vbCr
        sJscript2 = sJscript2 & "clearInterval(myPlayer);" & vbCr

        'sJscript2 = sJscript2 & "$(""#slider"").slider(""slide"") = " & vbCr
        'sJscript2 = sJscript2 & "function (event, ui) {" & vbCr
        'sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear + 1;" & vbCr
        'sJscript2 = sJscript2 & "$('#container').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        'sJscript2 = sJscript2 & "$('#container').highcharts().series[0].setData(dataAllYrs[YrIdx]);" & vbCr
        'sJscript2 = sJscript2 & "};" & vbCr

        sJscript2 = sJscript2 & "}" & vbCr
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
        'sJscript2 = sJscript2 & "$('#container').height = 1400;" & vbCr
        'sJscript2 = sJscript2 & "mychartmap.renderer.image('./Images/pcl.jpg', 0, 0, 60, 60)" & vbCr
        'sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        'sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        'sJscript2 = sJscript2 & "})" & vbCr
        'sJscript2 = sJscript2 & ".add();" & vbCr
        'sJscript2 = sJscript2 & "var myWidth = 700, myHeight = 700;" & vbCr
        'sJscript2 = sJscript2 & "mychartmap.setSize(myWidth, myHeight);" & vbCr
        'sJscript2 = sJscript2 & "mychartmap.hasUserSize = null;" & vbCr


        sJscript2 = sJscript2 & "    // Trigger change event to load map on startup" & vbCr
        sJscript2 = sJscript2 & "if (location.hash) {" & vbCr
        sJscript2 = sJscript2 & "$('#mapDropdown').val(location.hash.substr(1) + '.js');" & vbCr
        sJscript2 = sJscript2 & "} else { // for IE9" & vbCr
        sJscript2 = sJscript2 & "$($('#mapDropdown option')[0]).attr('selected', 'selected');" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "$('#mapDropdown').change();" & vbCr

        'jrs 2015/05/19
        sJscript2 = sJscript2 & "function Sliderutility() {" & vbCr
        sJscript2 = sJscript2 & "$(""#slider"").slider(" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "value: " & Session("FirstYear") & "," & vbCr
        sJscript2 = sJscript2 & "min: " & Session("FirstYear") & "," & vbCr
        sJscript2 = sJscript2 & "max: " & Session("DisplayLastYear") & "," & vbCr
        sJscript2 = sJscript2 & "step: 1," & vbCr
        sJscript2 = sJscript2 & "slide: function (event, ui) {" & vbCr
        sJscript2 = sJscript2 & "YrIdx = ui.value - FirstYear;" & vbCr
        sJscript2 = sJscript2 & "$('#container').highcharts().setTitle({text: FirstYear + YrIdx}, null);" & vbCr
        sJscript2 = sJscript2 & "$('#container').highcharts().series[0].setData(dataAllYrs[YrIdx]);" & vbCr
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

        '************* bubble chart **********************

        sJscript2 = sJscript2 & "<script>" & vbCr
        sJscript2 = sJscript2 & "function BuildBubbles(){ // encapsulate jQuery" & vbCr
        'sJscript2 = sJscript2 & "            $(function () {" & vbCr
        sJscript2 = sJscript2 & "$('#containerbubble').highcharts({" & vbCr

        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "type:       'bubble'," & vbCr
        sJscript2 = sJscript2 & "plotBorderWidth: 1," & vbCr
        sJscript2 = sJscript2 & "height: myHeight*0.75," & vbCr
        sJscript2 = sJscript2 & "width: myHeight*0.75," & vbCr
        sJscript2 = sJscript2 & "zoomType:   'xy'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "tooltip: {" & vbCr
        sJscript2 = sJscript2 & "formatter: function () {" & vbCr
        sJscript2 = sJscript2 & "return '<span style=""color:' + this.series.color + '"">●</span>' + ' ' + this.series.name + '<br>(' + this.point.x + ', ' + this.point.y + ')';" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        sJscript2 = sJscript2 & "xAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Probability'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "gridLineWidth: 1" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "yAxis: {" & vbCr
        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       'Cost'" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr
        sJscript2 = sJscript2 & "startOnTick: false," & vbCr
        sJscript2 = sJscript2 & "endOnTick: false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "plotOptions: {" & vbCr
        sJscript2 = sJscript2 & "bubble: {" & vbCr
        sJscript2 = sJscript2 & "dataLabels: {" & vbCr
        sJscript2 = sJscript2 & "enabled: true," & vbCr
        sJscript2 = sJscript2 & "formatter: function() {return this.point.name;}" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        'sJscript2 = sJscript2 & "enableMouseTracking: false" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "series: [" & vbCr

        For i = 1 To 4
            If i > 1 Then
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

        lblJS.Text = sJscript2

        'ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadCyberProfile", _
        '    sJscript2, False)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("DisYear") = Session("DisplayLastYear") - Session("FirstYear") + 1
            'jrs 2015/05/20 Temporary, we'll remove it once we have the file combo box
            Dim sFile As String = Session("PutFdrvRun2") & "\DBtest.run"
            Session("IFsRunFiles").Add(sFile, True)
            Session("RunFiles")(6) = sFile
            ReDim Preserve Session("vShortSce")(6)
            Session("vShortSce")(6) = "DBTest"
        End If

        PackJS()
    End Sub

    Private Sub LoadCPValues(ByRef VarIdx)
        Dim Yr As Integer = Year(Now)
        Dim Dim2Idx As Integer = -1, Dim3Idx As Integer = -1
        Dim i As Integer
        Dim MyFileforDisplay As IStorage
        Dim MyDF As IDataframe

        For i = 1 To NR
            gLoadForDisplayNET(Session("CPVarName"), VarIdx, GRPS(1, i), i, Session("CPDim2"), Dim2Idx, Session("CPDim3"), Dim3Idx,
                Session("PutFdrvRun") & "DBtest.run", 6, i = 1, i = NR, False, 1, Session("CPValues"), i, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
            'gLoadForDisplay(Session("CPVarName"), VarIdx, GRPS(1, i), i, Session("CPDim2"), Dim2Idx, Session("CPDim3"), Dim3Idx,
            'Session("PutFdrvRun") & "DBtest.run", 6, i = 1, i = NR, False, 1, Session("CPValues"), i, 1, Session("FirstYear"), Session("LastYear"), 0, False,
            'Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
            'Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
        Next
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
        ReDim Session("CPValuesBB")(Session("LastYear") - Session("FirstYear") + 1, 8)
        'If lstFiles.Selected(0) Then
        '    LoadSetCXPSCE()
        'End If

        'loop through variables
        For v = 1 To 2
            If v = 1 Then
                sVar = "ictcybevcost"
            Else
                sVar = "ictcybevprob"
            End If
            idx = IfsVars.Item(sVar).Index
            'just one file
            'For f = 0 To lstFiles.ListCount - 1
            iFile = 1

            Dim MyFileforDisplay As IStorage
            Dim MyDF As IDataframe

            'loop through actors
            For r = 1 To MaxVal(VDim1(idx)) - 1
                sDim1 = ""
                iDim1 = r
                'use total for targets
                T = -1
                COUNTE = COUNTE + 1

                If iFile = 0 Then
                    'If v = 1 Then
                    '    For y = 1 To LastYear - FirstYear + 1
                    '        PLOT(y, COUNTE) = Cost(r, T, y)
                    '    Next y
                    'Else
                    '    For y = 1 To LastYear - FirstYear + 1
                    '        PLOT(y, COUNTE) = Prob(r, T, y)
                    '    Next y
                    'End If
                    'SaveLabels idx
                    'GR1N(COUNTE) = GRPS(VDim1(idx), r)
                    'FDNG1(COUNTE) = GR1N$(COUNTE)
                    'GR2N(COUNTE) = GRPS(VDim2(idx), T)
                    'FDNG2(COUNTE) = GR2N$(COUNTE)
                Else
                    gLoadForDisplayNET(sVar, idx, "", r, "Total", -1, "", -1,
                        Session("PutFdrvRun") & "DBtest.run", 6, (v = 1 And r = 1) Or iFile = 0, (v = 2 And r = MaxVal(VDim1(idx)) - 1) Or iFile = 0,
                        False, 1, Session("CPValuesBB"), COUNTE, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                        Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"), MyFileforDisplay, MyDF)
                    'gLoadForDisplay(sVar, idx, "", r, "Total", -1, "", -1,
                    '                        Session("PutFdrvRun") & "DBtest.run", 6, (v = 1 And r = 1) Or iFile = 0, (v = 2 And r = MaxVal(VDim1(idx)) - 1) Or iFile = 0,
                    '                        False, 1, Session("CPValuesBB"), COUNTE, 1, Session("FirstYear"), Session("LastYear"), 0, False,
                    '                        Session("bLoadForDisplaySuccess"), Me, Session("RunFiles"), Session("PutFdrvRun"), Session("vShortSce"),
                    '                        Session("FileForDisplay"), Session("FinYear"), Session("IFs_MDB2Active"), Session("CurrencyVal"))
                End If
            Next r
            'Next f
        Next v
    End Sub

    Private Function PrepareDataForMap(ByVal yrIdx As Integer) As String
        Dim jScript As String = "", i As Integer
        For i = 1 To NR
            If i > 1 Then
                jScript = jScript & "," & vbCr
            End If
            jScript = jScript & "{" & vbCr
            'jrs 2015/05/13 jScript = jScript & """code"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """key"": """ & IFsRegs.Item(GRPS(1, i)).ISO3166_2Code & """," & vbCr
            jScript = jScript & """value"": " & Session("CPValues")(yrIdx, i) & vbCr
            jScript = jScript & "}"
        Next
        jScript = jScript & vbCr
        PrepareDataForMap = jScript
    End Function

    Private Function PlotColToHCSeriesBB(ByVal i As Integer) As String

        Dim Interval As Integer = 10
        Dim sJscript As String = ""
        Dim sName As String = ""
        sName = "{name: '" & getSeriesName(i) & "',"
        sJscript = sJscript & sName & vbCr

        Dim sColor As String = "", sColor2 As String
        If i < 9 Then
            Select Case i
                Case 1
                    sColor = "#b2ffb2" '"Lime"
                    sColor2 = "#00ff00"
                Case 2
                    sColor = "#ccccff" '"Blue"
                    sColor2 = "#0000ff"
                Case 3
                    sColor = "#ffcccc" '"Red"
                    sColor2 = "#ff0000"
                Case 4 'blue violet
                    sColor = "#dbbff6" '"rgba(138,43,226,0.5)"
                    sColor2 = "#8A2BE2"
            End Select
        End If
        sJscript = sJscript & "color: '" & sColor & "'," & vbCr

        sJscript = sJscript & "data: ["

        'jrs 2014/03/21
        Dim FstVal As Single = 0
        Dim j As Integer
        'jrs 2011/08/25
        Dim iNumDec As Integer = 2


        For j = 1 To Session("DisYear") Step Interval
            If Not IsDBNull(Session("CPValuesBB")(j, i)) Then
                If IsNumeric(Session("CPValuesBB")(j, i)) And IsNumeric(Session("CPValuesBB")(j, i + 4)) Then
                    sJscript = sJscript & "{name: '" & CStr(Session("FirstYear") + j - 1) & "', x: " & CStr(Math.Round(Session("CPValuesBB")(j, i + 4), iNumDec)) & ", y: "
                    sJscript = sJscript & CStr(Math.Round(Session("CPValuesBB")(j, i), iNumDec))
                    'jrs 2015/05/21
                    If j = Session("DisYear") Then
                        sJscript = sJscript & ", color: '" & sColor2 & "'"
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
            If Not IsDBNull(Session("CPValuesBB")(j, i)) Then
                If IsNumeric(Session("CPValuesBB")(j, i)) Then
                    sJscript = sJscript & "{name: '" & CStr(Session("FirstYear") + j - 1) & "', x: " & CStr(Session("CPValuesBB")(j, i + 4)) & ", y: "
                    sJscript = sJscript & CStr(Session("CPValuesBB")(j, i))
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

    Private Function getSeriesName(ByVal ii As Integer) As String
        Dim Legend As String = ""
        Select Case ii
            Case 1
                Legend = "Hacktivism"
            Case 2
                Legend = "Cyber Crime"
            Case 3
                Legend = "Cyber Espionage"
            Case 4
                Legend = "Cyber Warfare"
        End Select
        getSeriesName = Legend
    End Function

End Class

