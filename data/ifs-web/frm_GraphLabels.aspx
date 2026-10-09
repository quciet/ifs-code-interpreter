<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_GraphLabels" Codebehind="frm_GraphLabels.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
			<asp:button id="cmdUpdVar" style="Z-INDEX: 111; LEFT: 16px; POSITION: absolute; TOP: 296px; TEXT-ALIGN: center"
				accessKey="u" tabIndex="5" runat="server" Width="81" Height="33" BackColor="Control" ForeColor="ControlText"
				Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8.0" Font-Name="Arial" CausesValidation="True" Text="(U)pdate"></asp:button>
			<asp:textbox id="txtDim1" style="Z-INDEX: 108; LEFT: 216px; POSITION: absolute; TOP: 72px; TEXT-ALIGN: left"
				tabIndex="2" runat="server" Width="144px" Height="25" BackColor="Window" ForeColor="WindowText"
				Enabled="True" BorderStyle="Inset" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Textmode="Singleline" MaxLength="0" ReadOnly="False"></asp:textbox>
			<asp:textbox id="txtDim2" style="Z-INDEX: 107; LEFT: 376px; POSITION: absolute; TOP: 72px; TEXT-ALIGN: left"
				tabIndex="3" runat="server" Width="144px" Height="25" BackColor="Window" ForeColor="WindowText"
				Enabled="True" BorderStyle="Inset" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Textmode="Singleline" MaxLength="0" ReadOnly="False"></asp:textbox>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Height="15px" Width="100%" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Format Graph" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:listbox id="lstDim1" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 112px" runat="server"
				Width="504px" BackColor="White" Font-Size="X-Small" Font-Names="Arial" AutoPostBack="True"
				tabIndex="4" Height="176"></asp:listbox>
			<asp:textbox id="txtVarName" style="Z-INDEX: 100; LEFT: 16px; POSITION: absolute; TOP: 71px; TEXT-ALIGN: left"
				tabIndex="1" runat="server" Height="25" Width="185" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BorderStyle="Inset" Enabled="True"
				ForeColor="WindowText" BackColor="Window" ReadOnly="False" MaxLength="0" Textmode="Singleline"></asp:textbox>
			<asp:button id="cmdExit" style="Z-INDEX: 102; LEFT: 120px; POSITION: absolute; TOP: 296px; TEXT-ALIGN: center"
				accessKey="x" tabIndex="6" runat="server" Height="33" Width="81" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True"
				ForeColor="ControlText" BackColor="Control" Text="E(x)it" CausesValidation="True"></asp:button>
			<asp:label id="lblInstructions" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 35px; TEXT-ALIGN: left"
				tabIndex="7" runat="server" Height="26" Width="267" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BorderStyle="None" Enabled="True"
				ForeColor="WindowText" Visible="True" BackColor="Window" Text="Click Variable Name to edit for graph; edit cell and hit enter to register your change to the label:">Click Variable Name to edit for graph; edit cell and hit enter to register your change to the label:</asp:label>
			<script language="javascript">
{
	if ((document.all['txtVarName'] != null)&& (document.all['txtVarName'].disabled == false ) ) 
          {
               document.all('txtVarName').focus();
          }
}
			</script>
		</form>
	</body>
</HTML>
