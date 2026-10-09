<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ScatterChooseLabels" Codebehind="frm_ScatterChooseLabels.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>frm_ScatterChooseLabels</title>
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
            <asp:table id="WinFormsToWeb_PageBar" runat="server" Height="15px" Width="100%" CellSpacing="0">				
                <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Select Points and Labels" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            
	<br />	     
    

      <asp:table id="Table2" runat="server" Height="20px" Width="10%" CellSpacing="0" HorizontalAlign="Center">				
                <asp:TableRow ForeColor="White" ID="TableRow2" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell5" Width="60px"><asp:Button ID="cmdSave" runat="server" Text="Save" height="30px" Font-Bold="true"/></asp:TableCell>
					<asp:TableCell  ID="TableCell6" Width="60px"><asp:Button ID="cmdCancel" runat="server" Text="Cancel" height="30px" Font-Bold="true"/></asp:TableCell>
                     
             	</asp:TableRow>
	 </asp:table>
    
       <br />



     <asp:table id="Table1" runat="server" Height="15px" Width="12%" CellSpacing="0" HorizontalAlign="Center">				
                <asp:TableRow ForeColor="White" ID="TableRow1" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="TableCell1" Width="40px"><asp:Button ID="CheckAllLabels" runat="server" Text="Check All Labels" /></asp:TableCell>
					<asp:TableCell  ID="TableCell2" Width="40px"><asp:Button ID="UncheckAllLabels" runat="server" Text="Uncheck All Labels"   /></asp:TableCell>
                     
                    <asp:TableCell  ID="TableCell3" Width="40px"><asp:Button ID="CheckAllPoints" runat="server" Text="Check All Points" /></asp:TableCell>
					<asp:TableCell  ID="TableCell4" Width="40px"><asp:Button ID="UncheckAllPoints" runat="server" Text="Uncheck All Points" /></asp:TableCell>
				</asp:TableRow>
	 </asp:table>
    
       <br />
       <asp:GridView ID="GridView_Regions" runat="server" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
            HorizontalAlign="Center" AutoGenerateColumns="False">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"
                HorizontalAlign="Center" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
            <Columns>
                <asp:BoundField DataField="Countries" HeaderText="Countries" />
                <asp:BoundField DataField="XValue" HeaderText="XValue" />
                <asp:BoundField DataField="YValue" HeaderText="YValue" />
                <asp:TemplateField HeaderText="Labels">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkLabels" checked= "true" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Points">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkPoints" checked = "true" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
       
            </Columns>
        </asp:GridView>    




<br />
<br />
<br />

        <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px; visibility:hidden;"><img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>

            <br />

        
 
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
    var grp="Group="+doc.cmb_Country[doc.cmb_Country.selectedIndex].text;
    window.location.search=grp;
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
