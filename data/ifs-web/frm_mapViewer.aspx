<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_mapViewer" Codebehind="frm_mapViewer.aspx.vb" %>

<%@ Register Assembly="Coalesys.WebMenu" Namespace="Coalesys.WebMenu" TagPrefix="cswm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Display Map</title>
		<script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		    
		</script>	
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
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
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form id="Form1" runat="server">
            <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Display Map" ID="TitleCell"></asp:TableCell>	
				</asp:TableRow>
			</asp:table>
                   
                        <div id='map_canvas'></div>
            <input id="txtPtX"
                name="txtPtX" type="hidden" /><input id="txtPtY" name="txtPtY" type="hidden" />

            <input id="txtGoogleCtry" name="txtGoogleCtry"  type="hidden" runat="server"  />
    
            <cswm:WebMenu ID="wmPopUp" runat="server"></cswm:WebMenu>
            <asp:HiddenField ID="txtDataHolder" runat="server" Value="dontshowmenu" />
<script language="JavaScript"  type="text/javascript">
    var willSubmit = false;
    var url=document.getElementById('txtDataHolder').value;
	var pos = url.indexOf("yes");	   
	var tmpval;
	tmpval=document.getElementById('txtDataHolder').value;
  if (pos >-1)
    {
    
     
        willSubmit =true;
     
        var cname;
         
        url= url.substring(pos+1,url.length);
        pos = url.indexOf("=");

        var pos2 = url.indexOf("&x");
        var pos3 = url.indexOf("&y");

        
        cname = url.substring(pos+1, pos2);
        var xx = url.substring(pos2+3, pos3);
        var yy = url.substring(pos3+3, url.length);
        
        tmpval=cname;        
        
        document.getElementById('txtPtX').value=xx;        
        document.getElementById('txtPtY').value=yy;  
       // alert(cname + " " +xx + " "+ yy);      
        willSubmit=false; 
     }   
     
     
    else
    {
     document.getElementById('txtDataHolder').value="dontshowmenu" ;
    
    }
   
    
   
  		    function CheckDefaultYear()
            {
                
                var val=document.Form1.elements["hdfld"].value;
                
                if (val=="notassigned"){
                  
                  return true;
                  }
                  
                var one=val.substring(0,val.indexOf('|'),val);
                var two=val.substring(val.indexOf('|')+1,val.length,val);
             
               
                if (two < one)
                  return  true;
                else{
                 
                  window.alert('The Default Forecasting Is Set To '+one);        
                  return false;
                  }
            }
 
  	
	function ShowContextMenu()	
				{
	             
	        
	      		if(!willSubmit)
	      		 {
				   if (tmpval!='dontshowmenu' && tmpval!='')
				   {
				      if( count  > 1 )
				      {
				        document.getElementById('txtDataHolder').value="manual"
				        var url=window.location.href ;

                       var pos = url.indexOf("?");
   
                       if (pos >-1)
                         {
                            url= url.substring(0,pos);
                           
                            window.location.href = url;
                         }
                    
				        window.location.href = url;
				       } 
				      else{ 
				       count = count +1;		    
				        cswmHideNow();
						cswmShow('cntyInfoGroup', '', 'below', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
					 }
                   }
                 }
                }


				


	 if (tmpval!='dontshowmenu' && tmpval!='')
	    {
            var x = parseInt(document.getElementById('txtPtX').value) + parseInt(document.getElementById('imgMap').offsetLeft);	        
            var y = parseInt(document.getElementById('txtPtY').value) + parseInt(document.getElementById('imgMap').offsetTop);	        
         //   alert(x + " " + y);
            tmpval=tmpval.replace(/[\%20|\+]/g," ");
            
            document.getElementById('txtDataHolder').value="dontshowmenu";
            //KIA 06/03/2009 document.getElementById('cswmItemcntyInfoGroup_0').innerText=document.getElementById('cswmItemcntyInfoGroup_0').innerText + " "+tmpval; 
            document.getElementById('cswmItemcntyInfoGroup_0').innerHTML=document.getElementById('cswmItemcntyInfoGroup_0').innerHTML + " "+tmpval; 
	        cswmShow('cntyInfoGroup', '', 'below', x, y , 1);
	        
	    }           
	    
	   function modalWinImage() {
            
            myWidth = (document.body.clientWidth/2);
            myHeight = (document.body.clientHeight/2);
  
            //jrs 2011/06/24 
            tmpval=countryCode;
            countryCode ='';
            window.open('frm_MapImage.aspx?cname=' + tmpval,'frmimage','height=400,left='+myWidth +',top=' + myHeight+',width=600,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no ,modal=yes');
            
         }  
       function modalWinData()
       {
//            if (window.showModalDialog) {
//            window.showModalDialog("frm_DataSeries.aspx?cname=" + tmpval);
//            alert('')
//            } 
//            else {
                
                myWidth = (document.body.clientWidth/2) -200;
                myHeight = (document.body.clientHeight/2) -300;
            //jrs 2011/06/27 
            tmpval=countryCode;
            countryCode ='';
            window.open('frm_DataSeries.aspx?cname=' + tmpval,'frmdataseries','height=600,left='+myWidth +',top=' + myHeight+',width=400,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes');
            //}
       
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

<div>
       <script type="text/javascript" language="javascript">
        // Point Click Event Handler
			function onPointClick(e) {
					// Read point name
					//name=e.data.Name;
			        document.getElementById('txtGoogleCtry').value=e.data.Name;
			        countryCode = e.data.Name;

					// Read point value
					//value=e.data.YValue;

					// Read custom point attribute
					//attribute = e.data.Attributes['REGION_NAME'];

					// Write the info to the page
					//info = 'Point Name: ' + name + ', Value: ' + value + '<br>' + 'Full name: ' + attribute;

					//document.getElementById('pointClickLog').innerHTML = info;
                    cswmShow('cntyInfoGroup', '', 'below', tempX, tempY, 1);
					}

        </script>
 </div>			
 
  <div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px;"><img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>

  <div id="container" style="width: 90%; top: 10px; left: 10px; position: relative;"></div>

    <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>

             <table style="width: 100%">
                <tr>
                    <td colspan=4 style="width: 50%">
                        <asp:HiddenField ID="hdfld" runat="server" Visible="True" Value="notassigned" />
                        
                        <strong><br />&nbsp; &nbsp; &nbsp;After making changes in fields below, touch the Refresh button to repaint the 
									map.</strong></td>
                    <td colspan=4 style="width: 50%">
                        <br />
                        <asp:button id="cmdExit" runat="server" Text="Continue" Width="120px" Height="23px"></asp:button>&nbsp;<asp:Button id="cmdRefresh" runat="server" Text="Refresh Map" Width="120px" Height="23px"></asp:Button>&nbsp;<asp:Button id="cmdDataInfo" runat="server" Text="Data Information" Width="120px" Height="23px"></asp:Button><asp:Button id="cmdCategories" runat="server" Text="Define Categories" Width="120px" Height="23px"></asp:Button>
                        <input id="btnHelp" onclick="javascript:ShowHelp();"
                                type="button" value="Help"  style="width: 88px; height:23px"/>&nbsp;</td>
                                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16032);-->
                </tr>
                </table>
                
            <table style="width: 100%">
				<TR>
					<TD style="WIDTH: 9%; HEIGHT: 20px">
						<asp:label id="lblDataTable" runat="server">Data Table:</asp:label></TD>
					<TD style="WIDTH: 15%; HEIGHT: 20px" align="left"><asp:dropdownlist id="drpDataTable" runat="server" Width="156px" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD style="WIDTH: 10%; HEIGHT: 20px">
						<asp:label id="lblDataField" runat="server">Data Field:</asp:label></TD>
					<TD style="WIDTH: 17%; HEIGHT: 20px">
						<asp:dropdownlist id="drpDataField" runat="server" Width="608px" AutoPostBack="True"></asp:dropdownlist></TD>
					<TD style="WIDTH: 17%; HEIGHT: 20px">
                        &nbsp;&nbsp;</TD>
					<TD style="WIDTH: 6%; HEIGHT: 20px">
                        &nbsp;</TD>
					<TD style="WIDTH: 6%; HEIGHT: 20px">
					</TD>
					<TD style="WIDTH: 20%; HEIGHT: 20px"></TD>
				</TR>
				<TR>
					<TD style="WIDTH: 9%; HEIGHT: 20px"><asp:label id="lblDimension" runat="server" Visible="False">Dimension of Variable:</asp:label></TD>
					<TD style="WIDTH: 65%; HEIGHT: 20px" colspan="5"><asp:dropdownlist id="drpDimension" runat="server" Width="401px" Visible="False"></asp:dropdownlist>&nbsp; Display Type:
					<asp:dropdownlist id="drpDisplayType" runat="server" Width="145" Visible=true>
							<asp:ListItem Value="Equal Interval" Selected="True">Equal Interval</asp:ListItem>
							<asp:ListItem Value="Equal Count">Equal Count</asp:ListItem>
					</asp:dropdownlist>
					Number of Categories:
					<asp:dropdownlist id="drpNumberOfClasses" runat="server" Visible=true>
							<asp:ListItem Value="2">2</asp:ListItem>
							<asp:ListItem Value="3">3</asp:ListItem>
							<asp:ListItem Value="4">4</asp:ListItem>
							<asp:ListItem Value="5" Selected="True">5</asp:ListItem>
							<asp:ListItem Value="6">6</asp:ListItem>
							<asp:ListItem Value="7">7</asp:ListItem>
						</asp:dropdownlist>
                        &nbsp; &nbsp;
						Color:&nbsp;<asp:dropdownlist id="drpColor" runat="server">
							<asp:ListItem Value="4" Selected="True">Sandy Brown</asp:ListItem>
							<asp:ListItem Value="1">Green</asp:ListItem>
							<asp:ListItem Value="2">Blue</asp:ListItem>
							<asp:ListItem Value="0">Red</asp:ListItem>
							<asp:ListItem Value="3">Gray</asp:ListItem>
						</asp:dropdownlist>
						&nbsp; &nbsp; &nbsp;
                    </TD>
					<TD style="WIDTH: 6%; HEIGHT: 20px"><asp:label id="lblWave" runat="server" Visible="False">Survey Wave:</asp:label></TD>
					<TD style="WIDTH: 20%; HEIGHT: 20px"><asp:dropdownlist id="drpWave" runat="server" Visible="False"></asp:dropdownlist></TD>
				</TR>
				<tr>
					<TD style="WIDTH: 24%; HEIGHT: 20px" colspan="2">
					&nbsp; &nbsp; &nbsp;Projection:
                        <asp:dropdownlist id="drpProjection" runat="server">
							<asp:ListItem Value="Behrmann">Behrmann</asp:ListItem>
							<asp:ListItem Value="Eckert">Eckert</asp:ListItem>
                            <asp:ListItem Value="Equicylinder">Equicylinder</asp:ListItem>
							<asp:ListItem Value="Mercator">Mercator</asp:ListItem>
							<asp:ListItem Value="PlateCaree" Selected="True">PlateCaree</asp:ListItem>
							<asp:ListItem Value="Robinson">Robinson</asp:ListItem>
							<asp:ListItem Value="Sinusoidal">Sinusoidal</asp:ListItem>
						</asp:dropdownlist></TD>
					<TD style="WIDTH: 24%; HEIGHT: 20px" colspan="2">
						<asp:Label id="lblYear" runat="server" Visible="False">Year = 2000</asp:Label>
					</TD>
					<TD style="WIDTH: 52%; HEIGHT: 20px" colspan="4">
						<asp:Label id="lblVariable" runat="server" Visible="False">Variable:</asp:Label>
					</TD>
				</tr>
				<tr>
					<TD style="WIDTH: 100%; HEIGHT: 20px" colspan="8" align="center">
						<asp:Button id="cmdNext" runat="server" Text="Adv Years" Visible="False" OnClientClick="javascript:return CheckDefaultYear();"></asp:Button>&nbsp;&nbsp;&nbsp;
						<asp:Button id="cmdPrev" runat="server" Text="Reg Years" Visible="False"></asp:Button>&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="cmdFirst" runat="server" Text="First Year" Visible="False"></asp:Button>&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="cmdLast" runat="server" Text="Last Year" Visible="False" OnClientClick="javascript:return CheckDefaultYear();"></asp:Button>
					</TD>
				</tr>
			</TABLE>

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
