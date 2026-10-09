<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_SelctGeography" Codebehind="frm_SelctGeography.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>IFsWeb</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<META http-equiv="CACHE-CONTROL" content="NO-CACHE">
		<META http-equiv="Pragma" content="no-cache">
		<META http-equiv="Expires" content="-1">
		<script language="javascript">
			{
				//jrs 09/15/04 refresh in Netscape
				if (navigator.appName=='Netscape')
				{
					// read cookie
					var begin, end;
					begin = document.cookie.indexOf("RefreshGraph=");
					begin += 13;
					end = document.cookie.indexOf(";", begin);
					if (end == -1)
						end = document.cookie.length;
					var value;
					value = unescape(document.cookie.substring(begin, end));
					
					if ( value == 'true' )
					{
						document.write('<META HTTP-EQUIV="Refresh" CONTENT="0"> ');
					}
				}
			}
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
		
			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Graphical Display" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            &nbsp;
							
			<center>
			<table>
			<tr>
			<td style="width: 3px">
                   
            <asp:listbox id="lstDim"  runat="server"
				Height="426" Width="300"></asp:listbox>
            
            </td>
            </tr>
            </table>
            </center>
        <center>
		<table>
		<tr>
		<td style="width: 3px">
            <asp:Button ID="cmd_GListOpt" runat="server" BorderStyle="None" TabIndex="1" Text="Select" Width="88px" />
                </td>
                <td style="width: 3px">
            <asp:Button ID="cmd_GListExit" runat="server" BorderStyle="None" TabIndex="2" Text="Continue" Width="88px" />
                </td>
        </tr>
               </table> 
               </center>    
            
            
            
		</form>
		&nbsp;
        <br />
			
            
	</body>
</HTML>
