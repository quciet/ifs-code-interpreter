<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_LorenzGraph" Codebehind="frm_LorenzGraph.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Lorenz Curve</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
        <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <!--jam 2021/11/27-->
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
       <%-- <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>--%>


		 <!--jam 2025/03/11 New highcharts version libraries-->
		 <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
		 <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>

		   <!--jam 2025/03/11 New highcharts version libraries-->
		  <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>



	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Lorenz Graph" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 130; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>
			<table width="100%">
				<tr>
					<TD width="100%">
						<asp:Label id="lblYear" runat="server">Year: </asp:Label>
						<asp:textbox width="48px" MaxLength="4" id="txtYear" tabIndex="1" runat="server">2000
			</asp:textbox>
						<asp:Button id="cmdRefresh" runat="server" Width="72px" Visible="true" 
                            Text="(R)efresh" AccessKey="R"></asp:Button>
                        <asp:label id="lblAddYear" runat="server" Visible="False">Add Year: </asp:label>
						<asp:textbox id="txtAddYear" tabIndex="1" runat="server" Width="48px" MaxLength="4" Visible="False"
							>2000</asp:textbox>
						<asp:Button id="cmdAdd" runat="server" Width="72px" Visible="False" Text="Ok"></asp:Button>
						<asp:Label id="lblMsg" runat="server" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label></TD>
				</tr>
			</table>
			<aiswc:focuscontrol id="WinFormsToWeb_FocusControl" style="Z-INDEX: 107; LEFT: -32000px; POSITION: absolute; TOP: -32000px"
				runat="server"></aiswc:focuscontrol>
			<script language="javascript">
{
	if ((document.forms[0].txtYear != null)&& (document.forms[0].txtYear.disabled == false ) ) 
          {
               document.forms[0].txtYear.focus();
          }
    else
    {
		if ((document.forms[0].txtAddYear != null)&& (document.forms[0].txtAddYear.disabled == false ) ) 
          {
               document.forms[0].txtAddYear.focus();
          }
    }

        //jrs 2015/09/21
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

}
			</script>
			
			 <%--jam 2025/03/11 New highcharts version libraries container width adjusted from 90% to 97%--%>
            <%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>

			<div id="container" style="height: 90%; width: 97%; top: 10px; left: 10px; position: relative;"></div>

		    <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>

		</form>
	</body>
</HTML>

<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-3138457-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>