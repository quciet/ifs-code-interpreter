<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_SearchParam" Codebehind="frm_SearchParam.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_SearchParam</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Parameter Search" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="wmContext" runat="server" Width="16px" Height="16px" MenuBar-AbsoluteDockEnabled="False"
				MenuBar-AbsoluteDockPaddingAdjust="False" MenuBar-DisplayMode="None"></cswm:webmenu>
			<asp:label id="Label1" style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 35px" runat="server"
				Font-Bold="True" Font-Names="Arial"> Type Parameter name or description and click Search</asp:label>

                <asp:listbox id="lstVarIdx" style="Z-INDEX: 111; LEFT: 608px; POSITION: absolute; TOP: 56px"
				runat="server" Visible="False"></asp:listbox>
				<asp:button id="cmdContinue" style="Z-INDEX: 110; LEFT: 596px; POSITION: absolute; TOP: 128px"
				accessKey="C" tabIndex="5" runat="server" Text="Continue" ToolTip="Continue (Alt+C)" Width="88px"></asp:button>

                <input id="btnHelp" onclick="javascript:ShowHelp();"
                STYLE="LEFT:596px; POSITION:absolute; TOP:168px; width: 88px; height: 24px;" type="button" value="Help" />
                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16044);-->
				<asp:button id="cmdLoad" style="Z-INDEX: 109; LEFT: 596px; POSITION: absolute; TOP: 88px" accessKey="L"
				tabIndex="4" runat="server" Width="88px" Text="Load" ToolTip="Load (Alt+L)"></asp:button>
				<asp:button id="cmdSearch" style="Z-INDEX: 105; LEFT: 596px; POSITION: absolute; TOP: 48px"
				accessKey="S" tabIndex="3" runat="server" Width="88px" Text="Search" ToolTip="Search (Alt+S)"></asp:button>
			<asp:listbox id="lstSearch" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 112px"
				tabIndex="2" runat="server" Height="296" Width="556"></asp:listbox>
			<asp:textbox id="txtSearch" style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 55px" tabIndex="1"
				runat="server" Width="556"></asp:textbox>
				<asp:label id="Label2" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 80px" runat="server"
				Font-Bold="True" Width="476px" Font-Names="Arial">Select Parameter and click Load to return to Tree</asp:label>
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
				//jrs 2014/07/18 var hwindow = window.open("WebHelp/ifshelp.htm" + "#<id="+hid+">>wnd=" + "IFs Help", "__webCshStub", sParam);
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
				//var hwindow = window.open("./WebHelp/ifshelp.htm" + "#<id="+hid, "__webCshStub");
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
		</form>
	</body>
</HTML>
