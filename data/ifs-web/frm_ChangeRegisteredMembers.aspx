<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ChangeRegisteredMembers" Codebehind="frm_ChangeRegisteredMembers.aspx.vb" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
    <head runat="server">
        <title>Edit Groups</title>
    </head>
    <body style="overflow:hidden; height:100vh; width:100vw;margin:0px">
        <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                   <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Change Group Members" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            <cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 0%"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False">
		    </cswm:webmenu>
        <form id="form1" runat="server" style="height:100%; width:100%; margin:8px"> 
            <div style="display:inline-grid; width:fit-content; height:fit-content;">
                <div style="grid-row:1; grid-column:1;height:5vh; width:30vw">
                    <asp:Label ID="LabelGroups" runat="server" Text="Groups:"></asp:Label>
                    <asp:CheckBox ID="OnlyCoreGroups" Text="Show Only Core Groups" runat="server" Checked="True" AutoPostBack="true" /> 
                    <asp:Button ID="ChangeCoreGroups" runat="server" Text="Change Core Groups" Stlye="height:100%;" Enabled="False" />
                 </div>
                <br />
                <div style="grid-row:2; grid-column:1;height:70vh; width:30vw"">
                    <asp:ListBox ID="ListGroups" runat="server" Height="100%" Width="100%" AutoPostBack="true"></asp:ListBox>
                </div>
                <div style="grid-row:4; grid-column:1; padding-top:2%; height:10vh; margin-bottom:0%; width:30vw"">
                    <div style="display:grid; width:stretch;height:5vh">
                        <div style="grid-row:1; grid-column:1;">
                            <asp:Button ID="AddGroup" runat="server" Height="100%" Width="100%" Text="Add Group" ></asp:Button>
                         </div>
                        <div style="grid-row:1; grid-column:2;">
                            <asp:Button ID="CopyGroup" runat="server" Height="100%" Width="100%" Text="Copy Group" AutoPostBack="true"></asp:Button>
                         </div>
                        <div style="grid-row:1; grid-column:3;">
                            <asp:Button ID="DeleteGroup" runat="server" Height="100%" Width="100%" Text="Delete Group" AutoPostBack="true"></asp:Button>
                         </div>
                     </div>
                </div>
                <div style="grid-row:3; grid-column:1;height:5vh; padding-top:2vw; width:30vw"">
                    <div style="display:grid; width:stretch">
                        <div style="grid-row:2; grid-column:1;">
                            <asp:Label ID="GroupLabel" runat="server" Height="100%" Width="100%" Text="Name for New or Copied Group:" AutoPostBack="true"></asp:Label>
                         </div>
                        <div style="grid-row:2; grid-column:2;">
                            <asp:TextBox ID="GroupText" runat="server" Height="100%" Width="100%" AutoPostBack="false"></asp:TextBox>
                         </div>
                     </div>
                </div>
                <div style="grid-row:1; grid-column:2;height:5vh; width:30vw; align-content:center;padding-left:.5vw">
                    <asp:Label ID="LabelCurrent" runat="server" Text="Country in Group"></asp:Label>
                 </div>
                <div style="grid-row:2; grid-column:2;height:70vh; padding-left:.5vw;width:30vw">
                    <asp:ListBox ID="ListNationsInGroup" runat="server" Height="100%" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                    
                </div>
                <div style="grid-row:4; grid-column:2;padding-top:2%;padding-left:.5vw;height:5vh; width:30vw">
                    <asp:Button ID="CommitChanges" runat="server" Height="100%" Width="40%" Text="Commit All Changes"/>
                </div>
                <div style="grid-row:2; grid-column:3;display:grid;width:5vw; height:70vh;padding-left:.5vw;">
                    <div style="grid-row:1; grid-column:1; align-content:center">
                        <asp:Button ID="RemoveNation" runat="server" Text="---&gt;" Font-Bold="True" Font-Italic="False" Font-Names="Bauhaus 93" Font-Overline="False" Font-Size="XX-Large" Height="25%" Width="70%" />
                    </div>
                    <div style="grid-row:2; grid-column:1; align-content:center">
                        <asp:Button ID="AddNation" runat="server" Text="&lt;---" Font-Bold="True" Font-Italic="False" Font-Names="Bauhaus 93" Font-Overline="False" Font-Size="XX-Large" Height="25%" Width="70%" /> 
                    </div>
                </div>
                <div style="grid-row:1; grid-column:4;height:5vh; width:30vw; align-content:center">
                    <asp:Label ID="Label1" runat="server" Text="Country Not In Group"></asp:Label>
                 </div>
                <div style="grid-row:2; grid-column:4;height:70vh; width:30vw;">
                    <asp:ListBox ID="ListNationsNotInGroup" runat="server" Height="100%" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                </div>
                <div style="grid-row:3; grid-column:4; width:30vw; padding-top:2%; height:10vh; margin-bottom:0%;">
                    <asp:FileUpload runat="server" ID="ImportList" Style = "display:inline; width:100%; height:35%;margin-top:0px;" />
                </div>
            </div>
              
        </form>
    </body>
  
</html>
