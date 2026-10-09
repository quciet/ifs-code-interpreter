<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_TableFunc" Codebehind="frm_TablFunc.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Change Bi-Variate Functions</title>
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
        

       



	    <style type="text/css">
            #Table2
            {
                width: 926px;
            }
            .style1
            {
                width: 605px;
            }
            .style2
            {
                width: 225px;
            }
        </style>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="frm_TablFunc" method="post" runat="server">
            <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Change Bi-Variate Functions" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
             		
            <TABLE id="Table1" cellSpacing="1" cellPadding="1" border="1">
				<TR width="800">
					<TD>
						<asp:label id="Label2" runat="server">Filter Functions (press enter):</asp:label>&nbsp;
                        <asp:TextBox ID="txtFilter" runat="server" Width="436px"></asp:TextBox><br />
                        <asp:label id="Label1" runat="server">Functions (click to see/edit):</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="chk_ExtendList" runat="server" AutoPostBack="True" Width="88px" Text="Extend List"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:checkbox id="chkOrdByTime" runat="server" AutoPostBack="True" 
                            Width="173px" Text="Order by Last Change"></asp:checkbox><br>
						<asp:listbox id="Lst_Names" runat="server" Width="688px" AutoPostBack="True" Height="350px"></asp:listbox></TD>
                        <td valign=top class="style2">
                            <br />
                            <asp:checkbox id="chkRun" runat="server" AutoPostBack="True" Width="132px" 
                                Text="Used in Run" Checked=true></asp:checkbox><br>
                            <asp:checkbox id="chkRunHis" runat="server" AutoPostBack="True" Width="169px" 
                                Text="Used in Historical Run" Checked=true></asp:checkbox><br>
                            <asp:checkbox id="chkPre" runat="server" AutoPostBack="True" Width="159px" 
                                Text="Used in Preprocessor" Checked=true></asp:checkbox><br>
                            <asp:checkbox id="chkPreHis" runat="server" AutoPostBack="True" Width="224px" 
                                Text="Used in Historical Preprocessor" Checked=true></asp:checkbox><br>
                            <asp:checkbox id="chkNotUsed" runat="server" AutoPostBack="True" Width="132px" 
                                Text="Not Used" Checked=true></asp:checkbox><br>
                            <asp:checkbox id="chkAll" runat="server" AutoPostBack="True" Width="132px" 
                                Text="All" Checked=true Enabled=false></asp:checkbox><br>
                        </td>
				</TR>
             </table>
             
             <table id="Table2" cellSpacing="1" cellPadding="1" border="1">
				<TR>
					<TD style="TEXT-ALIGN: center">
						<asp:panel id="Panel1" runat="server" Height="208px" Width="224px" BorderStyle="Solid" BackColor="White">
							<P>Table Function Points</P>
							<P>&nbsp;
								<asp:Button id="but_Alter" runat="server" Text="Alter" Width="60px"></asp:Button>&nbsp;
								<asp:Button id="but_add" runat="server" Text="Add" Width="60px"></asp:Button>&nbsp;
								<asp:Button id="but_delete" runat="server" Text="Delete" Width="60px"></asp:Button></P>
							<P>&nbsp;X-Axis
								<asp:TextBox id="txt_x" runat="server" Width="84px" BorderStyle="Solid" BorderColor="Black"></asp:TextBox></P>
							<P>&nbsp;Y-Axis
								<asp:TextBox id="txt_y" runat="server" Width="84px" BorderStyle="Solid" BorderColor="Black"></asp:TextBox></P>
							<P>&nbsp;
								<asp:Button id="but_Previous" runat="server" Text="Previous" Width="60px"></asp:Button>&nbsp;
								<asp:Button id="but_next" runat="server" Text="Next" Width="60px"></asp:Button></P>
						</asp:panel>
						<P>
							<asp:button id="cmd_Fit" runat="server" Width="120px" Text="Analytic Function"></asp:button>&nbsp;
							<asp:button id="but_reset" runat="server" Width="120px" Text="Reverse Changes"></asp:button></P>
						<P>
							<asp:button id="cmd_Exit2" runat="server" Height="24px" Width="120px" Text="Exit"></asp:button>&nbsp;
							<input id="btnHelp" onclick="javascript:ShowHelp();"
                                type="button" value="Help" style="width: 120px; height: 24px" />
							</P>
                            <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16022);-->
					</TD>
					<TD style="TEXT-ALIGN: center" class="style1"  >
						<asp:Panel id="pnl_graph" runat="server" Height="300px" Width="600px" BorderStyle="Solid" BorderColor="Black">
							<%--<div id="Div1" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>       
                <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>--%>
                       <%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div> --%>
                        <div id="container" style="height: 300px; width: 600px; top: 1px; left: 1px; position: relative;"></div>
                <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>
           



						</asp:Panel>
                        <asp:Label ID="lblRSqr" runat="server" Text="R^2="></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblSE" runat="server" Text="SE ="></asp:Label>
                        <br />
                        </TD>
				</TR>
			</TABLE>
            

			<script language="javascript">
			    {
			        if ((document.forms[0].txtFilter != null) && (document.forms[0].txtFilter.disabled == false)) {
			            document.forms[0].txtFilter.focus();
			        }
			        //jrs 2012/01/16
			        function clickButton(e, buttonid) {
			            var evt = e ? e : window.event;
			            var bt = document.getElementById(buttonid);
			            if (bt) {
			                if (evt.keyCode == 13) {
			                    bt.click();
			                    return false;
			                }
			            }
			        }
			    }
			</script>

			</form>

          <%--  <asp:Panel id="Panel2" runat="server" Height="300px" Width="600px" BorderStyle="Solid" BorderColor="Black">
							 <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>	
						</asp:Panel>--%>

           <%--       <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>       
                <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>
           --%>
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