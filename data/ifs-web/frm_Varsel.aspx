<%@ Reference Page="~/Index.aspx" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Register TagPrefix="cc2" Namespace="CustomDropDownList.Thycotic.Web.UI.WebControls" Assembly="CustomDropDownList" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_VarSel" Codebehind="frm_Varsel.aspx.vb" EnableEventValidation="false"%>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</script>
		<script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>

         <!--JAM 2014-04-04-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSel_Vars.js"></script>

        <!--JAM 2013-04-04-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSelFx.js"></script>
        
          <!--JAM 2014-04-15-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSel_ForeVars.js"></script>

        <!--jrs 2023-05-19-->
        <!-- <script language ="javascript" type="text/javascript" src="Scripts/VarSel_VarLists.js"></!script>
		<script language ="javascript" type="text/javascript" src="Scripts/VarSel_VarListsH.js"></script>
		<script language ="javascript" type="text/javascript" src="Scripts/VarSel_VarListsHF.js"></script>-->

        <!--JAM 2013-04-15-->
        <script language ="javascript" type="text/javascript" src="Scripts/VarSelForeFx.js"></script>  
        
        <%--/JAM 2011-10-15 Browser detection for properly navigation/--%>
        <script type="text/javascript">
			var BrowserDetect = {
			init: function () {
				this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
				this.version = this.searchVersion(navigator.userAgent)
					|| this.searchVersion(navigator.appVersion)
					|| "an unknown version";
				this.OS = this.searchString(this.dataOS) || "an unknown OS";
			},
			searchString: function (data) {
				for (var i=0;i<data.length;i++)	{
					var dataString = data[i].string;
					var dataProp = data[i].prop;
					this.versionSearchString = data[i].versionSearch || data[i].identity;
					if (dataString) {
						if (dataString.indexOf(data[i].subString) != -1)
							return data[i].identity;
					}
					else if (dataProp)
						return data[i].identity;
				}
			},
			searchVersion: function (dataString) {
				var index = dataString.indexOf(this.versionSearchString);
				if (index == -1) return;
				return parseFloat(dataString.substring(index+this.versionSearchString.length+1));
			},
			dataBrowser: [
				{
					string: navigator.userAgent,
					subString: "Chrome",
					identity: "Chrome"
				},
				{ 	string: navigator.userAgent,
					subString: "OmniWeb",
					versionSearch: "OmniWeb/",
					identity: "OmniWeb"
				},
				{
					string: navigator.vendor,
					subString: "Apple",
					identity: "Safari"
				},
				{
					prop: window.opera,
					identity: "Opera"
				},
				{
					string: navigator.vendor,
					subString: "iCab",
					identity: "iCab"
				},
				{
					string: navigator.vendor,
					subString: "KDE",
					identity: "Konqueror"
				},
				{
					string: navigator.userAgent,
					subString: "Firefox",
					identity: "Firefox"
				},
				{
					string: navigator.vendor,
					subString: "Camino",
					identity: "Camino"
				},
				{		// for newer Netscapes (6+)
					string: navigator.userAgent,
					subString: "Netscape",
					identity: "Netscape"
				},
				{
					string: navigator.userAgent,
					subString: "MSIE",
					identity: "Explorer",
					versionSearch: "MSIE"
				},
				{
					string: navigator.userAgent,
					subString: "Gecko",
					identity: "Mozilla",
					versionSearch: "rv"
				},
				{ 		// for older Netscapes (4-)
					string: navigator.userAgent,
					subString: "Mozilla",
					identity: "Netscape",
					versionSearch: "Mozilla"
				}
			],
			dataOS : [
				{
					string: navigator.platform,
					subString: "Win",
					identity: "Windows"
				},
				{
					string: navigator.platform,
					subString: "Mac",
					identity: "Mac"
				},
				{
					string: navigator.platform,
					subString: "Linux",
					identity: "Linux"
				}
			]

		};
		BrowserDetect.init();
	</script>
		
			
		
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!-- jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>	
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="initVarSel();initVarSelEmp();initVarSelList();">
		<form runat="server" >
			<script type="text/javascript">
				function checkNewGListName() {
					var sNewGList = prompt("Please enter the name for the new GList", "");
					if (sNewGList == "") {
						alert("Please enter a name for the G-List");
						return false;
					}
                    txtGList.value = sNewGList
					return true;
				}
            </script>

		    <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
			<input id="txtGList" name="txtGList"  type="hidden" runat="server"/>
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Full Variable/Parameter Selection" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 116; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" Height="48px" Width="88px" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>			
            &nbsp;
			

            
            &nbsp;&nbsp;
            
				
				<asp:panel id="FrameProgress" style="Z-INDEX: 105; LEFT: 168px; POSITION: absolute; TOP: -109px"
				tabIndex="8" runat="server" Height="105" Width="337" Visible="False" BackColor="Window" Enabled="True" Font-Underline="False" ForeColor="WindowText" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8.0" Font-Name="Arial">
				<asp:Label id="ProgressBar1" style="LEFT: 16px; POSITION: absolute; TOP: 40px" tabIndex="9"
					runat="server" Width="305" Height="25" BackColor="Red"></asp:Label>
			</asp:panel>
            

                <table>
                    <tr>
                        <td width=28px>
                        </td>                        
                        <td>
                            <asp:Panel ID="frmGroupOpt" runat="server" Height="32px" Width="608px">
                                <asp:RadioButton ID="optReg" runat="server" AccessKey="r"  Checked="True"
                                    Text="Country" 
                                    GroupName="GroupingOptions" ToolTip="Loads single Country (Alt+R)" 
									onclick="EnableComputation();" />
                                <asp:RadioButton ID="optGroup" runat="server" AccessKey="g"  Text="Groups" 
                                    GroupName="GroupingOptions" ToolTip="Loads single Group (Alt+G)" 
									onclick="EnableComputation();" />
                                <asp:RadioButton ID="optGroupDec" runat="server" AccessKey="e" 
                                    Text="Decomposed Groups" GroupName="GroupingOptions" ToolTip="Decomposes a Group into its Member Countries (Alt+E)" AutoPostBack="False" 
									onclick="DisableComputation();"/>
                                <asp:RadioButton ID="optGList" runat="server" AccessKey="l"  Text="Geography-Lists" 
                                    GroupName="GroupingOptions" ToolTip="Loads a predefined list of Countries and/or Groups (Alt+L)" 
									onclick="DisableComputation();"/>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:label id="lbl_VarName" 
				            tabIndex="3" runat="server" Height="17px" Width="473" BorderStyle="None" Visible="True" BackColor="Window" Enabled="True" Font-Underline="False" ForeColor="WindowText" Text="Label1" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial">Label1</asp:label>                            
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:TextBox ID="txtVarSearch" runat="server" Width="606px" autocomplete="off"></asp:TextBox>                            
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:listbox id="lstVarName" 
				            runat="server" Height="88px" Width="608px" ></asp:listBox>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px></td>
                        <td>
                            <asp:label id="lbl_Dim1" 
				            tabIndex="4" runat="server" Height="17" Width="273" BorderStyle="None" Visible="True" 
                            BackColor="Window" Enabled="True" Font-Underline="False" ForeColor="WindowText"
				            Text="Label2" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" 
                            Font-Size="8.0" Font-Name="Arial"></asp:label><br />
                            <cc2:customdropdownlist id="cmb_DIM1" 
				            tabIndex="2" runat="server" Height="24px" Width="197px" Visible="False"></cc2:customdropdownlist><br />
                            <asp:listbox id="lstDIM1" runat="server"
				            Height="88px" Width="200px" Visible="False" AutoPostBack="True" Rows="8"></asp:listbox>
                        </td>
                    </tr>
                </table>	
                <table>
                    <tr>
                        <td width=28px>
                        </td>
                        <td>
                            <asp:label id="lbl_files" 
				            tabIndex="5" runat="server" Height="17" Width="336px" BorderStyle="None" Visible="True" BackColor="Window" Enabled="True" Font-Underline="False" ForeColor="WindowText" Text="Default File for Display:" Font-Strikeout="False" Font-Italic="False"
				            Font-Bold="False" Font-Size="8.0" Font-Name="Arial"></asp:label>
                        </td>
                        <td>
                            <asp:label id="lbl_vcat" 
				            tabIndex="6" runat="server" Height="17px" Width="249px" BorderStyle="None" BackColor="Window" Font-Underline="False"
				            ForeColor="WindowText" Text="Label1" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8pt" Font-Name="Arial" Font-Names="Arial"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td rowspan=4>
                            <asp:listbox id="lstFiles" runat="server"
				            Height="100px" Width="336px" SelectionMode="Multiple"></asp:listbox>
                        </td>
                        <td>
                            <asp:dropdownlist id="cmb_VCAT"
				            tabIndex="7" runat="server" Width="249" Visible="True" BackColor="Window" Enabled="True" Font-Underline="False"
				            ForeColor="WindowText" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" ></asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td width=28px>
                        </td>
                        <td colspan=2>
                            <TWC:ADVWEBGRID id="grd_VariableDisplay" 
				                runat="server" Height="144px" Width="608px" BorderStyle="Outset" Visible="False" BackColor="White" ForeColor="Black" BorderColor="DarkGray" MouseSelect="SingleCell" RowCount="5" HeaderBorders-Padding="0" HeaderBorders-Inner="all"
				                HeaderBorders-ColorDark="LightGray" HeaderBorders-Outer="border" HeaderBorders-Width="0" HeaderBorders-Color="LightGray" HeaderBorders-ColorLight="LightGray" HeaderBorders-Spacing="0" HeaderBorders-Collapse="Collapse" GridBackColorTo="ActiveBorder"
				                GridBackColor="Silver" FooterBorders-Padding="0" FooterBorders-Inner="none" FooterBorders-ColorDark="LightGray" FooterBorders-Outer="border" FooterBorders-Width="0" FooterBorders-Color="LightGray" FooterBorders-ColorLight="LightGray"
				                FooterBorders-Spacing="0" FooterBorders-Collapse="Collapse" TotalRows="5" HeaderVisible="False" GridBorders-Padding="2" GridBorders-Inner="all" GridBorders-ColorDark="LightGray" GridBorders-Outer="border" GridBorders-Width="0"
				                GridBorders-Color="LightGray" GridBorders-ColorLight="Transparent" GridBorders-Spacing="0" GridBorders-Collapse="Separate" FooterVisible="False">
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
					                Borders-Width="0" Borders-Outer="border" Borders-ColorDark="LightGray" Borders-Inner="all"
					                Borders-Padding="0" GradientOrientation="Horizontal" ImgLast="" ImgFirst="" HintPrev=""
					                PagerType="Button" Position="None" Height="20" HintLast="" ImgPrev="" Caption="" ImgNext=""
					                IndicatorFormat="" TextNext="Next" HintFirst="" TextLast="Last" TextFirst="First" BackColor="Control"
					                Alignment="Left" MaxPages="5" IndicatorType="PageNo" RowCountSelect="False" HintNext=""
					                Pager="PrevNextFirstLast" TextPrev="Prev" BackColorTo="White"></Controller>
				                <GlyphImages PrevYear="" NextYear="" PrevMonth="" NextMonth=""></GlyphImages>
				                <RowHeader GradientOrientation="Vertical" Visible="False" BackColor="Control" Borders-Collapse="Collapse"
					                Borders-Spacing="0" Borders-ColorLight="LightGray" Borders-Color="LightGray" Borders-Width="0"
					                Borders-Outer="border" Borders-ColorDark="LightGray" Borders-Inner="all" Borders-Padding="0"
					                BackColorTo="" Width="0"></RowHeader>
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
						                Color="White" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text"
						                Width="33" DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0"
						                ProgressColor="Red" DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
					                <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
						                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
						                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
						                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
						                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
						                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
						                PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="" FooterGradientOrientation="Vertical"
						                HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
						                Color="White" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text"
						                Width="33" DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0"
						                ProgressColor="Red" DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
					                <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
						                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
						                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
						                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
						                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
						                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
						                Font-Bold="True" PopupWidth="200" DataButtonType="Link" WidthType="Percent" Title="" FooterGradientOrientation="Vertical"
						                HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
						                Color="White" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text"
						                Width="33" DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0"
						                ProgressColor="Red" DynCheckFalse="0" Alignment="Left" DataField="" ButtonText=""></twc:Column>
				                </Columns>
				                <SortSettings Ascending="" Direction="Ascending" Column="-1" Show="False" InitDirection="Ascending"
					                Descending=""></SortSettings>
			                </TWC:ADVWEBGRID>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td colspan=2>
                            <asp:panel id="pnlContext" 
				                runat="server" Width="700px" BorderStyle="Outset" Visible="False" BackColor="White">
				                <P>&nbsp;
					                <asp:Label id="lblRowCol" runat="server">Label</asp:Label></P>
				                <P>
					                <asp:LinkButton id="lbtDefine" runat="server">Define</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					                <asp:LinkButton id="lbtDrivers" runat="server">Drivers</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:LinkButton id="lbtViewHistAnal" runat="server">View Historic Analog Variable</asp:LinkButton>
					                </P>
				                <P>
					                <asp:LinkButton id="lbtDelete" runat="server">Delete</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					                <A href="#1">
						                <asp:Label id="lblViewEquations" runat="server" ForeColor="Blue" Font-Underline="True">View Equations</asp:Label></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					                <asp:LinkButton id="lbtCancel" runat="server">Cancel</asp:LinkButton></P>
				                <P><A href="#1">
						                <asp:Label id="lblExplain" runat="server" ForeColor="Blue" Font-Underline="True">Explain</asp:Label></A>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					                <asp:LinkButton id="lbtViewCode" runat="server">View Code</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label id="lblDesc1" runat="server">Some options involve pop-ups that your browser may block.</asp:Label></P>
				                <P>
					                <asp:LinkButton id="lbtApplyGlist" runat="server" visible="False">Apply GList</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					                <asp:RadioButton id="optEditFor" runat="server" Visible="False" GroupName="For" Checked="True" Text="Edit Formula"></asp:RadioButton>&nbsp;
					                <asp:RadioButton id="optCopyFor" runat="server" GroupName="For" Text="Copy Formula"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				                </P>
			                </asp:panel>
                        </td>
                    </tr>
                </table>			
				<table>
                    <tr>
                        <td colspan=4>
                        </td>
                    </tr>
                    <tr>
                        <td width=28px>
                        </td>
                        <td width=150px>
                            <asp:button id="cmd_Clear" 
				            accessKey="C" tabIndex="12" runat="server" Height="25" Width="113px" BackColor="Control" Enabled="True" 
				            Font-Underline="False" ForeColor="ControlText" Text="Clear Select" CausesValidation="True"
				            Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" 
                                Font-Name="Arial" ToolTip="Clear Select (Alt+C)"></asp:button>
                        </td>
                        <td width=150px>
                            <asp:button id="cmdComputations" 
				            accessKey="o" tabIndex="13" runat="server" Height="25" Width="113px" BackColor="Control" 
                                Enabled="True" Font-Underline="False" ForeColor="ControlText" 
				            Text="Computations" CausesValidation="True" Font-Strikeout="False"
				            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" 
                                ToolTip="Computations (Alt+O)"></asp:button>
                        </td>
                        <td width=150px>
                            <asp:button id="Command1" 
				            accessKey="D" tabIndex="15" runat="server" Height="25" Width="113" BackColor="Control" Enabled="True"
				            Font-Underline="False" ForeColor="ControlText" Text="Go to Display" CausesValidation="True" Font-Strikeout="False"
				            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" ToolTip="Go to Display (Alt+D)"></asp:button>
                        </td>
                        <td width=150px>
                            <asp:button id="cmdGeneratejs" 
				            accessKey="D" tabIndex="15" runat="server" Height="25" Width="113" BackColor="Control" Enabled="True"
				            Font-Underline="False" ForeColor="ControlText" Text="Generate Javascript" ToolTip="Generate Javascript (Alt+J)" CausesValidation="True" Font-Strikeout="False"
				            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" ></asp:button>
                        </td>
                    </tr>
                </table>
            
             <input ID="fSubSetchosen" runat="server" type="hidden" />	
             <input ID="fSubSetchosenName" runat="server" type="hidden" />	
             <input ID="fEmpOrForeMode" runat="server" type="hidden" />	
             <input ID="fHistoricAnalogMode" runat="server" type="hidden" />
             <input ID="fVarchosen" runat="server" type="hidden" /> <!--JAM 2015-03-25 Saving lstVarName chosen value-->
				
			
			
				 
				
 
