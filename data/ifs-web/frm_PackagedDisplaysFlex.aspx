<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="IFsWeb.frm_PackagedDisplaysFlex" Codebehind="frm_PackagedDisplaysFlex.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
	<HEAD>
		<title>Flexible Displays</title>
		<SCRIPT language="JavaScript1.2" src="RoboHelp_CSH.js" type="text/javascript">
		</SCRIPT>		
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
        <!--jam 2021/11/27-->
		<%--<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<script language="javascript" type="text/javascript" src="Scripts/FilesInOrder.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/DimsInOrder.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/ListsInOrder.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/FlexDisplaysFx.js"></script>
        <!---------------------JAM 2013-03-29------------------------>
		<link rel="icon" href="http://localhost/ifs/ifsweb/images/favicon.ico">

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
	        <input id="txtListNames" name="txtListNames"  type="hidden" runat="server"/>
	        <input id="txtHistFor" name="txtHistFor"  type="hidden" runat="server"/>
	        <input id="txtFiles" name="txtFiles"  type="hidden" runat="server"/>
            <input id="txtDims" name="txtDims"  type="hidden" runat="server"/>
			<!--jrs 2023/09/20 -->
			<input id="txtDims2" name="txtDims2"  type="hidden" runat="server"/>
            <input id="txtPacks" name="txtPacks"  type="hidden" runat="server"/>
            <input id="txtDirtyDim" name="txtDirtyDim"  type="hidden" runat="server"/>
			<!--cdv 2021/02/02 Hidden element to store global var in -->
			<input id ="MaleFirst" name="MaleFirst" type ="hidden" runat ="server" />
			<!-- dsb 2025/11/4 Hidden element to store groups -->
			<!--<input id ="hdn_bUseGroup" name="hdn_bUseGroup" type="hidden" runat="server" />-->
			<input id="hdn_geoMode" name="hdn_geoMode" type="hidden" runat="server" />

          

			<asp:table id="WinFormsToWeb_PageBar" runat="server" CellSpacing="0" Height="15px" Width="100%">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                   <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Flexible Displays" ID="TitleCell"></asp:TableCell>
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
				    <tr><td style="height: 40px">
                            <asp:Panel ID="frmGroupOpt" runat="server" CssClass="group-options" Font-Names="Arial"> <!--CDV 2021/08/23-->
                                <asp:label id="Label1" runat="server"
				                Font-Bold="True" Font-Names="Arial">Geography Options:</asp:label>
                                <asp:RadioButton ID="optReg" runat="server" AccessKey="r" Checked="True" GroupName="GroupingOptions"
                                    Text="Country" ToolTip="Loads single Country (Alt+r)" />
                                <asp:RadioButton ID="optGroup" runat="server" AccessKey="g" GroupName="GroupingOptions"
                                    Text="Group" ToolTip="Loads single Group (Alt+g)"/>
                                <asp:RadioButton ID="optGroupDec" runat="server" AccessKey="e" GroupName="GroupingOptions"
                                    Text="Decomposed Group" ToolTip="Decomposes a Group into its Member Countries (Alt+e)" />
                                <asp:RadioButton ID="optGList" runat="server" AccessKey="l" GroupName="GroupingOptions"
                                    Text="Geography-List" ToolTip="Loads a predefined list of Countries and/or Groups (Alt+l)" />
                                <asp:RadioButton ID="optCtryAndGrp" runat="server" AccessKey="a" GroupName="GroupingOptions"
                                    Text="Country and Group" ToolTip="Loads both Countries and Groups (Alt+a)" /><!--CDV 2021/08/23-->
                                <asp:RadioButton ID="optCtryAndGrpDec" runat="server" AccessKey="b" GroupName="GroupingOptions"
                                    Text="Country and Decomposed Group" ToolTip="Loads both Countries and Decomposed Groups (Alt+b)" /><!--jrs 2025/05/30-->
								<!--CDV 2021/08/23--> <asp:CheckBox ID ="chkHideNonCoreGroups" runat ="server" AccessKey ="h" Text ="Hide Non-Core Groups"  Checked ="true"/>
								<!--DSB 2025/08/08--> <asp:CheckBox ID ="chkViewHistoryForecasts" runat ="server" AccessKey ="v" Text ="View History plus Forecast"  Checked ="true" />
                            </asp:Panel>
							
                        </td>
				    </tr>
			    </table>

			    <asp:label id="lblMsg" style="Z-INDEX: 111; LEFT: 31px; POSITION: absolute; TOP: 604px" runat="server" Font-Names="Arial"
				Height="24px" Width="424" Font-Size="Large" ForeColor="Red" Visible="False" Font-Bold="True">Label</asp:label>

				<!--DSB 2025/07/28-->
				<asp:label id="lblTopCategories" style="Z-INDEX: 102; LEFT: 9px; POSITION: absolute; TOP: 100px" runat="server"
				Font-Bold="True" Font-Names="Arial">Display Macro Categories:</asp:label> <!--DSB 2025/08/19 Changed the label-->
				<asp:listbox id="lstTopCategories" style="Z-INDEX: 110; LEFT: 9px; POSITION: absolute; TOP: 128px"
				runat="server" Height="128" Width="168" SelectionMode="Multiple"></asp:listbox>

				<asp:label id="lblCategories" style="Z-INDEX: 101; LEFT: 9px; POSITION: absolute; TOP: 270px" runat="server"
				Font-Bold="True" Font-Names="Arial">Display Meso Categories:</asp:label> <!--DSB 2025/08/19 Changed the label-->
				<asp:listbox id="lstCategories" style="Z-INDEX: 109; LEFT: 9px; POSITION: absolute; TOP: 290px; height: 128px"
				runat="server" Height="128" Width="168" SelectionMode="Multiple"></asp:listbox>
				<asp:label id="lblRunHorizon" style="Z-INDEX: 101; LEFT: 9px; POSITION: absolute; TOP: 450px" runat="server"
				Width="448" Font-Bold="True" Font-Names="Arial">Horizon:</asp:label>
				<asp:listbox id="lstRunHorizon" style="Z-INDEX: 109; LEFT: 9px; POSITION: absolute; TOP: 470px" runat="server"
				Height="106" Width="168"></asp:listbox>

				<asp:label id="lblInstructions" style="Z-INDEX: 101; LEFT: 209px; POSITION: absolute; TOP: 100px" runat="server"
				Width="500px" Font-Bold="True" Font-Names="Arial">Displays:</asp:label>
				
				<!--MTI 2026/05/18 Searchbox-->
                        <asp:TextBox 
                              ID="txtPackSearch" 
                              runat="server"
                              Style="Z-INDEX: 102; LEFT: 575px; POSITION: absolute; TOP: 100px; WIDTH: 300px"
                              Font-Names="Arial"
                              onkeyup="selectMatchingPack();" />
                   <!--MTI 2026/05/18 Searchbox-->

			    <asp:listbox id="lstPacks" style="Z-INDEX: 108; LEFT: 209px; POSITION: absolute; TOP: 128px" runat="server"
				Height="298" Width="456" SelectionMode="Multiple" Font-Names="Arial"></asp:listbox>
				<asp:label id="lblFiles" style="Z-INDEX: 101; LEFT: 209px; POSITION: absolute; TOP: 450px" runat="server"
				Width="448" Font-Bold="True" Font-Names="Arial">Scenarios:</asp:label>
				<asp:listbox id="lstFiles" style="Z-INDEX: 109; LEFT: 209px; POSITION: absolute; TOP: 470px" runat="server"
				Height="106" Width="456" SelectionMode="Multiple"></asp:listbox>
				<asp:label id="lblEmojiList" style="Z-INDEX: 101; LEFT: 409px; POSITION: absolute; TOP: 470px" runat="server"
				Width="448" Font-Bold="True" Font-Names="Arial">🟩Validated Variable<br />🛑Variable Under Development<br />🟰Bilateral Variable<br />🟣Parameter for Scenario Creation</asp:label>
				
				<asp:Image ID="imgLogoNEPAD" runat="server" style="Z-INDEX: 109; LEFT: 309px; POSITION: absolute; TOP: 470px"
					Height="106" Width="228" ImageUrl="~/Images/Logo_AUDA-NEPAD.png" Visible="False" />
				<asp:Image ID="imgRwandaFlag" runat="server" style="Z-INDEX: 109; LEFT: 709px; POSITION: absolute; TOP: 470px"
				Height="106" Width="228" ImageUrl="~/Images/RwandaFlag.png" Visible="False" />

			    <asp:label id="lblDim" style="Z-INDEX: 114; LEFT: 705px; POSITION: absolute; TOP: 100px" runat="server"
				Font-Bold="True" Font-Names="Arial">Geography:</asp:label>
				<input id="chkAllDim1s" type="checkbox" onclick="lstDimSelectAll_Click();" style="Z-INDEX: 114; LEFT: 805px; POSITION: absolute; TOP: 100px"/>
				<input id="cmdMirror" type="button" value="Mirror" onclick="cmdMirror_Click();" style="Z-INDEX: 114; LEFT: 1005px; POSITION: absolute; TOP: 100px"/>
		        
				<asp:listbox id="lstDim" style="Z-INDEX: 100; LEFT: 705px; POSITION: absolute; TOP: 128px" runat="server"
				    Height="298" Width="168" SelectionMode="Multiple"></asp:listbox>

			    <asp:listbox id="lstDim2" style="Z-INDEX: 117; LEFT: 905px; POSITION: absolute; TOP: 128px" runat="server"
				    Height="298" Width="168" SelectionMode="Multiple"></asp:listbox>
			    <asp:label id="lblDim2" style="Z-INDEX: 116; LEFT: 905px; POSITION: absolute; TOP: 100px" runat="server" 
			    Font-Names="Arial" Font-Bold="True" Width="132px">2nd Dimensions:</asp:label>
				
			    <asp:listbox id="lstDim3" style="Z-INDEX: 119; LEFT: 1105px; POSITION: absolute; TOP: 128px"
				    runat="server" SelectionMode="Multiple" Width="168" Height="298"></asp:listbox>
			    <asp:label id="lblDim3" style="Z-INDEX: 118; LEFT: 1105px; POSITION: absolute; TOP: 100px" runat="server"
				    Font-Bold="True" Width="127px" Font-Names="Arial">3rd Dimensions:</asp:label>
				    
				<asp:button id="cmdContinue" style="Z-INDEX: 104; LEFT: 576px; POSITION: absolute; TOP: 546px"
				tabIndex="5" runat="server" Width="112px" Text="Continue" Visible="False" Font-Names="Arial"></asp:button>
				<asp:button id="cmdExport" style="Z-INDEX: 105; LEFT: 287px; POSITION: absolute; TOP: 582px" tabIndex="2"
				runat="server" Width="112px" Text="Export Selection"></asp:button>
				<asp:button id="cmdLine" style="Z-INDEX: 106; LEFT: 159px; POSITION: absolute; TOP: 582px" tabIndex="1"
				runat="server" Width="112px" Text="Line Graph" Font-Names="Arial"></asp:button>
				<asp:button id="cmdTable" style="Z-INDEX: 107; LEFT: 31px; POSITION: absolute; TOP: 582px" runat="server"
				Width="112px" Text="Table"></asp:button>
			    <asp:button id="cmdAnnotation" style="Z-INDEX: 112; LEFT: 415px; POSITION: absolute; TOP: 582px"
				tabIndex="3" runat="server" Width="112px" Text="Explain Scenario"></asp:button>
    			<asp:button id="cmdMap" style="Z-INDEX: 120; LEFT: 543px; POSITION: absolute; TOP: 582px" accessKey="M"
				tabIndex="4" runat="server" Width="112px" Text="Map"></asp:button>
                <asp:DropDownList ID="cboFilter" style="Z-INDEX: 121; LEFT: 671px; POSITION: absolute; TOP: 582px" accessKey="F"
				tabIndex="6" runat="server" Width="200px" AutoPostBack="True"></asp:DropDownList>

			</div>

