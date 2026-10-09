<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_DisplayControl" Codebehind="frm_DisplayControl.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!-jam 2021/11/27-->
        <%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body LeftMargin="0" RightMargin="0" topmargin="0">
		<form runat="server">
            &nbsp;
			<asp:TextBox id="txt_YAxisLabel" style="Z-INDEX: 100; LEFT: 24px; POSITION: absolute; TOP: 272px; TEXT-ALIGN: left"
				tabIndex="4" runat="server" Height="25" Width="273" BackColor="Window" ForeColor="WindowText"
				Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8.0" Font-Name="Arial" Text="TableTitle" AutoPostBack="False" BorderStyle="Solid"
				Visible="True" Textmode="Singleline" MaxLength="0" ReadOnly="False"></asp:TextBox>
			<asp:Label id="lbl_YAxisLabel" style="Z-INDEX: 101; LEFT: 24px; POSITION: absolute; TOP: 248px; TEXT-ALIGN: left"
				tabIndex="6" runat="server" Height="17px" Width="273px" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				Font-Size="8pt" Font-Name="Arial" Text="lbl_YAxis Label" BorderStyle="None" Font-Names="Arial"></asp:Label>
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0" style="z-index: 112; left: 0px; position: absolute; top: 0px">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Display Control" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
            &nbsp;
			<asp:Button runat="server" STYLE="Z-INDEX:103; LEFT:240px; POSITION:absolute; TOP:32px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="81" Height="33" TabIndex="7" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:104; LEFT:24px; POSITION:absolute; TOP:143px; TEXT-ALIGN:left"
				TabIndex="2" Text="Text1" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Solid" ID="Txt_Interval"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Height="25" STYLE="Z-INDEX:105; LEFT:24px; POSITION:absolute; TOP:207px; TEXT-ALIGN:left"
				TabIndex="3" Text="TableTitle" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Solid" ID="txt_TableTitle"
				AutoPostBack="False" Width="273"></asp:TextBox>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:106; LEFT:24px; POSITION:absolute; TOP:63px; TEXT-ALIGN:left; bottom: 457px;"
				TabIndex="1" Text="Text1" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Solid" ID="Txt_PieYear"
				AutoPostBack="False"></asp:TextBox>
			<asp:Label runat="server" BackColor="Window" Text="Label1" ForeColor="WindowText" STYLE="Z-INDEX:107; LEFT:24px; POSITION:absolute; TOP:111px; TEXT-ALIGN:left"
				TabIndex="6" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" Enabled="True" Visible="True" BorderStyle="None" ID="lbl_interval"
				Height="25" Width="97"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="lbl_TableTitle" ForeColor="WindowText" Height="17px"
				STYLE="Z-INDEX:108; LEFT:24px; POSITION:absolute; TOP:183px; TEXT-ALIGN:left" TabIndex="6"
				Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BorderStyle="None" ID="lbl_TableTitle"
				Width="273px" Font-Names="Arial"></asp:Label>
			<asp:Label runat="server" BackColor="Window" Text="Label1" ForeColor="WindowText" Width="113px"
				Height="33px" STYLE="Z-INDEX:109; LEFT:24px; POSITION:absolute; TOP:31px; TEXT-ALIGN:left"
				TabIndex="6" Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BorderStyle="None" ID="lbl_PieYear" Font-Names="Arial"></asp:Label>
			<script language="javascript">
{
	if ((document.all['Txt_PieYear'] != null)&& (document.all['Txt_PieYear'].disabled == false ) ) 
          {
               document.all('Txt_PieYear').focus();
          }
}
			</script>

            <asp:TextBox ID="txt_XAxisLabel" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 110; left: 24px; position: absolute;
                top: 336px; text-align: left" TabIndex="5" Text="X Axis Label" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>
            <asp:Label ID="lbl_XAxisLabel" runat="server" BackColor="Window" BorderStyle="None"
                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px"
                Style="z-index: 111; left: 24px; position: absolute; top: 312px; text-align: left"
                TabIndex="6" Text="lbl_XAxis Label" Width="273px"></asp:Label>

            <asp:Label ID="lbl_YMin" runat="server" BackColor="Window" BorderStyle="None"
                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px"
                Style="z-index: 111; left: 24px; position: absolute; top: 387px; text-align: left"
                TabIndex="6" Text="Y Axis Min" Width="273px"></asp:Label>

            <asp:TextBox ID="txt_YMin" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 110; left: 24px; position: absolute;
                top: 410px; text-align: left" TabIndex="5" Text="" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>

            <asp:Label ID="lbl_YMax" runat="server" BackColor="Window" BorderStyle="None"
                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px"
                Style="z-index: 111; left: 24px; position: absolute; top: 450px; text-align: left"
                TabIndex="6" Text="Y Axis Max" Width="273px"></asp:Label>
            <asp:TextBox ID="txt_YMax" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 110; left: 24px; position: absolute;
                top: 475px; text-align: left" TabIndex="5" Text="" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>

			<asp:Label ID="lbl_GridLineWidth" runat="server" BackColor="Window" BorderStyle="None"
                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px"
                Style="z-index: 111; left: 24px; position: absolute; top: 510px; text-align: left"
                TabIndex="6" Text="Grid Line Width" Width="273px"></asp:Label>
			<asp:DropDownList ID="cboGridLineWidth" runat="server" Style="z-index: 111; left: 24px; position: absolute; top: 530px; text-align: left"></asp:DropDownList>


			<asp:Label ID="lblHorizontalLineValue" runat="server" BackColor="Window" BorderStyle="None"
                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px"
                Style="z-index: 111; left: 24px; position: absolute; top: 560px; text-align: left"
                TabIndex="6" Text="Horizontal Line Value" Width="273px"></asp:Label>
            <asp:TextBox ID="txtHorizontalLineValue" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 110; left: 24px; position: absolute;
                top: 580px; text-align: left" TabIndex="5" Text="" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>

			<asp:Label ID="lblHorizontalLineText" runat="server" BackColor="Window" BorderStyle="None"
                Font-Bold="False" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="8pt"
                Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="17px"
                Style="z-index: 111; left: 24px; position: absolute; top: 620px; text-align: left"
                TabIndex="6" Text="Horizontal Line Text" Width="273px"></asp:Label>
            <asp:TextBox ID="txtHorizontalLineText" runat="server" AutoPostBack="False" BackColor="Window"
                BorderStyle="Solid" Enabled="True" Font-Bold="False" Font-Italic="False" Font-Name="Arial"
                Font-Size="8.0" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText"
                Height="25" MaxLength="0" ReadOnly="False" Style="z-index: 110; left: 24px; position: absolute;
                top: 640px; text-align: left" TabIndex="5" Text="" TextMode="Singleline"
                Visible="True" Width="273"></asp:TextBox>
		</form>
	</body>
</HTML>
