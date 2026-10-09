<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_TableFunc2" Codebehind="frm_TablFunc2.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
         <!--jam 2021/11/27-->
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>--%>
         <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
       <%-- <script src="Scripts/highcharts.js"></script>
        <script src="Scripts/exporting.js"></script>--%>

		<!--jam 2025/03/11 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>

		 <!--jam 2025/03/11 New highcharts version libraries-->
		<script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>


	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="frm_TablFunc2" method="post" runat="server">
			<aiswc:messagebox id="MsgBox" style="Z-INDEX: 104; LEFT: 336px; POSITION: absolute; TOP: 48px" runat="server"></aiswc:messagebox>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Height="15px" Width="100%" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Display Functions" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table><cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 105; LEFT: 144px; POSITION: absolute; TOP: 16px"
				runat="server" Height="16px" Width="16px" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
			<table>
				<tr>
					<td>
			            <asp:label id="lbl_names0" runat="server">Filter Functions (press enter): </asp:label>
                        &nbsp;<asp:TextBox ID="txtFilter" runat="server" Width="502px"></asp:TextBox>
                        <br />
                        <br />
			                <asp:label id="lbl_names" runat="server">Functions (click to see/edit):</asp:label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:checkbox id="chk_ExtendList" runat="server" AutoPostBack="True" Width="88px" Text="Extend List"></asp:checkbox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:checkbox id="chkOrdByTime" runat="server" AutoPostBack="True" 
                            Width="173px" Text="Order by Last Change"></asp:checkbox>
                        <br />
						<asp:Label id="lblMsg" runat="server"
							Visible="False" Font-Bold="True" Font-Size="Large" ForeColor="Red">Label</asp:Label>
                        <br />
						<asp:listbox id="Lst_Names" runat="server" SelectionMode="Multiple" Height="96px" Width="688px"></asp:listbox>
					</td>
                    <td valign=top>
                        <br />
                        <asp:checkbox id="chkRun" runat="server" AutoPostBack="True" Width="132px" 
                            Text="Used in Run" Checked=true></asp:checkbox><br>
                        <asp:checkbox id="chkRunHis" runat="server" AutoPostBack="True" Width="169px" 
                            Text="Used in Historical Run" Checked=true></asp:checkbox><br>
                        <asp:checkbox id="chkPre" runat="server" AutoPostBack="True" Width="159px" 
                            Text="Used in Preprocessor" Checked=true></asp:checkbox><br>
                        <asp:checkbox id="chkPreHis" runat="server" AutoPostBack="True" Width="224px" 
                            Text="Used in Historical Preprocessor" Checked=true></asp:checkbox><br>
                        <asp:checkbox id="chkNotUsed" runat="server" AutoPostBack="True" Width="132px" 
                            Text="Not Used" Checked=true></asp:checkbox><br>
                        <asp:checkbox id="chkAll" runat="server" AutoPostBack="True" Width="132px" 
                            Text="All" Checked=true Enabled=false></asp:checkbox><br>
                    </td>
				</tr>
				<tr>
					<td colspan='2' >
						 <%--jam 2025/03/11 New highcharts version libraries container width adjusted from 90% to 100%--%>
			            <%--<div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>--%>
						<div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>
                            <div><asp:Label ID="lblJS" runat="server"></asp:Label></div>                        
					</td>
				</tr>
				<tr>
					<td colspan='2'>
						<twc:advwebgrid id="dbgFunctions" runat="server" Height="150px" Width="800px" FooterVisible="False">
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
								<twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" 
                                    ShowHint="False" DetailField=""
									TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
									ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="True"
									SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
									SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
									ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
									PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="Function Name" FooterGradientOrientation="Vertical"
									HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
									Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="25"
									DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
									DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
                                <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                                    ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                                    ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                                    DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                                    DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                                    FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                                    FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                                    HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                                    PopupColor="White" PopupColorTo="White" 
                                    PopupGradientOrientation="Vertical" PopupHeight="200"
                                    PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                                    SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="Function Spec"
                                    TitleRowSpan="True" TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True"
                                    Width="25" WidthType="Percent">
                                </twc:Column>
								<twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" 
                                    ShowHint="False" DetailField=""
									TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
									ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="True"
									SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
									SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
									ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
									PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="RSqr" FooterGradientOrientation="Vertical"
									HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
									Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="25"
									DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
									DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
							    <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" 
                                    CheckTrue="true" ClickConfirm="" Color="" ColumnHeaderCheckBox="False" 
                                    ColumnHeaderClick="False" ColumnHeaderNode="False" CssClass="" 
                                    DataButtonType="Link" DataField="" DetailColor="" DetailField="" 
                                    DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1" 
                                    DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" 
                                    FooterColor="Control" FooterColorTo="" FooterFormat="0" 
                                    FooterGradientOrientation="Vertical" FooterText="" FooterType="Text" 
                                    ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control" 
                                    HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle" 
                                    PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" 
                                    PopupHeight="200" PopupWidth="200" ProgressColor="Red" ShowHint="False" 
                                    SortFormat="Alphabetic" SortPrefix="" SortSuffix="" SubTitle="" 
                                    SubTitleSpan="0" Title="SE" TitleRowSpan="False" TitleSpan="0" Type="Normal" 
                                    VerticalAlignment="Middle" Visible="True" Width="25" WidthType="Percent">
                                </twc:Column>
							</Columns>
							<SortSettings Ascending="" Direction="Ascending" Column="-1" Show="True" InitDirection="Ascending"
								Descending=""></SortSettings>
						</twc:advwebgrid>					
                        </td>
				</tr>
			</table>
			<script language="javascript">
			    {
			        if ((document.forms[0].txtFilter != null) && (document.forms[0].txtFilter.disabled == false)) {
			            document.forms[0].txtFilter.focus();
			        }
			        //jrs 2012/01/16
			        function clickButton(e, buttonid) {
			            var evt = e ? e : window.event;
			            var bt = document.getElementById(buttonid);
			            if (bt) {
			                if (evt.keyCode == 13) {
			                    bt.click();
			                    return false;
			                }
			            }
			        }
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
