<%@ Reference Page="~/Index.aspx" %>
<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ScenarioCreate" Codebehind="frm_ScenarioCreate.aspx.vb" EnableEventValidation="false"%>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls,Version=1.0.2.226,Culture=neutral,PublicKeyToken=31BF3856AD364E35" %>
<%@ Register TagPrefix="cc2" Namespace="CustomDropDownList.Thycotic.Web.UI.WebControls" Assembly="CustomDropDownList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb Scenario Create</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">

		</SCRIPT>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        <script src="Scripts/highcharts.js"></script>
		<!--mti start slider  -->
		<STYLE>#slidercasing { BORDER-RIGHT: #000000 2px solid; BORDER-TOP: #000000 2px solid; Z-INDEX: 4; LEFT: 300px; BORDER-LEFT: #000000 2px solid; WIDTH: 75px; BORDER-BOTTOM: #000000 2px solid; POSITION: absolute; TOP: 20px; HEIGHT: 350px; BACKGROUND-COLOR: #ffffff }
	#slidertrack { BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; Z-INDEX: 5; LEFT: 135px; BORDER-LEFT: #000000 1px solid; WIDTH: 5px; BORDER-BOTTOM: #000000 1px solid; POSITION: absolute; TOP: 45px; HEIGHT: 300px; BACKGROUND-COLOR: #ffffff }
	#sliderbar { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 0px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 0px; Z-INDEX: 6; LEFT: 113px; PADDING-BOTTOM: 0px; BORDER-LEFT: #000000 1px solid; WIDTH: 50px; CURSOR: pointer; PADDING-TOP: 0px; BORDER-BOTTOM: #000000 1px solid; POSITION: absolute; HEIGHT: 20px; BACKGROUND-COLOR: #dddddd }
		</STYLE>
		<!--mti End Slider  -->
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form title="IFsWeb" runat="server" onsubmit="return validateForm();">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>					
					<asp:TableCell Text="Scenario Development and Change" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="wmMain" style="Z-INDEX: 108; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				Width="16px" Height="16px" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
			<cswm:webmenu id="wmContext" runat="server" Width="16px" Height="16px" MenuBar-AbsoluteDockEnabled="False"
				MenuBar-AbsoluteDockPaddingAdjust="False" MenuBar-DisplayMode="None"></cswm:webmenu>
			<table>
				<tr valign=top>
					<td width=40%>
						<iewc:treeview id="treeScenario" tabIndex="1" runat="server" Font-Names="Arial"
							Font-Size="9" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
							AutoPostBack="True" BorderStyle= "None" ShowLines="False" Enabled="True"></iewc:treeview>
					</td>
					<td width=60%>
			            <asp:panel id="Frm_nonscenario" style="Z-INDEX: 103;"
				            tabIndex="20" runat="server" Width="533" Height="118" BackColor="Window" Visible="False"
				            ForeColor="ControlText" Font-Names="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False"
				            Font-Strikeout="False" Font-Underline="False">
				            <asp:ListBox id="lst_nonscenario" tabIndex="21" runat="server" Height="74" Width="515" AutoPostBack="True"
					            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					            Font-Names="Arial" ForeColor="WindowText" BackColor="Window"></asp:ListBox>
			            </asp:panel>
			            <asp:label id="ContextMenu1" runat="server" BackColor="Red"></asp:label>
			            <asp:label id="lbl_hint" style="Z-INDEX: 101; "
				        tabIndex="11" runat="server" BackColor="Window" ForeColor="ControlText" Font-Names="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False"
				        Font-Strikeout="False" Font-Underline="False"></asp:label>
                        
                        <asp:panel id="frm_varname" style="Z-INDEX: 107; "
				        tabIndex="2" runat="server" Visible="False" ForeColor="ControlText" Font-Names="Arial"
				        Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False">
				            <asp:ListBox id="lst_varname" tabIndex="3" runat="server" Height="158" Width="400" Font-Size="8pt"
					            Font-Names="Arial" ForeColor="WindowText" BackColor="Window"></asp:ListBox>
	            		</asp:panel>
	            		<asp:panel id="frm_dim1" style="Z-INDEX: 113; "
                        tabIndex="4" runat="server" Width="177px" Height="375px" BackColor="White" Visible="False" ForeColor="WindowText" Font-Name="Arial"
                        Font-Size="8.25pt" Font-Bold="True" Font-Names="Arial" BorderStyle="Inset">Please pick a dimension from the dropdown: <BR><BR>
                            <cc2:customdropdownlist id="cmb_DIM1" tabIndex="5" runat="server" Height="17" Width="153" AutoPostBack="True"
					            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
					            Font-Size="8.0" Font-Names="Arial" ForeColor="WindowText" BackColor="Window"></cc2:customdropdownlist>
                            <asp:listbox id="lstDIM1" runat="server" Height="275px" Width="153px" AutoPostBack="True"></asp:listbox>
                                <br /><br />
                                <asp:Label ID="lblTot2" runat="server"
                                Text="Total is used to apply same value to all dimensions" Visible=false></asp:Label>
                        </asp:panel>
                        
                        <asp:placeholder id="frm_options" runat="server" Visible="False">
				            <asp:RadioButton id="_Opt_highlow_0" tabIndex="9" runat="server" Height="22" Width="49" AutoPostBack="True"
					            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					            Font-Names="Arial" ForeColor="ControlText" BackColor="Window" Text="High" GroupName="frm_options"></asp:RadioButton>
				            <asp:RadioButton id="_Opt_highlow_1" tabIndex="8" runat="server" Height="22" Width="49" AutoPostBack="True"
					            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					            Font-Names="Arial" ForeColor="ControlText" BackColor="Window" Text="Low" GroupName="frm_options"></asp:RadioButton>
				            <asp:RadioButton id="_Opt_highlow_2" tabIndex="7" runat="server" Height="22" Width="59" AutoPostBack="True"
					            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					            Font-Names="Arial" ForeColor="ControlText" BackColor="Window" Text="Medium" GroupName="frm_options"></asp:RadioButton>
        			    </asp:placeholder>
        			    <asp:placeholder id="frm_time"  runat="server" >
				            <asp:DropDownList id="cmbShiftYears" tabIndex="17" runat="server" Height="22" Width="38" AutoPostBack="True"
					            Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					            Font-Names="Arial" ForeColor="WindowText" BackColor="Window"></asp:DropDownList>
				            <asp:Label id="lblShiftYears" tabIndex="18" runat="server" Height="27" Width="39" Font-Underline="False"
					            Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Names="Arial"
					            ForeColor="ControlText" Text="Shift Years:"></asp:Label>
			            </asp:placeholder>
                        
                        <asp:placeholder id="frm_graph" Runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <div id="container"></div>
            				            <asp:Label ID="lblJS" runat="server"></asp:Label>                                                            
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;
                                    </td>
                                    <td>
					                    <asp:panel id="pnlSlider" style="Z-INDEX: 114; "
	                                        runat="server" Width="64px" Height="273px" BorderStyle="Groove">
	                                        <asp:TextBox id="txtSliderVal" runat="server" Width="54px"></asp:TextBox>
	                                        <DIV onmouseup="captureStart('sliderbar',1, 0,165)" onmousedown="captureStart('sliderbar',0, 0,165)"
		                                        id="sliderbar" style="LEFT: 6px; POSITION: relative; TOP: 165px"></DIV>
	                                        <DIV id="slidertrack" style="Z-INDEX: 118; LEFT: 26px; WIDTH: 8px; POSITION: relative; TOP: 0px; HEIGHT: 165px"></DIV>
	                                        <BR>
	                                        <asp:Button id="btnSlider" style="Z-INDEX: 117; LEFT: 6px; POSITION: relative; TOP: 0px" runat="server"
		                                        Height="21px" Width="48px" Text="Apply"></asp:Button>
                                        </asp:panel>			            
                                    </td>
                                </tr>
                            </table>
			            </asp:placeholder>  
            			          						            
			            <asp:panel id="Frm_Custom" style="Z-INDEX: 105; "
				        tabIndex="14" runat="server" ForeColor="ControlText" Font-Names="Arial" Font-Size="8.25"
				        Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False">
				            <asp:Button id="cmdCustom" runat="server" Height="33" Width="212" Font-Underline="False" Font-Strikeout="False"
					            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Names="Arial" ForeColor="ControlText"
					            BackColor="Control" Text="Fully Customize"></asp:Button>
                            <br />
				            <asp:Label id="lblFullyCustomized" tabIndex="19" runat="server" Height="15px" Width="66px" Font-Underline="False"
					            Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8pt" Font-Names="Arial"
					            ForeColor="ControlText" BackColor="Window" Text="Fully" ></asp:Label>
                            <br />
				            <asp:Label id="lblCustomized" tabIndex="15" runat="server" Height="15px" Width="54px" Font-Underline="False"
					            Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8pt" Font-Names="Arial"
					            ForeColor="ControlText" BackColor="Window" Text="Customized" ></asp:Label>
    			        </asp:panel>
						<asp:panel id="frm_VarDesc" style="Z-INDEX: 109; "
        				runat="server" Width="300px" Height="62px" Visible="False">
				            <P>
					            <asp:Label id="lblDesc" runat="server" Height="21px" Width="300px"></asp:Label>
			            </asp:panel>
        			    <asp:panel id="pnlMaxmin" style="Z-INDEX: 110; VISIBILITY: hidden; "
        				runat="server" Width="153px" Height="62px" Visible="False">
				            <P>&nbsp;
					            <asp:Label id="lblMax" runat="server" Height="21px" Width="62px">Maximum</asp:Label>&nbsp;
					            <asp:TextBox id="txtMax" runat="server" Height="31px" Width="56px"></asp:TextBox></P>
				            <P>&nbsp;
					            <asp:Label id="lblMin" runat="server" Height="22px" Width="69px">Minimum</asp:Label>
					            <asp:TextBox id="txtMin" runat="server" Height="25px" Width="56px"></asp:TextBox></P>
			            </asp:panel>
                        <asp:panel id="frm_FileName" style="Z-INDEX: 200; "
				            Width="456px" Height="278px" Runat="server" Visible="False" BackColor="White">
                                <P>&nbsp;
					            <asp:Label id="lbl_FileName" runat="server" Height="22px">File Name: </asp:Label>&nbsp;&nbsp;&nbsp;
					            <asp:TextBox id="txt_FileName" runat="server" Height="25px" Width="200px"></asp:TextBox></P>
                                <P>&nbsp;
					            <asp:Label id="lbl_DirName" runat="server" Height="22px">Folder Name: </asp:Label>
					            <asp:TextBox id="txt_DirName" runat="server" Height="25px" Width="200px"></asp:TextBox>
								<div id="divSaveInstructions" runat="server">
								<BR>
					            (e.g. your ID)<BR>
					            <BR>
					            Files are stored for 1 month after being saved, 
                                and can be retrieved under Add Scenario Component/User Defined Sceanrios/Your ID,
                                or under Open/Other/User Defined Scenarios/Your ID.
								</div>
								</P>
                                <p>
                                <asp:Button id="cmdFileName" runat="server" Height="33" Width="100" Font-Underline="False" Font-Strikeout="False"
					                                Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Names="Arial"
					                                ForeColor="ControlText" BackColor="Control" Text="Save"></asp:Button>&nbsp;&nbsp; 
                                <asp:Button id="cmdCancel" runat="server" Height="33" Width="100" Font-Underline="False" Font-Strikeout="False"
					                                Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Names="Arial"
					                                ForeColor="ControlText" BackColor="Control" Text="Cancel"></asp:Button>
                                </p>
			            </asp:panel>
					</td>
				</tr>
			</table>


            <aiswc:focuscontrol id="WinFormsToWeb_FocusControl" style="Z-INDEX: 111; LEFT: 42px; POSITION: absolute; TOP: 546px"
				runat="server"></aiswc:focuscontrol>
			<aiswc:mainmenu id="MainMenu1" style="Z-INDEX: 112; LEFT: 93px; POSITION: absolute; TOP: 491px"
				runat="server"></aiswc:mainmenu>
				
				
			<script language="javascript">
{
	if ((document.forms[0].treeScenario != null)&& (document.forms[0].treeScenario.disabled == false ) ) 
          {
               document.forms[0].treeScenario.focus();
          }

	if ((document.forms[0].cmb_DIM1 != null) && (document.forms[0].cmb_DIM1.disabled == false))
			{
				document.forms[0].cmb_DIM1.focus();
			}
		          
	if (document.forms[0].cmb_DIM1!=null)
	{
		if (document.forms[0].cmb_DIM1.captureEvents!=null)
				document.forms[0].cmb_DIM1.captureEvents(Event.KEYPRESS)
		document.forms[0].cmb_DIM1.onkeypress = updateDIM1;
	}

	//jrs 2005/05/25
	if (document.forms[0].lstDIM1 != null)
	{
		if (document.forms[0].lstDIM1.captureEvents!=null)
				document.forms[0].lstDIM1.captureEvents(Event.MOUSEDOWN)
		document.forms[0].lstDIM1.onmousedown = updateDim1Idx_onclick;
	}
	
	// jrs 2005/05/25
	function updateDim1Idx_onclick(e) {
		// handle combobox 
		var cmb;
		cmb = document.getElementById('cmb_DIM1');
		var lst;
		lst = document.getElementById('lstDIM1');
		  
		//cmb.selectedIndex = lst.selectedIndex + 1; 
		cmb.selectedIndex = -1; 
		return true; 
	}           
}
			</script>
			<!--mti Start slider -->
			<SCRIPT language="JavaScript">
