<%@ Reference Page="~/Index.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_EdnCohoDisp" Codebehind="frm_EdnCohoDisp.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Education Distribution</title>
		<%--<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>--%>	
		
		<%--JAM 2024/07/23 Bug fix related to GET parameters--%> 
		<script src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>"></script>

		
		<!--<script language="javascript" type="text/javascript" src="/Scripts/common.js"></script>-->
        <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/common.js")%>"></script>

		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>

        <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>


        <!--<script src="/Scripts/highcharts.js"></script>-->
        <!--<script src="/Scripts/exporting.js"></script>-->
        <!--jam 2023/01/11-->
      <%-- <script src="<%= Page.ResolveUrl("~/Scripts/highcharts.js")%>"></script>
       <script src="<%= Page.ResolveUrl("~/Scripts/exporting.js")%>"></script>--%>
		
		 <!--jam 2025/03/01 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
   
		<!--jam 2025/03/01 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>

        

	</HEAD>
	<body bgColor="white" leftMargin="0" rightMargin="0" topmargin="0">
		<form id="frm_EdnCohoDisp" runat="server">
			<div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px;">
                
                <!--jam 2023/01/11-->
                <%--<img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources..--%>
                
                
                <img id="loadimg" src="<%= Page.ResolveUrl("~/images/squares-circle.gif")%>"  height="93" width="93"><br>Loading resources...
                
                .</div>
            <asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!-- jam 2022/04/14 -->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>				
					<asp:TableCell Text="Education by Age, Sex and Level" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="wmMain" runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockPaddingAdjust="False"
				MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>			
			<table>                
                <tr>
                    <td align=center style="width: 203%">
                        
                        <table>
                            <tr>
                                <td>
									<%--jam 2025/03/01 New highcharts version libraries container width adjusted from 90% to 100%--%>
									<%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>	--%>	
									 <div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>
                                </td>
                                <td><div id="containerColumn" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>		</td>
                            </tr>
                        </table>                        		

                        <table>
                            <tr>
                                <td><div id="container1"></div></td>
                                <td><div id="container2"></div></td>
                            </tr>
                            <tr>
                                <td><div id="container3"></div></td>
                                <td><div id="container4"></div></td>
                            </tr>
                        </table>                        		

                        <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>                        
                    </td>
                </tr>
                <tr>
                    <td align=center style="width: 203%">
                        <asp:Panel ID="pnlCompare" runat="server" Visible=false>
                        <table>
                            <tr>
                                <td rowspan="2">
                                    <asp:Button id="cmdRefreshComp" tabIndex="11"
					                runat="server" Height="33" Width="113" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
					                Font-Italic="False" 
					                Text="Refresh"></asp:Button>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbReg1" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbYr1" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbSce1" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbReg2" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbYr2" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbSce2" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td rowspan="2">
									<asp:Image ID="imgEdCats" runat="server" 
									Height="45" Width="750" ImageUrl="~/Images/EducationPyramidCats.png"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="cmbReg3" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbYr3" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbSce3" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbReg4" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbYr4" runat="server">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="cmbSce4" runat="server">
                                    </asp:DropDownList>                            
                                </td>
                            </tr>
                        </table>                        		
                        
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align=center style="width: 203%">
                    <asp:listbox id="lstYear" tabIndex="4"
				runat="server" Width="121" Height="326" BackColor="Window" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
				Visible="False" AutoPostBack="True"></asp:listbox>
				<asp:listbox id="lstGroups" 
				tabIndex="8" runat="server" Width="121" Height="326" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Visible="False"></asp:listbox>
				<asp:listbox id="cntrylist" 
				tabIndex="4" runat="server" Width="121" Height="326" BackColor="Window" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
				Visible="False"></asp:listbox>
                    </td>
                </tr>
                <tr>
                    <td align=center style="width: 203%">
        				<asp:Label id="lbldata" 
		    			tabIndex="7" runat="server" Height="33" Width="449" ForeColor="ControlText" Font-Underline="False"
			    		Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial"
				    	BackColor="Window"></asp:Label>
				    	<asp:HiddenField ID="hfTitle" Value="" runat=server/>
                    </td>
                </tr>
                <tr>
                    <td>                   
				<TWC:AdvWebGrid id="grdcohort" 
				runat="server" Height="603px" Width="1050px" BorderStyle="Outset" FooterVisible="False" GridBorders-Collapse="Collapse"
				GridBorders-Spacing="0" GridBorders-ColorLight="LightGray" GridBorders-Color="LightGray" GridBorders-Width="0"
				GridBorders-Outer="border" GridBorders-ColorDark="LightGray" GridBorders-Inner="all" GridBorders-Padding="2"
				HeaderVisible="False" TotalRows="5" RowCount="5" MouseSelect="SingleCell" tabIndex="1" Visible="False">
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
			</TWC:AdvWebGrid>					                    			
                    </td>
                </tr>
                <tr>
                    <td align=center style="width: 203%">			        	
				        <asp:panel id="frameData" 
				        tabIndex="6" runat="server" Width="489" Height="60px" BackColor="Window" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
				        Visible="False">
			                <input id="cmdGridPrint" tabIndex="12" type="button" Value="Print Numbers"  
			                 onclick='PrintGrid();' style="height: 32px" />
				            <asp:Button id="cmdGridSave" tabIndex="11"
					        runat="server" Height="33" Width="113" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" 
					        Text="Save Numbers"></asp:Button>
				            <asp:Button id="cmdClose" tabIndex="10"
					        runat="server" Height="33" Width="113" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" 
					        Text="Exit"></asp:Button>			
			            </asp:panel>
                    </td>
                </tr>
                <tr>
                    <td align=center style="width: 218%">
                    		<asp:panel id="pnlMaxScale" tabIndex="6" runat="server" Width="489" Height="60px" BackColor="Window" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False"
				            Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Visible="False">
                                <b>Scaling Max for Population: </b><asp:TextBox ID="txtPop" runat="server"></asp:TextBox><br />
				                <asp:Button id="cmdOKMaxScale" tabIndex="11"
					                runat="server" Height="33" Width="113" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
					                Font-Italic="False" Font-Bold="False" 
					                Text="Ok"></asp:Button>
				                <asp:Button id="cmdCancleMaxScale" tabIndex="10"
					                runat="server" Height="33" Width="113" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
					                Font-Italic="False" Font-Bold="False" 
					                Text="Cancel"></asp:Button>
			                </asp:panel>			
                    </td>
                </tr>
            </table>
			
			<script language="javascript">
{
	if ((document.all['cntrylist'] != null)&& (document.all['cntrylist'].disabled == false ) ) 
          {
               document.all('cntrylist').focus();
	}

	if (document.getElementById('lblJS') == null) {
	    //JAM 2013-04-02
	    document.getElementById('firstloading').style.visibility = "hidden";
	}

}
			</script>

	<script type="text/javascript" language="javascript">

