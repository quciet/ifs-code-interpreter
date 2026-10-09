<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_SelectProjectFile" Codebehind="frm_SelectProjectFile.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Load Project Data</title>
        <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script></head>
<body leftMargin="0" topMargin="0" rightMargin="0" >
	<script type="text/javascript">

    function checkFile(sFileExists) {
        //Check if there was a backup file to indicate if another file was previously loaded
        //----------------------------------------------------------------------------------
		if (sFileExists == 'True') {
			var ans = confirm("Project Data has been already loaded to the system, are you sure you want to continue without removing previously added Project Data?")
			if (!ans) {
				return false;
			}
			else {
				return true;
			}
		}
		else {
			return true;
		}
    }
    </script> 
    <form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Load Project Data" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

				<table>
			<tr>
				<td width=28px></td>
				<td colspan="2">
					<br />
					<asp:ListBox ID="lstFiles" runat="server" Width="365px"></asp:ListBox>
					<br /><br />
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td width=28px></td>
				<td width=250px>
					<asp:Button id="cmdLoad" 
				    tabIndex="24" runat="server" Width="113" Height="25" Text="(L)oad" ToolTip="Load (Alt+l)" Font-Name="Arial"
				    Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				    CausesValidation="True" Enabled="True" ForeColor="ControlText" AccessKey="l"></asp:Button>
				</td>
				<td>
					<asp:Button id="cmdExit" 
					tabIndex="24" runat="server" Width="113" Height="25" Text="(C)ancel" ToolTip="Cancel (Alt+c)" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					CausesValidation="True" Enabled="True" ForeColor="ControlText" AccessKey="c"></asp:Button>
				</td>
			</tr>
		</table>

    </form>
</body>
</html>