<!--

document.onmousemove = getMouseXY

var tempY = 0
var posY = 0

var adjuststate = 0
var tempA = 0
var tempB = 0

var y = 0
var topPos = 0
var offsetPosY = 0
var mouseoffset = 0

var rangeMin = 0
var curKnownPos = 0
var rangeMax = 0

var varmax = 0;
var varmin = 0;

function getMouseXY(e) {


 	if(navigator.appName == "Netscape"){
		tempX = e.pageX
	 	tempY = e.pageY

	} else {
		tempX = window.event.clientX + window.document.body.scrollLeft
	 	tempY = window.event.clientY + window.document.body.scrollTop
	}
	
	// mti document.Show.MouseX.value = tempX
	// mti document.Show.MouseY.value = tempY

	if (tempX <= 0) {tempX = 0} 
	if (tempY <= 0) {tempY = 0}   

	posX = tempX
	posY = tempY
	
	if (adjuststate == 1){ 	// we need to move the slider

		topPos = parseFloat(document.getElementById(currentslider).style.top)
		
		// having moved we have the captured frozen point.
		tempB = tempA
		tempA = posY 		

		// work out the mouse in relation to the topos
		mouseoffset = tempB - topPos
		offsetPosY = posY - mouseoffset
		
		y = posY - tempB

		// get difference of mouse and toppos
	
		if (y > 1) y = 1;
		if (y < -1) y = -1;

		//if (y == 0){
		//		document.Show.DirectionY.value = "none"
		//}

		//if (y == -1){
		//		document.Show.DirectionY.value = "up"
		
		//}

		//if (y == 1){
		//		document.Show.DirectionY.value = "down"
		
		//}
		
		//document.Show.TopPos.value = topPos
		//document.Show.OffsetTopPos.value = mouseoffset
		//document.Show.TempA.value = tempA
		//document.Show.TempB.value = tempB
		//document.Show.DifferenceY.value = y

		if (offsetPosY < rangeMin){
			offsetPosY = rangeMin
		}

		if (offsetPosY > rangeMax){
			offsetPosY = rangeMax
		}

	//	if (offsetPosY <= rangeMax && offsetPosY >= rangeMin){

			document.getElementById(currentslider).style.top = offsetPosY	
	
	//	}
	//this.form.RangePos.value ="Test"
//document.Show.RangePos.value = 100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100))
		//document.show.txtSliderVal.text = 100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100))
	}

	return true

}

