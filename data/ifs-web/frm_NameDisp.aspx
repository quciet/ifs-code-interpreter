<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_NameDisp" Codebehind="frm_NameDisp.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
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
					<asp:TableCell Text="Define Names" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:Label runat="server" BackColor="Red" ID="ToolTip1"></asp:Label>
			<asp:Button runat="server" STYLE="LEFT:330px; POSITION:absolute; TOP:209px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="81" Height="33" TabIndex="4" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmd_Exit"></asp:Button>
			<asp:Button runat="server" STYLE="LEFT:186px; POSITION:absolute; TOP:209px; TEXT-ALIGN:center"
				Text="(P)rint" accessKey="P" Width="81" Height="33" TabIndex="3" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="but_Print" 
                Visible="False"></asp:Button>
			<asp:ListBox runat="server" Width="747px" Height="124" STYLE="LEFT:13px; POSITION:absolute; TOP:70px"
				TabIndex="2" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BorderStyle="Inset" BackColor="Window" Enabled="True" ForeColor="WindowText"
				SelectionMode="Single" Visible="True" ID="lst_Defns" AutoPostBack="True"></asp:ListBox>
			<asp:Label runat="server" BackColor="Window" Text="Definitions of variable and parameter names:"
				ForeColor="WindowText" Width="313" Height="17" STYLE="LEFT:28px; POSITION:absolute; TOP:46px; TEXT-ALIGN:left"
				TabIndex="1" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" Enabled="True" Visible="True" BorderStyle="None" ID="lbl__Explain"></asp:Label>
			<script language="javascript">
{
	if ((document.all['cmd_Exit'] != null)&& (document.all['cmd_Exit'].disabled == false ) ) 
          {
               document.all('cmd_Exit').focus();
          }
}
			</script>
		</form>
	</body>
</HTML>
