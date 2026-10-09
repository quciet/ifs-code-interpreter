<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_RecreateSce" Codebehind="frm_RecreateSce.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recreate Scenario File</title>
            <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0" >
	<script type="text/javascript">

    function StartProcess() {
        var doc = document.forms[0];
        var cmd = doc.cmdRecreate

        document.body.style.cursor = "wait";
        cmd.style.cursor = "wait";

		cmd.value = "In progress";

		return true;
		};

    </script> 

    <form id="form1" runat="server">
		<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
			<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
				Font-Bold="True">
				<asp:TableCell  ID="LogoCell" Width="40px">
				<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
				<asp:TableCell Text="Recreate Scenario File" ID="TitleCell"></asp:TableCell>
			</asp:TableRow>
		</asp:table>

		<table>
			<tr>
				<td width=28px></td>
				<td width=250px>
					Runfile:<br />
					<asp:DropDownList ID="cboRunFile" runat="server"></asp:DropDownList>
				</td>
				<td width=250px>
					Base:<br />
					<asp:DropDownList ID="cboBase" runat="server"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td width=28px></td>
				<td width=250px>
					<br />
					<asp:Button id="cmdRecreate" 
				    tabIndex="24" runat="server" Width="113" Height="25" Text="(R)ecreate" ToolTip="Recreate (Alt+r)" Font-Name="Arial"
				    Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				    Enabled="True" ForeColor="ControlText" AccessKey="r"></asp:Button>
				</td>
				<td>
					<br />
					<asp:Button id="cmdExit" 
					tabIndex="24" runat="server" Width="113" Height="25" Text="E(x)it" ToolTip="Exit (Alt+x)" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					CausesValidation="True" Enabled="True" ForeColor="ControlText" AccessKey="x"></asp:Button>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
