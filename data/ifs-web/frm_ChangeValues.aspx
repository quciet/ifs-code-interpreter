<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ChangeValues" Codebehind="frm_ChangeValues.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
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
		<STYLE>#slidercasing { BORDER-RIGHT: #000000 2px solid; BORDER-TOP: #000000 2px solid; Z-INDEX: 4; LEFT: 20px; BORDER-LEFT: #000000 2px solid; WIDTH: 350px; BORDER-BOTTOM: #000000 2px solid; POSITION: absolute; TOP: 20px; HEIGHT: 75px; BACKGROUND-COLOR: #ffffff }
	#slidertrack { BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; Z-INDEX: 5; LEFT: 45px; BORDER-LEFT: #000000 1px solid; WIDTH: 300px; BORDER-BOTTOM: #000000 1px solid; TOP: 135px; HEIGHT: 3px; BACKGROUND-COLOR: #ffffff }
	#sliderbar { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 0px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 0px; Z-INDEX: 6; LEFT: 23px; PADDING-BOTTOM: 0px; BORDER-LEFT: #000000 1px solid; WIDTH: 20px; CURSOR: pointer; PADDING-TOP: 0px; BORDER-BOTTOM: #000000 1px solid; POSITION: absolute; HEIGHT: 50px; BACKGROUND-COLOR: #dddddd }
		</STYLE>
		<!--mti End Slider  -->
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Change Values" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<table style="WIDTH: 100%; HEIGHT: 232px">
				<tr>
					<td style="BORDER-RIGHT: black thin solid; BORDER-TOP: black thin solid; BORDER-LEFT: black thin solid; BORDER-BOTTOM: black thin solid"
						width="50%">
						<P>&nbsp;
							<asp:label id="Label1" runat="server" Font-Bold="True">Information</asp:label></P>
						<P style="BORDER-RIGHT-WIDTH: thin; BORDER-RIGHT-COLOR: black">&nbsp;
							<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="1">
								<TR>
									<TD>
										<asp:label id="lbl_year" tabIndex="16" runat="server" Height="25" Width="57px" Font-Bold="True"
											Font-Names="Arial" Font-Name="Arial" Font-Size="8pt" ForeColor="WindowText" Text="Year:"
											BackColor="Window">Year:</asp:label></TD>
									<TD><asp:textbox id="txt_year" tabIndex="17" runat="server" Height="19" Width="73px" Font-Bold="False"
											Font-Name="Arial" Font-Size="8.0" ForeColor="WindowText" Text="Text3" BackColor="Window"
											BorderStyle="Solid" MaxLength="0" AutoPostBack="True" Font-Italic="False" Font-Strikeout="False"
											Font-Underline="False"></asp:textbox></TD>
									<TD>
										<asp:label id="lbl_min" tabIndex="13" runat="server" Height="25" Width="57" Font-Bold="True"
											Font-Names="Arial" Font-Name="Arial" Font-Size="8pt" ForeColor="WindowText" Text="Minimum:"
											BackColor="Window">Minimum:</asp:label></TD>
									<TD><asp:textbox id="txt_min" tabIndex="3" runat="server" Height="19" Width="73px" Font-Bold="False"
											Font-Name="Arial" Font-Size="8.0" ForeColor="WindowText" Text="Text4" BackColor="Window"
											BorderStyle="Solid" MaxLength="0" AutoPostBack="False" Font-Italic="False" Font-Strikeout="False"
											Font-Underline="False">Text4</asp:textbox></TD>
								</TR>
								<TR>
									<TD>
										<asp:label id="lbl_value" tabIndex="4" runat="server" Height="25" Width="57" Font-Bold="True"
											Font-Names="Arial" Font-Name="Arial" Font-Size="8pt" ForeColor="WindowText" Text="Value:"
											BackColor="Window">Value:</asp:label></TD>
									<TD><asp:textbox id="txt_value" tabIndex="1" runat="server" Height="19" Width="73" Font-Bold="False"
											Font-Name="Arial" Font-Size="8.0" ForeColor="WindowText" Text="Text6" BackColor="Window"
											BorderStyle="Solid" MaxLength="0" AutoPostBack="False" Font-Italic="False" Font-Strikeout="False"
											Font-Underline="False"></asp:textbox></TD>
									<TD>
										<asp:label id="lbl_max" tabIndex="9" runat="server" Height="17" Width="57px" Font-Bold="True"
											Font-Names="Arial" Font-Name="Arial" Font-Size="8pt" ForeColor="WindowText" Text="Maximum:"
											BackColor="Window">Maximum:</asp:label></TD>
									<TD><asp:textbox id="txt_max" tabIndex="2" runat="server" Height="19" Width="73" Font-Bold="False"
											Font-Name="Arial" Font-Size="8.0" ForeColor="WindowText" Text="Text5" BackColor="Window"
											BorderStyle="Solid" MaxLength="0" AutoPostBack="False" Font-Italic="False" Font-Strikeout="False"
											Font-Underline="False"></asp:textbox></TD>
								</TR>
							</TABLE>
							&nbsp;&nbsp;<br>
							&nbsp;&nbsp;&nbsp;</P>
					</td>
					<td width="50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <div id="container"></div>
						<div><asp:Label ID="lblJS" runat="server"></asp:Label></div>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:label id="lbl_FirstOnly" tabIndex="5" runat="server" Height="46px" Width="209" Font-Bold="True"
							Font-Names="Arial" Font-Name="Arial" Font-Size="8pt" ForeColor="WindowText" Text="The parameter you have selected is an initial condition and can only be changed in the first year."
							BackColor="Window">The parameter you have selected is an initial condition and can only be changed in the first year.</asp:label></td>
				</tr>
				<tr>
					<td style="BORDER-RIGHT: black thin solid; BORDER-TOP: black thin solid; BORDER-LEFT: black thin solid; BORDER-BOTTOM: black thin solid"
						width="50%">&nbsp;
						<asp:label id="Label2" runat="server" Font-Bold="True">Change Numerically </asp:label><br>
						<br>
						&nbsp;
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD>
									<asp:label id="lbl_desired" tabIndex="10" runat="server" Width="96px" Height="16px" Font-Bold="True"
										BackColor="Window" Text="Desired Value:" ForeColor="WindowText" Font-Size="8pt" Font-Name="Arial"
										Font-Names="Arial">Desired Value:</asp:label></TD>
								<TD>
									<asp:textbox id="txt_desired" tabIndex="11" runat="server" Width="73" Height="19" Font-Bold="False"
										BackColor="Window" Text="Desired" ForeColor="WindowText" Font-Size="8.0" Font-Name="Arial"
										Font-Underline="False" Font-Strikeout="False" Font-Italic="False" AutoPostBack="False" MaxLength="0"
										BorderStyle="Solid"></asp:textbox></TD>
							</TR>
							<TR>
								<TD>
									<asp:label id="lbl_repeat" tabIndex="14" runat="server" Width="176px" Height="5px" Font-Bold="True"
										BackColor="Window" Text="Years to Repeat or Interpolate:" ForeColor="WindowText" Font-Size="8pt"
										Font-Name="Arial" Font-Names="Arial">Years to Repeat or Interpolate:</asp:label></TD>
								<TD>
									<asp:textbox id="txt_repeat" tabIndex="12" runat="server" Width="73px" Height="19" Font-Bold="False"
										BackColor="Window" Text="Repeat" ForeColor="WindowText" Font-Size="8.0" Font-Name="Arial"
										Font-Underline="False" Font-Strikeout="False" Font-Italic="False" AutoPostBack="False" MaxLength="0"
										BorderStyle="Solid">Repeat</asp:textbox></TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<BR>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="300" border="1">
							<TR>
								<TD>
									<asp:button id="but_ChangeRepeat" tabIndex="13" runat="server" Width="129" Height="25" Font-Bold="False"
										BackColor="Control" Text="Change/Repeat" ForeColor="ControlText" Font-Size="8.0" Font-Name="Arial"
										Font-Underline="False" Font-Strikeout="False" Font-Italic="False"></asp:button></TD>
								<TD>
									<asp:button id="but_PreviousYear" tabIndex="14" runat="server" Width="129" Height="25" Font-Bold="False"
										BackColor="Control" Text="Previous Year" ForeColor="ControlText" Font-Size="8.0" Font-Name="Arial"
										Font-Underline="False" Font-Strikeout="False" Font-Italic="False"></asp:button></TD>
							</TR>
							<TR>
								<TD>
									<asp:button id="but_Interpolate" tabIndex="15" runat="server" Width="129" Height="25" Font-Bold="False"
										BackColor="Control" Text="Interpolate" ForeColor="ControlText" Font-Size="8.0" Font-Name="Arial"
										Font-Underline="False" Font-Strikeout="False" Font-Italic="False"></asp:button></TD>
								<TD>
									<asp:button id="but_NextYear" tabIndex="16" runat="server" Width="129" Height="25" Font-Bold="False"
										BackColor="Control" Text="Next Year" ForeColor="ControlText" Font-Size="8.0" Font-Name="Arial"
										Font-Underline="False" Font-Strikeout="False" Font-Italic="False"></asp:button></TD>
							</TR>
						</TABLE>
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td width="50%">
						<P>&nbsp;
							<asp:label id="lbl_SliderVal" tabIndex="21" runat="server" Height="17" Width="57" Font-Bold="True"
								Font-Names="Arial" Font-Name="Arial" Font-Size="8pt" ForeColor="WindowText" Text="Value:"
								BackColor="Window">Value:</asp:label>
							<asp:TextBox id="txt_sliderval" runat="server" Width="62px" ReadOnly="True"></asp:TextBox></P>
						<P></P>
						<DIV style="BORDER-RIGHT: black thin solid; BORDER-TOP: black thin solid; BORDER-LEFT: black thin solid; WIDTH: 280px; BORDER-BOTTOM: black thin solid; POSITION: relative; HEIGHT: 76px">
							<DIV onmouseup="captureStart('sliderbar',1, 32,242)" onmousedown="captureStart('sliderbar',0, 32,242)"
								id="sliderbar" style="Z-INDEX: 101; LEFT: 32px; POSITION: absolute; TOP: 16px"></DIV>
							<HR style="Z-INDEX: 103; LEFT: 32px; WIDTH: 230px; POSITION: absolute; TOP: 40px; HEIGHT: 8px"
								width="80.27%" color="darkgray" SIZE="8">
						</DIV>
						<P><asp:button id="but_Change" tabIndex="29" runat="server" Height="25" Width="137" Font-Bold="False"
								Font-Name="Arial" Font-Size="8.0" ForeColor="ControlText" Text="Register Change" 
								Font-Italic="False" Font-Strikeout="False" Font-Underline="False"></asp:button></P>
					</td>
				</tr>
				<tr>
					<td colSpan="2">
						<P><br>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="but_CancelAll" tabIndex="17" runat="server" Height="33" Width="144" Font-Bold="False"
								Font-Name="Arial" Font-Size="8.0" ForeColor="ControlText" Text="Cancel All Changes" 
								Font-Italic="False" Font-Strikeout="False" Font-Underline="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="but_another" accessKey="S" tabIndex="18" runat="server" Height="33" Width="208"
								Font-Bold="False" Font-Name="Arial" Font-Size="8.0" ForeColor="ControlText" Text="(S)ave/Select Another Parameter"
								Font-Italic="False" Font-Strikeout="False" Font-Underline="False"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:button id="cmd_Exit2" accessKey="x" tabIndex="19" runat="server" Height="33" Width="153"
								Font-Bold="False" Font-Name="Arial" Font-Size="8.0" ForeColor="ControlText" Text="E(x)it"
								Font-Italic="False" Font-Strikeout="False" Font-Underline="False"></asp:button>
								&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnHelp" onclick="javascript:ShowHelp();"
                                type="button" value="Help" style="width:153px; height:33px" />
                                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16042);-->
							</P>
						<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<TWC:ADVWEBGRID id="grd_VariableDisplay" runat="server" Height="124px" Width="552px" BorderStyle="Outset"
								FooterVisible="False" HeaderVisible="False" TotalRows="4" RowCount="4" GridBorders-Collapse="Collapse"
								GridBorders-Spacing="0" GridBorders-ColorLight="LightGray" GridBorders-Color="LightGray" GridBorders-Width="0"
								GridBorders-Outer="border" GridBorders-ColorDark="LightGray" GridBorders-Inner="all" GridBorders-Padding="2">
								<Images Cancel="" ImgCheckBoxHint="" Post="" NodeClose="" ActiveRow="" EditHint="Edit" ImgCheckBoxChecked=""
									ImgCheckBoxCheckedHint="" PostHint="Post" NodeCloseHint="Close Detail" NodeOpenHint="Open Detail"
									ImgCheckBox="" Edit="" AlwaysEditRow="" CancelHint="Cancel" NodeOpen=""></Images>
								<ClientEvents ButtonClick="" ImageClick="" DynComboChange="" DynEditDone="" NodeClick="" CellClick=""
									EditDone="" DynCheckClick="" DynEditChange="" ComboChange=""></ClientEvents>
								<Scroll Enable="True" Scrollbar3DLightColor="" ScrollbarShadowColor="" ScrollbarFaceColor=""
									ScrollbarTrackColor="" ScrollbarHighlightColor="" ScrollbarBaseColor="" Persistent="False"
									ScrollbarArrowColor="" ScrollbarDarkshadowColor=""></Scroll>
								<DatePickerEditor DateSeparator="dsSlash" GradientDirection="gdHorizontal" GradientStartColor="White"
									GradientEndColor="AliceBlue"></DatePickerEditor>
								<Controller Borders-Collapse="Collapse" Borders-Spacing="0" Borders-ColorLight="LightGray" Borders-Color="LightGray"
									Borders-Width="1" Borders-Outer="border" Borders-ColorDark="LightGray" Borders-Inner="all"
									Borders-Padding="2" GradientOrientation="Horizontal" ImgLast="" ImgFirst="" HintPrev=""
									PagerType="Button" Position="None" Height="20" HintLast="" ImgPrev="" Caption="" ImgNext=""
									IndicatorFormat="" TextNext="Next" HintFirst="" TextLast="Last" TextFirst="First" BackColor="Control"
									Alignment="Left" MaxPages="5" IndicatorType="PageNo" RowCountSelect="False" HintNext=""
									Pager="PrevNextFirstLast" TextPrev="Prev" BackColorTo="White"></Controller>
								<GlyphImages PrevYear="" NextYear="" PrevMonth="" NextMonth=""></GlyphImages>
								<RowHeader GradientOrientation="Vertical" Visible="False" BackColor="Control" Borders-Collapse="Collapse"
									Borders-Spacing="0" Borders-ColorLight="LightGray" Borders-Color="LightGray" Borders-Width="1"
									Borders-Outer="border" Borders-ColorDark="LightGray" Borders-Inner="all" Borders-Padding="2"
									BackColorTo="" Width="25"></RowHeader>
								<Bands Active="False" SecondaryColor="Info" PrimaryColor="White"></Bands>
								<Columns>
									<twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
										TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
										ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
										SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
										SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
										ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
										PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="" FooterGradientOrientation="Vertical"
										HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
										Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="33"
										DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
										DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
									<twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
										TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
										ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
										SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
										SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
										ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
										PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="" FooterGradientOrientation="Vertical"
										HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
										Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="33"
										DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
										DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
									<twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
										TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
										ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
										SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
										SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
										ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
										PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="" FooterGradientOrientation="Vertical"
										HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
										Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="33"
										DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
										DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
								</Columns>
								<SortSettings Ascending="" Direction="Ascending" Column="-1" Show="True" InitDirection="Ascending"
									Descending=""></SortSettings>
							</TWC:ADVWEBGRID></P>
					</td>
				</tr>
			</table>
			<aiswc:focuscontrol id="WinFormsToWeb_FocusControl" style="Z-INDEX: 125; LEFT: 648px; POSITION: absolute; TOP: 464px"
				runat="server"></aiswc:focuscontrol>
			<script language="javascript">
{
	if ((document.all['txt_value'] != null)&& (document.all['txt_value'].disabled == false ) ) 
          {
               document.all('txt_value').focus();
          }
}
			</script>
			<SCRIPT language="JavaScript">	


