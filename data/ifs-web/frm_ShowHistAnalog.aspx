<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_ShowHistAnalog" Codebehind="frm_ShowHistAnalog.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server">
        <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
		    <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
			    Font-Bold="True">
			    <asp:TableCell  ID="LogoCell" Width="40px">
                    <!--jam 2022/04/16-->
			    <a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
			    <asp:TableCell Text="Show Historic Analog Variables" ID="TitleCell"></asp:TableCell>
		    </asp:TableRow>
		</asp:table>
        <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 116; LEFT: 0px; POSITION: absolute; TOP: 16px"
    	runat="server" Height="48px" Width="88px" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>		
        <table style="z-index: 105; left: 16px; position: absolute; top: 92px; width: 100%;">
                <tr>
                    <td>
    			        <asp:label id="lblShowHistAnalog" tabIndex="8"
				        runat="server" Text="Select Formula to show its members in second grid" Width="526px" BackColor="Window" Height="17px"
				        Font-Bold="True" Font-Size="9.75pt" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False"
				        Font-Italic="False" Font-Name="Arial" Font-Names="Arial"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdFormulas" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                            Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#000084"><tr><td><font color="#FFFFFF"><div align="center"><b>Formula Detail</b></div></font></td></tr></table>'
                            GridLines="Vertical" Visible="False" Width="100%" AutoGenerateSelectButton="True">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="CodeVar" />
                                <asp:BoundField DataField="VarName" HeaderText="Variable Name" SortExpression="VarName" />
                                <asp:BoundField DataField="DEFINITION" HeaderText="Definition" ReadOnly="True" SortExpression="DEFINITION" />
                                <asp:BoundField DataField="Historic_Analog" HeaderText="Historic Analog" SortExpression="Historic_Analog" />
                                <asp:BoundField DataField="DIM2" HeaderText="Dim2" SortExpression="DIM2" />
                                <asp:BoundField DataField="DIM3" HeaderText="Dim3" SortExpression="DIM3" />
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="grdHistAnalogFormulas" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#000084"><tr><td><font color="#FFFFFF"><div align="center"><b>Historic Analog Formula Detail</b></div></font></td></tr></table>'
                            GridLines="Vertical" Visible="False" Width="100%" AutoGenerateSelectButton="True">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="ListOrder" HeaderText="ListOrder" SortExpression="ListOrder"
                                    Visible="False" />
                                <asp:BoundField DataField="CodeVar" HeaderText="Code" SortExpression="CodeVar" />
                                <asp:BoundField DataField="VarName" HeaderText="Variable Name" SortExpression="VarName" />
                                <asp:BoundField DataField="Formula" HeaderText="Formula" SortExpression="Formula" />
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            </asp:AccessDataSource>        	
    </form>
</body>
</html>
