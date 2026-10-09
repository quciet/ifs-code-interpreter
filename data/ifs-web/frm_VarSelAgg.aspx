<%@ Reference Page="~/Index.aspx" %>
<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_VarSelAgg" Codebehind="frm_VarSelAgg.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
</HEAD>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Group Aggregation" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
		                
            <table>
                    <tr>
                        <td width=28px></td>
                        <td>
							<asp:Label id="Label1" runat="server">Forecast:</asp:Label>
                        </td>
                        <td width=28px></td>
                        <td>
							<asp:Label id="lblHis" runat="server" Visible="false">History:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px></td>
                        <td>
							<asp:listbox id="lstAgg" runat="server"	
							Height="88px" Width="200px" AutoPostBack="True" Rows="8"></asp:listbox>
                        </td>
                        <td width=28px></td>
                        <td>
							<asp:listbox id="lstHist" runat="server" Visible ="false"
							Height="88px" Width="200px" AutoPostBack="True" Rows="8"></asp:listbox>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px></td>
                        <td></td>
                        <td width=28px></td>
                        <td>
							<asp:Label id="lblMixed" runat="server" visible="false" ForeColor="Red">The historic aggregation rules are mixed!:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px></td>
                        <td>
							<asp:button id="cmdAggSelect" Text="(S)elect" accessKey="S" tabIndex="12" runat="server" Height="25" Width="113px" BackColor="Control" Enabled="True" 
				            Font-Underline="False" ForeColor="ControlText" CausesValidation="True"
				            Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" 
                                Font-Name="Arial" ToolTip="Select (Alt+S)"></asp:button>
                        </td>
                        <td width=28px></td>
                        <td>
							<asp:button id="cmdAggCancel" Text="Cancel" accessKey="C" tabIndex="12" runat="server" Height="25" Width="113px" BackColor="Control" Enabled="True" 
				            Font-Underline="False" ForeColor="ControlText" CausesValidation="True"
				            Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" 
                                Font-Name="Arial" ToolTip="Cancel (Alt+C)"></asp:button>
                        </td>
                    </tr>
                </table>	

    </form>
</body>
</html>
