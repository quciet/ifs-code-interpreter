<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_WVSDisplay" Codebehind="frm_WVSDisplay.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>World Value Survey by Age</title>
		
        <!-- jam 2021/07/18
        <SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
        -->
        <!--jam 2021/07/18-->
         <SCRIPT language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript">
		</SCRIPT>	

		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
        
		<meta content="JavaScript" name="vs_defaultClientScript">
         <!--jam 2021/11/27-->
		<!--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">-->
		<META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
         <!--jam 2021/11/27-->
        <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
        <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        <!--jam 2021/07/18
        <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>
		<script language="javascript" type="text/javascript" src="Scripts/common.js"></script>		
        -->

        <!--jam 2021/07/18-->
        <%--<script src="<%= Page.ResolveUrl("~/Scripts/exporting.js")%>"></script>
        <script src="<%= Page.ResolveUrl("~/Scripts/highcharts.js")%>"></script>--%>


		<!--jam 2025/03/11 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>

		  <!--jam 2025/03/11 New highcharts version libraries-->
		 <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>

        <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/common.js")%>"></script>

	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form runat="server">
			<%--jam 2025/02/05 --%>
			<%--<div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px;"><img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>--%>
            
			  <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px;"><img id="loadimg" src="<%= Page.ResolveUrl("~/images/squares-circle.gif")%>"  height="93" width="93"><br>Loading resources...</div>
			
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="World Value Survey by Age" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 130; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
            &nbsp;
			<asp:dropdownlist id="cboFiles" style="Z-INDEX: 101; LEFT: 444px; POSITION: absolute; TOP: 88px" tabIndex="10"
				runat="server" Height="22" Width="329" BackColor="Window" AutoPostBack="True" ForeColor="WindowText"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				Font-Name="Arial"></asp:dropdownlist>
			<asp:dropdownlist id="cmbVariable" style="Z-INDEX: 102; LEFT: 276px; POSITION: absolute; TOP: 88px"
				tabIndex="8" runat="server" Height="22" Width="153" BackColor="Window" AutoPostBack="True" ForeColor="WindowText"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				Font-Name="Arial"></asp:dropdownlist>
			<asp:dropdownlist id="cmb_Country" style="Z-INDEX: 103; LEFT: 20px; POSITION: absolute; TOP: 88px"
				tabIndex="6" runat="server" Height="22" Width="232px" BackColor="Window" ForeColor="WindowText"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				Font-Name="Arial"></asp:dropdownlist>
			<asp:label id="Lbl_File" style="Z-INDEX: 106; LEFT: 444px; POSITION: absolute; TOP: 64px" tabIndex="11"
				runat="server" Height="17" Width="81" BackColor="Window" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial"
				Text="Select File:"></asp:label>
			<asp:label id="Label1" style="Z-INDEX: 107; LEFT: 276px; POSITION: absolute; TOP: 64px" tabIndex="9"
				runat="server" Height="17" Width="161" BackColor="Window" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial"
				Text="Variable"></asp:label>
			<asp:label id="lbl_countries" style="Z-INDEX: 108; LEFT: 20px; POSITION: absolute; TOP: 64px"
				tabIndex="7" runat="server" Height="17" Width="161" BackColor="Window" ForeColor="WindowText"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75"
				Font-Name="Arial" Text="Countries or Regions"></asp:label>
			<asp:Panel ID="pnlGraph" style="Z-INDEX: 112; LEFT: 21px; POSITION: absolute; TOP: 115px" runat="server">
				 <%--jam 2025/03/11 New highcharts version libraries container width adjusted from 90% to 100%--%>
				<%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>	
				<div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>
                <div><asp:Label ID="lblJS" runat="server"></asp:Label>&nbsp;</div>                        
			</asp:Panel>
			<script language="javascript">
{
	if ((document.all['grd_LineData'] != null)&& (document.all['grd_LineData'].disabled == false ) ) 
          {
               document.all('grd_LineData').focus();
	}

	if (document.getElementById('lblJS') == null) {
	    //JAM 2013-04-02
	    document.getElementById('firstloading').style.visibility = "hidden";
	}

}

//jrs 2011/05/04
function cntrylist_click()
{
    var doc = document.forms[0];
    var ctry="Country="+RegCode[doc.cmb_Country.selectedIndex+1];
    window.location.search=ctry;
}

//jrs 2011/05/05
function lstGroups_click()
{
    var doc = document.forms[0];
    //jam 2021/07/20
    //var grp = "Group=" + PrepareForURL(doc.cmb_Country[doc.cmb_Country.selectedIndex].text);
    var grp=PrepareForURL(doc.cmb_Country[doc.cmb_Country.selectedIndex].text);
    //jam 2021/07/20
    //window.location.search = grp;
    window.location.href = grp;
}
			</script>
			</form>
			<!--
			<table style="Z-INDEX: 115; LEFT: 190px; POSITION: absolute; TOP: 547px" >	
				<tr>
				<td Height="33" >
                                        <input class="btn" id="Submit1"  style="WIDTH: 85px; height:33px;" onclick="Maximize_Button()" type="submit" value="Maximize" name="Submit1">
                                        <input class="btn" id="Submit2"  style="WIDTH: 85px; height:33px;" onclick="Print_Button()" type="submit" value="Print" name="Submit2">
                                        <input class="btn" id="Submit3"  style="WIDTH: 85px; height:33px; " onclick="Export_Button()" type="submit" value="Export" name="Submit3">
                 
                </td>
		        </tr>
            </table>  
			-->
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