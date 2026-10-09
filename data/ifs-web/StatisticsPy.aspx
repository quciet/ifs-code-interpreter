<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_statisticsPy" Codebehind="StatisticsPy.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!-- jam 2021/11/28-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0" topMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Statistics" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 109; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu><asp:label id="ToolTip1" runat="server" BackColor="Red"></asp:label><asp:listbox id="Variables" style="Z-INDEX: 100; LEFT: 33px; POSITION: absolute; TOP: 128px"
				tabIndex="5" runat="server" Height="59" Width="392px" BackColor="Window" AutoPostBack="True" Visible="True" SelectionMode="Single" ForeColor="WindowText" Enabled="True" BorderStyle="Inset" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True"
				Font-Size="8.25" Font-Name="Arial"></asp:listbox>
            <asp:listbox id="Secondary" style="Z-INDEX: 103; LEFT: 33px; POSITION: absolute; TOP: 410px; height: 281px;"
				tabIndex="2" runat="server" Width="384px" BackColor="Window" AutoPostBack="True" 
                Visible="True" SelectionMode="Single" ForeColor="WindowText"
				Enabled="True" BorderStyle="Inset" Font-Underline="False" Font-Strikeout="False" 
                Font-Italic="False" Font-Bold="True" Font-Size="8.25" Font-Name="Arial"></asp:listbox>
            <asp:listbox id="Primary" 
                style="Z-INDEX: 102; LEFT: 33px; POSITION: absolute; TOP: 240px; height: 125px;" tabIndex="1"
				runat="server" Width="384px" BackColor="Window" AutoPostBack="True" Visible="True" 
                SelectionMode="Single" ForeColor="WindowText" Enabled="True" 
                BorderStyle="Inset" Font-Underline="False" Font-Strikeout="False" 
                Font-Italic="False" Font-Bold="True" Font-Size="8.25"
				Font-Name="Arial"></asp:listbox><asp:label id="Label3" style="Z-INDEX: 104; LEFT: 33px; POSITION: absolute; TOP: 96px; TEXT-ALIGN: left"
				tabIndex="6" runat="server" Height="17" Width="297" BackColor="White" Visible="True" ForeColor="ControlText" Enabled="True"
				BorderStyle="None" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="8.25" Font-Name="Arial"
				Text="Variable Names (Transformation Applied)"></asp:label>
            <asp:label id="Label2" style="Z-INDEX: 105; LEFT: 33px; POSITION: absolute; TOP: 380px; TEXT-ALIGN: left"
				tabIndex="4" runat="server" Height="17" Width="129" BackColor="White" Visible="True" 
                ForeColor="ControlText" Enabled="True" BorderStyle="None" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="8.25" 
                Font-Name="Arial" Text="Secondary Statistics"></asp:label><asp:label id="Label1" style="Z-INDEX: 106; LEFT: 33px; POSITION: absolute; TOP: 208px; TEXT-ALIGN: left"
				tabIndex="3" runat="server" Height="17" Width="105" BackColor="White" Visible="True" ForeColor="ControlText" Enabled="True" BorderStyle="None" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True"
				Font-Size="8.25" Font-Name="Arial" Text="Primary Statistics"></asp:label>
			<script language="javascript">
{
	if ((document.all['Primary'] != null)&& (document.all['Primary'].disabled == false ) ) 
          {
               document.all('Primary').focus();
          }
}
			</script>
			<aiswc:mainmenu id="MainMenu1" runat="server" Visible="False"></aiswc:mainmenu></form>
	</body>
</HTML>
