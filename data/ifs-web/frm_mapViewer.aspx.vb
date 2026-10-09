Imports AspMap
Imports System.Data.OleDb

Partial Class frm_mapViewer
    Inherits Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

    End Sub

#End Region
    Private cName As String
    Private Enum Classification
        EqualInterval = 0
        EqualCount = 1
        UniqueValue = 2
    End Enum

    Private Enum ColorRampType
        Red = 0
        Green = 1
        Blue = 2
        Gray = 3
    End Enum

    Private Structure ClassificationType
        Dim Type As Classification
        Dim NumberOfClasses As Integer
        Dim colorType As ColorRampType
    End Structure

    '*****************************
    'Change this to reference the session variable pointing to the IFS.mdb.
    Private CONNECTION_STRING As String
    '*****************************

    '*****************************
    'Change this to reference the location of the country.shp file
    'Private GEODATA_PATH As String = "C:\development\web\aspmaptest\data\country.shp"
    Private GEODATA_PATH As String = DataFileDir & "EsriWorld\country.shp"
    '*****************************

    Private Const NODATA_VALUE = "NO DATA"

    Private Const HEIGHT = 360 'Map height in pixels
    Private Const WIDTH = 540  'Map width in pixels
    'JRS 08/30/04
    Private DataBaseFile As String
    Private f As DataColumn
    Private valtbl As DataTable
    'jrs 10/01/04
    Private lstPts As New ArrayList
    Private lstEvts As New ArrayList
    'jrs 2011/06/21
    Dim bReLoadMap As Boolean


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load 'jrs 2011/11/11, Me.Load
        'jrs 2011/06/21
        bReLoadMap = False
        Dim i As Integer

        'Initialize the map object on the first call to the page
        ' and save the map to session state.
        CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;" &
            "Data Source=" & Session("IFsHistSeries_MDB") & ";" &
            "User ID=Admin;" &
            "Password=" & DataPW 'jrs 09/14/04
        'CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        '"Data Source=C:\development\web\aspmaptest\data\IFS.mdb;" & _
        '"User ID=Admin;" & _
        '"Password="
        'CONNECTION_STRING = ConfigurationSettings.AppSettings("CONNECTION_STRING")
        'GEODATA_PATH = ConfigurationSettings.AppSettings("GEODATA_PATH")


        CreateMenu()
        If Not IsPostBack Then
            'jrs 2012/01/30
            Session("AvailabilityParent") = "frm_mapViewer.aspx"

            'JAM 2017-10-22
            If IsNothing(Session("RangeChanged")) Then
                Session("RangeChanged") = False
                ReDim Session("RangeLimits")(7)
            End If

            'JAM 2017-10-22
            'If Not Session("RangeChanged") Or IsNothing(Session("RangeChanged")) Then
            '    'ReDim Session("RangeLimits")(Session("MapNoCat") - 1)
            '    'JAM 2017-10-29
            '    ReDim Session("RangeLimits")(7)
            'End If



            'JAM 2017-10-29 Keeping selected categories number
            If Not IsNothing(Session("MapNoCat")) Then
                If Session("MapNoCat") > 5 Then
                    drpNumberOfClasses.Text = Session("MapNoCat")
                End If
            End If

            'jrs 10/01/04

            'If Not Request.QueryString("W") Is Nothing Or Not Request.QueryString("H") Is Nothing Then
            '    bt.ImageUrl = "~/MapComponent.aspx?W=" & Request.QueryString("W").ToString & "&H=" & Request.QueryString("H").ToString
            '    ' btnMapImage.ImageUrl = "~/MapComponent.aspx?W=" & btnMapImage.Width.Value.ToString & "&H=" & btnMapImage.Height.Value.ToString()
            '    Session("RW") = Request.QueryString("W").ToString
            '    Session("RH") = Request.QueryString("H").ToString
            'Else
            '    btnMapImage.ImageUrl = "~/MapComponent.aspx?W=" & Session("RW").ToString & "&H=" & Session("RH").ToString
            'End If

            Session("maplstPts") = lstPts
            Session("maplstEvts") = lstEvts

            'jrs 2023/11/08
            If Not IsNothing(Session("MapColor")) Then
                drpColor.Text = Session("MapColor")
            End If
            If Not IsNothing(Session("MapNoCat")) Then
                drpNumberOfClasses.Text = Session("MapNoCat")
            End If
            If Not IsNothing(Session("MapDisplayType")) Then
                drpDisplayType.Text = Session("MapDisplayType")
            End If
            If Not IsNothing(Session("MapProjection")) Then
                drpProjection.Text = Session("MapProjection")
            End If


            'jrs 09/29/04
            If Session("MapChosenTable") = "Map" Then
                'jrs 2004/11/27
                Dim rsMap As DataTable
                SetDataTablefromAccess(Session("IFs_MDB2Active"), "SELECT FIPS_CODE,[StoreData],Country FROM [Map] ORDER BY COUNTRY", "", rsMap)
                Session("rsMap") = rsMap
                FillRegionMap2(Session("PLOT"), Session("PYear"), Session("GeoRegion"), Session("GR1N"), Session("GeoValue"),
                    Session("IFs_MDB2Active"), Session("MaxValue"), Session("rsMap"), Me)
            End If
            'JRS 08/30/04
            '   Establish Default Database, File, and Variable Name
            '   ---------------------------------------------------

            'jrs 09/30/04
            If Session("MapChosenTable") = "Map" Then
                DataBaseFile = Session("IFs_MDB2Active")
            Else
                DataBaseFile = Session("IFsHistSeries_MDB")
            End If
            Session("DataBaseFile") = DataBaseFile
            If Session("RecordSourceDef") <> "Map" Then
                Session("MapChosenTable") = "TimeSeries"
                Call UpdateFileComboBox()
                'jrs 09/14/04
                Call drpDataTable_SelectedIndexChanged(sender, New EventArgs)
                'jrs 09/14/04 Call UpdateVariableComboBox()

                'jrs 2005/10/24
                If Not (Session("MDGMapFieldName") = Nothing) Then
                    Dim j As Integer
                    For j = 0 To drpDataField.Items.Count
                        If Session("MDGMapFieldName") = drpDataField.Items(j).Text Then
                            Session("DataInfoIdx") = j
                            Exit For
                        End If
                    Next

                    'jrs 2008/07/04
                    If Not IsNumeric(Session("PYear")) Then
                        Session("PYear") = 1
                    End If
                    Session("MapYear") = Str(Session("FirstYear") + Session("PYear") - 1)
                    lblYear.Text = "Year = " + Session("MapYear")
                    lblDataTable.Visible = False
                    lblDataField.Visible = False
                    drpDataTable.Visible = False
                    drpDataField.Visible = False

                    'cmdNext.Visible = True
                    'cmdPrev.Visible = True
                    'cmdFirst.Visible = True
                    'cmdLast.Visible = True
                    'lblYear.Visible = True

                    lblVariable.Visible = True
                    lblVariable.Text = "Variable: " & Session("MDGMapFieldName")

                End If

                'jrs 2005/04/01
                If Session("DataInfoIdx") = Nothing Then
                    Session("MapFieldName") = "HDI"
                    'jrs 2006/04/24
                    Dim sDesc As String
                    Dim tblDict As DataTable
                    SetDataTablefromAccess(Session("PutFdrvRun") + "datadict.MDB", "SELECT * FROM DataDict WHERE Variable = 'HDI'", "", tblDict)
                    sDesc = tblDict.Rows(0).Item("Definition")

                    For i = 0 To drpDataField.Items.Count - 1
                        'jrs 09/14/04
                        If drpDataField.Items(i).Text = "HDI - " & sDesc & " - Index" Then
                            drpDataField.SelectedIndex = i
                            Exit For
                        End If
                    Next
                Else
                    drpDataField.SelectedIndex = Session("DataInfoIdx")
                    Session("DataInfoIdx") = Nothing
                End If
                'jrs 09/14/04
                Call drpDataField_SelectedIndexChanged(sender, New EventArgs)

                'jrs 2005/10/24
                If Not (Session("MDGMapFieldName") = Nothing) Then
                    lblDimension.Visible = False
                    drpDimension.Visible = False
                    Session("MDGMapFieldName") = Nothing
                End If

                'jrs 2011/11/16
                If Session("mapParent") = Nothing Then
                    Session("mapParent") = "frm_MainMenu.aspx"
                End If

            Else
                'jrs 2008/07/04
                If Not IsNumeric(Session("PYear")) Then
                    'jrs 10/31/04
                    Session("PYear") = 1
                End If
                Session("MapYear") = Str(Session("FirstYear") + Session("PYear") - 1)
                lblYear.Text = "Year = " + Session("MapYear")
                'jrs 09/30/04
                lblDataTable.Visible = False
                lblDataField.Visible = False
                drpDataTable.Visible = False
                drpDataField.Visible = False
                cmdNext.Visible = True
                cmdPrev.Visible = True
                cmdFirst.Visible = True
                cmdLast.Visible = True
                lblYear.Visible = True
                lblVariable.Visible = True
                'jrs 2008/03/28 lblVariable.Text = "Variable: " & Session("MapVar")
                lblVariable.Text = "Variable Name: " & "<br>" & Session("MapVar") & " = " & Session("VARN")(2, 1)
                'jrs 2006/02/24
                cmdDataInfo.Visible = False
                'JRS 2005/01/15 RepaintMap()
            End If
            'jrs 2011/06/17
            bReLoadMap = True
            'JRS 2005/01/15 
            RepaintMap()

            'jrs 09/14/04
            'sessionMap = BuildMap(WIDTH, HEIGHT, GEODATA_PATH)
            'Dim dataValueTable As DataTable = BuildDataTable(CONNECTION_STRING, "SELECT FIPS_CODE," & Session("MapFieldName") & " FROM " & Session("MapChosenTable"))
            'Dim dataValues As Hashtable = BuildHashTableFromDataTable(dataValueTable, "FIPS_CODE", Session("MapFieldName"))
            'Dim countryLayerADORS As ADODB.Recordset = BuildADORS(sessionMap.Layer(0), dataValues, "FIPS_CODE")
            'sessionMap.AddLayer(countryLayerADORS)
            'Dim sessionClassificationType As ClassificationType
            'sessionClassificationType.NumberOfClasses = 5
            'sessionClassificationType.Type = Classification.EqualInterval
            'sessionClassificationType.colorType = ColorRampType.Green
            'Session("classificationType") = sessionClassificationType
            'ClassifyLayer(sessionMap.Layer(0), "VALUE", sessionClassificationType.Type, sessionClassificationType.NumberOfClasses, sessionClassificationType.colorType)
            'Session("sessionMap") = sessionMap
        Else
            DataBaseFile = Session("DataBaseFile")
            '***
            'The display layer is reclassified with each load of the page.  This is 
            ' to counteract the difference in behavior between the application on 
            ' Windows XP IIS 5.x and Server 2003 IIS 6.0.  In the server version,
            ' the classification is lost between requests.  This merits more investigation.
            ' However, the fix for now is to rebuild the classification with each request.
            'jrs 2011/06/30
            'If Not Session("sessionMap") Is Nothing Then
            '    sessionMap = Session("sessionMap")

            '    Dim clType As ClassificationType = Session("classificationType")
            '    ClassifyLayer(sessionMap.Layer(0), "VALUE", clType.Type, clType.NumberOfClasses, clType.colorType)
            'Else
            '    Response.Write("Session map not found. Please reload the page.")
            'End If
            'jrs 2011/06/17
            bReLoadMap = True
        End If
        'jrs 2011/06/24 WinFormsToWeb_PageBar.Rows(0).Cells(0).Text = "Historic Map&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Session:" & Session("SessionIdx")

        'jrs 2013/09/04
        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "ShowHelpJS",
            ShowHelpJS(WinFormsToWeb_PageBar.Rows(0).Cells(1).Text, ""), True)

        'jrs 2016/01/12
        Select Case drpProjection.SelectedValue
            Case "Behrmann"
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/Behrmanncountry.js""></script>", False)
            Case "Eckert"
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/Eckertcountry.js""></script>", False)
            Case "Equicylinder"
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/Equicylindercountry.js""></script>", False)
            Case "Mercator"
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/Mercatorcountry.js""></script>", False)
            Case "Robinson"
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/Robinsoncountry.js""></script>", False)
            Case "Sinusoidal"
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/Sinusoidalcountry.js""></script>", False)
            Case Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "ShowMapJS",
                    "<script src=""Maps/country.js""></script>", False)
        End Select

    End Sub

    Private Sub CreateMenu()
        wmPopUp.UserData = "jsolorza::976739975"
        wmPopUp.ClearPixelImage = "blank.gif"
        wmPopUp.PopupIcon = "./images/Popup.gif"
        wmPopUp.ScrollDownActiveImage = "./images/ScrollDownActive.gif"
        wmPopUp.ScrollDownEnabledImage = "./images/ScrollDownEnabled.gif"
        wmPopUp.ScrollUpActiveImage = "./images/ScrollUpActive.gif"
        wmPopUp.ScrollUpEnabledImage = "./images/ScrollUpEnabled.gif"
        wmPopUp.SelectedPopupIcon = "./images/SelectedPopup.gif"
        wmPopUp.ScrollEnabled = True
        'AddHandler wmPopUp.ServerItemClick, AddressOf ItemClickHandlerContext
        wmPopUp.MenuBar.ShowOnMouseOver = True


        Dim cswmGroup1 As Coalesys.WebMenu.Group
        cswmGroup1 = wmPopUp.Groups.Add()
        cswmGroup1.ID = "cntyInfoGroup"

        Dim cswmItem As Coalesys.WebMenu.Item
        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Map Of"
        cswmItem.ID = "1"
        cswmItem.OnClick = "modalWinImage()"

        cswmItem = cswmGroup1.Items.Add()
        cswmItem.Caption = "Data Series"
        cswmItem.OnClick = "modalWinData()"
        cswmItem.ID = "2"

        wmPopUp.MenuBar.DisplayMode = Coalesys.WebMenu.DisplayModeConstants.None
        wmPopUp.ItemFont.PaddingLeft = 4

    End Sub
    Sub ItemClickHandlerContext(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objSender As Coalesys.WebMenu.WebMenu

        objSender = sender


        Dim objEA As Coalesys.WebMenu.ServerItemClickEventArgs
        objEA = e
        'strs = Split(Request.Form("txtDataHolder"), ",")
        'xpoint = CDbl(strs(0))
        'ypoint = CDbl(strs(1))
        'cName = Request.Form("txtDataHolder")

        'Session.Add("cNumber", cName)
        Select Case objEA.Item.ID
            Case "1"
                Support.FormManager.StateStore(Me)
                Support.FormManager.Show(Me, "frm_CountryProfile.aspx?ccode=" & Request.QueryString("cname"))

        End Select
        Response.Redirect(Request.Url.AbsolutePath)
    End Sub



    'BuildADORS
    'Create a new ADO recordset storing the geometry, key, and value field. The geometry and key values are
    'copied from the base layer passed as a parameter.  This recordset can then be added to the map as a new layer
    'for displaying ad-hoc data values stored in the "VALUE" field. Data values are retrieved from the data stored in 
    'the hashtable object, referenced by the keyfield

    Private Function BuildADORS(ByVal baseLayer As AspMap.Layer, ByVal datavalues As Hashtable, ByVal keyField As String) As ADODB.Recordset
        'Instantiate a new ADO Recordset
        Dim adoLayerRS As New ADODB.Recordset
        'Add the fields to the recordset
        adoLayerRS.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        With adoLayerRS
            .Fields.Append("SHP_DATA", ADODB.DataTypeEnum.adVariant)
            .Fields.Append(keyField, ADODB.DataTypeEnum.adVarChar, 255)
            .Fields.Append("VALUE", ADODB.DataTypeEnum.adVarChar, 255, ADODB.FieldAttributeEnum.adFldIsNullable)
            .Fields.Append("CINDEX", ADODB.DataTypeEnum.adInteger)
            .Fields.Append("NAME", ADODB.DataTypeEnum.adVarChar, 255)
        End With
        adoLayerRS.Open()

        'Reference the base layer recordset
        Dim baseRS As AspMap.Recordset = baseLayer.Recordset
        baseRS.MoveFirst()

        'Step through the base layer recordset
        While Not baseRS.EOF
            adoLayerRS.AddNew()
            '-- copy the geometry
            adoLayerRS.Fields("SHP_DATA").Value = baseRS.Shape.ShapeData
            '-- copy the key values
            adoLayerRS.Fields(keyField).Value = CStr(baseRS(keyField))
            '-- insert the data value
            Dim dataValue As Object = datavalues(adoLayerRS.Fields(keyField).Value)
            If (Not TypeOf (dataValue) Is DBNull) And (IsNumeric(dataValue)) Then
                adoLayerRS.Fields("VALUE").Value = CStr(dataValue)
            ElseIf dataValue Is Nothing Then
                adoLayerRS.Fields("VALUE").Value = NODATA_VALUE
            Else
                adoLayerRS.Fields("VALUE").Value = NODATA_VALUE
            End If
            adoLayerRS.Fields("CINDEX").Value = datavalues(CStr(baseRS(keyField)))

            adoLayerRS.Fields("NAME").Value = CStr(baseRS("NAME"))
            baseRS.MoveNext()
        End While
        'return the recordset
        Return adoLayerRS
    End Function

    'BuildHashTableFromDataTable
    'Create a hashtable storing data from a table column referenced by a unique key value
    'The hashtable is used as a lookup table for populating the ADO recordset used for the map
    'display.

    Private Function BuildHashTableFromDataTable(ByVal inputDataTable As DataTable, ByVal keyField As String, ByVal dataField As String) As Hashtable
        Dim outputHashTable As New Hashtable
        'Step through the rows of the table
        Dim dRow As DataRow
        For Each dRow In inputDataTable.Rows

            '-- Extract the key field value
            Dim keyValue As Object = dRow(keyField)
            '-- Extract the data field valuee
            Dim dataValue As Object = dRow(dataField)
            '-- Add a hashtable entry
            'jrs 2004/11/27 If Not TypeOf (dataValue) Is DBNull Then
            If Not TypeOf (dataValue) Is DBNull And Not TypeOf (keyValue) Is DBNull Then
                outputHashTable.Add(keyValue, dataValue)
            End If
        Next
        'Return the hashtable
        Return outputHashTable
    End Function

    'BuildDataTable
    'Build a datatable from the database named in the connection string using the query passed in the querystring

    Private Function BuildDataTable(ByVal connectionString As String, ByVal queryString As String) As DataTable
        'open a connection to the database
        Dim connection As New OleDbConnection(connectionString)
        Dim outputTable As New DataTable
        Try
            connection.Open()
            'create a data adapter
            Dim adapter As New OleDbDataAdapter(queryString, connection)
            'fill the data table
            adapter.Fill(outputTable)
        Finally
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        End Try
        'return the data table
        Return outputTable
    End Function

    'GetLayerValues
    'Return an array of values from the specified layer field
    Private Function GetLayerValues(ByVal layer As AspMap.Layer, ByVal field As String) As Double()
        Dim rs As AspMap.Recordset = layer.Recordset

        'jrs 09/29/04 begin
        Dim rsCounter As Integer = 0
        rs.MoveFirst()
        While Not rs.EOF
            If IsNumeric(rs(field)) Then
                rsCounter += 1
            End If
            rs.MoveNext()
        End While
        'jrs 09/29/04 end

        'jrs 09/29/04 Dim layerValues(rs.RecordCount) As Double
        Dim layerValues(rsCounter) As Double

        rs.MoveFirst()
        'jrs 09/29/04         Dim rsCounter As Integer = 0
        rsCounter = 0
        While Not rs.EOF
            If IsNumeric(rs(field)) Then
                layerValues(rsCounter) = rs(field)
                'jrs 09/29/04
                rsCounter += 1
            End If
            'jrs 09/29/04 rsCounter += 1
            rs.MoveNext()
        End While
        Return layerValues
    End Function

    'BuildColorRamp
    'Build a color ramp with specified start and end points

    Private Function BuildcolorRamp(ByVal numberOfColors As Integer, ByVal startValue As Integer, ByVal endValue As Integer, ByVal rampType As ColorRampType) As Integer()
        Dim outputColors(numberOfColors - 1) As Integer
        Dim cRamp As New ColorRamp
        Dim colorList() As Integer = cRamp.BuildColorRamp(numberOfColors, startValue, endValue)
        Dim i As Integer
        For i = 0 To outputColors.Length - 1
            Select Case rampType
                Case ColorRampType.Red
                    outputColors(i) = RGB(colorList(i), 0, 0)
                Case ColorRampType.Green
                    outputColors(i) = RGB(0, colorList(i), 0)
                Case ColorRampType.Blue
                    outputColors(i) = RGB(0, 0, colorList(i))
                Case ColorRampType.Gray
                    outputColors(i) = RGB(colorList(i), colorList(i), colorList(i))
            End Select
        Next
        Return outputColors
    End Function

    'BuildRenderer
    'Given a list of breakpoints, build a renderer for the specified layer with a color ramp,
    'using the given field 

    Private Sub BuildRenderer(ByVal classBreaks() As Double, ByVal layer As AspMap.Layer, ByVal field As String, ByVal colRampType As ColorRampType)
        Dim renderer As FeatureRenderer = layer.Renderer
        renderer.Clear()

        Dim colorList() As Integer = BuildcolorRamp(classBreaks.Length - 1, 240, 50, colRampType)

        Dim firstBreak As Feature = renderer.Add
        firstBreak.Expression = String.Format("{0} >= {1} AND {0} <= {2}", field, classBreaks(0), classBreaks(1))
        firstBreak.Symbol.FillColor = colorList(0)
        firstBreak.Symbol.Size = 1

        Dim break, nextBreak As Double
        Dim breakCounter As Integer
        Dim breakFeature As Feature
        For breakCounter = 1 To classBreaks.Length - 2
            break = classBreaks(breakCounter)
            nextBreak = classBreaks(breakCounter + 1)
            breakFeature = New Feature
            breakFeature = renderer.Add
            breakFeature.Expression = String.Format("{0} > {1} AND {0} <= {2}", field, break, nextBreak)
            breakFeature.Symbol.FillColor = colorList(breakCounter)
            breakFeature.Symbol.Size = 1
        Next
        'Symbolize "NO DATA" values
        Dim noDataFeature As Feature
        noDataFeature = renderer.Add
        noDataFeature.Expression = String.Format("{0} = NO DATA", field)
        noDataFeature.Symbol.FillColor = RGB(244, 238, 220)
        noDataFeature.Symbol.Size = 1
    End Sub

    Private Sub BuildUniqueValueRenderer(ByVal classBreaks() As Double, ByVal layer As AspMap.Layer, ByVal field As String, ByVal colRampType As ColorRampType)
        Dim renderer As FeatureRenderer = layer.Renderer
        renderer.Clear()
        Dim cRamp As New ColorRamp
        'JRS 09/08/04 Dim colorList() As Integer = cRamp.BuildColorRamp(classBreaks.Length - 1, 240, 100)
        Dim colorList() As Integer = BuildcolorRamp(classBreaks.Length - 1, 240, 50, colRampType)
        Dim break As Double
        Dim breakCounter As Integer
        Dim breakFeature As Feature
        For breakCounter = 0 To classBreaks.Length - 2 'JRS 09/08/04 - 1
            break = classBreaks(breakCounter)
            breakFeature = renderer.Add
            breakFeature.Expression = String.Format("{0} = {1}", field, break)
            breakFeature.Symbol.FillColor = colorList(breakCounter)
            breakFeature.Symbol.Size = 1
        Next
        'Symbolize "NO DATA"
        Dim noDataFeature As Feature = renderer.Add
        noDataFeature.Expression = String.Format("{0} = NO DATA", field)
        noDataFeature.Symbol.FillColor = RGB(244, 238, 220)
        noDataFeature.Symbol.Size = 1
    End Sub



    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        'jrs 2008/06/24
        Session.Remove("sessionMap")
        Session.Remove("maplstPts")
        Session.Remove("maplstEvts")
        Session.Remove("rsMap")
        Session.Remove("MapChosenTable")
        Session.Remove("DataBaseFile")
        'jrs 2011/06/24
        Session.Remove("DataDictVars")
        'jrs 2011/11/02
        Session.Remove("MapColor")
        Session.Remove("MapValueTable")
        Session.Remove("MapNoCat")
        Session.Remove("MapDisplayType")
        Session.Remove("MapProjection")

        'JAM 2017-10-30 Removing changing categories variables
        Session.Remove("RangeChanged")
        Session.Remove("RangeLimits")
        Session.Remove("RangeLimitsStart")
        Session.Remove("RangeLimitsEnd")
        Session.Remove("MapNoCat")


        Support.FormManager.Show(Me, Session("mapParent"))
    End Sub


    'JRS 08/30/04
    Sub UpdateFileComboBox()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        '   Add items to ComboBox (cbo) for data files upon load
        '   ----------------------------------------------------
        drpDataTable.Items.Clear()
        With drpDataTable
            .Items.Add(New ListItem("Agriculture"))
            .Items.Add(New ListItem("AgriOutput"))
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then .Items.Add(New ListItem("Automobiles"))
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then .Items.Add(New ListItem("AutoOutput"))
            .Items.Add(New ListItem("Economic"))
            .Items.Add(New ListItem("EcoOutput"))
            .Items.Add(New ListItem("Energy"))
            .Items.Add(New ListItem("EnergyOutput"))
            .Items.Add(New ListItem("Population"))
            .Items.Add(New ListItem("PopOutput"))
            .Items.Add(New ListItem("Social"))
            .Items.Add(New ListItem("SocialOutput"))
            .Items.Add(New ListItem("Transportation"))
            .Items.Add(New ListItem("TimeSeries"))
            If UCase(ModelLoad) = UCase("LoadCoVaTrA") Then .Items.Add(New ListItem("TimeSeriesAuto"))
            '        .AddItem "WVSTotalWave1"
            '        .AddItem "WVSTotalWave2"
            '        .AddItem "WVSTotalWave3"
            .Items.Add(New ListItem("WVSCohortData"))
        End With
        Dim i As Integer
        For i = 0 To drpDataTable.Items.Count - 1
            'jrs 09/14/04
            If drpDataTable.Items(i).Text = "TimeSeries" Then
                drpDataTable.SelectedIndex = i
                Exit For
            End If
        Next i



    End Sub

    'JRS 08/30/04
    Private Sub UpdateVariableComboBox()
        Dim Definition As String
        Dim VarName As String
        Dim i2 As Object
        Dim defnfound As String
        Dim loopf As Short
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -


        'JRSVBNET frm_Map2.DefInstance.Data1.DatabaseName = DataBaseFile
        'JRSVBNET frm_Map2.DefInstance.Data1.RecordSource = Session("MapChosenTable")
        'JRSVBNET frm_Map2.DefInstance.Data1.Refresh()
        Dim rs As DataTable
        'jrs 09/16/04
        SetDataTablefromAccess(DataBaseFile, "SELECT * FROM [" & Session("MapChosenTable") & "]", "", rs)

        drpDataField.Items.Clear()
        Dim sUnits As String
        For loopf = 1 To rs.Columns.Count - 1
            defnfound = CStr(False)
            f = rs.Columns(loopf)

            For i2 = 1 To NumbVars 'Check the standard IFsVar fields for variable name
                If UCase(V(i2)) = UCase(f.ColumnName) Then
                    drpDataField.Items.Add(New ListItem(f.ColumnName & " - " & VDef(i2) & " - " & VUnits(i2)))
                    defnfound = CStr(True)
                    Exit For
                End If
            Next i2

            If CBool(defnfound) = False Then 'Wasn't in IFsVar.MDB, so check DataDict.MDB for name
                '            Set db = OpenDatabase(FDRV$ + "Data\DataDict.MDB", , True)
                'jrs 09/16/04
                SetDataTablefromAccess(Session("PutFdrvRun") & "DataDict.MDB", "SELECT * FROM DataDict ORDER BY Variable", "", valtbl)
                Dim i As Integer
                i = 0
                Do
                    VarName = valtbl.Rows(i).Item("Variable")
                    Definition = valtbl.Rows(i).Item("Definition")
                    If IsDBNull(valtbl.Rows(i).Item("Units")) Then
                        sUnits = ""
                    Else
                        sUnits = valtbl.Rows(i).Item("Units")
                    End If
                    If UCase(VarName) = UCase(f.ColumnName) Then
                        'JRS 08/29/03
                        drpDataField.Items.Add(New ListItem(f.ColumnName & " - " & Definition & " - " & sUnits))
                        defnfound = CStr(True)
                        Exit Do
                    End If
                    i = i + 1
                    If valtbl.Rows.Count = i Then Exit Do
                Loop
            End If

            If CBool(defnfound) = False And Not f.ColumnName = "FIPS_CODE" Then 'Defn never found, add anyway
                drpDataField.Items.Add(New ListItem(f.ColumnName))
            End If

            '        drpDataField.AddItem f.ColumnName
        Next loopf
        drpDataField.SelectedIndex = 0
        Dim Instring As Integer
        Session("MapFieldName") = drpDataField.SelectedValue
        Instring = InStr(Session("MapFieldName"), " - ")
        If Instring > 1 Then 'strip off variable definition
            Session("MapFieldName") = Left(Session("MapFieldName"), Instring - 1)
        End If
    End Sub

    'JRS 08/30/04
    Private Sub drpDataField_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles drpDataField.SelectedIndexChanged

        Dim Instring As Integer
        Session("MapFieldName") = drpDataField.SelectedValue
        Instring = InStr(Session("MapFieldName"), " - ")
        If Instring > 1 Then 'strip off variable definition
            Session("MapFieldName") = Left(Session("MapFieldName"), Instring - 1)
        End If

        If drpDataTable.SelectedValue = "TimeSeries" Or
             drpDataTable.SelectedValue = "TimeSeriesAuto" Then

            '       Make Combination Box for Year Visible
            '       -------------------------------------
            drpDimension.Visible = True
            lblDimension.Visible = True

            '       Set up Database and Table Locations
            '       -----------------------------------

            Session("MapChosenTable") = "Series" + drpDataField.SelectedValue
            Instring = InStr(Session("MapChosenTable"), " - ")
            If Instring > 1 Then 'strip off variable definition
                Session("MapChosenTable") = Left(Session("MapChosenTable"), Instring - 1)
            End If

            '       Go to Subroutine to Relate Table and Identify Series or Cohort Dimensions
            '       -------------------------------------------------------------------------
            Call RelateSeriesCohort()
            'jrs 2011/06/24 drpNumberOfClasses.Visible = True
            'jrs 2011/06/24 drpDisplayType.Visible = True
            'JRS 2005/01/15 Call RepaintMap()

        ElseIf drpDataTable.SelectedValue = "WVSCohortData" Then

            '       Set up Database and Table Locations
            '       -----------------------------------

            DataBaseFile = Session("IFsWVSCohort_MDB")
            Session("DataBaseFile") = DataBaseFile
            Session("MapChosenTable") = drpDataField.SelectedValue
            Instring = InStr(Session("MapChosenTable"), " - ")
            If Instring > 1 Then 'strip off variable definition
                Session("MapChosenTable") = Left(Session("MapChosenTable"), Instring - 1)
            End If

            '       Make Combination Boxes for Year and Cohort Visible
            '       --------------------------------------------------
            drpDimension.Visible = True
            lblDimension.Visible = True
            drpWave.Visible = True
            lblWave.Visible = True

            '       Fill Wave Combination Box
            '       -------------------------
            drpWave.Items.Clear()

            Dim iloop As Integer
            Dim tablenamewave As String
            Dim smapchosentable As String
            For iloop = 1 To NWVSWaves '3 'add wave name when wave table exists

                tablenamewave = Session("MapChosenTable") + "W" + Trim(Str(iloop))
                On Error GoTo TryNextWave
                'jrs 09/16/04
                SetDataTablefromAccess(Session("IFsWVSCohort_MDB"), "SELECT * FROM [" & tablenamewave & "]", "", valtbl)
                'jrs 11/11/04
                If Not (valtbl Is Nothing) Then
                    drpWave.Items.Add("Wave " + Trim(Str(iloop)))
                    smapchosentable = tablenamewave
                    drpWave.SelectedIndex = drpWave.Items.Count - 1
                End If

TryNextWave:
                'jrs 11/11/04
                Err.Clear()
            Next iloop

            Session("MapChosenTable") = smapchosentable
            Dim Msg As String
            If Session("MapChosenTable") = "" Then
                Msg = "No Valid Waves of Data found for this variable."
                'JRSVBNET MsgBox(Msg)
                Exit Sub
            End If

            '       Go to Subroutine to Relate Table and Identify Series or Cohort Dimensions
            '       -------------------------------------------------------------------------
            Call RelateSeriesCohort()
            'jrs 2011/06/24 drpNumberOfClasses.Visible = True
            'jrs 2011/06/24 drpDisplayType.Visible = True
            'JRS 2005/01/15 Call RepaintMap()

        Else
            'jrs 2011/06/24 drpNumberOfClasses.Visible = True
            'jrs 2011/06/24 drpDisplayType.Visible = True
            'JRS 2005/01/15 Call RepaintMap()

        End If

        'jrs 2005/03/10
        RepaintMap()


    End Sub

    'JRS 08/30/04
    Private Sub RelateSeriesCohort()
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        Dim f As DataColumn
        Dim rs As DataTable
        Dim pw As String
        If DataBaseFile = Session("IFsHistSeries_MDB") Then
            pw = DataPW
        End If
        'jrs 09/16/04
        SetDataTablefromAccess(DataBaseFile, "SELECT * FROM [" & Session("MapChosenTable") & "]", "", rs, pw)
        'jrs 2005/03/24
        If rs Is Nothing Then
            Dim Message As String
            Message = "This variable doesn't have available data, please contact the System Administrator."
            LoadMessage(Message, Session("Message"))
            Support.FormManager.StateStore(Me)
            Support.FormManager.Show(Me, "frm_Message.aspx")
        End If
        drpDimension.Items.Clear()
        For Each f In rs.Columns
            'add variables to combobox; add definition if found
            If drpDataTable.SelectedValue = "TimeSeries" Or
                 drpDataTable.SelectedValue = "TimeSeriesAuto" Then
                'JRS 2/2/02
                If Not (f.Caption = "FIPS_CODE" Or f.Caption = "Country") Then 'If Val(f.Name) > 1900 And Val(f.Name) < 2020 Then
                    drpDimension.Items.Add(f.Caption)
                End If
            ElseIf InStr(f.Caption, "Cohort") Or f.Caption = "Total" Then
                drpDimension.Items.Add(f.Caption)
            End If
        Next f   'All fields
        'jrs 09/29/04
        drpDimension.SelectedIndex = drpDimension.Items.Count - 1

    End Sub

    'JRS 08/30/04
    Private Sub RepaintMap()
        'jrs 2011/06/21
        If bReLoadMap = False Then
            Exit Sub
        End If

        '   Identify field name within Related table from which to draw data
        '   ----------------------------------------------------------------

        If drpDataTable.SelectedValue = "TimeSeries" Or
                drpDataTable.SelectedValue = "TimeSeriesAuto" Then
            Session("MapFieldName") = drpDimension.SelectedValue
            'JRSVBNET frm_Map2.lbl_Variable = "Variable Name: " + Carret + _
            'JRSVBNET     drpDataField.SelectedValue + Carret + Session("MapFieldName")$
        ElseIf drpDataTable.SelectedValue = "WVSCohortData" Then
            Session("MapFieldName") = drpDimension.SelectedValue
            'JRSVBNET frm_Map2.lbl_Variable = "Variable Name: " + Carret + _
            'JRSVBNET drpDataField.SelectedValue + Carret + Session("MapFieldName")$
        ElseIf Session("MapChosenTable") <> "Map" Then 'jrs 09/30/04
            Session("MapFieldName") = drpDataField.SelectedValue
            'JRSVBNET frm_Map2.lbl_Variable = "Variable Name: " + Carret + Session("MapFieldName")$
        End If

        Dim InString As Integer
        InString = InStr(Session("MapFieldName"), " - ")
        If InString > 1 Then 'strip off variable definition
            Session("MapFieldName") = Left(Session("MapFieldName"), InString - 1)
        End If


        'JRSVBNET Dim dataValueTable As DataTable = BuildDataTable(CONNECTION_STRING, "SELECT FIPS_CODE,[" & Session("MapFieldName") & "] FROM " & Session("MapChosenTable"))
        Dim dataValueTable As DataTable
        Dim pw As String
        If DataBaseFile = Session("IFsHistSeries_MDB") Then
            pw = DataPW
        End If
        'jrs 2004/11/27
        If Session("MapChosenTable") <> "Map" Then
            'jrs 09/16/04
            SetDataTablefromAccess(DataBaseFile, "SELECT FIPS_CODE,[" & Session("MapFieldName") & "], Country FROM [" & Session("MapChosenTable") & "]", "", dataValueTable, pw)
        Else
            dataValueTable = Session("rsMap")
        End If

        'jrs 2011/06/21
        PackJScritpBuilder(dataValueTable)

    End Sub

    'JRS 08/30/04
    Private Sub drpDataTable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpDataTable.SelectedIndexChanged
        Dim InString As Object
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

        '   Return to Smaller Map Parameters form as default
        '   ------------------------------------------------

        drpDimension.Visible = False
        lblDimension.Visible = False
        drpWave.Visible = False
        lblWave.Visible = False
        'jrs 2005/04/01
        cmdDataInfo.Visible = (drpDataTable.SelectedValue = "TimeSeries")

        '   Identify table or file (Series/Cohort) chosen from DataFile combo box
        '   ---------------------------------------------------------------------

        If drpDataTable.SelectedValue = "TimeSeries" Or drpDataTable.SelectedValue = "TimeSeriesAuto" Then
            If drpDataTable.SelectedValue = "TimeSeries" Then
                DataBaseFile = Session("IFsHistSeries_MDB") '+ ", False, False," + DataPW$
            Else
                DataBaseFile = Session("IFsCoVaTrASeries_MDB")
            End If
            Session("DataBaseFile") = DataBaseFile
            Call SetUpSeriesCohort() 'File chosen; must set up table choice

            '       Make Combination Box for Year Visible
            '       -------------------------------------
            drpDimension.Visible = True
            lblDimension.Visible = True

            '       Set up Database and Table Locations
            '       -----------------------------------

            Session("MapChosenTable") = "Series" & drpDataField.SelectedValue
            InString = InStr(Session("MapChosenTable"), " - ")
            If InString > 1 Then 'strip off variable definition
                Session("MapChosenTable") = Left(Session("MapChosenTable"), InString - 1)
            End If

            '       Go to Subroutine to Relate Table and Identify Series or Cohort Dimensions
            '       -------------------------------------------------------------------------

            Call RelateSeriesCohort()
            'jrs 2011/06/24 drpNumberOfClasses.Visible = True
            'jrs 2011/06/24 drpDisplayType.Visible = True
            'JRS 2005/01/15 Call RepaintMap()
            Exit Sub
        ElseIf drpDataTable.SelectedValue = "WVSCohortData" Then
            DataBaseFile = Session("IFsWVSCohort_MDB")
            Session("DataBaseFile") = DataBaseFile
            Call SetUpSeriesCohort() 'File chosen; must set up table choice

            '       Make Combination Boxes for Year and Cohort Visible
            '       --------------------------------------------------

            drpDimension.Visible = True
            lblDimension.Visible = True
            drpWave.Visible = True
            lblWave.Visible = True

            '       Fill Wave Combination Box
            '       -------------------------

            'jrs 2015/10/07 already filled in SetUpSeriesCohort
            'drpWave.Items.Clear()
            'drpWave.Items.Add(New ListItem("Wave 1"))
            'drpWave.Items.Add(New ListItem("Wave 2"))
            'drpWave.Items.Add(New ListItem("Wave 3"))
            'drpWave.Items.Add(New ListItem("Wave 4"))
            'drpWave.SelectedIndex = 2

            '       Set up Database and Table Locations
            '       -----------------------------------

            DataBaseFile = Session("IFsWVSCohort_MDB")
            Session("DataBaseFile") = DataBaseFile
            Session("MapChosenTable") = drpDataField.SelectedValue
            InString = InStr(Session("MapChosenTable"), " - ")
            If InString > 1 Then 'strip off variable definition
                Session("MapChosenTable") = Left(Session("MapChosenTable"), InString - 1)
            End If
            Session("MapChosenTable") = Session("MapChosenTable") & "W3"

            '       Go to Subroutine to Relate Table and Identify Series or Cohort Dimensions
            '       -------------------------------------------------------------------------
            Call RelateSeriesCohort()

            'jrs 2011/06/24 drpDisplayType.Visible = True
            'JRS 2005/01/15 Call RepaintMap()
            Exit Sub

        Else
            DataBaseFile = Session("IFs_MDB2Active")
            If drpDataTable.SelectedValue = "Automobiles" Or drpDataTable.SelectedValue = "AutoOutput" Then
                DataBaseFile = Session("IFsCoVaTrA_MDB2")
            End If
            Session("DataBaseFile") = DataBaseFile

            Session("MapChosenTable") = drpDataTable.SelectedValue
            Call UpdateVariableComboBox()
            'JRS 2005/03/10
            Call RepaintMap()

        End If

        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    'JRS 08/30/04
    Private Sub SetUpSeriesCohort()
        Dim FullName As String
        Dim Definition As String
        Dim Vehicle As String
        Dim Cohort As String
        Dim Series As String
        Dim VarName As String
        Dim TableName As String
        Dim firstfound As String
        '- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        'JRS 08/09/02
        Dim valtbl As DataTable

        drpDataField.Items.Clear()

        '   Search DataDict for List of Series or Cohort Variables
        '   -----------------------------------------------------

        'jrs 2023/08/09 added upper()
        'jrs 09/16/04
        SetDataTablefromSQLite(Session("PutFdrvRun") & "DataDict.MDB", "SELECT * FROM DataDict ORDER BY Upper(Variable)", "Variable", valtbl)
        Dim i As Integer
        i = 0
        firstfound = CStr(False)
        'JRS 08/29/03
        Dim sUnits As String
        Do
            TableName = valtbl.Rows(i).Item("Table")
            VarName = valtbl.Rows(i).Item("Variable")
            Series = valtbl.Rows(i).Item("Series")
            Cohort = valtbl.Rows(i).Item("Cohort")
            Vehicle = valtbl.Rows(i).Item("CoVaTrA")
            Definition = valtbl.Rows(i).Item("Definition")
            'JRS 08/29/03
            If IsDBNull(valtbl.Rows(i).Item("Units")) Then
                sUnits = ""
            Else
                sUnits = valtbl.Rows(i).Item("Units")
            End If
            FullName = VarName & " - " & Definition & " - " & sUnits

            '       Put Year or Cohort Variables into Variable Name/Data Field Box
            '       --------------------------------------------------------------

            If DataBaseFile = Session("IFsHistSeries_MDB") And Series = "Yes" And Vehicle = "No" Then
                drpDataField.Items.Add(New ListItem(FullName))
                If CBool(firstfound) = False Then
                    firstfound = CStr(True)
                    drpDataField.SelectedIndex = 0
                    drpDataField_SelectedIndexChanged(Me, New EventArgs)
                End If
            End If

            If DataBaseFile = Session("IFsCoVaTrASeries_MDB") And Vehicle = "Yes" Then
                drpDataField.Items.Add(New ListItem(FullName))
                If CBool(firstfound) = False Then
                    firstfound = CStr(True)
                    drpDataField.SelectedIndex = 0
                    drpDataField_SelectedIndexChanged(Me, New EventArgs)
                End If
            End If


            If DataBaseFile = Session("IFsWVSCohort_MDB") And Cohort = "Yes" Then
                drpDataField.Items.Add(New ListItem(FullName))
                If CBool(firstfound) = False Then
                    firstfound = CStr(True)
                    drpDataField.SelectedIndex = 0
                    drpDataField_SelectedIndexChanged(Me, New EventArgs)
                End If
            End If

            i = i + 1
            If valtbl.Rows.Count = i Then Exit Do
        Loop


        'System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub drpDimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpDimension.SelectedIndexChanged
        'jrs 09/20/04
        'JRS 2005/01/15 Call RepaintMap()
    End Sub

    Private Sub drpWave_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpWave.SelectedIndexChanged
        'jrs 09/20/04
        Session("MapChosenTable") = drpDataField.SelectedValue
        Dim InString As Integer
        InString = InStr(Session("MapChosenTable"), " - ")
        If InString > 1 Then 'strip off variable definition
            Session("MapChosenTable") = Left(Session("MapChosenTable"), InString - 1)
        End If

        '   Use Wave Selection to Rename Table
        '   ----------------------------------

        If drpWave.SelectedValue = "Wave 1" Then
            Session("MapChosenTable") = Session("MapChosenTable") + "W1"
        ElseIf drpWave.SelectedValue = "Wave 2" Then
            Session("MapChosenTable") = Session("MapChosenTable") + "W2"
        ElseIf drpWave.SelectedValue = "Wave 3" Then
            Session("MapChosenTable") = Session("MapChosenTable") + "W3"
        ElseIf drpWave.SelectedValue = "Wave 4" Then
            Session("MapChosenTable") = Session("MapChosenTable") + "W4"
        Else
            Session("MapChosenTable") = Session("MapChosenTable") + "W5"
        End If

        '   Relate Appropriate Wave Table and Repaint Map
        '   ---------------------------------------------

        'JRS 2005/01/15 Call RepaintMap()
    End Sub

    'jrs 09/30/04
    Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        'jrs 2006/08/24 Refresh if previous Map was empty
        Dim bRepaint As Boolean
        Dim py As Integer
        Dim ly As Integer
        bRepaint = (Session("MaxValue") = -1000000)

        Session("PYear") = Session("PYear") + 5

        Session("PYear") = Amin(Session("FinYear")(1), Val(Session("PYear")))
        Session("MapYear") = Str(Session("FirstYear") + Session("PYear") - 1)
        lblYear.Text = "Year = " + Session("MapYear")
        lblYear.Visible = True
        hdfld.Value = Session("LastYearForecast").ToString.Trim + "|" + Session("MapYear").ToString.Trim   'KIA added on 1/28
        '   Fill Data File With Country-Specific Data From Model Run
        '   --------------------------------------------------------

        FillRegionMap2(Session("PLOT"), Session("PYear"), Session("GeoRegion"), Session("GR1N"), Session("GeoValue"),
            Session("IFs_MDB2Active"), Session("MaxValue"), Session("rsMap"), Me)
        'JRS 2005/01/15 Call RepaintMap()
        'jrs 2006/08/24
        py = Session("MapYear")
        ly = Session("LastYearForecast")
        If Not bRepaint Then
            bRepaint = (Session("MaxValue") = -1000000)
        End If
        If py <= ly Then 'bRepaint And 
            'jrs 2011/06/28
            bReLoadMap = True
            Call RepaintMap()

        End If
    End Sub

    'jrs 09/30/04
    Private Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        'jrs 2006/08/24 Refresh if previous Map was empty
        Dim bRepaint As Boolean
        bRepaint = (Session("MaxValue") = -1000000)

        Session("PYear") = Session("PYear") - 5
        If Session("PYear") < 1 Then Session("PYear") = 1
        Session("MapYear") = Str(Session("FirstYear") + Session("PYear") - 1)
        lblYear.Text = "Year = " + Session("MapYear")
        lblYear.Visible = True
        '   Fill Data File With Country-Specific Data From Model Run
        '   --------------------------------------------------------
        hdfld.Value = Session("LastYearForecast").ToString.Trim + "|" + Session("MapYear").ToString.Trim   'KIA added on 1/28
        FillRegionMap2(Session("PLOT"), Session("PYear"), Session("GeoRegion"), Session("GR1N"),
            Session("GeoValue"), Session("IFs_MDB2Active"), Session("MaxValue"), Session("rsMap"), Me)
        'JRS 2005/01/15 Call RepaintMap()
        'jrs 2006/08/24
        If Not bRepaint Then
            bRepaint = (Session("MaxValue") = -1000000)
        End If
        'If bRepaint Then
        'jrs 2011/06/28
        bReLoadMap = True

        Call RepaintMap()
        'End If
    End Sub

    'jrs 09/30/04
    Private Sub cmdFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFirst.Click
        'jrs 2006/08/24 Refresh if previous Map was empty
        Dim bRepaint As Boolean
        bRepaint = (Session("MaxValue") = -1000000)

        Session("PYear") = 1
        Session("MapYear") = Str(Session("FirstYear") + Session("PYear") - 1)
        lblYear.Text = "Year = " + Session("MapYear")
        lblYear.Visible = True
        '   Fill Data File With Country-Specific Data From Model Run
        '   --------------------------------------------------------
        hdfld.Value = Session("LastYearForecast").ToString.Trim + "|" + Session("MapYear").ToString.Trim   'KIA added on 1/28
        FillRegionMap2(Session("PLOT"), Session("PYear"), Session("GeoRegion"), Session("GR1N"), Session("GeoValue"),
            Session("IFs_MDB2Active"), Session("MaxValue"), Session("rsMap"), Me)
        'JRS 2005/01/15 Call RepaintMap()
        'jrs 2006/08/24
        If Not bRepaint Then
            bRepaint = (Session("MaxValue") = -1000000)
        End If
        'If bRepaint Then
        'jrs 2011/06/28
        bReLoadMap = True

        Call RepaintMap()
        'End If
    End Sub

    'jrs 09/30/04
    Private Sub cmdLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLast.Click
        'jrs 2006/08/24 Refresh if previous Map was empty
        Dim bRepaint As Boolean
        bRepaint = (Session("MaxValue") = -1000000)

        Session("PYear") = Amin(Session("FinYear")(1), Session("LastYear") - Session("FirstYear") + 1)
        Session("MapYear") = Str(Session("FirstYear") + Session("PYear") - 1)
        lblYear.Text = "Year = " + Session("MapYear")
        lblYear.Visible = True
        '   Fill Data File With Country-Specific Data From Model Run
        '   --------------------------------------------------------
        hdfld.Value = Session("LastYearForecast").ToString.Trim + "|" + Session("MapYear").ToString.Trim   'KIA added on 1/28  
        FillRegionMap2(Session("PLOT"), Session("PYear"), Session("GeoRegion"), Session("GR1N"),
            Session("GeoValue"), Session("IFs_MDB2Active"), Session("MaxValue"), Session("rsMap"), Me)
        'JRS 2005/01/15 Call RepaintMap()
        'jrs 2006/08/24
        If Not bRepaint Then
            bRepaint = (Session("MaxValue") = -1000000)
        End If
        'If bRepaint Then
        'jrs 2011/06/28
        bReLoadMap = True

        Call RepaintMap()
        'End If
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click

        'JAM 2017-10-30
        'jrs 2023/11/08 Session("RangeChanged") = False

        'JRS 2005/01/15
        Call RepaintMap()
    End Sub

    'jrs 2005/04/01
    Private Sub cmdDataInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDataInfo.Click
        Session("DataInfoIdx") = drpDataField.SelectedIndex
        Session("Scat_SE") = UCase(Mid(drpDataField.SelectedItem.Text, 1, InStr(drpDataField.SelectedValue, "-") - 2))

        'jrs 2011/06/24
        Dim db As String
        db = Session("PutFdrvRun") & "datadict.MDB"
        SetDataTablefromAccess(db, "Select * from DataDict order by Variable", "", valtbl)
        Session("DataDictVars") = valtbl

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "Availability.aspx?Scat_SE=" & Session("Scat_SE"))
    End Sub


    Protected Sub cmdFirst_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFirst.Load

    End Sub

    'jrs 2011/04/20
    Private Function BuildHashTableFromDataTable() As Hashtable
        Dim outputHashTable As New Hashtable
        Dim dTable As New DataTable
        Dim dRow As DataRow

        SetDataTablefromAccess(Session("PutFdrvRun") & "IFs.mdb", "SELECT Country, FIPS_CODE, NewRegion,20000 as ColorVal FROM RegionalizationFull ORDER BY Country", "Country", dTable)

        For Each dRow In dTable.Rows

            '-- Extract the key field value
            Dim keyValue As Object = dRow("FIPS_CODE")
            '-- Extract the data field value
            Dim dataValue As Object = dRow("NewRegion")
            '-- Add a hashtable entry
            'jrs 2004/11/27 If Not TypeOf (dataValue) Is DBNull Then
            If Not TypeOf (dataValue) Is DBNull And Not TypeOf (keyValue) Is DBNull Then
                outputHashTable.Add(keyValue, dataValue)
            End If
        Next
        'Return the hashtable
        Return outputHashTable
    End Function

    'jrs 2011/04/01
    'Procedure to build array with countries and values for Main Menu map
    Private Sub PackJScritpBuilder(ByRef dataValueTable As DataTable)
        On Error GoTo PackJScritpBuilder_Error 'JAM 2017-10-30

        'jrs 2011/11/02
        Session("MapColor") = drpColor.Text
        Session("MapValueTable") = dataValueTable
        Session("MapNoCat") = drpNumberOfClasses.Text
        Session("MapDisplayType") = drpDisplayType.Text
        Session("MapProjection") = drpProjection.Text



        Dim sVar As String
        If Session("MapChosenTable") = "Map" Then
            sVar = Session("VARN")(1, 1)
        Else
            sVar = Replace(Session("MapChosenTable"), "Series", "")
        End If

        'Sort MMValues
        'Create an array of columns
        Dim PlotRank() As DisplayColumn
        Dim ValTbl As DataTable
        ValTbl = Session("MapValueTable")
        Dim cc As Integer
        'jrs 2015/10/07
        If ValTbl Is Nothing Then
            cc = 0
        Else
            cc = ValTbl.Rows.Count
        End If
        ReDim PlotRank(cc)
        Dim i As Integer, RegIdx As Integer

        For i = 1 To cc
            PlotRank(i) = New DisplayColumn
            RegIdx = IFsRegs.Item(ValTbl.Rows(i - 1).Item("Country")).Index
            If RegIdx < 0 Then
                RegIdx = IFsRegs.Item(getRegionFromCtry(ValTbl.Rows(i - 1).Item("Country"))).Index
            End If
            PlotRank(i).Index = RegIdx
            PlotRank(i).Value = ValTbl.Rows(i - 1).Item(1)
            'to break ties
            PlotRank(i).Value2 = RegIdx
            'save original order
            PlotRank(i).Index2 = RegIdx
        Next i
        If cc > 0 Then
            'Sort the array
            QuickSortObj(PlotRank, 1, cc, False, True)
        End If


        Dim sJscript As String = ""

        sJscript = sJscript & "    var dataX = [" & vbCr
        sJscript = sJscript & PrepareDataForMap(PlotRank, cc)
        sJscript = sJscript & "];" & vbCr

        ClientScript.RegisterClientScriptBlock(Me.[GetType](), "LoadMMMapVars",
            sJscript, True)

        Dim sJscript2 As String

        sJscript2 = "<script type=""text/javascript"">" & vbCr

        sJscript2 = sJscript2 & "var myWidth = 0, myHeight = 0;" & vbCr
        sJscript2 = sJscript2 & "if( typeof( window.innerWidth ) == 'number' ) {" & vbCr
        'Non-IE
        sJscript2 = sJscript2 & "myWidth = window.innerWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = window.innerHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {" & vbCr
        '//IE 6+ in 'standards compliant mode'
        sJscript2 = sJscript2 & "myWidth = document.documentElement.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.documentElement.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "} else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {" & vbCr
        '//IE 4 compatible
        sJscript2 = sJscript2 & "myWidth = document.body.clientWidth;" & vbCr
        sJscript2 = sJscript2 & "myHeight = document.body.clientHeight;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "if (myWidth == 0)" & vbCr
        sJscript2 = sJscript2 & "{" & vbCr
        sJscript2 = sJscript2 & "myWidth = screen.width;" & vbCr
        sJscript2 = sJscript2 & "myHeight = screen.height;" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr

        sJscript2 = sJscript2 & "myWidth = myWidth * 0.95;" & vbCr
        sJscript2 = sJscript2 & "myHeight = myHeight * 0.90 - 100;" & vbCr

        sJscript2 = sJscript2 & "$(function () {" & vbCr

        sJscript2 = sJscript2 & "Highcharts.setOptions({" & vbCr
        sJscript2 = sJscript2 & "lang: {" & vbCr
        sJscript2 = sJscript2 & "contextButtonTitle: 'Options for Saving/Using'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr

        sJscript2 = sJscript2 & "$('#container').highcharts('Map', {" & vbCr
        sJscript2 = sJscript2 & "chart: {" & vbCr
        sJscript2 = sJscript2 & "height:   myHeight," & vbCr
        sJscript2 = sJscript2 & "zoomType: 'xy'," & vbCr
        sJscript2 = sJscript2 & "panning: true," & vbCr
        sJscript2 = sJscript2 & "panKey: 'shift'," & vbCr
        sJscript2 = sJscript2 & "borderWidth: 3," & vbCr
        sJscript2 = sJscript2 & "borderRadius: 15," & vbCr
        sJscript2 = sJscript2 & "spacingLeft: 60," & vbCr
        sJscript2 = sJscript2 & "events: {"
        sJscript2 = sJscript2 & "load: function(event) {"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.visibility = 'hidden';"
        sJscript2 = sJscript2 & "document.getElementById(""firstloading"").style.height = 0;"
        sJscript2 = sJscript2 & "}}"
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "title: {" & vbCr
        sJscript2 = sJscript2 & "text:       ''" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "credits: {" & vbCr
        sJscript2 = sJscript2 & "enabled:       false" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr

        sJscript2 = sJscript2 & "        mapNavigation: {" & vbCr
        sJscript2 = sJscript2 & "enabled: true," & vbCr
        sJscript2 = sJscript2 & "enableMouseWheelZoom: false," & vbCr
        sJscript2 = sJscript2 & "buttonOptions: {" & vbCr
        sJscript2 = sJscript2 & "align: 'right'" & vbCr
        sJscript2 = sJscript2 & "}" & vbCr
        sJscript2 = sJscript2 & "}," & vbCr


        Dim MMValLimits(Session("MapNoCat") - 1) As Single, j As Integer
        Dim MMValRange As Single = 0

        'jrs 2016/01/05
        If IsDBNull(PlotRank(1).Value) Then
            cc = 0
        End If

        If cc > 0 Then
            Do While IsDBNull(PlotRank(cc).Value) And cc > 1
                cc = cc - 1
            Loop
        End If

        If cc > 0 Then
            sJscript2 = sJscript2 & "                legend: {" & vbCr
            sJscript2 = sJscript2 & "title: {" & vbCr
            sJscript2 = sJscript2 & "text:       '" & sVar & "'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
            sJscript2 = sJscript2 & "layout:  'vertical'," & vbCr
            sJscript2 = sJscript2 & "align:  'left'," & vbCr
            sJscript2 = sJscript2 & "verticalAlign:  'bottom'," & vbCr
            sJscript2 = sJscript2 & "floating:  true," & vbCr
            sJscript2 = sJscript2 & "backgroundColor:  '#ffffff'" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr


            If Session("MapDisplayType") = "Equal Interval" Then
                MMValRange = (Val(PlotRank(1).Value) - Val(PlotRank(cc).Value)) / Session("MapNoCat")
                For j = 1 To Session("MapNoCat") - 1
                    MMValLimits(j) = Math.Round((Val(PlotRank(cc).Value) + j * MMValRange), Session("iRound"))
                    'JAM 2017-10-22
                    If Not Session("RangeChanged") Then 'JAM 2017-10-30 Keeping changed categories
                        Session("RangeLimits")(j) = MMValLimits(j).ToString("0.00")
                    End If

                    'JAM 2017-10-22
                    If Session("RangeChanged") Then
                        MMValLimits(j) = Val(Session("RangeLimitsEnd")(j))
                        'JAM 2017-10-30
                        Session("RangeLimits")(j) = Session("RangeLimitsEnd")(j)
                    End If

                Next
            Else
                MMValRange = cc / Session("MapNoCat")
                For j = 1 To Session("MapNoCat") - 1
                    MMValLimits(j) = Math.Round(Val(PlotRank((Session("MapNoCat") - j) * MMValRange).Value), Session("iRound"))
                    'JAM 2017-10-22
                    If Not Session("RangeChanged") Then 'JAM 2017-10-30 Keeping changed categories
                        Session("RangeLimits")(j) = MMValLimits(j).ToString("0.00")
                    End If

                    'JAM 2017-10-22
                    If Session("RangeChanged") Then
                        MMValLimits(j) = Math.Round(Val((Session("RangeLimitsEnd")(j)).Value), Session("iRound"))
                        Session("RangeLimits")(j) = Math.Round(Val((Session("RangeLimitsEnd")(j)).Value), Session("iRound")).ToString

                    End If

                Next
            End If


            sJscript2 = sJscript2 & "                colorAxis: {" & vbCr
            sJscript2 = sJscript2 & "                dataClasses: [" & vbCr
            sJscript2 = sJscript2 & "                {to: " & MMValLimits(1).ToString & "}," & vbCr
            For j = 2 To Session("MapNoCat") - 1
                sJscript2 = sJscript2 & "                {from: " & MMValLimits(j - 1).ToString & ", to: " & MMValLimits(j).ToString & "}," & vbCr
            Next
            sJscript2 = sJscript2 & "                {from: " & MMValLimits(Session("MapNoCat") - 1).ToString & "}" & vbCr
            sJscript2 = sJscript2 & "]," & vbCr
            If Session("MapColor") = "4" Then 'Sandy Brown
                sJscript2 = sJscript2 & "minColor: '#f7dcc8'," & vbCr
                sJscript2 = sJscript2 & "maxColor: '#f47200'" & vbCr
            ElseIf Session("MapColor") = "1" Then 'Green
                sJscript2 = sJscript2 & "minColor: '#98FF98'," & vbCr
                sJscript2 = sJscript2 & "maxColor: '#008000'" & vbCr
            ElseIf Session("MapColor") = "0" Then 'Red
                sJscript2 = sJscript2 & "minColor: '#FF0000'," & vbCr
                sJscript2 = sJscript2 & "maxColor: '#800000'" & vbCr
            ElseIf Session("MapColor") = "2" Then 'Blue
                sJscript2 = sJscript2 & "minColor: '#00FFFF'," & vbCr
                sJscript2 = sJscript2 & "maxColor: '#0000FF'" & vbCr
            Else 'Gray
                sJscript2 = sJscript2 & "minColor: '#D3D3D3'," & vbCr
                sJscript2 = sJscript2 & "maxColor: '#808080'" & vbCr
            End If
            sJscript2 = sJscript2 & "}," & vbCr

            sJscript2 = sJscript2 & "series : [{" & vbCr
            sJscript2 = sJscript2 & "mapData: Highcharts.maps['custom/world']},{" & vbCr
            sJscript2 = sJscript2 & "data: dataX," & vbCr
            sJscript2 = sJscript2 & "joinBy: ['ISO_2DIGIT', 'key']," & vbCr
            sJscript2 = sJscript2 & "mapData: Highcharts.maps['custom/world'], " & vbCr
            sJscript2 = sJscript2 & "name:   """ & sVar & """, " & vbCr
            sJscript2 = sJscript2 & "states: {" & vbCr
            sJscript2 = sJscript2 & "hover: {" & vbCr
            sJscript2 = sJscript2 & "color: '#ffff00'" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr
            sJscript2 = sJscript2 & "dataLabels: {" & vbCr
            sJscript2 = sJscript2 & "enabled: true," & vbCr
            sJscript2 = sJscript2 & "padding: 10," & vbCr
            sJscript2 = sJscript2 & "formatter: function () {" & vbCr
            sJscript2 = sJscript2 & "if (this.point.value > 0)" & vbCr
            sJscript2 = sJscript2 & "return this.point.properties['CNTRY_NAME'];" & vbCr
            sJscript2 = sJscript2 & "else" & vbCr
            sJscript2 = sJscript2 & "return '';" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}," & vbCr 'end of data Labels
            sJscript2 = sJscript2 & "tooltip: {" & vbCr
            sJscript2 = sJscript2 & "pointFormatter: function () {" & vbCr
            sJscript2 = sJscript2 & "return this.properties['UNSHRTNAM']+': '+this.value;" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr

            sJscript2 = sJscript2 & ", point: {" & vbCr
            sJscript2 = sJscript2 & "events: {" & vbCr
            sJscript2 = sJscript2 & "// On click, show popup menu" & vbCr
            sJscript2 = sJscript2 & "click: function () {" & vbCr
            sJscript2 = sJscript2 & "document.getElementById('txtGoogleCtry').value=this.properties['ISO_2DIGIT'];" & vbCr
            sJscript2 = sJscript2 & "countryCode = this.properties['ISO_2DIGIT'];" & vbCr
            sJscript2 = sJscript2 & "cswmShow('cntyInfoGroup', '', 'below', tempX, tempY, 1);" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
            sJscript2 = sJscript2 & "}" & vbCr
        Else
            sJscript2 = sJscript2 & "series : [{" & vbCr
            sJscript2 = sJscript2 & "mapData: Highcharts.maps['custom/world']" & vbCr
        End If

        sJscript2 = sJscript2 & "}]" & vbCr
        sJscript2 = sJscript2 & "}, function(chart){" & vbCr

        sJscript2 = sJscript2 & "chart.renderer.image('./Images/pcl.jpg', 10, 10, 60, 60)" & vbCr
        sJscript2 = sJscript2 & ".on('click', function () {" & vbCr
        sJscript2 = sJscript2 & "location.href = 'https://korbel.du.edu/pardee/';" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".attr({" & vbCr
        sJscript2 = sJscript2 & "zIndex: 7" & vbCr
        sJscript2 = sJscript2 & "})" & vbCr
        sJscript2 = sJscript2 & ".css({"
        sJscript2 = sJscript2 & "cursor: 'pointer'"
        sJscript2 = sJscript2 & "})"
        sJscript2 = sJscript2 & ".add();" & vbCr

        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "});" & vbCr
        sJscript2 = sJscript2 & "</script>"

        lblJS.Text = sJscript2

PackJScritpBuilder_Error:
        Exit Sub
    End Sub

    Private Function PrepareDataForMap(ByVal PlotRank As Object, ByVal cc As Integer) As String
        Dim jScript As String = "", i As Integer
        Dim iRound As Integer = 4
        If cc = 0 Then
            PrepareDataForMap = ""
            Exit Function
        End If
        'jrs 2016/01/04
        If Not IsDBNull(CType(PlotRank(1), DisplayColumn).Value) Then
            Select Case ABS_(Val(PlotRank(1).value))
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
        Else
            iRound = 2
        End If
        Session("iRound") = iRound
        Dim bFirst As Boolean = True
        For i = 1 To NR
            'jrs 2023/11/08 added and not Kiribati, not included in default map, other country names not showing because of that
            If Not IsDBNull(CType(PlotRank(i), DisplayColumn).Value) And CType(PlotRank(i), DisplayColumn).Index > -1 And
                    GRPS(1, CType(PlotRank(i), DisplayColumn).Index) <> "Kiribati" Then
                If Not bFirst Then
                    jScript = jScript & "," & vbCr
                Else
                    bFirst = False
                End If
                jScript = jScript & "{" & vbCr
                jScript = jScript & """key"": """ & IFsRegs.Item(GRPS(1, CType(PlotRank(i), DisplayColumn).Index)).ISO3166_2Code & """," & vbCr
                jScript = jScript & """value"": " & Math.Round(Val(CType(PlotRank(i), DisplayColumn).Value), iRound) & vbCr
                jScript = jScript & "}"

                Select Case IFsRegs.Item(GRPS(1, CType(PlotRank(i), DisplayColumn).Index)).ISO3166_2Code
                    Case "DK" 'Add Denmark (DK) data to Greenland (GL)
                        jScript = jScript & ",{" & vbCr
                        jScript = jScript & """key"": ""GL""," & vbCr
                        jScript = jScript & """value"": " & Math.Round(Val(CType(PlotRank(i), DisplayColumn).Value), iRound) & vbCr
                        jScript = jScript & "}"
                    Case "PS" 'Add Palestine's data to West Bank (currently only Gaza Strip)
                        jScript = jScript & ",{" & vbCr
                        jScript = jScript & """key"": ""WB""," & vbCr
                        jScript = jScript & """value"": " & Math.Round(Val(CType(PlotRank(i), DisplayColumn).Value), iRound) & vbCr
                        jScript = jScript & "}"
                End Select
            End If
        Next
        jScript = jScript & vbCr
        PrepareDataForMap = jScript
    End Function
    ''' <summary>
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' 
    '''       JAM 2017-10-22 Calling Categories definition Form
    ''' 
    ''' ''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdCategories_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCategories.Click
        On Error GoTo cmdCategories_Error 'JAM 2017-10-30

        Dim i As Integer
        Session("MapViewerRangeParent") = "frm_MapViewer.aspx"
        ReDim Session("RangeLimitsStart")(7)
        ReDim Session("RangeLimitsEnd")(7)

        Session("RangeLimitsStart")(1) = 0
        Session("RangeLimitsStart")(2) = Session("RangeLimits")(1)
        Session("RangeLimitsStart")(3) = Session("RangeLimits")(2)
        Session("RangeLimitsStart")(4) = Session("RangeLimits")(3)
        Session("RangeLimitsStart")(5) = Session("RangeLimits")(4)

        'JAM 2017-10-29 If there are more than five categories
        If Session("MapNoCat") > 5 Then
            Session("RangeLimitsStart")(6) = Session("RangeLimits")(5)
            If Session("MapNoCat") > 6 Then
                Session("RangeLimitsStart")(7) = Session("RangeLimits")(6)
            End If
        End If

        Session("RangeLimitsEnd")(1) = Session("RangeLimits")(1)
        Session("RangeLimitsEnd")(2) = Session("RangeLimits")(2)
        Session("RangeLimitsEnd")(3) = Session("RangeLimits")(3)
        Session("RangeLimitsEnd")(4) = Session("RangeLimits")(4)


        'JAM 2017-10-29 If there are more than five categories
        If Session("MapNoCat") > 5 Then
            If Session("MapNoCat") = 6 Then
                Session("RangeLimitsEnd")(5) = Session("RangeLimits")(5)
                Session("RangeLimitsEnd")(6) = "Above"
            Else
                If Session("MapNoCat") = 7 Then
                    Session("RangeLimitsEnd")(5) = Session("RangeLimits")(5)
                    Session("RangeLimitsEnd")(6) = Session("RangeLimits")(6)
                    Session("RangeLimitsEnd")(7) = "Above"
                End If
            End If
        Else
            Session("RangeLimitsEnd")(5) = "Above"
        End If

        Support.FormManager.StateStore(Me)
        Support.FormManager.Show(Me, "frm_MapViewerRanges.aspx")


cmdCategories_Error:
        Exit Sub

    End Sub
End Class
