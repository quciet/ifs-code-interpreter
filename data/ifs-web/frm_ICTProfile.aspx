<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ICTProfile" Codebehind="frm_ICTProfile.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_ICTProfile</title>
        <!--jam 2021/12/29-->
		<%--<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">--%>
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
            <asp:table id="WinFormsToWeb_PageBar" runat="server" Height="15px" Width="100%" CellSpacing="0">				
                <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
					<a href="http:\\www.pardee.du.edu"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
						
                        <asp:TableCell Text="Infrastructure Profile" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            
            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 103; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>
            <cswm:WebMenu ID="wmPopUp" runat="server"></cswm:WebMenu>
            <cswm:WebMenu ID="wmPopUp1" runat="server"></cswm:WebMenu>
            <cswm:WebMenu ID="wmPopUp2" runat="server"></cswm:WebMenu>
            <cswm:WebMenu ID="wmPopUp3" runat="server"></cswm:WebMenu>
            <cswm:WebMenu ID="wmPopUp4" runat="server"></cswm:WebMenu>

			<asp:label id="lbl_countries" style="Z-INDEX: 100; LEFT: 8px; POSITION: absolute; TOP: 64px"
				tabIndex="8" runat="server" BackColor="Window" Text="Countries or Regions" Font-Name="Arial"
				Font-Size="9.75pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				ForeColor="WindowText" Height="17px" Width="144px" Font-Names="Arial"></asp:label>
			<asp:label id="Label1" 
                style="Z-INDEX: 101; LEFT: 576px; POSITION: absolute; TOP: 64px; width: 850px;" tabIndex="11"
				runat="server" Height="21px" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="8.75pt" Font-Name="Arial"
				
                Text="Click on variable name to get options for additional information. Click on any row Weighting to change it and on any row Rank to see a full country list." 
                BackColor="Window" Font-Names="Arial"></asp:label>
            <asp:label id="lblIncome" 
                style="Z-INDEX: 103; LEFT: 576px; POSITION: absolute; TOP: 93px; width: 118px;" tabIndex="11"
				runat="server" BackColor="Window" Text="Select File:" Font-Name="Arial" 
                Font-Size="9.75" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" 
                ForeColor="WindowText" Height="21">GDP per Capita</asp:label>
            <asp:TextBox ID="txtIncome" runat="server" style="Z-INDEX: 101; LEFT: 776px; POSITION: absolute; TOP: 93px; "></asp:TextBox>
		    
            <asp:label id="Lbl_File" 
                style="Z-INDEX: 103; LEFT: 8px; POSITION: absolute; TOP: 93px; right: 1154px;" tabIndex="11"
				runat="server" BackColor="Window" Text="Select File:" Font-Name="Arial" 
                Font-Size="9.75" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" 
                ForeColor="WindowText" Height="21" Width="81"></asp:label>
                <asp:dropdownlist id="cmb_Country" style="Z-INDEX: 104; LEFT: 160px; POSITION: absolute; TOP: 64px"
				tabIndex="6" runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="208px"></asp:dropdownlist>
                <asp:dropdownlist id="cboFiles" style="Z-INDEX: 105; LEFT: 160px; POSITION: absolute; TOP: 93px" tabIndex="10" onChange="showhg();"
				runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="329" AutoPostBack="True"></asp:dropdownlist>
                <asp:label id="Lblyear" style="Z-INDEX: 99; LEFT: 384px; POSITION: absolute; TOP: 64px" tabIndex="9"
				runat="server" BackColor="Window" Text="Select Year" Font-Name="Arial" Font-Size="9.75pt" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px" Width="80px" Font-Names="Arial"></asp:label>
                <asp:dropdownlist id="Cmb_Year" style="Z-INDEX: 107; LEFT: 464px; POSITION: absolute; TOP: 64px" tabIndex="7" onChange="showhg();"
				runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="89" AutoPostBack="True"></asp:dropdownlist>
                
                


