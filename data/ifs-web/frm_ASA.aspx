<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_ASA" Codebehind="frm_ASA.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Advanced Sustainability Analysis</title>
   
   <%-- jam 2024/08/17 Bug fix related to GET parameters--%>
    <%-- <SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">--%>
    <SCRIPT language="JavaScript1.2" src="<%= Page.ResolveUrl("~/RoboHelp_CSH.js")%>" type="text/javascript">

	</SCRIPT>	
    <!--jam 2021/12/29-->
    <!--<script language="javascript" type="text/javascript" src="Scripts/common.js"></script>-->
     <script language="javascript" type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/common.js")%>"></script>
</head>
<body leftMargin="0" rightMargin="0" topmargin="0">
    <form id="frm_ASA" runat="server">
    			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
					<a href="http:\\www.pardee.du.edu"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>

                    <asp:TableCell Text="Advanced Sustainability Analysis" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 117; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>

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
    
            <asp:GridView ID="grdASA" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" style="Z-INDEX: 104; LEFT: 2px; POSITION: absolute; TOP: 119px">
                <Columns>
                    <asp:BoundField DataField="Category" HeaderText="Category" 
                        SortExpression="Category" />
                    <asp:ButtonField CommandName="Fossil Fuel Use (BBOE)" 
                        DataTextField="FossilFuelUse" HeaderText="Fossil Fuel Use&lt;br&gt;(BBOE)" 
                        Text="FossilFuelUse">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="Carbon Emissions (Billion Tons)" 
                        DataTextField="CarbonEmissions" 
                        HeaderText="Carbon Emissions&lt;br&gt;(Billion Tons)" Text="Button">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="Deforestation (Million Hectares)" 
                        DataTextField="Deforestation" 
                        HeaderText="Deforestation&lt;br&gt;(Million Hectares)" Text="Button">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="Water Use (Cubic Kilometers)" 
                        DataTextField="WaterUse" HeaderText="Water Use&lt;br&gt;(Cubic Kilometers)" 
                        Text="Button">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:ButtonField>
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

            <asp:GridView ID="grdASA2" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" style="Z-INDEX: 104; LEFT: 2px; POSITION: absolute; TOP: 700px">
                <Columns>
                    <asp:BoundField DataField="Category" HeaderText="Category" 
                        SortExpression="Category" />
                    <asp:ButtonField CommandName="Values" DataTextField="ASAValues" 
                        HeaderText="Values" Text="Button">
                    <HeaderStyle HorizontalAlign="Right" />
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="Cumulative Percentage Change" 
                        DataTextField="CumPerChg" HeaderText="Cumulative&lt;br&gt;Percentage Change" 
                        Text="Button">
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:ButtonField>
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

            <asp:label id="lblInstructions" style="Z-INDEX: 104; LEFT: 600px; POSITION: absolute; TOP: 64px"
				tabIndex="13" runat="server" Height="18" Width="322px" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="True" Font-Bold="False" Font-Size="9.75" Font-Name="Arial"
				Text="Double Click on name for table - Right Click for full name.">Click on numeric values to show over time</asp:label>


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
                        // jam 2021/12/29
                        //var grp = "Group=" + PrepareForURL(doc.cmb_Country[doc.cmb_Country.selectedIndex].text);

                        
                        var grp =  PrepareForURL(doc.cmb_Country[doc.cmb_Country.selectedIndex].text);

                        

                         //jam 2022/01/03
                        //window.location.search=grp;
                        window.location.href = grp;
    			    }

			</script>
</form>
</body>
</html>
