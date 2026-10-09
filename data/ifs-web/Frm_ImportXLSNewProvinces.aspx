<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.Frm_ImportXLSNewProvinces" Codebehind="Frm_ImportXLSNewProvinces.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import Excel Data</title>
    <script language="javascript" type="text/javascript" src="Scripts/actb.js"></script>
    <script language="javascript" type="text/javascript" src="Scripts/common.js"></script>
    <script language ="javascript" type="text/javascript" src="Scripts/AjaxMethodsHandler.js"></script>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="Frm_ImportXLSNew" runat="server">
        	<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Import Provinces Excel Data" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
			<cswm:webmenu id="cswmWebMenu" style="Z-INDEX: 104; LEFT: 0px; POSITION: absolute; TOP: 16px"
				runat="server" MenuBar-AbsoluteDragEnabled="False" MenuBar-AbsoluteDockEnabled="False">
			</cswm:webmenu>
		<br /><br />
		<table width="100%">
			<tr>
				<td><asp:Label ID="lblDatabase" runat="server" Text="IFs Database:  "></asp:Label>&nbsp;&nbsp;</td>
				<td><asp:TextBox ID="txtDatabaseFile" runat="server" Width="251px">IFsHistSeriesXXX.db</asp:TextBox></td>
				<td><asp:FileUpload ID="fuOpenFile" runat="server" accept=".xls, .xlsx" /><br />
					<asp:TextBox ID="txtFileSelected" runat="server" BackColor="Yellow" Width="208px"></asp:TextBox>
				</td>
				<td width="40%" rowspan="6" style="vertical-align:top">
					<asp:Panel ID="FrameSeriesOrientation" runat="server" GroupingText="Choose Excel Source Data Format:" >
						<asp:RadioButton ID="OptSeriesType0" runat="server" Text="Multiple  Series Down; Countries in Columns, Years in Rows " ForeColor="Blue" Enabled="False" /><br />
						<asp:RadioButton ID="OptSeriesType1" runat="server" Text="Multiple Series Down; Countries in Rows, Years in Columns " ForeColor="Red" Enabled="False" /><br />
						<asp:RadioButton ID="OptSeriesType2" runat="server" Text="Multiple Series Across; Years within Country " ForeColor="Blue" GroupName="FormatType" AutoPostBack="True" /><br />
						<asp:RadioButton ID="OptSeriesType3" runat="server" Text="Multiple Series Across; Countries within Year " ForeColor="Red" Enabled="False" /><br />
						<asp:RadioButton ID="OptSeriesType4" runat="server" Text="Single Series; Countries in Columns, Years in Rows " ForeColor="Blue" GroupName="FormatType" Enabled="False" /><br />
						<asp:RadioButton ID="OptSeriesType5" runat="server" Text="Single or Multiple Series; Countries in Rows, Years in Columns " ForeColor="Red" Checked="True" GroupName="FormatType" AutoPostBack="True" />
					</asp:Panel>
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblTable" runat="server" Visible="false" Text="IFs Series:"></asp:Label>&nbsp;&nbsp;</td>
				<td><asp:TextBox ID="txtTable" visible="false" runat="server">SeriesVariable</asp:TextBox></td>
				<td>
					<table>
						<tr>
							<td>Choose sheet: <br /><asp:listbox ID="lstSheets" runat="server" Height="76px" Width="237px" AutoPostBack="True"></asp:ListBox></td>
						</tr>
						<tr><td><asp:Button ID="cmdRefreshSheets" runat="server" Text="Refresh Sheets" /></td></tr>
					</table>                    
               </td>
			</tr>			
			<tr>
				<td><asp:Label ID="lblCountry4PRovinces" runat="server" Text="Select Country to which provinces belong:  "></asp:Label>&nbsp;&nbsp;</td>
				<td><asp:listbox id="lstCountry4Province" runat="server" Height="185px" Width="180px" AutoPostBack="True"></asp:listbox></td>
			</tr>
			<tr>
				<td colspan="3">Province Names in Excel Sheet Start at:</td>
			</tr>
			<tr>
				<td>
					<table>
						<tr>
							<td width="50%">Column: </td>
							<td width="50%"><asp:DropDownList ID="CmbCountryCol" runat="server" AutoPostBack="True"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Row:</td>
							<td width="50%"><asp:DropDownList ID="CmbCountryRow" runat="server" AutoPostBack="True"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Country: </td>
							<td width="50%"><asp:TextBox ID="TxtCountryFR" runat="server"></asp:TextBox></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td colspan="3">Years in Excel Sheet Start at:</td>
			</tr>
			<tr>
				<td>
					<table>
						<tr>
							<td width="50%">Column: </td>
							<td width="50%"><asp:DropDownList ID="CmbYearCol" runat="server" AutoPostBack="True"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Row:</td>
							<td width="50%"><asp:DropDownList ID="CmbYearRow" runat="server" AutoPostBack="True"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Year: </td>
							<td width="50%"><asp:TextBox ID="TxtYearFR" runat="server"></asp:TextBox></td>
						</tr>
					</table>
				</td>
			</tr>

			<tr>
				<td colspan="3">Data in Excel Sheet Start at:</td>
			</tr>
			<tr>
				<td>
					<table>
						<tr>
							<td width="50%">Column: </td>
							<td width="50%"><asp:DropDownList ID="cmbDataCol" runat="server" AutoPostBack="True" Enabled="False"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Row:</td>
							<td width="50%"><asp:DropDownList ID="cmbDataRow" runat="server" AutoPostBack="True" Enabled="False"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Data: </td>
							<td width="50%"><asp:TextBox ID="txtDataFR" runat="server" Enabled="False"></asp:TextBox></td>
						</tr>
					</table>
				</td>
			</tr>


			<tr>
				<td colspan="3">IFs Data Dictionary Values</td>
				<td rowspan="2">						
					<asp:Image ID="PicSeriesType" runat="server" Height="120px" ImageUrl="~/Images/SSCDYA.jpg" />
				</td>
			</tr>
			<tr>
				<td>
					<table>
						<tr>
							<td width="50%">Aggreg. rule: </td>
							<td width="50%"><asp:DropDownList ID="cmbaggregation" runat="server"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Decimal Places:</td>
							<td width="50%"><asp:DropDownList ID="cmbDecimals" runat="server"></asp:DropDownList></td>
						</tr>
					</table>
				</td>
				<td>
					<table>
						<tr>
							<td width="50%">Existing Variables: </td>
							<td width="50%">
    							<asp:TextBox ID="txtVarSearch" runat="server" Width="128px" autocomplete="off"></asp:TextBox><br />
								<asp:listbox id="lstVariablelist" runat="server" Height="88px" Width="128px" AutoPostBack="True"></asp:listbox>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<table>
						<tr>
							<td width="50%">Variable Name:</td>
							<td width="50%"><asp:TextBox ID="TxtVarName" runat="server"></asp:TextBox></td>
						</tr>
					</table>
				</td>
				<td colspan="2">
					<table>
						<tr>
							<td width="25%">Variable Definition:</td>
							<td width="75%"><asp:TextBox ID="TxtVarDesc" runat="server" Height="71px" Width="329px" TextMode="MultiLine"></asp:TextBox></td>
						</tr>
					</table>
				</td>
				<td style="text-align: center;">
					<asp:Button ID="cmdDatadict" runat="server" Text="Data Dictionary" Height="56px" Width="140px" />
					&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Button ID="CmdImport" runat="server" Text="Import" Height="56px" Width="140px" />
				</td>

			</tr>
			<tr>
				<td>
				</td>
				<td>
					<asp:Label ID="lblMessages" runat="server" Visible="False" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
				</td>
			</tr>
		</table>

		<asp:Button ID="btnInvs" runat="server" Text="Hidden Command" />	
         <asp:Button ID="btnEnterKey" runat="server" Text="Hidden Command" />		

