<%@ Reference Page="~/ScatVarS.aspx" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_computations" Codebehind="frm_computations.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
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
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body BgColor="white" LeftMargin="0" RightMargin="0" topmargin="0">
		<form runat="server">
			<TWC:ADVWEBGRID id="grdVars" style="Z-INDEX: 100; LEFT: 16px; POSITION: absolute; TOP: 200px" tabIndex="1"
				runat="server" Height="220px" Width="336px" BorderStyle="Outset" MouseSelect="SingleCell" RowCount="0"
				TotalRows="23" HeaderVisible="True" GridBorders-Padding="2" GridBorders-Inner="all" GridBorders-ColorDark="LightGray"
				GridBorders-Outer="border" GridBorders-Width="1" GridBorders-Color="LightGray" GridBorders-ColorLight="LightGray"
				GridBorders-Spacing="0" GridBorders-Collapse="Collapse" FooterVisible="False" HeaderBorders-Collapse="Collapse"
				HeaderBorders-Spacing="0" HeaderBorders-ColorLight="LightGray" HeaderBorders-Color="LightGray"
				HeaderBorders-Width="1" HeaderBorders-Outer="border" HeaderBorders-ColorDark="LightGray" HeaderBorders-Inner="all"
				HeaderBorders-Padding="2" RowHeight="0" DetailRowHeight="100" Visible="False" ActiveRowColor="128, 128, 255">
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
						PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title=" CodeVar" FooterGradientOrientation="Vertical"
						HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" HeaderFont-Bold="True" DynPrecision="2"
						HeaderColorTo="" Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text"
						Width="100" DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0"
						ProgressColor="Red" DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
					<twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
						TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
						ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
						SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
						SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
						ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
						PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="VarName" FooterGradientOrientation="Vertical"
						HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" HeaderFont-Bold="True" DynPrecision="2"
						HeaderColorTo="" Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text"
						Width="100" DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0"
						ProgressColor="Red" DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
				</Columns>
				<SortSettings Ascending="" Direction="Ascending" Column="-1" Show="True" InitDirection="Ascending"
					Descending=""></SortSettings>
			</TWC:ADVWEBGRID>
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Computations" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:Button runat="server" STYLE="Z-INDEX:101; LEFT:208px; POSITION:absolute; TOP:103px; TEXT-ALIGN:center"
				Text="(E)dit Names" accessKey="E" Enabled="False" Width="81" Height="33" TabIndex="5" Visible="False"
				Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Control" CausesValidation="True" ForeColor="ControlText"
				ID="cmdEdit"></asp:Button>
			<asp:Button runat="server" STYLE="Z-INDEX:102; LEFT:120px; POSITION:absolute; TOP:103px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="81" Height="33" TabIndex="4" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>
			<asp:Button runat="server" STYLE="Z-INDEX:103; LEFT:16px; POSITION:absolute; TOP:103px; TEXT-ALIGN:center"
				Text="(S)elect Vars." accessKey="S" Width="81" Height="33" TabIndex="3" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				BackColor="Control" CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdSelect"></asp:Button>
			<asp:TextBox runat="server" Width="209" Height="25" STYLE="Z-INDEX:104; LEFT:16px; POSITION:absolute; TOP:72px; TEXT-ALIGN:left"
				TabIndex="2" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtFormula" AutoPostBack="False"></asp:TextBox>
			<asp:Label runat="server" BackColor="Transparent" Text="Example:      a/b" Width="80px" Height="13"
				STYLE="Z-INDEX:105; LEFT:16px; POSITION:absolute; TOP:55px; TEXT-ALIGN:left" TabIndex="9"
				Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None"
				ID="lblExample"></asp:Label>
			<asp:Label runat="server" Text="Units?" Width="30" Height="13" STYLE="Z-INDEX:106; LEFT:120px; POSITION:absolute; TOP:143px; TEXT-ALIGN:left"
				TabIndex="7" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblUnits"></asp:Label>
			<asp:Label runat="server" Text="Dimensionality?" Width="73" Height="13" STYLE="Z-INDEX:107; LEFT:208px; POSITION:absolute; TOP:143px; TEXT-ALIGN:left"
				TabIndex="6" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblDimensionality"></asp:Label>
			<asp:Label runat="server" Text="Formula Name?" Width="74" Height="13" STYLE="Z-INDEX:108; LEFT:16px; POSITION:absolute; TOP:143px; TEXT-ALIGN:left"
				TabIndex="3" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblFormulaName"></asp:Label>
			<asp:Label runat="server" BackColor="Transparent" Text="Type your formula using any variable names"
				Width="264px" Height="13" STYLE="Z-INDEX:109; LEFT:16px; POSITION:absolute; TOP:40px; TEXT-ALIGN:left"
				TabIndex="1" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None"
				ID="lblFormula"></asp:Label>
			<input id="btnHelp" onclick="javascript:ShowHelp();"
                        type="button" value="Help" style="width: 88px; z-index: 112; left: 372px; position: absolute; top: 50px;" />
                        <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16051);-->
			<asp:Panel id="Panel1" style="Z-INDEX: 110; LEFT: 16px; POSITION: absolute; TOP: 35px; height: 296px;" runat="server" Width="344px" BackColor="White" Visible="False">
				<P>&nbsp;</P>
				<P>
				&nbsp;</P>
				<P>
					<TABLE>
						<TR>
							<TD>
								<asp:Label id="Label3" style="TEXT-ALIGN: left" tabIndex="5" runat="server" Visible="True"
									BorderStyle="None" Width="86px" Height="13" ForeColor="ControlText" BackColor="Transparent"
									Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
									Font-Name="Arial" Enabled="True" Text="Formula Name:">Formula Name:</asp:Label></TD>
							<TD>
								<asp:TextBox id="txtFormulaName" style="TEXT-ALIGN: left" tabIndex="9" runat="server" Visible="True"
									BorderStyle="Inset" Width="177" Height="25" ForeColor="WindowText" BackColor="Window" Font-Underline="False"
									Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
									Enabled="True" AutoPostBack="False" Textmode="Singleline" MaxLength="0" ReadOnly="False"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label2" style="TEXT-ALIGN: left" tabIndex="5" runat="server" Visible="True"
									BorderStyle="None" Width="86px" Height="29" ForeColor="ControlText" BackColor="Transparent"
									Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
									Font-Name="Arial" Enabled="True" Text="Dimension Name (optional):"></asp:Label></TD>
							<TD>
								<asp:TextBox id="txtDimensionality" style="TEXT-ALIGN: left" tabIndex="10" runat="server" Visible="True"
									BorderStyle="Inset" Width="177" Height="25" ForeColor="WindowText" BackColor="Window" Font-Underline="False"
									Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
									Enabled="True" AutoPostBack="False" Textmode="Singleline" MaxLength="0" ReadOnly="False"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label1" style="TEXT-ALIGN: left" tabIndex="6" runat="server" Visible="True"
									BorderStyle="None" Width="88" Height="37" ForeColor="ControlText" BackColor="Transparent"
									Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
									Font-Name="Arial" Enabled="True" Text="Units Name (optional):"></asp:Label></TD>
							<TD>
								<asp:TextBox id="txtUnits" style="TEXT-ALIGN: left" tabIndex="11" runat="server" Visible="True"
									BorderStyle="Inset" Width="177" Height="25" ForeColor="WindowText" BackColor="Window" Font-Underline="False"
									Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
									Enabled="True" AutoPostBack="False" Textmode="Singleline" MaxLength="0" ReadOnly="False"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD colSpan="2">
								<asp:Button id="cmdOk" style="TEXT-ALIGN: center" accessKey="O" tabIndex="12" runat="server"
									Width="81" Height="33" ForeColor="ControlText" CausesValidation="True" BackColor="Control"
									Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
									Font-Name="Arial" Enabled="True" Text="(O)k"></asp:Button></TD>
						</TR>
					</TABLE>
				</P>
			</asp:Panel>

			<script language="javascript">

