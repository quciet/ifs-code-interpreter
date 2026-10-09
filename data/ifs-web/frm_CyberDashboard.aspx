<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_CyberDashboard" Codebehind="frm_CyberDashboard.aspx.vb" %>

<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../../favicon.ico">

    <title>Cyber Risk Dashboard</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
    <%-- <script src="Scripts/jquery-1.7.2.js" type="text/javascript"></script>--%>
    <script language="javascript" type="text/javascript" src="Scripts/jquery-1.10.2.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/jquery-ui.js"></script>
    <link rel="stylesheet" href="Scripts/jquery-ui.css">

    <script language="javascript" type="text/javascript" src="Scripts/autocomplete_vars.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/autocompleteFx.js"></script>

    <%--        <script language ="javascript" type="text/javascript" src="Maps/country.geo.json"></script>
        <script language ="javascript" type="text/javascript" src="Maps/country.js"></script>--%>

    <!--JAM 2015-05-31 multiselect autocomplete required files-->
    <%--         <script type="text/javascript" src="Scripts/tokenize/jquery.tokenize.js"></script>
        <link rel="stylesheet" type="text/css" href="Scripts/tokenize/jquery.tokenize.css" />        --%>
    <!--------------------------------------------->

    <%--JRS 2015/07/10 chosen --%>
    <link rel="stylesheet" href="Scripts/chosen/docsupport/prism.css">
    <link rel="stylesheet" href="Scripts/chosen/chosen.css">
    <%--JRS 2015/07/10 chosen --%>

    <!--JAM 2015-08-07 jquery widget required files-->
    <link rel="stylesheet" href="Scripts/jqwidgets/styles/jqx.base.css" type="text/css" />
    <%--  <script type="text/javascript" src="../../scripts/jquery-1.11.1.min.js"></script>--%>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxcombobox.js"></script>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="Scripts/jqwidgets/jqxcheckbox.js"></script>

    <!--JAM 2015-08-07 jquery widget required files-->
    <asp:Label ID="lblJS" runat="server"></asp:Label>

    <asp:Label ID="lblJSMap" runat="server"></asp:Label>
    <asp:Label ID="lblJSBubble" runat="server"></asp:Label>
    <asp:Label ID="lblJSArea" runat="server"></asp:Label>
    <asp:Label ID="lblJSline" runat="server"></asp:Label>
    <!-- jrs 2015/06/08
    <script language ="javascript" type="text/javascript" src="Scripts/bubble-dragndrop.js"></script>
        -->

    <script language="javascript" type="text/javascript">
        // JAM 2015-05-16 Changing function name in order to avoid unnamed funcion
        //function Autotxt() {
        //$("#txtVariables").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
        //    source: VarListvse //availableTags
        //});
        ////JAM 2015-05-24 For automatic postback when a selection is done
        //$("#txtVariables").autocomplete({
        //    close: function (event, ui) {
        //        if ($("#txtVariables")[0].value > "") {
        //            //window.location.reload(true); 
        //            __doPostBack("#txtVariables", "");
        //        }
        //    }
        //});

        //$("#txtVariables2").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
        //    source: VarListvse //availableTags
        //});
        ////JAM 2015-05-24 For automatic postback when a selection is done
        //$("#txtVariables2").autocomplete({
        //    close: function (event, ui) {

        //        if ($("#txtVariables2")[0].value > "") {
        //            //window.location.reload(true); 
        //            __doPostBack("#txtVariables2", "");
        //        }
        //    }
        //});

        //$("#txtScenarioSingle").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
        //    source: SceListNames //availableTags
        //});
        ////JAM 2015-05-24 For automatic postback when a selection is done
        //$("#txtScenarioSingle").autocomplete({
        //    close: function (event, ui) {
        //        if ($("#txtScenarioSingle")[0].value > "") {
        //            //window.location.reload(true); 
        //            __doPostBack("#txtScenarioSingle", "");
        //        }
        //    }
        //});

        //$("#txtScenarioSingle2").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
        //    source: SceListNames //availableTags
        //});
        ////JAM 2015-05-24 For automatic postback when a selection is done
        //$("#txtScenarioSingle2").autocomplete({
        //    close: function (event, ui) {
        //        if ($("#txtScenarioSingle2")[0].value > "") {
        //            //window.location.reload(true); 
        //            __doPostBack("#txtScenarioSingle2", "");
        //        }
        //    }
        //});

        //$("#txtScenarioSingle3").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
        //    source: SceListNames //availableTags
        //});
        ////JAM 2015-05-24 For automatic postback when a selection is done
        //$("#txtScenarioSingle3").autocomplete({
        //    close: function (event, ui) {
        //        if ($("#txtScenarioSingle3")[0].value > "") {
        //            //window.location.reload(true); 
        //            __doPostBack("#txtScenarioSingle3", "");
        //        }
        //    }
        //});

        //$("#txtRegionSingle").autocomplete({ // JAM 2015-05-08 field with autocomplete functionality
        //    source: VarRegions //availableTags
        //});
        ////JAM 2015-05-24 For automatic postback when a selection is done
        //$("#txtRegionSingle").autocomplete({
        //    close: function (event, ui) {
        //        if ($("#txtRegionSingle")[0].value > "") {
        //            //window.location.reload(true); 
        //            __doPostBack("#txtRegionSingle", "");
        //        }
        //    }
        //});
        //}
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
            //init();
            //scrollToElement($('#txtVariables2'));
            //$('#txtVariables2').scrollIntoView();
            //Autotxt();
            BuildBubbles();
            BuildArea();
            BuildMaps();
            BuildLine();

            //while (!$("#containermap").highcharts()) {                    
            //}
            //window.location.hash = "imp";
            //Sliderutility();
            //initautocomplete(); //JAM 2015-05-31 Adding multiselect field
        }

            //function scrollToElement(ele) {
            //    $(window).scrollTop(ele.offset().top).scrollLeft(ele.offset().left);
            //}

    </script>

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <!--jam 2021/11/27-->
    <%--<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">--%>

    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/css/font-awesome.min.css")%>">

    <link href="css/DBstyle.css" rel="stylesheet">
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" onload="InitAll();">
    <%--JAM 2015-05-16 Loading javascript functions that will be used--%>
    <header>
       <div class="container">
       <div class="row">
        <div class="col-lg-6">
         <img src="images/logo_1.png" alt="logo" >
        </div>
 
     <div class="col-lg-6">
     <div class="download">
         <h1>Download or use the International Futures model online for free at:</h1>
         <a href="https://korbel.du.edu/pardee/access-ifs">www.pardee.du.edu</a>
     </div>    
     </div>

       </div>
       </div>
   </header>

    <section class="future_pro">
       <div class="container">
       <div class="row">
        <div class="col-lg-12">
        <div class="future_heading">
        <h1>Economic Benefits and Costs of Alternate Cyber Futures</h1>
        <p>What does the future of cyber hold? Will the costs of cybercrime and espionage outpace the benefits of ever-increasing connectivity? Will a cyber Pearl Harbor attack undo years of additional cyber-driven economic growth and lead to a retrenchment of technology? Or, will a wave of new technological breakthroughs, such as artificial intelligence and the Internet of Things, bring about a new age of security and prosperity?</p>
        <p id="futhd2">This Cyber Risk Dashboard is designed to help you think about how the continually evolving digital landscape could develop between now and 2030. Access the Dashboard's features by either scrolling down or clicking on the links at the bottom of this page.</p>
    
        <p>In collaboration with <br />
            <img src="images/Zurich.png" width=300 alt=""/><br />
        
            <img src="images/AClogo.png" width=336 alt=""/>
        </p>

            <table style="width:100%">
                <tr>
                    <td style="width:5%"></td>
                    <td style="width:25%"><a href="#exp"><font color="white"><u>Exploring alternative futures</u></font></a></td>
                    <td style="width:25%"><a href="#pro"><font color="white"><u>Cyber Risk Profile</u></font></a></td>
                    <td style="width:25%"><a href="#glo"><font color="white"><u>Global Perspective</u></font></a></td>
                    <td style="width:20%"><a href="#imp"><font color="white"><u>See the implications</u></font></a></td>
                </tr>
            </table>
        </div>
        </div>
       </div>
       </div>
   </section>

    <script type="text/javascript">

        var ld = (document.all);


        var ns4 = document.layers;
        var ns6 = document.getElementById && !document.all;
        var ie4 = document.all;


        //if (ns4)
        //    ld = document.loading;
        //else if (ns6)
        //    ld = document.getElementById("loading").style;
        //else if (ie4)
        //    ld = document.all.loading.style;


        function init() {
            if (ns4) { ld.visibility = "hidden"; }
            else if (ns6 || ie4) ld.display = "none";
        }
    </script>
    <form id="Form1" method="post" runat="server">

        <%--
        <script src="http://code.highcharts.com/highcharts.js"></script>
        <script src="http://code.highcharts.com/highcharts-more.js"></script>
        --%>

        <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/highcharts-more.js"></script>
        <script src="Scripts/map.js"></script>
        <%-- <script src="http://code.highcharts.com/maps/modules/map.js"></script> --%>
        <%-- <script src="http://code.highcharts.com/maps/highmaps.js"></script> --%>
        <%--<script src="http://code.highcharts.com/mapdata/index.js?1"></script>--%>
        <!--jam 2021/11/27-->
        <script src="<%= Page.ResolveUrl("~/Scripts/index.js?1")%>"></script>
        <%--<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/jquery-ui.js"></script>--%>
      <%--  <script src="http://www.highcharts.com/samples/maps/demo/all-maps/jquery.combobox.js"></script>--%>


        <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.9.2/jquery-ui.js")%>"></script>
        <script src="<%= Page.ResolveUrl("~/Scripts/jquery.combobox.js")%>"></script>

        <%-- <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.2/themes/base/jquery-ui.css" rel="stylesheet">--%>
    <!--jam 2021-11-27-->
        <%--   <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet">--%>

        <link href="<%= Page.ResolveUrl("~/css/font-awesome.css")%>" rel="stylesheet">

        
        <section class="exproling">
   <div class="container">
   <div class="row">
   <div class="col-lg-12">
   <div class="expore_heading">
   <h1><a name="exp"></a>Exploring alternative futures</h1>
       <p>What will the cyber world look like in 2030? Will cybercriminals, hacktivists, spies, and terrorists dominate increasingly insecure global networks? Will the internet fragment under strong state control? Will advances in encryption and biometrics provide cheap and effective security to all who venture online? Has cyber innovation in ICT run its course, or have we only scratched the surface? The graph below shows how cyber risks and benefits change under various scenarios. Hover over the bubbles to see scenario descriptions. Subsequent pages help you further explore risks and benefits.</p>
   </div>
   </div>

   <div class="col-lg-6">
   <div class="base_case">
   <div class="input-group">
   </div>
   <div id="containerbubble" class="img-responsive" alt=""></div> 
   </div>
   </div>
   <div class="col-lg-6">
   <div id="bd" class="bas_deatails">
        <asp:Label ID="lblScenarioDesc" runat="server" Text="Description of the Scenario selected"></asp:Label>
        <script language ="javascript" type="text/javascript">
            showSceDesc('Independent Internet');
        </script>                                                                  
   </div>
   </div>
            <table style="width:100%">
                <tr>
                    <td style="width:5%"></td>
                    <td style="width:25%"><a href="#exp"><font color="grey"><u>Exploring alternative futures</u></font></a></td>
                    <td style="width:25%"><a href="#pro"><font color="black"><u>Cyber Risk Profile</u></font></a></td>
                    <td style="width:25%"><a href="#glo"><font color="black"><u>Global Perspective</u></font></a></td>
                    <td style="width:20%"><a href="#imp"><font color="black"><u>See the implications</u></font></a></td>
                </tr>
            </table>
   </div>
   </div>    
   </section>

        <section class="cyber_risk">
       <div class="container">
         <div class="row">
         <div class="col-lg-12">
         <div class="cyber_heading">
         <h1><a name="pro"></a>Cyber Risk Profile</h1>
             <p>Who stands to lose the most should cyberwarfare become the norm? What countries would benefit the most from a new wave of cyber technologies? Explore the economic costs and benefits of each scenario and compare how they differ across countries and regions by using the dropdown menus.</p>             
         </div>
         </div>

        <div class="col-lg-12">
            <div class="cyber_risk_opts">
                <table style="width:100%">
                    <tr>
                        <td style="width: 1%"></td>
                        <td style="width: 44%">
                            <asp:RadioButton ID="optCosts" runat="server" text="Costs "  GroupName="CostBenefit" Checked="false" ToolTip="The economic benefits from cyber technologies include the often rapid relative growth rates of cyber-producing sectors, the contributions to production, productivity (and therefore growth across the broader economy) from investments in cyber technologies, and consumer-captured surpluses from cost reductions as the technologies develop (i.e., surpluses not represented in standard economic measures such as gross domestic product). Costs include the spending required to defend against adverse cyber events stemming from hacking, cybercrime, cyber espionage, and cyber terror or war, the costs of such events themselves, and opportunity costs—the potential economic benefits not realized because of forgone use of cyber-technologies in fear of such events, or due to a lack of human capital and infrastructure capacity necessary to fully deploy cyber technologies.  All of these benefits and costs obviously depend on the changing technological landscape and decisions made by households, organizations including corporations, and governments with respect to cyber security, its use and abuse." />
                            <asp:Radiobutton ID="optBenefits" runat="server" text="Benefits " GroupName="CostBenefit" Checked="false" ToolTip="The economic benefits from cyber technologies include the often rapid relative growth rates of cyber-producing sectors, the contributions to production, productivity (and therefore growth across the broader economy) from investments in cyber technologies, and consumer-captured surpluses from cost reductions as the technologies develop (i.e., surpluses not represented in standard economic measures such as gross domestic product). Costs include the spending required to defend against adverse cyber events stemming from hacking, cybercrime, cyber espionage, and cyber terror or war, the costs of such events themselves, and opportunity costs—the potential economic benefits not realized because of forgone use of cyber-technologies in fear of such events, or due to a lack of human capital and infrastructure capacity necessary to fully deploy cyber technologies.  All of these benefits and costs obviously depend on the changing technological landscape and decisions made by households, organizations including corporations, and governments with respect to cyber security, its use and abuse." />
                            <asp:Radiobutton ID="optBoth" runat="server" text="Both" GroupName="CostBenefit" Checked ="true" ToolTip="The economic benefits from cyber technologies include the often rapid relative growth rates of cyber-producing sectors, the contributions to production, productivity (and therefore growth across the broader economy) from investments in cyber technologies, and consumer-captured surpluses from cost reductions as the technologies develop (i.e., surpluses not represented in standard economic measures such as gross domestic product). Costs include the spending required to defend against adverse cyber events stemming from hacking, cybercrime, cyber espionage, and cyber terror or war, the costs of such events themselves, and opportunity costs—the potential economic benefits not realized because of forgone use of cyber-technologies in fear of such events, or due to a lack of human capital and infrastructure capacity necessary to fully deploy cyber technologies.  All of these benefits and costs obviously depend on the changing technological landscape and decisions made by households, organizations including corporations, and governments with respect to cyber security, its use and abuse." />
                        </td>
                        <td style="width: 44%" align="right">
                            <asp:RadioButton ID="optAnnualized" runat="server" text="Annualized " GroupName="CummAnn" Checked="true" />
                            <asp:RadioButton ID="optCumulative" runat="server" text="Cumulative" GroupName="CummAnn" Checked="false" />                    
                        </td>
                        <td style="width: 1%"></td>
                    </tr>
                 </table>
            </div>
        </div>

        <div class="cyber_risk_in">
        <div class="input-group cyber_1">
        <asp:DropDownList ID="cboScenarioSingle2" runat="server" AutoPostBack="False" onchange="showSceDesc(this.text)" class="form-control" ></asp:DropDownList>
        </div>
       </div>

         <div class="cyber_risk_in_1">
         <div class="cyber_simple">
             <asp:DropDownList ID="cboRegionSingle" runat="server" AutoPostBack="False" onchange="" class="chosen-select" style="height: 40px;"></asp:DropDownList>
        </div>
       </div>
        

        <div class="input-group cyber_2">
            <br /><br /><br />
        </div>

       <div class="col-lg-12">
       <div class="cyber_img">
            <div id="containerarea"></div>
           <div class="input-group cyber_2">
        </div>
       </div>
       </div>


            <table style="width:100%">
                <tr>
                    <td style="width:5%"></td>
                    <td style="width:25%"><a href="#exp"><font color="white"><u>Exploring alternative futures</u></font></a></td>
                    <td style="width:25%"><a href="#pro"><font color="yellow"><u>Cyber Risk Profile</u></font></a></td>
                    <td style="width:25%"><a href="#glo"><font color="white"><u>Global Perspective</u></font></a></td>
                    <td style="width:20%"><a href="#imp"><font color="white"><u>See the implications</u></font></a></td>
                </tr>
            </table>



         </div>
       </div>
   </section>


        <section class="global">
       <div class="container">
        <div class="row">

           <div class="col-lg-12">
         <div class="global_heading">
         <h1><a name="glo"></a>Global Perspective</h1>
         <p>Choose a scenario and variable from the dropdown menus and then click '>' to watch the variable evolve across the world and over time.</p>
         </div>
         </div>


         <div class="global_field">

         <div class="input-group">
            <asp:DropDownList ID="cboScenarioSingle3" runat="server" AutoPostBack="False" onchange="showSceDesc(this.text)" class="form-control"></asp:DropDownList>
         </div>

         </div>


         <div class="global_field2">
            For a full variable description click here: <input id="cmdInfo" type="image" style="height:20px;width:20px;" src="images/info.png" onclick="showVarDesc($('#cboVariables').val()); return false;" />
             &nbsp;&nbsp;&nbsp;
         <div class="input-group2">
             <asp:DropDownList ID="cboVariables" runat="server" AutoPostBack="False" class="chosen-select" ></asp:DropDownList>
         </div>
         </div>

         <div class="progress_1">
            <table style="width:100%">
                <tr>
                    <td style="width: 25%" align="right">
                        <button id="play" >></button>
                        <button id="pause" >||</button>
                        <button id="advance" >+1 Year</button>
                        <button id="regress" >-1 Year</button>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                    <td colspan="2"  style="width: 70%">
                        <div id="slider"></div>
                    </td>
                    <td style="width: 5%"></td>
                </tr>
            </table>
        </div>

        <div id="firstloading" style="text-align:center; visibility:visible;">
            <img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...
        </div>                    

        <div class="col-lg-12">
        <div class="gdp">
            <div id="demo-wrapper">
                <div id="mapBox">
                    <div id="up"></div>
                    <div class="selector" hidden>
                        <select id="mapDropdown" class="ui-widget combobox"></select>
                    </div>
                    <div id="containermap"></div> 
                </div>
            </div>
        </div>
            <table style="width:100%">
                <tr>
                    <td style="width:5%"></td>
                    <td style="width:25%"><a href="#exp"><font color="black"><u>Exploring alternative futures</u></font></a></td>
                    <td style="width:25%"><a href="#pro"><font color="black"><u>Cyber Risk Profile</u></font></a></td>
                    <td style="width:25%"><a href="#glo"><font color="grey"><u>Global Perspective</u></font></a></td>
                    <td style="width:20%"><a href="#imp"><font color="black"><u>See the implications</u></font></a></td>
                </tr>
            </table>

        </div>

        </div>
       </div>
   </section>

        <section class="implication">
     <div class="container">
       <div class="row">
          <div class="col-lg-12">
         <div class="implication_heading">
         <h1><a name="imp"></a>See the implications</h1>
             <p>Dive deeper into alternative cyber futures by selecting a variable from the dropdown menu and then choosing any number of countries, regions, and scenarios in the two text boxes.</p>
         </div>
         </div>

           <div class="col-lg-12">
          <div class="col-lg-5">
        <div class="imple_field">
        <div class="input-group">
            <asp:DropDownList ID="cboVariables2" runat="server" AutoPostBack="false" onchange="" class="chosen-select" ></asp:DropDownList>
         </div>
       </div>
         </div>

               <div id="CondDiv"></div>

        <div class="col-lg-2">
            <asp:Button ID="cmdRefresh" runat="server" Text="Refresh" style="margin-top: 5px; float: right; "/>
        </div>
        <div id="CondDiv2"></div>

        <div class="col-lg-5">
            <div class="Regs">
