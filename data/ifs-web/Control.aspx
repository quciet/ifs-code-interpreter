<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Control" Codebehind="Control.aspx.vb" %>
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
			<asp:Table ID="WinFormsToWeb_PageBar" 
				runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Specify Control Form" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:Label runat="server" BackColor="Red" ID="ToolTip1"></asp:Label>
			<asp:Button runat="server" STYLE="LEFT:288px; POSITION:absolute; TOP:399px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="145" Height="25" TabIndex="31" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmd_Exit2"></asp:Button>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:359px; TEXT-ALIGN:left"
				TabIndex="30" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_8"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:335px; TEXT-ALIGN:left"
				TabIndex="29" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_7"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:311px; TEXT-ALIGN:left"
				TabIndex="28" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_6"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:287px; TEXT-ALIGN:left"
				TabIndex="27" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_5"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:263px; TEXT-ALIGN:left"
				TabIndex="26" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_4"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:239px; TEXT-ALIGN:left"
				TabIndex="25" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_3"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:215px; TEXT-ALIGN:left"
				TabIndex="24" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_2"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:191px; TEXT-ALIGN:left"
				TabIndex="23" Text="bD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bD_1"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:359px; TEXT-ALIGN:left"
				TabIndex="22" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_8"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:335px; TEXT-ALIGN:left"
				TabIndex="21" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_7"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:311px; TEXT-ALIGN:left"
				TabIndex="20" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_6"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:287px; TEXT-ALIGN:left"
				TabIndex="19" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_5"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:263px; TEXT-ALIGN:left"
				TabIndex="18" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_4"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:239px; TEXT-ALIGN:left"
				TabIndex="17" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_3"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:215px; TEXT-ALIGN:left"
				TabIndex="16" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_2"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:191px; TEXT-ALIGN:left"
				TabIndex="15" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_1"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:272px; POSITION:absolute; TOP:167px; TEXT-ALIGN:left"
				TabIndex="7" Text="aD" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txt_aD"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:136px; POSITION:absolute; TOP:167px; TEXT-ALIGN:left"
				TabIndex="6" Text="aI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txt_aI"
				AutoPostBack="False"></asp:TextBox>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:359px; TEXT-ALIGN:left" TabIndex="14" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_8"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:335px; TEXT-ALIGN:left" TabIndex="13" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_7"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:311px; TEXT-ALIGN:left" TabIndex="12" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_6"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:287px; TEXT-ALIGN:left" TabIndex="11" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_5"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:263px; TEXT-ALIGN:left" TabIndex="10" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_4"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:239px; TEXT-ALIGN:left" TabIndex="9" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_3"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:215px; TEXT-ALIGN:left" TabIndex="8" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_2"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:191px; TEXT-ALIGN:left" TabIndex="5" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_1"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Generic Equation Form" Font-Name="Arial"
				Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Width="425" Height="25" STYLE="LEFT:16px; POSITION:absolute; TOP:39px; TEXT-ALIGN:left"
				TabIndex="4" Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_Eqn"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Dependent" Font-Name="Arial" Font-Size="8.25"
				Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="73"
				Height="73" STYLE="LEFT:272px; POSITION:absolute; TOP:71px; TEXT-ALIGN:left" TabIndex="3"
				Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_Dep"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Independent" Font-Name="Arial" Font-Size="8.25"
				Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="73"
				Height="73" STYLE="LEFT:136px; POSITION:absolute; TOP:71px; TEXT-ALIGN:left" TabIndex="2"
				Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_Ind"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="a" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:167px; TEXT-ALIGN:left" TabIndex="1" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_A"></asp:Label>
			<script language="javascript">
{
	if ((document.all['lbl_A'] != null)&& (document.all['lbl_A'].disabled == false ) ) 
          {
               document.all('lbl_A').focus();
          }
}
			</script>
		</form>
	</body>
</HTML>
