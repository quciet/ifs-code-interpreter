<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_EdnCostDisplay" Codebehind="frm_EdnCostDisplay.aspx.vb" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Scale-up Cost Display</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>		
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
		    <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
			<asp:label id="lblReg" style="Z-INDEX: 100; LEFT: 8px; POSITION: absolute; TOP: 64px" runat="server"
				Font-Bold="True">Regions:</asp:label>
			<asp:button id="cmdClear" style="Z-INDEX: 101; LEFT: 613px; POSITION: absolute; TOP: 322px" accessKey="C"
				tabIndex="4" runat="server" Width="88px" Text="Clear"></asp:button>
			
			
            <asp:listbox id="lstReg" style="Z-INDEX: 105; LEFT: 8px; POSITION: absolute; TOP: 96px" runat="server"
				Height="537px" Width="168" SelectionMode="Multiple"></asp:listbox>
			
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/14-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Scale-up Cost Display" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			
			<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				<tr>
					<td><cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 113; LEFT: 0px; POSITION: absolute; TOP: 16px"
							runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu></td>
				</tr>
			</table>
			<asp:label id="lblMsg" style="Z-INDEX: 106; LEFT: 17px; POSITION: absolute; TOP: 643px" runat="server"
				Height="24px" Width="424" Font-Size="Large" ForeColor="Red" Visible="False" Font-Bold="True">Label</asp:label>
			<asp:panel id="frm_Horizon" style="Z-INDEX: 107; LEFT: 721px; POSITION: absolute; TOP: 64px"
				tabIndex="3" runat="server" Height="172" Width="169" Font-Size="8.0" ForeColor="WindowText"
				Visible="False" Font-Bold="False" BackColor="Window" Font-Underline="False" Font-Name="Arial"
				Font-Italic="False" Font-Strikeout="False" Enabled="True">
				Change Horizon Year: <BR>
				<asp:TextBox ID="txtHorizon" runat="server"></asp:TextBox><asp:Button ID="cmdHorizon"
                    runat="server" Text="Submit" /><br />
                    <asp:DropDownList id="cmb_Horizon" tabIndex="4" runat="server"
					Font-Bold="False" Height="129" Width="153" Visible="True" ForeColor="WindowText"
					Font-Size="8.0" Enabled="True" Font-Strikeout="False" Font-Italic="False" Font-Name="Arial"
					Font-Underline="False" BackColor="Window" AutoPostBack="True"></asp:DropDownList>
			</asp:panel>

                <asp:listbox id="lstFiles" style="Z-INDEX: 109; LEFT: 200px; POSITION: absolute; TOP: 96px" runat="server"
				Height="106" Width="505px" SelectionMode="Multiple"></asp:listbox>
				
			<asp:button id="cmdAdd" style="Z-INDEX: 108; LEFT: 197px; POSITION: absolute; TOP: 322px" runat="server" Width="88px" Text="Add" AccessKey="A"></asp:button>
				<asp:button id="cmdTable" style="Z-INDEX: 110; LEFT: 301px; POSITION: absolute; TOP: 322px" runat="server"
				Width="88px" Text="Table" AccessKey="T" TabIndex="1"></asp:button><asp:button id="cmdLine" style="Z-INDEX: 111; LEFT: 405px; POSITION: absolute; TOP: 322px" tabIndex="2"
				runat="server" Width="88px" Text="Line Graph" AccessKey="L"></asp:button>
				<asp:button id="cmdBar" style="Z-INDEX: 112; LEFT: 509px; POSITION: absolute; TOP: 322px" tabIndex="3"
				runat="server" Width="88px" Text="Bar Graph" AccessKey="B"></asp:button>
				<asp:button id="cmdContinue" style="Z-INDEX: 113; LEFT: 717px; POSITION: absolute; TOP: 322px"
				tabIndex="5" runat="server" Width="88px" Text="Continue" Visible="False" AccessKey="X"></asp:button>
				<asp:label id="Label2" style="Z-INDEX: 114; LEFT: 200px; POSITION: absolute; TOP: 64px" runat="server"
				Font-Bold="True">Scenarios:</asp:label>

            <asp:CheckBox ID="chkUseFlatCost"  Text="Use Constant Cost" runat="server" style="Z-INDEX: 114; LEFT: 300px; POSITION: absolute; TOP: 64px"/>
            <asp:CheckBox ID="chkUseFlatEnr"  Text="Use Constant Enrollment" runat="server" style="Z-INDEX: 114; LEFT: 450px; POSITION: absolute; TOP: 64px"/>

            <asp:CheckBox ID="chkPrimary" runat="server" Style="z-index: 102; left: 200px;
                position: absolute; top: 216px" Checked="True" Font-Bold="True" Text="Primary Enrollment Target %" AccessKey="P"  ToolTip="Primary Enrollment Target (Alt + P)"/>
            <asp:TextBox ID="txtPriTarget" runat="server" Font-Bold="True" Style="z-index: 104;
                left: 450px; position: absolute; top: 216px" Width="48px" EnableViewState="False" TabIndex="1">100</asp:TextBox>
                
            <asp:CheckBox ID="chkSecondary" runat="server" Font-Bold="True" Style="z-index: 115; left: 200px;
                position: absolute; top: 248px" Text="Secondary Enrollment Target %" AccessKey="S" ToolTip="Secondary Enrollment Target (Alt + S)"></asp:CheckBox>
            <asp:TextBox ID="txtSecTarget" runat="server" Font-Bold="True" Style="z-index: 104;
                left: 450px; position: absolute; top: 248px" Width="48px" EnableViewState="False" TabIndex="1">100</asp:TextBox>
                
            <asp:CheckBox ID="chkTertiary" runat="server" Style="z-index: 102; left: 200px;
                position: absolute; top: 280px" Font-Bold="True" Text="Tertiary Enrollment Target %" AccessKey="T" Tooltip="Tertiary Enrollment Target (Alt+T)" TabIndex="2" />
            <asp:TextBox ID="txtTerTarget" runat="server" Font-Bold="True" Style="z-index: 104;
                left: 450px; position: absolute; top: 280px" Width="48px" EnableViewState="False" TabIndex="1">100</asp:TextBox>
                
			<asp:button id="cmdPriEnrType" style="Z-INDEX: 108; LEFT: 516px; POSITION: absolute; TOP: 216px" runat="server" Width="190px" Text="Using Gross Enrollment" AccessKey="G" ToolTip="Using Gross Enrollment (Alt+G)"></asp:button>
            <asp:Label ID="Label1" runat="server" Text="Label" style="Z-INDEX: 108; LEFT: 516px; POSITION: absolute; TOP: 248px">Start in</asp:Label>
            <asp:DropDownList ID="cmb_TrgtStartYr" style="Z-INDEX: 108; LEFT: 614px; POSITION: absolute; TOP: 249px" runat="server" Width="94px">
            </asp:DropDownList>    
            <asp:Label ID="Label3" runat="server" Text="Label" style="Z-INDEX: 108; LEFT: 516px; POSITION: absolute; TOP: 280px">Reach Goal in</asp:Label>
            <asp:DropDownList ID="cmb_TrgtEndYr" style="Z-INDEX: 108; LEFT: 612px; POSITION: absolute; TOP: 282px" runat="server" Width="94px">
            </asp:DropDownList>    

            &nbsp; &nbsp; &nbsp;&nbsp;
