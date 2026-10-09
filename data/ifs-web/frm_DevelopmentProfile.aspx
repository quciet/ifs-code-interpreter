<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_DevelopmentProfile" Codebehind="frm_DevelopmentProfile.aspx.vb" %>

<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>frm_DevelopmentProfile</title>

    <!-- jam 2021/07/30-->
    <!--<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	-->

    <!--jam 2021/07/30-->
    <script language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript">
    </script>

    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <!--jam 2021/11/27-->
   <%-- <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
    <style type="text/css">
        .style1 {
            width: 1199px;
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <asp:Table ID="WinFormsToWeb_PageBar" runat="server" Height="15px" Width="100%" CellSpacing="0">
            <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
                Font-Bold="True">
                <asp:TableCell ID="LogoCell" Width="40px">
                    <!--jam 2022-04-15-->
                    <a href="https://korbel.du.edu/pardee">
                        <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>
                </asp:TableCell>
                <asp:TableCell Text="Development Profile" ID="TitleCell"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <cswm:WebMenu ID="cswmWebMenu" style="z-index: 103; left: 0px; position: absolute; top: 16px"
            runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:WebMenu>

        <table>
            <tr>
                <td class="style1">
                    <asp:Panel ID="Panel1" runat="server">
                        &nbsp;&nbsp;
                            <asp:Label ID="lbl_countries" runat="server" BackColor="Window"
                                Font-Bold="True" Font-Italic="False" Font-Name="Arial" Font-Names="Arial"
                                Font-Size="9.75pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="WindowText" Height="17px" TabIndex="8" Text="Countries or Regions"
                                Width="144px"></asp:Label>
                        &nbsp;<asp:DropDownList ID="cmb_Country" runat="server" BackColor="Window"
                            Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0"
                            Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                            Height="22" TabIndex="6" Width="208px">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Lblyear" runat="server" BackColor="Window" Font-Bold="True"
                                Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="9.75pt"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                                Height="17px" TabIndex="9" Text="Select Year" Width="80px"></asp:Label>
                        <asp:DropDownList ID="Cmb_Year" runat="server" AutoPostBack="True"
                            BackColor="Window" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                            Font-Size="8.0" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="WindowText" Height="22" TabIndex="7" Width="89">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server" Width="582px" Height="21px"
                                ForeColor="WindowText" Font-Underline="False"
                                Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75pt" Font-Name="Arial"
                                Text="Click on variable description to get graph, or click on computed value to show over time."
                                BackColor="Window" Font-Names="Arial"></asp:Label><br />
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Panel ID="Panel2" runat="server" Width="1169px">
                        &nbsp;&nbsp;
                            <asp:Label ID="Lbl_File" runat="server" BackColor="Window" Font-Bold="True"
                                Font-Italic="False" Font-Name="Arial" Font-Size="9.75" Font-Strikeout="False"
                                Font-Underline="False" ForeColor="WindowText" Height="21" Text="Select File:"
                                Width="81"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:DropDownList ID="cboFiles" TabIndex="10" runat="server" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
                                Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="22" Width="329" AutoPostBack="True">
                            </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Panel ID="pnlCompare" runat="server" Visible="false">
                        &nbsp;&nbsp;
                            <asp:Label ID="lbl_Countries_Compare" runat="server" BackColor="Window"
                                Font-Bold="True" Font-Italic="False" Font-Name="Arial" Font-Names="Arial"
                                Font-Size="9.75pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="WindowText" Height="17px" TabIndex="8" Text="Compare with Countries or Regions"></asp:Label>
                        &nbsp;&nbsp;
                            <asp:DropDownList ID="cmb_Country_Compare" runat="server" BackColor="Window"
                                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Size="8.0"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                                Height="22" TabIndex="6" Width="208px" AutoPostBack="True">
                            </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label3" runat="server" BackColor="Window" Font-Bold="True"
                                Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="9.75pt"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                                Height="17px" TabIndex="9" Text="Select Year" Width="80px"></asp:Label>
                        <asp:DropDownList ID="Cmb_Year_Compare" runat="server" AutoPostBack="True"
                            BackColor="Window" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                            Font-Size="8.0" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="WindowText" Height="22" TabIndex="7" Width="89">
                        </asp:DropDownList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <twc:AdvWebGrid ID="grd_TableDisplay"
                        TabIndex="1" runat="server" Height="700px" Width="1082px" MouseSelect="SingleCell"
                        RowCount="69" TotalRows="69" HeaderVisible="True" GridBorders-Padding="2"
                        GridBorders-Inner="all" GridBorders-ColorDark="LightGray" GridBorders-Outer="border"
                        GridBorders-Width="1" GridBorders-Color="LightGray" GridBorders-ColorLight="LightGray"
                        GridBorders-Spacing="0" GridBorders-Collapse="Collapse" FooterVisible="False"
                        BorderStyle="Outset" HeaderBorders-Collapse="Collapse" HeaderBorders-Spacing="0"
                        HeaderBorders-ColorLight="LightGray" HeaderBorders-Color="LightGray"
                        HeaderBorders-Width="1" HeaderBorders-Outer="border"
                        HeaderBorders-ColorDark="LightGray" HeaderBorders-Inner="all"
                        HeaderBorders-Padding="1" RowHeight="0" DetailRowHeight="100">
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
                            Pager="PrevNextFirstLast" TextPrev="Prev" BackColorTo="White">
                        </Controller>
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
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="---" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic"
                                DetailColor="" SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White"
                                CheckTrue="true" ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title=" " FooterGradientOrientation="Vertical"
                                HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" DynPrecision="2" HeaderColorTo=""
                                Color="" HeaderAlignment="Left" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="300"
                                DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red"
                                DynCheckFalse="0" Alignment="Left" DataField="" ButtonText="" HeaderFont-Size="Small" HeaderFont-Bold="True">
                            </twc:Column>
                            <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True"
                                ShowHint="False" DetailField=""
                                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
                                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
                                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Computed Value" FooterGradientOrientation="Vertical"
                                HeaderGradientOrientation="Vertical" Formula="" SortSuffix="" HeaderFont-Bold="True" DynPrecision="2"
                                HeaderColorTo="" Color="" HeaderAlignment="Right" FooterAlignment="Left" SortPrefix="" DynEdit="Text"
                                Width="116" DynCheckTrue="1" CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0"
                                ProgressColor="Red" DynCheckFalse="0" Alignment="Right" DataField="" ButtonText="">
                            </twc:Column>
                            <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True"
                                ShowHint="False" DetailField=""
                                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
                                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
                                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Expected Value Predicted from GDP per Capita at PPP"
                                FooterGradientOrientation="Vertical" HeaderGradientOrientation="Vertical" Formula="" SortSuffix=""
                                HeaderFont-Size="XX-Small" HeaderFont-Bold="True" DynPrecision="2" HeaderColorTo="" Color=""
                                HeaderAlignment="Right" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="130" DynCheckTrue="1"
                                CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red" DynCheckFalse="0"
                                Alignment="Right" DataField="" ButtonText="">
                            </twc:Column>
                            <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True"
                                ShowHint="False" DetailField=""
                                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
                                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
                                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Standard Error (SE) of Estimate"
                                FooterGradientOrientation="Vertical" HeaderGradientOrientation="Vertical" Formula="" SortSuffix=""
                                HeaderFont-Size="X-Small" HeaderFont-Bold="True" DynPrecision="2" HeaderColorTo="" Color=""
                                HeaderAlignment="Right" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="116" DynCheckTrue="1"
                                CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red" DynCheckFalse="0"
                                Alignment="Right" DataField="" ButtonText="">
                            </twc:Column>
                            <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True"
                                ShowHint="False" DetailField=""
                                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
                                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
                                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Standard Errors of Value from Prediction"
                                FooterGradientOrientation="Vertical" HeaderGradientOrientation="Vertical" Formula="" SortSuffix=""
                                HeaderFont-Size="X-Small" HeaderFont-Bold="True" DynPrecision="2" HeaderColorTo="" Color=""
                                HeaderAlignment="Right" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="116" DynCheckTrue="1"
                                CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red" DynCheckFalse="0"
                                Alignment="Right" DataField="" ButtonText="">
                            </twc:Column>
                            <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True"
                                ShowHint="False" DetailField=""
                                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
                                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
                                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Contribution to Annual Growth (Percent)"
                                FooterGradientOrientation="Vertical" HeaderGradientOrientation="Vertical" Formula="" SortSuffix=""
                                HeaderFont-Size="X-Small" HeaderFont-Bold="True" DynPrecision="2" HeaderColorTo="" Color=""
                                HeaderAlignment="Right" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="116" DynCheckTrue="1"
                                CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red" DynCheckFalse="0"
                                Alignment="Right" DataField="" ButtonText="">
                            </twc:Column>
                            <twc:Column DetailForeColor="" ColumnHeaderClick="False" Visible="True"
                                ShowHint="False" DetailField=""
                                TitleSpan="0" PopupColorTo="White" FooterText="" VerticalAlignment="Middle" PopupHeight="200"
                                ButtonWidth="0" HeaderVAlignment="Middle" Type="Normal" ColumnHeaderNode="False" TitleRowSpan="False"
                                SubTitle="" Editor="None" ForeColor="" FooterColor="Control" SortFormat="Alphabetic" DetailColor=""
                                SubTitleSpan="0" HeaderColor="Control" ClickConfirm="" PopupColor="White" CheckTrue="true"
                                ColumnHeaderCheckBox="False" FooterColorTo="" FooterType="Text" PopupGradientOrientation="Vertical"
                                PopupWidth="200" DataButtonType="Link" WidthType="Absolute" Title="Parameter Contribution of Factor"
                                FooterGradientOrientation="Vertical" HeaderGradientOrientation="Vertical" Formula="" SortSuffix=""
                                HeaderFont-Size="X-Small" HeaderFont-Bold="True" DynPrecision="2" HeaderColorTo="" Color=""
                                HeaderAlignment="Right" FooterAlignment="Left" SortPrefix="" DynEdit="Text" Width="130" DynCheckTrue="1"
                                CheckFalse="false" CssClass="" FooterFormat="0" DetailSpan="0" ProgressColor="Red" DynCheckFalse="0"
                                Alignment="Right" DataField="" ButtonText="">
                            </twc:Column>
                        </Columns>
                        <SortSettings Ascending="" Direction="Ascending" Column="-1" Show="True" InitDirection="Ascending"
                            Descending=""></SortSettings>
                    </twc:AdvWebGrid>
                </td>
            </tr>
        </table>


        <script language="javascript">
            {
                if ((document.forms[0].cmb_Country != null) && (document.forms[0].cmb_Country.disabled == false)) {
                    document.forms[0].cmb_Country.focus();
                }
            }

            //jrs 2011/05/04
            function cntrylist_click() {
                var doc = document.forms[0];
                var ctry = "Country=" + RegCode[doc.cmb_Country.selectedIndex + 1];
                window.location.search = ctry;
            }

            //jrs 2011/05/05
            function lstGroups_click() {
                //jam 2021/07/30
                //var doc = document.forms[0];
                //var grp = "Group=" + doc.cmb_Country[doc.cmb_Country.selectedIndex].text;
                //window.location.search = grp;

                var doc = document.forms[0];
                var grp = doc.cmb_Country[doc.cmb_Country.selectedIndex].text;
                window.location.href = grp;


            }
        </script>

    </form>
</body>
</html>

<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-3138457-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>
