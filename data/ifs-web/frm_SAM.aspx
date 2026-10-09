<%@ Reference Page="~/ErrorHandler.aspx" %>
<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_SAM" Codebehind="frm_SAM.aspx.vb" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		
        <!--jam 2021/09/01-->
       <!-- <SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	-->

        <SCRIPT language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript">
		</SCRIPT>	


		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<style type="text/css">	table[id$="grdSAMDettbl1"] > tbody > tr:nth-child(1) > td,table[id$="grdSAMDettbl1"] > tbody > tr:nth-child(2) > td {
		background: #000084 !important; color:white !important; font-weight: bold;}
		table[id$="grdSAMDettbl1"] > tbody > tr:nth-child(1) > td div, table[id$="grdSAMDettbl1"] > tbody > tr:nth-child(2) > td div{color: white !important;}
		</style>
		<!--dsb 2026/07/15 Color the first two rows in Expand SAM view -->
		<script type="text/javascript">
			window.addEventListener("load", function () {
                //dsb 2026/07/16 Merge the major headings in the first header column of the table
                var table = document.querySelector('table[id$="grdSAMDettbl1"]');
                if (!table || !table.tBodies || table.tBodies.length === 0) {
                    return;
                }
                var rows = table.tBodies[0].rows;
                var grpCell = null;
                var grpSpan = 0;
                function finishGroup() {
                    if (grpCell && grpSpan > 1) {
                        grpCell.rowSpan = grpSpan;
                        grpCell.style.verticalAlign = "middle";
                    }
                }
                for (var rowIdx = 2; rowIdx < rows.length; rowIdx++) {
                    var row = rows[rowIdx];
                    if (!row.cells || row.cells.length === 0) {
                        continue;
                    }
                    var firstCell = row.cells[0];
                    var text = firstCell.textContent.replace(/\u00a0/g, "").trim();
                    if (text !== "") {
                        finishGroup();
                        grpCell = firstCell;
                        grpSpan = 1;
                    } else if (grpCell) {
                        grpSpan++;
                        row.removeChild(firstCell);
                    }
                }
				finishGroup();
            });
        </script>
        <!-- jam 2021/11/27 -->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/15-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Social Accounting Matrix" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 999; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
			<asp:panel id="pnlContext" style="Z-INDEX: 1000; LEFT: 80px; POSITION: absolute; TOP: 208px"
				runat="server" Width="642px" Height="200px" Visible="False" BackColor="White" BorderStyle="Outset">
				<P>&nbsp;
					<asp:Label id="lblRowCol" runat="server">Label</asp:Label></P>
				<P>
					<asp:LinkButton id="lbtExpandCell" runat="server">Expand Cell</asp:LinkButton></P>
				<P>
					<asp:LinkButton id="lbtShowOverTime" runat="server">Show Over Time</asp:LinkButton></P>
				<P>
					<asp:LinkButton id="lbtExplain" runat="server">Explain Cell Content</asp:LinkButton></P>
				<P>
					<asp:LinkButton id="lbtCancel" runat="server">Cancel</asp:LinkButton></P>
			</asp:panel>
			<asp:label id="lblUnits" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 425px" tabIndex="2"
				runat="server" Width="689" Height="17" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="True" Font-Size="8.25" Font-Name="Arial" BackColor="Transparent"
				Text="Currency Units in the SAM are"></asp:label>
			<asp:label id="Label1" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 441px"
				tabIndex="12" runat="server" Width="500" Height="16" ForeColor="ControlText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="8.25" Font-Name="Arial"
				BackColor="Transparent" Text="Double click on any numerical value for options.">Goods and Services flow from suppliers to users; Currency flows from users to suppliers.</asp:label>
			<asp:label id="lblInstructions" style="Z-INDEX: 104; LEFT: 16px; POSITION: absolute; TOP: 457px"
				tabIndex="12" runat="server" Width="265" Height="16" ForeColor="ControlText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="8.25" Font-Name="Arial"
				BackColor="Transparent" Text="Double click on any numerical value for options.">Click on any numerical value for options.</asp:label>
			<asp:datagrid id="dbgSAM" style="Z-INDEX: 115; LEFT: 0px; POSITION: absolute; TOP: 128px" tabIndex="13"
				runat="server" Width="720px" Height="248px" BackColor="White" BorderStyle="None" BorderColor="#999999"
				BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False">
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="Categories" HeaderText="Categories"></asp:BoundColumn>
					<asp:ButtonColumn DataTextField="Sectors" HeaderText="Sectors" CommandName="Sectors"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="Household" HeaderText="Household" CommandName="Household"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="Firms" HeaderText="Firms" CommandName="Firms"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="Capital" HeaderText="Capital" CommandName="Capital"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="Government" HeaderText="Government" CommandName="Government"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="ROW" HeaderText="ROW" CommandName="ROW"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="Total" HeaderText="Total" CommandName="Total"></asp:ButtonColumn>
					<asp:ButtonColumn DataTextField="Environment" HeaderText="Environment" CommandName="Environment"></asp:ButtonColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid><TWC:ADVWEBGRID id="grdSAMDet" style="Z-INDEX: 110; LEFT: 0px; POSITION: absolute; TOP: 126px" runat="server" CssClass="expandedSamGrid" 
				Width="718px" Height="243px" Visible="False" BackColor="White" RowCount="7" TotalRows="7" HeaderVisible="False"
				FooterVisible="False">
				<Images Cancel="" ImgCheckBoxHint="" Post="" NodeClose="" ActiveRow="" EditHint="Edit" ImgCheckBoxChecked=""
					ImgCheckBoxCheckedHint="" PostHint="Post" NodeCloseHint="Close Detail" NodeOpenHint="Open Detail"
					ImgCheckBox="" Edit="" AlwaysEditRow="" CancelHint="Cancel" NodeOpen=""></Images>
				<ClientEvents ButtonClick="" ImageClick="" DynComboChange="" DynEditDone="" NodeClick="" CellClick=""
					EditDone="" DynCheckClick="" DynEditChange="" ComboChange=""></ClientEvents>
				<Scroll Enable="False" Scrollbar3DLightColor="" ScrollbarShadowColor="" ScrollbarFaceColor=""
					ScrollbarTrackColor="" ScrollbarHighlightColor="" ScrollbarBaseColor="" Persistent="False"
					ScrollbarArrowColor="" ScrollbarDarkshadowColor=""></Scroll>
				<DatePickerEditor DateSeparator="dsSlash" GradientDirection="gdHorizontal" GradientStartColor="White"
					GradientEndColor="AliceBlue"></DatePickerEditor>
				<Controller Borders-Collapse="Collapse" Borders-Spacing="0" Borders-ColorLight="" Borders-Color=""
					Borders-Width="1" Borders-Outer="border" Borders-ColorDark="" Borders-Inner="all" Borders-Padding="2"
					GradientOrientation="Horizontal" ImgLast="" ImgFirst="" HintPrev="" PagerType="Button" Position="None"
					Height="20" HintLast="" ImgPrev="" Caption="" ImgNext="" IndicatorFormat="" TextNext="Next"
					HintFirst="" TextLast="Last" TextFirst="First" BackColor="Control" Alignment="Left" MaxPages="5"
					IndicatorType="PageNo" RowCountSelect="False" HintNext="" Pager="PrevNextFirstLast" TextPrev="Prev"
					BackColorTo="White"></Controller>
				<GlyphImages PrevYear="" NextYear="" PrevMonth="" NextMonth=""></GlyphImages>
				<RowHeader GradientOrientation="Vertical" Visible="False" BackColor="Control" Borders-Collapse="Collapse"
					Borders-Spacing="0" Borders-ColorLight="" Borders-Color="" Borders-Width="1" Borders-Outer="border"
					Borders-ColorDark="" Borders-Inner="all" Borders-Padding="2" BackColorTo="" Font-Size="12pt"
					Font-Names="Microsoft Sans Serif" Width="25"></RowHeader>
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
						DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="1" ProgressColor="Red"
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
			</TWC:ADVWEBGRID>
            
            <asp:Label id="Lbl_File" style="Z-INDEX: 112; LEFT: 8px; POSITION: absolute; TOP: 88px" tabIndex="11"
				runat="server" Width="81" Height="21" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" BackColor="Window"
				Text="Select File:"></asp:Label>
			<asp:Label id="Lblyear" style="Z-INDEX: 113; LEFT: 384px; POSITION: absolute; TOP: 64px" tabIndex="9"
				runat="server" Width="80px" Height="17px" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="True" Font-Size="9.75pt" Font-Name="Arial" BackColor="Window"
				Text="Select Year" Font-Names="Arial"></asp:Label>
			<asp:Label id="lbl_countries" style="Z-INDEX: 114; LEFT: 8px; POSITION: absolute; TOP: 64px"
				tabIndex="8" runat="server" Width="144px" Height="17px" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75pt" Font-Name="Arial"
				BackColor="Window" Text="Countries or Regions" Font-Names="Arial"></asp:Label>

            <asp:dropdownlist id="cboFiles" style="Z-INDEX: 102; LEFT: 160px; POSITION: absolute; TOP: 88px" tabIndex="10"
				runat="server" Width="329" Height="22" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="Window" AutoPostBack="True"></asp:dropdownlist>
			<asp:DropDownList id="Cmb_Year" style="Z-INDEX: 103; LEFT: 472px; POSITION: absolute; TOP: 64px" tabIndex="7"
				runat="server" Width="89" Height="22" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="Window" AutoPostBack="True"></asp:DropDownList>
			<asp:DropDownList id="cmb_Country" style="Z-INDEX: 106; LEFT: 160px; POSITION: absolute; TOP: 64px"
				tabIndex="6" runat="server" Width="208px" Height="22" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
				BackColor="Window"></asp:DropDownList>

			<asp:RadioButton id="optValues" style="Z-INDEX: 109; LEFT: 592px; POSITION: absolute; TOP: 72px"
				tabIndex="5" runat="server" Width="57" Height="17" Visible="False" ForeColor="ControlText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
				BackColor="Window" Text="&amp;Values" AutoPostBack="True" GroupName="Frame1" Checked="True"></asp:RadioButton>
			<asp:RadioButton id="optLabels" style="Z-INDEX: 111; LEFT: 664px; POSITION: absolute; TOP: 72px"
				tabIndex="4" runat="server" Width="57" Height="17" Visible="False" ForeColor="ControlText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
				BackColor="Window" Text="&amp;Labels" AutoPostBack="True" GroupName="Frame1"></asp:RadioButton>
			
			<asp:Button id="cmdContinueSam" runat="server" Text="Continue with SAM" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 88px"
				Visible="False"></asp:Button>
			<twc:AdvWebGrid id="grdSAMDetDesc" style="Z-INDEX: 116; LEFT: 392px; POSITION: absolute; TOP: 400px"
				runat="server" Height="32px" Width="72px" Visible="False">
				<Images Cancel="" ImgCheckBoxHint="" Post="" NodeClose="" ActiveRow="" EditHint="Edit" ImgCheckBoxChecked=""
					ImgCheckBoxCheckedHint="" PostHint="Post" NodeCloseHint="Close Detail" NodeOpenHint="Open Detail"
					ImgCheckBox="" Edit="" AlwaysEditRow="" CancelHint="Cancel" NodeOpen=""></Images>
				<ClientEvents ButtonClick="" ImageClick="" DynComboChange="" DynEditDone="" NodeClick="" CellClick=""
					EditDone="" DynCheckClick="" DynEditChange="" ComboChange=""></ClientEvents>
				<Scroll Enable="False" Scrollbar3DLightColor="" ScrollbarShadowColor="" ScrollbarFaceColor=""
					ScrollbarTrackColor="" ScrollbarHighlightColor="" ScrollbarBaseColor="" Persistent="False"
					ScrollbarArrowColor="" ScrollbarDarkshadowColor=""></Scroll>
				<DatePickerEditor DateSeparator="dsSlash" GradientDirection="gdHorizontal" GradientStartColor="White"
					GradientEndColor="AliceBlue"></DatePickerEditor>
				<Controller Borders-Collapse="Collapse" Borders-Spacing="0" Borders-ColorLight="LightGray" Borders-Color="LightGray"
					Borders-Width="1" Borders-Outer="border" Borders-ColorDark="LightGray" Borders-Inner="all"
					Borders-Padding="2" GradientOrientation="Horizontal" ImgLast="" ImgFirst="" HintPrev=""
					PagerType="Button" Position="Top" Height="20" HintLast="" ImgPrev="" Caption="" ImgNext=""
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
			</twc:AdvWebGrid>
			<twc:AdvWebGrid id="grdSAMDetPrn" style="Z-INDEX: 117; LEFT: 576px; POSITION: absolute; TOP: 408px"
				runat="server" Height="48px" Width="112px" Visible="False">
				<Images Cancel="" ImgCheckBoxHint="" Post="" NodeClose="" ActiveRow="" EditHint="Edit" ImgCheckBoxChecked=""
					ImgCheckBoxCheckedHint="" PostHint="Post" NodeCloseHint="Close Detail" NodeOpenHint="Open Detail"
					ImgCheckBox="" Edit="" AlwaysEditRow="" CancelHint="Cancel" NodeOpen=""></Images>
				<ClientEvents ButtonClick="" ImageClick="" DynComboChange="" DynEditDone="" NodeClick="" CellClick=""
					EditDone="" DynCheckClick="" DynEditChange="" ComboChange=""></ClientEvents>
				<Scroll Enable="False" Scrollbar3DLightColor="" ScrollbarShadowColor="" ScrollbarFaceColor=""
					ScrollbarTrackColor="" ScrollbarHighlightColor="" ScrollbarBaseColor="" Persistent="False"
					ScrollbarArrowColor="" ScrollbarDarkshadowColor=""></Scroll>
				<DatePickerEditor DateSeparator="dsSlash" GradientDirection="gdHorizontal" GradientStartColor="White"
					GradientEndColor="AliceBlue"></DatePickerEditor>
				<Controller Borders-Collapse="Collapse" Borders-Spacing="0" Borders-ColorLight="LightGray" Borders-Color="LightGray"
					Borders-Width="1" Borders-Outer="border" Borders-ColorDark="LightGray" Borders-Inner="all"
					Borders-Padding="2" GradientOrientation="Horizontal" ImgLast="" ImgFirst="" HintPrev=""
					PagerType="Button" Position="Top" Height="20" HintLast="" ImgPrev="" Caption="" ImgNext=""
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
			</twc:AdvWebGrid>
			<script language="javascript">
{
	if ((document.forms[0].cmb_Country != null)&& (document.forms[0].cmb_Country.disabled == false ) ) 
          {
               document.forms[0].cmb_Country.focus();
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
    //jam 2021/09/07
    var grp = "Group=" + doc.cmb_Country[doc.cmb_Country.selectedIndex].text;

    //var grp =  doc.cmb_Country[doc.cmb_Country.selectedIndex].text;
    //jam 2021/09/07
    window.location.search = grp;

     //window.location.href = grp;



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