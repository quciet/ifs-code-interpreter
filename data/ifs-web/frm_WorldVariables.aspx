<%@ Reference Page="~/frm_DisplayMenu.aspx" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cc2" Namespace="CustomDropDownList.Thycotic.Web.UI.WebControls" Assembly="CustomDropDownList" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_WorldVariables" Codebehind="frm_WorldVariables.aspx.vb" EnableEventValidation="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>World Forecast Variables</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>	
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
	</HEAD>
	<body leftMargin="0" rightMargin="0" topmargin="0">
		<script language="javascript">
{
	// jrs 09/06/04
	function updateVarNameIdx_onclick(e) {
		// handle combobox 
		var cmb;
		cmb = document.getElementById('cmb_VarName');
		var lst;
		lst = document.getElementById('lstVarName');
		  
		//cmb.selectedIndex = lst.selectedIndex + 1; 
		cmb.selectedIndex = -1; 
		return true; 
	} 
}
		</script>
		<form id="Form1" method="post" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="World Forecast Variables" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False"></cswm:webmenu>

                <table>
                    <tr>
                        <td width = '16px'></td>
                        <td>
                            <asp:label id="lbl_VarName" 
				            tabIndex="3" runat="server" Height="17px" Width="473" BorderStyle="None" Visible="False" BackColor="Window" ForeColor="WindowText" Font-Underline="False" Enabled="True" Font-Strikeout="False"
				            Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Label1"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td width = '16px'></td>
                        <td>
                        			<cc2:customdropdownlist id="cmb_VarName" 
                    				tabIndex="1" runat="server" Height="24px" Width="610px" AutoPostBack="True"></cc2:customdropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td width = '16px'></td>
                        <td>
			                <asp:listbox id="lstVarName" 
				                runat="server" Height="88px" Width="608px" AutoPostBack="True"></asp:listbox>
                        </td>
                    </tr>
                    <tr>
                        <td width = '16px'></td>
                        <td>
                        			<asp:label id="lbl_Dim1" 
				tabIndex="4" runat="server" Height="17" Width="273" BorderStyle="None" Visible="False"
				BackColor="Window" ForeColor="WindowText" Font-Underline="False" Enabled="True" Font-Strikeout="False"
				Font-Italic="False" Font-Bold="False" Font-Size="8.0" Font-Name="Arial" Text="Label2"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td width = '16px'></td>
                        <td>
			                <cc2:customdropdownlist id="cmb_DIM1" 
				                tabIndex="2" runat="server" Height="24px" Width="197px" Visible="False" AutoPostBack="True"></cc2:customdropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td width = '16px'></td>
                        <td>
		                    <asp:listbox id="lstDIM1" runat="server"
			                    Height="72px" Width="200px" Visible="False" AutoPostBack="True"></asp:listbox>
                        </td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td width = '24px'>
                        </td>
                        <td>
			                <asp:label id="lbl_files" 
				                tabIndex="5" runat="server" Height="16px" Width="225px" BorderStyle="None" BackColor="Window"
				                ForeColor="WindowText" Font-Underline="False" Font-Strikeout="False" Font-Italic="False"
				                Font-Bold="False" Font-Size="8pt" Font-Name="Arial" Text="Default File for Display:" Font-Names="Arial"></asp:label>
                        </td>
                        <td width = '24px'>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td width = '24px'>
                        </td>
                        <td>
			                <asp:dropdownlist id="cboFiles" 
				                runat="server" Width="329" Visible="True" BackColor="Window" ForeColor="WindowText"
				                Font-Underline="False" Enabled="True" Font-Strikeout="False" Font-Italic="False" Font-Bold="False"
				                Font-Size="8.0" Font-Name="Arial" AutoPostBack="True"></asp:dropdownlist>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td width = '24px'>
                        </td>
                        <td>
                        </td>
                        <td width = '24px'>
                        </td>
                        <td>
			                <asp:panel id="Panel1" runat="server"
				                Height="88px" Width="216px">
				                <P>
					                <asp:RadioButton id="optMap" runat="server" Text="Map" GroupName="Gini" 
                                        Checked="True"></asp:RadioButton></P>
				                <P>
					                <asp:RadioButton id="optLorenz" runat="server" Text="Lorenz" GroupName="Gini"></asp:RadioButton></P>
				                <P>
					                <asp:RadioButton id="optGini" runat="server" Text="Gini" GroupName="Gini"></asp:RadioButton></P>
				                <P>
					                <asp:RadioButton id="optHistogram" runat="server" Text="Histogram" GroupName="Gini"></asp:RadioButton></P>	
			                </asp:panel>

                        </td>
                    </tr>
                </table>


			<script language="javascript">
			    var myWidth = 0, myHeight = 0;
			    if (typeof (window.innerWidth) == 'number') {
			        //Non-IE
			        myWidth = window.innerWidth;
			        myHeight = window.innerHeight;
			    } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
			        //IE 6+ in 'standards compliant mode'
			        myWidth = document.documentElement.clientWidth;
			        myHeight = document.documentElement.clientHeight;
			    } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
			        //IE 4 compatible
			        myWidth = document.body.clientWidth;
			        myHeight = document.body.clientHeight;
			    }
			    //jrs 2011/03/22
			    if (myWidth == 0) {
			        myWidth = screen.width;
			        myHeight = screen.height;
			    }

			    //resize the variable list box
			    var room = myHeight - (300);
			    if (document.forms[0].lstVarName != null) {
			        document.getElementById('lstVarName').style.height = room + "px";
			    }
			    if (document.forms[0].lstDIM1 != null) {
			        document.getElementById('lstDIM1').style.height = room + "px";
			    }

	if ((document.forms[0].cmb_VarName.disabled == false))
    {
		document.forms[0].cmb_VarName.focus();
    }
    else
    {
		if ((document.forms[0].cmb_DIM1.disabled == false))
		{
			document.forms[0].cmb_DIM1.focus();
		}
    }
    
if (document.forms[0].cmb_VarName.captureEvents!=null)
            document.forms[0].cmb_VarName.captureEvents(Event.KEYPRESS)
document.forms[0].cmb_VarName.onkeypress = updateVarName;
            
if (document.forms[0].cmb_DIM1!=null)
{
	if (document.forms[0].cmb_DIM1.captureEvents!=null)
            document.forms[0].cmb_DIM1.captureEvents(Event.KEYPRESS)
	document.forms[0].cmb_DIM1.onkeypress = updateDIM1;
}

if (document.forms[0].lstVarName != null)
{
	if (document.forms[0].lstVarName.captureEvents!=null)
			document.forms[0].lstVarName.captureEvents(Event.MOUSEDOWN)
	document.forms[0].lstVarName.onmousedown = updateVarNameIdx_onclick;
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