<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_Cyber" Codebehind="frm_Cyber.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
	<HEAD>
		<title>Cyber Opportunity and Risk Displays</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/DimsInOrderChk.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/CyberDisplaysFx.js"></script>
        <!---------------------JAM 2013-03-29------------------------>
	

	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0" onload="init();javascript:SetLists();">

    <!---------------------JAM 2013-03-29------------------------>
 <div id="loading" style="position:absolute; width:100%; text-align:center; top:300px;"><img src="images/squares-circle.gif"  height="93" width="93"><br>Loading resources...</div>
<script type="text/javascript">
   
    var ld = (document.all);


    var ns4 = document.layers;
    var ns6 = document.getElementById && !document.all;
    var ie4 = document.all;


    if (ns4)
        ld = document.loading;
    else if (ns6)
        ld = document.getElementById("loading").style;
    else if (ie4)
        ld = document.all.loading.style;


    function init() {
        if (ns4) { ld.visibility = "hidden"; }
        else if (ns6 || ie4) ld.display = "none";
    }
</script>
    <!----------------------------------------------------------->

		<form id="Form1" method="post" runat="server">
	        <input id="txtVarNames" name="txtVarNames"  type="hidden" runat="server"/>
	        <input id="txtHistFor" name="txtHistFor"  type="hidden" runat="server"/>
	        <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
            <input id="txtDims" name="txtDims"  type="hidden" runat="server"/>

			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Cyber Opportunity and Risk Displays" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 113; LEFT: 0px; POSITION: absolute; TOP: 16px"
					runat="server" MenuBar-AbsoluteDockEnabled="False" MenuBar-AbsoluteDragEnabled="False"></cswm:webmenu>
						
			<asp:panel id="frm_Horizon" style="Z-INDEX: 125; LEFT: 0px; POSITION: absolute; TOP: 0px"
				tabIndex="3" runat="server" Height="1720" Width="3000" Font-Size="8.0" ForeColor="WindowText"
				Visible="False" Font-Bold="False" BackColor="Window" Font-Underline="False" Font-Name="Arial"
				Font-Italic="False" Font-Strikeout="False" Enabled="True">
				Change Horizon Year: <BR>
				<asp:TextBox ID="txtHorizon" runat="server"></asp:TextBox><asp:Button ID="cmdHorizon"
                    runat="server" Text="Submit" /><br />
                    <asp:DropDownList id="cmb_Horizon" tabIndex="4" runat="server"
					Font-Bold="False" Width="153" Visible="True" ForeColor="WindowText"
					Font-Size="8.0" Enabled="True" Font-Strikeout="False" Font-Italic="False" Font-Name="Arial"
					Font-Underline="False" BackColor="Window" AutoPostBack="True"></asp:DropDownList>
			</asp:panel>

			<div id="div_dims" style="display:none">
			    <table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
				    <tr><td style="height: 76px">
                            <asp:Panel ID="frmGroupOpt" runat="server" Height="32px" Style="z-index: 100; left: 0px;
                                position: absolute; top: 65px" Width="900px" Font-Names="Arial">
                                <asp:label id="Label1" style="Z-INDEX: 114; LEFT: 8px; POSITION: absolute; TOP: 8px" runat="server"
				                Font-Bold="True" Font-Names="Arial">Geography Options:</asp:label>
                                <asp:RadioButton ID="optReg" runat="server" AccessKey="r" Checked="True" GroupName="GroupingOptions"
                                    Style="z-index: 100; left: 163px; position: absolute; top: 8px" Text="Country"
                                    ToolTip="Loads single Country (Alt+R)" />
                                <asp:RadioButton ID="optGroup" runat="server" AccessKey="g" GroupName="GroupingOptions"
                                    Style="z-index: 101; left: 307px; position: absolute; top: 8px" Text="Groups"
                                    ToolTip="Loads single Group (Alt+G)"/>
                                <asp:RadioButton ID="optGroupDec" runat="server" AccessKey="e" GroupName="GroupingOptions"
                                    Style="z-index: 102; left: 395px; position: absolute; top: 8px" Text="Decomposed Groups"
                                    ToolTip="Decomposes a Group into its Member Countries (Alt+E)" />
                                <asp:RadioButton ID="optGList" runat="server" AccessKey="l" GroupName="GroupingOptions"
                                    Style="z-index: 104; left: 577px; position: absolute; top: 8px" Text="Geography-Lists"
                                    ToolTip="Loads a predefined list of Countries and/or Groups (Alt+L)" />
                            </asp:Panel>
                        </td>
				    </tr>
			    </table>

			    <asp:label id="lblMsg" style="Z-INDEX: 111; LEFT: 31px; POSITION: absolute; TOP: 604px" runat="server" Font-Names="Arial"
				Height="24px" Width="424" Font-Size="Large" ForeColor="Red" Visible="False" Font-Bold="True">Label</asp:label>

				<asp:label id="lblDim" style="Z-INDEX: 102; LEFT: 9px; POSITION: absolute; TOP: 100px" runat="server"
				Font-Bold="True" Font-Names="Arial">Geography:</asp:label>
				<asp:Panel id="divDim" runat="server" style="overflow:auto; height:250px; Z-INDEX: 110; LEFT: 9px; POSITION: absolute; TOP: 128px">
                    <asp:CheckBoxList id="lstDim" 
				    runat="server" Height="298" Width="168" SelectionMode="Multiple"></asp:CheckBoxList>
				</asp:Panel>

				<asp:label id="lblInstructions" style="Z-INDEX: 101; LEFT: 209px; POSITION: absolute; TOP: 100px" runat="server"
				Width="500px" Font-Bold="True" Font-Names="Arial">Forecast Variables:</asp:label>
			    <asp:listbox id="lstVars" style="Z-INDEX: 108; LEFT: 209px; POSITION: absolute; TOP: 128px" runat="server"
				Height="298" Width="456" Font-Names="Arial"></asp:listbox>
				<asp:label id="lblInstructionsH" style="Z-INDEX: 101; LEFT: 209px; POSITION: absolute; TOP: 100px" runat="server"
				Width="500px" Font-Bold="True" Font-Names="Arial" Visible="false">Historical Variables:</asp:label>
			    <asp:listbox id="lstVarsH" style="Z-INDEX: 108; LEFT: 209px; POSITION: absolute; TOP: 128px" runat="server"
				Height="298" Width="456" SelectionMode="Multiple" Font-Names="Arial" Visible="false"></asp:listbox>
				<asp:label id="lblPars" style="Z-INDEX: 101; LEFT: 209px; POSITION: absolute; TOP: 450px" runat="server"
				Width="448" Font-Bold="True" Font-Names="Arial">Parameters (click to edit):</asp:label>
				<asp:listbox id="lstPars" style="Z-INDEX: 109; LEFT: 209px; POSITION: absolute; TOP: 470px" runat="server"
				Height="106" Width="456" SelectionMode="Multiple"></asp:listbox>
			
			    <asp:label id="lblFiles" style="Z-INDEX: 114; LEFT: 705px; POSITION: absolute; TOP: 100px" runat="server"
				Font-Bold="True" Font-Names="Arial">Scenario Files:</asp:label>
		        <asp:listbox id="lstFiles" style="Z-INDEX: 100; LEFT: 705px; POSITION: absolute; TOP: 128px" runat="server"
				    Height="298" Width="168" SelectionMode="Multiple"></asp:listbox>
				    
				<asp:button id="cmdContinue" style="Z-INDEX: 104; LEFT: 576px; POSITION: absolute; TOP: 546px"
				tabIndex="5" runat="server" Width="112px" Text="Continue" Visible="False" Font-Names="Arial"></asp:button>
				<asp:button id="cmdLine" style="Z-INDEX: 106; LEFT: 31px; POSITION: absolute; TOP: 582px" tabIndex="1"
				runat="server" Width="112px" Text="Line Graph" Font-Names="Arial"></asp:button>
				<asp:button id="cmdTable" style="Z-INDEX: 107; LEFT: 159px; POSITION: absolute; TOP: 582px" runat="server"
				Width="112px" Text="Table"></asp:button>
                <asp:button id="cmdLineAC" style="Z-INDEX: 106; LEFT: 287px; POSITION: absolute; TOP: 582px" tabIndex="1"
				runat="server" Width="112px" Text="Line Graph AC" Font-Names="Arial"></asp:button>
                <asp:button id="cmdLineFC" style="Z-INDEX: 106; LEFT: 415px; POSITION: absolute; TOP: 582px" tabIndex="1"
				runat="server" Width="112px" Text="Line Graph FC" Font-Names="Arial"></asp:button>

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