<asp:Button ID="btnInvs" runat="server" Text="Hidden Command" />
<asp:Button ID="btnCont" runat="server" Text="Continue" />    

<input id="txtWidth" name="txtWidth" type="hidden" runat="server"/><input id="txtHeight" name="txtHeight" type="hidden" runat="server"/>          				

 
		
<script language="javascript" type="text/javascript" >
    var myWidth = 0, myHeight = 0;
    if (typeof (window.innerWidth) == 'number') {
        //Non-IE
        myWidth = window.innerWidth;
        myHeight = window.innerHeight;
    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
        //IE 6+ in 'standards compliant mode'
        myWidth = document.documentElement.clientWidth;
        myHeight = document.documentElement.clientHeight;
    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
        //IE 4 compatible
        myWidth = document.body.clientWidth;
        myHeight = document.body.clientHeight;
    }
    //jrs 2011/03/22
    if (myWidth == 0) {
        myWidth = screen.width;
        myHeight = screen.height;
    }

    //resize the variable list box
    var room = myHeight - (25 * 21.5);
    if (document.forms[0].lstVarName != null) {
        document.getElementById('lstVarName').style.height = room + "px";
    }
    if (document.forms[0].lstDIM1 != null) {
        document.getElementById('lstDIM1').style.height = room + "px";
    }

	 document.getElementById("btnInvs").style.visibility ="hidden"	;
	 document.getElementById("btnCont").style.visibility ="hidden"	;
	 
	 
	 //JAM 2011-11-18 Defining default button for txtVarSearch field
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
	 
	 
	 
	 
	 // jrs 2004/11/27
			function showhg() {
			var doc = document.forms[0];
		
			document.body.style.cursor = "wait";
			//doc.cmd_Exit.value = "Please Wait....";
			doc.cmdComputations.style.cursor = "wait";
		
			}
	
		function updateDim1Idx_onclick(e) {
		// handle combobox 
		var cmb;
		cmb = document.getElementById('cmb_DIM1');
		var lst;
		lst = document.getElementById('lstDIM1');
		  
		cmb.selectedIndex = lst.selectedIndex + 1; 
		cmb.selectedIndex = -1; 
		return true; 
	} 
	
	//JAM 2011-10-15 In IE there is an exception for code execution
	if (BrowserDetect.browser == "Explorer") 
	    {
	    
	        if ((document.forms[0].txtVarSearch.disabled == false))
                    {
                        document.forms[0].txtVarSearch.focus();
                    }
            else
                    {
                        if ((document.forms[0].cmb_DIM1 != null) && (document.forms[0].cmb_DIM1.disabled == false))
                        {
                            document.forms[0].cmb_DIM1.focus();
                            
                        }
                    }
    	
	    }
	else
	{
	 
	    
	     if ((document.getElementById('txtVarSearch').disabled==false))
            {
                null;
		       
            }
        else
           {
           
            
            if ((document.getElementById('cmb_DIM1') != null) && (document.getElementById('cmb_DIM1').disabled==false))
            {
               //JAM 2011-11-05 Changing cmb_DIM1 behavior focus
                document.forms[0].cmb_DIM1.focus();
               
            }
           
          } 
	
	}
	
	

	

	
	
	if (document.forms[0].lstDIM1 != null)
{
	if (document.forms[0].lstDIM1.captureEvents!=null)
			document.forms[0].lstDIM1.captureEvents(Event.MOUSEDOWN)
	document.forms[0].lstDIM1.onmousedown = updateDim1Idx_onclick;
}

