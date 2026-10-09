<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_NotePad" Codebehind="NotePad.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body LeftMargin="0" RightMargin="0" topmargin="0">
		<form runat="server">
			<asp:Table ID="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Annotate Working Scenario" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			
			<table>
			    <tr>
			        <td width=100%>
            			<asp:Label runat="server" BackColor="Red" ID="ToolTip1"></asp:Label>
			        </td>
			    </tr>			    
			    <tr>
			        <td width=100%>
			            <asp:textbox runat="server" Font-Name="Microsoft Sans Serif" Font-Size="8.25" Font-Bold="False"
				            Font-Italic="False" Font-Strikeout="False" Font-Underline="False" BackColor="Window" Enabled="True"
				            TabIndex="1" ID="rtf1" AutoPostBack="False"
				            TextMode="MultiLine" Height="600px" Width="1000px" ></asp:textbox>
				            <br /><br />
			        </td>
			    </tr>
			    <tr>
			        <td width=100%>
			            <asp:Button runat="server" 
				            Text="Exit" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Width="81" Height="33" TabIndex="4" CausesValidation="True"
				            Enabled="True" ForeColor="ControlText" ID="cmdExit"></asp:Button>

                            <input id="btnHelp" onclick="javascript:ShowHelp();"
                                STYLE="width: 88px; height: 32px;" type="button" value="Help" />
                                <!--RH_ShowHelp(0, 'WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,7504);-->
			                <script language="javascript">
                                {
	                                if ((document.all['rtf1'] != null)&& (document.all['rtf1'].disabled == false ) ) 
                                          {
                                               document.all('rtf1').focus();
                                          }
                                }
			                </script>

			            <asp:Button runat="server" STYLE="TEXT-ALIGN:center"
				            Text="Clear" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Width="81" Height="33" TabIndex="3" CausesValidation="True"
				            Enabled="True" ForeColor="ControlText" ID="cmdClear" Visible="False"></asp:Button>
			            <asp:Button runat="server" STYLE="TEXT-ALIGN:center"
				            Text="Save" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Width="81" Height="33" TabIndex="2" CausesValidation="True"
				            Enabled="True" ForeColor="ControlText" ID="cmdSave" Visible="False"></asp:Button>
			        </td>			    
			    </tr>
			    <tr>
			        <td width=100%>
			            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Text="message" Visible="False"></asp:Label>
			        </td>			    
			    </tr>
			</table>
						
		</form>
	</body>
</HTML>
