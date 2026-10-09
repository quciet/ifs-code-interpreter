<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_AddDelProvinces" Codebehind="frm_AddDelProvinces.aspx.vb" %>
<%@ Register TagPrefix="aiswc" Namespace="ArtinSoft.WinFormsToWeb.WebControls" Assembly="ArtinSoft.WinFormsToWeb.WebControls" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<%@ Register assembly="MarkItUp.WebControls.Timer" namespace="MarkItUp.WebControls" tagprefix="miu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Add Delete Raw Sub-Region Data</title>
        <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
        <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
        <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script></head>
<body leftMargin="0" topMargin="0" rightMargin="0" >
		<script type="text/javascript">

            function checkFolderToReSave(sFolderExists, ModelRunningWith) {
				//Check if there was a backup file to indicate if another file was previously loaded
				//----------------------------------------------------------------------------------
				if (ModelRunningWith != "Base") {
					if (sFolderExists == 'True') {
						var ans = confirm("Setup For " & ModelRunningWith & " Already Exist. Do you still want to overwright it?")
						if (!ans) {
							return false;
						}
						else {
							return true;
						}
					}
					else {
						return true;
					}
				}
				else {
					if (sFolderExists == 'False') {
						alert('The process will automatically save current setup/files. After that add process will start. So it will take a little more time. ');
						return true;
					}
					else {
						var ans = confirm("Setup For No State/Province Already Exist. Do you still want to overwright it?");
                        if (!ans) {
                            return false;
                        }
                        else {
                            return true;
                        }
					}
				}
			};

            function checkFolderToReSaveB4Switch(sFolderExists, ModelRunningWith) {
                //Check if there was a backup file to indicate if another file was previously loaded
                //----------------------------------------------------------------------------------
				txtSaveB4Switch.value = 'False';
				if (sFolderExists == 'False') {					
					var ans = confirm("Setup For " + ModelRunningWith + " Does Not Exist. Do You Want To Save It?");
					if (ans) {
						txtSaveB4Switch.value = 'True';
                        alert("After saving " + ModelRunningWith + " switching process will start. So, it will take little more time.");
					}
				}
				return true;
            };

			function checkB4Delete() {
                var doc = document.forms[0];
				if (doc.lstCtryProv.length == 0) {
					alert("Model is running without state/province data. There is nothing to delete.");
					return false;
				}
				else {
					if (lstCtryProv.selectedIndex < 0) {
						alert("Nothing is selected from country subregion list box to delete.");
						return false;
					}
					else {
						//jrs 2022/12/08
						bDeletingProcess.Value = 'True';
                        return true;
                    }
                }
            };

			function checkToAdd(ModelRunningWith) {
                //Check if there was a backup file to indicate if another file was previously loaded
                //----------------------------------------------------------------------------------
				if (lstRegions.value != "") {
                    if (ModelRunningWith != "Base") {
                        var ans = confirm("Are you sure you want to add " & lstRegions.Text & " on top of " & ModelRunningWith)
						if (!ans) {
							return false;
						}
						else {
							return true;
						}
                    }
                    else {
                        if (CheckAlreadyAdded(lstRegions.value) == 'False') {
                            return false;
                        }
                    }
				}
				else {
					alert("You must select a country / region to add");
					return false;
				}
			};

			function CheckAlreadyAdded(rname) {
                for (i = 0; i < lstSwitchHdn.length; i++) {
                    if (lstSwitchHdn[i].toUpperCase() == rname.toUpperCase()) {
						var txt = rname + " already exists in the switch list, please remove it in order to build it again or use the switch procedure to load this setup.";

                        alert(txt);
                        return 'False';

                    }

				}
                return 'True';

			};

			function CheckDelete() {
				if (lstSwitch.length == 0) {
					alert("There is nothing to remove.");
					return false;
				}
				else {
					if (lstSwitch.value == "") {
						alert("There is no item selected to remove.");
						return false;
					}
					else {
                        if (lstSwitch.value == "No State/Province") {
                            alert("Removing Base Setup (No State/Province) is not allowed. It has been reserved to switch anytime and set the model for base mode.");
                            return false;
						}
					}
				}
				return true;
			}
            
        </script> 
 
    <form id="frm_AddDelProvinces" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Add Delete Raw Sub-Region Data" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False">
			</cswm:webmenu>

				<table>
					<tr>
						<td width=28px></td>
						<td width=400px>
							<br />
							<asp:Label ID="lblExplanation" runat="server" BackColor="Window" BorderStyle="None"
                        Font-Bold="True" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="10pt"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="19px"
                        Text="Please select country name to view its availabe regions:"
                        Width="400px"></asp:Label>
						</td>
						<td width=400px>
							<br />
							<asp:Label ID="lblAvailableRuns" runat="server" BackColor="Window" BorderStyle="None"
								Font-Bold="True" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="10pt"
								 ForeColor="WindowText" Height="19px" Text="Available Runs:" Visible="False"></asp:Label>
						</td>
					</tr>

					<tr>
						<td width=28px></td>
						<td width=400px>
							<asp:ListBox ID="lstCountries" runat="server" Width="380px" Height="147px" AutoPostBack="True"></asp:ListBox>
						</td>
						<td width=400px>
							<asp:ListBox ID="lstSwitch" runat="server" Width="380px" Height="144px" Visible="False"></asp:ListBox>
						</td>
					</tr>

					<tr>
						<td width=28px></td>
						<td width=400px>
							<br />
							<asp:Label ID="Label1" runat="server" BackColor="Window" BorderStyle="None"
                        Font-Bold="True" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="10pt"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="19px"
                        Text="Please select region name and click Add or Delete to Process."
                        Width="400px"></asp:Label>
						</td>
						<td width=400px>
						</td>
					</tr>

					<tr>
						<td width=28px></td>
						<td width=400px>
							<asp:ListBox ID="lstRegions" runat="server" Width="380px" Height="147px"></asp:ListBox>
						</td>
						<td width=400px>
							<asp:Button ID="cmdRefresh" runat="server" Text="Refresh (L)ist" AccessKey="l" Width="100px" Visible="False" />
							&nbsp;&nbsp;
							<asp:Button ID="cmdRemove" runat="server" Text="(R)emove" AccessKey="r" Width="100px" Visible="False" />
							&nbsp;&nbsp;
							<asp:Button ID="cmdSwitch" runat="server" Text="(S)witch" AccessKey="s" Width="100px" Visible="False" />
						    <br />
                            <br />
                            <br />
                            <br />
                            <br />
						</td>
					</tr>

					<tr>
						<td width=28px></td>
						<td width=400px>
							<br />
							<asp:Label ID="Label2" runat="server" BackColor="Window" BorderStyle="None"
                        Font-Bold="True" Font-Italic="False" Font-Name="Arial" Font-Names="Arial" Font-Size="10pt"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="WindowText" Height="19px"
                        Text="Countries with Sub-Regions in the system:"
                        Width="400px"></asp:Label>
						</td>
						<td width=400px>
						</td>
					</tr>

					<tr>
						<td width=28px></td>
						<td width=400px>
							<asp:ListBox ID="lstCtryProv" runat="server" Width="380px" Height="147px"></asp:ListBox>
						</td>
						<td width=400px>
							<asp:Button ID="cmdAdd" runat="server" Text="(A)dd" AccessKey="a" Width="100px" />
							&nbsp;&nbsp;
							<asp:Button ID="cmdDelete" runat="server" Text="(D)elete" AccessKey="d" Width="100px" />
							&nbsp;&nbsp;
							<asp:Button ID="cmdExit" runat="server" Text="E(x)it" AccessKey="x" Width="100px" />
						    <br />
                            <br />
                            <br />
							<asp:Button ID="cmdSave" runat="server" Text="(S)ave Current Setup" AccessKey="s" Width="144px" />
							&nbsp;&nbsp;
							<asp:Button ID="cmdShowSwitch" runat="server" Text="S(w)itch Provinces" AccessKey="w" Width="144px" />
						</td>
					</tr>

				</table>

            <p>
			<miu:timer id="Timer1" runat="server" Interval="5000" MaxTicks="1" CommandArgument="1"></miu:timer>
			    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblJSReb" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="cmdRebuildBase" runat="server" Text="Rebuild (B)ase" AccessKey="b" Width="100px" Visible="False" />
            </p>
		<input id="txtSaveB4Switch" name="txtSaveB4Switch"  type="hidden" runat="server"  />
		<input id="bDeletingProcess" name="bDeletingProcess"  type="hidden" runat="server"  />
    </form>
</body>
</html>
