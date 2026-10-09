<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_SDG" Codebehind="frm_SDG.aspx.vb" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Millenium Development Goals</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
         <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<asp:dropdownlist id="cmbGoal" style="Z-INDEX: 102; LEFT: 26px; POSITION: absolute; TOP: 63px" runat="server"
				AutoPostBack="True" Width="232px"></asp:dropdownlist><asp:table id="grd_VariableDisplay" style="Z-INDEX: 107; LEFT: 464px; POSITION: absolute; TOP: 552px"
				runat="server" Width="280px" Height="178px" BorderColor="DarkGray" BorderStyle="Outset" Visible="False"></asp:table><asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="ActiveCaption" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.jpg" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Millennium Development Goals" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table><cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
				<asp:dropdownlist id="cmbTarget" style="Z-INDEX: 103; LEFT: 274px; POSITION: absolute; TOP: 63px"
				runat="server" AutoPostBack="True" Width="168px" Height="24px"></asp:dropdownlist>
				<asp:dropdownlist id="cmbIndicator" style="Z-INDEX: 101; LEFT: 458px; POSITION: absolute; TOP: 63px"
				runat="server" AutoPostBack="True" Width="136px" Height="24px"></asp:dropdownlist>
				<asp:dropdownlist id="cmb_Dim1" style="Z-INDEX: 104; LEFT: 610px; POSITION: absolute; TOP: 63px" runat="server" Width="240px" Height="24px"></asp:dropdownlist>
				
			<asp:panel id="Frame2" style="Z-INDEX: 114; LEFT: 608px; POSITION: absolute; TOP: 88px" runat="server"
				Width="200px" Height="86px" BorderStyle="Outset">
				<P>Display:</P>
				<P>
					<asp:RadioButton id="optChart" runat="server" AutoPostBack="True" GroupName="optDisp" Checked="True"
						Text="Chart"></asp:RadioButton>&nbsp;
					<asp:RadioButton id="optData" runat="server" AutoPostBack="True" GroupName="optDisp" Text="Data"></asp:RadioButton>&nbsp;
					<asp:RadioButton id="optBubble" runat="server" AutoPostBack="True" Visible="False" GroupName="optDisp"
						Text="Bubble" Enabled="False"></asp:RadioButton></P>
			</asp:panel>
			<asp:panel id="frm_Horizon" style="Z-INDEX: 111; LEFT: 860px; POSITION: absolute; TOP: 63px"
				runat="server" Width="192px" Height="128px" BorderStyle="Outset" Visible="False" BackColor="White">
				<P>Select Horizon</P>
				<P>
					<asp:DropDownList id="cmb_Horizon" runat="server" Width="168px" AutoPostBack="True" Height="64px"></asp:DropDownList></P>
			</asp:panel>
			<TABLE id="Table1" style="Z-INDEX: 112; LEFT: 24px; WIDTH: 704px; POSITION: absolute; TOP: 88px; HEIGHT: 40px"
				cellSpacing="0" cellPadding="0" width="300" border="0">
				<tr>
				    <td colspan="2">
			            <asp:panel id="Panel1" style="Z-INDEX: 105; " runat="server"
				            Width="578px" BorderColor="Transparent" BorderStyle="Outset">
				            <P>
					            <asp:Label id="lblGoal" runat="server" Width="568px" Font-Bold="True" Font-Size="Medium">Goal</asp:Label></P>
				            <P>
					            <asp:Label id="lblTarget" runat="server" Width="568px" Height="24px" Font-Bold="True">Target</asp:Label></P>
				            <P>
					            <asp:Label id="lblIndicator" runat="server" Width="560px" Height="24px">Indicator</asp:Label></P>
			                <P><asp:label id="lblGraphNote" style="Z-INDEX: 106; "
				            runat="server" Width="688px" Height="16px" Font-Bold="True" Font-Underline="True"></asp:label></P>
				            <P><asp:label id="lblProgressRate" style="Z-INDEX: 108; "
				            runat="server" Width="192px" Height="24px" Visible="False">Test</asp:label></P>
			            </asp:panel>				    
				    </td>
				</tr>
				<TR>
					<TD colSpan="2">
                        <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>		
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

					    <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>
                    </TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:table id="grd_TableDisplay" runat="server" Width="696px" Height="286px" BorderColor="DarkGray"
							BorderStyle="Outset" Visible="False"></asp:table></TD>
				</TR>
				<TR>
					<TD align="center">
                        <br />
                        <br />
                        <asp:listbox id="lstFiles" runat="server" Width="288px" Height="134px" SelectionMode="Multiple"
							AutoPostBack="True"></asp:listbox></TD>
					<TD align="center">
                        Use the Switch/Compare option to compare a country in the box below with the 
                        country selected below the menu bar<br /><asp:listbox id="lstcountries" runat="server" Width="296px" Height="134px" SelectionMode="Multiple"></asp:listbox></TD>
				</TR>
			</TABLE>
		</form>
			<script language="javascript">
{
	if ((document.forms[0].cmb_Country != null)&& (document.forms[0].cmb_Country.disabled == false ) ) 
          {
               document.forms[0].cmb_Country.focus();
          }
}

//jrs 2011/05/04
function cntrylist_click()
{
    var doc = document.forms[0];
    var ctry="Country="+RegCode[doc.cmb_Dim1.selectedIndex+1];
    window.location.search=ctry;
}

//jrs 2011/05/05
function lstGroups_click()
{
    var doc = document.forms[0];
    var grp="Group="+doc.cmb_Dim1[doc.cmb_Dim1.selectedIndex].text;
    window.location.search=grp;
}
			</script>
		
	</body>
</HTML>

<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-3138457-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>
