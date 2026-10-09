<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_Datadict" Codebehind="frm_Datadict.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Data Dictionary Values</title>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <script type="text/javascript">

        function checkToClear() {
            var ans = confirm("Would you like to clear the form?" )
            if (!ans) {
                return false;
            }
            else {
                return true;
            }
        };

        function checkToAdd() {
            var doc = document.forms[0];
            if (doc.txtDefinition.value == "") {
                alert("You must input a definition.");
                return false;
            }

            if (doc.txtNewCategory.value == "") {
                alert("You must input a category.");
                return false;
            }

            if (doc.txtSource.value == "") {
                alert("You must input a source.");
                return false;
            }

            if (doc.txtUnits.value == "") {
                alert("You must input a unit.");
                return false;
            }

            if (doc.txtAggregation.value == "") {
                alert("Aggregation rule cannot be blank. Put NONE for no aggregation rule.");
                return false;
            }

            if (doc.txtYears.value == "") {
                alert("You must input years covered in this dataset.");
                return false;
            }

            return true;
        };
    </script> 

    <form id="frm_Datadict" runat="server">
        	<asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
                    <asp:TableCell  ID="LogoCell" Width="40px">
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>			
					<asp:TableCell Text="Data Dictionary Values" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            <br />
        <table>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="lblVariable" runat="server" Text="Variable Name"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtVariable" runat="server" Width="242px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="lblDefinition" runat="server" Text="Definition:"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtDefinition" runat="server" Width="685px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="lblCategory" runat="server" Text="Choose Category "></asp:Label></td>
                <td width="25%"><asp:DropDownList ID="CmbVCat" runat="server" Width="141px" AutoPostBack="True"></asp:DropDownList></td>
                <td width="25%"><asp:Label ID="lblnewcat" runat="server" Text="Or, Type Category Name (If New Category)"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtNewCategory" runat="server" Width="242px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label1" runat="server" Text="Sub Category"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtSubGroup" runat="server" Width="242px"></asp:TextBox></td>
                <td width="25%"><asp:Label ID="Label2" runat="server" Text="Aggregation Rule"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtAggregation" runat="server" Width="242px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label3" runat="server" Text="Series"></asp:Label></td>
                <td width="25%"><asp:DropDownList ID="cmbSeries" runat="server" Width="141px"></asp:DropDownList></td>
                <td width="25%"><asp:Label ID="Label4" runat="server" Text="Cohort"></asp:Label></td>
                <td width="25%"><asp:DropDownList ID="cmbCohort" runat="server" Width="141px"></asp:DropDownList><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="lblCodeinsource" runat="server" Text="Code In Source"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtCodeInSource" runat="server" Width="242px"></asp:TextBox></td>
                <td width="25%"><asp:Label ID="lblFormula" runat="server" Text="Formula"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtFormula" runat="server" Width="242px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label5" runat="server" Text="Data Source"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtSource" runat="server" Width="242px"></asp:TextBox></td>
                <td width="25%"><asp:Label ID="Label6" runat="server" Text="Original Source"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtOriginal" runat="server" Width="242px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label7" runat="server" Text="Extended Source Definition:"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtExtended" runat="server" Width="685px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label8" runat="server" Text="Exact Name of the series in the data source:"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtNameinSource" runat="server" Width="685px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label9" runat="server" Text="Unit"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtUnits" runat="server" Width="242px"></asp:TextBox></td>
                <td width="25%"><asp:Label ID="Label10" runat="server" Text="Decimal Places"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtDecimal" runat="server" Width="93px"></asp:TextBox></td>
            </tr>
            <tr>
                <td widh="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label12" runat="server" Text="Notes"></asp:Label></td>
                <td widh="25%"><asp:TextBox ID="txtNotes" runat="server" Width="242px"></asp:TextBox></td>
                <td widh="25%"><asp:Label ID="Label11" runat="server" Text="Years"></asp:Label></td>
                <td widh="25%"><asp:TextBox ID="txtYears" runat="server" Width="242px"></asp:TextBox><br /></td>
            </tr>
            <tr>
                <td width="25%">&nbsp&nbsp&nbsp<asp:Label ID="Label14" runat="server" Text="Latest IFs Update"></asp:Label></td>
                <td width="25%"><asp:TextBox ID="txtlastIFs" runat="server" Width="242px"></asp:TextBox></td>
                <td width="25%"><asp:Label ID="Label15" runat="server" Text="Proprietory"></asp:Label>&nbsp&nbsp&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="cmbProprietory" runat="server" Width="141px"></asp:DropDownList></td>
                <td width="25%"><asp:Label ID="lblForeCast" runat="server" Text="Forecast"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="cmbForecast" runat="server" Width="141px"></asp:DropDownList></td>
            </tr>
        </table>
                                               
            
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdViewDataDict" runat="server" Text="View Master Dictionary" Width="200px" />&nbsp;&nbsp;&nbsp; <asp:Button ID="cmdOK" runat="server" Text="OK" Width="200px" />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdReset" runat="server" Text="Clear Form" Width="200px" />&nbsp;&nbsp;&nbsp; <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="200px" />
    </form>

    
</body>
</html>