<script language="javascript" type="text/javascript" >	
    document.getElementById("btnInvs").style.visibility = "hidden";
    //JAM 2011-11-19 Defining default button for txtVarSearch field
    document.getElementById("btnEnterKey").style.visibility = "hidden";

    //JAM 2011-11-19 Defining default button for txtVarSearch field
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

	function setVariableFromSearchClick() {
		//jrs 2023/10/31 added .replace, problem with LandUrban&Build
        document.getElementById("lstVariablelist").value = this.actb_currObject.replace("&amp;", "&");
        document.getElementById("btnInvs").click();

    }

    function selectData() {
        document.getElementById("txtVarSearch").select();
    }
    document.getElementById("txtVarSearch").onclick = selectData;

    if (variableList == null) {
        if (document.forms[0].lstVariablelist != null) {
            var lst = document.getElementById("lstVariablelist");
            variableList = new Array(lst.length);

            for (var count = 0; count < lst.length; count++) {
                variableList[count] = lst.options[count].innerHTML;

            }
        }
    }

    function BindDataToVarSearch() {
        var obj = actb(document.getElementById('txtVarSearch'), variableList);
        //KIA add the follwing event listener with your desired method what you want to call on a search list item is clicked
        this.actb_cur_obj_clicked.prototype.itemClicked = setVariableFromSearchClick;
        this.actb_fSize = '12px';
        this.actb_bgColor = '#888888';
        this.actb_textColor = '#FFFFFF';
        this.actb_hColor = '#000000';
        this.actb_fFamily = 'Verdana';
        this.actb_lim = 5;
        this.actb_timeOut = -1;
        this.actb_firstText = true;
        this.actb_hStyle = 'text-decoration:underline;font-weight="bold"';
        document.getElementById('txtVarSearch').value = "";
        document.getElementById('txtVarSearch').select();

    }
	BindDataToVarSearch();

</script>

    </form>
</body>
</html>