<script type="text/javascript">

if ((document.forms[0].txtHorizon != null) && (document.forms[0].txtHorizon.disabled == false)) {
    document.forms[0].txtHorizon.focus();
}
</script>
				
				</form>
	</body>
</html>

<script async src="https://www.googletagmanager.com/gtag/js?id=G-GLYCMH7BRR"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-GLYCMH7BRR');
</script>

                  <!--MTI 2026/05/18 Searchbox-->
                  <script type="text/javascript">
    function selectMatchingPack() {
        var searchText = document.getElementById('<%= txtPackSearch.ClientID %>').value.toLowerCase();
        var listBox = document.getElementById('<%= lstPacks.ClientID %>');

        if (searchText.length === 0) {
            return;
        }

		for (var i = 0; i < listBox.options.length; i++) {
            //jrs 2026/05/21 changed from text to value to avoid problems with emoji
            var itemText = listBox.options[i].value.toLowerCase();

            if (itemText.indexOf(searchText) === 0) {

                for (var j = 0; j < listBox.options.length; j++) {
                    listBox.options[j].selected = false;
                }

                listBox.options[i].selected = true;
                listBox.selectedIndex = i;
				listBox.options[i].scrollIntoView({ block: "nearest" });
				//jrs 2026/06/05 need to call the click event to update the dimensions list based on the pack selection
                listBox.dispatchEvent(new Event('click'));

                return;
            }
        }
    }
                  </script>
            <!--MTI 2026/05/18 Searchbox-->