{
	if ((document.forms[0].txtFormula != null)&& (document.forms[0].txtFormula.disabled == false ) ) 
          {
               document.forms[0].txtFormula.focus();
          }
    else
        {
            if ((document.forms[0].cmdSelect != null)&& (document.forms[0].cmdSelect.disabled == false ) ) 
            {
                document.forms[0].cmdSelect.focus();
            }
        }

	if ((document.forms[0].txtFormulaName != null)&& (document.forms[0].txtFormulaName.disabled == false ) ) 
          {
               document.forms[0].txtFormulaName.focus();
          }
    else
        {
            if ((document.forms[0].cmdOk != null)&& (document.forms[0].cmdOk.disabled == false ) ) 
            {
                document.forms[0].cmdOk.focus();
            }        
        }
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
			</script>

            &nbsp; &nbsp;
            <asp:CheckBox ID="chkNullsAsZeros" runat="server" AccessKey="n" Style="z-index: 99;
                left: 16px; position: absolute; top: 168px" Text="Use (N)ulls as Zeros" />
            <aiswc:FocusControl ID="WinFormsToWeb_FocusControl" runat="server" style="Z-INDEX: 110; LEFT: 472px; POSITION: absolute; TOP: 104px"></aiswc:FocusControl>                
		</form>
	</body>
</HTML>
