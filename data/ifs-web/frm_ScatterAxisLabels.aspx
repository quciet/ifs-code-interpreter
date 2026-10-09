<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_scatteraxislabels" Codebehind="frm_scatteraxislabels.aspx.vb" %>
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
            &nbsp; &nbsp;
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0" style="z-index: 112; left: 0px; position: absolute; top: 0px">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Graph Labels Definition" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
            &nbsp;
			<asp:Button runat="server" STYLE="Z-INDEX:103; LEFT:240px; POSITION:absolute; TOP:32px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="81" Height="33" TabIndex="1" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>
            &nbsp;
			<asp:TextBox runat="server" Height="25" STYLE="Z-INDEX:105; LEFT:24px; POSITION:absolute; TOP:96px; TEXT-ALIGN:left"
				TabIndex="3" Text="XAxisLabel" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Solid" ID="txt_XAxisLabel"
				AutoPostBack="False" Width="273"></asp:TextBox>
            <asp:TextBox ID="txt_YAxisLabel" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 105; left: 24px; position: absolute;
                top: 160px; text-align: left" TabIndex="4" Text="YAxisLabel" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>
            <asp:TextBox ID="txt_TitleLabel" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 105; left: 24px; position: absolute;
                top: 288px; text-align: left" TabIndex="6" Text="" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>
            <asp:TextBox ID="txt_SubTitleLabel" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 105; left: 24px; position: absolute;
                top: 224px; text-align: left" TabIndex="5" Text="" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>
            &nbsp; &nbsp;
            &nbsp;&nbsp;
			<asp:Label runat="server" BackColor="Window" Text="X Axis Label" ForeColor="WindowText" Height="17px"
				STYLE="Z-INDEX:108; LEFT:24px; POSITION:absolute; TOP:80px; TEXT-ALIGN:left" TabIndex="7"
				Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BorderStyle="None" ID="lbl_XAxisLabel"
				Width="273px" Font-Names="Arial"></asp:Label>
				&nbsp;
            <asp:Label ID="lbl_YAxisLabel" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="False"
                Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 108;
                left: 24px; position: absolute; top: 144px; text-align: left" TabIndex="8" Text="Y Axis Label"
                Width="273px"></asp:Label>
            <asp:Label ID="lbl_subtitle" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="False"
                Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 108;
                left: 24px; position: absolute; top: 208px; text-align: left" TabIndex="9" Text="SubTitle Label"
                Width="273px"></asp:Label>
                 <asp:Label ID="Label1" runat="server" BackColor="Window" BorderStyle="None" Font-Bold="False"
                Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt" Font-Strikeout="False"
                Font-Underline="False" ForeColor="WindowText" Height="17px" Style="z-index: 108;
                left: 24px; position: absolute; top: 272px; text-align: left" TabIndex="10" Text="Title Label"
                Width="273px"></asp:Label>

            &nbsp; &nbsp;
            &nbsp;&nbsp;
            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
			<script language="javascript">
{
	
          if ((document.all['txt_YAxisLabel'] != null)&& (document.all['txt_YAxisLabel'].disabled == false ) ) 
          {
               document.all('txt_YAxisLabel').focus();
          }
          if ((document.all['txt_XAxisLabel'] != null)&& (document.all['txt_XAxisLabel'].disabled == false ) ) 
          {
               document.all('txt_XAxisLabel').focus();
          }
}
			</script>

           
            
		</form>
	</body>
</HTML>
