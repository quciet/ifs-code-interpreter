<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Report" Codebehind="frm_Report.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Basic Report</title>

		<%--JAM 2024/08/17 Bug fix related to GET parameters--%> 
		<%-- <SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript"> </SCRIPT>--%>
		<script language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript"></script>

		
        <!-- jam 2021/06/19-->
		<!--<script language="javascript" type="text/javascript" src="Scripts/common.js"></script>-->

         <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/common.js")%>"></script>

		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<form id="frm_Report3" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/03-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Basic Report" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 117; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
                
            <asp:datagrid id=grdPopulation style="Z-INDEX: 110; LEFT: 1px; POSITION: absolute; TOP: 115px" tabIndex=15 runat="server" Height="200px" Width="256px" AllowPaging="True" DataSource="<%# DsPopulation1 %>" DataMember="IFSVAR" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" PageSize="4" BackColor="White" Visible="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="DEF" HeaderText="DEF"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue" HeaderText="ReportValue"></asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear" HeaderText="Pct1stYear"></asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase" HeaderText="PctBaseCase"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
            
            <asp:dropdownlist id="cboFiles" style="Z-INDEX: 101; LEFT: 290px; POSITION: absolute; TOP: 91px" tabIndex="5"
				runat="server" Height="22" Width="329" BackColor="Window" AutoPostBack="True" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"></asp:dropdownlist>
            <asp:dropdownlist id="Cmb_Year" style="Z-INDEX: 102; LEFT: 178px; POSITION: absolute; TOP: 91px" tabIndex="4"
				runat="server" Height="22" Width="89" BackColor="Window" AutoPostBack="True" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"
				Visible="False"></asp:dropdownlist>
            <asp:dropdownlist id="cmb_Country" style="Z-INDEX: 103; LEFT: 2px; POSITION: absolute; TOP: 91px"
				tabIndex="3" runat="server" Height="22" Width="153" BackColor="Window" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"></asp:dropdownlist>

            <asp:label id="lblInstructions" style="Z-INDEX: 112; LEFT: 426px; POSITION: absolute; TOP: 65px"
				tabIndex="13" runat="server" Height="18" Width="322px" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="True" Font-Bold="False" Font-Size="9.75" Font-Name="Arial"
				Text="Double Click on name for table - Right Click for full name.">Click on Variable name for table</asp:label>
            <asp:label id="Lbl_File" style="Z-INDEX: 113; LEFT: 290px; POSITION: absolute; TOP: 65px" tabIndex="5"
				runat="server" Height="17" Width="112px" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" Text="Select File:">Select Scenario:</asp:label>
            <asp:label id="lbl_countries" style="Z-INDEX: 115; LEFT: 2px; POSITION: absolute; TOP: 65px"
				tabIndex="1" runat="server" Height="17" Width="161" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" Text="Countries or Regions"></asp:label>
            <asp:label id="Lblyear" style="Z-INDEX: 114; LEFT: 178px; POSITION: absolute; TOP: 65px" tabIndex="2"
				runat="server" Height="17" Width="97" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" Text="Select Year" Visible="False">Select Year</asp:label>
                
            <asp:datagrid id=grdEnvironmental style="Z-INDEX: 106; LEFT: 272px; POSITION: absolute; TOP: 107px" tabIndex=16 runat="server" Height="200px" Width="256px" AllowPaging="True" DataSource="<%# DsSocial1 %>" DataMember="IFSVAR" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" PageSize="4" BackColor="White" Visible="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="DEF" HeaderText="DEF"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue" HeaderText="ReportValue"></asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear" HeaderText="Pct1stYear"></asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase" HeaderText="PctBaseCase"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid><asp:datagrid id=grdEconomic style="Z-INDEX: 111; LEFT: 535px; POSITION: absolute; TOP: 116px" tabIndex=17 runat="server" Height="200px" Width="256px" AllowPaging="True" DataSource="<%# DsEconomic1 %>" DataMember="IFSVAR" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" PageSize="4" BackColor="White" Visible="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="DEF" HeaderText="DEF"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue" HeaderText="ReportValue"></asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear" HeaderText="Pct1stYear"></asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase" HeaderText="PctBaseCase"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid><asp:datagrid id=grdEnergy style="Z-INDEX: 108; LEFT: 2px; POSITION: absolute; TOP: 325px" tabIndex=18 runat="server" Height="200px" Width="256px" AllowPaging="True" DataSource="<%# DsEnergy1 %>" DataMember="IFSVAR" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" PageSize="4" BackColor="White" Visible="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="DEF" HeaderText="DEF"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue" HeaderText="ReportValue"></asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear" HeaderText="Pct1stYear"></asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase" HeaderText="PctBaseCase"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid><asp:datagrid id=grdAgriculture style="Z-INDEX: 107; LEFT: 272px; POSITION: absolute; TOP: 323px" tabIndex=19 runat="server" Height="200px" Width="256px" AllowPaging="True" DataSource="<%# DsAgriculture1 %>" DataMember="IFSVAR" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" PageSize="4" BackColor="White" Visible="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="DEF" HeaderText="DEF"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue" HeaderText="ReportValue"></asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear" HeaderText="Pct1stYear"></asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase" HeaderText="PctBaseCase"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid><asp:datagrid id=grdPolitical style="Z-INDEX: 105; LEFT: 538px; POSITION: absolute; TOP: 320px" tabIndex=20 runat="server" Height="200px" Width="256px" AllowPaging="True" DataSource="<%# DsPolitical1 %>" DataMember="IFSVAR" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" PageSize="4" BackColor="White" Visible="False">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:BoundColumn DataField="DEF" HeaderText="DEF"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue" HeaderText="ReportValue"></asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear" HeaderText="Pct1stYear"></asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase" HeaderText="PctBaseCase"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
            
            <asp:datagrid id=grdReport style="Z-INDEX: 104; LEFT: 2px; POSITION: absolute; TOP: 129px" tabIndex=21 runat="server" Height="416px" Width="784px" DataSource="<%# DsReport1 %>" DataMember="Report" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" BackColor="White" >
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				<Columns>
					<asp:ButtonColumn DataTextField="DEF" HeaderText="Variable Name"></asp:ButtonColumn>
					<asp:BoundColumn DataField="ReportValue1" HeaderText="ReportValue1">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue2" HeaderText="ReportValue2">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue3" HeaderText="ReportValue3">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue4" HeaderText="ReportValue4">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue5" HeaderText="ReportValue5">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="ReportValue6" HeaderText="ReportValue6">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn DataField="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn DataField="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="Report2Dimension2" HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn DataField="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear1" HeaderText="Pct1stYear1">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear2" HeaderText="Pct1stYear2">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear3" HeaderText="Pct1stYear3">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear4" HeaderText="Pct1stYear4">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear5" HeaderText="Pct1stYear5">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Pct1stYear6" HeaderText="Pct1stYear6">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase1" HeaderText="PctBaseCase1">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase2" HeaderText="PctBaseCase2">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase3" HeaderText="PctBaseCase3">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase4" HeaderText="PctBaseCase4">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase5" HeaderText="PctBaseCase5">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="PctBaseCase6" HeaderText="PctBaseCase6">
						<HeaderStyle Width="80px"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="NAME" SortExpression="NAME" HeaderText="NAME"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="ReportDimension2" SortExpression="ReportDimension2" HeaderText="ReportDimension2"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="Report2Dimension2" SortExpression="Report2Dimension2"
						HeaderText="Report2Dimension2"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="DIMENSION2" SortExpression="DIMENSION2" HeaderText="DIMENSION2"></asp:BoundColumn>
					<asp:BoundColumn Visible="False" DataField="AGGREGATION" SortExpression="AGGREGATION" HeaderText="AGGREGATION"></asp:BoundColumn>
                    <asp:BoundColumn DataField="ReportDimension3" HeaderText="ReportDimension3" SortExpression="ReportDimension3"
                        Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Report2Dimension3" HeaderText="Report2Dimension3" SortExpression="Report2Dimension3"
                        Visible="False"></asp:BoundColumn>
				</Columns>
				<PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			</asp:datagrid>
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
    var grp="Group="+PrepareForURL(doc.cmb_Country[doc.cmb_Country.selectedIndex].text);
    window.location.search=grp;
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


