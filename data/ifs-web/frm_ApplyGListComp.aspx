<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ApplyGListComp" Codebehind="frm_ApplyGListComp.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_ApplyGListComp</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
        <!--jam 2021/11/27-->
		<%--<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="cmdContinue" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 35px"
				accessKey="C" runat="server" Text="(C)ontinue"></asp:Button>
			<asp:ListBox id="lstGList" style="Z-INDEX: 102; LEFT: 40px; POSITION: absolute; TOP: 64px" runat="server"
				Height="392px" Width="326px"></asp:ListBox>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Apply GList to Computation" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
		</form>
	</body>
</HTML>
