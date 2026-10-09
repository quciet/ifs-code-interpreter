<%@ Reference Page="~/ErrorHandler.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.Migrated_frm_MainMenu" Codebehind="frm_MainMenu.aspx.vb" %>


<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head>
		<title>IFs Main Menu</title>
        <%--<script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript"/>--%>
  
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		
        <!--jam 2021/11/27-->
        <%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>

        <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script> 

        <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/highcharts-more.js"></script>
         <script src="Scripts/map.js"></script>
        <script src="Scripts/exporting.js"></script>
        <script src="Maps/country.js"></script>
        <link rel="icon" href="Images/favicon.ico">
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form runat="server" id="mainForm">
        <!--JAM 2013-04-02-->
        <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px; z-index:50;"><img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>
		<!-- jrs 2026/04/21 us flag blue color is #002868, currently using same as graph border -->
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Main Menu" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False">
			</cswm:webmenu>
            <cswm:WebMenu ID="wmPopUp" runat="server"></cswm:WebMenu>

<script language="javascript" type ="text/javascript" >
    var willSubmit = true;
    var count = 1;
    function showHelp() {
        //jrs 09/17/04
        var doc = document.forms[0];
        //jrs 2006/12/21 var hwindow = window.open("./WebHelp/ifshelp.htm", "__webCshStub");			
        //jrs 2009/02/12 var hwindow = window.open("./WebHelp/ifshelp.htm#ifs_help_for_web_version/introduction_to_use_of_ifs___web_version/alternative_modes_of_use_web_version.htm", "__webCshStub");
        var hwindow = window.open("../assets/help/WebHelp/ifshelp.htm#ifs_help_for_web_version/introduction_to_use_of_ifs___web_version/alternative_modes_of_use_web_version.htm", "__webCshStub");
    };

    function showhg() {
        var doc = document.forms[0];
        //jrs 2015/04/09 check the map is there (map not working on mobile devices)
        if (document.getElementById('container') != null) {
            //JAM 2013-04-02
            document.getElementById('container').style.visibility = "hidden";
        }

        var imgurl = document.getElementById('loadimg').src;
        setTimeout(function () {
            document.getElementById('loadimg').src = imgurl;
        }, 1);
        document.getElementById('firstloading').style.visibility = "visible";

        document.body.style.cursor = "wait";
        //doc.cmd_Exit.value = "Please Wait....";
        //doc.cmd_Exit.style.cursor = "wait";

    };

    //jrs 2020/08/28
    function RUsureDEL() {
        if (confirm("Are you sure you want to delete this runfile?")) {
            showhg();
        }
        else {
            return false;
        }
    };

    //	var left = (window.innerwidth *5.70)/100; // (screen.width *5.70)/100;
    //    var top= (window.innerheight *58.37)/100;	// (screen.height  *58.37)/100;	
    //    var hp  = (window.innerheight*54.5)/100; // (screen.height*54.5)/100;
    //    var wp  = (window.innerwidth*94.92)/100;  // (screen.width*94.92)/100;
    
    function hidehg() {

        document.getElementById('firstloading').style.visibility = "hidden";
        document.getElementById('firstloading').style.height = 0;
        document.getElementById('loadimg').src = "";

    };

    //jrs 2022/12/12
    function GBYChangeCheck(BaseYear, FirstYearHistory, LastYearForecast) {
        //Ask for Year to be used
        var sNewYear = "";
        sNewYear = prompt("What year do you want to use as base year?", BaseYear);
        //validation, numeric
        if (isNaN(sNewYear)) {
            alert("Need to use a numeric value");
            txtConfirm.value = 0;
            return false;
        }
        //validation, range
        if (sNewYear < FirstYearHistory || sNewYear > LastYearForecast) {
            alert("Need to use value between " + FirstYearHistory + " and " + LastYearForecast);
            txtConfirm.value = 0;
            return false;
        }

        txtNewBaseYear.value = sNewYear;

        showhg();
        txtConfirm.value = 1;
        return true;
    };

    function checkToMergeMigration() {
        var msg = "File ifsHistSeriesDyadic.db needs to be located in the DATA folder in order to use this option. \n\n Go To Rebuil Base, and check the Bilateral option.";

        var confirmedChanges = confirm(msg);
        //jrs 2026/02/16 these files are no included with installation, no need to download anymore
        //if (confirmedChanges) {
        //    window.open("https://ifs02.du.edu/BilateralInteractionData/");
        //}
        return true;
    };

    function checkToAddMostRecent() {
        var msg = "This process will add a column to all Series of the Historical Data file. Do you wish to continue?";

        var confirmedChanges = confirm(msg);
        if (confirmedChanges) {
            showhg();
            txtConfirm.value = 1;
            return true;
        }
        else {
            txtConfirm.value = 0;
            return false;
        }
        
    };

