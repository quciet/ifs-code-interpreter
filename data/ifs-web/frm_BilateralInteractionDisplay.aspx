<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_BilateralInteractionDisplay" Codebehind="frm_BilateralInteractionDisplay.aspx.vb" %>

<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Bilateral Interaction</title>
    <script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
    </script>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
    <%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
    <%-- <meta http-equiv="Page-Enter" content="Alpha(opacity=100)">--%>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.2)">
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.2)">

    <script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
    <%-- freezing grid col and rows headers  >--%>
   <!--jam 2021/11/27-->
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>--%>


    <!--jam 2022-06-13-->
     <script type="text/javascript" src="Scripts/bilateral/jquery.min.js"></script>
     <script type="text/javascript" src="Scripts/bilateral/jquery-ui.min.js"></script>

    <%--<script src="Scripts/highcharts.js"></script>
    <script src="Scripts/exporting.js"></script>--%>


    <!--jam 2025/03/18 New highcharts version libraries-->
    <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>

    <!--jam 2025/03/18 New highcharts version libraries-->
    <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>


    <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
    <link href="Scripts/GridviewScroll.css" rel="stylesheet" />

      

    <%--VarSel================--%>
    <%-- <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
       

         <!--JAM 2014-04-04-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSel_Vars.js"></script>

        <!--JAM 2013-04-04-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSelFx.js"></script>
        
          <!--JAM 2014-04-15-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSel_ForeVars.js"></script>

        <!--JAM 2013-04-15-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSelForeFx.js"></script>  --%>


    <!---------------------JAM 2012-04-05------------------------>
    <%-- <style type="text/css">
        div#grdBilateral {
            width: 900px;
            height: 550px;
            overflow: scroll;
            position: relative;
        }

            div#grdBilateral th {
                top: expression(document.getElementById("AdjResultsDiv").scrollTop-2);
                left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
                position: relative;
                z-index: 20;
            }

        div#AdjResultsDiv {
            width: 90%; /*900px*/
            height: 85%; /*500px*/
            /*zz9-5:comm*/
            /*overflow: scroll;*/
            position: relative;
            display: none;
        }

            div#AdjResultsDiv th {
                top: expression(document.getElementById("AdjResultsDiv").scrollTop-2);
                left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
                position: relative;
                z-index: 20;
            }

        .GridViewHeaderStyle {
            /* font-family:Verdana;*/
            /* font-size:10px;*/
            /*color:White;*/
            color: Black;
            background: lightgray;
            /*background: gray url(Images/grid/header1.png) repeat-x;
	        line-height: 90px; */
        }
    </style>--%>

    <!----------------------------------------------------------->
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" onload="javascript:pageResize();gridviewScroll0();">
  <script type="text/javascript">        
      // Defining event listener function
      function gridviewScroll0() {
         var grid = this.document.getElementById("grdBilateral");
          if (!grid) {
            return;
          }
          var tw2 = Math.min(document.documentElement.clientWidth - 30, grid.offsetWidth + 30);
          gridView1 = $('#grdBilateral').gridviewScroll({
              width: tw2,
              height: 560,
              railcolor: "#F0F0F0",
              barcolor: "#CDCDCD",
              barhovercolor: "#606060",
              bgcolor: "#F0F0F0",
              freezesize: 1,
              arrowsize: 30,
              varrowtopimg: "Images/arrowvt.png",
              varrowbottomimg: "Images/arrowvb.png",
              harrowleftimg: "Images/arrowhl.png",
              harrowrightimg: "Images/arrowhr.png",
              //headerrowcount: 2,
              railsize: 16,
              barsize: 8
          });
      }
      // Attaching the event listener function to window's resize event
      window.addEventListener("resize", gridviewScroll0);
      // Calling the function for the first time
      gridviewScroll0();
    </script>
    <form id="Form1" method="post" runat="server">
        <asp:ScriptManager EnablePageMethods="true" EnablePartialRendering="true" ID="MainSM" runat="server" ScriptMode="Release" LoadScriptsBeforeUI="true"></asp:ScriptManager>
            <%-- asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="True" UpdateMode="Conditional" --%>
            <%-- ContentTemplate --%>
                <input id="txtFiles" name="txtFiles" type="hidden" runat="server" />
                <input id="txtTableCols" name="txtTableCols" type="hidden" value="2" runat="server" />
                <input id="txtClickedOption" name="txtClickedOption" type="hidden" value="N" runat="server" />
                <input id="txtCalledWhenLoaded" name="txtCalledWhenLoaded" type="hidden" value="N" runat="server" />
                <asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
                    <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
                        Font-Bold="True">
                        <asp:TableCell ID="LogoCell" Width="40px">
                            <!--jam 2022/04/15-->
                            <a href="https://korbel.du.edu/pardee">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>
                        </asp:TableCell>
                        <asp:TableCell Text="Bilateral Interaction" ID="TitleCell"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffff" border="0">
                    <tr>
                        <td>
                            <cswm:WebMenu ID="cswmWebMenu" style="z-index: 999; left: 0px; position: absolute; top: 16px"
                                runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:WebMenu>
                        </td>
                    </tr>
                </table>

                <!-------------------JAM 2012-04-04------------------------->
                <%--Wrapper Div which will scroll the GridView--%>
                <div id="AdjResultsDiv">

                    <asp:GridView ID="grdBilateral" runat="server" Visible="false" ShowHeader="false"
                        GridLines="Both" Width="90%" CellPadding="6">
                        <RowStyle  CssClass="GridviewScrollItem" HorizontalAlign="Right" VerticalAlign="Middle"/>
                        <%--  <HeaderStyle CssClass="GVFixedHeader" BackColor="#EBF3FF" Font-Size="13px" Font-Names="Times New Roman, Times, serif"
                BorderColor="Black" BorderStyle="Ridge" BorderWidth="2px" />--%>
                    </asp:GridView>

                </div>
                <!-------------------------------------------------------------->

                <table>
                    <asp:Panel ID="pnlMain" runat="server">
                        <tr valign="top">
                            <td width="55%">
                                <asp:Label ID="lblFiles" Visible="False" runat="server"
                                    Width="90%" Font-Bold="True" Font-Names="Arial">Scenarios: (multi-select allowed)</asp:Label>
                                <%--<br />--%>
                                <asp:UpdatePanel ID="UpdatelstFiles" Visible="False" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstFiles" Visible="False" runat="server"
                                            AutoPostBack="False" SelectionMode="Multiple" Height="70px" Width="90%"></asp:ListBox>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <%--<br />--%>

                                <asp:Label ID="lblHistSeries" runat="server"
                                    Font-Bold="True" Font-Names="Arial">Historical Series:</asp:Label>
                                <%--<asp:Button ID="btnConfirm" runat="server" OnClick="OnConfirm" Text="Raise Confirm" OnClientClick="Confirm()"/>--%>
                                <%--<asp:Button ID="btnConfirm" runat="server" OnClick="OnConfirm" Text="Raise Confirm" OnClientClick="return confirm('add record?');"/>--%>
                                <asp:Button ID="cmdDeleteGravity" runat="server"
                                    OnClientClick="Confirm()"
                                    OnClick="OnConfirm" Visible="False"
                                    Text="Delete Formula" />
                                &nbsp;  &nbsp;
                                 <asp:Button ID="cmdSelectCurrent" runat="server"
                                     Text="Select Current" />

                                <br />
                                <asp:UpdatePanel ID="updlstSeries" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstSeries"
                                            runat="server" Height="390px" Visible="True" Width="90%" AutoPostBack="True" SelectionMode="Single"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <asp:ListBox ID="lstVarNameSavedFormulas"
                                    runat="server" Height="390px" Visible="False" Width="90%" AutoPostBack="True" SelectionMode="Single"></asp:ListBox>

                                <asp:ListBox ID="lstVarNameMonadyc"
                                    runat="server" Height="390px" Visible="False" Width="90%" AutoPostBack="False" SelectionMode="Single"></asp:ListBox>

                                <asp:ListBox ID="lstVarNameForecast"
                                    runat="server" Height="300px" Visible="False" Width="90%" AutoPostBack="True" SelectionMode="Single"></asp:ListBox>
                                <asp:ListBox ID="lstVarNameForecastWHist"
                                    runat="server" Height="300px" Visible="False" Width="90%" AutoPostBack="True" SelectionMode="Single"></asp:ListBox>
                            </td>
                            <td width="15%">

                                <asp:Label ID="lblReg" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Actor: &nbsp;</asp:Label>
                                <asp:Button ID="cmdCurrentPartner"
                                    runat="server" Width="90px" ToolTip="Add Dyad for current Actor and Partner" Text="Add Dyad"></asp:Button>
                                <br />
                                <asp:ListBox ID="lstDim1" runat="server" AutoPostBack="True"
                                    Height="390px" Width="156" SelectionMode="Single"></asp:ListBox>
                            </td>
                            <td width="15%">

                                <asp:Label ID="Label1" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Partner:</asp:Label>
                                <br />
                                <asp:ListBox ID="lstDim1p" runat="server" AutoPostBack="True"
                                    Height="390px" Width="156" SelectionMode="Single"></asp:ListBox>
                            </td>


                            <td width="15%">
                                <asp:Panel ID="pnlGOpts" runat="server">

                                    <asp:Label ID="lblGOpts" runat="server"
                                        Font-Bold="True" Font-Names="Arial">Geography Options:</asp:Label>
                                    <br />
                                    <br />
                                    <asp:RadioButton ID="optReg" runat="server" AutoPostBack="True" AccessKey="r" Checked="True" GroupName="GroupingOptions"
                                        Font-Names="Arial" Text="Country"
                                        ToolTip="Loads single Country (Alt+R)" />
                                    <br />
                                    <br />
                                    <asp:RadioButton ID="optGroup" runat="server" AutoPostBack="True" AccessKey="g" GroupName="GroupingOptions"
                                        Font-Names="Arial" Text="Groups"
                                        ToolTip="Loads single Group (Alt+G)" />
                                    <br />
                                    <br />
                                    <asp:RadioButton ID="optGroupDec" runat="server" AutoPostBack="True" AccessKey="e" GroupName="GroupingOptions"
                                        Font-Names="Arial" Text="Decomposed Groups"
                                        ToolTip="Decomposes a Group into its Member Countries (Alt+E)" />
                                    <br />
                                    <br />
                                    <asp:RadioButton ID="optGList" runat="server" AutoPostBack="True" AccessKey="l" GroupName="GroupingOptions"
                                        Font-Names="Arial" Text="Geography-Lists"
                                        ToolTip="Loads a predefined list of Countries and/or Groups (Alt+L)" />
                                </asp:Panel>
                                <br />
                                <br />
                                <asp:Panel ID="Panel2" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlSelecteds" runat="server">
                        <tr valign="top">
                            <td width="55%">
                                <asp:Label ID="Label3" runat="server"
                                    Font-Bold="True" Font-Names="Arial">Selected Series: (multi-select allowed) &nbsp; </asp:Label>

                                <asp:Button ID="cmdRemove"
                                    runat="server" Width="150px" Text="Remove Selected(s)"></asp:Button>
                                &nbsp;  &nbsp;
                                 <asp:Button ID="cmdEditGravity"
                                     runat="server" Width="150px" Text="Edit Gravity"></asp:Button>

                                <asp:Panel ID="pnlNewFormula" runat="server" Visible="false">

                                    <asp:Label ID="lblNewFormula" runat="server"> &nbsp; New Formula:
                                    </asp:Label>
                                    <asp:TextBox ID="txtNewFormula" runat="server"
                                        Width="480"></asp:TextBox>
                                    <asp:Button ID="cmdRenameFormula" AutoPostBack="True" Text="OK"
                                        runat="server"></asp:Button>
                                    <asp:Button ID="cmdCloseRenameFormula" AutoPostBack="True" Text="X"
                                        runat="server"></asp:Button>
                                </asp:Panel>

                                <asp:UpdatePanel ID="UpdatelstSelSeries" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:ListBox ID="lstSelSeries"
                                            AutoPostBack="False" runat="server" Height="140px" Width="90%" SelectionMode="Multiple"></asp:ListBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <br />
                                <br />

                            </td>
                            <td colspan="2" width="30%">

                                <asp:Label ID="Label4" runat="server"
                                    Font-Bold="True" Width="148" Font-Names="Arial">Selected Dyads: (click to remove)</asp:Label>
                                <asp:Button ID="cmdRemoveAllDyads"
                                    runat="server" Width="100px" Text="Remove All"></asp:Button>
                                <br />
                                <asp:ListBox ID="lstSelDyads" runat="server"
                                    AutoPostBack="True" Height="140px" Width="95%" SelectionMode="Single"></asp:ListBox>
                            </td>


                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlChartTable" runat="server" Visible="false">
                        <tr>
                            <td colspan="3" align="center" style="width: 100%">

                                <%--jam 2025/03/18 New highcharts version libraries container width adjusted from 90% to 97%--%>
                                <%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>
                                <div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>
                                <br />
                                <div>
                                    <asp:Label ID="lblJS" runat="server" Visible="false"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="width: 100%">
                                <asp:Table ID="grdHL" runat="server" BorderStyle="Outset"
                                    BorderColor="DarkGray" Visible="false">
                                </asp:Table>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td align="center" style="width: 100%">
                            <asp:Panel ID="Panel1" runat="server" Width="1080px">
                                <asp:Button ID="cmdSearch" Style="z-index: 104; left: 32px" TabIndex="2"
                                    runat="server" Width="88px" Text="Search"></asp:Button>
                                <asp:Button ID="cmdClear" Style="z-index: 107; left: 136px" runat="server"
                                    Width="88px" Text="Clear"></asp:Button>
                                <asp:Button ID="cmdComputation" Style="z-index: 102; left: 240px"
                                    TabIndex="3" runat="server" Width="88px" Text="Computation"></asp:Button>
                                <asp:Button ID="cmdGravity" Style="z-index: 104; left: 344px" TabIndex="2"
                                    runat="server" Width="88px" Text="Gravity"></asp:Button>
                                <asp:Button ID="cmdTable" Style="z-index: 107; left: 408px" runat="server"
                                    Width="88px" Text="Table"></asp:Button>
                                <asp:Button ID="cmdDraw" Style="z-index: 102; left: 512px"
                                    TabIndex="3" runat="server" Width="88px" Text="Line"></asp:Button>
                                <asp:Button ID="cmdExport" Style="z-index: 102; left: 512px"
                                    TabIndex="3" runat="server" Width="88px" Text="Export" Visible="False"></asp:Button>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <script language="javascript">
                    var myWidth = 0, myHeight = 0;

                    function pageResize() {
                        //zz98
                        //jrs 2024/04/26 console.log("zzResize()...");
                        if (typeof (window.innerWidth) == 'number') {
                            /*Non-IE*/
                            myWidth = window.innerWidth;
                            myHeight = window.innerHeight;
                        } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                            /*//IE 6+ in 'standards compliant mode*/
                            myWidth = document.documentElement.clientWidth;
                            myHeight = document.documentElement.clientHeight;
                        } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                            /*//IE 4 compatible*/
                            myWidth = document.body.clientWidth;
                            myHeight = document.body.clientHeight;
                        }
                        if (myWidth == 0) {
                            myWidth = screen.width;
                            myHeight = screen.height;
                        }

                        //jrs 2012/08/03 only if listboxs are visible
                        if (document.forms[0].lstDim1 != null) {

                            //var doc = document.forms[0];
                            //var f2 = 0.95;
                            //var f = 1.025;
                            //var h = myHeight * f2 - (20 + 64) - (24 + 20) * f;
                            //if (h < 106 + 20) {
                            //    h = 106 + 20;
                            //}
                            //doc.lstDim1.style.height = h + 'px';
                            //--------------------------------------------------------
                            //var h2 = h * 0.7;
                            //doc.lstDim2.style.height = h2 + 'px';
                            //var T = (92 + h2) * f;
                            //document.getElementById('lblVars').style.top = T + 'px';
                            //var tt = T + 20;
                            //doc.lstVars.style.top = tt + 'px';

                            //var t2 = (tt + 106 + 20) * f;
                            //doc.cmdDraw.style.top = t2 + 'px';
                            //doc.cmdTable.style.top = t2 + 'px';
                            //doc.cmdClear.style.top = t2 + 'px';

                            //t2 = (92 + 106) * f;
                            //document.getElementById('lblFiles').style.top = t2 + 'px';
                            //t2 = t2 + 20;
                            //doc.lstFiles.style.top = t2 + 'px';
                            //h2 = (h - 106 - 20 - 20);
                            //doc.lstFiles.style.height = h2 + 'px';

                            this.document.getElementById("AdjResultsDiv").style.display = "none";
                        }
                        else if (document.getElementById("lblJS") != null) {
                            this.document.getElementById("AdjResultsDiv").style.display = "none";
                        }
                        else if (this.document.getElementById("AdjResultsDiv") != null) {
                            var w = myWidth * 0.9;
                            var h = myHeight * 0.9;
                            this.document.getElementById("AdjResultsDiv").style.display = "block";
                            this.document.getElementById("AdjResultsDiv").setAttribute("width", w.toFixed(0) + 'px');
                            this.document.getElementById("AdjResultsDiv").setAttribute("height", h.toFixed(0) + 'px');
                                            }
                    }

                    if ((document.forms[0].txtAWBeta != null) && (document.forms[0].txtAWBeta.disabled == false)) {
                        document.forms[0].txtAWBeta.focus();
                    }
                    if ((document.forms[0].txtDiscRt != null) && (document.forms[0].txtDiscRt.disabled == false)) {
                        document.forms[0].txtDiscRt.focus();
                    }
                    if ((document.forms[0].txtEditLbl != null) && (document.forms[0].txtEditLbl.disabled == false)) {
                        document.forms[0].txtEditLbl.focus();
                    }

                   

                    //jrs 2012/01/16
                    function clickButton(e, buttonid) {
                        var evt = e ? e : window.event;
                        var bt = document.getElementById(buttonid);
                        if (bt) {
                            if (evt.keyCode == 13) {
                                bt.click();
                                return false;
                            }
                        }
                    }
                    function gcopy(text) {
                        navigator.clipboard.writeText(text)
                            .then(() => {
                                alert('Table has been copied to clipboard.');
                            })
                            .catch(err => {
                                // This can happen if the user denies clipboard permissions:
                                alert('Could not copy : ' + err);
                            });
                    }
                    function replaceAll(string, search, replace) {
                        return string.split(search).join(replace);
                    }
                    function tablecopy() {
                        var oTable = document.getElementById('grdBilateral');
                        //gets rows of table
                        var rowLength = oTable.rows.length;
                        var tx = '';
                        //loops through rows    
                        for (i = 0; i < rowLength; i++) {
                            if (i === 0) {
                                //Header Row------------------------------------
                                var oCells = oTable.rows.item(i).cells;
                                //gets amount of cells of current row
                                var cellLength = oCells.length;
                                var rt = '';
                                //loops through each cell in current row
                                var maxDetailRows = 0;
                                for (var j = 0; j < cellLength; j++) {
                                    var cellVal = oCells.item(j).innerHTML;
                                    cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                    cellVal = replaceAll(cellVal, '</div>', '');
                                    cellVal = replaceAll(cellVal, '&nbsp;', '');
                                    cellVal = replaceAll(cellVal, '<br>', '|');
                                    maxDetailRows = Math.max(maxDetailRows, cellVal.split("|").length);
                                }
                                //ExportsGoods	  ExportsGoods
                                //Australia	      Australia 
                                //Argentina	      Brazil

                                for (var k = 0; k < maxDetailRows; k++) {
                                    var rowct = '';
                                    for (var j = 0; j < cellLength; j++) {
                                        var cellVal = oCells.item(j).innerHTML;
                                        cellVal = replaceAll(cellVal, '<div class="GridCellDiv">', '');
                                        cellVal = replaceAll(cellVal, '</div>', '');
                                        cellVal = replaceAll(cellVal, '&nbsp;', '');
                                        cellVal = replaceAll(cellVal, '<br>', '|');
                                        if (cellVal.split("|").length >= k + 1) {
                                            rowct = rowct + cellVal.split("|")[k] + (j == cellLength - 1 ? '' : '\t');
                                        }
                                        else {
                                            rowct = rowct + (j == cellLength - 1 ? '' : '\t');
                                        }
                                    }
                                    tx = tx + rowct + '\r\n';
                                }

                            }
                            else {
                                //data rows----------------------------------------------------
                                //gets cells of current row  
                                var oCells = oTable.rows.item(i).cells;

                                //gets amount of cells of current row
                                var cellLength = oCells.length;
                                var rt = '';
                                //loops through each cell in current row
                                for (var j = 0; j < cellLength; j++) {

                                    // get your cell info here

                                    var cellVal = oCells.item(j).textContent;
                                    var et = j === cellLength - 1 ? '' : '\t';
                                    rt = rt + cellVal + et;
                                }
                                var er = i === rowLength - 1 ? '' : '\r\n';
                                tx = tx + rt + er;
                            }

                        }

                        gcopy(tx);
                    }
                    function Confirm() {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        if (confirm("Do you want to delete the selected formula ?")) {
                            confirm_value.value = "Yes";
                        } else {
                            confirm_value.value = "No";
                        }
                        document.forms[0].appendChild(confirm_value);
                    }


                </script>
                
            <%--/ContentTemplate>
            <Triggers>

                <asp:AsyncPostBackTrigger ControlID="lstSeries" />
                <asp:AsyncPostBackTrigger ControlID="lstSelSeries" />
            </Triggers --%>
        </%-->
    </form>

</body>
</html>

<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-3138457-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();


</script>

