<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_MortDisplay" Codebehind="frm_MortDisplay.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>Mortality Display</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
     <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
       <%-- <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>--%>


        <!--jam 2025/03/11 New highcharts version libraries-->
        <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
        <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
   
        <!--jam 2025/03/11 New highcharts version libraries-->
        <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>

</head>
<body bgColor="white" leftMargin="0" rightMargin="0" topmargin="0">
    <form id="frm_MortDisplay" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma" Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/14-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Mortality Display: J-Curve" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

			<cswm:webmenu id="wmMain" runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockPaddingAdjust="False"
				MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>

        <table>
            <asp:Panel ID="pnlMort" runat="server">
                <tr>
                    <td style="width: 24%">
                            <asp:label id="lblReg" runat="server"
	                                Font-Bold="True">Regions:</asp:label>
	                        <br />
    				                
                            <asp:listbox id="lstDim1" runat="server"
                            Height="430" Width="200" SelectionMode="Multiple"></asp:listbox>
                    </td>
                    <td style="width: 24%">
                            <asp:label id="lblDim2" runat="server"
	                                Font-Bold="True" Width="117px">Mortality Type:</asp:label>
	                        <br />

                            <asp:listbox id="lstDim2" runat="server"
                            Height="430" Width="170" SelectionMode="Multiple"></asp:listbox>
                    </td>
                    <td style="width: 30%">
                            <asp:label id="lblDim3" runat="server"
	                                Font-Bold="True">Gender:</asp:label>
	                        <br />

                            <asp:listbox id="lstDim3" runat="server"
                            Height="130" Width="250" SelectionMode="Multiple"></asp:listbox>
	                        <br />

    		                
                            <asp:label id="lblFiles" runat="server"
	                                Font-Bold="True">Files:</asp:label>
	                        <br />

                            <asp:listbox id="lstFiles" runat="server"
                            Height="272" Width="250" SelectionMode="Multiple"></asp:listbox>
                    </td>
                    <td style="width: 22%">
                            <asp:label id="lblYears" runat="server"
	                                Font-Bold="True">Years:</asp:label>
	                        <br />
                            <asp:listbox id="lstYears" runat="server"
                            Height="430" Width="200" SelectionMode="Multiple"></asp:listbox>
                    </td>
                </tr>
            </asp:Panel>                
            <tr>
                <td colspan="4" align=center style="width: 100%">
                    <%--jam 2025/03/11 New highcharts version libraries container width adjusted from 90% to 100%--%>
                   <%-- <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>	--%>
                    <div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>	
                    <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>     
                    <br />                   
                </td>
            </tr>
            <tr>
                <td colspan="4" style="width: 100%">
                    <asp:Table id="grdMort" runat="server" BorderStyle="Outset"
				    BorderColor="DarkGray" Visible="false"></asp:Table>
                </td>
            </tr>
            <asp:Panel ID="pnlButtons" runat="server">           
                <tr>
                    <td colspan="4" align=center style="width: 100%">
                        <asp:button id="cmdGraph" style="Z-INDEX: 106; LEFT: 32px" tabIndex="1"
	                            runat="server" Width="88px" Text="Graph" AccessKey="g"></asp:button>
                        <asp:button id="cmdTable" style="Z-INDEX: 107; LEFT: 136px" runat="server"
	                            Width="88px" Text="Table" AccessKey="t"></asp:button>
                        <asp:button id="cmdClear" style="Z-INDEX: 107; LEFT: 240px" runat="server"
	                            Width="88px" Text="Clear" AccessKey="c"></asp:button>                    
                    </td>
                </tr>
            </asp:Panel>
            <asp:Panel ID="pnlAdltDef" runat="server" Visible="false">                       
                <tr>
                    <td colspan="4" style="width: 100%">
                        <asp:label id="lblDef1" runat="server"
                                Font-Bold="True">Please enter starting age for adults (only multiples of 5)</asp:label>
                        <asp:TextBox id="txtDef1" tabindex="1" runat="server" Width="88px"></asp:TextBox><br />
                        
                        <asp:label id="lblDef2" runat="server"
                                Font-Bold="True">Please enter ending age for adults (only multiples of 5 - 1 (or 100), ex. 54, 59, 64)</asp:label>
                        <asp:TextBox id="txtDef2" tabindex="1" runat="server" Width="88px"></asp:TextBox><br />

                        <asp:button id="cmdSaveAdltDef" style="Z-INDEX: 106; LEFT: 32px" tabIndex="1"
	                            runat="server" Width="88px" Text="Save" AccessKey="s"></asp:button>
                        <asp:button id="cmdCancelAdltDef" style="Z-INDEX: 107; LEFT: 136px" runat="server"
	                            Width="88px" Text="Cancel" AccessKey="c"></asp:button><br />
	                            
	                    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="red" Visible="false">
	                    Starting age has to be smaller than ending age, original values will not be changed.
	                    </asp:Label>
                    </td>
                </tr>
            </asp:Panel>
        </table>				
	</form>
</body>
</html>

<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>
