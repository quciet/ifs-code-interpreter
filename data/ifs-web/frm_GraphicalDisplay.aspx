<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_GraphicalDisplay" Codebehind="frm_GraphicalDisplay.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Graphical Display</title>
    <!-- jam 2021/11/27-->
   <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>

    <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
    <!--<script src="Scripts/highcharts.js"></script>-->
    <%--<script src="Scripts/exporting.js"></script>--%>

	 <!--jam 2025/02/15 New highcharts version libraries-->
	<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
	<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
   
	<!--jam 2025/02/15 New highcharts version libraries-->
	<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>




    <!--JAM 2020/12/20-->
    <!--jam 2025/02/15 New latest libraries-->
    <%--<script src="<%= Page.ResolveUrl("~/Scripts/highcharts.js")%>"></script>--%>
    <!--jrs 2024/10/30 this stoped working locally it requires now access to their server <script src="<%= Page.ResolveUrl("~/Scripts/exporting.js")%>"></script>-->
    <!--jam 2023/11/01-->
	<%--<link rel="icon" href="http://localhost/ifs/ifsweb/images/favicon.ico">--%>

    <link rel="icon" href="<%= Page.ResolveUrl("~/images/favicon.ico")%>">

	


</head>
<body leftMargin="0" topMargin="0" rightMargin="0">

<form id="frm_GraphicalDisplay" runat="server">
             <!--JAM 2013-04-09-->
            <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px; visibility:hidden;">
                <!--jam 2023/11/01-->
                <%--<img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...--%>

                <img id="loadimg" src="<%= Page.ResolveUrl("~/images/squares-circle.gif")%>"  height="93" width="93"><br>Loading resources...

            </div>

			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                
                        <!--JAM 2022/03/17-->

					<%--<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>--%>

                        <a href="https://korbel.du.edu/pardee"><img src="<%= Page.ResolveUrl("~/Images/pcl.png")%>" alt="IFs Home" style="width:30px;"></a>

                  
					</asp:TableCell>
					<asp:TableCell Text="Graphical Display" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 130; LEFT: 0px; POSITION: absolute; TOP: 16px"
							runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>

    <div id="divPanel">
			<asp:Panel runat="server" Width="300" Height="40" 
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_Horizon" BackColor="White">
				<asp:TextBox ID="txtHorizon" runat="server"></asp:TextBox><asp:Button ID="cmdHorizon"
                    runat="server" Text="Submit" /><br />
				<asp:DropDownList id="cmb_Horizon" tabIndex="8" runat="server" Height="20" Width="153" BackColor="Window"
					Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					Font-Size="8.0" Font-Name="Arial" Visible="True" AutoPostBack="True" ForeColor="WindowText"></asp:DropDownList>
			</asp:Panel>
			<asp:Panel runat="server" Width="300" Height="40" 
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_GoToYear" BackColor="White">
                <asp:TextBox ID="txtGoToYear" runat="server"></asp:TextBox><asp:Button ID="cmdGoToYear"
                    runat="server" Text="Submit" /></asp:Panel>
			<asp:Panel runat="server" Width="300" Height="40" 
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_VLine" BackColor="White">
                <asp:TextBox ID="txtVLine" runat="server"></asp:TextBox><asp:Button ID="cmdVLine"
                    runat="server" Text="Submit" /></asp:Panel>
    </div>

	<%--jam 2025/02/15 New highcharts version libraries container width adjusted from 90% to 100%--%>
   <%-- <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>
	<div id="container" style="height: 90%; width: 95%; top: 10px; left: 10px; position: relative;"></div>

    <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>

			<script language="javascript">
			{
				if ((document.forms[0].txtGoToYear != null) && (document.forms[0].txtGoToYear.disabled == false))
				{
					document.forms[0].txtGoToYear.focus();
				}
				if ((document.forms[0].txtHorizon != null) && (document.forms[0].txtHorizon.disabled == false))
				{
					document.forms[0].txtHorizon.focus();
				}
				if ((document.forms[0].txtVLine != null) && (document.forms[0].txtVLine.disabled == false))
				{
					document.forms[0].txtVLine.focus();
				}
			    //jrs 2012/01/16
                function clickButton(e, buttonid){
                      var evt = e ? e : window.event;
                      var bt = document.getElementById(buttonid);
                      if (bt){
                          if (evt.keyCode == 13){
                                bt.click();
                                return false;
                          }
                      }
                }				
			}
			</script>
			
    </form>
</body>
</html>
<script type="text/javascript">

<!--JAM 2013-04-09-->
function showhg() 
		    {
		        var doc = document.forms[0];
                //JAM 2013-04-09
		        document.getElementById('container').style.visibility = "hidden";

		        var imgurl = document.getElementById('loadimg').src;
		        setTimeout(function () {
		            document.getElementById('loadimg').src = imgurl;
		        }, 1);
		       document.getElementById('firstloading').style.visibility = "visible";
		    
			document.body.style.cursor = "wait";
			//doc.cmd_Exit.value = "Please Wait....";
			//doc.cmd_Exit.style.cursor = "wait";
			
			}
</script>
<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>
