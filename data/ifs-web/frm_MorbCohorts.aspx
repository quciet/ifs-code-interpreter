<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_MorbCohorts" Codebehind="frm_MorbCohorts.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<HTML>
	<HEAD>
		<title>Morbidity Distribution</title>
        <!--jam 2023/01/11-->
		<%--<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">--%>
            <SCRIPT language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>"> type="text/javascript">

		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
          <!--jam 2021/11/27-->
		<!--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">-->
        <!--jam 2021/11/27-->
        <!--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>-->
        <!--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>-->

        <script src="<%= Page.ResolveUrl("~/Scripts/jquery/1.8.2/jquery.min.js")%>"></script>
        
        <!--jam 2021/05/29-->
        <!--<script src="Scripts/highcharts.js"></script>-->
        <!--<script src="Scripts/exporting.js"></script>-->

         <!--jam 2021/05/29-->
        <!-- jam 2023/01/11-->
       <%-- <script src="<%= Page.ResolveUrl("~/Scripts/highcharts.js")%>"></script>
        <script src="<%= Page.ResolveUrl("~/Scripts/exporting.js")%>"></script>--%>


         <!--jam 2025/03/01 New highcharts version libraries-->
        <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/highcharts.js")%>"></script>
        <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/exporting.js")%>"></script>
   
        <!--jam 2025/03/01 New highcharts version libraries-->
        <script src="<%= Page.ResolveUrl("~/Scripts/highchartlatest/offline-exporting.js")%>"></script>



        <!--jam 2021/05/29-->
		<!--<script language="javascript" type="text/javascript" src="Scripts/common.js"></script>-->
        <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/common.js")%>"></script>


	</HEAD>
	<body bgColor="white" leftMargin="0" rightMargin="0" topmargin="0">
        <form id="frm_MorbCohorts" runat="server">
			<div id="firstloading" style="position:absolute; width:100%; text-align:center; top:300px;">
                <!--jam 2023/01/11-->
               <%-- <img id="loadimg" src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...--%>

                
                <img id="loadimg" src="<%= Page.ResolveUrl("~/images/squares-circle.gif")%>"  height="93" width="93"><br>Loading resources...
                
			</div>
            <asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/14-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>

                   


					</asp:TableCell>
					<asp:TableCell Text="Morbidity by Age, Sex and Cause" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="wmMain" runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockPaddingAdjust="False"
				MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>			
			<table>
				<tr>
                    <td align=center style="width: 146%">
                         <%--jam 2025/03/01 New highcharts version libraries container width adjusted from 90% to 100%--%>
                       <%-- <div id="container" style="height: 90%; width: 90%; top: 10px; left: 10px; position: relative;"></div>	--%>
                        <div id="container" style="height: 90%; width: 100%; top: 10px; left: 10px; position: relative;"></div>	
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
                    <td align=center style="width: 146%">
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
                    <td style="width: 146%">
                        <asp:Label id="lbldata" 
		    			tabIndex="7" runat="server" Height="33px" Width="472px" ForeColor="ControlText" Font-Underline="False"
			    		Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75pt" Font-Name="Arial"
				    	BackColor="Window" Font-Names="Arial"></asp:Label>
				    	<asp:HiddenField ID="hfTitle" Value="" runat=server/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 146%">			        	
				        <asp:panel id="frameData" 
				        tabIndex="6" runat="server" Width="816px" Height="60px" BackColor="Window" Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
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
					        <asp:Button id="cmdLine" tabIndex="10"
					        runat="server" Height="33" Width="113" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" 
					        Text="Line" Visible=false></asp:Button>			
                        </asp:panel>
                    </td>
                </tr>
                <tr>
                    <td>                    
				<TWC:AdvWebGrid id="grdcohort" 
				runat="server" Height="646px" Width="1154px" BorderStyle="Solid" FooterVisible="False" GridBorders-Collapse="Collapse"
				GridBorders-Spacing="0" GridBorders-ColorLight="LightGray" GridBorders-Color="LightGray" GridBorders-Width="1"
				GridBorders-Outer="border" GridBorders-ColorDark="LightGray" GridBorders-Inner="all" GridBorders-Padding="2"
				HeaderVisible="True" TotalRows="5" RowCount="5" MouseSelect="SingleCell" tabIndex="1" Visible="False" ActiveRow="-1" ActiveRowColor="Highlight" ActiveRowFontColor="HighlightText" AlwaysEdit="False" AutoEdit="False" BackgroundImage="" BorderColor="Window" BorderWidth="3px" DecimalPoint="." DefaultDynEdit="0" DefaultDynText="0" DetailRowHeight="100" DetailRowShow="Normal" GradientOrientation="Horizontal" GridBackColor="" GridBackColorTo="" HeaderBorders-Collapse="Collapse" HeaderBorders-Color="LightGray" HeaderBorders-ColorDark="LightGray" HeaderBorders-ColorLight="LightGray" HeaderBorders-Inner="all" HeaderBorders-Outer="border" HeaderBorders-Padding="2" HeaderBorders-Spacing="0" HeaderBorders-Width="1" HeaderStyle="Raised" HoverColor="" HoverFontColor="" RowHeight="0" SelectColor="Highlight" SelectFontColor="HighlightText" StartPage="1" StretchColumn="-1">
				<DatePickerEditor DateSeparator="dsSlash" GradientDirection="gdHorizontal" GradientStartColor="White"
					GradientEndColor="AliceBlue"/>
                    <ClientEvents ButtonClick="" CellClick="" ComboChange="" DynCheckClick="" DynComboChange=""
                        DynEditChange="" DynEditDone="" EditDone="" ImageClick="" NodeClick="" />
                    <SortSettings Ascending="" Column="-1" Descending="" Direction="Ascending" InitDirection="Ascending"
                        Show="True" />
                    <Controller Borders-Collapse="Collapse" Borders-Spacing="0" Borders-ColorLight="InactiveBorder"
					Borders-Width="1" Borders-Outer="below" Borders-ColorDark="ActiveBorder" Borders-Inner="all"
					Borders-Padding="2" GradientOrientation="Vertical" ImgLast="" ImgFirst="" HintPrev=""
					PagerType="Button" Position="None" Height="20" HintLast="" ImgPrev="" Caption="&lt;font color=&quot;white&quot;&gt;&lt;b&gt;Title&lt;/b&gt;&lt;/font&gt;" ImgNext=""
					IndicatorFormat="" TextNext="Next" HintFirst="" TextLast="Last" TextFirst="First" BackColor="#4572A7"
					Alignment="Left" MaxPages="5" IndicatorType="PageNo" RowCountSelect="False" HintNext=""
					Pager="DropDownList" TextPrev="Prev" BackColorTo="InactiveCaption" Borders-Color="LightGray">
                    </Controller>
                    <RowHeader BackColor="Control" BackColorTo="" Borders-Collapse="Collapse" Borders-Inner="all"
                        Borders-Outer="border" Borders-Padding="2" Borders-Spacing="0" Borders-Width="1"
                        Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif" Font-Overline="False"
                        Font-Size="12pt" Font-Strikeout="False" Font-Underline="False" GradientOrientation="Vertical"
                        Visible="False" Width="25" Borders-Color="LightGray" Borders-ColorDark="LightGray" Borders-ColorLight="LightGray" />
                    <Images ActiveRow="" AlwaysEditRow="" Cancel="" CancelHint="Cancel" Edit="" EditHint="Edit"
                        ImgCheckBox="" ImgCheckBoxChecked="" ImgCheckBoxCheckedHint="" ImgCheckBoxHint=""
                        NodeClose="" NodeCloseHint="Close Detail" NodeOpen="" NodeOpenHint="Open Detail"
                        Post="" PostHint="Post" />
                    <Columns>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="Age Category" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="Com Disease Male" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                        <twc:Column Alignment="Left" ButtonText="" ButtonWidth="0" CheckFalse="false" CheckTrue="true"
                            ClickConfirm="" Color="" ColumnHeaderCheckBox="False" ColumnHeaderClick="False"
                            ColumnHeaderNode="False" CssClass="" DataButtonType="Link" DataField="" DetailColor=""
                            DetailField="" DetailForeColor="" DetailSpan="0" DynCheckFalse="0" DynCheckTrue="1"
                            DynEdit="Text" DynPrecision="2" Editor="None" FooterAlignment="Left" FooterColor="Control"
                            FooterColorTo="" FooterFormat="0" FooterGradientOrientation="Vertical" FooterText=""
                            FooterType="Text" ForeColor="" Formula="" HeaderAlignment="Left" HeaderColor="Control"
                            HeaderColorTo="" HeaderGradientOrientation="Vertical" HeaderVAlignment="Middle"
                            PopupColor="White" PopupColorTo="White" PopupGradientOrientation="Vertical" PopupHeight="200"
                            PopupWidth="200" ProgressColor="Red" ShowHint="False" SortFormat="Alphabetic"
                            SortPrefix="" SortSuffix="" SubTitle="" SubTitleSpan="0" Title="" TitleRowSpan="False"
                            TitleSpan="0" Type="Normal" VerticalAlignment="Middle" Visible="True" Width="9"
                            WidthType="Percent">
                        </twc:Column>
                    </Columns>
                    <GlyphImages NextMonth="" NextYear="" PrevMonth="" PrevYear="" />
                    <Bands Active="False" PrimaryColor="White" SecondaryColor="Info" />
                    <Scroll Enable="False" Persistent="False" Scrollbar3DLightColor="" ScrollbarArrowColor=""
                        ScrollbarBaseColor="" ScrollbarDarkshadowColor="" ScrollbarFaceColor="" ScrollbarHighlightColor=""
                        ScrollbarShadowColor="" ScrollbarTrackColor="" />
                </twc:AdvWebGrid>
                    </td>
                </tr>
                <tr>
                    <td align=center style="width: 146%">
                        <TWC:AdvWebGrid id="grdcohortDet" 
				runat="server" Height="524px" Width="600" BorderStyle="Solid" FooterVisible="False" GridBorders-Collapse="Collapse"
				GridBorders-Spacing="0" GridBorders-ColorLight="LightGray" GridBorders-Color="LightGray" GridBorders-Width="1"
				GridBorders-Outer="border" GridBorders-ColorDark="LightGray" GridBorders-Inner="all" GridBorders-Padding="2"
				HeaderVisible="True" TotalRows="5" RowCount="5" MouseSelect="None" tabIndex="1" Visible="False" ActiveRow="-1" ActiveRowColor="Highlight" ActiveRowFontColor="HighlightText" AlwaysEdit="False" AutoEdit="False" BackgroundImage="" BorderColor="Window" BorderWidth="3px" DecimalPoint="." DefaultDynEdit="0" DefaultDynText="0" DetailRowHeight="100" DetailRowShow="Normal" GradientOrientation="Horizontal" GridBackColor="" GridBackColorTo="" HeaderBorders-Collapse="Collapse" HeaderBorders-Color="LightGray" HeaderBorders-ColorDark="LightGray" HeaderBorders-ColorLight="LightGray" HeaderBorders-Inner="all" HeaderBorders-Outer="border" HeaderBorders-Padding="2" HeaderBorders-Spacing="0" HeaderBorders-Width="1" HeaderStyle="Raised" HoverColor="" HoverFontColor="" RowHeight="0" SelectColor="Highlight" SelectFontColor="HighlightText" StartPage="1" StretchColumn="-1">
                            <DatePickerEditor DateSeparator="dsSlash" GradientDirection="gdHorizontal" GradientEndColor="AliceBlue"
                                GradientStartColor="White" />
                            <ClientEvents ButtonClick="" CellClick="" ComboChange="" DynCheckClick="" DynComboChange=""
                                DynEditChange="" DynEditDone="" EditDone="" ImageClick="" NodeClick="" />
                            <SortSettings Ascending="" Column="-1" Descending="" Direction="Ascending" InitDirection="Ascending"
                                Show="True" />
                            <Controller Borders-Collapse="Collapse" Borders-Spacing="0" Borders-ColorLight="InactiveBorder"
					Borders-Width="1" Borders-Outer="below" Borders-ColorDark="ActiveBorder" Borders-Inner="all"
					Borders-Padding="2" GradientOrientation="Vertical" ImgLast="" ImgFirst="" HintPrev=""
					PagerType="Button" Position="None" Height="20" HintLast="" ImgPrev="" Caption="&lt;font color=&quot;white&quot;&gt;&lt;b&gt;Title&lt;/b&gt;&lt;/font&gt;" ImgNext=""
					IndicatorFormat="" TextNext="Next" HintFirst="" TextLast="Last" TextFirst="First" BackColor="#4572A7"
					Alignment="Left" MaxPages="5" IndicatorType="PageNo" RowCountSelect="False" HintNext=""
					Pager="DropDownList" TextPrev="Prev" BackColorTo="InactiveCaption" Borders-Color="LightGray">
                            </Controller>
                            <RowHeader BackColor="Control" BackColorTo="" Borders-Collapse="Collapse" Borders-Inner="all"
                                Borders-Outer="border" Borders-Padding="2" Borders-Spacing="0" Borders-Width="1"
                                Font-Bold="False" Font-Italic="False" Font-Names="Microsoft Sans Serif" Font-Overline="False"
                                Font-Size="12pt" Font-Strikeout="False" Font-Underline="False" GradientOrientation="Vertical"
                                Visible="False" Width="25" Borders-Color="LightGray" Borders-ColorDark="LightGray" Borders-ColorLight="LightGray" />
                            <Images ActiveRow="" AlwaysEditRow="" Cancel="" CancelHint="Cancel" Edit="" EditHint="Edit"
                                ImgCheckBox="" ImgCheckBoxChecked="" ImgCheckBoxCheckedHint="" ImgCheckBoxHint=""
                                NodeClose="" NodeCloseHint="Close Detail" NodeOpen="" NodeOpenHint="Open Detail"
                                Post="" PostHint="Post" />
                            <Columns>
                                <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True" ShowHint="False" DetailField=""
						TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
						ButtonWidth="0" HeaderVAlignment="Middle" Type="LinkField" ColumnHeaderNode="False" TitleRowSpan="False"
						SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
						SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
						ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
						PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Hola" FooterGradientOrientation="Vertical"
						HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
						Color="" HeaderAlignment="Center" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="125"
						DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
						DynCheckFalse="0" Alignment="Center" DataField="" ButtonText="" FooterFont-Bold="True" FooterFont-Italic="False" FooterFont-Overline="False" FooterFont-Strikeout="False" FooterFont-Underline="False" HeaderFont-Overline="True">
                                </twc:Column>
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
						DynCheckFalse="0" Alignment="Left" DataField="" ButtonText="">
                                </twc:Column>
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
						DynCheckFalse="0" Alignment="Left" DataField="" ButtonText="">
                                </twc:Column>
                            </Columns>
                            <GlyphImages NextMonth="" NextYear="" PrevMonth="" PrevYear="" />
                            <Bands Active="False" PrimaryColor="White" SecondaryColor="Info" />
                            <Scroll Enable="False" Persistent="False" Scrollbar3DLightColor="" ScrollbarArrowColor=""
                                ScrollbarBaseColor="" ScrollbarDarkshadowColor="" ScrollbarFaceColor="" ScrollbarHighlightColor=""
                                ScrollbarShadowColor="" ScrollbarTrackColor="" />
			</TWC:AdvWebGrid>					
                    		<div><asp:Label ID="lblJSLine" runat="server" Visible="False"></asp:Label></div>   
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

 if (document.all['grdcohort'] != null)
    {
        var prtContent = document.all('grdcohort');
     }
 else
    {
        var prtContent = document.all('grdcohortDet');
     }
    
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

 if (document.all['grdcohort'] != null)
    alert("Use your printer's settings to change to landscape and to scale to page (95%)");
 
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
		<table style="Z-INDEX: 115; LEFT: 250px; POSITION: absolute; TOP: 597px" >	
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
</html>


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