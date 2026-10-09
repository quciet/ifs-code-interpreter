<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_PackagedDisplays" Codebehind="frm_PackagedDisplays.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Geographically-fixed Displays</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
        <link rel="icon" href="http://localhost/ifs/ifsweb/images/favicon.ico">
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="javascript:pageResize();">
		<form id="Form1" method="post" runat="server">
		    <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/03-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Geographically-fixed Displays" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td><cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 999; LEFT: 0px; POSITION: absolute; TOP: 16px"
							runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu></td>
				</tr>
			</table>
			<asp:label id="lblMsg" style="Z-INDEX: 111; LEFT: 32px; POSITION: absolute; TOP: 588px" runat="server"
				Font-Bold="True" Width="424" Height="24px" Visible="False" ForeColor="Red" Font-Size="Large">Label</asp:label>
			<asp:label id="Label1" style="Z-INDEX: 100; LEFT: 220px; POSITION: absolute; TOP: 64px" runat="server"
				Font-Bold="True" Width="448" Font-Names="Arial">Displays:</asp:label><asp:panel id="frm_Horizon" style="Z-INDEX: 113; LEFT: 696px; POSITION: absolute; TOP: 112px"
				tabIndex="3" runat="server" Font-Bold="False" Width="169" Height="172" Visible="False" ForeColor="WindowText" Font-Size="8.0" Enabled="True" Font-Strikeout="False" Font-Italic="False" Font-Name="Arial" Font-Underline="False" BackColor="Window">
Change Horizon Year:<br>
<asp:DropDownList id="cmb_Horizon" style="LEFT: 8px; POSITION: absolute; TOP: 16px" tabIndex="4" runat="server"
					Height="129" Width="153" Font-Size="8.0" ForeColor="WindowText" Visible="True"
					Font-Bold="False" BackColor="Window" Font-Underline="False" Font-Name="Arial"
					Font-Italic="False" Font-Strikeout="False" Enabled="True" AutoPostBack="True"></asp:DropDownList>
					
			</asp:panel><asp:button id="cmdAnnotation" style="Z-INDEX: 112; LEFT: 344px; POSITION: absolute; TOP: 556px"
				tabIndex="4" runat="server" Width="112px" Text="Explain Scenario"></asp:button>
			<asp:listbox id="lstCategories" style="Z-INDEX: 110; LEFT: 8px; POSITION: absolute; TOP: 92px"
				runat="server" AutoPostBack="True" Height="426" Width="190"></asp:listbox>
			<asp:label id="lblFiles" style="Z-INDEX: 101; LEFT: 9px; POSITION: absolute; TOP: 424px" runat="server"
				Width="448" Font-Bold="True" Font-Names="Arial">Scenarios:</asp:label>
				
			<asp:listbox id="lstFiles" style="Z-INDEX: 109; LEFT: 8px; POSITION: absolute; TOP: 444px" runat="server"
				SelectionMode="Multiple" Height="106" Width="456"></asp:listbox>
			<asp:listbox id="lstPacks" style="Z-INDEX: 108; LEFT: 220px; POSITION: absolute; TOP: 92px" runat="server"
				Height="318" Width="456"></asp:listbox><asp:button id="cmdTable" style="Z-INDEX: 107; LEFT: 32px; POSITION: absolute; TOP: 556px" runat="server"
				Width="88px" Text="Table"></asp:button><asp:button id="cmdLine" style="Z-INDEX: 106; LEFT: 136px; POSITION: absolute; TOP: 556px" tabIndex="1"
				runat="server" Width="88px" Text="Line Graph"></asp:button><asp:button id="cmdBar" style="Z-INDEX: 104; LEFT: 240px; POSITION: absolute; TOP: 556px" tabIndex="2"
				runat="server" Width="88px" Text="Bar Graph"></asp:button><asp:button id="cmdContinue" style="Z-INDEX: 102; LEFT: 472px; POSITION: absolute; TOP: 556px"
				tabIndex="3" runat="server" Width="88px" Text="Continue" Visible="False"></asp:button><asp:label id="Label2" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 64px" runat="server"
				Font-Bold="True" Font-Names="Arial">Display Categories:</asp:label>
		<script language="javascript">
                var myWidth = 0, myHeight = 0;

                function pageResize(){
                    if( typeof( window.innerWidth ) == 'number' ) {
                    /*Non-IE*/
                    myWidth = window.innerWidth;
                    myHeight = window.innerHeight;
                    } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
                    /*//IE 6+ in 'standards compliant mode*/
                    myWidth = document.documentElement.clientWidth;
                    myHeight = document.documentElement.clientHeight;
                    } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
                    /*//IE 4 compatible*/
                    myWidth = document.body.clientWidth;
                    myHeight = document.body.clientHeight;
                    }
                   if (myWidth == 0)
                    {
                    myWidth = screen.width;
                    myHeight = screen.height;
                    }

                    var doc = document.forms[0];
                    //pct of screen to be used
                    //myWidth = 0.9 * myWidth;
                    var f2 = 0.9;
                    var f = 1.025;
                    var h = myHeight * f2 - (20 + 64) - (106 + 24 + 20) * f;
                    if (h < 106 + 20)
                    {
                        h = 106 + 20;
                    }
                    doc.lstPacks.style.height = h + 'px';
                    doc.lstCategories.style.height = h + 'px';
                    var T = (92 + h) * f;
                    document.getElementById('lblFiles').style.top = T + 'px';
                    var tt = T+20;
                    doc.lstFiles.style.top = tt + 'px';

                    var t2 = (T + 106 + 20) * f;
                    doc.cmdAnnotation.style.top = t2 + 'px';
                    doc.cmdBar.style.top = t2 + 'px';
                    doc.cmdLine.style.top = t2 + 'px';
                    doc.cmdTable.style.top = t2 + 'px';
                    doc.cmdContinue.style.top = t2 + 'px';

                    ChangeListWidth();
                }
                
                function ChangeListWidth()
                {
                    //Change width of lists
                    var doc = document.forms[0];
                    var w3 = 0;
                    if (doc.lstDim2.style.visibility =="visible") 
                    {
                        w3 = 168;
                    }
                    var w4 = 0;
                    if (doc.lstDim3.style.visibility =="visible") 
                    {
                        w4 = 168;
                    }

                    var f3 = 0.9;
                    var SpaceBtwn = 16;
                    var Disp = myWidth - 9 - 11;
                    var NumbLst = 4;
                    
                    var DispInd = Disp / NumbLst;
                    var dd = DispInd * 2;
                    doc.lstPacks.style.width = dd + 'px';
                    doc.lstFiles.style.width = dd + 'px';
                    if (dd < 281)
                    {
                        document.getElementById('lblInstructions').style.width = Disp + 'px';
                    }
                    else
                    {
                        //original value
                        document.getElementById('lblInstructions').style.width = '500px';
                    }
  
                    var ll = 9 + dd + SpaceBtwn;
                    doc.lstCategories.style.left = ll + 'px';
                    document.getElementById('lblCategories').style.left = ll + 'px';
                    doc.lstRunHorizon.style.left = ll + 'px';
                    document.getElementById('lblRunHorizon').style.left = ll + 'px';
                    
                    dd = DispInd - SpaceBtwn;
                    doc.lstCategories.style.width = dd + 'px';
                    doc.lstRunHorizon.style.width = dd + 'px';
      
                }		
		</script>
		</form>
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