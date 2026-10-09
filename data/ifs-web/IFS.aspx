<%@ Reference Page="~/frm_MainMenu.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.Migrated_frm_IFS" Codebehind="IFS.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!-- jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<META HTTP-EQUIV="CACHE-CONTROL" CONTENT="NO-CACHE">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="Expires" CONTENT="-1">
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0" onLoad="if ('Navigator' == navigator.appName) document.forms[0].reset();">
		<form runat="server">
		<input id="txtResValueHolder" name="txtResValueHolder"  type="hidden" />
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Welcome to International Futures" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table><asp:button id="cmd_Exit" style="LEFT: 352px; POSITION: absolute; TOP: 384px; TEXT-ALIGN: center; Width:153"
				accessKey="C" tabIndex="1" runat="server" Height="33" BackColor="Control" Text="(C)ontinue" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				CausesValidation="True" Enabled="True" ForeColor="ControlText"></asp:button><asp:image id="WorldMap" style="LEFT: 64px; POSITION: absolute; TOP: 55px" tabIndex="3" runat="server"
				Width="544px" Height="304px" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Enabled="True"
				ForeColor="WindowText" Visible="True" BorderStyle="None" ImageUrl="WORLD_MA.BMP"></asp:image><asp:label id="lbl_IFsName" style="LEFT: 352px; POSITION: absolute; TOP: 440px; TEXT-ALIGN: left"
				tabIndex="4" runat="server" Width="265" Height="73" BackColor="Window" Text="International Futures" Font-Name="Arial" Font-Size="9.75pt" Font-Bold="True" Enabled="True" ForeColor="WindowText"
				Visible="True" BorderStyle="None" Font-Names="Arial">International Futures</asp:label><asp:label id="lbl_IFSExplain" style="LEFT: 64px; POSITION: absolute; TOP: 440px; TEXT-ALIGN: center"
				tabIndex="2" runat="server" Width="265" Height="121" BackColor="Window" Text="INTERNATIONAL FUTURES" Font-Name="Arial" Font-Size="9.75pt" Font-Bold="True" Enabled="True" ForeColor="WindowText"
				Visible="True" BorderStyle="None" Font-Names="Arial">INTERNATIONAL FUTURES</asp:label>
			<script language="javascript">
{
	if ((document.forms[0].cmd_Exit.disabled == false))
		{
			document.forms[0].cmd_Exit.focus();
		}
				
}       

            //jrs 2008/07/28
            //Try to find size of browser window, and not use screen
            //var hp  = (screen.height*54.5)/100;
            //var wp  = (screen.width*94.92)/100;
            //document.getElementById('txtResValueHolder').value=wp+","+hp;
              var myWidth = 0, myHeight = 0;
              if( typeof( window.innerWidth ) == 'number' ) {
                //Non-IE
                myWidth = window.innerWidth;
                myHeight = window.innerHeight;
              } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
                //IE 6+ in 'standards compliant mode'
                myWidth = document.documentElement.clientWidth;
                myHeight = document.documentElement.clientHeight;
              } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
                //IE 4 compatible
                myWidth = document.body.clientWidth;
                myHeight = document.body.clientHeight;
              }
              //jrs 2011/03/22
              if (myWidth == 0)
              {
                myWidth = screen.width;
                myHeight = screen.height;              
              }
                            
              myWidth = myWidth * 0.9492;
              myHeight = myHeight * 0.545;
              //window.alert( 'Width = ' + myWidth );
              //window.alert( 'Height = ' + myHeight );
              document.getElementById('txtResValueHolder').value=myWidth+","+myHeight;

</script>
			<script language="javascript">
			function showhg() {
			var doc = document.forms[0];

			//'mti WebForm Changes to be taken by Jose - take showhg function
			//var myForm = document.formName;
			//myForm.cmd_Exit.style.visibility="hidden";
			//alert("The Form has been sent please wait");
			
			document.body.style.cursor = "wait";
			doc.cmd_Exit.value = "Please Wait....";
			doc.cmd_Exit.style.cursor = "wait";
			//cmd_Exit.setAttribute("value","Please Wait...");
			
			
			//document.all('cmd_Exit').enabled = false;
			//document.body.style.cursor = "default";
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


