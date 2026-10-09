<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_GrowthAccounting" Codebehind="frm_GrowthAccounting.aspx.vb" %>

<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Growth Accounting</title>
    <script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
    </script>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
    <%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
    <script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
      <!--jam 2021/11/27-->
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
     <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
   <%-- <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/exporting.js"></script>--%>


    <!--jam 2025/03/11 New highcharts version libraries-->
    <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>

      <!--jam 2025/03/11 New highcharts version libraries-->
     <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>


    <!---------------------JAM 2012-04-05------------------------>
    <style type="text/css">
        div#grdGrowth {
            width: 900px;
            height: 550px;
            overflow: scroll;
            position: relative;
        }

            div#grdGrowth th {
                top: expression(document.getElementById("AdjResultsDiv").scrollTop-2);
                left: expression(parentNode.parentNode.parentNode.parentNode.scrollLeft);
                position: relative;
                z-index: 20;
            }

        div#AdjResultsDiv {
            width: 90%; /*900px*/
            height: 85%; /*500px*/
            overflow: scroll;
            position: relative;
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
    </style>
    <!----------------------------------------------------------->
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" onload="javascript:pageResize();">
    <form id="Form1" method="post" runat="server">
        <input id="txtFiles" name="txtFiles" type="hidden" runat="server" />
        <asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
            <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
                Font-Bold="True">
                <asp:TableCell ID="LogoCell" Width="40px">
                    <!--jam 2022/04/15-->
                    <a href="https://korbel.du.edu/pardee">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>
                </asp:TableCell>
                <asp:TableCell Text="Growth Accounting" ID="TitleCell"></asp:TableCell>
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

            <asp:GridView ID="grdGrowth" runat="server" Visible="false"
                GridLines="Both" Width="100%" CellPadding="6">
                <HeaderStyle CssClass="GridViewHeaderStyle" />
            </asp:GridView>

        </div>
        <!-------------------------------------------------------------->

        <table>
            <asp:Panel ID="pnlMort" runat="server">
                <tr valign="top">
                    <td width="40%">
                        <asp:Panel ID="frm_Horizon"
                            runat="server" Font-Bold="False" Width="456" Height="50" Visible="False" ForeColor="WindowText" Font-Size="8.0" Enabled="True"
                            Font-Strikeout="False" Font-Italic="False" Font-Name="Arial" Font-Underline="False" BackColor="Window">
                            <%--Change Horizon Year:<br>--%>

                            <div>
                                <asp:Label runat="server" Style="float: left;" Text="Change Horizon Year:"></asp:Label>
                                <asp:Button ID="cmdCloseHorizon" Style="float: right;"
                                    runat="server" Width="40px" Text="X"></asp:Button>
                            </div>
                            <asp:DropDownList ID="cmb_Horizon" runat="server"
                                Height="30" Width="456" Font-Size="8.0" ForeColor="WindowText" Visible="False"
                                Font-Bold="False" BackColor="Window" Font-Underline="False" Font-Name="Arial"
                                Font-Italic="False" Font-Strikeout="False" Enabled="True" AutoPostBack="True">
                            </asp:DropDownList>

                        </asp:Panel>

                        <asp:Label ID="lblReg" runat="server"
                            Font-Bold="True" Width="448" Font-Names="Arial">Region:</asp:Label>
                        <br />
                        <asp:ListBox ID="lstDim1" runat="server"
                            Height="457px" Width="456" SelectionMode="Multiple"></asp:ListBox>
                    </td>

                    <td width="15%">
                        <asp:Label ID="Label2" runat="server"
                            Font-Bold="True" Font-Names="Arial">Economic Sector:</asp:Label>
                        <br />
                        <asp:ListBox ID="lstDim2"
                            runat="server" Height="318" Width="168" SelectionMode="Multiple"></asp:ListBox>
                        <br />
                        <br />
                        <asp:Label ID="lblVars" runat="server"
                            Font-Bold="True" Font-Names="Arial">Variables:</asp:Label>
                        <br />
                        <asp:ListBox ID="lstVars"
                            runat="server" Height="106" Width="168" SelectionMode="Multiple"></asp:ListBox>
                    </td>

                    <td width="45%">

                        <asp:Label ID="lblFiles" runat="server"
                            Width="448" Font-Bold="True" Font-Names="Arial">Scenarios:</asp:Label>
                        <br />
                        <asp:ListBox ID="lstFiles" runat="server"
                            SelectionMode="Multiple" Height="318" Width="456"></asp:ListBox>
                        <br />



                    </td>
                </tr>
            </asp:Panel>

            <tr>
                <td colspan="3" align="center" style="width: 100%">

                    <%--jam 2025/03/11 New highcharts version libraries container width adjusted from 90% to 100%--%>
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
            <tr>
                <td colspan="3" align="center" style="width: 100%">
                    <asp:Panel ID="pnlButtons" runat="server" Width="1080px">
                        <asp:Button ID="cmdDraw" Style="z-index: 104; left: 32px" TabIndex="2"
                            runat="server" Width="88px" Text="Graph"></asp:Button>
                        <asp:Button ID="cmdTable" Style="z-index: 107; left: 136px" runat="server"
                            Width="88px" Text="Table"></asp:Button>
                        <asp:Button ID="cmdClear" Style="z-index: 102; left: 240px"
                            TabIndex="3" runat="server" Width="88px" Text="Clear"></asp:Button>
                      
                    </asp:Panel>
                </td>
            </tr>


        </table>



        <script language="javascript">
            var myWidth = 0, myHeight = 0;

            function pageResize() {
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
                    var doc = document.forms[0];
                    //pct of screen to be used
                    //myWidth = 0.9 * myWidth;
                    var f2 = 0.95;
                    var f = 1.025;
                    var h = myHeight * f2 - (20 + 64) - (24 + 20) * f;
                    if (h < 106 + 20) {
                        h = 106 + 20;
                    }
                    doc.lstDim1.style.height = h + 'px';

                    var h2 = h * 0.7;
                    doc.lstDim2.style.height = h2 + 'px';
                    var T = (92 + h2) * f;
                    document.getElementById('lblVars').style.top = T + 'px';
                    var tt = T + 20;
                    doc.lstVars.style.top = tt + 'px';

                    var t2 = (tt + 106 + 20) * f;
                    doc.cmdDraw.style.top = t2 + 'px';
                    doc.cmdTable.style.top = t2 + 'px';
                    doc.cmdClear.style.top = t2 + 'px';

                    t2 = (92 + 106) * f;
                    document.getElementById('lblFiles').style.top = t2 + 'px';
                    t2 = t2 + 20;
                    doc.lstFiles.style.top = t2 + 'px';
                    h2 = (h - 106 - 20 - 20);
                    doc.lstFiles.style.height = h2 + 'px';

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
                    .catch(() => {
                        alert('Failed to copy. You should enable the Experimental Web Platform Features flag in Chrome at chrome://flags/#enable-experimental-web-platform-features');
                    });
                 }

        </script>
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
