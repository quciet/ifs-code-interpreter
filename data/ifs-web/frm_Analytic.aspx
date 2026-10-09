<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Analytic" Codebehind="frm_Analytic.aspx.vb" %>
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
	<body LeftMargin="0" RightMargin="0">
		<form runat="server">
			<asp:Table ID="WinFormsToWeb_PageBar" STYLE="Z-INDEX:103; LEFT:1px; POSITION:absolute; TOP:0px"
				runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Specify Analytic Function" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:312px; POSITION:absolute; TOP:136px; TEXT-ALIGN:left"
				TabIndex="23" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txt_Top" AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:312px; POSITION:absolute; TOP:88px; TEXT-ALIGN:left"
				TabIndex="22" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txt_bottom" AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:287px; TEXT-ALIGN:left"
				TabIndex="20" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_8"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:263px; TEXT-ALIGN:left"
				TabIndex="19" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_7"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:239px; TEXT-ALIGN:left"
				TabIndex="18" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_6"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:215px; TEXT-ALIGN:left"
				TabIndex="17" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_5"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:191px; TEXT-ALIGN:left"
				TabIndex="16" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_4"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:167px; TEXT-ALIGN:left"
				TabIndex="15" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_3"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:143px; TEXT-ALIGN:left"
				TabIndex="14" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_2"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:119px; TEXT-ALIGN:left"
				TabIndex="13" Text="bI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="_txt_bI_1"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="65" Height="19" STYLE="LEFT:144px; POSITION:absolute; TOP:87px; TEXT-ALIGN:left"
				TabIndex="5" Text="aI" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txt_aI"
				AutoPostBack="False"></asp:TextBox>
			<asp:Button runat="server" STYLE="LEFT:32px; POSITION:absolute; TOP:343px; TEXT-ALIGN:center"
				Text="E(x)it - Abandon Changes" accessKey="x" Width="145" Height="25" TabIndex="24" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				BackColor="Control" CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmd_Exit"></asp:Button>
			<asp:Button runat="server" STYLE="LEFT:232px; POSITION:absolute; TOP:343px; TEXT-ALIGN:center"
				Text="(S)ave and Continue" accessKey="S" Width="153" Height="25" TabIndex="25" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				BackColor="Control" CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmd_Exit2"></asp:Button>
			<asp:Label runat="server" BackColor="Window" Text="To X Value" Font-Name="Arial" Font-Size="8.25"
				Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="104px"
				Height="16px" STYLE="LEFT:312px; POSITION:absolute; TOP:120px; TEXT-ALIGN:left" TabIndex="21"
				Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None" ID="Llb_To"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:287px; TEXT-ALIGN:left" TabIndex="12" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_8"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:263px; TEXT-ALIGN:left" TabIndex="11" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_7"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:239px; TEXT-ALIGN:left" TabIndex="10" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_6"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:215px; TEXT-ALIGN:left" TabIndex="9" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_5"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:191px; TEXT-ALIGN:left" TabIndex="8" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_4"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:167px; TEXT-ALIGN:left" TabIndex="7" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_3"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:143px; TEXT-ALIGN:left" TabIndex="6" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_2"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="bI" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:119px; TEXT-ALIGN:left" TabIndex="4" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="_lbl_bI_1"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Generic Equation Form" Font-Name="Arial"
				Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Width="425" Height="25" STYLE="LEFT:16px; POSITION:absolute; TOP:39px; TEXT-ALIGN:left"
				TabIndex="3" Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_Eqn"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Display Figure from X Value" Font-Name="Arial"
				Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Width="176px" Height="16px" STYLE="LEFT:312px; POSITION:absolute; TOP:72px; TEXT-ALIGN:left"
				TabIndex="2" Enabled="True" ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_DisplayRange"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="a" Font-Name="Arial" Font-Size="8.25" Font-Bold="True"
				Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Width="57" Height="17"
				STYLE="LEFT:56px; POSITION:absolute; TOP:87px; TEXT-ALIGN:left" TabIndex="1" Enabled="True"
				ForeColor="ControlText" Visible="True" BorderStyle="None" ID="lbl_A"></asp:Label>
			<script language="javascript">
{
	if ((document.forms[0].txt_aI != null)&& (document.forms[0].txt_aI.disabled == false ) ) 
          {
               document.forms[0].txt_aI.focus();
          }
}
			</script>
		</form>
	</body>
</HTML>
