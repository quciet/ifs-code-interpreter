<%@ Register TagPrefix="twc" Namespace="TMSWebControls" Assembly="TMSWebControls" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_AnalFunc" Codebehind="frm_AnalFunc.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Change Multi-Variate Functions</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
        <%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form runat="server">
            <asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Change Multi-Variate Functions" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            <table>
                <tr>
                    <td >
                        <asp:label id="lbl_names0" Font-Bold="True" Font-Size="9.75" runat="server" Font-Name="Arial">Filter Functions (press enter): </asp:label>
                        &nbsp;<asp:TextBox ID="txtFilter" runat="server" Width="502px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:checkbox id="chkOrdByTime" runat="server" AutoPostBack="True" 
                        Width="173px" Text="Order by Last Change"></asp:checkbox>
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>
    			        <asp:label id="Label1" 
				        runat="server" Width="526px" BackColor="Window" Height="17"
				        Font-Bold="True" Font-Size="9.75" ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False"
				        Font-Italic="False" Font-Name="Arial">Click on Function Name to change b coefficients, click on the value to change a.</asp:label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:button id="cmdContinue" 
				        runat="server" Width="86px" Text="Continue" UseSubmitBehavior="False"></asp:button>
                        &nbsp;&nbsp;&nbsp;
                        <input id="btnHelp" onclick="javascript:ShowHelp();"
                        type="button" value="Help" style="width: 86px;" />
                        <br />
                        <asp:label id="lbl_Eqn" 
				        runat="server" Height="17" Width="526px" BackColor="Window" Text="Countries or Regions" Font-Name="Arial"
				        Font-Size="9.75" Font-Bold="True" Font-Italic="False" Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText">Generic Equation Form</asp:label>
                    </td>

                </tr>
                <tr>
                    <td >
                        <!--jam 2023/03/05-->
                         <asp:GridView ID="GviewAnalFun" runat="server" style="margin-top: 0px" Height="96px" Width="720px" BorderStyle="None" BackColor="White" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BorderWidth="1px" BorderColor="#999999">
                          
                            <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedRowStyle>
				            <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
                            <RowStyle ForeColor="Black" BackColor="#EEEEEE"></RowStyle>

				            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>  

                             <Columns>
                                <asp:ButtonField buttontype="Link" DataTextField="function" HeaderText="Function Name" 
                                     SortExpression="function"  CommandName="FunName" />
                                <asp:ButtonField DataTextField="Intercept" HeaderText="a (intercept)" 
                                    SortExpression="Intercept" CommandName="a"/>
                                <asp:BoundField DataField="R2" HeaderText="R2"
                                    SortExpression="R2" />
                                <asp:BoundField DataField="StandardError" HeaderText="Standard Error" 
                                    SortExpression="StandardError" >
                                    <ItemStyle HorizontalAlign="Right" />
                                 </asp:BoundField>
                                  <asp:BoundField DataField="functionNoVis" HeaderText="Function Name 2" 
                                    SortExpression="functionNoVis" visible ="False">
                                    <ItemStyle HorizontalAlign="Right" />
                                 </asp:BoundField>
                            </Columns>

                              <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999"></PagerStyle>
                         </asp:GridView>

                        <asp:datagrid id=dbgAnalFun runat="server" Height="96px" Width="720px" BorderStyle="None" BackColor="White" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BorderWidth="1px" BorderColor="#999999" DataSource="<%# DsAnalFunc1 %>" DataMember="AnalFunc">
				            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				            <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				            <ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				            <Columns>
					            <asp:ButtonColumn DataTextField="Function Name" HeaderText="Function Name" CommandName="FunName"></asp:ButtonColumn>
					            <asp:BoundColumn Visible="False" DataField="a4" HeaderText="a (intercept)"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="Function Name" SortExpression="Function Name" HeaderText="Function Name"></asp:BoundColumn>
					            <asp:ButtonColumn DataTextField="a4" HeaderText="a (intercept)" CommandName="a"></asp:ButtonColumn>
				                <asp:BoundColumn DataField="R2" DataFormatString="{0:N4}" HeaderText="R^2">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SE" HeaderText="Standard Error"></asp:BoundColumn>
				            </Columns>
				            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			            </asp:datagrid>                        
                    </td>
                    <td valign=top>
                        <asp:Panel ID="pnlFilters" runat="server">
                            <br />
                            <asp:checkbox id="chkRun" runat="server" AutoPostBack="True" Width="132px" 
                                Text="Used in Run" Checked=true></asp:checkbox><br>
                            <asp:checkbox id="chkRunHis" runat="server" AutoPostBack="True" Width="169px" 
                                Text="Used in Historical Run" Checked=true></asp:checkbox>
                           
                            <br>
                            <asp:CheckBox ID="chkPre" runat="server" AutoPostBack="True" Checked="true" Text="Used in Preprocessor" Width="159px" />
                            <br>
                            <asp:CheckBox ID="chkPreHis" runat="server" AutoPostBack="True" Checked="true" Text="Used in Historical Preprocessor" Width="224px" />
                            <br>
                            <asp:CheckBox ID="chkNotUsed" runat="server" AutoPostBack="True" Checked="true" Text="Not Used" Width="132px" />
                            <br>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" Checked="true" Enabled="false" Text="All" Width="132px" />
                            <br></br>
                            </br>
                            </br>
                            </br>
                            </br>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>
                        <asp:label id="lblInstructions" 
				        runat="server" Visible="False" Font-Name="Arial" Font-Size="9.75" Font-Bold="True">Type new value of intercept a</asp:label><br />
                        <asp:label id="lblFunName" 
				        runat="server" Width="480px" Visible="False" Font-Size="9.75" Font-Bold="True" Font-Names="Arial">Label</asp:label>
                
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                
                        <asp:textbox id="txta" runat="server"
				        Height="26px" Width="104px" Visible="False" Font-Size="9.75" Font-Bold="True" Font-Names="Arial"></asp:textbox>                    
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>
                        <asp:Label id="lblIndepVar" 
				        runat="server" Height="24px" Width="432px" Visible="False" Font-Size="9.75" Font-Bold="True"
				        Font-Names="Arial">Label</asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

			            <asp:Label id="lblb" runat="server"
				        Height="32px" Width="33px" Visible="False" Font-Size="9.75" Font-Bold="True" Font-Names="Arial">Label</asp:Label>

			            &nbsp;

			            <asp:TextBox id="txtIndepVar" runat="server" Height="26px" Width="104px" Visible="False"></asp:TextBox>                    
                    </td>
                </tr>
                <tr>
                    <td colspan='2'>

                        <!--jam 2023/03/05-->
                         <asp:GridView ID="GviewIndepVar" runat="server" style="margin-top: 0px" Height="96px" Width="720px" BorderStyle="None" BackColor="White" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BorderWidth="1px" BorderColor="#999999">
                          
                              <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedRowStyle>
				            <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>


				            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>  


                            <SelectedRowStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedRowStyle>
				            <AlternatingRowStyle BackColor="Gainsboro"></AlternatingRowStyle>
				            <RowStyle ForeColor="Black" BackColor="#EEEEEE"></RowStyle>
				            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>


                              <Columns>
					            <asp:BoundField DataField="XVariable" HeaderText="Indep Var"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b1" HeaderText="b1"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b2" HeaderText="b2"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b3" HeaderText="b3"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b4" HeaderText="b4"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b5" HeaderText="b5"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b6" HeaderText="b6"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b7" HeaderText="b7"></asp:BoundField>
					            <asp:BoundField Visible="False" DataField="b8" HeaderText="b8"></asp:BoundField>
                                <asp:ButtonField DataTextField="b1" HeaderText="b1" CommandName="b1"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b2" HeaderText="b2" CommandName="b2"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b3" HeaderText="b3" CommandName="b3"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b4" HeaderText="b4" CommandName="b4"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b5" HeaderText="b5" CommandName="b5"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b6" HeaderText="b6" CommandName="b6"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b7" HeaderText="b7" CommandName="b7"></asp:ButtonField>
					            <asp:ButtonField DataTextField="b8" HeaderText="b8" CommandName="b8"></asp:ButtonField>
				            </Columns>
				           
                              <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999"></PagerStyle>
                         </asp:GridView>


                        <asp:datagrid id=dbgIndepVar runat="server" Height="132px" Width="720px" BorderStyle="None" BackColor="White" Visible="False" AutoGenerateColumns="False" GridLines="Vertical" CellPadding="3" BorderWidth="1px" BorderColor="#999999" DataSource="<%# DsIndepVar1 %>" DataMember="AnalFunc">
				            <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				            <AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
				            <ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
				            <HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
				            <FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
				            <Columns>
					            <asp:BoundColumn DataField="Indep Var" HeaderText="Indep Var"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b1" HeaderText="b1"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b2" HeaderText="b2"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b3" HeaderText="b3"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b4" HeaderText="b4"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b5" HeaderText="b5"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b6" HeaderText="b6"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b7" HeaderText="b7"></asp:BoundColumn>
					            <asp:BoundColumn Visible="False" DataField="b8" HeaderText="b8"></asp:BoundColumn>
					            <asp:ButtonColumn DataTextField="b1" HeaderText="b1" CommandName="b1"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b2" HeaderText="b2" CommandName="b2"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b3" HeaderText="b3" CommandName="b3"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b4" HeaderText="b4" CommandName="b4"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b5" HeaderText="b5" CommandName="b5"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b6" HeaderText="b6" CommandName="b6"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b7" HeaderText="b7" CommandName="b7"></asp:ButtonColumn>
					            <asp:ButtonColumn DataTextField="b8" HeaderText="b8" CommandName="b8"></asp:ButtonColumn>
				            </Columns>
				            <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages"></PagerStyle>
			            </asp:datagrid>                    
                    </td>
                </tr>
            </table>
			
            				
                <!-- // RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,16023 -->
			
			<script language="javascript">
{
	if ((document.forms[0].txta != null)&& (document.forms[0].txta.disabled == false ) ) 
          {
               document.forms[0].txta.focus();
          }
    else
    {
		if ((document.forms[0].txtIndepVar != null)&& (document.forms[0].txtIndepVar.disabled == false ) ) 
          {
               document.forms[0].txtIndepVar.focus();
          }
    }
}
			</script>

			<script language="javascript">
			    {
			        if ((document.forms[0].txtFilter != null) && (document.forms[0].txtFilter.disabled == false)) {
			            document.forms[0].txtFilter.focus();
			        }
			        //jrs 2012/01/16
			        function clickButton(e, buttonid) {
			            var evt = e ? e : window.event;
			            var bt = document.getElementById(buttonid);
			            if (bt) {
			                if (evt.keyCode == 13) {
			                    bt.click();
			                    return false;
			                }
			            }
			        }
			    }
			</script>

		</form>
	</body>
</HTML>

<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-3138457-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>


