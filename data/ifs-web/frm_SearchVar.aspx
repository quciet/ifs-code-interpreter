<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_SearchVar" Codebehind="frm_SearchVar.aspx.vb" %>
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
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 45px" runat="server"
				Font-Bold="True" Font-Names="Arial"> Type Variable name or description and click Search</asp:Label>
			<asp:ListBox id="lstVarIdx" style="Z-INDEX: 108; LEFT: 608px; POSITION: absolute; TOP: 56px"
				runat="server" Visible="False"></asp:ListBox>
			<asp:Button id="cmdContinue" style="Z-INDEX: 107; LEFT: 596px; POSITION: absolute; TOP: 128px"
				runat="server" Text="Continue" accessKey="C" tabIndex="6" Width="88px" ToolTip="Continue (Alt+C)"></asp:Button>
			<asp:Button id="cmdLoad" style="Z-INDEX: 106; LEFT: 596px; POSITION: absolute; TOP: 88px" runat="server"
				Width="88px" Text="Load" accessKey="L" tabIndex="4" ToolTip="Load (Alt+L)"></asp:Button>
			<asp:Button id="cmdSearch" style="Z-INDEX: 105; LEFT: 596px; POSITION: absolute; TOP: 48px"
				runat="server" Width="88px" Text="Search" accessKey="S" tabIndex="3" ToolTip="Search (Alt+S)"></asp:Button>
            <input id="btnHelp" onclick="javascript:ShowHelp();"
                STYLE="LEFT:596px; POSITION:absolute; TOP:168px; width: 88px; height: 24px;" type="button" value="Help" />				
                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16044);-->
			<asp:ListBox id="lstSearch" style="Z-INDEX: 104; LEFT: 17px; POSITION: absolute; TOP: 128px"
				runat="server" tabIndex="2" AutoPostBack="False" Height="296" Width="556"></asp:ListBox>
			<asp:TextBox id="txtSearch" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 69px" runat="server"
				tabIndex="1" Width="556"></asp:TextBox>
			<asp:Label id="Label2" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 101px" runat="server"
				Width="564px" Font-Bold="True" Font-Names="Arial">Select one and click Load to return to Variable/Parameter Selection</asp:Label>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Variable/Parameter Search" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
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
			<SCRIPT language="JavaScript" type="text/javascript">
	
			// Function which shows the Context Menu by calling
			// cswmShow and passing in coordinates.
			function ShowContextMenu(e)
			{
				// Quick and Dirty IE NN 6.1 Detection
				var flag = false;
				if (navigator.appName=='Netscape')
					flag = true;
				// NN
				if(flag)
					cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
				// IE
				else
					cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
			}
			
			// jrs 08/19/04 if list is visible 
			if (document.forms[0].lstSearch != null)
			{
				if (document.forms[0].lstSearch.captureEvents != null)
					document.forms[0].lstSearch.captureEvents(Event.MOUSEDOWN);
				document.forms[0].lstSearch.onmousedown=ShowContextMenu;
			}
			</SCRIPT>
			<SCRIPT language="JavaScript">
			function showExplain() {
			//mti August 2004 (new function added)
			var doc = document.forms[0];
			
			// jrs 11/11/04
			if (doc.lstSearch.selectedIndex > -1)
			{
				var hidboth = doc.lstSearch.options[doc.lstSearch.selectedIndex].value;
				//var startsel=hidboth.indexOf("-")+1;
				var endsel=hidboth.indexOf("/");
				var hid = hidboth.substring(0, endsel);
				
				var sParam = "left="+screen.width+",top="+screen.height+",width=100,height=100";
				//jrs 2012/01/17 var hwindow = window.open("WebHelp/ifshelp.htm" + "#<id="+hid+">>wnd=" + "IFs Help", "__webCshStub", sParam);
				//jrs 2014/07/18 RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,hid);
				var hwindow = window.open(IFsVarHelpPath[doc.lstSearch.selectedIndex]);
			}			
			}
			 
			function showEquation() {
			//mti August 2004 (new function added)
			var doc = document.forms[0];
			
			// jrs 11/11/04 Right click doesn't select an item
			if (doc.lstSearch.selectedIndex > -1)
			{
				var hidboth = doc.lstSearch.options[doc.lstSearch.selectedIndex].value;
				var startsel=hidboth.indexOf("/")+1;
				var endsel = hidboth.indexOf("-");
				var hid = hidboth.substring(startsel, endsel);
				//jrs 2012/01/17 var hwindow = window.open("./WebHelp/ifshelp.htm" + "#<id="+hid, "__webCshStub");
				//jrs 2014/07/18 RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,hid);
				var hwindow = window.open(IFsVarHelpEqnPath[doc.lstSearch.selectedIndex]);
			}
			}
			
			function showDefinition() {
			var doc = document.forms[0];
			
			// jrs 11/11/04 Right click doesn't select an item
			if (doc.lstSearch.selectedIndex > -1)
			{
				var hidboth = doc.lstSearch.options[doc.lstSearch.selectedIndex].value;
				var startsel=hidboth.indexOf("-")+1;
				var hid=hidboth.substring(startsel, hidboth.length);
				alert(hid);
			}
			}
			</SCRIPT>
			<cswm:webmenu id="wmContext" runat="server" Width="16px" Height="16px" MenuBar-AbsoluteDockEnabled="False"
				MenuBar-AbsoluteDockPaddingAdjust="False" MenuBar-DisplayMode="None" style="Z-INDEX: 109; LEFT: 712px; POSITION: absolute; TOP: 40px"></cswm:webmenu>
		</form>
	</body>
</HTML>
