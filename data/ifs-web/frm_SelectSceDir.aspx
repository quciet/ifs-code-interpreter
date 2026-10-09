<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_SelectSceDir" Codebehind="frm_SelectSceDir.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="miu" Namespace="MarkItUp.WebControls" Assembly="MarkItUp.WebControls.Timer" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<HTML>
    <HEAD>
    	<script language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</script>
		<script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>

    	<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
         <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">	--%>
    </HEAD>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="form1" runat="server">
        			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Select Scenario Directory" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

        <div>
            <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblRunYear" runat="server" Text="Run Year:" Font-Bold="True"></asp:Label>&nbsp;
            <asp:TextBox ID="txtYear" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdContinue" runat="server" Text="(C)ontinue" AccessKey="c" Width="100px" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdSelect" runat="server" Text="(S)elect" AccessKey="s" Width="100px" />
        </div>
        <div>
            <br />
            <asp:TreeView ID="treeRunSce" runat="server" ExpandDepth="0" ImageSet="Arrows">
                <SelectedNodeStyle Font-Bold="True" Font-Overline="True" BorderColor="Black" BorderStyle="Solid" />
            </asp:TreeView>
        </div>
        <div>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInst1" runat="server" Text="" Font-Bold="True"></asp:Label><br /><br />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInst2" runat="server" Text="Run Number:" Font-Bold="True" Visible="false"></asp:Label>
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRunNumber" runat="server" Text="" Font-Bold="True"></asp:Label>&nbsp;<br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblInst3" runat="server" Text="Year of Run:" Font-Bold="True" Visible="false"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblYearRun" runat="server" Text="" Font-Bold="True"></asp:Label><br /><br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <miu:timer id="Timer1" runat="server" Interval="5000" MaxTicks="1" CommandArgument="1"></miu:timer>
    </form>
</body>
</html>
