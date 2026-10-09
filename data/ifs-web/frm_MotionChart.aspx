<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_MotionChart" Codebehind="frm_MotionChart.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<html>
<head runat="server">
    <title>IFsWeb</title>
    <!--jam 2021/11/27-->
    <%--<script type="text/javascript" src="http://www.google.com/jsapi"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>--%>
     <script src="<%= Page.ResolveUrl("~/Scripts/loader.js")%>"></script>
</head>
<body leftMargin="0" rightMargin="0" topmargin="0">
    <form id="form1" runat="server">
        <input id="txtResValueHolder" name="txtResValueHolder"  type="hidden" />
		<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
			<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
				Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
				<asp:TableCell Text="Motion Chart (Using Gapminder by Google)" ID="TitleCell"></asp:TableCell>
			</asp:TableRow>
		</asp:Table>

			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
			
        <div id="chart_div" style="width: 600px; height: 300px;"></div>  

    </form>
</body>
</html>
