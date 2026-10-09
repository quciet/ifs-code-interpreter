<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_Table" Codebehind="frm_Table.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IFs Web Table</title>
</head>
<body>
    <form id="form1" runat="server">
           <asp:table id="WinFormsToWeb_PageBar" runat="server" Height="15px" Width="100%" CellSpacing="0">				
                <asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                
                        <!--JAM 2022/03/17-->

					<%--<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a>--%>

                        <a href="https://korbel.du.edu/pardee"><img src="<%= Page.ResolveUrl("~/Images/pcl.png")%>" alt="IFs Home" style="width:30px;"></a>

                  
					</asp:TableCell>

                    <asp:TableCell Text="Table" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 103; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>
    
            <asp:Table ID="grd_TableDisplay" runat="server" Width=100% GridLines=Both>
            </asp:Table>
    </form>
</body>
</html>
