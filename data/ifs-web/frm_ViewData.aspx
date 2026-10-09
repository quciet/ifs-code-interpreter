<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_ViewData" Codebehind="frm_ViewData.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Imported Data</title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="frm_ViewData" runat="server">
        	<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="View Imported Data" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
        <asp:Panel ID="Panel2" runat="server" BorderStyle="Groove">
                <asp:Button ID="cmdDeleteZeroes" runat="server" Text="Delete Zeros" Width="125px" />
                &nbsp;
                <asp:Button ID="cmdDeleteNegatives" runat="server" Text="Delete Negatives" Width="125px" />
                &nbsp;<asp:Button ID="cmdViewMissing" runat="server" Text="View Excluded" Width="125px" />
                <br />
                <br />
                Marking Threshold: <asp:TextBox ID="txtMarkThreshold" runat="server"></asp:TextBox>
                &nbsp;&nbsp; <asp:Button ID="cmdMarkTransients" runat="server" Text="Mark Transients" Width="125px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="CmdCancel" runat="server" Text="Cancel" Width="125px" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="CmdAccept" runat="server" Text="Accept Data" Width="125px" />
                <br />
            </asp:Panel>    
        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="75%" Font-Size="Small" GroupingText="You can click on columns to sort" ScrollBars="Auto">
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True">
                </asp:GridView>
                <table>
                    <tr>
                        <td style="vertical-align:top">
                            <asp:GridView ID="GrdShowMMC" runat="server" Caption="Excluded">
                            </asp:GridView>
                        </td>
                        <td style="vertical-align:top">
                            <asp:GridView ID="GrdShowMMC2" runat="server" Caption="Included">
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
    </form>
</body>
</html>
