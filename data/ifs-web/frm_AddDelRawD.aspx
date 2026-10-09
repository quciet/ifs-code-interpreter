<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_AddDelRawD" Codebehind="frm_AddDelRawD.aspx.vb" %>
<%@ Register assembly="MarkItUp.WebControls.Timer" namespace="MarkItUp.WebControls" tagprefix="miu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Delete Raw Country Data</title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0" >
	<script type="text/javascript">

		function addProcessMsg() {
            var doc = document.forms[0];
            var msg = "This process Adds a row with the New Country in all tables that "
            msg = msg + "  have a Country in it. Files Affected are IFs, IFsHistSeries, ";
            msg = msg + " IFsCoVaTra, IFsCoVaTraSeries and IFsWVSCohort. If you want to cancel this option click exit.";

            var grd = document.getElementById("GridView1");

			if (grd) {
				alert(msg);
			}
			else {
				var txt1 = doc.txtCountry.text;
                var txt2 = doc.txtFIPSCD.text;
				if (txt1 == "" || txt2 == "") {
                    alert("You must type a Country name and a FIPS CODE");
				}
            }
        }

        function delProcessMsg() {
            var doc = document.forms[0];
            var msg = "This process Deletes rows with the current Country in all tables that "
            msg = msg + "have that Country in it. Files Affected are IFs, IFsHistSeries, ";
            msg = msg + " IFsCoVaTra, IFsCoVaTraSeries and IFsWVSCohort. If you want to cancel this option click exit.";

            var grd = document.getElementById("GridView1");

            if (grd) {
                alert(msg);
			}
            else {
                var txt1 = doc.txtCountry.text;
                if (txt1 == "") {
                    alert("You must select a Country to delete");
                }
            }
        }

        function updProcessMsg() {
            var doc = document.forms[0];
            var msg = "This process Updates the country name in rows with the current Country in all tables that "
            msg = msg + "have that Country in it. Files Affected are IFs, IFsHistSeries, ";
            msg = msg + " IFsCoVaTra, IFsCoVaTraSeries and IFsWVSCohort. If you want to cancel this option click exit.";

            var grd = document.getElementById("GridView1");

            if (grd) {
                alert(msg);
            }
            else {
                var txt1 = doc.txtCountry.text;
                var txt2 = doc.txtNewCountry.text;
                if (txt1 == "" || txt2 == "") {
                    alert("You must type a Country name and a new Country name");
                }
            }        }

    </script> 
    <form id="frm_AddDelRawD" runat="server">
			<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Add Delete Raw Country Data" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>

			<table>
				<tr>
					<td width=200px align="center">
						<asp:Button ID="cmdAdd" runat="server" Text="(A)dd" AccessKey="a" Width="100px" />
					</td>
					<td width=200px align="center">
						<asp:Button ID="cmdUpdate" runat="server" Text="(U)pdate" AccessKey="u" Width="100px"/>
					</td>
					<td width=200px align="center">
						<asp:Button ID="cmdDelete" runat="server" Text="(D)elete" AccessKey="d" Width="100px"/>
					</td>
					<td width=200px align="center">
						<asp:Button ID="cmdExit" runat="server" Text="E(x)it" AccessKey="x" Width="100px"/>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True">
                            <SelectedRowStyle BackColor="#FFFF66" />
                        </asp:GridView>
					</td>
				</tr>
				<tr>
					<td colspan="4">
						<asp:Panel ID="pnlData" runat="server" Visible="False">
							<table>
								<tr>
									<td>
										<asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label></td>
									<td>
										<asp:TextBox ID="txtCountry" runat="server" Width="300px"></asp:TextBox></td>
								</tr>
								<tr>
									<td><asp:Label ID="lblFIPSCD" runat="server" Text="FIPS CODE:"></asp:Label></td>
									<td><asp:TextBox ID="txtFIPSCD" runat="server"></asp:TextBox></td>
								</tr>
								<tr>
									<td><asp:Label ID="lblNewCountry" runat="server" Text="New Country Name:"></asp:Label></td>
									<td><asp:TextBox ID="txtNewCountry" runat="server" Width="300px"></asp:TextBox></td>
								</tr>
							</table>
						</asp:Panel>
					</td>
				</tr>
			</table>

            <p>
			<miu:timer id="Timer1" runat="server" Interval="5000" MaxTicks="1" CommandArgument="1"></miu:timer>
			    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblJSReb" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
            </p>
    </form>
</body>
</html>
