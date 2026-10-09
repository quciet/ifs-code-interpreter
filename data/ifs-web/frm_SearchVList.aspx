<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_SearchVList" Codebehind="frm_SearchVList.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
		<title>frm_SearchParam</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
        <!--jam 2021/11/27-->
		<%--<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
		    <asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 34px" runat="server"
				Font-Bold="True" Font-Names="Arial"> Type Variable List, Variable name or description and click Search</asp:Label>
			<asp:ListBox id="lstVarIdx" style="Z-INDEX: 108; LEFT: 607px; POSITION: absolute; TOP: 56px"
				runat="server" Visible="False"></asp:ListBox>
			<asp:Button id="cmdLoad" style="Z-INDEX: 106; LEFT: 597px; POSITION: absolute; TOP: 88px" runat="server"
				Width="130px" Text="Load" accessKey="L" tabIndex="4" ToolTip="Load (Alt+L)"></asp:Button>
            <asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Search Variable Lists" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:Button id="cmdContinue" style="Z-INDEX: 107; LEFT: 597px; POSITION: absolute; TOP: 128px"
				runat="server" Text="Continue" accessKey="C" tabIndex="6" Width="130px" ToolTip="Continue (Alt+C)"></asp:Button>
			<asp:Button id="cmdSearch" style="Z-INDEX: 105; LEFT: 597px; POSITION: absolute; TOP: 48px"
				runat="server" Width="130px" Text="Search" accessKey="S" tabIndex="3" ToolTip="Search (Alt+S)"></asp:Button>
            <input id="btnHelp" onclick="javascript:ShowHelp();"
                STYLE="LEFT:597px; POSITION:absolute; TOP:168px; width: 130px; height: 24px;" type="button" value="Help" />				
                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16044);-->
			<asp:ListBox id="lstSearch" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 112px"
				runat="server" tabIndex="2" AutoPostBack="False" Height="296" Width="556"></asp:ListBox>
			<asp:TextBox id="txtSearch" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 58px" runat="server"
				tabIndex="1" Width="556"></asp:TextBox>
			<asp:Label id="Label2" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 90px" runat="server"
				Width="564px" Font-Bold="True" Font-Names="Arial">Select one and click Load to return to Flex Packaged Displays</asp:Label>
            
			<script language="javascript">
			{
				if ((document.forms[0].txtSearch != null) && (document.forms[0].txtSearch.disabled == false))
				{
					document.forms[0].txtSearch.focus();
				}
				//jrs 2012/01/16
                function clickButton(e, buttonid){
                      var evt = e ? e : window.event;
                      var bt = document.getElementById(buttonid);
                      if (bt){
                          if (evt.keyCode == 13){
                                bt.click();
                                return false;
                          }
                      }
                }		
			}
			</script>
		</form>
	</body>
</HTML>
