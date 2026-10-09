<%@ Reference Page="~/Index.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_DisplayMenu" Codebehind="frm_DisplayMenu.aspx.vb" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
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
        <!--jam 2021-11-27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form runat="server">
			<asp:Label id="lblMsg" style="Z-INDEX: 114; LEFT: 48px; POSITION: absolute; TOP: 88px" runat="server"
				Visible="False" ForeColor="Red" Font-Bold="True">Label</asp:Label>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Display Menu" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<asp:Panel id="pnlContext" style="Z-INDEX: 100; LEFT: 40px; POSITION: absolute; TOP: 368px"
				runat="server" Width="616px" Height="160px" Visible="False" BackColor="White" BorderStyle="Outset">
<P>&nbsp;
					<asp:Label id="lblRowCol" runat="server">Label</asp:Label></P>
<P>
					<asp:LinkButton id="lbtDefine" runat="server">Define</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:LinkButton id="lbtDrivers" runat="server">Drivers</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:LinkButton id="lbtCancel" runat="server">Cancel</asp:LinkButton></P>
<P>
					<asp:LinkButton id="lbtDelete" runat="server">Delete</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<A href="#1">
						<asp:Label id="lblViewEquations" runat="server" ForeColor="Blue" Font-Underline="True">View Equations</asp:Label></A></P>
					<P><A href="#1">
						<asp:Label id="lblExplain" runat="server" ForeColor="Blue" Font-Underline="True">Explain</asp:Label>
				</A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:LinkButton id="lbtViewCode" runat="server">View Code</asp:LinkButton></P>
			</asp:Panel>
			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 113; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
				
			<asp:textbox id="Txt_PieYear" style="Z-INDEX: 103; LEFT: 480px; POSITION: absolute; TOP: 291px; TEXT-ALIGN: left"
				tabIndex="7" runat="server" Height="25" Width="89" AutoPostBack="False" BorderStyle="Solid" Textmode="Singleline" MaxLength="0" ReadOnly="False" ForeColor="WindowText" Enabled="True" BackColor="Window"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Visible="False" Text="Text1"></asp:textbox><asp:textbox id="txt_TableTitle" style="Z-INDEX: 104; LEFT: 40px; POSITION: absolute; TOP: 291px; TEXT-ALIGN: left"
				tabIndex="6" runat="server" Height="25" Width="273" AutoPostBack="False" BorderStyle="Solid" Textmode="Singleline" MaxLength="0" ReadOnly="False" ForeColor="WindowText" Enabled="True" BackColor="Window" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Visible="False" Text="TableTitle"></asp:textbox><asp:textbox id="Txt_Interval" style="Z-INDEX: 105; LEFT: 360px; POSITION: absolute; TOP: 291px; TEXT-ALIGN: left"
				tabIndex="5" runat="server" Height="25" Width="89" AutoPostBack="False" BorderStyle="Solid" Textmode="Singleline" MaxLength="0" ReadOnly="False" ForeColor="WindowText" Enabled="True" BackColor="Window"
				Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Visible="False" Text="Text1"></asp:textbox>
				
				<asp:panel id="frm_Horizon" style="Z-INDEX: 1000; LEFT: 504px; POSITION: absolute; TOP: 112px"
				tabIndex="3" runat="server" Height="172" Width="300" ForeColor="WindowText" Enabled="True" BackColor="Window" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Visible="False">
				<asp:TextBox ID="txtHorizon" runat="server"></asp:TextBox><asp:Button ID="cmdHorizon"
                    runat="server" Text="Submit" /><br />
				<asp:DropDownList id="cmb_Horizon" tabIndex="4" runat="server"
					Font-Bold="False" ForeColor="WindowText" Visible="True" Width="153" Height="129" BackColor="Window"
					Font-Underline="False" Font-Name="Arial" Font-Size="8.0" Font-Italic="False" Font-Strikeout="False"
					Enabled="True" AutoPostBack="True"></asp:DropDownList>
			</asp:panel><asp:label id="lbl_PieYear" style="Z-INDEX: 107; LEFT: 480px; POSITION: absolute; TOP: 259px; TEXT-ALIGN: left"
				tabIndex="10" runat="server" Height="33" Width="113" BorderStyle="None" ForeColor="WindowText" Enabled="True"
				BackColor="Window" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8.0" Font-Name="Arial" Visible="False" Text="Label1"></asp:label><asp:label id="lbl_TableTitle" style="Z-INDEX: 108; LEFT: 40px; POSITION: absolute; TOP: 267px; TEXT-ALIGN: left"
				tabIndex="9" runat="server" Height="17" Width="273" BorderStyle="None" ForeColor="WindowText" Enabled="True" BackColor="Window" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Visible="False" Text="lbl_TableTitle"></asp:label><asp:label id="lbl_interval" style="Z-INDEX: 109; LEFT: 360px; POSITION: absolute; TOP: 259px; TEXT-ALIGN: left"
				tabIndex="8" runat="server" Height="25" Width="97" BorderStyle="None" ForeColor="WindowText" Enabled="True" BackColor="Window" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
				Font-Name="Arial" Visible="False" Text="Label1"></asp:label><asp:label id="Lbl_display" style="Z-INDEX: 110; LEFT: 48px; POSITION: absolute; TOP: 128px; TEXT-ALIGN: left"
				tabIndex="3" runat="server" Height="121px" Width="561" BorderStyle="None" ForeColor="WindowText" BackColor="Window" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" Visible="True" Text="Display Explanation" BorderColor="Transparent">Display Explanation</asp:label><asp:label id="lbl_CurSelect" style="Z-INDEX: 111; LEFT: 48px; POSITION: absolute; TOP: 331px; TEXT-ALIGN: left"
				tabIndex="2" runat="server" Height="25" Width="545" BorderStyle="None" ForeColor="WindowText" Enabled="True" BackColor="Window" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Visible="True" Text="Label1"></asp:label><aiswc:focuscontrol id="WinFormsToWeb_FocusControl" style="Z-INDEX: 112; LEFT: 600px; POSITION: absolute; TOP: 280px"
				runat="server"></aiswc:focuscontrol>
			<TWC:AdvWebGrid id="grd_VariableDisplay" style="Z-INDEX: 99; LEFT: 40px; POSITION: absolute; TOP: 368px"
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
			</TWC:AdvWebGrid>
			
			<input id="txtWidth" name="txtWidth" type="hidden" runat="server"/><input id="txtHeight" name="txtHeight" type="hidden" runat="server"/>          				

            <script language="javascript" type="text/javascript" >
            //jrs 2012/03/01
            //Try to find size of browser window, and not use screen
            //var hp  = (screen.height*54.5)/100;
            //var wp  = (screen.width*94.92)/100;
            //document.getElementById('txtResValueHolder').value=wp+","+hp;
            var myWidth = 0, myHeight = 0;
            if( typeof( window.innerWidth ) == 'number' ) {
                //Non-IE
                myWidth = window.innerWidth;
                myHeight = window.innerHeight;
              } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
                //IE 6+ in 'standards compliant mode'
                myWidth = document.documentElement.clientWidth;
                myHeight = document.documentElement.clientHeight;
              } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
                //IE 4 compatible
                myWidth = document.body.clientWidth;
                myHeight = document.body.clientHeight;
              }
              //jrs 2011/03/22
              if (myWidth == 0)
              {
                myWidth = screen.width;
                myHeight = screen.height;              
              }
                            
              myWidth = myWidth * 0.95;
              myHeight = myHeight * 0.75;

              document.getElementById('txtWidth').value=myWidth;        
              document.getElementById('txtHeight').value=myHeight;        
                              
              
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
