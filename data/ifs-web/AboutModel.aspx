<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_AboutModel" Codebehind="AboutModel.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!-- jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body LeftMargin="0" RightMargin="0" topmargin="0">
		<form runat="server">
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Feedback" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 103; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" Height="50px" Width="94px" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
			<asp:Label runat="server" BackColor="Red" ID="ToolTip1"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Label1" ForeColor="WindowText" Width="577px"
				Height="313px" STYLE="Z-INDEX:101; LEFT:40px; POSITION:absolute; TOP:80px; TEXT-ALIGN:left"
				TabIndex="1" Font-Name="Arial" Font-Size="10pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BorderStyle="None" ID="lbl_header" Font-Names="Arial"></asp:Label>
			<script language="javascript">
{
	if ((document.all['lbl_header'] != null)&& (document.all['lbl_header'].disabled == false ) ) 
          {
               document.all('lbl_header').focus();
          }
}
			</script>
			<aiswc:MainMenu ID="MainMenu1" runat="server"></aiswc:MainMenu>
		</form>
	</body>
</HTML>