if (document.forms[0].cmb_DIM1!=null)
{
	if (document.forms[0].cmb_DIM1.captureEvents!=null)
            document.forms[0].cmb_DIM1.captureEvents(Event.KEYPRESS)
	document.forms[0].cmb_DIM1.onkeypress = updateDIM1;
}
// JAM 2014-05-31
////if (document.forms[0].cmb_VCAT != null)
////{
////	
////	document.getElementById("cmb_VCAT").onchange = varSelCategoryChanged;
////}

	
	function setVariableFromList(){
document.getElementById("txtVarSearch").value= document.getElementById("lstVarName").value;

}

function setVariableFromSearchClick()
	         {
	        
	          document.getElementById("lstVarName").value=this.actb_currObject;
	          document.getElementById("btnInvs").click();
	            
	         }
				
	
			function showEquation(hid) {
				var hwindow = window.open("../assets/help/WebHelp/ifshelp.htm" + "#<id="+hid, "__webCshStub");
			}
			
			function showExplain(hid) {
				var sParam = "left="+screen.width+",top="+screen.height+",width=100,height=100";
				var hwindow = window.open("../assets/help/WebHelp/ifshelp.htm" + "#<id="+hid+">>wnd=" + "IFs Help", "__webCshStub", sParam);
			}
			
