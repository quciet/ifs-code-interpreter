<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_StartYearControl" Codebehind="frm_StartYearControl.aspx.vb" %>
<!-- CDV 2021/10/22 -->
<!DOCTYPE html>
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
         <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>--%>
	</HEAD>
	<body>
		<form runat="server">
			<p>
			<asp:Label runat="server" BackColor="Window" Text="Label1" ForeColor="WindowText" STYLE="Z-INDEX:109; LEFT: 10px; POSITION:relative; TOP:5px; TEXT-ALIGN:left; width: 200px; height: 25px; bottom: 307px;"
						TabIndex="6" Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
						Font-Underline="False" BorderStyle="None" ID="lbl_FirstYear" Font-Names="Arial"></asp:Label>
			</p>
			<p>
			<asp:TextBox ID="txtStartYear" runat="server" STYLE=" Z-INDEX:106;POSITION:relative; LEFT:10px; TOP:5px; width: 122px; Height:30px;"
				Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False" TabIndex="-1"></asp:TextBox>
			<asp:Button ID="cmdStartYear" runat="server" Text="Submit" STYLE="Z-INDEX:106;POSITION:relative; LEFT:10px; TOP:5px; width: 122px;Height:30px;"
				Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"/>
			</p>
			<p>
			<asp:DropDownList runat="server" STYLE="Z-INDEX:106; LEFT:10px; POSITION:relative; TOP:5px; TEXT-ALIGN:left; bottom: 220px; height: 35px; width: 130px;"
			TabIndex="1" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
			Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
			ReadOnly="False" Visible="True" BorderStyle="Solid" ID="lst_FirstYear"
			AutoPostBack="False"></asp:DropDownList>
			<asp:Button runat="server" STYLE="Z-INDEX:103; LEFT:10px; POSITION:relative; TOP:5px; TEXT-ALIGN:center; width: 122px;"
			Text="E(x)it" accessKey="x" Height="35px" TabIndex="7" Font-Name="Arial" Font-Size="8.0"
			Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
			CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>
			</p>
		</form>
	</body>
</html>
