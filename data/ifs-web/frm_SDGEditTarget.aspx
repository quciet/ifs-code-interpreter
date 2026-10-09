<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_SDGEditTarget" Codebehind="frm_SDGEditTarget.aspx.vb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sustainable Development Goal Edit Target</title>
    <meta content="JavaScript" name="vs_defaultClientScript">
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
    <form id="Form1" method="post" runat="server">
            <asp:table id="WinFormsToWeb_PageBar" runat="server" Width="100%" Height="15px" CellSpacing="0">
				<asp:TableRow ForeColor="White" ID="TitleRow" BackColor="#4572A7" Font-Size="Medium" Font-Names="Tahoma"
					Font-Bold="True">
					<asp:TableCell  ID="LogoCell" Width="40px">
                        <!--jam 2022/04/16-->
					<a href="https://korbel.du.edu/pardee"><asp:Image ID="imgLogo" runat="server" ImageUrl="~/Images/pcl.png" Width="30px" title="IFs Home" /></a></asp:TableCell>
					<asp:TableCell Text="Sustainable Development Goal Edit Target" ID="TitleCell"></asp:TableCell>
				</asp:TableRow>
			</asp:table>
            <br />
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%"></td>
                    <td style="width:60%">
                        <asp:RadioButton ID="optAbsolute" Text="Absolute Target" runat="server" Checked="True" GroupName="AbsTar" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="optRelative" text="Relative Target" runat="server" GroupName="AbsTar" />
                    </td>
                    <td style="width:20%"></td>
                </tr>
                <tr>
                    <td style="width:20%"></td>
                    <td style="width:60%">
                        <br />
                        <br />
                        Target Value:&nbsp;<asp:TextBox ID="txtTargetVal" runat="server" ></asp:TextBox>
                    </td>
                    <td style="width:20%"></td>
                </tr>
                <tr>
                    <td style="width:20%"></td>
                    <td style="width:60%">
                        <br />
                        <br />
                        Target Year:&nbsp;&nbsp;<asp:TextBox ID="txtTargetYear" runat="server"></asp:TextBox>
                    </td>
                    <td style="width:20%"></td>
                </tr>
                <tr>
                    <td style="width:20%"></td>
                    <td style="width:60%">
                        <br />
                        <br />
                        <asp:CheckBox ID="chkValence" runat="server" Text="Valence (checked is positive)" />
                    </td>
                    <td style="width:20%"></td>
                </tr>
                <tr>
                    <td style="width:20%"></td>
                    <td style="width:60%">
                        <br />
                        <br />
                        Note: If using Relative Target you need to enter the portion of the base year value you want to use as a target. For example if you want to increase an indicator by 25% you should use 1.25, if you want to reduce by half you should use 0.5. Also note that Valence only affects the coloring of the SDG set form, such that if the Valence is checked, then it&#39;s a positive valence, meaning more of the indicator is desirable.
                    </td>
                    <td style="width:20%"></td>
                </tr>
                <tr>
                    <td style="width:20%"></td>
                    <td style="width:60%">
                        <br />
                        <br />
                        <asp:Button ID="cmdSave" runat="server" Text="Save" Width="100px"  OnClientClick="return performCheck();" CausesValidation="False"/>
                        &nbsp;&nbsp;
                        <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="100px" />
                    </td>
                    <td style="width:20%"></td>
                </tr>
            </table>


            <input id="txtMax" name="txtMax"  type="hidden" runat="server"/>
            <input id="txtMin" name="txtMin"  type="hidden" runat="server"/>
            <input id="txtFirstYear" name="txtFirstYear"  type="hidden" runat="server"/>
            <input id="txtLastYear" name="txtLastYear"  type="hidden" runat="server"/>


    </form>
</body>
</html>

<script language="javascript" type ="text/javascript" >
    function performCheck() {
        //Page_ClientValidate("");
        var doc = document.forms[0];
        //Target Value validation
        var tarVal = doc.txtTargetVal.value;
        if (tarVal > "") {
            if (!Number(tarVal) && tarVal != "0") {
                alert('Target Value needs to be a numerical value');
                return false;
            }
            if (txtMax.value != 0 || txtMin.value != 0) {
                if (parseFloat(tarVal) < parseFloat(txtMin.value) || parseFloat(tarVal) > parseFloat(txtMax.value)) {
                    alert('Target Value needs to be between ' + txtMin.value + ' and ' + txtMax.value);
                    return false;
                }
            }
        }

        //Target Year validation
        var tarYear = doc.txtTargetYear.value;
        if (!Number(tarYear)) {
            alert('Target Year needs to be a numerical value');
            return false;
        }

        if (parseFloat(tarYear) < parseFloat(txtFirstYear.value) || parseFloat(tarYear) > parseFloat(txtLastYear.value)) {
            alert('Target Year needs to be between ' + txtFirstYear.value + ' and ' + txtLastYear.value);
            return false;
        }

        return true;
    }
</script>