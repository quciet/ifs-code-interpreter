<%@ Reference Page="~/Index.aspx" %>
<%@ Reference Page="~/frm_Message.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_TransformPy" Codebehind="TransformPy.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Transformation Options</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/28-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0">
		<form runat="server">
			<asp:table id="WinFormsToWeb_PageBar" style="Z-INDEX: 129; LEFT: 1px; POSITION: absolute; TOP: 0px"
				runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Transformation Options" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			
				
				<table width=100%>
				    <tr>
				        <td colspan=12 align=center>
				            <br />
			                <asp:label id="Label1" 
			                tabIndex="13" runat="server" Width="185" Height="17" BackColor="White" Text="TRANSFORMATION OPTIONS" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
			                Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				            
				        </td>
				    </tr>
				    <tr>
				        <td width=3%></td>
				        <td width=29%>
    			            <asp:label id="Label2" tabIndex="1" runat="server" Width="145" Height="17" BackColor="White" Text="VARIABLES CHOSEN"
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				            
				        </td>
				        <td width=6%>
				            <asp:label id="Label13" tabIndex="12" runat="server" Width="41" Height="17" BackColor="White" Text="NONE" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				        
				        </td>
				        <td width=6%>
                            <asp:label id="Label3" tabIndex="3" runat="server" Width="33" Height="17" BackColor="White" Text="LOG" 
                            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				        
				        </td>
				        <td width=6%>
                            <asp:label id="Label4" tabIndex="2" runat="server" Width="33" Height="17" BackColor="White" Text="X ^ 2" 
                            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				        
				        </td>
				        <td width=6%>
				            <asp:label id="Label5" tabIndex="4" runat="server" Width="33" Height="17" BackColor="White" Text="X ^ b" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>
				        </td>
				        <td width=6%>
				            <asp:label id="Label6" tabIndex="5" runat="server" Width="33" Height="17" BackColor="White" Text="e ^ X" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
		        		    Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>
				        </td>
				        <td width=6%>
				            <asp:label id="Label7" tabIndex="5" runat="server" Width="33" Height="17" BackColor="White" Text="Logistic" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
		        		    Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				        
				        </td>
				        <td width=6%>
				            <asp:label id="Label9" tabIndex="5" runat="server" Width="33" Height="17" BackColor="White" Text="X^2 (Pol)" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
		        		    Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>
				        </td>
				        <td width=6%>
				            <asp:label id="Label10" tabIndex="5" runat="server" Width="33" Height="17" BackColor="White" Text="X^3 (Pol)" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
		        		    Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>
				        </td>
				        <td>
				            <asp:label id="Label8" tabIndex="52" runat="server" Width="25" Height="17" BackColor="White" Text="b" 
				            Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>				        
				        </td>
				        <td>
                            <asp:Label id="lblBTitle" runat="server" Height="24px" Width="150px" Font-Bold="True" Font-Size="X-Small" Font-Names="Arial"
		                    Visible="False">Enter a value for b</asp:Label>				        
				        </td>
				    </tr>
				    <tr>
				        <td width=3%></td>
				        <td width=29%>
                            <asp:label id="lbl_dependent" tabIndex="6" runat="server" Width="193" Height="33" BackColor="White" Text="DEPENDENT =" 
                            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="True"></asp:label>								        
				        </td>
				        <td width=6%>
				            <asp:RadioButton id="_Dependent_4" tabIndex="24" runat="server" Height="17" Width="17" Text=" " Visible="True"
					        ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White"
					        AutoPostBack="True" GroupName="_Frames_0" Checked="False"></asp:RadioButton>
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_0" tabIndex="20" runat="server" Text=" " Visible="True" ForeColor="ControlText"
					        Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
					        Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White" AutoPostBack="True"
					        GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_1" tabIndex="21" runat="server" Text=" " Visible="True" ForeColor="ControlText"
                            Enabled="True" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
                            Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White" AutoPostBack="True"
                            GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_2" tabIndex="22" runat="server" Text=" " Visible="True"
					        ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White"
					        AutoPostBack="True" GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_3" tabIndex="23" runat="server" Height="17" Width="17" Text=" " Visible="True"
					        ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White"
					        AutoPostBack="True" GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_5" tabIndex="24" runat="server" Height="17" Width="17" Text=" " Visible="True"
					        ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White"
					        AutoPostBack="True" GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_6" tabIndex="24" runat="server" Height="17" Width="17" Text=" " Visible="False"
					        ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White"
					        AutoPostBack="True" GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Dependent_7" tabIndex="24" runat="server" Height="17" Width="17" Text=" " Visible="False"
					        ForeColor="ControlText" Enabled="True" Font-Underline="False" Font-Strikeout="False"
					        Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" BackColor="White"
					        AutoPostBack="True" GroupName="_Frames_0" Checked="False"></asp:RadioButton>				        
				        </td>
				        <td>
				            <asp:label id="lbl_Dependentb" tabIndex="50" runat="server" Width="17" Height="17" BackColor="White" Text="3" 
				            Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td>
    			            <asp:TextBox id="txt_Dependentb" runat="server" MaxLength="3" Visible="False" Width="64"></asp:TextBox>				        
    			            <asp:Button id="butDep" runat="server" Visible="False" Text="Ok"></asp:Button>
				        </td>
				    </tr>
				    
				    
				    <tr>
				        <td width=3%></td>				    
				        <td width=29%>
				            <asp:label id="_Independent_0" tabIndex="7" runat="server" Width="193" Height="33" BackColor="White" Text="INDEPENDENT1 =" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_4" tabIndex="29" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_0" tabIndex="25" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_1" tabIndex="26" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>									        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_2" tabIndex="27" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_3" tabIndex="28" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_5" tabIndex="28" runat="server" Height="17" Width="17" Text=" "
					        Visible="False" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_6" tabIndex="28" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent1_7" tabIndex="28" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_1"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td>
				            <asp:label id="lbl_Independent1b" tabIndex="51" runat="server" Width="17" Height="17" BackColor="White" Text="3" 
				            Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>																        
				        </td>
				        <td>
                            <asp:TextBox id="txt_Independent1b" runat="server" Visible="False" MaxLength="3" Width="64"></asp:TextBox>							        
                            <asp:Button id="butInd1" runat="server" Visible="False" Text="Ok"></asp:Button>
				        </td>				    
				    </tr>
				</table>							
							
			<asp:panel id="_Frames_2" width=100%
				tabIndex="16" runat="server" BackColor="White" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False">
				
                <table width=100%>
				    <tr>
				        <td width=3%></td>				    
				        <td width=29%>
				            <asp:label id="_Independent_1" tabIndex="8" runat="server" Width="193" Height="33" BackColor="White" Text="INDEPENDENT2 =" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>															        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_4" tabIndex="34" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_0" tabIndex="30" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_1" tabIndex="31" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_2" tabIndex="32" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_3" tabIndex="33" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_5" tabIndex="33" runat="server" Height="17" Width="17" Text=" "
					        Visible="False" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_6" tabIndex="33" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent2_7" tabIndex="33" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_2"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td>
			                <asp:label id="lbl_Independent2b" tabIndex="53" runat="server" Width="17" Height="17" BackColor="White" Text="3" 
			                Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
			                Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td>
				            <asp:TextBox id="txt_Independent2b" runat="server" Visible="False" MaxLength="3" Width="64"></asp:TextBox>
    			            <asp:Button id="butInd2" runat="server" Visible="False" Text="Ok"></asp:Button>
				        </td>				    
				    </tr>
				    
                </table>	
                			 
			</asp:panel>
			
			<asp:panel id="_Frames_3" width=100%
				tabIndex="17" runat="server" BackColor="White" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"> 				
				<table width=100%>
				    <tr>
				        <td width=3%></td>				    
				        <td width=29%>
				            <asp:label id="_Independent_2" tabIndex="9" runat="server" Width="193" Height="33" BackColor="White" Text="INDEPENDENT3 =" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent3_4" tabIndex="39" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent3_0" tabIndex="35" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent3_1" tabIndex="36" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent3_2" tabIndex="37" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent3_3" tabIndex="38" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent3_5" tabIndex="39" runat="server" Height="17" Width="17" Text=" "
					        Visible="False" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td style="width: 6%">
                            <asp:RadioButton id="_Independent3_6" tabIndex="39" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td style="width: 6%">
                            <asp:RadioButton id="_Independent3_7" tabIndex="39" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_3"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td>
				            <asp:label id="lbl_Independent3b" tabIndex="54" runat="server" Width="17" Height="17" BackColor="White" Text="3" 
				            Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td>
				            <asp:TextBox id="txt_Independent3b" runat="server" Visible="False" MaxLength="3" Width="64"></asp:TextBox>
			                <asp:Button id="butInd3" runat="server" Visible="False" Text="Ok"></asp:Button>
				        </td>				    
				    </tr>								    				
				</table>
			</asp:panel>

			<asp:panel id="_Frames_4" width=100%
				tabIndex="18" runat="server" BackColor="White" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False">
				
				<table  width=100%>
				<tr>
				        <td width=3%></td>				    
				        <td width=29%>
				            <asp:label id="_Independent_3" tabIndex="10" runat="server" Width="193" Height="33" BackColor="White" Text="INDEPENDENT4 =" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_4" tabIndex="44" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_0" tabIndex="40" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_1" tabIndex="41" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_2" tabIndex="42" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_3" tabIndex="43" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_5" tabIndex="44" runat="server" Height="17" Width="17" Text=" "
					        Visible="False" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_6" tabIndex="44" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent4_7" tabIndex="44" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_4"
					        Checked="False"></asp:RadioButton>				        				        
				        </td>
				        <td>
                            <asp:label id="lbl_Independent4b" tabIndex="55" runat="server" Width="17" Height="17" BackColor="White" Text="3" 
                            Font-Name="Arial" Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td>
    			            <asp:TextBox id="txt_Independent4b" runat="server" Visible="False" MaxLength="3" Width="64"></asp:TextBox>				        
			                <asp:Button id="butInd4" runat="server" Visible="False" Text="Ok"></asp:Button>    			            
				        </td>				    
				    </tr>				    
				</table> 
			</asp:panel>
			
			<asp:panel id="_Frames_5" width=100%
				tabIndex="19" runat="server" BackColor="White" Font-Name="Arial"
				Font-Size="8.0" Font-Bold="False" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False">
				
				<table width=100%>
				    <tr>
				        <td width=3%></td>				    				    
				        <td width=29%>
				            <asp:label id="_Independent_4" tabIndex="11" runat="server" Width="193" Height="33" BackColor="White" Text="INDEPENDENT5 =" 
				            Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				            Font-Underline="False" Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_4" tabIndex="49" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_0" tabIndex="45" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_1" tabIndex="46" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_2" tabIndex="47" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_3" tabIndex="48" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_5" tabIndex="49" runat="server" Height="17" Width="17" Text=" "
					        Visible="False" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_6" tabIndex="49" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td width=6%>
                            <asp:RadioButton id="_Independent5_7" tabIndex="49" runat="server" Height="17" Width="17" Text=" "
					        Visible="True" ForeColor="ControlText" Enabled="True" Font-Underline="False"
					        Font-Strikeout="False" Font-Italic="False" Font-Bold="False" Font-Size="8.0"
					        Font-Name="Arial" BackColor="White" AutoPostBack="True" GroupName="_Frames_5"
					        Checked="False"></asp:RadioButton>				        
				        </td>
				        <td>
    			            <asp:label id="lbl_Independent5b" tabIndex="56" runat="server" Width="17" Height="17" BackColor="White" Text="3" Font-Name="Arial"
				            Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
				            Enabled="True" ForeColor="ControlText" BorderStyle="None" Visible="False"></asp:label>				        
				        </td>
				        <td>
    			            <asp:TextBox id="txt_Independent5b" runat="server" Visible="False" MaxLength="3" Width="64"></asp:TextBox>				        
	            			<asp:Button id="butInd5" runat="server" Visible="False" Text="Ok"></asp:Button>
				        </td>				    
				    </tr>				
				</table>
			</asp:panel>

				
			<asp:button id="cmd_Return" style="Z-INDEX: 102; LEFT: 24px; POSITION: absolute; TOP: 399px; TEXT-ALIGN: center"
				tabIndex="57" runat="server" Width="217" Height="33" Text="Return To Variable Selection"
				Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" CausesValidation="True" Enabled="True" ForeColor="ControlText"></asp:button>
			<asp:button id="cmd_Continue" style="Z-INDEX: 101; LEFT: 336px; POSITION: absolute; TOP: 399px; TEXT-ALIGN: center"
				tabIndex="58" runat="server" Width="217" Height="33" Text="Compute and Show Statistics" Font-Name="Arial" Font-Size="8.25" Font-Bold="True" Font-Italic="False" Font-Strikeout="False"
				Font-Underline="False" CausesValidation="True" Enabled="True" ForeColor="ControlText"></asp:button>
            <input id="btnHelp" onclick="javascript:ShowHelp();"
                                type="button" value="Help" style="width: 217px; LEFT: 636px; POSITION: absolute; TOP: 399px; height:33px;" />				
                                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16051);-->
			
			<script language="javascript">
{
	if (document.forms[0].txt_Dependentb != null) 
          {
               document.forms[0].txt_Dependentb.focus();
          }
	if (document.forms[0].txt_Independent1b != null) 
          {
               document.forms[0].txt_Independent1b.focus();
          }
	if (document.forms[0].txt_Independent2b != null) 
          {
               document.forms[0].txt_Independent2b.focus();
          }
	if (document.forms[0].txt_Independent3b != null) 
          {
               document.forms[0].txt_Independent3b.focus();
          }
	if (document.forms[0].txt_Independent4b != null) 
          {
               document.forms[0].txt_Independent4b.focus();
          }
	if (document.forms[0].txt_Independent5b != null) 
          {
               document.forms[0].txt_Independent5b.focus();
          }
}
			</script>
		</form>
	</body>
</HTML>
