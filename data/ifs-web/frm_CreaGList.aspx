<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_CreaGList" Codebehind="frm_CreaGList.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
    <title>Create G-Lists</title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
            <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                   <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Create G-Lists" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 0%"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False">
		    </cswm:webmenu>

    <form id="form1" runat="server" style="margin:8px">

        <div style="display:inline-grid; width:fit-content; height:fit-content;">
                <div style="grid-row:1; grid-column:1;height:5vh; width:30vw">
                    <asp:Label ID="lblGList" runat="server" Text="G-Lists:"></asp:Label>&nbsp&nbsp&nbsp
                    <asp:RadioButton ID="optRegion" Text="Countries/Regions" runat="server" Checked="True" AutoPostBack="true" GroupName="GroupingOptions" ToolTip="Loads Countries (Alt+R)" AccessKey="r"  /> &nbsp&nbsp&nbsp
                    <asp:RadioButton ID="optGroups" Text="Groups" runat="server" Checked="False" AutoPostBack="true" GroupName="GroupingOptions" ToolTip="Loads Groups (Alt+G)" AccessKey="g"  /> 
                 </div>
                <br />
                <div style="grid-row:2; grid-column:1;height:70vh; width:30vw"">
                    <asp:ListBox ID="lstGListIdDesc" runat="server" Height="100%" Width="100%" AutoPostBack="true"></asp:ListBox>
                    <asp:ListBox ID="lstGListId" runat="server" AutoPostBack="true" Visible="False"></asp:ListBox>
                </div>

                <div>
                    <br />
                    <div style="display:grid; width:stretch">
                        <div style="grid-row:2; grid-column:1;">
                            <asp:Label ID="GListLabel" runat="server" Height="100%" Width="100%" Text="Name for New or Copied GList:" AutoPostBack="true"></asp:Label>
                         </div>
                        <div style="grid-row:2; grid-column:2;">
                            <asp:TextBox ID="txtGList" runat="server" Height="100%" Width="100%" AutoPostBack="false"></asp:TextBox>
                         </div>
                     </div>
                    <br />
                    <div style="display:grid; width:stretch;height:5vh">
                        <div style="grid-row:1; grid-column:1;">
                            <asp:Button ID="cmdAddGList" runat="server" Height="100%" Width="100%" Text="Add GList" ></asp:Button>
                         </div>
                        <div style="grid-row:1; grid-column:2;">
                            <asp:Button ID="cmdCopyGList" runat="server" Height="100%" Width="100%" Text="Copy GList" AutoPostBack="true"></asp:Button>
                         </div>
                        <div style="grid-row:1; grid-column:3;">
                            <asp:Button ID="cmdDeleteGList" runat="server" Height="100%" Width="100%" Text="Delete GList" AutoPostBack="true"></asp:Button>
                         </div>
                     </div>
                </div>
                <div style="grid-row:3; grid-column:2;padding-top:12%;padding-left:.5vw;height:5vh; width:30vw">
                    <asp:Button ID="cmdCommitChanges" runat="server" Height="100%" Width="40%" Text="Commit All Changes"/>
                </div>
                <div style="grid-row:1; grid-column:2;height:5vh; width:30vw; align-content:center;padding-left:.5vw">
                    <asp:Label ID="lblCurrent" runat="server" Text="Members in G-List"></asp:Label>
                 </div>
                <div style="grid-row:2; grid-column:2;height:70vh; padding-left:.5vw;width:30vw">
                    <asp:ListBox ID="lstNationsInGroup" runat="server" Height="100%" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                    
                </div>
                <div style="grid-row:2; grid-column:3;display:grid;width:5vw; height:70vh;padding-left:.5vw;">
                    <div style="grid-row:1; grid-column:1; align-content:center">
                        <asp:Button ID="cmdRemoveMember" runat="server" Text="---&gt;" Font-Bold="True" Font-Italic="False" Font-Names="Bauhaus 93" Font-Overline="False" Font-Size="XX-Large" Height="25%" Width="70%" />
                    </div>
                    <div style="grid-row:2; grid-column:1; align-content:center">
                        <asp:Button ID="cmdAddMember" runat="server" Text="&lt;---" Font-Bold="True" Font-Italic="False" Font-Names="Bauhaus 93" Font-Overline="False" Font-Size="XX-Large" Height="25%" Width="70%" /> 
                    </div>
                </div>
                <div style="grid-row:1; grid-column:4;height:5vh; width:30vw; align-content:center">
                    <asp:Label ID="Label1" runat="server" Text="Members Not In GList"></asp:Label>
                 </div>
                <div style="grid-row:2; grid-column:4;height:70vh; width:30vw;">
                    <asp:ListBox ID="lstNationsNotInGroup" runat="server" Height="100%" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                </div>
                <div style="grid-row:3; grid-column:4; width:30vw; padding-top:2%; height:10vh; margin-bottom:0%;">
                    <asp:FileUpload runat="server" ID="ImportList" Style = "display:inline; width:100%; height:35%;margin-top:0px;" />
                </div>
        </div>
    </form>
</body>
</html>
