<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_setextrapolate" Codebehind="setextrapolate.aspx.vb" %>
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
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Set Extrapolation Parameters" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table><asp:panel id="frameSetExtrapolate" style="LEFT: 16px; POSITION: absolute; TOP: 48px" tabIndex="7"
				runat="server" Height="360px" Width="344px" ForeColor="ControlText" Font-Underline="False" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="Window"><BR>
				<asp:Button id="cmdDefault" style="LEFT: 8px; POSITION: absolute; TOP: 312px" tabIndex="19"
					runat="server" Width="65" Height="25" BackColor="Control" Font-Name="Arial" Font-Size="8.0"
					Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="Default"></asp:Button>
				<asp:Button id="cmdClose" style="LEFT: 232px; POSITION: absolute; TOP: 312px" accessKey="C"
					tabIndex="18" runat="server" Width="73" Height="25" BackColor="Control" Font-Name="Arial"
					Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
					ForeColor="ControlText" Text="(C)lose"></asp:Button>
				<asp:Label id="Label1" style="LEFT: 8px; POSITION: absolute; TOP: 24px" tabIndex="17" runat="server"
					Width="169" Height="17" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="No. of Years to Extrapolate"></asp:Label>
				<asp:TextBox id="txtYearsAhead" style="LEFT: 200px; POSITION: absolute; TOP: 24px" tabIndex="12"
					runat="server" Width="73" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0"
					Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
					Text="25" MaxLength="0" AutoPostBack="False"></asp:TextBox>
				<BR>
				<asp:Label id="Label5" style="LEFT: 8px; POSITION: absolute; TOP: 48px" tabIndex="13" runat="server"
					Width="201" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="True" ForeColor="ControlText"
					Text="Parameters:"></asp:Label>
				<BR>
				<asp:RadioButton id="OptSCUNL" style="LEFT: 56px; POSITION: absolute; TOP: 88px" tabIndex="24" runat="server"
					Width="161" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText" Text="S-Curve (Non Linear)"
					AutoPostBack="True" GroupName="frameSetExtrapolate"></asp:RadioButton>
				<asp:RadioButton id="OptSCU" style="LEFT: 200px; POSITION: absolute; TOP: 64px" tabIndex="22" runat="server"
					Width="73" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText" Text="S-Curve"
					AutoPostBack="True" GroupName="frameSetExtrapolate"></asp:RadioButton>
				<asp:RadioButton id="OptExpo" style="LEFT: 104px; POSITION: absolute; TOP: 64px" tabIndex="21" runat="server"
					Width="89" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText" Text="Exponential"
					AutoPostBack="True" GroupName="frameSetExtrapolate"></asp:RadioButton>
				<asp:RadioButton id="OptPoly" style="LEFT: 16px; POSITION: absolute; TOP: 64px" tabIndex="20" runat="server"
					Width="89" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText" Text="Polynomial"
					AutoPostBack="True" GroupName="frameSetExtrapolate"></asp:RadioButton>
				<asp:TextBox id="txtP1" style="LEFT: 232px; POSITION: absolute; TOP: 120px" tabIndex="11" runat="server"
					Width="57" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
					Text="" MaxLength="0" AutoPostBack="False"></asp:TextBox>
				<asp:TextBox id="txtP2" style="LEFT: 232px; POSITION: absolute; TOP: 156px" tabIndex="10" runat="server"
					Width="57" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
					Text="" MaxLength="0" AutoPostBack="False"></asp:TextBox>
				<asp:TextBox id="txtP3" style="LEFT: 232px; POSITION: absolute; TOP: 192px" tabIndex="9" runat="server"
					Width="57" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
					Text="" MaxLength="0" AutoPostBack="False"></asp:TextBox>
				<asp:Button id="cmdSave" style="LEFT: 152px; POSITION: absolute; TOP: 312px" accessKey="S" tabIndex="8"
					runat="server" Width="73" Height="25" BackColor="Control" Font-Name="Arial" Font-Size="8.0"
					Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="(S)ave"></asp:Button>
				<asp:Button id="cmdClear" style="LEFT: 80px; POSITION: absolute; TOP: 312px" tabIndex="7" runat="server"
					Width="65" Height="25" BackColor="Control" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="Clear"></asp:Button>
				<asp:Label id="lblformula" style="LEFT: 16px; POSITION: absolute; TOP: 248px" tabIndex="23"
					runat="server" Width="272" Height="40px" BackColor="Window" Font-Name="Arial" Font-Size="8.0"
					Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="Label2"></asp:Label>
				<asp:Label id="lblP1" style="LEFT: 16px; POSITION: absolute; TOP: 120px" tabIndex="16" runat="server"
					Width="209" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="Parameter 1"></asp:Label>
				<asp:Label id="lblP2" style="LEFT: 16px; POSITION: absolute; TOP: 152px" tabIndex="15" runat="server"
					Width="209" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="Parameter 2"></asp:Label>
				<asp:Label id="lblP3" style="LEFT: 16px; POSITION: absolute; TOP: 192px" tabIndex="14" runat="server"
					Width="209" Height="25" BackColor="Window" Font-Name="Arial" Font-Size="8.0" Font-Bold="False"
					Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="ControlText"
					Text="Parameter 3"></asp:Label>
				<P>&nbsp;</P>
			</asp:panel>
			<script language="javascript">
{
	if ((document.all['frameSetExtrapolate'] != null)&& (document.all['frameSetExtrapolate'].disabled == false ) ) 
          {
               document.all('frameSetExtrapolate').focus();
          }
}
			</script>
		</form>
	</body>
</HTML>
