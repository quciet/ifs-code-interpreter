<%@ Page Language="VB" AutoEventWireup="false" Inherits="IFsWeb.frm_CreateVarList" Codebehind="frm_CreateVarList.aspx.vb" %>

<!-- cdv 2022/05/19 ****START**** -->
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 80px; margin-top: 40px">
            <asp:Label ID="Label1" runat="server" Text="Variable List Name:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" Width="167px" TabIndex="0"></asp:TextBox> 
            <br />
            <br />
            &nbsp;<asp:Button ID="Save" runat="server" Text="Save List" Height="40px" Width="104px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Cancel" runat="server" Text="Cancel" Height="40px" Width="103px" />
        </div>
    </form>
    <script type="text/javascript">
        document.getElementById("TextBox1").focus();
    </script>
</body>
</html>
<!-- cdv 2022/05/19 ****END**** -->
