<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.ErrorHandler" Codebehind="ErrorHandler.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ErrorHandler</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<h2>Error Handler for IFsWeb.</h2>
			<hr>
			<table width="100%" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 88px" >
			<tr>
			<td>
			<asp:label id="lblError" runat="server"
				Height="32px">Label</asp:label>
			</td>
			</tr>
			<tr>
			<td>
			<br>
			<br>
			<asp:Button id="btnContinue" runat="server" Width="80px" Height="40px" Text="Restart" ></asp:Button>
			</td>
			</tr>
			</table>
		</form>
	</body>
</HTML>