function setsliderbar(slider,min,max){
//mti August 2004 (new function added)
var doc = document.forms[0];
var curslval;
var iposY = 0;
currentslider = slider;
if (doc.txtSliderVal.value != '') {
	rangeMin = min
	rangeMax = max
	curslval = parseFloat(doc.txtSliderVal.value);
	varmax = parseFloat(doc.txtMax.value);
	varmin = parseFloat(doc.txtMin.value);
	iposY= rangeMax-((curslval - varmin)/(varmax - varmin))*(rangeMax - rangeMin);
	document.getElementById(currentslider).style.top = iposY;	
	}
}

function captureStart(slider, state, min, max){

// onmouseout="captureStart('sliderbar',1, 45,210)"
var doc = document.forms[0];
var slval;
var nVal=0;

//  var sVal='';
// var nVal = num.length;
var sChar='';
var nChar=0;

	currentslider = slider
	adjuststate = state
	rangeMin = min
	rangeMax = max
	

	if (adjuststate == 0){
		adjuststate = 1
		tempA = posY 	// gives me frozen point on Y axis
		//document.Show.CaptureState.value = "ON"
		//this.form.RangePos.value ="Test"
	} else {
		adjuststate = 0
		varmax = parseFloat(doc.txtMax.value);
		varmin = parseFloat(doc.txtMin.value);
		

        		
//alert(100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100)));
 		//doc.RangePos.value="Test";
 		//doc.txtSliderVal.value=((100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100)))/100)*(varmax - varmin) + varmin;
 		// this works doc.txtSliderVal.value=((rangeMax-offsetPosY) / (rangeMax - rangeMin))*(varmax - varmin) + varmin;
 		slval = ((rangeMax-offsetPosY) / (rangeMax - rangeMin))*(varmax - varmin) + varmin;
 		
		//document.Show.RangePos.value = 100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100))
		//document.Show.CaptureState.value = "OFF"
		
		//show 2 digits after decimal only
		var slval1 = new String(slval);
		nVal = slval1.length;
		//alert(nVal);
		for(i=0;i<nVal;i++)
		{
			sChar = slval1.charAt(i);
			nChar = sChar.charCodeAt(0);
			if (nChar = 46) { slval = slval1.substring(0,i+5); break;  }
		}
		
		
		doc.txtSliderVal.value = slval;
		doc.cmbShiftYears.disabled = false;
		//mti August 2004 (the line above is added)
	}

}

	function ShowContextMenu1()
	{
		// Quick and Dirty IE NN 6.1 Detection
		if(document.oncontextmenu)
		{
			// IE
			if(document.all)
				//cswmShow('Group_0bd4', '', 'below');
				//cswmShow('Group_fb2c', '', 'below');
				cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
			// NN 6.1
			else
				cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
			return false;
		}
		// Quick and Dirty NN4 Detection
		else if (document.layers)
		{
		    if (e.which == 3)
			{
				cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
				return false;
			}
		}
		// Quick and Dirty NN6.0 Detection
		else if (document.getElementById)
		{
		    if (e.which == 3)
			{
				e.preventDefault();
				cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
				return false;
			}
		}
    }

			function showExplain() {
			//mti August 2004 (new function added)
			var doc = document.forms[0];
			
			// jrs 11/11/04
			if (doc.lst_varname.selectedIndex > -1)
			{
				var hidboth = doc.lst_varname.options[doc.lst_varname.selectedIndex].value;
				var startsel=hidboth.indexOf("-")+1;
				var endsel=hidboth.indexOf("/");
				var hid = hidboth.substring(startsel, endsel);
				
				//loadData("WebHelp/ifshelp.htm" + "#<id="+hid);
				//RH_ShowHelp(0, '/WebHelp/ifshelp.htm' , 15, hid);
				//alert("./WebHelp/ifshelp.htm" + "#<id="+hid);
				
				var sParam = "left="+screen.width+",top="+screen.height+",width=100,height=100";
				//jrs2014/07/18 var hwindow = window.open("../assets/help/WebHelp/ifshelp.htm" + "#<id="+hid+">>wnd=" + "IFs Help", "__webCshStub", sParam);
                var hwindow = window.open(IFsVarHelpPath[doc.lst_varname.selectedIndex]);
				// RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT, hid);
			
			//<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">

			//</SCRIPT>		
				//var objhid = hid;
				//var HH_HELP_CONTEXT = 15;	
				//RH_ShowHelp(0, 'WebHelp/ifshelp.htm' , 15, hid);
				//RH_ShowHelp(0, 'WebHelp/ifshelp.htm' , 15, hid);
			}			
			}
			 
			function showEquation() {
			//mti August 2004 (new function added)
			var doc = document.forms[0];
			
			// jrs 11/11/04 Right click doesn't select an item
			if (doc.lst_varname.selectedIndex > -1)
			{
				var hidboth = doc.lst_varname.options[doc.lst_varname.selectedIndex].value;
				var startsel=hidboth.indexOf("/")+1;
				var hid = hidboth.substring(startsel, hidboth.length);
				//alert("./WebHelp/ifshelp.htm" + "#<id="+hid);
				//RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT, 0);
				//var hwindow = window.open("../assets/help/WebHelp/ifshelp.htm" + "#<id="+hid, "__webCshStub");
                var hwindow = window.open(IFsVarHelpEqnPath[doc.lst_varname.selectedIndex]);
			}
			}
				
				//jrs 2005/06/20		    
			function showHelp() {
				var hwindow = window.open("../assets/help/WebHelp/ifshelp.htm" + "#ifs_help_for_web_version/scenario_analysis/quick_scenario_analysis_with_tree_web_version/quick_scenario_analysis_with_tree_web_version.htm", "__webCshStub");
			}

            //jrs 2011/11/21
            function validateForm()
            {
                var doc = document.forms[0];
                if (doc.txtSliderVal!=null)
                {
    		        var SldVal = doc.txtSliderVal.value;
                    if (SldVal=="" || SldVal > varmax || SldVal < varmin )
                    {
                      alert("Invalid Slider Value");
                      return false;
                    }                 
                }
            }

			function showhg() {
			//var doc = document.forms[0];

			//'mti WebForm Changes to be taken by Jose - take showhg function
			//var myForm = document.formName;
			//myForm.cmd_Exit.style.visibility="hidden";
			//alert("The Form has been sent please wait");
			
			document.body.style.cursor = "wait";
			//doc.cmd_Exit.value = "Please Wait....";
			//doc.cmd_Exit.style.cursor = "wait";
			//cmd_Exit.setAttribute("value","Please Wait...");
			
			
			//document.all('cmd_Exit').enabled = false;
			//document.body.style.cursor = "default";
			}
