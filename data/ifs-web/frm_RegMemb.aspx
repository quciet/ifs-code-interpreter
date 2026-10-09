<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_RegMemb" Codebehind="frm_RegMemb.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>IFsWeb</title>
    <SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>
</head>
<body leftMargin="0" rightMargin="0" topmargin="0">
    <form id="frm_RegMemb" runat="server">
    <div>
        <asp:Table ID="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%" Font-Bold="True" Font-Size="Medium">
            <asp:TableRow ID="TitleRow" BackColor="#4572A7" Font-Bold="True" Font-Names="Tahoma"
                Font-Size="Medium" ForeColor="White" runat="server">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
                <asp:TableCell ID="TitleCell" Text="Identify Group or Country/Region Members" runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        &nbsp;&nbsp;
        <asp:Panel ID="Panel1" runat="server" Height="48px" Style="z-index: 110; left: 39px;
            position: absolute; top: 35px" Width="384px">
        <asp:RadioButton ID="optUsingRegs" runat="server" AutoPostBack="True"
            Font-Bold="True" Style="z-index: 100; left: 0px; position: absolute; top: 16px"
            Text="Using Regions" GroupName="IdentMem" />
        <asp:RadioButton ID="optUsingGroups" runat="server" AutoPostBack="True" Font-Bold="True"
            Style="z-index: 102; left: 192px; position: absolute; top: 16px" Text="Using Groups" Checked="True" GroupName="IdentMem" />
        </asp:Panel>
        <asp:Label ID="lbl_Names" runat="server" Font-Bold="True" Height="32px" Style="z-index: 103;
            left: 40px; position: absolute; top: 80px" Text="Countries/Regions (click to see members):"
            Width="176px" Visible="False"></asp:Label>
        <asp:Label ID="lbl_Groups" runat="server" Font-Bold="True" Height="40px" Style="z-index: 104;
            left: 40px; position: absolute; top: 80px" Text="Groups (Click to see members):" Width="160px"></asp:Label>
        <asp:Label ID="lbl_regional" runat="server" Font-Bold="True" Style="z-index: 105;
            left: 232px; position: absolute; top: 96px" Text="Country/Region Members:" Visible="False"></asp:Label>
        <asp:Label ID="lbl_GroupReg" runat="server" Font-Bold="True" Style="z-index: 106;
            left: 232px; position: absolute; top: 96px" Text="Group Members:"></asp:Label>
        <asp:Button ID="cmdExit" runat="server" AccessKey="x" Font-Bold="True" Style="z-index: 107;
            left: 66px; position: absolute; top: 463px" Text="E(x)it" Width="112px" />
        <input id="btnHelp" onclick="javascript:ShowHelp();"
                                type="button" value="Help" style="width: 112px; left: 266px; position: absolute; top: 463px" />            
                                <!--RH_ShowHelp(0, '../assets/help/WebHelp/ifshelp.htm' ,HH_HELP_CONTEXT,7506);-->
        <asp:ListBox ID="lst_Names" runat="server" AutoPostBack="True" Font-Bold="True" Height="324px"
            Style="z-index: 108; left: 40px; position: absolute; top: 128px" Width="168px"></asp:ListBox>
        <asp:ListBox ID="lst_Regional" runat="server" Font-Bold="True" Height="324px" Style="z-index: 109;
            left: 232px; position: absolute; top: 128px" Width="168px"></asp:ListBox>
        &nbsp;
    
    </div>
    </form>
</body>
</html>

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