function selectData(){
   document.getElementById("txtVarSearch").select();
  }
	document.getElementById("txtVarSearch").onclick=selectData;	
	
      if (variableList == null){
               if (document.forms[0].lstVarName!= null)
                { 
                var lst=document.getElementById("lstVarName");
                variableList =new Array(lst.length);
               
                 for (var count = 0; count < lst.length; count++)
                 {
	                 variableList[count]=lst.options[count].innerHTML;
	                  
	             }
	            }
            }
 
  function continueFromVarSel()
  {
    document.getElementById("btnCont").click();
  }

            
             function BindDataToVarSearch()
             {     
                var obj = actb(document.getElementById('txtVarSearch'),variableList);
                //KIA add the follwing event listener with your desired method what you want to call on a search list item is clicked
                this.actb_cur_obj_clicked.prototype.itemClicked = setVariableFromSearchClick;
                this.actb_fSize = '12px';
                this.actb_bgColor = '#888888';
                this.actb_textColor = '#FFFFFF';
                this.actb_hColor = '#000000';
                this.actb_fFamily = 'Verdana';
                this.actb_lim =5;
                this.actb_timeOut = -1;
                this.actb_firstText =true;
                this.actb_hStyle = 'text-decoration:underline;font-weight="bold"';  
                document.getElementById('txtVarSearch').value="";         
                document.getElementById('txtVarSearch').select();   
                
              }
                BindDataToVarSearch();	
        	
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
              document.getElementById('txtHeight').value = myHeight;

              //////////////////////////

              //////////////JAM 2014-05-18 Adding click event for context menu

              // Function which shows the Context Menu by calling
              // cswmShow and passing in coordinates.
              function ShowContextMenu(e) {
////                  cswmHideNow();
////                  document.getElementById("txtVarSearch").value = document.getElementById("lstVarName").value;


////                  if (navigator.appName == 'Netscape')

////                      cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
////                  // IE
////                  else
                  ////                      cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);

                  cswmHideNow();
                  document.getElementById("txtVarSearch").value = document.getElementById("lstVarName").value;
                  var bt = document.getElementById("btnInvs");
////                  alert('entra');
                  if (bt) {

                      bt.click();



                  }

                  if (navigator.appName == 'Netscape')

                      cswmShow('Group_0bd4', '', 'above', e.pageX, e.pageY, 1);
                  // IE
                  else
                      cswmShow('Group_0bd4', '', 'above', event.x + document.body.scrollLeft, event.y + document.body.scrollTop, 1);



              }


              if (document.forms[0].lstVarName != null) {

                  document.getElementById("txtVarSearch").value = document.getElementById("lstVarName").value;
               document.forms[0].lstVarName.onclick = ShowContextMenu;
              }

              //////////////////////////////////////////////////
              
              //jrs 2024/03/27
	function DisableComputation() {
		document.forms[0].cmdComputations.disabled = true;
	}

	function EnableComputation() {
        document.forms[0].cmdComputations.disabled = false;
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