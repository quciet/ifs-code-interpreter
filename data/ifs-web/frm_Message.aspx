<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Message" Codebehind="frm_Message.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_Message</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
        <!--jam 2021/11/27-->
		<%--<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
	    <style type="text/css">
            .auto-style1 {
                z-index: 102;
                left: 48px;
                position: absolute;
                top: 104px;
                width: 775px;
            }
        </style>
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
			<asp:Panel id="Panel1" runat="server"
				Height="184px" CssClass="auto-style1">
				<P>
					<asp:Label id="lblMsg" runat="server" Height="80px" Width="568px" Font-Bold="True" Font-Names="Arial">Label</asp:Label></P>
				<P>&nbsp;</P>
				<P align=right>
					<asp:Button id="cmdOK" accessKey="O" runat="server" Height="40px" Width="104px" Text="OK" ToolTip="OK (Alt+O)"></asp:Button></P>
			</asp:Panel>
		</form>
	</body>
</HTML>