<%--                 <script language ="javascript" type="text/javascript">
                     $('#lstRegionsMultiple').tokenize();
                     $('#lstSceMultiple').tokenize();
                     </script>--%>
                <%--<asp:listbox id="lstRegionsMultiple" SelectionMode="Multiple" runat="server" class="chosen-select" Style="width:470px"></asp:listbox>  --%>
                <div style="float: right; line-height: 35px; border-radius: 0;" id='lstRegionsMultiple'>
           </div>                      
            </div>
        </div>
     </div>
    <div id="CondDiv3"></div>
        <script type="text/javascript">
            //Adding extra break in case is iPad to make the chosen cboVariables2 work
            var isiPad = navigator.userAgent.match(/iPad/i) != null;
            //isiPad = true;
            if (isiPad == true) {
                var mydiv = document.getElementById("CondDiv");
                var mydiv2 = document.getElementById("CondDiv2");
                var mydiv3 = document.getElementById("CondDiv3");
                var brTag = document.createElement('br');
                mydiv.appendChild(brTag);
                brTag = document.createElement('br');
                mydiv2.appendChild(brTag);
                brTag = document.createElement('br');
                mydiv2.appendChild(brTag);
                brTag = document.createElement('br');
                mydiv3.appendChild(brTag);
            }
        </script>

    <div class="col-lg-12">
        <div class="col-lg-5">
        <p><font color="white">For a full variable description click here: <input id="cmdInfo2" type="image" style="height:20px;width:20px;" src="images/info.png" onclick="showVarDesc($('#cboVariables2').val()); return false;" /></font></p>
        </div>
        <div class="col-lg-7">
           <%-- <asp:listbox id="lstSceMultiple" SelectionMode="Multiple" runat="server" class="chosen-select" Style="width:550px"></asp:listbox>   --%>
            <div style="margin-right: -12px; margin-bottom: 15px; float: right; line-height: 35px; border-radius: 0;" id='lstSceMultiple'></div> 
        </div>
    </div>

    <br /><br />
    <br /><br />

     <div class="col-lg-12">
         <div class="ceo">
            <div id="containerline"></div>
             <div class="input-group imple_field"></div>
         </div>
     </div>


            <table style="width:100%">
                <tr>
                    <td style="width:5%"></td>
                    <td style="width:25%"><a href="#exp"><font color="white"><u>Exploring alternative futures</u></font></a></td>
                    <td style="width:25%"><a href="#pro"><font color="white"><u>Cyber Risk Profile</u></font></a></td>
                    <td style="width:25%"><a href="#glo"><font color="white"><u>Global Perspective</u></font></a></td>
                    <td style="width:20%"><a href="#imp"><font color="yellow"><u>See the implications</u></font></a></td>
                </tr>
            </table>

       </div>
     </div>       
   </section>

        <asp:Button ID="cmdGeneratejs" AccessKey="j" runat="server" Text="Generate Javascript" ToolTip="Generate Javascript (Alt+J)"></asp:Button>


        <%-- JAM 2015-05-17 Adding slider functionality--%>
        <br />
    </form>

    <footer>
      <div class="col-lg-12">
      <p>The Cyber Risk Dashboard is part of the Future of Cyber Risk Project, a collaboration between the Frederick S. Pardee Center for International Futures, the Atlantic Council, and Zurich Insurance. This dashboard represents only a small subset of the results that are covered in the Future of Cyber Risk report and a fraction of the capabilities available in the International Futures (IFs) system. If you would like to know more, we invite you to first read the report and then create your own alternative cyber futures with IFs—both are free to download or use online at <a href="https://korbel.du.edu/pardee/access-ifs">www.pardee.du.edu</a>.</p>
     </div>

    <div class="container">
       <div class="row">
        <div class="col-lg-6">
         <img src="images/logo_1.png" alt="logo" >
        </div>
 
     <div class="col-lg-6">
     <div class="download">
         <h1>Use the International Futures model online for free at:</h1>
         <a href="http://www.ifs.du.edu/ifs/frm_MainMenu.aspx">www.ifs.du edu</a>
     </div>    
     </div>

       </div>
       </div>

            </footer>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>--%>
    <script src="Scripts/chosen/chosen.jquery.js" type="text/javascript"></script>
    <script src="Scripts/chosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var isiPad = navigator.userAgent.match(/iPad/i) != null;
        //isiPad = true;
        if (isiPad == false) {

            var config = {
                '.chosen-select': {}
            }
            for (var selector in config) {
                $(selector).chosen(config[selector]);
            }

            // Chosen touch support.
            if ($('.chosen-container').length > 0) {
                $('.chosen-container').on('touchstart', function (e) {
                    e.stopPropagation(); e.preventDefault();
                    // Trigger the mousedown event.
                    $(this).trigger('mousedown');
                });
            }

            document.getElementById("futhd2").style.height = myHeight - 535;
        }
        else {
            //this is not working, can't reduce anymore
            document.getElementById("futhd2").style.height = myHeight - 1100;
        }

        //Height of Scenario Description box
        document.getElementById("bd").style.height = myHeight - 210;
        document.getElementById("bd").style.overflow = 'auto';
        document.getElementById("futhd2").style.overflow = 'auto';

        </script>

</body>
</html>
