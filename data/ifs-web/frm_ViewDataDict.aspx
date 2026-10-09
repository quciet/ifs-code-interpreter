<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_ViewDataDict" Codebehind="frm_ViewDataDict.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <title>Displaying Data Dictionary</title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="frm_ViewDataDictionary" runat="server">
        	<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Displaying Data Dictionary" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
		        <asp:Panel ID="Panel2" runat="server" BorderStyle="Groove">                
                &nbsp;
                &nbsp;
                <br />
                &nbsp;&nbsp; 
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="CmdCancel" runat="server" Text="Continue" Width="125px" />
                &nbsp;&nbsp;&nbsp;
                <br />
            </asp:Panel> 
        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="75%" Font-Size="Small" GroupingText="You can click on columns to sort" ScrollBars="Auto">
                <asp:GridView ID="GridView1" runat="server" AllowSorting="True">
                </asp:GridView>
                <asp:GridView ID="GrdShowMMC" runat="server">
                </asp:GridView>
            </asp:Panel>
                    
    </form>
</body>
</html>
