<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.Migrated_frm_Confirm" Codebehind="frm_Confirm.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_Confirm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
        <!--jam 2021/11/27-->
		<%--<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="International Futures" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:Panel id="Panel1" style="Z-INDEX: 102; LEFT: 48px; POSITION: absolute; TOP: 104px" runat="server"
				Height="184px" Width="600px">
				<P>
					<asp:Label id="lblMsg" runat="server" Height="80px" Width="568px" Font-Bold="True" Font-Names="Arial">Label</asp:Label></P>
				<P>&nbsp;</P>
				<P align="center">
					<asp:Button id="cmdOK" accessKey="Y" runat="server" Height="40px" Width="104px" Text="Yes" ToolTip="Yes (Alt+Y)"></asp:Button>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Button id="cmdNO" accessKey="N" runat="server" Height="40px" Width="104px" Text="No" ToolTip="No (Alt+N)"></asp:Button>
				</P>
			</asp:Panel>
		</form>
	</body>
</HTML>
