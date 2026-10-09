<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Scatter" Codebehind="Scatter.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>Scatter Graph</title>
        <SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
        <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
        <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
        <meta name="vs_defaultClientScript" content="JavaScript">
        <!--jam 2021/11/27-->
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        <%--<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
       
		<%-- <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>
		<script src="Scripts/highcharts-more.js"></script> <!--JAM 2018-04-29 Added for bubble chart-->--%>
		 <!--jam 2025/02/25 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts-more.js")%>"></script>


    </HEAD>
    <body leftMargin="0" rightMargin="0" topmargin="0">
              <form id="Form2" name="form1" method="post" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Scattergram of Raw Country Data" ID="TitleCell"></asp:TableCell>
					<asp:TableCell HorizontalAlign="Right" ID="CloseCell">
						<asp:Button runat="server" STYLE="Z-INDEX:106" ID="WinFormsToWeb_ButtonClose" Width="12px" Height="15px"
							Font-Size="XX-Small" Font-Names="Lucida Console" Font-Bold="True" Text="x"></asp:Button>
					</asp:TableCell>
				</asp:TableRow>
				 
			</asp:table>
 <cswm:WebMenu ID="cswmWebMenu" runat="server" MenuBar-AbsoluteDockEnabled="False"
                MenuBar-AbsoluteDragEnabled="False" style="z-index: 130; left: 0px; position: absolute;
                top: 16px"></cswm:WebMenu>
                <asp:button id="cmd_Plot" style="Z-INDEX: 102; LEFT: 310px; POSITION: absolute; TOP: 497px; TEXT-ALIGN: center"
				accessKey="E" tabIndex="6" runat="server" Height="33" Width="81" ForeColor="ControlText" Enabled="False" 
				CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" 
				Font-Size="8.0" Font-Name="Arial" visible ="false" Text="(E)xcel"></asp:button>
				<asp:button id="Cmd_Save" style="Z-INDEX: 103; LEFT: 206px; POSITION: absolute; TOP: 497px; TEXT-ALIGN: center"
				accessKey="S" tabIndex="5" runat="server" Height="33" Width="81" ForeColor="ControlText" Enabled="True" 
				CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" 
				Font-Size="8.0" Font-Name="Arial" visible ="false" Text="(S)ave"></asp:button>
				<asp:button id="cmd_Print" style="Z-INDEX: 104; LEFT: 94px; POSITION: absolute; TOP: 497px; TEXT-ALIGN: center"
				accessKey="P" tabIndex="4" runat="server" Height="33" Width="81" ForeColor="ControlText" Enabled="True" 
				CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" 
				Font-Size="8.0" Font-Name="Arial" visible ="false" Text="(P)rint"></asp:button>
                <asp:button id="cmd_Exit"  style="Z-INDEX: 101; LEFT: 461px; POSITION: absolute; TOP: 500px; TEXT-ALIGN: center"
				accessKey="C" tabIndex="7" runat="server" Height="33" Width="85" ForeColor="ControlText" Enabled="True" 
				CausesValidation="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" visible ="false" 
				Font-Bold="False"  Text="(C)ontinue"></asp:button>
                                			
				<asp:datagrid id="grd_Scatter" style="Z-INDEX: 105; LEFT: 368px; POSITION: absolute; TOP: 127px"
				tabIndex="3" runat="server" Height="33" Width="81" Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8" Font-Name="Microsoft Sans Serif" AllowPaging="True" Visible="False" PageSize="1" BorderStyle="Solid" HeaderStyle-ForeColor="ControlText" HeaderStyle-BackColor="Control" ItemStyle-ForeColor="WindowText" ItemStyle-BackColor="Window">
				<PagerStyle Mode="NumericPages"></PagerStyle>
				<SelectedItemStyle BackColor="Highlight"></SelectedItemStyle>
				<Columns>
					<asp:ButtonColumn Text="Select" ButtonType="LinkButton" CommandName="Select"></asp:ButtonColumn>
				</Columns>
			</asp:datagrid><asp:label id="cmd_FileAccess" style="Z-INDEX: 106; LEFT: 224px; POSITION: absolute; TOP: 87px"
				runat="server" BackColor="Red" Visible="False"></asp:label><asp:label id="OLE1" style="Z-INDEX: 107; LEFT: 56px; POSITION: absolute; TOP: 167px" tabIndex="2"
				runat="server" Height="33" Width="97" BackColor="Red" ForeColor="Black" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8.25" Font-Name="Microsoft Sans Serif" Text="OLE1" Visible="False" BorderStyle="Solid">OLE1</asp:label>
		
				<%--jam 2025/02/25 New highcharts version libraries container width adjusted from 90% to 100%--%>
                <%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>
				 <div id="container" style="height: 90%; width: 95%; top: 10px; left: 10px; position: relative;"></div>
              
				  <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>

                                </form>

    </body>
</HTML>

<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
window.dataLayer = window.dataLayer || [];
function gtag(){dataLayer.push(arguments);}
gtag('js', new Date());
gtag('config', 'G-GLYCMH7BRR');
</script>

