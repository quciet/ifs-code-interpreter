<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_ForecastGroupAggregation" Codebehind="frm_ForecastGroupAggregation.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Aggregation</title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0" >
    <form id="frm_ForecastGroupAggregation" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Group Aggregation" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
		<table>
			<tr>
				<td width=200px align="center">
					<asp:Label ID="Label1" runat="server" Text="Forecast:"></asp:Label>
				</td>
				<td width=200px align="center">
					<asp:Label ID="lblHis" runat="server" Text="History:" Visible="False"></asp:Label>
				</td>
			</tr>
			<tr>
				<td width=200px align="center">
					<asp:ListBox ID="lstAgg" runat="server" AutoPostBack="True"></asp:ListBox>
				</td>
				<td width=200px align="center">
					<asp:ListBox ID="lstHist" runat="server" Visible="False"></asp:ListBox>
				</td>
			</tr>
			<tr>
				<td width=200px align="center">
					<asp:Button ID="cmdSelect" runat="server" Text="(S)elect" AccessKey="s" />
				</td>
				<td width=200px align="center">
					<asp:Button ID="cmdCancel" runat="server" Text="(C)ancel" AccessKey="c" />
				</td>
			</tr>
		</table>
		<asp:Label ID="lblMixed" runat="server" Text="The historic aggregation rules are mixed!" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
    </form>
</body>
</html>
