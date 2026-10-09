<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_SDGset" Codebehind="frm_SDGset.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>SDG Overview Table</title>
		<%--<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>--%>	
        <!--jam 2024/09/13-->
        <SCRIPT language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript">
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
                        <!--jam 2022/04/15-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="SDG Overview Table" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            
            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
            <cswm:WebMenu ID="wmPopUp" runat="server"></cswm:WebMenu>
            <cswm:WebMenu ID="wmPopUp1" runat="server"></cswm:WebMenu>
            <cswm:WebMenu ID="wmPopUp2" runat="server"></cswm:WebMenu>

            			<asp:label id="lbl_countries" style="Z-INDEX: 100; LEFT: 8px; POSITION: absolute; TOP: 64px"
				tabIndex="8" runat="server" BackColor="Window" Text="Countries or Regions" Font-Name="Arial"
				Font-Size="9.75pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				ForeColor="WindowText" Height="17px" Width="144px" Font-Names="Arial"></asp:label>
			<asp:label id="Label1" 
                style="Z-INDEX: 101; LEFT: 600px; POSITION: absolute; TOP: 64px; width: 850px;" tabIndex="11"
				runat="server" Height="21px" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="8.75pt" Font-Name="Arial"
				
                Text="Click on description to go to graph, or click on scenario values to show over time. Red is used for values that are in the opposite direction of the goal." 
                BackColor="Window" Font-Names="Arial"></asp:label>
		    
            <asp:label id="Lbl_File" 
                style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 93px; right: 991px; width: 191px;" tabIndex="11"
				runat="server" BackColor="Window" Text="Select Reference Scenario:" Font-Name="Arial" 
                Font-Size="9.75pt" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" 
                ForeColor="WindowText" Height="21px" Font-Names="Arial"></asp:label>
            <asp:label id="Lbl_Sce2" 
                style="Z-INDEX: 103; LEFT: 550px; POSITION: absolute; TOP: 93px; right: 991px; width: 191px;" tabIndex="11"
				runat="server" BackColor="Window" Text="Select Intervention Scenario:" Font-Name="Arial" 
                Font-Size="9.75pt" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" 
                ForeColor="WindowText" Height="21px" Font-Names="Arial"></asp:label>
                <asp:dropdownlist id="cmb_Country" style="Z-INDEX: 104; LEFT: 190px; POSITION: absolute; TOP: 64px"
				tabIndex="6" runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="208px"></asp:dropdownlist>
                <asp:dropdownlist id="cboFiles" style="Z-INDEX: 105; LEFT: 190px; POSITION: absolute; TOP: 93px; right: 671px;" tabIndex="10" onChange="showhg();"
				runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="329" AutoPostBack="True"></asp:dropdownlist>
                <asp:dropdownlist id="cboSce2" style="Z-INDEX: 105; LEFT: 750px; POSITION: absolute; TOP: 93px" tabIndex="10" onChange="showhg();"
				runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="329" AutoPostBack="True"></asp:dropdownlist>
                <!-- jam 2024/11/17 Adding Goal Label -->
                <asp:label id="GoalLabel" 
                style="Z-INDEX: 103; LEFT: 1100px; POSITION: absolute; TOP: 93px; right: 1050px; width: 191px;" tabIndex="11"
		        runat="server" BackColor="Window" Text="Goal:" Font-Name="Arial" 
                Font-Size="9.75pt" Font-Bold="True" 
		        Font-Italic="False" Font-Strikeout="False" Font-Underline="False" 
                ForeColor="WindowText" Height="21px" Font-Names="Arial"></asp:label>
            
                <asp:dropdownlist id="cmb_Goal" style="Z-INDEX: 105; LEFT: 1150px; POSITION: absolute; TOP: 93px" tabIndex="10" onChange="showhg();"
                runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="329" AutoPostBack="True"></asp:dropdownlist>
                 <!-- -->
            
                <asp:label id="Lblyear" style="Z-INDEX: 99; LEFT: 410px; POSITION: absolute; TOP: 64px" tabIndex="9"
				runat="server" BackColor="Window" Text="Select Year" Font-Name="Arial" Font-Size="9.75pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px" Width="80px" Font-Names="Arial"></asp:label>
                <asp:dropdownlist id="Cmb_Year" style="Z-INDEX: 107; LEFT: 500px; POSITION: absolute; TOP: 64px" tabIndex="7" onChange="showhg();"
				runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="89" AutoPostBack="True"></asp:dropdownlist>
                
                


<br />
<br />
<br />

        <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px; visibility:hidden;"><img id="loadimg" src= "<%= Page.ResolveUrl("~/Images/squares-circle.gif")%>"  height="93" width="93"><br>Loading resources...</div>

<br />
       <asp:Table ID="grd_TableDisplay" runat="server" Width=95% GridLines=Both Font-Size=Small EnableViewState=true>
            <asp:TableHeaderRow BackColor=Gray BorderColor=White>
                <asp:TableHeaderCell Text="Indicators" Width="60%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="2015" Font-Bold=true Width="10%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Reference Scenario 2030" Font-Bold=true Width="10%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Intervention Scenario 2030" Font-Bold=true Width="10%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Target Value" Font-Bold=true Width="10%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="RowIdx" Font-Bold=true visible="false" Width="0%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="VarName " Font-Bold=true visible="false" Width="0%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Param2 " Font-Bold=true visible="false" Width="0%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Param3" Font-Bold=true visible="false" Width="0%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="FormulaDet" Font-Bold=true visible="false" Width="0%">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="FormulaId" Font-Bold=true visible="false" Width="0%">
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
         </asp:Table>
        
            <input id="txtRow" name="txtRow"  type="hidden" runat="server"  />
            <input id="txtCol" name="txtCol"  type="hidden" runat="server"  />

		</form>
		
	</body>
</HTML>

<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-3138457-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>

<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-3138457-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

    //jrs 2011/05/04
    function cntrylist_click() {
        var doc = document.forms[0];
        var ctry = "Country=" + RegCode[doc.cmb_Country.selectedIndex + 1];
        window.location.search = ctry;
    }

    //jrs 2011/05/05
    function lstGroups_click() {
        var doc = document.forms[0];
        var grp = "Group=" + doc.cmb_Country[doc.cmb_Country.selectedIndex].text;
        //2017/08/31 replace & with %26 for url (Latin American & Caribean)
        grp = grp.replace("&", "%26");
        window.location.search = grp;
    }

    // Point Click Event Handler
    function onPointClick(e, c) {
        // Read point name
        document.getElementById('txtRow').value = c;
        document.getElementById('txtCol').value = e;

        if (e == 2) {
            cswmShow('mnuSDGSetContext2', '', 'below', tempX, tempY, 1);
        } else if (e == 1) {
            cswmShow('mnuSDGSetContext1', '', 'below', tempX, tempY, 1);
        }
        else {
            cswmShow('mnuSDGSetContext', '', 'below', tempX, tempY, 1);
        }
    }

    
</script>

<script language="javascript" type ="text/javascript" >
    function showhg() {
        var doc = document.forms[0];
        document.getElementById('grd_TableDisplay').style.visibility = "hidden";

        var imgurl = document.getElementById('loadimg').src;
        setTimeout(function () {
            document.getElementById('loadimg').src = imgurl;
        }, 1);
        document.getElementById('firstloading').style.visibility = "visible";

        document.body.style.cursor = "wait";
        doc.submit();
    }

</script>

<script language="JavaScript1.2">
<!--

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