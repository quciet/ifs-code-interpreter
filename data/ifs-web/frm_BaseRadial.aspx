<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_BaseRadial" Codebehind="frm_BaseRadial.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Radial Display</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<META http-equiv="CACHE-CONTROL" content="NO-CACHE">
		<META http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="-1">
        <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>

		 <!--jam 2025/02/23 New highcharts version libraries-->
     <%--   <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>--%>


		 <!--jam 2025/02/23 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
   
		<!--jam 2025/02/23 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>


        <%--JAM 2018-08-02 Referencing local js library due incompatibility with high chart library--%>
      <%--  <script src="Scripts/highcharts-more.js"></script>--%>

			<!--jam 2025/02/23 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts-more.js")%>"></script>


        <%--<script src="http://code.highcharts.com/highcharts-more.js"></script>        
	</HEAD>--%>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Radial Graphical Display" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 130; LEFT: 0px; POSITION: absolute; TOP: 16px"
							runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>

    <div id="divPanel">
			<asp:Panel runat="server" Width="300" Height="40" 
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_Horizon" BackColor="White">
				<asp:TextBox ID="txtHorizon" runat="server"></asp:TextBox><asp:Button ID="cmdHorizon"
                    runat="server" Text="Submit" /><br />
				<asp:DropDownList id="cmb_Horizon" tabIndex="8" runat="server" Height="20" Width="153" BackColor="Window"
					Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					Font-Size="8.0" Font-Name="Arial" Visible="True" AutoPostBack="True" ForeColor="WindowText"></asp:DropDownList>
			</asp:Panel>
			<asp:Panel runat="server" Width="300" Height="40" 
				TabIndex="7" Visible="False" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" Enabled="True" ID="frm_GoToYear" BackColor="White">
                <asp:TextBox ID="txtGoToYear" runat="server"></asp:TextBox><asp:Button ID="cmdGoToYear"
                    runat="server" Text="Submit" /></asp:Panel>
	</div>
    
        <center>
    			<table>
			<tr>
			<td style="width: 3px"><asp:listbox id="lstYears"  runat="server"
				Height="426" Width="168" Visible="False" SelectionMode="Multiple"></asp:listbox><asp:label id="lblYears" style="Z-INDEX: 116; LEFT: 408px; POSITION: absolute; TOP: 56px" runat="server"
				Visible="False" Font-Bold="True">Years:</asp:label></td>
            </tr>
            </table>
            </center>
        <center>
		<table>
		<tr>
		<td style="width: 3px"><asp:Button ID="cmdDraw" runat="server" BorderStyle="None" TabIndex="1" Text="Graph" Width="88px" /></td>
        </tr>
        </table> 
        </center>    
		<center>
		<table>
		<tr>
		<td>
		   <%--jam 2025/02/23 New highcharts version libraries container width adjusted from 90% to 100%--%>
           <%-- <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>		--%>
            <div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>
			
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
		</td>
		</tr>
			</table>
			</center>
			
			<script language="javascript">
			{
				if ((document.forms[0].txtGoToYear != null) && (document.forms[0].txtGoToYear.disabled == false))
				{
					document.forms[0].txtGoToYear.focus();
				}
				if ((document.forms[0].txtHorizon != null) && (document.forms[0].txtHorizon.disabled == false))
				{
					document.forms[0].txtHorizon.focus();
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
