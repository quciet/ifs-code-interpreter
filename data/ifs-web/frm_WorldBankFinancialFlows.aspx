<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_WorldBankFinancialFlows" Codebehind="frm_WorldBankFinancialFlows.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>World Bank Financial Flows</title>
    <!--<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
	</SCRIPT>	-->

      <!--jam 2021/09/11-->
         <SCRIPT language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript">
		</SCRIPT>	

    <!--jam 2021/09/11-->
    <!--<script language="javascript" type="text/javascript" src="Scripts/common.js"></script>-->

    <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/common.js")%>"></script>
</head>
<body leftMargin="0" rightMargin="0" topmargin="0">
    <form id="frm_WorldBankFinancialFlows" runat="server">
    			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/15-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="World Bank Financial Flows" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 117; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>

            <asp:label id="lblInstructions" style="Z-INDEX: 112; LEFT: 426px; POSITION: absolute; TOP: 65px"
				tabIndex="13" runat="server" Height="18" Width="322px" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="True" Font-Bold="False" Font-Size="9.75" Font-Name="Arial"
				Text="Double Click on name for table - Right Click for full name.">Click on Variable name for table</asp:label>
            <asp:label id="Lbl_File" style="Z-INDEX: 113; LEFT: 290px; POSITION: absolute; TOP: 65px" tabIndex="5"
				runat="server" Height="17" Width="112px" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" Text="Select File:">Select Scenario:</asp:label>
            <asp:label id="lbl_countries" style="Z-INDEX: 115; LEFT: 2px; POSITION: absolute; TOP: 65px"
				tabIndex="1" runat="server" Height="17" Width="161" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="True" Font-Size="9.75" Font-Name="Arial" Text="Countries or Regions"></asp:label>

            <asp:dropdownlist id="cmb_Country" style="Z-INDEX: 103; LEFT: 2px; POSITION: absolute; TOP: 91px"
				tabIndex="3" runat="server" Height="22" Width="153" BackColor="Window" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"></asp:dropdownlist>

            <asp:dropdownlist id="cboFiles" style="Z-INDEX: 101; LEFT: 290px; POSITION: absolute; TOP: 91px" tabIndex="5"
				runat="server" Height="22" Width="329" BackColor="Window" AutoPostBack="True" ForeColor="WindowText" Font-Underline="False"
				Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial"></asp:dropdownlist>


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
			        var doc = document.forms[0];
			        var grp = "Group=" + PrepareForURL(doc.cmb_Country[doc.cmb_Country.selectedIndex].text);
			        window.location.search = grp;
			    }

			</script>
               
                <asp:GridView id=grdReport 
                    style="Z-INDEX: 104; LEFT: 2px; POSITION: absolute; TOP: 129px" 
                    runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" BackColor="White" 
                    BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
                    <Columns>
                        <asp:ButtonField DataTextField="DEF" HeaderText="DEF" SortExpression="DEF" ButtonType="link"/>
                        <asp:BoundField DataField="REPORTVALUE1" HeaderText="REPORTVALUE1" 
                            SortExpression="REPORTVALUE1"  ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="REPORTVALUE2" HeaderText="REPORTVALUE2" 
                            SortExpression="REPORTVALUE2" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="REPORTVALUE3" HeaderText="REPORTVALUE3" 
                            SortExpression="REPORTVALUE3" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="REPORTVALUE4" HeaderText="REPORTVALUE4" 
                            SortExpression="REPORTVALUE4" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="REPORTVALUE5" HeaderText="REPORTVALUE5" 
                            SortExpression="REPORTVALUE5" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="REPORTVALUE6" HeaderText="REPORTVALUE6" 
                            SortExpression="REPORTVALUE6" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" />
                        <asp:BoundField DataField="DIMENSION2" HeaderText="DIMENSION2" 
                            SortExpression="DIMENSION2" />
                        <asp:BoundField DataField="REPORTDIMENSION2" HeaderText="REPORTDIMENSION2" 
                            SortExpression="REPORTDIMENSION2" />
                        <asp:BoundField DataField="REPORT2DIMENSION2" HeaderText="REPORT2DIMENSION2" 
                            SortExpression="REPORT2DIMENSION2" />
                        <asp:BoundField DataField="AGGREGATION" HeaderText="AGGREGATION" 
                            SortExpression="AGGREGATION" />
                        <asp:BoundField DataField="Pct1stYear1" HeaderText="Pct1stYear1" 
                            SortExpression="Pct1stYear1" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="Pct1stYear2" HeaderText="Pct1stYear2" 
                            SortExpression="Pct1stYear2" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="Pct1stYear3" HeaderText="Pct1stYear3" 
                            SortExpression="Pct1stYear3" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="Pct1stYear4" HeaderText="Pct1stYear4" 
                            SortExpression="Pct1stYear4" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="Pct1stYear5" HeaderText="Pct1stYear5" 
                            SortExpression="Pct1stYear5" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="Pct1stYear6" HeaderText="Pct1stYear6" 
                            SortExpression="Pct1stYear6" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="PctBaseCase1" HeaderText="PctBaseCase1" 
                            SortExpression="PctBaseCase1" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="PctBaseCase2" HeaderText="PctBaseCase2" 
                            SortExpression="PctBaseCase2" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="PctBaseCase3" HeaderText="PctBaseCase3" 
                            SortExpression="PctBaseCase3" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="PctBaseCase4" HeaderText="PctBaseCase4" 
                            SortExpression="PctBaseCase4" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="PctBaseCase5" HeaderText="PctBaseCase5" 
                            SortExpression="PctBaseCase5" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="PctBaseCase6" HeaderText="PctBaseCase6" 
                            SortExpression="PctBaseCase6" ItemStyle-HorizontalAlign=Right/>
                        <asp:BoundField DataField="DIMENSION3" HeaderText="DIMENSION3" 
                            SortExpression="DIMENSION3" />
                        <asp:BoundField DataField="REPORTDIMENSION3" HeaderText="REPORTDIMENSION3" 
                            SortExpression="REPORTDIMENSION3" />
                        <asp:BoundField DataField="REPORT2DIMENSION3" HeaderText="REPORT2DIMENSION3" 
                            SortExpression="REPORT2DIMENSION3" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
               
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