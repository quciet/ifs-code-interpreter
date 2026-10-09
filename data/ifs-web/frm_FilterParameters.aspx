<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_FilterParameters" Codebehind="frm_FilterParameters.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
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
			<asp:Table ID="WinFormsToWeb_PageBar" 
				runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Filter Parameters" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			<cswm:WebMenu id="cswmWebMenu" style="Z-INDEX: 113; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:WebMenu>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:101; LEFT:128px; POSITION:absolute; TOP:200px; TEXT-ALIGN:left"
				TabIndex="5" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtRangeto" AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:102; LEFT:128px; POSITION:absolute; TOP:168px; TEXT-ALIGN:left"
				TabIndex="4" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtRangefrom"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:103; LEFT:128px; POSITION:absolute; TOP:136px; TEXT-ALIGN:left"
				TabIndex="3" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtRateChange"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:104; LEFT:128px; POSITION:absolute; TOP:104px; TEXT-ALIGN:left"
				TabIndex="2" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtMaximumLevel"
				AutoPostBack="False"></asp:TextBox>
			<asp:TextBox runat="server" Width="89" Height="25" STYLE="Z-INDEX:105; LEFT:128px; POSITION:absolute; TOP:72px; TEXT-ALIGN:left"
				TabIndex="1" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText" ReadOnly="False"
				MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtMinimumLevel"
				AutoPostBack="False"></asp:TextBox>
			<asp:Label runat="server" Text="to::" Width="15" Height="13" STYLE="Z-INDEX:106; LEFT:72px; POSITION:absolute; TOP:200px; TEXT-ALIGN:left"
				TabIndex="10" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblRangeto"></asp:Label>

            <asp:CheckBox ID="chkLoadedValues"  Text="Used only Loaded Values" Checked="true" runat="server" 
            STYLE="Z-INDEX:106; LEFT:25px; POSITION:absolute; TOP:233px; TEXT-ALIGN:left" 
            Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
			Font-Underline="False" />
				
            <asp:CheckBox ID="chkDisplayCount"  Text="Display only count of Filtered items" Checked="false" runat="server" 
            STYLE="Z-INDEX:106; LEFT:24px; POSITION:absolute; TOP:257px; TEXT-ALIGN:left" 
            Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
			Font-Underline="False" />

			<asp:Label runat="server" Text="Range from:" Width="58" Height="13" STYLE="Z-INDEX:107; LEFT:24px; POSITION:absolute; TOP:168px; TEXT-ALIGN:left"
				TabIndex="9" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblrange"></asp:Label>
			<asp:Label runat="server" Text="Rate of Change:" Width="78" Height="13" STYLE="Z-INDEX:108; LEFT:24px; POSITION:absolute; TOP:136px; TEXT-ALIGN:left"
				TabIndex="8" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblRate"></asp:Label>
			<asp:Label runat="server" Text="Maximum Level:" Width="76" Height="13" STYLE="Z-INDEX:109; LEFT:24px; POSITION:absolute; TOP:104px; TEXT-ALIGN:left"
				TabIndex="7" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblMaximumlevel"></asp:Label>
			<asp:Label runat="server" Text="Minimum Level:" Width="73" Height="13" STYLE="Z-INDEX:110; LEFT:24px; POSITION:absolute; TOP:72px; TEXT-ALIGN:left"
				TabIndex="6" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BackColor="Transparent" Enabled="True" ForeColor="ControlText" Visible="True"
				BorderStyle="None" ID="lblminlevel"></asp:Label>
			<script language="javascript">
{
	if ((document.all['txtMinimumLevel'] != null)&& (document.all['txtMinimumLevel'].disabled == false ) ) 
          {
               document.all('txtMinimumLevel').focus();
          }
}
			</script>

		</form>
	</body>
</HTML>
