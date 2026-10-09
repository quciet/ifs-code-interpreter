<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_CyberProfile" Codebehind="frm_CyberProfile.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
		<title>Cyber Risk Profile</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
       <%-- <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>--%>
        <script language ="javascript" type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/jquery-ui.js"></script>
         <link rel="stylesheet" href="Scripts/jquery-ui.css">

        <script language ="javascript" type="text/javascript" src="Scripts/autocomplete_vars.js"></script>  
        <script language ="javascript" type="text/javascript" src="Scripts/autocompleteFx.js"></script>
    

        <asp:Label ID="lblJS" runat="server"></asp:Label>

        <script language ="javascript" type="text/javascript" src="Scripts/bubble-dragndrop.js"></script>
    
        <script language ="javascript" type="text/javascript">
            // JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
            function Autotxt() {
                $("#autocompletetxt").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
                    source: VarListvse //availableTags
                });
            }
            //JAM 2015-05-17 Adding slider functionality
            //function Sliderutility() {
            //    $("#slider").slider(
            //    {
            //        value: 0,
            //        min: 0,
            //        max: 100,
            //        step: 10,
            //        slide: function (event, ui) {
            //            $("#percentage").val( ui.value + "%" );
            //        }
            //    }


            //    );
            //    $("#percentage").val($("#slider").slider("value") + "%");
            //}

            //JAM 2015-05-17 Adding all onload required functions
            function InitAll() {
                init();
                BuildMaps();
                Autotxt();
                BuildBubbles();
                //Sliderutility();
            }


        </script>

</head>
<body leftMargin="0" topMargin="0" rightMargin="0" onload="InitAll();"><%--JAM 2015-05-16 Loading javascript functions that will be used--%>
    <div id="loading" style="position:absolute; width:100%; text-align:center; top:300px;"><img src="http://www.ifs.du.edu/ifs/images/squares-circle.gif"  height="31" width="31"><br>Loading resources...</div>
<script type="text/javascript">

    var ld = (document.all);


    var ns4 = document.layers;
    var ns6 = document.getElementById && !document.all;
    var ie4 = document.all;


    if (ns4)
        ld = document.loading;
    else if (ns6)
        ld = document.getElementById("loading").style;
    else if (ie4)
        ld = document.all.loading.style;


    function init() {
        if (ns4) { ld.visibility = "hidden"; }
        else if (ns6 || ie4) ld.display = "none";
    }
</script>
    <form id="Form1" method="post" runat="server">

        <1--jam 2021/11/27-->
       <%-- <script src="http://code.highcharts.com/maps/highmaps.js"></script>--%>

         <script src="<%= Page.ResolveUrl("~/Scripts/highmaps.js")%>"></script>

       <%-- <script src="http://code.highcharts.com/mapdata/index.js?1"></script>--%>

        <script src="<%= Page.ResolveUrl("~/Scripts/index.js?1")%>"></script>

        <%--<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.js"></script>--%>
       <%-- <script src="http://www.highcharts.com/samples/maps/demo/all-maps/jquery.combobox.js"></script>--%>


         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.9.2/jquery-ui.js")%>"></script>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery.combobox.js")%>"></script>

       <%-- <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/themes/base/jquery-ui.css" rel="stylesheet">--%>
       <%-- <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet">--%>

        <link href="<%= Page.ResolveUrl("~/css/font-awesome.css")%>" rel="stylesheet">


        <table style="width:100%">
            <tr>
                <td style="width: 10%"></td>
                <td style="width: 80%">
                    <div id="containerbubble"></div> 
                </td>
                <td style="width: 10%"></td>
            </tr>
            <tr>
                <td style="width: 10%"></td>
                <td style="width: 80%">
                    <div id="demo-wrapper">
                        <div id="mapBox">
                            <div id="up"></div>
                            <div class="selector">
                                <select id="mapDropdown" class="ui-widget combobox"></select>
                            </div>
                            <div id="container"></div> 
                        </div>
                    </div>
                </td>
                <td style="width: 10%"></td>
            </tr>
            <tr>
                <td style="width: 10%"></td>
                <td style="width: 80%">
                    <div id ="play-controls">
                        <button id="play" >></button>
                        <button id="pause" >||</button>
                        <div id="slider"></div>
                    </div>
                </td>
                <td style="width: 10%"></td>
            </tr>
            <tr>
                <td style="width: 10%"></td>
                <td style="width: 80%">
                    <asp:TextBox ID="autocompletetxt" runat="server" Width="606px" AutoPostBack="True"></asp:TextBox>                            
                </td>
                <td style="width: 10%"></td>
            </tr>
        </table>
       <%-- JAM 2015-05-17 Adding slider functionality--%>
       </br>
       <label for="percentaget">Percentage (10% increments):</label>
       <input type="text" id="percentage" readonly style="border:0; color:#f6931f; font-weight:bold;">
    </form>
</body>
</html>