<br />
<br />
<br />

        <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px; visibility:hidden;"><img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>

            <br />

        <asp:Table ID="grd_TableDisplay" runat="server" Width=100% GridLines=Both Font-Size=Small EnableViewState=true>
            <asp:TableHeaderRow BackColor=Gray BorderColor=White>
                <asp:TableHeaderCell Text=" ">
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Computed Value" Font-Bold=true>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Expected Value Predicted from GDP per Capita at PPPP" Font-Bold=true>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Standard Error (SE) of Estimate" Font-Bold=true>
                </asp:TableHeaderCell>
                <asp:TableHeaderCell Text="Standard Errors of Value from Prediction" Font-Bold=true>
                </asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell Text="Transportation" Font-Bold=true>                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Roads per Carpita (km.)"><a href="frm_ICTProfile.aspx?row=1&col=0">Roads per Carpita (km.)</a>                  
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Paved Roads (% of total)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell Text="Rural Access Index (% of rural population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell Text="Energy" Font-Bold=true>                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Generating Capactity per Capita (kw)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Transmission Loss (%)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell Text="Electricity Access - National (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Electricity Access - Urban (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Electricity Access - Rural (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell Text="Water and Sanitation" Font-Bold=true>                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Access to Water - Safely Managed (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

<%--            <asp:TableRow>
                <asp:TableCell Text="Access to Water - Other Improved (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Access to Water - Unimproved (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>--%>

            <asp:TableRow>
                <asp:TableCell Text="Access to Sanitation - Safely Managed (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

<%--            <asp:TableRow>
                <asp:TableCell Text="Access to Sanitation - Shared (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

             <asp:TableRow>
                <asp:TableCell Text="Access to Sanitation - Unimproved (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>--%>


            <asp:TableRow>
                <asp:TableCell Text="Wastewater Collected (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Wastewater Treated (% of population)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Area Equiped for Irrigation (% of Crop and Grazing land)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="ICT" Font-Bold=true>                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Percentage of networked persons" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Mobile Telephone Subscriptions (per 100 persons)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
               
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Fixed Broadband Subscriptions (per 100 persons)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Mobile Broadband Subscriptions (per 100 persons)" >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Infrastructure Indices" Font-Bold=true>                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Text="Traditional (standard errors, World = 0 in 2010) " >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Modern (standard errors, World = 0 in 2010) " >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Modern (0-100) " >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Text="Overall (standard errors, World = 0 in 2010) " >                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
                <asp:TableCell HorizontalAlign=Right>                    
                </asp:TableCell>
            </asp:TableRow>

            
         </asp:Table>
        <asp:ListBox ID="lstFiles" runat="server" Visible=false SelectionMode="Multiple" 
                Width="520px"></asp:ListBox>
        <asp:ListBox ID="lstCountry" runat="server" Visible=false SelectionMode="Multiple" 
                Width="520px"></asp:ListBox>

 
			<script language="javascript">
			    {
			        var myWidth = 0;
			        var myHeight = 0;
			        
                    if (typeof (window.innerWidth) == 'number') {
			            /*Non-IE*/
			            myWidth = window.innerWidth;
			            myHeight = window.innerHeight;
			        } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
			            /*//IE 6+ in 'standards compliant mode*/
			            myWidth = document.documentElement.clientWidth;
			            myHeight = document.documentElement.clientHeight;
			        } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
			            /*//IE 4 compatible*/
			            myWidth = document.body.clientWidth;
			            myHeight = document.body.clientHeight;
			        }
			        if (myWidth == 0) {
			            myWidth = screen.width;
			            myHeight = screen.height;
			        }

			        var doc = document.forms[0];
			        //pct of screen to be used
			        //myWidth = 0.9 * myWidth;
			        var h = myHeight - 200;


	if ((document.forms[0].cmb_Country != null)&& (document.forms[0].cmb_Country.disabled == false ) ) 
          {
               document.forms[0].cmb_Country.focus();
           }

           if (document.forms[0].lstCountry != null) {
               document.forms[0].lstCountry.style.height = h + 'px';
           }

           if (document.forms[0].lstFiles != null) {
               document.forms[0].lstFiles.style.height = h + 'px';
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
    //jam 2021/12/29
    //var grp = "Group=" + doc.cmb_Country[doc.cmb_Country.selectedIndex].text;

    var grp = doc.cmb_Country[doc.cmb_Country.selectedIndex].text;

   
    //jam 2022/01/03
    //window.location.search=grp;
    window.location.href = grp;

}
			</script>
				
			<input id="txtRow" name="txtRow"  type="hidden" runat="server"  />
            <input id="txtCol" name="txtCol"  type="hidden" runat="server"  />
				
				<br />
            <br />
            <asp:Button ID="cmdSce" runat="server" AccessKey="c" 
                Text="Select and (C)ontinue" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdSceClear" runat="server" AccessKey="d" 
                Text="Clear All and back to (D)efault" Visible="False" />
				
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

// Point Click Event Handler
function onPointClick(e) {
    // Read point name
    document.getElementById('txtRow').value = e;

    if (e == 35) {
        cswmShow('mnuPerfRiskContext1', '', 'below', tempX, tempY, 1);
    }
    else {
        cswmShow('mnuPerfRiskContext', '', 'below', tempX, tempY, 1);
    }
}

function onPointClick2(e, c) {
    // Read point name
    document.getElementById('txtRow').value = e;
    document.getElementById('txtCol').value = c;

    cswmShow('mnuPerfRiskContext2', '', 'below', tempX, tempY, 1);
}

function onPointClick3(e, c) {
    // Read point name
    document.getElementById('txtRow').value = e;
    document.getElementById('txtCol').value = c;

    cswmShow('mnuPerfRiskContext3', '', 'below', tempX, tempY, 1);
}

function onPointClick4(e, c) {
    // Read point name
    document.getElementById('txtRow').value = e;
    document.getElementById('txtCol').value = c;

    cswmShow('mnuPerfRiskContext4', '', 'below', tempX, tempY, 1);
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