document.onmousemove = getMouseXY

var tempX = 0
var posX = 0

var adjuststate = 0
var tempA = 0
var tempB = 0

var x = 0
var leftPos = 0
var offsetPosX = 0
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

		leftPos = parseFloat(document.getElementById(currentslider).style.left)
		
		// having moved we have the captured frozen point.
		tempB = tempA
		tempA = posX 		

		// work out the mouse in relation to the topos
		mouseoffset = tempB - leftPos
		offsetPosX = posX - mouseoffset
		
		x = posX - tempB

		// get difference of mouse and toppos
	
		if (x > 1) x = 1;
		if (x < -1) x = -1;

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

		if (offsetPosX < rangeMin){
			offsetPosX = rangeMin
		}

		if (offsetPosX > rangeMax){
			offsetPosX = rangeMax
		}

	//	if (offsetPosY <= rangeMax && offsetPosY >= rangeMin){

			document.getElementById(currentslider).style.left = offsetPosX	
	
	//	}
	//this.form.RangePos.value ="Test"
//document.Show.RangePos.value = 100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100))
		//document.show.txtSliderVal.text = 100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100))
	}

	return true

}

function setsliderbar(slider,min,max){
//mti August 2004 (new function added)
//horizontal slider
var doc = document.forms[0];
var curslval;
var iposX = 0;
currentslider = slider;
if (doc.txt_sliderval.value != '') {
	rangeMin = min
	rangeMax = max
	curslval = parseFloat(doc.txt_sliderval.value);
	//for horz slider varmax/varmin is other way - mti
	varmax = parseFloat(doc.txt_min.value);
	varmin = parseFloat(doc.txt_max.value);
	iposX= rangeMax-((curslval - varmin)/(varmax - varmin))*(rangeMax - rangeMin);
	document.getElementById(currentslider).style.left = iposX;	
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
		tempA = posX 	// gives me frozen point on X axis
		//document.Show.CaptureState.value = "ON"
		//this.form.RangePos.value ="Test"
	} else {
		adjuststate = 0
		//for horz slider varmax/varmin is other way - mti
		varmax = parseFloat(doc.txt_min.value);
		varmin = parseFloat(doc.txt_max.value);
		

        		
//alert(100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100)));
 		//doc.RangePos.value="Test";
 		//doc.txtSliderVal.value=((100 - ((((offsetPosY - rangeMin) / (rangeMax - rangeMin)) * 100)))/100)*(varmax - varmin) + varmin;
 		// this works doc.txtSliderVal.value=((rangeMax-offsetPosY) / (rangeMax - rangeMin))*(varmax - varmin) + varmin;
 		slval = ((rangeMax-offsetPosX) / (rangeMax - rangeMin))*(varmax - varmin) + varmin;
 		
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
			if (nChar = 46) { slval = slval1.substring(0,i+4); break;  }
		}
		
		
		doc.txt_sliderval.value = slval;
		//doc.cmbShiftYears.disabled = false;
		//mti August 2004 (the line above is added)
	}

}	

if (document.all['txt_sliderval'] != null){
setsliderbar('sliderbar',32,242);
}
	
			</SCRIPT>
		</form>
	</body>
</HTML>
