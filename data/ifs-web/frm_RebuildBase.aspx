<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_RebuildBase" Codebehind="frm_RebuildBase.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="miu" Namespace="MarkItUp.WebControls" Assembly="MarkItUp.WebControls.Timer" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Rebuild Base International Futures</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="showpg();cmdRebBase_Click();">
		<script language="javascript">
			function showpg() {
				var doc = document.forms[0];

				if (doc.cmd_Cancel!=null && doc.cmd_Cancel.disabled == false && doc.cmd_Cancel.value == "Interrupt")
				{
					document.body.style.cursor = "wait";
					doc.cmd_Cancel.style.cursor = "wait";
					doc.cmd_Start.style.cursor = "wait";
				}
			};

			function cmdRebBase_Click() {
                var doc = document.forms[0];
				var btn = document.getElementById('cmd_RebBase');
				var aut = document.getElementById('txtAutomaticRun');
				if (aut.value == "1") {
                    aut.value = "2";
                    btn.click();
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
					<asp:TableCell Text="Rebuild Base" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<input id="txtAutomaticRun" name="txtAutomaticRun"  type="hidden" runat="server"  />
			<table width="100%">
				<tr>
					<td width="50%"><br>
					&nbsp;&nbsp;
						<asp:label id="lbl_RunYears" tabIndex="5" runat="server" Width="232px" 
                            Height="41px" BorderStyle="None"
							BackColor="Window" Text="Change Last Year of model:" Font-Name="Arial"
							Font-Size="10pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" 
                            Font-Underline="False" ForeColor="WindowText" Font-Names="Arial"></asp:label>&nbsp;<br />
                        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="lbl_RunYears0" tabIndex="5" runat="server" Width="86px" 
                            Height="33px" BorderStyle="None"
							BackColor="Window" Text="Last Year:" Font-Name="Arial"
							Font-Size="10pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" 
                            Font-Underline="False" ForeColor="WindowText" Font-Names="Arial"></asp:label>&nbsp;&nbsp;&nbsp; <asp:textbox 
                            id="txt_RunYears" tabIndex="6" runat="server" Width="57" Height="33" BorderStyle="Solid"
							BackColor="Window" Text="Text1" Visible="True" Font-Name="Arial" Font-Size="10pt" 
                            Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
							Enabled="True" ForeColor="WindowText" ReadOnly="False" MaxLength="0" Textmode="Singleline" 
                            AutoPostBack="False" Font-Names="Arial"></asp:textbox>&nbsp;
						
						&nbsp;&nbsp;
						<asp:button id="cmd_RebBase" accessKey="r" tabIndex="9" runat="server" 
                            Width="127px" Height="32px"
							BackColor="Control" Text="(R)ebuild Base" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText"
							CausesValidation="True"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmd_GotoRun" accessKey="r" tabIndex="9" runat="server" 
                            Width="127px" Height="32px"
							BackColor="Control" Text="(R)un the Model" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText"
							CausesValidation="True" Visible="False"></asp:button>&nbsp;&nbsp;
						<asp:button id="cmd_Exit" accessKey="x" tabIndex="11" runat="server" Width="127" Height="32px"
							BackColor="Control" Text="E(x)it" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
							Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True" ForeColor="ControlText"
							CausesValidation="True"></asp:button>&nbsp;
						    &nbsp;&nbsp;&nbsp;
                        	<br>
						<asp:CheckBox ID="chkBilateralData" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="X-Small" Text="Using Bilateral Data" AutoPostBack="True" />
                    </td>
					<td width="50%">
							<br />
							&nbsp;<!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16024);--></td>
				</tr>
				<tr>
					<td align="center" width="100%">
                            <table>
                            <tr>
                                <td><div id="container1"></div></td>
                                <td><div id="container2"></div></td>
                            </tr>
                            <tr>
                                <td><div id="container3"></div></td>
                                <td><div id="container4"></div></td>
                            </tr>
                        </table>                        		


                        <div><asp:Label ID="lblJSReb" runat="server"></asp:Label></div>                        
					</td>
				</tr>
			</table>
			<miu:timer id="Timer1" runat="server" Interval="5000" MaxTicks="1" CommandArgument="1"></miu:timer>
			<script language="javascript">
{
	window.history.forward();
}
			</script>
		</form>
	</body>
</HTML>
