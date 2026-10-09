<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_ScatterPower" Codebehind="frm_ScatterPower.aspx.vb" %>
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
					<asp:TableCell Text="Power Coefficient" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
            &nbsp;
			<asp:Button runat="server" STYLE="Z-INDEX:103; LEFT:322px; POSITION:absolute; TOP:81px; TEXT-ALIGN:center"
				Text="E(x)it" accessKey="x" Width="81" Height="33" TabIndex="7" Font-Name="Arial" Font-Size="8.0"
				Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Control"
				CausesValidation="True" Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>
            &nbsp;
			<asp:TextBox runat="server" Height="25" STYLE="Z-INDEX:105; LEFT:24px; POSITION:absolute; TOP:84px; TEXT-ALIGN:left"
				TabIndex="3" Text="Degree" Font-Name="Arial" Font-Size="8.0" Font-Bold="False" Font-Italic="False"
				Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True" ForeColor="WindowText"
				ReadOnly="False" MaxLength="0" Textmode="Singleline" Visible="True" BorderStyle="Inset" ID="txtPowerDegree"
				AutoPostBack="False" Width="273"></asp:TextBox>&nbsp;&nbsp;
			<asp:Label runat="server" BackColor="Window" Text="For Formulation (aX^b), Enter a value for b. You  can use Excel to determine b" ForeColor="WindowText" Height="17px"
				STYLE="Z-INDEX:108; LEFT:24px; POSITION:absolute; TOP:55px; TEXT-ALIGN:left" TabIndex="6"
				Font-Name="Arial" Font-Size="8pt" Font-Bold="False" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" BorderStyle="None" ID="lbl_VarperGraph"
				Width="382px" Font-Names="Arial"></asp:Label>
            &nbsp;&nbsp;
            &nbsp;
			<script language="javascript">
{
	if ((document.all['Txt_PieYear'] != null)&& (document.all['Txt_PieYear'].disabled == false ) ) 
          {
               document.all('Txt_PieYear').focus();
          }
          

                //jrs 2012/01/16
                function clickButton(e, buttonid){
                      var evt = e ? e : window.event;
                      var bt = document.getElementById(buttonid);
                      if (bt){
                          if (evt.keyCode == 13){
                                bt.click();
                                return false;
                          }
                      }
                }			          
}
			</script>

            &nbsp;
		</form>
	</body>
</HTML>