function PrintGrid()

{

 var prtContent = document.all('grdcohort');

 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 //var WinPrint = window.open('','');
 
 var TxtToPrint = prtContent.innerHTML;
 
 var TxtToPrint2 = TxtToPrint.replace('height: 169px', 'height: 550px'); //FIREFOX
 TxtToPrint2 = TxtToPrint2.replace('HEIGHT: 169px', 'HEIGHT: 550px'); //IE

 var TxtToPrint3 = TxtToPrint2.replace('width: 600px', 'width: 2000px');
 TxtToPrint3 = TxtToPrint3.replace('WIDTH: 600px', 'WIDTH: 2000px');

 var Title = document.all('hfTitle').value;
 
 WinPrint.document.write('<H2>' + Title + '</H2>' + TxtToPrint3);

 WinPrint.document.close();

 WinPrint.focus();

 alert("Use your printer's settings to change to landscape and to scale to page (50%)");
 
 WinPrint.print();

 WinPrint.close();
 
 

 //prtContent.innerHTML=strOldOne;

}

//jrs 2011/05/04
function cntrylist_click()
{
    var doc = document.forms[0];
    var ctry="Country="+RegCode[doc.cntrylist.selectedIndex+1];
    window.location.search=ctry;
}

//jrs 2011/05/05
function lstGroups_click()
{
    var doc = document.forms[0];
    var grp="Group="+PrepareForURL(doc.lstGroups[doc.lstGroups.selectedIndex].text);
    window.location.search=grp;
}


</script>

        </form>
		<!--
		<table style="Z-INDEX: 115; LEFT: 190px; POSITION: absolute; TOP: 597px" >	
				<tr>
				<td Height="33" >
                    <input class="btn" id="Submit1"  style="WIDTH: 85px; height:33px;" onclick="Maximize_Button()" type="submit" value="Maximize" name="Submit1">
                    <input class="btn" id="Submit2"  style="WIDTH: 85px; height:33px;" onclick="Print_Button()" type="submit" value="Print" name="Submit2">
                    <input class="btn" id="Submit3"  style="WIDTH: 85px; height:33px; " onclick="Export_Button()" type="submit" value="Export" name="Submit3">
                </td>
		        </tr>
        </table>  
		-->
		
	</body>
</HTML>


<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>
