<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_PackagedDisplays_Exp" Codebehind="frm_PackagedDisplays_Exp.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
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
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Explaining Variable Lists" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 113; LEFT: 0px; POSITION: absolute; TOP: 16px"
			runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
            
            &nbsp;
			<asp:label id="Label1" style="Z-INDEX: 101; LEFT: 25px; POSITION: absolute; TOP: 63px" tabIndex="8"
				runat="server" Text="Select Formula to show its members in second grid" Width="526px" BackColor="Window" Height="17px"
				Font-Bold="True" Font-Size="9.75pt" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Name="Arial" Font-Names="Arial"></asp:label>
            &nbsp;

            <table style="z-index: 105; left: 16px; position: absolute; top: 92px; width: 100%;">
                <tr>
                    <td>
                        <asp:GridView ID="grdVarList" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            GridLines="Vertical"  
                            Width="100%" AutoGenerateSelectButton="True" AllowPaging="True">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="ListOrder" HeaderText="ListOrder" SortExpression="ListOrder"
                                    Visible="False" />
                                <asp:BoundField DataField="VarName" HeaderText="Variable Name" 
                                    SortExpression="VarName" ItemStyle-Width="15%">
<ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DEFINITION" HeaderText="Definition" 
                                    SortExpression="DEFINITION" ItemStyle-Width="35%">
<ItemStyle Width="35%"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="" SortExpression="FormulaId" Visible=false>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("FormulaId") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFormulaId" runat="server" Text='<%# Bind("FormulaId") %>' Visible=false></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="0%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="FormulaDetHis" HeaderText="Historical Formula" 
                                    SortExpression="FormulaDetHis" ItemStyle-Width="15%">
<ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Historic_Analog" HeaderText="Historic Analog" 
                                    SortExpression="Historic_Analog" ItemStyle-Width="15%">
<ItemStyle Width="15%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Param2" HeaderText="Dim2" SortExpression="Param2" 
                                    ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Param3" HeaderText="Dim3" SortExpression="Param3" 
                                    ItemStyle-Width="10%">
<ItemStyle Width="10%"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="Gainsboro" />
                        </asp:GridView>
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
                                <asp:BoundField DataField="CodeVar" HeaderText="Code" SortExpression="CodeVar" />
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
                <tr>
                    <td>
                        <asp:GridView ID="grdHistAnalogDet" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            Caption='<table border="1" width="100%" cellpadding="0" cellspacing="0" bgcolor="#000084"><tr><td><font color="#FFFFFF"><div align="center"><b>Historic Analog Formula Detail</b></div></font></td></tr></table>'
                            GridLines="Vertical" Visible="False" Width="100%" AutoGenerateSelectButton="True">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:BoundField DataField="Dim2" HeaderText="Dim2" SortExpression="Dim2" />
                                <asp:BoundField DataField="Dim3" HeaderText="Dim3" SortExpression="Dim3" />
                                <asp:BoundField DataField="Historic Analog" HeaderText="Historic Analog" SortExpression="Historic Analog" />
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            &nbsp;
            &nbsp;&nbsp;
            <asp:AccessDataSource ID="dsVarList" runat="server">
            </asp:AccessDataSource>
            <asp:AccessDataSource ID="dsFormulas" runat="server">
            </asp:AccessDataSource>
            <asp:AccessDataSource ID="dsHistAnalogFormulas" runat="server">
            </asp:AccessDataSource>

		</form>
	</body>
</HTML>