//-->

if (document.forms[0].txtSliderVal != null){
	setsliderbar('sliderbar',0,165);
}
			</SCRIPT>
			<!--mti end slider -->
			<SCRIPT language="JavaScript" type="text/javascript">
	
			// Function which shows the Context Menu by calling
			// cswmShow and passing in coordinates.
			function ShowContextMenu(e)
			{
				// Quick and Dirty IE NN 6.1 Detection
				var flag = false;
				if (navigator.appName=='Netscape')
					flag = true;
				// NN
				if(flag)
					cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
				// IE
				else
					cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);
			}
			
			// jrs 08/19/04 if list is visible 
			if (document.forms[0].lst_varname != null)
			{
				if (document.forms[0].lst_varname.captureEvents != null)
					document.forms[0].lst_varname.captureEvents(Event.MOUSEDOWN);
				document.forms[0].lst_varname.onmousedown=ShowContextMenu;
				// capture event for context menu
				// Quick and Dirty NN4 Detection
				//if(document.layers)
				//{
				//	document.forms[0].lst_varname.captureEvents(Event.MOUSEDOWN);
				//	document.forms[0].lst_varname.onmousedown=ShowContextMenu;
				//}
				// Quick and Dirty IE Detection
				//else if(typeof document.oncontextmenu == "object")
				//{
				//	document.forms[0].lst_varname.oncontextmenu = ShowContextMenu;
				//}
				// Quick and Dirty NN6 Detection
				//else if(!document.all && document.getElementById)
				//{
					//  NN 6.1 requires different handling than 6.0
				//	if(navigator.vendorSub>=6.1)
				//		document.forms[0].lst_varname.oncontextmenu=ShowContextMenu;
				//	else
				//		document.forms[0].lst_varname.onmouseup=ShowContextMenu;
				//}
			}
			</SCRIPT>
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