</script>

            <input id="txtDataHolder" name="txtDataHolder"  type="hidden" runat="server"  />
            <input id="txtGoogleCtry" name="txtGoogleCtry"  type="hidden" runat="server"  />
            <input id="txtPtX" name="txtPtX" type="hidden" /><input id="txtPtY" name="txtPtY" type="hidden" />
            <input id="txtChromeMap" name="txtChromeMap"  type="hidden" runat="server"  />
            <input id="txtNewBaseYear" name="txtNewBaseYear"  type="hidden" runat="server"  />
            <input id="txtConfirm" name="txtConfirm"  type="hidden" runat="server"  />
	
<script language="JavaScript1.2">
//<!--

// Detect if the browser is IE or not.
// If it is not IE, we assume that the browser is NS.
var IE = document.all?true:false;

// If NS -- that is, !IE -- then set up for mouse capture
if (!IE) document.captureEvents(Event.MOUSEMOVE);

// Set-up to use getMouseXY function onMouseMove
document.onmousemove = getMouseXY;

// Temporary variables to hold mouse x-y pos.s
var tempX = 0;
var tempY = 0;

// Main function to retrieve mouse x-y pos.s

function getMouseXY(e) {
  if (IE) { // grab the x-y pos.s if browser is IE
    tempX = event.clientX + document.body.scrollLeft;
    tempY = event.clientY + document.body.scrollTop;
  } else {  // grab the x-y pos.s if browser is NS
    tempX = e.pageX;
    tempY = e.pageY;
  }  
  // catch possible negative values in NS4
  if (tempX < 0){tempX = 0;}
  if (tempY < 0){tempY = 0;}  
  return true;
}

//-->
</script>
    
    			                    
    <div id="container" style="width: 90%; top: 10px; left: 10px; position: relative;"></div>

    <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>

    <div id="MMDesc" style="top: 20px; position: relative;">
            <asp:label id="lbl_scenanal" 
				tabIndex="2" runat="server" BackColor="Window" Font-Name="Arial"
				Font-Size="9.75pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				ForeColor="Black" Text="Scenario Analysis Summary" BorderColor="Transparent" BorderStyle="None" Font-Names="Arial"></asp:label>    
    </div>

            <div id="DivSaveFile" style="position:absolute; width:100%; text-align:center; top:260px; z-index:100;">
                    <asp:panel id="frm_FileName" Width="100%" Height="100%" Runat="server" Visible="False" BackColor="White">
                                <P>&nbsp;
					            <asp:Label id="lbl_FileName" runat="server" Height="22px">File Name (No path, no extension): </asp:Label>&nbsp;&nbsp;&nbsp;
					            <asp:TextBox id="txt_FileName" runat="server" Height="25px" Width="200px"></asp:TextBox></P>
                                <P>&nbsp;
                                <asp:Button id="cmdFileName" runat="server" Height="33" Width="100" Font-Underline="False" Font-Strikeout="False"
					                                Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Names="Arial"
					                                ForeColor="ControlText" BackColor="Control" Text="Save"></asp:Button>&nbsp;&nbsp; 
                                <asp:Button id="cmdCancel" runat="server" Height="33" Width="100" Font-Underline="False" Font-Strikeout="False"
					                                Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Names="Arial"
					                                ForeColor="ControlText" BackColor="Control" Text="Cancel"></asp:Button>
                                <br /><br /><br /><br /><br /><br />
                                </P>
			            </asp:panel>
            </div>
		</form>
	</body>
</HTML>

<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>