<TWC:AdvWebGrid id="grd_VariableDisplay" style="Z-INDEX: 120; LEFT: 199px; POSITION: absolute; TOP: 354px"
				runat="server" Width="618px" Height="144px" BorderStyle="Outset" FooterVisible="False" GridBorders-Collapse="Collapse"
				GridBorders-Spacing="0" GridBorders-ColorLight="LightGray" GridBorders-Color="LightGray" GridBorders-Width="0"
				GridBorders-Outer="border" GridBorders-ColorDark="LightGray" GridBorders-Inner="all" GridBorders-Padding="2"
				HeaderVisible="False" TotalRows="5" RowCount="5" MouseSelect="SingleCell" tabIndex="1">
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
			</TWC:AdvWebGrid>                &nbsp;
            <asp:Panel ID="pnlContext" runat="server" BackColor="White" BorderStyle="Outset"
                Height="48px" Style="z-index: 123; left: 200px; position: absolute; top: 368px"
                Visible="False" Width="624px">
                <p>
                    &nbsp;
                    <asp:Label ID="lblRowCol" runat="server">Label</asp:Label></p>
                <p>
                    &nbsp;
                    <asp:LinkButton ID="lbtDelete" runat="server" Style="z-index: 100; left: 8px; position: absolute;
                        top: 40px">Delete</asp:LinkButton>
                </p>
                <p>
                    &nbsp; &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtCancel" runat="server" Style="z-index: 102; left: 8px; position: absolute;
                        top: 80px">Cancel</asp:LinkButton>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<a href="#1"></a>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </p>
                <p>
                    &nbsp;</p>
            </asp:Panel>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            
                <asp:CheckBox ID="chkShowUnitCost" runat="server" Style="z-index: 102; left: 201px;
                position: absolute; top: 522px" Font-Bold="True" Text="Show Unit Costs" AutoPostBack="False" TabIndex="2" />
                <asp:CheckBox ID="chkGDPPcnt" runat="server" Style="z-index: 102; left: 201px;
                position: absolute; top: 552px" Font-Bold="True" Text="Show As % of GDP" AutoPostBack="False" TabIndex="2" />
                <asp:CheckBox ID="chkGovConPcnt" runat="server" Style="z-index: 102; left: 201px;
                position: absolute; top: 582px" Font-Bold="True" Text="Show As % of GOVCON" AutoPostBack="False" TabIndex="2" />
                <asp:CheckBox ID="chkCostDiffFromPop" runat="server" Style="z-index: 102; left: 201px;
                position: absolute; top: 612px" Font-Bold="True" Text="Show Cost Diff. from Pop." AutoPostBack="False" TabIndex="2" />
                
    			<asp:button id="cmdCheckAll" style="Z-INDEX: 108; LEFT: 435px; POSITION: absolute; TOP: 521px" runat="server" Width="88px" Text="Check All" AccessKey="A"></asp:button>
    			<asp:button id="cmdUncheckAll" style="Z-INDEX: 108; LEFT: 435px; POSITION: absolute; TOP: 551px" runat="server" Width="88px" Text="Uncheck All" AccessKey="A"></asp:button>

                <asp:CheckBox ID="chkShowEnrDiff" runat="server" Style="z-index: 102; left: 562px;
                position: absolute; top: 522px" Font-Bold="True" Text="Show Enrolment Rate Differentials" AutoPostBack="False" TabIndex="2" />
                <asp:CheckBox ID="chkShowStudentsDiff" runat="server" Style="z-index: 102; left: 562px;
                position: absolute; top: 552px" Font-Bold="True" Text="Show Student Differentials" AutoPostBack="False" TabIndex="2" />
                <asp:CheckBox ID="chkShowAll" runat="server" Style="z-index: 102; left: 562px;
                position: absolute; top: 582px" Font-Bold="True" Text="Show Case Target and Differential" AutoPostBack="False" TabIndex="2" />

                <script type="text/javascript">
                    if ((document.forms[0].txtHorizon != null) && (document.forms[0].txtHorizon.disabled == false))
				    {
					    document.forms[0].txtHorizon.focus();
				    }
			        //jrs 2012/01/16
                    function clickButton(e, buttonid){
                          var evt = e ? e : window.event;
                          var bt = document.getElementById(buttonid);
                          if (bt){
                              if (evt.keyCode == 13){
                                    bt.click();
                                    return false;
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
