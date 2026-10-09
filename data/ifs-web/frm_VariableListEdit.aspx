<!-- cdv 2022/05/19 ****START****-->
<%@ Page Language = "VB" AutoEventWireup ="false" Inherits="IFsWeb.frm_VariableListEdit" Codebehind="frm_VariableListEdit.aspx.vb" %>
<%@ Register TagPrefix="cswm" Namespace="Coalesys.WebMenu" Assembly="Coalesys.WebMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
	<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
    
    <title></title>
    <style type="text/css">
        #Save {
            width: 94px;
            height: 29px;
        }
        #form1 {
            height: 406px;
            margin-bottom: 20px;
        }
        .auto-style1 {
            left: 200px;
        }
    </style>
    <script type="text/javascript">
        function confirmChanges() {
            var confirmedChanges = confirm("Are you sure you want to permanently save your changes? Any deletions cannot be undone");
            if (confirmedChanges) {
                var button = document.getElementById('<%= confirmedSave.ClientID %>');
                button.click();
            }
        }
        function checkBox() {
            document.getElementById('<%=OverwriteCheckbox.ClientID%>').checked = true
        }

        function uncheckBox() {
            document.getElementById('<%=OverwriteCheckbox.ClientID%>').checked = false
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div style="width: 513px; float : left; clear:both; margin-left:50px" >
            List Name
            <br />
            <asp:TextBox ID="txtListName" runat="server" Width="491px"></asp:TextBox>
            <br />
            <br />
            Lists
            <br />
            <asp:ListBox ID="LstListName" runat="server" Height="274px" Width="500px" OnSelectedIndexChanged="LstListName_SelectedIndexChanged" AutoPostBack="True" SelectionMode="Multiple"></asp:ListBox>
         </div>
         <div style ="float:inline-end; height: 324px;">
             <table>
                 <tbody>
                     <tr>
                         <td>
                            Variables<br />
                            <asp:ListBox ID="LstVariableList" runat="server" Height="293px" Width="310px" SelectionMode="Multiple"></asp:ListBox>
                         </td>
                         <td>
                              <asp:Label ID="FormulaID" runat="server" Text="FormulaGoesHere" Visible ="False"></asp:Label><br />
                              <asp:ListBox ID="LstComputationList" runat="server" Height="290px" Visible="False" Width="250px"></asp:ListBox>
                         </td>
                     </tr>
                 </tbody>
              </table>
         </div>
            <br />
            <div style = "position: relative; vertical-align:middle; display:inline-flex; top: 1px; left: 0px; height: 35px; float:left; clear:none;">
                <div style="height: 90px; width: 600px">
                    <button id="Save" onclick="confirmChanges(); return false;" style="height:35px; width:100px; display:inline; position:absolute; padding-top:0px; margin-top:0px; top: 0px; left: 0px;" > Save Changes</button>
                    <asp:Button ID="AddNewList" runat="server" Height="35px" Text="Add New List" Width="100px" Padding="5px" Style = "display:inline;" />
                    <asp:Button ID="Exit" runat="server" Height="35px" Text="Exit" Width="100px" Padding="5px" Style = "display:inline;"/>
                    <asp:Button ID="Export" runat="server" OnClientClick="uncheckBox()" Text="Export Selected 
    Lists" Width="100px" Height="35px" Padding="5px" Font-Size="X-Small" Style = "display:inline; position:absolute; padding-top:0px; margin-left:5px; margin-top:0px;" CssClass="auto-style1"/>
                    <br /><br />
                    <asp:Button ID="DeleteList" runat="server" Height="35px" Text="Delete Lists" Width="100px" Padding="5px" Style = "display:inline;padding-top:5px;" OnClientClick="if (confirm('You are about to delete all selected list, click yes if you want to continue. This cannot be undone')){return true}else{return false}"/>
                    <asp:Button ID="DeleteVariable" runat="server" Height="35px" Text="Delete Variables" Width="100px" Padding="5px" Style = "display:inline;padding-top:5px;" OnClientClick="if (confirm('You are about to delete all selected variables, click yes if you want to continue. This cannot be undone')){return true}else{return false}"/>
                    <asp:Button ID="ImportListButton" Style = "display:inline; position:absolute; padding-top:5px; margin-left:5px; margin-top:0px;" Text= "Import Selected 
    File" runat="server" Font-Size="X-Small" Height="35px" Width="100px"/>
                    <asp:FileUpload runat="server" ID="ImportList" Style = "display:inline; width:250px; height:35px;  position:absolute; padding-top:0px; margin-top:0px; top: 58px; left: 315px;"/>
                    <br /><br />
                    <asp:Label ID="lblInstrucions" runat="server" Text="" Visible ="False"></asp:Label>
                    <br />
                    <asp:Button ID="cmdUpdLstNm" runat="server" Height="35px" Text="Update List Name" Width="150px" Padding="5px" Style = "display:inline;padding-top:5px;" />
                    <asp:Button ID="cmdEditUnits" runat="server" Height="35px" Text="Edit Units (Y axis label)" Width="150px" Padding="5px" Style = "display:inline;padding-top:5px;" />
                    <br />
                    <asp:TextBox ID="txtUnits" runat="server" Width="491px" Visible ="false"></asp:TextBox>
                    <br />
                    <asp:Button ID="cmdOkUnits" runat="server" Height="35px" Text="Ok" Width="100px" Padding="5px" Style = "display:inline;padding-top:5px;" visible="false"/>
                    <br />
                    <asp:TextBox ID="txtNewLstNm" runat="server" Width="491px" Visible="false"></asp:TextBox>
                    <br />                    
                    <asp:Button ID="cmdOkNewLstNm" runat="server" Height="35px" Text="Ok" Width="100px" Padding="5px" Style = "display:inline;padding-top:5px;" visible="false"/>
                    <br />
                </div> 
            </div>
            <br />
        <div style="float:inline-start; margin-left:50px">
            <br /><br /><br /><br /><br /><br />
            <p>&nbsp;</p>
            <p><asp:Label ID="lblCat" runat="server" Text="Category" Visible ="True"></asp:Label></p>
            <p>
                <asp:TextBox ID="txtCategory" runat="server" Width="279px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnUpdateCategory" runat="server" Height="23px" Text="Update Category" Width="118px" />
            </p>
            <p>
                <asp:DropDownList ID="lstCategoryList" runat="server" Height="29px" Width="286px" OnSelectedIndexChanged="lstCategoryList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            
            </p>
            <!-- dsb 2025/10/21 ****START**** -->
            <p>
                <asp:Label ID="lblPriority" runat="server" Text="Priority List:" Visible="True"></asp:Label>
            </p>
            <p>
                <!--dsb 2025/12/9 Added selectedIndexChanged -->
                <asp:CheckBoxList ID="chkPriorityList" runat="server" AutoPostBack="true" OnSelectedIndexchanged="chkPriorityList_SelectedIndexChanged">
                </asp:CheckBoxList>
            </p><!-- dsb 2025/10/21 ****END**** -->
        </div>
        <div style="display:none">
            <asp:button ID="confirmedSave" ClientID="confirmedSaveClient" runat ="server" OnClick="confirmedSave_Click" AutoPostBack="false" ClientIDMode="Static"/>
            <asp:CheckBox ID="OverwriteCheckbox" clientID="OverwriteCheckboxID" runat="server"/>
        </div>
        <div>
            <asp:Label style="padding-left:50px;  display:block" ID="ChangesAlert" runat="server" Font-Bold="True" Font-Names="Adobe Gothic Std B" Font-Overline="False" Font-Size="X-Large" ForeColor="Red" Text="You Have Unsaved Changes. If you close IFs now, all unsaved changes will be lost." Visible="False"></asp:Label>
        </div>

        <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" ViewStateMode="Enabled">
        </asp:Timer>

        </form>
</body>

</html>
<!-- cdv 2022/05/19 ****END****-->
